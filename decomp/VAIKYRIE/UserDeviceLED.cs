using System;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using VAIKYRIE.Properties;

namespace VAIKYRIE;

public class UserDeviceLED : UserControl
{
	public delegate void delegateUserDeviceLED(int ch, int cmd, object obj);

	public delegate void DoInitMyDataInvoke(byte[] d);

	public int myMode = 1;

	public int myChannel = 0;

	public int rgbR1 = 255;

	public int rgbG1 = 0;

	public int rgbB1 = 0;

	public int myBrightness = 100;

	public int mySpeed = 1;

	private bool isFirst = true;

	private readonly int[] speedVal = new int[5] { 100, 100, 50, 1, 1 };

	public delegateUserDeviceLED ucDeviceLEDF;

	private bool isSendData = false;

	private IContainer components = null;

	private Button buttonYYMS;

	private Button buttonCHLS;

	private Button buttonQCZZ;

	private Button buttonZYJC;

	private Button buttonSXPM;

	private Button buttonQSPM;

	private Button buttonCHMS;

	private Button buttonXGSS;

	private Button buttonDSBS;

	private Button buttonDSXH;

	private Button buttonHXMS;

	private Button buttonJTDS;

	private Button button1;

	private Button button2;

	private Button button3;

	private Button button4;

	private Button button5;

	private Button button6;

	private Button button7;

	private Button button8;

	private Button buttonCA;

	private Button buttonC1;

	private Button buttonC2;

	private Button buttonC3;

	private Button buttonC4;

	private Button buttonCM;

	private UCScroll1 ucScroll11;

	private UCScroll1 ucScroll12;

	public Label label1;

	public Label label2;

	public Label label3;

	public Label label4;

	public Label label5;

	public Label label6;

	private UCColor1 ucColor11;

	public Label labelALL;

	public Label labelSub;

	private Button buttonWBSR;

	protected override CreateParams CreateParams
	{
		get
		{
			CreateParams createParams = ((UserControl)this).CreateParams;
			createParams.ExStyle |= 0x2000000;
			return createParams;
		}
	}

	private void ClearButtonBouns()
	{
		((ButtonBase)button1).FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
		((ButtonBase)button2).FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
		((ButtonBase)button3).FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
		((ButtonBase)button4).FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
		((ButtonBase)button5).FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
		((ButtonBase)button6).FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
		((ButtonBase)button7).FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
		((ButtonBase)button8).FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
	}

	public UserDeviceLED()
	{
		InitializeComponent();
		ucScroll11.upDateUCScroll1 = UpDateUCScroll1;
		ucScroll12.upDateUCScroll1 = UpDateUCScroll2;
		ucColor11.upDateUCColor1 = UpDateUCColor;
		ClearButtonBouns();
	}

	private void UpDateUCScroll1(int val)
	{
		myBrightness = val;
		((Control)label1).Text = val.ToString();
		UpDateUCColor1(rgbR1, rgbG1, rgbB1);
		SendLEDData();
	}

	private void UpDateUCScroll2(int val)
	{
		if (val < 33)
		{
			mySpeed = 4;
			((Control)label2).Text = "慢";
		}
		else if (val < 66)
		{
			mySpeed = 2;
			((Control)label2).Text = "中";
		}
		else
		{
			mySpeed = 1;
			((Control)label2).Text = "快";
		}
		SendLEDData();
	}

	private void UpDateUCColor1(int r, int g, int b)
	{
		rgbR1 = r;
		rgbG1 = g;
		rgbB1 = b;
		((Control)label3).Text = r.ToString();
		((Control)label4).Text = g.ToString();
		((Control)label5).Text = b.ToString();
		((Control)label6).Text = $"{rgbR1:X2}{rgbG1:X2}{rgbB1:X2}";
		((Control)labelALL).BackColor = Color.FromArgb(r, g, b);
		((Control)labelSub).BackColor = Color.FromArgb(r * myBrightness / 100, g * myBrightness / 100, b * myBrightness / 100);
	}

	private void UpDateUCColor(int r, int g, int b)
	{
		UpDateUCColor1(r, g, b);
		SendLEDData();
	}

	private void ButtonColor_Click(object sender, EventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Expected O, but got Unknown
		Button val = (Button)sender;
		int num = Convert.ToInt32(((Control)val).Name.Substring(6));
		((Control)button1).BackgroundImage = (Image)(object)Resources.BCR1;
		((Control)button2).BackgroundImage = (Image)(object)Resources.BCO1;
		((Control)button3).BackgroundImage = (Image)(object)Resources.BCY1;
		((Control)button4).BackgroundImage = (Image)(object)Resources.BCG1;
		((Control)button5).BackgroundImage = (Image)(object)Resources.BCC1;
		((Control)button6).BackgroundImage = (Image)(object)Resources.BCB1;
		((Control)button7).BackgroundImage = (Image)(object)Resources.BCP1;
		((Control)button8).BackgroundImage = (Image)(object)Resources.BCW1;
		switch (num)
		{
		case 1:
			UpDateUCColor1(255, 0, 0);
			((Control)button1).BackgroundImage = (Image)(object)Resources.BCR2;
			break;
		case 2:
			UpDateUCColor1(255, 127, 0);
			((Control)button2).BackgroundImage = (Image)(object)Resources.BCO2;
			break;
		case 3:
			UpDateUCColor1(255, 255, 0);
			((Control)button3).BackgroundImage = (Image)(object)Resources.BCY2;
			break;
		case 4:
			UpDateUCColor1(127, 255, 0);
			((Control)button4).BackgroundImage = (Image)(object)Resources.BCG2;
			break;
		case 5:
			UpDateUCColor1(0, 255, 255);
			((Control)button5).BackgroundImage = (Image)(object)Resources.BCC2;
			break;
		case 6:
			UpDateUCColor1(0, 127, 255);
			((Control)button6).BackgroundImage = (Image)(object)Resources.BCB2;
			break;
		case 7:
			UpDateUCColor1(255, 0, 255);
			((Control)button7).BackgroundImage = (Image)(object)Resources.BCP2;
			break;
		case 8:
			UpDateUCColor1(255, 255, 255);
			((Control)button8).BackgroundImage = (Image)(object)Resources.BCW2;
			break;
		}
		SendLEDData();
	}

	private void ButtonMode_Click(int mode)
	{
		((Control)buttonJTDS).BackgroundImage = (Image)(object)Resources.Bjtds2;
		((Control)buttonHXMS).BackgroundImage = (Image)(object)Resources.Bhxms2;
		((Control)buttonDSXH).BackgroundImage = (Image)(object)Resources.Bdsxh2;
		((Control)buttonDSBS).BackgroundImage = (Image)(object)Resources.Bdsbs2;
		((Control)buttonXGSS).BackgroundImage = (Image)(object)Resources.Bxgss2;
		((Control)buttonCHMS).BackgroundImage = (Image)(object)Resources.Bchms2;
		((Control)buttonQSPM).BackgroundImage = (Image)(object)Resources.Bqspm2;
		((Control)buttonSXPM).BackgroundImage = (Image)(object)Resources.Bsxpm2;
		((Control)buttonZYJC).BackgroundImage = (Image)(object)Resources.Bzyjc2;
		((Control)buttonQCZZ).BackgroundImage = (Image)(object)Resources.Bahms2;
		((Control)buttonCHLS).BackgroundImage = (Image)(object)Resources.Bchls2;
		((Control)buttonYYMS).BackgroundImage = (Image)(object)Resources.Byyms2;
		((Control)buttonWBSR).BackgroundImage = (Image)(object)Resources.Bwbsr2;
		switch (mode)
		{
		case 1:
			((Control)buttonJTDS).BackgroundImage = (Image)(object)Resources.Bjtds1;
			break;
		case 2:
			((Control)buttonHXMS).BackgroundImage = (Image)(object)Resources.Bhxms1;
			break;
		case 3:
			((Control)buttonDSXH).BackgroundImage = (Image)(object)Resources.Bdsxh1;
			break;
		case 4:
			((Control)buttonDSBS).BackgroundImage = (Image)(object)Resources.Bdsbs1;
			break;
		case 5:
			((Control)buttonXGSS).BackgroundImage = (Image)(object)Resources.Bxgss1;
			break;
		case 6:
			((Control)buttonCHMS).BackgroundImage = (Image)(object)Resources.Bchms1;
			break;
		case 7:
			((Control)buttonQSPM).BackgroundImage = (Image)(object)Resources.Bqspm1;
			break;
		case 8:
			((Control)buttonSXPM).BackgroundImage = (Image)(object)Resources.Bsxpm1;
			break;
		case 9:
			((Control)buttonZYJC).BackgroundImage = (Image)(object)Resources.Bzyjc1;
			break;
		case 10:
			((Control)buttonQCZZ).BackgroundImage = (Image)(object)Resources.Bahms1;
			break;
		case 11:
			((Control)buttonCHLS).BackgroundImage = (Image)(object)Resources.Bchls1;
			break;
		case 12:
			((Control)buttonYYMS).BackgroundImage = (Image)(object)Resources.Byyms1;
			break;
		case 100:
			((Control)buttonWBSR).BackgroundImage = (Image)(object)Resources.Bwbsr1;
			break;
		}
	}

	private void buttonJTDS_Click(object sender, EventArgs e)
	{
		myMode = 1;
		ButtonMode_Click(myMode);
		SendLEDData();
	}

	private void buttonHXMS_Click(object sender, EventArgs e)
	{
		myMode = 2;
		ButtonMode_Click(myMode);
		SendLEDData();
	}

	private void buttonDSXH_Click(object sender, EventArgs e)
	{
		myMode = 3;
		ButtonMode_Click(myMode);
		SendLEDData();
	}

	private void buttonDSBS_Click(object sender, EventArgs e)
	{
		myMode = 4;
		ButtonMode_Click(myMode);
		SendLEDData();
	}

	private void buttonXGSS_Click(object sender, EventArgs e)
	{
		myMode = 5;
		ButtonMode_Click(myMode);
		SendLEDData();
	}

	private void buttonCHMS_Click(object sender, EventArgs e)
	{
		myMode = 6;
		ButtonMode_Click(myMode);
		SendLEDData();
	}

	private void buttonQSPM_Click(object sender, EventArgs e)
	{
		myMode = 7;
		ButtonMode_Click(myMode);
		SendLEDData();
	}

	private void buttonSXPM_Click(object sender, EventArgs e)
	{
		myMode = 8;
		ButtonMode_Click(myMode);
		SendLEDData();
	}

	private void buttonZYJC_Click(object sender, EventArgs e)
	{
		myMode = 9;
		ButtonMode_Click(myMode);
		SendLEDData();
	}

	private void buttonQCZZ_Click(object sender, EventArgs e)
	{
		myMode = 10;
		ButtonMode_Click(myMode);
		SendLEDData1();
	}

	private void buttonCHLS_Click(object sender, EventArgs e)
	{
		myMode = 11;
		ButtonMode_Click(myMode);
		SendLEDData();
	}

	private void buttonYYMS_Click(object sender, EventArgs e)
	{
		myMode = 12;
		ButtonMode_Click(myMode);
		SendLEDData();
	}

	private void buttonWBSR_Click(object sender, EventArgs e)
	{
		myMode = 100;
		ButtonMode_Click(myMode);
		SendLEDData();
	}

	private void ButtonC_Click(int ch)
	{
		((Control)buttonCA).BackgroundImage = (Image)(object)Resources.BCHA2;
		((Control)buttonC1).BackgroundImage = (Image)(object)Resources.BCH12;
		((Control)buttonC2).BackgroundImage = (Image)(object)Resources.BCH22;
		((Control)buttonC3).BackgroundImage = (Image)(object)Resources.BCH32;
		((Control)buttonC4).BackgroundImage = (Image)(object)Resources.BCH42;
		((Control)buttonCM).BackgroundImage = (Image)(object)Resources.BCHM2;
		switch (ch)
		{
		case 0:
			((Control)buttonCA).BackgroundImage = (Image)(object)Resources.BCHA1;
			break;
		case 1:
			((Control)buttonC1).BackgroundImage = (Image)(object)Resources.BCH11;
			break;
		case 2:
			((Control)buttonC2).BackgroundImage = (Image)(object)Resources.BCH21;
			break;
		case 3:
			((Control)buttonC3).BackgroundImage = (Image)(object)Resources.BCH31;
			break;
		case 4:
			((Control)buttonC4).BackgroundImage = (Image)(object)Resources.BCH41;
			break;
		case 5:
			((Control)buttonCM).BackgroundImage = (Image)(object)Resources.BCHM1;
			break;
		}
	}

	private void buttonCA_Click(object sender, EventArgs e)
	{
		myChannel = 0;
		ButtonC_Click(myChannel);
	}

	private void buttonC1_Click(object sender, EventArgs e)
	{
		myChannel = 1;
		ButtonC_Click(myChannel);
	}

	private void buttonC2_Click(object sender, EventArgs e)
	{
		myChannel = 2;
		ButtonC_Click(myChannel);
	}

	private void buttonC3_Click(object sender, EventArgs e)
	{
		myChannel = 3;
		ButtonC_Click(myChannel);
	}

	private void buttonC4_Click(object sender, EventArgs e)
	{
		myChannel = 4;
		ButtonC_Click(myChannel);
	}

	private void buttonCM_Click(object sender, EventArgs e)
	{
		myChannel = 5;
		ButtonC_Click(myChannel);
	}

	private void SendLEDData()
	{
		byte[] obj = new byte[6]
		{
			(byte)myBrightness,
			(byte)mySpeed,
			(byte)rgbR1,
			(byte)rgbG1,
			(byte)rgbB1,
			0
		};
		ucDeviceLEDF?.Invoke(myChannel, myMode, obj);
		isSendData = true;
	}

	private void SendLEDData1()
	{
		byte[] obj = new byte[6]
		{
			0,
			(byte)mySpeed,
			0,
			0,
			0,
			0
		};
		ucDeviceLEDF?.Invoke(myChannel, 1, obj);
		isSendData = true;
	}

	public void ReceivedLEDData(byte[] data)
	{
		if (isFirst)
		{
			isFirst = false;
			if (data.Length != 0)
			{
				Thread thread = new Thread(InitMyData);
				thread.Start(data);
			}
			if (data.Length != 0 && isSendData)
			{
				isSendData = false;
			}
		}
	}

	public void InitMyData(object b)
	{
		DoInitMyDataInvoke doInitMyDataInvoke = DoInitMyData;
		((Control)this).BeginInvoke((Delegate)doInitMyDataInvoke, new object[1] { b });
	}

	public void DoInitMyData(byte[] d)
	{
		myMode = d[27];
		ButtonMode_Click(myMode);
		myBrightness = d[28];
		((Control)label1).Text = myBrightness.ToString();
		ucScroll11.setUCScroll1(myBrightness);
		mySpeed = d[29];
		if (mySpeed > 4)
		{
			mySpeed = 4;
		}
		ucScroll12.setUCScroll1(speedVal[mySpeed]);
		rgbR1 = d[30];
		rgbG1 = d[31];
		rgbB1 = d[32];
		UpDateUCColor1(rgbR1, rgbG1, rgbB1);
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
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Expected O, but got Unknown
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Expected O, but got Unknown
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Expected O, but got Unknown
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Expected O, but got Unknown
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Expected O, but got Unknown
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Expected O, but got Unknown
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Expected O, but got Unknown
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Expected O, but got Unknown
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Expected O, but got Unknown
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Expected O, but got Unknown
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected O, but got Unknown
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Expected O, but got Unknown
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Expected O, but got Unknown
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Expected O, but got Unknown
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Expected O, but got Unknown
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Expected O, but got Unknown
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Expected O, but got Unknown
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Expected O, but got Unknown
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Expected O, but got Unknown
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Expected O, but got Unknown
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Expected O, but got Unknown
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Expected O, but got Unknown
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Expected O, but got Unknown
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Expected O, but got Unknown
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Expected O, but got Unknown
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Expected O, but got Unknown
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Expected O, but got Unknown
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Expected O, but got Unknown
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Unknown result type (might be due to invalid IL or missing references)
		//IL_065c: Unknown result type (might be due to invalid IL or missing references)
		//IL_075c: Unknown result type (might be due to invalid IL or missing references)
		//IL_085f: Unknown result type (might be due to invalid IL or missing references)
		//IL_095f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_105c: Unknown result type (might be due to invalid IL or missing references)
		//IL_115c: Unknown result type (might be due to invalid IL or missing references)
		//IL_125c: Unknown result type (might be due to invalid IL or missing references)
		//IL_135c: Unknown result type (might be due to invalid IL or missing references)
		//IL_145c: Unknown result type (might be due to invalid IL or missing references)
		//IL_155c: Unknown result type (might be due to invalid IL or missing references)
		//IL_165c: Unknown result type (might be due to invalid IL or missing references)
		//IL_175c: Unknown result type (might be due to invalid IL or missing references)
		//IL_185c: Unknown result type (might be due to invalid IL or missing references)
		//IL_195c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bf5: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bff: Expected O, but got Unknown
		//IL_1c33: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cd0: Expected O, but got Unknown
		//IL_1d04: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d97: Unknown result type (might be due to invalid IL or missing references)
		//IL_1da1: Expected O, but got Unknown
		//IL_1dd2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e65: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e6f: Expected O, but got Unknown
		//IL_1ea0: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f33: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f3d: Expected O, but got Unknown
		//IL_1f6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_2001: Unknown result type (might be due to invalid IL or missing references)
		//IL_200b: Expected O, but got Unknown
		//IL_203f: Unknown result type (might be due to invalid IL or missing references)
		//IL_20d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_20dc: Expected O, but got Unknown
		//IL_2110: Unknown result type (might be due to invalid IL or missing references)
		//IL_2192: Unknown result type (might be due to invalid IL or missing references)
		//IL_219c: Expected O, but got Unknown
		//IL_21d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_22b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_233f: Unknown result type (might be due to invalid IL or missing references)
		//IL_2349: Expected O, but got Unknown
		//IL_2376: Unknown result type (might be due to invalid IL or missing references)
		//IL_23f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_2477: Unknown result type (might be due to invalid IL or missing references)
		//IL_279f: Unknown result type (might be due to invalid IL or missing references)
		//IL_27a9: Expected O, but got Unknown
		//IL_27be: Unknown result type (might be due to invalid IL or missing references)
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(UserDeviceLED));
		buttonYYMS = new Button();
		buttonCHLS = new Button();
		buttonQCZZ = new Button();
		buttonZYJC = new Button();
		buttonSXPM = new Button();
		buttonQSPM = new Button();
		buttonCHMS = new Button();
		buttonXGSS = new Button();
		buttonDSBS = new Button();
		buttonDSXH = new Button();
		buttonHXMS = new Button();
		buttonJTDS = new Button();
		button1 = new Button();
		button2 = new Button();
		button3 = new Button();
		button4 = new Button();
		button5 = new Button();
		button6 = new Button();
		button7 = new Button();
		button8 = new Button();
		buttonCA = new Button();
		buttonC1 = new Button();
		buttonC2 = new Button();
		buttonC3 = new Button();
		buttonC4 = new Button();
		buttonCM = new Button();
		label1 = new Label();
		label2 = new Label();
		label3 = new Label();
		label4 = new Label();
		label5 = new Label();
		label6 = new Label();
		labelALL = new Label();
		labelSub = new Label();
		buttonWBSR = new Button();
		ucColor11 = new UCColor1();
		ucScroll12 = new UCScroll1();
		ucScroll11 = new UCScroll1();
		((Control)this).SuspendLayout();
		((Control)buttonYYMS).BackColor = Color.Transparent;
		((Control)buttonYYMS).BackgroundImage = (Image)(object)Resources.Byyms2;
		((Control)buttonYYMS).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonYYMS).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonYYMS).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonYYMS).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonYYMS).FlatStyle = (FlatStyle)0;
		((Control)buttonYYMS).Location = new Point(237, 256);
		((Control)buttonYYMS).Margin = new Padding(0);
		((Control)buttonYYMS).Name = "buttonYYMS";
		((Control)buttonYYMS).Size = new Size(133, 27);
		((Control)buttonYYMS).TabIndex = 37;
		((ButtonBase)buttonYYMS).UseVisualStyleBackColor = false;
		((Control)buttonYYMS).Click += buttonYYMS_Click;
		((Control)buttonCHLS).BackColor = Color.Transparent;
		((Control)buttonCHLS).BackgroundImage = (Image)(object)Resources.Bchls2;
		((Control)buttonCHLS).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonCHLS).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonCHLS).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonCHLS).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonCHLS).FlatStyle = (FlatStyle)0;
		((Control)buttonCHLS).Location = new Point(67, 256);
		((Control)buttonCHLS).Margin = new Padding(0);
		((Control)buttonCHLS).Name = "buttonCHLS";
		((Control)buttonCHLS).Size = new Size(133, 27);
		((Control)buttonCHLS).TabIndex = 36;
		((ButtonBase)buttonCHLS).UseVisualStyleBackColor = false;
		((Control)buttonCHLS).Click += buttonCHLS_Click;
		((Control)buttonQCZZ).BackColor = Color.Transparent;
		((Control)buttonQCZZ).BackgroundImage = (Image)(object)Resources.Bahms2;
		((Control)buttonQCZZ).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonQCZZ).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonQCZZ).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonQCZZ).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonQCZZ).FlatStyle = (FlatStyle)0;
		((Control)buttonQCZZ).Location = new Point(237, 215);
		((Control)buttonQCZZ).Margin = new Padding(0);
		((Control)buttonQCZZ).Name = "buttonQCZZ";
		((Control)buttonQCZZ).Size = new Size(133, 27);
		((Control)buttonQCZZ).TabIndex = 35;
		((ButtonBase)buttonQCZZ).UseVisualStyleBackColor = false;
		((Control)buttonQCZZ).Click += buttonQCZZ_Click;
		((Control)buttonZYJC).BackColor = Color.Transparent;
		((Control)buttonZYJC).BackgroundImage = (Image)(object)Resources.Bzyjc2;
		((Control)buttonZYJC).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonZYJC).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonZYJC).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonZYJC).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonZYJC).FlatStyle = (FlatStyle)0;
		((Control)buttonZYJC).Location = new Point(67, 215);
		((Control)buttonZYJC).Margin = new Padding(0);
		((Control)buttonZYJC).Name = "buttonZYJC";
		((Control)buttonZYJC).Size = new Size(133, 27);
		((Control)buttonZYJC).TabIndex = 34;
		((ButtonBase)buttonZYJC).UseVisualStyleBackColor = false;
		((Control)buttonZYJC).Click += buttonZYJC_Click;
		((Control)buttonSXPM).BackColor = Color.Transparent;
		((Control)buttonSXPM).BackgroundImage = (Image)(object)Resources.Bsxpm2;
		((Control)buttonSXPM).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonSXPM).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonSXPM).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonSXPM).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonSXPM).FlatStyle = (FlatStyle)0;
		((Control)buttonSXPM).Location = new Point(237, 174);
		((Control)buttonSXPM).Margin = new Padding(0);
		((Control)buttonSXPM).Name = "buttonSXPM";
		((Control)buttonSXPM).Size = new Size(133, 27);
		((Control)buttonSXPM).TabIndex = 33;
		((ButtonBase)buttonSXPM).UseVisualStyleBackColor = false;
		((Control)buttonSXPM).Click += buttonSXPM_Click;
		((Control)buttonQSPM).BackColor = Color.Transparent;
		((Control)buttonQSPM).BackgroundImage = (Image)(object)Resources.Bqspm2;
		((Control)buttonQSPM).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonQSPM).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonQSPM).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonQSPM).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonQSPM).FlatStyle = (FlatStyle)0;
		((Control)buttonQSPM).Location = new Point(67, 174);
		((Control)buttonQSPM).Margin = new Padding(0);
		((Control)buttonQSPM).Name = "buttonQSPM";
		((Control)buttonQSPM).Size = new Size(133, 27);
		((Control)buttonQSPM).TabIndex = 32;
		((ButtonBase)buttonQSPM).UseVisualStyleBackColor = false;
		((Control)buttonQSPM).Click += buttonQSPM_Click;
		((Control)buttonCHMS).BackColor = Color.Transparent;
		((Control)buttonCHMS).BackgroundImage = (Image)(object)Resources.Bchms2;
		((Control)buttonCHMS).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonCHMS).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonCHMS).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonCHMS).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonCHMS).FlatStyle = (FlatStyle)0;
		((Control)buttonCHMS).Location = new Point(237, 133);
		((Control)buttonCHMS).Margin = new Padding(0);
		((Control)buttonCHMS).Name = "buttonCHMS";
		((Control)buttonCHMS).Size = new Size(133, 27);
		((Control)buttonCHMS).TabIndex = 31;
		((ButtonBase)buttonCHMS).UseVisualStyleBackColor = false;
		((Control)buttonCHMS).Click += buttonCHMS_Click;
		((Control)buttonXGSS).BackColor = Color.Transparent;
		((Control)buttonXGSS).BackgroundImage = (Image)(object)Resources.Bxgss2;
		((Control)buttonXGSS).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonXGSS).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonXGSS).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonXGSS).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonXGSS).FlatStyle = (FlatStyle)0;
		((Control)buttonXGSS).Location = new Point(67, 133);
		((Control)buttonXGSS).Margin = new Padding(0);
		((Control)buttonXGSS).Name = "buttonXGSS";
		((Control)buttonXGSS).Size = new Size(133, 27);
		((Control)buttonXGSS).TabIndex = 30;
		((ButtonBase)buttonXGSS).UseVisualStyleBackColor = false;
		((Control)buttonXGSS).Click += buttonXGSS_Click;
		((Control)buttonDSBS).BackColor = Color.Transparent;
		((Control)buttonDSBS).BackgroundImage = (Image)(object)Resources.Bdsbs2;
		((Control)buttonDSBS).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonDSBS).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonDSBS).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonDSBS).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonDSBS).FlatStyle = (FlatStyle)0;
		((Control)buttonDSBS).Location = new Point(237, 92);
		((Control)buttonDSBS).Margin = new Padding(0);
		((Control)buttonDSBS).Name = "buttonDSBS";
		((Control)buttonDSBS).Size = new Size(133, 27);
		((Control)buttonDSBS).TabIndex = 29;
		((ButtonBase)buttonDSBS).UseVisualStyleBackColor = false;
		((Control)buttonDSBS).Click += buttonDSBS_Click;
		((Control)buttonDSXH).BackColor = Color.Transparent;
		((Control)buttonDSXH).BackgroundImage = (Image)(object)Resources.Bdsxh2;
		((Control)buttonDSXH).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonDSXH).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonDSXH).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonDSXH).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonDSXH).FlatStyle = (FlatStyle)0;
		((Control)buttonDSXH).Location = new Point(67, 92);
		((Control)buttonDSXH).Margin = new Padding(0);
		((Control)buttonDSXH).Name = "buttonDSXH";
		((Control)buttonDSXH).Size = new Size(133, 27);
		((Control)buttonDSXH).TabIndex = 28;
		((ButtonBase)buttonDSXH).UseVisualStyleBackColor = false;
		((Control)buttonDSXH).Click += buttonDSXH_Click;
		((Control)buttonHXMS).BackColor = Color.Transparent;
		((Control)buttonHXMS).BackgroundImage = (Image)(object)Resources.Bhxms2;
		((Control)buttonHXMS).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonHXMS).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonHXMS).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonHXMS).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonHXMS).FlatStyle = (FlatStyle)0;
		((Control)buttonHXMS).Location = new Point(237, 51);
		((Control)buttonHXMS).Margin = new Padding(0);
		((Control)buttonHXMS).Name = "buttonHXMS";
		((Control)buttonHXMS).Size = new Size(133, 27);
		((Control)buttonHXMS).TabIndex = 27;
		((ButtonBase)buttonHXMS).UseVisualStyleBackColor = false;
		((Control)buttonHXMS).Click += buttonHXMS_Click;
		((Control)buttonJTDS).BackColor = Color.Transparent;
		((Control)buttonJTDS).BackgroundImage = (Image)(object)Resources.Bjtds1;
		((Control)buttonJTDS).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonJTDS).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonJTDS).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonJTDS).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonJTDS).FlatStyle = (FlatStyle)0;
		((Control)buttonJTDS).Location = new Point(67, 51);
		((Control)buttonJTDS).Margin = new Padding(0);
		((Control)buttonJTDS).Name = "buttonJTDS";
		((Control)buttonJTDS).Size = new Size(133, 27);
		((Control)buttonJTDS).TabIndex = 26;
		((ButtonBase)buttonJTDS).UseVisualStyleBackColor = false;
		((Control)buttonJTDS).Click += buttonJTDS_Click;
		((Control)button1).BackColor = Color.Transparent;
		((Control)button1).BackgroundImage = (Image)(object)Resources.BCR2;
		((Control)button1).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button1).FlatAppearance.BorderSize = 0;
		((ButtonBase)button1).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button1).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button1).FlatStyle = (FlatStyle)0;
		((Control)button1).Location = new Point(600, 343);
		((Control)button1).Margin = new Padding(0);
		((Control)button1).Name = "button1";
		((Control)button1).Size = new Size(30, 31);
		((Control)button1).TabIndex = 38;
		((ButtonBase)button1).UseVisualStyleBackColor = false;
		((Control)button1).Click += ButtonColor_Click;
		((Control)button2).BackColor = Color.Transparent;
		((Control)button2).BackgroundImage = (Image)(object)Resources.BCO1;
		((Control)button2).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button2).FlatAppearance.BorderSize = 0;
		((ButtonBase)button2).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button2).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button2).FlatStyle = (FlatStyle)0;
		((Control)button2).Location = new Point(657, 343);
		((Control)button2).Margin = new Padding(0);
		((Control)button2).Name = "button2";
		((Control)button2).Size = new Size(30, 31);
		((Control)button2).TabIndex = 39;
		((ButtonBase)button2).UseVisualStyleBackColor = false;
		((Control)button2).Click += ButtonColor_Click;
		((Control)button3).BackColor = Color.Transparent;
		((Control)button3).BackgroundImage = (Image)(object)Resources.BCY1;
		((Control)button3).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button3).FlatAppearance.BorderSize = 0;
		((ButtonBase)button3).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button3).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button3).FlatStyle = (FlatStyle)0;
		((Control)button3).Location = new Point(714, 343);
		((Control)button3).Margin = new Padding(0);
		((Control)button3).Name = "button3";
		((Control)button3).Size = new Size(30, 31);
		((Control)button3).TabIndex = 40;
		((ButtonBase)button3).UseVisualStyleBackColor = false;
		((Control)button3).Click += ButtonColor_Click;
		((Control)button4).BackColor = Color.Transparent;
		((Control)button4).BackgroundImage = (Image)(object)Resources.BCG1;
		((Control)button4).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button4).FlatAppearance.BorderSize = 0;
		((ButtonBase)button4).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button4).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button4).FlatStyle = (FlatStyle)0;
		((Control)button4).Location = new Point(771, 343);
		((Control)button4).Margin = new Padding(0);
		((Control)button4).Name = "button4";
		((Control)button4).Size = new Size(30, 31);
		((Control)button4).TabIndex = 41;
		((ButtonBase)button4).UseVisualStyleBackColor = false;
		((Control)button4).Click += ButtonColor_Click;
		((Control)button5).BackColor = Color.Transparent;
		((Control)button5).BackgroundImage = (Image)(object)Resources.BCC1;
		((Control)button5).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button5).FlatAppearance.BorderSize = 0;
		((ButtonBase)button5).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button5).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button5).FlatStyle = (FlatStyle)0;
		((Control)button5).Location = new Point(600, 386);
		((Control)button5).Margin = new Padding(0);
		((Control)button5).Name = "button5";
		((Control)button5).Size = new Size(30, 31);
		((Control)button5).TabIndex = 42;
		((ButtonBase)button5).UseVisualStyleBackColor = false;
		((Control)button5).Click += ButtonColor_Click;
		((Control)button6).BackColor = Color.Transparent;
		((Control)button6).BackgroundImage = (Image)(object)Resources.BCB1;
		((Control)button6).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button6).FlatAppearance.BorderSize = 0;
		((ButtonBase)button6).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button6).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button6).FlatStyle = (FlatStyle)0;
		((Control)button6).Location = new Point(657, 386);
		((Control)button6).Margin = new Padding(0);
		((Control)button6).Name = "button6";
		((Control)button6).Size = new Size(30, 31);
		((Control)button6).TabIndex = 43;
		((ButtonBase)button6).UseVisualStyleBackColor = false;
		((Control)button6).Click += ButtonColor_Click;
		((Control)button7).BackColor = Color.Transparent;
		((Control)button7).BackgroundImage = (Image)(object)Resources.BCP1;
		((Control)button7).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button7).FlatAppearance.BorderSize = 0;
		((ButtonBase)button7).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button7).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button7).FlatStyle = (FlatStyle)0;
		((Control)button7).Location = new Point(714, 386);
		((Control)button7).Margin = new Padding(0);
		((Control)button7).Name = "button7";
		((Control)button7).Size = new Size(30, 31);
		((Control)button7).TabIndex = 44;
		((ButtonBase)button7).UseVisualStyleBackColor = false;
		((Control)button7).Click += ButtonColor_Click;
		((Control)button8).BackColor = Color.Transparent;
		((Control)button8).BackgroundImage = (Image)(object)Resources.BCW1;
		((Control)button8).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button8).FlatAppearance.BorderSize = 0;
		((ButtonBase)button8).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button8).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button8).FlatStyle = (FlatStyle)0;
		((Control)button8).Location = new Point(771, 386);
		((Control)button8).Margin = new Padding(0);
		((Control)button8).Name = "button8";
		((Control)button8).Size = new Size(30, 31);
		((Control)button8).TabIndex = 45;
		((ButtonBase)button8).UseVisualStyleBackColor = false;
		((Control)button8).Click += ButtonColor_Click;
		((Control)buttonCA).BackColor = Color.Transparent;
		((Control)buttonCA).BackgroundImage = (Image)(object)Resources.BCHA1;
		((Control)buttonCA).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonCA).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonCA).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonCA).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonCA).FlatStyle = (FlatStyle)0;
		((Control)buttonCA).Location = new Point(415, 475);
		((Control)buttonCA).Margin = new Padding(0);
		((Control)buttonCA).Name = "buttonCA";
		((Control)buttonCA).Size = new Size(66, 26);
		((Control)buttonCA).TabIndex = 46;
		((ButtonBase)buttonCA).UseVisualStyleBackColor = false;
		((Control)buttonCA).Click += buttonCA_Click;
		((Control)buttonC1).BackColor = Color.Transparent;
		((Control)buttonC1).BackgroundImage = (Image)(object)Resources.BCH12;
		((Control)buttonC1).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonC1).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonC1).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonC1).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonC1).FlatStyle = (FlatStyle)0;
		((Control)buttonC1).Location = new Point(497, 475);
		((Control)buttonC1).Margin = new Padding(0);
		((Control)buttonC1).Name = "buttonC1";
		((Control)buttonC1).Size = new Size(66, 26);
		((Control)buttonC1).TabIndex = 47;
		((ButtonBase)buttonC1).UseVisualStyleBackColor = false;
		((Control)buttonC1).Click += buttonC1_Click;
		((Control)buttonC2).BackColor = Color.Transparent;
		((Control)buttonC2).BackgroundImage = (Image)(object)Resources.BCH22;
		((Control)buttonC2).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonC2).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonC2).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonC2).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonC2).FlatStyle = (FlatStyle)0;
		((Control)buttonC2).Location = new Point(579, 475);
		((Control)buttonC2).Margin = new Padding(0);
		((Control)buttonC2).Name = "buttonC2";
		((Control)buttonC2).Size = new Size(66, 26);
		((Control)buttonC2).TabIndex = 48;
		((ButtonBase)buttonC2).UseVisualStyleBackColor = false;
		((Control)buttonC2).Click += buttonC2_Click;
		((Control)buttonC3).BackColor = Color.Transparent;
		((Control)buttonC3).BackgroundImage = (Image)(object)Resources.BCH32;
		((Control)buttonC3).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonC3).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonC3).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonC3).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonC3).FlatStyle = (FlatStyle)0;
		((Control)buttonC3).Location = new Point(661, 475);
		((Control)buttonC3).Margin = new Padding(0);
		((Control)buttonC3).Name = "buttonC3";
		((Control)buttonC3).Size = new Size(66, 26);
		((Control)buttonC3).TabIndex = 49;
		((ButtonBase)buttonC3).UseVisualStyleBackColor = false;
		((Control)buttonC3).Click += buttonC3_Click;
		((Control)buttonC4).BackColor = Color.Transparent;
		((Control)buttonC4).BackgroundImage = (Image)(object)Resources.BCH42;
		((Control)buttonC4).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonC4).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonC4).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonC4).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonC4).FlatStyle = (FlatStyle)0;
		((Control)buttonC4).Location = new Point(743, 475);
		((Control)buttonC4).Margin = new Padding(0);
		((Control)buttonC4).Name = "buttonC4";
		((Control)buttonC4).Size = new Size(66, 26);
		((Control)buttonC4).TabIndex = 50;
		((ButtonBase)buttonC4).UseVisualStyleBackColor = false;
		((Control)buttonC4).Click += buttonC4_Click;
		((Control)buttonCM).BackColor = Color.Transparent;
		((Control)buttonCM).BackgroundImage = (Image)(object)Resources.BCHM2;
		((Control)buttonCM).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonCM).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonCM).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonCM).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonCM).FlatStyle = (FlatStyle)0;
		((Control)buttonCM).Location = new Point(825, 475);
		((Control)buttonCM).Margin = new Padding(0);
		((Control)buttonCM).Name = "buttonCM";
		((Control)buttonCM).Size = new Size(66, 26);
		((Control)buttonCM).TabIndex = 51;
		((ButtonBase)buttonCM).UseVisualStyleBackColor = false;
		((Control)buttonCM).Click += buttonCM_Click;
		((Control)label1).BackColor = Color.Transparent;
		label1.FlatStyle = (FlatStyle)0;
		((Control)label1).Font = new Font("微软雅黑", 12f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label1).ForeColor = Color.White;
		((Control)label1).Location = new Point(422, 321);
		((Control)label1).Margin = new Padding(0);
		((Control)label1).Name = "label1";
		((Control)label1).Size = new Size(47, 24);
		((Control)label1).TabIndex = 54;
		((Control)label1).Text = "100";
		label1.TextAlign = (ContentAlignment)64;
		((Control)label2).BackColor = Color.Transparent;
		label2.FlatStyle = (FlatStyle)0;
		((Control)label2).Font = new Font("微软雅黑", 12f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label2).ForeColor = Color.White;
		((Control)label2).Location = new Point(422, 389);
		((Control)label2).Margin = new Padding(0);
		((Control)label2).Name = "label2";
		((Control)label2).Size = new Size(47, 24);
		((Control)label2).TabIndex = 55;
		((Control)label2).Text = "快";
		label2.TextAlign = (ContentAlignment)64;
		((Control)label3).BackColor = Color.Transparent;
		label3.FlatStyle = (FlatStyle)0;
		((Control)label3).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label3).ForeColor = Color.White;
		((Control)label3).Location = new Point(506, 62);
		((Control)label3).Margin = new Padding(0);
		((Control)label3).Name = "label3";
		((Control)label3).Size = new Size(47, 24);
		((Control)label3).TabIndex = 56;
		((Control)label3).Text = "255";
		label3.TextAlign = (ContentAlignment)16;
		((Control)label4).BackColor = Color.Transparent;
		label4.FlatStyle = (FlatStyle)0;
		((Control)label4).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label4).ForeColor = Color.White;
		((Control)label4).Location = new Point(506, 92);
		((Control)label4).Margin = new Padding(0);
		((Control)label4).Name = "label4";
		((Control)label4).Size = new Size(47, 24);
		((Control)label4).TabIndex = 57;
		((Control)label4).Text = "0";
		label4.TextAlign = (ContentAlignment)16;
		((Control)label5).BackColor = Color.Transparent;
		label5.FlatStyle = (FlatStyle)0;
		((Control)label5).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label5).ForeColor = Color.White;
		((Control)label5).Location = new Point(506, 121);
		((Control)label5).Margin = new Padding(0);
		((Control)label5).Name = "label5";
		((Control)label5).Size = new Size(47, 24);
		((Control)label5).TabIndex = 58;
		((Control)label5).Text = "0";
		label5.TextAlign = (ContentAlignment)16;
		((Control)label6).BackColor = Color.Transparent;
		label6.FlatStyle = (FlatStyle)0;
		((Control)label6).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label6).ForeColor = Color.White;
		((Control)label6).Location = new Point(506, 150);
		((Control)label6).Margin = new Padding(0);
		((Control)label6).Name = "label6";
		((Control)label6).Size = new Size(69, 24);
		((Control)label6).TabIndex = 59;
		((Control)label6).Text = "FF0000";
		label6.TextAlign = (ContentAlignment)16;
		((Control)labelALL).BackColor = Color.Red;
		labelALL.FlatStyle = (FlatStyle)0;
		((Control)labelALL).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)labelALL).ForeColor = Color.Red;
		((Control)labelALL).Location = new Point(506, 186);
		((Control)labelALL).Margin = new Padding(0);
		((Control)labelALL).Name = "labelALL";
		((Control)labelALL).Size = new Size(20, 20);
		((Control)labelALL).TabIndex = 61;
		labelALL.TextAlign = (ContentAlignment)16;
		((Control)labelSub).BackColor = Color.Red;
		labelSub.FlatStyle = (FlatStyle)0;
		((Control)labelSub).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)labelSub).ForeColor = Color.Red;
		((Control)labelSub).Location = new Point(527, 206);
		((Control)labelSub).Margin = new Padding(0);
		((Control)labelSub).Name = "labelSub";
		((Control)labelSub).Size = new Size(16, 16);
		((Control)labelSub).TabIndex = 62;
		labelSub.TextAlign = (ContentAlignment)16;
		((Control)buttonWBSR).BackColor = Color.Transparent;
		((Control)buttonWBSR).BackgroundImage = (Image)(object)Resources.Bwbsr2;
		((Control)buttonWBSR).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonWBSR).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonWBSR).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonWBSR).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonWBSR).FlatStyle = (FlatStyle)0;
		((Control)buttonWBSR).Location = new Point(403, 271);
		((Control)buttonWBSR).Margin = new Padding(0);
		((Control)buttonWBSR).Name = "buttonWBSR";
		((Control)buttonWBSR).Size = new Size(76, 20);
		((Control)buttonWBSR).TabIndex = 63;
		((ButtonBase)buttonWBSR).UseVisualStyleBackColor = false;
		((Control)buttonWBSR).Click += buttonWBSR_Click;
		((Control)ucColor11).BackColor = Color.Transparent;
		((Control)ucColor11).BackgroundImage = (Image)componentResourceManager.GetObject("ucColor11.BackgroundImage");
		((Control)ucColor11).BackgroundImageLayout = (ImageLayout)2;
		((Control)ucColor11).Location = new Point(590, 96);
		((Control)ucColor11).Margin = new Padding(0);
		((Control)ucColor11).Name = "ucColor11";
		((Control)ucColor11).Size = new Size(220, 220);
		((Control)ucColor11).TabIndex = 60;
		((Control)ucScroll12).BackColor = Color.Transparent;
		((Control)ucScroll12).BackgroundImageLayout = (ImageLayout)4;
		((Control)ucScroll12).Location = new Point(73, 412);
		((Control)ucScroll12).Margin = new Padding(0);
		((Control)ucScroll12).Name = "ucScroll12";
		((Control)ucScroll12).Size = new Size(404, 16);
		((Control)ucScroll12).TabIndex = 53;
		((Control)ucScroll11).BackColor = Color.Transparent;
		((Control)ucScroll11).BackgroundImageLayout = (ImageLayout)4;
		((Control)ucScroll11).Location = new Point(73, 343);
		((Control)ucScroll11).Margin = new Padding(0);
		((Control)ucScroll11).Name = "ucScroll11";
		((Control)ucScroll11).Size = new Size(404, 16);
		((Control)ucScroll11).TabIndex = 52;
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)3;
		((Control)this).BackColor = Color.Transparent;
		((Control)this).BackgroundImage = (Image)(object)Resources.Background3_2;
		((Control)this).Controls.Add((Control)(object)buttonWBSR);
		((Control)this).Controls.Add((Control)(object)labelSub);
		((Control)this).Controls.Add((Control)(object)labelALL);
		((Control)this).Controls.Add((Control)(object)ucColor11);
		((Control)this).Controls.Add((Control)(object)label6);
		((Control)this).Controls.Add((Control)(object)label5);
		((Control)this).Controls.Add((Control)(object)label4);
		((Control)this).Controls.Add((Control)(object)label3);
		((Control)this).Controls.Add((Control)(object)ucScroll12);
		((Control)this).Controls.Add((Control)(object)ucScroll11);
		((Control)this).Controls.Add((Control)(object)buttonCM);
		((Control)this).Controls.Add((Control)(object)buttonC4);
		((Control)this).Controls.Add((Control)(object)buttonC3);
		((Control)this).Controls.Add((Control)(object)buttonC2);
		((Control)this).Controls.Add((Control)(object)buttonC1);
		((Control)this).Controls.Add((Control)(object)buttonCA);
		((Control)this).Controls.Add((Control)(object)button8);
		((Control)this).Controls.Add((Control)(object)button7);
		((Control)this).Controls.Add((Control)(object)button6);
		((Control)this).Controls.Add((Control)(object)button5);
		((Control)this).Controls.Add((Control)(object)button4);
		((Control)this).Controls.Add((Control)(object)button3);
		((Control)this).Controls.Add((Control)(object)button2);
		((Control)this).Controls.Add((Control)(object)button1);
		((Control)this).Controls.Add((Control)(object)buttonYYMS);
		((Control)this).Controls.Add((Control)(object)buttonCHLS);
		((Control)this).Controls.Add((Control)(object)buttonQCZZ);
		((Control)this).Controls.Add((Control)(object)buttonZYJC);
		((Control)this).Controls.Add((Control)(object)buttonSXPM);
		((Control)this).Controls.Add((Control)(object)buttonQSPM);
		((Control)this).Controls.Add((Control)(object)buttonCHMS);
		((Control)this).Controls.Add((Control)(object)buttonXGSS);
		((Control)this).Controls.Add((Control)(object)buttonDSBS);
		((Control)this).Controls.Add((Control)(object)buttonDSXH);
		((Control)this).Controls.Add((Control)(object)buttonHXMS);
		((Control)this).Controls.Add((Control)(object)buttonJTDS);
		((Control)this).Controls.Add((Control)(object)label1);
		((Control)this).Controls.Add((Control)(object)label2);
		((Control)this).DoubleBuffered = true;
		((Control)this).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)this).Location = new Point(0, 250);
		((Control)this).Margin = new Padding(0);
		((Control)this).Name = "UserDeviceLED";
		((Control)this).Size = new Size(916, 550);
		((Control)this).ResumeLayout(false);
	}
}
