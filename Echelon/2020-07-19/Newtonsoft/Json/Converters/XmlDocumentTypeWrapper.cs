using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x0200020B RID: 523
	[NullableContext(1)]
	[Nullable(0)]
	internal class XmlDocumentTypeWrapper : XmlNodeWrapper, IXmlDocumentType, IXmlNode
	{
		// Token: 0x060014FE RID: 5374 RVA: 0x0006F2E8 File Offset: 0x0006D4E8
		public XmlDocumentTypeWrapper(XmlDocumentType documentType) : base(documentType)
		{
			this._documentType = documentType;
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x060014FF RID: 5375 RVA: 0x0006F2F8 File Offset: 0x0006D4F8
		public string Name
		{
			get
			{
				return this._documentType.Name;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001500 RID: 5376 RVA: 0x0006F308 File Offset: 0x0006D508
		public string System
		{
			get
			{
				return this._documentType.SystemId;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001501 RID: 5377 RVA: 0x0006F318 File Offset: 0x0006D518
		public string Public
		{
			get
			{
				return this._documentType.PublicId;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001502 RID: 5378 RVA: 0x0006F328 File Offset: 0x0006D528
		public string InternalSubset
		{
			get
			{
				return this._documentType.InternalSubset;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001503 RID: 5379 RVA: 0x0006F338 File Offset: 0x0006D538
		[Nullable(2)]
		public override string LocalName
		{
			[NullableContext(2)]
			get
			{
				return "DOCTYPE";
			}
		}

		// Token: 0x0400095C RID: 2396
		private readonly XmlDocumentType _documentType;
	}
}
