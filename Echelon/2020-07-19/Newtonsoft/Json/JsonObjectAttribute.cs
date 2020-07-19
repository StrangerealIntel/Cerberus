using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x0200013F RID: 319
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = false)]
	public sealed class JsonObjectAttribute : JsonContainerAttribute
	{
		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000A47 RID: 2631 RVA: 0x0003EC2C File Offset: 0x0003CE2C
		// (set) Token: 0x06000A48 RID: 2632 RVA: 0x0003EC34 File Offset: 0x0003CE34
		public MemberSerialization MemberSerialization
		{
			get
			{
				return this._memberSerialization;
			}
			set
			{
				this._memberSerialization = value;
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x06000A49 RID: 2633 RVA: 0x0003EC40 File Offset: 0x0003CE40
		// (set) Token: 0x06000A4A RID: 2634 RVA: 0x0003EC50 File Offset: 0x0003CE50
		public MissingMemberHandling MissingMemberHandling
		{
			get
			{
				return this._missingMemberHandling.GetValueOrDefault();
			}
			set
			{
				this._missingMemberHandling = new MissingMemberHandling?(value);
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x06000A4B RID: 2635 RVA: 0x0003EC60 File Offset: 0x0003CE60
		// (set) Token: 0x06000A4C RID: 2636 RVA: 0x0003EC70 File Offset: 0x0003CE70
		public NullValueHandling ItemNullValueHandling
		{
			get
			{
				return this._itemNullValueHandling.GetValueOrDefault();
			}
			set
			{
				this._itemNullValueHandling = new NullValueHandling?(value);
			}
		}

		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000A4D RID: 2637 RVA: 0x0003EC80 File Offset: 0x0003CE80
		// (set) Token: 0x06000A4E RID: 2638 RVA: 0x0003EC90 File Offset: 0x0003CE90
		public Required ItemRequired
		{
			get
			{
				return this._itemRequired.GetValueOrDefault();
			}
			set
			{
				this._itemRequired = new Required?(value);
			}
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x0003ECA0 File Offset: 0x0003CEA0
		public JsonObjectAttribute()
		{
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x0003ECA8 File Offset: 0x0003CEA8
		public JsonObjectAttribute(MemberSerialization memberSerialization)
		{
			this.MemberSerialization = memberSerialization;
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0003ECB8 File Offset: 0x0003CEB8
		[NullableContext(1)]
		public JsonObjectAttribute(string id) : base(id)
		{
		}

		// Token: 0x040005BA RID: 1466
		private MemberSerialization _memberSerialization;

		// Token: 0x040005BB RID: 1467
		internal MissingMemberHandling? _missingMemberHandling;

		// Token: 0x040005BC RID: 1468
		internal Required? _itemRequired;

		// Token: 0x040005BD RID: 1469
		internal NullValueHandling? _itemNullValueHandling;
	}
}
