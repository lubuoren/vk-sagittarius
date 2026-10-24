using System.Runtime.InteropServices;

namespace UsbHid.USB.Structures;

public struct PspDeviceInterfaceDetailData
{
	public int cbSize;

	[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
	public string DevicePath;
}
