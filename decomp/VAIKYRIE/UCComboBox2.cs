using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VAIKYRIE.Properties;

namespace VAIKYRIE;

public class UCComboBox2 : UserControl
{
	public delegate void delegateUCComboBox2(int mode1, int mode2);

	private const int Height1 = 29;

	private const int Height2 = 146;

	private int imageX = 0;

	private int imageY = 31;

	private readonly Image pointImg = (Image)(object)Resources.CSYDCom2;

	public int myMode1 = 0;

	public int myMode2 = 1;

	public readonly string[,] myName = new string[4, 4]
	{
		{ "主板温度", "CPU温度", "显卡温度", "内存温度" },
		{ "CPU风扇速度", "显卡风扇速度", "", "" },
		{ "CPU频率", "显卡频率", "内存频率", "" },
		{ "CPU电压", "", "", "" }
	};

	public static readonly string[,] MyName = new string[4, 4]
	{
		{ "Main TEMP", "CPU TEMP", "GPU TEMP", "Mem TEMP" },
		{ "CPU FAN", "GPU FAN", "", "" },
		{ "CPU FREQ", "GPU FREQ", "Mem FREQ", "" },
		{ "CPU VOLT", "", "", "" }
	};

	public delegateUCComboBox2 ucComboBox;

	private IContainer components = null;

	private Button button2;

	private Button button1;

	private Button button11;

	private Button button12;

	private Button button13;

	private Button button14;

	private Button button24;

	private Button button23;

	private Button button22;

	private Button button21;

	public UCComboBox2()
	{
		InitializeComponent();
		((ButtonBase)button1).FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
		((ButtonBase)button2).FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
		((Control)button11).BackgroundImage = (Image)(object)Resources.CSYDCom3;
	}

	public void UCComboBoxCollect()
	{
		((Control)this).Height = 29;
	}

	protected override void OnPaint(PaintEventArgs pe)
	{
		((Control)this).OnPaint(pe);
		Graphics graphics = pe.Graphics;
		if (((Control)this).Height == 146)
		{
			Rectangle rectangle = new Rectangle(imageX, imageY, ((Control)this).Width / 2, ((Control)this).Height - imageY);
			graphics.DrawImage(pointImg, rectangle);
			rectangle.X = ((Control)this).Width / 2;
			graphics.DrawImage(pointImg, rectangle);
		}
	}

	private void SetUCComboBox2(int m, int val)
	{
		myMode2 = val;
		((Control)button2).Text = myName[m, val];
		((Control)button21).Text = myName[m, 0];
		((Control)button22).Text = myName[m, 1];
		((Control)button23).Text = myName[m, 2];
		((Control)button24).Text = myName[m, 3];
		if (((Control)button22).Text.Length == 0)
		{
			((Control)button22).Hide();
		}
		else
		{
			((Control)button22).Show();
		}
		if (((Control)button23).Text.Length == 0)
		{
			((Control)button23).Hide();
		}
		else
		{
			((Control)button23).Show();
		}
		if (((Control)button24).Text.Length == 0)
		{
			((Control)button24).Hide();
		}
		else
		{
			((Control)button24).Show();
		}
	}

	public void SetUCComboBoxMode(int m, int val)
	{
		SetUCComboBox2(m, val);
		myMode1 = m;
		clearButtonBackgroundImage();
		switch (m)
		{
		case 0:
			((Control)button1).Text = ((Control)button11).Text;
			((Control)button11).BackgroundImage = (Image)(object)Resources.CSYDCom3;
			break;
		case 1:
			((Control)button1).Text = ((Control)button12).Text;
			((Control)button12).BackgroundImage = (Image)(object)Resources.CSYDCom3;
			break;
		case 2:
			((Control)button1).Text = ((Control)button13).Text;
			((Control)button13).BackgroundImage = (Image)(object)Resources.CSYDCom3;
			break;
		case 3:
			((Control)button1).Text = ((Control)button14).Text;
			((Control)button14).BackgroundImage = (Image)(object)Resources.CSYDCom3;
			break;
		}
	}

	private void clearButtonBackgroundImage()
	{
		if (((Control)button11).BackgroundImage != null)
		{
			((Control)button11).BackgroundImage.Dispose();
			((Control)button11).BackgroundImage = null;
		}
		if (((Control)button12).BackgroundImage != null)
		{
			((Control)button12).BackgroundImage.Dispose();
			((Control)button12).BackgroundImage = null;
		}
		if (((Control)button13).BackgroundImage != null)
		{
			((Control)button13).BackgroundImage.Dispose();
			((Control)button13).BackgroundImage = null;
		}
		if (((Control)button14).BackgroundImage != null)
		{
			((Control)button14).BackgroundImage.Dispose();
			((Control)button14).BackgroundImage = null;
		}
	}

	private void button1_Click(object sender, EventArgs e)
	{
		if (((Control)this).Height == 29)
		{
			((Control)this).Height = 146;
		}
		else
		{
			((Control)this).Height = 29;
		}
		ucComboBox?.Invoke(-1, 0);
	}

	private void button11_Click(object sender, EventArgs e)
	{
		SetUCComboBoxMode(0, 0);
	}

	private void button12_Click(object sender, EventArgs e)
	{
		SetUCComboBoxMode(1, 0);
	}

	private void button13_Click(object sender, EventArgs e)
	{
		SetUCComboBoxMode(2, 0);
	}

	private void button14_Click(object sender, EventArgs e)
	{
		SetUCComboBoxMode(3, 0);
	}

	private void button21_Click(object sender, EventArgs e)
	{
		myMode2 = 0;
		((Control)button2).Text = ((Control)button21).Text;
		UCComboBoxCollect();
		ucComboBox?.Invoke(myMode1, myMode2);
	}

	private void button22_Click(object sender, EventArgs e)
	{
		myMode2 = 1;
		((Control)button2).Text = ((Control)button22).Text;
		UCComboBoxCollect();
		ucComboBox?.Invoke(myMode1, myMode2);
	}

	private void button23_Click(object sender, EventArgs e)
	{
		myMode2 = 2;
		((Control)button2).Text = ((Control)button23).Text;
		UCComboBoxCollect();
		ucComboBox?.Invoke(myMode1, myMode2);
	}

	private void button24_Click(object sender, EventArgs e)
	{
		myMode2 = 3;
		((Control)button2).Text = ((Control)button24).Text;
		UCComboBoxCollect();
		ucComboBox?.Invoke(myMode1, myMode2);
	}

	private void button2_MouseEnter(object sender, EventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Expected O, but got Unknown
		Button val = (Button)sender;
		((Control)val).BackgroundImage = (Image)(object)Resources.CSYDCom4;
	}

	private void button2_MouseLeave(object sender, EventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Expected O, but got Unknown
		Button val = (Button)sender;
		((Control)val).BackgroundImage.Dispose();
		((Control)val).BackgroundImage = null;
	}

	private void button1_MouseEnter(object sender, EventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Expected O, but got Unknown
		Button val = (Button)sender;
		if (!((Control)val).Text.Equals(((Control)button1).Text))
		{
			((Control)val).BackgroundImage = (Image)(object)Resources.CSYDCom4;
		}
	}

	private void button1_MouseLeave(object sender, EventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Expected O, but got Unknown
		Button val = (Button)sender;
		if (!((Control)val).Text.Equals(((Control)button1).Text) && ((Control)val).BackgroundImage != null)
		{
			((Control)val).BackgroundImage.Dispose();
			((Control)val).BackgroundImage = null;
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
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Expected O, but got Unknown
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Expected O, but got Unknown
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Expected O, but got Unknown
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_053d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Expected O, but got Unknown
		//IL_0574: Unknown result type (might be due to invalid IL or missing references)
		//IL_06aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b4: Expected O, but got Unknown
		//IL_06e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_081b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0825: Expected O, but got Unknown
		//IL_0856: Unknown result type (might be due to invalid IL or missing references)
		//IL_098c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0996: Expected O, but got Unknown
		//IL_09c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b07: Expected O, but got Unknown
		//IL_0b38: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c89: Expected O, but got Unknown
		//IL_0cb5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dcc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dd6: Expected O, but got Unknown
		//IL_0e06: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f5e: Unknown result type (might be due to invalid IL or missing references)
		button11 = new Button();
		button12 = new Button();
		button13 = new Button();
		button14 = new Button();
		button24 = new Button();
		button23 = new Button();
		button22 = new Button();
		button21 = new Button();
		button1 = new Button();
		button2 = new Button();
		((Control)this).SuspendLayout();
		((Control)button11).BackColor = Color.Transparent;
		((Control)button11).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button11).FlatAppearance.BorderSize = 0;
		((ButtonBase)button11).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button11).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button11).FlatStyle = (FlatStyle)0;
		((Control)button11).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)button11).ForeColor = Color.Silver;
		((Control)button11).Location = new Point(0, 29);
		((Control)button11).Margin = new Padding(0);
		((Control)button11).Name = "button11";
		((Control)button11).Size = new Size(181, 29);
		((Control)button11).TabIndex = 82;
		((Control)button11).Text = "温度";
		((ButtonBase)button11).TextAlign = (ContentAlignment)16;
		((ButtonBase)button11).UseVisualStyleBackColor = false;
		((Control)button11).Click += button11_Click;
		((Control)button11).MouseEnter += button1_MouseEnter;
		((Control)button11).MouseLeave += button1_MouseLeave;
		((Control)button12).BackColor = Color.Transparent;
		((Control)button12).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button12).FlatAppearance.BorderSize = 0;
		((ButtonBase)button12).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button12).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button12).FlatStyle = (FlatStyle)0;
		((Control)button12).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)button12).ForeColor = Color.Silver;
		((Control)button12).Location = new Point(0, 58);
		((Control)button12).Margin = new Padding(0);
		((Control)button12).Name = "button12";
		((Control)button12).Size = new Size(181, 29);
		((Control)button12).TabIndex = 83;
		((Control)button12).Text = "风扇速度";
		((ButtonBase)button12).TextAlign = (ContentAlignment)16;
		((ButtonBase)button12).UseVisualStyleBackColor = false;
		((Control)button12).Click += button12_Click;
		((Control)button12).MouseEnter += button1_MouseEnter;
		((Control)button12).MouseLeave += button1_MouseLeave;
		((Control)button13).BackColor = Color.Transparent;
		((Control)button13).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button13).FlatAppearance.BorderSize = 0;
		((ButtonBase)button13).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button13).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button13).FlatStyle = (FlatStyle)0;
		((Control)button13).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)button13).ForeColor = Color.Silver;
		((Control)button13).Location = new Point(0, 87);
		((Control)button13).Margin = new Padding(0);
		((Control)button13).Name = "button13";
		((Control)button13).Size = new Size(181, 29);
		((Control)button13).TabIndex = 84;
		((Control)button13).Text = "频率";
		((ButtonBase)button13).TextAlign = (ContentAlignment)16;
		((ButtonBase)button13).UseVisualStyleBackColor = false;
		((Control)button13).Click += button13_Click;
		((Control)button13).MouseEnter += button1_MouseEnter;
		((Control)button13).MouseLeave += button1_MouseLeave;
		((Control)button14).BackColor = Color.Transparent;
		((Control)button14).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button14).FlatAppearance.BorderSize = 0;
		((ButtonBase)button14).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button14).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button14).FlatStyle = (FlatStyle)0;
		((Control)button14).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)button14).ForeColor = Color.Silver;
		((Control)button14).Location = new Point(0, 116);
		((Control)button14).Margin = new Padding(0);
		((Control)button14).Name = "button14";
		((Control)button14).Size = new Size(181, 29);
		((Control)button14).TabIndex = 85;
		((Control)button14).Text = "电压";
		((ButtonBase)button14).TextAlign = (ContentAlignment)16;
		((ButtonBase)button14).UseVisualStyleBackColor = false;
		((Control)button14).Click += button14_Click;
		((Control)button14).MouseEnter += button1_MouseEnter;
		((Control)button14).MouseLeave += button1_MouseLeave;
		((Control)button24).BackColor = Color.Transparent;
		((Control)button24).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button24).FlatAppearance.BorderSize = 0;
		((ButtonBase)button24).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button24).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button24).FlatStyle = (FlatStyle)0;
		((Control)button24).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)button24).ForeColor = Color.Silver;
		((Control)button24).Location = new Point(181, 116);
		((Control)button24).Margin = new Padding(0);
		((Control)button24).Name = "button24";
		((Control)button24).Size = new Size(181, 29);
		((Control)button24).TabIndex = 89;
		((Control)button24).Text = "内存温度";
		((ButtonBase)button24).TextAlign = (ContentAlignment)16;
		((ButtonBase)button24).UseVisualStyleBackColor = false;
		((Control)button24).Click += button24_Click;
		((Control)button24).MouseEnter += button2_MouseEnter;
		((Control)button24).MouseLeave += button2_MouseLeave;
		((Control)button23).BackColor = Color.Transparent;
		((Control)button23).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button23).FlatAppearance.BorderSize = 0;
		((ButtonBase)button23).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button23).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button23).FlatStyle = (FlatStyle)0;
		((Control)button23).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)button23).ForeColor = Color.Silver;
		((Control)button23).Location = new Point(181, 87);
		((Control)button23).Margin = new Padding(0);
		((Control)button23).Name = "button23";
		((Control)button23).Size = new Size(181, 29);
		((Control)button23).TabIndex = 88;
		((Control)button23).Text = "显卡温度";
		((ButtonBase)button23).TextAlign = (ContentAlignment)16;
		((ButtonBase)button23).UseVisualStyleBackColor = false;
		((Control)button23).Click += button23_Click;
		((Control)button23).MouseEnter += button2_MouseEnter;
		((Control)button23).MouseLeave += button2_MouseLeave;
		((Control)button22).BackColor = Color.Transparent;
		((Control)button22).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button22).FlatAppearance.BorderSize = 0;
		((ButtonBase)button22).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button22).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button22).FlatStyle = (FlatStyle)0;
		((Control)button22).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)button22).ForeColor = Color.Silver;
		((Control)button22).Location = new Point(181, 58);
		((Control)button22).Margin = new Padding(0);
		((Control)button22).Name = "button22";
		((Control)button22).Size = new Size(181, 29);
		((Control)button22).TabIndex = 87;
		((Control)button22).Text = "CPU温度";
		((ButtonBase)button22).TextAlign = (ContentAlignment)16;
		((ButtonBase)button22).UseVisualStyleBackColor = false;
		((Control)button22).Click += button22_Click;
		((Control)button22).MouseEnter += button2_MouseEnter;
		((Control)button22).MouseLeave += button2_MouseLeave;
		((Control)button21).BackColor = Color.Transparent;
		((Control)button21).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button21).FlatAppearance.BorderSize = 0;
		((ButtonBase)button21).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button21).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button21).FlatStyle = (FlatStyle)0;
		((Control)button21).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)button21).ForeColor = Color.Silver;
		((Control)button21).Location = new Point(181, 29);
		((Control)button21).Margin = new Padding(0);
		((Control)button21).Name = "button21";
		((Control)button21).Size = new Size(181, 29);
		((Control)button21).TabIndex = 86;
		((Control)button21).Text = "主板温度";
		((ButtonBase)button21).TextAlign = (ContentAlignment)16;
		((ButtonBase)button21).UseVisualStyleBackColor = false;
		((Control)button21).Click += button21_Click;
		((Control)button21).MouseEnter += button2_MouseEnter;
		((Control)button21).MouseLeave += button2_MouseLeave;
		((Control)button1).BackColor = Color.Transparent;
		((Control)button1).BackgroundImage = (Image)(object)Resources.BLLCD2;
		((Control)button1).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button1).FlatAppearance.BorderSize = 0;
		((ButtonBase)button1).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button1).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button1).FlatStyle = (FlatStyle)0;
		((Control)button1).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)button1).ForeColor = Color.Silver;
		((Control)button1).Location = new Point(0, 0);
		((Control)button1).Margin = new Padding(0);
		((Control)button1).Name = "button1";
		((Control)button1).Size = new Size(181, 29);
		((Control)button1).TabIndex = 81;
		((Control)button1).Text = "温度";
		((ButtonBase)button1).TextAlign = (ContentAlignment)16;
		((ButtonBase)button1).UseVisualStyleBackColor = false;
		((Control)button1).Click += button1_Click;
		((Control)button2).BackColor = Color.Transparent;
		((Control)button2).BackgroundImage = (Image)(object)Resources.BRLCD2;
		((Control)button2).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button2).FlatAppearance.BorderSize = 0;
		((ButtonBase)button2).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button2).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button2).FlatStyle = (FlatStyle)0;
		((Control)button2).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)button2).ForeColor = Color.Silver;
		((Control)button2).Location = new Point(181, 0);
		((Control)button2).Margin = new Padding(0);
		((Control)button2).Name = "button2";
		((Control)button2).Size = new Size(181, 29);
		((Control)button2).TabIndex = 80;
		((Control)button2).Text = "CPU温度";
		((ButtonBase)button2).TextAlign = (ContentAlignment)16;
		((ButtonBase)button2).UseVisualStyleBackColor = false;
		((Control)button2).Click += button1_Click;
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)3;
		((Control)this).BackColor = Color.Transparent;
		((Control)this).Controls.Add((Control)(object)button24);
		((Control)this).Controls.Add((Control)(object)button23);
		((Control)this).Controls.Add((Control)(object)button22);
		((Control)this).Controls.Add((Control)(object)button21);
		((Control)this).Controls.Add((Control)(object)button14);
		((Control)this).Controls.Add((Control)(object)button13);
		((Control)this).Controls.Add((Control)(object)button12);
		((Control)this).Controls.Add((Control)(object)button11);
		((Control)this).Controls.Add((Control)(object)button1);
		((Control)this).Controls.Add((Control)(object)button2);
		((Control)this).DoubleBuffered = true;
		((Control)this).Margin = new Padding(0);
		((Control)this).Name = "UCComboBox2";
		((Control)this).Size = new Size(362, 29);
		((Control)this).ResumeLayout(false);
	}
}
