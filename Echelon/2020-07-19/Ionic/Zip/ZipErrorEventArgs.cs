using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x02000089 RID: 137
	[ComVisible(true)]
	public class ZipErrorEventArgs : ZipProgressEventArgs
	{
		// Token: 0x060002D4 RID: 724 RVA: 0x0001613C File Offset: 0x0001433C
		private ZipErrorEventArgs()
		{
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00016144 File Offset: 0x00014344
		internal static ZipErrorEventArgs Saving(string archiveName, ZipEntry entry, Exception exception)
		{
			return new ZipErrorEventArgs
			{
				EventType = ZipProgressEventType.Error_Saving,
				ArchiveName = archiveName,
				CurrentEntry = entry,
				_exc = exception
			};
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0001617C File Offset: 0x0001437C
		public Exception Exception
		{
			get
			{
				return this._exc;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00016184 File Offset: 0x00014384
		public string FileName
		{
			get
			{
				return base.CurrentEntry.LocalFileName;
			}
		}

		// Token: 0x04000166 RID: 358
		private Exception _exc;
	}
}
