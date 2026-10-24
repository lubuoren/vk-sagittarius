using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VAIKYRIE.Properties;

namespace VAIKYRIE;

public class UserMain : UserControl
{
	public delegate void delegateUserMain(int cmd);

	public delegateUserMain delegateMain;

	public Form1 form;

	private int myMode = 0;

	private IContainer components = null;

	private Button buttonMain;

	private Button buttonMainS;

	private Button buttonDevice;

	private Button buttonSetting;

	public UserMain()
	{
		InitializeComponent();
	}

	private void Buttom_Click(int mode)
	{
		switch (mode)
		{
		case 0:
			((Control)buttonMain).BackgroundImage = (Image)(object)Resources.BT1H;
			((Control)buttonMainS).BackgroundImage = (Image)(object)Resources.BT2L;
			((Control)buttonDevice).BackgroundImage = (Image)(object)Resources.BT3L;
			((Control)buttonSetting).BackgroundImage = (Image)(object)Resources.BT4L;
			break;
		case 1:
			((Control)buttonMain).BackgroundImage = (Image)(object)Resources.BT1L;
			((Control)buttonMainS).BackgroundImage = (Image)(object)Resources.BT2H;
			((Control)buttonDevice).BackgroundImage = (Image)(object)Resources.BT3L;
			((Control)buttonSetting).BackgroundImage = (Image)(object)Resources.BT4L;
			break;
		case 2:
			((Control)buttonMain).BackgroundImage = (Image)(object)Resources.BT1L;
			((Control)buttonMainS).BackgroundImage = (Image)(object)Resources.BT2L;
			((Control)buttonDevice).BackgroundImage = (Image)(object)Resources.BT3H;
			((Control)buttonSetting).BackgroundImage = (Image)(object)Resources.BT4L;
			break;
		case 3:
			((Control)buttonMain).BackgroundImage = (Image)(object)Resources.BT1L;
			((Control)buttonMainS).BackgroundImage = (Image)(object)Resources.BT2L;
			((Control)buttonDevice).BackgroundImage = (Image)(object)Resources.BT3L;
			((Control)buttonSetting).BackgroundImage = (Image)(object)Resources.BT4H;
			break;
		}
	}

	private void buttonMain_Click(object sender, EventArgs e)
	{
		myMode = 0;
		Buttom_Click(myMode);
		delegateMain?.Invoke(myMode);
	}

	private void buttonMainS_Click(object sender, EventArgs e)
	{
		myMode = 1;
		Buttom_Click(myMode);
		delegateMain?.Invoke(myMode);
	}

	private void buttonDevice_Click(object sender, EventArgs e)
	{
		myMode = 2;
		Buttom_Click(myMode);
		delegateMain?.Invoke(myMode);
	}

	private void buttonSetting_Click(object sender, EventArgs e)
	{
		myMode = 3;
		Buttom_Click(myMode);
		delegateMain?.Invoke(myMode);
	}

	private void UserMain_MouseDown(object sender, MouseEventArgs e)
	{
		form.Form1_MouseDown(form, e);
	}

	private void UserMain_MouseMove(object sender, MouseEventArgs e)
	{
		form.Form1_MouseMove(form, e);
	}

	private void UserMain_MouseUp(object sender, MouseEventArgs e)
	{
		form.Form1_MouseUp(form, e);
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
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Expected O, but got Unknown
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Expected O, but got Unknown
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0507: Expected O, but got Unknown
		//IL_0510: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Expected O, but got Unknown
		buttonMain = new Button();
		buttonMainS = new Button();
		buttonDevice = new Button();
		buttonSetting = new Button();
		((Control)this).SuspendLayout();
		((Control)buttonMain).BackColor = Color.Transparent;
		((Control)buttonMain).BackgroundImage = (Image)(object)Resources.BT1H;
		((Control)buttonMain).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonMain).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonMain).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonMain).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonMain).FlatStyle = (FlatStyle)0;
		((Control)buttonMain).Location = new Point(0, 96);
		((Control)buttonMain).Margin = new Padding(0);
		((Control)buttonMain).Name = "buttonMain";
		((Control)buttonMain).Size = new Size(38, 41);
		((Control)buttonMain).TabIndex = 11;
		((ButtonBase)buttonMain).UseVisualStyleBackColor = false;
		((Control)buttonMain).Click += buttonMain_Click;
		((Control)buttonMainS).BackColor = Color.Transparent;
		((Control)buttonMainS).BackgroundImage = (Image)(object)Resources.BT2L;
		((Control)buttonMainS).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonMainS).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonMainS).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonMainS).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonMainS).FlatStyle = (FlatStyle)0;
		((Control)buttonMainS).Location = new Point(0, 195);
		((Control)buttonMainS).Margin = new Padding(0);
		((Control)buttonMainS).Name = "buttonMainS";
		((Control)buttonMainS).Size = new Size(38, 41);
		((Control)buttonMainS).TabIndex = 12;
		((ButtonBase)buttonMainS).UseVisualStyleBackColor = false;
		((Control)buttonMainS).Click += buttonMainS_Click;
		((Control)buttonDevice).BackColor = Color.Transparent;
		((Control)buttonDevice).BackgroundImage = (Image)(object)Resources.BT3L;
		((Control)buttonDevice).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonDevice).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonDevice).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonDevice).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonDevice).FlatStyle = (FlatStyle)0;
		((Control)buttonDevice).Location = new Point(0, 294);
		((Control)buttonDevice).Margin = new Padding(0);
		((Control)buttonDevice).Name = "buttonDevice";
		((Control)buttonDevice).Size = new Size(38, 41);
		((Control)buttonDevice).TabIndex = 13;
		((ButtonBase)buttonDevice).UseVisualStyleBackColor = false;
		((Control)buttonDevice).Click += buttonDevice_Click;
		((Control)buttonSetting).BackColor = Color.Transparent;
		((Control)buttonSetting).BackgroundImage = (Image)(object)Resources.BT4L;
		((Control)buttonSetting).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonSetting).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonSetting).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonSetting).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonSetting).FlatStyle = (FlatStyle)0;
		((Control)buttonSetting).Location = new Point(0, 393);
		((Control)buttonSetting).Margin = new Padding(0);
		((Control)buttonSetting).Name = "buttonSetting";
		((Control)buttonSetting).Size = new Size(38, 41);
		((Control)buttonSetting).TabIndex = 14;
		((ButtonBase)buttonSetting).UseVisualStyleBackColor = false;
		((Control)buttonSetting).Click += buttonSetting_Click;
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)3;
		((Control)this).BackColor = Color.Transparent;
		((Control)this).BackgroundImage = (Image)(object)Resources.D1_1;
		((Control)this).BackgroundImageLayout = (ImageLayout)3;
		((Control)this).Controls.Add((Control)(object)buttonSetting);
		((Control)this).Controls.Add((Control)(object)buttonDevice);
		((Control)this).Controls.Add((Control)(object)buttonMainS);
		((Control)this).Controls.Add((Control)(object)buttonMain);
		((Control)this).DoubleBuffered = true;
		((Control)this).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)this).Margin = new Padding(0);
		((Control)this).Name = "UserMain";
		((Control)this).Size = new Size(89, 800);
		((Control)this).MouseDown += new MouseEventHandler(UserMain_MouseDown);
		((Control)this).MouseMove += new MouseEventHandler(UserMain_MouseMove);
		((Control)this).MouseUp += new MouseEventHandler(UserMain_MouseUp);
		((Control)this).ResumeLayout(false);
	}
}
