using System;

namespace Echelon.Stealer.Browsers.Helpers
{
	// Token: 0x02000029 RID: 41
	public static class IsNullExtension
	{
		// Token: 0x06000090 RID: 144 RVA: 0x00007598 File Offset: 0x00005798
		public static bool IsNotNull<T>(this T data)
		{
			return data != null;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000075A4 File Offset: 0x000057A4
		public static string IsNull(this string value, string defaultValue)
		{
			if (!string.IsNullOrEmpty(value))
			{
				return value;
			}
			return defaultValue;
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000075B4 File Offset: 0x000057B4
		public static bool IsNullOrEmpty(this string str)
		{
			return string.IsNullOrEmpty(str);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000075BC File Offset: 0x000057BC
		public static bool IsNull(this bool? value, bool def)
		{
			if (value != null)
			{
				return value.Value;
			}
			return def;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000075D4 File Offset: 0x000057D4
		public static T IsNull<T>(this T value) where T : class
		{
			if (value == null)
			{
				return Activator.CreateInstance<T>();
			}
			return value;
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000075E8 File Offset: 0x000057E8
		public static T IsNull<T>(this T value, T def) where T : class
		{
			if (value != null)
			{
				return value;
			}
			if (def == null)
			{
				return Activator.CreateInstance<T>();
			}
			return def;
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000760C File Offset: 0x0000580C
		public static int IsNull(this int? value, int def)
		{
			if (value != null)
			{
				return value.Value;
			}
			return def;
		}

		// Token: 0x06000097 RID: 151 RVA: 0x00007624 File Offset: 0x00005824
		public static long IsNull(this long? value, long def)
		{
			if (value != null)
			{
				return value.Value;
			}
			return def;
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000763C File Offset: 0x0000583C
		public static double IsNull(this double? value, double def)
		{
			if (value != null)
			{
				return value.Value;
			}
			return def;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00007654 File Offset: 0x00005854
		public static DateTime IsNull(this DateTime? value, DateTime def)
		{
			if (value != null)
			{
				return value.Value;
			}
			return def;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000766C File Offset: 0x0000586C
		public static Guid IsNull(this Guid? value, Guid def)
		{
			if (value != null)
			{
				return value.Value;
			}
			return def;
		}
	}
}
