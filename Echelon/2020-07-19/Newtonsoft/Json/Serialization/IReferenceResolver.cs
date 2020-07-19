using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200019A RID: 410
	[NullableContext(1)]
	public interface IReferenceResolver
	{
		// Token: 0x06000EFD RID: 3837
		object ResolveReference(object context, string reference);

		// Token: 0x06000EFE RID: 3838
		string GetReference(object context, object value);

		// Token: 0x06000EFF RID: 3839
		bool IsReferenced(object context, object value);

		// Token: 0x06000F00 RID: 3840
		void AddReference(object context, string reference, object value);
	}
}
