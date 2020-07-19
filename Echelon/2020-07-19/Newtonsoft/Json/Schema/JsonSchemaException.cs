using System;
using System.Runtime.Serialization;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x020001C4 RID: 452
	[Obsolete("JSON Schema validation has been moved to its own package. See https://www.newtonsoft.com/jsonschema for more details.")]
	[Serializable]
	public class JsonSchemaException : JsonException
	{
		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x000614B0 File Offset: 0x0005F6B0
		public int LineNumber { get; }

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06001179 RID: 4473 RVA: 0x000614B8 File Offset: 0x0005F6B8
		public int LinePosition { get; }

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x0600117A RID: 4474 RVA: 0x000614C0 File Offset: 0x0005F6C0
		public string Path { get; }

		// Token: 0x0600117B RID: 4475 RVA: 0x000614C8 File Offset: 0x0005F6C8
		public JsonSchemaException()
		{
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x000614D0 File Offset: 0x0005F6D0
		public JsonSchemaException(string message) : base(message)
		{
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x000614DC File Offset: 0x0005F6DC
		public JsonSchemaException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x000614E8 File Offset: 0x0005F6E8
		public JsonSchemaException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x000614F4 File Offset: 0x0005F6F4
		internal JsonSchemaException(string message, Exception innerException, string path, int lineNumber, int linePosition) : base(message, innerException)
		{
			this.Path = path;
			this.LineNumber = lineNumber;
			this.LinePosition = linePosition;
		}
	}
}
