using System;
using System.Runtime.InteropServices;

namespace UsbHid.USB.Classes.DllWrappers;

public static class User32
{
	[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
	public static extern IntPtr RegisterDeviceNotification(IntPtr hRecipient, IntPtr notificationFilter, int flags);

	[DllImport("user32.dll", SetLastError = true)]
	public static extern bool UnregisterDeviceNotification(IntPtr handle);
}
