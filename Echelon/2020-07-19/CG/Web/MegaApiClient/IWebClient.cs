using System;
using System.IO;

namespace CG.Web.MegaApiClient
{
	// Token: 0x020000F1 RID: 241
	public interface IWebClient
	{
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000841 RID: 2113
		// (set) Token: 0x06000842 RID: 2114
		int BufferSize { get; set; }

		// Token: 0x06000843 RID: 2115
		string PostRequestJson(Uri url, string jsonData);

		// Token: 0x06000844 RID: 2116
		string PostRequestRaw(Uri url, Stream dataStream);

		// Token: 0x06000845 RID: 2117
		Stream GetRequestRaw(Uri url);
	}
}
