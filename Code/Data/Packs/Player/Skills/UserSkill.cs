using System;

namespace L2_login
{
	public class UserSkill : Object_Base
	{
        public volatile bool OldSkill = false;
        public volatile uint Passive = 0;
        public volatile uint Level = 0;
        private System.DateTime _NextTime = new DateTime(0L);
        private System.DateTime _LastTime = new DateTime(0L);

        private static readonly object NextTimeLock = new object();
        private static readonly object LastTimeLock = new object();

        public bool IsReady()
        {
            if (System.DateTime.Now.Ticks >= NextTime.Ticks)
            {
                return true;
            }

            return false;
        }

        public System.DateTime NextTime
        {
            get
            {
                System.DateTime tmp;
                lock (NextTimeLock)
                {
                    tmp = this._NextTime;
                }
                return tmp;
            }
            set
            {
                lock (NextTimeLock)
                {
                    _NextTime = value;
                }
            }
        }
        public System.DateTime LastTime
		{
			get
			{
				System.DateTime tmp;
				lock(LastTimeLock)
				{
					tmp = this._LastTime;
				}
				return tmp;
			}
			set
			{
				lock(LastTimeLock)
				{
					_LastTime = value;
				}
			}
		}
	}//end of UserSkill
}
