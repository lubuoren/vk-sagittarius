using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using VAIKYRIE.Properties;

namespace VAIKYRIE;

public class UCDevFan1 : UserControl
{
	public delegate void delegateUCDevFan1();

	private const int MaxR = 12;

	private const int XStart = 42;

	private const int XEnd = 460;

	private const int YStart = 280;

	private const int YEnd = 28;

	private const int XCenter = 356;

	private bool isMouseDown = false;

	private bool is1 = false;

	private bool is2 = false;

	private bool is3 = false;

	private int xPos;

	private int yPos;

	private int centerX1 = 126;

	private int centerY1 = 228;

	private int centerX2 = 251;

	private int centerY2 = 162;

	private int centerX3 = 356;

	private int centerY3 = 52;

	public int[] fanVal;

	public delegateUCDevFan1 ucDeviceFAN1;

	private IContainer components = null;

	public UCDevFan1()
	{
		InitializeComponent();
		fanVal = new int[6] { 20, 50, 75, 20, 46, 90 };
	}

	private void UCDevFan1_Move(int x, int y)
	{
		bool flag = false;
		if (x < 42)
		{
			x = 42;
		}
		else if (x > 356)
		{
			x = 356;
		}
		if (y < 28)
		{
			y = 28;
		}
		else if (y > 280)
		{
			y = 280;
		}
		centerX1 = x;
		centerY1 = y;
		x = centerX2;
		y = centerY2;
		if (centerX1 > centerX2)
		{
			flag = true;
			x = centerX1;
		}
		if (centerY1 < centerY2)
		{
			flag = true;
			y = centerY1;
		}
		if (flag)
		{
			UCDevFan2_Move(x, y);
		}
	}

	private void UCDevFan2_Move(int x, int y)
	{
		bool flag = false;
		bool flag2 = false;
		if (x < 42)
		{
			x = 42;
		}
		else if (x > 356)
		{
			x = 356;
		}
		if (y < 28)
		{
			y = 28;
		}
		else if (y > 280)
		{
			y = 280;
		}
		centerX2 = x;
		centerY2 = y;
		x = centerX1;
		y = centerY1;
		if (centerX1 > centerX2)
		{
			flag = true;
			x = centerX2;
		}
		if (centerY1 < centerY2)
		{
			flag = true;
			y = centerY2;
		}
		if (flag)
		{
			UCDevFan1_Move(x, y);
		}
		x = centerX3;
		y = centerY3;
		if (centerX2 > centerX3)
		{
			flag2 = true;
			x = centerX2;
		}
		if (centerY2 < centerY3)
		{
			flag2 = true;
			y = centerY2;
		}
		if (flag2)
		{
			UCDevFan3_Move(x, y);
		}
	}

	private void UCDevFan3_Move(int x, int y)
	{
		bool flag = false;
		if (x < 42)
		{
			x = 42;
		}
		else if (x > 356)
		{
			x = 356;
		}
		if (y < 28)
		{
			y = 28;
		}
		else if (y > 280)
		{
			y = 280;
		}
		centerX3 = x;
		centerY3 = y;
		x = centerX2;
		y = centerY2;
		if (centerX2 > centerX3)
		{
			flag = true;
			x = centerX3;
		}
		if (centerY2 < centerY3)
		{
			flag = true;
			y = centerY3;
		}
		if (flag)
		{
			UCDevFan2_Move(x, y);
		}
	}

	private void UCDevFan1_MouseDown(object sender, MouseEventArgs e)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Invalid comparison between Unknown and I4
		if ((int)e.Button == 1048576)
		{
			xPos = e.X;
			yPos = e.Y;
			if ((xPos - centerX1) * (xPos - centerX1) + (yPos - centerY1) * (yPos - centerY1) < 144)
			{
				isMouseDown = true;
				is1 = true;
				UCDevFan1_Move(xPos, yPos);
				((Control)this).Invalidate();
			}
			else if ((xPos - centerX2) * (xPos - centerX2) + (yPos - centerY2) * (yPos - centerY2) < 144)
			{
				isMouseDown = true;
				is2 = true;
				UCDevFan2_Move(xPos, yPos);
				((Control)this).Invalidate();
			}
			else if ((xPos - centerX3) * (xPos - centerX3) + (yPos - centerY3) * (yPos - centerY3) < 144)
			{
				isMouseDown = true;
				is3 = true;
				UCDevFan3_Move(xPos, yPos);
				((Control)this).Invalidate();
			}
		}
	}

	private void UCDevFan1_MouseMove(object sender, MouseEventArgs e)
	{
		xPos = e.X;
		yPos = e.Y;
		if (isMouseDown)
		{
			if (is1)
			{
				UCDevFan1_Move(xPos, yPos);
			}
			if (is2)
			{
				UCDevFan2_Move(xPos, yPos);
			}
			if (is3)
			{
				UCDevFan3_Move(xPos, yPos);
			}
			((Control)this).Invalidate();
		}
	}

	private void UCDevFan1_MouseUp(object sender, MouseEventArgs e)
	{
		if (isMouseDown)
		{
			if (is1)
			{
				UCDevFan1_Move(xPos, yPos);
			}
			if (is2)
			{
				UCDevFan2_Move(xPos, yPos);
			}
			if (is3)
			{
				UCDevFan3_Move(xPos, yPos);
			}
			isMouseDown = false;
			is1 = false;
			is2 = false;
			is3 = false;
			((Control)this).Invalidate();
			fanVal[0] = 100 * (centerX1 - 42) / 418;
			fanVal[1] = 100 * (centerX2 - 42) / 418;
			fanVal[2] = 100 * (centerX3 - 42) / 418;
			fanVal[3] = 100 * (280 - centerY1) / 252;
			fanVal[4] = 100 * (280 - centerY2) / 252;
			fanVal[5] = 100 * (280 - centerY3) / 252;
			if (fanVal[2] <= fanVal[1])
			{
				fanVal[1] = fanVal[2] - 1;
			}
			if (fanVal[1] <= fanVal[0])
			{
				fanVal[0] = fanVal[1] - 1;
			}
			if (fanVal[0] < 1)
			{
				fanVal[0] = 1;
			}
			if (fanVal[1] <= fanVal[0])
			{
				fanVal[1] = fanVal[0] + 1;
			}
			if (fanVal[2] <= fanVal[1])
			{
				fanVal[2] = fanVal[1] + 1;
			}
			ucDeviceFAN1?.Invoke();
		}
	}

	protected override void OnPaint(PaintEventArgs pe)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Expected O, but got Unknown
		((Control)this).OnPaint(pe);
		Graphics graphics = pe.Graphics;
		graphics.SmoothingMode = (SmoothingMode)4;
		Color color = Color.FromArgb(238, 139, 4);
		Pen val = new Pen(color, 1f);
		Point point = default(Point);
		Point point2 = default(Point);
		Image fAN_GL = (Image)(object)Resources.FAN_GL;
		point.X = 42;
		point.Y = 280;
		point2.X = centerX1;
		point2.Y = centerY1;
		graphics.DrawLine(val, point, point2);
		point.X = centerX1;
		point.Y = centerY1;
		point2.X = centerX2;
		point2.Y = centerY2;
		graphics.DrawLine(val, point, point2);
		point.X = centerX2;
		point.Y = centerY2;
		point2.X = centerX3;
		point2.Y = centerY3;
		graphics.DrawLine(val, point, point2);
		point.X = centerX3;
		point.Y = centerY3;
		point2.X = 460;
		point2.Y = 28;
		graphics.DrawLine(val, point, point2);
		int num = fAN_GL.Width / 2 + 2;
		int num2 = fAN_GL.Height / 2 + 2;
		graphics.DrawImage(fAN_GL, centerX1 - num, centerY1 - num2);
		graphics.DrawImage(fAN_GL, centerX2 - num, centerY2 - num2);
		graphics.DrawImage(fAN_GL, centerX3 - num, centerY3 - num2);
		val.Dispose();
		fAN_GL.Dispose();
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
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Expected O, but got Unknown
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Expected O, but got Unknown
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Expected O, but got Unknown
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Expected O, but got Unknown
		((Control)this).SuspendLayout();
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)3;
		((Control)this).BackColor = Color.Transparent;
		((Control)this).BackgroundImage = (Image)(object)Resources.FANZDY;
		((Control)this).DoubleBuffered = true;
		((Control)this).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)this).Margin = new Padding(0);
		((Control)this).Name = "UCDevFan1";
		((Control)this).Size = new Size(492, 309);
		((Control)this).MouseDown += new MouseEventHandler(UCDevFan1_MouseDown);
		((Control)this).MouseMove += new MouseEventHandler(UCDevFan1_MouseMove);
		((Control)this).MouseUp += new MouseEventHandler(UCDevFan1_MouseUp);
		((Control)this).ResumeLayout(false);
	}
}
