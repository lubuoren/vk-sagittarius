using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using VAIKYRIE.Properties;

namespace VAIKYRIE;

public class UCState1 : UserControl
{
	private int myPercentage = 0;

	private float myVal = 0f;

	private StringFormat m_format = new StringFormat();

	private Font m_textFont = new Font("微软雅黑", 13.6f, (FontStyle)1, (GraphicsUnit)3, (byte)134);

	private SolidBrush m_textBrush = new SolidBrush(Color.White);

	private IContainer components = null;

	public UCState1()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Expected O, but got Unknown
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected O, but got Unknown
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Expected O, but got Unknown
		InitializeComponent();
		m_format.LineAlignment = (StringAlignment)1;
		m_format.Alignment = (StringAlignment)1;
	}

	public void SetUCState1(int percentage, float val)
	{
		if (percentage > 100)
		{
			percentage = 100;
		}
		myPercentage = percentage;
		myVal = val;
		((Control)this).Invalidate();
	}

	public void GetUCState1(ref int percentage, ref float val)
	{
		percentage = myPercentage;
		val = myVal;
	}

	protected override void OnPaint(PaintEventArgs pe)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Expected O, but got Unknown
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Expected O, but got Unknown
		((Control)this).OnPaint(pe);
		Graphics graphics = pe.Graphics;
		if (myPercentage != 0)
		{
			Bitmap val = new Bitmap(87, 89);
			Graphics val2 = Graphics.FromImage((Image)(object)val);
			GraphicsPath val3 = new GraphicsPath();
			val3.AddPie(0, 0, 87, 89, 180f, (float)myPercentage * 3.6f);
			val2.SetClip(val3);
			val2.DrawImage((Image)(object)Resources.Blh1, 0, 0);
			graphics.DrawImage((Image)(object)val, (float)(((Control)this).Width - 87) / 2f, (float)(((Control)this).Height - 89) / 2f);
			val2.Dispose();
			((Image)val).Dispose();
			val3.Dispose();
		}
		RectangleF rectangleF = new RectangleF(0f, 0f, 106f, 106f);
		graphics.DrawString(myVal.ToString(), m_textFont, (Brush)(object)m_textBrush, rectangleF, m_format);
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
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		((Control)this).SuspendLayout();
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)3;
		((Control)this).BackColor = Color.Transparent;
		((Control)this).BackgroundImageLayout = (ImageLayout)2;
		((Control)this).DoubleBuffered = true;
		((Control)this).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)this).Margin = new Padding(0);
		((Control)this).Name = "UCState1";
		((Control)this).Size = new Size(110, 110);
		((Control)this).ResumeLayout(false);
	}
}
