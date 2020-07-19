using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x02000136 RID: 310
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class JsonConverter
	{
		// Token: 0x06000A27 RID: 2599
		public abstract void WriteJson(JsonWriter writer, [Nullable(2)] object value, JsonSerializer serializer);

		// Token: 0x06000A28 RID: 2600
		[return: Nullable(2)]
		public abstract object ReadJson(JsonReader reader, Type objectType, [Nullable(2)] object existingValue, JsonSerializer serializer);

		// Token: 0x06000A29 RID: 2601
		public abstract bool CanConvert(Type objectType);

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000A2A RID: 2602 RVA: 0x0003EA30 File Offset: 0x0003CC30
		public virtual bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x0003EA34 File Offset: 0x0003CC34
		public virtual bool CanWrite
		{
			get
			{
				return true;
			}
		}
	}
}
