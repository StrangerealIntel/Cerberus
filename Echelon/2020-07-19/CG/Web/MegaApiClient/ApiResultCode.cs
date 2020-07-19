using System;

namespace CG.Web.MegaApiClient
{
	// Token: 0x020000E6 RID: 230
	public enum ApiResultCode
	{
		// Token: 0x040004CC RID: 1228
		Ok,
		// Token: 0x040004CD RID: 1229
		InternalError = -1,
		// Token: 0x040004CE RID: 1230
		BadArguments = -2,
		// Token: 0x040004CF RID: 1231
		RequestFailedRetry = -3,
		// Token: 0x040004D0 RID: 1232
		TooManyRequests = -4,
		// Token: 0x040004D1 RID: 1233
		RequestFailedPermanetly = -5,
		// Token: 0x040004D2 RID: 1234
		ToManyRequestsForThisResource = -6,
		// Token: 0x040004D3 RID: 1235
		ResourceAccessOutOfRange = -7,
		// Token: 0x040004D4 RID: 1236
		ResourceExpired = -8,
		// Token: 0x040004D5 RID: 1237
		ResourceNotExists = -9,
		// Token: 0x040004D6 RID: 1238
		CircularLinkage = -10,
		// Token: 0x040004D7 RID: 1239
		AccessDenied = -11,
		// Token: 0x040004D8 RID: 1240
		ResourceAlreadyExists = -12,
		// Token: 0x040004D9 RID: 1241
		RequestIncomplete = -13,
		// Token: 0x040004DA RID: 1242
		CryptographicError = -14,
		// Token: 0x040004DB RID: 1243
		BadSessionId = -15,
		// Token: 0x040004DC RID: 1244
		ResourceAdministrativelyBlocked = -16,
		// Token: 0x040004DD RID: 1245
		QuotaExceeded = -17,
		// Token: 0x040004DE RID: 1246
		ResourceTemporarilyNotAvailable = -18,
		// Token: 0x040004DF RID: 1247
		TooManyConnectionsOnThisResource = -19,
		// Token: 0x040004E0 RID: 1248
		FileCouldNotBeWrittenTo = -20,
		// Token: 0x040004E1 RID: 1249
		FileCouldNotBeReadFrom = -21,
		// Token: 0x040004E2 RID: 1250
		InvalidOrMissingApplicationKey = -22,
		// Token: 0x040004E3 RID: 1251
		TwoFactorAuthenticationError = -26
	}
}
