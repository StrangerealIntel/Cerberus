using System;
using System.IO;
using System.Runtime.Serialization;
using DamienG.Security.Cryptography;
using Newtonsoft.Json;

namespace CG.Web.MegaApiClient.Serialization
{
	// Token: 0x02000101 RID: 257
	internal class Attributes
	{
		// Token: 0x06000912 RID: 2322 RVA: 0x0003C954 File Offset: 0x0003AB54
		[JsonConstructor]
		private Attributes()
		{
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x0003C95C File Offset: 0x0003AB5C
		public Attributes(string name)
		{
			this.Name = name;
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0003C96C File Offset: 0x0003AB6C
		public Attributes(string name, Attributes originalAttributes)
		{
			this.Name = name;
			this.SerializedFingerprint = originalAttributes.SerializedFingerprint;
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0003C998 File Offset: 0x0003AB98
		public Attributes(string name, Stream stream, DateTime? modificationDate = null)
		{
			this.Name = name;
			if (modificationDate != null)
			{
				byte[] array = new byte[25];
				Buffer.BlockCopy(this.ComputeCrc(stream), 0, array, 0, 16);
				byte[] array2 = modificationDate.Value.ToEpoch().SerializeToBytes();
				Buffer.BlockCopy(array2, 0, array, 16, array2.Length);
				Array.Resize<byte>(ref array, array.Length - 9 + array2.Length);
				this.SerializedFingerprint = array.ToBase64();
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000916 RID: 2326 RVA: 0x0003CA18 File Offset: 0x0003AC18
		// (set) Token: 0x06000917 RID: 2327 RVA: 0x0003CA20 File Offset: 0x0003AC20
		[JsonProperty("n")]
		public string Name { get; set; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000918 RID: 2328 RVA: 0x0003CA2C File Offset: 0x0003AC2C
		// (set) Token: 0x06000919 RID: 2329 RVA: 0x0003CA34 File Offset: 0x0003AC34
		[JsonProperty("c", DefaultValueHandling = DefaultValueHandling.Ignore)]
		public string SerializedFingerprint { get; set; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x0003CA40 File Offset: 0x0003AC40
		// (set) Token: 0x0600091B RID: 2331 RVA: 0x0003CA48 File Offset: 0x0003AC48
		[JsonIgnore]
		public DateTime? ModificationDate { get; private set; }

		// Token: 0x0600091C RID: 2332 RVA: 0x0003CA54 File Offset: 0x0003AC54
		[OnDeserialized]
		public void OnDeserialized(StreamingContext context)
		{
			if (this.SerializedFingerprint != null)
			{
				byte[] array = this.SerializedFingerprint.FromBase64();
				this.ModificationDate = new DateTime?(array.DeserializeToLong(16, array.Length - 16).ToDateTime());
			}
		}

		// Token: 0x0600091D RID: 2333 RVA: 0x0003CA9C File Offset: 0x0003AC9C
		private uint[] ComputeCrc(Stream stream)
		{
			stream.Seek(0L, SeekOrigin.Begin);
			uint[] array = new uint[4];
			byte[] array2 = new byte[16];
			uint num = 0u;
			if (stream.Length <= 16L)
			{
				if (stream.Read(array2, 0, (int)stream.Length) != 0)
				{
					Buffer.BlockCopy(array2, 0, array, 0, array2.Length);
				}
			}
			else if (stream.Length <= 8192L)
			{
				byte[] buffer = new byte[stream.Length];
				int num2 = 0;
				while ((long)(num2 += stream.Read(buffer, num2, (int)stream.Length - num2)) < stream.Length)
				{
				}
				for (int i = 0; i < array.Length; i++)
				{
					int num3 = (int)((long)i * stream.Length / (long)array.Length);
					int num4 = (int)((long)(i + 1) * stream.Length / (long)array.Length);
					using (Crc32 crc = new Crc32(3988292384u, uint.MaxValue))
					{
						num = BitConverter.ToUInt32(crc.ComputeHash(buffer, num3, num4 - num3), 0);
					}
					array[i] = num;
				}
			}
			else
			{
				byte[] array3 = new byte[64];
				uint num5 = (uint)(8192 / (array3.Length * 4));
				long num6 = 0L;
				for (uint num7 = 0u; num7 < 4u; num7 += 1u)
				{
					byte[] array4 = null;
					uint num8 = uint.MaxValue;
					for (uint num9 = 0u; num9 < num5; num9 += 1u)
					{
						long num10 = (stream.Length - (long)array3.Length) * (long)((ulong)(num7 * num5 + num9)) / (long)((ulong)(4u * num5 - 1u));
						stream.Seek(num10 - num6, SeekOrigin.Current);
						num6 += num10 - num6;
						int num11 = stream.Read(array3, 0, array3.Length);
						num6 += (long)num11;
						using (Crc32 crc2 = new Crc32(3988292384u, num8))
						{
							array4 = crc2.ComputeHash(array3, 0, num11);
							byte[] array5 = new byte[array4.Length];
							array4.CopyTo(array5, 0);
							if (BitConverter.IsLittleEndian)
							{
								Array.Reverse(array5);
							}
							num8 = BitConverter.ToUInt32(array5, 0);
							num8 = ~num8;
						}
					}
					num = BitConverter.ToUInt32(array4, 0);
					array[(int)num7] = num;
				}
			}
			return array;
		}

		// Token: 0x0400053D RID: 1341
		private const int CrcArrayLength = 4;

		// Token: 0x0400053E RID: 1342
		private const int CrcSize = 16;

		// Token: 0x0400053F RID: 1343
		private const int FingerprintMaxSize = 25;

		// Token: 0x04000540 RID: 1344
		private const int MAXFULL = 8192;

		// Token: 0x04000541 RID: 1345
		private const uint CryptoPPCRC32Polynomial = 3988292384u;
	}
}
