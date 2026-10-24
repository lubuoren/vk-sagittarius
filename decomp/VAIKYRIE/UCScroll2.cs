using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VAIKYRIE.Properties;

namespace VAIKYRIE;

public class UCScroll2 : UserControl
{
	public delegate void delegate_UCScroll2(int val);

	private int myVal = 100;

	private bool isMouseDown = false;

	private int xPos;

	private readonly Image imageBK = (Image)(object)Resources.SYDjdt;

	public delegate_UCScroll2 upDateUCScroll2;

	private IContainer components = null;

	public UCScroll2()
	{
		InitializeComponent();
	}

	private void UCScroll2_MouseDown(object sender, MouseEventArgs e)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Invalid comparison between Unknown and I4
		if (!isMouseDown && (int)e.Button == 1048576)
		{
			xPos = e.X;
			myVal = xPos * 100 / imageBK.Width;
			if (myVal < 0)
			{
				myVal = 0;
			}
			if (myVal > 100)
			{
				myVal = 100;
			}
			isMouseDown = true;
			((Control)this).Invalidate();
		}
	}

	private void UCScroll2_MouseMove(object sender, MouseEventArgs e)
	{
		if (isMouseDown)
		{
			xPos = e.X;
			myVal = xPos * 100 / imageBK.Width;
			if (myVal < 0)
			{
				myVal = 0;
			}
			if (myVal > 100)
			{
				myVal = 100;
			}
			((Control)this).Invalidate();
		}
	}

	private void UCScroll2_MouseUp(object sender, MouseEventArgs e)
	{
		if (isMouseDown)
		{
			xPos = e.X;
			myVal = xPos * 100 / imageBK.Width;
			if (myVal < 0)
			{
				myVal = 0;
			}
			if (myVal > 100)
			{
				myVal = 100;
			}
			((Control)this).Invalidate();
			isMouseDown = false;
			upDateUCScroll2?.Invoke(myVal);
		}
	}

	protected override void OnPaint(PaintEventArgs pe)
	{
		((Control)this).OnPaint(pe);
		Graphics graphics = pe.Graphics;
		int width = imageBK.Width;
		int height = imageBK.Height;
		int width2 = width * myVal / 100;
		Rectangle rectangle = new Rectangle(0, (((Control)this).Height - height) / 2, width2, height);
		Rectangle rectangle2 = new Rectangle(0, 0, width2, height);
		graphics.DrawImage(imageBK, rectangle, rectangle2, (GraphicsUnit)2);
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
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Expected O, but got Unknown
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Expected O, but got Unknown
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		((Control)this).SuspendLayout();
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)3;
		((Control)this).BackColor = Color.Transparent;
		((Control)this).BackgroundImageLayout = (ImageLayout)4;
		((Control)this).DoubleBuffered = true;
		((Control)this).Margin = new Padding(0);
		((Control)this).Name = "UCScroll2";
		((Control)this).Size = new Size(301, 11);
		((Control)this).MouseDown += new MouseEventHandler(UCScroll2_MouseDown);
		((Control)this).MouseMove += new MouseEventHandler(UCScroll2_MouseMove);
		((Control)this).MouseUp += new MouseEventHandler(UCScroll2_MouseUp);
		((Control)this).ResumeLayout(false);
	}
}
