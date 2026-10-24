#define DEBUG
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using UsbHid.USB.Classes.DllWrappers;
using UsbHid.USB.Structures;

namespace UsbHid.USB.Classes;

public class DeviceChangeNotifier : Form
{
	public delegate void DeviceNotifyDelegate(Message msg);

	public delegate void DeviceAttachedDelegate();

	public delegate void DeviceDetachedDelegate();

	public IntPtr DeviceNotificationHandle;

	private static string devicepath;

	private static DeviceChangeNotifier mInstance;

	public static event DeviceNotifyDelegate DeviceNotify;

	public static event DeviceAttachedDelegate DeviceAttached;

	public static event DeviceDetachedDelegate DeviceDetached;

	public DeviceChangeNotifier()
	{
		RegisterForDeviceNotifications(((Control)this).Handle);
	}

	public static void Start(string devicePath)
	{
		devicepath = devicePath;
		Thread thread = new Thread(RunForm);
		thread.SetApartmentState(ApartmentState.STA);
		thread.IsBackground = true;
		thread.Start();
	}

	public static void Stop()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected O, but got Unknown
		try
		{
			if (mInstance == null)
			{
				throw new InvalidOperationException("Notifier not started");
			}
			DeviceChangeNotifier.DeviceNotify = null;
			((Control)mInstance).Invoke((Delegate)new MethodInvoker(mInstance.EndForm));
		}
		catch (Exception ex)
		{
			Debug.WriteLine(ex.Message);
		}
	}

	private static void RunForm()
	{
		Application.Run((Form)(object)new DeviceChangeNotifier());
	}

	private void EndForm()
	{
		((Form)this).Close();
	}

	protected override void SetVisibleCore(bool value)
	{
		if (mInstance == null)
		{
			mInstance = this;
			try
			{
				((Control)this).CreateHandle();
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
			}
		}
		((Form)this).SetVisibleCore(false);
	}

	protected override void WndProc(ref Message m)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		if (((Message)(ref m)).Msg == 537)
		{
			if (!IsNotificationForTargetDevice(m))
			{
				return;
			}
			HandleDeviceNotificationMessages(m);
		}
		((Form)this).WndProc(ref m);
	}

	public bool RegisterForDeviceNotifications(IntPtr windowHandle1)
	{
		Debug.WriteLine("usbGenericHidCommunication:registerForDeviceNotifications() -> Method called");
		DevBroadcastDeviceinterface devBroadcastDeviceinterface = new DevBroadcastDeviceinterface();
		IntPtr intPtr = IntPtr.Zero;
		Guid hidGuid = default(Guid);
		Hid.HidD_GetHidGuid(ref hidGuid);
		try
		{
			int cb = (devBroadcastDeviceinterface.dbcc_size = Marshal.SizeOf((object)devBroadcastDeviceinterface));
			devBroadcastDeviceinterface.dbcc_devicetype = 5;
			devBroadcastDeviceinterface.dbcc_reserved = 0;
			devBroadcastDeviceinterface.dbcc_classguid = hidGuid;
			intPtr = Marshal.AllocHGlobal(cb);
			Marshal.StructureToPtr((object)devBroadcastDeviceinterface, intPtr, fDeleteOld: true);
			DeviceNotificationHandle = User32.RegisterDeviceNotification(((Control)this).Handle, intPtr, 0);
			Marshal.PtrToStructure(intPtr, (object)devBroadcastDeviceinterface);
			int num = DeviceNotificationHandle.ToInt32();
			IntPtr zero = IntPtr.Zero;
			if (num == zero.ToInt32())
			{
				Debug.WriteLine("usbGenericHidCommunication:registerForDeviceNotifications() -> Notification registration failed");
				return false;
			}
			Debug.WriteLine("usbGenericHidCommunication:registerForDeviceNotifications() -> Notification registration succeded");
			return true;
		}
		catch (Exception ex)
		{
			Debug.WriteLine("usbGenericHidCommunication:registerForDeviceNotifications() -> EXCEPTION: An unknown exception has occured!");
			Debug.WriteLine(ex.Message);
		}
		finally
		{
			if (intPtr != IntPtr.Zero)
			{
				try
				{
					Marshal.FreeHGlobal(intPtr);
				}
				catch (Exception ex2)
				{
					Debug.WriteLine(ex2.Message);
				}
			}
		}
		return false;
	}

	public static bool IsNotificationForTargetDevice(Message m)
	{
		if (string.IsNullOrEmpty(devicepath))
		{
			return false;
		}
		try
		{
			DevBroadcastDeviceinterface1 devBroadcastDeviceinterface = new DevBroadcastDeviceinterface1();
			DevBroadcastHdr devBroadcastHdr = new DevBroadcastHdr();
			try
			{
				Marshal.PtrToStructure(((Message)(ref m)).LParam, (object)devBroadcastHdr);
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.Message);
				return false;
			}
			if (devBroadcastHdr.dbch_devicetype == 5)
			{
				int num = Convert.ToInt32((devBroadcastHdr.dbch_size - 32) / 2);
				devBroadcastDeviceinterface.dbcc_name = new char[num + 1];
				Marshal.PtrToStructure(((Message)(ref m)).LParam, (object)devBroadcastDeviceinterface);
				string text = new string(devBroadcastDeviceinterface.dbcc_name, 0, num);
				return string.Compare(text.ToLower(), devicepath.ToLower(), StringComparison.OrdinalIgnoreCase) == 0;
			}
		}
		catch (Exception ex2)
		{
			Debug.WriteLine("usbGenericHidCommunication:isNotificationForTargetDevice() -> EXCEPTION: An unknown exception has occured!");
			Debug.WriteLine(ex2.Message);
			return false;
		}
		return false;
	}

	public void HandleDeviceNotificationMessages(Message m)
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		if (((Message)(ref m)).Msg != 537)
		{
			return;
		}
		Debug.WriteLine("usbGenericHidCommunication:handleDeviceNotificationMessages() -> Device notification received");
		try
		{
			switch (((Message)(ref m)).WParam.ToInt32())
			{
			case 32768:
				Debug.WriteLine("usbGenericHidCommunication:handleDeviceNotificationMessages() -> New device attached");
				ReportDeviceAttached(m);
				break;
			case 32772:
				Debug.WriteLine("usbGenericHidCommunication:handleDeviceNotificationMessages() -> A device has been removed");
				if (IsNotificationForTargetDevice(m))
				{
					Debug.WriteLine("usbGenericHidCommunication:handleDeviceNotificationMessages() -> The target USB device has been removed - detaching...");
					ReportDeviceDetached(m);
				}
				break;
			default:
				Debug.WriteLine("usbGenericHidCommunication:handleDeviceNotificationMessages() -> Unknown notification message");
				break;
			}
		}
		catch (Exception ex)
		{
			Debug.WriteLine("usbGenericHidCommunication:handleDeviceNotificationMessages() -> EXCEPTION: An unknown exception has occured!");
			Debug.WriteLine(ex.Message);
		}
	}

	private void ReportDeviceDetached(Message message)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		if (DeviceChangeNotifier.DeviceDetached != null)
		{
			DeviceChangeNotifier.DeviceDetached();
		}
		if (DeviceChangeNotifier.DeviceNotify != null)
		{
			DeviceChangeNotifier.DeviceNotify(message);
		}
	}

	private void ReportDeviceAttached(Message message)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		if (DeviceChangeNotifier.DeviceAttached != null)
		{
			DeviceChangeNotifier.DeviceAttached();
		}
		if (DeviceChangeNotifier.DeviceNotify != null)
		{
			DeviceChangeNotifier.DeviceNotify(message);
		}
	}
}
