using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x02000146 RID: 326
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class JsonSerializationException : JsonException
	{
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x000408B0 File Offset: 0x0003EAB0
		public int LineNumber { get; }

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000ACC RID: 2764 RVA: 0x000408B8 File Offset: 0x0003EAB8
		public int LinePosition { get; }

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000ACD RID: 2765 RVA: 0x000408C0 File Offset: 0x0003EAC0
		[Nullable(2)]
		public string Path { [NullableContext(2)] get; }

		// Token: 0x06000ACE RID: 2766 RVA: 0x000408C8 File Offset: 0x0003EAC8
		public JsonSerializationException()
		{
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x000408D0 File Offset: 0x0003EAD0
		public JsonSerializationException(string message) : base(message)
		{
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x000408DC File Offset: 0x0003EADC
		public JsonSerializationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x000408E8 File Offset: 0x0003EAE8
		public JsonSerializationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x000408F4 File Offset: 0x0003EAF4
		public JsonSerializationException(string message, string path, int lineNumber, int linePosition, [Nullable(2)] Exception innerException) : base(message, innerException)
		{
			this.Path = path;
			this.LineNumber = lineNumber;
			this.LinePosition = linePosition;
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00040918 File Offset: 0x0003EB18
		internal static JsonSerializationException Create(JsonReader reader, string message)
		{
			return JsonSerializationException.Create(reader, message, null);
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00040924 File Offset: 0x0003EB24
		internal static JsonSerializationException Create(JsonReader reader, string message, [Nullable(2)] Exception ex)
		{
			return JsonSerializationException.Create(reader as IJsonLineInfo, reader.Path, message, ex);
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0004093C File Offset: 0x0003EB3C
		internal static JsonSerializationException Create([Nullable(2)] IJsonLineInfo lineInfo, string path, string message, [Nullable(2)] Exception ex)
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
			return new JsonSerializationException(message, path, lineNumber, linePosition, ex);
		}
	}
}
