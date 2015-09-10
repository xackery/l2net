using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Resources;
using System.Globalization;
using System.Diagnostics;

namespace L2_login
{

    public enum TargetType : byte
    {
        //0 - none | 1 - player | 2 - other players | 3 - npc | 4 - item
        ERROR = 255,
        NONE = 0,
        SELF = 1,
        PLAYER = 2,
        NPC = 3,
        ITEM = 4,
        MYPET = 5,
        MYPET1 = 6,
        MYPET2 = 7,
        MYPET3 = 8
    }

    public enum TextType : byte
    {
        LOCAL = 0,
        TRADE = 1,
        PARTY = 2,
        CLAN = 4,
        ALLY = 5,
        SYSTEM = 6,
        BOT = 7,
        ALL = 8,
        HERO = 9
    }

    public static class Globals
    {
        public static L2NET l2net_home;

        //1234567890123456
        public const string AES_Key = "#V^yw45?YLV$5wYa";
        public const string Code_Key = "$V%YWTaedcfwef0-";

        public const string Map_Key = "pK.+3!x8XzSW?@,B";
        public const string Map_Salt = "QaABcmPq$]@H+2u4NXxG";

        public const string Name = "L2.Net";
        public const string Version = "468";
        public const string VersionLetter = "";
        public const int MinDataPack = 392;
        public static string PATH = "";
        public const string SOUND_NAMESPACE = "L2_login.Sounds.";
        public const int LOAD_BUFFER = 6600000;

        public const int TimeOut = int.MaxValue;

        //IG/OOG : bot connection to login server
        public static System.Net.Sockets.Socket Login_GameSocket;
        //IG/OOG: bot conection to game server
        public static System.Net.Sockets.Socket Game_GameSocket;
        //IG: client connection to bot login server
        public static System.Net.Sockets.TcpListener Login_ClientLink;
        public static System.Net.Sockets.Socket Login_ClientSocket;
        //IG: client connection to bot game server
        public static System.Net.Sockets.TcpListener Game_ClientLink;
        public static System.Net.Sockets.Socket Game_ClientSocket;

        //GG Server
        public static System.Net.Sockets.TcpListener GG_Clientlink;
        public static System.Net.Sockets.TcpClient GG_TcpClient;
        public static System.Net.Sockets.NetworkStream GG_Clientstream;

        public volatile static bool clientsocket_ready = false;
        public volatile static bool gamesocket_ready = false;
        public volatile static bool clientport_ready = false;

        //stat stuff
        public static DateTime start_time;

        public volatile static bool dc_on_ig_close = false;
        public volatile static bool dump_pbuff_on_ig_close = false;

        // Experimental Content Firewall
        public volatile static bool lagfilter_TargetSelected = false;
        public volatile static bool lagfilter_TargetUnselected = false;
        public volatile static bool lagfilter_Skills = false;
        public volatile static bool lagfilter_ExBrExtraUserInfo = false;

        // Experimental Content Transform
        public volatile static bool lagfilter_xf_ci_striptitle = false;
        public volatile static bool lagfilter_xf_ci_stripenchant = false;
        public volatile static bool lagfilter_xf_ci_stripaug = false;
        public volatile static bool lagfilter_xf_ci_stripunseen = false;
        public volatile static bool lagfilter_xf_ci_striprecs = false;
        public volatile static bool lagfilter_xf_ci_simple_gender = false;
        public volatile static bool lagfilter_xf_ci_simple_apperance = false;
        public volatile static bool lagfilter_xf_ci_simple_race = false;

        public volatile static uint[] lagfilter_ignoreskills = new uint[] 
        {
            2008,  // Item - Beast Spirit Shot
            2009,  // Item - Beast Blessed Spirit Shot
            2031,  // Lesser Healing Potion
            2032,  // Healing Potion
            2033,  // Item - Beast Soul Shot
            2037,  // Greater Healing Potion
            2038,  // Quick Healing Potion
            2039,  // Soulshot: No Grade
            2047,  // Spiritshot: No Grade
            2061,  // Blessed Spiritshot: No Grade
            2136,  // Rice Cake
            2150,  // Soulshot: D-Grade
            2151,  // Soulshot: C-Grade
            2152,  // Soulshot: B-Grade
            2153,  // Soulshot: A-Grade
            2154,  // Soulshot: S-Grade
            2155,  // Spiritshot: D-Grade
            2156,  // Spiritshot: C-Grade
            2157,  // Spiritshot: B-Grade
            2158,  // Spiritshot: A-Grade
            2159,  // Spiritshot: S-Grade
            2160,  // Blessed Spiritshot: D-Grade
            2161,  // Blessed Spiritshot: C-Grade
            2162,  // Blessed Spiritshot: B-Grade
            2163,  // Blessed Spiritshot: A-Grade
            2164,  // Blessed Spiritshot: S-Grade
            2165,  // Energy Stone
            2287,  // Elixir of Life (Grade A)
            2287,  // Elixir of Life (Grade B)
            2287,  // Elixir of Life (Grade C)
            2287,  // Elixir of Life (Grade D)
            2287,  // Elixir of Life (Grade S)
            2287,  // Elixir of Life (No Grade)
            2288,  // Elixir of Mental Strength (Grade A)
            2288,  // Elixir of Mental Strength (Grade B)
            2288,  // Elixir of Mental Strength (Grade C)
            2288,  // Elixir of Mental Strength (Grade D)
            2288,  // Elixir of Mental Strength (Grade S)
            2288,  // Elixir of Mental Strength (No Grade)
            2289,  // Elixir of CP (Grade A)
            2289,  // Elixir of CP (Grade B)
            2289,  // Elixir of CP (Grade C)
            2289,  // Elixir of CP (Grade D)
            2289,  // Elixir of CP (Grade S)
            2289,  // Elixir of CP (No Grade)
            2395,  // Lesser Battlefield Healing Potion
            2398,  // Instant Haste Potion
            2401,  // Lesser Battlefield Mana Potion
            2402,  // Lesser Battlefield CP Potion
            2403,  // Spring CP Potion
            2530,  // Recovery Item for Battlefield Use
            2592,  // Quick Healing Potion
            2907,  // Battlefield Use Secret Medicine of Life
            2908,  // Battlefield Use Secret Medicine of Mind
            2909,  // Battlefield Use Secret Medicine of Will
            22036, // Beast Soulshot
            22037, // Beast Spiritshot
            22038, // Blessed Beast Spiritshot
            26024, // Temporary Healing Potion
            26025, // Powerful Healing Potion
            26026, // High-grade Healing Potion
            26050, // Blessed Spiritshot - D grade
            26051, // Blessed Spiritshot - C grade
            26052, // Blessed Spiritshot - B grade
            26053, // Blessed Spiritshot - A grade
            26054, // Blessed Spiritshot - S grade
            26055, // Spiritshot - D grade
            26056, // Spiritshot - C grade
            26057, // Spiritshot - B grade
            26058, // Spiritshot - A grade
            26059, // Spiritshot - S grade
            26060, // Soulshot - D grade
            26061, // Soulshot - C grade
            26062, // Soulshot - B grade
            26063, // Soulshot - A grade
            26064, // Soulshot - S grade
        };

        public static System.Threading.Thread gameprocessdatathread;

        public static System.Threading.Thread oog_loginthread;

        public static System.Threading.Thread ig_loginthread;
        public static System.Threading.Thread ig_listener;
        public static System.Threading.Thread loginsendthread;
        public static System.Threading.Thread loginreadthread;
        public static System.Threading.Thread ig_Gamelistener;
        //public static System.Threading.Thread GG_sendthread;

        public static System.Threading.Thread gamedrawthread;
        public static System.Threading.Thread directinputthread;

        public static SortedList Login_Servers;
        public static uint Login_SelectedServer = 0;
        public static int Login_State = 0;

        public static string UserName;
        public static string Password;
        public static string SecurityPin = "";

        public static bool FulllyIn = false;
        public static bool OOG = false;
        public static bool show_active_skills = true;

        //anti-assist/skills when picking up
        public static bool picking_up_items = false;

        public static bool GG_Servermode = false;
        public static bool GG_Clientmode = false;
        public static bool GG_QueryReceived = false;

        public static MixedPackets Mixer;

        public static ServerThread gamethread;
        public static ClientThread clientthread;
        public static BroadcastThread broadcastthread;
        public static BotAIThread botaithread;
        public static FollowRestThread followrestthread;
        public static ScriptEngine scriptthread;

        public static DX_Keyboard Keyboard;

        public static System.IO.StreamWriter text_out;

#if DEBUG
        public static System.IO.StreamWriter gamedataout;
        public static System.IO.StreamWriter gamedatato;
        public static System.IO.StreamWriter clientdataout;
        public static System.IO.StreamWriter clientdatato;
#endif

        public static Login login_window;
        public static Map map_window;
        public static BotOptionsScreen botoptionsscreen;
        public static Overlay overlaywindow;
        public static ShortCutBar shortcutwindow;
        public static Setup setupwindow;
        public static ScriptDebugger scriptdebugwindow;
        public static ActionWindow actionwindow;
        public static TradeWindow tradewindow;
        public static PrivateStoreSellWindow privatestoresellwindow;
        public static TargetInfoScreen targetinfoscreen;
        public static PetWindow petwindow;
        public static PetWindowGive petwindowgive;
        public static PetWindowTake petwindowtake;
        public static Captcha captchawindow;
        public static GameGuardServer ggwindow;
        public static GameGuardClient ggclientwindow;

        public static ExtendedActionWindow extendedactionwindow;
        public static MailboxWindow mailboxwindow; 
        
        public static bool pre_agree = false;
        public static string pre_blowfish = "6B60CB5B82CE90B1CC2B6C556C6C6C6C";
        public static string pre_protocol = "532";
        public static string pre_login_port = "2106";
        public static string pre_login_ip = "64.25.35.104";
        public static string pre_username = "";
        public static string pre_password = "";
        public static string pre_IG_listen_port = "2106";
        public static string pre_IG_listen_ip = "127.0.0.1";
        public static int pre_chron = 18;
        public static bool pre_chron_cmd = false;

        //added to support advanced command options
        public static string pre_gameserver_override_ip = "";
        public static string pre_gameserver_override_port = "";
        public static string pre_socks5_ip = "";
        public static string pre_socks5_port = "";
        public static string pre_socks5_username = "";
        public static string pre_socks5_password = "";
        public static bool pre_useGameServerOveride = false;
        public static bool pre_useProxyServerForLogin = false;
        public static bool pre_useProxyServerForGameserver = false;
        public static bool pre_checkAdvancedSettings = false;
        public static bool pre_EnterWorldCheckbox = false;
        public static bool pre_IG = false;
        public static bool pre_OOG = false;
        public static bool pre_GGSrv = false;
        public static bool pre_GGClient = false;

        public static bool enterworld_check = false;

        //used to know if we are in the gameserver yet
        public static bool enterworld_sent = false;

        public static string enterworld_custom = "";
        //adifenix(obce)
        public static bool pre_unknow_blowfish = false;
        public static bool pre_proxy_serv = false;
        public static string pre_game_srv_listen_prt = "";

        public static bool unknow_blowfish = false;
        public static string game_srv_listen_prt = "";

        public static bool pre_enterworld_ip = false;
        public static string [] pre_enterworld_ip_tab = new string[20];
        public static bool enterworld_ip = false;
        public static bool proxy_serv = false; // work as proxy server -blowfishless method
        public static byte[] proxy_serv_ip = new byte[4]; // ip from proxy serv
        public static byte[] proxy_serv_port = new byte[2]; // port from proxy server
        public static ArrayList ew_con_array= new ArrayList();
        public static ArrayList ew_chc_ed_array = new ArrayList();
        // packet window stuff ...------------------------------------------------
        public static pck_window_thr pck_thread= new pck_window_thr();
        //--------------------------------------------------------------------------
        public static bool Send_Blank_GG = false;
        public static bool Hide_Message_Boxes = false;

        public static string DirectInputKey = "-none-";
        public static bool DirectInputLast = false;
        public static bool DirectInputSetup = false;
        public static string DirectInputSetupValue = "";

        public static string DirectInputKey2 = "-none-";
        public static bool DirectInputLast2 = false;
        public static bool DirectInputSetup2 = false;
        public static string DirectInputSetupValue2 = "";

        public static bool DownloadNewCrests = false;
        public static bool SocialNpcs = false;
        public static bool NpcSay = false;
        public static bool SocialSelf = false;
        public static bool SocialPcs = false;
        public static bool ShowNamesNpcs = true;
        public static bool ShowNamesPcs = true;
        public static bool ShowNamesItems = true;
        public static bool IgnoreExitConf = false;
        public static bool ToggleBottingifGMAction = false;
        public static bool ToggleBottingifTeleported = false;

        public static string Script_MainFile = "";
        public static bool Script_Debugging = false;
        public const long Script_Ticks_Per_Switch = System.TimeSpan.TicksPerMillisecond * 1;
        public static string BotOptionsFile = "";

        public const int SLEEP_ClientSendThread = 75;
        public const int SLEEP_GameSendThread = 75;

        public const int SLEEP_Script_Reset = 500;
        public const int SLEEP_Script_Reset2 = 50;
        public const int SLEEP_WhileScript = 1;//sleep while paused... TODO: for infinite loops as well

        public const int SLEEP_ProcessDataThread = 10;//50
        public const long SLEEP_Animate = System.TimeSpan.TicksPerMillisecond * 120;
        public const long SLEEP_Sound_Alerts = System.TimeSpan.TicksPerMillisecond * 500;

        public const int SLEEP_BotAI = 100;
        //Oddi: Sleep_followrest..
        public const int SLEEP_FollowRestThread = 500;
        public const int SLEEP_DrawGameThread = 100;

        public const int SLEEP_WaitIGConnection = 1;
        public const int SLEEP_Load = 25;
        public const int SLEEP_LoginDelay = 250;

        public const int SLEEP_DirectInputDelay = 100;
        public const int SLEEP_SoundEngine = 100;

        public const int SLEEP_BotAIDelay = 500;
        public const int SLEEP_BotAIDelay_Target = 2000;
        public const int SLEEP_BotAIDelay_TargetInc = 25;
        public const int SLEEP_BotAIDelay_TargetSame = 250;
        //public const int SLEEP_BotAIDelay_Pickup = 1500;
        public const int SLEEP_BotAIDelay_PickupInc = 250;
        public const int PICKUP_Z_Diff = 50;

        public const int SLEEP_KillReset = 250;

        public const string SCRIPT_OUT_VAR = "@@@OUT";

        public const long MAX_SOUND_DELAY = System.TimeSpan.TicksPerSecond * 3;

        public static bool Use_Direct_Sound = true;
        public static bool Use_Hardware_Acceleration = true;
        public static int Texture_Mode = 1;
        public static int ViewRange = 0;
        public static bool White_Names = false;
        public static bool Suppress_Quakes = false;
        public static bool Popup_Captcha = false;
        public static bool AutolearnSkills = false;
        public static bool LogWriting = true;
        

        public static string Captcha_HTML1 = "captcha ";
        public static string Captcha_HTML2 = "";

        public const float Difficulty_Balance = 12;
        public const float Average_Word_Length = 5.10F;

        public static System.Random Rando = new Random(System.DateTime.Now.Millisecond);

        public const int THREAD_WAIT_DX = 75;//wait to read from players/npcs/items to draw on the map //don't want to wait too long... the gui thread will get locked
        public const int THREAD_WAIT_GUI = 200;//interaction with the gui //don't want to wait too long... the gui thread will get locked

        public const int MAX_LINES = 200;//max lines for the text box
        public const int MAX_LINES_PASS = 250;//max printed lines per pass
        public const int MAX_LINES_BYPASS = 500;//max printed lines per pass

        public const int MESSAGE_RESIZE_TIMER = 150;

        public const int MAX_MESSAGE_LEN = 128;

        public const int COUNT_SERVERNAME = 127;
        public const int COUNT_SYSTEMMSG = 3120;
        public const int COUNT_HENNAGROUP = 190;
        public const int COUNT_NPCNAME = 10090;
        public const int COUNT_ITEMNAME = 18100;
        public const int COUNT_CLASSES = 110;
        public const int COUNT_RACES = 6;
        public const int COUNT_SKILLS = 7460;
        public const int COUNT_ACTIONS = 190;
        public const int COUNT_QUESTS = 2690;
        public const int COUNT_ZONES = 500;
        public const int COUNT_NPCSTRING = 5460;

        public const float UNITS = 32768.0F;
        public const float ModX = Globals.UNITS * 20;
        public const float ModY = Globals.UNITS * 18;
        public const int ZRANGE_DIFF = 100;
        public const long SLEEP_TEXTURE = 1500 * System.TimeSpan.TicksPerMillisecond;
        public const long MAP_HOLD_STREAM = 5 * System.TimeSpan.TicksPerMinute;
        public const long MAP_HOLD_TEXTURE = 60 * System.TimeSpan.TicksPerMinute;

        public const int MIN_RADIUS = 6;
        public const float THRESHOLD = 5.0F;
        public const float THRESHOLD_L2NET = 50.0F;
        public const float INV_THOUSAND = 1.0F / 1000.0F;



        public const int BUFF_COUNT = 40;
        public const int ITEM_COUNT = 20;
        public const int COMBAT_COUNT = 10;

        public const int Skills_PerPage = 12;
        public const int Skills_Pages = 12;//normally 10

        public const uint Skill_SWEEP = 42;
        public const uint Skill_SPOIL = 254;
        public const uint Skill_SPOILCRUSH = 384;
        public const uint Skill_PLUNDER = 10548;

        public const uint ID_OFFSET = 1000;//multiply id by this and add quest prog to get the index in the array
        public const uint NPC_OFF = 1000000;

        public const int BUFFER_MAX = 131072;
        public const int BUFFER_PACKET = 65535;
        public const int BUFFER_NETWORK = 131072;

        public const int UDP_Port = 33801;

        public const string UnknownItem = "-unknown item-";
        public const string UnknownItemDesc = "-unknown item description-";
        public const string UnknownItemImagePath = "-no item image-";
        public const string UnknownNPC = "-unknown npc-";
        public const string UnknownTitle = "-unknown title-";
        public const string UnknownRace = "-unknown race-";
        public const string UnknownClass = "-unknown class-";
        public const string UnknownServer = "-unknown server-";

        public const long FAILED_BUFF = System.TimeSpan.TicksPerSecond * 4;
        public const double SKILL_MIN_REUSE = 100;
        public const double SKILL_INIT_REUSE = 2000;

        //real npc removal seems to be at 2048... or at least that is when we stop getting delete item packets for npcs
        public const int REMOVE_RANGE = 4000;
        public const int REMOVE_RANGE_INNER = 2048;
        //3584 loses shit...
        //3840 loses shit...
        //4096 is just barely too far
        public const long NPC_RemoveAtActive = System.TimeSpan.TicksPerDay * 60;//System.TimeSpan.TicksPerSecond * 300;//5 min
        public const long NPC_RemoveAtInvin = System.TimeSpan.TicksPerDay * 60;//2 months
        public const long NPC_RemoveAtDead = System.TimeSpan.TicksPerSecond * 15;//15 seconds
        public const long CLEAN_TIMER = System.TimeSpan.TicksPerSecond * 45;//45 seconds
        public const int CHAT_TIMER = 250;
        public const int PLAYERS_TIMER = 1500;
        public const int ITEMS_TIMER = 1500;
        public const int NPCS_TIMER = 1500;
        public const int INVENTORY_TIMER = 1000;
        public const int MYBUFFS_TIMER = 1500;

        public static string NumberGroupSeparator = System.Globalization.CultureInfo.InvariantCulture.NumberFormat.NumberGroupSeparator;
        public static char NumberDecimalSeparator = System.Globalization.CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator[0];

        public static System.Drawing.Brush Red = System.Drawing.Brushes.Red;
        public static System.Drawing.Brush Salmon = System.Drawing.Brushes.Salmon;
        public static System.Drawing.Brush White = System.Drawing.Brushes.White;
        public static System.Drawing.Brush Gray = System.Drawing.Brushes.Gray;
        public static System.Drawing.Brush Yellow = System.Drawing.Brushes.Yellow;
        public static System.Drawing.Brush Blue = System.Drawing.Brushes.Blue;
        public static System.Drawing.Brush Cyan = System.Drawing.Brushes.Cyan;
        public static System.Drawing.Brush Pink = System.Drawing.Brushes.Pink;
        public static System.Drawing.Brush Orange = System.Drawing.Brushes.OrangeRed;
        public static System.Drawing.Brush Gold = System.Drawing.Brushes.Gold;
        public static System.Drawing.Brush Green = System.Drawing.Brushes.Lime;
        public static System.Drawing.Brush LightYellow = System.Drawing.Brushes.LightGoldenrodYellow;

        public static System.Drawing.SolidBrush Tell_Brush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(((System.Byte)(253)), ((System.Byte)(0)), ((System.Byte)(253))));
        public static System.Drawing.SolidBrush Party_Brush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(251)), ((System.Byte)(0))));
        public static System.Drawing.SolidBrush Clan_Brush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(((System.Byte)(125)), ((System.Byte)(117)), ((System.Byte)(253))));
        public static System.Drawing.SolidBrush Trade_Brush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(((System.Byte)(234)), ((System.Byte)(162)), ((System.Byte)(245))));
        public static System.Drawing.SolidBrush Ally_Brush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(((System.Byte)(119)), ((System.Byte)(251)), ((System.Byte)(153))));
        public static System.Drawing.SolidBrush Announcement_Brush = new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(((System.Byte)(127)), ((System.Byte)(249)), ((System.Byte)(253))));

        //option stuff
        private static string _ProductKey = "LOVELKQKMGBOGNET";
        public static bool LoadedInterface = false;

        public static int Voice = 0;
        public static bool MinimizeToTray = false;
        public static bool DumpModeClient = false;
        public static bool DumpModeServer = false;
        public static bool AllowFiles = true;
        public static bool ScriptCompatibilityv386 = false;
        public static string L2Path = "";
        public static System.Windows.Forms.MainMenu back_menu;
        //end of option stuff

        public static ArrayList Bannedkeys = new ArrayList();

        public static GameData gamedata;

        public static SortedList servername;
        public static SortedList systemmsg;
        public static SortedList hennagrp;
        public static SortedList npcname;
        public static SortedList itemname;
        public static SortedList classes;
        public static SortedList races;
        public static SortedList skilllist;
        public static SortedList actionlist;
        public static SortedList questlist;
        public static SortedList zonelist;
        public static SortedList levelexp;
        public static SortedList npcstring;

        public static byte[] Login_GG_Reply;
        public static SortedList GG_List;

        public static ArrayList requested_clancrests = new ArrayList();
        public static ArrayList crestids = new ArrayList();
        public static SortedList clanlist = new SortedList();

        public static ResourceManager m_ResourceManager = new ResourceManager("L2_login.Languages.English", System.Reflection.Assembly.GetExecutingAssembly());
        public static int LanuageSet = 0;

        //public static uint LootType = 1;//1 - random
        public static uint ClanOnline = 0;
        public static uint ClanMembers = 0;
        public static uint LastRezz1 = 0;
        public static uint LastRezz2 = 0;

        public static bool CanPrint = false;
        public static bool Got_Skills = false;

        public static System.Threading.ReaderWriterLockSlim NPCLock = new System.Threading.ReaderWriterLockSlim();
        public static System.Threading.ReaderWriterLockSlim InventoryLock = new System.Threading.ReaderWriterLockSlim();
        public static System.Threading.ReaderWriterLockSlim PetInventoryLock = new System.Threading.ReaderWriterLockSlim();
        public static System.Threading.ReaderWriterLockSlim ItemLock = new System.Threading.ReaderWriterLockSlim();
        public static System.Threading.ReaderWriterLockSlim PlayerLock = new System.Threading.ReaderWriterLockSlim();
        public static System.Threading.ReaderWriterLockSlim BuyListLock = new System.Threading.ReaderWriterLockSlim();
        public static System.Threading.ReaderWriterLockSlim MyBuffsListLock = new System.Threading.ReaderWriterLockSlim();

        public static System.Threading.ReaderWriterLockSlim PartyLock = new System.Threading.ReaderWriterLockSlim();
        public static System.Threading.ReaderWriterLockSlim BuffsGivenLock = new System.Threading.ReaderWriterLockSlim();
        public static System.Threading.ReaderWriterLockSlim ClanListLock = new System.Threading.ReaderWriterLockSlim();
        public static System.Threading.ReaderWriterLockSlim SkillListLock = new System.Threading.ReaderWriterLockSlim();
        public static System.Threading.ReaderWriterLockSlim BuffListLock = new System.Threading.ReaderWriterLockSlim();
        public static System.Threading.ReaderWriterLockSlim ItemListLock = new System.Threading.ReaderWriterLockSlim();
        public static System.Threading.ReaderWriterLockSlim CombatListLock = new System.Threading.ReaderWriterLockSlim();
        public static System.Threading.ReaderWriterLockSlim DoNotItemLock = new System.Threading.ReaderWriterLockSlim();
        public static System.Threading.ReaderWriterLockSlim DoNotNPCLock = new System.Threading.ReaderWriterLockSlim();
        public static System.Threading.ReaderWriterLockSlim RestBelowLock = new System.Threading.ReaderWriterLockSlim();

        public static System.Threading.ReaderWriterLockSlim MobListLock = new System.Threading.ReaderWriterLockSlim();

        public static readonly object ItemImagesLock = new object();
        public static readonly object SkillImagesLock = new object();

        public static System.Threading.ReaderWriterLockSlim GameSendQueueLock = new System.Threading.ReaderWriterLockSlim();
        public static System.Threading.ReaderWriterLockSlim ClientSendQueueLock = new System.Threading.ReaderWriterLockSlim();
        public static System.Threading.ReaderWriterLockSlim GameReadQueueLock = new System.Threading.ReaderWriterLockSlim();

        public static System.Threading.ReaderWriterLockSlim ChatLock = new System.Threading.ReaderWriterLockSlim();

        //only for debugging
        public static AstarNode debugNode;
        public static AstarNode debugNode2;
        public static AstarNode debugNode3;

        public static System.Collections.ArrayList debugPath;

        public static string ProductKey
        {
            get
            {
                return _ProductKey;
            }
            set
            {
                _ProductKey = value.Replace("-","").ToUpperInvariant();
            }
        }
    }
}
