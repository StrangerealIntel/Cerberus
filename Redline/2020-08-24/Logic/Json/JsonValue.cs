using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using RedLine.Logic.Extensions;

namespace RedLine.Logic.Json
{
	// Token: 0x02000056 RID: 86
	public abstract class JsonValue : IEnumerable
	{
		// Token: 0x0600023D RID: 573 RVA: 0x00009360 File Offset: 0x00007560
		public static JsonValue Load(Stream stream)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			return JsonValue.Load(new StreamReader(stream, true));
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000937C File Offset: 0x0000757C
		public static JsonValue Load(TextReader textReader)
		{
			if (textReader == null)
			{
				throw new ArgumentNullException("textReader");
			}
			return JsonValue.ToJsonValue<object>(new JavaScriptReader(textReader).Read());
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000939C File Offset: 0x0000759C
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

		// Token: 0x06000240 RID: 576 RVA: 0x000093AC File Offset: 0x000075AC
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

		// Token: 0x06000241 RID: 577 RVA: 0x000093BC File Offset: 0x000075BC
		public static JsonValue ToJsonValue<T>(T ret)
		{
			if (ret == null)
			{
				return null;
			}
			T t;
			if ((t = ret) is bool)
			{
				bool value = (bool)((object)t);
				return new JsonPrimitive(value);
			}
			if ((t = ret) is byte)
			{
				byte value2 = (byte)((object)t);
				return new JsonPrimitive(value2);
			}
			if ((t = ret) is char)
			{
				char value3 = (char)((object)t);
				return new JsonPrimitive(value3);
			}
			if ((t = ret) is decimal)
			{
				decimal value4 = (decimal)((object)t);
				return new JsonPrimitive(value4);
			}
			if ((t = ret) is double)
			{
				double value5 = (double)((object)t);
				return new JsonPrimitive(value5);
			}
			if ((t = ret) is float)
			{
				float value6 = (float)((object)t);
				return new JsonPrimitive(value6);
			}
			if ((t = ret) is int)
			{
				int value7 = (int)((object)t);
				return new JsonPrimitive(value7);
			}
			if ((t = ret) is long)
			{
				long value8 = (long)((object)t);
				return new JsonPrimitive(value8);
			}
			if ((t = ret) is sbyte)
			{
				sbyte value9 = (sbyte)((object)t);
				return new JsonPrimitive(value9);
			}
			if ((t = ret) is short)
			{
				short value10 = (short)((object)t);
				return new JsonPrimitive(value10);
			}
			string value11;
			if ((value11 = (ret as string)) != null)
			{
				return new JsonPrimitive(value11);
			}
			if ((t = ret) is uint)
			{
				uint value12 = (uint)((object)t);
				return new JsonPrimitive(value12);
			}
			if ((t = ret) is ulong)
			{
				ulong value13 = (ulong)((object)t);
				return new JsonPrimitive(value13);
			}
			if ((t = ret) is ushort)
			{
				ushort value14 = (ushort)((object)t);
				return new JsonPrimitive(value14);
			}
			if ((t = ret) is DateTime)
			{
				DateTime value15 = (DateTime)((object)t);
				return new JsonPrimitive(value15);
			}
			if ((t = ret) is DateTimeOffset)
			{
				DateTimeOffset value16 = (DateTimeOffset)((object)t);
				return new JsonPrimitive(value16);
			}
			if ((t = ret) is Guid)
			{
				Guid value17 = (Guid)((object)t);
				return new JsonPrimitive(value17);
			}
			if ((t = ret) is TimeSpan)
			{
				TimeSpan value18 = (TimeSpan)((object)t);
				return new JsonPrimitive(value18);
			}
			Uri value19;
			if ((value19 = (ret as Uri)) != null)
			{
				return new JsonPrimitive(value19);
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

		// Token: 0x06000242 RID: 578 RVA: 0x00009758 File Offset: 0x00007958
		public static JsonValue Parse(string jsonString)
		{
			if (jsonString == null)
			{
				throw new ArgumentNullException("jsonString");
			}
			return JsonValue.Load(new StringReader(jsonString));
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000243 RID: 579 RVA: 0x00009773 File Offset: 0x00007973
		public virtual int Count
		{
			get
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000244 RID: 580
		public abstract JsonType JsonType { get; }

		// Token: 0x170000C1 RID: 193
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

		// Token: 0x170000C2 RID: 194
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

		// Token: 0x06000249 RID: 585 RVA: 0x00009773 File Offset: 0x00007973
		public virtual bool ContainsKey(string key)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000977A File Offset: 0x0000797A
		public virtual void Save(Stream stream, bool parsing)
		{
			if (stream == null)
			{
				throw new ArgumentNullException("stream");
			}
			this.Save(new StreamWriter(stream), parsing);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00009797 File Offset: 0x00007997
		public virtual void Save(TextWriter textWriter, bool parsing)
		{
			if (textWriter == null)
			{
				throw new ArgumentNullException("textWriter");
			}
			this.Savepublic(textWriter, parsing);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000097B0 File Offset: 0x000079B0
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
				bool flag = false;
				foreach (JsonValue jsonValue in ((IEnumerable<JsonValue>)((JsonArray)this)))
				{
					if (flag)
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
					flag = true;
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

		// Token: 0x0600024D RID: 589 RVA: 0x0000995C File Offset: 0x00007B5C
		public string ToString(bool saving = true)
		{
			StringWriter stringWriter = new StringWriter();
			this.Save(stringWriter, saving);
			return stringWriter.ToString();
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00009773 File Offset: 0x00007973
		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00009980 File Offset: 0x00007B80
		private bool NeedEscape(string src, int i)
		{
			char c = src[i];
			return c < ' ' || c == '"' || c == '\\' || (c >= '\ud800' && c <= '\udbff' && (i == src.Length - 1 || src[i + 1] < '\udc00' || src[i + 1] > '\udfff')) || (c >= '\udc00' && c <= '\udfff' && (i == 0 || src[i - 1] < '\ud800' || src[i - 1] > '\udbff')) || c == '\u2028' || c == '\u2029' || (c == '/' && i > 0 && src[i - 1] == '<');
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00009A48 File Offset: 0x00007C48
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

		// Token: 0x06000251 RID: 593 RVA: 0x00009A94 File Offset: 0x00007C94
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
							goto IL_D5;
						case '\f':
							sb.Append("\\f");
							break;
						case '\r':
							sb.Append("\\r");
							break;
						default:
							if (c != '"')
							{
								goto IL_D5;
							}
							sb.Append("\\\"");
							break;
						}
					}
					else if (c != '/')
					{
						if (c != '\\')
						{
							goto IL_D5;
						}
						sb.Append("\\\\");
					}
					else
					{
						sb.Append("\\/");
					}
					IL_FC:
					num = i + 1;
					goto IL_100;
					IL_D5:
					sb.Append("\\u");
					sb.Append(((int)src[i]).ToString("x04"));
					goto IL_FC;
				}
				IL_100:;
			}
			sb.Append(src, num, src.Length - num);
			return sb.ToString();
		}

		// Token: 0x06000252 RID: 594 RVA: 0x00009BC8 File Offset: 0x00007DC8
		public static implicit operator JsonValue(bool value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00009BD0 File Offset: 0x00007DD0
		public static implicit operator JsonValue(byte value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00009BD8 File Offset: 0x00007DD8
		public static implicit operator JsonValue(char value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00009BE0 File Offset: 0x00007DE0
		public static implicit operator JsonValue(decimal value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00009BE8 File Offset: 0x00007DE8
		public static implicit operator JsonValue(double value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00009BF0 File Offset: 0x00007DF0
		public static implicit operator JsonValue(float value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00009BF8 File Offset: 0x00007DF8
		public static implicit operator JsonValue(int value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00009C00 File Offset: 0x00007E00
		public static implicit operator JsonValue(long value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00009C08 File Offset: 0x00007E08
		public static implicit operator JsonValue(sbyte value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00009C10 File Offset: 0x00007E10
		public static implicit operator JsonValue(short value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00009C18 File Offset: 0x00007E18
		public static implicit operator JsonValue(string value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00009C20 File Offset: 0x00007E20
		public static implicit operator JsonValue(uint value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00009C28 File Offset: 0x00007E28
		public static implicit operator JsonValue(ulong value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00009C30 File Offset: 0x00007E30
		public static implicit operator JsonValue(ushort value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00009C38 File Offset: 0x00007E38
		public static implicit operator JsonValue(DateTime value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00009C40 File Offset: 0x00007E40
		public static implicit operator JsonValue(DateTimeOffset value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00009C48 File Offset: 0x00007E48
		public static implicit operator JsonValue(Guid value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00009C50 File Offset: 0x00007E50
		public static implicit operator JsonValue(TimeSpan value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00009C58 File Offset: 0x00007E58
		public static implicit operator JsonValue(Uri value)
		{
			return new JsonPrimitive(value);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00009C60 File Offset: 0x00007E60
		public static implicit operator bool(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToBoolean(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00009C85 File Offset: 0x00007E85
		public static implicit operator byte(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToByte(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00009CAA File Offset: 0x00007EAA
		public static implicit operator char(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToChar(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00009CCF File Offset: 0x00007ECF
		public static implicit operator decimal(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToDecimal(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00009CF4 File Offset: 0x00007EF4
		public static implicit operator double(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToDouble(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00009D19 File Offset: 0x00007F19
		public static implicit operator float(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToSingle(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x0600026B RID: 619 RVA: 0x00009D3E File Offset: 0x00007F3E
		public static implicit operator int(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToInt32(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x00009D63 File Offset: 0x00007F63
		public static implicit operator long(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToInt64(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x00009D88 File Offset: 0x00007F88
		public static implicit operator sbyte(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToSByte(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00009DAD File Offset: 0x00007FAD
		public static implicit operator short(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToInt16(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x00009DD2 File Offset: 0x00007FD2
		public static implicit operator string(JsonValue value)
		{
			if (value == null)
			{
				return null;
			}
			return value.ToString(true);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x00009DE0 File Offset: 0x00007FE0
		public static implicit operator uint(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToUInt32(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00009E05 File Offset: 0x00008005
		public static implicit operator ulong(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToUInt64(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00009E2A File Offset: 0x0000802A
		public static implicit operator ushort(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return Convert.ToUInt16(((JsonPrimitive)value).Value, NumberFormatInfo.InvariantInfo);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00009E4F File Offset: 0x0000804F
		public static implicit operator DateTime(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (DateTime)((JsonPrimitive)value).Value;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00009E6F File Offset: 0x0000806F
		public static implicit operator DateTimeOffset(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (DateTimeOffset)((JsonPrimitive)value).Value;
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00009E8F File Offset: 0x0000808F
		public static implicit operator TimeSpan(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (TimeSpan)((JsonPrimitive)value).Value;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x00009EAF File Offset: 0x000080AF
		public static implicit operator Guid(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (Guid)((JsonPrimitive)value).Value;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x00009ECF File Offset: 0x000080CF
		public static implicit operator Uri(JsonValue value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return (Uri)((JsonPrimitive)value).Value;
		}
	}
}
