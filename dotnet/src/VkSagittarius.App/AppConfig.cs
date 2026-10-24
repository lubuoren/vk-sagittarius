using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace VkSagittarius.App;

/// <summary>
/// 本地配置：界面缩放 + 灯效 + 风扇（每通道模式与自定义曲线点）。
/// 路径 ~/.config/vk-sagittarius/config.json。
///
/// 原程序的 config.d 只有常量定义、从未真正写过盘，所以风扇自定义曲线在重启后
/// 会丢；这里补上持久化——设备本身只存"模式号"，曲线点只在宿主侧。
/// </summary>
public sealed class AppConfig
{
    public double Scale { get; set; }
    public LedSettings Led { get; set; } = new();
    public FanSettings Fan { get; set; } = new();

    public sealed class LedSettings
    {
        public int Mode { get; set; } = 1;
        public int Channel { get; set; }
        public int Brightness { get; set; } = 100;
        public int Speed { get; set; } = 1;        // 设备侧 1/2/4
        public int R { get; set; } = 255;
        public int G { get; set; } = 40;
        public int B { get; set; } = 40;
    }

    public sealed class FanSettings
    {
        /// <summary>下标即通道号 1..4，对齐原程序 myALLMode。</summary>
        public int[] Modes { get; set; } = [0, 2, 2, 2, 2];
        public int[] Pwm { get; set; } = [20, 55, 100];
        public int[] Temp { get; set; } = [45, 65, 85];
    }

    private const double MinScale = 0.75, MaxScale = 3.0;

    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.Never,
    };

    public static string FilePath => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        ".config", "vk-sagittarius", "config.json");

    public static AppConfig Load()
    {
        try
        {
            if (File.Exists(FilePath))
            {
                var cfg = JsonSerializer.Deserialize<AppConfig>(File.ReadAllText(FilePath), Options);
                if (cfg is not null)
                {
                    cfg.Normalise();
                    return cfg;
                }
            }
        }
        catch (Exception)
        {
            // 配置损坏就当没有，继续用默认值
        }
        return new AppConfig { Scale = AutoDetectScale() };
    }

    public void Save()
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
            Normalise();
            File.WriteAllText(FilePath, JsonSerializer.Serialize(this, Options));
        }
        catch (Exception)
        {
            // 写不进去不影响使用
        }
    }

    private void Normalise()
    {
        if (Scale < MinScale || Scale > MaxScale) Scale = 1.0;

        Fan ??= new FanSettings();
        if (Fan.Modes is null || Fan.Modes.Length < 5) Fan.Modes = [0, 2, 2, 2, 2];
        for (var ch = 1; ch < 5; ch++)
            if (Fan.Modes[ch] is < 1 or > 5) Fan.Modes[ch] = 2;

        Fan.Pwm = Fix(Fan.Pwm, [20, 55, 100], 0, 100);
        Fan.Temp = Fix(Fan.Temp, [45, 65, 85], 0, 120);

        Led ??= new LedSettings();
        Led.Brightness = Math.Clamp(Led.Brightness, 0, 100);
        Led.Speed = Led.Speed is 1 or 2 or 4 ? Led.Speed : 1;
        Led.Channel = Math.Clamp(Led.Channel, 0, 5);
        Led.R = Math.Clamp(Led.R, 0, 255);
        Led.G = Math.Clamp(Led.G, 0, 255);
        Led.B = Math.Clamp(Led.B, 0, 255);
    }

    private static int[] Fix(int[]? actual, int[] fallback, int min, int max)
    {
        if (actual is null || actual.Length != fallback.Length) return fallback;
        var result = new int[actual.Length];
        for (var i = 0; i < actual.Length; i++)
            result[i] = Math.Clamp(actual[i], min, max);
        return result;
    }

    // ---------------------------------------------------------- 缩放自动探测

    public static double AutoDetectScale() => ScaleDetector.Detect();

    public static double? ParseScale(string? raw)
    {
        if (string.IsNullOrWhiteSpace(raw)) return null;
        if (!double.TryParse(raw, NumberStyles.Float, CultureInfo.InvariantCulture, out var v)) return null;
        if (v < MinScale || v > MaxScale) return null;
        return Math.Round(v, 3);
    }
}

/// <summary>
/// 缩放探测：Hyprland 的 fractional scale 只有 hyprctl 拿得到；其次 Xft.dpi/96；最后 1.0。
/// </summary>
internal static class ScaleDetector
{
    private const double Min = 0.75, Max = 3.0;

    public static double Detect()
    {
        if (FromHyprland() is { } h && h is >= Min and <= Max) return Math.Round(h, 3);
        if (FromXft() is { } d && d > 0)
        {
            var s = Math.Round(d / 96.0, 3);
            if (s is >= Min and <= Max) return s;
        }
        return 1.0;
    }

    private static double? FromHyprland()
    {
        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("HYPRLAND_INSTANCE_SIGNATURE")))
            return null;
        var json = Run("hyprctl", "monitors -j");
        if (string.IsNullOrEmpty(json)) return null;
        try
        {
            using var doc = JsonDocument.Parse(json);
            foreach (var m in doc.RootElement.EnumerateArray())
            {
                if (m.TryGetProperty("focused", out var f) && f.ValueKind == JsonValueKind.True &&
                    m.TryGetProperty("scale", out var sc) && sc.TryGetDouble(out var v) && v > 0)
                    return v;
            }
            foreach (var m in doc.RootElement.EnumerateArray())
                if (m.TryGetProperty("scale", out var sc) && sc.TryGetDouble(out var v) && v > 0)
                    return v;
        }
        catch (JsonException)
        {
            // 结构变了就放弃
        }
        return null;
    }

    private static double? FromXft()
    {
        var env = Environment.GetEnvironmentVariable("GDK_DPI_SCALE");
        if (double.TryParse(env, NumberStyles.Float, CultureInfo.InvariantCulture, out var gdk) && gdk > 0)
            return 96 * gdk;

        var output = Run("xrdb", "-query");
        foreach (var line in output.Split('\n'))
        {
            if (!line.Contains("Xft.dpi:", StringComparison.OrdinalIgnoreCase)) continue;
            var idx = line.IndexOf(':');
            if (idx >= 0 && double.TryParse(line[(idx + 1)..].Trim(),
                    NumberStyles.Float, CultureInfo.InvariantCulture, out var dpi))
                return dpi;
        }
        return null;
    }

    private static string Run(string file, string args)
    {
        try
        {
            var psi = new ProcessStartInfo(file, args)
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
            };
            using var p = Process.Start(psi);
            if (p is null) return "";
            var output = p.StandardOutput.ReadToEnd();
            if (!p.WaitForExit(800)) { try { p.Kill(); } catch (Exception) { } return ""; }
            return output;
        }
        catch (Exception)
        {
            return "";
        }
    }
}
