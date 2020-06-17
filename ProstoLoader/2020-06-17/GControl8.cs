using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

// Token: 0x02000013 RID: 19
public class GControl8 : Control
{
	// Token: 0x060001CC RID: 460 RVA: 0x00002C64 File Offset: 0x00000E64
	protected virtual void OnMouseEnter(EventArgs e)
	{
		base.OnMouseEnter(e);
		this.genum5_0 = GEnum5.Over;
		GControl8.smethod_0(this);
	}

	// Token: 0x060001CD RID: 461 RVA: 0x00002C7A File Offset: 0x00000E7A
	protected virtual void OnMouseDown(MouseEventArgs e)
	{
		base.OnMouseDown(e);
		this.genum5_0 = GEnum5.Down;
		GControl8.smethod_0(this);
	}

	// Token: 0x060001CE RID: 462 RVA: 0x00002C90 File Offset: 0x00000E90
	protected virtual void OnMouseLeave(EventArgs e)
	{
		base.OnMouseLeave(e);
		this.genum5_0 = GEnum5.None;
		GControl8.smethod_0(this);
	}

	// Token: 0x060001CF RID: 463 RVA: 0x00002CA6 File Offset: 0x00000EA6
	protected virtual void OnMouseUp(MouseEventArgs e)
	{
		base.OnMouseUp(e);
		this.genum5_0 = GEnum5.Over;
		GControl8.smethod_0(this);
	}

	// Token: 0x060001D0 RID: 464 RVA: 0x00002CBC File Offset: 0x00000EBC
	protected virtual void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);
		this.int_0 = GControl8.smethod_1(e);
		GControl8.smethod_0(this);
	}

	// Token: 0x060001D1 RID: 465 RVA: 0x00006D48 File Offset: 0x00004F48
	protected virtual void OnClick(EventArgs e)
	{
		base.OnClick(e);
		FormWindowState formWindowState = GControl8.smethod_3(GControl8.smethod_2(this));
		if (formWindowState == FormWindowState.Normal)
		{
			GControl8.smethod_4(GControl8.smethod_2(this), FormWindowState.Minimized);
			return;
		}
		if (formWindowState != FormWindowState.Maximized)
		{
			return;
		}
		GControl8.smethod_4(GControl8.smethod_2(this), FormWindowState.Minimized);
	}

	// Token: 0x17000031 RID: 49
	// (get) Token: 0x060001D2 RID: 466 RVA: 0x00002CD7 File Offset: 0x00000ED7
	// (set) Token: 0x060001D3 RID: 467 RVA: 0x00002CDF File Offset: 0x00000EDF
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

	// Token: 0x17000032 RID: 50
	// (get) Token: 0x060001D4 RID: 468 RVA: 0x00002CE8 File Offset: 0x00000EE8
	// (set) Token: 0x060001D5 RID: 469 RVA: 0x00002CF0 File Offset: 0x00000EF0
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

	// Token: 0x060001D6 RID: 470 RVA: 0x00002677 File Offset: 0x00000877
	protected virtual void OnResize(EventArgs e)
	{
		base.OnResize(e);
		base.Size = new Size(18, 18);
	}

	// Token: 0x060001D7 RID: 471 RVA: 0x00006D8C File Offset: 0x00004F8C
	public GControl8()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		GControl8.smethod_5(this, Color.White);
		base.Size = new Size(18, 18);
		this.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
		this.Font = new Font("Marlett", 12f);
	}

	// Token: 0x060001D8 RID: 472 RVA: 0x00006E14 File Offset: 0x00005014
	protected virtual void OnPaint(PaintEventArgs e)
	{
		Bitmap bitmap = GControl8.smethod_8(GControl8.smethod_6(this), GControl8.smethod_7(this));
		Graphics graphics = GControl8.smethod_9(bitmap);
		Rectangle rectangle = new Rectangle(0, 0, GControl8.smethod_6(this), GControl8.smethod_7(this));
		Graphics graphics2 = graphics;
		GControl8.smethod_10(graphics2, SmoothingMode.HighQuality);
		GControl8.smethod_11(graphics2, PixelOffsetMode.HighQuality);
		GControl8.smethod_12(graphics2, TextRenderingHint.ClearTypeGridFit);
		GControl8.smethod_14(graphics2, GControl8.smethod_13(this));
		GControl8.smethod_16(graphics2, GControl8.smethod_15(this.color_0), rectangle);
		graphics2.DrawString("0", GControl8.smethod_17(this), GControl8.smethod_15(this.Color_1), new Rectangle(2, 1, GControl8.smethod_6(this), GControl8.smethod_7(this)), GClass6.stringFormat_1);
		GEnum5 genum = this.genum5_0;
		if (genum == GEnum5.Over)
		{
			graphics2.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.White)), rectangle);
		}
		else if (genum == GEnum5.Down)
		{
			graphics2.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.Black)), rectangle);
		}
		base.OnPaint(e);
		graphics.Dispose();
		e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
		bitmap.Dispose();
	}

	// Token: 0x060001D9 RID: 473 RVA: 0x0000232F File Offset: 0x0000052F
	static void smethod_0(Control control_0)
	{
		control_0.Invalidate();
	}

	// Token: 0x060001DA RID: 474 RVA: 0x0000236A File Offset: 0x0000056A
	static int smethod_1(MouseEventArgs mouseEventArgs_0)
	{
		return mouseEventArgs_0.X;
	}

	// Token: 0x060001DB RID: 475 RVA: 0x00002C4B File Offset: 0x00000E4B
	static Form smethod_2(Control control_0)
	{
		return control_0.FindForm();
	}

	// Token: 0x060001DC RID: 476 RVA: 0x00002C53 File Offset: 0x00000E53
	static FormWindowState smethod_3(Form form_0)
	{
		return form_0.WindowState;
	}

	// Token: 0x060001DD RID: 477 RVA: 0x00002C5B File Offset: 0x00000E5B
	static void smethod_4(Form form_0, FormWindowState formWindowState_0)
	{
		form_0.WindowState = formWindowState_0;
	}

	// Token: 0x060001DE RID: 478 RVA: 0x00002372 File Offset: 0x00000572
	static void smethod_5(Control control_0, Color color_2)
	{
		control_0.BackColor = color_2;
	}

	// Token: 0x060001DF RID: 479 RVA: 0x0000237B File Offset: 0x0000057B
	static int smethod_6(Control control_0)
	{
		return control_0.Width;
	}

	// Token: 0x060001E0 RID: 480 RVA: 0x00002383 File Offset: 0x00000583
	static int smethod_7(Control control_0)
	{
		return control_0.Height;
	}

	// Token: 0x060001E1 RID: 481 RVA: 0x0000238B File Offset: 0x0000058B
	static Bitmap smethod_8(int int_1, int int_2)
	{
		return new Bitmap(int_1, int_2);
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x00002394 File Offset: 0x00000594
	static Graphics smethod_9(Image image_0)
	{
		return Graphics.FromImage(image_0);
	}

	// Token: 0x060001E3 RID: 483 RVA: 0x0000239C File Offset: 0x0000059C
	static void smethod_10(Graphics graphics_0, SmoothingMode smoothingMode_0)
	{
		graphics_0.SmoothingMode = smoothingMode_0;
	}

	// Token: 0x060001E4 RID: 484 RVA: 0x00002463 File Offset: 0x00000663
	static void smethod_11(Graphics graphics_0, PixelOffsetMode pixelOffsetMode_0)
	{
		graphics_0.PixelOffsetMode = pixelOffsetMode_0;
	}

	// Token: 0x060001E5 RID: 485 RVA: 0x000023A5 File Offset: 0x000005A5
	static void smethod_12(Graphics graphics_0, TextRenderingHint textRenderingHint_0)
	{
		graphics_0.TextRenderingHint = textRenderingHint_0;
	}

	// Token: 0x060001E6 RID: 486 RVA: 0x000023AE File Offset: 0x000005AE
	static Color smethod_13(Control control_0)
	{
		return control_0.BackColor;
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x000023B6 File Offset: 0x000005B6
	static void smethod_14(Graphics graphics_0, Color color_2)
	{
		graphics_0.Clear(color_2);
	}

	// Token: 0x060001E8 RID: 488 RVA: 0x000023BF File Offset: 0x000005BF
	static SolidBrush smethod_15(Color color_2)
	{
		return new SolidBrush(color_2);
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x000023C7 File Offset: 0x000005C7
	static void smethod_16(Graphics graphics_0, Brush brush_0, Rectangle rectangle_0)
	{
		graphics_0.FillRectangle(brush_0, rectangle_0);
	}

	// Token: 0x060001EA RID: 490 RVA: 0x0000247E File Offset: 0x0000067E
	static Font smethod_17(Control control_0)
	{
		return control_0.Font;
	}

	// Token: 0x0400004D RID: 77
	private GEnum5 genum5_0;

	// Token: 0x0400004E RID: 78
	private int int_0;

	// Token: 0x0400004F RID: 79
	private Color color_0 = Color.FromArgb(45, 47, 49);

	// Token: 0x04000050 RID: 80
	private Color color_1 = Color.FromArgb(243, 243, 243);
}
