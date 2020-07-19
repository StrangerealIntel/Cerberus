using System;

namespace CG.Web.MegaApiClient
{
	// Token: 0x020000E4 RID: 228
	public class DownloadException : Exception
	{
		// Token: 0x060007FA RID: 2042 RVA: 0x00039A50 File Offset: 0x00037C50
		public DownloadException() : base("Invalid file checksum")
		{
		}
	}
}
