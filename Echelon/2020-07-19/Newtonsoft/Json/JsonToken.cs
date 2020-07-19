using System;

namespace Newtonsoft.Json
{
	// Token: 0x0200014C RID: 332
	public enum JsonToken
	{
		// Token: 0x04000665 RID: 1637
		None,
		// Token: 0x04000666 RID: 1638
		StartObject,
		// Token: 0x04000667 RID: 1639
		StartArray,
		// Token: 0x04000668 RID: 1640
		StartConstructor,
		// Token: 0x04000669 RID: 1641
		PropertyName,
		// Token: 0x0400066A RID: 1642
		Comment,
		// Token: 0x0400066B RID: 1643
		Raw,
		// Token: 0x0400066C RID: 1644
		Integer,
		// Token: 0x0400066D RID: 1645
		Float,
		// Token: 0x0400066E RID: 1646
		String,
		// Token: 0x0400066F RID: 1647
		Boolean,
		// Token: 0x04000670 RID: 1648
		Null,
		// Token: 0x04000671 RID: 1649
		Undefined,
		// Token: 0x04000672 RID: 1650
		EndObject,
		// Token: 0x04000673 RID: 1651
		EndArray,
		// Token: 0x04000674 RID: 1652
		EndConstructor,
		// Token: 0x04000675 RID: 1653
		Date,
		// Token: 0x04000676 RID: 1654
		Bytes
	}
}
