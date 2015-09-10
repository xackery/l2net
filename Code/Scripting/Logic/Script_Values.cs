using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace L2_login
{
    public class ScriptLine
    {
        public string FullLine = "";
        
        public ScriptCommands Command = ScriptCommands.NULL;

        public bool Compilied = false;
        public string UnParsedParams = "";
        public ArrayList Params = new ArrayList();

        public int LinkedLine = -1;//for,next,do,loop,while,wend,endif
        public int LinkedLine2 = -1;//else
    }

    public enum ScriptCommands : int
    {
        ENCRYPTED = -100,
        MATH = -3,
        SET = -2,
        UNKNOWN = -1,
        NULL = 0,
        SORT = 1,
        DELETE_GLOBAL = 2,
        DELETE = 3,
        TEST_WEBSITE = 4,
        TEST_ODBC = 5,
        TEST_DNS = 6,
        TEST_PING = 7,
        SEND_EMAIL = 8,
        MESSAGE_BOX = 9,
        NMESSAGE_BOX = 10,
        NMESSAGE_BOX2 = 11,
        INCLUDE = 12,
        DEFAULT = 13,
        CASE = 14,
        SWITCH = 15,
        ENDSWITCH = 16,
        LOCK = 17,
        UNLOCK = 18,
        SET_EVENT = 19,
        PLAYWAV = 20,
        PLAYSOUND = 21,
        THREAD = 22,
        BREAK = 23,
        GET_TIME = 24,
        CALLSUB = 25,
        RETURNSUB = 26,
        SUB = 27,
        CALL_EXTERN = 28,
        CALL = 29,
        RETURN = 30,
        FUNCTION = 31,
        DISTANCE = 32,
        WHILE = 33,
        WEND = 34,
        DO = 35,
        LOOP = 36,
        FOREACH = 37,
        NEXTEACH = 38,
        FOR = 39,
        NEXT = 40,
        IF = 41,
        ELSE = 42,
        ENDIF = 43,
        DEFINE_GLOBAL = 44,
        DEFINE = 45,
        SLEEP = 46,
        PAUSE = 47,
        END_OF_FILE = 48,
        END_SCRIPT = 49,
        JUMP_TO_LINE = 50,
        LABEL = 51,
        JUMP_TO_LABEL = 52,
        PRINT_TEXT = 53,
        PRINT_DEBUG = 54,
        GET_RAND = 55,
        PLAYALARM = 56,
        DELETE_SHORTCUT = 57,
        REGISTER_SHORTCUT = 58,
        USE_SHORTCUT = 59,
        TARGET_SELF = 60,
        CANCEL_TARGET = 61,
        UDP_SEND = 62,
        UDP_SENDBB = 63,
        UDP_IP_SEND = 64,
        UDP_IP_SENDBB = 65,
        SLEEP_HUMAN_READING = 66,
        SLEEP_HUMAN_WRITING = 67,
        GET_ELIZA = 68,
        GET_NPCS = 69,
        GET_INVENTORY = 70,
        GET_ITEMS = 71,
        GET_PARTY = 72,
        GET_PLAYERS = 73,
        GTE_BUFFS = 74,
        FORCE_LOG = 75,
        IGNORE_ITEM = 76,
        CRYSTALIZE_ITEM = 77,
        DELETE_ITEM = 78,
        DROP_ITEM = 79,
        BOTSET = 80,
        INJECTBB = 81,
        INJECT = 82,
        INJECTBB_CLIENT = 83,
        INJECT_CLIENT = 84,
        COMMAND = 85,
        CLICK_NEAREST_ITEM = 86,
        CLEAR_WALLS = 87,
        ADD_WALL = 88,
        CLEAR_BORDER = 89,
        ADD_BORDER_PT = 90,
        NPC_DIALOG = 91,
        CHECK_TARGETING = 92,
        SET_TARGETING = 93,
        TARGET_NEAREST = 94,
        TARGET_NEAREST_NAME = 95,
        TARGET_NEAREST_ID = 96,
        TARGET = 97,
        TALK_TARGET = 98,
        ATTACK_TARGET = 99,
        USE_ACTION = 100,
        USE_SKILL = 101,
        USE_ITEM = 102,
        USE_ITEM_EXPLICIT = 103,
        ITEM_COUNT = 104,
        MOVE_TO = 105,
        MOVR_WAIT = 106,
        MOVE_SMART = 107,
        MOVE_INTERRUPT = 108,
        SAY_TEXT = 109,
        SAY_TO_CLIENT = 110,
        INVEN_GET_UID = 111,
        INVEN_GET_ITEMID = 112,
        CHAR_GET_NAME = 113,
        CHAR_GET_ID = 114,
        SKILL_GET_REUSE = 115,
        SKILL_GET_NAME = 116,
        SKILL_GET_DESC1 = 117,
        SKILL_GET_DESC2 = 118,
        SKILL_GET_DESC3 = 119,
        SKILL_GET_ID = 120,
        NPC_GET_NAME = 121,
        NPC_GET_ID = 122,
        ITEM_GET_NAME = 123,
        ITEM_GET_DESC = 124,
        ITEM_GET_ID = 125,
        CLAN_GET_NAME = 126,
        CLAN_GET_ID = 127,
        TAP_TO = 128,
        RESTART = 129,
        BLOCK = 130,
        BLOCKEX = 131,
        UNBLOCK = 132,
        UNBLOCKEX = 133,
        CLEAR_BLOCK = 134,
        CLEAR_BLOCKEX = 135,
        BLOCK_CLIENT = 136,
        BLOCKEX_CLIENT = 137,
        UNBLOCK_CLIENT = 138,
        UNBLOCKEX_CLIENT = 139,
        CLEAR_BLOCK_CLIENT = 140,
        CLEAR_BLOCKEX_CLIENT = 141,
        HEX_TO_DEC = 142,
        VAR_START = 143,
        VAR_END = 144,
        PUBLIC = 145,
        PRIVATE = 146,
        PROTECTED = 147,
        STATIC = 148,
        CLASS = 149,
        END_CLASS = 150,
        BLOCK_SELF = 151,
        BLOCKEX_SELF = 152,
        UNBLOCK_SELF = 153,
        UNBLOCKEX_SELF = 154,
        CLEAR_BLOCK_SELF = 155,
        CLEAR_BLOCKEX_SELF = 156,
        BLOCK_SELF_ALL = 157,
        BLOCKEX_SELF_ALL = 158,
        GET_ZONE = 159,
        GENERATE_POLY = 160,
        GET_EFFECTS = 161,
        USE_SKILL_SMART = 162,
        CANCEL_BUFF = 163,
        GET_SKILLS = 164,

        REBOOT = 1000,
        TWITCH_MOUSE = 1001,
        ENABLE_SCREENSAVER = 1002,
        BLOCK_SCREENSAVER = 1003,
        WAIT_FOR_START = 1004,
        UNLOCK_INPUT = 1005,
        LOCK_INPUT = 1006,
        COPY_FOLDER = 1007,
        GET_FILESIZE = 1008,
        DELETE_KEY = 1009,
        DELETE_KEYVALUE = 1010,
        DELETE_KEYTREE = 1011,
        GET_KEY = 1012,
        ADD_KEY = 1013,
        ADD_DWORD = 1014,
        ADD_KEYBINARY = 1015,
    }

    public partial class ScriptEngine
    {
        public static VariableList GlobalVariables = new VariableList();//_globals
        public static SortedList Files = new SortedList();//_files
        public static SortedList Threads = new SortedList();//_threads
        public static SortedList Locks = new SortedList();//_locks
        public static SortedList Classes = new SortedList();//_classes
        private static SortedList _blocked_serverpackets = new SortedList();
        private static SortedList _blocked_serverpacketsEX = new SortedList();
        private static SortedList _blocked_clientpackets = new SortedList();
        private static SortedList _blocked_clientpacketsEX = new SortedList();
        private static SortedList _blocked_Selfpackets = new SortedList();
        private static SortedList _blocked_SelfpacketsEX = new SortedList();

        private static readonly object _blocked_clientpacketsEXLock = new object();
        private static readonly object _blocked_clientpacketsLock = new object();
        private static readonly object _blocked_serverpacketsEXLock = new object();
        private static readonly object _blocked_serverpacketsLock = new object();
        private static readonly object _blocked_SelfpacketsEXLock = new object();
        private static readonly object _blocked_SelfpacketsLock = new object();
        /*private static readonly object _classesLock = new object();
        private static readonly object _locksLock = new object();
        private static readonly object _filesLock = new object();
        private static readonly object _threadsLock = new object();
        private static readonly object _globalsLock = new object();*/

        private static SortedList _events = new SortedList();
        private static SortedList _events_serverpackets = new SortedList();
        private static SortedList _events_serverpacketsEX = new SortedList();
        private static SortedList _events_clientpackets = new SortedList();
        private static SortedList _events_clientpacketsEX = new SortedList();
        private static SortedList _events_selfpackets = new SortedList();
        private static SortedList _events_selfpacketsEX = new SortedList();
        private static Queue _eventqueue = new Queue();
        private static readonly object events_clientpacketsLock = new object();
        private static readonly object events_clientpacketsEXLock = new object();
        private static readonly object events_serverpacketsLock = new object();
        private static readonly object events_serverpacketsEXLock = new object();
        private static readonly object events_selfpacketsLock = new object();
        private static readonly object events_selfpacketsEXLock = new object();
        private static readonly object eventsLock = new object();
        private static readonly object eventqueueLock = new object();

        private static int CurrentThread = 0;
        private volatile static int _thread_id = 0;
        private static bool BumpThread = false;

        public static bool is_Moving = false;
        private static int Moving_X = 0;
        private static int Moving_Y = 0;
        private static int Moving_Z = 0;
        private static ArrayList Moving_List = new ArrayList();

        //Used for MOVE_INTERRUPT command
        public static bool _moveSmartInterruptFlag = false;
        public static readonly object moveSmartInterruptFlagLock = new object();

        /*public static VariableList GlobalVariables
        {
            get
            {
                VariableList tmp;
                lock (_globalsLock)
                {
                    tmp = _globals;
                }
                return tmp;
            }
            set
            {
                lock (_globalsLock)
                {
                    _globals = value;
                }
            }
        }*/

        public static SortedList Blocked_ServerPacketsEX
        {
            get
            {
                SortedList tmp;
                lock (_blocked_serverpacketsEXLock)
                {
                    tmp = _blocked_serverpacketsEX;
                }
                return tmp;
            }
            set
            {
                lock (_blocked_serverpacketsEXLock)
                {
                    _blocked_serverpacketsEX = value;
                }
            }
        }

        public static SortedList Blocked_ServerPackets
        {
            get
            {
                SortedList tmp;
                lock (_blocked_serverpacketsLock)
                {
                    tmp = _blocked_serverpackets;
                }
                return tmp;
            }
            set
            {
                lock (_blocked_serverpacketsLock)
                {
                    _blocked_serverpackets = value;
                }
            }
        }

        public static SortedList Blocked_ClientPacketsEX
        {
            get
            {
                SortedList tmp;
                lock (_blocked_clientpacketsEXLock)
                {
                    tmp = _blocked_clientpacketsEX;
                }
                return tmp;
            }
            set
            {
                lock (_blocked_clientpacketsEXLock)
                {
                    _blocked_clientpacketsEX = value;
                }
            }
        }

        public static SortedList Blocked_ClientPackets
        {
            get
            {
                SortedList tmp;
                lock (_blocked_clientpacketsLock)
                {
                    tmp = _blocked_clientpackets;
                }
                return tmp;
            }
            set
            {
                lock (_blocked_clientpacketsLock)
                {
                    _blocked_clientpackets = value;
                }
            }
        }

        public static SortedList Blocked_SelfPackets
        {
            get
            {
                SortedList tmp;
                lock (_blocked_SelfpacketsLock)
                {
                    tmp = _blocked_Selfpackets;
                }
                return tmp;
            }
            set
            {
                lock (_blocked_SelfpacketsLock)
                {
                    _blocked_Selfpackets = value;
                }
            }
        }

        public static SortedList Blocked_SelfPacketsEX
        {
            get
            {
                SortedList tmp;
                lock (_blocked_SelfpacketsEXLock)
                {
                    tmp = _blocked_SelfpacketsEX;
                }
                return tmp;
            }
            set
            {
                lock (_blocked_SelfpacketsEXLock)
                {
                    _blocked_SelfpacketsEX = value;
                }
            }
        }

        /*public static SortedList Classes
        {
            get
            {
                SortedList tmp;
                lock (_classesLock)
                {
                    tmp = _classes;
                }
                return tmp;
            }
            set
            {
                lock (_classesLock)
                {
                    _classes = value;
                }
            }
        }

        public static SortedList Locks
        {
            get
            {
                SortedList tmp;
                lock (_locksLock)
                {
                    tmp = _locks;
                }
                return tmp;
            }
            set
            {
                lock (_locksLock)
                {
                    _locks = value;
                }
            }
        }

        public static SortedList Files
        {
            get
            {
                SortedList tmp;
                lock (_filesLock)
                {
                    tmp = _files;
                }
                return tmp;
            }
            set
            {
                lock (_filesLock)
                {
                    _files = value;
                }
            }
        }

        public static SortedList Threads
        {
            get
            {
                SortedList tmp;
                lock (_threadsLock)
                {
                    tmp = _threads;
                }
                return tmp;
            }
            set
            {
                lock (_threadsLock)
                {
                    _threads = value;
                }
            }
        }*/

        private static void ResetThreads()
        {
            CurrentThread = 0;

            _thread_id = _thread_id + 1;
        }

        private static int GetUniqueThreadID()
        {
            _thread_id = _thread_id + 1;
            return _thread_id;
        }

        private void ClearEvents()
        {
            lock (eventsLock)
            {
                _events.Clear();
            }
            lock (events_serverpacketsLock)
            {
                _events_serverpackets.Clear();
            }
            lock (events_serverpacketsEXLock)
            {
                _events_serverpacketsEX.Clear();
            }
            lock (events_serverpacketsLock)
            {
                _events_clientpackets.Clear();
            }
            lock (events_serverpacketsEXLock)
            {
                _events_clientpacketsEX.Clear();
            }
            lock (eventqueueLock)
            {
                _eventqueue.Clear();
            }
        }

        public static ScriptEventCaller EventsGetCaller(int key)
        {
            /*ScriptEventCaller sc_ev = null;
            lock (eventsLock)
            {
                sc_ev = (ScriptEventCaller)_events[key];
            }
            return sc_ev;*/
            return (ScriptEventCaller)_events[key];
        }

        public static ScriptEventCaller ServerPacketsGetCaller(int key)
        {
            /*ScriptEventCaller sc_ev = null;
            lock (events_serverpacketsLock)
            {
                sc_ev = (ScriptEventCaller)_events_serverpackets[key];
            }
            return sc_ev;*/
            return (ScriptEventCaller)_events_serverpackets[key];
        }

        public static ScriptEventCaller ServerPacketsEXGetCaller(int key)
        {
            /*ScriptEventCaller sc_ev = null;
            lock (events_serverpacketsEXLock)
            {
                sc_ev = (ScriptEventCaller)_events_serverpacketsEX[key];
            }
            return sc_ev;*/
            return (ScriptEventCaller)_events_serverpacketsEX[key];
        }

        public static ScriptEventCaller ClientPacketsGetCaller(int key)
        {
            /*ScriptEventCaller sc_ev = null;
            lock (events_clientpacketsLock)
            {
                sc_ev = (ScriptEventCaller)_events_clientpackets[key];
            }
            return sc_ev;*/
            return (ScriptEventCaller)_events_clientpackets[key];
        }

        public static ScriptEventCaller ClientPacketsEXGetCaller(int key)
        {
            /*ScriptEventCaller sc_ev = null;
            lock (events_clientpacketsEXLock)
            {
                sc_ev = (ScriptEventCaller)_events_clientpacketsEX[key];
            }
            return sc_ev;*/
            return (ScriptEventCaller)_events_clientpacketsEX[key];
        }

        public static ScriptEventCaller SelfPacketsGetCaller(int key)
        {
            /*ScriptEventCaller sc_ev = null;
            lock (events_selfpacketsLock)
            {
                sc_ev = (ScriptEventCaller)_events_selfpackets[key];
            }
            return sc_ev;*/
            return (ScriptEventCaller)_events_selfpackets[key];
        }

        public static ScriptEventCaller SelfPacketsEXGetCaller(int key)
        {
            /*ScriptEventCaller sc_ev = null;
            lock (events_selfpacketsEXLock)
            {
                sc_ev = (ScriptEventCaller)_events_selfpacketsEX[key];
            }
            return sc_ev;*/
            return (ScriptEventCaller)_events_selfpacketsEX[key];
        }

        public static bool EventsContainsKey(int key)
        {
            /*bool contains = false;
            lock (eventsLock)
            {
                contains = _events.ContainsKey(key);
            }
            return contains;*/
            return _events.ContainsKey(key);
        }

        public static bool ServerPacketsContainsKey(int key)
        {
            bool contains = false;
            lock (events_serverpacketsLock)
            {
                contains = _events_serverpackets.ContainsKey(key);
            }
            return contains;
        }

        public static bool ServerPacketsEXContainsKey(int key)
        {
            bool contains = false;
            lock (events_serverpacketsEXLock)
            {
                contains = _events_serverpacketsEX.ContainsKey(key);
            }
            return contains;
        }

        public static bool ClientPacketsContainsKey(int key)
        {
            bool contains = false;
            lock (events_serverpacketsLock)
            {
                contains = _events_clientpackets.ContainsKey(key);
            }
            return contains;
        }

        public static bool ClientPacketsEXContainsKey(int key)
        {
            bool contains = false;
            lock (events_serverpacketsEXLock)
            {
                contains = _events_clientpacketsEX.ContainsKey(key);
            }
            return contains;
        }
        public static bool SelfPacketsContainsKey(int key)
        {
            bool contains = false;
            lock (events_serverpacketsLock)
            {
                contains = _events_selfpackets.ContainsKey(key);
            }
            return contains;
        }

        public static bool SelfPacketsEXContainsKey(int key)
        {
            bool contains = false;
            lock (events_serverpacketsEXLock)
            {
                contains = _events_selfpacketsEX.ContainsKey(key);
            }
            return contains;
        }

        public static void EventsRemoveKey(int key)
        {
            lock (eventsLock)
            {
                _events.Remove(key);
            }
        }

        public static void ServerPacketsRemoveKey(int key)
        {
            lock (events_serverpacketsLock)
            {
                _events_serverpackets.Remove(key);
            }
        }

        public static void ServerPacketsEXRemoveKey(int key)
        {
            lock (events_serverpacketsEXLock)
            {
                _events_serverpacketsEX.Remove(key);
            }
        }

        public static void ClientPacketsRemoveKey(int key)
        {
            lock (events_clientpacketsLock)
            {
                _events_clientpackets.Remove(key);
            }
        }

        public static void ClientPacketsEXRemoveKey(int key)
        {
            lock (events_clientpacketsEXLock)
            {
                _events_clientpacketsEX.Remove(key);
            }
        }
        public static void SelfPacketsRemoveKey(int key)
        {
            lock (events_selfpacketsLock)
            {
                _events_selfpackets.Remove(key);
            }
        }

        public static void SelfPacketsEXRemoveKey(int key)
        {
            lock (events_selfpacketsEXLock)
            {
                _events_selfpacketsEX.Remove(key);
            }
        }

        public static void EventsAddKey(int key, ScriptEventCaller sc_ec)
        {
            lock (eventsLock)
            {
                _events.Add(key, sc_ec);
            }
        }

        public static void ServerPacketsAddKey(int key, ScriptEventCaller sc_ec)
        {
            lock (events_serverpacketsLock)
            {
                _events_serverpackets.Add(key, sc_ec);
            }
        }

        public static void ServerPacketsEXAddKey(int key, ScriptEventCaller sc_ec)
        {
            lock (events_serverpacketsEXLock)
            {
                _events_serverpacketsEX.Add(key, sc_ec);
            }
        }

        public static void ClientPacketsAddKey(int key, ScriptEventCaller sc_ec)
        {
            lock (events_clientpacketsLock)
            {
                _events_clientpackets.Add(key, sc_ec);
            }
        }

        public static void ClientPacketsEXAddKey(int key, ScriptEventCaller sc_ec)
        {
            lock (events_clientpacketsEXLock)
            {
                _events_clientpacketsEX.Add(key, sc_ec);
            }
        }

        public static void SelfPacketsAddKey(int key, ScriptEventCaller sc_ec)
        {
            lock (events_selfpacketsLock)
            {
                _events_selfpackets.Add(key, sc_ec);
            }
        }

        public static void SelfPacketsEXAddKey(int key, ScriptEventCaller sc_ec)
        {
            lock (events_selfpacketsEXLock)
            {
                _events_selfpacketsEX.Add(key, sc_ec);
            }
        }

        public static int EventQueueCount()
        {
            int tmp;
            lock (eventqueueLock)
            {
                tmp = _eventqueue.Count;
            }
            return tmp;
        }

        public static void SendToEventQueue(ScriptEvent sc_ev)
        {
            lock (eventqueueLock)
            {
                _eventqueue.Enqueue(sc_ev);
            }
        }

        public static ScriptEvent EventQueueDequeue()
        {
            ScriptEvent sc_ev = null;
            lock (eventqueueLock)
            {
                sc_ev = (ScriptEvent)_eventqueue.Dequeue();
            }
            return sc_ev;
        }

        public static SortedList Sublist
        {
            get
            {
                return ((ScriptFile)Files[((ScriptThread)Threads[CurrentThread]).Current_File])._sublist;
            }
            set
            {
                ((ScriptFile)Files[((ScriptThread)Threads[CurrentThread]).Current_File])._sublist = value;
            }
        }

        public static SortedList Functionlist
        {
            get
            {
                return ((ScriptFile)Files[((ScriptThread)Threads[CurrentThread]).Current_File])._functionlist;
            }
            set
            {
                ((ScriptFile)Files[((ScriptThread)Threads[CurrentThread]).Current_File])._functionlist = value;
            }
        }

        public static SortedList Labellist
        {
            get
            {
                return ((ScriptFile)Files[((ScriptThread)Threads[CurrentThread]).Current_File])._labellist;
            }
            set
            {
                ((ScriptFile)Files[((ScriptThread)Threads[CurrentThread]).Current_File])._labellist = value;
            }
        }

        public static ArrayList Stack
        {
            get
            {
                return ((ScriptThread)Threads[CurrentThread])._stack;
            }
            set
            {
                ((ScriptThread)Threads[CurrentThread])._stack = value;
            }
        }

        public static Stack Subcalls
        {
            get
            {
                return ((ScriptThread)Threads[CurrentThread])._subcalls;
            }
            set
            {
                ((ScriptThread)Threads[CurrentThread])._subcalls = value;
            }
        }

        public static Stack Functioncalls
        {
            get
            {
                return ((ScriptThread)Threads[CurrentThread])._functioncalls;
            }
            set
            {
                ((ScriptThread)Threads[CurrentThread])._functioncalls = value;
            }
        }

        public static int StackHeight
        {
            get
            {
                return ((ScriptThread)Threads[CurrentThread]).StackHeight;
            }
            set
            {
                ((ScriptThread)Threads[CurrentThread]).StackHeight = value;
            }
        }

        public static int Line_Pos
        {
            get
            {
                return ((ScriptThread)Threads[CurrentThread]).Line_Pos;
            }
            set
            {
                ((ScriptThread)Threads[CurrentThread]).Line_Pos = value;
            }
        }

        public static bool moveSmartInterruptFlag
        {
            get
            {
                return (_moveSmartInterruptFlag);
            }
            set
            {
                _moveSmartInterruptFlag = value;
            }
        }
    }//end of class
}
