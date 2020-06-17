using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

// Token: 0x02000016 RID: 22
[DefaultEvent("CheckedChanged")]
public class GControl11 : Control
{
	// Token: 0x1700003F RID: 63
	// (get) Token: 0x06000234 RID: 564 RVA: 0x00002EE0 File Offset: 0x000010E0
	// (set) Token: 0x06000235 RID: 565 RVA: 0x00002EE8 File Offset: 0x000010E8
	public bool Boolean_0
	{
		get
		{
			return this.bool_0;
		}
		set
		{
			this.bool_0 = value;
			this.method_0();
			if (this.gdelegate1_0 != null)
			{
				this.gdelegate1_0(this);
			}
			GControl11.smethod_0(this);
		}
	}

	// Token: 0x14000002 RID: 2
	// (add) Token: 0x06000236 RID: 566 RVA: 0x00007694 File Offset: 0x00005894
	// (remove) Token: 0x06000237 RID: 567 RVA: 0x000076CC File Offset: 0x000058CC
	public event GControl11.GDelegate1 Event_0
	{
		[CompilerGenerated]
		add
		{
			GControl11.GDelegate1 gdelegate = this.gdelegate1_0;
			GControl11.GDelegate1 gdelegate2;
			do
			{
				gdelegate2 = gdelegate;
				GControl11.GDelegate1 value2 = (GControl11.GDelegate1)GControl11.smethod_1(gdelegate2, value);
				gdelegate = Interlocked.CompareExchange<GControl11.GDelegate1>(ref this.gdelegate1_0, value2, gdelegate2);
			}
			while (gdelegate != gdelegate2);
		}
		[CompilerGenerated]
		remove
		{
			GControl11.GDelegate1 gdelegate = this.gdelegate1_0;
			GControl11.GDelegate1 gdelegate2;
			do
			{
				gdelegate2 = gdelegate;
				GControl11.GDelegate1 value2 = (GControl11.GDelegate1)GControl11.smethod_2(gdelegate2, value);
				gdelegate = Interlocked.CompareExchange<GControl11.GDelegate1>(ref this.gdelegate1_0, value2, gdelegate2);
			}
			while (gdelegate != gdelegate2);
		}
	}

	// Token: 0x06000238 RID: 568 RVA: 0x00002F11 File Offset: 0x00001111
	protected virtual void OnClick(EventArgs e)
	{
		if (!this.bool_0)
		{
			this.Boolean_0 = true;
		}
		base.OnClick(e);
	}

	// Token: 0x06000239 RID: 569 RVA: 0x00007708 File Offset: 0x00005908
	private void method_0()
	{
		if (GControl11.smethod_3(this) && this.bool_0)
		{
			IEnumerator enumerator = GControl11.smethod_6(GControl11.smethod_5(GControl11.smethod_4(this)));
			try
			{
				while (GControl11.smethod_8(enumerator))
				{
					Control control = (Control)GControl11.smethod_7(enumerator);
					if (control != this)
					{
						if (control is GControl11)
						{
							((GControl11)control).Boolean_0 = false;
							GControl11.smethod_0(this);
						}
					}
				}
			}
			finally
			{
				IDisposable disposable = enumerator as IDisposable;
				if (disposable != null)
				{
					GControl11.smethod_9(disposable);
				}
			}
			return;
		}
	}

	// Token: 0x0600023A RID: 570 RVA: 0x00002F29 File Offset: 0x00001129
	protected virtual void OnCreateControl()
	{
		base.OnCreateControl();
		this.method_0();
	}

	// Token: 0x17000040 RID: 64
	// (get) Token: 0x0600023B RID: 571 RVA: 0x00002F37 File Offset: 0x00001137
	// (set) Token: 0x0600023C RID: 572 RVA: 0x00002F3F File Offset: 0x0000113F
	[Category("Options")]
	public GControl11.GEnum2 GEnum2_0
	{
		get
		{
			return this.genum2_0;
		}
		set
		{
			this.genum2_0 = value;
		}
	}

	// Token: 0x0600023D RID: 573 RVA: 0x00002F48 File Offset: 0x00001148
	protected virtual void OnResize(EventArgs e)
	{
		base.OnResize(e);
		GControl11.smethod_10(this, 22);
	}

	// Token: 0x0600023E RID: 574 RVA: 0x00002F59 File Offset: 0x00001159
	protected virtual void OnMouseDown(MouseEventArgs e)
	{
		base.OnMouseDown(e);
		this.genum5_0 = GEnum5.Down;
		GControl11.smethod_0(this);
	}

	// Token: 0x0600023F RID: 575 RVA: 0x00002F6F File Offset: 0x0000116F
	protected virtual void OnMouseUp(MouseEventArgs e)
	{
		base.OnMouseUp(e);
		this.genum5_0 = GEnum5.Over;
		GControl11.smethod_0(this);
	}

	// Token: 0x06000240 RID: 576 RVA: 0x00002F85 File Offset: 0x00001185
	protected virtual void OnMouseEnter(EventArgs e)
	{
		base.OnMouseEnter(e);
		this.genum5_0 = GEnum5.Over;
		GControl11.smethod_0(this);
	}

	// Token: 0x06000241 RID: 577 RVA: 0x00002F9B File Offset: 0x0000119B
	protected virtual void OnMouseLeave(EventArgs e)
	{
		base.OnMouseLeave(e);
		this.genum5_0 = GEnum5.None;
		GControl11.smethod_0(this);
	}

	// Token: 0x06000242 RID: 578 RVA: 0x00007794 File Offset: 0x00005994
	public GControl11()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		GControl11.smethod_12(this, GControl11.smethod_11());
		base.Size = new Size(100, 22);
		this.BackColor = Color.FromArgb(60, 70, 73);
		this.Font = new Font("Segoe UI", 10f);
	}

	// Token: 0x06000243 RID: 579 RVA: 0x00007830 File Offset: 0x00005A30
	protected virtual void OnPaint(PaintEventArgs e)
	{
		this.method_1();
		Bitmap bitmap = GControl11.smethod_15(GControl11.smethod_13(this), GControl11.smethod_14(this));
		Graphics graphics = GControl11.smethod_16(bitmap);
		this.int_0 = GControl11.smethod_13(this) - 1;
		this.int_1 = GControl11.smethod_14(this) - 1;
		Rectangle rectangle_ = new Rectangle(0, 2, GControl11.smethod_14(this) - 5, GControl11.smethod_14(this) - 5);
		Rectangle rectangle_2 = new Rectangle(4, 6, this.int_1 - 12, this.int_1 - 12);
		Graphics graphics2 = graphics;
		GControl11.smethod_17(graphics2, SmoothingMode.HighQuality);
		GControl11.smethod_18(graphics2, TextRenderingHint.ClearTypeGridFit);
		GControl11.smethod_20(graphics2, GControl11.smethod_19(this));
		GControl11.GEnum2 genum = this.genum2_0;
		if (genum == GControl11.GEnum2.Style1)
		{
			GControl11.smethod_22(graphics2, GControl11.smethod_21(this.color_0), rectangle_);
			GEnum5 genum2 = this.genum5_0;
			if (genum2 == GEnum5.Over)
			{
				GControl11.smethod_24(graphics2, GControl11.smethod_23(this.color_1), rectangle_);
			}
			else if (genum2 == GEnum5.Down)
			{
				GControl11.smethod_24(graphics2, GControl11.smethod_23(this.color_1), rectangle_);
			}
			if (this.Boolean_0)
			{
				GControl11.smethod_22(graphics2, GControl11.smethod_21(this.color_1), rectangle_2);
			}
		}
		else if (genum == GControl11.GEnum2.Style2)
		{
			GControl11.smethod_22(graphics2, GControl11.smethod_21(this.color_0), rectangle_);
			GEnum5 genum2 = this.genum5_0;
			if (genum2 == GEnum5.Over)
			{
				GControl11.smethod_24(graphics2, GControl11.smethod_23(this.color_1), rectangle_);
				GControl11.smethod_22(graphics2, GControl11.smethod_21(Color.FromArgb(118, 213, 170)), rectangle_);
			}
			else if (genum2 == GEnum5.Down)
			{
				GControl11.smethod_24(graphics2, GControl11.smethod_23(this.color_1), rectangle_);
				GControl11.smethod_22(graphics2, GControl11.smethod_21(Color.FromArgb(118, 213, 170)), rectangle_);
			}
			if (this.Boolean_0)
			{
				GControl11.smethod_22(graphics2, GControl11.smethod_21(this.color_1), rectangle_2);
			}
		}
		graphics2.DrawString(GControl11.smethod_25(this), GControl11.smethod_26(this), GControl11.smethod_21(this.color_2), new Rectangle(20, 2, this.int_0, this.int_1), GClass6.stringFormat_0);
		base.OnPaint(e);
		graphics.Dispose();
		e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
		bitmap.Dispose();
	}

	// Token: 0x06000244 RID: 580 RVA: 0x00007A64 File Offset: 0x00005C64
	private void method_1()
	{
		GClass0 gclass = GClass6.smethod_3(this);
		this.color_1 = gclass.color_0;
	}

	// Token: 0x06000245 RID: 581 RVA: 0x0000232F File Offset: 0x0000052F
	static void smethod_0(Control control_0)
	{
		control_0.Invalidate();
	}

	// Token: 0x06000246 RID: 582 RVA: 0x000025AF File Offset: 0x000007AF
	static Delegate smethod_1(Delegate delegate_0, Delegate delegate_1)
	{
		return Delegate.Combine(delegate_0, delegate_1);
	}

	// Token: 0x06000247 RID: 583 RVA: 0x000025B8 File Offset: 0x000007B8
	static Delegate smethod_2(Delegate delegate_0, Delegate delegate_1)
	{
		return Delegate.Remove(delegate_0, delegate_1);
	}

	// Token: 0x06000248 RID: 584 RVA: 0x00002FB1 File Offset: 0x000011B1
	static bool smethod_3(Control control_0)
	{
		return control_0.IsHandleCreated;
	}

	// Token: 0x06000249 RID: 585 RVA: 0x00002FB9 File Offset: 0x000011B9
	static Control smethod_4(Control control_0)
	{
		return control_0.Parent;
	}

	// Token: 0x0600024A RID: 586 RVA: 0x00002FC1 File Offset: 0x000011C1
	static Control.ControlCollection smethod_5(Control control_0)
	{
		return control_0.Controls;
	}

	// Token: 0x0600024B RID: 587 RVA: 0x00002FC9 File Offset: 0x000011C9
	static IEnumerator smethod_6(ArrangedElementCollection arrangedElementCollection_0)
	{
		return arrangedElementCollection_0.GetEnumerator();
	}

	// Token: 0x0600024C RID: 588 RVA: 0x00002FD1 File Offset: 0x000011D1
	static object smethod_7(IEnumerator ienumerator_0)
	{
		return ienumerator_0.Current;
	}

	// Token: 0x0600024D RID: 589 RVA: 0x00002FD9 File Offset: 0x000011D9
	static bool smethod_8(IEnumerator ienumerator_0)
	{
		return ienumerator_0.MoveNext();
	}

	// Token: 0x0600024E RID: 590 RVA: 0x00002FE1 File Offset: 0x000011E1
	static void smethod_9(IDisposable idisposable_0)
	{
		idisposable_0.Dispose();
	}

	// Token: 0x0600024F RID: 591 RVA: 0x00002337 File Offset: 0x00000537
	static void smethod_10(Control control_0, int int_2)
	{
		control_0.Height = int_2;
	}

	// Token: 0x06000250 RID: 592 RVA: 0x000025C1 File Offset: 0x000007C1
	static Cursor smethod_11()
	{
		return Cursors.Hand;
	}

	// Token: 0x06000251 RID: 593 RVA: 0x000025C8 File Offset: 0x000007C8
	static void smethod_12(Control control_0, Cursor cursor_0)
	{
		control_0.Cursor = cursor_0;
	}

	// Token: 0x06000252 RID: 594 RVA: 0x0000237B File Offset: 0x0000057B
	static int smethod_13(Control control_0)
	{
		return control_0.Width;
	}

	// Token: 0x06000253 RID: 595 RVA: 0x00002383 File Offset: 0x00000583
	static int smethod_14(Control control_0)
	{
		return control_0.Height;
	}

	// Token: 0x06000254 RID: 596 RVA: 0x0000238B File Offset: 0x0000058B
	static Bitmap smethod_15(int int_2, int int_3)
	{
		return new Bitmap(int_2, int_3);
	}

	// Token: 0x06000255 RID: 597 RVA: 0x00002394 File Offset: 0x00000594
	static Graphics smethod_16(Image image_0)
	{
		return Graphics.FromImage(image_0);
	}

	// Token: 0x06000256 RID: 598 RVA: 0x0000239C File Offset: 0x0000059C
	static void smethod_17(Graphics graphics_0, SmoothingMode smoothingMode_0)
	{
		graphics_0.SmoothingMode = smoothingMode_0;
	}

	// Token: 0x06000257 RID: 599 RVA: 0x000023A5 File Offset: 0x000005A5
	static void smethod_18(Graphics graphics_0, TextRenderingHint textRenderingHint_0)
	{
		graphics_0.TextRenderingHint = textRenderingHint_0;
	}

	// Token: 0x06000258 RID: 600 RVA: 0x000023AE File Offset: 0x000005AE
	static Color smethod_19(Control control_0)
	{
		return control_0.BackColor;
	}

	// Token: 0x06000259 RID: 601 RVA: 0x000023B6 File Offset: 0x000005B6
	static void smethod_20(Graphics graphics_0, Color color_3)
	{
		graphics_0.Clear(color_3);
	}

	// Token: 0x0600025A RID: 602 RVA: 0x000023BF File Offset: 0x000005BF
	static SolidBrush smethod_21(Color color_3)
	{
		return new SolidBrush(color_3);
	}

	// Token: 0x0600025B RID: 603 RVA: 0x00002FE9 File Offset: 0x000011E9
	static void smethod_22(Graphics graphics_0, Brush brush_0, Rectangle rectangle_0)
	{
		graphics_0.FillEllipse(brush_0, rectangle_0);
	}

	// Token: 0x0600025C RID: 604 RVA: 0x000025E3 File Offset: 0x000007E3
	static Pen smethod_23(Color color_3)
	{
		return new Pen(color_3);
	}

	// Token: 0x0600025D RID: 605 RVA: 0x00002FF3 File Offset: 0x000011F3
	static void smethod_24(Graphics graphics_0, Pen pen_0, Rectangle rectangle_0)
	{
		graphics_0.DrawEllipse(pen_0, rectangle_0);
	}

	// Token: 0x0600025E RID: 606 RVA: 0x00002476 File Offset: 0x00000676
	static string smethod_25(Control control_0)
	{
		return control_0.Text;
	}

	// Token: 0x0600025F RID: 607 RVA: 0x0000247E File Offset: 0x0000067E
	static Font smethod_26(Control control_0)
	{
		return control_0.Font;
	}

	// Token: 0x04000066 RID: 102
	private GEnum5 genum5_0;

	// Token: 0x04000067 RID: 103
	private int int_0;

	// Token: 0x04000068 RID: 104
	private int int_1;

	// Token: 0x04000069 RID: 105
	private GControl11.GEnum2 genum2_0;

	// Token: 0x0400006A RID: 106
	private bool bool_0;

	// Token: 0x0400006B RID: 107
	[CompilerGenerated]
	private GControl11.GDelegate1 gdelegate1_0;

	// Token: 0x0400006C RID: 108
	private Color color_0 = Color.FromArgb(45, 47, 49);

	// Token: 0x0400006D RID: 109
	private Color color_1 = GClass6.color_0;

	// Token: 0x0400006E RID: 110
	private Color color_2 = Color.FromArgb(243, 243, 243);

	// Token: 0x02000017 RID: 23
	// (Invoke) Token: 0x06000261 RID: 609
	public delegate void GDelegate1(object sender);

	// Token: 0x02000018 RID: 24
	[Flags]
	public enum GEnum2
	{
		// Token: 0x04000070 RID: 112
		Style1 = 0,
		// Token: 0x04000071 RID: 113
		Style2 = 1
	}
}
