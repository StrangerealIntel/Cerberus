using System;
using System.Collections.Generic;
using System.Linq;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020001CA RID: 458
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class JsonSchemaResolver
	{
		// Token: 0x170003DF RID: 991
		// (get) Token: 0x060011DF RID: 4575 RVA: 0x00062B08 File Offset: 0x00060D08
		// (set) Token: 0x060011E0 RID: 4576 RVA: 0x00062B10 File Offset: 0x00060D10
		public IList<JsonSchema> LoadedSchemas { get; protected set; }

		// Token: 0x060011E1 RID: 4577 RVA: 0x00062B1C File Offset: 0x00060D1C
		public JsonSchemaResolver()
		{
			this.LoadedSchemas = new List<JsonSchema>();
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x00062B30 File Offset: 0x00060D30
		public virtual JsonSchema GetSchema(string reference)
		{
			JsonSchema jsonSchema = this.LoadedSchemas.SingleOrDefault((JsonSchema s) => string.Equals(s.Id, reference, StringComparison.Ordinal));
			if (jsonSchema == null)
			{
				jsonSchema = this.LoadedSchemas.SingleOrDefault((JsonSchema s) => string.Equals(s.Location, reference, StringComparison.Ordinal));
			}
			return jsonSchema;
		}
	}
}
