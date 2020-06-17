using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

// Token: 0x02000005 RID: 5
public class GControl1 : Control
{
	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000067 RID: 103 RVA: 0x000023D1 File Offset: 0x000005D1
	// (set) Token: 0x06000068 RID: 104 RVA: 0x000023D9 File Offset: 0x000005D9
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

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x06000069 RID: 105 RVA: 0x000023E2 File Offset: 0x000005E2
	// (set) Token: 0x0600006A RID: 106 RVA: 0x000023EA File Offset: 0x000005EA
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

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x0600006B RID: 107 RVA: 0x000023F3 File Offset: 0x000005F3
	// (set) Token: 0x0600006C RID: 108 RVA: 0x000023FB File Offset: 0x000005FB
	[Category("Options")]
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

	// Token: 0x0600006D RID: 109 RVA: 0x00002404 File Offset: 0x00000604
	protected virtual void OnMouseDown(MouseEventArgs e)
	{
		base.OnMouseDown(e);
		this.genum5_0 = GEnum5.Down;
		GControl1.smethod_0(this);
	}

	// Token: 0x0600006E RID: 110 RVA: 0x0000241A File Offset: 0x0000061A
	protected virtual void OnMouseUp(MouseEventArgs e)
	{
		base.OnMouseUp(e);
		this.genum5_0 = GEnum5.Over;
		GControl1.smethod_0(this);
	}

	// Token: 0x0600006F RID: 111 RVA: 0x00002430 File Offset: 0x00000630
	protected virtual void OnMouseEnter(EventArgs e)
	{
		base.OnMouseEnter(e);
		this.genum5_0 = GEnum5.Over;
		GControl1.smethod_0(this);
	}

	// Token: 0x06000070 RID: 112 RVA: 0x00002446 File Offset: 0x00000646
	protected virtual void OnMouseLeave(EventArgs e)
	{
		base.OnMouseLeave(e);
		this.genum5_0 = GEnum5.None;
		GControl1.smethod_0(this);
	}

	// Token: 0x06000071 RID: 113 RVA: 0x000052D4 File Offset: 0x000034D4
	public GControl1()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		base.Size = new Size(106, 32);
		this.BackColor = Color.Transparent;
		this.Font = new Font("Segoe UI", 12f);
		this.Cursor = Cursors.Hand;
	}

	// Token: 0x06000072 RID: 114 RVA: 0x0000535C File Offset: 0x0000355C
	protected virtual void OnPaint(PaintEventArgs e)
	{
		this.method_0();
		Bitmap image_ = GControl1.smethod_3(GControl1.smethod_1(this), GControl1.smethod_2(this));
		Graphics graphics = GControl1.smethod_4(image_);
		this.int_0 = GControl1.smethod_1(this) - 1;
		this.int_1 = GControl1.smethod_2(this) - 1;
		GraphicsPath graphicsPath_ = GControl1.smethod_5();
		Rectangle rectangle = new Rectangle(0, 0, this.int_0, this.int_1);
		Graphics graphics_ = graphics;
		GControl1.smethod_6(graphics_, SmoothingMode.HighQuality);
		GControl1.smethod_7(graphics_, PixelOffsetMode.HighQuality);
		GControl1.smethod_8(graphics_, TextRenderingHint.ClearTypeGridFit);
		GControl1.smethod_10(graphics_, GControl1.smethod_9(this));
		switch (this.genum5_0)
		{
		case GEnum5.None:
			if (!this.Boolean_0)
			{
				GControl1.smethod_16(graphics_, GControl1.smethod_11(this.color_0), rectangle);
				GControl1.smethod_15(graphics_, GControl1.smethod_13(this), GControl1.smethod_14(this), GControl1.smethod_11(this.color_1), rectangle, GClass6.stringFormat_1);
			}
			else
			{
				graphicsPath_ = GClass6.smethod_0(rectangle, 6);
				GControl1.smethod_12(graphics_, GControl1.smethod_11(this.color_0), graphicsPath_);
				GControl1.smethod_15(graphics_, GControl1.smethod_13(this), GControl1.smethod_14(this), GControl1.smethod_11(this.color_1), rectangle, GClass6.stringFormat_1);
			}
			break;
		case GEnum5.Over:
			if (this.Boolean_0)
			{
				graphicsPath_ = GClass6.smethod_0(rectangle, 6);
				GControl1.smethod_12(graphics_, GControl1.smethod_11(this.color_0), graphicsPath_);
				GControl1.smethod_12(graphics_, GControl1.smethod_11(Color.FromArgb(20, Color.White)), graphicsPath_);
				GControl1.smethod_15(graphics_, GControl1.smethod_13(this), GControl1.smethod_14(this), GControl1.smethod_11(this.color_1), rectangle, GClass6.stringFormat_1);
			}
			else
			{
				GControl1.smethod_16(graphics_, GControl1.smethod_11(this.color_0), rectangle);
				GControl1.smethod_16(graphics_, GControl1.smethod_11(Color.FromArgb(20, Color.White)), rectangle);
				GControl1.smethod_15(graphics_, GControl1.smethod_13(this), GControl1.smethod_14(this), GControl1.smethod_11(this.color_1), rectangle, GClass6.stringFormat_1);
			}
			break;
		case GEnum5.Down:
			if (this.Boolean_0)
			{
				graphicsPath_ = GClass6.smethod_0(rectangle, 6);
				GControl1.smethod_12(graphics_, GControl1.smethod_11(this.color_0), graphicsPath_);
				GControl1.smethod_12(graphics_, GControl1.smethod_11(Color.FromArgb(20, Color.Black)), graphicsPath_);
				GControl1.smethod_15(graphics_, GControl1.smethod_13(this), GControl1.smethod_14(this), GControl1.smethod_11(this.color_1), rectangle, GClass6.stringFormat_1);
			}
			else
			{
				GControl1.smethod_16(graphics_, GControl1.smethod_11(this.color_0), rectangle);
				GControl1.smethod_16(graphics_, GControl1.smethod_11(Color.FromArgb(20, Color.Black)), rectangle);
				GControl1.smethod_15(graphics_, GControl1.smethod_13(this), GControl1.smethod_14(this), GControl1.smethod_11(this.color_1), rectangle, GClass6.stringFormat_1);
			}
			break;
		}
		base.OnPaint(e);
		GControl1.smethod_17(graphics);
		GControl1.smethod_19(GControl1.smethod_18(e), InterpolationMode.HighQualityBicubic);
		GControl1.smethod_20(GControl1.smethod_18(e), image_, 0, 0);
		GControl1.smethod_21(image_);
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00005648 File Offset: 0x00003848
	private void method_0()
	{
		GClass0 gclass = GClass6.smethod_3(this);
		this.color_0 = gclass.color_0;
	}

	// Token: 0x06000074 RID: 116 RVA: 0x0000232F File Offset: 0x0000052F
	static void smethod_0(Control control_0)
	{
		control_0.Invalidate();
	}

	// Token: 0x06000075 RID: 117 RVA: 0x0000237B File Offset: 0x0000057B
	static int smethod_1(Control control_0)
	{
		return control_0.Width;
	}

	// Token: 0x06000076 RID: 118 RVA: 0x00002383 File Offset: 0x00000583
	static int smethod_2(Control control_0)
	{
		return control_0.Height;
	}

	// Token: 0x06000077 RID: 119 RVA: 0x0000238B File Offset: 0x0000058B
	static Bitmap smethod_3(int int_2, int int_3)
	{
		return new Bitmap(int_2, int_3);
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00002394 File Offset: 0x00000594
	static Graphics smethod_4(Image image_0)
	{
		return Graphics.FromImage(image_0);
	}

	// Token: 0x06000079 RID: 121 RVA: 0x0000245C File Offset: 0x0000065C
	static GraphicsPath smethod_5()
	{
		return new GraphicsPath();
	}

	// Token: 0x0600007A RID: 122 RVA: 0x0000239C File Offset: 0x0000059C
	static void smethod_6(Graphics graphics_0, SmoothingMode smoothingMode_0)
	{
		graphics_0.SmoothingMode = smoothingMode_0;
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00002463 File Offset: 0x00000663
	static void smethod_7(Graphics graphics_0, PixelOffsetMode pixelOffsetMode_0)
	{
		graphics_0.PixelOffsetMode = pixelOffsetMode_0;
	}

	// Token: 0x0600007C RID: 124 RVA: 0x000023A5 File Offset: 0x000005A5
	static void smethod_8(Graphics graphics_0, TextRenderingHint textRenderingHint_0)
	{
		graphics_0.TextRenderingHint = textRenderingHint_0;
	}

	// Token: 0x0600007D RID: 125 RVA: 0x000023AE File Offset: 0x000005AE
	static Color smethod_9(Control control_0)
	{
		return control_0.BackColor;
	}

	// Token: 0x0600007E RID: 126 RVA: 0x000023B6 File Offset: 0x000005B6
	static void smethod_10(Graphics graphics_0, Color color_2)
	{
		graphics_0.Clear(color_2);
	}

	// Token: 0x0600007F RID: 127 RVA: 0x000023BF File Offset: 0x000005BF
	static SolidBrush smethod_11(Color color_2)
	{
		return new SolidBrush(color_2);
	}

	// Token: 0x06000080 RID: 128 RVA: 0x0000246C File Offset: 0x0000066C
	static void smethod_12(Graphics graphics_0, Brush brush_0, GraphicsPath graphicsPath_0)
	{
		graphics_0.FillPath(brush_0, graphicsPath_0);
	}

	// Token: 0x06000081 RID: 129 RVA: 0x00002476 File Offset: 0x00000676
	static string smethod_13(Control control_0)
	{
		return control_0.Text;
	}

	// Token: 0x06000082 RID: 130 RVA: 0x0000247E File Offset: 0x0000067E
	static Font smethod_14(Control control_0)
	{
		return control_0.Font;
	}

	// Token: 0x06000083 RID: 131 RVA: 0x00002486 File Offset: 0x00000686
	static void smethod_15(Graphics graphics_0, string string_0, Font font_0, Brush brush_0, RectangleF rectangleF_0, StringFormat stringFormat_0)
	{
		graphics_0.DrawString(string_0, font_0, brush_0, rectangleF_0, stringFormat_0);
	}

	// Token: 0x06000084 RID: 132 RVA: 0x000023C7 File Offset: 0x000005C7
	static void smethod_16(Graphics graphics_0, Brush brush_0, Rectangle rectangle_0)
	{
		graphics_0.FillRectangle(brush_0, rectangle_0);
	}

	// Token: 0x06000085 RID: 133 RVA: 0x00002495 File Offset: 0x00000695
	static void smethod_17(Graphics graphics_0)
	{
		graphics_0.Dispose();
	}

	// Token: 0x06000086 RID: 134 RVA: 0x0000249D File Offset: 0x0000069D
	static Graphics smethod_18(PaintEventArgs paintEventArgs_0)
	{
		return paintEventArgs_0.Graphics;
	}

	// Token: 0x06000087 RID: 135 RVA: 0x000024A5 File Offset: 0x000006A5
	static void smethod_19(Graphics graphics_0, InterpolationMode interpolationMode_0)
	{
		graphics_0.InterpolationMode = interpolationMode_0;
	}

	// Token: 0x06000088 RID: 136 RVA: 0x000024AE File Offset: 0x000006AE
	static void smethod_20(Graphics graphics_0, Image image_0, int int_2, int int_3)
	{
		graphics_0.DrawImageUnscaled(image_0, int_2, int_3);
	}

	// Token: 0x06000089 RID: 137 RVA: 0x000024B9 File Offset: 0x000006B9
	static void smethod_21(Image image_0)
	{
		image_0.Dispose();
	}

	// Token: 0x04000012 RID: 18
	private int int_0;

	// Token: 0x04000013 RID: 19
	private int int_1;

	// Token: 0x04000014 RID: 20
	private bool bool_0;

	// Token: 0x04000015 RID: 21
	private GEnum5 genum5_0;

	// Token: 0x04000016 RID: 22
	private Color color_0 = GClass6.color_0;

	// Token: 0x04000017 RID: 23
	private Color color_1 = Color.FromArgb(243, 243, 243);
}
