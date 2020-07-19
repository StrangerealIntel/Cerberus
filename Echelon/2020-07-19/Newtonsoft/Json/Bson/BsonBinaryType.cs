using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200021D RID: 541
	internal enum BsonBinaryType : byte
	{
		// Token: 0x04000972 RID: 2418
		Binary,
		// Token: 0x04000973 RID: 2419
		Function,
		// Token: 0x04000974 RID: 2420
		[Obsolete("This type has been deprecated in the BSON specification. Use Binary instead.")]
		BinaryOld,
		// Token: 0x04000975 RID: 2421
		[Obsolete("This type has been deprecated in the BSON specification. Use Uuid instead.")]
		UuidOld,
		// Token: 0x04000976 RID: 2422
		Uuid,
		// Token: 0x04000977 RID: 2423
		Md5,
		// Token: 0x04000978 RID: 2424
		UserDefined = 128
	}
}
