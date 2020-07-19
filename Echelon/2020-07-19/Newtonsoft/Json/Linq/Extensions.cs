using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020001D2 RID: 466
	[NullableContext(1)]
	[Nullable(0)]
	public static class Extensions
	{
		// Token: 0x060011F2 RID: 4594 RVA: 0x000633A4 File Offset: 0x000615A4
		public static IJEnumerable<JToken> Ancestors<[Nullable(0)] T>(this IEnumerable<T> source) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return source.SelectMany((T j) => j.Ancestors()).AsJEnumerable();
		}

		// Token: 0x060011F3 RID: 4595 RVA: 0x000633E0 File Offset: 0x000615E0
		public static IJEnumerable<JToken> AncestorsAndSelf<[Nullable(0)] T>(this IEnumerable<T> source) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return source.SelectMany((T j) => j.AncestorsAndSelf()).AsJEnumerable();
		}

		// Token: 0x060011F4 RID: 4596 RVA: 0x0006341C File Offset: 0x0006161C
		public static IJEnumerable<JToken> Descendants<[Nullable(0)] T>(this IEnumerable<T> source) where T : JContainer
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return source.SelectMany((T j) => j.Descendants()).AsJEnumerable();
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x00063458 File Offset: 0x00061658
		public static IJEnumerable<JToken> DescendantsAndSelf<[Nullable(0)] T>(this IEnumerable<T> source) where T : JContainer
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return source.SelectMany((T j) => j.DescendantsAndSelf()).AsJEnumerable();
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x00063494 File Offset: 0x00061694
		public static IJEnumerable<JProperty> Properties(this IEnumerable<JObject> source)
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return source.SelectMany((JObject d) => d.Properties()).AsJEnumerable<JProperty>();
		}

		// Token: 0x060011F7 RID: 4599 RVA: 0x000634D0 File Offset: 0x000616D0
		public static IJEnumerable<JToken> Values(this IEnumerable<JToken> source, [Nullable(2)] object key)
		{
			return source.Values(key).AsJEnumerable();
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x000634E0 File Offset: 0x000616E0
		public static IJEnumerable<JToken> Values(this IEnumerable<JToken> source)
		{
			return source.Values(null);
		}

		// Token: 0x060011F9 RID: 4601 RVA: 0x000634EC File Offset: 0x000616EC
		public static IEnumerable<U> Values<[Nullable(2)] U>(this IEnumerable<JToken> source, object key)
		{
			return source.Values(key);
		}

		// Token: 0x060011FA RID: 4602 RVA: 0x000634F8 File Offset: 0x000616F8
		public static IEnumerable<U> Values<[Nullable(2)] U>(this IEnumerable<JToken> source)
		{
			return source.Values(null);
		}

		// Token: 0x060011FB RID: 4603 RVA: 0x00063504 File Offset: 0x00061704
		public static U Value<[Nullable(2)] U>(this IEnumerable<JToken> value)
		{
			return value.Value<JToken, U>();
		}

		// Token: 0x060011FC RID: 4604 RVA: 0x0006350C File Offset: 0x0006170C
		public static U Value<[Nullable(0)] T, [Nullable(2)] U>(this IEnumerable<T> value) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			JToken jtoken = value as JToken;
			if (jtoken == null)
			{
				throw new ArgumentException("Source value must be a JToken.");
			}
			return jtoken.Convert<JToken, U>();
		}

		// Token: 0x060011FD RID: 4605 RVA: 0x00063538 File Offset: 0x00061738
		internal static IEnumerable<U> Values<[Nullable(0)] T, [Nullable(2)] U>(this IEnumerable<T> source, [Nullable(2)] object key) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			if (key == null)
			{
				foreach (T t in source)
				{
					JValue jvalue = t as JValue;
					if (jvalue != null)
					{
						yield return jvalue.Convert<JValue, U>();
					}
					else
					{
						foreach (JToken token in t.Children())
						{
							yield return token.Convert<JToken, U>();
						}
						IEnumerator<JToken> enumerator2 = null;
					}
				}
				IEnumerator<T> enumerator = null;
			}
			else
			{
				foreach (T t2 in source)
				{
					JToken jtoken = t2[key];
					if (jtoken != null)
					{
						yield return jtoken.Convert<JToken, U>();
					}
				}
				IEnumerator<T> enumerator = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x060011FE RID: 4606 RVA: 0x00063550 File Offset: 0x00061750
		public static IJEnumerable<JToken> Children<[Nullable(0)] T>(this IEnumerable<T> source) where T : JToken
		{
			return source.Children<T, JToken>().AsJEnumerable();
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x00063560 File Offset: 0x00061760
		public static IEnumerable<U> Children<[Nullable(0)] T, [Nullable(2)] U>(this IEnumerable<T> source) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			return source.SelectMany((T c) => c.Children()).Convert<JToken, U>();
		}

		// Token: 0x06001200 RID: 4608 RVA: 0x0006359C File Offset: 0x0006179C
		internal static IEnumerable<U> Convert<[Nullable(0)] T, [Nullable(2)] U>(this IEnumerable<T> source) where T : JToken
		{
			ValidationUtils.ArgumentNotNull(source, "source");
			foreach (T t in source)
			{
				yield return t.Convert<JToken, U>();
			}
			IEnumerator<T> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001201 RID: 4609 RVA: 0x000635AC File Offset: 0x000617AC
		[return: MaybeNull]
		internal static U Convert<[Nullable(0)] T, [Nullable(2)] U>(this T token) where T : JToken
		{
			if (token == null)
			{
				return default(U);
			}
			if (token is U)
			{
				U result = token as U;
				if (typeof(U) != typeof(IComparable) && typeof(U) != typeof(IFormattable))
				{
					return result;
				}
			}
			JValue jvalue = token as JValue;
			if (jvalue == null)
			{
				throw new InvalidCastException("Cannot cast {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, token.GetType(), typeof(T)));
			}
			object value = jvalue.Value;
			if (value is U)
			{
				return (U)((object)value);
			}
			Type type = typeof(U);
			if (ReflectionUtils.IsNullableType(type))
			{
				if (jvalue.Value == null)
				{
					return default(U);
				}
				type = Nullable.GetUnderlyingType(type);
			}
			return (U)((object)System.Convert.ChangeType(jvalue.Value, type, CultureInfo.InvariantCulture));
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x000636D8 File Offset: 0x000618D8
		public static IJEnumerable<JToken> AsJEnumerable(this IEnumerable<JToken> source)
		{
			return source.AsJEnumerable<JToken>();
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x000636E0 File Offset: 0x000618E0
		public static IJEnumerable<T> AsJEnumerable<[Nullable(0)] T>(this IEnumerable<T> source) where T : JToken
		{
			if (source == null)
			{
				return null;
			}
			IJEnumerable<T> ijenumerable = source as IJEnumerable<T>;
			if (ijenumerable != null)
			{
				return ijenumerable;
			}
			return new JEnumerable<T>(source);
		}
	}
}
