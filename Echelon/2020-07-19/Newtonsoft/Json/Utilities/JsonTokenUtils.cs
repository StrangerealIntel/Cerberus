using System;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x0200017A RID: 378
	internal static class JsonTokenUtils
	{
		// Token: 0x06000DDC RID: 3548 RVA: 0x00050CEC File Offset: 0x0004EEEC
		internal static bool IsEndToken(JsonToken token)
		{
			return token - JsonToken.EndObject <= 2;
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x00050CFC File Offset: 0x0004EEFC
		internal static bool IsStartToken(JsonToken token)
		{
			return token - JsonToken.StartObject <= 2;
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x00050D0C File Offset: 0x0004EF0C
		internal static bool IsPrimitiveToken(JsonToken token)
		{
			return token - JsonToken.Integer <= 5 || token - JsonToken.Date <= 1;
		}
	}
}
