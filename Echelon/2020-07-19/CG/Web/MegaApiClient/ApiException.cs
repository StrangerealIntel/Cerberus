using System;

namespace CG.Web.MegaApiClient
{
	// Token: 0x020000E3 RID: 227
	public class ApiException : Exception
	{
		// Token: 0x060007F6 RID: 2038 RVA: 0x00039A14 File Offset: 0x00037C14
		internal ApiException(ApiResultCode apiResultCode)
		{
			this.ApiResultCode = apiResultCode;
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x00039A24 File Offset: 0x00037C24
		// (set) Token: 0x060007F8 RID: 2040 RVA: 0x00039A2C File Offset: 0x00037C2C
		public ApiResultCode ApiResultCode { get; private set; }

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x00039A38 File Offset: 0x00037C38
		public override string Message
		{
			get
			{
				return string.Format("API response: {0}", this.ApiResultCode);
			}
		}
	}
}
