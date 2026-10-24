using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;

namespace VkSagittarius.Core;

public sealed record TelemetrySnapshot(
    double CpuTemp, double GpuTemp, int CpuUsage, int MemUsage,
    double CpuFreqMhz, double GpuFreqMhz, double MemFreqMhz,
    double CpuVoltage, double MemVoltage, int CpuFanRpm, int GpuFanRpm)
{
    public static readonly TelemetrySnapshot Empty = new(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
}

/// <summary>
/// Linux 传感器采集，替代原程序的 WMI + cpuidsdk.dll。
/// 温度/风扇读 hwmon，占用读 /proc/stat，内存读 /proc/meminfo，
/// GPU 走 nvidia-smi 或 amdgpu hwmon。取不到的项返回 0。
/// </summary>
public sealed class TelemetryService
{
    private static readonly string[] CpuHwmon = ["k10temp", "zenpower", "coretemp", "cpu_thermal", "soc_thermal"];
    private static readonly string[] GpuHwmon = ["amdgpu", "nouveau", "radeon"];
    private static readonly string[] PreferLabels = ["Tctl", "Tdie", "Package id 0", "Composite", "edge"];

    private long _lastTotal, _lastIdle;
    private int _cpuUsage;

    public TelemetrySnapshot Sample()
    {
        UpdateCpuUsage();
        var (gpuTemp, gpuFreq, gpuFan) = ReadGpu();
        return new TelemetrySnapshot(
            CpuTemp: ReadCpuTemp(),
            GpuTemp: gpuTemp,
            CpuUsage: _cpuUsage,
            MemUsage: ReadMemUsage(),
            CpuFreqMhz: ReadCpuFreq(),
            GpuFreqMhz: gpuFreq,
            MemFreqMhz: ReadMemFreq(),
            CpuVoltage: ReadCpuVoltage(),
            MemVoltage: 0,
            CpuFanRpm: ReadFanRpm(),
            GpuFanRpm: gpuFan);
    }

    // ---------------------------------------------------------- hwmon

    private static IEnumerable<(string Dir, string Name)> Hwmons()
    {
        const string root = "/sys/class/hwmon";
        if (!Directory.Exists(root)) yield break;
        foreach (var dir in Directory.GetDirectories(root).OrderBy(x => x, StringComparer.Ordinal))
            yield return (dir, ReadText(Path.Combine(dir, "name")));
    }

    private static string ReadText(string path)
    {
        try { return File.ReadAllText(path).Trim(); }
        catch (IOException) { return ""; }
        catch (UnauthorizedAccessException) { return ""; }
    }

    private static double ReadCpuTemp()
    {
        foreach (var (dir, name) in Hwmons())
        {
            if (!CpuHwmon.Any(p => name.StartsWith(p, StringComparison.Ordinal))) continue;

            var labels = new Dictionary<string, string>(StringComparer.Ordinal);
            foreach (var f in Directory.GetFiles(dir, "temp*_label"))
                labels[Path.GetFileName(f).Replace("_label", "")] = ReadText(f);

            foreach (var pick in PreferLabels)
            {
                foreach (var (stem, label) in labels)
                {
                    if (label != pick) continue;
                    if (double.TryParse(ReadText(Path.Combine(dir, stem + "_input")),
                            NumberStyles.Float, CultureInfo.InvariantCulture, out var milli))
                        return milli / 1000.0;
                }
            }
            foreach (var f in Directory.GetFiles(dir, "temp*_input").OrderBy(x => x, StringComparer.Ordinal))
            {
                if (double.TryParse(ReadText(f), NumberStyles.Float, CultureInfo.InvariantCulture, out var milli))
                    return milli / 1000.0;
            }
        }
        return 0;
    }

    private static int ReadFanRpm()
    {
        foreach (var (dir, _) in Hwmons())
        {
            foreach (var f in Directory.GetFiles(dir, "fan*_input").OrderBy(x => x, StringComparer.Ordinal))
            {
                if (int.TryParse(ReadText(f), out var rpm) && rpm > 0) return rpm;
            }
        }
        return 0;
    }

    private static double ReadCpuVoltage()
    {
        foreach (var (dir, name) in Hwmons())
        {
            if (!CpuHwmon.Any(p => name.StartsWith(p, StringComparison.Ordinal))) continue;
            if (double.TryParse(ReadText(Path.Combine(dir, "in0_input")),
                    NumberStyles.Float, CultureInfo.InvariantCulture, out var milli) && milli > 0)
                return milli / 1000.0;
        }
        return 0;
    }

    private static double ReadCpuFreq()
    {
        var f = Directory.Exists("/sys/devices/system/cpu")
            ? Directory.GetDirectories("/sys/devices/system/cpu", "cpu[0-9]*")
                .Select(c => Path.Combine(c, "cpufreq/scaling_cur_freq"))
                .FirstOrDefault(File.Exists)
            : null;
        if (f is not null && double.TryParse(ReadText(f), out var khz) && khz > 0)
            return khz / 1000.0;

        var mhz = new List<double>();
        foreach (var line in ReadText("/proc/cpuinfo").Split('\n'))
        {
            if (!line.StartsWith("cpu MHz", StringComparison.OrdinalIgnoreCase)) continue;
            var idx = line.IndexOf(':');
            if (idx > 0 && double.TryParse(line[(idx + 1)..].Trim(),
                    NumberStyles.Float, CultureInfo.InvariantCulture, out var v))
                mhz.Add(v);
        }
        return mhz.Count > 0 ? mhz.Average() : 0;
    }

    private static double ReadMemFreq()
    {
        // EDAC 只有在内存支持时才有 ddr_speed_mts；否则 0。
        const string root = "/sys/devices/system/edac";
        if (!Directory.Exists(root)) return 0;
        foreach (var mc in Directory.GetDirectories(root, "mc*"))
        {
            foreach (var dimm in Directory.GetDirectories(mc, "dimm*"))
            {
                if (double.TryParse(ReadText(Path.Combine(dimm, "ddr_speed_mts")),
                        NumberStyles.Float, CultureInfo.InvariantCulture, out var mts) && mts > 0)
                    return mts;
            }
        }
        return 0;
    }

    private void UpdateCpuUsage()
    {
        var line = ReadText("/proc/stat").Split('\n').FirstOrDefault();
        if (line is null) return;
        var nums = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1)
            .Select(x => long.TryParse(x, out var v) ? v : 0).ToArray();
        if (nums.Length < 4) return;
        var idle = nums[3] + (nums.Length > 4 ? nums[4] : 0);
        var total = nums.Sum();
        if (_lastTotal > 0)
        {
            var dTotal = total - _lastTotal;
            var dIdle = idle - _lastIdle;
            if (dTotal > 0) _cpuUsage = (int)Math.Clamp(Math.Round(100.0 * (dTotal - dIdle) / dTotal), 0, 100);
        }
        _lastTotal = total;
        _lastIdle = idle;
    }

    private static int ReadMemUsage()
    {
        long total = 0, avail = 0;
        foreach (var line in ReadText("/proc/meminfo").Split('\n'))
        {
            if (line.StartsWith("MemTotal:", StringComparison.Ordinal))
                total = ParseKb(line);
            else if (line.StartsWith("MemAvailable:", StringComparison.Ordinal))
                avail = ParseKb(line);
        }
        if (total <= 0) return 0;
        return (int)Math.Clamp(Math.Round(100.0 * (total - avail) / total), 0, 100);
    }

    private static long ParseKb(string line)
    {
        var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return parts.Length > 1 && long.TryParse(parts[1], out var kb) ? kb * 1024 : 0;
    }

    // ---------------------------------------------------------- GPU

    private static (double Temp, double Freq, int Fan) ReadGpu()
    {
        var nv = ReadNvidia();
        if (nv is { } v) return v;

        foreach (var (dir, name) in Hwmons())
        {
            if (!GpuHwmon.Contains(name)) continue;
            var temp = double.TryParse(ReadText(Path.Combine(dir, "temp1_input")),
                NumberStyles.Float, CultureInfo.InvariantCulture, out var t) ? t / 1000.0 : 0;
            var fan = int.TryParse(ReadText(Path.Combine(dir, "fan1_input")), out var f) ? f : 0;
            var freq = 0.0;
            var sclk = ReadText(Path.Combine(dir, "device", "pp_dpm_sclk"));
            foreach (var line in sclk.Split('\n'))
            {
                if (!line.TrimEnd().EndsWith('*')) continue;
                var colon = line.IndexOf(':');
                if (colon > 0 && double.TryParse(line[(colon + 1)..].Replace("Mhz", "").Trim(),
                        NumberStyles.Float, CultureInfo.InvariantCulture, out var mhz))
                    freq = mhz;
            }
            return (temp, freq, fan);
        }
        return (0, 0, 0);
    }

    private static (double, double, int)? ReadNvidia()
    {
        try
        {
            var psi = new ProcessStartInfo("nvidia-smi",
                "--query-gpu=temperature.gpu,utilization.gpu,clocks.current.graphics,fan.speed " +
                "--format=csv,noheader,nounits")
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
            };
            using var proc = Process.Start(psi);
            if (proc is null) return null;
            var output = proc.StandardOutput.ReadToEnd();
            if (!proc.WaitForExit(2000)) { try { proc.Kill(); } catch { } return null; }
            var line = output.Split('\n', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
            if (line is null) return null;
            var f = line.Split(',').Select(x => x.Trim()).ToArray();
            if (f.Length < 4) return null;
            var temp = double.Parse(f[0], CultureInfo.InvariantCulture);
            var freq = double.Parse(f[2], CultureInfo.InvariantCulture);
            var fan = f[3] is "[N/A]" or "" ? 0 : (int)double.Parse(f[3], CultureInfo.InvariantCulture);
            return (temp, freq, fan);
        }
        catch (Exception)
        {
            return null;
        }
    }
}
