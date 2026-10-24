using System;
using System.Runtime.InteropServices;
using UsbHid.USB.Structures;

namespace UsbHid.USB.Classes.DllWrappers;

public static class SetupApi
{
	[DllImport("setupapi.dll", SetLastError = true)]
	public static extern int SetupDiCreateDeviceInfoList(ref Guid classGuid, int hwndParent);

	[DllImport("setupapi.dll", SetLastError = true)]
	public static extern int SetupDiDestroyDeviceInfoList(IntPtr deviceInfoSet);

	[DllImport("setupapi.dll", SetLastError = true)]
	public static extern IntPtr SetupDiGetClassDevs(ref Guid lpHidGuid, IntPtr enumerator, IntPtr hwndParent, int flags);

	[DllImport("setupapi.dll", SetLastError = true)]
	public static extern bool SetupDiEnumDeviceInterfaces(IntPtr deviceInfoSet, IntPtr deviceInfoData, ref Guid hidGuid, int memberIndex, ref SpDeviceInterfaceData deviceInterfaceData);

	[DllImport("setupapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
	public static extern bool SetupDiGetDeviceInterfaceDetail(IntPtr deviceInfoSet, ref SpDeviceInterfaceData deviceInterfaceData, IntPtr deviceInterfaceDetailData, int deviceInterfaceDetailDataSize, ref int requiredSize, IntPtr deviceInfoData);
}
