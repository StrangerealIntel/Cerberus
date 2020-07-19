using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000226 RID: 550
	internal class BsonBoolean : BsonValue
	{
		// Token: 0x060015F8 RID: 5624 RVA: 0x000734B0 File Offset: 0x000716B0
		private BsonBoolean(bool value) : base(value, BsonType.Boolean)
		{
		}

		// Token: 0x04000996 RID: 2454
		public static readonly BsonBoolean False = new BsonBoolean(false);

		// Token: 0x04000997 RID: 2455
		public static readonly BsonBoolean True = new BsonBoolean(true);
	}
}
