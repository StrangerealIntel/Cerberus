using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020001CE RID: 462
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	public class ValidationEventArgs : EventArgs
	{
		// Token: 0x060011EA RID: 4586 RVA: 0x00063360 File Offset: 0x00061560
		internal ValidationEventArgs(JsonSchemaException ex)
		{
			ValidationUtils.ArgumentNotNull(ex, "ex");
			this._ex = ex;
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x060011EB RID: 4587 RVA: 0x0006337C File Offset: 0x0006157C
		public JsonSchemaException Exception
		{
			get
			{
				return this._ex;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x060011EC RID: 4588 RVA: 0x00063384 File Offset: 0x00061584
		public string Path
		{
			get
			{
				return this._ex.Path;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x060011ED RID: 4589 RVA: 0x00063394 File Offset: 0x00061594
		public string Message
		{
			get
			{
				return this._ex.Message;
			}
		}

		// Token: 0x040008C9 RID: 2249
		private readonly JsonSchemaException _ex;
	}
}
