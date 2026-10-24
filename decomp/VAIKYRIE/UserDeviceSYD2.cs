using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VAIKYRIE.Properties;

namespace VAIKYRIE;

public class UserDeviceSYD2 : UserControl
{
	public delegate void delegateUserDeviceSYD2(int ch, int cmd, object obj);

	private bool onOff = true;

	public int myMode = 1;

	public int myChannel = 0;

	public int rgbR1 = 255;

	public int rgbG1 = 0;

	public int rgbB1 = 0;

	public int myBrightness = 100;

	public int mySpeed = 2;

	public delegateUserDeviceSYD2 ucDeviceLEDS2;

	private bool isSendData = false;

	private IContainer components = null;

	public Label labelSub;

	public Label labelALL;

	private UCColor1 ucColor11;

	public Label label6;

	public Label label5;

	public Label label4;

	public Label label3;

	private Button button8;

	private Button button7;

	private Button button6;

	private Button button5;

	private Button button4;

	private Button button3;

	private Button button2;

	private Button button1;

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

	private Label label;

	private Button buttonONOFF;

	public Label label1;

	public Label label2;

	private UCScroll2 ucScroll21;

	private UCScroll2 ucScroll22;

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

	public UserDeviceSYD2()
	{
		InitializeComponent();
		ucScroll21.upDateUCScroll2 = UpDateUCScroll1;
		ucScroll22.upDateUCScroll2 = UpDateUCScroll2;
		ucColor11.upDateUCColor1 = UpDateUCColor;
		ClearButtonBouns();
	}

	private void UpDateUCScroll1(int val)
	{
		buttonONOFF_ToON();
		myBrightness = val;
		((Control)label1).Text = val.ToString();
		UpDateUCColor1(rgbR1, rgbG1, rgbB1);
		SendLEDData();
	}

	private void UpDateUCScroll2(int val)
	{
		buttonONOFF_ToON();
		if (val < 33)
		{
			mySpeed = 8;
			((Control)label2).Text = "慢";
		}
		else if (val < 66)
		{
			mySpeed = 4;
			((Control)label2).Text = "中";
		}
		else
		{
			mySpeed = 2;
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
		buttonONOFF_ToON();
		UpDateUCColor1(r, g, b);
		SendLEDData();
	}

	private void buttonONOFF_ToON()
	{
		if (!onOff)
		{
			onOff = true;
			((Control)buttonONOFF).BackgroundImage = (Image)(object)Resources.SYDON;
		}
	}

	private void buttonONOFF_Click(object sender, EventArgs e)
	{
		if (onOff)
		{
			onOff = false;
			((Control)buttonONOFF).BackgroundImage = (Image)(object)Resources.SYDOFF;
		}
		else
		{
			onOff = true;
			((Control)buttonONOFF).BackgroundImage = (Image)(object)Resources.SYDON;
		}
		SendLEDData();
	}

	private void ButtonMode_Click(int mode)
	{
		((Control)buttonJTDS).BackgroundImage = (Image)(object)Resources.SYDjtds2;
		((Control)buttonHXMS).BackgroundImage = (Image)(object)Resources.SYDhxms2;
		((Control)buttonDSXH).BackgroundImage = (Image)(object)Resources.SYDdsxh2;
		((Control)buttonDSBS).BackgroundImage = (Image)(object)Resources.SYDdsbs2;
		((Control)buttonXGSS).BackgroundImage = (Image)(object)Resources.SYDxgss2;
		((Control)buttonCHMS).BackgroundImage = (Image)(object)Resources.SYDshms2;
		((Control)buttonQSPM).BackgroundImage = (Image)(object)Resources.SYDqspm2;
		((Control)buttonSXPM).BackgroundImage = (Image)(object)Resources.SYDsxpm2;
		((Control)buttonZYJC).BackgroundImage = (Image)(object)Resources.SYDzyjc2;
		((Control)buttonQCZZ).BackgroundImage = (Image)(object)Resources.SYDqczz2;
		((Control)buttonCHLS).BackgroundImage = (Image)(object)Resources.SYDshls2;
		((Control)buttonYYMS).BackgroundImage = (Image)(object)Resources.SYDyyms12;
		switch (mode)
		{
		case 1:
			((Control)buttonJTDS).BackgroundImage = (Image)(object)Resources.SYDjtds1;
			break;
		case 2:
			((Control)buttonHXMS).BackgroundImage = (Image)(object)Resources.SYDhxms1;
			break;
		case 3:
			((Control)buttonDSXH).BackgroundImage = (Image)(object)Resources.SYDdsxh1;
			break;
		case 4:
			((Control)buttonDSBS).BackgroundImage = (Image)(object)Resources.SYDdsbs1;
			break;
		case 5:
			((Control)buttonXGSS).BackgroundImage = (Image)(object)Resources.SYDxgss1;
			break;
		case 6:
			((Control)buttonCHMS).BackgroundImage = (Image)(object)Resources.SYDshms1;
			break;
		case 7:
			((Control)buttonQSPM).BackgroundImage = (Image)(object)Resources.SYDqspm1;
			break;
		case 8:
			((Control)buttonSXPM).BackgroundImage = (Image)(object)Resources.SYDsxpm1;
			break;
		case 9:
			((Control)buttonZYJC).BackgroundImage = (Image)(object)Resources.SYDzyjc1;
			break;
		case 10:
			((Control)buttonQCZZ).BackgroundImage = (Image)(object)Resources.SYDqczz1;
			break;
		case 11:
			((Control)buttonCHLS).BackgroundImage = (Image)(object)Resources.SYDshls1;
			break;
		case 12:
			((Control)buttonYYMS).BackgroundImage = (Image)(object)Resources.SYDyyms11;
			break;
		}
	}

	private void buttonJTDS_Click(object sender, EventArgs e)
	{
		buttonONOFF_ToON();
		myMode = 1;
		ButtonMode_Click(myMode);
		SendLEDData();
	}

	private void buttonHXMS_Click(object sender, EventArgs e)
	{
		buttonONOFF_ToON();
		myMode = 2;
		ButtonMode_Click(myMode);
		SendLEDData();
	}

	private void buttonDSXH_Click(object sender, EventArgs e)
	{
		buttonONOFF_ToON();
		myMode = 3;
		ButtonMode_Click(myMode);
		SendLEDData();
	}

	private void buttonDSBS_Click(object sender, EventArgs e)
	{
		buttonONOFF_ToON();
		myMode = 4;
		ButtonMode_Click(myMode);
		SendLEDData();
	}

	private void buttonXGSS_Click(object sender, EventArgs e)
	{
		buttonONOFF_ToON();
		myMode = 5;
		ButtonMode_Click(myMode);
		SendLEDData();
	}

	private void buttonCHMS_Click(object sender, EventArgs e)
	{
		buttonONOFF_ToON();
		myMode = 6;
		ButtonMode_Click(myMode);
		SendLEDData();
	}

	private void buttonQSPM_Click(object sender, EventArgs e)
	{
		buttonONOFF_ToON();
		myMode = 7;
		ButtonMode_Click(myMode);
		SendLEDData();
	}

	private void buttonSXPM_Click(object sender, EventArgs e)
	{
		buttonONOFF_ToON();
		myMode = 8;
		ButtonMode_Click(myMode);
		SendLEDData();
	}

	private void buttonZYJC_Click(object sender, EventArgs e)
	{
		buttonONOFF_ToON();
		myMode = 9;
		ButtonMode_Click(myMode);
		SendLEDData();
	}

	private void buttonQCZZ_Click(object sender, EventArgs e)
	{
		buttonONOFF_ToON();
		myMode = 10;
		ButtonMode_Click(myMode);
		SendLEDData();
	}

	private void buttonCHLS_Click(object sender, EventArgs e)
	{
		buttonONOFF_ToON();
		myMode = 11;
		ButtonMode_Click(myMode);
		SendLEDData();
	}

	private void buttonYYMS_Click(object sender, EventArgs e)
	{
		buttonONOFF_ToON();
		myMode = 12;
		ButtonMode_Click(myMode);
		SendLEDData();
	}

	private void ButtonColor_Click(object sender, EventArgs e)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Expected O, but got Unknown
		buttonONOFF_ToON();
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
		ucDeviceLEDS2?.Invoke(myChannel, onOff ? myMode : 0, obj);
		isSendData = true;
	}

	public void ReceivedLEDData(byte[] data)
	{
		if (data.Length != 0 && isSendData)
		{
			isSendData = false;
		}
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
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Expected O, but got Unknown
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Expected O, but got Unknown
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Expected O, but got Unknown
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Expected O, but got Unknown
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Expected O, but got Unknown
		//IL_0512: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05af: Expected O, but got Unknown
		//IL_05e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06da: Unknown result type (might be due to invalid IL or missing references)
		//IL_07da: Unknown result type (might be due to invalid IL or missing references)
		//IL_08da: Unknown result type (might be due to invalid IL or missing references)
		//IL_09da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ada: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bda: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cda: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dda: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eda: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fda: Unknown result type (might be due to invalid IL or missing references)
		//IL_10da: Unknown result type (might be due to invalid IL or missing references)
		//IL_11da: Unknown result type (might be due to invalid IL or missing references)
		//IL_12da: Unknown result type (might be due to invalid IL or missing references)
		//IL_13da: Unknown result type (might be due to invalid IL or missing references)
		//IL_14da: Unknown result type (might be due to invalid IL or missing references)
		//IL_15da: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_17d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_18d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_19ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bd3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bdd: Expected O, but got Unknown
		//IL_1c11: Unknown result type (might be due to invalid IL or missing references)
		//IL_1ca4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1cae: Expected O, but got Unknown
		//IL_1ce2: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d80: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e02: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e66: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e70: Expected O, but got Unknown
		//IL_1e9d: Unknown result type (might be due to invalid IL or missing references)
		//IL_2167: Unknown result type (might be due to invalid IL or missing references)
		//IL_2171: Expected O, but got Unknown
		//IL_2186: Unknown result type (might be due to invalid IL or missing references)
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(UserDeviceSYD2));
		labelSub = new Label();
		labelALL = new Label();
		label6 = new Label();
		label5 = new Label();
		label4 = new Label();
		label3 = new Label();
		button8 = new Button();
		button7 = new Button();
		button6 = new Button();
		button5 = new Button();
		button4 = new Button();
		button3 = new Button();
		button2 = new Button();
		button1 = new Button();
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
		label = new Label();
		buttonONOFF = new Button();
		label1 = new Label();
		label2 = new Label();
		ucScroll22 = new UCScroll2();
		ucScroll21 = new UCScroll2();
		ucColor11 = new UCColor1();
		((Control)this).SuspendLayout();
		((Control)labelSub).BackColor = Color.Red;
		labelSub.FlatStyle = (FlatStyle)0;
		((Control)labelSub).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)labelSub).ForeColor = Color.Red;
		((Control)labelSub).Location = new Point(527, 206);
		((Control)labelSub).Margin = new Padding(0);
		((Control)labelSub).Name = "labelSub";
		((Control)labelSub).Size = new Size(16, 16);
		((Control)labelSub).TabIndex = 77;
		labelSub.TextAlign = (ContentAlignment)16;
		((Control)labelALL).BackColor = Color.Red;
		labelALL.FlatStyle = (FlatStyle)0;
		((Control)labelALL).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)labelALL).ForeColor = Color.Red;
		((Control)labelALL).Location = new Point(506, 186);
		((Control)labelALL).Margin = new Padding(0);
		((Control)labelALL).Name = "labelALL";
		((Control)labelALL).Size = new Size(20, 20);
		((Control)labelALL).TabIndex = 76;
		labelALL.TextAlign = (ContentAlignment)16;
		((Control)label6).BackColor = Color.Transparent;
		label6.FlatStyle = (FlatStyle)0;
		((Control)label6).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label6).ForeColor = Color.White;
		((Control)label6).Location = new Point(506, 150);
		((Control)label6).Margin = new Padding(0);
		((Control)label6).Name = "label6";
		((Control)label6).Size = new Size(69, 24);
		((Control)label6).TabIndex = 74;
		((Control)label6).Text = "FF0000";
		label6.TextAlign = (ContentAlignment)16;
		((Control)label5).BackColor = Color.Transparent;
		label5.FlatStyle = (FlatStyle)0;
		((Control)label5).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label5).ForeColor = Color.White;
		((Control)label5).Location = new Point(506, 121);
		((Control)label5).Margin = new Padding(0);
		((Control)label5).Name = "label5";
		((Control)label5).Size = new Size(47, 24);
		((Control)label5).TabIndex = 73;
		((Control)label5).Text = "0";
		label5.TextAlign = (ContentAlignment)16;
		((Control)label4).BackColor = Color.Transparent;
		label4.FlatStyle = (FlatStyle)0;
		((Control)label4).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label4).ForeColor = Color.White;
		((Control)label4).Location = new Point(506, 92);
		((Control)label4).Margin = new Padding(0);
		((Control)label4).Name = "label4";
		((Control)label4).Size = new Size(47, 24);
		((Control)label4).TabIndex = 72;
		((Control)label4).Text = "0";
		label4.TextAlign = (ContentAlignment)16;
		((Control)label3).BackColor = Color.Transparent;
		label3.FlatStyle = (FlatStyle)0;
		((Control)label3).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label3).ForeColor = Color.White;
		((Control)label3).Location = new Point(506, 62);
		((Control)label3).Margin = new Padding(0);
		((Control)label3).Name = "label3";
		((Control)label3).Size = new Size(47, 24);
		((Control)label3).TabIndex = 71;
		((Control)label3).Text = "255";
		label3.TextAlign = (ContentAlignment)16;
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
		((Control)button8).TabIndex = 70;
		((ButtonBase)button8).UseVisualStyleBackColor = false;
		((Control)button8).Click += ButtonColor_Click;
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
		((Control)button7).TabIndex = 69;
		((ButtonBase)button7).UseVisualStyleBackColor = false;
		((Control)button7).Click += ButtonColor_Click;
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
		((Control)button6).TabIndex = 68;
		((ButtonBase)button6).UseVisualStyleBackColor = false;
		((Control)button6).Click += ButtonColor_Click;
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
		((Control)button5).TabIndex = 67;
		((ButtonBase)button5).UseVisualStyleBackColor = false;
		((Control)button5).Click += ButtonColor_Click;
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
		((Control)button4).TabIndex = 66;
		((ButtonBase)button4).UseVisualStyleBackColor = false;
		((Control)button4).Click += ButtonColor_Click;
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
		((Control)button3).TabIndex = 65;
		((ButtonBase)button3).UseVisualStyleBackColor = false;
		((Control)button3).Click += ButtonColor_Click;
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
		((Control)button2).TabIndex = 64;
		((ButtonBase)button2).UseVisualStyleBackColor = false;
		((Control)button2).Click += ButtonColor_Click;
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
		((Control)button1).TabIndex = 63;
		((ButtonBase)button1).UseVisualStyleBackColor = false;
		((Control)button1).Click += ButtonColor_Click;
		((Control)buttonYYMS).BackColor = Color.Transparent;
		((Control)buttonYYMS).BackgroundImage = (Image)(object)Resources.SYDyyms12;
		((Control)buttonYYMS).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonYYMS).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonYYMS).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonYYMS).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonYYMS).FlatStyle = (FlatStyle)0;
		((Control)buttonYYMS).Location = new Point(329, 257);
		((Control)buttonYYMS).Margin = new Padding(0);
		((Control)buttonYYMS).Name = "buttonYYMS";
		((Control)buttonYYMS).Size = new Size(113, 27);
		((Control)buttonYYMS).TabIndex = 89;
		((ButtonBase)buttonYYMS).UseVisualStyleBackColor = false;
		((Control)buttonYYMS).Click += buttonYYMS_Click;
		((Control)buttonCHLS).BackColor = Color.Transparent;
		((Control)buttonCHLS).BackgroundImage = (Image)(object)Resources.SYDshls2;
		((Control)buttonCHLS).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonCHLS).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonCHLS).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonCHLS).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonCHLS).FlatStyle = (FlatStyle)0;
		((Control)buttonCHLS).Location = new Point(197, 257);
		((Control)buttonCHLS).Margin = new Padding(0);
		((Control)buttonCHLS).Name = "buttonCHLS";
		((Control)buttonCHLS).Size = new Size(113, 27);
		((Control)buttonCHLS).TabIndex = 88;
		((ButtonBase)buttonCHLS).UseVisualStyleBackColor = false;
		((Control)buttonCHLS).Click += buttonCHLS_Click;
		((Control)buttonQCZZ).BackColor = Color.Transparent;
		((Control)buttonQCZZ).BackgroundImage = (Image)(object)Resources.SYDqczz2;
		((Control)buttonQCZZ).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonQCZZ).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonQCZZ).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonQCZZ).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonQCZZ).FlatStyle = (FlatStyle)0;
		((Control)buttonQCZZ).Location = new Point(329, 216);
		((Control)buttonQCZZ).Margin = new Padding(0);
		((Control)buttonQCZZ).Name = "buttonQCZZ";
		((Control)buttonQCZZ).Size = new Size(113, 27);
		((Control)buttonQCZZ).TabIndex = 87;
		((ButtonBase)buttonQCZZ).UseVisualStyleBackColor = false;
		((Control)buttonQCZZ).Click += buttonQCZZ_Click;
		((Control)buttonZYJC).BackColor = Color.Transparent;
		((Control)buttonZYJC).BackgroundImage = (Image)(object)Resources.SYDzyjc2;
		((Control)buttonZYJC).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonZYJC).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonZYJC).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonZYJC).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonZYJC).FlatStyle = (FlatStyle)0;
		((Control)buttonZYJC).Location = new Point(197, 216);
		((Control)buttonZYJC).Margin = new Padding(0);
		((Control)buttonZYJC).Name = "buttonZYJC";
		((Control)buttonZYJC).Size = new Size(113, 27);
		((Control)buttonZYJC).TabIndex = 86;
		((ButtonBase)buttonZYJC).UseVisualStyleBackColor = false;
		((Control)buttonZYJC).Click += buttonZYJC_Click;
		((Control)buttonSXPM).BackColor = Color.Transparent;
		((Control)buttonSXPM).BackgroundImage = (Image)(object)Resources.SYDsxpm2;
		((Control)buttonSXPM).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonSXPM).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonSXPM).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonSXPM).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonSXPM).FlatStyle = (FlatStyle)0;
		((Control)buttonSXPM).Location = new Point(329, 175);
		((Control)buttonSXPM).Margin = new Padding(0);
		((Control)buttonSXPM).Name = "buttonSXPM";
		((Control)buttonSXPM).Size = new Size(113, 27);
		((Control)buttonSXPM).TabIndex = 85;
		((ButtonBase)buttonSXPM).UseVisualStyleBackColor = false;
		((Control)buttonSXPM).Click += buttonSXPM_Click;
		((Control)buttonQSPM).BackColor = Color.Transparent;
		((Control)buttonQSPM).BackgroundImage = (Image)(object)Resources.SYDqspm2;
		((Control)buttonQSPM).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonQSPM).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonQSPM).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonQSPM).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonQSPM).FlatStyle = (FlatStyle)0;
		((Control)buttonQSPM).Location = new Point(197, 175);
		((Control)buttonQSPM).Margin = new Padding(0);
		((Control)buttonQSPM).Name = "buttonQSPM";
		((Control)buttonQSPM).Size = new Size(113, 27);
		((Control)buttonQSPM).TabIndex = 84;
		((ButtonBase)buttonQSPM).UseVisualStyleBackColor = false;
		((Control)buttonQSPM).Click += buttonQSPM_Click;
		((Control)buttonCHMS).BackColor = Color.Transparent;
		((Control)buttonCHMS).BackgroundImage = (Image)(object)Resources.SYDshms2;
		((Control)buttonCHMS).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonCHMS).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonCHMS).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonCHMS).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonCHMS).FlatStyle = (FlatStyle)0;
		((Control)buttonCHMS).Location = new Point(329, 134);
		((Control)buttonCHMS).Margin = new Padding(0);
		((Control)buttonCHMS).Name = "buttonCHMS";
		((Control)buttonCHMS).Size = new Size(113, 27);
		((Control)buttonCHMS).TabIndex = 83;
		((ButtonBase)buttonCHMS).UseVisualStyleBackColor = false;
		((Control)buttonCHMS).Click += buttonCHMS_Click;
		((Control)buttonXGSS).BackColor = Color.Transparent;
		((Control)buttonXGSS).BackgroundImage = (Image)(object)Resources.SYDxgss2;
		((Control)buttonXGSS).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonXGSS).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonXGSS).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonXGSS).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonXGSS).FlatStyle = (FlatStyle)0;
		((Control)buttonXGSS).Location = new Point(197, 134);
		((Control)buttonXGSS).Margin = new Padding(0);
		((Control)buttonXGSS).Name = "buttonXGSS";
		((Control)buttonXGSS).Size = new Size(113, 27);
		((Control)buttonXGSS).TabIndex = 82;
		((ButtonBase)buttonXGSS).UseVisualStyleBackColor = false;
		((Control)buttonXGSS).Click += buttonXGSS_Click;
		((Control)buttonDSBS).BackColor = Color.Transparent;
		((Control)buttonDSBS).BackgroundImage = (Image)(object)Resources.SYDdsbs2;
		((Control)buttonDSBS).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonDSBS).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonDSBS).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonDSBS).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonDSBS).FlatStyle = (FlatStyle)0;
		((Control)buttonDSBS).Location = new Point(329, 93);
		((Control)buttonDSBS).Margin = new Padding(0);
		((Control)buttonDSBS).Name = "buttonDSBS";
		((Control)buttonDSBS).Size = new Size(113, 27);
		((Control)buttonDSBS).TabIndex = 81;
		((ButtonBase)buttonDSBS).UseVisualStyleBackColor = false;
		((Control)buttonDSBS).Click += buttonDSBS_Click;
		((Control)buttonDSXH).BackColor = Color.Transparent;
		((Control)buttonDSXH).BackgroundImage = (Image)(object)Resources.SYDdsxh2;
		((Control)buttonDSXH).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonDSXH).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonDSXH).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonDSXH).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonDSXH).FlatStyle = (FlatStyle)0;
		((Control)buttonDSXH).Location = new Point(197, 93);
		((Control)buttonDSXH).Margin = new Padding(0);
		((Control)buttonDSXH).Name = "buttonDSXH";
		((Control)buttonDSXH).Size = new Size(113, 27);
		((Control)buttonDSXH).TabIndex = 80;
		((ButtonBase)buttonDSXH).UseVisualStyleBackColor = false;
		((Control)buttonDSXH).Click += buttonDSXH_Click;
		((Control)buttonHXMS).BackColor = Color.Transparent;
		((Control)buttonHXMS).BackgroundImage = (Image)(object)Resources.SYDhxms2;
		((Control)buttonHXMS).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonHXMS).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonHXMS).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonHXMS).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonHXMS).FlatStyle = (FlatStyle)0;
		((Control)buttonHXMS).Location = new Point(329, 52);
		((Control)buttonHXMS).Margin = new Padding(0);
		((Control)buttonHXMS).Name = "buttonHXMS";
		((Control)buttonHXMS).Size = new Size(113, 27);
		((Control)buttonHXMS).TabIndex = 79;
		((ButtonBase)buttonHXMS).UseVisualStyleBackColor = false;
		((Control)buttonHXMS).Click += buttonHXMS_Click;
		((Control)buttonJTDS).BackColor = Color.Transparent;
		((Control)buttonJTDS).BackgroundImage = (Image)(object)Resources.SYDjtds1;
		((Control)buttonJTDS).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonJTDS).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonJTDS).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonJTDS).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonJTDS).FlatStyle = (FlatStyle)0;
		((Control)buttonJTDS).Location = new Point(197, 52);
		((Control)buttonJTDS).Margin = new Padding(0);
		((Control)buttonJTDS).Name = "buttonJTDS";
		((Control)buttonJTDS).Size = new Size(113, 27);
		((Control)buttonJTDS).TabIndex = 78;
		((ButtonBase)buttonJTDS).UseVisualStyleBackColor = false;
		((Control)buttonJTDS).Click += buttonJTDS_Click;
		label.Image = (Image)(object)Resources.SYDUD;
		((Control)label).Location = new Point(42, 29);
		((Control)label).Margin = new Padding(0);
		((Control)label).Name = "label";
		((Control)label).Size = new Size(112, 416);
		((Control)label).TabIndex = 90;
		((Control)buttonONOFF).BackColor = Color.Transparent;
		((Control)buttonONOFF).BackgroundImage = (Image)(object)Resources.SYDON;
		((Control)buttonONOFF).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonONOFF).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonONOFF).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonONOFF).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonONOFF).FlatStyle = (FlatStyle)0;
		((Control)buttonONOFF).Location = new Point(41, 349);
		((Control)buttonONOFF).Margin = new Padding(0);
		((Control)buttonONOFF).Name = "buttonONOFF";
		((Control)buttonONOFF).Size = new Size(22, 95);
		((Control)buttonONOFF).TabIndex = 91;
		((ButtonBase)buttonONOFF).UseVisualStyleBackColor = false;
		((Control)buttonONOFF).Click += buttonONOFF_Click;
		((Control)label1).BackColor = Color.Transparent;
		label1.FlatStyle = (FlatStyle)0;
		((Control)label1).Font = new Font("微软雅黑", 12f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label1).ForeColor = Color.White;
		((Control)label1).Location = new Point(446, 320);
		((Control)label1).Margin = new Padding(0);
		((Control)label1).Name = "label1";
		((Control)label1).Size = new Size(47, 24);
		((Control)label1).TabIndex = 92;
		((Control)label1).Text = "100";
		label1.TextAlign = (ContentAlignment)64;
		((Control)label2).BackColor = Color.Transparent;
		label2.FlatStyle = (FlatStyle)0;
		((Control)label2).Font = new Font("微软雅黑", 12f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label2).ForeColor = Color.White;
		((Control)label2).Location = new Point(446, 388);
		((Control)label2).Margin = new Padding(0);
		((Control)label2).Name = "label2";
		((Control)label2).Size = new Size(47, 24);
		((Control)label2).TabIndex = 93;
		((Control)label2).Text = "快";
		label2.TextAlign = (ContentAlignment)64;
		((Control)ucScroll22).BackColor = Color.Transparent;
		((Control)ucScroll22).BackgroundImageLayout = (ImageLayout)4;
		((Control)ucScroll22).Location = new Point(200, 413);
		((Control)ucScroll22).Margin = new Padding(0);
		((Control)ucScroll22).Name = "ucScroll22";
		((Control)ucScroll22).Size = new Size(301, 11);
		((Control)ucScroll22).TabIndex = 95;
		((Control)ucScroll21).BackColor = Color.Transparent;
		((Control)ucScroll21).BackgroundImageLayout = (ImageLayout)4;
		((Control)ucScroll21).Location = new Point(200, 344);
		((Control)ucScroll21).Margin = new Padding(0);
		((Control)ucScroll21).Name = "ucScroll21";
		((Control)ucScroll21).Size = new Size(301, 11);
		((Control)ucScroll21).TabIndex = 94;
		((Control)ucColor11).BackColor = Color.Transparent;
		((Control)ucColor11).BackgroundImage = (Image)componentResourceManager.GetObject("ucColor11.BackgroundImage");
		((Control)ucColor11).BackgroundImageLayout = (ImageLayout)2;
		((Control)ucColor11).Location = new Point(590, 96);
		((Control)ucColor11).Margin = new Padding(0);
		((Control)ucColor11).Name = "ucColor11";
		((Control)ucColor11).Size = new Size(220, 220);
		((Control)ucColor11).TabIndex = 75;
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)3;
		((Control)this).BackColor = Color.Transparent;
		((Control)this).BackgroundImage = (Image)(object)Resources.SydZDYBK;
		((Control)this).Controls.Add((Control)(object)ucScroll22);
		((Control)this).Controls.Add((Control)(object)ucScroll21);
		((Control)this).Controls.Add((Control)(object)buttonONOFF);
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
		((Control)this).Controls.Add((Control)(object)labelSub);
		((Control)this).Controls.Add((Control)(object)labelALL);
		((Control)this).Controls.Add((Control)(object)ucColor11);
		((Control)this).Controls.Add((Control)(object)label6);
		((Control)this).Controls.Add((Control)(object)label5);
		((Control)this).Controls.Add((Control)(object)label4);
		((Control)this).Controls.Add((Control)(object)label3);
		((Control)this).Controls.Add((Control)(object)button8);
		((Control)this).Controls.Add((Control)(object)button7);
		((Control)this).Controls.Add((Control)(object)button6);
		((Control)this).Controls.Add((Control)(object)button5);
		((Control)this).Controls.Add((Control)(object)button4);
		((Control)this).Controls.Add((Control)(object)button3);
		((Control)this).Controls.Add((Control)(object)button2);
		((Control)this).Controls.Add((Control)(object)button1);
		((Control)this).Controls.Add((Control)(object)label);
		((Control)this).Controls.Add((Control)(object)label1);
		((Control)this).Controls.Add((Control)(object)label2);
		((Control)this).DoubleBuffered = true;
		((Control)this).Font = new Font("微软雅黑", 9f);
		((Control)this).Location = new Point(0, 250);
		((Control)this).Margin = new Padding(0);
		((Control)this).Name = "UserDeviceSYD2";
		((Control)this).Size = new Size(916, 550);
		((Control)this).ResumeLayout(false);
	}
}
