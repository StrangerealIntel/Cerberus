using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

// Token: 0x02000012 RID: 18
public class GControl7 : Control
{
	// Token: 0x060001AD RID: 429 RVA: 0x00002BB6 File Offset: 0x00000DB6
	protected virtual void OnMouseEnter(EventArgs e)
	{
		base.OnMouseEnter(e);
		this.genum5_0 = GEnum5.Over;
		GControl7.smethod_0(this);
	}

	// Token: 0x060001AE RID: 430 RVA: 0x00002BCC File Offset: 0x00000DCC
	protected virtual void OnMouseDown(MouseEventArgs e)
	{
		base.OnMouseDown(e);
		this.genum5_0 = GEnum5.Down;
		GControl7.smethod_0(this);
	}

	// Token: 0x060001AF RID: 431 RVA: 0x00002BE2 File Offset: 0x00000DE2
	protected virtual void OnMouseLeave(EventArgs e)
	{
		base.OnMouseLeave(e);
		this.genum5_0 = GEnum5.None;
		GControl7.smethod_0(this);
	}

	// Token: 0x060001B0 RID: 432 RVA: 0x00002BF8 File Offset: 0x00000DF8
	protected virtual void OnMouseUp(MouseEventArgs e)
	{
		base.OnMouseUp(e);
		this.genum5_0 = GEnum5.Over;
		GControl7.smethod_0(this);
	}

	// Token: 0x060001B1 RID: 433 RVA: 0x00002C0E File Offset: 0x00000E0E
	protected virtual void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);
		this.int_0 = GControl7.smethod_1(e);
		GControl7.smethod_0(this);
	}

	// Token: 0x060001B2 RID: 434 RVA: 0x00006B0C File Offset: 0x00004D0C
	protected virtual void OnClick(EventArgs e)
	{
		base.OnClick(e);
		FormWindowState formWindowState = GControl7.smethod_3(GControl7.smethod_2(this));
		if (formWindowState != FormWindowState.Normal)
		{
			if (formWindowState == FormWindowState.Maximized)
			{
				GControl7.smethod_4(GControl7.smethod_2(this), FormWindowState.Normal);
				return;
			}
		}
		else
		{
			GControl7.smethod_4(GControl7.smethod_2(this), FormWindowState.Maximized);
		}
	}

	// Token: 0x1700002F RID: 47
	// (get) Token: 0x060001B3 RID: 435 RVA: 0x00002C29 File Offset: 0x00000E29
	// (set) Token: 0x060001B4 RID: 436 RVA: 0x00002C31 File Offset: 0x00000E31
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

	// Token: 0x17000030 RID: 48
	// (get) Token: 0x060001B5 RID: 437 RVA: 0x00002C3A File Offset: 0x00000E3A
	// (set) Token: 0x060001B6 RID: 438 RVA: 0x00002C42 File Offset: 0x00000E42
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

	// Token: 0x060001B7 RID: 439 RVA: 0x00002677 File Offset: 0x00000877
	protected virtual void OnResize(EventArgs e)
	{
		base.OnResize(e);
		base.Size = new Size(18, 18);
	}

	// Token: 0x060001B8 RID: 440 RVA: 0x00006B50 File Offset: 0x00004D50
	public GControl7()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		GControl7.smethod_5(this, Color.White);
		base.Size = new Size(18, 18);
		this.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
		this.Font = new Font("Marlett", 12f);
	}

	// Token: 0x060001B9 RID: 441 RVA: 0x00006BD8 File Offset: 0x00004DD8
	protected virtual void OnPaint(PaintEventArgs e)
	{
		Bitmap bitmap = GControl7.smethod_8(GControl7.smethod_6(this), GControl7.smethod_7(this));
		Graphics graphics = GControl7.smethod_9(bitmap);
		Rectangle rectangle = new Rectangle(0, 0, GControl7.smethod_6(this), GControl7.smethod_7(this));
		Graphics graphics2 = graphics;
		GControl7.smethod_10(graphics2, SmoothingMode.HighQuality);
		GControl7.smethod_11(graphics2, PixelOffsetMode.HighQuality);
		GControl7.smethod_12(graphics2, TextRenderingHint.ClearTypeGridFit);
		GControl7.smethod_14(graphics2, GControl7.smethod_13(this));
		GControl7.smethod_16(graphics2, GControl7.smethod_15(this.color_0), rectangle);
		if (GControl7.smethod_3(GControl7.smethod_2(this)) != FormWindowState.Maximized)
		{
			if (base.FindForm().WindowState == FormWindowState.Normal)
			{
				graphics2.DrawString("2", this.Font, new SolidBrush(this.Color_1), new Rectangle(1, 1, base.Width, base.Height), GClass6.stringFormat_1);
			}
		}
		else
		{
			graphics2.DrawString("1", GControl7.smethod_17(this), GControl7.smethod_15(this.Color_1), new Rectangle(1, 1, GControl7.smethod_6(this), GControl7.smethod_7(this)), GClass6.stringFormat_1);
		}
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

	// Token: 0x060001BA RID: 442 RVA: 0x0000232F File Offset: 0x0000052F
	static void smethod_0(Control control_0)
	{
		control_0.Invalidate();
	}

	// Token: 0x060001BB RID: 443 RVA: 0x0000236A File Offset: 0x0000056A
	static int smethod_1(MouseEventArgs mouseEventArgs_0)
	{
		return mouseEventArgs_0.X;
	}

	// Token: 0x060001BC RID: 444 RVA: 0x00002C4B File Offset: 0x00000E4B
	static Form smethod_2(Control control_0)
	{
		return control_0.FindForm();
	}

	// Token: 0x060001BD RID: 445 RVA: 0x00002C53 File Offset: 0x00000E53
	static FormWindowState smethod_3(Form form_0)
	{
		return form_0.WindowState;
	}

	// Token: 0x060001BE RID: 446 RVA: 0x00002C5B File Offset: 0x00000E5B
	static void smethod_4(Form form_0, FormWindowState formWindowState_0)
	{
		form_0.WindowState = formWindowState_0;
	}

	// Token: 0x060001BF RID: 447 RVA: 0x00002372 File Offset: 0x00000572
	static void smethod_5(Control control_0, Color color_2)
	{
		control_0.BackColor = color_2;
	}

	// Token: 0x060001C0 RID: 448 RVA: 0x0000237B File Offset: 0x0000057B
	static int smethod_6(Control control_0)
	{
		return control_0.Width;
	}

	// Token: 0x060001C1 RID: 449 RVA: 0x00002383 File Offset: 0x00000583
	static int smethod_7(Control control_0)
	{
		return control_0.Height;
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x0000238B File Offset: 0x0000058B
	static Bitmap smethod_8(int int_1, int int_2)
	{
		return new Bitmap(int_1, int_2);
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x00002394 File Offset: 0x00000594
	static Graphics smethod_9(Image image_0)
	{
		return Graphics.FromImage(image_0);
	}

	// Token: 0x060001C4 RID: 452 RVA: 0x0000239C File Offset: 0x0000059C
	static void smethod_10(Graphics graphics_0, SmoothingMode smoothingMode_0)
	{
		graphics_0.SmoothingMode = smoothingMode_0;
	}

	// Token: 0x060001C5 RID: 453 RVA: 0x00002463 File Offset: 0x00000663
	static void smethod_11(Graphics graphics_0, PixelOffsetMode pixelOffsetMode_0)
	{
		graphics_0.PixelOffsetMode = pixelOffsetMode_0;
	}

	// Token: 0x060001C6 RID: 454 RVA: 0x000023A5 File Offset: 0x000005A5
	static void smethod_12(Graphics graphics_0, TextRenderingHint textRenderingHint_0)
	{
		graphics_0.TextRenderingHint = textRenderingHint_0;
	}

	// Token: 0x060001C7 RID: 455 RVA: 0x000023AE File Offset: 0x000005AE
	static Color smethod_13(Control control_0)
	{
		return control_0.BackColor;
	}

	// Token: 0x060001C8 RID: 456 RVA: 0x000023B6 File Offset: 0x000005B6
	static void smethod_14(Graphics graphics_0, Color color_2)
	{
		graphics_0.Clear(color_2);
	}

	// Token: 0x060001C9 RID: 457 RVA: 0x000023BF File Offset: 0x000005BF
	static SolidBrush smethod_15(Color color_2)
	{
		return new SolidBrush(color_2);
	}

	// Token: 0x060001CA RID: 458 RVA: 0x000023C7 File Offset: 0x000005C7
	static void smethod_16(Graphics graphics_0, Brush brush_0, Rectangle rectangle_0)
	{
		graphics_0.FillRectangle(brush_0, rectangle_0);
	}

	// Token: 0x060001CB RID: 459 RVA: 0x0000247E File Offset: 0x0000067E
	static Font smethod_17(Control control_0)
	{
		return control_0.Font;
	}

	// Token: 0x04000049 RID: 73
	private GEnum5 genum5_0;

	// Token: 0x0400004A RID: 74
	private int int_0;

	// Token: 0x0400004B RID: 75
	private Color color_0 = Color.FromArgb(45, 47, 49);

	// Token: 0x0400004C RID: 76
	private Color color_1 = Color.FromArgb(243, 243, 243);
}
