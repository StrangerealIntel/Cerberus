using System;
using System.Runtime.InteropServices;
using Ionic.Zlib;

namespace Ionic.Zip
{
	// Token: 0x02000082 RID: 130
	// (Invoke) Token: 0x060002A3 RID: 675
	[ComVisible(true)]
	public delegate CompressionLevel SetCompressionCallback(string localFileName, string fileNameInArchive);
}
