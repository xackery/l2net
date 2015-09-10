using System;

namespace L2_login
{
    public class NPCInfo : Object_Base
    {
        public volatile uint NPCID = 0;//this it the NPC type (from the file)
        public volatile uint isAttackable = 0;//0 = invincible... non zero = attackable
        public volatile float X = 0;
        public volatile float Y = 0;
        public volatile float Z = 0;
        public volatile int Heading = 0;
        public volatile uint MatkSpeed = 0;
        public volatile uint PatkSpeed = 0;
        public volatile uint Level = 0;

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

        public volatile uint RHand = 0;
        public volatile uint LRHand = 0;//unknown, maybe correct
        public volatile uint LHand = 0;
        public volatile uint NameShows = 0;//byte

        public volatile uint isRunning = 1;// running = 1   walking = 0//byte
        public volatile uint isSitting = 1;
        public volatile uint isInCombat = 0;//byte
        private volatile uint _isAlikeDead = 0;//byte
        private volatile uint _isInvisible = 0;//0 - false | 1 - true | 2 - summoned//byte
        public volatile uint isTargetable = 0;//byte
        public volatile uint showName = 0;//byte
        

        private string _Name = "";
        private string _Title = "";
        public volatile int Karma = 0;//maybe
        public volatile uint PvPFlag = 0;//maybe
        public volatile uint SummonedNameColor = 0;//maybe

        public volatile uint AbnormalEffects = 0;
        public volatile uint ExtendedEffects = 0;
        public volatile uint TeamCircle = 0;//byte
        private System.Collections.ArrayList _AbnEffects = new System.Collections.ArrayList();

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

        private long _RemoveAt = long.MaxValue;
        public volatile bool Persist = false;
        public volatile bool Ignore = false;
        public volatile bool IsSpoiled = false;

        private readonly object RemoveAtLock = new object();
        private readonly object lastMoveTimeLock = new object();
        private readonly object TitleLock = new object();
        private readonly object NameLock = new object();
        private readonly object AbnEffectsLock = new object();

        private System.Collections.SortedList _my_buffs;
        private readonly object my_buffsLock = new object();


        public bool HasEffect(AbnormalEffects test)
        {
            return AbnEffects.IndexOf((uint)test) != -1;

            //return (AbnormalEffects & (uint)test) != 0;
        }

        public bool HasExtendedEffect(ExtendedEffects test)
        {
            return (ExtendedEffects & (uint)test) != 0;
        }

        public bool PriorityTarget()
        {
            if (this.TargetID == 0)
            {
                return false;
            }

            //check if targeting any party member
            Globals.PartyLock.EnterReadLock();
            try
            {
                if (Globals.gamedata.PartyMembers.ContainsKey(this.TargetID))
                {
                    return true;
                }
            }
            finally
            {
                Globals.PartyLock.ExitReadLock();
            }

            //check if targeting any out of party member
            if (Globals.gamedata.botoptions.OOP == 1 && Globals.gamedata.botoptions.OOPIDs.Contains(this.TargetID))
            {
                return true;
            }

            return false;
        }

        public bool CheckCombat()
        {
            //if we are in combat, check against player and party
            if (isInCombat == 0)
            {
                return false;
            }
            else
            {
                //need to change this...
                //currently... not in combat if target is targeting us, friendly, or npc
                //if we are targeting the player return false

                if (this.TargetID == Globals.gamedata.my_char.ID)
                {
                    return false;
                }

                if (PriorityTarget())
                {
                    return false;
                }

                CharInfo char_target = null;

                Globals.PlayerLock.EnterReadLock();
                try
                {
                    char_target = Util.GetChar(TargetID);
                }
                finally
                {
                    Globals.PlayerLock.ExitReadLock();
                }

                if (char_target != null)
                {
                    return true;
                }

                return false;
            }
        }

        public void Active()
        {
            if (isAttackable == 0x00 || Persist)
            {
                //town npc...
                RemoveAt = System.DateTime.Now.Ticks + Globals.NPC_RemoveAtInvin;
            }
            else
            {
                RemoveAt = System.DateTime.Now.Ticks + Globals.NPC_RemoveAtActive;
            }
        }

        public long RemoveAt
        {
            get
            {
                long tmp;
                lock (RemoveAtLock)
                {
                    tmp = this._RemoveAt;
                }
                return tmp;
            }
            set
            {
                lock (RemoveAtLock)
                {
                    _RemoveAt = value;
                }
            }
        }
        public uint isAlikeDead
        {
            get
            {
                return _isAlikeDead;
            }
            set
            {
                _isAlikeDead = value;

                if (_isAlikeDead == 1)
                {
                    RemoveAt = System.DateTime.Now.Ticks + Globals.NPC_RemoveAtDead;
                }
                else
                {
                    Active();
                }
            }
        }
        public uint isInvisible
        {
            get
            {
                return _isInvisible;
            }
            set
            {
                _isInvisible = value;

                if (_isInvisible == 0)
                {
                    Active();
                }
                else
                {
                    RemoveAt = System.DateTime.Now.Ticks + Globals.NPC_RemoveAtDead;
                }
            }
        }
        public string Name
        {
            get
            {
                string tmp;
                lock (NameLock)
                {
                    tmp = this._Name;
                }
                return tmp;
            }
            set
            {
                lock (NameLock)
                {
                    _Name = value;
                }
                Active();
            }
        }
        public string Title
        {
            get
            {
                string tmp;
                lock (TitleLock)
                {
                    tmp = this._Title;
                }
                return tmp;
            }
            set
            {
                lock (TitleLock)
                {
                    _Title = value;
                }
                Active();
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
                lock (lastMoveTimeLock)
                {
                    tmp = this._lastMoveTime;
                }
                return tmp;
            }
            set
            {
                lock (lastMoveTimeLock)
                {
                    _lastMoveTime = value;
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

        public NPCInfo()
        {
            my_buffs = new System.Collections.SortedList();
        }

        public void Copy(NPCInfo copy)
        {
            NPCID = copy.NPCID;
            isAttackable = copy.isAttackable;
            X = copy.X;
            Y = copy.Y;
            Z = copy.Z;
            Heading = copy.Heading;
            MatkSpeed = copy.MatkSpeed;
            PatkSpeed = copy.PatkSpeed;

            RunSpeed = copy.RunSpeed;
            WalkSpeed = copy.WalkSpeed;
            SwimRunSpeed = copy.SwimRunSpeed;
            SwimWalkSpeed = copy.SwimWalkSpeed;
            flRunSpeed = copy.flRunSpeed;
            flWalkSpeed = copy.flWalkSpeed;
            FlyRunSpeed = copy.FlyRunSpeed;
            FlyWalkSpeed = copy.FlyWalkSpeed;

            MoveSpeedMult = copy.MoveSpeedMult;
            AttackSpeedMult = copy.AttackSpeedMult;
            CollisionRadius = copy.CollisionRadius;
            CollisionHeight = copy.CollisionHeight;

            RHand = copy.RHand;
            LRHand = copy.LRHand;
            LHand = copy.LHand;
            NameShows = copy.NameShows;

            isRunning = copy.isRunning;
            isInCombat = copy.isInCombat;
            isAlikeDead = copy.isAlikeDead;
            isInvisible = copy.isInvisible;

            Name = copy.Name;
            Title = copy.Title;

            Karma = copy.Karma;
            PvPFlag = copy.PvPFlag;
            SummonedNameColor = copy.SummonedNameColor;

            AbnormalEffects = copy.AbnormalEffects;
            ExtendedEffects = copy.ExtendedEffects;
            my_buffs = copy.my_buffs;
        }

        public void LoadEX(ByteBuffer buff)
        {
            /*FE 
            66 01 

            03 46 10 48 //id

            00 25 00 ED //NPCID?
            
            BE 4E A2 0C 
            07 00 00 00 
            00 00 00 00 
            38 00 40 C6 
            0F 00 

            B0 43 00 00 //x
            80 97 02 00 //y
            58 F2 FF FF //z
            24 3F 00 00 8E 01 00 00 2B 01 00 00 00 00 80 3F 97 08 8A 3F 01 01 00 00 00 00 00 00 00 00 00 14 39 00 00 14 39 00 00 0C 00 00 */

            /* -----------------------------------------------------
             * --- from adifenix :P
             *  FE 66 01
                D8 F1 A0 4E id
                00 // 1 - update (smal pck) or animaion.. / 0 - normal pck
                25 00 //?
                ED BE 4E A2 //?
                0C //?
                07 01 00 00 //?
                00 00 00 00 //?
                38 00 //?
                2C 9C 0F 00 typeid
                7C D5 FE FF  x
                DF 72 02 00  y
                48 F3 FF FF  z
                AD 54 00 00  // cast ata sped ?
                4E 01 00 00 //atta sped ?
                17 01 00 00 // ..?
                00 00 //?
                80 3F // max?
                24 97 // cur?
            80 3F // max ?
            01 00 // cur?
            00 00 00 00 //?
            00 00 00 00 //?
            00 //??
            41 02 00 00 -- if im not wrong ... if u chnage the last 02(in alot of cases there is 0c) to 00 then u cant see title (naughty boys and girls at ti :P)
            after some tests ..
            it look like now alot of thngs deppends on l2 files .. ex showing hp bar
            i tested it using same packed (guard of city) and just changed the typeid to mob one and hp bar apeared ..
            ------------------------------------------------
             // thats how i would parse it
             */
            ID = buff.ReadUInt32();
            int update = buff.ReadByte();
            if (update == 0) // aka normal "fresh" data 
            {
                buff.ReadInt16();
                buff.ReadInt32();
                buff.ReadByte();
                buff.ReadInt32();
                buff.ReadInt32();
                buff.ReadInt16();
                NPCID = buff.ReadUInt32();
                Name = Util.GetNPCName(NPCID);
                Title = Util.GetNPCTitle(NPCID);
                X = buff.ReadInt32();
                Y = buff.ReadInt32();
                Z = buff.ReadInt32();
                Dest_X = X;
                Dest_Y = Y;
                Dest_Z = Z;
                buff.ReadUInt32(); //??
                buff.ReadUInt32(); //8E 01 00 00 
                buff.ReadUInt32(); //2B 01 00 00
                buff.ReadUInt16(); //00 00

                buff.ReadUInt32(); //80 3F 97 08 
                buff.ReadUInt16(); //8A 3F 

                buff.ReadByte(); //01
                buff.ReadByte(); //01
                buff.ReadByte(); //00

                buff.ReadUInt32(); //00 00 00 00
                buff.ReadUInt32(); //00 00 00 00

                Cur_HP = buff.ReadUInt32(); //14 39 00 00 
                Max_HP = buff.ReadUInt32(); //14 39 00 00 
                NameShows = buff.ReadByte();// if 0 = hide name ..
                buff.ReadByte(); //00
                buff.ReadByte(); //00
            }
        }

        public void Load(ByteBuffer buff)
        {
            //ct2.2: 0C D5 5B 30 4F A6 95 0F 00 01 00 00 00 86 F2 01 00 54 68 FF FF 30 F2 FF FF 00 00 00 00 00 00 00 00 4D 01 00 00 16 01 00 00 AF 00 00 00 2C 00 00 00 AF 00 00 00 2C 00 00 00 AF 00 00 00 2C 00 00 00 AF 00 00 00 2C 00 00 00 9A 99 99 99 99 99 F1 3F 81 43 A8 52 B3 07 F0 3F 00 00 00 00 00 00 30 40 00 00 00 00 00 00 43 40 C8 09 00 00 00 00 00 00 06 1B 00 00 01 01 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 30 40 00 00 00 00 00 00 43 40 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00
            //int offset = 1;

            //13 March 2013
            //packet parser: cddddddddddddddddddffffdddcccccdhdsddddddddccffddddccddddddddchcdhchd(d)
            ID = buff.ReadUInt32();
            NPCID = buff.ReadUInt32();
            isAttackable = buff.ReadUInt32();

            X = buff.ReadInt32();
            Y = buff.ReadInt32();
            Z = buff.ReadInt32();
            Dest_X = X;
            Dest_Y = Y;
            Dest_Z = Z;

            Heading = buff.ReadInt32();
            buff.ReadUInt32();
            MatkSpeed = buff.ReadUInt32();
            PatkSpeed = buff.ReadUInt32();

            RunSpeed = buff.ReadUInt32();
            WalkSpeed = buff.ReadUInt32();
            SwimRunSpeed = buff.ReadUInt32();
            SwimWalkSpeed = buff.ReadUInt32();
            flRunSpeed = buff.ReadUInt32();
            flWalkSpeed = buff.ReadUInt32();
            FlyRunSpeed = buff.ReadUInt32();
            FlyWalkSpeed = buff.ReadUInt32();

            MoveSpeedMult = System.Convert.ToSingle(buff.ReadDouble());
            AttackSpeedMult = System.Convert.ToSingle(buff.ReadDouble());
            CollisionRadius = System.Convert.ToSingle(buff.ReadDouble());
            CollisionHeight = System.Convert.ToSingle(buff.ReadDouble());

            RHand = buff.ReadUInt32();
            LRHand = buff.ReadUInt32();
            LHand = buff.ReadUInt32();

            NameShows = buff.ReadByte();
            isRunning = buff.ReadByte();
            isInCombat = buff.ReadByte();
            isAlikeDead = buff.ReadByte();
            buff.ReadByte();
            isInvisible = 0;

            buff.ReadUInt32(); //FF FF FF FF 
            Name = buff.ReadString(); //00 00 
            buff.ReadUInt32(); //FF FF FF FF 
            /*
            Name = buff.ReadString();//Name
            if (Name.Length == 0)
            {
                Name = Util.GetNPCName(NPCID);
            }

            buff.ReadUInt32(); //FF FF FF FF 
            */
            if (Name.Length == 0)
            {
                Name = Util.GetNPCName(NPCID);
            }
            Title = buff.ReadString();

            //try and get the title for npcs (mobs, not summons)
            if (Title.Length == 0)
            {
                Title = Util.GetNPCTitle(NPCID);
            }

            //if that failed, we get the NPC's master's title... mobs get their name as their title, o well
            if (Title.Length == 0)
            {
                Title = Name;
            }
            //Globals.l2net_home.Add_Text("nametemp: " + nametemp + "Name: " + Name + "Title: " + Title, Globals.Red, TextType.ALL);

            //GD OK

            SummonedNameColor = buff.ReadUInt32();//0 = normal, 1 = summoned name color
            PvPFlag = buff.ReadUInt32();
            Karma = buff.ReadInt32();
            AbnormalEffects = buff.ReadUInt32();
            buff.ReadUInt32(); //clan ID??
            buff.ReadUInt32(); //crest ID??

            buff.ReadUInt32(); //clan ID
            buff.ReadUInt32(); //crest ID
            buff.ReadByte();   //isflying
            TeamCircle = buff.ReadByte();

            buff.ReadDouble();                      // col height
            buff.ReadDouble();                      // col rad

            buff.ReadUInt32();                      // 00 00 00 00
            buff.ReadUInt32();                      // 00 00 00 00
            buff.ReadInt32();                       // 00 00 00 00
            buff.ReadInt32();                       // 00 00 00 00

            //  buff.ReadInt32();                   // 00 00 00 00
            isTargetable = buff.ReadByte();         // 01
            showName = buff.ReadByte();             // 01
            if (isTargetable == 0 || showName == 0)
            {
                GameData.badmobs_ignored += 1;
            }
            ExtendedEffects = buff.ReadUInt32();    // 00 00 00 00
            buff.ReadInt32();                       // roacknow

            buff.ReadInt32();                       // rock now - cur hp
            buff.ReadInt32();                       // rock now - max hp
            buff.ReadInt32();                       // rock now - cur mp
            buff.ReadInt32();                       // rock now - max m
            buff.ReadInt32();                       // rock now - cur cp
            buff.ReadInt32();                       // rock now - max cp
            buff.ReadByte();                        //00
            buff.ReadInt16();                       //00 00
            buff.ReadByte();
            buff.ReadInt32();
            buff.ReadInt16();
            buff.ReadByte();
            buff.ReadInt16();

            int abn_count = buff.ReadInt32();

            if (abn_count >= 30)
            {
#if DEBUG
                Globals.l2net_home.Add_Text("More than 30 abnormal effects, probably an error... Count: " + abn_count, Globals.Yellow, TextType.BOT);
#endif
                abn_count = 10;
            }

            //Globals.l2net_home.Add_Text("count " + abn_count, Globals.Yellow, TextType.BOT);

            AbnEffects.Clear();
            for (uint i = 0; i < (uint)abn_count; i++)
            {
                uint tmpabneff = buff.ReadUInt32();
                //Globals.l2net_home.Add_Text("Adding abnormal vfx to npc : " + tmpabneff.ToString("X2") + " to : " + Name, Globals.Yellow, TextType.BOT);
                AbnEffects.Add(tmpabneff);
            }

        }

        public void LoadServerObject(ByteBuffer buff)
        {

            ID = buff.ReadUInt32();
            NPCID = buff.ReadUInt32();
            Name = buff.ReadString();
            if (Name.Length == 0)
            {
                Name = Util.GetNPCName(NPCID);
            }

            //buff.ReadUInt32(); //type
            isAttackable = buff.ReadUInt32();
            X = buff.ReadInt32();
            Y = buff.ReadInt32();
            Z = buff.ReadInt32();
            Heading = buff.ReadInt32();
            CollisionRadius = (float)buff.ReadDouble();
            CollisionHeight = (float)buff.ReadDouble();

            //isInvisible = buff.ReadUInt32();
           // buff.ReadUInt32();
           // buff.ReadUInt32();

            Cur_HP = buff.ReadUInt32();
            Max_HP = buff.ReadUInt32();
            Cur_MP = buff.ReadUInt32();
            Max_MP = buff.ReadUInt32();
            
            //buff.ReadUInt32(); //hpshown
            //buff.ReadUInt32(); //dmggrade

            //ID = buff.ReadUInt32();
            //NPCID = buff.ReadUInt32();
            //Name = buff.ReadString();
            //if (Name.Length == 0)
            //{
            //Name = Util.GetNPCName(NPCID);
            //}
            //isAttackable = buff.ReadUInt32();

            //X = buff.ReadInt32();
            //Y = buff.ReadInt32();
            //Z = buff.ReadInt32();

            //buff.ReadInt32();

            //MoveSpeedMult = System.Convert.ToSingle(buff.ReadDouble());
            //AttackSpeedMult = System.Convert.ToSingle(buff.ReadDouble());
            //CollisionRadius = System.Convert.ToSingle(buff.ReadDouble());
            //CollisionHeight = System.Convert.ToSingle(buff.ReadDouble());

            //Cur_HP = buff.ReadUInt32();
            //Max_HP = buff.ReadUInt32();

            //buff.ReadInt32();//type
            //buff.ReadInt32();

            Persist = true;
        }

        public void LoadStaticObject(ByteBuffer buff)
        {
            /*9F 
            AE C1 06 01 //id
            6D 01 10 58 //npcid

            01 00 00 00 //type
            01 00 00 00 //targetable
            01 00 00 00 //meshindex 
            01 00 00 00 //closed
            00 00 00 00 //enemy

            D8 E1 90 00 //currhp
            D8 E1 90 00 //maxhp
            00 00 00 00 //hpshown
            00 00 00 00 //dmgGrade
            */

            /*		
            writeC(0x9F);
		    writeD(_staticObjectId);
		    writeD(_objectId);
		    writeD(_type);
		    writeD(_targetable);
		    writeD(_meshIndex);
		    writeD(_closed);
		    writeD(_enemy);
		    writeD(_currentHp);
		    writeD(_maxHp);
		    writeD(_hpShown);
		    writeD(_damageGrade);*/

            ID = buff.ReadUInt32();
            NPCID = buff.ReadUInt32();
            Name = Util.GetNPCName(NPCID);

            buff.ReadUInt32(); //type
            isAttackable = buff.ReadUInt32();
            isInvisible = buff.ReadUInt32();
            buff.ReadUInt32();
            buff.ReadUInt32();

            Cur_HP = buff.ReadUInt32();
            Max_HP = buff.ReadUInt32();

            buff.ReadUInt32(); //hpshown
            buff.ReadUInt32(); //dmggrade




            //ID = buff.ReadUInt32();
            //NPCID = buff.ReadUInt32();
            //Name = buff.ReadString();
            //if (Name.Length == 0)
            //{
            //    Name = Util.GetNPCName(NPCID);
            //}
            //isAttackable = buff.ReadUInt32();

            //X = buff.ReadInt32();
            //Y = buff.ReadInt32();
            //Z = buff.ReadInt32();

            //buff.ReadInt32();//heading

            //MoveSpeedMult = System.Convert.ToSingle(buff.ReadDouble());
            //AttackSpeedMult = System.Convert.ToSingle(buff.ReadDouble());
            //CollisionRadius = System.Convert.ToSingle(buff.ReadDouble());
            //CollisionHeight = System.Convert.ToSingle(buff.ReadDouble());

            //Cur_HP = buff.ReadUInt32();
            //Max_HP = buff.ReadUInt32();

            //buff.ReadInt32();//type
            //buff.ReadInt32();

            Persist = true;
        }

        public void Update(ByteBuffer buff)
        {
            uint data = buff.ReadByte();

            switch (data)
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
                    Globals.l2net_home.Add_Text("Cur hp: " + Cur_HP.ToString());
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
                    buff.ReadUInt32();
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

            Active();
        }
    }//end of NPCInfo
}