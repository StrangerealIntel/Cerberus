using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

namespace Echelon.Stealer.Browsers.Helpers.NoiseMe.Drags.App.Models.JSON
{
	// Token: 0x02000034 RID: 52
	public abstract class JsonValue : IEnumerable
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00009EEC File Offset: 0x000080EC
		public virtual int Count
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000114 RID: 276
		public abstract JsonType JsonType { get; }

		// Token: 0x17000025 RID: 37
		public virtual JsonValue this[int index]
		{
			get
			{
				throw new InvalidOperationException();
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17000026 RID: 38
		public virtual JsonValue this[string key]
		{
			get
			{
				throw new InvalidOperationException();
			}
			set
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00009F14 File Offset: 0x00008114
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00009F1C File Offset: 0x0000811C
		public static JsonValue Load(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			return JsonValue.Load(new StreamReader(stream, true));
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00009F3C File Offset: 0x0000813C
		public static JsonValue Load(TextReader textReader)
		{
			if (textReader == null)
			{
				throw new ArgumentNullException("textReader");
			}
			return JsonValue.ToJsonValue<object>(new JavaScriptReader(textReader).Read());
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00009F60 File Offset: 0x00008160
		private static IEnumerable<KeyValuePair<string, JsonValue>> ToJsonPairEnumerable(IEnumerable<KeyValuePair<string, object>> kvpc)
		{
			foreach (KeyValuePair<string, object> keyValuePair in kvpc)
			{
				yield return new KeyValuePair<string, JsonValue>(keyValuePair.Key, JsonValue.ToJsonValue<object>(keyValuePair.Value));
			}
			IEnumerator<KeyValuePair<string, object>> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00009F70 File Offset: 0x00008170
		private static IEnumerable<JsonValue> ToJsonValueEnumerable(IEnumerable arr)
		{
			foreach (object ret in arr)
			{
				yield return JsonValue.ToJsonValue<object>(ret);
			}
			IEnumerator enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00009F80 File Offset: 0x00008180
		public static JsonValue ToJsonValue<T>(T ret)
		{
			if (ret == null)
			{
				return null;
			}
			T t;
			if ((t = ret) is bool)
			{
				return new JsonPrimitive((bool)((object)t));
			}
			if ((t = ret) is byte)
			{
				return new JsonPrimitive((byte)((object)t));
			}
			if ((t = ret) is char)
			{
				return new JsonPrimitive((char)((object)t));
			}
			if ((t = ret) is decimal)
			{
				return new JsonPrimitive((decimal)((object)t));
			}
			if ((t = ret) is double)
			{
				return new JsonPrimitive((double)((object)t));
			}
			if ((t = ret) is float)
			{
				return new JsonPrimitive((float)((object)t));
			}
			if ((t = ret) is int)
			{
				return new JsonPrimitive((int)((object)t));
			}
			if ((t = ret) is long)
			{
				return new JsonPrimitive((long)((object)t));
			}
			if ((t = ret) is sbyte)
			{
				return new JsonPrimitive((sbyte)((object)t));
			}
			if ((t = ret) is short)
			{
				return new JsonPrimitive((short)((object)t));
			}
			string value;
			if ((value = (ret as string)) != null)
			{
				return new JsonPrimitive(value);
			}
			if ((t = ret) is uint)
			{
				return new JsonPrimitive((uint)((object)t));
			}
			if ((t = ret) is ulong)
			{
				return new JsonPrimitive((ulong)((object)t));
			}
			if ((t = ret) is ushort)
			{
				return new JsonPrimitive((ushort)((object)t));
			}
			if ((t = ret) is DateTime)
			{
				return new JsonPrimitive((DateTime)((object)t));
			}
			if ((t = ret) is DateTimeOffset)
			{
				return new JsonPrimitive((DateTimeOffset)((object)t));
			}
			if ((t = ret) is Guid)
			{
				return new JsonPrimitive((Guid)((object)t));
			}
			if ((t = ret) is TimeSpan)
			{
				return new JsonPrimitive((TimeSpan)((object)t));
			}
			Uri value2;
			if ((value2 = (ret as Uri)) != null)
			{
				return new JsonPrimitive(value2);
			}
			IEnumerable<KeyValuePair<string, object>> enumerable = ret as IEnumerable<KeyValuePair<string, object>>;
			if (enumerable != null)
			{
				return new JsonObject(JsonValue.ToJsonPairEnumerable(enumerable));
			}
			IEnumerable enumerable2 = ret as IEnumerable;
			if (enumerable2 != null)
			{
				return new JsonArray(JsonValue.ToJsonValueEnumerable(enumerable2));
			}
			if (!(ret is IEnumerable))
			{
				PropertyInfo[] properties = ret.GetType().GetProperties();
				Dictionary<string, object> dictionary = new Dictionary<string, object>();
				foreach (PropertyInfo propertyInfo in properties)
				{
					dictionary.Add(propertyInfo.Name, propertyInfo.GetValue(ret, null).IsNull("null"));
				}
				if (dictionary.Count > 0)
				{
					return new JsonObject(JsonValue.ToJsonPairEnumerable(dictionary));
				}
			}
			throw new NotSupportedException(string.Format("Unexpected parser return type: {0}", ret.GetType()));
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000A30C File Offset: 0x0000850C
		public static JsonValue Parse(string jsonString)
		{
			if (jsonString == null)
			{
				throw new ArgumentNullException("jsonString");
			}
			return JsonValue.Load(new StringReader(jsonString));
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000A32C File Offset: 0x0000852C
		public virtual bool ContainsKey(string key)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000A334 File Offset: 0x00008534
		public virtual void Save(Stream stream, bool parsing)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.Save(new StreamWriter(stream), parsing);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000A354 File Offset: 0x00008554
		public virtual void Save(TextWriter textWriter, bool parsing)
		{
			if (textWriter == null)
			{
				throw new ArgumentNullException("textWriter");
			}
			this.Savepublic(textWriter, parsing);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000A370 File Offset: 0x00008570
		private void Savepublic(TextWriter w, bool saving)
		{
			switch (this.JsonType)
			{
			case JsonType.String:
				if (saving)
				{
					w.Write('"');
				}
				w.Write(this.EscapeString(((JsonPrimitive)this).GetFormattedString()));
				if (saving)
				{
					w.Write('"');
					return;
				}
				return;
			case JsonType.Object:
			{
				w.Write('{');
				bool flag = false;
				foreach (KeyValuePair<string, JsonValue> keyValuePair in ((JsonObject)this))
				{
					if (flag)
					{
						w.Write(", ");
					}
					w.Write('"');
					w.Write(this.EscapeString(keyValuePair.Key));
					w.Write("\": ");
					if (keyValuePair.Value == null)
					{
						w.Write("null");
					}
					else
					{
						keyValuePair.Value.Savepublic(w, saving);
					}
					flag = true;
				}
				w.Write('}');
				return;
			}
			case JsonType.Array:
			{
				w.Write('[');
				bool flag2 = false;
				foreach (JsonValue jsonValue in ((IEnumerable<JsonValue>)((JsonArray)this)))
				{
					if (flag2)
					{
						w.Write(", ");
					}
					if (jsonValue != null)
					{
						jsonValue.Savepublic(w, saving);
					}
					else
					{
						w.Write("null");
					}
					flag2 = true;
				}
				w.Write(']');
				return;
			}
			case JsonType.Boolean:
				w.Write(this ? "true" : "false");
				return;
			}
			w.Write(((JsonPrimitive)this).GetFormattedString());
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000A550 File Offset: 0x00008750
		public string ToString(bool saving = true)
		{
			StringWriter stringWriter = new StringWriter();
			this.Save(stringWriter, saving);
			return stringWriter.ToString();
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000A578 File Offset: 0x00008778
		private bool NeedEscape(string src, int i)
		{
			char c = src[i];
			return c < ' ' || c == '"' || c == '\\' || (c >= '\ud800' && c <= '\udbff' && (i == src.Length - 1 || src[i + 1] < '\udc00' || src[i + 1] > '\udfff')) || (c >= '\udc00' && c <= '\udfff' && (i == 0 || src[i - 1] < '\ud800' || src[i - 1] > '\udbff')) || c == '\u2028' || c == '\u2029' || (c == '/' && i > 0 && src[i - 1] == '<');
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000A66C File Offset: 0x0000886C
		public string EscapeString(string src)
		{
			if (src == null)
			{
				return null;
			}
			for (int i = 0; i < src.Length; i++)
			{
				if (this.NeedEscape(src, i))
				{
					StringBuilder stringBuilder = new StringBuilder();
					if (i > 0)
					{
						stringBuilder.Append(src, 0, i);
					}
					return this.DoEscapeString(stringBuilder, src, i);
				}
			}
			return src;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000A6C8 File Offset: 0x000088C8
		private string DoEscapeString(StringBuilder sb, string src, int cur)
		{
			int num = cur;
			for (int i = cur; i < src.Length; i++)
			{
				if (this.NeedEscape(src, i))
				{
					sb.Append(src, num, i - num);
					char c = src[i];
					if (c <= '"')
					{
						switch (c)
						{
						case '\b':
							sb.Append("\\b");
							break;
						case '\t':
							sb.Append("\\t");
							break;
						case '\n':
							sb.Append("\\n");
							break;
						case '\v':
							goto IL_FC;
						case '\f':
							sb.Append("\\f");
							break;
						case '\r':
							sb.Append("\\r");
							break;
						default:
							if (c != '"')
							{
								goto IL_FC;
							}
							sb.Append("\\\"");
							break;
						}
					}
					else if (c != '/')
					{
						if (c != '\\')
						{
							goto IL_FC;
						}
						sb.Append("\\\\");
					}
					else
					{
						sb.Append("\\/");
					}
					IL_123:
					num = i + 1;
					goto IL_127;
					IL_FC:
					sb.Append("\\u");
					sb.Append(((int)src[i]).ToString("x04"));
					goto IL_123;
				}
				IL_127:;
			}
			sb.Append(src, num, src.Length - num);
			return sb.ToString();
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000A828 File Offset: 0x00008A28
		public static implicit operator JsonValue(bool value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000A830 File Offset: 0x00008A30
		public static implicit operator JsonValue(byte value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000A838 File Offset: 0x00008A38
		public static implicit operator JsonValue(char value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000A840 File Offset: 0x00008A40
		public static implicit operator JsonValue(decimal value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000A848 File Offset: 0x00008A48
		public static implicit operator JsonValue(double value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000A850 File Offset: 0x00008A50
		public static implicit operator JsonValue(float value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000A858 File Offset: 0x00008A58
		public static implicit operator JsonValue(int value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000A860 File Offset: 0x00008A60
		public static implicit operator JsonValue(long value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000A868 File Offset: 0x00008A68
		public static implicit operator JsonValue(sbyte value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000A870 File Offset: 0x00008A70
		public static implicit operator JsonValue(short value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000A878 File Offset: 0x00008A78
		public static implicit operator JsonValue(string value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000A880 File Offset: 0x00008A80
		public static implicit operator JsonValue(uint value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000A888 File Offset: 0x00008A88
		public static implicit operator JsonValue(ulong value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000A890 File Offset: 0x00008A90
		public static implicit operator JsonValue(ushort value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000A898 File Offset: 0x00008A98
		public static implicit operator JsonValue(DateTime value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000A8A0 File Offset: 0x00008AA0
		public static implicit operator JsonValue(DateTimeOffset value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000A8A8 File Offset: 0x00008AA8
		public static implicit operator JsonValue(Guid value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000A8B0 File Offset: 0x00008AB0
		public static implicit operator JsonValue(TimeSpan value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000A8B8 File Offset: 0x00008AB8
		public static implicit operator JsonValue(Uri value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000A8C0 File Offset: 0x00008AC0
		public static implicit operator bool(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToBoolean(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000A8E8 File Offset: 0x00008AE8
		public static implicit operator byte(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToByte(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000A910 File Offset: 0x00008B10
		public static implicit operator char(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToChar(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000A938 File Offset: 0x00008B38
		public static implicit operator decimal(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToDecimal(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000A960 File Offset: 0x00008B60
		public static implicit operator double(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToDouble(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000A988 File Offset: 0x00008B88
		public static implicit operator float(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToSingle(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000A9B0 File Offset: 0x00008BB0
		public static implicit operator int(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToInt32(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000A9D8 File Offset: 0x00008BD8
		public static implicit operator long(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToInt64(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000AA00 File Offset: 0x00008C00
		public static implicit operator sbyte(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToSByte(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000AA28 File Offset: 0x00008C28
		public static implicit operator short(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToInt16(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000AA50 File Offset: 0x00008C50
		public static implicit operator string(JsonValue value)
		{
			if (value == null)
			{
				return null;
			}
			return value.ToString(true);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000AA64 File Offset: 0x00008C64
		public static implicit operator uint(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToUInt32(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000AA8C File Offset: 0x00008C8C
		public static implicit operator ulong(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToUInt64(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000AAB4 File Offset: 0x00008CB4
		public static implicit operator ushort(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToUInt16(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000AADC File Offset: 0x00008CDC
		public static implicit operator DateTime(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (DateTime)((JsonPrimitive)value).Value;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000AB00 File Offset: 0x00008D00
		public static implicit operator DateTimeOffset(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (DateTimeOffset)((JsonPrimitive)value).Value;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000AB24 File Offset: 0x00008D24
		public static implicit operator TimeSpan(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (TimeSpan)((JsonPrimitive)value).Value;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000AB48 File Offset: 0x00008D48
		public static implicit operator Guid(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (Guid)((JsonPrimitive)value).Value;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000AB6C File Offset: 0x00008D6C
		public static implicit operator Uri(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (Uri)((JsonPrimitive)value).Value;
		}

		// Token: 0x0400006D RID: 109
		public static string buildversion = "V6.3.1 by Ambal2221, original code by MadCode. Special for xss.is";
	}
}
