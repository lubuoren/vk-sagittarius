#define DEBUG
using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using UsbHid.DEBUG.Classes;
using UsbHid.USB.Classes.DllWrappers;
using UsbHid.USB.Structures;

namespace UsbHid.USB.Classes;

public static class DeviceCommunication
{
	private static Debugging debuger = new Debugging();

	public static bool WriteRawReportToDevice(byte[] outputReportBuffer, ref DeviceInformationStructure deviceInformation)
	{
		if (!deviceInformation.IsDeviceAttached)
		{
			Debug.WriteLine("usbGenericHidCommunication:writeReportToDevice(): -> No device attached!");
			return false;
		}
		int lpNumberOfBytesWritten = 0;
		try
		{
			return Kernel32.WriteFile(deviceInformation.WriteHandle, outputReportBuffer, outputReportBuffer.Length, ref lpNumberOfBytesWritten, IntPtr.Zero);
		}
		catch (Exception)
		{
			Debug.WriteLine("daicong usbGenericHidCommunication:writeReportToDevice(): -> EXCEPTION: When attempting to send an output report");
			return false;
		}
	}

	public static bool ReadRawReportFromDeviceAsync(ref byte[] inputReportBuffer, ref int numberOfBytesRead, ref DeviceInformationStructure deviceInformation)
	{
		if (!deviceInformation.IsDeviceAttached)
		{
			Debug.WriteLine("usbGenericHidCommunication:readReportFromDevice(): -> No device attached!");
			return false;
		}
		NativeOverlapped nativeOverlapped = default(NativeOverlapped);
		IntPtr intPtr;
		IntPtr intPtr2;
		IntPtr intPtr3;
		bool flag;
		try
		{
			intPtr = Kernel32.CreateEvent(IntPtr.Zero, bManualReset: false, bInitialState: false, "");
			nativeOverlapped.OffsetLow = 0;
			nativeOverlapped.OffsetHigh = 0;
			nativeOverlapped.EventHandle = intPtr;
			intPtr2 = Marshal.AllocHGlobal(inputReportBuffer.Length);
			intPtr3 = Marshal.AllocHGlobal(Marshal.SizeOf((object)nativeOverlapped));
			Marshal.StructureToPtr((object)nativeOverlapped, intPtr3, fDeleteOld: false);
			flag = Kernel32.ReadFile(deviceInformation.ReadHandle, intPtr2, inputReportBuffer.Length, ref numberOfBytesRead, intPtr3);
			if (flag)
			{
				Debug.WriteLine("usbGenericHidCommunication:readReportFromDevice(): -> Read Ok");
			}
			Marshal.Copy(intPtr2, inputReportBuffer, 0, numberOfBytesRead);
		}
		catch (Exception)
		{
			Debug.WriteLine("usbGenericHidCommunication:readReportFromDevice(): -> EXCEPTION: When attempting to receive an input report");
			return false;
		}
		Marshal.FreeHGlobal(intPtr2);
		Marshal.FreeHGlobal(intPtr3);
		Kernel32.CloseHandle(intPtr);
		return flag;
	}

	public static bool ReadRawReportFromDevice(ref byte[] inputReportBuffer, ref int numberOfBytesRead, ref DeviceInformationStructure deviceInformation)
	{
		if (!deviceInformation.IsDeviceAttached)
		{
			Debug.WriteLine("usbGenericHidCommunication:readReportFromDevice(): -> No device attached!");
			return false;
		}
		NativeOverlapped nativeOverlapped = default(NativeOverlapped);
		IntPtr intPtr;
		IntPtr intPtr2;
		IntPtr intPtr3;
		try
		{
			intPtr = Kernel32.CreateEvent(IntPtr.Zero, bManualReset: false, bInitialState: false, "");
			nativeOverlapped.OffsetLow = 0;
			nativeOverlapped.OffsetHigh = 0;
			nativeOverlapped.EventHandle = intPtr;
			intPtr2 = Marshal.AllocHGlobal(inputReportBuffer.Length);
			intPtr3 = Marshal.AllocHGlobal(Marshal.SizeOf((object)nativeOverlapped));
			Marshal.StructureToPtr((object)nativeOverlapped, intPtr3, fDeleteOld: false);
			Debug.WriteLine("usbGenericHidCommunication:readReportFromDevice(): -> Attempting to ReadFile");
			if (!Kernel32.ReadFile(deviceInformation.ReadHandle, intPtr2, inputReportBuffer.Length, ref numberOfBytesRead, intPtr3))
			{
				Debug.WriteLine("usbGenericHidCommunication:readReportFromDevice(): -> ReadFile started, waiting for completion...");
				switch (Kernel32.WaitForSingleObject(intPtr, 3000))
				{
				case 0:
					break;
				case 258:
					Kernel32.CancelIo(deviceInformation.ReadHandle);
					if (!deviceInformation.HidHandle.IsInvalid)
					{
						deviceInformation.HidHandle.Close();
					}
					if (!deviceInformation.ReadHandle.IsInvalid)
					{
						deviceInformation.ReadHandle.Close();
					}
					if (!deviceInformation.WriteHandle.IsInvalid)
					{
						deviceInformation.WriteHandle.Close();
					}
					Debug.WriteLine("usbGenericHidCommunication:readReportFromDevice(): -> ReadFile timedout! USB device detached");
					return false;
				default:
					Debug.WriteLine("usbGenericHidCommunication:readReportFromDevice(): -> ReadFile unspecified error! USB device detached");
					return false;
				}
				Kernel32.GetOverlappedResult(deviceInformation.ReadHandle, intPtr3, ref numberOfBytesRead, bWait: false);
				Debug.WriteLine("usbGenericHidCommunication:readReportFromDevice(): -> ReadFile successful (overlapped) {0} bytes read", numberOfBytesRead.ToString(CultureInfo.InvariantCulture));
			}
			Marshal.Copy(intPtr2, inputReportBuffer, 0, numberOfBytesRead);
			Debug.WriteLine("usbGenericHidCommunication:readReportFromDevice(): -> ReadFile successful {0} bytes read", numberOfBytesRead.ToString(CultureInfo.InvariantCulture));
		}
		catch (Exception)
		{
			Debug.WriteLine("usbGenericHidCommunication:readReportFromDevice(): -> EXCEPTION: When attempting to receive an input report");
			return false;
		}
		Marshal.FreeHGlobal(intPtr2);
		Marshal.FreeHGlobal(intPtr3);
		Kernel32.CloseHandle(intPtr);
		return true;
	}

	public static bool ReadSingleReportFromDevice(ref byte[] inputReportBuffer, ref DeviceInformationStructure deviceInformation)
	{
		int numberOfBytesRead = 0;
		if (inputReportBuffer.Length != deviceInformation.Capabilities.InputReportByteLength)
		{
			Debug.WriteLine("usbGenericHidCommunication:readSingleReportFromDevice(): -> ERROR: The referenced inputReportBuffer size is incorrect for the input report size!");
			return false;
		}
		return ReadRawReportFromDevice(ref inputReportBuffer, ref numberOfBytesRead, ref deviceInformation);
	}

	public static bool ReadMultipleReportsFromDevice(ref byte[] inputReportBuffer, int numberOfReports, ref DeviceInformationStructure deviceInformation)
	{
		bool flag = false;
		int numberOfBytesRead = 0;
		long num = 0L;
		byte[] inputReportBuffer2 = new byte[inputReportBuffer.Length];
		if (numberOfReports == 0)
		{
			Debug.WriteLine("usbGenericHidCommunication:readMultipleReportsFromDevice(): -> ERROR: You cannot request 0 reports!");
			return false;
		}
		if (numberOfReports > 128)
		{
			Debug.WriteLine("usbGenericHidCommunication:readMultipleReportsFromDevice(): -> ERROR: Reference application testing does not verify the code for more than 128 reports");
			return false;
		}
		if (inputReportBuffer.Length != deviceInformation.Capabilities.InputReportByteLength * numberOfReports)
		{
			Debug.WriteLine("usbGenericHidCommunication:readMultipleReportsFromDevice(): -> ERROR: The referenced inputReportBuffer size is incorrect for the number of input reports requested!");
			return false;
		}
		for (; num != deviceInformation.Capabilities.InputReportByteLength * numberOfReports; num += numberOfBytesRead)
		{
			Debug.WriteLine("usbGenericHidCommunication:readMultipleReportsFromDevice(): -> Reading from device...");
			flag = ReadRawReportFromDevice(ref inputReportBuffer2, ref numberOfBytesRead, ref deviceInformation);
			if (!flag)
			{
				return false;
			}
			Array.Copy(inputReportBuffer2, 0L, inputReportBuffer, num, numberOfBytesRead);
		}
		return flag;
	}
}
