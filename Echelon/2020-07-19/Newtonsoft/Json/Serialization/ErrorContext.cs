using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000195 RID: 405
	[NullableContext(1)]
	[Nullable(0)]
	public class ErrorContext
	{
		// Token: 0x06000EEB RID: 3819 RVA: 0x00055FB8 File Offset: 0x000541B8
		internal ErrorContext([Nullable(2)] object originalObject, [Nullable(2)] object member, string path, Exception error)
		{
			this.OriginalObject = originalObject;
			this.Member = member;
			this.Error = error;
			this.Path = path;
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x00055FE0 File Offset: 0x000541E0
		// (set) Token: 0x06000EED RID: 3821 RVA: 0x00055FE8 File Offset: 0x000541E8
		internal bool Traced { get; set; }

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000EEE RID: 3822 RVA: 0x00055FF4 File Offset: 0x000541F4
		public Exception Error { get; }

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000EEF RID: 3823 RVA: 0x00055FFC File Offset: 0x000541FC
		[Nullable(2)]
		public object OriginalObject { [NullableContext(2)] get; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000EF0 RID: 3824 RVA: 0x00056004 File Offset: 0x00054204
		[Nullable(2)]
		public object Member { [NullableContext(2)] get; }

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x0005600C File Offset: 0x0005420C
		public string Path { get; }

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000EF2 RID: 3826 RVA: 0x00056014 File Offset: 0x00054214
		// (set) Token: 0x06000EF3 RID: 3827 RVA: 0x0005601C File Offset: 0x0005421C
		public bool Handled { get; set; }
	}
}
