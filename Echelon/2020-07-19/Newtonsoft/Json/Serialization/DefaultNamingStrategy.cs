using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000190 RID: 400
	public class DefaultNamingStrategy : NamingStrategy
	{
		// Token: 0x06000ED4 RID: 3796 RVA: 0x000559E8 File Offset: 0x00053BE8
		[NullableContext(1)]
		protected override string ResolvePropertyName(string name)
		{
			return name;
		}
	}
}
