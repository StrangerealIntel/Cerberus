using System;
using System.Diagnostics;
using Ionic.Zip;

namespace Ionic
{
	// Token: 0x02000094 RID: 148
	internal abstract class SelectionCriterion
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060002EE RID: 750 RVA: 0x00016284 File Offset: 0x00014484
		// (set) Token: 0x060002EF RID: 751 RVA: 0x0001628C File Offset: 0x0001448C
		internal virtual bool Verbose { get; set; }

		// Token: 0x060002F0 RID: 752
		internal abstract bool Evaluate(string filename);

		// Token: 0x060002F1 RID: 753 RVA: 0x00016298 File Offset: 0x00014498
		[Conditional("SelectorTrace")]
		protected static void CriterionTrace(string format, params object[] args)
		{
		}

		// Token: 0x060002F2 RID: 754
		internal abstract bool Evaluate(ZipEntry entry);
	}
}
