namespace L2_login
{
	public class BotOptions
	{
		//party
        public volatile int ActiveFollow;//1 - yes
		private string _ActiveFollowName = "";
        public volatile uint ActiveFollowID = 0;
        public volatile int ActiveFollowStyle = 0;//1 - walker style | 0 - l2.net style(move to pawn)
        public volatile float ActiveFollowDistance = 200;
        public volatile int ActiveFollowAttack = 0;//1 - yes
        public volatile int ActiveFollowAttackInstant = 0; //1 = yes
        public volatile int ActiveFollowTarget = 0;//1 - yes
        public volatile int AutoSweep = 0;//1 = yes
        public volatile int AutoSpoil = 0;//1 = yes
        public volatile int AutoSpoilUntilSuccess = 0;//1 = yes
        public volatile int SpoilCrush = 0;//1 = yes
        public volatile int Plunder = 1;//1 = yes
        public volatile int IgnoreItems = 1;//1 = yes
        public volatile int IgnoreMeshlessItems = 1;//1 = yes
        public volatile int AcceptParty = 0;//1 = yes
        private string _AcceptPartyNames = "";
        public volatile int LeavePartyOnLeader = 0;//1 - yes
        public volatile int AcceptRezClan = 0;//1 - yes
        public volatile int AcceptPartyClan = 0;//1 - yes
        public volatile int AcceptRezAlly = 0;//1 - yes
        public volatile int AcceptPartyAlly = 0;//1 - yes
        public volatile int AcceptRezParty = 0;//1 - yes
        public volatile int SendParty = 0;//1 = yes
        private string _SendPartyNames = "";
        public volatile int OOP = 0;//1 = yes
        private string _OOPNames = "";
        private System.Collections.ArrayList _OOPNamesArray = new System.Collections.ArrayList();
        private System.Collections.ArrayList _OOPIDs = new System.Collections.ArrayList();
        public volatile int AcceptRez = 0;//1 = yes
        private string _AcceptRezNames = "";
        public volatile int HealRange = 550;//1 = yes
        public volatile int LootRange = 250;
        public volatile int MoveRange = 150;
        public volatile int Pickup = 0;//1 - yes
        public volatile int PickupAfterAttack = 0; //1 - yes
        public volatile int OnlyPickMine = 1; //1 - yes
        public volatile int Target = 0;//1 - yes
        public volatile int Cancel_Target = 0;//1 - yes
        public volatile int MoveToLoc = 0;//1 - yes
        public volatile int OutOfCombat = 0;//1 - yes
        public string Moveto_X = "0";
        public string Moveto_Y = "0";
        public string Moveto_Z = "0";
        public volatile int MoveToLeash = 100;
        public volatile int Attack = 0;//1 - yes
        public volatile int MoveFirst = 0;//1 - yes
        public volatile int ControlBuffing = 0;//1 - yes
        public volatile int ShiftBuffing = 0;//1 - yes
        public volatile int ProtectPriority = 0;//1 = yes
        public volatile int MoveFirstNormal = 0;//1 - yes
        public volatile int MoveBeforeTargeting = 0; //1 = yes
        public volatile int DeadLogout = 0; //1 = yes
        public volatile int DeadReturn = -1; //0 = town, 1 = clanhall, 2 = castle, 3 = siege HQ, 4 = fortress
        public volatile int DeadLogoutDelay = 30;
        public volatile int DeadReturnDelay = 10;
        public volatile int DeadToggleBotting = 0; // 1 = yes
        public volatile int SpoilMPAbove = 50;
        public volatile int PetAssist = 0; //1 = yes
        public volatile int SummonAssist = 0; //1 = yes
        public volatile int PetAttackSolo = 0; //1 = yes
        public volatile int SummonInstantAttack = 0; //1 = yes
        /* Advanced */
        public volatile int AntiKSDelay = 5;
        public volatile int AutoFollowDelay = 5;
        public volatile int BlacklistTries = 5;
        public volatile int PickupTimeout = 2;

        /* Rest Below */
        public volatile int RestBelowHP = 0; //1 = yes
        public volatile float RestBelowHealth = 0;
        public volatile int RestBelowMP = 0; //1 = yes
        public volatile float RestBelowMana = 0;

        /* Rest Until */
        //public volatile int RestUntilHP = 0; //1 = yes /* Not Needed */
        public volatile float RestUntilHealth = 0;
        //public volatile int RestUntilMP = 0; //1 = yes /* Not Needed */
        public volatile float RestUntilMana = 0;

        /* Follow Rest */
        public volatile int FollowRest = 0; //1 = yes
        private string _FollowRestName = "";
        public volatile uint FollowRestID = 0;

        public volatile int StuckCheck = 0; //1 = yes
        public volatile int AutoBlacklist = 0; //1 = yes

        //Pick & Attack Only
        public volatile int PickOnlyItemsInList = 0; // 1 = yes
        public volatile int AttackOnlyMobsInList = 0; //1 = yes

        //custom window title
        public volatile bool CustomWindowTitle = false;
        public volatile string CustomWindowTitle_Text = "";

        private readonly object FollowRestNameLock = new object();
        private readonly object ActiveFollowNameLock = new object();
        private readonly object AcceptPartyNamesLock = new object();
        private readonly object SendPartyNamesLock = new object();
        private readonly object OOPNamesLock = new object();
        private readonly object OOPNamesArrayLock = new object();
        private readonly object OOPIDsLock = new object();
        private readonly object AcceptRezNamesLock = new object();

		public static System.Collections.ArrayList BuffTargets = new System.Collections.ArrayList(Globals.BUFF_COUNT);
        public static System.Collections.ArrayList ItemTargets = new System.Collections.ArrayList(Globals.ITEM_COUNT);
        public static System.Collections.ArrayList CombatTargets = new System.Collections.ArrayList(Globals.COMBAT_COUNT);
        public static System.Collections.ArrayList DoNotItems = new System.Collections.ArrayList();
        public static System.Collections.ArrayList DoNotNPCs = new System.Collections.ArrayList();

        public static int Target_TYPE = 0;
        public static int Target_ATTACKABLE = 0;
        public static int Target_ALIVE = 0;
        public static int Target_INBOX = 0;
        public static int Target_COMBAT = 2;
        public static int Target_TARGETTED = 0;
        public static int Target_TARGETING = 0;
        public static int Target_IGNORE = 0;
        public static int Target_ZRANGE = 250;
        public static int Target_Pathfinding = 0;

		public BotOptions()
		{
            Globals.BuffListLock.EnterWriteLock();
			try
			{
                BotOptions.BuffTargets.Clear();
			}
			finally
			{
                Globals.BuffListLock.ExitWriteLock();
			}

            Globals.ItemListLock.EnterWriteLock();
			try
			{
                BotOptions.ItemTargets.Clear();
			}
			finally
			{
                Globals.ItemListLock.ExitWriteLock();
			}

            Globals.CombatListLock.EnterWriteLock();
            try
            {
                BotOptions.CombatTargets.Clear();
            }
            finally
            {
                Globals.CombatListLock.ExitWriteLock();
            }
        }

        public string AcceptPartyNames
        {
            get
            {
                string tmp;
                lock(AcceptPartyNamesLock)
                {
                    tmp = this._AcceptPartyNames;
                }
                return tmp;
            }
            set
            {
                lock(AcceptPartyNamesLock)
                {
                    _AcceptPartyNames = value.ToUpperInvariant();
                }
            }
        }
        public string SendPartyNames
        {
            get
            {
                string tmp;
                lock (SendPartyNamesLock)
                {
                    tmp = this._SendPartyNames;
                }
                return tmp;
            }
            set
            {
                lock (SendPartyNamesLock)
                {
                    _SendPartyNames = value;
                }
            }
        }
        public string OOPNames
        {
            get
            {
                string tmp;
                lock (OOPNamesLock)
                {
                    tmp = this._OOPNames;
                }
                return tmp;
            }
            set
            {
                lock (OOPNamesLock)
                {
                    _OOPNames = value.ToUpperInvariant();
                }
            }
        }
        public System.Collections.ArrayList OOPNamesArray
        {
            get
            {
                System.Collections.ArrayList tmp;
                lock (OOPNamesArrayLock)
                {
                    tmp = this._OOPNamesArray;
                }
                return tmp;
            }
            set
            {
                lock (OOPNamesArrayLock)
                {
                    _OOPNamesArray = value;
                }
            }
        }
        public System.Collections.ArrayList OOPIDs
        {
            get
            {
                System.Collections.ArrayList tmp;
                lock (OOPIDsLock)
                {
                    tmp = this._OOPIDs;
                }
                return tmp;
            }
            set
            {
                lock (OOPIDsLock)
                {
                    _OOPIDs = value;
                }
            }
        }
        public string AcceptRezNames
        {
            get
            {
                string tmp;
                lock(AcceptRezNamesLock)
                {
                    tmp = this._AcceptRezNames;
                }
                return tmp;
            }
            set
            {
                lock(AcceptRezNamesLock)
                {
                    _AcceptRezNames = value.ToUpperInvariant();
                }
            }
        }
		public string ActiveFollowName
		{
			get
			{
				string tmp;
				lock(ActiveFollowNameLock)
				{
					tmp = this._ActiveFollowName;
				}
				return tmp;
			}
			set
			{
				lock(ActiveFollowNameLock)
				{
					_ActiveFollowName = value;
				}
			}
		}
        public string FollowRestName
        {
            get
            {
                string tmp;
                lock (FollowRestNameLock)
                {
                    tmp = this._FollowRestName;
                }
                return tmp;
            }
            set
            {
                lock (FollowRestNameLock)
                {
                    _FollowRestName = value;
                }
            }
        }


//////////Other
		public void Set_ActiveFollow(string name)
		{
			ActiveFollowName = name;

			bool found = false;
            CharInfo player = null;

            Globals.PlayerLock.EnterReadLock();
			try
			{
                player = Util.GetChar(name);
            }
            finally
            {
                Globals.PlayerLock.ExitReadLock();
            }

            if (player != null)
            {
                found = true;
                ActiveFollowID = player.ID;
            }

			if(!found)
			{
				//ActiveFollow = 0;
				ActiveFollowID = 0;
			}
		}

        public void Set_FollowRest(string name)
        {
            FollowRestName = name;

            bool found = false;
            CharInfo player = null;

            Globals.PlayerLock.EnterReadLock();
            try
            {
                player = Util.GetChar(name);
            }
            finally
            {
                Globals.PlayerLock.ExitReadLock();
            }

            if (player != null)
            {
                found = true;
                FollowRestID = player.ID;
            }

            if (!found)
            {
                //FollowRest = 0;
                FollowRestID = 0;
            }
        }
	}//end of class
}
