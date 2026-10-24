using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VAIKYRIE.Properties;

namespace VAIKYRIE;

public class UCComboBox1 : UserControl
{
	public delegate void delegateUCComboBox1(int mode);

	private int imageX = 0;

	private int imageY = 28;

	private readonly Image pointImg = (Image)(object)Resources.CSYDCom2;

	private readonly Color color1 = Color.FromArgb(105, 90, 71);

	private readonly Color color2 = Color.Silver;

	public int myMode = 1;

	private readonly string[,] myName = new string[2, 4]
	{
		{ "CPU温度", "显卡温度", "CPU负载", "内存使用" },
		{ "向上单向", "向下单向", "中间散开", "向中聚拢" }
	};

	public delegateUCComboBox1 ucComboBox1;

	private IContainer components = null;

	private Button button1;

	private Button button2;

	private Button button3;

	private Button button4;

	private Button button5;

	public UCComboBox1()
	{
		InitializeComponent();
		((ButtonBase)button1).FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
	}

	public void SetUCComboBox(int val)
	{
		Button obj = button1;
		string text = (((Control)button2).Text = myName[val, 0]);
		((Control)obj).Text = text;
		((Control)button3).Text = myName[val, 1];
		((Control)button4).Text = myName[val, 2];
		((Control)button5).Text = myName[val, 3];
	}

	public void SetUCComboBoxMode(int m)
	{
		switch (m)
		{
		case 1:
			myMode = 1;
			((Control)this).Height = 26;
			((Control)button1).Text = ((Control)button2).Text;
			break;
		case 2:
			myMode = 2;
			((Control)this).Height = 26;
			((Control)button1).Text = ((Control)button3).Text;
			break;
		case 3:
			myMode = 3;
			((Control)this).Height = 26;
			((Control)button1).Text = ((Control)button4).Text;
			break;
		case 4:
			myMode = 4;
			((Control)this).Height = 26;
			((Control)button1).Text = ((Control)button5).Text;
			break;
		}
	}

	protected override void OnPaint(PaintEventArgs pe)
	{
		((Control)this).OnPaint(pe);
		Graphics graphics = pe.Graphics;
		Rectangle rectangle = new Rectangle(imageX, imageY, pointImg.Width, pointImg.Height);
		graphics.DrawImage(pointImg, rectangle);
	}

	private void button1_Click(object sender, EventArgs e)
	{
		((Control)this).Height = 130;
	}

	private void button2_MouseEnter(object sender, EventArgs e)
	{
		((Control)button2).BackgroundImage = (Image)(object)Resources.CSYDCom3;
		((Control)button2).ForeColor = color1;
	}

	private void button2_MouseLeave(object sender, EventArgs e)
	{
		((Control)button2).BackgroundImage.Dispose();
		((Control)button2).BackgroundImage = null;
		((Control)button2).ForeColor = color2;
	}

	private void button3_MouseEnter(object sender, EventArgs e)
	{
		((Control)button3).BackgroundImage = (Image)(object)Resources.CSYDCom3;
		((Control)button3).ForeColor = color1;
	}

	private void button3_MouseLeave(object sender, EventArgs e)
	{
		((Control)button3).BackgroundImage.Dispose();
		((Control)button3).BackgroundImage = null;
		((Control)button3).ForeColor = color2;
	}

	private void button4_MouseEnter(object sender, EventArgs e)
	{
		((Control)button4).BackgroundImage = (Image)(object)Resources.CSYDCom3;
		((Control)button4).ForeColor = color1;
	}

	private void button4_MouseLeave(object sender, EventArgs e)
	{
		((Control)button4).BackgroundImage.Dispose();
		((Control)button4).BackgroundImage = null;
		((Control)button4).ForeColor = color2;
	}

	private void button5_MouseEnter(object sender, EventArgs e)
	{
		((Control)button5).BackgroundImage = (Image)(object)Resources.CSYDCom3;
		((Control)button5).ForeColor = color1;
	}

	private void button5_MouseLeave(object sender, EventArgs e)
	{
		((Control)button5).BackgroundImage.Dispose();
		((Control)button5).BackgroundImage = null;
		((Control)button5).ForeColor = color2;
	}

	private void button2_Click(object sender, EventArgs e)
	{
		myMode = 1;
		((Control)this).Height = 26;
		((Control)button1).Text = ((Control)button2).Text;
		ucComboBox1?.Invoke(myMode);
	}

	private void button3_Click(object sender, EventArgs e)
	{
		myMode = 2;
		((Control)this).Height = 26;
		((Control)button1).Text = ((Control)button3).Text;
		ucComboBox1?.Invoke(myMode);
	}

	private void button4_Click(object sender, EventArgs e)
	{
		myMode = 3;
		((Control)this).Height = 26;
		((Control)button1).Text = ((Control)button4).Text;
		ucComboBox1?.Invoke(myMode);
	}

	private void button5_Click(object sender, EventArgs e)
	{
		myMode = 4;
		((Control)this).Height = 26;
		((Control)button1).Text = ((Control)button5).Text;
		ucComboBox1?.Invoke(myMode);
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
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Expected O, but got Unknown
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Expected O, but got Unknown
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Expected O, but got Unknown
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_0510: Expected O, but got Unknown
		//IL_053d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0684: Unknown result type (might be due to invalid IL or missing references)
		//IL_068e: Expected O, but got Unknown
		//IL_06ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d2: Expected O, but got Unknown
		//IL_07d5: Unknown result type (might be due to invalid IL or missing references)
		button3 = new Button();
		button4 = new Button();
		button5 = new Button();
		button2 = new Button();
		button1 = new Button();
		((Control)this).SuspendLayout();
		((Control)button3).BackColor = Color.Transparent;
		((Control)button3).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button3).FlatAppearance.BorderSize = 0;
		((ButtonBase)button3).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button3).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button3).FlatStyle = (FlatStyle)0;
		((Control)button3).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)button3).ForeColor = Color.Silver;
		((Control)button3).Location = new Point(0, 53);
		((Control)button3).Margin = new Padding(0);
		((Control)button3).Name = "button3";
		((Control)button3).Size = new Size(201, 25);
		((Control)button3).TabIndex = 81;
		((Control)button3).Text = "显卡温度";
		((ButtonBase)button3).TextAlign = (ContentAlignment)16;
		((ButtonBase)button3).UseVisualStyleBackColor = false;
		((Control)button3).Click += button3_Click;
		((Control)button3).MouseEnter += button3_MouseEnter;
		((Control)button3).MouseLeave += button3_MouseLeave;
		((Control)button4).BackColor = Color.Transparent;
		((Control)button4).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button4).FlatAppearance.BorderSize = 0;
		((ButtonBase)button4).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button4).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button4).FlatStyle = (FlatStyle)0;
		((Control)button4).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)button4).ForeColor = Color.Silver;
		((Control)button4).Location = new Point(0, 78);
		((Control)button4).Margin = new Padding(0);
		((Control)button4).Name = "button4";
		((Control)button4).Size = new Size(201, 25);
		((Control)button4).TabIndex = 82;
		((Control)button4).Text = "CPU负载";
		((ButtonBase)button4).TextAlign = (ContentAlignment)16;
		((ButtonBase)button4).UseVisualStyleBackColor = false;
		((Control)button4).Click += button4_Click;
		((Control)button4).MouseEnter += button4_MouseEnter;
		((Control)button4).MouseLeave += button4_MouseLeave;
		((Control)button5).BackColor = Color.Transparent;
		((Control)button5).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button5).FlatAppearance.BorderSize = 0;
		((ButtonBase)button5).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button5).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button5).FlatStyle = (FlatStyle)0;
		((Control)button5).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)button5).ForeColor = Color.Silver;
		((Control)button5).Location = new Point(0, 103);
		((Control)button5).Margin = new Padding(0);
		((Control)button5).Name = "button5";
		((Control)button5).Size = new Size(201, 25);
		((Control)button5).TabIndex = 83;
		((Control)button5).Text = "内存使用";
		((ButtonBase)button5).TextAlign = (ContentAlignment)16;
		((ButtonBase)button5).UseVisualStyleBackColor = false;
		((Control)button5).Click += button5_Click;
		((Control)button5).MouseEnter += button5_MouseEnter;
		((Control)button5).MouseLeave += button5_MouseLeave;
		((Control)button2).BackColor = Color.Transparent;
		((Control)button2).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button2).FlatAppearance.BorderSize = 0;
		((ButtonBase)button2).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button2).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button2).FlatStyle = (FlatStyle)0;
		((Control)button2).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)button2).ForeColor = Color.Silver;
		((Control)button2).Location = new Point(0, 28);
		((Control)button2).Margin = new Padding(0);
		((Control)button2).Name = "button2";
		((Control)button2).Size = new Size(201, 25);
		((Control)button2).TabIndex = 80;
		((Control)button2).Text = "CPU温度";
		((ButtonBase)button2).TextAlign = (ContentAlignment)16;
		((ButtonBase)button2).UseVisualStyleBackColor = false;
		((Control)button2).Click += button2_Click;
		((Control)button2).MouseEnter += button2_MouseEnter;
		((Control)button2).MouseLeave += button2_MouseLeave;
		((Control)button1).BackColor = Color.Transparent;
		((Control)button1).BackgroundImage = (Image)(object)Resources.CSYDCom0;
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
		((Control)button1).Size = new Size(201, 26);
		((Control)button1).TabIndex = 79;
		((Control)button1).Text = "CPU温度";
		((ButtonBase)button1).TextAlign = (ContentAlignment)16;
		((ButtonBase)button1).UseVisualStyleBackColor = false;
		((Control)button1).Click += button1_Click;
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)3;
		((Control)this).BackColor = Color.Transparent;
		((Control)this).Controls.Add((Control)(object)button5);
		((Control)this).Controls.Add((Control)(object)button4);
		((Control)this).Controls.Add((Control)(object)button3);
		((Control)this).Controls.Add((Control)(object)button2);
		((Control)this).Controls.Add((Control)(object)button1);
		((Control)this).DoubleBuffered = true;
		((Control)this).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)this).Margin = new Padding(0);
		((Control)this).Name = "UCComboBox1";
		((Control)this).Size = new Size(201, 26);
		((Control)this).ResumeLayout(false);
	}
}
