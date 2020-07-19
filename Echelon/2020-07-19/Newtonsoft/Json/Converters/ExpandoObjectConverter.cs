using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000200 RID: 512
	[NullableContext(1)]
	[Nullable(0)]
	public class ExpandoObjectConverter : JsonConverter
	{
		// Token: 0x060014AB RID: 5291 RVA: 0x0006DE2C File Offset: 0x0006C02C
		public override void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer)
		{
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x0006DE30 File Offset: 0x0006C030
		[return: Nullable(2)]
		public override object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer)
		{
			return this.ReadValue(reader);
		}

		// Token: 0x060014AD RID: 5293 RVA: 0x0006DE3C File Offset: 0x0006C03C
		[return: Nullable(2)]
		private object ReadValue(JsonReader reader)
		{
			if (!reader.MoveToContent())
			{
				throw JsonSerializationException.Create(reader, "Unexpected end when reading ExpandoObject.");
			}
			JsonToken tokenType = reader.TokenType;
			if (tokenType == JsonToken.StartObject)
			{
				return this.ReadObject(reader);
			}
			if (tokenType == JsonToken.StartArray)
			{
				return this.ReadList(reader);
			}
			if (JsonTokenUtils.IsPrimitiveToken(reader.TokenType))
			{
				return reader.Value;
			}
			throw JsonSerializationException.Create(reader, "Unexpected token when converting ExpandoObject: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
		}

		// Token: 0x060014AE RID: 5294 RVA: 0x0006DEC8 File Offset: 0x0006C0C8
		private object ReadList(JsonReader reader)
		{
			IList<object> list = new List<object>();
			while (reader.Read())
			{
				JsonToken tokenType = reader.TokenType;
				if (tokenType != JsonToken.Comment)
				{
					if (tokenType == JsonToken.EndArray)
					{
						return list;
					}
					object item = this.ReadValue(reader);
					list.Add(item);
				}
			}
			throw JsonSerializationException.Create(reader, "Unexpected end when reading ExpandoObject.");
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x0006DF24 File Offset: 0x0006C124
		private object ReadObject(JsonReader reader)
		{
			IDictionary<string, object> dictionary = new ExpandoObject();
			while (reader.Read())
			{
				JsonToken tokenType = reader.TokenType;
				if (tokenType != JsonToken.PropertyName)
				{
					if (tokenType != JsonToken.Comment)
					{
						if (tokenType == JsonToken.EndObject)
						{
							return dictionary;
						}
					}
				}
				else
				{
					string key = reader.Value.ToString();
					if (!reader.Read())
					{
						throw JsonSerializationException.Create(reader, "Unexpected end when reading ExpandoObject.");
					}
					object value = this.ReadValue(reader);
					dictionary[key] = value;
				}
			}
			throw JsonSerializationException.Create(reader, "Unexpected end when reading ExpandoObject.");
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x0006DFB0 File Offset: 0x0006C1B0
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(ExpandoObject);
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x060014B1 RID: 5297 RVA: 0x0006DFC4 File Offset: 0x0006C1C4
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}
	}
}
