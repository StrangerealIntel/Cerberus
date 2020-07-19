using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000175 RID: 373
	[NullableContext(1)]
	[Nullable(0)]
	internal class FSharpUtils
	{
		// Token: 0x06000DAB RID: 3499 RVA: 0x0004FC50 File Offset: 0x0004DE50
		private FSharpUtils(Assembly fsharpCoreAssembly)
		{
			this.FSharpCoreAssembly = fsharpCoreAssembly;
			Type type = fsharpCoreAssembly.GetType("Microsoft.FSharp.Reflection.FSharpType");
			MethodInfo methodWithNonPublicFallback = FSharpUtils.GetMethodWithNonPublicFallback(type, "IsUnion", BindingFlags.Static | BindingFlags.Public);
			this.IsUnion = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodWithNonPublicFallback);
			MethodInfo methodWithNonPublicFallback2 = FSharpUtils.GetMethodWithNonPublicFallback(type, "GetUnionCases", BindingFlags.Static | BindingFlags.Public);
			this.GetUnionCases = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodWithNonPublicFallback2);
			Type type2 = fsharpCoreAssembly.GetType("Microsoft.FSharp.Reflection.FSharpValue");
			this.PreComputeUnionTagReader = FSharpUtils.CreateFSharpFuncCall(type2, "PreComputeUnionTagReader");
			this.PreComputeUnionReader = FSharpUtils.CreateFSharpFuncCall(type2, "PreComputeUnionReader");
			this.PreComputeUnionConstructor = FSharpUtils.CreateFSharpFuncCall(type2, "PreComputeUnionConstructor");
			Type type3 = fsharpCoreAssembly.GetType("Microsoft.FSharp.Reflection.UnionCaseInfo");
			this.GetUnionCaseInfoName = JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(type3.GetProperty("Name"));
			this.GetUnionCaseInfoTag = JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(type3.GetProperty("Tag"));
			this.GetUnionCaseInfoDeclaringType = JsonTypeReflector.ReflectionDelegateFactory.CreateGet<object>(type3.GetProperty("DeclaringType"));
			this.GetUnionCaseInfoFields = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(type3.GetMethod("GetFields"));
			Type type4 = fsharpCoreAssembly.GetType("Microsoft.FSharp.Collections.ListModule");
			this._ofSeq = type4.GetMethod("OfSeq");
			this._mapType = fsharpCoreAssembly.GetType("Microsoft.FSharp.Collections.FSharpMap`2");
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x0004FDA0 File Offset: 0x0004DFA0
		public static FSharpUtils Instance
		{
			get
			{
				return FSharpUtils._instance;
			}
		}

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x0004FDA8 File Offset: 0x0004DFA8
		// (set) Token: 0x06000DAE RID: 3502 RVA: 0x0004FDB0 File Offset: 0x0004DFB0
		public Assembly FSharpCoreAssembly { get; private set; }

		// Token: 0x170002EA RID: 746
		// (get) Token: 0x06000DAF RID: 3503 RVA: 0x0004FDBC File Offset: 0x0004DFBC
		// (set) Token: 0x06000DB0 RID: 3504 RVA: 0x0004FDC4 File Offset: 0x0004DFC4
		[Nullable(new byte[]
		{
			1,
			2,
			1
		})]
		public MethodCall<object, object> IsUnion { [return: Nullable(new byte[]
		{
			1,
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			1,
			2,
			1
		})] private set; }

		// Token: 0x170002EB RID: 747
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x0004FDD0 File Offset: 0x0004DFD0
		// (set) Token: 0x06000DB2 RID: 3506 RVA: 0x0004FDD8 File Offset: 0x0004DFD8
		[Nullable(new byte[]
		{
			1,
			2,
			1
		})]
		public MethodCall<object, object> GetUnionCases { [return: Nullable(new byte[]
		{
			1,
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			1,
			2,
			1
		})] private set; }

		// Token: 0x170002EC RID: 748
		// (get) Token: 0x06000DB3 RID: 3507 RVA: 0x0004FDE4 File Offset: 0x0004DFE4
		// (set) Token: 0x06000DB4 RID: 3508 RVA: 0x0004FDEC File Offset: 0x0004DFEC
		[Nullable(new byte[]
		{
			1,
			2,
			1
		})]
		public MethodCall<object, object> PreComputeUnionTagReader { [return: Nullable(new byte[]
		{
			1,
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			1,
			2,
			1
		})] private set; }

		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x0004FDF8 File Offset: 0x0004DFF8
		// (set) Token: 0x06000DB6 RID: 3510 RVA: 0x0004FE00 File Offset: 0x0004E000
		[Nullable(new byte[]
		{
			1,
			2,
			1
		})]
		public MethodCall<object, object> PreComputeUnionReader { [return: Nullable(new byte[]
		{
			1,
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			1,
			2,
			1
		})] private set; }

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000DB7 RID: 3511 RVA: 0x0004FE0C File Offset: 0x0004E00C
		// (set) Token: 0x06000DB8 RID: 3512 RVA: 0x0004FE14 File Offset: 0x0004E014
		[Nullable(new byte[]
		{
			1,
			2,
			1
		})]
		public MethodCall<object, object> PreComputeUnionConstructor { [return: Nullable(new byte[]
		{
			1,
			2,
			1
		})] get; [param: Nullable(new byte[]
		{
			1,
			2,
			1
		})] private set; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000DB9 RID: 3513 RVA: 0x0004FE20 File Offset: 0x0004E020
		// (set) Token: 0x06000DBA RID: 3514 RVA: 0x0004FE28 File Offset: 0x0004E028
		public Func<object, object> GetUnionCaseInfoDeclaringType { get; private set; }

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000DBB RID: 3515 RVA: 0x0004FE34 File Offset: 0x0004E034
		// (set) Token: 0x06000DBC RID: 3516 RVA: 0x0004FE3C File Offset: 0x0004E03C
		public Func<object, object> GetUnionCaseInfoName { get; private set; }

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000DBD RID: 3517 RVA: 0x0004FE48 File Offset: 0x0004E048
		// (set) Token: 0x06000DBE RID: 3518 RVA: 0x0004FE50 File Offset: 0x0004E050
		public Func<object, object> GetUnionCaseInfoTag { get; private set; }

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000DBF RID: 3519 RVA: 0x0004FE5C File Offset: 0x0004E05C
		// (set) Token: 0x06000DC0 RID: 3520 RVA: 0x0004FE64 File Offset: 0x0004E064
		[Nullable(new byte[]
		{
			1,
			1,
			2
		})]
		public MethodCall<object, object> GetUnionCaseInfoFields { [return: Nullable(new byte[]
		{
			1,
			1,
			2
		})] get; [param: Nullable(new byte[]
		{
			1,
			1,
			2
		})] private set; }

		// Token: 0x06000DC1 RID: 3521 RVA: 0x0004FE70 File Offset: 0x0004E070
		public static void EnsureInitialized(Assembly fsharpCoreAssembly)
		{
			if (FSharpUtils._instance == null)
			{
				object @lock = FSharpUtils.Lock;
				lock (@lock)
				{
					if (FSharpUtils._instance == null)
					{
						FSharpUtils._instance = new FSharpUtils(fsharpCoreAssembly);
					}
				}
			}
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x0004FED0 File Offset: 0x0004E0D0
		private static MethodInfo GetMethodWithNonPublicFallback(Type type, string methodName, BindingFlags bindingFlags)
		{
			MethodInfo method = type.GetMethod(methodName, bindingFlags);
			if (method == null && (bindingFlags & BindingFlags.NonPublic) != BindingFlags.NonPublic)
			{
				method = type.GetMethod(methodName, bindingFlags | BindingFlags.NonPublic);
			}
			return method;
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x0004FF10 File Offset: 0x0004E110
		[return: Nullable(new byte[]
		{
			1,
			2,
			1
		})]
		private static MethodCall<object, object> CreateFSharpFuncCall(Type type, string methodName)
		{
			MethodInfo methodWithNonPublicFallback = FSharpUtils.GetMethodWithNonPublicFallback(type, methodName, BindingFlags.Static | BindingFlags.Public);
			MethodInfo method = methodWithNonPublicFallback.ReturnType.GetMethod("Invoke", BindingFlags.Instance | BindingFlags.Public);
			MethodCall<object, object> call = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(methodWithNonPublicFallback);
			MethodCall<object, object> invoke = JsonTypeReflector.ReflectionDelegateFactory.CreateMethodCall<object>(method);
			return ([Nullable(2)] object target, [Nullable(new byte[]
			{
				1,
				2
			})] object[] args) => new FSharpFunction(call(target, args), invoke);
		}

		// Token: 0x06000DC4 RID: 3524 RVA: 0x0004FF70 File Offset: 0x0004E170
		public ObjectConstructor<object> CreateSeq(Type t)
		{
			MethodInfo method = this._ofSeq.MakeGenericMethod(new Type[]
			{
				t
			});
			return JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(method);
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x0004FFA4 File Offset: 0x0004E1A4
		public ObjectConstructor<object> CreateMap(Type keyType, Type valueType)
		{
			return (ObjectConstructor<object>)typeof(FSharpUtils).GetMethod("BuildMapCreator").MakeGenericMethod(new Type[]
			{
				keyType,
				valueType
			}).Invoke(this, null);
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x0004FFE8 File Offset: 0x0004E1E8
		[NullableContext(2)]
		[return: Nullable(1)]
		public ObjectConstructor<object> BuildMapCreator<TKey, TValue>()
		{
			ConstructorInfo constructor = this._mapType.MakeGenericType(new Type[]
			{
				typeof(TKey),
				typeof(TValue)
			}).GetConstructor(new Type[]
			{
				typeof(IEnumerable<Tuple<TKey, TValue>>)
			});
			ObjectConstructor<object> ctorDelegate = JsonTypeReflector.ReflectionDelegateFactory.CreateParameterizedConstructor(constructor);
			return delegate([Nullable(new byte[]
			{
				1,
				2
			})] object[] args)
			{
				IEnumerable<Tuple<TKey, TValue>> enumerable = from kv in (IEnumerable<KeyValuePair<TKey, TValue>>)args[0]
				select new Tuple<TKey, TValue>(kv.Key, kv.Value);
				return ctorDelegate(new object[]
				{
					enumerable
				});
			};
		}

		// Token: 0x04000740 RID: 1856
		private static readonly object Lock = new object();

		// Token: 0x04000741 RID: 1857
		[Nullable(2)]
		private static FSharpUtils _instance;

		// Token: 0x04000742 RID: 1858
		private MethodInfo _ofSeq;

		// Token: 0x04000743 RID: 1859
		private Type _mapType;

		// Token: 0x0400074E RID: 1870
		public const string FSharpSetTypeName = "FSharpSet`1";

		// Token: 0x0400074F RID: 1871
		public const string FSharpListTypeName = "FSharpList`1";

		// Token: 0x04000750 RID: 1872
		public const string FSharpMapTypeName = "FSharpMap`2";
	}
}
