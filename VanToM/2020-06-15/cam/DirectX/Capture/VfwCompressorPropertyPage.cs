using System;
using System.Windows.Forms;
using cam.DShowNET;

namespace cam.DirectX.Capture
{
	// Token: 0x020000A4 RID: 164
	public class VfwCompressorPropertyPage : PropertyPage
	{
		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000376 RID: 886 RVA: 0x00014670 File Offset: 0x00012870
		// (set) Token: 0x06000377 RID: 887 RVA: 0x000146C4 File Offset: 0x000128C4
		public override byte[] State
		{
			get
			{
				byte[] array = null;
				int num = 0;
				int state = this.vfwCompressDialogs.GetState(null, ref num);
				if (state == 0 && num > 0)
				{
					array = new byte[checked(num - 1 + 1)];
					state = this.vfwCompressDialogs.GetState(array, ref num);
					if (state != 0)
					{
						array = null;
					}
				}
				return array;
			}
			set
			{
				int num = this.vfwCompressDialogs.SetState(value, value.Length);
			}
		}

		// Token: 0x06000378 RID: 888 RVA: 0x000146E4 File Offset: 0x000128E4
		public VfwCompressorPropertyPage(string name__1, IAMVfwCompressDialogs compressDialogs)
		{
			this.vfwCompressDialogs = null;
			this.Name = name__1;
			this.SupportsPersisting = true;
			this.vfwCompressDialogs = compressDialogs;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00014708 File Offset: 0x00012908
		public override void Show(Control owner)
		{
			this.vfwCompressDialogs.ShowDialog(VfwCompressDialogs.QueryConfig, owner.Handle);
		}

		// Token: 0x040002B2 RID: 690
		protected IAMVfwCompressDialogs vfwCompressDialogs;
	}
}
