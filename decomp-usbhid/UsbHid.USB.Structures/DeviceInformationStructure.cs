using System;
using System.Collections;
using Microsoft.Win32.SafeHandles;
using UsbHid.USB.Classes;

namespace UsbHid.USB.Structures;

public struct DeviceInformationStructure
{
	public delegate void ConnectedChangedDelegate(bool isConnected);

	public int TargetVendorId;

	public int TargetProductId;

	public HiddAttributes Attributes;

	public HidpCaps Capabilities;

	public SafeFileHandle ReadHandle;

	public SafeFileHandle WriteHandle;

	public SafeFileHandle HidHandle;

	public IntPtr DeviceNotificationHandle;

	public ArrayList listName;

	private string _devicePathName;

	private bool _isDeviceAttached;

	public string DevicePathName
	{
		get
		{
			return _devicePathName;
		}
		set
		{
			_devicePathName = value;
			if (string.IsNullOrEmpty(_devicePathName))
			{
				DeviceChangeNotifier.Stop();
			}
			else
			{
				DeviceChangeNotifier.Start(_devicePathName);
			}
		}
	}

	public bool IsDeviceAttached
	{
		get
		{
			return _isDeviceAttached;
		}
		set
		{
			if (_isDeviceAttached != value)
			{
				_isDeviceAttached = value;
				if (this.ConnectedChanged != null)
				{
					this.ConnectedChanged(value);
				}
			}
		}
	}

	public event ConnectedChangedDelegate ConnectedChanged;
}
