using System;
using System.ComponentModel;

namespace Ionic
{
	// Token: 0x02000093 RID: 147
	internal enum ComparisonOperator
	{
		// Token: 0x04000176 RID: 374
		[Description(">")]
		GreaterThan,
		// Token: 0x04000177 RID: 375
		[Description(">=")]
		GreaterThanOrEqualTo,
		// Token: 0x04000178 RID: 376
		[Description("<")]
		LesserThan,
		// Token: 0x04000179 RID: 377
		[Description("<=")]
		LesserThanOrEqualTo,
		// Token: 0x0400017A RID: 378
		[Description("=")]
		EqualTo,
		// Token: 0x0400017B RID: 379
		[Description("!=")]
		NotEqualTo
	}
}
