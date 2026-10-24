using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace VkSagittarius.Core;

/// <summary>单台设备的动作门面：帧构造 + 传输 + 状态回读。</summary>
public sealed class VkDevice : IDisposable
{
    private readonly IHidTransport _transport;

    public int Class => _transport.DeviceClass;
    public int Index => _transport.Index;
    public string Path => _transport.Path;
    public string Name => $"{Proto.ClassName(Class)} ({Path})";

    public VkDevice(IHidTransport transport) => _transport = transport;

    public void Send(byte[] report) => _transport.Write(report);

    /// <summary>
    /// 连接后把本机序号写给设备（cmd=1）。原程序在设备连上/断开时重排同类设备的
    /// index，设备侧靠这个序号区分多个同型号设备；单设备场景写 0 即可。
    /// </summary>
    public void Register() => Send(Proto.BuildReport(Class, Index, Proto.Cmd.State));

    public Proto.DeviceState? RequestState(int timeoutMs = 500, TelemetrySnapshot? tele = null)
    {
        var t = tele ?? TelemetrySnapshot.Empty;
        Send(Proto.BuildReport(Class, Index, Proto.Cmd.GetState, 0, 0,
            Proto.TelemetryPayload(Class, (int)t.CpuTemp, (int)t.GpuTemp)));
        var deadline = Environment.TickCount64 + timeoutMs;
        while (Environment.TickCount64 < deadline)
        {
            var data = _transport.Read(50);
            if (data is null) continue;
            if (Proto.ParseState(data) is { } st) return st;
        }
        return null;
    }

    // ---------------------------------------------------------- 心跳

    public void SendAudio(TelemetrySnapshot tele, int audio8, int audio12) =>
        Send(Proto.BuildReport(Class, Index, Proto.Cmd.Audio, 0, 0,
            Proto.TelemetryPayload(Class, (int)tele.CpuTemp, (int)tele.GpuTemp,
                audio8, tele.CpuUsage, tele.MemUsage, audio12)));

    // ---------------------------------------------------------- 灯效 / 风扇

    public void SetLed(int mode, (int R, int G, int B) rgb, int brightness = 100,
                       int speed = Proto.LedSpeedFast, int channel = 0) =>
        Send(Proto.BuildReport(Class, Index, Proto.Cmd.Led, channel, mode,
            Proto.LedPayload(brightness, speed, rgb)));

    public void SetFan(int channel, int mode, (int[] Pwm, int[] Temp)? custom = null) =>
        Send(Proto.BuildReport(Class, Index, Proto.Cmd.Fan, channel, mode, Proto.FanPayload(mode, custom)));

    // ---------------------------------------------------------- LCD

    public void LcdTemps(TelemetrySnapshot t)
    {
        var payload = string.Join(",",
            "",                              // 主板温度：Linux 无对应传感器
            t.CpuTemp.ToString("0.0", CultureInfo.InvariantCulture),
            t.GpuTemp.ToString("0.0", CultureInfo.InvariantCulture),
            "",                              // 内存温度
            t.CpuFanRpm.ToString(CultureInfo.InvariantCulture),
            t.GpuFanRpm.ToString(CultureInfo.InvariantCulture));
        Send(Proto.BuildReport(Class, Index, Proto.Cmd.Lcd, 0, (int)Proto.LcdSub.Temp, Proto.LcdText(payload)));
    }

    public void LcdFreqs(TelemetrySnapshot t)
    {
        var payload = string.Join(",",
            t.CpuFreqMhz.ToString("0", CultureInfo.InvariantCulture),
            t.GpuFreqMhz.ToString("0", CultureInfo.InvariantCulture),
            t.MemFreqMhz.ToString("0", CultureInfo.InvariantCulture),
            t.CpuVoltage.ToString("0.000", CultureInfo.InvariantCulture),
            t.MemVoltage.ToString("0.000", CultureInfo.InvariantCulture));
        Send(Proto.BuildReport(Class, Index, Proto.Cmd.Lcd, 0, (int)Proto.LcdSub.Freq, Proto.LcdText(payload)));
    }

    public void LcdClock(bool hour24 = true, bool blank = false, DateTime? now = null)
    {
        string payload;
        if (blank)
        {
            payload = " , , ,";
        }
        else
        {
            var t = now ?? DateTime.Now;
            var hm = t.ToString(hour24 ? "HH\\:mm" : "hh\\:mm", CultureInfo.InvariantCulture);
            payload = $"{t:yyyy-MM-dd},{hm},{hm},";
        }
        Send(Proto.BuildReport(Class, Index, Proto.Cmd.Lcd, 0, (int)Proto.LcdSub.Clock, Proto.LcdText(payload)));
    }

    public void LcdConfig(IReadOnlyList<Proto.LcdSlot> slots) =>
        Send(Proto.BuildReport(Class, Index, Proto.Cmd.Lcd, 0, (int)Proto.LcdSub.Config, Proto.LcdConfig(slots)));

    public void LcdSimple(Proto.LcdSub sub) =>
        Send(Proto.BuildReport(Class, Index, Proto.Cmd.Lcd, 0, (int)sub));

    public void LcdName(string filename) =>
        Send(Proto.BuildReport(Class, Index, Proto.Cmd.Lcd, 0, (int)Proto.LcdSub.Name, Proto.LcdName(filename)));

    public void Dispose() => _transport.Dispose();
}

/// <summary>一次运行中打开的整套设备：按类别分发，共享遥测与音频电平。</summary>
public sealed class DeviceSet : IDisposable
{
    public IReadOnlyList<VkDevice> Devices { get; }
    public Dictionary<int, VkDevice> ByClass { get; } = new();
    public TelemetryService Telemetry { get; } = new();
    public AudioMeter Meter { get; } = new();
    public TelemetrySnapshot Snapshot { get; private set; } = TelemetrySnapshot.Empty;
    public bool AudioAvailable => Meter.Backend is not null;

    public DeviceSet(IEnumerable<IHidTransport> transports)
    {
        Devices = transports.Select(t => new VkDevice(t)).ToList();
        foreach (var d in Devices) ByClass.TryAdd(d.Class, d);
    }

    public VkDevice? Lcd => ByClass.GetValueOrDefault(3);
    public VkDevice? FanLed => ByClass.GetValueOrDefault(1) ?? ByClass.GetValueOrDefault(2);
    public bool Any => Devices.Count > 0;

    public void RegisterAll()
    {
        foreach (var d in Devices) d.Register();
    }

    public TelemetrySnapshot Refresh()
    {
        Snapshot = Telemetry.Sample();
        return Snapshot;
    }

    public void Dispose()
    {
        Meter.Dispose();
        foreach (var d in Devices) d.Dispose();
    }
}
