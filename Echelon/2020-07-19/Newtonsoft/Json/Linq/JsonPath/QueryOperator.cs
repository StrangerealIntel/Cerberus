using System;

namespace Newtonsoft.Json.Linq.JsonPath
{
	// Token: 0x020001EF RID: 495
	internal enum QueryOperator
	{
		// Token: 0x0400092B RID: 2347
		None,
		// Token: 0x0400092C RID: 2348
		Equals,
		// Token: 0x0400092D RID: 2349
		NotEquals,
		// Token: 0x0400092E RID: 2350
		Exists,
		// Token: 0x0400092F RID: 2351
		LessThan,
		// Token: 0x04000930 RID: 2352
		LessThanOrEquals,
		// Token: 0x04000931 RID: 2353
		GreaterThan,
		// Token: 0x04000932 RID: 2354
		GreaterThanOrEquals,
		// Token: 0x04000933 RID: 2355
		And,
		// Token: 0x04000934 RID: 2356
		Or,
		// Token: 0x04000935 RID: 2357
		RegexEquals,
		// Token: 0x04000936 RID: 2358
		StrictEquals,
		// Token: 0x04000937 RID: 2359
		StrictNotEquals
	}
}
