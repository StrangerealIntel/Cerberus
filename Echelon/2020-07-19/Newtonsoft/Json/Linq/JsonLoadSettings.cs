using System;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020001DD RID: 477
	public class JsonLoadSettings
	{
		// Token: 0x0600131E RID: 4894 RVA: 0x00066578 File Offset: 0x00064778
		public JsonLoadSettings()
		{
			this._lineInfoHandling = LineInfoHandling.Load;
			this._commentHandling = CommentHandling.Ignore;
			this._duplicatePropertyNameHandling = DuplicatePropertyNameHandling.Replace;
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x00066598 File Offset: 0x00064798
		// (set) Token: 0x06001320 RID: 4896 RVA: 0x000665A0 File Offset: 0x000647A0
		public CommentHandling CommentHandling
		{
			get
			{
				return this._commentHandling;
			}
			set
			{
				if (value < CommentHandling.Ignore || value > CommentHandling.Load)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._commentHandling = value;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06001321 RID: 4897 RVA: 0x000665C4 File Offset: 0x000647C4
		// (set) Token: 0x06001322 RID: 4898 RVA: 0x000665CC File Offset: 0x000647CC
		public LineInfoHandling LineInfoHandling
		{
			get
			{
				return this._lineInfoHandling;
			}
			set
			{
				if (value < LineInfoHandling.Ignore || value > LineInfoHandling.Load)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._lineInfoHandling = value;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x000665F0 File Offset: 0x000647F0
		// (set) Token: 0x06001324 RID: 4900 RVA: 0x000665F8 File Offset: 0x000647F8
		public DuplicatePropertyNameHandling DuplicatePropertyNameHandling
		{
			get
			{
				return this._duplicatePropertyNameHandling;
			}
			set
			{
				if (value < DuplicatePropertyNameHandling.Replace || value > DuplicatePropertyNameHandling.Error)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._duplicatePropertyNameHandling = value;
			}
		}

		// Token: 0x040008E2 RID: 2274
		private CommentHandling _commentHandling;

		// Token: 0x040008E3 RID: 2275
		private LineInfoHandling _lineInfoHandling;

		// Token: 0x040008E4 RID: 2276
		private DuplicatePropertyNameHandling _duplicatePropertyNameHandling;
	}
}
