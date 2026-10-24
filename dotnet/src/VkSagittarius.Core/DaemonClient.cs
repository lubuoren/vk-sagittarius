using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace VkSagittarius.Core;

/// <summary>
/// GUI 侧的守护进程客户端。协议：一行一条 JSON。
///   请求：{"id":1,"op":"led","led":{...}}
///   应答：{"id":1,"ok":true}            ← 按 id 匹配
///   事件：{"event":"status","status":{...}} / {"event":"log","message":"..."}
/// </summary>
public sealed class DaemonClient : IDisposable
{
    private Socket? _socket;
    private NetworkStream? _stream;
    private CancellationTokenSource? _cts;
    private readonly SemaphoreSlim _writeLock = new(1, 1);
    private readonly ConcurrentDictionary<int, TaskCompletionSource<IpcResponse>> _pending = new();
    private readonly StringBuilder _buffer = new();
    private int _nextId;

    public string SocketPath { get; }
    public bool Connected => _socket?.Connected == true;

    public event Action<StatusDto>? StatusReceived;
    public event Action<string, string>? LogReceived;     // (message, level)
    public event Action? Disconnected;

    public DaemonClient(string? socketPath = null) => SocketPath = socketPath ?? Ipc.SocketPath();

    public async Task<bool> ConnectAsync(TimeSpan timeout)
    {
        var deadline = DateTime.UtcNow + timeout;
        while (DateTime.UtcNow < deadline)
        {
            try
            {
                var socket = new Socket(AddressFamily.Unix, SocketType.Stream, ProtocolType.Unspecified);
                await socket.ConnectAsync(new UnixDomainSocketEndPoint(SocketPath));
                _socket = socket;
                _stream = new NetworkStream(socket, ownsSocket: true);
                _cts = new CancellationTokenSource();
                _ = Task.Run(() => ReadLoop(_cts.Token));
                return true;
            }
            catch (Exception)
            {
                await Task.Delay(150);
            }
        }
        return false;
    }

    private async Task ReadLoop(CancellationToken token)
    {
        var buffer = new byte[8192];
        try
        {
            while (!token.IsCancellationRequested && _stream is not null)
            {
                var n = await _stream.ReadAsync(buffer, token);
                if (n <= 0) break;
                _buffer.Append(Encoding.UTF8.GetString(buffer, 0, n));

                while (true)
                {
                    var text = _buffer.ToString();
                    var nl = text.IndexOf('\n');
                    if (nl < 0) break;
                    var line = text[..nl];
                    _buffer.Remove(0, nl + 1);
                    if (line.Trim().Length == 0) continue;
                    Dispatch(line);
                }
            }
        }
        catch (OperationCanceledException) { }
        catch (Exception) { }
        finally
        {
            foreach (var kv in _pending)
                kv.Value.TrySetResult(new IpcResponse { Id = kv.Key, Ok = false, Error = "连接已断开" });
            _pending.Clear();
            Disconnected?.Invoke();
        }
    }

    private void Dispatch(string line)
    {
        try
        {
            using var doc = JsonDocument.Parse(line);
            var root = doc.RootElement;

            if (root.TryGetProperty("event", out var ev))
            {
                var kind = ev.GetString();
                if (kind == "status" && root.TryGetProperty("status", out var st))
                {
                    var status = st.Deserialize<StatusDto>(Ipc.Json);
                    if (status is not null) StatusReceived?.Invoke(status);
                }
                else if (kind == "log")
                {
                    var msg = root.TryGetProperty("message", out var m) ? m.GetString() ?? "" : "";
                    var lvl = root.TryGetProperty("level", out var l) ? l.GetString() ?? "info" : "info";
                    LogReceived?.Invoke(msg, lvl);
                }
                return;
            }

            if (root.TryGetProperty("id", out var idEl) && idEl.TryGetInt32(out var id))
            {
                var resp = root.Deserialize<IpcResponse>(Ipc.Json);
                if (resp is not null && _pending.TryRemove(id, out var tcs))
                    tcs.TrySetResult(resp);
            }
        }
        catch (JsonException)
        {
            // 忽略坏行
        }
    }

    public async Task<IpcResponse> SendAsync(IpcRequest request, TimeSpan? timeout = null)
    {
        if (_stream is null)
            return new IpcResponse { Id = request.Id, Ok = false, Error = "未连接守护进程" };

        var id = Interlocked.Increment(ref _nextId);
        request.Id = id;
        var tcs = new TaskCompletionSource<IpcResponse>(TaskCreationOptions.RunContinuationsAsynchronously);
        _pending[id] = tcs;

        var payload = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(request, Ipc.Json) + "\n");
        try
        {
            await _writeLock.WaitAsync();
            try { await _stream.WriteAsync(payload); }
            finally { _writeLock.Release(); }
        }
        catch (Exception ex)
        {
            _pending.TryRemove(id, out _);
            return new IpcResponse { Id = id, Ok = false, Error = ex.Message };
        }

        var wait = timeout ?? TimeSpan.FromSeconds(10);
        var finished = await Task.WhenAny(tcs.Task, Task.Delay(wait));
        if (finished == tcs.Task) return await tcs.Task;

        _pending.TryRemove(id, out _);
        return new IpcResponse { Id = id, Ok = false, Error = "守护进程无响应" };
    }

    public Task<IpcResponse> StatusAsync(TimeSpan? timeout = null) =>
        SendAsync(new IpcRequest { Op = Ipc.OpStatus }, timeout);

    public void Dispose()
    {
        _cts?.Cancel();
        try { _stream?.Dispose(); } catch (Exception) { }
        try { _socket?.Dispose(); } catch (Exception) { }
        _cts?.Dispose();
        _writeLock.Dispose();
    }
}
