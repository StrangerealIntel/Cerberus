using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Ionic.Zip
{
	// Token: 0x0200008D RID: 141
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00009")]
	[ComVisible(true)]
	[Serializable]
	public class BadCrcException : ZipException
	{
		// Token: 0x060002E4 RID: 740 RVA: 0x00016218 File Offset: 0x00014418
		public BadCrcException()
		{
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00016220 File Offset: 0x00014420
		public BadCrcException(string message) : base(message)
		{
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0001622C File Offset: 0x0001442C
		protected BadCrcException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
