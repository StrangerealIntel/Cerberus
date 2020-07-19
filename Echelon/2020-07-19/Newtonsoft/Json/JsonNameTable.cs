using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json
{
	// Token: 0x0200013E RID: 318
	public abstract class JsonNameTable
	{
		// Token: 0x06000A45 RID: 2629
		[NullableContext(1)]
		[return: Nullable(2)]
		public abstract string Get(char[] key, int start, int length);
	}
}
