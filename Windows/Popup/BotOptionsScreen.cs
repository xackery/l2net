using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

namespace L2_login
{
    /// <summary>
    /// Summary description for BotOptions.
    /// </summary>
    public class BotOptionsScreen : System.Windows.Forms.Form
    {
        private IContainer components;
        private System.Windows.Forms.TabControl tabControl_botpages;
        private System.Windows.Forms.TabPage tabPage_party;
        private System.Windows.Forms.CheckBox checkBox_activefollow;
        private System.Windows.Forms.TextBox textBox_activefollow_name;
        private System.Windows.Forms.RadioButton radioButton_ActiveFollow_style1;
        private System.Windows.Forms.TextBox textBox_ActiveFollow_Dist;
        private System.Windows.Forms.RadioButton radioButton_ActiveFollow_style2;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.CheckBox checkBox_activefollow_attack;
        private System.Windows.Forms.TabPage tabPage_buffsheals;
        private System.Windows.Forms.TabPage tabPage_items;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.CheckBox checkBox_item1;
        private System.Windows.Forms.TextBox textBox_itemdelay1;
        private System.Windows.Forms.TextBox textBox_itemper1;
        private System.Windows.Forms.ComboBox comboBox_item1;
        private System.Windows.Forms.ComboBox comboBox_trait1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label_followname;
        private System.Windows.Forms.Label label_followdistance;
        private System.Windows.Forms.Label label_delaymsec;
        private System.Windows.Forms.Label label_on4;
        private System.Windows.Forms.ListView listView_buffheal;
        private System.Windows.Forms.ColumnHeader columnHeader_skill;
        private System.Windows.Forms.ColumnHeader columnHeader_trait;
        private System.Windows.Forms.ColumnHeader columnHeader_names;
        private System.Windows.Forms.ColumnHeader columnHeader_xx;
        private System.Windows.Forms.ColumnHeader columnHeader_delay;
        private System.Windows.Forms.ColumnHeader columnHeader_traitID;
        private System.Windows.Forms.ColumnHeader columnHeader_scID;
        private System.Windows.Forms.ColumnHeader columnHeader_mp;
        private System.Windows.Forms.Button button_add;
        private System.Windows.Forms.Button button_update;
        private System.Windows.Forms.TextBox textBox_buffheal_names;
        private System.Windows.Forms.ComboBox comboBox_buffheal_trait;
        private System.Windows.Forms.TextBox textBox_buffheal_min_per;
        private System.Windows.Forms.TextBox textBox_buffheal_delay;
        private System.Windows.Forms.TextBox textBox_buffheal_mp;
        private System.Windows.Forms.CheckBox checkBox_buffheal_on;
        private System.Windows.Forms.Label label_buffheal_mp;
        private System.Windows.Forms.Label label_buffheal_names;
        private System.Windows.Forms.Label label_buffheal_on;
        private System.Windows.Forms.Label label_buffheal_trait;
        private System.Windows.Forms.Label label_buffheal_delay;
        private System.Windows.Forms.Label label_buffheal_minper;
        private System.Windows.Forms.Label label_buffheal_skill;
        private System.Windows.Forms.Button button_saveoptions;
        private System.Windows.Forms.Button button_loadoptions;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Label label_target;
        private System.Windows.Forms.CheckBox checkBox_target;
        private System.Windows.Forms.ColumnHeader columnHeader_needtarget;
        private System.Windows.Forms.Button button_updateitem;
        private System.Windows.Forms.Button button_additem;
        private System.Windows.Forms.ListView listView_item;
        private System.Windows.Forms.ColumnHeader columnHeader_i_item;
        private System.Windows.Forms.ColumnHeader columnHeader_i_trait;
        private System.Windows.Forms.ColumnHeader columnHeader_i_per;
        private System.Windows.Forms.ColumnHeader columnHeader_i_delay;
        private System.Windows.Forms.ColumnHeader columnHeader_i_traitid;
        private TabPage tabPage_donot;
        private Panel panel3;
        private Panel panel2;
        private TextBox textBox_donot_npcs;
        private Button button_donot_npcs;
        private ListView listView_donot_npcs;
        private TextBox textBox_donot_items;
        private Button button_donot_items;
        private ListView listView_donot_items;
        private ContextMenuStrip contextMenuStrip_donot_items;
        private ToolStripMenuItem removeToolStripMenuItem;
        private ContextMenuStrip contextMenuStrip_donot_npcs;
        private ToolStripMenuItem removeToolStripMenuItem1;
        private ColumnHeader columnHeader_donot_npc_id;
        private ColumnHeader columnHeader_donot_npc_name;
        private ColumnHeader columnHeader_donot_item_id;
        private ColumnHeader columnHeader_donot_item_name;
        private Label label_donot_npcID;
        private Label label_donot_npcs;
        private Label label_donot_itemID;
        private Label label_donot_items;
        private ContextMenuStrip contextMenuStrip_buff;
        private ToolStripMenuItem removeToolStripMenuItem2;
        private ContextMenuStrip contextMenuStrip_item;
        private ToolStripMenuItem removeToolStripMenuItem3;
        private CheckBox checkBox_ignoreitems;
        private Label label_buffrange;
        private TextBox textBox_buffrange;
        private TextBox textBox_accept_party;
        private CheckBox checkBox_accept_party;
        private TextBox textBox_accept_rez;
        private CheckBox checkBox_accept_rez;
        private TabPage tabPage_polygon;
        private ListView listView_border;
        private ColumnHeader columnHeader_x;
        private ColumnHeader columnHeader_y;
        private Button button_addcur_polygon;
        private Label label_polgon_y;
        private TextBox textBox_polygon_y;
        private TextBox textBox_polygon_x;
        private Label label_polygon_x;
        private Button button_updatepolygon;
        private Button button_addpolygon;
        private ContextMenuStrip contextMenuStrip_polygon;
        private TabPage tabPage_combat;
        private ListView listView_combat;
        private ColumnHeader columnHeader_combat_trait;
        private ColumnHeader columnHeader_combat_shortcut;
        private ColumnHeader columnHeader_combat_percent;
        private ColumnHeader columnHeader_combat_delay;
        private ColumnHeader columnHeader_combat_mp;
        private ColumnHeader columnHeader_combat_traitID;
        private ColumnHeader columnHeader_combat_shortcutID;
        private Label label_combat_on;
        private CheckBox checkBox_combat_on;
        private ColumnHeader columnHeader_combat_conditional;
        private ColumnHeader columnHeader_combat_conditionalID;
        private Label label1;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label2;
        private TextBox textBox3;
        private Label label3;
        private CheckBox checkBox1;
        private Button button1;
        private Button button2;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private TextBox textBox4;
        private ComboBox comboBox1;
        private CheckBox checkBox2;
        private TextBox textBox5;
        private TextBox textBox6;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader11;
        private ColumnHeader columnHeader12;
        private ColumnHeader columnHeader13;
        private ColumnHeader columnHeader14;
        private ColumnHeader columnHeader15;
        private ColumnHeader columnHeader16;
        private ColumnHeader columnHeader17;
        private ColumnHeader columnHeader18;
        private Label label_combat_conditional;
        private ComboBox comboBox_combat_conditional;
        private Label label_combat_page;
        private TextBox textBox_combat_sc_page;
        private TextBox textBox_combat_sc_item;
        private Label label_combat_mp;
        private TextBox textBox_combat_mp;
        private Label label_combat_trait;
        private Label label_combat_delay;
        private Label label_combat_percent;
        private Label label_combat_shortcut;
        private ComboBox comboBox_combat_trait;
        private TextBox textBox_combat_delay;
        private TextBox textBox_combat_min_per;
        private Button button_combat_add;
        private Button button_combat_update;
        private ContextMenuStrip contextMenuStrip_combat;
        private ToolStripMenuItem removeToolStripMenuItem5;
        private ToolStripMenuItem removeToolStripMenuItem4;
        private CheckBox checkBox_activefollow_target;
        private CheckBox checkBox_buff_shift;
        private CheckBox checkBox_buff_control;
        private Label label_zrange;
        private TextBox textBox_zrange;
        private TabPage tabPage_autofighter;
        private Label label_pickup_range;
        private TextBox textBox_pickup_range;
        private CheckBox checkBox_pickup;
        private Button button_box_generate;
        private Label label_box_offset;
        private TextBox textBox_box_offset;
        private Label label_box_sides;
        private TextBox textBox_box_sides;
        private Label label_box_radius;
        private TextBox textBox_box_radius;
        private ToolStripMenuItem removeAllToolStripMenuItem;
        private ToolStripMenuItem moveUpToolStripMenuItem;
        private ToolStripMenuItem moveUpToolStripMenuItem1;
        private ToolStripMenuItem moveUpToolStripMenuItem2;
        private TextBox textBox_auto_invite;
        private CheckBox checkBox_auto_invite;
        private TabPage tabPage_soundalerts;
        private CheckBox checkBox_n1waywar;
        private TextBox textBox_player;
        private TextBox textBox_clan;
        private TextBox textBox_cp;
        private TextBox textBox_mp;
        private TextBox textBox_hp;
        private CheckBox checkBox_player;
        private CheckBox checkBox_clan;
        private Label label10;
        private CheckBox checkBox_cp;
        private Label label11;
        private CheckBox checkBox_mp;
        private Label label12;
        private CheckBox checkBox_hp;
        private CheckBox checkBox_1waywar;
        private CheckBox checkBox_2waywar;
        private Button button_close;
        private TextBox textBox_oop;
        private CheckBox checkBox_oop;
        private TabPage tabPage_target;
        private RadioButton radioButton_type2;
        private RadioButton radioButton_type1;
        private RadioButton radioButton_type0;
        private RadioButton radioButton_attackable2;
        private RadioButton radioButton_attackable1;
        private RadioButton radioButton_attackable0;
        private RadioButton radioButton_alive2;
        private RadioButton radioButton_alive1;
        private RadioButton radioButton_alive0;
        private RadioButton radioButton_inbox2;
        private RadioButton radioButton_inbox1;
        private RadioButton radioButton_inbox0;
        private RadioButton radioButton_combat2;
        private RadioButton radioButton_combat1;
        private RadioButton radioButton_combat0;
        private CheckBox checkBox_portect_priority;
        private GroupBox groupBox1;
        private GroupBox groupBox5;
        private GroupBox groupBox4;
        private GroupBox groupBox3;
        private GroupBox groupBox2;
        private CheckBox checkBox_clan_ignore;
        private CheckBox checkBox_player_ignore;
        private CheckBox checkBox_active_move_first;
        private CheckBox checkBox_friendchat;
        private CheckBox checkBox_privatemessage;
        private CheckBox checkBox_whitechat;
        private ComboBox comboBox_buffheal_skill;
        private Button button_autoss_activate;
        private ComboBox combobox_autoss;
        private Label label_autoss;
        private Button button_autoss_deactivate;
        private ToolStripMenuItem removeAllToolStripMenuItem1;
        private ToolStripMenuItem removeAllToolStripMenuItem2;
        private TabPage tabPage_RestOptions;
        private GroupBox groupBox_Rest_Party;
        private GroupBox groupBox_Rest_Solo;
        private NumericUpDown numericUpDown_RestBelowHP;
        private CheckBox checkBox_RestBelowHP;
        private Label label_percent_HP;
        private Label label_percent_MP;
        private NumericUpDown numericUpDown_RestBelowMP;
        private CheckBox checkBox_RestBelowMP;
        private Label label13;
        private NumericUpDown numericUpDown_RestUntilMP;
        private Label label17;
        private NumericUpDown numericUpDown_RestUntilHP;
        private CheckBox checkBox_RestUntilMP;
        private CheckBox checkBox_RestUntilHP;
        private TextBox textBox_FollowRestName;
        private CheckBox checkBox_FollowRest;
        private CheckBox checkBox_Ign_TreasureChests;
        private CheckBox checkBox_Ign_Raidbosses;

        //All Raidbosses and minions
        //private uint[] RaidBossIDs = new uint[] { 25001, 25004, 25007, 25010, 25013, 25016, 25019, 25020, 25023, 25026, 25029, 25032, 25035, 25038, 25041, 25044, 25047, 25050, 25051, 25054, 25057, 25060, 25063, 25064, 25067, 25070, 25073, 25076, 25079, 25082, 25085, 25088, 25089, 25092, 25095, 25098, 25099, 25102, 25103, 25106, 25109, 25112, 25115, 25118, 25119, 25122, 25125, 25126, 25127, 25128, 25131, 25134, 25137, 25140, 25143, 25146, 25149, 25152, 25155, 25158, 25159, 25162, 25163, 25166, 25169, 25170, 25173, 25176, 25179, 25182, 25185, 25188, 25189, 25192, 25198, 25199, 25202, 25205, 25208, 25211, 25214, 25217, 25220, 25223, 25226, 25229, 25230, 25233, 25234, 25235, 25238, 25241, 25244, 25245, 25248, 25249, 25252, 25255, 25256, 25259, 25260, 25263, 25266, 25269, 25272, 25273, 25276, 25277, 25280, 25281, 25282, 25283, 25286, 25290, 25293, 25296, 25299, 25302, 25305, 25306, 25309, 25312, 25315, 25316, 25319, 25322, 25325, 25328, 25352, 25354, 25357, 25360, 25362, 25365, 25366, 25369, 25372, 25373, 25375, 25378, 25380, 25383, 25385, 25388, 25391, 25392, 25394, 25395, 25398, 25401, 25404, 25407, 25410, 25412, 25415, 25418, 25420, 25423, 25426, 25429, 25431, 25434, 25437, 25438, 25441, 25444, 25447, 25450, 25453, 25456, 25460, 25463, 25467, 25470, 25473, 25475, 25478, 25481, 25484, 25487, 25490, 25493, 25496, 25498, 25501, 25504, 25506, 25509, 25512, 25514, 25523, 25524, 25527, 25528, 25531, 25532, 25534, 25536, 25539, 25540, 25542, 25544, 25603, 25609, 25610, 25611, 25612, 25616, 25617, 25618, 25619, 25620, 25621, 25622, 25624, 25671, 25674, 25677, 25680, 25681, 25684, 25687, 25690, 25691, 25692, 25693, 25694, 25695, 29030, 29033, 29036, 29037, 29056, 29060, 29062, 29065, 29095, 29096, 29129, 29132, 29135, 29138, 29141, 29144, 29147, 25374, 25503, 25502, 25499, 25500, 25287, 25288, 29064, 29063, 25443, 25371, 25399, 25257, 25258, 25206, 25207, 25147, 25210, 25209, 25031, 25030, 25327, 25326, 25435, 25436, 25046, 25271, 25270, 25120, 25121, 25058, 25059, 25148, 25037, 25036, 25077, 25274, 25275, 25084, 25083, 25081, 25526, 25218, 25219, 25153, 25009, 25008, 25110, 25111, 25370, 25468, 25682, 25469, 25683, 25045, 25278, 25222, 25221, 25268, 25267, 25379, 25184, 25332, 25419, 25543, 25432, 25433, 25537, 25445, 25461, 25462, 25017, 25442, 25150, 25279, 25151, 25497, 25323, 25317, 25318, 25292, 25291, 25307, 25308, 25200, 25156, 25157, 25053, 25541, 25294, 25101, 25525, 25492, 25676, 25295, 25403, 25012, 25427, 25428, 25451, 25466, 25464, 25465, 25132, 25133, 25130, 25015, 25014, 25386, 25353, 25246, 25247, 25675, 25491, 25002, 25201, 25011, 25177, 25679, 25489, 25397, 25253, 25142, 25689, 25141, 25688, 25187, 25186, 25171, 25172, 25480, 25479, 25382, 25116, 25117, 25298, 25297, 25167, 25449, 25448, 25408, 25409, 25104, 25105, 25025, 25024, 25477, 25476, 25061, 25062, 25237, 25236, 25048, 25049, 25180, 25181, 25174, 25175, 25228, 25227, 25027, 25028, 25439, 25440, 25264, 25265, 25474, 25093, 25094, 25204, 25183, 25368, 25367, 25330, 25396, 25406, 25405, 25123, 25124, 25107, 25108, 25284, 25285, 25387, 25250, 25393, 25114, 25113, 25034, 25080, 25452, 25321, 25033, 25331, 25239, 25313, 25314, 25191, 25417, 25416, 25413, 25414, 25505, 25381, 25422, 25400, 25193, 25194, 25203, 25022, 25021, 25168, 25003, 25483, 25377, 25052, 25078, 25231, 25232, 25160, 25507, 25508, 25389, 25390, 25042, 25043, 25096, 25097, 25402, 25161, 25411, 25074, 25075, 25072, 25071, 25251, 25300, 25213, 25212, 25320, 25139, 25384, 25324, 25301, 25178, 25310, 25311, 25482, 25458, 25069, 25068, 25459, 25457, 25364, 25363, 25446, 25144, 25145, 25216, 25215, 25355, 25356, 29058, 29057, 25138, 25224, 25225, 25091, 25090, 25673, 25516, 25672, 25515, 25488, 25494, 25495, 25100, 29097, 29098, 25678, 25455, 25358, 25359, 25018, 25055, 25136, 25135, 25430, 25242, 25243, 25303, 25304, 25065, 25066, 25254, 25486, 25485, 25087, 25086, 25425, 25424, 25039, 25040, 25262, 25261, 25421, 25545, 25005, 25006, 25472, 25686, 25471, 25685, 25510, 25511, 25056, 25538, 25329, 25129, 25376, 25154, 25240, 25454, 25361 };
        //GoD
        private uint[] RaidBossIDs = new uint[] { 29001, 29006, 29014, 29068, 29020, 29022, 29028, 29047, 25001, 25004, 25007, 25010, 25013, 25016, 25019, 25023, 25026, 25029, 25032, 25038, 25041, 25044, 25047, 25050, 25051, 25057, 25060, 25063, 25064, 25067, 25070, 25076, 25079, 25082, 25085, 25088, 25089, 25092, 25095, 25098, 25099, 25102, 25103, 25106, 25112, 25115, 25118, 25119, 25122, 25125, 25127, 25128, 25131, 25134, 25143, 25146, 25149, 25152, 25155, 25158, 25159, 25163, 25166, 25169, 25170, 25179, 25182, 25185, 25188, 25189, 25192, 25208, 25211, 25214, 25223, 25226, 25229, 25230, 25233, 25235, 25238, 25241, 25244, 25245, 25248, 25249, 25252, 25255, 25256, 25259, 25260, 25263, 25269, 25272, 25273, 25276, 25290, 25293, 25299, 25302, 25305, 25306, 25309, 25312, 25315, 25316, 25319, 25322, 25325, 25328, 25339, 25342, 25346, 25349, 25352, 25354, 25357, 25360, 25362, 25365, 25366, 25369, 25373, 25375, 25378, 25380, 25383, 25385, 25388, 25391, 25392, 25394, 25395, 25398, 25404, 25410, 25415, 25418, 25420, 25423, 25426, 25429, 25431, 25434, 25437, 25438, 25441, 25444, 25447, 25450, 25453, 25456, 25460, 25463, 25473, 25475, 25478, 25481, 25484, 25493, 25496, 25498, 25501, 25504, 25506, 25512, 29060, 29096, 29062, 25523, 25524, 29056, 29054, 25527, 29065, 29095, 25528, 25536, 25539, 25540, 25542, 25544, 29213, 25603, 25609, 25610, 25611, 25612, 29118, 29129, 29132, 29135, 29138, 29141, 29144, 29147, 25643, 25644, 25645, 25646, 25647, 25648, 25649, 25650, 25651, 25652, 29150, 25623, 25624, 25625, 25626, 29163, 25665, 25666, 25667, 25671, 25674, 25677, 25680, 25681, 25684, 25687, 29179, 25725, 25726, 25727, 25718, 25719, 25720, 25721, 25722, 25723, 25724, 25710, 29181, 25020, 25173, 29195, 29196, 25696, 25697, 25698, 25859, 25862, 25856, 25855, 25796, 25797, 25799, 29197, 25745, 25758, 25785, 25875, 25779, 25775, 25870, 25871, 29191, 29218, 25809, 25811, 25813, 25815, 25816, 25818, 25820, 25002, 25003, 25005, 25006, 25008, 25009, 25011, 25012, 25014, 25015, 25017, 25018, 25021, 25022, 25024, 25025, 25027, 25028, 25030, 25031, 25033, 25034, 25036, 25037, 25039, 25040, 25042, 25043, 25045, 25046, 25048, 25049, 25052, 25053, 25055, 25056, 25058, 25059, 25061, 25062, 25065, 25066, 25068, 25069, 25071, 25072, 25074, 25075, 25077, 25078, 25080, 25081, 25083, 25084, 25086, 25087, 25090, 25091, 25093, 25094, 25096, 25097, 25100, 25101, 25104, 25105, 25107, 25108, 25110, 25111, 25113, 25114, 25116, 25117, 25120, 25121, 25123, 25124, 25129, 25130, 25132, 25133, 25135, 25136, 25138, 25139, 25141, 25142, 25144, 25145, 25147, 25148, 25150, 25151, 25153, 25154, 25156, 25157, 25160, 25161, 25167, 25168, 25171, 25172, 25174, 25175, 25177, 25178, 25180, 25181, 25183, 25184, 25186, 25187, 25190, 25191, 25193, 25194, 25200, 25201, 25203, 25204, 25206, 25207, 25209, 25210, 25212, 25213, 25215, 25216, 25218, 25219, 25221, 25222, 25224, 25225, 25227, 25228, 25231, 25232, 25236, 25237, 25239, 25240, 25242, 25243, 25246, 25247, 25250, 25251, 25253, 25254, 25257, 25258, 25261, 25262, 25264, 25265, 25267, 25268, 25270, 25271, 25274, 25275, 25278, 25279, 25291, 25292, 25294, 25295, 25297, 25298, 25300, 25301, 25303, 25304, 25307, 25308, 25310, 25311, 25313, 25314, 25317, 25318, 25320, 25321, 25323, 25324, 25326, 25327, 25329, 25330, 25331, 25332, 25353, 25355, 25356, 25358, 25359, 25361, 25363, 25364, 25367, 25368, 25370, 25371, 25374, 25376, 25377, 25379, 25381, 25382, 25384, 25386, 25387, 25389, 25390, 25393, 25396, 25397, 25399, 25400, 25402, 25403, 25405, 25406, 25408, 25409, 25411, 25413, 25414, 25416, 25417, 25419, 25421, 25422, 25424, 25425, 25427, 25428, 25430, 25432, 25433, 25435, 25436, 25439, 25440, 25442, 25443, 25445, 25446, 25448, 25449, 25451, 25452, 25454, 25455, 25457, 25458, 25459, 25461, 25462, 25464, 25465, 25466, 25468, 25469, 25471, 25472, 25474, 25476, 25477, 25479, 25480, 25482, 25483, 25485, 25486, 25488, 25489, 25491, 25492, 25494, 25495, 25497, 25499, 25500, 25502, 25503, 25505, 25507, 25508, 25510, 25511, 25515, 25516, 25525, 25526, 25537, 25538, 25541, 25543, 25545, 25672, 25673, 25675, 25676, 25678, 25679, 25682, 25683, 25685, 25686, 25688, 25689, 25765, 25826, 25842, 25860, 25861, 25863, 25864, 25878, 29057, 29058, 29063, 29064, 29097, 29098 };
        
        private CheckBox checkBox_StuckCheck;
        private CheckBox checkBox_OnlyPickMine;
        private CheckBox checkBox_ignore_no_mesh;
        private Label label_followrestname;
        private Button button_clearoptions;
        //All treasure chests
        private uint[] TreasureChestIDs = new uint[] { 18265, 18266, 18267, 18268, 18269, 18270, 18271, 18272, 18273, 18274, 18275, 18276, 18277, 18278, 18279, 18280, 18281, 18282, 18283, 18284, 18285, 18286, 18287, 18288, 18289, 18290, 18291, 18292, 18293, 18294, 18295, 18296, 18297, 18298, 21671, 21694, 21717, 21740, 21763, 21786, 21801, 21802, 21803, 21804, 21805, 21806, 21807, 21808, 21809, 21810, 21811, 21812, 21813, 21814, 21815, 21816, 21817, 21818, 21819, 21820, 21821, 21822, 32492, 32493 };
        private CheckBox checkBox_Ign_Summons;
        //All summons
        private uint[] SummonIDs = new uint[] { 14702, 14703, 14704, 14705, 14706, 14707, 14708, 14709, 14710, 14711, 14712, 14713, 14714, 14715, 14716, 14717, 14718, 14719, 14720, 14721, 14722, 14723, 14724, 14725, 14726, 14727, 14728, 14729, 14730, 14731, 14732, 14733, 14734, 14735, 14736, 106, 139, 14296, 14298, 14300, 14301, 14302, 14303, 14304, 14305, 14306, 14307, 14308, 14309, 14310, 14311, 14312, 14313, 14314, 14315, 14316, 14317, 14318, 14319, 14320, 14321, 14322, 14323, 14324, 14325, 14326, 14327, 14328, 14329, 14330, 14331, 14332, 14333, 14334, 14335, 14336, 14337, 14338, 14339, 14340, 14341, 14342, 14295, 14297, 14299, 14929, 14947, 14948, 14949, 14950, 14951, 14952, 14953, 14971, 15022, 15023, 15024, 15025, 15026, 15027, 15028, 15029, 15030, 15031, 14074, 14075, 14076, 14077, 14078, 14079, 14080, 14081, 14082, 14083, 14084, 14085, 14086, 14087, 14088, 14089, 14090, 14091, 14092, 14093, 14094, 14095, 14096, 14097, 14098, 14099, 14100, 14101, 14102, 14103, 14104, 14105, 14106, 14107, 14108, 14109, 14110, 19365, 19387, 14799, 14800, 14801, 14802, 14803, 14804, 14805, 14806, 14807, 14808, 14809, 14810, 14811, 14812, 14813, 14814, 14815, 14816, 14817, 14818, 14819, 14820, 14821, 14822, 14823, 14824, 14825, 14826, 14827, 14828, 14829, 14830, 14831, 14832, 14833, 14834, 14835, 14038, 14039, 14040, 14041, 14042, 14043, 14044, 14045, 14046, 14047, 14048, 14049, 14050, 14051, 14052, 14053, 14054, 14055, 14056, 14057, 14058, 14059, 14060, 14061, 14062, 14063, 14064, 14065, 14066, 14067, 14068, 14069, 14070, 14071, 14072, 14073, 14836, 14871, 14872, 14873, 14874, 14875, 14876, 14877, 14878, 14879, 14880, 14881, 14882, 14883, 14884, 14885, 14251, 14252, 14253, 14254, 14255, 14256, 14257, 14258, 14259, 14260, 14265, 14266, 14267, 14268, 14269, 14270, 14271, 14272, 14273, 14274, 14275, 14276, 14277, 14278, 14279, 14280, 14281, 14282, 14283, 14284, 14285, 14286, 14287, 14288, 14289, 14290, 14291, 14292, 14293, 14294, 14207, 14208, 14209, 14210, 14211, 14212, 14213, 14214, 14215, 14216, 14217, 14218, 14219, 14220, 14221, 14222, 14223, 14224, 14225, 14226, 14227, 14228, 14229, 14230, 14231, 14232, 14233, 14234, 14235, 14236, 14237, 14238, 14239, 14240, 14241, 14242, 14243, 14244, 14245, 14246, 14247, 14248, 14249, 14250, 14111, 14112, 14113, 14114, 14115, 14116, 14117, 14118, 14119, 14120, 14121, 14122, 14123, 14124, 14125, 14126, 14127, 14128, 14129, 14130, 14131, 14132, 14133, 14134, 14135, 14136, 14137, 14138, 14139, 14140, 14141, 14142, 14143, 14144, 14145, 14146, 14147, 14148, 14149, 14150, 14151, 14152, 14153, 14154, 14155, 14156, 14157, 14158, 14837, 14886, 14887, 14888, 14889, 14890, 14891, 14892, 14893, 14894, 14895, 14896, 14897, 14898, 14899, 14900, 14663, 14664, 14665, 14666, 14667, 14668, 14669, 14670, 14671, 14672, 14673, 14674, 14675, 14676, 14677, 14678, 14679, 14680, 14681, 14682, 14683, 14684, 14685, 14686, 14687, 14688, 14689, 14690, 14691, 14692, 14693, 14694, 14695, 14696, 14697, 14698, 14699, 14700, 14701, 14391, 14392, 14393, 14394, 14395, 14396, 14397, 14398, 14399, 14400, 14401, 14402, 14403, 14404, 14405, 14406, 14407, 14408, 14409, 14410, 14411, 14412, 14413, 14414, 14415, 14416, 14417, 14418, 14419, 14420, 14421, 14422, 14423, 14424, 14425, 14426, 14427, 14428, 14429, 14430, 14431, 14432, 14433, 14434, 14159, 14160, 14161, 14162, 14163, 14164, 14165, 14166, 14167, 14168, 14169, 14170, 14171, 14172, 14173, 14174, 14175, 14176, 14177, 14178, 14179, 14180, 14181, 14182, 14183, 14184, 14185, 14186, 14187, 14188, 14189, 14190, 14191, 14192, 14193, 14194, 14195, 14196, 14197, 14198, 14199, 14200, 14201, 14202, 14203, 14204, 14205, 14206, 14343, 14345, 14347, 14349, 14350, 14351, 14352, 14353, 14354, 14355, 14356, 14357, 14358, 14359, 14360, 14344, 14346, 14348, 14361, 14362, 14363, 14364, 14365, 14366, 14367, 14368, 14369, 14370, 14371, 14372, 14373, 14374, 14375, 14376, 14377, 14378, 14379, 14380, 14381, 14382, 14383, 14384, 14385, 14386, 14387, 14388, 14389, 14390, 14619, 14620, 14621, 14622, 14623, 14624, 14625, 14626, 14627, 14628, 14633, 14634, 14635, 14636, 14637, 14638, 14639, 14640, 14641, 14642, 14643, 14644, 14645, 14646, 14647, 14648, 14649, 14650, 14651, 14652, 14653, 14654, 14655, 14656, 14657, 14658, 14659, 14660, 14661, 14662, 14001, 14002, 14003, 14004, 14005, 14006, 14007, 14008, 14009, 14010, 14011, 14012, 14013, 14014, 14015, 14016, 14017, 14018, 14019, 14020, 14021, 14022, 14023, 14024, 14025, 14026, 14027, 14028, 14029, 14030, 14031, 14032, 14033, 14034, 14035, 14036, 14037, 14930, 14954, 14955, 14956, 14957, 14958, 14959, 14960, 14972, 15032, 15033, 15034, 15035, 15036, 15037, 15038, 15039, 15040, 15041, 14435, 14436, 14437, 14438, 14439, 14440, 14441, 14442, 14443, 14444, 14449, 14450, 14451, 14452, 14453, 14454, 14455, 14456, 14457, 14458, 14459, 14460, 14461, 14462, 14463, 14464, 14465, 14466, 14467, 14468, 14469, 14470, 14471, 14472, 14473, 14474, 14475, 14476, 14477, 14478, 14479, 14480, 14481, 14482, 14483, 14484, 14485, 14486, 14487, 14488, 14489, 14490, 14491, 14492, 14493, 14494, 14495, 14496, 14497, 14498, 14499, 14500, 14501, 14502, 14503, 14504, 14505, 14506, 14507, 14508, 14509, 14510, 14511, 14512, 14513, 14514, 14515, 14516, 14517, 14518, 14519, 14520, 14521, 14522, 14523, 14524, 14525, 14526, 14527, 14528, 14529, 14530, 14531, 14532, 14533, 14534, 14535, 14536, 14537, 14538, 14539, 14540, 14541, 14542, 14543, 14544, 14545, 14546, 14547, 14548, 14549, 14550, 14551, 14552, 14553, 14554, 14555, 14556, 14557, 14558, 14559, 14560, 14561, 14562, 14563, 14564, 14565, 14566, 14567, 14568, 14569, 14570, 14571, 14572, 14573, 14574, 16098, 16099, 16100, 16101, 16102, 12564, 14575, 14576, 14577, 14578, 14579, 14580, 14581, 14582, 14583, 14584, 14585, 14586, 14587, 14588, 14589, 14590, 14591, 14592, 14593, 14594, 14595, 14596, 14597, 14598, 14599, 14600, 14601, 14602, 14603, 14604, 14605, 14606, 14607, 14608, 14609, 14610, 14611, 14612, 14613, 14614, 14615, 14616, 14617, 14618, 30627, 19256, 2529, 14933, 14943, 15010, 15011, 15012, 15013, 15014, 15015, 15016, 15017, 15018, 15019, 15020, 15021, 14925, 14944, 14945, 14946, 15000, 15001, 15002, 15003, 15004, 15005, 15006, 15007, 15008, 15009, 14931, 14961, 14962, 14963, 14964, 14965, 14966, 14967, 14973, 15042, 15043, 15044, 15045, 15046, 15047, 15048, 15049, 15050, 15051, 14936, 14937, 14938, 14939, 14974, 14975, 14976, 14977, 14978, 14979, 14980, 14981, 14982, 14983, 14984, 14985 };

        private ArrayList item_ids = new ArrayList();
        private ArrayList skill_ids = new ArrayList();
        private int last_buff = -1;
        private int last_item = -1;
        private int last_toggle = -1;
        private int last_polygon = -1;
        private TabPage tabPage_autofighter_advanced;
        private Label label_blacklist_tries;
        private Label label_autofollow_delay;
        private Label label_anti_ks_delay;
        private NumericUpDown numericUpDown_blacklist_tries;
        private NumericUpDown numericUpDown_autofollow_delay;
        private NumericUpDown numericUpDown_anti_ks_delay;
        private CheckBox checkBox_AttackOnly;
        private CheckBox checkBox_PickOnly;
        private Label label_Custom_WindowTitle;
        private Button button_Custom_WindowTitle_Set;
        private TextBox textBox_Custom_WindowTitle;
        private Button button_Custom_WindowTitle_Reset;
        private ComboBox comboBox_LootType;
        private TabPage tabPage_content_filter;
        private Label label19;
        private SplitContainer splitContainer1;
        private Label label18;
        private CheckBox cf_targetselected;
        private CheckBox cf_targetunselected;
        private CheckBox cf_filtermagicskill;
        private CheckBox cf_striptitle;
        private CheckBox cf_one_gender;
        private CheckBox cf_stripenchant;
        private CheckBox cf_norecs;
        private CheckBox cf_stripaugment;
        private CheckBox cf_simple_appearance;
        private CheckBox cf_zerononvisible;
        private CheckBox cf_ExBrExtraUserInfo;
        private CheckBox cf_dwarfmode;
        private TabPage tabPage_player_sorting;
        private TextBox textBox7;
        private Label label24;
        private Label label23;
        private Label ps_label1;
        private ListView lv_player_sort;
        private ColumnHeader player_sort_col_ID;
        private ColumnHeader player_sort_col_cname;
        private ColumnHeader player_sort_col_prio;
        private TextBox textBox9;
        private Label label26;
        private TextBox textBox8;
        private Label label25;
        private CheckBox checkBox_AutoBlacklist;
        private CheckBox checkBox_DeadReturn;
        private CheckBox checkBox_DeadLogOut;
        private TextBox textBox_DeadReturnDelay;
        private TextBox textBox_DeadLogOutDelay;
        private ComboBox comboBox_DeadReturn;
        private CheckBox checkBox_DeadToggleBotting;
        private GroupBox groupBox_DeadSettings;
        private GroupBox groupBox_StuckSettings;
        private GroupBox groupBox_PickupSettings;
        private GroupBox groupBox_SpoilSettings;
        private CheckBox checkBox_spoilcrush;
        private CheckBox checkBox_autospoil;
        private CheckBox checkBox_autosweep;
        private GroupBox groupBox_AttackSettings;
        private CheckBox checkBox_active_move_first_normal;
        private Label label_active_move_range;
        private TextBox textBox_active_move_range;
        private CheckBox checkBox_active_attack;
        private GroupBox groupBox_TargetSettings;
        private CheckBox checkBox_movebeforetargeting;
        private CheckBox checkBox_active_target;
        private GroupBox groupBox_FollowSettings;
        private GroupBox groupBox_RezSettings;
        private GroupBox groupBox_PartySettings;
        private GroupBox groupBox_BuffSettings1;
        private GroupBox groupBox_WindowTitle;
        private GroupBox groupBox_AdvancedS;
        private GroupBox groupBox_SoundAlerts;
        private GroupBox groupBox_LogOut;
        private CheckBox checkBox_1waywar_logout;
        private TextBox textBox_player_logout;
        private CheckBox checkBox_2waywar_logout;
        private TextBox textBox_clan_logout;
        private CheckBox checkBox_player_logout;
        private TextBox textBox_cp_logout;
        private CheckBox checkBox_clan_logout;
        private TextBox textBox_hp_logout;
        private TextBox textBox_mp_logout;
        private Label label14;
        private Label label16;
        private CheckBox checkBox_hp_logout;
        private CheckBox checkBox_cp_logout;
        private CheckBox checkBox_mp_logout;
        private Label label15;
        private CheckBox checkBox_n1waywar_logout;
        private CheckBox checkBox_UntilSuccess;
        private CheckBox checkBox_PickupAfterAttack;
        private Label label_SpoilMP;
        private TextBox textBox_spoil_mp;
        private GroupBox groupBox_Pet;
        private CheckBox checkBox_pet_autoassist;
        private CheckBox checkBox_activefollow_attack_Instant;
        private ToolTip toolTip_Instant_attack;
        private Label label27;
        private NumericUpDown numericUpDown_pickuptimeout;
        private CheckBox checkBox_Summon_autoassist;
        private CheckBox checkBox_pet_soloattack;
        private CheckBox checkBox_summon_instantattack;
        private CheckBox checkBox_use_plunder;
        private CheckBox checkBox_cancel_target;
        private CheckBox checkBox_drop_leader;
        private CheckBox checkBox_accept_party_alliance;
        private CheckBox checkBox_accept_party_clan;
        private CheckBox checkBox_accept_rez_alliance;
        private CheckBox checkBox_accept_rez_clan;
        private CheckBox checkBox_accept_rez_Party;
        private GroupBox groupBox6;
        private Label label30;
        private Label label29;
        private Label label28;
        private TextBox textBox_Moveto_Z;
        private TextBox textBox_Moveto_X;
        private TextBox textBox_Moveto_Y;
        private Button Set_CurrentXYZ;
        private CheckBox checkBox_MoveToLoc;
        private CheckBox checkBox_OutOfCombat;
        private Label label31;
        private TextBox textBox_MoveToLeash;
        private TabPage tabPage_toggles;
        private ComboBox comboBox_skills_toggle;
        private Button button_update_toggle;
        private Button button_add_toggle;
        private TextBox textBox_greaterthen_toggle;
        private Label label35;
        private Label label36;
        private Label label39;
        private ComboBox comboBox_trait_toggle;
        private CheckBox checkBox_onoff_toggle;
        private ListView listView_toggles;
        private ColumnHeader columnHeader_Toggle_Skill;
        private ColumnHeader columnHeader_Toggle_Trait;
        private ColumnHeader columnHeader_Toggle_LesserThen;
        private ColumnHeader columnHeader_Toggle_Biggerthan;
        private Label label32;
        private TextBox textBox_lesserthen_toggle;
        private Label label38;
        private ColumnHeader columnHeader_Toggle_TraitID;
        private ColumnHeader columnHeader_SkillID;
        private ContextMenuStrip contextMenuStrip_toggle;
        private ToolStripMenuItem toolStripMenuItem1;
        private int last_combat = -1;

        public BotOptionsScreen()
        {
            InitializeComponent();

            listView_buffheal.DoubleClick += new EventHandler(listView_buffheal_DoubleClick);
            listView_toggles.DoubleClick += new EventHandler(listView_toggles_DoubleClick);
            listView_item.DoubleClick += new EventHandler(listView_item_DoubleClick);
            listView_border.DoubleClick += new EventHandler(listView_border_DoubleClick);
            listView_combat.DoubleClick += new EventHandler(listView_combat_DoubleClick);

            comboBox_buffheal_trait.SelectedIndex = 0;
            comboBox_trait1.SelectedIndex = 0;
            comboBox_combat_trait.SelectedIndex = 0;
            comboBox_combat_conditional.SelectedIndex = 0;
            comboBox_LootType.SelectedIndex = 0;
            comboBox_DeadReturn.SelectedIndex = 0;

            //need to setup the items in the item list
            Setup();

            UpdateUI();

            if (!String.IsNullOrEmpty(Globals.BotOptionsFile))
            {

                ClearData();

                StreamReader filein = new StreamReader(Globals.BotOptionsFile);//create a new streamwritter from the stream it returns
                ReadData(filein);//load everything
                filein.Close();//close the file

                Globals.BotOptionsFile = "";

                button_save_Click(null, null);
            }


            //this.GotFocus += new EventHandler(BotOptionsScreen_GotFocus);
        }

        public void UpdateUI()
        {
            this.Text = Globals.m_ResourceManager.GetString("menuItem_bot_options");
            checkBox_activefollow.Text = Globals.m_ResourceManager.GetString("checkBox_activefollow");
            label_followname.Text = Globals.m_ResourceManager.GetString("label_followname");
            label_followdistance.Text = Globals.m_ResourceManager.GetString("label_followdistance");
            checkBox_activefollow_attack.Text = Globals.m_ResourceManager.GetString("checkBox_activefollow_attack");
            button_save.Text = Globals.m_ResourceManager.GetString("Apply");
            button_cancel.Text = Globals.m_ResourceManager.GetString("button_cancel");
            label21.Text = Globals.m_ResourceManager.GetString("Trait");
            label22.Text = Globals.m_ResourceManager.GetString("Item");
            label_delaymsec.Text = Globals.m_ResourceManager.GetString("label_delaymsec");
            label_on4.Text = Globals.m_ResourceManager.GetString("label_on");

            button_loadoptions.Text = Globals.m_ResourceManager.GetString("menuItem_bot_load");
            button_saveoptions.Text = Globals.m_ResourceManager.GetString("menuItem_bot_save");
            button_clearoptions.Text = Globals.m_ResourceManager.GetString("menuItem_bot_clear");

            button_update.Text = Globals.m_ResourceManager.GetString("Update");
            button_updateitem.Text = Globals.m_ResourceManager.GetString("Update");

            label_target.Text = Globals.m_ResourceManager.GetString("NeedTarget");
            label_buffheal_skill.Text = Globals.m_ResourceManager.GetString("Skill");

            label_buffheal_names.Text = Globals.m_ResourceManager.GetString("Names");
            label_buffheal_on.Text = Globals.m_ResourceManager.GetString("label_on"); ;
            label_buffheal_trait.Text = Globals.m_ResourceManager.GetString("Trait");
            label_buffheal_delay.Text = Globals.m_ResourceManager.GetString("delaysec");
            //label_buffheal_minper;
            //label_buffheal_mp;

            columnHeader_skill.Text = Globals.m_ResourceManager.GetString("Skill");
            columnHeader_trait.Text = Globals.m_ResourceManager.GetString("Trait");
            columnHeader_names.Text = Globals.m_ResourceManager.GetString("Names");
            columnHeader_delay.Text = Globals.m_ResourceManager.GetString("delaysec");
            columnHeader_needtarget.Text = Globals.m_ResourceManager.GetString("NeedTarget");

            columnHeader_i_item.Text = Globals.m_ResourceManager.GetString("Item");
            columnHeader_i_trait.Text = Globals.m_ResourceManager.GetString("Trait");
            columnHeader_i_delay.Text = Globals.m_ResourceManager.GetString("label_delaymsec");

            columnHeader_donot_item_name.Text = Globals.m_ResourceManager.GetString("col_Item");
            columnHeader_donot_npc_name.Text = Globals.m_ResourceManager.GetString("col_NPC");

            tabPage_party.Text = Globals.m_ResourceManager.GetString("party");
            tabPage_buffsheals.Text = Globals.m_ResourceManager.GetString("buffs_heals");
            tabPage_items.Text = Globals.m_ResourceManager.GetString("tab_Items");
            tabPage_donot.Text = Globals.m_ResourceManager.GetString("do_not");

            button_add.Text = Globals.m_ResourceManager.GetString("add");
            button_additem.Text = Globals.m_ResourceManager.GetString("add");
            button_donot_items.Text = Globals.m_ResourceManager.GetString("add");
            button_donot_npcs.Text = Globals.m_ResourceManager.GetString("add");

            label_donot_items.Text = Globals.m_ResourceManager.GetString("tab_Items");
            label_donot_npcs.Text = Globals.m_ResourceManager.GetString("tab_NPC");

            checkBox_ignoreitems.Text = Globals.m_ResourceManager.GetString("ignore_unknown_items");
            checkBox_autospoil.Text = Globals.m_ResourceManager.GetString("auto_spoil");
            checkBox_spoilcrush.Text = Globals.m_ResourceManager.GetString("spoil_crush");
            checkBox_autosweep.Text = Globals.m_ResourceManager.GetString("auto_sweep");

            //353
            tabPage_combat.Text = Globals.m_ResourceManager.GetString("combat");
            label_combat_on.Text = Globals.m_ResourceManager.GetString("label_on");
            label_combat_trait.Text = Globals.m_ResourceManager.GetString("Trait");
            label_combat_conditional.Text = Globals.m_ResourceManager.GetString("conditional");
            label_combat_shortcut.Text = Globals.m_ResourceManager.GetString("ShortCut");
            label_combat_page.Text = Globals.m_ResourceManager.GetString("Page");
            label_combat_delay.Text = Globals.m_ResourceManager.GetString("label_delaymsec");
            button_combat_add.Text = Globals.m_ResourceManager.GetString("add");
            button_combat_update.Text = Globals.m_ResourceManager.GetString("Update");
            columnHeader_combat_trait.Text = Globals.m_ResourceManager.GetString("Trait");
            columnHeader_combat_conditional.Text = Globals.m_ResourceManager.GetString("conditional");
            columnHeader_combat_shortcut.Text = Globals.m_ResourceManager.GetString("ShortCut");
            columnHeader_combat_delay.Text = Globals.m_ResourceManager.GetString("label_delaymsec");

            tabPage_polygon.Text = Globals.m_ResourceManager.GetString("polygon");
            button_addpolygon.Text = Globals.m_ResourceManager.GetString("add");
            button_updatepolygon.Text = Globals.m_ResourceManager.GetString("Update");
            button_addcur_polygon.Text = Globals.m_ResourceManager.GetString("add_current");
            label_zrange.Text = Globals.m_ResourceManager.GetString("zrange");
            label_box_radius.Text = Globals.m_ResourceManager.GetString("radius");
            label_box_sides.Text = Globals.m_ResourceManager.GetString("sides");
            label_box_offset.Text = Globals.m_ResourceManager.GetString("offset");
            button_box_generate.Text = Globals.m_ResourceManager.GetString("generate_box");

            tabPage_autofighter.Text = Globals.m_ResourceManager.GetString("autofighter");
            checkBox_active_target.Text = Globals.m_ResourceManager.GetString("active_target");
            checkBox_active_attack.Text = Globals.m_ResourceManager.GetString("active_attack");
            checkBox_pickup.Text = Globals.m_ResourceManager.GetString("active_pickup");
            label_pickup_range.Text = Globals.m_ResourceManager.GetString("active_pickup_range");

            checkBox_activefollow_target.Text = Globals.m_ResourceManager.GetString("active_follow_target");
            checkBox_accept_party.Text = Globals.m_ResourceManager.GetString("accept_party");
            checkBox_accept_rez.Text = Globals.m_ResourceManager.GetString("accept_rez");
            checkBox_buff_control.Text = Globals.m_ResourceManager.GetString("buff_control");
            checkBox_buff_shift.Text = Globals.m_ResourceManager.GetString("buff_shift");
            label_buffrange.Text = Globals.m_ResourceManager.GetString("buff_range");

            //AutoSS Stuff
            label_autoss.Text = Globals.m_ResourceManager.GetString("label_autoss");
            button_autoss_activate.Text = Globals.m_ResourceManager.GetString("but_ss_activate");
            button_autoss_deactivate.Text = Globals.m_ResourceManager.GetString("but_ss_deactivate");

            //384
            button_close.Text = Globals.m_ResourceManager.GetString("button_npc_close");
            tabPage_RestOptions.Text = Globals.m_ResourceManager.GetString("rest_options");
            tabPage_soundalerts.Text = Globals.m_ResourceManager.GetString("sound_alerts");
            tabPage_target.Text = Globals.m_ResourceManager.GetString("targeting");
            groupBox_Rest_Solo.Text = Globals.m_ResourceManager.GetString("solo_settings");
            groupBox_Rest_Party.Text = Globals.m_ResourceManager.GetString("party_settings");
            checkBox_RestBelowHP.Text = Globals.m_ResourceManager.GetString("rest_below");
            checkBox_RestBelowMP.Text = Globals.m_ResourceManager.GetString("rest_below");
            checkBox_FollowRest.Text = Globals.m_ResourceManager.GetString("follow_rest");
            label_followrestname.Text = Globals.m_ResourceManager.GetString("label_followname");
            checkBox_auto_invite.Text = Globals.m_ResourceManager.GetString("auto_send_party_invite");
            checkBox_oop.Text = Globals.m_ResourceManager.GetString("OOP_members");
            radioButton_ActiveFollow_style1.Text = Globals.m_ResourceManager.GetString("walker_style");
            radioButton_ActiveFollow_style2.Text = Globals.m_ResourceManager.GetString("l2net_style");
            checkBox_active_move_first.Text = Globals.m_ResourceManager.GetString("move_before_attack");
            label_active_move_range.Text = Globals.m_ResourceManager.GetString("move_range");
            checkBox_RestUntilHP.Text = Globals.m_ResourceManager.GetString("until");
            checkBox_RestUntilMP.Text = Globals.m_ResourceManager.GetString("until");
            checkBox_ignore_no_mesh.Text = Globals.m_ResourceManager.GetString("ignore_meshless_items");
            checkBox_Ign_Raidbosses.Text = Globals.m_ResourceManager.GetString("ignore_raidbosses");
            checkBox_Ign_TreasureChests.Text = Globals.m_ResourceManager.GetString("ignore_chests");
            checkBox_2waywar.Text = Globals.m_ResourceManager.GetString("2_way_war");
            checkBox_2waywar_logout.Text = Globals.m_ResourceManager.GetString("2_way_war");
            checkBox_1waywar.Text = Globals.m_ResourceManager.GetString("1_way_war");
            checkBox_1waywar_logout.Text = Globals.m_ResourceManager.GetString("1_way_war");
            checkBox_n1waywar.Text = Globals.m_ResourceManager.GetString("n1_way_war");
            checkBox_n1waywar_logout.Text = Globals.m_ResourceManager.GetString("n1_way_war");
            checkBox_clan.Text = Globals.m_ResourceManager.GetString("tab_Clan");
            checkBox_clan_logout.Text = Globals.m_ResourceManager.GetString("tab_Clan");
            checkBox_player.Text = Globals.m_ResourceManager.GetString("player");
            checkBox_player_logout.Text = Globals.m_ResourceManager.GetString("player");
            checkBox_clan_ignore.Text = Globals.m_ResourceManager.GetString("ignore_party_members");
            checkBox_player_ignore.Text = Globals.m_ResourceManager.GetString("ignore_party_members");
            checkBox_whitechat.Text = Globals.m_ResourceManager.GetString("white_chat");
            checkBox_privatemessage.Text = Globals.m_ResourceManager.GetString("private_message");
            checkBox_friendchat.Text = Globals.m_ResourceManager.GetString("friend_chat");

            //387
            //Party
            //radioButton_ActiveFollow_style1.Text = Globals.m_ResourceManager.GetString("walker_style");
            //radioButton_ActiveFollow_style2.Text = Globals.m_ResourceManager.GetString("l2net_style");
            //checkBox_activefollow_target.Text = Globals.m_ResourceManager.GetString("active_follow_target");
            //checkBox_auto_invite.Text = Globals.m_ResourceManager.GetString("auto_send_party_invite");
            //checkBox_oop.Text = Globals.m_ResourceManager.GetString("oop_members");

            //394
            checkBox_cancel_target.Text = Globals.m_ResourceManager.GetString("cancel_target");

            //Autofighter
            checkBox_active_move_first_normal.Text = Globals.m_ResourceManager.GetString("move_normal_before_attack");
            //checkBox_active_move_first.Text = Globals.m_ResourceManager.GetString("move_before_attack");
            //label_active_move_range.Text = Globals.m_ResourceManager.GetString("move_range");
            checkBox_StuckCheck.Text = Globals.m_ResourceManager.GetString("auto_unstuck");
            checkBox_OnlyPickMine.Text = Globals.m_ResourceManager.GetString("only_pick_mine");

            //Advanced Tab
            tabPage_autofighter_advanced.Text = Globals.m_ResourceManager.GetString("advanced");
            label_anti_ks_delay.Text = Globals.m_ResourceManager.GetString("anti_ks_delay");
            label_autofollow_delay.Text = Globals.m_ResourceManager.GetString("auto_follow_delay");
            label_blacklist_tries.Text = Globals.m_ResourceManager.GetString("blacklist_tries");
            label_Custom_WindowTitle.Text = Globals.m_ResourceManager.GetString("custom_window_title");
            button_Custom_WindowTitle_Set.Text = Globals.m_ResourceManager.GetString("btn_set");
            button_Custom_WindowTitle_Reset.Text = Globals.m_ResourceManager.GetString("btn_reset");

            //Do Not
            checkBox_PickOnly.Text = Globals.m_ResourceManager.GetString("pick_only");
            checkBox_AttackOnly.Text = Globals.m_ResourceManager.GetString("attack_only");
            checkBox_Ign_Summons.Text = Globals.m_ResourceManager.GetString("ignore_summons");

            //Targeting
            radioButton_type0.Text = Globals.m_ResourceManager.GetString("npcs");
            radioButton_type1.Text = Globals.m_ResourceManager.GetString("players");
            radioButton_type2.Text = Globals.m_ResourceManager.GetString("both");
            radioButton_attackable0.Text = Globals.m_ResourceManager.GetString("attackable");
            radioButton_attackable1.Text = Globals.m_ResourceManager.GetString("invincible");
            radioButton_attackable2.Text = Globals.m_ResourceManager.GetString("both");
            radioButton_alive0.Text = Globals.m_ResourceManager.GetString("alive");
            radioButton_alive1.Text = Globals.m_ResourceManager.GetString("dead");
            radioButton_alive2.Text = Globals.m_ResourceManager.GetString("both");
            radioButton_inbox0.Text = Globals.m_ResourceManager.GetString("in_box");
            radioButton_inbox1.Text = Globals.m_ResourceManager.GetString("not_in_box");
            radioButton_inbox2.Text = Globals.m_ResourceManager.GetString("both");
            radioButton_combat0.Text = Globals.m_ResourceManager.GetString("dont_ks");
            radioButton_combat1.Text = Globals.m_ResourceManager.GetString("only_ks");
            radioButton_combat2.Text = Globals.m_ResourceManager.GetString("both");
            checkBox_portect_priority.Text = Globals.m_ResourceManager.GetString("protect_priority");


            this.Refresh();
        }

        void listView_combat_DoubleClick(object sender, EventArgs e)
        {
            last_combat = listView_combat.SelectedIndices[0];

            comboBox_combat_trait.SelectedIndex = Util.GetInt32(listView_combat.Items[listView_combat.SelectedIndices[0]].SubItems[6].Text);
            comboBox_combat_conditional.SelectedIndex = Util.GetInt32(listView_combat.Items[listView_combat.SelectedIndices[0]].SubItems[7].Text);
            textBox_combat_min_per.Text = listView_combat.Items[listView_combat.SelectedIndices[0]].SubItems[2].Text;
            textBox_combat_sc_item.Text = (Util.GetInt32(listView_combat.Items[listView_combat.SelectedIndices[0]].SubItems[8].Text) % 12 + 1).ToString();
            textBox_combat_sc_page.Text = (Util.GetInt32(listView_combat.Items[listView_combat.SelectedIndices[0]].SubItems[8].Text) / 12 + 1).ToString();
            textBox_combat_delay.Text = listView_combat.Items[listView_combat.SelectedIndices[0]].SubItems[4].Text;
            textBox_combat_mp.Text = listView_combat.Items[listView_combat.SelectedIndices[0]].SubItems[5].Text;
            checkBox_combat_on.Checked = listView_combat.Items[listView_combat.SelectedIndices[0]].Checked;

            button_combat_update.Enabled = true;
        }

        void listView_border_DoubleClick(object sender, EventArgs e)
        {
            last_polygon = listView_border.SelectedIndices[0];

            textBox_polygon_x.Text = listView_border.Items[listView_border.SelectedIndices[0]].SubItems[0].Text;
            textBox_polygon_y.Text = listView_border.Items[listView_border.SelectedIndices[0]].SubItems[1].Text;

            button_updatepolygon.Enabled = true;
        }

        void listView_item_DoubleClick(object sender, EventArgs e)
        {
            last_item = listView_item.SelectedIndices[0];

            textBox_itemper1.Text = listView_item.Items[listView_item.SelectedIndices[0]].SubItems[2].Text;
            textBox_itemdelay1.Text = listView_item.Items[listView_item.SelectedIndices[0]].SubItems[3].Text;
            comboBox_trait1.SelectedIndex = Util.GetInt32(listView_item.Items[listView_item.SelectedIndices[0]].SubItems[4].Text);

            for (int i = 0; i < item_ids.Count; i++)
            {
                if ((uint)item_ids[i] == Util.GetUInt32(listView_item.Items[listView_item.SelectedIndices[0]].SubItems[5].Text))
                {
                    comboBox_item1.SelectedIndex = i;
                }
            }

            checkBox_item1.Checked = listView_item.Items[listView_item.SelectedIndices[0]].Checked;

            button_updateitem.Enabled = true;
        }

        void listView_toggles_DoubleClick(object sender, EventArgs e)
        {
            last_toggle = listView_toggles.SelectedIndices[0];

            comboBox_trait_toggle.SelectedIndex = Util.GetInt32(listView_toggles.Items[listView_toggles.SelectedIndices[0]].SubItems[4].Text);
            textBox_greaterthen_toggle.Text = listView_toggles.Items[listView_toggles.SelectedIndices[0]].SubItems[2].Text;
            textBox_lesserthen_toggle.Text = listView_toggles.Items[listView_toggles.SelectedIndices[0]].SubItems[3].Text;

            for (int i = 0; i < skill_ids.Count; i++)
            {
                if ((uint)skill_ids[i] == Util.GetUInt32(listView_toggles.Items[listView_toggles.SelectedIndices[0]].SubItems[5].Text))
                {
                    comboBox_skills_toggle.SelectedIndex = i;
                }
            }

            checkBox_onoff_toggle.Checked = listView_toggles.Items[listView_toggles.SelectedIndices[0]].Checked;

            button_update_toggle.Enabled = true;
        }

        void listView_buffheal_DoubleClick(object sender, EventArgs e)
        {
            last_buff = listView_buffheal.SelectedIndices[0];

            textBox_buffheal_names.Text = listView_buffheal.Items[listView_buffheal.SelectedIndices[0]].SubItems[2].Text;
            comboBox_buffheal_trait.SelectedIndex = Util.GetInt32(listView_buffheal.Items[listView_buffheal.SelectedIndices[0]].SubItems[7].Text);
            textBox_buffheal_min_per.Text = listView_buffheal.Items[listView_buffheal.SelectedIndices[0]].SubItems[3].Text;
            textBox_buffheal_delay.Text = listView_buffheal.Items[listView_buffheal.SelectedIndices[0]].SubItems[4].Text;
            textBox_buffheal_mp.Text = listView_buffheal.Items[listView_buffheal.SelectedIndices[0]].SubItems[5].Text;

            if (listView_buffheal.Items[listView_buffheal.SelectedIndices[0]].SubItems[6].Text == "1")
                checkBox_target.Checked = true;
            else
                checkBox_target.Checked = false;

            for (int i = 0; i < skill_ids.Count; i++)
            {
                if ((uint)skill_ids[i] == Util.GetUInt32(listView_buffheal.Items[listView_buffheal.SelectedIndices[0]].SubItems[8].Text))
                {
                    comboBox_buffheal_skill.SelectedIndex = i;
                }
            }

            //textBox_sc_item.Text = (Util.GetInt32(listView_buffheal.Items[listView_buffheal.SelectedIndices[0]].SubItems[8].Text) % 12 + 1).ToString();
            //textBox_sc_page.Text = (Util.GetInt32(listView_buffheal.Items[listView_buffheal.SelectedIndices[0]].SubItems[8].Text) / 12 + 1).ToString();

            checkBox_buffheal_on.Checked = listView_buffheal.Items[listView_buffheal.SelectedIndices[0]].Checked;

            button_update.Enabled = true;
        }

        /*void BotOptionsScreen_GotFocus(object sender, EventArgs e)
        {
            Setup();
        }*/

        public void Setup()
        {
            System.Threading.Tasks.Parallel.For(0, 4, (i) =>
                {
                    switch (i)
                    {
                        case 0:
                            SetSkills();
                            break;
                        case 1:
                            SetItems();
                            break;
                        case 2:
                            SetDonotLists();
                            break;
                        case 3:
                            LoadSettings();
                            break;
                    }
                }
            );
        }

        public void SetSkills()
        {
            int tmp = comboBox_buffheal_skill.SelectedIndex;

            skill_ids.Clear();
            ArrayList skill_names = new ArrayList();

            Globals.SkillListLock.EnterReadLock();
            try
            {
                foreach (UserSkill sk_inf in Globals.gamedata.skills.Values)
                {

                    if (sk_inf.Passive == 0)
                    {
                        skill_ids.Add(sk_inf.ID); //do we need passive skills here?
                        skill_names.Add(Util.GetSkillName(sk_inf.ID, sk_inf.Level));
                    }
                }
            }
            finally
            {
                Globals.SkillListLock.ExitReadLock();
            }

            //need to sort the list...
            for (int i = 0; i < skill_ids.Count; i++)
            {
                for (int j = 0; j < skill_ids.Count - i - 1; j++)
                {
                    if (System.String.Compare((string)skill_names[j], (string)skill_names[j + 1]) > 0)
                    {
                        //store the first one...
                        string tmp_s = (string)skill_names[j];
                        uint tmp_i = (uint)skill_ids[j];
                        //copy from j+1 to j
                        skill_names[j] = skill_names[j + 1];
                        skill_ids[j] = skill_ids[j + 1];
                        //copy from tmp to j+1
                        skill_names[j + 1] = tmp_s;
                        skill_ids[j + 1] = tmp_i;
                    }
                }
            }

            //done sorting
            comboBox_buffheal_skill.Items.Clear();
            for (int i = 0; i < skill_names.Count; i++)
            {
                comboBox_buffheal_skill.Items.Add(skill_names[i]);
                comboBox_skills_toggle.Items.Add(skill_names[i]);
            }

            if (tmp < comboBox_buffheal_skill.Items.Count)
            {
                comboBox_buffheal_skill.SelectedIndex = tmp;
                comboBox_skills_toggle.SelectedIndex = tmp;

            }
        }

        public void SetItems()
        {
            int tmp = comboBox_item1.SelectedIndex;
            int tmp2 = combobox_autoss.SelectedIndex;
            //int[] soulshotIDs = new int[] { 6645, 1466, 1465, 1464, 1463, 1835, 5789, 1467, 6646, 6647, 3951, 3950, 3949, 3948, 3947, 3952, 2513, 2512, 2511, 2510, 2509, 5790, 2514 ,13037, 13045, 13055, 20332, 22082, 22083, 22084, 22085, 22086, 20333, 20334, 22072, 22073, 22074, 22075, 22076, 22077, 22078, 22079, 22080, 22081};
            int[] soulshotIDs = new int[] { 1463, 1464, 1465, 1466, 1467, 1835, 5789, 6535, 6536, 6537, 6538, 6539, 6540, 22082, 22083, 22084, 22085, 22086, 21845, 21846, 21847, 21848, 21849, 21850, 17754, 33774, 33775, 33776, 33777, 33778, 33779, 33780, 22433, 2509, 2510, 2511, 2512, 2513, 2514, 5790, 22077, 22078, 22079, 22080, 22081, 21851, 21852, 21853, 21854, 21855, 21856, 19441, 33781, 33782, 33783, 33784, 33785, 33786, 33787, 3947, 3948, 3949, 3950, 3951, 3952, 22072, 22073, 22074, 22075, 22076, 19442, 33788, 33789, 33790, 33791, 33792, 33793, 33794, 22434 };

            item_ids.Clear();
            ArrayList item_names = new ArrayList();
            ArrayList item_names_ss = new ArrayList();

            Globals.InventoryLock.EnterReadLock();
            try
            {
                foreach (InventoryInfo inv_inf in Globals.gamedata.inventory.Values)
                {
                    item_ids.Add(inv_inf.ItemID);
                }
            }
            finally
            {
                Globals.InventoryLock.ExitReadLock();
            }

            for (int i = 0; i < item_ids.Count; i++)
            {
                uint tmpID = (uint)item_ids[i];

                item_names.Add(Util.GetItemName(tmpID));

                foreach (int ssID in soulshotIDs)
                {
                    if (ssID == tmpID)
                    {
                        item_names_ss.Add(Util.GetItemName(tmpID));
                        break;
                    }
                }
            }

            //need to sort the list...
            for (int i = 0; i < item_ids.Count; i++)
            {
                for (int j = 0; j < item_ids.Count - i - 1; j++)
                {
                    if (System.String.Compare((string)item_names[j], (string)item_names[j + 1]) > 0)
                    {
                        //store the first one...
                        string tmp_s = (string)item_names[j];
                        uint tmp_i = (uint)item_ids[j];
                        //copy from j+1 to j
                        item_names[j] = item_names[j + 1];
                        item_ids[j] = item_ids[j + 1];
                        //copy from tmp to j+1
                        item_names[j + 1] = tmp_s;
                        item_ids[j + 1] = tmp_i;
                    }
                }
            }

            for (int i = 0; i < item_names_ss.Count; i++)
            {
                for (int j = 0; j < item_names_ss.Count - i - 1; j++)
                {
                    if (System.String.Compare((string)item_names_ss[j], (string)item_names_ss[j + 1]) > 0)
                    {
                        //store the first one...
                        string tmp_s = (string)item_names_ss[j];
                        //copy from j+1 to j
                        item_names_ss[j] = item_names_ss[j + 1];
                        //copy from tmp to j+1
                        item_names_ss[j + 1] = tmp_s;
                    }
                }
            }
            //done sorting

            comboBox_item1.Items.Clear();
            for (int i = 0; i < item_names.Count; i++)
            {
                comboBox_item1.Items.Add(item_names[i]);
            }

            combobox_autoss.Items.Clear();
            for (int i = 0; i < item_names_ss.Count; i++)
            {
                combobox_autoss.Items.Add(item_names_ss[i]);
            }

            if (tmp < comboBox_item1.Items.Count)
            {
                comboBox_item1.SelectedIndex = tmp;
            }
            if (tmp2 < combobox_autoss.Items.Count)
            {
                combobox_autoss.SelectedIndex = tmp2;
            }
        }

        public void SetDonotLists()
        {
            //Globals.l2net_home.Add_Text("Set Donot Lists", Globals.Green);
            uint id;
            uint tmpID;
            listView_donot_items.Items.Clear();
            listView_donot_npcs.Items.Clear();

            Globals.DoNotItemLock.EnterReadLock();
            try
            {
                foreach (uint o in BotOptions.DoNotItems)
                {
                    id = o;

                    //ListViewItem item = listView_donot_items.FindItemWithText(id.ToString());
                    //if (item == null)
                    //{
                        ListViewItem ObjListItem = listView_donot_items.Items.Add(id.ToString());
                        ObjListItem.SubItems.Add(Util.GetItemName(id));
                        //Globals.l2net_home.Add_Text("ID: " + id.ToString() + " Added to listview_donot_items", Globals.Green);
                    //}
                }

            }
            finally
            {
                Globals.DoNotItemLock.ExitReadLock();
            }

            Globals.DoNotNPCLock.EnterReadLock();
            try
            {
                foreach (uint o in BotOptions.DoNotNPCs)
                {
                    id = o;

                    //We don't want all raidbosses and treasure chests to show up in the listview.
                    if (id > Globals.NPC_OFF)
                    {
                        tmpID = id - Globals.NPC_OFF;
                    }
                    else
                    {
                        tmpID = id;
                    }

                    if (!IsRaidBossOrChest(tmpID))
                    {
                        ListViewItem item = listView_donot_npcs.FindItemWithText(id.ToString());
                        if (item == null)
                        {
                            ListViewItem ObjListItem = listView_donot_npcs.Items.Add(id.ToString());
                            ObjListItem.SubItems.Add(Util.GetNPCName(id));
                        }
                    }
                }
            }
            finally
            {
                Globals.DoNotNPCLock.ExitReadLock();
            }
        }

        private bool IsRaidBossOrChest(uint tmpID)
        {
            foreach (uint rbID in RaidBossIDs)
            {
                if (rbID == tmpID)
                {
                    return true;
                }
            }
            foreach (uint chestID in TreasureChestIDs)
            {
                if (chestID == tmpID)
                {
                    return true;
                }
            }
            foreach (uint summID in SummonIDs)
            {
                if (summID == tmpID)
                {
                    return true;
                }
            }

            return false;
        }

        public void LoadSettings()
        {
            ////////////////////////PARTY OPTIONS
            if (Globals.gamedata.botoptions.ActiveFollow == 1)
                checkBox_activefollow.Checked = true;
            else
                checkBox_activefollow.Checked = false;

            textBox_activefollow_name.Text = Globals.gamedata.botoptions.ActiveFollowName;

            if (Globals.gamedata.botoptions.ActiveFollowStyle == 1)
            {
                radioButton_ActiveFollow_style1.Checked = true;
                radioButton_ActiveFollow_style2.Checked = false;
            }
            else
            {
                radioButton_ActiveFollow_style1.Checked = false;
                radioButton_ActiveFollow_style2.Checked = true;
            }

            textBox_ActiveFollow_Dist.Text = Util.Float_Int32(Globals.gamedata.botoptions.ActiveFollowDistance).ToString();

            if (Globals.gamedata.botoptions.ActiveFollowAttack == 1)
                checkBox_activefollow_attack.Checked = true;
            else
                checkBox_activefollow_attack.Checked = false;

            if (Globals.gamedata.botoptions.ActiveFollowAttackInstant == 1)
                checkBox_activefollow_attack_Instant.Checked = true;
            else
                checkBox_activefollow_attack_Instant.Checked = false;

            if (Globals.gamedata.botoptions.ActiveFollowTarget == 1)
                checkBox_activefollow_target.Checked = true;
            else
                checkBox_activefollow_target.Checked = false;

            if (Globals.gamedata.botoptions.AutoSweep == 1)
                checkBox_autosweep.Checked = true;
            else
                checkBox_autosweep.Checked = false;

            if (Globals.gamedata.botoptions.AutoSpoil == 1)
                checkBox_autospoil.Checked = true;
            else
                checkBox_autospoil.Checked = false;

            if (Globals.gamedata.botoptions.AutoSpoilUntilSuccess == 1)
                checkBox_UntilSuccess.Checked = true;
            else
                checkBox_UntilSuccess.Checked = false;

            textBox_spoil_mp.Text = Globals.gamedata.botoptions.SpoilMPAbove.ToString();

            if (Globals.gamedata.botoptions.SpoilCrush == 1)
                checkBox_spoilcrush.Checked = true;
            else
                checkBox_spoilcrush.Checked = false;

            if (Globals.gamedata.botoptions.Plunder == 1)
                checkBox_use_plunder.Checked = true;
            else
                checkBox_use_plunder.Checked = false;

            textBox_buffrange.Text = Globals.gamedata.botoptions.HealRange.ToString();

            if (Globals.gamedata.botoptions.Target == 1)
            {
                checkBox_active_target.Checked = true;
                checkBox_movebeforetargeting.Enabled = true;
            }
            else
                checkBox_active_target.Checked = false;
            if (Globals.gamedata.botoptions.Attack == 1)
                checkBox_active_attack.Checked = true;
            else
                checkBox_active_attack.Checked = false;
            if (Globals.gamedata.botoptions.Pickup == 1)
                checkBox_pickup.Checked = true;
            else
                checkBox_pickup.Checked = false;
            if (Globals.gamedata.botoptions.PetAssist == 1)
                checkBox_pet_autoassist.Checked = true;
            else
                checkBox_pet_autoassist.Checked = false;
            if (Globals.gamedata.botoptions.PickupAfterAttack == 1)
                checkBox_PickupAfterAttack.Checked = true;
            else
                checkBox_PickupAfterAttack.Checked = false;
            textBox_pickup_range.Text = Globals.gamedata.botoptions.LootRange.ToString();
            if (Globals.gamedata.botoptions.ControlBuffing == 1)
                checkBox_buff_control.Checked = true;
            else
                checkBox_buff_control.Checked = false;
            if (Globals.gamedata.botoptions.ShiftBuffing == 1)
                checkBox_buff_shift.Checked = true;
            else
                checkBox_buff_shift.Checked = false;

            textBox_zrange.Text = BotOptions.Target_ZRANGE.ToString();

            //party stuff
            if (Globals.gamedata.botoptions.AcceptParty == 1)
                checkBox_accept_party.Checked = true;
            else
                checkBox_accept_party.Checked = false;
            textBox_accept_party.Text = Globals.gamedata.botoptions.AcceptPartyNames;

            if (Globals.gamedata.botoptions.SendParty == 1)
                checkBox_auto_invite.Checked = true;
            else
                checkBox_auto_invite.Checked = false;
            textBox_auto_invite.Text = Globals.gamedata.botoptions.SendPartyNames;

            if (Globals.gamedata.botoptions.OOP == 1)
                checkBox_oop.Checked = true;
            else
                checkBox_oop.Checked = false;
            textBox_oop.Text = Globals.gamedata.botoptions.OOPNames;

            if (Globals.gamedata.botoptions.LeavePartyOnLeader == 1)
                checkBox_drop_leader.Checked = true;
            else
                checkBox_drop_leader.Checked = false;

            if (Globals.gamedata.botoptions.AcceptRezClan == 1)
                checkBox_accept_rez_clan.Checked = true;
            else
                checkBox_accept_rez_clan.Checked = false;

            if (Globals.gamedata.botoptions.AcceptRezParty == 1)
                checkBox_accept_rez_Party.Checked = true;
            else
                checkBox_accept_rez_Party.Checked = false;

            if (Globals.gamedata.botoptions.AcceptRezAlly == 1)
                checkBox_accept_rez_alliance.Checked = true;
            else
                checkBox_accept_rez_alliance.Checked = false;

            if (Globals.gamedata.botoptions.AcceptPartyClan == 1)
                checkBox_accept_party_clan.Checked = true;
            else
                checkBox_accept_party_clan.Checked = false;

            if (Globals.gamedata.botoptions.AcceptPartyAlly == 1)
                checkBox_accept_party_alliance.Checked = true;
            else
                checkBox_accept_party_alliance.Checked = false;

            if (Globals.gamedata.botoptions.AcceptRez == 1)
                checkBox_accept_rez.Checked = true;
            else
                checkBox_accept_rez.Checked = false;
            textBox_accept_rez.Text = Globals.gamedata.botoptions.AcceptRezNames;

            if (Globals.gamedata.botoptions.ProtectPriority == 1)
                checkBox_portect_priority.Checked = true;
            else
                checkBox_portect_priority.Checked = false;

            if (Globals.gamedata.botoptions.IgnoreItems == 1)
                checkBox_ignoreitems.Checked = true;
            else
                checkBox_ignoreitems.Checked = false;

            if (Globals.gamedata.botoptions.AutoBlacklist == 1)
                checkBox_AutoBlacklist.Checked = true;
            else
                checkBox_AutoBlacklist.Checked = false;

            if (Globals.gamedata.botoptions.StuckCheck == 1)
                checkBox_StuckCheck.Checked = true;
            else
                checkBox_StuckCheck.Checked =false;
            //bounding polygon
            listView_border.Items.Clear();

            foreach (Point p in Globals.gamedata.Paths.PointList)
            {
                ListViewItem ObjListItem = listView_border.Items.Add(p.X.ToString());
                ObjListItem.SubItems.Add(p.Y.ToString());
            }



            //sound alerts
            checkBox_2waywar.Checked = Globals.gamedata.alertoptions.beepon_2waywar;
            checkBox_1waywar.Checked = Globals.gamedata.alertoptions.beepon_1waywar;
            checkBox_n1waywar.Checked = Globals.gamedata.alertoptions.beepon_n1waywar;
            checkBox_hp.Checked = Globals.gamedata.alertoptions.beepon_hp;
            checkBox_mp.Checked = Globals.gamedata.alertoptions.beepon_mp;
            checkBox_cp.Checked = Globals.gamedata.alertoptions.beepon_cp;
            checkBox_clan.Checked = Globals.gamedata.alertoptions.beepon_clan;
            checkBox_player.Checked = Globals.gamedata.alertoptions.beepon_player;
            textBox_hp.Text = Globals.gamedata.alertoptions.hp_value.ToString();
            textBox_mp.Text = Globals.gamedata.alertoptions.mp_value.ToString();
            textBox_cp.Text = Globals.gamedata.alertoptions.cp_value.ToString();
            textBox_clan.Text = Globals.gamedata.alertoptions.clan_value;
            textBox_player.Text = Globals.gamedata.alertoptions.player_value;

            //Logout
            checkBox_2waywar_logout.Checked = Globals.gamedata.alertoptions.logouton_2waywar;
            checkBox_1waywar_logout.Checked = Globals.gamedata.alertoptions.logouton_1waywar;
            checkBox_n1waywar_logout.Checked = Globals.gamedata.alertoptions.logouton_n1waywar;
            checkBox_hp_logout.Checked = Globals.gamedata.alertoptions.logouton_hp;
            checkBox_mp_logout.Checked = Globals.gamedata.alertoptions.logouton_mp;
            checkBox_cp_logout.Checked = Globals.gamedata.alertoptions.logouton_cp;
            checkBox_clan_logout.Checked = Globals.gamedata.alertoptions.logouton_clan;
            checkBox_player_logout.Checked = Globals.gamedata.alertoptions.logouton_player;
            textBox_hp_logout.Text = Globals.gamedata.alertoptions.hp_value_logout.ToString();
            textBox_mp_logout.Text = Globals.gamedata.alertoptions.mp_value_logout.ToString();
            textBox_cp_logout.Text = Globals.gamedata.alertoptions.cp_value_logout.ToString();
            textBox_clan_logout.Text = Globals.gamedata.alertoptions.clan_value_logout;
            textBox_player_logout.Text = Globals.gamedata.alertoptions.player_value_logout;

            //targeting
            if (BotOptions.Target_TYPE == 0)
                radioButton_type0.Checked = true;
            else if (BotOptions.Target_TYPE == 1)
                radioButton_type1.Checked = true;
            else if (BotOptions.Target_TYPE == 2)
                radioButton_type2.Checked = true;
            if (BotOptions.Target_ATTACKABLE == 0)
                radioButton_attackable0.Checked = true;
            else if (BotOptions.Target_ATTACKABLE == 1)
                radioButton_attackable1.Checked = true;
            else if (BotOptions.Target_ATTACKABLE == 2)
                radioButton_attackable2.Checked = true;
            if (BotOptions.Target_ALIVE == 0)
                radioButton_alive0.Checked = true;
            else if (BotOptions.Target_ALIVE == 1)
                radioButton_alive1.Checked = true;
            else if (BotOptions.Target_ALIVE == 2)
                radioButton_alive2.Checked = true;
            if (BotOptions.Target_INBOX == 0)
                radioButton_inbox0.Checked = true;
            else if (BotOptions.Target_INBOX == 1)
                radioButton_inbox1.Checked = true;
            else if (BotOptions.Target_INBOX == 2)
                radioButton_inbox2.Checked = true;
            if (BotOptions.Target_COMBAT == 0)
                radioButton_combat0.Checked = true;
            else if (BotOptions.Target_COMBAT == 1)
                radioButton_combat1.Checked = true;
            else if (BotOptions.Target_COMBAT == 2)
                radioButton_combat2.Checked = true;

            //no need to load buffs/heals or combat settings since those can't be set via scripting
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ColumnHeader columnHeader_i_itemid;
            this.tabControl_botpages = new System.Windows.Forms.TabControl();
            this.tabPage_party = new System.Windows.Forms.TabPage();
            this.groupBox_RezSettings = new System.Windows.Forms.GroupBox();
            this.checkBox_accept_rez_Party = new System.Windows.Forms.CheckBox();
            this.checkBox_accept_rez_alliance = new System.Windows.Forms.CheckBox();
            this.checkBox_accept_rez_clan = new System.Windows.Forms.CheckBox();
            this.checkBox_accept_rez = new System.Windows.Forms.CheckBox();
            this.textBox_accept_rez = new System.Windows.Forms.TextBox();
            this.groupBox_PartySettings = new System.Windows.Forms.GroupBox();
            this.checkBox_accept_party_alliance = new System.Windows.Forms.CheckBox();
            this.checkBox_accept_party_clan = new System.Windows.Forms.CheckBox();
            this.checkBox_accept_party = new System.Windows.Forms.CheckBox();
            this.textBox_accept_party = new System.Windows.Forms.TextBox();
            this.checkBox_drop_leader = new System.Windows.Forms.CheckBox();
            this.checkBox_auto_invite = new System.Windows.Forms.CheckBox();
            this.textBox_oop = new System.Windows.Forms.TextBox();
            this.comboBox_LootType = new System.Windows.Forms.ComboBox();
            this.checkBox_oop = new System.Windows.Forms.CheckBox();
            this.textBox_auto_invite = new System.Windows.Forms.TextBox();
            this.groupBox_BuffSettings1 = new System.Windows.Forms.GroupBox();
            this.textBox_buffrange = new System.Windows.Forms.TextBox();
            this.label_buffrange = new System.Windows.Forms.Label();
            this.checkBox_buff_control = new System.Windows.Forms.CheckBox();
            this.checkBox_buff_shift = new System.Windows.Forms.CheckBox();
            this.groupBox_FollowSettings = new System.Windows.Forms.GroupBox();
            this.checkBox_activefollow_attack_Instant = new System.Windows.Forms.CheckBox();
            this.checkBox_activefollow = new System.Windows.Forms.CheckBox();
            this.textBox_activefollow_name = new System.Windows.Forms.TextBox();
            this.label_followname = new System.Windows.Forms.Label();
            this.radioButton_ActiveFollow_style1 = new System.Windows.Forms.RadioButton();
            this.radioButton_ActiveFollow_style2 = new System.Windows.Forms.RadioButton();
            this.textBox_ActiveFollow_Dist = new System.Windows.Forms.TextBox();
            this.label_followdistance = new System.Windows.Forms.Label();
            this.checkBox_activefollow_attack = new System.Windows.Forms.CheckBox();
            this.checkBox_activefollow_target = new System.Windows.Forms.CheckBox();
            this.tabPage_autofighter = new System.Windows.Forms.TabPage();
            this.groupBox_PickupSettings = new System.Windows.Forms.GroupBox();
            this.checkBox_PickupAfterAttack = new System.Windows.Forms.CheckBox();
            this.checkBox_pickup = new System.Windows.Forms.CheckBox();
            this.textBox_pickup_range = new System.Windows.Forms.TextBox();
            this.label_pickup_range = new System.Windows.Forms.Label();
            this.checkBox_OnlyPickMine = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label31 = new System.Windows.Forms.Label();
            this.textBox_MoveToLeash = new System.Windows.Forms.TextBox();
            this.checkBox_MoveToLoc = new System.Windows.Forms.CheckBox();
            this.checkBox_OutOfCombat = new System.Windows.Forms.CheckBox();
            this.label30 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.textBox_Moveto_Z = new System.Windows.Forms.TextBox();
            this.textBox_Moveto_X = new System.Windows.Forms.TextBox();
            this.textBox_Moveto_Y = new System.Windows.Forms.TextBox();
            this.Set_CurrentXYZ = new System.Windows.Forms.Button();
            this.checkBox_active_move_first = new System.Windows.Forms.CheckBox();
            this.groupBox_Pet = new System.Windows.Forms.GroupBox();
            this.checkBox_summon_instantattack = new System.Windows.Forms.CheckBox();
            this.checkBox_pet_soloattack = new System.Windows.Forms.CheckBox();
            this.checkBox_Summon_autoassist = new System.Windows.Forms.CheckBox();
            this.checkBox_pet_autoassist = new System.Windows.Forms.CheckBox();
            this.groupBox_DeadSettings = new System.Windows.Forms.GroupBox();
            this.checkBox_DeadToggleBotting = new System.Windows.Forms.CheckBox();
            this.checkBox_DeadReturn = new System.Windows.Forms.CheckBox();
            this.textBox_DeadReturnDelay = new System.Windows.Forms.TextBox();
            this.checkBox_DeadLogOut = new System.Windows.Forms.CheckBox();
            this.textBox_DeadLogOutDelay = new System.Windows.Forms.TextBox();
            this.comboBox_DeadReturn = new System.Windows.Forms.ComboBox();
            this.groupBox_StuckSettings = new System.Windows.Forms.GroupBox();
            this.checkBox_StuckCheck = new System.Windows.Forms.CheckBox();
            this.checkBox_AutoBlacklist = new System.Windows.Forms.CheckBox();
            this.groupBox_AttackSettings = new System.Windows.Forms.GroupBox();
            this.checkBox_cancel_target = new System.Windows.Forms.CheckBox();
            this.checkBox_active_move_first_normal = new System.Windows.Forms.CheckBox();
            this.label_active_move_range = new System.Windows.Forms.Label();
            this.textBox_active_move_range = new System.Windows.Forms.TextBox();
            this.checkBox_active_attack = new System.Windows.Forms.CheckBox();
            this.groupBox_SpoilSettings = new System.Windows.Forms.GroupBox();
            this.checkBox_use_plunder = new System.Windows.Forms.CheckBox();
            this.label_SpoilMP = new System.Windows.Forms.Label();
            this.textBox_spoil_mp = new System.Windows.Forms.TextBox();
            this.checkBox_UntilSuccess = new System.Windows.Forms.CheckBox();
            this.checkBox_spoilcrush = new System.Windows.Forms.CheckBox();
            this.checkBox_autospoil = new System.Windows.Forms.CheckBox();
            this.checkBox_autosweep = new System.Windows.Forms.CheckBox();
            this.groupBox_TargetSettings = new System.Windows.Forms.GroupBox();
            this.checkBox_movebeforetargeting = new System.Windows.Forms.CheckBox();
            this.checkBox_active_target = new System.Windows.Forms.CheckBox();
            this.tabPage_autofighter_advanced = new System.Windows.Forms.TabPage();
            this.groupBox_WindowTitle = new System.Windows.Forms.GroupBox();
            this.label_Custom_WindowTitle = new System.Windows.Forms.Label();
            this.textBox_Custom_WindowTitle = new System.Windows.Forms.TextBox();
            this.button_Custom_WindowTitle_Reset = new System.Windows.Forms.Button();
            this.button_Custom_WindowTitle_Set = new System.Windows.Forms.Button();
            this.groupBox_AdvancedS = new System.Windows.Forms.GroupBox();
            this.label27 = new System.Windows.Forms.Label();
            this.numericUpDown_pickuptimeout = new System.Windows.Forms.NumericUpDown();
            this.label_anti_ks_delay = new System.Windows.Forms.Label();
            this.numericUpDown_anti_ks_delay = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_autofollow_delay = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_blacklist_tries = new System.Windows.Forms.NumericUpDown();
            this.label_autofollow_delay = new System.Windows.Forms.Label();
            this.label_blacklist_tries = new System.Windows.Forms.Label();
            this.tabPage_RestOptions = new System.Windows.Forms.TabPage();
            this.groupBox_Rest_Party = new System.Windows.Forms.GroupBox();
            this.label_followrestname = new System.Windows.Forms.Label();
            this.textBox_FollowRestName = new System.Windows.Forms.TextBox();
            this.checkBox_FollowRest = new System.Windows.Forms.CheckBox();
            this.groupBox_Rest_Solo = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.numericUpDown_RestUntilMP = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.numericUpDown_RestUntilHP = new System.Windows.Forms.NumericUpDown();
            this.checkBox_RestUntilMP = new System.Windows.Forms.CheckBox();
            this.checkBox_RestUntilHP = new System.Windows.Forms.CheckBox();
            this.label_percent_MP = new System.Windows.Forms.Label();
            this.numericUpDown_RestBelowMP = new System.Windows.Forms.NumericUpDown();
            this.checkBox_RestBelowMP = new System.Windows.Forms.CheckBox();
            this.label_percent_HP = new System.Windows.Forms.Label();
            this.numericUpDown_RestBelowHP = new System.Windows.Forms.NumericUpDown();
            this.checkBox_RestBelowHP = new System.Windows.Forms.CheckBox();
            this.tabPage_target = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radioButton_combat0 = new System.Windows.Forms.RadioButton();
            this.radioButton_combat1 = new System.Windows.Forms.RadioButton();
            this.radioButton_combat2 = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButton_inbox0 = new System.Windows.Forms.RadioButton();
            this.radioButton_inbox1 = new System.Windows.Forms.RadioButton();
            this.radioButton_inbox2 = new System.Windows.Forms.RadioButton();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButton_alive0 = new System.Windows.Forms.RadioButton();
            this.radioButton_alive1 = new System.Windows.Forms.RadioButton();
            this.radioButton_alive2 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioButton_attackable2 = new System.Windows.Forms.RadioButton();
            this.radioButton_attackable0 = new System.Windows.Forms.RadioButton();
            this.radioButton_attackable1 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton_type0 = new System.Windows.Forms.RadioButton();
            this.radioButton_type1 = new System.Windows.Forms.RadioButton();
            this.radioButton_type2 = new System.Windows.Forms.RadioButton();
            this.checkBox_portect_priority = new System.Windows.Forms.CheckBox();
            this.tabPage_buffsheals = new System.Windows.Forms.TabPage();
            this.comboBox_buffheal_skill = new System.Windows.Forms.ComboBox();
            this.label_target = new System.Windows.Forms.Label();
            this.checkBox_target = new System.Windows.Forms.CheckBox();
            this.button_update = new System.Windows.Forms.Button();
            this.button_add = new System.Windows.Forms.Button();
            this.label_buffheal_mp = new System.Windows.Forms.Label();
            this.textBox_buffheal_mp = new System.Windows.Forms.TextBox();
            this.label_buffheal_names = new System.Windows.Forms.Label();
            this.label_buffheal_on = new System.Windows.Forms.Label();
            this.label_buffheal_trait = new System.Windows.Forms.Label();
            this.label_buffheal_delay = new System.Windows.Forms.Label();
            this.label_buffheal_minper = new System.Windows.Forms.Label();
            this.label_buffheal_skill = new System.Windows.Forms.Label();
            this.textBox_buffheal_names = new System.Windows.Forms.TextBox();
            this.comboBox_buffheal_trait = new System.Windows.Forms.ComboBox();
            this.checkBox_buffheal_on = new System.Windows.Forms.CheckBox();
            this.textBox_buffheal_delay = new System.Windows.Forms.TextBox();
            this.textBox_buffheal_min_per = new System.Windows.Forms.TextBox();
            this.listView_buffheal = new System.Windows.Forms.ListView();
            this.columnHeader_skill = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_trait = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_names = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_xx = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_delay = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_mp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_needtarget = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_traitID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_scID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip_buff = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.moveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage_toggles = new System.Windows.Forms.TabPage();
            this.label32 = new System.Windows.Forms.Label();
            this.textBox_lesserthen_toggle = new System.Windows.Forms.TextBox();
            this.comboBox_skills_toggle = new System.Windows.Forms.ComboBox();
            this.button_update_toggle = new System.Windows.Forms.Button();
            this.button_add_toggle = new System.Windows.Forms.Button();
            this.textBox_greaterthen_toggle = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.label38 = new System.Windows.Forms.Label();
            this.label39 = new System.Windows.Forms.Label();
            this.comboBox_trait_toggle = new System.Windows.Forms.ComboBox();
            this.checkBox_onoff_toggle = new System.Windows.Forms.CheckBox();
            this.listView_toggles = new System.Windows.Forms.ListView();
            this.columnHeader_Toggle_Skill = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Toggle_Trait = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Toggle_LesserThen = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Toggle_Biggerthan = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_Toggle_TraitID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_SkillID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip_toggle = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage_items = new System.Windows.Forms.TabPage();
            this.button_autoss_deactivate = new System.Windows.Forms.Button();
            this.button_autoss_activate = new System.Windows.Forms.Button();
            this.combobox_autoss = new System.Windows.Forms.ComboBox();
            this.label_autoss = new System.Windows.Forms.Label();
            this.button_updateitem = new System.Windows.Forms.Button();
            this.button_additem = new System.Windows.Forms.Button();
            this.listView_item = new System.Windows.Forms.ListView();
            this.columnHeader_i_item = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_i_trait = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_i_per = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_i_delay = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_i_traitid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip_item = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.moveUpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.label21 = new System.Windows.Forms.Label();
            this.comboBox_trait1 = new System.Windows.Forms.ComboBox();
            this.label_on4 = new System.Windows.Forms.Label();
            this.label_delaymsec = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.checkBox_item1 = new System.Windows.Forms.CheckBox();
            this.textBox_itemdelay1 = new System.Windows.Forms.TextBox();
            this.textBox_itemper1 = new System.Windows.Forms.TextBox();
            this.comboBox_item1 = new System.Windows.Forms.ComboBox();
            this.tabPage_combat = new System.Windows.Forms.TabPage();
            this.button_combat_update = new System.Windows.Forms.Button();
            this.button_combat_add = new System.Windows.Forms.Button();
            this.label_combat_conditional = new System.Windows.Forms.Label();
            this.comboBox_combat_conditional = new System.Windows.Forms.ComboBox();
            this.label_combat_page = new System.Windows.Forms.Label();
            this.textBox_combat_sc_page = new System.Windows.Forms.TextBox();
            this.textBox_combat_sc_item = new System.Windows.Forms.TextBox();
            this.label_combat_mp = new System.Windows.Forms.Label();
            this.textBox_combat_mp = new System.Windows.Forms.TextBox();
            this.label_combat_trait = new System.Windows.Forms.Label();
            this.label_combat_delay = new System.Windows.Forms.Label();
            this.label_combat_percent = new System.Windows.Forms.Label();
            this.label_combat_shortcut = new System.Windows.Forms.Label();
            this.comboBox_combat_trait = new System.Windows.Forms.ComboBox();
            this.textBox_combat_delay = new System.Windows.Forms.TextBox();
            this.textBox_combat_min_per = new System.Windows.Forms.TextBox();
            this.label_combat_on = new System.Windows.Forms.Label();
            this.checkBox_combat_on = new System.Windows.Forms.CheckBox();
            this.listView_combat = new System.Windows.Forms.ListView();
            this.columnHeader_combat_trait = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_combat_conditional = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_combat_percent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_combat_shortcut = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_combat_delay = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_combat_mp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_combat_traitID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_combat_conditionalID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_combat_shortcutID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip_combat = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.moveUpToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage_polygon = new System.Windows.Forms.TabPage();
            this.button_box_generate = new System.Windows.Forms.Button();
            this.label_box_offset = new System.Windows.Forms.Label();
            this.textBox_box_offset = new System.Windows.Forms.TextBox();
            this.label_box_sides = new System.Windows.Forms.Label();
            this.textBox_box_sides = new System.Windows.Forms.TextBox();
            this.label_box_radius = new System.Windows.Forms.Label();
            this.textBox_box_radius = new System.Windows.Forms.TextBox();
            this.label_zrange = new System.Windows.Forms.Label();
            this.textBox_zrange = new System.Windows.Forms.TextBox();
            this.button_addcur_polygon = new System.Windows.Forms.Button();
            this.label_polgon_y = new System.Windows.Forms.Label();
            this.textBox_polygon_y = new System.Windows.Forms.TextBox();
            this.textBox_polygon_x = new System.Windows.Forms.TextBox();
            this.label_polygon_x = new System.Windows.Forms.Label();
            this.button_updatepolygon = new System.Windows.Forms.Button();
            this.button_addpolygon = new System.Windows.Forms.Button();
            this.listView_border = new System.Windows.Forms.ListView();
            this.columnHeader_x = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_y = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip_polygon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage_donot = new System.Windows.Forms.TabPage();
            this.panel3 = new System.Windows.Forms.Panel();
            this.checkBox_AttackOnly = new System.Windows.Forms.CheckBox();
            this.checkBox_Ign_Summons = new System.Windows.Forms.CheckBox();
            this.checkBox_Ign_TreasureChests = new System.Windows.Forms.CheckBox();
            this.checkBox_Ign_Raidbosses = new System.Windows.Forms.CheckBox();
            this.label_donot_npcID = new System.Windows.Forms.Label();
            this.label_donot_npcs = new System.Windows.Forms.Label();
            this.textBox_donot_npcs = new System.Windows.Forms.TextBox();
            this.button_donot_npcs = new System.Windows.Forms.Button();
            this.listView_donot_npcs = new System.Windows.Forms.ListView();
            this.columnHeader_donot_npc_id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_donot_npc_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip_donot_npcs = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkBox_PickOnly = new System.Windows.Forms.CheckBox();
            this.checkBox_ignore_no_mesh = new System.Windows.Forms.CheckBox();
            this.checkBox_ignoreitems = new System.Windows.Forms.CheckBox();
            this.label_donot_itemID = new System.Windows.Forms.Label();
            this.label_donot_items = new System.Windows.Forms.Label();
            this.textBox_donot_items = new System.Windows.Forms.TextBox();
            this.button_donot_items = new System.Windows.Forms.Button();
            this.listView_donot_items = new System.Windows.Forms.ListView();
            this.columnHeader_donot_item_id = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader_donot_item_name = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip_donot_items = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAllToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage_soundalerts = new System.Windows.Forms.TabPage();
            this.groupBox_LogOut = new System.Windows.Forms.GroupBox();
            this.checkBox_1waywar_logout = new System.Windows.Forms.CheckBox();
            this.textBox_player_logout = new System.Windows.Forms.TextBox();
            this.checkBox_2waywar_logout = new System.Windows.Forms.CheckBox();
            this.textBox_clan_logout = new System.Windows.Forms.TextBox();
            this.checkBox_player_logout = new System.Windows.Forms.CheckBox();
            this.textBox_cp_logout = new System.Windows.Forms.TextBox();
            this.checkBox_clan_logout = new System.Windows.Forms.CheckBox();
            this.textBox_hp_logout = new System.Windows.Forms.TextBox();
            this.textBox_mp_logout = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.checkBox_hp_logout = new System.Windows.Forms.CheckBox();
            this.checkBox_cp_logout = new System.Windows.Forms.CheckBox();
            this.checkBox_mp_logout = new System.Windows.Forms.CheckBox();
            this.label15 = new System.Windows.Forms.Label();
            this.checkBox_n1waywar_logout = new System.Windows.Forms.CheckBox();
            this.groupBox_SoundAlerts = new System.Windows.Forms.GroupBox();
            this.checkBox_2waywar = new System.Windows.Forms.CheckBox();
            this.checkBox_player_ignore = new System.Windows.Forms.CheckBox();
            this.checkBox_friendchat = new System.Windows.Forms.CheckBox();
            this.checkBox_clan_ignore = new System.Windows.Forms.CheckBox();
            this.checkBox_1waywar = new System.Windows.Forms.CheckBox();
            this.textBox_player = new System.Windows.Forms.TextBox();
            this.checkBox_privatemessage = new System.Windows.Forms.CheckBox();
            this.textBox_clan = new System.Windows.Forms.TextBox();
            this.checkBox_player = new System.Windows.Forms.CheckBox();
            this.checkBox_n1waywar = new System.Windows.Forms.CheckBox();
            this.checkBox_clan = new System.Windows.Forms.CheckBox();
            this.textBox_cp = new System.Windows.Forms.TextBox();
            this.checkBox_whitechat = new System.Windows.Forms.CheckBox();
            this.textBox_mp = new System.Windows.Forms.TextBox();
            this.textBox_hp = new System.Windows.Forms.TextBox();
            this.checkBox_hp = new System.Windows.Forms.CheckBox();
            this.label12 = new System.Windows.Forms.Label();
            this.checkBox_mp = new System.Windows.Forms.CheckBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.checkBox_cp = new System.Windows.Forms.CheckBox();
            this.tabPage_content_filter = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cf_ExBrExtraUserInfo = new System.Windows.Forms.CheckBox();
            this.label18 = new System.Windows.Forms.Label();
            this.cf_targetselected = new System.Windows.Forms.CheckBox();
            this.cf_targetunselected = new System.Windows.Forms.CheckBox();
            this.cf_filtermagicskill = new System.Windows.Forms.CheckBox();
            this.cf_dwarfmode = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.cf_striptitle = new System.Windows.Forms.CheckBox();
            this.cf_one_gender = new System.Windows.Forms.CheckBox();
            this.cf_stripenchant = new System.Windows.Forms.CheckBox();
            this.cf_norecs = new System.Windows.Forms.CheckBox();
            this.cf_stripaugment = new System.Windows.Forms.CheckBox();
            this.cf_simple_appearance = new System.Windows.Forms.CheckBox();
            this.cf_zerononvisible = new System.Windows.Forms.CheckBox();
            this.tabPage_player_sorting = new System.Windows.Forms.TabPage();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.ps_label1 = new System.Windows.Forms.Label();
            this.lv_player_sort = new System.Windows.Forms.ListView();
            this.player_sort_col_ID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.player_sort_col_cname = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.player_sort_col_prio = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button_save = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_saveoptions = new System.Windows.Forms.Button();
            this.button_loadoptions = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button_close = new System.Windows.Forms.Button();
            this.button_clearoptions = new System.Windows.Forms.Button();
            this.toolTip_Instant_attack = new System.Windows.Forms.ToolTip(this.components);
            columnHeader_i_itemid = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl_botpages.SuspendLayout();
            this.tabPage_party.SuspendLayout();
            this.groupBox_RezSettings.SuspendLayout();
            this.groupBox_PartySettings.SuspendLayout();
            this.groupBox_BuffSettings1.SuspendLayout();
            this.groupBox_FollowSettings.SuspendLayout();
            this.tabPage_autofighter.SuspendLayout();
            this.groupBox_PickupSettings.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox_Pet.SuspendLayout();
            this.groupBox_DeadSettings.SuspendLayout();
            this.groupBox_StuckSettings.SuspendLayout();
            this.groupBox_AttackSettings.SuspendLayout();
            this.groupBox_SpoilSettings.SuspendLayout();
            this.groupBox_TargetSettings.SuspendLayout();
            this.tabPage_autofighter_advanced.SuspendLayout();
            this.groupBox_WindowTitle.SuspendLayout();
            this.groupBox_AdvancedS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_pickuptimeout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_anti_ks_delay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_autofollow_delay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_blacklist_tries)).BeginInit();
            this.tabPage_RestOptions.SuspendLayout();
            this.groupBox_Rest_Party.SuspendLayout();
            this.groupBox_Rest_Solo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RestUntilMP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RestUntilHP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RestBelowMP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RestBelowHP)).BeginInit();
            this.tabPage_target.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage_buffsheals.SuspendLayout();
            this.contextMenuStrip_buff.SuspendLayout();
            this.tabPage_toggles.SuspendLayout();
            this.contextMenuStrip_toggle.SuspendLayout();
            this.tabPage_items.SuspendLayout();
            this.contextMenuStrip_item.SuspendLayout();
            this.tabPage_combat.SuspendLayout();
            this.contextMenuStrip_combat.SuspendLayout();
            this.tabPage_polygon.SuspendLayout();
            this.contextMenuStrip_polygon.SuspendLayout();
            this.tabPage_donot.SuspendLayout();
            this.panel3.SuspendLayout();
            this.contextMenuStrip_donot_npcs.SuspendLayout();
            this.panel2.SuspendLayout();
            this.contextMenuStrip_donot_items.SuspendLayout();
            this.tabPage_soundalerts.SuspendLayout();
            this.groupBox_LogOut.SuspendLayout();
            this.groupBox_SoundAlerts.SuspendLayout();
            this.tabPage_content_filter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tabPage_player_sorting.SuspendLayout();
            this.SuspendLayout();
            // 
            // columnHeader_i_itemid
            // 
            columnHeader_i_itemid.Text = "ItemID";
            columnHeader_i_itemid.Width = 0;
            // 
            // tabControl_botpages
            // 
            this.tabControl_botpages.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl_botpages.Controls.Add(this.tabPage_party);
            this.tabControl_botpages.Controls.Add(this.tabPage_autofighter);
            this.tabControl_botpages.Controls.Add(this.tabPage_autofighter_advanced);
            this.tabControl_botpages.Controls.Add(this.tabPage_RestOptions);
            this.tabControl_botpages.Controls.Add(this.tabPage_target);
            this.tabControl_botpages.Controls.Add(this.tabPage_buffsheals);
            this.tabControl_botpages.Controls.Add(this.tabPage_toggles);
            this.tabControl_botpages.Controls.Add(this.tabPage_items);
            this.tabControl_botpages.Controls.Add(this.tabPage_combat);
            this.tabControl_botpages.Controls.Add(this.tabPage_polygon);
            this.tabControl_botpages.Controls.Add(this.tabPage_donot);
            this.tabControl_botpages.Controls.Add(this.tabPage_soundalerts);
            this.tabControl_botpages.Controls.Add(this.tabPage_content_filter);
            this.tabControl_botpages.Controls.Add(this.tabPage_player_sorting);
            this.tabControl_botpages.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl_botpages.HotTrack = true;
            this.tabControl_botpages.ItemSize = new System.Drawing.Size(23, 100);
            this.tabControl_botpages.Location = new System.Drawing.Point(-1, 37);
            this.tabControl_botpages.Multiline = true;
            this.tabControl_botpages.Name = "tabControl_botpages";
            this.tabControl_botpages.SelectedIndex = 0;
            this.tabControl_botpages.Size = new System.Drawing.Size(623, 385);
            this.tabControl_botpages.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl_botpages.TabIndex = 2;
            this.tabControl_botpages.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl_botpages_DrawItem);
            // 
            // tabPage_party
            // 
            this.tabPage_party.Controls.Add(this.groupBox_RezSettings);
            this.tabPage_party.Controls.Add(this.groupBox_PartySettings);
            this.tabPage_party.Controls.Add(this.groupBox_BuffSettings1);
            this.tabPage_party.Controls.Add(this.groupBox_FollowSettings);
            this.tabPage_party.Location = new System.Drawing.Point(104, 4);
            this.tabPage_party.Name = "tabPage_party";
            this.tabPage_party.Size = new System.Drawing.Size(515, 377);
            this.tabPage_party.TabIndex = 0;
            this.tabPage_party.Text = "Party";
            this.tabPage_party.UseVisualStyleBackColor = true;
            // 
            // groupBox_RezSettings
            // 
            this.groupBox_RezSettings.Controls.Add(this.checkBox_accept_rez_Party);
            this.groupBox_RezSettings.Controls.Add(this.checkBox_accept_rez_alliance);
            this.groupBox_RezSettings.Controls.Add(this.checkBox_accept_rez_clan);
            this.groupBox_RezSettings.Controls.Add(this.checkBox_accept_rez);
            this.groupBox_RezSettings.Controls.Add(this.textBox_accept_rez);
            this.groupBox_RezSettings.Location = new System.Drawing.Point(3, 301);
            this.groupBox_RezSettings.Name = "groupBox_RezSettings";
            this.groupBox_RezSettings.Size = new System.Drawing.Size(507, 73);
            this.groupBox_RezSettings.TabIndex = 29;
            this.groupBox_RezSettings.TabStop = false;
            this.groupBox_RezSettings.Text = "Rez Settings";
            // 
            // checkBox_accept_rez_Party
            // 
            this.checkBox_accept_rez_Party.Location = new System.Drawing.Point(295, 45);
            this.checkBox_accept_rez_Party.Name = "checkBox_accept_rez_Party";
            this.checkBox_accept_rez_Party.Size = new System.Drawing.Size(181, 26);
            this.checkBox_accept_rez_Party.TabIndex = 31;
            this.checkBox_accept_rez_Party.Text = "Accept Rez from Party";
            // 
            // checkBox_accept_rez_alliance
            // 
            this.checkBox_accept_rez_alliance.Location = new System.Drawing.Point(142, 44);
            this.checkBox_accept_rez_alliance.Name = "checkBox_accept_rez_alliance";
            this.checkBox_accept_rez_alliance.Size = new System.Drawing.Size(181, 26);
            this.checkBox_accept_rez_alliance.TabIndex = 30;
            this.checkBox_accept_rez_alliance.Text = "Accept Rez from Alliance";
            // 
            // checkBox_accept_rez_clan
            // 
            this.checkBox_accept_rez_clan.Location = new System.Drawing.Point(7, 44);
            this.checkBox_accept_rez_clan.Name = "checkBox_accept_rez_clan";
            this.checkBox_accept_rez_clan.Size = new System.Drawing.Size(173, 26);
            this.checkBox_accept_rez_clan.TabIndex = 29;
            this.checkBox_accept_rez_clan.Text = "Accept Rez from Clan";
            // 
            // checkBox_accept_rez
            // 
            this.checkBox_accept_rez.Location = new System.Drawing.Point(7, 19);
            this.checkBox_accept_rez.Name = "checkBox_accept_rez";
            this.checkBox_accept_rez.Size = new System.Drawing.Size(112, 24);
            this.checkBox_accept_rez.TabIndex = 19;
            this.checkBox_accept_rez.Text = "Auto Accept Rez";
            // 
            // textBox_accept_rez
            // 
            this.textBox_accept_rez.Location = new System.Drawing.Point(125, 19);
            this.textBox_accept_rez.Name = "textBox_accept_rez";
            this.textBox_accept_rez.Size = new System.Drawing.Size(377, 20);
            this.textBox_accept_rez.TabIndex = 20;
            this.textBox_accept_rez.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox_PartySettings
            // 
            this.groupBox_PartySettings.Controls.Add(this.checkBox_accept_party_alliance);
            this.groupBox_PartySettings.Controls.Add(this.checkBox_accept_party_clan);
            this.groupBox_PartySettings.Controls.Add(this.checkBox_accept_party);
            this.groupBox_PartySettings.Controls.Add(this.textBox_accept_party);
            this.groupBox_PartySettings.Controls.Add(this.checkBox_drop_leader);
            this.groupBox_PartySettings.Controls.Add(this.checkBox_auto_invite);
            this.groupBox_PartySettings.Controls.Add(this.textBox_oop);
            this.groupBox_PartySettings.Controls.Add(this.comboBox_LootType);
            this.groupBox_PartySettings.Controls.Add(this.checkBox_oop);
            this.groupBox_PartySettings.Controls.Add(this.textBox_auto_invite);
            this.groupBox_PartySettings.Location = new System.Drawing.Point(3, 165);
            this.groupBox_PartySettings.Name = "groupBox_PartySettings";
            this.groupBox_PartySettings.Size = new System.Drawing.Size(507, 125);
            this.groupBox_PartySettings.TabIndex = 28;
            this.groupBox_PartySettings.TabStop = false;
            this.groupBox_PartySettings.Text = "Party Settings";
            // 
            // checkBox_accept_party_alliance
            // 
            this.checkBox_accept_party_alliance.Location = new System.Drawing.Point(141, 97);
            this.checkBox_accept_party_alliance.Name = "checkBox_accept_party_alliance";
            this.checkBox_accept_party_alliance.Size = new System.Drawing.Size(181, 26);
            this.checkBox_accept_party_alliance.TabIndex = 28;
            this.checkBox_accept_party_alliance.Text = "Accept Party from Alliance";
            // 
            // checkBox_accept_party_clan
            // 
            this.checkBox_accept_party_clan.Location = new System.Drawing.Point(6, 97);
            this.checkBox_accept_party_clan.Name = "checkBox_accept_party_clan";
            this.checkBox_accept_party_clan.Size = new System.Drawing.Size(173, 26);
            this.checkBox_accept_party_clan.TabIndex = 27;
            this.checkBox_accept_party_clan.Text = "Accept Party from Clan";
            // 
            // checkBox_accept_party
            // 
            this.checkBox_accept_party.Location = new System.Drawing.Point(6, 72);
            this.checkBox_accept_party.Name = "checkBox_accept_party";
            this.checkBox_accept_party.Size = new System.Drawing.Size(143, 24);
            this.checkBox_accept_party.TabIndex = 17;
            this.checkBox_accept_party.Text = "Auto Accept Party Invite";
            // 
            // textBox_accept_party
            // 
            this.textBox_accept_party.Location = new System.Drawing.Point(155, 74);
            this.textBox_accept_party.Name = "textBox_accept_party";
            this.textBox_accept_party.Size = new System.Drawing.Size(214, 20);
            this.textBox_accept_party.TabIndex = 18;
            this.textBox_accept_party.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkBox_drop_leader
            // 
            this.checkBox_drop_leader.Location = new System.Drawing.Point(379, 73);
            this.checkBox_drop_leader.Name = "checkBox_drop_leader";
            this.checkBox_drop_leader.Size = new System.Drawing.Size(143, 21);
            this.checkBox_drop_leader.TabIndex = 26;
            this.checkBox_drop_leader.Text = "Drop Party if Leader";
            // 
            // checkBox_auto_invite
            // 
            this.checkBox_auto_invite.Location = new System.Drawing.Point(6, 49);
            this.checkBox_auto_invite.Name = "checkBox_auto_invite";
            this.checkBox_auto_invite.Size = new System.Drawing.Size(143, 24);
            this.checkBox_auto_invite.TabIndex = 21;
            this.checkBox_auto_invite.Text = "Auto Send Party Invite";
            // 
            // textBox_oop
            // 
            this.textBox_oop.Location = new System.Drawing.Point(155, 23);
            this.textBox_oop.Name = "textBox_oop";
            this.textBox_oop.Size = new System.Drawing.Size(346, 20);
            this.textBox_oop.TabIndex = 24;
            this.textBox_oop.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBox_LootType
            // 
            this.comboBox_LootType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_LootType.FormattingEnabled = true;
            this.comboBox_LootType.Items.AddRange(new object[] {
            "Finders Keepers",
            "Random",
            "Random Including Spoil",
            "By Turn",
            "By Turn Including Spoil"});
            this.comboBox_LootType.Location = new System.Drawing.Point(375, 48);
            this.comboBox_LootType.Name = "comboBox_LootType";
            this.comboBox_LootType.Size = new System.Drawing.Size(126, 21);
            this.comboBox_LootType.TabIndex = 25;
            // 
            // checkBox_oop
            // 
            this.checkBox_oop.Location = new System.Drawing.Point(6, 23);
            this.checkBox_oop.Name = "checkBox_oop";
            this.checkBox_oop.Size = new System.Drawing.Size(143, 24);
            this.checkBox_oop.TabIndex = 23;
            this.checkBox_oop.Text = "OOP Members";
            // 
            // textBox_auto_invite
            // 
            this.textBox_auto_invite.Location = new System.Drawing.Point(155, 49);
            this.textBox_auto_invite.Name = "textBox_auto_invite";
            this.textBox_auto_invite.Size = new System.Drawing.Size(214, 20);
            this.textBox_auto_invite.TabIndex = 22;
            // 
            // groupBox_BuffSettings1
            // 
            this.groupBox_BuffSettings1.Controls.Add(this.textBox_buffrange);
            this.groupBox_BuffSettings1.Controls.Add(this.label_buffrange);
            this.groupBox_BuffSettings1.Controls.Add(this.checkBox_buff_control);
            this.groupBox_BuffSettings1.Controls.Add(this.checkBox_buff_shift);
            this.groupBox_BuffSettings1.Location = new System.Drawing.Point(3, 106);
            this.groupBox_BuffSettings1.Name = "groupBox_BuffSettings1";
            this.groupBox_BuffSettings1.Size = new System.Drawing.Size(376, 49);
            this.groupBox_BuffSettings1.TabIndex = 27;
            this.groupBox_BuffSettings1.TabStop = false;
            this.groupBox_BuffSettings1.Text = "Buff Settings";
            // 
            // textBox_buffrange
            // 
            this.textBox_buffrange.Location = new System.Drawing.Point(6, 19);
            this.textBox_buffrange.Name = "textBox_buffrange";
            this.textBox_buffrange.Size = new System.Drawing.Size(47, 20);
            this.textBox_buffrange.TabIndex = 14;
            this.textBox_buffrange.Text = "550";
            this.textBox_buffrange.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_buffrange
            // 
            this.label_buffrange.Location = new System.Drawing.Point(55, 19);
            this.label_buffrange.Name = "label_buffrange";
            this.label_buffrange.Size = new System.Drawing.Size(102, 24);
            this.label_buffrange.TabIndex = 10;
            this.label_buffrange.Text = "Buff/Heal Range";
            this.label_buffrange.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBox_buff_control
            // 
            this.checkBox_buff_control.Location = new System.Drawing.Point(257, 18);
            this.checkBox_buff_control.Name = "checkBox_buff_control";
            this.checkBox_buff_control.Size = new System.Drawing.Size(103, 24);
            this.checkBox_buff_control.TabIndex = 15;
            this.checkBox_buff_control.Text = "Buff with Control";
            // 
            // checkBox_buff_shift
            // 
            this.checkBox_buff_shift.Location = new System.Drawing.Point(158, 19);
            this.checkBox_buff_shift.Name = "checkBox_buff_shift";
            this.checkBox_buff_shift.Size = new System.Drawing.Size(102, 24);
            this.checkBox_buff_shift.TabIndex = 16;
            this.checkBox_buff_shift.Text = "Buff with Shift";
            // 
            // groupBox_FollowSettings
            // 
            this.groupBox_FollowSettings.Controls.Add(this.checkBox_activefollow_attack_Instant);
            this.groupBox_FollowSettings.Controls.Add(this.checkBox_activefollow);
            this.groupBox_FollowSettings.Controls.Add(this.textBox_activefollow_name);
            this.groupBox_FollowSettings.Controls.Add(this.label_followname);
            this.groupBox_FollowSettings.Controls.Add(this.radioButton_ActiveFollow_style1);
            this.groupBox_FollowSettings.Controls.Add(this.radioButton_ActiveFollow_style2);
            this.groupBox_FollowSettings.Controls.Add(this.textBox_ActiveFollow_Dist);
            this.groupBox_FollowSettings.Controls.Add(this.label_followdistance);
            this.groupBox_FollowSettings.Controls.Add(this.checkBox_activefollow_attack);
            this.groupBox_FollowSettings.Controls.Add(this.checkBox_activefollow_target);
            this.groupBox_FollowSettings.Location = new System.Drawing.Point(3, 3);
            this.groupBox_FollowSettings.Name = "groupBox_FollowSettings";
            this.groupBox_FollowSettings.Size = new System.Drawing.Size(376, 100);
            this.groupBox_FollowSettings.TabIndex = 26;
            this.groupBox_FollowSettings.TabStop = false;
            this.groupBox_FollowSettings.Text = "Follow Settings";
            // 
            // checkBox_activefollow_attack_Instant
            // 
            this.checkBox_activefollow_attack_Instant.AutoSize = true;
            this.checkBox_activefollow_attack_Instant.Enabled = false;
            this.checkBox_activefollow_attack_Instant.Location = new System.Drawing.Point(277, 74);
            this.checkBox_activefollow_attack_Instant.Name = "checkBox_activefollow_attack_Instant";
            this.checkBox_activefollow_attack_Instant.Size = new System.Drawing.Size(92, 17);
            this.checkBox_activefollow_attack_Instant.TabIndex = 11;
            this.checkBox_activefollow_attack_Instant.Text = "Instant Attack";
            this.toolTip_Instant_attack.SetToolTip(this.checkBox_activefollow_attack_Instant, "Attacks instantly when char got a target. Both active follow attack and active fo" +
                    "llow target needs to be checked for this to work");
            this.checkBox_activefollow_attack_Instant.UseVisualStyleBackColor = true;
            // 
            // checkBox_activefollow
            // 
            this.checkBox_activefollow.Location = new System.Drawing.Point(6, 19);
            this.checkBox_activefollow.Name = "checkBox_activefollow";
            this.checkBox_activefollow.Size = new System.Drawing.Size(90, 24);
            this.checkBox_activefollow.TabIndex = 0;
            this.checkBox_activefollow.Text = "Active Follow";
            // 
            // textBox_activefollow_name
            // 
            this.textBox_activefollow_name.Location = new System.Drawing.Point(96, 19);
            this.textBox_activefollow_name.Name = "textBox_activefollow_name";
            this.textBox_activefollow_name.Size = new System.Drawing.Size(175, 20);
            this.textBox_activefollow_name.TabIndex = 3;
            this.textBox_activefollow_name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_followname
            // 
            this.label_followname.Location = new System.Drawing.Point(277, 19);
            this.label_followname.Name = "label_followname";
            this.label_followname.Size = new System.Drawing.Size(58, 24);
            this.label_followname.TabIndex = 5;
            this.label_followname.Text = "Name";
            this.label_followname.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // radioButton_ActiveFollow_style1
            // 
            this.radioButton_ActiveFollow_style1.Checked = true;
            this.radioButton_ActiveFollow_style1.Location = new System.Drawing.Point(6, 44);
            this.radioButton_ActiveFollow_style1.Name = "radioButton_ActiveFollow_style1";
            this.radioButton_ActiveFollow_style1.Size = new System.Drawing.Size(90, 24);
            this.radioButton_ActiveFollow_style1.TabIndex = 1;
            this.radioButton_ActiveFollow_style1.TabStop = true;
            this.radioButton_ActiveFollow_style1.Text = "Walker Style";
            // 
            // radioButton_ActiveFollow_style2
            // 
            this.radioButton_ActiveFollow_style2.Location = new System.Drawing.Point(96, 44);
            this.radioButton_ActiveFollow_style2.Name = "radioButton_ActiveFollow_style2";
            this.radioButton_ActiveFollow_style2.Size = new System.Drawing.Size(83, 24);
            this.radioButton_ActiveFollow_style2.TabIndex = 2;
            this.radioButton_ActiveFollow_style2.Text = "L2.Net Style";
            // 
            // textBox_ActiveFollow_Dist
            // 
            this.textBox_ActiveFollow_Dist.Location = new System.Drawing.Point(185, 44);
            this.textBox_ActiveFollow_Dist.Name = "textBox_ActiveFollow_Dist";
            this.textBox_ActiveFollow_Dist.Size = new System.Drawing.Size(86, 20);
            this.textBox_ActiveFollow_Dist.TabIndex = 4;
            this.textBox_ActiveFollow_Dist.Text = "150";
            this.textBox_ActiveFollow_Dist.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_followdistance
            // 
            this.label_followdistance.Location = new System.Drawing.Point(277, 44);
            this.label_followdistance.Name = "label_followdistance";
            this.label_followdistance.Size = new System.Drawing.Size(49, 24);
            this.label_followdistance.TabIndex = 4;
            this.label_followdistance.Text = "Distance";
            this.label_followdistance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBox_activefollow_attack
            // 
            this.checkBox_activefollow_attack.Location = new System.Drawing.Point(6, 70);
            this.checkBox_activefollow_attack.Name = "checkBox_activefollow_attack";
            this.checkBox_activefollow_attack.Size = new System.Drawing.Size(124, 24);
            this.checkBox_activefollow_attack.TabIndex = 9;
            this.checkBox_activefollow_attack.Text = "Active Follow Attack";
            this.toolTip_Instant_attack.SetToolTip(this.checkBox_activefollow_attack, "Starts attacking when leader is killing a target. To attack instantly you need to" +
                    " enable follow attack and follow target");
            this.checkBox_activefollow_attack.CheckedChanged += new System.EventHandler(this.checkBox_activefollow_attack_CheckedChanged);
            // 
            // checkBox_activefollow_target
            // 
            this.checkBox_activefollow_target.Location = new System.Drawing.Point(136, 70);
            this.checkBox_activefollow_target.Name = "checkBox_activefollow_target";
            this.checkBox_activefollow_target.Size = new System.Drawing.Size(135, 24);
            this.checkBox_activefollow_target.TabIndex = 10;
            this.checkBox_activefollow_target.Text = "Active Follow Target";
            this.toolTip_Instant_attack.SetToolTip(this.checkBox_activefollow_target, "Follow leader targeting. Follow target and follow attack needs to be enabled to e" +
                    "nable instant attack.");
            this.checkBox_activefollow_target.CheckedChanged += new System.EventHandler(this.checkBox_activefollow_target_CheckedChanged);
            // 
            // tabPage_autofighter
            // 
            this.tabPage_autofighter.Controls.Add(this.groupBox_PickupSettings);
            this.tabPage_autofighter.Controls.Add(this.groupBox6);
            this.tabPage_autofighter.Controls.Add(this.groupBox_Pet);
            this.tabPage_autofighter.Controls.Add(this.groupBox_DeadSettings);
            this.tabPage_autofighter.Controls.Add(this.groupBox_StuckSettings);
            this.tabPage_autofighter.Controls.Add(this.groupBox_AttackSettings);
            this.tabPage_autofighter.Controls.Add(this.groupBox_SpoilSettings);
            this.tabPage_autofighter.Controls.Add(this.groupBox_TargetSettings);
            this.tabPage_autofighter.Location = new System.Drawing.Point(104, 4);
            this.tabPage_autofighter.Name = "tabPage_autofighter";
            this.tabPage_autofighter.Size = new System.Drawing.Size(515, 377);
            this.tabPage_autofighter.TabIndex = 7;
            this.tabPage_autofighter.Text = "Autofighter";
            this.tabPage_autofighter.UseVisualStyleBackColor = true;
            // 
            // groupBox_PickupSettings
            // 
            this.groupBox_PickupSettings.Controls.Add(this.checkBox_PickupAfterAttack);
            this.groupBox_PickupSettings.Controls.Add(this.checkBox_pickup);
            this.groupBox_PickupSettings.Controls.Add(this.textBox_pickup_range);
            this.groupBox_PickupSettings.Controls.Add(this.label_pickup_range);
            this.groupBox_PickupSettings.Controls.Add(this.checkBox_OnlyPickMine);
            this.groupBox_PickupSettings.Location = new System.Drawing.Point(229, 78);
            this.groupBox_PickupSettings.Name = "groupBox_PickupSettings";
            this.groupBox_PickupSettings.Size = new System.Drawing.Size(284, 70);
            this.groupBox_PickupSettings.TabIndex = 45;
            this.groupBox_PickupSettings.TabStop = false;
            this.groupBox_PickupSettings.Text = "Pickup Settings";
            // 
            // checkBox_PickupAfterAttack
            // 
            this.checkBox_PickupAfterAttack.Location = new System.Drawing.Point(101, 17);
            this.checkBox_PickupAfterAttack.Name = "checkBox_PickupAfterAttack";
            this.checkBox_PickupAfterAttack.Size = new System.Drawing.Size(120, 24);
            this.checkBox_PickupAfterAttack.TabIndex = 33;
            this.checkBox_PickupAfterAttack.Text = "Pickup After Attack";
            // 
            // checkBox_pickup
            // 
            this.checkBox_pickup.Location = new System.Drawing.Point(6, 17);
            this.checkBox_pickup.Name = "checkBox_pickup";
            this.checkBox_pickup.Size = new System.Drawing.Size(64, 24);
            this.checkBox_pickup.TabIndex = 22;
            this.checkBox_pickup.Text = "Pickup";
            // 
            // textBox_pickup_range
            // 
            this.textBox_pickup_range.Location = new System.Drawing.Point(102, 41);
            this.textBox_pickup_range.Name = "textBox_pickup_range";
            this.textBox_pickup_range.Size = new System.Drawing.Size(60, 20);
            this.textBox_pickup_range.TabIndex = 23;
            this.textBox_pickup_range.Text = "250";
            this.textBox_pickup_range.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_pickup_range
            // 
            this.label_pickup_range.Location = new System.Drawing.Point(162, 38);
            this.label_pickup_range.Name = "label_pickup_range";
            this.label_pickup_range.Size = new System.Drawing.Size(77, 24);
            this.label_pickup_range.TabIndex = 24;
            this.label_pickup_range.Text = "Pickup Range";
            this.label_pickup_range.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBox_OnlyPickMine
            // 
            this.checkBox_OnlyPickMine.Checked = true;
            this.checkBox_OnlyPickMine.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_OnlyPickMine.Location = new System.Drawing.Point(6, 38);
            this.checkBox_OnlyPickMine.Name = "checkBox_OnlyPickMine";
            this.checkBox_OnlyPickMine.Size = new System.Drawing.Size(108, 24);
            this.checkBox_OnlyPickMine.TabIndex = 32;
            this.checkBox_OnlyPickMine.Text = "Only Pick Mine";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label31);
            this.groupBox6.Controls.Add(this.textBox_MoveToLeash);
            this.groupBox6.Controls.Add(this.checkBox_MoveToLoc);
            this.groupBox6.Controls.Add(this.checkBox_OutOfCombat);
            this.groupBox6.Controls.Add(this.label30);
            this.groupBox6.Controls.Add(this.label29);
            this.groupBox6.Controls.Add(this.label28);
            this.groupBox6.Controls.Add(this.textBox_Moveto_Z);
            this.groupBox6.Controls.Add(this.textBox_Moveto_X);
            this.groupBox6.Controls.Add(this.textBox_Moveto_Y);
            this.groupBox6.Controls.Add(this.Set_CurrentXYZ);
            this.groupBox6.Controls.Add(this.checkBox_active_move_first);
            this.groupBox6.Location = new System.Drawing.Point(6, 145);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(223, 226);
            this.groupBox6.TabIndex = 44;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "When No Mobs";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(1, 70);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(39, 13);
            this.label31.TabIndex = 48;
            this.label31.Text = "Leash:";
            // 
            // textBox_MoveToLeash
            // 
            this.textBox_MoveToLeash.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_MoveToLeash.Location = new System.Drawing.Point(45, 68);
            this.textBox_MoveToLeash.Name = "textBox_MoveToLeash";
            this.textBox_MoveToLeash.Size = new System.Drawing.Size(51, 20);
            this.textBox_MoveToLeash.TabIndex = 47;
            this.textBox_MoveToLeash.Text = "100";
            // 
            // checkBox_MoveToLoc
            // 
            this.checkBox_MoveToLoc.AutoSize = true;
            this.checkBox_MoveToLoc.Location = new System.Drawing.Point(4, 19);
            this.checkBox_MoveToLoc.Name = "checkBox_MoveToLoc";
            this.checkBox_MoveToLoc.Size = new System.Drawing.Size(86, 17);
            this.checkBox_MoveToLoc.TabIndex = 46;
            this.checkBox_MoveToLoc.Text = "Move to Loc";
            this.checkBox_MoveToLoc.UseVisualStyleBackColor = true;
            // 
            // checkBox_OutOfCombat
            // 
            this.checkBox_OutOfCombat.AutoSize = true;
            this.checkBox_OutOfCombat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.checkBox_OutOfCombat.Location = new System.Drawing.Point(90, 20);
            this.checkBox_OutOfCombat.Name = "checkBox_OutOfCombat";
            this.checkBox_OutOfCombat.Size = new System.Drawing.Size(94, 17);
            this.checkBox_OutOfCombat.TabIndex = 42;
            this.checkBox_OutOfCombat.Text = "Out of Combat";
            this.checkBox_OutOfCombat.UseVisualStyleBackColor = true;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(4, 45);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(17, 13);
            this.label30.TabIndex = 45;
            this.label30.Text = "X:";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(75, 45);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(17, 13);
            this.label29.TabIndex = 44;
            this.label29.Text = "Y:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(149, 45);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(17, 13);
            this.label28.TabIndex = 43;
            this.label28.Text = "Z:";
            // 
            // textBox_Moveto_Z
            // 
            this.textBox_Moveto_Z.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Moveto_Z.Location = new System.Drawing.Point(169, 43);
            this.textBox_Moveto_Z.Name = "textBox_Moveto_Z";
            this.textBox_Moveto_Z.Size = new System.Drawing.Size(40, 18);
            this.textBox_Moveto_Z.TabIndex = 42;
            this.textBox_Moveto_Z.Text = "0";
            // 
            // textBox_Moveto_X
            // 
            this.textBox_Moveto_X.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Moveto_X.Location = new System.Drawing.Point(22, 43);
            this.textBox_Moveto_X.Name = "textBox_Moveto_X";
            this.textBox_Moveto_X.Size = new System.Drawing.Size(51, 18);
            this.textBox_Moveto_X.TabIndex = 41;
            this.textBox_Moveto_X.Text = "0";
            // 
            // textBox_Moveto_Y
            // 
            this.textBox_Moveto_Y.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_Moveto_Y.Location = new System.Drawing.Point(93, 43);
            this.textBox_Moveto_Y.Name = "textBox_Moveto_Y";
            this.textBox_Moveto_Y.Size = new System.Drawing.Size(54, 18);
            this.textBox_Moveto_Y.TabIndex = 40;
            this.textBox_Moveto_Y.Text = "0";
            // 
            // Set_CurrentXYZ
            // 
            this.Set_CurrentXYZ.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Set_CurrentXYZ.Location = new System.Drawing.Point(183, 17);
            this.Set_CurrentXYZ.Name = "Set_CurrentXYZ";
            this.Set_CurrentXYZ.Size = new System.Drawing.Size(35, 22);
            this.Set_CurrentXYZ.TabIndex = 29;
            this.Set_CurrentXYZ.Text = "SET";
            this.Set_CurrentXYZ.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.Set_CurrentXYZ.UseVisualStyleBackColor = true;
            this.Set_CurrentXYZ.Click += new System.EventHandler(this.Set_CurrentXYZ_Click);
            // 
            // checkBox_active_move_first
            // 
            this.checkBox_active_move_first.Enabled = false;
            this.checkBox_active_move_first.Location = new System.Drawing.Point(7, 196);
            this.checkBox_active_move_first.Name = "checkBox_active_move_first";
            this.checkBox_active_move_first.Size = new System.Drawing.Size(155, 24);
            this.checkBox_active_move_first.TabIndex = 28;
            this.checkBox_active_move_first.Text = "Smart Move Before Attack";
            this.checkBox_active_move_first.Visible = false;
            this.checkBox_active_move_first.CheckedChanged += new System.EventHandler(this.checkBox_active_move_first_CheckedChanged);
            // 
            // groupBox_Pet
            // 
            this.groupBox_Pet.Controls.Add(this.checkBox_summon_instantattack);
            this.groupBox_Pet.Controls.Add(this.checkBox_pet_soloattack);
            this.groupBox_Pet.Controls.Add(this.checkBox_Summon_autoassist);
            this.groupBox_Pet.Controls.Add(this.checkBox_pet_autoassist);
            this.groupBox_Pet.Location = new System.Drawing.Point(229, 251);
            this.groupBox_Pet.Name = "groupBox_Pet";
            this.groupBox_Pet.Size = new System.Drawing.Size(281, 62);
            this.groupBox_Pet.TabIndex = 48;
            this.groupBox_Pet.TabStop = false;
            this.groupBox_Pet.Text = "Pet Settings";
            // 
            // checkBox_summon_instantattack
            // 
            this.checkBox_summon_instantattack.AutoSize = true;
            this.checkBox_summon_instantattack.Location = new System.Drawing.Point(137, 38);
            this.checkBox_summon_instantattack.Name = "checkBox_summon_instantattack";
            this.checkBox_summon_instantattack.Size = new System.Drawing.Size(92, 17);
            this.checkBox_summon_instantattack.TabIndex = 3;
            this.checkBox_summon_instantattack.Text = "Instant Attack";
            this.checkBox_summon_instantattack.UseVisualStyleBackColor = true;
            // 
            // checkBox_pet_soloattack
            // 
            this.checkBox_pet_soloattack.AutoSize = true;
            this.checkBox_pet_soloattack.Location = new System.Drawing.Point(6, 38);
            this.checkBox_pet_soloattack.Name = "checkBox_pet_soloattack";
            this.checkBox_pet_soloattack.Size = new System.Drawing.Size(81, 17);
            this.checkBox_pet_soloattack.TabIndex = 2;
            this.checkBox_pet_soloattack.Text = "Attack Solo";
            this.checkBox_pet_soloattack.UseVisualStyleBackColor = true;
            // 
            // checkBox_Summon_autoassist
            // 
            this.checkBox_Summon_autoassist.AutoSize = true;
            this.checkBox_Summon_autoassist.Location = new System.Drawing.Point(137, 19);
            this.checkBox_Summon_autoassist.Name = "checkBox_Summon_autoassist";
            this.checkBox_Summon_autoassist.Size = new System.Drawing.Size(122, 17);
            this.checkBox_Summon_autoassist.TabIndex = 1;
            this.checkBox_Summon_autoassist.Text = "Summon Auto Assist";
            this.checkBox_Summon_autoassist.UseVisualStyleBackColor = true;
            // 
            // checkBox_pet_autoassist
            // 
            this.checkBox_pet_autoassist.AutoSize = true;
            this.checkBox_pet_autoassist.Location = new System.Drawing.Point(6, 19);
            this.checkBox_pet_autoassist.Name = "checkBox_pet_autoassist";
            this.checkBox_pet_autoassist.Size = new System.Drawing.Size(97, 17);
            this.checkBox_pet_autoassist.TabIndex = 0;
            this.checkBox_pet_autoassist.Text = "Pet Auto Assist";
            this.checkBox_pet_autoassist.UseVisualStyleBackColor = true;
            // 
            // groupBox_DeadSettings
            // 
            this.groupBox_DeadSettings.Controls.Add(this.checkBox_DeadToggleBotting);
            this.groupBox_DeadSettings.Controls.Add(this.checkBox_DeadReturn);
            this.groupBox_DeadSettings.Controls.Add(this.textBox_DeadReturnDelay);
            this.groupBox_DeadSettings.Controls.Add(this.checkBox_DeadLogOut);
            this.groupBox_DeadSettings.Controls.Add(this.textBox_DeadLogOutDelay);
            this.groupBox_DeadSettings.Controls.Add(this.comboBox_DeadReturn);
            this.groupBox_DeadSettings.Location = new System.Drawing.Point(229, 146);
            this.groupBox_DeadSettings.Name = "groupBox_DeadSettings";
            this.groupBox_DeadSettings.Size = new System.Drawing.Size(281, 106);
            this.groupBox_DeadSettings.TabIndex = 47;
            this.groupBox_DeadSettings.TabStop = false;
            this.groupBox_DeadSettings.Text = "Dead Settings";
            // 
            // checkBox_DeadToggleBotting
            // 
            this.checkBox_DeadToggleBotting.AutoSize = true;
            this.checkBox_DeadToggleBotting.Location = new System.Drawing.Point(6, 19);
            this.checkBox_DeadToggleBotting.Name = "checkBox_DeadToggleBotting";
            this.checkBox_DeadToggleBotting.Size = new System.Drawing.Size(124, 17);
            this.checkBox_DeadToggleBotting.TabIndex = 41;
            this.checkBox_DeadToggleBotting.Text = "Dead Toggle Botting";
            this.checkBox_DeadToggleBotting.UseVisualStyleBackColor = true;
            // 
            // checkBox_DeadReturn
            // 
            this.checkBox_DeadReturn.AutoSize = true;
            this.checkBox_DeadReturn.Location = new System.Drawing.Point(6, 42);
            this.checkBox_DeadReturn.Name = "checkBox_DeadReturn";
            this.checkBox_DeadReturn.Size = new System.Drawing.Size(132, 17);
            this.checkBox_DeadReturn.TabIndex = 37;
            this.checkBox_DeadReturn.Text = "Dead Return    Delay: ";
            this.checkBox_DeadReturn.UseVisualStyleBackColor = true;
            // 
            // textBox_DeadReturnDelay
            // 
            this.textBox_DeadReturnDelay.Location = new System.Drawing.Point(142, 39);
            this.textBox_DeadReturnDelay.Name = "textBox_DeadReturnDelay";
            this.textBox_DeadReturnDelay.Size = new System.Drawing.Size(50, 20);
            this.textBox_DeadReturnDelay.TabIndex = 39;
            this.textBox_DeadReturnDelay.Text = "10";
            this.textBox_DeadReturnDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_DeadReturnDelay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_DeadReturnDelay_KeyPress);
            // 
            // checkBox_DeadLogOut
            // 
            this.checkBox_DeadLogOut.AutoSize = true;
            this.checkBox_DeadLogOut.Location = new System.Drawing.Point(6, 65);
            this.checkBox_DeadLogOut.Name = "checkBox_DeadLogOut";
            this.checkBox_DeadLogOut.Size = new System.Drawing.Size(130, 17);
            this.checkBox_DeadLogOut.TabIndex = 36;
            this.checkBox_DeadLogOut.Text = "Dead Logout    Delay:";
            this.checkBox_DeadLogOut.UseVisualStyleBackColor = true;
            // 
            // textBox_DeadLogOutDelay
            // 
            this.textBox_DeadLogOutDelay.Location = new System.Drawing.Point(142, 65);
            this.textBox_DeadLogOutDelay.Name = "textBox_DeadLogOutDelay";
            this.textBox_DeadLogOutDelay.Size = new System.Drawing.Size(50, 20);
            this.textBox_DeadLogOutDelay.TabIndex = 38;
            this.textBox_DeadLogOutDelay.Text = "30";
            this.textBox_DeadLogOutDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_DeadLogOutDelay.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_DeadLogOutDelay_KeyPress);
            // 
            // comboBox_DeadReturn
            // 
            this.comboBox_DeadReturn.FormattingEnabled = true;
            this.comboBox_DeadReturn.Items.AddRange(new object[] {
            "Town",
            "Clan Hall",
            "Castle",
            "Siege HQ",
            "Fortress"});
            this.comboBox_DeadReturn.Location = new System.Drawing.Point(198, 39);
            this.comboBox_DeadReturn.Name = "comboBox_DeadReturn";
            this.comboBox_DeadReturn.Size = new System.Drawing.Size(83, 21);
            this.comboBox_DeadReturn.TabIndex = 40;
            // 
            // groupBox_StuckSettings
            // 
            this.groupBox_StuckSettings.Controls.Add(this.checkBox_StuckCheck);
            this.groupBox_StuckSettings.Controls.Add(this.checkBox_AutoBlacklist);
            this.groupBox_StuckSettings.Location = new System.Drawing.Point(229, 314);
            this.groupBox_StuckSettings.Name = "groupBox_StuckSettings";
            this.groupBox_StuckSettings.Size = new System.Drawing.Size(283, 57);
            this.groupBox_StuckSettings.TabIndex = 46;
            this.groupBox_StuckSettings.TabStop = false;
            this.groupBox_StuckSettings.Text = "Stuck Settings";
            // 
            // checkBox_StuckCheck
            // 
            this.checkBox_StuckCheck.Location = new System.Drawing.Point(112, 19);
            this.checkBox_StuckCheck.Name = "checkBox_StuckCheck";
            this.checkBox_StuckCheck.Size = new System.Drawing.Size(100, 24);
            this.checkBox_StuckCheck.TabIndex = 31;
            this.checkBox_StuckCheck.Text = "Auto Unstuck";
            // 
            // checkBox_AutoBlacklist
            // 
            this.checkBox_AutoBlacklist.Location = new System.Drawing.Point(6, 19);
            this.checkBox_AutoBlacklist.Name = "checkBox_AutoBlacklist";
            this.checkBox_AutoBlacklist.Size = new System.Drawing.Size(100, 24);
            this.checkBox_AutoBlacklist.TabIndex = 34;
            this.checkBox_AutoBlacklist.Text = "Auto Blacklist";
            // 
            // groupBox_AttackSettings
            // 
            this.groupBox_AttackSettings.Controls.Add(this.checkBox_cancel_target);
            this.groupBox_AttackSettings.Controls.Add(this.checkBox_active_move_first_normal);
            this.groupBox_AttackSettings.Controls.Add(this.label_active_move_range);
            this.groupBox_AttackSettings.Controls.Add(this.textBox_active_move_range);
            this.groupBox_AttackSettings.Controls.Add(this.checkBox_active_attack);
            this.groupBox_AttackSettings.Location = new System.Drawing.Point(4, 56);
            this.groupBox_AttackSettings.Name = "groupBox_AttackSettings";
            this.groupBox_AttackSettings.Size = new System.Drawing.Size(225, 91);
            this.groupBox_AttackSettings.TabIndex = 43;
            this.groupBox_AttackSettings.TabStop = false;
            this.groupBox_AttackSettings.Text = "Attack Settings";
            // 
            // checkBox_cancel_target
            // 
            this.checkBox_cancel_target.Location = new System.Drawing.Point(6, 39);
            this.checkBox_cancel_target.Name = "checkBox_cancel_target";
            this.checkBox_cancel_target.Size = new System.Drawing.Size(144, 24);
            this.checkBox_cancel_target.TabIndex = 38;
            this.checkBox_cancel_target.Text = "Cancel Target on Dead";
            // 
            // checkBox_active_move_first_normal
            // 
            this.checkBox_active_move_first_normal.Location = new System.Drawing.Point(92, 19);
            this.checkBox_active_move_first_normal.Name = "checkBox_active_move_first_normal";
            this.checkBox_active_move_first_normal.Size = new System.Drawing.Size(124, 24);
            this.checkBox_active_move_first_normal.TabIndex = 37;
            this.checkBox_active_move_first_normal.Text = "Move Before Attack";
            this.checkBox_active_move_first_normal.CheckedChanged += new System.EventHandler(this.checkBox_active_move_first_normal_CheckedChanged);
            // 
            // label_active_move_range
            // 
            this.label_active_move_range.Location = new System.Drawing.Point(68, 60);
            this.label_active_move_range.Name = "label_active_move_range";
            this.label_active_move_range.Size = new System.Drawing.Size(75, 24);
            this.label_active_move_range.TabIndex = 36;
            this.label_active_move_range.Text = "Move Range";
            this.label_active_move_range.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox_active_move_range
            // 
            this.textBox_active_move_range.Location = new System.Drawing.Point(6, 63);
            this.textBox_active_move_range.Name = "textBox_active_move_range";
            this.textBox_active_move_range.Size = new System.Drawing.Size(60, 20);
            this.textBox_active_move_range.TabIndex = 35;
            this.textBox_active_move_range.Text = "150";
            this.textBox_active_move_range.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkBox_active_attack
            // 
            this.checkBox_active_attack.Location = new System.Drawing.Point(6, 19);
            this.checkBox_active_attack.Name = "checkBox_active_attack";
            this.checkBox_active_attack.Size = new System.Drawing.Size(92, 24);
            this.checkBox_active_attack.TabIndex = 34;
            this.checkBox_active_attack.Text = "Active Attack";
            this.checkBox_active_attack.CheckedChanged += new System.EventHandler(this.checkBox_active_attack_CheckedChanged);
            // 
            // groupBox_SpoilSettings
            // 
            this.groupBox_SpoilSettings.Controls.Add(this.checkBox_use_plunder);
            this.groupBox_SpoilSettings.Controls.Add(this.label_SpoilMP);
            this.groupBox_SpoilSettings.Controls.Add(this.textBox_spoil_mp);
            this.groupBox_SpoilSettings.Controls.Add(this.checkBox_UntilSuccess);
            this.groupBox_SpoilSettings.Controls.Add(this.checkBox_spoilcrush);
            this.groupBox_SpoilSettings.Controls.Add(this.checkBox_autospoil);
            this.groupBox_SpoilSettings.Controls.Add(this.checkBox_autosweep);
            this.groupBox_SpoilSettings.Location = new System.Drawing.Point(229, 3);
            this.groupBox_SpoilSettings.Name = "groupBox_SpoilSettings";
            this.groupBox_SpoilSettings.Size = new System.Drawing.Size(284, 70);
            this.groupBox_SpoilSettings.TabIndex = 44;
            this.groupBox_SpoilSettings.TabStop = false;
            this.groupBox_SpoilSettings.Text = "Spoil Settings";
            // 
            // checkBox_use_plunder
            // 
            this.checkBox_use_plunder.Checked = true;
            this.checkBox_use_plunder.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_use_plunder.Location = new System.Drawing.Point(106, 40);
            this.checkBox_use_plunder.Name = "checkBox_use_plunder";
            this.checkBox_use_plunder.Size = new System.Drawing.Size(77, 24);
            this.checkBox_use_plunder.TabIndex = 34;
            this.checkBox_use_plunder.Text = "Plunder";
            // 
            // label_SpoilMP
            // 
            this.label_SpoilMP.AutoSize = true;
            this.label_SpoilMP.Location = new System.Drawing.Point(207, 24);
            this.label_SpoilMP.Name = "label_SpoilMP";
            this.label_SpoilMP.Size = new System.Drawing.Size(32, 13);
            this.label_SpoilMP.TabIndex = 33;
            this.label_SpoilMP.Text = "MP >";
            // 
            // textBox_spoil_mp
            // 
            this.textBox_spoil_mp.Location = new System.Drawing.Point(245, 21);
            this.textBox_spoil_mp.Name = "textBox_spoil_mp";
            this.textBox_spoil_mp.Size = new System.Drawing.Size(30, 20);
            this.textBox_spoil_mp.TabIndex = 32;
            this.textBox_spoil_mp.Text = "50";
            this.textBox_spoil_mp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_spoil_mp_KeyPress);
            // 
            // checkBox_UntilSuccess
            // 
            this.checkBox_UntilSuccess.Location = new System.Drawing.Point(119, 19);
            this.checkBox_UntilSuccess.Name = "checkBox_UntilSuccess";
            this.checkBox_UntilSuccess.Size = new System.Drawing.Size(140, 24);
            this.checkBox_UntilSuccess.TabIndex = 31;
            this.checkBox_UntilSuccess.Text = "Until Success";
            // 
            // checkBox_spoilcrush
            // 
            this.checkBox_spoilcrush.Location = new System.Drawing.Point(6, 40);
            this.checkBox_spoilcrush.Name = "checkBox_spoilcrush";
            this.checkBox_spoilcrush.Size = new System.Drawing.Size(107, 24);
            this.checkBox_spoilcrush.TabIndex = 29;
            this.checkBox_spoilcrush.Text = "Use Spoil Crush";
            // 
            // checkBox_autospoil
            // 
            this.checkBox_autospoil.Location = new System.Drawing.Point(6, 19);
            this.checkBox_autospoil.Name = "checkBox_autospoil";
            this.checkBox_autospoil.Size = new System.Drawing.Size(75, 24);
            this.checkBox_autospoil.TabIndex = 28;
            this.checkBox_autospoil.Text = "Auto Spoil";
            this.checkBox_autospoil.CheckedChanged += new System.EventHandler(this.checkBox_autospoil_CheckedChanged);
            // 
            // checkBox_autosweep
            // 
            this.checkBox_autosweep.Enabled = false;
            this.checkBox_autosweep.Location = new System.Drawing.Point(189, 40);
            this.checkBox_autosweep.Name = "checkBox_autosweep";
            this.checkBox_autosweep.Size = new System.Drawing.Size(86, 24);
            this.checkBox_autosweep.TabIndex = 30;
            this.checkBox_autosweep.Text = "Auto Sweep";
            // 
            // groupBox_TargetSettings
            // 
            this.groupBox_TargetSettings.Controls.Add(this.checkBox_movebeforetargeting);
            this.groupBox_TargetSettings.Controls.Add(this.checkBox_active_target);
            this.groupBox_TargetSettings.Location = new System.Drawing.Point(3, 3);
            this.groupBox_TargetSettings.Name = "groupBox_TargetSettings";
            this.groupBox_TargetSettings.Size = new System.Drawing.Size(226, 52);
            this.groupBox_TargetSettings.TabIndex = 42;
            this.groupBox_TargetSettings.TabStop = false;
            this.groupBox_TargetSettings.Text = "Target Settings";
            // 
            // checkBox_movebeforetargeting
            // 
            this.checkBox_movebeforetargeting.Enabled = false;
            this.checkBox_movebeforetargeting.Location = new System.Drawing.Point(93, 19);
            this.checkBox_movebeforetargeting.Name = "checkBox_movebeforetargeting";
            this.checkBox_movebeforetargeting.Size = new System.Drawing.Size(143, 24);
            this.checkBox_movebeforetargeting.TabIndex = 37;
            this.checkBox_movebeforetargeting.Text = "Move Before Targeting";
            this.checkBox_movebeforetargeting.CheckedChanged += new System.EventHandler(this.checkBox_movebeforetargeting_CheckedChanged);
            // 
            // checkBox_active_target
            // 
            this.checkBox_active_target.Location = new System.Drawing.Point(6, 19);
            this.checkBox_active_target.Name = "checkBox_active_target";
            this.checkBox_active_target.Size = new System.Drawing.Size(90, 24);
            this.checkBox_active_target.TabIndex = 36;
            this.checkBox_active_target.Text = "Active Target";
            this.checkBox_active_target.CheckedChanged += new System.EventHandler(this.checkBox_active_target_CheckedChanged);
            // 
            // tabPage_autofighter_advanced
            // 
            this.tabPage_autofighter_advanced.Controls.Add(this.groupBox_WindowTitle);
            this.tabPage_autofighter_advanced.Controls.Add(this.groupBox_AdvancedS);
            this.tabPage_autofighter_advanced.Location = new System.Drawing.Point(104, 4);
            this.tabPage_autofighter_advanced.Name = "tabPage_autofighter_advanced";
            this.tabPage_autofighter_advanced.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_autofighter_advanced.Size = new System.Drawing.Size(515, 377);
            this.tabPage_autofighter_advanced.TabIndex = 12;
            this.tabPage_autofighter_advanced.Text = "Advanced";
            this.tabPage_autofighter_advanced.UseVisualStyleBackColor = true;
            // 
            // groupBox_WindowTitle
            // 
            this.groupBox_WindowTitle.Controls.Add(this.label_Custom_WindowTitle);
            this.groupBox_WindowTitle.Controls.Add(this.textBox_Custom_WindowTitle);
            this.groupBox_WindowTitle.Controls.Add(this.button_Custom_WindowTitle_Reset);
            this.groupBox_WindowTitle.Controls.Add(this.button_Custom_WindowTitle_Set);
            this.groupBox_WindowTitle.Location = new System.Drawing.Point(6, 181);
            this.groupBox_WindowTitle.Name = "groupBox_WindowTitle";
            this.groupBox_WindowTitle.Size = new System.Drawing.Size(499, 40);
            this.groupBox_WindowTitle.TabIndex = 11;
            this.groupBox_WindowTitle.TabStop = false;
            // 
            // label_Custom_WindowTitle
            // 
            this.label_Custom_WindowTitle.AutoSize = true;
            this.label_Custom_WindowTitle.Location = new System.Drawing.Point(6, 16);
            this.label_Custom_WindowTitle.Name = "label_Custom_WindowTitle";
            this.label_Custom_WindowTitle.Size = new System.Drawing.Size(107, 13);
            this.label_Custom_WindowTitle.TabIndex = 8;
            this.label_Custom_WindowTitle.Text = "Custom Window Title";
            // 
            // textBox_Custom_WindowTitle
            // 
            this.textBox_Custom_WindowTitle.Location = new System.Drawing.Point(119, 13);
            this.textBox_Custom_WindowTitle.Name = "textBox_Custom_WindowTitle";
            this.textBox_Custom_WindowTitle.Size = new System.Drawing.Size(177, 20);
            this.textBox_Custom_WindowTitle.TabIndex = 6;
            // 
            // button_Custom_WindowTitle_Reset
            // 
            this.button_Custom_WindowTitle_Reset.Location = new System.Drawing.Point(383, 10);
            this.button_Custom_WindowTitle_Reset.Name = "button_Custom_WindowTitle_Reset";
            this.button_Custom_WindowTitle_Reset.Size = new System.Drawing.Size(75, 23);
            this.button_Custom_WindowTitle_Reset.TabIndex = 9;
            this.button_Custom_WindowTitle_Reset.Text = "Reset";
            this.button_Custom_WindowTitle_Reset.UseVisualStyleBackColor = true;
            this.button_Custom_WindowTitle_Reset.Click += new System.EventHandler(this.button_Custom_WindowTitle_Reset_Click);
            // 
            // button_Custom_WindowTitle_Set
            // 
            this.button_Custom_WindowTitle_Set.Location = new System.Drawing.Point(302, 10);
            this.button_Custom_WindowTitle_Set.Name = "button_Custom_WindowTitle_Set";
            this.button_Custom_WindowTitle_Set.Size = new System.Drawing.Size(75, 23);
            this.button_Custom_WindowTitle_Set.TabIndex = 7;
            this.button_Custom_WindowTitle_Set.Text = "Set";
            this.button_Custom_WindowTitle_Set.UseVisualStyleBackColor = true;
            this.button_Custom_WindowTitle_Set.Click += new System.EventHandler(this.button_Custom_WindowTitle_Set_Click);
            // 
            // groupBox_AdvancedS
            // 
            this.groupBox_AdvancedS.Controls.Add(this.label27);
            this.groupBox_AdvancedS.Controls.Add(this.numericUpDown_pickuptimeout);
            this.groupBox_AdvancedS.Controls.Add(this.label_anti_ks_delay);
            this.groupBox_AdvancedS.Controls.Add(this.numericUpDown_anti_ks_delay);
            this.groupBox_AdvancedS.Controls.Add(this.numericUpDown_autofollow_delay);
            this.groupBox_AdvancedS.Controls.Add(this.numericUpDown_blacklist_tries);
            this.groupBox_AdvancedS.Controls.Add(this.label_autofollow_delay);
            this.groupBox_AdvancedS.Controls.Add(this.label_blacklist_tries);
            this.groupBox_AdvancedS.Location = new System.Drawing.Point(3, 3);
            this.groupBox_AdvancedS.Name = "groupBox_AdvancedS";
            this.groupBox_AdvancedS.Size = new System.Drawing.Size(204, 121);
            this.groupBox_AdvancedS.TabIndex = 10;
            this.groupBox_AdvancedS.TabStop = false;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(6, 94);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(81, 13);
            this.label27.TabIndex = 7;
            this.label27.Text = "Pickup Timeout";
            // 
            // numericUpDown_pickuptimeout
            // 
            this.numericUpDown_pickuptimeout.Location = new System.Drawing.Point(111, 92);
            this.numericUpDown_pickuptimeout.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_pickuptimeout.Name = "numericUpDown_pickuptimeout";
            this.numericUpDown_pickuptimeout.Size = new System.Drawing.Size(49, 20);
            this.numericUpDown_pickuptimeout.TabIndex = 6;
            this.numericUpDown_pickuptimeout.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label_anti_ks_delay
            // 
            this.label_anti_ks_delay.AutoSize = true;
            this.label_anti_ks_delay.Location = new System.Drawing.Point(6, 16);
            this.label_anti_ks_delay.Name = "label_anti_ks_delay";
            this.label_anti_ks_delay.Size = new System.Drawing.Size(72, 13);
            this.label_anti_ks_delay.TabIndex = 3;
            this.label_anti_ks_delay.Text = "Anti KS Delay";
            // 
            // numericUpDown_anti_ks_delay
            // 
            this.numericUpDown_anti_ks_delay.Location = new System.Drawing.Point(111, 14);
            this.numericUpDown_anti_ks_delay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_anti_ks_delay.Name = "numericUpDown_anti_ks_delay";
            this.numericUpDown_anti_ks_delay.Size = new System.Drawing.Size(49, 20);
            this.numericUpDown_anti_ks_delay.TabIndex = 0;
            this.numericUpDown_anti_ks_delay.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // numericUpDown_autofollow_delay
            // 
            this.numericUpDown_autofollow_delay.Location = new System.Drawing.Point(111, 40);
            this.numericUpDown_autofollow_delay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_autofollow_delay.Name = "numericUpDown_autofollow_delay";
            this.numericUpDown_autofollow_delay.Size = new System.Drawing.Size(49, 20);
            this.numericUpDown_autofollow_delay.TabIndex = 1;
            this.numericUpDown_autofollow_delay.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // numericUpDown_blacklist_tries
            // 
            this.numericUpDown_blacklist_tries.Location = new System.Drawing.Point(111, 66);
            this.numericUpDown_blacklist_tries.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown_blacklist_tries.Name = "numericUpDown_blacklist_tries";
            this.numericUpDown_blacklist_tries.Size = new System.Drawing.Size(49, 20);
            this.numericUpDown_blacklist_tries.TabIndex = 2;
            this.numericUpDown_blacklist_tries.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // label_autofollow_delay
            // 
            this.label_autofollow_delay.AutoSize = true;
            this.label_autofollow_delay.Location = new System.Drawing.Point(6, 42);
            this.label_autofollow_delay.Name = "label_autofollow_delay";
            this.label_autofollow_delay.Size = new System.Drawing.Size(86, 13);
            this.label_autofollow_delay.TabIndex = 4;
            this.label_autofollow_delay.Text = "Autofollow Delay";
            // 
            // label_blacklist_tries
            // 
            this.label_blacklist_tries.AutoSize = true;
            this.label_blacklist_tries.Location = new System.Drawing.Point(6, 68);
            this.label_blacklist_tries.Name = "label_blacklist_tries";
            this.label_blacklist_tries.Size = new System.Drawing.Size(72, 13);
            this.label_blacklist_tries.TabIndex = 5;
            this.label_blacklist_tries.Text = "Blacklist Tries";
            // 
            // tabPage_RestOptions
            // 
            this.tabPage_RestOptions.Controls.Add(this.groupBox_Rest_Party);
            this.tabPage_RestOptions.Controls.Add(this.groupBox_Rest_Solo);
            this.tabPage_RestOptions.Location = new System.Drawing.Point(104, 4);
            this.tabPage_RestOptions.Name = "tabPage_RestOptions";
            this.tabPage_RestOptions.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_RestOptions.Size = new System.Drawing.Size(515, 377);
            this.tabPage_RestOptions.TabIndex = 11;
            this.tabPage_RestOptions.Text = "Rest Options";
            this.tabPage_RestOptions.UseVisualStyleBackColor = true;
            // 
            // groupBox_Rest_Party
            // 
            this.groupBox_Rest_Party.Controls.Add(this.label_followrestname);
            this.groupBox_Rest_Party.Controls.Add(this.textBox_FollowRestName);
            this.groupBox_Rest_Party.Controls.Add(this.checkBox_FollowRest);
            this.groupBox_Rest_Party.Location = new System.Drawing.Point(3, 174);
            this.groupBox_Rest_Party.Name = "groupBox_Rest_Party";
            this.groupBox_Rest_Party.Size = new System.Drawing.Size(384, 58);
            this.groupBox_Rest_Party.TabIndex = 1;
            this.groupBox_Rest_Party.TabStop = false;
            this.groupBox_Rest_Party.Text = "Party Settings";
            // 
            // label_followrestname
            // 
            this.label_followrestname.Location = new System.Drawing.Point(229, 12);
            this.label_followrestname.Name = "label_followrestname";
            this.label_followrestname.Size = new System.Drawing.Size(175, 24);
            this.label_followrestname.TabIndex = 6;
            this.label_followrestname.Text = "Name";
            this.label_followrestname.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox_FollowRestName
            // 
            this.textBox_FollowRestName.Location = new System.Drawing.Point(123, 15);
            this.textBox_FollowRestName.Name = "textBox_FollowRestName";
            this.textBox_FollowRestName.Size = new System.Drawing.Size(100, 20);
            this.textBox_FollowRestName.TabIndex = 5;
            // 
            // checkBox_FollowRest
            // 
            this.checkBox_FollowRest.Location = new System.Drawing.Point(5, 19);
            this.checkBox_FollowRest.Name = "checkBox_FollowRest";
            this.checkBox_FollowRest.Size = new System.Drawing.Size(112, 17);
            this.checkBox_FollowRest.TabIndex = 4;
            this.checkBox_FollowRest.Text = "Follow Rest";
            this.checkBox_FollowRest.UseVisualStyleBackColor = true;
            // 
            // groupBox_Rest_Solo
            // 
            this.groupBox_Rest_Solo.Controls.Add(this.label13);
            this.groupBox_Rest_Solo.Controls.Add(this.numericUpDown_RestUntilMP);
            this.groupBox_Rest_Solo.Controls.Add(this.label17);
            this.groupBox_Rest_Solo.Controls.Add(this.numericUpDown_RestUntilHP);
            this.groupBox_Rest_Solo.Controls.Add(this.checkBox_RestUntilMP);
            this.groupBox_Rest_Solo.Controls.Add(this.checkBox_RestUntilHP);
            this.groupBox_Rest_Solo.Controls.Add(this.label_percent_MP);
            this.groupBox_Rest_Solo.Controls.Add(this.numericUpDown_RestBelowMP);
            this.groupBox_Rest_Solo.Controls.Add(this.checkBox_RestBelowMP);
            this.groupBox_Rest_Solo.Controls.Add(this.label_percent_HP);
            this.groupBox_Rest_Solo.Controls.Add(this.numericUpDown_RestBelowHP);
            this.groupBox_Rest_Solo.Controls.Add(this.checkBox_RestBelowHP);
            this.groupBox_Rest_Solo.Location = new System.Drawing.Point(3, 3);
            this.groupBox_Rest_Solo.Name = "groupBox_Rest_Solo";
            this.groupBox_Rest_Solo.Size = new System.Drawing.Size(384, 165);
            this.groupBox_Rest_Solo.TabIndex = 0;
            this.groupBox_Rest_Solo.TabStop = false;
            this.groupBox_Rest_Solo.Text = "Solo Settings";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(302, 46);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(34, 13);
            this.label13.TabIndex = 11;
            this.label13.Text = "% MP";
            // 
            // numericUpDown_RestUntilMP
            // 
            this.numericUpDown_RestUntilMP.Location = new System.Drawing.Point(244, 44);
            this.numericUpDown_RestUntilMP.Name = "numericUpDown_RestUntilMP";
            this.numericUpDown_RestUntilMP.Size = new System.Drawing.Size(54, 20);
            this.numericUpDown_RestUntilMP.TabIndex = 10;
            this.numericUpDown_RestUntilMP.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(302, 20);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(33, 13);
            this.label17.TabIndex = 9;
            this.label17.Text = "% HP";
            // 
            // numericUpDown_RestUntilHP
            // 
            this.numericUpDown_RestUntilHP.Location = new System.Drawing.Point(243, 16);
            this.numericUpDown_RestUntilHP.Name = "numericUpDown_RestUntilHP";
            this.numericUpDown_RestUntilHP.Size = new System.Drawing.Size(54, 20);
            this.numericUpDown_RestUntilHP.TabIndex = 8;
            this.numericUpDown_RestUntilHP.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // checkBox_RestUntilMP
            // 
            this.checkBox_RestUntilMP.AutoSize = true;
            this.checkBox_RestUntilMP.Location = new System.Drawing.Point(190, 45);
            this.checkBox_RestUntilMP.Name = "checkBox_RestUntilMP";
            this.checkBox_RestUntilMP.Size = new System.Drawing.Size(47, 17);
            this.checkBox_RestUntilMP.TabIndex = 7;
            this.checkBox_RestUntilMP.Text = "Until";
            this.checkBox_RestUntilMP.UseVisualStyleBackColor = true;
            // 
            // checkBox_RestUntilHP
            // 
            this.checkBox_RestUntilHP.AutoSize = true;
            this.checkBox_RestUntilHP.Location = new System.Drawing.Point(190, 19);
            this.checkBox_RestUntilHP.Name = "checkBox_RestUntilHP";
            this.checkBox_RestUntilHP.Size = new System.Drawing.Size(47, 17);
            this.checkBox_RestUntilHP.TabIndex = 6;
            this.checkBox_RestUntilHP.Text = "Until";
            this.checkBox_RestUntilHP.UseVisualStyleBackColor = true;
            // 
            // label_percent_MP
            // 
            this.label_percent_MP.AutoSize = true;
            this.label_percent_MP.Location = new System.Drawing.Point(150, 46);
            this.label_percent_MP.Name = "label_percent_MP";
            this.label_percent_MP.Size = new System.Drawing.Size(34, 13);
            this.label_percent_MP.TabIndex = 5;
            this.label_percent_MP.Text = "% MP";
            // 
            // numericUpDown_RestBelowMP
            // 
            this.numericUpDown_RestBelowMP.Location = new System.Drawing.Point(92, 42);
            this.numericUpDown_RestBelowMP.Name = "numericUpDown_RestBelowMP";
            this.numericUpDown_RestBelowMP.Size = new System.Drawing.Size(53, 20);
            this.numericUpDown_RestBelowMP.TabIndex = 4;
            this.numericUpDown_RestBelowMP.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // checkBox_RestBelowMP
            // 
            this.checkBox_RestBelowMP.AutoSize = true;
            this.checkBox_RestBelowMP.Location = new System.Drawing.Point(6, 45);
            this.checkBox_RestBelowMP.Name = "checkBox_RestBelowMP";
            this.checkBox_RestBelowMP.Size = new System.Drawing.Size(80, 17);
            this.checkBox_RestBelowMP.TabIndex = 3;
            this.checkBox_RestBelowMP.Text = "Rest Below";
            this.checkBox_RestBelowMP.UseVisualStyleBackColor = true;
            // 
            // label_percent_HP
            // 
            this.label_percent_HP.AutoSize = true;
            this.label_percent_HP.Location = new System.Drawing.Point(150, 20);
            this.label_percent_HP.Name = "label_percent_HP";
            this.label_percent_HP.Size = new System.Drawing.Size(33, 13);
            this.label_percent_HP.TabIndex = 2;
            this.label_percent_HP.Text = "% HP";
            // 
            // numericUpDown_RestBelowHP
            // 
            this.numericUpDown_RestBelowHP.Location = new System.Drawing.Point(91, 16);
            this.numericUpDown_RestBelowHP.Name = "numericUpDown_RestBelowHP";
            this.numericUpDown_RestBelowHP.Size = new System.Drawing.Size(54, 20);
            this.numericUpDown_RestBelowHP.TabIndex = 1;
            this.numericUpDown_RestBelowHP.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // checkBox_RestBelowHP
            // 
            this.checkBox_RestBelowHP.AutoSize = true;
            this.checkBox_RestBelowHP.Location = new System.Drawing.Point(6, 17);
            this.checkBox_RestBelowHP.Name = "checkBox_RestBelowHP";
            this.checkBox_RestBelowHP.Size = new System.Drawing.Size(80, 17);
            this.checkBox_RestBelowHP.TabIndex = 0;
            this.checkBox_RestBelowHP.Text = "Rest Below";
            this.checkBox_RestBelowHP.UseVisualStyleBackColor = true;
            // 
            // tabPage_target
            // 
            this.tabPage_target.Controls.Add(this.groupBox5);
            this.tabPage_target.Controls.Add(this.groupBox4);
            this.tabPage_target.Controls.Add(this.groupBox3);
            this.tabPage_target.Controls.Add(this.groupBox2);
            this.tabPage_target.Controls.Add(this.groupBox1);
            this.tabPage_target.Controls.Add(this.checkBox_portect_priority);
            this.tabPage_target.Location = new System.Drawing.Point(104, 4);
            this.tabPage_target.Name = "tabPage_target";
            this.tabPage_target.Size = new System.Drawing.Size(515, 377);
            this.tabPage_target.TabIndex = 10;
            this.tabPage_target.Text = "Targeting";
            this.tabPage_target.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.radioButton_combat0);
            this.groupBox5.Controls.Add(this.radioButton_combat1);
            this.groupBox5.Controls.Add(this.radioButton_combat2);
            this.groupBox5.Location = new System.Drawing.Point(9, 148);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(335, 33);
            this.groupBox5.TabIndex = 31;
            this.groupBox5.TabStop = false;
            // 
            // radioButton_combat0
            // 
            this.radioButton_combat0.AutoSize = true;
            this.radioButton_combat0.Location = new System.Drawing.Point(25, 10);
            this.radioButton_combat0.Name = "radioButton_combat0";
            this.radioButton_combat0.Size = new System.Drawing.Size(67, 17);
            this.radioButton_combat0.TabIndex = 12;
            this.radioButton_combat0.Text = "Don\'t KS";
            // 
            // radioButton_combat1
            // 
            this.radioButton_combat1.AutoSize = true;
            this.radioButton_combat1.Location = new System.Drawing.Point(116, 10);
            this.radioButton_combat1.Name = "radioButton_combat1";
            this.radioButton_combat1.Size = new System.Drawing.Size(63, 17);
            this.radioButton_combat1.TabIndex = 13;
            this.radioButton_combat1.Text = "Only KS";
            // 
            // radioButton_combat2
            // 
            this.radioButton_combat2.AutoSize = true;
            this.radioButton_combat2.Checked = true;
            this.radioButton_combat2.Location = new System.Drawing.Point(207, 10);
            this.radioButton_combat2.Name = "radioButton_combat2";
            this.radioButton_combat2.Size = new System.Drawing.Size(47, 17);
            this.radioButton_combat2.TabIndex = 14;
            this.radioButton_combat2.TabStop = true;
            this.radioButton_combat2.Text = "Both";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButton_inbox0);
            this.groupBox4.Controls.Add(this.radioButton_inbox1);
            this.groupBox4.Controls.Add(this.radioButton_inbox2);
            this.groupBox4.Location = new System.Drawing.Point(9, 115);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(335, 33);
            this.groupBox4.TabIndex = 30;
            this.groupBox4.TabStop = false;
            // 
            // radioButton_inbox0
            // 
            this.radioButton_inbox0.AutoSize = true;
            this.radioButton_inbox0.Checked = true;
            this.radioButton_inbox0.Location = new System.Drawing.Point(25, 10);
            this.radioButton_inbox0.Name = "radioButton_inbox0";
            this.radioButton_inbox0.Size = new System.Drawing.Size(55, 17);
            this.radioButton_inbox0.TabIndex = 9;
            this.radioButton_inbox0.TabStop = true;
            this.radioButton_inbox0.Text = "In Box";
            // 
            // radioButton_inbox1
            // 
            this.radioButton_inbox1.AutoSize = true;
            this.radioButton_inbox1.Location = new System.Drawing.Point(116, 10);
            this.radioButton_inbox1.Name = "radioButton_inbox1";
            this.radioButton_inbox1.Size = new System.Drawing.Size(75, 17);
            this.radioButton_inbox1.TabIndex = 10;
            this.radioButton_inbox1.Text = "Not In Box";
            // 
            // radioButton_inbox2
            // 
            this.radioButton_inbox2.AutoSize = true;
            this.radioButton_inbox2.Location = new System.Drawing.Point(207, 10);
            this.radioButton_inbox2.Name = "radioButton_inbox2";
            this.radioButton_inbox2.Size = new System.Drawing.Size(47, 17);
            this.radioButton_inbox2.TabIndex = 11;
            this.radioButton_inbox2.Text = "Both";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButton_alive0);
            this.groupBox3.Controls.Add(this.radioButton_alive1);
            this.groupBox3.Controls.Add(this.radioButton_alive2);
            this.groupBox3.Location = new System.Drawing.Point(9, 82);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(335, 33);
            this.groupBox3.TabIndex = 29;
            this.groupBox3.TabStop = false;
            // 
            // radioButton_alive0
            // 
            this.radioButton_alive0.AutoSize = true;
            this.radioButton_alive0.Checked = true;
            this.radioButton_alive0.Location = new System.Drawing.Point(25, 10);
            this.radioButton_alive0.Name = "radioButton_alive0";
            this.radioButton_alive0.Size = new System.Drawing.Size(48, 17);
            this.radioButton_alive0.TabIndex = 6;
            this.radioButton_alive0.TabStop = true;
            this.radioButton_alive0.Text = "Alive";
            // 
            // radioButton_alive1
            // 
            this.radioButton_alive1.AutoSize = true;
            this.radioButton_alive1.Location = new System.Drawing.Point(116, 10);
            this.radioButton_alive1.Name = "radioButton_alive1";
            this.radioButton_alive1.Size = new System.Drawing.Size(51, 17);
            this.radioButton_alive1.TabIndex = 7;
            this.radioButton_alive1.Text = "Dead";
            // 
            // radioButton_alive2
            // 
            this.radioButton_alive2.AutoSize = true;
            this.radioButton_alive2.Location = new System.Drawing.Point(207, 10);
            this.radioButton_alive2.Name = "radioButton_alive2";
            this.radioButton_alive2.Size = new System.Drawing.Size(47, 17);
            this.radioButton_alive2.TabIndex = 8;
            this.radioButton_alive2.Text = "Both";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton_attackable2);
            this.groupBox2.Controls.Add(this.radioButton_attackable0);
            this.groupBox2.Controls.Add(this.radioButton_attackable1);
            this.groupBox2.Location = new System.Drawing.Point(9, 49);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(335, 33);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            // 
            // radioButton_attackable2
            // 
            this.radioButton_attackable2.AutoSize = true;
            this.radioButton_attackable2.Location = new System.Drawing.Point(207, 10);
            this.radioButton_attackable2.Name = "radioButton_attackable2";
            this.radioButton_attackable2.Size = new System.Drawing.Size(47, 17);
            this.radioButton_attackable2.TabIndex = 5;
            this.radioButton_attackable2.Text = "Both";
            // 
            // radioButton_attackable0
            // 
            this.radioButton_attackable0.AutoSize = true;
            this.radioButton_attackable0.Checked = true;
            this.radioButton_attackable0.Location = new System.Drawing.Point(25, 10);
            this.radioButton_attackable0.Name = "radioButton_attackable0";
            this.radioButton_attackable0.Size = new System.Drawing.Size(76, 17);
            this.radioButton_attackable0.TabIndex = 3;
            this.radioButton_attackable0.TabStop = true;
            this.radioButton_attackable0.Text = "Attackable";
            // 
            // radioButton_attackable1
            // 
            this.radioButton_attackable1.AutoSize = true;
            this.radioButton_attackable1.Location = new System.Drawing.Point(116, 10);
            this.radioButton_attackable1.Name = "radioButton_attackable1";
            this.radioButton_attackable1.Size = new System.Drawing.Size(70, 17);
            this.radioButton_attackable1.TabIndex = 4;
            this.radioButton_attackable1.Text = "Invincible";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton_type0);
            this.groupBox1.Controls.Add(this.radioButton_type1);
            this.groupBox1.Controls.Add(this.radioButton_type2);
            this.groupBox1.Location = new System.Drawing.Point(9, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(335, 33);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            // 
            // radioButton_type0
            // 
            this.radioButton_type0.AutoSize = true;
            this.radioButton_type0.Checked = true;
            this.radioButton_type0.Location = new System.Drawing.Point(25, 10);
            this.radioButton_type0.Name = "radioButton_type0";
            this.radioButton_type0.Size = new System.Drawing.Size(52, 17);
            this.radioButton_type0.TabIndex = 0;
            this.radioButton_type0.TabStop = true;
            this.radioButton_type0.Tag = "";
            this.radioButton_type0.Text = "NPCs";
            // 
            // radioButton_type1
            // 
            this.radioButton_type1.AutoSize = true;
            this.radioButton_type1.Location = new System.Drawing.Point(116, 10);
            this.radioButton_type1.Name = "radioButton_type1";
            this.radioButton_type1.Size = new System.Drawing.Size(59, 17);
            this.radioButton_type1.TabIndex = 1;
            this.radioButton_type1.Text = "Players";
            // 
            // radioButton_type2
            // 
            this.radioButton_type2.AutoSize = true;
            this.radioButton_type2.Location = new System.Drawing.Point(207, 10);
            this.radioButton_type2.Name = "radioButton_type2";
            this.radioButton_type2.Size = new System.Drawing.Size(47, 17);
            this.radioButton_type2.TabIndex = 2;
            this.radioButton_type2.Text = "Both";
            // 
            // checkBox_portect_priority
            // 
            this.checkBox_portect_priority.Enabled = false;
            this.checkBox_portect_priority.Location = new System.Drawing.Point(109, 238);
            this.checkBox_portect_priority.Name = "checkBox_portect_priority";
            this.checkBox_portect_priority.Size = new System.Drawing.Size(313, 24);
            this.checkBox_portect_priority.TabIndex = 26;
            this.checkBox_portect_priority.Text = "Protect Priority";
            // 
            // tabPage_buffsheals
            // 
            this.tabPage_buffsheals.Controls.Add(this.comboBox_buffheal_skill);
            this.tabPage_buffsheals.Controls.Add(this.label_target);
            this.tabPage_buffsheals.Controls.Add(this.checkBox_target);
            this.tabPage_buffsheals.Controls.Add(this.button_update);
            this.tabPage_buffsheals.Controls.Add(this.button_add);
            this.tabPage_buffsheals.Controls.Add(this.label_buffheal_mp);
            this.tabPage_buffsheals.Controls.Add(this.textBox_buffheal_mp);
            this.tabPage_buffsheals.Controls.Add(this.label_buffheal_names);
            this.tabPage_buffsheals.Controls.Add(this.label_buffheal_on);
            this.tabPage_buffsheals.Controls.Add(this.label_buffheal_trait);
            this.tabPage_buffsheals.Controls.Add(this.label_buffheal_delay);
            this.tabPage_buffsheals.Controls.Add(this.label_buffheal_minper);
            this.tabPage_buffsheals.Controls.Add(this.label_buffheal_skill);
            this.tabPage_buffsheals.Controls.Add(this.textBox_buffheal_names);
            this.tabPage_buffsheals.Controls.Add(this.comboBox_buffheal_trait);
            this.tabPage_buffsheals.Controls.Add(this.checkBox_buffheal_on);
            this.tabPage_buffsheals.Controls.Add(this.textBox_buffheal_delay);
            this.tabPage_buffsheals.Controls.Add(this.textBox_buffheal_min_per);
            this.tabPage_buffsheals.Controls.Add(this.listView_buffheal);
            this.tabPage_buffsheals.Location = new System.Drawing.Point(104, 4);
            this.tabPage_buffsheals.Name = "tabPage_buffsheals";
            this.tabPage_buffsheals.Size = new System.Drawing.Size(515, 377);
            this.tabPage_buffsheals.TabIndex = 2;
            this.tabPage_buffsheals.Text = "Buffs/Heals";
            this.tabPage_buffsheals.UseVisualStyleBackColor = true;
            // 
            // comboBox_buffheal_skill
            // 
            this.comboBox_buffheal_skill.Location = new System.Drawing.Point(142, 64);
            this.comboBox_buffheal_skill.Name = "comboBox_buffheal_skill";
            this.comboBox_buffheal_skill.Size = new System.Drawing.Size(137, 21);
            this.comboBox_buffheal_skill.TabIndex = 72;
            // 
            // label_target
            // 
            this.label_target.Location = new System.Drawing.Point(368, 8);
            this.label_target.Name = "label_target";
            this.label_target.Size = new System.Drawing.Size(128, 16);
            this.label_target.TabIndex = 71;
            this.label_target.Text = "Need Target";
            this.label_target.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBox_target
            // 
            this.checkBox_target.Checked = true;
            this.checkBox_target.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_target.Location = new System.Drawing.Point(456, 24);
            this.checkBox_target.Name = "checkBox_target";
            this.checkBox_target.Size = new System.Drawing.Size(24, 24);
            this.checkBox_target.TabIndex = 2;
            // 
            // button_update
            // 
            this.button_update.Enabled = false;
            this.button_update.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_update.Location = new System.Drawing.Point(240, 96);
            this.button_update.Name = "button_update";
            this.button_update.Size = new System.Drawing.Size(128, 24);
            this.button_update.TabIndex = 10;
            this.button_update.Text = "Update";
            this.button_update.Click += new System.EventHandler(this.button_update_Click);
            // 
            // button_add
            // 
            this.button_add.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_add.Location = new System.Drawing.Point(88, 96);
            this.button_add.Name = "button_add";
            this.button_add.Size = new System.Drawing.Size(128, 24);
            this.button_add.TabIndex = 9;
            this.button_add.Text = "Add";
            this.button_add.Click += new System.EventHandler(this.button_add_Click);
            // 
            // label_buffheal_mp
            // 
            this.label_buffheal_mp.Location = new System.Drawing.Point(436, 48);
            this.label_buffheal_mp.Name = "label_buffheal_mp";
            this.label_buffheal_mp.Size = new System.Drawing.Size(60, 16);
            this.label_buffheal_mp.TabIndex = 64;
            this.label_buffheal_mp.Text = "MP>";
            this.label_buffheal_mp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_buffheal_mp
            // 
            this.textBox_buffheal_mp.Location = new System.Drawing.Point(436, 64);
            this.textBox_buffheal_mp.MaxLength = 4;
            this.textBox_buffheal_mp.Name = "textBox_buffheal_mp";
            this.textBox_buffheal_mp.Size = new System.Drawing.Size(60, 20);
            this.textBox_buffheal_mp.TabIndex = 8;
            this.textBox_buffheal_mp.Text = "100";
            this.textBox_buffheal_mp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_buffheal_names
            // 
            this.label_buffheal_names.Location = new System.Drawing.Point(192, 8);
            this.label_buffheal_names.Name = "label_buffheal_names";
            this.label_buffheal_names.Size = new System.Drawing.Size(104, 16);
            this.label_buffheal_names.TabIndex = 62;
            this.label_buffheal_names.Text = "Names";
            this.label_buffheal_names.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_buffheal_on
            // 
            this.label_buffheal_on.Location = new System.Drawing.Point(24, 8);
            this.label_buffheal_on.Name = "label_buffheal_on";
            this.label_buffheal_on.Size = new System.Drawing.Size(56, 16);
            this.label_buffheal_on.TabIndex = 61;
            this.label_buffheal_on.Text = "On";
            this.label_buffheal_on.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_buffheal_trait
            // 
            this.label_buffheal_trait.Location = new System.Drawing.Point(16, 48);
            this.label_buffheal_trait.Name = "label_buffheal_trait";
            this.label_buffheal_trait.Size = new System.Drawing.Size(120, 16);
            this.label_buffheal_trait.TabIndex = 60;
            this.label_buffheal_trait.Text = "Trait";
            this.label_buffheal_trait.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_buffheal_delay
            // 
            this.label_buffheal_delay.Location = new System.Drawing.Point(339, 48);
            this.label_buffheal_delay.Name = "label_buffheal_delay";
            this.label_buffheal_delay.Size = new System.Drawing.Size(89, 16);
            this.label_buffheal_delay.TabIndex = 58;
            this.label_buffheal_delay.Text = "Delay(sec)";
            this.label_buffheal_delay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_buffheal_minper
            // 
            this.label_buffheal_minper.Location = new System.Drawing.Point(285, 48);
            this.label_buffheal_minper.Name = "label_buffheal_minper";
            this.label_buffheal_minper.Size = new System.Drawing.Size(48, 16);
            this.label_buffheal_minper.TabIndex = 57;
            this.label_buffheal_minper.Text = "XX < %";
            this.label_buffheal_minper.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_buffheal_skill
            // 
            this.label_buffheal_skill.Location = new System.Drawing.Point(176, 48);
            this.label_buffheal_skill.Name = "label_buffheal_skill";
            this.label_buffheal_skill.Size = new System.Drawing.Size(72, 16);
            this.label_buffheal_skill.TabIndex = 56;
            this.label_buffheal_skill.Text = "Skill";
            this.label_buffheal_skill.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_buffheal_names
            // 
            this.textBox_buffheal_names.Location = new System.Drawing.Point(64, 24);
            this.textBox_buffheal_names.Name = "textBox_buffheal_names";
            this.textBox_buffheal_names.Size = new System.Drawing.Size(352, 20);
            this.textBox_buffheal_names.TabIndex = 1;
            // 
            // comboBox_buffheal_trait
            // 
            this.comboBox_buffheal_trait.Items.AddRange(new object[] {
            "Always",
            "CP",
            "HP",
            "MP",
            "Dead",
            "Charges",
            "Souls",
            "Death Penalty",
            "Bleeding",
            "Poisoned",
            "Iced",
            "Shackled",
            "Feared",
            "Stunned",
            "Slept",
            "Muted",
            "Rooted",
            "Paralyzed",
            "Petrified",
            "Ultimate Defense"});
            this.comboBox_buffheal_trait.Location = new System.Drawing.Point(16, 64);
            this.comboBox_buffheal_trait.Name = "comboBox_buffheal_trait";
            this.comboBox_buffheal_trait.Size = new System.Drawing.Size(120, 21);
            this.comboBox_buffheal_trait.TabIndex = 3;
            // 
            // checkBox_buffheal_on
            // 
            this.checkBox_buffheal_on.Checked = true;
            this.checkBox_buffheal_on.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_buffheal_on.Location = new System.Drawing.Point(27, 24);
            this.checkBox_buffheal_on.Name = "checkBox_buffheal_on";
            this.checkBox_buffheal_on.Size = new System.Drawing.Size(24, 24);
            this.checkBox_buffheal_on.TabIndex = 0;
            // 
            // textBox_buffheal_delay
            // 
            this.textBox_buffheal_delay.Location = new System.Drawing.Point(339, 64);
            this.textBox_buffheal_delay.MaxLength = 8;
            this.textBox_buffheal_delay.Name = "textBox_buffheal_delay";
            this.textBox_buffheal_delay.Size = new System.Drawing.Size(91, 20);
            this.textBox_buffheal_delay.TabIndex = 7;
            this.textBox_buffheal_delay.Text = "1";
            this.textBox_buffheal_delay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_buffheal_min_per
            // 
            this.textBox_buffheal_min_per.Location = new System.Drawing.Point(285, 65);
            this.textBox_buffheal_min_per.MaxLength = 3;
            this.textBox_buffheal_min_per.Name = "textBox_buffheal_min_per";
            this.textBox_buffheal_min_per.Size = new System.Drawing.Size(48, 20);
            this.textBox_buffheal_min_per.TabIndex = 6;
            this.textBox_buffheal_min_per.Text = "60";
            this.textBox_buffheal_min_per.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // listView_buffheal
            // 
            this.listView_buffheal.CheckBoxes = true;
            this.listView_buffheal.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_skill,
            this.columnHeader_trait,
            this.columnHeader_names,
            this.columnHeader_xx,
            this.columnHeader_delay,
            this.columnHeader_mp,
            this.columnHeader_needtarget,
            this.columnHeader_traitID,
            this.columnHeader_scID});
            this.listView_buffheal.ContextMenuStrip = this.contextMenuStrip_buff;
            this.listView_buffheal.FullRowSelect = true;
            this.listView_buffheal.GridLines = true;
            this.listView_buffheal.Location = new System.Drawing.Point(16, 126);
            this.listView_buffheal.MultiSelect = false;
            this.listView_buffheal.Name = "listView_buffheal";
            this.listView_buffheal.Size = new System.Drawing.Size(488, 224);
            this.listView_buffheal.TabIndex = 11;
            this.listView_buffheal.UseCompatibleStateImageBehavior = false;
            this.listView_buffheal.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader_skill
            // 
            this.columnHeader_skill.Text = "Skill";
            this.columnHeader_skill.Width = 91;
            // 
            // columnHeader_trait
            // 
            this.columnHeader_trait.Text = "Trait";
            this.columnHeader_trait.Width = 55;
            // 
            // columnHeader_names
            // 
            this.columnHeader_names.Text = "Names";
            this.columnHeader_names.Width = 53;
            // 
            // columnHeader_xx
            // 
            this.columnHeader_xx.Text = "XX<%";
            this.columnHeader_xx.Width = 41;
            // 
            // columnHeader_delay
            // 
            this.columnHeader_delay.Text = "Delay(sec)";
            this.columnHeader_delay.Width = 62;
            // 
            // columnHeader_mp
            // 
            this.columnHeader_mp.Text = "MP>";
            this.columnHeader_mp.Width = 47;
            // 
            // columnHeader_needtarget
            // 
            this.columnHeader_needtarget.Text = "Need Target";
            this.columnHeader_needtarget.Width = 54;
            // 
            // columnHeader_traitID
            // 
            this.columnHeader_traitID.Text = "TraitID";
            this.columnHeader_traitID.Width = 0;
            // 
            // columnHeader_scID
            // 
            this.columnHeader_scID.Text = "SkillID";
            this.columnHeader_scID.Width = 67;
            // 
            // contextMenuStrip_buff
            // 
            this.contextMenuStrip_buff.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem2,
            this.moveUpToolStripMenuItem});
            this.contextMenuStrip_buff.Name = "contextMenuStrip1";
            this.contextMenuStrip_buff.Size = new System.Drawing.Size(123, 48);
            // 
            // removeToolStripMenuItem2
            // 
            this.removeToolStripMenuItem2.Name = "removeToolStripMenuItem2";
            this.removeToolStripMenuItem2.Size = new System.Drawing.Size(122, 22);
            this.removeToolStripMenuItem2.Text = "Remove";
            this.removeToolStripMenuItem2.Click += new System.EventHandler(this.removeToolStripMenuItem2_Click);
            // 
            // moveUpToolStripMenuItem
            // 
            this.moveUpToolStripMenuItem.Name = "moveUpToolStripMenuItem";
            this.moveUpToolStripMenuItem.Size = new System.Drawing.Size(122, 22);
            this.moveUpToolStripMenuItem.Text = "Move Up";
            this.moveUpToolStripMenuItem.Click += new System.EventHandler(this.moveUpToolStripMenuItem_Click);
            // 
            // tabPage_toggles
            // 
            this.tabPage_toggles.Controls.Add(this.label32);
            this.tabPage_toggles.Controls.Add(this.textBox_lesserthen_toggle);
            this.tabPage_toggles.Controls.Add(this.comboBox_skills_toggle);
            this.tabPage_toggles.Controls.Add(this.button_update_toggle);
            this.tabPage_toggles.Controls.Add(this.button_add_toggle);
            this.tabPage_toggles.Controls.Add(this.textBox_greaterthen_toggle);
            this.tabPage_toggles.Controls.Add(this.label35);
            this.tabPage_toggles.Controls.Add(this.label36);
            this.tabPage_toggles.Controls.Add(this.label38);
            this.tabPage_toggles.Controls.Add(this.label39);
            this.tabPage_toggles.Controls.Add(this.comboBox_trait_toggle);
            this.tabPage_toggles.Controls.Add(this.checkBox_onoff_toggle);
            this.tabPage_toggles.Controls.Add(this.listView_toggles);
            this.tabPage_toggles.Location = new System.Drawing.Point(104, 4);
            this.tabPage_toggles.Name = "tabPage_toggles";
            this.tabPage_toggles.Size = new System.Drawing.Size(515, 377);
            this.tabPage_toggles.TabIndex = 15;
            this.tabPage_toggles.Text = "Toggle Skills";
            this.tabPage_toggles.UseVisualStyleBackColor = true;
            // 
            // label32
            // 
            this.label32.Location = new System.Drawing.Point(384, 6);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(48, 16);
            this.label32.TabIndex = 93;
            this.label32.Text = "XX < %";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_lesserthen_toggle
            // 
            this.textBox_lesserthen_toggle.Location = new System.Drawing.Point(378, 25);
            this.textBox_lesserthen_toggle.MaxLength = 4;
            this.textBox_lesserthen_toggle.Name = "textBox_lesserthen_toggle";
            this.textBox_lesserthen_toggle.Size = new System.Drawing.Size(60, 20);
            this.textBox_lesserthen_toggle.TabIndex = 92;
            this.textBox_lesserthen_toggle.Text = "100";
            this.textBox_lesserthen_toggle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBox_skills_toggle
            // 
            this.comboBox_skills_toggle.Location = new System.Drawing.Point(169, 25);
            this.comboBox_skills_toggle.Name = "comboBox_skills_toggle";
            this.comboBox_skills_toggle.Size = new System.Drawing.Size(137, 21);
            this.comboBox_skills_toggle.TabIndex = 91;
            // 
            // button_update_toggle
            // 
            this.button_update_toggle.Enabled = false;
            this.button_update_toggle.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_update_toggle.Location = new System.Drawing.Point(244, 54);
            this.button_update_toggle.Name = "button_update_toggle";
            this.button_update_toggle.Size = new System.Drawing.Size(128, 24);
            this.button_update_toggle.TabIndex = 81;
            this.button_update_toggle.Text = "Update";
            this.button_update_toggle.Click += new System.EventHandler(this.button_update_toggle_Click);
            // 
            // button_add_toggle
            // 
            this.button_add_toggle.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_add_toggle.Location = new System.Drawing.Point(92, 54);
            this.button_add_toggle.Name = "button_add_toggle";
            this.button_add_toggle.Size = new System.Drawing.Size(128, 24);
            this.button_add_toggle.TabIndex = 80;
            this.button_add_toggle.Text = "Add";
            this.button_add_toggle.Click += new System.EventHandler(this.button_add_toggle_Click);
            // 
            // textBox_greaterthen_toggle
            // 
            this.textBox_greaterthen_toggle.Location = new System.Drawing.Point(312, 25);
            this.textBox_greaterthen_toggle.MaxLength = 4;
            this.textBox_greaterthen_toggle.Name = "textBox_greaterthen_toggle";
            this.textBox_greaterthen_toggle.Size = new System.Drawing.Size(60, 20);
            this.textBox_greaterthen_toggle.TabIndex = 79;
            this.textBox_greaterthen_toggle.Text = "100";
            this.textBox_greaterthen_toggle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label35
            // 
            this.label35.Location = new System.Drawing.Point(10, 9);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(56, 16);
            this.label35.TabIndex = 87;
            this.label35.Text = "On";
            this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label36
            // 
            this.label36.Location = new System.Drawing.Point(43, 9);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(120, 16);
            this.label36.TabIndex = 86;
            this.label36.Text = "Trait";
            this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label38
            // 
            this.label38.Location = new System.Drawing.Point(319, 6);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(48, 16);
            this.label38.TabIndex = 84;
            this.label38.Text = "XX > %";
            this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label39
            // 
            this.label39.Location = new System.Drawing.Point(203, 9);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(72, 16);
            this.label39.TabIndex = 83;
            this.label39.Text = "Skill";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox_trait_toggle
            // 
            this.comboBox_trait_toggle.Items.AddRange(new object[] {
            "HP",
            "MP"});
            this.comboBox_trait_toggle.Location = new System.Drawing.Point(43, 25);
            this.comboBox_trait_toggle.Name = "comboBox_trait_toggle";
            this.comboBox_trait_toggle.Size = new System.Drawing.Size(120, 21);
            this.comboBox_trait_toggle.TabIndex = 76;
            // 
            // checkBox_onoff_toggle
            // 
            this.checkBox_onoff_toggle.Checked = true;
            this.checkBox_onoff_toggle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_onoff_toggle.Location = new System.Drawing.Point(13, 25);
            this.checkBox_onoff_toggle.Name = "checkBox_onoff_toggle";
            this.checkBox_onoff_toggle.Size = new System.Drawing.Size(24, 24);
            this.checkBox_onoff_toggle.TabIndex = 73;
            // 
            // listView_toggles
            // 
            this.listView_toggles.CheckBoxes = true;
            this.listView_toggles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_Toggle_Skill,
            this.columnHeader_Toggle_Trait,
            this.columnHeader_Toggle_LesserThen,
            this.columnHeader_Toggle_Biggerthan,
            this.columnHeader_Toggle_TraitID,
            this.columnHeader_SkillID});
            this.listView_toggles.ContextMenuStrip = this.contextMenuStrip_toggle;
            this.listView_toggles.FullRowSelect = true;
            this.listView_toggles.GridLines = true;
            this.listView_toggles.Location = new System.Drawing.Point(13, 84);
            this.listView_toggles.MultiSelect = false;
            this.listView_toggles.Name = "listView_toggles";
            this.listView_toggles.Size = new System.Drawing.Size(488, 276);
            this.listView_toggles.TabIndex = 82;
            this.listView_toggles.UseCompatibleStateImageBehavior = false;
            this.listView_toggles.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader_Toggle_Skill
            // 
            this.columnHeader_Toggle_Skill.Text = "Skill";
            this.columnHeader_Toggle_Skill.Width = 186;
            // 
            // columnHeader_Toggle_Trait
            // 
            this.columnHeader_Toggle_Trait.Text = "Trait";
            this.columnHeader_Toggle_Trait.Width = 78;
            // 
            // columnHeader_Toggle_LesserThen
            // 
            this.columnHeader_Toggle_LesserThen.DisplayIndex = 3;
            this.columnHeader_Toggle_LesserThen.Text = "XX<%";
            this.columnHeader_Toggle_LesserThen.Width = 38;
            // 
            // columnHeader_Toggle_Biggerthan
            // 
            this.columnHeader_Toggle_Biggerthan.DisplayIndex = 2;
            this.columnHeader_Toggle_Biggerthan.Text = "XX>%";
            this.columnHeader_Toggle_Biggerthan.Width = 51;
            // 
            // columnHeader_Toggle_TraitID
            // 
            this.columnHeader_Toggle_TraitID.Text = "TraitID";
            this.columnHeader_Toggle_TraitID.Width = 48;
            // 
            // columnHeader_SkillID
            // 
            this.columnHeader_SkillID.Text = "SkillID";
            // 
            // contextMenuStrip_toggle
            // 
            this.contextMenuStrip_toggle.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip_toggle.Name = "contextMenuStrip1";
            this.contextMenuStrip_toggle.Size = new System.Drawing.Size(153, 48);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem1.Text = "Remove";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // tabPage_items
            // 
            this.tabPage_items.Controls.Add(this.button_autoss_deactivate);
            this.tabPage_items.Controls.Add(this.button_autoss_activate);
            this.tabPage_items.Controls.Add(this.combobox_autoss);
            this.tabPage_items.Controls.Add(this.label_autoss);
            this.tabPage_items.Controls.Add(this.button_updateitem);
            this.tabPage_items.Controls.Add(this.button_additem);
            this.tabPage_items.Controls.Add(this.listView_item);
            this.tabPage_items.Controls.Add(this.label21);
            this.tabPage_items.Controls.Add(this.comboBox_trait1);
            this.tabPage_items.Controls.Add(this.label_on4);
            this.tabPage_items.Controls.Add(this.label_delaymsec);
            this.tabPage_items.Controls.Add(this.label20);
            this.tabPage_items.Controls.Add(this.label22);
            this.tabPage_items.Controls.Add(this.checkBox_item1);
            this.tabPage_items.Controls.Add(this.textBox_itemdelay1);
            this.tabPage_items.Controls.Add(this.textBox_itemper1);
            this.tabPage_items.Controls.Add(this.comboBox_item1);
            this.tabPage_items.Location = new System.Drawing.Point(104, 4);
            this.tabPage_items.Name = "tabPage_items";
            this.tabPage_items.Size = new System.Drawing.Size(515, 377);
            this.tabPage_items.TabIndex = 3;
            this.tabPage_items.Text = "Items";
            this.tabPage_items.UseVisualStyleBackColor = true;
            // 
            // button_autoss_deactivate
            // 
            this.button_autoss_deactivate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_autoss_deactivate.Location = new System.Drawing.Point(401, 22);
            this.button_autoss_deactivate.Name = "button_autoss_deactivate";
            this.button_autoss_deactivate.Size = new System.Drawing.Size(104, 24);
            this.button_autoss_deactivate.TabIndex = 35;
            this.button_autoss_deactivate.Text = "Deactivate";
            this.button_autoss_deactivate.Click += new System.EventHandler(this.button_autoss_deactivate_Click);
            // 
            // button_autoss_activate
            // 
            this.button_autoss_activate.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_autoss_activate.Location = new System.Drawing.Point(291, 22);
            this.button_autoss_activate.Name = "button_autoss_activate";
            this.button_autoss_activate.Size = new System.Drawing.Size(104, 24);
            this.button_autoss_activate.TabIndex = 34;
            this.button_autoss_activate.Text = "Activate";
            this.button_autoss_activate.Click += new System.EventHandler(this.button_autoss_activate_Click);
            // 
            // combobox_autoss
            // 
            this.combobox_autoss.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.combobox_autoss.DropDownWidth = 250;
            this.combobox_autoss.Location = new System.Drawing.Point(117, 25);
            this.combobox_autoss.Name = "combobox_autoss";
            this.combobox_autoss.Size = new System.Drawing.Size(168, 21);
            this.combobox_autoss.TabIndex = 33;
            // 
            // label_autoss
            // 
            this.label_autoss.AutoSize = true;
            this.label_autoss.Location = new System.Drawing.Point(10, 28);
            this.label_autoss.Name = "label_autoss";
            this.label_autoss.Size = new System.Drawing.Size(101, 13);
            this.label_autoss.TabIndex = 32;
            this.label_autoss.Text = "Auto Soul/Spiritshot";
            // 
            // button_updateitem
            // 
            this.button_updateitem.Enabled = false;
            this.button_updateitem.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_updateitem.Location = new System.Drawing.Point(247, 101);
            this.button_updateitem.Name = "button_updateitem";
            this.button_updateitem.Size = new System.Drawing.Size(128, 24);
            this.button_updateitem.TabIndex = 6;
            this.button_updateitem.Text = "Update";
            this.button_updateitem.Click += new System.EventHandler(this.button_updateitem_Click);
            // 
            // button_additem
            // 
            this.button_additem.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_additem.Location = new System.Drawing.Point(95, 101);
            this.button_additem.Name = "button_additem";
            this.button_additem.Size = new System.Drawing.Size(128, 24);
            this.button_additem.TabIndex = 5;
            this.button_additem.Text = "Add";
            this.button_additem.Click += new System.EventHandler(this.button_additem_Click);
            // 
            // listView_item
            // 
            this.listView_item.CheckBoxes = true;
            this.listView_item.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_i_item,
            this.columnHeader_i_trait,
            this.columnHeader_i_per,
            this.columnHeader_i_delay,
            this.columnHeader_i_traitid,
            columnHeader_i_itemid});
            this.listView_item.ContextMenuStrip = this.contextMenuStrip_item;
            this.listView_item.FullRowSelect = true;
            this.listView_item.GridLines = true;
            this.listView_item.Location = new System.Drawing.Point(15, 141);
            this.listView_item.MultiSelect = false;
            this.listView_item.Name = "listView_item";
            this.listView_item.Size = new System.Drawing.Size(488, 224);
            this.listView_item.TabIndex = 7;
            this.listView_item.UseCompatibleStateImageBehavior = false;
            this.listView_item.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader_i_item
            // 
            this.columnHeader_i_item.Text = "Item";
            this.columnHeader_i_item.Width = 261;
            // 
            // columnHeader_i_trait
            // 
            this.columnHeader_i_trait.Text = "Trait";
            this.columnHeader_i_trait.Width = 61;
            // 
            // columnHeader_i_per
            // 
            this.columnHeader_i_per.Text = "XX<%";
            this.columnHeader_i_per.Width = 41;
            // 
            // columnHeader_i_delay
            // 
            this.columnHeader_i_delay.Text = "Delay(msec)";
            this.columnHeader_i_delay.Width = 93;
            // 
            // columnHeader_i_traitid
            // 
            this.columnHeader_i_traitid.Text = "TraitID";
            this.columnHeader_i_traitid.Width = 0;
            // 
            // contextMenuStrip_item
            // 
            this.contextMenuStrip_item.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem3,
            this.moveUpToolStripMenuItem1});
            this.contextMenuStrip_item.Name = "contextMenuStrip_item";
            this.contextMenuStrip_item.Size = new System.Drawing.Size(123, 48);
            // 
            // removeToolStripMenuItem3
            // 
            this.removeToolStripMenuItem3.Name = "removeToolStripMenuItem3";
            this.removeToolStripMenuItem3.Size = new System.Drawing.Size(122, 22);
            this.removeToolStripMenuItem3.Text = "Remove";
            this.removeToolStripMenuItem3.Click += new System.EventHandler(this.removeToolStripMenuItem3_Click);
            // 
            // moveUpToolStripMenuItem1
            // 
            this.moveUpToolStripMenuItem1.Name = "moveUpToolStripMenuItem1";
            this.moveUpToolStripMenuItem1.Size = new System.Drawing.Size(122, 22);
            this.moveUpToolStripMenuItem1.Text = "Move Up";
            this.moveUpToolStripMenuItem1.Click += new System.EventHandler(this.moveUpToolStripMenuItem1_Click);
            // 
            // label21
            // 
            this.label21.Location = new System.Drawing.Point(42, 58);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(120, 16);
            this.label21.TabIndex = 31;
            this.label21.Text = "Trait";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox_trait1
            // 
            this.comboBox_trait1.Items.AddRange(new object[] {
            "Always",
            "CP",
            "HP",
            "MP",
            "Dead",
            "Charges",
            "Souls",
            "Death Penalty",
            "Bleeding",
            "Poisoned",
            "Iced",
            "Shackled",
            "Feared",
            "Stunned",
            "Slept",
            "Muted",
            "Rooted",
            "Paralyzed",
            "Petrified",
            "Ultimate Defense"});
            this.comboBox_trait1.Location = new System.Drawing.Point(42, 74);
            this.comboBox_trait1.Name = "comboBox_trait1";
            this.comboBox_trait1.Size = new System.Drawing.Size(120, 21);
            this.comboBox_trait1.TabIndex = 1;
            // 
            // label_on4
            // 
            this.label_on4.Location = new System.Drawing.Point(8, 58);
            this.label_on4.Name = "label_on4";
            this.label_on4.Size = new System.Drawing.Size(40, 16);
            this.label_on4.TabIndex = 29;
            this.label_on4.Text = "On";
            this.label_on4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_delaymsec
            // 
            this.label_delaymsec.Location = new System.Drawing.Point(403, 58);
            this.label_delaymsec.Name = "label_delaymsec";
            this.label_delaymsec.Size = new System.Drawing.Size(95, 16);
            this.label_delaymsec.TabIndex = 28;
            this.label_delaymsec.Text = "Delay(msec)";
            this.label_delaymsec.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label20
            // 
            this.label20.Location = new System.Drawing.Point(349, 58);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(48, 16);
            this.label20.TabIndex = 27;
            this.label20.Text = "XX < %";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label22
            // 
            this.label22.Location = new System.Drawing.Point(172, 58);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(168, 16);
            this.label22.TabIndex = 25;
            this.label22.Text = "Item";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBox_item1
            // 
            this.checkBox_item1.Location = new System.Drawing.Point(10, 74);
            this.checkBox_item1.Name = "checkBox_item1";
            this.checkBox_item1.Size = new System.Drawing.Size(24, 24);
            this.checkBox_item1.TabIndex = 0;
            // 
            // textBox_itemdelay1
            // 
            this.textBox_itemdelay1.Location = new System.Drawing.Point(403, 74);
            this.textBox_itemdelay1.MaxLength = 12;
            this.textBox_itemdelay1.Name = "textBox_itemdelay1";
            this.textBox_itemdelay1.Size = new System.Drawing.Size(95, 20);
            this.textBox_itemdelay1.TabIndex = 4;
            this.textBox_itemdelay1.Text = "100";
            this.textBox_itemdelay1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_itemper1
            // 
            this.textBox_itemper1.Location = new System.Drawing.Point(349, 74);
            this.textBox_itemper1.MaxLength = 3;
            this.textBox_itemper1.Name = "textBox_itemper1";
            this.textBox_itemper1.Size = new System.Drawing.Size(48, 20);
            this.textBox_itemper1.TabIndex = 3;
            this.textBox_itemper1.Text = "60";
            this.textBox_itemper1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBox_item1
            // 
            this.comboBox_item1.DropDownWidth = 250;
            this.comboBox_item1.Location = new System.Drawing.Point(172, 74);
            this.comboBox_item1.Name = "comboBox_item1";
            this.comboBox_item1.Size = new System.Drawing.Size(168, 21);
            this.comboBox_item1.TabIndex = 2;
            // 
            // tabPage_combat
            // 
            this.tabPage_combat.Controls.Add(this.button_combat_update);
            this.tabPage_combat.Controls.Add(this.button_combat_add);
            this.tabPage_combat.Controls.Add(this.label_combat_conditional);
            this.tabPage_combat.Controls.Add(this.comboBox_combat_conditional);
            this.tabPage_combat.Controls.Add(this.label_combat_page);
            this.tabPage_combat.Controls.Add(this.textBox_combat_sc_page);
            this.tabPage_combat.Controls.Add(this.textBox_combat_sc_item);
            this.tabPage_combat.Controls.Add(this.label_combat_mp);
            this.tabPage_combat.Controls.Add(this.textBox_combat_mp);
            this.tabPage_combat.Controls.Add(this.label_combat_trait);
            this.tabPage_combat.Controls.Add(this.label_combat_delay);
            this.tabPage_combat.Controls.Add(this.label_combat_percent);
            this.tabPage_combat.Controls.Add(this.label_combat_shortcut);
            this.tabPage_combat.Controls.Add(this.comboBox_combat_trait);
            this.tabPage_combat.Controls.Add(this.textBox_combat_delay);
            this.tabPage_combat.Controls.Add(this.textBox_combat_min_per);
            this.tabPage_combat.Controls.Add(this.label_combat_on);
            this.tabPage_combat.Controls.Add(this.checkBox_combat_on);
            this.tabPage_combat.Controls.Add(this.listView_combat);
            this.tabPage_combat.Location = new System.Drawing.Point(104, 4);
            this.tabPage_combat.Name = "tabPage_combat";
            this.tabPage_combat.Size = new System.Drawing.Size(515, 377);
            this.tabPage_combat.TabIndex = 6;
            this.tabPage_combat.Text = "Combat";
            this.tabPage_combat.UseVisualStyleBackColor = true;
            // 
            // button_combat_update
            // 
            this.button_combat_update.Enabled = false;
            this.button_combat_update.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_combat_update.Location = new System.Drawing.Point(211, 96);
            this.button_combat_update.Name = "button_combat_update";
            this.button_combat_update.Size = new System.Drawing.Size(128, 24);
            this.button_combat_update.TabIndex = 85;
            this.button_combat_update.Text = "Update";
            this.button_combat_update.Click += new System.EventHandler(this.button_combat_update_Click);
            // 
            // button_combat_add
            // 
            this.button_combat_add.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_combat_add.Location = new System.Drawing.Point(68, 96);
            this.button_combat_add.Name = "button_combat_add";
            this.button_combat_add.Size = new System.Drawing.Size(128, 24);
            this.button_combat_add.TabIndex = 84;
            this.button_combat_add.Text = "Add";
            this.button_combat_add.Click += new System.EventHandler(this.button_combat_add_Click);
            // 
            // label_combat_conditional
            // 
            this.label_combat_conditional.Location = new System.Drawing.Point(183, 10);
            this.label_combat_conditional.Name = "label_combat_conditional";
            this.label_combat_conditional.Size = new System.Drawing.Size(120, 16);
            this.label_combat_conditional.TabIndex = 83;
            this.label_combat_conditional.Text = "Conditional";
            this.label_combat_conditional.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox_combat_conditional
            // 
            this.comboBox_combat_conditional.Items.AddRange(new object[] {
            ">=",
            "<="});
            this.comboBox_combat_conditional.Location = new System.Drawing.Point(183, 26);
            this.comboBox_combat_conditional.Name = "comboBox_combat_conditional";
            this.comboBox_combat_conditional.Size = new System.Drawing.Size(120, 21);
            this.comboBox_combat_conditional.TabIndex = 82;
            // 
            // label_combat_page
            // 
            this.label_combat_page.Location = new System.Drawing.Point(107, 50);
            this.label_combat_page.Name = "label_combat_page";
            this.label_combat_page.Size = new System.Drawing.Size(64, 16);
            this.label_combat_page.TabIndex = 81;
            this.label_combat_page.Text = "Page";
            this.label_combat_page.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_combat_sc_page
            // 
            this.textBox_combat_sc_page.Location = new System.Drawing.Point(115, 66);
            this.textBox_combat_sc_page.MaxLength = 2;
            this.textBox_combat_sc_page.Name = "textBox_combat_sc_page";
            this.textBox_combat_sc_page.Size = new System.Drawing.Size(48, 20);
            this.textBox_combat_sc_page.TabIndex = 72;
            this.textBox_combat_sc_page.Text = "1";
            this.textBox_combat_sc_page.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_combat_sc_item
            // 
            this.textBox_combat_sc_item.Location = new System.Drawing.Point(35, 66);
            this.textBox_combat_sc_item.MaxLength = 2;
            this.textBox_combat_sc_item.Name = "textBox_combat_sc_item";
            this.textBox_combat_sc_item.Size = new System.Drawing.Size(48, 20);
            this.textBox_combat_sc_item.TabIndex = 71;
            this.textBox_combat_sc_item.Text = "1";
            this.textBox_combat_sc_item.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_combat_mp
            // 
            this.label_combat_mp.Location = new System.Drawing.Point(312, 50);
            this.label_combat_mp.Name = "label_combat_mp";
            this.label_combat_mp.Size = new System.Drawing.Size(45, 16);
            this.label_combat_mp.TabIndex = 80;
            this.label_combat_mp.Text = "MP>";
            this.label_combat_mp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_combat_mp
            // 
            this.textBox_combat_mp.Location = new System.Drawing.Point(309, 66);
            this.textBox_combat_mp.MaxLength = 4;
            this.textBox_combat_mp.Name = "textBox_combat_mp";
            this.textBox_combat_mp.Size = new System.Drawing.Size(48, 20);
            this.textBox_combat_mp.TabIndex = 75;
            this.textBox_combat_mp.Text = "100";
            this.textBox_combat_mp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_combat_trait
            // 
            this.label_combat_trait.Location = new System.Drawing.Point(57, 10);
            this.label_combat_trait.Name = "label_combat_trait";
            this.label_combat_trait.Size = new System.Drawing.Size(120, 16);
            this.label_combat_trait.TabIndex = 79;
            this.label_combat_trait.Text = "Trait";
            this.label_combat_trait.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_combat_delay
            // 
            this.label_combat_delay.Location = new System.Drawing.Point(183, 50);
            this.label_combat_delay.Name = "label_combat_delay";
            this.label_combat_delay.Size = new System.Drawing.Size(120, 16);
            this.label_combat_delay.TabIndex = 78;
            this.label_combat_delay.Text = "Delay(msec)";
            this.label_combat_delay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_combat_percent
            // 
            this.label_combat_percent.Location = new System.Drawing.Point(309, 10);
            this.label_combat_percent.Name = "label_combat_percent";
            this.label_combat_percent.Size = new System.Drawing.Size(48, 16);
            this.label_combat_percent.TabIndex = 77;
            this.label_combat_percent.Text = "%";
            this.label_combat_percent.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_combat_shortcut
            // 
            this.label_combat_shortcut.Location = new System.Drawing.Point(27, 50);
            this.label_combat_shortcut.Name = "label_combat_shortcut";
            this.label_combat_shortcut.Size = new System.Drawing.Size(72, 16);
            this.label_combat_shortcut.TabIndex = 76;
            this.label_combat_shortcut.Text = "ShortCut";
            this.label_combat_shortcut.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox_combat_trait
            // 
            this.comboBox_combat_trait.Items.AddRange(new object[] {
            "Always",
            "CP",
            "HP",
            "MP",
            "Dead",
            "Charges",
            "Souls",
            "Death Penalty"});
            this.comboBox_combat_trait.Location = new System.Drawing.Point(57, 26);
            this.comboBox_combat_trait.Name = "comboBox_combat_trait";
            this.comboBox_combat_trait.Size = new System.Drawing.Size(120, 21);
            this.comboBox_combat_trait.TabIndex = 70;
            // 
            // textBox_combat_delay
            // 
            this.textBox_combat_delay.Location = new System.Drawing.Point(183, 66);
            this.textBox_combat_delay.MaxLength = 8;
            this.textBox_combat_delay.Name = "textBox_combat_delay";
            this.textBox_combat_delay.Size = new System.Drawing.Size(120, 20);
            this.textBox_combat_delay.TabIndex = 74;
            this.textBox_combat_delay.Text = "1";
            this.textBox_combat_delay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_combat_min_per
            // 
            this.textBox_combat_min_per.Location = new System.Drawing.Point(309, 26);
            this.textBox_combat_min_per.MaxLength = 3;
            this.textBox_combat_min_per.Name = "textBox_combat_min_per";
            this.textBox_combat_min_per.Size = new System.Drawing.Size(48, 20);
            this.textBox_combat_min_per.TabIndex = 73;
            this.textBox_combat_min_per.Text = "60";
            this.textBox_combat_min_per.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_combat_on
            // 
            this.label_combat_on.Location = new System.Drawing.Point(20, 10);
            this.label_combat_on.Name = "label_combat_on";
            this.label_combat_on.Size = new System.Drawing.Size(56, 16);
            this.label_combat_on.TabIndex = 63;
            this.label_combat_on.Text = "On";
            this.label_combat_on.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBox_combat_on
            // 
            this.checkBox_combat_on.Checked = true;
            this.checkBox_combat_on.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_combat_on.Location = new System.Drawing.Point(25, 26);
            this.checkBox_combat_on.Name = "checkBox_combat_on";
            this.checkBox_combat_on.Size = new System.Drawing.Size(24, 24);
            this.checkBox_combat_on.TabIndex = 62;
            // 
            // listView_combat
            // 
            this.listView_combat.CheckBoxes = true;
            this.listView_combat.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_combat_trait,
            this.columnHeader_combat_conditional,
            this.columnHeader_combat_percent,
            this.columnHeader_combat_shortcut,
            this.columnHeader_combat_delay,
            this.columnHeader_combat_mp,
            this.columnHeader_combat_traitID,
            this.columnHeader_combat_conditionalID,
            this.columnHeader_combat_shortcutID});
            this.listView_combat.ContextMenuStrip = this.contextMenuStrip_combat;
            this.listView_combat.FullRowSelect = true;
            this.listView_combat.GridLines = true;
            this.listView_combat.Location = new System.Drawing.Point(8, 131);
            this.listView_combat.MultiSelect = false;
            this.listView_combat.Name = "listView_combat";
            this.listView_combat.Size = new System.Drawing.Size(488, 224);
            this.listView_combat.TabIndex = 12;
            this.listView_combat.UseCompatibleStateImageBehavior = false;
            this.listView_combat.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader_combat_trait
            // 
            this.columnHeader_combat_trait.Text = "Trait";
            this.columnHeader_combat_trait.Width = 77;
            // 
            // columnHeader_combat_conditional
            // 
            this.columnHeader_combat_conditional.Text = "Conditional";
            this.columnHeader_combat_conditional.Width = 70;
            // 
            // columnHeader_combat_percent
            // 
            this.columnHeader_combat_percent.Text = "%";
            this.columnHeader_combat_percent.Width = 41;
            // 
            // columnHeader_combat_shortcut
            // 
            this.columnHeader_combat_shortcut.Text = "ShortCut";
            this.columnHeader_combat_shortcut.Width = 62;
            // 
            // columnHeader_combat_delay
            // 
            this.columnHeader_combat_delay.Text = "Delay(msec)";
            this.columnHeader_combat_delay.Width = 78;
            // 
            // columnHeader_combat_mp
            // 
            this.columnHeader_combat_mp.Text = "MP>";
            this.columnHeader_combat_mp.Width = 47;
            // 
            // columnHeader_combat_traitID
            // 
            this.columnHeader_combat_traitID.Text = "TraitID";
            this.columnHeader_combat_traitID.Width = 0;
            // 
            // columnHeader_combat_conditionalID
            // 
            this.columnHeader_combat_conditionalID.Text = "ConditionalID";
            this.columnHeader_combat_conditionalID.Width = 0;
            // 
            // columnHeader_combat_shortcutID
            // 
            this.columnHeader_combat_shortcutID.Text = "ShortCutID";
            this.columnHeader_combat_shortcutID.Width = 0;
            // 
            // contextMenuStrip_combat
            // 
            this.contextMenuStrip_combat.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem5,
            this.moveUpToolStripMenuItem2});
            this.contextMenuStrip_combat.Name = "contextMenuStrip_combat";
            this.contextMenuStrip_combat.Size = new System.Drawing.Size(123, 48);
            // 
            // removeToolStripMenuItem5
            // 
            this.removeToolStripMenuItem5.Name = "removeToolStripMenuItem5";
            this.removeToolStripMenuItem5.Size = new System.Drawing.Size(122, 22);
            this.removeToolStripMenuItem5.Text = "Remove";
            this.removeToolStripMenuItem5.Click += new System.EventHandler(this.removeToolStripMenuItem5_Click);
            // 
            // moveUpToolStripMenuItem2
            // 
            this.moveUpToolStripMenuItem2.Name = "moveUpToolStripMenuItem2";
            this.moveUpToolStripMenuItem2.Size = new System.Drawing.Size(122, 22);
            this.moveUpToolStripMenuItem2.Text = "Move Up";
            this.moveUpToolStripMenuItem2.Click += new System.EventHandler(this.moveUpToolStripMenuItem2_Click);
            // 
            // tabPage_polygon
            // 
            this.tabPage_polygon.Controls.Add(this.button_box_generate);
            this.tabPage_polygon.Controls.Add(this.label_box_offset);
            this.tabPage_polygon.Controls.Add(this.textBox_box_offset);
            this.tabPage_polygon.Controls.Add(this.label_box_sides);
            this.tabPage_polygon.Controls.Add(this.textBox_box_sides);
            this.tabPage_polygon.Controls.Add(this.label_box_radius);
            this.tabPage_polygon.Controls.Add(this.textBox_box_radius);
            this.tabPage_polygon.Controls.Add(this.label_zrange);
            this.tabPage_polygon.Controls.Add(this.textBox_zrange);
            this.tabPage_polygon.Controls.Add(this.button_addcur_polygon);
            this.tabPage_polygon.Controls.Add(this.label_polgon_y);
            this.tabPage_polygon.Controls.Add(this.textBox_polygon_y);
            this.tabPage_polygon.Controls.Add(this.textBox_polygon_x);
            this.tabPage_polygon.Controls.Add(this.label_polygon_x);
            this.tabPage_polygon.Controls.Add(this.button_updatepolygon);
            this.tabPage_polygon.Controls.Add(this.button_addpolygon);
            this.tabPage_polygon.Controls.Add(this.listView_border);
            this.tabPage_polygon.Location = new System.Drawing.Point(104, 4);
            this.tabPage_polygon.Name = "tabPage_polygon";
            this.tabPage_polygon.Size = new System.Drawing.Size(515, 377);
            this.tabPage_polygon.TabIndex = 5;
            this.tabPage_polygon.Text = "Bounding Polygon";
            this.tabPage_polygon.UseVisualStyleBackColor = true;
            // 
            // button_box_generate
            // 
            this.button_box_generate.Location = new System.Drawing.Point(76, 307);
            this.button_box_generate.Name = "button_box_generate";
            this.button_box_generate.Size = new System.Drawing.Size(128, 24);
            this.button_box_generate.TabIndex = 82;
            this.button_box_generate.Text = "Generate Box";
            this.button_box_generate.Click += new System.EventHandler(this.button_box_generate_Click);
            // 
            // label_box_offset
            // 
            this.label_box_offset.Location = new System.Drawing.Point(114, 277);
            this.label_box_offset.Name = "label_box_offset";
            this.label_box_offset.Size = new System.Drawing.Size(157, 24);
            this.label_box_offset.TabIndex = 80;
            this.label_box_offset.Text = "Offset";
            this.label_box_offset.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox_box_offset
            // 
            this.textBox_box_offset.Location = new System.Drawing.Point(8, 281);
            this.textBox_box_offset.Name = "textBox_box_offset";
            this.textBox_box_offset.Size = new System.Drawing.Size(100, 20);
            this.textBox_box_offset.TabIndex = 81;
            this.textBox_box_offset.Text = "0";
            this.textBox_box_offset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_box_sides
            // 
            this.label_box_sides.Location = new System.Drawing.Point(114, 251);
            this.label_box_sides.Name = "label_box_sides";
            this.label_box_sides.Size = new System.Drawing.Size(157, 24);
            this.label_box_sides.TabIndex = 78;
            this.label_box_sides.Text = "Sides";
            this.label_box_sides.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox_box_sides
            // 
            this.textBox_box_sides.Location = new System.Drawing.Point(8, 255);
            this.textBox_box_sides.Name = "textBox_box_sides";
            this.textBox_box_sides.Size = new System.Drawing.Size(100, 20);
            this.textBox_box_sides.TabIndex = 79;
            this.textBox_box_sides.Text = "4";
            this.textBox_box_sides.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_box_radius
            // 
            this.label_box_radius.Location = new System.Drawing.Point(114, 225);
            this.label_box_radius.Name = "label_box_radius";
            this.label_box_radius.Size = new System.Drawing.Size(157, 24);
            this.label_box_radius.TabIndex = 76;
            this.label_box_radius.Text = "Radius";
            this.label_box_radius.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox_box_radius
            // 
            this.textBox_box_radius.Location = new System.Drawing.Point(8, 229);
            this.textBox_box_radius.Name = "textBox_box_radius";
            this.textBox_box_radius.Size = new System.Drawing.Size(100, 20);
            this.textBox_box_radius.TabIndex = 77;
            this.textBox_box_radius.Text = "500";
            this.textBox_box_radius.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_zrange
            // 
            this.label_zrange.Location = new System.Drawing.Point(114, 122);
            this.label_zrange.Name = "label_zrange";
            this.label_zrange.Size = new System.Drawing.Size(157, 24);
            this.label_zrange.TabIndex = 74;
            this.label_zrange.Text = "Z Range";
            this.label_zrange.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox_zrange
            // 
            this.textBox_zrange.Location = new System.Drawing.Point(8, 126);
            this.textBox_zrange.Name = "textBox_zrange";
            this.textBox_zrange.Size = new System.Drawing.Size(100, 20);
            this.textBox_zrange.TabIndex = 75;
            this.textBox_zrange.Text = "1000";
            this.textBox_zrange.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button_addcur_polygon
            // 
            this.button_addcur_polygon.Location = new System.Drawing.Point(76, 95);
            this.button_addcur_polygon.Name = "button_addcur_polygon";
            this.button_addcur_polygon.Size = new System.Drawing.Size(128, 24);
            this.button_addcur_polygon.TabIndex = 4;
            this.button_addcur_polygon.Text = "Add Current Location";
            this.button_addcur_polygon.Click += new System.EventHandler(this.button_addcur_polygon_Click);
            // 
            // label_polgon_y
            // 
            this.label_polgon_y.Location = new System.Drawing.Point(170, 22);
            this.label_polgon_y.Name = "label_polgon_y";
            this.label_polgon_y.Size = new System.Drawing.Size(64, 16);
            this.label_polgon_y.TabIndex = 73;
            this.label_polgon_y.Text = "Y";
            this.label_polgon_y.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_polygon_y
            // 
            this.textBox_polygon_y.Location = new System.Drawing.Point(152, 38);
            this.textBox_polygon_y.MaxLength = 13;
            this.textBox_polygon_y.Name = "textBox_polygon_y";
            this.textBox_polygon_y.Size = new System.Drawing.Size(100, 20);
            this.textBox_polygon_y.TabIndex = 1;
            this.textBox_polygon_y.Text = "0";
            this.textBox_polygon_y.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_polygon_x
            // 
            this.textBox_polygon_x.Location = new System.Drawing.Point(38, 38);
            this.textBox_polygon_x.MaxLength = 13;
            this.textBox_polygon_x.Name = "textBox_polygon_x";
            this.textBox_polygon_x.Size = new System.Drawing.Size(100, 20);
            this.textBox_polygon_x.TabIndex = 0;
            this.textBox_polygon_x.Text = "0";
            this.textBox_polygon_x.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_polygon_x
            // 
            this.label_polygon_x.Location = new System.Drawing.Point(53, 22);
            this.label_polygon_x.Name = "label_polygon_x";
            this.label_polygon_x.Size = new System.Drawing.Size(72, 16);
            this.label_polygon_x.TabIndex = 72;
            this.label_polygon_x.Text = "X";
            this.label_polygon_x.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_updatepolygon
            // 
            this.button_updatepolygon.Enabled = false;
            this.button_updatepolygon.Location = new System.Drawing.Point(146, 65);
            this.button_updatepolygon.Name = "button_updatepolygon";
            this.button_updatepolygon.Size = new System.Drawing.Size(128, 24);
            this.button_updatepolygon.TabIndex = 3;
            this.button_updatepolygon.Text = "Update";
            this.button_updatepolygon.Click += new System.EventHandler(this.button_updatepolygon_Click);
            // 
            // button_addpolygon
            // 
            this.button_addpolygon.Location = new System.Drawing.Point(11, 65);
            this.button_addpolygon.Name = "button_addpolygon";
            this.button_addpolygon.Size = new System.Drawing.Size(128, 24);
            this.button_addpolygon.TabIndex = 2;
            this.button_addpolygon.Text = "Add";
            this.button_addpolygon.Click += new System.EventHandler(this.button_addpolygon_Click);
            // 
            // listView_border
            // 
            this.listView_border.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_x,
            this.columnHeader_y});
            this.listView_border.ContextMenuStrip = this.contextMenuStrip_polygon;
            this.listView_border.FullRowSelect = true;
            this.listView_border.GridLines = true;
            this.listView_border.Location = new System.Drawing.Point(277, 19);
            this.listView_border.MultiSelect = false;
            this.listView_border.Name = "listView_border";
            this.listView_border.Size = new System.Drawing.Size(205, 323);
            this.listView_border.TabIndex = 5;
            this.listView_border.UseCompatibleStateImageBehavior = false;
            this.listView_border.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader_x
            // 
            this.columnHeader_x.Text = "X";
            this.columnHeader_x.Width = 100;
            // 
            // columnHeader_y
            // 
            this.columnHeader_y.Text = "Y";
            this.columnHeader_y.Width = 100;
            // 
            // contextMenuStrip_polygon
            // 
            this.contextMenuStrip_polygon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem4,
            this.removeAllToolStripMenuItem});
            this.contextMenuStrip_polygon.Name = "contextMenuStrip_polygon";
            this.contextMenuStrip_polygon.Size = new System.Drawing.Size(135, 48);
            // 
            // removeToolStripMenuItem4
            // 
            this.removeToolStripMenuItem4.Name = "removeToolStripMenuItem4";
            this.removeToolStripMenuItem4.Size = new System.Drawing.Size(134, 22);
            this.removeToolStripMenuItem4.Text = "Remove";
            this.removeToolStripMenuItem4.Click += new System.EventHandler(this.removeToolStripMenuItem4_Click);
            // 
            // removeAllToolStripMenuItem
            // 
            this.removeAllToolStripMenuItem.Name = "removeAllToolStripMenuItem";
            this.removeAllToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.removeAllToolStripMenuItem.Text = "Remove All";
            this.removeAllToolStripMenuItem.Click += new System.EventHandler(this.removeAllToolStripMenuItem_Click);
            // 
            // tabPage_donot
            // 
            this.tabPage_donot.Controls.Add(this.panel3);
            this.tabPage_donot.Controls.Add(this.panel2);
            this.tabPage_donot.Location = new System.Drawing.Point(104, 4);
            this.tabPage_donot.Name = "tabPage_donot";
            this.tabPage_donot.Size = new System.Drawing.Size(515, 377);
            this.tabPage_donot.TabIndex = 4;
            this.tabPage_donot.Text = "Do Not";
            this.tabPage_donot.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.checkBox_AttackOnly);
            this.panel3.Controls.Add(this.checkBox_Ign_Summons);
            this.panel3.Controls.Add(this.checkBox_Ign_TreasureChests);
            this.panel3.Controls.Add(this.checkBox_Ign_Raidbosses);
            this.panel3.Controls.Add(this.label_donot_npcID);
            this.panel3.Controls.Add(this.label_donot_npcs);
            this.panel3.Controls.Add(this.textBox_donot_npcs);
            this.panel3.Controls.Add(this.button_donot_npcs);
            this.panel3.Controls.Add(this.listView_donot_npcs);
            this.panel3.Location = new System.Drawing.Point(253, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(248, 352);
            this.panel3.TabIndex = 1;
            // 
            // checkBox_AttackOnly
            // 
            this.checkBox_AttackOnly.ForeColor = System.Drawing.Color.Red;
            this.checkBox_AttackOnly.Location = new System.Drawing.Point(5, 271);
            this.checkBox_AttackOnly.Name = "checkBox_AttackOnly";
            this.checkBox_AttackOnly.Size = new System.Drawing.Size(235, 24);
            this.checkBox_AttackOnly.TabIndex = 9;
            this.checkBox_AttackOnly.Text = "Attack Only NPCs in List";
            // 
            // checkBox_Ign_Summons
            // 
            this.checkBox_Ign_Summons.Checked = true;
            this.checkBox_Ign_Summons.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Ign_Summons.Location = new System.Drawing.Point(125, 293);
            this.checkBox_Ign_Summons.Name = "checkBox_Ign_Summons";
            this.checkBox_Ign_Summons.Size = new System.Drawing.Size(115, 24);
            this.checkBox_Ign_Summons.TabIndex = 8;
            this.checkBox_Ign_Summons.Text = "Ignore Summons";
            // 
            // checkBox_Ign_TreasureChests
            // 
            this.checkBox_Ign_TreasureChests.Checked = true;
            this.checkBox_Ign_TreasureChests.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Ign_TreasureChests.Location = new System.Drawing.Point(5, 315);
            this.checkBox_Ign_TreasureChests.Name = "checkBox_Ign_TreasureChests";
            this.checkBox_Ign_TreasureChests.Size = new System.Drawing.Size(235, 24);
            this.checkBox_Ign_TreasureChests.TabIndex = 7;
            this.checkBox_Ign_TreasureChests.Text = "Ignore Chests";
            // 
            // checkBox_Ign_Raidbosses
            // 
            this.checkBox_Ign_Raidbosses.Checked = true;
            this.checkBox_Ign_Raidbosses.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Ign_Raidbosses.Location = new System.Drawing.Point(5, 293);
            this.checkBox_Ign_Raidbosses.Name = "checkBox_Ign_Raidbosses";
            this.checkBox_Ign_Raidbosses.Size = new System.Drawing.Size(121, 24);
            this.checkBox_Ign_Raidbosses.TabIndex = 6;
            this.checkBox_Ign_Raidbosses.Text = "Ignore Raidbosses";
            // 
            // label_donot_npcID
            // 
            this.label_donot_npcID.AutoSize = true;
            this.label_donot_npcID.Location = new System.Drawing.Point(24, 33);
            this.label_donot_npcID.Name = "label_donot_npcID";
            this.label_donot_npcID.Size = new System.Drawing.Size(18, 13);
            this.label_donot_npcID.TabIndex = 5;
            this.label_donot_npcID.Text = "ID";
            // 
            // label_donot_npcs
            // 
            this.label_donot_npcs.AutoSize = true;
            this.label_donot_npcs.Location = new System.Drawing.Point(103, 11);
            this.label_donot_npcs.Name = "label_donot_npcs";
            this.label_donot_npcs.Size = new System.Drawing.Size(34, 13);
            this.label_donot_npcs.TabIndex = 4;
            this.label_donot_npcs.Text = "NPCs";
            // 
            // textBox_donot_npcs
            // 
            this.textBox_donot_npcs.Location = new System.Drawing.Point(48, 30);
            this.textBox_donot_npcs.Name = "textBox_donot_npcs";
            this.textBox_donot_npcs.Size = new System.Drawing.Size(147, 20);
            this.textBox_donot_npcs.TabIndex = 0;
            // 
            // button_donot_npcs
            // 
            this.button_donot_npcs.Location = new System.Drawing.Point(83, 56);
            this.button_donot_npcs.Name = "button_donot_npcs";
            this.button_donot_npcs.Size = new System.Drawing.Size(75, 23);
            this.button_donot_npcs.TabIndex = 1;
            this.button_donot_npcs.Text = "Add";
            this.button_donot_npcs.Click += new System.EventHandler(this.button_donot_npcs_Click);
            // 
            // listView_donot_npcs
            // 
            this.listView_donot_npcs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_donot_npc_id,
            this.columnHeader_donot_npc_name});
            this.listView_donot_npcs.ContextMenuStrip = this.contextMenuStrip_donot_npcs;
            this.listView_donot_npcs.FullRowSelect = true;
            this.listView_donot_npcs.GridLines = true;
            this.listView_donot_npcs.Location = new System.Drawing.Point(5, 85);
            this.listView_donot_npcs.MultiSelect = false;
            this.listView_donot_npcs.Name = "listView_donot_npcs";
            this.listView_donot_npcs.Size = new System.Drawing.Size(240, 180);
            this.listView_donot_npcs.TabIndex = 2;
            this.listView_donot_npcs.UseCompatibleStateImageBehavior = false;
            this.listView_donot_npcs.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader_donot_npc_id
            // 
            this.columnHeader_donot_npc_id.Text = "ID";
            // 
            // columnHeader_donot_npc_name
            // 
            this.columnHeader_donot_npc_name.Text = "NPC";
            this.columnHeader_donot_npc_name.Width = 148;
            // 
            // contextMenuStrip_donot_npcs
            // 
            this.contextMenuStrip_donot_npcs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem1,
            this.removeAllToolStripMenuItem1});
            this.contextMenuStrip_donot_npcs.Name = "contextMenuStrip_donot_npcs";
            this.contextMenuStrip_donot_npcs.Size = new System.Drawing.Size(135, 48);
            // 
            // removeToolStripMenuItem1
            // 
            this.removeToolStripMenuItem1.Name = "removeToolStripMenuItem1";
            this.removeToolStripMenuItem1.Size = new System.Drawing.Size(134, 22);
            this.removeToolStripMenuItem1.Text = "Remove";
            this.removeToolStripMenuItem1.Click += new System.EventHandler(this.removeToolStripMenuItem1_Click);
            // 
            // removeAllToolStripMenuItem1
            // 
            this.removeAllToolStripMenuItem1.Name = "removeAllToolStripMenuItem1";
            this.removeAllToolStripMenuItem1.Size = new System.Drawing.Size(134, 22);
            this.removeAllToolStripMenuItem1.Text = "Remove All";
            this.removeAllToolStripMenuItem1.Click += new System.EventHandler(this.removeAllToolStripMenuItem1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.checkBox_PickOnly);
            this.panel2.Controls.Add(this.checkBox_ignore_no_mesh);
            this.panel2.Controls.Add(this.checkBox_ignoreitems);
            this.panel2.Controls.Add(this.label_donot_itemID);
            this.panel2.Controls.Add(this.label_donot_items);
            this.panel2.Controls.Add(this.textBox_donot_items);
            this.panel2.Controls.Add(this.button_donot_items);
            this.panel2.Controls.Add(this.listView_donot_items);
            this.panel2.Location = new System.Drawing.Point(4, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(248, 352);
            this.panel2.TabIndex = 0;
            // 
            // checkBox_PickOnly
            // 
            this.checkBox_PickOnly.ForeColor = System.Drawing.Color.Red;
            this.checkBox_PickOnly.Location = new System.Drawing.Point(3, 271);
            this.checkBox_PickOnly.Name = "checkBox_PickOnly";
            this.checkBox_PickOnly.Size = new System.Drawing.Size(239, 24);
            this.checkBox_PickOnly.TabIndex = 6;
            this.checkBox_PickOnly.Text = "Pick Only Items in List";
            // 
            // checkBox_ignore_no_mesh
            // 
            this.checkBox_ignore_no_mesh.Checked = true;
            this.checkBox_ignore_no_mesh.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_ignore_no_mesh.Location = new System.Drawing.Point(3, 315);
            this.checkBox_ignore_no_mesh.Name = "checkBox_ignore_no_mesh";
            this.checkBox_ignore_no_mesh.Size = new System.Drawing.Size(239, 24);
            this.checkBox_ignore_no_mesh.TabIndex = 5;
            this.checkBox_ignore_no_mesh.Text = "Ignore Meshless Items";
            // 
            // checkBox_ignoreitems
            // 
            this.checkBox_ignoreitems.Checked = true;
            this.checkBox_ignoreitems.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_ignoreitems.Location = new System.Drawing.Point(3, 293);
            this.checkBox_ignoreitems.Name = "checkBox_ignoreitems";
            this.checkBox_ignoreitems.Size = new System.Drawing.Size(136, 24);
            this.checkBox_ignoreitems.TabIndex = 3;
            this.checkBox_ignoreitems.Text = "Ignore Unknown Items";
            // 
            // label_donot_itemID
            // 
            this.label_donot_itemID.AutoSize = true;
            this.label_donot_itemID.Location = new System.Drawing.Point(26, 33);
            this.label_donot_itemID.Name = "label_donot_itemID";
            this.label_donot_itemID.Size = new System.Drawing.Size(18, 13);
            this.label_donot_itemID.TabIndex = 4;
            this.label_donot_itemID.Text = "ID";
            // 
            // label_donot_items
            // 
            this.label_donot_items.AutoSize = true;
            this.label_donot_items.Location = new System.Drawing.Point(107, 11);
            this.label_donot_items.Name = "label_donot_items";
            this.label_donot_items.Size = new System.Drawing.Size(32, 13);
            this.label_donot_items.TabIndex = 3;
            this.label_donot_items.Text = "Items";
            // 
            // textBox_donot_items
            // 
            this.textBox_donot_items.Location = new System.Drawing.Point(50, 30);
            this.textBox_donot_items.Name = "textBox_donot_items";
            this.textBox_donot_items.Size = new System.Drawing.Size(147, 20);
            this.textBox_donot_items.TabIndex = 0;
            // 
            // button_donot_items
            // 
            this.button_donot_items.Location = new System.Drawing.Point(85, 56);
            this.button_donot_items.Name = "button_donot_items";
            this.button_donot_items.Size = new System.Drawing.Size(75, 23);
            this.button_donot_items.TabIndex = 1;
            this.button_donot_items.Text = "Add";
            this.button_donot_items.Click += new System.EventHandler(this.button_donot_items_Click);
            // 
            // listView_donot_items
            // 
            this.listView_donot_items.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader_donot_item_id,
            this.columnHeader_donot_item_name});
            this.listView_donot_items.ContextMenuStrip = this.contextMenuStrip_donot_items;
            this.listView_donot_items.FullRowSelect = true;
            this.listView_donot_items.GridLines = true;
            this.listView_donot_items.Location = new System.Drawing.Point(4, 85);
            this.listView_donot_items.MultiSelect = false;
            this.listView_donot_items.Name = "listView_donot_items";
            this.listView_donot_items.Size = new System.Drawing.Size(239, 180);
            this.listView_donot_items.TabIndex = 2;
            this.listView_donot_items.UseCompatibleStateImageBehavior = false;
            this.listView_donot_items.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader_donot_item_id
            // 
            this.columnHeader_donot_item_id.Text = "ID";
            // 
            // columnHeader_donot_item_name
            // 
            this.columnHeader_donot_item_name.Text = "Item";
            this.columnHeader_donot_item_name.Width = 151;
            // 
            // contextMenuStrip_donot_items
            // 
            this.contextMenuStrip_donot_items.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.removeToolStripMenuItem,
            this.removeAllToolStripMenuItem2});
            this.contextMenuStrip_donot_items.Name = "contextMenuStrip_donot_items";
            this.contextMenuStrip_donot_items.Size = new System.Drawing.Size(135, 48);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // removeAllToolStripMenuItem2
            // 
            this.removeAllToolStripMenuItem2.Name = "removeAllToolStripMenuItem2";
            this.removeAllToolStripMenuItem2.Size = new System.Drawing.Size(134, 22);
            this.removeAllToolStripMenuItem2.Text = "Remove All";
            this.removeAllToolStripMenuItem2.Click += new System.EventHandler(this.removeAllToolStripMenuItem2_Click);
            // 
            // tabPage_soundalerts
            // 
            this.tabPage_soundalerts.Controls.Add(this.groupBox_LogOut);
            this.tabPage_soundalerts.Controls.Add(this.groupBox_SoundAlerts);
            this.tabPage_soundalerts.Location = new System.Drawing.Point(104, 4);
            this.tabPage_soundalerts.Name = "tabPage_soundalerts";
            this.tabPage_soundalerts.Size = new System.Drawing.Size(515, 377);
            this.tabPage_soundalerts.TabIndex = 9;
            this.tabPage_soundalerts.Text = "Sound Alerts/Log Out";
            this.tabPage_soundalerts.UseVisualStyleBackColor = true;
            // 
            // groupBox_LogOut
            // 
            this.groupBox_LogOut.Controls.Add(this.checkBox_1waywar_logout);
            this.groupBox_LogOut.Controls.Add(this.textBox_player_logout);
            this.groupBox_LogOut.Controls.Add(this.checkBox_2waywar_logout);
            this.groupBox_LogOut.Controls.Add(this.textBox_clan_logout);
            this.groupBox_LogOut.Controls.Add(this.checkBox_player_logout);
            this.groupBox_LogOut.Controls.Add(this.textBox_cp_logout);
            this.groupBox_LogOut.Controls.Add(this.checkBox_clan_logout);
            this.groupBox_LogOut.Controls.Add(this.textBox_hp_logout);
            this.groupBox_LogOut.Controls.Add(this.textBox_mp_logout);
            this.groupBox_LogOut.Controls.Add(this.label14);
            this.groupBox_LogOut.Controls.Add(this.label16);
            this.groupBox_LogOut.Controls.Add(this.checkBox_hp_logout);
            this.groupBox_LogOut.Controls.Add(this.checkBox_cp_logout);
            this.groupBox_LogOut.Controls.Add(this.checkBox_mp_logout);
            this.groupBox_LogOut.Controls.Add(this.label15);
            this.groupBox_LogOut.Controls.Add(this.checkBox_n1waywar_logout);
            this.groupBox_LogOut.Location = new System.Drawing.Point(3, 160);
            this.groupBox_LogOut.Name = "groupBox_LogOut";
            this.groupBox_LogOut.Size = new System.Drawing.Size(501, 100);
            this.groupBox_LogOut.TabIndex = 64;
            this.groupBox_LogOut.TabStop = false;
            this.groupBox_LogOut.Text = "Log Out Settings";
            // 
            // checkBox_1waywar_logout
            // 
            this.checkBox_1waywar_logout.Location = new System.Drawing.Point(6, 39);
            this.checkBox_1waywar_logout.Name = "checkBox_1waywar_logout";
            this.checkBox_1waywar_logout.Size = new System.Drawing.Size(82, 24);
            this.checkBox_1waywar_logout.TabIndex = 49;
            this.checkBox_1waywar_logout.Text = "1 Way War";
            // 
            // textBox_player_logout
            // 
            this.textBox_player_logout.Location = new System.Drawing.Point(259, 50);
            this.textBox_player_logout.Name = "textBox_player_logout";
            this.textBox_player_logout.Size = new System.Drawing.Size(216, 20);
            this.textBox_player_logout.TabIndex = 61;
            this.textBox_player_logout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkBox_2waywar_logout
            // 
            this.checkBox_2waywar_logout.Location = new System.Drawing.Point(6, 19);
            this.checkBox_2waywar_logout.Name = "checkBox_2waywar_logout";
            this.checkBox_2waywar_logout.Size = new System.Drawing.Size(82, 24);
            this.checkBox_2waywar_logout.TabIndex = 48;
            this.checkBox_2waywar_logout.Text = "2 Way War";
            // 
            // textBox_clan_logout
            // 
            this.textBox_clan_logout.Location = new System.Drawing.Point(259, 24);
            this.textBox_clan_logout.Name = "textBox_clan_logout";
            this.textBox_clan_logout.Size = new System.Drawing.Size(216, 20);
            this.textBox_clan_logout.TabIndex = 59;
            this.textBox_clan_logout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkBox_player_logout
            // 
            this.checkBox_player_logout.Location = new System.Drawing.Point(206, 46);
            this.checkBox_player_logout.Name = "checkBox_player_logout";
            this.checkBox_player_logout.Size = new System.Drawing.Size(56, 24);
            this.checkBox_player_logout.TabIndex = 60;
            this.checkBox_player_logout.Text = "Player";
            // 
            // textBox_cp_logout
            // 
            this.textBox_cp_logout.Location = new System.Drawing.Point(141, 62);
            this.textBox_cp_logout.Name = "textBox_cp_logout";
            this.textBox_cp_logout.Size = new System.Drawing.Size(37, 20);
            this.textBox_cp_logout.TabIndex = 56;
            this.textBox_cp_logout.Text = "50";
            this.textBox_cp_logout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkBox_clan_logout
            // 
            this.checkBox_clan_logout.Location = new System.Drawing.Point(206, 20);
            this.checkBox_clan_logout.Name = "checkBox_clan_logout";
            this.checkBox_clan_logout.Size = new System.Drawing.Size(56, 24);
            this.checkBox_clan_logout.TabIndex = 57;
            this.checkBox_clan_logout.Text = "Clan";
            // 
            // textBox_hp_logout
            // 
            this.textBox_hp_logout.Location = new System.Drawing.Point(141, 19);
            this.textBox_hp_logout.Name = "textBox_hp_logout";
            this.textBox_hp_logout.Size = new System.Drawing.Size(37, 20);
            this.textBox_hp_logout.TabIndex = 52;
            this.textBox_hp_logout.Text = "50";
            this.textBox_hp_logout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_mp_logout
            // 
            this.textBox_mp_logout.Location = new System.Drawing.Point(141, 41);
            this.textBox_mp_logout.Name = "textBox_mp_logout";
            this.textBox_mp_logout.Size = new System.Drawing.Size(37, 20);
            this.textBox_mp_logout.TabIndex = 54;
            this.textBox_mp_logout.Text = "50";
            this.textBox_mp_logout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label14
            // 
            this.label14.Location = new System.Drawing.Point(177, 65);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(22, 17);
            this.label14.TabIndex = 63;
            this.label14.Text = "%";
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(177, 24);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(23, 15);
            this.label16.TabIndex = 58;
            this.label16.Text = "%";
            // 
            // checkBox_hp_logout
            // 
            this.checkBox_hp_logout.Location = new System.Drawing.Point(94, 19);
            this.checkBox_hp_logout.Name = "checkBox_hp_logout";
            this.checkBox_hp_logout.Size = new System.Drawing.Size(59, 24);
            this.checkBox_hp_logout.TabIndex = 51;
            this.checkBox_hp_logout.Text = "HP <";
            // 
            // checkBox_cp_logout
            // 
            this.checkBox_cp_logout.Location = new System.Drawing.Point(94, 60);
            this.checkBox_cp_logout.Name = "checkBox_cp_logout";
            this.checkBox_cp_logout.Size = new System.Drawing.Size(59, 24);
            this.checkBox_cp_logout.TabIndex = 55;
            this.checkBox_cp_logout.Text = "CP <";
            // 
            // checkBox_mp_logout
            // 
            this.checkBox_mp_logout.Location = new System.Drawing.Point(94, 39);
            this.checkBox_mp_logout.Name = "checkBox_mp_logout";
            this.checkBox_mp_logout.Size = new System.Drawing.Size(59, 24);
            this.checkBox_mp_logout.TabIndex = 53;
            this.checkBox_mp_logout.Text = "MP <";
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(177, 44);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(22, 17);
            this.label15.TabIndex = 62;
            this.label15.Text = "%";
            // 
            // checkBox_n1waywar_logout
            // 
            this.checkBox_n1waywar_logout.Location = new System.Drawing.Point(6, 60);
            this.checkBox_n1waywar_logout.Name = "checkBox_n1waywar_logout";
            this.checkBox_n1waywar_logout.Size = new System.Drawing.Size(92, 24);
            this.checkBox_n1waywar_logout.TabIndex = 50;
            this.checkBox_n1waywar_logout.Text = "-1 Way War";
            // 
            // groupBox_SoundAlerts
            // 
            this.groupBox_SoundAlerts.Controls.Add(this.checkBox_2waywar);
            this.groupBox_SoundAlerts.Controls.Add(this.checkBox_player_ignore);
            this.groupBox_SoundAlerts.Controls.Add(this.checkBox_friendchat);
            this.groupBox_SoundAlerts.Controls.Add(this.checkBox_clan_ignore);
            this.groupBox_SoundAlerts.Controls.Add(this.checkBox_1waywar);
            this.groupBox_SoundAlerts.Controls.Add(this.textBox_player);
            this.groupBox_SoundAlerts.Controls.Add(this.checkBox_privatemessage);
            this.groupBox_SoundAlerts.Controls.Add(this.textBox_clan);
            this.groupBox_SoundAlerts.Controls.Add(this.checkBox_player);
            this.groupBox_SoundAlerts.Controls.Add(this.checkBox_n1waywar);
            this.groupBox_SoundAlerts.Controls.Add(this.checkBox_clan);
            this.groupBox_SoundAlerts.Controls.Add(this.textBox_cp);
            this.groupBox_SoundAlerts.Controls.Add(this.checkBox_whitechat);
            this.groupBox_SoundAlerts.Controls.Add(this.textBox_mp);
            this.groupBox_SoundAlerts.Controls.Add(this.textBox_hp);
            this.groupBox_SoundAlerts.Controls.Add(this.checkBox_hp);
            this.groupBox_SoundAlerts.Controls.Add(this.label12);
            this.groupBox_SoundAlerts.Controls.Add(this.checkBox_mp);
            this.groupBox_SoundAlerts.Controls.Add(this.label10);
            this.groupBox_SoundAlerts.Controls.Add(this.label11);
            this.groupBox_SoundAlerts.Controls.Add(this.checkBox_cp);
            this.groupBox_SoundAlerts.Location = new System.Drawing.Point(3, 3);
            this.groupBox_SoundAlerts.Name = "groupBox_SoundAlerts";
            this.groupBox_SoundAlerts.Size = new System.Drawing.Size(501, 151);
            this.groupBox_SoundAlerts.TabIndex = 37;
            this.groupBox_SoundAlerts.TabStop = false;
            this.groupBox_SoundAlerts.Text = "Sound Alerts";
            // 
            // checkBox_2waywar
            // 
            this.checkBox_2waywar.Location = new System.Drawing.Point(6, 19);
            this.checkBox_2waywar.Name = "checkBox_2waywar";
            this.checkBox_2waywar.Size = new System.Drawing.Size(82, 24);
            this.checkBox_2waywar.TabIndex = 16;
            this.checkBox_2waywar.Text = "2 Way War";
            // 
            // checkBox_player_ignore
            // 
            this.checkBox_player_ignore.AutoSize = true;
            this.checkBox_player_ignore.Location = new System.Drawing.Point(287, 122);
            this.checkBox_player_ignore.Name = "checkBox_player_ignore";
            this.checkBox_player_ignore.Size = new System.Drawing.Size(129, 17);
            this.checkBox_player_ignore.TabIndex = 33;
            this.checkBox_player_ignore.Text = "Ignore Party Members";
            this.checkBox_player_ignore.UseVisualStyleBackColor = true;
            // 
            // checkBox_friendchat
            // 
            this.checkBox_friendchat.AutoSize = true;
            this.checkBox_friendchat.Location = new System.Drawing.Point(94, 67);
            this.checkBox_friendchat.Name = "checkBox_friendchat";
            this.checkBox_friendchat.Size = new System.Drawing.Size(80, 17);
            this.checkBox_friendchat.TabIndex = 36;
            this.checkBox_friendchat.Text = "Friend Chat";
            this.checkBox_friendchat.UseVisualStyleBackColor = true;
            // 
            // checkBox_clan_ignore
            // 
            this.checkBox_clan_ignore.AutoSize = true;
            this.checkBox_clan_ignore.Location = new System.Drawing.Point(287, 96);
            this.checkBox_clan_ignore.Name = "checkBox_clan_ignore";
            this.checkBox_clan_ignore.Size = new System.Drawing.Size(129, 17);
            this.checkBox_clan_ignore.TabIndex = 32;
            this.checkBox_clan_ignore.Text = "Ignore Party Members";
            this.checkBox_clan_ignore.UseVisualStyleBackColor = true;
            // 
            // checkBox_1waywar
            // 
            this.checkBox_1waywar.Location = new System.Drawing.Point(6, 39);
            this.checkBox_1waywar.Name = "checkBox_1waywar";
            this.checkBox_1waywar.Size = new System.Drawing.Size(82, 24);
            this.checkBox_1waywar.TabIndex = 17;
            this.checkBox_1waywar.Text = "1 Way War";
            // 
            // textBox_player
            // 
            this.textBox_player.Location = new System.Drawing.Point(68, 120);
            this.textBox_player.Name = "textBox_player";
            this.textBox_player.Size = new System.Drawing.Size(216, 20);
            this.textBox_player.TabIndex = 29;
            this.textBox_player.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkBox_privatemessage
            // 
            this.checkBox_privatemessage.AutoSize = true;
            this.checkBox_privatemessage.Location = new System.Drawing.Point(94, 42);
            this.checkBox_privatemessage.Name = "checkBox_privatemessage";
            this.checkBox_privatemessage.Size = new System.Drawing.Size(105, 17);
            this.checkBox_privatemessage.TabIndex = 35;
            this.checkBox_privatemessage.Text = "Private Message";
            this.checkBox_privatemessage.UseVisualStyleBackColor = true;
            // 
            // textBox_clan
            // 
            this.textBox_clan.Location = new System.Drawing.Point(69, 94);
            this.textBox_clan.Name = "textBox_clan";
            this.textBox_clan.Size = new System.Drawing.Size(216, 20);
            this.textBox_clan.TabIndex = 27;
            this.textBox_clan.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkBox_player
            // 
            this.checkBox_player.Location = new System.Drawing.Point(6, 118);
            this.checkBox_player.Name = "checkBox_player";
            this.checkBox_player.Size = new System.Drawing.Size(56, 24);
            this.checkBox_player.TabIndex = 28;
            this.checkBox_player.Text = "Player";
            // 
            // checkBox_n1waywar
            // 
            this.checkBox_n1waywar.Location = new System.Drawing.Point(6, 62);
            this.checkBox_n1waywar.Name = "checkBox_n1waywar";
            this.checkBox_n1waywar.Size = new System.Drawing.Size(92, 24);
            this.checkBox_n1waywar.TabIndex = 18;
            this.checkBox_n1waywar.Text = "-1 Way War";
            // 
            // checkBox_clan
            // 
            this.checkBox_clan.Location = new System.Drawing.Point(6, 92);
            this.checkBox_clan.Name = "checkBox_clan";
            this.checkBox_clan.Size = new System.Drawing.Size(56, 24);
            this.checkBox_clan.TabIndex = 25;
            this.checkBox_clan.Text = "Clan";
            // 
            // textBox_cp
            // 
            this.textBox_cp.Location = new System.Drawing.Point(248, 62);
            this.textBox_cp.Name = "textBox_cp";
            this.textBox_cp.Size = new System.Drawing.Size(37, 20);
            this.textBox_cp.TabIndex = 24;
            this.textBox_cp.Text = "50";
            this.textBox_cp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkBox_whitechat
            // 
            this.checkBox_whitechat.AutoSize = true;
            this.checkBox_whitechat.Location = new System.Drawing.Point(94, 23);
            this.checkBox_whitechat.Name = "checkBox_whitechat";
            this.checkBox_whitechat.Size = new System.Drawing.Size(79, 17);
            this.checkBox_whitechat.TabIndex = 34;
            this.checkBox_whitechat.Text = "White Chat";
            this.checkBox_whitechat.UseVisualStyleBackColor = true;
            // 
            // textBox_mp
            // 
            this.textBox_mp.Location = new System.Drawing.Point(248, 39);
            this.textBox_mp.Name = "textBox_mp";
            this.textBox_mp.Size = new System.Drawing.Size(37, 20);
            this.textBox_mp.TabIndex = 22;
            this.textBox_mp.Text = "50";
            this.textBox_mp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_hp
            // 
            this.textBox_hp.Location = new System.Drawing.Point(248, 20);
            this.textBox_hp.Name = "textBox_hp";
            this.textBox_hp.Size = new System.Drawing.Size(37, 20);
            this.textBox_hp.TabIndex = 20;
            this.textBox_hp.Text = "50";
            this.textBox_hp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkBox_hp
            // 
            this.checkBox_hp.Location = new System.Drawing.Point(201, 19);
            this.checkBox_hp.Name = "checkBox_hp";
            this.checkBox_hp.Size = new System.Drawing.Size(59, 24);
            this.checkBox_hp.TabIndex = 19;
            this.checkBox_hp.Text = "HP <";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(284, 21);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(40, 17);
            this.label12.TabIndex = 26;
            this.label12.Text = "%";
            // 
            // checkBox_mp
            // 
            this.checkBox_mp.Location = new System.Drawing.Point(201, 39);
            this.checkBox_mp.Name = "checkBox_mp";
            this.checkBox_mp.Size = new System.Drawing.Size(59, 24);
            this.checkBox_mp.TabIndex = 21;
            this.checkBox_mp.Text = "MP <";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(284, 67);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 15);
            this.label10.TabIndex = 31;
            this.label10.Text = "%";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(284, 44);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(40, 15);
            this.label11.TabIndex = 30;
            this.label11.Text = "%";
            // 
            // checkBox_cp
            // 
            this.checkBox_cp.Location = new System.Drawing.Point(201, 60);
            this.checkBox_cp.Name = "checkBox_cp";
            this.checkBox_cp.Size = new System.Drawing.Size(59, 24);
            this.checkBox_cp.TabIndex = 23;
            this.checkBox_cp.Text = "CP <";
            // 
            // tabPage_content_filter
            // 
            this.tabPage_content_filter.Controls.Add(this.splitContainer1);
            this.tabPage_content_filter.Location = new System.Drawing.Point(104, 4);
            this.tabPage_content_filter.Name = "tabPage_content_filter";
            this.tabPage_content_filter.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_content_filter.Size = new System.Drawing.Size(515, 377);
            this.tabPage_content_filter.TabIndex = 13;
            this.tabPage_content_filter.Text = "Content Filter";
            this.tabPage_content_filter.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(9, 6);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.splitContainer1.Panel1.Controls.Add(this.cf_ExBrExtraUserInfo);
            this.splitContainer1.Panel1.Controls.Add(this.label18);
            this.splitContainer1.Panel1.Controls.Add(this.cf_targetselected);
            this.splitContainer1.Panel1.Controls.Add(this.cf_targetunselected);
            this.splitContainer1.Panel1.Controls.Add(this.cf_filtermagicskill);
            this.splitContainer1.Panel1.Margin = new System.Windows.Forms.Padding(5);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(5);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.splitContainer1.Panel2.Controls.Add(this.cf_dwarfmode);
            this.splitContainer1.Panel2.Controls.Add(this.label19);
            this.splitContainer1.Panel2.Controls.Add(this.cf_striptitle);
            this.splitContainer1.Panel2.Controls.Add(this.cf_one_gender);
            this.splitContainer1.Panel2.Controls.Add(this.cf_stripenchant);
            this.splitContainer1.Panel2.Controls.Add(this.cf_norecs);
            this.splitContainer1.Panel2.Controls.Add(this.cf_stripaugment);
            this.splitContainer1.Panel2.Controls.Add(this.cf_simple_appearance);
            this.splitContainer1.Panel2.Controls.Add(this.cf_zerononvisible);
            this.splitContainer1.Panel2.Margin = new System.Windows.Forms.Padding(5);
            this.splitContainer1.Panel2.Padding = new System.Windows.Forms.Padding(5);
            this.splitContainer1.Size = new System.Drawing.Size(484, 333);
            this.splitContainer1.SplitterDistance = 181;
            this.splitContainer1.TabIndex = 10;
            // 
            // cf_ExBrExtraUserInfo
            // 
            this.cf_ExBrExtraUserInfo.AutoSize = true;
            this.cf_ExBrExtraUserInfo.Location = new System.Drawing.Point(9, 122);
            this.cf_ExBrExtraUserInfo.Name = "cf_ExBrExtraUserInfo";
            this.cf_ExBrExtraUserInfo.Size = new System.Drawing.Size(125, 17);
            this.cf_ExBrExtraUserInfo.TabIndex = 4;
            this.cf_ExBrExtraUserInfo.Text = "Filter Event User Info";
            this.cf_ExBrExtraUserInfo.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(58, 21);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(66, 13);
            this.label18.TabIndex = 3;
            this.label18.Text = "Packet Filter";
            // 
            // cf_targetselected
            // 
            this.cf_targetselected.AutoSize = true;
            this.cf_targetselected.Cursor = System.Windows.Forms.Cursors.Default;
            this.cf_targetselected.Location = new System.Drawing.Point(8, 75);
            this.cf_targetselected.Name = "cf_targetselected";
            this.cf_targetselected.Size = new System.Drawing.Size(124, 17);
            this.cf_targetselected.TabIndex = 0;
            this.cf_targetselected.Text = "Filter TargetSelected";
            this.cf_targetselected.UseVisualStyleBackColor = true;
            // 
            // cf_targetunselected
            // 
            this.cf_targetunselected.AutoSize = true;
            this.cf_targetunselected.Location = new System.Drawing.Point(8, 98);
            this.cf_targetunselected.Name = "cf_targetunselected";
            this.cf_targetunselected.Size = new System.Drawing.Size(136, 17);
            this.cf_targetunselected.TabIndex = 1;
            this.cf_targetunselected.Text = "Filter TargetUnselected";
            this.cf_targetunselected.UseVisualStyleBackColor = true;
            // 
            // cf_filtermagicskill
            // 
            this.cf_filtermagicskill.AutoSize = true;
            this.cf_filtermagicskill.Location = new System.Drawing.Point(8, 52);
            this.cf_filtermagicskill.Name = "cf_filtermagicskill";
            this.cf_filtermagicskill.Size = new System.Drawing.Size(150, 17);
            this.cf_filtermagicskill.TabIndex = 2;
            this.cf_filtermagicskill.Text = "Filter Consumables Effects";
            this.cf_filtermagicskill.UseVisualStyleBackColor = true;
            // 
            // cf_dwarfmode
            // 
            this.cf_dwarfmode.AutoSize = true;
            this.cf_dwarfmode.Enabled = false;
            this.cf_dwarfmode.Location = new System.Drawing.Point(13, 214);
            this.cf_dwarfmode.Name = "cf_dwarfmode";
            this.cf_dwarfmode.Size = new System.Drawing.Size(122, 17);
            this.cf_dwarfmode.TabIndex = 11;
            this.cf_dwarfmode.Text = "Simplify Player Race";
            this.cf_dwarfmode.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(98, 21);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(91, 13);
            this.label19.TabIndex = 10;
            this.label19.Text = "Packet Transform";
            // 
            // cf_striptitle
            // 
            this.cf_striptitle.AutoSize = true;
            this.cf_striptitle.Enabled = false;
            this.cf_striptitle.Location = new System.Drawing.Point(13, 52);
            this.cf_striptitle.Name = "cf_striptitle";
            this.cf_striptitle.Size = new System.Drawing.Size(107, 17);
            this.cf_striptitle.TabIndex = 3;
            this.cf_striptitle.Text = "Strip Player Titles";
            this.cf_striptitle.UseVisualStyleBackColor = true;
            // 
            // cf_one_gender
            // 
            this.cf_one_gender.AutoSize = true;
            this.cf_one_gender.Enabled = false;
            this.cf_one_gender.Location = new System.Drawing.Point(13, 190);
            this.cf_one_gender.Name = "cf_one_gender";
            this.cf_one_gender.Size = new System.Drawing.Size(131, 17);
            this.cf_one_gender.TabIndex = 8;
            this.cf_one_gender.Text = "Simplify Player Gender";
            this.cf_one_gender.UseVisualStyleBackColor = true;
            // 
            // cf_stripenchant
            // 
            this.cf_stripenchant.AutoSize = true;
            this.cf_stripenchant.Enabled = false;
            this.cf_stripenchant.Location = new System.Drawing.Point(13, 75);
            this.cf_stripenchant.Name = "cf_stripenchant";
            this.cf_stripenchant.Size = new System.Drawing.Size(122, 17);
            this.cf_stripenchant.TabIndex = 4;
            this.cf_stripenchant.Text = "Strip Player Enchant";
            this.cf_stripenchant.UseVisualStyleBackColor = true;
            // 
            // cf_norecs
            // 
            this.cf_norecs.AutoSize = true;
            this.cf_norecs.Enabled = false;
            this.cf_norecs.Location = new System.Drawing.Point(13, 144);
            this.cf_norecs.Name = "cf_norecs";
            this.cf_norecs.Size = new System.Drawing.Size(134, 17);
            this.cf_norecs.TabIndex = 9;
            this.cf_norecs.Text = "Strip Player Reputation";
            this.cf_norecs.UseVisualStyleBackColor = true;
            // 
            // cf_stripaugment
            // 
            this.cf_stripaugment.AutoSize = true;
            this.cf_stripaugment.Enabled = false;
            this.cf_stripaugment.Location = new System.Drawing.Point(13, 98);
            this.cf_stripaugment.Name = "cf_stripaugment";
            this.cf_stripaugment.Size = new System.Drawing.Size(124, 17);
            this.cf_stripaugment.TabIndex = 5;
            this.cf_stripaugment.Text = "Strip Player Augment";
            this.cf_stripaugment.UseVisualStyleBackColor = true;
            // 
            // cf_simple_appearance
            // 
            this.cf_simple_appearance.AutoSize = true;
            this.cf_simple_appearance.Enabled = false;
            this.cf_simple_appearance.Location = new System.Drawing.Point(13, 167);
            this.cf_simple_appearance.Name = "cf_simple_appearance";
            this.cf_simple_appearance.Size = new System.Drawing.Size(137, 17);
            this.cf_simple_appearance.TabIndex = 7;
            this.cf_simple_appearance.Text = "Simplify Player Features";
            this.cf_simple_appearance.UseVisualStyleBackColor = true;
            // 
            // cf_zerononvisible
            // 
            this.cf_zerononvisible.AutoSize = true;
            this.cf_zerononvisible.Enabled = false;
            this.cf_zerononvisible.Location = new System.Drawing.Point(13, 121);
            this.cf_zerononvisible.Name = "cf_zerononvisible";
            this.cf_zerononvisible.Size = new System.Drawing.Size(115, 17);
            this.cf_zerononvisible.TabIndex = 6;
            this.cf_zerononvisible.Text = "Strip Unseen Items";
            this.cf_zerononvisible.UseVisualStyleBackColor = true;
            // 
            // tabPage_player_sorting
            // 
            this.tabPage_player_sorting.Controls.Add(this.textBox9);
            this.tabPage_player_sorting.Controls.Add(this.label26);
            this.tabPage_player_sorting.Controls.Add(this.textBox8);
            this.tabPage_player_sorting.Controls.Add(this.label25);
            this.tabPage_player_sorting.Controls.Add(this.textBox7);
            this.tabPage_player_sorting.Controls.Add(this.label24);
            this.tabPage_player_sorting.Controls.Add(this.label23);
            this.tabPage_player_sorting.Controls.Add(this.ps_label1);
            this.tabPage_player_sorting.Controls.Add(this.lv_player_sort);
            this.tabPage_player_sorting.Location = new System.Drawing.Point(104, 4);
            this.tabPage_player_sorting.Name = "tabPage_player_sorting";
            this.tabPage_player_sorting.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_player_sorting.Size = new System.Drawing.Size(515, 377);
            this.tabPage_player_sorting.TabIndex = 14;
            this.tabPage_player_sorting.Text = "Player Sorting";
            this.tabPage_player_sorting.UseVisualStyleBackColor = true;
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(414, 87);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(57, 20);
            this.textBox9.TabIndex = 9;
            this.textBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(299, 90);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(56, 13);
            this.label26.TabIndex = 8;
            this.label26.Text = "Max Z Diff";
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(414, 59);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(57, 20);
            this.textBox8.TabIndex = 7;
            this.textBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(299, 62);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(48, 13);
            this.label25.TabIndex = 6;
            this.label25.Text = "Max Dist";
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(414, 29);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(57, 20);
            this.textBox7.TabIndex = 5;
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(299, 31);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(109, 13);
            this.label24.TabIndex = 4;
            this.label24.Text = "Equidistant Threshold";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(293, 36);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(0, 13);
            this.label23.TabIndex = 3;
            // 
            // ps_label1
            // 
            this.ps_label1.AutoSize = true;
            this.ps_label1.Location = new System.Drawing.Point(305, 45);
            this.ps_label1.Name = "ps_label1";
            this.ps_label1.Size = new System.Drawing.Size(0, 13);
            this.ps_label1.TabIndex = 2;
            // 
            // lv_player_sort
            // 
            this.lv_player_sort.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.player_sort_col_ID,
            this.player_sort_col_cname,
            this.player_sort_col_prio});
            this.lv_player_sort.Location = new System.Drawing.Point(10, 6);
            this.lv_player_sort.Name = "lv_player_sort";
            this.lv_player_sort.Size = new System.Drawing.Size(268, 333);
            this.lv_player_sort.TabIndex = 0;
            this.lv_player_sort.UseCompatibleStateImageBehavior = false;
            this.lv_player_sort.View = System.Windows.Forms.View.Details;
            // 
            // player_sort_col_ID
            // 
            this.player_sort_col_ID.Text = "ID";
            this.player_sort_col_ID.Width = 39;
            // 
            // player_sort_col_cname
            // 
            this.player_sort_col_cname.Text = "Class";
            this.player_sort_col_cname.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.player_sort_col_cname.Width = 164;
            // 
            // player_sort_col_prio
            // 
            this.player_sort_col_prio.Text = "Priority";
            this.player_sort_col_prio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button_save
            // 
            this.button_save.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_save.Location = new System.Drawing.Point(8, 428);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(128, 24);
            this.button_save.TabIndex = 3;
            this.button_save.Text = "Apply";
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_cancel.Location = new System.Drawing.Point(485, 428);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(128, 24);
            this.button_cancel.TabIndex = 4;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // button_saveoptions
            // 
            this.button_saveoptions.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_saveoptions.Location = new System.Drawing.Point(242, 8);
            this.button_saveoptions.Name = "button_saveoptions";
            this.button_saveoptions.Size = new System.Drawing.Size(144, 23);
            this.button_saveoptions.TabIndex = 1;
            this.button_saveoptions.Text = "Save Options";
            this.button_saveoptions.Click += new System.EventHandler(this.button_saveoptions_Click);
            // 
            // button_loadoptions
            // 
            this.button_loadoptions.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_loadoptions.Location = new System.Drawing.Point(8, 8);
            this.button_loadoptions.Name = "button_loadoptions";
            this.button_loadoptions.Size = new System.Drawing.Size(144, 23);
            this.button_loadoptions.TabIndex = 0;
            this.button_loadoptions.Text = "Load Options";
            this.button_loadoptions.Click += new System.EventHandler(this.button_loadoptions_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(232, 48);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 16);
            this.label1.TabIndex = 69;
            this.label1.Text = "Page";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(240, 64);
            this.textBox1.MaxLength = 2;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(48, 20);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "1";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(168, 64);
            this.textBox2.MaxLength = 2;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(48, 20);
            this.textBox2.TabIndex = 4;
            this.textBox2.Text = "1";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(456, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 16);
            this.label2.TabIndex = 64;
            this.label2.Text = "MP>";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(456, 64);
            this.textBox3.MaxLength = 4;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(32, 20);
            this.textBox3.TabIndex = 8;
            this.textBox3.Text = "100";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(368, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 16);
            this.label3.TabIndex = 71;
            this.label3.Text = "Need Target";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // checkBox1
            // 
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(456, 24);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(24, 24);
            this.checkBox1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Enabled = false;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button1.Location = new System.Drawing.Point(240, 96);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(128, 24);
            this.button1.TabIndex = 10;
            this.button1.Text = "Update";
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button2.Location = new System.Drawing.Point(88, 96);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(128, 24);
            this.button2.TabIndex = 9;
            this.button2.Text = "Add";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(192, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 16);
            this.label4.TabIndex = 62;
            this.label4.Text = "Names";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(24, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 16);
            this.label5.TabIndex = 61;
            this.label5.Text = "On";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(24, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 16);
            this.label6.TabIndex = 60;
            this.label6.Text = "Trait";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(376, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 16);
            this.label7.TabIndex = 58;
            this.label7.Text = "Delay(sec)";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(320, 48);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 16);
            this.label8.TabIndex = 57;
            this.label8.Text = "XX < %";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(160, 48);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 16);
            this.label9.TabIndex = 56;
            this.label9.Text = "ShortCut";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(64, 24);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(352, 20);
            this.textBox4.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.Items.AddRange(new object[] {
            "Always",
            "CP",
            "HP",
            "MP",
            "Dead"});
            this.comboBox1.Location = new System.Drawing.Point(16, 64);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(120, 21);
            this.comboBox1.TabIndex = 3;
            // 
            // checkBox2
            // 
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(27, 24);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(24, 24);
            this.checkBox2.TabIndex = 0;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(376, 64);
            this.textBox5.MaxLength = 8;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(72, 20);
            this.textBox5.TabIndex = 7;
            this.textBox5.Text = "1";
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(320, 64);
            this.textBox6.MaxLength = 3;
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(48, 20);
            this.textBox6.TabIndex = 6;
            this.textBox6.Text = "60";
            this.textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Names";
            this.columnHeader7.Width = 187;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Trait";
            this.columnHeader11.Width = 55;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "ShortCut";
            this.columnHeader12.Width = 53;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "XX<%";
            this.columnHeader13.Width = 41;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Delay(sec)";
            this.columnHeader14.Width = 62;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "MP>";
            this.columnHeader15.Width = 47;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "Need Target";
            this.columnHeader16.Width = 20;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "TraitID";
            this.columnHeader17.Width = 0;
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "ShortCutID";
            this.columnHeader18.Width = 0;
            // 
            // button_close
            // 
            this.button_close.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_close.Location = new System.Drawing.Point(242, 428);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(128, 24);
            this.button_close.TabIndex = 6;
            this.button_close.Text = "Close";
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // button_clearoptions
            // 
            this.button_clearoptions.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_clearoptions.Location = new System.Drawing.Point(469, 8);
            this.button_clearoptions.Name = "button_clearoptions";
            this.button_clearoptions.Size = new System.Drawing.Size(144, 23);
            this.button_clearoptions.TabIndex = 7;
            this.button_clearoptions.Text = "Clear Options";
            this.button_clearoptions.Click += new System.EventHandler(this.button_clearoptions_Click);
            // 
            // BotOptionsScreen
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(625, 464);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl_botpages);
            this.Controls.Add(this.button_loadoptions);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.button_clearoptions);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.button_saveoptions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(631, 488);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(631, 488);
            this.Name = "BotOptionsScreen";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bot Options";
            this.tabControl_botpages.ResumeLayout(false);
            this.tabPage_party.ResumeLayout(false);
            this.groupBox_RezSettings.ResumeLayout(false);
            this.groupBox_RezSettings.PerformLayout();
            this.groupBox_PartySettings.ResumeLayout(false);
            this.groupBox_PartySettings.PerformLayout();
            this.groupBox_BuffSettings1.ResumeLayout(false);
            this.groupBox_BuffSettings1.PerformLayout();
            this.groupBox_FollowSettings.ResumeLayout(false);
            this.groupBox_FollowSettings.PerformLayout();
            this.tabPage_autofighter.ResumeLayout(false);
            this.groupBox_PickupSettings.ResumeLayout(false);
            this.groupBox_PickupSettings.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox_Pet.ResumeLayout(false);
            this.groupBox_Pet.PerformLayout();
            this.groupBox_DeadSettings.ResumeLayout(false);
            this.groupBox_DeadSettings.PerformLayout();
            this.groupBox_StuckSettings.ResumeLayout(false);
            this.groupBox_AttackSettings.ResumeLayout(false);
            this.groupBox_AttackSettings.PerformLayout();
            this.groupBox_SpoilSettings.ResumeLayout(false);
            this.groupBox_SpoilSettings.PerformLayout();
            this.groupBox_TargetSettings.ResumeLayout(false);
            this.tabPage_autofighter_advanced.ResumeLayout(false);
            this.groupBox_WindowTitle.ResumeLayout(false);
            this.groupBox_WindowTitle.PerformLayout();
            this.groupBox_AdvancedS.ResumeLayout(false);
            this.groupBox_AdvancedS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_pickuptimeout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_anti_ks_delay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_autofollow_delay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_blacklist_tries)).EndInit();
            this.tabPage_RestOptions.ResumeLayout(false);
            this.groupBox_Rest_Party.ResumeLayout(false);
            this.groupBox_Rest_Party.PerformLayout();
            this.groupBox_Rest_Solo.ResumeLayout(false);
            this.groupBox_Rest_Solo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RestUntilMP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RestUntilHP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RestBelowMP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_RestBelowHP)).EndInit();
            this.tabPage_target.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage_buffsheals.ResumeLayout(false);
            this.tabPage_buffsheals.PerformLayout();
            this.contextMenuStrip_buff.ResumeLayout(false);
            this.tabPage_toggles.ResumeLayout(false);
            this.tabPage_toggles.PerformLayout();
            this.contextMenuStrip_toggle.ResumeLayout(false);
            this.tabPage_items.ResumeLayout(false);
            this.tabPage_items.PerformLayout();
            this.contextMenuStrip_item.ResumeLayout(false);
            this.tabPage_combat.ResumeLayout(false);
            this.tabPage_combat.PerformLayout();
            this.contextMenuStrip_combat.ResumeLayout(false);
            this.tabPage_polygon.ResumeLayout(false);
            this.tabPage_polygon.PerformLayout();
            this.contextMenuStrip_polygon.ResumeLayout(false);
            this.tabPage_donot.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.contextMenuStrip_donot_npcs.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.contextMenuStrip_donot_items.ResumeLayout(false);
            this.tabPage_soundalerts.ResumeLayout(false);
            this.groupBox_LogOut.ResumeLayout(false);
            this.groupBox_LogOut.PerformLayout();
            this.groupBox_SoundAlerts.ResumeLayout(false);
            this.groupBox_SoundAlerts.PerformLayout();
            this.tabPage_content_filter.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.tabPage_player_sorting.ResumeLayout(false);
            this.tabPage_player_sorting.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private void button_cancel_Click(object sender, System.EventArgs e)
        {
            this.Hide();
        }

        private void button_save_Click(object sender, System.EventArgs e)
        {
            //save out the options
            ////////////////////////PARTY OPTIONS
            if (checkBox_activefollow.Checked)
                Globals.gamedata.botoptions.ActiveFollow = 1;
            else
                Globals.gamedata.botoptions.ActiveFollow = 0;

            Globals.gamedata.botoptions.Set_ActiveFollow(textBox_activefollow_name.Text);

            if (radioButton_ActiveFollow_style1.Checked)
                Globals.gamedata.botoptions.ActiveFollowStyle = 1;//walker style
            else
                Globals.gamedata.botoptions.ActiveFollowStyle = 0;//l2.net style

            Globals.gamedata.botoptions.ActiveFollowDistance = Util.GetInt32(textBox_ActiveFollow_Dist.Text);

            if (checkBox_activefollow_attack.Checked)
                Globals.gamedata.botoptions.ActiveFollowAttack = 1;
            else
                Globals.gamedata.botoptions.ActiveFollowAttack = 0;

            if (checkBox_activefollow_attack_Instant.Checked)
                Globals.gamedata.botoptions.ActiveFollowAttackInstant = 1;
            else
                Globals.gamedata.botoptions.ActiveFollowAttackInstant = 0;

            if (checkBox_activefollow_target.Checked)
                Globals.gamedata.botoptions.ActiveFollowTarget = 1;
            else
                Globals.gamedata.botoptions.ActiveFollowTarget = 0;

            if (checkBox_autosweep.Checked)
                Globals.gamedata.botoptions.AutoSweep = 1;
            else
                Globals.gamedata.botoptions.AutoSweep = 0;

            if (checkBox_autospoil.Checked)
                Globals.gamedata.botoptions.AutoSpoil = 1;
            else
                Globals.gamedata.botoptions.AutoSpoil = 0;

            if (checkBox_UntilSuccess.Checked)
                Globals.gamedata.botoptions.AutoSpoilUntilSuccess = 1;
            else
                Globals.gamedata.botoptions.AutoSpoilUntilSuccess = 0;

            Globals.gamedata.botoptions.SpoilMPAbove = System.Convert.ToInt32(textBox_spoil_mp.Text);

            if (checkBox_spoilcrush.Checked)
                Globals.gamedata.botoptions.SpoilCrush = 1;
            else
                Globals.gamedata.botoptions.SpoilCrush = 0;

            if (checkBox_use_plunder.Checked)
                Globals.gamedata.botoptions.Plunder = 1;
            else
                Globals.gamedata.botoptions.Plunder = 0;

            if (checkBox_ignoreitems.Checked)
                Globals.gamedata.botoptions.IgnoreItems = 1;
            else
                Globals.gamedata.botoptions.IgnoreItems = 0;

            if (checkBox_PickOnly.Checked)
                Globals.gamedata.botoptions.PickOnlyItemsInList = 1;
            else
                Globals.gamedata.botoptions.PickOnlyItemsInList = 0;

            if (checkBox_AttackOnly.Checked)
                Globals.gamedata.botoptions.AttackOnlyMobsInList = 1;
            else
                Globals.gamedata.botoptions.AttackOnlyMobsInList = 0;

            if (checkBox_ignore_no_mesh.Checked)
                Globals.gamedata.botoptions.IgnoreMeshlessItems = 1;
            else
                Globals.gamedata.botoptions.IgnoreMeshlessItems = 0;

            Globals.gamedata.botoptions.HealRange = Util.GetInt32(textBox_buffrange.Text);

            if (checkBox_accept_party.Checked)
                Globals.gamedata.botoptions.AcceptParty = 1;
            else
                Globals.gamedata.botoptions.AcceptParty = 0;

            Globals.gamedata.botoptions.AcceptPartyNames = textBox_accept_party.Text;

            if (checkBox_auto_invite.Checked)
                Globals.gamedata.botoptions.SendParty = 1;
            else
                Globals.gamedata.botoptions.SendParty = 0;

            Globals.gamedata.botoptions.SendPartyNames = textBox_auto_invite.Text;

            if (checkBox_oop.Checked)
                Globals.gamedata.botoptions.OOP = 1;
            else
                Globals.gamedata.botoptions.OOP = 0;
            Globals.gamedata.botoptions.OOPNames = textBox_oop.Text;

            if (checkBox_drop_leader.Checked)
                Globals.gamedata.botoptions.LeavePartyOnLeader = 1;
            else
                Globals.gamedata.botoptions.LeavePartyOnLeader = 0;

            if (checkBox_accept_rez_clan.Checked)
                Globals.gamedata.botoptions.AcceptRezClan = 1;
            else
                Globals.gamedata.botoptions.AcceptRezClan = 0;

            if (checkBox_accept_rez_Party.Checked)
                Globals.gamedata.botoptions.AcceptRezParty = 1;
            else
                Globals.gamedata.botoptions.AcceptRezParty = 0;

            if (checkBox_accept_rez_alliance.Checked)
                Globals.gamedata.botoptions.AcceptRezAlly = 1;
            else
                Globals.gamedata.botoptions.AcceptRezAlly = 0;

            if (checkBox_accept_party_clan.Checked)
                Globals.gamedata.botoptions.AcceptPartyClan = 1;
            else
                Globals.gamedata.botoptions.AcceptPartyClan = 0;

            if (checkBox_accept_party_alliance.Checked)
                Globals.gamedata.botoptions.AcceptPartyAlly = 1;
            else
                Globals.gamedata.botoptions.AcceptPartyAlly = 0;

            

            //set up oops ID
            Globals.gamedata.botoptions.OOPIDs = new ArrayList();
            Globals.gamedata.botoptions.OOPNamesArray = Util.GetArray(Globals.gamedata.botoptions.OOPNames);
            Globals.gamedata.botoptions.OOPIDs = new System.Collections.ArrayList();

            if (Globals.gamedata.botoptions.OOPNamesArray.Count > 0)
            {
                Globals.PlayerLock.EnterReadLock();
                try
                {
                    foreach (Player_Info player in Globals.gamedata.nearby_chars.Values)
                    {
                        if (Globals.gamedata.botoptions.OOPNamesArray.Contains(player.Name.ToUpperInvariant()))
                        {
                            Globals.gamedata.botoptions.OOPIDs.Add(player.ID);
                        }
                    }
                }
                catch
                {
                    //oops
                }
                finally
                {
                    Globals.PlayerLock.ExitReadLock();
                }
            }
            /*********** Rest Options **************/
            /* Rest Below */
            if (checkBox_RestBelowHP.Checked)
            {
                Globals.gamedata.botoptions.RestBelowHP = 1;
                try
                {
                    Globals.gamedata.botoptions.RestBelowHealth = (Globals.gamedata.my_char.Max_HP * (float)numericUpDown_RestBelowHP.Value / 100);
                }
                catch
                {
                    Globals.l2net_home.Add_Error("Invalid rest below HP value, please enter a number between 0 and 100");
                }
                Globals.l2net_home.Add_Text("Rest Below " + Globals.gamedata.botoptions.RestBelowHealth.ToString() + " HP", Globals.Green, TextType.BOT);
            }
            else
                Globals.gamedata.botoptions.RestBelowHP = 0;

            if (checkBox_RestBelowMP.Checked)
            {
                Globals.gamedata.botoptions.RestBelowMP = 1;
                try
                {
                    Globals.gamedata.botoptions.RestBelowMana = (Globals.gamedata.my_char.Max_MP * (float)numericUpDown_RestBelowMP.Value / 100);
                }
                catch
                {
                    Globals.l2net_home.Add_Error("Invalid rest below MP value, please enter a number between 0 and 100");
                }
                Globals.l2net_home.Add_Text("Rest Below " + Globals.gamedata.botoptions.RestBelowMana.ToString() + " MP", Globals.Green, TextType.BOT);
            }
            else
                Globals.gamedata.botoptions.RestBelowMP = 0;

            /* Rest Until */
            if (checkBox_RestUntilHP.Checked)
            {
                /*
                Globals.gamedata.botoptions.RestUntilHP = 1;
                */
                try
                {
                    Globals.gamedata.botoptions.RestUntilHealth = (Globals.gamedata.my_char.Max_HP * (float)numericUpDown_RestUntilHP.Value / 100);
                }
                catch
                {
                    Globals.l2net_home.Add_Error("Invalid rest until HP value, please enter a number between 0 and 100");
                }
                Globals.l2net_home.Add_Text("Rest Until " + Globals.gamedata.botoptions.RestUntilHealth.ToString() + " HP", Globals.Green, TextType.BOT);
            }
            else
            {
                /*
                Globals.gamedata.botoptions.RestUntilHP = 0;
                 */
                Globals.gamedata.botoptions.RestUntilHealth = Globals.gamedata.my_char.Max_HP;
            }

            if (checkBox_RestUntilMP.Checked)
            {
                /*
                Globals.gamedata.botoptions.RestUntilMP = 1;
                */
                try
                {
                    Globals.gamedata.botoptions.RestUntilMana = (Globals.gamedata.my_char.Max_MP * (float)numericUpDown_RestUntilMP.Value / 100);
                }
                catch
                {
                    Globals.l2net_home.Add_Error("Invalid rest until MP value, please enter a number between 0 and 100");
                }
                Globals.l2net_home.Add_Text("Rest Until " + Globals.gamedata.botoptions.RestUntilMana.ToString() + " MP", Globals.Green, TextType.BOT);
            }
            else
            {
                /*
                Globals.gamedata.botoptions.RestUntilMP = 0;
                 */
                Globals.gamedata.botoptions.RestUntilMana = Globals.gamedata.my_char.Max_MP;
            }


            /* Follow Rest */
            if (checkBox_FollowRest.Checked)
                Globals.gamedata.botoptions.FollowRest = 1;
            else
                Globals.gamedata.botoptions.FollowRest = 0;
            Globals.gamedata.botoptions.Set_FollowRest(textBox_FollowRestName.Text); //ActiveFollow(textBox_activefollow_name.Text);

            /* Stuck Check */
            if (checkBox_StuckCheck.Checked)
                Globals.gamedata.botoptions.StuckCheck = 1;
            else
                Globals.gamedata.botoptions.StuckCheck = 0;

            /* Auto Blacklist */
            if (checkBox_AutoBlacklist.Checked)
                Globals.gamedata.botoptions.AutoBlacklist = 1;
            else
                Globals.gamedata.botoptions.AutoBlacklist = 0;


            /*************************/

            if (checkBox_accept_rez.Checked)
                Globals.gamedata.botoptions.AcceptRez = 1;
            else
                Globals.gamedata.botoptions.AcceptRez = 0;

            Globals.gamedata.botoptions.AcceptRezNames = textBox_accept_rez.Text;

            if (checkBox_active_target.Checked)
                Globals.gamedata.botoptions.Target = 1;
            else
                Globals.gamedata.botoptions.Target = 0;

            if (checkBox_cancel_target.Checked)
                Globals.gamedata.botoptions.Cancel_Target = 1;
            else
                Globals.gamedata.botoptions.Cancel_Target = 0;

            //move to location stuff
            if (checkBox_MoveToLoc.Checked)
                Globals.gamedata.botoptions.MoveToLoc = 1;
            else
                Globals.gamedata.botoptions.MoveToLoc = 0;

            if (checkBox_OutOfCombat.Checked)
                Globals.gamedata.botoptions.OutOfCombat = 1;
            else
                Globals.gamedata.botoptions.OutOfCombat = 0;

            Globals.gamedata.botoptions.Moveto_X = textBox_Moveto_X.Text;
            Globals.gamedata.botoptions.Moveto_Y = textBox_Moveto_Y.Text;
            Globals.gamedata.botoptions.Moveto_Z = textBox_Moveto_Z.Text;
            Globals.gamedata.botoptions.MoveToLeash = System.Convert.ToInt32(textBox_MoveToLeash.Text);


            if (checkBox_active_attack.Checked)
                Globals.gamedata.botoptions.Attack = 1;
            else
                Globals.gamedata.botoptions.Attack = 0;

            if (checkBox_pet_autoassist.Checked)
                Globals.gamedata.botoptions.PetAssist = 1;
            else
                Globals.gamedata.botoptions.PetAssist = 0;

            if (checkBox_Summon_autoassist.Checked)
                Globals.gamedata.botoptions.SummonAssist = 1;
            else
                Globals.gamedata.botoptions.SummonAssist = 0;

            if (checkBox_pet_soloattack.Checked)
                Globals.gamedata.botoptions.PetAttackSolo = 1;
            else
                Globals.gamedata.botoptions.PetAttackSolo = 0;

            if (checkBox_summon_instantattack.Checked)
                Globals.gamedata.botoptions.SummonInstantAttack = 1;
            else
                Globals.gamedata.botoptions.SummonInstantAttack = 0;

            if (checkBox_active_move_first.Checked)
                Globals.gamedata.botoptions.MoveFirst = 1;
            else
                Globals.gamedata.botoptions.MoveFirst = 0;

            Globals.gamedata.botoptions.MoveRange = Util.GetInt32(textBox_active_move_range.Text);

            if (checkBox_pickup.Checked)
                Globals.gamedata.botoptions.Pickup = 1;
            else
                Globals.gamedata.botoptions.Pickup = 0;

            if (checkBox_PickupAfterAttack.Checked)
                Globals.gamedata.botoptions.PickupAfterAttack = 1;
            else
                Globals.gamedata.botoptions.PickupAfterAttack = 0;

            if (checkBox_OnlyPickMine.Checked)
                Globals.gamedata.botoptions.OnlyPickMine = 1;
            else
                Globals.gamedata.botoptions.OnlyPickMine = 0;

            Globals.gamedata.botoptions.LootRange = Util.GetInt32(textBox_pickup_range.Text);

            if (checkBox_buff_control.Checked)
                Globals.gamedata.botoptions.ControlBuffing = 1;
            else
                Globals.gamedata.botoptions.ControlBuffing = 0;

            if (checkBox_buff_shift.Checked)
                Globals.gamedata.botoptions.ShiftBuffing = 1;
            else
                Globals.gamedata.botoptions.ShiftBuffing = 0;

            if (checkBox_portect_priority.Checked)
                Globals.gamedata.botoptions.ProtectPriority = 1;
            else
                Globals.gamedata.botoptions.ProtectPriority = 0;

            BotOptions.Target_ZRANGE = Util.GetInt32(textBox_zrange.Text);

            ////////////////////////BUFF OPTIONS
            Globals.BuffListLock.EnterWriteLock();
            try
            {
                BotOptions.BuffTargets.Clear();

                string inp, tmp;

                foreach (System.Windows.Forms.ListViewItem lv in listView_buffheal.CheckedItems)
                {
                    BuffTargetClass btc = new BuffTargetClass();

                    btc.Active = lv.Checked;
                    btc.Type = (BuffTriggers)Util.GetInt32(lv.SubItems[7].Text);
                    btc.SkillID = Util.GetUInt32(lv.SubItems[8].Text);
                    btc.Min_Per = Util.GetInt32(lv.SubItems[3].Text);
                    btc.TickDuration = Util.GetInt64(lv.SubItems[4].Text) * TimeSpan.TicksPerSecond;
                    btc.Min_MP = Util.GetInt32(lv.SubItems[5].Text);
                    btc.NeedTarget = Util.GetInt32(lv.SubItems[6].Text);

                    btc.TargetNames.Clear();

                    //set the names
                    inp = lv.SubItems[2].Text;
                    while (inp.Length > 0)
                    {
                        int pipe;
                        pipe = inp.IndexOf(';');
                        if (pipe == -1)
                        {
                            tmp = inp;
                            inp = "";
                            tmp = tmp.ToUpperInvariant();
                            btc.TargetNames.Add(tmp);
                        }
                        else
                        {
                            tmp = inp.Substring(0, pipe);
                            inp = inp.Remove(0, pipe + 1);
                            tmp = tmp.ToUpperInvariant();
                            btc.TargetNames.Add(tmp);
                        }
                    }

                    BotOptions.BuffTargets.Add(btc);
                }
            }//unlock
            finally
            {
                Globals.BuffListLock.ExitWriteLock();
            }

            ////////////////////////ITEM OPTIONS
            Globals.ItemListLock.EnterWriteLock();
            try
            {
                BotOptions.ItemTargets.Clear();

                foreach (System.Windows.Forms.ListViewItem lv in listView_item.CheckedItems)
                {
                    ItemTargetClass it;

                    it = new ItemTargetClass();
                    it.Active = lv.Checked;
                    it.Type = (BuffTriggers)Util.GetInt32(lv.SubItems[4].Text);
                    it.ItemID = Util.GetUInt32(lv.SubItems[5].Text);
                    it.Min_Per = Util.GetInt32(lv.SubItems[2].Text);
                    it.TickDuration = ((long)Util.GetInt32(lv.SubItems[3].Text)) * System.TimeSpan.TicksPerMillisecond;

                    BotOptions.ItemTargets.Add(it);
                }

            }
            finally
            {
                Globals.ItemListLock.ExitWriteLock();
            }
            ////////////////////////COMBAT OPTIONS
            Globals.CombatListLock.EnterWriteLock();
            try
            {
                BotOptions.CombatTargets.Clear();

                foreach (System.Windows.Forms.ListViewItem lv in listView_combat.CheckedItems)
                {
                    CombatTargetClass ct;

                    ct = new CombatTargetClass();
                    ct.Active = lv.Checked;
                    ct.Type = (BuffTriggers)Util.GetInt32(lv.SubItems[6].Text);
                    ct.Conditional = Util.GetInt32(lv.SubItems[7].Text);
                    ct.ShortCutID = Util.GetInt32(lv.SubItems[8].Text);
                    ct.Min_Per = Util.GetInt32(lv.SubItems[2].Text);
                    ct.Min_MP = Util.GetInt32(lv.SubItems[5].Text);
                    ct.TickDuration = ((long)Util.GetInt32(lv.SubItems[4].Text)) * System.TimeSpan.TicksPerMillisecond;

                    BotOptions.CombatTargets.Add(ct);
                }

            }
            finally
            {
                Globals.CombatListLock.ExitWriteLock();
            }
            ///////////////////////Do Not
            Globals.DoNotItemLock.EnterWriteLock();
            try
            {
                BotOptions.DoNotItems.Clear();

                foreach (System.Windows.Forms.ListViewItem lv in listView_donot_items.Items)
                {
                    if (lv != null)
                    {
                        BotOptions.DoNotItems.Add(Util.GetUInt32(lv.SubItems[0].Text)); //Item ID
                    }
                }

            }
            finally
            {
                Globals.DoNotItemLock.ExitWriteLock();
            }

            Globals.DoNotNPCLock.EnterWriteLock();
            try
            {
                BotOptions.DoNotNPCs.Clear();

                foreach (System.Windows.Forms.ListViewItem lv in listView_donot_npcs.Items)
                {
                    try
                    {
                        if (lv != null)
                        {
                            BotOptions.DoNotNPCs.Add(Util.GetUInt32(lv.SubItems[0].Text));
                        }
                    }
                    catch
                    {

                    }
                }

                if (checkBox_Ign_Raidbosses.Checked)
                {
                    uint tmpID;
                    foreach (uint rbID in RaidBossIDs)
                    {
                        tmpID = rbID; //Meh...
                        if (tmpID < Globals.NPC_OFF)
                        {
                            tmpID += Globals.NPC_OFF;
                        }
                        BotOptions.DoNotNPCs.Add(tmpID);
                    }
                }

                if (checkBox_Ign_Summons.Checked)
                {
                    uint tmpID;
                    foreach (uint smID in SummonIDs)
                    {
                        tmpID = smID; //Meh...
                        if (tmpID < Globals.NPC_OFF)
                        {
                            tmpID += Globals.NPC_OFF;
                        }
                        BotOptions.DoNotNPCs.Add(tmpID);
                    }
                }

                if (checkBox_Ign_TreasureChests.Checked)
                {
                    uint tmpID;
                    foreach (uint chestID in TreasureChestIDs)
                    {
                        tmpID = chestID;
                        if (tmpID < Globals.NPC_OFF)
                        {
                            tmpID += Globals.NPC_OFF;
                        }
                        BotOptions.DoNotNPCs.Add(tmpID);
                    }
                }
            }
            finally
            {
                Globals.DoNotNPCLock.ExitWriteLock();
            }

            ////////////////////////////Bounding Polygon
            Globals.gamedata.Paths.PointList.Clear();

            foreach (System.Windows.Forms.ListViewItem lv in listView_border.Items)
            {
                Point p = new Point();
                p.X = Util.GetInt32(lv.SubItems[0].Text);
                p.Y = Util.GetInt32(lv.SubItems[1].Text);

                Globals.gamedata.Paths.PointList.Add(p);
            }

            //Sound Alerts
            Globals.gamedata.alertoptions.beepon_2waywar = checkBox_2waywar.Checked;
            Globals.gamedata.alertoptions.beepon_1waywar = checkBox_1waywar.Checked;
            Globals.gamedata.alertoptions.beepon_n1waywar = checkBox_n1waywar.Checked;
            Globals.gamedata.alertoptions.beepon_hp = checkBox_hp.Checked;
            Globals.gamedata.alertoptions.beepon_mp = checkBox_mp.Checked;
            Globals.gamedata.alertoptions.beepon_cp = checkBox_cp.Checked;
            Globals.gamedata.alertoptions.beepon_clan = checkBox_clan.Checked;
            Globals.gamedata.alertoptions.beepon_player = checkBox_player.Checked;
            Globals.gamedata.alertoptions.hp_value = Util.GetInt32(textBox_hp.Text);
            Globals.gamedata.alertoptions.mp_value = Util.GetInt32(textBox_mp.Text);
            Globals.gamedata.alertoptions.cp_value = Util.GetInt32(textBox_cp.Text);
            Globals.gamedata.alertoptions.clan_value = textBox_clan.Text;
            Globals.gamedata.alertoptions.player_value = textBox_player.Text;
            Globals.gamedata.alertoptions.beepon_clan_ignoreparty = checkBox_clan_ignore.Checked;
            Globals.gamedata.alertoptions.beepon_player_ignoreparty = checkBox_player_ignore.Checked;
            Globals.gamedata.alertoptions.beepon_whitechat = checkBox_whitechat.Checked;
            Globals.gamedata.alertoptions.beepon_privatemessage = checkBox_privatemessage.Checked;
            Globals.gamedata.alertoptions.beepon_friendchat = checkBox_friendchat.Checked;

            //Logout
            Globals.gamedata.alertoptions.logouton_2waywar = checkBox_2waywar_logout.Checked;
            Globals.gamedata.alertoptions.logouton_1waywar = checkBox_1waywar_logout.Checked;
            Globals.gamedata.alertoptions.logouton_n1waywar = checkBox_n1waywar_logout.Checked;
            Globals.gamedata.alertoptions.logouton_hp = checkBox_hp_logout.Checked;
            Globals.gamedata.alertoptions.logouton_mp = checkBox_mp_logout.Checked;
            Globals.gamedata.alertoptions.logouton_cp = checkBox_cp_logout.Checked;
            Globals.gamedata.alertoptions.logouton_clan = checkBox_clan_logout.Checked;
            Globals.gamedata.alertoptions.logouton_player = checkBox_player_logout.Checked;
            Globals.gamedata.alertoptions.hp_value_logout = Util.GetInt32(textBox_hp_logout.Text);
            Globals.gamedata.alertoptions.mp_value_logout = Util.GetInt32(textBox_mp_logout.Text);
            Globals.gamedata.alertoptions.cp_value_logout = Util.GetInt32(textBox_cp_logout.Text);
            Globals.gamedata.alertoptions.clan_value_logout = textBox_clan_logout.Text;
            Globals.gamedata.alertoptions.player_value_logout = textBox_player_logout.Text;

            //targeting
            if (radioButton_type0.Checked)
                BotOptions.Target_TYPE = 0;
            if (radioButton_type1.Checked)
                BotOptions.Target_TYPE = 1;
            if (radioButton_type2.Checked)
                BotOptions.Target_TYPE = 2;
            if (radioButton_attackable0.Checked)
                BotOptions.Target_ATTACKABLE = 0;
            if (radioButton_attackable1.Checked)
                BotOptions.Target_ATTACKABLE = 1;
            if (radioButton_attackable2.Checked)
                BotOptions.Target_ATTACKABLE = 2;
            if (radioButton_alive0.Checked)
                BotOptions.Target_ALIVE = 0;
            if (radioButton_alive1.Checked)
                BotOptions.Target_ALIVE = 1;
            if (radioButton_alive2.Checked)
                BotOptions.Target_ALIVE = 2;
            if (radioButton_inbox0.Checked)
                BotOptions.Target_INBOX = 0;
            if (radioButton_inbox1.Checked)
                BotOptions.Target_INBOX = 1;
            if (radioButton_inbox2.Checked)
                BotOptions.Target_INBOX = 2;
            if (radioButton_combat0.Checked)
                BotOptions.Target_COMBAT = 0;
            if (radioButton_combat1.Checked)
                BotOptions.Target_COMBAT = 1;
            if (radioButton_combat2.Checked)
                BotOptions.Target_COMBAT = 2;
            //this.Hide();

            //Normal Move Before Attack v385//
            if (checkBox_active_move_first_normal.Checked)
                Globals.gamedata.botoptions.MoveFirstNormal = 1;
            else
                Globals.gamedata.botoptions.MoveFirstNormal = 0;

            //Move before targeting v391
            if (checkBox_movebeforetargeting.Checked)
                Globals.gamedata.botoptions.MoveBeforeTargeting = 1;
            else
                Globals.gamedata.botoptions.MoveBeforeTargeting = 0;

            //Dead logout/return/toggle v391
            if (checkBox_DeadReturn.Checked)
            {
                Globals.gamedata.botoptions.DeadReturn = comboBox_DeadReturn.SelectedIndex;
                Globals.gamedata.botoptions.DeadReturnDelay = System.Convert.ToInt32(textBox_DeadReturnDelay.Text);
            }
            else
            {
                Globals.gamedata.botoptions.DeadReturn = -1;
            }

            if (checkBox_DeadLogOut.Checked)
            {
                Globals.gamedata.botoptions.DeadLogout = 1;
                Globals.gamedata.botoptions.DeadLogoutDelay = System.Convert.ToInt32(textBox_DeadLogOutDelay.Text);
            }
            else
                Globals.gamedata.botoptions.DeadLogout = 0;

            if (checkBox_DeadToggleBotting.Checked)
                Globals.gamedata.botoptions.DeadToggleBotting = 1;
            else
                Globals.gamedata.botoptions.DeadToggleBotting = 0;

            //Advanced
            Globals.gamedata.botoptions.AntiKSDelay = (int)numericUpDown_anti_ks_delay.Value;
            Globals.gamedata.botoptions.AutoFollowDelay = (int)numericUpDown_autofollow_delay.Value;
            Globals.gamedata.botoptions.BlacklistTries = (int)numericUpDown_blacklist_tries.Value;
            Globals.gamedata.botoptions.PickupTimeout = (int)numericUpDown_pickuptimeout.Value;


            //Loot Type v386
            if (comboBox_LootType.SelectedIndex == 0)
            {
                Globals.gamedata.LootType = 0;
            }
            else if (comboBox_LootType.SelectedIndex == 1)
            {
                Globals.gamedata.LootType = 1;
            }
            else if (comboBox_LootType.SelectedIndex == 2)
            {
                Globals.gamedata.LootType = 2;
            }
            else if (comboBox_LootType.SelectedIndex == 3)
            {
                Globals.gamedata.LootType = 3;
            }
            else if (comboBox_LootType.SelectedIndex == 4)
            {
                Globals.gamedata.LootType = 4;
            }
            else
            {
                Globals.gamedata.LootType = 0;
            }

            // Content Filter
            if (cf_targetselected.Checked)
                Globals.lagfilter_TargetSelected = true;
            else
                Globals.lagfilter_TargetSelected = false;

            if (cf_targetunselected.Checked)
                Globals.lagfilter_TargetUnselected = true;
            else
                Globals.lagfilter_TargetUnselected = false;

            if (cf_filtermagicskill.Checked)
                Globals.lagfilter_Skills = true;
            else
                Globals.lagfilter_Skills = false;

            if (cf_ExBrExtraUserInfo.Checked)
                Globals.lagfilter_ExBrExtraUserInfo = true;
            else
                Globals.lagfilter_ExBrExtraUserInfo = false;

            if (cf_striptitle.Checked)
                Globals.lagfilter_xf_ci_striptitle = true;
            else
                Globals.lagfilter_xf_ci_striptitle = false;

            if (cf_stripenchant.Checked)
                Globals.lagfilter_xf_ci_stripenchant = true;
            else
                Globals.lagfilter_xf_ci_stripenchant = false;

            if (cf_stripaugment.Checked)
                Globals.lagfilter_xf_ci_stripaug = true;
            else
                Globals.lagfilter_xf_ci_stripaug = false;

            if (cf_zerononvisible.Checked)
                Globals.lagfilter_xf_ci_stripunseen = true;
            else
                Globals.lagfilter_xf_ci_stripunseen = false;

            if (cf_one_gender.Checked)
                Globals.lagfilter_xf_ci_simple_gender = true;
            else
                Globals.lagfilter_xf_ci_simple_gender = false;

            if (cf_simple_appearance.Checked)
                Globals.lagfilter_xf_ci_simple_apperance = true;
            else
                Globals.lagfilter_xf_ci_simple_apperance = false;

            if (cf_norecs.Checked)
                Globals.lagfilter_xf_ci_striprecs = true;
            else
                Globals.lagfilter_xf_ci_striprecs = false;

            if (cf_dwarfmode.Checked)
                Globals.lagfilter_xf_ci_simple_race = true;
            else
                Globals.lagfilter_xf_ci_simple_race = false;

        }

        private void button_add_Click(object sender, System.EventArgs e)
        {
            button_update.Enabled = false;

            try
            {
                uint id = (uint)skill_ids[comboBox_buffheal_skill.SelectedIndex];// (Util.GetInt32(textBox_sc_item.Text) - 1) + (Util.GetInt32(textBox_sc_page.Text) - 1) * Globals.Skills_PerPage;

                ListViewItem ObjListItem = listView_buffheal.Items.Add(comboBox_buffheal_skill.Text);//name
                ObjListItem.SubItems.Add(comboBox_buffheal_trait.Text);
                ObjListItem.SubItems.Add(textBox_buffheal_names.Text);
                ObjListItem.SubItems.Add(textBox_buffheal_min_per.Text);
                ObjListItem.SubItems.Add(textBox_buffheal_delay.Text);
                ObjListItem.SubItems.Add(textBox_buffheal_mp.Text);
                if (checkBox_target.Checked)
                    ObjListItem.SubItems.Add("1");
                else
                    ObjListItem.SubItems.Add("0");
                ObjListItem.SubItems.Add(comboBox_buffheal_trait.SelectedIndex.ToString());
                ObjListItem.SubItems.Add(id.ToString());

                ObjListItem.Checked = checkBox_buffheal_on.Checked;
            }
            catch
            {
                //failed... oh well
            }
        }

        private void button_additem_Click(object sender, System.EventArgs e)
        {
            button_updateitem.Enabled = false;

            try
            {
                uint id = (uint)item_ids[comboBox_item1.SelectedIndex];

                ListViewItem ObjListItem = listView_item.Items.Add(comboBox_item1.Text);//name
                ObjListItem.SubItems.Add(comboBox_trait1.Text);
                ObjListItem.SubItems.Add(textBox_itemper1.Text);
                ObjListItem.SubItems.Add(textBox_itemdelay1.Text);
                ObjListItem.SubItems.Add(comboBox_trait1.SelectedIndex.ToString());
                if (comboBox_item1.SelectedIndex == -1)
                    ObjListItem.SubItems.Add("0");
                else
                    ObjListItem.SubItems.Add(id.ToString());

                ObjListItem.Checked = checkBox_item1.Checked;
            }
            catch
            {
                //failed... oh well
            }
        }

        private void button_loadoptions_Click(object sender, System.EventArgs e)
        {
            //load data
            this.Enabled = false;//diable the screen

            try
            {
                DialogResult okOrNo;//dialog return value, if it is DialogResult.OK then everything is OK

                openFileDialog1.InitialDirectory = Globals.PATH + "\\Options";//Initial dir, where it begins looking at.
                openFileDialog1.Filter = "Option Files (*.l2d)|*.l2d";//this particualr format is for one file type.
                openFileDialog1.FilterIndex = 1;//this means that the first description is the default one.
                openFileDialog1.RestoreDirectory = true;//The next dialog box opened will start at the inital dir.
                okOrNo = openFileDialog1.ShowDialog();//open the dialog box and save the result.			

                if (okOrNo == DialogResult.OK)//if the dialog box works
                {
                    //load now
                    ClearData();

                    StreamReader filein = new StreamReader(openFileDialog1.OpenFile());//create a new streamwritter from the stream it returns
                    ReadData(filein);//load everything
                    filein.Close();//close the file
                }
            }
            catch
            {
                Globals.l2net_home.Add_Error("ERROR WHILE LOADING DATA!");
            }

            this.Enabled = true;//renable everything
        }

        private void button_saveoptions_Click(object sender, System.EventArgs e)
        {
            //save data
            this.Enabled = false;

            try
            {
                DialogResult okOrNo;//dialog return value, if it is DialogResult.OK then everything is OK

                saveFileDialog1.InitialDirectory = Globals.PATH + "\\Options";//Initial dir, where it begins looking at.
                saveFileDialog1.Filter = "Option Files (*.l2d)|*.l2d";//this particualr format is for one file type.
                saveFileDialog1.FilterIndex = 1;//this means that the first description is the default one.
                saveFileDialog1.RestoreDirectory = true;//The next dialog box opened will start at the inital dir.
                okOrNo = saveFileDialog1.ShowDialog();//open the dialog box and save the result.			

                if (okOrNo == DialogResult.OK)//if the dialog box displays OK
                {
                    StreamWriter fileout = new StreamWriter(saveFileDialog1.OpenFile());//create a streamwritter from the stream the dialog box returns
                    StoreData(fileout);//store everything
                    fileout.Close();//close the file
                }
            }
            catch
            {
                Globals.l2net_home.Add_Error("ERROR WHILE SAVING DATA!");
            }

            this.Enabled = true;
        }

        private void button_clearoptions_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        private void ClearData()
        {
            #region Clear Botoptions
            //Set everything to its initial state
            try
            {
                //Party tab
                checkBox_activefollow.Checked = false;
                radioButton_ActiveFollow_style1.Checked = false;
                radioButton_ActiveFollow_style2.Checked = true;
                textBox_activefollow_name.Text = "";
                textBox_ActiveFollow_Dist.Text = "";
                checkBox_activefollow_attack.Checked = false;
                checkBox_accept_party.Checked = false;
                textBox_accept_party.Text = "";
                checkBox_accept_rez.Checked = false;
                textBox_accept_rez.Text = "";
                checkBox_activefollow_target.Checked = false;
                checkBox_buff_control.Checked = false;
                checkBox_buff_shift.Checked = false;
                checkBox_auto_invite.Checked = false;
                textBox_auto_invite.Text = "";
                checkBox_oop.Checked = false;
                textBox_oop.Text = "";
                comboBox_LootType.SelectedIndex = 0;
                checkBox_drop_leader.Checked = false;
                checkBox_accept_rez_clan.Checked = false;
                checkBox_accept_rez_Party.Checked = false;
                checkBox_accept_party_clan.Checked = false;
                checkBox_accept_rez_alliance.Checked = false;
                checkBox_accept_party_alliance.Checked = false;
                textBox_ActiveFollow_Dist.Text = "200";

                //Buffs
                listView_buffheal.Items.Clear();

                //Toggles
                listView_toggles.Items.Clear();

                //Items
                listView_item.Items.Clear();

                //DoNot
                listView_donot_items.Items.Clear();
                listView_donot_npcs.Items.Clear();
                checkBox_Ign_Raidbosses.Checked = true;
                checkBox_Ign_TreasureChests.Checked = true;
                checkBox_ignore_no_mesh.Checked = true;
                checkBox_ignoreitems.Checked = true;
                checkBox_Ign_Summons.Checked = true;
                checkBox_AttackOnly.Checked = false;
                checkBox_PickOnly.Checked = false;

                //Autofighter
                checkBox_cancel_target.Checked = false;
                checkBox_autospoil.Checked = false;
                checkBox_MoveToLoc.Checked = false;
                checkBox_OutOfCombat.Checked = false;
                checkBox_UntilSuccess.Checked = false;
                checkBox_spoilcrush.Checked = false;
                checkBox_use_plunder.Checked = true;
                checkBox_autosweep.Checked = false;
                checkBox_active_target.Checked = false;
                checkBox_active_attack.Checked = false;
                checkBox_pickup.Checked = false;
                textBox_pickup_range.Text = "250";
                checkBox_active_move_first.Checked = false;
                textBox_active_move_range.Text = "100";
                checkBox_StuckCheck.Checked = false;
                checkBox_OnlyPickMine.Checked = true;
                checkBox_active_move_first_normal.Checked = false;
                checkBox_AutoBlacklist.Checked = false;
                checkBox_DeadLogOut.Checked = false;
                checkBox_DeadReturn.Checked = false;
                comboBox_DeadReturn.SelectedIndex = 0;
                textBox_DeadLogOutDelay.Text = "30";
                textBox_DeadReturnDelay.Text = "10";
                checkBox_DeadToggleBotting.Checked = false;
                textBox_spoil_mp.Text = "50";
                checkBox_cancel_target.Checked = false;
                textBox_Moveto_X.Text = "0";
                textBox_Moveto_Y.Text = "0";
                textBox_Moveto_Z.Text = "0";
                textBox_MoveToLeash.Text = "100";

                //Advanced
                numericUpDown_anti_ks_delay.Value = 20;
                numericUpDown_autofollow_delay.Value = 10;
                numericUpDown_blacklist_tries.Value = 5;
                numericUpDown_pickuptimeout.Value = 2;
                textBox_Custom_WindowTitle.Text = null;
                this.Text = "Bot Options";
                Globals.gamedata.botoptions.CustomWindowTitle = false;
                Globals.l2net_home.SetName();


                //Bounding Polygon
                listView_border.Items.Clear();
                BotOptions.Target_ZRANGE = 1000;

                //Combat
                listView_combat.Items.Clear();

                //Targeting
                checkBox_portect_priority.Checked = false;
                radioButton_type0.Checked = true;
                radioButton_type1.Checked = false;
                radioButton_type2.Checked = false;
                radioButton_attackable0.Checked = true;
                radioButton_attackable1.Checked = false;
                radioButton_attackable2.Checked = false;
                radioButton_alive0.Checked = true;
                radioButton_alive1.Checked = false;
                radioButton_alive2.Checked = false;
                radioButton_inbox0.Checked = true;
                radioButton_inbox1.Checked = false;
                radioButton_inbox2.Checked = false;
                radioButton_combat0.Checked = false;
                radioButton_combat1.Checked = false;
                radioButton_combat2.Checked = true;

                //Sound Alerts
                checkBox_2waywar.Checked = false;
                checkBox_1waywar.Checked = false;
                checkBox_n1waywar.Checked = false;
                checkBox_hp.Checked = false;
                checkBox_mp.Checked = false;
                checkBox_cp.Checked = false;
                checkBox_clan.Checked = false;
                checkBox_player.Checked = false;
                textBox_hp.Text = "";
                textBox_mp.Text = "";
                textBox_cp.Text = "";
                textBox_clan.Text = "";
                textBox_player.Text = "";
                checkBox_clan_ignore.Checked = false;
                checkBox_player_ignore.Checked = false;
                checkBox_whitechat.Checked = false;
                checkBox_privatemessage.Checked = false;
                checkBox_friendchat.Checked = false;

                //Log Out
                checkBox_2waywar_logout.Checked = false;
                checkBox_1waywar_logout.Checked = false;
                checkBox_n1waywar_logout.Checked = false;
                checkBox_hp_logout.Checked = false;
                checkBox_mp_logout.Checked = false;
                checkBox_cp_logout.Checked = false;
                checkBox_clan_logout.Checked = false;
                checkBox_player_logout.Checked = false;
                textBox_hp_logout.Text = "";
                textBox_mp_logout.Text = "";
                textBox_cp_logout.Text = "";
                textBox_clan_logout.Text = "";
                textBox_player_logout.Text = "";

                //Rest Options
                checkBox_RestBelowHP.Checked = false;
                numericUpDown_RestBelowHP.Value = 50;
                checkBox_RestUntilHP.Checked = false;
                numericUpDown_RestUntilHP.Value = 100;
                checkBox_RestBelowMP.Checked = false;
                numericUpDown_RestBelowMP.Value = 50;
                checkBox_RestUntilMP.Checked = false;
                numericUpDown_RestUntilMP.Value = 100;
                checkBox_FollowRest.Checked = false;
                textBox_FollowRestName.Text = "";

                // v389 Content Filtering
                cf_targetselected.Checked = false;
                cf_targetunselected.Checked = false;
                cf_filtermagicskill.Checked = false;
                cf_ExBrExtraUserInfo.Checked = false;
                cf_striptitle.Checked = false;
                cf_stripenchant.Checked = false;
                cf_stripaugment.Checked = false;
                cf_zerononvisible.Checked = false;
                cf_one_gender.Checked = false;
                cf_simple_appearance.Checked = false;
                cf_norecs.Checked = false;
                cf_dwarfmode.Checked = false;

            }
            catch
            {
            }

            #endregion
        }

        private void ReadData(StreamReader file)
        {
            string line;

            //active follow
            if (Util.GetInt32(file.ReadLine()) == 1)
                checkBox_activefollow.Checked = true;
            else
                checkBox_activefollow.Checked = false;
            //active follow style
            if (Util.GetInt32(file.ReadLine()) == 1)
                radioButton_ActiveFollow_style1.Checked = true;
            else
                radioButton_ActiveFollow_style2.Checked = true;
            //names
            textBox_activefollow_name.Text = file.ReadLine();
            //dist
            textBox_ActiveFollow_Dist.Text = file.ReadLine();
            //active follow attack
            if (Util.GetInt32(file.ReadLine()) == 1)
                checkBox_activefollow_attack.Checked = true;
            else
                checkBox_activefollow_attack.Checked = false;

            //load buffs
            try
            {
                int cnt = Util.GetInt32(file.ReadLine());
                for (int i = 0; i < cnt; i++)
                {
                    //name
                    string names = file.ReadLine();
                    ListViewItem ObjListItem = listView_buffheal.Items.Add("");
                    //checked
                    line = file.ReadLine();
                    if (Util.GetInt32(line) == 1)
                        ObjListItem.Checked = true;
                    else
                        ObjListItem.Checked = false;
                    //type
                    int type = Util.GetInt32(file.ReadLine());
                    ObjListItem.SubItems.Add(BuffTargetClass.Get_BuffTrigger_Name(type));

                    //shortcut
                    uint sc_id = Util.GetUInt32(file.ReadLine());
                    ObjListItem.SubItems[0].Text = Util.GetSkillName(sc_id, 1);
                    ObjListItem.SubItems.Add(names); //((sc_id % Globals.Skills_PerPage) + 1).ToString() + " : " + (((int)(sc_id / Globals.Skills_PerPage)) + 1).ToString());
                    ObjListItem.SubItems.Add(file.ReadLine());//Min_Per
                    ObjListItem.SubItems.Add(file.ReadLine());//TickDuration
                    ObjListItem.SubItems.Add(file.ReadLine());//Min_MP
                    ObjListItem.SubItems.Add(file.ReadLine());//NeedTarget
                    ObjListItem.SubItems.Add(type.ToString());//trait id
                    ObjListItem.SubItems.Add(sc_id.ToString());//shortcut id
                }
            }
            catch
            {
                //failed on buffs... bleh
                return;
            }

            //load items
            try
            {
                int cnt = Util.GetInt32(file.ReadLine());
                for (int i = 0; i < cnt; i++)
                {
                    //itemname
                    line = file.ReadLine();
                    ListViewItem ObjListItem = listView_item.Items.Add(line);
                    //checked
                    line = file.ReadLine();
                    if (Util.GetInt32(line) == 1)
                        ObjListItem.Checked = true;
                    else
                        ObjListItem.Checked = false;
                    //type
                    int type = Util.GetInt32(file.ReadLine());
                    ObjListItem.SubItems.Add(BuffTargetClass.Get_BuffTrigger_Name(type));

                    ObjListItem.SubItems.Add(file.ReadLine());//Min_Per
                    ObjListItem.SubItems.Add(file.ReadLine());//TickDuration
                    ObjListItem.SubItems.Add(type.ToString());//trait id
                    ObjListItem.SubItems.Add(file.ReadLine());//item id
                }
            }
            catch
            {
                //failed on items...bleh, maybe old save data?
                return;
            }

            //load DoNotItems
            try
            {
                int cnt = Util.GetInt32(file.ReadLine());

                uint id;
                for (int i = 0; i < cnt; i++)
                {
                    id = Util.GetUInt32(file.ReadLine());
                    ListViewItem ObjListItem = listView_donot_items.Items.Add(id.ToString());
                    ObjListItem.SubItems.Add(Util.GetItemName(id));
                }
            }
            catch
            {
                //failed to load DoNotItems
                return;
            }

            //load DoNotNPCs
            try
            {
                int cnt = Util.GetInt32(file.ReadLine());

                uint id;
                for (int i = 0; i < cnt; i++)
                {
                    id = Util.GetUInt32(file.ReadLine());
                    ListViewItem ObjListItem = listView_donot_npcs.Items.Add(id.ToString());
                    ObjListItem.SubItems.Add(Util.GetNPCName(id));
                }
            }
            catch
            {
                //failed to load DoNotNPCs
                return;
            }

            try
            {
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_autospoil.Checked = true;
                else
                    checkBox_autospoil.Checked = false;

                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_spoilcrush.Checked = true;
                else
                    checkBox_spoilcrush.Checked = false;

                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_autosweep.Checked = true;
                else
                    checkBox_autosweep.Checked = false;

                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_ignoreitems.Checked = true;
                else
                    checkBox_ignoreitems.Checked = false;
            }
            catch
            {
                //failed to load... old data file?
                return;
            }

            //v227 stuff
            try
            {
                Globals.gamedata.botoptions.HealRange = Util.GetInt32(file.ReadLine());
                textBox_buffrange.Text = Globals.gamedata.botoptions.HealRange.ToString();

                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_accept_party.Checked = true;
                else
                    checkBox_accept_party.Checked = false;

                textBox_accept_party.Text = file.ReadLine();

                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_accept_rez.Checked = true;
                else
                    checkBox_accept_rez.Checked = false;

                textBox_accept_rez.Text = file.ReadLine();
            }
            catch
            {
                return;
            }

            //bounding polygon stuff
            try
            {
                int p_ct = Util.GetInt32(file.ReadLine());

                listView_border.Items.Clear();

                for (int i = 0; i < p_ct; i++)
                {
                    ListViewItem ObjListItem = listView_border.Items.Add(file.ReadLine());
                    ObjListItem.SubItems.Add(file.ReadLine());
                }
            }
            catch
            {
                return;
            }

            //load combat settings
            try
            {
                int cnt = Util.GetInt32(file.ReadLine());
                for (int i = 0; i < cnt; i++)
                {
                    CombatTargetClass ct = new CombatTargetClass();

                    if (Util.GetInt32(file.ReadLine()) == 1)
                        ct.Active = true;
                    else
                        ct.Active = false;

                    ct.Type = (BuffTriggers)Util.GetInt32(file.ReadLine());
                    ct.Conditional = Util.GetInt32(file.ReadLine());
                    ct.ShortCutID = Util.GetInt32(file.ReadLine());
                    ct.Min_Per = Util.GetInt32(file.ReadLine());
                    ct.Min_MP = Util.GetInt32(file.ReadLine());
                    ct.TickDuration = Util.GetInt32(file.ReadLine());

                    string type = BuffTargetClass.Get_BuffTrigger_Name((int)ct.Type);

                    ListViewItem ObjListItem = new ListViewItem(type);

                    switch (ct.Conditional)
                    {
                        case 0://>
                            ObjListItem.SubItems.Add(">=");
                            break;
                        case 1://<
                            ObjListItem.SubItems.Add("<=");
                            break;
                    }
                    ObjListItem.SubItems.Add(ct.Min_Per.ToString());
                    ObjListItem.SubItems.Add(((ct.ShortCutID % Globals.Skills_PerPage) + 1).ToString() + " : " + (((int)(ct.ShortCutID / Globals.Skills_PerPage)) + 1).ToString());
                    ObjListItem.SubItems.Add(ct.TickDuration.ToString());
                    ObjListItem.SubItems.Add(ct.Min_MP.ToString());
                    ObjListItem.SubItems.Add(((byte)ct.Type).ToString());
                    ObjListItem.SubItems.Add(ct.Conditional.ToString());
                    ObjListItem.SubItems.Add(ct.ShortCutID.ToString());
                    ObjListItem.Checked = ct.Active;

                    listView_combat.Items.Add(ObjListItem);
                }
            }
            catch
            {
                //failed on items...bleh, maybe old save data?
                return;
            }

            try
            {
                //active follow target
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_activefollow_target.Checked = true;
                else
                    checkBox_activefollow_target.Checked = false;
            }
            catch
            {
                //old data files
                return;
            }

            try
            {
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_active_target.Checked = true;
                else
                    checkBox_active_target.Checked = false;

                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_active_attack.Checked = true;
                else
                    checkBox_active_attack.Checked = false;

                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_pickup.Checked = true;
                else
                    checkBox_pickup.Checked = false;

                textBox_pickup_range.Text = file.ReadLine();

                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_buff_control.Checked = true;
                else
                    checkBox_buff_control.Checked = false;

                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_buff_shift.Checked = true;
                else
                    checkBox_buff_shift.Checked = false;

            }
            catch
            {
                //old data files
                return;
            }

            try
            {
                BotOptions.Target_ZRANGE = Util.GetInt32(file.ReadLine());
            }
            catch
            {
                //old data files
                return;
            }

            try
            {
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_auto_invite.Checked = true;
                else
                    checkBox_auto_invite.Checked = false;

                textBox_auto_invite.Text = file.ReadLine();

                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_oop.Checked = true;
                else
                    checkBox_oop.Checked = false;

                textBox_oop.Text = file.ReadLine();

                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_portect_priority.Checked = true;
                else
                    checkBox_portect_priority.Checked = false;
            }
            catch
            {
                //old data files
                return;
            }

            //v360 stuff
            try
            {
                //sound alerts
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_2waywar.Checked = true;
                else
                    checkBox_2waywar.Checked = false;
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_1waywar.Checked = true;
                else
                    checkBox_1waywar.Checked = false;
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_n1waywar.Checked = true;
                else
                    checkBox_n1waywar.Checked = false;
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_hp.Checked = true;
                else
                    checkBox_hp.Checked = false;
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_mp.Checked = true;
                else
                    checkBox_mp.Checked = false;
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_cp.Checked = true;
                else
                    checkBox_cp.Checked = false;
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_clan.Checked = true;
                else
                    checkBox_clan.Checked = false;
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_player.Checked = true;
                else
                    checkBox_player.Checked = false;
                textBox_hp.Text = file.ReadLine();
                textBox_mp.Text = file.ReadLine();
                textBox_cp.Text = file.ReadLine();
                textBox_clan.Text = file.ReadLine();
                textBox_player.Text = file.ReadLine();

                //logout
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_2waywar_logout.Checked = true;
                else
                    checkBox_2waywar_logout.Checked = false;
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_1waywar_logout.Checked = true;
                else
                    checkBox_1waywar_logout.Checked = false;
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_n1waywar_logout.Checked = true;
                else
                    checkBox_n1waywar_logout.Checked = false;
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_hp_logout.Checked = true;
                else
                    checkBox_hp_logout.Checked = false;
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_mp_logout.Checked = true;
                else
                    checkBox_mp_logout.Checked = false;
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_cp_logout.Checked = true;
                else
                    checkBox_cp_logout.Checked = false;
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_clan_logout.Checked = true;
                else
                    checkBox_clan_logout.Checked = false;
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_player_logout.Checked = true;
                else
                    checkBox_player_logout.Checked = false;
                textBox_hp_logout.Text = file.ReadLine();
                textBox_mp_logout.Text = file.ReadLine();
                textBox_cp_logout.Text = file.ReadLine();
                textBox_clan_logout.Text = file.ReadLine();
                textBox_player_logout.Text = file.ReadLine();

                //targeting stuff
                switch (Util.GetInt32(file.ReadLine()))
                {
                    case 0:
                        radioButton_type0.Checked = true;
                        break;
                    case 1:
                        radioButton_type1.Checked = true;
                        break;
                    default:
                        radioButton_type2.Checked = true;
                        break;
                }
                switch (Util.GetInt32(file.ReadLine()))
                {
                    case 0:
                        radioButton_attackable0.Checked = true;
                        break;
                    case 1:
                        radioButton_attackable1.Checked = true;
                        break;
                    default:
                        radioButton_attackable2.Checked = true;
                        break;
                }
                switch (Util.GetInt32(file.ReadLine()))
                {
                    case 0:
                        radioButton_alive0.Checked = true;
                        break;
                    case 1:
                        radioButton_alive1.Checked = true;
                        break;
                    default:
                        radioButton_alive2.Checked = true;
                        break;
                }
                switch (Util.GetInt32(file.ReadLine()))
                {
                    case 0:
                        radioButton_inbox0.Checked = true;
                        break;
                    case 1:
                        radioButton_inbox1.Checked = true;
                        break;
                    default:
                        radioButton_inbox2.Checked = true;
                        break;
                }
                switch (Util.GetInt32(file.ReadLine()))
                {
                    case 0:
                        radioButton_combat0.Checked = true;
                        break;
                    case 1:
                        radioButton_combat1.Checked = true;
                        break;
                    default:
                        radioButton_combat2.Checked = true;
                        break;
                }
            }
            catch
            {
                //old data files
            }

            //v371 stuff
            try
            {
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_active_move_first.Checked = true;
                else
                    checkBox_active_move_first.Checked = false;

                textBox_active_move_range.Text = file.ReadLine();

                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_clan_ignore.Checked = true;
                else
                    checkBox_clan_ignore.Checked = false;

                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_player_ignore.Checked = true;
                else
                    checkBox_player_ignore.Checked = false;


                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_whitechat.Checked = true;
                else
                    checkBox_whitechat.Checked = false;

                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_privatemessage.Checked = true;
                else
                    checkBox_privatemessage.Checked = false;

                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_friendchat.Checked = true;
                else
                    checkBox_friendchat.Checked = false;
            }
            catch
            {
                //old data file
            }

            //v383 Rest options
            try
            {
                //Solo Settings
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_RestBelowHP.Checked = true;
                else
                    checkBox_RestBelowHP.Checked = false;
                numericUpDown_RestBelowHP.Value = System.Convert.ToInt32(file.ReadLine());

                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_RestUntilHP.Checked = true;
                else
                    checkBox_RestUntilHP.Checked = false;
                numericUpDown_RestUntilHP.Value = System.Convert.ToInt32(file.ReadLine());

                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_RestBelowMP.Checked = true;
                else
                    checkBox_RestBelowMP.Checked = false;
                numericUpDown_RestBelowMP.Value = System.Convert.ToInt32(file.ReadLine());

                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_RestUntilMP.Checked = true;
                else
                    checkBox_RestUntilMP.Checked = false;
                numericUpDown_RestUntilMP.Value = System.Convert.ToInt32(file.ReadLine());

                //Party Settings
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_FollowRest.Checked = true;
                else
                    checkBox_FollowRest.Checked = false;
                textBox_FollowRestName.Text = file.ReadLine();
            }
            catch
            {
                //old data file
            }

            //v384 Ignore Raidbosses and Chests
            //v384 Beta 2: Auto Unstuck
            //v384 Beta 6: Ignore Meshless Items
            try
            {
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_Ign_Raidbosses.Checked = true;
                else
                    checkBox_Ign_Raidbosses.Checked = false;

                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_Ign_TreasureChests.Checked = true;
                else
                    checkBox_Ign_TreasureChests.Checked = false;

                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_StuckCheck.Checked = true;
                else
                    checkBox_StuckCheck.Checked = false;

                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_ignore_no_mesh.Checked = true;
                else
                    checkBox_ignore_no_mesh.Checked = false;
            }
            catch
            {
                //old data file
            }

            //v385 Ignore Summons & Normal Follow Attack & Only Pick Mine
            try
            {
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_Ign_Summons.Checked = true;
                else
                    checkBox_Ign_Summons.Checked = false;

                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_active_move_first_normal.Checked = true;
                else
                    checkBox_active_move_first_normal.Checked = false;

                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_OnlyPickMine.Checked = true;
                else
                    checkBox_OnlyPickMine.Checked = false;
            }
            catch
            {
                //old data file
            }

            //v386 Advanced Autofighter, Partyloot, Attack/pick only, Custom window title
            try
            {
                numericUpDown_anti_ks_delay.Value = System.Convert.ToInt32(file.ReadLine());
                numericUpDown_autofollow_delay.Value = System.Convert.ToInt32(file.ReadLine());
                numericUpDown_blacklist_tries.Value = System.Convert.ToInt32(file.ReadLine());
                //Loot Type
                comboBox_LootType.SelectedIndex = Util.GetInt32(file.ReadLine());
                //Attack Only
                if (Util.GetInt32(file.ReadLine()) == 1)
                {
                    checkBox_AttackOnly.Checked = true;
                }
                else
                {
                    checkBox_AttackOnly.Checked = false;
                }
                //Pick Only
                if (Util.GetInt32(file.ReadLine()) == 1)
                {
                    checkBox_PickOnly.Checked = true;
                }
                else
                {
                    checkBox_PickOnly.Checked = false;
                }
                textBox_Custom_WindowTitle.Text = file.ReadLine().ToString();
                if (textBox_Custom_WindowTitle.Text != String.Empty)
                {
                    this.Text = textBox_Custom_WindowTitle.Text;
                    Globals.gamedata.botoptions.CustomWindowTitle = true;
                    Globals.l2net_home.SetName(textBox_Custom_WindowTitle.Text);
                }
                else
                {
                    Globals.gamedata.botoptions.CustomWindowTitle = false;
                    Globals.l2net_home.SetName();
                    this.Text = "Bot Options";
                }

                // v389
                // Content Filtering

                if (Util.GetInt32(file.ReadLine()) == 1)
                {
                    cf_targetselected.Checked = true;
                }
                else
                {
                    cf_targetselected.Checked = false;
                }

                if (Util.GetInt32(file.ReadLine()) == 1)
                {
                    cf_targetunselected.Checked = true;
                }
                else
                {
                    cf_targetunselected.Checked = false;
                }

                if (Util.GetInt32(file.ReadLine()) == 1)
                {
                    cf_filtermagicskill.Checked = true;
                }
                else
                {
                    cf_filtermagicskill.Checked = false;
                }

                if (Util.GetInt32(file.ReadLine()) == 1)
                {
                    cf_ExBrExtraUserInfo.Checked = true;
                }
                else
                {
                    cf_ExBrExtraUserInfo.Checked = false;
                }

                if (Util.GetInt32(file.ReadLine()) == 1)
                {
                    cf_striptitle.Checked = true;
                }
                else
                {
                    cf_striptitle.Checked = false;
                }

                if (Util.GetInt32(file.ReadLine()) == 1)
                {
                    cf_stripenchant.Checked = true;
                }
                else
                {
                    cf_stripenchant.Checked = false;
                }

                if (Util.GetInt32(file.ReadLine()) == 1)
                {
                    cf_stripaugment.Checked = true;
                }
                else
                {
                    cf_stripaugment.Checked = false;
                }

                if (Util.GetInt32(file.ReadLine()) == 1)
                {
                    cf_zerononvisible.Checked = true;
                }
                else
                {
                    cf_zerononvisible.Checked = false;
                }

                if (Util.GetInt32(file.ReadLine()) == 1)
                {
                    cf_one_gender.Checked = true;
                }
                else
                {
                    cf_one_gender.Checked = false;
                }

                if (Util.GetInt32(file.ReadLine()) == 1)
                {
                    cf_simple_appearance.Checked = true;
                }
                else
                {
                    cf_simple_appearance.Checked = false;
                }

                if (Util.GetInt32(file.ReadLine()) == 1)
                {
                    cf_norecs.Checked = true;
                }
                else
                {
                    cf_norecs.Checked = false;
                }

                if (Util.GetInt32(file.ReadLine()) == 1)
                {
                    cf_dwarfmode.Checked = true;
                }
                else
                {
                    cf_dwarfmode.Checked = false;
                }


                //v391: Auto Blacklist
                if (Util.GetInt32(file.ReadLine()) == 1)
                {
                    checkBox_AutoBlacklist.Checked = true;
                }
                else
                {
                    checkBox_AutoBlacklist.Checked = false;
                }
                //v391: Move before target
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_movebeforetargeting.Checked = true;
                else
                    checkBox_movebeforetargeting.Checked = false;

                //v391: Dead logout/return/Toggle
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_DeadLogOut.Checked = true;
                else
                    checkBox_DeadLogOut.Checked = false;

                int tmp = Util.GetInt32(file.ReadLine());

                if (tmp >= 0)
                {
                    checkBox_DeadReturn.Checked = true;
                    comboBox_DeadReturn.SelectedIndex = tmp;
                }
                else
                {
                    checkBox_DeadReturn.Checked = false;
                    comboBox_DeadReturn.SelectedIndex = 0;
                }

                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_DeadToggleBotting.Checked = true;
                else
                    checkBox_DeadToggleBotting.Checked = false;

                //v391 AutoSpoil Until success
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_UntilSuccess.Checked = true;
                else
                    checkBox_UntilSuccess.Checked = false;

                //v391 Pickup After Attack
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_PickupAfterAttack.Checked = true;
                else
                    checkBox_PickupAfterAttack.Checked = false;

                //v391 Spoil MP Above
                textBox_spoil_mp.Text = file.ReadLine();
                if (string.IsNullOrWhiteSpace(textBox_spoil_mp.Text))
                    textBox_spoil_mp.Text = "100";

                //v391 Pet Assist
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_pet_autoassist.Checked = true;
                else
                    checkBox_pet_autoassist.Checked = false;

                //v391 Active follow attack instant
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_activefollow_attack_Instant.Checked = true;
                else
                    checkBox_activefollow_attack_Instant.Checked = false;

                //v392 Pickuptimeout
                numericUpDown_pickuptimeout.Value = (Util.GetInt32(file.ReadLine()));

                //v392B11: Summon stuff
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_Summon_autoassist.Checked = true;
                else
                    checkBox_Summon_autoassist.Checked = false;

                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_pet_soloattack.Checked = true;
                else
                    checkBox_pet_soloattack.Checked = false;

                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_summon_instantattack.Checked = true;
                else
                    checkBox_summon_instantattack.Checked = false;

                //v393: Plunder
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_use_plunder.Checked = true;
                else
                    checkBox_use_plunder.Checked = false;

            }
            catch
            {
                //Old Data file
            }
            //v396 cancel target
            try
            {
                //v396: Cancel_target
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_cancel_target.Checked = true;
                else
                    checkBox_cancel_target.Checked = false;

                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_drop_leader.Checked = true;
                else
                    checkBox_drop_leader.Checked = false;
            }
            catch
            {
                //Old Data file
            }
            //party/alliance accept shit
            try
            {
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_accept_rez_clan.Checked = true;
                else
                    checkBox_accept_rez_clan.Checked = false;

                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_accept_rez_alliance.Checked = true;
                else
                    checkBox_accept_rez_alliance.Checked = false; 
                
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_accept_party_clan.Checked = true;
                else
                    checkBox_accept_party_clan.Checked = false;

                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_accept_party_alliance.Checked = true;
                else
                    checkBox_accept_party_alliance.Checked = false;


            }
            catch
            {
                //Old Data file
            }

            //party rez accept
            try
            {
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_accept_rez_Party.Checked = true;
                else
                    checkBox_accept_rez_Party.Checked = false;
                }
            catch
            {
                //Old Data file
            }
            
            //Out of combat stuff
            try
            {
                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_MoveToLoc.Checked = true;
                else
                    checkBox_MoveToLoc.Checked = false;

                if (Util.GetInt32(file.ReadLine()) == 1)
                    checkBox_OutOfCombat.Checked = true;
                else
                    checkBox_OutOfCombat.Checked = false;


                textBox_Moveto_X.Text = Util.GetInt32(file.ReadLine()).ToString();
                textBox_Moveto_Y.Text = Util.GetInt32(file.ReadLine()).ToString();
                textBox_Moveto_Z.Text = Util.GetInt32(file.ReadLine()).ToString();
                textBox_MoveToLeash.Text = Util.GetInt32(file.ReadLine()).ToString();

            }
            catch
            {
                //Old Data file
            }

            //load toggles
            try
            {
                int cnt = Util.GetInt32(file.ReadLine());
                for (int i = 0; i < cnt; i++)
                {
                    //skillname
                    string skillname = file.ReadLine();
                    ListViewItem ObjListItem = listView_toggles.Items.Add(skillname);

                    //checked
                    line = file.ReadLine();
                    if (Util.GetInt32(line) == 1)
                        ObjListItem.Checked = true;
                    else
                        ObjListItem.Checked = false;

                    //trait
                    int trait = Util.GetInt32(file.ReadLine());
                    ObjListItem.SubItems.Add(BuffTargetClass.Get_BuffTrigger_Name_Toggle(trait));
                    
                    ObjListItem.SubItems.Add(file.ReadLine()); //XX>%
                    ObjListItem.SubItems.Add(file.ReadLine()); //XX<%
                    ObjListItem.SubItems.Add(trait.ToString()); //traitID
                    ObjListItem.SubItems.Add(file.ReadLine());//shortcut id
                }
            }
            catch
            {
                //failed on toggles... bleh
                return;
            }

            /*all the buff/heal settings
                    file.WriteLine(lv.SubItems[0].Text);//skill name
                    if (lv.Checked)
                        file.WriteLine("1");
                    else
                        file.WriteLine("0");
                    file.WriteLine(Util.GetInt32(lv.SubItems[1].Text));//trait
                    file.WriteLine(Util.GetInt32(lv.SubItems[2].Text));//XX>%
                    file.WriteLine(Util.GetInt32(lv.SubItems[3].Text));//XX<%
                    file.WriteLine(Util.GetInt32(lv.SubItems[4].Text));//traitID
                    file.WriteLine(Util.GetInt32(lv.SubItems[5].Text));//skillID
            }*/
        }

        private void StoreData(StreamWriter file)
        {
            //active follow?
            if (checkBox_activefollow.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");
            //active follow style
            if (radioButton_ActiveFollow_style1.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");
            //names
            file.WriteLine(textBox_activefollow_name.Text);
            //dist
            file.WriteLine(textBox_ActiveFollow_Dist.Text);
            //active follow attack?
            if (checkBox_activefollow_attack.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            try
            {
                //all the buff/heal settings
                file.WriteLine(listView_buffheal.Items.Count.ToString());//how many?
                foreach (System.Windows.Forms.ListViewItem lv in listView_buffheal.Items)
                {
                    file.WriteLine(lv.SubItems[2].Text);//names
                    if (lv.Checked)
                        file.WriteLine("1");
                    else
                        file.WriteLine("0");
                    file.WriteLine(Util.GetInt32(lv.SubItems[7].Text));//Type
                    file.WriteLine(Util.GetInt32(lv.SubItems[8].Text));//SkillID   //ShortCutID
                    file.WriteLine(Util.GetInt32(lv.SubItems[3].Text));//Min_Per
                    file.WriteLine(Util.GetInt64(lv.SubItems[4].Text));//TickDuration
                    file.WriteLine(Util.GetInt32(lv.SubItems[5].Text));//Min_MP
                    file.WriteLine(Util.GetInt32(lv.SubItems[6].Text));//NeedTarget
                }
            }
            catch
            {
                //crashed writing the buffs... meh
            }

            try
            {
                //all the items
                file.WriteLine(listView_item.Items.Count.ToString());//how many?
                foreach (System.Windows.Forms.ListViewItem lv in listView_item.Items)
                {
                    file.WriteLine(lv.SubItems[0].Text);//names
                    if (lv.Checked)
                        file.WriteLine("1");
                    else
                        file.WriteLine("0");
                    file.WriteLine(Util.GetInt32(lv.SubItems[4].Text));//Type
                    file.WriteLine(Util.GetInt32(lv.SubItems[2].Text));//Min_Per
                    file.WriteLine(Util.GetInt32(lv.SubItems[3].Text));//TickDuration
                    file.WriteLine(Util.GetInt32(lv.SubItems[5].Text));//item id

                }
            }
            catch
            {
                //crashed writing the items... meh
            }

            try
            {
                //DoNot items
                file.WriteLine(listView_donot_items.Items.Count.ToString());//how many?
                foreach (System.Windows.Forms.ListViewItem lv in listView_donot_items.Items)
                {
                    file.WriteLine(lv.SubItems[0].Text);//names
                }
            }
            catch
            {
                //crashed writing the items... meh
            }

            try
            {
                //DoNot NPCs
                file.WriteLine(listView_donot_npcs.Items.Count.ToString());//how many?
                foreach (System.Windows.Forms.ListViewItem lv in listView_donot_npcs.Items)
                {
                    file.WriteLine(lv.SubItems[0].Text);//names
                }
            }
            catch
            {
                //crashed writing the items... meh
            }

            if (checkBox_autospoil.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            if (checkBox_spoilcrush.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            if (checkBox_autosweep.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            if (checkBox_ignoreitems.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            file.WriteLine(textBox_buffrange.Text);

            if (checkBox_accept_party.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            file.WriteLine(textBox_accept_party.Text);

            if (checkBox_accept_rez.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            file.WriteLine(textBox_accept_rez.Text);

            //bounding polygon
            file.WriteLine(listView_border.Items.Count.ToString());
            foreach (System.Windows.Forms.ListViewItem lv in listView_border.Items)
            {
                file.WriteLine(lv.SubItems[0].Text);
                file.WriteLine(lv.SubItems[1].Text);
            }

            //combat stuff
            file.WriteLine(listView_combat.Items.Count.ToString());
            foreach (System.Windows.Forms.ListViewItem lv in listView_combat.Items)
            {
                if (lv.Checked)
                    file.WriteLine("1");
                else
                    file.WriteLine("0");

                file.WriteLine(lv.SubItems[6].Text);//trait id
                file.WriteLine(lv.SubItems[7].Text);//conditional id
                file.WriteLine(lv.SubItems[8].Text);//shortcut id
                file.WriteLine(lv.SubItems[2].Text);//min per
                file.WriteLine(lv.SubItems[5].Text);//min mp
                file.WriteLine(lv.SubItems[4].Text);//delay
            }

            //active follow target
            if (checkBox_activefollow_target.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            if (checkBox_active_target.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            if (checkBox_active_attack.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            if (checkBox_pickup.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            file.WriteLine(textBox_pickup_range.Text);

            if (checkBox_buff_control.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            if (checkBox_buff_shift.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            file.WriteLine(BotOptions.Target_ZRANGE.ToString());

            if (checkBox_auto_invite.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            file.WriteLine(textBox_auto_invite.Text);

            if (checkBox_oop.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            file.WriteLine(textBox_oop.Text);

            if (checkBox_portect_priority.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            //sounds alerts
            if (checkBox_2waywar.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");
            if (checkBox_1waywar.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");
            if (checkBox_n1waywar.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");
            if (checkBox_hp.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");
            if (checkBox_mp.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");
            if (checkBox_cp.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");
            if (checkBox_clan.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");
            if (checkBox_player.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");
            file.WriteLine(textBox_hp.Text);
            file.WriteLine(textBox_mp.Text);
            file.WriteLine(textBox_cp.Text);
            file.WriteLine(textBox_clan.Text);
            file.WriteLine(textBox_player.Text);

            //logout
            if (checkBox_2waywar_logout.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");
            if (checkBox_1waywar_logout.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");
            if (checkBox_n1waywar_logout.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");
            if (checkBox_hp_logout.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");
            if (checkBox_mp_logout.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");
            if (checkBox_cp_logout.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");
            if (checkBox_clan_logout.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");
            if (checkBox_player_logout.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");
            file.WriteLine(textBox_hp_logout.Text);
            file.WriteLine(textBox_mp_logout.Text);
            file.WriteLine(textBox_cp_logout.Text);
            file.WriteLine(textBox_clan_logout.Text);
            file.WriteLine(textBox_player_logout.Text);

            if (radioButton_type0.Checked)
                file.WriteLine("0");
            else if (radioButton_type1.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("2");
            if (radioButton_attackable0.Checked)
                file.WriteLine("0");
            else if (radioButton_attackable1.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("2");
            if (radioButton_alive0.Checked)
                file.WriteLine("0");
            else if (radioButton_alive1.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("2");
            if (radioButton_inbox0.Checked)
                file.WriteLine("0");
            else if (radioButton_inbox1.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("2");
            if (radioButton_combat0.Checked)
                file.WriteLine("0");
            else if (radioButton_combat1.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("2");

            if (checkBox_active_move_first.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            file.WriteLine(textBox_active_move_range.Text);

            if (checkBox_clan_ignore.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");
            if (checkBox_player_ignore.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            if (checkBox_whitechat.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");
            if (checkBox_privatemessage.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");
            if (checkBox_friendchat.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");
            //Rest options - Solo Settings
            if (checkBox_RestBelowHP.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");
            file.WriteLine(numericUpDown_RestBelowHP.Value.ToString());

            if (checkBox_RestUntilHP.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");
            file.WriteLine(numericUpDown_RestUntilHP.Value.ToString());

            if (checkBox_RestBelowMP.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");
            file.WriteLine(numericUpDown_RestBelowMP.Value.ToString());

            if (checkBox_RestUntilMP.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");
            file.WriteLine(numericUpDown_RestUntilMP.Value.ToString());

            //Rest options - Party Settings
            if (checkBox_FollowRest.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");
            file.WriteLine(textBox_FollowRestName.Text);

            //Ignore raidbosses and chests
            if (checkBox_Ign_Raidbosses.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            if (checkBox_Ign_TreasureChests.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            //Auto unstuck
            if (checkBox_StuckCheck.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            //Ignore Meshless Items

            if (checkBox_ignore_no_mesh.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            //Ignore Summons
            if (checkBox_Ign_Summons.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            //Normal Move Before Attack
            if (checkBox_active_move_first_normal.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            //Only Pick Mine
            if (checkBox_OnlyPickMine.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            //Advanced Autofighter Settings
            file.WriteLine(numericUpDown_anti_ks_delay.Value.ToString());
            file.WriteLine(numericUpDown_autofollow_delay.Value.ToString());
            file.WriteLine(numericUpDown_blacklist_tries.Value.ToString());

            //Loot Type
            file.WriteLine(comboBox_LootType.SelectedIndex.ToString());

            //Attack Only NPC's in list
            if (checkBox_AttackOnly.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            //Pick Only Items in list
            if (checkBox_PickOnly.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            //Custom Window Title
            file.WriteLine(textBox_Custom_WindowTitle.Text);

            // v389
            // Content Filter
            if (cf_targetselected.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            if (cf_targetunselected.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            if (cf_filtermagicskill.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            if (cf_ExBrExtraUserInfo.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            if (cf_striptitle.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            if (cf_stripenchant.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            if (cf_stripaugment.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            if (cf_zerononvisible.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            if (cf_one_gender.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            if (cf_simple_appearance.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            if (cf_norecs.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            if (cf_dwarfmode.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            if (checkBox_AutoBlacklist.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            //Move before targeting
            if (checkBox_movebeforetargeting.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            //Dead logout/return/toggle botting
            if (checkBox_DeadLogOut.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            if (checkBox_DeadReturn.Checked)
                file.WriteLine(comboBox_DeadReturn.SelectedIndex.ToString());
            else
                file.WriteLine("-1");

            if (checkBox_DeadToggleBotting.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            //Autospoil until success
            if (checkBox_UntilSuccess.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            //Pickup after attack
            if (checkBox_PickupAfterAttack.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            //Spoil mp above
            file.WriteLine(textBox_spoil_mp.Text.ToString());

            //Pet autoassist
            if (checkBox_pet_autoassist.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            //Active follow attack instant
            if (checkBox_activefollow_attack_Instant.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            //Pickup delay

            file.WriteLine(numericUpDown_pickuptimeout.Value.ToString());

            //392B11: summon stuff
            if (checkBox_Summon_autoassist.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            if (checkBox_pet_soloattack.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            if (checkBox_summon_instantattack.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            //v393: Plunder
            if (checkBox_use_plunder.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            //396 cancel_target
            if (checkBox_cancel_target.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            if (checkBox_drop_leader.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            if (checkBox_accept_rez_clan.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            if (checkBox_accept_rez_alliance.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            if (checkBox_accept_party_clan.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            if (checkBox_accept_party_alliance.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            if (checkBox_accept_rez_Party.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            //418 move to loc
            if (checkBox_MoveToLoc.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            if (checkBox_OutOfCombat.Checked)
                file.WriteLine("1");
            else
                file.WriteLine("0");

            try
            {
                file.WriteLine(textBox_Moveto_X.Text.ToString());
                file.WriteLine(textBox_Moveto_Y.Text.ToString());
                file.WriteLine(textBox_Moveto_Z.Text.ToString());
                file.WriteLine(textBox_MoveToLeash.Text.ToString());
            }
            catch
            {
                file.WriteLine("0");
                file.WriteLine("0");
                file.WriteLine("0");
                file.WriteLine("100");
            }
            try
            {
                //all the toggles settings
                file.WriteLine(listView_toggles.Items.Count.ToString());//how many?
                foreach (System.Windows.Forms.ListViewItem lv in listView_toggles.Items)
                {
                    file.WriteLine(lv.SubItems[0].Text);//skill name
                    if (lv.Checked)
                        file.WriteLine("1");
                    else
                        file.WriteLine("0");
                    file.WriteLine(Util.GetInt32(lv.SubItems[4].Text));//trait
                    file.WriteLine(Util.GetInt32(lv.SubItems[2].Text));//XX>%
                    file.WriteLine(Util.GetInt32(lv.SubItems[3].Text));//XX<%
                    file.WriteLine(Util.GetInt32(lv.SubItems[5].Text));//skillID
                }
            }
            catch
            {
                //crashed writing the buffs... meh
            }
        }


        private void button_update_toggle_Click(object sender, EventArgs e)
        {

            button_update_toggle.Enabled = false;

            try
            {
                uint id = (uint)skill_ids[comboBox_skills_toggle.SelectedIndex]; 

                ListViewItem ObjListItem = listView_toggles.Items.Add(comboBox_skills_toggle.Text);//name
                ObjListItem.SubItems.Add(comboBox_trait_toggle.Text);
                ObjListItem.SubItems.Add(textBox_greaterthen_toggle.Text);
                ObjListItem.SubItems.Add(textBox_lesserthen_toggle.Text);
                ObjListItem.SubItems.Add(comboBox_trait_toggle.SelectedIndex.ToString());
                ObjListItem.SubItems.Add(id.ToString());

                ObjListItem.Checked = checkBox_onoff_toggle.Checked;

                listView_toggles.Items[last_toggle] = ObjListItem;
            }
            catch
            {
                //failed... oh well
            }

        }

        private void button_update_Click(object sender, System.EventArgs e)
        {
            button_update.Enabled = false;

            try
            {
                uint id = (uint)skill_ids[comboBox_buffheal_skill.SelectedIndex];// (Util.GetInt32(textBox_sc_item.Text) - 1) + (Util.GetInt32(textBox_sc_page.Text) - 1) * Globals.Skills_PerPage;

                ListViewItem ObjListItem = new ListViewItem(comboBox_buffheal_skill.Text);//name
                ObjListItem.SubItems.Add(comboBox_buffheal_trait.Text);
                ObjListItem.SubItems.Add(textBox_buffheal_names.Text);
                ObjListItem.SubItems.Add(textBox_buffheal_min_per.Text);
                ObjListItem.SubItems.Add(textBox_buffheal_delay.Text);
                ObjListItem.SubItems.Add(textBox_buffheal_mp.Text);
                if (checkBox_target.Checked)
                    ObjListItem.SubItems.Add("1");
                else
                    ObjListItem.SubItems.Add("0");
                ObjListItem.SubItems.Add(comboBox_buffheal_trait.SelectedIndex.ToString());
                ObjListItem.SubItems.Add(id.ToString());

                ObjListItem.Checked = checkBox_buffheal_on.Checked;

                listView_buffheal.Items[last_buff] = ObjListItem;
            }
            catch
            {
                //failed... oh well
            }
        }

        private void button_updateitem_Click(object sender, System.EventArgs e)
        {
            button_updateitem.Enabled = false;

            try
            {
                uint id = (uint)item_ids[comboBox_item1.SelectedIndex];

                ListViewItem ObjListItem = new ListViewItem(comboBox_item1.Text);//name
                ObjListItem.SubItems.Add(comboBox_trait1.Text);
                ObjListItem.SubItems.Add(textBox_itemper1.Text);
                ObjListItem.SubItems.Add(textBox_itemdelay1.Text);
                ObjListItem.SubItems.Add(comboBox_trait1.SelectedIndex.ToString());
                if (comboBox_item1.SelectedIndex == -1)
                    ObjListItem.SubItems.Add("0");
                else
                    ObjListItem.SubItems.Add(id.ToString());

                ObjListItem.Checked = checkBox_item1.Checked;

                listView_item.Items[last_item] = ObjListItem;
            }
            catch
            {
                //failed... oh well
            }
        }

        private void removeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //remove donot npcs
            if (listView_donot_npcs.SelectedIndices.Count > 0)
            {
                listView_donot_npcs.Items.RemoveAt(listView_donot_npcs.SelectedIndices[0]);
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //remove donot items
            if (listView_donot_items.SelectedIndices.Count > 0)
            {
                listView_donot_items.Items.RemoveAt(listView_donot_items.SelectedIndices[0]);
            }
        }

        private void button_donot_items_Click(object sender, EventArgs e)
        {
            //add item
            uint id = Util.GetUInt32(textBox_donot_items.Text);

            string name = Util.GetItemName(id);

            ListViewItem ObjListItem = listView_donot_items.Items.Add(id.ToString());//name
            ObjListItem.SubItems.Add(name);
        }

        private void button_donot_npcs_Click(object sender, EventArgs e)
        {
            //add npc
            uint id = Util.GetUInt32(textBox_donot_npcs.Text);

            if (id < Globals.NPC_OFF)
            {
                id += Globals.NPC_OFF;
            }

            string name = Util.GetNPCName(id);

            ListViewItem ObjListItem = listView_donot_npcs.Items.Add(id.ToString());//name
            ObjListItem.SubItems.Add(name);
        }

        private void button_addpolygon_Click(object sender, EventArgs e)
        {
            button_updatepolygon.Enabled = false;

            int x = Util.GetInt32(textBox_polygon_x.Text);
            int y = Util.GetInt32(textBox_polygon_y.Text);

            ListViewItem ObjListItem = listView_border.Items.Add(x.ToString());
            ObjListItem.SubItems.Add(y.ToString());
        }

        private void button_addcur_polygon_Click(object sender, EventArgs e)
        {
            int x = Util.Float_Int32(Globals.gamedata.my_char.X);
            int y = Util.Float_Int32(Globals.gamedata.my_char.Y);

            ListViewItem ObjListItem = listView_border.Items.Add(x.ToString());
            ObjListItem.SubItems.Add(y.ToString());
        }

        private void removeToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            //remove polygon
            if (listView_border.SelectedIndices.Count > 0)
            {
                listView_border.Items.RemoveAt(listView_border.SelectedIndices[0]);
            }
        }

        private void button_updatepolygon_Click(object sender, EventArgs e)
        {
            button_updatepolygon.Enabled = false;

            int x = Util.GetInt32(textBox_polygon_x.Text);
            int y = Util.GetInt32(textBox_polygon_y.Text);

            ListViewItem ObjListItem = new ListViewItem(x.ToString());
            ObjListItem.SubItems.Add(y.ToString());

            listView_border.Items[last_polygon] = ObjListItem;
        }

        private void button_combat_add_Click(object sender, EventArgs e)
        {
            button_combat_update.Enabled = false;

            try
            {
                int id = (Util.GetInt32(textBox_combat_sc_item.Text) - 1) + (Util.GetInt32(textBox_combat_sc_page.Text) - 1) * Globals.Skills_PerPage;

                ListViewItem ObjListItem = listView_combat.Items.Add(comboBox_combat_trait.Text);
                ObjListItem.SubItems.Add(comboBox_combat_conditional.Text);
                ObjListItem.SubItems.Add(textBox_combat_min_per.Text);
                ObjListItem.SubItems.Add(textBox_combat_sc_item.Text + " : " + textBox_combat_sc_page.Text);
                ObjListItem.SubItems.Add(textBox_combat_delay.Text);
                ObjListItem.SubItems.Add(textBox_combat_mp.Text);
                ObjListItem.SubItems.Add(comboBox_combat_trait.SelectedIndex.ToString());
                ObjListItem.SubItems.Add(comboBox_combat_conditional.SelectedIndex.ToString());
                ObjListItem.SubItems.Add(id.ToString());

                ObjListItem.Checked = checkBox_combat_on.Checked;
            }
            catch
            {
                //failed... oh well
            }
        }

        private void button_combat_update_Click(object sender, EventArgs e)
        {
            button_combat_update.Enabled = false;

            try
            {
                int id = (Util.GetInt32(textBox_combat_sc_item.Text) - 1) + (Util.GetInt32(textBox_combat_sc_page.Text) - 1) * Globals.Skills_PerPage;

                ListViewItem ObjListItem = new ListViewItem(comboBox_combat_trait.Text);
                ObjListItem.SubItems.Add(comboBox_combat_conditional.Text);
                ObjListItem.SubItems.Add(textBox_combat_min_per.Text);
                ObjListItem.SubItems.Add(textBox_combat_sc_item.Text + " : " + textBox_combat_sc_page.Text);
                ObjListItem.SubItems.Add(textBox_combat_delay.Text);
                ObjListItem.SubItems.Add(textBox_combat_mp.Text);
                ObjListItem.SubItems.Add(comboBox_combat_trait.SelectedIndex.ToString());
                ObjListItem.SubItems.Add(comboBox_combat_conditional.SelectedIndex.ToString());
                ObjListItem.SubItems.Add(id.ToString());

                ObjListItem.Checked = checkBox_combat_on.Checked;

                listView_combat.Items[last_combat] = ObjListItem;
            }
            catch
            {
                //failed... oh well
            }
        }

        private void button_box_generate_Click(object sender, EventArgs e)
        {
            double radius = Util.GetDouble(textBox_box_radius.Text);
            int sides = Util.GetInt32(textBox_box_sides.Text);
            double offset = Util.GetDouble(textBox_box_offset.Text);

            if (sides < 3)
            {
                return;
            }

            listView_border.Items.Clear();

            offset = offset - 180 / sides + 90;

            for (int i = 0; i < sides; i++)
            {
                double degrees = (360 / sides * i + offset) * Math.PI / 180;
                int x = (int)(radius * Math.Cos(degrees)) + Util.Float_Int32(Globals.gamedata.my_char.X);
                int y = (int)(radius * Math.Sin(degrees)) + Util.Float_Int32(Globals.gamedata.my_char.Y);

                ListViewItem ObjListItem = listView_border.Items.Add(x.ToString());
                ObjListItem.SubItems.Add(y.ToString());
            }
        }

        private void removeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView_border.Items.Clear();
        }

        private void removeToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //need to remove the highlighted buff/heals
            if (listView_buffheal.SelectedIndices.Count > 0)
            {
                listView_buffheal.Items.RemoveAt(listView_buffheal.SelectedIndices[0]);
            }
        }

        private void moveUpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView_buffheal.SelectedIndices.Count > 0)
            {
                int ind = listView_buffheal.SelectedIndices[0];
                if (ind > 0)
                {
                    ListViewItem tmp = (ListViewItem)listView_buffheal.Items[ind].Clone();
                    listView_buffheal.Items[ind] = (ListViewItem)listView_buffheal.Items[ind - 1].Clone();
                    listView_buffheal.Items[ind - 1] = tmp;
                }
            }
        }

        private void removeToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            //need to remove the highlighted item
            if (listView_item.SelectedIndices.Count > 0)
            {
                listView_item.Items.RemoveAt(listView_item.SelectedIndices[0]);
            }
        }

        private void moveUpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (listView_item.SelectedIndices.Count > 0)
            {
                int ind = listView_item.SelectedIndices[0];
                if (ind > 0)
                {
                    ListViewItem tmp = (ListViewItem)listView_item.Items[ind].Clone();
                    listView_item.Items[ind] = (ListViewItem)listView_item.Items[ind - 1].Clone();
                    listView_item.Items[ind - 1] = tmp;
                }
            }
        }

        private void removeToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            //remove combat
            if (listView_combat.SelectedIndices.Count > 0)
            {
                listView_combat.Items.RemoveAt(listView_combat.SelectedIndices[0]);
            }
        }

        private void moveUpToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (listView_combat.SelectedIndices.Count > 0)
            {
                int ind = listView_combat.SelectedIndices[0];
                if (ind > 0)
                {
                    ListViewItem tmp = (ListViewItem)listView_combat.Items[ind].Clone();
                    listView_combat.Items[ind] = (ListViewItem)listView_combat.Items[ind - 1].Clone();
                    listView_combat.Items[ind - 1] = tmp;
                }
            }
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button_autoss_activate_Click(object sender, EventArgs e)
        {
            if (combobox_autoss.SelectedItem == null)
            {
                return;
            }

            ByteBuffer buff = new ByteBuffer(11);
            uint id = Util.GetItemID(combobox_autoss.SelectedItem.ToString());

            buff.WriteByte((byte)PClient.EXPacket);
            buff.WriteByte((byte)PClientEX.RequestAutoSoulShot);
            buff.WriteByte(0x00);
            buff.WriteUInt32(id);
            buff.WriteUInt32(1);

            Globals.gamedata.SendToGameServer(buff);

        }

        private void button_autoss_deactivate_Click(object sender, EventArgs e)
        {
            if (combobox_autoss.SelectedItem == null)
            {
                return;
            }

            ByteBuffer buff = new ByteBuffer(11);
            uint id = Util.GetItemID(combobox_autoss.SelectedItem.ToString());

            buff.WriteByte((byte)PClient.EXPacket);
            buff.WriteByte((byte)PClientEX.RequestAutoSoulShot);
            buff.WriteByte(0x00);
            buff.WriteUInt32(id);
            buff.WriteUInt32(0);

            Globals.gamedata.SendToGameServer(buff);

        }

        private void removeAllToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Clear donot NPCs
            listView_donot_npcs.Items.Clear();
        }

        private void removeAllToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //Clear donot items
            listView_donot_items.Items.Clear();
        }

        private void checkBox_active_move_first_normal_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_active_move_first_normal.Checked)
            {
                checkBox_active_move_first.Checked = false;
                checkBox_movebeforetargeting.Checked = false;
            }
        }

        private void checkBox_active_move_first_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_active_move_first.Checked)
                checkBox_active_move_first_normal.Checked = false;
        }

        private void button_Custom_WindowTitle_Set_Click(object sender, EventArgs e)
        {
            this.Text = textBox_Custom_WindowTitle.Text;
            Globals.gamedata.botoptions.CustomWindowTitle = true;
            Globals.gamedata.botoptions.CustomWindowTitle_Text = textBox_Custom_WindowTitle.Text;
            Globals.l2net_home.SetName(textBox_Custom_WindowTitle.Text);
        }

        private void button_Custom_WindowTitle_Reset_Click(object sender, EventArgs e)
        {
            Globals.gamedata.botoptions.CustomWindowTitle = false;
            Globals.l2net_home.SetName();
            this.Text = "Bot Options";
        }

        private void checkBox_active_target_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_active_target.Checked && checkBox_active_attack.Checked)
            {
                checkBox_movebeforetargeting.Enabled = true;
            }
            else
            {
                checkBox_movebeforetargeting.Enabled = false;
                checkBox_movebeforetargeting.Checked = false;
            }
        }

        private void checkBox_movebeforetargeting_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_movebeforetargeting.Checked)
                checkBox_active_move_first_normal.Checked = false;
        }

        private void checkBox_active_attack_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_active_target.Checked && checkBox_active_attack.Checked)
            {
                checkBox_movebeforetargeting.Enabled = true;
            }
            else
            {
                checkBox_movebeforetargeting.Enabled = false;
                checkBox_movebeforetargeting.Checked = false;
            }
        }

        private void textBox_DeadLogOutDelay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+")) && (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\b")))
                e.Handled = true;
        }

        private void textBox_DeadReturnDelay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+")) && (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\b")))
                e.Handled = true;
        }

        private void tabControl_botpages_DrawItem(object sender, DrawItemEventArgs e)
        {
            /*
    Dim g As Graphics
    Dim sText As String
    Dim iX As Integer
    Dim iY As Integer
    Dim sizeText As SizeF
    Dim ctlTab As TabControl


    Dim r As New RectangleF(e.Bounds.X, e.Bounds.Y + 2, e.Bounds.Width, e.Bounds.Height - 2)

    ctlTab = CType(sender, TabControl)

    g = e.Graphics
    sText = ctlTab.TabPages(e.Index).Text
    sizeText = g.MeasureString(sText, ctlTab.Font)
    iX = e.Bounds.Left + 6
    iY = e.Bounds.Top + (e.Bounds.Height - sizeText.Height) / 2

    If tabMain.SelectedIndex = e.Index Then 'Selected
        g.FillRectangle(New SolidBrush(Color.Red), e.Bounds)
    Else
        g.FillRectangle(New SolidBrush(Color.LightBlue), e.Bounds)
    End If

    g.DrawString(sText, ctlTab.Font, Brushes.Black, iX, iY)*/
            Graphics g = default(Graphics);
            string sText = null;
            int iX = 0;
            int iY = 0;
            SizeF sizeText = default(SizeF);
            TabControl ctlTab = default(TabControl);


            RectangleF r = new RectangleF(e.Bounds.X, e.Bounds.Y + 2, e.Bounds.Width, e.Bounds.Height - 2);

            ctlTab = (TabControl)sender;

            g = e.Graphics;
            sText = tabControl_botpages.TabPages[e.Index].Text;
            sizeText = g.MeasureString(sText, ctlTab.Font);
            iX = e.Bounds.Left + 6;
            //iY = e.Bounds.Top + (e.Bounds.Height - sizeText.Height) / 2;
            iY = System.Convert.ToInt32(e.Bounds.Top + (e.Bounds.Height - sizeText.Height) / 2);

            //Selected
            if (tabControl_botpages.SelectedIndex == e.Index)
            {
                g.FillRectangle(new SolidBrush(Color.White), e.Bounds);
            }
            else
            {
                g.FillRectangle(new SolidBrush(Color.LightGray), e.Bounds);
            }

            g.DrawString(sText, ctlTab.Font, Brushes.Black, iX, iY);
        }

        private void textBox_spoil_mp_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+")) && (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\b")))
                e.Handled = true;
        }

        private void checkBox_activefollow_attack_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_activefollow_attack.Checked && checkBox_activefollow_target.Checked)
                checkBox_activefollow_attack_Instant.Enabled = true;
            else
            {
                checkBox_activefollow_attack_Instant.Enabled = false;
                checkBox_activefollow_attack_Instant.Checked = false;
            }
        }

        private void checkBox_activefollow_target_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_activefollow_attack.Checked && checkBox_activefollow_target.Checked)
                checkBox_activefollow_attack_Instant.Enabled = true;
            else
            {
                checkBox_activefollow_attack_Instant.Enabled = false;
                checkBox_activefollow_attack_Instant.Checked = false;
            }
        }

        private void checkBox_autospoil_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_autospoil.Checked)
            {
                checkBox_cancel_target.Checked = false;
                checkBox_cancel_target.Enabled = false;
                checkBox_autosweep.Enabled = true;
            }
            if (!checkBox_autospoil.Checked)
            {
                checkBox_autosweep.Enabled = false;
                checkBox_autosweep.Checked = false;
                checkBox_cancel_target.Enabled = true;
                checkBox_cancel_target.Checked = false;
            }
        }

        private void Set_CurrentXYZ_Click(object sender, EventArgs e)
        {
            textBox_Moveto_X.Text = Util.Float_Int32(Globals.gamedata.my_char.X).ToString();
            textBox_Moveto_Y.Text = Util.Float_Int32(Globals.gamedata.my_char.Y).ToString();
            textBox_Moveto_Z.Text = Util.Float_Int32(Globals.gamedata.my_char.Z).ToString();
        }

        private void button_add_toggle_Click(object sender, EventArgs e)
        {
            button_update_toggle.Enabled = false;

            try
            {
                uint id = (uint)skill_ids[comboBox_skills_toggle.SelectedIndex]; 

                ListViewItem ObjListItem = listView_toggles.Items.Add(comboBox_skills_toggle.Text);//name
                ObjListItem.SubItems.Add(comboBox_trait_toggle.Text);
                ObjListItem.SubItems.Add(textBox_greaterthen_toggle.Text);
                ObjListItem.SubItems.Add(textBox_lesserthen_toggle.Text);
                ObjListItem.SubItems.Add(comboBox_trait_toggle.SelectedIndex.ToString());
                ObjListItem.SubItems.Add(id.ToString());

                ObjListItem.Checked = checkBox_onoff_toggle.Checked;
            }
            catch
            {
                //failed... oh well
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //need to remove the highlighted buff/heals
            if (listView_toggles.SelectedIndices.Count > 0)
            {
                listView_toggles.Items.RemoveAt(listView_toggles.SelectedIndices[0]);
            }
        }






    }//end of class
}//end of namespace
