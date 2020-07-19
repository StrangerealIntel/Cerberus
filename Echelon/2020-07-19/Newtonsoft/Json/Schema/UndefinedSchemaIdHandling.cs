using System;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020001CD RID: 461
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public enum UndefinedSchemaIdHandling
	{
		// Token: 0x040008C6 RID: 2246
		None,
		// Token: 0x040008C7 RID: 2247
		UseTypeName,
		// Token: 0x040008C8 RID: 2248
		UseAssemblyQualifiedName
	}
}
