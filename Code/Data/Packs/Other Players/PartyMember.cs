using System;

namespace L2_login
{
	public class PartyMember : Object_Base
	{
		private string _Name = "";
        public volatile float Cur_CP = 0;
        public volatile float Max_CP = 0;
        public volatile float Cur_HP = 0;
        public volatile float Max_HP = 0;
        public volatile float Cur_MP = 0;
        public volatile float Max_MP = 0;
        public volatile uint Level = 0;
        public volatile uint Class = 0;
        public volatile uint petID = 0;
        public volatile uint pet1ID = 0;
        public volatile uint pet2ID = 0;
        public volatile uint pet3ID = 0;
        public volatile int X = 0;
        public volatile int Y = 0;
        public volatile int Z = 0;
        public volatile int exp_bonus = 0; // god? 
        public volatile int exp_bonus2 = 0; // god?

		private readonly object NameLock = new object();

        private System.Collections.SortedList _my_buffs;
        private readonly object my_buffsLock = new object();
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
					_Name = value;
				}
			}
		}
        public System.Collections.SortedList my_buffs
        {
            get
            {
                System.Collections.SortedList tmp;
                lock (my_buffsLock)
                {
                    tmp = this._my_buffs;
                }
                return tmp;
            }
            set
            {
                lock (my_buffsLock)
                {
                    _my_buffs = value;
                }
            }
        }

        public PartyMember()
        {
            my_buffs = new System.Collections.SortedList();
		}

		public void Load(ByteBuffer buff)// (adifenix)
		{
			ID = buff.ReadUInt32();//System.BitConverter.ToUInt32(buff,off);off+=4;
			Name = buff.ReadString();//Util.Get_String(buff,ref off);
			Cur_CP = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,off);off+=4;
			Max_CP = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,off);off+=4;
			Cur_HP = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,off);off+=4;
			Max_HP = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,off);off+=4;
			Cur_MP = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,off);off+=4;
			Max_MP = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,off);off+=4;

            exp_bonus = buff.ReadInt32();

			Level = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,off);off+=4;
			Class = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,off);off+=4;
			buff.ReadUInt32();//x00 or x01 (sex)
			buff.ReadUInt32();//race

            buff.ReadUInt32();//
            buff.ReadUInt32();//

            buff.ReadUInt32();//??


            uint number_of_pets = buff.ReadUInt32();
            if (number_of_pets > 0)
            {
                for (int i = 0; i < number_of_pets; i++)
                {

                    switch (i)
                    {
                        case 0:
                            petID = buff.ReadUInt32();//petID
                            break;
                        case 1:
                            pet1ID = buff.ReadUInt32();//petID
                            break;
                        case 2:
                            pet2ID = buff.ReadUInt32();//petID
                            break;
                        case 3:
                            pet3ID = buff.ReadUInt32();//petID
                            break;
                        default:
                            buff.ReadUInt32();
                            break;
                    }


                    buff.ReadUInt32();//NPCID
                    buff.ReadUInt32();//type (pet or summon)
                    buff.ReadString();//name
                    buff.ReadUInt32();//cur hp
                    buff.ReadUInt32();//max hp
                    buff.ReadUInt32();//cur mp
                    buff.ReadUInt32();//max mp
                    buff.ReadUInt32();//level
                }
            }
            
			UpdataHealInfo();
		}

		public void LoadLoc(ByteBuffer buff)
		{
			X = buff.ReadInt32();//System.BitConverter.ToInt32(buff,off);off+=4;
			Y = buff.ReadInt32();//System.BitConverter.ToInt32(buff,off);off+=4;
			Z = buff.ReadInt32();//System.BitConverter.ToInt32(buff,off);off+=4;
		}

		public void Update(ByteBuffer buff)
		{
			Name = buff.ReadString();//Util.Get_String(buff,ref off);
			Cur_CP = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,off);off+=4;
			Max_CP = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,off);off+=4;
			Cur_HP = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,off);off+=4;
			Max_HP = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,off);off+=4;
			Cur_MP = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,off);off+=4;
			Max_MP = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,off);off+=4;
			Level = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,off);off+=4;
			Class = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,off);off+=4;
            if (Globals.gamedata.Chron >= Chronicle.CT3_0)
            {
                exp_bonus = buff.ReadInt32();
                exp_bonus2 = buff.ReadInt32();
            }

			UpdataHealInfo();
		}

		private void UpdataHealInfo()
		{
            CharInfo player = null;

            Globals.PlayerLock.EnterReadLock();
			try
			{
                player = Util.GetChar(ID);
    		}//unlock
			finally
			{
                Globals.PlayerLock.ExitReadLock();
			}

            if (player != null)
            {
                player.Cur_HP = Cur_HP;
                player.Max_HP = Max_HP;
                player.Cur_MP = Cur_MP;
                player.Max_MP = Max_MP;
                player.Cur_CP = Cur_CP;
                player.Max_CP = Max_CP;
            }
        }
	}//end of class
}
