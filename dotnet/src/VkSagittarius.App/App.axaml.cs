using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using VkSagittarius.App.ViewModels;
using VkSagittarius.App.Views;
using VkSagittarius.Core;

namespace VkSagittarius.App;

public sealed record DeviceBootstrap(DeviceSet Set, bool Demo, IReadOnlyList<string> Errors);

public sealed class App : Application
{
    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var args = desktop.Args ?? [];
            var boot = CreateDevice(args.Contains("--demo"));

            var vm = new MainViewModel(boot);
            var window = new MainWindow { DataContext = vm };
            desktop.MainWindow = window;
            desktop.ShutdownRequested += (_, _) => vm.Shutdown();
        }
        base.OnFrameworkInitializationCompleted();
    }

    /// <summary>
    /// 打开设备。forceDemo 只由 --demo 触发；否则先真连，连不上才退演示模式，
    /// 并把每台设备的失败原因原样带出去——权限不足不能再被伪装成"没有设备"。
    /// </summary>
    public static DeviceBootstrap CreateDevice(bool forceDemo)
    {
        if (forceDemo)
            return new DeviceBootstrap(
                new DeviceSet([new FakeTransport(1), new FakeTransport(3)]),
                Demo: true,
                Errors: ["已指定 --demo"]);

        var report = DeviceScanner.OpenAll();
        if (report.Devices.Count > 0)
            return new DeviceBootstrap(new DeviceSet(report.Devices), Demo: false, Errors: report.Errors);

        var errors = report.Errors.Count > 0
            ? report.Errors
            : [$"未发现 VID 0x{Proto.Vid:X4} / PID 0x5201-0x5203 的 HID 设备"];

        return new DeviceBootstrap(
            new DeviceSet([new FakeTransport(1), new FakeTransport(3)]),
            Demo: true,
            Errors: errors);
    }

    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace();
}
