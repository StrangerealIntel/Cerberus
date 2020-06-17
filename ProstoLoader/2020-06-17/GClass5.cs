using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

// Token: 0x02000023 RID: 35
public class GClass5 : TreeView
{
	// Token: 0x0600036D RID: 877 RVA: 0x000096B4 File Offset: 0x000078B4
	protected virtual void OnDrawNode(DrawTreeNodeEventArgs e)
	{
		try
		{
			Rectangle rect = new Rectangle(GClass5.smethod_0(e).Location.X, e.Bounds.Location.Y, e.Bounds.Width, e.Bounds.Height);
			TreeNodeStates treeNodeStates = this.treeNodeStates_0;
			if (treeNodeStates != TreeNodeStates.Selected)
			{
				if (treeNodeStates == TreeNodeStates.Checked)
				{
					e.Graphics.FillRectangle(Brushes.Green, rect);
					e.Graphics.DrawString(e.Node.Text, new Font("Segoe UI", 8f), Brushes.Black, new Rectangle(rect.X + 2, rect.Y + 2, rect.Width, rect.Height), GClass6.stringFormat_0);
					base.Invalidate();
				}
				else if (treeNodeStates == TreeNodeStates.Default)
				{
					e.Graphics.FillRectangle(Brushes.Red, rect);
					e.Graphics.DrawString(e.Node.Text, new Font("Segoe UI", 8f), Brushes.LimeGreen, new Rectangle(rect.X + 2, rect.Y + 2, rect.Width, rect.Height), GClass6.stringFormat_0);
					base.Invalidate();
				}
			}
			else
			{
				e.Graphics.FillRectangle(Brushes.Green, rect);
				e.Graphics.DrawString(e.Node.Text, new Font("Segoe UI", 8f), Brushes.Black, new Rectangle(rect.X + 2, rect.Y + 2, rect.Width, rect.Height), GClass6.stringFormat_0);
				base.Invalidate();
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
		}
		base.OnDrawNode(e);
	}

	// Token: 0x0600036E RID: 878 RVA: 0x000098B8 File Offset: 0x00007AB8
	public GClass5()
	{
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		GClass5.smethod_1(this, this.color_0);
		GClass5.smethod_2(this, Color.White);
		GClass5.smethod_3(this, this.color_1);
		GClass5.smethod_4(this, TreeViewDrawMode.OwnerDrawAll);
	}

	// Token: 0x0600036F RID: 879 RVA: 0x0000992C File Offset: 0x00007B2C
	protected virtual void OnPaint(PaintEventArgs e)
	{
		Bitmap bitmap = GClass5.smethod_7(GClass5.smethod_5(this), GClass5.smethod_6(this));
		Graphics graphics = GClass5.smethod_8(bitmap);
		Rectangle rectangle_ = new Rectangle(0, 0, GClass5.smethod_5(this), GClass5.smethod_6(this));
		GClass5.smethod_9(graphics, SmoothingMode.HighQuality);
		GClass5.smethod_10(graphics, PixelOffsetMode.HighQuality);
		GClass5.smethod_11(graphics, TextRenderingHint.ClearTypeGridFit);
		GClass5.smethod_13(graphics, GClass5.smethod_12(this));
		GClass5.smethod_15(graphics, GClass5.smethod_14(this.color_0), rectangle_);
		graphics.DrawString(GClass5.smethod_16(this), GClass5.smethod_17("Segoe UI", 8f), GClass5.smethod_18(), new Rectangle(this.method_0().X + 2, base.Bounds.Y + 2, base.Bounds.Width, base.Bounds.Height), GClass6.stringFormat_0);
		base.OnPaint(e);
		graphics.Dispose();
		e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
		bitmap.Dispose();
	}

	// Token: 0x06000370 RID: 880 RVA: 0x000035AA File Offset: 0x000017AA
	static Rectangle smethod_0(DrawTreeNodeEventArgs drawTreeNodeEventArgs_0)
	{
		return drawTreeNodeEventArgs_0.Bounds;
	}

	// Token: 0x06000371 RID: 881 RVA: 0x00002372 File Offset: 0x00000572
	static void smethod_1(Control control_0, Color color_2)
	{
		control_0.BackColor = color_2;
	}

	// Token: 0x06000372 RID: 882 RVA: 0x000028AF File Offset: 0x00000AAF
	static void smethod_2(Control control_0, Color color_2)
	{
		control_0.ForeColor = color_2;
	}

	// Token: 0x06000373 RID: 883 RVA: 0x000035B2 File Offset: 0x000017B2
	static void smethod_3(TreeView treeView_0, Color color_2)
	{
		treeView_0.LineColor = color_2;
	}

	// Token: 0x06000374 RID: 884 RVA: 0x000035BB File Offset: 0x000017BB
	static void smethod_4(TreeView treeView_0, TreeViewDrawMode treeViewDrawMode_0)
	{
		treeView_0.DrawMode = treeViewDrawMode_0;
	}

	// Token: 0x06000375 RID: 885 RVA: 0x0000237B File Offset: 0x0000057B
	static int smethod_5(Control control_0)
	{
		return control_0.Width;
	}

	// Token: 0x06000376 RID: 886 RVA: 0x00002383 File Offset: 0x00000583
	static int smethod_6(Control control_0)
	{
		return control_0.Height;
	}

	// Token: 0x06000377 RID: 887 RVA: 0x0000238B File Offset: 0x0000058B
	static Bitmap smethod_7(int int_0, int int_1)
	{
		return new Bitmap(int_0, int_1);
	}

	// Token: 0x06000378 RID: 888 RVA: 0x00002394 File Offset: 0x00000594
	static Graphics smethod_8(Image image_0)
	{
		return Graphics.FromImage(image_0);
	}

	// Token: 0x06000379 RID: 889 RVA: 0x0000239C File Offset: 0x0000059C
	static void smethod_9(Graphics graphics_0, SmoothingMode smoothingMode_0)
	{
		graphics_0.SmoothingMode = smoothingMode_0;
	}

	// Token: 0x0600037A RID: 890 RVA: 0x00002463 File Offset: 0x00000663
	static void smethod_10(Graphics graphics_0, PixelOffsetMode pixelOffsetMode_0)
	{
		graphics_0.PixelOffsetMode = pixelOffsetMode_0;
	}

	// Token: 0x0600037B RID: 891 RVA: 0x000023A5 File Offset: 0x000005A5
	static void smethod_11(Graphics graphics_0, TextRenderingHint textRenderingHint_0)
	{
		graphics_0.TextRenderingHint = textRenderingHint_0;
	}

	// Token: 0x0600037C RID: 892 RVA: 0x000023AE File Offset: 0x000005AE
	static Color smethod_12(Control control_0)
	{
		return control_0.BackColor;
	}

	// Token: 0x0600037D RID: 893 RVA: 0x000023B6 File Offset: 0x000005B6
	static void smethod_13(Graphics graphics_0, Color color_2)
	{
		graphics_0.Clear(color_2);
	}

	// Token: 0x0600037E RID: 894 RVA: 0x000023BF File Offset: 0x000005BF
	static SolidBrush smethod_14(Color color_2)
	{
		return new SolidBrush(color_2);
	}

	// Token: 0x0600037F RID: 895 RVA: 0x000023C7 File Offset: 0x000005C7
	static void smethod_15(Graphics graphics_0, Brush brush_0, Rectangle rectangle_0)
	{
		graphics_0.FillRectangle(brush_0, rectangle_0);
	}

	// Token: 0x06000380 RID: 896 RVA: 0x00002476 File Offset: 0x00000676
	static string smethod_16(Control control_0)
	{
		return control_0.Text;
	}

	// Token: 0x06000381 RID: 897 RVA: 0x000025D1 File Offset: 0x000007D1
	static Font smethod_17(string string_0, float float_0)
	{
		return new Font(string_0, float_0);
	}

	// Token: 0x06000382 RID: 898 RVA: 0x000035C4 File Offset: 0x000017C4
	static Brush smethod_18()
	{
		return Brushes.Black;
	}

	// Token: 0x06000383 RID: 899 RVA: 0x000035CB File Offset: 0x000017CB
	Rectangle method_0()
	{
		return base.Bounds;
	}

	// Token: 0x040000B4 RID: 180
	private TreeNodeStates treeNodeStates_0;

	// Token: 0x040000B5 RID: 181
	private Color color_0 = Color.FromArgb(45, 47, 49);

	// Token: 0x040000B6 RID: 182
	private Color color_1 = Color.FromArgb(25, 27, 29);
}
