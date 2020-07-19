using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace Ionic.Zip
{
	// Token: 0x0200008E RID: 142
	[Guid("ebc25cf6-9120-4283-b972-0e5520d00008")]
	[ComVisible(true)]
	[Serializable]
	public class SfxGenerationException : ZipException
	{
		// Token: 0x060002E7 RID: 743 RVA: 0x00016238 File Offset: 0x00014438
		public SfxGenerationException()
		{
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00016240 File Offset: 0x00014440
		public SfxGenerationException(string message) : base(message)
		{
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0001624C File Offset: 0x0001444C
		protected SfxGenerationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
