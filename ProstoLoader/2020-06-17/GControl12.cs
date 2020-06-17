using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

// Token: 0x02000019 RID: 25
public class GControl12 : Control
{
	// Token: 0x06000264 RID: 612 RVA: 0x00002FFD File Offset: 0x000011FD
	protected virtual void CreateHandle()
	{
		base.CreateHandle();
		GControl12.smethod_0(this, DockStyle.Bottom);
	}

	// Token: 0x06000265 RID: 613 RVA: 0x0000300C File Offset: 0x0000120C
	protected virtual void OnTextChanged(EventArgs e)
	{
		base.OnTextChanged(e);
		GControl12.smethod_1(this);
	}

	// Token: 0x17000041 RID: 65
	// (get) Token: 0x06000266 RID: 614 RVA: 0x0000301B File Offset: 0x0000121B
	// (set) Token: 0x06000267 RID: 615 RVA: 0x00003023 File Offset: 0x00001223
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

	// Token: 0x17000042 RID: 66
	// (get) Token: 0x06000268 RID: 616 RVA: 0x0000302C File Offset: 0x0000122C
	// (set) Token: 0x06000269 RID: 617 RVA: 0x00003034 File Offset: 0x00001234
	[Category("Colors")]
	public Color Color_1
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

	// Token: 0x17000043 RID: 67
	// (get) Token: 0x0600026A RID: 618 RVA: 0x0000303D File Offset: 0x0000123D
	// (set) Token: 0x0600026B RID: 619 RVA: 0x00003045 File Offset: 0x00001245
	[Category("Colors")]
	public Color Color_2
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

	// Token: 0x17000044 RID: 68
	// (get) Token: 0x0600026C RID: 620 RVA: 0x0000304E File Offset: 0x0000124E
	// (set) Token: 0x0600026D RID: 621 RVA: 0x00003056 File Offset: 0x00001256
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

	// Token: 0x0600026E RID: 622 RVA: 0x00007A84 File Offset: 0x00005C84
	public string method_0()
	{
		return string.Concat(new object[]
		{
			DateTime.Now.Date,
			" ",
			DateTime.Now.Hour,
			":",
			DateTime.Now.Minute
		});
	}

	// Token: 0x0600026F RID: 623 RVA: 0x00007AEC File Offset: 0x00005CEC
	public GControl12()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		GControl12.smethod_3(this, GControl12.smethod_2("Segoe UI", 8f));
		GControl12.smethod_4(this, Color.White);
		base.Size = new Size(GControl12.smethod_5(this), 20);
	}

	// Token: 0x06000270 RID: 624 RVA: 0x00007B6C File Offset: 0x00005D6C
	protected virtual void OnPaint(PaintEventArgs e)
	{
		this.method_1();
		Bitmap bitmap = GControl12.smethod_7(GControl12.smethod_5(this), GControl12.smethod_6(this));
		Graphics graphics = GControl12.smethod_8(bitmap);
		this.int_0 = GControl12.smethod_5(this);
		this.int_1 = GControl12.smethod_6(this);
		Rectangle rectangle_ = new Rectangle(0, 0, this.int_0, this.int_1);
		Graphics graphics2 = graphics;
		GControl12.smethod_9(graphics2, SmoothingMode.HighQuality);
		GControl12.smethod_10(graphics2, PixelOffsetMode.HighQuality);
		GControl12.smethod_11(graphics2, TextRenderingHint.ClearTypeGridFit);
		GControl12.smethod_12(graphics2, this.Color_0);
		GControl12.smethod_14(graphics2, GControl12.smethod_13(this.Color_0), rectangle_);
		graphics2.DrawString(GControl12.smethod_15(this), GControl12.smethod_16(this), GControl12.smethod_17(), new Rectangle(10, 4, this.int_0, this.int_1), GClass6.stringFormat_0);
		graphics2.FillRectangle(new SolidBrush(this.color_2), new Rectangle(4, 4, 4, 14));
		if (this.Boolean_0)
		{
			graphics2.DrawString(this.method_0(), this.Font, new SolidBrush(this.color_1), new Rectangle(-4, 2, this.int_0, this.int_1), new StringFormat
			{
				Alignment = StringAlignment.Far,
				LineAlignment = StringAlignment.Center
			});
		}
		base.OnPaint(e);
		graphics.Dispose();
		e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
		bitmap.Dispose();
	}

	// Token: 0x06000271 RID: 625 RVA: 0x00007CC8 File Offset: 0x00005EC8
	private void method_1()
	{
		GClass0 gclass = GClass6.smethod_3(this);
		this.color_2 = gclass.color_0;
	}

	// Token: 0x06000272 RID: 626 RVA: 0x0000305F File Offset: 0x0000125F
	static void smethod_0(Control control_0, DockStyle dockStyle_0)
	{
		control_0.Dock = dockStyle_0;
	}

	// Token: 0x06000273 RID: 627 RVA: 0x0000232F File Offset: 0x0000052F
	static void smethod_1(Control control_0)
	{
		control_0.Invalidate();
	}

	// Token: 0x06000274 RID: 628 RVA: 0x000025D1 File Offset: 0x000007D1
	static Font smethod_2(string string_0, float float_0)
	{
		return new Font(string_0, float_0);
	}

	// Token: 0x06000275 RID: 629 RVA: 0x000025DA File Offset: 0x000007DA
	static void smethod_3(Control control_0, Font font_0)
	{
		control_0.Font = font_0;
	}

	// Token: 0x06000276 RID: 630 RVA: 0x000028AF File Offset: 0x00000AAF
	static void smethod_4(Control control_0, Color color_3)
	{
		control_0.ForeColor = color_3;
	}

	// Token: 0x06000277 RID: 631 RVA: 0x0000237B File Offset: 0x0000057B
	static int smethod_5(Control control_0)
	{
		return control_0.Width;
	}

	// Token: 0x06000278 RID: 632 RVA: 0x00002383 File Offset: 0x00000583
	static int smethod_6(Control control_0)
	{
		return control_0.Height;
	}

	// Token: 0x06000279 RID: 633 RVA: 0x0000238B File Offset: 0x0000058B
	static Bitmap smethod_7(int int_2, int int_3)
	{
		return new Bitmap(int_2, int_3);
	}

	// Token: 0x0600027A RID: 634 RVA: 0x00002394 File Offset: 0x00000594
	static Graphics smethod_8(Image image_0)
	{
		return Graphics.FromImage(image_0);
	}

	// Token: 0x0600027B RID: 635 RVA: 0x0000239C File Offset: 0x0000059C
	static void smethod_9(Graphics graphics_0, SmoothingMode smoothingMode_0)
	{
		graphics_0.SmoothingMode = smoothingMode_0;
	}

	// Token: 0x0600027C RID: 636 RVA: 0x00002463 File Offset: 0x00000663
	static void smethod_10(Graphics graphics_0, PixelOffsetMode pixelOffsetMode_0)
	{
		graphics_0.PixelOffsetMode = pixelOffsetMode_0;
	}

	// Token: 0x0600027D RID: 637 RVA: 0x000023A5 File Offset: 0x000005A5
	static void smethod_11(Graphics graphics_0, TextRenderingHint textRenderingHint_0)
	{
		graphics_0.TextRenderingHint = textRenderingHint_0;
	}

	// Token: 0x0600027E RID: 638 RVA: 0x000023B6 File Offset: 0x000005B6
	static void smethod_12(Graphics graphics_0, Color color_3)
	{
		graphics_0.Clear(color_3);
	}

	// Token: 0x0600027F RID: 639 RVA: 0x000023BF File Offset: 0x000005BF
	static SolidBrush smethod_13(Color color_3)
	{
		return new SolidBrush(color_3);
	}

	// Token: 0x06000280 RID: 640 RVA: 0x000023C7 File Offset: 0x000005C7
	static void smethod_14(Graphics graphics_0, Brush brush_0, Rectangle rectangle_0)
	{
		graphics_0.FillRectangle(brush_0, rectangle_0);
	}

	// Token: 0x06000281 RID: 641 RVA: 0x00002476 File Offset: 0x00000676
	static string smethod_15(Control control_0)
	{
		return control_0.Text;
	}

	// Token: 0x06000282 RID: 642 RVA: 0x0000247E File Offset: 0x0000067E
	static Font smethod_16(Control control_0)
	{
		return control_0.Font;
	}

	// Token: 0x06000283 RID: 643 RVA: 0x00002896 File Offset: 0x00000A96
	static Brush smethod_17()
	{
		return Brushes.White;
	}

	// Token: 0x04000072 RID: 114
	private int int_0;

	// Token: 0x04000073 RID: 115
	private int int_1;

	// Token: 0x04000074 RID: 116
	private bool bool_0;

	// Token: 0x04000075 RID: 117
	private Color color_0 = Color.FromArgb(45, 47, 49);

	// Token: 0x04000076 RID: 118
	private Color color_1 = Color.White;

	// Token: 0x04000077 RID: 119
	private Color color_2 = GClass6.color_0;
}
