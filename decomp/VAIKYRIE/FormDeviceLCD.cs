using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using VAIKYRIE.Properties;

namespace VAIKYRIE;

public class FormDeviceLCD : Form
{
	public delegate void delegateFormDevice(object dev, int cmd, int ch = 0, int mode = 0, object obj = null);

	private const byte MainTemp = 0;

	private const byte CPUTemp = 1;

	private const byte GPUTemp = 2;

	private const byte MemTemp = 3;

	private const byte CPUFan = 16;

	private const byte GPUFan = 17;

	private const byte CPUFrequency = 32;

	private const byte GPUFrequency = 33;

	private const byte MemFrequency = 34;

	private const byte CPUVoltage = 48;

	private const byte MemVoltage = 49;

	private const byte TimerMode = 64;

	private const byte LCDMode = 96;

	private const byte LCDName = 97;

	private const byte LCDCommd = 98;

	private const byte LCDR0 = 99;

	private const byte LCDR90 = 100;

	private const byte LCDR180 = 101;

	private const byte LCDR270 = 102;

	private const byte LCDW = 103;

	private const byte LCDB = 104;

	private const byte LCDCommdD0 = 105;

	private const byte LCDCommdD1 = 106;

	private const int THeight1 = 61;

	private const int THeight2 = 63;

	private const int SendDelayTimer = 1000;

	private bool isOn = true;

	private string mcuName = "";

	public bool isTimer = false;

	public int myCh = 3;

	public int myTimer = 24;

	private bool isSendData = false;

	private int modeCount = 1;

	private bool isZYGLQ = false;

	public bool[] isTimerVal = new bool[5];

	public int[] myChVal = new int[5] { 3, 3, 3, 3, 3 };

	public int[] myTimerVal = new int[5] { 24, 24, 24, 24, 24 };

	public int myMode11 = 0;

	public int myMode12 = 1;

	public int myMode21 = 0;

	public int myMode22 = 1;

	public int myMode31 = 0;

	public int myMode32 = 1;

	public int modeCh = 0;

	public int[,] modeVal = new int[5, 3]
	{
		{ 1, 1, 1 },
		{ 1, 1, 1 },
		{ 1, 1, 1 },
		{ 1, 1, 1 },
		{ 1, 1, 1 }
	};

	private System.Timers.Timer m_timer = null;

	public delegateFormDevice delegateFD;

	public Form1 form;

	private IContainer components = null;

	private TextBox textBox1;

	private TextBox textBox2;

	private TextBox textBox3;

	private TextBox textBox4;

	private TextBox textBox5;

	private UCLCDDisplay1 uclcdDisplay11;

	private UCLCDDisplay2 uclcdDisplay21;

	private UCLCDDisplay2 uclcdDisplay22;

	private UCLCDDisplay2 uclcdDisplay23;

	private UCLCDDisplay2 uclcdDisplay24;

	private UCLCDDisplay2 uclcdDisplay25;

	public Label label1;

	private Button buttonLCD;

	private UCComboBox2 ucComboBox21;

	private UCComboBox2 ucComboBox22;

	private UCComboBox2 ucComboBox23;

	private Button button1;

	private Button button2;

	private Button button3;

	private Button buttonTimer;

	private Button buttonZDY;

	private Button buttonYY;

	private Button buttonMR;

	private Button buttonOnOff;

	public Label labelMP4;

	public Label labelSetting;

	private Button buttonBlack;

	private Button buttonWhite;

	private Button button000;

	private Button button090;

	private Button button180;

	private Button button270;

	public Label label2;

	private Button buttonBJYY;

	private Button buttonSub;

	private Button buttonAdd;

	private Button buttonZYGLQ;

	public FormDeviceLCD()
	{
		InitializeComponent();
		((ButtonBase)buttonTimer).FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
		((ButtonBase)buttonZDY).FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
		ucComboBox21.ucComboBox = ucComboBox1;
		ucComboBox22.ucComboBox = ucComboBox2;
		ucComboBox23.ucComboBox = ucComboBox3;
		m_timer = new System.Timers.Timer(1000.0);
		m_timer.Elapsed += Timer_event;
		m_timer.AutoReset = true;
		m_timer.Start();
	}

	public void ReceivedData(byte[] data)
	{
		if (data.Length == 0)
		{
		}
	}

	private void buttonBJYY_Click(object sender, EventArgs e)
	{
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		if (mcuName.Length == 0)
		{
			MessageBox.Show("请选择背景文件！");
			return;
		}
		if (((Control)labelMP4).Text.Length > 46)
		{
			MessageBox.Show("背景文件名过长，请重命名或者选择其他文件！");
			return;
		}
		((Control)labelSetting).Show();
		Application.DoEvents();
		while (isSendData)
		{
			Thread.Sleep(1);
		}
		isSendData = true;
		m_timer.Stop();
		Form1.InitUsbPan();
		delegateFD?.Invoke(this, UserDevice.USB_PACKED_LCD, 0, 97, Encoding.ASCII.GetBytes(((Control)labelMP4).Text + "\0"));
		Thread.Sleep(1000);
		((Control)this).Cursor = Cursors.WaitCursor;
		for (int i = 0; i < 30; i++)
		{
			Thread.Sleep(1000);
			Form1.CheakUsbPan();
			if (Form1.UIn)
			{
				Thread.Sleep(1000);
				break;
			}
		}
		if (Form1.UIn)
		{
			string destFileName = Form1.UInName + ((Control)labelMP4).Text;
			try
			{
				File.Copy(mcuName, destFileName, overwrite: true);
			}
			catch
			{
				MessageBox.Show("上传背景视频或图片失败！", "请检查您的设备状态");
			}
			Thread.Sleep(2000);
		}
		else
		{
			MessageBox.Show("上传背景视频或图片失败！", "请检查您的设备状态");
		}
		((Control)this).Cursor = Cursors.Default;
		delegateFD?.Invoke(this, UserDevice.USB_PACKED_LCD, 0, 98);
		Thread.Sleep(1000);
		m_timer.Start();
		isSendData = false;
		((Control)labelSetting).Hide();
	}

	private void buttonYY_Click(object sender, EventArgs e)
	{
		((Control)labelSetting).Show();
		Application.DoEvents();
		while (isSendData)
		{
			Thread.Sleep(1);
		}
		isSendData = true;
		m_timer.Stop();
		byte[] array = new byte[50];
		int num = 0;
		int num2 = 0;
		while (num < modeCount)
		{
			array[num2] = (byte)(isTimerVal[num] ? 1u : 0u);
			array[num2 + 1] = (byte)myChVal[num];
			array[num2 + 2] = (byte)myTimerVal[num];
			array[num2 + 3] = (byte)modeVal[num, 0];
			array[num2 + 4] = (byte)modeVal[num, 1];
			array[num2 + 5] = (byte)modeVal[num, 2];
			switch (num)
			{
			case 0:
				array[num2 + 6] = (byte)Convert.ToInt32(((Control)textBox1).Text);
				array[num2 + 7] = (byte)(Convert.ToInt32(((Control)textBox1).Text) >> 8);
				break;
			case 1:
				array[num2 + 6] = (byte)Convert.ToInt32(((Control)textBox2).Text);
				array[num2 + 7] = (byte)(Convert.ToInt32(((Control)textBox2).Text) >> 8);
				break;
			case 2:
				array[num2 + 6] = (byte)Convert.ToInt32(((Control)textBox3).Text);
				array[num2 + 7] = (byte)(Convert.ToInt32(((Control)textBox3).Text) >> 8);
				break;
			case 3:
				array[num2 + 6] = (byte)Convert.ToInt32(((Control)textBox4).Text);
				array[num2 + 7] = (byte)(Convert.ToInt32(((Control)textBox4).Text) >> 8);
				break;
			case 4:
				array[num2 + 6] = (byte)Convert.ToInt32(((Control)textBox5).Text);
				array[num2 + 7] = (byte)(Convert.ToInt32(((Control)textBox5).Text) >> 8);
				break;
			}
			num++;
			num2 += 10;
		}
		while (num < 5)
		{
			array[num2] = array[num2 - 10];
			array[num2 + 1] = array[num2 - 9];
			array[num2 + 2] = array[num2 - 8];
			array[num2 + 3] = array[num2 - 7];
			array[num2 + 4] = array[num2 - 6];
			array[num2 + 5] = array[num2 - 5];
			array[num2 + 6] = 0;
			array[num2 + 7] = 0;
			num++;
			num2 += 10;
		}
		Thread.Sleep(1000);
		delegateFD?.Invoke(this, UserDevice.USB_PACKED_LCD, 0, 96, array);
		Thread.Sleep(1000);
		m_timer.Start();
		isSendData = false;
		((Control)labelSetting).Hide();
	}

	private void buttonYY_Click1(object sender, EventArgs e)
	{
		while (isSendData)
		{
			Thread.Sleep(1);
		}
		isSendData = true;
		m_timer.Stop();
		delegateFD?.Invoke(this, UserDevice.USB_PACKED_LCD, 0, 98);
		Thread.Sleep(1000);
		m_timer.Start();
		isSendData = false;
	}

	private void Timer_event(object sender, ElapsedEventArgs e)
	{
		m_timer.Stop();
		while (isSendData)
		{
			Thread.Sleep(1);
		}
		isSendData = true;
		m_timer.Start();
		delegateFD?.Invoke(this, UserDevice.USB_PACKED_LCD, 0, 0, Encoding.ASCII.GetBytes(GetLcdDisplayValNo(0, 0) + "," + GetLcdDisplayValNo(0, 1) + "," + GetLcdDisplayValNo(0, 2) + "," + GetLcdDisplayValNo(0, 3) + "," + GetLcdDisplayValNo(1, 0) + "," + GetLcdDisplayValNo(1, 1) + ","));
		Thread.Sleep(330);
		delegateFD?.Invoke(this, UserDevice.USB_PACKED_LCD, 0, 32, Encoding.ASCII.GetBytes(GetLcdDisplayValNo(2, 0) + "," + GetLcdDisplayValNo(2, 1) + "," + GetLcdDisplayValNo(2, 2) + "," + GetLcdDisplayValNo(3, 0) + "," + GetLcdDisplayValNo(3, 1) + ","));
		Thread.Sleep(330);
		if (isOn)
		{
			delegateFD?.Invoke(this, UserDevice.USB_PACKED_LCD, 0, 64, Encoding.ASCII.GetBytes(DateTime.Now.ToString("yyyy-MM-dd") + "," + DateTime.Now.ToShortTimeString() + "," + DateTime.Now.ToString("hh:mm") + ","));
		}
		else
		{
			delegateFD?.Invoke(this, UserDevice.USB_PACKED_LCD, 0, 64, Encoding.ASCII.GetBytes(" , , ,"));
		}
		isSendData = false;
	}

	private void FormDeviceLCD_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (m_timer != null)
		{
			m_timer.Stop();
			m_timer.Close();
			m_timer.Dispose();
		}
		form = null;
	}

	private void ucComboBox1(int mode1, int mode2)
	{
		if (mode1 == -1)
		{
			ucComboBox22.UCComboBoxCollect();
			ucComboBox23.UCComboBoxCollect();
			return;
		}
		myMode11 = mode1;
		myMode12 = mode2;
		SetMyArryMode();
		SetMyLcdDisplay();
		SetMyLcdDisplayL();
	}

	private void ucComboBox2(int mode1, int mode2)
	{
		if (mode1 == -1)
		{
			ucComboBox21.UCComboBoxCollect();
			ucComboBox23.UCComboBoxCollect();
			return;
		}
		myMode21 = mode1;
		myMode22 = mode2;
		SetMyArryMode();
		SetMyLcdDisplay();
		SetMyLcdDisplayL();
	}

	private void ucComboBox3(int mode1, int mode2)
	{
		if (mode1 == -1)
		{
			ucComboBox21.UCComboBoxCollect();
			ucComboBox22.UCComboBoxCollect();
			return;
		}
		myMode31 = mode1;
		myMode32 = mode2;
		SetMyArryMode();
		SetMyLcdDisplay();
		SetMyLcdDisplayL();
	}

	private void textBox_KeyPress(object sender, KeyPressEventArgs e)
	{
		if (!char.IsNumber(e.KeyChar) && e.KeyChar != '\b')
		{
			e.Handled = true;
		}
	}

	private string GetLcdDisplayValNo(int m1, int m2)
	{
		string result = "";
		switch (m1)
		{
		case 0:
			switch (m2)
			{
			case 0:
				result = Form1.MainTemp;
				break;
			case 1:
				result = Form1.CPUTemp.ToString("0.0");
				break;
			case 2:
				result = Form1.GPUTemp.ToString("0.0");
				break;
			case 3:
				result = Form1.MemTemp;
				break;
			}
			break;
		case 1:
			switch (m2)
			{
			case 0:
				result = Form1.CPUFan;
				break;
			case 1:
				result = Form1.GPUFan;
				break;
			}
			break;
		case 2:
			switch (m2)
			{
			case 0:
				result = Form1.CPUNowFrequency;
				break;
			case 1:
				result = Form1.GPUFrequency;
				break;
			case 2:
				result = Form1.MemNowFrequency;
				break;
			}
			break;
		case 3:
			switch (m2)
			{
			case 0:
				result = Form1.CPUVoltage;
				break;
			case 1:
				result = Form1.MemVoltage;
				break;
			}
			break;
		}
		return result;
	}

	private string GetLcdDisplayVal(int m1, int m2)
	{
		string result = "";
		switch (m1)
		{
		case 0:
			switch (m2)
			{
			case 0:
				result = Form1.MainTemp + "℃";
				break;
			case 1:
				result = Form1.CPUTemp.ToString("0.0") + "℃";
				break;
			case 2:
				result = Form1.GPUTemp.ToString("0.0") + "℃";
				break;
			case 3:
				result = Form1.MemTemp + "℃";
				break;
			}
			break;
		case 1:
			switch (m2)
			{
			case 0:
				result = Form1.CPUFan + "RPM";
				break;
			case 1:
				result = Form1.GPUFan + "RPM";
				break;
			}
			break;
		case 2:
			switch (m2)
			{
			case 0:
				result = Form1.CPUNowFrequency + "MHz";
				break;
			case 1:
				result = Form1.GPUFrequency + "MHz";
				break;
			case 2:
				result = Form1.MemNowFrequency + "MHz";
				break;
			}
			break;
		case 3:
			switch (m2)
			{
			case 0:
				result = Form1.CPUVoltage + "V";
				break;
			case 1:
				result = Form1.MemVoltage + "V";
				break;
			}
			break;
		}
		return result;
	}

	private void SetMyLcdDisplay()
	{
		if (isTimer)
		{
			if (myTimer == 24)
			{
				uclcdDisplay11.SetUCLCDDisplay1Val(1, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToShortTimeString(), "", "", "", "");
			}
			else
			{
				uclcdDisplay11.SetUCLCDDisplay1Val(1, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToString("hh:mm"), "", "", "", "");
			}
		}
		else
		{
			uclcdDisplay11.SetUCLCDDisplay1Val(myCh, UCComboBox2.MyName[myMode11, myMode12], GetLcdDisplayVal(myMode11, myMode12), UCComboBox2.MyName[myMode21, myMode22], GetLcdDisplayVal(myMode21, myMode22), UCComboBox2.MyName[myMode31, myMode32], GetLcdDisplayVal(myMode31, myMode32));
		}
	}

	private void SetMyLcdDisplayL()
	{
		UCLCDDisplay2 uCLCDDisplay = modeCh switch
		{
			0 => uclcdDisplay21, 
			1 => uclcdDisplay22, 
			2 => uclcdDisplay23, 
			3 => uclcdDisplay24, 
			4 => uclcdDisplay25, 
			_ => uclcdDisplay21, 
		};
		if (isTimer)
		{
			if (myTimer == 24)
			{
				uCLCDDisplay.SetUCLCDDisplay2Val(DateTime.Now.ToShortTimeString());
			}
			else
			{
				uCLCDDisplay.SetUCLCDDisplay2Val(DateTime.Now.ToString("hh:mm"));
			}
		}
		else if (myCh == 2)
		{
			uCLCDDisplay.SetUCLCDDisplay2Val(UCComboBox2.MyName[myMode21, myMode22]);
		}
		else
		{
			uCLCDDisplay.SetUCLCDDisplay2Val(UCComboBox2.MyName[myMode11, myMode12]);
		}
	}

	private void setMyNowMode(int n)
	{
		modeCh = n;
		myMode11 = modeVal[n, 0] >> 4;
		myMode12 = modeVal[n, 0] & 0xF;
		myMode21 = modeVal[n, 1] >> 4;
		myMode22 = modeVal[n, 1] & 0xF;
		myMode31 = modeVal[n, 2] >> 4;
		myMode32 = modeVal[n, 2] & 0xF;
		isTimer = isTimerVal[modeCh];
		myCh = myChVal[modeCh];
		myTimer = myTimerVal[modeCh];
	}

	private void SetMyArryMode()
	{
		modeVal[modeCh, 0] = (myMode11 << 4) + myMode12;
		modeVal[modeCh, 1] = (myMode21 << 4) + myMode22;
		modeVal[modeCh, 2] = (myMode31 << 4) + myMode32;
		isTimerVal[modeCh] = isTimer;
		myChVal[modeCh] = myCh;
		myTimerVal[modeCh] = myTimer;
	}

	private void SetOtherModeUI()
	{
		if (myTimer == 24)
		{
			((Control)buttonTimer).BackgroundImage = (Image)(object)Resources.BTLCD24;
		}
		else
		{
			((Control)buttonTimer).BackgroundImage = (Image)(object)Resources.BTLCD12;
		}
		if (isTimer)
		{
			((Control)buttonTimer).Height = 63;
			((Control)buttonZDY).Height = 61;
		}
		else
		{
			((Control)buttonTimer).Height = 61;
			((Control)buttonZDY).Height = 63;
		}
		button_Click();
	}

	private void uclcdDisplay21_Click(object sender, EventArgs e)
	{
		((Control)uclcdDisplay21).BackColor = Color.FromArgb(255, 200, 11);
		((Control)uclcdDisplay22).BackColor = Color.Transparent;
		((Control)uclcdDisplay23).BackColor = Color.Transparent;
		((Control)uclcdDisplay24).BackColor = Color.Transparent;
		((Control)uclcdDisplay25).BackColor = Color.Transparent;
		setMyNowMode(0);
		SetUCComboBoxMode();
		SetOtherModeUI();
		SetMyLcdDisplay();
	}

	private void uclcdDisplay22_Click(object sender, EventArgs e)
	{
		((Control)uclcdDisplay22).BackColor = Color.FromArgb(255, 200, 11);
		((Control)uclcdDisplay21).BackColor = Color.Transparent;
		((Control)uclcdDisplay23).BackColor = Color.Transparent;
		((Control)uclcdDisplay24).BackColor = Color.Transparent;
		((Control)uclcdDisplay25).BackColor = Color.Transparent;
		setMyNowMode(1);
		SetUCComboBoxMode();
		SetOtherModeUI();
		SetMyLcdDisplay();
	}

	private void uclcdDisplay23_Click(object sender, EventArgs e)
	{
		((Control)uclcdDisplay23).BackColor = Color.FromArgb(255, 200, 11);
		((Control)uclcdDisplay21).BackColor = Color.Transparent;
		((Control)uclcdDisplay22).BackColor = Color.Transparent;
		((Control)uclcdDisplay24).BackColor = Color.Transparent;
		((Control)uclcdDisplay25).BackColor = Color.Transparent;
		setMyNowMode(2);
		SetUCComboBoxMode();
		SetOtherModeUI();
		SetMyLcdDisplay();
	}

	private void uclcdDisplay24_Click(object sender, EventArgs e)
	{
		((Control)uclcdDisplay24).BackColor = Color.FromArgb(255, 200, 11);
		((Control)uclcdDisplay21).BackColor = Color.Transparent;
		((Control)uclcdDisplay22).BackColor = Color.Transparent;
		((Control)uclcdDisplay23).BackColor = Color.Transparent;
		((Control)uclcdDisplay25).BackColor = Color.Transparent;
		setMyNowMode(3);
		SetUCComboBoxMode();
		SetOtherModeUI();
		SetMyLcdDisplay();
	}

	private void uclcdDisplay25_Click(object sender, EventArgs e)
	{
		((Control)uclcdDisplay25).BackColor = Color.FromArgb(255, 200, 11);
		((Control)uclcdDisplay21).BackColor = Color.Transparent;
		((Control)uclcdDisplay22).BackColor = Color.Transparent;
		((Control)uclcdDisplay23).BackColor = Color.Transparent;
		((Control)uclcdDisplay24).BackColor = Color.Transparent;
		setMyNowMode(4);
		SetUCComboBoxMode();
		SetOtherModeUI();
		SetMyLcdDisplay();
	}

	private void button_Click()
	{
		((Control)button1).BackgroundImage = (Image)(object)Resources.BCLCD1;
		((Control)button2).BackgroundImage = (Image)(object)Resources.BCLCD2;
		((Control)button3).BackgroundImage = (Image)(object)Resources.BCLCD3;
		switch (myCh)
		{
		case 1:
			((Control)button1).BackgroundImage = (Image)(object)Resources.BCLCD1_1;
			((Control)ucComboBox21).Show();
			((Control)ucComboBox22).Hide();
			((Control)ucComboBox23).Hide();
			break;
		case 2:
			((Control)button2).BackgroundImage = (Image)(object)Resources.BCLCD2_1;
			((Control)ucComboBox22).Location = new Point(495, 534);
			((Control)ucComboBox23).Location = new Point(495, 573);
			((Control)ucComboBox21).Hide();
			((Control)ucComboBox22).Show();
			((Control)ucComboBox23).Show();
			break;
		case 3:
			((Control)button3).BackgroundImage = (Image)(object)Resources.BCLCD3_1;
			((Control)ucComboBox22).Location = new Point(495, 573);
			((Control)ucComboBox23).Location = new Point(495, 612);
			((Control)ucComboBox21).Show();
			((Control)ucComboBox22).Show();
			((Control)ucComboBox23).Show();
			break;
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		myCh = 1;
		button_Click();
		SetMyArryMode();
		SetMyLcdDisplay();
		SetMyLcdDisplayL();
	}

	private void button2_Click(object sender, EventArgs e)
	{
		myCh = 2;
		button_Click();
		SetMyArryMode();
		SetMyLcdDisplay();
		SetMyLcdDisplayL();
	}

	private void button3_Click(object sender, EventArgs e)
	{
		myCh = 3;
		button_Click();
		SetMyArryMode();
		SetMyLcdDisplay();
		SetMyLcdDisplayL();
	}

	private void buttonTimer_Click(object sender, EventArgs e)
	{
		if (!isTimer)
		{
			isTimer = true;
			((Control)buttonTimer).Height = 63;
			((Control)buttonZDY).Height = 61;
		}
		else if (myTimer == 24)
		{
			myTimer = 12;
			((Control)buttonTimer).BackgroundImage = (Image)(object)Resources.BTLCD12;
		}
		else
		{
			myTimer = 24;
			((Control)buttonTimer).BackgroundImage = (Image)(object)Resources.BTLCD24;
		}
		SetMyArryMode();
		SetMyLcdDisplay();
		SetMyLcdDisplayL();
	}

	private void buttonZDY_Click(object sender, EventArgs e)
	{
		isTimer = false;
		((Control)buttonTimer).Height = 61;
		((Control)buttonZDY).Height = 63;
		SetMyArryMode();
		SetMyLcdDisplay();
		SetMyLcdDisplayL();
	}

	private void SetUCComboBoxMode()
	{
		ucComboBox21.SetUCComboBoxMode(myMode11, myMode12);
		ucComboBox22.SetUCComboBoxMode(myMode21, myMode22);
		ucComboBox23.SetUCComboBoxMode(myMode31, myMode32);
	}

	private void buttonMR_Click(object sender, EventArgs e)
	{
		myMode11 = 0;
		myMode12 = 1;
		myMode21 = 0;
		myMode22 = 1;
		myMode31 = 0;
		myMode32 = 1;
		SetUCComboBoxMode();
		SetMyArryMode();
		SetMyLcdDisplay();
		SetMyLcdDisplayL();
	}

	private void buttonLCD_Click(object sender, EventArgs e)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Invalid comparison between Unknown and I4
		OpenFileDialog val = new OpenFileDialog();
		((FileDialog)val).Filter = "支持格式(*.png;*.bmp;*.jpg;*.gif;*.mp4;*.mov;*.avi)|*.png;*.bmp;*.jpg;*.gif;*.mp4;*.mov;*.avi";
		((FileDialog)val).Title = "Open";
		if ((int)((CommonDialog)val).ShowDialog() == 1)
		{
			mcuName = ((FileDialog)val).FileName;
			((Control)labelMP4).Text = val.SafeFileName;
			uclcdDisplay11.SetUCLCDDisplayBackgroundImage(mcuName);
		}
	}

	private void buttonOnOff_Click(object sender, EventArgs e)
	{
		if (isOn)
		{
			isOn = false;
			((Control)uclcdDisplay11.label1).Hide();
			((Control)uclcdDisplay11.label2).Hide();
			((Control)uclcdDisplay11.label3).Hide();
			((Control)uclcdDisplay11.label4).Hide();
			((Control)uclcdDisplay11.label5).Hide();
			((Control)uclcdDisplay11.label6).Hide();
			((Control)buttonOnOff).BackgroundImage = (Image)(object)Resources.BLcdOff;
			((Control)labelSetting).Show();
			Application.DoEvents();
			while (isSendData)
			{
				Thread.Sleep(1);
			}
			isSendData = true;
			m_timer.Stop();
			byte[] array = new byte[50];
			int num = 0;
			int num2 = 0;
			while (num < 5)
			{
				array[num2] = 1;
				array[num2 + 1] = (byte)myChVal[num];
				array[num2 + 2] = (byte)myTimerVal[num];
				array[num2 + 3] = (byte)modeVal[num, 0];
				array[num2 + 4] = (byte)modeVal[num, 1];
				array[num2 + 5] = (byte)modeVal[num, 2];
				array[num2 + 6] = 1;
				array[num2 + 7] = 0;
				num++;
				num2 += 10;
			}
			Thread.Sleep(1000);
			delegateFD?.Invoke(this, UserDevice.USB_PACKED_LCD, 0, 96, array);
			Thread.Sleep(1000);
			m_timer.Start();
			isSendData = false;
			((Control)labelSetting).Hide();
			return;
		}
		isOn = true;
		((Control)uclcdDisplay11.label1).Show();
		((Control)uclcdDisplay11.label2).Show();
		((Control)uclcdDisplay11.label3).Show();
		((Control)uclcdDisplay11.label4).Show();
		((Control)uclcdDisplay11.label5).Show();
		((Control)uclcdDisplay11.label6).Show();
		((Control)buttonOnOff).BackgroundImage = (Image)(object)Resources.BLcdOn;
		((Control)labelSetting).Show();
		Application.DoEvents();
		while (isSendData)
		{
			Thread.Sleep(1);
		}
		isSendData = true;
		m_timer.Stop();
		byte[] array2 = new byte[50];
		int num3 = 0;
		int num4 = 0;
		while (num3 < modeCount)
		{
			array2[num4] = (byte)(isTimerVal[num3] ? 1u : 0u);
			array2[num4 + 1] = (byte)myChVal[num3];
			array2[num4 + 2] = (byte)myTimerVal[num3];
			array2[num4 + 3] = (byte)modeVal[num3, 0];
			array2[num4 + 4] = (byte)modeVal[num3, 1];
			array2[num4 + 5] = (byte)modeVal[num3, 2];
			switch (num3)
			{
			case 0:
				array2[num4 + 6] = (byte)Convert.ToInt32(((Control)textBox1).Text);
				array2[num4 + 7] = (byte)(Convert.ToInt32(((Control)textBox1).Text) >> 8);
				break;
			case 1:
				array2[num4 + 6] = (byte)Convert.ToInt32(((Control)textBox2).Text);
				array2[num4 + 7] = (byte)(Convert.ToInt32(((Control)textBox2).Text) >> 8);
				break;
			case 2:
				array2[num4 + 6] = (byte)Convert.ToInt32(((Control)textBox3).Text);
				array2[num4 + 7] = (byte)(Convert.ToInt32(((Control)textBox3).Text) >> 8);
				break;
			case 3:
				array2[num4 + 6] = (byte)Convert.ToInt32(((Control)textBox4).Text);
				array2[num4 + 7] = (byte)(Convert.ToInt32(((Control)textBox4).Text) >> 8);
				break;
			case 4:
				array2[num4 + 6] = (byte)Convert.ToInt32(((Control)textBox5).Text);
				array2[num4 + 7] = (byte)(Convert.ToInt32(((Control)textBox5).Text) >> 8);
				break;
			}
			num3++;
			num4 += 10;
		}
		while (num3 < 5)
		{
			array2[num4] = array2[num4 - 10];
			array2[num4 + 1] = array2[num4 - 9];
			array2[num4 + 2] = array2[num4 - 8];
			array2[num4 + 3] = array2[num4 - 7];
			array2[num4 + 4] = array2[num4 - 6];
			array2[num4 + 5] = array2[num4 - 5];
			array2[num4 + 6] = 0;
			array2[num4 + 7] = 0;
			num3++;
			num4 += 10;
		}
		Thread.Sleep(1000);
		delegateFD?.Invoke(this, UserDevice.USB_PACKED_LCD, 0, 96, array2);
		Thread.Sleep(1000);
		m_timer.Start();
		isSendData = false;
		((Control)labelSetting).Hide();
	}

	private void buttonBlack_Click(object sender, EventArgs e)
	{
		while (isSendData)
		{
			Thread.Sleep(1);
		}
		isSendData = true;
		m_timer.Stop();
		delegateFD?.Invoke(this, UserDevice.USB_PACKED_LCD, 0, 104);
		Thread.Sleep(1000);
		m_timer.Start();
		isSendData = false;
	}

	private void buttonWhite_Click(object sender, EventArgs e)
	{
		while (isSendData)
		{
			Thread.Sleep(1);
		}
		isSendData = true;
		m_timer.Stop();
		delegateFD?.Invoke(this, UserDevice.USB_PACKED_LCD, 0, 103);
		Thread.Sleep(1000);
		m_timer.Start();
		isSendData = false;
	}

	private void button000_Click(object sender, EventArgs e)
	{
		while (isSendData)
		{
			Thread.Sleep(1);
		}
		isSendData = true;
		m_timer.Stop();
		delegateFD?.Invoke(this, UserDevice.USB_PACKED_LCD, 0, 99);
		Thread.Sleep(1000);
		m_timer.Start();
		isSendData = false;
	}

	private void button090_Click(object sender, EventArgs e)
	{
		while (isSendData)
		{
			Thread.Sleep(1);
		}
		isSendData = true;
		m_timer.Stop();
		delegateFD?.Invoke(this, UserDevice.USB_PACKED_LCD, 0, 100);
		Thread.Sleep(1000);
		m_timer.Start();
		isSendData = false;
	}

	private void button180_Click(object sender, EventArgs e)
	{
		while (isSendData)
		{
			Thread.Sleep(1);
		}
		isSendData = true;
		m_timer.Stop();
		delegateFD?.Invoke(this, UserDevice.USB_PACKED_LCD, 0, 101);
		Thread.Sleep(1000);
		m_timer.Start();
		isSendData = false;
	}

	private void button270_Click(object sender, EventArgs e)
	{
		while (isSendData)
		{
			Thread.Sleep(1);
		}
		isSendData = true;
		m_timer.Stop();
		delegateFD?.Invoke(this, UserDevice.USB_PACKED_LCD, 0, 102);
		Thread.Sleep(1000);
		m_timer.Start();
		isSendData = false;
	}

	private void ModeSet(int val)
	{
		((Control)uclcdDisplay22).Hide();
		((Control)uclcdDisplay23).Hide();
		((Control)uclcdDisplay24).Hide();
		((Control)uclcdDisplay25).Hide();
		((Control)textBox2).Hide();
		((Control)textBox3).Hide();
		((Control)textBox4).Hide();
		((Control)textBox5).Hide();
		switch (val)
		{
		case 1:
			((Control)textBox2).Text = "0";
			((Control)textBox3).Text = "0";
			((Control)textBox4).Text = "0";
			((Control)textBox5).Text = "0";
			break;
		case 2:
			((Control)uclcdDisplay22).Show();
			((Control)textBox2).Show();
			((Control)textBox2).Text = "6";
			((Control)textBox3).Text = "0";
			((Control)textBox4).Text = "0";
			((Control)textBox5).Text = "0";
			break;
		case 3:
			((Control)uclcdDisplay22).Show();
			((Control)textBox2).Show();
			((Control)uclcdDisplay23).Show();
			((Control)textBox3).Show();
			((Control)textBox2).Text = "6";
			((Control)textBox3).Text = "6";
			((Control)textBox4).Text = "0";
			((Control)textBox5).Text = "0";
			break;
		case 4:
			((Control)uclcdDisplay22).Show();
			((Control)textBox2).Show();
			((Control)uclcdDisplay23).Show();
			((Control)textBox3).Show();
			((Control)uclcdDisplay24).Show();
			((Control)textBox4).Show();
			((Control)textBox2).Text = "6";
			((Control)textBox3).Text = "6";
			((Control)textBox4).Text = "6";
			((Control)textBox5).Text = "0";
			break;
		case 5:
			((Control)uclcdDisplay22).Show();
			((Control)textBox2).Show();
			((Control)uclcdDisplay23).Show();
			((Control)textBox3).Show();
			((Control)uclcdDisplay24).Show();
			((Control)textBox4).Show();
			((Control)uclcdDisplay25).Show();
			((Control)textBox5).Show();
			((Control)textBox2).Text = "6";
			((Control)textBox3).Text = "6";
			((Control)textBox4).Text = "6";
			((Control)textBox5).Text = "6";
			break;
		}
	}

	private void buttonSub_Click(object sender, EventArgs e)
	{
		if (modeCount > 1)
		{
			modeCount--;
		}
		ModeSet(modeCount);
	}

	private void buttonAdd_Click(object sender, EventArgs e)
	{
		if (modeCount < 5)
		{
			modeCount++;
		}
		ModeSet(modeCount);
	}

	private void buttonZYGLQ_Click(object sender, EventArgs e)
	{
		if (isZYGLQ)
		{
			isZYGLQ = false;
			((Control)buttonZYGLQ).BackgroundImage = (Image)(object)Resources.BLCDZYGLQ0;
			((Control)button000).Enabled = true;
			((Control)button090).Enabled = true;
			((Control)button180).Enabled = true;
			((Control)button270).Enabled = true;
			((Control)buttonBlack).Enabled = true;
			((Control)buttonWhite).Enabled = true;
			((Control)buttonBJYY).Enabled = true;
			((Control)buttonYY).Enabled = true;
			((Control)buttonOnOff).Enabled = true;
			while (isSendData)
			{
				Thread.Sleep(1);
			}
			isSendData = true;
			m_timer.Stop();
			delegateFD?.Invoke(this, UserDevice.USB_PACKED_LCD, 0, 98);
			Thread.Sleep(1000);
			m_timer.Start();
			isSendData = false;
		}
		else
		{
			isZYGLQ = true;
			((Control)buttonZYGLQ).BackgroundImage = (Image)(object)Resources.BLCDZYGLQ1;
			((Control)button000).Enabled = false;
			((Control)button090).Enabled = false;
			((Control)button180).Enabled = false;
			((Control)button270).Enabled = false;
			((Control)buttonBlack).Enabled = false;
			((Control)buttonWhite).Enabled = false;
			((Control)buttonBJYY).Enabled = false;
			((Control)buttonYY).Enabled = false;
			((Control)buttonOnOff).Enabled = false;
			while (isSendData)
			{
				Thread.Sleep(1);
			}
			isSendData = true;
			m_timer.Stop();
			delegateFD?.Invoke(this, UserDevice.USB_PACKED_LCD, 0, 105);
			Thread.Sleep(1000);
			m_timer.Start();
			isSendData = false;
		}
	}

	private void FormDeviceLCD_MouseDown(object sender, MouseEventArgs e)
	{
		ucComboBox21.UCComboBoxCollect();
		ucComboBox22.UCComboBoxCollect();
		ucComboBox23.UCComboBoxCollect();
		form.Form1_MouseDown(this, e);
	}

	private void FormDeviceLCD_MouseMove(object sender, MouseEventArgs e)
	{
		form.Form1_MouseMove(this, e);
	}

	private void FormDeviceLCD_MouseUp(object sender, MouseEventArgs e)
	{
		form.Form1_MouseUp(this, e);
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
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Expected O, but got Unknown
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Expected O, but got Unknown
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Expected O, but got Unknown
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Expected O, but got Unknown
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Expected O, but got Unknown
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Expected O, but got Unknown
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Expected O, but got Unknown
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Expected O, but got Unknown
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Expected O, but got Unknown
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Expected O, but got Unknown
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Expected O, but got Unknown
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Expected O, but got Unknown
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Expected O, but got Unknown
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Expected O, but got Unknown
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Expected O, but got Unknown
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Expected O, but got Unknown
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Expected O, but got Unknown
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Expected O, but got Unknown
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Expected O, but got Unknown
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Expected O, but got Unknown
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Expected O, but got Unknown
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Expected O, but got Unknown
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Expected O, but got Unknown
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Expected O, but got Unknown
		//IL_0553: Unknown result type (might be due to invalid IL or missing references)
		//IL_055d: Expected O, but got Unknown
		//IL_05a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0624: Unknown result type (might be due to invalid IL or missing references)
		//IL_062e: Expected O, but got Unknown
		//IL_067b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0685: Expected O, but got Unknown
		//IL_06c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_074c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0756: Expected O, but got Unknown
		//IL_0788: Unknown result type (might be due to invalid IL or missing references)
		//IL_0792: Expected O, but got Unknown
		//IL_07d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_09cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0acf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bcf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ccf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dcf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ecf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fcf: Unknown result type (might be due to invalid IL or missing references)
		//IL_10cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_1168: Unknown result type (might be due to invalid IL or missing references)
		//IL_1172: Expected O, but got Unknown
		//IL_11b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_123a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1244: Expected O, but got Unknown
		//IL_1281: Unknown result type (might be due to invalid IL or missing references)
		//IL_137a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1469: Unknown result type (might be due to invalid IL or missing references)
		//IL_1569: Unknown result type (might be due to invalid IL or missing references)
		//IL_1666: Unknown result type (might be due to invalid IL or missing references)
		//IL_1766: Unknown result type (might be due to invalid IL or missing references)
		//IL_1866: Unknown result type (might be due to invalid IL or missing references)
		//IL_18fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1905: Expected O, but got Unknown
		//IL_1948: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a42: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b42: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c42: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d4e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1dc3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1edb: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f78: Unknown result type (might be due to invalid IL or missing references)
		//IL_2012: Unknown result type (might be due to invalid IL or missing references)
		//IL_20ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_2140: Unknown result type (might be due to invalid IL or missing references)
		//IL_2221: Unknown result type (might be due to invalid IL or missing references)
		//IL_2570: Unknown result type (might be due to invalid IL or missing references)
		//IL_257a: Expected O, but got Unknown
		//IL_25c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_25cc: Expected O, but got Unknown
		//IL_25d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_25df: Expected O, but got Unknown
		//IL_25e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_25f2: Expected O, but got Unknown
		//IL_25fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_2605: Expected O, but got Unknown
		textBox1 = new TextBox();
		textBox2 = new TextBox();
		textBox3 = new TextBox();
		textBox4 = new TextBox();
		textBox5 = new TextBox();
		label1 = new Label();
		buttonLCD = new Button();
		button1 = new Button();
		button2 = new Button();
		button3 = new Button();
		buttonTimer = new Button();
		buttonZDY = new Button();
		buttonYY = new Button();
		buttonMR = new Button();
		buttonOnOff = new Button();
		labelMP4 = new Label();
		labelSetting = new Label();
		buttonBlack = new Button();
		buttonWhite = new Button();
		button000 = new Button();
		button090 = new Button();
		button180 = new Button();
		button270 = new Button();
		label2 = new Label();
		buttonBJYY = new Button();
		buttonSub = new Button();
		buttonAdd = new Button();
		ucComboBox21 = new UCComboBox2();
		ucComboBox22 = new UCComboBox2();
		ucComboBox23 = new UCComboBox2();
		uclcdDisplay25 = new UCLCDDisplay2();
		uclcdDisplay24 = new UCLCDDisplay2();
		uclcdDisplay23 = new UCLCDDisplay2();
		uclcdDisplay22 = new UCLCDDisplay2();
		uclcdDisplay21 = new UCLCDDisplay2();
		uclcdDisplay11 = new UCLCDDisplay1();
		buttonZYGLQ = new Button();
		((Control)this).SuspendLayout();
		((Control)textBox1).BackColor = Color.FromArgb(71, 71, 71);
		((TextBoxBase)textBox1).BorderStyle = (BorderStyle)0;
		((Control)textBox1).Cursor = Cursors.Default;
		((Control)textBox1).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)textBox1).ForeColor = Color.FromArgb(224, 224, 224);
		((Control)textBox1).Location = new Point(84, 648);
		((Control)textBox1).Margin = new Padding(0);
		((TextBoxBase)textBox1).MaxLength = 3;
		((Control)textBox1).Name = "textBox1";
		((Control)textBox1).Size = new Size(28, 16);
		((Control)textBox1).TabIndex = 22;
		((Control)textBox1).Text = "6";
		textBox1.TextAlign = (HorizontalAlignment)2;
		((Control)textBox1).KeyPress += new KeyPressEventHandler(textBox_KeyPress);
		((Control)textBox2).BackColor = Color.FromArgb(71, 71, 71);
		((TextBoxBase)textBox2).BorderStyle = (BorderStyle)0;
		((Control)textBox2).Cursor = Cursors.Default;
		((Control)textBox2).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)textBox2).ForeColor = Color.FromArgb(224, 224, 224);
		((Control)textBox2).Location = new Point(141, 648);
		((Control)textBox2).Margin = new Padding(0);
		((TextBoxBase)textBox2).MaxLength = 3;
		((Control)textBox2).Name = "textBox2";
		((Control)textBox2).Size = new Size(28, 16);
		((Control)textBox2).TabIndex = 23;
		((Control)textBox2).Text = "6";
		textBox2.TextAlign = (HorizontalAlignment)2;
		((Control)textBox2).Visible = false;
		((Control)textBox2).KeyPress += new KeyPressEventHandler(textBox_KeyPress);
		((Control)textBox3).BackColor = Color.FromArgb(71, 71, 71);
		((TextBoxBase)textBox3).BorderStyle = (BorderStyle)0;
		((Control)textBox3).Cursor = Cursors.Default;
		((Control)textBox3).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)textBox3).ForeColor = Color.FromArgb(224, 224, 224);
		((Control)textBox3).Location = new Point(198, 648);
		((Control)textBox3).Margin = new Padding(0);
		((TextBoxBase)textBox3).MaxLength = 3;
		((Control)textBox3).Name = "textBox3";
		((Control)textBox3).Size = new Size(28, 16);
		((Control)textBox3).TabIndex = 24;
		((Control)textBox3).Text = "6";
		textBox3.TextAlign = (HorizontalAlignment)2;
		((Control)textBox3).Visible = false;
		((Control)textBox3).KeyPress += new KeyPressEventHandler(textBox_KeyPress);
		((Control)textBox4).BackColor = Color.FromArgb(71, 71, 71);
		((TextBoxBase)textBox4).BorderStyle = (BorderStyle)0;
		((Control)textBox4).Cursor = Cursors.Default;
		((Control)textBox4).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)textBox4).ForeColor = Color.FromArgb(224, 224, 224);
		((Control)textBox4).Location = new Point(255, 648);
		((Control)textBox4).Margin = new Padding(0);
		((TextBoxBase)textBox4).MaxLength = 3;
		((Control)textBox4).Name = "textBox4";
		((Control)textBox4).Size = new Size(28, 16);
		((Control)textBox4).TabIndex = 25;
		((Control)textBox4).Text = "6";
		textBox4.TextAlign = (HorizontalAlignment)2;
		((Control)textBox4).Visible = false;
		((Control)textBox4).KeyPress += new KeyPressEventHandler(textBox_KeyPress);
		((Control)textBox5).BackColor = Color.FromArgb(71, 71, 71);
		((TextBoxBase)textBox5).BorderStyle = (BorderStyle)0;
		((Control)textBox5).Cursor = Cursors.Default;
		((Control)textBox5).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)textBox5).ForeColor = Color.FromArgb(224, 224, 224);
		((Control)textBox5).Location = new Point(312, 648);
		((Control)textBox5).Margin = new Padding(0);
		((TextBoxBase)textBox5).MaxLength = 3;
		((Control)textBox5).Name = "textBox5";
		((Control)textBox5).Size = new Size(28, 16);
		((Control)textBox5).TabIndex = 26;
		((Control)textBox5).Text = "6";
		textBox5.TextAlign = (HorizontalAlignment)2;
		((Control)textBox5).Visible = false;
		((Control)textBox5).KeyPress += new KeyPressEventHandler(textBox_KeyPress);
		((Control)label1).BackColor = Color.Transparent;
		label1.FlatStyle = (FlatStyle)0;
		((Control)label1).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
		((Control)label1).ForeColor = Color.FromArgb(141, 149, 163);
		((Control)label1).Location = new Point(494, 307);
		((Control)label1).Margin = new Padding(0);
		((Control)label1).Name = "label1";
		((Control)label1).Size = new Size(40, 25);
		((Control)label1).TabIndex = 49;
		((Control)label1).Text = "背景";
		label1.TextAlign = (ContentAlignment)16;
		((Control)buttonLCD).BackColor = Color.Transparent;
		((Control)buttonLCD).BackgroundImage = (Image)(object)Resources.BPLCD1;
		((Control)buttonLCD).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonLCD).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonLCD).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonLCD).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonLCD).FlatStyle = (FlatStyle)0;
		((Control)buttonLCD).Location = new Point(534, 304);
		((Control)buttonLCD).Margin = new Padding(0);
		((Control)buttonLCD).Name = "buttonLCD";
		((Control)buttonLCD).Size = new Size(27, 28);
		((Control)buttonLCD).TabIndex = 50;
		((ButtonBase)buttonLCD).UseVisualStyleBackColor = false;
		((Control)buttonLCD).Click += buttonLCD_Click;
		((Control)button1).BackColor = Color.Transparent;
		((Control)button1).BackgroundImage = (Image)(object)Resources.BCLCD1;
		((Control)button1).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button1).FlatAppearance.BorderSize = 0;
		((ButtonBase)button1).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button1).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button1).FlatStyle = (FlatStyle)0;
		((Control)button1).Location = new Point(777, 508);
		((Control)button1).Margin = new Padding(0);
		((Control)button1).Name = "button1";
		((Control)button1).Size = new Size(16, 16);
		((Control)button1).TabIndex = 54;
		((ButtonBase)button1).UseVisualStyleBackColor = false;
		((Control)button1).Click += button1_Click;
		((Control)button2).BackColor = Color.Transparent;
		((Control)button2).BackgroundImage = (Image)(object)Resources.BCLCD2;
		((Control)button2).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button2).FlatAppearance.BorderSize = 0;
		((ButtonBase)button2).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button2).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button2).FlatStyle = (FlatStyle)0;
		((Control)button2).Location = new Point(798, 508);
		((Control)button2).Margin = new Padding(0);
		((Control)button2).Name = "button2";
		((Control)button2).Size = new Size(16, 16);
		((Control)button2).TabIndex = 55;
		((ButtonBase)button2).UseVisualStyleBackColor = false;
		((Control)button2).Click += button2_Click;
		((Control)button3).BackColor = Color.Transparent;
		((Control)button3).BackgroundImage = (Image)(object)Resources.BCLCD3_1;
		((Control)button3).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button3).FlatAppearance.BorderSize = 0;
		((ButtonBase)button3).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button3).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button3).FlatStyle = (FlatStyle)0;
		((Control)button3).Location = new Point(819, 508);
		((Control)button3).Margin = new Padding(0);
		((Control)button3).Name = "button3";
		((Control)button3).Size = new Size(16, 16);
		((Control)button3).TabIndex = 56;
		((ButtonBase)button3).UseVisualStyleBackColor = false;
		((Control)button3).Click += button3_Click;
		((Control)buttonTimer).BackColor = Color.Transparent;
		((Control)buttonTimer).BackgroundImage = (Image)(object)Resources.BTLCD24;
		((Control)buttonTimer).BackgroundImageLayout = (ImageLayout)0;
		((ButtonBase)buttonTimer).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonTimer).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonTimer).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonTimer).FlatStyle = (FlatStyle)0;
		((Control)buttonTimer).Location = new Point(495, 416);
		((Control)buttonTimer).Margin = new Padding(0);
		((Control)buttonTimer).Name = "buttonTimer";
		((Control)buttonTimer).Size = new Size(81, 61);
		((Control)buttonTimer).TabIndex = 57;
		((ButtonBase)buttonTimer).UseVisualStyleBackColor = false;
		((Control)buttonTimer).Click += buttonTimer_Click;
		((Control)buttonZDY).BackColor = Color.Transparent;
		((Control)buttonZDY).BackgroundImage = (Image)(object)Resources.BZLCD2;
		((Control)buttonZDY).BackgroundImageLayout = (ImageLayout)0;
		((ButtonBase)buttonZDY).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonZDY).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonZDY).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonZDY).FlatStyle = (FlatStyle)0;
		((Control)buttonZDY).Location = new Point(588, 416);
		((Control)buttonZDY).Margin = new Padding(0);
		((Control)buttonZDY).Name = "buttonZDY";
		((Control)buttonZDY).Size = new Size(81, 63);
		((Control)buttonZDY).TabIndex = 58;
		((ButtonBase)buttonZDY).UseVisualStyleBackColor = false;
		((Control)buttonZDY).Click += buttonZDY_Click;
		((Control)buttonYY).BackColor = Color.Transparent;
		((Control)buttonYY).BackgroundImage = (Image)(object)Resources.BYLCD;
		((Control)buttonYY).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonYY).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonYY).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonYY).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonYY).FlatStyle = (FlatStyle)0;
		((Control)buttonYY).Location = new Point(780, 669);
		((Control)buttonYY).Margin = new Padding(0);
		((Control)buttonYY).Name = "buttonYY";
		((Control)buttonYY).Size = new Size(78, 25);
		((Control)buttonYY).TabIndex = 59;
		((ButtonBase)buttonYY).UseVisualStyleBackColor = false;
		((Control)buttonYY).Click += buttonYY_Click;
		((Control)buttonMR).BackColor = Color.Transparent;
		((Control)buttonMR).BackgroundImage = (Image)(object)Resources.BMLCD;
		((Control)buttonMR).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonMR).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonMR).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonMR).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonMR).FlatStyle = (FlatStyle)0;
		((Control)buttonMR).Location = new Point(678, 669);
		((Control)buttonMR).Margin = new Padding(0);
		((Control)buttonMR).Name = "buttonMR";
		((Control)buttonMR).Size = new Size(78, 25);
		((Control)buttonMR).TabIndex = 60;
		((ButtonBase)buttonMR).UseVisualStyleBackColor = false;
		((Control)buttonMR).Click += buttonMR_Click;
		((Control)buttonOnOff).BackColor = Color.Transparent;
		((Control)buttonOnOff).BackgroundImage = (Image)(object)Resources.BLcdOn;
		((Control)buttonOnOff).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonOnOff).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonOnOff).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonOnOff).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonOnOff).FlatStyle = (FlatStyle)0;
		((Control)buttonOnOff).Location = new Point(423, 376);
		((Control)buttonOnOff).Margin = new Padding(0);
		((Control)buttonOnOff).Name = "buttonOnOff";
		((Control)buttonOnOff).Size = new Size(22, 94);
		((Control)buttonOnOff).TabIndex = 61;
		((ButtonBase)buttonOnOff).UseVisualStyleBackColor = false;
		((Control)buttonOnOff).Click += buttonOnOff_Click;
		((Control)labelMP4).BackColor = Color.Transparent;
		labelMP4.FlatStyle = (FlatStyle)0;
		((Control)labelMP4).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)labelMP4).ForeColor = Color.FromArgb(141, 149, 163);
		((Control)labelMP4).Location = new Point(572, 307);
		((Control)labelMP4).Margin = new Padding(0);
		((Control)labelMP4).Name = "labelMP4";
		((Control)labelMP4).Size = new Size(288, 25);
		((Control)labelMP4).TabIndex = 62;
		labelMP4.TextAlign = (ContentAlignment)16;
		((Control)labelSetting).BackColor = Color.Transparent;
		labelSetting.FlatStyle = (FlatStyle)0;
		((Control)labelSetting).Font = new Font("微软雅黑", 15f, (FontStyle)1, (GraphicsUnit)3, (byte)134);
		((Control)labelSetting).ForeColor = Color.FromArgb(141, 149, 163);
		((Control)labelSetting).Location = new Point(29, 31);
		((Control)labelSetting).Margin = new Padding(0);
		((Control)labelSetting).Name = "labelSetting";
		((Control)labelSetting).Size = new Size(241, 56);
		((Control)labelSetting).TabIndex = 63;
		((Control)labelSetting).Text = "设置中，请稍等。。。";
		labelSetting.TextAlign = (ContentAlignment)16;
		((Control)labelSetting).Visible = false;
		((Control)buttonBlack).BackColor = Color.Black;
		((Control)buttonBlack).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonBlack).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonBlack).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonBlack).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonBlack).FlatStyle = (FlatStyle)0;
		((Control)buttonBlack).Location = new Point(814, 281);
		((Control)buttonBlack).Margin = new Padding(0);
		((Control)buttonBlack).Name = "buttonBlack";
		((Control)buttonBlack).Size = new Size(16, 16);
		((Control)buttonBlack).TabIndex = 64;
		((ButtonBase)buttonBlack).UseVisualStyleBackColor = false;
		((Control)buttonBlack).Click += buttonBlack_Click;
		((Control)buttonWhite).BackColor = Color.White;
		((Control)buttonWhite).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonWhite).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonWhite).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonWhite).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonWhite).FlatStyle = (FlatStyle)0;
		((Control)buttonWhite).Location = new Point(834, 281);
		((Control)buttonWhite).Margin = new Padding(0);
		((Control)buttonWhite).Name = "buttonWhite";
		((Control)buttonWhite).Size = new Size(15, 15);
		((Control)buttonWhite).TabIndex = 65;
		((ButtonBase)buttonWhite).UseVisualStyleBackColor = false;
		((Control)buttonWhite).Click += buttonWhite_Click;
		((Control)button000).BackColor = Color.Transparent;
		((Control)button000).BackgroundImage = (Image)(object)Resources.BLCDXZ0;
		((Control)button000).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button000).FlatAppearance.BorderSize = 0;
		((ButtonBase)button000).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button000).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button000).FlatStyle = (FlatStyle)0;
		((Control)button000).Location = new Point(332, 269);
		((Control)button000).Margin = new Padding(0);
		((Control)button000).Name = "button000";
		((Control)button000).Size = new Size(28, 28);
		((Control)button000).TabIndex = 66;
		((ButtonBase)button000).UseVisualStyleBackColor = false;
		((Control)button000).Click += button000_Click;
		((Control)button090).BackColor = Color.Transparent;
		((Control)button090).BackgroundImage = (Image)(object)Resources.BLCDXZ90;
		((Control)button090).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button090).FlatAppearance.BorderSize = 0;
		((ButtonBase)button090).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button090).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button090).FlatStyle = (FlatStyle)0;
		((Control)button090).Location = new Point(119, 269);
		((Control)button090).Margin = new Padding(0);
		((Control)button090).Name = "button090";
		((Control)button090).Size = new Size(28, 28);
		((Control)button090).TabIndex = 67;
		((ButtonBase)button090).UseVisualStyleBackColor = false;
		((Control)button090).Click += button090_Click;
		((Control)button180).BackColor = Color.Transparent;
		((Control)button180).BackgroundImage = (Image)(object)Resources.BLCDXZ180;
		((Control)button180).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button180).FlatAppearance.BorderSize = 0;
		((ButtonBase)button180).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button180).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button180).FlatStyle = (FlatStyle)0;
		((Control)button180).Location = new Point(190, 269);
		((Control)button180).Margin = new Padding(0);
		((Control)button180).Name = "button180";
		((Control)button180).Size = new Size(28, 28);
		((Control)button180).TabIndex = 68;
		((ButtonBase)button180).UseVisualStyleBackColor = false;
		((Control)button180).Click += button180_Click;
		((Control)button270).BackColor = Color.Transparent;
		((Control)button270).BackgroundImage = (Image)(object)Resources.BLCDXZ270;
		((Control)button270).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button270).FlatAppearance.BorderSize = 0;
		((ButtonBase)button270).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button270).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button270).FlatStyle = (FlatStyle)0;
		((Control)button270).Location = new Point(261, 269);
		((Control)button270).Margin = new Padding(0);
		((Control)button270).Name = "button270";
		((Control)button270).Size = new Size(28, 28);
		((Control)button270).TabIndex = 69;
		((ButtonBase)button270).UseVisualStyleBackColor = false;
		((Control)button270).Click += button270_Click;
		((Control)label2).BackColor = Color.Transparent;
		label2.FlatStyle = (FlatStyle)0;
		((Control)label2).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
		((Control)label2).ForeColor = Color.FromArgb(141, 149, 163);
		((Control)label2).Location = new Point(773, 275);
		((Control)label2).Margin = new Padding(0);
		((Control)label2).Name = "label2";
		((Control)label2).Size = new Size(40, 25);
		((Control)label2).TabIndex = 70;
		((Control)label2).Text = "文字";
		label2.TextAlign = (ContentAlignment)16;
		((Control)buttonBJYY).BackColor = Color.Transparent;
		((Control)buttonBJYY).BackgroundImage = (Image)(object)Resources.BYLCD;
		((Control)buttonBJYY).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonBJYY).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonBJYY).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonBJYY).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonBJYY).FlatStyle = (FlatStyle)0;
		((Control)buttonBJYY).Location = new Point(780, 342);
		((Control)buttonBJYY).Margin = new Padding(0);
		((Control)buttonBJYY).Name = "buttonBJYY";
		((Control)buttonBJYY).Size = new Size(78, 25);
		((Control)buttonBJYY).TabIndex = 71;
		((ButtonBase)buttonBJYY).UseVisualStyleBackColor = false;
		((Control)buttonBJYY).Click += buttonBJYY_Click;
		((Control)buttonSub).BackColor = Color.Transparent;
		((Control)buttonSub).BackgroundImage = (Image)(object)Resources.BLCDSub;
		((Control)buttonSub).BackgroundImageLayout = (ImageLayout)0;
		((ButtonBase)buttonSub).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonSub).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonSub).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonSub).FlatStyle = (FlatStyle)0;
		((Control)buttonSub).Location = new Point(356, 599);
		((Control)buttonSub).Margin = new Padding(0);
		((Control)buttonSub).Name = "buttonSub";
		((Control)buttonSub).Size = new Size(28, 42);
		((Control)buttonSub).TabIndex = 72;
		((ButtonBase)buttonSub).UseVisualStyleBackColor = false;
		((Control)buttonSub).Click += buttonSub_Click;
		((Control)buttonAdd).BackColor = Color.Transparent;
		((Control)buttonAdd).BackgroundImage = (Image)(object)Resources.BLCDAdd;
		((Control)buttonAdd).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonAdd).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonAdd).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonAdd).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonAdd).FlatStyle = (FlatStyle)0;
		((Control)buttonAdd).Location = new Point(386, 599);
		((Control)buttonAdd).Margin = new Padding(0);
		((Control)buttonAdd).Name = "buttonAdd";
		((Control)buttonAdd).Size = new Size(28, 42);
		((Control)buttonAdd).TabIndex = 73;
		((ButtonBase)buttonAdd).UseVisualStyleBackColor = false;
		((Control)buttonAdd).Click += buttonAdd_Click;
		((Control)ucComboBox21).BackColor = Color.Transparent;
		((Control)ucComboBox21).Location = new Point(495, 534);
		((Control)ucComboBox21).Margin = new Padding(0);
		((Control)ucComboBox21).Name = "ucComboBox21";
		((Control)ucComboBox21).Size = new Size(362, 29);
		((Control)ucComboBox21).TabIndex = 51;
		((Control)ucComboBox22).BackColor = Color.Transparent;
		((Control)ucComboBox22).Location = new Point(495, 573);
		((Control)ucComboBox22).Margin = new Padding(0);
		((Control)ucComboBox22).Name = "ucComboBox22";
		((Control)ucComboBox22).Size = new Size(362, 29);
		((Control)ucComboBox22).TabIndex = 52;
		((Control)ucComboBox23).BackColor = Color.Transparent;
		((Control)ucComboBox23).Location = new Point(495, 612);
		((Control)ucComboBox23).Margin = new Padding(0);
		((Control)ucComboBox23).Name = "ucComboBox23";
		((Control)ucComboBox23).Size = new Size(362, 29);
		((Control)ucComboBox23).TabIndex = 53;
		((Control)uclcdDisplay25).BackColor = Color.FromArgb(26, 29, 35);
		((Control)uclcdDisplay25).Location = new Point(298, 599);
		((Control)uclcdDisplay25).Margin = new Padding(0);
		((Control)uclcdDisplay25).Name = "uclcdDisplay25";
		((Control)uclcdDisplay25).Size = new Size(56, 42);
		((Control)uclcdDisplay25).TabIndex = 32;
		((Control)uclcdDisplay25).Visible = false;
		((Control)uclcdDisplay25).Click += uclcdDisplay25_Click;
		((Control)uclcdDisplay24).BackColor = Color.FromArgb(26, 29, 35);
		((Control)uclcdDisplay24).Location = new Point(241, 599);
		((Control)uclcdDisplay24).Margin = new Padding(0);
		((Control)uclcdDisplay24).Name = "uclcdDisplay24";
		((Control)uclcdDisplay24).Size = new Size(56, 42);
		((Control)uclcdDisplay24).TabIndex = 31;
		((Control)uclcdDisplay24).Visible = false;
		((Control)uclcdDisplay24).Click += uclcdDisplay24_Click;
		((Control)uclcdDisplay23).BackColor = Color.FromArgb(26, 29, 35);
		((Control)uclcdDisplay23).Location = new Point(184, 599);
		((Control)uclcdDisplay23).Margin = new Padding(0);
		((Control)uclcdDisplay23).Name = "uclcdDisplay23";
		((Control)uclcdDisplay23).Size = new Size(56, 42);
		((Control)uclcdDisplay23).TabIndex = 30;
		((Control)uclcdDisplay23).Visible = false;
		((Control)uclcdDisplay23).Click += uclcdDisplay23_Click;
		((Control)uclcdDisplay22).BackColor = Color.FromArgb(26, 29, 35);
		((Control)uclcdDisplay22).Location = new Point(127, 599);
		((Control)uclcdDisplay22).Margin = new Padding(0);
		((Control)uclcdDisplay22).Name = "uclcdDisplay22";
		((Control)uclcdDisplay22).Size = new Size(56, 42);
		((Control)uclcdDisplay22).TabIndex = 29;
		((Control)uclcdDisplay22).Visible = false;
		((Control)uclcdDisplay22).Click += uclcdDisplay22_Click;
		((Control)uclcdDisplay21).BackColor = Color.FromArgb(26, 29, 35);
		((Control)uclcdDisplay21).Location = new Point(70, 599);
		((Control)uclcdDisplay21).Margin = new Padding(0);
		((Control)uclcdDisplay21).Name = "uclcdDisplay21";
		((Control)uclcdDisplay21).Size = new Size(56, 42);
		((Control)uclcdDisplay21).TabIndex = 28;
		((Control)uclcdDisplay21).Click += uclcdDisplay21_Click;
		((Control)uclcdDisplay11).BackColor = Color.Transparent;
		((Control)uclcdDisplay11).BackgroundImageLayout = (ImageLayout)3;
		((Control)uclcdDisplay11).Location = new Point(112, 364);
		((Control)uclcdDisplay11).Margin = new Padding(0);
		((Control)uclcdDisplay11).Name = "uclcdDisplay11";
		((Control)uclcdDisplay11).Size = new Size(260, 192);
		((Control)uclcdDisplay11).TabIndex = 27;
		((Control)buttonZYGLQ).BackColor = Color.Transparent;
		((Control)buttonZYGLQ).BackgroundImage = (Image)(object)Resources.BLCDZYGLQ0;
		((Control)buttonZYGLQ).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonZYGLQ).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonZYGLQ).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonZYGLQ).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonZYGLQ).FlatStyle = (FlatStyle)0;
		((Control)buttonZYGLQ).Location = new Point(367, 669);
		((Control)buttonZYGLQ).Margin = new Padding(0);
		((Control)buttonZYGLQ).Name = "buttonZYGLQ";
		((Control)buttonZYGLQ).Size = new Size(78, 25);
		((Control)buttonZYGLQ).TabIndex = 74;
		((ButtonBase)buttonZYGLQ).UseVisualStyleBackColor = false;
		((Control)buttonZYGLQ).Click += buttonZYGLQ_Click;
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)3;
		((Control)this).BackColor = Color.White;
		((Control)this).BackgroundImage = (Image)(object)Resources.Background6_1;
		((Control)this).BackgroundImageLayout = (ImageLayout)3;
		((Form)this).ClientSize = new Size(916, 800);
		((Control)this).Controls.Add((Control)(object)buttonZYGLQ);
		((Control)this).Controls.Add((Control)(object)buttonAdd);
		((Control)this).Controls.Add((Control)(object)buttonSub);
		((Control)this).Controls.Add((Control)(object)buttonBJYY);
		((Control)this).Controls.Add((Control)(object)label2);
		((Control)this).Controls.Add((Control)(object)button270);
		((Control)this).Controls.Add((Control)(object)button180);
		((Control)this).Controls.Add((Control)(object)button090);
		((Control)this).Controls.Add((Control)(object)button000);
		((Control)this).Controls.Add((Control)(object)labelSetting);
		((Control)this).Controls.Add((Control)(object)labelMP4);
		((Control)this).Controls.Add((Control)(object)buttonOnOff);
		((Control)this).Controls.Add((Control)(object)button3);
		((Control)this).Controls.Add((Control)(object)button2);
		((Control)this).Controls.Add((Control)(object)button1);
		((Control)this).Controls.Add((Control)(object)ucComboBox21);
		((Control)this).Controls.Add((Control)(object)ucComboBox22);
		((Control)this).Controls.Add((Control)(object)ucComboBox23);
		((Control)this).Controls.Add((Control)(object)buttonLCD);
		((Control)this).Controls.Add((Control)(object)label1);
		((Control)this).Controls.Add((Control)(object)uclcdDisplay25);
		((Control)this).Controls.Add((Control)(object)uclcdDisplay24);
		((Control)this).Controls.Add((Control)(object)uclcdDisplay23);
		((Control)this).Controls.Add((Control)(object)uclcdDisplay22);
		((Control)this).Controls.Add((Control)(object)uclcdDisplay21);
		((Control)this).Controls.Add((Control)(object)uclcdDisplay11);
		((Control)this).Controls.Add((Control)(object)textBox5);
		((Control)this).Controls.Add((Control)(object)textBox4);
		((Control)this).Controls.Add((Control)(object)textBox3);
		((Control)this).Controls.Add((Control)(object)textBox2);
		((Control)this).Controls.Add((Control)(object)textBox1);
		((Control)this).Controls.Add((Control)(object)buttonTimer);
		((Control)this).Controls.Add((Control)(object)buttonZDY);
		((Control)this).Controls.Add((Control)(object)buttonMR);
		((Control)this).Controls.Add((Control)(object)buttonYY);
		((Control)this).Controls.Add((Control)(object)buttonBlack);
		((Control)this).Controls.Add((Control)(object)buttonWhite);
		((Control)this).DoubleBuffered = true;
		((Control)this).Font = new Font("微软雅黑", 10.5f);
		((Form)this).FormBorderStyle = (FormBorderStyle)0;
		((Form)this).Location = new Point(89, 0);
		((Form)this).MaximizeBox = false;
		((Form)this).MinimizeBox = false;
		((Control)this).Name = "FormDeviceLCD";
		((Control)this).Text = "FormDeviceLCD";
		((Form)this).FormClosing += new FormClosingEventHandler(FormDeviceLCD_FormClosing);
		((Control)this).MouseDown += new MouseEventHandler(FormDeviceLCD_MouseDown);
		((Control)this).MouseMove += new MouseEventHandler(FormDeviceLCD_MouseMove);
		((Control)this).MouseUp += new MouseEventHandler(FormDeviceLCD_MouseUp);
		((Control)this).ResumeLayout(false);
		((Control)this).PerformLayout();
	}
}
