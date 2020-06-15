using System;
using System.Windows.Forms;

namespace cam.DirectX.Capture
{
	// Token: 0x02000099 RID: 153
	public class PropertyPage : IDisposable
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000344 RID: 836 RVA: 0x00013B68 File Offset: 0x00011D68
		// (set) Token: 0x06000345 RID: 837 RVA: 0x00013B78 File Offset: 0x00011D78
		public virtual byte[] State
		{
			get
			{
				byte[] result;
				return result;
			}
			set
			{
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00013B7C File Offset: 0x00011D7C
		public PropertyPage()
		{
			this.SupportsPersisting = false;
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00013B8C File Offset: 0x00011D8C
		public virtual void Show(Control owner)
		{
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00013B90 File Offset: 0x00011D90
		public void Dispose()
		{
		}

		// Token: 0x04000292 RID: 658
		public string Name;

		// Token: 0x04000293 RID: 659
		public bool SupportsPersisting;
	}
}
