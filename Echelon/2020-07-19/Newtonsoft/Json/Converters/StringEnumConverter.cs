using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000205 RID: 517
	[NullableContext(1)]
	[Nullable(0)]
	public class StringEnumConverter : JsonConverter
	{
		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x060014CF RID: 5327 RVA: 0x0006EAD0 File Offset: 0x0006CCD0
		// (set) Token: 0x060014D0 RID: 5328 RVA: 0x0006EAE8 File Offset: 0x0006CCE8
		[Obsolete("StringEnumConverter.CamelCaseText is obsolete. Set StringEnumConverter.NamingStrategy with CamelCaseNamingStrategy instead.")]
		public bool CamelCaseText
		{
			get
			{
				return this.NamingStrategy is CamelCaseNamingStrategy;
			}
			set
			{
				if (value)
				{
					if (this.NamingStrategy is CamelCaseNamingStrategy)
					{
						return;
					}
					this.NamingStrategy = new CamelCaseNamingStrategy();
					return;
				}
				else
				{
					if (!(this.NamingStrategy is CamelCaseNamingStrategy))
					{
						return;
					}
					this.NamingStrategy = null;
					return;
				}
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x060014D1 RID: 5329 RVA: 0x0006EB28 File Offset: 0x0006CD28
		// (set) Token: 0x060014D2 RID: 5330 RVA: 0x0006EB30 File Offset: 0x0006CD30
		[Nullable(2)]
		public NamingStrategy NamingStrategy { [NullableContext(2)] get; [NullableContext(2)] set; }

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x060014D3 RID: 5331 RVA: 0x0006EB3C File Offset: 0x0006CD3C
		// (set) Token: 0x060014D4 RID: 5332 RVA: 0x0006EB44 File Offset: 0x0006CD44
		public bool AllowIntegerValues { get; set; } = true;

		// Token: 0x060014D5 RID: 5333 RVA: 0x0006EB50 File Offset: 0x0006CD50
		public StringEnumConverter()
		{
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x0006EB60 File Offset: 0x0006CD60
		[Obsolete("StringEnumConverter(bool) is obsolete. Create a converter with StringEnumConverter(NamingStrategy, bool) instead.")]
		public StringEnumConverter(bool camelCaseText)
		{
			if (camelCaseText)
			{
				this.NamingStrategy = new CamelCaseNamingStrategy();
			}
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x0006EB80 File Offset: 0x0006CD80
		public StringEnumConverter(NamingStrategy namingStrategy, bool allowIntegerValues = true)
		{
			this.NamingStrategy = namingStrategy;
			this.AllowIntegerValues = allowIntegerValues;
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x0006EBA0 File Offset: 0x0006CDA0
		public StringEnumConverter(Type namingStrategyType)
		{
			ValidationUtils.ArgumentNotNull(namingStrategyType, "namingStrategyType");
			this.NamingStrategy = JsonTypeReflector.CreateNamingStrategyInstance(namingStrategyType, null);
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x0006EBC8 File Offset: 0x0006CDC8
		public StringEnumConverter(Type namingStrategyType, object[] namingStrategyParameters)
		{
			ValidationUtils.ArgumentNotNull(namingStrategyType, "namingStrategyType");
			this.NamingStrategy = JsonTypeReflector.CreateNamingStrategyInstance(namingStrategyType, namingStrategyParameters);
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x0006EBF0 File Offset: 0x0006CDF0
		public StringEnumConverter(Type namingStrategyType, object[] namingStrategyParameters, bool allowIntegerValues)
		{
			ValidationUtils.ArgumentNotNull(namingStrategyType, "namingStrategyType");
			this.NamingStrategy = JsonTypeReflector.CreateNamingStrategyInstance(namingStrategyType, namingStrategyParameters);
			this.AllowIntegerValues = allowIntegerValues;
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x0006EC20 File Offset: 0x0006CE20
		public override void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			Enum @enum = (Enum)value;
			string value2;
			if (EnumUtils.TryToString(@enum.GetType(), value, this.NamingStrategy, out value2))
			{
				writer.WriteValue(value2);
				return;
			}
			if (!this.AllowIntegerValues)
			{
				throw JsonSerializationException.Create(null, writer.ContainerPath, "Integer value {0} is not allowed.".FormatWith(CultureInfo.InvariantCulture, @enum.ToString("D")), null);
			}
			writer.WriteValue(value);
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x0006ECA0 File Offset: 0x0006CEA0
		[return: Nullable(2)]
		public override object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType != JsonToken.Null)
			{
				bool flag = ReflectionUtils.IsNullableType(objectType);
				Type type = flag ? Nullable.GetUnderlyingType(objectType) : objectType;
				try
				{
					if (reader.TokenType == JsonToken.String)
					{
						object value = reader.Value;
						string value2 = (value != null) ? value.ToString() : null;
						if (StringUtils.IsNullOrEmpty(value2) && flag)
						{
							return null;
						}
						return EnumUtils.ParseEnum(type, this.NamingStrategy, value2, !this.AllowIntegerValues);
					}
					else if (reader.TokenType == JsonToken.Integer)
					{
						if (!this.AllowIntegerValues)
						{
							throw JsonSerializationException.Create(reader, "Integer value {0} is not allowed.".FormatWith(CultureInfo.InvariantCulture, reader.Value));
						}
						return ConvertUtils.ConvertOrCast(reader.Value, CultureInfo.InvariantCulture, type);
					}
				}
				catch (Exception ex)
				{
					throw JsonSerializationException.Create(reader, "Error converting value {0} to type '{1}'.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(reader.Value), objectType), ex);
				}
				throw JsonSerializationException.Create(reader, "Unexpected token {0} when parsing enum.".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			if (!ReflectionUtils.IsNullableType(objectType))
			{
				throw JsonSerializationException.Create(reader, "Cannot convert null value to {0}.".FormatWith(CultureInfo.InvariantCulture, objectType));
			}
			return null;
		}

		// Token: 0x060014DD RID: 5341 RVA: 0x0006EDF8 File Offset: 0x0006CFF8
		public override bool CanConvert(Type objectType)
		{
			return (ReflectionUtils.IsNullableType(objectType) ? Nullable.GetUnderlyingType(objectType) : objectType).IsEnum();
		}
	}
}
