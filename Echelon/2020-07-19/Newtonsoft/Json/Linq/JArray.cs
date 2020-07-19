using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020001D4 RID: 468
	[NullableContext(1)]
	[Nullable(0)]
	public class JArray : JContainer, IList<JToken>, ICollection<JToken>, IEnumerable<JToken>, IEnumerable
	{
		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06001205 RID: 4613 RVA: 0x00063714 File Offset: 0x00061914
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001206 RID: 4614 RVA: 0x0006371C File Offset: 0x0006191C
		public override JTokenType Type
		{
			get
			{
				return JTokenType.Array;
			}
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x00063720 File Offset: 0x00061920
		public JArray()
		{
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x00063734 File Offset: 0x00061934
		public JArray(JArray other) : base(other)
		{
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x00063748 File Offset: 0x00061948
		public JArray(params object[] content) : this(content)
		{
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x00063754 File Offset: 0x00061954
		public JArray(object content)
		{
			this.Add(content);
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00063770 File Offset: 0x00061970
		internal override bool DeepEquals(JToken node)
		{
			JArray jarray = node as JArray;
			return jarray != null && base.ContentsEqual(jarray);
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x00063798 File Offset: 0x00061998
		internal override JToken CloneToken()
		{
			return new JArray(this);
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x000637A0 File Offset: 0x000619A0
		public new static JArray Load(JsonReader reader)
		{
			return JArray.Load(reader, null);
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x000637AC File Offset: 0x000619AC
		public new static JArray Load(JsonReader reader, [Nullable(2)] JsonLoadSettings settings)
		{
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading JArray from JsonReader.");
			}
			reader.MoveToContent();
			if (reader.TokenType != JsonToken.StartArray)
			{
				throw JsonReaderException.Create(reader, "Error reading JArray from JsonReader. Current JsonReader item is not an array: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JArray jarray = new JArray();
			jarray.SetLineInfo(reader as IJsonLineInfo, settings);
			jarray.ReadTokenFrom(reader, settings);
			return jarray;
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x00063830 File Offset: 0x00061A30
		public new static JArray Parse(string json)
		{
			return JArray.Parse(json, null);
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x0006383C File Offset: 0x00061A3C
		public new static JArray Parse(string json, [Nullable(2)] JsonLoadSettings settings)
		{
			JArray result;
			using (JsonReader jsonReader = new JsonTextReader(new StringReader(json)))
			{
				JArray jarray = JArray.Load(jsonReader, settings);
				while (jsonReader.Read())
				{
				}
				result = jarray;
			}
			return result;
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x0006388C File Offset: 0x00061A8C
		public new static JArray FromObject(object o)
		{
			return JArray.FromObject(o, JsonSerializer.CreateDefault());
		}

		// Token: 0x06001212 RID: 4626 RVA: 0x0006389C File Offset: 0x00061A9C
		public new static JArray FromObject(object o, JsonSerializer jsonSerializer)
		{
			JToken jtoken = JToken.FromObjectInternal(o, jsonSerializer);
			if (jtoken.Type != JTokenType.Array)
			{
				throw new ArgumentException("Object serialized to {0}. JArray instance expected.".FormatWith(CultureInfo.InvariantCulture, jtoken.Type));
			}
			return (JArray)jtoken;
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x000638E8 File Offset: 0x00061AE8
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WriteStartArray();
			for (int i = 0; i < this._values.Count; i++)
			{
				this._values[i].WriteTo(writer, converters);
			}
			writer.WriteEndArray();
		}

		// Token: 0x170003E6 RID: 998
		[Nullable(2)]
		public override JToken this[object key]
		{
			[return: Nullable(2)]
			get
			{
				ValidationUtils.ArgumentNotNull(key, "key");
				if (!(key is int))
				{
					throw new ArgumentException("Accessed JArray values with invalid key value: {0}. Int32 array index expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				return this.GetItem((int)key);
			}
			[param: Nullable(2)]
			set
			{
				ValidationUtils.ArgumentNotNull(key, "key");
				if (!(key is int))
				{
					throw new ArgumentException("Set JArray values with invalid key value: {0}. Int32 array index expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				this.SetItem((int)key, value);
			}
		}

		// Token: 0x170003E7 RID: 999
		public JToken this[int index]
		{
			get
			{
				return this.GetItem(index);
			}
			set
			{
				this.SetItem(index, value);
			}
		}

		// Token: 0x06001218 RID: 4632 RVA: 0x000639CC File Offset: 0x00061BCC
		[NullableContext(2)]
		internal override int IndexOfItem(JToken item)
		{
			if (item == null)
			{
				return -1;
			}
			return this._values.IndexOfReference(item);
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x000639E4 File Offset: 0x00061BE4
		internal override void MergeItem(object content, [Nullable(2)] JsonMergeSettings settings)
		{
			IEnumerable enumerable = (base.IsMultiContent(content) || content is JArray) ? ((IEnumerable)content) : null;
			if (enumerable == null)
			{
				return;
			}
			JContainer.MergeEnumerableContent(this, enumerable, settings);
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x00063A28 File Offset: 0x00061C28
		public int IndexOf(JToken item)
		{
			return this.IndexOfItem(item);
		}

		// Token: 0x0600121B RID: 4635 RVA: 0x00063A34 File Offset: 0x00061C34
		public void Insert(int index, JToken item)
		{
			this.InsertItem(index, item, false);
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x00063A40 File Offset: 0x00061C40
		public void RemoveAt(int index)
		{
			this.RemoveItemAt(index);
		}

		// Token: 0x0600121D RID: 4637 RVA: 0x00063A4C File Offset: 0x00061C4C
		public IEnumerator<JToken> GetEnumerator()
		{
			return this.Children().GetEnumerator();
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x00063A6C File Offset: 0x00061C6C
		public void Add(JToken item)
		{
			this.Add(item);
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x00063A78 File Offset: 0x00061C78
		public void Clear()
		{
			this.ClearItems();
		}

		// Token: 0x06001220 RID: 4640 RVA: 0x00063A80 File Offset: 0x00061C80
		public bool Contains(JToken item)
		{
			return this.ContainsItem(item);
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00063A8C File Offset: 0x00061C8C
		public void CopyTo(JToken[] array, int arrayIndex)
		{
			this.CopyItemsTo(array, arrayIndex);
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06001222 RID: 4642 RVA: 0x00063A98 File Offset: 0x00061C98
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00063A9C File Offset: 0x00061C9C
		public bool Remove(JToken item)
		{
			return this.RemoveItem(item);
		}

		// Token: 0x06001224 RID: 4644 RVA: 0x00063AA8 File Offset: 0x00061CA8
		internal override int GetDeepHashCode()
		{
			return base.ContentsHashCode();
		}

		// Token: 0x040008D1 RID: 2257
		private readonly List<JToken> _values = new List<JToken>();
	}
}
