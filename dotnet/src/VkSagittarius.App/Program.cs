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
        if (args.Contains("--self-test"))
            return SelfTest();
        if (args.Contains("--headless"))
            return HeadlessRun(ArgInt(args, "--seconds"));

        return BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
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
    private static int HeadlessRun(int seconds)
    {
        var candidates = DeviceScanner.Scan();
        var set = App.CreateDeviceSet(candidates.Count == 0);
        if (!set.Any)
        {
            Console.Error.WriteLine("没有可用设备，退出");
            return 2;
        }

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
    private static int SelfTest()
    {
        Console.WriteLine("VK 射手座控制中心 · 自检");
        var candidates = DeviceScanner.Scan();
        Console.WriteLine($"枚举到 {candidates.Count} 个 VID 0x{Proto.Vid:X4} HID 节点");
        foreach (var c in candidates)
            Console.WriteLine($"  {c.Path}  PID 0x{c.Pid:X4}  类别 {c.DeviceClass}  {c.Name}");

        var set = App.CreateDeviceSet(candidates.Count == 0);
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
