using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace L2_login
{
    public class SmartTimer
    {
        public delegate void Tick();

        public Tick OnTimerTick;
        private const int InternalTick = 200;

        public volatile bool Initialized = false;
        public volatile int Interval;
        private Thread _timerThread;
        private bool _fire = false;
        private System.DateTime _lastfired = new DateTime(0L);

        private readonly object LastFiredLock = new object();
        private readonly object FireLock = new object();

        private void Run()
        {
            while (true)
            {
                Thread.Sleep(InternalTick);

                if (Fire && System.DateTime.Now > LastFired.AddMilliseconds(Interval))
                {
                    OnTimerTick();
                }

                /*lock (TimerLock)
                {
                    if (Fire && System.DateTime.Now > LastFired.AddMilliseconds(Interval))
                    {
                        OnTimerTick();
                    }
                }*/
            }
        }

        private void Init()
        {
            Initialized = true;

            LastFired = System.DateTime.Now;

            _timerThread = new Thread(new ThreadStart(Run));
            _timerThread.IsBackground = true;
            //_timerThread.Priority = System.Threading.ThreadPriority.AboveNormal;
            _timerThread.Start();
        }

        public void Start()
        {
            Fire = true;

            if (!Initialized)
            {
                Init();
            }
        }

        public void Stop()
        {
            Fire = false;
        }

        private System.DateTime LastFired
        {
            get
            {
                System.DateTime tmp;
                lock (LastFiredLock)
                {
                    tmp = this._lastfired;
                }
                return tmp;
            }
            set
            {
                lock (LastFiredLock)
                {
                    _lastfired = value;
                }
            }
        }
        private bool Fire
        {
            get
            {
                bool tmp;
                lock (FireLock)
                {
                    tmp = this._fire;
                }
                return tmp;
            }
            set
            {
                lock (FireLock)
                {
                    if (value == true && _fire == false)
                    {
                        LastFired = System.DateTime.Now;
                    }

                    _fire = value;
                }
            }
        }
    }
}
