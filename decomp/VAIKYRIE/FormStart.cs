using System.ComponentModel;
using System.Drawing;
using System.Timers;
using System.Windows.Forms;
using VAIKYRIE.Properties;

namespace VAIKYRIE;

public class FormStart : Form
{
	private Timer m_timer;

	private const int JDT_Start_X = 282;

	private const int JDT_End_X = 556;

	private const int JDT_Y = 345;

	private const int JDT_W = 80;

	private const int JDT_H = 35;

	private int jdt_X = 282;

	private int jdt_N_X = 0;

	private IContainer components = null;

	public FormStart()
	{
		InitializeComponent();
		m_timer = new Timer(50.0);
		m_timer.Elapsed += timer_event;
		m_timer.AutoReset = true;
		m_timer.Start();
	}

	private void timer_event(object sender, ElapsedEventArgs e)
	{
		((Control)this).Invalidate();
	}

	public void setMyClose()
	{
		Control.CheckForIllegalCrossThreadCalls = false;
		m_timer.Stop();
		m_timer.Close();
		m_timer.Dispose();
		((Control)this).Hide();
		Control.CheckForIllegalCrossThreadCalls = true;
	}

	protected override void OnPaint(PaintEventArgs pe)
	{
		((Form)this).OnPaint(pe);
		Graphics graphics = pe.Graphics;
		if (jdt_X == 282 && jdt_N_X < 80)
		{
			graphics.DrawImage((Image)(object)Resources.LOADING_9202, jdt_X, 345, jdt_N_X, 35);
			jdt_N_X += 5;
		}
		else if (jdt_X < 556)
		{
			jdt_X += 5;
			graphics.DrawImage((Image)(object)Resources.LOADING_9202, jdt_X, 345, jdt_N_X, 35);
		}
		else if (jdt_X >= 556 && jdt_N_X > 0)
		{
			jdt_X += 5;
			jdt_N_X -= 5;
			graphics.DrawImage((Image)(object)Resources.LOADING_9202, jdt_X, 345, jdt_N_X, 35);
		}
		else
		{
			jdt_X = 282;
			jdt_N_X = 0;
		}
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
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Expected O, but got Unknown
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FormStart));
		((Control)this).SuspendLayout();
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)3;
		((Control)this).BackColor = Color.White;
		((Control)this).BackgroundImage = (Image)(object)Resources.LOADING_9201;
		((Control)this).BackgroundImageLayout = (ImageLayout)2;
		((Form)this).ClientSize = new Size(920, 640);
		((Control)this).Cursor = Cursors.WaitCursor;
		((Control)this).DoubleBuffered = true;
		((Control)this).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Form)this).FormBorderStyle = (FormBorderStyle)0;
		((Form)this).Icon = (Icon)componentResourceManager.GetObject("$this.Icon");
		((Form)this).MaximizeBox = false;
		((Form)this).MinimizeBox = false;
		((Control)this).Name = "FormStart";
		((Form)this).StartPosition = (FormStartPosition)1;
		((Control)this).Text = "FormStart";
		((Control)this).ResumeLayout(false);
	}
}
