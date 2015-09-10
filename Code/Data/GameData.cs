using System;

namespace L2_login
{
    public enum DeviceState : int
    {
        Lost = 0,
        Ready = 1,
        Broke = 2,
        Fixed = 3,
    }

    public struct Vertex
    {
        public SlimDX.Vector4 Position;
        public int Color;
        public float Tu;
        public float Tv;
    }
    
    public class DrawData
	{
		public uint ID;
		public int X;
		public int Y;
		public string Text;
		public int Color1;//0 - black, 1 - yellow, 2 - blue, 3 - green
		public int Color2;//0 - red, 1 - purple, 2 - lpurple, 3 - black
        public float Radius;
	}

	public class Server
	{
		public uint ID = 0;
		public string IP = "0.0.0.0";
		public int Port = 0;
	}

    public enum Chronicle : byte
    {
        Prelude = 0,
        Chronicle_1 = 1,
        Chronicle_2 = 2,
        Chronicle_3 = 3,
        Chronicle_4 = 4,
        Chronicle_5 = 5,
        Interlude = 6,
        CT1 = 7,
        CT1_5 = 8,
        CT2_1 = 9,
        CT2_2 = 10,
        CT2_3 = 11,
        CT2_4 = 12,
        CT2_5 = 13,
        CT2_6 = 14,
        CT3_0 = 15,
        CT3_1 = 16,
        CT3_2 = 17,
        CT4_0 = 18
    }

    public enum AbnormalEffects : uint
    {
        //BLEEDING = 0x000001,
        //POISON = 0x000002,
        //REDCIRCLE = 0x000004,
        //ICE = 0x000008,
        //WIND = 0x000010,
        //FEAR = 0x000020,
        //STUN = 0x000040,
        //SLEEP = 0x000080,
        //MUTED = 0x000100,
        //ROOT = 0x000200,
        //HOLD_1 = 0x000400,  // paralysis
        //HOLD_2 = 0x000800,  // Petrified
        //UNKNOWN_13 = 0x001000,
        //BIG_HEAD = 0x002000,
        //FLAME = 0x004000,
        //UNKNOWN_16 = 0x008000,
        //GROW = 0x010000,
        //FLOATING_ROOT = 0x020000,  // Frintezza Stun
        //DANCE_STUNNED = 0x040000,  // Frintezza Stun
        //FIREROOT_STUN = 0x080000,
        //STEALTH = 0x100000,
        //IMPRISIONING_1 = 0x200000,
        //IMPRISIONING_2 = 0x400000,
        //MAGIC_CIRCLE = 0x800000,  // SoE
        //ICE2 = 0x1000000,
        //EARTHQUAKE = 0x2000000,
        //UNKNOWN_27 = 0x4000000,
        //INVULNERABLE = 0x8000000,
        //VITALITY = 0x10000000,
        //REAL_TARGET = 0x20000000,
        //DEATH_MARK = 0x40000000,
        //SKULL_FEAR = 0x80000000,
        //ARCANE_SHIELD = 0x008000,
        //CONFUSED = 0x0020,


        /*These are wrong */
        ICE = 0x98,
        WIND = 0x97,
        MUTED = 0x96,
        HOLD_1 = 0x95,
        INVULNERABLE = 0x94,
        FLOATING_ROOT = 0x93,
        DANCE_STUNNED = 0x92,
        FIREROOT_STUN = 0x91,
        SKULL_FEAR = 0x90,
        STEALTH = 0x8F,
        REDCIRCLE = 0x8E,
        FLAME = 0x8D,
        IMPRISIONING_1 = 0x8C,
        IMPRISIONING_2 = 0x8B,
        MAGIC_CIRCLE = 0x8A,
        ICE2 = 0x89,
        EARTHQUAKE = 0x88,
        VITALITY = 0x87,
        REAL_TARGET = 0x86,
        DEATH_MARK = 0x85,
        CONFUSED = 0x84,
        /*End bad section */

        TOGGLE_BUFF = 0x00,
        BLEEDING = 0x01,
        POISON = 0x02,
        STUN = 0x07,
        SLEEP = 0x08,
        SILENCE = 0x09,
        ROOT = 0x0A,
        PETRIFIED = 0x0C,
        VITALITY_HERB = 0x1D,
        FEAR = 0x20,
        XP_BUFF = 0x28, //mentees appreciation etc
        POLYMORPHED = 0x32,
        HERB_OF_POWER = 0x45,
        HERB_OF_MAGIC = 0x46,
    }

    public enum ExtendedEffects : uint
    {
        INVINCIBLE     = 0x000001,
	    AIR_STUN       = 0x000002,
	    AIR_ROOT       = 0x000004,
	    BAGUETTE_SWORD = 0x000008,
	    YELLOW_AFFRO   = 0x000010,
	    PINK_AFFRO     = 0x000020,
	    BLACK_AFFRO    = 0x000040,
	    UNKNOWN8       = 0x000080,
	    STIGMA_SHILIEN = 0x000100,
	    STAKATOROOT    = 0x000200,
	    FREEZING       = 0x000400,
	    VESPER         = 0x000800,
    }

    public enum MyRelation : uint
    {
        LEADER_RIGHTS   = 0x40,
        DEFENDER        = 0x80,
        ATTACKER        = 0x180,        
        CROWN           = 0xC0,
        FLAG            = 0x1C0,
    }

    public enum RelationStates : uint
    {
		PARTY1			= 0x00001, // party member
		PARTY2			= 0x00002, // party member
		PARTY3			= 0x00004, // party member
		PARTY4			= 0x00008, // party member
		PARTYLEADER		= 0x00010, // true if is party leader
		HAS_PARTY		= 0x00020, // true if is in party
		CLAN_MEMBER		= 0x00040, // true if is in clan
		LEADER			= 0x00080, // true if is clan leader
		CLAN_MATE		= 0x00100, // true if is in same clan
		INSIEGE			= 0x00200, // true if in siege
		ATTACKER		= 0x00400, // true when attacker
		ALLY			= 0x00800, // blue siege icon, cannot have if red
		ENEMY			= 0x01000, // true when red icon, doesn't matter with blue
		MUTUAL_WAR		= 0x04000, // double fist
		ONESIDED_WAR	= 0x08000, // single fist
		ALLY_MEMBER		= 0x10000, // clan is in alliance
		TERRITORY_WAR	= 0x80000, // show Territory War icon		
    }

	public class GameData
	{
		private System.Collections.Queue _gamesendqueue = new System.Collections.Queue();
		private System.Collections.Queue _gamereadqueue = new System.Collections.Queue();
		private System.Collections.Queue _clientsendqueue = new System.Collections.Queue();
		private System.Collections.Queue _clientreadqueue = new System.Collections.Queue();

        private System.Collections.Queue _LocalChatQueue = new System.Collections.Queue();
        private System.Collections.Queue _PrivateMsgQueue = new System.Collections.Queue();

        //stats tabpage stuff
        public static ulong meshless_ignored = 0;
        public static ulong badmobs_ignored = 0;
        public static ulong initial_Adena = 0;
        public static ulong current_Adena = 0;
        public static ulong initial_XP = 0;
        public static ulong initial_SP = 0;
        public volatile bool initial_XP_Gained_received = false;
        public volatile bool initial_SP_Gained_received = false;
        public volatile bool initial_Adena_Gained_received = false;
        
        
        public System.Collections.Stack stc_buffer = new System.Collections.Stack();

        private static Player_Info _my_char;
        private static Pet_Info _my_pet;
        private static Pet_Info _my_pet1;
        private static Pet_Info _my_pet2;
        private static Pet_Info _my_pet3;
        private static BotOptions _botoptions;
        private static AlertOptions _alertoptions;

		private System.Collections.ArrayList _BuffsGiven = new System.Collections.ArrayList();
        private System.Collections.ArrayList _Moblist = new System.Collections.ArrayList();
        private System.Collections.SortedList _inventory = new System.Collections.SortedList();
        private System.Collections.SortedList _skills = new System.Collections.SortedList();
        private System.Collections.SortedList _buffs = new System.Collections.SortedList();
        private System.Collections.SortedList _buylist = new System.Collections.SortedList();

        private System.Collections.SortedList _PartyMembers = new System.Collections.SortedList();
        public volatile uint PartyLeader = 0;
        public volatile uint PartyLoot = 0;
        public volatile uint PartyCount = 0;
        public volatile uint LootType = 0;

        public long LoginTime = 0;
        public volatile uint GameTime = 0;

        private System.Collections.SortedList _nearby_chars = new System.Collections.SortedList();
        private System.Collections.SortedList _nearby_npcs = new System.Collections.SortedList();
        private System.Collections.SortedList _nearby_items = new System.Collections.SortedList();

        private System.Collections.ArrayList _ShortCuts = new System.Collections.ArrayList(Globals.Skills_Pages * Globals.Skills_PerPage);

		private Polygon _Paths = new Polygon();
		private System.Collections.ArrayList _Walls = new System.Collections.ArrayList();

        private byte[] _sessionID = new byte[4];
        private byte[] _login_ok = new byte[8];
		private byte[] _play_ok = new byte[8];
		private byte[] _blow_key = new byte[21];
		private byte[] _game_key = new byte[16];
        private byte[] _gameguardInit = new byte[16];
		private Crypt _crypt_out;// = new Crypt();
		private Crypt _crypt_in;// = new Crypt();
		private Crypt _crypt_clientout;// = new Crypt();
		private Crypt _crypt_clientin;// = new Crypt();

        public volatile bool BOTTING = true;
        public volatile bool OOG = true;
        public volatile bool teleported = false;
        public volatile BotState BOT_STATE = BotState.Nothing;
        public volatile bool autoreply = false;
        public volatile bool autoreplyPM = false;

        public volatile int Server_ID = 0;
        public volatile int Obfuscation_Key = 0;

        public volatile uint yesno_state = 0;

        public volatile ScriptState CurrentScriptState = ScriptState.Stopped;

        private string _Login_IP;
        public volatile int Login_Port;

        private string _Game_IP;
        public volatile int Game_Port;

        private string _IG_Local_IP = "127.0.0.1";
        private string _GG_IP = "127.0.0.1";
        public volatile int IG_Local_Login_Port = 2106;
        public volatile int IG_Local_Game_Port = 7777;
        public volatile int GG_Port = 1337;

        public volatile bool ManualGameKey = false;
        public volatile bool Override_GameServer = false;
        public volatile bool OverrideProtocol = false;
        public volatile bool SecurityPinOldClient = false;
        private string _Override_Game_IP;
        public volatile int Override_Game_Port;
        public volatile bool UseProxy_LoginServer = false;
        public volatile bool UseProxy_GameServer = false;
        private string _Proxy_IP;
        public volatile int Proxy_Port;
        private string _Proxy_UserName;
        private string _Proxy_Password;
        public volatile bool Unkown_Blowfish = false;
        public volatile bool LS_GS_Same_IP = false;
        public volatile bool SecurityPinWindow = false;
        public volatile bool SecurityPinOk = false;
        public volatile bool SecurityPinSent = false;


        public volatile Chronicle Chron;

        public volatile int Protocol;//ushort

        public volatile bool logged_in = false;
        public volatile bool running = false;
        public volatile bool drawing_game = false;
        public volatile bool ig_login = false;
        public volatile bool login_failed = false;

        public volatile bool Control = false;
        public volatile bool Shift = false;
        public volatile bool AddPolygon = false;
        public volatile bool PointClicked = false;
        public volatile int New_Point_i;
        public volatile uint blistID = 0;
        public volatile uint cur_zone = 0;


        public volatile System.Net.Sockets.Socket GGsocket;

        //TD:Global pathmanager for A*
        private PathManager _pathManager = new PathManager();

        private readonly object PartyMembersLock = new object();
        private readonly object ShortCutsLock = new object();
        private readonly object sessionIDLock = new object();
        private readonly object gameguardInitLock = new object();
        private readonly object skillsLock = new object();
        private readonly object buffsLock = new object();
        private readonly object Proxy_IPLock = new object();
        private readonly object Proxy_UserNameLock = new object();
        private readonly object Proxy_PasswordLock = new object();
        private readonly object Override_Game_IPLock = new object();
        private readonly object ChangingScriptstateLock = new object();
		private readonly object PathsLock = new object();
		private readonly object WallsLock = new object();
		private readonly object login_okLock = new object();
		private readonly object play_okLock = new object();
		private readonly object blow_keyLock = new object();
		private readonly object game_keyLock = new object();
		private readonly object crypt_outLock = new object();
		private readonly object crypt_inLock = new object();
		private readonly object crypt_clientoutLock = new object();
		private readonly object crypt_clientinLock = new object();
        private readonly object BuffsGivenLock = new object();
        private readonly object MoblistLock = new object();
		private readonly object IG_Local_IPLock = new object();
        private readonly object GG_IPLock = new object();
        private readonly object Login_IPLock = new object();
        private readonly object Game_IPLock = new object();
		private readonly object gamesendqueueLock = new object();
		private readonly object gamereadqueueLock = new object();
		private readonly object clientsendqueueLock = new object();
		private readonly object clientreadqueueLock = new object();
        private readonly object LocalChatQueueLock = new object();
        private readonly object PrivateMsgQueueLock = new object();
		private readonly object nearby_charsLock = new object();
		private readonly object nearby_itemsLock = new object();
		private readonly object nearby_npcsLock = new object();
		private readonly object inventoryLock = new object();
        private readonly object buylistLock = new object();
        private readonly object pathManagerLock = new object();
        //old SummonIDs
        //public uint[] SummonIDs = new uint[] { 1538, 1561, 12077, 12311, 12312, 12313, 12526, 12527, 12528, 12564, 12621, 14001, 14002, 14003, 14004, 14005, 14006, 14007, 14008, 14009, 14010, 14011, 14012, 14013, 14014, 14015, 14016, 14017, 14018, 14019, 14020, 14021, 14022, 14023, 14024, 14025, 14026, 14027, 14028, 14029, 14030, 14031, 14032, 14033, 14034, 14035, 14036, 14037, 14038, 14039, 14040, 14041, 14042, 14043, 14044, 14045, 14046, 14047, 14048, 14049, 14050, 14051, 14052, 14053, 14054, 14055, 14056, 14057, 14058, 14059, 14060, 14061, 14062, 14063, 14064, 14065, 14066, 14067, 14068, 14069, 14070, 14071, 14072, 14073, 14074, 14075, 14076, 14077, 14078, 14079, 14080, 14081, 14082, 14083, 14084, 14085, 14086, 14087, 14088, 14089, 14090, 14091, 14092, 14093, 14094, 14095, 14096, 14097, 14098, 14099, 14100, 14101, 14102, 14103, 14104, 14105, 14106, 14107, 14108, 14109, 14110, 14111, 14112, 14113, 14114, 14115, 14116, 14117, 14118, 14119, 14120, 14121, 14122, 14123, 14124, 14125, 14126, 14127, 14128, 14129, 14130, 14131, 14132, 14133, 14134, 14135, 14136, 14137, 14138, 14139, 14140, 14141, 14142, 14143, 14144, 14145, 14146, 14147, 14148, 14149, 14150, 14151, 14152, 14153, 14154, 14155, 14156, 14157, 14158, 14159, 14160, 14161, 14162, 14163, 14164, 14165, 14166, 14167, 14168, 14169, 14170, 14171, 14172, 14173, 14174, 14175, 14176, 14177, 14178, 14179, 14180, 14181, 14182, 14183, 14184, 14185, 14186, 14187, 14188, 14189, 14190, 14191, 14192, 14193, 14194, 14195, 14196, 14197, 14198, 14199, 14200, 14201, 14202, 14203, 14204, 14205, 14206, 14207, 14208, 14209, 14210, 14211, 14212, 14213, 14214, 14215, 14216, 14217, 14218, 14219, 14220, 14221, 14222, 14223, 14224, 14225, 14226, 14227, 14228, 14229, 14230, 14231, 14232, 14233, 14234, 14235, 14236, 14237, 14238, 14239, 14240, 14241, 14242, 14243, 14244, 14245, 14246, 14247, 14248, 14249, 14250, 14251, 14252, 14253, 14254, 14255, 14256, 14257, 14258, 14259, 14260, 14265, 14266, 14267, 14268, 14269, 14270, 14271, 14272, 14273, 14274, 14275, 14276, 14277, 14278, 14279, 14280, 14281, 14282, 14283, 14284, 14285, 14286, 14287, 14288, 14289, 14290, 14291, 14292, 14293, 14294, 14295, 14296, 14297, 14298, 14299, 14300, 14301, 14302, 14303, 14304, 14305, 14306, 14307, 14308, 14309, 14310, 14311, 14312, 14313, 14314, 14315, 14316, 14317, 14318, 14319, 14320, 14321, 14322, 14323, 14324, 14325, 14326, 14327, 14328, 14329, 14330, 14331, 14332, 14333, 14334, 14335, 14336, 14337, 14338, 14339, 14340, 14341, 14342, 14343, 14344, 14345, 14346, 14347, 14348, 14349, 14350, 14351, 14352, 14353, 14354, 14355, 14356, 14357, 14358, 14359, 14360, 14361, 14362, 14363, 14364, 14365, 14366, 14367, 14368, 14369, 14370, 14371, 14372, 14373, 14374, 14375, 14376, 14377, 14378, 14379, 14380, 14381, 14382, 14383, 14384, 14385, 14386, 14387, 14388, 14389, 14390, 14391, 14392, 14393, 14394, 14395, 14396, 14397, 14398, 14399, 14400, 14401, 14402, 14403, 14404, 14405, 14406, 14407, 14408, 14409, 14410, 14411, 14412, 14413, 14414, 14415, 14416, 14417, 14418, 14419, 14420, 14421, 14422, 14423, 14424, 14425, 14426, 14427, 14428, 14429, 14430, 14431, 14432, 14433, 14434, 14435, 14436, 14437, 14438, 14439, 14440, 14441, 14442, 14443, 14444, 14449, 14450, 14451, 14452, 14453, 14454, 14455, 14456, 14457, 14458, 14459, 14460, 14461, 14462, 14463, 14464, 14465, 14466, 14467, 14468, 14469, 14470, 14471, 14472, 14473, 14474, 14475, 14476, 14477, 14478, 14479, 14480, 14481, 14482, 14483, 14484, 14485, 14486, 14487, 14488, 14489, 14490, 14491, 14492, 14493, 14494, 14495, 14496, 14497, 14498, 14499, 14500, 14501, 14502, 14503, 14504, 14505, 14506, 14507, 14508, 14509, 14510, 14511, 14512, 14513, 14514, 14515, 14516, 14517, 14518, 14519, 14520, 14521, 14522, 14523, 14524, 14525, 14526, 14527, 14528, 14529, 14530, 14531, 14532, 14533, 14534, 14535, 14536, 14537, 14538, 14539, 14540, 14541, 14542, 14543, 14544, 14545, 14546, 14547, 14548, 14549, 14550, 14551, 14552, 14553, 14554, 14555, 14556, 14557, 14558, 14559, 14560, 14561, 14562, 14563, 14564, 14565, 14566, 14567, 14568, 14569, 14570, 14571, 14572, 14573, 14574, 14575, 14576, 14577, 14578, 14579, 14580, 14581, 14582, 14583, 14584, 14585, 14586, 14587, 14588, 14589, 14590, 14591, 14592, 14593, 14594, 14595, 14596, 14597, 14598, 14599, 14600, 14601, 14602, 14603, 14604, 14605, 14606, 14607, 14608, 14609, 14610, 14611, 14612, 14613, 14614, 14615, 14616, 14617, 14618, 14619, 14620, 14621, 14622, 14623, 14624, 14625, 14626, 14627, 14628, 14633, 14634, 14635, 14636, 14637, 14638, 14639, 14640, 14641, 14642, 14643, 14644, 14645, 14646, 14647, 14648, 14649, 14650, 14651, 14652, 14653, 14654, 14655, 14656, 14657, 14658, 14659, 14660, 14661, 14662, 14663, 14664, 14665, 14666, 14667, 14668, 14669, 14670, 14671, 14672, 14673, 14674, 14675, 14676, 14677, 14678, 14679, 14680, 14681, 14682, 14683, 14684, 14685, 14686, 14687, 14688, 14689, 14690, 14691, 14692, 14693, 14694, 14695, 14696, 14697, 14698, 14699, 14700, 14701, 14702, 14703, 14704, 14705, 14706, 14707, 14708, 14709, 14710, 14711, 14712, 14713, 14714, 14715, 14716, 14717, 14718, 14719, 14720, 14721, 14722, 14723, 14724, 14725, 14726, 14727, 14728, 14729, 14730, 14731, 14732, 14733, 14734, 14735, 14736, 14799, 14800, 14801, 14802, 14803, 14804, 14805, 14806, 14807, 14808, 14809, 14810, 14811, 14812, 14813, 14814, 14815, 14816, 14817, 14818, 14819, 14820, 14821, 14822, 14823, 14824, 14825, 14826, 14827, 14828, 14829, 14830, 14831, 14832, 14833, 14834, 14835, 14836, 14837, 14838, 14870, 14871, 14872, 14873, 14874, 14875, 14876, 14877, 14878, 14879, 14880, 14881, 14882, 14883, 14884, 14885, 14886, 14887, 14888, 14889, 14890, 14891, 14892, 14893, 14894, 14895, 14896, 14897, 14898, 14899, 14900, 14901, 14902, 14903, 14904, 14905, 14906, 14907, 14908, 14909, 14910, 14911, 14912, 14913, 14914, 14915, 14916, 14917, 14918, 16025, 16030, 16037, 16038, 16039, 16040, 16041, 16042, 16043, 16044, 16045, 16046, 16050, 16051, 16052, 16053, 16067, 16068 };
        public uint[] SummonIDs = new uint[] { 14702, 14703, 14704, 14705, 14706, 14707, 14708, 14709, 14710, 14711, 14712, 14713, 14714, 14715, 14716, 14717, 14718, 14719, 14720, 14721, 14722, 14723, 14724, 14725, 14726, 14727, 14728, 14729, 14730, 14731, 14732, 14733, 14734, 14735, 14736, 106, 139, 14296, 14298, 14300, 14301, 14302, 14303, 14304, 14305, 14306, 14307, 14308, 14309, 14310, 14311, 14312, 14313, 14314, 14315, 14316, 14317, 14318, 14319, 14320, 14321, 14322, 14323, 14324, 14325, 14326, 14327, 14328, 14329, 14330, 14331, 14332, 14333, 14334, 14335, 14336, 14337, 14338, 14339, 14340, 14341, 14342, 14295, 14297, 14299, 14929, 14947, 14948, 14949, 14950, 14951, 14952, 14953, 14971, 15022, 15023, 15024, 15025, 15026, 15027, 15028, 15029, 15030, 15031, 14074, 14075, 14076, 14077, 14078, 14079, 14080, 14081, 14082, 14083, 14084, 14085, 14086, 14087, 14088, 14089, 14090, 14091, 14092, 14093, 14094, 14095, 14096, 14097, 14098, 14099, 14100, 14101, 14102, 14103, 14104, 14105, 14106, 14107, 14108, 14109, 14110, 19365, 19387, 14799, 14800, 14801, 14802, 14803, 14804, 14805, 14806, 14807, 14808, 14809, 14810, 14811, 14812, 14813, 14814, 14815, 14816, 14817, 14818, 14819, 14820, 14821, 14822, 14823, 14824, 14825, 14826, 14827, 14828, 14829, 14830, 14831, 14832, 14833, 14834, 14835, 14038, 14039, 14040, 14041, 14042, 14043, 14044, 14045, 14046, 14047, 14048, 14049, 14050, 14051, 14052, 14053, 14054, 14055, 14056, 14057, 14058, 14059, 14060, 14061, 14062, 14063, 14064, 14065, 14066, 14067, 14068, 14069, 14070, 14071, 14072, 14073, 14836, 14871, 14872, 14873, 14874, 14875, 14876, 14877, 14878, 14879, 14880, 14881, 14882, 14883, 14884, 14885, 14251, 14252, 14253, 14254, 14255, 14256, 14257, 14258, 14259, 14260, 14265, 14266, 14267, 14268, 14269, 14270, 14271, 14272, 14273, 14274, 14275, 14276, 14277, 14278, 14279, 14280, 14281, 14282, 14283, 14284, 14285, 14286, 14287, 14288, 14289, 14290, 14291, 14292, 14293, 14294, 14207, 14208, 14209, 14210, 14211, 14212, 14213, 14214, 14215, 14216, 14217, 14218, 14219, 14220, 14221, 14222, 14223, 14224, 14225, 14226, 14227, 14228, 14229, 14230, 14231, 14232, 14233, 14234, 14235, 14236, 14237, 14238, 14239, 14240, 14241, 14242, 14243, 14244, 14245, 14246, 14247, 14248, 14249, 14250, 14111, 14112, 14113, 14114, 14115, 14116, 14117, 14118, 14119, 14120, 14121, 14122, 14123, 14124, 14125, 14126, 14127, 14128, 14129, 14130, 14131, 14132, 14133, 14134, 14135, 14136, 14137, 14138, 14139, 14140, 14141, 14142, 14143, 14144, 14145, 14146, 14147, 14148, 14149, 14150, 14151, 14152, 14153, 14154, 14155, 14156, 14157, 14158, 14837, 14886, 14887, 14888, 14889, 14890, 14891, 14892, 14893, 14894, 14895, 14896, 14897, 14898, 14899, 14900, 14663, 14664, 14665, 14666, 14667, 14668, 14669, 14670, 14671, 14672, 14673, 14674, 14675, 14676, 14677, 14678, 14679, 14680, 14681, 14682, 14683, 14684, 14685, 14686, 14687, 14688, 14689, 14690, 14691, 14692, 14693, 14694, 14695, 14696, 14697, 14698, 14699, 14700, 14701, 14391, 14392, 14393, 14394, 14395, 14396, 14397, 14398, 14399, 14400, 14401, 14402, 14403, 14404, 14405, 14406, 14407, 14408, 14409, 14410, 14411, 14412, 14413, 14414, 14415, 14416, 14417, 14418, 14419, 14420, 14421, 14422, 14423, 14424, 14425, 14426, 14427, 14428, 14429, 14430, 14431, 14432, 14433, 14434, 14159, 14160, 14161, 14162, 14163, 14164, 14165, 14166, 14167, 14168, 14169, 14170, 14171, 14172, 14173, 14174, 14175, 14176, 14177, 14178, 14179, 14180, 14181, 14182, 14183, 14184, 14185, 14186, 14187, 14188, 14189, 14190, 14191, 14192, 14193, 14194, 14195, 14196, 14197, 14198, 14199, 14200, 14201, 14202, 14203, 14204, 14205, 14206, 14343, 14345, 14347, 14349, 14350, 14351, 14352, 14353, 14354, 14355, 14356, 14357, 14358, 14359, 14360, 14344, 14346, 14348, 14361, 14362, 14363, 14364, 14365, 14366, 14367, 14368, 14369, 14370, 14371, 14372, 14373, 14374, 14375, 14376, 14377, 14378, 14379, 14380, 14381, 14382, 14383, 14384, 14385, 14386, 14387, 14388, 14389, 14390, 14619, 14620, 14621, 14622, 14623, 14624, 14625, 14626, 14627, 14628, 14633, 14634, 14635, 14636, 14637, 14638, 14639, 14640, 14641, 14642, 14643, 14644, 14645, 14646, 14647, 14648, 14649, 14650, 14651, 14652, 14653, 14654, 14655, 14656, 14657, 14658, 14659, 14660, 14661, 14662, 14001, 14002, 14003, 14004, 14005, 14006, 14007, 14008, 14009, 14010, 14011, 14012, 14013, 14014, 14015, 14016, 14017, 14018, 14019, 14020, 14021, 14022, 14023, 14024, 14025, 14026, 14027, 14028, 14029, 14030, 14031, 14032, 14033, 14034, 14035, 14036, 14037, 14930, 14954, 14955, 14956, 14957, 14958, 14959, 14960, 14972, 15032, 15033, 15034, 15035, 15036, 15037, 15038, 15039, 15040, 15041, 14435, 14436, 14437, 14438, 14439, 14440, 14441, 14442, 14443, 14444, 14449, 14450, 14451, 14452, 14453, 14454, 14455, 14456, 14457, 14458, 14459, 14460, 14461, 14462, 14463, 14464, 14465, 14466, 14467, 14468, 14469, 14470, 14471, 14472, 14473, 14474, 14475, 14476, 14477, 14478, 14479, 14480, 14481, 14482, 14483, 14484, 14485, 14486, 14487, 14488, 14489, 14490, 14491, 14492, 14493, 14494, 14495, 14496, 14497, 14498, 14499, 14500, 14501, 14502, 14503, 14504, 14505, 14506, 14507, 14508, 14509, 14510, 14511, 14512, 14513, 14514, 14515, 14516, 14517, 14518, 14519, 14520, 14521, 14522, 14523, 14524, 14525, 14526, 14527, 14528, 14529, 14530, 14531, 14532, 14533, 14534, 14535, 14536, 14537, 14538, 14539, 14540, 14541, 14542, 14543, 14544, 14545, 14546, 14547, 14548, 14549, 14550, 14551, 14552, 14553, 14554, 14555, 14556, 14557, 14558, 14559, 14560, 14561, 14562, 14563, 14564, 14565, 14566, 14567, 14568, 14569, 14570, 14571, 14572, 14573, 14574, 16098, 16099, 16100, 16101, 16102, 12564, 14575, 14576, 14577, 14578, 14579, 14580, 14581, 14582, 14583, 14584, 14585, 14586, 14587, 14588, 14589, 14590, 14591, 14592, 14593, 14594, 14595, 14596, 14597, 14598, 14599, 14600, 14601, 14602, 14603, 14604, 14605, 14606, 14607, 14608, 14609, 14610, 14611, 14612, 14613, 14614, 14615, 14616, 14617, 14618, 30627, 19256, 2529, 14933, 14943, 15010, 15011, 15012, 15013, 15014, 15015, 15016, 15017, 15018, 15019, 15020, 15021, 14925, 14944, 14945, 14946, 15000, 15001, 15002, 15003, 15004, 15005, 15006, 15007, 15008, 15009, 14931, 14961, 14962, 14963, 14964, 14965, 14966, 14967, 14973, 15042, 15043, 15044, 15045, 15046, 15047, 15048, 15049, 15050, 15051, 14936, 14937, 14938, 14939, 14974, 14975, 14976, 14977, 14978, 14979, 14980, 14981, 14982, 14983, 14984, 14985 };

        public System.Collections.SortedList ClassNick = new System.Collections.SortedList() 
        { 
            {0,"Lo"},       // Human Fighter
            {1,"Lo"},       // Human Warrior
            {2,"Glad"},     // Gladiator
            {3,"WL"},       // Warlord
            {4,"Lo"},       // Human Knight
            {5,"Pal"},      // Paladin
            {6,"DA"},       // Dark Avenger
            {7,"Lo"},       // Human Rogue
            {8,"TH"},       // Treasure Hunter
            {9,"HE"},       // Hawkeye
            {10,"Lo"},      // Human Mage
            {11,"Lo"},      // Human Wizard
            {12,"Sorc"},    // Sorceror
            {13,"Necr"},    // Necromancer
            {14,"Warl"},    // Warlock
            {15,"Lo"},      // Cleric
            {16,"BP"},      // Bishop
            {17,"PP"},      // Prophet
            {18,"Lo"},      // Elven Fighter
            {19,"Lo"},      // Elven Knight
            {20,"TK"},      // Temple Knight
            {21,"SwS"},     // Sword Singer
            {22,"Lo"},      // Elven Scout
            {23,"PW"},      // Plains Walker
            {24,"SR"},      // Silver Ranger
            {25,"Lo"},      // Elven Mage
            {26,"Lo"},      // Elven Wizard
            {27,"SpS"},     // Spell Singer
            {28,"ES"},      // Elemental Summoner
            {29,"Lo"},      // Elven Oracle
            {30,"EE"},      // Elven Elder
            {31,"Lo"},      // Elf Fighter
            {32,"Lo"},      // Paulus Knight
            {33,"SK"},      // Shillien Knight
            {34,"BD"},      // BladeDancer
            {35,"Lo"},      // Dark Elf Assassin
            {36,"AW"},      // Abyss Walker
            {37,"PR"},      // Phantom Ranger
            {38,"Lo"},      // Dark Elf Mage
            {39,"Lo"},      // Dark Wizard
            {40,"SH"},      // Spellhowler
            {41,"PS"},      // Phantom Summoner
            {42,"Lo"},      // Shillien Oracle
            {43,"SE"},      // Shillien Elder
            {44,"Lo"},      // Orc Fighter
            {45,"Lo"},      // Orc Raider
            {46,"Dest"},    // Destroyer
            {47,"Lo"},      // Orc Monk
            {48,"Tyr"},     // Tyrant
            {49,"Lo"},      // Orc Mage
            {50,"Lo"},      // Orc Shaman
            {51,"OL"},      // Overlord
            {52,"WC"},      // Warcryer
            {53,"Lo"},      // Dwarf Fighter
            {54,"Lo"},      // Scavenger
            {55,"BH"},      // BountyHunter
            {56,"Lo"},      // Artisan
            {57,"WS"},      // Warsmith
            {88,"Glad+"},   // Duelist
            {89,"WL+"},     // Dreadnought
            {90,"Pal+"},    // Phoenix Knight
            {91,"DA+"},     // Hell Knight
            {92,"HE+"},     // Sagittarius
            {93,"TH+"},     // Adventurer
            {94,"Sorc+"},   // Archmage
            {95,"Necr+"},   // Soultaker
            {96,"Warl+"},   // Arcana Lord
            {97,"BP+"},     // Cardinal
            {98,"PP+"},     // Hierophant
            {99,"TK+"},     // Evas Templar
            {100,"SwS+"},   // Elven Sword Muse
            {101,"PW+"},    // Wind Rider
            {102,"SR+"},    // Moonlight Sentinel
            {103,"SpS+"},   // Mystic Muse
            {104,"ES+"},    // Elven Elemental Master
            {105,"EE+"},    // Evas Saint
            {106,"SK+"},    // Shillien Templar
            {107,"BD+"},    // SpectralDancer
            {108,"AW+"},    // Ghost Hunter
            {109,"PR+"},    // Ghost Sentinel
            {110,"SH+"},    // Storm Screamer
            {111,"PS+"},    // Spectral Master
            {112,"SE+"},    // Shillien Saint
            {113,"Dest+"},  // Titan
            {114,"Tyr+"},   // Grand Khavatari
            {115,"OL+"},    // Dominator
            {116,"WC+"},    // Doomcryer
            {117,"BH+"},    // FortuneSeeker
            {118,"WS+"},    // Maestro
            {123,"Lo"},     // Kamael Male Soldier
            {124,"Lo"},     // Kamael Female Soldier
            {125,"Lo"},     // Kamael Male Trooper
            {126,"Lo"},     // Kamael Female Warder
            {127,"Bers"},   // Berserker
            {128,"MSB"},    // Kamael Male Soulbreaker
            {129,"FSB"},    // Kamael Female Soulbreaker
            {130,"Arb"},    // Arbalester
            {131,"Bers+"},  // Doombringer
            {132,"MSB+"},   // Kamael Male Soulhound
            {133,"FSB+"},   // Kamael Female Soulhound
            {134,"Arb+"},   // Trickster
            {135,"Insp"},   // Inspector
            {136,"Insp+"},  // Judicator
        };

		public GameData()
		{
		}

        public void CreateCrypt()
        {
            crypt_in = new Crypt();
            crypt_out = new Crypt();
            crypt_clientin = new Crypt();
            crypt_clientout = new Crypt();
        }

		public void SendToGameServer(ByteBuffer buff)
		{
            uint b0 = 0, b1 = 0;//byte
            bool expacket = false;
            try
            {
                b0 = buff.GetByte(0);
                b1 = buff.GetByte(1);
            }
            catch
            {

            }

            if (b0 == System.Convert.ToUInt32(PClient.EXPacket))
                expacket = true;

            //do we have an event for this packet?
            if (ScriptEngine.SelfPacketsContainsKey((int)b0))
            {
                ScriptEvent sc_ev = new ScriptEvent();
                sc_ev.Type = EventType.SelfPacket;
                sc_ev.Type2 = (int)b0;
                sc_ev.Variables.Add(new ScriptVariable(buff, "PACKET", Var_Types.BYTEBUFFER, Var_State.PUBLIC));
                sc_ev.Variables.Add(new ScriptVariable(System.DateTime.Now.Ticks, "TIMESTAMP", Var_Types.INT, Var_State.PUBLIC));
                ScriptEngine.SendToEventQueue(sc_ev);
            }

            if (expacket && ScriptEngine.SelfPacketsEXContainsKey((int)b1))
            {
                ScriptEvent sc_ev = new ScriptEvent();
                sc_ev.Type = EventType.SelfPacketEX;
                sc_ev.Type2 = (int)b1;
                sc_ev.Variables.Add(new ScriptVariable(buff, "PACKET", Var_Types.BYTEBUFFER, Var_State.PUBLIC));
                sc_ev.Variables.Add(new ScriptVariable(System.DateTime.Now.Ticks, "TIMESTAMP", Var_Types.INT, Var_State.PUBLIC));
                ScriptEngine.SendToEventQueue(sc_ev);
            }

            if (ScriptEngine.Blocked_SelfPackets.ContainsKey((int)b0) || (expacket && ScriptEngine.Blocked_SelfPacketsEX.ContainsKey((int)b1)))
            {
                //blocked packet... do nothing
            }
            else
            {
                if (Globals.Mixer != null)
                {
                    Globals.Mixer.Encrypt0(buff);
                }

                Globals.GameSendQueueLock.EnterWriteLock();
                try
                {
                    Globals.gamedata.gamesendqueue.Enqueue(buff);
                }
                finally
                {
                    Globals.GameSendQueueLock.ExitWriteLock();
                }

                if (Globals.gamethread.sendthread.ThreadState == System.Threading.ThreadState.WaitSleepJoin)
                {
                    try
                    {
                        Globals.gamethread.sendthread.Interrupt();
                    }
                    catch (System.Threading.ThreadInterruptedException)
                    {
                        //everything worked perfect
                    }
                    catch
                    {
                        Globals.l2net_home.Add_Error("SendToGameServer error");
                    }
                }
            }
		}

        public void SendToGameServerInject(ByteBuffer buff)
        {
            if (Globals.Mixer != null)
            {
                Globals.Mixer.Encrypt0(buff);
            }

            Globals.GameSendQueueLock.EnterWriteLock();
            try
            {
                Globals.gamedata.gamesendqueue.Enqueue(buff);
            }
            finally
            {
                Globals.GameSendQueueLock.ExitWriteLock();
            }

            if (Globals.gamethread.sendthread.ThreadState == System.Threading.ThreadState.WaitSleepJoin)
            {
                try
                {
                    Globals.gamethread.sendthread.Interrupt();
                }
                catch (System.Threading.ThreadInterruptedException)
                {
                    //everything worked perfect
                }
                catch
                {
                    Globals.l2net_home.Add_Error("SendToGameServer error");
                }
            }
        }

        public void SendToGameServerNF(ByteBuffer buff)
        {
            Globals.GameSendQueueLock.EnterWriteLock();
            try
            {
                Globals.gamedata.gamesendqueue.Enqueue(buff);
            }
            finally
            {
                Globals.GameSendQueueLock.ExitWriteLock();
            }

            if (Globals.gamethread.sendthread.ThreadState == System.Threading.ThreadState.WaitSleepJoin)
            {
                try
                {
                    Globals.gamethread.sendthread.Interrupt();
                }
                catch (System.Threading.ThreadInterruptedException)
                {
                    //everything worked perfect
                }
                catch
                {
                    Globals.l2net_home.Add_Error("SendToGameServerNF error");
                }
            }
        }

		public void SendToClient(ByteBuffer buff)
		{
            Globals.ClientSendQueueLock.EnterWriteLock();
            try
            {
                Globals.gamedata.clientsendqueue.Enqueue(buff);
            }
            finally
            {
                Globals.ClientSendQueueLock.ExitWriteLock();
            }

            if (Globals.clientthread.sendthread.ThreadState == System.Threading.ThreadState.WaitSleepJoin)
			{
				try
				{
                    Globals.clientthread.sendthread.Interrupt();
				}
				catch (System.Threading.ThreadInterruptedException)
				{
					//everything worked perfect
				}
                catch
				{
					Globals.l2net_home.Add_Error("SendToClient error");
				}
			}
		}

		public void SendToBotRead(ByteBuffer buff)
		{
            Globals.GameReadQueueLock.EnterWriteLock();
            try
            {
                Globals.gamedata.gamereadqueue.Enqueue(buff);
            }
            finally
            {
                Globals.GameReadQueueLock.ExitWriteLock();
            }

            if (Globals.gameprocessdatathread.ThreadState == System.Threading.ThreadState.WaitSleepJoin)
			{
				try
				{
                    Globals.gameprocessdatathread.Interrupt();
				}
				catch (System.Threading.ThreadInterruptedException)
				{
					//everything worked perfect
				}
                catch
				{
					Globals.l2net_home.Add_Error("SendToBotRead error");
				}
			}
		}

        public int GetCount_DataToServer()
        {
            Globals.GameSendQueueLock.EnterWriteLock();
            try
            {
                return Globals.gamedata.gamesendqueue.Count;
            }
            finally
            {
                Globals.GameSendQueueLock.ExitWriteLock();
            }
        }

        public int GetCount_DataToClient()
        {
            Globals.ClientSendQueueLock.EnterWriteLock();
            try
            {
                return Globals.gamedata.clientsendqueue.Count;
            }
            finally
            {
                Globals.ClientSendQueueLock.ExitWriteLock();
            }
        }

        public int GetCount_DataToBot()
        {
            Globals.GameReadQueueLock.EnterWriteLock();
            try
            {
                return Globals.gamedata.gamereadqueue.Count;
            }
            finally
            {
                Globals.GameReadQueueLock.ExitWriteLock();
            }
        }

		public void SetBlowfishKey(string blow_key_s)
		{
            //need to convert hex to byte stuff
            blow_key = new byte[blow_key_s.Length / 2];

            string sm;

            for (int i = 0; i < blow_key_s.Length; i += 2)
			{
                sm = (blow_key_s[i].ToString() + blow_key_s[i + 1].ToString());
                blow_key[i/2] = byte.Parse(sm, System.Globalization.NumberStyles.HexNumber);
			}
		}

        public void SetBlowfishKey(byte[] blow_key_n)
        {
            blow_key = new byte[blow_key_n.Length];

            for (int i = 0; i < blow_key_n.Length; i++)
            {
                blow_key[i] = blow_key_n[i];
            }
        }

		public void Set_Char_To_Buffing()
		{
			lock(ChangingScriptstateLock)
			{
				//if the script was running
                if (CurrentScriptState == ScriptState.Running)
				{
					//lets set it to be paused while we do our shit
                    CurrentScriptState = ScriptState.Paused;

					//need to sleep for a moment while we wait for the script thread to pause
                    System.Threading.Thread.Sleep(Globals.SLEEP_Script_Reset2);
				}
			}
		}

		public void Set_Char_To_Normal()
		{
			lock(ChangingScriptstateLock)
			{
                if (CurrentScriptState == ScriptState.Paused)
				{
					//lets get restore our target
                    if (Globals.gamedata.my_char.TargetID != Globals.gamedata.my_char.LastTarget)
                    {
                        //Oddi: This is what was causing l2net to get disconnected, it tried to target with ID = 0
                        //Globals.l2net_home.Add_Text("Restore last target, ID: " + Globals.gamedata.my_char.LastTarget.ToString("X2"), Globals.Green, TextType.BOT);
                        if (Globals.gamedata.my_char.LastTarget != 0)
                        {
                            int x = 0, y = 0, z = 0;
                            Util.GetCharLoc(Globals.gamedata.my_char.LastTarget, ref x, ref y, ref z);
                            ServerPackets.Target(Globals.gamedata.my_char.LastTarget, x, y, z, false);
                        }
                    }

					//need to sleep for a moment while we wait for the target to be restored
                    System.Threading.Thread.Sleep(Globals.SLEEP_Script_Reset); //500ms

					//lets restore the script running state
                    CurrentScriptState = ScriptState.DelayStart;
				}
			}
		}

		public System.Collections.ArrayList Walls
		{
			get
			{
				System.Collections.ArrayList tmp;
				lock(WallsLock)
				{
					tmp = this._Walls;
				}
				return tmp;
			}
			set
			{
				lock(WallsLock)
				{
					_Walls = value;
				}
			}
		}
		public Polygon Paths
		{
			get
			{
				Polygon tmp;
				lock(PathsLock)
				{
					tmp = this._Paths;
				}
				return tmp;
			}
			set
			{
				lock(PathsLock)
				{
					_Paths = value;
				}
			}
		}
        public System.Collections.SortedList inventory
		{
			get
			{
                System.Collections.SortedList tmp;
				lock(inventoryLock)
				{
					tmp = this._inventory;
				}
				return tmp;
			}
			set
			{
				lock(inventoryLock)
				{
					_inventory = value;
				}
			}
		}
        public System.Collections.SortedList buylist
        {
            get
            {
                System.Collections.SortedList tmp;
                lock (buylistLock)
                {
                    tmp = this._buylist;
                }
                return tmp;
            }
            set
            {
                lock (buylistLock)
                {
                    _buylist = value;
                }
            }
        }
        public System.Collections.SortedList nearby_npcs
		{
			get
			{
                System.Collections.SortedList tmp;
				lock(nearby_npcsLock)
				{
					tmp = this._nearby_npcs;
				}
				return tmp;
			}
			set
			{
				lock(nearby_npcsLock)
				{
					_nearby_npcs = value;
				}
			}
		}
		public System.Collections.SortedList nearby_items
		{
			get
			{
                System.Collections.SortedList tmp;
				lock(nearby_itemsLock)
				{
					tmp = this._nearby_items;
				}
				return tmp;
			}
			set
			{
				lock(nearby_itemsLock)
				{
					_nearby_items = value;
				}
			}
		}
        public System.Collections.SortedList nearby_chars
		{
			get
			{
                System.Collections.SortedList tmp;
				lock(nearby_charsLock)
				{
					tmp = this._nearby_chars;
				}
				return tmp;
			}
			set
			{
				lock(nearby_charsLock)
				{
					_nearby_chars = value;
				}
			}
		}
		public System.Collections.Queue clientreadqueue
		{
			get
			{
				System.Collections.Queue tmp;
				lock(clientreadqueueLock)
				{
					tmp = this._clientreadqueue;
				}
				return tmp;
			}
			set
			{
				lock(clientreadqueueLock)
				{
					_clientreadqueue = value;
				}
			}
		}
		public System.Collections.Queue clientsendqueue
		{
			get
			{
				System.Collections.Queue tmp;
				lock(clientsendqueueLock)
				{
					tmp = this._clientsendqueue;
				}
				return tmp;
			}
			set
			{
				lock(clientsendqueueLock)
				{
					_clientsendqueue = value;
				}
			}
		}
		public System.Collections.Queue gamereadqueue
		{
			get
			{
				System.Collections.Queue tmp;
				lock(gamereadqueueLock)
				{
					tmp = this._gamereadqueue;
				}
				return tmp;
			}
			set
			{
				lock(gamereadqueueLock)
				{
					_gamereadqueue = value;
				}
			}
		}
		public System.Collections.Queue gamesendqueue
		{
			get
			{
				System.Collections.Queue tmp;
				lock(gamesendqueueLock)
				{
					tmp = this._gamesendqueue;
				}
				return tmp;
			}
			set
			{
				lock(gamesendqueueLock)
				{
					_gamesendqueue = value;
				}
			}
		}

        public System.Collections.Queue LocalChatQueue
        {
            get
            {
                System.Collections.Queue tmp;
                lock (LocalChatQueueLock)
                {
                    tmp = this._LocalChatQueue;
                }
                return tmp;
            }
            set
            {
                lock (LocalChatQueueLock)
                {
                    _LocalChatQueue = value;
                }
            }
        }

        public System.Collections.Queue PrivateMsgQueue
        {
            get
            {
                System.Collections.Queue tmp;
                lock (PrivateMsgQueueLock)
                {
                    tmp = this._PrivateMsgQueue;
                }
                return tmp;
            }
            set
            {
                lock (PrivateMsgQueueLock)
                {
                    _PrivateMsgQueue = value;
                }
            }
        }
		public string Game_IP
		{
			get
			{
				string tmp;
				lock(Game_IPLock)
				{
					tmp = this._Game_IP;
				}
				return tmp;
			}
			set
			{
				lock(Game_IPLock)
				{
					_Game_IP = value;
				}
			}
		}
        public string Login_IP
        {
            get
            {
                string tmp;
                lock (Login_IPLock)
                {
                    tmp = this._Login_IP;
                }
                return tmp;
            }
            set
            {
                lock (Login_IPLock)
                {
                    _Login_IP = value;
                }
            }
        }
		public string IG_Local_IP
		{
			get
			{
				string tmp;
				lock(IG_Local_IPLock)
				{
					tmp = this._IG_Local_IP;
				}
				return tmp;
			}
			set
			{
				lock(IG_Local_IPLock)
				{
					_IG_Local_IP = value;
				}
			}
		}

        public string GG_IP
        {
            get
            {
                string tmp;
                lock (GG_IPLock)
                {
                    tmp = this._GG_IP;
                }
                return tmp;
            }
            set
            {
                lock (GG_IPLock)
                {
                    _GG_IP = value;
                }
            }
        }


        public string Override_Game_IP
        {
            get
            {
                string tmp;
                lock (Override_Game_IPLock)
                {
                    tmp = this._Override_Game_IP;
                }
                return tmp;
            }
            set
            {
                lock (Override_Game_IPLock)
                {
                    _Override_Game_IP = value;
                }
            }
        }
        public string Proxy_IP
        {
            get
            {
                string tmp;
                lock (Proxy_IPLock)
                {
                    tmp = this._Proxy_IP;
                }
                return tmp;
            }
            set
            {
                lock (Proxy_IPLock)
                {
                    _Proxy_IP = value;
                }
            }
        }
        public string Proxy_UserName
        {
            get
            {
                string tmp;
                lock (Proxy_UserNameLock)
                {
                    tmp = this._Proxy_UserName;
                }
                return tmp;
            }
            set
            {
                lock (Proxy_UserNameLock)
                {
                    _Proxy_UserName = value;
                }
            }
        }
        public string Proxy_Password
        {
            get
            {
                string tmp;
                lock (Proxy_PasswordLock)
                {
                    tmp = this._Proxy_Password;
                }
                return tmp;
            }
            set
            {
                lock (Proxy_PasswordLock)
                {
                    _Proxy_Password = value;
                }
            }
        }
		public System.Collections.ArrayList BuffsGiven
		{
			get
			{
				System.Collections.ArrayList tmp;
				lock(BuffsGivenLock)
				{
					tmp = this._BuffsGiven;
				}
				return tmp;
			}
			set
			{
				lock(BuffsGivenLock)
				{
					_BuffsGiven = value;
				}
			}
		}


		public System.Collections.ArrayList MobList
		{
			get
			{
				System.Collections.ArrayList tmp;
                lock (MoblistLock)
				{
                    tmp = this._Moblist;
				}
				return tmp;
			}
			set
			{
                lock (MoblistLock)
				{
                    _Moblist = value;
				}
			}
		}

        
		public Crypt crypt_clientin
		{
			get
			{
				Crypt tmp;
				lock(crypt_clientinLock)
				{
					tmp = this._crypt_clientin;
				}
				return tmp;
			}
			set
			{
				lock(crypt_clientinLock)
				{
					_crypt_clientin = value;
				}
			}
		}
		public Crypt crypt_clientout
		{
			get
			{
				Crypt tmp;
				lock(crypt_clientoutLock)
				{
					tmp = this._crypt_clientout;
				}
				return tmp;
			}
			set
			{
				lock(crypt_clientoutLock)
				{
					_crypt_clientout = value;
				}
			}
		}
		public Crypt crypt_in
		{
			get
			{
				Crypt tmp;
				lock(crypt_inLock)
				{
					tmp = this._crypt_in;
				}
				return tmp;
			}
			set
			{
				lock(crypt_inLock)
				{
					_crypt_in = value;
				}
			}
		}
		public Crypt crypt_out
		{
			get
			{
				Crypt tmp;
				lock(crypt_outLock)
				{
					tmp = this._crypt_out;
				}
				return tmp;
			}
			set
			{
				lock(crypt_outLock)
				{
					_crypt_out = value;
				}
			}
		}
		public byte[] game_key
		{
			get
			{
				byte[] tmp;
				lock(game_keyLock)
				{
					tmp = this._game_key;
				}
				return tmp;
			}
			set
			{
				lock(game_keyLock)
				{
					_game_key = value;
				}
			}
		}
		public byte[] blow_key
		{
			get
			{
				byte[] tmp;
				lock(blow_keyLock)
				{
					tmp = this._blow_key;
				}
				return tmp;
			}
			set
			{
				lock(blow_keyLock)
				{
					_blow_key = value;
				}
			}
		}
		public byte[] play_ok
		{
			get
			{
				byte[] tmp;
				lock(play_okLock)
				{
					tmp = this._play_ok;
				}
				return tmp;
			}
			set
			{
				lock(play_okLock)
				{
					_play_ok = value;
				}
			}
		}
		public byte[] login_ok
		{
			get
			{
				byte[] tmp;
				lock(login_okLock)
				{
					tmp = this._login_ok;
				}
				return tmp;
			}
			set
			{
				lock(login_okLock)
				{
					_login_ok = value;
				}
			}
		}
        public System.Collections.SortedList mybuffs
        {
            get
            {
                System.Collections.SortedList tmp;
                lock (buffsLock)
                {
                    tmp = this._buffs;
                }
                return tmp;
            }
            set
            {
                lock (buffsLock)
                {
                    _buffs = value;
                }
            }
        }

        public System.Collections.SortedList skills
        {
            get
            {
                System.Collections.SortedList tmp;
                lock (skillsLock)
                {
                    tmp = this._skills;
                }
                return tmp;
            }
            set
            {
                lock (skillsLock)
                {
                    _skills = value;
                }
            }
        }
        public System.Collections.SortedList PartyMembers
        {
            get
            {
                System.Collections.SortedList tmp;
                lock (PartyMembersLock)
                {
                    tmp = this._PartyMembers;
                }
                return tmp;
            }
            set
            {
                lock (PartyMembersLock)
                {
                    _PartyMembers = value;
                }
            }
        }
        public byte[] sessionID
        {
            get
            {
                byte[] tmp;
                lock (sessionIDLock)
                {
                    tmp = this._sessionID;
                }
                return tmp;
            }
            set
            {
                lock (sessionIDLock)
                {
                    _sessionID = value;
                }
            }
        }

        public byte[] gameguardInit
        {
            get
            {
                byte[] tmp;
                lock (gameguardInitLock)
                {
                    tmp = this._gameguardInit;
                }
                return tmp;
            }
            set
            {
                lock (gameguardInitLock)
                {
                    _gameguardInit = value;
                }
            }
        }


        public System.Collections.ArrayList ShortCuts
        {
            get
            {
                System.Collections.ArrayList tmp;
                lock (ShortCutsLock)
                {
                    tmp = this._ShortCuts;
                }
                return tmp;
            }
            set
            {
                lock (ShortCutsLock)
                {
                    _ShortCuts = value;
                }
            }
        }
        public PathManager pathManager
        {
            get
            {
                PathManager tmp;
                lock (pathManagerLock)
                {
                    tmp = this._pathManager;
                }
                return tmp;
            }
            set
            {
                lock (pathManagerLock)
                {
                    _pathManager = value;
                }
            }
        }
        public Player_Info my_char
        {
            get
            {
                return GameData._my_char;
            }
            set
            {
                GameData._my_char = value;
            }
        }
        public Pet_Info my_pet
        {
            get
            {
                return GameData._my_pet;
            }
            set
            {
                GameData._my_pet = value;
            }
        }
        public Pet_Info my_pet1
        {
            get
            {
                return GameData._my_pet1;
            }
            set
            {
                GameData._my_pet1 = value;
            }
        }
        public Pet_Info my_pet2
        {
            get
            {
                return GameData._my_pet2;
            }
            set
            {
                GameData._my_pet2 = value;
            }
        }
        public Pet_Info my_pet3
        {
            get
            {
                return GameData._my_pet3;
            }
            set
            {
                GameData._my_pet3 = value;
            }
        }
        public BotOptions botoptions
        {
            get
            {
                return GameData._botoptions;
            }
            set
            {
                GameData._botoptions = value;
            }
        }
        public AlertOptions alertoptions
        {
            get
            {
                return GameData._alertoptions;
            }
            set
            {
                GameData._alertoptions = value;
            }
        }

        public bool CanAttack()
        {
            switch(BOT_STATE)
            {
                case BotState.Nothing:
                case BotState.Attacking:
                    return true;
            }

            return false;
        }

        public bool ReadyState()
        {
            switch (BOT_STATE)
            {
                case BotState.Nothing:
                case BotState.Attacking:
                case BotState.FinishedBuffing:
                    return true;
            }

            return false;
        }

        public bool JustBuffing()
        {
            switch (BOT_STATE)
            {
                case BotState.Buffing:
                case BotState.BuffWaiting:
                    return true;
            }

            return false;
        }

        public bool DoTargeting(uint targeter, uint target)
        {
            if (Globals.gamedata.BOTTING)
            {
                if ((Globals.gamedata.my_char.ID != targeter) && (Globals.gamedata.my_char.TargetID != target))
                {
                    if ((Globals.gamedata.botoptions.ActiveFollowID == targeter) && (Globals.gamedata.botoptions.ActiveFollowTarget == 1))
                    {
                        if ((targeter != 0) && (target != 0))
                        {
                            if (Globals.gamedata.ReadyState())
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
	}//end of class
}
