using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x02000132 RID: 306
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
	public sealed class JsonArrayAttribute : JsonContainerAttribute
	{
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x0003DDE4 File Offset: 0x0003BFE4
		// (set) Token: 0x060009C0 RID: 2496 RVA: 0x0003DDEC File Offset: 0x0003BFEC
		public bool AllowNullItems
		{
			get
			{
				return this._allowNullItems;
			}
			set
			{
				this._allowNullItems = value;
			}
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x0003DDF8 File Offset: 0x0003BFF8
		public JsonArrayAttribute()
		{
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0003DE00 File Offset: 0x0003C000
		public JsonArrayAttribute(bool allowNullItems)
		{
			this._allowNullItems = allowNullItems;
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0003DE10 File Offset: 0x0003C010
		[NullableContext(1)]
		public JsonArrayAttribute(string id) : base(id)
		{
		}

		// Token: 0x040005A1 RID: 1441
		private bool _allowNullItems;
	}
}
