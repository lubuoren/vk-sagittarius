using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using CSCore.CoreAudioAPI;
using UsbHid;
using UsbHid.USB.Classes.Messaging;
using VAIKYRIE.Properties;

namespace VAIKYRIE;

public class UserDevice : UserControl
{
	public delegate void delegateUserDevice(int cmd, int sub, object obj);

	private delegate void DoUsbHidInvoke(object val);

	public delegateUserDevice delegateUD;

	public readonly ushort USB_BRAND_1 = 1;

	public const ushort USB_ID_1 = 1;

	public const ushort USB_ID_2 = 2;

	public const ushort USB_ID_3 = 3;

	public const ushort USB_ID_4 = 4;

	public const ushort USB_ID_5 = 5;

	public const ushort USB_ID_6 = 6;

	public const ushort USB_ID_7 = 7;

	public const ushort USB_ID_8 = 8;

	public const ushort USB_ID_9 = 9;

	public const ushort USB_ID_10 = 10;

	public const byte USB_PACKED_Head0 = 220;

	public const byte USB_PACKED_Head = 220;

	public const byte USB_PACKED_ONOFF = 0;

	public const byte USB_PACKED_STATE = 1;

	public const byte USB_PACKED_GET_STATE = 2;

	public const byte USB_PACKED_AUDIO = 4;

	public const byte USB_PACKED_MOTOR = 5;

	public static readonly byte USB_PACKED_FAN = 6;

	public static readonly byte USB_PACKED_LED = 16;

	public static readonly byte USB_PACKED_LCD = 48;

	private const byte LED_MODE_RGB = 1;

	private readonly int UserButtonY = 80;

	public ArrayList myUsbList = null;

	private ArrayList myButtonList = null;

	private ArrayList hidNameList1 = null;

	private ArrayList hidNameList2 = null;

	private ArrayList hidNameList3 = null;

	private ArrayList hidNameList4 = null;

	private UsbHidDevice device1 = null;

	private UsbHidDevice device2 = null;

	private UsbHidDevice device3 = null;

	private UsbHidDevice device4 = null;

	private System.Timers.Timer m_timer = null;

	private System.Timers.Timer audioTimer = null;

	private int scanCount = 0;

	public int nowUserButton = 0;

	private bool workingcon = false;

	private bool workingDis = false;

	private byte myAudio = 0;

	private ushort myU16Audio = 0;

	private bool isTimerIN = false;

	private IContainer components = null;

	private Button buttonX;

	private Button button1;

	public UserDevice()
	{
		InitializeComponent();
		myUsbList = new ArrayList();
		myButtonList = new ArrayList();
		hidNameList1 = new ArrayList();
		hidNameList2 = new ArrayList();
		hidNameList3 = new ArrayList();
		hidNameList4 = new ArrayList();
		((ButtonBase)buttonX).FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
		InitMyTimer();
		audioTimer = new System.Timers.Timer(100.0);
		audioTimer.Elapsed += timer_cb;
		audioTimer.AutoReset = true;
		audioTimer.Start();
	}

	private void InitMyTimer()
	{
		m_timer = new System.Timers.Timer(100.0);
		m_timer.Elapsed += Timer_event;
		m_timer.AutoReset = true;
		m_timer.Stop();
	}

	public void Scan_UsbHid()
	{
		scanCount = 0;
		m_timer.Stop();
		m_timer.Interval = 2000.0;
		m_timer.Start();
	}

	private void Timer_event(object sender, ElapsedEventArgs e)
	{
		if (isTimerIN)
		{
			Thread.Sleep(10);
		}
		isTimerIN = true;
		if (m_timer.Interval == 2000.0)
		{
			m_timer.Interval = 200.0;
		}
		else
		{
			m_timer.Interval = 3000.0;
		}
		scanCount++;
		if (scanCount >= 3)
		{
			m_timer.Stop();
			if (device1 != null)
			{
				device1.Dispose();
				device1 = null;
			}
			if (device2 != null)
			{
				device2.Dispose();
				device2 = null;
			}
			if (device3 != null)
			{
				device3.Dispose();
				device3 = null;
			}
			if (device4 != null)
			{
				device4.Dispose();
				device4 = null;
			}
			return;
		}
		if (device1 == null)
		{
			device1 = new UsbHidDevice(1046, 20993, hidNameList1);
			device1.OnConnected += DeviceOnConnected1;
			device1.OnDisConnected += DeviceOnDisConnected1;
			device1.DataReceived += DeviceDataReceived1;
		}
		else if (!device1.IsDeviceConnected)
		{
			device1.Connect();
		}
		if (device2 == null)
		{
			device2 = new UsbHidDevice(1046, 20994, hidNameList2);
			device2.OnConnected += DeviceOnConnected2;
			device2.OnDisConnected += DeviceOnDisConnected2;
			device2.DataReceived += DeviceDataReceived2;
		}
		else if (!device2.IsDeviceConnected)
		{
			device2.Connect();
		}
		if (device3 == null)
		{
			device3 = new UsbHidDevice(1046, 20995, hidNameList3);
			device3.OnConnected += DeviceOnConnected3;
			device3.OnDisConnected += DeviceOnDisConnected3;
			device3.DataReceived += DeviceDataReceived3;
		}
		else if (!device3.IsDeviceConnected)
		{
			device3.Connect();
		}
		isTimerIN = false;
	}

	private void userButton_Click(object sender, EventArgs e)
	{
		for (int i = 0; i < myButtonList.Count; i++)
		{
			ArrayList arrayList = (ArrayList)myButtonList[i];
			UserButton userButton = (UserButton)arrayList[1];
			UserButton userButton2 = (UserButton)sender;
			if (userButton == userButton2)
			{
				nowUserButton = i;
				((Control)userButton).BackgroundImage = (Image)(object)userButton.bitmap2;
			}
			else
			{
				((Control)userButton).BackgroundImage = (Image)(object)userButton.bitmap1;
			}
		}
		delegateUD?.Invoke(6, nowUserButton, 0);
	}

	private void ADDUserButton(int dev)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Expected O, but got Unknown
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		UserButton userButton = new UserButton();
		((Control)userButton).BackColor = Color.Transparent;
		((Control)userButton).BackgroundImage = null;
		((Control)userButton).BackgroundImageLayout = (ImageLayout)3;
		((Control)userButton).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)userButton).Location = new Point(13, 59 + myButtonList.Count * UserButtonY);
		((Control)userButton).Margin = new Padding(0);
		((Control)userButton).Name = "userButton" + myButtonList.Count;
		((Control)userButton).Size = new Size(211, 76);
		((Control)this).Controls.Add((Control)(object)userButton);
		((Control)userButton).Click += userButton_Click;
		userButton.myType = dev;
		switch (dev)
		{
		case 1:
			((Control)userButton).BackgroundImage = (Image)(object)Resources.Device1_1;
			userButton.bitmap1 = Resources.Device1_1;
			userButton.bitmap2 = Resources.Device1_2;
			break;
		case 2:
			((Control)userButton).BackgroundImage = (Image)(object)Resources.Device2_1;
			userButton.bitmap1 = Resources.Device2_1;
			userButton.bitmap2 = Resources.Device2_2;
			break;
		case 3:
			((Control)userButton).BackgroundImage = (Image)(object)Resources.Device3_1;
			userButton.bitmap1 = Resources.Device3_1;
			userButton.bitmap2 = Resources.Device3_2;
			break;
		case 4:
			((Control)userButton).BackgroundImage = (Image)(object)Resources.Device1_1;
			userButton.bitmap1 = Resources.Device1_1;
			userButton.bitmap2 = Resources.Device1_2;
			break;
		}
		ArrayList arrayList = new ArrayList();
		arrayList.Add("SerialNumber:");
		arrayList.Add(userButton);
		if (myButtonList.Count == 0)
		{
			((Control)userButton).BackgroundImage = (Image)(object)userButton.bitmap2;
		}
		myButtonList.Add(arrayList);
	}

	private void DeleteUserButton(int count)
	{
		ArrayList arrayList = (ArrayList)myButtonList[count];
		UserButton userButton = (UserButton)arrayList[1];
		((Control)this).Controls.Remove((Control)(object)userButton);
		((Component)(object)userButton).Dispose();
		arrayList.Clear();
		myButtonList.RemoveAt(count);
		if (nowUserButton == count)
		{
			nowUserButton = 0;
			if (myButtonList.Count > 0)
			{
				arrayList = (ArrayList)myButtonList[0];
				userButton = (UserButton)arrayList[1];
				((Control)userButton).BackgroundImage = (Image)(object)userButton.bitmap2;
			}
		}
		for (int i = count; i < myButtonList.Count; i++)
		{
			arrayList = (ArrayList)myButtonList[i];
			userButton = (UserButton)arrayList[1];
			((Control)userButton).Location = new Point(13, 59 + i * UserButtonY);
		}
	}

	private void DoUsbHidEvent(object val)
	{
		ArrayList arrayList = (ArrayList)val;
		switch ((int)arrayList[0])
		{
		case 0:
			if (myUsbList.Count > 0)
			{
				for (int num = myUsbList.Count - 1; num >= 0; num--)
				{
					UsbHidDevice usbHidDevice = (UsbHidDevice)myUsbList[num];
					if (!usbHidDevice.IsDeviceConnected)
					{
						myUsbList.RemoveAt(num);
						usbHidDevice.Dispose();
						DeleteUserButton(num);
						delegateUD?.Invoke(0, num, 0);
					}
				}
				for (int i = 0; i < myUsbList.Count; i++)
				{
					UsbHidDevice usbHidDevice2 = (UsbHidDevice)myUsbList[i];
					byte[] obj3 = new byte[14]
					{
						220, 220, 0, 0, 1, 0, 0, 0, 0, 0,
						0, 1, 0, 0
					};
					obj3[2] = (byte)USB_BRAND_1;
					obj3[3] = (byte)(USB_BRAND_1 >> 8);
					obj3[10] = (byte)i;
					byte[] parameters3 = obj3;
					CommandMessage message3 = new CommandMessage(parameters3);
					usbHidDevice2.SendMessage(message3);
				}
			}
			workingDis = false;
			break;
		case 1:
		{
			byte[] obj4 = new byte[14]
			{
				220, 220, 0, 0, 1, 0, 0, 0, 0, 0,
				0, 1, 0, 0
			};
			obj4[2] = (byte)USB_BRAND_1;
			obj4[3] = (byte)(USB_BRAND_1 >> 8);
			obj4[10] = (byte)myUsbList.Count;
			byte[] parameters4 = obj4;
			CommandMessage message4 = new CommandMessage(parameters4);
			device1.SendMessage(message4);
			myUsbList.Add(device1);
			ADDUserButton(1);
			delegateUD?.Invoke(1, 1, 0);
			device1 = null;
			Scan_UsbHid();
			workingcon = false;
			break;
		}
		case 2:
		{
			byte[] array = (byte[])arrayList[1];
			delegateUD?.Invoke(2, array[11], array);
			break;
		}
		case 16:
		{
			byte[] obj2 = new byte[14]
			{
				220, 220, 0, 0, 2, 0, 0, 0, 0, 0,
				0, 1, 0, 0
			};
			obj2[2] = (byte)USB_BRAND_1;
			obj2[3] = (byte)(USB_BRAND_1 >> 8);
			obj2[10] = (byte)myUsbList.Count;
			byte[] parameters2 = obj2;
			CommandMessage message2 = new CommandMessage(parameters2);
			device2.SendMessage(message2);
			myUsbList.Add(device2);
			ADDUserButton(2);
			delegateUD?.Invoke(1, 2, 0);
			device2 = null;
			Scan_UsbHid();
			workingcon = false;
			break;
		}
		case 17:
		{
			byte[] obj = new byte[14]
			{
				220, 220, 0, 0, 3, 0, 0, 0, 0, 0,
				0, 1, 0, 0
			};
			obj[2] = (byte)USB_BRAND_1;
			obj[3] = (byte)(USB_BRAND_1 >> 8);
			obj[10] = (byte)myUsbList.Count;
			byte[] parameters = obj;
			CommandMessage message = new CommandMessage(parameters);
			if (device3 != null)
			{
				device3.SendMessage(message);
				myUsbList.Add(device3);
				ADDUserButton(3);
				delegateUD?.Invoke(1, 3, 0);
				device3 = null;
				Scan_UsbHid();
				workingcon = false;
			}
			else
			{
				workingcon = false;
			}
			break;
		}
		}
	}

	private void UsbHidEvent(object hid)
	{
		DoUsbHidInvoke doUsbHidInvoke = DoUsbHidEvent;
		((Control)this).BeginInvoke((Delegate)doUsbHidInvoke, new object[1] { hid });
	}

	private void DeviceOnConnected1()
	{
		while (workingcon)
		{
			Thread.Sleep(1);
		}
		workingcon = true;
		ArrayList arrayList = new ArrayList();
		arrayList.Add(1);
		Thread thread = new Thread(UsbHidEvent);
		thread.Start(arrayList);
	}

	private void DeviceOnDisConnected1()
	{
		while (workingDis)
		{
			Thread.Sleep(1);
		}
		workingDis = true;
		ArrayList arrayList = new ArrayList();
		arrayList.Add(0);
		Thread thread = new Thread(UsbHidEvent);
		thread.Start(arrayList);
	}

	private void DeviceDataReceived1(byte[] data)
	{
		if (data.Length != 0)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(2);
			arrayList.Add(data);
			Thread thread = new Thread(UsbHidEvent);
			thread.Start(arrayList);
		}
	}

	public void sendDeviceData1(int count, int cmd, int ch, int mode, byte[] data)
	{
		try
		{
			UsbHidDevice usbHidDevice = (UsbHidDevice)myUsbList[count];
			byte[] obj = new byte[14]
			{
				220, 220, 0, 0, 1, 0, 0, 0, 0, 0,
				0, 0, 0, 0
			};
			obj[2] = (byte)USB_BRAND_1;
			obj[3] = (byte)(USB_BRAND_1 >> 8);
			obj[10] = (byte)count;
			obj[11] = (byte)cmd;
			obj[12] = (byte)ch;
			obj[13] = (byte)mode;
			byte[] first = obj;
			if (data == null)
			{
				byte[] array = new byte[46];
				array[42] = (byte)Form1.CPUTemp;
				array[43] = (byte)Form1.GPUTemp;
				array[44] = myAudio;
				data = array;
			}
			byte[] parameters = first.Concat(data).ToArray();
			CommandMessage message = new CommandMessage(parameters);
			usbHidDevice.SendMessage(message);
		}
		catch
		{
			Console.WriteLine("Dai: UsbHidDevice myUsbList is Null!");
		}
	}

	private void DeviceOnConnected2()
	{
		while (workingcon)
		{
			Thread.Sleep(1);
		}
		workingcon = true;
		ArrayList arrayList = new ArrayList();
		arrayList.Add(16);
		Thread thread = new Thread(UsbHidEvent);
		thread.Start(arrayList);
	}

	private void DeviceOnDisConnected2()
	{
		while (workingDis)
		{
			Thread.Sleep(1);
		}
		workingDis = true;
		ArrayList arrayList = new ArrayList();
		arrayList.Add(0);
		Thread thread = new Thread(UsbHidEvent);
		thread.Start(arrayList);
	}

	private void DeviceDataReceived2(byte[] data)
	{
		if (data.Length != 0)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(2);
			arrayList.Add(data);
			Thread thread = new Thread(UsbHidEvent);
			thread.Start(arrayList);
		}
	}

	public void sendDeviceData2(int count, int cmd, int ch, int mode, byte[] data)
	{
		try
		{
			UsbHidDevice usbHidDevice = (UsbHidDevice)myUsbList[count];
			byte[] obj = new byte[14]
			{
				220, 220, 0, 0, 2, 0, 0, 0, 0, 0,
				0, 0, 0, 0
			};
			obj[2] = (byte)USB_BRAND_1;
			obj[3] = (byte)(USB_BRAND_1 >> 8);
			obj[10] = (byte)count;
			obj[11] = (byte)cmd;
			obj[12] = (byte)ch;
			obj[13] = (byte)mode;
			byte[] first = obj;
			if (data == null)
			{
				byte[] array = new byte[46];
				array[40] = (byte)Form1.CPUUsage;
				array[41] = (byte)Convert.ToSingle(Form1.MemUsage);
				array[42] = (byte)Form1.CPUTemp;
				array[43] = (byte)Form1.GPUTemp;
				array[44] = (byte)myU16Audio;
				array[45] = (byte)(myU16Audio >> 8);
				data = array;
			}
			byte[] parameters = first.Concat(data).ToArray();
			CommandMessage message = new CommandMessage(parameters);
			usbHidDevice.SendMessage(message);
		}
		catch
		{
			Console.WriteLine("Dai: UsbHidDevice myUsbList is Null!");
		}
	}

	private void DeviceOnConnected3()
	{
		while (workingcon)
		{
			Thread.Sleep(1);
		}
		workingcon = true;
		ArrayList arrayList = new ArrayList();
		arrayList.Add(17);
		Thread thread = new Thread(UsbHidEvent);
		thread.Start(arrayList);
	}

	private void DeviceOnDisConnected3()
	{
		while (workingDis)
		{
			Thread.Sleep(1);
		}
		workingDis = true;
		ArrayList arrayList = new ArrayList();
		arrayList.Add(0);
		Thread thread = new Thread(UsbHidEvent);
		thread.Start(arrayList);
	}

	private void DeviceDataReceived3(byte[] data)
	{
		if (data.Length != 0)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(2);
			arrayList.Add(data);
			Thread thread = new Thread(UsbHidEvent);
			thread.Start(arrayList);
		}
	}

	public void sendDeviceData3(int count, int cmd, int ch, int mode, byte[] data)
	{
		try
		{
			UsbHidDevice usbHidDevice = (UsbHidDevice)myUsbList[count];
			byte[] obj = new byte[14]
			{
				220, 220, 0, 0, 3, 0, 0, 0, 0, 0,
				0, 0, 0, 0
			};
			obj[2] = (byte)USB_BRAND_1;
			obj[3] = (byte)(USB_BRAND_1 >> 8);
			obj[10] = (byte)count;
			obj[11] = (byte)cmd;
			obj[12] = (byte)ch;
			obj[13] = (byte)mode;
			byte[] first = obj;
			if (data == null)
			{
				byte[] array = new byte[46];
				array[40] = (byte)Form1.CPUUsage;
				array[41] = (byte)Convert.ToSingle(Form1.MemUsage);
				array[42] = (byte)Form1.CPUTemp;
				array[43] = (byte)Form1.GPUTemp;
				array[44] = (byte)myU16Audio;
				array[45] = (byte)(myU16Audio >> 8);
				data = array;
			}
			byte[] parameters = first.Concat(data).ToArray();
			CommandMessage message = new CommandMessage(parameters);
			usbHidDevice.SendMessage(message);
		}
		catch
		{
			Console.WriteLine("Dai: UsbHidDevice myUsbList is Null!");
		}
	}

	private void DeviceOnConnected4()
	{
		while (workingcon)
		{
			Thread.Sleep(1);
		}
		workingcon = true;
		ArrayList arrayList = new ArrayList();
		arrayList.Add(18);
		Thread thread = new Thread(UsbHidEvent);
		thread.Start(arrayList);
	}

	private void DeviceOnDisConnected4()
	{
		while (workingDis)
		{
			Thread.Sleep(1);
		}
		workingDis = true;
		ArrayList arrayList = new ArrayList();
		arrayList.Add(0);
		Thread thread = new Thread(UsbHidEvent);
		thread.Start(arrayList);
	}

	private void DeviceDataReceived4(byte[] data)
	{
		if (data.Length != 0)
		{
			ArrayList arrayList = new ArrayList();
			arrayList.Add(2);
			arrayList.Add(data);
			Thread thread = new Thread(UsbHidEvent);
			thread.Start(arrayList);
		}
	}

	public void sendDeviceData4(int count, int cmd, int ch, int mode, byte[] data)
	{
		try
		{
			UsbHidDevice usbHidDevice = (UsbHidDevice)myUsbList[count];
			byte[] obj = new byte[14]
			{
				220, 220, 0, 0, 2, 0, 0, 0, 0, 0,
				0, 0, 0, 0
			};
			obj[2] = (byte)USB_BRAND_1;
			obj[3] = (byte)(USB_BRAND_1 >> 8);
			obj[10] = (byte)count;
			obj[11] = (byte)cmd;
			obj[12] = (byte)ch;
			obj[13] = (byte)mode;
			byte[] first = obj;
			if (data == null)
			{
				byte[] array = new byte[46];
				array[40] = (byte)Form1.CPUUsage;
				array[41] = (byte)Convert.ToSingle(Form1.MemUsage);
				array[42] = (byte)Form1.CPUTemp;
				array[43] = (byte)Form1.GPUTemp;
				array[44] = (byte)myU16Audio;
				array[45] = (byte)(myU16Audio >> 8);
				data = array;
			}
			byte[] parameters = first.Concat(data).ToArray();
			CommandMessage message = new CommandMessage(parameters);
			usbHidDevice.SendMessage(message);
		}
		catch
		{
			Console.WriteLine("Dai: UsbHidDevice myUsbList is Null!");
		}
	}

	private void buttonX_Click(object sender, EventArgs e)
	{
		delegateUD?.Invoke(-1, 0, null);
	}

	private void button1_Click(object sender, EventArgs e)
	{
		delegateUD?.Invoke(-2, 0, null);
	}

	private AudioSessionManager2 GetDefaultAudioSessionManager2(DataFlow dataFlow)
	{
		using MMDeviceEnumerator mMDeviceEnumerator = new MMDeviceEnumerator();
		if (mMDeviceEnumerator == null)
		{
			return null;
		}
		using MMDevice mMDevice = mMDeviceEnumerator.GetDefaultAudioEndpoint(dataFlow, Role.Multimedia);
		if (mMDevice == null)
		{
			return null;
		}
		return AudioSessionManager2.FromMMDevice(mMDevice);
	}

	private void getVolume()
	{
		try
		{
			using AudioSessionManager2 audioSessionManager = GetDefaultAudioSessionManager2(DataFlow.Render);
			if (audioSessionManager == null)
			{
				return;
			}
			using AudioSessionEnumerator audioSessionEnumerator = audioSessionManager.GetSessionEnumerator();
			if (audioSessionEnumerator == null)
			{
				return;
			}
			bool flag = false;
			foreach (AudioSessionControl item in audioSessionEnumerator)
			{
				using AudioMeterInformation audioMeterInformation = item.QueryInterface<AudioMeterInformation>();
				float peakValue = audioMeterInformation.GetPeakValue();
				if (peakValue > 0f)
				{
					myAudio = (byte)(peakValue * 255f);
					myU16Audio = (ushort)(peakValue * 4095f);
					flag = true;
				}
			}
			if (!flag)
			{
				myAudio = 0;
				myU16Audio = 0;
			}
		}
		catch (Exception ex)
		{
			Console.Write("Unexpected error occured. Might be no audio device configured in your system." + ex.Message);
		}
	}

	private void timer_cb(object sender, ElapsedEventArgs e)
	{
		getVolume();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		((ContainerControl)this).Dispose(disposing);
	}

	private void InitializeComponent()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Expected O, but got Unknown
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		buttonX = new Button();
		button1 = new Button();
		((Control)this).SuspendLayout();
		((Control)buttonX).BackColor = Color.Transparent;
		((Control)buttonX).BackgroundImage = (Image)(object)Resources.X;
		((Control)buttonX).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonX).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonX).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonX).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonX).FlatStyle = (FlatStyle)0;
		((Control)buttonX).Location = new Point(233, 4);
		((Control)buttonX).Margin = new Padding(0);
		((Control)buttonX).Name = "buttonX";
		((Control)buttonX).Size = new Size(24, 16);
		((Control)buttonX).TabIndex = 27;
		((ButtonBase)buttonX).UseVisualStyleBackColor = false;
		((Control)buttonX).Click += buttonX_Click;
		((Control)button1).BackColor = Color.Transparent;
		((Control)button1).BackgroundImage = (Image)(object)Resources.zxh;
		((Control)button1).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button1).FlatAppearance.BorderSize = 0;
		((ButtonBase)button1).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button1).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button1).FlatStyle = (FlatStyle)0;
		((Control)button1).Location = new Point(209, 4);
		((Control)button1).Margin = new Padding(0);
		((Control)button1).Name = "button1";
		((Control)button1).Size = new Size(24, 16);
		((Control)button1).TabIndex = 28;
		((ButtonBase)button1).UseVisualStyleBackColor = false;
		((Control)button1).Click += button1_Click;
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)3;
		((Control)this).BackColor = Color.Transparent;
		((Control)this).BackgroundImage = (Image)(object)Resources.D1_3;
		((Control)this).BackgroundImageLayout = (ImageLayout)3;
		((Control)this).Controls.Add((Control)(object)button1);
		((Control)this).Controls.Add((Control)(object)buttonX);
		((Control)this).DoubleBuffered = true;
		((Control)this).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)this).Margin = new Padding(0);
		((Control)this).Name = "UserDevice";
		((Control)this).Size = new Size(275, 800);
		((Control)this).ResumeLayout(false);
	}
}
