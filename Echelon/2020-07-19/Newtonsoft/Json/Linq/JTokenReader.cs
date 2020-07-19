using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x020001E1 RID: 481
	[NullableContext(1)]
	[Nullable(0)]
	public class JTokenReader : JsonReader, IJsonLineInfo
	{
		// Token: 0x17000429 RID: 1065
		// (get) Token: 0x060013C6 RID: 5062 RVA: 0x000690A0 File Offset: 0x000672A0
		[Nullable(2)]
		public JToken CurrentToken
		{
			[NullableContext(2)]
			get
			{
				return this._current;
			}
		}

		// Token: 0x060013C7 RID: 5063 RVA: 0x000690A8 File Offset: 0x000672A8
		public JTokenReader(JToken token)
		{
			ValidationUtils.ArgumentNotNull(token, "token");
			this._root = token;
		}

		// Token: 0x060013C8 RID: 5064 RVA: 0x000690C4 File Offset: 0x000672C4
		public JTokenReader(JToken token, string initialPath) : this(token)
		{
			this._initialPath = initialPath;
		}

		// Token: 0x060013C9 RID: 5065 RVA: 0x000690D4 File Offset: 0x000672D4
		public override bool Read()
		{
			if (base.CurrentState != JsonReader.State.Start)
			{
				if (this._current == null)
				{
					return false;
				}
				JContainer jcontainer = this._current as JContainer;
				if (jcontainer != null && this._parent != jcontainer)
				{
					return this.ReadInto(jcontainer);
				}
				return this.ReadOver(this._current);
			}
			else
			{
				if (this._current == this._root)
				{
					return false;
				}
				this._current = this._root;
				this.SetToken(this._current);
				return true;
			}
		}

		// Token: 0x060013CA RID: 5066 RVA: 0x0006915C File Offset: 0x0006735C
		private bool ReadOver(JToken t)
		{
			if (t == this._root)
			{
				return this.ReadToEnd();
			}
			JToken next = t.Next;
			if (next != null && next != t && t != t.Parent.Last)
			{
				this._current = next;
				this.SetToken(this._current);
				return true;
			}
			if (t.Parent == null)
			{
				return this.ReadToEnd();
			}
			return this.SetEnd(t.Parent);
		}

		// Token: 0x060013CB RID: 5067 RVA: 0x000691D8 File Offset: 0x000673D8
		private bool ReadToEnd()
		{
			this._current = null;
			base.SetToken(JsonToken.None);
			return false;
		}

		// Token: 0x060013CC RID: 5068 RVA: 0x000691EC File Offset: 0x000673EC
		private JsonToken? GetEndToken(JContainer c)
		{
			switch (c.Type)
			{
			case JTokenType.Object:
				return new JsonToken?(JsonToken.EndObject);
			case JTokenType.Array:
				return new JsonToken?(JsonToken.EndArray);
			case JTokenType.Constructor:
				return new JsonToken?(JsonToken.EndConstructor);
			case JTokenType.Property:
				return null;
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("Type", c.Type, "Unexpected JContainer type.");
			}
		}

		// Token: 0x060013CD RID: 5069 RVA: 0x00069260 File Offset: 0x00067460
		private bool ReadInto(JContainer c)
		{
			JToken first = c.First;
			if (first == null)
			{
				return this.SetEnd(c);
			}
			this.SetToken(first);
			this._current = first;
			this._parent = c;
			return true;
		}

		// Token: 0x060013CE RID: 5070 RVA: 0x0006929C File Offset: 0x0006749C
		private bool SetEnd(JContainer c)
		{
			JsonToken? endToken = this.GetEndToken(c);
			if (endToken != null)
			{
				base.SetToken(endToken.GetValueOrDefault());
				this._current = c;
				this._parent = c;
				return true;
			}
			return this.ReadOver(c);
		}

		// Token: 0x060013CF RID: 5071 RVA: 0x000692E8 File Offset: 0x000674E8
		private void SetToken(JToken token)
		{
			switch (token.Type)
			{
			case JTokenType.Object:
				base.SetToken(JsonToken.StartObject);
				return;
			case JTokenType.Array:
				base.SetToken(JsonToken.StartArray);
				return;
			case JTokenType.Constructor:
				base.SetToken(JsonToken.StartConstructor, ((JConstructor)token).Name);
				return;
			case JTokenType.Property:
				base.SetToken(JsonToken.PropertyName, ((JProperty)token).Name);
				return;
			case JTokenType.Comment:
				base.SetToken(JsonToken.Comment, ((JValue)token).Value);
				return;
			case JTokenType.Integer:
				base.SetToken(JsonToken.Integer, ((JValue)token).Value);
				return;
			case JTokenType.Float:
				base.SetToken(JsonToken.Float, ((JValue)token).Value);
				return;
			case JTokenType.String:
				base.SetToken(JsonToken.String, ((JValue)token).Value);
				return;
			case JTokenType.Boolean:
				base.SetToken(JsonToken.Boolean, ((JValue)token).Value);
				return;
			case JTokenType.Null:
				base.SetToken(JsonToken.Null, ((JValue)token).Value);
				return;
			case JTokenType.Undefined:
				base.SetToken(JsonToken.Undefined, ((JValue)token).Value);
				return;
			case JTokenType.Date:
			{
				object obj = ((JValue)token).Value;
				if (obj is DateTime)
				{
					DateTime value = (DateTime)obj;
					obj = DateTimeUtils.EnsureDateTime(value, base.DateTimeZoneHandling);
				}
				base.SetToken(JsonToken.Date, obj);
				return;
			}
			case JTokenType.Raw:
				base.SetToken(JsonToken.Raw, ((JValue)token).Value);
				return;
			case JTokenType.Bytes:
				base.SetToken(JsonToken.Bytes, ((JValue)token).Value);
				return;
			case JTokenType.Guid:
				base.SetToken(JsonToken.String, this.SafeToString(((JValue)token).Value));
				return;
			case JTokenType.Uri:
			{
				object value2 = ((JValue)token).Value;
				JsonToken newToken = JsonToken.String;
				Uri uri = value2 as Uri;
				base.SetToken(newToken, (uri != null) ? uri.OriginalString : this.SafeToString(value2));
				return;
			}
			case JTokenType.TimeSpan:
				base.SetToken(JsonToken.String, this.SafeToString(((JValue)token).Value));
				return;
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("Type", token.Type, "Unexpected JTokenType.");
			}
		}

		// Token: 0x060013D0 RID: 5072 RVA: 0x000694F8 File Offset: 0x000676F8
		[NullableContext(2)]
		private string SafeToString(object value)
		{
			if (value == null)
			{
				return null;
			}
			return value.ToString();
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x00069508 File Offset: 0x00067708
		bool IJsonLineInfo.HasLineInfo()
		{
			if (base.CurrentState == JsonReader.State.Start)
			{
				return false;
			}
			IJsonLineInfo current = this._current;
			return current != null && current.HasLineInfo();
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x0006953C File Offset: 0x0006773C
		int IJsonLineInfo.LineNumber
		{
			get
			{
				if (base.CurrentState == JsonReader.State.Start)
				{
					return 0;
				}
				IJsonLineInfo current = this._current;
				if (current != null)
				{
					return current.LineNumber;
				}
				return 0;
			}
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x060013D3 RID: 5075 RVA: 0x00069570 File Offset: 0x00067770
		int IJsonLineInfo.LinePosition
		{
			get
			{
				if (base.CurrentState == JsonReader.State.Start)
				{
					return 0;
				}
				IJsonLineInfo current = this._current;
				if (current != null)
				{
					return current.LinePosition;
				}
				return 0;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x000695A4 File Offset: 0x000677A4
		public override string Path
		{
			get
			{
				string text = base.Path;
				if (this._initialPath == null)
				{
					this._initialPath = this._root.Path;
				}
				if (!StringUtils.IsNullOrEmpty(this._initialPath))
				{
					if (StringUtils.IsNullOrEmpty(text))
					{
						return this._initialPath;
					}
					if (text.StartsWith('['))
					{
						text = this._initialPath + text;
					}
					else
					{
						text = this._initialPath + "." + text;
					}
				}
				return text;
			}
		}

		// Token: 0x040008F7 RID: 2295
		private readonly JToken _root;

		// Token: 0x040008F8 RID: 2296
		[Nullable(2)]
		private string _initialPath;

		// Token: 0x040008F9 RID: 2297
		[Nullable(2)]
		private JToken _parent;

		// Token: 0x040008FA RID: 2298
		[Nullable(2)]
		private JToken _current;
	}
}
