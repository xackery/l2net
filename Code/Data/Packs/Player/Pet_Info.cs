namespace L2_login
{
    public class Pet_Info : Object_Base
    {
        public volatile uint SummonType = 0;
        public volatile uint NPCID = 0;

        public volatile float X = 0;
        public volatile float Y = 0;
        public volatile float Z = 0;
        public volatile uint isAttackAble = 0;

        public volatile int Heading = 0;

        public volatile uint MatkSpeed = 0;
        public volatile uint PatkSpeed = 0;
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

        public volatile uint LWeapon = 0;
        public volatile uint Armor = 0;
        public volatile uint RWeapon = 0;

        public volatile uint HasOwner = 0;//1 has ownwer, 0 no owner
        public volatile uint isRunning = 1;//running = 1
        public volatile uint isInCombat = 0;//attacking = 1
        public volatile uint isAlikeDead = 0;//dead = 1
        public volatile uint isSummoned = 0;//0 = teleported, 1 = default, 2 = summoned

        private string _Name = "";
        private string _Title = "";

        public volatile uint PvPFlag = 0;
        public volatile int Karma = 0;

        public volatile uint Cur_Fed = 0;
        public volatile uint Max_Fed = 0;

        public volatile float Cur_HP = 0;
        public volatile float Max_HP = 0;

        public volatile float Cur_MP = 0;
        public volatile float Max_MP = 0;

        public volatile float Cur_CP = 0;
        public volatile float Max_CP = 0;

        public volatile uint SP = 0;
        public volatile uint Level = 0;
        private ulong _XP = 0;

        private ulong _XP_ThisLevel = 0;
        private ulong _XP_NextLevel = 0;

        public volatile uint Cur_Load = 0;
        public volatile uint Max_Load = 0;

        public volatile uint Patk = 0;
        public volatile uint PDef = 0;
        public volatile uint Matk = 0;
        public volatile uint MDef = 0;
        public volatile uint Accuracy = 0;
        public volatile uint Evasion = 0;
        public volatile uint Focus = 0;

        public volatile uint AbnormalEffects = 0;
        public volatile uint Mountable = 0;//1 = ride-able

        public volatile uint TeamCircle = 0;//1= Blue, 2 = red//byte
        public volatile uint SSUsage = 0;
        public volatile uint SPSUSage = 0;

        public volatile uint Form = 0;

        public volatile float Dest_X = 0;
        public volatile float Dest_Y = 0;
        public volatile float Dest_Z = 0;
        public volatile bool Moving = false;
        private System.DateTime _lastMoveTime = System.DateTime.Now;
        public volatile uint MoveTarget = 0;
        public volatile TargetType MoveTargetType = TargetType.NONE;

        public volatile uint TargetID = 0;
        public volatile TargetType CurrentTargetType = TargetType.NONE;

        private System.Collections.SortedList _inventory = new System.Collections.SortedList();

        private readonly object NameLock = new object();
        private readonly object TitleLock = new object();
        private readonly object XPLock = new object();
        private readonly object XP_ThisLevelLock = new object();
        private readonly object XP_NextLevelLock = new object();

        private readonly object lastMoveTimeLock = new object();

        private readonly object inventoryLock = new object();
        private System.Collections.ArrayList _AbnEffects = new System.Collections.ArrayList();
        public bool HasEffect(AbnormalEffects test)
        {
            return (AbnormalEffects & (uint)test) != 0;
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
            }
        }
        public ulong XP
        {
            get
            {
                ulong tmp;
                lock (XPLock)
                {
                    tmp = this._XP;
                }
                return tmp;
            }
            set
            {
                lock (XPLock)
                {
                    _XP = value;
                }
            }
        }
        public ulong XP_ThisLevel
        {
            get
            {
                ulong tmp;
                lock (XP_ThisLevelLock)
                {
                    tmp = this._XP_ThisLevel;
                }
                return tmp;
            }
            set
            {
                lock (XP_ThisLevelLock)
                {
                    _XP_ThisLevel = value;
                }
            }
        }
        public ulong XP_NextLevel
        {
            get
            {
                ulong tmp;
                lock (XP_NextLevelLock)
                {
                    tmp = this._XP_NextLevel;
                }
                return tmp;
            }
            set
            {
                lock (XP_NextLevelLock)
                {
                    _XP_NextLevel = value;
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

        public System.Collections.SortedList inventory
        {
            get
            {
                System.Collections.SortedList tmp;
                lock (inventoryLock)
                {
                    tmp = this._inventory;
                }
                return tmp;
            }
            set
            {
                lock (inventoryLock)
                {
                    _inventory = value;
                }
            }
        }

        public void Load_Pet(ByteBuffer buff)
        {
            SummonType = buff.ReadUInt32(); //1 = summon, 2 = pet
            ID = buff.ReadUInt32();
            NPCID = buff.ReadUInt32();
            isAttackAble = buff.ReadUInt32();//attackable = 0

            X = buff.ReadUInt32();
            Y = buff.ReadUInt32();
            Z = buff.ReadUInt32();
            Heading = buff.ReadInt32();
            buff.ReadUInt32();//0x00

            MatkSpeed = buff.ReadUInt32();
            PatkSpeed = buff.ReadUInt32();
            RunSpeed = buff.ReadUInt32();
            WalkSpeed = buff.ReadUInt32();
            SwimRunSpeed = buff.ReadUInt32();
            SwimWalkSpeed = buff.ReadUInt32();
            flRunSpeed = buff.ReadUInt32();
            flWalkSpeed = buff.ReadUInt32();
            if (Globals.gamedata.Chron >= Chronicle.CT1)
            {
                FlyRunSpeed = buff.ReadUInt32();
                FlyWalkSpeed = buff.ReadUInt32();
            }

            MoveSpeedMult = System.Convert.ToSingle(buff.ReadDouble());
            AttackSpeedMult = System.Convert.ToSingle(buff.ReadDouble());
            CollisionRadius = System.Convert.ToSingle(buff.ReadDouble());
            CollisionHeight = System.Convert.ToSingle(buff.ReadDouble());

            LWeapon = buff.ReadUInt32();
            Armor = buff.ReadUInt32();
            RWeapon = buff.ReadUInt32();

            HasOwner = buff.ReadByte(); //owneronline
            isRunning = buff.ReadByte();
            isInCombat = buff.ReadByte();
            isAlikeDead = buff.ReadByte();
            isSummoned = buff.ReadByte(); //isSummoned 0=teleported  1=default   2=summoned

            if (Globals.gamedata.Chron >= Chronicle.CT3_0)
            {
                buff.ReadUInt32(); //FF FF FF FF
            }
            Name = buff.ReadString();
            if (string.IsNullOrWhiteSpace(Name))
            {
                Name = Util.GetNPCName(NPCID);//"Unnamed Pet";
            }
            if (Globals.gamedata.Chron >= Chronicle.CT3_0)
            {
                buff.ReadUInt32(); //FF FF FF FF
            }
            Title = buff.ReadString(); //OwnerName
            buff.ReadUInt32();//1

            PvPFlag = buff.ReadUInt32();
            Karma = buff.ReadInt32();

            Cur_Fed = buff.ReadUInt32();
            Max_Fed = buff.ReadUInt32();

            Cur_HP = buff.ReadUInt32();
            Max_HP = buff.ReadUInt32();

            Cur_MP = buff.ReadUInt32();
            Max_MP = buff.ReadUInt32();

            SP = buff.ReadUInt32();
            Level = buff.ReadUInt32();
            XP = buff.ReadUInt64();

            XP_ThisLevel = buff.ReadUInt64();
            XP_NextLevel = buff.ReadUInt64();

            Cur_Load = buff.ReadUInt32();
            Max_Load = buff.ReadUInt32();

            Patk = buff.ReadUInt32();
            PDef = buff.ReadUInt32();
            Accuracy = buff.ReadUInt32();// p
            Evasion = buff.ReadUInt32();//p
            Focus = buff.ReadUInt32();//p
            Matk = buff.ReadUInt32();
            MDef = buff.ReadUInt32();

            buff.ReadUInt32();//m acu
            buff.ReadUInt32();//m eva
            buff.ReadUInt32();//m crit

            buff.ReadUInt32();// speed
            buff.ReadUInt32();//patak sped
            buff.ReadUInt32();// cast

            /*if (Globals.gamedata.Chron < Chronicle.CT3_0)
            {
                AbnormalEffects = buff.ReadUInt32(); //AbnormalEffect bleed=1; poison=2; poison & bleed=3; flame=4;
            }
            else
            {
                buff.ReadUInt32(); //??
            }*/
            Mountable = buff.ReadUInt16();

            buff.ReadByte();
            buff.ReadUInt16();

            if (Globals.gamedata.Chron >= Chronicle.CT1)
            {
                TeamCircle = buff.ReadByte();
                /*if (Globals.gamedata.Chron >= Chronicle.CT3_0)
                {
                    //buff.ReadUInt32(); //00 00 00 00
                    buff.ReadUInt16();
                    AbnormalEffects = buff.ReadUInt32();
                    buff.ReadUInt32(); //00 00 00 00
                    buff.ReadUInt16(); //00 00 00 00
                }*/
                SSUsage = buff.ReadUInt32();
                SPSUSage = buff.ReadUInt32();
                Form = buff.ReadUInt32();
                buff.ReadUInt32();//0x00
                if (Globals.gamedata.Chron >= Chronicle.CT3_0)
                {
                    //buff.ReadUInt16(); //00 00
                    try
                    {
                        buff.ReadUInt32(); //00 00
                        buff.ReadUInt32(); //02 00 00 00 current pet points
                        buff.ReadUInt32(); //06 00 00 00 max pet points

                        uint abn_count = buff.ReadUInt32();
                        if (abn_count < 30) // well ... its oddi wayso :P
                        {
                            _AbnEffects.Add(buff.ReadUInt32());
                        }


                    }
                    catch
                    {

                    }
                }
            }
            else
            {
                HasOwner = buff.ReadByte();
                SSUsage = buff.ReadUInt32();
                Form = buff.ReadUInt32();
                buff.ReadUInt32();//0x00
            }
        }

        public void PetUpdate(ByteBuffer buff)
        {
            SummonType = buff.ReadUInt32();
            ID = buff.ReadUInt32();

            X = buff.ReadUInt32();
            Y = buff.ReadUInt32();
            Z = buff.ReadUInt32();

            buff.ReadString(); //title

            Cur_Fed = buff.ReadUInt32();
            Max_Fed = buff.ReadUInt32();

            Cur_HP = buff.ReadUInt32();
            Max_HP = buff.ReadUInt32();

            Cur_MP = buff.ReadUInt32();
            Max_MP = buff.ReadUInt32();

            Level = buff.ReadUInt32();
            XP = buff.ReadUInt64();
            XP_ThisLevel = buff.ReadUInt64();
            XP_NextLevel = buff.ReadUInt64();

            if (Globals.gamedata.Chron >= Chronicle.CT3_0)
            {
                buff.ReadUInt32(); //00 00 00 00
            }
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
                    XP = buff.ReadUInt64();
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
    }
}
