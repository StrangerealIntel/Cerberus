using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Threading;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020001D6 RID: 470
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class JContainer : JToken, IList<JToken>, ICollection<JToken>, IEnumerable<JToken>, IEnumerable, ITypedList, IBindingList, IList, ICollection, INotifyCollectionChanged
	{
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06001238 RID: 4664 RVA: 0x00063DD8 File Offset: 0x00061FD8
		// (remove) Token: 0x06001239 RID: 4665 RVA: 0x00063DF4 File Offset: 0x00061FF4
		public event ListChangedEventHandler ListChanged
		{
			add
			{
				this._listChanged = (ListChangedEventHandler)Delegate.Combine(this._listChanged, value);
			}
			remove
			{
				this._listChanged = (ListChangedEventHandler)Delegate.Remove(this._listChanged, value);
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600123A RID: 4666 RVA: 0x00063E10 File Offset: 0x00062010
		// (remove) Token: 0x0600123B RID: 4667 RVA: 0x00063E2C File Offset: 0x0006202C
		public event AddingNewEventHandler AddingNew
		{
			add
			{
				this._addingNew = (AddingNewEventHandler)Delegate.Combine(this._addingNew, value);
			}
			remove
			{
				this._addingNew = (AddingNewEventHandler)Delegate.Remove(this._addingNew, value);
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x0600123C RID: 4668 RVA: 0x00063E48 File Offset: 0x00062048
		// (remove) Token: 0x0600123D RID: 4669 RVA: 0x00063E64 File Offset: 0x00062064
		public event NotifyCollectionChangedEventHandler CollectionChanged
		{
			add
			{
				this._collectionChanged = (NotifyCollectionChangedEventHandler)Delegate.Combine(this._collectionChanged, value);
			}
			remove
			{
				this._collectionChanged = (NotifyCollectionChangedEventHandler)Delegate.Remove(this._collectionChanged, value);
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x0600123E RID: 4670
		protected abstract IList<JToken> ChildrenTokens { get; }

		// Token: 0x0600123F RID: 4671 RVA: 0x00063E80 File Offset: 0x00062080
		internal JContainer()
		{
		}

		// Token: 0x06001240 RID: 4672 RVA: 0x00063E88 File Offset: 0x00062088
		internal JContainer(JContainer other) : this()
		{
			ValidationUtils.ArgumentNotNull(other, "other");
			int num = 0;
			foreach (JToken content in ((IEnumerable<JToken>)other))
			{
				this.AddInternal(num, content, false);
				num++;
			}
		}

		// Token: 0x06001241 RID: 4673 RVA: 0x00063EF4 File Offset: 0x000620F4
		internal void CheckReentrancy()
		{
			if (this._busy)
			{
				throw new InvalidOperationException("Cannot change {0} during a collection change event.".FormatWith(CultureInfo.InvariantCulture, base.GetType()));
			}
		}

		// Token: 0x06001242 RID: 4674 RVA: 0x00063F1C File Offset: 0x0006211C
		internal virtual IList<JToken> CreateChildrenCollection()
		{
			return new List<JToken>();
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x00063F24 File Offset: 0x00062124
		protected virtual void OnAddingNew(AddingNewEventArgs e)
		{
			AddingNewEventHandler addingNew = this._addingNew;
			if (addingNew == null)
			{
				return;
			}
			addingNew(this, e);
		}

		// Token: 0x06001244 RID: 4676 RVA: 0x00063F3C File Offset: 0x0006213C
		protected virtual void OnListChanged(ListChangedEventArgs e)
		{
			ListChangedEventHandler listChanged = this._listChanged;
			if (listChanged != null)
			{
				this._busy = true;
				try
				{
					listChanged(this, e);
				}
				finally
				{
					this._busy = false;
				}
			}
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x00063F84 File Offset: 0x00062184
		protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			NotifyCollectionChangedEventHandler collectionChanged = this._collectionChanged;
			if (collectionChanged != null)
			{
				this._busy = true;
				try
				{
					collectionChanged(this, e);
				}
				finally
				{
					this._busy = false;
				}
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001246 RID: 4678 RVA: 0x00063FCC File Offset: 0x000621CC
		public override bool HasValues
		{
			get
			{
				return this.ChildrenTokens.Count > 0;
			}
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00063FDC File Offset: 0x000621DC
		internal bool ContentsEqual(JContainer container)
		{
			if (container == this)
			{
				return true;
			}
			IList<JToken> childrenTokens = this.ChildrenTokens;
			IList<JToken> childrenTokens2 = container.ChildrenTokens;
			if (childrenTokens.Count != childrenTokens2.Count)
			{
				return false;
			}
			for (int i = 0; i < childrenTokens.Count; i++)
			{
				if (!childrenTokens[i].DeepEquals(childrenTokens2[i]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001248 RID: 4680 RVA: 0x00064048 File Offset: 0x00062248
		[Nullable(2)]
		public override JToken First
		{
			[NullableContext(2)]
			get
			{
				IList<JToken> childrenTokens = this.ChildrenTokens;
				if (childrenTokens.Count <= 0)
				{
					return null;
				}
				return childrenTokens[0];
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001249 RID: 4681 RVA: 0x00064078 File Offset: 0x00062278
		[Nullable(2)]
		public override JToken Last
		{
			[NullableContext(2)]
			get
			{
				IList<JToken> childrenTokens = this.ChildrenTokens;
				int count = childrenTokens.Count;
				if (count <= 0)
				{
					return null;
				}
				return childrenTokens[count - 1];
			}
		}

		// Token: 0x0600124A RID: 4682 RVA: 0x000640AC File Offset: 0x000622AC
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public override JEnumerable<JToken> Children()
		{
			return new JEnumerable<JToken>(this.ChildrenTokens);
		}

		// Token: 0x0600124B RID: 4683 RVA: 0x000640BC File Offset: 0x000622BC
		public override IEnumerable<T> Values<[Nullable(2)] T>()
		{
			return this.ChildrenTokens.Convert<JToken, T>();
		}

		// Token: 0x0600124C RID: 4684 RVA: 0x000640CC File Offset: 0x000622CC
		public IEnumerable<JToken> Descendants()
		{
			return this.GetDescendants(false);
		}

		// Token: 0x0600124D RID: 4685 RVA: 0x000640D8 File Offset: 0x000622D8
		public IEnumerable<JToken> DescendantsAndSelf()
		{
			return this.GetDescendants(true);
		}

		// Token: 0x0600124E RID: 4686 RVA: 0x000640E4 File Offset: 0x000622E4
		internal IEnumerable<JToken> GetDescendants(bool self)
		{
			if (self)
			{
				yield return this;
			}
			foreach (JToken o in this.ChildrenTokens)
			{
				yield return o;
				JContainer jcontainer = o as JContainer;
				if (jcontainer != null)
				{
					foreach (JToken jtoken in jcontainer.Descendants())
					{
						yield return jtoken;
					}
					IEnumerator<JToken> enumerator2 = null;
				}
				o = null;
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600124F RID: 4687 RVA: 0x000640FC File Offset: 0x000622FC
		[NullableContext(2)]
		internal bool IsMultiContent([NotNull] object content)
		{
			return content is IEnumerable && !(content is string) && !(content is JToken) && !(content is byte[]);
		}

		// Token: 0x06001250 RID: 4688 RVA: 0x00064130 File Offset: 0x00062330
		internal JToken EnsureParentToken([Nullable(2)] JToken item, bool skipParentCheck)
		{
			if (item == null)
			{
				return JValue.CreateNull();
			}
			if (skipParentCheck)
			{
				return item;
			}
			if (item.Parent != null || item == this || (item.HasValues && base.Root == item))
			{
				item = item.CloneToken();
			}
			return item;
		}

		// Token: 0x06001251 RID: 4689
		[NullableContext(2)]
		internal abstract int IndexOfItem(JToken item);

		// Token: 0x06001252 RID: 4690 RVA: 0x00064188 File Offset: 0x00062388
		[NullableContext(2)]
		internal virtual void InsertItem(int index, JToken item, bool skipParentCheck)
		{
			IList<JToken> childrenTokens = this.ChildrenTokens;
			if (index > childrenTokens.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index must be within the bounds of the List.");
			}
			this.CheckReentrancy();
			item = this.EnsureParentToken(item, skipParentCheck);
			JToken jtoken = (index == 0) ? null : childrenTokens[index - 1];
			JToken jtoken2 = (index == childrenTokens.Count) ? null : childrenTokens[index];
			this.ValidateToken(item, null);
			item.Parent = this;
			item.Previous = jtoken;
			if (jtoken != null)
			{
				jtoken.Next = item;
			}
			item.Next = jtoken2;
			if (jtoken2 != null)
			{
				jtoken2.Previous = item;
			}
			childrenTokens.Insert(index, item);
			if (this._listChanged != null)
			{
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, index));
			}
			if (this._collectionChanged != null)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
			}
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x0006426C File Offset: 0x0006246C
		internal virtual void RemoveItemAt(int index)
		{
			IList<JToken> childrenTokens = this.ChildrenTokens;
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Index is less than 0.");
			}
			if (index >= childrenTokens.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index is equal to or greater than Count.");
			}
			this.CheckReentrancy();
			JToken jtoken = childrenTokens[index];
			JToken jtoken2 = (index == 0) ? null : childrenTokens[index - 1];
			JToken jtoken3 = (index == childrenTokens.Count - 1) ? null : childrenTokens[index + 1];
			if (jtoken2 != null)
			{
				jtoken2.Next = jtoken3;
			}
			if (jtoken3 != null)
			{
				jtoken3.Previous = jtoken2;
			}
			jtoken.Parent = null;
			jtoken.Previous = null;
			jtoken.Next = null;
			childrenTokens.RemoveAt(index);
			if (this._listChanged != null)
			{
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
			}
			if (this._collectionChanged != null)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, jtoken, index));
			}
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x00064360 File Offset: 0x00062560
		[NullableContext(2)]
		internal virtual bool RemoveItem(JToken item)
		{
			if (item != null)
			{
				int num = this.IndexOfItem(item);
				if (num >= 0)
				{
					this.RemoveItemAt(num);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x00064390 File Offset: 0x00062590
		internal virtual JToken GetItem(int index)
		{
			return this.ChildrenTokens[index];
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x000643A0 File Offset: 0x000625A0
		[NullableContext(2)]
		internal virtual void SetItem(int index, JToken item)
		{
			IList<JToken> childrenTokens = this.ChildrenTokens;
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "Index is less than 0.");
			}
			if (index >= childrenTokens.Count)
			{
				throw new ArgumentOutOfRangeException("index", "Index is equal to or greater than Count.");
			}
			JToken jtoken = childrenTokens[index];
			if (JContainer.IsTokenUnchanged(jtoken, item))
			{
				return;
			}
			this.CheckReentrancy();
			item = this.EnsureParentToken(item, false);
			this.ValidateToken(item, jtoken);
			JToken jtoken2 = (index == 0) ? null : childrenTokens[index - 1];
			JToken jtoken3 = (index == childrenTokens.Count - 1) ? null : childrenTokens[index + 1];
			item.Parent = this;
			item.Previous = jtoken2;
			if (jtoken2 != null)
			{
				jtoken2.Next = item;
			}
			item.Next = jtoken3;
			if (jtoken3 != null)
			{
				jtoken3.Previous = item;
			}
			childrenTokens[index] = item;
			jtoken.Parent = null;
			jtoken.Previous = null;
			jtoken.Next = null;
			if (this._listChanged != null)
			{
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, index));
			}
			if (this._collectionChanged != null)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, item, jtoken, index));
			}
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x000644CC File Offset: 0x000626CC
		internal virtual void ClearItems()
		{
			this.CheckReentrancy();
			IList<JToken> childrenTokens = this.ChildrenTokens;
			foreach (JToken jtoken in childrenTokens)
			{
				jtoken.Parent = null;
				jtoken.Previous = null;
				jtoken.Next = null;
			}
			childrenTokens.Clear();
			if (this._listChanged != null)
			{
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
			}
			if (this._collectionChanged != null)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
			}
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x0006456C File Offset: 0x0006276C
		internal virtual void ReplaceItem(JToken existing, JToken replacement)
		{
			if (existing == null || existing.Parent != this)
			{
				return;
			}
			int index = this.IndexOfItem(existing);
			this.SetItem(index, replacement);
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x000645A0 File Offset: 0x000627A0
		[NullableContext(2)]
		internal virtual bool ContainsItem(JToken item)
		{
			return this.IndexOfItem(item) != -1;
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x000645B0 File Offset: 0x000627B0
		internal virtual void CopyItemsTo(Array array, int arrayIndex)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			if (arrayIndex < 0)
			{
				throw new ArgumentOutOfRangeException("arrayIndex", "arrayIndex is less than 0.");
			}
			if (arrayIndex >= array.Length && arrayIndex != 0)
			{
				throw new ArgumentException("arrayIndex is equal to or greater than the length of array.");
			}
			if (this.Count > array.Length - arrayIndex)
			{
				throw new ArgumentException("The number of elements in the source JObject is greater than the available space from arrayIndex to the end of the destination array.");
			}
			int num = 0;
			foreach (JToken value in this.ChildrenTokens)
			{
				array.SetValue(value, arrayIndex + num);
				num++;
			}
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x00064674 File Offset: 0x00062874
		internal static bool IsTokenUnchanged(JToken currentValue, [Nullable(2)] JToken newValue)
		{
			JValue jvalue = currentValue as JValue;
			if (jvalue == null)
			{
				return false;
			}
			if (newValue == null)
			{
				return jvalue.Type == JTokenType.Null;
			}
			return jvalue.Equals(newValue);
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x000646AC File Offset: 0x000628AC
		internal virtual void ValidateToken(JToken o, [Nullable(2)] JToken existing)
		{
			ValidationUtils.ArgumentNotNull(o, "o");
			if (o.Type == JTokenType.Property)
			{
				throw new ArgumentException("Can not add {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, o.GetType(), base.GetType()));
			}
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x000646F8 File Offset: 0x000628F8
		[NullableContext(2)]
		public virtual void Add(object content)
		{
			this.AddInternal(this.ChildrenTokens.Count, content, false);
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x0006471C File Offset: 0x0006291C
		internal void AddAndSkipParentCheck(JToken token)
		{
			this.AddInternal(this.ChildrenTokens.Count, token, true);
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x00064740 File Offset: 0x00062940
		[NullableContext(2)]
		public void AddFirst(object content)
		{
			this.AddInternal(0, content, false);
		}

		// Token: 0x06001260 RID: 4704 RVA: 0x0006474C File Offset: 0x0006294C
		[NullableContext(2)]
		internal void AddInternal(int index, object content, bool skipParentCheck)
		{
			if (this.IsMultiContent(content))
			{
				IEnumerable enumerable = (IEnumerable)content;
				int num = index;
				using (IEnumerator enumerator = enumerable.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object content2 = enumerator.Current;
						this.AddInternal(num, content2, skipParentCheck);
						num++;
					}
					return;
				}
			}
			JToken item = JContainer.CreateFromContent(content);
			this.InsertItem(index, item, skipParentCheck);
		}

		// Token: 0x06001261 RID: 4705 RVA: 0x000647D0 File Offset: 0x000629D0
		internal static JToken CreateFromContent([Nullable(2)] object content)
		{
			JToken jtoken = content as JToken;
			if (jtoken != null)
			{
				return jtoken;
			}
			return new JValue(content);
		}

		// Token: 0x06001262 RID: 4706 RVA: 0x000647F8 File Offset: 0x000629F8
		public JsonWriter CreateWriter()
		{
			return new JTokenWriter(this);
		}

		// Token: 0x06001263 RID: 4707 RVA: 0x00064800 File Offset: 0x00062A00
		public void ReplaceAll(object content)
		{
			this.ClearItems();
			this.Add(content);
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00064810 File Offset: 0x00062A10
		public void RemoveAll()
		{
			this.ClearItems();
		}

		// Token: 0x06001265 RID: 4709
		internal abstract void MergeItem(object content, [Nullable(2)] JsonMergeSettings settings);

		// Token: 0x06001266 RID: 4710 RVA: 0x00064818 File Offset: 0x00062A18
		public void Merge(object content)
		{
			this.MergeItem(content, null);
		}

		// Token: 0x06001267 RID: 4711 RVA: 0x00064824 File Offset: 0x00062A24
		public void Merge(object content, [Nullable(2)] JsonMergeSettings settings)
		{
			this.MergeItem(content, settings);
		}

		// Token: 0x06001268 RID: 4712 RVA: 0x00064830 File Offset: 0x00062A30
		internal void ReadTokenFrom(JsonReader reader, [Nullable(2)] JsonLoadSettings options)
		{
			int depth = reader.Depth;
			if (!reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading {0} from JsonReader.".FormatWith(CultureInfo.InvariantCulture, base.GetType().Name));
			}
			this.ReadContentFrom(reader, options);
			if (reader.Depth > depth)
			{
				throw JsonReaderException.Create(reader, "Unexpected end of content while loading {0}.".FormatWith(CultureInfo.InvariantCulture, base.GetType().Name));
			}
		}

		// Token: 0x06001269 RID: 4713 RVA: 0x000648AC File Offset: 0x00062AAC
		internal void ReadContentFrom(JsonReader r, [Nullable(2)] JsonLoadSettings settings)
		{
			ValidationUtils.ArgumentNotNull(r, "r");
			IJsonLineInfo lineInfo = r as IJsonLineInfo;
			JContainer jcontainer = this;
			for (;;)
			{
				JProperty jproperty = jcontainer as JProperty;
				if (jproperty != null && jproperty.Value != null)
				{
					if (jcontainer == this)
					{
						break;
					}
					jcontainer = jcontainer.Parent;
				}
				switch (r.TokenType)
				{
				case JsonToken.None:
					goto IL_218;
				case JsonToken.StartObject:
				{
					JObject jobject = new JObject();
					jobject.SetLineInfo(lineInfo, settings);
					jcontainer.Add(jobject);
					jcontainer = jobject;
					goto IL_218;
				}
				case JsonToken.StartArray:
				{
					JArray jarray = new JArray();
					jarray.SetLineInfo(lineInfo, settings);
					jcontainer.Add(jarray);
					jcontainer = jarray;
					goto IL_218;
				}
				case JsonToken.StartConstructor:
				{
					JConstructor jconstructor = new JConstructor(r.Value.ToString());
					jconstructor.SetLineInfo(lineInfo, settings);
					jcontainer.Add(jconstructor);
					jcontainer = jconstructor;
					goto IL_218;
				}
				case JsonToken.PropertyName:
				{
					JProperty jproperty2 = JContainer.ReadProperty(r, settings, lineInfo, jcontainer);
					if (jproperty2 != null)
					{
						jcontainer = jproperty2;
						goto IL_218;
					}
					r.Skip();
					goto IL_218;
				}
				case JsonToken.Comment:
					if (settings != null && settings.CommentHandling == CommentHandling.Load)
					{
						JValue jvalue = JValue.CreateComment(r.Value.ToString());
						jvalue.SetLineInfo(lineInfo, settings);
						jcontainer.Add(jvalue);
						goto IL_218;
					}
					goto IL_218;
				case JsonToken.Integer:
				case JsonToken.Float:
				case JsonToken.String:
				case JsonToken.Boolean:
				case JsonToken.Date:
				case JsonToken.Bytes:
				{
					JValue jvalue = new JValue(r.Value);
					jvalue.SetLineInfo(lineInfo, settings);
					jcontainer.Add(jvalue);
					goto IL_218;
				}
				case JsonToken.Null:
				{
					JValue jvalue = JValue.CreateNull();
					jvalue.SetLineInfo(lineInfo, settings);
					jcontainer.Add(jvalue);
					goto IL_218;
				}
				case JsonToken.Undefined:
				{
					JValue jvalue = JValue.CreateUndefined();
					jvalue.SetLineInfo(lineInfo, settings);
					jcontainer.Add(jvalue);
					goto IL_218;
				}
				case JsonToken.EndObject:
					if (jcontainer == this)
					{
						return;
					}
					jcontainer = jcontainer.Parent;
					goto IL_218;
				case JsonToken.EndArray:
					if (jcontainer == this)
					{
						return;
					}
					jcontainer = jcontainer.Parent;
					goto IL_218;
				case JsonToken.EndConstructor:
					if (jcontainer == this)
					{
						return;
					}
					jcontainer = jcontainer.Parent;
					goto IL_218;
				}
				goto Block_4;
				IL_218:
				if (!r.Read())
				{
					return;
				}
			}
			return;
			Block_4:
			throw new InvalidOperationException("The JsonReader should not be on a token of type {0}.".FormatWith(CultureInfo.InvariantCulture, r.TokenType));
		}

		// Token: 0x0600126A RID: 4714 RVA: 0x00064AE0 File Offset: 0x00062CE0
		[NullableContext(2)]
		private static JProperty ReadProperty([Nullable(1)] JsonReader r, JsonLoadSettings settings, IJsonLineInfo lineInfo, [Nullable(1)] JContainer parent)
		{
			DuplicatePropertyNameHandling duplicatePropertyNameHandling = (settings != null) ? settings.DuplicatePropertyNameHandling : DuplicatePropertyNameHandling.Replace;
			JObject jobject = (JObject)parent;
			string text = r.Value.ToString();
			JProperty jproperty = jobject.Property(text, StringComparison.Ordinal);
			if (jproperty != null)
			{
				if (duplicatePropertyNameHandling == DuplicatePropertyNameHandling.Ignore)
				{
					return null;
				}
				if (duplicatePropertyNameHandling == DuplicatePropertyNameHandling.Error)
				{
					throw JsonReaderException.Create(r, "Property with the name '{0}' already exists in the current JSON object.".FormatWith(CultureInfo.InvariantCulture, text));
				}
			}
			JProperty jproperty2 = new JProperty(text);
			jproperty2.SetLineInfo(lineInfo, settings);
			if (jproperty == null)
			{
				parent.Add(jproperty2);
			}
			else
			{
				jproperty.Replace(jproperty2);
			}
			return jproperty2;
		}

		// Token: 0x0600126B RID: 4715 RVA: 0x00064B74 File Offset: 0x00062D74
		internal int ContentsHashCode()
		{
			int num = 0;
			foreach (JToken jtoken in this.ChildrenTokens)
			{
				num ^= jtoken.GetDeepHashCode();
			}
			return num;
		}

		// Token: 0x0600126C RID: 4716 RVA: 0x00064BD0 File Offset: 0x00062DD0
		string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
		{
			return string.Empty;
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x00064BD8 File Offset: 0x00062DD8
		[return: Nullable(2)]
		PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
		{
			ICustomTypeDescriptor customTypeDescriptor = this.First as ICustomTypeDescriptor;
			if (customTypeDescriptor == null)
			{
				return null;
			}
			return customTypeDescriptor.GetProperties();
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x00064BF4 File Offset: 0x00062DF4
		int IList<JToken>.IndexOf(JToken item)
		{
			return this.IndexOfItem(item);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x00064C00 File Offset: 0x00062E00
		void IList<JToken>.Insert(int index, JToken item)
		{
			this.InsertItem(index, item, false);
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x00064C0C File Offset: 0x00062E0C
		void IList<JToken>.RemoveAt(int index)
		{
			this.RemoveItemAt(index);
		}

		// Token: 0x170003F1 RID: 1009
		JToken IList<JToken>.this[int index]
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

		// Token: 0x06001273 RID: 4723 RVA: 0x00064C30 File Offset: 0x00062E30
		void ICollection<JToken>.Add(JToken item)
		{
			this.Add(item);
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x00064C3C File Offset: 0x00062E3C
		void ICollection<JToken>.Clear()
		{
			this.ClearItems();
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x00064C44 File Offset: 0x00062E44
		bool ICollection<JToken>.Contains(JToken item)
		{
			return this.ContainsItem(item);
		}

		// Token: 0x06001276 RID: 4726 RVA: 0x00064C50 File Offset: 0x00062E50
		void ICollection<JToken>.CopyTo(JToken[] array, int arrayIndex)
		{
			this.CopyItemsTo(array, arrayIndex);
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001277 RID: 4727 RVA: 0x00064C5C File Offset: 0x00062E5C
		bool ICollection<JToken>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x00064C60 File Offset: 0x00062E60
		bool ICollection<JToken>.Remove(JToken item)
		{
			return this.RemoveItem(item);
		}

		// Token: 0x06001279 RID: 4729 RVA: 0x00064C6C File Offset: 0x00062E6C
		[return: Nullable(2)]
		private JToken EnsureValue(object value)
		{
			if (value == null)
			{
				return null;
			}
			JToken jtoken = value as JToken;
			if (jtoken != null)
			{
				return jtoken;
			}
			throw new ArgumentException("Argument is not a JToken.");
		}

		// Token: 0x0600127A RID: 4730 RVA: 0x00064CA0 File Offset: 0x00062EA0
		int IList.Add(object value)
		{
			this.Add(this.EnsureValue(value));
			return this.Count - 1;
		}

		// Token: 0x0600127B RID: 4731 RVA: 0x00064CB8 File Offset: 0x00062EB8
		void IList.Clear()
		{
			this.ClearItems();
		}

		// Token: 0x0600127C RID: 4732 RVA: 0x00064CC0 File Offset: 0x00062EC0
		bool IList.Contains(object value)
		{
			return this.ContainsItem(this.EnsureValue(value));
		}

		// Token: 0x0600127D RID: 4733 RVA: 0x00064CD0 File Offset: 0x00062ED0
		int IList.IndexOf(object value)
		{
			return this.IndexOfItem(this.EnsureValue(value));
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x00064CE0 File Offset: 0x00062EE0
		void IList.Insert(int index, object value)
		{
			this.InsertItem(index, this.EnsureValue(value), false);
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x0600127F RID: 4735 RVA: 0x00064CF4 File Offset: 0x00062EF4
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001280 RID: 4736 RVA: 0x00064CF8 File Offset: 0x00062EF8
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001281 RID: 4737 RVA: 0x00064CFC File Offset: 0x00062EFC
		void IList.Remove(object value)
		{
			this.RemoveItem(this.EnsureValue(value));
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x00064D0C File Offset: 0x00062F0C
		void IList.RemoveAt(int index)
		{
			this.RemoveItemAt(index);
		}

		// Token: 0x170003F5 RID: 1013
		object IList.this[int index]
		{
			get
			{
				return this.GetItem(index);
			}
			set
			{
				this.SetItem(index, this.EnsureValue(value));
			}
		}

		// Token: 0x06001285 RID: 4741 RVA: 0x00064D34 File Offset: 0x00062F34
		void ICollection.CopyTo(Array array, int index)
		{
			this.CopyItemsTo(array, index);
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x00064D40 File Offset: 0x00062F40
		public int Count
		{
			get
			{
				return this.ChildrenTokens.Count;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001287 RID: 4743 RVA: 0x00064D50 File Offset: 0x00062F50
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001288 RID: 4744 RVA: 0x00064D54 File Offset: 0x00062F54
		object ICollection.SyncRoot
		{
			get
			{
				if (this._syncRoot == null)
				{
					Interlocked.CompareExchange(ref this._syncRoot, new object(), null);
				}
				return this._syncRoot;
			}
		}

		// Token: 0x06001289 RID: 4745 RVA: 0x00064D7C File Offset: 0x00062F7C
		void IBindingList.AddIndex(PropertyDescriptor property)
		{
		}

		// Token: 0x0600128A RID: 4746 RVA: 0x00064D80 File Offset: 0x00062F80
		object IBindingList.AddNew()
		{
			AddingNewEventArgs addingNewEventArgs = new AddingNewEventArgs();
			this.OnAddingNew(addingNewEventArgs);
			if (addingNewEventArgs.NewObject == null)
			{
				throw new JsonException("Could not determine new value to add to '{0}'.".FormatWith(CultureInfo.InvariantCulture, base.GetType()));
			}
			JToken jtoken = addingNewEventArgs.NewObject as JToken;
			if (jtoken == null)
			{
				throw new JsonException("New item to be added to collection must be compatible with {0}.".FormatWith(CultureInfo.InvariantCulture, typeof(JToken)));
			}
			this.Add(jtoken);
			return jtoken;
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x0600128B RID: 4747 RVA: 0x00064E00 File Offset: 0x00063000
		bool IBindingList.AllowEdit
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x0600128C RID: 4748 RVA: 0x00064E04 File Offset: 0x00063004
		bool IBindingList.AllowNew
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x0600128D RID: 4749 RVA: 0x00064E08 File Offset: 0x00063008
		bool IBindingList.AllowRemove
		{
			get
			{
				return true;
			}
		}

		// Token: 0x0600128E RID: 4750 RVA: 0x00064E0C File Offset: 0x0006300C
		void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600128F RID: 4751 RVA: 0x00064E14 File Offset: 0x00063014
		int IBindingList.Find(PropertyDescriptor property, object key)
		{
			throw new NotSupportedException();
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001290 RID: 4752 RVA: 0x00064E1C File Offset: 0x0006301C
		bool IBindingList.IsSorted
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00064E20 File Offset: 0x00063020
		void IBindingList.RemoveIndex(PropertyDescriptor property)
		{
		}

		// Token: 0x06001292 RID: 4754 RVA: 0x00064E24 File Offset: 0x00063024
		void IBindingList.RemoveSort()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001293 RID: 4755 RVA: 0x00064E2C File Offset: 0x0006302C
		ListSortDirection IBindingList.SortDirection
		{
			get
			{
				return ListSortDirection.Ascending;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x00064E30 File Offset: 0x00063030
		[Nullable(2)]
		PropertyDescriptor IBindingList.SortProperty
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06001295 RID: 4757 RVA: 0x00064E34 File Offset: 0x00063034
		bool IBindingList.SupportsChangeNotification
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x00064E38 File Offset: 0x00063038
		bool IBindingList.SupportsSearching
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06001297 RID: 4759 RVA: 0x00064E3C File Offset: 0x0006303C
		bool IBindingList.SupportsSorting
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001298 RID: 4760 RVA: 0x00064E40 File Offset: 0x00063040
		internal static void MergeEnumerableContent(JContainer target, IEnumerable content, [Nullable(2)] JsonMergeSettings settings)
		{
			switch ((settings != null) ? settings.MergeArrayHandling : MergeArrayHandling.Concat)
			{
			case MergeArrayHandling.Concat:
				using (IEnumerator enumerator = content.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						JToken content2 = (JToken)obj;
						target.Add(content2);
					}
					return;
				}
				break;
			case MergeArrayHandling.Union:
				break;
			case MergeArrayHandling.Replace:
				goto IL_CB;
			case MergeArrayHandling.Merge:
				goto IL_11A;
			default:
				goto IL_1CC;
			}
			HashSet<JToken> hashSet = new HashSet<JToken>(target, JToken.EqualityComparer);
			using (IEnumerator enumerator = content.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object obj2 = enumerator.Current;
					JToken jtoken = (JToken)obj2;
					if (hashSet.Add(jtoken))
					{
						target.Add(jtoken);
					}
				}
				return;
			}
			IL_CB:
			if (target == content)
			{
				return;
			}
			target.ClearItems();
			using (IEnumerator enumerator = content.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object obj3 = enumerator.Current;
					JToken content3 = (JToken)obj3;
					target.Add(content3);
				}
				return;
			}
			IL_11A:
			int num = 0;
			using (IEnumerator enumerator = content.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					object obj4 = enumerator.Current;
					if (num < target.Count)
					{
						JContainer jcontainer = target[num] as JContainer;
						if (jcontainer != null)
						{
							jcontainer.Merge(obj4, settings);
						}
						else if (obj4 != null)
						{
							JToken jtoken2 = JContainer.CreateFromContent(obj4);
							if (jtoken2.Type != JTokenType.Null)
							{
								target[num] = jtoken2;
							}
						}
					}
					else
					{
						target.Add(obj4);
					}
					num++;
				}
				return;
			}
			IL_1CC:
			throw new ArgumentOutOfRangeException("settings", "Unexpected merge array handling when merging JSON.");
		}

		// Token: 0x040008D4 RID: 2260
		[Nullable(2)]
		internal ListChangedEventHandler _listChanged;

		// Token: 0x040008D5 RID: 2261
		[Nullable(2)]
		internal AddingNewEventHandler _addingNew;

		// Token: 0x040008D6 RID: 2262
		[Nullable(2)]
		internal NotifyCollectionChangedEventHandler _collectionChanged;

		// Token: 0x040008D7 RID: 2263
		[Nullable(2)]
		private object _syncRoot;

		// Token: 0x040008D8 RID: 2264
		private bool _busy;
	}
}
