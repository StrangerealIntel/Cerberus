using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020001D8 RID: 472
	[NullableContext(1)]
	[Nullable(0)]
	public class JObject : JContainer, IDictionary<string, JToken>, ICollection<KeyValuePair<string, JToken>>, IEnumerable<KeyValuePair<string, JToken>>, IEnumerable, INotifyPropertyChanged, ICustomTypeDescriptor, INotifyPropertyChanging
	{
		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x060012A1 RID: 4769 RVA: 0x0006513C File Offset: 0x0006333C
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._properties;
			}
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x060012A2 RID: 4770 RVA: 0x00065144 File Offset: 0x00063344
		// (remove) Token: 0x060012A3 RID: 4771 RVA: 0x00065180 File Offset: 0x00063380
		[Nullable(2)]
		[method: NullableContext(2)]
		[Nullable(2)]
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x1400000F RID: 15
		// (add) Token: 0x060012A4 RID: 4772 RVA: 0x000651BC File Offset: 0x000633BC
		// (remove) Token: 0x060012A5 RID: 4773 RVA: 0x000651F8 File Offset: 0x000633F8
		[Nullable(2)]
		[method: NullableContext(2)]
		[Nullable(2)]
		public event PropertyChangingEventHandler PropertyChanging;

		// Token: 0x060012A6 RID: 4774 RVA: 0x00065234 File Offset: 0x00063434
		public JObject()
		{
		}

		// Token: 0x060012A7 RID: 4775 RVA: 0x00065248 File Offset: 0x00063448
		public JObject(JObject other) : base(other)
		{
		}

		// Token: 0x060012A8 RID: 4776 RVA: 0x0006525C File Offset: 0x0006345C
		public JObject(params object[] content) : this(content)
		{
		}

		// Token: 0x060012A9 RID: 4777 RVA: 0x00065268 File Offset: 0x00063468
		public JObject(object content)
		{
			this.Add(content);
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x00065284 File Offset: 0x00063484
		internal override bool DeepEquals(JToken node)
		{
			JObject jobject = node as JObject;
			return jobject != null && this._properties.Compare(jobject._properties);
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x000652B8 File Offset: 0x000634B8
		[NullableContext(2)]
		internal override int IndexOfItem(JToken item)
		{
			if (item == null)
			{
				return -1;
			}
			return this._properties.IndexOfReference(item);
		}

		// Token: 0x060012AC RID: 4780 RVA: 0x000652D0 File Offset: 0x000634D0
		[NullableContext(2)]
		internal override void InsertItem(int index, JToken item, bool skipParentCheck)
		{
			if (item != null && item.Type == JTokenType.Comment)
			{
				return;
			}
			base.InsertItem(index, item, skipParentCheck);
		}

		// Token: 0x060012AD RID: 4781 RVA: 0x000652F0 File Offset: 0x000634F0
		internal override void ValidateToken(JToken o, [Nullable(2)] JToken existing)
		{
			ValidationUtils.ArgumentNotNull(o, "o");
			if (o.Type != JTokenType.Property)
			{
				throw new ArgumentException("Can not add {0} to {1}.".FormatWith(CultureInfo.InvariantCulture, o.GetType(), base.GetType()));
			}
			JProperty jproperty = (JProperty)o;
			if (existing != null)
			{
				JProperty jproperty2 = (JProperty)existing;
				if (jproperty.Name == jproperty2.Name)
				{
					return;
				}
			}
			if (this._properties.TryGetValue(jproperty.Name, out existing))
			{
				throw new ArgumentException("Can not add property {0} to {1}. Property with the same name already exists on object.".FormatWith(CultureInfo.InvariantCulture, jproperty.Name, base.GetType()));
			}
		}

		// Token: 0x060012AE RID: 4782 RVA: 0x000653A0 File Offset: 0x000635A0
		internal override void MergeItem(object content, [Nullable(2)] JsonMergeSettings settings)
		{
			JObject jobject = content as JObject;
			if (jobject == null)
			{
				return;
			}
			foreach (KeyValuePair<string, JToken> keyValuePair in jobject)
			{
				JProperty jproperty = this.Property(keyValuePair.Key, (settings != null) ? settings.PropertyNameComparison : StringComparison.Ordinal);
				if (jproperty == null)
				{
					this.Add(keyValuePair.Key, keyValuePair.Value);
				}
				else if (keyValuePair.Value != null)
				{
					JContainer jcontainer = jproperty.Value as JContainer;
					if (jcontainer == null || jcontainer.Type != keyValuePair.Value.Type)
					{
						if (!JObject.IsNull(keyValuePair.Value) || (settings != null && settings.MergeNullValueHandling == MergeNullValueHandling.Merge))
						{
							jproperty.Value = keyValuePair.Value;
						}
					}
					else
					{
						jcontainer.Merge(keyValuePair.Value, settings);
					}
				}
			}
		}

		// Token: 0x060012AF RID: 4783 RVA: 0x000654B8 File Offset: 0x000636B8
		private static bool IsNull(JToken token)
		{
			if (token.Type == JTokenType.Null)
			{
				return true;
			}
			JValue jvalue = token as JValue;
			return jvalue != null && jvalue.Value == null;
		}

		// Token: 0x060012B0 RID: 4784 RVA: 0x000654F4 File Offset: 0x000636F4
		internal void InternalPropertyChanged(JProperty childProperty)
		{
			this.OnPropertyChanged(childProperty.Name);
			if (this._listChanged != null)
			{
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, this.IndexOfItem(childProperty)));
			}
			if (this._collectionChanged != null)
			{
				this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, childProperty, childProperty, this.IndexOfItem(childProperty)));
			}
		}

		// Token: 0x060012B1 RID: 4785 RVA: 0x00065550 File Offset: 0x00063750
		internal void InternalPropertyChanging(JProperty childProperty)
		{
			this.OnPropertyChanging(childProperty.Name);
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x00065560 File Offset: 0x00063760
		internal override JToken CloneToken()
		{
			return new JObject(this);
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x060012B3 RID: 4787 RVA: 0x00065568 File Offset: 0x00063768
		public override JTokenType Type
		{
			get
			{
				return JTokenType.Object;
			}
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x0006556C File Offset: 0x0006376C
		public IEnumerable<JProperty> Properties()
		{
			return this._properties.Cast<JProperty>();
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x0006557C File Offset: 0x0006377C
		[return: Nullable(2)]
		public JProperty Property(string name)
		{
			return this.Property(name, StringComparison.Ordinal);
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x00065588 File Offset: 0x00063788
		[return: Nullable(2)]
		public JProperty Property(string name, StringComparison comparison)
		{
			if (name == null)
			{
				return null;
			}
			JToken jtoken;
			if (this._properties.TryGetValue(name, out jtoken))
			{
				return (JProperty)jtoken;
			}
			if (comparison != StringComparison.Ordinal)
			{
				for (int i = 0; i < this._properties.Count; i++)
				{
					JProperty jproperty = (JProperty)this._properties[i];
					if (string.Equals(jproperty.Name, name, comparison))
					{
						return jproperty;
					}
				}
			}
			return null;
		}

		// Token: 0x060012B7 RID: 4791 RVA: 0x00065604 File Offset: 0x00063804
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public JEnumerable<JToken> PropertyValues()
		{
			return new JEnumerable<JToken>(from p in this.Properties()
			select p.Value);
		}

		// Token: 0x17000405 RID: 1029
		[Nullable(2)]
		public override JToken this[object key]
		{
			[return: Nullable(2)]
			get
			{
				ValidationUtils.ArgumentNotNull(key, "key");
				string text = key as string;
				if (text == null)
				{
					throw new ArgumentException("Accessed JObject values with invalid key value: {0}. Object property name expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				return this[text];
			}
			[param: Nullable(2)]
			set
			{
				ValidationUtils.ArgumentNotNull(key, "key");
				string text = key as string;
				if (text == null)
				{
					throw new ArgumentException("Set JObject values with invalid key value: {0}. Object property name expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
				}
				this[text] = value;
			}
		}

		// Token: 0x17000406 RID: 1030
		[Nullable(2)]
		public JToken this[string propertyName]
		{
			[return: Nullable(2)]
			get
			{
				ValidationUtils.ArgumentNotNull(propertyName, "propertyName");
				JProperty jproperty = this.Property(propertyName, StringComparison.Ordinal);
				if (jproperty == null)
				{
					return null;
				}
				return jproperty.Value;
			}
			[param: Nullable(2)]
			set
			{
				JProperty jproperty = this.Property(propertyName, StringComparison.Ordinal);
				if (jproperty != null)
				{
					jproperty.Value = value;
					return;
				}
				this.OnPropertyChanging(propertyName);
				this.Add(propertyName, value);
				this.OnPropertyChanged(propertyName);
			}
		}

		// Token: 0x060012BC RID: 4796 RVA: 0x00065734 File Offset: 0x00063934
		public new static JObject Load(JsonReader reader)
		{
			return JObject.Load(reader, null);
		}

		// Token: 0x060012BD RID: 4797 RVA: 0x00065740 File Offset: 0x00063940
		public new static JObject Load(JsonReader reader, [Nullable(2)] JsonLoadSettings settings)
		{
			ValidationUtils.ArgumentNotNull(reader, "reader");
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading JObject from JsonReader.");
			}
			reader.MoveToContent();
			if (reader.TokenType != JsonToken.StartObject)
			{
				throw JsonReaderException.Create(reader, "Error reading JObject from JsonReader. Current JsonReader item is not an object: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JObject jobject = new JObject();
			jobject.SetLineInfo(reader as IJsonLineInfo, settings);
			jobject.ReadTokenFrom(reader, settings);
			return jobject;
		}

		// Token: 0x060012BE RID: 4798 RVA: 0x000657CC File Offset: 0x000639CC
		public new static JObject Parse(string json)
		{
			return JObject.Parse(json, null);
		}

		// Token: 0x060012BF RID: 4799 RVA: 0x000657D8 File Offset: 0x000639D8
		public new static JObject Parse(string json, [Nullable(2)] JsonLoadSettings settings)
		{
			JObject result;
			using (JsonReader jsonReader = new JsonTextReader(new StringReader(json)))
			{
				JObject jobject = JObject.Load(jsonReader, settings);
				while (jsonReader.Read())
				{
				}
				result = jobject;
			}
			return result;
		}

		// Token: 0x060012C0 RID: 4800 RVA: 0x00065828 File Offset: 0x00063A28
		public new static JObject FromObject(object o)
		{
			return JObject.FromObject(o, JsonSerializer.CreateDefault());
		}

		// Token: 0x060012C1 RID: 4801 RVA: 0x00065838 File Offset: 0x00063A38
		public new static JObject FromObject(object o, JsonSerializer jsonSerializer)
		{
			JToken jtoken = JToken.FromObjectInternal(o, jsonSerializer);
			if (jtoken.Type != JTokenType.Object)
			{
				throw new ArgumentException("Object serialized to {0}. JObject instance expected.".FormatWith(CultureInfo.InvariantCulture, jtoken.Type));
			}
			return (JObject)jtoken;
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x00065884 File Offset: 0x00063A84
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WriteStartObject();
			for (int i = 0; i < this._properties.Count; i++)
			{
				this._properties[i].WriteTo(writer, converters);
			}
			writer.WriteEndObject();
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x000658D0 File Offset: 0x00063AD0
		[NullableContext(2)]
		public JToken GetValue(string propertyName)
		{
			return this.GetValue(propertyName, StringComparison.Ordinal);
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x000658DC File Offset: 0x00063ADC
		[NullableContext(2)]
		public JToken GetValue(string propertyName, StringComparison comparison)
		{
			if (propertyName == null)
			{
				return null;
			}
			JProperty jproperty = this.Property(propertyName, comparison);
			if (jproperty == null)
			{
				return null;
			}
			return jproperty.Value;
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x000658FC File Offset: 0x00063AFC
		public bool TryGetValue(string propertyName, StringComparison comparison, [Nullable(2), NotNullWhen(true)] out JToken value)
		{
			value = this.GetValue(propertyName, comparison);
			return value != null;
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x00065910 File Offset: 0x00063B10
		public void Add(string propertyName, [Nullable(2)] JToken value)
		{
			this.Add(new JProperty(propertyName, value));
		}

		// Token: 0x060012C7 RID: 4807 RVA: 0x00065920 File Offset: 0x00063B20
		public bool ContainsKey(string propertyName)
		{
			ValidationUtils.ArgumentNotNull(propertyName, "propertyName");
			return this._properties.Contains(propertyName);
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x060012C8 RID: 4808 RVA: 0x0006593C File Offset: 0x00063B3C
		ICollection<string> IDictionary<string, JToken>.Keys
		{
			get
			{
				return this._properties.Keys;
			}
		}

		// Token: 0x060012C9 RID: 4809 RVA: 0x0006594C File Offset: 0x00063B4C
		public bool Remove(string propertyName)
		{
			JProperty jproperty = this.Property(propertyName, StringComparison.Ordinal);
			if (jproperty == null)
			{
				return false;
			}
			jproperty.Remove();
			return true;
		}

		// Token: 0x060012CA RID: 4810 RVA: 0x00065978 File Offset: 0x00063B78
		public bool TryGetValue(string propertyName, [Nullable(2), NotNullWhen(true)] out JToken value)
		{
			JProperty jproperty = this.Property(propertyName, StringComparison.Ordinal);
			if (jproperty == null)
			{
				value = null;
				return false;
			}
			value = jproperty.Value;
			return true;
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x060012CB RID: 4811 RVA: 0x000659A8 File Offset: 0x00063BA8
		[Nullable(new byte[]
		{
			1,
			2
		})]
		ICollection<JToken> IDictionary<string, JToken>.Values
		{
			[return: Nullable(new byte[]
			{
				1,
				2
			})]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060012CC RID: 4812 RVA: 0x000659B0 File Offset: 0x00063BB0
		void ICollection<KeyValuePair<string, JToken>>.Add([Nullable(new byte[]
		{
			0,
			1,
			2
		})] KeyValuePair<string, JToken> item)
		{
			this.Add(new JProperty(item.Key, item.Value));
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x000659CC File Offset: 0x00063BCC
		void ICollection<KeyValuePair<string, JToken>>.Clear()
		{
			base.RemoveAll();
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x000659D4 File Offset: 0x00063BD4
		bool ICollection<KeyValuePair<string, JToken>>.Contains([Nullable(new byte[]
		{
			0,
			1,
			2
		})] KeyValuePair<string, JToken> item)
		{
			JProperty jproperty = this.Property(item.Key, StringComparison.Ordinal);
			return jproperty != null && jproperty.Value == item.Value;
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x00065A0C File Offset: 0x00063C0C
		void ICollection<KeyValuePair<string, JToken>>.CopyTo([Nullable(new byte[]
		{
			1,
			0,
			1,
			2
		})] KeyValuePair<string, JToken>[] array, int arrayIndex)
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
			if (base.Count > array.Length - arrayIndex)
			{
				throw new ArgumentException("The number of elements in the source JObject is greater than the available space from arrayIndex to the end of the destination array.");
			}
			int num = 0;
			foreach (JToken jtoken in this._properties)
			{
				JProperty jproperty = (JProperty)jtoken;
				array[arrayIndex + num] = new KeyValuePair<string, JToken>(jproperty.Name, jproperty.Value);
				num++;
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x060012D0 RID: 4816 RVA: 0x00065AE0 File Offset: 0x00063CE0
		bool ICollection<KeyValuePair<string, JToken>>.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x00065AE4 File Offset: 0x00063CE4
		bool ICollection<KeyValuePair<string, JToken>>.Remove([Nullable(new byte[]
		{
			0,
			1,
			2
		})] KeyValuePair<string, JToken> item)
		{
			if (!((ICollection<KeyValuePair<string, JToken>>)this).Contains(item))
			{
				return false;
			}
			((IDictionary<string, JToken>)this).Remove(item.Key);
			return true;
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x00065B04 File Offset: 0x00063D04
		internal override int GetDeepHashCode()
		{
			return base.ContentsHashCode();
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x00065B0C File Offset: 0x00063D0C
		[return: Nullable(new byte[]
		{
			1,
			0,
			1,
			2
		})]
		public IEnumerator<KeyValuePair<string, JToken>> GetEnumerator()
		{
			foreach (JToken jtoken in this._properties)
			{
				JProperty jproperty = (JProperty)jtoken;
				yield return new KeyValuePair<string, JToken>(jproperty.Name, jproperty.Value);
			}
			IEnumerator<JToken> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x00065B1C File Offset: 0x00063D1C
		protected virtual void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
			if (propertyChanged == null)
			{
				return;
			}
			propertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x00065B38 File Offset: 0x00063D38
		protected virtual void OnPropertyChanging(string propertyName)
		{
			PropertyChangingEventHandler propertyChanging = this.PropertyChanging;
			if (propertyChanging == null)
			{
				return;
			}
			propertyChanging(this, new PropertyChangingEventArgs(propertyName));
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x00065B54 File Offset: 0x00063D54
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			return ((ICustomTypeDescriptor)this).GetProperties(null);
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x00065B60 File Offset: 0x00063D60
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			PropertyDescriptor[] array = new PropertyDescriptor[base.Count];
			int num = 0;
			foreach (KeyValuePair<string, JToken> keyValuePair in this)
			{
				array[num] = new JPropertyDescriptor(keyValuePair.Key);
				num++;
			}
			return new PropertyDescriptorCollection(array);
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x00065BD8 File Offset: 0x00063DD8
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return AttributeCollection.Empty;
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x00065BE0 File Offset: 0x00063DE0
		[NullableContext(2)]
		string ICustomTypeDescriptor.GetClassName()
		{
			return null;
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x00065BE4 File Offset: 0x00063DE4
		[NullableContext(2)]
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x00065BE8 File Offset: 0x00063DE8
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return new TypeConverter();
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x00065BF0 File Offset: 0x00063DF0
		[NullableContext(2)]
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x00065BF4 File Offset: 0x00063DF4
		[NullableContext(2)]
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x00065BF8 File Offset: 0x00063DF8
		[return: Nullable(2)]
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x00065BFC File Offset: 0x00063DFC
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return EventDescriptorCollection.Empty;
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x00065C04 File Offset: 0x00063E04
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return EventDescriptorCollection.Empty;
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x00065C0C File Offset: 0x00063E0C
		[return: Nullable(2)]
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			if (pd is JPropertyDescriptor)
			{
				return this;
			}
			return null;
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x00065C1C File Offset: 0x00063E1C
		protected override DynamicMetaObject GetMetaObject(Expression parameter)
		{
			return new DynamicProxyMetaObject<JObject>(parameter, this, new JObject.JObjectDynamicProxy());
		}

		// Token: 0x040008DB RID: 2267
		private readonly JPropertyKeyedCollection _properties = new JPropertyKeyedCollection();

		// Token: 0x0200030C RID: 780
		[Nullable(new byte[]
		{
			0,
			1
		})]
		private class JObjectDynamicProxy : DynamicProxy<JObject>
		{
			// Token: 0x0600188B RID: 6283 RVA: 0x0007A040 File Offset: 0x00078240
			public override bool TryGetMember(JObject instance, GetMemberBinder binder, [Nullable(2)] out object result)
			{
				result = instance[binder.Name];
				return true;
			}

			// Token: 0x0600188C RID: 6284 RVA: 0x0007A054 File Offset: 0x00078254
			public override bool TrySetMember(JObject instance, SetMemberBinder binder, object value)
			{
				JToken jtoken = value as JToken;
				if (jtoken == null)
				{
					jtoken = new JValue(value);
				}
				instance[binder.Name] = jtoken;
				return true;
			}

			// Token: 0x0600188D RID: 6285 RVA: 0x0007A088 File Offset: 0x00078288
			public override IEnumerable<string> GetDynamicMemberNames(JObject instance)
			{
				return from p in instance.Properties()
				select p.Name;
			}
		}
	}
}
