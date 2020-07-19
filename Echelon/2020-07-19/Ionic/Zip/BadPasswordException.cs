using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Ionic.Zip
{
	// Token: 0x0200008B RID: 139
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000B")]
	[ComVisible(true)]
	[Serializable]
	public class BadPasswordException : ZipException
	{
		// Token: 0x060002DC RID: 732 RVA: 0x000161C0 File Offset: 0x000143C0
		public BadPasswordException()
		{
		}

		// Token: 0x060002DD RID: 733 RVA: 0x000161C8 File Offset: 0x000143C8
		public BadPasswordException(string message) : base(message)
		{
		}

		// Token: 0x060002DE RID: 734 RVA: 0x000161D4 File Offset: 0x000143D4
		public BadPasswordException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002DF RID: 735 RVA: 0x000161E0 File Offset: 0x000143E0
		protected BadPasswordException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
