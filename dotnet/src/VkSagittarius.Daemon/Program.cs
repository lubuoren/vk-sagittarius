using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using VkSagittarius.Core;

namespace VkSagittarius.Daemon;

/// <summary>
/// vksagd：常驻后台。独占 HID 设备、跑心跳、通过 Unix socket 给 GUI 用。
/// 由 systemd --user 的 vk-sagittarius.service 拉起，登录即运行；GUI 关掉不影响下发。
/// </summary>
internal static class Program
{
    private static int Main(string[] args)
    {
        if (args.Contains("--version"))
        {
            Console.WriteLine($"vksagd {DaemonServer.Version}");
            return 0;
        }

        var demo = args.Contains("--demo");
        var path = Arg(args, "--path");
        var socket = Arg(args, "--socket");
        var once = args.Contains("--check");       // 只起一次、握手自检后退出（CI 用）

        DeviceBootstrap boot;
        try
        {
            boot = DeviceFactory.Open(demo, path);
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"打开设备失败：{ex.Message}");
            return 2;
        }

        using var server = new DaemonServer(boot, socket);
        server.Log += line => Console.WriteLine(line);

        try
        {
            server.Start();
        }
        catch (InvalidOperationException ex)
        {
            Console.Error.WriteLine(ex.Message);
            boot.Set.Dispose();
            return 3;
        }

        if (once)
        {
            var status = server.BuildStatus();
            Console.WriteLine($"check ok: devices={status.Devices.Length} demo={status.Demo} frames={status.Frames}");
            server.Dispose();
            return 0;
        }

        var quit = new ManualResetEventSlim(false);
        Console.CancelKeyPress += (_, e) => { e.Cancel = true; quit.Set(); };
        using var sigterm = PosixSignalRegistration.Create(PosixSignal.SIGTERM, ctx =>
        {
            ctx.Cancel = true;
            quit.Set();
        });

        Console.WriteLine($"vksagd {DaemonServer.Version} 运行中，Ctrl-C 退出");
        quit.Wait();
        return 0;
    }

    private static string? Arg(string[] args, string name)
    {
        var i = Array.IndexOf(args, name);
        return i >= 0 && i + 1 < args.Length ? args[i + 1] : null;
    }
}
