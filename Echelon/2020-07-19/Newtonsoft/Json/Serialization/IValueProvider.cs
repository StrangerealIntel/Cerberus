using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200019D RID: 413
	[NullableContext(1)]
	public interface IValueProvider
	{
		// Token: 0x06000F05 RID: 3845
		void SetValue(object target, [Nullable(2)] object value);

		// Token: 0x06000F06 RID: 3846
		[return: Nullable(2)]
		object GetValue(object target);
	}
}
