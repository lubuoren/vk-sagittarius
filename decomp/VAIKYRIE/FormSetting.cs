using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Win32;
using VAIKYRIE.Properties;

namespace VAIKYRIE;

public class FormSetting : Form
{
	private IContainer components = null;

	public Label label1;

	private Button buttonOnOff;

	public Label label2;

	public FormSetting()
	{
		InitializeComponent();
		((ButtonBase)buttonOnOff).FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
		string executablePath = Application.ExecutablePath;
		RegistryKey localMachine = Registry.LocalMachine;
		RegistryKey registryKey = localMachine.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
		string text = (string)registryKey.GetValue("VAIKYRIE");
		if (text == null)
		{
			registryKey.SetValue("VAIKYRIE", executablePath);
			((Control)buttonOnOff).BackgroundImage = (Image)(object)Resources.settingOn;
		}
		else if (!text.Contains("VAIKYRIE"))
		{
			((Control)buttonOnOff).BackgroundImage = (Image)(object)Resources.settingOff;
		}
		else
		{
			((Control)buttonOnOff).BackgroundImage = (Image)(object)Resources.settingOn;
		}
		registryKey.Close();
		localMachine.Close();
	}

	private void buttonOnOff_Click(object sender, EventArgs e)
	{
		string executablePath = Application.ExecutablePath;
		RegistryKey localMachine = Registry.LocalMachine;
		RegistryKey registryKey = localMachine.CreateSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run");
		string text = (string)registryKey.GetValue("VAIKYRIE");
		if (text == null || !text.Contains("VAIKYRIE"))
		{
			registryKey.SetValue("VAIKYRIE", executablePath);
			((Control)buttonOnOff).BackgroundImage = (Image)(object)Resources.settingOn;
		}
		else
		{
			registryKey.SetValue("VAIKYRIE", "");
			((Control)buttonOnOff).BackgroundImage = (Image)(object)Resources.settingOff;
		}
		registryKey.Close();
		localMachine.Close();
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
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Expected O, but got Unknown
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Expected O, but got Unknown
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Expected O, but got Unknown
		label1 = new Label();
		buttonOnOff = new Button();
		label2 = new Label();
		((Control)this).SuspendLayout();
		((Control)label1).BackColor = Color.Transparent;
		label1.FlatStyle = (FlatStyle)0;
		((Control)label1).Font = new Font("微软雅黑", 14.25f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label1).ForeColor = Color.FromArgb(141, 149, 163);
		((Control)label1).Location = new Point(144, 300);
		((Control)label1).Margin = new Padding(0);
		((Control)label1).Name = "label1";
		((Control)label1).Size = new Size(103, 38);
		((Control)label1).TabIndex = 50;
		((Control)label1).Text = "开机启动";
		label1.TextAlign = (ContentAlignment)64;
		((Control)buttonOnOff).BackColor = Color.Transparent;
		((Control)buttonOnOff).BackgroundImage = (Image)(object)Resources.settingOn;
		((Control)buttonOnOff).BackgroundImageLayout = (ImageLayout)4;
		((ButtonBase)buttonOnOff).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonOnOff).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonOnOff).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonOnOff).FlatStyle = (FlatStyle)0;
		((Control)buttonOnOff).Location = new Point(258, 309);
		((Control)buttonOnOff).Margin = new Padding(0);
		((Control)buttonOnOff).Name = "buttonOnOff";
		((Control)buttonOnOff).Size = new Size(43, 20);
		((Control)buttonOnOff).TabIndex = 116;
		((ButtonBase)buttonOnOff).UseVisualStyleBackColor = false;
		((Control)buttonOnOff).Click += buttonOnOff_Click;
		((Control)label2).BackColor = Color.Transparent;
		label2.FlatStyle = (FlatStyle)0;
		((Control)label2).Font = new Font("微软雅黑", 14.25f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label2).ForeColor = Color.FromArgb(141, 149, 163);
		((Control)label2).Location = new Point(159, 351);
		((Control)label2).Margin = new Padding(0);
		((Control)label2).Name = "label2";
		((Control)label2).Size = new Size(254, 38);
		((Control)label2).TabIndex = 117;
		((Control)label2).Text = "软件版本：1.16";
		label2.TextAlign = (ContentAlignment)16;
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)3;
		((Control)this).BackColor = Color.White;
		((Control)this).BackgroundImage = (Image)(object)Resources.LabelSet;
		((Form)this).ClientSize = new Size(916, 800);
		((Control)this).Controls.Add((Control)(object)label2);
		((Control)this).Controls.Add((Control)(object)buttonOnOff);
		((Control)this).Controls.Add((Control)(object)label1);
		((Control)this).DoubleBuffered = true;
		((Control)this).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Form)this).FormBorderStyle = (FormBorderStyle)0;
		((Form)this).Location = new Point(89, 0);
		((Form)this).MaximizeBox = false;
		((Form)this).MinimizeBox = false;
		((Control)this).Name = "FormSetting";
		((Control)this).Text = "FormSetting";
		((Control)this).ResumeLayout(false);
	}
}
