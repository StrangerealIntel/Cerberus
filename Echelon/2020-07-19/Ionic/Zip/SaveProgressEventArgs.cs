using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x02000087 RID: 135
	[ComVisible(true)]
	public class SaveProgressEventArgs : ZipProgressEventArgs
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x00015EFC File Offset: 0x000140FC
		internal SaveProgressEventArgs(string archiveName, bool before, int entriesTotal, int entriesSaved, ZipEntry entry) : base(archiveName, before ? ZipProgressEventType.Saving_BeforeWriteEntry : ZipProgressEventType.Saving_AfterWriteEntry)
		{
			base.EntriesTotal = entriesTotal;
			base.CurrentEntry = entry;
			this._entriesSaved = entriesSaved;
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00015F2C File Offset: 0x0001412C
		internal SaveProgressEventArgs()
		{
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00015F34 File Offset: 0x00014134
		internal SaveProgressEventArgs(string archiveName, ZipProgressEventType flavor) : base(archiveName, flavor)
		{
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00015F40 File Offset: 0x00014140
		internal static SaveProgressEventArgs ByteUpdate(string archiveName, ZipEntry entry, long bytesXferred, long totalBytes)
		{
			return new SaveProgressEventArgs(archiveName, ZipProgressEventType.Saving_EntryBytesRead)
			{
				ArchiveName = archiveName,
				CurrentEntry = entry,
				BytesTransferred = bytesXferred,
				TotalBytesToTransfer = totalBytes
			};
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00015F78 File Offset: 0x00014178
		internal static SaveProgressEventArgs Started(string archiveName)
		{
			return new SaveProgressEventArgs(archiveName, ZipProgressEventType.Saving_Started);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00015F94 File Offset: 0x00014194
		internal static SaveProgressEventArgs Completed(string archiveName)
		{
			return new SaveProgressEventArgs(archiveName, ZipProgressEventType.Saving_Completed);
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00015FB0 File Offset: 0x000141B0
		public int EntriesSaved
		{
			get
			{
				return this._entriesSaved;
			}
		}

		// Token: 0x04000163 RID: 355
		private int _entriesSaved;
	}
}
