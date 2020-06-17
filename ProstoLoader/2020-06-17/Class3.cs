using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

// Token: 0x0200002C RID: 44
[CompilerGenerated]
internal static class Class3
{
	// Token: 0x0600048E RID: 1166 RVA: 0x00003F0C File Offset: 0x0000210C
	private static string smethod_0(CultureInfo cultureInfo_0)
	{
		if (cultureInfo_0 == null)
		{
			return "";
		}
		return Class3.smethod_9(cultureInfo_0);
	}

	// Token: 0x0600048F RID: 1167 RVA: 0x000107AC File Offset: 0x0000E9AC
	private static Assembly smethod_1(AssemblyName assemblyName_0)
	{
		foreach (Assembly assembly in Class3.smethod_11(Class3.smethod_10()))
		{
			AssemblyName assemblyName_ = Class3.smethod_12(assembly);
			if (Class3.smethod_14(Class3.smethod_13(assemblyName_), Class3.smethod_13(assemblyName_0), StringComparison.InvariantCultureIgnoreCase) && Class3.smethod_14(Class3.smethod_0(Class3.smethod_15(assemblyName_)), Class3.smethod_0(Class3.smethod_15(assemblyName_0)), StringComparison.InvariantCultureIgnoreCase))
			{
				return assembly;
			}
		}
		return null;
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x00010814 File Offset: 0x0000EA14
	private static void smethod_2(Stream stream_0, Stream stream_1)
	{
		byte[] array = new byte[81920];
		int int_;
		while ((int_ = Class3.smethod_17(stream_0, array, 0, array.Length)) != 0)
		{
			Class3.smethod_16(stream_1, array, 0, int_);
		}
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x00010848 File Offset: 0x0000EA48
	private static Stream smethod_3(string string_0)
	{
		Assembly assembly_ = Class3.smethod_18();
		if (Class3.smethod_19(string_0, ".compressed"))
		{
			Stream stream = Class3.smethod_20(assembly_, string_0);
			Stream result;
			try
			{
				DeflateStream deflateStream = Class3.smethod_21(stream, CompressionMode.Decompress);
				try
				{
					MemoryStream memoryStream = Class3.smethod_22();
					Class3.smethod_2(deflateStream, memoryStream);
					Class3.smethod_23(memoryStream, 0L);
					result = memoryStream;
				}
				finally
				{
					if (deflateStream != null)
					{
						Class3.smethod_24(deflateStream);
					}
				}
			}
			finally
			{
				if (stream != null)
				{
					Class3.smethod_24(stream);
				}
			}
			return result;
		}
		return Class3.smethod_20(assembly_, string_0);
	}

	// Token: 0x06000492 RID: 1170 RVA: 0x000108D4 File Offset: 0x0000EAD4
	private static Stream smethod_4(Dictionary<string, string> dictionary_3, string string_0)
	{
		string string_;
		if (dictionary_3.TryGetValue(string_0, out string_))
		{
			return Class3.smethod_3(string_);
		}
		return null;
	}

	// Token: 0x06000493 RID: 1171 RVA: 0x000108F4 File Offset: 0x0000EAF4
	private static byte[] smethod_5(Stream stream_0)
	{
		byte[] array = new byte[Class3.smethod_25(stream_0)];
		Class3.smethod_17(stream_0, array, 0, array.Length);
		return array;
	}

	// Token: 0x06000494 RID: 1172 RVA: 0x0001091C File Offset: 0x0000EB1C
	private static Assembly smethod_6(Dictionary<string, string> dictionary_3, Dictionary<string, string> dictionary_4, AssemblyName assemblyName_0)
	{
		string text = Class3.smethod_26(Class3.smethod_13(assemblyName_0));
		if (Class3.smethod_15(assemblyName_0) != null && !Class3.smethod_27(Class3.smethod_9(Class3.smethod_15(assemblyName_0))))
		{
			text = Class3.smethod_28(Class3.smethod_9(Class3.smethod_15(assemblyName_0)), ".", text);
		}
		Stream stream = Class3.smethod_4(dictionary_3, text);
		byte[] byte_;
		try
		{
			if (stream == null)
			{
				return null;
			}
			byte_ = Class3.smethod_5(stream);
		}
		finally
		{
			if (stream != null)
			{
				Class3.smethod_24(stream);
			}
		}
		Stream stream2 = Class3.smethod_4(dictionary_4, text);
		try
		{
			if (stream2 != null)
			{
				byte[] byte_2 = Class3.smethod_5(stream2);
				return Class3.smethod_29(byte_, byte_2);
			}
		}
		finally
		{
			if (stream2 != null)
			{
				Class3.smethod_24(stream2);
			}
		}
		return Class3.smethod_30(byte_);
	}

	// Token: 0x06000495 RID: 1173 RVA: 0x000109DC File Offset: 0x0000EBDC
	public static Assembly smethod_7(object object_1, ResolveEventArgs resolveEventArgs_0)
	{
		object object_2 = Class3.object_0;
		bool flag = false;
		try
		{
			Class3.smethod_31(object_2, ref flag);
			if (Class3.dictionary_0.ContainsKey(Class3.smethod_32(resolveEventArgs_0)))
			{
				return null;
			}
		}
		finally
		{
			if (flag)
			{
				Class3.smethod_33(object_2);
			}
		}
		AssemblyName assemblyName_ = Class3.smethod_34(Class3.smethod_32(resolveEventArgs_0));
		Assembly assembly = Class3.smethod_1(assemblyName_);
		if (Class3.smethod_35(assembly, null))
		{
			return assembly;
		}
		assembly = Class3.smethod_6(Class3.dictionary_1, Class3.dictionary_2, assemblyName_);
		if (Class3.smethod_36(assembly, null))
		{
			object_2 = Class3.object_0;
			flag = false;
			try
			{
				Class3.smethod_31(object_2, ref flag);
				Class3.dictionary_0[Class3.smethod_32(resolveEventArgs_0)] = true;
			}
			finally
			{
				if (flag)
				{
					Class3.smethod_33(object_2);
				}
			}
			if ((Class3.smethod_37(assemblyName_) & AssemblyNameFlags.Retargetable) != AssemblyNameFlags.None)
			{
				assembly = Class3.smethod_38(assemblyName_);
			}
		}
		return assembly;
	}

	// Token: 0x06000496 RID: 1174 RVA: 0x00010AC0 File Offset: 0x0000ECC0
	// Note: this type is marked as 'beforefieldinit'.
	static Class3()
	{
		Class3.dictionary_1.Add("system.io.compression", "costura.system.io.compression.dll.compressed");
		Class3.dictionary_1.Add("system.io.compression.filesystem", "costura.system.io.compression.filesystem.dll.compressed");
		Class3.dictionary_1.Add("system.shim", "costura.system.shim.dll.compressed");
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x00010B34 File Offset: 0x0000ED34
	public static void smethod_8()
	{
		if (Class3.smethod_40(ref Class3.int_0, 1) != 1)
		{
			goto IL_32;
		}
		IL_0E:
		int num = 1677281082;
		IL_13:
		switch ((num ^ 128676545) % 4)
		{
		case 0:
			goto IL_0E;
		case 2:
			IL_32:
			Class3.smethod_41(Class3.smethod_10(), new ResolveEventHandler(Class3.smethod_7));
			num = 1308392332;
			goto IL_13;
		case 3:
			return;
		}
	}

	// Token: 0x06000498 RID: 1176 RVA: 0x00003F1D File Offset: 0x0000211D
	static string smethod_9(CultureInfo cultureInfo_0)
	{
		return cultureInfo_0.Name;
	}

	// Token: 0x06000499 RID: 1177 RVA: 0x00003F25 File Offset: 0x00002125
	static AppDomain smethod_10()
	{
		return AppDomain.CurrentDomain;
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x00003F2C File Offset: 0x0000212C
	static Assembly[] smethod_11(AppDomain appDomain_0)
	{
		return appDomain_0.GetAssemblies();
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x00003F34 File Offset: 0x00002134
	static AssemblyName smethod_12(Assembly assembly_0)
	{
		return assembly_0.GetName();
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x00003F3C File Offset: 0x0000213C
	static string smethod_13(AssemblyName assemblyName_0)
	{
		return assemblyName_0.Name;
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x00003F44 File Offset: 0x00002144
	static bool smethod_14(string string_0, string string_1, StringComparison stringComparison_0)
	{
		return string.Equals(string_0, string_1, stringComparison_0);
	}

	// Token: 0x0600049E RID: 1182 RVA: 0x00003F4E File Offset: 0x0000214E
	static CultureInfo smethod_15(AssemblyName assemblyName_0)
	{
		return assemblyName_0.CultureInfo;
	}

	// Token: 0x0600049F RID: 1183 RVA: 0x00003981 File Offset: 0x00001B81
	static void smethod_16(Stream stream_0, byte[] byte_0, int int_1, int int_2)
	{
		stream_0.Write(byte_0, int_1, int_2);
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x00003F56 File Offset: 0x00002156
	static int smethod_17(Stream stream_0, byte[] byte_0, int int_1, int int_2)
	{
		return stream_0.Read(byte_0, int_1, int_2);
	}

	// Token: 0x060004A1 RID: 1185 RVA: 0x00003F61 File Offset: 0x00002161
	static Assembly smethod_18()
	{
		return Assembly.GetExecutingAssembly();
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x00003F68 File Offset: 0x00002168
	static bool smethod_19(string string_0, string string_1)
	{
		return string_0.EndsWith(string_1);
	}

	// Token: 0x060004A3 RID: 1187 RVA: 0x00003F71 File Offset: 0x00002171
	static Stream smethod_20(Assembly assembly_0, string string_0)
	{
		return assembly_0.GetManifestResourceStream(string_0);
	}

	// Token: 0x060004A4 RID: 1188 RVA: 0x00003F7A File Offset: 0x0000217A
	static DeflateStream smethod_21(Stream stream_0, CompressionMode compressionMode_0)
	{
		return new DeflateStream(stream_0, compressionMode_0);
	}

	// Token: 0x060004A5 RID: 1189 RVA: 0x00003F83 File Offset: 0x00002183
	static MemoryStream smethod_22()
	{
		return new MemoryStream();
	}

	// Token: 0x060004A6 RID: 1190 RVA: 0x00003F8A File Offset: 0x0000218A
	static void smethod_23(Stream stream_0, long long_0)
	{
		stream_0.Position = long_0;
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x00002FE1 File Offset: 0x000011E1
	static void smethod_24(IDisposable idisposable_0)
	{
		idisposable_0.Dispose();
	}

	// Token: 0x060004A8 RID: 1192 RVA: 0x00003F93 File Offset: 0x00002193
	static long smethod_25(Stream stream_0)
	{
		return stream_0.Length;
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x00003F9B File Offset: 0x0000219B
	static string smethod_26(string string_0)
	{
		return string_0.ToLowerInvariant();
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x00003881 File Offset: 0x00001A81
	static bool smethod_27(string string_0)
	{
		return string.IsNullOrEmpty(string_0);
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x00003FA3 File Offset: 0x000021A3
	static string smethod_28(string string_0, string string_1, string string_2)
	{
		return string_0 + string_1 + string_2;
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x00003FAD File Offset: 0x000021AD
	static Assembly smethod_29(byte[] byte_0, byte[] byte_1)
	{
		return Assembly.Load(byte_0, byte_1);
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x00003FB6 File Offset: 0x000021B6
	static Assembly smethod_30(byte[] byte_0)
	{
		return Assembly.Load(byte_0);
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x00003FBE File Offset: 0x000021BE
	static void smethod_31(object object_1, ref bool bool_0)
	{
		Monitor.Enter(object_1, ref bool_0);
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x00003FC7 File Offset: 0x000021C7
	static string smethod_32(ResolveEventArgs resolveEventArgs_0)
	{
		return resolveEventArgs_0.Name;
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x00003FCF File Offset: 0x000021CF
	static void smethod_33(object object_1)
	{
		Monitor.Exit(object_1);
	}

	// Token: 0x060004B1 RID: 1201 RVA: 0x00003FD7 File Offset: 0x000021D7
	static AssemblyName smethod_34(string string_0)
	{
		return new AssemblyName(string_0);
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x00003FDF File Offset: 0x000021DF
	static bool smethod_35(Assembly assembly_0, Assembly assembly_1)
	{
		return assembly_0 != assembly_1;
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x00003FE8 File Offset: 0x000021E8
	static bool smethod_36(Assembly assembly_0, Assembly assembly_1)
	{
		return assembly_0 == assembly_1;
	}

	// Token: 0x060004B4 RID: 1204 RVA: 0x00003FF1 File Offset: 0x000021F1
	static AssemblyNameFlags smethod_37(AssemblyName assemblyName_0)
	{
		return assemblyName_0.Flags;
	}

	// Token: 0x060004B5 RID: 1205 RVA: 0x00003FF9 File Offset: 0x000021F9
	static Assembly smethod_38(AssemblyName assemblyName_0)
	{
		return Assembly.Load(assemblyName_0);
	}

	// Token: 0x060004B6 RID: 1206 RVA: 0x00004001 File Offset: 0x00002201
	static object smethod_39()
	{
		return new object();
	}

	// Token: 0x060004B7 RID: 1207 RVA: 0x00004008 File Offset: 0x00002208
	static int smethod_40(ref int int_1, int int_2)
	{
		return Interlocked.Exchange(ref int_1, int_2);
	}

	// Token: 0x060004B8 RID: 1208 RVA: 0x00004011 File Offset: 0x00002211
	static void smethod_41(AppDomain appDomain_0, ResolveEventHandler resolveEventHandler_0)
	{
		appDomain_0.AssemblyResolve += resolveEventHandler_0;
	}

	// Token: 0x0400012E RID: 302
	private static object object_0 = Class3.smethod_39();

	// Token: 0x0400012F RID: 303
	private static Dictionary<string, bool> dictionary_0 = new Dictionary<string, bool>();

	// Token: 0x04000130 RID: 304
	private static Dictionary<string, string> dictionary_1 = new Dictionary<string, string>();

	// Token: 0x04000131 RID: 305
	private static Dictionary<string, string> dictionary_2 = new Dictionary<string, string>();

	// Token: 0x04000132 RID: 306
	private static int int_0;
}
