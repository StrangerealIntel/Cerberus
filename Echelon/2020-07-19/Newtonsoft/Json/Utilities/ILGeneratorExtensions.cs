using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000176 RID: 374
	[NullableContext(1)]
	[Nullable(0)]
	internal static class ILGeneratorExtensions
	{
		// Token: 0x06000DC8 RID: 3528 RVA: 0x0005006C File Offset: 0x0004E26C
		public static void PushInstance(this ILGenerator generator, Type type)
		{
			generator.Emit(OpCodes.Ldarg_0);
			if (type.IsValueType())
			{
				generator.Emit(OpCodes.Unbox, type);
				return;
			}
			generator.Emit(OpCodes.Castclass, type);
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x000500A0 File Offset: 0x0004E2A0
		public static void PushArrayInstance(this ILGenerator generator, int argsIndex, int arrayIndex)
		{
			generator.Emit(OpCodes.Ldarg, argsIndex);
			generator.Emit(OpCodes.Ldc_I4, arrayIndex);
			generator.Emit(OpCodes.Ldelem_Ref);
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x000500C8 File Offset: 0x0004E2C8
		public static void BoxIfNeeded(this ILGenerator generator, Type type)
		{
			if (type.IsValueType())
			{
				generator.Emit(OpCodes.Box, type);
				return;
			}
			generator.Emit(OpCodes.Castclass, type);
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x000500F0 File Offset: 0x0004E2F0
		public static void UnboxIfNeeded(this ILGenerator generator, Type type)
		{
			if (type.IsValueType())
			{
				generator.Emit(OpCodes.Unbox_Any, type);
				return;
			}
			generator.Emit(OpCodes.Castclass, type);
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x00050118 File Offset: 0x0004E318
		public static void CallMethod(this ILGenerator generator, MethodInfo methodInfo)
		{
			if (methodInfo.IsFinal || !methodInfo.IsVirtual)
			{
				generator.Emit(OpCodes.Call, methodInfo);
				return;
			}
			generator.Emit(OpCodes.Callvirt, methodInfo);
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x0005014C File Offset: 0x0004E34C
		public static void Return(this ILGenerator generator)
		{
			generator.Emit(OpCodes.Ret);
		}
	}
}
