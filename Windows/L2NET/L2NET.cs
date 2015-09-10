//#define TESTING

using System;
using System.Collections;
using System.Windows.Forms;
using System.Data;
using System.Resources;
using System.Globalization;
using System.Diagnostics;

namespace L2_login
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public partial class L2NET : System.Windows.Forms.Form
	{
		private System.ComponentModel.IContainer components;

        public ListViewColumnSorter lvwColumnSorter_inventory;
        public ListViewColumnSorter lvwColumnSorter_npc_data;
        public ListViewColumnSorter lvwColumnSorter_item_data;
        public ListViewColumnSorter lvwColumnSorter_players_data;
        public ListViewColumnSorter lvwColumnSorter_mybuffs_data;

        public ListViewColumnSorter lvwColumnSorter_skills;
        public ListViewColumnSorter lvwColumnSorter_clan;

        public ListView listView_inventory;
        public ListView listView_npc_data;
        public ListView listView_items_data;
        public ListView listView_players_data;
        public ListView listView_mybuffs_data;

        public ListView listView_skills;
        public ListView listView_char_clan;
        public ListView listView_char_data;
        public ListView listView_stats_data;

        public ArrayList listView_inventory_items;
        public ArrayList listView_npc_data_items;
        public ArrayList listView_items_data_items;
        public ArrayList listView_players_data_items;
        public ArrayList listView_mybuffs_data_items;

        public SmartTimer timer_chat;
        public SmartTimer timer_players;
        public SmartTimer timer_items;
        public SmartTimer timer_npcs;
        public SmartTimer timer_inventory;
        public SmartTimer timer_mybuffs;
        
        private System.Collections.SortedList ScriptWindows = new SortedList();

        private System.Collections.Queue Chat_Messages = new Queue();

		#region form items

        public RichTextBoxEx richTextBox_dialog;

        public System.Collections.SortedList imageList_skills_loaded = new SortedList();
        public System.Windows.Forms.ImageList imageList_skills;
        public System.Windows.Forms.ImageList imageList_crests;
        public System.Collections.SortedList imageList_items_loaded = new SortedList();
        public System.Windows.Forms.ImageList imageList_items;

        private System.Windows.Forms.ToolStripMenuItem menuItem_actions;
        private System.Windows.Forms.PictureBox pictureBox_clan_crest;
        private System.Windows.Forms.Panel panel_charinfo;
        private System.Windows.Forms.Panel panel_char;
        private System.Windows.Forms.Panel panel_chat;
        private System.Windows.Forms.TextBox textBox_say;
        private System.Windows.Forms.Button button_sendtext;
        private System.Windows.Forms.Label label_char_name;
        private System.Windows.Forms.Label label_char_hp;
        private System.Windows.Forms.Label label_char_mp;
        private System.Windows.Forms.Label label_char_cp;
        private System.Windows.Forms.TabControl tabControl_char;
        private System.Windows.Forms.TabPage tabPage_char_info;
        private System.Windows.Forms.TabPage tabPage_char_inv;
        private System.Windows.Forms.Label label_info_cp;
        private System.Windows.Forms.Label label_info_mp;
        private System.Windows.Forms.Label label_info_hp;
        private System.Windows.Forms.Label label_info_title;
        private System.Windows.Forms.Label label_info_name;
        private System.Windows.Forms.Label label_info_xp;
        private System.Windows.Forms.Label label_info_sp;
        private System.Windows.Forms.Label label_info_level;
        private System.Windows.Forms.Label label_info_str;
        private System.Windows.Forms.Label label_info_dex;
        private System.Windows.Forms.Label label_info_con;
        private System.Windows.Forms.Label label_info_int;
        private System.Windows.Forms.Label label_info_wit;
        private System.Windows.Forms.Label label_info_men;
        private System.Windows.Forms.ComboBox comboBox_msg_type;
        private System.Windows.Forms.Button button_yesno_yes;
        private System.Windows.Forms.Button button_yesno_no;
        private System.Windows.Forms.Label label_yesno;
        private System.Windows.Forms.Panel panel_yesno;
        private System.Windows.Forms.Label label_info_karma;
        private System.Windows.Forms.Label label_info_load;
        private System.Windows.Forms.Label label_info_patk;
        private System.Windows.Forms.Label label_info_pdef;
        private System.Windows.Forms.Label label_info_acc;
        private System.Windows.Forms.Label label_info_crit;
        private System.Windows.Forms.Label label_info_atkspd;
        private System.Windows.Forms.Label label_info_matkspd;
        private System.Windows.Forms.Label label_info_spd;
        private System.Windows.Forms.Label label_info_eva;
        private System.Windows.Forms.Label label_info_mdef;
        private System.Windows.Forms.Label label_info_matk;
        private System.Windows.Forms.Label label_info_pvp;
        private System.Windows.Forms.ColumnHeader columnHeader169;
        private System.Windows.Forms.ColumnHeader columnHeader173;
        private System.Windows.Forms.ColumnHeader columnHeader170;
        private System.Windows.Forms.ColumnHeader columnHeader171;
        private System.Windows.Forms.ColumnHeader columnHeader172;
        private System.Windows.Forms.Panel panel_inven_head;
        private System.Windows.Forms.Panel panel_inven_rhand;
        private System.Windows.Forms.Panel panel_inven_top;
        private System.Windows.Forms.Panel panel_inven_pants;
        private System.Windows.Forms.Panel panel_inven_lhand;
        private System.Windows.Forms.Panel panel_inven_gloves;
        private System.Windows.Forms.Panel panel_inven_boots;
        private System.Windows.Forms.Panel panel_inven_acc;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel_inven_neck;
        private System.Windows.Forms.Panel panel_inven_lear;
        private System.Windows.Forms.Panel panel_inven_rear;
        private System.Windows.Forms.Panel panel_inven_rfinger;
        private System.Windows.Forms.Panel panel_inven_lfinger;
        private System.Windows.Forms.Panel panel_tat1;
        private System.Windows.Forms.Panel panel_tat2;
        private System.Windows.Forms.Panel panel_tat3;
        private System.Windows.Forms.TabPage tabPage_char_skills;
        private System.Windows.Forms.ColumnHeader columnHeader180;
        private System.Windows.Forms.ColumnHeader columnHeader178;
        private System.Windows.Forms.ColumnHeader columnHeader177;
        private System.Windows.Forms.Panel panel_party_1;
        private System.Windows.Forms.Label label_1_name;
        private System.Windows.Forms.Label label_1_cp;
        private System.Windows.Forms.Label label_1_hp;
        private System.Windows.Forms.Panel panel_party_2;
        private System.Windows.Forms.Label label_2_cp;
        private System.Windows.Forms.Label label_2_mp;
        private System.Windows.Forms.Label label_2_hp;
        private System.Windows.Forms.Label label_2_name;
        private System.Windows.Forms.Panel panel_party_3;
        private System.Windows.Forms.Label label_3_cp;
        private System.Windows.Forms.Label label_3_mp;
        private System.Windows.Forms.Label label_3_hp;
        private System.Windows.Forms.Label label_3_name;
        private System.Windows.Forms.Panel panel_party_4;
        private System.Windows.Forms.Label label_4_cp;
        private System.Windows.Forms.Label label_4_mp;
        private System.Windows.Forms.Label label_4_hp;
        private System.Windows.Forms.Label label_4_name;
        private System.Windows.Forms.Panel panel_party_8;
        private System.Windows.Forms.Label label_8_cp;
        private System.Windows.Forms.Label label_8_mp;
        private System.Windows.Forms.Label label_8_hp;
        private System.Windows.Forms.Label label_8_name;
        private System.Windows.Forms.Panel panel_party_7;
        private System.Windows.Forms.Label label_7_cp;
        private System.Windows.Forms.Label label_7_mp;
        private System.Windows.Forms.Label label_7_hp;
        private System.Windows.Forms.Label label_7_name;
        private System.Windows.Forms.Panel panel_party_6;
        private System.Windows.Forms.Label label_6_cp;
        private System.Windows.Forms.Label label_6_mp;
        private System.Windows.Forms.Label label_6_hp;
        private System.Windows.Forms.Label label_6_name;
        private System.Windows.Forms.Panel panel_party_5;
        private System.Windows.Forms.Label label_5_cp;
        private System.Windows.Forms.Label label_5_mp;
        private System.Windows.Forms.Label label_5_hp;
        private System.Windows.Forms.Label label_5_name;
        private System.Windows.Forms.Label label_char_level;
        private System.Windows.Forms.Panel panel_dead;
        private System.Windows.Forms.Button button_dead_town;
        private System.Windows.Forms.Button button_dead_castle;
        private System.Windows.Forms.Button button_dead_siege;
        private System.Windows.Forms.Button button_dead_clanhall;
        private System.Windows.Forms.TabPage tabPage_char_clan;
        private System.Windows.Forms.ColumnHeader columnHeader191;
        private System.Windows.Forms.ColumnHeader columnHeader192;
        private System.Windows.Forms.ColumnHeader columnHeader193;
        private System.Windows.Forms.ColumnHeader columnHeader194;
        private System.Windows.Forms.Label label_clan_online;
        private System.Windows.Forms.Label label_clan_leader;
        private System.Windows.Forms.Label label_clan_name;
        private System.Windows.Forms.Label label_clan_level;
        private System.Windows.Forms.Label label_clan_war;
        private System.Windows.Forms.Label label_clan_hall;
        private System.Windows.Forms.Label label_clan_castle;
        private System.Windows.Forms.Label label_caln_ally;
        private System.Windows.Forms.Panel panel_npc_chat;
        private System.Windows.Forms.Button button_npc_close;
        private System.Windows.Forms.Label label_1_mp;
        private System.Windows.Forms.Panel panel_charinfo_ul;
        private System.Windows.Forms.Label label_target_name;
        private System.Windows.Forms.Label label_target_hp;
        private System.Windows.Forms.Label label_target_cp;
        private System.Windows.Forms.Label label_target_mp;
        private System.Windows.Forms.Label label_youdied;
        private System.Windows.Forms.Label label_clan_rep;
        private System.Windows.Forms.Label label_char_xp;
        private System.Windows.Forms.NotifyIcon notifyIcon_us;
        private System.Windows.Forms.Panel panel_target;
        private System.Windows.Forms.Panel panel_party_cover;
        private OpenFileDialog openFileDialog1;
        private ContextMenuStrip contextMenuStrip_inventory;
        private ToolStripMenuItem dropStackToolStripMenuItem;
        private ToolStripMenuItem deleteStackToolStripMenuItem;
        private ToolStripMenuItem crystalizeToolStripMenuItem;
        private ContextMenuStrip contextMenuStrip_notify;
        private ToolStripMenuItem closeToolStripMenuItem;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem menuItem_File;
        private ToolStripMenuItem menuItem_Commands;
        private ToolStripMenuItem menuItem_Options;
        private ToolStripMenuItem menuItem_scripting;
        private ToolStripMenuItem menuItem_Help;
        private ToolStripMenuItem menuItem_cmd_logon;
        private ToolStripMenuItem menuItem_cmd_game;
        private ToolStripSeparator toolStripSeparator1;
        public ToolStripMenuItem menuItem_cmd_overlay;
        public ToolStripMenuItem menuItem_cmd_shortcut;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem menuItem_launchl2;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem menuItem_exit;
        private ToolStripMenuItem menuItem_cmd_cancel;
        private ToolStripMenuItem menuItem_toggle_botting;
        private ToolStripSeparator toolStripSeparator5;
        private ToolStripMenuItem menuItem_cmd_restart;
        private ToolStripMenuItem menuItem_cmd_logout;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripSeparator toolStripSeparator6;
        private ToolStripMenuItem menuItem_help_donate;
        private ToolStripMenuItem menuItem_about;
        private ToolStripMenuItem menuItem_hosts;
        private ToolStripMenuItem menuItem_language;
        private ToolStripSeparator toolStripSeparator7;
        private ToolStripMenuItem menuItem_forcecollect;
        private ToolStripMenuItem menuItem_loadscript;
        private ToolStripMenuItem menuItem_startscript;
        private ToolStripMenuItem menuItem_scriptwindow;
        private ToolStripMenuItem menuItem_scriptdebugger;
        private ToolStripSeparator toolStripSeparator9;
        private ToolStripMenuItem menuItem_debug_mode;
        private ToolStripMenuItem menuItem_dump_mode;
        private ToolStripSeparator toolStripSeparator10;
        private ToolStripMenuItem menuItem_killthreads;
        private Panel panel_inven_shirt;
        private ToolStripMenuItem forceLogToolStripMenuItem;
        private Button button_dead_fort;
        private Button button_close_accept;
        private Button button1_close_dead;
        private ToolStripMenuItem menuItem_forums;
        private ToolStripSeparator toolStripSeparator11;
        private ToolStripMenuItem eULAToolStripMenuItem;
        private ToolStripMenuItem menuItem_options_setup;
        private ToolStripMenuItem menuItem_saveinterface;
        private ToolStripSeparator toolStripSeparator12;
        private ToolStripMenuItem toggleBottingToolStripMenuItem;
        private ToolStripMenuItem botOptionsToolStripMenuItem;
        private Label label_zrange_map;
        public TrackBar trackBar_map_zoom;
        public TextBox textBox_zrange_map;
        private CheckBox checkBox_op_control;
        private CheckBox checkBox_op_shift;
        public CheckBox checkBox_minimap;
        private ColumnHeader columnHeader103;
        private ColumnHeader columnHeader104;
        private ColumnHeader columnHeader80;
        private ColumnHeader columnHeader81;
        private ColumnHeader columnHeader83;
        private ColumnHeader columnHeader84;
        private ColumnHeader columnHeader85;
        private ColumnHeader columnHeader130;
        private ColumnHeader columnHeader132;
        private ColumnHeader columnHeader131;
        private ColumnHeader columnHeader137;
        private ColumnHeader columnHeader135;
        private ColumnHeader columnHeader136;
        private TabPage tabPage_char_detail;
        private TabPage tabPage_players;
        private TabPage tabPage_items;
        private TabPage tabPage_npc;
        private TabPage tabPage_npc_chat;
        private ToolStripMenuItem menuItem_dump_mode_server;
        private ToolStripMenuItem startScriptToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator8;
        private ToolStripMenuItem forceLogToolStripMenuItem1;
        private Label label_inventory_count;
        private ColumnHeader columnHeader82;
        private TabControl tabControl_ChatSelect;
        private TabPage tab_all;
        private TabPage tab_local;
        private TabPage tab_trade;
        private TabPage tab_clan;
        private TabPage tab_alliance;
        private TabPage tab_bot;
        private TabPage tab_system;
        private TabPage tab_party;
        private ColorListBox colorListBox_all;
        private ColorListBox colorListBox_system;
        private ColorListBox colorListBox_bot;
        private ColorListBox colorListBox_local;
        private ColorListBox colorListBox_trade;
        private ColorListBox colorListBox_party;
        private ColorListBox colorListBox_clan;
        private ColorListBox colorListBox_ally;
        private RadioButton radiobutton_passive;
        private RadioButton radiobutton_active;
        private ContextMenuStrip contextMenuStrip_Items;
        private ToolStripMenuItem addToDoNotListToolStripMenuItem;
        private ToolStripMenuItem addTodoNotListToolStripMenuItem1;
        private ContextMenuStrip contextMenuStrip_NPC;
        private ToolStripMenuItem addToDoNotListNPCToolStripMenuItem;
        private TabPage tabPage_buffs;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ToolStripMenuItem addToBlackListNPCToolStripMenuItem;
        private ToolStripMenuItem blacklistTargetToolStripMenuItem;
        private RadioButton radioButton_inv_quest;
        private RadioButton radioButton_inv_equipped;
        private RadioButton radioButton_inv_items;
        private ToolStripMenuItem menuItem_closeclient;
        private ToolStripMenuItem menuItem_encryptscript;
        private TabPage tab_hero;
        private ColorListBox colorListBox_hero;
        private Label label_char_vitality;
        private VistaStyleProgressBar.ProgressBar progressBar_char_HP;
        private VistaStyleProgressBar.ProgressBar progressBar_char_CP;
        private VistaStyleProgressBar.ProgressBar progressBar_char_XP;
        private VistaStyleProgressBar.ProgressBar progressBar_char_MP;
        private Label label_info_darkness_desc;
        private Label label_info_earth_desc;
        private Label label_info_water_desc;
        private Label label_info_divinity_desc;
        private Label label_info_wind_desc;
        private Label label_info_fire_desc;
        private Label label_info_atk_attrib_val_desc;
        private Label label_info_atk_attrib_descr;
        private Label label_info_recommend_desc;
        private Label label_info_fame_desc;
        private Label label_info_atk_attrib;
        private Label label_info_darkness;
        private Label label_info_earth;
        private Label label_info_water;
        private Label label_info_divinity;
        private Label label_info_wind;
        private Label label_info_fire;
        private Label label_info_atk_attrib_value;
        private Label label_info_fame;
        private Label label_info_eval;
        private ToolStripMenuItem showTargetInfoToolStripMenuItem;
        private ToolStripMenuItem petWindowToolStripMenuItem;
        public volatile TextBox textBox_rtb_input;
        private ToolStripMenuItem extendedActionsToolStripMenuItem;
        private ToolStripMenuItem toolStrip_pck;
        private wyDay.Controls.AutomaticUpdater automaticUpdater1;
        private ToolStripMenuItem menuitem_help_checkforupdates;
        private Label label_info_mcritical;
        private Label label_info_mevasion;
        private Label label_info_maccuracy;
        private Label label_info_mcrit_descr;
        private Label label_info_meva_descr;
        private Label label_info_macc_descr;
        private ToolStripMenuItem menuitem_cmd_GG;
        private ToolStripMenuItem menuitem_cmd_ggclient;
        private CheckBox checkBox_BoundingPoints;
        private ToolStripMenuItem menuItem_toggle_autoreply;
        private ToolStripMenuItem menuItem_toggle_autoreplyPM;
        private Label label_clan_castle_text;
        private Label label_clan_hall_text;
        private Label label_clan_war_text;
        private Label label_clan_rep_text;
        private Label label_clan_lvl_text;
        private Label label_clan_ally_text;
        private Label label_clan_name_text;
        private Label label_clan_leader_text;
        private TabPage tabPage_stats;
        private Label label_SP;
        private Label label_XP;
        private Label label_Adena;
        private Label label3;
        private Label label2;
        private Label label1;
        public ListView listView_stats;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader10;
        private Button button_clear_stats;
        private Label label_adena_total;
        private Label label5;
        private ColumnHeader columnHeader8;
        private Label label_meshlessignored;
        private Label label6;
        private Label label_badmobs;
        private Label label7;
        private ToolStripMenuItem disconectClientToolStripMenuItem;
        private SaveFileDialog saveFileDialog1;
		#endregion

        public L2NET(string[] args)
		{
            SplashScreen splash = new SplashScreen();
            //splash.TopMost = true;
            splash.Show();
            splash.Update();

            Process.GetCurrentProcess().PriorityClass = System.Diagnostics.ProcessPriorityClass.AboveNormal;
            //System.Threading.Thread thread = System.Threading.Thread.CurrentThread;
            //thread.Priority = System.Threading.ThreadPriority.AboveNormal;

			InitializeComponent();

            if (!automaticUpdater1.ClosingForInstall)
            {

                Globals.gamedata = new GameData();
                Globals.l2net_home = this;

                //need to setup our chat box shit first
                timer_chat = new SmartTimer();
                timer_chat.Interval = Globals.CHAT_TIMER;
                timer_chat.OnTimerTick += timer_chat_Tick;

                Load_Interface();

                //do our loading here...
                GameServer.Init(args);

                SetName();

                timer_players = new SmartTimer();
                timer_players.Interval = Globals.PLAYERS_TIMER;
                timer_players.OnTimerTick += timer_players_Tick;

                timer_items = new SmartTimer();
                timer_items.Interval = Globals.ITEMS_TIMER;
                timer_items.OnTimerTick += timer_items_Tick;

                timer_npcs = new SmartTimer();
                timer_npcs.Interval = Globals.NPCS_TIMER;
                timer_npcs.OnTimerTick += timer_npcs_Tick;

                timer_inventory = new SmartTimer();
                timer_inventory.Interval = Globals.INVENTORY_TIMER;
                timer_inventory.OnTimerTick += timer_inventory_Tick;

                timer_mybuffs = new SmartTimer();
                timer_mybuffs.Interval = Globals.MYBUFFS_TIMER;
                timer_mybuffs.OnTimerTick += timer_mybuffs_Tick;

                this.SizeChanged += new EventHandler(L2NET_SizeChanged);
                this.GotFocus += new EventHandler(L2NET_GotFocus);
                this.notifyIcon_us.DoubleClick += new EventHandler(notifyIcon_us_DoubleClick);

                listView_inventory_items = new ArrayList();
                lvwColumnSorter_inventory = new ListViewColumnSorter();
                listView_inventory.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listView_inventory_RetrieveVirtualItem);
                listView_inventory.ColumnClick += new ColumnClickEventHandler(listView_inventory_ColumnClick);

                listView_npc_data_items = new ArrayList();
                lvwColumnSorter_npc_data = new ListViewColumnSorter();
                listView_npc_data.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listView_npc_data_RetrieveVirtualItem);
                listView_npc_data.ColumnClick += new ColumnClickEventHandler(listView_npc_data_ColumnClick);

                listView_items_data_items = new ArrayList();
                lvwColumnSorter_item_data = new ListViewColumnSorter();
                listView_items_data.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listView_items_data_RetrieveVirtualItem);
                listView_items_data.ColumnClick += new ColumnClickEventHandler(listView_items_data_ColumnClick);

                listView_players_data_items = new ArrayList();
                lvwColumnSorter_players_data = new ListViewColumnSorter();
                listView_players_data.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listView_players_data_RetrieveVirtualItem);
                listView_players_data.ColumnClick += new ColumnClickEventHandler(listView_players_data_ColumnClick);

                listView_mybuffs_data_items = new ArrayList();
                lvwColumnSorter_mybuffs_data = new ListViewColumnSorter();
                listView_mybuffs_data.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(listView_mybuffs_data_RetrieveVirtualItem);
                listView_mybuffs_data.ColumnClick += new ColumnClickEventHandler(listView_mybuffs_data_ColumnClick);

                lvwColumnSorter_skills = new ListViewColumnSorter();
                listView_skills.ListViewItemSorter = lvwColumnSorter_skills;
                listView_skills.ColumnClick += new ColumnClickEventHandler(listView_skills_ColumnClick);

                lvwColumnSorter_clan = new ListViewColumnSorter();
                listView_char_clan.ListViewItemSorter = lvwColumnSorter_clan;
                listView_char_clan.ColumnClick += new ColumnClickEventHandler(listView_char_clan_ColumnClick);

                listView_inventory.DoubleClick += new EventHandler(listView_inventory_DoubleClick);
                listView_npc_data.DoubleClick += new EventHandler(listView_npc_data_DoubleClick);
                listView_items_data.DoubleClick += new EventHandler(listView_items_data_DoubleClick);
                listView_players_data.DoubleClick += new EventHandler(listView_players_data_DoubleClick);
                listView_skills.DoubleClick += new EventHandler(listView_skills_DoubleClick);

                listView_inventory.SelectedIndexChanged += new EventHandler(listView_inventory_SelectedIndexChanged);
                listView_npc_data.SelectedIndexChanged += new EventHandler(listView_npc_data_SelectedIndexChanged);
                listView_items_data.SelectedIndexChanged += new EventHandler(listView_items_data_SelectedIndexChanged);
                listView_players_data.SelectedIndexChanged += new EventHandler(listView_players_data_SelectedIndexChanged);
                listView_skills.SelectedIndexChanged += new EventHandler(listView_skills_SelectedIndexChanged);

                label_char_name.DoubleClick += new EventHandler(panel_charinfo_DoubleClick);
                panel_charinfo.DoubleClick += new EventHandler(panel_charinfo_DoubleClick);

                checkBox_op_control.CheckedChanged += new EventHandler(checkBox_op_control_CheckedChanged);
                checkBox_op_shift.CheckedChanged += new EventHandler(checkBox_op_shift_CheckedChanged);

                panel_yesno.Hide();
                panel_dead.Hide();
                panel_npc_chat.Hide();

                tabControl_char.SelectedIndexChanged += new EventHandler(tabControl_char_SelectedIndexChanged);
                tabControl_char.HandleCreated += new EventHandler(TabControl_HandleCreated);

                comboBox_msg_type.SelectedIndex = 0;

                System.Drawing.Bitmap img;
                try
                {
                    img = new System.Drawing.Bitmap(Globals.PATH + "\\Crests\\0.bmp");
                }
                catch
                {
                    Add_Error("failed to load Crests\\0.bmp, generating substitute", false);
                    img = new System.Drawing.Bitmap(16, 8);
                }

                imageList_crests.Images.Add(img);
                Globals.crestids.Add((uint)0);

                this.Closing += new System.ComponentModel.CancelEventHandler(L2NET_Closing);

                richTextBox_dialog.LinkClicked += new LinkClickedEventHandler(richTextBox_dialog_LinkClicked);

                Globals.CanPrint = true;
                menuItem_cmd_logon_Click(null, null);

#if TESTING && DEBUG
            //TESTING MAP ENGINE OFFLINE
            Globals.gamedata.running = true;
            Globals.gamedata.drawing_game = true;
            Globals.gamedrawthread = new System.Threading.Thread(new System.Threading.ThreadStart(MapThread.DrawGameThread));
            Globals.gamedrawthread.IsBackground = true;
            Globals.gamedrawthread.Start();
            //END OF TESTING MAP ENGINE OFFLINE
#endif

                //Load pre bot options
                if (!String.IsNullOrEmpty(Globals.BotOptionsFile))
                {
                    if (Globals.botoptionsscreen == null || Globals.botoptionsscreen.IsDisposed == true)
                    {
                        Globals.botoptionsscreen = new BotOptionsScreen();
                    }
                    else
                    {
                        Globals.botoptionsscreen.Setup();
                    }
                    //Globals.botoptionsscreen.TopMost = true;
                    //Globals.botoptionsscreen.BringToFront();
                    //Globals.botoptionsscreen.Show();
                }
                //menuItem_Options_Click(null, null);
                //Globals.botoptionsscreen.Hide();

                splash.Close();
                splash.Dispose();
                splash = null;

            }
        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
            try
            {
                base.Dispose(disposing);
            }
            catch
            {
                //something was trying to allocate something while we were closing... oh well
            }
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(L2NET));
            this.panel_party_5 = new System.Windows.Forms.Panel();
            this.label_5_cp = new System.Windows.Forms.Label();
            this.label_5_mp = new System.Windows.Forms.Label();
            this.label_5_hp = new System.Windows.Forms.Label();
            this.label_5_name = new System.Windows.Forms.Label();
            this.panel_party_6 = new System.Windows.Forms.Panel();
            this.label_6_cp = new System.Windows.Forms.Label();
            this.label_6_mp = new System.Windows.Forms.Label();
            this.label_6_hp = new System.Windows.Forms.Label();
            this.label_6_name = new System.Windows.Forms.Label();
            this.panel_party_7 = new System.Windows.Forms.Panel();
            this.label_7_cp = new System.Windows.Forms.Label();
            this.label_7_mp = new System.Windows.Forms.Label();
            this.label_7_hp = new System.Windows.Forms.Label();
            this.label_7_name = new System.Windows.Forms.Label();
            this.panel_party_8 = new System.Windows.Forms.Panel();
            this.label_8_cp = new System.Windows.Forms.Label();
            this.label_8_mp = new System.Windows.Forms.Label();
            this.label_8_hp = new System.Windows.Forms.Label();
            this.label_8_name = new System.Windows.Forms.Label();
            this.panel_party_4 = new System.Windows.Forms.Panel();
            this.label_4_cp = new System.Windows.Forms.Label();
            this.label_4_mp = new System.Windows.Forms.Label();
            this.label_4_hp = new System.Windows.Forms.Label();
            this.label_4_name = new System.Windows.Forms.Label();
            this.panel_party_3 = new System.Windows.Forms.Panel();
            this.label_3_cp = new System.Windows.Forms.Label();
            this.label_3_mp = new System.Windows.Forms.Label();
            this.label_3_hp = new System.Windows.Forms.Label();
            this.label_3_name = new System.Windows.Forms.Label();
            this.panel_party_2 = new System.Windows.Forms.Panel();
            this.label_2_cp = new System.Windows.Forms.Label();
            this.label_2_mp = new System.Windows.Forms.Label();
            this.label_2_hp = new System.Windows.Forms.Label();
            this.label_2_name = new System.Windows.Forms.Label();
            this.panel_party_1 = new System.Windows.Forms.Panel();
            this.label_1_cp = new System.Windows.Forms.Label();
            this.label_1_mp = new System.Windows.Forms.Label();
            this.label_1_hp = new System.Windows.Forms.Label();
            this.label_1_name = new System.Windows.Forms.Label();
            this.panel_charinfo = new System.Windows.Forms.Panel();
            this.panel_target = new System.Windows.Forms.Panel();
            this.label_target_cp = new System.Windows.Forms.Label();
            this.label_target_mp = new System.Windows.Forms.Label();
            this.label_target_hp = new System.Windows.Forms.Label();
            this.label_target_name = new System.Windows.Forms.Label();
            this.panel_charinfo_ul = new System.Windows.Forms.Panel();
            this.progressBar_char_XP = new VistaStyleProgressBar.ProgressBar();
            this.progressBar_char_MP = new VistaStyleProgressBar.ProgressBar();
            this.progressBar_char_CP = new VistaStyleProgressBar.ProgressBar();
            this.label_char_vitality = new System.Windows.Forms.Label();
            this.label_char_xp = new System.Windows.Forms.Label();
            this.label_char_level = new System.Windows.Forms.Label();
            this.label_char_cp = new System.Windows.Forms.Label();
            this.label_char_mp = new System.Windows.Forms.Label();
            this.label_char_name = new System.Windows.Forms.Label();
            this.progressBar_char_HP = new VistaStyleProgressBar.ProgressBar();
            this.label_char_hp = new System.Windows.Forms.Label();
            this.panel_party_cover = new System.Windows.Forms.Panel();
            this.panel_char = new System.Windows.Forms.Panel();
            this.tabControl_char = new System.Windows.Forms.TabControl();
            this.tabPage_char_info = new System.Windows.Forms.TabPage();
            this.label_info_mcritical = new System.Windows.Forms.Label();
            this.label_info_mevasion = new System.Windows.Forms.Label();
            this.label_info_maccuracy = new System.Windows.Forms.Label();
            this.label_info_mcrit_descr = new System.Windows.Forms.Label();
            this.label_info_meva_descr = new System.Windows.Forms.Label();
            this.label_info_macc_descr = new System.Windows.Forms.Label();
            this.label_info_eval = new System.Windows.Forms.Label();
            this.label_info_fame = new System.Windows.Forms.Label();
            this.label_info_darkness = new System.Windows.Forms.Label();
            this.label_info_earth = new System.Windows.Forms.Label();
            this.label_info_water = new System.Windows.Forms.Label();
            this.label_info_divinity = new System.Windows.Forms.Label();
            this.label_info_wind = new System.Windows.Forms.Label();
            this.label_info_fire = new System.Windows.Forms.Label();
            this.label_info_atk_attrib_value = new System.Windows.Forms.Label();
            this.label_info_atk_attrib = new System.Windows.Forms.Label();
            this.label_info_recommend_desc = new System.Windows.Forms.Label();
            this.label_info_fame_desc = new System.Windows.Forms.Label();
            this.label_info_darkness_desc = new System.Windows.Forms.Label();
            this.label_info_earth_desc = new System.Windows.Forms.Label();
            this.label_info_water_desc = new System.Windows.Forms.Label();
            this.label_info_divinity_desc = new System.Windows.Forms.Label();
            this.label_info_wind_desc = new System.Windows.Forms.Label();
            this.label_info_fire_desc = new System.Windows.Forms.Label();
            this.label_info_atk_attrib_val_desc = new System.Windows.Forms.Label();
            this.label_info_atk_attrib_descr = new System.Windows.Forms.Label();
            this.label_info_pvp = new System.Windows.Forms.Label();
            this.label_info_matkspd = new System.Windows.Forms.Label();
            this.label_info_spd = new System.Windows.Forms.Label();
            this.label_info_eva = new System.Windows.Forms.Label();
            this.label_info_mdef = new System.Windows.Forms.Label();
            this.panel_dead = new System.Windows.Forms.Panel();
            this.button1_close_dead = new System.Windows.Forms.Button();
            this.button_dead_fort = new System.Windows.Forms.Button();
            this.label_youdied = new System.Windows.Forms.Label();
            this.button_dead_siege = new System.Windows.Forms.Button();
            this.button_dead_clanhall = new System.Windows.Forms.Button();
            this.button_dead_castle = new System.Windows.Forms.Button();
            this.button_dead_town = new System.Windows.Forms.Button();
            this.panel_yesno = new System.Windows.Forms.Panel();
            this.button_close_accept = new System.Windows.Forms.Button();
            this.label_yesno = new System.Windows.Forms.Label();
            this.button_yesno_no = new System.Windows.Forms.Button();
            this.button_yesno_yes = new System.Windows.Forms.Button();
            this.label_info_matk = new System.Windows.Forms.Label();
            this.label_info_atkspd = new System.Windows.Forms.Label();
            this.label_info_crit = new System.Windows.Forms.Label();
            this.label_info_acc = new System.Windows.Forms.Label();
            this.label_info_pdef = new System.Windows.Forms.Label();
            this.label_info_patk = new System.Windows.Forms.Label();
            this.label_info_load = new System.Windows.Forms.Label();
            this.label_info_karma = new System.Windows.Forms.Label();
            this.label_info_men = new System.Windows.Forms.Label();
            this.label_info_wit = new System.Windows.Forms.Label();
            this.label_info_int = new System.Windows.Forms.Label();
            this.label_info_con = new System.Windows.Forms.Label();
            this.label_info_dex = new System.Windows.Forms.Label();
            this.label_info_str = new System.Windows.Forms.Label();
            this.label_info_level = new System.Windows.Forms.Label();
            this.label_info_sp = new System.Windows.Forms.Label();
            this.label_info_xp = new System.Windows.Forms.Label();
            this.label_info_cp = new System.Windows.Forms.Label();
            this.label_info_mp = new System.Windows.Forms.Label();
            this.label_info_hp = new System.Windows.Forms.Label();
            this.label_info_title = new System.Windows.Forms.Label();
            this.label_info_name = new System.Windows.Forms.Label();
            this.tabPage_char_inv = new System.Windows.Forms.TabPage();
            this.radioButton_inv_quest = new System.Windows.Forms.RadioButton();
            this.radioButton_inv_equipped = new System.Windows.Forms.RadioButton();
            this.radioButton_inv_items = new System.Windows.Forms.RadioButton();
            this.label_inventory_count = new System.Windows.Forms.Label();
            this.panel_inven_shirt = new System.Windows.Forms.Panel();
            this.panel_tat3 = new System.Windows.Forms.Panel();
            this.panel_tat2 = new System.Windows.Forms.Panel();
            this.panel_tat1 = new System.Windows.Forms.Panel();
            this.panel_inven_rfinger = new System.Windows.Forms.Panel();
            this.panel_inven_lfinger = new System.Windows.Forms.Panel();
            this.panel_inven_rear = new System.Windows.Forms.Panel();
            this.panel_inven_lear = new System.Windows.Forms.Panel();
            this.panel_inven_neck = new System.Windows.Forms.Panel();
            this.panel_inven_acc = new System.Windows.Forms.Panel();
            this.panel_inven_boots = new System.Windows.Forms.Panel();
            this.panel_inven_gloves = new System.Windows.Forms.Panel();
            this.panel_inven_lhand = new System.Windows.Forms.Panel();
            this.panel_inven_pants = new System.Windows.Forms.Panel();
            this.panel_inven_top = new System.Windows.Forms.Panel();
            this.panel_inven_rhand = new System.Windows.Forms.Panel();
            this.panel_inven_head = new System.Windows.Forms.Panel();
            this.listView_inventory = new System.Windows.Forms.ListView();
            this.columnHeader169 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader170 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader171 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader172 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader173 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip_inventory = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.dropStackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteStackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.crystalizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addTodoNotListToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList_items = new System.Windows.Forms.ImageList(this.components);
            this.tabPage_char_skills = new System.Windows.Forms.TabPage();
            this.radiobutton_passive = new System.Windows.Forms.RadioButton();
            this.radiobutton_active = new System.Windows.Forms.RadioButton();
            this.listView_skills = new System.Windows.Forms.ListView();
            this.columnHeader177 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader178 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader180 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList_skills = new System.Windows.Forms.ImageList(this.components);
            this.tabPage_char_clan = new System.Windows.Forms.TabPage();
            this.label_clan_castle_text = new System.Windows.Forms.Label();
            this.label_clan_hall_text = new System.Windows.Forms.Label();
            this.label_clan_war_text = new System.Windows.Forms.Label();
            this.label_clan_rep_text = new System.Windows.Forms.Label();
            this.label_clan_lvl_text = new System.Windows.Forms.Label();
            this.label_clan_ally_text = new System.Windows.Forms.Label();
            this.label_clan_name_text = new System.Windows.Forms.Label();
            this.label_clan_leader_text = new System.Windows.Forms.Label();
            this.label_clan_rep = new System.Windows.Forms.Label();
            this.pictureBox_clan_crest = new System.Windows.Forms.PictureBox();
            this.label_caln_ally = new System.Windows.Forms.Label();
            this.label_clan_castle = new System.Windows.Forms.Label();
            this.label_clan_hall = new System.Windows.Forms.Label();
            this.label_clan_war = new System.Windows.Forms.Label();
            this.label_clan_level = new System.Windows.Forms.Label();
            this.label_clan_name = new System.Windows.Forms.Label();
            this.label_clan_leader = new System.Windows.Forms.Label();
            this.label_clan_online = new System.Windows.Forms.Label();
            this.listView_char_clan = new System.Windows.Forms.ListView();
            this.columnHeader191 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader192 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader193 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader194 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList_crests = new System.Windows.Forms.ImageList(this.components);
            this.tabPage_char_detail = new System.Windows.Forms.TabPage();
            this.listView_char_data = new System.Windows.Forms.ListView();
            this.columnHeader103 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader104 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage_players = new System.Windows.Forms.TabPage();
            this.listView_players_data = new System.Windows.Forms.ListView();
            this.columnHeader80 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader81 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader82 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader83 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader84 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader85 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage_items = new System.Windows.Forms.TabPage();
            this.listView_items_data = new System.Windows.Forms.ListView();
            this.columnHeader130 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader131 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader132 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip_Items = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToDoNotListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage_npc = new System.Windows.Forms.TabPage();
            this.listView_npc_data = new System.Windows.Forms.ListView();
            this.columnHeader135 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader136 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader137 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip_NPC = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToDoNotListNPCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToBlackListNPCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabPage_npc_chat = new System.Windows.Forms.TabPage();
            this.panel_npc_chat = new System.Windows.Forms.Panel();
            this.textBox_rtb_input = new System.Windows.Forms.TextBox();
            this.richTextBox_dialog = new L2_login.RichTextBoxEx();
            this.button_npc_close = new System.Windows.Forms.Button();
            this.tabPage_buffs = new System.Windows.Forms.TabPage();
            this.listView_mybuffs_data = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPage_stats = new System.Windows.Forms.TabPage();
            this.label_badmobs = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label_meshlessignored = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label_adena_total = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.button_clear_stats = new System.Windows.Forms.Button();
            this.listView_stats = new System.Windows.Forms.ListView();
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label_SP = new System.Windows.Forms.Label();
            this.label_XP = new System.Windows.Forms.Label();
            this.label_Adena = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBox_op_control = new System.Windows.Forms.CheckBox();
            this.checkBox_op_shift = new System.Windows.Forms.CheckBox();
            this.label_zrange_map = new System.Windows.Forms.Label();
            this.trackBar_map_zoom = new System.Windows.Forms.TrackBar();
            this.textBox_zrange_map = new System.Windows.Forms.TextBox();
            this.checkBox_minimap = new System.Windows.Forms.CheckBox();
            this.panel_chat = new System.Windows.Forms.Panel();
            this.checkBox_BoundingPoints = new System.Windows.Forms.CheckBox();
            this.comboBox_msg_type = new System.Windows.Forms.ComboBox();
            this.button_sendtext = new System.Windows.Forms.Button();
            this.textBox_say = new System.Windows.Forms.TextBox();
            this.tabControl_ChatSelect = new System.Windows.Forms.TabControl();
            this.tab_all = new System.Windows.Forms.TabPage();
            this.colorListBox_all = new L2_login.ColorListBox();
            this.tab_system = new System.Windows.Forms.TabPage();
            this.colorListBox_system = new L2_login.ColorListBox();
            this.tab_bot = new System.Windows.Forms.TabPage();
            this.colorListBox_bot = new L2_login.ColorListBox();
            this.tab_local = new System.Windows.Forms.TabPage();
            this.colorListBox_local = new L2_login.ColorListBox();
            this.tab_trade = new System.Windows.Forms.TabPage();
            this.colorListBox_trade = new L2_login.ColorListBox();
            this.tab_party = new System.Windows.Forms.TabPage();
            this.colorListBox_party = new L2_login.ColorListBox();
            this.tab_clan = new System.Windows.Forms.TabPage();
            this.colorListBox_clan = new L2_login.ColorListBox();
            this.tab_alliance = new System.Windows.Forms.TabPage();
            this.colorListBox_ally = new L2_login.ColorListBox();
            this.tab_hero = new System.Windows.Forms.TabPage();
            this.colorListBox_hero = new L2_login.ColorListBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.notifyIcon_us = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip_notify = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toggleBottingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.botOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startScriptToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.forceLogToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuItem_File = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_cmd_logon = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_cmd_game = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_cmd_GG = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_cmd_ggclient = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItem_cmd_overlay = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_cmd_shortcut = new System.Windows.Forms.ToolStripMenuItem();
            this.petWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_actions = new System.Windows.Forms.ToolStripMenuItem();
            this.extendedActionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItem_options_setup = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_saveinterface = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItem_launchl2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItem_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_Commands = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_cmd_cancel = new System.Windows.Forms.ToolStripMenuItem();
            this.blacklistTargetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showTargetInfoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItem_toggle_botting = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_toggle_autoreply = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_toggle_autoreplyPM = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItem_cmd_restart = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_cmd_logout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItem_closeclient = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_killthreads = new System.Windows.Forms.ToolStripMenuItem();
            this.forceLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_Options = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_scripting = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_loadscript = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_startscript = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItem_scriptwindow = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_scriptdebugger = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_encryptscript = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItem_debug_mode = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_dump_mode = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_dump_mode_server = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip_pck = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_Help = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_about = new System.Windows.Forms.ToolStripMenuItem();
            this.eULAToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_forums = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_help_donate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuitem_help_checkforupdates = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItem_language = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_hosts = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItem_forcecollect = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.automaticUpdater1 = new wyDay.Controls.AutomaticUpdater();
            this.disconectClientToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_party_5.SuspendLayout();
            this.panel_party_6.SuspendLayout();
            this.panel_party_7.SuspendLayout();
            this.panel_party_8.SuspendLayout();
            this.panel_party_4.SuspendLayout();
            this.panel_party_3.SuspendLayout();
            this.panel_party_2.SuspendLayout();
            this.panel_party_1.SuspendLayout();
            this.panel_charinfo.SuspendLayout();
            this.panel_target.SuspendLayout();
            this.panel_charinfo_ul.SuspendLayout();
            this.panel_char.SuspendLayout();
            this.tabControl_char.SuspendLayout();
            this.tabPage_char_info.SuspendLayout();
            this.panel_dead.SuspendLayout();
            this.panel_yesno.SuspendLayout();
            this.tabPage_char_inv.SuspendLayout();
            this.contextMenuStrip_inventory.SuspendLayout();
            this.tabPage_char_skills.SuspendLayout();
            this.tabPage_char_clan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_clan_crest)).BeginInit();
            this.tabPage_char_detail.SuspendLayout();
            this.tabPage_players.SuspendLayout();
            this.tabPage_items.SuspendLayout();
            this.contextMenuStrip_Items.SuspendLayout();
            this.tabPage_npc.SuspendLayout();
            this.contextMenuStrip_NPC.SuspendLayout();
            this.tabPage_npc_chat.SuspendLayout();
            this.panel_npc_chat.SuspendLayout();
            this.tabPage_buffs.SuspendLayout();
            this.tabPage_stats.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_map_zoom)).BeginInit();
            this.panel_chat.SuspendLayout();
            this.tabControl_ChatSelect.SuspendLayout();
            this.tab_all.SuspendLayout();
            this.tab_system.SuspendLayout();
            this.tab_bot.SuspendLayout();
            this.tab_local.SuspendLayout();
            this.tab_trade.SuspendLayout();
            this.tab_party.SuspendLayout();
            this.tab_clan.SuspendLayout();
            this.tab_alliance.SuspendLayout();
            this.tab_hero.SuspendLayout();
            this.contextMenuStrip_notify.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.automaticUpdater1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_party_5
            // 
            this.panel_party_5.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel_party_5.BackgroundImage")));
            this.panel_party_5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_party_5.Controls.Add(this.label_5_cp);
            this.panel_party_5.Controls.Add(this.label_5_mp);
            this.panel_party_5.Controls.Add(this.label_5_hp);
            this.panel_party_5.Controls.Add(this.label_5_name);
            this.panel_party_5.ForeColor = System.Drawing.Color.White;
            this.panel_party_5.Location = new System.Drawing.Point(0, 404);
            this.panel_party_5.Name = "panel_party_5";
            this.panel_party_5.Size = new System.Drawing.Size(128, 64);
            this.panel_party_5.TabIndex = 8;
            // 
            // label_5_cp
            // 
            this.label_5_cp.BackColor = System.Drawing.Color.Transparent;
            this.label_5_cp.Location = new System.Drawing.Point(15, 16);
            this.label_5_cp.Name = "label_5_cp";
            this.label_5_cp.Size = new System.Drawing.Size(113, 16);
            this.label_5_cp.TabIndex = 7;
            this.label_5_cp.Text = "cp";
            this.label_5_cp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_5_mp
            // 
            this.label_5_mp.BackColor = System.Drawing.Color.Transparent;
            this.label_5_mp.Location = new System.Drawing.Point(15, 48);
            this.label_5_mp.Name = "label_5_mp";
            this.label_5_mp.Size = new System.Drawing.Size(113, 16);
            this.label_5_mp.TabIndex = 6;
            this.label_5_mp.Text = "mp";
            this.label_5_mp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_5_hp
            // 
            this.label_5_hp.BackColor = System.Drawing.Color.Transparent;
            this.label_5_hp.Location = new System.Drawing.Point(15, 32);
            this.label_5_hp.Name = "label_5_hp";
            this.label_5_hp.Size = new System.Drawing.Size(113, 16);
            this.label_5_hp.TabIndex = 5;
            this.label_5_hp.Text = "hp";
            this.label_5_hp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_5_name
            // 
            this.label_5_name.BackColor = System.Drawing.Color.Transparent;
            this.label_5_name.Location = new System.Drawing.Point(15, 0);
            this.label_5_name.Name = "label_5_name";
            this.label_5_name.Size = new System.Drawing.Size(113, 16);
            this.label_5_name.TabIndex = 1;
            this.label_5_name.Text = "Name";
            this.label_5_name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_5_name.Click += new System.EventHandler(this.label_5_name_Click);
            // 
            // panel_party_6
            // 
            this.panel_party_6.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel_party_6.BackgroundImage")));
            this.panel_party_6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_party_6.Controls.Add(this.label_6_cp);
            this.panel_party_6.Controls.Add(this.label_6_mp);
            this.panel_party_6.Controls.Add(this.label_6_hp);
            this.panel_party_6.Controls.Add(this.label_6_name);
            this.panel_party_6.ForeColor = System.Drawing.Color.White;
            this.panel_party_6.Location = new System.Drawing.Point(0, 468);
            this.panel_party_6.Name = "panel_party_6";
            this.panel_party_6.Size = new System.Drawing.Size(128, 64);
            this.panel_party_6.TabIndex = 7;
            // 
            // label_6_cp
            // 
            this.label_6_cp.BackColor = System.Drawing.Color.Transparent;
            this.label_6_cp.Location = new System.Drawing.Point(15, 16);
            this.label_6_cp.Name = "label_6_cp";
            this.label_6_cp.Size = new System.Drawing.Size(113, 16);
            this.label_6_cp.TabIndex = 7;
            this.label_6_cp.Text = "cp";
            this.label_6_cp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_6_mp
            // 
            this.label_6_mp.BackColor = System.Drawing.Color.Transparent;
            this.label_6_mp.Location = new System.Drawing.Point(15, 48);
            this.label_6_mp.Name = "label_6_mp";
            this.label_6_mp.Size = new System.Drawing.Size(113, 16);
            this.label_6_mp.TabIndex = 6;
            this.label_6_mp.Text = "mp";
            this.label_6_mp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_6_hp
            // 
            this.label_6_hp.BackColor = System.Drawing.Color.Transparent;
            this.label_6_hp.Location = new System.Drawing.Point(15, 32);
            this.label_6_hp.Name = "label_6_hp";
            this.label_6_hp.Size = new System.Drawing.Size(113, 16);
            this.label_6_hp.TabIndex = 5;
            this.label_6_hp.Text = "hp";
            this.label_6_hp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_6_name
            // 
            this.label_6_name.BackColor = System.Drawing.Color.Transparent;
            this.label_6_name.Location = new System.Drawing.Point(15, 0);
            this.label_6_name.Name = "label_6_name";
            this.label_6_name.Size = new System.Drawing.Size(113, 16);
            this.label_6_name.TabIndex = 1;
            this.label_6_name.Text = "Name";
            this.label_6_name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_6_name.Click += new System.EventHandler(this.label_6_name_Click);
            // 
            // panel_party_7
            // 
            this.panel_party_7.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel_party_7.BackgroundImage")));
            this.panel_party_7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_party_7.Controls.Add(this.label_7_cp);
            this.panel_party_7.Controls.Add(this.label_7_mp);
            this.panel_party_7.Controls.Add(this.label_7_hp);
            this.panel_party_7.Controls.Add(this.label_7_name);
            this.panel_party_7.ForeColor = System.Drawing.Color.White;
            this.panel_party_7.Location = new System.Drawing.Point(0, 532);
            this.panel_party_7.Name = "panel_party_7";
            this.panel_party_7.Size = new System.Drawing.Size(128, 64);
            this.panel_party_7.TabIndex = 6;
            // 
            // label_7_cp
            // 
            this.label_7_cp.BackColor = System.Drawing.Color.Transparent;
            this.label_7_cp.Location = new System.Drawing.Point(15, 16);
            this.label_7_cp.Name = "label_7_cp";
            this.label_7_cp.Size = new System.Drawing.Size(113, 16);
            this.label_7_cp.TabIndex = 7;
            this.label_7_cp.Text = "cp";
            this.label_7_cp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_7_mp
            // 
            this.label_7_mp.BackColor = System.Drawing.Color.Transparent;
            this.label_7_mp.Location = new System.Drawing.Point(15, 48);
            this.label_7_mp.Name = "label_7_mp";
            this.label_7_mp.Size = new System.Drawing.Size(113, 16);
            this.label_7_mp.TabIndex = 6;
            this.label_7_mp.Text = "mp";
            this.label_7_mp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_7_hp
            // 
            this.label_7_hp.BackColor = System.Drawing.Color.Transparent;
            this.label_7_hp.Location = new System.Drawing.Point(15, 32);
            this.label_7_hp.Name = "label_7_hp";
            this.label_7_hp.Size = new System.Drawing.Size(113, 16);
            this.label_7_hp.TabIndex = 5;
            this.label_7_hp.Text = "hp";
            this.label_7_hp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_7_name
            // 
            this.label_7_name.BackColor = System.Drawing.Color.Transparent;
            this.label_7_name.Location = new System.Drawing.Point(15, 0);
            this.label_7_name.Name = "label_7_name";
            this.label_7_name.Size = new System.Drawing.Size(113, 16);
            this.label_7_name.TabIndex = 1;
            this.label_7_name.Text = "Name";
            this.label_7_name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_7_name.Click += new System.EventHandler(this.label_7_name_Click);
            // 
            // panel_party_8
            // 
            this.panel_party_8.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel_party_8.BackgroundImage")));
            this.panel_party_8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_party_8.Controls.Add(this.label_8_cp);
            this.panel_party_8.Controls.Add(this.label_8_mp);
            this.panel_party_8.Controls.Add(this.label_8_hp);
            this.panel_party_8.Controls.Add(this.label_8_name);
            this.panel_party_8.ForeColor = System.Drawing.Color.White;
            this.panel_party_8.Location = new System.Drawing.Point(0, 596);
            this.panel_party_8.Name = "panel_party_8";
            this.panel_party_8.Size = new System.Drawing.Size(128, 64);
            this.panel_party_8.TabIndex = 4;
            // 
            // label_8_cp
            // 
            this.label_8_cp.BackColor = System.Drawing.Color.Transparent;
            this.label_8_cp.Location = new System.Drawing.Point(15, 16);
            this.label_8_cp.Name = "label_8_cp";
            this.label_8_cp.Size = new System.Drawing.Size(113, 16);
            this.label_8_cp.TabIndex = 7;
            this.label_8_cp.Text = "cp";
            this.label_8_cp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_8_mp
            // 
            this.label_8_mp.BackColor = System.Drawing.Color.Transparent;
            this.label_8_mp.Location = new System.Drawing.Point(15, 48);
            this.label_8_mp.Name = "label_8_mp";
            this.label_8_mp.Size = new System.Drawing.Size(113, 16);
            this.label_8_mp.TabIndex = 6;
            this.label_8_mp.Text = "mp";
            this.label_8_mp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_8_hp
            // 
            this.label_8_hp.BackColor = System.Drawing.Color.Transparent;
            this.label_8_hp.Location = new System.Drawing.Point(15, 32);
            this.label_8_hp.Name = "label_8_hp";
            this.label_8_hp.Size = new System.Drawing.Size(113, 16);
            this.label_8_hp.TabIndex = 5;
            this.label_8_hp.Text = "hp";
            this.label_8_hp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_8_name
            // 
            this.label_8_name.BackColor = System.Drawing.Color.Transparent;
            this.label_8_name.Location = new System.Drawing.Point(15, 0);
            this.label_8_name.Name = "label_8_name";
            this.label_8_name.Size = new System.Drawing.Size(113, 16);
            this.label_8_name.TabIndex = 1;
            this.label_8_name.Text = "Name";
            this.label_8_name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_8_name.Click += new System.EventHandler(this.label_8_name_Click);
            // 
            // panel_party_4
            // 
            this.panel_party_4.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel_party_4.BackgroundImage")));
            this.panel_party_4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_party_4.Controls.Add(this.label_4_cp);
            this.panel_party_4.Controls.Add(this.label_4_mp);
            this.panel_party_4.Controls.Add(this.label_4_hp);
            this.panel_party_4.Controls.Add(this.label_4_name);
            this.panel_party_4.ForeColor = System.Drawing.Color.White;
            this.panel_party_4.Location = new System.Drawing.Point(0, 340);
            this.panel_party_4.Name = "panel_party_4";
            this.panel_party_4.Size = new System.Drawing.Size(128, 64);
            this.panel_party_4.TabIndex = 3;
            // 
            // label_4_cp
            // 
            this.label_4_cp.BackColor = System.Drawing.Color.Transparent;
            this.label_4_cp.Location = new System.Drawing.Point(15, 16);
            this.label_4_cp.Name = "label_4_cp";
            this.label_4_cp.Size = new System.Drawing.Size(113, 16);
            this.label_4_cp.TabIndex = 7;
            this.label_4_cp.Text = "cp";
            this.label_4_cp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_4_mp
            // 
            this.label_4_mp.BackColor = System.Drawing.Color.Transparent;
            this.label_4_mp.Location = new System.Drawing.Point(15, 48);
            this.label_4_mp.Name = "label_4_mp";
            this.label_4_mp.Size = new System.Drawing.Size(113, 16);
            this.label_4_mp.TabIndex = 6;
            this.label_4_mp.Text = "mp";
            this.label_4_mp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_4_hp
            // 
            this.label_4_hp.BackColor = System.Drawing.Color.Transparent;
            this.label_4_hp.Location = new System.Drawing.Point(15, 32);
            this.label_4_hp.Name = "label_4_hp";
            this.label_4_hp.Size = new System.Drawing.Size(113, 16);
            this.label_4_hp.TabIndex = 5;
            this.label_4_hp.Text = "hp";
            this.label_4_hp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_4_name
            // 
            this.label_4_name.BackColor = System.Drawing.Color.Transparent;
            this.label_4_name.Location = new System.Drawing.Point(15, 0);
            this.label_4_name.Name = "label_4_name";
            this.label_4_name.Size = new System.Drawing.Size(113, 16);
            this.label_4_name.TabIndex = 1;
            this.label_4_name.Text = "Name";
            this.label_4_name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_4_name.Click += new System.EventHandler(this.label_4_name_Click);
            // 
            // panel_party_3
            // 
            this.panel_party_3.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel_party_3.BackgroundImage")));
            this.panel_party_3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_party_3.Controls.Add(this.label_3_cp);
            this.panel_party_3.Controls.Add(this.label_3_mp);
            this.panel_party_3.Controls.Add(this.label_3_hp);
            this.panel_party_3.Controls.Add(this.label_3_name);
            this.panel_party_3.ForeColor = System.Drawing.Color.White;
            this.panel_party_3.Location = new System.Drawing.Point(0, 276);
            this.panel_party_3.Name = "panel_party_3";
            this.panel_party_3.Size = new System.Drawing.Size(128, 64);
            this.panel_party_3.TabIndex = 2;
            // 
            // label_3_cp
            // 
            this.label_3_cp.BackColor = System.Drawing.Color.Transparent;
            this.label_3_cp.Location = new System.Drawing.Point(15, 16);
            this.label_3_cp.Name = "label_3_cp";
            this.label_3_cp.Size = new System.Drawing.Size(113, 16);
            this.label_3_cp.TabIndex = 7;
            this.label_3_cp.Text = "cp";
            this.label_3_cp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_3_mp
            // 
            this.label_3_mp.BackColor = System.Drawing.Color.Transparent;
            this.label_3_mp.Location = new System.Drawing.Point(15, 48);
            this.label_3_mp.Name = "label_3_mp";
            this.label_3_mp.Size = new System.Drawing.Size(113, 16);
            this.label_3_mp.TabIndex = 6;
            this.label_3_mp.Text = "mp";
            this.label_3_mp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_3_hp
            // 
            this.label_3_hp.BackColor = System.Drawing.Color.Transparent;
            this.label_3_hp.Location = new System.Drawing.Point(15, 32);
            this.label_3_hp.Name = "label_3_hp";
            this.label_3_hp.Size = new System.Drawing.Size(113, 16);
            this.label_3_hp.TabIndex = 5;
            this.label_3_hp.Text = "hp";
            this.label_3_hp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_3_name
            // 
            this.label_3_name.BackColor = System.Drawing.Color.Transparent;
            this.label_3_name.Location = new System.Drawing.Point(15, 0);
            this.label_3_name.Name = "label_3_name";
            this.label_3_name.Size = new System.Drawing.Size(113, 16);
            this.label_3_name.TabIndex = 1;
            this.label_3_name.Text = "Name";
            this.label_3_name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_3_name.Click += new System.EventHandler(this.label_3_name_Click);
            // 
            // panel_party_2
            // 
            this.panel_party_2.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel_party_2.BackgroundImage")));
            this.panel_party_2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_party_2.Controls.Add(this.label_2_cp);
            this.panel_party_2.Controls.Add(this.label_2_mp);
            this.panel_party_2.Controls.Add(this.label_2_hp);
            this.panel_party_2.Controls.Add(this.label_2_name);
            this.panel_party_2.ForeColor = System.Drawing.Color.White;
            this.panel_party_2.Location = new System.Drawing.Point(0, 212);
            this.panel_party_2.Name = "panel_party_2";
            this.panel_party_2.Size = new System.Drawing.Size(128, 64);
            this.panel_party_2.TabIndex = 1;
            // 
            // label_2_cp
            // 
            this.label_2_cp.BackColor = System.Drawing.Color.Transparent;
            this.label_2_cp.Location = new System.Drawing.Point(15, 16);
            this.label_2_cp.Name = "label_2_cp";
            this.label_2_cp.Size = new System.Drawing.Size(113, 16);
            this.label_2_cp.TabIndex = 7;
            this.label_2_cp.Text = "cp";
            this.label_2_cp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_2_mp
            // 
            this.label_2_mp.BackColor = System.Drawing.Color.Transparent;
            this.label_2_mp.Location = new System.Drawing.Point(15, 48);
            this.label_2_mp.Name = "label_2_mp";
            this.label_2_mp.Size = new System.Drawing.Size(113, 16);
            this.label_2_mp.TabIndex = 6;
            this.label_2_mp.Text = "mp";
            this.label_2_mp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_2_hp
            // 
            this.label_2_hp.BackColor = System.Drawing.Color.Transparent;
            this.label_2_hp.Location = new System.Drawing.Point(15, 32);
            this.label_2_hp.Name = "label_2_hp";
            this.label_2_hp.Size = new System.Drawing.Size(113, 16);
            this.label_2_hp.TabIndex = 5;
            this.label_2_hp.Text = "hp";
            this.label_2_hp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_2_name
            // 
            this.label_2_name.BackColor = System.Drawing.Color.Transparent;
            this.label_2_name.Location = new System.Drawing.Point(15, 0);
            this.label_2_name.Name = "label_2_name";
            this.label_2_name.Size = new System.Drawing.Size(113, 16);
            this.label_2_name.TabIndex = 1;
            this.label_2_name.Text = "Name";
            this.label_2_name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_2_name.Click += new System.EventHandler(this.label_2_name_Click);
            // 
            // panel_party_1
            // 
            this.panel_party_1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel_party_1.BackgroundImage")));
            this.panel_party_1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_party_1.Controls.Add(this.label_1_cp);
            this.panel_party_1.Controls.Add(this.label_1_mp);
            this.panel_party_1.Controls.Add(this.label_1_hp);
            this.panel_party_1.Controls.Add(this.label_1_name);
            this.panel_party_1.ForeColor = System.Drawing.Color.White;
            this.panel_party_1.Location = new System.Drawing.Point(0, 148);
            this.panel_party_1.Name = "panel_party_1";
            this.panel_party_1.Size = new System.Drawing.Size(128, 64);
            this.panel_party_1.TabIndex = 0;
            // 
            // label_1_cp
            // 
            this.label_1_cp.BackColor = System.Drawing.Color.Transparent;
            this.label_1_cp.ForeColor = System.Drawing.Color.White;
            this.label_1_cp.Location = new System.Drawing.Point(15, 16);
            this.label_1_cp.Name = "label_1_cp";
            this.label_1_cp.Size = new System.Drawing.Size(113, 16);
            this.label_1_cp.TabIndex = 7;
            this.label_1_cp.Text = "cp";
            this.label_1_cp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_1_mp
            // 
            this.label_1_mp.BackColor = System.Drawing.Color.Transparent;
            this.label_1_mp.ForeColor = System.Drawing.Color.White;
            this.label_1_mp.Location = new System.Drawing.Point(15, 48);
            this.label_1_mp.Name = "label_1_mp";
            this.label_1_mp.Size = new System.Drawing.Size(113, 16);
            this.label_1_mp.TabIndex = 6;
            this.label_1_mp.Text = "mp";
            this.label_1_mp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_1_hp
            // 
            this.label_1_hp.BackColor = System.Drawing.Color.Transparent;
            this.label_1_hp.ForeColor = System.Drawing.Color.White;
            this.label_1_hp.Location = new System.Drawing.Point(15, 32);
            this.label_1_hp.Name = "label_1_hp";
            this.label_1_hp.Size = new System.Drawing.Size(113, 16);
            this.label_1_hp.TabIndex = 5;
            this.label_1_hp.Text = "hp";
            this.label_1_hp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_1_name
            // 
            this.label_1_name.BackColor = System.Drawing.Color.Transparent;
            this.label_1_name.ForeColor = System.Drawing.Color.White;
            this.label_1_name.Location = new System.Drawing.Point(15, 0);
            this.label_1_name.Name = "label_1_name";
            this.label_1_name.Size = new System.Drawing.Size(113, 16);
            this.label_1_name.TabIndex = 1;
            this.label_1_name.Text = "Name";
            this.label_1_name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_1_name.Click += new System.EventHandler(this.label_1_name_Click);
            // 
            // panel_charinfo
            // 
            this.panel_charinfo.Controls.Add(this.panel_target);
            this.panel_charinfo.Controls.Add(this.panel_charinfo_ul);
            this.panel_charinfo.Controls.Add(this.panel_party_8);
            this.panel_charinfo.Controls.Add(this.panel_party_6);
            this.panel_charinfo.Controls.Add(this.panel_party_7);
            this.panel_charinfo.Controls.Add(this.panel_party_5);
            this.panel_charinfo.Controls.Add(this.panel_party_4);
            this.panel_charinfo.Controls.Add(this.panel_party_3);
            this.panel_charinfo.Controls.Add(this.panel_party_2);
            this.panel_charinfo.Controls.Add(this.panel_party_1);
            this.panel_charinfo.Controls.Add(this.panel_party_cover);
            this.panel_charinfo.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_charinfo.ForeColor = System.Drawing.Color.White;
            this.panel_charinfo.Location = new System.Drawing.Point(0, 0);
            this.panel_charinfo.Name = "panel_charinfo";
            this.panel_charinfo.Size = new System.Drawing.Size(128, 659);
            this.panel_charinfo.TabIndex = 16;
            // 
            // panel_target
            // 
            this.panel_target.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel_target.BackgroundImage")));
            this.panel_target.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_target.Controls.Add(this.label_target_cp);
            this.panel_target.Controls.Add(this.label_target_mp);
            this.panel_target.Controls.Add(this.label_target_hp);
            this.panel_target.Controls.Add(this.label_target_name);
            this.panel_target.ForeColor = System.Drawing.Color.White;
            this.panel_target.Location = new System.Drawing.Point(0, 0);
            this.panel_target.Name = "panel_target";
            this.panel_target.Size = new System.Drawing.Size(128, 64);
            this.panel_target.TabIndex = 9;
            // 
            // label_target_cp
            // 
            this.label_target_cp.BackColor = System.Drawing.Color.Transparent;
            this.label_target_cp.ForeColor = System.Drawing.Color.White;
            this.label_target_cp.Location = new System.Drawing.Point(15, 16);
            this.label_target_cp.Name = "label_target_cp";
            this.label_target_cp.Size = new System.Drawing.Size(113, 16);
            this.label_target_cp.TabIndex = 7;
            this.label_target_cp.Text = "-cp-";
            this.label_target_cp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_target_mp
            // 
            this.label_target_mp.BackColor = System.Drawing.Color.Transparent;
            this.label_target_mp.ForeColor = System.Drawing.Color.White;
            this.label_target_mp.Location = new System.Drawing.Point(15, 48);
            this.label_target_mp.Name = "label_target_mp";
            this.label_target_mp.Size = new System.Drawing.Size(113, 16);
            this.label_target_mp.TabIndex = 6;
            this.label_target_mp.Text = "-mp-";
            this.label_target_mp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_target_hp
            // 
            this.label_target_hp.BackColor = System.Drawing.Color.Transparent;
            this.label_target_hp.ForeColor = System.Drawing.Color.White;
            this.label_target_hp.Location = new System.Drawing.Point(15, 32);
            this.label_target_hp.Name = "label_target_hp";
            this.label_target_hp.Size = new System.Drawing.Size(113, 16);
            this.label_target_hp.TabIndex = 5;
            this.label_target_hp.Text = "-hp-";
            this.label_target_hp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_target_name
            // 
            this.label_target_name.BackColor = System.Drawing.Color.Transparent;
            this.label_target_name.Location = new System.Drawing.Point(15, 0);
            this.label_target_name.Name = "label_target_name";
            this.label_target_name.Size = new System.Drawing.Size(113, 16);
            this.label_target_name.TabIndex = 1;
            this.label_target_name.Text = "-none-";
            this.label_target_name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_charinfo_ul
            // 
            this.panel_charinfo_ul.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel_charinfo_ul.BackgroundImage")));
            this.panel_charinfo_ul.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_charinfo_ul.Controls.Add(this.progressBar_char_XP);
            this.panel_charinfo_ul.Controls.Add(this.progressBar_char_MP);
            this.panel_charinfo_ul.Controls.Add(this.progressBar_char_CP);
            this.panel_charinfo_ul.Controls.Add(this.label_char_vitality);
            this.panel_charinfo_ul.Controls.Add(this.label_char_xp);
            this.panel_charinfo_ul.Controls.Add(this.label_char_level);
            this.panel_charinfo_ul.Controls.Add(this.label_char_cp);
            this.panel_charinfo_ul.Controls.Add(this.label_char_mp);
            this.panel_charinfo_ul.Controls.Add(this.label_char_name);
            this.panel_charinfo_ul.Controls.Add(this.progressBar_char_HP);
            this.panel_charinfo_ul.Controls.Add(this.label_char_hp);
            this.panel_charinfo_ul.Location = new System.Drawing.Point(0, 64);
            this.panel_charinfo_ul.Name = "panel_charinfo_ul";
            this.panel_charinfo_ul.Size = new System.Drawing.Size(128, 84);
            this.panel_charinfo_ul.TabIndex = 6;
            // 
            // progressBar_char_XP
            // 
            this.progressBar_char_XP.Animate = false;
            this.progressBar_char_XP.BackColor = System.Drawing.Color.Transparent;
            this.progressBar_char_XP.BackgroundColor = System.Drawing.Color.Transparent;
            this.progressBar_char_XP.BarText = "XP";
            this.progressBar_char_XP.BarTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.progressBar_char_XP.EndColor = System.Drawing.Color.DarkGray;
            this.progressBar_char_XP.Location = new System.Drawing.Point(15, 68);
            this.progressBar_char_XP.Name = "progressBar_char_XP";
            this.progressBar_char_XP.Size = new System.Drawing.Size(113, 16);
            this.progressBar_char_XP.StartColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.progressBar_char_XP.TabIndex = 31;
            // 
            // progressBar_char_MP
            // 
            this.progressBar_char_MP.BackColor = System.Drawing.Color.Transparent;
            this.progressBar_char_MP.BackgroundColor = System.Drawing.Color.Transparent;
            this.progressBar_char_MP.BarText = "MP";
            this.progressBar_char_MP.BarTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.progressBar_char_MP.EndColor = System.Drawing.Color.Blue;
            this.progressBar_char_MP.Location = new System.Drawing.Point(15, 52);
            this.progressBar_char_MP.Name = "progressBar_char_MP";
            this.progressBar_char_MP.Size = new System.Drawing.Size(113, 16);
            this.progressBar_char_MP.StartColor = System.Drawing.Color.Navy;
            this.progressBar_char_MP.TabIndex = 31;
            // 
            // progressBar_char_CP
            // 
            this.progressBar_char_CP.BackColor = System.Drawing.Color.Transparent;
            this.progressBar_char_CP.BackgroundColor = System.Drawing.Color.Transparent;
            this.progressBar_char_CP.BarText = "CP";
            this.progressBar_char_CP.BarTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.progressBar_char_CP.EndColor = System.Drawing.Color.Orange;
            this.progressBar_char_CP.ForeColor = System.Drawing.Color.Transparent;
            this.progressBar_char_CP.Location = new System.Drawing.Point(15, 22);
            this.progressBar_char_CP.Name = "progressBar_char_CP";
            this.progressBar_char_CP.Size = new System.Drawing.Size(113, 16);
            this.progressBar_char_CP.StartColor = System.Drawing.Color.Yellow;
            this.progressBar_char_CP.TabIndex = 31;
            // 
            // label_char_vitality
            // 
            this.label_char_vitality.BackColor = System.Drawing.Color.Transparent;
            this.label_char_vitality.ForeColor = System.Drawing.Color.White;
            this.label_char_vitality.Location = new System.Drawing.Point(-3, 65);
            this.label_char_vitality.Name = "label_char_vitality";
            this.label_char_vitality.Size = new System.Drawing.Size(19, 16);
            this.label_char_vitality.TabIndex = 29;
            this.label_char_vitality.Text = "vit";
            this.label_char_vitality.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_char_xp
            // 
            this.label_char_xp.BackColor = System.Drawing.Color.Transparent;
            this.label_char_xp.ForeColor = System.Drawing.Color.White;
            this.label_char_xp.Location = new System.Drawing.Point(15, 66);
            this.label_char_xp.Name = "label_char_xp";
            this.label_char_xp.Size = new System.Drawing.Size(113, 16);
            this.label_char_xp.TabIndex = 11;
            this.label_char_xp.Text = "xp";
            this.label_char_xp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_char_level
            // 
            this.label_char_level.BackColor = System.Drawing.Color.Green;
            this.label_char_level.Location = new System.Drawing.Point(15, 6);
            this.label_char_level.Name = "label_char_level";
            this.label_char_level.Size = new System.Drawing.Size(24, 16);
            this.label_char_level.TabIndex = 5;
            this.label_char_level.Text = "0";
            this.label_char_level.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_char_cp
            // 
            this.label_char_cp.BackColor = System.Drawing.Color.Transparent;
            this.label_char_cp.Location = new System.Drawing.Point(15, 22);
            this.label_char_cp.Name = "label_char_cp";
            this.label_char_cp.Size = new System.Drawing.Size(113, 16);
            this.label_char_cp.TabIndex = 4;
            this.label_char_cp.Text = "cp";
            this.label_char_cp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_char_mp
            // 
            this.label_char_mp.BackColor = System.Drawing.Color.Transparent;
            this.label_char_mp.Location = new System.Drawing.Point(15, 52);
            this.label_char_mp.Name = "label_char_mp";
            this.label_char_mp.Size = new System.Drawing.Size(113, 16);
            this.label_char_mp.TabIndex = 3;
            this.label_char_mp.Text = "mp";
            this.label_char_mp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_char_name
            // 
            this.label_char_name.BackColor = System.Drawing.Color.Transparent;
            this.label_char_name.Location = new System.Drawing.Point(39, 6);
            this.label_char_name.Name = "label_char_name";
            this.label_char_name.Size = new System.Drawing.Size(86, 16);
            this.label_char_name.TabIndex = 0;
            this.label_char_name.Text = "Name";
            this.label_char_name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar_char_HP
            // 
            this.progressBar_char_HP.BackColor = System.Drawing.Color.Transparent;
            this.progressBar_char_HP.BackgroundColor = System.Drawing.Color.Transparent;
            this.progressBar_char_HP.BarText = "HP";
            this.progressBar_char_HP.BarTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.progressBar_char_HP.EndColor = System.Drawing.Color.Red;
            this.progressBar_char_HP.Location = new System.Drawing.Point(15, 38);
            this.progressBar_char_HP.Name = "progressBar_char_HP";
            this.progressBar_char_HP.Size = new System.Drawing.Size(113, 16);
            this.progressBar_char_HP.StartColor = System.Drawing.Color.DarkRed;
            this.progressBar_char_HP.TabIndex = 30;
            // 
            // label_char_hp
            // 
            this.label_char_hp.BackColor = System.Drawing.Color.Transparent;
            this.label_char_hp.ForeColor = System.Drawing.Color.Transparent;
            this.label_char_hp.Location = new System.Drawing.Point(15, 38);
            this.label_char_hp.Name = "label_char_hp";
            this.label_char_hp.Size = new System.Drawing.Size(113, 16);
            this.label_char_hp.TabIndex = 2;
            this.label_char_hp.Text = "hp";
            this.label_char_hp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_party_cover
            // 
            this.panel_party_cover.Location = new System.Drawing.Point(0, 148);
            this.panel_party_cover.Name = "panel_party_cover";
            this.panel_party_cover.Size = new System.Drawing.Size(128, 512);
            this.panel_party_cover.TabIndex = 27;
            // 
            // panel_char
            // 
            this.panel_char.Controls.Add(this.tabControl_char);
            this.panel_char.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel_char.Location = new System.Drawing.Point(754, 0);
            this.panel_char.Name = "panel_char";
            this.panel_char.Size = new System.Drawing.Size(262, 659);
            this.panel_char.TabIndex = 20;
            // 
            // tabControl_char
            // 
            this.tabControl_char.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControl_char.Controls.Add(this.tabPage_char_info);
            this.tabControl_char.Controls.Add(this.tabPage_char_inv);
            this.tabControl_char.Controls.Add(this.tabPage_char_skills);
            this.tabControl_char.Controls.Add(this.tabPage_char_clan);
            this.tabControl_char.Controls.Add(this.tabPage_char_detail);
            this.tabControl_char.Controls.Add(this.tabPage_players);
            this.tabControl_char.Controls.Add(this.tabPage_items);
            this.tabControl_char.Controls.Add(this.tabPage_npc);
            this.tabControl_char.Controls.Add(this.tabPage_npc_chat);
            this.tabControl_char.Controls.Add(this.tabPage_buffs);
            this.tabControl_char.Controls.Add(this.tabPage_stats);
            this.tabControl_char.HotTrack = true;
            this.tabControl_char.Location = new System.Drawing.Point(0, 0);
            this.tabControl_char.Multiline = true;
            this.tabControl_char.Name = "tabControl_char";
            this.tabControl_char.SelectedIndex = 0;
            this.tabControl_char.Size = new System.Drawing.Size(264, 638);
            this.tabControl_char.TabIndex = 0;
            // 
            // tabPage_char_info
            // 
            this.tabPage_char_info.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPage_char_info.BackgroundImage")));
            this.tabPage_char_info.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tabPage_char_info.Controls.Add(this.label_info_mcritical);
            this.tabPage_char_info.Controls.Add(this.label_info_mevasion);
            this.tabPage_char_info.Controls.Add(this.label_info_maccuracy);
            this.tabPage_char_info.Controls.Add(this.label_info_mcrit_descr);
            this.tabPage_char_info.Controls.Add(this.label_info_meva_descr);
            this.tabPage_char_info.Controls.Add(this.label_info_macc_descr);
            this.tabPage_char_info.Controls.Add(this.label_info_eval);
            this.tabPage_char_info.Controls.Add(this.label_info_fame);
            this.tabPage_char_info.Controls.Add(this.label_info_darkness);
            this.tabPage_char_info.Controls.Add(this.label_info_earth);
            this.tabPage_char_info.Controls.Add(this.label_info_water);
            this.tabPage_char_info.Controls.Add(this.label_info_divinity);
            this.tabPage_char_info.Controls.Add(this.label_info_wind);
            this.tabPage_char_info.Controls.Add(this.label_info_fire);
            this.tabPage_char_info.Controls.Add(this.label_info_atk_attrib_value);
            this.tabPage_char_info.Controls.Add(this.label_info_atk_attrib);
            this.tabPage_char_info.Controls.Add(this.label_info_recommend_desc);
            this.tabPage_char_info.Controls.Add(this.label_info_fame_desc);
            this.tabPage_char_info.Controls.Add(this.label_info_darkness_desc);
            this.tabPage_char_info.Controls.Add(this.label_info_earth_desc);
            this.tabPage_char_info.Controls.Add(this.label_info_water_desc);
            this.tabPage_char_info.Controls.Add(this.label_info_divinity_desc);
            this.tabPage_char_info.Controls.Add(this.label_info_wind_desc);
            this.tabPage_char_info.Controls.Add(this.label_info_fire_desc);
            this.tabPage_char_info.Controls.Add(this.label_info_atk_attrib_val_desc);
            this.tabPage_char_info.Controls.Add(this.label_info_atk_attrib_descr);
            this.tabPage_char_info.Controls.Add(this.label_info_pvp);
            this.tabPage_char_info.Controls.Add(this.label_info_matkspd);
            this.tabPage_char_info.Controls.Add(this.label_info_spd);
            this.tabPage_char_info.Controls.Add(this.label_info_eva);
            this.tabPage_char_info.Controls.Add(this.label_info_mdef);
            this.tabPage_char_info.Controls.Add(this.panel_dead);
            this.tabPage_char_info.Controls.Add(this.panel_yesno);
            this.tabPage_char_info.Controls.Add(this.label_info_matk);
            this.tabPage_char_info.Controls.Add(this.label_info_atkspd);
            this.tabPage_char_info.Controls.Add(this.label_info_crit);
            this.tabPage_char_info.Controls.Add(this.label_info_acc);
            this.tabPage_char_info.Controls.Add(this.label_info_pdef);
            this.tabPage_char_info.Controls.Add(this.label_info_patk);
            this.tabPage_char_info.Controls.Add(this.label_info_load);
            this.tabPage_char_info.Controls.Add(this.label_info_karma);
            this.tabPage_char_info.Controls.Add(this.label_info_men);
            this.tabPage_char_info.Controls.Add(this.label_info_wit);
            this.tabPage_char_info.Controls.Add(this.label_info_int);
            this.tabPage_char_info.Controls.Add(this.label_info_con);
            this.tabPage_char_info.Controls.Add(this.label_info_dex);
            this.tabPage_char_info.Controls.Add(this.label_info_str);
            this.tabPage_char_info.Controls.Add(this.label_info_level);
            this.tabPage_char_info.Controls.Add(this.label_info_sp);
            this.tabPage_char_info.Controls.Add(this.label_info_xp);
            this.tabPage_char_info.Controls.Add(this.label_info_cp);
            this.tabPage_char_info.Controls.Add(this.label_info_mp);
            this.tabPage_char_info.Controls.Add(this.label_info_hp);
            this.tabPage_char_info.Controls.Add(this.label_info_title);
            this.tabPage_char_info.Controls.Add(this.label_info_name);
            this.tabPage_char_info.Location = new System.Drawing.Point(4, 58);
            this.tabPage_char_info.Name = "tabPage_char_info";
            this.tabPage_char_info.Size = new System.Drawing.Size(256, 576);
            this.tabPage_char_info.TabIndex = 0;
            this.tabPage_char_info.Text = "Char";
            this.tabPage_char_info.UseVisualStyleBackColor = true;
            // 
            // label_info_mcritical
            // 
            this.label_info_mcritical.BackColor = System.Drawing.Color.Transparent;
            this.label_info_mcritical.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_mcritical.Location = new System.Drawing.Point(192, 227);
            this.label_info_mcritical.Name = "label_info_mcritical";
            this.label_info_mcritical.Size = new System.Drawing.Size(56, 16);
            this.label_info_mcritical.TabIndex = 61;
            this.label_info_mcritical.Text = "0";
            this.label_info_mcritical.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_info_mevasion
            // 
            this.label_info_mevasion.BackColor = System.Drawing.Color.Transparent;
            this.label_info_mevasion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_mevasion.Location = new System.Drawing.Point(72, 239);
            this.label_info_mevasion.Name = "label_info_mevasion";
            this.label_info_mevasion.Size = new System.Drawing.Size(56, 16);
            this.label_info_mevasion.TabIndex = 60;
            this.label_info_mevasion.Text = "0";
            this.label_info_mevasion.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_info_maccuracy
            // 
            this.label_info_maccuracy.BackColor = System.Drawing.Color.Transparent;
            this.label_info_maccuracy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_maccuracy.Location = new System.Drawing.Point(72, 226);
            this.label_info_maccuracy.Name = "label_info_maccuracy";
            this.label_info_maccuracy.Size = new System.Drawing.Size(56, 16);
            this.label_info_maccuracy.TabIndex = 59;
            this.label_info_maccuracy.Text = "0";
            this.label_info_maccuracy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_info_mcrit_descr
            // 
            this.label_info_mcrit_descr.BackColor = System.Drawing.Color.Transparent;
            this.label_info_mcrit_descr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label_info_mcrit_descr.Location = new System.Drawing.Point(133, 226);
            this.label_info_mcrit_descr.Name = "label_info_mcrit_descr";
            this.label_info_mcrit_descr.Size = new System.Drawing.Size(70, 16);
            this.label_info_mcrit_descr.TabIndex = 57;
            this.label_info_mcrit_descr.Text = "M.Critical";
            this.label_info_mcrit_descr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_info_meva_descr
            // 
            this.label_info_meva_descr.BackColor = System.Drawing.Color.Transparent;
            this.label_info_meva_descr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label_info_meva_descr.Location = new System.Drawing.Point(12, 239);
            this.label_info_meva_descr.Name = "label_info_meva_descr";
            this.label_info_meva_descr.Size = new System.Drawing.Size(70, 16);
            this.label_info_meva_descr.TabIndex = 56;
            this.label_info_meva_descr.Text = "M.Evasion";
            this.label_info_meva_descr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_info_macc_descr
            // 
            this.label_info_macc_descr.BackColor = System.Drawing.Color.Transparent;
            this.label_info_macc_descr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label_info_macc_descr.Location = new System.Drawing.Point(12, 224);
            this.label_info_macc_descr.Name = "label_info_macc_descr";
            this.label_info_macc_descr.Size = new System.Drawing.Size(70, 16);
            this.label_info_macc_descr.TabIndex = 55;
            this.label_info_macc_descr.Text = "M.Accuracy";
            this.label_info_macc_descr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_info_eval
            // 
            this.label_info_eval.BackColor = System.Drawing.Color.Transparent;
            this.label_info_eval.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_eval.Location = new System.Drawing.Point(195, 406);
            this.label_info_eval.Name = "label_info_eval";
            this.label_info_eval.Size = new System.Drawing.Size(53, 16);
            this.label_info_eval.TabIndex = 54;
            this.label_info_eval.Text = "0/0";
            this.label_info_eval.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_info_fame
            // 
            this.label_info_fame.BackColor = System.Drawing.Color.Transparent;
            this.label_info_fame.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_fame.Location = new System.Drawing.Point(192, 390);
            this.label_info_fame.Name = "label_info_fame";
            this.label_info_fame.Size = new System.Drawing.Size(56, 16);
            this.label_info_fame.TabIndex = 53;
            this.label_info_fame.Text = "0";
            this.label_info_fame.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_info_darkness
            // 
            this.label_info_darkness.BackColor = System.Drawing.Color.Transparent;
            this.label_info_darkness.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_darkness.Location = new System.Drawing.Point(192, 364);
            this.label_info_darkness.Name = "label_info_darkness";
            this.label_info_darkness.Size = new System.Drawing.Size(56, 16);
            this.label_info_darkness.TabIndex = 52;
            this.label_info_darkness.Text = "0";
            this.label_info_darkness.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_info_earth
            // 
            this.label_info_earth.BackColor = System.Drawing.Color.Transparent;
            this.label_info_earth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_earth.Location = new System.Drawing.Point(192, 348);
            this.label_info_earth.Name = "label_info_earth";
            this.label_info_earth.Size = new System.Drawing.Size(56, 16);
            this.label_info_earth.TabIndex = 51;
            this.label_info_earth.Text = "0";
            this.label_info_earth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_info_water
            // 
            this.label_info_water.BackColor = System.Drawing.Color.Transparent;
            this.label_info_water.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_water.Location = new System.Drawing.Point(192, 332);
            this.label_info_water.Name = "label_info_water";
            this.label_info_water.Size = new System.Drawing.Size(56, 16);
            this.label_info_water.TabIndex = 50;
            this.label_info_water.Text = "0";
            this.label_info_water.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_info_divinity
            // 
            this.label_info_divinity.BackColor = System.Drawing.Color.Transparent;
            this.label_info_divinity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_divinity.Location = new System.Drawing.Point(72, 364);
            this.label_info_divinity.Name = "label_info_divinity";
            this.label_info_divinity.Size = new System.Drawing.Size(56, 16);
            this.label_info_divinity.TabIndex = 49;
            this.label_info_divinity.Text = "0";
            this.label_info_divinity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_info_wind
            // 
            this.label_info_wind.BackColor = System.Drawing.Color.Transparent;
            this.label_info_wind.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_wind.Location = new System.Drawing.Point(72, 348);
            this.label_info_wind.Name = "label_info_wind";
            this.label_info_wind.Size = new System.Drawing.Size(56, 16);
            this.label_info_wind.TabIndex = 48;
            this.label_info_wind.Text = "0";
            this.label_info_wind.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_info_fire
            // 
            this.label_info_fire.BackColor = System.Drawing.Color.Transparent;
            this.label_info_fire.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_fire.Location = new System.Drawing.Point(72, 332);
            this.label_info_fire.Name = "label_info_fire";
            this.label_info_fire.Size = new System.Drawing.Size(56, 16);
            this.label_info_fire.TabIndex = 47;
            this.label_info_fire.Text = "0";
            this.label_info_fire.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_info_atk_attrib_value
            // 
            this.label_info_atk_attrib_value.BackColor = System.Drawing.Color.Transparent;
            this.label_info_atk_attrib_value.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_atk_attrib_value.Location = new System.Drawing.Point(222, 306);
            this.label_info_atk_attrib_value.Name = "label_info_atk_attrib_value";
            this.label_info_atk_attrib_value.Size = new System.Drawing.Size(26, 16);
            this.label_info_atk_attrib_value.TabIndex = 46;
            this.label_info_atk_attrib_value.Text = "0";
            this.label_info_atk_attrib_value.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_info_atk_attrib
            // 
            this.label_info_atk_attrib.BackColor = System.Drawing.Color.Transparent;
            this.label_info_atk_attrib.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_atk_attrib.Location = new System.Drawing.Point(75, 306);
            this.label_info_atk_attrib.Name = "label_info_atk_attrib";
            this.label_info_atk_attrib.Size = new System.Drawing.Size(48, 16);
            this.label_info_atk_attrib.TabIndex = 45;
            this.label_info_atk_attrib.Text = "None";
            this.label_info_atk_attrib.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_info_recommend_desc
            // 
            this.label_info_recommend_desc.BackColor = System.Drawing.Color.Transparent;
            this.label_info_recommend_desc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label_info_recommend_desc.Location = new System.Drawing.Point(132, 406);
            this.label_info_recommend_desc.Name = "label_info_recommend_desc";
            this.label_info_recommend_desc.Size = new System.Drawing.Size(70, 16);
            this.label_info_recommend_desc.TabIndex = 43;
            this.label_info_recommend_desc.Text = "Recommend";
            this.label_info_recommend_desc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_info_fame_desc
            // 
            this.label_info_fame_desc.BackColor = System.Drawing.Color.Transparent;
            this.label_info_fame_desc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label_info_fame_desc.Location = new System.Drawing.Point(132, 390);
            this.label_info_fame_desc.Name = "label_info_fame_desc";
            this.label_info_fame_desc.Size = new System.Drawing.Size(70, 16);
            this.label_info_fame_desc.TabIndex = 42;
            this.label_info_fame_desc.Text = "Fame";
            this.label_info_fame_desc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_info_darkness_desc
            // 
            this.label_info_darkness_desc.BackColor = System.Drawing.Color.Transparent;
            this.label_info_darkness_desc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label_info_darkness_desc.Location = new System.Drawing.Point(132, 364);
            this.label_info_darkness_desc.Name = "label_info_darkness_desc";
            this.label_info_darkness_desc.Size = new System.Drawing.Size(70, 16);
            this.label_info_darkness_desc.TabIndex = 41;
            this.label_info_darkness_desc.Text = "Dark";
            this.label_info_darkness_desc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_info_earth_desc
            // 
            this.label_info_earth_desc.BackColor = System.Drawing.Color.Transparent;
            this.label_info_earth_desc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label_info_earth_desc.Location = new System.Drawing.Point(132, 348);
            this.label_info_earth_desc.Name = "label_info_earth_desc";
            this.label_info_earth_desc.Size = new System.Drawing.Size(70, 16);
            this.label_info_earth_desc.TabIndex = 40;
            this.label_info_earth_desc.Text = "Earth";
            this.label_info_earth_desc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_info_water_desc
            // 
            this.label_info_water_desc.BackColor = System.Drawing.Color.Transparent;
            this.label_info_water_desc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label_info_water_desc.Location = new System.Drawing.Point(132, 332);
            this.label_info_water_desc.Name = "label_info_water_desc";
            this.label_info_water_desc.Size = new System.Drawing.Size(70, 16);
            this.label_info_water_desc.TabIndex = 39;
            this.label_info_water_desc.Text = "Water";
            this.label_info_water_desc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_info_divinity_desc
            // 
            this.label_info_divinity_desc.BackColor = System.Drawing.Color.Transparent;
            this.label_info_divinity_desc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label_info_divinity_desc.Location = new System.Drawing.Point(10, 364);
            this.label_info_divinity_desc.Name = "label_info_divinity_desc";
            this.label_info_divinity_desc.Size = new System.Drawing.Size(70, 16);
            this.label_info_divinity_desc.TabIndex = 38;
            this.label_info_divinity_desc.Text = "Holy";
            this.label_info_divinity_desc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_info_wind_desc
            // 
            this.label_info_wind_desc.BackColor = System.Drawing.Color.Transparent;
            this.label_info_wind_desc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label_info_wind_desc.Location = new System.Drawing.Point(10, 348);
            this.label_info_wind_desc.Name = "label_info_wind_desc";
            this.label_info_wind_desc.Size = new System.Drawing.Size(70, 16);
            this.label_info_wind_desc.TabIndex = 37;
            this.label_info_wind_desc.Text = "Wind";
            this.label_info_wind_desc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_info_fire_desc
            // 
            this.label_info_fire_desc.BackColor = System.Drawing.Color.Transparent;
            this.label_info_fire_desc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label_info_fire_desc.Location = new System.Drawing.Point(10, 332);
            this.label_info_fire_desc.Name = "label_info_fire_desc";
            this.label_info_fire_desc.Size = new System.Drawing.Size(70, 16);
            this.label_info_fire_desc.TabIndex = 36;
            this.label_info_fire_desc.Text = "Fire";
            this.label_info_fire_desc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_info_atk_attrib_val_desc
            // 
            this.label_info_atk_attrib_val_desc.BackColor = System.Drawing.Color.Transparent;
            this.label_info_atk_attrib_val_desc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label_info_atk_attrib_val_desc.Location = new System.Drawing.Point(132, 306);
            this.label_info_atk_attrib_val_desc.Name = "label_info_atk_attrib_val_desc";
            this.label_info_atk_attrib_val_desc.Size = new System.Drawing.Size(95, 16);
            this.label_info_atk_attrib_val_desc.TabIndex = 35;
            this.label_info_atk_attrib_val_desc.Text = "Atk Attribute Value";
            this.label_info_atk_attrib_val_desc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_info_atk_attrib_descr
            // 
            this.label_info_atk_attrib_descr.BackColor = System.Drawing.Color.Transparent;
            this.label_info_atk_attrib_descr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(163)))), ((int)(((byte)(161)))), ((int)(((byte)(163)))));
            this.label_info_atk_attrib_descr.Location = new System.Drawing.Point(10, 306);
            this.label_info_atk_attrib_descr.Name = "label_info_atk_attrib_descr";
            this.label_info_atk_attrib_descr.Size = new System.Drawing.Size(70, 16);
            this.label_info_atk_attrib_descr.TabIndex = 34;
            this.label_info_atk_attrib_descr.Text = "Atk Attribute";
            this.label_info_atk_attrib_descr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_info_pvp
            // 
            this.label_info_pvp.BackColor = System.Drawing.Color.Transparent;
            this.label_info_pvp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_pvp.Location = new System.Drawing.Point(56, 406);
            this.label_info_pvp.Name = "label_info_pvp";
            this.label_info_pvp.Size = new System.Drawing.Size(72, 16);
            this.label_info_pvp.TabIndex = 33;
            this.label_info_pvp.Text = "0/0";
            this.label_info_pvp.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_info_matkspd
            // 
            this.label_info_matkspd.BackColor = System.Drawing.Color.Transparent;
            this.label_info_matkspd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_matkspd.Location = new System.Drawing.Point(192, 213);
            this.label_info_matkspd.Name = "label_info_matkspd";
            this.label_info_matkspd.Size = new System.Drawing.Size(56, 16);
            this.label_info_matkspd.TabIndex = 31;
            this.label_info_matkspd.Text = "0";
            this.label_info_matkspd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_info_spd
            // 
            this.label_info_spd.BackColor = System.Drawing.Color.Transparent;
            this.label_info_spd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_spd.Location = new System.Drawing.Point(192, 197);
            this.label_info_spd.Name = "label_info_spd";
            this.label_info_spd.Size = new System.Drawing.Size(56, 16);
            this.label_info_spd.TabIndex = 30;
            this.label_info_spd.Text = "0";
            this.label_info_spd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_info_eva
            // 
            this.label_info_eva.BackColor = System.Drawing.Color.Transparent;
            this.label_info_eva.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_eva.Location = new System.Drawing.Point(192, 181);
            this.label_info_eva.Name = "label_info_eva";
            this.label_info_eva.Size = new System.Drawing.Size(56, 16);
            this.label_info_eva.TabIndex = 29;
            this.label_info_eva.Text = "0";
            this.label_info_eva.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_info_mdef
            // 
            this.label_info_mdef.BackColor = System.Drawing.Color.Transparent;
            this.label_info_mdef.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_mdef.Location = new System.Drawing.Point(192, 165);
            this.label_info_mdef.Name = "label_info_mdef";
            this.label_info_mdef.Size = new System.Drawing.Size(56, 16);
            this.label_info_mdef.TabIndex = 28;
            this.label_info_mdef.Text = "0";
            this.label_info_mdef.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel_dead
            // 
            this.panel_dead.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel_dead.BackColor = System.Drawing.SystemColors.Control;
            this.panel_dead.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_dead.Controls.Add(this.button1_close_dead);
            this.panel_dead.Controls.Add(this.button_dead_fort);
            this.panel_dead.Controls.Add(this.label_youdied);
            this.panel_dead.Controls.Add(this.button_dead_siege);
            this.panel_dead.Controls.Add(this.button_dead_clanhall);
            this.panel_dead.Controls.Add(this.button_dead_castle);
            this.panel_dead.Controls.Add(this.button_dead_town);
            this.panel_dead.Location = new System.Drawing.Point(6, 370);
            this.panel_dead.Name = "panel_dead";
            this.panel_dead.Size = new System.Drawing.Size(247, 68);
            this.panel_dead.TabIndex = 24;
            this.panel_dead.Visible = false;
            // 
            // button1_close_dead
            // 
            this.button1_close_dead.Location = new System.Drawing.Point(224, 3);
            this.button1_close_dead.Name = "button1_close_dead";
            this.button1_close_dead.Size = new System.Drawing.Size(18, 23);
            this.button1_close_dead.TabIndex = 6;
            this.button1_close_dead.Text = "x";
            this.button1_close_dead.Click += new System.EventHandler(this.button1_close_dead_Click);
            // 
            // button_dead_fort
            // 
            this.button_dead_fort.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_dead_fort.Location = new System.Drawing.Point(139, 36);
            this.button_dead_fort.Name = "button_dead_fort";
            this.button_dead_fort.Size = new System.Drawing.Size(46, 24);
            this.button_dead_fort.TabIndex = 5;
            this.button_dead_fort.Text = "Fortress";
            this.button_dead_fort.Click += new System.EventHandler(this.button_dead_fort_Click);
            // 
            // label_youdied
            // 
            this.label_youdied.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_youdied.Location = new System.Drawing.Point(5, -2);
            this.label_youdied.Name = "label_youdied";
            this.label_youdied.Size = new System.Drawing.Size(241, 35);
            this.label_youdied.TabIndex = 4;
            this.label_youdied.Text = "You Have Died!";
            this.label_youdied.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_dead_siege
            // 
            this.button_dead_siege.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_dead_siege.Location = new System.Drawing.Point(187, 36);
            this.button_dead_siege.Name = "button_dead_siege";
            this.button_dead_siege.Size = new System.Drawing.Size(57, 24);
            this.button_dead_siege.TabIndex = 3;
            this.button_dead_siege.Text = "Siege HQ";
            this.button_dead_siege.Click += new System.EventHandler(this.button_dead_siege_Click);
            // 
            // button_dead_clanhall
            // 
            this.button_dead_clanhall.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_dead_clanhall.Location = new System.Drawing.Point(84, 36);
            this.button_dead_clanhall.Name = "button_dead_clanhall";
            this.button_dead_clanhall.Size = new System.Drawing.Size(54, 24);
            this.button_dead_clanhall.TabIndex = 2;
            this.button_dead_clanhall.Text = "Clan Hall";
            this.button_dead_clanhall.Click += new System.EventHandler(this.button_dead_clanhall_Click);
            // 
            // button_dead_castle
            // 
            this.button_dead_castle.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_dead_castle.Location = new System.Drawing.Point(43, 36);
            this.button_dead_castle.Name = "button_dead_castle";
            this.button_dead_castle.Size = new System.Drawing.Size(41, 24);
            this.button_dead_castle.TabIndex = 1;
            this.button_dead_castle.Text = "Castle";
            this.button_dead_castle.Click += new System.EventHandler(this.button_dead_castle_Click);
            // 
            // button_dead_town
            // 
            this.button_dead_town.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_dead_town.Location = new System.Drawing.Point(2, 36);
            this.button_dead_town.Name = "button_dead_town";
            this.button_dead_town.Size = new System.Drawing.Size(41, 24);
            this.button_dead_town.TabIndex = 0;
            this.button_dead_town.Text = "Town";
            this.button_dead_town.Click += new System.EventHandler(this.button_dead_town_Click);
            // 
            // panel_yesno
            // 
            this.panel_yesno.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_yesno.Controls.Add(this.button_close_accept);
            this.panel_yesno.Controls.Add(this.label_yesno);
            this.panel_yesno.Controls.Add(this.button_yesno_no);
            this.panel_yesno.Controls.Add(this.button_yesno_yes);
            this.panel_yesno.Location = new System.Drawing.Point(43, 501);
            this.panel_yesno.Name = "panel_yesno";
            this.panel_yesno.Size = new System.Drawing.Size(179, 112);
            this.panel_yesno.TabIndex = 23;
            this.panel_yesno.Visible = false;
            // 
            // button_close_accept
            // 
            this.button_close_accept.Location = new System.Drawing.Point(156, 3);
            this.button_close_accept.Name = "button_close_accept";
            this.button_close_accept.Size = new System.Drawing.Size(18, 23);
            this.button_close_accept.TabIndex = 3;
            this.button_close_accept.Text = "x";
            this.button_close_accept.Click += new System.EventHandler(this.button_close_accept_Click);
            // 
            // label_yesno
            // 
            this.label_yesno.Location = new System.Drawing.Point(-2, 0);
            this.label_yesno.Name = "label_yesno";
            this.label_yesno.Size = new System.Drawing.Size(180, 79);
            this.label_yesno.TabIndex = 2;
            this.label_yesno.Text = "question";
            this.label_yesno.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_yesno_no
            // 
            this.button_yesno_no.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_yesno_no.Location = new System.Drawing.Point(98, 83);
            this.button_yesno_no.Name = "button_yesno_no";
            this.button_yesno_no.Size = new System.Drawing.Size(64, 24);
            this.button_yesno_no.TabIndex = 1;
            this.button_yesno_no.Text = "No";
            this.button_yesno_no.Click += new System.EventHandler(this.button_yesno_no_Click);
            // 
            // button_yesno_yes
            // 
            this.button_yesno_yes.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_yesno_yes.Location = new System.Drawing.Point(14, 83);
            this.button_yesno_yes.Name = "button_yesno_yes";
            this.button_yesno_yes.Size = new System.Drawing.Size(64, 24);
            this.button_yesno_yes.TabIndex = 0;
            this.button_yesno_yes.Text = "Yes";
            this.button_yesno_yes.Click += new System.EventHandler(this.button_yesno_yes_Click);
            // 
            // label_info_matk
            // 
            this.label_info_matk.BackColor = System.Drawing.Color.Transparent;
            this.label_info_matk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_matk.Location = new System.Drawing.Point(192, 149);
            this.label_info_matk.Name = "label_info_matk";
            this.label_info_matk.Size = new System.Drawing.Size(56, 16);
            this.label_info_matk.TabIndex = 27;
            this.label_info_matk.Text = "0";
            this.label_info_matk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_info_atkspd
            // 
            this.label_info_atkspd.BackColor = System.Drawing.Color.Transparent;
            this.label_info_atkspd.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_atkspd.Location = new System.Drawing.Point(72, 213);
            this.label_info_atkspd.Name = "label_info_atkspd";
            this.label_info_atkspd.Size = new System.Drawing.Size(56, 16);
            this.label_info_atkspd.TabIndex = 26;
            this.label_info_atkspd.Text = "0";
            this.label_info_atkspd.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_info_crit
            // 
            this.label_info_crit.BackColor = System.Drawing.Color.Transparent;
            this.label_info_crit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_crit.Location = new System.Drawing.Point(72, 197);
            this.label_info_crit.Name = "label_info_crit";
            this.label_info_crit.Size = new System.Drawing.Size(56, 16);
            this.label_info_crit.TabIndex = 25;
            this.label_info_crit.Text = "0";
            this.label_info_crit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_info_acc
            // 
            this.label_info_acc.BackColor = System.Drawing.Color.Transparent;
            this.label_info_acc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_acc.Location = new System.Drawing.Point(72, 181);
            this.label_info_acc.Name = "label_info_acc";
            this.label_info_acc.Size = new System.Drawing.Size(56, 16);
            this.label_info_acc.TabIndex = 24;
            this.label_info_acc.Text = "0";
            this.label_info_acc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_info_pdef
            // 
            this.label_info_pdef.BackColor = System.Drawing.Color.Transparent;
            this.label_info_pdef.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_pdef.Location = new System.Drawing.Point(72, 165);
            this.label_info_pdef.Name = "label_info_pdef";
            this.label_info_pdef.Size = new System.Drawing.Size(56, 16);
            this.label_info_pdef.TabIndex = 23;
            this.label_info_pdef.Text = "0";
            this.label_info_pdef.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_info_patk
            // 
            this.label_info_patk.BackColor = System.Drawing.Color.Transparent;
            this.label_info_patk.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_patk.Location = new System.Drawing.Point(72, 149);
            this.label_info_patk.Name = "label_info_patk";
            this.label_info_patk.Size = new System.Drawing.Size(56, 16);
            this.label_info_patk.TabIndex = 22;
            this.label_info_patk.Text = "0";
            this.label_info_patk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_info_load
            // 
            this.label_info_load.BackColor = System.Drawing.Color.Transparent;
            this.label_info_load.ForeColor = System.Drawing.Color.White;
            this.label_info_load.Location = new System.Drawing.Point(136, 92);
            this.label_info_load.Name = "label_info_load";
            this.label_info_load.Size = new System.Drawing.Size(120, 16);
            this.label_info_load.TabIndex = 21;
            this.label_info_load.Text = "load";
            this.label_info_load.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_info_karma
            // 
            this.label_info_karma.BackColor = System.Drawing.Color.Transparent;
            this.label_info_karma.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_karma.Location = new System.Drawing.Point(56, 390);
            this.label_info_karma.Name = "label_info_karma";
            this.label_info_karma.Size = new System.Drawing.Size(72, 16);
            this.label_info_karma.TabIndex = 19;
            this.label_info_karma.Text = "karma";
            this.label_info_karma.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_info_men
            // 
            this.label_info_men.BackColor = System.Drawing.Color.Transparent;
            this.label_info_men.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_men.Location = new System.Drawing.Point(208, 282);
            this.label_info_men.Name = "label_info_men";
            this.label_info_men.Size = new System.Drawing.Size(32, 16);
            this.label_info_men.TabIndex = 18;
            this.label_info_men.Text = "MEN";
            this.label_info_men.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_info_wit
            // 
            this.label_info_wit.BackColor = System.Drawing.Color.Transparent;
            this.label_info_wit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_wit.Location = new System.Drawing.Point(128, 282);
            this.label_info_wit.Name = "label_info_wit";
            this.label_info_wit.Size = new System.Drawing.Size(32, 16);
            this.label_info_wit.TabIndex = 17;
            this.label_info_wit.Text = "WIT";
            this.label_info_wit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_info_int
            // 
            this.label_info_int.BackColor = System.Drawing.Color.Transparent;
            this.label_info_int.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_int.Location = new System.Drawing.Point(48, 282);
            this.label_info_int.Name = "label_info_int";
            this.label_info_int.Size = new System.Drawing.Size(32, 16);
            this.label_info_int.TabIndex = 16;
            this.label_info_int.Text = "INT";
            this.label_info_int.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_info_con
            // 
            this.label_info_con.BackColor = System.Drawing.Color.Transparent;
            this.label_info_con.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_con.Location = new System.Drawing.Point(208, 262);
            this.label_info_con.Name = "label_info_con";
            this.label_info_con.Size = new System.Drawing.Size(32, 16);
            this.label_info_con.TabIndex = 15;
            this.label_info_con.Text = "CON";
            this.label_info_con.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_info_dex
            // 
            this.label_info_dex.BackColor = System.Drawing.Color.Transparent;
            this.label_info_dex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_dex.Location = new System.Drawing.Point(128, 262);
            this.label_info_dex.Name = "label_info_dex";
            this.label_info_dex.Size = new System.Drawing.Size(32, 16);
            this.label_info_dex.TabIndex = 14;
            this.label_info_dex.Text = "DEX";
            this.label_info_dex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_info_str
            // 
            this.label_info_str.BackColor = System.Drawing.Color.Transparent;
            this.label_info_str.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_str.Location = new System.Drawing.Point(48, 262);
            this.label_info_str.Name = "label_info_str";
            this.label_info_str.Size = new System.Drawing.Size(32, 16);
            this.label_info_str.TabIndex = 13;
            this.label_info_str.Text = "STR";
            this.label_info_str.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_info_level
            // 
            this.label_info_level.BackColor = System.Drawing.Color.Transparent;
            this.label_info_level.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_level.Location = new System.Drawing.Point(40, 62);
            this.label_info_level.Name = "label_info_level";
            this.label_info_level.Size = new System.Drawing.Size(24, 16);
            this.label_info_level.TabIndex = 12;
            this.label_info_level.Text = "lvl";
            this.label_info_level.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_info_sp
            // 
            this.label_info_sp.BackColor = System.Drawing.Color.Transparent;
            this.label_info_sp.ForeColor = System.Drawing.Color.White;
            this.label_info_sp.Location = new System.Drawing.Point(136, 106);
            this.label_info_sp.Name = "label_info_sp";
            this.label_info_sp.Size = new System.Drawing.Size(120, 16);
            this.label_info_sp.TabIndex = 11;
            this.label_info_sp.Text = "sp";
            this.label_info_sp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_info_xp
            // 
            this.label_info_xp.BackColor = System.Drawing.Color.Transparent;
            this.label_info_xp.ForeColor = System.Drawing.Color.White;
            this.label_info_xp.Location = new System.Drawing.Point(24, 104);
            this.label_info_xp.Name = "label_info_xp";
            this.label_info_xp.Size = new System.Drawing.Size(112, 16);
            this.label_info_xp.TabIndex = 10;
            this.label_info_xp.Text = "xp";
            this.label_info_xp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_info_cp
            // 
            this.label_info_cp.BackColor = System.Drawing.Color.Transparent;
            this.label_info_cp.ForeColor = System.Drawing.Color.White;
            this.label_info_cp.Location = new System.Drawing.Point(136, 76);
            this.label_info_cp.Name = "label_info_cp";
            this.label_info_cp.Size = new System.Drawing.Size(120, 16);
            this.label_info_cp.TabIndex = 9;
            this.label_info_cp.Text = "cp";
            this.label_info_cp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_info_mp
            // 
            this.label_info_mp.BackColor = System.Drawing.Color.Transparent;
            this.label_info_mp.ForeColor = System.Drawing.Color.White;
            this.label_info_mp.Location = new System.Drawing.Point(24, 90);
            this.label_info_mp.Name = "label_info_mp";
            this.label_info_mp.Size = new System.Drawing.Size(112, 16);
            this.label_info_mp.TabIndex = 8;
            this.label_info_mp.Text = "mp";
            this.label_info_mp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_info_hp
            // 
            this.label_info_hp.BackColor = System.Drawing.Color.Transparent;
            this.label_info_hp.ForeColor = System.Drawing.Color.White;
            this.label_info_hp.Location = new System.Drawing.Point(32, 76);
            this.label_info_hp.Name = "label_info_hp";
            this.label_info_hp.Size = new System.Drawing.Size(96, 16);
            this.label_info_hp.TabIndex = 7;
            this.label_info_hp.Text = "hp";
            this.label_info_hp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_info_title
            // 
            this.label_info_title.BackColor = System.Drawing.Color.Transparent;
            this.label_info_title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_title.Location = new System.Drawing.Point(152, 48);
            this.label_info_title.Name = "label_info_title";
            this.label_info_title.Size = new System.Drawing.Size(80, 16);
            this.label_info_title.TabIndex = 6;
            this.label_info_title.Text = "title";
            this.label_info_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_info_name
            // 
            this.label_info_name.BackColor = System.Drawing.Color.Transparent;
            this.label_info_name.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(174)))), ((int)(((byte)(151)))), ((int)(((byte)(121)))));
            this.label_info_name.Location = new System.Drawing.Point(32, 24);
            this.label_info_name.Name = "label_info_name";
            this.label_info_name.Size = new System.Drawing.Size(176, 16);
            this.label_info_name.TabIndex = 5;
            this.label_info_name.Text = "Name";
            this.label_info_name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage_char_inv
            // 
            this.tabPage_char_inv.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("tabPage_char_inv.BackgroundImage")));
            this.tabPage_char_inv.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.tabPage_char_inv.Controls.Add(this.radioButton_inv_quest);
            this.tabPage_char_inv.Controls.Add(this.radioButton_inv_equipped);
            this.tabPage_char_inv.Controls.Add(this.radioButton_inv_items);
            this.tabPage_char_inv.Controls.Add(this.label_inventory_count);
            this.tabPage_char_inv.Controls.Add(this.panel_inven_shirt);
            this.tabPage_char_inv.Controls.Add(this.panel_tat3);
            this.tabPage_char_inv.Controls.Add(this.panel_tat2);
            this.tabPage_char_inv.Controls.Add(this.panel_tat1);
            this.tabPage_char_inv.Controls.Add(this.panel_inven_rfinger);
            this.tabPage_char_inv.Controls.Add(this.panel_inven_lfinger);
            this.tabPage_char_inv.Controls.Add(this.panel_inven_rear);
            this.tabPage_char_inv.Controls.Add(this.panel_inven_lear);
            this.tabPage_char_inv.Controls.Add(this.panel_inven_neck);
            this.tabPage_char_inv.Controls.Add(this.panel_inven_acc);
            this.tabPage_char_inv.Controls.Add(this.panel_inven_boots);
            this.tabPage_char_inv.Controls.Add(this.panel_inven_gloves);
            this.tabPage_char_inv.Controls.Add(this.panel_inven_lhand);
            this.tabPage_char_inv.Controls.Add(this.panel_inven_pants);
            this.tabPage_char_inv.Controls.Add(this.panel_inven_top);
            this.tabPage_char_inv.Controls.Add(this.panel_inven_rhand);
            this.tabPage_char_inv.Controls.Add(this.panel_inven_head);
            this.tabPage_char_inv.Controls.Add(this.listView_inventory);
            this.tabPage_char_inv.Location = new System.Drawing.Point(4, 58);
            this.tabPage_char_inv.Name = "tabPage_char_inv";
            this.tabPage_char_inv.Size = new System.Drawing.Size(256, 576);
            this.tabPage_char_inv.TabIndex = 1;
            this.tabPage_char_inv.Text = "Inv";
            this.tabPage_char_inv.UseVisualStyleBackColor = true;
            // 
            // radioButton_inv_quest
            // 
            this.radioButton_inv_quest.AutoSize = true;
            this.radioButton_inv_quest.Location = new System.Drawing.Point(194, 155);
            this.radioButton_inv_quest.Name = "radioButton_inv_quest";
            this.radioButton_inv_quest.Size = new System.Drawing.Size(52, 17);
            this.radioButton_inv_quest.TabIndex = 21;
            this.radioButton_inv_quest.Text = "Quest";
            this.radioButton_inv_quest.UseVisualStyleBackColor = true;
            this.radioButton_inv_quest.CheckedChanged += new System.EventHandler(this.radioButton_inv_quest_CheckedChanged);
            // 
            // radioButton_inv_equipped
            // 
            this.radioButton_inv_equipped.AutoSize = true;
            this.radioButton_inv_equipped.Location = new System.Drawing.Point(92, 155);
            this.radioButton_inv_equipped.Name = "radioButton_inv_equipped";
            this.radioButton_inv_equipped.Size = new System.Drawing.Size(69, 17);
            this.radioButton_inv_equipped.TabIndex = 20;
            this.radioButton_inv_equipped.Text = "Equipped";
            this.radioButton_inv_equipped.UseVisualStyleBackColor = true;
            this.radioButton_inv_equipped.CheckedChanged += new System.EventHandler(this.radioButton_inv_equipped_CheckedChanged);
            // 
            // radioButton_inv_items
            // 
            this.radioButton_inv_items.AutoSize = true;
            this.radioButton_inv_items.Checked = true;
            this.radioButton_inv_items.Location = new System.Drawing.Point(14, 155);
            this.radioButton_inv_items.Name = "radioButton_inv_items";
            this.radioButton_inv_items.Size = new System.Drawing.Size(49, 17);
            this.radioButton_inv_items.TabIndex = 19;
            this.radioButton_inv_items.TabStop = true;
            this.radioButton_inv_items.Text = "Items";
            this.radioButton_inv_items.UseVisualStyleBackColor = true;
            this.radioButton_inv_items.CheckedChanged += new System.EventHandler(this.radioButton_inv_items_CheckedChanged);
            // 
            // label_inventory_count
            // 
            this.label_inventory_count.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_inventory_count.Location = new System.Drawing.Point(78, 519);
            this.label_inventory_count.Name = "label_inventory_count";
            this.label_inventory_count.Size = new System.Drawing.Size(100, 23);
            this.label_inventory_count.TabIndex = 18;
            this.label_inventory_count.Text = "-/-";
            this.label_inventory_count.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel_inven_shirt
            // 
            this.panel_inven_shirt.BackColor = System.Drawing.Color.Transparent;
            this.panel_inven_shirt.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_inven_shirt.Location = new System.Drawing.Point(14, 37);
            this.panel_inven_shirt.Name = "panel_inven_shirt";
            this.panel_inven_shirt.Size = new System.Drawing.Size(32, 32);
            this.panel_inven_shirt.TabIndex = 17;
            // 
            // panel_tat3
            // 
            this.panel_tat3.BackColor = System.Drawing.Color.Transparent;
            this.panel_tat3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_tat3.Location = new System.Drawing.Point(223, 94);
            this.panel_tat3.Name = "panel_tat3";
            this.panel_tat3.Size = new System.Drawing.Size(24, 24);
            this.panel_tat3.TabIndex = 16;
            // 
            // panel_tat2
            // 
            this.panel_tat2.BackColor = System.Drawing.Color.Transparent;
            this.panel_tat2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_tat2.Location = new System.Drawing.Point(223, 67);
            this.panel_tat2.Name = "panel_tat2";
            this.panel_tat2.Size = new System.Drawing.Size(24, 24);
            this.panel_tat2.TabIndex = 15;
            // 
            // panel_tat1
            // 
            this.panel_tat1.BackColor = System.Drawing.Color.Transparent;
            this.panel_tat1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_tat1.Location = new System.Drawing.Point(223, 40);
            this.panel_tat1.Name = "panel_tat1";
            this.panel_tat1.Size = new System.Drawing.Size(24, 24);
            this.panel_tat1.TabIndex = 14;
            // 
            // panel_inven_rfinger
            // 
            this.panel_inven_rfinger.BackColor = System.Drawing.Color.Transparent;
            this.panel_inven_rfinger.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_inven_rfinger.Location = new System.Drawing.Point(176, 113);
            this.panel_inven_rfinger.Name = "panel_inven_rfinger";
            this.panel_inven_rfinger.Size = new System.Drawing.Size(32, 32);
            this.panel_inven_rfinger.TabIndex = 13;
            // 
            // panel_inven_lfinger
            // 
            this.panel_inven_lfinger.BackColor = System.Drawing.Color.Transparent;
            this.panel_inven_lfinger.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_inven_lfinger.Location = new System.Drawing.Point(137, 113);
            this.panel_inven_lfinger.Name = "panel_inven_lfinger";
            this.panel_inven_lfinger.Size = new System.Drawing.Size(32, 32);
            this.panel_inven_lfinger.TabIndex = 12;
            // 
            // panel_inven_rear
            // 
            this.panel_inven_rear.BackColor = System.Drawing.Color.Transparent;
            this.panel_inven_rear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_inven_rear.Location = new System.Drawing.Point(176, 75);
            this.panel_inven_rear.Name = "panel_inven_rear";
            this.panel_inven_rear.Size = new System.Drawing.Size(32, 32);
            this.panel_inven_rear.TabIndex = 11;
            // 
            // panel_inven_lear
            // 
            this.panel_inven_lear.BackColor = System.Drawing.Color.Transparent;
            this.panel_inven_lear.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_inven_lear.Location = new System.Drawing.Point(137, 75);
            this.panel_inven_lear.Name = "panel_inven_lear";
            this.panel_inven_lear.Size = new System.Drawing.Size(32, 32);
            this.panel_inven_lear.TabIndex = 10;
            // 
            // panel_inven_neck
            // 
            this.panel_inven_neck.BackColor = System.Drawing.Color.Transparent;
            this.panel_inven_neck.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_inven_neck.Location = new System.Drawing.Point(176, 37);
            this.panel_inven_neck.Name = "panel_inven_neck";
            this.panel_inven_neck.Size = new System.Drawing.Size(32, 32);
            this.panel_inven_neck.TabIndex = 9;
            // 
            // panel_inven_acc
            // 
            this.panel_inven_acc.BackColor = System.Drawing.Color.Transparent;
            this.panel_inven_acc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_inven_acc.Location = new System.Drawing.Point(137, 37);
            this.panel_inven_acc.Name = "panel_inven_acc";
            this.panel_inven_acc.Size = new System.Drawing.Size(32, 32);
            this.panel_inven_acc.TabIndex = 8;
            // 
            // panel_inven_boots
            // 
            this.panel_inven_boots.BackColor = System.Drawing.Color.Transparent;
            this.panel_inven_boots.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_inven_boots.Location = new System.Drawing.Point(92, 113);
            this.panel_inven_boots.Name = "panel_inven_boots";
            this.panel_inven_boots.Size = new System.Drawing.Size(32, 32);
            this.panel_inven_boots.TabIndex = 7;
            // 
            // panel_inven_gloves
            // 
            this.panel_inven_gloves.BackColor = System.Drawing.Color.Transparent;
            this.panel_inven_gloves.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_inven_gloves.Location = new System.Drawing.Point(14, 113);
            this.panel_inven_gloves.Name = "panel_inven_gloves";
            this.panel_inven_gloves.Size = new System.Drawing.Size(32, 32);
            this.panel_inven_gloves.TabIndex = 6;
            // 
            // panel_inven_lhand
            // 
            this.panel_inven_lhand.BackColor = System.Drawing.Color.Transparent;
            this.panel_inven_lhand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_inven_lhand.Location = new System.Drawing.Point(92, 75);
            this.panel_inven_lhand.Name = "panel_inven_lhand";
            this.panel_inven_lhand.Size = new System.Drawing.Size(32, 32);
            this.panel_inven_lhand.TabIndex = 5;
            // 
            // panel_inven_pants
            // 
            this.panel_inven_pants.BackColor = System.Drawing.Color.Transparent;
            this.panel_inven_pants.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_inven_pants.Location = new System.Drawing.Point(53, 113);
            this.panel_inven_pants.Name = "panel_inven_pants";
            this.panel_inven_pants.Size = new System.Drawing.Size(32, 32);
            this.panel_inven_pants.TabIndex = 4;
            // 
            // panel_inven_top
            // 
            this.panel_inven_top.BackColor = System.Drawing.Color.Transparent;
            this.panel_inven_top.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_inven_top.Location = new System.Drawing.Point(53, 75);
            this.panel_inven_top.Name = "panel_inven_top";
            this.panel_inven_top.Size = new System.Drawing.Size(32, 32);
            this.panel_inven_top.TabIndex = 3;
            // 
            // panel_inven_rhand
            // 
            this.panel_inven_rhand.BackColor = System.Drawing.Color.Transparent;
            this.panel_inven_rhand.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_inven_rhand.Location = new System.Drawing.Point(14, 75);
            this.panel_inven_rhand.Name = "panel_inven_rhand";
            this.panel_inven_rhand.Size = new System.Drawing.Size(32, 32);
            this.panel_inven_rhand.TabIndex = 2;
            // 
            // panel_inven_head
            // 
            this.panel_inven_head.BackColor = System.Drawing.Color.Transparent;
            this.panel_inven_head.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_inven_head.Location = new System.Drawing.Point(53, 37);
            this.panel_inven_head.Name = "panel_inven_head";
            this.panel_inven_head.Size = new System.Drawing.Size(32, 32);
            this.panel_inven_head.TabIndex = 1;
            // 
            // listView_inventory
            // 
            this.listView_inventory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_inventory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader169,
            this.columnHeader170,
            this.columnHeader171,
            this.columnHeader172,
            this.columnHeader173});
            this.listView_inventory.ContextMenuStrip = this.contextMenuStrip_inventory;
            this.listView_inventory.FullRowSelect = true;
            this.listView_inventory.GridLines = true;
            this.listView_inventory.Location = new System.Drawing.Point(3, 172);
            this.listView_inventory.MultiSelect = false;
            this.listView_inventory.Name = "listView_inventory";
            this.listView_inventory.Size = new System.Drawing.Size(253, 341);
            this.listView_inventory.SmallImageList = this.imageList_items;
            this.listView_inventory.TabIndex = 0;
            this.listView_inventory.UseCompatibleStateImageBehavior = false;
            this.listView_inventory.View = System.Windows.Forms.View.Details;
            this.listView_inventory.VirtualMode = true;
            // 
            // columnHeader169
            // 
            this.columnHeader169.Text = "Item";
            this.columnHeader169.Width = 46;
            // 
            // columnHeader170
            // 
            this.columnHeader170.Text = "Count";
            this.columnHeader170.Width = 49;
            // 
            // columnHeader171
            // 
            this.columnHeader171.Text = "Equipped";
            // 
            // columnHeader172
            // 
            this.columnHeader172.Text = "Slot";
            this.columnHeader172.Width = 39;
            // 
            // columnHeader173
            // 
            this.columnHeader173.Text = "ObjID";
            // 
            // contextMenuStrip_inventory
            // 
            this.contextMenuStrip_inventory.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dropStackToolStripMenuItem,
            this.deleteStackToolStripMenuItem,
            this.crystalizeToolStripMenuItem,
            this.addTodoNotListToolStripMenuItem1});
            this.contextMenuStrip_inventory.Name = "contextMenuStrip_inventory";
            this.contextMenuStrip_inventory.Size = new System.Drawing.Size(180, 92);
            // 
            // dropStackToolStripMenuItem
            // 
            this.dropStackToolStripMenuItem.Name = "dropStackToolStripMenuItem";
            this.dropStackToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.dropStackToolStripMenuItem.Text = "Drop Stack";
            this.dropStackToolStripMenuItem.Click += new System.EventHandler(this.dropStackToolStripMenuItem_Click);
            // 
            // deleteStackToolStripMenuItem
            // 
            this.deleteStackToolStripMenuItem.Name = "deleteStackToolStripMenuItem";
            this.deleteStackToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.deleteStackToolStripMenuItem.Text = "Delete Stack";
            this.deleteStackToolStripMenuItem.Click += new System.EventHandler(this.deleteStackToolStripMenuItem_Click);
            // 
            // crystalizeToolStripMenuItem
            // 
            this.crystalizeToolStripMenuItem.Name = "crystalizeToolStripMenuItem";
            this.crystalizeToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.crystalizeToolStripMenuItem.Text = "Crystalize";
            this.crystalizeToolStripMenuItem.Click += new System.EventHandler(this.crystalizeToolStripMenuItem_Click);
            // 
            // addTodoNotListToolStripMenuItem1
            // 
            this.addTodoNotListToolStripMenuItem1.Name = "addTodoNotListToolStripMenuItem1";
            this.addTodoNotListToolStripMenuItem1.Size = new System.Drawing.Size(179, 22);
            this.addTodoNotListToolStripMenuItem1.Text = "Add to \"Do Not\" list";
            this.addTodoNotListToolStripMenuItem1.Click += new System.EventHandler(this.addTodoNotListToolStripMenuItem1_Click);
            // 
            // imageList_items
            // 
            this.imageList_items.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList_items.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList_items.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tabPage_char_skills
            // 
            this.tabPage_char_skills.Controls.Add(this.radiobutton_passive);
            this.tabPage_char_skills.Controls.Add(this.radiobutton_active);
            this.tabPage_char_skills.Controls.Add(this.listView_skills);
            this.tabPage_char_skills.Location = new System.Drawing.Point(4, 58);
            this.tabPage_char_skills.Name = "tabPage_char_skills";
            this.tabPage_char_skills.Size = new System.Drawing.Size(256, 576);
            this.tabPage_char_skills.TabIndex = 2;
            this.tabPage_char_skills.Text = "Skills";
            this.tabPage_char_skills.UseVisualStyleBackColor = true;
            // 
            // radiobutton_passive
            // 
            this.radiobutton_passive.AutoSize = true;
            this.radiobutton_passive.Location = new System.Drawing.Point(134, 4);
            this.radiobutton_passive.Name = "radiobutton_passive";
            this.radiobutton_passive.Size = new System.Drawing.Size(61, 17);
            this.radiobutton_passive.TabIndex = 3;
            this.radiobutton_passive.Text = "Passive";
            this.radiobutton_passive.UseVisualStyleBackColor = true;
            // 
            // radiobutton_active
            // 
            this.radiobutton_active.AutoSize = true;
            this.radiobutton_active.Checked = true;
            this.radiobutton_active.Location = new System.Drawing.Point(43, 4);
            this.radiobutton_active.Name = "radiobutton_active";
            this.radiobutton_active.Size = new System.Drawing.Size(54, 17);
            this.radiobutton_active.TabIndex = 2;
            this.radiobutton_active.TabStop = true;
            this.radiobutton_active.Text = "Active";
            this.radiobutton_active.UseVisualStyleBackColor = true;
            this.radiobutton_active.CheckedChanged += new System.EventHandler(this.radiobutton_active_CheckedChanged);
            // 
            // listView_skills
            // 
            this.listView_skills.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_skills.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader177,
            this.columnHeader178,
            this.columnHeader180});
            this.listView_skills.FullRowSelect = true;
            this.listView_skills.GridLines = true;
            this.listView_skills.Location = new System.Drawing.Point(3, 24);
            this.listView_skills.MultiSelect = false;
            this.listView_skills.Name = "listView_skills";
            this.listView_skills.Size = new System.Drawing.Size(253, 538);
            this.listView_skills.SmallImageList = this.imageList_skills;
            this.listView_skills.TabIndex = 1;
            this.listView_skills.UseCompatibleStateImageBehavior = false;
            this.listView_skills.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader177
            // 
            this.columnHeader177.Text = "Name";
            // 
            // columnHeader178
            // 
            this.columnHeader178.Text = "Level";
            // 
            // columnHeader180
            // 
            this.columnHeader180.Text = "ObjID";
            // 
            // imageList_skills
            // 
            this.imageList_skills.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList_skills.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList_skills.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tabPage_char_clan
            // 
            this.tabPage_char_clan.Controls.Add(this.label_clan_castle_text);
            this.tabPage_char_clan.Controls.Add(this.label_clan_hall_text);
            this.tabPage_char_clan.Controls.Add(this.label_clan_war_text);
            this.tabPage_char_clan.Controls.Add(this.label_clan_rep_text);
            this.tabPage_char_clan.Controls.Add(this.label_clan_lvl_text);
            this.tabPage_char_clan.Controls.Add(this.label_clan_ally_text);
            this.tabPage_char_clan.Controls.Add(this.label_clan_name_text);
            this.tabPage_char_clan.Controls.Add(this.label_clan_leader_text);
            this.tabPage_char_clan.Controls.Add(this.label_clan_rep);
            this.tabPage_char_clan.Controls.Add(this.pictureBox_clan_crest);
            this.tabPage_char_clan.Controls.Add(this.label_caln_ally);
            this.tabPage_char_clan.Controls.Add(this.label_clan_castle);
            this.tabPage_char_clan.Controls.Add(this.label_clan_hall);
            this.tabPage_char_clan.Controls.Add(this.label_clan_war);
            this.tabPage_char_clan.Controls.Add(this.label_clan_level);
            this.tabPage_char_clan.Controls.Add(this.label_clan_name);
            this.tabPage_char_clan.Controls.Add(this.label_clan_leader);
            this.tabPage_char_clan.Controls.Add(this.label_clan_online);
            this.tabPage_char_clan.Controls.Add(this.listView_char_clan);
            this.tabPage_char_clan.Location = new System.Drawing.Point(4, 58);
            this.tabPage_char_clan.Name = "tabPage_char_clan";
            this.tabPage_char_clan.Size = new System.Drawing.Size(256, 576);
            this.tabPage_char_clan.TabIndex = 3;
            this.tabPage_char_clan.Text = "Clan";
            this.tabPage_char_clan.UseVisualStyleBackColor = true;
            // 
            // label_clan_castle_text
            // 
            this.label_clan_castle_text.Location = new System.Drawing.Point(3, 119);
            this.label_clan_castle_text.Name = "label_clan_castle_text";
            this.label_clan_castle_text.Size = new System.Drawing.Size(45, 16);
            this.label_clan_castle_text.TabIndex = 18;
            this.label_clan_castle_text.Text = "Castle:";
            // 
            // label_clan_hall_text
            // 
            this.label_clan_hall_text.Location = new System.Drawing.Point(3, 105);
            this.label_clan_hall_text.Name = "label_clan_hall_text";
            this.label_clan_hall_text.Size = new System.Drawing.Size(45, 16);
            this.label_clan_hall_text.TabIndex = 17;
            this.label_clan_hall_text.Text = "Hall:";
            // 
            // label_clan_war_text
            // 
            this.label_clan_war_text.Location = new System.Drawing.Point(3, 89);
            this.label_clan_war_text.Name = "label_clan_war_text";
            this.label_clan_war_text.Size = new System.Drawing.Size(45, 16);
            this.label_clan_war_text.TabIndex = 16;
            this.label_clan_war_text.Text = "War:";
            // 
            // label_clan_rep_text
            // 
            this.label_clan_rep_text.Location = new System.Drawing.Point(3, 72);
            this.label_clan_rep_text.Name = "label_clan_rep_text";
            this.label_clan_rep_text.Size = new System.Drawing.Size(45, 16);
            this.label_clan_rep_text.TabIndex = 15;
            this.label_clan_rep_text.Text = "Rep:";
            // 
            // label_clan_lvl_text
            // 
            this.label_clan_lvl_text.Location = new System.Drawing.Point(3, 56);
            this.label_clan_lvl_text.Name = "label_clan_lvl_text";
            this.label_clan_lvl_text.Size = new System.Drawing.Size(45, 16);
            this.label_clan_lvl_text.TabIndex = 14;
            this.label_clan_lvl_text.Text = "Lvl:";
            // 
            // label_clan_ally_text
            // 
            this.label_clan_ally_text.Location = new System.Drawing.Point(3, 40);
            this.label_clan_ally_text.Name = "label_clan_ally_text";
            this.label_clan_ally_text.Size = new System.Drawing.Size(45, 16);
            this.label_clan_ally_text.TabIndex = 13;
            this.label_clan_ally_text.Text = "Ally:";
            // 
            // label_clan_name_text
            // 
            this.label_clan_name_text.Location = new System.Drawing.Point(3, 8);
            this.label_clan_name_text.Name = "label_clan_name_text";
            this.label_clan_name_text.Size = new System.Drawing.Size(45, 16);
            this.label_clan_name_text.TabIndex = 12;
            this.label_clan_name_text.Text = "Name:";
            // 
            // label_clan_leader_text
            // 
            this.label_clan_leader_text.Location = new System.Drawing.Point(3, 24);
            this.label_clan_leader_text.Name = "label_clan_leader_text";
            this.label_clan_leader_text.Size = new System.Drawing.Size(45, 16);
            this.label_clan_leader_text.TabIndex = 11;
            this.label_clan_leader_text.Text = "Leader:";
            // 
            // label_clan_rep
            // 
            this.label_clan_rep.Location = new System.Drawing.Point(50, 72);
            this.label_clan_rep.Name = "label_clan_rep";
            this.label_clan_rep.Size = new System.Drawing.Size(48, 16);
            this.label_clan_rep.TabIndex = 10;
            this.label_clan_rep.Text = "0";
            // 
            // pictureBox_clan_crest
            // 
            this.pictureBox_clan_crest.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox_clan_crest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pictureBox_clan_crest.Location = new System.Drawing.Point(216, 8);
            this.pictureBox_clan_crest.Name = "pictureBox_clan_crest";
            this.pictureBox_clan_crest.Size = new System.Drawing.Size(16, 16);
            this.pictureBox_clan_crest.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox_clan_crest.TabIndex = 9;
            this.pictureBox_clan_crest.TabStop = false;
            // 
            // label_caln_ally
            // 
            this.label_caln_ally.Location = new System.Drawing.Point(50, 40);
            this.label_caln_ally.Name = "label_caln_ally";
            this.label_caln_ally.Size = new System.Drawing.Size(88, 16);
            this.label_caln_ally.TabIndex = 8;
            this.label_caln_ally.Text = "ally";
            // 
            // label_clan_castle
            // 
            this.label_clan_castle.Location = new System.Drawing.Point(50, 119);
            this.label_clan_castle.Name = "label_clan_castle";
            this.label_clan_castle.Size = new System.Drawing.Size(48, 16);
            this.label_clan_castle.TabIndex = 7;
            this.label_clan_castle.Text = "castle";
            // 
            // label_clan_hall
            // 
            this.label_clan_hall.Location = new System.Drawing.Point(50, 105);
            this.label_clan_hall.Name = "label_clan_hall";
            this.label_clan_hall.Size = new System.Drawing.Size(48, 16);
            this.label_clan_hall.TabIndex = 6;
            this.label_clan_hall.Text = "hall";
            // 
            // label_clan_war
            // 
            this.label_clan_war.Location = new System.Drawing.Point(50, 88);
            this.label_clan_war.Name = "label_clan_war";
            this.label_clan_war.Size = new System.Drawing.Size(88, 16);
            this.label_clan_war.TabIndex = 5;
            this.label_clan_war.Text = "war";
            // 
            // label_clan_level
            // 
            this.label_clan_level.Location = new System.Drawing.Point(50, 56);
            this.label_clan_level.Name = "label_clan_level";
            this.label_clan_level.Size = new System.Drawing.Size(48, 16);
            this.label_clan_level.TabIndex = 4;
            this.label_clan_level.Text = "lvl";
            // 
            // label_clan_name
            // 
            this.label_clan_name.Location = new System.Drawing.Point(50, 8);
            this.label_clan_name.Name = "label_clan_name";
            this.label_clan_name.Size = new System.Drawing.Size(88, 16);
            this.label_clan_name.TabIndex = 3;
            this.label_clan_name.Text = "name";
            // 
            // label_clan_leader
            // 
            this.label_clan_leader.Location = new System.Drawing.Point(50, 24);
            this.label_clan_leader.Name = "label_clan_leader";
            this.label_clan_leader.Size = new System.Drawing.Size(88, 16);
            this.label_clan_leader.TabIndex = 2;
            this.label_clan_leader.Text = "leader";
            // 
            // label_clan_online
            // 
            this.label_clan_online.Location = new System.Drawing.Point(104, 117);
            this.label_clan_online.Name = "label_clan_online";
            this.label_clan_online.Size = new System.Drawing.Size(142, 16);
            this.label_clan_online.TabIndex = 1;
            this.label_clan_online.Text = "Members Online: 0/0";
            this.label_clan_online.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // listView_char_clan
            // 
            this.listView_char_clan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_char_clan.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader191,
            this.columnHeader192,
            this.columnHeader193,
            this.columnHeader194});
            this.listView_char_clan.FullRowSelect = true;
            this.listView_char_clan.GridLines = true;
            this.listView_char_clan.LargeImageList = this.imageList_crests;
            this.listView_char_clan.Location = new System.Drawing.Point(3, 141);
            this.listView_char_clan.Name = "listView_char_clan";
            this.listView_char_clan.Size = new System.Drawing.Size(253, 421);
            this.listView_char_clan.TabIndex = 0;
            this.listView_char_clan.UseCompatibleStateImageBehavior = false;
            this.listView_char_clan.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader191
            // 
            this.columnHeader191.Text = "Name";
            this.columnHeader191.Width = 78;
            // 
            // columnHeader192
            // 
            this.columnHeader192.Text = "Level";
            this.columnHeader192.Width = 33;
            // 
            // columnHeader193
            // 
            this.columnHeader193.Text = "Class";
            this.columnHeader193.Width = 68;
            // 
            // columnHeader194
            // 
            this.columnHeader194.Text = "Online";
            this.columnHeader194.Width = 35;
            // 
            // imageList_crests
            // 
            this.imageList_crests.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList_crests.ImageSize = new System.Drawing.Size(16, 12);
            this.imageList_crests.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // tabPage_char_detail
            // 
            this.tabPage_char_detail.Controls.Add(this.listView_char_data);
            this.tabPage_char_detail.Location = new System.Drawing.Point(4, 58);
            this.tabPage_char_detail.Name = "tabPage_char_detail";
            this.tabPage_char_detail.Size = new System.Drawing.Size(256, 576);
            this.tabPage_char_detail.TabIndex = 5;
            this.tabPage_char_detail.Text = "Char Detail";
            this.tabPage_char_detail.UseVisualStyleBackColor = true;
            // 
            // listView_char_data
            // 
            this.listView_char_data.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_char_data.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView_char_data.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader103,
            this.columnHeader104});
            this.listView_char_data.FullRowSelect = true;
            this.listView_char_data.GridLines = true;
            this.listView_char_data.Location = new System.Drawing.Point(3, 3);
            this.listView_char_data.MultiSelect = false;
            this.listView_char_data.Name = "listView_char_data";
            this.listView_char_data.Size = new System.Drawing.Size(253, 559);
            this.listView_char_data.TabIndex = 0;
            this.listView_char_data.UseCompatibleStateImageBehavior = false;
            this.listView_char_data.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader103
            // 
            this.columnHeader103.Text = "Trait";
            this.columnHeader103.Width = 71;
            // 
            // columnHeader104
            // 
            this.columnHeader104.Text = "Value";
            this.columnHeader104.Width = 120;
            // 
            // tabPage_players
            // 
            this.tabPage_players.Controls.Add(this.listView_players_data);
            this.tabPage_players.Location = new System.Drawing.Point(4, 58);
            this.tabPage_players.Name = "tabPage_players";
            this.tabPage_players.Size = new System.Drawing.Size(256, 576);
            this.tabPage_players.TabIndex = 6;
            this.tabPage_players.Text = "Players";
            this.tabPage_players.UseVisualStyleBackColor = true;
            // 
            // listView_players_data
            // 
            this.listView_players_data.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_players_data.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView_players_data.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader80,
            this.columnHeader81,
            this.columnHeader82,
            this.columnHeader83,
            this.columnHeader84,
            this.columnHeader85});
            this.listView_players_data.FullRowSelect = true;
            this.listView_players_data.GridLines = true;
            this.listView_players_data.Location = new System.Drawing.Point(3, 3);
            this.listView_players_data.MultiSelect = false;
            this.listView_players_data.Name = "listView_players_data";
            this.listView_players_data.Size = new System.Drawing.Size(253, 595);
            this.listView_players_data.SmallImageList = this.imageList_crests;
            this.listView_players_data.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView_players_data.TabIndex = 0;
            this.listView_players_data.UseCompatibleStateImageBehavior = false;
            this.listView_players_data.View = System.Windows.Forms.View.Details;
            this.listView_players_data.VirtualMode = true;
            // 
            // columnHeader80
            // 
            this.columnHeader80.Text = "War";
            // 
            // columnHeader81
            // 
            this.columnHeader81.Text = "Name";
            // 
            // columnHeader82
            // 
            this.columnHeader82.Text = "Class";
            // 
            // columnHeader83
            // 
            this.columnHeader83.Text = "Clan";
            // 
            // columnHeader84
            // 
            this.columnHeader84.Text = "Ally";
            // 
            // columnHeader85
            // 
            this.columnHeader85.Text = "ObjID";
            // 
            // tabPage_items
            // 
            this.tabPage_items.Controls.Add(this.listView_items_data);
            this.tabPage_items.Location = new System.Drawing.Point(4, 58);
            this.tabPage_items.Name = "tabPage_items";
            this.tabPage_items.Size = new System.Drawing.Size(256, 576);
            this.tabPage_items.TabIndex = 7;
            this.tabPage_items.Text = "Items";
            this.tabPage_items.UseVisualStyleBackColor = true;
            // 
            // listView_items_data
            // 
            this.listView_items_data.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_items_data.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView_items_data.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader130,
            this.columnHeader131,
            this.columnHeader132});
            this.listView_items_data.ContextMenuStrip = this.contextMenuStrip_Items;
            this.listView_items_data.FullRowSelect = true;
            this.listView_items_data.GridLines = true;
            this.listView_items_data.Location = new System.Drawing.Point(3, 0);
            this.listView_items_data.MultiSelect = false;
            this.listView_items_data.Name = "listView_items_data";
            this.listView_items_data.Size = new System.Drawing.Size(253, 616);
            this.listView_items_data.SmallImageList = this.imageList_items;
            this.listView_items_data.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView_items_data.TabIndex = 1;
            this.listView_items_data.UseCompatibleStateImageBehavior = false;
            this.listView_items_data.View = System.Windows.Forms.View.Details;
            this.listView_items_data.VirtualMode = true;
            // 
            // columnHeader130
            // 
            this.columnHeader130.Text = "Item";
            // 
            // columnHeader131
            // 
            this.columnHeader131.Text = "Count";
            // 
            // columnHeader132
            // 
            this.columnHeader132.Text = "ObjID";
            // 
            // contextMenuStrip_Items
            // 
            this.contextMenuStrip_Items.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToDoNotListToolStripMenuItem});
            this.contextMenuStrip_Items.Name = "contextMenuStrip_Items";
            this.contextMenuStrip_Items.Size = new System.Drawing.Size(180, 26);
            // 
            // addToDoNotListToolStripMenuItem
            // 
            this.addToDoNotListToolStripMenuItem.Name = "addToDoNotListToolStripMenuItem";
            this.addToDoNotListToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.addToDoNotListToolStripMenuItem.Text = "Add to \"Do Not\" list";
            this.addToDoNotListToolStripMenuItem.Click += new System.EventHandler(this.addToDoNotListToolStripMenuItem_Click);
            // 
            // tabPage_npc
            // 
            this.tabPage_npc.Controls.Add(this.listView_npc_data);
            this.tabPage_npc.Location = new System.Drawing.Point(4, 58);
            this.tabPage_npc.Name = "tabPage_npc";
            this.tabPage_npc.Size = new System.Drawing.Size(256, 576);
            this.tabPage_npc.TabIndex = 8;
            this.tabPage_npc.Text = "NPC";
            this.tabPage_npc.UseVisualStyleBackColor = true;
            // 
            // listView_npc_data
            // 
            this.listView_npc_data.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_npc_data.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView_npc_data.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader135,
            this.columnHeader136,
            this.columnHeader137,
            this.columnHeader5});
            this.listView_npc_data.ContextMenuStrip = this.contextMenuStrip_NPC;
            this.listView_npc_data.FullRowSelect = true;
            this.listView_npc_data.GridLines = true;
            this.listView_npc_data.Location = new System.Drawing.Point(3, 3);
            this.listView_npc_data.MultiSelect = false;
            this.listView_npc_data.Name = "listView_npc_data";
            this.listView_npc_data.Size = new System.Drawing.Size(253, 613);
            this.listView_npc_data.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView_npc_data.TabIndex = 2;
            this.listView_npc_data.UseCompatibleStateImageBehavior = false;
            this.listView_npc_data.View = System.Windows.Forms.View.Details;
            this.listView_npc_data.VirtualMode = true;
            // 
            // columnHeader135
            // 
            this.columnHeader135.Text = "Name";
            // 
            // columnHeader136
            // 
            this.columnHeader136.Text = "Title";
            // 
            // columnHeader137
            // 
            this.columnHeader137.Text = "ObjID";
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Type ID";
            // 
            // contextMenuStrip_NPC
            // 
            this.contextMenuStrip_NPC.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToDoNotListNPCToolStripMenuItem,
            this.addToBlackListNPCToolStripMenuItem});
            this.contextMenuStrip_NPC.Name = "contextMenuStrip_NPC";
            this.contextMenuStrip_NPC.Size = new System.Drawing.Size(180, 48);
            // 
            // addToDoNotListNPCToolStripMenuItem
            // 
            this.addToDoNotListNPCToolStripMenuItem.Name = "addToDoNotListNPCToolStripMenuItem";
            this.addToDoNotListNPCToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.addToDoNotListNPCToolStripMenuItem.Text = "Add to \"Do Not\" list";
            this.addToDoNotListNPCToolStripMenuItem.Click += new System.EventHandler(this.addToDoNotListNPCToolStripMenuItem_Click);
            // 
            // addToBlackListNPCToolStripMenuItem
            // 
            this.addToBlackListNPCToolStripMenuItem.Name = "addToBlackListNPCToolStripMenuItem";
            this.addToBlackListNPCToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.addToBlackListNPCToolStripMenuItem.Text = "Blacklist NPC";
            this.addToBlackListNPCToolStripMenuItem.Click += new System.EventHandler(this.addToBlackListNPCToolStripMenuItem_Click);
            // 
            // tabPage_npc_chat
            // 
            this.tabPage_npc_chat.Controls.Add(this.panel_npc_chat);
            this.tabPage_npc_chat.Location = new System.Drawing.Point(4, 58);
            this.tabPage_npc_chat.Name = "tabPage_npc_chat";
            this.tabPage_npc_chat.Size = new System.Drawing.Size(256, 576);
            this.tabPage_npc_chat.TabIndex = 9;
            this.tabPage_npc_chat.Text = "NPC Chat";
            this.tabPage_npc_chat.UseVisualStyleBackColor = true;
            // 
            // panel_npc_chat
            // 
            this.panel_npc_chat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_npc_chat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_npc_chat.Controls.Add(this.textBox_rtb_input);
            this.panel_npc_chat.Controls.Add(this.richTextBox_dialog);
            this.panel_npc_chat.Controls.Add(this.button_npc_close);
            this.panel_npc_chat.Location = new System.Drawing.Point(3, 3);
            this.panel_npc_chat.Name = "panel_npc_chat";
            this.panel_npc_chat.Size = new System.Drawing.Size(253, 610);
            this.panel_npc_chat.TabIndex = 25;
            // 
            // textBox_rtb_input
            // 
            this.textBox_rtb_input.Location = new System.Drawing.Point(3, 200);
            this.textBox_rtb_input.Name = "textBox_rtb_input";
            this.textBox_rtb_input.Size = new System.Drawing.Size(245, 20);
            this.textBox_rtb_input.TabIndex = 2;
            this.textBox_rtb_input.Visible = false;
            // 
            // richTextBox_dialog
            // 
            this.richTextBox_dialog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox_dialog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox_dialog.Location = new System.Drawing.Point(3, 3);
            this.richTextBox_dialog.Name = "richTextBox_dialog";
            this.richTextBox_dialog.ReadOnly = true;
            this.richTextBox_dialog.Size = new System.Drawing.Size(245, 587);
            this.richTextBox_dialog.TabIndex = 1;
            this.richTextBox_dialog.Text = "";
            // 
            // button_npc_close
            // 
            this.button_npc_close.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.button_npc_close.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_npc_close.Location = new System.Drawing.Point(39, 578);
            this.button_npc_close.Name = "button_npc_close";
            this.button_npc_close.Size = new System.Drawing.Size(173, 24);
            this.button_npc_close.TabIndex = 0;
            this.button_npc_close.Text = "Close";
            this.button_npc_close.Click += new System.EventHandler(this.button_npc_close_Click);
            // 
            // tabPage_buffs
            // 
            this.tabPage_buffs.Controls.Add(this.listView_mybuffs_data);
            this.tabPage_buffs.Location = new System.Drawing.Point(4, 58);
            this.tabPage_buffs.Name = "tabPage_buffs";
            this.tabPage_buffs.Size = new System.Drawing.Size(256, 576);
            this.tabPage_buffs.TabIndex = 10;
            this.tabPage_buffs.Text = "Buffs";
            this.tabPage_buffs.UseVisualStyleBackColor = true;
            // 
            // listView_mybuffs_data
            // 
            this.listView_mybuffs_data.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_mybuffs_data.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader4,
            this.columnHeader3});
            this.listView_mybuffs_data.FullRowSelect = true;
            this.listView_mybuffs_data.GridLines = true;
            this.listView_mybuffs_data.Location = new System.Drawing.Point(0, 0);
            this.listView_mybuffs_data.MultiSelect = false;
            this.listView_mybuffs_data.Name = "listView_mybuffs_data";
            this.listView_mybuffs_data.Size = new System.Drawing.Size(253, 592);
            this.listView_mybuffs_data.SmallImageList = this.imageList_skills;
            this.listView_mybuffs_data.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.listView_mybuffs_data.TabIndex = 2;
            this.listView_mybuffs_data.UseCompatibleStateImageBehavior = false;
            this.listView_mybuffs_data.View = System.Windows.Forms.View.Details;
            this.listView_mybuffs_data.VirtualMode = true;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Level";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Remaining";
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "ObjID";
            // 
            // tabPage_stats
            // 
            this.tabPage_stats.Controls.Add(this.label_badmobs);
            this.tabPage_stats.Controls.Add(this.label7);
            this.tabPage_stats.Controls.Add(this.label_meshlessignored);
            this.tabPage_stats.Controls.Add(this.label6);
            this.tabPage_stats.Controls.Add(this.label_adena_total);
            this.tabPage_stats.Controls.Add(this.label5);
            this.tabPage_stats.Controls.Add(this.button_clear_stats);
            this.tabPage_stats.Controls.Add(this.listView_stats);
            this.tabPage_stats.Controls.Add(this.label_SP);
            this.tabPage_stats.Controls.Add(this.label_XP);
            this.tabPage_stats.Controls.Add(this.label_Adena);
            this.tabPage_stats.Controls.Add(this.label3);
            this.tabPage_stats.Controls.Add(this.label2);
            this.tabPage_stats.Controls.Add(this.label1);
            this.tabPage_stats.Location = new System.Drawing.Point(4, 58);
            this.tabPage_stats.Name = "tabPage_stats";
            this.tabPage_stats.Size = new System.Drawing.Size(256, 576);
            this.tabPage_stats.TabIndex = 11;
            this.tabPage_stats.Text = "Stats";
            this.tabPage_stats.UseVisualStyleBackColor = true;
            // 
            // label_badmobs
            // 
            this.label_badmobs.BackColor = System.Drawing.Color.Transparent;
            this.label_badmobs.ForeColor = System.Drawing.Color.Black;
            this.label_badmobs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_badmobs.Location = new System.Drawing.Point(97, 91);
            this.label_badmobs.Name = "label_badmobs";
            this.label_badmobs.Size = new System.Drawing.Size(143, 16);
            this.label_badmobs.TabIndex = 36;
            this.label_badmobs.Text = "0";
            this.label_badmobs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label7.Location = new System.Drawing.Point(6, 91);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(94, 16);
            this.label7.TabIndex = 35;
            this.label7.Text = "Bad Mobs ignored: ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_meshlessignored
            // 
            this.label_meshlessignored.BackColor = System.Drawing.Color.Transparent;
            this.label_meshlessignored.ForeColor = System.Drawing.Color.Black;
            this.label_meshlessignored.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_meshlessignored.Location = new System.Drawing.Point(97, 75);
            this.label_meshlessignored.Name = "label_meshlessignored";
            this.label_meshlessignored.Size = new System.Drawing.Size(143, 16);
            this.label_meshlessignored.TabIndex = 34;
            this.label_meshlessignored.Text = "0";
            this.label_meshlessignored.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Location = new System.Drawing.Point(6, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(94, 16);
            this.label6.TabIndex = 33;
            this.label6.Text = "Meshless ignored: ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_adena_total
            // 
            this.label_adena_total.BackColor = System.Drawing.Color.Transparent;
            this.label_adena_total.ForeColor = System.Drawing.Color.Black;
            this.label_adena_total.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_adena_total.Location = new System.Drawing.Point(71, 11);
            this.label_adena_total.Name = "label_adena_total";
            this.label_adena_total.Size = new System.Drawing.Size(178, 16);
            this.label_adena_total.TabIndex = 32;
            this.label_adena_total.Text = "0";
            this.label_adena_total.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(6, 11);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 16);
            this.label5.TabIndex = 31;
            this.label5.Text = "Total Adena: ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // button_clear_stats
            // 
            this.button_clear_stats.Location = new System.Drawing.Point(3, 118);
            this.button_clear_stats.Name = "button_clear_stats";
            this.button_clear_stats.Size = new System.Drawing.Size(75, 23);
            this.button_clear_stats.TabIndex = 30;
            this.button_clear_stats.Text = "Clear Stats";
            this.button_clear_stats.UseVisualStyleBackColor = true;
            this.button_clear_stats.Click += new System.EventHandler(this.button_clear_stats_Click);
            // 
            // listView_stats
            // 
            this.listView_stats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.listView_stats.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader10});
            this.listView_stats.FullRowSelect = true;
            this.listView_stats.GridLines = true;
            this.listView_stats.Location = new System.Drawing.Point(0, 154);
            this.listView_stats.MultiSelect = false;
            this.listView_stats.Name = "listView_stats";
            this.listView_stats.Size = new System.Drawing.Size(255, 451);
            this.listView_stats.TabIndex = 29;
            this.listView_stats.UseCompatibleStateImageBehavior = false;
            this.listView_stats.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Name";
            this.columnHeader8.Width = 113;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Item";
            this.columnHeader6.Width = 48;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Count";
            this.columnHeader7.Width = 44;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "ObjID";
            this.columnHeader10.Width = 79;
            // 
            // label_SP
            // 
            this.label_SP.BackColor = System.Drawing.Color.Transparent;
            this.label_SP.ForeColor = System.Drawing.Color.Black;
            this.label_SP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_SP.Location = new System.Drawing.Point(71, 59);
            this.label_SP.Name = "label_SP";
            this.label_SP.Size = new System.Drawing.Size(178, 16);
            this.label_SP.TabIndex = 28;
            this.label_SP.Text = "0";
            this.label_SP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_XP
            // 
            this.label_XP.BackColor = System.Drawing.Color.Transparent;
            this.label_XP.ForeColor = System.Drawing.Color.Black;
            this.label_XP.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_XP.Location = new System.Drawing.Point(71, 43);
            this.label_XP.Name = "label_XP";
            this.label_XP.Size = new System.Drawing.Size(175, 16);
            this.label_XP.TabIndex = 27;
            this.label_XP.Text = "0";
            this.label_XP.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_Adena
            // 
            this.label_Adena.BackColor = System.Drawing.Color.Transparent;
            this.label_Adena.ForeColor = System.Drawing.Color.Black;
            this.label_Adena.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label_Adena.Location = new System.Drawing.Point(71, 27);
            this.label_Adena.Name = "label_Adena";
            this.label_Adena.Size = new System.Drawing.Size(178, 16);
            this.label_Adena.TabIndex = 26;
            this.label_Adena.Text = "0";
            this.label_Adena.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(6, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 16);
            this.label3.TabIndex = 25;
            this.label3.Text = "SP/h: ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(6, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 16);
            this.label2.TabIndex = 24;
            this.label2.Text = "XP/h:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 16);
            this.label1.TabIndex = 23;
            this.label1.Text = "Adena/h:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBox_op_control
            // 
            this.checkBox_op_control.Location = new System.Drawing.Point(224, 4);
            this.checkBox_op_control.Name = "checkBox_op_control";
            this.checkBox_op_control.Size = new System.Drawing.Size(73, 18);
            this.checkBox_op_control.TabIndex = 2;
            this.checkBox_op_control.Text = "control";
            // 
            // checkBox_op_shift
            // 
            this.checkBox_op_shift.Location = new System.Drawing.Point(303, 4);
            this.checkBox_op_shift.Name = "checkBox_op_shift";
            this.checkBox_op_shift.Size = new System.Drawing.Size(50, 18);
            this.checkBox_op_shift.TabIndex = 3;
            this.checkBox_op_shift.Text = "shift";
            // 
            // label_zrange_map
            // 
            this.label_zrange_map.AutoSize = true;
            this.label_zrange_map.Location = new System.Drawing.Point(547, 7);
            this.label_zrange_map.Name = "label_zrange_map";
            this.label_zrange_map.Size = new System.Drawing.Size(73, 13);
            this.label_zrange_map.TabIndex = 31;
            this.label_zrange_map.Text = "Map Z Range";
            // 
            // trackBar_map_zoom
            // 
            this.trackBar_map_zoom.LargeChange = 12;
            this.trackBar_map_zoom.Location = new System.Drawing.Point(0, 1);
            this.trackBar_map_zoom.Maximum = 128;
            this.trackBar_map_zoom.Minimum = 2;
            this.trackBar_map_zoom.Name = "trackBar_map_zoom";
            this.trackBar_map_zoom.Size = new System.Drawing.Size(121, 45);
            this.trackBar_map_zoom.TabIndex = 0;
            this.trackBar_map_zoom.TickFrequency = 8;
            this.trackBar_map_zoom.Value = 8;
            // 
            // textBox_zrange_map
            // 
            this.textBox_zrange_map.Location = new System.Drawing.Point(506, 3);
            this.textBox_zrange_map.MaxLength = 8;
            this.textBox_zrange_map.Name = "textBox_zrange_map";
            this.textBox_zrange_map.Size = new System.Drawing.Size(35, 20);
            this.textBox_zrange_map.TabIndex = 30;
            this.textBox_zrange_map.Text = "500";
            // 
            // checkBox_minimap
            // 
            this.checkBox_minimap.Location = new System.Drawing.Point(127, 4);
            this.checkBox_minimap.Name = "checkBox_minimap";
            this.checkBox_minimap.Size = new System.Drawing.Size(91, 18);
            this.checkBox_minimap.TabIndex = 1;
            this.checkBox_minimap.Text = "Map";
            // 
            // panel_chat
            // 
            this.panel_chat.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_chat.Controls.Add(this.checkBox_BoundingPoints);
            this.panel_chat.Controls.Add(this.comboBox_msg_type);
            this.panel_chat.Controls.Add(this.button_sendtext);
            this.panel_chat.Controls.Add(this.textBox_say);
            this.panel_chat.Controls.Add(this.checkBox_op_control);
            this.panel_chat.Controls.Add(this.checkBox_op_shift);
            this.panel_chat.Controls.Add(this.label_zrange_map);
            this.panel_chat.Controls.Add(this.textBox_zrange_map);
            this.panel_chat.Controls.Add(this.trackBar_map_zoom);
            this.panel_chat.Controls.Add(this.checkBox_minimap);
            this.panel_chat.Controls.Add(this.tabControl_ChatSelect);
            this.panel_chat.Location = new System.Drawing.Point(128, 452);
            this.panel_chat.Name = "panel_chat";
            this.panel_chat.Size = new System.Drawing.Size(626, 207);
            this.panel_chat.TabIndex = 22;
            // 
            // checkBox_BoundingPoints
            // 
            this.checkBox_BoundingPoints.Location = new System.Drawing.Point(359, 4);
            this.checkBox_BoundingPoints.Name = "checkBox_BoundingPoints";
            this.checkBox_BoundingPoints.Size = new System.Drawing.Size(141, 18);
            this.checkBox_BoundingPoints.TabIndex = 33;
            this.checkBox_BoundingPoints.Text = "Add Polygon Points";
            this.checkBox_BoundingPoints.CheckedChanged += new System.EventHandler(this.checkBox_BoundingPoints_CheckedChanged);
            // 
            // comboBox_msg_type
            // 
            this.comboBox_msg_type.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox_msg_type.DropDownWidth = 150;
            this.comboBox_msg_type.Items.AddRange(new object[] {
            "Local",
            "Shout",
            "Tell",
            "Party",
            "Clan",
            "GM",
            "Petition Reply to GM",
            "Petition From GM",
            "Trade",
            "Alliance",
            "Announcement",
            "Boat Message",
            "Fake1",
            "Fake2",
            "Fake3",
            "PartyRoomAll",
            "PartyRoomCommander",
            "HeroVoice"});
            this.comboBox_msg_type.Location = new System.Drawing.Point(0, 34);
            this.comboBox_msg_type.Name = "comboBox_msg_type";
            this.comboBox_msg_type.Size = new System.Drawing.Size(88, 21);
            this.comboBox_msg_type.TabIndex = 0;
            this.comboBox_msg_type.Text = "comboBox1";
            // 
            // button_sendtext
            // 
            this.button_sendtext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_sendtext.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_sendtext.Location = new System.Drawing.Point(552, 32);
            this.button_sendtext.Name = "button_sendtext";
            this.button_sendtext.Size = new System.Drawing.Size(72, 24);
            this.button_sendtext.TabIndex = 2;
            this.button_sendtext.Text = "Say";
            this.button_sendtext.Click += new System.EventHandler(this.button_sendtext_Click);
            // 
            // textBox_say
            // 
            this.textBox_say.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_say.Location = new System.Drawing.Point(88, 34);
            this.textBox_say.Name = "textBox_say";
            this.textBox_say.Size = new System.Drawing.Size(465, 20);
            this.textBox_say.TabIndex = 1;
            // 
            // tabControl_ChatSelect
            // 
            this.tabControl_ChatSelect.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl_ChatSelect.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl_ChatSelect.Controls.Add(this.tab_all);
            this.tabControl_ChatSelect.Controls.Add(this.tab_system);
            this.tabControl_ChatSelect.Controls.Add(this.tab_bot);
            this.tabControl_ChatSelect.Controls.Add(this.tab_local);
            this.tabControl_ChatSelect.Controls.Add(this.tab_trade);
            this.tabControl_ChatSelect.Controls.Add(this.tab_party);
            this.tabControl_ChatSelect.Controls.Add(this.tab_clan);
            this.tabControl_ChatSelect.Controls.Add(this.tab_alliance);
            this.tabControl_ChatSelect.Controls.Add(this.tab_hero);
            this.tabControl_ChatSelect.Location = new System.Drawing.Point(-3, 52);
            this.tabControl_ChatSelect.Multiline = true;
            this.tabControl_ChatSelect.Name = "tabControl_ChatSelect";
            this.tabControl_ChatSelect.Padding = new System.Drawing.Point(10, 2);
            this.tabControl_ChatSelect.SelectedIndex = 0;
            this.tabControl_ChatSelect.Size = new System.Drawing.Size(633, 156);
            this.tabControl_ChatSelect.TabIndex = 32;
            // 
            // tab_all
            // 
            this.tab_all.Controls.Add(this.colorListBox_all);
            this.tab_all.Location = new System.Drawing.Point(4, 4);
            this.tab_all.Name = "tab_all";
            this.tab_all.Padding = new System.Windows.Forms.Padding(3);
            this.tab_all.Size = new System.Drawing.Size(625, 132);
            this.tab_all.TabIndex = 0;
            this.tab_all.Text = "All";
            this.tab_all.UseVisualStyleBackColor = true;
            // 
            // colorListBox_all
            // 
            this.colorListBox_all.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.colorListBox_all.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(27)))));
            this.colorListBox_all.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.colorListBox_all.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.colorListBox_all.Font = new System.Drawing.Font("Arial", 9F);
            this.colorListBox_all.FormattingEnabled = true;
            this.colorListBox_all.HorizontalScrollbar = true;
            this.colorListBox_all.Location = new System.Drawing.Point(0, 0);
            this.colorListBox_all.Name = "colorListBox_all";
            this.colorListBox_all.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.colorListBox_all.Size = new System.Drawing.Size(625, 132);
            this.colorListBox_all.TabIndex = 0;
            // 
            // tab_system
            // 
            this.tab_system.Controls.Add(this.colorListBox_system);
            this.tab_system.Location = new System.Drawing.Point(4, 4);
            this.tab_system.Name = "tab_system";
            this.tab_system.Size = new System.Drawing.Size(625, 132);
            this.tab_system.TabIndex = 6;
            this.tab_system.Text = "System";
            this.tab_system.UseVisualStyleBackColor = true;
            // 
            // colorListBox_system
            // 
            this.colorListBox_system.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.colorListBox_system.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(27)))));
            this.colorListBox_system.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.colorListBox_system.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.colorListBox_system.Font = new System.Drawing.Font("Arial", 9F);
            this.colorListBox_system.FormattingEnabled = true;
            this.colorListBox_system.HorizontalScrollbar = true;
            this.colorListBox_system.Location = new System.Drawing.Point(0, 0);
            this.colorListBox_system.Name = "colorListBox_system";
            this.colorListBox_system.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.colorListBox_system.Size = new System.Drawing.Size(625, 132);
            this.colorListBox_system.TabIndex = 1;
            // 
            // tab_bot
            // 
            this.tab_bot.Controls.Add(this.colorListBox_bot);
            this.tab_bot.Location = new System.Drawing.Point(4, 4);
            this.tab_bot.Name = "tab_bot";
            this.tab_bot.Size = new System.Drawing.Size(625, 132);
            this.tab_bot.TabIndex = 5;
            this.tab_bot.Text = "Bot";
            this.tab_bot.UseVisualStyleBackColor = true;
            // 
            // colorListBox_bot
            // 
            this.colorListBox_bot.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.colorListBox_bot.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(27)))));
            this.colorListBox_bot.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.colorListBox_bot.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.colorListBox_bot.Font = new System.Drawing.Font("Arial", 9F);
            this.colorListBox_bot.FormattingEnabled = true;
            this.colorListBox_bot.HorizontalScrollbar = true;
            this.colorListBox_bot.Location = new System.Drawing.Point(0, 0);
            this.colorListBox_bot.Name = "colorListBox_bot";
            this.colorListBox_bot.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.colorListBox_bot.Size = new System.Drawing.Size(625, 132);
            this.colorListBox_bot.TabIndex = 1;
            // 
            // tab_local
            // 
            this.tab_local.Controls.Add(this.colorListBox_local);
            this.tab_local.Location = new System.Drawing.Point(4, 4);
            this.tab_local.Name = "tab_local";
            this.tab_local.Padding = new System.Windows.Forms.Padding(3);
            this.tab_local.Size = new System.Drawing.Size(625, 132);
            this.tab_local.TabIndex = 1;
            this.tab_local.Text = "Local";
            this.tab_local.UseVisualStyleBackColor = true;
            // 
            // colorListBox_local
            // 
            this.colorListBox_local.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.colorListBox_local.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(27)))));
            this.colorListBox_local.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.colorListBox_local.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.colorListBox_local.Font = new System.Drawing.Font("Arial", 9F);
            this.colorListBox_local.FormattingEnabled = true;
            this.colorListBox_local.HorizontalScrollbar = true;
            this.colorListBox_local.Location = new System.Drawing.Point(0, 0);
            this.colorListBox_local.Name = "colorListBox_local";
            this.colorListBox_local.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.colorListBox_local.Size = new System.Drawing.Size(625, 132);
            this.colorListBox_local.TabIndex = 1;
            // 
            // tab_trade
            // 
            this.tab_trade.Controls.Add(this.colorListBox_trade);
            this.tab_trade.Location = new System.Drawing.Point(4, 4);
            this.tab_trade.Name = "tab_trade";
            this.tab_trade.Size = new System.Drawing.Size(625, 132);
            this.tab_trade.TabIndex = 2;
            this.tab_trade.Text = "Trade";
            this.tab_trade.UseVisualStyleBackColor = true;
            // 
            // colorListBox_trade
            // 
            this.colorListBox_trade.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.colorListBox_trade.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(27)))));
            this.colorListBox_trade.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.colorListBox_trade.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.colorListBox_trade.Font = new System.Drawing.Font("Arial", 9F);
            this.colorListBox_trade.FormattingEnabled = true;
            this.colorListBox_trade.HorizontalScrollbar = true;
            this.colorListBox_trade.Location = new System.Drawing.Point(0, 0);
            this.colorListBox_trade.Name = "colorListBox_trade";
            this.colorListBox_trade.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.colorListBox_trade.Size = new System.Drawing.Size(625, 132);
            this.colorListBox_trade.TabIndex = 1;
            // 
            // tab_party
            // 
            this.tab_party.Controls.Add(this.colorListBox_party);
            this.tab_party.Location = new System.Drawing.Point(4, 4);
            this.tab_party.Name = "tab_party";
            this.tab_party.Size = new System.Drawing.Size(625, 132);
            this.tab_party.TabIndex = 7;
            this.tab_party.Text = "Party";
            this.tab_party.UseVisualStyleBackColor = true;
            // 
            // colorListBox_party
            // 
            this.colorListBox_party.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.colorListBox_party.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(27)))));
            this.colorListBox_party.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.colorListBox_party.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.colorListBox_party.Font = new System.Drawing.Font("Arial", 9F);
            this.colorListBox_party.FormattingEnabled = true;
            this.colorListBox_party.HorizontalScrollbar = true;
            this.colorListBox_party.Location = new System.Drawing.Point(0, 0);
            this.colorListBox_party.Name = "colorListBox_party";
            this.colorListBox_party.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.colorListBox_party.Size = new System.Drawing.Size(625, 132);
            this.colorListBox_party.TabIndex = 1;
            // 
            // tab_clan
            // 
            this.tab_clan.Controls.Add(this.colorListBox_clan);
            this.tab_clan.Location = new System.Drawing.Point(4, 4);
            this.tab_clan.Name = "tab_clan";
            this.tab_clan.Size = new System.Drawing.Size(625, 132);
            this.tab_clan.TabIndex = 3;
            this.tab_clan.Text = "Clan";
            this.tab_clan.UseVisualStyleBackColor = true;
            // 
            // colorListBox_clan
            // 
            this.colorListBox_clan.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.colorListBox_clan.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(27)))));
            this.colorListBox_clan.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.colorListBox_clan.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.colorListBox_clan.Font = new System.Drawing.Font("Arial", 9F);
            this.colorListBox_clan.FormattingEnabled = true;
            this.colorListBox_clan.HorizontalScrollbar = true;
            this.colorListBox_clan.Location = new System.Drawing.Point(0, 0);
            this.colorListBox_clan.Name = "colorListBox_clan";
            this.colorListBox_clan.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.colorListBox_clan.Size = new System.Drawing.Size(625, 132);
            this.colorListBox_clan.TabIndex = 1;
            // 
            // tab_alliance
            // 
            this.tab_alliance.Controls.Add(this.colorListBox_ally);
            this.tab_alliance.Location = new System.Drawing.Point(4, 4);
            this.tab_alliance.Name = "tab_alliance";
            this.tab_alliance.Size = new System.Drawing.Size(625, 132);
            this.tab_alliance.TabIndex = 4;
            this.tab_alliance.Text = "Alliance";
            this.tab_alliance.UseVisualStyleBackColor = true;
            // 
            // colorListBox_ally
            // 
            this.colorListBox_ally.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.colorListBox_ally.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(27)))));
            this.colorListBox_ally.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.colorListBox_ally.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.colorListBox_ally.Font = new System.Drawing.Font("Arial", 9F);
            this.colorListBox_ally.FormattingEnabled = true;
            this.colorListBox_ally.HorizontalScrollbar = true;
            this.colorListBox_ally.Location = new System.Drawing.Point(0, 0);
            this.colorListBox_ally.Name = "colorListBox_ally";
            this.colorListBox_ally.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.colorListBox_ally.Size = new System.Drawing.Size(625, 132);
            this.colorListBox_ally.TabIndex = 1;
            // 
            // tab_hero
            // 
            this.tab_hero.Controls.Add(this.colorListBox_hero);
            this.tab_hero.Location = new System.Drawing.Point(4, 4);
            this.tab_hero.Name = "tab_hero";
            this.tab_hero.Size = new System.Drawing.Size(625, 132);
            this.tab_hero.TabIndex = 8;
            this.tab_hero.Text = "Hero";
            this.tab_hero.UseVisualStyleBackColor = true;
            // 
            // colorListBox_hero
            // 
            this.colorListBox_hero.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.colorListBox_hero.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(28)))), ((int)(((byte)(28)))), ((int)(((byte)(27)))));
            this.colorListBox_hero.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.colorListBox_hero.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.colorListBox_hero.Font = new System.Drawing.Font("Arial", 9F);
            this.colorListBox_hero.FormattingEnabled = true;
            this.colorListBox_hero.HorizontalScrollbar = true;
            this.colorListBox_hero.Location = new System.Drawing.Point(2, 0);
            this.colorListBox_hero.Name = "colorListBox_hero";
            this.colorListBox_hero.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.colorListBox_hero.Size = new System.Drawing.Size(625, 132);
            this.colorListBox_hero.TabIndex = 2;
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 0;
            this.toolTip1.ShowAlways = true;
            // 
            // notifyIcon_us
            // 
            this.notifyIcon_us.ContextMenuStrip = this.contextMenuStrip_notify;
            this.notifyIcon_us.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon_us.Icon")));
            this.notifyIcon_us.Text = "L2.Net";
            // 
            // contextMenuStrip_notify
            // 
            this.contextMenuStrip_notify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleBottingToolStripMenuItem,
            this.botOptionsToolStripMenuItem,
            this.startScriptToolStripMenuItem,
            this.toolStripSeparator8,
            this.closeToolStripMenuItem,
            this.forceLogToolStripMenuItem1});
            this.contextMenuStrip_notify.Name = "contextMenuStrip_notify";
            this.contextMenuStrip_notify.Size = new System.Drawing.Size(154, 120);
            // 
            // toggleBottingToolStripMenuItem
            // 
            this.toggleBottingToolStripMenuItem.Checked = true;
            this.toggleBottingToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toggleBottingToolStripMenuItem.Name = "toggleBottingToolStripMenuItem";
            this.toggleBottingToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.toggleBottingToolStripMenuItem.Text = "Toggle Botting";
            this.toggleBottingToolStripMenuItem.Click += new System.EventHandler(this.toggleBottingToolStripMenuItem_Click);
            // 
            // botOptionsToolStripMenuItem
            // 
            this.botOptionsToolStripMenuItem.Name = "botOptionsToolStripMenuItem";
            this.botOptionsToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.botOptionsToolStripMenuItem.Text = "Bot Options";
            this.botOptionsToolStripMenuItem.Click += new System.EventHandler(this.botOptionsToolStripMenuItem_Click);
            // 
            // startScriptToolStripMenuItem
            // 
            this.startScriptToolStripMenuItem.Name = "startScriptToolStripMenuItem";
            this.startScriptToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.startScriptToolStripMenuItem.Text = "Start Script";
            this.startScriptToolStripMenuItem.Click += new System.EventHandler(this.menuItem_startscript_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(150, 6);
            // 
            // closeToolStripMenuItem
            // 
            this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            this.closeToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.closeToolStripMenuItem.Text = "Close";
            this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
            // 
            // forceLogToolStripMenuItem1
            // 
            this.forceLogToolStripMenuItem1.Name = "forceLogToolStripMenuItem1";
            this.forceLogToolStripMenuItem1.Size = new System.Drawing.Size(153, 22);
            this.forceLogToolStripMenuItem1.Text = "Force Log";
            this.forceLogToolStripMenuItem1.Click += new System.EventHandler(this.forceLogToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_File,
            this.menuItem_Commands,
            this.menuItem_Options,
            this.menuItem_scripting,
            this.menuItem_Help});
            this.menuStrip1.Location = new System.Drawing.Point(128, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(626, 24);
            this.menuStrip1.TabIndex = 27;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuItem_File
            // 
            this.menuItem_File.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_cmd_logon,
            this.menuItem_cmd_game,
            this.menuitem_cmd_GG,
            this.menuitem_cmd_ggclient,
            this.toolStripSeparator1,
            this.menuItem_cmd_overlay,
            this.menuItem_cmd_shortcut,
            this.petWindowToolStripMenuItem,
            this.menuItem_actions,
            this.extendedActionsToolStripMenuItem,
            this.toolStripSeparator3,
            this.menuItem_options_setup,
            this.menuItem_saveinterface,
            this.toolStripSeparator12,
            this.menuItem_launchl2,
            this.toolStripSeparator2,
            this.menuItem_exit});
            this.menuItem_File.Name = "menuItem_File";
            this.menuItem_File.Size = new System.Drawing.Size(39, 20);
            this.menuItem_File.Text = "&File";
            // 
            // menuItem_cmd_logon
            // 
            this.menuItem_cmd_logon.Name = "menuItem_cmd_logon";
            this.menuItem_cmd_logon.Size = new System.Drawing.Size(174, 22);
            this.menuItem_cmd_logon.Text = "Logon Window";
            this.menuItem_cmd_logon.Click += new System.EventHandler(this.menuItem_cmd_logon_Click);
            // 
            // menuItem_cmd_game
            // 
            this.menuItem_cmd_game.Name = "menuItem_cmd_game";
            this.menuItem_cmd_game.Size = new System.Drawing.Size(174, 22);
            this.menuItem_cmd_game.Text = "Game Window";
            this.menuItem_cmd_game.Click += new System.EventHandler(this.menuItem_cmd_game_Click);
            // 
            // menuitem_cmd_GG
            // 
            this.menuitem_cmd_GG.Name = "menuitem_cmd_GG";
            this.menuitem_cmd_GG.Size = new System.Drawing.Size(174, 22);
            this.menuitem_cmd_GG.Text = "Gameguard Server";
            this.menuitem_cmd_GG.Click += new System.EventHandler(this.menuitem_cmd_GG_Click);
            // 
            // menuitem_cmd_ggclient
            // 
            this.menuitem_cmd_ggclient.Name = "menuitem_cmd_ggclient";
            this.menuitem_cmd_ggclient.Size = new System.Drawing.Size(174, 22);
            this.menuitem_cmd_ggclient.Text = "Gameguard Client";
            this.menuitem_cmd_ggclient.Click += new System.EventHandler(this.menuitem_cmd_ggclient_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(171, 6);
            // 
            // menuItem_cmd_overlay
            // 
            this.menuItem_cmd_overlay.Name = "menuItem_cmd_overlay";
            this.menuItem_cmd_overlay.Size = new System.Drawing.Size(174, 22);
            this.menuItem_cmd_overlay.Text = "Overlay Window";
            this.menuItem_cmd_overlay.Click += new System.EventHandler(this.menuItem_cmd_overlay_Click);
            // 
            // menuItem_cmd_shortcut
            // 
            this.menuItem_cmd_shortcut.Name = "menuItem_cmd_shortcut";
            this.menuItem_cmd_shortcut.Size = new System.Drawing.Size(174, 22);
            this.menuItem_cmd_shortcut.Text = "Shortcut Window";
            this.menuItem_cmd_shortcut.Click += new System.EventHandler(this.menuItem_cmd_shortcut_Click);
            // 
            // petWindowToolStripMenuItem
            // 
            this.petWindowToolStripMenuItem.Name = "petWindowToolStripMenuItem";
            this.petWindowToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.petWindowToolStripMenuItem.Text = "Pet Window";
            this.petWindowToolStripMenuItem.Click += new System.EventHandler(this.petWindowToolStripMenuItem_Click);
            // 
            // menuItem_actions
            // 
            this.menuItem_actions.Name = "menuItem_actions";
            this.menuItem_actions.Size = new System.Drawing.Size(174, 22);
            this.menuItem_actions.Text = "Actions";
            this.menuItem_actions.Click += new System.EventHandler(this.ActionsToolStripMenuItem_Click);
            // 
            // extendedActionsToolStripMenuItem
            // 
            this.extendedActionsToolStripMenuItem.Name = "extendedActionsToolStripMenuItem";
            this.extendedActionsToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.extendedActionsToolStripMenuItem.Text = "Extended Actions";
            this.extendedActionsToolStripMenuItem.Click += new System.EventHandler(this.extendedActionsToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(171, 6);
            // 
            // menuItem_options_setup
            // 
            this.menuItem_options_setup.Name = "menuItem_options_setup";
            this.menuItem_options_setup.Size = new System.Drawing.Size(174, 22);
            this.menuItem_options_setup.Text = "Setup";
            this.menuItem_options_setup.Click += new System.EventHandler(this.menuItem_options_setup_Click);
            // 
            // menuItem_saveinterface
            // 
            this.menuItem_saveinterface.Name = "menuItem_saveinterface";
            this.menuItem_saveinterface.Size = new System.Drawing.Size(174, 22);
            this.menuItem_saveinterface.Text = "Save Interface";
            this.menuItem_saveinterface.Click += new System.EventHandler(this.menuItem_saveinterface_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(171, 6);
            // 
            // menuItem_launchl2
            // 
            this.menuItem_launchl2.Name = "menuItem_launchl2";
            this.menuItem_launchl2.Size = new System.Drawing.Size(174, 22);
            this.menuItem_launchl2.Text = "Launch Lineage 2";
            this.menuItem_launchl2.Click += new System.EventHandler(this.menuItem_launchl2_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(171, 6);
            // 
            // menuItem_exit
            // 
            this.menuItem_exit.Name = "menuItem_exit";
            this.menuItem_exit.Size = new System.Drawing.Size(174, 22);
            this.menuItem_exit.Text = "Exit";
            this.menuItem_exit.Click += new System.EventHandler(this.menuItem_exit_Click);
            // 
            // menuItem_Commands
            // 
            this.menuItem_Commands.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_cmd_cancel,
            this.blacklistTargetToolStripMenuItem,
            this.showTargetInfoToolStripMenuItem,
            this.toolStripSeparator6,
            this.menuItem_toggle_botting,
            this.menuItem_toggle_autoreply,
            this.menuItem_toggle_autoreplyPM,
            this.toolStripSeparator5,
            this.menuItem_cmd_restart,
            this.menuItem_cmd_logout,
            this.toolStripSeparator4,
            this.menuItem_closeclient,
            this.menuItem_killthreads,
            this.forceLogToolStripMenuItem,
            this.disconectClientToolStripMenuItem});
            this.menuItem_Commands.Name = "menuItem_Commands";
            this.menuItem_Commands.Size = new System.Drawing.Size(80, 20);
            this.menuItem_Commands.Text = "&Commands";
            // 
            // menuItem_cmd_cancel
            // 
            this.menuItem_cmd_cancel.Name = "menuItem_cmd_cancel";
            this.menuItem_cmd_cancel.Size = new System.Drawing.Size(201, 22);
            this.menuItem_cmd_cancel.Text = "Cancel Target";
            this.menuItem_cmd_cancel.Click += new System.EventHandler(this.menuItem_cmd_cancel_Click);
            // 
            // blacklistTargetToolStripMenuItem
            // 
            this.blacklistTargetToolStripMenuItem.Name = "blacklistTargetToolStripMenuItem";
            this.blacklistTargetToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.blacklistTargetToolStripMenuItem.Text = "Blacklist Target";
            this.blacklistTargetToolStripMenuItem.Click += new System.EventHandler(this.blacklistTargetToolStripMenuItem_Click);
            // 
            // showTargetInfoToolStripMenuItem
            // 
            this.showTargetInfoToolStripMenuItem.Name = "showTargetInfoToolStripMenuItem";
            this.showTargetInfoToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.showTargetInfoToolStripMenuItem.Text = "Show Target Info";
            this.showTargetInfoToolStripMenuItem.Visible = false;
            this.showTargetInfoToolStripMenuItem.Click += new System.EventHandler(this.showTargetInfoToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(198, 6);
            // 
            // menuItem_toggle_botting
            // 
            this.menuItem_toggle_botting.Checked = true;
            this.menuItem_toggle_botting.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuItem_toggle_botting.Name = "menuItem_toggle_botting";
            this.menuItem_toggle_botting.Size = new System.Drawing.Size(201, 22);
            this.menuItem_toggle_botting.Text = "Toggle Botting";
            this.menuItem_toggle_botting.Click += new System.EventHandler(this.menuItem_toggle_botting_Click);
            // 
            // menuItem_toggle_autoreply
            // 
            this.menuItem_toggle_autoreply.Name = "menuItem_toggle_autoreply";
            this.menuItem_toggle_autoreply.Size = new System.Drawing.Size(201, 22);
            this.menuItem_toggle_autoreply.Text = "Toggle Auto Reply Local";
            this.menuItem_toggle_autoreply.Click += new System.EventHandler(this.menuItem_toggle_autoreply_Click);
            // 
            // menuItem_toggle_autoreplyPM
            // 
            this.menuItem_toggle_autoreplyPM.Name = "menuItem_toggle_autoreplyPM";
            this.menuItem_toggle_autoreplyPM.Size = new System.Drawing.Size(201, 22);
            this.menuItem_toggle_autoreplyPM.Text = "Toggle Auto Reply PM";
            this.menuItem_toggle_autoreplyPM.Click += new System.EventHandler(this.menuItem_toggle_autoreplyPM_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(198, 6);
            // 
            // menuItem_cmd_restart
            // 
            this.menuItem_cmd_restart.Name = "menuItem_cmd_restart";
            this.menuItem_cmd_restart.Size = new System.Drawing.Size(201, 22);
            this.menuItem_cmd_restart.Text = "Restart";
            this.menuItem_cmd_restart.Click += new System.EventHandler(this.menuItem_cmd_restart_Click);
            // 
            // menuItem_cmd_logout
            // 
            this.menuItem_cmd_logout.Name = "menuItem_cmd_logout";
            this.menuItem_cmd_logout.Size = new System.Drawing.Size(201, 22);
            this.menuItem_cmd_logout.Text = "Logout";
            this.menuItem_cmd_logout.Click += new System.EventHandler(this.menuItem_cmd_logout_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(198, 6);
            // 
            // menuItem_closeclient
            // 
            this.menuItem_closeclient.Name = "menuItem_closeclient";
            this.menuItem_closeclient.Size = new System.Drawing.Size(201, 22);
            this.menuItem_closeclient.Text = "Close Client";
            this.menuItem_closeclient.Click += new System.EventHandler(this.menuItem_closeclient_Click);
            // 
            // menuItem_killthreads
            // 
            this.menuItem_killthreads.Name = "menuItem_killthreads";
            this.menuItem_killthreads.Size = new System.Drawing.Size(201, 22);
            this.menuItem_killthreads.Text = "Kill Threads";
            this.menuItem_killthreads.Click += new System.EventHandler(this.menuItem_killthreads_Click);
            // 
            // forceLogToolStripMenuItem
            // 
            this.forceLogToolStripMenuItem.Name = "forceLogToolStripMenuItem";
            this.forceLogToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.forceLogToolStripMenuItem.Text = "Force Log";
            this.forceLogToolStripMenuItem.Click += new System.EventHandler(this.forceLogToolStripMenuItem_Click);
            // 
            // menuItem_Options
            // 
            this.menuItem_Options.Name = "menuItem_Options";
            this.menuItem_Options.Size = new System.Drawing.Size(83, 20);
            this.menuItem_Options.Text = "Bot &Options";
            this.menuItem_Options.Click += new System.EventHandler(this.menuItem_Options_Click);
            // 
            // menuItem_scripting
            // 
            this.menuItem_scripting.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_loadscript,
            this.menuItem_startscript,
            this.toolStripSeparator10,
            this.menuItem_scriptwindow,
            this.menuItem_scriptdebugger,
            this.menuItem_encryptscript,
            this.toolStripSeparator9,
            this.menuItem_debug_mode,
            this.menuItem_dump_mode,
            this.menuItem_dump_mode_server,
            this.toolStrip_pck});
            this.menuItem_scripting.Name = "menuItem_scripting";
            this.menuItem_scripting.Size = new System.Drawing.Size(67, 20);
            this.menuItem_scripting.Text = "&Scripting";
            // 
            // menuItem_loadscript
            // 
            this.menuItem_loadscript.Name = "menuItem_loadscript";
            this.menuItem_loadscript.Size = new System.Drawing.Size(177, 22);
            this.menuItem_loadscript.Text = "Set Script Main";
            this.menuItem_loadscript.Click += new System.EventHandler(this.menuItem_loadscript_Click);
            // 
            // menuItem_startscript
            // 
            this.menuItem_startscript.Name = "menuItem_startscript";
            this.menuItem_startscript.Size = new System.Drawing.Size(177, 22);
            this.menuItem_startscript.Text = "Start Script";
            this.menuItem_startscript.Click += new System.EventHandler(this.menuItem_startscript_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(174, 6);
            // 
            // menuItem_scriptwindow
            // 
            this.menuItem_scriptwindow.Name = "menuItem_scriptwindow";
            this.menuItem_scriptwindow.Size = new System.Drawing.Size(177, 22);
            this.menuItem_scriptwindow.Text = "Script Editor";
            this.menuItem_scriptwindow.Click += new System.EventHandler(this.menuItem_scriptwindow_Click);
            // 
            // menuItem_scriptdebugger
            // 
            this.menuItem_scriptdebugger.Name = "menuItem_scriptdebugger";
            this.menuItem_scriptdebugger.Size = new System.Drawing.Size(177, 22);
            this.menuItem_scriptdebugger.Text = "Script Debugger";
            this.menuItem_scriptdebugger.Click += new System.EventHandler(this.menuItem_scriptdebugger_Click);
            // 
            // menuItem_encryptscript
            // 
            this.menuItem_encryptscript.Name = "menuItem_encryptscript";
            this.menuItem_encryptscript.Size = new System.Drawing.Size(177, 22);
            this.menuItem_encryptscript.Text = "Encrypt Script";
            this.menuItem_encryptscript.Click += new System.EventHandler(this.menuItem_encryptscript_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(174, 6);
            // 
            // menuItem_debug_mode
            // 
            this.menuItem_debug_mode.Name = "menuItem_debug_mode";
            this.menuItem_debug_mode.Size = new System.Drawing.Size(177, 22);
            this.menuItem_debug_mode.Text = "Debug Mode";
            this.menuItem_debug_mode.Click += new System.EventHandler(this.menuItem_debug_mode_Click);
            // 
            // menuItem_dump_mode
            // 
            this.menuItem_dump_mode.Name = "menuItem_dump_mode";
            this.menuItem_dump_mode.Size = new System.Drawing.Size(177, 22);
            this.menuItem_dump_mode.Text = "Client Dump Mode";
            this.menuItem_dump_mode.Click += new System.EventHandler(this.menuItem_dump_mode_Click);
            // 
            // menuItem_dump_mode_server
            // 
            this.menuItem_dump_mode_server.Name = "menuItem_dump_mode_server";
            this.menuItem_dump_mode_server.Size = new System.Drawing.Size(177, 22);
            this.menuItem_dump_mode_server.Text = "Server Dump Mode";
            this.menuItem_dump_mode_server.Click += new System.EventHandler(this.menuItem_dump_mode_server_Click);
            // 
            // toolStrip_pck
            // 
            this.toolStrip_pck.Name = "toolStrip_pck";
            this.toolStrip_pck.Size = new System.Drawing.Size(177, 22);
            this.toolStrip_pck.Text = "Packet Window";
            this.toolStrip_pck.Click += new System.EventHandler(this.toolStrip_pck_Click);
            // 
            // menuItem_Help
            // 
            this.menuItem_Help.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_about,
            this.eULAToolStripMenuItem,
            this.menuItem_forums,
            this.menuItem_help_donate,
            this.menuitem_help_checkforupdates,
            this.toolStripSeparator7,
            this.menuItem_language,
            this.menuItem_hosts,
            this.toolStripSeparator11,
            this.menuItem_forcecollect});
            this.menuItem_Help.Name = "menuItem_Help";
            this.menuItem_Help.Size = new System.Drawing.Size(44, 20);
            this.menuItem_Help.Text = "&Help";
            // 
            // menuItem_about
            // 
            this.menuItem_about.Name = "menuItem_about";
            this.menuItem_about.Size = new System.Drawing.Size(173, 22);
            this.menuItem_about.Text = "About";
            this.menuItem_about.Click += new System.EventHandler(this.menuItem_about_Click);
            // 
            // eULAToolStripMenuItem
            // 
            this.eULAToolStripMenuItem.Name = "eULAToolStripMenuItem";
            this.eULAToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.eULAToolStripMenuItem.Text = "EULA";
            this.eULAToolStripMenuItem.Click += new System.EventHandler(this.eULAToolStripMenuItem_Click);
            // 
            // menuItem_forums
            // 
            this.menuItem_forums.Name = "menuItem_forums";
            this.menuItem_forums.Size = new System.Drawing.Size(173, 22);
            this.menuItem_forums.Text = "Forums";
            this.menuItem_forums.Click += new System.EventHandler(this.menuItem_forums_Click);
            // 
            // menuItem_help_donate
            // 
            this.menuItem_help_donate.Name = "menuItem_help_donate";
            this.menuItem_help_donate.Size = new System.Drawing.Size(173, 22);
            this.menuItem_help_donate.Text = "Donate";
            this.menuItem_help_donate.Click += new System.EventHandler(this.menuItem_help_donate_Click);
            // 
            // menuitem_help_checkforupdates
            // 
            this.menuitem_help_checkforupdates.Name = "menuitem_help_checkforupdates";
            this.menuitem_help_checkforupdates.Size = new System.Drawing.Size(173, 22);
            this.menuitem_help_checkforupdates.Text = "Check for Updates";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(170, 6);
            // 
            // menuItem_language
            // 
            this.menuItem_language.Name = "menuItem_language";
            this.menuItem_language.Size = new System.Drawing.Size(173, 22);
            this.menuItem_language.Text = "Language";
            this.menuItem_language.Click += new System.EventHandler(this.menuItem_language_Click);
            // 
            // menuItem_hosts
            // 
            this.menuItem_hosts.Name = "menuItem_hosts";
            this.menuItem_hosts.Size = new System.Drawing.Size(173, 22);
            this.menuItem_hosts.Text = "Hosts";
            this.menuItem_hosts.Click += new System.EventHandler(this.menuItem_hosts_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(170, 6);
            // 
            // menuItem_forcecollect
            // 
            this.menuItem_forcecollect.Name = "menuItem_forcecollect";
            this.menuItem_forcecollect.Size = new System.Drawing.Size(173, 22);
            this.menuItem_forcecollect.Text = "Force Collect";
            this.menuItem_forcecollect.Click += new System.EventHandler(this.menuItem_forcecollect_Click);
            // 
            // automaticUpdater1
            // 
            this.automaticUpdater1.ContainerForm = this;
            this.automaticUpdater1.GUID = "c2d56b23-7b32-472d-94b5-5ede3926dfe2";
            this.automaticUpdater1.Location = new System.Drawing.Point(636, 4);
            this.automaticUpdater1.Name = "automaticUpdater1";
            this.automaticUpdater1.Size = new System.Drawing.Size(16, 16);
            this.automaticUpdater1.TabIndex = 29;
            this.automaticUpdater1.ToolStripItem = this.menuitem_help_checkforupdates;
            this.automaticUpdater1.wyUpdateCommandline = null;
            this.automaticUpdater1.wyUpdateLocation = "L2NetUpdater.exe";
            // 
            // disconectClientToolStripMenuItem
            // 
            this.disconectClientToolStripMenuItem.Name = "disconectClientToolStripMenuItem";
            this.disconectClientToolStripMenuItem.Size = new System.Drawing.Size(201, 22);
            this.disconectClientToolStripMenuItem.Text = "Disconect Client";
            this.disconectClientToolStripMenuItem.Click += new System.EventHandler(this.disconectClientToolStripMenuItem_Click);
            // 
            // L2NET
            // 
            this.AcceptButton = this.button_sendtext;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1016, 659);
            this.Controls.Add(this.automaticUpdater1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.panel_char);
            this.Controls.Add(this.panel_charinfo);
            this.Controls.Add(this.panel_chat);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "L2NET";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "no name";
            this.panel_party_5.ResumeLayout(false);
            this.panel_party_6.ResumeLayout(false);
            this.panel_party_7.ResumeLayout(false);
            this.panel_party_8.ResumeLayout(false);
            this.panel_party_4.ResumeLayout(false);
            this.panel_party_3.ResumeLayout(false);
            this.panel_party_2.ResumeLayout(false);
            this.panel_party_1.ResumeLayout(false);
            this.panel_charinfo.ResumeLayout(false);
            this.panel_target.ResumeLayout(false);
            this.panel_charinfo_ul.ResumeLayout(false);
            this.panel_char.ResumeLayout(false);
            this.tabControl_char.ResumeLayout(false);
            this.tabPage_char_info.ResumeLayout(false);
            this.panel_dead.ResumeLayout(false);
            this.panel_yesno.ResumeLayout(false);
            this.tabPage_char_inv.ResumeLayout(false);
            this.tabPage_char_inv.PerformLayout();
            this.contextMenuStrip_inventory.ResumeLayout(false);
            this.tabPage_char_skills.ResumeLayout(false);
            this.tabPage_char_skills.PerformLayout();
            this.tabPage_char_clan.ResumeLayout(false);
            this.tabPage_char_clan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_clan_crest)).EndInit();
            this.tabPage_char_detail.ResumeLayout(false);
            this.tabPage_players.ResumeLayout(false);
            this.tabPage_items.ResumeLayout(false);
            this.contextMenuStrip_Items.ResumeLayout(false);
            this.tabPage_npc.ResumeLayout(false);
            this.contextMenuStrip_NPC.ResumeLayout(false);
            this.tabPage_npc_chat.ResumeLayout(false);
            this.panel_npc_chat.ResumeLayout(false);
            this.panel_npc_chat.PerformLayout();
            this.tabPage_buffs.ResumeLayout(false);
            this.tabPage_stats.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_map_zoom)).EndInit();
            this.panel_chat.ResumeLayout(false);
            this.panel_chat.PerformLayout();
            this.tabControl_ChatSelect.ResumeLayout(false);
            this.tab_all.ResumeLayout(false);
            this.tab_system.ResumeLayout(false);
            this.tab_bot.ResumeLayout(false);
            this.tab_local.ResumeLayout(false);
            this.tab_trade.ResumeLayout(false);
            this.tab_party.ResumeLayout(false);
            this.tab_clan.ResumeLayout(false);
            this.tab_alliance.ResumeLayout(false);
            this.tab_hero.ResumeLayout(false);
            this.contextMenuStrip_notify.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.automaticUpdater1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/*********************/
	
		private void menuItem_about_Click(object sender, System.EventArgs e)
		{
			About about = new About();
			about.ShowDialog();
		}

		private void Exit()
		{
			this.Close();
		}

		private void menuItem_exit_Click(object sender, System.EventArgs e)
		{
			Exit();
		}

		private void menuItem_closeclient_Click(object sender, System.EventArgs e)
		{
			if(!Globals.gamedata.OOG)
			{
				//need to prompt if they really want to
				if (MessageBox.Show("Are you sure you want to close the client?","Close Client Verification",MessageBoxButtons.YesNo) == DialogResult.No)
				{
					return;
				}

				//close the connection to the client
				//but keep the game connection going so they can bot oog
				//
                ByteBuffer close_client = new ByteBuffer(1);
                close_client.WriteByte((byte)PServer.LogOutOk);
                Globals.gamedata.SendToClient(close_client);

                System.Threading.Thread.Sleep(250);

                Globals.gamedata.OOG = true;

				try
				{
                    Globals.Game_ClientLink.Stop();
				}
				catch
				{
					Globals.l2net_home.Add_Error("ERROR closing clientlink...");
				}
			}
		}

		private void L2NET_Closing(object sender, System.ComponentModel.CancelEventArgs cancel)
		{
			VoicePlayer.PlaySound(2);

            if (Globals.IgnoreExitConf == false)
            {
                if (MessageBox.Show("Are you want quit?", "Quit Verification", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    cancel.Cancel = true;
                    return;
                }
            }

			//need to clean up or w/e I guess
			Util.Free_Assets();
		}

		private void menuItem_forcecollect_Click(object sender, System.EventArgs e)
		{
			Util.Force_Collect();
		}

		private void notifyIcon_us_DoubleClick(object sender, EventArgs e)
		{
			if (this.WindowState == FormWindowState.Minimized)
				this.WindowState = FormWindowState.Normal;

            this.Menu = Globals.back_menu;
		}

		private void L2NET_SizeChanged(object sender, EventArgs e)
		{
			//
			if(this.WindowState == System.Windows.Forms.FormWindowState.Minimized)
			{
                if (Globals.MinimizeToTray)
				{
                    Globals.back_menu = this.Menu;
                    
                    //this.Hide();
                    notifyIcon_us.Visible = true;
					this.ShowInTaskbar = false;
				}
			}
			else
			{
				notifyIcon_us.Visible = false;
				this.ShowInTaskbar = true;

				//this.Refresh();
			}
		}

        delegate void SetName_Callback();
        public void SetName()
        {
            SetName(null);
        }

        public void SetName(string nm)
        {
            if (nm == null && !Globals.gamedata.botoptions.CustomWindowTitle)
            {
                if (this.InvokeRequired)
                {
                    SetName_Callback d = new SetName_Callback(SetName);
                    this.Invoke(d);
                    return;
                }

#if DEBUG
            if (Globals.gamedata.my_char.Name.Length != 0)
            {
                this.Text = Globals.gamedata.my_char.Name + ": " + Globals.Name + " rev " + Globals.Version + Globals.VersionLetter + " - DEBUG";
                notifyIcon_us.Text = Globals.gamedata.my_char.Name + ": " + Globals.Name + " rev " + Globals.Version + Globals.VersionLetter + " - DEBUG";
            }
            else
            {
                this.Text = Globals.Name + " rev " + Globals.Version + Globals.VersionLetter + " - DEBUG";
                notifyIcon_us.Text = Globals.Name + " rev " + Globals.Version + Globals.VersionLetter + " - DEBUG";
            }
#else
                if (Globals.gamedata.my_char.Name.Length != 0)
                {
                    this.Text = Globals.gamedata.my_char.Name + ": " + Globals.Name + " rev " + Globals.Version + Globals.VersionLetter;
                    notifyIcon_us.Text = Globals.gamedata.my_char.Name + ": " + Globals.Name + " rev " + Globals.Version + Globals.VersionLetter;
                }
                else
                {
                    this.Text = Globals.Name + " rev " + Globals.Version + Globals.VersionLetter;
                    notifyIcon_us.Text = Globals.Name + " rev " + Globals.Version + Globals.VersionLetter;
                }
#endif

                label_char_name.Text = Globals.gamedata.my_char.Name;
                label_info_name.Text = Globals.gamedata.my_char.Name;
            }
            else
            {
                this.Text = Globals.gamedata.botoptions.CustomWindowTitle_Text;
                notifyIcon_us.Text = Globals.gamedata.botoptions.CustomWindowTitle_Text;
                label_char_name.Text = Globals.gamedata.my_char.Name;
                label_info_name.Text = Globals.gamedata.my_char.Name;
            }
        }

		private void L2NET_GotFocus(object sender, EventArgs e)
		{
			this.Refresh();
		}

        void richTextBox_dialog_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            //clicked a link in the npc dialog
            panel_npc_chat.Hide();

            int pnd = e.LinkText.IndexOf('#');

            string link = e.LinkText.Substring(pnd+1);

            if (link.ToUpperInvariant().Contains("CAPTCHA"))
            {
                link = "captcha " + Globals.l2net_home.textBox_rtb_input.Text;
                Globals.l2net_home.textBox_rtb_input.Text = "";
                Globals.l2net_home.textBox_rtb_input.Visible = false;
            }

            ServerPackets.NPC_Chat_Click(link);
        }

        private void menuItem_launchl2_Click(object sender, EventArgs e)
        {
            try
            {
                //need to run the lineage 2 client
                System.Diagnostics.Process conv = new System.Diagnostics.Process();
                conv.StartInfo.FileName = Globals.L2Path;
                conv.Start();
            }
            catch
            {
                Globals.l2net_home.Add_Error("Failed to launch Lineage 2! Invalid path?");
            }
        }

        private void button_close_accept_Click(object sender, EventArgs e)
        {
            Hide_YesNo();
        }

        private void button1_close_dead_Click(object sender, EventArgs e)
        {
            Hide_Dead();
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Exit();
        }

        private void toggleBottingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Toggle_Botting();
        }

        private void botOptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menuItem_Options_Click(sender, e);
        }

        private void radiobutton_active_CheckedChanged(object sender, EventArgs e)
        {
            if (radiobutton_active.Checked)
            {
                Globals.show_active_skills = true;
                AddInfo.Set_SkillList(1);
            }
            else
            {
                Globals.show_active_skills = false;
                AddInfo.Set_SkillList(0);
            }

        }

        private void radioButton_inv_items_CheckedChanged(object sender, EventArgs e)
        {
            ServerPackets.RequestInventory();
        }

        private void radioButton_inv_equipped_CheckedChanged(object sender, EventArgs e)
        {
            ServerPackets.RequestInventory();
        }

        private void radioButton_inv_quest_CheckedChanged(object sender, EventArgs e)
        {
            ServerPackets.RequestInventory();
        }

        private void petWindowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.petwindow == null || Globals.petwindow.IsDisposed == true)
            {
                Globals.petwindow = new PetWindow();
            }
            Globals.petwindow.TopMost = true;
            Globals.petwindow.BringToFront();
            Globals.petwindow.Show();
        }

        private void extendedActionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.extendedactionwindow == null || Globals.extendedactionwindow.IsDisposed == true)
            {
                Globals.extendedactionwindow = new ExtendedActionWindow();
            }
            Globals.extendedactionwindow.TopMost = true;
            Globals.extendedactionwindow.BringToFront();
            Globals.extendedactionwindow.Show();
        }

        private void toolStrip_pck_Click(object sender, EventArgs e)
        {
            if (Globals.pck_thread.pck_window == null || Globals.pck_thread.pck_window.IsDisposed == true)
            {
                Globals.pck_thread.pck_window = new packet_window();
             }
           
            //Globals.pck_thread.pck_window.TopMost = true;
            Globals.pck_thread.pck_window.BringToFront();
            Globals.pck_thread.pck_window.Show();
            if (!Globals.pck_thread.run)
            {
                Globals.pck_thread.start();
            }
            //Globals.pck_thread.pck_window.refresh_window();
        }

        private void menuitem_cmd_GG_Click(object sender, EventArgs e)
        {
            if (Globals.ggwindow == null || Globals.ggwindow.IsDisposed == true)
            {
                Globals.ggwindow = new GameGuardServer();
            }
            Globals.ggwindow.TopMost = true;
            Globals.ggwindow.BringToFront();
            Globals.ggwindow.Show();
        }

        private void menuitem_cmd_ggclient_Click(object sender, EventArgs e)
        {
            if (Globals.ggclientwindow == null || Globals.ggclientwindow.IsDisposed == true)
            {
                Globals.ggclientwindow = new GameGuardClient();
            }
            Globals.ggclientwindow.TopMost = true;
            Globals.ggclientwindow.BringToFront();
            Globals.ggclientwindow.Show();
        }


        private void button_clear_stats_Click(object sender, EventArgs e)
        {
            label_Adena.Text = "0";
            label_XP.Text = "0";
            label_SP.Text = "0";
            label_badmobs.Text = "0";
            label_meshlessignored.Text = "0";
            label_adena_total.Text = "0";

            GameData.initial_Adena = 0;
            GameData.current_Adena = 0;
            GameData.initial_XP = 0;
            GameData.initial_SP = 0;
            GameData.badmobs_ignored = 0;
            GameData.meshless_ignored = 0;

            Globals.gamedata.initial_XP_Gained_received = false;
            Globals.gamedata.initial_SP_Gained_received = false;
            Globals.gamedata.initial_Adena_Gained_received = false;
            Globals.l2net_home.listView_stats.Items.Clear();

        }

        private void DevToolz_Click(object sender, EventArgs e)
        {
            ByteBuffer buff = new ByteBuffer(37);
            buff.WriteByte((byte)PServer.SpawnItem);
            buff.WriteUInt32(Globals.gamedata.my_char.ID);
            buff.WriteUInt32(1152295325);
            buff.WriteUInt32(1231212312);
            buff.WriteInt32(Util.Float_Int32(Globals.gamedata.my_char.X));

            buff.WriteInt32(Util.Float_Int32(Globals.gamedata.my_char.Y));
            buff.WriteInt32(Util.Float_Int32(Globals.gamedata.my_char.Z));
            buff.WriteUInt32(1);
            buff.WriteUInt32(184467440);
            buff.WriteUInt32(0);

            buff.ResetIndex();
            buff.ReadByte();
            ClientPackets.ItemDrop(buff);
            buff.ResetIndex();
            //Globals.gamedata.SendToClient(buff);
        }

        private void disconectClientToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Globals.gamedata.OOG)
            {
                //need to prompt if they really want to
                if (MessageBox.Show("Are you sure you want to disconect the client?", "Disconect Client Verification", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }

                Globals.gamedata.OOG = true;

                try
                {
                    Globals.Game_ClientLink.Stop();
                    Globals.Game_ClientSocket.Close();
                }
                catch
                {
                    Globals.l2net_home.Add_Error("ERROR closing client connection...");
                }
            }
        }






	}//end of class
}//end of namespace