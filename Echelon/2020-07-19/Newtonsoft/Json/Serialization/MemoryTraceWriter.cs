using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x020001B6 RID: 438
	[NullableContext(1)]
	[Nullable(0)]
	public class MemoryTraceWriter : ITraceWriter
	{
		// Token: 0x17000389 RID: 905
		// (get) Token: 0x0600109B RID: 4251 RVA: 0x0005EA88 File Offset: 0x0005CC88
		// (set) Token: 0x0600109C RID: 4252 RVA: 0x0005EA90 File Offset: 0x0005CC90
		public TraceLevel LevelFilter { get; set; }

		// Token: 0x0600109D RID: 4253 RVA: 0x0005EA9C File Offset: 0x0005CC9C
		public MemoryTraceWriter()
		{
			this.LevelFilter = TraceLevel.Verbose;
			this._traceMessages = new Queue<string>();
			this._lock = new object();
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x0005EAC4 File Offset: 0x0005CCC4
		public void Trace(TraceLevel level, string message, [Nullable(2)] Exception ex)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(DateTime.Now.ToString("yyyy'-'MM'-'dd'T'HH':'mm':'ss'.'fff", CultureInfo.InvariantCulture));
			stringBuilder.Append(" ");
			stringBuilder.Append(level.ToString("g"));
			stringBuilder.Append(" ");
			stringBuilder.Append(message);
			string item = stringBuilder.ToString();
			object @lock = this._lock;
			lock (@lock)
			{
				if (this._traceMessages.Count >= 1000)
				{
					this._traceMessages.Dequeue();
				}
				this._traceMessages.Enqueue(item);
			}
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x0005EB94 File Offset: 0x0005CD94
		public IEnumerable<string> GetTraceMessages()
		{
			return this._traceMessages;
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x0005EB9C File Offset: 0x0005CD9C
		public override string ToString()
		{
			object @lock = this._lock;
			string result;
			lock (@lock)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string value in this._traceMessages)
				{
					if (stringBuilder.Length > 0)
					{
						stringBuilder.AppendLine();
					}
					stringBuilder.Append(value);
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x04000835 RID: 2101
		private readonly Queue<string> _traceMessages;

		// Token: 0x04000836 RID: 2102
		private readonly object _lock;
	}
}
