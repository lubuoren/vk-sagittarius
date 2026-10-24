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

public sealed class App : Application
{
    public DeviceSet? Set { get; private set; }
    public bool Demo { get; private set; }

    public override void Initialize() => AvaloniaXamlLoader.Load(this);

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var args = desktop.Args ?? [];
            Demo = args.Contains("--demo");
            Set = CreateDeviceSet(Demo);
            Demo = Demo || !Set.Devices.Any(d => d.Path != "/dev/fake");

            var vm = new MainViewModel(Set, Demo);
            var window = new MainWindow { DataContext = vm };
            desktop.MainWindow = window;
            desktop.ShutdownRequested += (_, _) => vm.Shutdown();
        }
        base.OnFrameworkInitializationCompleted();
    }

    public static DeviceSet CreateDeviceSet(bool demo)
    {
        if (!demo)
        {
            try
            {
                var transports = DeviceScanner.OpenAll();
                if (transports.Count > 0) return new DeviceSet(transports);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"打开真实设备失败：{ex.Message}");
            }
        }
        return new DeviceSet([new FakeTransport(1), new FakeTransport(3)]);
    }
}
