using System;

namespace L2_login
{
    public enum BuffState : int
    {
        //0 nothing/following | 1 - attacking | 2 - buffing | 3 - waiting for buff to be used
        DoesntHave = -1,
        Needs = 0,
        Has = 1,//doesn't need
    }

	public class CharBuffTimer
	{
		private string _Name = "";
		private System.Collections.ArrayList _BuffTimes = new System.Collections.ArrayList();

		private readonly object NameLock = new object();
		private readonly object BuffTimesLock = new object();


		public CharBuffTimer()
		{
			BuffTimes.Clear();
		}

		public string Name
		{
			get
			{
				string tmp;
				lock(NameLock)
				{
					tmp = this._Name;
				}
				return tmp;
			}
			set
			{
				lock(NameLock)
				{
                    _Name = value.ToUpperInvariant();
				}
			}
		}
		public System.Collections.ArrayList BuffTimes
		{
			get
			{
				System.Collections.ArrayList tmp;
				lock(BuffTimesLock)
				{
					tmp = this._BuffTimes;
				}
				return tmp;
			}
			set
			{
				lock(BuffTimesLock)
				{
					_BuffTimes = value;
				}
			}
		}

        public BuffState Has_Buff(BuffTargetClass bft)
		{
            if (Globals.gamedata.my_char.Cur_MP < bft.Min_MP)
            {
                //not enough mp... fck this
                return BuffState.Has;
            }

            long expire = System.DateTime.Now.Ticks - bft.TickDuration;

            foreach (BuffTimer bt in BuffTimes)
            {
                if (bt.SkillID == bft.SkillID)
                {
                    if (bt.LastTickTime < expire)
                    {
                        //has buff and it HAS expired
                        break;
                    }
                    else
                    {
                        //has buff and it hasnt expired
                        return BuffState.Has;
                    }
                }
            }

			//need to check the conditional on the buff/heal to see if it needs to be used
			switch(bft.Type)
			{
                case BuffTriggers.Always://always
					foreach(BuffTimer bt in BuffTimes)
					{
                        if (bt.SkillID == bft.SkillID)
						{
                            if (bt.LastTickTime < expire)
                            {
                                if (System.String.Equals(Globals.gamedata.my_char.Name.ToUpperInvariant(), Name))
                                {
                                    if (Globals.gamedata.my_char.Cur_HP == 0)
                                    {
                                        return BuffState.Has;//dead...
                                    }
                                    return BuffState.Needs;
                                }
                                else
                                {
                                    CharInfo player = null;

                                    Globals.PlayerLock.EnterReadLock();
                                    try
                                    {
                                        player = Util.GetChar(Name);
                                    }
                                    finally
                                    {
                                        Globals.PlayerLock.ExitReadLock();
                                    }

                                    if (player != null)
                                    {
                                        if (player.Cur_HP == 0)
                                        {
                                            return BuffState.Has;//dead...
                                        }
                                        return BuffState.Needs;
                                    }
                                }
                            }
                            else
                            {
                                //has buff and it hasnt expired
                                return BuffState.Has;
                            }
						}
					}

					//buff isnt in the list
					//so false
					return BuffState.DoesntHave;
                case BuffTriggers.CP://cp
					//trivial case, do the always need it?
					if(bft.Min_Per > 100)
						return 0;

                    if (System.String.Equals(Globals.gamedata.my_char.Name.ToUpperInvariant(), Name))
                    {
                        if (Globals.gamedata.my_char.Cur_HP == 0)
                        {
                            return BuffState.Has;//dead...
                        }
                        if (Globals.gamedata.my_char.Max_CP == 0)
                        {
                            return BuffState.Has;//prevent divide by zero error
                        }
                        if ((Globals.gamedata.my_char.Cur_CP * 100) / Globals.gamedata.my_char.Max_CP < bft.Min_Per)
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else
                    {
                        CharInfo player = null;

                        Globals.PlayerLock.EnterReadLock();
                        try
                        {
                            player = Util.GetChar(Name);
                        }
                        finally
                        {
                            Globals.PlayerLock.ExitReadLock();
                        }

                        if (player != null)
                        {
                            if (player.Cur_HP == 0)
                            {
                                return BuffState.Has;//dead...
                            }
                            if (player.Max_CP == 0)
                            {
                                return BuffState.Has;//prevent divide by zero error
                            }
                            if ((player.Cur_CP * 100) / player.Max_CP < bft.Min_Per)
                            {
                                return BuffState.Needs;
                            }
                            return BuffState.Has;
                        }
                    }
                    break;
                case BuffTriggers.HP://hp
					//trivial case, do the always need it?
					if(bft.Min_Per > 100)
						return 0;

                    if (System.String.Equals(Globals.gamedata.my_char.Name.ToUpperInvariant(), Name))
                    {
                        if (Globals.gamedata.my_char.Cur_HP == 0)
                        {
                            return BuffState.Has;//dead...
                        }
                        if (Globals.gamedata.my_char.Max_HP == 0)
                        {
                            return BuffState.Has;//prevent divide by zero error
                        }
                        if ((Globals.gamedata.my_char.Cur_HP * 100) / Globals.gamedata.my_char.Max_HP < bft.Min_Per)
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET", Name))
                    {
                        if (Globals.gamedata.my_pet.Cur_HP == 0) //.my_char.Cur_HP == 0)
                        {
                            return BuffState.Has;//dead...
                        }
                        if (Globals.gamedata.my_pet.Max_HP == 0) //.my_char.Max_HP == 0)
                        {
                            return BuffState.Has;//prevent divide by zero error
                        }
                        if ((Globals.gamedata.my_pet.Cur_HP * 100) / Globals.gamedata.my_pet.Max_HP < bft.Min_Per)//((Globals.gamedata.my_char.Cur_HP * 100) / Globals.gamedata.my_char.Max_HP < bft.Min_Per)
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET1", Name))
                    {
                        if (Globals.gamedata.my_pet1.Cur_HP == 0) //.my_char.Cur_HP == 0)
                        {
                            return BuffState.Has;//dead...
                        }
                        if (Globals.gamedata.my_pet1.Max_HP == 0) //.my_char.Max_HP == 0)
                        {
                            return BuffState.Has;//prevent divide by zero error
                        }
                        if ((Globals.gamedata.my_pet1.Cur_HP * 100) / Globals.gamedata.my_pet1.Max_HP < bft.Min_Per)//((Globals.gamedata.my_char.Cur_HP * 100) / Globals.gamedata.my_char.Max_HP < bft.Min_Per)
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET2", Name))
                    {
                        if (Globals.gamedata.my_pet2.Cur_HP == 0) //.my_char.Cur_HP == 0)
                        {
                            return BuffState.Has;//dead...
                        }
                        if (Globals.gamedata.my_pet2.Max_HP == 0) //.my_char.Max_HP == 0)
                        {
                            return BuffState.Has;//prevent divide by zero error
                        }
                        if ((Globals.gamedata.my_pet2.Cur_HP * 100) / Globals.gamedata.my_pet2.Max_HP < bft.Min_Per)//((Globals.gamedata.my_char.Cur_HP * 100) / Globals.gamedata.my_char.Max_HP < bft.Min_Per)
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET3", Name))
                    {
                        if (Globals.gamedata.my_pet3.Cur_HP == 0) //.my_char.Cur_HP == 0)
                        {
                            return BuffState.Has;//dead...
                        }
                        if (Globals.gamedata.my_pet3.Max_HP == 0) //.my_char.Max_HP == 0)
                        {
                            return BuffState.Has;//prevent divide by zero error
                        }
                        if ((Globals.gamedata.my_pet3.Cur_HP * 100) / Globals.gamedata.my_pet3.Max_HP < bft.Min_Per)//((Globals.gamedata.my_char.Cur_HP * 100) / Globals.gamedata.my_char.Max_HP < bft.Min_Per)
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else
                    {
                        CharInfo player = null;

                        Globals.PlayerLock.EnterReadLock();
                        try
                        {
                            player = Util.GetChar(Name);
                        }
                        finally
                        {
                            Globals.PlayerLock.ExitReadLock();
                        }

                        if (player != null)
                        {
                            if (player.Cur_HP == 0)
                            {
                                return BuffState.Has;//dead...
                            }
                            if (player.Max_HP == 0)
                            {
                                return BuffState.Has;//prevent divide by zero error
                            }
                            if ((player.Cur_HP * 100) / player.Max_HP < bft.Min_Per)
                            {
                                return BuffState.Needs;
                            }
                            return BuffState.Has;
                        }
                    }
					break;
                case BuffTriggers.MP://mp
					//trivial case, do the always need it?
					if(bft.Min_Per > 100)
						return 0;

                    if (System.String.Equals(Globals.gamedata.my_char.Name.ToUpperInvariant(), Name))
                    {
                        if (Globals.gamedata.my_char.Cur_HP == 0)
                        {
                            return BuffState.Has;//dead...
                        }
                        if (Globals.gamedata.my_char.Max_MP == 0)
                        {
                            return BuffState.Has;//prevent divide by zero error
                        }
                        if ((Globals.gamedata.my_char.Cur_MP * 100) / Globals.gamedata.my_char.Max_MP < bft.Min_Per)
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET", Name))
                    {
                        if (Globals.gamedata.my_pet.Cur_MP == 0) //.my_char.Cur_MP == 0)
                        {
                            return BuffState.Has;//dead...
                        }
                        if (Globals.gamedata.my_pet.Max_MP == 0) //.my_char.Max_MP == 0)
                        {
                            return BuffState.Has;//prevent divide by zero error
                        }
                        if ((Globals.gamedata.my_pet.Cur_MP * 100) / Globals.gamedata.my_pet.Max_MP < bft.Min_Per)//((Globals.gamedata.my_char.Cur_MP * 100) / Globals.gamedata.my_char.Max_MP < bft.Min_Per)
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET1", Name))
                    {
                        if (Globals.gamedata.my_pet1.Cur_MP == 0) //.my_char.Cur_MP == 0)
                        {
                            return BuffState.Has;//dead...
                        }
                        if (Globals.gamedata.my_pet1.Max_MP == 0) //.my_char.Max_MP == 0)
                        {
                            return BuffState.Has;//prevent divide by zero error
                        }
                        if ((Globals.gamedata.my_pet1.Cur_MP * 100) / Globals.gamedata.my_pet1.Max_MP < bft.Min_Per)//((Globals.gamedata.my_char.Cur_MP * 100) / Globals.gamedata.my_char.Max_MP < bft.Min_Per)
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET2", Name))
                    {
                        if (Globals.gamedata.my_pet2.Cur_MP == 0) //.my_char.Cur_MP == 0)
                        {
                            return BuffState.Has;//dead...
                        }
                        if (Globals.gamedata.my_pet2.Max_MP == 0) //.my_char.Max_MP == 0)
                        {
                            return BuffState.Has;//prevent divide by zero error
                        }
                        if ((Globals.gamedata.my_pet2.Cur_MP * 100) / Globals.gamedata.my_pet2.Max_MP < bft.Min_Per)//((Globals.gamedata.my_char.Cur_MP * 100) / Globals.gamedata.my_char.Max_MP < bft.Min_Per)
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET3", Name))
                    {
                        if (Globals.gamedata.my_pet3.Cur_MP == 0) //.my_char.Cur_MP == 0)
                        {
                            return BuffState.Has;//dead...
                        }
                        if (Globals.gamedata.my_pet3.Max_MP == 0) //.my_char.Max_MP == 0)
                        {
                            return BuffState.Has;//prevent divide by zero error
                        }
                        if ((Globals.gamedata.my_pet3.Cur_MP * 100) / Globals.gamedata.my_pet3.Max_MP < bft.Min_Per)//((Globals.gamedata.my_char.Cur_MP * 100) / Globals.gamedata.my_char.Max_MP < bft.Min_Per)
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else
                    {
                        CharInfo player = null;

                        Globals.PlayerLock.EnterReadLock();
                        try
                        {
                            player = Util.GetChar(Name);
                        }
                        finally
                        {
                            Globals.PlayerLock.ExitReadLock();
                        }

                        if (player != null)
                        {
                            if (player.Cur_HP == 0)
                            {
                                return BuffState.Has;//dead...
                            }
                            if (player.Max_MP == 0)
                            {
                                return BuffState.Has;//prevent divide by zero error
                            }
                            if ((player.Cur_MP * 100) / player.Max_MP < bft.Min_Per)
                            {
                                return BuffState.Needs;
                            }
                            return BuffState.Has;
                        }
                    }
					break;
                case BuffTriggers.Dead://DEAD
                    if (System.String.Equals(Globals.gamedata.my_char.Name.ToUpperInvariant(), Name))
                    {
                        if (Globals.gamedata.my_char.Cur_HP == 0)
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else
                    {
                        CharInfo player = null;

                        Globals.PlayerLock.EnterReadLock();
                        try
                        {
                            player = Util.GetChar(Name);
                        }
                        finally
                        {
                            Globals.PlayerLock.ExitReadLock();
                        }

                        if (player != null)
                        {
                            if (player.Cur_HP == 0 || player.isAlikeDead == 1)
                            {
                                return BuffState.Needs;
                            }
                            return BuffState.Has;
                        }
                    }
                    break;
                case BuffTriggers.Charges:
                    if (System.String.Equals(Globals.gamedata.my_char.Name.ToUpperInvariant(), Name))
                    {
                        if (Globals.gamedata.my_char.Charges < bft.Min_Per)
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    break;
                case BuffTriggers.Souls:
                    if (System.String.Equals(Globals.gamedata.my_char.Name.ToUpperInvariant(), Name))
                    {
                        if (Globals.gamedata.my_char.Souls < bft.Min_Per)
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    break;
                case BuffTriggers.DeathPenalty:
                    if (System.String.Equals(Globals.gamedata.my_char.Name.ToUpperInvariant(), Name))
                    {
                        if (Globals.gamedata.my_char.DeathPenalty >= bft.Min_Per)
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    break;
                case BuffTriggers.AB_Bleeding:
                    if (System.String.Equals(Globals.gamedata.my_char.Name.ToUpperInvariant(), Name))
                    {
                        if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.BLEEDING))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET", Name))
                    {
                        if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.BLEEDING))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET1", Name))
                    {
                        if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.BLEEDING))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET2", Name))
                    {
                        if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.BLEEDING))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET3", Name))
                    {
                        if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.BLEEDING))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else
                    {
                        CharInfo player = null;

                        Globals.PlayerLock.EnterReadLock();
                        try
                        {
                            player = Util.GetChar(Name);
                        }
                        finally
                        {
                            Globals.PlayerLock.ExitReadLock();
                        }

                        if (player != null)
                        {
                            if (player.HasEffect(AbnormalEffects.BLEEDING))
                            {
                                return BuffState.Needs;
                            }
                            return BuffState.Has;
                        }
                    }
                    break;
                case BuffTriggers.AB_Poison:
                    if (System.String.Equals(Globals.gamedata.my_char.Name.ToUpperInvariant(), Name))
                    {
                        if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.POISON))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET", Name))
                    {
                        if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.POISON))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET1", Name))
                    {
                        if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.POISON))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET2", Name))
                    {
                        if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.POISON))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET3", Name))
                    {
                        if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.POISON))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else
                    {
                        CharInfo player = null;

                        Globals.PlayerLock.EnterReadLock();
                        try
                        {
                            player = Util.GetChar(Name);
                        }
                        finally
                        {
                            Globals.PlayerLock.ExitReadLock();
                        }

                        if (player != null)
                        {
                            if (player.HasEffect(AbnormalEffects.POISON))
                            {
                                return BuffState.Needs;
                            }
                            return BuffState.Has;
                        }
                    }
                    break;
                case BuffTriggers.AB_Ice:
                    if (System.String.Equals(Globals.gamedata.my_char.Name.ToUpperInvariant(), Name))
                    {
                        if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.ICE))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET", Name))
                    {
                        if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.ICE))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET1", Name))
                    {
                        if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.ICE))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET2", Name))
                    {
                        if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.ICE))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET3", Name))
                    {
                        if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.ICE))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else
                    {
                        CharInfo player = null;

                        Globals.PlayerLock.EnterReadLock();
                        try
                        {
                            player = Util.GetChar(Name);
                        }
                        finally
                        {
                            Globals.PlayerLock.ExitReadLock();
                        }

                        if (player != null)
                        {
                            if (player.HasEffect(AbnormalEffects.ICE))
                            {
                                return BuffState.Needs;
                            }
                            return BuffState.Has;
                        }
                    }
                    break;
                case BuffTriggers.AB_Wind:
                    if (System.String.Equals(Globals.gamedata.my_char.Name.ToUpperInvariant(), Name))
                    {
                        if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.WIND))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET", Name))
                    {
                        if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.WIND))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET1", Name))
                    {
                        if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.WIND))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET2", Name))
                    {
                        if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.WIND))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET3", Name))
                    {
                        if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.WIND))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else
                    {
                        CharInfo player = null;

                        Globals.PlayerLock.EnterReadLock();
                        try
                        {
                            player = Util.GetChar(Name);
                        }
                        finally
                        {
                            Globals.PlayerLock.ExitReadLock();
                        }

                        if (player != null)
                        {
                            if (player.HasEffect(AbnormalEffects.WIND))
                            {
                                return BuffState.Needs;
                            }
                            return BuffState.Has;
                        }
                    }
                    break;
                case BuffTriggers.AB_Fear:
                    if (System.String.Equals(Globals.gamedata.my_char.Name.ToUpperInvariant(), Name))
                    {
                        if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.FEAR))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET", Name))
                    {
                        if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.FEAR))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET1", Name))
                    {
                        if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.FEAR))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET2", Name))
                    {
                        if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.FEAR))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET3", Name))
                    {
                        if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.FEAR))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else
                    {
                        CharInfo player = null;

                        Globals.PlayerLock.EnterReadLock();
                        try
                        {
                            player = Util.GetChar(Name);
                        }
                        finally
                        {
                            Globals.PlayerLock.ExitReadLock();
                        }

                        if (player != null)
                        {
                            if (player.HasEffect(AbnormalEffects.FEAR))
                            {
                                return BuffState.Needs;
                            }
                            return BuffState.Has;
                        }
                    }
                    break;
                case BuffTriggers.AB_Stun:
                    if (System.String.Equals(Globals.gamedata.my_char.Name.ToUpperInvariant(), Name))
                    {
                        if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.STUN))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET", Name))
                    {
                        if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.STUN))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET1", Name))
                    {
                        if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.STUN))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET2", Name))
                    {
                        if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.STUN))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET3", Name))
                    {
                        if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.STUN))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else
                    {
                        CharInfo player = null;

                        Globals.PlayerLock.EnterReadLock();
                        try
                        {
                            player = Util.GetChar(Name);
                        }
                        finally
                        {
                            Globals.PlayerLock.ExitReadLock();
                        }

                        if (player != null)
                        {
                            if (player.HasEffect(AbnormalEffects.STUN))
                            {
                                return BuffState.Needs;
                            }
                            return BuffState.Has;
                        }
                    }
                    break;
                case BuffTriggers.AB_Sleep:
                    if (System.String.Equals(Globals.gamedata.my_char.Name.ToUpperInvariant(), Name))
                    {
                        if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.SLEEP))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET", Name))
                    {
                        if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.SLEEP))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET1", Name))
                    {
                        if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.SLEEP))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET2", Name))
                    {
                        if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.SLEEP))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET3", Name))
                    {
                        if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.SLEEP))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else
                    {
                        CharInfo player = null;

                        Globals.PlayerLock.EnterReadLock();
                        try
                        {
                            player = Util.GetChar(Name);
                        }
                        finally
                        {
                            Globals.PlayerLock.ExitReadLock();
                        }

                        if (player != null)
                        {
                            if (player.HasEffect(AbnormalEffects.SLEEP))
                            {
                                return BuffState.Needs;
                            }
                            return BuffState.Has;
                        }
                    }
                    break;
                case BuffTriggers.AB_Muted:
                    if (System.String.Equals(Globals.gamedata.my_char.Name.ToUpperInvariant(), Name))
                    {
                        if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.MUTED))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET", Name))
                    {
                        if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.MUTED))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET1", Name))
                    {
                        if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.MUTED))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET2", Name))
                    {
                        if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.MUTED))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET3", Name))
                    {
                        if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.MUTED))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else
                    {
                        CharInfo player = null;

                        Globals.PlayerLock.EnterReadLock();
                        try
                        {
                            player = Util.GetChar(Name);
                        }
                        finally
                        {
                            Globals.PlayerLock.ExitReadLock();
                        }

                        if (player != null)
                        {
                            if (player.HasEffect(AbnormalEffects.MUTED))
                            {
                                return BuffState.Needs;
                            }
                            return BuffState.Has;
                        }
                    }
                    break;
                case BuffTriggers.AB_Root:
                    if (System.String.Equals(Globals.gamedata.my_char.Name.ToUpperInvariant(), Name))
                    {
                        if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.ROOT))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET", Name))
                    {
                        if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.ROOT))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET1", Name))
                    {
                        if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.ROOT))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET2", Name))
                    {
                        if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.ROOT))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET3", Name))
                    {
                        if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.ROOT))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }

                    else
                    {
                        CharInfo player = null;

                        Globals.PlayerLock.EnterReadLock();
                        try
                        {
                            player = Util.GetChar(Name);
                        }
                        finally
                        {
                            Globals.PlayerLock.ExitReadLock();
                        }

                        if (player != null)
                        {
                            if (player.HasEffect(AbnormalEffects.ROOT))
                            {
                                return BuffState.Needs;
                            }
                            return BuffState.Has;
                        }
                    }
                    break;
                case BuffTriggers.AB_Paralysis:
                    if (System.String.Equals(Globals.gamedata.my_char.Name.ToUpperInvariant(), Name))
                    {
                        if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.HOLD_1))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET", Name))
                    {
                        if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.HOLD_1))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET1", Name))
                    {
                        if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.HOLD_1))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET2", Name))
                    {
                        if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.HOLD_1))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET3", Name))
                    {
                        if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.HOLD_1))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else
                    {
                        CharInfo player = null;

                        Globals.PlayerLock.EnterReadLock();
                        try
                        {
                            player = Util.GetChar(Name);
                        }
                        finally
                        {
                            Globals.PlayerLock.ExitReadLock();
                        }

                        if (player != null)
                        {
                            if (player.HasEffect(AbnormalEffects.HOLD_1))
                            {
                                return BuffState.Needs;
                            }
                            return BuffState.Has;
                        }
                    }
                    break;
                case BuffTriggers.AB_Petrified:
                    if (System.String.Equals(Globals.gamedata.my_char.Name.ToUpperInvariant(), Name))
                    {
                        if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.PETRIFIED))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET", Name))
                    {
                        if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.PETRIFIED))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET1", Name))
                    {
                        if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.PETRIFIED))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET2", Name))
                    {
                        if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.PETRIFIED))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET3", Name))
                    {
                        if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.PETRIFIED))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else
                    {
                        CharInfo player = null;

                        Globals.PlayerLock.EnterReadLock();
                        try
                        {
                            player = Util.GetChar(Name);
                        }
                        finally
                        {
                            Globals.PlayerLock.ExitReadLock();
                        }

                        if (player != null)
                        {
                            if (player.HasEffect(AbnormalEffects.PETRIFIED))
                            {
                                return BuffState.Needs;
                            }
                            return BuffState.Has;
                        }
                    }
                    break;
                case BuffTriggers.AB_Invulnerable:
                    if (System.String.Equals(Globals.gamedata.my_char.Name.ToUpperInvariant(), Name))
                    {
                        if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.INVULNERABLE))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET", Name))
                    {
                        if (Globals.gamedata.my_pet.HasEffect(AbnormalEffects.INVULNERABLE))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET1", Name))
                    {
                        if (Globals.gamedata.my_pet1.HasEffect(AbnormalEffects.INVULNERABLE))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET2", Name))
                    {
                        if (Globals.gamedata.my_pet2.HasEffect(AbnormalEffects.INVULNERABLE))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else if (System.String.Equals("PET3", Name))
                    {
                        if (Globals.gamedata.my_pet3.HasEffect(AbnormalEffects.INVULNERABLE))
                        {
                            return BuffState.Needs;
                        }
                        return BuffState.Has;
                    }
                    else
                    {
                        CharInfo player = null;

                        Globals.PlayerLock.EnterReadLock();
                        try
                        {
                            player = Util.GetChar(Name);
                        }
                        finally
                        {
                            Globals.PlayerLock.ExitReadLock();
                        }

                        if (player != null)
                        {
                            if (player.HasEffect(AbnormalEffects.INVULNERABLE))
                            {
                                return BuffState.Needs;
                            }
                            return BuffState.Has;
                        }
                    }
                    break;
                default:
                    return BuffState.Has;
			}

            //couldnt find the player... they dont need shit

			//need to remove this line later to make sure all code paths return values
            return BuffState.Has;
		}

		public void Add_Buff(BuffTargetClass bft)
		{
			BuffTimer bt = new BuffTimer();
            bt.SkillID = bft.SkillID;
			bt.LastTickTime = 0;

			BuffTimes.Add(bt);
		}

		public void Add_Buff(uint sc_id, long tm)
		{
			BuffTimer bt = new BuffTimer();
            bt.SkillID = sc_id;
			bt.LastTickTime = tm;

			BuffTimes.Add(bt);
		}

		public long Get_Time(uint sc_id)
		{
			foreach(BuffTimer bt in BuffTimes)
			{
                if (bt.SkillID == sc_id)
				{
					return bt.LastTickTime;
				}
			}

			//couldnt find the skill
			//so return time of zero
			return 0;
		}

		public void Add_Time(uint sc_id, long tm)
		{
			foreach(BuffTimer bt in BuffTimes)
			{
                if (bt.SkillID == sc_id)
				{
					bt.LastTickTime += tm;
					return;
				}
			}
		}


		public void Set_Time(uint sc_id, long tm)
		{
			foreach(BuffTimer bt in BuffTimes)
			{
                if (bt.SkillID == sc_id)
				{
					bt.LastTickTime = tm;
					return;
				}
			}

			//couldnt find the skill
			//so lets add it to the list
			BuffTimer bt2 = new BuffTimer();
            bt2.SkillID = sc_id;
			bt2.LastTickTime = tm;
			BuffTimes.Add(bt2);
		}
	}
}
