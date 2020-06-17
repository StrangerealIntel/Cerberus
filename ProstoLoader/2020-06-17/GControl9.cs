using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

// Token: 0x02000014 RID: 20
public class GControl9 : Control
{
	// Token: 0x17000033 RID: 51
	// (get) Token: 0x060001EB RID: 491 RVA: 0x00002CF9 File Offset: 0x00000EF9
	// (set) Token: 0x060001EC RID: 492 RVA: 0x00002D01 File Offset: 0x00000F01
	public long Int64_0
	{
		get
		{
			return this.long_0;
		}
		set
		{
			if (value <= this.long_2 & value >= this.long_1)
			{
				this.long_0 = value;
			}
			GControl9.smethod_0(this);
		}
	}

	// Token: 0x17000034 RID: 52
	// (get) Token: 0x060001ED RID: 493 RVA: 0x00002D2B File Offset: 0x00000F2B
	// (set) Token: 0x060001EE RID: 494 RVA: 0x00002D33 File Offset: 0x00000F33
	public long Int64_1
	{
		get
		{
			return this.long_2;
		}
		set
		{
			if (value > this.long_1)
			{
				this.long_2 = value;
			}
			if (this.long_0 > this.long_2)
			{
				this.long_0 = this.long_2;
			}
			GControl9.smethod_0(this);
		}
	}

	// Token: 0x17000035 RID: 53
	// (get) Token: 0x060001EF RID: 495 RVA: 0x00002D65 File Offset: 0x00000F65
	// (set) Token: 0x060001F0 RID: 496 RVA: 0x00002D6D File Offset: 0x00000F6D
	public long Int64_2
	{
		get
		{
			return this.long_1;
		}
		set
		{
			if (value < this.long_2)
			{
				this.long_1 = value;
			}
			if (this.long_0 < this.long_1)
			{
				this.long_0 = this.Int64_2;
			}
			GControl9.smethod_0(this);
		}
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x00006F30 File Offset: 0x00005130
	protected virtual void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);
		this.int_2 = GControl9.smethod_1(e).X;
		this.int_3 = e.Location.Y;
		base.Invalidate();
		if (e.X < base.Width - 23)
		{
			this.Cursor = Cursors.IBeam;
			return;
		}
		this.Cursor = Cursors.Hand;
	}

	// Token: 0x060001F2 RID: 498 RVA: 0x00006F9C File Offset: 0x0000519C
	protected virtual void OnMouseDown(MouseEventArgs e)
	{
		base.OnMouseDown(e);
		if (this.int_2 > GControl9.smethod_2(this) - 21 && this.int_2 < GControl9.smethod_2(this) - 3)
		{
			if (this.int_3 >= 15)
			{
				if (this.Int64_0 - 1L >= this.long_1)
				{
					this.long_0 -= 1L;
				}
			}
			else if (this.Int64_0 + 1L <= this.long_2)
			{
				this.long_0 += 1L;
			}
		}
		else
		{
			this.bool_0 = !this.bool_0;
			GControl9.smethod_3(this);
		}
		GControl9.smethod_0(this);
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x00007058 File Offset: 0x00005258
	protected virtual void OnKeyPress(KeyPressEventArgs e)
	{
		base.OnKeyPress(e);
		try
		{
			if (this.bool_0)
			{
				this.long_0 = Convert.ToInt64(this.long_0.ToString() + e.KeyChar.ToString());
			}
			if (this.long_0 > this.long_2)
			{
				this.long_0 = this.long_2;
			}
			base.Invalidate();
		}
		catch
		{
		}
	}

	// Token: 0x060001F4 RID: 500 RVA: 0x00002D9F File Offset: 0x00000F9F
	protected virtual void OnKeyDown(KeyEventArgs e)
	{
		base.OnKeyDown(e);
		if (GControl9.smethod_4(e) == Keys.Back)
		{
			this.Int64_0 = 0L;
		}
	}

	// Token: 0x060001F5 RID: 501 RVA: 0x00002DC2 File Offset: 0x00000FC2
	protected virtual void OnResize(EventArgs e)
	{
		base.OnResize(e);
		GControl9.smethod_5(this, 29);
	}

	// Token: 0x17000036 RID: 54
	// (get) Token: 0x060001F6 RID: 502 RVA: 0x00002DD3 File Offset: 0x00000FD3
	// (set) Token: 0x060001F7 RID: 503 RVA: 0x00002DDB File Offset: 0x00000FDB
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

	// Token: 0x17000037 RID: 55
	// (get) Token: 0x060001F8 RID: 504 RVA: 0x00002DE4 File Offset: 0x00000FE4
	// (set) Token: 0x060001F9 RID: 505 RVA: 0x00002DEC File Offset: 0x00000FEC
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

	// Token: 0x060001FA RID: 506 RVA: 0x000070D4 File Offset: 0x000052D4
	public GControl9()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		GControl9.smethod_7(this, GControl9.smethod_6("Segoe UI", 10f));
		GControl9.smethod_8(this, Color.FromArgb(60, 70, 73));
		GControl9.smethod_9(this, Color.White);
		this.long_1 = 0L;
		this.long_2 = 9999999L;
	}

	// Token: 0x060001FB RID: 507 RVA: 0x00007168 File Offset: 0x00005368
	protected virtual void OnPaint(PaintEventArgs e)
	{
		this.method_0();
		Bitmap bitmap = GControl9.smethod_11(GControl9.smethod_2(this), GControl9.smethod_10(this));
		Graphics graphics = GControl9.smethod_12(bitmap);
		this.int_0 = GControl9.smethod_2(this);
		this.int_1 = GControl9.smethod_10(this);
		Rectangle rectangle_ = new Rectangle(0, 0, this.int_0, this.int_1);
		GControl9.smethod_13(graphics, SmoothingMode.HighQuality);
		GControl9.smethod_14(graphics, PixelOffsetMode.HighQuality);
		GControl9.smethod_15(graphics, TextRenderingHint.ClearTypeGridFit);
		GControl9.smethod_17(graphics, GControl9.smethod_16(this));
		GControl9.smethod_19(graphics, GControl9.smethod_18(this.color_0), rectangle_);
		graphics.FillRectangle(GControl9.smethod_18(this.color_1), new Rectangle(GControl9.smethod_2(this) - 24, 0, 24, this.int_1));
		graphics.DrawString("+", new Font("Segoe UI", 12f), Brushes.White, new Point(base.Width - 12, 8), GClass6.stringFormat_1);
		graphics.DrawString("-", new Font("Segoe UI", 10f, FontStyle.Bold), Brushes.White, new Point(base.Width - 12, 22), GClass6.stringFormat_1);
		graphics.DrawString(this.Int64_0.ToString(), this.Font, Brushes.White, new Rectangle(5, 1, this.int_0, this.int_1), new StringFormat
		{
			LineAlignment = StringAlignment.Center
		});
		base.OnPaint(e);
		graphics.Dispose();
		e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
		bitmap.Dispose();
	}

	// Token: 0x060001FC RID: 508 RVA: 0x000072FC File Offset: 0x000054FC
	private void method_0()
	{
		GClass0 gclass = GClass6.smethod_3(this);
		this.color_1 = gclass.color_0;
	}

	// Token: 0x060001FD RID: 509 RVA: 0x0000232F File Offset: 0x0000052F
	static void smethod_0(Control control_0)
	{
		control_0.Invalidate();
	}

	// Token: 0x060001FE RID: 510 RVA: 0x0000283B File Offset: 0x00000A3B
	static Point smethod_1(MouseEventArgs mouseEventArgs_0)
	{
		return mouseEventArgs_0.Location;
	}

	// Token: 0x060001FF RID: 511 RVA: 0x0000237B File Offset: 0x0000057B
	static int smethod_2(Control control_0)
	{
		return control_0.Width;
	}

	// Token: 0x06000200 RID: 512 RVA: 0x00002DF5 File Offset: 0x00000FF5
	static bool smethod_3(Control control_0)
	{
		return control_0.Focus();
	}

	// Token: 0x06000201 RID: 513 RVA: 0x00002DFD File Offset: 0x00000FFD
	static Keys smethod_4(KeyEventArgs keyEventArgs_0)
	{
		return keyEventArgs_0.KeyCode;
	}

	// Token: 0x06000202 RID: 514 RVA: 0x00002337 File Offset: 0x00000537
	static void smethod_5(Control control_0, int int_4)
	{
		control_0.Height = int_4;
	}

	// Token: 0x06000203 RID: 515 RVA: 0x000025D1 File Offset: 0x000007D1
	static Font smethod_6(string string_0, float float_0)
	{
		return new Font(string_0, float_0);
	}

	// Token: 0x06000204 RID: 516 RVA: 0x000025DA File Offset: 0x000007DA
	static void smethod_7(Control control_0, Font font_0)
	{
		control_0.Font = font_0;
	}

	// Token: 0x06000205 RID: 517 RVA: 0x00002372 File Offset: 0x00000572
	static void smethod_8(Control control_0, Color color_2)
	{
		control_0.BackColor = color_2;
	}

	// Token: 0x06000206 RID: 518 RVA: 0x000028AF File Offset: 0x00000AAF
	static void smethod_9(Control control_0, Color color_2)
	{
		control_0.ForeColor = color_2;
	}

	// Token: 0x06000207 RID: 519 RVA: 0x00002383 File Offset: 0x00000583
	static int smethod_10(Control control_0)
	{
		return control_0.Height;
	}

	// Token: 0x06000208 RID: 520 RVA: 0x0000238B File Offset: 0x0000058B
	static Bitmap smethod_11(int int_4, int int_5)
	{
		return new Bitmap(int_4, int_5);
	}

	// Token: 0x06000209 RID: 521 RVA: 0x00002394 File Offset: 0x00000594
	static Graphics smethod_12(Image image_0)
	{
		return Graphics.FromImage(image_0);
	}

	// Token: 0x0600020A RID: 522 RVA: 0x0000239C File Offset: 0x0000059C
	static void smethod_13(Graphics graphics_0, SmoothingMode smoothingMode_0)
	{
		graphics_0.SmoothingMode = smoothingMode_0;
	}

	// Token: 0x0600020B RID: 523 RVA: 0x00002463 File Offset: 0x00000663
	static void smethod_14(Graphics graphics_0, PixelOffsetMode pixelOffsetMode_0)
	{
		graphics_0.PixelOffsetMode = pixelOffsetMode_0;
	}

	// Token: 0x0600020C RID: 524 RVA: 0x000023A5 File Offset: 0x000005A5
	static void smethod_15(Graphics graphics_0, TextRenderingHint textRenderingHint_0)
	{
		graphics_0.TextRenderingHint = textRenderingHint_0;
	}

	// Token: 0x0600020D RID: 525 RVA: 0x000023AE File Offset: 0x000005AE
	static Color smethod_16(Control control_0)
	{
		return control_0.BackColor;
	}

	// Token: 0x0600020E RID: 526 RVA: 0x000023B6 File Offset: 0x000005B6
	static void smethod_17(Graphics graphics_0, Color color_2)
	{
		graphics_0.Clear(color_2);
	}

	// Token: 0x0600020F RID: 527 RVA: 0x000023BF File Offset: 0x000005BF
	static SolidBrush smethod_18(Color color_2)
	{
		return new SolidBrush(color_2);
	}

	// Token: 0x06000210 RID: 528 RVA: 0x000023C7 File Offset: 0x000005C7
	static void smethod_19(Graphics graphics_0, Brush brush_0, Rectangle rectangle_0)
	{
		graphics_0.FillRectangle(brush_0, rectangle_0);
	}

	// Token: 0x04000051 RID: 81
	private int int_0;

	// Token: 0x04000052 RID: 82
	private int int_1;

	// Token: 0x04000053 RID: 83
	private GEnum5 genum5_0;

	// Token: 0x04000054 RID: 84
	private int int_2;

	// Token: 0x04000055 RID: 85
	private int int_3;

	// Token: 0x04000056 RID: 86
	private long long_0;

	// Token: 0x04000057 RID: 87
	private long long_1;

	// Token: 0x04000058 RID: 88
	private long long_2;

	// Token: 0x04000059 RID: 89
	private bool bool_0;

	// Token: 0x0400005A RID: 90
	private Color color_0 = Color.FromArgb(45, 47, 49);

	// Token: 0x0400005B RID: 91
	private Color color_1 = GClass6.color_0;
}
