using System;
using System.Runtime.CompilerServices;
using System.Xml.Linq;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000213 RID: 531
	[NullableContext(1)]
	[Nullable(0)]
	internal class XDocumentTypeWrapper : XObjectWrapper, IXmlDocumentType, IXmlNode
	{
		// Token: 0x0600153D RID: 5437 RVA: 0x0006F6C8 File Offset: 0x0006D8C8
		public XDocumentTypeWrapper(XDocumentType documentType) : base(documentType)
		{
			this._documentType = documentType;
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x0600153E RID: 5438 RVA: 0x0006F6D8 File Offset: 0x0006D8D8
		public string Name
		{
			get
			{
				return this._documentType.Name;
			}
		}

		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x0600153F RID: 5439 RVA: 0x0006F6E8 File Offset: 0x0006D8E8
		public string System
		{
			get
			{
				return this._documentType.SystemId;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001540 RID: 5440 RVA: 0x0006F6F8 File Offset: 0x0006D8F8
		public string Public
		{
			get
			{
				return this._documentType.PublicId;
			}
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06001541 RID: 5441 RVA: 0x0006F708 File Offset: 0x0006D908
		public string InternalSubset
		{
			get
			{
				return this._documentType.InternalSubset;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001542 RID: 5442 RVA: 0x0006F718 File Offset: 0x0006D918
		[Nullable(2)]
		public override string LocalName
		{
			[NullableContext(2)]
			get
			{
				return "DOCTYPE";
			}
		}

		// Token: 0x04000961 RID: 2401
		private readonly XDocumentType _documentType;
	}
}
