using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x02000086 RID: 134
	[ComVisible(true)]
	public class AddProgressEventArgs : ZipProgressEventArgs
	{
		// Token: 0x060002BD RID: 701 RVA: 0x00015E88 File Offset: 0x00014088
		internal AddProgressEventArgs()
		{
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00015E90 File Offset: 0x00014090
		private AddProgressEventArgs(string archiveName, ZipProgressEventType flavor) : base(archiveName, flavor)
		{
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00015E9C File Offset: 0x0001409C
		internal static AddProgressEventArgs AfterEntry(string archiveName, ZipEntry entry, int entriesTotal)
		{
			return new AddProgressEventArgs(archiveName, ZipProgressEventType.Adding_AfterAddEntry)
			{
				EntriesTotal = entriesTotal,
				CurrentEntry = entry
			};
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00015EC4 File Offset: 0x000140C4
		internal static AddProgressEventArgs Started(string archiveName)
		{
			return new AddProgressEventArgs(archiveName, ZipProgressEventType.Adding_Started);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00015EE0 File Offset: 0x000140E0
		internal static AddProgressEventArgs Completed(string archiveName)
		{
			return new AddProgressEventArgs(archiveName, ZipProgressEventType.Adding_Completed);
		}
	}
}
