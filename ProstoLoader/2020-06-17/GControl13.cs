using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

// Token: 0x0200001A RID: 26
public class GControl13 : Control
{
	// Token: 0x06000284 RID: 644 RVA: 0x00003068 File Offset: 0x00001268
	protected virtual void OnMouseDown(MouseEventArgs e)
	{
		base.OnMouseDown(e);
		this.genum5_0 = GEnum5.Down;
		GControl13.smethod_0(this);
	}

	// Token: 0x06000285 RID: 645 RVA: 0x0000307E File Offset: 0x0000127E
	protected virtual void OnMouseUp(MouseEventArgs e)
	{
		base.OnMouseUp(e);
		this.genum5_0 = GEnum5.Over;
		GControl13.smethod_0(this);
	}

	// Token: 0x06000286 RID: 646 RVA: 0x00003094 File Offset: 0x00001294
	protected virtual void OnMouseEnter(EventArgs e)
	{
		base.OnMouseEnter(e);
		this.genum5_0 = GEnum5.Over;
		GControl13.smethod_0(this);
	}

	// Token: 0x06000287 RID: 647 RVA: 0x000030AA File Offset: 0x000012AA
	protected virtual void OnMouseLeave(EventArgs e)
	{
		base.OnMouseLeave(e);
		this.genum5_0 = GEnum5.None;
		GControl13.smethod_0(this);
	}

	// Token: 0x06000288 RID: 648 RVA: 0x00007CE8 File Offset: 0x00005EE8
	private bool[] method_0()
	{
		bool[] array = new bool[4];
		using (IEnumerator enumerator = GControl13.smethod_3(GControl13.smethod_2(GControl13.smethod_1(this))))
		{
			while (enumerator.MoveNext())
			{
				Control control = (Control)GControl13.smethod_4(enumerator);
				if (control is GControl13 && control != this && this.Rectangle_0.IntersectsWith(this.Rectangle_0))
				{
					double num = Math.Atan2((double)(base.Left - control.Left), (double)(base.Top - control.Top)) * 2.0 / 3.1415926535897931;
					if (num / 1.0 == num)
					{
						array[(int)num + 1] = true;
					}
				}
			}
		}
		return array;
	}

	// Token: 0x17000045 RID: 69
	// (get) Token: 0x06000289 RID: 649 RVA: 0x000030C0 File Offset: 0x000012C0
	private Rectangle Rectangle_0
	{
		get
		{
			return new Rectangle(GControl13.smethod_5(this), GControl13.smethod_6(this), GControl13.smethod_7(this), GControl13.smethod_8(this));
		}
	}

	// Token: 0x17000046 RID: 70
	// (get) Token: 0x0600028A RID: 650 RVA: 0x000030DF File Offset: 0x000012DF
	// (set) Token: 0x0600028B RID: 651 RVA: 0x000030E7 File Offset: 0x000012E7
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

	// Token: 0x17000047 RID: 71
	// (get) Token: 0x0600028C RID: 652 RVA: 0x000030F0 File Offset: 0x000012F0
	// (set) Token: 0x0600028D RID: 653 RVA: 0x000030F8 File Offset: 0x000012F8
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

	// Token: 0x17000048 RID: 72
	// (get) Token: 0x0600028E RID: 654 RVA: 0x00003101 File Offset: 0x00001301
	// (set) Token: 0x0600028F RID: 655 RVA: 0x00003109 File Offset: 0x00001309
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

	// Token: 0x06000290 RID: 656 RVA: 0x00003112 File Offset: 0x00001312
	protected virtual void OnResize(EventArgs e)
	{
		base.OnResize(e);
	}

	// Token: 0x06000291 RID: 657 RVA: 0x0000311B File Offset: 0x0000131B
	protected virtual void OnCreateControl()
	{
		base.OnCreateControl();
	}

	// Token: 0x06000292 RID: 658 RVA: 0x00007DC0 File Offset: 0x00005FC0
	public GControl13()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		base.Size = new Size(106, 32);
		this.BackColor = Color.Transparent;
		this.Font = new Font("Segoe UI", 12f);
		this.Cursor = Cursors.Hand;
	}

	// Token: 0x06000293 RID: 659 RVA: 0x00007E48 File Offset: 0x00006048
	protected virtual void OnPaint(PaintEventArgs e)
	{
		this.method_1();
		Bitmap image_ = GControl13.smethod_9(GControl13.smethod_7(this), GControl13.smethod_8(this));
		Graphics graphics = GControl13.smethod_10(image_);
		this.int_0 = GControl13.smethod_7(this);
		this.int_1 = GControl13.smethod_8(this);
		GraphicsPath graphicsPath_ = GControl13.smethod_11();
		bool[] array = this.method_0();
		GraphicsPath graphicsPath = GClass6.smethod_1(0f, 0f, (float)this.int_0, (float)this.int_1, 0.3, !array[2] && !array[1], !array[1] && !array[0], !array[3] && !array[0], !array[3] && !array[2]);
		Rectangle rectangle = new Rectangle(0, 0, this.int_0, this.int_1);
		Graphics graphics_ = graphics;
		GControl13.smethod_12(graphics_, SmoothingMode.HighQuality);
		GControl13.smethod_13(graphics_, PixelOffsetMode.HighQuality);
		GControl13.smethod_14(graphics_, TextRenderingHint.ClearTypeGridFit);
		GControl13.smethod_16(graphics_, GControl13.smethod_15(this));
		switch (this.genum5_0)
		{
		case GEnum5.None:
			if (this.Boolean_0)
			{
				graphicsPath_ = graphicsPath;
				GControl13.smethod_18(graphics_, GControl13.smethod_17(this.color_0), graphicsPath_);
				GControl13.smethod_21(graphics_, GControl13.smethod_19(this), GControl13.smethod_20(this), GControl13.smethod_17(this.color_1), rectangle, GClass6.stringFormat_1);
			}
			else
			{
				GControl13.smethod_22(graphics_, GControl13.smethod_17(this.color_0), rectangle);
				GControl13.smethod_21(graphics_, GControl13.smethod_19(this), GControl13.smethod_20(this), GControl13.smethod_17(this.color_1), rectangle, GClass6.stringFormat_1);
			}
			break;
		case GEnum5.Over:
			if (this.Boolean_0)
			{
				graphicsPath_ = graphicsPath;
				GControl13.smethod_18(graphics_, GControl13.smethod_17(this.color_0), graphicsPath_);
				GControl13.smethod_18(graphics_, GControl13.smethod_17(Color.FromArgb(20, Color.White)), graphicsPath_);
				GControl13.smethod_21(graphics_, GControl13.smethod_19(this), GControl13.smethod_20(this), GControl13.smethod_17(this.color_1), rectangle, GClass6.stringFormat_1);
			}
			else
			{
				GControl13.smethod_22(graphics_, GControl13.smethod_17(this.color_0), rectangle);
				GControl13.smethod_22(graphics_, GControl13.smethod_17(Color.FromArgb(20, Color.White)), rectangle);
				GControl13.smethod_21(graphics_, GControl13.smethod_19(this), GControl13.smethod_20(this), GControl13.smethod_17(this.color_1), rectangle, GClass6.stringFormat_1);
			}
			break;
		case GEnum5.Down:
			if (!this.Boolean_0)
			{
				GControl13.smethod_22(graphics_, GControl13.smethod_17(this.color_0), rectangle);
				GControl13.smethod_22(graphics_, GControl13.smethod_17(Color.FromArgb(20, Color.Black)), rectangle);
				GControl13.smethod_21(graphics_, GControl13.smethod_19(this), GControl13.smethod_20(this), GControl13.smethod_17(this.color_1), rectangle, GClass6.stringFormat_1);
			}
			else
			{
				graphicsPath_ = graphicsPath;
				GControl13.smethod_18(graphics_, GControl13.smethod_17(this.color_0), graphicsPath_);
				GControl13.smethod_18(graphics_, GControl13.smethod_17(Color.FromArgb(20, Color.Black)), graphicsPath_);
				GControl13.smethod_21(graphics_, GControl13.smethod_19(this), GControl13.smethod_20(this), GControl13.smethod_17(this.color_1), rectangle, GClass6.stringFormat_1);
			}
			break;
		}
		base.OnPaint(e);
		GControl13.smethod_23(graphics);
		GControl13.smethod_25(GControl13.smethod_24(e), InterpolationMode.HighQualityBicubic);
		GControl13.smethod_26(GControl13.smethod_24(e), image_, 0, 0);
		GControl13.smethod_27(image_);
	}

	// Token: 0x06000294 RID: 660 RVA: 0x00008194 File Offset: 0x00006394
	private void method_1()
	{
		GClass0 gclass = GClass6.smethod_3(this);
		this.color_0 = gclass.color_0;
	}

	// Token: 0x06000295 RID: 661 RVA: 0x0000232F File Offset: 0x0000052F
	static void smethod_0(Control control_0)
	{
		control_0.Invalidate();
	}

	// Token: 0x06000296 RID: 662 RVA: 0x00002FB9 File Offset: 0x000011B9
	static Control smethod_1(Control control_0)
	{
		return control_0.Parent;
	}

	// Token: 0x06000297 RID: 663 RVA: 0x00002FC1 File Offset: 0x000011C1
	static Control.ControlCollection smethod_2(Control control_0)
	{
		return control_0.Controls;
	}

	// Token: 0x06000298 RID: 664 RVA: 0x00002FC9 File Offset: 0x000011C9
	static IEnumerator smethod_3(ArrangedElementCollection arrangedElementCollection_0)
	{
		return arrangedElementCollection_0.GetEnumerator();
	}

	// Token: 0x06000299 RID: 665 RVA: 0x00002FD1 File Offset: 0x000011D1
	static object smethod_4(IEnumerator ienumerator_0)
	{
		return ienumerator_0.Current;
	}

	// Token: 0x0600029A RID: 666 RVA: 0x00003123 File Offset: 0x00001323
	static int smethod_5(Control control_0)
	{
		return control_0.Left;
	}

	// Token: 0x0600029B RID: 667 RVA: 0x0000312B File Offset: 0x0000132B
	static int smethod_6(Control control_0)
	{
		return control_0.Top;
	}

	// Token: 0x0600029C RID: 668 RVA: 0x0000237B File Offset: 0x0000057B
	static int smethod_7(Control control_0)
	{
		return control_0.Width;
	}

	// Token: 0x0600029D RID: 669 RVA: 0x00002383 File Offset: 0x00000583
	static int smethod_8(Control control_0)
	{
		return control_0.Height;
	}

	// Token: 0x0600029E RID: 670 RVA: 0x0000238B File Offset: 0x0000058B
	static Bitmap smethod_9(int int_2, int int_3)
	{
		return new Bitmap(int_2, int_3);
	}

	// Token: 0x0600029F RID: 671 RVA: 0x00002394 File Offset: 0x00000594
	static Graphics smethod_10(Image image_0)
	{
		return Graphics.FromImage(image_0);
	}

	// Token: 0x060002A0 RID: 672 RVA: 0x0000245C File Offset: 0x0000065C
	static GraphicsPath smethod_11()
	{
		return new GraphicsPath();
	}

	// Token: 0x060002A1 RID: 673 RVA: 0x0000239C File Offset: 0x0000059C
	static void smethod_12(Graphics graphics_0, SmoothingMode smoothingMode_0)
	{
		graphics_0.SmoothingMode = smoothingMode_0;
	}

	// Token: 0x060002A2 RID: 674 RVA: 0x00002463 File Offset: 0x00000663
	static void smethod_13(Graphics graphics_0, PixelOffsetMode pixelOffsetMode_0)
	{
		graphics_0.PixelOffsetMode = pixelOffsetMode_0;
	}

	// Token: 0x060002A3 RID: 675 RVA: 0x000023A5 File Offset: 0x000005A5
	static void smethod_14(Graphics graphics_0, TextRenderingHint textRenderingHint_0)
	{
		graphics_0.TextRenderingHint = textRenderingHint_0;
	}

	// Token: 0x060002A4 RID: 676 RVA: 0x000023AE File Offset: 0x000005AE
	static Color smethod_15(Control control_0)
	{
		return control_0.BackColor;
	}

	// Token: 0x060002A5 RID: 677 RVA: 0x000023B6 File Offset: 0x000005B6
	static void smethod_16(Graphics graphics_0, Color color_2)
	{
		graphics_0.Clear(color_2);
	}

	// Token: 0x060002A6 RID: 678 RVA: 0x000023BF File Offset: 0x000005BF
	static SolidBrush smethod_17(Color color_2)
	{
		return new SolidBrush(color_2);
	}

	// Token: 0x060002A7 RID: 679 RVA: 0x0000246C File Offset: 0x0000066C
	static void smethod_18(Graphics graphics_0, Brush brush_0, GraphicsPath graphicsPath_0)
	{
		graphics_0.FillPath(brush_0, graphicsPath_0);
	}

	// Token: 0x060002A8 RID: 680 RVA: 0x00002476 File Offset: 0x00000676
	static string smethod_19(Control control_0)
	{
		return control_0.Text;
	}

	// Token: 0x060002A9 RID: 681 RVA: 0x0000247E File Offset: 0x0000067E
	static Font smethod_20(Control control_0)
	{
		return control_0.Font;
	}

	// Token: 0x060002AA RID: 682 RVA: 0x00002486 File Offset: 0x00000686
	static void smethod_21(Graphics graphics_0, string string_0, Font font_0, Brush brush_0, RectangleF rectangleF_0, StringFormat stringFormat_0)
	{
		graphics_0.DrawString(string_0, font_0, brush_0, rectangleF_0, stringFormat_0);
	}

	// Token: 0x060002AB RID: 683 RVA: 0x000023C7 File Offset: 0x000005C7
	static void smethod_22(Graphics graphics_0, Brush brush_0, Rectangle rectangle_0)
	{
		graphics_0.FillRectangle(brush_0, rectangle_0);
	}

	// Token: 0x060002AC RID: 684 RVA: 0x00002495 File Offset: 0x00000695
	static void smethod_23(Graphics graphics_0)
	{
		graphics_0.Dispose();
	}

	// Token: 0x060002AD RID: 685 RVA: 0x0000249D File Offset: 0x0000069D
	static Graphics smethod_24(PaintEventArgs paintEventArgs_0)
	{
		return paintEventArgs_0.Graphics;
	}

	// Token: 0x060002AE RID: 686 RVA: 0x000024A5 File Offset: 0x000006A5
	static void smethod_25(Graphics graphics_0, InterpolationMode interpolationMode_0)
	{
		graphics_0.InterpolationMode = interpolationMode_0;
	}

	// Token: 0x060002AF RID: 687 RVA: 0x000024AE File Offset: 0x000006AE
	static void smethod_26(Graphics graphics_0, Image image_0, int int_2, int int_3)
	{
		graphics_0.DrawImageUnscaled(image_0, int_2, int_3);
	}

	// Token: 0x060002B0 RID: 688 RVA: 0x000024B9 File Offset: 0x000006B9
	static void smethod_27(Image image_0)
	{
		image_0.Dispose();
	}

	// Token: 0x04000078 RID: 120
	private int int_0;

	// Token: 0x04000079 RID: 121
	private int int_1;

	// Token: 0x0400007A RID: 122
	private GEnum5 genum5_0;

	// Token: 0x0400007B RID: 123
	private bool bool_0;

	// Token: 0x0400007C RID: 124
	private Color color_0 = GClass6.color_0;

	// Token: 0x0400007D RID: 125
	private Color color_1 = Color.FromArgb(243, 243, 243);
}
