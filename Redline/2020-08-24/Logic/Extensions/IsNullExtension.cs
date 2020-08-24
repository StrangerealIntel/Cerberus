using System;

namespace RedLine.Logic.Extensions
{
	// Token: 0x02000065 RID: 101
	public static class IsNullExtension
	{
		// Token: 0x060002B5 RID: 693 RVA: 0x0000C0F7 File Offset: 0x0000A2F7
		public static bool IsNotNull<T>(this T data)
		{
			return data != null;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x0000C102 File Offset: 0x0000A302
		public static string IsNull(this string value, string defaultValue)
		{
			if (!string.IsNullOrEmpty(value))
			{
				return value;
			}
			return defaultValue;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000C10F File Offset: 0x0000A30F
		public static bool IsNullOrEmpty(this string str)
		{
			return string.IsNullOrEmpty(str);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000C117 File Offset: 0x0000A317
		public static bool IsNull(this bool? value, bool def)
		{
			if (value != null)
			{
				return value.Value;
			}
			return def;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000C12B File Offset: 0x0000A32B
		public static T IsNull<T>(this T value) where T : class
		{
			if (value == null)
			{
				return Activator.CreateInstance<T>();
			}
			return value;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000C13C File Offset: 0x0000A33C
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

		// Token: 0x060002BB RID: 699 RVA: 0x0000C157 File Offset: 0x0000A357
		public static int IsNull(this int? value, int def)
		{
			if (value != null)
			{
				return value.Value;
			}
			return def;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0000C16B File Offset: 0x0000A36B
		public static long IsNull(this long? value, long def)
		{
			if (value != null)
			{
				return value.Value;
			}
			return def;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x0000C17F File Offset: 0x0000A37F
		public static double IsNull(this double? value, double def)
		{
			if (value != null)
			{
				return value.Value;
			}
			return def;
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0000C193 File Offset: 0x0000A393
		public static DateTime IsNull(this DateTime? value, DateTime def)
		{
			if (value != null)
			{
				return value.Value;
			}
			return def;
		}

		// Token: 0x060002BF RID: 703 RVA: 0x0000C1A7 File Offset: 0x0000A3A7
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
