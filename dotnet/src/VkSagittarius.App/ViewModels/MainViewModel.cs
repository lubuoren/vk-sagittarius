using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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
    public bool Online { get; init; }
    public IBrush Accent => Online ? Brushes.LimeGreen : Brushes.Gray;
}

/// <summary>
/// 前端：只做客户端。设备由 vksagd 独占，这里通过 Unix socket 发命令、收状态。
/// 关掉窗口不影响心跳（后端是独立进程 / systemd user service）。
/// </summary>
public sealed class MainViewModel : ObservableObject
{
    private readonly AppConfig _config;
    private DaemonClient? _client;
    private double _uiScale;
    private bool _ledSynced;
    private bool _daemonDemo;
    private string[] _daemonErrors = [];
    private string _audioBackend = "";
    private TelemetryDto _lastTelemetry = new(0, 0, 0, 0, 0, 0, 0, 0, 0, 0);

    public MainViewModel()
    {
        _config = AppConfig.Load();
        _uiScale = AppConfig.ParseScale(Environment.GetEnvironmentVariable("VKSAG_SCALE")) ?? _config.Scale;

        Devices = new ObservableCollection<DeviceItem>();
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

        for (var ch = 1; ch < 5; ch++) _fanModeByCh[ch] = _config.Fan.Modes[ch];
        Pwm1 = _config.Fan.Pwm[0];
        Pwm2 = _config.Fan.Pwm[1];
        Pwm3 = _config.Fan.Pwm[2];
        Temp1 = _config.Fan.Temp[0];
        Temp2 = _config.Fan.Temp[1];
        Temp3 = _config.Fan.Temp[2];

        _brightness = _config.Led.Brightness;
        _r = _config.Led.R; _g = _config.Led.G; _b = _config.Led.B;
        UpdateColor();

        _selectedLedMode = LedModes.FirstOrDefault(m => m.Id == _config.Led.Mode) ?? LedModes[0];
        _selectedLedChannel = LedChannels.FirstOrDefault(c => c.Id == _config.Led.Channel) ?? LedChannels[0];
        _selectedFanChannel = FanChannels[0];
        _selectedFanMode = FanModes.FirstOrDefault(m => m.Id == _fanModeByCh[1]) ?? FanModes[1];

        _selectedScaleOption = ScaleOptions.FirstOrDefault(o => Math.Abs(o.Id / 1000.0 - _uiScale) < 0.001)
                               ?? new Option((int)Math.Round(_uiScale * 1000), $"{_uiScale:0.###}×");
        if (!ScaleOptions.Contains(_selectedScaleOption)) ScaleOptions.Add(_selectedScaleOption);

        ApplyLedCommand = new RelayCommand(ApplyLed);
        ApplyFanCommand = new RelayCommand(ApplyFan);
        RefreshCommand = new RelayCommand(() => _ = RefreshAsync());
        RotateCommand = new RelayCommand(Rotate);
        ScreenCommand = new RelayCommand(Screen);
        ClockCommand = new RelayCommand(ToggleClock);
        PresetColorCommand = new RelayCommand(PresetColor);
        ToggleMonitorCommand = new RelayCommand(ToggleMonitor);
        ClearLogCommand = new RelayCommand(FrameLog.Clear);
        RescanCommand = new RelayCommand(Rescan);
        ToggleAutostartCommand = new RelayCommand(ToggleAutostart);
        ScaleResetCommand = new RelayCommand(() =>
        {
            if (ScaleOptions.FirstOrDefault(o => o.Id == 1000) is { } opt) SelectedScaleOption = opt;
        });

        _uiTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
        _uiTimer.Tick += (_, _) => PushHistory();
        _uiTimer.Start();

        _ = ConnectAsync();
    }

    // ---------------------------------------------------------- 后端连接

    private async Task ConnectAsync()
    {
        var client = new DaemonClient();
        client.StatusReceived += s => Dispatcher.UIThread.Post(() => ApplyStatus(s));
        client.LogReceived += (msg, _) => Dispatcher.UIThread.Post(() => Log(msg));
        client.Disconnected += () => Dispatcher.UIThread.Post(() =>
        {
            DaemonOnline = false;
            Log("× 与后台进程的连接已断开");
            OnPropertyChanged(nameof(ModeBanner));
            OnPropertyChanged(nameof(ModeDetail));
        });

        var ok = await client.ConnectAsync(TimeSpan.FromSeconds(1.5));
        if (!ok)
        {
            Log("后台进程未运行，尝试启动 vksagd…");
            TryStartDaemon();
            ok = await client.ConnectAsync(TimeSpan.FromSeconds(3));
        }

        if (!ok)
        {
            client.Dispose();
            DaemonOnline = false;
            Log("× 无法连接后台进程：`systemctl --user start vk-sagittarius` 或直接运行 vksagd");
            OnPropertyChanged(nameof(ModeBanner));
            OnPropertyChanged(nameof(ModeDetail));
            return;
        }

        _client = client;
        DaemonOnline = true;
        Log("已连接后台进程 vksagd");
        foreach (var line in TailDaemonLog()) Log(line);
        await RefreshAsync();
    }

    private static void TryStartDaemon()
    {
        try
        {
            using var p = Process.Start(new ProcessStartInfo("systemctl", "--user start vk-sagittarius")
            {
                UseShellExecute = false,
                RedirectStandardError = true,
            });
            if (p is not null && p.WaitForExit(2500) && p.ExitCode == 0) return;
        }
        catch (Exception) { }

        try
        {
            var exe = Path.Combine(AppContext.BaseDirectory, "vksagd");
            if (!File.Exists(exe)) return;
            Process.Start(new ProcessStartInfo(exe)
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            });
        }
        catch (Exception) { }
    }

    private static IEnumerable<string> TailDaemonLog()
    {
        try
        {
            if (!File.Exists(Ipc.LogFile)) return [];
            return File.ReadAllLines(Ipc.LogFile).TakeLast(6)
                .Select(l => l.Length > 11 ? l[11..] : l).ToArray();
        }
        catch (Exception)
        {
            return [];
        }
    }

    public bool DaemonOnline { get; private set; }
    public bool IsDemo => _daemonDemo;

    public string ModeBanner => !DaemonOnline
        ? "未连接后台进程"
        : _daemonDemo
            ? (_daemonErrors.FirstOrDefault()?.Contains("权限") == true
                ? "后台在演示模式：权限不足"
                : "后台在演示模式")
            : "后台已连接真实设备";

    public string ModeDetail => !DaemonOnline
        ? "GUI 需要 vksagd 后台进程。\n启动：systemctl --user start vk-sagittarius\n或直接运行 vksagd。"
        : string.Join("\n", _daemonErrors);

    public string AudioBanner => string.IsNullOrEmpty(_audioBackend)
        ? "音频后端：不可用（parec / pw-cat 未安装）"
        : $"音频后端：{_audioBackend}";

    // ---------------------------------------------------------- 状态应用

    private void ApplyStatus(StatusDto status)
    {
        _daemonDemo = status.Demo;
        _daemonErrors = status.Errors;
        _audioBackend = status.AudioBackend ?? "";
        MonitorRunning = status.MonitorRunning;
        FrameCount = status.Frames;

        OnPropertyChanged(nameof(ModeBanner));
        OnPropertyChanged(nameof(ModeDetail));
        OnPropertyChanged(nameof(IsDemo));
        OnPropertyChanged(nameof(AudioBanner));

        SyncDevices(status);
        ApplyTelemetry(status.Telemetry);
        ApplyState(status.State);
    }

    private void SyncDevices(StatusDto status)
    {
        var wanted = status.Devices.Select(d => Proto.ClassName(d.DeviceClass)).ToList();
        if (Devices.Count == wanted.Count && Devices.Select(d => d.Title).SequenceEqual(wanted)) return;

        Devices.Clear();
        foreach (var d in status.Devices)
        {
            Devices.Add(new DeviceItem
            {
                Title = Proto.ClassName(d.DeviceClass),
                Subtitle = $"{d.Path}   VID 0x{Proto.Vid:X4}  PID 0x{Proto.PidForClass(d.DeviceClass):X4}",
                Online = !d.Path.Contains("fake"),
            });
        }
    }

    private void ApplyTelemetry(TelemetryDto t)
    {
        _lastTelemetry = t;
        CpuTemp = $"{t.CpuTemp:0.0} ℃";
        GpuTemp = $"{t.GpuTemp:0.0} ℃";
        CpuUsage = $"{t.CpuUsage} %";
        MemUsage = $"{t.MemUsage} %";
        CpuFreq = $"{t.CpuFreqMhz:0} MHz";
        FanRpm = $"{t.CpuFanRpm} RPM";
    }

    private void ApplyState(StateDto? state)
    {
        if (state is null) return;

        foreach (var f in state.Fans)
        {
            SetRpm(f.Channel, f.Rpm);
            if (f.Channel is >= 1 and <= 4) _fanModeByCh[f.Channel] = f.Mode;
        }
        if (SelectedFanChannel is { } cur &&
            FanModes.FirstOrDefault(m => m.Id == _fanModeByCh[cur.Id]) is { } m2)
            SelectedFanMode = m2;

        if (!_ledSynced && state.Led is { } led)
        {
            _ledSynced = true;
            SelectedLedMode = LedModes.FirstOrDefault(m => m.Id == led.Mode) ?? SelectedLedMode;
            Brightness = led.Brightness;
            Speed = led.Speed switch { 1 => 100, 2 => 50, 4 => 1, _ => Speed };
            R = led.R;
            G = led.G;
            B = led.B;
            Log($"从设备回读：模式 {led.Mode}，亮度 {led.Brightness}，颜色 #{led.R:X2}{led.G:X2}{led.B:X2}");
        }

        var parts = state.Fans.OrderBy(f => f.Channel)
            .Select(f => $"风扇{f.Channel}: {f.Mode}档 {f.Rpm}RPM").ToList();
        if (state.Led is { } l)
            parts.Add($"灯效: 模式{l.Mode} 亮度{l.Brightness} 速度{l.Speed} #{l.R:X2}{l.G:X2}{l.B:X2}");
        LedStateText = parts.Count > 0 ? string.Join(" | ", parts) : "（等待设备回报）";
    }

    private async Task RefreshAsync()
    {
        if (_client is null) return;
        var resp = await _client.StatusAsync(TimeSpan.FromSeconds(2));
        if (resp.Status is { } st) ApplyStatus(st);
    }

    // ---------------------------------------------------------- 遥测展示

    public ObservableCollection<DeviceItem> Devices { get; }

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

    private int CurrentSpeedCode => Speed < 33 ? Proto.LedSpeedSlow : Speed < 66 ? Proto.LedSpeedMid : Proto.LedSpeedFast;

    private void ApplyLed()
    {
        var speed = CurrentSpeedCode;
        _config.Led.Mode = SelectedLedMode.Id;
        _config.Led.Channel = SelectedLedChannel.Id;
        _config.Led.Brightness = (int)Brightness;
        _config.Led.Speed = speed;
        _config.Led.R = (int)R;
        _config.Led.G = (int)G;
        _config.Led.B = (int)B;
        _config.Save();

        Send(new IpcRequest
            {
                Op = Ipc.OpLed,
                Led = new LedRequest(SelectedLedMode.Id, SelectedLedChannel.Id, (int)Brightness, speed,
                    (int)R, (int)G, (int)B),
            },
            SelectedLedMode.Id == 10
                ? $"LED → 全彩自动（通道 {SelectedLedChannel.Id}，速度 {speed}）"
                : $"LED → 模式 {SelectedLedMode.Id} 通道 {SelectedLedChannel.Id} 亮度 {Brightness:0} 速度 {speed} #{ColorHex}");
    }

    // ---------------------------------------------------------- 风扇

    public IReadOnlyList<Option> FanChannels { get; }
    public ObservableCollection<Option> FanModes { get; }

    private readonly int[] _fanModeByCh = [0, 2, 2, 2, 2];

    private Option _selectedFanChannel;
    public Option SelectedFanChannel
    {
        get => _selectedFanChannel;
        set
        {
            if (!Set(ref _selectedFanChannel, value) || value is null) return;
            SelectedFanMode = FanModes.FirstOrDefault(m => m.Id == _fanModeByCh[value.Id]) ?? FanModes[1];
        }
    }

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
        var ch = SelectedFanChannel.Id;
        var mode = SelectedFanMode.Id;
        _fanModeByCh[ch] = mode;
        _config.Fan.Modes = (int[])_fanModeByCh.Clone();
        _config.Fan.Pwm = [(int)Pwm1, (int)Pwm2, (int)Pwm3];
        _config.Fan.Temp = [(int)Temp1, (int)Temp2, (int)Temp3];
        _config.Save();

        Send(new IpcRequest
            {
                Op = Ipc.OpFan,
                Fan = mode == Proto.FanModeCustom
                    ? new FanRequest(ch, mode, [(int)Pwm1, (int)Pwm2, (int)Pwm3], [(int)Temp1, (int)Temp2, (int)Temp3])
                    : new FanRequest(ch, mode, null, null),
            },
            mode == Proto.FanModeCustom
                ? $"FAN → 通道 {ch} 自定义 PWM[{(int)Pwm1},{(int)Pwm2},{(int)Pwm3}] TEMP[{(int)Temp1},{(int)Temp2},{(int)Temp3}]（已保存）"
                : $"FAN → 通道 {ch} 预设「{SelectedFanMode.Label}」（已保存）");
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
        Send(new IpcRequest { Op = Ipc.OpLcd, Sub = (int)sub }, $"LCD → 旋转 {param}°");
    }

    private void Screen(object? param) =>
        Send(new IpcRequest
            {
                Op = Ipc.OpLcd,
                Sub = (int)(param?.ToString() == "black" ? Proto.LcdSub.Black : Proto.LcdSub.White),
            },
            param?.ToString() == "black" ? "LCD → 整屏黑" : "LCD → 整屏白");

    private void ToggleClock() =>
        Send(new IpcRequest { Op = Ipc.OpClock, Hour24 = Hour24, Blank = !ClockEnabled },
            ClockEnabled ? "LCD → 显示时间" : "LCD → 隐藏时间");

    public async Task UploadAsync(string path)
    {
        var resp = await SendAsync(new IpcRequest { Op = Ipc.OpUpload, FilePath = path }, null);
        Log(resp.Ok ? $"✓ 背景上传完成：{Path.GetFileName(path)}" : $"× 上传失败：{resp.Error}");
    }

    // ---------------------------------------------------------- 命令发送

    private void Send(IpcRequest request, string logMessage)
    {
        Log(logMessage);
        _ = SendAsync(request, null);
    }

    private async Task<IpcResponse> SendAsync(IpcRequest request, string? logMessage)
    {
        if (_client is null)
        {
            Log("× 未连接后台进程");
            return new IpcResponse { Ok = false, Error = "未连接后台进程" };
        }
        if (logMessage is not null) Log(logMessage);
        var resp = await _client.SendAsync(request, TimeSpan.FromSeconds(60));
        if (!resp.Ok) Log($"× {request.Op} 失败：{resp.Error}");
        return resp;
    }

    // ---------------------------------------------------------- 监控 / 日志

    private bool _monitorRunning = true;
    public bool MonitorRunning
    {
        get => _monitorRunning;
        private set { if (Set(ref _monitorRunning, value)) OnPropertyChanged(nameof(MonitorButtonText)); }
    }
    public string MonitorButtonText => MonitorRunning ? "暂停心跳" : "启动心跳";

    private long _frameCount;
    public long FrameCount { get => _frameCount; private set => Set(ref _frameCount, value); }

    public RelayCommand ToggleMonitorCommand { get; }
    public RelayCommand RefreshCommand { get; }
    public RelayCommand ClearLogCommand { get; }
    public RelayCommand RescanCommand { get; }

    private void ToggleMonitor()
    {
        var op = MonitorRunning ? Ipc.OpPause : Ipc.OpResume;
        Send(new IpcRequest { Op = op }, MonitorRunning ? "心跳已暂停" : "心跳已恢复");
        MonitorRunning = !MonitorRunning;
    }

    /// <summary>后台不支持进程内重扫：重启服务后重连。</summary>
    private void Rescan()
    {
        Log("重启后台进程以重新扫描设备…");
        try
        {
            using var p = Process.Start(new ProcessStartInfo("systemctl", "--user restart vk-sagittarius")
            {
                UseShellExecute = false,
                RedirectStandardError = true,
            });
            p?.WaitForExit(5000);
        }
        catch (Exception ex)
        {
            Log($"× 重启失败：{ex.Message}");
        }
        _client?.Dispose();
        _client = null;
        _ = ConnectAsync();
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

    // ---------------------------------------------------------- 系统信息 / 曲线

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
        _cpuHist.Add(_lastTelemetry.CpuTemp);
        _gpuHist.Add(_lastTelemetry.GpuTemp);
        if (_cpuHist.Count > 60) _cpuHist.RemoveAt(0);
        if (_gpuHist.Count > 60) _gpuHist.RemoveAt(0);
        OnPropertyChanged(nameof(CpuGeometry));
        OnPropertyChanged(nameof(GpuGeometry));
    }

    private readonly DispatcherTimer _uiTimer;

    // ---------------------------------------------------------- 开机自启

    private static string AutostartFile => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        ".config", "autostart", "vk-sagittarius.desktop");

    public bool AutostartEnabled => File.Exists(AutostartFile);
    public string AutostartText => AutostartEnabled ? "开机自启：开" : "开机自启：关";
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
                Directory.CreateDirectory(Path.GetDirectoryName(AutostartFile)!);
                var exe = Environment.ProcessPath ?? "/usr/bin/vk-sagittarius";
                File.WriteAllText(AutostartFile, $"""
                    [Desktop Entry]
                    Type=Application
                    Name=VK 射手座控制中心
                    Exec={exe} --tray
                    X-GNOME-Autostart-enabled=true
                    NoDisplay=false
                    Terminal=false
                    """);
                Log($"开机自启：已开启（{exe} --tray，只驻留托盘）");
            }
        }
        catch (Exception ex) { Log($"× 自启设置失败：{ex.Message}"); }
        OnPropertyChanged(nameof(AutostartEnabled));
        OnPropertyChanged(nameof(AutostartText));
    }

    // ---------------------------------------------------------- 界面缩放

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
            _config.Scale = UiScale;
            _config.Save();
            Log($"界面缩放 → {UiScale:0.###}×（已保存）");
        }
    }

    public RelayCommand ScaleResetCommand { get; }

    public void Shutdown()
    {
        _uiTimer.Stop();
        _client?.Dispose();
        _client = null;
    }
}
