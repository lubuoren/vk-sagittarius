using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace VAIKYRIE;

public class UCLCDDisplay2 : UserControl
{
	private IContainer components = null;

	public Label label1;

	public UCLCDDisplay2()
	{
		InitializeComponent();
	}

	public void SetUCLCDDisplay2Val(string str)
	{
		((Control)label1).Text = str;
	}

	private void label1_Click(object sender, EventArgs e)
	{
		((Control)this).OnClick((EventArgs)null);
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
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		label1 = new Label();
		((Control)this).SuspendLayout();
		((Control)label1).BackColor = Color.Transparent;
		label1.FlatStyle = (FlatStyle)0;
		((Control)label1).Font = new Font("Impact", 12f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
		((Control)label1).ForeColor = Color.FromArgb(141, 141, 141);
		((Control)label1).Location = new Point(0, 0);
		((Control)label1).Margin = new Padding(0);
		((Control)label1).Name = "label1";
		((Control)label1).Size = new Size(56, 42);
		((Control)label1).TabIndex = 48;
		((Control)label1).Text = "CPU TEMP";
		label1.TextAlign = (ContentAlignment)32;
		((Control)label1).Click += label1_Click;
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)3;
		((Control)this).BackColor = Color.Transparent;
		((Control)this).Controls.Add((Control)(object)label1);
		((Control)this).DoubleBuffered = true;
		((Control)this).Margin = new Padding(0);
		((Control)this).Name = "UCLCDDisplay2";
		((Control)this).Size = new Size(56, 42);
		((Control)this).ResumeLayout(false);
	}
}
