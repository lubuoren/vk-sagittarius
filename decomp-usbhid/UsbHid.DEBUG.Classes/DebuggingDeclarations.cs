using System.Runtime.InteropServices;

namespace UsbHid.DEBUG.Classes;

internal sealed class DebuggingDeclarations
{
	public const short FORMAT_MESSAGE_FROM_SYSTEM = 4096;

	[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
	public static extern int FormatMessage(int dwFlags, ref long lpSource, int dwMessageId, int dwLanguageZId, string lpBuffer, int nSize, int Arguments);
}
