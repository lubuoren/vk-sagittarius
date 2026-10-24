using System;

namespace UsbHid.USB.Structures;

public struct SpDeviceInterfaceData
{
	public int cbSize;

	public Guid InterfaceClassGuid;

	public int Flags;

	public IntPtr Reserved;
}
