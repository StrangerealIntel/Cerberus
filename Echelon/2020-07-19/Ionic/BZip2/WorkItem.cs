using System;
using System.IO;

namespace Ionic.BZip2
{
	// Token: 0x020000BC RID: 188
	internal class WorkItem
	{
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x0002ADB8 File Offset: 0x00028FB8
		// (set) Token: 0x06000627 RID: 1575 RVA: 0x0002ADC0 File Offset: 0x00028FC0
		public BZip2Compressor Compressor { get; private set; }

		// Token: 0x06000628 RID: 1576 RVA: 0x0002ADCC File Offset: 0x00028FCC
		public WorkItem(int ix, int blockSize)
		{
			this.ms = new MemoryStream();
			this.bw = new BitWriter(this.ms);
			this.Compressor = new BZip2Compressor(this.bw, blockSize);
			this.index = ix;
		}

		// Token: 0x04000320 RID: 800
		public int index;

		// Token: 0x04000321 RID: 801
		public MemoryStream ms;

		// Token: 0x04000322 RID: 802
		public int ordinal;

		// Token: 0x04000323 RID: 803
		public BitWriter bw;
	}
}
