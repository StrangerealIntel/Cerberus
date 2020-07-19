using System;
using System.Net;

namespace CG.Web.MegaApiClient
{
	// Token: 0x020000EF RID: 239
	public interface ISession
	{
		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600083A RID: 2106
		string Client { get; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600083B RID: 2107
		IPAddress IpAddress { get; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x0600083C RID: 2108
		string Country { get; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x0600083D RID: 2109
		DateTime LoginTime { get; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x0600083E RID: 2110
		DateTime LastSeenTime { get; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600083F RID: 2111
		SessionStatus Status { get; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000840 RID: 2112
		string SessionId { get; }
	}
}
