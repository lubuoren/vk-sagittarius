using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AxWMPLib;

namespace VAIKYRIE;

public class UCLCDDisplay1 : UserControl
{
	private IContainer components = null;

	public Label label1;

	public Label label2;

	public Label label3;

	public Label label4;

	public Label label5;

	public Label label6;

	public AxWindowsMediaPlayer axWindowsMediaPlayer1;

	public UCLCDDisplay1()
	{
		InitializeComponent();
		axWindowsMediaPlayer1.settings.setMode("loop", varfMode: true);
		((Control)label1).Location = new Point((((Control)this).Width - ((Control)label1).Width) / 2, ((Control)label1).Location.Y);
		((Control)label2).Location = new Point((((Control)this).Width - ((Control)label2).Width) / 2, ((Control)label2).Location.Y);
		((Control)label3).Location = new Point((((Control)this).Width / 2 - ((Control)label3).Width) / 2, ((Control)label3).Location.Y);
		((Control)label4).Location = new Point((((Control)this).Width / 2 - ((Control)label4).Width) / 2, ((Control)label4).Location.Y);
		((Control)label5).Location = new Point((((Control)this).Width * 3 / 2 - ((Control)label5).Width) / 2, ((Control)label5).Location.Y);
		((Control)label6).Location = new Point((((Control)this).Width * 3 / 2 - ((Control)label6).Width) / 2, ((Control)label6).Location.Y);
	}

	public void SetUCLCDDisplayBackgroundImage(string strName)
	{
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Expected O, but got Unknown
		string text = strName.ToLower();
		if (text.Contains(".mp4") || text.Contains(".mov") || text.Contains(".avi"))
		{
			if (((Control)this).BackgroundImage != null)
			{
				Image backgroundImage = ((Control)this).BackgroundImage;
				((Control)this).BackgroundImage = null;
				backgroundImage.Dispose();
			}
			((Control)axWindowsMediaPlayer1).Show();
			axWindowsMediaPlayer1.URL = strName;
			axWindowsMediaPlayer1.settings.setMode("loop", varfMode: true);
			axWindowsMediaPlayer1.Ctlcontrols.play();
		}
		else
		{
			((Control)axWindowsMediaPlayer1).Hide();
			if (((Control)this).BackgroundImage != null)
			{
				Image backgroundImage2 = ((Control)this).BackgroundImage;
				((Control)this).BackgroundImage = null;
				backgroundImage2.Dispose();
			}
			((Control)this).BackgroundImage = (Image)new Bitmap(strName);
		}
	}

	public void SetUCLCDDisplay1Val(int n, string a1, string a2, string b1, string b2, string c1, string c2)
	{
		switch (n)
		{
		case 0:
			((Control)label1).Text = "";
			((Control)label2).Text = "";
			((Control)label3).Text = "";
			((Control)label4).Text = "";
			((Control)label5).Text = "";
			((Control)label6).Text = "";
			break;
		case 1:
			((Control)label1).Text = a1;
			((Control)label2).Text = a2;
			((Control)label3).Text = "";
			((Control)label4).Text = "";
			((Control)label5).Text = "";
			((Control)label6).Text = "";
			break;
		case 2:
			((Control)label1).Text = "";
			((Control)label2).Text = "";
			((Control)label3).Text = b1;
			((Control)label4).Text = b2;
			((Control)label5).Text = c1;
			((Control)label6).Text = c2;
			break;
		case 3:
			((Control)label1).Text = a1;
			((Control)label2).Text = a2;
			((Control)label3).Text = b1;
			((Control)label4).Text = b2;
			((Control)label5).Text = c1;
			((Control)label6).Text = c2;
			break;
		}
		((Control)label1).Location = new Point((((Control)this).Width - ((Control)label1).Width) / 2, ((Control)label1).Location.Y);
		((Control)label2).Location = new Point((((Control)this).Width - ((Control)label2).Width) / 2, ((Control)label2).Location.Y);
		((Control)label3).Location = new Point((((Control)this).Width / 2 - ((Control)label3).Width) / 2, ((Control)label3).Location.Y);
		((Control)label4).Location = new Point((((Control)this).Width / 2 - ((Control)label4).Width) / 2, ((Control)label4).Location.Y);
		((Control)label5).Location = new Point((((Control)this).Width * 3 / 2 - ((Control)label5).Width) / 2, ((Control)label5).Location.Y);
		((Control)label6).Location = new Point((((Control)this).Width * 3 / 2 - ((Control)label6).Width) / 2, ((Control)label6).Location.Y);
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
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Expected O, but got Unknown
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Expected O, but got Unknown
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Expected O, but got Unknown
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Expected O, but got Unknown
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Expected O, but got Unknown
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_0510: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Expected O, but got Unknown
		//IL_0554: Unknown result type (might be due to invalid IL or missing references)
		//IL_0601: Unknown result type (might be due to invalid IL or missing references)
		//IL_060b: Expected O, but got Unknown
		//IL_06e6: Unknown result type (might be due to invalid IL or missing references)
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(UCLCDDisplay1));
		label1 = new Label();
		label2 = new Label();
		label3 = new Label();
		label4 = new Label();
		label5 = new Label();
		label6 = new Label();
		axWindowsMediaPlayer1 = new AxWindowsMediaPlayer();
		((ISupportInitialize)axWindowsMediaPlayer1).BeginInit();
		((Control)this).SuspendLayout();
		((Control)label1).AutoSize = true;
		((Control)label1).BackColor = Color.Transparent;
		label1.FlatStyle = (FlatStyle)0;
		((Control)label1).Font = new Font("Impact", 12f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
		((Control)label1).ForeColor = Color.FromArgb(141, 141, 141);
		((Control)label1).Location = new Point(65, 42);
		((Control)label1).Margin = new Padding(0);
		((Control)label1).Name = "label1";
		((Control)label1).Size = new Size(71, 20);
		((Control)label1).TabIndex = 47;
		((Control)label1).Text = "CPU TEMP";
		label1.TextAlign = (ContentAlignment)32;
		((Control)label2).AutoSize = true;
		((Control)label2).BackColor = Color.Transparent;
		label2.FlatStyle = (FlatStyle)0;
		((Control)label2).Font = new Font("Impact", 18f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
		((Control)label2).ForeColor = Color.FromArgb(87, 87, 87);
		((Control)label2).Location = new Point(65, 58);
		((Control)label2).Margin = new Padding(0);
		((Control)label2).Name = "label2";
		((Control)label2).Size = new Size(60, 29);
		((Control)label2).TabIndex = 48;
		((Control)label2).Text = "56℃";
		label2.TextAlign = (ContentAlignment)32;
		((Control)label3).AutoSize = true;
		((Control)label3).BackColor = Color.Transparent;
		label3.FlatStyle = (FlatStyle)0;
		((Control)label3).Font = new Font("Impact", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
		((Control)label3).ForeColor = Color.FromArgb(141, 141, 141);
		((Control)label3).Location = new Point(0, 120);
		((Control)label3).Margin = new Padding(0);
		((Control)label3).Name = "label3";
		((Control)label3).Size = new Size(62, 18);
		((Control)label3).TabIndex = 49;
		((Control)label3).Text = "GPU TEMP";
		label3.TextAlign = (ContentAlignment)32;
		((Control)label4).AutoSize = true;
		((Control)label4).BackColor = Color.Transparent;
		label4.FlatStyle = (FlatStyle)0;
		((Control)label4).Font = new Font("Impact", 15.75f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
		((Control)label4).ForeColor = Color.FromArgb(87, 87, 87);
		((Control)label4).Location = new Point(0, 134);
		((Control)label4).Margin = new Padding(0);
		((Control)label4).Name = "label4";
		((Control)label4).Size = new Size(51, 26);
		((Control)label4).TabIndex = 50;
		((Control)label4).Text = "56℃";
		label4.TextAlign = (ContentAlignment)32;
		((Control)label5).AutoSize = true;
		((Control)label5).BackColor = Color.Transparent;
		label5.FlatStyle = (FlatStyle)0;
		((Control)label5).Font = new Font("Impact", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
		((Control)label5).ForeColor = Color.FromArgb(141, 141, 141);
		((Control)label5).Location = new Point(129, 120);
		((Control)label5).Margin = new Padding(0);
		((Control)label5).Name = "label5";
		((Control)label5).Size = new Size(54, 18);
		((Control)label5).TabIndex = 51;
		((Control)label5).Text = "CPU FAN";
		label5.TextAlign = (ContentAlignment)32;
		((Control)label6).AutoSize = true;
		((Control)label6).BackColor = Color.Transparent;
		label6.FlatStyle = (FlatStyle)0;
		((Control)label6).Font = new Font("Impact", 15.75f, (FontStyle)0, (GraphicsUnit)3, (byte)0);
		((Control)label6).ForeColor = Color.FromArgb(87, 87, 87);
		((Control)label6).Location = new Point(129, 134);
		((Control)label6).Margin = new Padding(0);
		((Control)label6).Name = "label6";
		((Control)label6).Size = new Size(90, 26);
		((Control)label6).TabIndex = 52;
		((Control)label6).Text = "1666RPM";
		label6.TextAlign = (ContentAlignment)32;
		((Control)axWindowsMediaPlayer1).Dock = (DockStyle)5;
		((AxHost)axWindowsMediaPlayer1).Enabled = true;
		((Control)axWindowsMediaPlayer1).Location = new Point(0, 0);
		((Control)axWindowsMediaPlayer1).Name = "axWindowsMediaPlayer1";
		((AxHost)axWindowsMediaPlayer1).OcxState = (State)componentResourceManager.GetObject("axWindowsMediaPlayer1.OcxState");
		((Control)axWindowsMediaPlayer1).Size = new Size(260, 192);
		((Control)axWindowsMediaPlayer1).TabIndex = 53;
		((Control)axWindowsMediaPlayer1).Visible = false;
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)3;
		((Control)this).BackColor = Color.Transparent;
		((Control)this).BackgroundImageLayout = (ImageLayout)3;
		((Control)this).Controls.Add((Control)(object)label5);
		((Control)this).Controls.Add((Control)(object)label6);
		((Control)this).Controls.Add((Control)(object)label3);
		((Control)this).Controls.Add((Control)(object)label4);
		((Control)this).Controls.Add((Control)(object)label1);
		((Control)this).Controls.Add((Control)(object)label2);
		((Control)this).Controls.Add((Control)(object)axWindowsMediaPlayer1);
		((Control)this).DoubleBuffered = true;
		((Control)this).Margin = new Padding(0);
		((Control)this).Name = "UCLCDDisplay1";
		((Control)this).Size = new Size(260, 192);
		((ISupportInitialize)axWindowsMediaPlayer1).EndInit();
		((Control)this).ResumeLayout(false);
		((Control)this).PerformLayout();
	}
}
