using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

// Token: 0x02000025 RID: 37
public static class GClass6
{
	// Token: 0x060003AB RID: 939 RVA: 0x00009E34 File Offset: 0x00008034
	public static GraphicsPath smethod_0(Rectangle rectangle_0, int int_0)
	{
		GraphicsPath graphicsPath = GClass6.smethod_4();
		int num = int_0 * 2;
		graphicsPath.AddArc(new Rectangle(rectangle_0.X, rectangle_0.Y, num, num), -180f, 90f);
		graphicsPath.AddArc(new Rectangle(rectangle_0.Width - num + rectangle_0.X, rectangle_0.Y, num, num), -90f, 90f);
		graphicsPath.AddArc(new Rectangle(rectangle_0.Width - num + rectangle_0.X, rectangle_0.Height - num + rectangle_0.Y, num, num), 0f, 90f);
		graphicsPath.AddArc(new Rectangle(rectangle_0.X, rectangle_0.Height - num + rectangle_0.Y, num, num), 90f, 90f);
		graphicsPath.AddLine(new Point(rectangle_0.X, rectangle_0.Height - num + rectangle_0.Y), new Point(rectangle_0.X, int_0 + rectangle_0.Y));
		return graphicsPath;
	}

	// Token: 0x060003AC RID: 940 RVA: 0x00009F40 File Offset: 0x00008140
	public static GraphicsPath smethod_1(float float_0, float float_1, float float_2, float float_3, double double_0 = 0.3, bool bool_0 = true, bool bool_1 = true, bool bool_2 = true, bool bool_3 = true)
	{
		float num = GClass6.smethod_5(float_2, float_3) * (float)double_0;
		float num2 = float_0 + float_2;
		float num3 = float_1 + float_3;
		GraphicsPath graphicsPath_;
		GraphicsPath result = graphicsPath_ = GClass6.smethod_4();
		if (bool_0)
		{
			GClass6.smethod_6(graphicsPath_, float_0, float_1, num, num, 180f, 90f);
		}
		else
		{
			GClass6.smethod_7(graphicsPath_, float_0, float_1, float_0, float_1);
		}
		if (bool_1)
		{
			GClass6.smethod_6(graphicsPath_, num2 - num, float_1, num, num, 270f, 90f);
		}
		else
		{
			GClass6.smethod_7(graphicsPath_, num2, float_1, num2, float_1);
		}
		if (bool_2)
		{
			GClass6.smethod_6(graphicsPath_, num2 - num, num3 - num, num, num, 0f, 90f);
		}
		else
		{
			GClass6.smethod_7(graphicsPath_, num2, num3, num2, num3);
		}
		if (bool_3)
		{
			GClass6.smethod_6(graphicsPath_, float_0, num3 - num, num, num, 90f, 90f);
		}
		else
		{
			GClass6.smethod_7(graphicsPath_, float_0, num3, float_0, num3);
		}
		GClass6.smethod_8(graphicsPath_);
		return result;
	}

	// Token: 0x060003AD RID: 941 RVA: 0x0000A008 File Offset: 0x00008208
	public static GraphicsPath smethod_2(int int_0, int int_1, bool bool_0)
	{
		GraphicsPath graphicsPath = GClass6.smethod_4();
		int num = 12;
		int num2 = 6;
		if (!bool_0)
		{
			GClass6.smethod_9(graphicsPath, int_0, int_1 + num2, int_0 + num, int_1 + num2);
			GClass6.smethod_9(graphicsPath, int_0 + num, int_1 + num2, int_0 + num2, int_1);
		}
		else
		{
			GClass6.smethod_9(graphicsPath, int_0 + 1, int_1, int_0 + num + 1, int_1);
			GClass6.smethod_9(graphicsPath, int_0 + num, int_1, int_0 + num2, int_1 + num2 - 1);
		}
		GClass6.smethod_8(graphicsPath);
		return graphicsPath;
	}

	// Token: 0x060003AE RID: 942 RVA: 0x0000A070 File Offset: 0x00008270
	public static GClass0 smethod_3(Control control_0)
	{
		if (control_0 == null)
		{
			throw GClass6.smethod_10();
		}
		GClass0 gclass = new GClass0();
		while (control_0 != null && GClass6.smethod_14(GClass6.smethod_12(control_0), GClass6.smethod_13(typeof(GControl18).TypeHandle)))
		{
			control_0 = GClass6.smethod_11(control_0);
		}
		if (control_0 != null)
		{
			GControl18 gcontrol = (GControl18)control_0;
			gclass.color_0 = gcontrol.Color_3;
		}
		return gclass;
	}

	// Token: 0x060003AF RID: 943 RVA: 0x0000A0C8 File Offset: 0x000082C8
	// Note: this type is marked as 'beforefieldinit'.
	static GClass6()
	{
		StringFormat stringFormat_ = GClass6.smethod_15();
		GClass6.smethod_16(stringFormat_, StringAlignment.Near);
		GClass6.smethod_17(stringFormat_, StringAlignment.Near);
		GClass6.stringFormat_0 = stringFormat_;
		StringFormat stringFormat_2 = GClass6.smethod_15();
		GClass6.smethod_16(stringFormat_2, StringAlignment.Center);
		GClass6.smethod_17(stringFormat_2, StringAlignment.Center);
		GClass6.stringFormat_1 = stringFormat_2;
	}

	// Token: 0x060003B0 RID: 944 RVA: 0x0000245C File Offset: 0x0000065C
	static GraphicsPath smethod_4()
	{
		return new GraphicsPath();
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x00003673 File Offset: 0x00001873
	static float smethod_5(float float_0, float float_1)
	{
		return Math.Min(float_0, float_1);
	}

	// Token: 0x060003B2 RID: 946 RVA: 0x0000367C File Offset: 0x0000187C
	static void smethod_6(GraphicsPath graphicsPath_0, float float_0, float float_1, float float_2, float float_3, float float_4, float float_5)
	{
		graphicsPath_0.AddArc(float_0, float_1, float_2, float_3, float_4, float_5);
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x0000368D File Offset: 0x0000188D
	static void smethod_7(GraphicsPath graphicsPath_0, float float_0, float float_1, float float_2, float float_3)
	{
		graphicsPath_0.AddLine(float_0, float_1, float_2, float_3);
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x0000369A File Offset: 0x0000189A
	static void smethod_8(GraphicsPath graphicsPath_0)
	{
		graphicsPath_0.CloseFigure();
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x000036A2 File Offset: 0x000018A2
	static void smethod_9(GraphicsPath graphicsPath_0, int int_0, int int_1, int int_2, int int_3)
	{
		graphicsPath_0.AddLine(int_0, int_1, int_2, int_3);
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x000036AF File Offset: 0x000018AF
	static ArgumentNullException smethod_10()
	{
		return new ArgumentNullException();
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x000036B6 File Offset: 0x000018B6
	static Control smethod_11(Control control_0)
	{
		return control_0.Parent;
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x000036BE File Offset: 0x000018BE
	static Type smethod_12(object object_0)
	{
		return object_0.GetType();
	}

	// Token: 0x060003B9 RID: 953 RVA: 0x00002068 File Offset: 0x00000268
	static Type smethod_13(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	// Token: 0x060003BA RID: 954 RVA: 0x000036C6 File Offset: 0x000018C6
	static bool smethod_14(Type type_0, Type type_1)
	{
		return type_0 != type_1;
	}

	// Token: 0x060003BB RID: 955 RVA: 0x000036CF File Offset: 0x000018CF
	static StringFormat smethod_15()
	{
		return new StringFormat();
	}

	// Token: 0x060003BC RID: 956 RVA: 0x000036D6 File Offset: 0x000018D6
	static void smethod_16(StringFormat stringFormat_2, StringAlignment stringAlignment_0)
	{
		stringFormat_2.Alignment = stringAlignment_0;
	}

	// Token: 0x060003BD RID: 957 RVA: 0x000036DF File Offset: 0x000018DF
	static void smethod_17(StringFormat stringFormat_2, StringAlignment stringAlignment_0)
	{
		stringFormat_2.LineAlignment = stringAlignment_0;
	}

	// Token: 0x040000C5 RID: 197
	public static Color color_0 = Color.FromArgb(35, 168, 109);

	// Token: 0x040000C6 RID: 198
	public static readonly StringFormat stringFormat_0;

	// Token: 0x040000C7 RID: 199
	public static readonly StringFormat stringFormat_1;
}
