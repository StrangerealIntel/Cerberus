using System;
using SmartAssembly.StringsEncoding;

namespace ChromV1
{
	// Token: 0x02000063 RID: 99
	public class KeyParameter : ICipherParameters
	{
		// Token: 0x0600022E RID: 558 RVA: 0x00011880 File Offset: 0x0000FA80
		public KeyParameter(byte[] key)
		{
			if (key == null)
			{
				throw new ArgumentNullException(Strings.Get(107395385));
			}
			this.key = (byte[])key.Clone();
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000118B0 File Offset: 0x0000FAB0
		public KeyParameter(byte[] key, int keyOff, int keyLen)
		{
			if (key == null)
			{
				throw new ArgumentNullException(Strings.Get(107395385));
			}
			if (keyOff < 0 || keyOff > key.Length)
			{
				throw new ArgumentOutOfRangeException(Strings.Get(107395380));
			}
			if (keyLen < 0 || keyOff + keyLen > key.Length)
			{
				throw new ArgumentOutOfRangeException(Strings.Get(107395339));
			}
			this.key = new byte[keyLen];
			Array.Copy(key, keyOff, this.key, 0, keyLen);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0001193C File Offset: 0x0000FB3C
		public byte[] GetKey()
		{
			return (byte[])this.key.Clone();
		}

		// Token: 0x040000E3 RID: 227
		private readonly byte[] key;
	}
}
