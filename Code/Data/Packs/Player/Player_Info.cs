using System;
namespace L2_login
{
	/// <summary>
	/// Summary description for Char_Info.
	/// </summary>
	public class Player_Info : Object_Base
	{
		private string _Name = "";
		private string _Title = "";
        public volatile uint Sex = 0;
        public volatile uint Race = 0;
        public volatile uint Class = 0;
        public volatile uint BaseClass = 0;
        public volatile uint ActiveClass = 0;
        public volatile uint Active = 0;
        public volatile float X = 0;
        public volatile float Y = 0;
        public volatile float Z = 0;
        public volatile float Cur_HP = 0;
        public volatile float Cur_MP = 0;
        public volatile uint SP = 0;
        private ulong _XP = 0;
        public volatile float XPPercent = 0;

        public volatile uint Level = 0;
        public volatile uint INT = 0;
        public volatile uint STR = 0;
        public volatile uint CON = 0;
        public volatile uint MEN = 0;
        public volatile uint DEX = 0;
        public volatile uint WIT = 0;
        public volatile uint charID = 0;

        public volatile float Max_HP = 0;
        public volatile float Max_MP = 0;

        public volatile float Cur_CP = 0;
        public volatile float Max_CP = 0;

        public volatile float Dest_X = 0;
        public volatile float Dest_Y = 0;
        public volatile float Dest_Z = 0;
        public volatile bool Moving = false;
		private System.DateTime _lastMoveTime = System.DateTime.Now;
        public volatile uint MoveTarget = 0;
        public volatile TargetType MoveTargetType = TargetType.NONE;
		private System.DateTime _lastVerifyTime = System.DateTime.Now;

        public volatile int Heading = 0;

        public volatile uint Weapon_Equipped = 0;
        public volatile int Fame = 0;
        public volatile int Allow_Minimap = 0;
        public volatile int Vitality_Points = 0;
        public volatile uint ExtendedEffects = 0;

        public volatile uint Cur_Load = 0;
        public volatile uint Max_Load = 0;

        public volatile uint obj_Under = 0;
        public volatile uint obj_REar = 0;
        public volatile uint obj_LEar = 0;
        public volatile uint obj_Neck = 0;
        public volatile uint obj_RFinger = 0;
        public volatile uint obj_LFinger = 0;
        public volatile uint obj_Head = 0;
        public volatile uint obj_RHand = 0;
        public volatile uint obj_LHand = 0;
        public volatile uint obj_Gloves = 0;
        public volatile uint obj_Chest = 0;
        public volatile uint obj_Legs = 0;
        public volatile uint obj_Feet = 0;
        public volatile uint obj_Back = 0;
        public volatile uint obj_LRHand = 0;
        public volatile uint obj_Hair = 0;
        public volatile uint obj_Face = 0;

        public volatile uint itm_Under = 0;
        public volatile uint itm_REar = 0;
        public volatile uint itm_LEar = 0;
        public volatile uint itm_Neck = 0;
        public volatile uint itm_RFinger = 0;
        public volatile uint itm_LFinger = 0;
        public volatile uint itm_Head = 0;
        public volatile uint itm_RHand = 0;
        public volatile uint itm_LHand = 0;
        public volatile uint itm_Gloves = 0;
        public volatile uint itm_Chest = 0;
        public volatile uint itm_Legs = 0;
        public volatile uint itm_Feet = 0;
        public volatile uint itm_Back = 0;
        public volatile uint itm_LRHand = 0;
        public volatile uint itm_Hair = 0;
        public volatile uint itm_Face = 0;
        public volatile uint itm_rbracelet = 0;
        public volatile uint itm_lbracelet = 0;
        public volatile uint itm_talisman1 = 0;
        public volatile uint itm_talisman2 = 0;
        public volatile uint itm_talisman3 = 0;
        public volatile uint itm_talisman4 = 0;
        public volatile uint itm_talisman5 = 0;
        public volatile uint itm_talisman6 = 0;


        public volatile uint aug_Under = 0;
        public volatile uint aug_REar = 0;
        public volatile uint aug_LEar = 0;
        public volatile uint aug_Neck = 0;
        public volatile uint aug_RFinger = 0;
        public volatile uint aug_LFinger = 0;
        public volatile uint aug_Head = 0;
        public volatile uint aug_RHand = 0;
        public volatile uint aug_LHand = 0;
        public volatile uint aug_Gloves = 0;
        public volatile uint aug_Chest = 0;
        public volatile uint aug_Legs = 0;
        public volatile uint aug_Feet = 0;
        public volatile uint aug_Back = 0;
        public volatile uint aug_LRHand = 0;
        public volatile uint aug_Hair = 0;
        public volatile uint aug_Face = 0;
        public volatile uint aug_rbracelet = 0;
        public volatile uint aug_lbracelet = 0;
        public volatile uint aug_talisman1 = 0;
        public volatile uint aug_talisman2 = 0;
        public volatile uint aug_talisman3 = 0;
        public volatile uint aug_talisman4 = 0;
        public volatile uint aug_talisman5 = 0;
        public volatile uint aug_talisman6 = 0;

        public volatile uint Patk = 0;
        public volatile uint PatkSpeed = 0;
        public volatile uint PDef = 0;
        public volatile uint Evasion = 0;
        public volatile uint Accuracy = 0;
        public volatile uint Focus = 0;
        public volatile uint Matk = 0;
        public volatile uint MatkSpeed = 0;
        public volatile uint MDef = 0;

        public volatile uint PvPFlag = 0;
        public volatile int Karma = 0;

        public volatile float RunSpeed = 0;
        public volatile float WalkSpeed = 0;
        public volatile uint SwimRunSpeed = 0;
        public volatile uint SwimWalkSpeed = 0;
        public volatile uint flRunSpeed = 0;
        public volatile uint flWalkSpeed = 0;
        public volatile uint FlyRunSpeed = 0;
        public volatile uint FlyWalkSpeed = 0;

        public volatile float MoveSpeedMult = 0;
        public volatile float AttackSpeedMult = 0;
        public volatile float CollisionRadius = 0;
        public volatile float CollisionHeight = 0;

        public volatile uint HairSytle = 0;
        public volatile uint HairColor = 0;
        public volatile uint Face = 0;
        public volatile uint AccessLevel = 0;

        public volatile uint ClanID = 0;
        public volatile uint ClanCrestID = 0;
        public volatile uint AllyID = 0;
        public volatile uint AllyCrestID = 0;
        public volatile uint isClanLeader = 0;
        public volatile uint MountType = 0;//byte
        public volatile uint PrivateStoreType = 0;//byte
        public volatile uint hasDwarfCraft = 0;//byte
        public volatile uint PKCount = 0;
        public volatile uint PvPCount = 0;

        public volatile uint CubicCount = 0;//ushort
		private System.Collections.ArrayList _Cubics = new System.Collections.ArrayList();

        private System.Collections.ArrayList _AbnEffects = new System.Collections.ArrayList();

        public volatile uint FindParty = 0;//byte

        public volatile uint AbnormalEffects = 0;
        private ulong _ClanPrivileges = 0;
        public volatile uint isRunning = 1;
        public volatile uint isSitting = 1;
        public volatile uint isAlikeDead = 0;

        public volatile uint RecLeft = 0;//ushort
        public volatile uint RecAmount = 0;//0 = white | 255 = blue//ushort
        public volatile uint InventoryLimit = 0;//ushort
        public volatile uint SpecialEffects = 0;
        public volatile uint EnchantAmount = 0;//byte

        public volatile uint TeamCircle = 0;//1= Blue, 2 = red//byte

        public volatile uint ClanCrestIDLarge = 0;

        public volatile uint HeroIcon = 0;//byte
        public volatile uint HeroGlow = 0;//byte

        public volatile uint isFishing = 0;//0x01 - fishing//byte
        public volatile int FishX = 0;
        public volatile int FishY = 0;
        public volatile int FishZ = 0;

        public volatile uint NameColor = 0;
        public volatile uint TitleColor = 0;
        public volatile uint PledgeClass = 0;
        public volatile uint DemonSword = 0;

        public volatile int Transform_ID = 0;
        public volatile int Agathon_ID = 0;

        public volatile uint Symbol1 = 0;
        public volatile uint Symbol2 = 0;
        public volatile uint Symbol3 = 0;
        public volatile uint MaxTats = 0;

        public volatile uint TargetID = 0;
        public volatile uint TargetColor = 0;//ushort
        public volatile TargetType CurrentTargetType = TargetType.NONE;
        public volatile bool TargetSpoiled = false;
        public volatile bool CannotSeeTarget = false;

        public volatile uint LastTarget = 0;
        public volatile uint BuffTarget = 0;
        public volatile uint BuffTargetLast = 0;
        public volatile uint BuffNeedTarget = 0;
        public volatile uint BuffSkillID = 0;
		private System.DateTime _lastbufftime;

        public volatile uint isInCombat = 0;//byte
        public volatile bool isAttacking = false;

        public volatile uint Charges = 0;
        public volatile uint Souls = 0;
        public volatile uint DeathPenalty = 0;

        public volatile uint HitTime = 0;
        public double ExpireFactor = 1.0;
        public long ExpiresTime = 0;
        public volatile uint Resisted = 0;

        public volatile int AtkAttrib = 0;
        public volatile int AtkAttribVal = 0;

        public volatile int DefAttribFire = 0;
        public volatile int DefAttribWater = 0;
        public volatile int DefAttribWind = 0;
        public volatile int DefAttribEarth = 0;
        public volatile int DefAttribHoly = 0;
        public volatile int DefAttribUnholy = 0;
        public volatile uint MAccuracy = 0;
        public volatile uint MEvasion = 0;
        public volatile uint MCritical = 0;

		//need to keep track of:
		//sit/stand
		//in party
		//party leader
		//loot type
		
		private readonly object lastbufftimeLock = new object();
		private readonly object lastVerifyTimeLock = new object();
		private readonly object lastMoveTimeLock = new object();
        private readonly object ClanPrivilegesLock = new object();
        private readonly object XPLock = new object();
		private readonly object CubicsLock = new object();
        private readonly object AbnEffectsLock = new object();
		private readonly object TitleLock = new object();
		private readonly object NameLock = new object();

        public bool HasEffect(AbnormalEffects test)
        {
            return AbnEffects.IndexOf((uint)test) != -1;
        }
        
        public bool HasExtendedEffect(ExtendedEffects test)
        {
            return (ExtendedEffects & (uint)test) != 0;
        }

        public bool MyCharRelation(MyRelation test)
        {
            return (isClanLeader & (uint)test) != 0;
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
					_Name = value;
				}
                Globals.l2net_home.SetName();
			}
		}
		public string Title
		{
			get
			{
				string tmp;
				lock(TitleLock)
				{
					tmp = this._Title;
				}
				return tmp;
			}
			set
			{
				lock(TitleLock)
				{
					_Title = value;
				}
			}
		}
		public System.Collections.ArrayList Cubics
		{
			get
			{
				System.Collections.ArrayList tmp;
				lock(CubicsLock)
				{
					tmp = this._Cubics;
				}
				return tmp;
			}
			set
			{
				lock(CubicsLock)
				{
					_Cubics = value;
				}
			}
		}

        public System.Collections.ArrayList AbnEffects
        {
            get
            {
                System.Collections.ArrayList tmp;
                lock (AbnEffectsLock)
                {
                    tmp = this._AbnEffects;
                }
                return tmp;
            }
            set
            {
                lock (AbnEffectsLock)
                {
                    _AbnEffects = value;
                }
            }
        }

		public ulong XP
		{
			get
			{
				ulong tmp;
				lock(XPLock)
				{
					tmp = this._XP;
				}
				return tmp;
			}
			set
			{
				lock(XPLock)
				{
					_XP = value;
				}
			}
		}

        public ulong ClanPrivileges
        {
            get
            {
                ulong tmp;
                lock (ClanPrivilegesLock)
                {
                    tmp = this._ClanPrivileges;
                }
                return tmp;
            }
            set
            {
                lock (ClanPrivilegesLock)
                {
                    _ClanPrivileges = value;
                }
            }
        }
		public System.DateTime LastBuffTime
		{
			get
			{
				System.DateTime tmp;
				lock(lastbufftimeLock)
				{
					tmp = this._lastbufftime;
				}
				return tmp;
			}
			set
			{
				lock(lastbufftimeLock)
				{
					_lastbufftime = value;
				}
			}
		}
		public System.DateTime lastMoveTime
		{
			get
			{
				System.DateTime tmp;
				lock(lastMoveTimeLock)
				{
					tmp = this._lastMoveTime;
				}
				return tmp;
			}
			set
			{
				lock(lastMoveTimeLock)
				{
					_lastMoveTime = value;
				}
			}
		}
		public System.DateTime lastVerifyTime
		{
			get
			{
				System.DateTime tmp;
				lock(lastVerifyTimeLock)
				{
					tmp = this._lastVerifyTime;
				}
				return tmp;
			}
			set
			{
				lock(lastVerifyTimeLock)
				{
					_lastVerifyTime = value;
				}
			}
		}

        public bool CanBuff()
        {
            if (BuffNeedTarget == 0)
            {
                return true;
            }
            if (BuffTarget == TargetID)
            {
                return true;
            }
            return false;
        }

		public Player_Info()
		{
			Moving = false;
			TargetSpoiled = false;
			lastVerifyTime = System.DateTime.Now;
            Globals.gamedata.BOT_STATE = BotState.Nothing;
			BuffTarget = 0;
            BuffTargetLast = 0;
			Clear_Skills();
            Clear_MyBuffs();
            Clear_Party();
		}

        private void Clear_Party()
        {
            Globals.PartyLock.EnterWriteLock();
            try
            {
                Globals.gamedata.PartyMembers.Clear();
                Globals.gamedata.PartyCount = 0;
            }
            finally
            {
                Globals.PartyLock.ExitWriteLock();
            }
        }

		private void Clear_Skills()
		{
            Globals.SkillListLock.EnterWriteLock();
			try
			{
                Globals.gamedata.skills.Clear();
			}
			finally
			{
                Globals.SkillListLock.ExitWriteLock();
			}
		}

        private void Clear_MyBuffs()
        {
            Globals.MyBuffsListLock.EnterWriteLock();
            try
            {
                Globals.gamedata.mybuffs.Clear();
            }
            finally
            {
                Globals.MyBuffsListLock.ExitWriteLock();
            }
        }
        public void LoadStatsEX(ByteBuffer buff)
        {
            ID = buff.ReadUInt32();//A0 B9 B0 49
            // still working on this
            // next one is 1-4 bytes probably bit mask ...


         }
        public void LoadItemsEX(ByteBuffer buff)
        {
            ID = buff.ReadUInt32();//A0 B9 B0 49
            obj_Under = buff.ReadUInt32();//0 0 0 0
            obj_REar = buff.ReadUInt32();//F 46 8B 40
            obj_LEar = buff.ReadUInt32();//91 E5 78 40
            obj_Neck = buff.ReadUInt32();//58 E8 50 40
            obj_RFinger = buff.ReadUInt32();//64 A1 87 40
            obj_LFinger = buff.ReadUInt32();//D9 64 83 40
            obj_Head = buff.ReadUInt32();//41 13 89 40
            obj_RHand = buff.ReadUInt32();//C2 E4 88 40
            obj_LHand = buff.ReadUInt32();//97 E6 89 40
            obj_Gloves = buff.ReadUInt32();//37 39 8B 40
            obj_Chest = buff.ReadUInt32();//DE 16 8B 40
            obj_Legs = buff.ReadUInt32();//93 1A 87 40
            obj_Feet = buff.ReadUInt32();//9C FA 84 40
            obj_Back = buff.ReadUInt32();//0 0 0 0
            obj_LRHand = buff.ReadUInt32();//0 0 0 0
            obj_Hair = buff.ReadUInt32();//E1 43 8B 40
            obj_Face = buff.ReadUInt32();

            //GD OK

            buff.ReadUInt32();//C7 //right bracelet
            buff.ReadUInt32();//C7 //left bracelet
            buff.ReadUInt32();//C7 //deco 1
            buff.ReadUInt32();//C7 //deco 2
            buff.ReadUInt32();//C7 //deco 3
            buff.ReadUInt32();//C7 //deco 4
            buff.ReadUInt32();//C7 //deco 5
            buff.ReadUInt32();//C7 //deco 6
            buff.ReadUInt32();//CT2.3 - belt obj id


            itm_Under = buff.ReadUInt32();//0 0 0 0
            itm_REar = buff.ReadUInt32();//5 1A 0 0
            itm_LEar = buff.ReadUInt32();//3 1A 0 0
            itm_Neck = buff.ReadUInt32();//98 3 0 0
            itm_RFinger = buff.ReadUInt32();//6 1A 0 0
            itm_LFinger = buff.ReadUInt32();//79 3 0 0
            itm_Head = buff.ReadUInt32();//23 2 0 0
            itm_RHand = buff.ReadUInt32();//B9 19 0 0
            itm_LHand = buff.ReadUInt32();//E9 18 0 0
            itm_Gloves = buff.ReadUInt32();//8A 16 0 0
            itm_Chest = buff.ReadUInt32();//60 9 0 0
            itm_Legs = buff.ReadUInt32();//65 9 0 0
            itm_Feet = buff.ReadUInt32();//96 16 0 0
            itm_Back = buff.ReadUInt32();//0 0 0 0
            itm_LRHand = buff.ReadUInt32();//0 0 0 0
            itm_Hair = buff.ReadUInt32();//E 1E 0 0
            itm_Face = buff.ReadUInt32();

            itm_rbracelet = buff.ReadUInt32();//C7 //right bracelet
            itm_lbracelet = buff.ReadUInt32();//C7 //left bracelet
            itm_talisman1 = buff.ReadUInt32();//C7 //deco 1
            itm_talisman2 = buff.ReadUInt32();//C7 //deco 2
            itm_talisman3 = buff.ReadUInt32();//C7 //deco 3
            itm_talisman4 = buff.ReadUInt32();//C7 //deco 4
            itm_talisman5 = buff.ReadUInt32();//C7 //deco 5
            itm_talisman6 = buff.ReadUInt32();//C7 //deco 6
            buff.ReadUInt32();//CT2.3 belt itm id 

            //Lindivor just a bunch of zeroes..
            for (int i = 0; i < 35; i++)
            {
                buff.ReadUInt32();
            }

            //GD OK
        }

        public void Load_UserEX(ByteBuffer buff)
        {
            ID = buff.ReadUInt32();
            buff.ReadInt32();//8B 01 00 00 //size
            buff.ReadByte(); //17
            buff.ReadInt32();//00 FF FF FE 
            buff.ReadInt32();//00 00 00 00 
            buff.ReadInt16();//28 00
            buff.ReadInt16();//0C 00

            Name = buff.ReadString(); //54 00 75 00 6C 00 6C 00 61 00 6D 00 6F 00 72 00 65 00 44 00 65 00 77 00 00 00 //tullamoreDew
            
            buff.ReadByte();//00
            buff.ReadInt32(); //00 00 00 00 
            buff.ReadInt32(); //08 00 00 00

            Level = (uint)buff.ReadByte(); //3E

            buff.ReadInt16(); //0E 00 
            STR = buff.ReadUInt16();
            DEX = buff.ReadUInt16();
            CON = buff.ReadUInt16();
            INT = buff.ReadUInt16();
            WIT = buff.ReadUInt16();
            MEN = buff.ReadUInt16();

            buff.ReadInt16(); //0E 00 
            Max_HP = buff.ReadInt32(); //EE 0C 00 00
            Max_MP = buff.ReadInt32(); //30 04 00 00 
            Max_CP = buff.ReadInt32(); //1C 07 00 00 

            buff.ReadInt16(); //26 00 
            Cur_HP = buff.ReadInt32(); //EE 0C 00 00 
            Cur_MP = buff.ReadInt32(); //30 04 00 00 
            Cur_CP = buff.ReadInt32(); //1C 07 00 00 
            Vitality_Points = buff.ReadInt32(); //CF 5D 18 00 
            buff.ReadInt32(); //00 00 00 00
            XP = buff.ReadUInt64(); //4A 96 44 0A 00 00 00 00
            XPPercent = System.Convert.ToSingle(buff.ReadDouble()); //F7 53 D6 BD C3 23 CC 3F

            buff.ReadInt16(); //03 00
            buff.ReadByte(); //00

            buff.ReadInt16(); //0F 00 
            buff.ReadInt32(); //04 00 00 00 
            buff.ReadInt32(); //02 00 00 00
            buff.ReadInt32(); //00 00 00 00 
            buff.ReadByte(); //00


            buff.ReadInt16(); //05 00
            buff.ReadInt16(); //00 00
            buff.ReadByte(); //00

            buff.ReadInt16(); //38 00
            buff.ReadInt16(); //14 00

            Patk = buff.ReadUInt32(); //0C 00 00 00 //patk
            PatkSpeed = buff.ReadUInt32(); //86 01 00 00 //atkspd
            PDef = buff.ReadUInt32(); //9E 00 00 00 //pdef
            Evasion = buff.ReadUInt32(); //67 00 00 00 //pevasion
            Accuracy = buff.ReadUInt32(); //63 00 00 00 //paccuracy
            Focus = buff.ReadUInt32(); //42 00 00 00 //pcritical
            Matk = buff.ReadUInt32(); //0A 00 00 00 //matk
            MatkSpeed = buff.ReadUInt32(); //E4 00 00 00 //castspd
            buff.ReadUInt32(); //86 01 00 00 //atkspd again?
            MEvasion = buff.ReadUInt32(); //8E 00 00 00 //mevasion
            MDef = buff.ReadUInt32(); //50 00 00 00 //mdef
            MAccuracy = buff.ReadUInt32(); //8E 00 00 00 //maccuracy
            MCritical = buff.ReadUInt32(); //22 00 00 00 //mcrit

            buff.ReadInt16(); //0E 00 
            buff.ReadInt32(); //00 00 00 00 
            buff.ReadInt32(); //00 00 00 00 
            buff.ReadInt32(); //00 00 00 00 

            buff.ReadInt16(); //12 00 
            X = buff.ReadInt32(); //54 9D 00 00
            Y = buff.ReadInt32(); //1A D1 00 00
            Z = buff.ReadInt32(); //10 F3 FF FF
            Heading = buff.ReadInt32(); //00 00 00 00

            buff.ReadInt16(); //12 00 
            buff.ReadInt16();//84 00 
            buff.ReadInt16();//52 00 
            buff.ReadInt16();//32 00
            buff.ReadInt16();//32 00 
            buff.ReadInt32();//00 00 00 00 
            buff.ReadInt32();//00 00 00 00

            buff.ReadInt16(); //12 00 
            System.Convert.ToSingle(buff.ReadDouble()); //55 55 55 55 55 55 F1 3F <-- not working
            RunSpeed = 143; //Ugly
            MoveSpeedMult = System.Convert.ToSingle(buff.ReadDouble()); //31 0B ED 9C 66 81 F6 3F 

            buff.ReadInt16(); //12 00 
            buff.ReadInt32(); //00 00 00 00 
            buff.ReadInt16(); //00 00
            buff.ReadInt16(); //22 40 
            buff.ReadInt16(); //00 00
            buff.ReadInt32(); //00 00 00 00
            buff.ReadInt16(); //37 40 

            buff.ReadInt16(); //05 00 
            buff.ReadInt16(); //FE 00 
            buff.ReadByte(); //00 

            buff.ReadInt16(); //20 00 
            buff.ReadInt32(); //00 00 00 00 
            buff.ReadInt32(); //00 00 00 00 
            buff.ReadInt32(); //00 00 00 00 
            buff.ReadInt32(); //00 00 00 00 
            buff.ReadInt32(); //00 00 00 00 
            buff.ReadInt32(); //00 00 00 00 
            buff.ReadInt32(); //00 00 00 00 
            buff.ReadInt16(); //00 00

            buff.ReadInt16();//16 00
            buff.ReadInt32();//00 00 00 00 
            buff.ReadInt32();//00 00 00 00 
            buff.ReadInt32();//00 00 00 00 
            RecAmount = buff.ReadUInt32();//00 00 00 00 
            RecLeft = buff.ReadUInt32(); //14 00 00 00 

            buff.ReadInt16(); //0B 00 
            buff.ReadByte(); //E0
            buff.ReadByte(); //22 
            buff.ReadInt32(); //02 00 00 00 
            buff.ReadInt16(); //00 00 
            buff.ReadByte(); //00 
            
            buff.ReadInt16(); //09 00
            buff.ReadInt32(); //00 00 00 00
            buff.ReadInt16(); //00 00
            buff.ReadByte(); //00

            buff.ReadInt16();//04 00 
            buff.ReadByte(); //00
            buff.ReadByte(); //01

            buff.ReadInt16(); //0A 00 
            buff.ReadInt32();//FF FF FF 00 
            buff.ReadInt32();//A2 F9 EC 00 


            buff.ReadInt16();//09 00 
            buff.ReadInt32();//00 00 00 00
            buff.ReadInt16();//50 00 
            buff.ReadByte();//00 


            buff.ReadInt16(); //0D 00
            buff.ReadInt32();//01 00 00 00 
            buff.ReadInt32();//00 00 00 00 
            buff.ReadInt16();//00 00
            buff.ReadByte();//00  


        }

        public void Load_User(ByteBuffer buff)
		{

            //tauti dddddSddddQfddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddddhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhddddddddddddddddddddddddddddddddddffffddddSdddddcccddh

            if (Globals.gamedata.Chron >= Chronicle.CT3_1)
            {
                X = buff.ReadInt32();//E7 3F 2 0
                Y = buff.ReadInt32();//9D 64 0 0
                Z = buff.ReadInt32();//28 F8 FF FF
                Heading = buff.ReadInt32();//0 0 0 0
                ID = buff.ReadUInt32();//A0 B9 B0 49

                Name = buff.ReadString();//4B 0 61 0 72 0 76 0 6F 0 6B 0 0 0

                Race = buff.ReadUInt32();//0 0 0 0
                Sex = buff.ReadUInt32();//1 0 0 0
                BaseClass = buff.ReadUInt32();//61 0 0 0 //base class
                Level = buff.ReadUInt32();//4C 0 0 0

                XP = buff.ReadUInt64();//FB 62 76 43 0 0 0 0
                if (ID == Globals.gamedata.my_char.ID && !Globals.gamedata.initial_XP_Gained_received)
                {
                    //get initial time
                    DateTime temp = DateTime.Now;
                    Globals.start_time = temp;

                    Globals.gamedata.initial_XP_Gained_received = true;
                    GameData.initial_XP = XP;
                }

                XPPercent = System.Convert.ToSingle(buff.ReadDouble());

                STR = buff.ReadUInt32();//12 0 0 0
                DEX = buff.ReadUInt32();//15 0 0 0
                CON = buff.ReadUInt32();//1F 0 0 0
                INT = buff.ReadUInt32();//27 0 0 0
                WIT = buff.ReadUInt32();//1A 0 0 0
                MEN = buff.ReadUInt32();//23 0 0 0
                Max_HP = buff.ReadUInt32();//9B 11 0 0
                Cur_HP = buff.ReadUInt32();//9B 11 0 0
                Max_MP = buff.ReadUInt32();//F F 0 0
                Cur_MP = buff.ReadUInt32();//F F 0 0

                SP = buff.ReadUInt32();//CA DF 59 0
                if (ID == Globals.gamedata.my_char.ID && !Globals.gamedata.initial_SP_Gained_received)
                {
                    Globals.gamedata.initial_SP_Gained_received = true;
                    GameData.initial_SP = SP;
                }


                Cur_Load = buff.ReadUInt32();//CE 41 0 0
                Max_Load = buff.ReadUInt32();//7C 28 1 0


                Weapon_Equipped = buff.ReadUInt32(); // 20 no weapon, 40 weapon equip

                obj_Under = buff.ReadUInt32();//0 0 0 0
                obj_REar = buff.ReadUInt32();//F 46 8B 40
                obj_LEar = buff.ReadUInt32();//91 E5 78 40
                obj_Neck = buff.ReadUInt32();//58 E8 50 40
                obj_RFinger = buff.ReadUInt32();//64 A1 87 40
                obj_LFinger = buff.ReadUInt32();//D9 64 83 40
                obj_Head = buff.ReadUInt32();//41 13 89 40
                obj_RHand = buff.ReadUInt32();//C2 E4 88 40
                obj_LHand = buff.ReadUInt32();//97 E6 89 40
                obj_Gloves = buff.ReadUInt32();//37 39 8B 40
                obj_Chest = buff.ReadUInt32();//DE 16 8B 40
                obj_Legs = buff.ReadUInt32();//93 1A 87 40
                obj_Feet = buff.ReadUInt32();//9C FA 84 40
                obj_Back = buff.ReadUInt32();//0 0 0 0
                obj_LRHand = buff.ReadUInt32();//0 0 0 0
                obj_Hair = buff.ReadUInt32();//E1 43 8B 40
                obj_Face = buff.ReadUInt32();

                //GD OK

                buff.ReadUInt32();//C7 //right bracelet
                buff.ReadUInt32();//C7 //left bracelet
                buff.ReadUInt32();//C7 //deco 1
                buff.ReadUInt32();//C7 //deco 2
                buff.ReadUInt32();//C7 //deco 3
                buff.ReadUInt32();//C7 //deco 4
                buff.ReadUInt32();//C7 //deco 5
                buff.ReadUInt32();//C7 //deco 6
                buff.ReadUInt32();//CT2.3 - belt obj id


                itm_Under = buff.ReadUInt32();//0 0 0 0
                itm_REar = buff.ReadUInt32();//5 1A 0 0
                itm_LEar = buff.ReadUInt32();//3 1A 0 0
                itm_Neck = buff.ReadUInt32();//98 3 0 0
                itm_RFinger = buff.ReadUInt32();//6 1A 0 0
                itm_LFinger = buff.ReadUInt32();//79 3 0 0
                itm_Head = buff.ReadUInt32();//23 2 0 0
                itm_RHand = buff.ReadUInt32();//B9 19 0 0
                itm_LHand = buff.ReadUInt32();//E9 18 0 0
                itm_Gloves = buff.ReadUInt32();//8A 16 0 0
                itm_Chest = buff.ReadUInt32();//60 9 0 0
                itm_Legs = buff.ReadUInt32();//65 9 0 0
                itm_Feet = buff.ReadUInt32();//96 16 0 0
                itm_Back = buff.ReadUInt32();//0 0 0 0
                itm_LRHand = buff.ReadUInt32();//0 0 0 0
                itm_Hair = buff.ReadUInt32();//E 1E 0 0
                itm_Face = buff.ReadUInt32();

                itm_rbracelet = buff.ReadUInt32();//C7 //right bracelet
                itm_lbracelet = buff.ReadUInt32();//C7 //left bracelet
                itm_talisman1 = buff.ReadUInt32();//C7 //deco 1
                itm_talisman2 = buff.ReadUInt32();//C7 //deco 2
                itm_talisman3 = buff.ReadUInt32();//C7 //deco 3
                itm_talisman4 = buff.ReadUInt32();//C7 //deco 4
                itm_talisman5 = buff.ReadUInt32();//C7 //deco 5
                itm_talisman6 = buff.ReadUInt32();//C7 //deco 6
                buff.ReadUInt32();//CT2.3 belt itm id 

                //GD OK

                //hhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhhh
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();
                buff.ReadUInt16();



                buff.ReadUInt32();
                buff.ReadUInt32();
                buff.ReadUInt32();
                buff.ReadUInt32();
                buff.ReadUInt32();
                buff.ReadUInt32(); 
                buff.ReadUInt32(); 
                buff.ReadUInt32(); 
                buff.ReadUInt32(); 
                buff.ReadUInt32(); 
                buff.ReadUInt32(); 

                //GD OK

                Patk = buff.ReadUInt32();//F8 1 0 0
                PatkSpeed = buff.ReadUInt32();//FD 1 0 0
                PDef = buff.ReadUInt32();//C3 2 0 0
                Evasion = buff.ReadUInt32();//5F 0 0 0
                Accuracy = buff.ReadUInt32();//71 0 0 0
                Focus = buff.ReadUInt32();//28 0 0 0
                Matk = buff.ReadUInt32();//99 2 0 0
                MatkSpeed = buff.ReadUInt32();//DD 2 0 0
                PatkSpeed = buff.ReadUInt32();//twice...who knows why//FD 1 0 0
                MDef = buff.ReadUInt32();//22 5 0 0
                MAccuracy = buff.ReadUInt32(); //m.accuracy
                MEvasion = buff.ReadUInt32(); //m.evasion
                MCritical = buff.ReadUInt32(); //m.critical
                PvPFlag = buff.ReadUInt32();//0 0 0 0
                Karma = buff.ReadInt32();//0 0 0 0

                RunSpeed = buff.ReadUInt32();//78 0 0 0
                WalkSpeed = buff.ReadUInt32();//4E 0 0 0
                SwimRunSpeed = buff.ReadUInt32();//32 0 0 0
                SwimWalkSpeed = buff.ReadUInt32();//32 0 0 0
                flRunSpeed = buff.ReadUInt32();//0 0 0 0
                flWalkSpeed = buff.ReadUInt32();//0 0 0 0
                FlyRunSpeed = buff.ReadUInt32();//0 0 0 0
                FlyWalkSpeed = buff.ReadUInt32();//0 0 0 0

                //GD OK

                MoveSpeedMult = System.Convert.ToSingle(buff.ReadDouble());//8F C2 F5 28 5C 8F F4 3F
                AttackSpeedMult = System.Convert.ToSingle(buff.ReadDouble());//4E EA 78 8A 24 53 FD 3F

                CollisionRadius = System.Convert.ToSingle(buff.ReadDouble());//0 0 0 0 0 0 1A 40
                CollisionHeight = System.Convert.ToSingle(buff.ReadDouble());// 0 0 0 0 0 80 36 40

                //GD OK

                HairSytle = buff.ReadUInt32();//0 0 0 0
                HairColor = buff.ReadUInt32();//1 0 0 0
                Face = buff.ReadUInt32();//1 0 0 0
                AccessLevel = buff.ReadUInt32();//0 0 0 0 //is GM?

                Title = buff.ReadString();//4C 0 65 0 74 0 73 0 20 0 62 0 65 0 20 0 66 0 72 0 69 0 65 0 6E 0 64 0 73 0 21 0 0 0

                //GD OK

                ClanID = buff.ReadUInt32();//D1 2E 0 0
                ClanCrestID = buff.ReadUInt32();//EE C6 0 0
                AllyID = buff.ReadUInt32();//0 0 0 0
                AllyCrestID = buff.ReadUInt32();//0 0 0 0
                isClanLeader = buff.ReadUInt32();//20 2 0 0 //relation

                //GD OK

                MountType = buff.ReadByte();//0
                PrivateStoreType = buff.ReadByte();//0
                hasDwarfCraft = buff.ReadByte();//0

                PKCount = buff.ReadUInt32();//F 0 0 0
                PvPCount = buff.ReadUInt32();//E4 2 0 0

                CubicCount = buff.ReadUInt16();//0 0

                Cubics.Clear();
                for (uint i = 0; i < (uint)CubicCount; i++)
                {
                    uint tmpc = buff.ReadUInt16();//ushort
                    Cubics.Add(tmpc);
                }


                buff.ReadUInt16();
                //AbnormalEffects = buff.ReadUInt32();//Abnormal effects no longer stored here, 00 FE FF FF
                buff.ReadUInt32(); 

                RecLeft = buff.ReadUInt16();//9 0
                RecAmount = buff.ReadUInt16();//13 0

                //GD OK

                buff.ReadUInt32();//0 0 0 0 //getMountNpcId() + 1000000

                InventoryLimit = buff.ReadUInt16();//50 0

                //GD OK

                ActiveClass = buff.ReadUInt32();//classid again //61 0 0 0
                SpecialEffects = buff.ReadUInt32();//special effects? //0 0 0 0

                Max_CP = buff.ReadUInt32();//92 9 0 0
                Cur_CP = buff.ReadUInt32();//92 9 0 0

                //GD OK

                EnchantAmount = buff.ReadByte();//0
                TeamCircle = buff.ReadByte();//0

                ClanCrestIDLarge = buff.ReadUInt32();//0 0 0 0

                HeroIcon = buff.ReadByte();//is noble //1
                HeroGlow = buff.ReadByte();//is hero glowing //0

                isFishing = buff.ReadByte();//0

                FishX = buff.ReadInt32();//0 0 0 0

                FishY = buff.ReadInt32();//0 0 0 0

                FishZ = buff.ReadInt32();//0 0 0 0


                NameColor = buff.ReadUInt32();//0 0 0 0

                isRunning = buff.ReadByte();//0


                PledgeClass = buff.ReadUInt32();//pldege class //40 BB 1A 0
                buff.ReadUInt32();//0 5 0 0 //pledge type

                TitleColor = buff.ReadUInt32();//title color //0 0 0 0

                DemonSword = buff.ReadUInt32();//z sword? //0 0 0 0

                Transform_ID = buff.ReadInt32();//Transformation ID

                //these changed in CT2.3...
                AtkAttrib = buff.ReadInt16();//Attack Element
                AtkAttribVal = buff.ReadInt16();//Attack Element Value
                DefAttribFire = buff.ReadInt16();//Def Attr Fire
                DefAttribWater = buff.ReadInt16();//Def Attr Water
                DefAttribWind = buff.ReadInt16();//Def Attr Wind
                DefAttribEarth = buff.ReadInt16();//Def Attr Earth
                DefAttribHoly = buff.ReadInt16();//Def Attr Holy
                DefAttribUnholy = buff.ReadInt16();//Def Attr Unholy

                Agathon_ID = buff.ReadInt32();//AgathionId


                Fame = buff.ReadInt32(); //Fame
                Allow_Minimap = buff.ReadInt32();// Allow or Prevent opening of mini map (hb cert)
                Vitality_Points = buff.ReadInt32();//Vitality Level
                ExtendedEffects = buff.ReadUInt32(); // EXtended VFX

                buff.ReadUInt32(); //00
                buff.ReadUInt32(); //00
                buff.ReadByte(); //00
                
                //Abnormal effects? 
                //1 herb: 01 00 00 00 2C 00 00 00 00
                //2 herbs: 02 00 00 00 24 00 00 00 27 00 00 00 00
                AbnormalEffects = 0x00;
                uint AbnEffCount = buff.ReadUInt32();
                //Globals.l2net_home.Add_Text("Abn effects count: " + AbnEffCount.ToString(), Globals.Green, TextType.BOT);

                AbnEffects.Clear();

                for (uint i = 0; i < (uint)AbnEffCount; i++)
                {
                    uint tmpabneff = buff.ReadUInt32();
#if DEBUG
                    Globals.l2net_home.Add_Text("Adding abnormal vfx (userinfo): " + tmpabneff.ToString("X2"), Globals.Yellow, TextType.BOT);
#endif
                    AbnEffects.Add(tmpabneff);
                }
                //if (AbnEffCount < 1)
                //{
                //    AbnormalEffects = 0x00;
                //}
                buff.ReadByte(); //00
            }
            else
            {
                X = buff.ReadInt32();//E7 3F 2 0
                Y = buff.ReadInt32();//9D 64 0 0
                Z = buff.ReadInt32();//28 F8 FF FF
                Heading = buff.ReadInt32();//0 0 0 0
                ID = buff.ReadUInt32();//A0 B9 B0 49

                Name = buff.ReadString();//4B 0 61 0 72 0 76 0 6F 0 6B 0 0 0
                Race = buff.ReadUInt32();//0 0 0 0
                Sex = buff.ReadUInt32();//1 0 0 0
                BaseClass = buff.ReadUInt32();//61 0 0 0 //base class

                Level = buff.ReadUInt32();//4C 0 0 0
                XP = buff.ReadUInt64();//FB 62 76 43 0 0 0 0
                if (Globals.gamedata.Chron >= Chronicle.CT2_6)
                {
                    XPPercent = System.Convert.ToSingle(buff.ReadDouble());//buff.ReadUInt64(); //00 00 00 00 00 00 00 00
                }
                STR = buff.ReadUInt32();//12 0 0 0
                DEX = buff.ReadUInt32();//15 0 0 0
                CON = buff.ReadUInt32();//1F 0 0 0
                INT = buff.ReadUInt32();//27 0 0 0
                WIT = buff.ReadUInt32();//1A 0 0 0
                MEN = buff.ReadUInt32();//23 0 0 0

                Max_HP = buff.ReadUInt32();//9B 11 0 0
                Cur_HP = buff.ReadUInt32();//9B 11 0 0
                Max_MP = buff.ReadUInt32();//F F 0 0
                Cur_MP = buff.ReadUInt32();//F F 0 0
                SP = buff.ReadUInt32();//CA DF 59 0
                Cur_Load = buff.ReadUInt32();//CE 41 0 0
                Max_Load = buff.ReadUInt32();//7C 28 1 0
                Weapon_Equipped = buff.ReadUInt32(); // 20 no weapon, 40 weapon equip

                obj_Under = buff.ReadUInt32();//0 0 0 0
                obj_REar = buff.ReadUInt32();//F 46 8B 40
                obj_LEar = buff.ReadUInt32();//91 E5 78 40
                obj_Neck = buff.ReadUInt32();//58 E8 50 40
                obj_RFinger = buff.ReadUInt32();//64 A1 87 40
                obj_LFinger = buff.ReadUInt32();//D9 64 83 40
                obj_Head = buff.ReadUInt32();//41 13 89 40
                obj_RHand = buff.ReadUInt32();//C2 E4 88 40
                obj_LHand = buff.ReadUInt32();//97 E6 89 40
                obj_Gloves = buff.ReadUInt32();//37 39 8B 40
                obj_Chest = buff.ReadUInt32();//DE 16 8B 40
                obj_Legs = buff.ReadUInt32();//93 1A 87 40
                obj_Feet = buff.ReadUInt32();//9C FA 84 40
                obj_Back = buff.ReadUInt32();//0 0 0 0
                obj_LRHand = buff.ReadUInt32();//0 0 0 0
                obj_Hair = buff.ReadUInt32();//E1 43 8B 40
                obj_Face = buff.ReadUInt32();

                buff.ReadUInt32();//C7 //right bracelet
                buff.ReadUInt32();//C7 //left bracelet
                buff.ReadUInt32();//C7 //deco 1
                buff.ReadUInt32();//C7 //deco 2
                buff.ReadUInt32();//C7 //deco 3
                buff.ReadUInt32();//C7 //deco 4
                buff.ReadUInt32();//C7 //deco 5
                buff.ReadUInt32();//C7 //deco 6

                if (Globals.gamedata.Chron >= Chronicle.CT2_3)
                {
                    buff.ReadUInt32();//CT2.3 - belt obj id
                }

                itm_Under = buff.ReadUInt32();//0 0 0 0
                itm_REar = buff.ReadUInt32();//5 1A 0 0
                itm_LEar = buff.ReadUInt32();//3 1A 0 0
                itm_Neck = buff.ReadUInt32();//98 3 0 0
                itm_RFinger = buff.ReadUInt32();//6 1A 0 0
                itm_LFinger = buff.ReadUInt32();//79 3 0 0
                itm_Head = buff.ReadUInt32();//23 2 0 0
                itm_RHand = buff.ReadUInt32();//B9 19 0 0
                itm_LHand = buff.ReadUInt32();//E9 18 0 0
                itm_Gloves = buff.ReadUInt32();//8A 16 0 0
                itm_Chest = buff.ReadUInt32();//60 9 0 0
                itm_Legs = buff.ReadUInt32();//65 9 0 0
                itm_Feet = buff.ReadUInt32();//96 16 0 0
                itm_Back = buff.ReadUInt32();//0 0 0 0
                itm_LRHand = buff.ReadUInt32();//0 0 0 0
                itm_Hair = buff.ReadUInt32();//E 1E 0 0
                itm_Face = buff.ReadUInt32();

                itm_rbracelet = buff.ReadUInt32();//C7 //right bracelet
                itm_lbracelet = buff.ReadUInt32();//C7 //left bracelet
                itm_talisman1 = buff.ReadUInt32();//C7 //deco 1
                itm_talisman2 = buff.ReadUInt32();//C7 //deco 2
                itm_talisman3 = buff.ReadUInt32();//C7 //deco 3
                itm_talisman4 = buff.ReadUInt32();//C7 //deco 4
                itm_talisman5 = buff.ReadUInt32();//C7 //deco 5
                itm_talisman6 = buff.ReadUInt32();//C7 //deco 6

                if (Globals.gamedata.Chron >= Chronicle.CT2_3)
                {
                    buff.ReadUInt32();//CT2.3 belt itm id 
                }

                aug_Under = buff.ReadUInt32();
                aug_REar = buff.ReadUInt32();
                aug_LEar = buff.ReadUInt32();
                aug_Neck = buff.ReadUInt32();
                aug_RFinger = buff.ReadUInt32();
                aug_LFinger = buff.ReadUInt32();
                aug_Head = buff.ReadUInt32();
                aug_RHand = buff.ReadUInt32();
                aug_LHand = buff.ReadUInt32();
                aug_Gloves = buff.ReadUInt32();
                aug_Chest = buff.ReadUInt32();
                aug_Legs = buff.ReadUInt32();
                aug_Feet = buff.ReadUInt32();
                aug_Back = buff.ReadUInt32();
                aug_LRHand = buff.ReadUInt32();
                aug_Hair = buff.ReadUInt32();
                aug_Face = buff.ReadUInt32();

                aug_rbracelet = buff.ReadUInt32();
                aug_lbracelet = buff.ReadUInt32();
                aug_talisman1 = buff.ReadUInt32();
                aug_talisman2 = buff.ReadUInt32();
                aug_talisman3 = buff.ReadUInt32();
                aug_talisman4 = buff.ReadUInt32();
                aug_talisman5 = buff.ReadUInt32();
                aug_talisman6 = buff.ReadUInt32();

                if (Globals.gamedata.Chron >= Chronicle.CT2_3)
                {
                    buff.ReadUInt32();//CT2.3 - belt aug id 

                    buff.ReadUInt32();//CT2.3 - max talisman slots
                    buff.ReadUInt32();//CT2.3 - cloak status
                }

                if (Globals.gamedata.Chron >= Chronicle.CT3_0)
                {
                    buff.ReadUInt32();
                    buff.ReadUInt32();
                    buff.ReadUInt32();

                    buff.ReadUInt32(); //00 00 00 00 
                    buff.ReadUInt32(); //00 00 00 00 
                    buff.ReadUInt32(); //00 00 00 00 
                    buff.ReadUInt32(); //00 00 00 00 
                    buff.ReadUInt32(); //00 00 00 00 
                    buff.ReadUInt32(); //00 00 00 00 
                }

                //if (Globals.gamedata.Chron >= Chronicle.CT3_1)
                //{
                //    buff.ReadUInt32(); //00 00 00 00
                //    buff.ReadUInt32(); //00 00 00 00
                //    buff.ReadUInt32(); //00 00 00 00
                //    buff.ReadUInt32(); //00 00 00 00
                //    buff.ReadUInt32(); //00 00 00 00
                //    buff.ReadUInt32(); //00 00 00 00
                //    buff.ReadUInt32(); //00 00 00 00
                //    buff.ReadUInt32(); //00 00 00 00
                //}

                Patk = buff.ReadUInt32();//F8 1 0 0
                PatkSpeed = buff.ReadUInt32();//FD 1 0 0
                PDef = buff.ReadUInt32();//C3 2 0 0
                Evasion = buff.ReadUInt32();//5F 0 0 0
                Accuracy = buff.ReadUInt32();//71 0 0 0
                Focus = buff.ReadUInt32();//28 0 0 0
                Matk = buff.ReadUInt32();//99 2 0 0

                MatkSpeed = buff.ReadUInt32();//DD 2 0 0
                PatkSpeed = buff.ReadUInt32();//twice...who knows why//FD 1 0 0

                MDef = buff.ReadUInt32();//22 5 0 0

                if (Globals.gamedata.Chron >= Chronicle.CT3_0)
                {
                    MAccuracy = buff.ReadUInt32(); //m.accuracy
                    MEvasion = buff.ReadUInt32(); //m.evasion
                    MCritical = buff.ReadUInt32(); //m.critical

                }

                PvPFlag = buff.ReadUInt32();//0 0 0 0
                Karma = buff.ReadInt32();//0 0 0 0

                RunSpeed = buff.ReadUInt32();//78 0 0 0
                WalkSpeed = buff.ReadUInt32();//4E 0 0 0
                SwimRunSpeed = buff.ReadUInt32();//32 0 0 0
                SwimWalkSpeed = buff.ReadUInt32();//32 0 0 0
                flRunSpeed = buff.ReadUInt32();//0 0 0 0
                flWalkSpeed = buff.ReadUInt32();//0 0 0 0
                FlyRunSpeed = buff.ReadUInt32();//0 0 0 0
                FlyWalkSpeed = buff.ReadUInt32();//0 0 0 0

                MoveSpeedMult = System.Convert.ToSingle(buff.ReadDouble());//8F C2 F5 28 5C 8F F4 3F
                AttackSpeedMult = System.Convert.ToSingle(buff.ReadDouble());//4E EA 78 8A 24 53 FD 3F

                CollisionRadius = System.Convert.ToSingle(buff.ReadDouble());//0 0 0 0 0 0 1A 40
                CollisionHeight = System.Convert.ToSingle(buff.ReadDouble());// 0 0 0 0 0 80 36 40

                HairSytle = buff.ReadUInt32();//0 0 0 0
                HairColor = buff.ReadUInt32();//1 0 0 0
                Face = buff.ReadUInt32();//1 0 0 0
                AccessLevel = buff.ReadUInt32();//0 0 0 0 //is GM?

                Title = buff.ReadString();//4C 0 65 0 74 0 73 0 20 0 62 0 65 0 20 0 66 0 72 0 69 0 65 0 6E 0 64 0 73 0 21 0 0 0

                ClanID = buff.ReadUInt32();//D1 2E 0 0
                ClanCrestID = buff.ReadUInt32();//EE C6 0 0
                AllyID = buff.ReadUInt32();//0 0 0 0
                AllyCrestID = buff.ReadUInt32();//0 0 0 0
                isClanLeader = buff.ReadUInt32();//20 2 0 0 //relation

                MountType = buff.ReadByte();//0
                PrivateStoreType = buff.ReadByte();//0
                hasDwarfCraft = buff.ReadByte();//0
                PKCount = buff.ReadUInt32();//F 0 0 0
                PvPCount = buff.ReadUInt32();//E4 2 0 0

                CubicCount = buff.ReadUInt16();//0 0
                Cubics.Clear();
                for (uint i = 0; i < (uint)CubicCount; i++)
                {
                    uint tmpc = buff.ReadUInt16();//ushort
                    Cubics.Add(tmpc);
                }


                FindParty = buff.ReadByte();//0


                AbnormalEffects = buff.ReadUInt32();//0 0 0 0
                buff.ReadByte();// isFlyingMounted  

                ClanPrivileges = buff.ReadUInt32();//8E AC C 0

                RecLeft = buff.ReadUInt16();//9 0
                RecAmount = buff.ReadUInt16();//13 0
                buff.ReadUInt32();//0 0 0 0 //getMountNpcId() + 1000000

                InventoryLimit = buff.ReadUInt16();//50 0

                ActiveClass = buff.ReadUInt32();//classid again //61 0 0 0
                SpecialEffects = buff.ReadUInt32();//special effects? //0 0 0 0

                Max_CP = buff.ReadUInt32();//92 9 0 0
                Cur_CP = buff.ReadUInt32();//92 9 0 0


                EnchantAmount = buff.ReadByte();//0
                TeamCircle = buff.ReadByte();//0

                ClanCrestIDLarge = buff.ReadUInt32();//0 0 0 0

                HeroIcon = buff.ReadByte();//is noble //1
                HeroGlow = buff.ReadByte();//is hero glowing //0

                try
                {
                    isFishing = buff.ReadByte();//0

                    FishX = buff.ReadInt32();//0 0 0 0

                    FishY = buff.ReadInt32();//0 0 0 0

                    FishZ = buff.ReadInt32();//0 0 0 0


                    NameColor = buff.ReadUInt32();//0 0 0 0

                    isRunning = buff.ReadByte();//0


                    PledgeClass = buff.ReadUInt32();//pldege class //40 BB 1A 0
                    buff.ReadUInt32();//0 5 0 0 //pledge type

                    TitleColor = buff.ReadUInt32();//title color //0 0 0 0

                    DemonSword = buff.ReadUInt32();//z sword? //0 0 0 0

                    Transform_ID = buff.ReadInt32();//Transformation ID

                    //these changed in CT2.3...
                    AtkAttrib = buff.ReadInt16();//Attack Element
                    AtkAttribVal = buff.ReadInt16();//Attack Element Value
                    DefAttribFire = buff.ReadInt16();//Def Attr Fire
                    DefAttribWater = buff.ReadInt16();//Def Attr Water
                    DefAttribWind = buff.ReadInt16();//Def Attr Wind
                    DefAttribEarth = buff.ReadInt16();//Def Attr Earth
                    DefAttribHoly = buff.ReadInt16();//Def Attr Holy
                    DefAttribUnholy = buff.ReadInt16();//Def Attr Unholy

                    Agathon_ID = buff.ReadInt32();//AgathionId

                    if (Globals.gamedata.Chron >= Chronicle.CT2_1)
                    {
                        //C9 - CT2.5
                        Fame = buff.ReadInt32(); //Fame
                        Allow_Minimap = buff.ReadInt32();// Allow or Prevent opening of mini map (hb cert)
                        Vitality_Points = buff.ReadInt32();//Vitality Level
                        ExtendedEffects = buff.ReadUInt32(); // EXtended VFX
                    }

                    if (Globals.gamedata.Chron >= Chronicle.CT3_0)
                    {
                        buff.ReadUInt32(); //00
                        buff.ReadUInt32(); //00
                        buff.ReadByte(); //00
                    }
                }
                catch
                {
                }
            }
        }

		public void Load(ByteBuffer buff)
		{
			Name = buff.ReadString();//Util.Get_String(buff,ref offset);

			ID = buff.ReadUInt32();//System.BitConverter.ToUInt32(buff,offset);
            charID = ID; //ID gets converted to objID later on.

			Title = buff.ReadString();//Util.Get_String(buff,ref offset);

			Globals.gamedata.sessionID[0] = buff.ReadByte();
            Globals.gamedata.sessionID[1] = buff.ReadByte();
            Globals.gamedata.sessionID[2] = buff.ReadByte();
            Globals.gamedata.sessionID[3] = buff.ReadByte();

			ClanID = buff.ReadUInt32();

			buff.ReadUInt32();//00

			Sex = buff.ReadUInt32();
			Race = buff.ReadUInt32();
			BaseClass = buff.ReadUInt32();
			Active = buff.ReadUInt32();

			X = buff.ReadInt32();
			Y = buff.ReadInt32();
			Z = buff.ReadInt32();
            Dest_X = X;
            Dest_Y = Y;
            Dest_Z = Z;

            Cur_HP = System.Convert.ToSingle(buff.ReadDouble());
            Cur_MP = System.Convert.ToSingle(buff.ReadDouble());

            SP = buff.ReadUInt32();
            XP = buff.ReadUInt64();//c5

            Level = buff.ReadUInt32();
            Karma = buff.ReadInt32();

            buff.ReadUInt32();// PK Kills

            //linvior ok
            STR = buff.ReadUInt32();
            DEX = buff.ReadUInt32();
            CON = buff.ReadUInt32();
            INT = buff.ReadUInt32();
            WIT = buff.ReadUInt32();
            MEN = buff.ReadUInt32();


            Globals.gamedata.LoginTime = System.DateTime.Now.Ticks;
            Globals.gamedata.GameTime = buff.ReadUInt32();//reset on 24 hour timers
            buff.ReadUInt32();//00

            Class = buff.ReadUInt32();//class id


            Globals.gamedata.gameguardInit[0] = buff.ReadByte();
            Globals.gamedata.gameguardInit[1] = buff.ReadByte();
            Globals.gamedata.gameguardInit[2] = buff.ReadByte();
            Globals.gamedata.gameguardInit[3] = buff.ReadByte();
            Globals.gamedata.gameguardInit[4] = buff.ReadByte();
            Globals.gamedata.gameguardInit[5] = buff.ReadByte();
            Globals.gamedata.gameguardInit[6] = buff.ReadByte();
            Globals.gamedata.gameguardInit[7] = buff.ReadByte();
            Globals.gamedata.gameguardInit[8] = buff.ReadByte();
            Globals.gamedata.gameguardInit[9] = buff.ReadByte();
            Globals.gamedata.gameguardInit[10] = buff.ReadByte();
            Globals.gamedata.gameguardInit[11] = buff.ReadByte();
            Globals.gamedata.gameguardInit[12] = buff.ReadByte();
            Globals.gamedata.gameguardInit[13] = buff.ReadByte();
            Globals.gamedata.gameguardInit[14] = buff.ReadByte();
            Globals.gamedata.gameguardInit[15] = buff.ReadByte();

            //64 bytes of God knows what...
            buff.ReadUInt64();
            buff.ReadUInt64();
            buff.ReadUInt64();
            buff.ReadUInt64();
            buff.ReadUInt64();
            buff.ReadUInt64();
            buff.ReadUInt64();
            buff.ReadUInt64();

            //key
            if (Globals.gamedata.Chron >= Chronicle.CT1_5)
            {
                //last 4 bytes has our key thingy
                //buff.SetIndex(buff.Length() - 4);
                int mix = buff.ReadInt32();

                Globals.Mixer = new MixedPackets(mix);
            }
        }

        public void Update(ByteBuffer buff)
        {

            uint data = buff.ReadByte();
				
			switch(data)
			{
				case 0x01://level
                    Level = buff.ReadUInt32();
					break;
				case 0x02://exp
                    XP = buff.ReadUInt64();
					break;
				case 0x03://str
                    STR = buff.ReadUInt32();
					break;
				case 0x04://dex
                    DEX = buff.ReadUInt32();
					break;
				case 0x05://con
                    CON = buff.ReadUInt32();
					break;
				case 0x06://int
                    INT = buff.ReadUInt32();
					break;
				case 0x07://wit
                    WIT = buff.ReadUInt32();
					break;
				case 0x08://men
                    MEN = buff.ReadUInt32();
					break;
				case 0x09://cur hp
                    Cur_HP = buff.ReadUInt32();
					break;
				case 0x0A://max hp
                    Max_HP = buff.ReadUInt32();
					break;
				case 0x0B://cur mp
                    Cur_MP = buff.ReadUInt32();
					break;
				case 0x0C://max mp
                    Max_MP = buff.ReadUInt32();
					break;
				case 0x0D://sp
                    SP = buff.ReadUInt32();
					break;
				case 0x0E://cur load
                    Cur_Load = buff.ReadUInt32();
					break;
				case 0x0F://max load
                    Max_Load = buff.ReadUInt32();
					break;
				case 0x10://..
                    buff.ReadUInt32();
					break;
				case 0x11://patk
                    Patk = buff.ReadUInt32();
					break;
				case 0x12://atk spd
                    PatkSpeed = buff.ReadUInt32();
					break;
				case 0x13://pdef
                    PDef = buff.ReadUInt32();
					break;
				case 0x14://evasion
                    Evasion = buff.ReadUInt32();
					break;
				case 0x15://acc
                    Accuracy = buff.ReadUInt32();
					break;
				case 0x16://crit
                    Focus = buff.ReadUInt32();
					break;
				case 0x17://m atk
                    Matk = buff.ReadUInt32();
					break;
				case 0x18://cast spd
                    MatkSpeed = buff.ReadUInt32();
					break;
				case 0x19://mdef
                    MDef = buff.ReadUInt32();
					break;
				case 0x1A://pvp flag
                    PvPFlag = buff.ReadUInt32();
					break;
				case 0x1B://karma
                    Karma = buff.ReadInt32();
					break;
				case 0x1C://..
                    buff.ReadUInt32();
					break;
				case 0x1D://..
                    buff.ReadUInt32();
					break;
				case 0x1E://..
                    buff.ReadUInt32();
					break;
				case 0x1F://..
                    buff.ReadUInt32();
					break;
				case 0x20://..
                    buff.ReadUInt32();
					break;
				case 0x21://cur cp
                    Cur_CP = buff.ReadUInt32();
					break;
				case 0x22://max cp
                    Max_CP = buff.ReadUInt32();
					break;
                default:
                    buff.ReadUInt32();
                    break;
			}
		}

		public void Clear_Botting_Buffing(bool success)
		{
            string name = Util.GetCharName(Globals.gamedata.my_char.BuffTarget).ToUpperInvariant();

            Globals.BuffsGivenLock.EnterWriteLock();
			try
			{
                if (Globals.gamedata.my_char.BuffTarget != 0)
				{
                    for (int i = 0; i < Globals.gamedata.BuffsGiven.Count; i++)
					{
                        if (Util.GetCharID(((CharBuffTimer)Globals.gamedata.BuffsGiven[i]).Name) == Globals.gamedata.my_char.BuffTarget)
						{
							if(success)
                                ((CharBuffTimer)Globals.gamedata.BuffsGiven[i]).Set_Time(Globals.gamedata.my_char.BuffSkillID, System.DateTime.Now.Ticks);
							else
                                ((CharBuffTimer)Globals.gamedata.BuffsGiven[i]).Add_Time(Globals.gamedata.my_char.BuffSkillID, Globals.FAILED_BUFF);
							break;
						}
					}
				}
			}
            catch
			{
				Globals.l2net_home.Add_Error("crash: Clear_Botting_Buffing");
			}
			finally
			{
                Globals.BuffsGivenLock.ExitWriteLock();

                Globals.gamedata.my_char.BuffTargetLast = Globals.gamedata.my_char.BuffTarget;
                Globals.gamedata.my_char.BuffTarget = 0;
                Globals.gamedata.BOT_STATE = BotState.FinishedBuffing;
                Globals.gamedata.Set_Char_To_Normal();

                /*if (Globals.gamedata.my_char.BuffNeedTarget == 0)
                {
                    //we don't need a target for this buff...
                    Globals.gamedata.BOT_STATE = BotState.Nothing;
                }*/
			}
		}
	}//end of class
}
