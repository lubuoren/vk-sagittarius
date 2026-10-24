using System;
using System.Buffers.Binary;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace VkSagittarius.Core;

/// <summary>
/// 音频电平表。Windows 端用 CSCore 取默认播放设备所有会话的峰值；
/// Linux 端从默认 sink 的 monitor 源抓 s16le 单声道裸流算峰值。
/// parec / pw-cat 都不存在时静默返回 0，不影响其它功能。
/// </summary>
public sealed class AudioMeter : IDisposable
{
    private readonly int _rate;
    private readonly int _chunkSamples;
    private Thread? _thread;
    private Process? _proc;
    private volatile bool _stop;
    private double _peak;

    public string? Backend { get; private set; }
    public double Level => _peak;
    public int Audio8 => (int)Math.Clamp(_peak * 255, 0, 255);
    public int Audio12 => (int)Math.Clamp(_peak * 4095, 0, 4095);

    public AudioMeter(int rate = 8000, int chunkMs = 20)
    {
        _rate = rate;
        _chunkSamples = Math.Max(1, rate * chunkMs / 1000);
    }

    public bool Start()
    {
        var (file, args) = PickCommand();
        if (file is null) return false;
        try
        {
            _proc = Process.Start(new ProcessStartInfo(file, args)
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
            });
        }
        catch (Exception)
        {
            return false;
        }
        if (_proc is null) return false;
        Backend = file;
        _thread = new Thread(Pump) { IsBackground = true, Name = "vksag-audio" };
        _thread.Start();
        return true;
    }

    private (string? File, string Args) PickCommand()
    {
        if (Which("parec") is { } parec)
            return (parec, $"--format=s16le --rate={_rate} --channels=1 --latency-msec=20 --device=@DEFAULT_MONITOR@");
        if (Which("pw-cat") is { } pwcat)
            return (pwcat, $"--record --raw --format=s16 --rate={_rate} --channels=1 --target=@DEFAULT_AUDIO_SINK@ -");
        return (null, "");
    }

    private static string? Which(string name)
    {
        var path = Environment.GetEnvironmentVariable("PATH") ?? "";
        foreach (var dir in path.Split(Path.PathSeparator, StringSplitOptions.RemoveEmptyEntries))
        {
            var candidate = Path.Combine(dir, name);
            if (File.Exists(candidate)) return candidate;
        }
        return null;
    }

    private void Pump()
    {
        var stream = _proc?.StandardOutput.BaseStream;
        if (stream is null) return;
        var buf = new byte[_chunkSamples * 2];
        var carry = new byte[2];
        var carryLen = 0;

        while (!_stop)
        {
            int read;
            try { read = stream.Read(buf, carryLen, buf.Length - carryLen); }
            catch (IOException) { break; }
            catch (ObjectDisposedException) { break; }
            if (read <= 0) break;

            var total = carryLen + read;
            var usable = total - (total % 2);
            var span = new ReadOnlySpan<byte>(buf, 0, usable);
            var peak = 0;
            for (var i = 0; i + 1 < usable; i += 2)
            {
                var s = Math.Abs(BinaryPrimitives.ReadInt16LittleEndian(span[i..]));
                if (s > peak) peak = s;
            }
            var level = peak / 32768.0;
            // 快升慢降，贴近 CSCore 每 100ms 采样的观感
            _peak = level > _peak ? level : _peak * 0.6;
            carryLen = total - usable;
            if (carryLen > 0) buf.AsSpan(usable, carryLen).CopyTo(carry);
            Buffer.BlockCopy(carry, 0, buf, 0, carryLen);
        }
        _peak = 0;
    }

    public void Dispose()
    {
        _stop = true;
        try { if (_proc is { HasExited: false }) _proc.Kill(); } catch (Exception) { }
        _thread?.Join(500);
        _proc?.Dispose();
    }
}
