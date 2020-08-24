using System;
using System.IO;
using RedLine.Logic.Json;

namespace RedLine.Logic.Extensions
{
	// Token: 0x02000064 RID: 100
	public static class JsonExt
	{
		// Token: 0x060002B3 RID: 691 RVA: 0x0000C0DD File Offset: 0x0000A2DD
		public static JsonValue FromJSON(this string json)
		{
			return JsonValue.Load(new StringReader(json));
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000C0EA File Offset: 0x0000A2EA
		public static string ToJSON<T>(this T instance)
		{
			return JsonValue.ToJsonValue<T>(instance);
		}
	}
}
