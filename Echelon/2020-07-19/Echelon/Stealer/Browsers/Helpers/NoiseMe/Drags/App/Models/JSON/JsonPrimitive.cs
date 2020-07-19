using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Echelon.Stealer.Browsers.Helpers.NoiseMe.Drags.App.Models.JSON
{
	// Token: 0x02000032 RID: 50
	public class JsonPrimitive : JsonValue
	{
		// Token: 0x060000FA RID: 250 RVA: 0x00009AC8 File Offset: 0x00007CC8
		public JsonPrimitive(bool value)
		{
			this.Value = value;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00009ADC File Offset: 0x00007CDC
		public JsonPrimitive(byte value)
		{
			this.Value = value;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00009AF0 File Offset: 0x00007CF0
		public JsonPrimitive(char value)
		{
			this.Value = value;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00009B04 File Offset: 0x00007D04
		public JsonPrimitive(decimal value)
		{
			this.Value = value;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00009B18 File Offset: 0x00007D18
		public JsonPrimitive(double value)
		{
			this.Value = value;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00009B2C File Offset: 0x00007D2C
		public JsonPrimitive(float value)
		{
			this.Value = value;
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00009B40 File Offset: 0x00007D40
		public JsonPrimitive(int value)
		{
			this.Value = value;
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00009B54 File Offset: 0x00007D54
		public JsonPrimitive(long value)
		{
			this.Value = value;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00009B68 File Offset: 0x00007D68
		public JsonPrimitive(sbyte value)
		{
			this.Value = value;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00009B7C File Offset: 0x00007D7C
		public JsonPrimitive(short value)
		{
			this.Value = value;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00009B90 File Offset: 0x00007D90
		public JsonPrimitive(string value)
		{
			this.Value = value;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00009BA0 File Offset: 0x00007DA0
		public JsonPrimitive(DateTime value)
		{
			this.Value = value;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00009BB4 File Offset: 0x00007DB4
		public JsonPrimitive(uint value)
		{
			this.Value = value;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00009BC8 File Offset: 0x00007DC8
		public JsonPrimitive(ulong value)
		{
			this.Value = value;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00009BDC File Offset: 0x00007DDC
		public JsonPrimitive(ushort value)
		{
			this.Value = value;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00009BF0 File Offset: 0x00007DF0
		public JsonPrimitive(DateTimeOffset value)
		{
			this.Value = value;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00009C04 File Offset: 0x00007E04
		public JsonPrimitive(Guid value)
		{
			this.Value = value;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00009C18 File Offset: 0x00007E18
		public JsonPrimitive(TimeSpan value)
		{
			this.Value = value;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00009C2C File Offset: 0x00007E2C
		public JsonPrimitive(Uri value)
		{
			this.Value = value;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00009C3C File Offset: 0x00007E3C
		public JsonPrimitive(object value)
		{
			this.Value = value;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600010E RID: 270 RVA: 0x00009C4C File Offset: 0x00007E4C
		public object Value { get; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00009C54 File Offset: 0x00007E54
		public override JsonType JsonType
		{
			get
			{
				if (this.Value == null)
				{
					return JsonType.String;
				}
				TypeCode typeCode = Type.GetTypeCode(this.Value.GetType());
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

		// Token: 0x06000110 RID: 272 RVA: 0x00009CB8 File Offset: 0x00007EB8
		public override void Save(Stream stream, bool parsing)
		{
			JsonType jsonType = this.JsonType;
			if (jsonType == JsonType.String)
			{
				stream.WriteByte(34);
				byte[] bytes = Encoding.UTF8.GetBytes(base.EscapeString(this.Value.ToString()));
				stream.Write(bytes, 0, bytes.Length);
				stream.WriteByte(34);
				return;
			}
			if (jsonType != JsonType.Boolean)
			{
				byte[] bytes2 = Encoding.UTF8.GetBytes(this.GetFormattedString());
				stream.Write(bytes2, 0, bytes2.Length);
				return;
			}
			if ((bool)this.Value)
			{
				stream.Write(JsonPrimitive.true_bytes, 0, 4);
				return;
			}
			stream.Write(JsonPrimitive.false_bytes, 0, 5);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00009D60 File Offset: 0x00007F60
		public string GetFormattedString()
		{
			JsonType jsonType = this.JsonType;
			if (jsonType != JsonType.String)
			{
				if (jsonType != JsonType.Number)
				{
					throw new InvalidOperationException();
				}
				string text = (!(this.Value is float) && !(this.Value is double)) ? ((IFormattable)this.Value).ToString("G", NumberFormatInfo.InvariantInfo) : ((IFormattable)this.Value).ToString("R", NumberFormatInfo.InvariantInfo);
				if (text == "NaN" || text == "Infinity" || text == "-Infinity")
				{
					return "\"" + text + "\"";
				}
				return text;
			}
			else if (this.Value is string || this.Value == null)
			{
				string text2 = this.Value as string;
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
				if (this.Value is char)
				{
					return this.Value.ToString();
				}
				string str = "GetFormattedString from value type ";
				Type type = this.Value.GetType();
				throw new NotImplementedException(str + ((type != null) ? type.ToString() : null));
			}
		}

		// Token: 0x04000064 RID: 100
		private static readonly byte[] true_bytes = Encoding.UTF8.GetBytes("true");

		// Token: 0x04000065 RID: 101
		private static readonly byte[] false_bytes = Encoding.UTF8.GetBytes("false");
	}
}
