using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VAIKYRIE.Properties;

namespace VAIKYRIE;

public class UCState3 : UserControl
{
	private StringFormat m_format = new StringFormat();

	private Font m_textFont = new Font("微软雅黑", 11.6f, (FontStyle)1, (GraphicsUnit)3, (byte)134);

	private SolidBrush m_textBrush = new SolidBrush(Color.White);

	private int myPercentage = 0;

	private float myVal = 0f;

	private IContainer components = null;

	public UCState3()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Expected O, but got Unknown
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Expected O, but got Unknown
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

	protected override void OnPaint(PaintEventArgs pe)
	{
		((Control)this).OnPaint(pe);
		Graphics graphics = pe.Graphics;
		RectangleF rectangleF = new RectangleF(0f, 0f, ((Control)this).Width - 2, ((Control)this).Height - 2);
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
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Expected O, but got Unknown
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		((Control)this).SuspendLayout();
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)3;
		((Control)this).BackColor = Color.Transparent;
		((Control)this).BackgroundImage = (Image)(object)Resources.Blh3;
		((Control)this).BackgroundImageLayout = (ImageLayout)2;
		((Control)this).DoubleBuffered = true;
		((Control)this).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)this).Margin = new Padding(0);
		((Control)this).Name = "UCState3";
		((Control)this).Size = new Size(73, 75);
		((Control)this).ResumeLayout(false);
	}
}
