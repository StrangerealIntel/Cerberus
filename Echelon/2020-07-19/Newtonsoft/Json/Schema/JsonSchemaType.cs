using System;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020001CB RID: 459
	[Flags]
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public enum JsonSchemaType
	{
		// Token: 0x040008BA RID: 2234
		None = 0,
		// Token: 0x040008BB RID: 2235
		String = 1,
		// Token: 0x040008BC RID: 2236
		Float = 2,
		// Token: 0x040008BD RID: 2237
		Integer = 4,
		// Token: 0x040008BE RID: 2238
		Boolean = 8,
		// Token: 0x040008BF RID: 2239
		Object = 16,
		// Token: 0x040008C0 RID: 2240
		Array = 32,
		// Token: 0x040008C1 RID: 2241
		Null = 64,
		// Token: 0x040008C2 RID: 2242
		Any = 127
	}
}
