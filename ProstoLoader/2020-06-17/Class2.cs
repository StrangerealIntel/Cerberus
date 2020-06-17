using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;

// Token: 0x0200002A RID: 42
[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
internal class Class2
{
	// Token: 0x06000440 RID: 1088 RVA: 0x00002109 File Offset: 0x00000309
	internal Class2()
	{
	}

	// Token: 0x17000063 RID: 99
	// (get) Token: 0x06000441 RID: 1089 RVA: 0x00003A16 File Offset: 0x00001C16
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static ResourceManager ResourceManager_0
	{
		get
		{
			if (Class2.resourceManager_0 == null)
			{
				Class2.resourceManager_0 = Class2.smethod_2("\\*p}DU=o1z%wVn4xN:\\[=s4m_S\"", Class2.smethod_1(Class2.smethod_0(typeof(Class2).TypeHandle)));
			}
			return Class2.resourceManager_0;
		}
	}

	// Token: 0x17000064 RID: 100
	// (set) Token: 0x06000442 RID: 1090 RVA: 0x00003A42 File Offset: 0x00001C42
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static CultureInfo CultureInfo_0
	{
		set
		{
			Class2.cultureInfo_0 = value;
		}
	}

	// Token: 0x17000065 RID: 101
	// (get) Token: 0x06000443 RID: 1091 RVA: 0x00003A4A File Offset: 0x00001C4A
	internal static string String_0
	{
		get
		{
			return Class2.smethod_3(Class2.ResourceManager_0, "config", Class2.cultureInfo_0);
		}
	}

	// Token: 0x17000066 RID: 102
	// (get) Token: 0x06000444 RID: 1092 RVA: 0x00003A60 File Offset: 0x00001C60
	internal static byte[] Byte_0
	{
		get
		{
			return (byte[])Class2.smethod_4(Class2.ResourceManager_0, "ConfuserEx", Class2.cultureInfo_0);
		}
	}

	// Token: 0x17000067 RID: 103
	// (get) Token: 0x06000445 RID: 1093 RVA: 0x00003A7B File Offset: 0x00001C7B
	internal static Bitmap Bitmap_0
	{
		get
		{
			return (Bitmap)Class2.smethod_4(Class2.ResourceManager_0, "imageres_81", Class2.cultureInfo_0);
		}
	}

	// Token: 0x17000068 RID: 104
	// (get) Token: 0x06000446 RID: 1094 RVA: 0x00003A96 File Offset: 0x00001C96
	internal static Bitmap Bitmap_1
	{
		get
		{
			return (Bitmap)Class2.smethod_4(Class2.ResourceManager_0, "imageres_84", Class2.cultureInfo_0);
		}
	}

	// Token: 0x17000069 RID: 105
	// (get) Token: 0x06000447 RID: 1095 RVA: 0x00003AB1 File Offset: 0x00001CB1
	internal static Bitmap Bitmap_2
	{
		get
		{
			return (Bitmap)Class2.smethod_4(Class2.ResourceManager_0, "imageres_98", Class2.cultureInfo_0);
		}
	}

	// Token: 0x1700006A RID: 106
	// (get) Token: 0x06000448 RID: 1096 RVA: 0x00003ACC File Offset: 0x00001CCC
	internal static Bitmap Bitmap_3
	{
		get
		{
			return (Bitmap)Class2.smethod_4(Class2.ResourceManager_0, "imageres_981", Class2.cultureInfo_0);
		}
	}

	// Token: 0x1700006B RID: 107
	// (get) Token: 0x06000449 RID: 1097 RVA: 0x00003AE7 File Offset: 0x00001CE7
	internal static Bitmap Bitmap_4
	{
		get
		{
			return (Bitmap)Class2.smethod_4(Class2.ResourceManager_0, "imageres_99", Class2.cultureInfo_0);
		}
	}

	// Token: 0x1700006C RID: 108
	// (get) Token: 0x0600044A RID: 1098 RVA: 0x00003B02 File Offset: 0x00001D02
	internal static string String_1
	{
		get
		{
			return Class2.smethod_3(Class2.ResourceManager_0, "Program", Class2.cultureInfo_0);
		}
	}

	// Token: 0x0600044B RID: 1099 RVA: 0x00002068 File Offset: 0x00000268
	static Type smethod_0(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	// Token: 0x0600044C RID: 1100 RVA: 0x00003B18 File Offset: 0x00001D18
	static Assembly smethod_1(Type type_0)
	{
		return type_0.Assembly;
	}

	// Token: 0x0600044D RID: 1101 RVA: 0x00003B20 File Offset: 0x00001D20
	static ResourceManager smethod_2(string string_0, Assembly assembly_0)
	{
		return new ResourceManager(string_0, assembly_0);
	}

	// Token: 0x0600044E RID: 1102 RVA: 0x00003B29 File Offset: 0x00001D29
	static string smethod_3(ResourceManager resourceManager_1, string string_0, CultureInfo cultureInfo_1)
	{
		return resourceManager_1.GetString(string_0, cultureInfo_1);
	}

	// Token: 0x0600044F RID: 1103 RVA: 0x00003B33 File Offset: 0x00001D33
	static object smethod_4(ResourceManager resourceManager_1, string string_0, CultureInfo cultureInfo_1)
	{
		return resourceManager_1.GetObject(string_0, cultureInfo_1);
	}

	// Token: 0x0400012B RID: 299
	private static ResourceManager resourceManager_0;

	// Token: 0x0400012C RID: 300
	private static CultureInfo cultureInfo_0;
}
