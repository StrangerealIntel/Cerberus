using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200022B RID: 555
	internal enum BsonType : sbyte
	{
		// Token: 0x040009A0 RID: 2464
		Number = 1,
		// Token: 0x040009A1 RID: 2465
		String,
		// Token: 0x040009A2 RID: 2466
		Object,
		// Token: 0x040009A3 RID: 2467
		Array,
		// Token: 0x040009A4 RID: 2468
		Binary,
		// Token: 0x040009A5 RID: 2469
		Undefined,
		// Token: 0x040009A6 RID: 2470
		Oid,
		// Token: 0x040009A7 RID: 2471
		Boolean,
		// Token: 0x040009A8 RID: 2472
		Date,
		// Token: 0x040009A9 RID: 2473
		Null,
		// Token: 0x040009AA RID: 2474
		Regex,
		// Token: 0x040009AB RID: 2475
		Reference,
		// Token: 0x040009AC RID: 2476
		Code,
		// Token: 0x040009AD RID: 2477
		Symbol,
		// Token: 0x040009AE RID: 2478
		CodeWScope,
		// Token: 0x040009AF RID: 2479
		Integer,
		// Token: 0x040009B0 RID: 2480
		TimeStamp,
		// Token: 0x040009B1 RID: 2481
		Long,
		// Token: 0x040009B2 RID: 2482
		MinKey = -1,
		// Token: 0x040009B3 RID: 2483
		MaxKey = 127
	}
}
