using System;

namespace L2_login
{
	public class ItemTargetClass : BaseTargetClass
	{
        public volatile BuffTriggers Type;
        public volatile uint ItemID;//generic item type
        public volatile int Min_Per;
		private long _TickDuration;
		private long _LastTickTime = 0L;

		private readonly object TickDurationLock = new object();
		private readonly object LastTickTimeLock = new object();

		public ItemTargetClass()
		{
			Reset();
		}

		public void Reset()
		{
			Active = false;
			Type = 0;
			ItemID = 0;
			Min_Per = 0;
			TickDuration = 0;
			LastTickTime = 0;
		}

		public long TickDuration
		{
			get
			{
				long tmp;
				lock(TickDurationLock)
				{
					tmp = this._TickDuration;

                    /*if (Globals.RunDegraded && tmp < Globals.Degraded_MinItemTimer)
                    {
                        tmp = Globals.Degraded_MinItemTimer;
                    }*/
				}
				return tmp;
			}
			set
			{
				lock(TickDurationLock)
				{
                    _TickDuration = value;
				}
			}
		}

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
