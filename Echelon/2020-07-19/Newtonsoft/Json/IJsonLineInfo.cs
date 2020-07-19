using System;

namespace Newtonsoft.Json
{
	// Token: 0x02000131 RID: 305
	public interface IJsonLineInfo
	{
		// Token: 0x060009BC RID: 2492
		bool HasLineInfo();

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060009BD RID: 2493
		int LineNumber { get; }

		// Token: 0x1700022D RID: 557
		// (get) Token: 0x060009BE RID: 2494
		int LinePosition { get; }
	}
}
