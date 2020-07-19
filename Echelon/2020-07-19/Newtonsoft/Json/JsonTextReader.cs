using System;
using System.Globalization;
using System.IO;
using System.Numerics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x0200014A RID: 330
	[NullableContext(1)]
	[Nullable(0)]
	public class JsonTextReader : JsonReader, IJsonLineInfo
	{
		// Token: 0x06000B70 RID: 2928 RVA: 0x0004215C File Offset: 0x0004035C
		public JsonTextReader(TextReader reader)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			this._reader = reader;
			this._lineNumber = 1;
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000B71 RID: 2929 RVA: 0x00042184 File Offset: 0x00040384
		// (set) Token: 0x06000B72 RID: 2930 RVA: 0x0004218C File Offset: 0x0004038C
		[Nullable(2)]
		public JsonNameTable PropertyNameTable { [NullableContext(2)] get; [NullableContext(2)] set; }

		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06000B73 RID: 2931 RVA: 0x00042198 File Offset: 0x00040398
		// (set) Token: 0x06000B74 RID: 2932 RVA: 0x000421A0 File Offset: 0x000403A0
		[Nullable(2)]
		public IArrayPool<char> ArrayPool
		{
			[NullableContext(2)]
			get
			{
				return this._arrayPool;
			}
			[NullableContext(2)]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._arrayPool = value;
			}
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x000421BC File Offset: 0x000403BC
		private void EnsureBufferNotEmpty()
		{
			if (this._stringBuffer.IsEmpty)
			{
				this._stringBuffer = new StringBuffer(this._arrayPool, 1024);
			}
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x000421E4 File Offset: 0x000403E4
		private void SetNewLine(bool hasNextChar)
		{
			if (hasNextChar && this._chars[this._charPos] == '\n')
			{
				this._charPos++;
			}
			this.OnNewLine(this._charPos);
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0004221C File Offset: 0x0004041C
		private void OnNewLine(int pos)
		{
			this._lineNumber++;
			this._lineStartPos = pos;
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x00042234 File Offset: 0x00040434
		private void ParseString(char quote, ReadType readType)
		{
			this._charPos++;
			this.ShiftBufferIfNeeded();
			this.ReadStringIntoBuffer(quote);
			this.ParseReadString(quote, readType);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0004225C File Offset: 0x0004045C
		private void ParseReadString(char quote, ReadType readType)
		{
			base.SetPostValueState(true);
			switch (readType)
			{
			case ReadType.ReadAsInt32:
			case ReadType.ReadAsDecimal:
			case ReadType.ReadAsBoolean:
				return;
			case ReadType.ReadAsBytes:
			{
				byte[] value;
				Guid guid;
				if (this._stringReference.Length == 0)
				{
					value = CollectionUtils.ArrayEmpty<byte>();
				}
				else if (this._stringReference.Length == 36 && ConvertUtils.TryConvertGuid(this._stringReference.ToString(), out guid))
				{
					value = guid.ToByteArray();
				}
				else
				{
					value = Convert.FromBase64CharArray(this._stringReference.Chars, this._stringReference.StartIndex, this._stringReference.Length);
				}
				base.SetToken(JsonToken.Bytes, value, false);
				return;
			}
			case ReadType.ReadAsString:
			{
				string value2 = this._stringReference.ToString();
				base.SetToken(JsonToken.String, value2, false);
				this._quoteChar = quote;
				return;
			}
			}
			if (this._dateParseHandling != DateParseHandling.None)
			{
				DateParseHandling dateParseHandling;
				if (readType == ReadType.ReadAsDateTime)
				{
					dateParseHandling = DateParseHandling.DateTime;
				}
				else if (readType == ReadType.ReadAsDateTimeOffset)
				{
					dateParseHandling = DateParseHandling.DateTimeOffset;
				}
				else
				{
					dateParseHandling = this._dateParseHandling;
				}
				DateTimeOffset dateTimeOffset;
				if (dateParseHandling == DateParseHandling.DateTime)
				{
					DateTime dateTime;
					if (DateTimeUtils.TryParseDateTime(this._stringReference, base.DateTimeZoneHandling, base.DateFormatString, base.Culture, out dateTime))
					{
						base.SetToken(JsonToken.Date, dateTime, false);
						return;
					}
				}
				else if (DateTimeUtils.TryParseDateTimeOffset(this._stringReference, base.DateFormatString, base.Culture, out dateTimeOffset))
				{
					base.SetToken(JsonToken.Date, dateTimeOffset, false);
					return;
				}
			}
			base.SetToken(JsonToken.String, this._stringReference.ToString(), false);
			this._quoteChar = quote;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0004240C File Offset: 0x0004060C
		private static void BlockCopyChars(char[] src, int srcOffset, char[] dst, int dstOffset, int count)
		{
			Buffer.BlockCopy(src, srcOffset * 2, dst, dstOffset * 2, count * 2);
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x00042420 File Offset: 0x00040620
		private void ShiftBufferIfNeeded()
		{
			int num = this._chars.Length;
			if ((double)(num - this._charPos) <= (double)num * 0.1 || num >= 1073741823)
			{
				int num2 = this._charsUsed - this._charPos;
				if (num2 > 0)
				{
					JsonTextReader.BlockCopyChars(this._chars, this._charPos, this._chars, 0, num2);
				}
				this._lineStartPos -= this._charPos;
				this._charPos = 0;
				this._charsUsed = num2;
				this._chars[this._charsUsed] = '\0';
			}
		}

		// Token: 0x06000B7C RID: 2940 RVA: 0x000424BC File Offset: 0x000406BC
		private int ReadData(bool append)
		{
			return this.ReadData(append, 0);
		}

		// Token: 0x06000B7D RID: 2941 RVA: 0x000424C8 File Offset: 0x000406C8
		private void PrepareBufferForReadData(bool append, int charsRequired)
		{
			if (this._charsUsed + charsRequired >= this._chars.Length - 1)
			{
				if (append)
				{
					int num = this._chars.Length * 2;
					int minSize = Math.Max((num < 0) ? int.MaxValue : num, this._charsUsed + charsRequired + 1);
					char[] array = BufferUtils.RentBuffer(this._arrayPool, minSize);
					JsonTextReader.BlockCopyChars(this._chars, 0, array, 0, this._chars.Length);
					BufferUtils.ReturnBuffer(this._arrayPool, this._chars);
					this._chars = array;
					return;
				}
				int num2 = this._charsUsed - this._charPos;
				if (num2 + charsRequired + 1 >= this._chars.Length)
				{
					char[] array2 = BufferUtils.RentBuffer(this._arrayPool, num2 + charsRequired + 1);
					if (num2 > 0)
					{
						JsonTextReader.BlockCopyChars(this._chars, this._charPos, array2, 0, num2);
					}
					BufferUtils.ReturnBuffer(this._arrayPool, this._chars);
					this._chars = array2;
				}
				else if (num2 > 0)
				{
					JsonTextReader.BlockCopyChars(this._chars, this._charPos, this._chars, 0, num2);
				}
				this._lineStartPos -= this._charPos;
				this._charPos = 0;
				this._charsUsed = num2;
			}
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x0004260C File Offset: 0x0004080C
		private int ReadData(bool append, int charsRequired)
		{
			if (this._isEndOfFile)
			{
				return 0;
			}
			this.PrepareBufferForReadData(append, charsRequired);
			int count = this._chars.Length - this._charsUsed - 1;
			int num = this._reader.Read(this._chars, this._charsUsed, count);
			this._charsUsed += num;
			if (num == 0)
			{
				this._isEndOfFile = true;
			}
			this._chars[this._charsUsed] = '\0';
			return num;
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00042688 File Offset: 0x00040888
		private bool EnsureChars(int relativePosition, bool append)
		{
			return this._charPos + relativePosition < this._charsUsed || this.ReadChars(relativePosition, append);
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x000426A8 File Offset: 0x000408A8
		private bool ReadChars(int relativePosition, bool append)
		{
			if (this._isEndOfFile)
			{
				return false;
			}
			int num = this._charPos + relativePosition - this._charsUsed + 1;
			int num2 = 0;
			do
			{
				int num3 = this.ReadData(append, num - num2);
				if (num3 == 0)
				{
					break;
				}
				num2 += num3;
			}
			while (num2 < num);
			return num2 >= num;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x00042700 File Offset: 0x00040900
		public override bool Read()
		{
			this.EnsureBuffer();
			for (;;)
			{
				switch (this._currentState)
				{
				case JsonReader.State.Start:
				case JsonReader.State.Property:
				case JsonReader.State.ArrayStart:
				case JsonReader.State.Array:
				case JsonReader.State.ConstructorStart:
				case JsonReader.State.Constructor:
					goto IL_4C;
				case JsonReader.State.ObjectStart:
				case JsonReader.State.Object:
					goto IL_53;
				case JsonReader.State.PostValue:
					if (this.ParsePostValue(false))
					{
						return true;
					}
					continue;
				case JsonReader.State.Finished:
					goto IL_65;
				}
				break;
			}
			goto IL_DA;
			IL_4C:
			return this.ParseValue();
			IL_53:
			return this.ParseObject();
			IL_65:
			if (!this.EnsureChars(0, false))
			{
				base.SetToken(JsonToken.None);
				return false;
			}
			this.EatWhitespace();
			if (this._isEndOfFile)
			{
				base.SetToken(JsonToken.None);
				return false;
			}
			if (this._chars[this._charPos] == '/')
			{
				this.ParseComment(true);
				return true;
			}
			throw JsonReaderException.Create(this, "Additional text encountered after finished reading JSON content: {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
			IL_DA:
			throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x0004280C File Offset: 0x00040A0C
		public override int? ReadAsInt32()
		{
			return (int?)this.ReadNumberValue(ReadType.ReadAsInt32);
		}

		// Token: 0x06000B83 RID: 2947 RVA: 0x0004281C File Offset: 0x00040A1C
		public override DateTime? ReadAsDateTime()
		{
			return (DateTime?)this.ReadStringValue(ReadType.ReadAsDateTime);
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x0004282C File Offset: 0x00040A2C
		[NullableContext(2)]
		public override string ReadAsString()
		{
			return (string)this.ReadStringValue(ReadType.ReadAsString);
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x0004283C File Offset: 0x00040A3C
		[NullableContext(2)]
		public override byte[] ReadAsBytes()
		{
			this.EnsureBuffer();
			bool flag = false;
			switch (this._currentState)
			{
			case JsonReader.State.Start:
			case JsonReader.State.Property:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.Array:
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
				break;
			case JsonReader.State.Complete:
			case JsonReader.State.ObjectStart:
			case JsonReader.State.Object:
			case JsonReader.State.Closed:
			case JsonReader.State.Error:
				goto IL_265;
			case JsonReader.State.PostValue:
				if (this.ParsePostValue(true))
				{
					return null;
				}
				break;
			case JsonReader.State.Finished:
				this.ReadFinished();
				return null;
			default:
				goto IL_265;
			}
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= '\'')
				{
					if (c <= '\r')
					{
						if (c != '\0')
						{
							switch (c)
							{
							case '\t':
								break;
							case '\n':
								this.ProcessLineFeed();
								continue;
							case '\v':
							case '\f':
								goto IL_23C;
							case '\r':
								this.ProcessCarriageReturn(false);
								continue;
							default:
								goto IL_23C;
							}
						}
						else
						{
							if (this.ReadNullChar())
							{
								break;
							}
							continue;
						}
					}
					else if (c != ' ')
					{
						if (c != '"' && c != '\'')
						{
							goto IL_23C;
						}
						goto IL_117;
					}
					this._charPos++;
					continue;
				}
				if (c <= '[')
				{
					if (c == ',')
					{
						this.ProcessValueComma();
						continue;
					}
					if (c == '/')
					{
						this.ParseComment(false);
						continue;
					}
					if (c == '[')
					{
						goto IL_193;
					}
				}
				else
				{
					if (c == ']')
					{
						goto IL_1CE;
					}
					if (c == 'n')
					{
						goto IL_1AF;
					}
					if (c == '{')
					{
						this._charPos++;
						base.SetToken(JsonToken.StartObject);
						base.ReadIntoWrappedTypeObject();
						flag = true;
						continue;
					}
				}
				IL_23C:
				this._charPos++;
				if (!char.IsWhiteSpace(c))
				{
					goto Block_22;
				}
			}
			base.SetToken(JsonToken.None, null, false);
			return null;
			IL_117:
			this.ParseString(c, ReadType.ReadAsBytes);
			byte[] array = (byte[])this.Value;
			if (flag)
			{
				base.ReaderReadAndAssert();
				if (this.TokenType != JsonToken.EndObject)
				{
					throw JsonReaderException.Create(this, "Error reading bytes. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, this.TokenType));
				}
				base.SetToken(JsonToken.Bytes, array, false);
			}
			return array;
			IL_193:
			this._charPos++;
			base.SetToken(JsonToken.StartArray);
			return base.ReadArrayIntoByteArray();
			IL_1AF:
			this.HandleNull();
			return null;
			IL_1CE:
			this._charPos++;
			if (this._currentState == JsonReader.State.Array || this._currentState == JsonReader.State.ArrayStart || this._currentState == JsonReader.State.PostValue)
			{
				base.SetToken(JsonToken.EndArray);
				return null;
			}
			throw this.CreateUnexpectedCharacterException(c);
			Block_22:
			throw this.CreateUnexpectedCharacterException(c);
			IL_265:
			throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x00042AD4 File Offset: 0x00040CD4
		[NullableContext(2)]
		private object ReadStringValue(ReadType readType)
		{
			this.EnsureBuffer();
			switch (this._currentState)
			{
			case JsonReader.State.Start:
			case JsonReader.State.Property:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.Array:
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
				break;
			case JsonReader.State.Complete:
			case JsonReader.State.ObjectStart:
			case JsonReader.State.Object:
			case JsonReader.State.Closed:
			case JsonReader.State.Error:
				goto IL_308;
			case JsonReader.State.PostValue:
				if (this.ParsePostValue(true))
				{
					return null;
				}
				break;
			case JsonReader.State.Finished:
				this.ReadFinished();
				return null;
			default:
				goto IL_308;
			}
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= 'I')
				{
					if (c <= '\r')
					{
						if (c != '\0')
						{
							switch (c)
							{
							case '\t':
								break;
							case '\n':
								this.ProcessLineFeed();
								continue;
							case '\v':
							case '\f':
								goto IL_2DF;
							case '\r':
								this.ProcessCarriageReturn(false);
								continue;
							default:
								goto IL_2DF;
							}
						}
						else
						{
							if (this.ReadNullChar())
							{
								break;
							}
							continue;
						}
					}
					else
					{
						switch (c)
						{
						case ' ':
							break;
						case '!':
						case '#':
						case '$':
						case '%':
						case '&':
						case '(':
						case ')':
						case '*':
						case '+':
							goto IL_2DF;
						case '"':
						case '\'':
							goto IL_16E;
						case ',':
							this.ProcessValueComma();
							continue;
						case '-':
							goto IL_17E;
						case '.':
						case '0':
						case '1':
						case '2':
						case '3':
						case '4':
						case '5':
						case '6':
						case '7':
						case '8':
						case '9':
							goto IL_1B7;
						case '/':
							this.ParseComment(false);
							continue;
						default:
							if (c != 'I')
							{
								goto IL_2DF;
							}
							goto IL_242;
						}
					}
					this._charPos++;
					continue;
				}
				if (c <= ']')
				{
					if (c == 'N')
					{
						goto IL_24A;
					}
					if (c == ']')
					{
						goto IL_271;
					}
				}
				else
				{
					if (c == 'f')
					{
						goto IL_1E2;
					}
					if (c == 'n')
					{
						goto IL_252;
					}
					if (c == 't')
					{
						goto IL_1E2;
					}
				}
				IL_2DF:
				this._charPos++;
				if (!char.IsWhiteSpace(c))
				{
					goto Block_24;
				}
			}
			base.SetToken(JsonToken.None, null, false);
			return null;
			IL_16E:
			this.ParseString(c, readType);
			return this.FinishReadQuotedStringValue(readType);
			IL_17E:
			if (this.EnsureChars(1, true) && this._chars[this._charPos + 1] == 'I')
			{
				return this.ParseNumberNegativeInfinity(readType);
			}
			this.ParseNumber(readType);
			return this.Value;
			IL_1B7:
			if (readType != ReadType.ReadAsString)
			{
				this._charPos++;
				throw this.CreateUnexpectedCharacterException(c);
			}
			this.ParseNumber(ReadType.ReadAsString);
			return this.Value;
			IL_1E2:
			if (readType != ReadType.ReadAsString)
			{
				this._charPos++;
				throw this.CreateUnexpectedCharacterException(c);
			}
			string text = (c == 't') ? JsonConvert.True : JsonConvert.False;
			if (!this.MatchValueWithTrailingSeparator(text))
			{
				throw this.CreateUnexpectedCharacterException(this._chars[this._charPos]);
			}
			base.SetToken(JsonToken.String, text);
			return text;
			IL_242:
			return this.ParseNumberPositiveInfinity(readType);
			IL_24A:
			return this.ParseNumberNaN(readType);
			IL_252:
			this.HandleNull();
			return null;
			IL_271:
			this._charPos++;
			if (this._currentState == JsonReader.State.Array || this._currentState == JsonReader.State.ArrayStart || this._currentState == JsonReader.State.PostValue)
			{
				base.SetToken(JsonToken.EndArray);
				return null;
			}
			throw this.CreateUnexpectedCharacterException(c);
			Block_24:
			throw this.CreateUnexpectedCharacterException(c);
			IL_308:
			throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x00042E10 File Offset: 0x00041010
		[NullableContext(2)]
		private object FinishReadQuotedStringValue(ReadType readType)
		{
			switch (readType)
			{
			case ReadType.ReadAsBytes:
			case ReadType.ReadAsString:
				return this.Value;
			case ReadType.ReadAsDateTime:
			{
				object value = this.Value;
				if (value is DateTime)
				{
					DateTime dateTime = (DateTime)value;
					return dateTime;
				}
				return base.ReadDateTimeString((string)this.Value);
			}
			case ReadType.ReadAsDateTimeOffset:
			{
				object value = this.Value;
				if (value is DateTimeOffset)
				{
					DateTimeOffset dateTimeOffset = (DateTimeOffset)value;
					return dateTimeOffset;
				}
				return base.ReadDateTimeOffsetString((string)this.Value);
			}
			}
			throw new ArgumentOutOfRangeException("readType");
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x00042EC4 File Offset: 0x000410C4
		private JsonReaderException CreateUnexpectedCharacterException(char c)
		{
			return JsonReaderException.Create(this, "Unexpected character encountered while parsing value: {0}.".FormatWith(CultureInfo.InvariantCulture, c));
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x00042EE4 File Offset: 0x000410E4
		public override bool? ReadAsBoolean()
		{
			this.EnsureBuffer();
			switch (this._currentState)
			{
			case JsonReader.State.Start:
			case JsonReader.State.Property:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.Array:
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
				break;
			case JsonReader.State.Complete:
			case JsonReader.State.ObjectStart:
			case JsonReader.State.Object:
			case JsonReader.State.Closed:
			case JsonReader.State.Error:
				goto IL_301;
			case JsonReader.State.PostValue:
				if (this.ParsePostValue(true))
				{
					return null;
				}
				break;
			case JsonReader.State.Finished:
				this.ReadFinished();
				return null;
			default:
				goto IL_301;
			}
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= '9')
				{
					if (c != '\0')
					{
						switch (c)
						{
						case '\t':
							break;
						case '\n':
							this.ProcessLineFeed();
							continue;
						case '\v':
						case '\f':
							goto IL_2D0;
						case '\r':
							this.ProcessCarriageReturn(false);
							continue;
						default:
							switch (c)
							{
							case ' ':
								break;
							case '!':
							case '#':
							case '$':
							case '%':
							case '&':
							case '(':
							case ')':
							case '*':
							case '+':
								goto IL_2D0;
							case '"':
							case '\'':
								goto IL_161;
							case ',':
								this.ProcessValueComma();
								continue;
							case '-':
							case '.':
							case '0':
							case '1':
							case '2':
							case '3':
							case '4':
							case '5':
							case '6':
							case '7':
							case '8':
							case '9':
								goto IL_191;
							case '/':
								this.ParseComment(false);
								continue;
							default:
								goto IL_2D0;
							}
							break;
						}
						this._charPos++;
						continue;
					}
					if (this.ReadNullChar())
					{
						break;
					}
					continue;
				}
				else if (c <= 'f')
				{
					if (c == ']')
					{
						goto IL_25A;
					}
					if (c == 'f')
					{
						goto IL_1EC;
					}
				}
				else
				{
					if (c == 'n')
					{
						goto IL_181;
					}
					if (c == 't')
					{
						goto IL_1EC;
					}
				}
				IL_2D0:
				this._charPos++;
				if (!char.IsWhiteSpace(c))
				{
					goto Block_18;
				}
			}
			base.SetToken(JsonToken.None, null, false);
			return null;
			IL_161:
			this.ParseString(c, ReadType.Read);
			return base.ReadBooleanString(this._stringReference.ToString());
			IL_181:
			this.HandleNull();
			return null;
			IL_191:
			this.ParseNumber(ReadType.Read);
			object value = this.Value;
			bool flag;
			if (value is System.Numerics.BigInteger)
			{
				System.Numerics.BigInteger left = (System.Numerics.BigInteger)value;
				flag = (left != 0L);
			}
			else
			{
				flag = Convert.ToBoolean(this.Value, CultureInfo.InvariantCulture);
			}
			base.SetToken(JsonToken.Boolean, flag, false);
			return new bool?(flag);
			IL_1EC:
			bool flag2 = c == 't';
			string value2 = flag2 ? JsonConvert.True : JsonConvert.False;
			if (!this.MatchValueWithTrailingSeparator(value2))
			{
				throw this.CreateUnexpectedCharacterException(this._chars[this._charPos]);
			}
			base.SetToken(JsonToken.Boolean, flag2);
			return new bool?(flag2);
			IL_25A:
			this._charPos++;
			if (this._currentState == JsonReader.State.Array || this._currentState == JsonReader.State.ArrayStart || this._currentState == JsonReader.State.PostValue)
			{
				base.SetToken(JsonToken.EndArray);
				return null;
			}
			throw this.CreateUnexpectedCharacterException(c);
			Block_18:
			throw this.CreateUnexpectedCharacterException(c);
			IL_301:
			throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x00043218 File Offset: 0x00041418
		private void ProcessValueComma()
		{
			this._charPos++;
			if (this._currentState != JsonReader.State.PostValue)
			{
				base.SetToken(JsonToken.Undefined);
				JsonReaderException ex = this.CreateUnexpectedCharacterException(',');
				this._charPos--;
				throw ex;
			}
			base.SetStateBasedOnCurrent();
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x00043268 File Offset: 0x00041468
		[NullableContext(2)]
		private object ReadNumberValue(ReadType readType)
		{
			this.EnsureBuffer();
			switch (this._currentState)
			{
			case JsonReader.State.Start:
			case JsonReader.State.Property:
			case JsonReader.State.ArrayStart:
			case JsonReader.State.Array:
			case JsonReader.State.ConstructorStart:
			case JsonReader.State.Constructor:
				break;
			case JsonReader.State.Complete:
			case JsonReader.State.ObjectStart:
			case JsonReader.State.Object:
			case JsonReader.State.Closed:
			case JsonReader.State.Error:
				goto IL_26E;
			case JsonReader.State.PostValue:
				if (this.ParsePostValue(true))
				{
					return null;
				}
				break;
			case JsonReader.State.Finished:
				this.ReadFinished();
				return null;
			default:
				goto IL_26E;
			}
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= '9')
				{
					if (c != '\0')
					{
						switch (c)
						{
						case '\t':
							break;
						case '\n':
							this.ProcessLineFeed();
							continue;
						case '\v':
						case '\f':
							goto IL_245;
						case '\r':
							this.ProcessCarriageReturn(false);
							continue;
						default:
							switch (c)
							{
							case ' ':
								break;
							case '!':
							case '#':
							case '$':
							case '%':
							case '&':
							case '(':
							case ')':
							case '*':
							case '+':
								goto IL_245;
							case '"':
							case '\'':
								goto IL_151;
							case ',':
								this.ProcessValueComma();
								continue;
							case '-':
								goto IL_179;
							case '.':
							case '0':
							case '1':
							case '2':
							case '3':
							case '4':
							case '5':
							case '6':
							case '7':
							case '8':
							case '9':
								goto IL_1B2;
							case '/':
								this.ParseComment(false);
								continue;
							default:
								goto IL_245;
							}
							break;
						}
						this._charPos++;
						continue;
					}
					if (this.ReadNullChar())
					{
						break;
					}
					continue;
				}
				else if (c <= 'N')
				{
					if (c == 'I')
					{
						goto IL_171;
					}
					if (c == 'N')
					{
						goto IL_169;
					}
				}
				else
				{
					if (c == ']')
					{
						goto IL_1D7;
					}
					if (c == 'n')
					{
						goto IL_161;
					}
				}
				IL_245:
				this._charPos++;
				if (!char.IsWhiteSpace(c))
				{
					goto Block_17;
				}
			}
			base.SetToken(JsonToken.None, null, false);
			return null;
			IL_151:
			this.ParseString(c, readType);
			return this.FinishReadQuotedNumber(readType);
			IL_161:
			this.HandleNull();
			return null;
			IL_169:
			return this.ParseNumberNaN(readType);
			IL_171:
			return this.ParseNumberPositiveInfinity(readType);
			IL_179:
			if (this.EnsureChars(1, true) && this._chars[this._charPos + 1] == 'I')
			{
				return this.ParseNumberNegativeInfinity(readType);
			}
			this.ParseNumber(readType);
			return this.Value;
			IL_1B2:
			this.ParseNumber(readType);
			return this.Value;
			IL_1D7:
			this._charPos++;
			if (this._currentState == JsonReader.State.Array || this._currentState == JsonReader.State.ArrayStart || this._currentState == JsonReader.State.PostValue)
			{
				base.SetToken(JsonToken.EndArray);
				return null;
			}
			throw this.CreateUnexpectedCharacterException(c);
			Block_17:
			throw this.CreateUnexpectedCharacterException(c);
			IL_26E:
			throw JsonReaderException.Create(this, "Unexpected state: {0}.".FormatWith(CultureInfo.InvariantCulture, base.CurrentState));
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x00043508 File Offset: 0x00041708
		[NullableContext(2)]
		private object FinishReadQuotedNumber(ReadType readType)
		{
			if (readType == ReadType.ReadAsInt32)
			{
				return base.ReadInt32String(this._stringReference.ToString());
			}
			if (readType == ReadType.ReadAsDecimal)
			{
				return base.ReadDecimalString(this._stringReference.ToString());
			}
			if (readType != ReadType.ReadAsDouble)
			{
				throw new ArgumentOutOfRangeException("readType");
			}
			return base.ReadDoubleString(this._stringReference.ToString());
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x00043594 File Offset: 0x00041794
		public override DateTimeOffset? ReadAsDateTimeOffset()
		{
			return (DateTimeOffset?)this.ReadStringValue(ReadType.ReadAsDateTimeOffset);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x000435A4 File Offset: 0x000417A4
		public override decimal? ReadAsDecimal()
		{
			return (decimal?)this.ReadNumberValue(ReadType.ReadAsDecimal);
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x000435B4 File Offset: 0x000417B4
		public override double? ReadAsDouble()
		{
			return (double?)this.ReadNumberValue(ReadType.ReadAsDouble);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x000435C4 File Offset: 0x000417C4
		private void HandleNull()
		{
			if (!this.EnsureChars(1, true))
			{
				this._charPos = this._charsUsed;
				throw base.CreateUnexpectedEndException();
			}
			if (this._chars[this._charPos + 1] == 'u')
			{
				this.ParseNull();
				return;
			}
			this._charPos += 2;
			throw this.CreateUnexpectedCharacterException(this._chars[this._charPos - 1]);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x00043638 File Offset: 0x00041838
		private void ReadFinished()
		{
			if (this.EnsureChars(0, false))
			{
				this.EatWhitespace();
				if (this._isEndOfFile)
				{
					return;
				}
				if (this._chars[this._charPos] != '/')
				{
					throw JsonReaderException.Create(this, "Additional text encountered after finished reading JSON content: {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
				}
				this.ParseComment(false);
			}
			base.SetToken(JsonToken.None);
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x000436B8 File Offset: 0x000418B8
		private bool ReadNullChar()
		{
			if (this._charsUsed == this._charPos)
			{
				if (this.ReadData(false) == 0)
				{
					this._isEndOfFile = true;
					return true;
				}
			}
			else
			{
				this._charPos++;
			}
			return false;
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x000436F0 File Offset: 0x000418F0
		private void EnsureBuffer()
		{
			if (this._chars == null)
			{
				this._chars = BufferUtils.RentBuffer(this._arrayPool, 1024);
				this._chars[0] = '\0';
			}
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x0004371C File Offset: 0x0004191C
		private void ReadStringIntoBuffer(char quote)
		{
			int num = this._charPos;
			int charPos = this._charPos;
			int lastWritePosition = this._charPos;
			this._stringBuffer.Position = 0;
			char c2;
			for (;;)
			{
				char c = this._chars[num++];
				if (c <= '\r')
				{
					if (c != '\0')
					{
						if (c != '\n')
						{
							if (c == '\r')
							{
								this._charPos = num - 1;
								this.ProcessCarriageReturn(true);
								num = this._charPos;
							}
						}
						else
						{
							this._charPos = num - 1;
							this.ProcessLineFeed();
							num = this._charPos;
						}
					}
					else if (this._charsUsed == num - 1)
					{
						num--;
						if (this.ReadData(true) == 0)
						{
							break;
						}
					}
				}
				else if (c != '"' && c != '\'')
				{
					if (c == '\\')
					{
						this._charPos = num;
						if (!this.EnsureChars(0, true))
						{
							goto Block_10;
						}
						int writeToPosition = num - 1;
						c2 = this._chars[num];
						num++;
						char c3;
						if (c2 <= '\\')
						{
							if (c2 <= '\'')
							{
								if (c2 != '"' && c2 != '\'')
								{
									goto Block_14;
								}
							}
							else if (c2 != '/')
							{
								if (c2 != '\\')
								{
									goto Block_16;
								}
								c3 = '\\';
								goto IL_2C6;
							}
							c3 = c2;
						}
						else if (c2 <= 'f')
						{
							if (c2 != 'b')
							{
								if (c2 != 'f')
								{
									goto Block_19;
								}
								c3 = '\f';
							}
							else
							{
								c3 = '\b';
							}
						}
						else
						{
							if (c2 != 'n')
							{
								switch (c2)
								{
								case 'r':
									c3 = '\r';
									goto IL_2C6;
								case 't':
									c3 = '\t';
									goto IL_2C6;
								case 'u':
									this._charPos = num;
									c3 = this.ParseUnicode();
									if (StringUtils.IsLowSurrogate(c3))
									{
										c3 = '�';
									}
									else if (StringUtils.IsHighSurrogate(c3))
									{
										bool flag;
										do
										{
											flag = false;
											if (this.EnsureChars(2, true) && this._chars[this._charPos] == '\\' && this._chars[this._charPos + 1] == 'u')
											{
												char writeChar = c3;
												this._charPos += 2;
												c3 = this.ParseUnicode();
												if (!StringUtils.IsLowSurrogate(c3))
												{
													if (StringUtils.IsHighSurrogate(c3))
													{
														writeChar = '�';
														flag = true;
													}
													else
													{
														writeChar = '�';
													}
												}
												this.EnsureBufferNotEmpty();
												this.WriteCharToBuffer(writeChar, lastWritePosition, writeToPosition);
												lastWritePosition = this._charPos;
											}
											else
											{
												c3 = '�';
											}
										}
										while (flag);
									}
									num = this._charPos;
									goto IL_2C6;
								}
								goto Block_21;
							}
							c3 = '\n';
						}
						IL_2C6:
						this.EnsureBufferNotEmpty();
						this.WriteCharToBuffer(c3, lastWritePosition, writeToPosition);
						lastWritePosition = num;
					}
				}
				else if (this._chars[num - 1] == quote)
				{
					goto Block_28;
				}
			}
			this._charPos = num;
			throw JsonReaderException.Create(this, "Unterminated string. Expected delimiter: {0}.".FormatWith(CultureInfo.InvariantCulture, quote));
			Block_10:
			throw JsonReaderException.Create(this, "Unterminated string. Expected delimiter: {0}.".FormatWith(CultureInfo.InvariantCulture, quote));
			Block_14:
			Block_16:
			Block_19:
			Block_21:
			this._charPos = num;
			throw JsonReaderException.Create(this, "Bad JSON escape sequence: {0}.".FormatWith(CultureInfo.InvariantCulture, "\\" + c2.ToString()));
			Block_28:
			this.FinishReadStringIntoBuffer(num - 1, charPos, lastWritePosition);
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x00043A60 File Offset: 0x00041C60
		private void FinishReadStringIntoBuffer(int charPos, int initialPosition, int lastWritePosition)
		{
			if (initialPosition == lastWritePosition)
			{
				this._stringReference = new StringReference(this._chars, initialPosition, charPos - initialPosition);
			}
			else
			{
				this.EnsureBufferNotEmpty();
				if (charPos > lastWritePosition)
				{
					this._stringBuffer.Append(this._arrayPool, this._chars, lastWritePosition, charPos - lastWritePosition);
				}
				this._stringReference = new StringReference(this._stringBuffer.InternalBuffer, 0, this._stringBuffer.Position);
			}
			this._charPos = charPos + 1;
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x00043AE8 File Offset: 0x00041CE8
		private void WriteCharToBuffer(char writeChar, int lastWritePosition, int writeToPosition)
		{
			if (writeToPosition > lastWritePosition)
			{
				this._stringBuffer.Append(this._arrayPool, this._chars, lastWritePosition, writeToPosition - lastWritePosition);
			}
			this._stringBuffer.Append(this._arrayPool, writeChar);
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x00043B20 File Offset: 0x00041D20
		private char ConvertUnicode(bool enoughChars)
		{
			if (!enoughChars)
			{
				throw JsonReaderException.Create(this, "Unexpected end while parsing Unicode escape sequence.");
			}
			int value;
			if (ConvertUtils.TryHexTextToInt(this._chars, this._charPos, this._charPos + 4, out value))
			{
				char result = Convert.ToChar(value);
				this._charPos += 4;
				return result;
			}
			throw JsonReaderException.Create(this, "Invalid Unicode escape sequence: \\u{0}.".FormatWith(CultureInfo.InvariantCulture, new string(this._chars, this._charPos, 4)));
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x00043BA0 File Offset: 0x00041DA0
		private char ParseUnicode()
		{
			return this.ConvertUnicode(this.EnsureChars(4, true));
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x00043BB0 File Offset: 0x00041DB0
		private void ReadNumberIntoBuffer()
		{
			int num = this._charPos;
			for (;;)
			{
				char c = this._chars[num];
				if (c == '\0')
				{
					this._charPos = num;
					if (this._charsUsed != num)
					{
						return;
					}
					if (this.ReadData(true) == 0)
					{
						break;
					}
				}
				else
				{
					if (this.ReadNumberCharIntoBuffer(c, num))
					{
						return;
					}
					num++;
				}
			}
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x00043C08 File Offset: 0x00041E08
		private bool ReadNumberCharIntoBuffer(char currentChar, int charPos)
		{
			if (currentChar <= 'X')
			{
				switch (currentChar)
				{
				case '+':
				case '-':
				case '.':
				case '0':
				case '1':
				case '2':
				case '3':
				case '4':
				case '5':
				case '6':
				case '7':
				case '8':
				case '9':
				case 'A':
				case 'B':
				case 'C':
				case 'D':
				case 'E':
				case 'F':
					break;
				case ',':
				case '/':
				case ':':
				case ';':
				case '<':
				case '=':
				case '>':
				case '?':
				case '@':
					goto IL_B9;
				default:
					if (currentChar != 'X')
					{
						goto IL_B9;
					}
					break;
				}
			}
			else
			{
				switch (currentChar)
				{
				case 'a':
				case 'b':
				case 'c':
				case 'd':
				case 'e':
				case 'f':
					break;
				default:
					if (currentChar != 'x')
					{
						goto IL_B9;
					}
					break;
				}
			}
			return false;
			IL_B9:
			this._charPos = charPos;
			if (char.IsWhiteSpace(currentChar) || currentChar == ',' || currentChar == '}' || currentChar == ']' || currentChar == ')' || currentChar == '/')
			{
				return true;
			}
			throw JsonReaderException.Create(this, "Unexpected character encountered while parsing number: {0}.".FormatWith(CultureInfo.InvariantCulture, currentChar));
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x00043D2C File Offset: 0x00041F2C
		private void ClearRecentString()
		{
			this._stringBuffer.Position = 0;
			this._stringReference = default(StringReference);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x00043D48 File Offset: 0x00041F48
		private bool ParsePostValue(bool ignoreComments)
		{
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= ')')
				{
					if (c <= '\r')
					{
						if (c != '\0')
						{
							switch (c)
							{
							case '\t':
								break;
							case '\n':
								this.ProcessLineFeed();
								continue;
							case '\v':
							case '\f':
								goto IL_161;
							case '\r':
								this.ProcessCarriageReturn(false);
								continue;
							default:
								goto IL_161;
							}
						}
						else
						{
							if (this._charsUsed != this._charPos)
							{
								this._charPos++;
								continue;
							}
							if (this.ReadData(false) == 0)
							{
								break;
							}
							continue;
						}
					}
					else if (c != ' ')
					{
						if (c != ')')
						{
							goto IL_161;
						}
						goto IL_F7;
					}
					this._charPos++;
					continue;
				}
				if (c <= '/')
				{
					if (c == ',')
					{
						goto IL_121;
					}
					if (c == '/')
					{
						this.ParseComment(!ignoreComments);
						if (!ignoreComments)
						{
							return true;
						}
						continue;
					}
				}
				else
				{
					if (c == ']')
					{
						goto IL_DF;
					}
					if (c == '}')
					{
						goto IL_C7;
					}
				}
				IL_161:
				if (!char.IsWhiteSpace(c))
				{
					goto IL_17F;
				}
				this._charPos++;
			}
			this._currentState = JsonReader.State.Finished;
			return false;
			IL_C7:
			this._charPos++;
			base.SetToken(JsonToken.EndObject);
			return true;
			IL_DF:
			this._charPos++;
			base.SetToken(JsonToken.EndArray);
			return true;
			IL_F7:
			this._charPos++;
			base.SetToken(JsonToken.EndConstructor);
			return true;
			IL_121:
			this._charPos++;
			base.SetStateBasedOnCurrent();
			return false;
			IL_17F:
			if (base.SupportMultipleContent && this.Depth == 0)
			{
				base.SetStateBasedOnCurrent();
				return false;
			}
			throw JsonReaderException.Create(this, "After parsing a value an unexpected character was encountered: {0}.".FormatWith(CultureInfo.InvariantCulture, c));
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x00043F14 File Offset: 0x00042114
		private bool ParseObject()
		{
			for (;;)
			{
				char c = this._chars[this._charPos];
				if (c <= '\r')
				{
					if (c != '\0')
					{
						switch (c)
						{
						case '\t':
							break;
						case '\n':
							this.ProcessLineFeed();
							continue;
						case '\v':
						case '\f':
							goto IL_D5;
						case '\r':
							this.ProcessCarriageReturn(false);
							continue;
						default:
							goto IL_D5;
						}
					}
					else
					{
						if (this._charsUsed != this._charPos)
						{
							this._charPos++;
							continue;
						}
						if (this.ReadData(false) == 0)
						{
							break;
						}
						continue;
					}
				}
				else if (c != ' ')
				{
					if (c == '/')
					{
						goto IL_A2;
					}
					if (c != '}')
					{
						goto IL_D5;
					}
					goto IL_8A;
				}
				this._charPos++;
				continue;
				IL_D5:
				if (!char.IsWhiteSpace(c))
				{
					goto IL_F3;
				}
				this._charPos++;
			}
			return false;
			IL_8A:
			base.SetToken(JsonToken.EndObject);
			this._charPos++;
			return true;
			IL_A2:
			this.ParseComment(true);
			return true;
			IL_F3:
			return this.ParseProperty();
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x00044020 File Offset: 0x00042220
		private bool ParseProperty()
		{
			char c = this._chars[this._charPos];
			char c2;
			if (c == '"' || c == '\'')
			{
				this._charPos++;
				c2 = c;
				this.ShiftBufferIfNeeded();
				this.ReadStringIntoBuffer(c2);
			}
			else
			{
				if (!this.ValidIdentifierChar(c))
				{
					throw JsonReaderException.Create(this, "Invalid property identifier character: {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
				}
				c2 = '\0';
				this.ShiftBufferIfNeeded();
				this.ParseUnquotedProperty();
			}
			string text;
			if (this.PropertyNameTable != null)
			{
				text = this.PropertyNameTable.Get(this._stringReference.Chars, this._stringReference.StartIndex, this._stringReference.Length);
				if (text == null)
				{
					text = this._stringReference.ToString();
				}
			}
			else
			{
				text = this._stringReference.ToString();
			}
			this.EatWhitespace();
			if (this._chars[this._charPos] != ':')
			{
				throw JsonReaderException.Create(this, "Invalid character after parsing property name. Expected ':' but got: {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
			}
			this._charPos++;
			base.SetToken(JsonToken.PropertyName, text);
			this._quoteChar = c2;
			this.ClearRecentString();
			return true;
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x00044188 File Offset: 0x00042388
		private bool ValidIdentifierChar(char value)
		{
			return char.IsLetterOrDigit(value) || value == '_' || value == '$';
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x000441A4 File Offset: 0x000423A4
		private void ParseUnquotedProperty()
		{
			int charPos = this._charPos;
			for (;;)
			{
				char c = this._chars[this._charPos];
				if (c == '\0')
				{
					if (this._charsUsed != this._charPos)
					{
						goto IL_41;
					}
					if (this.ReadData(true) == 0)
					{
						break;
					}
				}
				else if (this.ReadUnquotedPropertyReportIfDone(c, charPos))
				{
					return;
				}
			}
			throw JsonReaderException.Create(this, "Unexpected end while parsing unquoted property name.");
			IL_41:
			this._stringReference = new StringReference(this._chars, charPos, this._charPos - charPos);
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0004421C File Offset: 0x0004241C
		private bool ReadUnquotedPropertyReportIfDone(char currentChar, int initialPosition)
		{
			if (this.ValidIdentifierChar(currentChar))
			{
				this._charPos++;
				return false;
			}
			if (char.IsWhiteSpace(currentChar) || currentChar == ':')
			{
				this._stringReference = new StringReference(this._chars, initialPosition, this._charPos - initialPosition);
				return true;
			}
			throw JsonReaderException.Create(this, "Invalid JavaScript property identifier character: {0}.".FormatWith(CultureInfo.InvariantCulture, currentChar));
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x00044294 File Offset: 0x00042494
		private bool ParseValue()
		{
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c <= 'N')
				{
					if (c <= ' ')
					{
						if (c != '\0')
						{
							switch (c)
							{
							case '\t':
								break;
							case '\n':
								this.ProcessLineFeed();
								continue;
							case '\v':
							case '\f':
								goto IL_2A6;
							case '\r':
								this.ProcessCarriageReturn(false);
								continue;
							default:
								if (c != ' ')
								{
									goto IL_2A6;
								}
								break;
							}
							this._charPos++;
							continue;
						}
						if (this._charsUsed != this._charPos)
						{
							this._charPos++;
							continue;
						}
						if (this.ReadData(false) == 0)
						{
							break;
						}
						continue;
					}
					else if (c <= '/')
					{
						if (c == '"')
						{
							goto IL_12E;
						}
						switch (c)
						{
						case '\'':
							goto IL_12E;
						case ')':
							goto IL_264;
						case ',':
							goto IL_25A;
						case '-':
							goto IL_1CA;
						case '/':
							goto IL_203;
						}
					}
					else
					{
						if (c == 'I')
						{
							goto IL_1C0;
						}
						if (c == 'N')
						{
							goto IL_1B6;
						}
					}
				}
				else if (c <= 'f')
				{
					if (c == '[')
					{
						goto IL_22B;
					}
					if (c == ']')
					{
						goto IL_242;
					}
					if (c == 'f')
					{
						goto IL_140;
					}
				}
				else if (c <= 't')
				{
					if (c == 'n')
					{
						goto IL_148;
					}
					if (c == 't')
					{
						goto IL_138;
					}
				}
				else
				{
					if (c == 'u')
					{
						goto IL_20C;
					}
					if (c == '{')
					{
						goto IL_214;
					}
				}
				IL_2A6:
				if (!char.IsWhiteSpace(c))
				{
					goto IL_2C4;
				}
				this._charPos++;
			}
			return false;
			IL_12E:
			this.ParseString(c, ReadType.Read);
			return true;
			IL_138:
			this.ParseTrue();
			return true;
			IL_140:
			this.ParseFalse();
			return true;
			IL_148:
			if (this.EnsureChars(1, true))
			{
				char c2 = this._chars[this._charPos + 1];
				if (c2 == 'u')
				{
					this.ParseNull();
				}
				else
				{
					if (c2 != 'e')
					{
						throw this.CreateUnexpectedCharacterException(this._chars[this._charPos]);
					}
					this.ParseConstructor();
				}
				return true;
			}
			this._charPos++;
			throw base.CreateUnexpectedEndException();
			IL_1B6:
			this.ParseNumberNaN(ReadType.Read);
			return true;
			IL_1C0:
			this.ParseNumberPositiveInfinity(ReadType.Read);
			return true;
			IL_1CA:
			if (this.EnsureChars(1, true) && this._chars[this._charPos + 1] == 'I')
			{
				this.ParseNumberNegativeInfinity(ReadType.Read);
			}
			else
			{
				this.ParseNumber(ReadType.Read);
			}
			return true;
			IL_203:
			this.ParseComment(true);
			return true;
			IL_20C:
			this.ParseUndefined();
			return true;
			IL_214:
			this._charPos++;
			base.SetToken(JsonToken.StartObject);
			return true;
			IL_22B:
			this._charPos++;
			base.SetToken(JsonToken.StartArray);
			return true;
			IL_242:
			this._charPos++;
			base.SetToken(JsonToken.EndArray);
			return true;
			IL_25A:
			base.SetToken(JsonToken.Undefined);
			return true;
			IL_264:
			this._charPos++;
			base.SetToken(JsonToken.EndConstructor);
			return true;
			IL_2C4:
			if (char.IsNumber(c) || c == '-' || c == '.')
			{
				this.ParseNumber(ReadType.Read);
				return true;
			}
			throw this.CreateUnexpectedCharacterException(c);
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x00044594 File Offset: 0x00042794
		private void ProcessLineFeed()
		{
			this._charPos++;
			this.OnNewLine(this._charPos);
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x000445B0 File Offset: 0x000427B0
		private void ProcessCarriageReturn(bool append)
		{
			this._charPos++;
			this.SetNewLine(this.EnsureChars(1, append));
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x000445D0 File Offset: 0x000427D0
		private void EatWhitespace()
		{
			for (;;)
			{
				char c = this._chars[this._charPos];
				if (c != '\0')
				{
					if (c != '\n')
					{
						if (c != '\r')
						{
							if (c != ' ' && !char.IsWhiteSpace(c))
							{
								return;
							}
							this._charPos++;
						}
						else
						{
							this.ProcessCarriageReturn(false);
						}
					}
					else
					{
						this.ProcessLineFeed();
					}
				}
				else if (this._charsUsed == this._charPos)
				{
					if (this.ReadData(false) == 0)
					{
						break;
					}
				}
				else
				{
					this._charPos++;
				}
			}
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0004466C File Offset: 0x0004286C
		private void ParseConstructor()
		{
			if (!this.MatchValueWithTrailingSeparator("new"))
			{
				throw JsonReaderException.Create(this, "Unexpected content while parsing JSON.");
			}
			this.EatWhitespace();
			int charPos = this._charPos;
			char c;
			for (;;)
			{
				c = this._chars[this._charPos];
				if (c == '\0')
				{
					if (this._charsUsed != this._charPos)
					{
						goto IL_57;
					}
					if (this.ReadData(true) == 0)
					{
						break;
					}
				}
				else
				{
					if (!char.IsLetterOrDigit(c))
					{
						goto IL_8C;
					}
					this._charPos++;
				}
			}
			throw JsonReaderException.Create(this, "Unexpected end while parsing constructor.");
			IL_57:
			int charPos2 = this._charPos;
			this._charPos++;
			goto IL_116;
			IL_8C:
			if (c == '\r')
			{
				charPos2 = this._charPos;
				this.ProcessCarriageReturn(true);
			}
			else if (c == '\n')
			{
				charPos2 = this._charPos;
				this.ProcessLineFeed();
			}
			else if (char.IsWhiteSpace(c))
			{
				charPos2 = this._charPos;
				this._charPos++;
			}
			else
			{
				if (c != '(')
				{
					throw JsonReaderException.Create(this, "Unexpected character while parsing constructor: {0}.".FormatWith(CultureInfo.InvariantCulture, c));
				}
				charPos2 = this._charPos;
			}
			IL_116:
			this._stringReference = new StringReference(this._chars, charPos, charPos2 - charPos);
			string value = this._stringReference.ToString();
			this.EatWhitespace();
			if (this._chars[this._charPos] != '(')
			{
				throw JsonReaderException.Create(this, "Unexpected character while parsing constructor: {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
			}
			this._charPos++;
			this.ClearRecentString();
			base.SetToken(JsonToken.StartConstructor, value);
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x00044824 File Offset: 0x00042A24
		private void ParseNumber(ReadType readType)
		{
			this.ShiftBufferIfNeeded();
			char firstChar = this._chars[this._charPos];
			int charPos = this._charPos;
			this.ReadNumberIntoBuffer();
			this.ParseReadNumber(readType, firstChar, charPos);
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x00044860 File Offset: 0x00042A60
		private void ParseReadNumber(ReadType readType, char firstChar, int initialPosition)
		{
			base.SetPostValueState(true);
			this._stringReference = new StringReference(this._chars, initialPosition, this._charPos - initialPosition);
			bool flag = char.IsDigit(firstChar) && this._stringReference.Length == 1;
			bool flag2 = firstChar == '0' && this._stringReference.Length > 1 && this._stringReference.Chars[this._stringReference.StartIndex + 1] != '.' && this._stringReference.Chars[this._stringReference.StartIndex + 1] != 'e' && this._stringReference.Chars[this._stringReference.StartIndex + 1] != 'E';
			object value;
			JsonToken newToken;
			switch (readType)
			{
			case ReadType.Read:
			case ReadType.ReadAsInt64:
			{
				if (flag)
				{
					value = (long)((ulong)firstChar - 48UL);
					newToken = JsonToken.Integer;
					goto IL_6BF;
				}
				if (flag2)
				{
					string text = this._stringReference.ToString();
					try
					{
						value = (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase) ? Convert.ToInt64(text, 16) : Convert.ToInt64(text, 8));
					}
					catch (Exception ex)
					{
						throw this.ThrowReaderError("Input string '{0}' is not a valid number.".FormatWith(CultureInfo.InvariantCulture, text), ex);
					}
					newToken = JsonToken.Integer;
					goto IL_6BF;
				}
				long num;
				ParseResult parseResult = ConvertUtils.Int64TryParse(this._stringReference.Chars, this._stringReference.StartIndex, this._stringReference.Length, out num);
				if (parseResult == ParseResult.Success)
				{
					value = num;
					newToken = JsonToken.Integer;
					goto IL_6BF;
				}
				if (parseResult != ParseResult.Overflow)
				{
					if (this._floatParseHandling == FloatParseHandling.Decimal)
					{
						decimal num2;
						parseResult = ConvertUtils.DecimalTryParse(this._stringReference.Chars, this._stringReference.StartIndex, this._stringReference.Length, out num2);
						if (parseResult != ParseResult.Success)
						{
							throw this.ThrowReaderError("Input string '{0}' is not a valid decimal.".FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
						}
						value = num2;
					}
					else
					{
						double num3;
						if (!double.TryParse(this._stringReference.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out num3))
						{
							throw this.ThrowReaderError("Input string '{0}' is not a valid number.".FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
						}
						value = num3;
					}
					newToken = JsonToken.Float;
					goto IL_6BF;
				}
				string text2 = this._stringReference.ToString();
				if (text2.Length > 380)
				{
					throw this.ThrowReaderError("JSON integer {0} is too large to parse.".FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
				}
				value = JsonTextReader.BigIntegerParse(text2, CultureInfo.InvariantCulture);
				newToken = JsonToken.Integer;
				goto IL_6BF;
			}
			case ReadType.ReadAsInt32:
				if (flag)
				{
					value = (int)(firstChar - '0');
				}
				else
				{
					if (flag2)
					{
						string text3 = this._stringReference.ToString();
						try
						{
							value = (text3.StartsWith("0x", StringComparison.OrdinalIgnoreCase) ? Convert.ToInt32(text3, 16) : Convert.ToInt32(text3, 8));
							goto IL_2AF;
						}
						catch (Exception ex2)
						{
							throw this.ThrowReaderError("Input string '{0}' is not a valid integer.".FormatWith(CultureInfo.InvariantCulture, text3), ex2);
						}
					}
					int num4;
					ParseResult parseResult2 = ConvertUtils.Int32TryParse(this._stringReference.Chars, this._stringReference.StartIndex, this._stringReference.Length, out num4);
					if (parseResult2 == ParseResult.Success)
					{
						value = num4;
					}
					else
					{
						if (parseResult2 == ParseResult.Overflow)
						{
							throw this.ThrowReaderError("JSON integer {0} is too large or small for an Int32.".FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
						}
						throw this.ThrowReaderError("Input string '{0}' is not a valid integer.".FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
					}
				}
				IL_2AF:
				newToken = JsonToken.Integer;
				goto IL_6BF;
			case ReadType.ReadAsString:
			{
				string text4 = this._stringReference.ToString();
				if (flag2)
				{
					try
					{
						if (text4.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
						{
							Convert.ToInt64(text4, 16);
						}
						else
						{
							Convert.ToInt64(text4, 8);
						}
						goto IL_18C;
					}
					catch (Exception ex3)
					{
						throw this.ThrowReaderError("Input string '{0}' is not a valid number.".FormatWith(CultureInfo.InvariantCulture, text4), ex3);
					}
				}
				double num5;
				if (!double.TryParse(text4, NumberStyles.Float, CultureInfo.InvariantCulture, out num5))
				{
					throw this.ThrowReaderError("Input string '{0}' is not a valid number.".FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
				}
				IL_18C:
				newToken = JsonToken.String;
				value = text4;
				goto IL_6BF;
			}
			case ReadType.ReadAsDecimal:
				if (flag)
				{
					value = firstChar - 48m;
				}
				else
				{
					if (flag2)
					{
						string text5 = this._stringReference.ToString();
						try
						{
							value = Convert.ToDecimal(text5.StartsWith("0x", StringComparison.OrdinalIgnoreCase) ? Convert.ToInt64(text5, 16) : Convert.ToInt64(text5, 8));
							goto IL_3AD;
						}
						catch (Exception ex4)
						{
							throw this.ThrowReaderError("Input string '{0}' is not a valid decimal.".FormatWith(CultureInfo.InvariantCulture, text5), ex4);
						}
					}
					decimal num6;
					if (ConvertUtils.DecimalTryParse(this._stringReference.Chars, this._stringReference.StartIndex, this._stringReference.Length, out num6) != ParseResult.Success)
					{
						throw this.ThrowReaderError("Input string '{0}' is not a valid decimal.".FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
					}
					value = num6;
				}
				IL_3AD:
				newToken = JsonToken.Float;
				goto IL_6BF;
			case ReadType.ReadAsDouble:
				if (flag)
				{
					value = (double)firstChar - 48.0;
				}
				else
				{
					if (flag2)
					{
						string text6 = this._stringReference.ToString();
						try
						{
							value = Convert.ToDouble(text6.StartsWith("0x", StringComparison.OrdinalIgnoreCase) ? Convert.ToInt64(text6, 16) : Convert.ToInt64(text6, 8));
							goto IL_49E;
						}
						catch (Exception ex5)
						{
							throw this.ThrowReaderError("Input string '{0}' is not a valid double.".FormatWith(CultureInfo.InvariantCulture, text6), ex5);
						}
					}
					double num7;
					if (!double.TryParse(this._stringReference.ToString(), NumberStyles.Float, CultureInfo.InvariantCulture, out num7))
					{
						throw this.ThrowReaderError("Input string '{0}' is not a valid double.".FormatWith(CultureInfo.InvariantCulture, this._stringReference.ToString()), null);
					}
					value = num7;
				}
				IL_49E:
				newToken = JsonToken.Float;
				goto IL_6BF;
			}
			throw JsonReaderException.Create(this, "Cannot read number value as type.");
			IL_6BF:
			this.ClearRecentString();
			base.SetToken(newToken, value, false);
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x00044F80 File Offset: 0x00043180
		private JsonReaderException ThrowReaderError(string message, [Nullable(2)] Exception ex = null)
		{
			base.SetToken(JsonToken.Undefined, null, false);
			return JsonReaderException.Create(this, message, ex);
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00044F94 File Offset: 0x00043194
		[MethodImpl(MethodImplOptions.NoInlining)]
		private static object BigIntegerParse(string number, CultureInfo culture)
		{
			return System.Numerics.BigInteger.Parse(number, culture);
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x00044FA4 File Offset: 0x000431A4
		private void ParseComment(bool setToken)
		{
			this._charPos++;
			if (!this.EnsureChars(1, false))
			{
				throw JsonReaderException.Create(this, "Unexpected end while parsing comment.");
			}
			bool flag;
			if (this._chars[this._charPos] == '*')
			{
				flag = false;
			}
			else
			{
				if (this._chars[this._charPos] != '/')
				{
					throw JsonReaderException.Create(this, "Error parsing comment. Expected: *, got {0}.".FormatWith(CultureInfo.InvariantCulture, this._chars[this._charPos]));
				}
				flag = true;
			}
			this._charPos++;
			int charPos = this._charPos;
			for (;;)
			{
				char c = this._chars[this._charPos];
				if (c <= '\n')
				{
					if (c != '\0')
					{
						if (c == '\n')
						{
							if (flag)
							{
								goto Block_16;
							}
							this.ProcessLineFeed();
							continue;
						}
					}
					else
					{
						if (this._charsUsed != this._charPos)
						{
							this._charPos++;
							continue;
						}
						if (this.ReadData(true) == 0)
						{
							break;
						}
						continue;
					}
				}
				else if (c != '\r')
				{
					if (c == '*')
					{
						this._charPos++;
						if (!flag && this.EnsureChars(0, true) && this._chars[this._charPos] == '/')
						{
							goto Block_14;
						}
						continue;
					}
				}
				else
				{
					if (flag)
					{
						goto Block_15;
					}
					this.ProcessCarriageReturn(true);
					continue;
				}
				this._charPos++;
			}
			if (!flag)
			{
				throw JsonReaderException.Create(this, "Unexpected end while parsing comment.");
			}
			this.EndComment(setToken, charPos, this._charPos);
			return;
			Block_14:
			this.EndComment(setToken, charPos, this._charPos - 1);
			this._charPos++;
			return;
			Block_15:
			this.EndComment(setToken, charPos, this._charPos);
			return;
			Block_16:
			this.EndComment(setToken, charPos, this._charPos);
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x00045184 File Offset: 0x00043384
		private void EndComment(bool setToken, int initialPosition, int endPosition)
		{
			if (setToken)
			{
				base.SetToken(JsonToken.Comment, new string(this._chars, initialPosition, endPosition - initialPosition));
			}
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x000451A4 File Offset: 0x000433A4
		private bool MatchValue(string value)
		{
			return this.MatchValue(this.EnsureChars(value.Length - 1, true), value);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x000451BC File Offset: 0x000433BC
		private bool MatchValue(bool enoughChars, string value)
		{
			if (!enoughChars)
			{
				this._charPos = this._charsUsed;
				throw base.CreateUnexpectedEndException();
			}
			for (int i = 0; i < value.Length; i++)
			{
				if (this._chars[this._charPos + i] != value[i])
				{
					this._charPos += i;
					return false;
				}
			}
			this._charPos += value.Length;
			return true;
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0004523C File Offset: 0x0004343C
		private bool MatchValueWithTrailingSeparator(string value)
		{
			return this.MatchValue(value) && (!this.EnsureChars(0, false) || this.IsSeparator(this._chars[this._charPos]) || this._chars[this._charPos] == '\0');
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x00045294 File Offset: 0x00043494
		private bool IsSeparator(char c)
		{
			if (c <= ')')
			{
				switch (c)
				{
				case '\t':
				case '\n':
				case '\r':
					break;
				case '\v':
				case '\f':
					goto IL_B6;
				default:
					if (c != ' ')
					{
						if (c != ')')
						{
							goto IL_B6;
						}
						if (base.CurrentState == JsonReader.State.Constructor || base.CurrentState == JsonReader.State.ConstructorStart)
						{
							return true;
						}
						return false;
					}
					break;
				}
				return true;
			}
			if (c <= '/')
			{
				if (c != ',')
				{
					if (c != '/')
					{
						goto IL_B6;
					}
					if (!this.EnsureChars(1, false))
					{
						return false;
					}
					char c2 = this._chars[this._charPos + 1];
					return c2 == '*' || c2 == '/';
				}
			}
			else if (c != ']' && c != '}')
			{
				goto IL_B6;
			}
			return true;
			IL_B6:
			if (char.IsWhiteSpace(c))
			{
				return true;
			}
			return false;
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0004536C File Offset: 0x0004356C
		private void ParseTrue()
		{
			if (this.MatchValueWithTrailingSeparator(JsonConvert.True))
			{
				base.SetToken(JsonToken.Boolean, true);
				return;
			}
			throw JsonReaderException.Create(this, "Error parsing boolean value.");
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x00045398 File Offset: 0x00043598
		private void ParseNull()
		{
			if (this.MatchValueWithTrailingSeparator(JsonConvert.Null))
			{
				base.SetToken(JsonToken.Null);
				return;
			}
			throw JsonReaderException.Create(this, "Error parsing null value.");
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x000453C0 File Offset: 0x000435C0
		private void ParseUndefined()
		{
			if (this.MatchValueWithTrailingSeparator(JsonConvert.Undefined))
			{
				base.SetToken(JsonToken.Undefined);
				return;
			}
			throw JsonReaderException.Create(this, "Error parsing undefined value.");
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x000453E8 File Offset: 0x000435E8
		private void ParseFalse()
		{
			if (this.MatchValueWithTrailingSeparator(JsonConvert.False))
			{
				base.SetToken(JsonToken.Boolean, false);
				return;
			}
			throw JsonReaderException.Create(this, "Error parsing boolean value.");
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00045414 File Offset: 0x00043614
		private object ParseNumberNegativeInfinity(ReadType readType)
		{
			return this.ParseNumberNegativeInfinity(readType, this.MatchValueWithTrailingSeparator(JsonConvert.NegativeInfinity));
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00045428 File Offset: 0x00043628
		private object ParseNumberNegativeInfinity(ReadType readType, bool matched)
		{
			if (matched)
			{
				if (readType != ReadType.Read)
				{
					if (readType == ReadType.ReadAsString)
					{
						base.SetToken(JsonToken.String, JsonConvert.NegativeInfinity);
						return JsonConvert.NegativeInfinity;
					}
					if (readType != ReadType.ReadAsDouble)
					{
						goto IL_5C;
					}
				}
				if (this._floatParseHandling == FloatParseHandling.Double)
				{
					base.SetToken(JsonToken.Float, double.NegativeInfinity);
					return double.NegativeInfinity;
				}
				IL_5C:
				throw JsonReaderException.Create(this, "Cannot read -Infinity value.");
			}
			throw JsonReaderException.Create(this, "Error parsing -Infinity value.");
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x000454AC File Offset: 0x000436AC
		private object ParseNumberPositiveInfinity(ReadType readType)
		{
			return this.ParseNumberPositiveInfinity(readType, this.MatchValueWithTrailingSeparator(JsonConvert.PositiveInfinity));
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x000454C0 File Offset: 0x000436C0
		private object ParseNumberPositiveInfinity(ReadType readType, bool matched)
		{
			if (matched)
			{
				if (readType != ReadType.Read)
				{
					if (readType == ReadType.ReadAsString)
					{
						base.SetToken(JsonToken.String, JsonConvert.PositiveInfinity);
						return JsonConvert.PositiveInfinity;
					}
					if (readType != ReadType.ReadAsDouble)
					{
						goto IL_5C;
					}
				}
				if (this._floatParseHandling == FloatParseHandling.Double)
				{
					base.SetToken(JsonToken.Float, double.PositiveInfinity);
					return double.PositiveInfinity;
				}
				IL_5C:
				throw JsonReaderException.Create(this, "Cannot read Infinity value.");
			}
			throw JsonReaderException.Create(this, "Error parsing Infinity value.");
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x00045544 File Offset: 0x00043744
		private object ParseNumberNaN(ReadType readType)
		{
			return this.ParseNumberNaN(readType, this.MatchValueWithTrailingSeparator(JsonConvert.NaN));
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x00045558 File Offset: 0x00043758
		private object ParseNumberNaN(ReadType readType, bool matched)
		{
			if (matched)
			{
				if (readType != ReadType.Read)
				{
					if (readType == ReadType.ReadAsString)
					{
						base.SetToken(JsonToken.String, JsonConvert.NaN);
						return JsonConvert.NaN;
					}
					if (readType != ReadType.ReadAsDouble)
					{
						goto IL_5C;
					}
				}
				if (this._floatParseHandling == FloatParseHandling.Double)
				{
					base.SetToken(JsonToken.Float, double.NaN);
					return double.NaN;
				}
				IL_5C:
				throw JsonReaderException.Create(this, "Cannot read NaN value.");
			}
			throw JsonReaderException.Create(this, "Error parsing NaN value.");
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x000455DC File Offset: 0x000437DC
		public override void Close()
		{
			base.Close();
			if (this._chars != null)
			{
				BufferUtils.ReturnBuffer(this._arrayPool, this._chars);
				this._chars = null;
			}
			if (base.CloseInput)
			{
				TextReader reader = this._reader;
				if (reader != null)
				{
					reader.Close();
				}
			}
			this._stringBuffer.Clear(this._arrayPool);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0004564C File Offset: 0x0004384C
		public bool HasLineInfo()
		{
			return true;
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000BBD RID: 3005 RVA: 0x00045650 File Offset: 0x00043850
		public int LineNumber
		{
			get
			{
				if (base.CurrentState == JsonReader.State.Start && this.LinePosition == 0 && this.TokenType != JsonToken.Comment)
				{
					return 0;
				}
				return this._lineNumber;
			}
		}

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000BBE RID: 3006 RVA: 0x0004567C File Offset: 0x0004387C
		public int LinePosition
		{
			get
			{
				return this._charPos - this._lineStartPos;
			}
		}

		// Token: 0x0400064B RID: 1611
		private const char UnicodeReplacementChar = '�';

		// Token: 0x0400064C RID: 1612
		private const int MaximumJavascriptIntegerCharacterLength = 380;

		// Token: 0x0400064D RID: 1613
		private const int LargeBufferLength = 1073741823;

		// Token: 0x0400064E RID: 1614
		private readonly TextReader _reader;

		// Token: 0x0400064F RID: 1615
		[Nullable(2)]
		private char[] _chars;

		// Token: 0x04000650 RID: 1616
		private int _charsUsed;

		// Token: 0x04000651 RID: 1617
		private int _charPos;

		// Token: 0x04000652 RID: 1618
		private int _lineStartPos;

		// Token: 0x04000653 RID: 1619
		private int _lineNumber;

		// Token: 0x04000654 RID: 1620
		private bool _isEndOfFile;

		// Token: 0x04000655 RID: 1621
		private StringBuffer _stringBuffer;

		// Token: 0x04000656 RID: 1622
		private StringReference _stringReference;

		// Token: 0x04000657 RID: 1623
		[Nullable(2)]
		private IArrayPool<char> _arrayPool;
	}
}
