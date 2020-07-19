using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000227 RID: 551
	internal class BsonString : BsonValue
	{
		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060015FA RID: 5626 RVA: 0x000734D8 File Offset: 0x000716D8
		// (set) Token: 0x060015FB RID: 5627 RVA: 0x000734E0 File Offset: 0x000716E0
		public int ByteCount { get; set; }

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060015FC RID: 5628 RVA: 0x000734EC File Offset: 0x000716EC
		public bool IncludeLength { get; }

		// Token: 0x060015FD RID: 5629 RVA: 0x000734F4 File Offset: 0x000716F4
		public BsonString(object value, bool includeLength) : base(value, BsonType.String)
		{
			this.IncludeLength = includeLength;
		}
	}
}
