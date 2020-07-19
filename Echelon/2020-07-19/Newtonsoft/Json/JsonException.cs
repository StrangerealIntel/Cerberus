using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x0200013B RID: 315
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class JsonException : Exception
	{
		// Token: 0x06000A3A RID: 2618 RVA: 0x0003EB9C File Offset: 0x0003CD9C
		public JsonException()
		{
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0003EBA4 File Offset: 0x0003CDA4
		public JsonException(string message) : base(message)
		{
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x0003EBB0 File Offset: 0x0003CDB0
		public JsonException(string message, [Nullable(2)] Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0003EBBC File Offset: 0x0003CDBC
		public JsonException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x0003EBC8 File Offset: 0x0003CDC8
		internal static JsonException Create(IJsonLineInfo lineInfo, string path, string message)
		{
			message = JsonPosition.FormatMessage(lineInfo, path, message);
			return new JsonException(message);
		}
	}
}
