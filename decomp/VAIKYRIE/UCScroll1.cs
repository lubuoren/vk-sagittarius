using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VAIKYRIE.Properties;

namespace VAIKYRIE;

public class UCScroll1 : UserControl
{
	public delegate void delegate_UCScroll1(int val);

	private int myVal = 100;

	private bool isMouseDown = false;

	private int xPos;

	private int xPY = 2;

	private readonly Image imageBK = (Image)(object)Resources.BSCB2;

	private readonly Image imageBB = (Image)(object)Resources.BSCB1;

	public delegate_UCScroll1 upDateUCScroll1;

	private IContainer components = null;

	public UCScroll1()
	{
		InitializeComponent();
	}

	private void UCScroll1_MouseDown(object sender, MouseEventArgs e)
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

	private void UCScroll1_MouseMove(object sender, MouseEventArgs e)
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

	private void UCScroll1_MouseUp(object sender, MouseEventArgs e)
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
			upDateUCScroll1?.Invoke(myVal);
		}
	}

	public void setUCScroll1(int val)
	{
		myVal = val;
		((Control)this).Invalidate();
	}

	protected override void OnPaint(PaintEventArgs pe)
	{
		((Control)this).OnPaint(pe);
		Graphics graphics = pe.Graphics;
		int width = imageBK.Width;
		int height = imageBK.Height;
		int num = width * myVal / 100;
		Rectangle rectangle = new Rectangle(0, (((Control)this).Height - height) / 2, num, height);
		Rectangle rectangle2 = new Rectangle(0, 0, num, height);
		graphics.DrawImage(imageBK, rectangle, rectangle2, (GraphicsUnit)2);
		graphics.DrawImage(imageBB, num - xPY, 0);
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
		((Control)this).Name = "UCScroll1";
		((Control)this).Size = new Size(404, 16);
		((Control)this).MouseDown += new MouseEventHandler(UCScroll1_MouseDown);
		((Control)this).MouseMove += new MouseEventHandler(UCScroll1_MouseMove);
		((Control)this).MouseUp += new MouseEventHandler(UCScroll1_MouseUp);
		((Control)this).ResumeLayout(false);
	}
}
