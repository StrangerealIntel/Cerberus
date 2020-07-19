using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200019E RID: 414
	[NullableContext(2)]
	[Nullable(0)]
	public class JsonArrayContract : JsonContainerContract
	{
		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000F07 RID: 3847 RVA: 0x00056160 File Offset: 0x00054360
		public Type CollectionItemType { get; }

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x00056168 File Offset: 0x00054368
		public bool IsMultidimensionalArray { get; }

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000F09 RID: 3849 RVA: 0x00056170 File Offset: 0x00054370
		internal bool IsArray { get; }

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x00056178 File Offset: 0x00054378
		internal bool ShouldCreateWrapper { get; }

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000F0B RID: 3851 RVA: 0x00056180 File Offset: 0x00054380
		// (set) Token: 0x06000F0C RID: 3852 RVA: 0x00056188 File Offset: 0x00054388
		internal bool CanDeserialize { get; private set; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000F0D RID: 3853 RVA: 0x00056194 File Offset: 0x00054394
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

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x000561D0 File Offset: 0x000543D0
		// (set) Token: 0x06000F0F RID: 3855 RVA: 0x000561D8 File Offset: 0x000543D8
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
				this.CanDeserialize = true;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x000561E8 File Offset: 0x000543E8
		// (set) Token: 0x06000F11 RID: 3857 RVA: 0x000561F0 File Offset: 0x000543F0
		public bool HasParameterizedCreator { get; set; }

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x000561FC File Offset: 0x000543FC
		internal bool HasParameterizedCreatorInternal
		{
			get
			{
				return this.HasParameterizedCreator || this._parameterizedCreator != null || this._parameterizedConstructor != null;
			}
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x00056224 File Offset: 0x00054424
		[NullableContext(1)]
		public JsonArrayContract(Type underlyingType) : base(underlyingType)
		{
			this.ContractType = JsonContractType.Array;
			this.IsArray = (base.CreatedType.IsArray || (this.NonNullableUnderlyingType.IsGenericType() && this.NonNullableUnderlyingType.GetGenericTypeDefinition().FullName == "System.Linq.EmptyPartition`1"));
			bool canDeserialize;
			Type type;
			if (this.IsArray)
			{
				this.CollectionItemType = ReflectionUtils.GetCollectionItemType(base.UnderlyingType);
				this.IsReadOnlyOrFixedSize = true;
				this._genericCollectionDefinitionType = typeof(List<>).MakeGenericType(new Type[]
				{
					this.CollectionItemType
				});
				canDeserialize = true;
				this.IsMultidimensionalArray = (base.CreatedType.IsArray && base.UnderlyingType.GetArrayRank() > 1);
			}
			else if (typeof(IList).IsAssignableFrom(this.NonNullableUnderlyingType))
			{
				if (ReflectionUtils.ImplementsGenericDefinition(this.NonNullableUnderlyingType, typeof(ICollection<>), out this._genericCollectionDefinitionType))
				{
					this.CollectionItemType = this._genericCollectionDefinitionType.GetGenericArguments()[0];
				}
				else
				{
					this.CollectionItemType = ReflectionUtils.GetCollectionItemType(this.NonNullableUnderlyingType);
				}
				if (this.NonNullableUnderlyingType == typeof(IList))
				{
					base.CreatedType = typeof(List<object>);
				}
				if (this.CollectionItemType != null)
				{
					this._parameterizedConstructor = CollectionUtils.ResolveEnumerableCollectionConstructor(this.NonNullableUnderlyingType, this.CollectionItemType);
				}
				this.IsReadOnlyOrFixedSize = ReflectionUtils.InheritsGenericDefinition(this.NonNullableUnderlyingType, typeof(ReadOnlyCollection<>));
				canDeserialize = true;
			}
			else if (ReflectionUtils.ImplementsGenericDefinition(this.NonNullableUnderlyingType, typeof(ICollection<>), out this._genericCollectionDefinitionType))
			{
				this.CollectionItemType = this._genericCollectionDefinitionType.GetGenericArguments()[0];
				if (ReflectionUtils.IsGenericDefinition(this.NonNullableUnderlyingType, typeof(ICollection<>)) || ReflectionUtils.IsGenericDefinition(this.NonNullableUnderlyingType, typeof(IList<>)))
				{
					base.CreatedType = typeof(List<>).MakeGenericType(new Type[]
					{
						this.CollectionItemType
					});
				}
				if (ReflectionUtils.IsGenericDefinition(this.NonNullableUnderlyingType, typeof(ISet<>)))
				{
					base.CreatedType = typeof(HashSet<>).MakeGenericType(new Type[]
					{
						this.CollectionItemType
					});
				}
				this._parameterizedConstructor = CollectionUtils.ResolveEnumerableCollectionConstructor(this.NonNullableUnderlyingType, this.CollectionItemType);
				canDeserialize = true;
				this.ShouldCreateWrapper = 1;
			}
			else if (ReflectionUtils.ImplementsGenericDefinition(this.NonNullableUnderlyingType, typeof(IEnumerable<>), out type))
			{
				this.CollectionItemType = type.GetGenericArguments()[0];
				if (ReflectionUtils.IsGenericDefinition(base.UnderlyingType, typeof(IEnumerable<>)))
				{
					base.CreatedType = typeof(List<>).MakeGenericType(new Type[]
					{
						this.CollectionItemType
					});
				}
				this._parameterizedConstructor = CollectionUtils.ResolveEnumerableCollectionConstructor(this.NonNullableUnderlyingType, this.CollectionItemType);
				this.StoreFSharpListCreatorIfNecessary(this.NonNullableUnderlyingType);
				if (this.NonNullableUnderlyingType.IsGenericType() && this.NonNullableUnderlyingType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
				{
					this._genericCollectionDefinitionType = type;
					this.IsReadOnlyOrFixedSize = false;
					this.ShouldCreateWrapper = 0;
					canDeserialize = true;
				}
				else
				{
					this._genericCollectionDefinitionType = typeof(List<>).MakeGenericType(new Type[]
					{
						this.CollectionItemType
					});
					this.IsReadOnlyOrFixedSize = true;
					this.ShouldCreateWrapper = 1;
					canDeserialize = this.HasParameterizedCreatorInternal;
				}
			}
			else
			{
				canDeserialize = false;
				this.ShouldCreateWrapper = 1;
			}
			this.CanDeserialize = canDeserialize;
			Type createdType;
			ObjectConstructor<object> parameterizedCreator;
			if (this.CollectionItemType != null && ImmutableCollectionsUtils.TryBuildImmutableForArrayContract(this.NonNullableUnderlyingType, this.CollectionItemType, out createdType, out parameterizedCreator))
			{
				base.CreatedType = createdType;
				this._parameterizedCreator = parameterizedCreator;
				this.IsReadOnlyOrFixedSize = true;
				this.CanDeserialize = true;
			}
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x00056644 File Offset: 0x00054844
		[NullableContext(1)]
		internal IWrappedCollection CreateWrapper(object list)
		{
			if (this._genericWrapperCreator == null)
			{
				this._genericWrapperType = typeof(CollectionWrapper<>).MakeGenericType(new Type[]
				{
					this.CollectionItemType
				});
				Type type;
				if (ReflectionUtils.InheritsGenericDefinition(this._genericCollectionDefinitionType, typeof(List<>)) || this._genericCollectionDefinitionType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
				{
					type = typeof(ICollection<>).MakeGenericType(new Type[]
					{
						this.CollectionItemType
					});
				}
				else
				{
					type = this._genericCollectionDefinitionType;
				}
				ConstructorInfo constructor = this._genericWrapperType.GetConstructor(new Type[]
				{
					type
				});
				this._genericWrapperCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(constructor);
			}
			return (IWrappedCollection)this._genericWrapperCreator(new object[]
			{
				list
			});
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0005672C File Offset: 0x0005492C
		[NullableContext(1)]
		internal IList CreateTemporaryCollection()
		{
			if (this._genericTemporaryCollectionCreator == null)
			{
				Type type = (this.IsMultidimensionalArray || this.CollectionItemType == null) ? typeof(object) : this.CollectionItemType;
				Type type2 = typeof(List<>).MakeGenericType(new Type[]
				{
					type
				});
				this._genericTemporaryCollectionCreator = JsonTypeReflector.ReflectionDelegateFactory.CreateDefaultConstructor<object>(type2);
			}
			return (IList)this._genericTemporaryCollectionCreator();
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x000567B8 File Offset: 0x000549B8
		[NullableContext(1)]
		private void StoreFSharpListCreatorIfNecessary(Type underlyingType)
		{
			if (!this.HasParameterizedCreatorInternal && underlyingType.Name == "FSharpList`1")
			{
				FSharpUtils.EnsureInitialized(underlyingType.Assembly());
				this._parameterizedCreator = FSharpUtils.Instance.CreateSeq(this.CollectionItemType);
			}
		}

		// Token: 0x040007A6 RID: 1958
		private readonly Type _genericCollectionDefinitionType;

		// Token: 0x040007A7 RID: 1959
		private Type _genericWrapperType;

		// Token: 0x040007A8 RID: 1960
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private ObjectConstructor<object> _genericWrapperCreator;

		// Token: 0x040007A9 RID: 1961
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private Func<object> _genericTemporaryCollectionCreator;

		// Token: 0x040007AD RID: 1965
		private readonly ConstructorInfo _parameterizedConstructor;

		// Token: 0x040007AE RID: 1966
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private ObjectConstructor<object> _parameterizedCreator;

		// Token: 0x040007AF RID: 1967
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private ObjectConstructor<object> _overrideCreator;
	}
}
