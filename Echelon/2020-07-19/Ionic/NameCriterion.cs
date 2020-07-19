using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Ionic.Zip;

namespace Ionic
{
	// Token: 0x02000097 RID: 151
	internal class NameCriterion : SelectionCriterion
	{
		// Token: 0x17000053 RID: 83
		// (set) Token: 0x060002FE RID: 766 RVA: 0x00016600 File Offset: 0x00014800
		internal virtual string MatchingFileSpec
		{
			set
			{
				if (Directory.Exists(value))
				{
					this._MatchingFileSpec = ".\\" + value + "\\*.*";
				}
				else
				{
					this._MatchingFileSpec = value;
				}
				this._regexString = "^" + Regex.Escape(this._MatchingFileSpec).Replace("\\\\\\*\\.\\*", "\\\\([^\\.]+|.*\\.[^\\\\\\.]*)").Replace("\\.\\*", "\\.[^\\\\\\.]*").Replace("\\*", ".*").Replace("\\?", "[^\\\\\\.]") + "$";
				this._re = new Regex(this._regexString, RegexOptions.IgnoreCase);
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x000166AC File Offset: 0x000148AC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("name ").Append(EnumUtil.GetDescription(this.Operator)).Append(" '").Append(this._MatchingFileSpec).Append("'");
			return stringBuilder.ToString();
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0001670C File Offset: 0x0001490C
		internal override bool Evaluate(string filename)
		{
			return this._Evaluate(filename);
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00016718 File Offset: 0x00014918
		private bool _Evaluate(string fullpath)
		{
			string input = (this._MatchingFileSpec.IndexOf('\\') == -1) ? Path.GetFileName(fullpath) : fullpath;
			bool flag = this._re.IsMatch(input);
			if (this.Operator != ComparisonOperator.EqualTo)
			{
				flag = !flag;
			}
			return flag;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00016768 File Offset: 0x00014968
		internal override bool Evaluate(ZipEntry entry)
		{
			string fullpath = entry.FileName.Replace("/", "\\");
			return this._Evaluate(fullpath);
		}

		// Token: 0x04000182 RID: 386
		private Regex _re;

		// Token: 0x04000183 RID: 387
		private string _regexString;

		// Token: 0x04000184 RID: 388
		internal ComparisonOperator Operator;

		// Token: 0x04000185 RID: 389
		private string _MatchingFileSpec;
	}
}
