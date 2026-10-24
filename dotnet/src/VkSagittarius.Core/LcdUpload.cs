using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace VkSagittarius.Core;

/// <summary>
/// LCD 换背景图/视频。
///
/// 关键差异：LCD 内容不是走 HID 传的。原程序发 mode=97 带上文件名后，设备把自己
/// 切换成 USB 大容量存储（U 盘），宿主把文件拷进根目录，再发 mode=98 让设备重启
/// 到正常模式。Windows 版靠 DriveInfo.GetDrives() 前后对比找新盘符；
/// Linux 端对比 /sys/block 找新块设备，优先用 udisksctl（普通用户）挂载，
/// 失败再退回 root 的 mount。
/// </summary>
public sealed class LcdUploader
{
    private static readonly string[] IgnorePrefixes =
        ["loop", "ram", "zram", "dm-", "md", "sr", "nbd", "fd"];

    private readonly Action<string> _log;
    public LcdUploader(Action<string>? log = null) => _log = log ?? (_ => { });

    public bool Upload(VkDevice lcd, string sourceFile, string? deviceName = null,
                       int appearTimeoutMs = 30000)
    {
        if (lcd.Class != 3) throw new ArgumentException("只有 LCD 设备（类别 3）支持上传背景");
        if (!File.Exists(sourceFile)) throw new FileNotFoundException("源文件不存在", sourceFile);

        var name = deviceName ?? Path.GetFileName(sourceFile);
        var before = SnapshotBlockDevices();

        _log($"→ 通知设备进入存储模式：{name}");
        lcd.LcdName(name);
        Thread.Sleep(1000);

        var node = WaitForNewBlockDevice(before, appearTimeoutMs);
        if (node is null)
        {
            _log("× 未检测到新块设备：确认支持图片/视频格式，且设备处于可控状态");
            return false;
        }
        _log($"→ 设备已出现：{node}");

        var mounted = Mount(node, out var mountPoint);
        if (!mounted || mountPoint is null)
        {
            _log($"× 挂载 {node} 失败（需要 udisks2 或 root 权限）");
            TryApply(lcd);
            return false;
        }

        try
        {
            var dest = Path.Combine(mountPoint, name);
            _log($"→ 复制到 {dest}");
            CopySynced(sourceFile, dest);
            _log("→ 复制完成，准备卸载");
        }
        finally
        {
            Unmount(node, mountPoint);
        }

        Thread.Sleep(2000);
        TryApply(lcd);
        _log("✓ 已发送应用指令（设备会短暂重启）");
        return true;
    }

    private void TryApply(VkDevice lcd)
    {
        try { lcd.LcdSimple(Proto.LcdSub.Apply); }
        catch (Exception ex) { _log($"× 发送应用指令失败：{ex.Message}"); }
    }

    private static void CopySynced(string src, string dest)
    {
        using var input = File.OpenRead(src);
        using var output = new FileStream(dest, FileMode.Create, FileAccess.Write, FileShare.None);
        input.CopyTo(output, 1 << 20);
        output.Flush(true);   // 落盘再卸载，避免 U 盘缓存丢失
    }

    // ---------------------------------------------------------- 块设备发现

    private static HashSet<string> SnapshotBlockDevices()
    {
        var set = new HashSet<string>(StringComparer.Ordinal);
        const string root = "/sys/block";
        if (!Directory.Exists(root)) return set;
        foreach (var dir in Directory.GetDirectories(root))
        {
            var name = Path.GetFileName(dir);
            if (IgnorePrefixes.Any(p => name.StartsWith(p, StringComparison.Ordinal))) continue;
            set.Add(name);
        }
        return set;
    }

    private string? WaitForNewBlockDevice(HashSet<string> before, int timeoutMs)
    {
        var deadline = Environment.TickCount64 + timeoutMs;
        while (Environment.TickCount64 < deadline)
        {
            Thread.Sleep(1000);
            var now = SnapshotBlockDevices();
            var fresh = now.Except(before).ToList();
            if (fresh.Count == 0) continue;

            foreach (var disk in fresh)
            {
                var path = $"/dev/{disk}";
                // 优先第一个分区，设备是 FAT 分区表
                var part = Directory.GetDirectories($"/sys/block/{disk}")
                    .FirstOrDefault(p => Path.GetFileName(p).StartsWith(disk, StringComparison.Ordinal));
                var target = part is null ? path : $"/dev/{Path.GetFileName(part)}";
                if (File.Exists(target)) return target;
                if (File.Exists(path)) return path;
            }
        }
        return null;
    }

    // ---------------------------------------------------------- 挂载

    private bool Mount(string node, out string? mountPoint)
    {
        mountPoint = null;

        if (Run("udisksctl", $"mount -b {node} --no-user-interaction", out var udisksOut, 15000))
        {
            var m = Regex.Match(udisksOut, @"at (?<p>/\S+)");
            if (m.Success && Directory.Exists(m.Groups["p"].Value))
            {
                mountPoint = m.Groups["p"].Value;
                return true;
            }
        }

        if (GetEuid() == 0)
        {
            var dir = Path.Combine("/mnt", "vksag-lcd");
            Directory.CreateDirectory(dir);
            if (Run("mount", $"-t vfat -o rw,umask=000,iocharset=utf8 {node} {dir}", out var err, 15000))
            {
                mountPoint = dir;
                return true;
            }
            _log($"  mount 输出：{err.Trim()}");
        }
        return false;
    }

    private void Unmount(string node, string mountPoint)
    {
        if (!Run("udisksctl", $"unmount -b {node} --no-user-interaction", out _, 15000))
            Run("umount", mountPoint, out _, 15000);
    }

    [System.Runtime.InteropServices.DllImport("libc", EntryPoint = "geteuid")]
    private static extern uint geteuid();

    private static int GetEuid() => (int)geteuid();

    private static bool Run(string file, string args, out string output, int timeoutMs)
    {
        output = "";
        try
        {
            var psi = new ProcessStartInfo(file, args)
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
            };
            using var proc = Process.Start(psi);
            if (proc is null) return false;
            output = proc.StandardOutput.ReadToEnd() + proc.StandardError.ReadToEnd();
            if (!proc.WaitForExit(timeoutMs))
            {
                try { proc.Kill(); } catch (Exception) { }
                return false;
            }
            return proc.ExitCode == 0;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
