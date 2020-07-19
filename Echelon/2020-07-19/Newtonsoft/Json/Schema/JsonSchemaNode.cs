using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020001C8 RID: 456
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	internal class JsonSchemaNode
	{
		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x060011D0 RID: 4560 RVA: 0x00062934 File Offset: 0x00060B34
		public string Id { get; }

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x060011D1 RID: 4561 RVA: 0x0006293C File Offset: 0x00060B3C
		public ReadOnlyCollection<JsonSchema> Schemas { get; }

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x060011D2 RID: 4562 RVA: 0x00062944 File Offset: 0x00060B44
		public Dictionary<string, JsonSchemaNode> Properties { get; }

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x0006294C File Offset: 0x00060B4C
		public Dictionary<string, JsonSchemaNode> PatternProperties { get; }

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x060011D4 RID: 4564 RVA: 0x00062954 File Offset: 0x00060B54
		public List<JsonSchemaNode> Items { get; }

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x0006295C File Offset: 0x00060B5C
		// (set) Token: 0x060011D6 RID: 4566 RVA: 0x00062964 File Offset: 0x00060B64
		public JsonSchemaNode AdditionalProperties { get; set; }

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x060011D7 RID: 4567 RVA: 0x00062970 File Offset: 0x00060B70
		// (set) Token: 0x060011D8 RID: 4568 RVA: 0x00062978 File Offset: 0x00060B78
		public JsonSchemaNode AdditionalItems { get; set; }

		// Token: 0x060011D9 RID: 4569 RVA: 0x00062984 File Offset: 0x00060B84
		public JsonSchemaNode(JsonSchema schema)
		{
			this.Schemas = new ReadOnlyCollection<JsonSchema>(new JsonSchema[]
			{
				schema
			});
			this.Properties = new Dictionary<string, JsonSchemaNode>();
			this.PatternProperties = new Dictionary<string, JsonSchemaNode>();
			this.Items = new List<JsonSchemaNode>();
			this.Id = JsonSchemaNode.GetId(this.Schemas);
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x000629E4 File Offset: 0x00060BE4
		private JsonSchemaNode(JsonSchemaNode source, JsonSchema schema)
		{
			this.Schemas = new ReadOnlyCollection<JsonSchema>(source.Schemas.Union(new JsonSchema[]
			{
				schema
			}).ToList<JsonSchema>());
			this.Properties = new Dictionary<string, JsonSchemaNode>(source.Properties);
			this.PatternProperties = new Dictionary<string, JsonSchemaNode>(source.PatternProperties);
			this.Items = new List<JsonSchemaNode>(source.Items);
			this.AdditionalProperties = source.AdditionalProperties;
			this.AdditionalItems = source.AdditionalItems;
			this.Id = JsonSchemaNode.GetId(this.Schemas);
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x00062A7C File Offset: 0x00060C7C
		public JsonSchemaNode Combine(JsonSchema schema)
		{
			return new JsonSchemaNode(this, schema);
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x00062A88 File Offset: 0x00060C88
		public static string GetId(IEnumerable<JsonSchema> schemata)
		{
			return string.Join("-", (from s in schemata
			select s.InternalId).OrderBy((string id) => id, StringComparer.Ordinal));
		}
	}
}
