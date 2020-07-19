using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Ionic.Zip
{
	// Token: 0x0200008A RID: 138
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00006")]
	[ComVisible(true)]
	[Serializable]
	public class ZipException : Exception
	{
		// Token: 0x060002D8 RID: 728 RVA: 0x00016194 File Offset: 0x00014394
		public ZipException()
		{
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0001619C File Offset: 0x0001439C
		public ZipException(string message) : base(message)
		{
		}

		// Token: 0x060002DA RID: 730 RVA: 0x000161A8 File Offset: 0x000143A8
		public ZipException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000161B4 File Offset: 0x000143B4
		protected ZipException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
