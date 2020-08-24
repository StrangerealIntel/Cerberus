using System;

namespace RedLine.Logic.Extensions
{
	// Token: 0x02000066 RID: 102
	public static class Extensions
	{
		// Token: 0x060002C0 RID: 704 RVA: 0x0000C1BB File Offset: 0x0000A3BB
		public static T ForceTo<T>(this object @this)
		{
			return (T)((object)Convert.ChangeType(@this, typeof(T)));
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0000C1D2 File Offset: 0x0000A3D2
		public static string StripQuotes(this string value)
		{
			return value.Replace("\"", string.Empty);
		}
	}
}
