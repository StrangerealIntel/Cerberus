using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

// Token: 0x0200000A RID: 10
public class GControl4 : Control
{
	// Token: 0x060000D4 RID: 212 RVA: 0x000026B9 File Offset: 0x000008B9
	protected virtual void OnResize(EventArgs e)
	{
		base.OnResize(e);
		GControl4.smethod_0(this, 180);
		GControl4.smethod_1(this, 80);
	}

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x060000D5 RID: 213 RVA: 0x000026D5 File Offset: 0x000008D5
	// (set) Token: 0x060000D6 RID: 214 RVA: 0x000026DD File Offset: 0x000008DD
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

	// Token: 0x1700000F RID: 15
	// (get) Token: 0x060000D7 RID: 215 RVA: 0x000026E6 File Offset: 0x000008E6
	// (set) Token: 0x060000D8 RID: 216 RVA: 0x000026EE File Offset: 0x000008EE
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

	// Token: 0x17000010 RID: 16
	// (get) Token: 0x060000D9 RID: 217 RVA: 0x000026F7 File Offset: 0x000008F7
	// (set) Token: 0x060000DA RID: 218 RVA: 0x000026FF File Offset: 0x000008FF
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

	// Token: 0x17000011 RID: 17
	// (get) Token: 0x060000DB RID: 219 RVA: 0x00002708 File Offset: 0x00000908
	// (set) Token: 0x060000DC RID: 220 RVA: 0x00002710 File Offset: 0x00000910
	[Category("Colors")]
	public Color Color_3
	{
		get
		{
			return this.color_3;
		}
		set
		{
			this.color_3 = value;
		}
	}

	// Token: 0x17000012 RID: 18
	// (get) Token: 0x060000DD RID: 221 RVA: 0x00002719 File Offset: 0x00000919
	// (set) Token: 0x060000DE RID: 222 RVA: 0x00002721 File Offset: 0x00000921
	[Category("Colors")]
	public Color Color_4
	{
		get
		{
			return this.color_4;
		}
		set
		{
			this.color_4 = value;
		}
	}

	// Token: 0x17000013 RID: 19
	// (get) Token: 0x060000DF RID: 223 RVA: 0x0000272A File Offset: 0x0000092A
	// (set) Token: 0x060000E0 RID: 224 RVA: 0x00002732 File Offset: 0x00000932
	[Category("Colors")]
	public Color Color_5
	{
		get
		{
			return this.color_5;
		}
		set
		{
			this.color_5 = value;
		}
	}

	// Token: 0x17000014 RID: 20
	// (get) Token: 0x060000E1 RID: 225 RVA: 0x0000273B File Offset: 0x0000093B
	// (set) Token: 0x060000E2 RID: 226 RVA: 0x00002743 File Offset: 0x00000943
	[Category("Colors")]
	public Color Color_6
	{
		get
		{
			return this.color_6;
		}
		set
		{
			this.color_6 = value;
		}
	}

	// Token: 0x17000015 RID: 21
	// (get) Token: 0x060000E3 RID: 227 RVA: 0x0000274C File Offset: 0x0000094C
	// (set) Token: 0x060000E4 RID: 228 RVA: 0x00002754 File Offset: 0x00000954
	[Category("Colors")]
	public Color Color_7
	{
		get
		{
			return this.color_7;
		}
		set
		{
			this.color_7 = value;
		}
	}

	// Token: 0x17000016 RID: 22
	// (get) Token: 0x060000E5 RID: 229 RVA: 0x0000275D File Offset: 0x0000095D
	// (set) Token: 0x060000E6 RID: 230 RVA: 0x00002765 File Offset: 0x00000965
	[Category("Colors")]
	public Color Color_8
	{
		get
		{
			return this.color_8;
		}
		set
		{
			this.color_8 = value;
		}
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x00005CB4 File Offset: 0x00003EB4
	public GControl4()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		GControl4.smethod_2(this, Color.FromArgb(60, 70, 73));
		base.Size = new Size(160, 80);
		this.Font = new Font("Segoe UI", 12f);
	}

	// Token: 0x060000E8 RID: 232 RVA: 0x00005DD4 File Offset: 0x00003FD4
	protected virtual void OnPaint(PaintEventArgs e)
	{
		Bitmap bitmap = GControl4.smethod_5(GControl4.smethod_3(this), GControl4.smethod_4(this));
		Graphics graphics = GControl4.smethod_6(bitmap);
		this.int_0 = GControl4.smethod_3(this) - 1;
		this.int_1 = GControl4.smethod_4(this) - 1;
		GControl4.smethod_7(graphics, SmoothingMode.HighQuality);
		GControl4.smethod_8(graphics, PixelOffsetMode.HighQuality);
		GControl4.smethod_9(graphics, TextRenderingHint.ClearTypeGridFit);
		GControl4.smethod_11(graphics, GControl4.smethod_10(this));
		graphics.FillRectangle(GControl4.smethod_12(this.color_0), new Rectangle(0, 0, 20, 40));
		graphics.FillRectangle(new SolidBrush(this.color_1), new Rectangle(20, 0, 20, 40));
		graphics.FillRectangle(new SolidBrush(this.color_2), new Rectangle(40, 0, 20, 40));
		graphics.FillRectangle(new SolidBrush(this.color_3), new Rectangle(60, 0, 20, 40));
		graphics.FillRectangle(new SolidBrush(this.color_4), new Rectangle(80, 0, 20, 40));
		graphics.FillRectangle(new SolidBrush(this.color_5), new Rectangle(100, 0, 20, 40));
		graphics.FillRectangle(new SolidBrush(this.color_6), new Rectangle(120, 0, 20, 40));
		graphics.FillRectangle(new SolidBrush(this.color_7), new Rectangle(140, 0, 20, 40));
		graphics.FillRectangle(new SolidBrush(this.color_8), new Rectangle(160, 0, 20, 40));
		graphics.DrawString("Color Palette", this.Font, new SolidBrush(this.color_8), new Rectangle(0, 22, this.int_0, this.int_1), GClass6.stringFormat_1);
		base.OnPaint(e);
		graphics.Dispose();
		e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
		bitmap.Dispose();
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x0000276E File Offset: 0x0000096E
	static void smethod_0(Control control_0, int int_2)
	{
		control_0.Width = int_2;
	}

	// Token: 0x060000EA RID: 234 RVA: 0x00002337 File Offset: 0x00000537
	static void smethod_1(Control control_0, int int_2)
	{
		control_0.Height = int_2;
	}

	// Token: 0x060000EB RID: 235 RVA: 0x00002372 File Offset: 0x00000572
	static void smethod_2(Control control_0, Color color_9)
	{
		control_0.BackColor = color_9;
	}

	// Token: 0x060000EC RID: 236 RVA: 0x0000237B File Offset: 0x0000057B
	static int smethod_3(Control control_0)
	{
		return control_0.Width;
	}

	// Token: 0x060000ED RID: 237 RVA: 0x00002383 File Offset: 0x00000583
	static int smethod_4(Control control_0)
	{
		return control_0.Height;
	}

	// Token: 0x060000EE RID: 238 RVA: 0x0000238B File Offset: 0x0000058B
	static Bitmap smethod_5(int int_2, int int_3)
	{
		return new Bitmap(int_2, int_3);
	}

	// Token: 0x060000EF RID: 239 RVA: 0x00002394 File Offset: 0x00000594
	static Graphics smethod_6(Image image_0)
	{
		return Graphics.FromImage(image_0);
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x0000239C File Offset: 0x0000059C
	static void smethod_7(Graphics graphics_0, SmoothingMode smoothingMode_0)
	{
		graphics_0.SmoothingMode = smoothingMode_0;
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x00002463 File Offset: 0x00000663
	static void smethod_8(Graphics graphics_0, PixelOffsetMode pixelOffsetMode_0)
	{
		graphics_0.PixelOffsetMode = pixelOffsetMode_0;
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x000023A5 File Offset: 0x000005A5
	static void smethod_9(Graphics graphics_0, TextRenderingHint textRenderingHint_0)
	{
		graphics_0.TextRenderingHint = textRenderingHint_0;
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x000023AE File Offset: 0x000005AE
	static Color smethod_10(Control control_0)
	{
		return control_0.BackColor;
	}

	// Token: 0x060000F4 RID: 244 RVA: 0x000023B6 File Offset: 0x000005B6
	static void smethod_11(Graphics graphics_0, Color color_9)
	{
		graphics_0.Clear(color_9);
	}

	// Token: 0x060000F5 RID: 245 RVA: 0x000023BF File Offset: 0x000005BF
	static SolidBrush smethod_12(Color color_9)
	{
		return new SolidBrush(color_9);
	}

	// Token: 0x04000028 RID: 40
	private int int_0;

	// Token: 0x04000029 RID: 41
	private int int_1;

	// Token: 0x0400002A RID: 42
	private Color color_0 = Color.FromArgb(220, 85, 96);

	// Token: 0x0400002B RID: 43
	private Color color_1 = Color.FromArgb(10, 154, 157);

	// Token: 0x0400002C RID: 44
	private Color color_2 = Color.FromArgb(0, 128, 255);

	// Token: 0x0400002D RID: 45
	private Color color_3 = Color.FromArgb(35, 168, 109);

	// Token: 0x0400002E RID: 46
	private Color color_4 = Color.FromArgb(253, 181, 63);

	// Token: 0x0400002F RID: 47
	private Color color_5 = Color.FromArgb(155, 88, 181);

	// Token: 0x04000030 RID: 48
	private Color color_6 = Color.FromArgb(45, 47, 49);

	// Token: 0x04000031 RID: 49
	private Color color_7 = Color.FromArgb(63, 70, 73);

	// Token: 0x04000032 RID: 50
	private Color color_8 = Color.FromArgb(243, 243, 243);
}
