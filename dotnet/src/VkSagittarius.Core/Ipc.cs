using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace VkSagittarius.Core;

// ---------------------------------------------------------------- 传输用的 DTO

public sealed record DeviceDto(int DeviceClass, string Path, int Index, string Name);

public sealed record TelemetryDto(
    double CpuTemp, double GpuTemp, int CpuUsage, int MemUsage,
    double CpuFreqMhz, double GpuFreqMhz, double MemFreqMhz,
    double CpuVoltage, int CpuFanRpm, int GpuFanRpm)
{
    public static TelemetryDto From(TelemetrySnapshot t) => new(
        t.CpuTemp, t.GpuTemp, t.CpuUsage, t.MemUsage, t.CpuFreqMhz, t.GpuFreqMhz,
        t.MemFreqMhz, t.CpuVoltage, t.CpuFanRpm, t.GpuFanRpm);
}

public sealed record FanDto(int Channel, int Mode, int Rpm);

public sealed record LedDto(int Mode, int Brightness, int Speed, int R, int G, int B);

public sealed record StateDto(FanDto[] Fans, LedDto? Led);

public sealed record StatusDto(
    DeviceDto[] Devices, bool Demo, string[] Errors, bool MonitorRunning,
    long Frames, long StateUpdates, TelemetryDto Telemetry, StateDto? State,
    string? AudioBackend, string Version);

public sealed record LedRequest(int Mode, int Channel, int Brightness, int Speed, int R, int G, int B);

public sealed record FanRequest(int Channel, int Mode, int[]? Pwm, int[]? Temp);

/// <summary>一条请求。op 决定用哪个字段，其余留空。</summary>
public sealed class IpcRequest
{
    public int Id { get; set; }
    public string Op { get; set; } = "";

    public LedRequest? Led { get; set; }
    public FanRequest? Fan { get; set; }

    /// <summary>LCD 子命令号（0/32/64/96/98..104）。</summary>
    public int Sub { get; set; }
    public bool Hour24 { get; set; } = true;
    public bool Blank { get; set; }

    /// <summary>上传背景时的源文件路径。</summary>
    public string? FilePath { get; set; }
}

public sealed class IpcResponse
{
    public int Id { get; set; }
    public bool Ok { get; set; }
    public string? Error { get; set; }
    public StatusDto? Status { get; set; }
}

public sealed class IpcEvent
{
    public string Event { get; set; } = "";
    public StatusDto? Status { get; set; }
    public string? Message { get; set; }
    public string? Level { get; set; }
}

public static class Ipc
{
    public const string OpStatus = "status";
    public const string OpLed = "led";
    public const string OpFan = "fan";
    public const string OpLcd = "lcd";
    public const string OpClock = "clock";
    public const string OpUpload = "upload";
    public const string OpRescan = "rescan";
    public const string OpPause = "pause";
    public const string OpResume = "resume";
    public const string OpQuit = "quit";

    public static readonly JsonSerializerOptions Json = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
    };

    /// <summary>XDG_RUNTIME_DIR 下的 Unix socket；没有就退到 /tmp。</summary>
    public static string SocketPath()
    {
        var overridePath = Environment.GetEnvironmentVariable("VKSAG_SOCKET");
        if (!string.IsNullOrEmpty(overridePath))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(overridePath)!);
            return overridePath;
        }

        var runtime = Environment.GetEnvironmentVariable("XDG_RUNTIME_DIR");
        var dir = string.IsNullOrEmpty(runtime)
            ? Path.Combine(Path.GetTempPath(), $"vk-sagittarius-{Environment.UserName}")
            : Path.Combine(runtime, "vk-sagittarius");
        Directory.CreateDirectory(dir);
        return Path.Combine(dir, "daemon.sock");
    }

    public static string ConfigDir => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), ".config", "vk-sagittarius");

    public static string LogFile => Path.Combine(ConfigDir, "daemon.log");
}
