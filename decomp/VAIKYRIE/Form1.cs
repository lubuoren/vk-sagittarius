using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using VAIKYRIE.Properties;

namespace VAIKYRIE;

public class Form1 : Form
{
	public delegate void DoSystemInfoInvoke(string str);

	public static string ComputerName;

	public static string UserName;

	public static string CPUName;

	public static string GPUName;

	public static string WindowsName;

	public static string MainName;

	public static string MainTemp;

	public static int CPUUsage;

	public static float CPUTemp;

	public static string CPUFrequency;

	public static string CPUNowFrequency;

	public static string CPUPower;

	public static string CPUVoltage;

	public static string CPUFan;

	public static string CPUCore;

	public static string CPUThreading;

	public static ArrayList CPUCoreTemp = null;

	public static int GPUUsage;

	public static float GPUTemp;

	public static string GPUFrequency;

	public static string GPUPower;

	public static string GPUFan;

	public static string GPUBrand;

	public static string GPUProcessor;

	public static string GPUMem;

	public static string DisplayName;

	public static string DisplayResolution;

	public static string DisplayRefresh;

	public static string DisplaySize;

	public static float NetworkUp;

	public static float NetworkDown;

	public static float MainPower;

	public static string MainBrand;

	public static string MainProcessor;

	public static string DRAMPower;

	public static string MemFrequency;

	public static string MemNowFrequency;

	public static string MemUsage;

	public static string MemVoltage;

	public static string MemTemp;

	public static string MemSize;

	public static string MemSubInfo;

	public static ArrayList MemSubMInfo = null;

	public static ArrayList HDDInfo = null;

	public static bool UIn = false;

	public static string UInName = "";

	public static ArrayList arrayListPAN = null;

	private const int CPU_GPU_COUNT = 5;

	private int cpuGpuCount = 4;

	public ArrayList CPUTempArray;

	public ArrayList GPUTempArray;

	public static byte configHead = 220;

	public static string configFile = "config.d";

	public const int WM_DEVICECHANGE = 537;

	public const int DBT_DEVICEARRIVAL = 32768;

	public const int DBT_CONFIGCHANGECANCELED = 25;

	public const int DBT_CONFIGCHANGED = 24;

	public const int DBT_CUSTOMEVENT = 32774;

	public const int DBT_DEVICEQUERYREMOVE = 32769;

	public const int DBT_DEVICEQUERYREMOVEFAILED = 32770;

	public const int DBT_DEVICEREMOVECOMPLETE = 32772;

	public const int DBT_DEVICEREMOVEPENDING = 32771;

	public const int DBT_DEVICETYPESPECIFIC = 32773;

	public const int DBT_DEVNODES_CHANGED = 7;

	public const int DBT_QUERYCHANGECONFIG = 23;

	public const int DBT_USERDEFINED = 65535;

	private FormMain formMain = null;

	private FormMainDetails formMainDetails = null;

	private FormSetting formSetting = null;

	private ArrayList formDeviceArray = null;

	private bool isMouseDown;

	private Point _mousePoint;

	private int myMode = 0;

	private int nowFormDevice = 0;

	public System.Timers.Timer m_timer;

	private int proc_index = 0;

	private int adapter_index = 0;

	public static CPUIDSDK pSDK;

	private int timerCount = 0;

	private NetworkAdapter workAdapter = null;

	private FormStart formStart = null;

	private bool kaiji = false;

	private IContainer components = null;

	private UserMain userMain1;

	private UserCPU userCPU1;

	private UserDevice userDevice1;

	private NotifyIcon mainNotifyIcon;

	private ContextMenuStrip contextMenuStrip1;

	private ToolStripMenuItem 退出ToolStripMenuItem;

	public Label bkLabel;

	private void InitNetWorkSpeed()
	{
		NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
		NetworkInterface[] array = allNetworkInterfaces;
		foreach (NetworkInterface networkInterface in array)
		{
			if (networkInterface.Name.Contains("WLAN"))
			{
				workAdapter = new NetworkAdapter(null);
				workAdapter.NetworkAdapterNew(networkInterface);
			}
		}
		if (workAdapter == null)
		{
			workAdapter = new NetworkAdapter(null);
		}
		workAdapter.init();
	}

	private void FormInitDc()
	{
		InitNetWorkSpeed();
		int _version = 0;
		int _errorcode = 0;
		int _extended_errorcode = 0;
		pSDK = new CPUIDSDK();
		bool flag = pSDK.CreateInstance();
		flag = pSDK.Init("", "cpuidsdk.dll", 2147483647u, ref _errorcode, ref _extended_errorcode);
		if ((long)_errorcode != 0)
		{
			switch ((uint)_errorcode)
			{
			case 1u:
				switch ((uint)_extended_errorcode)
				{
				case 1u:
				{
					string text = "You are running a trial version of the DLL SDK. In order to make it work, please run CPU-Z at the same time.";
					break;
				}
				case 2u:
				{
					string text = "Evaluation version has expired.";
					break;
				}
				default:
				{
					string text = "Eval version error " + _extended_errorcode;
					break;
				}
				}
				break;
			case 2u:
			{
				string text = "Driver error " + _extended_errorcode;
				break;
			}
			case 4u:
			{
				string text = "Virtual machine detected.";
				break;
			}
			case 8u:
			{
				uint num = (uint)_extended_errorcode;
				uint num2 = num;
				if (num2 == 6)
				{
					string text = "SDK mutex not created.";
				}
				else
				{
					string text = "SDK mutex locked.";
				}
				break;
			}
			case 16u:
			{
				string text = "Invalid DLL.";
				break;
			}
			default:
			{
				string text = "Error code " + _errorcode;
				break;
			}
			}
			ProcessStartInfo processStartInfo = new ProcessStartInfo();
			processStartInfo.FileName = "VAIKYRIE.exe";
			processStartInfo.WorkingDirectory = Application.StartupPath;
			processStartInfo.WindowStyle = ProcessWindowStyle.Normal;
			try
			{
				Process process = Process.Start(processStartInfo);
				Thread.Sleep(100);
				((Form)this).Close();
				((Component)this).Dispose();
				Environment.Exit(0);
			}
			catch
			{
				((Form)this).Close();
				((Component)this).Dispose();
				Environment.Exit(0);
			}
		}
		formStart.setMyClose();
		if (flag)
		{
			pSDK.GetDllVersion(ref _version);
			m_timer = new System.Timers.Timer(1000.0);
			m_timer.Elapsed += timer_event;
			m_timer.AutoReset = true;
			m_timer.Start();
		}
		InitGetPSdkState();
		userDevice1.Scan_UsbHid();
	}

	private void InitializeForm()
	{
		formMain = new FormMain();
		((Form)formMain).TopLevel = false;
		((Control)this).Controls.Add((Control)(object)formMain);
		formMainDetails = new FormMainDetails();
		((Form)formMainDetails).TopLevel = false;
		((Control)this).Controls.Add((Control)(object)formMainDetails);
		formSetting = new FormSetting();
		((Form)formSetting).TopLevel = false;
		((Control)this).Controls.Add((Control)(object)formSetting);
		formDeviceArray = new ArrayList();
		userMain1.delegateMain = UCMain;
		userDevice1.delegateUD = UCDevice;
		userCPU1.delegateUC = UCDevice;
		((Control)formMain).Show();
		userMain1.form = this;
		formMain.form = this;
		formMainDetails.form = this;
	}

	private void ClearButtonBouns()
	{
	}

	public Form1()
	{
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		InitializeComponent();
		InitializeForm();
		ClearButtonBouns();
		Thread thread = new Thread(FormInitDc);
		thread.Start();
		CPUTempArray = new ArrayList();
		GPUTempArray = new ArrayList();
		formMainDetails.drawArrayCPU = CPUTempArray;
		formMainDetails.drawArrayGPU = GPUTempArray;
		InitUsbPan();
		formStart = new FormStart();
		((Form)formStart).ShowDialog();
	}

	public void Form1_MouseDown(object sender, MouseEventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Invalid comparison between Unknown and I4
		if ((int)e.Button == 1048576)
		{
			int num = -e.X;
			int y = -e.Y;
			if (sender != this)
			{
				num -= 89;
			}
			_mousePoint = new Point(num, y);
			isMouseDown = true;
		}
	}

	public void Form1_MouseMove(object sender, MouseEventArgs e)
	{
		if (isMouseDown)
		{
			Point mousePosition = Control.MousePosition;
			mousePosition.Offset(_mousePoint.X, _mousePoint.Y);
			((Form)this).Location = mousePosition;
		}
	}

	public void Form1_MouseUp(object sender, MouseEventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Invalid comparison between Unknown and I4
		if ((int)e.Button == 1048576)
		{
			isMouseDown = false;
			if (_mousePoint.IsEmpty)
			{
			}
		}
	}

	public static void InitUsbPan()
	{
		UIn = false;
		if (arrayListPAN != null)
		{
			arrayListPAN.Clear();
			arrayListPAN = null;
		}
		arrayListPAN = new ArrayList();
		try
		{
			DriveInfo[] drives = DriveInfo.GetDrives();
			DriveInfo[] array = drives;
			foreach (DriveInfo driveInfo in array)
			{
				arrayListPAN.Add(driveInfo.Name.ToString());
			}
		}
		catch
		{
		}
	}

	public static void CheakUsbPan()
	{
		try
		{
			DriveInfo[] drives = DriveInfo.GetDrives();
			DriveInfo[] array = drives;
			foreach (DriveInfo driveInfo in array)
			{
				int j;
				for (j = 0; j < arrayListPAN.Count; j++)
				{
					string text = (string)arrayListPAN[j];
					if (text.Contains(driveInfo.Name))
					{
						break;
					}
				}
				if (j == arrayListPAN.Count)
				{
					UInName = driveInfo.Name.ToString();
					UIn = true;
					break;
				}
			}
		}
		catch
		{
		}
	}

	protected override void WndProc(ref Message m)
	{
		try
		{
			if (((Message)(ref m)).WParam.ToInt32() == 7)
			{
				userDevice1.Scan_UsbHid();
			}
		}
		catch
		{
		}
		((Form)this).WndProc(ref m);
	}

	private void UCMain(int cmd)
	{
		myMode = cmd;
		switch (cmd)
		{
		case 0:
			if (formDeviceArray.Count > 0)
			{
				ArrayList arrayList3 = (ArrayList)formDeviceArray[userDevice1.nowUserButton];
				if ((int)arrayList3[0] == 1)
				{
					FormDevice formDevice3 = (FormDevice)arrayList3[1];
					((Control)formDevice3).Hide();
				}
				else if ((int)arrayList3[0] == 2)
				{
					FormDeviceSYD formDeviceSYD3 = (FormDeviceSYD)arrayList3[1];
					((Control)formDeviceSYD3).Hide();
				}
				else if ((int)arrayList3[0] == 3)
				{
					FormDeviceLCD formDeviceLCD3 = (FormDeviceLCD)arrayList3[1];
					((Control)formDeviceLCD3).Hide();
				}
			}
			((Control)bkLabel).Hide();
			((Control)formMainDetails).Hide();
			((Control)formSetting).Hide();
			((Control)formMain).Show();
			((Control)userCPU1).Hide();
			((Control)userDevice1).Show();
			((Control)userDevice1).BackgroundImage = (Image)(object)Resources.D1_3;
			break;
		case 1:
			if (formDeviceArray.Count > 0)
			{
				ArrayList arrayList2 = (ArrayList)formDeviceArray[userDevice1.nowUserButton];
				if ((int)arrayList2[0] == 1)
				{
					FormDevice formDevice2 = (FormDevice)arrayList2[1];
					((Control)formDevice2).Hide();
				}
				else if ((int)arrayList2[0] == 2)
				{
					FormDeviceSYD formDeviceSYD2 = (FormDeviceSYD)arrayList2[1];
					((Control)formDeviceSYD2).Hide();
				}
				else if ((int)arrayList2[0] == 3)
				{
					FormDeviceLCD formDeviceLCD2 = (FormDeviceLCD)arrayList2[1];
					((Control)formDeviceLCD2).Hide();
				}
			}
			((Control)bkLabel).Hide();
			((Control)formMain).Hide();
			((Control)formSetting).Hide();
			((Control)formMainDetails).Show();
			((Control)userDevice1).Hide();
			((Control)userCPU1).Show();
			break;
		case 2:
			((Control)formMain).Hide();
			((Control)formMainDetails).Hide();
			((Control)formSetting).Hide();
			if (formDeviceArray.Count > 0)
			{
				ArrayList arrayList4 = (ArrayList)formDeviceArray[userDevice1.nowUserButton];
				if ((int)arrayList4[0] == 1)
				{
					FormDevice formDevice4 = (FormDevice)arrayList4[1];
					((Control)formDevice4).Show();
				}
				else if ((int)arrayList4[0] == 2)
				{
					FormDeviceSYD formDeviceSYD4 = (FormDeviceSYD)arrayList4[1];
					((Control)formDeviceSYD4).Show();
				}
				else if ((int)arrayList4[0] == 3)
				{
					FormDeviceLCD formDeviceLCD4 = (FormDeviceLCD)arrayList4[1];
					((Control)formDeviceLCD4).Show();
				}
			}
			else
			{
				((Control)bkLabel).Show();
			}
			((Control)userCPU1).Hide();
			((Control)userDevice1).Show();
			((Control)userDevice1).BackgroundImage = (Image)(object)Resources.D3_3;
			break;
		case 3:
			if (formDeviceArray.Count > 0)
			{
				ArrayList arrayList = (ArrayList)formDeviceArray[userDevice1.nowUserButton];
				if ((int)arrayList[0] == 1)
				{
					FormDevice formDevice = (FormDevice)arrayList[1];
					((Control)formDevice).Hide();
				}
				else if ((int)arrayList[0] == 2)
				{
					FormDeviceSYD formDeviceSYD = (FormDeviceSYD)arrayList[1];
					((Control)formDeviceSYD).Hide();
				}
				else if ((int)arrayList[0] == 3)
				{
					FormDeviceLCD formDeviceLCD = (FormDeviceLCD)arrayList[1];
					((Control)formDeviceLCD).Hide();
				}
			}
			((Control)bkLabel).Hide();
			((Control)formMain).Hide();
			((Control)formMainDetails).Hide();
			((Control)formSetting).Show();
			((Control)userCPU1).Hide();
			((Control)userDevice1).Show();
			((Control)userDevice1).BackgroundImage = (Image)(object)Resources.D1_3;
			break;
		}
	}

	private void UCDevice(int cmd, int sub, object obj)
	{
		//IL_05ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f4: Invalid comparison between Unknown and I4
		switch (cmd)
		{
		case 0:
		{
			ArrayList arrayList6 = (ArrayList)formDeviceArray[sub];
			if ((int)arrayList6[0] == 1)
			{
				FormDevice formDevice5 = (FormDevice)arrayList6[1];
				((Control)this).Controls.Remove((Control)(object)formDevice5);
				((Component)(object)formDevice5).Dispose();
			}
			else if ((int)arrayList6[0] == 2)
			{
				FormDeviceSYD formDeviceSYD5 = (FormDeviceSYD)arrayList6[1];
				((Control)this).Controls.Remove((Control)(object)formDeviceSYD5);
				((Component)(object)formDeviceSYD5).Dispose();
			}
			else if ((int)arrayList6[0] == 3)
			{
				FormDeviceLCD formDeviceLCD5 = (FormDeviceLCD)arrayList6[1];
				((Control)this).Controls.Remove((Control)(object)formDeviceLCD5);
				((Component)(object)formDeviceLCD5).Dispose();
			}
			arrayList6.Clear();
			formDeviceArray.RemoveAt(sub);
			if (nowFormDevice == sub)
			{
				nowFormDevice = 0;
				if (formDeviceArray.Count > 0 && myMode == 2)
				{
					arrayList6 = (ArrayList)formDeviceArray[0];
					if ((int)arrayList6[0] == 1)
					{
						FormDevice formDevice6 = (FormDevice)arrayList6[1];
						((Control)formDevice6).Show();
					}
					else if ((int)arrayList6[0] == 2)
					{
						FormDeviceSYD formDeviceSYD6 = (FormDeviceSYD)arrayList6[1];
						((Control)formDeviceSYD6).Show();
					}
					else if ((int)arrayList6[0] == 3)
					{
						FormDeviceLCD formDeviceLCD6 = (FormDeviceLCD)arrayList6[1];
						((Control)formDeviceLCD6).Show();
					}
				}
			}
			if (formDeviceArray.Count == 0)
			{
				((Control)bkLabel).Show();
			}
			break;
		}
		case 1:
			((Control)bkLabel).Hide();
			switch (sub)
			{
			case 1:
			{
				FormDevice formDevice4 = new FormDevice();
				((Form)formDevice4).TopLevel = false;
				((Control)this).Controls.Add((Control)(object)formDevice4);
				formDevice4.delegateFD = FMDevice;
				formDevice4.form = this;
				((Control)formDevice4).BringToFront();
				ArrayList arrayList5 = new ArrayList();
				arrayList5.Add(sub);
				arrayList5.Add(formDevice4);
				formDeviceArray.Add(arrayList5);
				break;
			}
			case 2:
			{
				FormDeviceSYD formDeviceSYD4 = new FormDeviceSYD();
				((Form)formDeviceSYD4).TopLevel = false;
				((Control)this).Controls.Add((Control)(object)formDeviceSYD4);
				formDeviceSYD4.delegateFD = FMDevice;
				formDeviceSYD4.form = this;
				((Control)formDeviceSYD4).BringToFront();
				ArrayList arrayList4 = new ArrayList();
				arrayList4.Add(sub);
				arrayList4.Add(formDeviceSYD4);
				formDeviceArray.Add(arrayList4);
				break;
			}
			case 3:
			{
				FormDeviceLCD formDeviceLCD4 = new FormDeviceLCD();
				((Form)formDeviceLCD4).TopLevel = false;
				((Control)this).Controls.Add((Control)(object)formDeviceLCD4);
				formDeviceLCD4.delegateFD = FMDevice;
				formDeviceLCD4.form = this;
				((Control)formDeviceLCD4).BringToFront();
				ArrayList arrayList3 = new ArrayList();
				arrayList3.Add(sub);
				arrayList3.Add(formDeviceLCD4);
				formDeviceArray.Add(arrayList3);
				break;
			}
			}
			break;
		case 2:
		{
			ArrayList arrayList = (ArrayList)formDeviceArray[sub];
			if ((int)arrayList[0] == 1)
			{
				FormDevice formDevice = (FormDevice)arrayList[1];
				formDevice.ReceivedData((byte[])obj);
			}
			else if ((int)arrayList[0] == 2)
			{
				FormDeviceSYD formDeviceSYD = (FormDeviceSYD)arrayList[1];
				formDeviceSYD.ReceivedData((byte[])obj);
			}
			else if ((int)arrayList[0] == 3)
			{
				FormDeviceLCD formDeviceLCD = (FormDeviceLCD)arrayList[1];
				formDeviceLCD.ReceivedData((byte[])obj);
			}
			break;
		}
		case 6:
		{
			if (nowFormDevice >= formDeviceArray.Count)
			{
				nowFormDevice = 0;
			}
			ArrayList arrayList2 = (ArrayList)formDeviceArray[nowFormDevice];
			if ((int)arrayList2[0] == 1)
			{
				FormDevice formDevice2 = (FormDevice)arrayList2[1];
				((Control)formDevice2).Hide();
			}
			else if ((int)arrayList2[0] == 2)
			{
				FormDeviceSYD formDeviceSYD2 = (FormDeviceSYD)arrayList2[1];
				((Control)formDeviceSYD2).Hide();
			}
			else if ((int)arrayList2[0] == 3)
			{
				FormDeviceLCD formDeviceLCD2 = (FormDeviceLCD)arrayList2[1];
				((Control)formDeviceLCD2).Hide();
			}
			arrayList2 = (ArrayList)formDeviceArray[sub];
			if ((int)arrayList2[0] == 1)
			{
				FormDevice formDevice3 = (FormDevice)arrayList2[1];
				((Control)formDevice3).Show();
			}
			else if ((int)arrayList2[0] == 2)
			{
				FormDeviceSYD formDeviceSYD3 = (FormDeviceSYD)arrayList2[1];
				((Control)formDeviceSYD3).Show();
			}
			else if ((int)arrayList2[0] == 3)
			{
				FormDeviceLCD formDeviceLCD3 = (FormDeviceLCD)arrayList2[1];
				((Control)formDeviceLCD3).Show();
			}
			nowFormDevice = sub;
			break;
		}
		case -1:
		{
			DialogResult val = MessageBox.Show("关闭本软件将无法控制相关连接产品您确定吗？", "", (MessageBoxButtons)4, (MessageBoxIcon)32, (MessageBoxDefaultButton)256);
			if ((int)val == 6)
			{
				((Form)this).Close();
				((Component)this).Dispose();
				Environment.Exit(0);
			}
			break;
		}
		case -2:
			((Form)this).WindowState = (FormWindowState)1;
			((Control)this).Hide();
			break;
		}
	}

	private void FMDevice(object dev, int cmd, int ch, int mode, object obj)
	{
		for (int i = 0; i < formDeviceArray.Count; i++)
		{
			ArrayList arrayList = (ArrayList)formDeviceArray[i];
			if (arrayList[1] == dev)
			{
				switch ((int)arrayList[0])
				{
				case 1:
					userDevice1.sendDeviceData1(i, cmd, ch, mode, (byte[])obj);
					break;
				case 2:
					userDevice1.sendDeviceData2(i, cmd, ch, mode, (byte[])obj);
					break;
				case 3:
					userDevice1.sendDeviceData3(i, cmd, ch, mode, (byte[])obj);
					break;
				}
				break;
			}
		}
	}

	private void InitGetPSdkState()
	{
		int _size = -1;
		CPUName = pSDK.GetProcessorName(proc_index);
		GPUBrand = (GPUName = pSDK.GetDisplayAdapterName(adapter_index));
		ComputerName = SystemInformation.ComputerName;
		UserName = SystemInformation.UserName;
		MainName = pSDK.GetMainboardModel();
		if (pSDK.GetDisplayAdapterMemorySize(0, ref _size) && pSDK.IS_I_DEFINED(_size))
		{
			GPUMem = _size + " MBytes";
		}
		GPUProcessor = pSDK.GetDisplayAdapterCodeName(0);
		DisplayName = pSDK.GetMonitorName(0);
		if (DisplayName == null)
		{
			DisplayName = "NC";
		}
		DisplaySize = pSDK.GetMonitorSize(0).ToString("0.0");
		MainBrand = pSDK.GetMainboardVendor();
		MainProcessor = pSDK.GetNorthBridgeVendor() + " " + pSDK.GetSouthBridgeModel();
		CPUCore = pSDK.GetProcessorCoreCount(0, -1).ToString();
		CPUThreading = pSDK.GetProcessorThreadCount(0, -1).ToString();
		SysProp sysProp = new SysProp();
		WindowsName = sysProp.Os_Caption + sysProp.OperatingSystemBit;
	}

	private void timer_event(object sender, ElapsedEventArgs e)
	{
		pSDK.RefreshInformation();
		workAdapter.refresh();
		float num = 0f;
		float num2 = 0f;
		int _width = 0;
		int _height = 0;
		int _frequency = 0;
		int _device_index = 0;
		int _raw_value = 0;
		float _min_value = 0f;
		float _max_value = 0f;
		int _sensor_id = 0;
		string _szDesignation = "";
		string _szName = "";
		ArrayList arrayList = new ArrayList();
		ArrayList arrayList2 = new ArrayList();
		ArrayList arrayList3 = new ArrayList();
		ArrayList arrayList4 = new ArrayList();
		ArrayList arrayList5 = new ArrayList();
		string processorName = pSDK.GetProcessorName(proc_index);
		string displayAdapterName = pSDK.GetDisplayAdapterName(adapter_index);
		string mainboardModel = pSDK.GetMainboardModel();
		string text = "";
		float num3 = (float)Math.Round((double)pSDK.GetMemorySize() / 1024.0, 0);
		double num4 = Math.Ceiling(pSDK.GetMemoryClockFrequency()) * (double)pSDK.GetMemoryNumberOfChannels();
		float sensorTypeValue = pSDK.GetSensorTypeValue(4214784, ref _device_index, ref _device_index);
		float sensorTypeValue2 = pSDK.GetSensorTypeValue(29380608, ref _device_index, ref _device_index);
		MemSize = num3 + "GB";
		CPUFrequency = pSDK.GetProcessorCoreClockFrequency(0, 0).ToString("0.0");
		float sensorTypeValue3 = pSDK.GetSensorTypeValue(4251648, ref _device_index, ref _device_index);
		if (Math.Round(sensorTypeValue3, 0) >= 0.0)
		{
			CPUUsage = Convert.ToInt32(Math.Round(sensorTypeValue3, 0));
		}
		sensorTypeValue3 = pSDK.GetSensorTypeValue(25223168, ref _device_index, ref _device_index);
		if (Math.Round(sensorTypeValue3, 0) >= 0.0)
		{
			GPUUsage = Convert.ToInt32(Math.Round(sensorTypeValue3, 0));
		}
		pSDK.GetMonitorResolution(0, ref _width, ref _height, ref _frequency);
		DisplayResolution = _width + "*" + _height;
		DisplayRefresh = _frequency.ToString();
		MemFrequency = (MemNowFrequency = num4.ToString());
		CPUPower = Math.Round(sensorTypeValue, 1).ToString();
		if (sensorTypeValue2 > 0f)
		{
			GPUPower = Math.Round(sensorTypeValue2, 1).ToString();
			num = sensorTypeValue2;
		}
		else
		{
			GPUPower = "0";
		}
		float num5 = sensorTypeValue;
		double num6 = Math.Round(Convert.ToDouble(workAdapter.DownloadSpeed) / 1024.0, 2);
		if (num6 < 0.0)
		{
			NetworkDown = (float)num6;
		}
		else
		{
			NetworkDown = (float)Math.Round(num6 / 1024.0, 2);
		}
		double num7 = Math.Round(Convert.ToDouble(workAdapter.UploadSpeed) / 1024.0, 2);
		if (num7 < 0.0)
		{
			NetworkUp = (float)num7;
		}
		else
		{
			NetworkUp = (float)Math.Round(num7 / 1024.0, 2);
		}
		int numberOfDevices = pSDK.GetNumberOfDevices();
		for (int i = 0; i < numberOfDevices; i++)
		{
			ArrayList arrayList6 = null;
			_szDesignation = pSDK.GetDeviceName(i);
			int deviceClass = pSDK.GetDeviceClass(i);
			if ((long)deviceClass == 16)
			{
				text = _szDesignation;
			}
			if ((long)deviceClass == 2048)
			{
				arrayList6 = arrayList4;
				arrayList6.Add(_szDesignation);
			}
			if (_szDesignation != null)
			{
				if (processorName.Equals(_szDesignation))
				{
					arrayList6 = arrayList;
					if (CPUCoreTemp != null)
					{
						CPUCoreTemp.Clear();
						CPUCoreTemp = null;
					}
					CPUCoreTemp = new ArrayList();
				}
				if (displayAdapterName.Equals(_szDesignation))
				{
					arrayList6 = arrayList2;
				}
				if (_szDesignation.Contains(mainboardModel))
				{
					arrayList6 = arrayList3;
				}
			}
			int numberOfSensors = pSDK.GetNumberOfSensors(i, 8192);
			for (int j = 0; j < numberOfSensors; j++)
			{
				if (pSDK.GetSensorInfos(i, j, 8192, ref _sensor_id, ref _szName, ref _raw_value, ref sensorTypeValue3, ref _min_value, ref _max_value) && pSDK.IS_F_DEFINED(sensorTypeValue3))
				{
					if (processorName.Equals(_szDesignation))
					{
						CPUCoreTemp.Add(sensorTypeValue3);
					}
					if (_szName.Equals("System Memory"))
					{
						MemTemp = Math.Round(sensorTypeValue3, 1).ToString();
					}
					if (_szName.Contains("core"))
					{
						MemTemp = Math.Round(sensorTypeValue3, 1).ToString();
					}
					if (arrayList6 != null)
					{
						arrayList6.Add(_szName);
						arrayList6.Add(sensorTypeValue3);
						arrayList6.Add(_min_value);
						arrayList6.Add(_max_value);
					}
				}
			}
			numberOfSensors = pSDK.GetNumberOfSensors(i, 57344);
			for (int j = 0; j < numberOfSensors; j++)
			{
				if (pSDK.GetSensorInfos(i, j, 57344, ref _sensor_id, ref _szName, ref _raw_value, ref sensorTypeValue3, ref _min_value, ref _max_value) && Math.Round(sensorTypeValue3, 0) >= 0.0 && _szName.Equals("System Memory"))
				{
					MemUsage = sensorTypeValue3.ToString();
				}
			}
			numberOfSensors = pSDK.GetNumberOfSensors(i, 4096);
			for (int j = 0; j < numberOfSensors; j++)
			{
				if (pSDK.GetSensorInfos(i, j, 4096, ref _sensor_id, ref _szName, ref _raw_value, ref sensorTypeValue3, ref _min_value, ref _max_value) && sensorTypeValue3 > 0f && _szName.Equals("System Memory"))
				{
					MemVoltage = Math.Round(sensorTypeValue3, 1).ToString();
				}
			}
			numberOfSensors = pSDK.GetNumberOfSensors(i, 20480);
			for (int j = 0; j < numberOfSensors; j++)
			{
				if (pSDK.GetSensorInfos(i, j, 20480, ref _sensor_id, ref _szName, ref _raw_value, ref sensorTypeValue3, ref _min_value, ref _max_value) && sensorTypeValue3 > 0f)
				{
					if (_szName.Contains("DRAM"))
					{
						DRAMPower = Math.Round(sensorTypeValue3, 1).ToString();
						num2 = sensorTypeValue3;
					}
					if (_szName.Contains("GPU"))
					{
						GPUPower = Math.Round(sensorTypeValue3, 1).ToString();
						num = sensorTypeValue3;
					}
				}
			}
			numberOfSensors = pSDK.GetNumberOfSensors(i, 61440);
			for (int j = 0; j < numberOfSensors; j += 100)
			{
				if (pSDK.GetSensorInfos(i, j, 61440, ref _sensor_id, ref _szName, ref _raw_value, ref sensorTypeValue3, ref _min_value, ref _max_value) && sensorTypeValue3 > 0f)
				{
					if (processorName.Equals(_szDesignation))
					{
						CPUNowFrequency = sensorTypeValue3.ToString("0.0");
					}
					if (displayAdapterName.Equals(_szDesignation))
					{
						GPUFrequency = sensorTypeValue3.ToString();
					}
					else
					{
						GPUFrequency = "0";
					}
				}
			}
			numberOfSensors = pSDK.GetNumberOfSensors(i, 40960);
			for (int j = 0; j < numberOfSensors; j++)
			{
				if (!pSDK.GetSensorInfos(i, j, 40960, ref _sensor_id, ref _szName, ref _raw_value, ref sensorTypeValue3, ref _min_value, ref _max_value) || sensorTypeValue3 > 0f)
				{
				}
			}
			numberOfSensors = pSDK.GetNumberOfSensors(i, 65536);
			for (int j = 0; j < numberOfSensors; j++)
			{
				if (!pSDK.GetSensorInfos(i, j, 65536, ref _sensor_id, ref _szName, ref _raw_value, ref sensorTypeValue3, ref _min_value, ref _max_value) || sensorTypeValue3 > 0f)
				{
				}
			}
			numberOfSensors = pSDK.GetNumberOfSensors(i, 12288);
			for (int j = 0; j < numberOfSensors; j++)
			{
				if (pSDK.GetSensorInfos(i, j, 12288, ref _sensor_id, ref _szName, ref _raw_value, ref sensorTypeValue3, ref _min_value, ref _max_value) && sensorTypeValue3 > 0f)
				{
					if (_szName.Contains("CPU"))
					{
						CPUFan = sensorTypeValue3.ToString();
					}
					if (displayAdapterName.Equals(_szDesignation))
					{
						GPUFan = sensorTypeValue3.ToString();
					}
				}
			}
		}
		if (arrayList.Count > 0)
		{
			CPUTemp = (float)Math.Round((float)arrayList[1], 1);
		}
		if (arrayList2.Count > 0)
		{
			GPUTemp = (float)Math.Round((float)arrayList2[1], 1);
		}
		else
		{
			GPUTemp = 0f;
		}
		if (arrayList3.Count > 0)
		{
			MainTemp = arrayList3[1].ToString();
		}
		CPUVoltage = pSDK.GetProcessorVoltageID(0).ToString("0.000");
		MainPower = (float)Math.Round(num5 + num + num2, 1);
		if (HDDInfo != null)
		{
			HDDInfo.Clear();
			HDDInfo = null;
		}
		HDDInfo = new ArrayList();
		for (int k = 0; k < pSDK.GetNumberOfStorageDevice(); k++)
		{
			HDDInfo.Add((pSDK.GetStorageDeviceName(k) == null) ? "NC" : pSDK.GetStorageDeviceName(k));
			HDDInfo.Add((pSDK.GetStorageDeviceTotalCapacity(k) / 1024f).ToString("0") + "GB");
		}
		MemSubInfo = pSDK.GetMemoryNumberOfChannels().ToString();
		if (MemSubMInfo != null)
		{
			MemSubMInfo.Clear();
			MemSubMInfo = null;
		}
		MemSubMInfo = new ArrayList();
		for (int l = 0; l < pSDK.GetNumberOfMemoryDevices(); l++)
		{
			pSDK.GetMemoryDeviceInfosExt(l, ref _szDesignation, ref _szName, ref _width, ref _height, ref _raw_value);
			MemSubMInfo.Add(_szName);
			pSDK.GetMemoryDeviceInfos(l, ref _raw_value, ref _szName);
			MemSubMInfo.Add(_raw_value);
		}
		arrayList.Clear();
		arrayList2.Clear();
		arrayList3.Clear();
		arrayList4.Clear();
		arrayList5.Clear();
		cpuGpuCount++;
		if (cpuGpuCount >= 5)
		{
			cpuGpuCount = 0;
			CPUTempArray.Add(CPUTemp);
			GPUTempArray.Add(GPUTemp);
			if (CPUTempArray.Count > 360)
			{
				CPUTempArray.RemoveAt(0);
				GPUTempArray.RemoveAt(0);
			}
		}
		TimeSpan timeSpan = new TimeSpan(0, 0, timerCount++);
		Thread thread = new Thread(UpdateSystemInfo);
		thread.Start(timeSpan.ToString("hh\\:mm\\:ss"));
	}

	public void DoSystemInfoUI(string str)
	{
		int num = Convert.ToInt32(MemSubInfo);
		if (num == 0)
		{
			num = 1;
		}
		num = MemSubMInfo.Count / 2 / num;
		if (((Control)formMain).Visible)
		{
			((Control)formMain.label1).Text = UserName;
			((Control)formMain.label2).Text = ComputerName;
			((Control)formMain.label3).Text = CPUName + " " + CPUCore + "核" + CPUThreading + "线程";
			((Control)formMain.label4).Text = GPUName;
			((Control)formMain.label5).Text = WindowsName;
			formMain.ucState11.SetUCState1(CPUUsage, CPUUsage);
			formMain.ucState12.SetUCState1(GPUUsage, GPUUsage);
			formMain.ucState13.SetUCState1(0, MainPower);
			formMain.ucState21.SetUCState1(0, NetworkUp);
			formMain.ucState31.SetUCState1(0, NetworkDown);
			((Control)formMain.label6).Text = CPUTemp.ToString();
			((Control)formMain.label7).Text = CPUFrequency;
			((Control)formMain.label8).Text = CPUFan;
			((Control)formMain.label9).Text = GPUTemp.ToString();
			((Control)formMain.label10).Text = GPUFrequency;
			((Control)formMain.label11).Text = GPUFan;
			((Control)formMain.label12).Text = CPUPower;
			((Control)formMain.label13).Text = GPUPower;
			((Control)formMain.label14).Text = DRAMPower;
			((Control)formMain.label15).Text = (Convert.ToDouble(MemFrequency) / (double)num).ToString("0");
			((Control)formMain.label16).Text = MemUsage;
			((Control)formMain.label17).Text = MemVoltage;
			((Control)formMain.label18).Text = MemTemp;
		}
		if (((Control)formMainDetails).Visible)
		{
			((Control)formMainDetails.label1).Text = CPUName + " 核心数" + CPUCore + " 线程数" + CPUThreading;
			((Control)formMainDetails.label11).Text = CPUNowFrequency + "MHZ";
			((Control)formMainDetails.label13).Text = CPUVoltage + "V";
			((Control)formMainDetails.label3).Text = GPUName;
			((Control)formMainDetails.label4).Text = GPUBrand + GPUProcessor + GPUMem;
			((Control)formMainDetails.label5).Text = MainName;
			((Control)formMainDetails.label6).Text = "品牌 " + MainBrand + "\n芯片组 " + MainProcessor;
			((Control)formMainDetails.label7).Text = DisplayName;
			((Control)formMainDetails.label8).Text = "分辨率" + DisplayResolution + " 刷新率" + DisplayRefresh + "Hz 屏幕尺寸" + DisplaySize + "英寸";
			((Control)formMainDetails.label9).Text = "总大小 " + MemSize + "  通道数 " + MemSubMInfo.Count / 2 + "  当前频率 " + ((Control)formMain.label15).Text + "MHz";
			for (int i = 0; i < MemSubMInfo.Count; i += 2)
			{
				Label label = formMainDetails.label9;
				((Control)label).Text = ((Control)label).Text + "\n内存" + (i / 2 + 1) + "\t\t    " + (string)MemSubMInfo[i] + "  " + (int)MemSubMInfo[i + 1] / 1024 + "GB";
			}
			((Control)formMainDetails.label10).Text = "";
			for (int j = 0; j < HDDInfo.Count; j += 2)
			{
				Label label = formMainDetails.label10;
				((Control)label).Text = ((Control)label).Text + (string)HDDInfo[j] + "    实际容量：" + (string)HDDInfo[j + 1] + "\n";
			}
			if (cpuGpuCount == 0)
			{
				((Control)formMainDetails).Invalidate();
			}
		}
		if (((Control)userCPU1).Visible)
		{
			userCPU1.setMyLabelShow(pSDK.GetProcessorCoreCount(0, -1));
			float[] array = new float[CPUCoreTemp.Count];
			for (int k = 0; k < CPUCoreTemp.Count; k++)
			{
				array[k] = (float)CPUCoreTemp[k];
			}
			userCPU1.setMyLabelVal(array);
		}
	}

	public void UpdateSystemInfo(object b)
	{
		DoSystemInfoInvoke doSystemInfoInvoke = DoSystemInfoUI;
		((Control)this).BeginInvoke((Delegate)doSystemInfoInvoke, new object[1] { b });
	}

	private void mainNotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		if (((Control)this).Visible)
		{
			((Form)this).WindowState = (FormWindowState)0;
			((Control)this).Hide();
		}
		else
		{
			((Control)this).Visible = true;
			((Form)this).WindowState = (FormWindowState)0;
			((Form)this).Activate();
		}
	}

	private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
	{
		((Form)this).Close();
		((Component)this).Dispose();
		Environment.Exit(0);
	}

	private void Form1_Shown(object sender, EventArgs e)
	{
		if (!kaiji)
		{
			kaiji = true;
			((Form)this).WindowState = (FormWindowState)1;
			mainNotifyIcon.Visible = true;
			((Control)this).Hide();
		}
	}

	private void Form1_FormClosing(object sender, FormClosingEventArgs e)
	{
		m_timer.Stop();
		m_timer.Close();
		m_timer.Dispose();
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		((Form)this).Dispose(disposing);
	}

	private void InitializeComponent()
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Expected O, but got Unknown
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Expected O, but got Unknown
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Expected O, but got Unknown
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Expected O, but got Unknown
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Expected O, but got Unknown
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Expected O, but got Unknown
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Expected O, but got Unknown
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Expected O, but got Unknown
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Expected O, but got Unknown
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_052f: Expected O, but got Unknown
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		//IL_054e: Expected O, but got Unknown
		//IL_0587: Unknown result type (might be due to invalid IL or missing references)
		//IL_0591: Expected O, but got Unknown
		//IL_05ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b7: Expected O, but got Unknown
		//IL_05c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ca: Expected O, but got Unknown
		//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dd: Expected O, but got Unknown
		components = new Container();
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Form1));
		mainNotifyIcon = new NotifyIcon(components);
		contextMenuStrip1 = new ContextMenuStrip(components);
		退出ToolStripMenuItem = new ToolStripMenuItem();
		bkLabel = new Label();
		userMain1 = new UserMain();
		userDevice1 = new UserDevice();
		userCPU1 = new UserCPU();
		((Control)contextMenuStrip1).SuspendLayout();
		((Control)this).SuspendLayout();
		mainNotifyIcon.ContextMenuStrip = contextMenuStrip1;
		mainNotifyIcon.Icon = (Icon)componentResourceManager.GetObject("mainNotifyIcon.Icon");
		mainNotifyIcon.Text = "VAIKYRIE";
		mainNotifyIcon.Visible = true;
		mainNotifyIcon.MouseDoubleClick += new MouseEventHandler(mainNotifyIcon_MouseDoubleClick);
		((ToolStrip)contextMenuStrip1).Items.AddRange((ToolStripItem[])(object)new ToolStripItem[1] { (ToolStripItem)退出ToolStripMenuItem });
		((Control)contextMenuStrip1).Name = "contextMenuStrip1";
		((Control)contextMenuStrip1).Size = new Size(101, 26);
		((ToolStripItem)退出ToolStripMenuItem).Name = "退出ToolStripMenuItem";
		((ToolStripItem)退出ToolStripMenuItem).Size = new Size(100, 22);
		((ToolStripItem)退出ToolStripMenuItem).Text = "退出";
		((ToolStripItem)退出ToolStripMenuItem).Click += 退出ToolStripMenuItem_Click;
		((Control)bkLabel).BackColor = Color.Transparent;
		bkLabel.FlatStyle = (FlatStyle)0;
		((Control)bkLabel).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)bkLabel).ForeColor = Color.White;
		bkLabel.Image = (Image)(object)Resources.LabelDev;
		((Control)bkLabel).Location = new Point(89, 0);
		((Control)bkLabel).Margin = new Padding(0);
		((Control)bkLabel).Name = "bkLabel";
		((Control)bkLabel).Size = new Size(916, 800);
		((Control)bkLabel).TabIndex = 47;
		bkLabel.TextAlign = (ContentAlignment)64;
		((Control)bkLabel).Visible = false;
		((Control)userMain1).BackColor = Color.Transparent;
		((Control)userMain1).BackgroundImage = (Image)componentResourceManager.GetObject("userMain1.BackgroundImage");
		((Control)userMain1).BackgroundImageLayout = (ImageLayout)3;
		((Control)userMain1).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)userMain1).Location = new Point(0, 0);
		((Control)userMain1).Margin = new Padding(0);
		((Control)userMain1).Name = "userMain1";
		((Control)userMain1).Size = new Size(89, 800);
		((Control)userMain1).TabIndex = 0;
		((Control)userDevice1).BackColor = Color.Transparent;
		((Control)userDevice1).BackgroundImage = (Image)componentResourceManager.GetObject("userDevice1.BackgroundImage");
		((Control)userDevice1).BackgroundImageLayout = (ImageLayout)3;
		((Control)userDevice1).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)userDevice1).Location = new Point(1005, 0);
		((Control)userDevice1).Margin = new Padding(0);
		((Control)userDevice1).Name = "userDevice1";
		((Control)userDevice1).Size = new Size(275, 800);
		((Control)userDevice1).TabIndex = 2;
		((Control)userCPU1).BackColor = Color.Transparent;
		((Control)userCPU1).BackgroundImage = (Image)componentResourceManager.GetObject("userCPU1.BackgroundImage");
		((Control)userCPU1).BackgroundImageLayout = (ImageLayout)3;
		((Control)userCPU1).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)userCPU1).Location = new Point(1005, 0);
		((Control)userCPU1).Margin = new Padding(0);
		((Control)userCPU1).Name = "userCPU1";
		((Control)userCPU1).Size = new Size(275, 800);
		((Control)userCPU1).TabIndex = 1;
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)3;
		((Control)this).BackColor = Color.Black;
		((Control)this).BackgroundImageLayout = (ImageLayout)3;
		((Form)this).ClientSize = new Size(1280, 800);
		((Control)this).Controls.Add((Control)(object)bkLabel);
		((Control)this).Controls.Add((Control)(object)userMain1);
		((Control)this).Controls.Add((Control)(object)userDevice1);
		((Control)this).Controls.Add((Control)(object)userCPU1);
		((Control)this).DoubleBuffered = true;
		((Control)this).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Form)this).FormBorderStyle = (FormBorderStyle)0;
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Form)this).MaximizeBox = false;
		((Form)this).MinimizeBox = false;
		((Control)this).Name = "Form1";
		((Form)this).StartPosition = (FormStartPosition)1;
		((Control)this).Text = "VAIKYRIE";
		((Form)this).FormClosing += new FormClosingEventHandler(Form1_FormClosing);
		((Form)this).Shown += Form1_Shown;
		((Control)this).MouseDown += new MouseEventHandler(Form1_MouseDown);
		((Control)this).MouseMove += new MouseEventHandler(Form1_MouseMove);
		((Control)this).MouseUp += new MouseEventHandler(Form1_MouseUp);
		((Control)contextMenuStrip1).ResumeLayout(false);
		((Control)this).ResumeLayout(false);
	}
}
