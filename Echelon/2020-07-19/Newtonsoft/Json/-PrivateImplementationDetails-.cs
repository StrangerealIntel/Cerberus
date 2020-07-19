using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// Token: 0x0200022D RID: 557
[CompilerGenerated]
internal sealed class <PrivateImplementationDetails>
{
	// Token: 0x06001637 RID: 5687 RVA: 0x00073B30 File Offset: 0x00071D30
	internal static uint ComputeStringHash(string s)
	{
		uint num;
		if (s != null)
		{
			num = 2166136261u;
			for (int i = 0; i < s.Length; i++)
			{
				num = ((uint)s[i] ^ num) * 16777619u;
			}
		}
		return num;
	}

	// Token: 0x040009B8 RID: 2488 RVA: 0x000837C3 File Offset: 0x000819C3
	internal static readonly Newtonsoft.Json.<PrivateImplementationDetails>.__StaticArrayInitTypeSize=6 3DE43C11C7130AF9014115BCDC2584DFE6B50579;

	// Token: 0x040009B9 RID: 2489 RVA: 0x000837C9 File Offset: 0x000819C9
	internal static readonly Newtonsoft.Json.<PrivateImplementationDetails>.__StaticArrayInitTypeSize=28 9E31F24F64765FCAA589F589324D17C9FCF6A06D;

	// Token: 0x040009BA RID: 2490 RVA: 0x000837E5 File Offset: 0x000819E5
	internal static readonly Newtonsoft.Json.<PrivateImplementationDetails>.__StaticArrayInitTypeSize=10 D40004AB0E92BF6C8DFE481B56BE3D04ABDA76EB;

	// Token: 0x040009BB RID: 2491 RVA: 0x000837EF File Offset: 0x000819EF
	internal static readonly Newtonsoft.Json.<PrivateImplementationDetails>.__StaticArrayInitTypeSize=52 DD3AEFEADB1CD615F3017763F1568179FEE640B0;

	// Token: 0x040009BC RID: 2492 RVA: 0x00083823 File Offset: 0x00081A23
	internal static readonly Newtonsoft.Json.<PrivateImplementationDetails>.__StaticArrayInitTypeSize=36 E289D9D3D233BC253E8C0FA8C2AFDD86A407CE30;

	// Token: 0x040009BD RID: 2493 RVA: 0x00083847 File Offset: 0x00081A47
	internal static readonly Newtonsoft.Json.<PrivateImplementationDetails>.__StaticArrayInitTypeSize=52 E92B39D8233061927D9ACDE54665E68E7535635A;

	// Token: 0x02000327 RID: 807
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 6)]
	private struct __StaticArrayInitTypeSize=6
	{
	}

	// Token: 0x02000328 RID: 808
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 10)]
	private struct __StaticArrayInitTypeSize=10
	{
	}

	// Token: 0x02000329 RID: 809
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 28)]
	private struct __StaticArrayInitTypeSize=28
	{
	}

	// Token: 0x0200032A RID: 810
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 36)]
	private struct __StaticArrayInitTypeSize=36
	{
	}

	// Token: 0x0200032B RID: 811
	[StructLayout(LayoutKind.Explicit, Pack = 1, Size = 52)]
	private struct __StaticArrayInitTypeSize=52
	{
	}
}
