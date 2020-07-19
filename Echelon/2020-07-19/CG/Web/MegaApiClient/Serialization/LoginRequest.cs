using System;
using Newtonsoft.Json;

namespace CG.Web.MegaApiClient.Serialization
{
	// Token: 0x0200010B RID: 267
	internal class LoginRequest : RequestBase
	{
		// Token: 0x0600094F RID: 2383 RVA: 0x0003D150 File Offset: 0x0003B350
		public LoginRequest(string userHandle, string passwordHash) : base("us")
		{
			this.UserHandle = userHandle;
			this.PasswordHash = passwordHash;
		}

		// Token: 0x06000950 RID: 2384 RVA: 0x0003D16C File Offset: 0x0003B36C
		public LoginRequest(string userHandle, string passwordHash, string mfaKey) : base("us")
		{
			this.UserHandle = userHandle;
			this.PasswordHash = passwordHash;
			this.MFAKey = mfaKey;
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000951 RID: 2385 RVA: 0x0003D1A0 File Offset: 0x0003B3A0
		// (set) Token: 0x06000952 RID: 2386 RVA: 0x0003D1A8 File Offset: 0x0003B3A8
		[JsonProperty("user")]
		public string UserHandle { get; private set; }

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000953 RID: 2387 RVA: 0x0003D1B4 File Offset: 0x0003B3B4
		// (set) Token: 0x06000954 RID: 2388 RVA: 0x0003D1BC File Offset: 0x0003B3BC
		[JsonProperty("uh")]
		public string PasswordHash { get; private set; }

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x0003D1C8 File Offset: 0x0003B3C8
		// (set) Token: 0x06000956 RID: 2390 RVA: 0x0003D1D0 File Offset: 0x0003B3D0
		[JsonProperty("mfa")]
		public string MFAKey { get; private set; }
	}
}
