using System;

namespace ChromV1
{
	// Token: 0x02000061 RID: 97
	public interface IGcmMultiplier
	{
		// Token: 0x06000229 RID: 553
		void Init(byte[] H);

		// Token: 0x0600022A RID: 554
		void MultiplyH(byte[] x);
	}
}
