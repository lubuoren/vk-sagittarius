using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using VkSagittarius.App.ViewModels;

namespace VkSagittarius.App.Views;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        UploadButton.Click += OnUploadClick;
        Closing += (_, _) => (DataContext as MainViewModel)?.Shutdown();
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
