using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

// Token: 0x02000024 RID: 36
public class GControl18 : ContainerControl
{
	// Token: 0x1700005E RID: 94
	// (get) Token: 0x06000384 RID: 900 RVA: 0x000035D3 File Offset: 0x000017D3
	// (set) Token: 0x06000385 RID: 901 RVA: 0x000035DB File Offset: 0x000017DB
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

	// Token: 0x1700005F RID: 95
	// (get) Token: 0x06000386 RID: 902 RVA: 0x000035E4 File Offset: 0x000017E4
	// (set) Token: 0x06000387 RID: 903 RVA: 0x000035EC File Offset: 0x000017EC
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

	// Token: 0x17000060 RID: 96
	// (get) Token: 0x06000388 RID: 904 RVA: 0x000035F5 File Offset: 0x000017F5
	// (set) Token: 0x06000389 RID: 905 RVA: 0x000035FD File Offset: 0x000017FD
	[Category("Colors")]
	public Color Color_2
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

	// Token: 0x17000061 RID: 97
	// (get) Token: 0x0600038A RID: 906 RVA: 0x00003606 File Offset: 0x00001806
	// (set) Token: 0x0600038B RID: 907 RVA: 0x0000360E File Offset: 0x0000180E
	[Category("Colors")]
	public Color Color_3
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

	// Token: 0x17000062 RID: 98
	// (get) Token: 0x0600038C RID: 908 RVA: 0x00003617 File Offset: 0x00001817
	// (set) Token: 0x0600038D RID: 909 RVA: 0x0000361F File Offset: 0x0000181F
	[Category("Options")]
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

	// Token: 0x0600038E RID: 910 RVA: 0x00009A30 File Offset: 0x00007C30
	protected virtual void OnMouseDown(MouseEventArgs e)
	{
		base.OnMouseDown(e);
		if (GControl18.smethod_0(e) == MouseButtons.Left & new Rectangle(0, 0, GControl18.smethod_1(this), this.int_2).Contains(e.Location))
		{
			this.bool_0 = true;
			this.point_0 = e.Location;
		}
	}

	// Token: 0x0600038F RID: 911 RVA: 0x00009A88 File Offset: 0x00007C88
	private void GControl18_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		if (this.Boolean_0 && (GControl18.smethod_0(e) == MouseButtons.Left & new Rectangle(0, 0, GControl18.smethod_1(this), this.int_2).Contains(e.Location)))
		{
			if (base.FindForm().WindowState == FormWindowState.Normal)
			{
				base.FindForm().WindowState = FormWindowState.Maximized;
				base.FindForm().Refresh();
				return;
			}
			if (base.FindForm().WindowState == FormWindowState.Maximized)
			{
				base.FindForm().WindowState = FormWindowState.Normal;
				base.FindForm().Refresh();
			}
		}
	}

	// Token: 0x06000390 RID: 912 RVA: 0x00003628 File Offset: 0x00001828
	protected virtual void OnMouseUp(MouseEventArgs e)
	{
		base.OnMouseUp(e);
		this.bool_0 = false;
	}

	// Token: 0x06000391 RID: 913 RVA: 0x00009B1C File Offset: 0x00007D1C
	protected virtual void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);
		if (this.bool_0)
		{
			GControl18.smethod_2(this).Location = new Point(GControl18.smethod_3().X - this.point_0.X, Control.MousePosition.Y - this.point_0.Y);
		}
	}

	// Token: 0x06000392 RID: 914 RVA: 0x00009B7C File Offset: 0x00007D7C
	protected virtual void OnCreateControl()
	{
		base.OnCreateControl();
		GControl18.smethod_5(GControl18.smethod_4(this), FormBorderStyle.None);
		GControl18.smethod_6(GControl18.smethod_4(this), false);
		GControl18.smethod_7(GControl18.smethod_4(this), Color.Fuchsia);
		GControl18.smethod_9(GControl18.smethod_8(GControl18.smethod_4(this)), FormStartPosition.CenterScreen);
		GControl18.smethod_10(this, DockStyle.Fill);
		GControl18.smethod_11(this);
	}

	// Token: 0x06000393 RID: 915 RVA: 0x00009BD8 File Offset: 0x00007DD8
	public GControl18()
	{
		base.MouseDoubleClick += this.GControl18_MouseDoubleClick;
		base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
		this.DoubleBuffered = true;
		this.BackColor = Color.White;
		this.Font = new Font("Segoe UI", 12f);
	}

	// Token: 0x06000394 RID: 916 RVA: 0x00009CE4 File Offset: 0x00007EE4
	protected virtual void OnPaint(PaintEventArgs e)
	{
		Bitmap bitmap = GControl18.smethod_13(GControl18.smethod_1(this), GControl18.smethod_12(this));
		Graphics graphics = GControl18.smethod_14(bitmap);
		this.int_0 = GControl18.smethod_1(this);
		this.int_1 = GControl18.smethod_12(this);
		Rectangle rectangle = new Rectangle(0, 0, this.int_0, this.int_1);
		Rectangle rectangle_ = new Rectangle(0, 0, this.int_0, 50);
		GControl18.smethod_15(graphics, SmoothingMode.HighQuality);
		GControl18.smethod_16(graphics, PixelOffsetMode.HighQuality);
		GControl18.smethod_17(graphics, TextRenderingHint.ClearTypeGridFit);
		GControl18.smethod_19(graphics, GControl18.smethod_18(this));
		GControl18.smethod_21(graphics, GControl18.smethod_20(this.color_1), rectangle);
		GControl18.smethod_21(graphics, GControl18.smethod_20(this.color_0), rectangle_);
		graphics.FillRectangle(GControl18.smethod_20(Color.FromArgb(243, 243, 243)), new Rectangle(13, 16, 4, 18));
		graphics.DrawString(this.Text, this.Font, new SolidBrush(this.color_4), new Rectangle(26, 15, this.int_0, this.int_1), GClass6.stringFormat_0);
		graphics.DrawRectangle(new Pen(this.color_2), rectangle);
		base.OnPaint(e);
		graphics.Dispose();
		e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		e.Graphics.DrawImageUnscaled(bitmap, 0, 0);
		bitmap.Dispose();
	}

	// Token: 0x06000395 RID: 917 RVA: 0x0000359A File Offset: 0x0000179A
	static MouseButtons smethod_0(MouseEventArgs mouseEventArgs_0)
	{
		return mouseEventArgs_0.Button;
	}

	// Token: 0x06000396 RID: 918 RVA: 0x0000237B File Offset: 0x0000057B
	static int smethod_1(Control control_0)
	{
		return control_0.Width;
	}

	// Token: 0x06000397 RID: 919 RVA: 0x00002FB9 File Offset: 0x000011B9
	static Control smethod_2(Control control_0)
	{
		return control_0.Parent;
	}

	// Token: 0x06000398 RID: 920 RVA: 0x00003638 File Offset: 0x00001838
	static Point smethod_3()
	{
		return Control.MousePosition;
	}

	// Token: 0x06000399 RID: 921 RVA: 0x0000363F File Offset: 0x0000183F
	static Form smethod_4(ContainerControl containerControl_0)
	{
		return containerControl_0.ParentForm;
	}

	// Token: 0x0600039A RID: 922 RVA: 0x00003647 File Offset: 0x00001847
	static void smethod_5(Form form_0, FormBorderStyle formBorderStyle_0)
	{
		form_0.FormBorderStyle = formBorderStyle_0;
	}

	// Token: 0x0600039B RID: 923 RVA: 0x00003650 File Offset: 0x00001850
	static void smethod_6(Form form_0, bool bool_2)
	{
		form_0.AllowTransparency = bool_2;
	}

	// Token: 0x0600039C RID: 924 RVA: 0x00003659 File Offset: 0x00001859
	static void smethod_7(Form form_0, Color color_8)
	{
		form_0.TransparencyKey = color_8;
	}

	// Token: 0x0600039D RID: 925 RVA: 0x00003662 File Offset: 0x00001862
	static Form smethod_8(Control control_0)
	{
		return control_0.FindForm();
	}

	// Token: 0x0600039E RID: 926 RVA: 0x0000366A File Offset: 0x0000186A
	static void smethod_9(Form form_0, FormStartPosition formStartPosition_0)
	{
		form_0.StartPosition = formStartPosition_0;
	}

	// Token: 0x0600039F RID: 927 RVA: 0x0000305F File Offset: 0x0000125F
	static void smethod_10(Control control_0, DockStyle dockStyle_0)
	{
		control_0.Dock = dockStyle_0;
	}

	// Token: 0x060003A0 RID: 928 RVA: 0x0000232F File Offset: 0x0000052F
	static void smethod_11(Control control_0)
	{
		control_0.Invalidate();
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x00002383 File Offset: 0x00000583
	static int smethod_12(Control control_0)
	{
		return control_0.Height;
	}

	// Token: 0x060003A2 RID: 930 RVA: 0x0000238B File Offset: 0x0000058B
	static Bitmap smethod_13(int int_3, int int_4)
	{
		return new Bitmap(int_3, int_4);
	}

	// Token: 0x060003A3 RID: 931 RVA: 0x00002394 File Offset: 0x00000594
	static Graphics smethod_14(Image image_0)
	{
		return Graphics.FromImage(image_0);
	}

	// Token: 0x060003A4 RID: 932 RVA: 0x0000239C File Offset: 0x0000059C
	static void smethod_15(Graphics graphics_0, SmoothingMode smoothingMode_0)
	{
		graphics_0.SmoothingMode = smoothingMode_0;
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x00002463 File Offset: 0x00000663
	static void smethod_16(Graphics graphics_0, PixelOffsetMode pixelOffsetMode_0)
	{
		graphics_0.PixelOffsetMode = pixelOffsetMode_0;
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x000023A5 File Offset: 0x000005A5
	static void smethod_17(Graphics graphics_0, TextRenderingHint textRenderingHint_0)
	{
		graphics_0.TextRenderingHint = textRenderingHint_0;
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x000023AE File Offset: 0x000005AE
	static Color smethod_18(Control control_0)
	{
		return control_0.BackColor;
	}

	// Token: 0x060003A8 RID: 936 RVA: 0x000023B6 File Offset: 0x000005B6
	static void smethod_19(Graphics graphics_0, Color color_8)
	{
		graphics_0.Clear(color_8);
	}

	// Token: 0x060003A9 RID: 937 RVA: 0x000023BF File Offset: 0x000005BF
	static SolidBrush smethod_20(Color color_8)
	{
		return new SolidBrush(color_8);
	}

	// Token: 0x060003AA RID: 938 RVA: 0x000023C7 File Offset: 0x000005C7
	static void smethod_21(Graphics graphics_0, Brush brush_0, Rectangle rectangle_0)
	{
		graphics_0.FillRectangle(brush_0, rectangle_0);
	}

	// Token: 0x040000B7 RID: 183
	private int int_0;

	// Token: 0x040000B8 RID: 184
	private int int_1;

	// Token: 0x040000B9 RID: 185
	private bool bool_0;

	// Token: 0x040000BA RID: 186
	private bool bool_1;

	// Token: 0x040000BB RID: 187
	private Point point_0 = new Point(0, 0);

	// Token: 0x040000BC RID: 188
	private int int_2 = 50;

	// Token: 0x040000BD RID: 189
	private Color color_0 = Color.FromArgb(45, 47, 49);

	// Token: 0x040000BE RID: 190
	private Color color_1 = Color.FromArgb(60, 70, 73);

	// Token: 0x040000BF RID: 191
	private Color color_2 = Color.FromArgb(53, 58, 60);

	// Token: 0x040000C0 RID: 192
	private Color color_3 = GClass6.color_0;

	// Token: 0x040000C1 RID: 193
	private Color color_4 = Color.FromArgb(234, 234, 234);

	// Token: 0x040000C2 RID: 194
	private Color color_5 = Color.FromArgb(171, 171, 172);

	// Token: 0x040000C3 RID: 195
	private Color color_6 = Color.FromArgb(196, 199, 200);

	// Token: 0x040000C4 RID: 196
	public Color color_7 = Color.FromArgb(45, 47, 49);
}
