using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000201 RID: 513
	[NullableContext(1)]
	[Nullable(0)]
	public class IsoDateTimeConverter : DateTimeConverterBase
	{
		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x060014B3 RID: 5299 RVA: 0x0006DFD0 File Offset: 0x0006C1D0
		// (set) Token: 0x060014B4 RID: 5300 RVA: 0x0006DFD8 File Offset: 0x0006C1D8
		public DateTimeStyles DateTimeStyles
		{
			get
			{
				return this._dateTimeStyles;
			}
			set
			{
				this._dateTimeStyles = value;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x060014B5 RID: 5301 RVA: 0x0006DFE4 File Offset: 0x0006C1E4
		// (set) Token: 0x060014B6 RID: 5302 RVA: 0x0006DFF8 File Offset: 0x0006C1F8
		[Nullable(2)]
		public string DateTimeFormat
		{
			[NullableContext(2)]
			get
			{
				return this._dateTimeFormat ?? string.Empty;
			}
			[NullableContext(2)]
			set
			{
				this._dateTimeFormat = (StringUtils.IsNullOrEmpty(value) ? null : value);
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x060014B7 RID: 5303 RVA: 0x0006E014 File Offset: 0x0006C214
		// (set) Token: 0x060014B8 RID: 5304 RVA: 0x0006E028 File Offset: 0x0006C228
		public CultureInfo Culture
		{
			get
			{
				return this._culture ?? CultureInfo.CurrentCulture;
			}
			set
			{
				this._culture = value;
			}
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x0006E034 File Offset: 0x0006C234
		public override void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer)
		{
			string value2;
			if (value is DateTime)
			{
				DateTime dateTime = (DateTime)value;
				if ((this._dateTimeStyles & DateTimeStyles.AdjustToUniversal) == DateTimeStyles.AdjustToUniversal || (this._dateTimeStyles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.AssumeUniversal)
				{
					dateTime = dateTime.ToUniversalTime();
				}
				value2 = dateTime.ToString(this._dateTimeFormat ?? "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK", this.Culture);
			}
			else
			{
				if (!(value is DateTimeOffset))
				{
					throw new JsonSerializationException("Unexpected value when converting date. Expected DateTime or DateTimeOffset, got {0}.".FormatWith(CultureInfo.InvariantCulture, ReflectionUtils.GetObjectType(value)));
				}
				DateTimeOffset dateTimeOffset = (DateTimeOffset)value;
				if ((this._dateTimeStyles & DateTimeStyles.AdjustToUniversal) == DateTimeStyles.AdjustToUniversal || (this._dateTimeStyles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.AssumeUniversal)
				{
					dateTimeOffset = dateTimeOffset.ToUniversalTime();
				}
				value2 = dateTimeOffset.ToString(this._dateTimeFormat ?? "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK", this.Culture);
			}
			writer.WriteValue(value2);
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x0006E128 File Offset: 0x0006C328
		[return: Nullable(2)]
		public override object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer)
		{
			bool flag = ReflectionUtils.IsNullableType(objectType);
			if (reader.TokenType == JsonToken.Null)
			{
				if (!flag)
				{
					throw JsonSerializationException.Create(reader, "Cannot convert null value to {0}.".FormatWith(CultureInfo.InvariantCulture, objectType));
				}
				return null;
			}
			else
			{
				Type left = flag ? Nullable.GetUnderlyingType(objectType) : objectType;
				if (reader.TokenType == JsonToken.Date)
				{
					if (left == typeof(DateTimeOffset))
					{
						if (!(reader.Value is DateTimeOffset))
						{
							return new DateTimeOffset((DateTime)reader.Value);
						}
						return reader.Value;
					}
					else
					{
						object value = reader.Value;
						if (value is DateTimeOffset)
						{
							return ((DateTimeOffset)value).DateTime;
						}
						return reader.Value;
					}
				}
				else
				{
					if (reader.TokenType != JsonToken.String)
					{
						throw JsonSerializationException.Create(reader, "Unexpected token parsing date. Expected String, got {0}.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
					}
					object value2 = reader.Value;
					string text = (value2 != null) ? value2.ToString() : null;
					if (StringUtils.IsNullOrEmpty(text) && flag)
					{
						return null;
					}
					if (left == typeof(DateTimeOffset))
					{
						if (!StringUtils.IsNullOrEmpty(this._dateTimeFormat))
						{
							return DateTimeOffset.ParseExact(text, this._dateTimeFormat, this.Culture, this._dateTimeStyles);
						}
						return DateTimeOffset.Parse(text, this.Culture, this._dateTimeStyles);
					}
					else
					{
						if (!StringUtils.IsNullOrEmpty(this._dateTimeFormat))
						{
							return DateTime.ParseExact(text, this._dateTimeFormat, this.Culture, this._dateTimeStyles);
						}
						return DateTime.Parse(text, this.Culture, this._dateTimeStyles);
					}
				}
			}
		}

		// Token: 0x0400094D RID: 2381
		private const string DefaultDateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";

		// Token: 0x0400094E RID: 2382
		private DateTimeStyles _dateTimeStyles = DateTimeStyles.RoundtripKind;

		// Token: 0x0400094F RID: 2383
		[Nullable(2)]
		private string _dateTimeFormat;

		// Token: 0x04000950 RID: 2384
		[Nullable(2)]
		private CultureInfo _culture;
	}
}
