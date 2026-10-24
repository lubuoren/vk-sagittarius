using System;
using System.Linq;
using System.Threading;
using VkSagittarius.Core;
using Xunit;

namespace VkSagittarius.Tests;

public class ProtocolTests
{
    // 期望值直接照抄反编译结果：UserDevice.DoUsbHidEvent / sendDeviceDataN
    // 65 字节 = ReportId(00) + 14 字节头 + 50 字节零填充
    private const string StateFrame =
        "00 DC DC 01 00 01 00 00 00 00 00 00 01 00 00 " +
        "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 " +
        "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 " +
        "00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 " +
        "00 00";

    [Fact]
    public void StateFrame_MatchesDecompiledLayout()
    {
        var report = Proto.BuildReport(1, 0, Proto.Cmd.State);
        Assert.Equal(65, report.Length);
        Assert.Equal(StateFrame, Proto.Hex(report));
        Assert.Equal(65, StateFrame.Split(' ').Length);
    }

    [Fact]
    public void LedFrame_MatchesDecompiledLayout()
    {
        var report = Proto.BuildReport(1, 2, Proto.Cmd.Led, 0, 1,
            Proto.LedPayload(100, Proto.LedSpeedFast, (255, 0, 0)));
        var hex = Proto.Hex(report).Split(' ');
        // hex[0] 是 ReportId；hex[1..14] 是 14 字节头；负载从 hex[15] 起
        Assert.Equal(["00", "DC", "DC", "01", "00", "01", "00", "00", "00", "00", "00", "02", "10", "00", "01"],
            hex[..15]);
        // 负载：亮度, 速度, R, G, B, 0
        Assert.Equal(["64", "01", "FF", "00", "00", "00"], hex[15..21]);
        Assert.Equal(65, report.Length);
    }

    [Fact]
    public void FanPayload_PresetAndCustomOrdering()
    {
        Assert.Equal([20, 20, 60, 100, 40, 60, 75], Proto.FanPayload(2));
        Assert.Equal([97, 98, 99, 100, 30, 70, 75], Proto.FanPayload(4));
        // 自定义：0, temp1..3, pwm1..3（与原程序数组顺序一致）
        Assert.Equal([0, 45, 65, 85, 20, 55, 100],
            Proto.FanPayload(Proto.FanModeCustom, ([20, 55, 100], [45, 65, 85])));
    }

    [Fact]
    public void TelemetryPayload_Class1VsClass2Layout()
    {
        var a = Proto.TelemetryPayload(1, cpuTemp: 55, gpuTemp: 66, audio8: 200);
        Assert.Equal(46, a.Length);
        Assert.Equal(55, a[42]);
        Assert.Equal(66, a[43]);
        Assert.Equal(200, a[44]);

        var b = Proto.TelemetryPayload(2, cpuTemp: 55, gpuTemp: 66, cpuUsage: 30, memUsage: 40, audio12: 0x1234);
        Assert.Equal(30, b[40]);
        Assert.Equal(40, b[41]);
        Assert.Equal(55, b[42]);
        Assert.Equal(66, b[43]);
        Assert.Equal(0x34, b[44]);
        Assert.Equal(0x12, b[45]);
    }

    [Fact]
    public void LedAutoModeUsesSpecialPayload()
    {
        // 原程序 SendLEDData1：mode 固定 1，负载 {0,速度,0,0,0,0}
        var (mode, payload) = Proto.LedAutoPayload(Proto.LedSpeedMid);
        Assert.Equal(1, mode);
        Assert.Equal([0, Proto.LedSpeedMid, 0, 0, 0, 0], payload);
    }

    [Fact]
    public void LcdSlotPacksMetricGroups()
    {
        var slot = new Proto.LcdSlot(Clock: false, Layout: 3, Hour24: 24,
            M1: 1, M2: 0, M3: 1, M4: 1, M5: 1, M6: 1);
        Assert.Equal([0x00, 0x03, 0x18, 0x10, 0x11, 0x11, 0x00, 0x00, 0x00, 0x00], slot.Pack());
    }

    [Fact]
    public void LcdConfigPadsWithPreviousSlotHeader()
    {
        var one = new Proto.LcdSlot(Layout: 2, Hour24: 12, M1: 1, M2: 2, M3: 1, M4: 1, ExtLo: 5, ExtHi: 6);
        var cfg = Proto.LcdConfig([one]);
        Assert.Equal(50, cfg.Length);
        Assert.Equal(one.Pack(), cfg[..10]);
        // 后续组复制首组前 6 字节，其余 4 字节归零
        Assert.Equal(one.Pack()[..6], cfg[10..16]);
        Assert.All(cfg[16..20], b => Assert.Equal(0, b));
    }

    [Fact]
    public void LcdNameAppendsNul()
    {
        Assert.Equal("abc.png\0"u8.ToArray(), Proto.LcdName("abc.png"));
        Assert.Throws<ArgumentException>(() => Proto.LcdName(new string('x', 60)));
    }

    /// <summary>按原程序布局造一份状态回应：帧头在 frameStart，cmd 在 frameStart+12。</summary>
    private static byte[] BuildStateReply(int frameStart, int cls = 1)
    {
        var buf = new byte[frameStart + 64];
        buf[frameStart] = 0xDC;
        buf[frameStart + 1] = 0xDC;
        buf[frameStart + 2] = 0x01;
        buf[frameStart + 4] = (byte)cls;
        buf[frameStart + 12] = 2;                                   // GetState
        buf[frameStart + 15] = 2;                                   // 风扇1 模式
        buf[frameStart + 16] = 0x20; buf[frameStart + 17] = 0x03;   // 800 RPM
        buf[frameStart + 18] = 1;                                   // 风扇2 模式
        buf[frameStart + 19] = 0x2C; buf[frameStart + 20] = 0x01;   // 300 RPM
        buf[frameStart + 27] = 6;                                   // 灯效模式
        buf[frameStart + 28] = 100;
        buf[frameStart + 29] = 1;
        buf[frameStart + 30] = 0xFF; buf[frameStart + 31] = 0x40; buf[frameStart + 32] = 0x00;
        return buf;
    }

    [Theory]
    [InlineData(false)]  // 无 ReportId，帧头在 0
    [InlineData(true)]   // 带 ReportId，帧头在 1
    public void ParseState_WithAndWithoutReportId(bool withReportId)
    {
        var frameStart = withReportId ? 1 : 0;
        var buf = BuildStateReply(frameStart);

        var st = Proto.ParseState(buf);
        Assert.NotNull(st);
        Assert.Equal(2, st!.Fans[1].Mode);
        Assert.Equal(800, st.Fans[1].Rpm);
        Assert.Equal(1, st.Fans[2].Mode);
        Assert.Equal(300, st.Fans[2].Rpm);
        Assert.Equal(6, st.Led!.Value.Mode);
        Assert.Equal(100, st.Led.Value.Brightness);
        Assert.Equal((255, 64, 0), (st.Led.Value.R, st.Led.Value.G, st.Led.Value.B));
    }

    [Fact]
    public void ParseState_RejectsOtherCommands()
    {
        var buf = new byte[65];
        buf[1] = 0xDC; buf[2] = 0xDC; buf[12] = 4;
        Assert.Null(Proto.ParseState(buf));
    }
}

public class DeviceScannerTests
{
    [Fact]
    public void PidMappingMatchesOriginal()
    {
        Assert.Equal(1, Proto.ClassForPid(0x5201));
        Assert.Equal(2, Proto.ClassForPid(0x5202));
        Assert.Equal(3, Proto.ClassForPid(0x5203));
        Assert.Equal(0, Proto.ClassForPid(0x1234));
        Assert.Equal(0x5203, Proto.PidForClass(3));
    }

    [Fact]
    public void ScanDoesNotThrowWithoutKernelSupport()
    {
        var list = DeviceScanner.Scan();
        Assert.NotNull(list);
    }
}

public class MonitorEngineTests
{
    [Fact]
    public async Task LoopSendsAudioAndStateFramesAndLcdCycle()
    {
        var fan = new FakeTransport(1, 0);
        var lcd = new FakeTransport(3, 0);
        using var set = new DeviceSet([fan, lcd]);
        using var engine = new MonitorEngine(set);

        using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(700));
        await engine.RunAsync(cts.Token);

        var audioFrames = fan.Sent.Where(f => f.Length > 12 && f[12] == (byte)Proto.Cmd.Audio).ToList();
        var stateFrames = fan.Sent.Where(f => f.Length > 12 && f[12] == (byte)Proto.Cmd.GetState).ToList();
        Assert.True(audioFrames.Count >= 4, $"音频帧过少：{audioFrames.Count}");
        Assert.True(stateFrames.Count >= 1, "没有发出状态回读");
        Assert.Equal(65, audioFrames[0].Length);

        var lcdSubs = lcd.Sent.Where(f => f.Length > 14 && f[12] == (byte)Proto.Cmd.Lcd).Select(f => f[14]).ToList();
        Assert.Contains((byte)Proto.LcdSub.Temp, lcdSubs);
        Assert.Contains((byte)Proto.LcdSub.Freq, lcdSubs);
        Assert.Contains((byte)Proto.LcdSub.Clock, lcdSubs);
    }

    [Fact]
    public void LcdCycleEmitsReadableText()
    {
        var lcd = new FakeTransport(3, 0);
        using var set = new DeviceSet([lcd]);
        using var engine = new MonitorEngine(set);
        var tele = new TelemetrySnapshot(55.4, 62.1, 30, 45, 4200, 1800, 3200, 1.35, 0, 1100, 900);
        engine.SendLcdCycle(set.Lcd!, tele);

        var temps = lcd.Sent.First(f => f[14] == (byte)Proto.LcdSub.Temp);
        var text = System.Text.Encoding.ASCII.GetString(temps[15..].TakeWhile(b => b != 0).ToArray());
        Assert.Equal(",55.4,62.1,,1100,900", text);

        var freqs = lcd.Sent.First(f => f[14] == (byte)Proto.LcdSub.Freq);
        var ftext = System.Text.Encoding.ASCII.GetString(freqs[15..].TakeWhile(b => b != 0).ToArray());
        Assert.Equal("4200,1800,3200,1.350,0.000", ftext);
    }
}
