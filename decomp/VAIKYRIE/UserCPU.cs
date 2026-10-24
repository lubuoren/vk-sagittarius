using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VAIKYRIE.Properties;

namespace VAIKYRIE;

public class UserCPU : UserControl
{
	public delegate void delegateUserCPU(int cmd, int sub, object obj);

	public delegateUserCPU delegateUC;

	private ArrayList labelArray;

	private bool fast = false;

	private IContainer components = null;

	private Button buttonX;

	public Panel panel1;

	private Button button1;

	public UserCPU()
	{
		InitializeComponent();
		((ButtonBase)buttonX).FlatAppearance.BorderColor = Color.FromArgb(0, 0, 0, 0);
		labelArray = new ArrayList();
	}

	public void setMyLabelShow(int n)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Expected O, but got Unknown
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Expected O, but got Unknown
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		if (!fast)
		{
			fast = true;
			for (int i = 0; i < n; i++)
			{
				Label val = new Label();
				((Control)val).BackColor = Color.Transparent;
				val.FlatStyle = (FlatStyle)0;
				((Control)val).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
				((Control)val).ForeColor = Color.DarkGray;
				((Control)val).Location = new Point(14, 15 + i * 49);
				((Control)val).Margin = new Padding(0);
				((Control)val).Name = "label" + i;
				((Control)val).Size = new Size(187, 35);
				((Control)val).TabIndex = i + 1000;
				((Control)val).Text = "Core#" + i + "      36℃";
				val.TextAlign = (ContentAlignment)16;
				((Control)val).Visible = true;
				((Control)panel1).Controls.Add((Control)(object)val);
				labelArray.Add(val);
			}
		}
	}

	public void setMyLabelVal(float[] val)
	{
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Expected O, but got Unknown
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		int num = 0;
		if (labelArray.Count > val.Length)
		{
			for (int i = 0; i < labelArray.Count; i++)
			{
				Label val2 = (Label)labelArray[i];
				((Control)val2).Text = "Core#" + i + "      " + val[num] + "℃";
				if (val[num] <= 40f)
				{
					((Control)val2).ForeColor = Color.Green;
				}
				else if (val[num] <= 80f)
				{
					((Control)val2).ForeColor = Color.Yellow;
				}
				else
				{
					((Control)val2).ForeColor = Color.Red;
				}
			}
			return;
		}
		for (int j = val.Length - labelArray.Count; j < val.Length; j++)
		{
			Label val3 = (Label)labelArray[num];
			((Control)val3).Text = "Core#" + num + "      " + val[j] + "℃";
			if (val[j] <= 40f)
			{
				((Control)val3).ForeColor = Color.Green;
			}
			else if (val[j] <= 80f)
			{
				((Control)val3).ForeColor = Color.Yellow;
			}
			else
			{
				((Control)val3).ForeColor = Color.Red;
			}
			num++;
		}
	}

	private void buttonX_Click(object sender, EventArgs e)
	{
		delegateUC?.Invoke(-1, 0, null);
	}

	private void button1_Click(object sender, EventArgs e)
	{
		delegateUC?.Invoke(-2, 0, null);
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
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Expected O, but got Unknown
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		buttonX = new Button();
		panel1 = new Panel();
		button1 = new Button();
		((Control)this).SuspendLayout();
		((Control)buttonX).BackColor = Color.Transparent;
		((Control)buttonX).BackgroundImage = (Image)(object)Resources.X;
		((Control)buttonX).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonX).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonX).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonX).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonX).FlatStyle = (FlatStyle)0;
		((Control)buttonX).Location = new Point(233, 4);
		((Control)buttonX).Margin = new Padding(0);
		((Control)buttonX).Name = "buttonX";
		((Control)buttonX).Size = new Size(24, 16);
		((Control)buttonX).TabIndex = 31;
		((ButtonBase)buttonX).UseVisualStyleBackColor = false;
		((Control)buttonX).Click += buttonX_Click;
		((ScrollableControl)panel1).AutoScroll = true;
		((Control)panel1).BackColor = Color.FromArgb(63, 68, 80);
		((Control)panel1).BackgroundImageLayout = (ImageLayout)0;
		((Control)panel1).ForeColor = Color.Transparent;
		((Control)panel1).Location = new Point(12, 63);
		((Control)panel1).Margin = new Padding(0);
		((Control)panel1).Name = "panel1";
		((Control)panel1).Size = new Size(221, 512);
		((Control)panel1).TabIndex = 88;
		((Control)button1).BackColor = Color.Transparent;
		((Control)button1).BackgroundImage = (Image)(object)Resources.zxh;
		((Control)button1).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)button1).FlatAppearance.BorderSize = 0;
		((ButtonBase)button1).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)button1).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)button1).FlatStyle = (FlatStyle)0;
		((Control)button1).Location = new Point(209, 4);
		((Control)button1).Margin = new Padding(0);
		((Control)button1).Name = "button1";
		((Control)button1).Size = new Size(24, 16);
		((Control)button1).TabIndex = 89;
		((ButtonBase)button1).UseVisualStyleBackColor = false;
		((Control)button1).Click += button1_Click;
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)3;
		((Control)this).BackColor = Color.Transparent;
		((Control)this).BackgroundImage = (Image)(object)Resources.D2_3;
		((Control)this).BackgroundImageLayout = (ImageLayout)3;
		((Control)this).Controls.Add((Control)(object)button1);
		((Control)this).Controls.Add((Control)(object)panel1);
		((Control)this).Controls.Add((Control)(object)buttonX);
		((Control)this).DoubleBuffered = true;
		((Control)this).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)this).Margin = new Padding(0);
		((Control)this).Name = "UserCPU";
		((Control)this).Size = new Size(275, 800);
		((Control)this).ResumeLayout(false);
	}
}
