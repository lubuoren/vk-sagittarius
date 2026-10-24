using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VAIKYRIE.Properties;

namespace VAIKYRIE;

public class UserDeviceSYD1 : UserControl
{
	public delegate void delegateUserDeviceSYD1(int ch, int cmd, object obj);

	private const bool myUp = true;

	private const bool myDown = false;

	public bool onOff = true;

	public int myChannel = 0;

	public bool isXTXX_YPLD = true;

	public bool myChannelUp_Down = true;

	public int danseAndCaihong1 = 1;

	public int danseAndCaihong2 = 1;

	public int myMode1 = 1;

	public int myMode2 = 1;

	public int rgbR1 = 255;

	public int rgbG1 = 0;

	public int rgbB1 = 0;

	public int rgbR2 = 255;

	public int rgbG2 = 0;

	public int rgbB2 = 0;

	public int myBrightness = 100;

	public int mySpeed = 1;

	public int myMode = 1;

	public bool nzyp = true;

	public int danseAndCaihong = 1;

	public int rgbR = 255;

	public int rgbG = 0;

	public int rgbB = 0;

	public delegateUserDeviceSYD1 ucDeviceLEDS1;

	private bool isSendData = false;

	private IContainer components = null;

	private Button buttonONOFF;

	private Label label;

	private UCColor2 ucColor21;

	private UCColor2 ucColor22;

	private Button buttonUpDown;

	private Button button8;

	private Button button7;

	private Button button6;

	private Button button5;

	private Button button4;

	private Button button3;

	private Button button2;

	private Button button1;

	private Button buttonY8;

	private Button buttonY7;

	private Button buttonY6;

	private Button buttonY5;

	private Button buttonY4;

	private Button buttonY3;

	private Button buttonY2;

	private Button buttonY1;

	public Label labelDown;

	public Label labelUp;

	public Label labelYSub;

	public Label labelYALL;

	public Label label1;

	public Label label2;

	private Button buttonNZ;

	private Button buttonWZ;

	private Button buttonDS;

	private Button buttonCH;

	private Button buttonYCH;

	private Button buttonYDS;

	private Button buttonXTXX;

	private Button buttonYPLD;

	private UCComboBox1 ucComboBox11;

	private UCComboBox1 ucComboBox12;

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
		((ButtonBase)buttonY1).FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
		((ButtonBase)buttonY2).FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
		((ButtonBase)buttonY3).FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
		((ButtonBase)buttonY4).FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
		((ButtonBase)buttonY5).FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
		((ButtonBase)buttonY6).FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
		((ButtonBase)buttonY7).FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
		((ButtonBase)buttonY8).FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
	}

	public UserDeviceSYD1()
	{
		InitializeComponent();
		ClearButtonBouns();
		ucComboBox12.SetUCComboBox(1);
		ucComboBox11.ucComboBox1 = ucComboBoxF1;
		ucComboBox12.ucComboBox1 = ucComboBoxF2;
		ucColor21.upDateUCColor2 = upDateUCColorF1;
		ucColor22.upDateUCColor2 = upDateUCColorF2;
	}

	private void UpDateUCColor1(int r, int g, int b)
	{
		if (myChannelUp_Down)
		{
			rgbR1 = r;
			rgbG1 = g;
			rgbB1 = b;
		}
		else
		{
			rgbR2 = r;
			rgbG2 = g;
			rgbB2 = b;
		}
		((Control)labelUp).BackColor = Color.FromArgb(rgbR1, rgbG1, rgbB1);
		((Control)labelDown).BackColor = Color.FromArgb(rgbR2, rgbG2, rgbB2);
	}

	private void UpDateUCCombo1(int mode)
	{
		if (myChannelUp_Down)
		{
			myMode1 = mode;
		}
		else
		{
			myMode2 = mode;
		}
	}

	private void UpDateUCColorM1(int dancai)
	{
		if (myChannelUp_Down)
		{
			danseAndCaihong1 = dancai;
		}
		else
		{
			danseAndCaihong2 = dancai;
		}
		switch (dancai)
		{
		case 1:
			((Control)buttonDS).BackgroundImage = (Image)(object)Resources.BSYDCDS1;
			((Control)buttonCH).BackgroundImage = (Image)(object)Resources.BSYDCCH2;
			break;
		case 2:
			((Control)buttonDS).BackgroundImage = (Image)(object)Resources.BSYDCDS2;
			((Control)buttonCH).BackgroundImage = (Image)(object)Resources.BSYDCCH1;
			break;
		}
	}

	private void UpDateUCColor2(int r, int g, int b)
	{
		rgbR = r;
		rgbG = g;
		rgbB = b;
		((Control)labelYALL).BackColor = Color.FromArgb(r, g, b);
	}

	private void UpDateUCCombo2(int mode)
	{
		myMode = mode;
	}

	private void UpDateUCColorM2(int dancai)
	{
		danseAndCaihong = dancai;
		switch (dancai)
		{
		case 1:
			((Control)buttonYDS).BackgroundImage = (Image)(object)Resources.BSYDCDS1;
			((Control)buttonYCH).BackgroundImage = (Image)(object)Resources.BSYDCCH2;
			break;
		case 2:
			((Control)buttonYDS).BackgroundImage = (Image)(object)Resources.BSYDCDS2;
			((Control)buttonYCH).BackgroundImage = (Image)(object)Resources.BSYDCCH1;
			break;
		}
	}

	private void upDateUCColorF1(int r, int g, int b)
	{
		buttonONOFF_ToON();
		UpDateUCColor1(r, g, b);
		SendLEDData();
	}

	private void upDateUCColorF2(int r, int g, int b)
	{
		buttonONOFF_ToON();
		UpDateUCColor2(r, g, b);
		SendLEDData();
	}

	private void ucComboBoxF1(int mode)
	{
		buttonONOFF_ToON();
		UpDateUCCombo1(mode);
		SendLEDData();
	}

	private void ucComboBoxF2(int mode)
	{
		buttonONOFF_ToON();
		UpDateUCCombo2(mode);
		SendLEDData();
	}

	private void buttonDS_Click(object sender, EventArgs e)
	{
		buttonONOFF_ToON();
		UpDateUCColorM1(1);
		SendLEDData();
	}

	private void buttonCH_Click(object sender, EventArgs e)
	{
		buttonONOFF_ToON();
		UpDateUCColorM1(2);
		SendLEDData();
	}

	private void buttonYDS_Click(object sender, EventArgs e)
	{
		buttonONOFF_ToON();
		UpDateUCColorM2(1);
		SendLEDData();
	}

	private void buttonYCH_Click(object sender, EventArgs e)
	{
		buttonONOFF_ToON();
		UpDateUCColorM2(2);
		SendLEDData();
	}

	private void buttonXTXX_Click(object sender, EventArgs e)
	{
		buttonONOFF_ToON();
		isXTXX_YPLD = true;
		((Control)label1).Show();
		((Control)label2).Hide();
		((Control)buttonXTXX).BackgroundImage = (Image)(object)Resources.BSYDMXTXX1;
		((Control)buttonYPLD).BackgroundImage = (Image)(object)Resources.BSYDMYPLD2;
		SendLEDData();
	}

	private void buttonYPLD_Click(object sender, EventArgs e)
	{
		buttonONOFF_ToON();
		isXTXX_YPLD = false;
		((Control)label2).Show();
		((Control)label1).Hide();
		((Control)buttonXTXX).BackgroundImage = (Image)(object)Resources.BSYDMXTXX2;
		((Control)buttonYPLD).BackgroundImage = (Image)(object)Resources.BSYDMYPLD1;
		SendLEDData();
	}

	private void buttonY_Click(object sender, EventArgs e)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Expected O, but got Unknown
		buttonONOFF_ToON();
		Button val = (Button)sender;
		int num = Convert.ToInt32(((Control)val).Name.Substring(7));
		((Control)buttonY1).BackgroundImage = (Image)(object)Resources.BCR1;
		((Control)buttonY2).BackgroundImage = (Image)(object)Resources.BCO1;
		((Control)buttonY3).BackgroundImage = (Image)(object)Resources.BCY1;
		((Control)buttonY4).BackgroundImage = (Image)(object)Resources.BCG1;
		((Control)buttonY5).BackgroundImage = (Image)(object)Resources.BCC1;
		((Control)buttonY6).BackgroundImage = (Image)(object)Resources.BCB1;
		((Control)buttonY7).BackgroundImage = (Image)(object)Resources.BCP1;
		((Control)buttonY8).BackgroundImage = (Image)(object)Resources.BCW1;
		switch (num)
		{
		case 1:
			UpDateUCColor2(255, 0, 0);
			((Control)buttonY1).BackgroundImage = (Image)(object)Resources.BCR2;
			break;
		case 2:
			UpDateUCColor2(255, 127, 0);
			((Control)buttonY2).BackgroundImage = (Image)(object)Resources.BCO2;
			break;
		case 3:
			UpDateUCColor2(255, 255, 0);
			((Control)buttonY3).BackgroundImage = (Image)(object)Resources.BCY2;
			break;
		case 4:
			UpDateUCColor2(127, 255, 0);
			((Control)buttonY4).BackgroundImage = (Image)(object)Resources.BCG2;
			break;
		case 5:
			UpDateUCColor2(0, 255, 255);
			((Control)buttonY5).BackgroundImage = (Image)(object)Resources.BCC2;
			break;
		case 6:
			UpDateUCColor2(0, 127, 255);
			((Control)buttonY6).BackgroundImage = (Image)(object)Resources.BCB2;
			break;
		case 7:
			UpDateUCColor2(255, 0, 255);
			((Control)buttonY7).BackgroundImage = (Image)(object)Resources.BCP2;
			break;
		case 8:
			UpDateUCColor2(255, 255, 255);
			((Control)buttonY8).BackgroundImage = (Image)(object)Resources.BCW2;
			break;
		}
		SendLEDData();
	}

	private void buttonNZ_Click(object sender, EventArgs e)
	{
		buttonONOFF_ToON();
		nzyp = true;
		((Control)buttonNZ).BackgroundImage = (Image)(object)Resources.BSYDNZ1;
		((Control)buttonWZ).BackgroundImage = (Image)(object)Resources.BSYDWZ2;
		SendLEDData();
	}

	private void buttonWZ_Click(object sender, EventArgs e)
	{
		buttonONOFF_ToON();
		nzyp = false;
		((Control)buttonNZ).BackgroundImage = (Image)(object)Resources.BSYDNZ2;
		((Control)buttonWZ).BackgroundImage = (Image)(object)Resources.BSYDWZ1;
		SendLEDData();
	}

	private void button_Click(object sender, EventArgs e)
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

	private void buttonUpDown_Click(object sender, EventArgs e)
	{
		if (myChannelUp_Down)
		{
			myChannelUp_Down = false;
			((Control)buttonUpDown).BackgroundImage = (Image)(object)Resources.SYDDown;
			ucComboBox11.SetUCComboBoxMode(myMode2);
			UpDateUCColorM1(danseAndCaihong2);
		}
		else
		{
			myChannelUp_Down = true;
			((Control)buttonUpDown).BackgroundImage = (Image)(object)Resources.SYDUp;
			ucComboBox11.SetUCComboBoxMode(myMode1);
			UpDateUCColorM1(danseAndCaihong1);
		}
	}

	private void SendLEDData()
	{
		int cmd = 0;
		if (onOff)
		{
			cmd = ((!isXTXX_YPLD) ? 48 : 32);
		}
		byte[] obj = new byte[19]
		{
			(byte)myBrightness,
			(byte)mySpeed,
			(byte)rgbR1,
			(byte)rgbG1,
			(byte)rgbB1,
			(byte)rgbR2,
			(byte)rgbG2,
			(byte)rgbB2,
			(byte)rgbR,
			(byte)rgbG,
			(byte)rgbB,
			(byte)myMode1,
			(byte)myMode2,
			(byte)myMode,
			(byte)(nzyp ? 1u : 0u),
			(byte)danseAndCaihong1,
			(byte)danseAndCaihong2,
			(byte)danseAndCaihong,
			0
		};
		ucDeviceLEDS1?.Invoke(myChannel, cmd, obj);
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
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Expected O, but got Unknown
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Expected O, but got Unknown
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Expected O, but got Unknown
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0aab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0da8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fa8: Unknown result type (might be due to invalid IL or missing references)
		//IL_10a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_12a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_13a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_1441: Unknown result type (might be due to invalid IL or missing references)
		//IL_144b: Expected O, but got Unknown
		//IL_147f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1501: Unknown result type (might be due to invalid IL or missing references)
		//IL_150b: Expected O, but got Unknown
		//IL_153f: Unknown result type (might be due to invalid IL or missing references)
		//IL_15c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_15cb: Expected O, but got Unknown
		//IL_15ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_168e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1698: Expected O, but got Unknown
		//IL_16cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_174e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1758: Expected O, but got Unknown
		//IL_179a: Unknown result type (might be due to invalid IL or missing references)
		//IL_181c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1826: Expected O, but got Unknown
		//IL_186b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1961: Unknown result type (might be due to invalid IL or missing references)
		//IL_1a61: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b61: Unknown result type (might be due to invalid IL or missing references)
		//IL_1c61: Unknown result type (might be due to invalid IL or missing references)
		//IL_1d61: Unknown result type (might be due to invalid IL or missing references)
		//IL_1e61: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_205e: Unknown result type (might be due to invalid IL or missing references)
		//IL_20ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_20f7: Expected O, but got Unknown
		//IL_211a: Unknown result type (might be due to invalid IL or missing references)
		//IL_2187: Unknown result type (might be due to invalid IL or missing references)
		//IL_2191: Expected O, but got Unknown
		//IL_21b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_2218: Unknown result type (might be due to invalid IL or missing references)
		//IL_2222: Expected O, but got Unknown
		//IL_2252: Unknown result type (might be due to invalid IL or missing references)
		//IL_22b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_22c3: Expected O, but got Unknown
		//IL_22f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_2602: Unknown result type (might be due to invalid IL or missing references)
		//IL_260c: Expected O, but got Unknown
		//IL_2621: Unknown result type (might be due to invalid IL or missing references)
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(UserDeviceSYD1));
		buttonONOFF = new Button();
		label = new Label();
		buttonUpDown = new Button();
		button8 = new Button();
		button7 = new Button();
		button6 = new Button();
		button5 = new Button();
		button4 = new Button();
		button3 = new Button();
		button2 = new Button();
		button1 = new Button();
		buttonY8 = new Button();
		buttonY7 = new Button();
		buttonY6 = new Button();
		buttonY5 = new Button();
		buttonY4 = new Button();
		buttonY3 = new Button();
		buttonY2 = new Button();
		buttonY1 = new Button();
		labelDown = new Label();
		labelUp = new Label();
		labelYSub = new Label();
		labelYALL = new Label();
		label1 = new Label();
		label2 = new Label();
		buttonNZ = new Button();
		buttonWZ = new Button();
		buttonDS = new Button();
		buttonCH = new Button();
		buttonYCH = new Button();
		buttonYDS = new Button();
		buttonXTXX = new Button();
		buttonYPLD = new Button();
		ucComboBox12 = new UCComboBox1();
		ucComboBox11 = new UCComboBox1();
		ucColor22 = new UCColor2();
		ucColor21 = new UCColor2();
		((Control)this).SuspendLayout();
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
		((Control)buttonONOFF).TabIndex = 93;
		((ButtonBase)buttonONOFF).UseVisualStyleBackColor = false;
		((Control)buttonONOFF).Click += buttonONOFF_Click;
		label.Image = (Image)(object)Resources.SYDUD;
		((Control)label).Location = new Point(42, 29);
		((Control)label).Margin = new Padding(0);
		((Control)label).Name = "label";
		((Control)label).Size = new Size(112, 416);
		((Control)label).TabIndex = 92;
		((Control)buttonUpDown).BackColor = Color.Transparent;
		((Control)buttonUpDown).BackgroundImage = (Image)(object)Resources.SYDUp;
		((Control)buttonUpDown).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonUpDown).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonUpDown).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonUpDown).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonUpDown).FlatStyle = (FlatStyle)0;
		((Control)buttonUpDown).Location = new Point(227, 107);
		((Control)buttonUpDown).Margin = new Padding(0);
		((Control)buttonUpDown).Name = "buttonUpDown";
		((Control)buttonUpDown).Size = new Size(22, 95);
		((Control)buttonUpDown).TabIndex = 96;
		((ButtonBase)buttonUpDown).UseVisualStyleBackColor = false;
		((Control)buttonUpDown).Click += buttonUpDown_Click;
		((Control)button8).BackColor = Color.Transparent;
		((Control)button8).BackgroundImage = (Image)(object)Resources.BCW1;
		((Control)button8).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button8).FlatAppearance.BorderSize = 0;
		((ButtonBase)button8).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button8).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button8).FlatStyle = (FlatStyle)0;
		((Control)button8).Location = new Point(527, 191);
		((Control)button8).Margin = new Padding(0);
		((Control)button8).Name = "button8";
		((Control)button8).Size = new Size(21, 21);
		((Control)button8).TabIndex = 104;
		((ButtonBase)button8).UseVisualStyleBackColor = false;
		((Control)button8).Click += button_Click;
		((Control)button7).BackColor = Color.Transparent;
		((Control)button7).BackgroundImage = (Image)(object)Resources.BCP1;
		((Control)button7).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button7).FlatAppearance.BorderSize = 0;
		((ButtonBase)button7).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button7).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button7).FlatStyle = (FlatStyle)0;
		((Control)button7).Location = new Point(527, 159);
		((Control)button7).Margin = new Padding(0);
		((Control)button7).Name = "button7";
		((Control)button7).Size = new Size(21, 21);
		((Control)button7).TabIndex = 103;
		((ButtonBase)button7).UseVisualStyleBackColor = false;
		((Control)button7).Click += button_Click;
		((Control)button6).BackColor = Color.Transparent;
		((Control)button6).BackgroundImage = (Image)(object)Resources.BCB1;
		((Control)button6).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button6).FlatAppearance.BorderSize = 0;
		((ButtonBase)button6).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button6).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button6).FlatStyle = (FlatStyle)0;
		((Control)button6).Location = new Point(527, 127);
		((Control)button6).Margin = new Padding(0);
		((Control)button6).Name = "button6";
		((Control)button6).Size = new Size(21, 21);
		((Control)button6).TabIndex = 102;
		((ButtonBase)button6).UseVisualStyleBackColor = false;
		((Control)button6).Click += button_Click;
		((Control)button5).BackColor = Color.Transparent;
		((Control)button5).BackgroundImage = (Image)(object)Resources.BCC1;
		((Control)button5).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button5).FlatAppearance.BorderSize = 0;
		((ButtonBase)button5).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button5).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button5).FlatStyle = (FlatStyle)0;
		((Control)button5).Location = new Point(527, 93);
		((Control)button5).Margin = new Padding(0);
		((Control)button5).Name = "button5";
		((Control)button5).Size = new Size(21, 21);
		((Control)button5).TabIndex = 101;
		((ButtonBase)button5).UseVisualStyleBackColor = false;
		((Control)button5).Click += button_Click;
		((Control)button4).BackColor = Color.Transparent;
		((Control)button4).BackgroundImage = (Image)(object)Resources.BCG1;
		((Control)button4).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button4).FlatAppearance.BorderSize = 0;
		((ButtonBase)button4).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button4).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button4).FlatStyle = (FlatStyle)0;
		((Control)button4).Location = new Point(495, 191);
		((Control)button4).Margin = new Padding(0);
		((Control)button4).Name = "button4";
		((Control)button4).Size = new Size(21, 21);
		((Control)button4).TabIndex = 100;
		((ButtonBase)button4).UseVisualStyleBackColor = false;
		((Control)button4).Click += button_Click;
		((Control)button3).BackColor = Color.Transparent;
		((Control)button3).BackgroundImage = (Image)(object)Resources.BCY1;
		((Control)button3).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button3).FlatAppearance.BorderSize = 0;
		((ButtonBase)button3).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button3).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button3).FlatStyle = (FlatStyle)0;
		((Control)button3).Location = new Point(495, 159);
		((Control)button3).Margin = new Padding(0);
		((Control)button3).Name = "button3";
		((Control)button3).Size = new Size(21, 21);
		((Control)button3).TabIndex = 99;
		((ButtonBase)button3).UseVisualStyleBackColor = false;
		((Control)button3).Click += button_Click;
		((Control)button2).BackColor = Color.Transparent;
		((Control)button2).BackgroundImage = (Image)(object)Resources.BCO1;
		((Control)button2).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button2).FlatAppearance.BorderSize = 0;
		((ButtonBase)button2).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button2).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button2).FlatStyle = (FlatStyle)0;
		((Control)button2).Location = new Point(495, 127);
		((Control)button2).Margin = new Padding(0);
		((Control)button2).Name = "button2";
		((Control)button2).Size = new Size(21, 21);
		((Control)button2).TabIndex = 98;
		((ButtonBase)button2).UseVisualStyleBackColor = false;
		((Control)button2).Click += button_Click;
		((Control)button1).BackColor = Color.Transparent;
		((Control)button1).BackgroundImage = (Image)(object)Resources.BCSYDR1;
		((Control)button1).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button1).FlatAppearance.BorderSize = 0;
		((ButtonBase)button1).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button1).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button1).FlatStyle = (FlatStyle)0;
		((Control)button1).Location = new Point(495, 93);
		((Control)button1).Margin = new Padding(0);
		((Control)button1).Name = "button1";
		((Control)button1).Size = new Size(21, 21);
		((Control)button1).TabIndex = 97;
		((ButtonBase)button1).UseVisualStyleBackColor = false;
		((Control)button1).Click += button_Click;
		((Control)buttonY8).BackColor = Color.Transparent;
		((Control)buttonY8).BackgroundImage = (Image)(object)Resources.BCW1;
		((Control)buttonY8).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonY8).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonY8).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonY8).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonY8).FlatStyle = (FlatStyle)0;
		((Control)buttonY8).Location = new Point(527, 392);
		((Control)buttonY8).Margin = new Padding(0);
		((Control)buttonY8).Name = "buttonY8";
		((Control)buttonY8).Size = new Size(21, 21);
		((Control)buttonY8).TabIndex = 112;
		((ButtonBase)buttonY8).UseVisualStyleBackColor = false;
		((Control)buttonY8).Click += buttonY_Click;
		((Control)buttonY7).BackColor = Color.Transparent;
		((Control)buttonY7).BackgroundImage = (Image)(object)Resources.BCP1;
		((Control)buttonY7).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonY7).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonY7).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonY7).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonY7).FlatStyle = (FlatStyle)0;
		((Control)buttonY7).Location = new Point(527, 360);
		((Control)buttonY7).Margin = new Padding(0);
		((Control)buttonY7).Name = "buttonY7";
		((Control)buttonY7).Size = new Size(21, 21);
		((Control)buttonY7).TabIndex = 111;
		((ButtonBase)buttonY7).UseVisualStyleBackColor = false;
		((Control)buttonY7).Click += buttonY_Click;
		((Control)buttonY6).BackColor = Color.Transparent;
		((Control)buttonY6).BackgroundImage = (Image)(object)Resources.BCB1;
		((Control)buttonY6).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonY6).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonY6).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonY6).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonY6).FlatStyle = (FlatStyle)0;
		((Control)buttonY6).Location = new Point(527, 328);
		((Control)buttonY6).Margin = new Padding(0);
		((Control)buttonY6).Name = "buttonY6";
		((Control)buttonY6).Size = new Size(21, 21);
		((Control)buttonY6).TabIndex = 110;
		((ButtonBase)buttonY6).UseVisualStyleBackColor = false;
		((Control)buttonY6).Click += buttonY_Click;
		((Control)buttonY5).BackColor = Color.Transparent;
		((Control)buttonY5).BackgroundImage = (Image)(object)Resources.BCC1;
		((Control)buttonY5).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonY5).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonY5).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonY5).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonY5).FlatStyle = (FlatStyle)0;
		((Control)buttonY5).Location = new Point(527, 294);
		((Control)buttonY5).Margin = new Padding(0);
		((Control)buttonY5).Name = "buttonY5";
		((Control)buttonY5).Size = new Size(21, 21);
		((Control)buttonY5).TabIndex = 109;
		((ButtonBase)buttonY5).UseVisualStyleBackColor = false;
		((Control)buttonY5).Click += buttonY_Click;
		((Control)buttonY4).BackColor = Color.Transparent;
		((Control)buttonY4).BackgroundImage = (Image)(object)Resources.BCG1;
		((Control)buttonY4).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonY4).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonY4).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonY4).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonY4).FlatStyle = (FlatStyle)0;
		((Control)buttonY4).Location = new Point(495, 392);
		((Control)buttonY4).Margin = new Padding(0);
		((Control)buttonY4).Name = "buttonY4";
		((Control)buttonY4).Size = new Size(21, 21);
		((Control)buttonY4).TabIndex = 108;
		((ButtonBase)buttonY4).UseVisualStyleBackColor = false;
		((Control)buttonY4).Click += buttonY_Click;
		((Control)buttonY3).BackColor = Color.Transparent;
		((Control)buttonY3).BackgroundImage = (Image)(object)Resources.BCY1;
		((Control)buttonY3).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonY3).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonY3).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonY3).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonY3).FlatStyle = (FlatStyle)0;
		((Control)buttonY3).Location = new Point(495, 360);
		((Control)buttonY3).Margin = new Padding(0);
		((Control)buttonY3).Name = "buttonY3";
		((Control)buttonY3).Size = new Size(21, 21);
		((Control)buttonY3).TabIndex = 107;
		((ButtonBase)buttonY3).UseVisualStyleBackColor = false;
		((Control)buttonY3).Click += buttonY_Click;
		((Control)buttonY2).BackColor = Color.Transparent;
		((Control)buttonY2).BackgroundImage = (Image)(object)Resources.BCO1;
		((Control)buttonY2).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonY2).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonY2).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonY2).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonY2).FlatStyle = (FlatStyle)0;
		((Control)buttonY2).Location = new Point(495, 328);
		((Control)buttonY2).Margin = new Padding(0);
		((Control)buttonY2).Name = "buttonY2";
		((Control)buttonY2).Size = new Size(21, 21);
		((Control)buttonY2).TabIndex = 106;
		((ButtonBase)buttonY2).UseVisualStyleBackColor = false;
		((Control)buttonY2).Click += buttonY_Click;
		((Control)buttonY1).BackColor = Color.Transparent;
		((Control)buttonY1).BackgroundImage = (Image)(object)Resources.BCSYDR1;
		((Control)buttonY1).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonY1).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonY1).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonY1).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonY1).FlatStyle = (FlatStyle)0;
		((Control)buttonY1).Location = new Point(495, 294);
		((Control)buttonY1).Margin = new Padding(0);
		((Control)buttonY1).Name = "buttonY1";
		((Control)buttonY1).Size = new Size(21, 21);
		((Control)buttonY1).TabIndex = 105;
		((ButtonBase)buttonY1).UseVisualStyleBackColor = false;
		((Control)buttonY1).Click += buttonY_Click;
		((Control)labelDown).BackColor = Color.Red;
		labelDown.FlatStyle = (FlatStyle)0;
		((Control)labelDown).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)labelDown).ForeColor = Color.Red;
		((Control)labelDown).Location = new Point(457, 199);
		((Control)labelDown).Margin = new Padding(0);
		((Control)labelDown).Name = "labelDown";
		((Control)labelDown).Size = new Size(16, 16);
		((Control)labelDown).TabIndex = 114;
		labelDown.TextAlign = (ContentAlignment)16;
		((Control)labelUp).BackColor = Color.Red;
		labelUp.FlatStyle = (FlatStyle)0;
		((Control)labelUp).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)labelUp).ForeColor = Color.Red;
		((Control)labelUp).Location = new Point(457, 182);
		((Control)labelUp).Margin = new Padding(0);
		((Control)labelUp).Name = "labelUp";
		((Control)labelUp).Size = new Size(16, 16);
		((Control)labelUp).TabIndex = 113;
		labelUp.TextAlign = (ContentAlignment)16;
		((Control)labelYSub).BackColor = Color.Red;
		labelYSub.FlatStyle = (FlatStyle)0;
		((Control)labelYSub).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)labelYSub).ForeColor = Color.Red;
		((Control)labelYSub).Location = new Point(457, 407);
		((Control)labelYSub).Margin = new Padding(0);
		((Control)labelYSub).Name = "labelYSub";
		((Control)labelYSub).Size = new Size(16, 16);
		((Control)labelYSub).TabIndex = 116;
		labelYSub.TextAlign = (ContentAlignment)16;
		((Control)labelYSub).Visible = false;
		((Control)labelYALL).BackColor = Color.Red;
		labelYALL.FlatStyle = (FlatStyle)0;
		((Control)labelYALL).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)labelYALL).ForeColor = Color.Red;
		((Control)labelYALL).Location = new Point(457, 390);
		((Control)labelYALL).Margin = new Padding(0);
		((Control)labelYALL).Name = "labelYALL";
		((Control)labelYALL).Size = new Size(16, 16);
		((Control)labelYALL).TabIndex = 115;
		labelYALL.TextAlign = (ContentAlignment)16;
		((Control)label1).BackColor = Color.Transparent;
		label1.FlatStyle = (FlatStyle)0;
		((Control)label1).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label1).ForeColor = Color.Red;
		label1.Image = (Image)(object)Resources.LSYDMK;
		((Control)label1).Location = new Point(232, 69);
		((Control)label1).Margin = new Padding(0);
		((Control)label1).Name = "label1";
		((Control)label1).Size = new Size(9, 9);
		((Control)label1).TabIndex = 117;
		label1.TextAlign = (ContentAlignment)16;
		((Control)label2).BackColor = Color.Transparent;
		label2.FlatStyle = (FlatStyle)0;
		((Control)label2).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label2).ForeColor = Color.Red;
		label2.Image = (Image)(object)Resources.LSYDMK;
		((Control)label2).Location = new Point(232, 269);
		((Control)label2).Margin = new Padding(0);
		((Control)label2).Name = "label2";
		((Control)label2).Size = new Size(9, 9);
		((Control)label2).TabIndex = 118;
		label2.TextAlign = (ContentAlignment)16;
		((Control)label2).Visible = false;
		((Control)buttonNZ).BackColor = Color.Transparent;
		((Control)buttonNZ).BackgroundImage = (Image)(object)Resources.BSYDNZ1;
		((Control)buttonNZ).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonNZ).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonNZ).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonNZ).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonNZ).FlatStyle = (FlatStyle)0;
		((Control)buttonNZ).Location = new Point(582, 382);
		((Control)buttonNZ).Margin = new Padding(0);
		((Control)buttonNZ).Name = "buttonNZ";
		((Control)buttonNZ).Size = new Size(53, 11);
		((Control)buttonNZ).TabIndex = 121;
		((ButtonBase)buttonNZ).UseVisualStyleBackColor = false;
		((Control)buttonNZ).Click += buttonNZ_Click;
		((Control)buttonWZ).BackColor = Color.Transparent;
		((Control)buttonWZ).BackgroundImage = (Image)(object)Resources.BSYDWZ2;
		((Control)buttonWZ).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonWZ).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonWZ).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonWZ).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonWZ).FlatStyle = (FlatStyle)0;
		((Control)buttonWZ).Location = new Point(582, 400);
		((Control)buttonWZ).Margin = new Padding(0);
		((Control)buttonWZ).Name = "buttonWZ";
		((Control)buttonWZ).Size = new Size(53, 11);
		((Control)buttonWZ).TabIndex = 122;
		((ButtonBase)buttonWZ).UseVisualStyleBackColor = false;
		((Control)buttonWZ).Click += buttonWZ_Click;
		((Control)buttonDS).BackColor = Color.Transparent;
		((Control)buttonDS).BackgroundImage = (Image)(object)Resources.BSYDCDS1;
		((Control)buttonDS).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonDS).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonDS).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonDS).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonDS).FlatStyle = (FlatStyle)0;
		((Control)buttonDS).Location = new Point(584, 138);
		((Control)buttonDS).Margin = new Padding(0);
		((Control)buttonDS).Name = "buttonDS";
		((Control)buttonDS).Size = new Size(61, 27);
		((Control)buttonDS).TabIndex = 123;
		((ButtonBase)buttonDS).UseVisualStyleBackColor = false;
		((Control)buttonDS).Click += buttonDS_Click;
		((Control)buttonCH).BackColor = Color.Transparent;
		((Control)buttonCH).BackgroundImage = (Image)(object)Resources.BSYDCCH2;
		((Control)buttonCH).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonCH).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonCH).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonCH).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonCH).FlatStyle = (FlatStyle)0;
		((Control)buttonCH).Location = new Point(722, 138);
		((Control)buttonCH).Margin = new Padding(0);
		((Control)buttonCH).Name = "buttonCH";
		((Control)buttonCH).Size = new Size(61, 27);
		((Control)buttonCH).TabIndex = 124;
		((ButtonBase)buttonCH).UseVisualStyleBackColor = false;
		((Control)buttonCH).Click += buttonCH_Click;
		((Control)buttonYCH).BackColor = Color.Transparent;
		((Control)buttonYCH).BackgroundImage = (Image)(object)Resources.BSYDCCH2;
		((Control)buttonYCH).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonYCH).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonYCH).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonYCH).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonYCH).FlatStyle = (FlatStyle)0;
		((Control)buttonYCH).Location = new Point(722, 340);
		((Control)buttonYCH).Margin = new Padding(0);
		((Control)buttonYCH).Name = "buttonYCH";
		((Control)buttonYCH).Size = new Size(61, 27);
		((Control)buttonYCH).TabIndex = 126;
		((ButtonBase)buttonYCH).UseVisualStyleBackColor = false;
		((Control)buttonYCH).Click += buttonYCH_Click;
		((Control)buttonYDS).BackColor = Color.Transparent;
		((Control)buttonYDS).BackgroundImage = (Image)(object)Resources.BSYDCDS1;
		((Control)buttonYDS).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonYDS).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonYDS).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonYDS).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonYDS).FlatStyle = (FlatStyle)0;
		((Control)buttonYDS).Location = new Point(584, 340);
		((Control)buttonYDS).Margin = new Padding(0);
		((Control)buttonYDS).Name = "buttonYDS";
		((Control)buttonYDS).Size = new Size(61, 27);
		((Control)buttonYDS).TabIndex = 125;
		((ButtonBase)buttonYDS).UseVisualStyleBackColor = false;
		((Control)buttonYDS).Click += buttonYDS_Click;
		((Control)buttonXTXX).BackColor = Color.Transparent;
		((Control)buttonXTXX).BackgroundImage = (Image)(object)Resources.BSYDMXTXX1;
		((Control)buttonXTXX).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonXTXX).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonXTXX).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonXTXX).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonXTXX).FlatStyle = (FlatStyle)0;
		((Control)buttonXTXX).Location = new Point(762, 45);
		((Control)buttonXTXX).Margin = new Padding(0);
		((Control)buttonXTXX).Name = "buttonXTXX";
		((Control)buttonXTXX).Size = new Size(53, 12);
		((Control)buttonXTXX).TabIndex = 127;
		((ButtonBase)buttonXTXX).UseVisualStyleBackColor = false;
		((Control)buttonXTXX).Click += buttonXTXX_Click;
		((Control)buttonYPLD).BackColor = Color.Transparent;
		((Control)buttonYPLD).BackgroundImage = (Image)(object)Resources.BSYDMYPLD2;
		((Control)buttonYPLD).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonYPLD).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonYPLD).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonYPLD).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonYPLD).FlatStyle = (FlatStyle)0;
		((Control)buttonYPLD).Location = new Point(762, 450);
		((Control)buttonYPLD).Margin = new Padding(0);
		((Control)buttonYPLD).Name = "buttonYPLD";
		((Control)buttonYPLD).Size = new Size(52, 12);
		((Control)buttonYPLD).TabIndex = 128;
		((ButtonBase)buttonYPLD).UseVisualStyleBackColor = false;
		((Control)buttonYPLD).Click += buttonYPLD_Click;
		((Control)ucComboBox12).BackColor = Color.Transparent;
		((Control)ucComboBox12).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)ucComboBox12).Location = new Point(583, 296);
		((Control)ucComboBox12).Margin = new Padding(0);
		((Control)ucComboBox12).Name = "ucComboBox12";
		((Control)ucComboBox12).Size = new Size(201, 26);
		((Control)ucComboBox12).TabIndex = 130;
		((Control)ucComboBox11).BackColor = Color.Transparent;
		((Control)ucComboBox11).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)ucComboBox11).Location = new Point(583, 94);
		((Control)ucComboBox11).Margin = new Padding(0);
		((Control)ucComboBox11).Name = "ucComboBox11";
		((Control)ucComboBox11).Size = new Size(201, 26);
		((Control)ucComboBox11).TabIndex = 129;
		((Control)ucColor22).BackColor = Color.Transparent;
		((Control)ucColor22).BackgroundImage = (Image)componentResourceManager.GetObject("ucColor22.BackgroundImage");
		((Control)ucColor22).BackgroundImageLayout = (ImageLayout)2;
		((Control)ucColor22).Location = new Point(291, 283);
		((Control)ucColor22).Margin = new Padding(0);
		((Control)ucColor22).Name = "ucColor22";
		((Control)ucColor22).Size = new Size(139, 139);
		((Control)ucColor22).TabIndex = 95;
		((Control)ucColor21).BackColor = Color.Transparent;
		((Control)ucColor21).BackgroundImage = (Image)componentResourceManager.GetObject("ucColor21.BackgroundImage");
		((Control)ucColor21).BackgroundImageLayout = (ImageLayout)2;
		((Control)ucColor21).Location = new Point(291, 84);
		((Control)ucColor21).Margin = new Padding(0);
		((Control)ucColor21).Name = "ucColor21";
		((Control)ucColor21).Size = new Size(139, 139);
		((Control)ucColor21).TabIndex = 94;
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)3;
		((Control)this).BackColor = Color.Transparent;
		((Control)this).BackgroundImage = (Image)(object)Resources.SydZHBK;
		((Control)this).Controls.Add((Control)(object)ucComboBox12);
		((Control)this).Controls.Add((Control)(object)ucComboBox11);
		((Control)this).Controls.Add((Control)(object)buttonYPLD);
		((Control)this).Controls.Add((Control)(object)buttonXTXX);
		((Control)this).Controls.Add((Control)(object)buttonYCH);
		((Control)this).Controls.Add((Control)(object)buttonYDS);
		((Control)this).Controls.Add((Control)(object)buttonCH);
		((Control)this).Controls.Add((Control)(object)buttonDS);
		((Control)this).Controls.Add((Control)(object)buttonWZ);
		((Control)this).Controls.Add((Control)(object)buttonNZ);
		((Control)this).Controls.Add((Control)(object)label2);
		((Control)this).Controls.Add((Control)(object)label1);
		((Control)this).Controls.Add((Control)(object)labelYSub);
		((Control)this).Controls.Add((Control)(object)labelYALL);
		((Control)this).Controls.Add((Control)(object)labelDown);
		((Control)this).Controls.Add((Control)(object)labelUp);
		((Control)this).Controls.Add((Control)(object)buttonY8);
		((Control)this).Controls.Add((Control)(object)buttonY7);
		((Control)this).Controls.Add((Control)(object)buttonY6);
		((Control)this).Controls.Add((Control)(object)buttonY5);
		((Control)this).Controls.Add((Control)(object)buttonY4);
		((Control)this).Controls.Add((Control)(object)buttonY3);
		((Control)this).Controls.Add((Control)(object)buttonY2);
		((Control)this).Controls.Add((Control)(object)buttonY1);
		((Control)this).Controls.Add((Control)(object)button8);
		((Control)this).Controls.Add((Control)(object)button7);
		((Control)this).Controls.Add((Control)(object)button6);
		((Control)this).Controls.Add((Control)(object)button5);
		((Control)this).Controls.Add((Control)(object)button4);
		((Control)this).Controls.Add((Control)(object)button3);
		((Control)this).Controls.Add((Control)(object)button2);
		((Control)this).Controls.Add((Control)(object)button1);
		((Control)this).Controls.Add((Control)(object)buttonUpDown);
		((Control)this).Controls.Add((Control)(object)ucColor22);
		((Control)this).Controls.Add((Control)(object)ucColor21);
		((Control)this).Controls.Add((Control)(object)buttonONOFF);
		((Control)this).Controls.Add((Control)(object)label);
		((Control)this).DoubleBuffered = true;
		((Control)this).Font = new Font("微软雅黑", 9f);
		((Control)this).Location = new Point(0, 250);
		((Control)this).Margin = new Padding(0);
		((Control)this).Name = "UserDeviceSYD1";
		((Control)this).Size = new Size(916, 550);
		((Control)this).ResumeLayout(false);
	}
}
