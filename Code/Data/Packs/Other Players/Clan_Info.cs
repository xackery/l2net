using System;

namespace L2_login
{
	public class Clan_Info : Object_Base
	{
        public volatile uint AllyID = 0;
        public volatile uint CrestID = 0;
        public volatile uint LargeCrestID = 0;
		private string _ClanName = "";
		private string _AllyName = "";

		private readonly object ClanNameLock = new object();
		private readonly object AllyNameLock = new object();

		public string ClanName
		{
			get
			{
				string tmp;
				lock(ClanNameLock)
				{
					tmp = this._ClanName;
				}
				return tmp;
			}
			set
			{
				lock(ClanNameLock)
				{
					_ClanName = value;
				}
			}
		}
		public string AllyName
		{
			get
			{
				string tmp;
				lock(AllyNameLock)
				{
					tmp = this._AllyName;
				}
				return tmp;
			}
			set
			{
				lock(AllyNameLock)
				{
					_AllyName = value;
				}
			}
		}

		public Clan_Info()
		{
			ID = 0;
			AllyID = 0;
			CrestID = 0;
			LargeCrestID = 0;
			ClanName = "";
			AllyName = "";
		}

		public void Load(ByteBuffer buff)
		{
			ID = buff.ReadUInt32();
			ClanName = buff.ReadString();
			AllyName = buff.ReadString();
		}
	}
}
