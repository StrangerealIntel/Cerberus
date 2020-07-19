using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using SmartAssembly.Zip;

namespace SmartAssembly.StringsEncoding
{
	// Token: 0x0200006A RID: 106
	public sealed class Strings
	{
		// Token: 0x06000248 RID: 584 RVA: 0x00012C38 File Offset: 0x00010E38
		public static string Get(int A_0)
		{
			A_0 ^= 107396847;
			A_0 -= Strings.offset;
			if (!Strings.cacheStrings)
			{
				return Strings.GetFromResource(A_0);
			}
			return Strings.GetCachedOrResource(A_0);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00012C64 File Offset: 0x00010E64
		public static string GetCachedOrResource(int A_0)
		{
			object obj = Strings.hashtableLock;
			lock (obj)
			{
				string text;
				Strings.hashtable.TryGetValue(A_0, out text);
				if (text != null)
				{
					return text;
				}
			}
			return Strings.GetFromResource(A_0);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00012CBC File Offset: 0x00010EBC
		public static string GetFromResource(int A_0)
		{
			byte[] array = Strings.bytes;
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
				num2 = ((num & 63) << 8) + (int)Strings.bytes[index++];
			}
			else
			{
				num2 = ((num & 31) << 24) + ((int)Strings.bytes[index++] << 16) + ((int)Strings.bytes[index++] << 8) + (int)Strings.bytes[index++];
			}
			string result;
			try
			{
				byte[] array2 = Convert.FromBase64String(Encoding.UTF8.GetString(Strings.bytes, index, num2));
				string text = string.Intern(Encoding.UTF8.GetString(array2, 0, array2.Length));
				if (Strings.cacheStrings)
				{
					Strings.CacheString(A_0, text);
				}
				result = text;
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00012DA8 File Offset: 0x00010FA8
		public static void CacheString(int A_0, string A_1)
		{
			try
			{
				object obj = Strings.hashtableLock;
				lock (obj)
				{
					Strings.hashtable.Add(A_0, A_1);
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00012E00 File Offset: 0x00011000
		static Strings()
		{
			if (Strings.MustUseCache == "1")
			{
				Strings.cacheStrings = true;
				Strings.hashtable = new Dictionary<int, string>();
			}
			Strings.offset = Convert.ToInt32(Strings.OffsetValue);
			using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("{e4775d96-dfa1-43fa-97d8-8f29405b74d4}"))
			{
				int num = Convert.ToInt32(manifestResourceStream.Length);
				byte[] array = new byte[num];
				manifestResourceStream.Read(array, 0, num);
				Strings.bytes = SimpleZip.Unzip(array);
			}
		}

		// Token: 0x040000FA RID: 250
		private static readonly string MustUseCache = "0";

		// Token: 0x040000FB RID: 251
		private static readonly string OffsetValue = "240";

		// Token: 0x040000FC RID: 252
		private static readonly byte[] bytes = null;

		// Token: 0x040000FD RID: 253
		private static readonly Dictionary<int, string> hashtable;

		// Token: 0x040000FE RID: 254
		private static readonly object hashtableLock = new object();

		// Token: 0x040000FF RID: 255
		private static readonly bool cacheStrings = false;

		// Token: 0x04000100 RID: 256
		private static readonly int offset = 0;
	}
}
