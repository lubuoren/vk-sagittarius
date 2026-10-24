using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VkSagittarius.Core;

/// <summary>
/// VK 射手座控制中心的 HID 线协议。
/// 全部字节布局来自对 VAIKYRIE.exe / UsbHid.dll 的反编译结果。
/// </summary>
public static class Proto
{
    public const byte Head = 0xDC;
    public const ushort Brand = 1;
    public const int HeaderLen = 14;
    public const int DataLen = 64;           // 不含 ReportId 的数据长度
    public const int MaxPayload = DataLen - HeaderLen; // 50
    public const byte ReportId = 0;

    public const ushort Vid = 0x0416;

    public enum Cmd : byte
    {
        State = 1,      // 设备序号登记
        GetState = 2,   // 状态回读（顺带携带遥测）
        Audio = 4,      // 音频电平 + 遥测，10Hz
        Motor = 5,      // 保留，原程序未使用
        Fan = 6,        // 风扇曲线
        Led = 16,       // 灯效
        Lcd = 48,       // LCD（mode 为子命令）
    }

    public enum LcdSub : byte
    {
        Temp = 0,
        Freq = 32,
        Clock = 64,
        Config = 96,
        Name = 97,
        Apply = 98,
        Rot0 = 99,
        Rot90 = 100,
        Rot180 = 101,
        Rot270 = 102,
        White = 103,
        Black = 104,
        ScreenOff = 105,
        ScreenOn = 106,
    }

    public static int ClassForPid(ushort pid) => pid switch
    {
        0x5201 => 1,
        0x5202 => 2,
        0x5203 => 3,
        _ => 0,
    };

    public static ushort PidForClass(int cls) => cls switch
    {
        1 => 0x5201,
        2 => 0x5202,
        3 => 0x5203,
        _ => throw new ArgumentOutOfRangeException(nameof(cls)),
    };

    public static string ClassName(int cls) => cls switch
    {
        1 => "风扇/灯效控制器",
        2 => "SYD 灯效控制器",
        3 => "LCD 屏幕",
        _ => "未知设备",
    };

    // ------------------------------------------------------------ 帧构造

    public static byte[] BuildFrame(int cls, int index, Cmd cmd, int ch = 0, int mode = 0,
                                    ReadOnlySpan<byte> payload = default)
    {
        if (cls is < 1 or > 3) throw new ArgumentOutOfRangeException(nameof(cls));
        if (payload.Length > MaxPayload)
            throw new ArgumentException($"负载最长 {MaxPayload} 字节，收到 {payload.Length}", nameof(payload));

        var buf = new byte[HeaderLen + payload.Length];
        buf[0] = Head;
        buf[1] = Head;
        buf[2] = (byte)(Brand & 0xFF);
        buf[3] = (byte)(Brand >> 8);
        buf[4] = (byte)cls;
        buf[10] = (byte)index;
        buf[11] = (byte)cmd;
        buf[12] = (byte)ch;
        buf[13] = (byte)mode;
        payload.CopyTo(buf.AsSpan(HeaderLen));
        return buf;
    }

    /// <summary>写往 hidraw 的完整报文：ReportId 0 + 64 字节数据。</summary>
    public static byte[] BuildReport(int cls, int index, Cmd cmd, int ch = 0, int mode = 0,
                                     ReadOnlySpan<byte> payload = default)
    {
        var frame = BuildFrame(cls, index, cmd, ch, mode, payload);
        var report = new byte[1 + DataLen];
        report[0] = ReportId;
        frame.CopyTo(report, 1);
        return report;
    }

    // ------------------------------------------------------------ 遥测负载

    /// <summary>
    /// 原程序 sendDeviceData1/2/3 在 data==null 时构造的 46 字节负载。
    /// 类别 1：[42]=CPU 温度 [43]=GPU 温度 [44]=音频(0..255)
    /// 类别 2/3：[40]=CPU 占用 [41]=内存占用 [42]=CPU 温度 [43]=GPU 温度 [44..45]=音频(0..4095 LE)
    /// </summary>
    public static byte[] TelemetryPayload(int cls, int cpuTemp = 0, int gpuTemp = 0,
                                          int audio8 = 0, int cpuUsage = 0, int memUsage = 0,
                                          int audio12 = 0)
    {
        var buf = new byte[46];
        if (cls == 1)
        {
            buf[42] = (byte)cpuTemp;
            buf[43] = (byte)gpuTemp;
            buf[44] = (byte)audio8;
        }
        else
        {
            buf[40] = (byte)cpuUsage;
            buf[41] = (byte)memUsage;
            buf[42] = (byte)cpuTemp;
            buf[43] = (byte)gpuTemp;
            buf[44] = (byte)(audio12 & 0xFF);
            buf[45] = (byte)(audio12 >> 8);
        }
        return buf;
    }

    // ------------------------------------------------------------ 灯效

    public const int LedSpeedSlow = 4, LedSpeedMid = 2, LedSpeedFast = 1;

    /// <summary>原程序 UserDeviceLED 的按钮模式号（中文含义为推断，以实测为准）。</summary>
    public static readonly IReadOnlyDictionary<int, string> LedModes = new Dictionary<int, string>
    {
        [1] = "静态单色（默认）",
        [2] = "呼吸模式",
        [3] = "单色循环",
        [4] = "单色闪烁",
        [5] = "星光闪烁",
        [6] = "彩虹模式",
        [7] = "全色频谱",
        [8] = "声显频谱",
        [9] = "自由键触",
        [10] = "全彩自动",
        [11] = "彩虹流水",
        [12] = "音乐模式",
        [100] = "外部输入",
    };

    public static byte[] LedPayload(int brightness, int speed, (int R, int G, int B) rgb) =>
    [
        (byte)Math.Clamp(brightness, 0, 255),
        (byte)Math.Clamp(speed, 0, 255),
        (byte)Math.Clamp(rgb.R, 0, 255),
        (byte)Math.Clamp(rgb.G, 0, 255),
        (byte)Math.Clamp(rgb.B, 0, 255),
        0,
    ];

    // ------------------------------------------------------------ 风扇

    public sealed record FanCurve(string Name, byte[] Pwm, byte[] Temp);

    /// <summary>预设曲线：4 个 PWM 点 + 3 个温度阈值（原程序 fanPWM/fanTemp 数组）。</summary>
    public static readonly IReadOnlyDictionary<int, FanCurve> FanCurves = new Dictionary<int, FanCurve>
    {
        [1] = new("静音", [0, 0, 65, 100], [30, 70, 75]),
        [2] = new("标准", [20, 20, 60, 100], [40, 60, 75]),
        [3] = new("性能", [30, 30, 99, 100], [25, 70, 75]),
        [4] = new("全速", [97, 98, 99, 100], [30, 70, 75]),
    };

    public const int FanModeCustom = 5;

    /// <summary>
    /// 预设模式字节序为「pwm1..pwm4, temp1..temp3」；
    /// 自定义模式为「0, temp1..temp3, pwm1..pwm3」——原程序两者顺序不同，照抄。
    /// </summary>
    public static byte[] FanPayload(int mode, (int[] Pwm, int[] Temp)? custom = null)
    {
        if (FanCurves.TryGetValue(mode, out var c))
            return [.. c.Pwm, .. c.Temp];
        if (mode == FanModeCustom)
        {
            if (custom is not { } cc || cc.Pwm.Length != 3 || cc.Temp.Length != 3)
                throw new ArgumentException("自定义曲线需要 3 个 PWM 点 + 3 个温度点");
            return [0, (byte)cc.Temp[0], (byte)cc.Temp[1], (byte)cc.Temp[2],
                    (byte)cc.Pwm[0], (byte)cc.Pwm[1], (byte)cc.Pwm[2]];
        }
        throw new ArgumentOutOfRangeException(nameof(mode), $"未知风扇模式 {mode}");
    }

    // ------------------------------------------------------------ LCD

    public static byte[] LcdText(string payload) => Encoding.ASCII.GetBytes(payload);

    /// <summary>模式 97 的负载：ASCII 文件名 + NUL（原程序限制 46 字节以内）。</summary>
    public static byte[] LcdName(string filename)
    {
        var raw = Encoding.ASCII.GetBytes(filename);
        if (raw.Length > MaxPayload - 2)
            throw new ArgumentException("文件名过长，设备只接受较短的名字", nameof(filename));
        var buf = new byte[raw.Length + 1];
        raw.CopyTo(buf, 0);
        return buf;
    }

    /// <summary>LCD 显示配置中的一组（原程序 10 字节）。</summary>
    public readonly record struct LcdSlot(
        bool Clock = false,
        int Layout = 3,
        int Hour24 = 24,
        int M1 = 0, int M2 = 0,
        int M3 = 0, int M4 = 0,
        int M5 = 0, int M6 = 0,
        int ExtLo = 0, int ExtHi = 0)
    {
        /// <summary>原程序每组占 10 字节，实际只写前 8 字节，后 2 字节留 0。</summary>
        public byte[] Pack() =>
        [
            (byte)(Clock ? 1 : 0),
            (byte)Layout,
            (byte)Hour24,
            (byte)((M1 << 4) | (M2 & 0xF)),
            (byte)((M3 << 4) | (M4 & 0xF)),
            (byte)((M5 << 4) | (M6 & 0xF)),
            (byte)ExtLo,
            (byte)ExtHi,
            0,
            0,
        ];
    }

    /// <summary>模式 96 的 50 字节负载：5 组 x 10 字节，缺的组复制上一组的头 6 字节、其余归零。</summary>
    public static byte[] LcdConfig(IReadOnlyList<LcdSlot> slots)
    {
        if (slots.Count == 0) throw new ArgumentException("至少需要一组显示配置", nameof(slots));
        var outBuf = new List<byte>(MaxPayload);
        foreach (var s in slots) outBuf.AddRange(s.Pack());
        while (outBuf.Count < MaxPayload)
        {
            var prev = outBuf.GetRange(outBuf.Count - 10, 10);
            outBuf.AddRange(prev.Take(6));
            outBuf.AddRange([0, 0, 0, 0]);
        }
        return outBuf.Take(MaxPayload).ToArray();
    }

    // ------------------------------------------------------------ 状态回读

    public readonly record struct FanState(int Mode, int Rpm);
    public readonly record struct LedState(int Mode, int Brightness, int Speed, int R, int G, int B);

    public sealed class DeviceState
    {
        public int Command { get; init; }
        public Dictionary<int, FanState> Fans { get; } = new();
        public LedState? Led { get; init; }
        public byte[] Raw { get; init; } = [];

        public string Describe()
        {
            var parts = Fans.OrderBy(k => k.Key)
                .Select(kv => $"风扇{kv.Key}: {kv.Value.Mode}档 {kv.Value.Rpm}RPM").ToList();
            if (Led is { } led)
                parts.Add($"灯效: 模式{led.Mode} 亮度{led.Brightness} 速度{led.Speed} " +
                          $"#{led.R:X2}{led.G:X2}{led.B:X2}");
            return parts.Count > 0 ? string.Join(" | ", parts) : "（无状态数据）";
        }
    }

    /// <summary>容忍输入报告是否带 ReportId，定位 0xDC 0xDC 帧头。</summary>
    private static int FindSync(ReadOnlySpan<byte> buf)
    {
        for (var i = 0; i < 3; i++)
            if (buf.Length > i + 1 && buf[i] == Head && buf[i + 1] == Head) return i;
        return 0;
    }

    /// <summary>
    /// 解析 GetState 回应。相对帧头偏移：
    /// [12]=2；[15]=风扇1模式 [16..17]=转速 LE，共 4 组每 3 字节；
    /// [27]=灯效模式 [28]=亮度 [29]=速度 [30..32]=RGB。
    /// </summary>
    public static DeviceState? ParseState(ReadOnlySpan<byte> buf)
    {
        if (buf.Length < 27) return null;
        var b = FindSync(buf);
        if (buf.Length < b + 33) return null;
        if (buf[b + 12] != (byte)Cmd.GetState) return null;

        var st = new DeviceState
        {
            Command = buf[b + 12],
            Raw = buf.ToArray(),
            Led = new LedState(buf[b + 27], buf[b + 28], buf[b + 29], buf[b + 30], buf[b + 31], buf[b + 32]),
        };
        for (var ch = 1; ch <= 4; ch++)
        {
            var off = b + 15 + (ch - 1) * 3;
            st.Fans[ch] = new FanState(buf[off], buf[off + 1] | (buf[off + 2] << 8));
        }
        return st;
    }

    public static string Hex(ReadOnlySpan<byte> data) =>
        string.Join(' ', data.ToArray().Select(x => x.ToString("X2")));
}
