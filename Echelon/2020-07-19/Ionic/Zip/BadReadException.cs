using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Ionic.Zip
{
	// Token: 0x0200008C RID: 140
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000A")]
	[ComVisible(true)]
	[Serializable]
	public class BadReadException : ZipException
	{
		// Token: 0x060002E0 RID: 736 RVA: 0x000161EC File Offset: 0x000143EC
		public BadReadException()
		{
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000161F4 File Offset: 0x000143F4
		public BadReadException(string message) : base(message)
		{
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00016200 File Offset: 0x00014400
		public BadReadException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0001620C File Offset: 0x0001440C
		protected BadReadException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
