using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace VkSagittarius.Core;

/// <summary>
/// 后台守护进程：独占 HID 设备、跑心跳、通过 Unix socket 对外提供控制 API。
/// GUI 只是客户端，关掉窗口不影响 LCD/灯效心跳——这是与 RustDesk 相同的分工。
/// </summary>
public sealed class DaemonServer : IDisposable
{
    private readonly DeviceBootstrap _boot;
    private readonly DeviceSet _set;
    private readonly MonitorEngine _engine;
    private readonly string _socketPath;
    private readonly object _clientsLock = new();
    private readonly List<Client> _clients = new();
    private Socket? _listener;
    private CancellationTokenSource? _cts;
    private Task? _acceptTask;
    private Task? _broadcastTask;
    private readonly StringBuilder _logRing = new();

    public const string Version = "1.0.7";
    public bool Demo => _boot.Demo;
    public bool MonitorRunning { get; private set; }
    public event Action<string>? Log;

    private readonly bool _persistLog;

    public DaemonServer(DeviceBootstrap boot, string? socketPath = null, bool persistLog = true)
    {
        _persistLog = persistLog;
        _boot = boot;
        _set = boot.Set;
        _socketPath = socketPath ?? Ipc.SocketPath();
        _engine = new MonitorEngine(_set, m => WriteLog(m));
        _engine.StateUpdated += _ => BroadcastStatusAsync().ConfigureAwait(false);
    }

    public void Start()
    {
        _cts = new CancellationTokenSource();

        if (File.Exists(_socketPath))
        {
            // 上一个进程可能没清干净：先试连，连得上说明已有实例
            if (IsAlive(_socketPath))
                throw new InvalidOperationException($"已有守护进程在监听 {_socketPath}");
            File.Delete(_socketPath);
        }

        _listener = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.Unspecified);
        _listener.Bind(new UnixDomainSocketEndPoint(_socketPath));
        _listener.Listen(8);

        _set.RegisterAll();
        _engine.Start();
        MonitorRunning = true;

        WriteLog($"守护进程监听 {_socketPath}（{_set.Devices.Count} 台设备）");
        if (_boot.Demo)
            foreach (var e in _boot.Errors) WriteLog($"演示模式原因：{e}", "warn");

        _acceptTask = Task.Run(() => AcceptLoop(_cts.Token));
        _broadcastTask = Task.Run(() => BroadcastLoop(_cts.Token));
    }

    private static bool IsAlive(string path)
    {
        try
        {
            using var probe = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.Unspecified);
            probe.Connect(new UnixDomainSocketEndPoint(path));
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private async Task AcceptLoop(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            Socket socket;
            try
            {
                socket = await _listener!.AcceptAsync(token);
            }
            catch (OperationCanceledException) { break; }
            catch (ObjectDisposedException) { break; }
            catch (Exception ex)
            {
                WriteLog($"接受连接失败：{ex.Message}", "error");
                continue;
            }
            var client = new Client(socket);
            lock (_clientsLock) _clients.Add(client);
            WriteLog($"GUI 已连接（{_clients.Count}）");
            _ = Task.Run(() => Serve(client, token), token);
        }
    }

    private async Task Serve(Client client, CancellationToken token)
    {
        var buffer = new byte[8192];
        var pending = new StringBuilder();
        try
        {
            while (!token.IsCancellationRequested)
            {
                var n = await client.Stream.ReadAsync(buffer, token);
                if (n <= 0) break;
                pending.Append(Encoding.UTF8.GetString(buffer, 0, n));

                while (true)
                {
                    var text = pending.ToString();
                    var nl = text.IndexOf('\n');
                    if (nl < 0) break;
                    var line = text[..nl];
                    pending.Remove(0, nl + 1);
                    if (line.Trim().Length == 0) continue;
                    await HandleLine(client, line);
                }
            }
        }
        catch (OperationCanceledException) { }
        catch (IOException) { }
        catch (Exception ex)
        {
            WriteLog($"客户端异常：{ex.Message}", "warn");
        }
        finally
        {
            lock (_clientsLock) _clients.Remove(client);
            client.Dispose();
            WriteLog($"GUI 断开（{_clients.Count}）");
        }
    }

    private async Task HandleLine(Client client, string line)
    {
        IpcRequest? req;
        try
        {
            req = JsonSerializer.Deserialize<IpcRequest>(line, Ipc.Json);
        }
        catch (JsonException ex)
        {
            WriteLog($"请求解析失败：{ex.Message}", "error");
            return;
        }
        if (req is null) return;

        var resp = new IpcResponse { Id = req.Id };
        try
        {
            switch (req.Op)
            {
                case Ipc.OpStatus:
                    resp.Ok = true;
                    resp.Status = BuildStatus();
                    break;

                case Ipc.OpLed:
                    if (req.Led is not { } led) throw new ArgumentException("缺少 led 参数");
                    var target = _set.FanLed ?? throw new InvalidOperationException("没有风扇/灯效设备");
                    if (led.Mode == 10)
                    {
                        var (mode, payload) = Proto.LedAutoPayload(led.Speed);
                        target.Send(Proto.BuildReport(target.Class, target.Index, Proto.Cmd.Led,
                            led.Channel, mode, payload));
                    }
                    else
                    {
                        target.SetLed(led.Mode, (led.R, led.G, led.B), led.Brightness, led.Speed, led.Channel);
                    }
                    WriteLog($"LED ← 模式 {led.Mode} 通道 {led.Channel} 亮度 {led.Brightness} " +
                             $"速度 {led.Speed} #{led.R:X2}{led.G:X2}{led.B:X2}");
                    resp.Ok = true;
                    break;

                case Ipc.OpFan:
                    if (req.Fan is not { } fan) throw new ArgumentException("缺少 fan 参数");
                    var fanDev = _set.FanLed ?? throw new InvalidOperationException("没有风扇/灯效设备");
                    if (fan.Mode == Proto.FanModeCustom)
                    {
                        if (fan.Pwm is not { Length: 3 } || fan.Temp is not { Length: 3 })
                            throw new ArgumentException("自定义曲线需要 3 个 PWM 点 + 3 个温度点");
                        fanDev.SetFan(fan.Channel, Proto.FanModeCustom, (fan.Pwm, fan.Temp));
                    }
                    else
                    {
                        fanDev.SetFan(fan.Channel, fan.Mode);
                    }
                    WriteLog($"FAN ← 通道 {fan.Channel} 模式 {fan.Mode}");
                    resp.Ok = true;
                    break;

                case Ipc.OpLcd:
                {
                    var lcd = _set.Lcd ?? throw new InvalidOperationException("没有 LCD 设备");
                    lcd.LcdSimple((Proto.LcdSub)req.Sub);
                    WriteLog($"LCD ← 子命令 {req.Sub}");
                    resp.Ok = true;
                    break;
                }

                case Ipc.OpClock:
                {
                    var lcd = _set.Lcd ?? throw new InvalidOperationException("没有 LCD 设备");
                    lcd.LcdClock(req.Hour24, req.Blank);
                    resp.Ok = true;
                    break;
                }

                case Ipc.OpUpload:
                {
                    var lcd = _set.Lcd ?? throw new InvalidOperationException("没有 LCD 设备");
                    if (string.IsNullOrEmpty(req.FilePath)) throw new ArgumentException("缺少文件路径");
                    var path = req.FilePath;
                    resp.Ok = await Task.Run(() =>
                        new LcdUploader(m => WriteLog("上传：" + m)).Upload(lcd, path));
                    break;
                }

                case Ipc.OpRescan:
                    throw new NotSupportedException("进程内重扫需要重启守护进程（systemctl --user restart vk-sagittarius）");

                case Ipc.OpPause:
                    _engine.Stop();
                    MonitorRunning = false;
                    WriteLog("心跳已暂停");
                    resp.Ok = true;
                    break;

                case Ipc.OpResume:
                    _engine.Start();
                    MonitorRunning = true;
                    WriteLog("心跳已恢复");
                    resp.Ok = true;
                    break;

                case Ipc.OpQuit:
                    resp.Ok = true;
                    WriteLog("收到退出请求");
                    _ = Task.Run(Dispose);
                    break;

                default:
                    throw new NotSupportedException($"未知命令 {req.Op}");
            }
        }
        catch (Exception ex)
        {
            resp.Ok = false;
            resp.Error = ex.Message;
            WriteLog($"命令 {req.Op} 失败：{ex.Message}", "error");
        }

        await client.SendAsync(JsonSerializer.Serialize(resp, Ipc.Json) + "\n");
    }

    private async Task BroadcastLoop(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            try { await Task.Delay(1000, token); }
            catch (OperationCanceledException) { break; }
            await BroadcastStatusAsync();
        }
    }

    private async Task BroadcastStatusAsync()
    {
        Client[] snapshot;
        lock (_clientsLock)
        {
            if (_clients.Count == 0) return;
            snapshot = _clients.ToArray();
        }
        var payload = JsonSerializer.Serialize(new IpcEvent
        {
            Event = "status",
            Status = BuildStatus(),
        }, Ipc.Json) + "\n";
        foreach (var c in snapshot)
        {
            try { await c.SendAsync(payload); }
            catch (Exception) { lock (_clientsLock) _clients.Remove(c); }
        }
    }

    public StatusDto BuildStatus()
    {
        var tele = _set.Snapshot;
        var st = _engine.LastState;
        StateDto? state = null;
        if (st is not null)
        {
            var fans = st.Fans.OrderBy(k => k.Key)
                .Select(kv => new FanDto(kv.Key, kv.Value.Mode, kv.Value.Rpm)).ToArray();
            LedDto? led = st.Led is { } l ? new LedDto(l.Mode, l.Brightness, l.Speed, l.R, l.G, l.B) : null;
            state = new StateDto(fans, led);
        }

        return new StatusDto(
            _set.Devices.Select(d => new DeviceDto(d.Class, d.Path, d.Index, d.Name)).ToArray(),
            _boot.Demo,
            _boot.Errors.ToArray(),
            MonitorRunning,
            _engine.FramesSent,
            _engine.StateUpdates,
            TelemetryDto.From(tele),
            state,
            _set.Meter.Backend,
            Version);
    }

    private void WriteLog(string message, string level = "info")
    {
        var line = $"{DateTime.Now:HH:mm:ss} {message}";
        Log?.Invoke(line);
        if (!_persistLog) return;
        try
        {
            Directory.CreateDirectory(Ipc.ConfigDir);
            File.AppendAllText(Ipc.LogFile, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} [{level}] {message}\n");
        }
        catch (Exception) { }

        Client[] snapshot;
        lock (_clientsLock) snapshot = _clients.ToArray();
        if (snapshot.Length == 0) return;
        var payload = JsonSerializer.Serialize(new IpcEvent { Event = "log", Message = line, Level = level }, Ipc.Json) + "\n";
        foreach (var c in snapshot)
        {
            try { c.SendAsync(payload).GetAwaiter().GetResult(); }
            catch (Exception) { lock (_clientsLock) _clients.Remove(c); }
        }
    }

    private bool _disposed;

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;

        try { _cts?.Cancel(); } catch (ObjectDisposedException) { }
        try { _engine.Dispose(); } catch (Exception) { }
        try { _listener?.Close(); } catch (Exception) { }
        try { _set.Dispose(); } catch (Exception) { }
        try { if (File.Exists(_socketPath)) File.Delete(_socketPath); } catch (Exception) { }
        try { _cts?.Dispose(); } catch (Exception) { }
    }

    private sealed class Client : IDisposable
    {
        private readonly SemaphoreSlim _writeLock = new(1, 1);
        public NetworkStream Stream { get; }

        public Client(Socket socket) => Stream = new NetworkStream(socket, ownsSocket: true);

        public async Task SendAsync(string payload)
        {
            var bytes = Encoding.UTF8.GetBytes(payload);
            await _writeLock.WaitAsync();
            try { await Stream.WriteAsync(bytes); }
            finally { _writeLock.Release(); }
        }

        public void Dispose()
        {
            try { Stream.Dispose(); } catch (Exception) { }
            _writeLock.Dispose();
        }
    }
}
