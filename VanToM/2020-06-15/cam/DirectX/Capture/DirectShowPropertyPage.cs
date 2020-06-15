using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using cam.DShowNET;

namespace cam.DirectX.Capture
{
	// Token: 0x02000045 RID: 69
	public class DirectShowPropertyPage : PropertyPage
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x000125D4 File Offset: 0x000107D4
		public DirectShowPropertyPage(string name__1, ISpecifyPropertyPages specifyPropertyPages)
		{
			this.Name = name__1;
			this.SupportsPersisting = false;
			this.specifyPropertyPages = specifyPropertyPages;
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000125F4 File Offset: 0x000107F4
		public override void Show(Control owner)
		{
			DsCAUUID dsCAUUID = default(DsCAUUID);
			try
			{
				int num = this.specifyPropertyPages.GetPages(ref dsCAUUID);
				object obj = this.specifyPropertyPages;
				num = DirectShowPropertyPage.OleCreatePropertyFrame(owner.Handle, 30, 30, null, 1, ref obj, dsCAUUID.cElems, dsCAUUID.pElems, 0, 0, IntPtr.Zero);
			}
			finally
			{
				if (dsCAUUID.pElems != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(dsCAUUID.pElems);
				}
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x00012680 File Offset: 0x00010880
		public void Dispose()
		{
			if (this.specifyPropertyPages != null)
			{
				Marshal.ReleaseComObject(this.specifyPropertyPages);
			}
			this.specifyPropertyPages = null;
		}

		// Token: 0x060001AB RID: 427
		[DllImport("olepro32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		private static extern int OleCreatePropertyFrame(IntPtr hwndOwner, int x, int y, string lpszCaption, int cObjects, [MarshalAs(UnmanagedType.Interface)] [In] ref object ppUnk, int cPages, IntPtr pPageClsID, int lcid, int dwReserved, IntPtr pvReserved);

		// Token: 0x04000172 RID: 370
		protected ISpecifyPropertyPages specifyPropertyPages;
	}
}
