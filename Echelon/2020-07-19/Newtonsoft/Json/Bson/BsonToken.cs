using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000221 RID: 545
	internal abstract class BsonToken
	{
		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x060015E2 RID: 5602
		public abstract BsonType Type { get; }

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x060015E3 RID: 5603 RVA: 0x00073368 File Offset: 0x00071568
		// (set) Token: 0x060015E4 RID: 5604 RVA: 0x00073370 File Offset: 0x00071570
		public BsonToken Parent { get; set; }

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x060015E5 RID: 5605 RVA: 0x0007337C File Offset: 0x0007157C
		// (set) Token: 0x060015E6 RID: 5606 RVA: 0x00073384 File Offset: 0x00071584
		public int CalculatedSize { get; set; }
	}
}
