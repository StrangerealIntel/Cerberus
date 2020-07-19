using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json
{
	// Token: 0x02000137 RID: 311
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class JsonConverter<[Nullable(2)] T> : JsonConverter
	{
		// Token: 0x06000A2D RID: 2605 RVA: 0x0003EA40 File Offset: 0x0003CC40
		public sealed override void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer)
		{
			if (!((value != null) ? (value is T) : ReflectionUtils.IsNullable(typeof(T))))
			{
				throw new JsonSerializationException("Converter cannot write specified value to JSON. {0} is required.".FormatWith(CultureInfo.InvariantCulture, typeof(T)));
			}
			this.WriteJson(writer, (T)((object)value), serializer);
		}

		// Token: 0x06000A2E RID: 2606
		public abstract void WriteJson(JsonWriter writer, [AllowNull] T value, JsonSerializer serializer);

		// Token: 0x06000A2F RID: 2607 RVA: 0x0003EAA8 File Offset: 0x0003CCA8
		[return: Nullable(2)]
		public sealed override object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer)
		{
			bool flag = existingValue == null;
			if (!flag && !(existingValue is T))
			{
				throw new JsonSerializationException("Converter cannot read JSON with the specified existing value. {0} is required.".FormatWith(CultureInfo.InvariantCulture, typeof(T)));
			}
			return this.ReadJson(reader, objectType, flag ? default(T) : ((T)((object)existingValue)), !flag, serializer);
		}

		// Token: 0x06000A30 RID: 2608
		public abstract T ReadJson(JsonReader reader, Type objectType, [AllowNull] T existingValue, bool hasExistingValue, JsonSerializer serializer);

		// Token: 0x06000A31 RID: 2609 RVA: 0x0003EB1C File Offset: 0x0003CD1C
		public sealed override bool CanConvert(Type objectType)
		{
			return typeof(T).IsAssignableFrom(objectType);
		}
	}
}
