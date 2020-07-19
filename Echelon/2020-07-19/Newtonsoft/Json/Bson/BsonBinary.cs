using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000228 RID: 552
	internal class BsonBinary : BsonValue
	{
		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060015FE RID: 5630 RVA: 0x00073508 File Offset: 0x00071708
		// (set) Token: 0x060015FF RID: 5631 RVA: 0x00073510 File Offset: 0x00071710
		public BsonBinaryType BinaryType { get; set; }

		// Token: 0x06001600 RID: 5632 RVA: 0x0007351C File Offset: 0x0007171C
		public BsonBinary(byte[] value, BsonBinaryType binaryType) : base(value, BsonType.Binary)
		{
			this.BinaryType = binaryType;
		}
	}
}
