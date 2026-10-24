using System.IO;

namespace UsbHid.USB.Structures;

public struct SyncObjT
{
	public FileStream Fs;

	public byte[] Buf;
}
