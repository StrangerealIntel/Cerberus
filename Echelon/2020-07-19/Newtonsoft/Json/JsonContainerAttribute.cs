using System;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x02000134 RID: 308
	[NullableContext(2)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
	public abstract class JsonContainerAttribute : Attribute
	{
		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060009C5 RID: 2501 RVA: 0x0003DE24 File Offset: 0x0003C024
		// (set) Token: 0x060009C6 RID: 2502 RVA: 0x0003DE2C File Offset: 0x0003C02C
		public string Id { get; set; }

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x060009C7 RID: 2503 RVA: 0x0003DE38 File Offset: 0x0003C038
		// (set) Token: 0x060009C8 RID: 2504 RVA: 0x0003DE40 File Offset: 0x0003C040
		public string Title { get; set; }

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x0003DE4C File Offset: 0x0003C04C
		// (set) Token: 0x060009CA RID: 2506 RVA: 0x0003DE54 File Offset: 0x0003C054
		public string Description { get; set; }

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x0003DE60 File Offset: 0x0003C060
		// (set) Token: 0x060009CC RID: 2508 RVA: 0x0003DE68 File Offset: 0x0003C068
		public Type ItemConverterType { get; set; }

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x0003DE74 File Offset: 0x0003C074
		// (set) Token: 0x060009CE RID: 2510 RVA: 0x0003DE7C File Offset: 0x0003C07C
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public object[] ItemConverterParameters { [return: Nullable(new byte[]
		{
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			2,
			1
		})] set; }

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x0003DE88 File Offset: 0x0003C088
		// (set) Token: 0x060009D0 RID: 2512 RVA: 0x0003DE90 File Offset: 0x0003C090
		public Type NamingStrategyType
		{
			get
			{
				return this._namingStrategyType;
			}
			set
			{
				this._namingStrategyType = value;
				this.NamingStrategyInstance = null;
			}
		}

		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x0003DEA0 File Offset: 0x0003C0A0
		// (set) Token: 0x060009D2 RID: 2514 RVA: 0x0003DEA8 File Offset: 0x0003C0A8
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public object[] NamingStrategyParameters
		{
			[return: Nullable(new byte[]
			{
				2,
				1
			})]
			get
			{
				return this._namingStrategyParameters;
			}
			[param: Nullable(new byte[]
			{
				2,
				1
			})]
			set
			{
				this._namingStrategyParameters = value;
				this.NamingStrategyInstance = null;
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x0003DEB8 File Offset: 0x0003C0B8
		// (set) Token: 0x060009D4 RID: 2516 RVA: 0x0003DEC0 File Offset: 0x0003C0C0
		internal NamingStrategy NamingStrategyInstance { get; set; }

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060009D5 RID: 2517 RVA: 0x0003DECC File Offset: 0x0003C0CC
		// (set) Token: 0x060009D6 RID: 2518 RVA: 0x0003DEDC File Offset: 0x0003C0DC
		public bool IsReference
		{
			get
			{
				return this._isReference.GetValueOrDefault();
			}
			set
			{
				this._isReference = new bool?(value);
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060009D7 RID: 2519 RVA: 0x0003DEEC File Offset: 0x0003C0EC
		// (set) Token: 0x060009D8 RID: 2520 RVA: 0x0003DEFC File Offset: 0x0003C0FC
		public bool ItemIsReference
		{
			get
			{
				return this._itemIsReference.GetValueOrDefault();
			}
			set
			{
				this._itemIsReference = new bool?(value);
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x0003DF0C File Offset: 0x0003C10C
		// (set) Token: 0x060009DA RID: 2522 RVA: 0x0003DF1C File Offset: 0x0003C11C
		public ReferenceLoopHandling ItemReferenceLoopHandling
		{
			get
			{
				return this._itemReferenceLoopHandling.GetValueOrDefault();
			}
			set
			{
				this._itemReferenceLoopHandling = new ReferenceLoopHandling?(value);
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x0003DF2C File Offset: 0x0003C12C
		// (set) Token: 0x060009DC RID: 2524 RVA: 0x0003DF3C File Offset: 0x0003C13C
		public TypeNameHandling ItemTypeNameHandling
		{
			get
			{
				return this._itemTypeNameHandling.GetValueOrDefault();
			}
			set
			{
				this._itemTypeNameHandling = new TypeNameHandling?(value);
			}
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x0003DF4C File Offset: 0x0003C14C
		protected JsonContainerAttribute()
		{
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x0003DF54 File Offset: 0x0003C154
		[NullableContext(1)]
		protected JsonContainerAttribute(string id)
		{
			this.Id = id;
		}

		// Token: 0x040005A8 RID: 1448
		internal bool? _isReference;

		// Token: 0x040005A9 RID: 1449
		internal bool? _itemIsReference;

		// Token: 0x040005AA RID: 1450
		internal ReferenceLoopHandling? _itemReferenceLoopHandling;

		// Token: 0x040005AB RID: 1451
		internal TypeNameHandling? _itemTypeNameHandling;

		// Token: 0x040005AC RID: 1452
		private Type _namingStrategyType;

		// Token: 0x040005AD RID: 1453
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private object[] _namingStrategyParameters;
	}
}
