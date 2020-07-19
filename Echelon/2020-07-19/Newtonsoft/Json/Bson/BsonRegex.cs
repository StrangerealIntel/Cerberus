using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000229 RID: 553
	internal class BsonRegex : BsonToken
	{
		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001601 RID: 5633 RVA: 0x00073530 File Offset: 0x00071730
		// (set) Token: 0x06001602 RID: 5634 RVA: 0x00073538 File Offset: 0x00071738
		public BsonString Pattern { get; set; }

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001603 RID: 5635 RVA: 0x00073544 File Offset: 0x00071744
		// (set) Token: 0x06001604 RID: 5636 RVA: 0x0007354C File Offset: 0x0007174C
		public BsonString Options { get; set; }

		// Token: 0x06001605 RID: 5637 RVA: 0x00073558 File Offset: 0x00071758
		public BsonRegex(string pattern, string options)
		{
			this.Pattern = new BsonString(pattern, false);
			this.Options = new BsonString(options, false);
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001606 RID: 5638 RVA: 0x0007357C File Offset: 0x0007177C
		public override BsonType Type
		{
			get
			{
				return BsonType.Regex;
			}
		}
	}
}
