using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

// Token: 0x0200000F RID: 15
public class GControl5 : ContainerControl
{
	// Token: 0x17000028 RID: 40
	// (get) Token: 0x06000153 RID: 339 RVA: 0x00002A02 File Offset: 0x00000C02
	// (set) Token: 0x06000154 RID: 340 RVA: 0x00002A0A File Offset: 0x00000C0A
	[Category("Colors")]
	public Color Color_0
	{
		get
		{
			return this.color_0;
		}
		set
		{
			this.color_0 = value;
		}
	}

	// Token: 0x17000029 RID: 41
	// (get) Token: 0x06000155 RID: 341 RVA: 0x00002A13 File Offset: 0x00000C13
	// (set) Token: 0x06000156 RID: 342 RVA: 0x00002A1B File Offset: 0x00000C1B
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

	// Token: 0x06000157 RID: 343 RVA: 0x000063C8 File Offset: 0x000045C8
	public GControl5()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		GControl5.smethod_0(this, Color.Transparent);
		base.Size = new Size(240, 180);
		this.Font = new Font("Segoe ui", 10f);
	}

	// Token: 0x06000158 RID: 344 RVA: 0x00006448 File Offset: 0x00004648
	protected virtual void OnPaint(PaintEventArgs e)
	{
		this.method_0();
		Bitmap bitmap = GControl5.smethod_3(GControl5.smethod_1(this), GControl5.smethod_2(this));
		Graphics graphics = GControl5.smethod_4(bitmap);
		this.int_0 = GControl5.smethod_1(this) - 1;
		this.int_1 = GControl5.smethod_2(this) - 1;
		GraphicsPath graphicsPath_ = GControl5.smethod_5();
		GraphicsPath graphicsPath_2 = GControl5.smethod_5();
		GraphicsPath graphicsPath_3 = GControl5.smethod_5();
		Rectangle rectangle_ = new Rectangle(8, 8, this.int_0 - 16, this.int_1 - 16);
		Graphics graphics2 = graphics;
		GControl5.smethod_6(graphics2, SmoothingMode.HighQuality);
		GControl5.smethod_7(graphics2, PixelOffsetMode.HighQuality);
		GControl5.smethod_8(graphics2, TextRenderingHint.ClearTypeGridFit);
		GControl5.smethod_10(graphics2, GControl5.smethod_9(this));
		graphicsPath_ = GClass6.smethod_0(rectangle_, 8);
		GControl5.smethod_12(graphics2, GControl5.smethod_11(this.color_0), graphicsPath_);
		graphicsPath_2 = GClass6.smethod_2(28, 2, false);
		GControl5.smethod_12(graphics2, GControl5.smethod_11(this.color_0), graphicsPath_2);
		graphicsPath_3 = GClass6.smethod_2(28, 8, true);
		GControl5.smethod_12(graphics2, GControl5.smethod_11(Color.FromArgb(60, 70, 73)), graphicsPath_3);
		if (this.Boolean_0)
		{
			graphics2.DrawString(GControl5.smethod_13(this), GControl5.smethod_14(this), GControl5.smethod_11(this.color_1), new Rectangle(16, 16, this.int_0, this.int_1), GClass6.stringFormat_0);
		}
		base.OnPaint(e);
		graphics.Dispose();
		e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
		bitmap.Dispose();
	}

	// Token: 0x06000159 RID: 345 RVA: 0x000065B0 File Offset: 0x000047B0
	private void method_0()
	{
		GClass0 gclass = GClass6.smethod_3(this);
		this.color_1 = gclass.color_0;
	}

	// Token: 0x0600015A RID: 346 RVA: 0x00002372 File Offset: 0x00000572
	static void smethod_0(Control control_0, Color color_2)
	{
		control_0.BackColor = color_2;
	}

	// Token: 0x0600015B RID: 347 RVA: 0x0000237B File Offset: 0x0000057B
	static int smethod_1(Control control_0)
	{
		return control_0.Width;
	}

	// Token: 0x0600015C RID: 348 RVA: 0x00002383 File Offset: 0x00000583
	static int smethod_2(Control control_0)
	{
		return control_0.Height;
	}

	// Token: 0x0600015D RID: 349 RVA: 0x0000238B File Offset: 0x0000058B
	static Bitmap smethod_3(int int_2, int int_3)
	{
		return new Bitmap(int_2, int_3);
	}

	// Token: 0x0600015E RID: 350 RVA: 0x00002394 File Offset: 0x00000594
	static Graphics smethod_4(Image image_0)
	{
		return Graphics.FromImage(image_0);
	}

	// Token: 0x0600015F RID: 351 RVA: 0x0000245C File Offset: 0x0000065C
	static GraphicsPath smethod_5()
	{
		return new GraphicsPath();
	}

	// Token: 0x06000160 RID: 352 RVA: 0x0000239C File Offset: 0x0000059C
	static void smethod_6(Graphics graphics_0, SmoothingMode smoothingMode_0)
	{
		graphics_0.SmoothingMode = smoothingMode_0;
	}

	// Token: 0x06000161 RID: 353 RVA: 0x00002463 File Offset: 0x00000663
	static void smethod_7(Graphics graphics_0, PixelOffsetMode pixelOffsetMode_0)
	{
		graphics_0.PixelOffsetMode = pixelOffsetMode_0;
	}

	// Token: 0x06000162 RID: 354 RVA: 0x000023A5 File Offset: 0x000005A5
	static void smethod_8(Graphics graphics_0, TextRenderingHint textRenderingHint_0)
	{
		graphics_0.TextRenderingHint = textRenderingHint_0;
	}

	// Token: 0x06000163 RID: 355 RVA: 0x000023AE File Offset: 0x000005AE
	static Color smethod_9(Control control_0)
	{
		return control_0.BackColor;
	}

	// Token: 0x06000164 RID: 356 RVA: 0x000023B6 File Offset: 0x000005B6
	static void smethod_10(Graphics graphics_0, Color color_2)
	{
		graphics_0.Clear(color_2);
	}

	// Token: 0x06000165 RID: 357 RVA: 0x000023BF File Offset: 0x000005BF
	static SolidBrush smethod_11(Color color_2)
	{
		return new SolidBrush(color_2);
	}

	// Token: 0x06000166 RID: 358 RVA: 0x0000246C File Offset: 0x0000066C
	static void smethod_12(Graphics graphics_0, Brush brush_0, GraphicsPath graphicsPath_0)
	{
		graphics_0.FillPath(brush_0, graphicsPath_0);
	}

	// Token: 0x06000167 RID: 359 RVA: 0x00002476 File Offset: 0x00000676
	static string smethod_13(Control control_0)
	{
		return control_0.Text;
	}

	// Token: 0x06000168 RID: 360 RVA: 0x0000247E File Offset: 0x0000067E
	static Font smethod_14(Control control_0)
	{
		return control_0.Font;
	}

	// Token: 0x04000040 RID: 64
	private int int_0;

	// Token: 0x04000041 RID: 65
	private int int_1;

	// Token: 0x04000042 RID: 66
	private bool bool_0 = true;

	// Token: 0x04000043 RID: 67
	private Color color_0 = Color.FromArgb(60, 70, 73);

	// Token: 0x04000044 RID: 68
	private Color color_1 = GClass6.color_0;
}
