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
    private DeviceSet _set;
    private MonitorEngine _engine;
    private bool _demo;
    private IReadOnlyList<string> _errors;

    public MainViewModel(DeviceBootstrap boot)
    {
        _set = boot.Set;
        _demo = boot.Demo;
        _errors = boot.Errors;

        Devices = new ObservableCollection<DeviceItem>(BuildDeviceItems(_set));

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
        RescanCommand = new RelayCommand(Rescan);
        ToggleAutostartCommand = new RelayCommand(ToggleAutostart);

        _selectedScaleOption = ScaleOptions.FirstOrDefault(o => Math.Abs(o.Id / 1000.0 - UiScale) < 0.001)
                               ?? new Option((int)Math.Round(UiScale * 1000), $"{UiScale:0.###}×");
        if (!ScaleOptions.Contains(_selectedScaleOption)) ScaleOptions.Add(_selectedScaleOption);
        ScaleResetCommand = new RelayCommand(() =>
        {
            var opt = ScaleOptions.FirstOrDefault(o => o.Id == 1000);
            if (opt is not null) SelectedScaleOption = opt;
        });

        _uiTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        _uiTimer.Tick += OnUiTick;

        _engine = NewEngine(_set);
        _set.RegisterAll();
        Refresh();
        ReportConnection();
        _engine.Start();
        MonitorRunning = true;
        StartUiTimer();
    }

    private static List<DeviceItem> BuildDeviceItems(DeviceSet set) => set.Devices.Select(d => new DeviceItem
    {
        Title = Proto.ClassName(d.Class),
        Subtitle = $"{d.Path}   VID 0x{Proto.Vid:X4}  PID 0x{Proto.PidForClass(d.Class):X4}",
        Badge = $"类别 {d.Class}",
        Online = d.Path != "/dev/fake",
    }).ToList();

    private MonitorEngine NewEngine(DeviceSet set)
    {
        var engine = new MonitorEngine(set, Log);
        engine.StateUpdated += st => Dispatcher.UIThread.Post(() => ApplyState(st));
        return engine;
    }

    /// <summary>把连接结果如实写进横幅与日志：权限不足、没插设备、--demo 要能区分。</summary>
    private void ReportConnection()
    {
        OnPropertyChanged(nameof(IsDemo));
        OnPropertyChanged(nameof(ModeBanner));
        OnPropertyChanged(nameof(ModeDetail));
        OnPropertyChanged(nameof(AudioBanner));

        if (!_demo)
        {
            Log($"已连接 {_set.Devices.Count} 台设备：" +
                string.Join("、", _set.Devices.Select(d => $"{Proto.ClassName(d.Class)}@{d.Path}")));
            return;
        }
        Log("演示模式（帧只写内存假设备）：" + (_errors.Count > 0 ? _errors[0] : "原因未知"));
        foreach (var err in _errors.Skip(1)) Log("  " + err);
    }

    private void Rescan()
    {
        var boot = App.CreateDevice(false);
        if (!boot.Demo)
        {
            _engine.Stop();
            _engine.Dispose();
            _set.Dispose();
        }
        else
        {
            boot.Set.Dispose();          // 仍是演示数据，丢弃
            Log($"× 重新扫描未连上：{boot.Errors.FirstOrDefault() ?? "未知原因"}");
            _demo = true;
            _errors = boot.Errors;
            ReportConnection();
            return;
        }

        _set = boot.Set;
        _demo = boot.Demo;
        _errors = boot.Errors;
        Devices.Clear();
        foreach (var item in BuildDeviceItems(_set)) Devices.Add(item);
        _engine = NewEngine(_set);
        _set.RegisterAll();
        ReportConnection();
        _engine.Start();
        MonitorRunning = true;
        Refresh();
    }

    // ---------------------------------------------------------- 设备 / 遥测

    public ObservableCollection<DeviceItem> Devices { get; }
    public bool IsDemo => _demo;

    public string ModeBanner => _demo
        ? (_errors.Count > 0 && _errors[0].Contains("权限") ? "未连上设备：权限不足（演示模式）"
           : _errors.Count > 0 && _errors[0].Contains("--demo") ? "演示模式（手动指定 --demo）"
           : "未发现设备（演示模式）")
        : "已连接真实设备";

    /// <summary>完整原因，鼠标悬停横幅可见。</summary>
    public string ModeDetail => _demo
        ? string.Join("\n", _errors) + "\n\n权限不足时执行：\n" +
          "sudo install -m644 /usr/lib/udev/rules.d/99-vk-sagittarius.rules /etc/udev/rules.d/ 2>/dev/null || true\n" +
          "sudo udevadm control --reload && sudo udevadm trigger --subsystem-match=hidraw\n" +
          "然后点「重新扫描」。"
        : $"已连接 {_set.Devices.Count} 台设备";

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

    private bool _ledSynced;

    /// <summary>设备回读 → 界面同步。原程序：灯效只在首次收到状态时回填一次，
    /// 风扇每路模式则持续跟随设备（myALLMode 与 data[ch*3+12]）。</summary>
    private void ApplyState(Proto.DeviceState st)
    {
        for (var ch = 1; ch <= 4; ch++)
        {
            if (!st.Fans.TryGetValue(ch, out var f)) continue;
            SetRpm(ch, f.Rpm);
            _fanModeByCh[ch] = f.Mode;
        }
        if (SelectedFanChannel is { } cur &&
            FanModes.FirstOrDefault(m => m.Id == _fanModeByCh[cur.Id]) is { } m2)
            SelectedFanMode = m2;

        if (!_ledSynced && st.Led is { } led)
        {
            _ledSynced = true;
            SelectedLedMode = LedModes.FirstOrDefault(m => m.Id == led.Mode) ?? SelectedLedMode;
            Brightness = led.Brightness;
            // 设备侧速度档 1/2/4 → 滑杆 100/50/1（原程序 speedVal = {100,100,50,1,1}）
            Speed = led.Speed switch { 1 => 100, 2 => 50, 4 => 1, _ => Speed };
            R = led.R;
            G = led.G;
            B = led.B;
            Log($"从设备回读：模式 {led.Mode}，亮度 {led.Brightness}，颜色 #{led.R:X2}{led.G:X2}{led.B:X2}");
        }

        LedStateText = st.Describe();
    }

    private readonly string[] _fanRpmByCh = ["-", "-", "-", "-"];
    public string FanRpm1 => _fanRpmByCh[0];
    public string FanRpm2 => _fanRpmByCh[1];
    public string FanRpm3 => _fanRpmByCh[2];
    public string FanRpm4 => _fanRpmByCh[3];

    private void SetRpm(int ch, int rpm)
    {
        if (ch is < 1 or > 4) return;
        _fanRpmByCh[ch - 1] = $"{rpm} RPM";
        OnPropertyChanged($"FanRpm{ch}");
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
            if (SelectedLedMode.Id == 10)
            {
                // 原程序 SendLEDData1：mode 固定 1，负载 {0,速度,0,0,0,0}
                var (mode, payload) = Proto.LedAutoPayload(speed);
                dev.Send(Proto.BuildReport(dev.Class, dev.Index, Proto.Cmd.Led,
                    SelectedLedChannel.Id, mode, payload));
                Log($"LED → 全彩自动（通道 {SelectedLedChannel.Id}，速度 {speed}，负载全零由设备自行跑色）");
            }
            else
            {
                dev.SetLed(SelectedLedMode.Id, rgb, (int)Brightness, speed, SelectedLedChannel.Id);
                Log($"LED → 模式 {SelectedLedMode.Id} 通道 {SelectedLedChannel.Id} " +
                    $"亮度 {Brightness:0} 速度 {speed} #{ColorHex}");
            }
        }
        catch (Exception ex) { Log($"× LED 发送失败：{ex.Message}"); }
    }

    // ---------------------------------------------------------- 风扇

    public IReadOnlyList<Option> FanChannels { get; }
    public ObservableCollection<Option> FanModes { get; }

    private Option _selectedFanChannel;
    public Option SelectedFanChannel
    {
        get => _selectedFanChannel;
        set
        {
            if (!Set(ref _selectedFanChannel, value) || value is null) return;
            // 原程序 myALLMode[通道] 记忆每路各自的曲线模式
            SelectedFanMode = FanModes.FirstOrDefault(m => m.Id == _fanModeByCh[value.Id]) ?? FanModes[1];
        }
    }
    private Option _selectedFanMode;
    public Option SelectedFanMode { get => _selectedFanMode; set => Set(ref _selectedFanMode, value); }

    /// <summary>每路风扇的曲线模式（下标即通道号，1..4），对齐原程序 myALLMode。</summary>
    private readonly int[] _fanModeByCh = [2, 2, 2, 2, 2];

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
            _fanModeByCh[SelectedFanChannel.Id] = SelectedFanMode.Id;
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
    public RelayCommand RescanCommand { get; }

    private void ToggleMonitor()
    {
        if (MonitorRunning) { _engine.Stop(); MonitorRunning = false; Log("心跳已暂停"); }
        else { _engine.Start(); MonitorRunning = true; Log("心跳已启动"); }
    }

    // ---------------------------------------------------------- 系统信息 / 曲线 / 自启

    private static readonly SystemInfo SysInfo = SystemInfo.Read();

    public string SysCpu => string.IsNullOrEmpty(SysInfo.CpuName) ? "未知" : SysInfo.CpuName;
    public string SysCores => $"{SysInfo.Cores} 线程";
    public string SysMem => SysInfo.MemTotal;
    public string SysGpu => string.IsNullOrEmpty(SysInfo.GpuName) ? "未识别" : SysInfo.GpuName;
    public string SysKernel => SysInfo.Kernel;
    public string SysHost => SysInfo.HostName;
    public string SysUptime => SysInfo.Uptime;

    private readonly List<double> _cpuHist = new();
    private readonly List<double> _gpuHist = new();

    /// <summary>温度曲线（画布 420x110，60 个采样点，1Hz）。</summary>
    public Geometry CpuGeometry => BuildGeometry(_cpuHist, 420, 110);
    public Geometry GpuGeometry => BuildGeometry(_gpuHist, 420, 110);

    private static Geometry BuildGeometry(List<double> data, double w, double h)
    {
        if (data.Count == 0) return new StreamGeometry();
        var max = Math.Max(90.0, data.Max());
        var step = data.Count > 1 ? w / (data.Count - 1) : w;
        var sb = new System.Text.StringBuilder();
        for (var i = 0; i < data.Count; i++)
        {
            var x = i * step;
            var y = h - Math.Clamp(data[i] / max, 0, 1) * (h - 6);
            sb.Append(i == 0 ? $"M {x:0.#},{y:0.#}" : $" L {x:0.#},{y:0.#}");
        }
        return Geometry.Parse(sb.ToString());
    }

    private void PushHistory()
    {
        _cpuHist.Add(_set.Snapshot.CpuTemp);
        _gpuHist.Add(_set.Snapshot.GpuTemp);
        if (_cpuHist.Count > 60) _cpuHist.RemoveAt(0);
        if (_gpuHist.Count > 60) _gpuHist.RemoveAt(0);
        OnPropertyChanged(nameof(CpuGeometry));
        OnPropertyChanged(nameof(GpuGeometry));
    }

    // 原程序 Form1 有定时器刷新系统信息；这里同样用 1Hz 的 UI 定时器
    private readonly DispatcherTimer _uiTimer;

    private void StartUiTimer()
    {
        _uiTimer.Start();
    }

    private void OnUiTick(object? sender, EventArgs e)
    {
        Refresh();
        PushHistory();
    }

    // ---------------------------------------------------------- 开机自启

    private static string AutostartFile => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        ".config", "autostart", "vk-sagittarius.desktop");

    public bool AutostartEnabled => File.Exists(AutostartFile);

    public RelayCommand ToggleAutostartCommand { get; }

    private void ToggleAutostart()
    {
        try
        {
            if (AutostartEnabled)
            {
                File.Delete(AutostartFile);
                Log("开机自启：已关闭");
            }
            else
            {
                var dir = Path.GetDirectoryName(AutostartFile)!;
                Directory.CreateDirectory(dir);
                var exe = Environment.ProcessPath ?? "/usr/bin/vk-sagittarius";
                File.WriteAllText(AutostartFile, $"""
                    [Desktop Entry]
                    Type=Application
                    Name=VK 射手座控制中心
                    Exec={exe} --headless
                    X-GNOME-Autostart-enabled=true
                    NoDisplay=false
                    Terminal=false
                    """);
                Log($"开机自启：已开启（{exe} --headless）");
            }
        }
        catch (Exception ex) { Log($"× 自启设置失败：{ex.Message}"); }
        OnPropertyChanged(nameof(AutostartEnabled));
        OnPropertyChanged(nameof(AutostartText));
    }

    public string AutostartText => AutostartEnabled ? "开机自启：开" : "开机自启：关";

    // ---------------------------------------------------------- 界面缩放

    /// <summary>
    /// 界面缩放。XWayland + Hyprland 的 fractional scale（如 1.875）不会传给 X11 程序，
    /// Avalonia 默认按 1.0 画，于是 4K 屏上字小、再被合成器放大发虚。
    /// 这里显式接管：CLI --scale / 环境变量 VKSAG_SCALE / 配置文件 / 界面选择，
    /// 用 LayoutTransform 缩放整棵可视树（矢量重绘，不是位图放大）。
    /// </summary>
    private double _uiScale = ScaleConfig.Resolve();

    public double UiScale
    {
        get => _uiScale;
        private set
        {
            if (!Set(ref _uiScale, value)) return;
            OnPropertyChanged(nameof(WindowWidth));
            OnPropertyChanged(nameof(WindowHeight));
            OnPropertyChanged(nameof(MinWindowWidth));
            OnPropertyChanged(nameof(MinWindowHeight));
            OnPropertyChanged(nameof(UiScaleText));
        }
    }

    public double WindowWidth => Math.Round(1200 * UiScale);
    public double WindowHeight => Math.Round(840 * UiScale);
    public double MinWindowWidth => Math.Round(900 * UiScale);
    public double MinWindowHeight => Math.Round(600 * UiScale);
    public string UiScaleText => $"{UiScale:0.###}×";

    public List<Option> ScaleOptions { get; } =
    [
        new Option(1000, "1.00×"), new Option(1250, "1.25×"), new Option(1500, "1.50×"),
        new Option(1750, "1.75×"), new Option(1875, "1.875×"), new Option(2000, "2.00×"),
    ];

    private Option _selectedScaleOption = null!;
    public Option SelectedScaleOption
    {
        get => _selectedScaleOption;
        set
        {
            if (!Set(ref _selectedScaleOption, value) || value is null) return;
            UiScale = value.Id / 1000.0;
            ScaleConfig.Save(UiScale);
            Log($"界面缩放 → {UiScale:0.###}×（已保存到 ~/.config/vk-sagittarius/config.json）");
        }
    }

    public RelayCommand ScaleResetCommand { get; }

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
        _uiTimer.Stop();
        _engine.Dispose();
        _set.Dispose();
    }
}
