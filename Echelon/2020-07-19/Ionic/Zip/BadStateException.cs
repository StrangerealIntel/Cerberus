using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Ionic.Zip
{
	// Token: 0x0200008F RID: 143
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00007")]
	[ComVisible(true)]
	[Serializable]
	public class BadStateException : ZipException
	{
		// Token: 0x060002EA RID: 746 RVA: 0x00016258 File Offset: 0x00014458
		public BadStateException()
		{
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00016260 File Offset: 0x00014460
		public BadStateException(string message) : base(message)
		{
		}

		// Token: 0x060002EC RID: 748 RVA: 0x0001626C File Offset: 0x0001446C
		public BadStateException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00016278 File Offset: 0x00014478
		protected BadStateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
