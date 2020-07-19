using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x02000085 RID: 133
	[ComVisible(true)]
	public class ReadProgressEventArgs : ZipProgressEventArgs
	{
		// Token: 0x060002B6 RID: 694 RVA: 0x00015DC0 File Offset: 0x00013FC0
		internal ReadProgressEventArgs()
		{
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00015DC8 File Offset: 0x00013FC8
		private ReadProgressEventArgs(string archiveName, ZipProgressEventType flavor) : base(archiveName, flavor)
		{
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00015DD4 File Offset: 0x00013FD4
		internal static ReadProgressEventArgs Before(string archiveName, int entriesTotal)
		{
			return new ReadProgressEventArgs(archiveName, ZipProgressEventType.Reading_BeforeReadEntry)
			{
				EntriesTotal = entriesTotal
			};
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00015DF8 File Offset: 0x00013FF8
		internal static ReadProgressEventArgs After(string archiveName, ZipEntry entry, int entriesTotal)
		{
			return new ReadProgressEventArgs(archiveName, ZipProgressEventType.Reading_AfterReadEntry)
			{
				EntriesTotal = entriesTotal,
				CurrentEntry = entry
			};
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00015E20 File Offset: 0x00014020
		internal static ReadProgressEventArgs Started(string archiveName)
		{
			return new ReadProgressEventArgs(archiveName, ZipProgressEventType.Reading_Started);
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00015E3C File Offset: 0x0001403C
		internal static ReadProgressEventArgs ByteUpdate(string archiveName, ZipEntry entry, long bytesXferred, long totalBytes)
		{
			return new ReadProgressEventArgs(archiveName, ZipProgressEventType.Reading_ArchiveBytesRead)
			{
				CurrentEntry = entry,
				BytesTransferred = bytesXferred,
				TotalBytesToTransfer = totalBytes
			};
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00015E6C File Offset: 0x0001406C
		internal static ReadProgressEventArgs Completed(string archiveName)
		{
			return new ReadProgressEventArgs(archiveName, ZipProgressEventType.Reading_Completed);
		}
	}
}
