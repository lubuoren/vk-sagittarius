using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace UsbHid.USB.Classes.DllWrappers;

public static class Kernel32
{
	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern int CancelIo(SafeFileHandle hFile);

	[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	public static extern IntPtr CreateEvent(IntPtr securityAttributes, bool bManualReset, bool bInitialState, string lpName);

	[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	public static extern SafeFileHandle CreateFile(string lpFileName, uint dwDesiredAccess, int dwShareMode, IntPtr lpSecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, int hTemplateFile);

	[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	public static extern bool GetOverlappedResult(SafeFileHandle hFile, IntPtr lpOverlapped, ref int lpNumberOfBytesTransferred, bool bWait);

	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern bool ReadFile(SafeFileHandle hFile, IntPtr lpBuffer, int nNumberOfBytesToRead, ref int lpNumberOfBytesRead, IntPtr lpOverlapped);

	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern int WaitForSingleObject(IntPtr hHandle, int dwMilliseconds);

	[DllImport("kernel32.dll", SetLastError = true)]
	public static extern bool WriteFile(SafeFileHandle hFile, byte[] lpBuffer, int nNumberOfBytesToWrite, ref int lpNumberOfBytesWritten, IntPtr lpOverlapped);

	[DllImport("kernel32", SetLastError = true)]
	public static extern bool CloseHandle(IntPtr h);
}
