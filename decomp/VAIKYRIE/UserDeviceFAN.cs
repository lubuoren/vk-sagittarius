using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using VAIKYRIE.Properties;

namespace VAIKYRIE;

public class UserDeviceFAN : UserControl
{
	public delegate void delegateUserDeviceFAN(int ch, int cmd, object obj);

	public delegateUserDeviceFAN ucDeviceFANF;

	private readonly byte[] fanTemp1 = new byte[3] { 30, 70, 75 };

	private readonly byte[] fanTemp2 = new byte[3] { 40, 60, 75 };

	private readonly byte[] fanTemp3 = new byte[3] { 25, 70, 75 };

	private readonly byte[] fanTemp4 = new byte[3] { 30, 70, 75 };

	private readonly byte[] fanPWM1 = new byte[4] { 0, 0, 65, 100 };

	private readonly byte[] fanPWM2 = new byte[4] { 20, 20, 60, 100 };

	private readonly byte[] fanPWM3 = new byte[4] { 30, 30, 99, 100 };

	private readonly byte[] fanPWM4 = new byte[4] { 97, 98, 99, 100 };

	public int myMode = 2;

	public int myChannel = 1;

	private bool isSendData = false;

	public int[] myALLMode = new int[5] { 2, 2, 2, 2, 2 };

	private IContainer components = null;

	private Button button1;

	private Button button2;

	private Button button3;

	private Button button4;

	private Button buttonJY;

	private Button buttonBZ;

	private Button buttonXN;

	private Button buttonQS;

	public Label label1;

	public Label label2;

	public Label label3;

	public Label label4;

	private Label label;

	private Button buttonZDY;

	private UCDevFan1 ucDevFan1;

	public UserDeviceFAN()
	{
		InitializeComponent();
		ucDevFan1.ucDeviceFAN1 = delegateUCDevFan;
	}

	private void delegateUCDevFan()
	{
		SendFanData();
	}

	private void Button_Click(int ch)
	{
		((Control)button1).BackgroundImage = (Image)(object)Resources.BFC12;
		((Control)button2).BackgroundImage = (Image)(object)Resources.BFC22;
		((Control)button3).BackgroundImage = (Image)(object)Resources.BFC32;
		((Control)button4).BackgroundImage = (Image)(object)Resources.BFC42;
		switch (ch)
		{
		case 0:
			break;
		case 1:
			((Control)button1).BackgroundImage = (Image)(object)Resources.BFC11;
			break;
		case 2:
			((Control)button2).BackgroundImage = (Image)(object)Resources.BFC21;
			break;
		case 3:
			((Control)button3).BackgroundImage = (Image)(object)Resources.BFC31;
			break;
		case 4:
			((Control)button4).BackgroundImage = (Image)(object)Resources.BFC41;
			break;
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		myChannel = 1;
		Button_Click(myChannel);
		ButtonMM_Click(myALLMode[myChannel]);
	}

	private void button2_Click(object sender, EventArgs e)
	{
		myChannel = 2;
		Button_Click(myChannel);
		ButtonMM_Click(myALLMode[myChannel]);
	}

	private void button3_Click(object sender, EventArgs e)
	{
		myChannel = 3;
		Button_Click(myChannel);
		ButtonMM_Click(myALLMode[myChannel]);
	}

	private void button4_Click(object sender, EventArgs e)
	{
		myChannel = 4;
		Button_Click(myChannel);
		ButtonMM_Click(myALLMode[myChannel]);
	}

	private void ButtonMM_Click(int mode)
	{
		if (myChannel == 0)
		{
			myALLMode[0] = (myALLMode[1] = (myALLMode[2] = (myALLMode[3] = (myALLMode[4] = mode))));
		}
		else
		{
			myALLMode[myChannel] = mode;
		}
		ButtonM_Click(mode);
	}

	private void ButtonM_Click(int mode)
	{
		((Control)buttonJY).BackgroundImage = (Image)(object)Resources.BFJY2;
		((Control)buttonBZ).BackgroundImage = (Image)(object)Resources.BFBZ2;
		((Control)buttonXN).BackgroundImage = (Image)(object)Resources.BFXN2;
		((Control)buttonQS).BackgroundImage = (Image)(object)Resources.BFQS2;
		((Control)buttonZDY).BackgroundImage = (Image)(object)Resources.BFZDY2;
		switch (mode)
		{
		case 1:
			((Control)buttonJY).BackgroundImage = (Image)(object)Resources.BFJY1;
			((Control)ucDevFan1).Hide();
			((Control)label).Show();
			label.Image = (Image)(object)Resources.FANJY;
			break;
		case 2:
			((Control)buttonBZ).BackgroundImage = (Image)(object)Resources.BFBZ1;
			((Control)ucDevFan1).Hide();
			((Control)label).Show();
			label.Image = (Image)(object)Resources.FANBZ;
			break;
		case 3:
			((Control)buttonXN).BackgroundImage = (Image)(object)Resources.BFXN1;
			((Control)ucDevFan1).Hide();
			((Control)label).Show();
			label.Image = (Image)(object)Resources.FANXN;
			break;
		case 4:
			((Control)buttonQS).BackgroundImage = (Image)(object)Resources.BFQS1;
			((Control)ucDevFan1).Hide();
			((Control)label).Show();
			label.Image = (Image)(object)Resources.FANQS;
			break;
		case 5:
			((Control)buttonZDY).BackgroundImage = (Image)(object)Resources.BFZDY1;
			((Control)label).Hide();
			((Control)ucDevFan1).Show();
			break;
		}
	}

	private void buttonJY_Click(object sender, EventArgs e)
	{
		myMode = 1;
		ButtonMM_Click(myMode);
		SendFanData();
	}

	private void buttonBZ_Click(object sender, EventArgs e)
	{
		myMode = 2;
		ButtonMM_Click(myMode);
		SendFanData();
	}

	private void buttonXN_Click(object sender, EventArgs e)
	{
		myMode = 3;
		ButtonMM_Click(myMode);
		SendFanData();
	}

	private void buttonQS_Click(object sender, EventArgs e)
	{
		myMode = 4;
		ButtonMM_Click(myMode);
		SendFanData();
	}

	private void buttonZDY_Click(object sender, EventArgs e)
	{
		myMode = 5;
		ButtonMM_Click(myMode);
		SendFanData();
	}

	private void SendFanData()
	{
		int num = myALLMode[myChannel];
		byte[] obj = num switch
		{
			1 => fanPWM1.Concat(fanTemp1).ToArray(), 
			2 => fanPWM2.Concat(fanTemp2).ToArray(), 
			3 => fanPWM3.Concat(fanTemp3).ToArray(), 
			4 => fanPWM4.Concat(fanTemp4).ToArray(), 
			_ => new byte[7]
			{
				0,
				(byte)ucDevFan1.fanVal[3],
				(byte)ucDevFan1.fanVal[4],
				(byte)ucDevFan1.fanVal[5],
				(byte)ucDevFan1.fanVal[0],
				(byte)ucDevFan1.fanVal[1],
				(byte)ucDevFan1.fanVal[2]
			}, 
		};
		ucDeviceFANF?.Invoke(myChannel, num, obj);
		isSendData = true;
	}

	public void ReceivedFANData(byte[] data)
	{
		if (data.Length == 0)
		{
			return;
		}
		((Control)label1).Text = (data[17] * 256 + data[16]).ToString();
		((Control)label2).Text = (data[20] * 256 + data[19]).ToString();
		((Control)label3).Text = (data[23] * 256 + data[22]).ToString();
		((Control)label4).Text = (data[26] * 256 + data[25]).ToString();
		if (isSendData)
		{
			isSendData = false;
			return;
		}
		if (myALLMode[myChannel] != data[myChannel * 3 + 12])
		{
			myMode = data[myChannel * 3 + 12];
			ButtonMM_Click(myMode);
		}
		myALLMode[1] = data[15];
		myALLMode[2] = data[18];
		myALLMode[3] = data[21];
		myALLMode[4] = data[24];
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
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_054a: Unknown result type (might be due to invalid IL or missing references)
		//IL_064a: Unknown result type (might be due to invalid IL or missing references)
		//IL_074a: Unknown result type (might be due to invalid IL or missing references)
		//IL_084a: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ed: Expected O, but got Unknown
		//IL_091e: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09bb: Expected O, but got Unknown
		//IL_09ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a82: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8c: Expected O, but got Unknown
		//IL_0ac0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b53: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b5d: Expected O, but got Unknown
		//IL_0b91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d86: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d90: Expected O, but got Unknown
		//IL_0da8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0db2: Expected O, but got Unknown
		//IL_0dd2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f6c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f76: Expected O, but got Unknown
		//IL_0f8b: Unknown result type (might be due to invalid IL or missing references)
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(UserDeviceFAN));
		button1 = new Button();
		button2 = new Button();
		button3 = new Button();
		button4 = new Button();
		buttonJY = new Button();
		buttonBZ = new Button();
		buttonXN = new Button();
		buttonQS = new Button();
		label1 = new Label();
		label2 = new Label();
		label3 = new Label();
		label4 = new Label();
		label = new Label();
		buttonZDY = new Button();
		ucDevFan1 = new UCDevFan1();
		((Control)this).SuspendLayout();
		((Control)button1).BackColor = Color.Transparent;
		((Control)button1).BackgroundImage = (Image)(object)Resources.BFC11;
		((Control)button1).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button1).FlatAppearance.BorderSize = 0;
		((ButtonBase)button1).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button1).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button1).FlatStyle = (FlatStyle)0;
		((Control)button1).Location = new Point(90, 85);
		((Control)button1).Margin = new Padding(0);
		((Control)button1).Name = "button1";
		((Control)button1).Size = new Size(125, 42);
		((Control)button1).TabIndex = 27;
		((ButtonBase)button1).UseVisualStyleBackColor = false;
		((Control)button1).Click += button1_Click;
		((Control)button2).BackColor = Color.Transparent;
		((Control)button2).BackgroundImage = (Image)(object)Resources.BFC22;
		((Control)button2).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button2).FlatAppearance.BorderSize = 0;
		((ButtonBase)button2).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button2).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button2).FlatStyle = (FlatStyle)0;
		((Control)button2).Location = new Point(90, 156);
		((Control)button2).Margin = new Padding(0);
		((Control)button2).Name = "button2";
		((Control)button2).Size = new Size(125, 42);
		((Control)button2).TabIndex = 28;
		((ButtonBase)button2).UseVisualStyleBackColor = false;
		((Control)button2).Click += button2_Click;
		((Control)button3).BackColor = Color.Transparent;
		((Control)button3).BackgroundImage = (Image)(object)Resources.BFC32;
		((Control)button3).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button3).FlatAppearance.BorderSize = 0;
		((ButtonBase)button3).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button3).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button3).FlatStyle = (FlatStyle)0;
		((Control)button3).Location = new Point(90, 227);
		((Control)button3).Margin = new Padding(0);
		((Control)button3).Name = "button3";
		((Control)button3).Size = new Size(125, 42);
		((Control)button3).TabIndex = 29;
		((ButtonBase)button3).UseVisualStyleBackColor = false;
		((Control)button3).Click += button3_Click;
		((Control)button4).BackColor = Color.Transparent;
		((Control)button4).BackgroundImage = (Image)(object)Resources.BFC42;
		((Control)button4).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button4).FlatAppearance.BorderSize = 0;
		((ButtonBase)button4).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button4).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button4).FlatStyle = (FlatStyle)0;
		((Control)button4).Location = new Point(90, 298);
		((Control)button4).Margin = new Padding(0);
		((Control)button4).Name = "button4";
		((Control)button4).Size = new Size(125, 42);
		((Control)button4).TabIndex = 30;
		((ButtonBase)button4).UseVisualStyleBackColor = false;
		((Control)button4).Click += button4_Click;
		((Control)buttonJY).BackColor = Color.Transparent;
		((Control)buttonJY).BackgroundImage = (Image)(object)Resources.BFJY2;
		((Control)buttonJY).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonJY).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonJY).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonJY).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonJY).FlatStyle = (FlatStyle)0;
		((Control)buttonJY).Location = new Point(368, 392);
		((Control)buttonJY).Margin = new Padding(0);
		((Control)buttonJY).Name = "buttonJY";
		((Control)buttonJY).Size = new Size(66, 26);
		((Control)buttonJY).TabIndex = 31;
		((ButtonBase)buttonJY).UseVisualStyleBackColor = false;
		((Control)buttonJY).Click += buttonJY_Click;
		((Control)buttonBZ).BackColor = Color.Transparent;
		((Control)buttonBZ).BackgroundImage = (Image)(object)Resources.BFBZ1;
		((Control)buttonBZ).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonBZ).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonBZ).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonBZ).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonBZ).FlatStyle = (FlatStyle)0;
		((Control)buttonBZ).Location = new Point(474, 392);
		((Control)buttonBZ).Margin = new Padding(0);
		((Control)buttonBZ).Name = "buttonBZ";
		((Control)buttonBZ).Size = new Size(66, 26);
		((Control)buttonBZ).TabIndex = 32;
		((ButtonBase)buttonBZ).UseVisualStyleBackColor = false;
		((Control)buttonBZ).Click += buttonBZ_Click;
		((Control)buttonXN).BackColor = Color.Transparent;
		((Control)buttonXN).BackgroundImage = (Image)(object)Resources.BFXN2;
		((Control)buttonXN).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonXN).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonXN).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonXN).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonXN).FlatStyle = (FlatStyle)0;
		((Control)buttonXN).Location = new Point(580, 392);
		((Control)buttonXN).Margin = new Padding(0);
		((Control)buttonXN).Name = "buttonXN";
		((Control)buttonXN).Size = new Size(66, 26);
		((Control)buttonXN).TabIndex = 33;
		((ButtonBase)buttonXN).UseVisualStyleBackColor = false;
		((Control)buttonXN).Click += buttonXN_Click;
		((Control)buttonQS).BackColor = Color.Transparent;
		((Control)buttonQS).BackgroundImage = (Image)(object)Resources.BFQS2;
		((Control)buttonQS).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonQS).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonQS).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonQS).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonQS).FlatStyle = (FlatStyle)0;
		((Control)buttonQS).Location = new Point(686, 392);
		((Control)buttonQS).Margin = new Padding(0);
		((Control)buttonQS).Name = "buttonQS";
		((Control)buttonQS).Size = new Size(66, 26);
		((Control)buttonQS).TabIndex = 34;
		((ButtonBase)buttonQS).UseVisualStyleBackColor = false;
		((Control)buttonQS).Click += buttonQS_Click;
		((Control)label1).BackColor = Color.Transparent;
		label1.FlatStyle = (FlatStyle)0;
		((Control)label1).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label1).ForeColor = Color.Orange;
		((Control)label1).Location = new Point(224, 94);
		((Control)label1).Margin = new Padding(0);
		((Control)label1).Name = "label1";
		((Control)label1).Size = new Size(97, 24);
		((Control)label1).TabIndex = 57;
		((Control)label1).Text = "255 RPM";
		label1.TextAlign = (ContentAlignment)16;
		((Control)label2).BackColor = Color.Transparent;
		label2.FlatStyle = (FlatStyle)0;
		((Control)label2).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label2).ForeColor = Color.Orange;
		((Control)label2).Location = new Point(224, 165);
		((Control)label2).Margin = new Padding(0);
		((Control)label2).Name = "label2";
		((Control)label2).Size = new Size(97, 24);
		((Control)label2).TabIndex = 58;
		((Control)label2).Text = "255 RPM";
		label2.TextAlign = (ContentAlignment)16;
		((Control)label3).BackColor = Color.Transparent;
		label3.FlatStyle = (FlatStyle)0;
		((Control)label3).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label3).ForeColor = Color.Orange;
		((Control)label3).Location = new Point(224, 236);
		((Control)label3).Margin = new Padding(0);
		((Control)label3).Name = "label3";
		((Control)label3).Size = new Size(97, 24);
		((Control)label3).TabIndex = 59;
		((Control)label3).Text = "255 RPM";
		label3.TextAlign = (ContentAlignment)16;
		((Control)label4).BackColor = Color.Transparent;
		label4.FlatStyle = (FlatStyle)0;
		((Control)label4).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label4).ForeColor = Color.Orange;
		((Control)label4).Location = new Point(224, 307);
		((Control)label4).Margin = new Padding(0);
		((Control)label4).Name = "label4";
		((Control)label4).Size = new Size(97, 24);
		((Control)label4).TabIndex = 60;
		((Control)label4).Text = "255 RPM";
		label4.TextAlign = (ContentAlignment)16;
		label.Image = (Image)(object)Resources.FANBZ;
		((Control)label).Location = new Point(368, 58);
		((Control)label).Margin = new Padding(0);
		((Control)label).Name = "label";
		((Control)label).Size = new Size(492, 309);
		((Control)label).TabIndex = 61;
		((Control)buttonZDY).BackColor = Color.Transparent;
		((Control)buttonZDY).BackgroundImage = (Image)(object)Resources.BFZDY2;
		((Control)buttonZDY).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonZDY).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonZDY).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonZDY).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonZDY).FlatStyle = (FlatStyle)0;
		((Control)buttonZDY).Location = new Point(792, 392);
		((Control)buttonZDY).Margin = new Padding(0);
		((Control)buttonZDY).Name = "buttonZDY";
		((Control)buttonZDY).Size = new Size(66, 26);
		((Control)buttonZDY).TabIndex = 62;
		((ButtonBase)buttonZDY).UseVisualStyleBackColor = false;
		((Control)buttonZDY).Click += buttonZDY_Click;
		((Control)ucDevFan1).BackColor = Color.Transparent;
		((Control)ucDevFan1).BackgroundImage = (Image)componentResourceManager.GetObject("ucDevFan1.BackgroundImage");
		((Control)ucDevFan1).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)ucDevFan1).Location = new Point(368, 58);
		((Control)ucDevFan1).Margin = new Padding(0);
		((Control)ucDevFan1).Name = "ucDevFan1";
		((Control)ucDevFan1).Size = new Size(492, 309);
		((Control)ucDevFan1).TabIndex = 63;
		((Control)ucDevFan1).Visible = false;
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)3;
		((Control)this).BackColor = Color.Transparent;
		((Control)this).BackgroundImage = (Image)(object)Resources.Background4_1;
		((Control)this).Controls.Add((Control)(object)buttonZDY);
		((Control)this).Controls.Add((Control)(object)label4);
		((Control)this).Controls.Add((Control)(object)label3);
		((Control)this).Controls.Add((Control)(object)label2);
		((Control)this).Controls.Add((Control)(object)label1);
		((Control)this).Controls.Add((Control)(object)buttonQS);
		((Control)this).Controls.Add((Control)(object)buttonXN);
		((Control)this).Controls.Add((Control)(object)buttonBZ);
		((Control)this).Controls.Add((Control)(object)buttonJY);
		((Control)this).Controls.Add((Control)(object)button4);
		((Control)this).Controls.Add((Control)(object)button3);
		((Control)this).Controls.Add((Control)(object)button2);
		((Control)this).Controls.Add((Control)(object)button1);
		((Control)this).Controls.Add((Control)(object)ucDevFan1);
		((Control)this).Controls.Add((Control)(object)label);
		((Control)this).DoubleBuffered = true;
		((Control)this).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)this).Location = new Point(0, 250);
		((Control)this).Margin = new Padding(0);
		((Control)this).Name = "UserDeviceFAN";
		((Control)this).Size = new Size(916, 550);
		((Control)this).ResumeLayout(false);
	}
}
