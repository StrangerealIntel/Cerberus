using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;

// Token: 0x02000020 RID: 32
[DefaultEvent("Scroll")]
public class GControl17 : Control
{
	// Token: 0x0600033D RID: 829 RVA: 0x000091DC File Offset: 0x000073DC
	protected virtual void OnMouseDown(MouseEventArgs e)
	{
		base.OnMouseDown(e);
		if (GControl17.smethod_0(e) == MouseButtons.Left)
		{
			this.int_2 = GControl17.smethod_2((float)(this.int_5 - this.int_3) / (float)(this.int_4 - this.int_3) * (float)(GControl17.smethod_1(this) - 11));
			this.rectangle_0 = new Rectangle(this.int_2, 0, 10, 20);
			this.bool_0 = this.rectangle_0.Contains(e.Location);
		}
	}

	// Token: 0x0600033E RID: 830 RVA: 0x00009260 File Offset: 0x00007460
	protected virtual void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);
		if (this.bool_0 && GControl17.smethod_3(e) > -1 && GControl17.smethod_3(e) < GControl17.smethod_1(this) + 1)
		{
			this.Int32_2 = this.int_3 + GControl17.smethod_2((float)(this.int_4 - this.int_3) * ((float)GControl17.smethod_3(e) / (float)GControl17.smethod_1(this)));
		}
	}

	// Token: 0x0600033F RID: 831 RVA: 0x0000347F File Offset: 0x0000167F
	protected virtual void OnMouseUp(MouseEventArgs e)
	{
		base.OnMouseUp(e);
		this.bool_0 = false;
	}

	// Token: 0x17000057 RID: 87
	// (get) Token: 0x06000340 RID: 832 RVA: 0x0000348F File Offset: 0x0000168F
	// (set) Token: 0x06000341 RID: 833 RVA: 0x00003497 File Offset: 0x00001697
	public GControl17.GEnum4 GEnum4_0
	{
		get
		{
			return this.genum4_0;
		}
		set
		{
			this.genum4_0 = value;
		}
	}

	// Token: 0x17000058 RID: 88
	// (get) Token: 0x06000342 RID: 834 RVA: 0x000034A0 File Offset: 0x000016A0
	// (set) Token: 0x06000343 RID: 835 RVA: 0x000034A8 File Offset: 0x000016A8
	[Category("Colors")]
	public Color Color_0
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

	// Token: 0x17000059 RID: 89
	// (get) Token: 0x06000344 RID: 836 RVA: 0x000034B1 File Offset: 0x000016B1
	// (set) Token: 0x06000345 RID: 837 RVA: 0x000034B9 File Offset: 0x000016B9
	[Category("Colors")]
	public Color Color_1
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

	// Token: 0x14000004 RID: 4
	// (add) Token: 0x06000346 RID: 838 RVA: 0x000092C8 File Offset: 0x000074C8
	// (remove) Token: 0x06000347 RID: 839 RVA: 0x00009300 File Offset: 0x00007500
	public event GControl17.GDelegate3 Event_0
	{
		[CompilerGenerated]
		add
		{
			GControl17.GDelegate3 gdelegate = this.gdelegate3_0;
			GControl17.GDelegate3 gdelegate2;
			do
			{
				gdelegate2 = gdelegate;
				GControl17.GDelegate3 value2 = (GControl17.GDelegate3)GControl17.smethod_4(gdelegate2, value);
				gdelegate = Interlocked.CompareExchange<GControl17.GDelegate3>(ref this.gdelegate3_0, value2, gdelegate2);
			}
			while (gdelegate != gdelegate2);
		}
		[CompilerGenerated]
		remove
		{
			GControl17.GDelegate3 gdelegate = this.gdelegate3_0;
			GControl17.GDelegate3 gdelegate2;
			do
			{
				gdelegate2 = gdelegate;
				GControl17.GDelegate3 value2 = (GControl17.GDelegate3)GControl17.smethod_5(gdelegate2, value);
				gdelegate = Interlocked.CompareExchange<GControl17.GDelegate3>(ref this.gdelegate3_0, value2, gdelegate2);
			}
			while (gdelegate != gdelegate2);
		}
	}

	// Token: 0x1700005A RID: 90
	// (get) Token: 0x06000348 RID: 840 RVA: 0x000034C2 File Offset: 0x000016C2
	// (set) Token: 0x06000349 RID: 841 RVA: 0x000034C5 File Offset: 0x000016C5
	public int Int32_0
	{
		get
		{
			return 0;
		}
		set
		{
			this.int_3 = value;
			if (value > this.int_5)
			{
				this.int_5 = value;
			}
			if (value > this.int_4)
			{
				this.int_4 = value;
			}
			GControl17.smethod_6(this);
		}
	}

	// Token: 0x1700005B RID: 91
	// (get) Token: 0x0600034A RID: 842 RVA: 0x000034F4 File Offset: 0x000016F4
	// (set) Token: 0x0600034B RID: 843 RVA: 0x000034FC File Offset: 0x000016FC
	public int Int32_1
	{
		get
		{
			return this.int_4;
		}
		set
		{
			this.int_4 = value;
			if (value < this.int_5)
			{
				this.int_5 = value;
			}
			if (value < this.int_3)
			{
				this.int_3 = value;
			}
			GControl17.smethod_6(this);
		}
	}

	// Token: 0x1700005C RID: 92
	// (get) Token: 0x0600034C RID: 844 RVA: 0x0000352B File Offset: 0x0000172B
	// (set) Token: 0x0600034D RID: 845 RVA: 0x00003533 File Offset: 0x00001733
	public int Int32_2
	{
		get
		{
			return this.int_5;
		}
		set
		{
			if (value == this.int_5)
			{
				return;
			}
			if (value > this.int_4)
			{
			}
			this.int_5 = value;
			GControl17.smethod_6(this);
			if (this.gdelegate3_0 != null)
			{
				this.gdelegate3_0(this);
			}
		}
	}

	// Token: 0x1700005D RID: 93
	// (get) Token: 0x0600034E RID: 846 RVA: 0x00003569 File Offset: 0x00001769
	// (set) Token: 0x0600034F RID: 847 RVA: 0x00003571 File Offset: 0x00001771
	public bool Boolean_0
	{
		get
		{
			return this.bool_1;
		}
		set
		{
			this.bool_1 = value;
		}
	}

	// Token: 0x06000350 RID: 848 RVA: 0x00009338 File Offset: 0x00007538
	protected virtual void OnKeyDown(KeyEventArgs e)
	{
		base.OnKeyDown(e);
		if (GControl17.smethod_7(e) != Keys.Subtract)
		{
			if (GControl17.smethod_7(e) == Keys.Add)
			{
				if (this.Int32_2 == this.int_4)
				{
					return;
				}
				this.Int32_2++;
			}
			return;
		}
		if (this.Int32_2 != 0)
		{
			this.Int32_2--;
			return;
		}
	}

	// Token: 0x06000351 RID: 849 RVA: 0x0000357A File Offset: 0x0000177A
	protected virtual void OnTextChanged(EventArgs e)
	{
		base.OnTextChanged(e);
		GControl17.smethod_6(this);
	}

	// Token: 0x06000352 RID: 850 RVA: 0x00003589 File Offset: 0x00001789
	protected virtual void OnResize(EventArgs e)
	{
		base.OnResize(e);
		GControl17.smethod_8(this, 23);
	}

	// Token: 0x06000353 RID: 851 RVA: 0x00009398 File Offset: 0x00007598
	public GControl17()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		GControl17.smethod_8(this, 18);
		GControl17.smethod_9(this, Color.FromArgb(60, 70, 73));
	}

	// Token: 0x06000354 RID: 852 RVA: 0x00009420 File Offset: 0x00007620
	protected virtual void OnPaint(PaintEventArgs e)
	{
		this.method_0();
		Bitmap bitmap = GControl17.smethod_11(GControl17.smethod_1(this), GControl17.smethod_10(this));
		Graphics graphics = GControl17.smethod_12(bitmap);
		this.int_0 = GControl17.smethod_1(this) - 1;
		this.int_1 = GControl17.smethod_10(this) - 1;
		Rectangle rect = new Rectangle(1, 6, this.int_0 - 2, 8);
		GraphicsPath graphicsPath = GControl17.smethod_13();
		GraphicsPath graphicsPath2 = GControl17.smethod_13();
		Graphics graphics2 = graphics;
		GControl17.smethod_14(graphics2, SmoothingMode.HighQuality);
		GControl17.smethod_15(graphics2, PixelOffsetMode.HighQuality);
		GControl17.smethod_16(graphics2, TextRenderingHint.ClearTypeGridFit);
		GControl17.smethod_18(graphics2, GControl17.smethod_17(this));
		this.int_2 = GControl17.smethod_2((float)(this.int_5 - this.int_3) / (float)(this.int_4 - this.int_3) * (float)(this.int_0 - 10));
		this.rectangle_0 = new Rectangle(this.int_2, 0, 10, 20);
		this.rectangle_1 = new Rectangle(this.int_2, 4, 11, 14);
		graphicsPath.AddRectangle(rect);
		graphics2.SetClip(graphicsPath);
		graphics2.FillRectangle(new SolidBrush(this.color_0), new Rectangle(0, 7, this.int_0, 8));
		graphics2.FillRectangle(new SolidBrush(this.color_1), new Rectangle(0, 7, this.rectangle_0.X + this.rectangle_0.Width, 8));
		graphics2.ResetClip();
		HatchBrush brush = new HatchBrush(HatchStyle.Plaid, this.Color_1, this.color_1);
		graphics2.FillRectangle(brush, new Rectangle(-10, 7, this.rectangle_0.X + this.rectangle_0.Width, 8));
		GControl17.GEnum4 genum = this.GEnum4_0;
		if (genum != GControl17.GEnum4.Slider)
		{
			if (genum == GControl17.GEnum4.Knob)
			{
				graphicsPath2.AddEllipse(this.rectangle_1);
				graphics2.FillPath(new SolidBrush(this.color_2), graphicsPath2);
			}
		}
		else
		{
			graphicsPath2.AddRectangle(this.rectangle_0);
			graphics2.FillPath(new SolidBrush(this.color_2), graphicsPath2);
		}
		if (this.Boolean_0)
		{
			graphics2.DrawString(this.Int32_2.ToString(), new Font("Segoe UI", 8f), Brushes.White, new Rectangle(1, 6, this.int_0, this.int_1), new StringFormat
			{
				Alignment = StringAlignment.Far,
				LineAlignment = StringAlignment.Far
			});
		}
		base.OnPaint(e);
		graphics.Dispose();
		e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
		bitmap.Dispose();
	}

	// Token: 0x06000355 RID: 853 RVA: 0x00009694 File Offset: 0x00007894
	private void method_0()
	{
		GClass0 gclass = GClass6.smethod_3(this);
		this.color_1 = gclass.color_0;
	}

	// Token: 0x06000356 RID: 854 RVA: 0x0000359A File Offset: 0x0000179A
	static MouseButtons smethod_0(MouseEventArgs mouseEventArgs_0)
	{
		return mouseEventArgs_0.Button;
	}

	// Token: 0x06000357 RID: 855 RVA: 0x0000237B File Offset: 0x0000057B
	static int smethod_1(Control control_0)
	{
		return control_0.Width;
	}

	// Token: 0x06000358 RID: 856 RVA: 0x000035A2 File Offset: 0x000017A2
	static int smethod_2(float float_0)
	{
		return Convert.ToInt32(float_0);
	}

	// Token: 0x06000359 RID: 857 RVA: 0x0000236A File Offset: 0x0000056A
	static int smethod_3(MouseEventArgs mouseEventArgs_0)
	{
		return mouseEventArgs_0.X;
	}

	// Token: 0x0600035A RID: 858 RVA: 0x000025AF File Offset: 0x000007AF
	static Delegate smethod_4(Delegate delegate_0, Delegate delegate_1)
	{
		return Delegate.Combine(delegate_0, delegate_1);
	}

	// Token: 0x0600035B RID: 859 RVA: 0x000025B8 File Offset: 0x000007B8
	static Delegate smethod_5(Delegate delegate_0, Delegate delegate_1)
	{
		return Delegate.Remove(delegate_0, delegate_1);
	}

	// Token: 0x0600035C RID: 860 RVA: 0x0000232F File Offset: 0x0000052F
	static void smethod_6(Control control_0)
	{
		control_0.Invalidate();
	}

	// Token: 0x0600035D RID: 861 RVA: 0x00002DFD File Offset: 0x00000FFD
	static Keys smethod_7(KeyEventArgs keyEventArgs_0)
	{
		return keyEventArgs_0.KeyCode;
	}

	// Token: 0x0600035E RID: 862 RVA: 0x00002337 File Offset: 0x00000537
	static void smethod_8(Control control_0, int int_6)
	{
		control_0.Height = int_6;
	}

	// Token: 0x0600035F RID: 863 RVA: 0x00002372 File Offset: 0x00000572
	static void smethod_9(Control control_0, Color color_4)
	{
		control_0.BackColor = color_4;
	}

	// Token: 0x06000360 RID: 864 RVA: 0x00002383 File Offset: 0x00000583
	static int smethod_10(Control control_0)
	{
		return control_0.Height;
	}

	// Token: 0x06000361 RID: 865 RVA: 0x0000238B File Offset: 0x0000058B
	static Bitmap smethod_11(int int_6, int int_7)
	{
		return new Bitmap(int_6, int_7);
	}

	// Token: 0x06000362 RID: 866 RVA: 0x00002394 File Offset: 0x00000594
	static Graphics smethod_12(Image image_0)
	{
		return Graphics.FromImage(image_0);
	}

	// Token: 0x06000363 RID: 867 RVA: 0x0000245C File Offset: 0x0000065C
	static GraphicsPath smethod_13()
	{
		return new GraphicsPath();
	}

	// Token: 0x06000364 RID: 868 RVA: 0x0000239C File Offset: 0x0000059C
	static void smethod_14(Graphics graphics_0, SmoothingMode smoothingMode_0)
	{
		graphics_0.SmoothingMode = smoothingMode_0;
	}

	// Token: 0x06000365 RID: 869 RVA: 0x00002463 File Offset: 0x00000663
	static void smethod_15(Graphics graphics_0, PixelOffsetMode pixelOffsetMode_0)
	{
		graphics_0.PixelOffsetMode = pixelOffsetMode_0;
	}

	// Token: 0x06000366 RID: 870 RVA: 0x000023A5 File Offset: 0x000005A5
	static void smethod_16(Graphics graphics_0, TextRenderingHint textRenderingHint_0)
	{
		graphics_0.TextRenderingHint = textRenderingHint_0;
	}

	// Token: 0x06000367 RID: 871 RVA: 0x000023AE File Offset: 0x000005AE
	static Color smethod_17(Control control_0)
	{
		return control_0.BackColor;
	}

	// Token: 0x06000368 RID: 872 RVA: 0x000023B6 File Offset: 0x000005B6
	static void smethod_18(Graphics graphics_0, Color color_4)
	{
		graphics_0.Clear(color_4);
	}

	// Token: 0x040000A1 RID: 161
	private int int_0;

	// Token: 0x040000A2 RID: 162
	private int int_1;

	// Token: 0x040000A3 RID: 163
	private int int_2;

	// Token: 0x040000A4 RID: 164
	private bool bool_0;

	// Token: 0x040000A5 RID: 165
	private Rectangle rectangle_0;

	// Token: 0x040000A6 RID: 166
	private Rectangle rectangle_1;

	// Token: 0x040000A7 RID: 167
	private GControl17.GEnum4 genum4_0;

	// Token: 0x040000A8 RID: 168
	[CompilerGenerated]
	private GControl17.GDelegate3 gdelegate3_0;

	// Token: 0x040000A9 RID: 169
	private int int_3;

	// Token: 0x040000AA RID: 170
	private int int_4 = 10;

	// Token: 0x040000AB RID: 171
	private int int_5;

	// Token: 0x040000AC RID: 172
	private bool bool_1;

	// Token: 0x040000AD RID: 173
	private Color color_0 = Color.FromArgb(45, 47, 49);

	// Token: 0x040000AE RID: 174
	private Color color_1 = GClass6.color_0;

	// Token: 0x040000AF RID: 175
	private Color color_2 = Color.FromArgb(25, 27, 29);

	// Token: 0x040000B0 RID: 176
	private Color color_3 = Color.FromArgb(23, 148, 92);

	// Token: 0x02000021 RID: 33
	[Flags]
	public enum GEnum4
	{
		// Token: 0x040000B2 RID: 178
		Slider = 0,
		// Token: 0x040000B3 RID: 179
		Knob = 1
	}

	// Token: 0x02000022 RID: 34
	// (Invoke) Token: 0x0600036A RID: 874
	public delegate void GDelegate3(object sender);
}
