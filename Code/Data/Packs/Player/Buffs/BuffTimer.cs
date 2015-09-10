using System;

namespace L2_login
{
	public class BuffTimer
	{
		public volatile uint SkillID = 0;
        private long _LastTickTime = 0;

		private readonly object LastTickTimeLock = new object();

		public long LastTickTime
		{
			get
			{
				long tmp;
				lock(LastTickTimeLock)
				{
					tmp = this._LastTickTime;
				}
				return tmp;
			}
			set
			{
				lock(LastTickTimeLock)
				{
					_LastTickTime = value;
				}
			}
		}
	}
}
