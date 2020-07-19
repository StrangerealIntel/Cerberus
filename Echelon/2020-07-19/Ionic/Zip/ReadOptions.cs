using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Ionic.Zip
{
	// Token: 0x020000AF RID: 175
	[ComVisible(true)]
	public class ReadOptions
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x00024EF4 File Offset: 0x000230F4
		// (set) Token: 0x06000515 RID: 1301 RVA: 0x00024EFC File Offset: 0x000230FC
		public EventHandler<ReadProgressEventArgs> ReadProgress { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x00024F08 File Offset: 0x00023108
		// (set) Token: 0x06000517 RID: 1303 RVA: 0x00024F10 File Offset: 0x00023110
		public TextWriter StatusMessageWriter { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x00024F1C File Offset: 0x0002311C
		// (set) Token: 0x06000519 RID: 1305 RVA: 0x00024F24 File Offset: 0x00023124
		public Encoding Encoding { get; set; }
	}
}
