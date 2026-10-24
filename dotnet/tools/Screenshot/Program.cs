using System;
using System.IO;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Headless;
using Avalonia.Threading;
using VkSagittarius.App;
using VkSagittarius.App.ViewModels;
using VkSagittarius.App.Views;
using VkSagittarius.Core;

// 用 Avalonia headless 平台 + Skia 渲染真实界面截图，供无显示器环境验证 UI。
// 用法：dotnet run --project tools/Screenshot -- <输出目录>
var outDir = args.Length > 0 ? args[0] : "screenshots";
Directory.CreateDirectory(outDir);

AppBuilder.Configure<App>()
    .UseHeadless(new AvaloniaHeadlessPlatformOptions { UseHeadlessDrawing = false })
    .UseSkia()
    .SetupWithoutStarting();

var set = new DeviceSet([new FakeTransport(1), new FakeTransport(3)]);
var vm = new MainViewModel(new DeviceBootstrap(set, Demo: true, Errors: ["已指定 --demo"]));
var window = new MainWindow { DataContext = vm };
window.Show();

void Pump(int ms)
{
    var deadline = Environment.TickCount64 + ms;
    while (Environment.TickCount64 < deadline)
    {
        Dispatcher.UIThread.RunJobs();
        Thread.Sleep(16);
    }
}

Pump(1100);

// 触发几个动作，让日志与状态有内容可看
vm.PresetColorCommand.Execute("0,200,255");
vm.ApplyFanCommand.Execute(null);
Pump(500);

var tabs = window.FindControl<TabControl>("Tabs")!;
var names = new[] { "led", "fan", "lcd" };
var rc = 0;

for (var i = 0; i < names.Length; i++)
{
    tabs.SelectedIndex = i;
    Pump(300);
    var frame = window.CaptureRenderedFrame();
    if (frame is null)
    {
        Console.Error.WriteLine($"标签页 {names[i]} 渲染失败");
        rc = 1;
        continue;
    }
    var path = Path.Combine(outDir, $"ui-{names[i]}.png");
    frame.Save(path);
    Console.WriteLine($"已保存 {path}  ({frame.PixelSize.Width}x{frame.PixelSize.Height})");
}

Console.WriteLine($"日志 {vm.FrameLog.Count} 条");
foreach (var line in vm.FrameLog) Console.WriteLine("  " + line);

vm.Shutdown();
return rc;
