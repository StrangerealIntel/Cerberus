using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;

// Token: 0x02000001 RID: 1
internal class <Module>
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	static <Module>()
	{
		<Module>.smethod_31();
		<Module>.smethod_2();
		<Module>.smethod_0();
		Class3.smethod_8();
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00004024 File Offset: 0x00002224
	private static void smethod_0()
	{
		string string_ = "COR";
		Type type_ = <Module>.smethod_3(typeof(Environment).TypeHandle);
		MethodInfo methodInfo = <Module>.smethod_5(type_, "GetEnvironmentVariable", new Type[]
		{
			<Module>.smethod_4(typeof(string).TypeHandle)
		});
		if (methodInfo != null && <Module>.smethod_8("1", <Module>.smethod_7(methodInfo, null, new object[]
		{
			<Module>.smethod_6(string_, "_ENABLE_PROFILING")
		})))
		{
			<Module>.smethod_9(null);
		}
		Thread thread_ = <Module>.smethod_10(new ParameterizedThreadStart(<Module>.smethod_1));
		<Module>.smethod_11(thread_, true);
		<Module>.smethod_12(thread_, null);
	}

	// Token: 0x06000003 RID: 3 RVA: 0x000040BC File Offset: 0x000022BC
	private static void smethod_1(object object_0)
	{
		Thread thread = object_0 as Thread;
		if (thread == null)
		{
			thread = <Module>.smethod_13(new ParameterizedThreadStart(<Module>.smethod_1));
			<Module>.smethod_14(thread, true);
			<Module>.smethod_16(thread, <Module>.smethod_15());
			<Module>.smethod_17(500);
		}
		for (;;)
		{
			if (<Module>.smethod_18() || <Module>.smethod_19())
			{
				<Module>.smethod_20(null);
			}
			if (!<Module>.smethod_21(thread))
			{
				<Module>.smethod_22(null);
			}
			<Module>.smethod_23(1000);
		}
	}

	// Token: 0x06000004 RID: 4
	[DllImport("kernel32.dll")]
	internal unsafe static extern bool VirtualProtect(byte* pByte_0, int int_0, uint uint_0, ref uint uint_1);

	// Token: 0x06000005 RID: 5 RVA: 0x00004134 File Offset: 0x00002334
	internal unsafe static void smethod_2()
	{
		Module module_ = <Module>.smethod_25(<Module>.smethod_24(typeof(<Module>).TypeHandle));
		byte* ptr = (byte*)((void*)<Module>.smethod_26(module_));
		byte* ptr2 = ptr + 60;
		ptr2 = ptr + *(uint*)ptr2;
		ptr2 += 6;
		ushort num = *(ushort*)ptr2;
		ptr2 += 14;
		ushort num2 = *(ushort*)ptr2;
		ptr2 = ptr2 + 4 + num2;
		byte* ptr3 = stackalloc byte[(UIntPtr)11];
		uint num5;
		if (<Module>.smethod_28(<Module>.smethod_27(module_), 0) == '<')
		{
			uint num3 = *(uint*)(ptr2 - 16);
			uint num4 = *(uint*)(ptr2 - 120);
			uint[] array = new uint[(int)num];
			uint[] array2 = new uint[(int)num];
			uint[] array3 = new uint[(int)num];
			for (int i = 0; i < (int)num; i++)
			{
				<Module>.VirtualProtect(ptr2, 8, 64u, ref num5);
				<Module>.smethod_30(new byte[8], 0, (IntPtr)((void*)ptr2), 8);
				array[i] = *(uint*)(ptr2 + 12);
				array2[i] = *(uint*)(ptr2 + 8);
				array3[i] = *(uint*)(ptr2 + 20);
				ptr2 += 40;
			}
			if (num4 != 0u)
			{
				for (int j = 0; j < (int)num; j++)
				{
					if (array[j] <= num4 && num4 < array[j] + array2[j])
					{
						num4 = num4 - array[j] + array3[j];
						break;
					}
				}
				byte* ptr4 = ptr + num4;
				uint num6 = *(uint*)ptr4;
				for (int k = 0; k < (int)num; k++)
				{
					if (array[k] <= num6 && num6 < array[k] + array2[k])
					{
						num6 = num6 - array[k] + array3[k];
						IL_15E:
						byte* ptr5 = ptr + num6;
						uint num7 = *(uint*)(ptr4 + 12);
						for (int l = 0; l < (int)num; l++)
						{
							if (array[l] <= num7 && num7 < array[l] + array2[l])
							{
								num7 = num7 - array[l] + array3[l];
								break;
							}
						}
						uint num8 = *(uint*)ptr5 + 2u;
						for (int m = 0; m < (int)num; m++)
						{
							if (array[m] <= num8 && num8 < array[m] + array2[m])
							{
								num8 = num8 - array[m] + array3[m];
								IL_1E8:
								<Module>.VirtualProtect(ptr + num7, 11, 64u, ref num5);
								*(int*)ptr3 = 1818522734;
								*(int*)(ptr3 + 4) = 1818504812;
								*(short*)(ptr3 + 8) = 108;
								ptr3[10] = 0;
								for (int n = 0; n < 11; n++)
								{
									(ptr + num7)[n] = ptr3[n];
								}
								<Module>.VirtualProtect(ptr + num8, 11, 64u, ref num5);
								*(int*)ptr3 = 1866691662;
								*(int*)(ptr3 + 4) = 1852404846;
								*(short*)(ptr3 + 8) = 25973;
								ptr3[10] = 0;
								for (int num9 = 0; num9 < 11; num9++)
								{
									(ptr + num8)[num9] = ptr3[num9];
								}
								goto IL_28F;
							}
						}
						goto IL_1E8;
					}
				}
				goto IL_15E;
			}
			IL_28F:
			for (int num10 = 0; num10 < (int)num; num10++)
			{
				if (array[num10] <= num3 && num3 < array[num10] + array2[num10])
				{
					num3 = num3 - array[num10] + array3[num10];
					break;
				}
			}
			byte* ptr6 = ptr + num3;
			<Module>.VirtualProtect(ptr6, 72, 64u, ref num5);
			uint num11 = *(uint*)(ptr6 + 8);
			for (int num12 = 0; num12 < (int)num; num12++)
			{
				if (array[num12] <= num11 && num11 < array[num12] + array2[num12])
				{
					num11 = num11 - array[num12] + array3[num12];
					IL_31F:
					*(int*)ptr6 = 0;
					*(int*)(ptr6 + 4) = 0;
					*(int*)(ptr6 + 8) = 0;
					*(int*)(ptr6 + 12) = 0;
					byte* ptr7 = ptr + num11;
					<Module>.VirtualProtect(ptr7, 4, 64u, ref num5);
					*(int*)ptr7 = 0;
					ptr7 += 12;
					ptr7 += *(uint*)ptr7;
					ptr7 = (ptr7 + 7L & -4L);
					ptr7 += 2;
					ushort num13 = (ushort)(*ptr7);
					ptr7 += 2;
					for (int num14 = 0; num14 < (int)num13; num14++)
					{
						<Module>.VirtualProtect(ptr7, 8, 64u, ref num5);
						ptr7 += 4;
						ptr7 += 4;
						for (int num15 = 0; num15 < 8; num15++)
						{
							<Module>.VirtualProtect(ptr7, 4, 64u, ref num5);
							*ptr7 = 0;
							ptr7++;
							if (*ptr7 == 0)
							{
								ptr7 += 3;
								break;
							}
							*ptr7 = 0;
							ptr7++;
							if (*ptr7 == 0)
							{
								ptr7 += 2;
								break;
							}
							*ptr7 = 0;
							ptr7++;
							if (*ptr7 == 0)
							{
								ptr7++;
								break;
							}
							*ptr7 = 0;
							ptr7++;
						}
					}
					return;
				}
			}
			goto IL_31F;
		}
		byte* ptr8 = ptr + *(uint*)(ptr2 - 16);
		if (*(uint*)(ptr2 - 120) != 0u)
		{
			byte* ptr9 = ptr + *(uint*)(ptr2 - 120);
			byte* ptr10 = ptr + *(uint*)ptr9;
			byte* ptr11 = ptr + *(uint*)(ptr9 + 12);
			byte* ptr12 = ptr + *(uint*)ptr10 + 2;
			<Module>.VirtualProtect(ptr11, 11, 64u, ref num5);
			*(int*)ptr3 = 1818522734;
			*(int*)(ptr3 + 4) = 1818504812;
			*(short*)(ptr3 + 8) = 108;
			ptr3[10] = 0;
			for (int num16 = 0; num16 < 11; num16++)
			{
				ptr11[num16] = ptr3[num16];
			}
			<Module>.VirtualProtect(ptr12, 11, 64u, ref num5);
			*(int*)ptr3 = 1866691662;
			*(int*)(ptr3 + 4) = 1852404846;
			*(short*)(ptr3 + 8) = 25973;
			ptr3[10] = 0;
			for (int num17 = 0; num17 < 11; num17++)
			{
				ptr12[num17] = ptr3[num17];
			}
		}
		for (int num18 = 0; num18 < (int)num; num18++)
		{
			<Module>.VirtualProtect(ptr2, 8, 64u, ref num5);
			<Module>.smethod_29(new byte[8], 0, (IntPtr)((void*)ptr2), 8);
			ptr2 += 40;
		}
		<Module>.VirtualProtect(ptr8, 72, 64u, ref num5);
		byte* ptr13 = ptr + *(uint*)(ptr8 + 8);
		*(int*)ptr8 = 0;
		*(int*)(ptr8 + 4) = 0;
		*(int*)(ptr8 + 8) = 0;
		*(int*)(ptr8 + 12) = 0;
		<Module>.VirtualProtect(ptr13, 4, 64u, ref num5);
		*(int*)ptr13 = 0;
		ptr13 += 12;
		ptr13 += *(uint*)ptr13;
		ptr13 = (ptr13 + 7L & -4L);
		ptr13 += 2;
		ushort num19 = (ushort)(*ptr13);
		ptr13 += 2;
		int num20 = 0;
		IL_65E:
		while (num20 < (int)num19)
		{
			<Module>.VirtualProtect(ptr13, 8, 64u, ref num5);
			ptr13 += 4;
			ptr13 += 4;
			int num21 = 0;
			while (num21 < 8)
			{
				<Module>.VirtualProtect(ptr13, 4, 64u, ref num5);
				*ptr13 = 0;
				ptr13++;
				if (*ptr13 != 0)
				{
					*ptr13 = 0;
					ptr13++;
					if (*ptr13 != 0)
					{
						*ptr13 = 0;
						ptr13++;
						if (*ptr13 != 0)
						{
							*ptr13 = 0;
							ptr13++;
							num21++;
							continue;
						}
						ptr13++;
					}
					else
					{
						ptr13 += 2;
					}
				}
				else
				{
					ptr13 += 3;
				}
				IL_658:
				num20++;
				goto IL_65E;
			}
			goto IL_658;
		}
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00002068 File Offset: 0x00000268
	static Type smethod_3(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00002068 File Offset: 0x00000268
	static Type smethod_4(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00002070 File Offset: 0x00000270
	static MethodInfo smethod_5(Type type_0, string string_0, Type[] type_1)
	{
		return type_0.GetMethod(string_0, type_1);
	}

	// Token: 0x06000009 RID: 9 RVA: 0x0000207A File Offset: 0x0000027A
	static string smethod_6(string string_0, string string_1)
	{
		return string_0 + string_1;
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002083 File Offset: 0x00000283
	static object smethod_7(MethodBase methodBase_0, object object_0, object[] object_1)
	{
		return methodBase_0.Invoke(object_0, object_1);
	}

	// Token: 0x0600000B RID: 11 RVA: 0x0000208D File Offset: 0x0000028D
	static bool smethod_8(object object_0, object object_1)
	{
		return object_0.Equals(object_1);
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00002096 File Offset: 0x00000296
	static void smethod_9(string string_0)
	{
		Environment.FailFast(string_0);
	}

	// Token: 0x0600000D RID: 13 RVA: 0x0000209E File Offset: 0x0000029E
	static Thread smethod_10(ParameterizedThreadStart parameterizedThreadStart_0)
	{
		return new Thread(parameterizedThreadStart_0);
	}

	// Token: 0x0600000E RID: 14 RVA: 0x000020A6 File Offset: 0x000002A6
	static void smethod_11(Thread thread_0, bool bool_0)
	{
		thread_0.IsBackground = bool_0;
	}

	// Token: 0x0600000F RID: 15 RVA: 0x000020AF File Offset: 0x000002AF
	static void smethod_12(Thread thread_0, object object_0)
	{
		thread_0.Start(object_0);
	}

	// Token: 0x06000010 RID: 16 RVA: 0x0000209E File Offset: 0x0000029E
	static Thread smethod_13(ParameterizedThreadStart parameterizedThreadStart_0)
	{
		return new Thread(parameterizedThreadStart_0);
	}

	// Token: 0x06000011 RID: 17 RVA: 0x000020A6 File Offset: 0x000002A6
	static void smethod_14(Thread thread_0, bool bool_0)
	{
		thread_0.IsBackground = bool_0;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x000020B8 File Offset: 0x000002B8
	static Thread smethod_15()
	{
		return Thread.CurrentThread;
	}

	// Token: 0x06000013 RID: 19 RVA: 0x000020AF File Offset: 0x000002AF
	static void smethod_16(Thread thread_0, object object_0)
	{
		thread_0.Start(object_0);
	}

	// Token: 0x06000014 RID: 20 RVA: 0x000020BF File Offset: 0x000002BF
	static void smethod_17(int int_0)
	{
		Thread.Sleep(int_0);
	}

	// Token: 0x06000015 RID: 21 RVA: 0x000020C7 File Offset: 0x000002C7
	static bool smethod_18()
	{
		return Debugger.IsAttached;
	}

	// Token: 0x06000016 RID: 22 RVA: 0x000020CE File Offset: 0x000002CE
	static bool smethod_19()
	{
		return Debugger.IsLogging();
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00002096 File Offset: 0x00000296
	static void smethod_20(string string_0)
	{
		Environment.FailFast(string_0);
	}

	// Token: 0x06000018 RID: 24 RVA: 0x000020D5 File Offset: 0x000002D5
	static bool smethod_21(Thread thread_0)
	{
		return thread_0.IsAlive;
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00002096 File Offset: 0x00000296
	static void smethod_22(string string_0)
	{
		Environment.FailFast(string_0);
	}

	// Token: 0x0600001A RID: 26 RVA: 0x000020BF File Offset: 0x000002BF
	static void smethod_23(int int_0)
	{
		Thread.Sleep(int_0);
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00002068 File Offset: 0x00000268
	static Type smethod_24(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	// Token: 0x0600001C RID: 28 RVA: 0x000020DD File Offset: 0x000002DD
	static Module smethod_25(Type type_0)
	{
		return type_0.Module;
	}

	// Token: 0x0600001D RID: 29 RVA: 0x000020E5 File Offset: 0x000002E5
	static IntPtr smethod_26(Module module_0)
	{
		return Marshal.GetHINSTANCE(module_0);
	}

	// Token: 0x0600001E RID: 30 RVA: 0x000020ED File Offset: 0x000002ED
	static string smethod_27(Module module_0)
	{
		return module_0.FullyQualifiedName;
	}

	// Token: 0x0600001F RID: 31 RVA: 0x000020F5 File Offset: 0x000002F5
	static char smethod_28(string string_0, int int_0)
	{
		return string_0[int_0];
	}

	// Token: 0x06000020 RID: 32 RVA: 0x000020FE File Offset: 0x000002FE
	static void smethod_29(byte[] byte_0, int int_0, IntPtr intptr_0, int int_1)
	{
		Marshal.Copy(byte_0, int_0, intptr_0, int_1);
	}

	// Token: 0x06000021 RID: 33 RVA: 0x000020FE File Offset: 0x000002FE
	static void smethod_30(byte[] byte_0, int int_0, IntPtr intptr_0, int int_1)
	{
		Marshal.Copy(byte_0, int_0, intptr_0, int_1);
	}

	// Token: 0x06000022 RID: 34
	[DllImport("kernel32.dll", EntryPoint = "VirtualProtect")]
	internal static extern bool VirtualProtect_1(IntPtr intptr_0, uint uint_0, uint uint_1, ref uint uint_2);

	// Token: 0x06000023 RID: 35 RVA: 0x000047A8 File Offset: 0x000029A8
	internal unsafe static void smethod_31()
	{
		Module module = typeof(<Module>).Module;
		string fullyQualifiedName = module.FullyQualifiedName;
		bool flag = fullyQualifiedName.Length > 0 && fullyQualifiedName[0] == '<';
		byte* ptr = (byte*)((void*)Marshal.GetHINSTANCE(module));
		byte* ptr2 = ptr + *(uint*)(ptr + 60);
		ushort num = *(ushort*)(ptr2 + 6);
		ushort num2 = *(ushort*)(ptr2 + 20);
		uint* ptr3 = null;
		uint num3 = 0u;
		uint* ptr4 = (uint*)(ptr2 + 24 + num2);
		uint num4 = 1222311860u;
		uint num5 = 2639738772u;
		uint num6 = 1838251433u;
		uint num7 = 3652249165u;
		for (int i = 0; i < (int)num; i++)
		{
			uint num8 = *(ptr4++) * *(ptr4++);
			if (num8 == 2080009944u)
			{
				ptr3 = (uint*)(ptr + (flag ? ptr4[3] : ptr4[1]) / 4u);
				num3 = (flag ? ptr4[2] : (*ptr4)) >> 2;
			}
			else if (num8 != 0u)
			{
				uint* ptr5 = (uint*)(ptr + (flag ? ptr4[3] : ptr4[1]) / 4u);
				uint num9 = ptr4[2] >> 2;
				for (uint num10 = 0u; num10 < num9; num10 += 1u)
				{
					uint num11 = (num4 ^ *(ptr5++)) + num5 + num6 * num7;
					num4 = num5;
					num5 = num7;
					num7 = num11;
				}
			}
			ptr4 += 8;
		}
		uint[] array = new uint[16];
		uint[] array2 = new uint[16];
		for (int j = 0; j < 16; j++)
		{
			array[j] = num7;
			array2[j] = num5;
			num4 = (num5 >> 5 | num5 << 27);
			num5 = (num6 >> 3 | num6 << 29);
			num6 = (num7 >> 7 | num7 << 25);
			num7 = (num4 >> 11 | num4 << 21);
		}
		array[0] = (array[0] ^ array2[0]);
		array[1] = array[1] * array2[1];
		array[2] = array[2] + array2[2];
		array[3] = (array[3] ^ array2[3]);
		array[4] = array[4] * array2[4];
		array[5] = array[5] + array2[5];
		array[6] = (array[6] ^ array2[6]);
		array[7] = array[7] * array2[7];
		array[8] = array[8] + array2[8];
		array[9] = (array[9] ^ array2[9]);
		array[10] = array[10] * array2[10];
		array[11] = array[11] + array2[11];
		array[12] = (array[12] ^ array2[12]);
		array[13] = array[13] * array2[13];
		array[14] = array[14] + array2[14];
		array[15] = (array[15] ^ array2[15]);
		uint num12 = 64u;
		<Module>.VirtualProtect_1((IntPtr)((void*)ptr3), num3 << 2, 64u, ref num12);
		if (num12 == 64u)
		{
			return;
		}
		uint num13 = 0u;
		for (uint num14 = 0u; num14 < num3; num14 += 1u)
		{
			*ptr3 ^= array[(int)((UIntPtr)(num13 & 15u))];
			array[(int)((UIntPtr)(num13 & 15u))] = (array[(int)((UIntPtr)(num13 & 15u))] ^ *(ptr3++)) + 1035675673u;
			num13 += 1u;
		}
	}
}
