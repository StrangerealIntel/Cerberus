using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200019C RID: 412
	[NullableContext(1)]
	public interface ITraceWriter
	{
		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000F03 RID: 3843
		TraceLevel LevelFilter { get; }

		// Token: 0x06000F04 RID: 3844
		void Trace(TraceLevel level, string message, [Nullable(2)] Exception ex);
	}
}
