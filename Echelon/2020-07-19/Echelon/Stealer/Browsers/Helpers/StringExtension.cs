using System;
using System.Globalization;
using System.Text;

namespace Echelon.Stealer.Browsers.Helpers
{
	// Token: 0x0200002C RID: 44
	public static class StringExtension
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x000088EC File Offset: 0x00006AEC
		public static T ForceTo<T>(this object @this)
		{
			return (T)((object)Convert.ChangeType(@this, typeof(T)));
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00008904 File Offset: 0x00006B04
		public static string Remove(this string input, string strToRemove)
		{
			if (input.IsNullOrEmpty())
			{
				return null;
			}
			return input.Replace(strToRemove, "");
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00008920 File Offset: 0x00006B20
		public static string Left(this string input, int minusRight = 1)
		{
			if (input.IsNullOrEmpty() || input.Length <= minusRight)
			{
				return null;
			}
			return input.Substring(0, input.Length - minusRight);
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x0000894C File Offset: 0x00006B4C
		public static CultureInfo ToCultureInfo(this string culture, CultureInfo defaultCulture)
		{
			if (!culture.IsNullOrEmpty())
			{
				return defaultCulture;
			}
			return new CultureInfo(culture);
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00008964 File Offset: 0x00006B64
		public static string ToCamelCasing(this string value)
		{
			if (!string.IsNullOrEmpty(value))
			{
				return value.Substring(0, 1).ToUpper() + value.Substring(1, value.Length - 1);
			}
			return value;
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000089A4 File Offset: 0x00006BA4
		public static double? ToDouble(this string value, string culture = "en-US")
		{
			double? result;
			try
			{
				result = new double?(double.Parse(value, new CultureInfo(culture)));
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000089EC File Offset: 0x00006BEC
		public static bool? ToBoolean(this string value)
		{
			bool value2 = false;
			if (bool.TryParse(value, out value2))
			{
				return new bool?(value2);
			}
			return null;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00008A1C File Offset: 0x00006C1C
		public static int? ToInt32(this string value)
		{
			int value2 = 0;
			if (int.TryParse(value, out value2))
			{
				return new int?(value2);
			}
			return null;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00008A4C File Offset: 0x00006C4C
		public static long? ToInt64(this string value)
		{
			long value2 = 0L;
			if (long.TryParse(value, out value2))
			{
				return new long?(value2);
			}
			return null;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00008A80 File Offset: 0x00006C80
		public static string AddQueyString(this string url, string queryStringKey, string queryStringValue)
		{
			string text = (url.Split(new char[]
			{
				'?'
			}).Length <= 1) ? "?" : "&";
			return string.Concat(new string[]
			{
				url,
				text,
				queryStringKey,
				"=",
				queryStringValue
			});
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00008AE4 File Offset: 0x00006CE4
		public static string FormatFirstLetterUpperCase(this string value, string culture = "en-US")
		{
			return CultureInfo.GetCultureInfo(culture).TextInfo.ToTitleCase(value);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00008AF8 File Offset: 0x00006CF8
		public static string FillLeftWithZeros(this string value, int decimalDigits)
		{
			if (!string.IsNullOrEmpty(value))
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(value);
				string[] array = value.Split(new char[]
				{
					','
				});
				for (int i = array[array.Length - 1].Length; i < decimalDigits; i++)
				{
					stringBuilder.Append("0");
				}
				value = stringBuilder.ToString();
			}
			return value;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00008B64 File Offset: 0x00006D64
		public static string FormatWithDecimalDigits(this string value, bool removeCurrencySymbol, bool returnZero, int? decimalDigits)
		{
			if (value.IsNullOrEmpty())
			{
				return value;
			}
			if (!value.IndexOf(",").Equals(-1))
			{
				string[] array = value.Split(new char[]
				{
					','
				});
				if (array.Length.Equals(2) && array[1].Length > 0)
				{
					value = array[0] + "," + array[1].Substring(0, (array[1].Length >= decimalDigits.Value) ? decimalDigits.Value : array[1].Length);
				}
			}
			if (decimalDigits == null)
			{
				return value;
			}
			return value.FillLeftWithZeros(decimalDigits.Value);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00008C3C File Offset: 0x00006E3C
		public static string FormatWithoutDecimalDigits(this string value, bool removeCurrencySymbol, bool returnZero, int? decimalDigits, CultureInfo culture)
		{
			if (removeCurrencySymbol)
			{
				value = value.Remove(culture.NumberFormat.CurrencySymbol).Trim();
			}
			return value;
		}
	}
}
