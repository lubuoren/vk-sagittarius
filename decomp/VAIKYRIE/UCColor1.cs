using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VAIKYRIE.Properties;

namespace VAIKYRIE;

public class UCColor1 : UserControl
{
	public delegate void delegate_UCColor1(int r, int g, int b);

	public delegate_UCColor1 upDateUCColor1;

	private readonly Image pointImg = (Image)(object)Resources.CBKZH2;

	public int imageX = 0;

	public int imageY = 0;

	private bool isMouseDown = false;

	private int xPos;

	private int yPos;

	private int centerX;

	private int centerY;

	private readonly int MaxR = 110;

	private readonly int MinR = 60;

	private readonly int RGBR = 85;

	private double rgbVal = 0.0;

	public byte colourR = byte.MaxValue;

	public byte colourG = 0;

	public byte colourB = 0;

	private IContainer components = null;

	public UCColor1()
	{
		InitializeComponent();
		centerX = ((Control)this).Width / 2;
		centerY = ((Control)this).Height / 2;
		imageX = centerX - RGBR - pointImg.Width / 2;
		imageY = centerY - pointImg.Height / 2;
	}

	protected override void OnPaint(PaintEventArgs pe)
	{
		((Control)this).OnPaint(pe);
		Graphics graphics = pe.Graphics;
		Rectangle rectangle = new Rectangle(imageX, imageY, pointImg.Width, pointImg.Height);
		graphics.DrawImage(pointImg, rectangle);
	}

	private void DCColourSelect_Move()
	{
		if (xPos == centerX)
		{
			if (yPos > centerY)
			{
				imageX = xPos - pointImg.Width / 2;
				imageY = centerY + RGBR - pointImg.Height / 2;
			}
			else
			{
				imageX = xPos - pointImg.Width / 2;
				imageY = centerY - RGBR - pointImg.Height / 2;
			}
		}
		else if (xPos > centerX)
		{
			double num = (double)(yPos - centerY) / (double)(xPos - centerX);
			double num2 = Math.Pow((double)(RGBR * RGBR) / (1.0 + num * num), 0.5);
			double num3 = num * num2;
			imageX = (int)(num2 + (double)centerX - (double)(pointImg.Width / 2));
			imageY = (int)(num3 + (double)centerY - (double)(pointImg.Height / 2));
		}
		else
		{
			double num4 = (double)(yPos - centerY) / (double)(xPos - centerX);
			double num5 = 0.0 - Math.Pow((double)(RGBR * RGBR) / (1.0 + num4 * num4), 0.5);
			double num6 = num4 * num5;
			imageX = (int)(num5 + (double)centerX - (double)(pointImg.Width / 2));
			imageY = (int)(num6 + (double)centerY - (double)(pointImg.Height / 2));
		}
	}

	private void setRGBVal(double val)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		val -= 90.0;
		if (val < 0.0)
		{
			val += 360.0;
		}
		val = (val + 0.0) % 360.0;
		if (val <= 60.0)
		{
			num = 255;
			num3 = (int)(255.0 * val / 60.0);
		}
		else if (val <= 120.0)
		{
			num = (int)(255.0 * (120.0 - val) / 60.0);
			num3 = 255;
		}
		else if (val <= 180.0)
		{
			num2 = (int)(255.0 * (val - 120.0) / 60.0);
			num3 = 255;
		}
		else if (val <= 240.0)
		{
			num2 = 255;
			num3 = (int)(255.0 * (240.0 - val) / 60.0);
		}
		else if (val <= 300.0)
		{
			num2 = 255;
			num = (int)(255.0 * (val - 240.0) / 60.0);
		}
		else
		{
			num = 255;
			num2 = (int)(255.0 * (360.0 - val) / 60.0);
		}
		colourR = (byte)num;
		colourB = (byte)num3;
		colourG = (byte)num2;
		upDateUCColor1?.Invoke(num, num2, num3);
	}

	private void UCColor1_MouseDown(object sender, MouseEventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Invalid comparison between Unknown and I4
		if ((int)e.Button == 1048576)
		{
			xPos = e.X;
			yPos = e.Y;
			if ((xPos - centerX) * (xPos - centerX) + (yPos - centerY) * (yPos - centerY) < MaxR * MaxR && (xPos - centerX) * (xPos - centerX) + (yPos - centerY) * (yPos - centerY) > MinR * MinR)
			{
				isMouseDown = true;
				DCColourSelect_Move();
				((Control)this).Invalidate();
			}
		}
	}

	private void UCColor1_MouseMove(object sender, MouseEventArgs e)
	{
		xPos = e.X;
		yPos = e.Y;
		if (isMouseDown)
		{
			DCColourSelect_Move();
			((Control)this).Invalidate();
		}
	}

	private void UCColor1_MouseUp(object sender, MouseEventArgs e)
	{
		if (isMouseDown)
		{
			DCColourSelect_Move();
			isMouseDown = false;
			double num = centerX;
			double num2 = centerY;
			double num3 = (double)e.X - num;
			double num4 = (double)e.Y - num2;
			if (num4 < 0.0)
			{
				rgbVal = Math.Atan(num3 / num4) * 180.0 / Math.PI;
			}
			else if (num4 > 0.0)
			{
				rgbVal = Math.Atan(num3 / num4) * 180.0 / Math.PI;
				rgbVal += 180.0;
			}
			else if (num3 > 0.0)
			{
				rgbVal = 270.0;
			}
			else
			{
				rgbVal = 90.0;
			}
			setRGBVal(rgbVal);
			((Control)this).Invalidate();
		}
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
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Expected O, but got Unknown
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Expected O, but got Unknown
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Expected O, but got Unknown
		((Control)this).SuspendLayout();
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)3;
		((Control)this).BackColor = Color.Transparent;
		((Control)this).BackgroundImage = (Image)(object)Resources.CBKZH1;
		((Control)this).BackgroundImageLayout = (ImageLayout)2;
		((Control)this).DoubleBuffered = true;
		((Control)this).Margin = new Padding(0);
		((Control)this).Name = "UCColor1";
		((Control)this).Size = new Size(220, 220);
		((Control)this).MouseDown += new MouseEventHandler(UCColor1_MouseDown);
		((Control)this).MouseMove += new MouseEventHandler(UCColor1_MouseMove);
		((Control)this).MouseUp += new MouseEventHandler(UCColor1_MouseUp);
		((Control)this).ResumeLayout(false);
	}
}
