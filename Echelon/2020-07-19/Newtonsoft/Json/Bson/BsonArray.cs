using System;
using System.Collections;
using System.Collections.Generic;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000223 RID: 547
	internal class BsonArray : BsonToken, IEnumerable<BsonToken>, IEnumerable
	{
		// Token: 0x060015ED RID: 5613 RVA: 0x00073408 File Offset: 0x00071608
		public void Add(BsonToken token)
		{
			this._children.Add(token);
			token.Parent = this;
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060015EE RID: 5614 RVA: 0x00073420 File Offset: 0x00071620
		public override BsonType Type
		{
			get
			{
				return BsonType.Array;
			}
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x00073424 File Offset: 0x00071624
		public IEnumerator<BsonToken> GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x00073438 File Offset: 0x00071638
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000990 RID: 2448
		private readonly List<BsonToken> _children = new List<BsonToken>();
	}
}
