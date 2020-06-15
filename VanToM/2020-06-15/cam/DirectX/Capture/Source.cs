using System;

namespace cam.DirectX.Capture
{
	// Token: 0x020000A0 RID: 160
	public class Source : IDisposable
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600035E RID: 862 RVA: 0x000140B8 File Offset: 0x000122B8
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x0600035F RID: 863 RVA: 0x000140C0 File Offset: 0x000122C0
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000360 RID: 864 RVA: 0x000140C8 File Offset: 0x000122C8
		// (set) Token: 0x06000361 RID: 865 RVA: 0x000140D8 File Offset: 0x000122D8
		public virtual bool Enabled
		{
			get
			{
				bool result;
				return result;
			}
			set
			{
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x000140DC File Offset: 0x000122DC
		~Source()
		{
			this.Dispose();
		}

		// Token: 0x06000363 RID: 867 RVA: 0x0001410C File Offset: 0x0001230C
		public virtual void Dispose()
		{
			this.m_name = null;
		}

		// Token: 0x040002AD RID: 685
		protected string m_name;
	}
}
