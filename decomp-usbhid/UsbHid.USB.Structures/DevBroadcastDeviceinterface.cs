using System;
using System.Runtime.InteropServices;

namespace UsbHid.USB.Structures;

[StructLayout(LayoutKind.Sequential)]
public class DevBroadcastDeviceinterface
{
	internal int dbcc_size;

	internal int dbcc_devicetype;

	internal int dbcc_reserved;

	internal Guid dbcc_classguid;

	internal short dbcc_name;
}
