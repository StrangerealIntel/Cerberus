using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020001A6 RID: 422
	[NullableContext(2)]
	[Nullable(0)]
	public class JsonDictionaryContract : JsonContainerContract
	{
		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000F4D RID: 3917 RVA: 0x00056D78 File Offset: 0x00054F78
		// (set) Token: 0x06000F4E RID: 3918 RVA: 0x00056D80 File Offset: 0x00054F80
		[Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		public Func<string, string> DictionaryKeyResolver { [return: Nullable(new byte[]
		{
			2,
			1,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1,
			1
		})] set; }

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000F4F RID: 3919 RVA: 0x00056D8C File Offset: 0x00054F8C
		public Type DictionaryKeyType { get; }

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000F50 RID: 3920 RVA: 0x00056D94 File Offset: 0x00054F94
		public Type DictionaryValueType { get; }

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x00056D9C File Offset: 0x00054F9C
		// (set) Token: 0x06000F52 RID: 3922 RVA: 0x00056DA4 File Offset: 0x00054FA4
		internal JsonContract KeyContract { get; set; }

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x00056DB0 File Offset: 0x00054FB0
		internal bool ShouldCreateWrapper { get; }

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x00056DB8 File Offset: 0x00054FB8
		[Nullable(new byte[]
		{
			2,
			1
		})]
		internal ObjectConstructor<object> ParameterizedCreator
		{
			[return: Nullable(new byte[]
			{
				2,
				1
			})]
			get
			{
				if (this._parameterizedCreator == null && this._parameterizedConstructor != null)
				{
					this._parameterizedCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(this._parameterizedConstructor);
				}
				return this._parameterizedCreator;
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x00056DF4 File Offset: 0x00054FF4
		// (set) Token: 0x06000F56 RID: 3926 RVA: 0x00056DFC File Offset: 0x00054FFC
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public ObjectConstructor<object> OverrideCreator
		{
			[return: Nullable(new byte[]
			{
				2,
				1
			})]
			get
			{
				return this._overrideCreator;
			}
			[param: Nullable(new byte[]
			{
				2,
				1
			})]
			set
			{
				this._overrideCreator = value;
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x00056E08 File Offset: 0x00055008
		// (set) Token: 0x06000F58 RID: 3928 RVA: 0x00056E10 File Offset: 0x00055010
		public bool HasParameterizedCreator { get; set; }

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000F59 RID: 3929 RVA: 0x00056E1C File Offset: 0x0005501C
		internal bool HasParameterizedCreatorInternal
		{
			get
			{
				return this.HasParameterizedCreator || this._parameterizedCreator != null || this._parameterizedConstructor != null;
			}
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x00056E44 File Offset: 0x00055044
		[NullableContext(1)]
		public JsonDictionaryContract(Type underlyingType) : base(underlyingType)
		{
			this.ContractType = JsonContractType.Dictionary;
			Type type;
			Type type2;
			if (ReflectionUtils.ImplementsGenericDefinition(underlyingType, typeof(IDictionary<, >), out this._genericCollectionDefinitionType))
			{
				type = this._genericCollectionDefinitionType.GetGenericArguments()[0];
				type2 = this._genericCollectionDefinitionType.GetGenericArguments()[1];
				if (ReflectionUtils.IsGenericDefinition(base.UnderlyingType, typeof(IDictionary<, >)))
				{
					base.CreatedType = typeof(Dictionary<, >).MakeGenericType(new Type[]
					{
						type,
						type2
					});
				}
				else if (underlyingType.IsGenericType() && underlyingType.GetGenericTypeDefinition().FullName == "System.Collections.Concurrent.ConcurrentDictionary`2")
				{
					this.ShouldCreateWrapper = 1;
				}
			}
			else
			{
				ReflectionUtils.GetDictionaryKeyValueTypes(base.UnderlyingType, out type, out type2);
				if (base.UnderlyingType == typeof(IDictionary))
				{
					base.CreatedType = typeof(Dictionary<object, object>);
				}
			}
			if (type != null && type2 != null)
			{
				this._parameterizedConstructor = CollectionUtils.ResolveEnumerableCollectionConstructor(base.CreatedType, typeof(KeyValuePair<, >).MakeGenericType(new Type[]
				{
					type,
					type2
				}), typeof(IDictionary<, >).MakeGenericType(new Type[]
				{
					type,
					type2
				}));
				if (!this.HasParameterizedCreatorInternal && underlyingType.Name == "FSharpMap`2")
				{
					FSharpUtils.EnsureInitialized(underlyingType.Assembly());
					this._parameterizedCreator = FSharpUtils.Instance.CreateMap(type, type2);
				}
			}
			if (!typeof(IDictionary).IsAssignableFrom(base.CreatedType))
			{
				this.ShouldCreateWrapper = 1;
			}
			this.DictionaryKeyType = type;
			this.DictionaryValueType = type2;
			Type createdType;
			ObjectConstructor<object> parameterizedCreator;
			if (this.DictionaryKeyType != null && this.DictionaryValueType != null && ImmutableCollectionsUtils.TryBuildImmutableForDictionaryContract(underlyingType, this.DictionaryKeyType, this.DictionaryValueType, out createdType, out parameterizedCreator))
			{
				base.CreatedType = createdType;
				this._parameterizedCreator = parameterizedCreator;
				this.IsReadOnlyOrFixedSize = true;
			}
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x00057070 File Offset: 0x00055270
		[NullableContext(1)]
		internal IWrappedDictionary CreateWrapper(object dictionary)
		{
			if (this._genericWrapperCreator == null)
			{
				this._genericWrapperType = typeof(DictionaryWrapper<, >).MakeGenericType(new Type[]
				{
					this.DictionaryKeyType,
					this.DictionaryValueType
				});
				ConstructorInfo constructor = this._genericWrapperType.GetConstructor(new Type[]
				{
					this._genericCollectionDefinitionType
				});
				this._genericWrapperCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(constructor);
			}
			return (IWrappedDictionary)this._genericWrapperCreator(new object[]
			{
				dictionary
			});
		}

		// Token: 0x06000F5C RID: 3932 RVA: 0x00057100 File Offset: 0x00055300
		[NullableContext(1)]
		internal IDictionary CreateTemporaryDictionary()
		{
			if (this._genericTemporaryDictionaryCreator == null)
			{
				Type type = typeof(Dictionary<, >).MakeGenericType(new Type[]
				{
					this.DictionaryKeyType ?? typeof(object),
					this.DictionaryValueType ?? typeof(object)
				});
				this._genericTemporaryDictionaryCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateDefaultConstructor<object>(type);
			}
			return (IDictionary)this._genericTemporaryDictionaryCreator();
		}

		// Token: 0x040007DA RID: 2010
		private readonly Type _genericCollectionDefinitionType;

		// Token: 0x040007DB RID: 2011
		private Type _genericWrapperType;

		// Token: 0x040007DC RID: 2012
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private ObjectConstructor<object> _genericWrapperCreator;

		// Token: 0x040007DD RID: 2013
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private Func<object> _genericTemporaryDictionaryCreator;

		// Token: 0x040007DF RID: 2015
		private readonly ConstructorInfo _parameterizedConstructor;

		// Token: 0x040007E0 RID: 2016
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private ObjectConstructor<object> _overrideCreator;

		// Token: 0x040007E1 RID: 2017
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private ObjectConstructor<object> _parameterizedCreator;
	}
}
