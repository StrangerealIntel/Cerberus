using System;
using System.IO;
using System.Text;
using Ionic.Zip;

namespace Ionic
{
	// Token: 0x02000098 RID: 152
	internal class TypeCriterion : SelectionCriterion
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000304 RID: 772 RVA: 0x000167A0 File Offset: 0x000149A0
		// (set) Token: 0x06000305 RID: 773 RVA: 0x000167B0 File Offset: 0x000149B0
		internal string AttributeString
		{
			get
			{
				return this.ObjectType.ToString();
			}
			set
			{
				if (value.Length != 1 || (value[0] != 'D' && value[0] != 'F'))
				{
					throw new ArgumentException("Specify a single character: either D or F");
				}
				this.ObjectType = value[0];
			}
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00016804 File Offset: 0x00014A04
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("type ").Append(EnumUtil.GetDescription(this.Operator)).Append(" ").Append(this.AttributeString);
			return stringBuilder.ToString();
		}

		// Token: 0x06000307 RID: 775 RVA: 0x00016858 File Offset: 0x00014A58
		internal override bool Evaluate(string filename)
		{
			bool flag = (this.ObjectType == 'D') ? Directory.Exists(filename) : File.Exists(filename);
			if (this.Operator != ComparisonOperator.EqualTo)
			{
				flag = !flag;
			}
			return flag;
		}

		// Token: 0x06000308 RID: 776 RVA: 0x0001689C File Offset: 0x00014A9C
		internal override bool Evaluate(ZipEntry entry)
		{
			bool flag = (this.ObjectType == 'D') ? entry.IsDirectory : (!entry.IsDirectory);
			if (this.Operator != ComparisonOperator.EqualTo)
			{
				flag = !flag;
			}
			return flag;
		}

		// Token: 0x04000186 RID: 390
		private char ObjectType;

		// Token: 0x04000187 RID: 391
		internal ComparisonOperator Operator;
	}
}
