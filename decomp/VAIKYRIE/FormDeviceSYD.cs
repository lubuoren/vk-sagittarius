using System;
using System.ComponentModel;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;
using VAIKYRIE.Properties;

namespace VAIKYRIE;

public class FormDeviceSYD : Form
{
	public delegate void delegateFormDevice(object dev, int cmd, int ch = 0, int mode = 0, object obj = null);

	private int myMode = 1;

	public delegateFormDevice delegateFD;

	private readonly byte USB_PACKED_LED = UserDevice.USB_PACKED_LED;

	private readonly byte USB_PACKED_GET_STATE = 2;

	private readonly byte USB_PACKED_AUDIO = 4;

	private int myCount = 0;

	private Timer m_timer = null;

	public Form1 form;

	private IContainer components = null;

	private Button buttonZDYMS;

	private Button buttonZHMS;

	private UserDeviceSYD2 userDeviceSYD21;

	private UserDeviceSYD1 userDeviceSYD11;

	public FormDeviceSYD()
	{
		InitializeComponent();
		m_timer = new Timer(100.0);
		m_timer.Elapsed += Timer_event;
		m_timer.AutoReset = true;
		m_timer.Start();
		userDeviceSYD11.ucDeviceLEDS1 = SendMyLEDData;
		userDeviceSYD21.ucDeviceLEDS2 = SendMyLEDData;
	}

	private void Timer_event(object sender, ElapsedEventArgs e)
	{
		myCount++;
		if (myCount == 10)
		{
			myCount = 0;
			delegateFD?.Invoke(this, USB_PACKED_GET_STATE);
		}
		else
		{
			delegateFD?.Invoke(this, USB_PACKED_AUDIO);
		}
	}

	private void SendMyLEDData(int ch, int cmd, object obj)
	{
		delegateFD?.Invoke(this, USB_PACKED_LED, ch, cmd, obj);
	}

	public void ReceivedData(byte[] data)
	{
		if (data.Length != 0 && data[12] == USB_PACKED_GET_STATE)
		{
			userDeviceSYD21.ReceivedLEDData(data);
			userDeviceSYD11.ReceivedLEDData(data);
		}
	}

	private void buttonZDYMS_Click(int mode)
	{
		switch (mode)
		{
		case 1:
			((Control)buttonZHMS).BackgroundImage = (Image)(object)Resources.ButZHMS1;
			((Control)buttonZDYMS).BackgroundImage = (Image)(object)Resources.ButZDYMS2;
			((Control)userDeviceSYD21).Hide();
			((Control)userDeviceSYD11).Show();
			break;
		case 2:
			((Control)buttonZHMS).BackgroundImage = (Image)(object)Resources.ButZHMS2;
			((Control)buttonZDYMS).BackgroundImage = (Image)(object)Resources.ButZDYMS1;
			((Control)userDeviceSYD11).Hide();
			((Control)userDeviceSYD21).Show();
			break;
		}
	}

	private void buttonZHMS_Click(object sender, EventArgs e)
	{
		myMode = 1;
		buttonZDYMS_Click(myMode);
	}

	private void buttonZDYMS_Click(object sender, EventArgs e)
	{
		myMode = 2;
		buttonZDYMS_Click(myMode);
	}

	private void FormDeviceSYD_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (m_timer != null)
		{
			m_timer.Stop();
			m_timer.Close();
			m_timer.Dispose();
		}
		form = null;
	}

	private void FormDeviceSYD_MouseDown(object sender, MouseEventArgs e)
	{
		form.Form1_MouseDown(this, e);
	}

	private void FormDeviceSYD_MouseMove(object sender, MouseEventArgs e)
	{
		form.Form1_MouseMove(this, e);
	}

	private void FormDeviceSYD_MouseUp(object sender, MouseEventArgs e)
	{
		form.Form1_MouseUp(this, e);
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
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Expected O, but got Unknown
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Expected O, but got Unknown
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Expected O, but got Unknown
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Expected O, but got Unknown
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Expected O, but got Unknown
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Expected O, but got Unknown
		//IL_049f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Expected O, but got Unknown
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Expected O, but got Unknown
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FormDeviceSYD));
		buttonZDYMS = new Button();
		buttonZHMS = new Button();
		userDeviceSYD11 = new UserDeviceSYD1();
		userDeviceSYD21 = new UserDeviceSYD2();
		((Control)this).SuspendLayout();
		((Control)buttonZDYMS).BackColor = Color.Transparent;
		((Control)buttonZDYMS).BackgroundImage = (Image)(object)Resources.ButZDYMS2;
		((Control)buttonZDYMS).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonZDYMS).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonZDYMS).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonZDYMS).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonZDYMS).FlatStyle = (FlatStyle)0;
		((Control)buttonZDYMS).Location = new Point(815, 206);
		((Control)buttonZDYMS).Margin = new Padding(0);
		((Control)buttonZDYMS).Name = "buttonZDYMS";
		((Control)buttonZDYMS).Size = new Size(57, 12);
		((Control)buttonZDYMS).TabIndex = 15;
		((ButtonBase)buttonZDYMS).UseVisualStyleBackColor = false;
		((Control)buttonZDYMS).Click += buttonZDYMS_Click;
		((Control)buttonZHMS).BackColor = Color.Transparent;
		((Control)buttonZHMS).BackgroundImage = (Image)(object)Resources.ButZHMS1;
		((Control)buttonZHMS).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonZHMS).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonZHMS).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonZHMS).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonZHMS).FlatStyle = (FlatStyle)0;
		((Control)buttonZHMS).Location = new Point(722, 206);
		((Control)buttonZHMS).Margin = new Padding(0);
		((Control)buttonZHMS).Name = "buttonZHMS";
		((Control)buttonZHMS).Size = new Size(47, 12);
		((Control)buttonZHMS).TabIndex = 14;
		((ButtonBase)buttonZHMS).UseVisualStyleBackColor = false;
		((Control)buttonZHMS).Click += buttonZHMS_Click;
		((Control)userDeviceSYD11).BackColor = Color.Transparent;
		((Control)userDeviceSYD11).BackgroundImage = (Image)componentResourceManager.GetObject("userDeviceSYD11.BackgroundImage");
		((Control)userDeviceSYD11).Font = new Font("微软雅黑", 9f);
		((Control)userDeviceSYD11).Location = new Point(0, 250);
		((Control)userDeviceSYD11).Margin = new Padding(0);
		((Control)userDeviceSYD11).Name = "userDeviceSYD11";
		((Control)userDeviceSYD11).Size = new Size(916, 550);
		((Control)userDeviceSYD11).TabIndex = 17;
		((Control)userDeviceSYD21).BackColor = Color.Transparent;
		((Control)userDeviceSYD21).BackgroundImage = (Image)componentResourceManager.GetObject("userDeviceSYD21.BackgroundImage");
		((Control)userDeviceSYD21).Font = new Font("微软雅黑", 9f);
		((Control)userDeviceSYD21).Location = new Point(0, 250);
		((Control)userDeviceSYD21).Margin = new Padding(0);
		((Control)userDeviceSYD21).Name = "userDeviceSYD21";
		((Control)userDeviceSYD21).Size = new Size(916, 550);
		((Control)userDeviceSYD21).TabIndex = 16;
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)3;
		((Control)this).BackColor = Color.White;
		((Control)this).BackgroundImage = (Image)(object)Resources.Background3;
		((Control)this).BackgroundImageLayout = (ImageLayout)3;
		((Form)this).ClientSize = new Size(916, 800);
		((Control)this).Controls.Add((Control)(object)buttonZDYMS);
		((Control)this).Controls.Add((Control)(object)buttonZHMS);
		((Control)this).Controls.Add((Control)(object)userDeviceSYD11);
		((Control)this).Controls.Add((Control)(object)userDeviceSYD21);
		((Control)this).DoubleBuffered = true;
		((Control)this).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Form)this).FormBorderStyle = (FormBorderStyle)0;
		((Form)this).Location = new Point(89, 0);
		((Form)this).MaximizeBox = false;
		((Form)this).MinimizeBox = false;
		((Control)this).Name = "FormDeviceSYD";
		((Control)this).Text = "FormDeviceSYD";
		((Form)this).FormClosing += new FormClosingEventHandler(FormDeviceSYD_FormClosing);
		((Control)this).MouseDown += new MouseEventHandler(FormDeviceSYD_MouseDown);
		((Control)this).MouseMove += new MouseEventHandler(FormDeviceSYD_MouseMove);
		((Control)this).MouseUp += new MouseEventHandler(FormDeviceSYD_MouseUp);
		((Control)this).ResumeLayout(false);
	}
}
