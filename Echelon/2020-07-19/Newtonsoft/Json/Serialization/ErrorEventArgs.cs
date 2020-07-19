using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000196 RID: 406
	[NullableContext(1)]
	[Nullable(0)]
	public class ErrorEventArgs : EventArgs
	{
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000EF4 RID: 3828 RVA: 0x00056028 File Offset: 0x00054228
		[Nullable(2)]
		public object CurrentObject { [NullableContext(2)] get; }

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x00056030 File Offset: 0x00054230
		public ErrorContext ErrorContext { get; }

		// Token: 0x06000EF6 RID: 3830 RVA: 0x00056038 File Offset: 0x00054238
		public ErrorEventArgs([Nullable(2)] object currentObject, ErrorContext errorContext)
		{
			this.CurrentObject = currentObject;
			this.ErrorContext = errorContext;
		}
	}
}
