using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace VkSagittarius.Core;

public interface IHidTransport : IDisposable
{
    int DeviceClass { get; }
    int Index { get; }
    string Path { get; }
    void Write(ReadOnlySpan<byte> report);
    /// <summary>读取一条输入报告，超时返回 null。</summary>
    byte[]? Read(int timeoutMs = 0);
}

/// <summary>
/// Linux hidraw 后端。Windows 版用 SetupAPI + CreateFile；这里直接 open/read/write
/// /dev/hidrawN，并靠 /sys/class/hidraw 的 uevent 枚举 VID/PID，无第三方依赖。
/// </summary>
public sealed class HidrawTransport : IHidTransport
{
    private const int O_RDWR = 2;
    private const int O_NONBLOCK = 0x800;
    private const short POLLIN = 0x001;

    [StructLayout(LayoutKind.Sequential)]
    private struct PollFd
    {
        public int Fd;
        public short Events;
        public short Revents;
    }

    [DllImport("libc", SetLastError = true, CharSet = CharSet.Ansi)]
    private static extern int open(string path, int flags);

    [DllImport("libc", SetLastError = true)]
    private static extern nint read(int fd, byte[] buf, nint count);

    [DllImport("libc", SetLastError = true)]
    private static extern nint write(int fd, byte[] buf, nint count);

    [DllImport("libc", SetLastError = true)]
    private static extern int close(int fd);

    [DllImport("libc", SetLastError = true)]
    private static extern int poll([In, Out] PollFd[] fds, nuint nfds, int timeout);

    private int _fd;

    public int DeviceClass { get; }
    public int Index { get; }
    public string Path { get; }

    public HidrawTransport(string path, int deviceClass, int index)
    {
        Path = path;
        DeviceClass = deviceClass;
        Index = index;
        _fd = open(path, O_RDWR | O_NONBLOCK);
        if (_fd < 0)
        {
            var errno = Marshal.GetLastWin32Error();
            throw errno == 13
                ? new UnauthorizedAccessException($"无权访问 {path}（安装 udev 规则或调整权限）")
                : new IOException($"打开 {path} 失败，errno={errno}");
        }
    }

    public void Write(ReadOnlySpan<byte> report)
    {
        if (_fd < 0) throw new ObjectDisposedException(nameof(HidrawTransport));
        var data = report.ToArray();
        if (data.Length == 0 || data[0] != Proto.ReportId)
            data = [Proto.ReportId, .. data];

        for (var attempt = 0; attempt < 3; attempt++)
        {
            var n = write(_fd, data, data.Length);
            if (n == data.Length) return;
            if (n < 0 && Marshal.GetLastWin32Error() is 11 or 4)   // EAGAIN / EINTR
            {
                Thread.Sleep(2);
                continue;
            }
            throw new IOException($"写入 {Path} 失败，errno={Marshal.GetLastWin32Error()}");
        }
        throw new IOException($"写入 {Path} 持续阻塞");
    }

    public byte[]? Read(int timeoutMs = 0)
    {
        if (_fd < 0) return null;
        var fds = new[] { new PollFd { Fd = _fd, Events = POLLIN } };
        var ready = poll(fds, 1, timeoutMs);
        if (ready <= 0) return null;
        var buf = new byte[64];
        var n = read(_fd, buf, buf.Length);
        if (n <= 0) return null;
        return buf.AsSpan(0, (int)n).ToArray();
    }

    public void Dispose()
    {
        if (_fd >= 0)
        {
            close(_fd);
            _fd = -1;
        }
    }
}

public sealed record HidCandidate(string Path, ushort Vid, ushort Pid, string Name, string Serial, string UsbProduct)
{
    public int DeviceClass => Proto.ClassForPid(Pid);
}

public sealed record DeviceOpenReport(IReadOnlyList<IHidTransport> Devices, IReadOnlyList<string> Errors);

public static class DeviceScanner
{
    private const string SysRoot = "/sys/class/hidraw";

    public static IReadOnlyList<HidCandidate> Scan()
    {
        var list = new List<HidCandidate>();
        if (!Directory.Exists(SysRoot)) return list;

        foreach (var node in Directory.GetDirectories(SysRoot).OrderBy(p => p, StringComparer.Ordinal))
        {
            var uevent = Path.Combine(node, "device", "uevent");
            if (!File.Exists(uevent)) continue;
            var info = ReadUevent(uevent);
            if (!info.TryGetValue("HID_ID", out var hidId)) continue;
            var parts = hidId.Split(':');
            if (parts.Length != 3) continue;
            if (!ushort.TryParse(parts[1], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var vid))
                continue;
            if (!ushort.TryParse(parts[2], NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var pid))
                continue;
            if (vid != Proto.Vid) continue;
            info.TryGetValue("HID_NAME", out var name);
            info.TryGetValue("HID_UNIQ", out var serial);
            var usbProduct = ReadText(Path.Combine(node, "device", "..", "..", "product"));
            list.Add(new HidCandidate($"/dev/{Path.GetFileName(node)}", vid, pid,
                (name ?? "").Trim(), (serial ?? "").Trim(), usbProduct));
        }
        return list;
    }

    private static string ReadText(string path)
    {
        try { return File.ReadAllText(path).Trim(); }
        catch (IOException) { return ""; }
        catch (UnauthorizedAccessException) { return ""; }
    }

    /// <summary>只打开指定节点（用于有多台同 PID 设备、或要避开调试器时）。</summary>
    public static DeviceOpenReport OpenPath(string path)
    {
        var errors = new List<string>();
        foreach (var cand in Scan())
        {
            if (!string.Equals(cand.Path, path, StringComparison.Ordinal)) continue;
            if (cand.DeviceClass == 0)
            {
                errors.Add($"{path}：PID 0x{cand.Pid:X4} 不是已知的 VK 设备");
                return new DeviceOpenReport([], errors);
            }
            try
            {
                return new DeviceOpenReport([new HidrawTransport(cand.Path, cand.DeviceClass, 0)], errors);
            }
            catch (Exception ex)
            {
                errors.Add($"{cand.Path}：{ex.Message}");
                return new DeviceOpenReport([], errors);
            }
        }
        errors.Add($"{path}：未找到该 hidraw 节点（先运行 --list 看现有节点）");
        return new DeviceOpenReport([], errors);
    }

    /// <summary>
    /// 逐个尝试打开，失败原因逐条收集——不要因为一台设备没权限就把整批降级成"没设备"。
    /// </summary>
    public static DeviceOpenReport OpenAll()
    {
        var counters = new Dictionary<int, int>();
        var devices = new List<IHidTransport>();
        var errors = new List<string>();

        foreach (var cand in Scan())
        {
            var cls = cand.DeviceClass;
            if (cls == 0) continue;
            var idx = counters.GetValueOrDefault(cls);
            counters[cls] = idx + 1;
            try
            {
                devices.Add(new HidrawTransport(cand.Path, cls, idx));
            }
            catch (Exception ex)
            {
                errors.Add($"{cand.Path}（PID 0x{cand.Pid:X4}，{cand.Name}）：{ex.Message}");
            }
        }
        return new DeviceOpenReport(devices, errors);
    }

    private static Dictionary<string, string> ReadUevent(string path)
    {
        var dict = new Dictionary<string, string>(StringComparer.Ordinal);
        try
        {
            foreach (var line in File.ReadAllLines(path))
            {
                var i = line.IndexOf('=');
                if (i <= 0) continue;
                dict[line[..i]] = line[(i + 1)..];
            }
        }
        catch (IOException) { }
        catch (UnauthorizedAccessException) { }
        return dict;
    }
}

/// <summary>内存假设备：没有硬件时用于自检与截图，回放一份合成状态。</summary>
public sealed class FakeTransport : IHidTransport
{
    private readonly Queue<byte[]> _queue = new();

    public int DeviceClass { get; }
    public int Index { get; }
    public string Path => "/dev/fake";
    public List<byte[]> Sent { get; } = new();

    public FakeTransport(int deviceClass, int index = 0) 
    {
        DeviceClass = deviceClass;
        Index = index;
    }

    public void Write(ReadOnlySpan<byte> report)
    {
        var data = report.ToArray();
        Sent.Add(data);
        if (data.Length > 12 && data[12] == (byte)Proto.Cmd.GetState)
            _queue.Enqueue(SyntheticState(DeviceClass));
    }

    public byte[]? Read(int timeoutMs = 0) => _queue.Count > 0 ? _queue.Dequeue() : null;

    public static byte[] SyntheticState(int cls, int seed = 0)
    {
        const int frameStart = 1;                 // 前面留一个 ReportId
        var buf = new byte[frameStart + Proto.DataLen];
        buf[frameStart] = Proto.Head;
        buf[frameStart + 1] = Proto.Head;
        buf[frameStart + 2] = (byte)Proto.Brand;
        buf[frameStart + 4] = (byte)cls;
        buf[frameStart + 12] = (byte)Proto.Cmd.GetState;
        for (var ch = 1; ch <= 4; ch++)
        {
            var off = frameStart + 15 + (ch - 1) * 3;
            buf[off] = (byte)(ch == 1 ? 2 : 1);
            var rpm = 900 + ch * 140 + seed;
            buf[off + 1] = (byte)(rpm & 0xFF);
            buf[off + 2] = (byte)(rpm >> 8);
        }
        buf[frameStart + 27] = 6;
        buf[frameStart + 28] = 100;
        buf[frameStart + 29] = 1;
        buf[frameStart + 30] = 255;
        buf[frameStart + 31] = 0;
        buf[frameStart + 32] = 0;
        return buf;
    }

    public void Dispose() { }
}
