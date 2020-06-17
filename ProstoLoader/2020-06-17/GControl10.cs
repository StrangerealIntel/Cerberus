using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

// Token: 0x02000015 RID: 21
public class GControl10 : Control
{
	// Token: 0x17000038 RID: 56
	// (get) Token: 0x06000211 RID: 529 RVA: 0x00002E05 File Offset: 0x00001005
	// (set) Token: 0x06000212 RID: 530 RVA: 0x00002E0D File Offset: 0x0000100D
	[Category("Control")]
	public int Int32_0
	{
		get
		{
			return this.int_3;
		}
		set
		{
			if (value < this.int_2)
			{
				this.int_2 = value;
			}
			this.int_3 = value;
			GControl10.smethod_0(this);
		}
	}

	// Token: 0x17000039 RID: 57
	// (get) Token: 0x06000213 RID: 531 RVA: 0x00002E2C File Offset: 0x0000102C
	// (set) Token: 0x06000214 RID: 532 RVA: 0x00002E34 File Offset: 0x00001034
	[Category("Control")]
	public int Int32_1
	{
		get
		{
			return this.int_2;
		}
		set
		{
			if (value > this.int_3)
			{
				value = this.int_3;
				GControl10.smethod_0(this);
			}
			this.int_2 = value;
			GControl10.smethod_0(this);
		}
	}

	// Token: 0x1700003A RID: 58
	// (get) Token: 0x06000215 RID: 533 RVA: 0x00002E5A File Offset: 0x0000105A
	// (set) Token: 0x06000216 RID: 534 RVA: 0x00002E62 File Offset: 0x00001062
	public bool Boolean_0
	{
		get
		{
			return this.bool_0;
		}
		set
		{
			this.bool_0 = value;
		}
	}

	// Token: 0x1700003B RID: 59
	// (get) Token: 0x06000217 RID: 535 RVA: 0x00002E6B File Offset: 0x0000106B
	// (set) Token: 0x06000218 RID: 536 RVA: 0x00002E73 File Offset: 0x00001073
	public bool Boolean_1
	{
		get
		{
			return this.bool_1;
		}
		set
		{
			this.bool_1 = value;
		}
	}

	// Token: 0x1700003C RID: 60
	// (get) Token: 0x06000219 RID: 537 RVA: 0x00002E7C File Offset: 0x0000107C
	// (set) Token: 0x0600021A RID: 538 RVA: 0x00002E84 File Offset: 0x00001084
	public bool Boolean_2
	{
		get
		{
			return this.bool_2;
		}
		set
		{
			this.bool_2 = value;
		}
	}

	// Token: 0x1700003D RID: 61
	// (get) Token: 0x0600021B RID: 539 RVA: 0x00002E8D File Offset: 0x0000108D
	// (set) Token: 0x0600021C RID: 540 RVA: 0x00002E95 File Offset: 0x00001095
	[Category("Colors")]
	public Color Color_0
	{
		get
		{
			return this.color_1;
		}
		set
		{
			this.color_1 = value;
		}
	}

	// Token: 0x1700003E RID: 62
	// (get) Token: 0x0600021D RID: 541 RVA: 0x00002E9E File Offset: 0x0000109E
	// (set) Token: 0x0600021E RID: 542 RVA: 0x00002EA6 File Offset: 0x000010A6
	[Category("Colors")]
	public Color Color_1
	{
		get
		{
			return this.color_2;
		}
		set
		{
			this.color_2 = value;
		}
	}

	// Token: 0x0600021F RID: 543 RVA: 0x00002EAF File Offset: 0x000010AF
	protected virtual void OnResize(EventArgs e)
	{
		base.OnResize(e);
		GControl10.smethod_1(this, 42);
	}

	// Token: 0x06000220 RID: 544 RVA: 0x00002EC0 File Offset: 0x000010C0
	protected virtual void CreateHandle()
	{
		base.CreateHandle();
		GControl10.smethod_1(this, 42);
	}

	// Token: 0x06000221 RID: 545 RVA: 0x00002ED0 File Offset: 0x000010D0
	public void method_0(int int_4)
	{
		this.Int32_1 += int_4;
	}

	// Token: 0x06000222 RID: 546 RVA: 0x0000731C File Offset: 0x0000551C
	public GControl10()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		GControl10.smethod_2(this, Color.FromArgb(60, 70, 73));
		GControl10.smethod_1(this, 42);
	}

	// Token: 0x06000223 RID: 547 RVA: 0x000073A4 File Offset: 0x000055A4
	protected virtual void OnPaint(PaintEventArgs e)
	{
		this.method_1();
		Bitmap bitmap = GControl10.smethod_5(GControl10.smethod_3(this), GControl10.smethod_4(this));
		Graphics graphics = GControl10.smethod_6(bitmap);
		this.int_0 = GControl10.smethod_3(this) - 1;
		this.int_1 = GControl10.smethod_4(this) - 1;
		Rectangle rectangle = new Rectangle(0, 24, this.int_0, this.int_1);
		GraphicsPath graphicsPath = GControl10.smethod_7();
		GraphicsPath path = GControl10.smethod_7();
		GraphicsPath path2 = GControl10.smethod_7();
		Graphics graphics2 = graphics;
		GControl10.smethod_8(graphics2, SmoothingMode.HighQuality);
		GControl10.smethod_9(graphics2, PixelOffsetMode.HighQuality);
		GControl10.smethod_10(graphics2, TextRenderingHint.ClearTypeGridFit);
		GControl10.smethod_12(graphics2, GControl10.smethod_11(this));
		int num = (int)((float)this.int_2 / (float)this.int_3 * (float)GControl10.smethod_3(this));
		int int32_ = this.Int32_1;
		if (int32_ == 0)
		{
			GControl10.smethod_14(graphics2, GControl10.smethod_13(this.color_0), rectangle);
			graphics2.FillRectangle(GControl10.smethod_13(this.color_1), new Rectangle(0, 24, num - 1, this.int_1 - 1));
		}
		else if (int32_ == 100)
		{
			graphics2.FillRectangle(new SolidBrush(this.color_0), rectangle);
			graphics2.FillRectangle(new SolidBrush(this.color_1), new Rectangle(0, 24, num - 1, this.int_1 - 1));
		}
		else
		{
			graphics2.FillRectangle(new SolidBrush(this.color_0), rectangle);
			graphicsPath.AddRectangle(new Rectangle(0, 24, num - 1, this.int_1 - 1));
			graphics2.FillPath(new SolidBrush(this.color_1), graphicsPath);
			if (this.bool_0)
			{
				HatchBrush brush = new HatchBrush(HatchStyle.Plaid, this.color_2, this.color_1);
				graphics2.FillRectangle(brush, new Rectangle(0, 24, num - 1, this.int_1 - 1));
			}
			if (this.bool_1)
			{
				path = GClass6.smethod_0(new Rectangle(num - 18, 0, 34, 16), 4);
				graphics2.FillPath(new SolidBrush(this.color_0), path);
				path2 = GClass6.smethod_2(num - 9, 16, true);
				graphics2.FillPath(new SolidBrush(this.color_0), path2);
				string s = this.bool_2 ? (this.Int32_1.ToString() + "%") : this.Int32_1.ToString();
				int x = this.bool_2 ? (num - 15) : (num - 11);
				graphics2.DrawString(s, new Font("Segoe UI", 10f), new SolidBrush(this.color_1), new Rectangle(x, -2, this.int_0, this.int_1), GClass6.stringFormat_0);
			}
		}
		base.OnPaint(e);
		graphics.Dispose();
		e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
		bitmap.Dispose();
	}

	// Token: 0x06000224 RID: 548 RVA: 0x00007674 File Offset: 0x00005874
	private void method_1()
	{
		GClass0 gclass = GClass6.smethod_3(this);
		this.color_1 = gclass.color_0;
	}

	// Token: 0x06000225 RID: 549 RVA: 0x0000232F File Offset: 0x0000052F
	static void smethod_0(Control control_0)
	{
		control_0.Invalidate();
	}

	// Token: 0x06000226 RID: 550 RVA: 0x00002337 File Offset: 0x00000537
	static void smethod_1(Control control_0, int int_4)
	{
		control_0.Height = int_4;
	}

	// Token: 0x06000227 RID: 551 RVA: 0x00002372 File Offset: 0x00000572
	static void smethod_2(Control control_0, Color color_3)
	{
		control_0.BackColor = color_3;
	}

	// Token: 0x06000228 RID: 552 RVA: 0x0000237B File Offset: 0x0000057B
	static int smethod_3(Control control_0)
	{
		return control_0.Width;
	}

	// Token: 0x06000229 RID: 553 RVA: 0x00002383 File Offset: 0x00000583
	static int smethod_4(Control control_0)
	{
		return control_0.Height;
	}

	// Token: 0x0600022A RID: 554 RVA: 0x0000238B File Offset: 0x0000058B
	static Bitmap smethod_5(int int_4, int int_5)
	{
		return new Bitmap(int_4, int_5);
	}

	// Token: 0x0600022B RID: 555 RVA: 0x00002394 File Offset: 0x00000594
	static Graphics smethod_6(Image image_0)
	{
		return Graphics.FromImage(image_0);
	}

	// Token: 0x0600022C RID: 556 RVA: 0x0000245C File Offset: 0x0000065C
	static GraphicsPath smethod_7()
	{
		return new GraphicsPath();
	}

	// Token: 0x0600022D RID: 557 RVA: 0x0000239C File Offset: 0x0000059C
	static void smethod_8(Graphics graphics_0, SmoothingMode smoothingMode_0)
	{
		graphics_0.SmoothingMode = smoothingMode_0;
	}

	// Token: 0x0600022E RID: 558 RVA: 0x00002463 File Offset: 0x00000663
	static void smethod_9(Graphics graphics_0, PixelOffsetMode pixelOffsetMode_0)
	{
		graphics_0.PixelOffsetMode = pixelOffsetMode_0;
	}

	// Token: 0x0600022F RID: 559 RVA: 0x000023A5 File Offset: 0x000005A5
	static void smethod_10(Graphics graphics_0, TextRenderingHint textRenderingHint_0)
	{
		graphics_0.TextRenderingHint = textRenderingHint_0;
	}

	// Token: 0x06000230 RID: 560 RVA: 0x000023AE File Offset: 0x000005AE
	static Color smethod_11(Control control_0)
	{
		return control_0.BackColor;
	}

	// Token: 0x06000231 RID: 561 RVA: 0x000023B6 File Offset: 0x000005B6
	static void smethod_12(Graphics graphics_0, Color color_3)
	{
		graphics_0.Clear(color_3);
	}

	// Token: 0x06000232 RID: 562 RVA: 0x000023BF File Offset: 0x000005BF
	static SolidBrush smethod_13(Color color_3)
	{
		return new SolidBrush(color_3);
	}

	// Token: 0x06000233 RID: 563 RVA: 0x000023C7 File Offset: 0x000005C7
	static void smethod_14(Graphics graphics_0, Brush brush_0, Rectangle rectangle_0)
	{
		graphics_0.FillRectangle(brush_0, rectangle_0);
	}

	// Token: 0x0400005C RID: 92
	private int int_0;

	// Token: 0x0400005D RID: 93
	private int int_1;

	// Token: 0x0400005E RID: 94
	private int int_2;

	// Token: 0x0400005F RID: 95
	private int int_3 = 100;

	// Token: 0x04000060 RID: 96
	private bool bool_0 = true;

	// Token: 0x04000061 RID: 97
	private bool bool_1 = true;

	// Token: 0x04000062 RID: 98
	private bool bool_2;

	// Token: 0x04000063 RID: 99
	private Color color_0 = Color.FromArgb(45, 47, 49);

	// Token: 0x04000064 RID: 100
	private Color color_1 = GClass6.color_0;

	// Token: 0x04000065 RID: 101
	private Color color_2 = Color.FromArgb(23, 148, 92);
}
