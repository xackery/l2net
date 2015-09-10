using System;

namespace L2_login
{
	public class CharInfo : Object_Base
	{
        public volatile float X = 0;
        public volatile float Y = 0;
        public volatile float Z = 0;
        public volatile int Heading = 0;
		private string _Name = "";
        public volatile uint Race = 0;
        public volatile uint Sex = 0;
        public volatile uint Class = 0;
        public volatile uint BaseClass = 0;
        public volatile uint Level = 0;

        public volatile uint Underwear = 0;
        public volatile uint Head = 0;
        public volatile uint RHand = 0;
        public volatile uint LHand = 0;
        public volatile uint Gloves = 0;
        public volatile uint Chest = 0;
        public volatile uint Legs = 0;
        public volatile uint Feet = 0;
        public volatile uint Back = 0;
        public volatile uint LRHand = 0;
        public volatile uint Hair = 0;
        public volatile uint DollFace = 0;
        public volatile uint aug_RHand = 0;
        public volatile uint aug_LHand = 0;

        public volatile uint PvPFlag = 0;
        public volatile int Karma = 0;

        public volatile float MatkSpeed = 0;
        public volatile float PatkSpeed = 0;

        public volatile uint PvPFlag2 = 0;//?
        public volatile int Karma2 = 0;//?

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

		private string _Title = "";
        private uint _ClanID = 0;
        private uint _ClanCrestID = 0;
        public volatile uint AllyID = 0;
        public volatile uint AllyCrestID = 0;
        public volatile uint SiegeFlags = 0;//dunno

        public volatile uint isSitting = 1;// standing = 1  sitting = 0//byte
        public volatile uint isRunning = 1;// running = 1   walking = 0//byte
        public volatile uint isInCombat = 0;//byte
        public volatile uint isAlikeDead = 0;//byte

        public volatile uint Invisible = 0;// invisible = 1  visible = 0//byte
        public volatile uint MountType = 0;// 1 on strider   2 on wyvern   0 no mount//byte
        public volatile uint PrivateStoreType = 0;//  1 - sellshop//byte

        public volatile uint CubicCount = 0;//ushort
		private System.Collections.ArrayList _Cubics = new System.Collections.ArrayList();
        private System.Collections.ArrayList _AbnEffects = new System.Collections.ArrayList();

        public volatile uint FindParty = 0;//byte

        public volatile uint AbnormalEffects = 0;
        public volatile uint ExtendedEffects = 0;
        public volatile uint RelationStates = 0;
        public volatile uint isFlying = 0;//byte
        public volatile uint RecAmount = 0;//0 = white | 255 = blue//ushort

        public volatile uint EnchantAmount = 0;//byte
        public volatile uint TeamCircle = 0;//byte

        public volatile uint ClanCrestIDLarge = 0;

        public volatile uint HeroIcon = 0;//byte
        public volatile uint HeroGlow = 0;//byte

        public volatile uint Transform_ID = 0;
        public volatile uint Agathon_ID = 0;

        public volatile uint isFishing = 0;//0x01 - fishing//byte
        public volatile int FishX = 0;
        public volatile int FishY = 0;
        public volatile int FishZ = 0;

        public volatile uint NameColor = 0;
        public volatile uint TitleColor = 0;
        public volatile uint PledgeClass = 0;
        public volatile uint DemonSword = 0;

        public volatile uint Relation = 0;
        public volatile int WarState = 0;

        public volatile float Dest_X = 0;
        public volatile float Dest_Y = 0;
        public volatile float Dest_Z = 0;
        public volatile bool Moving = false;
		private System.DateTime _lastMoveTime = System.DateTime.Now;
        public volatile uint MoveTarget = 0;
        public volatile TargetType MoveTargetType = TargetType.NONE;

        public volatile uint TargetID = 0;
        public volatile TargetType CurrentTargetType = TargetType.NONE;

        public volatile float Cur_HP = 0;
        public volatile float Max_HP = 0;
        public volatile float Cur_MP = 0;
        public volatile float Max_MP = 0;
        public volatile float Cur_CP = 0;
        public volatile float Max_CP = 0;

        private string _ClanName = "";
        public volatile int ClanCrestIndex = 0;
        private string _AllyName = "";
		private System.Collections.SortedList _my_buffs;

		private readonly object my_buffsLock = new object();
        private readonly object ClanNameLock = new object();
        private readonly object AllyNameLock = new object();
        private readonly object ClanIDLock = new object();
        private readonly object ClanCrestIDLock = new object();
        private readonly object ClanCrestIndexLock = new object();
        private readonly object lastMoveTimeLock = new object();
		private readonly object CubicsLock = new object();
        private readonly object AbnEffectsLock = new object();
		private readonly object TitleLock = new object();
		private readonly object NameLock = new object();

        public bool HasEffect(AbnormalEffects test)
        {
            return AbnEffects.IndexOf((uint)test) != -1;
            //return (AbnormalEffects & (uint)test) != 0;
        }
        
        public bool HasExtendedEffect(ExtendedEffects test)
        {
            return (ExtendedEffects & (uint)test) != 0;
        }
        
		public bool HasRelation(RelationStates test)
		{
			return(Relation & (uint)test) != 0;
		}
		
        public bool CheckCombat()
		{
			if(isInCombat == 0)
				return false;
			return true;
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
		public uint ClanID
		{
			get
			{
				uint tmp;
				lock(ClanIDLock)
				{
					tmp = this._ClanID;
				}
				return tmp;
			}
			set
			{
				lock(ClanIDLock)
				{
					_ClanID = value;
				}

                if (_ClanID == 0)
                {
                    //we should probably clear out the clan and ally name here...
                    ClanName = "";
                    AllyName = "";
                    ClanCrestID = 0;
                }
                else
                {
                    //do clan shit
                    Clan_Info ci = null;

                    try
                    {
                        Globals.ClanListLock.EnterReadLock();
                        try
                        {
                            if (Globals.clanlist.ContainsKey(_ClanID))
                            {
                                ci = (Clan_Info)Globals.clanlist[_ClanID];
                            }
                        }
                        finally
                        {
                            Globals.ClanListLock.ExitReadLock();
                        }
                    }
                    catch
                    {
                        //oh well
                    }

                    if (ci != null)
                    {
                        ClanName = ci.ClanName;
                        AllyName = ci.AllyName;
                    }
                    else if (Globals.gamedata.OOG)
                    {
                        //we need to request the clan/ally/warlist
                        ServerPackets.RequestClanInfo(_ClanID);
                        //request warstate
                    }
                }
			}
		}
		public uint ClanCrestID
		{
			get
			{
				uint tmp;
				lock(ClanCrestIDLock)
				{
					tmp = this._ClanCrestID;
				}
				return tmp;
			}
			set
			{
				lock(ClanCrestIDLock)
				{
					_ClanCrestID = value;

                    if (_ClanCrestID == 0)
                    {
                        ClanCrestIndex = -1;
                    }
                    else
                    {
                        //and the crest shit...
                        for (int i = 0; i < Globals.crestids.Count; i++)
                        {
                            if ((uint)(Globals.crestids[i]) == _ClanCrestID)
                            {
                                ClanCrestIndex = i;
                                return;
                            }
                        }

                        //if we get here... we didn't find the crest
                        //lets make sure we havent requested it already
                        //and then request it if we need it
                        if (System.IO.File.Exists(Globals.PATH + "\\Crests\\" + _ClanCrestID.ToString() + ".bmp"))
                        {
                            //try to load the crest and add to the array
                            System.Drawing.Bitmap img = new System.Drawing.Bitmap(Globals.PATH + "\\Crests\\" + _ClanCrestID.ToString() + ".bmp");
                            Globals.l2net_home.imageList_crests.Images.Add(img);
                            Globals.crestids.Add(_ClanCrestID);
                            ClanCrestIndex = Globals.l2net_home.imageList_crests.Images.Count - 1;
                        }
                        else
                        {
                            if (Globals.DownloadNewCrests)
                            {
                                //otherwise, lets request that we download the crest
                                //dont request the same crest twice in the same run
                                if (Globals.requested_clancrests.Contains(_ClanCrestID))
                                {
                                    //already requested this clan crest... ignore
                                }
                                else
                                {
                                    Globals.requested_clancrests.Add(_ClanCrestID);
                                    ServerPackets.RequestCrest(_ClanCrestID);
                                }
                            }
                            else
                            {
                                //the blank crest image thingy
                                ClanCrestIndex = 0;
                            }
                        }
                        //end of clan crest shit
                    }
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
        public string ClanName
        {
            get
            {
                if (_ClanName == "" && _ClanID != 0)
                {
                    //do clan shit
                    Clan_Info ci = null;

                    try
                    {
                        Globals.ClanListLock.EnterReadLock();
                        try
                        {
                            if (Globals.clanlist.ContainsKey(_ClanID))
                            {
                                ci = (Clan_Info)Globals.clanlist[_ClanID];
                            }
                        }
                        finally
                        {
                            Globals.ClanListLock.ExitReadLock();
                        }
                    }
                    catch
                    {
                        //oh well
                    }

                    if (ci != null)
                    {
                        ClanName = ci.ClanName;
                        AllyName = ci.AllyName;
                    }
                    else if (Globals.gamedata.OOG)
                    {
                        //we need to request the clan/ally/warlist
                        ServerPackets.RequestClanInfo(_ClanID);
                        //request warstate
                    }
                }

                string tmp;
                lock (ClanNameLock)
                {
                    tmp = this._ClanName;
                }
                return tmp;
            }
            set
            {
                lock (ClanNameLock)
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
                lock (AllyNameLock)
                {
                    tmp = this._AllyName;
                }
                return tmp;
            }
            set
            {
                lock (AllyNameLock)
                {
                    _AllyName = value;
                }
            }
        }
		public System.Collections.SortedList my_buffs
		{
			get
			{
                System.Collections.SortedList tmp;
				lock(my_buffsLock)
				{
					tmp = this._my_buffs;
				}
				return tmp;
			}
			set
			{
				lock(my_buffsLock)
				{
					_my_buffs = value;
				}
			}
		}

		public CharInfo()
		{
			Moving = false;
            my_buffs = new System.Collections.SortedList();
		}
               
	public void Load(ByteBuffer buff)
	{
		//int offset = 1;

        /*
         * 31 
            6E C8 FE FF 
            9B 50 02 00 
            20 F4 FF FF 

            00 00 00 00 

            68 5F 11 4A id
            4C 00 61 00 64 00 79 00 64 00 77 00 61 00 72 00 66 00 00 00 
            04 race
            00 sex
            01 ?
            35 00 00 00 base class ??

            F0 33 00 00 
            61 84 00 00 
            5F 84 00 00 
            00 00 00 00 
            B8 45 00 00 
            62 84 00 00 
            63 84 00 00 
            65 84 00 00 
            00 00 00 00 
            5F 84 00 00 
            00 00 00 00 
            00 00 00 00 -1
 
            EF 33 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 -2

            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 -3

            00 00 00 00 
            00 00 00 00 -4

            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 -5

            00 00 00 00 

            02 01 00 00 // karma  ?

            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 
            DE 00 00 00 // 222 ? cast speed ?
            79 02 00 00 // 633 attac k speed
            00 00 00 00 
            83 00 
            50 00 
            32 00 
            32 00 
            00 00 // title ?
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            F0 3F 93 C1 51 F2 EA 3C 
            02 40 00 00 00 00 00 00 
            14 40 00 00 
            00 00 00 00 
            33 40 00 00 
            00 00 
            01 00 00 00 
            02 00 00 00 
            49 00 6D 00 61 00 53 00 68 00 6F 00 72 00 74 00 73 00 74 00 75 00 66 00 66 00 00 00 //title
            1F 03 10 60 clanid
            6B 9B 00 00 clansrect
            00 00 00 00 allyid
            00 00 00 00 alycrest
            01 is sitting
            01 is runing
            00 in combat
            00 isalikedead
            00 invi
            00 mount
            00 store
            00 00 cub
            00 find pt
            00 
            23 00 rec ?
            00 00 00 00 
            9C 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 00 00 00 
            00 FF FF FF //color
            00 FF 6F 00 //head ?
            00 
            04 00 
            00 A2 F9 EC //tit color
            00 00 
            D0 07 00 00 // 2k - clan rep?
            00 00 00 00 trans
            00 00 00 00 aghat
            01 00 00 00 
            00 00 00 00 
            00 00 
            79 28 00 00 
            7C 3C 00 00 
            7C 3C 00 00 
            96 0A 00 00 
            96 0A 00 00 
            00 00 00 00 
            00 00 
            01 00 00 00 // effect
            28 00 
            00 
         */
		X = buff.ReadInt32();
		Y = buff.ReadInt32();
		Z = buff.ReadInt32();
		Dest_X = X;
		Dest_Y = Y;
		Dest_Z = Z;

		buff.ReadInt32(); // in G+ and onward this is now the Vechile ID

		ID = buff.ReadUInt32();
		Name = buff.ReadString();

		//Race = buff.ReadUInt32();
		//Sex = buff.ReadUInt32();
        Race = buff.ReadByte();
        buff.ReadByte();
        Sex = buff.ReadUInt32();

		BaseClass = buff.ReadUInt32();

		Underwear = buff.ReadUInt32();
		Head = buff.ReadUInt32();
		RHand = buff.ReadUInt32();
		LHand = buff.ReadUInt32();
		Gloves = buff.ReadUInt32();
		Chest = buff.ReadUInt32();
		Legs = buff.ReadUInt32();
		Feet = buff.ReadUInt32();
		Back = buff.ReadUInt32();
		LRHand = buff.ReadUInt32();
		Hair = buff.ReadUInt32();
		DollFace = buff.ReadUInt32();

        //GD OK

		buff.ReadUInt32();//right bracelet
		buff.ReadUInt32();//left bracelet
		buff.ReadUInt32();//deco 1
		buff.ReadUInt32();//deco 2
		buff.ReadUInt32();//deco 3
		buff.ReadUInt32();//deco 4
		buff.ReadUInt32();//deco 5
		buff.ReadUInt32();//deco 6

		buff.ReadUInt32();//belt
	
        //GD OK

		buff.ReadUInt32();//aug underwear
		buff.ReadUInt32();//aug head
		aug_RHand = buff.ReadUInt32();
		aug_LHand = buff.ReadUInt32();

		buff.ReadUInt32();//aug gloves
		buff.ReadUInt32();//aug chest
		buff.ReadUInt32();//aug legs
		buff.ReadUInt32();//aug feet
		buff.ReadUInt32();//aug back
		buff.ReadUInt32();//aug lr hand
		buff.ReadUInt32();//aug hair
		buff.ReadUInt32();//aug doll face
	
        //GD OK

		buff.ReadUInt32();//aug right bracelet
		buff.ReadUInt32();//aug left bracelet
		buff.ReadUInt32();//aug deco 1
		buff.ReadUInt32();//aug deco 2
		buff.ReadUInt32();//aug deco 3
		buff.ReadUInt32();//aug deco 4
		buff.ReadUInt32();//aug deco 5
		buff.ReadUInt32();//aug deco 6

		buff.ReadUInt32();//aug belt


        buff.ReadUInt32();//05 00 00 00 
        buff.ReadUInt32();//01 00 00 00 

		buff.ReadUInt32(); // old pvp flag
		buff.ReadInt32();  // old karma

        //GD OK


        buff.ReadUInt32();
        buff.ReadUInt32();
        buff.ReadUInt32();
        buff.ReadUInt32(); //00 00 00 00
        buff.ReadUInt32(); //00 00 00 00
        buff.ReadUInt32(); //00 00 00 00
        //buff.ReadUInt32();
        buff.ReadByte(); //00 00 00 00
        buff.ReadInt16();
        MatkSpeed = buff.ReadUInt32();
        PatkSpeed = buff.ReadUInt32();
        buff.ReadUInt32();//00
        RunSpeed = buff.ReadUInt16();
        WalkSpeed = buff.ReadUInt16();
        SwimRunSpeed = buff.ReadUInt16();
        SwimWalkSpeed = buff.ReadUInt16();
        //PvPFlag = buff.ReadUInt32();
        //Karma = buff.ReadInt32(); 

        //buff.ReadUInt32(); //00 00 00 00
        //buff.ReadUInt32(); //64 00 00 00 

        //GD OK

		//MatkSpeed = buff.ReadUInt32();
		//PatkSpeed = buff.ReadUInt32();

        buff.ReadUInt32(); //00 00 00 00 
        buff.ReadUInt32(); //00 00 00 00 
        buff.ReadUInt32(); //00 00 00 00 
        buff.ReadUInt16();
		/*RunSpeed = buff.ReadUInt32();
		WalkSpeed = buff.ReadUInt32();
		SwimRunSpeed = buff.ReadUInt32();
		SwimWalkSpeed = buff.ReadUInt32();
		flRunSpeed = buff.ReadUInt32();
		flWalkSpeed = buff.ReadUInt32();
		FlyRunSpeed = buff.ReadUInt32();
		FlyWalkSpeed = buff.ReadUInt32();*/

		//MoveSpeedMult = System.Convert.ToSingle(buff.ReadDouble());
		//AttackSpeedMult = System.Convert.ToSingle(buff.ReadDouble());
		CollisionRadius = System.Convert.ToSingle(buff.ReadDouble());
		CollisionHeight = System.Convert.ToSingle(buff.ReadDouble());

        //GD OK
        buff.ReadUInt32();
        buff.ReadUInt32();
        buff.ReadUInt16();
		HairSytle = buff.ReadUInt32();
		HairColor = buff.ReadUInt32();
		Face = buff.ReadUInt32();

		Title = buff.ReadString();
		ClanID = buff.ReadUInt32();
		ClanCrestID = buff.ReadUInt32();
		AllyID = buff.ReadUInt32();
		AllyCrestID = buff.ReadUInt32();

        //GD OK

		isSitting = buff.ReadByte();
		isRunning = buff.ReadByte();
		isInCombat = buff.ReadByte();
		isAlikeDead = buff.ReadByte();
        // re: hide 
        //
        // When a player casts hide the server sets their CI 
        // AbnormalEffect to the Stealth Mask and sets this byte to 1.
        // It then sends all clients a DeleteObject packet.
        // Since the Delete object packet is what actually makes the player
        // vanish this byte is responsible for handling what happens if the player
        // logs out and then back in.  Basically if this byte is set when 
        // a player logs in, this tells all clients not to draw the actor :)
        // !! This byte alone does not mean a player is invisible, just that they will
        //  load invisible !!
		Invisible = buff.ReadByte();
		MountType = buff.ReadByte();
		PrivateStoreType = buff.ReadByte();
	
		CubicCount = buff.ReadUInt16();
		Cubics.Clear();
		for (uint i = 0; i < CubicCount; i++)
		{
			uint tmpc = buff.ReadUInt16();//ushort
			Cubics.Add(tmpc);
		}
    
		FindParty = buff.ReadByte();


        //buff.ReadUInt32();


		isFlying = buff.ReadByte();
		RecAmount = buff.ReadUInt16();

        buff.ReadUInt32(); //00 00 00 00 
		Class = buff.ReadUInt32();//class id

        //GD OK

		buff.ReadUInt32();//00
		EnchantAmount = buff.ReadByte();
		TeamCircle = buff.ReadByte();//team color

		ClanCrestIDLarge = buff.ReadUInt32();

		HeroIcon = buff.ReadByte();//is noble
		HeroGlow = buff.ReadByte();//is hero

		isFishing = buff.ReadByte();
		FishX = buff.ReadInt32();
		FishY = buff.ReadInt32();
		FishZ = buff.ReadInt32();

		NameColor = buff.ReadUInt32();
		Heading = buff.ReadInt32();//heading
		PledgeClass = buff.ReadByte();
        buff.ReadByte();//pledge type
		TitleColor = buff.ReadUInt32();

        //GD OK

		DemonSword = buff.ReadUInt16();//z sword? //0 0 0 0
		buff.ReadUInt32();//clan rep
		Transform_ID = buff.ReadUInt32();//transformation id
		Agathon_ID = buff.ReadUInt32();//agathon id


        buff.ReadUInt32();//01 00 00 00 
	

		ExtendedEffects = buff.ReadUInt32(); // Extended VFX (stigma etc)
        buff.ReadInt16();


        Cur_CP =  buff.ReadUInt32(); //64 00 00 00 

        Max_HP =  buff.ReadUInt32(); //C1 00 00 00
        Cur_HP = buff.ReadUInt32(); //C1 00 00 00
        Max_MP = buff.ReadUInt32(); //27 00 00 00
        Cur_MP =  buff.ReadUInt32(); //27 00 00 00

        buff.ReadUInt32(); //00 00 00 00 
        buff.ReadByte(); //00

        AbnormalEffects = 0x00;
        uint AbnEffCount = buff.ReadUInt32();
        AbnEffects.Clear();

        for (uint i = 0; i < (uint)AbnEffCount; i++)
        {
            uint tmpabneff = buff.ReadUInt32();
            //Globals.l2net_home.Add_Text("Adding abnormal vfx: " + tmpabneff.ToString("X2") + "to : " + Name, Globals.Yellow, TextType.BOT);
            AbnEffects.Add(tmpabneff);
        }
        buff.ReadInt16();
        }

        public void Load_UI(ByteBuffer buff)
        {
            //this only gets called if l2j fucks up

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
            buff.ReadUInt64();//FB 62 76 43 0 0 0 0 //XP
            buff.ReadUInt32();//12 0 0 0 //STR
            buff.ReadUInt32();//15 0 0 0 //DEX
            buff.ReadUInt32();//1F 0 0 0 //CON
            buff.ReadUInt32();//27 0 0 0 //INT
            buff.ReadUInt32();//1A 0 0 0 //WIT
            buff.ReadUInt32();//23 0 0 0 //MEN

            Max_HP = buff.ReadUInt32();//9B 11 0 0
            Cur_HP = buff.ReadUInt32();//9B 11 0 0
            Max_MP = buff.ReadUInt32();//F F 0 0
            Cur_MP = buff.ReadUInt32();//F F 0 0
            buff.ReadUInt32();//CA DF 59 0 //SP
            buff.ReadUInt32();//CE 41 0 0 //Cur_Load
            buff.ReadUInt32();//7C 28 1 0 //Max_Load
            buff.ReadUInt32();//some junk data//0x28//28 0 0 0

            buff.ReadUInt32();//0 0 0 0 //obj_Under
            buff.ReadUInt32();//F 46 8B 40 //obj_REar
            buff.ReadUInt32();//91 E5 78 40 //obj_LEar
            buff.ReadUInt32();//58 E8 50 40 //obj_Neck
            buff.ReadUInt32();//64 A1 87 40 //obj_RFinger
            buff.ReadUInt32();//D9 64 83 40 //obj_LFinger
            buff.ReadUInt32();//41 13 89 40 //obj_Head
            buff.ReadUInt32();//C2 E4 88 40 //obj_RHand
            buff.ReadUInt32();//97 E6 89 40 //obj_LHand
            buff.ReadUInt32();//37 39 8B 40 //obj_Gloves
            buff.ReadUInt32();//DE 16 8B 40 //obj_Chest
            buff.ReadUInt32();//93 1A 87 40 //obj_Legs
            buff.ReadUInt32();//9C FA 84 40 //obj_Feet
            buff.ReadUInt32();//0 0 0 0 //obj_Back
            buff.ReadUInt32();//0 0 0 0 //obj_LRHand
            buff.ReadUInt32();//E1 43 8B 40 //obj_Hair
            buff.ReadUInt32(); //obj_Face

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
                buff.ReadUInt32();//CT2.3
            }

            Underwear = buff.ReadUInt32();//0 0 0 0
            buff.ReadUInt32();//5 1A 0 0 //REar
            buff.ReadUInt32();//3 1A 0 0 //LEar
            buff.ReadUInt32();//98 3 0 0 //Neck
            buff.ReadUInt32();//6 1A 0 0 //RFinger
            buff.ReadUInt32();//79 3 0 0 //LFinger
            Head = buff.ReadUInt32();//23 2 0 0
            RHand = buff.ReadUInt32();//B9 19 0 0
            LHand = buff.ReadUInt32();//E9 18 0 0
            Gloves = buff.ReadUInt32();//8A 16 0 0
            Chest = buff.ReadUInt32();//60 9 0 0
            Legs = buff.ReadUInt32();//65 9 0 0
            Feet = buff.ReadUInt32();//96 16 0 0
            Back = buff.ReadUInt32();//0 0 0 0
            LRHand = buff.ReadUInt32();//0 0 0 0
            Hair = buff.ReadUInt32();//E 1E 0 0
            DollFace = buff.ReadUInt32();

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
                buff.ReadUInt32();//CT2.3
            }

            buff.ReadUInt32();//aug underwear
            buff.ReadUInt32();//aug Rear
            buff.ReadUInt32();//aug Lear
            buff.ReadUInt32();//aug neck
            buff.ReadUInt32();//aug R finger
            buff.ReadUInt32();//aug L finger
            buff.ReadUInt32();//aug head
            aug_RHand = buff.ReadUInt32();
            aug_LHand = buff.ReadUInt32();
            buff.ReadUInt32();//aug gloves
            buff.ReadUInt32();//aug chest
            buff.ReadUInt32();//aug legs
            buff.ReadUInt32();//aug feet
            buff.ReadUInt32();//aug back
            buff.ReadUInt32();//aug lr hand
            buff.ReadUInt32();//aug hair
            buff.ReadUInt32();//aug face

            buff.ReadUInt32();//aug right bracelet
            buff.ReadUInt32();//aug left bracelet
            buff.ReadUInt32();//aug deco 1
            buff.ReadUInt32();//aug deco 2
            buff.ReadUInt32();//aug deco 3
            buff.ReadUInt32();//aug deco 4
            buff.ReadUInt32();//aug deco 5
            buff.ReadUInt32();//aug deco 6

            if (Globals.gamedata.Chron >= Chronicle.CT2_3)
            {
                buff.ReadUInt32();//CT2.3

                buff.ReadUInt32();//CT2.3
                buff.ReadUInt32();//CT2.3
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

            buff.ReadUInt32();//F8 1 0 0 //Patk
            PatkSpeed = buff.ReadUInt32();//FD 1 0 0
            buff.ReadUInt32();//C3 2 0 0 //PDef
            buff.ReadUInt32();//5F 0 0 0 //Evasion
            buff.ReadUInt32();//71 0 0 0 //Accuracy
            buff.ReadUInt32();//28 0 0 0 //Focus
            buff.ReadUInt32();//99 2 0 0 //Matk

            MatkSpeed = buff.ReadUInt32();//DD 2 0 0
            PatkSpeed = buff.ReadUInt32();//twice...who knows why//FD 1 0 0

            buff.ReadUInt32();//22 5 0 0 //MDef

            if (Globals.gamedata.Chron >= Chronicle.CT3_0)
            {
                buff.ReadUInt32(); //m.accuracy
                buff.ReadUInt32(); //m.evasion
                buff.ReadUInt32(); //m.critical

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
            buff.ReadUInt32();//0 0 0 0 //is GM? //AccessLevel

            Title = buff.ReadString();//4C 0 65 0 74 0 73 0 20 0 62 0 65 0 20 0 66 0 72 0 69 0 65 0 6E 0 64 0 73 0 21 0 0 0

            ClanID = buff.ReadUInt32();//D1 2E 0 0
            ClanCrestID = buff.ReadUInt32();//EE C6 0 0
            AllyID = buff.ReadUInt32();//0 0 0 0
            AllyCrestID = buff.ReadUInt32();//0 0 0 0
            buff.ReadUInt32();//20 2 0 0 //relation //isClanLeader

            MountType = buff.ReadByte();//0
            PrivateStoreType = buff.ReadByte();//0
            buff.ReadByte();//0 //hasDwarfCraft
            buff.ReadUInt32();//F 0 0 0 //PKCount
            buff.ReadUInt32();//E4 2 0 0 //PvPCount

            CubicCount = buff.ReadUInt16();//0 0
            Cubics.Clear();
            for (uint i = 0; i < (uint)CubicCount; i++)
            {
                uint tmpc = buff.ReadUInt16();//ushort
                Cubics.Add(tmpc);
            }

            FindParty = buff.ReadByte();//0

            AbnormalEffects = buff.ReadUInt32();//0 0 0 0

            //Globals.l2net_home.Add_Text("AbnormalEffects: " + AbnormalEffects.ToString("X2"), Globals.Green, TextType.BOT);
            buff.ReadByte();//dunno//0

            buff.ReadUInt32();//8E AC C 0 //ClanPrivileges

            isFlying = buff.ReadUInt16();//9 0
            RecAmount = buff.ReadUInt16();//13 0
            buff.ReadUInt32();//0 0 0 0 //getMountNpcId() + 1000000
            buff.ReadUInt16();//50 0 //InventoryLimit

            Class = buff.ReadUInt32();//classid again //61 0 0 0
            buff.ReadUInt32();//special effects? //0 0 0 0 //SpecialEffects

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

                Transform_ID = buff.ReadUInt32();//Transformation ID

                //these changed in CT2.3...
                buff.ReadInt16();//Attack Element
                buff.ReadInt16();//Attack Element Value
                buff.ReadInt16();//Def Attr Fire
                buff.ReadInt16();//Def Attr Water
                buff.ReadInt16();//Def Attr Wind
                buff.ReadInt16();//Def Attr Earth
                buff.ReadInt16();//Def Attr Holy
                buff.ReadInt16();//Def Attr Unholy

                Agathon_ID = buff.ReadUInt32();//AgathionId

                if (Globals.gamedata.Chron >= Chronicle.CT2_1)
                {
                    //C9 - CT2.5
                    buff.ReadInt32(); //Fame
                    buff.ReadInt32();// Allow or Prevent opening of mini map (hb cert)
                    buff.ReadInt32();//Vitality Level
                    buff.ReadUInt32(); // EXtended VFX
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

        public void CopyNew(CharInfo ch_inf)
        {
            X = ch_inf.X;
            Y = ch_inf.Y;
            Z = ch_inf.Z;

            Underwear = ch_inf.Underwear;
            Head = ch_inf.Head;
            RHand = ch_inf.RHand;
            LHand = ch_inf.LHand;
            Gloves = ch_inf.Gloves;
            Chest = ch_inf.Chest;
            Legs = ch_inf.Legs;
            Feet = ch_inf.Feet;
            Back = ch_inf.Back;
            LRHand = ch_inf.LRHand;
            Hair = ch_inf.Hair;
            DollFace = ch_inf.DollFace;

            aug_RHand = ch_inf.aug_RHand;
            aug_LHand = ch_inf.aug_LHand;

            PvPFlag = ch_inf.PvPFlag;
            Karma = ch_inf.Karma;

            MatkSpeed = ch_inf.MatkSpeed;
            PatkSpeed = ch_inf.PatkSpeed;

            PvPFlag2 = ch_inf.PvPFlag2;
            Karma2 = ch_inf.Karma2;

            RunSpeed = ch_inf.RunSpeed;
            WalkSpeed = ch_inf.WalkSpeed;
            SwimRunSpeed = ch_inf.SwimRunSpeed;
            SwimWalkSpeed = ch_inf.SwimWalkSpeed;
            flRunSpeed = ch_inf.flRunSpeed;
            flWalkSpeed = ch_inf.flWalkSpeed;
            FlyRunSpeed = ch_inf.FlyRunSpeed;
            FlyWalkSpeed = ch_inf.FlyWalkSpeed;

            MoveSpeedMult = ch_inf.MoveSpeedMult;
            AttackSpeedMult = ch_inf.AttackSpeedMult;
            CollisionRadius = ch_inf.CollisionRadius;
            CollisionHeight = ch_inf.CollisionHeight;

            HairSytle = ch_inf.HairSytle;
            HairColor = ch_inf.HairColor;
            Face = ch_inf.Face;

            Title = ch_inf.Title;
            ClanID = ch_inf.ClanID;
            ClanCrestID = ch_inf.ClanCrestID;
            AllyID = ch_inf.AllyID;
            AllyCrestID = ch_inf.AllyCrestID;
            SiegeFlags = ch_inf.SiegeFlags;

            isSitting = ch_inf.isSitting;
            isRunning = ch_inf.isRunning;
            isInCombat = ch_inf.isInCombat;
            isAlikeDead = ch_inf.isAlikeDead;

            isFlying = ch_inf.isFlying;
            RecAmount = ch_inf.RecAmount;

            Class = ch_inf.Class;
            BaseClass = ch_inf.BaseClass;

            HeroIcon = ch_inf.HeroIcon;
            HeroGlow = ch_inf.HeroGlow;

            isFishing = ch_inf.isFishing;
            FishX = ch_inf.FishX;
            FishY = ch_inf.FishY;
            FishZ = ch_inf.FishZ;

            NameColor = ch_inf.NameColor;

            DemonSword = ch_inf.DemonSword;

            AbnormalEffects = ch_inf.AbnormalEffects;
            ExtendedEffects = ch_inf.ExtendedEffects;
            // Relation = ch_inf.Relation;
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
                    buff.ReadUInt64();
					break;
				case 0x03://str
                    buff.ReadUInt32();
					break;
				case 0x04://dex
                    buff.ReadUInt32();
					break;
				case 0x05://con
                    buff.ReadUInt32();
					break;
				case 0x06://int
                    buff.ReadUInt32();
					break;
				case 0x07://wit
                    buff.ReadUInt32();
					break;
				case 0x08://men
                    buff.ReadUInt32();
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
                    buff.ReadUInt32();//SP
					break;
				case 0x0E://cur load
                    buff.ReadUInt32();
					break;
				case 0x0F://max load
                    buff.ReadUInt32();
					break;
				case 0x10://..
                    buff.ReadUInt32();
					break;
				case 0x11://patk
                    buff.ReadUInt32();
					break;
				case 0x12://atk spd
                    PatkSpeed = buff.ReadUInt32();
					break;
				case 0x13://pdef
                    buff.ReadUInt32();
					break;
				case 0x14://evasion
                    buff.ReadUInt32();
					break;
				case 0x15://acc
                    buff.ReadUInt32();
					break;
				case 0x16://crit
                    buff.ReadUInt32();
					break;
				case 0x17://m atk
                    buff.ReadUInt32();
					break;
				case 0x18://cast spd
					MatkSpeed = buff.ReadUInt32();
					break;
				case 0x19://mdef
                    buff.ReadUInt32();
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
	}//end of player info
}
