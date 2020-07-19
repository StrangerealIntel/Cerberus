using System;

namespace CG.Web.MegaApiClient
{
	// Token: 0x020000E1 RID: 225
	public class ApiRequestFailedEventArgs : EventArgs
	{
		// Token: 0x060007DF RID: 2015 RVA: 0x000395C8 File Offset: 0x000377C8
		public ApiRequestFailedEventArgs(Uri url, int attemptNum, TimeSpan retryDelay, ApiResultCode apiResult, string responseJson) : this(url, attemptNum, retryDelay, apiResult, responseJson, null)
		{
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x000395D8 File Offset: 0x000377D8
		public ApiRequestFailedEventArgs(Uri url, int attemptNum, TimeSpan retryDelay, ApiResultCode apiResult, Exception exception) : this(url, attemptNum, retryDelay, apiResult, null, exception)
		{
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x000395E8 File Offset: 0x000377E8
		private ApiRequestFailedEventArgs(Uri url, int attemptNum, TimeSpan retryDelay, ApiResultCode apiResult, string responseJson, Exception exception)
		{
			this.ApiUrl = url;
			this.AttemptNum = attemptNum;
			this.RetryDelay = retryDelay;
			this.ApiResult = apiResult;
			this.ResponseJson = responseJson;
			this.Exception = exception;
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x00039620 File Offset: 0x00037820
		public Uri ApiUrl { get; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x00039628 File Offset: 0x00037828
		public ApiResultCode ApiResult { get; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x00039630 File Offset: 0x00037830
		public string ResponseJson { get; }

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x00039638 File Offset: 0x00037838
		public int AttemptNum { get; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x00039640 File Offset: 0x00037840
		public TimeSpan RetryDelay { get; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060007E7 RID: 2023 RVA: 0x00039648 File Offset: 0x00037848
		public Exception Exception { get; }
	}
}
