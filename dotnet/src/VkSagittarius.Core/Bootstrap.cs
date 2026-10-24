using System;
using System.Collections.Generic;

namespace VkSagittarius.Core;

public sealed record DeviceBootstrap(DeviceSet Set, bool Demo, IReadOnlyList<string> Errors);

/// <summary>
/// 打开设备的唯一入口（GUI 直连模式与 vksagd 共用）。
/// 失败原因逐条带出，绝不把 EACCES 伪装成"没有设备"。
/// </summary>
public static class DeviceFactory
{
    public static DeviceBootstrap Open(bool forceDemo, string? path = null)
    {
        if (forceDemo)
            return Demo("已指定 --demo");

        if (!string.IsNullOrEmpty(path))
        {
            var one = DeviceScanner.OpenPath(path);
            return one.Devices.Count > 0
                ? new DeviceBootstrap(new DeviceSet(one.Devices), false, one.Errors)
                : Demo(one.Errors);
        }

        var report = DeviceScanner.OpenAll();
        if (report.Devices.Count > 0)
            return new DeviceBootstrap(new DeviceSet(report.Devices), false, report.Errors);

        return Demo(report.Errors.Count > 0
            ? report.Errors
            : [$"未发现 VID 0x{Proto.Vid:X4} / PID 0x5201-0x5203 的 HID 设备"]);
    }

    private static DeviceBootstrap Demo(IReadOnlyList<string> errors) =>
        new(new DeviceSet([new FakeTransport(1), new FakeTransport(3)]), true, errors);

    public static DeviceBootstrap Demo(string reason) => Demo((IReadOnlyList<string>)[reason]);
}
