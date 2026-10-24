using System;
using System.Linq;
using Avalonia;
using VkSagittarius.Core;

namespace VkSagittarius.App;

internal static class Program
{
    [STAThread]
    public static int Main(string[] args)
    {
        if (args.Contains("--list"))
            return ListDevices();
        if (args.Contains("--self-test"))
            return SelfTest(ArgStr(args, "--path"));
        if (args.Contains("--headless"))
            return HeadlessRun(ArgInt(args, "--seconds"), ArgStr(args, "--path"));

        // --scale 优先级最高：写进环境变量后由 ScaleConfig 统一读取
        if (ArgStr(args, "--scale") is { } scaleArg)
            Environment.SetEnvironmentVariable("VKSAG_SCALE", scaleArg);

        return BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }

    private static string? ArgStr(string[] args, string name)
    {
        var i = Array.IndexOf(args, name);
        return i >= 0 && i + 1 < args.Length ? args[i + 1] : null;
    }

    /// <summary>列出所有 VID 0x0416 的 hidraw 节点，含序列号与 USB 产品名——用来分辨
    /// 哪一个是 VK 控制器、哪一个是同 PID 的 Nuvoton 调试器。</summary>
    private static int ListDevices()
    {
        var list = DeviceScanner.Scan();
        Console.WriteLine($"VID 0x{Proto.Vid:X4} 的 HID 节点：{list.Count} 个");
        Console.WriteLine($"{"节点",-14}{"PID",-10}{"类别",-6}{"序列号",-20}{"HID 名",-20}USB 产品名");
        foreach (var c in list)
            Console.WriteLine($"{c.Path,-14}0x{c.Pid:X4}    {c.DeviceClass,-6}{c.Serial,-20}{c.Name,-20}{c.UsbProduct}");
        if (list.Count == 0)
            Console.WriteLine("（没有匹配设备：确认散热器/控制盒的 USB 线已接到主板，或 udev 未生效）");
        return 0;
    }

    private static int ArgInt(string[] args, string name)
    {
        var i = Array.IndexOf(args, name);
        return i >= 0 && i + 1 < args.Length && int.TryParse(args[i + 1], out var v) ? v : 0;
    }

    private static void TryCancel(CancellationTokenSource cts)
    {
        try { cts.Cancel(); }
        catch (ObjectDisposedException) { }
    }

    /// <summary>
    /// 常驻心跳模式：不开窗口，只维持 LCD / 灯效 / 音频心跳，供 systemd user service 使用。
    /// 加 --seconds N 可在 N 秒后自行退出（便于验证）。
    /// </summary>
    private static int HeadlessRun(int seconds, string? path = null)
    {
        var boot = DeviceFactory.Open(false, path);
        if (boot.Demo)
        {
            Console.Error.WriteLine("没有可用的真实设备，心跳模式不启动。原因：");
            foreach (var err in boot.Errors) Console.Error.WriteLine("  " + err);
            Console.Error.WriteLine("权限不足时安装 udev 规则：sudo install -m644 packaging/99-vk-sagittarius.rules /etc/udev/rules.d/");
            boot.Set.Dispose();
            return 2;
        }

        var set = boot.Set;
        using var cts = new CancellationTokenSource(
            seconds > 0 ? TimeSpan.FromSeconds(seconds) : Timeout.InfiniteTimeSpan);
        Console.CancelKeyPress += (_, e) => { e.Cancel = true; TryCancel(cts); };
        AppDomain.CurrentDomain.ProcessExit += (_, _) => TryCancel(cts);

        using var engine = new MonitorEngine(set, msg => Console.WriteLine($"{DateTime.Now:HH:mm:ss} {msg}"));
        engine.Start();
        Console.WriteLine($"心跳已启动：{set.Devices.Count} 台设备，" +
                          string.Join(", ", set.Devices.Select(d => $"{Proto.ClassName(d.Class)}@{d.Path}")));

        cts.Token.WaitHandle.WaitOne(Timeout.Infinite);
        engine.Stop();
        Console.WriteLine($"退出：发出 {engine.FramesSent} 帧，状态回读 {engine.StateUpdates} 次");
        set.Dispose();
        return 0;
    }

    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();

    /// <summary>无界面自检：解析设备 → 发一轮帧 → 打印结果，用于 CI 与排障。</summary>
    private static int SelfTest(string? path = null)
    {
        Console.WriteLine("VK 射手座控制中心 · 自检");
        var candidates = DeviceScanner.Scan();
        Console.WriteLine($"枚举到 {candidates.Count} 个 VID 0x{Proto.Vid:X4} HID 节点");
        foreach (var c in candidates)
            Console.WriteLine($"  {c.Path}  PID 0x{c.Pid:X4}  类别 {c.DeviceClass}  {c.Name}");

        var boot = DeviceFactory.Open(false, path);
        var set = boot.Set;
        Console.WriteLine($"{boot.Demo switch { true => "演示模式（无真实设备）", false => "真实设备已连接" }}");
        foreach (var err in boot.Errors) Console.WriteLine("  ! " + err);
        Console.WriteLine($"打开设备 {set.Devices.Count} 台：" +
                          string.Join(", ", set.Devices.Select(d => $"{Proto.ClassName(d.Class)}@{d.Path}")));

        set.RegisterAll();
        var tele = set.Refresh();
        Console.WriteLine($"遥测 CPU {tele.CpuTemp:0.0}℃ / GPU {tele.GpuTemp:0.0}℃ / 占用 {tele.CpuUsage}% / " +
                          $"内存 {tele.MemUsage}% / 频率 {tele.CpuFreqMhz:0}MHz / 风扇 {tele.CpuFanRpm}RPM");

        if (set.FanLed is { } fan)
        {
            fan.SetLed(6, (255, 40, 40), 100, Proto.LedSpeedFast, 0);
            fan.SetFan(1, 2);
            var st = fan.RequestState(400, tele);
            Console.WriteLine("状态回读：" + (st?.Describe() ?? "无回应（假设备应返回合成状态）"));
        }
        if (set.Lcd is { } lcd)
        {
            lcd.LcdTemps(tele);
            lcd.LcdFreqs(tele);
            lcd.LcdClock(true);
            Console.WriteLine("LCD 三段文本已发送");
        }
        set.Dispose();
        Console.WriteLine("自检完成");
        return 0;
    }
}
