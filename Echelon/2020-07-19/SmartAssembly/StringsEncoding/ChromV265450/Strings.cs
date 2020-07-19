using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using SmartAssembly.Zip;

namespace SmartAssembly.StringsEncoding
{
	// Token: 0x02000077 RID: 119
	public sealed class Strings
	{
		// Token: 0x06000283 RID: 643 RVA: 0x000156AC File Offset: 0x000138AC
		public static string Get(int A_0)
		{
			A_0 ^= 107396847;
			A_0 -= ChromV265450.Strings.offset;
			if (!ChromV265450.Strings.cacheStrings)
			{
				return ChromV265450.Strings.GetFromResource(A_0);
			}
			return ChromV265450.Strings.GetCachedOrResource(A_0);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000156D8 File Offset: 0x000138D8
		public static string GetCachedOrResource(int A_0)
		{
			object obj = ChromV265450.Strings.hashtableLock;
			lock (obj)
			{
				string text;
				ChromV265450.Strings.hashtable.TryGetValue(A_0, out text);
				if (text != null)
				{
					return text;
				}
			}
			return ChromV265450.Strings.GetFromResource(A_0);
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00015730 File Offset: 0x00013930
		public static string GetFromResource(int A_0)
		{
			byte[] array = ChromV265450.Strings.bytes;
			int index = A_0 + 1;
			int num = array[A_0];
			int num2;
			if ((num & 128) == 0)
			{
				num2 = num;
				if (num2 == 0)
				{
					return string.Empty;
				}
			}
			else if ((num & 64) == 0)
			{
				num2 = ((num & 63) << 8) + (int)ChromV265450.Strings.bytes[index++];
			}
			else
			{
				num2 = ((num & 31) << 24) + ((int)ChromV265450.Strings.bytes[index++] << 16) + ((int)ChromV265450.Strings.bytes[index++] << 8) + (int)ChromV265450.Strings.bytes[index++];
			}
			string result;
			try
			{
				byte[] array2 = Convert.FromBase64String(Encoding.UTF8.GetString(ChromV265450.Strings.bytes, index, num2));
				string text = string.Intern(Encoding.UTF8.GetString(array2, 0, array2.Length));
				if (ChromV265450.Strings.cacheStrings)
				{
					ChromV265450.Strings.CacheString(A_0, text);
				}
				result = text;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0001581C File Offset: 0x00013A1C
		public static void CacheString(int A_0, string A_1)
		{
			try
			{
				object obj = ChromV265450.Strings.hashtableLock;
				lock (obj)
				{
					ChromV265450.Strings.hashtable.Add(A_0, A_1);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000287 RID: 647 RVA: 0x00015874 File Offset: 0x00013A74
		static Strings()
		{
			if (ChromV265450.Strings.MustUseCache == "1")
			{
				ChromV265450.Strings.cacheStrings = true;
				ChromV265450.Strings.hashtable = new Dictionary<int, string>();
			}
			ChromV265450.Strings.offset = Convert.ToInt32(ChromV265450.Strings.OffsetValue);
			using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("{1084b41b-54c8-4d89-99d4-af1358dc4ba0}"))
			{
				int num = Convert.ToInt32(manifestResourceStream.Length);
				byte[] array = new byte[num];
				manifestResourceStream.Read(array, 0, num);
				ChromV265450.Strings.bytes = ChromV265450.SimpleZip.Unzip(array);
			}
		}

		// Token: 0x0400012A RID: 298
		private static readonly string MustUseCache = "0";

		// Token: 0x0400012B RID: 299
		private static readonly string OffsetValue = "242";

		// Token: 0x0400012C RID: 300
		private static readonly byte[] bytes = null;

		// Token: 0x0400012D RID: 301
		private static readonly Dictionary<int, string> hashtable;

		// Token: 0x0400012E RID: 302
		private static readonly object hashtableLock = new object();

		// Token: 0x0400012F RID: 303
		private static readonly bool cacheStrings = false;

		// Token: 0x04000130 RID: 304
		private static readonly int offset = 0;
	}
}
