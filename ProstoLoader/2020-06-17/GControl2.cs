using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

// Token: 0x02000006 RID: 6
[DefaultEvent("CheckedChanged")]
public class GControl2 : Control
{
	// Token: 0x0600008A RID: 138 RVA: 0x000024C1 File Offset: 0x000006C1
	protected virtual void OnTextChanged(EventArgs e)
	{
		base.OnTextChanged(e);
		GControl2.smethod_0(this);
	}

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x0600008B RID: 139 RVA: 0x000024D0 File Offset: 0x000006D0
	// (set) Token: 0x0600008C RID: 140 RVA: 0x000024D8 File Offset: 0x000006D8
	public bool Boolean_0
	{
		get
		{
			return this.bool_0;
		}
		set
		{
			this.bool_0 = value;
			GControl2.smethod_0(this);
		}
	}

	// Token: 0x14000001 RID: 1
	// (add) Token: 0x0600008D RID: 141 RVA: 0x00005668 File Offset: 0x00003868
	// (remove) Token: 0x0600008E RID: 142 RVA: 0x000056A4 File Offset: 0x000038A4
	public event GControl2.GDelegate0 Event_0
	{
		[CompilerGenerated]
		add
		{
			GControl2.GDelegate0 gdelegate = this.gdelegate0_0;
			GControl2.GDelegate0 gdelegate2;
			do
			{
				gdelegate2 = gdelegate;
				GControl2.GDelegate0 value2 = (GControl2.GDelegate0)GControl2.smethod_1(gdelegate2, value);
				gdelegate = Interlocked.CompareExchange<GControl2.GDelegate0>(ref this.gdelegate0_0, value2, gdelegate2);
			}
			while (gdelegate != gdelegate2);
		}
		[CompilerGenerated]
		remove
		{
			GControl2.GDelegate0 gdelegate = this.gdelegate0_0;
			GControl2.GDelegate0 gdelegate2;
			do
			{
				gdelegate2 = gdelegate;
				GControl2.GDelegate0 value2 = (GControl2.GDelegate0)GControl2.smethod_2(gdelegate2, value);
				gdelegate = Interlocked.CompareExchange<GControl2.GDelegate0>(ref this.gdelegate0_0, value2, gdelegate2);
			}
			while (gdelegate != gdelegate2);
		}
	}

	// Token: 0x0600008F RID: 143 RVA: 0x000024E7 File Offset: 0x000006E7
	protected virtual void OnClick(EventArgs e)
	{
		this.bool_0 = !this.bool_0;
		if (this.gdelegate0_0 != null)
		{
			this.gdelegate0_0(this);
		}
		base.OnClick(e);
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x06000090 RID: 144 RVA: 0x00002513 File Offset: 0x00000713
	// (set) Token: 0x06000091 RID: 145 RVA: 0x0000251B File Offset: 0x0000071B
	[Category("Options")]
	public GControl2.GEnum1 GEnum1_0
	{
		get
		{
			return this.genum1_0;
		}
		set
		{
			this.genum1_0 = value;
		}
	}

	// Token: 0x06000092 RID: 146 RVA: 0x00002524 File Offset: 0x00000724
	protected virtual void OnResize(EventArgs e)
	{
		base.OnResize(e);
		GControl2.smethod_3(this, 22);
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x06000093 RID: 147 RVA: 0x00002535 File Offset: 0x00000735
	// (set) Token: 0x06000094 RID: 148 RVA: 0x0000253D File Offset: 0x0000073D
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

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x06000095 RID: 149 RVA: 0x00002546 File Offset: 0x00000746
	// (set) Token: 0x06000096 RID: 150 RVA: 0x0000254E File Offset: 0x0000074E
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

	// Token: 0x06000097 RID: 151 RVA: 0x00002557 File Offset: 0x00000757
	protected virtual void OnMouseDown(MouseEventArgs e)
	{
		base.OnMouseDown(e);
		this.genum5_0 = GEnum5.Down;
		GControl2.smethod_0(this);
	}

	// Token: 0x06000098 RID: 152 RVA: 0x0000256D File Offset: 0x0000076D
	protected virtual void OnMouseUp(MouseEventArgs e)
	{
		base.OnMouseUp(e);
		this.genum5_0 = GEnum5.Over;
		GControl2.smethod_0(this);
	}

	// Token: 0x06000099 RID: 153 RVA: 0x00002583 File Offset: 0x00000783
	protected virtual void OnMouseEnter(EventArgs e)
	{
		base.OnMouseEnter(e);
		this.genum5_0 = GEnum5.Over;
		GControl2.smethod_0(this);
	}

	// Token: 0x0600009A RID: 154 RVA: 0x00002599 File Offset: 0x00000799
	protected virtual void OnMouseLeave(EventArgs e)
	{
		base.OnMouseLeave(e);
		this.genum5_0 = GEnum5.None;
		GControl2.smethod_0(this);
	}

	// Token: 0x0600009B RID: 155 RVA: 0x000056DC File Offset: 0x000038DC
	public GControl2()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		GControl2.smethod_4(this, Color.FromArgb(60, 70, 73));
		GControl2.smethod_6(this, GControl2.smethod_5());
		GControl2.smethod_8(this, GControl2.smethod_7("Segoe UI", 10f));
		base.Size = new Size(112, 22);
	}

	// Token: 0x0600009C RID: 156 RVA: 0x00005778 File Offset: 0x00003978
	protected virtual void OnPaint(PaintEventArgs e)
	{
		this.method_0();
		Bitmap bitmap = GControl2.smethod_11(GControl2.smethod_9(this), GControl2.smethod_10(this));
		Graphics graphics = GControl2.smethod_12(bitmap);
		this.int_0 = GControl2.smethod_9(this) - 1;
		this.int_1 = GControl2.smethod_10(this) - 1;
		Rectangle rectangle = new Rectangle(0, 2, GControl2.smethod_10(this) - 5, GControl2.smethod_10(this) - 5);
		Graphics graphics2 = graphics;
		GControl2.smethod_13(graphics2, SmoothingMode.HighQuality);
		GControl2.smethod_14(graphics2, TextRenderingHint.ClearTypeGridFit);
		GControl2.smethod_16(graphics2, GControl2.smethod_15(this));
		GControl2.GEnum1 genum = this.genum1_0;
		if (genum != GControl2.GEnum1.Style1)
		{
			if (genum == GControl2.GEnum1.Style2)
			{
				graphics2.FillRectangle(new SolidBrush(this.color_0), rectangle);
				GEnum5 genum2 = this.genum5_0;
				if (genum2 == GEnum5.Over)
				{
					graphics2.DrawRectangle(new Pen(this.color_2), rectangle);
					graphics2.FillRectangle(new SolidBrush(Color.FromArgb(118, 213, 170)), rectangle);
				}
				else if (genum2 == GEnum5.Down)
				{
					graphics2.DrawRectangle(new Pen(this.color_2), rectangle);
					graphics2.FillRectangle(new SolidBrush(Color.FromArgb(118, 213, 170)), rectangle);
				}
				if (this.Boolean_0)
				{
					graphics2.DrawString("ü", new Font("Wingdings", 18f), new SolidBrush(this.color_2), new Rectangle(5, 7, this.int_1 - 9, this.int_1 - 9), GClass6.stringFormat_1);
				}
				if (!base.Enabled)
				{
					graphics2.FillRectangle(new SolidBrush(Color.FromArgb(54, 58, 61)), rectangle);
					graphics2.DrawString(this.Text, this.Font, new SolidBrush(Color.FromArgb(48, 119, 91)), new Rectangle(20, 2, this.int_0, this.int_1), GClass6.stringFormat_0);
				}
				graphics2.DrawString(this.Text, this.Font, new SolidBrush(this.color_1), new Rectangle(20, 2, this.int_0, this.int_1), GClass6.stringFormat_0);
			}
		}
		else
		{
			GControl2.smethod_18(graphics2, GControl2.smethod_17(this.color_0), rectangle);
			GEnum5 genum2 = this.genum5_0;
			if (genum2 == GEnum5.Over)
			{
				GControl2.smethod_20(graphics2, GControl2.smethod_19(this.color_2), rectangle);
			}
			else if (genum2 == GEnum5.Down)
			{
				GControl2.smethod_20(graphics2, GControl2.smethod_19(this.color_2), rectangle);
			}
			if (this.Boolean_0)
			{
				graphics2.DrawString("ü", GControl2.smethod_7("Wingdings", 18f), GControl2.smethod_17(this.color_2), new Rectangle(5, 7, this.int_1 - 9, this.int_1 - 9), GClass6.stringFormat_1);
			}
			if (!base.Enabled)
			{
				graphics2.FillRectangle(new SolidBrush(Color.FromArgb(54, 58, 61)), rectangle);
				graphics2.DrawString(this.Text, this.Font, new SolidBrush(Color.FromArgb(140, 142, 143)), new Rectangle(20, 2, this.int_0, this.int_1), GClass6.stringFormat_0);
			}
			graphics2.DrawString(this.Text, this.Font, new SolidBrush(this.color_1), new Rectangle(20, 2, this.int_0, this.int_1), GClass6.stringFormat_0);
		}
		base.OnPaint(e);
		graphics.Dispose();
		e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
		bitmap.Dispose();
	}

	// Token: 0x0600009D RID: 157 RVA: 0x00005AEC File Offset: 0x00003CEC
	private void method_0()
	{
		GClass0 gclass = GClass6.smethod_3(this);
		this.color_2 = gclass.color_0;
	}

	// Token: 0x0600009E RID: 158 RVA: 0x0000232F File Offset: 0x0000052F
	static void smethod_0(Control control_0)
	{
		control_0.Invalidate();
	}

	// Token: 0x0600009F RID: 159 RVA: 0x000025AF File Offset: 0x000007AF
	static Delegate smethod_1(Delegate delegate_0, Delegate delegate_1)
	{
		return Delegate.Combine(delegate_0, delegate_1);
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x000025B8 File Offset: 0x000007B8
	static Delegate smethod_2(Delegate delegate_0, Delegate delegate_1)
	{
		return Delegate.Remove(delegate_0, delegate_1);
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x00002337 File Offset: 0x00000537
	static void smethod_3(Control control_0, int int_2)
	{
		control_0.Height = int_2;
	}

	// Token: 0x060000A2 RID: 162 RVA: 0x00002372 File Offset: 0x00000572
	static void smethod_4(Control control_0, Color color_3)
	{
		control_0.BackColor = color_3;
	}

	// Token: 0x060000A3 RID: 163 RVA: 0x000025C1 File Offset: 0x000007C1
	static Cursor smethod_5()
	{
		return Cursors.Hand;
	}

	// Token: 0x060000A4 RID: 164 RVA: 0x000025C8 File Offset: 0x000007C8
	static void smethod_6(Control control_0, Cursor cursor_0)
	{
		control_0.Cursor = cursor_0;
	}

	// Token: 0x060000A5 RID: 165 RVA: 0x000025D1 File Offset: 0x000007D1
	static Font smethod_7(string string_0, float float_0)
	{
		return new Font(string_0, float_0);
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x000025DA File Offset: 0x000007DA
	static void smethod_8(Control control_0, Font font_0)
	{
		control_0.Font = font_0;
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x0000237B File Offset: 0x0000057B
	static int smethod_9(Control control_0)
	{
		return control_0.Width;
	}

	// Token: 0x060000A8 RID: 168 RVA: 0x00002383 File Offset: 0x00000583
	static int smethod_10(Control control_0)
	{
		return control_0.Height;
	}

	// Token: 0x060000A9 RID: 169 RVA: 0x0000238B File Offset: 0x0000058B
	static Bitmap smethod_11(int int_2, int int_3)
	{
		return new Bitmap(int_2, int_3);
	}

	// Token: 0x060000AA RID: 170 RVA: 0x00002394 File Offset: 0x00000594
	static Graphics smethod_12(Image image_0)
	{
		return Graphics.FromImage(image_0);
	}

	// Token: 0x060000AB RID: 171 RVA: 0x0000239C File Offset: 0x0000059C
	static void smethod_13(Graphics graphics_0, SmoothingMode smoothingMode_0)
	{
		graphics_0.SmoothingMode = smoothingMode_0;
	}

	// Token: 0x060000AC RID: 172 RVA: 0x000023A5 File Offset: 0x000005A5
	static void smethod_14(Graphics graphics_0, TextRenderingHint textRenderingHint_0)
	{
		graphics_0.TextRenderingHint = textRenderingHint_0;
	}

	// Token: 0x060000AD RID: 173 RVA: 0x000023AE File Offset: 0x000005AE
	static Color smethod_15(Control control_0)
	{
		return control_0.BackColor;
	}

	// Token: 0x060000AE RID: 174 RVA: 0x000023B6 File Offset: 0x000005B6
	static void smethod_16(Graphics graphics_0, Color color_3)
	{
		graphics_0.Clear(color_3);
	}

	// Token: 0x060000AF RID: 175 RVA: 0x000023BF File Offset: 0x000005BF
	static SolidBrush smethod_17(Color color_3)
	{
		return new SolidBrush(color_3);
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x000023C7 File Offset: 0x000005C7
	static void smethod_18(Graphics graphics_0, Brush brush_0, Rectangle rectangle_0)
	{
		graphics_0.FillRectangle(brush_0, rectangle_0);
	}

	// Token: 0x060000B1 RID: 177 RVA: 0x000025E3 File Offset: 0x000007E3
	static Pen smethod_19(Color color_3)
	{
		return new Pen(color_3);
	}

	// Token: 0x060000B2 RID: 178 RVA: 0x000025EB File Offset: 0x000007EB
	static void smethod_20(Graphics graphics_0, Pen pen_0, Rectangle rectangle_0)
	{
		graphics_0.DrawRectangle(pen_0, rectangle_0);
	}

	// Token: 0x04000018 RID: 24
	private int int_0;

	// Token: 0x04000019 RID: 25
	private int int_1;

	// Token: 0x0400001A RID: 26
	private GEnum5 genum5_0;

	// Token: 0x0400001B RID: 27
	private GControl2.GEnum1 genum1_0;

	// Token: 0x0400001C RID: 28
	private bool bool_0;

	// Token: 0x0400001D RID: 29
	[CompilerGenerated]
	private GControl2.GDelegate0 gdelegate0_0;

	// Token: 0x0400001E RID: 30
	private Color color_0 = Color.FromArgb(45, 47, 49);

	// Token: 0x0400001F RID: 31
	private Color color_1 = Color.FromArgb(243, 243, 243);

	// Token: 0x04000020 RID: 32
	private Color color_2 = GClass6.color_0;

	// Token: 0x02000007 RID: 7
	// (Invoke) Token: 0x060000B4 RID: 180
	public delegate void GDelegate0(object sender);

	// Token: 0x02000008 RID: 8
	[Flags]
	public enum GEnum1
	{
		// Token: 0x04000022 RID: 34
		Style1 = 0,
		// Token: 0x04000023 RID: 35
		Style2 = 1
	}
}
