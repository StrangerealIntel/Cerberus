using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020001D5 RID: 469
	[NullableContext(1)]
	[Nullable(0)]
	public class JConstructor : JContainer
	{
		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x00063AB0 File Offset: 0x00061CB0
		protected override IList<JToken> ChildrenTokens
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x00063AB8 File Offset: 0x00061CB8
		[NullableContext(2)]
		internal override int IndexOfItem(JToken item)
		{
			if (item == null)
			{
				return -1;
			}
			return this._values.IndexOfReference(item);
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x00063AD0 File Offset: 0x00061CD0
		internal override void MergeItem(object content, [Nullable(2)] JsonMergeSettings settings)
		{
			JConstructor jconstructor = content as JConstructor;
			if (jconstructor == null)
			{
				return;
			}
			if (jconstructor.Name != null)
			{
				this.Name = jconstructor.Name;
			}
			JContainer.MergeEnumerableContent(this, jconstructor, settings);
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06001228 RID: 4648 RVA: 0x00063B10 File Offset: 0x00061D10
		// (set) Token: 0x06001229 RID: 4649 RVA: 0x00063B18 File Offset: 0x00061D18
		[Nullable(2)]
		public string Name
		{
			[NullableContext(2)]
			get
			{
				return this._name;
			}
			[NullableContext(2)]
			set
			{
				this._name = value;
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x00063B24 File Offset: 0x00061D24
		public override JTokenType Type
		{
			get
			{
				return JTokenType.Constructor;
			}
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x00063B28 File Offset: 0x00061D28
		public JConstructor()
		{
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x00063B3C File Offset: 0x00061D3C
		public JConstructor(JConstructor other) : base(other)
		{
			this._name = other.Name;
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x00063B5C File Offset: 0x00061D5C
		public JConstructor(string name, params object[] content) : this(name, content)
		{
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x00063B68 File Offset: 0x00061D68
		public JConstructor(string name, object content) : this(name)
		{
			this.Add(content);
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x00063B78 File Offset: 0x00061D78
		public JConstructor(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException("Constructor name cannot be empty.", "name");
			}
			this._name = name;
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00063BD0 File Offset: 0x00061DD0
		internal override bool DeepEquals(JToken node)
		{
			JConstructor jconstructor = node as JConstructor;
			return jconstructor != null && this._name == jconstructor.Name && base.ContentsEqual(jconstructor);
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00063C10 File Offset: 0x00061E10
		internal override JToken CloneToken()
		{
			return new JConstructor(this);
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x00063C18 File Offset: 0x00061E18
		public override void WriteTo(JsonWriter writer, params JsonConverter[] converters)
		{
			writer.WriteStartConstructor(this._name);
			int count = this._values.Count;
			for (int i = 0; i < count; i++)
			{
				this._values[i].WriteTo(writer, converters);
			}
			writer.WriteEndConstructor();
		}

		// Token: 0x170003EC RID: 1004
		[Nullable(2)]
		public override JToken this[object key]
		{
			[return: Nullable(2)]
			get
			{
				ValidationUtils.ArgumentNotNull(key, "key");
				if (key is int)
				{
					int index = (int)key;
					return this.GetItem(index);
				}
				throw new ArgumentException("Accessed JConstructor values with invalid key value: {0}. Argument position index expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
			}
			[param: Nullable(2)]
			set
			{
				ValidationUtils.ArgumentNotNull(key, "key");
				if (key is int)
				{
					int index = (int)key;
					this.SetItem(index, value);
					return;
				}
				throw new ArgumentException("Set JConstructor values with invalid key value: {0}. Argument position index expected.".FormatWith(CultureInfo.InvariantCulture, MiscellaneousUtils.ToString(key)));
			}
		}

		// Token: 0x06001235 RID: 4661 RVA: 0x00063D1C File Offset: 0x00061F1C
		internal override int GetDeepHashCode()
		{
			string name = this._name;
			return ((name != null) ? name.GetHashCode() : 0) ^ base.ContentsHashCode();
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00063D40 File Offset: 0x00061F40
		public new static JConstructor Load(JsonReader reader)
		{
			return JConstructor.Load(reader, null);
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x00063D4C File Offset: 0x00061F4C
		public new static JConstructor Load(JsonReader reader, [Nullable(2)] JsonLoadSettings settings)
		{
			if (reader.TokenType == JsonToken.None && !reader.Read())
			{
				throw JsonReaderException.Create(reader, "Error reading JConstructor from JsonReader.");
			}
			reader.MoveToContent();
			if (reader.TokenType != JsonToken.StartConstructor)
			{
				throw JsonReaderException.Create(reader, "Error reading JConstructor from JsonReader. Current JsonReader item is not a constructor: {0}".FormatWith(CultureInfo.InvariantCulture, reader.TokenType));
			}
			JConstructor jconstructor = new JConstructor((string)reader.Value);
			jconstructor.SetLineInfo(reader as IJsonLineInfo, settings);
			jconstructor.ReadTokenFrom(reader, settings);
			return jconstructor;
		}

		// Token: 0x040008D2 RID: 2258
		[Nullable(2)]
		private string _name;

		// Token: 0x040008D3 RID: 2259
		private readonly List<JToken> _values = new List<JToken>();
	}
}
