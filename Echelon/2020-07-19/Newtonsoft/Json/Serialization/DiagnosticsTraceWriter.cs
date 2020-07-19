using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000193 RID: 403
	public class DiagnosticsTraceWriter : ITraceWriter
	{
		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x00055D7C File Offset: 0x00053F7C
		// (set) Token: 0x06000EE4 RID: 3812 RVA: 0x00055D84 File Offset: 0x00053F84
		public TraceLevel LevelFilter { get; set; }

		// Token: 0x06000EE5 RID: 3813 RVA: 0x00055D90 File Offset: 0x00053F90
		private TraceEventType GetTraceEventType(TraceLevel level)
		{
			switch (level)
			{
			case TraceLevel.Error:
				return TraceEventType.Error;
			case TraceLevel.Warning:
				return TraceEventType.Warning;
			case TraceLevel.Info:
				return TraceEventType.Information;
			case TraceLevel.Verbose:
				return TraceEventType.Verbose;
			default:
				throw new ArgumentOutOfRangeException("level");
			}
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x00055DC4 File Offset: 0x00053FC4
		[NullableContext(1)]
		public void Trace(TraceLevel level, string message, [Nullable(2)] Exception ex)
		{
			if (level == TraceLevel.Off)
			{
				return;
			}
			TraceEventCache eventCache = new TraceEventCache();
			TraceEventType traceEventType = this.GetTraceEventType(level);
			foreach (object obj in System.Diagnostics.Trace.Listeners)
			{
				TraceListener traceListener = (TraceListener)obj;
				if (!traceListener.IsThreadSafe)
				{
					TraceListener obj2 = traceListener;
					lock (obj2)
					{
						traceListener.TraceEvent(eventCache, "Newtonsoft.Json", traceEventType, 0, message);
						goto IL_7D;
					}
					goto IL_6E;
				}
				goto IL_6E;
				IL_7D:
				if (System.Diagnostics.Trace.AutoFlush)
				{
					traceListener.Flush();
					continue;
				}
				continue;
				IL_6E:
				traceListener.TraceEvent(eventCache, "Newtonsoft.Json", traceEventType, 0, message);
				goto IL_7D;
			}
		}
	}
}
