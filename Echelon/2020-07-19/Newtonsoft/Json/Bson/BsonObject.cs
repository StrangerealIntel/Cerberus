using System;
using System.Collections;
using System.Collections.Generic;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000222 RID: 546
	internal class BsonObject : BsonToken, IEnumerable<BsonProperty>, IEnumerable
	{
		// Token: 0x060015E8 RID: 5608 RVA: 0x00073398 File Offset: 0x00071598
		public void Add(string name, BsonToken token)
		{
			this._children.Add(new BsonProperty
			{
				Name = new BsonString(name, false),
				Value = token
			});
			token.Parent = this;
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x060015E9 RID: 5609 RVA: 0x000733D4 File Offset: 0x000715D4
		public override BsonType Type
		{
			get
			{
				return BsonType.Object;
			}
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x000733D8 File Offset: 0x000715D8
		public IEnumerator<BsonProperty> GetEnumerator()
		{
			return this._children.GetEnumerator();
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x000733EC File Offset: 0x000715EC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400098F RID: 2447
		private readonly List<BsonProperty> _children = new List<BsonProperty>();
	}
}
