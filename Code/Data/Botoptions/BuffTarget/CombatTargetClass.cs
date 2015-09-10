using System;
using System.Collections.Generic;
using System.Text;

namespace L2_login
{
    public class CombatTargetClass : BaseTargetClass
    {
        public volatile BuffTriggers Type;
        public volatile int Conditional;//0 >=, 1 <=
        public volatile int ShortCutID;
        public volatile int Min_Per;
        private long _TickDuration;
        private long _LastTickTime = 0L;
        public volatile int Min_MP;

        private readonly object TickDurationLock = new object();
        private readonly object LastTickTimeLock = new object();

        public CombatTargetClass()
        {
            Reset();
        }

        public void Reset()
        {
            Active = false;
            Type = 0;
            Conditional = 0;
            ShortCutID = 0;
            Min_Per = 100;
            TickDuration = 0L;
            LastTickTime = 0L;
            Min_MP = 0;
        }

        public long TickDuration
        {
            get
            {
                long tmp;
                lock (TickDurationLock)
                {
                    tmp = this._TickDuration;
                }
                return tmp;
            }
            set
            {
                lock (TickDurationLock)
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
                lock (LastTickTimeLock)
                {
                    tmp = this._LastTickTime;
                }
                return tmp;
            }
            set
            {
                lock (LastTickTimeLock)
                {
                    _LastTickTime = value;
                }
            }
        }
    }
}
