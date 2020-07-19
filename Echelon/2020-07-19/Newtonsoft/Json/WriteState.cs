using System;

namespace Newtonsoft.Json
{
	// Token: 0x0200015B RID: 347
	public enum WriteState
	{
		// Token: 0x040006BB RID: 1723
		Error,
		// Token: 0x040006BC RID: 1724
		Closed,
		// Token: 0x040006BD RID: 1725
		Object,
		// Token: 0x040006BE RID: 1726
		Array,
		// Token: 0x040006BF RID: 1727
		Constructor,
		// Token: 0x040006C0 RID: 1728
		Property,
		// Token: 0x040006C1 RID: 1729
		Start
	}
}
