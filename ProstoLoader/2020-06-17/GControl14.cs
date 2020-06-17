using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

// Token: 0x0200001B RID: 27
public class GControl14 : TabControl
{
	// Token: 0x060002B1 RID: 689 RVA: 0x00003133 File Offset: 0x00001333
	protected virtual void CreateHandle()
	{
		base.CreateHandle();
		GControl14.smethod_0(this, TabAlignment.Top);
	}

	// Token: 0x17000049 RID: 73
	// (get) Token: 0x060002B2 RID: 690 RVA: 0x00003142 File Offset: 0x00001342
	// (set) Token: 0x060002B3 RID: 691 RVA: 0x0000314A File Offset: 0x0000134A
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

	// Token: 0x1700004A RID: 74
	// (get) Token: 0x060002B4 RID: 692 RVA: 0x00003153 File Offset: 0x00001353
	// (set) Token: 0x060002B5 RID: 693 RVA: 0x0000315B File Offset: 0x0000135B
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

	// Token: 0x060002B6 RID: 694 RVA: 0x000081B4 File Offset: 0x000063B4
	public GControl14()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		GControl14.smethod_1(this, Color.FromArgb(60, 70, 73));
		GControl14.smethod_3(this, GControl14.smethod_2("Segoe UI", 10f));
		GControl14.smethod_4(this, TabSizeMode.Fixed);
		base.ItemSize = new Size(120, 40);
	}

	// Token: 0x060002B7 RID: 695 RVA: 0x00008244 File Offset: 0x00006444
	protected virtual void OnPaint(PaintEventArgs e)
	{
		this.method_0();
		Bitmap bitmap = GControl14.smethod_7(GControl14.smethod_5(this), GControl14.smethod_6(this));
		Graphics graphics = GControl14.smethod_8(bitmap);
		this.int_0 = GControl14.smethod_5(this) - 1;
		this.int_1 = GControl14.smethod_6(this) - 1;
		Graphics graphics2 = graphics;
		GControl14.smethod_9(graphics2, SmoothingMode.HighQuality);
		GControl14.smethod_10(graphics2, PixelOffsetMode.HighQuality);
		GControl14.smethod_11(graphics2, TextRenderingHint.ClearTypeGridFit);
		GControl14.smethod_12(graphics2, this.color_1);
		try
		{
			GControl14.smethod_1(GControl14.smethod_13(this), this.color_0);
		}
		catch
		{
		}
		for (int i = 0; i <= base.TabCount - 1; i++)
		{
			Rectangle rectangle = new Rectangle(new Point(GControl14.smethod_14(this, i).Location.X + 2, base.GetTabRect(i).Location.Y), new Size(base.GetTabRect(i).Width, base.GetTabRect(i).Height));
			Rectangle rectangle2 = new Rectangle(rectangle.Location, new Size(rectangle.Width, rectangle.Height));
			if (i == base.SelectedIndex)
			{
				graphics2.FillRectangle(new SolidBrush(this.color_1), rectangle2);
				graphics2.FillRectangle(new SolidBrush(this.color_2), rectangle2);
				if (base.ImageList != null)
				{
					try
					{
						if (base.ImageList.Images[base.TabPages[i].ImageIndex] != null)
						{
							graphics2.DrawImage(base.ImageList.Images[base.TabPages[i].ImageIndex], new Point(rectangle2.Location.X + 8, rectangle2.Location.Y + 6));
							graphics2.DrawString("      " + base.TabPages[i].Text, this.Font, Brushes.White, rectangle2, GClass6.stringFormat_1);
						}
						else
						{
							graphics2.DrawString(base.TabPages[i].Text, this.Font, Brushes.White, rectangle2, GClass6.stringFormat_1);
						}
						goto IL_3D8;
					}
					catch (Exception ex)
					{
						throw new Exception(ex.Message);
					}
				}
				graphics2.DrawString(base.TabPages[i].Text, this.Font, Brushes.White, rectangle2, GClass6.stringFormat_1);
			}
			else
			{
				graphics2.FillRectangle(new SolidBrush(this.color_1), rectangle2);
				if (base.ImageList != null)
				{
					try
					{
						if (base.ImageList.Images[base.TabPages[i].ImageIndex] != null)
						{
							graphics2.DrawImage(base.ImageList.Images[base.TabPages[i].ImageIndex], new Point(rectangle2.Location.X + 8, rectangle2.Location.Y + 6));
							graphics2.DrawString("      " + base.TabPages[i].Text, this.Font, new SolidBrush(Color.White), rectangle2, new StringFormat
							{
								LineAlignment = StringAlignment.Center,
								Alignment = StringAlignment.Center
							});
						}
						else
						{
							graphics2.DrawString(base.TabPages[i].Text, this.Font, new SolidBrush(Color.White), rectangle2, new StringFormat
							{
								LineAlignment = StringAlignment.Center,
								Alignment = StringAlignment.Center
							});
						}
						goto IL_3D8;
					}
					catch (Exception ex2)
					{
						throw new Exception(ex2.Message);
					}
				}
				graphics2.DrawString(base.TabPages[i].Text, this.Font, new SolidBrush(Color.White), rectangle2, new StringFormat
				{
					LineAlignment = StringAlignment.Center,
					Alignment = StringAlignment.Center
				});
			}
			IL_3D8:;
		}
		base.OnPaint(e);
		graphics.Dispose();
		e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
		bitmap.Dispose();
	}

	// Token: 0x060002B8 RID: 696 RVA: 0x000086AC File Offset: 0x000068AC
	private void method_0()
	{
		GClass0 gclass = GClass6.smethod_3(this);
		this.color_2 = gclass.color_0;
	}

	// Token: 0x060002B9 RID: 697 RVA: 0x00003164 File Offset: 0x00001364
	static void smethod_0(TabControl tabControl_0, TabAlignment tabAlignment_0)
	{
		tabControl_0.Alignment = tabAlignment_0;
	}

	// Token: 0x060002BA RID: 698 RVA: 0x00002372 File Offset: 0x00000572
	static void smethod_1(Control control_0, Color color_3)
	{
		control_0.BackColor = color_3;
	}

	// Token: 0x060002BB RID: 699 RVA: 0x000025D1 File Offset: 0x000007D1
	static Font smethod_2(string string_0, float float_0)
	{
		return new Font(string_0, float_0);
	}

	// Token: 0x060002BC RID: 700 RVA: 0x000025DA File Offset: 0x000007DA
	static void smethod_3(Control control_0, Font font_0)
	{
		control_0.Font = font_0;
	}

	// Token: 0x060002BD RID: 701 RVA: 0x0000316D File Offset: 0x0000136D
	static void smethod_4(TabControl tabControl_0, TabSizeMode tabSizeMode_0)
	{
		tabControl_0.SizeMode = tabSizeMode_0;
	}

	// Token: 0x060002BE RID: 702 RVA: 0x0000237B File Offset: 0x0000057B
	static int smethod_5(Control control_0)
	{
		return control_0.Width;
	}

	// Token: 0x060002BF RID: 703 RVA: 0x00002383 File Offset: 0x00000583
	static int smethod_6(Control control_0)
	{
		return control_0.Height;
	}

	// Token: 0x060002C0 RID: 704 RVA: 0x0000238B File Offset: 0x0000058B
	static Bitmap smethod_7(int int_2, int int_3)
	{
		return new Bitmap(int_2, int_3);
	}

	// Token: 0x060002C1 RID: 705 RVA: 0x00002394 File Offset: 0x00000594
	static Graphics smethod_8(Image image_0)
	{
		return Graphics.FromImage(image_0);
	}

	// Token: 0x060002C2 RID: 706 RVA: 0x0000239C File Offset: 0x0000059C
	static void smethod_9(Graphics graphics_0, SmoothingMode smoothingMode_0)
	{
		graphics_0.SmoothingMode = smoothingMode_0;
	}

	// Token: 0x060002C3 RID: 707 RVA: 0x00002463 File Offset: 0x00000663
	static void smethod_10(Graphics graphics_0, PixelOffsetMode pixelOffsetMode_0)
	{
		graphics_0.PixelOffsetMode = pixelOffsetMode_0;
	}

	// Token: 0x060002C4 RID: 708 RVA: 0x000023A5 File Offset: 0x000005A5
	static void smethod_11(Graphics graphics_0, TextRenderingHint textRenderingHint_0)
	{
		graphics_0.TextRenderingHint = textRenderingHint_0;
	}

	// Token: 0x060002C5 RID: 709 RVA: 0x000023B6 File Offset: 0x000005B6
	static void smethod_12(Graphics graphics_0, Color color_3)
	{
		graphics_0.Clear(color_3);
	}

	// Token: 0x060002C6 RID: 710 RVA: 0x00003176 File Offset: 0x00001376
	static TabPage smethod_13(TabControl tabControl_0)
	{
		return tabControl_0.SelectedTab;
	}

	// Token: 0x060002C7 RID: 711 RVA: 0x0000317E File Offset: 0x0000137E
	static Rectangle smethod_14(TabControl tabControl_0, int int_2)
	{
		return tabControl_0.GetTabRect(int_2);
	}

	// Token: 0x0400007E RID: 126
	private int int_0;

	// Token: 0x0400007F RID: 127
	private int int_1;

	// Token: 0x04000080 RID: 128
	private Color color_0 = Color.FromArgb(60, 70, 73);

	// Token: 0x04000081 RID: 129
	private Color color_1 = Color.FromArgb(45, 47, 49);

	// Token: 0x04000082 RID: 130
	private Color color_2 = GClass6.color_0;
}
