using System;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x0200014B RID: 331
	[NullableContext(2)]
	[Nullable(0)]
	public class JsonTextWriter : JsonWriter
	{
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000BBF RID: 3007 RVA: 0x0004568C File Offset: 0x0004388C
		[Nullable(1)]
		private Base64Encoder Base64Encoder
		{
			[NullableContext(1)]
			get
			{
				if (this._base64Encoder == null)
				{
					this._base64Encoder = new Base64Encoder(this._writer);
				}
				return this._base64Encoder;
			}
		}

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000BC0 RID: 3008 RVA: 0x000456B0 File Offset: 0x000438B0
		// (set) Token: 0x06000BC1 RID: 3009 RVA: 0x000456B8 File Offset: 0x000438B8
		public IArrayPool<char> ArrayPool
		{
			get
			{
				return this._arrayPool;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._arrayPool = value;
			}
		}

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000BC2 RID: 3010 RVA: 0x000456D4 File Offset: 0x000438D4
		// (set) Token: 0x06000BC3 RID: 3011 RVA: 0x000456DC File Offset: 0x000438DC
		public int Indentation
		{
			get
			{
				return this._indentation;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("Indentation value must be greater than 0.");
				}
				this._indentation = value;
			}
		}

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000BC4 RID: 3012 RVA: 0x000456F8 File Offset: 0x000438F8
		// (set) Token: 0x06000BC5 RID: 3013 RVA: 0x00045700 File Offset: 0x00043900
		public char QuoteChar
		{
			get
			{
				return this._quoteChar;
			}
			set
			{
				if (value != '"' && value != '\'')
				{
					throw new ArgumentException("Invalid JavaScript string quote character. Valid quote characters are ' and \".");
				}
				this._quoteChar = value;
				this.UpdateCharEscapeFlags();
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000BC6 RID: 3014 RVA: 0x0004572C File Offset: 0x0004392C
		// (set) Token: 0x06000BC7 RID: 3015 RVA: 0x00045734 File Offset: 0x00043934
		public char IndentChar
		{
			get
			{
				return this._indentChar;
			}
			set
			{
				if (value != this._indentChar)
				{
					this._indentChar = value;
					this._indentChars = null;
				}
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000BC8 RID: 3016 RVA: 0x00045750 File Offset: 0x00043950
		// (set) Token: 0x06000BC9 RID: 3017 RVA: 0x00045758 File Offset: 0x00043958
		public bool QuoteName
		{
			get
			{
				return this._quoteName;
			}
			set
			{
				this._quoteName = value;
			}
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x00045764 File Offset: 0x00043964
		[NullableContext(1)]
		public JsonTextWriter(TextWriter textWriter)
		{
			if (textWriter == null)
			{
				throw new ArgumentNullException("textWriter");
			}
			this._writer = textWriter;
			this._quoteChar = '"';
			this._quoteName = true;
			this._indentChar = ' ';
			this._indentation = 2;
			this.UpdateCharEscapeFlags();
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x000457B8 File Offset: 0x000439B8
		public override void Flush()
		{
			this._writer.Flush();
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x000457C8 File Offset: 0x000439C8
		public override void Close()
		{
			base.Close();
			this.CloseBufferAndWriter();
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x000457D8 File Offset: 0x000439D8
		private void CloseBufferAndWriter()
		{
			if (this._writeBuffer != null)
			{
				BufferUtils.ReturnBuffer(this._arrayPool, this._writeBuffer);
				this._writeBuffer = null;
			}
			if (base.CloseOutput)
			{
				TextWriter writer = this._writer;
				if (writer == null)
				{
					return;
				}
				writer.Close();
			}
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0004582C File Offset: 0x00043A2C
		public override void WriteStartObject()
		{
			base.InternalWriteStart(JsonToken.StartObject, JsonContainerType.Object);
			this._writer.Write('{');
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x00045844 File Offset: 0x00043A44
		public override void WriteStartArray()
		{
			base.InternalWriteStart(JsonToken.StartArray, JsonContainerType.Array);
			this._writer.Write('[');
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0004585C File Offset: 0x00043A5C
		[NullableContext(1)]
		public override void WriteStartConstructor(string name)
		{
			base.InternalWriteStart(JsonToken.StartConstructor, JsonContainerType.Constructor);
			this._writer.Write("new ");
			this._writer.Write(name);
			this._writer.Write('(');
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x000458A0 File Offset: 0x00043AA0
		protected override void WriteEnd(JsonToken token)
		{
			switch (token)
			{
			case JsonToken.EndObject:
				this._writer.Write('}');
				return;
			case JsonToken.EndArray:
				this._writer.Write(']');
				return;
			case JsonToken.EndConstructor:
				this._writer.Write(')');
				return;
			default:
				throw JsonWriterException.Create(this, "Invalid JsonToken: " + token.ToString(), null);
			}
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x00045914 File Offset: 0x00043B14
		[NullableContext(1)]
		public override void WritePropertyName(string name)
		{
			base.InternalWritePropertyName(name);
			this.WriteEscapedString(name, this._quoteName);
			this._writer.Write(':');
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00045938 File Offset: 0x00043B38
		[NullableContext(1)]
		public override void WritePropertyName(string name, bool escape)
		{
			base.InternalWritePropertyName(name);
			if (escape)
			{
				this.WriteEscapedString(name, this._quoteName);
			}
			else
			{
				if (this._quoteName)
				{
					this._writer.Write(this._quoteChar);
				}
				this._writer.Write(name);
				if (this._quoteName)
				{
					this._writer.Write(this._quoteChar);
				}
			}
			this._writer.Write(':');
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x000459BC File Offset: 0x00043BBC
		internal override void OnStringEscapeHandlingChanged()
		{
			this.UpdateCharEscapeFlags();
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x000459C4 File Offset: 0x00043BC4
		private void UpdateCharEscapeFlags()
		{
			this._charEscapeFlags = JavaScriptUtils.GetCharEscapeFlags(base.StringEscapeHandling, this._quoteChar);
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x000459E0 File Offset: 0x00043BE0
		protected override void WriteIndent()
		{
			int num = base.Top * this._indentation;
			int num2 = this.SetIndentChars();
			this._writer.Write(this._indentChars, 0, num2 + Math.Min(num, 12));
			while ((num -= 12) > 0)
			{
				this._writer.Write(this._indentChars, num2, Math.Min(num, 12));
			}
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x00045A4C File Offset: 0x00043C4C
		private int SetIndentChars()
		{
			string newLine = this._writer.NewLine;
			int length = newLine.Length;
			bool flag = this._indentChars != null && this._indentChars.Length == 12 + length;
			if (flag)
			{
				for (int num = 0; num != length; num++)
				{
					if (newLine[num] != this._indentChars[num])
					{
						flag = false;
						break;
					}
				}
			}
			if (!flag)
			{
				this._indentChars = (newLine + new string(this._indentChar, 12)).ToCharArray();
			}
			return length;
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x00045AE8 File Offset: 0x00043CE8
		protected override void WriteValueDelimiter()
		{
			this._writer.Write(',');
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x00045AF8 File Offset: 0x00043CF8
		protected override void WriteIndentSpace()
		{
			this._writer.Write(' ');
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x00045B08 File Offset: 0x00043D08
		[NullableContext(1)]
		private void WriteValueInternal(string value, JsonToken token)
		{
			this._writer.Write(value);
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x00045B18 File Offset: 0x00043D18
		public override void WriteValue(object value)
		{
			if (value is System.Numerics.BigInteger)
			{
				System.Numerics.BigInteger bigInteger = (System.Numerics.BigInteger)value;
				base.InternalWriteValue(JsonToken.Integer);
				this.WriteValueInternal(bigInteger.ToString(CultureInfo.InvariantCulture), JsonToken.String);
				return;
			}
			base.WriteValue(value);
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00045B60 File Offset: 0x00043D60
		public override void WriteNull()
		{
			base.InternalWriteValue(JsonToken.Null);
			this.WriteValueInternal(JsonConvert.Null, JsonToken.Null);
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00045B78 File Offset: 0x00043D78
		public override void WriteUndefined()
		{
			base.InternalWriteValue(JsonToken.Undefined);
			this.WriteValueInternal(JsonConvert.Undefined, JsonToken.Undefined);
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x00045B90 File Offset: 0x00043D90
		public override void WriteRaw(string json)
		{
			base.InternalWriteRaw();
			this._writer.Write(json);
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00045BA4 File Offset: 0x00043DA4
		public override void WriteValue(string value)
		{
			base.InternalWriteValue(JsonToken.String);
			if (value == null)
			{
				this.WriteValueInternal(JsonConvert.Null, JsonToken.Null);
				return;
			}
			this.WriteEscapedString(value, true);
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00045BCC File Offset: 0x00043DCC
		[NullableContext(1)]
		private void WriteEscapedString(string value, bool quote)
		{
			this.EnsureWriteBuffer();
			JavaScriptUtils.WriteEscapedJavaScriptString(this._writer, value, this._quoteChar, quote, this._charEscapeFlags, base.StringEscapeHandling, this._arrayPool, ref this._writeBuffer);
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x00045C10 File Offset: 0x00043E10
		public override void WriteValue(int value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue(value);
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x00045C20 File Offset: 0x00043E20
		[CLSCompliant(false)]
		public override void WriteValue(uint value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((long)((ulong)value));
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x00045C34 File Offset: 0x00043E34
		public override void WriteValue(long value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue(value);
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x00045C44 File Offset: 0x00043E44
		[CLSCompliant(false)]
		public override void WriteValue(ulong value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue(value, false);
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00045C58 File Offset: 0x00043E58
		public override void WriteValue(float value)
		{
			base.InternalWriteValue(JsonToken.Float);
			this.WriteValueInternal(JsonConvert.ToString(value, base.FloatFormatHandling, this.QuoteChar, false), JsonToken.Float);
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00045C8C File Offset: 0x00043E8C
		public override void WriteValue(float? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.InternalWriteValue(JsonToken.Float);
			this.WriteValueInternal(JsonConvert.ToString(value.GetValueOrDefault(), base.FloatFormatHandling, this.QuoteChar, true), JsonToken.Float);
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x00045CD8 File Offset: 0x00043ED8
		public override void WriteValue(double value)
		{
			base.InternalWriteValue(JsonToken.Float);
			this.WriteValueInternal(JsonConvert.ToString(value, base.FloatFormatHandling, this.QuoteChar, false), JsonToken.Float);
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x00045D0C File Offset: 0x00043F0C
		public override void WriteValue(double? value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.InternalWriteValue(JsonToken.Float);
			this.WriteValueInternal(JsonConvert.ToString(value.GetValueOrDefault(), base.FloatFormatHandling, this.QuoteChar, true), JsonToken.Float);
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x00045D58 File Offset: 0x00043F58
		public override void WriteValue(bool value)
		{
			base.InternalWriteValue(JsonToken.Boolean);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.Boolean);
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x00045D70 File Offset: 0x00043F70
		public override void WriteValue(short value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((int)value);
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x00045D80 File Offset: 0x00043F80
		[CLSCompliant(false)]
		public override void WriteValue(ushort value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((int)value);
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x00045D90 File Offset: 0x00043F90
		public override void WriteValue(char value)
		{
			base.InternalWriteValue(JsonToken.String);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.String);
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x00045DA8 File Offset: 0x00043FA8
		public override void WriteValue(byte value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((int)value);
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x00045DB8 File Offset: 0x00043FB8
		[CLSCompliant(false)]
		public override void WriteValue(sbyte value)
		{
			base.InternalWriteValue(JsonToken.Integer);
			this.WriteIntegerValue((int)value);
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00045DC8 File Offset: 0x00043FC8
		public override void WriteValue(decimal value)
		{
			base.InternalWriteValue(JsonToken.Float);
			this.WriteValueInternal(JsonConvert.ToString(value), JsonToken.Float);
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00045DE0 File Offset: 0x00043FE0
		public override void WriteValue(DateTime value)
		{
			base.InternalWriteValue(JsonToken.Date);
			value = DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
			if (StringUtils.IsNullOrEmpty(base.DateFormatString))
			{
				int count = this.WriteValueToBuffer(value);
				this._writer.Write(this._writeBuffer, 0, count);
				return;
			}
			this._writer.Write(this._quoteChar);
			this._writer.Write(value.ToString(base.DateFormatString, base.Culture));
			this._writer.Write(this._quoteChar);
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x00045E74 File Offset: 0x00044074
		private int WriteValueToBuffer(DateTime value)
		{
			this.EnsureWriteBuffer();
			int num = 0;
			this._writeBuffer[num++] = this._quoteChar;
			num = DateTimeUtils.WriteDateTimeString(this._writeBuffer, num, value, null, value.Kind, base.DateFormatHandling);
			this._writeBuffer[num++] = this._quoteChar;
			return num;
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00045ED8 File Offset: 0x000440D8
		public override void WriteValue(byte[] value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.InternalWriteValue(JsonToken.Bytes);
			this._writer.Write(this._quoteChar);
			this.Base64Encoder.Encode(value, 0, value.Length);
			this.Base64Encoder.Flush();
			this._writer.Write(this._quoteChar);
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x00045F3C File Offset: 0x0004413C
		public override void WriteValue(DateTimeOffset value)
		{
			base.InternalWriteValue(JsonToken.Date);
			if (StringUtils.IsNullOrEmpty(base.DateFormatString))
			{
				int count = this.WriteValueToBuffer(value);
				this._writer.Write(this._writeBuffer, 0, count);
				return;
			}
			this._writer.Write(this._quoteChar);
			this._writer.Write(value.ToString(base.DateFormatString, base.Culture));
			this._writer.Write(this._quoteChar);
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00045FC4 File Offset: 0x000441C4
		private int WriteValueToBuffer(DateTimeOffset value)
		{
			this.EnsureWriteBuffer();
			int num = 0;
			this._writeBuffer[num++] = this._quoteChar;
			num = DateTimeUtils.WriteDateTimeString(this._writeBuffer, num, (base.DateFormatHandling == DateFormatHandling.IsoDateFormat) ? value.DateTime : value.UtcDateTime, new TimeSpan?(value.Offset), DateTimeKind.Local, base.DateFormatHandling);
			this._writeBuffer[num++] = this._quoteChar;
			return num;
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x00046040 File Offset: 0x00044240
		public override void WriteValue(Guid value)
		{
			base.InternalWriteValue(JsonToken.String);
			string value2 = value.ToString("D", CultureInfo.InvariantCulture);
			this._writer.Write(this._quoteChar);
			this._writer.Write(value2);
			this._writer.Write(this._quoteChar);
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x0004609C File Offset: 0x0004429C
		public override void WriteValue(TimeSpan value)
		{
			base.InternalWriteValue(JsonToken.String);
			string value2 = value.ToString(null, CultureInfo.InvariantCulture);
			this._writer.Write(this._quoteChar);
			this._writer.Write(value2);
			this._writer.Write(this._quoteChar);
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x000460F4 File Offset: 0x000442F4
		public override void WriteValue(Uri value)
		{
			if (value == null)
			{
				this.WriteNull();
				return;
			}
			base.InternalWriteValue(JsonToken.String);
			this.WriteEscapedString(value.OriginalString, true);
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x00046130 File Offset: 0x00044330
		public override void WriteComment(string text)
		{
			base.InternalWriteComment();
			this._writer.Write("/*");
			this._writer.Write(text);
			this._writer.Write("*/");
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x00046174 File Offset: 0x00044374
		[NullableContext(1)]
		public override void WriteWhitespace(string ws)
		{
			base.InternalWriteWhitespace(ws);
			this._writer.Write(ws);
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x0004618C File Offset: 0x0004438C
		private void EnsureWriteBuffer()
		{
			if (this._writeBuffer == null)
			{
				this._writeBuffer = BufferUtils.RentBuffer(this._arrayPool, 35);
			}
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x000461AC File Offset: 0x000443AC
		private void WriteIntegerValue(long value)
		{
			if (value >= 0L && value <= 9L)
			{
				this._writer.Write((char)(48L + value));
				return;
			}
			bool flag = value < 0L;
			this.WriteIntegerValue((ulong)(flag ? (-(ulong)value) : value), flag);
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x000461FC File Offset: 0x000443FC
		private void WriteIntegerValue(ulong value, bool negative)
		{
			if (!negative & value <= 9UL)
			{
				this._writer.Write((char)(48UL + value));
				return;
			}
			int count = this.WriteNumberToBuffer(value, negative);
			this._writer.Write(this._writeBuffer, 0, count);
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x00046250 File Offset: 0x00044450
		private int WriteNumberToBuffer(ulong value, bool negative)
		{
			if (value <= (ulong)-1)
			{
				return this.WriteNumberToBuffer((uint)value, negative);
			}
			this.EnsureWriteBuffer();
			int num = MathUtils.IntLength(value);
			if (negative)
			{
				num++;
				this._writeBuffer[0] = '-';
			}
			int num2 = num;
			do
			{
				ulong num3 = value / 10UL;
				ulong num4 = value - num3 * 10UL;
				this._writeBuffer[--num2] = (char)(48UL + num4);
				value = num3;
			}
			while (value != 0UL);
			return num;
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x000462C0 File Offset: 0x000444C0
		private void WriteIntegerValue(int value)
		{
			if (value >= 0 && value <= 9)
			{
				this._writer.Write((char)(48 + value));
				return;
			}
			bool flag = value < 0;
			this.WriteIntegerValue((uint)(flag ? (-(uint)value) : value), flag);
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x0004630C File Offset: 0x0004450C
		private void WriteIntegerValue(uint value, bool negative)
		{
			if (!negative & value <= 9u)
			{
				this._writer.Write((char)(48u + value));
				return;
			}
			int count = this.WriteNumberToBuffer(value, negative);
			this._writer.Write(this._writeBuffer, 0, count);
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x0004635C File Offset: 0x0004455C
		private int WriteNumberToBuffer(uint value, bool negative)
		{
			this.EnsureWriteBuffer();
			int num = MathUtils.IntLength((ulong)value);
			if (negative)
			{
				num++;
				this._writeBuffer[0] = '-';
			}
			int num2 = num;
			do
			{
				uint num3 = value / 10u;
				uint num4 = value - num3 * 10u;
				this._writeBuffer[--num2] = (char)(48u + num4);
				value = num3;
			}
			while (value != 0u);
			return num;
		}

		// Token: 0x04000659 RID: 1625
		private const int IndentCharBufferSize = 12;

		// Token: 0x0400065A RID: 1626
		[Nullable(1)]
		private readonly TextWriter _writer;

		// Token: 0x0400065B RID: 1627
		private Base64Encoder _base64Encoder;

		// Token: 0x0400065C RID: 1628
		private char _indentChar;

		// Token: 0x0400065D RID: 1629
		private int _indentation;

		// Token: 0x0400065E RID: 1630
		private char _quoteChar;

		// Token: 0x0400065F RID: 1631
		private bool _quoteName;

		// Token: 0x04000660 RID: 1632
		private bool[] _charEscapeFlags;

		// Token: 0x04000661 RID: 1633
		private char[] _writeBuffer;

		// Token: 0x04000662 RID: 1634
		private IArrayPool<char> _arrayPool;

		// Token: 0x04000663 RID: 1635
		private char[] _indentChars;
	}
}
