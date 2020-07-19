using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200019B RID: 411
	[NullableContext(1)]
	public interface ISerializationBinder
	{
		// Token: 0x06000F01 RID: 3841
		Type BindToType([Nullable(2)] string assemblyName, string typeName);

		// Token: 0x06000F02 RID: 3842
		[NullableContext(2)]
		void BindToName([Nullable(1)] Type serializedType, out string assemblyName, out string typeName);
	}
}
