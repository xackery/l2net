using System;

namespace L2_login
{
	public class SkillList
	{
		private System.Collections.ArrayList _Levels = new System.Collections.ArrayList();

		private readonly object LevelsLock = new object();

		public System.Collections.ArrayList Levels
		{
			get
			{
				System.Collections.ArrayList tmp;
				lock(LevelsLock)
				{
					tmp = this._Levels;
				}
				return tmp;
			}
			set
			{
				lock(LevelsLock)
				{
					_Levels = value;
				}
			}
		}

		public SkillList()
		{
			Levels.Clear();
		}

		public void Add_Level(SkillInfo sk_inf)
		{
			Levels.Add(sk_inf);
		}

		public SkillInfo GetLevel(uint lvl)
		{
			for(int i = 0; i < Levels.Count; i++)
			{
				if( ((SkillInfo)Levels[i]).Level == lvl)
					return (SkillInfo)(Levels[i]);
			}

			return new SkillInfo();
		}
	}
}
