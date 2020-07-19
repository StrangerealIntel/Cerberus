using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020001BC RID: 444
	[NullableContext(1)]
	[Nullable(0)]
	internal class SerializationBinderAdapter : ISerializationBinder
	{
		// Token: 0x060010BA RID: 4282 RVA: 0x0005EEF4 File Offset: 0x0005D0F4
		public SerializationBinderAdapter(SerializationBinder serializationBinder)
		{
			this.SerializationBinder = serializationBinder;
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x0005EF04 File Offset: 0x0005D104
		public Type BindToType([Nullable(2)] string assemblyName, string typeName)
		{
			return this.SerializationBinder.BindToType(assemblyName, typeName);
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x0005EF14 File Offset: 0x0005D114
		[NullableContext(2)]
		public void BindToName([Nullable(1)] Type serializedType, out string assemblyName, out string typeName)
		{
			this.SerializationBinder.BindToName(serializedType, out assemblyName, out typeName);
		}

		// Token: 0x0400083D RID: 2109
		public readonly SerializationBinder SerializationBinder;
	}
}
