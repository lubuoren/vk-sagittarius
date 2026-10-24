using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform;
using Avalonia.Platform.Storage;
using VkSagittarius.App.ViewModels;

namespace VkSagittarius.App.Views;

public partial class MainWindow : Window
{
    private TrayIcon? _tray;

    public MainWindow()
    {
        InitializeComponent();
        UploadButton.Click += OnUploadClick;
        TryCreateTray();
        Closing += (_, _) =>
        {
            _tray?.Dispose();
            (DataContext as MainViewModel)?.Shutdown();
        };
    }

    /// <summary>托盘图标（原程序 mainNotifyIcon + 右键「退出」）。Linux 上依赖
    /// StatusNotifier / libappindicator，失败就跳过，不影响主功能。</summary>
    private void TryCreateTray()
    {
        try
        {
            using var icon = AssetLoader.Open(new Uri("avares://VkSagittarius.App/Assets/icon.png"));
            var menu = new NativeMenu();
            var showItem = new NativeMenuItem("显示窗口");
            showItem.Click += (_, _) => { Show(); WindowState = WindowState.Normal; Activate(); };
            var exitItem = new NativeMenuItem("退出");
            exitItem.Click += (_, _) => Close();
            menu.Add(showItem);
            menu.Add(new NativeMenuItemSeparator());
            menu.Add(exitItem);

            _tray = new TrayIcon
            {
                Icon = new WindowIcon(icon),
                ToolTipText = "VK 射手座控制中心",
                Menu = menu,
                IsVisible = true,
            };
            _tray.Clicked += (_, _) => { Show(); WindowState = WindowState.Normal; Activate(); };
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"托盘图标不可用：{ex.Message}");
            _tray = null;
        }
    }

    private async void OnUploadClick(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (DataContext is not MainViewModel vm) return;
        var files = await StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "选择 LCD 背景图或视频",
            AllowMultiple = false,
            FileTypeFilter =
            [
                new FilePickerFileType("图片/视频")
                {
                    Patterns = ["*.png", "*.bmp", "*.jpg", "*.gif", "*.mp4", "*.mov", "*.avi"],
                },
            ],
        });
        var file = files.FirstOrDefault();
        if (file?.TryGetLocalPath() is { } path)
            await vm.UploadAsync(path);
    }
}
