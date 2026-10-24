using System.Runtime.InteropServices;

namespace UsbHid.USB.Structures;

[StructLayout(LayoutKind.Sequential)]
public class DevBroadcastHdr
{
	internal int dbch_size;

	internal int dbch_devicetype;

	internal int dbch_reserved;
}
