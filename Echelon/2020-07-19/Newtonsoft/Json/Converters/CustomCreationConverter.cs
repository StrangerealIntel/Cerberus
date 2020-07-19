using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020001FA RID: 506
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class CustomCreationConverter<[Nullable(2)] T> : JsonConverter
	{
		// Token: 0x0600148C RID: 5260 RVA: 0x0006CEB4 File Offset: 0x0006B0B4
		public override void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer)
		{
			throw new NotSupportedException("CustomCreationConverter should only be used while deserializing.");
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x0006CEC0 File Offset: 0x0006B0C0
		[return: Nullable(2)]
		public override object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			T t = this.Create(objectType);
			if (t == null)
			{
				throw new JsonSerializationException("No object created.");
			}
			serializer.Populate(reader, t);
			return t;
		}

		// Token: 0x0600148E RID: 5262
		public abstract T Create(Type objectType);

		// Token: 0x0600148F RID: 5263 RVA: 0x0006CF14 File Offset: 0x0006B114
		public override bool CanConvert(Type objectType)
		{
			return typeof(T).IsAssignableFrom(objectType);
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06001490 RID: 5264 RVA: 0x0006CF28 File Offset: 0x0006B128
		public override bool CanWrite
		{
			get
			{
				return false;
			}
		}
	}
}
