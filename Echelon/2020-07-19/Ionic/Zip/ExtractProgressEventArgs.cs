using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x02000088 RID: 136
	[ComVisible(true)]
	public class ExtractProgressEventArgs : ZipProgressEventArgs
	{
		// Token: 0x060002C9 RID: 713 RVA: 0x00015FB8 File Offset: 0x000141B8
		internal ExtractProgressEventArgs(string archiveName, bool before, int entriesTotal, int entriesExtracted, ZipEntry entry, string extractLocation) : base(archiveName, before ? ZipProgressEventType.Extracting_BeforeExtractEntry : ZipProgressEventType.Extracting_AfterExtractEntry)
		{
			base.EntriesTotal = entriesTotal;
			base.CurrentEntry = entry;
			this._entriesExtracted = entriesExtracted;
			this._target = extractLocation;
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00015FF0 File Offset: 0x000141F0
		internal ExtractProgressEventArgs(string archiveName, ZipProgressEventType flavor) : base(archiveName, flavor)
		{
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00015FFC File Offset: 0x000141FC
		internal ExtractProgressEventArgs()
		{
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00016004 File Offset: 0x00014204
		internal static ExtractProgressEventArgs BeforeExtractEntry(string archiveName, ZipEntry entry, string extractLocation)
		{
			return new ExtractProgressEventArgs
			{
				ArchiveName = archiveName,
				EventType = ZipProgressEventType.Extracting_BeforeExtractEntry,
				CurrentEntry = entry,
				_target = extractLocation
			};
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0001603C File Offset: 0x0001423C
		internal static ExtractProgressEventArgs ExtractExisting(string archiveName, ZipEntry entry, string extractLocation)
		{
			return new ExtractProgressEventArgs
			{
				ArchiveName = archiveName,
				EventType = ZipProgressEventType.Extracting_ExtractEntryWouldOverwrite,
				CurrentEntry = entry,
				_target = extractLocation
			};
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00016074 File Offset: 0x00014274
		internal static ExtractProgressEventArgs AfterExtractEntry(string archiveName, ZipEntry entry, string extractLocation)
		{
			return new ExtractProgressEventArgs
			{
				ArchiveName = archiveName,
				EventType = ZipProgressEventType.Extracting_AfterExtractEntry,
				CurrentEntry = entry,
				_target = extractLocation
			};
		}

		// Token: 0x060002CF RID: 719 RVA: 0x000160AC File Offset: 0x000142AC
		internal static ExtractProgressEventArgs ExtractAllStarted(string archiveName, string extractLocation)
		{
			return new ExtractProgressEventArgs(archiveName, ZipProgressEventType.Extracting_BeforeExtractAll)
			{
				_target = extractLocation
			};
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x000160D0 File Offset: 0x000142D0
		internal static ExtractProgressEventArgs ExtractAllCompleted(string archiveName, string extractLocation)
		{
			return new ExtractProgressEventArgs(archiveName, ZipProgressEventType.Extracting_AfterExtractAll)
			{
				_target = extractLocation
			};
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x000160F4 File Offset: 0x000142F4
		internal static ExtractProgressEventArgs ByteUpdate(string archiveName, ZipEntry entry, long bytesWritten, long totalBytes)
		{
			return new ExtractProgressEventArgs(archiveName, ZipProgressEventType.Extracting_EntryBytesWritten)
			{
				ArchiveName = archiveName,
				CurrentEntry = entry,
				BytesTransferred = bytesWritten,
				TotalBytesToTransfer = totalBytes
			};
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0001612C File Offset: 0x0001432C
		public int EntriesExtracted
		{
			get
			{
				return this._entriesExtracted;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060002D3 RID: 723 RVA: 0x00016134 File Offset: 0x00014334
		public string ExtractLocation
		{
			get
			{
				return this._target;
			}
		}

		// Token: 0x04000164 RID: 356
		private int _entriesExtracted;

		// Token: 0x04000165 RID: 357
		private string _target;
	}
}
