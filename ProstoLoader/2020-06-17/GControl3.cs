using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

// Token: 0x02000009 RID: 9
public class GControl3 : Control
{
	// Token: 0x060000B7 RID: 183 RVA: 0x000025F5 File Offset: 0x000007F5
	protected virtual void OnMouseEnter(EventArgs e)
	{
		base.OnMouseEnter(e);
		this.genum5_0 = GEnum5.Over;
		GControl3.smethod_0(this);
	}

	// Token: 0x060000B8 RID: 184 RVA: 0x0000260B File Offset: 0x0000080B
	protected virtual void OnMouseDown(MouseEventArgs e)
	{
		base.OnMouseDown(e);
		this.genum5_0 = GEnum5.Down;
		GControl3.smethod_0(this);
	}

	// Token: 0x060000B9 RID: 185 RVA: 0x00002621 File Offset: 0x00000821
	protected virtual void OnMouseLeave(EventArgs e)
	{
		base.OnMouseLeave(e);
		this.genum5_0 = GEnum5.None;
		GControl3.smethod_0(this);
	}

	// Token: 0x060000BA RID: 186 RVA: 0x00002637 File Offset: 0x00000837
	protected virtual void OnMouseUp(MouseEventArgs e)
	{
		base.OnMouseUp(e);
		this.genum5_0 = GEnum5.Over;
		GControl3.smethod_0(this);
	}

	// Token: 0x060000BB RID: 187 RVA: 0x0000264D File Offset: 0x0000084D
	protected virtual void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);
		this.int_0 = GControl3.smethod_1(e);
		GControl3.smethod_0(this);
	}

	// Token: 0x060000BC RID: 188 RVA: 0x00002668 File Offset: 0x00000868
	protected virtual void OnClick(EventArgs e)
	{
		base.OnClick(e);
		GControl3.smethod_2(0);
	}

	// Token: 0x060000BD RID: 189 RVA: 0x00002677 File Offset: 0x00000877
	protected virtual void OnResize(EventArgs e)
	{
		base.OnResize(e);
		base.Size = new Size(18, 18);
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x060000BE RID: 190 RVA: 0x0000268F File Offset: 0x0000088F
	// (set) Token: 0x060000BF RID: 191 RVA: 0x00002697 File Offset: 0x00000897
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

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x060000C0 RID: 192 RVA: 0x000026A0 File Offset: 0x000008A0
	// (set) Token: 0x060000C1 RID: 193 RVA: 0x000026A8 File Offset: 0x000008A8
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

	// Token: 0x060000C2 RID: 194 RVA: 0x00005B0C File Offset: 0x00003D0C
	public GControl3()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		GControl3.smethod_3(this, Color.White);
		base.Size = new Size(18, 18);
		this.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
		this.Font = new Font("Marlett", 10f);
	}

	// Token: 0x060000C3 RID: 195 RVA: 0x00005B98 File Offset: 0x00003D98
	protected virtual void OnPaint(PaintEventArgs e)
	{
		Bitmap bitmap = GControl3.smethod_6(GControl3.smethod_4(this), GControl3.smethod_5(this));
		Graphics graphics = GControl3.smethod_7(bitmap);
		Rectangle rectangle = new Rectangle(0, 0, GControl3.smethod_4(this), GControl3.smethod_5(this));
		Graphics graphics2 = graphics;
		GControl3.smethod_8(graphics2, SmoothingMode.HighQuality);
		GControl3.smethod_9(graphics2, PixelOffsetMode.HighQuality);
		GControl3.smethod_10(graphics2, TextRenderingHint.ClearTypeGridFit);
		GControl3.smethod_12(graphics2, GControl3.smethod_11(this));
		GControl3.smethod_14(graphics2, GControl3.smethod_13(this.color_0), rectangle);
		graphics2.DrawString("r", GControl3.smethod_15(this), GControl3.smethod_13(this.Color_1), new Rectangle(0, 0, GControl3.smethod_4(this), GControl3.smethod_5(this)), GClass6.stringFormat_1);
		GEnum5 genum = this.genum5_0;
		if (genum != GEnum5.Over)
		{
			if (genum == GEnum5.Down)
			{
				graphics2.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.Black)), rectangle);
			}
		}
		else
		{
			graphics2.FillRectangle(new SolidBrush(Color.FromArgb(30, Color.White)), rectangle);
		}
		base.OnPaint(e);
		graphics.Dispose();
		e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
		bitmap.Dispose();
	}

	// Token: 0x060000C4 RID: 196 RVA: 0x0000232F File Offset: 0x0000052F
	static void smethod_0(Control control_0)
	{
		control_0.Invalidate();
	}

	// Token: 0x060000C5 RID: 197 RVA: 0x0000236A File Offset: 0x0000056A
	static int smethod_1(MouseEventArgs mouseEventArgs_0)
	{
		return mouseEventArgs_0.X;
	}

	// Token: 0x060000C6 RID: 198 RVA: 0x000026B1 File Offset: 0x000008B1
	static void smethod_2(int int_1)
	{
		Environment.Exit(int_1);
	}

	// Token: 0x060000C7 RID: 199 RVA: 0x00002372 File Offset: 0x00000572
	static void smethod_3(Control control_0, Color color_2)
	{
		control_0.BackColor = color_2;
	}

	// Token: 0x060000C8 RID: 200 RVA: 0x0000237B File Offset: 0x0000057B
	static int smethod_4(Control control_0)
	{
		return control_0.Width;
	}

	// Token: 0x060000C9 RID: 201 RVA: 0x00002383 File Offset: 0x00000583
	static int smethod_5(Control control_0)
	{
		return control_0.Height;
	}

	// Token: 0x060000CA RID: 202 RVA: 0x0000238B File Offset: 0x0000058B
	static Bitmap smethod_6(int int_1, int int_2)
	{
		return new Bitmap(int_1, int_2);
	}

	// Token: 0x060000CB RID: 203 RVA: 0x00002394 File Offset: 0x00000594
	static Graphics smethod_7(Image image_0)
	{
		return Graphics.FromImage(image_0);
	}

	// Token: 0x060000CC RID: 204 RVA: 0x0000239C File Offset: 0x0000059C
	static void smethod_8(Graphics graphics_0, SmoothingMode smoothingMode_0)
	{
		graphics_0.SmoothingMode = smoothingMode_0;
	}

	// Token: 0x060000CD RID: 205 RVA: 0x00002463 File Offset: 0x00000663
	static void smethod_9(Graphics graphics_0, PixelOffsetMode pixelOffsetMode_0)
	{
		graphics_0.PixelOffsetMode = pixelOffsetMode_0;
	}

	// Token: 0x060000CE RID: 206 RVA: 0x000023A5 File Offset: 0x000005A5
	static void smethod_10(Graphics graphics_0, TextRenderingHint textRenderingHint_0)
	{
		graphics_0.TextRenderingHint = textRenderingHint_0;
	}

	// Token: 0x060000CF RID: 207 RVA: 0x000023AE File Offset: 0x000005AE
	static Color smethod_11(Control control_0)
	{
		return control_0.BackColor;
	}

	// Token: 0x060000D0 RID: 208 RVA: 0x000023B6 File Offset: 0x000005B6
	static void smethod_12(Graphics graphics_0, Color color_2)
	{
		graphics_0.Clear(color_2);
	}

	// Token: 0x060000D1 RID: 209 RVA: 0x000023BF File Offset: 0x000005BF
	static SolidBrush smethod_13(Color color_2)
	{
		return new SolidBrush(color_2);
	}

	// Token: 0x060000D2 RID: 210 RVA: 0x000023C7 File Offset: 0x000005C7
	static void smethod_14(Graphics graphics_0, Brush brush_0, Rectangle rectangle_0)
	{
		graphics_0.FillRectangle(brush_0, rectangle_0);
	}

	// Token: 0x060000D3 RID: 211 RVA: 0x0000247E File Offset: 0x0000067E
	static Font smethod_15(Control control_0)
	{
		return control_0.Font;
	}

	// Token: 0x04000024 RID: 36
	private GEnum5 genum5_0;

	// Token: 0x04000025 RID: 37
	private int int_0;

	// Token: 0x04000026 RID: 38
	private Color color_0 = Color.FromArgb(168, 35, 35);

	// Token: 0x04000027 RID: 39
	private Color color_1 = Color.FromArgb(243, 243, 243);
}
