using System;

namespace L2_login
{
    public class CharBuff : Object_Base
    {
        public volatile uint SkillLevel = 0;
        private long _ExpiresTime = 0;
        public int EFFECT_TIME = 0;
        private readonly object ExpiresTimeLock = new object();

        public long ExpiresTime
        {
            get
            {
                long tmp;
                lock (ExpiresTimeLock)
                {
                    tmp = this._ExpiresTime;
                }
                return tmp;
            }
            set
            {
                lock (ExpiresTimeLock)
                {
                    _ExpiresTime = value;
                }
            }
        }
    }
}
