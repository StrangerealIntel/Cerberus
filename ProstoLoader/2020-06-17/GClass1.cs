using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

// Token: 0x0200000C RID: 12
public class GClass1 : ComboBox
{
	// Token: 0x060000F7 RID: 247 RVA: 0x0000278A File Offset: 0x0000098A
	protected virtual void OnMouseDown(MouseEventArgs e)
	{
		base.OnMouseDown(e);
		this.genum5_0 = GEnum5.Down;
		GClass1.smethod_0(this);
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x000027A0 File Offset: 0x000009A0
	protected virtual void OnMouseUp(MouseEventArgs e)
	{
		base.OnMouseUp(e);
		this.genum5_0 = GEnum5.Over;
		GClass1.smethod_0(this);
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x000027B6 File Offset: 0x000009B6
	protected virtual void OnMouseEnter(EventArgs e)
	{
		base.OnMouseEnter(e);
		this.genum5_0 = GEnum5.Over;
		GClass1.smethod_0(this);
	}

	// Token: 0x060000FA RID: 250 RVA: 0x000027CC File Offset: 0x000009CC
	protected virtual void OnMouseLeave(EventArgs e)
	{
		base.OnMouseLeave(e);
		this.genum5_0 = GEnum5.None;
		GClass1.smethod_0(this);
	}

	// Token: 0x060000FB RID: 251 RVA: 0x00005FA8 File Offset: 0x000041A8
	protected virtual void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);
		this.int_3 = GClass1.smethod_1(e).X;
		this.int_4 = e.Location.Y;
		base.Invalidate();
		if (e.X >= base.Width - 41)
		{
			this.Cursor = Cursors.Hand;
			return;
		}
		this.Cursor = Cursors.IBeam;
	}

	// Token: 0x060000FC RID: 252 RVA: 0x000027E2 File Offset: 0x000009E2
	protected virtual void OnDrawItem(DrawItemEventArgs e)
	{
		base.OnDrawItem(e);
		GClass1.smethod_0(this);
		if ((GClass1.smethod_2(e) & DrawItemState.Selected) == DrawItemState.Selected)
		{
			GClass1.smethod_0(this);
		}
	}

	// Token: 0x060000FD RID: 253 RVA: 0x00002802 File Offset: 0x00000A02
	protected virtual void OnClick(EventArgs e)
	{
		base.OnClick(e);
		GClass1.smethod_0(this);
	}

	// Token: 0x17000017 RID: 23
	// (get) Token: 0x060000FE RID: 254 RVA: 0x00002811 File Offset: 0x00000A11
	// (set) Token: 0x060000FF RID: 255 RVA: 0x00002819 File Offset: 0x00000A19
	[Category("Colors")]
	public Color Color_0
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

	// Token: 0x17000018 RID: 24
	// (get) Token: 0x06000100 RID: 256 RVA: 0x00002822 File Offset: 0x00000A22
	// (set) Token: 0x06000101 RID: 257 RVA: 0x00006014 File Offset: 0x00004214
	private int Int32_0
	{
		get
		{
			return this.int_2;
		}
		set
		{
			this.int_2 = value;
			try
			{
				this.method_1(value);
			}
			catch
			{
			}
			GClass1.smethod_0(this);
		}
	}

	// Token: 0x06000102 RID: 258 RVA: 0x0000604C File Offset: 0x0000424C
	public void method_0(object sender, DrawItemEventArgs e)
	{
		if (GClass1.smethod_3(e) < 0)
		{
			return;
		}
		GClass1.smethod_4(e);
		GClass1.smethod_5(e);
		GClass1.smethod_7(GClass1.smethod_6(e), SmoothingMode.HighQuality);
		GClass1.smethod_8(GClass1.smethod_6(e), PixelOffsetMode.HighQuality);
		GClass1.smethod_9(GClass1.smethod_6(e), TextRenderingHint.ClearTypeGridFit);
		GClass1.smethod_10(GClass1.smethod_6(e), InterpolationMode.HighQualityBicubic);
		if ((GClass1.smethod_2(e) & DrawItemState.Selected) == DrawItemState.Selected)
		{
			GClass1.smethod_13(GClass1.smethod_6(e), GClass1.smethod_11(this.color_2), GClass1.smethod_12(e));
		}
		else
		{
			GClass1.smethod_13(GClass1.smethod_6(e), GClass1.smethod_11(this.color_0), GClass1.smethod_12(e));
		}
		GClass1.smethod_6(e).DrawString(GClass1.smethod_16(this, GClass1.smethod_15(GClass1.smethod_14(this), GClass1.smethod_3(e))), GClass1.smethod_17("Segoe UI", 8f), GClass1.smethod_18(), new Rectangle(GClass1.smethod_12(e).X + 2, e.Bounds.Y + 2, e.Bounds.Width, e.Bounds.Height));
		e.Graphics.Dispose();
	}

	// Token: 0x06000103 RID: 259 RVA: 0x0000282A File Offset: 0x00000A2A
	protected virtual void OnResize(EventArgs e)
	{
		base.OnResize(e);
		GClass1.smethod_19(this, 18);
	}

	// Token: 0x06000104 RID: 260 RVA: 0x0000616C File Offset: 0x0000436C
	public GClass1()
	{
		GClass1.smethod_20(this, new DrawItemEventHandler(this.method_0));
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		GClass1.smethod_21(this, DrawMode.OwnerDrawFixed);
		GClass1.smethod_22(this, Color.FromArgb(45, 45, 48));
		GClass1.smethod_23(this, Color.White);
		GClass1.smethod_24(this, ComboBoxStyle.DropDownList);
		GClass1.smethod_26(this, GClass1.smethod_25());
		this.Int32_0 = 0;
		GClass1.smethod_27(this, 18);
		GClass1.smethod_29(this, GClass1.smethod_28("Segoe UI", 8f, FontStyle.Regular));
	}

	// Token: 0x06000105 RID: 261 RVA: 0x00006234 File Offset: 0x00004434
	protected virtual void OnPaint(PaintEventArgs e)
	{
		Bitmap bitmap = GClass1.smethod_32(GClass1.smethod_30(this), GClass1.smethod_31(this));
		Graphics graphics = GClass1.smethod_33(bitmap);
		this.int_0 = GClass1.smethod_30(this);
		this.int_1 = GClass1.smethod_31(this);
		Rectangle rectangle_ = new Rectangle(0, 0, this.int_0, this.int_1);
		Rectangle rectangle_2 = new Rectangle(GClass1.smethod_34(this.int_0 - 40), 0, this.int_0, this.int_1);
		GraphicsPath graphicsPath_ = GClass1.smethod_35();
		GClass1.smethod_35();
		GClass1.smethod_36(graphics, Color.FromArgb(45, 45, 48));
		GClass1.smethod_7(graphics, SmoothingMode.HighQuality);
		GClass1.smethod_8(graphics, PixelOffsetMode.HighQuality);
		GClass1.smethod_9(graphics, TextRenderingHint.ClearTypeGridFit);
		GClass1.smethod_13(graphics, GClass1.smethod_11(this.color_1), rectangle_);
		GClass1.smethod_37(graphicsPath_);
		GClass1.smethod_38(graphicsPath_, rectangle_2);
		GClass1.smethod_39(graphics, graphicsPath_);
		GClass1.smethod_13(graphics, GClass1.smethod_11(this.color_0), rectangle_2);
		GClass1.smethod_40(graphics);
		GClass1.smethod_42(graphics, GClass1.smethod_41(), this.int_0 - 10, 6, this.int_0 - 30, 6);
		GClass1.smethod_42(graphics, GClass1.smethod_41(), this.int_0 - 10, 12, this.int_0 - 30, 12);
		GClass1.smethod_42(graphics, GClass1.smethod_41(), this.int_0 - 10, 18, this.int_0 - 30, 18);
		graphics.DrawString(GClass1.smethod_43(this), GClass1.smethod_44(this), GClass1.smethod_18(), new Point(4, 6), GClass6.stringFormat_0);
		graphics.Dispose();
		e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
		bitmap.Dispose();
	}

	// Token: 0x06000106 RID: 262 RVA: 0x0000232F File Offset: 0x0000052F
	static void smethod_0(Control control_0)
	{
		control_0.Invalidate();
	}

	// Token: 0x06000107 RID: 263 RVA: 0x0000283B File Offset: 0x00000A3B
	static Point smethod_1(MouseEventArgs mouseEventArgs_0)
	{
		return mouseEventArgs_0.Location;
	}

	// Token: 0x06000108 RID: 264 RVA: 0x00002843 File Offset: 0x00000A43
	static DrawItemState smethod_2(DrawItemEventArgs drawItemEventArgs_0)
	{
		return drawItemEventArgs_0.State;
	}

	// Token: 0x06000109 RID: 265 RVA: 0x0000284B File Offset: 0x00000A4B
	void method_1(int int_5)
	{
		base.SelectedIndex = int_5;
	}

	// Token: 0x0600010A RID: 266 RVA: 0x00002854 File Offset: 0x00000A54
	static int smethod_3(DrawItemEventArgs drawItemEventArgs_0)
	{
		return drawItemEventArgs_0.Index;
	}

	// Token: 0x0600010B RID: 267 RVA: 0x0000285C File Offset: 0x00000A5C
	static void smethod_4(DrawItemEventArgs drawItemEventArgs_0)
	{
		drawItemEventArgs_0.DrawBackground();
	}

	// Token: 0x0600010C RID: 268 RVA: 0x00002864 File Offset: 0x00000A64
	static void smethod_5(DrawItemEventArgs drawItemEventArgs_0)
	{
		drawItemEventArgs_0.DrawFocusRectangle();
	}

	// Token: 0x0600010D RID: 269 RVA: 0x0000286C File Offset: 0x00000A6C
	static Graphics smethod_6(DrawItemEventArgs drawItemEventArgs_0)
	{
		return drawItemEventArgs_0.Graphics;
	}

	// Token: 0x0600010E RID: 270 RVA: 0x0000239C File Offset: 0x0000059C
	static void smethod_7(Graphics graphics_0, SmoothingMode smoothingMode_0)
	{
		graphics_0.SmoothingMode = smoothingMode_0;
	}

	// Token: 0x0600010F RID: 271 RVA: 0x00002463 File Offset: 0x00000663
	static void smethod_8(Graphics graphics_0, PixelOffsetMode pixelOffsetMode_0)
	{
		graphics_0.PixelOffsetMode = pixelOffsetMode_0;
	}

	// Token: 0x06000110 RID: 272 RVA: 0x000023A5 File Offset: 0x000005A5
	static void smethod_9(Graphics graphics_0, TextRenderingHint textRenderingHint_0)
	{
		graphics_0.TextRenderingHint = textRenderingHint_0;
	}

	// Token: 0x06000111 RID: 273 RVA: 0x000024A5 File Offset: 0x000006A5
	static void smethod_10(Graphics graphics_0, InterpolationMode interpolationMode_0)
	{
		graphics_0.InterpolationMode = interpolationMode_0;
	}

	// Token: 0x06000112 RID: 274 RVA: 0x000023BF File Offset: 0x000005BF
	static SolidBrush smethod_11(Color color_3)
	{
		return new SolidBrush(color_3);
	}

	// Token: 0x06000113 RID: 275 RVA: 0x00002874 File Offset: 0x00000A74
	static Rectangle smethod_12(DrawItemEventArgs drawItemEventArgs_0)
	{
		return drawItemEventArgs_0.Bounds;
	}

	// Token: 0x06000114 RID: 276 RVA: 0x000023C7 File Offset: 0x000005C7
	static void smethod_13(Graphics graphics_0, Brush brush_0, Rectangle rectangle_0)
	{
		graphics_0.FillRectangle(brush_0, rectangle_0);
	}

	// Token: 0x06000115 RID: 277 RVA: 0x0000287C File Offset: 0x00000A7C
	static ComboBox.ObjectCollection smethod_14(ComboBox comboBox_0)
	{
		return comboBox_0.Items;
	}

	// Token: 0x06000116 RID: 278 RVA: 0x00002884 File Offset: 0x00000A84
	static object smethod_15(ComboBox.ObjectCollection objectCollection_0, int int_5)
	{
		return objectCollection_0[int_5];
	}

	// Token: 0x06000117 RID: 279 RVA: 0x0000288D File Offset: 0x00000A8D
	static string smethod_16(ListControl listControl_0, object object_0)
	{
		return listControl_0.GetItemText(object_0);
	}

	// Token: 0x06000118 RID: 280 RVA: 0x000025D1 File Offset: 0x000007D1
	static Font smethod_17(string string_0, float float_0)
	{
		return new Font(string_0, float_0);
	}

	// Token: 0x06000119 RID: 281 RVA: 0x00002896 File Offset: 0x00000A96
	static Brush smethod_18()
	{
		return Brushes.White;
	}

	// Token: 0x0600011A RID: 282 RVA: 0x00002337 File Offset: 0x00000537
	static void smethod_19(Control control_0, int int_5)
	{
		control_0.Height = int_5;
	}

	// Token: 0x0600011B RID: 283 RVA: 0x0000289D File Offset: 0x00000A9D
	static void smethod_20(ComboBox comboBox_0, DrawItemEventHandler drawItemEventHandler_0)
	{
		comboBox_0.DrawItem += drawItemEventHandler_0;
	}

	// Token: 0x0600011C RID: 284 RVA: 0x000028A6 File Offset: 0x00000AA6
	static void smethod_21(ComboBox comboBox_0, DrawMode drawMode_0)
	{
		comboBox_0.DrawMode = drawMode_0;
	}

	// Token: 0x0600011D RID: 285 RVA: 0x00002372 File Offset: 0x00000572
	static void smethod_22(Control control_0, Color color_3)
	{
		control_0.BackColor = color_3;
	}

	// Token: 0x0600011E RID: 286 RVA: 0x000028AF File Offset: 0x00000AAF
	static void smethod_23(Control control_0, Color color_3)
	{
		control_0.ForeColor = color_3;
	}

	// Token: 0x0600011F RID: 287 RVA: 0x000028B8 File Offset: 0x00000AB8
	static void smethod_24(ComboBox comboBox_0, ComboBoxStyle comboBoxStyle_0)
	{
		comboBox_0.DropDownStyle = comboBoxStyle_0;
	}

	// Token: 0x06000120 RID: 288 RVA: 0x000025C1 File Offset: 0x000007C1
	static Cursor smethod_25()
	{
		return Cursors.Hand;
	}

	// Token: 0x06000121 RID: 289 RVA: 0x000025C8 File Offset: 0x000007C8
	static void smethod_26(Control control_0, Cursor cursor_0)
	{
		control_0.Cursor = cursor_0;
	}

	// Token: 0x06000122 RID: 290 RVA: 0x000028C1 File Offset: 0x00000AC1
	static void smethod_27(ComboBox comboBox_0, int int_5)
	{
		comboBox_0.ItemHeight = int_5;
	}

	// Token: 0x06000123 RID: 291 RVA: 0x000028CA File Offset: 0x00000ACA
	static Font smethod_28(string string_0, float float_0, FontStyle fontStyle_0)
	{
		return new Font(string_0, float_0, fontStyle_0);
	}

	// Token: 0x06000124 RID: 292 RVA: 0x000025DA File Offset: 0x000007DA
	static void smethod_29(Control control_0, Font font_0)
	{
		control_0.Font = font_0;
	}

	// Token: 0x06000125 RID: 293 RVA: 0x0000237B File Offset: 0x0000057B
	static int smethod_30(Control control_0)
	{
		return control_0.Width;
	}

	// Token: 0x06000126 RID: 294 RVA: 0x00002383 File Offset: 0x00000583
	static int smethod_31(Control control_0)
	{
		return control_0.Height;
	}

	// Token: 0x06000127 RID: 295 RVA: 0x0000238B File Offset: 0x0000058B
	static Bitmap smethod_32(int int_5, int int_6)
	{
		return new Bitmap(int_5, int_6);
	}

	// Token: 0x06000128 RID: 296 RVA: 0x00002394 File Offset: 0x00000594
	static Graphics smethod_33(Image image_0)
	{
		return Graphics.FromImage(image_0);
	}

	// Token: 0x06000129 RID: 297 RVA: 0x000028D4 File Offset: 0x00000AD4
	static int smethod_34(int int_5)
	{
		return Convert.ToInt32(int_5);
	}

	// Token: 0x0600012A RID: 298 RVA: 0x0000245C File Offset: 0x0000065C
	static GraphicsPath smethod_35()
	{
		return new GraphicsPath();
	}

	// Token: 0x0600012B RID: 299 RVA: 0x000023B6 File Offset: 0x000005B6
	static void smethod_36(Graphics graphics_0, Color color_3)
	{
		graphics_0.Clear(color_3);
	}

	// Token: 0x0600012C RID: 300 RVA: 0x000028DC File Offset: 0x00000ADC
	static void smethod_37(GraphicsPath graphicsPath_0)
	{
		graphicsPath_0.Reset();
	}

	// Token: 0x0600012D RID: 301 RVA: 0x000028E4 File Offset: 0x00000AE4
	static void smethod_38(GraphicsPath graphicsPath_0, Rectangle rectangle_0)
	{
		graphicsPath_0.AddRectangle(rectangle_0);
	}

	// Token: 0x0600012E RID: 302 RVA: 0x000028ED File Offset: 0x00000AED
	static void smethod_39(Graphics graphics_0, GraphicsPath graphicsPath_0)
	{
		graphics_0.SetClip(graphicsPath_0);
	}

	// Token: 0x0600012F RID: 303 RVA: 0x000028F6 File Offset: 0x00000AF6
	static void smethod_40(Graphics graphics_0)
	{
		graphics_0.ResetClip();
	}

	// Token: 0x06000130 RID: 304 RVA: 0x000028FE File Offset: 0x00000AFE
	static Pen smethod_41()
	{
		return Pens.White;
	}

	// Token: 0x06000131 RID: 305 RVA: 0x00002905 File Offset: 0x00000B05
	static void smethod_42(Graphics graphics_0, Pen pen_0, int int_5, int int_6, int int_7, int int_8)
	{
		graphics_0.DrawLine(pen_0, int_5, int_6, int_7, int_8);
	}

	// Token: 0x06000132 RID: 306 RVA: 0x00002476 File Offset: 0x00000676
	static string smethod_43(Control control_0)
	{
		return control_0.Text;
	}

	// Token: 0x06000133 RID: 307 RVA: 0x0000247E File Offset: 0x0000067E
	static Font smethod_44(Control control_0)
	{
		return control_0.Font;
	}

	// Token: 0x04000034 RID: 52
	private int int_0;

	// Token: 0x04000035 RID: 53
	private int int_1;

	// Token: 0x04000036 RID: 54
	private int int_2;

	// Token: 0x04000037 RID: 55
	private int int_3;

	// Token: 0x04000038 RID: 56
	private int int_4;

	// Token: 0x04000039 RID: 57
	private GEnum5 genum5_0;

	// Token: 0x0400003A RID: 58
	private Color color_0 = Color.FromArgb(25, 27, 29);

	// Token: 0x0400003B RID: 59
	private Color color_1 = Color.FromArgb(45, 47, 49);

	// Token: 0x0400003C RID: 60
	private Color color_2 = Color.FromArgb(35, 168, 109);
}
