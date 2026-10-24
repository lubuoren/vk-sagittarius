using System;
using System.ComponentModel;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;
using VAIKYRIE.Properties;

namespace VAIKYRIE;

public class FormDevice : Form
{
	public delegate void delegateFormDevice(object dev, int cmd, int ch = 0, int mode = 0, object obj = null);

	public delegateFormDevice delegateFD;

	private readonly byte USB_PACKED_FAN = UserDevice.USB_PACKED_FAN;

	private readonly byte USB_PACKED_LED = UserDevice.USB_PACKED_LED;

	private readonly byte USB_PACKED_GET_STATE = 2;

	private readonly byte USB_PACKED_AUDIO = 4;

	private int myCount = 0;

	private Timer m_timer = null;

	public Form1 form;

	private IContainer components = null;

	private UserDeviceLED userDeviceLED1;

	private UserDeviceFAN userDeviceFAN1;

	private Button buttonLED;

	private Button buttonFAN;

	public FormDevice()
	{
		InitializeComponent();
		m_timer = new Timer(100.0);
		m_timer.Elapsed += Timer_event;
		m_timer.AutoReset = true;
		m_timer.Start();
		userDeviceLED1.ucDeviceLEDF = SendMyLEDData;
		userDeviceFAN1.ucDeviceFANF = SendMyFANData;
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

	private void SendMyFANData(int ch, int cmd, object obj)
	{
		delegateFD?.Invoke(this, USB_PACKED_FAN, ch, cmd, obj);
	}

	public void ReceivedData(byte[] data)
	{
		if (data.Length != 0 && data[12] == USB_PACKED_GET_STATE)
		{
			userDeviceFAN1.ReceivedFANData(data);
			userDeviceLED1.ReceivedLEDData(data);
		}
	}

	private void buttonLED_Click(object sender, EventArgs e)
	{
		((Control)buttonLED).BackgroundImage = (Image)(object)Resources.dgkzt1;
		((Control)buttonFAN).BackgroundImage = (Image)(object)Resources.fskzt2;
		((Control)userDeviceFAN1).Hide();
		((Control)userDeviceLED1).Show();
	}

	private void buttonFAN_Click(object sender, EventArgs e)
	{
		((Control)buttonLED).BackgroundImage = (Image)(object)Resources.dgkzt2;
		((Control)buttonFAN).BackgroundImage = (Image)(object)Resources.fskzt1;
		((Control)userDeviceLED1).Hide();
		((Control)userDeviceFAN1).Show();
	}

	private void FormDevice_FormClosing(object sender, FormClosingEventArgs e)
	{
		if (m_timer != null)
		{
			m_timer.Stop();
			m_timer.Close();
			m_timer.Dispose();
		}
		form = null;
	}

	private void FormDevice_MouseDown(object sender, MouseEventArgs e)
	{
		form.Form1_MouseDown(this, e);
	}

	private void FormDevice_MouseMove(object sender, MouseEventArgs e)
	{
		form.Form1_MouseMove(this, e);
	}

	private void FormDevice_MouseUp(object sender, MouseEventArgs e)
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
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Expected O, but got Unknown
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Expected O, but got Unknown
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Expected O, but got Unknown
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Expected O, but got Unknown
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Expected O, but got Unknown
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04aa: Expected O, but got Unknown
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Expected O, but got Unknown
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FormDevice));
		buttonLED = new Button();
		buttonFAN = new Button();
		userDeviceLED1 = new UserDeviceLED();
		userDeviceFAN1 = new UserDeviceFAN();
		((Control)this).SuspendLayout();
		((Control)buttonLED).BackColor = Color.Transparent;
		((Control)buttonLED).BackgroundImage = (Image)(object)Resources.dgkzt1;
		((Control)buttonLED).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonLED).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonLED).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonLED).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonLED).FlatStyle = (FlatStyle)0;
		((Control)buttonLED).Location = new Point(722, 206);
		((Control)buttonLED).Margin = new Padding(0);
		((Control)buttonLED).Name = "buttonLED";
		((Control)buttonLED).Size = new Size(58, 12);
		((Control)buttonLED).TabIndex = 12;
		((ButtonBase)buttonLED).UseVisualStyleBackColor = false;
		((Control)buttonLED).Click += buttonLED_Click;
		((Control)buttonFAN).BackColor = Color.Transparent;
		((Control)buttonFAN).BackgroundImage = (Image)(object)Resources.fskzt2;
		((Control)buttonFAN).BackgroundImageLayout = (ImageLayout)3;
		((ButtonBase)buttonFAN).FlatAppearance.BorderSize = 0;
		((ButtonBase)buttonFAN).FlatAppearance.MouseDownBackColor = Color.Transparent;
		((ButtonBase)buttonFAN).FlatAppearance.MouseOverBackColor = Color.Transparent;
		((ButtonBase)buttonFAN).FlatStyle = (FlatStyle)0;
		((Control)buttonFAN).Location = new Point(815, 206);
		((Control)buttonFAN).Margin = new Padding(0);
		((Control)buttonFAN).Name = "buttonFAN";
		((Control)buttonFAN).Size = new Size(58, 12);
		((Control)buttonFAN).TabIndex = 13;
		((ButtonBase)buttonFAN).UseVisualStyleBackColor = false;
		((Control)buttonFAN).Click += buttonFAN_Click;
		((Control)userDeviceLED1).BackColor = Color.Transparent;
		((Control)userDeviceLED1).BackgroundImage = (Image)(object)Resources.Background3_2;
		((Control)userDeviceLED1).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)userDeviceLED1).Location = new Point(0, 250);
		((Control)userDeviceLED1).Margin = new Padding(0);
		((Control)userDeviceLED1).Name = "userDeviceLED1";
		((Control)userDeviceLED1).Size = new Size(916, 550);
		((Control)userDeviceLED1).TabIndex = 0;
		((Control)userDeviceFAN1).BackColor = Color.Transparent;
		((Control)userDeviceFAN1).BackgroundImage = (Image)componentResourceManager.GetObject("userDeviceFAN1.BackgroundImage");
		((Control)userDeviceFAN1).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)userDeviceFAN1).Location = new Point(0, 250);
		((Control)userDeviceFAN1).Margin = new Padding(0);
		((Control)userDeviceFAN1).Name = "userDeviceFAN1";
		((Control)userDeviceFAN1).Size = new Size(916, 550);
		((Control)userDeviceFAN1).TabIndex = 1;
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)3;
		((Control)this).BackColor = Color.White;
		((Control)this).BackgroundImage = (Image)(object)Resources.Background3;
		((Control)this).BackgroundImageLayout = (ImageLayout)3;
		((Form)this).ClientSize = new Size(916, 800);
		((Control)this).Controls.Add((Control)(object)buttonFAN);
		((Control)this).Controls.Add((Control)(object)buttonLED);
		((Control)this).Controls.Add((Control)(object)userDeviceLED1);
		((Control)this).Controls.Add((Control)(object)userDeviceFAN1);
		((Control)this).DoubleBuffered = true;
		((Control)this).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Form)this).FormBorderStyle = (FormBorderStyle)0;
		((Form)this).Location = new Point(89, 0);
		((Form)this).MaximizeBox = false;
		((Form)this).MinimizeBox = false;
		((Control)this).Name = "FormDevice";
		((Control)this).Text = "FormDevice";
		((Form)this).FormClosing += new FormClosingEventHandler(FormDevice_FormClosing);
		((Control)this).MouseDown += new MouseEventHandler(FormDevice_MouseDown);
		((Control)this).MouseMove += new MouseEventHandler(FormDevice_MouseMove);
		((Control)this).MouseUp += new MouseEventHandler(FormDevice_MouseUp);
		((Control)this).ResumeLayout(false);
	}
}
