using System;
using System.Collections.Generic;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x02000143 RID: 323
	[NullableContext(2)]
	[Nullable(0)]
	public abstract class JsonReader : IDisposable
	{
		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000A7B RID: 2683 RVA: 0x0003F198 File Offset: 0x0003D398
		protected JsonReader.State CurrentState
		{
			get
			{
				return this._currentState;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x0003F1A0 File Offset: 0x0003D3A0
		// (set) Token: 0x06000A7D RID: 2685 RVA: 0x0003F1A8 File Offset: 0x0003D3A8
		public bool CloseInput { get; set; }

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000A7E RID: 2686 RVA: 0x0003F1B4 File Offset: 0x0003D3B4
		// (set) Token: 0x06000A7F RID: 2687 RVA: 0x0003F1BC File Offset: 0x0003D3BC
		public bool SupportMultipleContent { get; set; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000A80 RID: 2688 RVA: 0x0003F1C8 File Offset: 0x0003D3C8
		// (set) Token: 0x06000A81 RID: 2689 RVA: 0x0003F1D0 File Offset: 0x0003D3D0
		public virtual char QuoteChar
		{
			get
			{
				return this._quoteChar;
			}
			protected internal set
			{
				this._quoteChar = value;
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x0003F1DC File Offset: 0x0003D3DC
		// (set) Token: 0x06000A83 RID: 2691 RVA: 0x0003F1E4 File Offset: 0x0003D3E4
		public DateTimeZoneHandling DateTimeZoneHandling
		{
			get
			{
				return this._dateTimeZoneHandling;
			}
			set
			{
				if (value < DateTimeZoneHandling.Local || value > DateTimeZoneHandling.RoundtripKind)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._dateTimeZoneHandling = value;
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x0003F208 File Offset: 0x0003D408
		// (set) Token: 0x06000A85 RID: 2693 RVA: 0x0003F210 File Offset: 0x0003D410
		public DateParseHandling DateParseHandling
		{
			get
			{
				return this._dateParseHandling;
			}
			set
			{
				if (value < DateParseHandling.None || value > DateParseHandling.DateTimeOffset)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._dateParseHandling = value;
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x0003F234 File Offset: 0x0003D434
		// (set) Token: 0x06000A87 RID: 2695 RVA: 0x0003F23C File Offset: 0x0003D43C
		public FloatParseHandling FloatParseHandling
		{
			get
			{
				return this._floatParseHandling;
			}
			set
			{
				if (value < FloatParseHandling.Double || value > FloatParseHandling.Decimal)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._floatParseHandling = value;
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x0003F260 File Offset: 0x0003D460
		// (set) Token: 0x06000A89 RID: 2697 RVA: 0x0003F268 File Offset: 0x0003D468
		public string DateFormatString
		{
			get
			{
				return this._dateFormatString;
			}
			set
			{
				this._dateFormatString = value;
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x0003F274 File Offset: 0x0003D474
		// (set) Token: 0x06000A8B RID: 2699 RVA: 0x0003F27C File Offset: 0x0003D47C
		public int? MaxDepth
		{
			get
			{
				return this._maxDepth;
			}
			set
			{
				int? num = value;
				int num2 = 0;
				if (num.GetValueOrDefault() <= num2 & num != null)
				{
					throw new ArgumentException("Value must be positive.", "value");
				}
				this._maxDepth = value;
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x0003F2C4 File Offset: 0x0003D4C4
		public virtual JsonToken TokenType
		{
			get
			{
				return this._tokenType;
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x0003F2CC File Offset: 0x0003D4CC
		public virtual object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x0003F2D4 File Offset: 0x0003D4D4
		public virtual Type ValueType
		{
			get
			{
				object value = this._value;
				if (value == null)
				{
					return null;
				}
				return value.GetType();
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000A8F RID: 2703 RVA: 0x0003F2EC File Offset: 0x0003D4EC
		public virtual int Depth
		{
			get
			{
				List<JsonPosition> stack = this._stack;
				int num = (stack != null) ? stack.Count : 0;
				if (JsonTokenUtils.IsStartToken(this.TokenType) || this._currentPosition.Type == JsonContainerType.None)
				{
					return num;
				}
				return num + 1;
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000A90 RID: 2704 RVA: 0x0003F33C File Offset: 0x0003D53C
		[Nullable(1)]
		public virtual string Path
		{
			[NullableContext(1)]
			get
			{
				if (this._currentPosition.Type == JsonContainerType.None)
				{
					return string.Empty;
				}
				JsonPosition? currentPosition = (this._currentState != JsonReader.State.ArrayStart && this._currentState != JsonReader.State.ConstructorStart && this._currentState != JsonReader.State.ObjectStart) ? new JsonPosition?(this._currentPosition) : null;
				return JsonPosition.BuildPath(this._stack, currentPosition);
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000A91 RID: 2705 RVA: 0x0003F3BC File Offset: 0x0003D5BC
		// (set) Token: 0x06000A92 RID: 2706 RVA: 0x0003F3D0 File Offset: 0x0003D5D0
		[Nullable(1)]
		public CultureInfo Culture
		{
			[NullableContext(1)]
			get
			{
				return this._culture ?? CultureInfo.InvariantCulture;
			}
			[NullableContext(1)]
			set
			{
				this._culture = value;
			}
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x0003F3DC File Offset: 0x0003D5DC
		internal JsonPosition GetPosition(int depth)
		{
			if (this._stack != null && depth < this._stack.Count)
			{
				return this._stack[depth];
			}
			return this._currentPosition;
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x0003F410 File Offset: 0x0003D610
		protected JsonReader()
		{
			this._currentState = JsonReader.State.Start;
			this._dateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind;
			this._dateParseHandling = DateParseHandling.DateTime;
			this._floatParseHandling = FloatParseHandling.Double;
			this.CloseInput = true;
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x0003F43C File Offset: 0x0003D63C
		private void Push(JsonContainerType value)
		{
			this.UpdateScopeWithFinishedValue();
			if (this._currentPosition.Type == JsonContainerType.None)
			{
				this._currentPosition = new JsonPosition(value);
				return;
			}
			if (this._stack == null)
			{
				this._stack = new List<JsonPosition>();
			}
			this._stack.Add(this._currentPosition);
			this._currentPosition = new JsonPosition(value);
			if (this._maxDepth != null)
			{
				int num = this.Depth + 1;
				int? maxDepth = this._maxDepth;
				if ((num > maxDepth.GetValueOrDefault() & maxDepth != null) && !this._hasExceededMaxDepth)
				{
					this._hasExceededMaxDepth = true;
					throw JsonReaderException.Create(this, "The reader's MaxDepth of {0} has been exceeded.".FormatWith(CultureInfo.InvariantCulture, this._maxDepth));
				}
			}
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x0003F50C File Offset: 0x0003D70C
		private JsonContainerType Pop()
		{
			JsonPosition currentPosition;
			if (this._stack != null && this._stack.Count > 0)
			{
				currentPosition = this._currentPosition;
				this._currentPosition = this._stack[this._stack.Count - 1];
				this._stack.RemoveAt(this._stack.Count - 1);
			}
			else
			{
				currentPosition = this._currentPosition;
				this._currentPosition = default(JsonPosition);
			}
			if (this._maxDepth != null)
			{
				int depth = this.Depth;
				int? maxDepth = this._maxDepth;
				if (depth <= maxDepth.GetValueOrDefault() & maxDepth != null)
				{
					this._hasExceededMaxDepth = false;
				}
			}
			return currentPosition.Type;
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x0003F5D4 File Offset: 0x0003D7D4
		private JsonContainerType Peek()
		{
			return this._currentPosition.Type;
		}

		// Token: 0x06000A98 RID: 2712
		public abstract bool Read();

		// Token: 0x06000A99 RID: 2713 RVA: 0x0003F5E4 File Offset: 0x0003D7E4
		public virtual int? ReadAsInt32()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken != JsonToken.None)
			{
				switch (contentToken)
				{
				case JsonToken.Integer:
				case JsonToken.Float:
				{
					object value = this.Value;
					int num;
					if (value is int)
					{
						num = (int)value;
						return new int?(num);
					}
					if (value is System.Numerics.BigInteger)
					{
						System.Numerics.BigInteger value2 = (System.Numerics.BigInteger)value;
						num = (int)value2;
					}
					else
					{
						try
						{
							num = Convert.ToInt32(value, CultureInfo.InvariantCulture);
						}
						catch (Exception ex)
						{
							throw JsonReaderException.Create(this, "Could not convert to integer: {0}.".FormatWith(CultureInfo.InvariantCulture, value), ex);
						}
					}
					this.SetToken(JsonToken.Integer, num, false);
					return new int?(num);
				}
				case JsonToken.String:
				{
					string s = (string)this.Value;
					return this.ReadInt32String(s);
				}
				case JsonToken.Null:
				case JsonToken.EndArray:
					goto IL_3A;
				}
				throw JsonReaderException.Create(this, "Error reading integer. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
			}
			IL_3A:
			return null;
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x0003F6F8 File Offset: 0x0003D8F8
		internal int? ReadInt32String(string s)
		{
			if (StringUtils.IsNullOrEmpty(s))
			{
				this.SetToken(JsonToken.Null, null, false);
				return null;
			}
			int num;
			if (int.TryParse(s, NumberStyles.Integer, this.Culture, out num))
			{
				this.SetToken(JsonToken.Integer, num, false);
				return new int?(num);
			}
			this.SetToken(JsonToken.String, s, false);
			throw JsonReaderException.Create(this, "Could not convert string to integer: {0}.".FormatWith(CultureInfo.InvariantCulture, s));
		}

		// Token: 0x06000A9B RID: 2715 RVA: 0x0003F774 File Offset: 0x0003D974
		public virtual string ReadAsString()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken <= JsonToken.String)
			{
				if (contentToken != JsonToken.None)
				{
					if (contentToken != JsonToken.String)
					{
						goto IL_40;
					}
					return (string)this.Value;
				}
			}
			else if (contentToken != JsonToken.Null && contentToken != JsonToken.EndArray)
			{
				goto IL_40;
			}
			return null;
			IL_40:
			if (JsonTokenUtils.IsPrimitiveToken(contentToken))
			{
				object value = this.Value;
				if (value != null)
				{
					IFormattable formattable = value as IFormattable;
					string text;
					if (formattable != null)
					{
						text = formattable.ToString(null, this.Culture);
					}
					else
					{
						Uri uri = value as Uri;
						text = ((uri != null) ? uri.OriginalString : value.ToString());
					}
					this.SetToken(JsonToken.String, text, false);
					return text;
				}
			}
			throw JsonReaderException.Create(this, "Error reading string. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0003F848 File Offset: 0x0003DA48
		public virtual byte[] ReadAsBytes()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken <= JsonToken.String)
			{
				switch (contentToken)
				{
				case JsonToken.None:
					break;
				case JsonToken.StartObject:
				{
					this.ReadIntoWrappedTypeObject();
					byte[] array = this.ReadAsBytes();
					this.ReaderReadAndAssert();
					if (this.TokenType != JsonToken.EndObject)
					{
						throw JsonReaderException.Create(this, "Error reading bytes. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, this.TokenType));
					}
					this.SetToken(JsonToken.Bytes, array, false);
					return array;
				}
				case JsonToken.StartArray:
					return this.ReadArrayIntoByteArray();
				default:
				{
					if (contentToken != JsonToken.String)
					{
						goto IL_130;
					}
					string text = (string)this.Value;
					byte[] array2;
					Guid guid;
					if (text.Length == 0)
					{
						array2 = CollectionUtils.ArrayEmpty<byte>();
					}
					else if (ConvertUtils.TryConvertGuid(text, out guid))
					{
						array2 = guid.ToByteArray();
					}
					else
					{
						array2 = Convert.FromBase64String(text);
					}
					this.SetToken(JsonToken.Bytes, array2, false);
					return array2;
				}
				}
			}
			else if (contentToken != JsonToken.Null && contentToken != JsonToken.EndArray)
			{
				if (contentToken != JsonToken.Bytes)
				{
					goto IL_130;
				}
				object value = this.Value;
				if (value is Guid)
				{
					byte[] array3 = ((Guid)value).ToByteArray();
					this.SetToken(JsonToken.Bytes, array3, false);
					return array3;
				}
				return (byte[])this.Value;
			}
			return null;
			IL_130:
			throw JsonReaderException.Create(this, "Error reading bytes. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x0003F9A4 File Offset: 0x0003DBA4
		[NullableContext(1)]
		internal byte[] ReadArrayIntoByteArray()
		{
			List<byte> list = new List<byte>();
			do
			{
				if (!this.Read())
				{
					this.SetToken(JsonToken.None);
				}
			}
			while (!this.ReadArrayElementIntoByteArrayReportDone(list));
			byte[] array = list.ToArray();
			this.SetToken(JsonToken.Bytes, array, false);
			return array;
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x0003F9E8 File Offset: 0x0003DBE8
		[NullableContext(1)]
		private bool ReadArrayElementIntoByteArrayReportDone(List<byte> buffer)
		{
			JsonToken tokenType = this.TokenType;
			if (tokenType <= JsonToken.Comment)
			{
				if (tokenType == JsonToken.None)
				{
					throw JsonReaderException.Create(this, "Unexpected end when reading bytes.");
				}
				if (tokenType == JsonToken.Comment)
				{
					return false;
				}
			}
			else
			{
				if (tokenType == JsonToken.Integer)
				{
					buffer.Add(Convert.ToByte(this.Value, CultureInfo.InvariantCulture));
					return false;
				}
				if (tokenType == JsonToken.EndArray)
				{
					return true;
				}
			}
			throw JsonReaderException.Create(this, "Unexpected token when reading bytes: {0}.".FormatWith(CultureInfo.InvariantCulture, this.TokenType));
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x0003FA78 File Offset: 0x0003DC78
		public virtual double? ReadAsDouble()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken != JsonToken.None)
			{
				switch (contentToken)
				{
				case JsonToken.Integer:
				case JsonToken.Float:
				{
					object value = this.Value;
					double num;
					if (value is double)
					{
						num = (double)value;
						return new double?(num);
					}
					if (value is System.Numerics.BigInteger)
					{
						System.Numerics.BigInteger value2 = (System.Numerics.BigInteger)value;
						num = (double)value2;
					}
					else
					{
						num = Convert.ToDouble(value, CultureInfo.InvariantCulture);
					}
					this.SetToken(JsonToken.Float, num, false);
					return new double?(num);
				}
				case JsonToken.String:
					return this.ReadDoubleString((string)this.Value);
				case JsonToken.Null:
				case JsonToken.EndArray:
					goto IL_3A;
				}
				throw JsonReaderException.Create(this, "Error reading double. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
			}
			IL_3A:
			return null;
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x0003FB60 File Offset: 0x0003DD60
		internal double? ReadDoubleString(string s)
		{
			if (StringUtils.IsNullOrEmpty(s))
			{
				this.SetToken(JsonToken.Null, null, false);
				return null;
			}
			double num;
			if (double.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, this.Culture, out num))
			{
				this.SetToken(JsonToken.Float, num, false);
				return new double?(num);
			}
			this.SetToken(JsonToken.String, s, false);
			throw JsonReaderException.Create(this, "Could not convert string to double: {0}.".FormatWith(CultureInfo.InvariantCulture, s));
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x0003FBE0 File Offset: 0x0003DDE0
		public virtual bool? ReadAsBoolean()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken != JsonToken.None)
			{
				switch (contentToken)
				{
				case JsonToken.Integer:
				case JsonToken.Float:
				{
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
					this.SetToken(JsonToken.Boolean, flag, false);
					return new bool?(flag);
				}
				case JsonToken.String:
					return this.ReadBooleanString((string)this.Value);
				case JsonToken.Boolean:
					return new bool?((bool)this.Value);
				case JsonToken.Null:
				case JsonToken.EndArray:
					goto IL_3A;
				}
				throw JsonReaderException.Create(this, "Error reading boolean. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
			}
			IL_3A:
			return null;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x0003FCC8 File Offset: 0x0003DEC8
		internal bool? ReadBooleanString(string s)
		{
			if (StringUtils.IsNullOrEmpty(s))
			{
				this.SetToken(JsonToken.Null, null, false);
				return null;
			}
			bool flag;
			if (bool.TryParse(s, out flag))
			{
				this.SetToken(JsonToken.Boolean, flag, false);
				return new bool?(flag);
			}
			this.SetToken(JsonToken.String, s, false);
			throw JsonReaderException.Create(this, "Could not convert string to boolean: {0}.".FormatWith(CultureInfo.InvariantCulture, s));
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x0003FD3C File Offset: 0x0003DF3C
		public virtual decimal? ReadAsDecimal()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken != JsonToken.None)
			{
				switch (contentToken)
				{
				case JsonToken.Integer:
				case JsonToken.Float:
				{
					object value = this.Value;
					decimal num;
					if (value is decimal)
					{
						num = (decimal)value;
						return new decimal?(num);
					}
					if (value is System.Numerics.BigInteger)
					{
						System.Numerics.BigInteger value2 = (System.Numerics.BigInteger)value;
						num = (decimal)value2;
					}
					else
					{
						try
						{
							num = Convert.ToDecimal(value, CultureInfo.InvariantCulture);
						}
						catch (Exception ex)
						{
							throw JsonReaderException.Create(this, "Could not convert to decimal: {0}.".FormatWith(CultureInfo.InvariantCulture, value), ex);
						}
					}
					this.SetToken(JsonToken.Float, num, false);
					return new decimal?(num);
				}
				case JsonToken.String:
					return this.ReadDecimalString((string)this.Value);
				case JsonToken.Null:
				case JsonToken.EndArray:
					goto IL_3A;
				}
				throw JsonReaderException.Create(this, "Error reading decimal. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
			}
			IL_3A:
			return null;
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x0003FE4C File Offset: 0x0003E04C
		internal decimal? ReadDecimalString(string s)
		{
			if (StringUtils.IsNullOrEmpty(s))
			{
				this.SetToken(JsonToken.Null, null, false);
				return null;
			}
			decimal num;
			if (decimal.TryParse(s, NumberStyles.Number, this.Culture, out num))
			{
				this.SetToken(JsonToken.Float, num, false);
				return new decimal?(num);
			}
			if (ConvertUtils.DecimalTryParse(s.ToCharArray(), 0, s.Length, out num) == ParseResult.Success)
			{
				this.SetToken(JsonToken.Float, num, false);
				return new decimal?(num);
			}
			this.SetToken(JsonToken.String, s, false);
			throw JsonReaderException.Create(this, "Could not convert string to decimal: {0}.".FormatWith(CultureInfo.InvariantCulture, s));
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x0003FEF8 File Offset: 0x0003E0F8
		public virtual DateTime? ReadAsDateTime()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken <= JsonToken.String)
			{
				if (contentToken != JsonToken.None)
				{
					if (contentToken != JsonToken.String)
					{
						goto IL_9A;
					}
					return this.ReadDateTimeString((string)this.Value);
				}
			}
			else if (contentToken != JsonToken.Null && contentToken != JsonToken.EndArray)
			{
				if (contentToken != JsonToken.Date)
				{
					goto IL_9A;
				}
				object value = this.Value;
				if (value is DateTimeOffset)
				{
					this.SetToken(JsonToken.Date, ((DateTimeOffset)value).DateTime, false);
				}
				return new DateTime?((DateTime)this.Value);
			}
			return null;
			IL_9A:
			throw JsonReaderException.Create(this, "Error reading date. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, this.TokenType));
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x0003FFC4 File Offset: 0x0003E1C4
		internal DateTime? ReadDateTimeString(string s)
		{
			if (StringUtils.IsNullOrEmpty(s))
			{
				this.SetToken(JsonToken.Null, null, false);
				return null;
			}
			DateTime dateTime;
			if (DateTimeUtils.TryParseDateTime(s, this.DateTimeZoneHandling, this._dateFormatString, this.Culture, out dateTime))
			{
				dateTime = DateTimeUtils.EnsureDateTime(dateTime, this.DateTimeZoneHandling);
				this.SetToken(JsonToken.Date, dateTime, false);
				return new DateTime?(dateTime);
			}
			if (DateTime.TryParse(s, this.Culture, DateTimeStyles.RoundtripKind, out dateTime))
			{
				dateTime = DateTimeUtils.EnsureDateTime(dateTime, this.DateTimeZoneHandling);
				this.SetToken(JsonToken.Date, dateTime, false);
				return new DateTime?(dateTime);
			}
			throw JsonReaderException.Create(this, "Could not convert string to DateTime: {0}.".FormatWith(CultureInfo.InvariantCulture, s));
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00040088 File Offset: 0x0003E288
		public virtual DateTimeOffset? ReadAsDateTimeOffset()
		{
			JsonToken contentToken = this.GetContentToken();
			if (contentToken <= JsonToken.String)
			{
				if (contentToken != JsonToken.None)
				{
					if (contentToken != JsonToken.String)
					{
						goto IL_9D;
					}
					string s = (string)this.Value;
					return this.ReadDateTimeOffsetString(s);
				}
			}
			else if (contentToken != JsonToken.Null && contentToken != JsonToken.EndArray)
			{
				if (contentToken != JsonToken.Date)
				{
					goto IL_9D;
				}
				object value = this.Value;
				if (value is DateTime)
				{
					DateTime dateTime = (DateTime)value;
					this.SetToken(JsonToken.Date, new DateTimeOffset(dateTime), false);
				}
				return new DateTimeOffset?((DateTimeOffset)this.Value);
			}
			return null;
			IL_9D:
			throw JsonReaderException.Create(this, "Error reading date. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, contentToken));
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x00040154 File Offset: 0x0003E354
		internal DateTimeOffset? ReadDateTimeOffsetString(string s)
		{
			if (StringUtils.IsNullOrEmpty(s))
			{
				this.SetToken(JsonToken.Null, null, false);
				return null;
			}
			DateTimeOffset dateTimeOffset;
			if (DateTimeUtils.TryParseDateTimeOffset(s, this._dateFormatString, this.Culture, out dateTimeOffset))
			{
				this.SetToken(JsonToken.Date, dateTimeOffset, false);
				return new DateTimeOffset?(dateTimeOffset);
			}
			if (DateTimeOffset.TryParse(s, this.Culture, DateTimeStyles.RoundtripKind, out dateTimeOffset))
			{
				this.SetToken(JsonToken.Date, dateTimeOffset, false);
				return new DateTimeOffset?(dateTimeOffset);
			}
			this.SetToken(JsonToken.String, s, false);
			throw JsonReaderException.Create(this, "Could not convert string to DateTimeOffset: {0}.".FormatWith(CultureInfo.InvariantCulture, s));
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00040204 File Offset: 0x0003E404
		internal void ReaderReadAndAssert()
		{
			if (!this.Read())
			{
				throw this.CreateUnexpectedEndException();
			}
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00040218 File Offset: 0x0003E418
		[NullableContext(1)]
		internal JsonReaderException CreateUnexpectedEndException()
		{
			return JsonReaderException.Create(this, "Unexpected end when reading JSON.");
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x00040228 File Offset: 0x0003E428
		internal void ReadIntoWrappedTypeObject()
		{
			this.ReaderReadAndAssert();
			if (this.Value != null && this.Value.ToString() == "$type")
			{
				this.ReaderReadAndAssert();
				if (this.Value != null && this.Value.ToString().StartsWith("System.Byte[]", StringComparison.Ordinal))
				{
					this.ReaderReadAndAssert();
					if (this.Value.ToString() == "$value")
					{
						return;
					}
				}
			}
			throw JsonReaderException.Create(this, "Error reading bytes. Unexpected token: {0}.".FormatWith(CultureInfo.InvariantCulture, JsonToken.StartObject));
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x000402CC File Offset: 0x0003E4CC
		public void Skip()
		{
			if (this.TokenType == JsonToken.PropertyName)
			{
				this.Read();
			}
			if (JsonTokenUtils.IsStartToken(this.TokenType))
			{
				int depth = this.Depth;
				while (this.Read() && depth < this.Depth)
				{
				}
			}
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x0004031C File Offset: 0x0003E51C
		protected void SetToken(JsonToken newToken)
		{
			this.SetToken(newToken, null, true);
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00040328 File Offset: 0x0003E528
		protected void SetToken(JsonToken newToken, object value)
		{
			this.SetToken(newToken, value, true);
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x00040334 File Offset: 0x0003E534
		protected void SetToken(JsonToken newToken, object value, bool updateIndex)
		{
			this._tokenType = newToken;
			this._value = value;
			switch (newToken)
			{
			case JsonToken.StartObject:
				this._currentState = JsonReader.State.ObjectStart;
				this.Push(JsonContainerType.Object);
				return;
			case JsonToken.StartArray:
				this._currentState = JsonReader.State.ArrayStart;
				this.Push(JsonContainerType.Array);
				return;
			case JsonToken.StartConstructor:
				this._currentState = JsonReader.State.ConstructorStart;
				this.Push(JsonContainerType.Constructor);
				return;
			case JsonToken.PropertyName:
				this._currentState = JsonReader.State.Property;
				this._currentPosition.PropertyName = (string)value;
				return;
			case JsonToken.Comment:
				break;
			case JsonToken.Raw:
			case JsonToken.Integer:
			case JsonToken.Float:
			case JsonToken.String:
			case JsonToken.Boolean:
			case JsonToken.Null:
			case JsonToken.Undefined:
			case JsonToken.Date:
			case JsonToken.Bytes:
				this.SetPostValueState(updateIndex);
				break;
			case JsonToken.EndObject:
				this.ValidateEnd(JsonToken.EndObject);
				return;
			case JsonToken.EndArray:
				this.ValidateEnd(JsonToken.EndArray);
				return;
			case JsonToken.EndConstructor:
				this.ValidateEnd(JsonToken.EndConstructor);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x0004040C File Offset: 0x0003E60C
		internal void SetPostValueState(bool updateIndex)
		{
			if (this.Peek() != JsonContainerType.None || this.SupportMultipleContent)
			{
				this._currentState = JsonReader.State.PostValue;
			}
			else
			{
				this.SetFinished();
			}
			if (updateIndex)
			{
				this.UpdateScopeWithFinishedValue();
			}
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00040444 File Offset: 0x0003E644
		private void UpdateScopeWithFinishedValue()
		{
			if (this._currentPosition.HasIndex)
			{
				this._currentPosition.Position = this._currentPosition.Position + 1;
			}
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00040468 File Offset: 0x0003E668
		private void ValidateEnd(JsonToken endToken)
		{
			JsonContainerType jsonContainerType = this.Pop();
			if (this.GetTypeForCloseToken(endToken) != jsonContainerType)
			{
				throw JsonReaderException.Create(this, "JsonToken {0} is not valid for closing JsonType {1}.".FormatWith(CultureInfo.InvariantCulture, endToken, jsonContainerType));
			}
			if (this.Peek() != JsonContainerType.None || this.SupportMultipleContent)
			{
				this._currentState = JsonReader.State.PostValue;
				return;
			}
			this.SetFinished();
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x000404D4 File Offset: 0x0003E6D4
		protected void SetStateBasedOnCurrent()
		{
			JsonContainerType jsonContainerType = this.Peek();
			switch (jsonContainerType)
			{
			case JsonContainerType.None:
				this.SetFinished();
				return;
			case JsonContainerType.Object:
				this._currentState = JsonReader.State.Object;
				return;
			case JsonContainerType.Array:
				this._currentState = JsonReader.State.Array;
				return;
			case JsonContainerType.Constructor:
				this._currentState = JsonReader.State.Constructor;
				return;
			default:
				throw JsonReaderException.Create(this, "While setting the reader state back to current object an unexpected JsonType was encountered: {0}".FormatWith(CultureInfo.InvariantCulture, jsonContainerType));
			}
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00040544 File Offset: 0x0003E744
		private void SetFinished()
		{
			this._currentState = (this.SupportMultipleContent ? JsonReader.State.Start : JsonReader.State.Finished);
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x00040560 File Offset: 0x0003E760
		private JsonContainerType GetTypeForCloseToken(JsonToken token)
		{
			switch (token)
			{
			case JsonToken.EndObject:
				return JsonContainerType.Object;
			case JsonToken.EndArray:
				return JsonContainerType.Array;
			case JsonToken.EndConstructor:
				return JsonContainerType.Constructor;
			default:
				throw JsonReaderException.Create(this, "Not a valid close JsonToken: {0}".FormatWith(CultureInfo.InvariantCulture, token));
			}
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x000405A0 File Offset: 0x0003E7A0
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x000405B0 File Offset: 0x0003E7B0
		protected virtual void Dispose(bool disposing)
		{
			if (this._currentState != JsonReader.State.Closed && disposing)
			{
				this.Close();
			}
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x000405CC File Offset: 0x0003E7CC
		public virtual void Close()
		{
			this._currentState = JsonReader.State.Closed;
			this._tokenType = JsonToken.None;
			this._value = null;
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x000405E4 File Offset: 0x0003E7E4
		internal void ReadAndAssert()
		{
			if (!this.Read())
			{
				throw JsonSerializationException.Create(this, "Unexpected end when reading JSON.");
			}
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x00040600 File Offset: 0x0003E800
		internal void ReadForTypeAndAssert(JsonContract contract, bool hasConverter)
		{
			if (!this.ReadForType(contract, hasConverter))
			{
				throw JsonSerializationException.Create(this, "Unexpected end when reading JSON.");
			}
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x0004061C File Offset: 0x0003E81C
		internal bool ReadForType(JsonContract contract, bool hasConverter)
		{
			if (hasConverter)
			{
				return this.Read();
			}
			switch ((contract != null) ? contract.InternalReadType : ReadType.Read)
			{
			case ReadType.Read:
				return this.ReadAndMoveToContent();
			case ReadType.ReadAsInt32:
				this.ReadAsInt32();
				break;
			case ReadType.ReadAsInt64:
			{
				bool result = this.ReadAndMoveToContent();
				if (this.TokenType == JsonToken.Undefined)
				{
					throw JsonReaderException.Create(this, "An undefined token is not a valid {0}.".FormatWith(CultureInfo.InvariantCulture, ((contract != null) ? contract.UnderlyingType : null) ?? typeof(long)));
				}
				return result;
			}
			case ReadType.ReadAsBytes:
				this.ReadAsBytes();
				break;
			case ReadType.ReadAsString:
				this.ReadAsString();
				break;
			case ReadType.ReadAsDecimal:
				this.ReadAsDecimal();
				break;
			case ReadType.ReadAsDateTime:
				this.ReadAsDateTime();
				break;
			case ReadType.ReadAsDateTimeOffset:
				this.ReadAsDateTimeOffset();
				break;
			case ReadType.ReadAsDouble:
				this.ReadAsDouble();
				break;
			case ReadType.ReadAsBoolean:
				this.ReadAsBoolean();
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return this.TokenType > JsonToken.None;
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00040744 File Offset: 0x0003E944
		internal bool ReadAndMoveToContent()
		{
			return this.Read() && this.MoveToContent();
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0004075C File Offset: 0x0003E95C
		internal bool MoveToContent()
		{
			JsonToken tokenType = this.TokenType;
			while (tokenType == JsonToken.None || tokenType == JsonToken.Comment)
			{
				if (!this.Read())
				{
					return false;
				}
				tokenType = this.TokenType;
			}
			return true;
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x00040798 File Offset: 0x0003E998
		private JsonToken GetContentToken()
		{
			while (this.Read())
			{
				JsonToken tokenType = this.TokenType;
				if (tokenType != JsonToken.Comment)
				{
					return tokenType;
				}
			}
			this.SetToken(JsonToken.None);
			return JsonToken.None;
		}

		// Token: 0x040005D8 RID: 1496
		private JsonToken _tokenType;

		// Token: 0x040005D9 RID: 1497
		private object _value;

		// Token: 0x040005DA RID: 1498
		internal char _quoteChar;

		// Token: 0x040005DB RID: 1499
		internal JsonReader.State _currentState;

		// Token: 0x040005DC RID: 1500
		private JsonPosition _currentPosition;

		// Token: 0x040005DD RID: 1501
		private CultureInfo _culture;

		// Token: 0x040005DE RID: 1502
		private DateTimeZoneHandling _dateTimeZoneHandling;

		// Token: 0x040005DF RID: 1503
		private int? _maxDepth;

		// Token: 0x040005E0 RID: 1504
		private bool _hasExceededMaxDepth;

		// Token: 0x040005E1 RID: 1505
		internal DateParseHandling _dateParseHandling;

		// Token: 0x040005E2 RID: 1506
		internal FloatParseHandling _floatParseHandling;

		// Token: 0x040005E3 RID: 1507
		private string _dateFormatString;

		// Token: 0x040005E4 RID: 1508
		private List<JsonPosition> _stack;

		// Token: 0x020002B1 RID: 689
		[NullableContext(0)]
		protected internal enum State
		{
			// Token: 0x04000BA9 RID: 2985
			Start,
			// Token: 0x04000BAA RID: 2986
			Complete,
			// Token: 0x04000BAB RID: 2987
			Property,
			// Token: 0x04000BAC RID: 2988
			ObjectStart,
			// Token: 0x04000BAD RID: 2989
			Object,
			// Token: 0x04000BAE RID: 2990
			ArrayStart,
			// Token: 0x04000BAF RID: 2991
			Array,
			// Token: 0x04000BB0 RID: 2992
			Closed,
			// Token: 0x04000BB1 RID: 2993
			PostValue,
			// Token: 0x04000BB2 RID: 2994
			ConstructorStart,
			// Token: 0x04000BB3 RID: 2995
			Constructor,
			// Token: 0x04000BB4 RID: 2996
			Error,
			// Token: 0x04000BB5 RID: 2997
			Finished
		}
	}
}
