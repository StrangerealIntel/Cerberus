using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020001A5 RID: 421
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class JsonContract
	{
		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000F33 RID: 3891 RVA: 0x00056934 File Offset: 0x00054B34
		public Type UnderlyingType { get; }

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000F34 RID: 3892 RVA: 0x0005693C File Offset: 0x00054B3C
		// (set) Token: 0x06000F35 RID: 3893 RVA: 0x00056944 File Offset: 0x00054B44
		public Type CreatedType
		{
			get
			{
				return this._createdType;
			}
			set
			{
				ValidationUtils.ArgumentNotNull(value, "value");
				this._createdType = value;
				this.IsSealed = this._createdType.IsSealed();
				this.IsInstantiable = (!this._createdType.IsInterface() && !this._createdType.IsAbstract());
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000F36 RID: 3894 RVA: 0x000569A4 File Offset: 0x00054BA4
		// (set) Token: 0x06000F37 RID: 3895 RVA: 0x000569AC File Offset: 0x00054BAC
		public bool? IsReference { get; set; }

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000F38 RID: 3896 RVA: 0x000569B8 File Offset: 0x00054BB8
		// (set) Token: 0x06000F39 RID: 3897 RVA: 0x000569C0 File Offset: 0x00054BC0
		[Nullable(2)]
		public JsonConverter Converter { [NullableContext(2)] get; [NullableContext(2)] set; }

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000F3A RID: 3898 RVA: 0x000569CC File Offset: 0x00054BCC
		// (set) Token: 0x06000F3B RID: 3899 RVA: 0x000569D4 File Offset: 0x00054BD4
		[Nullable(2)]
		public JsonConverter InternalConverter { [NullableContext(2)] get; [NullableContext(2)] internal set; }

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000F3C RID: 3900 RVA: 0x000569E0 File Offset: 0x00054BE0
		public IList<SerializationCallback> OnDeserializedCallbacks
		{
			get
			{
				if (this._onDeserializedCallbacks == null)
				{
					this._onDeserializedCallbacks = new List<SerializationCallback>();
				}
				return this._onDeserializedCallbacks;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000F3D RID: 3901 RVA: 0x00056A00 File Offset: 0x00054C00
		public IList<SerializationCallback> OnDeserializingCallbacks
		{
			get
			{
				if (this._onDeserializingCallbacks == null)
				{
					this._onDeserializingCallbacks = new List<SerializationCallback>();
				}
				return this._onDeserializingCallbacks;
			}
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000F3E RID: 3902 RVA: 0x00056A20 File Offset: 0x00054C20
		public IList<SerializationCallback> OnSerializedCallbacks
		{
			get
			{
				if (this._onSerializedCallbacks == null)
				{
					this._onSerializedCallbacks = new List<SerializationCallback>();
				}
				return this._onSerializedCallbacks;
			}
		}

		// Token: 0x1700032A RID: 810
		// (get) Token: 0x06000F3F RID: 3903 RVA: 0x00056A40 File Offset: 0x00054C40
		public IList<SerializationCallback> OnSerializingCallbacks
		{
			get
			{
				if (this._onSerializingCallbacks == null)
				{
					this._onSerializingCallbacks = new List<SerializationCallback>();
				}
				return this._onSerializingCallbacks;
			}
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06000F40 RID: 3904 RVA: 0x00056A60 File Offset: 0x00054C60
		public IList<SerializationErrorCallback> OnErrorCallbacks
		{
			get
			{
				if (this._onErrorCallbacks == null)
				{
					this._onErrorCallbacks = new List<SerializationErrorCallback>();
				}
				return this._onErrorCallbacks;
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000F41 RID: 3905 RVA: 0x00056A80 File Offset: 0x00054C80
		// (set) Token: 0x06000F42 RID: 3906 RVA: 0x00056A88 File Offset: 0x00054C88
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public Func<object> DefaultCreator { [return: Nullable(new byte[]
		{
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1
		})] set; }

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000F43 RID: 3907 RVA: 0x00056A94 File Offset: 0x00054C94
		// (set) Token: 0x06000F44 RID: 3908 RVA: 0x00056A9C File Offset: 0x00054C9C
		public bool DefaultCreatorNonPublic { get; set; }

		// Token: 0x06000F45 RID: 3909 RVA: 0x00056AA8 File Offset: 0x00054CA8
		internal JsonContract(Type underlyingType)
		{
			ValidationUtils.ArgumentNotNull(underlyingType, "underlyingType");
			this.UnderlyingType = underlyingType;
			underlyingType = ReflectionUtils.EnsureNotByRefType(underlyingType);
			this.IsNullable = ReflectionUtils.IsNullable(underlyingType);
			this.NonNullableUnderlyingType = ((this.IsNullable && ReflectionUtils.IsNullableType(underlyingType)) ? Nullable.GetUnderlyingType(underlyingType) : underlyingType);
			this._createdType = (this.CreatedType = this.NonNullableUnderlyingType);
			this.IsConvertable = ConvertUtils.IsConvertible(this.NonNullableUnderlyingType);
			this.IsEnum = this.NonNullableUnderlyingType.IsEnum();
			this.InternalReadType = ReadType.Read;
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x00056B4C File Offset: 0x00054D4C
		internal void InvokeOnSerializing(object o, StreamingContext context)
		{
			if (this._onSerializingCallbacks != null)
			{
				foreach (SerializationCallback serializationCallback in this._onSerializingCallbacks)
				{
					serializationCallback(o, context);
				}
			}
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x00056BB0 File Offset: 0x00054DB0
		internal void InvokeOnSerialized(object o, StreamingContext context)
		{
			if (this._onSerializedCallbacks != null)
			{
				foreach (SerializationCallback serializationCallback in this._onSerializedCallbacks)
				{
					serializationCallback(o, context);
				}
			}
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x00056C14 File Offset: 0x00054E14
		internal void InvokeOnDeserializing(object o, StreamingContext context)
		{
			if (this._onDeserializingCallbacks != null)
			{
				foreach (SerializationCallback serializationCallback in this._onDeserializingCallbacks)
				{
					serializationCallback(o, context);
				}
			}
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x00056C78 File Offset: 0x00054E78
		internal void InvokeOnDeserialized(object o, StreamingContext context)
		{
			if (this._onDeserializedCallbacks != null)
			{
				foreach (SerializationCallback serializationCallback in this._onDeserializedCallbacks)
				{
					serializationCallback(o, context);
				}
			}
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x00056CDC File Offset: 0x00054EDC
		internal void InvokeOnError(object o, StreamingContext context, ErrorContext errorContext)
		{
			if (this._onErrorCallbacks != null)
			{
				foreach (SerializationErrorCallback serializationErrorCallback in this._onErrorCallbacks)
				{
					serializationErrorCallback(o, context, errorContext);
				}
			}
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x00056D40 File Offset: 0x00054F40
		internal static SerializationCallback CreateSerializationCallback(MethodInfo callbackMethodInfo)
		{
			return delegate(object o, StreamingContext context)
			{
				callbackMethodInfo.Invoke(o, new object[]
				{
					context
				});
			};
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x00056D5C File Offset: 0x00054F5C
		internal static SerializationErrorCallback CreateSerializationErrorCallback(MethodInfo callbackMethodInfo)
		{
			return delegate(object o, StreamingContext context, ErrorContext econtext)
			{
				callbackMethodInfo.Invoke(o, new object[]
				{
					context,
					econtext
				});
			};
		}

		// Token: 0x040007C1 RID: 1985
		internal bool IsNullable;

		// Token: 0x040007C2 RID: 1986
		internal bool IsConvertable;

		// Token: 0x040007C3 RID: 1987
		internal bool IsEnum;

		// Token: 0x040007C4 RID: 1988
		internal Type NonNullableUnderlyingType;

		// Token: 0x040007C5 RID: 1989
		internal ReadType InternalReadType;

		// Token: 0x040007C6 RID: 1990
		internal JsonContractType ContractType;

		// Token: 0x040007C7 RID: 1991
		internal bool IsReadOnlyOrFixedSize;

		// Token: 0x040007C8 RID: 1992
		internal bool IsSealed;

		// Token: 0x040007C9 RID: 1993
		internal bool IsInstantiable;

		// Token: 0x040007CA RID: 1994
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private List<SerializationCallback> _onDeserializedCallbacks;

		// Token: 0x040007CB RID: 1995
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private List<SerializationCallback> _onDeserializingCallbacks;

		// Token: 0x040007CC RID: 1996
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private List<SerializationCallback> _onSerializedCallbacks;

		// Token: 0x040007CD RID: 1997
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private List<SerializationCallback> _onSerializingCallbacks;

		// Token: 0x040007CE RID: 1998
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private List<SerializationErrorCallback> _onErrorCallbacks;

		// Token: 0x040007CF RID: 1999
		private Type _createdType;
	}
}
