using System;
using System.IO;

namespace Echelon.Stealer.Browsers.Helpers.NoiseMe.Drags.App.Models.JSON
{
	// Token: 0x02000030 RID: 48
	public static class JsonExt
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x00009790 File Offset: 0x00007990
		public static JsonValue FromJSON(this string json)
		{
			return JsonValue.Load(new StringReader(json));
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000097A0 File Offset: 0x000079A0
		public static string ToJSON<T>(this T instance)
		{
			return JsonValue.ToJsonValue<T>(instance);
		}
	}
}
