using System;
using System.Security.Cryptography;

namespace CG.Web.MegaApiClient.Cryptography
{
	// Token: 0x0200011C RID: 284
	internal class CachedCryptoTransform : ICryptoTransform, IDisposable
	{
		// Token: 0x0600099E RID: 2462 RVA: 0x0003D858 File Offset: 0x0003BA58
		public CachedCryptoTransform(Func<ICryptoTransform> factory, bool isKnownReusable)
		{
			this.factory = factory;
			this.isKnownReusable = isKnownReusable;
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0003D870 File Offset: 0x0003BA70
		public void Dispose()
		{
			ICryptoTransform cryptoTransform = this.cachedInstance;
			if (cryptoTransform == null)
			{
				return;
			}
			cryptoTransform.Dispose();
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0003D888 File Offset: 0x0003BA88
		public int TransformBlock(byte[] inputBuffer, int inputOffset, int inputCount, byte[] outputBuffer, int outputOffset)
		{
			return this.Forward<int>((ICryptoTransform x) => x.TransformBlock(inputBuffer, inputOffset, inputCount, outputBuffer, outputOffset));
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x0003D8D8 File Offset: 0x0003BAD8
		public byte[] TransformFinalBlock(byte[] inputBuffer, int inputOffset, int inputCount)
		{
			if (this.isKnownReusable && this.cachedInstance != null)
			{
				return this.cachedInstance.TransformFinalBlock(inputBuffer, inputOffset, inputCount);
			}
			return this.Forward<byte[]>((ICryptoTransform x) => x.TransformFinalBlock(inputBuffer, inputOffset, inputCount));
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x0003D94C File Offset: 0x0003BB4C
		public int InputBlockSize
		{
			get
			{
				return this.Forward<int>((ICryptoTransform x) => x.InputBlockSize);
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060009A3 RID: 2467 RVA: 0x0003D978 File Offset: 0x0003BB78
		public int OutputBlockSize
		{
			get
			{
				return this.Forward<int>((ICryptoTransform x) => x.OutputBlockSize);
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x0003D9A4 File Offset: 0x0003BBA4
		public bool CanTransformMultipleBlocks
		{
			get
			{
				return this.Forward<bool>((ICryptoTransform x) => x.CanTransformMultipleBlocks);
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060009A5 RID: 2469 RVA: 0x0003D9D0 File Offset: 0x0003BBD0
		public bool CanReuseTransform
		{
			get
			{
				return this.Forward<bool>((ICryptoTransform x) => x.CanReuseTransform);
			}
		}

		// Token: 0x060009A6 RID: 2470 RVA: 0x0003D9FC File Offset: 0x0003BBFC
		private T Forward<T>(Func<ICryptoTransform, T> action)
		{
			ICryptoTransform cryptoTransform = this.cachedInstance ?? this.factory();
			T result;
			try
			{
				result = action(cryptoTransform);
			}
			finally
			{
				if (!this.isKnownReusable && !cryptoTransform.CanReuseTransform)
				{
					cryptoTransform.Dispose();
				}
				else
				{
					this.cachedInstance = cryptoTransform;
				}
			}
			return result;
		}

		// Token: 0x04000574 RID: 1396
		private readonly Func<ICryptoTransform> factory;

		// Token: 0x04000575 RID: 1397
		private readonly bool isKnownReusable;

		// Token: 0x04000576 RID: 1398
		private ICryptoTransform cachedInstance;
	}
}
