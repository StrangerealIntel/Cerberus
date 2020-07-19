using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x020001FD RID: 509
	public abstract class DateTimeConverterBase : JsonConverter
	{
		// Token: 0x0600149C RID: 5276 RVA: 0x0006D570 File Offset: 0x0006B770
		[NullableContext(1)]
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(DateTime) || objectType == typeof(DateTime?) || (objectType == typeof(DateTimeOffset) || objectType == typeof(DateTimeOffset?));
		}
	}
}
