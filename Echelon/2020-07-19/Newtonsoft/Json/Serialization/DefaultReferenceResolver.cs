using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000191 RID: 401
	[NullableContext(1)]
	[Nullable(0)]
	internal class DefaultReferenceResolver : IReferenceResolver
	{
		// Token: 0x06000ED6 RID: 3798 RVA: 0x000559F4 File Offset: 0x00053BF4
		private BidirectionalDictionary<string, object> GetMappings(object context)
		{
			JsonSerializerInternalBase jsonSerializerInternalBase = context as JsonSerializerInternalBase;
			if (jsonSerializerInternalBase == null)
			{
				JsonSerializerProxy jsonSerializerProxy = context as JsonSerializerProxy;
				if (jsonSerializerProxy == null)
				{
					throw new JsonException("The DefaultReferenceResolver can only be used internally.");
				}
				jsonSerializerInternalBase = jsonSerializerProxy.GetInternalSerializer();
			}
			return jsonSerializerInternalBase.DefaultReferenceMappings;
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x00055A3C File Offset: 0x00053C3C
		public object ResolveReference(object context, string reference)
		{
			object result;
			this.GetMappings(context).TryGetByFirst(reference, out result);
			return result;
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x00055A60 File Offset: 0x00053C60
		public string GetReference(object context, object value)
		{
			BidirectionalDictionary<string, object> mappings = this.GetMappings(context);
			string text;
			if (!mappings.TryGetBySecond(value, out text))
			{
				this._referenceCount++;
				text = this._referenceCount.ToString(CultureInfo.InvariantCulture);
				mappings.Set(text, value);
			}
			return text;
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x00055AB0 File Offset: 0x00053CB0
		public void AddReference(object context, string reference, object value)
		{
			this.GetMappings(context).Set(reference, value);
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x00055AC0 File Offset: 0x00053CC0
		public bool IsReferenced(object context, object value)
		{
			string text;
			return this.GetMappings(context).TryGetBySecond(value, out text);
		}

		// Token: 0x04000792 RID: 1938
		private int _referenceCount;
	}
}
