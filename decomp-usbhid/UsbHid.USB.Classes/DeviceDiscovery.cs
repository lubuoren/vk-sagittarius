#define DEBUG
using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using UsbHid.USB.Classes.DllWrappers;
using UsbHid.USB.Structures;

namespace UsbHid.USB.Classes;

public static class DeviceDiscovery
{
	public static bool FindHidDevices(ref string[] listOfDevicePathNames, ref int numberOfDevicesFound)
	{
		Debug.WriteLine("usbGenericHidCommunication:findHidDevices() -> Method called");
		int requiredSize = 0;
		IntPtr intPtr = IntPtr.Zero;
		IntPtr intPtr2 = default(IntPtr);
		bool flag = false;
		SpDeviceInterfaceData deviceInterfaceData = default(SpDeviceInterfaceData);
		Guid hidGuid = default(Guid);
		Hid.HidD_GetHidGuid(ref hidGuid);
		Debug.WriteLine("usbGenericHidCommunication:findHidDevices() -> Fetched GUID for HID devices ({0})", hidGuid.ToString());
		bool flag2;
		int num;
		try
		{
			Debug.WriteLine("usbGenericHidCommunication:findHidDevices() -> Using SetupDiGetClassDevs to get all devices with the correct GUID");
			intPtr2 = SetupApi.SetupDiGetClassDevs(ref hidGuid, IntPtr.Zero, IntPtr.Zero, 18);
			flag2 = false;
			num = 0;
			deviceInterfaceData.cbSize = Marshal.SizeOf((object)deviceInterfaceData);
			do
			{
				if (!SetupApi.SetupDiEnumDeviceInterfaces(intPtr2, IntPtr.Zero, ref hidGuid, num, ref deviceInterfaceData))
				{
					flag = true;
				}
				else
				{
					SetupApi.SetupDiGetDeviceInterfaceDetail(intPtr2, ref deviceInterfaceData, IntPtr.Zero, 0, ref requiredSize, IntPtr.Zero);
					intPtr = Marshal.AllocHGlobal(requiredSize);
					Marshal.WriteInt32(intPtr, (IntPtr.Size == 4) ? (4 + Marshal.SystemDefaultCharSize) : 8);
					SetupApi.SetupDiGetDeviceInterfaceDetail(intPtr2, ref deviceInterfaceData, intPtr, requiredSize, ref requiredSize, IntPtr.Zero);
					IntPtr ptr = new IntPtr(intPtr.ToInt32() + 4);
					listOfDevicePathNames[num] = Marshal.PtrToStringAuto(ptr);
					flag2 = true;
				}
				num++;
			}
			while (!flag);
		}
		catch (Exception)
		{
			Debug.WriteLine("usbGenericHidCommunication:findHidDevices() -> EXCEPTION: Something went south whilst trying to get devices with matching GUIDs - giving up!");
			return false;
		}
		finally
		{
			if (intPtr != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(intPtr);
			}
			if (intPtr2 != IntPtr.Zero)
			{
				SetupApi.SetupDiDestroyDeviceInfoList(intPtr2);
			}
		}
		if (flag2)
		{
			Debug.WriteLine("usbGenericHidCommunication:findHidDevices() -> Found {0} devices with matching GUID", (num - 1).ToString(CultureInfo.InvariantCulture));
			numberOfDevicesFound = num - 2;
		}
		else
		{
			Debug.WriteLine("usbGenericHidCommunication:findHidDevices() -> No matching devices found");
		}
		return flag2;
	}

	public static bool FindTargetDevice(ref DeviceInformationStructure deviceInformation)
	{
		Debug.WriteLine("usbGenericHidCommunication:findTargetDevice() -> Method called");
		string[] listOfDevicePathNames = new string[128];
		int numberOfDevicesFound = 0;
		try
		{
			bool flag = false;
			deviceInformation.IsDeviceAttached = false;
			if (FindHidDevices(ref listOfDevicePathNames, ref numberOfDevicesFound))
			{
				Debug.WriteLine("usbGenericHidCommunication:findTargetDevice() -> Devices with matching GUID found...");
				int num = 0;
				do
				{
					Debug.WriteLine("usbGenericHidCommunication:findTargetDevice() -> Performing CreateFile to listIndex {0}", num.ToString(CultureInfo.InvariantCulture));
					deviceInformation.HidHandle = Kernel32.CreateFile(listOfDevicePathNames[num], 0u, 3, IntPtr.Zero, 3, 0, 0);
					if (!deviceInformation.HidHandle.IsInvalid)
					{
						deviceInformation.Attributes.Size = Marshal.SizeOf((object)deviceInformation.Attributes);
						if (Hid.HidD_GetAttributes(deviceInformation.HidHandle, ref deviceInformation.Attributes))
						{
							Debug.WriteLine($"usbGenericHidCommunication:findTargetDevice() -> Found device with VID {Convert.ToString(deviceInformation.Attributes.VendorID, 16)}, PID {Convert.ToString(deviceInformation.Attributes.ProductID, 16)} and Version number {Convert.ToString(deviceInformation.Attributes.VersionNumber, 16)}, Name is {Convert.ToString(listOfDevicePathNames[num])}");
							if (deviceInformation.Attributes.VendorID == deviceInformation.TargetVendorId && deviceInformation.Attributes.ProductID == deviceInformation.TargetProductId)
							{
								bool flag2 = false;
								for (int i = 0; i < deviceInformation.listName.Count; i++)
								{
									string text = (string)deviceInformation.listName[i];
									if (text.Equals(listOfDevicePathNames[num]))
									{
										flag2 = true;
										Debug.WriteLine("usbGenericHidCommunication:findTargetDevice() -> Device is Existence... Continuing...");
										deviceInformation.HidHandle.Close();
									}
								}
								if (!flag2)
								{
									Debug.WriteLine("usbGenericHidCommunication:findTargetDevice() -> Device with matching VID and PID found!");
									flag = true;
									deviceInformation.DevicePathName = listOfDevicePathNames[num];
									deviceInformation.listName.Add(deviceInformation.DevicePathName);
								}
							}
							else
							{
								Debug.WriteLine("usbGenericHidCommunication:findTargetDevice() -> Device didn't match... Continuing...");
								deviceInformation.HidHandle.Close();
							}
						}
						else
						{
							Debug.WriteLine("usbGenericHidCommunication:findTargetDevice() -> Something bad happened - couldn't fill the HIDD_ATTRIBUTES, giving up!");
							deviceInformation.HidHandle.Close();
						}
					}
					num++;
				}
				while (!flag && num != numberOfDevicesFound + 1);
			}
			if (flag)
			{
				QueryDeviceCapabilities(ref deviceInformation);
				Debug.WriteLine("usbGenericHidCommunication:findTargetDevice() -> Opening a readHandle to the device");
				deviceInformation.ReadHandle = Kernel32.CreateFile(deviceInformation.DevicePathName, 2147483648u, 3, IntPtr.Zero, 3, 1073741824, 0);
				if (deviceInformation.ReadHandle.IsInvalid)
				{
					Debug.WriteLine("usbGenericHidCommunication:findTargetDevice() -> Unable to open a readHandle to the device!");
					return false;
				}
				Debug.WriteLine("usbGenericHidCommunication:findTargetDevice() -> Opening a writeHandle to the device");
				deviceInformation.WriteHandle = Kernel32.CreateFile(deviceInformation.DevicePathName, 1073741824u, 3, IntPtr.Zero, 3, 0, 0);
				if (deviceInformation.WriteHandle.IsInvalid)
				{
					Debug.WriteLine("usbGenericHidCommunication:findTargetDevice() -> Unable to open a writeHandle to the device!");
					deviceInformation.WriteHandle.Close();
					return false;
				}
				deviceInformation.IsDeviceAttached = true;
				return true;
			}
			Debug.WriteLine("usbGenericHidCommunication:findTargetDevice() -> No matching device found!");
			return false;
		}
		catch (Exception)
		{
			Debug.WriteLine("usbGenericHidCommunication:findTargetDevice() -> EXCEPTION: Unknown - device not found");
			return false;
		}
	}

	public static void QueryDeviceCapabilities(ref DeviceInformationStructure deviceInformation)
	{
		IntPtr preparsedData = default(IntPtr);
		try
		{
			Hid.HidD_GetPreparsedData(deviceInformation.HidHandle, ref preparsedData);
			if (Hid.HidP_GetCaps(preparsedData, ref deviceInformation.Capabilities) != 0)
			{
				Debug.WriteLine("usbGenericHidCommunication:queryDeviceCapabilities() -> Device query results:");
				Debug.WriteLine("usbGenericHidCommunication:queryDeviceCapabilities() ->     Usage: {0}", Convert.ToString(deviceInformation.Capabilities.Usage, 16));
				Debug.WriteLine("usbGenericHidCommunication:queryDeviceCapabilities() ->     Usage Page: {0}", Convert.ToString(deviceInformation.Capabilities.UsagePage, 16));
				Debug.WriteLine("usbGenericHidCommunication:queryDeviceCapabilities() ->     Input Report Byte Length: {0}", deviceInformation.Capabilities.InputReportByteLength.ToString(CultureInfo.InvariantCulture));
				Debug.WriteLine("usbGenericHidCommunication:queryDeviceCapabilities() ->     Output Report Byte Length: {0}", deviceInformation.Capabilities.OutputReportByteLength.ToString(CultureInfo.InvariantCulture));
				Debug.WriteLine("usbGenericHidCommunication:queryDeviceCapabilities() ->     Feature Report Byte Length: {0}", deviceInformation.Capabilities.FeatureReportByteLength.ToString(CultureInfo.InvariantCulture));
				Debug.WriteLine("usbGenericHidCommunication:queryDeviceCapabilities() ->     Number of Link Collection Nodes: {0}", deviceInformation.Capabilities.NumberLinkCollectionNodes.ToString(CultureInfo.InvariantCulture));
				Debug.WriteLine("usbGenericHidCommunication:queryDeviceCapabilities() ->     Number of Input Button Caps: {0}", deviceInformation.Capabilities.NumberInputButtonCaps.ToString(CultureInfo.InvariantCulture));
				Debug.WriteLine("usbGenericHidCommunication:queryDeviceCapabilities() ->     Number of Input Value Caps: {0}", deviceInformation.Capabilities.NumberInputValueCaps.ToString(CultureInfo.InvariantCulture));
				Debug.WriteLine("usbGenericHidCommunication:queryDeviceCapabilities() ->     Number of Input Data Indices: {0}", deviceInformation.Capabilities.NumberInputDataIndices.ToString(CultureInfo.InvariantCulture));
				Debug.WriteLine("usbGenericHidCommunication:queryDeviceCapabilities() ->     Number of Output Button Caps: {0}", deviceInformation.Capabilities.NumberOutputButtonCaps.ToString(CultureInfo.InvariantCulture));
				Debug.WriteLine("usbGenericHidCommunication:queryDeviceCapabilities() ->     Number of Output Value Caps: {0}", deviceInformation.Capabilities.NumberOutputValueCaps.ToString(CultureInfo.InvariantCulture));
				Debug.WriteLine("usbGenericHidCommunication:queryDeviceCapabilities() ->     Number of Output Data Indices: {0}", deviceInformation.Capabilities.NumberOutputDataIndices.ToString(CultureInfo.InvariantCulture));
				Debug.WriteLine("usbGenericHidCommunication:queryDeviceCapabilities() ->     Number of Feature Button Caps: {0}", deviceInformation.Capabilities.NumberFeatureButtonCaps.ToString(CultureInfo.InvariantCulture));
				Debug.WriteLine("usbGenericHidCommunication:queryDeviceCapabilities() ->     Number of Feature Value Caps: {0}", deviceInformation.Capabilities.NumberFeatureValueCaps.ToString(CultureInfo.InvariantCulture));
				Debug.WriteLine("usbGenericHidCommunication:queryDeviceCapabilities() ->     Number of Feature Data Indices: {0}", deviceInformation.Capabilities.NumberFeatureDataIndices.ToString(CultureInfo.InvariantCulture));
			}
		}
		catch (Exception)
		{
			Debug.WriteLine("usbGenericHidCommunication:queryDeviceCapabilities() -> EXECEPTION: An unrecoverable error has occurred!");
			throw;
		}
		finally
		{
			if (preparsedData != IntPtr.Zero)
			{
				Hid.HidD_FreePreparsedData(preparsedData);
			}
		}
	}
}
