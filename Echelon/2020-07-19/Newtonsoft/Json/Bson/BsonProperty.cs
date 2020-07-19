using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200022A RID: 554
	internal class BsonProperty
	{
		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001607 RID: 5639 RVA: 0x00073580 File Offset: 0x00071780
		// (set) Token: 0x06001608 RID: 5640 RVA: 0x00073588 File Offset: 0x00071788
		public BsonString Name { get; set; }

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06001609 RID: 5641 RVA: 0x00073594 File Offset: 0x00071794
		// (set) Token: 0x0600160A RID: 5642 RVA: 0x0007359C File Offset: 0x0007179C
		public BsonToken Value { get; set; }
	}
}
