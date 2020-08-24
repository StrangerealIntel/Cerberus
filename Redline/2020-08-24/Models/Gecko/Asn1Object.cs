using System;
using System.Collections.Generic;
using System.Text;

namespace RedLine.Models.Gecko
{
	// Token: 0x02000034 RID: 52
	public class Asn1Object
	{
		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00004598 File Offset: 0x00002798
		// (set) Token: 0x06000142 RID: 322 RVA: 0x000045A0 File Offset: 0x000027A0
		public Asn1Type ObjectType { get; set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000143 RID: 323 RVA: 0x000045A9 File Offset: 0x000027A9
		// (set) Token: 0x06000144 RID: 324 RVA: 0x000045B1 File Offset: 0x000027B1
		public byte[] ObjectData { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000145 RID: 325 RVA: 0x000045BA File Offset: 0x000027BA
		// (set) Token: 0x06000146 RID: 326 RVA: 0x000045C2 File Offset: 0x000027C2
		public int ObjectLength { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000147 RID: 327 RVA: 0x000045CB File Offset: 0x000027CB
		// (set) Token: 0x06000148 RID: 328 RVA: 0x000045D3 File Offset: 0x000027D3
		public List<Asn1Object> Objects { get; set; }

		// Token: 0x06000149 RID: 329 RVA: 0x000045DC File Offset: 0x000027DC
		public Asn1Object()
		{
			this.Objects = new List<Asn1Object>();
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000045F0 File Offset: 0x000027F0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			Asn1Type objectType = this.ObjectType;
			switch (objectType)
			{
			case Asn1Type.Integer:
				foreach (byte b in this.ObjectData)
				{
					stringBuilder2.AppendFormat("{0:X2}", b);
				}
				stringBuilder.Append("\tINTEGER ").Append(stringBuilder2).AppendLine();
				break;
			case Asn1Type.BitString:
			case Asn1Type.Null:
				break;
			case Asn1Type.OctetString:
				foreach (byte b2 in this.ObjectData)
				{
					stringBuilder2.AppendFormat("{0:X2}", b2);
				}
				stringBuilder.Append("\tOCTETSTRING ").AppendLine(stringBuilder2.ToString());
				break;
			case Asn1Type.ObjectIdentifier:
				foreach (byte b3 in this.ObjectData)
				{
					stringBuilder2.AppendFormat("{0:X2}", b3);
				}
				stringBuilder.Append("\tOBJECTIDENTIFIER ").AppendLine(stringBuilder2.ToString());
				break;
			default:
				if (objectType == Asn1Type.Sequence)
				{
					stringBuilder.AppendLine("SEQUENCE {");
				}
				break;
			}
			foreach (Asn1Object asn1Object in this.Objects)
			{
				stringBuilder.Append(asn1Object.ToString());
			}
			if (this.ObjectType == Asn1Type.Sequence)
			{
				stringBuilder.AppendLine("}");
			}
			stringBuilder2.Remove(0, stringBuilder2.Length - 1);
			return stringBuilder.ToString();
		}
	}
}
