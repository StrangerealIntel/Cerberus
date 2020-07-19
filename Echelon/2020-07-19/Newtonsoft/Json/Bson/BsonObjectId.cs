using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200021F RID: 543
	[Obsolete("BSON reading and writing has been moved to its own package. See https://www.nuget.org/packages/Newtonsoft.Json.Bson for more details.")]
	public class BsonObjectId
	{
		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060015BE RID: 5566 RVA: 0x000725E4 File Offset: 0x000707E4
		public byte[] Value { get; }

		// Token: 0x060015BF RID: 5567 RVA: 0x000725EC File Offset: 0x000707EC
		public BsonObjectId(byte[] value)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			if (value.Length != 12)
			{
				throw new ArgumentException("An ObjectId must be 12 bytes", "value");
			}
			this.Value = value;
		}
	}
}
