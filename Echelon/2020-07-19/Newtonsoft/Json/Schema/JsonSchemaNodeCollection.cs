using System;
using System.Collections.ObjectModel;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020001C9 RID: 457
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaNodeCollection : KeyedCollection<string, JsonSchemaNode>
	{
		// Token: 0x060011DD RID: 4573 RVA: 0x00062AF8 File Offset: 0x00060CF8
		protected override string GetKeyForItem(JsonSchemaNode item)
		{
			return item.Id;
		}
	}
}
