using System;
using System.Linq;
using System.Text;

namespace CG.Web.MegaApiClient
{
	// Token: 0x020000E7 RID: 231
	internal static class Extensions
	{
		// Token: 0x060007FC RID: 2044 RVA: 0x00039A74 File Offset: 0x00037C74
		public static string ToBase64(this byte[] data)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(Convert.ToBase64String(data));
			stringBuilder.Replace('+', '-');
			stringBuilder.Replace('/', '_');
			stringBuilder.Replace("=", string.Empty);
			return stringBuilder.ToString();
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00039AC4 File Offset: 0x00037CC4
		public static byte[] FromBase64(this string data)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(data);
			stringBuilder.Append(string.Empty.PadRight((4 - data.Length % 4) % 4, '='));
			stringBuilder.Replace('-', '+');
			stringBuilder.Replace('_', '/');
			stringBuilder.Replace(",", string.Empty);
			return Convert.FromBase64String(stringBuilder.ToString());
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00039B34 File Offset: 0x00037D34
		public static string ToUTF8String(this byte[] data)
		{
			return Encoding.UTF8.GetString(data);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00039B44 File Offset: 0x00037D44
		public static byte[] ToBytes(this string data)
		{
			return Encoding.UTF8.GetBytes(data);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00039B54 File Offset: 0x00037D54
		public static byte[] ToBytesPassword(this string data)
		{
			uint[] array = new uint[data.Length + 3 >> 2];
			for (int i = 0; i < data.Length; i++)
			{
				array[i >> 2] |= (uint)((uint)data[i] << (24 - (i & 3) * 8 & 31));
			}
			return array.SelectMany(delegate(uint x)
			{
				byte[] bytes = BitConverter.GetBytes(x);
				if (BitConverter.IsLittleEndian)
				{
					Array.Reverse(bytes);
				}
				return bytes;
			}).ToArray<byte>();
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00039BD8 File Offset: 0x00037DD8
		public static T[] CopySubArray<T>(this T[] source, int length, int offset = 0)
		{
			T[] array = new T[length];
			while (--length >= 0)
			{
				if (source.Length > offset + length)
				{
					array[length] = source[offset + length];
				}
			}
			return array;
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00039C1C File Offset: 0x00037E1C
		public static BigInteger FromMPINumber(this byte[] data)
		{
			byte[] array = new byte[((int)data[0] * 256 + (int)data[1] + 7) / 8];
			Array.Copy(data, 2, array, 0, array.Length);
			return new BigInteger(array, -1, 0);
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00039C58 File Offset: 0x00037E58
		public static DateTime ToDateTime(this long seconds)
		{
			return Extensions.EpochStart.AddSeconds((double)seconds).ToLocalTime();
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00039C80 File Offset: 0x00037E80
		public static long ToEpoch(this DateTime datetime)
		{
			return (long)datetime.ToUniversalTime().Subtract(Extensions.EpochStart).TotalSeconds;
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00039CB0 File Offset: 0x00037EB0
		public static long DeserializeToLong(this byte[] data, int index, int length)
		{
			byte b = data[index];
			long num = 0L;
			if (b > 8 || (int)b >= length)
			{
				throw new ArgumentException("Invalid value");
			}
			while (b > 0)
			{
				long num2 = num << 8;
				byte b2 = b;
				b = b2 - 1;
				num = num2 + (long)((ulong)data[index + (int)b2]);
			}
			return num;
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00039CF8 File Offset: 0x00037EF8
		public static byte[] SerializeToBytes(this long data)
		{
			byte[] array = new byte[9];
			byte b = 0;
			while (data != 0L)
			{
				array[(int)(b += 1)] = (byte)data;
				data >>= 8;
			}
			array[0] = b;
			Array.Resize<byte>(ref array, (int)(array[0] + 1));
			return array;
		}

		// Token: 0x040004E4 RID: 1252
		private static readonly DateTime EpochStart = new DateTime(1970, 1, 1, 0, 0, 0, 0);
	}
}
