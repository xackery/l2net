using System;

namespace L2_login
{
    public enum BuffTriggers : byte
    {
        //0 - always, 1 - cp, 2 - hp, 3 - mp
        Always = 0,
        CP = 1,
        HP = 2,
        MP = 3,
        Dead = 4,
        Charges = 5,
        Souls = 6,
        DeathPenalty = 7,
        AB_Bleeding = 8,
        AB_Poison = 9,
        AB_Ice = 10,
        AB_Wind = 11,
        AB_Fear = 12,
        AB_Stun = 13,
        AB_Sleep = 14,
        AB_Muted = 15,
        AB_Root = 16,
        AB_Paralysis = 17,
        AB_Petrified = 18,
        AB_Invulnerable = 19,
    }

	public class BuffTargetClass : BaseTargetClass
	{
        public static string Get_BuffTrigger_Name(int type)
        {
            switch (type)
            {
                case 0://Always
                    return "Always";
                case 1://CP
                    return "CP";
                case 2://HP
                    return "HP";
                case 3://MP
                    return "MP";
                case 4://Dead
                    return "Dead";
                case 5://Charges
                    return "Charges";
                case 6://Souls
                    return "Souls";
                case 7://Death Penalty
                    return "Death Penalty";
                case 8://Bleeding
                    return "Bleeding";
                case 9://Poisoned
                    return "Poisoned";
                case 10://Iced
                    return "Iced";
                case 11://Shackled
                    return "Shackled";
                case 12://Feared
                    return "Feared";
                case 13://Stunned
                    return "Stunned";
                case 14://Slept
                    return "Slept";
                case 15://Muted
                    return "Muted";
                case 16://Rooted
                    return "Rooted";
                case 17://Paralyzed
                    return "Paralyzed";
                case 18://Petrified
                    return "Petrified";
                case 19://Ultimate Defense
                    return "Ultimate Defense";
                default:
                    return "unknown";
            }
        }
        public static string Get_BuffTrigger_Name_Toggle(int type)
        {
            switch (type)
            {
                case 0://HP
                    return "HP";
                case 1://MP
                    return "MP";
                default:
                    return "unknown";
            }
        }

		private System.Collections.ArrayList _TargetNames = new System.Collections.ArrayList();//uppercase array of target names
        public volatile BuffTriggers Type;
		public volatile uint SkillID;
        public volatile int Min_Per;
		private long _TickDuration;
        public volatile int Min_MP;
        public volatile int NeedTarget;//dont target, 1 - need target
        public volatile int Range;

		private readonly object TargetNamesLock = new object();
		private readonly object TickDurationLock = new object();

		public BuffTargetClass()
		{
			Reset();
		}

		public void Reset()
		{
			Active = false;
			TargetNames.Clear();
			Type = 0;
			SkillID = 0;
			Min_Per = 100;
			TickDuration = 0L;
			Min_MP = 0;
            Range = Globals.gamedata.botoptions.HealRange;
		}

        public bool IsReady()
        {
            Globals.SkillListLock.EnterReadLock();
            try
            {
                if (Globals.gamedata.skills.ContainsKey(SkillID))
                {
                    UserSkill us = Util.GetSkill(SkillID);

                    if (us.IsReady())
                    {
                        return true;
                    }
                }
            }
            finally
            {
                Globals.SkillListLock.ExitReadLock();
            }
            return false;
        }

		public System.Collections.ArrayList TargetNames
		{
			get
			{
				System.Collections.ArrayList tmp;
				lock(TargetNamesLock)
				{
					tmp = this._TargetNames;
				}
				return tmp;
			}
			set
			{
				lock(TargetNamesLock)
				{
					_TargetNames = value;
				}
			}
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
	}
}
