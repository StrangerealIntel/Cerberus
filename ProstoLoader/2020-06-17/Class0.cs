using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

// Token: 0x02000002 RID: 2
internal class Class0
{
	// Token: 0x06000024 RID: 36 RVA: 0x00004AB0 File Offset: 0x00002CB0
	public static void smethod_0(string string_0)
	{
		string text = Class0.smethod_2(Class0.smethod_1(), "configconfuser.crproj");
		string text2 = Class2.String_0;
		string text3 = Class0.smethod_2(Class0.smethod_1(), "⁬⁮⁯‎‭‭⁮‪‌⁮‫‍⁬​‪‫‌‫⁬⁯⁮‪‌⁫⁭‎⁯‭⁯⁫​​⁪⁬‭​⁬⁪‮⁫‮");
		string string_ = Class0.smethod_5(Class0.smethod_4(Class0.smethod_3(string_0)));
		text2 = Class0.smethod_6(Class0.smethod_6(Class0.smethod_6(text2, "%path%", string_), "%basedir%", string_), "%stub%", string_0);
		Class0.smethod_7(text, text2);
		Class0.smethod_8(Class0.smethod_2(Class0.smethod_1(), "confuser.zip"), Class2.Byte_0);
		if (Class0.smethod_9(text3))
		{
			Class0.smethod_10(text3, true);
		}
		Class0.smethod_11(text3);
		Class0.smethod_12(Class0.smethod_2(Class0.smethod_1(), "confuser.zip"), text3);
		ProcessStartInfo processStartInfo_ = Class0.smethod_13();
		Class0.smethod_14(processStartInfo_, Class0.smethod_2(text3, "\\Confuser.CLI.exe"));
		Class0.smethod_15(processStartInfo_, true);
		Class0.smethod_16(processStartInfo_, ProcessWindowStyle.Hidden);
		Class0.smethod_17(processStartInfo_, Class0.smethod_2("-n ", text));
		Class0.smethod_19(Class0.smethod_18(processStartInfo_));
		Class0.smethod_20(Class0.smethod_2(Class0.smethod_1(), "confuser.zip"));
		Class0.smethod_20(Class0.smethod_2(Class0.smethod_1(), "configconfuser.crproj"));
		Class0.smethod_10(text3, true);
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00002111 File Offset: 0x00000311
	static string smethod_1()
	{
		return Path.GetTempPath();
	}

	// Token: 0x06000027 RID: 39 RVA: 0x0000207A File Offset: 0x0000027A
	static string smethod_2(string string_0, string string_1)
	{
		return string_0 + string_1;
	}

	// Token: 0x06000028 RID: 40 RVA: 0x00002118 File Offset: 0x00000318
	static FileInfo smethod_3(string string_0)
	{
		return new FileInfo(string_0);
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00002120 File Offset: 0x00000320
	static DirectoryInfo smethod_4(FileInfo fileInfo_0)
	{
		return fileInfo_0.Directory;
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00002128 File Offset: 0x00000328
	static string smethod_5(object object_0)
	{
		return object_0.ToString();
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00002130 File Offset: 0x00000330
	static string smethod_6(string string_0, string string_1, string string_2)
	{
		return string_0.Replace(string_1, string_2);
	}

	// Token: 0x0600002C RID: 44 RVA: 0x0000213A File Offset: 0x0000033A
	static void smethod_7(string string_0, string string_1)
	{
		File.WriteAllText(string_0, string_1);
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00002143 File Offset: 0x00000343
	static void smethod_8(string string_0, byte[] byte_0)
	{
		File.WriteAllBytes(string_0, byte_0);
	}

	// Token: 0x0600002E RID: 46 RVA: 0x0000214C File Offset: 0x0000034C
	static bool smethod_9(string string_0)
	{
		return Directory.Exists(string_0);
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00002154 File Offset: 0x00000354
	static void smethod_10(string string_0, bool bool_0)
	{
		Directory.Delete(string_0, bool_0);
	}

	// Token: 0x06000030 RID: 48 RVA: 0x0000215D File Offset: 0x0000035D
	static DirectoryInfo smethod_11(string string_0)
	{
		return Directory.CreateDirectory(string_0);
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00002165 File Offset: 0x00000365
	static void smethod_12(string string_0, string string_1)
	{
		ZipFile.ExtractToDirectory(string_0, string_1);
	}

	// Token: 0x06000032 RID: 50 RVA: 0x0000216E File Offset: 0x0000036E
	static ProcessStartInfo smethod_13()
	{
		return new ProcessStartInfo();
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00002175 File Offset: 0x00000375
	static void smethod_14(ProcessStartInfo processStartInfo_0, string string_0)
	{
		processStartInfo_0.FileName = string_0;
	}

	// Token: 0x06000034 RID: 52 RVA: 0x0000217E File Offset: 0x0000037E
	static void smethod_15(ProcessStartInfo processStartInfo_0, bool bool_0)
	{
		processStartInfo_0.UseShellExecute = bool_0;
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00002187 File Offset: 0x00000387
	static void smethod_16(ProcessStartInfo processStartInfo_0, ProcessWindowStyle processWindowStyle_0)
	{
		processStartInfo_0.WindowStyle = processWindowStyle_0;
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00002190 File Offset: 0x00000390
	static void smethod_17(ProcessStartInfo processStartInfo_0, string string_0)
	{
		processStartInfo_0.Arguments = string_0;
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00002199 File Offset: 0x00000399
	static Process smethod_18(ProcessStartInfo processStartInfo_0)
	{
		return Process.Start(processStartInfo_0);
	}

	// Token: 0x06000038 RID: 56 RVA: 0x000021A1 File Offset: 0x000003A1
	static void smethod_19(Process process_0)
	{
		process_0.WaitForExit();
	}

	// Token: 0x06000039 RID: 57 RVA: 0x000021A9 File Offset: 0x000003A9
	static void smethod_20(string string_0)
	{
		File.Delete(string_0);
	}
}
