using System;
using System.Drawing;
using System.Windows.Forms;

// Token: 0x02000010 RID: 16
public class GClass4 : Label
{
	// Token: 0x06000169 RID: 361 RVA: 0x00002A24 File Offset: 0x00000C24
	protected virtual void OnTextChanged(EventArgs e)
	{
		base.OnTextChanged(e);
		GClass4.smethod_0(this);
	}

	// Token: 0x0600016A RID: 362 RVA: 0x000065D0 File Offset: 0x000047D0
	public GClass4()
	{
		base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
		GClass4.smethod_2(this, GClass4.smethod_1("Segoe UI", 8f));
		GClass4.smethod_3(this, Color.White);
		GClass4.smethod_4(this, Color.Transparent);
		GClass4.smethod_6(this, GClass4.smethod_5(this));
	}

	// Token: 0x0600016B RID: 363 RVA: 0x0000232F File Offset: 0x0000052F
	static void smethod_0(Control control_0)
	{
		control_0.Invalidate();
	}

	// Token: 0x0600016C RID: 364 RVA: 0x000025D1 File Offset: 0x000007D1
	static Font smethod_1(string string_0, float float_0)
	{
		return new Font(string_0, float_0);
	}

	// Token: 0x0600016D RID: 365 RVA: 0x000025DA File Offset: 0x000007DA
	static void smethod_2(Control control_0, Font font_0)
	{
		control_0.Font = font_0;
	}

	// Token: 0x0600016E RID: 366 RVA: 0x000028AF File Offset: 0x00000AAF
	static void smethod_3(Control control_0, Color color_0)
	{
		control_0.ForeColor = color_0;
	}

	// Token: 0x0600016F RID: 367 RVA: 0x00002372 File Offset: 0x00000572
	static void smethod_4(Control control_0, Color color_0)
	{
		control_0.BackColor = color_0;
	}

	// Token: 0x06000170 RID: 368 RVA: 0x00002476 File Offset: 0x00000676
	static string smethod_5(Control control_0)
	{
		return control_0.Text;
	}

	// Token: 0x06000171 RID: 369 RVA: 0x00002340 File Offset: 0x00000540
	static void smethod_6(Control control_0, string string_0)
	{
		control_0.Text = string_0;
	}
}
