#define DEBUG
using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using UsbHid.USB.Classes;
using UsbHid.USB.Classes.Messaging;
using UsbHid.USB.Structures;

namespace UsbHid;

public class UsbHidDevice : IDisposable
{
	public delegate void DataReceivedDelegate(byte[] data);

	public delegate void ConnectedDelegate();

	public delegate void DisConnectedDelegate();

	private DeviceInformationStructure _deviceInformation;

	private readonly BackgroundWorker _worker;

	private FileStream _fsDeviceRead;

	public string DevicePath => _deviceInformation.DevicePathName;

	public bool IsDeviceConnected => _deviceInformation.IsDeviceAttached;

	public event DataReceivedDelegate DataReceived;

	public event ConnectedDelegate OnConnected;

	public event DisConnectedDelegate OnDisConnected;

	public UsbHidDevice(int vendorId, int productId, ArrayList listName)
	{
		_worker = new BackgroundWorker();
		_worker.DoWork += WorkerDoWork;
		_deviceInformation.listName = listName;
		_deviceInformation.TargetVendorId = vendorId;
		_deviceInformation.TargetProductId = productId;
		_deviceInformation.ConnectedChanged += DeviceConnectedChanged;
		DeviceChangeNotifier.DeviceAttached += DeviceChangeNotifierDeviceAttached;
		DeviceChangeNotifier.DeviceDetached += DeviceChangeNotifierDeviceDetached;
	}

	~UsbHidDevice()
	{
		Disconnect();
	}

	private void ReadCompleted(IAsyncResult iResult)
	{
		SyncObjT syncObjT = (SyncObjT)iResult.AsyncState;
		try
		{
			syncObjT.Fs.EndRead(iResult);
			try
			{
				if (this.DataReceived != null)
				{
					this.DataReceived(syncObjT.Buf);
				}
			}
			finally
			{
				BeginAsyncRead(ref syncObjT.Fs, syncObjT.Buf.Length);
			}
		}
		catch (IOException ex)
		{
			Debug.WriteLine(ex.ToString());
		}
	}

	private void DeviceConnectedChanged(bool isConnected)
	{
		if (isConnected)
		{
			ReportConnected();
			_worker.RunWorkerAsync();
		}
		else
		{
			ReportDisConnected();
		}
	}

	private void DeviceChangeNotifierDeviceDetached()
	{
		Disconnect();
	}

	private void DeviceChangeNotifierDeviceAttached()
	{
	}

	public bool Connect()
	{
		DeviceDiscovery.FindTargetDevice(ref _deviceInformation);
		return IsDeviceConnected;
	}

	public void Disconnect()
	{
		if (_fsDeviceRead != null)
		{
			_fsDeviceRead.Close();
		}
		if (IsDeviceConnected)
		{
			Debug.WriteLine("usbGenericHidCommunication:detachUsbDevice() -> Detaching device and closing file handles");
			if (!_deviceInformation.HidHandle.IsInvalid)
			{
				_deviceInformation.HidHandle.Close();
			}
			if (!_deviceInformation.ReadHandle.IsInvalid)
			{
				_deviceInformation.ReadHandle.Close();
			}
			if (!_deviceInformation.WriteHandle.IsInvalid)
			{
				_deviceInformation.WriteHandle.Close();
			}
			_deviceInformation.IsDeviceAttached = false;
		}
		else
		{
			Debug.WriteLine("usbGenericHidCommunication:detachUsbDevice() -> No device attached");
		}
	}

	public bool SendMessage(IMesage message)
	{
		return DeviceCommunication.WriteRawReportToDevice(message.MessageData, ref _deviceInformation);
	}

	private void BeginAsyncRead(ref FileStream fs, int iBufLen)
	{
		SyncObjT syncObjT = new SyncObjT
		{
			Fs = fs,
			Buf = new byte[iBufLen]
		};
		try
		{
			fs.BeginRead(syncObjT.Buf, 0, iBufLen, ReadCompleted, syncObjT);
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex.Message);
		}
	}

	private void WorkerDoWork(object sender, DoWorkEventArgs e)
	{
		_fsDeviceRead = new FileStream(_deviceInformation.ReadHandle, FileAccess.Read, 4096, isAsync: true);
		BeginAsyncRead(ref _fsDeviceRead, _deviceInformation.Capabilities.InputReportByteLength);
	}

	private void ReportConnected()
	{
		if (this.OnConnected != null)
		{
			this.OnConnected();
		}
	}

	private void ReportDisConnected()
	{
		if (this.OnDisConnected != null)
		{
			this.OnDisConnected();
		}
		for (int i = 0; i < _deviceInformation.listName.Count; i++)
		{
			string text = (string)_deviceInformation.listName[i];
			if (text.Equals(_deviceInformation.DevicePathName))
			{
				_deviceInformation.listName.RemoveAt(i);
			}
		}
	}

	public void Dispose()
	{
		Disconnect();
		GC.SuppressFinalize(this);
	}
}
