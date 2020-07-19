using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000181 RID: 385
	[NullableContext(2)]
	[Nullable(0)]
	internal class ReflectionMember
	{
		// Token: 0x170002F4 RID: 756
		// (get) Token: 0x06000E0A RID: 3594 RVA: 0x00051500 File Offset: 0x0004F700
		// (set) Token: 0x06000E0B RID: 3595 RVA: 0x00051508 File Offset: 0x0004F708
		public Type MemberType { get; set; }

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x06000E0C RID: 3596 RVA: 0x00051514 File Offset: 0x0004F714
		// (set) Token: 0x06000E0D RID: 3597 RVA: 0x0005151C File Offset: 0x0004F71C
		[Nullable(new byte[]
		{
			2,
			1,
			2
		})]
		public Func<object, object> Getter { [return: Nullable(new byte[]
		{
			2,
			1,
			2
		})] get; [param: Nullable(new byte[]
		{
			2,
			1,
			2
		})] set; }

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x00051528 File Offset: 0x0004F728
		// (set) Token: 0x06000E0F RID: 3599 RVA: 0x00051530 File Offset: 0x0004F730
		[Nullable(new byte[]
		{
			2,
			1,
			2
		})]
		public Action<object, object> Setter { [return: Nullable(new byte[]
		{
			2,
			1,
			2
		})] get; [param: Nullable(new byte[]
		{
			2,
			1,
			2
		})] set; }
	}
}
