using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic.CompilerServices;

// Token: 0x02000013 RID: 19
internal class Dynamic
{
	// Token: 0x06000062 RID: 98 RVA: 0x00004588 File Offset: 0x00003588
	public static T CreateApi<T>(string name, string method)
	{
		IntPtr notMatchAddress;
		return (T)((object)Marshal.GetDelegateForFunctionPointer(Dynamic.GetProcAddress(Dynamic.GetInternalModuleBaseAddr(name, notMatchAddress), method), typeof(T)));
	}

	// Token: 0x06000063 RID: 99 RVA: 0x000045BC File Offset: 0x000035BC
	public static IntPtr GetInternalModuleBaseAddr(string ModuleName, IntPtr NotMatchAddress)
	{
		IntPtr intPtr = 0;
		if (!ModuleName.Contains(".dll"))
		{
			ModuleName += ".dll";
		}
		try
		{
			foreach (object obj in Process.GetCurrentProcess().Modules)
			{
				ProcessModule processModule = (ProcessModule)obj;
				if (Operators.CompareString(processModule.ModuleName.ToLower(), ModuleName, false) == 0)
				{
					IntPtr value;
					if (NotMatchAddress == value)
					{
						return processModule.BaseAddress;
					}
					if (!(processModule.BaseAddress == NotMatchAddress))
					{
						return processModule.BaseAddress;
					}
				}
			}
		}
		finally
		{
			IEnumerator enumerator;
			if (enumerator is IDisposable)
			{
				(enumerator as IDisposable).Dispose();
			}
		}
		return Dynamic.LoadLibrary(ModuleName);
	}

	// Token: 0x06000064 RID: 100 RVA: 0x0000468C File Offset: 0x0000368C
	public static byte[] ReadByteArray(IntPtr Address, int Size)
	{
		checked
		{
			byte[] array = new byte[Size - 1 + 1];
			int num = 0;
			int num2 = Size - 1;
			for (int i = num; i <= num2; i++)
			{
				switch (IntPtr.Size)
				{
				case 4:
					array[i] = Marshal.ReadByte((IntPtr)(Address.ToInt32() + i));
					break;
				case 8:
					array[i] = Marshal.ReadByte((IntPtr)(Address.ToInt64() + unchecked((long)i)));
					break;
				}
			}
			return array;
		}
	}

	// Token: 0x06000065 RID: 101 RVA: 0x0000470C File Offset: 0x0000370C
	public static IntPtr GetProcAddress(IntPtr ModuleAddress, string ExportName)
	{
		switch (IntPtr.Size)
		{
		case 4:
			return (IntPtr)Dynamic.InternalGetProcAddressManual32((int)ModuleAddress, ExportName);
		case 8:
			return (IntPtr)Dynamic.InternalGetProcAddressManual64((long)ModuleAddress, ExportName);
		}
		IntPtr result;
		return result;
	}

	// Token: 0x06000066 RID: 102 RVA: 0x0000476C File Offset: 0x0000376C
	public static int InternalGetProcAddressManual32(int ModuleAddress, string Export)
	{
		checked
		{
			int num = Marshal.ReadInt32((IntPtr)(ModuleAddress + 60));
			int num2 = Marshal.ReadInt32((IntPtr)(ModuleAddress + num + 120));
			byte[] value = Dynamic.ReadByteArray((IntPtr)(ModuleAddress + num2), 40);
			int num3 = BitConverter.ToInt32(value, 24);
			int num4 = BitConverter.ToInt32(value, 32) + ModuleAddress;
			int num5 = BitConverter.ToInt32(value, 28) + ModuleAddress;
			int num6 = BitConverter.ToInt32(value, 36) + ModuleAddress;
			IntPtr intPtr = Marshal.AllocHGlobal(64);
			int num7 = 0;
			int num8 = num3;
			for (int i = num7; i <= num8; i++)
			{
				int num9 = Marshal.ReadInt32((IntPtr)(num4 + (i - 1) * 4));
				Marshal.Copy(Dynamic.ReadByteArray((IntPtr)(ModuleAddress + num9), 64), 0, intPtr, 64);
				string left = Marshal.PtrToStringAnsi(intPtr);
				int num10 = (int)BitConverter.ToInt16(Dynamic.ReadByteArray((IntPtr)(num6 + (i - 1) * 2), 2), 0);
				int result = BitConverter.ToInt32(Dynamic.ReadByteArray((IntPtr)(num5 + num10 * 4), 4), 0) + ModuleAddress;
				if (Operators.CompareString(left, Export, false) == 0)
				{
					Marshal.FreeHGlobal(intPtr);
					return result;
				}
			}
			Marshal.FreeHGlobal(intPtr);
			return 0;
		}
	}

	// Token: 0x06000067 RID: 103 RVA: 0x00004898 File Offset: 0x00003898
	public static long InternalGetProcAddressManual64(long ModuleAddress, string Export)
	{
		checked
		{
			int num = Marshal.ReadInt32((IntPtr)(ModuleAddress + 60L));
			int num2 = Marshal.ReadInt32((IntPtr)(ModuleAddress + unchecked((long)num) + 136L));
			byte[] value = Dynamic.ReadByteArray((IntPtr)(ModuleAddress + unchecked((long)num2)), 40);
			int num3 = BitConverter.ToInt32(value, 24);
			long num4 = unchecked((long)BitConverter.ToInt32(value, 32)) + ModuleAddress;
			long num5 = unchecked((long)BitConverter.ToInt32(value, 28)) + ModuleAddress;
			long num6 = unchecked((long)BitConverter.ToInt32(value, 36)) + ModuleAddress;
			IntPtr intPtr = Marshal.AllocHGlobal(64);
			int num7 = 0;
			int num8 = num3;
			for (int i = num7; i <= num8; i++)
			{
				int num9 = Marshal.ReadInt32((IntPtr)(num4 + unchecked((long)(checked((i - 1) * 4)))));
				Marshal.Copy(Dynamic.ReadByteArray((IntPtr)(ModuleAddress + unchecked((long)num9)), 64), 0, intPtr, 64);
				string left = Marshal.PtrToStringAnsi(intPtr);
				int num10 = (int)BitConverter.ToInt16(Dynamic.ReadByteArray((IntPtr)(num6 + unchecked((long)(checked((i - 1) * 2)))), 2), 0);
				long result = unchecked((long)BitConverter.ToInt32(Dynamic.ReadByteArray((IntPtr)(checked(num5 + unchecked((long)(checked(num10 * 4))))), 4), 0)) + ModuleAddress;
				if (Operators.CompareString(left, Export, false) == 0)
				{
					Marshal.FreeHGlobal(intPtr);
					return result;
				}
			}
			Marshal.FreeHGlobal(intPtr);
			return 0L;
		}
	}

	// Token: 0x04000030 RID: 48
	private static readonly Dynamic.LoadLibraryAParameters LoadLibrary = Dynamic.CreateApi<Dynamic.LoadLibraryAParameters>("kernel32", "LoadLibraryA");

	// Token: 0x02000023 RID: 35
	// (Invoke) Token: 0x060000AF RID: 175
	public delegate IntPtr LoadLibraryAParameters(string name);
}
