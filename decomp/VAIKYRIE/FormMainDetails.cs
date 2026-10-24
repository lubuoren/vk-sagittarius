using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using VAIKYRIE.Properties;

namespace VAIKYRIE;

public class FormMainDetails : Form
{
	public ArrayList drawArrayCPU;

	public ArrayList drawArrayGPU;

	private bool draw = true;

	public Form1 form;

	private IContainer components = null;

	public Label label1;

	public Label label2;

	public Label label3;

	public Label label4;

	public Label label5;

	public Label label6;

	public Label label7;

	public Label label8;

	public Label label9;

	public Label label10;

	public Label label11;

	public Label label12;

	public Label label13;

	public FormMainDetails()
	{
		InitializeComponent();
	}

	private void label9_MouseEnter(object sender, EventArgs e)
	{
		((Control)label9).Size = new Size(251, 147);
	}

	private void label9_MouseLeave(object sender, EventArgs e)
	{
		((Control)label9).Size = new Size(251, 62);
	}

	protected override void OnPaint(PaintEventArgs pe)
	{
		((Form)this).OnPaint(pe);
		Graphics graphics = pe.Graphics;
		if (draw)
		{
			draw = false;
			graphics.SmoothingMode = (SmoothingMode)4;
			DrawVectorgraphItemVal(graphics, drawArrayCPU, Color.FromArgb(208, 151, 66));
			DrawVectorgraphItemVal(graphics, drawArrayGPU, Color.FromArgb(73, 104, 119));
			draw = true;
		}
	}

	private void DrawVectorgraphItemVal(Graphics graphics, ArrayList array, Color color)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		if (array.Count == 0)
		{
			return;
		}
		int count = array.Count;
		Pen val = new Pen(color, 1f);
		float num = 70f;
		float num2 = 352f;
		double num3 = 0.0;
		double num4 = 2.15;
		double num5 = 2.6;
		Point point = default(Point);
		Point point2 = default(Point);
		for (int i = 0; i < count - 1; i++)
		{
			point.X = (int)((double)num + num3);
			point2.X = (int)((double)num + num3 + num4);
			num3 += num4;
			if (array[i] != null)
			{
				point.Y = (int)((double)num2 - (double)(float)array[i] * num5);
				if (array[i + 1] != null)
				{
					point2.Y = (int)((double)num2 - (double)(float)array[i + 1] * num5);
					graphics.DrawLine(val, point, point2);
				}
			}
		}
		val.Dispose();
	}

	private void FormMainDetails_MouseDown(object sender, MouseEventArgs e)
	{
		form.Form1_MouseDown(this, e);
	}

	private void FormMainDetails_MouseMove(object sender, MouseEventArgs e)
	{
		form.Form1_MouseMove(this, e);
	}

	private void FormMainDetails_MouseUp(object sender, MouseEventArgs e)
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
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Expected O, but got Unknown
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Expected O, but got Unknown
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Expected O, but got Unknown
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Expected O, but got Unknown
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Expected O, but got Unknown
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Expected O, but got Unknown
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Expected O, but got Unknown
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Expected O, but got Unknown
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Expected O, but got Unknown
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Expected O, but got Unknown
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Expected O, but got Unknown
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Expected O, but got Unknown
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Expected O, but got Unknown
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Expected O, but got Unknown
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Expected O, but got Unknown
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Expected O, but got Unknown
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Expected O, but got Unknown
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0558: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Expected O, but got Unknown
		//IL_0593: Unknown result type (might be due to invalid IL or missing references)
		//IL_0618: Unknown result type (might be due to invalid IL or missing references)
		//IL_0622: Expected O, but got Unknown
		//IL_0653: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e2: Expected O, but got Unknown
		//IL_0716: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c7: Expected O, but got Unknown
		//IL_07fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0872: Unknown result type (might be due to invalid IL or missing references)
		//IL_087c: Expected O, but got Unknown
		//IL_08ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_092f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0939: Expected O, but got Unknown
		//IL_096d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0a: Expected O, but got Unknown
		//IL_0a3e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bcd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd7: Expected O, but got Unknown
		//IL_0c1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c29: Expected O, but got Unknown
		//IL_0c32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c3c: Expected O, but got Unknown
		//IL_0c45: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4f: Expected O, but got Unknown
		label1 = new Label();
		label2 = new Label();
		label3 = new Label();
		label4 = new Label();
		label5 = new Label();
		label6 = new Label();
		label7 = new Label();
		label8 = new Label();
		label9 = new Label();
		label10 = new Label();
		label11 = new Label();
		label12 = new Label();
		label13 = new Label();
		((Control)this).SuspendLayout();
		((Control)label1).BackColor = Color.Transparent;
		label1.FlatStyle = (FlatStyle)0;
		((Control)label1).Font = new Font("微软雅黑", 7.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label1).ForeColor = Color.Gray;
		((Control)label1).Location = new Point(36, 504);
		((Control)label1).Margin = new Padding(0);
		((Control)label1).Name = "label1";
		((Control)label1).Size = new Size(251, 16);
		((Control)label1).TabIndex = 25;
		label1.TextAlign = (ContentAlignment)16;
		((Control)label2).BackColor = Color.Transparent;
		label2.FlatStyle = (FlatStyle)0;
		((Control)label2).Font = new Font("微软雅黑", 7.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label2).ForeColor = Color.Gray;
		((Control)label2).Location = new Point(36, 522);
		((Control)label2).Margin = new Padding(0);
		((Control)label2).Name = "label2";
		((Control)label2).Size = new Size(52, 16);
		((Control)label2).TabIndex = 26;
		((Control)label2).Text = "当前频率 ";
		label2.TextAlign = (ContentAlignment)16;
		((Control)label3).BackColor = Color.Transparent;
		label3.FlatStyle = (FlatStyle)0;
		((Control)label3).Font = new Font("微软雅黑", 7.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label3).ForeColor = Color.Gray;
		((Control)label3).Location = new Point(333, 504);
		((Control)label3).Margin = new Padding(0);
		((Control)label3).Name = "label3";
		((Control)label3).Size = new Size(251, 16);
		((Control)label3).TabIndex = 27;
		label3.TextAlign = (ContentAlignment)16;
		((Control)label4).BackColor = Color.Transparent;
		label4.FlatStyle = (FlatStyle)0;
		((Control)label4).Font = new Font("微软雅黑", 7.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label4).ForeColor = Color.Gray;
		((Control)label4).Location = new Point(333, 522);
		((Control)label4).Margin = new Padding(0);
		((Control)label4).Name = "label4";
		((Control)label4).Size = new Size(251, 16);
		((Control)label4).TabIndex = 28;
		label4.TextAlign = (ContentAlignment)16;
		((Control)label5).BackColor = Color.Transparent;
		label5.FlatStyle = (FlatStyle)0;
		((Control)label5).Font = new Font("微软雅黑", 7.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label5).ForeColor = Color.Gray;
		((Control)label5).Location = new Point(630, 504);
		((Control)label5).Margin = new Padding(0);
		((Control)label5).Name = "label5";
		((Control)label5).Size = new Size(251, 16);
		((Control)label5).TabIndex = 29;
		label5.TextAlign = (ContentAlignment)16;
		((Control)label6).BackColor = Color.Transparent;
		label6.FlatStyle = (FlatStyle)0;
		((Control)label6).Font = new Font("微软雅黑", 7.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label6).ForeColor = Color.Gray;
		((Control)label6).Location = new Point(630, 522);
		((Control)label6).Margin = new Padding(0);
		((Control)label6).Name = "label6";
		((Control)label6).Size = new Size(251, 52);
		((Control)label6).TabIndex = 30;
		((Control)label7).BackColor = Color.Transparent;
		label7.FlatStyle = (FlatStyle)0;
		((Control)label7).Font = new Font("微软雅黑", 7.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label7).ForeColor = Color.Gray;
		((Control)label7).Location = new Point(36, 626);
		((Control)label7).Margin = new Padding(0);
		((Control)label7).Name = "label7";
		((Control)label7).Size = new Size(251, 16);
		((Control)label7).TabIndex = 31;
		label7.TextAlign = (ContentAlignment)16;
		((Control)label8).BackColor = Color.Transparent;
		label8.FlatStyle = (FlatStyle)0;
		((Control)label8).Font = new Font("微软雅黑", 7.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label8).ForeColor = Color.Gray;
		((Control)label8).Location = new Point(36, 644);
		((Control)label8).Margin = new Padding(0);
		((Control)label8).Name = "label8";
		((Control)label8).Size = new Size(251, 16);
		((Control)label8).TabIndex = 32;
		label8.TextAlign = (ContentAlignment)16;
		((Control)label9).BackColor = Color.Transparent;
		label9.FlatStyle = (FlatStyle)0;
		((Control)label9).Font = new Font("微软雅黑", 7.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label9).ForeColor = Color.Gray;
		((Control)label9).Location = new Point(333, 624);
		((Control)label9).Margin = new Padding(0);
		((Control)label9).Name = "label9";
		((Control)label9).Size = new Size(251, 62);
		((Control)label9).TabIndex = 33;
		((Control)label9).MouseEnter += label9_MouseEnter;
		((Control)label9).MouseLeave += label9_MouseLeave;
		((Control)label10).BackColor = Color.Transparent;
		label10.FlatStyle = (FlatStyle)0;
		((Control)label10).Font = new Font("微软雅黑", 7.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label10).ForeColor = Color.Gray;
		((Control)label10).Location = new Point(630, 624);
		((Control)label10).Margin = new Padding(0);
		((Control)label10).Name = "label10";
		((Control)label10).Size = new Size(251, 99);
		((Control)label10).TabIndex = 34;
		((Control)label11).BackColor = Color.Transparent;
		label11.FlatStyle = (FlatStyle)0;
		((Control)label11).Font = new Font("微软雅黑", 7.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label11).ForeColor = Color.SteelBlue;
		((Control)label11).Location = new Point(84, 522);
		((Control)label11).Margin = new Padding(0);
		((Control)label11).Name = "label11";
		((Control)label11).Size = new Size(68, 16);
		((Control)label11).TabIndex = 35;
		label11.TextAlign = (ContentAlignment)16;
		((Control)label12).BackColor = Color.Transparent;
		label12.FlatStyle = (FlatStyle)0;
		((Control)label12).Font = new Font("微软雅黑", 7.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label12).ForeColor = Color.Gray;
		((Control)label12).Location = new Point(156, 522);
		((Control)label12).Margin = new Padding(0);
		((Control)label12).Name = "label12";
		((Control)label12).Size = new Size(52, 16);
		((Control)label12).TabIndex = 36;
		((Control)label12).Text = "当前电压 ";
		label12.TextAlign = (ContentAlignment)16;
		((Control)label13).BackColor = Color.Transparent;
		label13.FlatStyle = (FlatStyle)0;
		((Control)label13).Font = new Font("微软雅黑", 7.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label13).ForeColor = Color.SteelBlue;
		((Control)label13).Location = new Point(204, 522);
		((Control)label13).Margin = new Padding(0);
		((Control)label13).Name = "label13";
		((Control)label13).Size = new Size(68, 16);
		((Control)label13).TabIndex = 37;
		label13.TextAlign = (ContentAlignment)16;
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)3;
		((Control)this).BackColor = Color.White;
		((Control)this).BackgroundImage = (Image)(object)Resources.Background2_1_New;
		((Control)this).BackgroundImageLayout = (ImageLayout)3;
		((Form)this).ClientSize = new Size(916, 800);
		((Control)this).Controls.Add((Control)(object)label13);
		((Control)this).Controls.Add((Control)(object)label12);
		((Control)this).Controls.Add((Control)(object)label11);
		((Control)this).Controls.Add((Control)(object)label10);
		((Control)this).Controls.Add((Control)(object)label9);
		((Control)this).Controls.Add((Control)(object)label8);
		((Control)this).Controls.Add((Control)(object)label7);
		((Control)this).Controls.Add((Control)(object)label6);
		((Control)this).Controls.Add((Control)(object)label5);
		((Control)this).Controls.Add((Control)(object)label4);
		((Control)this).Controls.Add((Control)(object)label3);
		((Control)this).Controls.Add((Control)(object)label2);
		((Control)this).Controls.Add((Control)(object)label1);
		((Control)this).DoubleBuffered = true;
		((Control)this).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Form)this).FormBorderStyle = (FormBorderStyle)0;
		((Form)this).Location = new Point(89, 0);
		((Form)this).MaximizeBox = false;
		((Form)this).MinimizeBox = false;
		((Control)this).Name = "FormMainDetails";
		((Control)this).Text = "FormMainDetails";
		((Control)this).MouseDown += new MouseEventHandler(FormMainDetails_MouseDown);
		((Control)this).MouseMove += new MouseEventHandler(FormMainDetails_MouseMove);
		((Control)this).MouseUp += new MouseEventHandler(FormMainDetails_MouseUp);
		((Control)this).ResumeLayout(false);
	}
}
