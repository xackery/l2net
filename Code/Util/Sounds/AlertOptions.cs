using System;

namespace L2_login
{
	/// <summary>
	/// Summary description for SoundOptions.
	/// </summary>
	public class AlertOptions
	{
        //1 - start
        //2 - close
        //3 - error
        //4 - error
        //5 - error
        //6 - error
        //7 - garbage collect
        //8 - mail
        //9 - error

        public volatile bool beepon_2waywar;
        public volatile bool beepon_1waywar;
        public volatile bool beepon_n1waywar;
        public volatile bool beepon_hp;
        public volatile bool beepon_mp;
        public volatile bool beepon_cp;
        public volatile bool beepon_clan;
        public volatile bool beepon_player;

        public volatile bool beepon_clan_ignoreparty;
        public volatile bool beepon_player_ignoreparty;

        public volatile bool beepon_whitechat;
        public volatile bool beepon_privatemessage;
        public volatile bool beepon_friendchat;

        public volatile bool logouton_2waywar;
        public volatile bool logouton_1waywar;
        public volatile bool logouton_n1waywar;
        public volatile bool logouton_hp;
        public volatile bool logouton_mp;
        public volatile bool logouton_cp;
        public volatile bool logouton_clan;
        public volatile bool logouton_player;

        public volatile int hp_value;
        public volatile int mp_value;
        public volatile int cp_value;
        public volatile string _clan_value;
        public volatile string _player_value;

        public volatile int hp_value_logout;
        public volatile int mp_value_logout;
        public volatile int cp_value_logout;
        private string _clan_value_logout;
        private string _player_value_logout;

		private readonly object clan_valueLock = new object();
		private readonly object player_valueLock = new object();

		public AlertOptions()
		{
			beepon_2waywar = false;
			beepon_1waywar = false;
            beepon_n1waywar = false;
		}

		public string clan_value
		{
			get
			{
				string tmp;
				lock(clan_valueLock)
				{
					tmp = this._clan_value;
				}
				return tmp;
			}
			set
			{
				lock(clan_valueLock)
				{
					_clan_value = value.ToUpperInvariant();
				}
			}
		}
		public string player_value
		{
			get
			{
				string tmp;
				lock(player_valueLock)
				{
					tmp = this._player_value;
				}
				return tmp;
			}
			set
			{
				lock(player_valueLock)
				{
					_player_value = value.ToUpperInvariant();
				}
			}
		}

        public string clan_value_logout
        {
            get
            {
                string tmp;
                lock (clan_valueLock)
                {
                    tmp = this._clan_value_logout;
                }
                return tmp;
            }
            set
            {
                lock (clan_valueLock)
                {
                    _clan_value_logout = value.ToUpperInvariant();
                }
            }
        }
        public string player_value_logout
        {
            get
            {
                string tmp;
                lock (player_valueLock)
                {
                    tmp = this._player_value_logout;
                }
                return tmp;
            }
            set
            {
                lock (player_valueLock)
                {
                    _player_value_logout = value.ToUpperInvariant();
                }
            }
        }
	}//end of class
}//end of namespace