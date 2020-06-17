using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

// Token: 0x0200000D RID: 13
public class GClass2 : ContextMenuStrip
{
	// Token: 0x06000134 RID: 308 RVA: 0x00002914 File Offset: 0x00000B14
	protected virtual void OnTextChanged(EventArgs e)
	{
		base.OnTextChanged(e);
		GClass2.smethod_0(this);
	}

	// Token: 0x06000135 RID: 309 RVA: 0x00002923 File Offset: 0x00000B23
	public GClass2()
	{
		GClass2.smethod_2(this, GClass2.smethod_1(new GClass2.GClass3()));
		GClass2.smethod_3(this, false);
		GClass2.smethod_4(this, Color.White);
		GClass2.smethod_6(this, GClass2.smethod_5("Segoe UI", 8f));
	}

	// Token: 0x06000136 RID: 310 RVA: 0x00002962 File Offset: 0x00000B62
	protected virtual void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);
		GClass2.smethod_8(GClass2.smethod_7(e), TextRenderingHint.ClearTypeGridFit);
	}

	// Token: 0x06000137 RID: 311 RVA: 0x0000232F File Offset: 0x0000052F
	static void smethod_0(Control control_0)
	{
		control_0.Invalidate();
	}

	// Token: 0x06000138 RID: 312 RVA: 0x00002977 File Offset: 0x00000B77
	static ToolStripProfessionalRenderer smethod_1(ProfessionalColorTable professionalColorTable_0)
	{
		return new ToolStripProfessionalRenderer(professionalColorTable_0);
	}

	// Token: 0x06000139 RID: 313 RVA: 0x0000297F File Offset: 0x00000B7F
	static void smethod_2(ToolStrip toolStrip_0, ToolStripRenderer toolStripRenderer_0)
	{
		toolStrip_0.Renderer = toolStripRenderer_0;
	}

	// Token: 0x0600013A RID: 314 RVA: 0x00002988 File Offset: 0x00000B88
	static void smethod_3(ToolStripDropDownMenu toolStripDropDownMenu_0, bool bool_0)
	{
		toolStripDropDownMenu_0.ShowImageMargin = bool_0;
	}

	// Token: 0x0600013B RID: 315 RVA: 0x00002991 File Offset: 0x00000B91
	static void smethod_4(ToolStrip toolStrip_0, Color color_0)
	{
		toolStrip_0.ForeColor = color_0;
	}

	// Token: 0x0600013C RID: 316 RVA: 0x000025D1 File Offset: 0x000007D1
	static Font smethod_5(string string_0, float float_0)
	{
		return new Font(string_0, float_0);
	}

	// Token: 0x0600013D RID: 317 RVA: 0x000025DA File Offset: 0x000007DA
	static void smethod_6(Control control_0, Font font_0)
	{
		control_0.Font = font_0;
	}

	// Token: 0x0600013E RID: 318 RVA: 0x0000249D File Offset: 0x0000069D
	static Graphics smethod_7(PaintEventArgs paintEventArgs_0)
	{
		return paintEventArgs_0.Graphics;
	}

	// Token: 0x0600013F RID: 319 RVA: 0x000023A5 File Offset: 0x000005A5
	static void smethod_8(Graphics graphics_0, TextRenderingHint textRenderingHint_0)
	{
		graphics_0.TextRenderingHint = textRenderingHint_0;
	}

	// Token: 0x0200000E RID: 14
	public class GClass3 : ProfessionalColorTable
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000140 RID: 320 RVA: 0x0000299A File Offset: 0x00000B9A
		// (set) Token: 0x06000141 RID: 321 RVA: 0x000029A2 File Offset: 0x00000BA2
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

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000142 RID: 322 RVA: 0x000029AB File Offset: 0x00000BAB
		// (set) Token: 0x06000143 RID: 323 RVA: 0x000029B3 File Offset: 0x00000BB3
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

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000144 RID: 324 RVA: 0x000029BC File Offset: 0x00000BBC
		// (set) Token: 0x06000145 RID: 325 RVA: 0x000029C4 File Offset: 0x00000BC4
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

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000299A File Offset: 0x00000B9A
		public virtual Color ButtonSelectedBorder
		{
			get
			{
				return this.color_0;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000029AB File Offset: 0x00000BAB
		public virtual Color CheckBackground
		{
			get
			{
				return this.color_1;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000029AB File Offset: 0x00000BAB
		public virtual Color CheckPressedBackground
		{
			get
			{
				return this.color_1;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000149 RID: 329 RVA: 0x000029AB File Offset: 0x00000BAB
		public virtual Color CheckSelectedBackground
		{
			get
			{
				return this.color_1;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000029AB File Offset: 0x00000BAB
		public virtual Color ImageMarginGradientBegin
		{
			get
			{
				return this.color_1;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600014B RID: 331 RVA: 0x000029AB File Offset: 0x00000BAB
		public virtual Color ImageMarginGradientEnd
		{
			get
			{
				return this.color_1;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600014C RID: 332 RVA: 0x000029AB File Offset: 0x00000BAB
		public virtual Color ImageMarginGradientMiddle
		{
			get
			{
				return this.color_1;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600014D RID: 333 RVA: 0x000029BC File Offset: 0x00000BBC
		public virtual Color MenuBorder
		{
			get
			{
				return this.color_2;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600014E RID: 334 RVA: 0x000029BC File Offset: 0x00000BBC
		public virtual Color MenuItemBorder
		{
			get
			{
				return this.color_2;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600014F RID: 335 RVA: 0x000029AB File Offset: 0x00000BAB
		public virtual Color MenuItemSelected
		{
			get
			{
				return this.color_1;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000150 RID: 336 RVA: 0x000029BC File Offset: 0x00000BBC
		public virtual Color SeparatorDark
		{
			get
			{
				return this.color_2;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000299A File Offset: 0x00000B9A
		public virtual Color ToolStripDropDownBackground
		{
			get
			{
				return this.color_0;
			}
		}

		// Token: 0x0400003D RID: 61
		private Color color_0 = Color.FromArgb(45, 47, 49);

		// Token: 0x0400003E RID: 62
		private Color color_1 = GClass6.color_0;

		// Token: 0x0400003F RID: 63
		private Color color_2 = Color.FromArgb(53, 58, 60);
	}
}
