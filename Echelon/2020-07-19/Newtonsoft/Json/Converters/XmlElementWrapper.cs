using System;
using System.Runtime.CompilerServices;
using System.Xml;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x02000209 RID: 521
	[NullableContext(1)]
	[Nullable(0)]
	internal class XmlElementWrapper : XmlNodeWrapper, IXmlElement, IXmlNode
	{
		// Token: 0x060014F4 RID: 5364 RVA: 0x0006F228 File Offset: 0x0006D428
		public XmlElementWrapper(XmlElement element) : base(element)
		{
			this._element = element;
		}

		// Token: 0x060014F5 RID: 5365 RVA: 0x0006F238 File Offset: 0x0006D438
		public void SetAttributeNode(IXmlNode attribute)
		{
			XmlNodeWrapper xmlNodeWrapper = (XmlNodeWrapper)attribute;
			this._element.SetAttributeNode((XmlAttribute)xmlNodeWrapper.WrappedNode);
		}

		// Token: 0x060014F6 RID: 5366 RVA: 0x0006F268 File Offset: 0x0006D468
		public string GetPrefixOfNamespace(string namespaceUri)
		{
			return this._element.GetPrefixOfNamespace(namespaceUri);
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x0006F278 File Offset: 0x0006D478
		public bool IsEmpty
		{
			get
			{
				return this._element.IsEmpty;
			}
		}

		// Token: 0x0400095A RID: 2394
		private readonly XmlElement _element;
	}
}
