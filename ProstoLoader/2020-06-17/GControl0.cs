using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

// Token: 0x02000003 RID: 3
public class GControl0 : Control
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x0600003A RID: 58 RVA: 0x000021B1 File Offset: 0x000003B1
	// (set) Token: 0x0600003B RID: 59 RVA: 0x00004BD0 File Offset: 0x00002DD0
	private Timer Timer_0
	{
		get
		{
			return this.timer_0;
		}
		set
		{
			if (this.timer_0 != null)
			{
				GControl0.smethod_0(this.timer_0, new EventHandler(this.method_1));
			}
			this.timer_0 = value;
			if (this.timer_0 != null)
			{
				GControl0.smethod_1(this.timer_0, new EventHandler(this.method_1));
			}
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x0600003C RID: 60 RVA: 0x000021B9 File Offset: 0x000003B9
	// (set) Token: 0x0600003D RID: 61 RVA: 0x000021C1 File Offset: 0x000003C1
	[Category("Options")]
	public GControl0.GEnum0 GEnum0_0
	{
		get
		{
			return this.genum0_0;
		}
		set
		{
			this.genum0_0 = value;
		}
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x0600003E RID: 62 RVA: 0x000021CA File Offset: 0x000003CA
	// (set) Token: 0x0600003F RID: 63 RVA: 0x000021D2 File Offset: 0x000003D2
	[Category("Options")]
	public virtual string Text
	{
		get
		{
			return this.method_2();
		}
		set
		{
			this.method_3(value);
			if (this.string_0 != null)
			{
				this.string_0 = value;
			}
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000040 RID: 64 RVA: 0x000021EA File Offset: 0x000003EA
	// (set) Token: 0x06000041 RID: 65 RVA: 0x000021F5 File Offset: 0x000003F5
	[Category("Options")]
	public bool Boolean_0
	{
		get
		{
			return !GControl0.smethod_2(this);
		}
		set
		{
			GControl0.smethod_3(this, value);
		}
	}

	// Token: 0x06000042 RID: 66 RVA: 0x000021FE File Offset: 0x000003FE
	protected virtual void OnTextChanged(EventArgs e)
	{
		base.OnTextChanged(e);
		GControl0.smethod_4(this);
	}

	// Token: 0x06000043 RID: 67 RVA: 0x0000220D File Offset: 0x0000040D
	protected virtual void OnResize(EventArgs e)
	{
		base.OnResize(e);
		GControl0.smethod_5(this, 42);
	}

	// Token: 0x06000044 RID: 68 RVA: 0x0000221E File Offset: 0x0000041E
	public void method_0(GControl0.GEnum0 genum0_1, string string_1, int int_3)
	{
		this.genum0_0 = genum0_1;
		GControl0.smethod_6(this, string_1);
		this.Boolean_0 = true;
		this.Timer_0 = GControl0.smethod_7();
		GControl0.smethod_8(this.Timer_0, int_3);
		GControl0.smethod_9(this.Timer_0, true);
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00002258 File Offset: 0x00000458
	private void method_1(object sender, EventArgs e)
	{
		this.Boolean_0 = false;
		GControl0.smethod_9(this.Timer_0, false);
		GControl0.smethod_10(this.Timer_0);
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00002278 File Offset: 0x00000478
	protected virtual void OnMouseDown(MouseEventArgs e)
	{
		base.OnMouseDown(e);
		this.genum5_0 = GEnum5.Down;
		GControl0.smethod_4(this);
	}

	// Token: 0x06000047 RID: 71 RVA: 0x0000228E File Offset: 0x0000048E
	protected virtual void OnMouseUp(MouseEventArgs e)
	{
		base.OnMouseUp(e);
		this.genum5_0 = GEnum5.Over;
		GControl0.smethod_4(this);
	}

	// Token: 0x06000048 RID: 72 RVA: 0x000022A4 File Offset: 0x000004A4
	protected virtual void OnMouseEnter(EventArgs e)
	{
		base.OnMouseEnter(e);
		this.genum5_0 = GEnum5.Over;
		GControl0.smethod_4(this);
	}

	// Token: 0x06000049 RID: 73 RVA: 0x000022BA File Offset: 0x000004BA
	protected virtual void OnMouseLeave(EventArgs e)
	{
		base.OnMouseLeave(e);
		this.genum5_0 = GEnum5.None;
		GControl0.smethod_4(this);
	}

	// Token: 0x0600004A RID: 74 RVA: 0x000022D0 File Offset: 0x000004D0
	protected virtual void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);
		this.int_2 = GControl0.smethod_11(e);
		GControl0.smethod_4(this);
	}

	// Token: 0x0600004B RID: 75 RVA: 0x000022EB File Offset: 0x000004EB
	protected virtual void OnClick(EventArgs e)
	{
		base.OnClick(e);
		this.Boolean_0 = false;
	}

	// Token: 0x0600004C RID: 76 RVA: 0x00004C24 File Offset: 0x00002E24
	public GControl0()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		GControl0.smethod_12(this, Color.FromArgb(60, 70, 73));
		base.Size = new Size(576, 42);
		base.Location = new Point(10, 61);
		this.Font = new Font("Segoe UI", 10f);
		this.Cursor = Cursors.Hand;
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00004D14 File Offset: 0x00002F14
	protected virtual void OnPaint(PaintEventArgs e)
	{
		Bitmap bitmap = GControl0.smethod_15(GControl0.smethod_13(this), GControl0.smethod_14(this));
		Graphics graphics = GControl0.smethod_16(bitmap);
		this.int_0 = GControl0.smethod_13(this) - 1;
		this.int_1 = GControl0.smethod_14(this) - 1;
		Rectangle rectangle = new Rectangle(0, 0, this.int_0, this.int_1);
		Graphics graphics2 = graphics;
		GControl0.smethod_17(graphics2, SmoothingMode.HighQuality);
		GControl0.smethod_18(graphics2, TextRenderingHint.ClearTypeGridFit);
		GControl0.smethod_20(graphics2, GControl0.smethod_19(this));
		switch (this.genum0_0)
		{
		case GControl0.GEnum0.Success:
			GControl0.smethod_22(graphics2, GControl0.smethod_21(this.color_0), rectangle);
			graphics2.FillEllipse(GControl0.smethod_21(this.color_1), new Rectangle(8, 9, 24, 24));
			graphics2.FillEllipse(new SolidBrush(this.color_0), new Rectangle(10, 11, 20, 20));
			graphics2.DrawString("ü", new Font("Wingdings", 22f), new SolidBrush(this.color_1), new Rectangle(7, 7, this.int_0, this.int_1), GClass6.stringFormat_0);
			graphics2.DrawString(this.Text, this.Font, new SolidBrush(this.color_1), new Rectangle(48, 12, this.int_0, this.int_1), GClass6.stringFormat_0);
			graphics2.FillEllipse(new SolidBrush(Color.FromArgb(35, Color.Black)), new Rectangle(this.int_0 - 30, this.int_1 - 29, 17, 17));
			graphics2.DrawString("r", new Font("Marlett", 8f), new SolidBrush(this.color_0), new Rectangle(this.int_0 - 28, 16, this.int_0, this.int_1), GClass6.stringFormat_0);
			if (this.genum5_0 == GEnum5.Over)
			{
				graphics2.DrawString("r", new Font("Marlett", 8f), new SolidBrush(Color.FromArgb(25, Color.White)), new Rectangle(this.int_0 - 28, 16, this.int_0, this.int_1), GClass6.stringFormat_0);
			}
			break;
		case GControl0.GEnum0.Error:
			graphics2.FillRectangle(new SolidBrush(this.color_2), rectangle);
			graphics2.FillEllipse(new SolidBrush(this.color_3), new Rectangle(8, 9, 24, 24));
			graphics2.FillEllipse(new SolidBrush(this.color_2), new Rectangle(10, 11, 20, 20));
			graphics2.DrawString("r", new Font("Marlett", 16f), new SolidBrush(this.color_3), new Rectangle(6, 11, this.int_0, this.int_1), GClass6.stringFormat_0);
			graphics2.DrawString(this.Text, this.Font, new SolidBrush(this.color_3), new Rectangle(48, 12, this.int_0, this.int_1), GClass6.stringFormat_0);
			graphics2.FillEllipse(new SolidBrush(Color.FromArgb(35, Color.Black)), new Rectangle(this.int_0 - 32, this.int_1 - 29, 17, 17));
			graphics2.DrawString("r", new Font("Marlett", 8f), new SolidBrush(this.color_2), new Rectangle(this.int_0 - 30, 17, this.int_0, this.int_1), GClass6.stringFormat_0);
			if (this.genum5_0 == GEnum5.Over)
			{
				graphics2.DrawString("r", new Font("Marlett", 8f), new SolidBrush(Color.FromArgb(25, Color.White)), new Rectangle(this.int_0 - 30, 15, this.int_0, this.int_1), GClass6.stringFormat_0);
			}
			break;
		case GControl0.GEnum0.Info:
			graphics2.FillRectangle(new SolidBrush(this.color_4), rectangle);
			graphics2.FillEllipse(new SolidBrush(this.color_5), new Rectangle(8, 9, 24, 24));
			graphics2.FillEllipse(new SolidBrush(this.color_4), new Rectangle(10, 11, 20, 20));
			graphics2.DrawString("¡", new Font("Segoe UI", 20f, FontStyle.Bold), new SolidBrush(this.color_5), new Rectangle(12, -4, this.int_0, this.int_1), GClass6.stringFormat_0);
			graphics2.DrawString(this.Text, this.Font, new SolidBrush(this.color_5), new Rectangle(48, 12, this.int_0, this.int_1), GClass6.stringFormat_0);
			graphics2.FillEllipse(new SolidBrush(Color.FromArgb(35, Color.Black)), new Rectangle(this.int_0 - 32, this.int_1 - 29, 17, 17));
			graphics2.DrawString("r", new Font("Marlett", 8f), new SolidBrush(this.color_4), new Rectangle(this.int_0 - 30, 17, this.int_0, this.int_1), GClass6.stringFormat_0);
			if (this.genum5_0 == GEnum5.Over)
			{
				graphics2.DrawString("r", new Font("Marlett", 8f), new SolidBrush(Color.FromArgb(25, Color.White)), new Rectangle(this.int_0 - 30, 17, this.int_0, this.int_1), GClass6.stringFormat_0);
			}
			break;
		}
		base.OnPaint(e);
		graphics.Dispose();
		e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
		bitmap.Dispose();
	}

	// Token: 0x0600004E RID: 78 RVA: 0x000022FB File Offset: 0x000004FB
	static void smethod_0(Timer timer_1, EventHandler eventHandler_0)
	{
		timer_1.Tick -= eventHandler_0;
	}

	// Token: 0x0600004F RID: 79 RVA: 0x00002304 File Offset: 0x00000504
	static void smethod_1(Timer timer_1, EventHandler eventHandler_0)
	{
		timer_1.Tick += eventHandler_0;
	}

	// Token: 0x06000050 RID: 80 RVA: 0x0000230D File Offset: 0x0000050D
	string method_2()
	{
		return base.Text;
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00002315 File Offset: 0x00000515
	void method_3(string string_1)
	{
		base.Text = string_1;
	}

	// Token: 0x06000052 RID: 82 RVA: 0x0000231E File Offset: 0x0000051E
	static bool smethod_2(Control control_0)
	{
		return control_0.Visible;
	}

	// Token: 0x06000053 RID: 83 RVA: 0x00002326 File Offset: 0x00000526
	static void smethod_3(Control control_0, bool bool_0)
	{
		control_0.Visible = bool_0;
	}

	// Token: 0x06000054 RID: 84 RVA: 0x0000232F File Offset: 0x0000052F
	static void smethod_4(Control control_0)
	{
		control_0.Invalidate();
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00002337 File Offset: 0x00000537
	static void smethod_5(Control control_0, int int_3)
	{
		control_0.Height = int_3;
	}

	// Token: 0x06000056 RID: 86 RVA: 0x00002340 File Offset: 0x00000540
	static void smethod_6(Control control_0, string string_1)
	{
		control_0.Text = string_1;
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00002349 File Offset: 0x00000549
	static Timer smethod_7()
	{
		return new Timer();
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00002350 File Offset: 0x00000550
	static void smethod_8(Timer timer_1, int int_3)
	{
		timer_1.Interval = int_3;
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00002359 File Offset: 0x00000559
	static void smethod_9(Timer timer_1, bool bool_0)
	{
		timer_1.Enabled = bool_0;
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00002362 File Offset: 0x00000562
	static void smethod_10(Component component_0)
	{
		component_0.Dispose();
	}

	// Token: 0x0600005B RID: 91 RVA: 0x0000236A File Offset: 0x0000056A
	static int smethod_11(MouseEventArgs mouseEventArgs_0)
	{
		return mouseEventArgs_0.X;
	}

	// Token: 0x0600005C RID: 92 RVA: 0x00002372 File Offset: 0x00000572
	static void smethod_12(Control control_0, Color color_6)
	{
		control_0.BackColor = color_6;
	}

	// Token: 0x0600005D RID: 93 RVA: 0x0000237B File Offset: 0x0000057B
	static int smethod_13(Control control_0)
	{
		return control_0.Width;
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00002383 File Offset: 0x00000583
	static int smethod_14(Control control_0)
	{
		return control_0.Height;
	}

	// Token: 0x0600005F RID: 95 RVA: 0x0000238B File Offset: 0x0000058B
	static Bitmap smethod_15(int int_3, int int_4)
	{
		return new Bitmap(int_3, int_4);
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00002394 File Offset: 0x00000594
	static Graphics smethod_16(Image image_0)
	{
		return Graphics.FromImage(image_0);
	}

	// Token: 0x06000061 RID: 97 RVA: 0x0000239C File Offset: 0x0000059C
	static void smethod_17(Graphics graphics_0, SmoothingMode smoothingMode_0)
	{
		graphics_0.SmoothingMode = smoothingMode_0;
	}

	// Token: 0x06000062 RID: 98 RVA: 0x000023A5 File Offset: 0x000005A5
	static void smethod_18(Graphics graphics_0, TextRenderingHint textRenderingHint_0)
	{
		graphics_0.TextRenderingHint = textRenderingHint_0;
	}

	// Token: 0x06000063 RID: 99 RVA: 0x000023AE File Offset: 0x000005AE
	static Color smethod_19(Control control_0)
	{
		return control_0.BackColor;
	}

	// Token: 0x06000064 RID: 100 RVA: 0x000023B6 File Offset: 0x000005B6
	static void smethod_20(Graphics graphics_0, Color color_6)
	{
		graphics_0.Clear(color_6);
	}

	// Token: 0x06000065 RID: 101 RVA: 0x000023BF File Offset: 0x000005BF
	static SolidBrush smethod_21(Color color_6)
	{
		return new SolidBrush(color_6);
	}

	// Token: 0x06000066 RID: 102 RVA: 0x000023C7 File Offset: 0x000005C7
	static void smethod_22(Graphics graphics_0, Brush brush_0, Rectangle rectangle_0)
	{
		graphics_0.FillRectangle(brush_0, rectangle_0);
	}

	// Token: 0x04000001 RID: 1
	private int int_0;

	// Token: 0x04000002 RID: 2
	private int int_1;

	// Token: 0x04000003 RID: 3
	private GControl0.GEnum0 genum0_0;

	// Token: 0x04000004 RID: 4
	private string string_0;

	// Token: 0x04000005 RID: 5
	private GEnum5 genum5_0;

	// Token: 0x04000006 RID: 6
	private int int_2;

	// Token: 0x04000007 RID: 7
	private Timer timer_0;

	// Token: 0x04000008 RID: 8
	private Color color_0 = Color.FromArgb(60, 85, 79);

	// Token: 0x04000009 RID: 9
	private Color color_1 = Color.FromArgb(35, 169, 110);

	// Token: 0x0400000A RID: 10
	private Color color_2 = Color.FromArgb(87, 71, 71);

	// Token: 0x0400000B RID: 11
	private Color color_3 = Color.FromArgb(254, 142, 122);

	// Token: 0x0400000C RID: 12
	private Color color_4 = Color.FromArgb(70, 91, 94);

	// Token: 0x0400000D RID: 13
	private Color color_5 = Color.FromArgb(97, 185, 186);

	// Token: 0x02000004 RID: 4
	[Flags]
	public enum GEnum0
	{
		// Token: 0x0400000F RID: 15
		Success = 0,
		// Token: 0x04000010 RID: 16
		Error = 1,
		// Token: 0x04000011 RID: 17
		Info = 2
	}
}
