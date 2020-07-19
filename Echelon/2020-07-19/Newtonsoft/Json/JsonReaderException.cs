using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x02000144 RID: 324
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class JsonReaderException : JsonException
	{
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000ABF RID: 2751 RVA: 0x000407CC File Offset: 0x0003E9CC
		public int LineNumber { get; }

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000AC0 RID: 2752 RVA: 0x000407D4 File Offset: 0x0003E9D4
		public int LinePosition { get; }

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x000407DC File Offset: 0x0003E9DC
		[Nullable(2)]
		public string Path { [NullableContext(2)] get; }

		// Token: 0x06000AC2 RID: 2754 RVA: 0x000407E4 File Offset: 0x0003E9E4
		public JsonReaderException()
		{
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x000407EC File Offset: 0x0003E9EC
		public JsonReaderException(string message) : base(message)
		{
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x000407F8 File Offset: 0x0003E9F8
		public JsonReaderException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x00040804 File Offset: 0x0003EA04
		public JsonReaderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00040810 File Offset: 0x0003EA10
		public JsonReaderException(string message, string path, int lineNumber, int linePosition, [Nullable(2)] Exception innerException) : base(message, innerException)
		{
			this.Path = path;
			this.LineNumber = lineNumber;
			this.LinePosition = linePosition;
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x00040834 File Offset: 0x0003EA34
		internal static JsonReaderException Create(JsonReader reader, string message)
		{
			return JsonReaderException.Create(reader, message, null);
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00040840 File Offset: 0x0003EA40
		internal static JsonReaderException Create(JsonReader reader, string message, [Nullable(2)] Exception ex)
		{
			return JsonReaderException.Create(reader as IJsonLineInfo, reader.Path, message, ex);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00040858 File Offset: 0x0003EA58
		internal static JsonReaderException Create([Nullable(2)] IJsonLineInfo lineInfo, string path, string message, [Nullable(2)] Exception ex)
		{
			message = JsonPosition.FormatMessage(lineInfo, path, message);
			int lineNumber;
			int linePosition;
			if (lineInfo != null && lineInfo.HasLineInfo())
			{
				lineNumber = lineInfo.LineNumber;
				linePosition = lineInfo.LinePosition;
			}
			else
			{
				lineNumber = 0;
				linePosition = 0;
			}
			return new JsonReaderException(message, path, lineNumber, linePosition, ex);
		}
	}
}
