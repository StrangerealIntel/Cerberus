using System;
using SmartAssembly.StringsEncoding;

namespace ChromV1
{
	// Token: 0x02000065 RID: 101
	public class ParametersWithIV : ICipherParameters
	{
		// Token: 0x06000236 RID: 566 RVA: 0x00011A08 File Offset: 0x0000FC08
		public ParametersWithIV(ICipherParameters parameters, byte[] iv) : this(parameters, iv, 0, iv.Length)
		{
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00011A18 File Offset: 0x0000FC18
		public ParametersWithIV(ICipherParameters parameters, byte[] iv, int ivOff, int ivLen)
		{
			if (parameters == null)
			{
				throw new ArgumentNullException(Strings.Get(107395330));
			}
			if (iv == null)
			{
				throw new ArgumentNullException(Strings.Get(107395345));
			}
			this.Parameters = parameters;
			this.iv = new byte[ivLen];
			Array.Copy(iv, ivOff, this.iv, 0, ivLen);
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00011A80 File Offset: 0x0000FC80
		public ICipherParameters Parameters { get; }

		// Token: 0x06000239 RID: 569 RVA: 0x00011A88 File Offset: 0x0000FC88
		public byte[] GetIV()
		{
			return (byte[])this.iv.Clone();
		}

		// Token: 0x040000E4 RID: 228
		private readonly byte[] iv;
	}
}
