using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000224 RID: 548
	internal class BsonEmpty : BsonToken
	{
		// Token: 0x060015F2 RID: 5618 RVA: 0x00073454 File Offset: 0x00071654
		private BsonEmpty(BsonType type)
		{
			this.Type = type;
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060015F3 RID: 5619 RVA: 0x00073464 File Offset: 0x00071664
		public override BsonType Type { get; }

		// Token: 0x04000991 RID: 2449
		public static readonly BsonToken Null = new BsonEmpty(BsonType.Null);

		// Token: 0x04000992 RID: 2450
		public static readonly BsonToken Undefined = new BsonEmpty(BsonType.Undefined);
	}
}
