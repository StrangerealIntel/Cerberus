using System;
using System.Collections.Generic;
using System.Text;

namespace Echelon.Stealer.Browsers.Gecko
{
	// Token: 0x02000037 RID: 55
	public class Gecko4
	{
		// Token: 0x06000152 RID: 338 RVA: 0x0000AE9C File Offset: 0x0000909C
		public Gecko4()
		{
			this.Objects = new List<Gecko4>();
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000AEB0 File Offset: 0x000090B0
		// (set) Token: 0x06000154 RID: 340 RVA: 0x0000AEB8 File Offset: 0x000090B8
		public Gecko2 ObjectType { get; set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000AEC4 File Offset: 0x000090C4
		// (set) Token: 0x06000156 RID: 342 RVA: 0x0000AECC File Offset: 0x000090CC
		public byte[] ObjectData { get; set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000157 RID: 343 RVA: 0x0000AED8 File Offset: 0x000090D8
		// (set) Token: 0x06000158 RID: 344 RVA: 0x0000AEE0 File Offset: 0x000090E0
		public int ObjectLength { get; set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000159 RID: 345 RVA: 0x0000AEEC File Offset: 0x000090EC
		// (set) Token: 0x0600015A RID: 346 RVA: 0x0000AEF4 File Offset: 0x000090F4
		public List<Gecko4> Objects { get; set; }

		// Token: 0x0600015B RID: 347 RVA: 0x0000AF00 File Offset: 0x00009100
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			Gecko2 objectType = this.ObjectType;
			switch (objectType)
			{
			case Gecko2.Integer:
				foreach (byte b in this.ObjectData)
				{
					stringBuilder2.AppendFormat("{0:X2}", b);
				}
				stringBuilder.Append("\tINTEGER ").Append(stringBuilder2).AppendLine();
				break;
			case Gecko2.BitString:
			case Gecko2.Null:
				break;
			case Gecko2.OctetString:
				foreach (byte b2 in this.ObjectData)
				{
					stringBuilder2.AppendFormat("{0:X2}", b2);
				}
				stringBuilder.Append("\tOCTETSTRING ").AppendLine(stringBuilder2.ToString());
				break;
			case Gecko2.ObjectIdentifier:
				foreach (byte b3 in this.ObjectData)
				{
					stringBuilder2.AppendFormat("{0:X2}", b3);
				}
				stringBuilder.Append("\tOBJECTIDENTIFIER ").AppendLine(stringBuilder2.ToString());
				break;
			default:
				if (objectType == Gecko2.Sequence)
				{
					stringBuilder.AppendLine("SEQUENCE {");
				}
				break;
			}
			foreach (Gecko4 value in this.Objects)
			{
				stringBuilder.Append(value);
			}
			if (this.ObjectType == Gecko2.Sequence)
			{
				stringBuilder.AppendLine("}");
			}
			stringBuilder2.Remove(0, stringBuilder2.Length - 1);
			return stringBuilder.ToString();
		}
	}
}
