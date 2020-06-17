using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

// Token: 0x0200001C RID: 28
[DefaultEvent("TextChanged")]
public class GControl15 : Control
{
	// Token: 0x1700004B RID: 75
	// (get) Token: 0x060002C8 RID: 712 RVA: 0x00003187 File Offset: 0x00001387
	// (set) Token: 0x060002C9 RID: 713 RVA: 0x0000318F File Offset: 0x0000138F
	[Category("Options")]
	public HorizontalAlignment HorizontalAlignment_0
	{
		get
		{
			return this.horizontalAlignment_0;
		}
		set
		{
			this.horizontalAlignment_0 = value;
			if (this.textBox_0 != null)
			{
				GControl15.smethod_0(this.textBox_0, value);
			}
		}
	}

	// Token: 0x1700004C RID: 76
	// (get) Token: 0x060002CA RID: 714 RVA: 0x000031AC File Offset: 0x000013AC
	// (set) Token: 0x060002CB RID: 715 RVA: 0x000031B4 File Offset: 0x000013B4
	[Category("Options")]
	public int Int32_0
	{
		get
		{
			return this.int_2;
		}
		set
		{
			this.int_2 = value;
			if (this.textBox_0 != null)
			{
				GControl15.smethod_1(this.textBox_0, value);
			}
		}
	}

	// Token: 0x1700004D RID: 77
	// (get) Token: 0x060002CC RID: 716 RVA: 0x000031D1 File Offset: 0x000013D1
	// (set) Token: 0x060002CD RID: 717 RVA: 0x000031D9 File Offset: 0x000013D9
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
			if (this.textBox_0 != null)
			{
				GControl15.smethod_2(this.textBox_0, value);
			}
		}
	}

	// Token: 0x1700004E RID: 78
	// (get) Token: 0x060002CE RID: 718 RVA: 0x000031F6 File Offset: 0x000013F6
	// (set) Token: 0x060002CF RID: 719 RVA: 0x000031FE File Offset: 0x000013FE
	[Category("Options")]
	public bool Boolean_1
	{
		get
		{
			return this.bool_1;
		}
		set
		{
			this.bool_1 = value;
			if (this.textBox_0 != null)
			{
				GControl15.smethod_3(this.textBox_0, value);
			}
		}
	}

	// Token: 0x1700004F RID: 79
	// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000321B File Offset: 0x0000141B
	// (set) Token: 0x060002D1 RID: 721 RVA: 0x000086CC File Offset: 0x000068CC
	[Category("Options")]
	public bool Boolean_2
	{
		get
		{
			return this.bool_2;
		}
		set
		{
			this.bool_2 = value;
			if (this.textBox_0 != null)
			{
				GControl15.smethod_4(this.textBox_0, value);
				if (value)
				{
					GControl15.smethod_6(this.textBox_0, GControl15.smethod_5(this) - 11);
					return;
				}
				GControl15.smethod_8(this, GControl15.smethod_7(this.textBox_0) + 11);
			}
		}
	}

	// Token: 0x17000050 RID: 80
	// (get) Token: 0x060002D2 RID: 722 RVA: 0x00003223 File Offset: 0x00001423
	// (set) Token: 0x060002D3 RID: 723 RVA: 0x0000322B File Offset: 0x0000142B
	[Category("Options")]
	public bool Boolean_3
	{
		get
		{
			return this.bool_3;
		}
		set
		{
			this.bool_3 = value;
		}
	}

	// Token: 0x17000051 RID: 81
	// (get) Token: 0x060002D4 RID: 724 RVA: 0x00003234 File Offset: 0x00001434
	// (set) Token: 0x060002D5 RID: 725 RVA: 0x0000323C File Offset: 0x0000143C
	[Category("Options")]
	public virtual string Text
	{
		get
		{
			return this.method_1();
		}
		set
		{
			this.method_2(value);
			if (this.textBox_0 != null)
			{
				GControl15.smethod_9(this.textBox_0, value);
			}
		}
	}

	// Token: 0x17000052 RID: 82
	// (get) Token: 0x060002D6 RID: 726 RVA: 0x00003259 File Offset: 0x00001459
	// (set) Token: 0x060002D7 RID: 727 RVA: 0x00008720 File Offset: 0x00006920
	[Category("Options")]
	public virtual Font Font
	{
		get
		{
			return this.method_3();
		}
		set
		{
			this.method_4(value);
			if (this.textBox_0 != null)
			{
				GControl15.smethod_10(this.textBox_0, value);
				this.textBox_0.Location = new Point(3, 5);
				this.textBox_0.Width = base.Width - 6;
				if (!this.bool_2)
				{
					base.Height = this.textBox_0.Height + 11;
				}
			}
		}
	}

	// Token: 0x060002D8 RID: 728 RVA: 0x00003261 File Offset: 0x00001461
	protected virtual void OnCreateControl()
	{
		base.OnCreateControl();
		if (!GControl15.smethod_12(GControl15.smethod_11(this), this.textBox_0))
		{
			GControl15.smethod_13(GControl15.smethod_11(this), this.textBox_0);
		}
	}

	// Token: 0x060002D9 RID: 729 RVA: 0x0000328D File Offset: 0x0000148D
	private void textBox_0_TextChanged(object sender, EventArgs e)
	{
		GControl15.smethod_9(this, GControl15.smethod_14(this.textBox_0));
	}

	// Token: 0x060002DA RID: 730 RVA: 0x0000878C File Offset: 0x0000698C
	private void textBox_0_KeyDown(object sender, KeyEventArgs e)
	{
		if (GControl15.smethod_15(e) && GControl15.smethod_16(e) == Keys.A)
		{
			GControl15.smethod_17(this.textBox_0);
			GControl15.smethod_18(e, true);
		}
		if (GControl15.smethod_15(e))
		{
			if (GControl15.smethod_16(e) == Keys.C)
			{
				GControl15.smethod_19(this.textBox_0);
				GControl15.smethod_18(e, true);
			}
		}
	}

	// Token: 0x060002DB RID: 731 RVA: 0x000087E4 File Offset: 0x000069E4
	protected virtual void OnResize(EventArgs e)
	{
		this.textBox_0.Location = new Point(5, 5);
		this.textBox_0.Width = base.Width - 10;
		if (this.bool_2)
		{
			this.textBox_0.Height = base.Height - 11;
		}
		else
		{
			base.Height = this.textBox_0.Height + 11;
		}
		base.OnResize(e);
	}

	// Token: 0x17000053 RID: 83
	// (get) Token: 0x060002DC RID: 732 RVA: 0x000032A0 File Offset: 0x000014A0
	// (set) Token: 0x060002DD RID: 733 RVA: 0x000032A8 File Offset: 0x000014A8
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

	// Token: 0x17000054 RID: 84
	// (get) Token: 0x060002DE RID: 734 RVA: 0x000032A0 File Offset: 0x000014A0
	// (set) Token: 0x060002DF RID: 735 RVA: 0x000032A8 File Offset: 0x000014A8
	public virtual Color ForeColor
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

	// Token: 0x060002E0 RID: 736 RVA: 0x000032B1 File Offset: 0x000014B1
	protected virtual void OnMouseDown(MouseEventArgs e)
	{
		base.OnMouseDown(e);
		this.genum5_0 = GEnum5.Down;
		GControl15.smethod_20(this);
	}

	// Token: 0x060002E1 RID: 737 RVA: 0x000032C7 File Offset: 0x000014C7
	protected virtual void OnMouseUp(MouseEventArgs e)
	{
		base.OnMouseUp(e);
		this.genum5_0 = GEnum5.Over;
		GControl15.smethod_21(this.textBox_0);
		GControl15.smethod_20(this);
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x000032E9 File Offset: 0x000014E9
	protected virtual void OnMouseEnter(EventArgs e)
	{
		base.OnMouseEnter(e);
		this.genum5_0 = GEnum5.Over;
		if (this.Boolean_3)
		{
			GControl15.smethod_21(this.textBox_0);
		}
		GControl15.smethod_20(this);
	}

	// Token: 0x060002E3 RID: 739 RVA: 0x00003313 File Offset: 0x00001513
	protected virtual void OnMouseLeave(EventArgs e)
	{
		base.OnMouseLeave(e);
		this.genum5_0 = GEnum5.None;
		GControl15.smethod_20(this);
	}

	// Token: 0x060002E4 RID: 740 RVA: 0x00008850 File Offset: 0x00006A50
	public GControl15()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		GControl15.smethod_22(this, Color.Transparent);
		this.textBox_0 = GControl15.smethod_23();
		GControl15.smethod_10(this.textBox_0, GControl15.smethod_24("Segoe UI", 10f));
		GControl15.smethod_9(this.textBox_0, GControl15.smethod_14(this));
		GControl15.smethod_22(this.textBox_0, this.color_0);
		GControl15.smethod_25(this.textBox_0, this.color_1);
		GControl15.smethod_1(this.textBox_0, this.int_2);
		GControl15.smethod_4(this.textBox_0, this.bool_2);
		GControl15.smethod_2(this.textBox_0, this.bool_0);
		GControl15.smethod_3(this.textBox_0, this.bool_1);
		GControl15.smethod_26(this.textBox_0, BorderStyle.None);
		this.textBox_0.Location = new Point(5, 5);
		this.textBox_0.Width = base.Width - 10;
		this.textBox_0.Cursor = Cursors.IBeam;
		if (!this.bool_2)
		{
			base.Height = this.textBox_0.Height + 11;
		}
		else
		{
			this.textBox_0.Height = base.Height - 11;
		}
		this.textBox_0.TextChanged += this.textBox_0_TextChanged;
		this.textBox_0.KeyDown += this.textBox_0_KeyDown;
	}

	// Token: 0x060002E5 RID: 741 RVA: 0x00008A00 File Offset: 0x00006C00
	protected virtual void OnPaint(PaintEventArgs e)
	{
		this.method_0();
		Bitmap image_ = GControl15.smethod_28(GControl15.smethod_27(this), GControl15.smethod_5(this));
		Graphics graphics_ = GControl15.smethod_29(image_);
		this.int_0 = GControl15.smethod_27(this) - 1;
		this.int_1 = GControl15.smethod_5(this) - 1;
		Rectangle rectangle_ = new Rectangle(0, 0, this.int_0, this.int_1);
		GControl15.smethod_30(graphics_, SmoothingMode.HighQuality);
		GControl15.smethod_31(graphics_, PixelOffsetMode.HighQuality);
		GControl15.smethod_32(graphics_, TextRenderingHint.ClearTypeGridFit);
		GControl15.smethod_34(graphics_, GControl15.smethod_33(this));
		GControl15.smethod_22(this.textBox_0, this.color_0);
		GControl15.smethod_25(this.textBox_0, this.color_1);
		GControl15.smethod_36(graphics_, GControl15.smethod_35(this.color_0), rectangle_);
		base.OnPaint(e);
		GControl15.smethod_37(graphics_);
		GControl15.smethod_39(GControl15.smethod_38(e), InterpolationMode.HighQualityBicubic);
		GControl15.smethod_40(GControl15.smethod_38(e), image_, 0, 0);
		GControl15.smethod_41(image_);
	}

	// Token: 0x060002E6 RID: 742 RVA: 0x00008AE0 File Offset: 0x00006CE0
	private void method_0()
	{
		GClass0 gclass = GClass6.smethod_3(this);
		this.color_2 = gclass.color_0;
	}

	// Token: 0x060002E7 RID: 743 RVA: 0x00003329 File Offset: 0x00001529
	static void smethod_0(TextBox textBox_1, HorizontalAlignment horizontalAlignment_1)
	{
		textBox_1.TextAlign = horizontalAlignment_1;
	}

	// Token: 0x060002E8 RID: 744 RVA: 0x00003332 File Offset: 0x00001532
	static void smethod_1(TextBoxBase textBoxBase_0, int int_3)
	{
		textBoxBase_0.MaxLength = int_3;
	}

	// Token: 0x060002E9 RID: 745 RVA: 0x0000333B File Offset: 0x0000153B
	static void smethod_2(TextBoxBase textBoxBase_0, bool bool_4)
	{
		textBoxBase_0.ReadOnly = bool_4;
	}

	// Token: 0x060002EA RID: 746 RVA: 0x00003344 File Offset: 0x00001544
	static void smethod_3(TextBox textBox_1, bool bool_4)
	{
		textBox_1.UseSystemPasswordChar = bool_4;
	}

	// Token: 0x060002EB RID: 747 RVA: 0x0000334D File Offset: 0x0000154D
	static void smethod_4(TextBoxBase textBoxBase_0, bool bool_4)
	{
		textBoxBase_0.Multiline = bool_4;
	}

	// Token: 0x060002EC RID: 748 RVA: 0x00002383 File Offset: 0x00000583
	static int smethod_5(Control control_0)
	{
		return control_0.Height;
	}

	// Token: 0x060002ED RID: 749 RVA: 0x00003356 File Offset: 0x00001556
	static void smethod_6(Control control_0, int int_3)
	{
		control_0.Height = int_3;
	}

	// Token: 0x060002EE RID: 750 RVA: 0x0000335F File Offset: 0x0000155F
	static int smethod_7(Control control_0)
	{
		return control_0.Height;
	}

	// Token: 0x060002EF RID: 751 RVA: 0x00002337 File Offset: 0x00000537
	static void smethod_8(Control control_0, int int_3)
	{
		control_0.Height = int_3;
	}

	// Token: 0x060002F0 RID: 752 RVA: 0x0000230D File Offset: 0x0000050D
	string method_1()
	{
		return base.Text;
	}

	// Token: 0x060002F1 RID: 753 RVA: 0x00002315 File Offset: 0x00000515
	void method_2(string string_0)
	{
		base.Text = string_0;
	}

	// Token: 0x060002F2 RID: 754 RVA: 0x00002340 File Offset: 0x00000540
	static void smethod_9(Control control_0, string string_0)
	{
		control_0.Text = string_0;
	}

	// Token: 0x060002F3 RID: 755 RVA: 0x00003367 File Offset: 0x00001567
	Font method_3()
	{
		return base.Font;
	}

	// Token: 0x060002F4 RID: 756 RVA: 0x0000336F File Offset: 0x0000156F
	void method_4(Font font_0)
	{
		base.Font = font_0;
	}

	// Token: 0x060002F5 RID: 757 RVA: 0x000025DA File Offset: 0x000007DA
	static void smethod_10(Control control_0, Font font_0)
	{
		control_0.Font = font_0;
	}

	// Token: 0x060002F6 RID: 758 RVA: 0x00002B68 File Offset: 0x00000D68
	static Control.ControlCollection smethod_11(Control control_0)
	{
		return control_0.Controls;
	}

	// Token: 0x060002F7 RID: 759 RVA: 0x00002B70 File Offset: 0x00000D70
	static bool smethod_12(Control.ControlCollection controlCollection_0, Control control_0)
	{
		return controlCollection_0.Contains(control_0);
	}

	// Token: 0x060002F8 RID: 760 RVA: 0x00002B79 File Offset: 0x00000D79
	static void smethod_13(Control.ControlCollection controlCollection_0, Control control_0)
	{
		controlCollection_0.Add(control_0);
	}

	// Token: 0x060002F9 RID: 761 RVA: 0x00002476 File Offset: 0x00000676
	static string smethod_14(Control control_0)
	{
		return control_0.Text;
	}

	// Token: 0x060002FA RID: 762 RVA: 0x00003378 File Offset: 0x00001578
	static bool smethod_15(KeyEventArgs keyEventArgs_0)
	{
		return keyEventArgs_0.Control;
	}

	// Token: 0x060002FB RID: 763 RVA: 0x00002DFD File Offset: 0x00000FFD
	static Keys smethod_16(KeyEventArgs keyEventArgs_0)
	{
		return keyEventArgs_0.KeyCode;
	}

	// Token: 0x060002FC RID: 764 RVA: 0x00003380 File Offset: 0x00001580
	static void smethod_17(TextBoxBase textBoxBase_0)
	{
		textBoxBase_0.SelectAll();
	}

	// Token: 0x060002FD RID: 765 RVA: 0x00003388 File Offset: 0x00001588
	static void smethod_18(KeyEventArgs keyEventArgs_0, bool bool_4)
	{
		keyEventArgs_0.SuppressKeyPress = bool_4;
	}

	// Token: 0x060002FE RID: 766 RVA: 0x00003391 File Offset: 0x00001591
	static void smethod_19(TextBoxBase textBoxBase_0)
	{
		textBoxBase_0.Copy();
	}

	// Token: 0x060002FF RID: 767 RVA: 0x0000232F File Offset: 0x0000052F
	static void smethod_20(Control control_0)
	{
		control_0.Invalidate();
	}

	// Token: 0x06000300 RID: 768 RVA: 0x00003399 File Offset: 0x00001599
	static bool smethod_21(Control control_0)
	{
		return control_0.Focus();
	}

	// Token: 0x06000301 RID: 769 RVA: 0x00002372 File Offset: 0x00000572
	static void smethod_22(Control control_0, Color color_3)
	{
		control_0.BackColor = color_3;
	}

	// Token: 0x06000302 RID: 770 RVA: 0x000033A1 File Offset: 0x000015A1
	static TextBox smethod_23()
	{
		return new TextBox();
	}

	// Token: 0x06000303 RID: 771 RVA: 0x000025D1 File Offset: 0x000007D1
	static Font smethod_24(string string_0, float float_0)
	{
		return new Font(string_0, float_0);
	}

	// Token: 0x06000304 RID: 772 RVA: 0x000028AF File Offset: 0x00000AAF
	static void smethod_25(Control control_0, Color color_3)
	{
		control_0.ForeColor = color_3;
	}

	// Token: 0x06000305 RID: 773 RVA: 0x000033A8 File Offset: 0x000015A8
	static void smethod_26(TextBoxBase textBoxBase_0, BorderStyle borderStyle_0)
	{
		textBoxBase_0.BorderStyle = borderStyle_0;
	}

	// Token: 0x06000306 RID: 774 RVA: 0x0000237B File Offset: 0x0000057B
	static int smethod_27(Control control_0)
	{
		return control_0.Width;
	}

	// Token: 0x06000307 RID: 775 RVA: 0x0000238B File Offset: 0x0000058B
	static Bitmap smethod_28(int int_3, int int_4)
	{
		return new Bitmap(int_3, int_4);
	}

	// Token: 0x06000308 RID: 776 RVA: 0x00002394 File Offset: 0x00000594
	static Graphics smethod_29(Image image_0)
	{
		return Graphics.FromImage(image_0);
	}

	// Token: 0x06000309 RID: 777 RVA: 0x0000239C File Offset: 0x0000059C
	static void smethod_30(Graphics graphics_0, SmoothingMode smoothingMode_0)
	{
		graphics_0.SmoothingMode = smoothingMode_0;
	}

	// Token: 0x0600030A RID: 778 RVA: 0x00002463 File Offset: 0x00000663
	static void smethod_31(Graphics graphics_0, PixelOffsetMode pixelOffsetMode_0)
	{
		graphics_0.PixelOffsetMode = pixelOffsetMode_0;
	}

	// Token: 0x0600030B RID: 779 RVA: 0x000023A5 File Offset: 0x000005A5
	static void smethod_32(Graphics graphics_0, TextRenderingHint textRenderingHint_0)
	{
		graphics_0.TextRenderingHint = textRenderingHint_0;
	}

	// Token: 0x0600030C RID: 780 RVA: 0x000023AE File Offset: 0x000005AE
	static Color smethod_33(Control control_0)
	{
		return control_0.BackColor;
	}

	// Token: 0x0600030D RID: 781 RVA: 0x000023B6 File Offset: 0x000005B6
	static void smethod_34(Graphics graphics_0, Color color_3)
	{
		graphics_0.Clear(color_3);
	}

	// Token: 0x0600030E RID: 782 RVA: 0x000023BF File Offset: 0x000005BF
	static SolidBrush smethod_35(Color color_3)
	{
		return new SolidBrush(color_3);
	}

	// Token: 0x0600030F RID: 783 RVA: 0x000023C7 File Offset: 0x000005C7
	static void smethod_36(Graphics graphics_0, Brush brush_0, Rectangle rectangle_0)
	{
		graphics_0.FillRectangle(brush_0, rectangle_0);
	}

	// Token: 0x06000310 RID: 784 RVA: 0x00002495 File Offset: 0x00000695
	static void smethod_37(Graphics graphics_0)
	{
		graphics_0.Dispose();
	}

	// Token: 0x06000311 RID: 785 RVA: 0x0000249D File Offset: 0x0000069D
	static Graphics smethod_38(PaintEventArgs paintEventArgs_0)
	{
		return paintEventArgs_0.Graphics;
	}

	// Token: 0x06000312 RID: 786 RVA: 0x000024A5 File Offset: 0x000006A5
	static void smethod_39(Graphics graphics_0, InterpolationMode interpolationMode_0)
	{
		graphics_0.InterpolationMode = interpolationMode_0;
	}

	// Token: 0x06000313 RID: 787 RVA: 0x000024AE File Offset: 0x000006AE
	static void smethod_40(Graphics graphics_0, Image image_0, int int_3, int int_4)
	{
		graphics_0.DrawImageUnscaled(image_0, int_3, int_4);
	}

	// Token: 0x06000314 RID: 788 RVA: 0x000024B9 File Offset: 0x000006B9
	static void smethod_41(Image image_0)
	{
		image_0.Dispose();
	}

	// Token: 0x04000083 RID: 131
	private int int_0;

	// Token: 0x04000084 RID: 132
	private int int_1;

	// Token: 0x04000085 RID: 133
	private GEnum5 genum5_0;

	// Token: 0x04000086 RID: 134
	private TextBox textBox_0;

	// Token: 0x04000087 RID: 135
	private HorizontalAlignment horizontalAlignment_0;

	// Token: 0x04000088 RID: 136
	private int int_2 = 32767;

	// Token: 0x04000089 RID: 137
	private bool bool_0;

	// Token: 0x0400008A RID: 138
	private bool bool_1;

	// Token: 0x0400008B RID: 139
	private bool bool_2;

	// Token: 0x0400008C RID: 140
	private bool bool_3;

	// Token: 0x0400008D RID: 141
	private Color color_0 = Color.FromArgb(45, 47, 49);

	// Token: 0x0400008E RID: 142
	private Color color_1 = Color.FromArgb(192, 192, 192);

	// Token: 0x0400008F RID: 143
	private Color color_2 = GClass6.color_0;
}
