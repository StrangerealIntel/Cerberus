using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020001AA RID: 426
	public class JsonLinqContract : JsonContract
	{
		// Token: 0x06000F7B RID: 3963 RVA: 0x000574B0 File Offset: 0x000556B0
		[NullableContext(1)]
		public JsonLinqContract(Type underlyingType) : base(underlyingType)
		{
			this.ContractType = JsonContractType.Linq;
		}
	}
}
