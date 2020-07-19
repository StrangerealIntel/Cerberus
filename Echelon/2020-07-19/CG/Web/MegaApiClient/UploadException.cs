using System;

namespace CG.Web.MegaApiClient
{
	// Token: 0x020000E5 RID: 229
	public class UploadException : Exception
	{
		// Token: 0x060007FB RID: 2043 RVA: 0x00039A60 File Offset: 0x00037C60
		public UploadException(string error) : base("Upload error: " + error)
		{
		}
	}
}
