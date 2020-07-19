using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x0200007D RID: 125
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000F")]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	public class ComHelper
	{
		// Token: 0x0600028F RID: 655 RVA: 0x00015CC0 File Offset: 0x00013EC0
		public bool IsZipFile(string filename)
		{
			return ZipFile.IsZipFile(filename);
		}

		// Token: 0x06000290 RID: 656 RVA: 0x00015CC8 File Offset: 0x00013EC8
		public bool IsZipFileWithExtract(string filename)
		{
			return ZipFile.IsZipFile(filename, true);
		}

		// Token: 0x06000291 RID: 657 RVA: 0x00015CD4 File Offset: 0x00013ED4
		public bool CheckZip(string filename)
		{
			return ZipFile.CheckZip(filename);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x00015CDC File Offset: 0x00013EDC
		public bool CheckZipPassword(string filename, string password)
		{
			return ZipFile.CheckZipPassword(filename, password);
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00015CE8 File Offset: 0x00013EE8
		public void FixZipDirectory(string filename)
		{
			ZipFile.FixZipDirectory(filename);
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00015CF0 File Offset: 0x00013EF0
		public string GetZipLibraryVersion()
		{
			return ZipFile.LibraryVersion.ToString();
		}
	}
}
