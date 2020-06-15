using System;
using System.Runtime.InteropServices;

namespace cam.DShowNET.Device
{
	// Token: 0x02000065 RID: 101
	[ComVisible(false)]
	public class DsDevice : IDisposable
	{
		// Token: 0x06000282 RID: 642 RVA: 0x0001297C File Offset: 0x00010B7C
		public void Dispose()
		{
			if (this.Mon != null)
			{
				Marshal.ReleaseComObject(this.Mon);
			}
			this.Mon = null;
		}

		// Token: 0x040001EC RID: 492
		public string Name;

		// Token: 0x040001ED RID: 493
		public UCOMIMoniker Mon;
	}
}
