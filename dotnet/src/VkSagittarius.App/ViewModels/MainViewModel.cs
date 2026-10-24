using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Media;
using Avalonia.Threading;
using VkSagittarius.Core;

namespace VkSagittarius.App.ViewModels;

public sealed class DeviceItem : ObservableObject
{
    public string Title { get; init; } = "";
    public string Subtitle { get; init; } = "";
    public string Badge { get; init; } = "";
    public bool Online { get; init; }
    public IBrush Accent => Online ? Brushes.LimeGreen : Brushes.Gray;
}

public sealed class MainViewModel : ObservableObject
{
    private readonly DeviceSet _set;
    private readonly MonitorEngine _engine;
    private bool _demo;

    public MainViewModel(DeviceSet set, bool demo)
    {
        _set = set;
        _demo = demo;
        _engine = new MonitorEngine(set, Log);
        _engine.StateUpdated += st => Dispatcher.UIThread.Post(() => ApplyState(st));

        Devices = new ObservableCollection<DeviceItem>(set.Devices.Select(d => new DeviceItem
        {
            Title = Proto.ClassName(d.Class),
            Subtitle = $"{d.Path}   VID 0x{Proto.Vid:X4}  PID 0x{Proto.PidForClass(d.Class):X4}",
            Badge = $"类别 {d.Class}",
            Online = true,
        }));

        LedModes = new ObservableCollection<Option>(Proto.LedModes
            .OrderBy(kv => kv.Key)
            .Select(kv => new Option(kv.Key, $"{kv.Key:000}  {kv.Value}")));
        LedChannels =
        [
            new Option(0, "全部通道"), new Option(1, "通道 1"), new Option(2, "通道 2"),
            new Option(3, "通道 3"), new Option(4, "通道 4"), new Option(5, "主板通道"),
        ];
        FanChannels = [new Option(1, "风扇 1"), new Option(2, "风扇 2"), new Option(3, "风扇 3"), new Option(4, "风扇 4")];
        FanModes = new ObservableCollection<Option>(
            Proto.FanCurves.OrderBy(kv => kv.Key).Select(kv => new Option(kv.Key, $"{kv.Key}  {kv.Value.Name}"))
                .Append(new Option(Proto.FanModeCustom, "5  自定义曲线")));

        _selectedLedMode = LedModes[0];
        _selectedLedChannel = LedChannels[0];
        _selectedFanChannel = FanChannels[0];
        _selectedFanMode = FanModes[1];

        ApplyLedCommand = new RelayCommand(ApplyLed);
        ApplyFanCommand = new RelayCommand(ApplyFan);
        RefreshCommand = new RelayCommand(Refresh);
        RotateCommand = new RelayCommand(Rotate);
        ScreenCommand = new RelayCommand(Screen);
        ClockCommand = new RelayCommand(ToggleClock);
        PresetColorCommand = new RelayCommand(PresetColor);
        ToggleMonitorCommand = new RelayCommand(ToggleMonitor);
        ClearLogCommand = new RelayCommand(FrameLog.Clear);

        set.RegisterAll();
        Refresh();
        Log($"已打开 {set.Devices.Count} 台设备" + (demo ? "（演示模式：写入假设备）" : ""));
        _engine.Start();
        MonitorRunning = true;
    }

    // ---------------------------------------------------------- 设备 / 遥测

    public ObservableCollection<DeviceItem> Devices { get; }
    public bool IsDemo => _demo;
    public string ModeBanner => _demo ? "演示模式（无硬件，帧写入内存假设备）" : "已连接真实设备";
    public string AudioBanner => _set.AudioAvailable ? $"音频后端：{_set.Meter.Backend}" : "音频后端：不可用（parec / pw-cat 未安装）";

    private string _cpuTemp = "-";
    public string CpuTemp { get => _cpuTemp; private set => Set(ref _cpuTemp, value); }
    private string _gpuTemp = "-";
    public string GpuTemp { get => _gpuTemp; private set => Set(ref _gpuTemp, value); }
    private string _cpuUsage = "-";
    public string CpuUsage { get => _cpuUsage; private set => Set(ref _cpuUsage, value); }
    private string _memUsage = "-";
    public string MemUsage { get => _memUsage; private set => Set(ref _memUsage, value); }
    private string _cpuFreq = "-";
    public string CpuFreq { get => _cpuFreq; private set => Set(ref _cpuFreq, value); }
    private string _fanRpm = "-";
    public string FanRpm { get => _fanRpm; private set => Set(ref _fanRpm, value); }

    public void Refresh()
    {
        var t = _set.Refresh();
        CpuTemp = $"{t.CpuTemp:0.0} ℃";
        GpuTemp = $"{t.GpuTemp:0.0} ℃";
        CpuUsage = $"{t.CpuUsage} %";
        MemUsage = $"{t.MemUsage} %";
        CpuFreq = $"{t.CpuFreqMhz:0} MHz";
        FanRpm = $"{t.CpuFanRpm} RPM";
        OnPropertyChanged(nameof(AudioBanner));
    }

    private void ApplyState(Proto.DeviceState st)
    {
        if (st.Fans.TryGetValue(1, out var f1)) FanRpm = $"{f1.Rpm} RPM";
        LedStateText = st.Describe();
    }

    private string _ledStateText = "（等待设备回报）";
    public string LedStateText { get => _ledStateText; private set => Set(ref _ledStateText, value); }

    // ---------------------------------------------------------- 灯效

    public ObservableCollection<Option> LedModes { get; }
    public IReadOnlyList<Option> LedChannels { get; }

    private Option _selectedLedMode;
    public Option SelectedLedMode { get => _selectedLedMode; set => Set(ref _selectedLedMode, value); }
    private Option _selectedLedChannel;
    public Option SelectedLedChannel { get => _selectedLedChannel; set => Set(ref _selectedLedChannel, value); }

    private double _brightness = 100;
    public double Brightness
    {
        get => _brightness;
        set { if (Set(ref _brightness, value)) OnPropertyChanged(nameof(BrightnessText)); }
    }
    public string BrightnessText => $"{Brightness:0}";

    private double _speed = 60;
    public double Speed
    {
        get => _speed;
        set { if (Set(ref _speed, value)) OnPropertyChanged(nameof(SpeedText)); }
    }
    public string SpeedText => Speed < 33 ? "慢" : Speed < 66 ? "中" : "快";

    private double _r = 255, _g = 40, _b = 40;
    public double R { get => _r; set { if (Set(ref _r, value)) UpdateColor(); } }
    public double G { get => _g; set { if (Set(ref _g, value)) UpdateColor(); } }
    public double B { get => _b; set { if (Set(ref _b, value)) UpdateColor(); } }

    private string _colorHex = "FF2828";
    public string ColorHex { get => _colorHex; private set => Set(ref _colorHex, value); }
    private IBrush _colorBrush = new SolidColorBrush(Color.FromRgb(0xFF, 0x28, 0x28));
    public IBrush ColorBrush { get => _colorBrush; private set => Set(ref _colorBrush, value); }

    private void UpdateColor()
    {
        var (r, g, b) = ((byte)R, (byte)G, (byte)B);
        ColorHex = $"{r:X2}{g:X2}{b:X2}";
        ColorBrush = new SolidColorBrush(Color.FromRgb(r, g, b));
    }

    public RelayCommand ApplyLedCommand { get; }
    public RelayCommand PresetColorCommand { get; }

    private void PresetColor(object? param)
    {
        if (param is not string spec) return;
        var parts = spec.Split(',');
        if (parts.Length != 3) return;
        R = int.Parse(parts[0]);
        G = int.Parse(parts[1]);
        B = int.Parse(parts[2]);
        ApplyLed();
    }

    private void ApplyLed()
    {
        var dev = _set.FanLed;
        if (dev is null) { Log("× 没有风扇/灯效设备"); return; }
        var speed = Speed < 33 ? Proto.LedSpeedSlow : Speed < 66 ? Proto.LedSpeedMid : Proto.LedSpeedFast;
        var rgb = ((int)R, (int)G, (int)B);
        try
        {
            dev.SetLed(SelectedLedMode.Id, rgb, (int)Brightness, speed, SelectedLedChannel.Id);
            Log($"LED → 模式 {SelectedLedMode.Id} 通道 {SelectedLedChannel.Id} " +
                $"亮度 {Brightness:0} 速度 {speed} #{ColorHex}");
        }
        catch (Exception ex) { Log($"× LED 发送失败：{ex.Message}"); }
    }

    // ---------------------------------------------------------- 风扇

    public IReadOnlyList<Option> FanChannels { get; }
    public ObservableCollection<Option> FanModes { get; }

    private Option _selectedFanChannel;
    public Option SelectedFanChannel { get => _selectedFanChannel; set => Set(ref _selectedFanChannel, value); }
    private Option _selectedFanMode;
    public Option SelectedFanMode { get => _selectedFanMode; set => Set(ref _selectedFanMode, value); }

    public double Pwm1 { get; set; } = 20;
    public double Pwm2 { get; set; } = 55;
    public double Pwm3 { get; set; } = 100;
    public double Temp1 { get; set; } = 45;
    public double Temp2 { get; set; } = 65;
    public double Temp3 { get; set; } = 85;

    public RelayCommand ApplyFanCommand { get; }

    private void ApplyFan()
    {
        var dev = _set.FanLed;
        if (dev is null) { Log("× 没有风扇/灯效设备"); return; }
        try
        {
            if (SelectedFanMode.Id == Proto.FanModeCustom)
            {
                dev.SetFan(SelectedFanChannel.Id, Proto.FanModeCustom,
                    ([(int)Pwm1, (int)Pwm2, (int)Pwm3], [(int)Temp1, (int)Temp2, (int)Temp3]));
                Log($"FAN → 通道 {SelectedFanChannel.Id} 自定义 PWM[{(int)Pwm1},{(int)Pwm2},{(int)Pwm3}] " +
                    $"TEMP[{(int)Temp1},{(int)Temp2},{(int)Temp3}]");
            }
            else
            {
                dev.SetFan(SelectedFanChannel.Id, SelectedFanMode.Id);
                Log($"FAN → 通道 {SelectedFanChannel.Id} 预设「{SelectedFanMode.Label}」");
            }
        }
        catch (Exception ex) { Log($"× 风扇发送失败：{ex.Message}"); }
    }

    // ---------------------------------------------------------- LCD

    private bool _clockEnabled = true;
    public bool ClockEnabled { get => _clockEnabled; set => Set(ref _clockEnabled, value); }
    private bool _hour24 = true;
    public bool Hour24 { get => _hour24; set => Set(ref _hour24, value); }

    public RelayCommand RotateCommand { get; }
    public RelayCommand ScreenCommand { get; }
    public RelayCommand ClockCommand { get; }

    private void Rotate(object? param)
    {
        var sub = (param?.ToString()) switch
        {
            "90" => Proto.LcdSub.Rot90,
            "180" => Proto.LcdSub.Rot180,
            "270" => Proto.LcdSub.Rot270,
            _ => Proto.LcdSub.Rot0,
        };
        SendLcd(sub, $"旋转 {param}°");
    }

    private void Screen(object? param) =>
        SendLcd(param?.ToString() == "black" ? Proto.LcdSub.Black : Proto.LcdSub.White,
            param?.ToString() == "black" ? "整屏黑" : "整屏白");

    private void ToggleClock() => SendLcd(Proto.LcdSub.Clock, ClockEnabled ? "显示时间" : "隐藏时间");

    private void SendLcd(Proto.LcdSub sub, string what)
    {
        var dev = _set.Lcd;
        if (dev is null) { Log("× 没有 LCD 设备"); return; }
        try
        {
            if (sub == Proto.LcdSub.Clock) dev.LcdClock(Hour24, blank: !ClockEnabled);
            else dev.LcdSimple(sub);
            Log($"LCD → {what}");
        }
        catch (Exception ex) { Log($"× LCD 发送失败：{ex.Message}"); }
    }

    public async Task UploadAsync(string path)
    {
        var dev = _set.Lcd;
        if (dev is null) { Log("× 没有 LCD 设备"); return; }
        if (_demo)
        {
            Log($"（演示模式）跳过真实上传：{path}");
            return;
        }
        var ok = await Task.Run(() => new LcdUploader(Log).Upload(dev, path));
        Log(ok ? "✓ 背景上传完成" : "× 背景上传失败");
    }

    // ---------------------------------------------------------- 监控 / 日志

    private bool _monitorRunning;
    public bool MonitorRunning
    {
        get => _monitorRunning;
        private set { if (Set(ref _monitorRunning, value)) OnPropertyChanged(nameof(MonitorButtonText)); }
    }
    public string MonitorButtonText => MonitorRunning ? "暂停心跳" : "启动心跳";

    public RelayCommand ToggleMonitorCommand { get; }
    public RelayCommand RefreshCommand { get; }
    public RelayCommand ClearLogCommand { get; }

    private void ToggleMonitor()
    {
        if (MonitorRunning) { _engine.Stop(); MonitorRunning = false; Log("心跳已暂停"); }
        else { _engine.Start(); MonitorRunning = true; Log("心跳已启动"); }
    }

    public ObservableCollection<string> FrameLog { get; } = new();

    public void Log(string message)
    {
        void Add()
        {
            FrameLog.Insert(0, $"{DateTime.Now:HH:mm:ss}  {message}");
            while (FrameLog.Count > 200) FrameLog.RemoveAt(FrameLog.Count - 1);
        }
        if (Dispatcher.UIThread.CheckAccess()) Add();
        else Dispatcher.UIThread.Post(Add);
    }

    public void Shutdown()
    {
        _engine.Dispose();
        _set.Dispose();
    }
}
