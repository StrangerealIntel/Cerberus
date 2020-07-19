using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020001B3 RID: 435
	public class JsonStringContract : JsonPrimitiveContract
	{
		// Token: 0x06001081 RID: 4225 RVA: 0x0005E3EC File Offset: 0x0005C5EC
		[NullableContext(1)]
		public JsonStringContract(Type underlyingType) : base(underlyingType)
		{
			this.ContractType = JsonContractType.String;
		}
	}
}
