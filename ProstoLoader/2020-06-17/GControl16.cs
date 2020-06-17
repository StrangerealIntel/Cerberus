using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

// Token: 0x0200001D RID: 29
[DefaultEvent("CheckedChanged")]
public class GControl16 : Control
{
	// Token: 0x14000003 RID: 3
	// (add) Token: 0x06000315 RID: 789 RVA: 0x00008B00 File Offset: 0x00006D00
	// (remove) Token: 0x06000316 RID: 790 RVA: 0x00008B3C File Offset: 0x00006D3C
	public event GControl16.GDelegate2 Event_0
	{
		[CompilerGenerated]
		add
		{
			GControl16.GDelegate2 gdelegate = this.gdelegate2_0;
			GControl16.GDelegate2 gdelegate2;
			do
			{
				gdelegate2 = gdelegate;
				GControl16.GDelegate2 value2 = (GControl16.GDelegate2)GControl16.smethod_0(gdelegate2, value);
				gdelegate = Interlocked.CompareExchange<GControl16.GDelegate2>(ref this.gdelegate2_0, value2, gdelegate2);
			}
			while (gdelegate != gdelegate2);
		}
		[CompilerGenerated]
		remove
		{
			GControl16.GDelegate2 gdelegate = this.gdelegate2_0;
			GControl16.GDelegate2 gdelegate2;
			do
			{
				gdelegate2 = gdelegate;
				GControl16.GDelegate2 value2 = (GControl16.GDelegate2)GControl16.smethod_1(gdelegate2, value);
				gdelegate = Interlocked.CompareExchange<GControl16.GDelegate2>(ref this.gdelegate2_0, value2, gdelegate2);
			}
			while (gdelegate != gdelegate2);
		}
	}

	// Token: 0x17000055 RID: 85
	// (get) Token: 0x06000317 RID: 791 RVA: 0x000033B1 File Offset: 0x000015B1
	// (set) Token: 0x06000318 RID: 792 RVA: 0x000033B9 File Offset: 0x000015B9
	[Category("Options")]
	public GControl16.GEnum3 GEnum3_0
	{
		get
		{
			return this.genum3_0;
		}
		set
		{
			this.genum3_0 = value;
		}
	}

	// Token: 0x17000056 RID: 86
	// (get) Token: 0x06000319 RID: 793 RVA: 0x000033C2 File Offset: 0x000015C2
	// (set) Token: 0x0600031A RID: 794 RVA: 0x000033CA File Offset: 0x000015CA
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

	// Token: 0x0600031B RID: 795 RVA: 0x000033D3 File Offset: 0x000015D3
	protected virtual void OnTextChanged(EventArgs e)
	{
		base.OnTextChanged(e);
		GControl16.smethod_2(this);
	}

	// Token: 0x0600031C RID: 796 RVA: 0x000033E2 File Offset: 0x000015E2
	protected virtual void OnResize(EventArgs e)
	{
		base.OnResize(e);
		GControl16.smethod_3(this, 76);
		GControl16.smethod_4(this, 33);
	}

	// Token: 0x0600031D RID: 797 RVA: 0x000033FB File Offset: 0x000015FB
	protected virtual void OnMouseEnter(EventArgs e)
	{
		base.OnMouseEnter(e);
		this.genum5_0 = GEnum5.Over;
		GControl16.smethod_2(this);
	}

	// Token: 0x0600031E RID: 798 RVA: 0x00003411 File Offset: 0x00001611
	protected virtual void OnMouseDown(MouseEventArgs e)
	{
		base.OnMouseDown(e);
		this.genum5_0 = GEnum5.Down;
		GControl16.smethod_2(this);
	}

	// Token: 0x0600031F RID: 799 RVA: 0x00003427 File Offset: 0x00001627
	protected virtual void OnMouseLeave(EventArgs e)
	{
		base.OnMouseLeave(e);
		this.genum5_0 = GEnum5.None;
		GControl16.smethod_2(this);
	}

	// Token: 0x06000320 RID: 800 RVA: 0x0000343D File Offset: 0x0000163D
	protected virtual void OnMouseUp(MouseEventArgs e)
	{
		base.OnMouseUp(e);
		this.genum5_0 = GEnum5.Over;
		GControl16.smethod_2(this);
	}

	// Token: 0x06000321 RID: 801 RVA: 0x00003453 File Offset: 0x00001653
	protected virtual void OnClick(EventArgs e)
	{
		base.OnClick(e);
		this.bool_0 = !this.bool_0;
		if (this.gdelegate2_0 != null)
		{
			this.gdelegate2_0(this);
		}
	}

	// Token: 0x06000322 RID: 802 RVA: 0x00008B78 File Offset: 0x00006D78
	public GControl16()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		GControl16.smethod_5(this, Color.Transparent);
		base.Size = new Size(44, GControl16.smethod_6(this) + 1);
		this.Cursor = Cursors.Hand;
		this.Font = new Font("Segoe UI", 10f);
		base.Size = new Size(76, 33);
	}

	// Token: 0x06000323 RID: 803 RVA: 0x00008C48 File Offset: 0x00006E48
	protected virtual void OnPaint(PaintEventArgs e)
	{
		this.method_0();
		Bitmap bitmap = GControl16.smethod_8(GControl16.smethod_7(this), GControl16.smethod_6(this));
		Graphics graphics = GControl16.smethod_9(bitmap);
		this.int_0 = GControl16.smethod_7(this) - 1;
		this.int_1 = GControl16.smethod_6(this) - 1;
		GraphicsPath graphicsPath = GControl16.smethod_10();
		GraphicsPath graphicsPath2 = GControl16.smethod_10();
		Rectangle rectangle_ = new Rectangle(0, 0, this.int_0, this.int_1);
		Rectangle rectangle = new Rectangle(GControl16.smethod_11(this.int_0 / 2), 0, 38, this.int_1);
		Graphics graphics2 = graphics;
		GControl16.smethod_12(graphics2, SmoothingMode.HighQuality);
		GControl16.smethod_13(graphics2, PixelOffsetMode.HighQuality);
		GControl16.smethod_14(graphics2, TextRenderingHint.ClearTypeGridFit);
		GControl16.smethod_16(graphics2, GControl16.smethod_15(this));
		switch (this.genum3_0)
		{
		case GControl16.GEnum3.Style1:
			graphicsPath = GClass6.smethod_0(rectangle_, 6);
			graphicsPath2 = GClass6.smethod_0(rectangle, 6);
			GControl16.smethod_18(graphics2, GControl16.smethod_17(this.color_2), graphicsPath);
			GControl16.smethod_18(graphics2, GControl16.smethod_17(this.color_3), graphicsPath2);
			graphics2.DrawString("OFF", GControl16.smethod_19(this), GControl16.smethod_17(this.color_2), new Rectangle(19, 1, this.int_0, this.int_1), GClass6.stringFormat_1);
			if (this.Boolean_0)
			{
				graphicsPath = GClass6.smethod_0(rectangle_, 6);
				graphicsPath2 = GClass6.smethod_0(new Rectangle(Convert.ToInt32(this.int_0 / 2), 0, 38, this.int_1), 6);
				graphics2.FillPath(new SolidBrush(this.color_3), graphicsPath);
				graphics2.FillPath(new SolidBrush(this.color_0), graphicsPath2);
				graphics2.DrawString("ON", this.Font, new SolidBrush(this.color_0), new Rectangle(8, 7, this.int_0, this.int_1), GClass6.stringFormat_0);
			}
			break;
		case GControl16.GEnum3.Style2:
			graphicsPath = GClass6.smethod_0(rectangle_, 6);
			rectangle = new Rectangle(4, 4, 36, this.int_1 - 8);
			graphicsPath2 = GClass6.smethod_0(rectangle, 4);
			graphics2.FillPath(new SolidBrush(this.color_1), graphicsPath);
			graphics2.FillPath(new SolidBrush(this.color_3), graphicsPath2);
			graphics2.DrawLine(new Pen(this.color_2), 18, 20, 18, 12);
			graphics2.DrawLine(new Pen(this.color_2), 22, 20, 22, 12);
			graphics2.DrawLine(new Pen(this.color_2), 26, 20, 26, 12);
			graphics2.DrawString("r", new Font("Marlett", 8f), new SolidBrush(this.color_4), new Rectangle(19, 2, base.Width, base.Height), GClass6.stringFormat_1);
			if (this.Boolean_0)
			{
				graphicsPath = GClass6.smethod_0(rectangle_, 6);
				rectangle = new Rectangle(Convert.ToInt32(this.int_0 / 2) - 2, 4, 36, this.int_1 - 8);
				graphicsPath2 = GClass6.smethod_0(rectangle, 4);
				graphics2.FillPath(new SolidBrush(this.color_0), graphicsPath);
				graphics2.FillPath(new SolidBrush(this.color_3), graphicsPath2);
				graphics2.DrawLine(new Pen(this.color_2), Convert.ToInt32(this.int_0 / 2) + 12, 20, Convert.ToInt32(this.int_0 / 2) + 12, 12);
				graphics2.DrawLine(new Pen(this.color_2), Convert.ToInt32(this.int_0 / 2) + 16, 20, Convert.ToInt32(this.int_0 / 2) + 16, 12);
				graphics2.DrawLine(new Pen(this.color_2), Convert.ToInt32(this.int_0 / 2) + 20, 20, Convert.ToInt32(this.int_0 / 2) + 20, 12);
				graphics2.DrawString("ü", new Font("Wingdings", 14f), new SolidBrush(this.color_4), new Rectangle(8, 7, base.Width, base.Height), GClass6.stringFormat_0);
			}
			break;
		case GControl16.GEnum3.Style3:
			graphicsPath = GClass6.smethod_0(rectangle_, 16);
			rectangle = new Rectangle(this.int_0 - 28, 4, 22, this.int_1 - 8);
			graphicsPath2.AddEllipse(rectangle);
			graphics2.FillPath(new SolidBrush(this.color_3), graphicsPath);
			graphics2.FillPath(new SolidBrush(this.color_1), graphicsPath2);
			graphics2.DrawString("OFF", this.Font, new SolidBrush(this.color_1), new Rectangle(-12, 2, this.int_0, this.int_1), GClass6.stringFormat_1);
			if (this.Boolean_0)
			{
				graphicsPath = GClass6.smethod_0(rectangle_, 16);
				rectangle = new Rectangle(6, 4, 22, this.int_1 - 8);
				graphicsPath2.Reset();
				graphicsPath2.AddEllipse(rectangle);
				graphics2.FillPath(new SolidBrush(this.color_3), graphicsPath);
				graphics2.FillPath(new SolidBrush(this.color_0), graphicsPath2);
				graphics2.DrawString("ON", this.Font, new SolidBrush(this.color_0), new Rectangle(12, 2, this.int_0, this.int_1), GClass6.stringFormat_1);
			}
			break;
		case GControl16.GEnum3.Style4:
			if (!this.Boolean_0)
			{
			}
			break;
		case GControl16.GEnum3.Style5:
		{
			bool boolean_ = this.Boolean_0;
			break;
		}
		}
		base.OnPaint(e);
		graphics.Dispose();
		e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
		bitmap.Dispose();
	}

	// Token: 0x06000324 RID: 804 RVA: 0x000091BC File Offset: 0x000073BC
	private void method_0()
	{
		GClass0 gclass = GClass6.smethod_3(this);
		this.color_0 = gclass.color_0;
	}

	// Token: 0x06000325 RID: 805 RVA: 0x000025AF File Offset: 0x000007AF
	static Delegate smethod_0(Delegate delegate_0, Delegate delegate_1)
	{
		return Delegate.Combine(delegate_0, delegate_1);
	}

	// Token: 0x06000326 RID: 806 RVA: 0x000025B8 File Offset: 0x000007B8
	static Delegate smethod_1(Delegate delegate_0, Delegate delegate_1)
	{
		return Delegate.Remove(delegate_0, delegate_1);
	}

	// Token: 0x06000327 RID: 807 RVA: 0x0000232F File Offset: 0x0000052F
	static void smethod_2(Control control_0)
	{
		control_0.Invalidate();
	}

	// Token: 0x06000328 RID: 808 RVA: 0x0000276E File Offset: 0x0000096E
	static void smethod_3(Control control_0, int int_2)
	{
		control_0.Width = int_2;
	}

	// Token: 0x06000329 RID: 809 RVA: 0x00002337 File Offset: 0x00000537
	static void smethod_4(Control control_0, int int_2)
	{
		control_0.Height = int_2;
	}

	// Token: 0x0600032A RID: 810 RVA: 0x00002372 File Offset: 0x00000572
	static void smethod_5(Control control_0, Color color_5)
	{
		control_0.BackColor = color_5;
	}

	// Token: 0x0600032B RID: 811 RVA: 0x00002383 File Offset: 0x00000583
	static int smethod_6(Control control_0)
	{
		return control_0.Height;
	}

	// Token: 0x0600032C RID: 812 RVA: 0x0000237B File Offset: 0x0000057B
	static int smethod_7(Control control_0)
	{
		return control_0.Width;
	}

	// Token: 0x0600032D RID: 813 RVA: 0x0000238B File Offset: 0x0000058B
	static Bitmap smethod_8(int int_2, int int_3)
	{
		return new Bitmap(int_2, int_3);
	}

	// Token: 0x0600032E RID: 814 RVA: 0x00002394 File Offset: 0x00000594
	static Graphics smethod_9(Image image_0)
	{
		return Graphics.FromImage(image_0);
	}

	// Token: 0x0600032F RID: 815 RVA: 0x0000245C File Offset: 0x0000065C
	static GraphicsPath smethod_10()
	{
		return new GraphicsPath();
	}

	// Token: 0x06000330 RID: 816 RVA: 0x000028D4 File Offset: 0x00000AD4
	static int smethod_11(int int_2)
	{
		return Convert.ToInt32(int_2);
	}

	// Token: 0x06000331 RID: 817 RVA: 0x0000239C File Offset: 0x0000059C
	static void smethod_12(Graphics graphics_0, SmoothingMode smoothingMode_0)
	{
		graphics_0.SmoothingMode = smoothingMode_0;
	}

	// Token: 0x06000332 RID: 818 RVA: 0x00002463 File Offset: 0x00000663
	static void smethod_13(Graphics graphics_0, PixelOffsetMode pixelOffsetMode_0)
	{
		graphics_0.PixelOffsetMode = pixelOffsetMode_0;
	}

	// Token: 0x06000333 RID: 819 RVA: 0x000023A5 File Offset: 0x000005A5
	static void smethod_14(Graphics graphics_0, TextRenderingHint textRenderingHint_0)
	{
		graphics_0.TextRenderingHint = textRenderingHint_0;
	}

	// Token: 0x06000334 RID: 820 RVA: 0x000023AE File Offset: 0x000005AE
	static Color smethod_15(Control control_0)
	{
		return control_0.BackColor;
	}

	// Token: 0x06000335 RID: 821 RVA: 0x000023B6 File Offset: 0x000005B6
	static void smethod_16(Graphics graphics_0, Color color_5)
	{
		graphics_0.Clear(color_5);
	}

	// Token: 0x06000336 RID: 822 RVA: 0x000023BF File Offset: 0x000005BF
	static SolidBrush smethod_17(Color color_5)
	{
		return new SolidBrush(color_5);
	}

	// Token: 0x06000337 RID: 823 RVA: 0x0000246C File Offset: 0x0000066C
	static void smethod_18(Graphics graphics_0, Brush brush_0, GraphicsPath graphicsPath_0)
	{
		graphics_0.FillPath(brush_0, graphicsPath_0);
	}

	// Token: 0x06000338 RID: 824 RVA: 0x0000247E File Offset: 0x0000067E
	static Font smethod_19(Control control_0)
	{
		return control_0.Font;
	}

	// Token: 0x04000090 RID: 144
	private int int_0;

	// Token: 0x04000091 RID: 145
	private int int_1;

	// Token: 0x04000092 RID: 146
	private GControl16.GEnum3 genum3_0;

	// Token: 0x04000093 RID: 147
	private bool bool_0;

	// Token: 0x04000094 RID: 148
	private GEnum5 genum5_0;

	// Token: 0x04000095 RID: 149
	[CompilerGenerated]
	private GControl16.GDelegate2 gdelegate2_0;

	// Token: 0x04000096 RID: 150
	private Color color_0 = GClass6.color_0;

	// Token: 0x04000097 RID: 151
	private Color color_1 = Color.FromArgb(220, 85, 96);

	// Token: 0x04000098 RID: 152
	private Color color_2 = Color.FromArgb(84, 85, 86);

	// Token: 0x04000099 RID: 153
	private Color color_3 = Color.FromArgb(45, 47, 49);

	// Token: 0x0400009A RID: 154
	private Color color_4 = Color.FromArgb(243, 243, 243);

	// Token: 0x0200001E RID: 30
	// (Invoke) Token: 0x0600033A RID: 826
	public delegate void GDelegate2(object sender);

	// Token: 0x0200001F RID: 31
	[Flags]
	public enum GEnum3
	{
		// Token: 0x0400009C RID: 156
		Style1 = 0,
		// Token: 0x0400009D RID: 157
		Style2 = 1,
		// Token: 0x0400009E RID: 158
		Style3 = 2,
		// Token: 0x0400009F RID: 159
		Style4 = 3,
		// Token: 0x040000A0 RID: 160
		Style5 = 4
	}
}
