using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VAIKYRIE.Properties;

namespace VAIKYRIE;

public class FormMain : Form
{
	public Form1 form;

	private IContainer components = null;

	public Label label1;

	public Label label2;

	public Label label3;

	public Label label4;

	public Label label5;

	public UCState1 ucState11;

	public UCState1 ucState12;

	public UCState1 ucState13;

	public UCState2 ucState21;

	public UCState3 ucState31;

	public UCState4 ucState41;

	public UCState4 ucState42;

	public UCState4 ucState43;

	public UCState4 ucState44;

	public UCState5 ucState51;

	public UCState5 ucState52;

	public UCState5 ucState53;

	public UCState5 ucState54;

	public UCState5 ucState55;

	public UCState5 ucState56;

	public UCState5 ucState57;

	public UCState5 ucState58;

	public UCState5 ucState59;

	public Label label6;

	public Label label7;

	public Label label8;

	public Label label9;

	public Label label10;

	public Label label11;

	public Label label12;

	public Label label13;

	public Label label14;

	public Label label15;

	public Label label16;

	public Label label17;

	public Label label18;

	public FormMain()
	{
		InitializeComponent();
	}

	private void FormMain_MouseDown(object sender, MouseEventArgs e)
	{
		form.Form1_MouseDown(this, e);
	}

	private void FormMain_MouseMove(object sender, MouseEventArgs e)
	{
		form.Form1_MouseMove(this, e);
	}

	private void FormMain_MouseUp(object sender, MouseEventArgs e)
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
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Expected O, but got Unknown
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Expected O, but got Unknown
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Expected O, but got Unknown
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Expected O, but got Unknown
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Expected O, but got Unknown
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Expected O, but got Unknown
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Expected O, but got Unknown
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Expected O, but got Unknown
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Expected O, but got Unknown
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Expected O, but got Unknown
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Expected O, but got Unknown
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Expected O, but got Unknown
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Expected O, but got Unknown
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Expected O, but got Unknown
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Expected O, but got Unknown
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected O, but got Unknown
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Expected O, but got Unknown
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Expected O, but got Unknown
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Expected O, but got Unknown
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Expected O, but got Unknown
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Expected O, but got Unknown
		//IL_050b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0590: Unknown result type (might be due to invalid IL or missing references)
		//IL_059a: Expected O, but got Unknown
		//IL_05ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0661: Unknown result type (might be due to invalid IL or missing references)
		//IL_066b: Expected O, but got Unknown
		//IL_069f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0721: Unknown result type (might be due to invalid IL or missing references)
		//IL_072b: Expected O, but got Unknown
		//IL_075f: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07eb: Expected O, but got Unknown
		//IL_081f: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bc: Expected O, but got Unknown
		//IL_08f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0972: Unknown result type (might be due to invalid IL or missing references)
		//IL_097c: Expected O, but got Unknown
		//IL_09b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3c: Expected O, but got Unknown
		//IL_0a70: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0afc: Expected O, but got Unknown
		//IL_0b30: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bbc: Expected O, but got Unknown
		//IL_0bf0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7c: Expected O, but got Unknown
		//IL_0cb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d3c: Expected O, but got Unknown
		//IL_0d70: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dfc: Expected O, but got Unknown
		//IL_0e30: Unknown result type (might be due to invalid IL or missing references)
		//IL_0eb2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ebc: Expected O, but got Unknown
		//IL_0ef0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f69: Expected O, but got Unknown
		//IL_0f8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f98: Expected O, but got Unknown
		//IL_0fbb: Unknown result type (might be due to invalid IL or missing references)
		//IL_101e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1028: Expected O, but got Unknown
		//IL_104d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1057: Expected O, but got Unknown
		//IL_107a: Unknown result type (might be due to invalid IL or missing references)
		//IL_10dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_10e7: Expected O, but got Unknown
		//IL_110c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1116: Expected O, but got Unknown
		//IL_1139: Unknown result type (might be due to invalid IL or missing references)
		//IL_119c: Unknown result type (might be due to invalid IL or missing references)
		//IL_11a6: Expected O, but got Unknown
		//IL_11cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_11d5: Expected O, but got Unknown
		//IL_11f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_125b: Unknown result type (might be due to invalid IL or missing references)
		//IL_1265: Expected O, but got Unknown
		//IL_128a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1294: Expected O, but got Unknown
		//IL_12b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_131a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1324: Expected O, but got Unknown
		//IL_1349: Unknown result type (might be due to invalid IL or missing references)
		//IL_1353: Expected O, but got Unknown
		//IL_1376: Unknown result type (might be due to invalid IL or missing references)
		//IL_13d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_13e3: Expected O, but got Unknown
		//IL_1408: Unknown result type (might be due to invalid IL or missing references)
		//IL_1412: Expected O, but got Unknown
		//IL_1435: Unknown result type (might be due to invalid IL or missing references)
		//IL_1498: Unknown result type (might be due to invalid IL or missing references)
		//IL_14a2: Expected O, but got Unknown
		//IL_14c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_14d1: Expected O, but got Unknown
		//IL_14f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1557: Unknown result type (might be due to invalid IL or missing references)
		//IL_1561: Expected O, but got Unknown
		//IL_1586: Unknown result type (might be due to invalid IL or missing references)
		//IL_1590: Expected O, but got Unknown
		//IL_15b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1616: Unknown result type (might be due to invalid IL or missing references)
		//IL_1620: Expected O, but got Unknown
		//IL_1645: Unknown result type (might be due to invalid IL or missing references)
		//IL_164f: Expected O, but got Unknown
		//IL_1672: Unknown result type (might be due to invalid IL or missing references)
		//IL_16d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_16dc: Expected O, but got Unknown
		//IL_1701: Unknown result type (might be due to invalid IL or missing references)
		//IL_170b: Expected O, but got Unknown
		//IL_172e: Unknown result type (might be due to invalid IL or missing references)
		//IL_178e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1798: Expected O, but got Unknown
		//IL_17bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_17c7: Expected O, but got Unknown
		//IL_17ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_184a: Unknown result type (might be due to invalid IL or missing references)
		//IL_1854: Expected O, but got Unknown
		//IL_1879: Unknown result type (might be due to invalid IL or missing references)
		//IL_1883: Expected O, but got Unknown
		//IL_18a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_1906: Unknown result type (might be due to invalid IL or missing references)
		//IL_1910: Expected O, but got Unknown
		//IL_1935: Unknown result type (might be due to invalid IL or missing references)
		//IL_193f: Expected O, but got Unknown
		//IL_1962: Unknown result type (might be due to invalid IL or missing references)
		//IL_19c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_19cd: Expected O, but got Unknown
		//IL_19f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_19fc: Expected O, but got Unknown
		//IL_1a1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aa4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1aae: Expected O, but got Unknown
		//IL_1ace: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b42: Unknown result type (might be due to invalid IL or missing references)
		//IL_1b4c: Expected O, but got Unknown
		//IL_1b6f: Unknown result type (might be due to invalid IL or missing references)
		//IL_1be3: Unknown result type (might be due to invalid IL or missing references)
		//IL_1bed: Expected O, but got Unknown
		//IL_1c0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f36: Expected O, but got Unknown
		//IL_1f7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f88: Expected O, but got Unknown
		//IL_1f91: Unknown result type (might be due to invalid IL or missing references)
		//IL_1f9b: Expected O, but got Unknown
		//IL_1fa4: Unknown result type (might be due to invalid IL or missing references)
		//IL_1fae: Expected O, but got Unknown
		ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(FormMain));
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
		label14 = new Label();
		label15 = new Label();
		label16 = new Label();
		label17 = new Label();
		label18 = new Label();
		ucState59 = new UCState5();
		ucState58 = new UCState5();
		ucState57 = new UCState5();
		ucState56 = new UCState5();
		ucState55 = new UCState5();
		ucState54 = new UCState5();
		ucState53 = new UCState5();
		ucState52 = new UCState5();
		ucState51 = new UCState5();
		ucState44 = new UCState4();
		ucState43 = new UCState4();
		ucState42 = new UCState4();
		ucState41 = new UCState4();
		ucState31 = new UCState3();
		ucState21 = new UCState2();
		ucState13 = new UCState1();
		ucState12 = new UCState1();
		ucState11 = new UCState1();
		((Control)this).SuspendLayout();
		((Control)label1).BackColor = Color.Transparent;
		label1.FlatStyle = (FlatStyle)0;
		((Control)label1).Font = new Font("微软雅黑", 24f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label1).ForeColor = Color.Black;
		((Control)label1).Location = new Point(49, 40);
		((Control)label1).Margin = new Padding(0);
		((Control)label1).Name = "label1";
		((Control)label1).Size = new Size(427, 50);
		((Control)label1).TabIndex = 23;
		label1.TextAlign = (ContentAlignment)16;
		((Control)label2).BackColor = Color.Transparent;
		label2.FlatStyle = (FlatStyle)0;
		((Control)label2).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label2).ForeColor = Color.Black;
		((Control)label2).Location = new Point(49, 90);
		((Control)label2).Margin = new Padding(0);
		((Control)label2).Name = "label2";
		((Control)label2).Size = new Size(427, 24);
		((Control)label2).TabIndex = 24;
		label2.TextAlign = (ContentAlignment)16;
		((Control)label3).BackColor = Color.Transparent;
		label3.FlatStyle = (FlatStyle)0;
		((Control)label3).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label3).ForeColor = Color.Black;
		((Control)label3).Location = new Point(49, 114);
		((Control)label3).Margin = new Padding(0);
		((Control)label3).Name = "label3";
		((Control)label3).Size = new Size(427, 24);
		((Control)label3).TabIndex = 25;
		label3.TextAlign = (ContentAlignment)16;
		((Control)label4).BackColor = Color.Transparent;
		label4.FlatStyle = (FlatStyle)0;
		((Control)label4).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label4).ForeColor = Color.Black;
		((Control)label4).Location = new Point(49, 138);
		((Control)label4).Margin = new Padding(0);
		((Control)label4).Name = "label4";
		((Control)label4).Size = new Size(427, 24);
		((Control)label4).TabIndex = 26;
		label4.TextAlign = (ContentAlignment)16;
		((Control)label5).BackColor = Color.Transparent;
		label5.FlatStyle = (FlatStyle)0;
		((Control)label5).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label5).ForeColor = Color.Black;
		((Control)label5).Location = new Point(49, 162);
		((Control)label5).Margin = new Padding(0);
		((Control)label5).Name = "label5";
		((Control)label5).Size = new Size(427, 24);
		((Control)label5).TabIndex = 27;
		label5.TextAlign = (ContentAlignment)16;
		((Control)label6).BackColor = Color.Transparent;
		label6.FlatStyle = (FlatStyle)0;
		((Control)label6).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label6).ForeColor = Color.White;
		((Control)label6).Location = new Point(312, 336);
		((Control)label6).Margin = new Padding(0);
		((Control)label6).Name = "label6";
		((Control)label6).Size = new Size(88, 24);
		((Control)label6).TabIndex = 46;
		((Control)label6).Text = "42";
		label6.TextAlign = (ContentAlignment)64;
		((Control)label7).BackColor = Color.Transparent;
		label7.FlatStyle = (FlatStyle)0;
		((Control)label7).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label7).ForeColor = Color.White;
		((Control)label7).Location = new Point(298, 379);
		((Control)label7).Margin = new Padding(0);
		((Control)label7).Name = "label7";
		((Control)label7).Size = new Size(88, 24);
		((Control)label7).TabIndex = 47;
		label7.TextAlign = (ContentAlignment)64;
		((Control)label8).BackColor = Color.Transparent;
		label8.FlatStyle = (FlatStyle)0;
		((Control)label8).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label8).ForeColor = Color.White;
		((Control)label8).Location = new Point(298, 421);
		((Control)label8).Margin = new Padding(0);
		((Control)label8).Name = "label8";
		((Control)label8).Size = new Size(88, 24);
		((Control)label8).TabIndex = 48;
		label8.TextAlign = (ContentAlignment)64;
		((Control)label9).BackColor = Color.Transparent;
		label9.FlatStyle = (FlatStyle)0;
		((Control)label9).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label9).ForeColor = Color.White;
		((Control)label9).Location = new Point(743, 360);
		((Control)label9).Margin = new Padding(0);
		((Control)label9).Name = "label9";
		((Control)label9).Size = new Size(88, 24);
		((Control)label9).TabIndex = 49;
		((Control)label9).Text = "42";
		label9.TextAlign = (ContentAlignment)64;
		((Control)label10).BackColor = Color.Transparent;
		label10.FlatStyle = (FlatStyle)0;
		((Control)label10).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label10).ForeColor = Color.White;
		((Control)label10).Location = new Point(728, 403);
		((Control)label10).Margin = new Padding(0);
		((Control)label10).Name = "label10";
		((Control)label10).Size = new Size(88, 24);
		((Control)label10).TabIndex = 50;
		label10.TextAlign = (ContentAlignment)64;
		((Control)label11).BackColor = Color.Transparent;
		label11.FlatStyle = (FlatStyle)0;
		((Control)label11).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label11).ForeColor = Color.White;
		((Control)label11).Location = new Point(728, 445);
		((Control)label11).Margin = new Padding(0);
		((Control)label11).Name = "label11";
		((Control)label11).Size = new Size(88, 24);
		((Control)label11).TabIndex = 51;
		label11.TextAlign = (ContentAlignment)64;
		((Control)label12).BackColor = Color.Transparent;
		label12.FlatStyle = (FlatStyle)0;
		((Control)label12).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label12).ForeColor = Color.White;
		((Control)label12).Location = new Point(315, 543);
		((Control)label12).Margin = new Padding(0);
		((Control)label12).Name = "label12";
		((Control)label12).Size = new Size(88, 24);
		((Control)label12).TabIndex = 52;
		label12.TextAlign = (ContentAlignment)64;
		((Control)label13).BackColor = Color.Transparent;
		label13.FlatStyle = (FlatStyle)0;
		((Control)label13).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label13).ForeColor = Color.White;
		((Control)label13).Location = new Point(315, 586);
		((Control)label13).Margin = new Padding(0);
		((Control)label13).Name = "label13";
		((Control)label13).Size = new Size(88, 24);
		((Control)label13).TabIndex = 53;
		label13.TextAlign = (ContentAlignment)64;
		((Control)label14).BackColor = Color.Transparent;
		label14.FlatStyle = (FlatStyle)0;
		((Control)label14).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label14).ForeColor = Color.White;
		((Control)label14).Location = new Point(315, 628);
		((Control)label14).Margin = new Padding(0);
		((Control)label14).Name = "label14";
		((Control)label14).Size = new Size(88, 24);
		((Control)label14).TabIndex = 54;
		label14.TextAlign = (ContentAlignment)64;
		((Control)label15).BackColor = Color.Transparent;
		label15.FlatStyle = (FlatStyle)0;
		((Control)label15).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label15).ForeColor = Color.White;
		((Control)label15).Location = new Point(770, 553);
		((Control)label15).Margin = new Padding(0);
		((Control)label15).Name = "label15";
		((Control)label15).Size = new Size(88, 24);
		((Control)label15).TabIndex = 55;
		label15.TextAlign = (ContentAlignment)64;
		((Control)label16).BackColor = Color.Transparent;
		label16.FlatStyle = (FlatStyle)0;
		((Control)label16).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label16).ForeColor = Color.White;
		((Control)label16).Location = new Point(770, 588);
		((Control)label16).Margin = new Padding(0);
		((Control)label16).Name = "label16";
		((Control)label16).Size = new Size(88, 24);
		((Control)label16).TabIndex = 56;
		label16.TextAlign = (ContentAlignment)64;
		((Control)label17).BackColor = Color.Transparent;
		label17.FlatStyle = (FlatStyle)0;
		((Control)label17).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label17).ForeColor = Color.White;
		((Control)label17).Location = new Point(770, 623);
		((Control)label17).Margin = new Padding(0);
		((Control)label17).Name = "label17";
		((Control)label17).Size = new Size(88, 24);
		((Control)label17).TabIndex = 57;
		label17.TextAlign = (ContentAlignment)64;
		((Control)label18).BackColor = Color.Transparent;
		label18.FlatStyle = (FlatStyle)0;
		((Control)label18).Font = new Font("微软雅黑", 9f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)label18).ForeColor = Color.White;
		((Control)label18).Location = new Point(770, 658);
		((Control)label18).Margin = new Padding(0);
		((Control)label18).Name = "label18";
		((Control)label18).Size = new Size(88, 24);
		((Control)label18).TabIndex = 58;
		label18.TextAlign = (ContentAlignment)64;
		((Control)ucState59).BackColor = Color.Transparent;
		((Control)ucState59).BackgroundImage = (Image)componentResourceManager.GetObject("ucState59.BackgroundImage");
		((Control)ucState59).BackgroundImageLayout = (ImageLayout)2;
		((Control)ucState59).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)ucState59).Location = new Point(250, 629);
		((Control)ucState59).Margin = new Padding(0);
		((Control)ucState59).Name = "ucState59";
		((Control)ucState59).Size = new Size(161, 5);
		((Control)ucState59).TabIndex = 45;
		((Control)ucState58).BackColor = Color.Transparent;
		((Control)ucState58).BackgroundImage = (Image)componentResourceManager.GetObject("ucState58.BackgroundImage");
		((Control)ucState58).BackgroundImageLayout = (ImageLayout)2;
		((Control)ucState58).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)ucState58).Location = new Point(250, 587);
		((Control)ucState58).Margin = new Padding(0);
		((Control)ucState58).Name = "ucState58";
		((Control)ucState58).Size = new Size(161, 5);
		((Control)ucState58).TabIndex = 44;
		((Control)ucState57).BackColor = Color.Transparent;
		((Control)ucState57).BackgroundImage = (Image)componentResourceManager.GetObject("ucState57.BackgroundImage");
		((Control)ucState57).BackgroundImageLayout = (ImageLayout)2;
		((Control)ucState57).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)ucState57).Location = new Point(250, 544);
		((Control)ucState57).Margin = new Padding(0);
		((Control)ucState57).Name = "ucState57";
		((Control)ucState57).Size = new Size(161, 5);
		((Control)ucState57).TabIndex = 43;
		((Control)ucState56).BackColor = Color.Transparent;
		((Control)ucState56).BackgroundImage = (Image)componentResourceManager.GetObject("ucState56.BackgroundImage");
		((Control)ucState56).BackgroundImageLayout = (ImageLayout)2;
		((Control)ucState56).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)ucState56).Location = new Point(681, 445);
		((Control)ucState56).Margin = new Padding(0);
		((Control)ucState56).Name = "ucState56";
		((Control)ucState56).Size = new Size(161, 5);
		((Control)ucState56).TabIndex = 42;
		((Control)ucState55).BackColor = Color.Transparent;
		((Control)ucState55).BackgroundImage = (Image)componentResourceManager.GetObject("ucState55.BackgroundImage");
		((Control)ucState55).BackgroundImageLayout = (ImageLayout)2;
		((Control)ucState55).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)ucState55).Location = new Point(681, 403);
		((Control)ucState55).Margin = new Padding(0);
		((Control)ucState55).Name = "ucState55";
		((Control)ucState55).Size = new Size(161, 5);
		((Control)ucState55).TabIndex = 41;
		((Control)ucState54).BackColor = Color.Transparent;
		((Control)ucState54).BackgroundImage = (Image)componentResourceManager.GetObject("ucState54.BackgroundImage");
		((Control)ucState54).BackgroundImageLayout = (ImageLayout)2;
		((Control)ucState54).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)ucState54).Location = new Point(681, 360);
		((Control)ucState54).Margin = new Padding(0);
		((Control)ucState54).Name = "ucState54";
		((Control)ucState54).Size = new Size(161, 5);
		((Control)ucState54).TabIndex = 40;
		((Control)ucState53).BackColor = Color.Transparent;
		((Control)ucState53).BackgroundImage = (Image)componentResourceManager.GetObject("ucState53.BackgroundImage");
		((Control)ucState53).BackgroundImageLayout = (ImageLayout)2;
		((Control)ucState53).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)ucState53).Location = new Point(250, 421);
		((Control)ucState53).Margin = new Padding(0);
		((Control)ucState53).Name = "ucState53";
		((Control)ucState53).Size = new Size(161, 5);
		((Control)ucState53).TabIndex = 39;
		((Control)ucState52).BackColor = Color.Transparent;
		((Control)ucState52).BackgroundImage = (Image)componentResourceManager.GetObject("ucState52.BackgroundImage");
		((Control)ucState52).BackgroundImageLayout = (ImageLayout)2;
		((Control)ucState52).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)ucState52).Location = new Point(250, 379);
		((Control)ucState52).Margin = new Padding(0);
		((Control)ucState52).Name = "ucState52";
		((Control)ucState52).Size = new Size(161, 5);
		((Control)ucState52).TabIndex = 38;
		((Control)ucState51).BackColor = Color.Transparent;
		((Control)ucState51).BackgroundImage = (Image)componentResourceManager.GetObject("ucState51.BackgroundImage");
		((Control)ucState51).BackgroundImageLayout = (ImageLayout)2;
		((Control)ucState51).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)ucState51).Location = new Point(250, 336);
		((Control)ucState51).Margin = new Padding(0);
		((Control)ucState51).Name = "ucState51";
		((Control)ucState51).Size = new Size(161, 5);
		((Control)ucState51).TabIndex = 37;
		((Control)ucState44).BackColor = Color.Transparent;
		((Control)ucState44).BackgroundImage = (Image)componentResourceManager.GetObject("ucState44.BackgroundImage");
		((Control)ucState44).BackgroundImageLayout = (ImageLayout)2;
		((Control)ucState44).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)ucState44).Location = new Point(693, 670);
		((Control)ucState44).Margin = new Padding(0);
		((Control)ucState44).Name = "ucState44";
		((Control)ucState44).Size = new Size(116, 5);
		((Control)ucState44).TabIndex = 36;
		((Control)ucState43).BackColor = Color.Transparent;
		((Control)ucState43).BackgroundImage = (Image)componentResourceManager.GetObject("ucState43.BackgroundImage");
		((Control)ucState43).BackgroundImageLayout = (ImageLayout)2;
		((Control)ucState43).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)ucState43).Location = new Point(693, 635);
		((Control)ucState43).Margin = new Padding(0);
		((Control)ucState43).Name = "ucState43";
		((Control)ucState43).Size = new Size(116, 5);
		((Control)ucState43).TabIndex = 35;
		((Control)ucState42).BackColor = Color.Transparent;
		((Control)ucState42).BackgroundImage = (Image)componentResourceManager.GetObject("ucState42.BackgroundImage");
		((Control)ucState42).BackgroundImageLayout = (ImageLayout)2;
		((Control)ucState42).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)ucState42).Location = new Point(693, 599);
		((Control)ucState42).Margin = new Padding(0);
		((Control)ucState42).Name = "ucState42";
		((Control)ucState42).Size = new Size(116, 5);
		((Control)ucState42).TabIndex = 34;
		((Control)ucState41).BackColor = Color.Transparent;
		((Control)ucState41).BackgroundImage = (Image)componentResourceManager.GetObject("ucState41.BackgroundImage");
		((Control)ucState41).BackgroundImageLayout = (ImageLayout)2;
		((Control)ucState41).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)ucState41).Location = new Point(693, 564);
		((Control)ucState41).Margin = new Padding(0);
		((Control)ucState41).Name = "ucState41";
		((Control)ucState41).Size = new Size(116, 5);
		((Control)ucState41).TabIndex = 33;
		((Control)ucState31).BackColor = Color.Transparent;
		((Control)ucState31).BackgroundImage = (Image)componentResourceManager.GetObject("ucState31.BackgroundImage");
		((Control)ucState31).BackgroundImageLayout = (ImageLayout)2;
		((Control)ucState31).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)ucState31).Location = new Point(584, 607);
		((Control)ucState31).Margin = new Padding(0);
		((Control)ucState31).Name = "ucState31";
		((Control)ucState31).Size = new Size(73, 75);
		((Control)ucState31).TabIndex = 32;
		((Control)ucState21).BackColor = Color.Transparent;
		((Control)ucState21).BackgroundImage = (Image)componentResourceManager.GetObject("ucState21.BackgroundImage");
		((Control)ucState21).BackgroundImageLayout = (ImageLayout)2;
		((Control)ucState21).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)ucState21).Location = new Point(504, 560);
		((Control)ucState21).Margin = new Padding(0);
		((Control)ucState21).Name = "ucState21";
		((Control)ucState21).Size = new Size(72, 74);
		((Control)ucState21).TabIndex = 31;
		((Control)ucState13).BackColor = Color.Transparent;
		((Control)ucState13).BackgroundImage = (Image)(object)Resources.Blh1;
		((Control)ucState13).BackgroundImageLayout = (ImageLayout)2;
		((Control)ucState13).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)ucState13).Location = new Point(64, 536);
		((Control)ucState13).Margin = new Padding(0);
		((Control)ucState13).Name = "ucState13";
		((Control)ucState13).Size = new Size(110, 110);
		((Control)ucState13).TabIndex = 30;
		((Control)ucState12).BackColor = Color.Transparent;
		((Control)ucState12).BackgroundImageLayout = (ImageLayout)2;
		((Control)ucState12).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)ucState12).Location = new Point(495, 352);
		((Control)ucState12).Margin = new Padding(0);
		((Control)ucState12).Name = "ucState12";
		((Control)ucState12).Size = new Size(110, 110);
		((Control)ucState12).TabIndex = 29;
		((Control)ucState11).BackColor = Color.Transparent;
		((Control)ucState11).BackgroundImageLayout = (ImageLayout)2;
		((Control)ucState11).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Control)ucState11).Location = new Point(64, 328);
		((Control)ucState11).Margin = new Padding(0);
		((Control)ucState11).Name = "ucState11";
		((Control)ucState11).Size = new Size(110, 110);
		((Control)ucState11).TabIndex = 28;
		((ContainerControl)this).AutoScaleMode = (AutoScaleMode)3;
		((Control)this).BackColor = Color.White;
		((Control)this).BackgroundImage = (Image)(object)Resources.Background1;
		((Control)this).BackgroundImageLayout = (ImageLayout)3;
		((Form)this).ClientSize = new Size(916, 800);
		((Control)this).Controls.Add((Control)(object)ucState59);
		((Control)this).Controls.Add((Control)(object)ucState58);
		((Control)this).Controls.Add((Control)(object)ucState57);
		((Control)this).Controls.Add((Control)(object)ucState56);
		((Control)this).Controls.Add((Control)(object)ucState55);
		((Control)this).Controls.Add((Control)(object)ucState54);
		((Control)this).Controls.Add((Control)(object)ucState53);
		((Control)this).Controls.Add((Control)(object)ucState52);
		((Control)this).Controls.Add((Control)(object)ucState51);
		((Control)this).Controls.Add((Control)(object)ucState44);
		((Control)this).Controls.Add((Control)(object)ucState43);
		((Control)this).Controls.Add((Control)(object)ucState42);
		((Control)this).Controls.Add((Control)(object)ucState41);
		((Control)this).Controls.Add((Control)(object)ucState31);
		((Control)this).Controls.Add((Control)(object)ucState21);
		((Control)this).Controls.Add((Control)(object)ucState13);
		((Control)this).Controls.Add((Control)(object)ucState12);
		((Control)this).Controls.Add((Control)(object)ucState11);
		((Control)this).Controls.Add((Control)(object)label5);
		((Control)this).Controls.Add((Control)(object)label4);
		((Control)this).Controls.Add((Control)(object)label3);
		((Control)this).Controls.Add((Control)(object)label2);
		((Control)this).Controls.Add((Control)(object)label1);
		((Control)this).Controls.Add((Control)(object)label6);
		((Control)this).Controls.Add((Control)(object)label7);
		((Control)this).Controls.Add((Control)(object)label8);
		((Control)this).Controls.Add((Control)(object)label9);
		((Control)this).Controls.Add((Control)(object)label10);
		((Control)this).Controls.Add((Control)(object)label11);
		((Control)this).Controls.Add((Control)(object)label12);
		((Control)this).Controls.Add((Control)(object)label14);
		((Control)this).Controls.Add((Control)(object)label13);
		((Control)this).Controls.Add((Control)(object)label15);
		((Control)this).Controls.Add((Control)(object)label16);
		((Control)this).Controls.Add((Control)(object)label17);
		((Control)this).Controls.Add((Control)(object)label18);
		((Control)this).DoubleBuffered = true;
		((Control)this).Font = new Font("微软雅黑", 10.5f, (FontStyle)0, (GraphicsUnit)3, (byte)134);
		((Form)this).FormBorderStyle = (FormBorderStyle)0;
		((Form)this).Location = new Point(89, 0);
		((Form)this).MaximizeBox = false;
		((Form)this).MinimizeBox = false;
		((Control)this).Name = "FormMain";
		((Control)this).Text = "FormMain";
		((Control)this).MouseDown += new MouseEventHandler(FormMain_MouseDown);
		((Control)this).MouseMove += new MouseEventHandler(FormMain_MouseMove);
		((Control)this).MouseUp += new MouseEventHandler(FormMain_MouseUp);
		((Control)this).ResumeLayout(false);
	}
}
