using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text.Json;

namespace VkSagittarius.App;

/// <summary>
/// 界面缩放的三级来源：环境变量 VKSAG_SCALE（CLI --scale 也写这里）→ 配置文件 →
/// 自动探测（Xft.dpi）。写回 ~/.config/vk-sagittarius/config.json。
///
/// 背景：Hyprland 开着 xwayland:force_zero_scaling 时，fractional scale（例如 1.875）
/// 不会传给 X11 程序，Avalonia 按 1.0 绘制，于是 4K 屏上界面偏小、还会被合成器放大发虚。
/// </summary>
public static class ScaleConfig
{
    private const double Min = 0.75, Max = 3.0;

    private static string FilePath => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        ".config", "vk-sagittarius", "config.json");

    private sealed class Model
    {
        public double Scale { get; set; }
    }

    public static double Resolve()
    {
        var fromEnv = Parse(Environment.GetEnvironmentVariable("VKSAG_SCALE"));
        if (fromEnv is { } e) return e;

        var fromFile = Parse(ReadSaved());
        if (fromFile is { } f) return f;

        return AutoDetect();
    }

    public static void Save(double scale)
    {
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
            File.WriteAllText(FilePath,
                JsonSerializer.Serialize(new Model { Scale = scale }, new JsonSerializerOptions { WriteIndented = true }));
        }
        catch (Exception)
        {
            // 配置写不进去不该影响使用
        }
    }

    private static string? ReadSaved()
    {
        try
        {
            if (!File.Exists(FilePath)) return null;
            return JsonSerializer.Deserialize<Model>(File.ReadAllText(FilePath))?.Scale
                .ToString(CultureInfo.InvariantCulture);
        }
        catch (Exception)
        {
            return null;
        }
    }

    private static double? Parse(string? raw)
    {
        if (string.IsNullOrWhiteSpace(raw)) return null;
        if (!double.TryParse(raw, NumberStyles.Float, CultureInfo.InvariantCulture, out var v)) return null;
        if (v < Min || v > Max) return null;
        return Math.Round(v, 3);
    }

    /// <summary>Xft.dpi / 96；读不到就 1.0（大多数 X11 桌面把 dpi 设成 96×缩放）。</summary>
    public static double AutoDetect()
    {
        var dpi = ReadXftDpi() ?? ParseEnvDpi();
        if (dpi is { } d && d > 0)
        {
            var scale = Math.Round(d / 96.0, 3);
            if (scale is >= Min and <= Max) return scale;
        }
        return 1.0;
    }

    private static double? ParseEnvDpi()
    {
        var raw = Environment.GetEnvironmentVariable("GDK_DPI_SCALE");
        return double.TryParse(raw, NumberStyles.Float, CultureInfo.InvariantCulture, out var v) && v > 0 ? 96 * v : null;
    }

    private static double? ReadXftDpi()
    {
        try
        {
            var psi = new ProcessStartInfo("xrdb", "-query")
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
            };
            using var p = Process.Start(psi);
            if (p is null) return null;
            var output = p.StandardOutput.ReadToEnd();
            if (!p.WaitForExit(600)) { try { p.Kill(); } catch (Exception) { } return null; }
            foreach (var line in output.Split('\n'))
            {
                if (!line.Contains("Xft.dpi:", StringComparison.OrdinalIgnoreCase)) continue;
                var idx = line.IndexOf(':');
                if (idx < 0) continue;
                if (double.TryParse(line[(idx + 1)..].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out var dpi))
                    return dpi;
            }
        }
        catch (Exception)
        {
            // 没有 xrdb 或没有 DISPLAY：忽略
        }
        return null;
    }
}
