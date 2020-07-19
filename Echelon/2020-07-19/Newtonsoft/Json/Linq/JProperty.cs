using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020001D9 RID: 473
	[NullableContext(1)]
	[Nullable(0)]
	public class JProperty : JContainer
	{
		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x060012E3 RID: 4835 RVA: 0x00065C2C File Offset: 0x00063E2C
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x060012E4 RID: 4836 RVA: 0x00065C34 File Offset: 0x00063E34
		public string Name
		{
			[DebuggerStepThrough]
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x060012E5 RID: 4837 RVA: 0x00065C3C File Offset: 0x00063E3C
		// (set) Token: 0x060012E6 RID: 4838 RVA: 0x00065C4C File Offset: 0x00063E4C
		public new JToken Value
		{
			[DebuggerStepThrough]
			get
			{
				return this._content._token;
			}
			set
			{
				base.CheckReentrancy();
				JToken item = value ?? JValue.CreateNull();
				if (this._content._token == null)
				{
					this.InsertItem(0, item, false);
					return;
				}
				this.SetItem(0, item);
			}
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x00065C94 File Offset: 0x00063E94
		public JProperty(JProperty other) : base(other)
		{
			this._name = other.Name;
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x00065CB4 File Offset: 0x00063EB4
		internal override JToken GetItem(int index)
		{
			if (index != 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			return this.Value;
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x00065CC8 File Offset: 0x00063EC8
		[NullableContext(2)]
		internal override void SetItem(int index, JToken item)
		{
			if (index != 0)
			{
				throw new ArgumentOutOfRangeException();
			}
			if (JContainer.IsTokenUnchanged(this.Value, item))
			{
				return;
			}
			JObject jobject = (JObject)base.Parent;
			if (jobject != null)
			{
				jobject.InternalPropertyChanging(this);
			}
			base.SetItem(0, item);
			JObject jobject2 = (JObject)base.Parent;
			if (jobject2 == null)
			{
				return;
			}
			jobject2.InternalPropertyChanged(this);
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00065D38 File Offset: 0x00063F38
		[NullableContext(2)]
		internal override bool RemoveItem(JToken item)
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x00065D58 File Offset: 0x00063F58
		internal override void RemoveItemAt(int index)
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00065D78 File Offset: 0x00063F78
		[NullableContext(2)]
		internal override int IndexOfItem(JToken item)
		{
			if (item == null)
			{
				return -1;
			}
			return this._content.IndexOf(item);
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x00065D90 File Offset: 0x00063F90
		[NullableContext(2)]
		internal override void InsertItem(int index, JToken item, bool skipParentCheck)
		{
			if (item != null && item.Type == JTokenType.Comment)
			{
				return;
			}
			if (this.Value != null)
			{
				throw new JsonException("{0} cannot have multiple values.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
			}
			base.InsertItem(0, item, false);
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x00065DE8 File Offset: 0x00063FE8
		[NullableContext(2)]
		internal override bool ContainsItem(JToken item)
		{
			return this.Value == item;
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x00065DF4 File Offset: 0x00063FF4
		internal override void MergeItem(object content, [Nullable(2)] JsonMergeSettings settings)
		{
			JProperty jproperty = content as JProperty;
			JToken jtoken = (jproperty != null) ? jproperty.Value : null;
			if (jtoken != null && jtoken.Type != JTokenType.Null)
			{
				this.Value = jtoken;
			}
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x00065E38 File Offset: 0x00064038
		internal override void ClearItems()
		{
			throw new JsonException("Cannot add or remove items from {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JProperty)));
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x00065E58 File Offset: 0x00064058
		internal override bool DeepEquals(JToken node)
		{
			JProperty jproperty = node as JProperty;
			return jproperty != null && this._name == jproperty.Name && base.ContentsEqual(jproperty);
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x00065E98 File Offset: 0x00064098
		internal override JToken CloneToken()
		{
			return new JProperty(this);
		}

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x060012F3 RID: 4851 RVA: 0x00065EA0 File Offset: 0x000640A0
		public override JTokenType Type
		{
			[DebuggerStepThrough]
			get
			{
				return JTokenType.Property;
			}
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x00065EA4 File Offset: 0x000640A4
		internal JProperty(string name)
		{
			ValidationUtils.ArgumentNotNull(name, "name");
			this._name = name;
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x00065ECC File Offset: 0x000640CC
		public JProperty(string name, params object[] content) : this(name, content)
		{
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x00065ED8 File Offset: 0x000640D8
		public JProperty(string name, [Nullable(2)] object content)
		{
			ValidationUtils.ArgumentNotNull(name, "name");
			this._name = name;
			this.Value = (base.IsMultiContent(content) ? new JArray(content) : JContainer.CreateFromContent(content));
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x00065F30 File Offset: 0x00064130
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WritePropertyName(this._name);
			JToken value = this.Value;
			if (value != null)
			{
				value.WriteTo(writer, converters);
				return;
			}
			writer.WriteNull();
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x00065F6C File Offset: 0x0006416C
		internal override int GetDeepHashCode()
		{
			int hashCode = this._name.GetHashCode();
			JToken value = this.Value;
			return hashCode ^ ((value != null) ? value.GetDeepHashCode() : 0);
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x00065FA4 File Offset: 0x000641A4
		public new static JProperty Load(JsonReader reader)
		{
			return JProperty.Load(reader, null);
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x00065FB0 File Offset: 0x000641B0
		public new static JProperty Load(JsonReader reader, [Nullable(2)] JsonLoadSettings settings)
		{
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading JProperty from JsonReader.");
			}
			reader.MoveToContent();
			if (reader.TokenType != JsonToken.PropertyName)
			{
				throw JsonReaderException.Create(reader, "Error reading JProperty from JsonReader. Current JsonReader item is not a property: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JProperty jproperty = new JProperty((string)reader.Value);
			jproperty.SetLineInfo(reader as IJsonLineInfo, settings);
			jproperty.ReadTokenFrom(reader, settings);
			return jproperty;
		}

		// Token: 0x040008DE RID: 2270
		private readonly JProperty.JPropertyList _content = new JProperty.JPropertyList();

		// Token: 0x040008DF RID: 2271
		private readonly string _name;

		// Token: 0x0200030F RID: 783
		[Nullable(0)]
		private class JPropertyList : IList<JToken>, ICollection<JToken>, IEnumerable<JToken>, IEnumerable
		{
			// Token: 0x06001899 RID: 6297 RVA: 0x0007A238 File Offset: 0x00078438
			public IEnumerator<JToken> GetEnumerator()
			{
				if (this._token != null)
				{
					yield return this._token;
				}
				yield break;
			}

			// Token: 0x0600189A RID: 6298 RVA: 0x0007A248 File Offset: 0x00078448
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x0600189B RID: 6299 RVA: 0x0007A250 File Offset: 0x00078450
			public void Add(JToken item)
			{
				this._token = item;
			}

			// Token: 0x0600189C RID: 6300 RVA: 0x0007A25C File Offset: 0x0007845C
			public void Clear()
			{
				this._token = null;
			}

			// Token: 0x0600189D RID: 6301 RVA: 0x0007A268 File Offset: 0x00078468
			public bool Contains(JToken item)
			{
				return this._token == item;
			}

			// Token: 0x0600189E RID: 6302 RVA: 0x0007A274 File Offset: 0x00078474
			public void CopyTo(JToken[] array, int arrayIndex)
			{
				if (this._token != null)
				{
					array[arrayIndex] = this._token;
				}
			}

			// Token: 0x0600189F RID: 6303 RVA: 0x0007A290 File Offset: 0x00078490
			public bool Remove(JToken item)
			{
				if (this._token == item)
				{
					this._token = null;
					return true;
				}
				return false;
			}

			// Token: 0x170004E5 RID: 1253
			// (get) Token: 0x060018A0 RID: 6304 RVA: 0x0007A2A8 File Offset: 0x000784A8
			public int Count
			{
				get
				{
					if (this._token == null)
					{
						return 0;
					}
					return 1;
				}
			}

			// Token: 0x170004E6 RID: 1254
			// (get) Token: 0x060018A1 RID: 6305 RVA: 0x0007A2B8 File Offset: 0x000784B8
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x060018A2 RID: 6306 RVA: 0x0007A2BC File Offset: 0x000784BC
			public int IndexOf(JToken item)
			{
				if (this._token != item)
				{
					return -1;
				}
				return 0;
			}

			// Token: 0x060018A3 RID: 6307 RVA: 0x0007A2D0 File Offset: 0x000784D0
			public void Insert(int index, JToken item)
			{
				if (index == 0)
				{
					this._token = item;
				}
			}

			// Token: 0x060018A4 RID: 6308 RVA: 0x0007A2E0 File Offset: 0x000784E0
			public void RemoveAt(int index)
			{
				if (index == 0)
				{
					this._token = null;
				}
			}

			// Token: 0x170004E7 RID: 1255
			public JToken this[int index]
			{
				get
				{
					if (index != 0)
					{
						throw new IndexOutOfRangeException();
					}
					return this._token;
				}
				set
				{
					if (index != 0)
					{
						throw new IndexOutOfRangeException();
					}
					this._token = value;
				}
			}

			// Token: 0x04000C9E RID: 3230
			[Nullable(2)]
			internal JToken _token;
		}
	}
}
