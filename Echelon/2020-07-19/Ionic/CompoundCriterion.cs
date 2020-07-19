using System;
using System.Text;
using Ionic.Zip;

namespace Ionic
{
	// Token: 0x0200009A RID: 154
	internal class CompoundCriterion : SelectionCriterion
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000312 RID: 786 RVA: 0x00016D04 File Offset: 0x00014F04
		// (set) Token: 0x06000313 RID: 787 RVA: 0x00016D0C File Offset: 0x00014F0C
		internal SelectionCriterion Right
		{
			get
			{
				return this._Right;
			}
			set
			{
				this._Right = value;
				if (value == null)
				{
					this.Conjunction = LogicalConjunction.NONE;
					return;
				}
				if (this.Conjunction == LogicalConjunction.NONE)
				{
					this.Conjunction = LogicalConjunction.AND;
				}
			}
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00016D38 File Offset: 0x00014F38
		internal override bool Evaluate(string filename)
		{
			bool flag = this.Left.Evaluate(filename);
			switch (this.Conjunction)
			{
			case LogicalConjunction.AND:
				if (flag)
				{
					flag = this.Right.Evaluate(filename);
				}
				break;
			case LogicalConjunction.OR:
				if (!flag)
				{
					flag = this.Right.Evaluate(filename);
				}
				break;
			case LogicalConjunction.XOR:
				flag ^= this.Right.Evaluate(filename);
				break;
			default:
				throw new ArgumentException("Conjunction");
			}
			return flag;
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00016DC8 File Offset: 0x00014FC8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(").Append((this.Left != null) ? this.Left.ToString() : "null").Append(" ").Append(this.Conjunction.ToString()).Append(" ").Append((this.Right != null) ? this.Right.ToString() : "null").Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00016E70 File Offset: 0x00015070
		internal override bool Evaluate(ZipEntry entry)
		{
			bool flag = this.Left.Evaluate(entry);
			switch (this.Conjunction)
			{
			case LogicalConjunction.AND:
				if (flag)
				{
					flag = this.Right.Evaluate(entry);
				}
				break;
			case LogicalConjunction.OR:
				if (!flag)
				{
					flag = this.Right.Evaluate(entry);
				}
				break;
			case LogicalConjunction.XOR:
				flag ^= this.Right.Evaluate(entry);
				break;
			}
			return flag;
		}

		// Token: 0x0400018A RID: 394
		internal LogicalConjunction Conjunction;

		// Token: 0x0400018B RID: 395
		internal SelectionCriterion Left;

		// Token: 0x0400018C RID: 396
		private SelectionCriterion _Right;
	}
}
