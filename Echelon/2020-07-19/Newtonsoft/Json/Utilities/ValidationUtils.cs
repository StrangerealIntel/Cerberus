using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200018B RID: 395
	internal static class ValidationUtils
	{
		// Token: 0x06000E88 RID: 3720 RVA: 0x000535D0 File Offset: 0x000517D0
		[NullableContext(1)]
		public static void ArgumentNotNull([Nullable(2), NotNull] object value, string parameterName)
		{
			if (value == null)
			{
				throw new ArgumentNullException(parameterName);
			}
		}
	}
}
