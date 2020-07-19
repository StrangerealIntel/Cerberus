using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x02000130 RID: 304
	[NullableContext(1)]
	public interface IArrayPool<[Nullable(2)] T>
	{
		// Token: 0x060009BA RID: 2490
		T[] Rent(int minimumLength);

		// Token: 0x060009BB RID: 2491
		void Return([Nullable(new byte[]
		{
			2,
			1
		})] T[] array);
	}
}
