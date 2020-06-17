using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

// Token: 0x02000011 RID: 17
public class GControl6 : Control
{
	// Token: 0x1700002A RID: 42
	// (get) Token: 0x06000172 RID: 370 RVA: 0x00002A33 File Offset: 0x00000C33
	// (set) Token: 0x06000173 RID: 371 RVA: 0x00006628 File Offset: 0x00004828
	private ListBox ListBox_0
	{
		get
		{
			return this.listBox_0;
		}
		set
		{
			if (this.listBox_0 != null)
			{
				GControl6.smethod_0(this.listBox_0, new DrawItemEventHandler(this.method_2));
			}
			this.listBox_0 = value;
			if (this.listBox_0 != null)
			{
				GControl6.smethod_1(this.listBox_0, new DrawItemEventHandler(this.method_2));
			}
		}
	}

	// Token: 0x1700002B RID: 43
	// (get) Token: 0x06000174 RID: 372 RVA: 0x00002A3B File Offset: 0x00000C3B
	// (set) Token: 0x06000175 RID: 373 RVA: 0x0000667C File Offset: 0x0000487C
	[Category("Options")]
	public string[] String_0
	{
		get
		{
			return this.string_0;
		}
		set
		{
			this.string_0 = value;
			GControl6.smethod_3(GControl6.smethod_2(this.ListBox_0));
			GControl6.smethod_4(GControl6.smethod_2(this.ListBox_0), value);
			GControl6.smethod_5(this);
		}
	}

	// Token: 0x1700002C RID: 44
	// (get) Token: 0x06000176 RID: 374 RVA: 0x00002A43 File Offset: 0x00000C43
	// (set) Token: 0x06000177 RID: 375 RVA: 0x00002A4B File Offset: 0x00000C4B
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

	// Token: 0x1700002D RID: 45
	// (get) Token: 0x06000178 RID: 376 RVA: 0x00002A54 File Offset: 0x00000C54
	public string String_1
	{
		get
		{
			return GControl6.smethod_7(GControl6.smethod_6(this.ListBox_0));
		}
	}

	// Token: 0x1700002E RID: 46
	// (get) Token: 0x06000179 RID: 377 RVA: 0x00002A66 File Offset: 0x00000C66
	public int Int32_0
	{
		get
		{
			return GControl6.smethod_8(this.ListBox_0);
		}
	}

	// Token: 0x0600017A RID: 378 RVA: 0x00002A73 File Offset: 0x00000C73
	public void method_0()
	{
		GControl6.smethod_3(GControl6.smethod_2(this.ListBox_0));
	}

	// Token: 0x0600017B RID: 379 RVA: 0x000066BC File Offset: 0x000048BC
	public void method_1()
	{
		for (int i = GControl6.smethod_10(GControl6.smethod_9(this.ListBox_0)) - 1; i >= 0; i += -1)
		{
			GControl6.smethod_12(GControl6.smethod_2(this.ListBox_0), GControl6.smethod_11(GControl6.smethod_9(this.ListBox_0), i));
		}
	}

	// Token: 0x0600017C RID: 380 RVA: 0x00006708 File Offset: 0x00004908
	public void method_2(object sender, DrawItemEventArgs e)
	{
		if (GControl6.smethod_13(e) < 0)
		{
			return;
		}
		GControl6.smethod_14(e);
		GControl6.smethod_15(e);
		GControl6.smethod_17(GControl6.smethod_16(e), SmoothingMode.HighQuality);
		GControl6.smethod_18(GControl6.smethod_16(e), PixelOffsetMode.HighQuality);
		GControl6.smethod_19(GControl6.smethod_16(e), InterpolationMode.HighQualityBicubic);
		GControl6.smethod_20(GControl6.smethod_16(e), TextRenderingHint.ClearTypeGridFit);
		if (GControl6.smethod_22(GControl6.smethod_21(e).ToString(), "Selected,") < 0)
		{
			e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(51, 53, 55)), new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
			e.Graphics.DrawString(" " + this.ListBox_0.Items[e.Index].ToString(), new Font("Segoe UI", 8f), Brushes.White, (float)e.Bounds.X, (float)(e.Bounds.Y + 2));
		}
		else
		{
			GControl6.smethod_16(e).FillRectangle(GControl6.smethod_23(this.color_1), new Rectangle(GControl6.smethod_24(e).X, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height));
			e.Graphics.DrawString(" " + this.ListBox_0.Items[e.Index].ToString(), new Font("Segoe UI", 8f), Brushes.White, (float)e.Bounds.X, (float)(e.Bounds.Y + 2));
		}
		e.Graphics.Dispose();
	}

	// Token: 0x0600017D RID: 381 RVA: 0x00002A85 File Offset: 0x00000C85
	protected virtual void OnCreateControl()
	{
		base.OnCreateControl();
		if (!GControl6.smethod_26(GControl6.smethod_25(this), this.ListBox_0))
		{
			GControl6.smethod_27(GControl6.smethod_25(this), this.ListBox_0);
		}
	}

	// Token: 0x0600017E RID: 382 RVA: 0x00002AB1 File Offset: 0x00000CB1
	public void method_3(object[] object_0)
	{
		GControl6.smethod_12(GControl6.smethod_2(this.ListBox_0), "");
		GControl6.smethod_4(GControl6.smethod_2(this.ListBox_0), object_0);
	}

	// Token: 0x0600017F RID: 383 RVA: 0x00002AD9 File Offset: 0x00000CD9
	public void method_4(object object_0)
	{
		GControl6.smethod_12(GControl6.smethod_2(this.ListBox_0), "");
		GControl6.smethod_28(GControl6.smethod_2(this.ListBox_0), object_0);
	}

	// Token: 0x06000180 RID: 384 RVA: 0x00006904 File Offset: 0x00004B04
	public GControl6()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		GControl6.smethod_30(this.ListBox_0, DrawMode.OwnerDrawFixed);
		GControl6.smethod_31(this.ListBox_0, false);
		GControl6.smethod_32(this.ListBox_0, false);
		GControl6.smethod_33(this.ListBox_0, BorderStyle.None);
		GControl6.smethod_34(this.ListBox_0, this.color_0);
		GControl6.smethod_35(this.ListBox_0, Color.White);
		this.ListBox_0.Location = new Point(3, 3);
		this.ListBox_0.Font = new Font("Segoe UI", 8f);
		this.ListBox_0.ItemHeight = 20;
		this.ListBox_0.Items.Clear();
		this.ListBox_0.IntegralHeight = false;
		base.Size = new Size(131, 101);
		this.BackColor = this.color_0;
	}

	// Token: 0x06000181 RID: 385 RVA: 0x00006A2C File Offset: 0x00004C2C
	protected virtual void OnPaint(PaintEventArgs e)
	{
		this.method_5();
		Bitmap bitmap = GControl6.smethod_38(GControl6.smethod_36(this), GControl6.smethod_37(this));
		Graphics graphics = GControl6.smethod_39(bitmap);
		Rectangle rect = new Rectangle(0, 0, GControl6.smethod_36(this), GControl6.smethod_37(this));
		GControl6.smethod_17(graphics, SmoothingMode.HighQuality);
		GControl6.smethod_18(graphics, PixelOffsetMode.HighQuality);
		GControl6.smethod_20(graphics, TextRenderingHint.ClearTypeGridFit);
		GControl6.smethod_41(graphics, GControl6.smethod_40(this));
		this.ListBox_0.Size = new Size(GControl6.smethod_36(this) - 6, GControl6.smethod_37(this) - 2);
		graphics.FillRectangle(new SolidBrush(this.color_0), rect);
		base.OnPaint(e);
		graphics.Dispose();
		e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
		bitmap.Dispose();
	}

	// Token: 0x06000182 RID: 386 RVA: 0x00006AEC File Offset: 0x00004CEC
	private void method_5()
	{
		GClass0 gclass = GClass6.smethod_3(this);
		this.color_1 = gclass.color_0;
	}

	// Token: 0x06000183 RID: 387 RVA: 0x00002B02 File Offset: 0x00000D02
	static void smethod_0(ListBox listBox_1, DrawItemEventHandler drawItemEventHandler_0)
	{
		listBox_1.DrawItem -= drawItemEventHandler_0;
	}

	// Token: 0x06000184 RID: 388 RVA: 0x00002B0B File Offset: 0x00000D0B
	static void smethod_1(ListBox listBox_1, DrawItemEventHandler drawItemEventHandler_0)
	{
		listBox_1.DrawItem += drawItemEventHandler_0;
	}

	// Token: 0x06000185 RID: 389 RVA: 0x00002B14 File Offset: 0x00000D14
	static ListBox.ObjectCollection smethod_2(ListBox listBox_1)
	{
		return listBox_1.Items;
	}

	// Token: 0x06000186 RID: 390 RVA: 0x00002B1C File Offset: 0x00000D1C
	static void smethod_3(ListBox.ObjectCollection objectCollection_0)
	{
		objectCollection_0.Clear();
	}

	// Token: 0x06000187 RID: 391 RVA: 0x00002B24 File Offset: 0x00000D24
	static void smethod_4(ListBox.ObjectCollection objectCollection_0, object[] object_0)
	{
		objectCollection_0.AddRange(object_0);
	}

	// Token: 0x06000188 RID: 392 RVA: 0x0000232F File Offset: 0x0000052F
	static void smethod_5(Control control_0)
	{
		control_0.Invalidate();
	}

	// Token: 0x06000189 RID: 393 RVA: 0x00002B2D File Offset: 0x00000D2D
	static object smethod_6(ListBox listBox_1)
	{
		return listBox_1.SelectedItem;
	}

	// Token: 0x0600018A RID: 394 RVA: 0x00002128 File Offset: 0x00000328
	static string smethod_7(object object_0)
	{
		return object_0.ToString();
	}

	// Token: 0x0600018B RID: 395 RVA: 0x00002B35 File Offset: 0x00000D35
	static int smethod_8(ListControl listControl_0)
	{
		return listControl_0.SelectedIndex;
	}

	// Token: 0x0600018C RID: 396 RVA: 0x00002B3D File Offset: 0x00000D3D
	static ListBox.SelectedObjectCollection smethod_9(ListBox listBox_1)
	{
		return listBox_1.SelectedItems;
	}

	// Token: 0x0600018D RID: 397 RVA: 0x00002B45 File Offset: 0x00000D45
	static int smethod_10(ListBox.SelectedObjectCollection selectedObjectCollection_0)
	{
		return selectedObjectCollection_0.Count;
	}

	// Token: 0x0600018E RID: 398 RVA: 0x00002B4D File Offset: 0x00000D4D
	static object smethod_11(ListBox.SelectedObjectCollection selectedObjectCollection_0, int int_0)
	{
		return selectedObjectCollection_0[int_0];
	}

	// Token: 0x0600018F RID: 399 RVA: 0x00002B56 File Offset: 0x00000D56
	static void smethod_12(ListBox.ObjectCollection objectCollection_0, object object_0)
	{
		objectCollection_0.Remove(object_0);
	}

	// Token: 0x06000190 RID: 400 RVA: 0x00002854 File Offset: 0x00000A54
	static int smethod_13(DrawItemEventArgs drawItemEventArgs_0)
	{
		return drawItemEventArgs_0.Index;
	}

	// Token: 0x06000191 RID: 401 RVA: 0x0000285C File Offset: 0x00000A5C
	static void smethod_14(DrawItemEventArgs drawItemEventArgs_0)
	{
		drawItemEventArgs_0.DrawBackground();
	}

	// Token: 0x06000192 RID: 402 RVA: 0x00002864 File Offset: 0x00000A64
	static void smethod_15(DrawItemEventArgs drawItemEventArgs_0)
	{
		drawItemEventArgs_0.DrawFocusRectangle();
	}

	// Token: 0x06000193 RID: 403 RVA: 0x0000286C File Offset: 0x00000A6C
	static Graphics smethod_16(DrawItemEventArgs drawItemEventArgs_0)
	{
		return drawItemEventArgs_0.Graphics;
	}

	// Token: 0x06000194 RID: 404 RVA: 0x0000239C File Offset: 0x0000059C
	static void smethod_17(Graphics graphics_0, SmoothingMode smoothingMode_0)
	{
		graphics_0.SmoothingMode = smoothingMode_0;
	}

	// Token: 0x06000195 RID: 405 RVA: 0x00002463 File Offset: 0x00000663
	static void smethod_18(Graphics graphics_0, PixelOffsetMode pixelOffsetMode_0)
	{
		graphics_0.PixelOffsetMode = pixelOffsetMode_0;
	}

	// Token: 0x06000196 RID: 406 RVA: 0x000024A5 File Offset: 0x000006A5
	static void smethod_19(Graphics graphics_0, InterpolationMode interpolationMode_0)
	{
		graphics_0.InterpolationMode = interpolationMode_0;
	}

	// Token: 0x06000197 RID: 407 RVA: 0x000023A5 File Offset: 0x000005A5
	static void smethod_20(Graphics graphics_0, TextRenderingHint textRenderingHint_0)
	{
		graphics_0.TextRenderingHint = textRenderingHint_0;
	}

	// Token: 0x06000198 RID: 408 RVA: 0x00002843 File Offset: 0x00000A43
	static DrawItemState smethod_21(DrawItemEventArgs drawItemEventArgs_0)
	{
		return drawItemEventArgs_0.State;
	}

	// Token: 0x06000199 RID: 409 RVA: 0x00002B5F File Offset: 0x00000D5F
	static int smethod_22(string string_1, string string_2)
	{
		return string_1.IndexOf(string_2);
	}

	// Token: 0x0600019A RID: 410 RVA: 0x000023BF File Offset: 0x000005BF
	static SolidBrush smethod_23(Color color_2)
	{
		return new SolidBrush(color_2);
	}

	// Token: 0x0600019B RID: 411 RVA: 0x00002874 File Offset: 0x00000A74
	static Rectangle smethod_24(DrawItemEventArgs drawItemEventArgs_0)
	{
		return drawItemEventArgs_0.Bounds;
	}

	// Token: 0x0600019C RID: 412 RVA: 0x00002B68 File Offset: 0x00000D68
	static Control.ControlCollection smethod_25(Control control_0)
	{
		return control_0.Controls;
	}

	// Token: 0x0600019D RID: 413 RVA: 0x00002B70 File Offset: 0x00000D70
	static bool smethod_26(Control.ControlCollection controlCollection_0, Control control_0)
	{
		return controlCollection_0.Contains(control_0);
	}

	// Token: 0x0600019E RID: 414 RVA: 0x00002B79 File Offset: 0x00000D79
	static void smethod_27(Control.ControlCollection controlCollection_0, Control control_0)
	{
		controlCollection_0.Add(control_0);
	}

	// Token: 0x0600019F RID: 415 RVA: 0x00002B82 File Offset: 0x00000D82
	static int smethod_28(ListBox.ObjectCollection objectCollection_0, object object_0)
	{
		return objectCollection_0.Add(object_0);
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x00002B8B File Offset: 0x00000D8B
	static ListBox smethod_29()
	{
		return new ListBox();
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x00002B92 File Offset: 0x00000D92
	static void smethod_30(ListBox listBox_1, DrawMode drawMode_0)
	{
		listBox_1.DrawMode = drawMode_0;
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x00002B9B File Offset: 0x00000D9B
	static void smethod_31(ListBox listBox_1, bool bool_0)
	{
		listBox_1.ScrollAlwaysVisible = bool_0;
	}

	// Token: 0x060001A3 RID: 419 RVA: 0x00002BA4 File Offset: 0x00000DA4
	static void smethod_32(ListBox listBox_1, bool bool_0)
	{
		listBox_1.HorizontalScrollbar = bool_0;
	}

	// Token: 0x060001A4 RID: 420 RVA: 0x00002BAD File Offset: 0x00000DAD
	static void smethod_33(ListBox listBox_1, BorderStyle borderStyle_0)
	{
		listBox_1.BorderStyle = borderStyle_0;
	}

	// Token: 0x060001A5 RID: 421 RVA: 0x00002372 File Offset: 0x00000572
	static void smethod_34(Control control_0, Color color_2)
	{
		control_0.BackColor = color_2;
	}

	// Token: 0x060001A6 RID: 422 RVA: 0x000028AF File Offset: 0x00000AAF
	static void smethod_35(Control control_0, Color color_2)
	{
		control_0.ForeColor = color_2;
	}

	// Token: 0x060001A7 RID: 423 RVA: 0x0000237B File Offset: 0x0000057B
	static int smethod_36(Control control_0)
	{
		return control_0.Width;
	}

	// Token: 0x060001A8 RID: 424 RVA: 0x00002383 File Offset: 0x00000583
	static int smethod_37(Control control_0)
	{
		return control_0.Height;
	}

	// Token: 0x060001A9 RID: 425 RVA: 0x0000238B File Offset: 0x0000058B
	static Bitmap smethod_38(int int_0, int int_1)
	{
		return new Bitmap(int_0, int_1);
	}

	// Token: 0x060001AA RID: 426 RVA: 0x00002394 File Offset: 0x00000594
	static Graphics smethod_39(Image image_0)
	{
		return Graphics.FromImage(image_0);
	}

	// Token: 0x060001AB RID: 427 RVA: 0x000023AE File Offset: 0x000005AE
	static Color smethod_40(Control control_0)
	{
		return control_0.BackColor;
	}

	// Token: 0x060001AC RID: 428 RVA: 0x000023B6 File Offset: 0x000005B6
	static void smethod_41(Graphics graphics_0, Color color_2)
	{
		graphics_0.Clear(color_2);
	}

	// Token: 0x04000045 RID: 69
	private ListBox listBox_0 = GControl6.smethod_29();

	// Token: 0x04000046 RID: 70
	private string[] string_0 = new string[]
	{
		""
	};

	// Token: 0x04000047 RID: 71
	private Color color_0 = Color.FromArgb(45, 47, 49);

	// Token: 0x04000048 RID: 72
	private Color color_1 = GClass6.color_0;
}
