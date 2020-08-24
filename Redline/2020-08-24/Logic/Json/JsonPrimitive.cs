using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace RedLine.Logic.Json
{
	// Token: 0x02000054 RID: 84
	public class JsonPrimitive : JsonValue
	{
		// Token: 0x06000224 RID: 548 RVA: 0x00008FB7 File Offset: 0x000071B7
		public JsonPrimitive(bool value)
		{
			this.value = value;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x00008FCB File Offset: 0x000071CB
		public JsonPrimitive(byte value)
		{
			this.value = value;
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00008FDF File Offset: 0x000071DF
		public JsonPrimitive(char value)
		{
			this.value = value;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00008FF3 File Offset: 0x000071F3
		public JsonPrimitive(decimal value)
		{
			this.value = value;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x00009007 File Offset: 0x00007207
		public JsonPrimitive(double value)
		{
			this.value = value;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000901B File Offset: 0x0000721B
		public JsonPrimitive(float value)
		{
			this.value = value;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000902F File Offset: 0x0000722F
		public JsonPrimitive(int value)
		{
			this.value = value;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00009043 File Offset: 0x00007243
		public JsonPrimitive(long value)
		{
			this.value = value;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00009057 File Offset: 0x00007257
		public JsonPrimitive(sbyte value)
		{
			this.value = value;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000906B File Offset: 0x0000726B
		public JsonPrimitive(short value)
		{
			this.value = value;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000907F File Offset: 0x0000727F
		public JsonPrimitive(string value)
		{
			this.value = value;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x0000908E File Offset: 0x0000728E
		public JsonPrimitive(DateTime value)
		{
			this.value = value;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000090A2 File Offset: 0x000072A2
		public JsonPrimitive(uint value)
		{
			this.value = value;
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000090B6 File Offset: 0x000072B6
		public JsonPrimitive(ulong value)
		{
			this.value = value;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000090CA File Offset: 0x000072CA
		public JsonPrimitive(ushort value)
		{
			this.value = value;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000090DE File Offset: 0x000072DE
		public JsonPrimitive(DateTimeOffset value)
		{
			this.value = value;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x000090F2 File Offset: 0x000072F2
		public JsonPrimitive(Guid value)
		{
			this.value = value;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00009106 File Offset: 0x00007306
		public JsonPrimitive(TimeSpan value)
		{
			this.value = value;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000907F File Offset: 0x0000727F
		public JsonPrimitive(Uri value)
		{
			this.value = value;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000907F File Offset: 0x0000727F
		public JsonPrimitive(object value)
		{
			this.value = value;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000238 RID: 568 RVA: 0x0000911A File Offset: 0x0000731A
		public object Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000239 RID: 569 RVA: 0x00009124 File Offset: 0x00007324
		public override JsonType JsonType
		{
			get
			{
				if (this.value == null)
				{
					return JsonType.String;
				}
				TypeCode typeCode = Type.GetTypeCode(this.value.GetType());
				switch (typeCode)
				{
				case TypeCode.Object:
				case TypeCode.Char:
					break;
				case TypeCode.DBNull:
					return JsonType.Number;
				case TypeCode.Boolean:
					return JsonType.Boolean;
				default:
					if (typeCode != TypeCode.DateTime && typeCode != TypeCode.String)
					{
						return JsonType.Number;
					}
					break;
				}
				return JsonType.String;
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00009178 File Offset: 0x00007378
		public override void Save(Stream stream, bool parsing)
		{
			JsonType jsonType = this.JsonType;
			if (jsonType == JsonType.String)
			{
				stream.WriteByte(34);
				byte[] bytes = Encoding.UTF8.GetBytes(base.EscapeString(this.value.ToString()));
				stream.Write(bytes, 0, bytes.Length);
				stream.WriteByte(34);
				return;
			}
			if (jsonType != JsonType.Boolean)
			{
				byte[] bytes = Encoding.UTF8.GetBytes(this.GetFormattedString());
				stream.Write(bytes, 0, bytes.Length);
				return;
			}
			if ((bool)this.value)
			{
				stream.Write(JsonPrimitive.true_bytes, 0, 4);
				return;
			}
			stream.Write(JsonPrimitive.false_bytes, 0, 5);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00009210 File Offset: 0x00007410
		public string GetFormattedString()
		{
			JsonType jsonType = this.JsonType;
			if (jsonType != JsonType.String)
			{
				if (jsonType != JsonType.Number)
				{
					throw new InvalidOperationException();
				}
				string text;
				if (this.value is float || this.value is double)
				{
					text = ((IFormattable)this.value).ToString("R", NumberFormatInfo.InvariantInfo);
				}
				else
				{
					text = ((IFormattable)this.value).ToString("G", NumberFormatInfo.InvariantInfo);
				}
				if (text == "NaN" || text == "Infinity" || text == "-Infinity")
				{
					return "\"" + text + "\"";
				}
				return text;
			}
			else if (this.value is string || this.value == null)
			{
				string text2 = this.value as string;
				if (string.IsNullOrEmpty(text2))
				{
					return "null";
				}
				return text2.Trim(new char[]
				{
					'"'
				});
			}
			else
			{
				if (this.value is char)
				{
					return this.value.ToString();
				}
				throw new NotImplementedException("GetFormattedString from value type " + this.value.GetType());
			}
		}

		// Token: 0x0400012B RID: 299
		private object value;

		// Token: 0x0400012C RID: 300
		private static readonly byte[] true_bytes = Encoding.UTF8.GetBytes("true");

		// Token: 0x0400012D RID: 301
		private static readonly byte[] false_bytes = Encoding.UTF8.GetBytes("false");
	}
}
