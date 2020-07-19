using System;
using System.IO;
using System.Text;
using Ionic.Zip;

namespace Ionic
{
	// Token: 0x02000099 RID: 153
	internal class AttributesCriterion : SelectionCriterion
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600030A RID: 778 RVA: 0x000168EC File Offset: 0x00014AEC
		// (set) Token: 0x0600030B RID: 779 RVA: 0x000169A4 File Offset: 0x00014BA4
		internal string AttributeString
		{
			get
			{
				string text = "";
				if ((this._Attributes & FileAttributes.Hidden) != (FileAttributes)0)
				{
					text += "H";
				}
				if ((this._Attributes & FileAttributes.System) != (FileAttributes)0)
				{
					text += "S";
				}
				if ((this._Attributes & FileAttributes.ReadOnly) != (FileAttributes)0)
				{
					text += "R";
				}
				if ((this._Attributes & FileAttributes.Archive) != (FileAttributes)0)
				{
					text += "A";
				}
				if ((this._Attributes & FileAttributes.ReparsePoint) != (FileAttributes)0)
				{
					text += "L";
				}
				if ((this._Attributes & FileAttributes.NotContentIndexed) != (FileAttributes)0)
				{
					text += "I";
				}
				return text;
			}
			set
			{
				this._Attributes = FileAttributes.Normal;
				foreach (char c in value.ToUpper())
				{
					char c2 = c;
					if (c2 != 'A')
					{
						switch (c2)
						{
						case 'H':
							if ((this._Attributes & FileAttributes.Hidden) != (FileAttributes)0)
							{
								throw new ArgumentException(string.Format("Repeated flag. ({0})", c), "value");
							}
							this._Attributes |= FileAttributes.Hidden;
							goto IL_1D9;
						case 'I':
							if ((this._Attributes & FileAttributes.NotContentIndexed) != (FileAttributes)0)
							{
								throw new ArgumentException(string.Format("Repeated flag. ({0})", c), "value");
							}
							this._Attributes |= FileAttributes.NotContentIndexed;
							goto IL_1D9;
						case 'J':
						case 'K':
							break;
						case 'L':
							if ((this._Attributes & FileAttributes.ReparsePoint) != (FileAttributes)0)
							{
								throw new ArgumentException(string.Format("Repeated flag. ({0})", c), "value");
							}
							this._Attributes |= FileAttributes.ReparsePoint;
							goto IL_1D9;
						default:
							switch (c2)
							{
							case 'R':
								if ((this._Attributes & FileAttributes.ReadOnly) != (FileAttributes)0)
								{
									throw new ArgumentException(string.Format("Repeated flag. ({0})", c), "value");
								}
								this._Attributes |= FileAttributes.ReadOnly;
								goto IL_1D9;
							case 'S':
								if ((this._Attributes & FileAttributes.System) != (FileAttributes)0)
								{
									throw new ArgumentException(string.Format("Repeated flag. ({0})", c), "value");
								}
								this._Attributes |= FileAttributes.System;
								goto IL_1D9;
							}
							break;
						}
						throw new ArgumentException(value);
					}
					if ((this._Attributes & FileAttributes.Archive) != (FileAttributes)0)
					{
						throw new ArgumentException(string.Format("Repeated flag. ({0})", c), "value");
					}
					this._Attributes |= FileAttributes.Archive;
					IL_1D9:;
				}
			}
		}

		// Token: 0x0600030C RID: 780 RVA: 0x00016BA0 File Offset: 0x00014DA0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("attributes ").Append(EnumUtil.GetDescription(this.Operator)).Append(" ").Append(this.AttributeString);
			return stringBuilder.ToString();
		}

		// Token: 0x0600030D RID: 781 RVA: 0x00016BF4 File Offset: 0x00014DF4
		private bool _EvaluateOne(FileAttributes fileAttrs, FileAttributes criterionAttrs)
		{
			return (this._Attributes & criterionAttrs) != criterionAttrs || (fileAttrs & criterionAttrs) == criterionAttrs;
		}

		// Token: 0x0600030E RID: 782 RVA: 0x00016C24 File Offset: 0x00014E24
		internal override bool Evaluate(string filename)
		{
			if (Directory.Exists(filename))
			{
				return this.Operator != ComparisonOperator.EqualTo;
			}
			FileAttributes attributes = File.GetAttributes(filename);
			return this._Evaluate(attributes);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x00016C5C File Offset: 0x00014E5C
		private bool _Evaluate(FileAttributes fileAttrs)
		{
			bool flag = this._EvaluateOne(fileAttrs, FileAttributes.Hidden);
			if (flag)
			{
				flag = this._EvaluateOne(fileAttrs, FileAttributes.System);
			}
			if (flag)
			{
				flag = this._EvaluateOne(fileAttrs, FileAttributes.ReadOnly);
			}
			if (flag)
			{
				flag = this._EvaluateOne(fileAttrs, FileAttributes.Archive);
			}
			if (flag)
			{
				flag = this._EvaluateOne(fileAttrs, FileAttributes.NotContentIndexed);
			}
			if (flag)
			{
				flag = this._EvaluateOne(fileAttrs, FileAttributes.ReparsePoint);
			}
			if (this.Operator != ComparisonOperator.EqualTo)
			{
				flag = !flag;
			}
			return flag;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x00016CDC File Offset: 0x00014EDC
		internal override bool Evaluate(ZipEntry entry)
		{
			FileAttributes attributes = entry.Attributes;
			return this._Evaluate(attributes);
		}

		// Token: 0x04000188 RID: 392
		private FileAttributes _Attributes;

		// Token: 0x04000189 RID: 393
		internal ComparisonOperator Operator;
	}
}
