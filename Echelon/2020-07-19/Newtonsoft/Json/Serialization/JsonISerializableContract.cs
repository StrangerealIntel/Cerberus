using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020001A9 RID: 425
	public class JsonISerializableContract : JsonContainerContract
	{
		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x0005748C File Offset: 0x0005568C
		// (set) Token: 0x06000F79 RID: 3961 RVA: 0x00057494 File Offset: 0x00055694
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public ObjectConstructor<object> ISerializableCreator { [return: Nullable(new byte[]
		{
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1
		})] set; }

		// Token: 0x06000F7A RID: 3962 RVA: 0x000574A0 File Offset: 0x000556A0
		[NullableContext(1)]
		public JsonISerializableContract(Type underlyingType) : base(underlyingType)
		{
			this.ContractType = JsonContractType.Serializable;
		}
	}
}
