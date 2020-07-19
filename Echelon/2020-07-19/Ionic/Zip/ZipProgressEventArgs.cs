using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x02000084 RID: 132
	[ComVisible(true)]
	public class ZipProgressEventArgs : EventArgs
	{
		// Token: 0x060002A6 RID: 678 RVA: 0x00015D04 File Offset: 0x00013F04
		internal ZipProgressEventArgs()
		{
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00015D0C File Offset: 0x00013F0C
		internal ZipProgressEventArgs(string archiveName, ZipProgressEventType flavor)
		{
			this._archiveName = archiveName;
			this._flavor = flavor;
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00015D24 File Offset: 0x00013F24
		// (set) Token: 0x060002A9 RID: 681 RVA: 0x00015D2C File Offset: 0x00013F2C
		public int EntriesTotal
		{
			get
			{
				return this._entriesTotal;
			}
			set
			{
				this._entriesTotal = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060002AA RID: 682 RVA: 0x00015D38 File Offset: 0x00013F38
		// (set) Token: 0x060002AB RID: 683 RVA: 0x00015D40 File Offset: 0x00013F40
		public ZipEntry CurrentEntry
		{
			get
			{
				return this._latestEntry;
			}
			set
			{
				this._latestEntry = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060002AC RID: 684 RVA: 0x00015D4C File Offset: 0x00013F4C
		// (set) Token: 0x060002AD RID: 685 RVA: 0x00015D54 File Offset: 0x00013F54
		public bool Cancel
		{
			get
			{
				return this._cancel;
			}
			set
			{
				this._cancel = (this._cancel || value);
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060002AE RID: 686 RVA: 0x00015D70 File Offset: 0x00013F70
		// (set) Token: 0x060002AF RID: 687 RVA: 0x00015D78 File Offset: 0x00013F78
		public ZipProgressEventType EventType
		{
			get
			{
				return this._flavor;
			}
			set
			{
				this._flavor = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060002B0 RID: 688 RVA: 0x00015D84 File Offset: 0x00013F84
		// (set) Token: 0x060002B1 RID: 689 RVA: 0x00015D8C File Offset: 0x00013F8C
		public string ArchiveName
		{
			get
			{
				return this._archiveName;
			}
			set
			{
				this._archiveName = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x00015D98 File Offset: 0x00013F98
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x00015DA0 File Offset: 0x00013FA0
		public long BytesTransferred
		{
			get
			{
				return this._bytesTransferred;
			}
			set
			{
				this._bytesTransferred = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x00015DAC File Offset: 0x00013FAC
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x00015DB4 File Offset: 0x00013FB4
		public long TotalBytesToTransfer
		{
			get
			{
				return this._totalBytesToTransfer;
			}
			set
			{
				this._totalBytesToTransfer = value;
			}
		}

		// Token: 0x0400015C RID: 348
		private int _entriesTotal;

		// Token: 0x0400015D RID: 349
		private bool _cancel;

		// Token: 0x0400015E RID: 350
		private ZipEntry _latestEntry;

		// Token: 0x0400015F RID: 351
		private ZipProgressEventType _flavor;

		// Token: 0x04000160 RID: 352
		private string _archiveName;

		// Token: 0x04000161 RID: 353
		private long _bytesTransferred;

		// Token: 0x04000162 RID: 354
		private long _totalBytesToTransfer;
	}
}
