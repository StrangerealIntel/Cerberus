using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x02000135 RID: 309
	[NullableContext(1)]
	[Nullable(0)]
	public static class JsonConvert
	{
		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060009DF RID: 2527 RVA: 0x0003DF64 File Offset: 0x0003C164
		// (set) Token: 0x060009E0 RID: 2528 RVA: 0x0003DF6C File Offset: 0x0003C16C
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public static Func<JsonSerializerSettings> DefaultSettings { [return: Nullable(new byte[]
		{
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1
		})] set; }

		// Token: 0x060009E1 RID: 2529 RVA: 0x0003DF74 File Offset: 0x0003C174
		public static string ToString(DateTime value)
		{
			return JsonConvert.ToString(value, DateFormatHandling.IsoDateFormat, DateTimeZoneHandling.RoundtripKind);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x0003DF80 File Offset: 0x0003C180
		public static string ToString(DateTime value, DateFormatHandling format, DateTimeZoneHandling timeZoneHandling)
		{
			DateTime value2 = DateTimeUtils.EnsureDateTime(value, timeZoneHandling);
			string result;
			using (StringWriter stringWriter = StringUtils.CreateStringWriter(64))
			{
				stringWriter.Write('"');
				DateTimeUtils.WriteDateTimeString(stringWriter, value2, format, null, CultureInfo.InvariantCulture);
				stringWriter.Write('"');
				result = stringWriter.ToString();
			}
			return result;
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x0003DFE8 File Offset: 0x0003C1E8
		public static string ToString(DateTimeOffset value)
		{
			return JsonConvert.ToString(value, DateFormatHandling.IsoDateFormat);
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0003DFF4 File Offset: 0x0003C1F4
		public static string ToString(DateTimeOffset value, DateFormatHandling format)
		{
			string result;
			using (StringWriter stringWriter = StringUtils.CreateStringWriter(64))
			{
				stringWriter.Write('"');
				DateTimeUtils.WriteDateTimeOffsetString(stringWriter, value, format, null, CultureInfo.InvariantCulture);
				stringWriter.Write('"');
				result = stringWriter.ToString();
			}
			return result;
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x0003E054 File Offset: 0x0003C254
		public static string ToString(bool value)
		{
			if (!value)
			{
				return JsonConvert.False;
			}
			return JsonConvert.True;
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x0003E068 File Offset: 0x0003C268
		public static string ToString(char value)
		{
			return JsonConvert.ToString(char.ToString(value));
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x0003E078 File Offset: 0x0003C278
		public static string ToString(Enum value)
		{
			return value.ToString("D");
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x0003E088 File Offset: 0x0003C288
		public static string ToString(int value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x0003E098 File Offset: 0x0003C298
		public static string ToString(short value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0003E0A8 File Offset: 0x0003C2A8
		[CLSCompliant(false)]
		public static string ToString(ushort value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0003E0B8 File Offset: 0x0003C2B8
		[CLSCompliant(false)]
		public static string ToString(uint value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x0003E0C8 File Offset: 0x0003C2C8
		public static string ToString(long value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x0003E0D8 File Offset: 0x0003C2D8
		private static string ToStringInternal(System.Numerics.BigInteger value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x0003E0E8 File Offset: 0x0003C2E8
		[CLSCompliant(false)]
		public static string ToString(ulong value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x0003E0F8 File Offset: 0x0003C2F8
		public static string ToString(float value)
		{
			return JsonConvert.EnsureDecimalPlace((double)value, value.ToString("R", CultureInfo.InvariantCulture));
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x0003E114 File Offset: 0x0003C314
		internal static string ToString(float value, FloatFormatHandling floatFormatHandling, char quoteChar, bool nullable)
		{
			return JsonConvert.EnsureFloatFormat((double)value, JsonConvert.EnsureDecimalPlace((double)value, value.ToString("R", CultureInfo.InvariantCulture)), floatFormatHandling, quoteChar, nullable);
		}

		// Token: 0x060009F1 RID: 2545 RVA: 0x0003E138 File Offset: 0x0003C338
		private static string EnsureFloatFormat(double value, string text, FloatFormatHandling floatFormatHandling, char quoteChar, bool nullable)
		{
			if (floatFormatHandling == FloatFormatHandling.Symbol || (!double.IsInfinity(value) && !double.IsNaN(value)))
			{
				return text;
			}
			if (floatFormatHandling != FloatFormatHandling.DefaultValue)
			{
				return quoteChar.ToString() + text + quoteChar.ToString();
			}
			if (nullable)
			{
				return JsonConvert.Null;
			}
			return "0.0";
		}

		// Token: 0x060009F2 RID: 2546 RVA: 0x0003E198 File Offset: 0x0003C398
		public static string ToString(double value)
		{
			return JsonConvert.EnsureDecimalPlace(value, value.ToString("R", CultureInfo.InvariantCulture));
		}

		// Token: 0x060009F3 RID: 2547 RVA: 0x0003E1B4 File Offset: 0x0003C3B4
		internal static string ToString(double value, FloatFormatHandling floatFormatHandling, char quoteChar, bool nullable)
		{
			return JsonConvert.EnsureFloatFormat(value, JsonConvert.EnsureDecimalPlace(value, value.ToString("R", CultureInfo.InvariantCulture)), floatFormatHandling, quoteChar, nullable);
		}

		// Token: 0x060009F4 RID: 2548 RVA: 0x0003E1D8 File Offset: 0x0003C3D8
		private static string EnsureDecimalPlace(double value, string text)
		{
			if (double.IsNaN(value) || double.IsInfinity(value) || text.IndexOf('.') != -1 || text.IndexOf('E') != -1 || text.IndexOf('e') != -1)
			{
				return text;
			}
			return text + ".0";
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x0003E238 File Offset: 0x0003C438
		private static string EnsureDecimalPlace(string text)
		{
			if (text.IndexOf('.') != -1)
			{
				return text;
			}
			return text + ".0";
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x0003E258 File Offset: 0x0003C458
		public static string ToString(byte value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x0003E268 File Offset: 0x0003C468
		[CLSCompliant(false)]
		public static string ToString(sbyte value)
		{
			return value.ToString(null, CultureInfo.InvariantCulture);
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x0003E278 File Offset: 0x0003C478
		public static string ToString(decimal value)
		{
			return JsonConvert.EnsureDecimalPlace(value.ToString(null, CultureInfo.InvariantCulture));
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0003E28C File Offset: 0x0003C48C
		public static string ToString(Guid value)
		{
			return JsonConvert.ToString(value, '"');
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0003E298 File Offset: 0x0003C498
		internal static string ToString(Guid value, char quoteChar)
		{
			string str = value.ToString("D", CultureInfo.InvariantCulture);
			string text = quoteChar.ToString(CultureInfo.InvariantCulture);
			return text + str + text;
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x0003E2D0 File Offset: 0x0003C4D0
		public static string ToString(TimeSpan value)
		{
			return JsonConvert.ToString(value, '"');
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x0003E2DC File Offset: 0x0003C4DC
		internal static string ToString(TimeSpan value, char quoteChar)
		{
			return JsonConvert.ToString(value.ToString(), quoteChar);
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x0003E2F4 File Offset: 0x0003C4F4
		public static string ToString([Nullable(2)] Uri value)
		{
			if (value == null)
			{
				return JsonConvert.Null;
			}
			return JsonConvert.ToString(value, '"');
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x0003E310 File Offset: 0x0003C510
		internal static string ToString(Uri value, char quoteChar)
		{
			return JsonConvert.ToString(value.OriginalString, quoteChar);
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x0003E320 File Offset: 0x0003C520
		public static string ToString([Nullable(2)] string value)
		{
			return JsonConvert.ToString(value, '"');
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x0003E32C File Offset: 0x0003C52C
		public static string ToString([Nullable(2)] string value, char delimiter)
		{
			return JsonConvert.ToString(value, delimiter, StringEscapeHandling.Default);
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0003E338 File Offset: 0x0003C538
		public static string ToString([Nullable(2)] string value, char delimiter, StringEscapeHandling stringEscapeHandling)
		{
			if (delimiter != '"' && delimiter != '\'')
			{
				throw new ArgumentException("Delimiter must be a single or double quote.", "delimiter");
			}
			return JavaScriptUtils.ToEscapedJavaScriptString(value, delimiter, true, stringEscapeHandling);
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0003E364 File Offset: 0x0003C564
		public static string ToString([Nullable(2)] object value)
		{
			if (value == null)
			{
				return JsonConvert.Null;
			}
			switch (ConvertUtils.GetTypeCode(value.GetType()))
			{
			case PrimitiveTypeCode.Char:
				return JsonConvert.ToString((char)value);
			case PrimitiveTypeCode.Boolean:
				return JsonConvert.ToString((bool)value);
			case PrimitiveTypeCode.SByte:
				return JsonConvert.ToString((sbyte)value);
			case PrimitiveTypeCode.Int16:
				return JsonConvert.ToString((short)value);
			case PrimitiveTypeCode.UInt16:
				return JsonConvert.ToString((ushort)value);
			case PrimitiveTypeCode.Int32:
				return JsonConvert.ToString((int)value);
			case PrimitiveTypeCode.Byte:
				return JsonConvert.ToString((byte)value);
			case PrimitiveTypeCode.UInt32:
				return JsonConvert.ToString((uint)value);
			case PrimitiveTypeCode.Int64:
				return JsonConvert.ToString((long)value);
			case PrimitiveTypeCode.UInt64:
				return JsonConvert.ToString((ulong)value);
			case PrimitiveTypeCode.Single:
				return JsonConvert.ToString((float)value);
			case PrimitiveTypeCode.Double:
				return JsonConvert.ToString((double)value);
			case PrimitiveTypeCode.DateTime:
				return JsonConvert.ToString((DateTime)value);
			case PrimitiveTypeCode.DateTimeOffset:
				return JsonConvert.ToString((DateTimeOffset)value);
			case PrimitiveTypeCode.Decimal:
				return JsonConvert.ToString((decimal)value);
			case PrimitiveTypeCode.Guid:
				return JsonConvert.ToString((Guid)value);
			case PrimitiveTypeCode.TimeSpan:
				return JsonConvert.ToString((TimeSpan)value);
			case PrimitiveTypeCode.BigInteger:
				return JsonConvert.ToStringInternal((System.Numerics.BigInteger)value);
			case PrimitiveTypeCode.Uri:
				return JsonConvert.ToString((Uri)value);
			case PrimitiveTypeCode.String:
				return JsonConvert.ToString((string)value);
			case PrimitiveTypeCode.DBNull:
				return JsonConvert.Null;
			}
			throw new ArgumentException("Unsupported type: {0}. Use the JsonSerializer class to get the object's JSON representation.".FormatWith(CultureInfo.InvariantCulture, value.GetType()));
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0003E54C File Offset: 0x0003C74C
		[DebuggerStepThrough]
		public static string SerializeObject([Nullable(2)] object value)
		{
			return JsonConvert.SerializeObject(value, null, null);
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0003E558 File Offset: 0x0003C758
		[DebuggerStepThrough]
		public static string SerializeObject([Nullable(2)] object value, Formatting formatting)
		{
			return JsonConvert.SerializeObject(value, formatting, null);
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0003E564 File Offset: 0x0003C764
		[DebuggerStepThrough]
		public static string SerializeObject([Nullable(2)] object value, params JsonConverter[] converters)
		{
			object obj;
			if (converters == null || converters.Length == 0)
			{
				obj = null;
			}
			else
			{
				(obj = new JsonSerializerSettings()).Converters = converters;
			}
			JsonSerializerSettings settings = obj;
			return JsonConvert.SerializeObject(value, null, settings);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0003E5A0 File Offset: 0x0003C7A0
		[DebuggerStepThrough]
		public static string SerializeObject([Nullable(2)] object value, Formatting formatting, params JsonConverter[] converters)
		{
			object obj;
			if (converters == null || converters.Length == 0)
			{
				obj = null;
			}
			else
			{
				(obj = new JsonSerializerSettings()).Converters = converters;
			}
			JsonSerializerSettings settings = obj;
			return JsonConvert.SerializeObject(value, null, formatting, settings);
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x0003E5DC File Offset: 0x0003C7DC
		[DebuggerStepThrough]
		public static string SerializeObject([Nullable(2)] object value, JsonSerializerSettings settings)
		{
			return JsonConvert.SerializeObject(value, null, settings);
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x0003E5E8 File Offset: 0x0003C7E8
		[NullableContext(2)]
		[DebuggerStepThrough]
		[return: Nullable(1)]
		public static string SerializeObject(object value, Type type, JsonSerializerSettings settings)
		{
			JsonSerializer jsonSerializer = JsonSerializer.CreateDefault(settings);
			return JsonConvert.SerializeObjectInternal(value, type, jsonSerializer);
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x0003E608 File Offset: 0x0003C808
		[NullableContext(2)]
		[DebuggerStepThrough]
		[return: Nullable(1)]
		public static string SerializeObject(object value, Formatting formatting, JsonSerializerSettings settings)
		{
			return JsonConvert.SerializeObject(value, null, formatting, settings);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0003E614 File Offset: 0x0003C814
		[NullableContext(2)]
		[DebuggerStepThrough]
		[return: Nullable(1)]
		public static string SerializeObject(object value, Type type, Formatting formatting, JsonSerializerSettings settings)
		{
			JsonSerializer jsonSerializer = JsonSerializer.CreateDefault(settings);
			jsonSerializer.Formatting = formatting;
			return JsonConvert.SerializeObjectInternal(value, type, jsonSerializer);
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x0003E63C File Offset: 0x0003C83C
		private static string SerializeObjectInternal([Nullable(2)] object value, [Nullable(2)] Type type, JsonSerializer jsonSerializer)
		{
			StringWriter stringWriter = new StringWriter(new StringBuilder(256), CultureInfo.InvariantCulture);
			using (JsonTextWriter jsonTextWriter = new JsonTextWriter(stringWriter))
			{
				jsonTextWriter.Formatting = jsonSerializer.Formatting;
				jsonSerializer.Serialize(jsonTextWriter, value, type);
			}
			return stringWriter.ToString();
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x0003E6A4 File Offset: 0x0003C8A4
		[DebuggerStepThrough]
		[return: Nullable(2)]
		public static object DeserializeObject(string value)
		{
			return JsonConvert.DeserializeObject(value, null, null);
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x0003E6B0 File Offset: 0x0003C8B0
		[DebuggerStepThrough]
		[return: Nullable(2)]
		public static object DeserializeObject(string value, JsonSerializerSettings settings)
		{
			return JsonConvert.DeserializeObject(value, null, settings);
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x0003E6BC File Offset: 0x0003C8BC
		[DebuggerStepThrough]
		[return: Nullable(2)]
		public static object DeserializeObject(string value, Type type)
		{
			return JsonConvert.DeserializeObject(value, type, null);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0003E6C8 File Offset: 0x0003C8C8
		[DebuggerStepThrough]
		public static T DeserializeObject<[Nullable(2)] T>(string value)
		{
			return JsonConvert.DeserializeObject<T>(value, null);
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0003E6D4 File Offset: 0x0003C8D4
		[DebuggerStepThrough]
		public static T DeserializeAnonymousType<[Nullable(2)] T>(string value, T anonymousTypeObject)
		{
			return JsonConvert.DeserializeObject<T>(value);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0003E6DC File Offset: 0x0003C8DC
		[DebuggerStepThrough]
		public static T DeserializeAnonymousType<[Nullable(2)] T>(string value, T anonymousTypeObject, JsonSerializerSettings settings)
		{
			return JsonConvert.DeserializeObject<T>(value, settings);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0003E6E8 File Offset: 0x0003C8E8
		[DebuggerStepThrough]
		[return: MaybeNull]
		public static T DeserializeObject<[Nullable(2)] T>(string value, params JsonConverter[] converters)
		{
			return (T)((object)JsonConvert.DeserializeObject(value, typeof(T), converters));
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0003E700 File Offset: 0x0003C900
		[DebuggerStepThrough]
		[return: MaybeNull]
		public static T DeserializeObject<[Nullable(2)] T>(string value, [Nullable(2)] JsonSerializerSettings settings)
		{
			return (T)((object)JsonConvert.DeserializeObject(value, typeof(T), settings));
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0003E718 File Offset: 0x0003C918
		[DebuggerStepThrough]
		[return: Nullable(2)]
		public static object DeserializeObject(string value, Type type, params JsonConverter[] converters)
		{
			object obj;
			if (converters == null || converters.Length == 0)
			{
				obj = null;
			}
			else
			{
				(obj = new JsonSerializerSettings()).Converters = converters;
			}
			JsonSerializerSettings settings = obj;
			return JsonConvert.DeserializeObject(value, type, settings);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0003E754 File Offset: 0x0003C954
		[NullableContext(2)]
		public static object DeserializeObject([Nullable(1)] string value, Type type, JsonSerializerSettings settings)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			JsonSerializer jsonSerializer = JsonSerializer.CreateDefault(settings);
			if (!jsonSerializer.IsCheckAdditionalContentSet())
			{
				jsonSerializer.CheckAdditionalContent = true;
			}
			object result;
			using (JsonTextReader jsonTextReader = new JsonTextReader(new StringReader(value)))
			{
				result = jsonSerializer.Deserialize(jsonTextReader, type);
			}
			return result;
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0003E7C0 File Offset: 0x0003C9C0
		[DebuggerStepThrough]
		public static void PopulateObject(string value, object target)
		{
			JsonConvert.PopulateObject(value, target, null);
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0003E7CC File Offset: 0x0003C9CC
		public static void PopulateObject(string value, object target, [Nullable(2)] JsonSerializerSettings settings)
		{
			JsonSerializer jsonSerializer = JsonSerializer.CreateDefault(settings);
			using (JsonReader jsonReader = new JsonTextReader(new StringReader(value)))
			{
				jsonSerializer.Populate(jsonReader, target);
				if (settings != null && settings.CheckAdditionalContent)
				{
					while (jsonReader.Read())
					{
						if (jsonReader.TokenType != JsonToken.Comment)
						{
							throw JsonSerializationException.Create(jsonReader, "Additional text found in JSON string after finishing deserializing object.");
						}
					}
				}
			}
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0003E84C File Offset: 0x0003CA4C
		public static string SerializeXmlNode([Nullable(2)] XmlNode node)
		{
			return JsonConvert.SerializeXmlNode(node, Formatting.None);
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0003E858 File Offset: 0x0003CA58
		public static string SerializeXmlNode([Nullable(2)] XmlNode node, Formatting formatting)
		{
			XmlNodeConverter xmlNodeConverter = new XmlNodeConverter();
			return JsonConvert.SerializeObject(node, formatting, new JsonConverter[]
			{
				xmlNodeConverter
			});
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0003E880 File Offset: 0x0003CA80
		public static string SerializeXmlNode([Nullable(2)] XmlNode node, Formatting formatting, bool omitRootObject)
		{
			XmlNodeConverter xmlNodeConverter = new XmlNodeConverter
			{
				OmitRootObject = omitRootObject
			};
			return JsonConvert.SerializeObject(node, formatting, new JsonConverter[]
			{
				xmlNodeConverter
			});
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0003E8B0 File Offset: 0x0003CAB0
		[return: Nullable(2)]
		public static XmlDocument DeserializeXmlNode(string value)
		{
			return JsonConvert.DeserializeXmlNode(value, null);
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0003E8BC File Offset: 0x0003CABC
		[NullableContext(2)]
		public static XmlDocument DeserializeXmlNode([Nullable(1)] string value, string deserializeRootElementName)
		{
			return JsonConvert.DeserializeXmlNode(value, deserializeRootElementName, false);
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0003E8C8 File Offset: 0x0003CAC8
		[NullableContext(2)]
		public static XmlDocument DeserializeXmlNode([Nullable(1)] string value, string deserializeRootElementName, bool writeArrayAttribute)
		{
			return JsonConvert.DeserializeXmlNode(value, deserializeRootElementName, writeArrayAttribute, false);
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0003E8D4 File Offset: 0x0003CAD4
		[NullableContext(2)]
		public static XmlDocument DeserializeXmlNode([Nullable(1)] string value, string deserializeRootElementName, bool writeArrayAttribute, bool encodeSpecialCharacters)
		{
			XmlNodeConverter xmlNodeConverter = new XmlNodeConverter();
			xmlNodeConverter.DeserializeRootElementName = deserializeRootElementName;
			xmlNodeConverter.WriteArrayAttribute = writeArrayAttribute;
			xmlNodeConverter.EncodeSpecialCharacters = encodeSpecialCharacters;
			return (XmlDocument)JsonConvert.DeserializeObject(value, typeof(XmlDocument), new JsonConverter[]
			{
				xmlNodeConverter
			});
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0003E920 File Offset: 0x0003CB20
		public static string SerializeXNode([Nullable(2)] XObject node)
		{
			return JsonConvert.SerializeXNode(node, Formatting.None);
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0003E92C File Offset: 0x0003CB2C
		public static string SerializeXNode([Nullable(2)] XObject node, Formatting formatting)
		{
			return JsonConvert.SerializeXNode(node, formatting, false);
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0003E938 File Offset: 0x0003CB38
		public static string SerializeXNode([Nullable(2)] XObject node, Formatting formatting, bool omitRootObject)
		{
			XmlNodeConverter xmlNodeConverter = new XmlNodeConverter
			{
				OmitRootObject = omitRootObject
			};
			return JsonConvert.SerializeObject(node, formatting, new JsonConverter[]
			{
				xmlNodeConverter
			});
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0003E968 File Offset: 0x0003CB68
		[return: Nullable(2)]
		public static XDocument DeserializeXNode(string value)
		{
			return JsonConvert.DeserializeXNode(value, null);
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0003E974 File Offset: 0x0003CB74
		[NullableContext(2)]
		public static XDocument DeserializeXNode([Nullable(1)] string value, string deserializeRootElementName)
		{
			return JsonConvert.DeserializeXNode(value, deserializeRootElementName, false);
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0003E980 File Offset: 0x0003CB80
		[NullableContext(2)]
		public static XDocument DeserializeXNode([Nullable(1)] string value, string deserializeRootElementName, bool writeArrayAttribute)
		{
			return JsonConvert.DeserializeXNode(value, deserializeRootElementName, writeArrayAttribute, false);
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0003E98C File Offset: 0x0003CB8C
		[NullableContext(2)]
		public static XDocument DeserializeXNode([Nullable(1)] string value, string deserializeRootElementName, bool writeArrayAttribute, bool encodeSpecialCharacters)
		{
			XmlNodeConverter xmlNodeConverter = new XmlNodeConverter();
			xmlNodeConverter.DeserializeRootElementName = deserializeRootElementName;
			xmlNodeConverter.WriteArrayAttribute = writeArrayAttribute;
			xmlNodeConverter.EncodeSpecialCharacters = encodeSpecialCharacters;
			return (XDocument)JsonConvert.DeserializeObject(value, typeof(XDocument), new JsonConverter[]
			{
				xmlNodeConverter
			});
		}

		// Token: 0x040005AF RID: 1455
		public static readonly string True = "true";

		// Token: 0x040005B0 RID: 1456
		public static readonly string False = "false";

		// Token: 0x040005B1 RID: 1457
		public static readonly string Null = "null";

		// Token: 0x040005B2 RID: 1458
		public static readonly string Undefined = "undefined";

		// Token: 0x040005B3 RID: 1459
		public static readonly string PositiveInfinity = "Infinity";

		// Token: 0x040005B4 RID: 1460
		public static readonly string NegativeInfinity = "-Infinity";

		// Token: 0x040005B5 RID: 1461
		public static readonly string NaN = "NaN";
	}
}
