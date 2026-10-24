using System;
using System.Threading;
using System.Threading.Tasks;

namespace VkSagittarius.Core;

/// <summary>
/// 心跳循环，复刻原程序的多路定时器：
///   100ms  → cmd 4（音频电平 + 遥测），送给类别 1/2 设备
///   1000ms → cmd 2（状态回读），送给类别 1/2 设备
///   1000ms → LCD 三段文本（温度 0 / 频率 32 / 时间 64），每段间隔 330ms
/// 原程序用三个 System.Timers.Timer 并发跑，这里合成一个调度循环，便于测试与退出。
/// </summary>
public sealed class MonitorEngine : IDisposable
{
    private readonly DeviceSet _set;
    private readonly Action<string> _log;
    private CancellationTokenSource? _cts;
    private Task? _loop;

    public Proto.DeviceState? LastState { get; private set; }
    public TelemetrySnapshot LastTelemetry { get; private set; } = TelemetrySnapshot.Empty;
    public DateTimeOffset LastStateAt { get; private set; }
    public long StateUpdates { get; private set; }
    public long FramesSent { get; private set; }
    public bool ClockEnabled { get; set; } = true;
    public bool Hour24 { get; set; } = true;

    public event Action<Proto.DeviceState>? StateUpdated;

    public MonitorEngine(DeviceSet set, Action<string>? log = null)
    {
        _set = set;
        _log = log ?? (_ => { });
    }

    public void Start()
    {
        if (_loop is not null) return;
        _cts = new CancellationTokenSource();
        _loop = Task.Run(() => RunAsync(_cts.Token));
    }

    public async Task RunAsync(CancellationToken token)
    {
        var nextAudio = 0L;
        var nextPoll = 0L;
        var nextLcd = 0L;
        var lcdQueue = new Queue<(long Due, Proto.LcdSub Sub)>();
        var clock = Environment.TickCount64;

        while (!token.IsCancellationRequested)
        {
            var now = Environment.TickCount64;
            var tele = _set.Refresh();
            LastTelemetry = tele;

            if (now >= nextAudio)
            {
                nextAudio = now + 100;
                var fan = _set.FanLed;
                if (fan is not null)
                {
                    try
                    {
                        fan.SendAudio(tele, _set.Meter.Audio8, _set.Meter.Audio12);
                        FramesSent++;
                    }
                    catch (Exception ex) { _log($"音频帧发送失败：{ex.Message}"); }
                }
            }

            if (now >= nextPoll)
            {
                nextPoll = now + 1000;
                var fan = _set.FanLed;
                if (fan is not null)
                {
                    try
                    {
                        var st = fan.RequestState(300, tele);
                        if (st is not null)
                        {
                            LastState = st;
                            LastStateAt = DateTimeOffset.Now;
                            StateUpdates++;
                            StateUpdated?.Invoke(st);
                        }
                    }
                    catch (Exception ex) { _log($"状态回读失败：{ex.Message}"); }
                }
            }

            // LCD 三段文本按 0/330/660ms 排队发送，不阻塞音频与状态轮询
            if (now >= nextLcd)
            {
                nextLcd = now + 1000;
                if (_set.Lcd is not null)
                {
                    lcdQueue.Enqueue((now, Proto.LcdSub.Temp));
                    lcdQueue.Enqueue((now + 330, Proto.LcdSub.Freq));
                    lcdQueue.Enqueue((now + 660, Proto.LcdSub.Clock));
                }
            }
            while (lcdQueue.Count > 0 && now >= lcdQueue.Peek().Due)
            {
                var (_, sub) = lcdQueue.Dequeue();
                var lcd = _set.Lcd;
                if (lcd is null) continue;
                try
                {
                    switch (sub)
                    {
                        case Proto.LcdSub.Temp: lcd.LcdTemps(tele); break;
                        case Proto.LcdSub.Freq: lcd.LcdFreqs(tele); break;
                        default: lcd.LcdClock(Hour24, blank: !ClockEnabled); break;
                    }
                    FramesSent++;
                }
                catch (Exception ex) { _log($"LCD 更新失败：{ex.Message}"); }
            }

            var elapsed = Environment.TickCount64 - clock;
            var sleep = 20 - (int)(elapsed % 20);
            if (sleep < 1) sleep = 1;
            try { await Task.Delay(sleep, token); }
            catch (OperationCanceledException) { break; }
        }
    }

    /// <summary>一轮 LCD 刷新：温度 → 频率 → 时间，间隔 330ms（原程序 Thread.Sleep(330)）。</summary>
    public void SendLcdCycle(VkDevice lcd, TelemetrySnapshot tele, CancellationToken token = default)
    {
        lcd.LcdTemps(tele);
        FramesSent++;
        if (token.WaitHandle.WaitOne(330)) return;
        lcd.LcdFreqs(tele);
        FramesSent++;
        if (token.WaitHandle.WaitOne(330)) return;
        lcd.LcdClock(Hour24, blank: !ClockEnabled);
        FramesSent++;
    }

    public void Stop()
    {
        _cts?.Cancel();
        try { _loop?.Wait(1500); } catch (AggregateException) { }
        _loop = null;
        _cts?.Dispose();
        _cts = null;
    }

    public void Dispose() => Stop();
}
