using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x0200014F RID: 335
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class JsonWriterException : JsonException
	{
		// Token: 0x170002CC RID: 716
		// (get) Token: 0x06000CAB RID: 3243 RVA: 0x000495F0 File Offset: 0x000477F0
		[Nullable(2)]
		public string Path { [NullableContext(2)] get; }

		// Token: 0x06000CAC RID: 3244 RVA: 0x000495F8 File Offset: 0x000477F8
		public JsonWriterException()
		{
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x00049600 File Offset: 0x00047800
		public JsonWriterException(string message) : base(message)
		{
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0004960C File Offset: 0x0004780C
		public JsonWriterException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00049618 File Offset: 0x00047818
		public JsonWriterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00049624 File Offset: 0x00047824
		public JsonWriterException(string message, string path, [Nullable(2)] Exception innerException) : base(message, innerException)
		{
			this.Path = path;
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00049638 File Offset: 0x00047838
		internal static JsonWriterException Create(JsonWriter writer, string message, [Nullable(2)] Exception ex)
		{
			return JsonWriterException.Create(writer.ContainerPath, message, ex);
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x00049648 File Offset: 0x00047848
		internal static JsonWriterException Create(string path, string message, [Nullable(2)] Exception ex)
		{
			message = JsonPosition.FormatMessage(null, path, message);
			return new JsonWriterException(message, path, ex);
		}
	}
}
