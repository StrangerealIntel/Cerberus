using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000207 RID: 519
	[NullableContext(1)]
	[Nullable(0)]
	public class VersionConverter : JsonConverter
	{
		// Token: 0x060014E2 RID: 5346 RVA: 0x0006F004 File Offset: 0x0006D204
		public override void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			if (value is Version)
			{
				writer.WriteValue(value.ToString());
				return;
			}
			throw new JsonSerializationException("Expected Version object value");
		}

		// Token: 0x060014E3 RID: 5347 RVA: 0x0006F038 File Offset: 0x0006D238
		[return: Nullable(2)]
		public override object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			if (reader.TokenType == JsonToken.String)
			{
				try
				{
					return new Version((string)reader.Value);
				}
				catch (Exception ex)
				{
					throw JsonSerializationException.Create(reader, "Error parsing version string: {0}".FormatWith(CultureInfo.InvariantCulture, reader.Value), ex);
				}
			}
			throw JsonSerializationException.Create(reader, "Unexpected token or value when parsing version. Token: {0}, Value: {1}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType, reader.Value));
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x0006F0D0 File Offset: 0x0006D2D0
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(Version);
		}
	}
}
