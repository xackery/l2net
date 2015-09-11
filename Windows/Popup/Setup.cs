using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace L2_login
{
	/// <summary>
	/// Summary description for Setup.
	/// </summary>
	public class Setup : System.Windows.Forms.Form
	{
		public System.Windows.Forms.ComboBox comboBox_voice;
		private System.Windows.Forms.Label label_voice;
		private System.Windows.Forms.CheckBox checkBox_MinToTray;
		private System.Windows.Forms.Button button_cancel;
		private System.Windows.Forms.Button button_save;
		public System.Windows.Forms.TextBox textBox_key;
		private System.Windows.Forms.Label label_productkey;
        private CheckBox checkBox_scriptIO;
        private Label label_l2_path;
        public TextBox textBox_l2path;
        private Label label_directinput;
        private CheckBox checkBox_white_names;
        public Label label_toggle_key;
        public Button button_change_toggle;
        private CheckBox checkBox_supress_quakes;
        private CheckBox checkBox_blank_gg;
        private CheckBox checkBox_hide_mess;
        public ComboBox comboBox_texturemode;
        private Label label_texturemode;
        private Label label_viewrange;
        public ComboBox comboBox_viewrange;
        public CheckBox checkBox_downloadcrests;
        public CheckBox checkBox_social_npcs;
        public CheckBox checkBox_social_self;
        public CheckBox checkBox_social_pcs;
        public CheckBox checkBox_shownames_items;
        public CheckBox checkBox_shownames_players;
        public CheckBox checkBox_shownames_npcs;
        public CheckBox checkBox_IgnoreExitConf;
        public Button button_change_kill;
        public Label label_kill_key;
        private Label label_directinput2;
        private CheckBox checkBox_script386;
        private CheckBox checkBox_usegpu;
        private CheckBox net_dc_on_ig_crit;
        private CheckBox net_dump_on_ig_crit;
        public CheckBox checkBox_ToggleBottingIfGMAction;
        private CheckBox checkBox_togglebotifported;
        private CheckBox checkBox_captcha_popup;
        private TextBox textBox_captcha_html1;
        private Label label_captcha_html;
        private TextBox textBox_captcha_html2;
        private CheckBox checkBox_AutolearnSkills;
        private CheckBox checkBox_writetolog;
        public CheckBox checkBox_npc_say;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Setup()
		{
			InitializeComponent();

			UpdateUI();
            comboBox_voice.SelectedIndex = Globals.Voice;
            checkBox_MinToTray.Checked = Globals.MinimizeToTray;
            textBox_key.Text = Globals.ProductKey;
            checkBox_scriptIO.Checked = Globals.AllowFiles;
            checkBox_script386.Checked = Globals.ScriptCompatibilityv386;
            textBox_l2path.Text = Globals.L2Path;
            label_toggle_key.Text = Globals.DirectInputKey;
            label_kill_key.Text = Globals.DirectInputKey2;
            checkBox_white_names.Checked = Globals.White_Names;
            checkBox_supress_quakes.Checked = Globals.Suppress_Quakes;
            checkBox_blank_gg.Checked = Globals.Send_Blank_GG;
            checkBox_hide_mess.Checked = Globals.Hide_Message_Boxes;
            comboBox_texturemode.SelectedIndex = Globals.Texture_Mode;
            comboBox_viewrange.SelectedIndex = Globals.ViewRange;
            checkBox_usegpu.Checked = Globals.Use_Hardware_Acceleration;
            checkBox_AutolearnSkills.Checked = Globals.AutolearnSkills;
            checkBox_writetolog.Checked = Globals.LogWriting;
            checkBox_captcha_popup.Checked = Globals.Popup_Captcha;
            textBox_captcha_html1.Text = Globals.Captcha_HTML1;
            textBox_captcha_html2.Text = Globals.Captcha_HTML2;

            checkBox_downloadcrests.Checked = Globals.DownloadNewCrests;
            checkBox_social_self.Checked = Globals.SocialSelf;
            checkBox_social_pcs.Checked = Globals.SocialPcs;
            checkBox_social_npcs.Checked = Globals.SocialNpcs;
            checkBox_npc_say.Checked = Globals.NpcSay;
            checkBox_shownames_npcs.Checked = Globals.ShowNamesNpcs;
            checkBox_shownames_players.Checked = Globals.ShowNamesPcs;
            checkBox_shownames_items.Checked = Globals.ShowNamesItems;
            checkBox_IgnoreExitConf.Checked = Globals.IgnoreExitConf;
            checkBox_ToggleBottingIfGMAction.Checked = Globals.ToggleBottingifGMAction;
            checkBox_togglebotifported.Checked = Globals.ToggleBottingifTeleported;

            net_dc_on_ig_crit.Checked = Globals.dc_on_ig_close;
            net_dump_on_ig_crit.Checked = Globals.dump_pbuff_on_ig_close;

		}

		public void UpdateUI()
		{
            button_save.Text = Globals.m_ResourceManager.GetString("Apply");
            button_cancel.Text = Globals.m_ResourceManager.GetString("button_cancel");

            label_voice.Text = Globals.m_ResourceManager.GetString("voice");
            checkBox_MinToTray.Text = Globals.m_ResourceManager.GetString("min_to_tray");
            checkBox_scriptIO.Text = Globals.m_ResourceManager.GetString("script_io");
            label_l2_path.Text = Globals.m_ResourceManager.GetString("l2_path");
            label_directinput.Text = Globals.m_ResourceManager.GetString("toggle_key");
            label_productkey.Text = Globals.m_ResourceManager.GetString("product_key");
            //checkBox_blank_gg.Text = Globals.m_ResourceManager.GetString("toggle_key");
            //checkBox_hide_mess.Text = Globals.m_ResourceManager.GetString("product_key");

            checkBox_downloadcrests.Text = Globals.m_ResourceManager.GetString("checkBox_downloadcrests");
            checkBox_social_self.Text = Globals.m_ResourceManager.GetString("checkBox_social_self");
            checkBox_social_pcs.Text = Globals.m_ResourceManager.GetString("checkBox_social_pcs");
            checkBox_social_npcs.Text = Globals.m_ResourceManager.GetString("checkBox_social_npcs");
            checkBox_shownames_npcs.Text = Globals.m_ResourceManager.GetString("show_names") + " - " + Globals.m_ResourceManager.GetString("tab_NPC");
            checkBox_shownames_players.Text = Globals.m_ResourceManager.GetString("show_names") + " - " + Globals.m_ResourceManager.GetString("tab_Players");
            checkBox_shownames_items.Text = Globals.m_ResourceManager.GetString("show_names") + " - " + Globals.m_ResourceManager.GetString("tab_Items");
            checkBox_IgnoreExitConf.Text = Globals.m_ResourceManager.GetString("check_ignexitconf");

            this.Refresh();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.comboBox_voice = new System.Windows.Forms.ComboBox();
            this.label_voice = new System.Windows.Forms.Label();
            this.checkBox_MinToTray = new System.Windows.Forms.CheckBox();
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_save = new System.Windows.Forms.Button();
            this.textBox_key = new System.Windows.Forms.TextBox();
            this.label_productkey = new System.Windows.Forms.Label();
            this.checkBox_scriptIO = new System.Windows.Forms.CheckBox();
            this.label_l2_path = new System.Windows.Forms.Label();
            this.textBox_l2path = new System.Windows.Forms.TextBox();
            this.label_directinput = new System.Windows.Forms.Label();
            this.checkBox_white_names = new System.Windows.Forms.CheckBox();
            this.label_toggle_key = new System.Windows.Forms.Label();
            this.button_change_toggle = new System.Windows.Forms.Button();
            this.checkBox_supress_quakes = new System.Windows.Forms.CheckBox();
            this.checkBox_blank_gg = new System.Windows.Forms.CheckBox();
            this.checkBox_hide_mess = new System.Windows.Forms.CheckBox();
            this.comboBox_texturemode = new System.Windows.Forms.ComboBox();
            this.label_texturemode = new System.Windows.Forms.Label();
            this.label_viewrange = new System.Windows.Forms.Label();
            this.comboBox_viewrange = new System.Windows.Forms.ComboBox();
            this.checkBox_downloadcrests = new System.Windows.Forms.CheckBox();
            this.checkBox_social_npcs = new System.Windows.Forms.CheckBox();
            this.checkBox_social_self = new System.Windows.Forms.CheckBox();
            this.checkBox_social_pcs = new System.Windows.Forms.CheckBox();
            this.checkBox_shownames_items = new System.Windows.Forms.CheckBox();
            this.checkBox_shownames_players = new System.Windows.Forms.CheckBox();
            this.checkBox_shownames_npcs = new System.Windows.Forms.CheckBox();
            this.checkBox_IgnoreExitConf = new System.Windows.Forms.CheckBox();
            this.button_change_kill = new System.Windows.Forms.Button();
            this.label_kill_key = new System.Windows.Forms.Label();
            this.label_directinput2 = new System.Windows.Forms.Label();
            this.checkBox_script386 = new System.Windows.Forms.CheckBox();
            this.checkBox_usegpu = new System.Windows.Forms.CheckBox();
            this.net_dc_on_ig_crit = new System.Windows.Forms.CheckBox();
            this.net_dump_on_ig_crit = new System.Windows.Forms.CheckBox();
            this.checkBox_ToggleBottingIfGMAction = new System.Windows.Forms.CheckBox();
            this.checkBox_togglebotifported = new System.Windows.Forms.CheckBox();
            this.checkBox_captcha_popup = new System.Windows.Forms.CheckBox();
            this.textBox_captcha_html1 = new System.Windows.Forms.TextBox();
            this.label_captcha_html = new System.Windows.Forms.Label();
            this.textBox_captcha_html2 = new System.Windows.Forms.TextBox();
            this.checkBox_AutolearnSkills = new System.Windows.Forms.CheckBox();
            this.checkBox_writetolog = new System.Windows.Forms.CheckBox();
            this.checkBox_npc_say = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // comboBox_voice
            // 
            this.comboBox_voice.Items.AddRange(new object[] {
            "None",
            "Angelica",
            "Claes",
            "Henrietta",
            "Rico",
            "Triela",
            "Chinatsu",
            "Miu"});
            this.comboBox_voice.Location = new System.Drawing.Point(128, 12);
            this.comboBox_voice.Name = "comboBox_voice";
            this.comboBox_voice.Size = new System.Drawing.Size(144, 21);
            this.comboBox_voice.TabIndex = 0;
            // 
            // label_voice
            // 
            this.label_voice.Location = new System.Drawing.Point(8, 12);
            this.label_voice.Name = "label_voice";
            this.label_voice.Size = new System.Drawing.Size(114, 23);
            this.label_voice.TabIndex = 1;
            this.label_voice.Text = "Voice";
            this.label_voice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBox_MinToTray
            // 
            this.checkBox_MinToTray.Location = new System.Drawing.Point(17, 39);
            this.checkBox_MinToTray.Name = "checkBox_MinToTray";
            this.checkBox_MinToTray.Size = new System.Drawing.Size(269, 24);
            this.checkBox_MinToTray.TabIndex = 1;
            this.checkBox_MinToTray.Text = "Minimize to Tray";
            // 
            // button_cancel
            // 
            this.button_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_cancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_cancel.Location = new System.Drawing.Point(465, 408);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(96, 24);
            this.button_cancel.TabIndex = 8;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // button_save
            // 
            this.button_save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_save.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_save.Location = new System.Drawing.Point(8, 408);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(96, 24);
            this.button_save.TabIndex = 7;
            this.button_save.Text = "Apply";
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // textBox_key
            // 
            this.textBox_key.Location = new System.Drawing.Point(132, 156);
            this.textBox_key.MaxLength = 19;
            this.textBox_key.Name = "textBox_key";
            this.textBox_key.Size = new System.Drawing.Size(144, 20);
            this.textBox_key.TabIndex = 4;
            this.textBox_key.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_productkey
            // 
            this.label_productkey.Location = new System.Drawing.Point(12, 153);
            this.label_productkey.Name = "label_productkey";
            this.label_productkey.Size = new System.Drawing.Size(114, 23);
            this.label_productkey.TabIndex = 21;
            this.label_productkey.Text = "Product Key";
            this.label_productkey.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBox_scriptIO
            // 
            this.checkBox_scriptIO.Location = new System.Drawing.Point(301, 12);
            this.checkBox_scriptIO.Name = "checkBox_scriptIO";
            this.checkBox_scriptIO.Size = new System.Drawing.Size(269, 24);
            this.checkBox_scriptIO.TabIndex = 2;
            this.checkBox_scriptIO.Text = "Allow Script I/O";
            // 
            // label_l2_path
            // 
            this.label_l2_path.Location = new System.Drawing.Point(8, 78);
            this.label_l2_path.Name = "label_l2_path";
            this.label_l2_path.Size = new System.Drawing.Size(114, 23);
            this.label_l2_path.TabIndex = 24;
            this.label_l2_path.Text = "Lineage 2 Path";
            this.label_l2_path.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_l2path
            // 
            this.textBox_l2path.Location = new System.Drawing.Point(128, 78);
            this.textBox_l2path.MaxLength = 256;
            this.textBox_l2path.Name = "textBox_l2path";
            this.textBox_l2path.Size = new System.Drawing.Size(144, 20);
            this.textBox_l2path.TabIndex = 3;
            this.textBox_l2path.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_directinput
            // 
            this.label_directinput.Location = new System.Drawing.Point(8, 107);
            this.label_directinput.Name = "label_directinput";
            this.label_directinput.Size = new System.Drawing.Size(114, 23);
            this.label_directinput.TabIndex = 25;
            this.label_directinput.Text = "Toggle Key";
            this.label_directinput.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBox_white_names
            // 
            this.checkBox_white_names.Location = new System.Drawing.Point(17, 249);
            this.checkBox_white_names.Name = "checkBox_white_names";
            this.checkBox_white_names.Size = new System.Drawing.Size(269, 24);
            this.checkBox_white_names.TabIndex = 5;
            this.checkBox_white_names.Text = "White Names";
            // 
            // label_toggle_key
            // 
            this.label_toggle_key.Location = new System.Drawing.Point(128, 98);
            this.label_toggle_key.Name = "label_toggle_key";
            this.label_toggle_key.Size = new System.Drawing.Size(115, 26);
            this.label_toggle_key.TabIndex = 4;
            this.label_toggle_key.Text = "-none-";
            this.label_toggle_key.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_change_toggle
            // 
            this.button_change_toggle.Location = new System.Drawing.Point(249, 100);
            this.button_change_toggle.Name = "button_change_toggle";
            this.button_change_toggle.Size = new System.Drawing.Size(39, 23);
            this.button_change_toggle.TabIndex = 3;
            this.button_change_toggle.Text = "X";
            this.button_change_toggle.Click += new System.EventHandler(this.button_change_toggle_Click);
            // 
            // checkBox_supress_quakes
            // 
            this.checkBox_supress_quakes.Location = new System.Drawing.Point(301, 106);
            this.checkBox_supress_quakes.Name = "checkBox_supress_quakes";
            this.checkBox_supress_quakes.Padding = new System.Windows.Forms.Padding(2);
            this.checkBox_supress_quakes.Size = new System.Drawing.Size(269, 24);
            this.checkBox_supress_quakes.TabIndex = 6;
            this.checkBox_supress_quakes.Text = "Suppress Earthquakes";
            // 
            // checkBox_blank_gg
            // 
            this.checkBox_blank_gg.Location = new System.Drawing.Point(301, 127);
            this.checkBox_blank_gg.Name = "checkBox_blank_gg";
            this.checkBox_blank_gg.Padding = new System.Windows.Forms.Padding(2);
            this.checkBox_blank_gg.Size = new System.Drawing.Size(269, 24);
            this.checkBox_blank_gg.TabIndex = 26;
            this.checkBox_blank_gg.Text = "Send Blank Reply for Unknown GG Queries";
            // 
            // checkBox_hide_mess
            // 
            this.checkBox_hide_mess.Location = new System.Drawing.Point(301, 148);
            this.checkBox_hide_mess.Name = "checkBox_hide_mess";
            this.checkBox_hide_mess.Padding = new System.Windows.Forms.Padding(2);
            this.checkBox_hide_mess.Size = new System.Drawing.Size(269, 24);
            this.checkBox_hide_mess.TabIndex = 27;
            this.checkBox_hide_mess.Text = "Hide Message Boxes";
            // 
            // comboBox_texturemode
            // 
            this.comboBox_texturemode.Items.AddRange(new object[] {
            "None",
            "Linear",
            "Gaussian"});
            this.comboBox_texturemode.Location = new System.Drawing.Point(128, 304);
            this.comboBox_texturemode.Name = "comboBox_texturemode";
            this.comboBox_texturemode.Size = new System.Drawing.Size(144, 21);
            this.comboBox_texturemode.TabIndex = 28;
            // 
            // label_texturemode
            // 
            this.label_texturemode.Location = new System.Drawing.Point(8, 302);
            this.label_texturemode.Name = "label_texturemode";
            this.label_texturemode.Size = new System.Drawing.Size(114, 23);
            this.label_texturemode.TabIndex = 29;
            this.label_texturemode.Text = "Texture Mode";
            this.label_texturemode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_viewrange
            // 
            this.label_viewrange.Location = new System.Drawing.Point(8, 329);
            this.label_viewrange.Name = "label_viewrange";
            this.label_viewrange.Size = new System.Drawing.Size(114, 23);
            this.label_viewrange.TabIndex = 31;
            this.label_viewrange.Text = "View Range";
            this.label_viewrange.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox_viewrange
            // 
            this.comboBox_viewrange.Items.AddRange(new object[] {
            "Local",
            "Nearby",
            "All"});
            this.comboBox_viewrange.Location = new System.Drawing.Point(128, 331);
            this.comboBox_viewrange.Name = "comboBox_viewrange";
            this.comboBox_viewrange.Size = new System.Drawing.Size(144, 21);
            this.comboBox_viewrange.TabIndex = 30;
            // 
            // checkBox_downloadcrests
            // 
            this.checkBox_downloadcrests.Location = new System.Drawing.Point(301, 169);
            this.checkBox_downloadcrests.Name = "checkBox_downloadcrests";
            this.checkBox_downloadcrests.Padding = new System.Windows.Forms.Padding(2);
            this.checkBox_downloadcrests.Size = new System.Drawing.Size(269, 24);
            this.checkBox_downloadcrests.TabIndex = 32;
            this.checkBox_downloadcrests.Text = "download new crests";
            // 
            // checkBox_social_npcs
            // 
            this.checkBox_social_npcs.Location = new System.Drawing.Point(301, 190);
            this.checkBox_social_npcs.Name = "checkBox_social_npcs";
            this.checkBox_social_npcs.Padding = new System.Windows.Forms.Padding(2);
            this.checkBox_social_npcs.Size = new System.Drawing.Size(95, 24);
            this.checkBox_social_npcs.TabIndex = 35;
            this.checkBox_social_npcs.Text = "social npcs";
            // 
            // checkBox_social_self
            // 
            this.checkBox_social_self.Checked = true;
            this.checkBox_social_self.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_social_self.Location = new System.Drawing.Point(301, 211);
            this.checkBox_social_self.Name = "checkBox_social_self";
            this.checkBox_social_self.Padding = new System.Windows.Forms.Padding(2);
            this.checkBox_social_self.Size = new System.Drawing.Size(269, 24);
            this.checkBox_social_self.TabIndex = 33;
            this.checkBox_social_self.Text = "social self";
            // 
            // checkBox_social_pcs
            // 
            this.checkBox_social_pcs.Location = new System.Drawing.Point(301, 232);
            this.checkBox_social_pcs.Name = "checkBox_social_pcs";
            this.checkBox_social_pcs.Padding = new System.Windows.Forms.Padding(2);
            this.checkBox_social_pcs.Size = new System.Drawing.Size(269, 24);
            this.checkBox_social_pcs.TabIndex = 34;
            this.checkBox_social_pcs.Text = "social pcs";
            // 
            // checkBox_shownames_items
            // 
            this.checkBox_shownames_items.Checked = true;
            this.checkBox_shownames_items.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_shownames_items.Location = new System.Drawing.Point(301, 295);
            this.checkBox_shownames_items.Name = "checkBox_shownames_items";
            this.checkBox_shownames_items.Padding = new System.Windows.Forms.Padding(2);
            this.checkBox_shownames_items.Size = new System.Drawing.Size(269, 24);
            this.checkBox_shownames_items.TabIndex = 38;
            this.checkBox_shownames_items.Text = "Show Names Items";
            // 
            // checkBox_shownames_players
            // 
            this.checkBox_shownames_players.Checked = true;
            this.checkBox_shownames_players.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_shownames_players.Location = new System.Drawing.Point(301, 274);
            this.checkBox_shownames_players.Name = "checkBox_shownames_players";
            this.checkBox_shownames_players.Padding = new System.Windows.Forms.Padding(2);
            this.checkBox_shownames_players.Size = new System.Drawing.Size(269, 24);
            this.checkBox_shownames_players.TabIndex = 37;
            this.checkBox_shownames_players.Text = "Show Names Players";
            // 
            // checkBox_shownames_npcs
            // 
            this.checkBox_shownames_npcs.Checked = true;
            this.checkBox_shownames_npcs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_shownames_npcs.Location = new System.Drawing.Point(301, 253);
            this.checkBox_shownames_npcs.Name = "checkBox_shownames_npcs";
            this.checkBox_shownames_npcs.Padding = new System.Windows.Forms.Padding(2);
            this.checkBox_shownames_npcs.Size = new System.Drawing.Size(269, 24);
            this.checkBox_shownames_npcs.TabIndex = 36;
            this.checkBox_shownames_npcs.Text = "Show Names NPC\'s";
            // 
            // checkBox_IgnoreExitConf
            // 
            this.checkBox_IgnoreExitConf.Location = new System.Drawing.Point(301, 316);
            this.checkBox_IgnoreExitConf.Name = "checkBox_IgnoreExitConf";
            this.checkBox_IgnoreExitConf.Padding = new System.Windows.Forms.Padding(2);
            this.checkBox_IgnoreExitConf.Size = new System.Drawing.Size(269, 24);
            this.checkBox_IgnoreExitConf.TabIndex = 39;
            this.checkBox_IgnoreExitConf.Text = "Ignore Exit Confirmation";
            // 
            // button_change_kill
            // 
            this.button_change_kill.Location = new System.Drawing.Point(249, 123);
            this.button_change_kill.Name = "button_change_kill";
            this.button_change_kill.Size = new System.Drawing.Size(39, 23);
            this.button_change_kill.TabIndex = 40;
            this.button_change_kill.Text = "X";
            this.button_change_kill.Click += new System.EventHandler(this.button_change_kill_Click);
            // 
            // label_kill_key
            // 
            this.label_kill_key.Location = new System.Drawing.Point(128, 121);
            this.label_kill_key.Name = "label_kill_key";
            this.label_kill_key.Size = new System.Drawing.Size(115, 26);
            this.label_kill_key.TabIndex = 41;
            this.label_kill_key.Text = "-none-";
            this.label_kill_key.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_directinput2
            // 
            this.label_directinput2.Location = new System.Drawing.Point(8, 130);
            this.label_directinput2.Name = "label_directinput2";
            this.label_directinput2.Size = new System.Drawing.Size(114, 23);
            this.label_directinput2.TabIndex = 42;
            this.label_directinput2.Text = "Force Quit Key";
            this.label_directinput2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // checkBox_script386
            // 
            this.checkBox_script386.Location = new System.Drawing.Point(301, 33);
            this.checkBox_script386.Name = "checkBox_script386";
            this.checkBox_script386.Size = new System.Drawing.Size(269, 24);
            this.checkBox_script386.TabIndex = 43;
            this.checkBox_script386.Text = "Script Compatibility Mode v386";
            // 
            // checkBox_usegpu
            // 
            this.checkBox_usegpu.Checked = true;
            this.checkBox_usegpu.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_usegpu.Location = new System.Drawing.Point(17, 270);
            this.checkBox_usegpu.Name = "checkBox_usegpu";
            this.checkBox_usegpu.Size = new System.Drawing.Size(269, 24);
            this.checkBox_usegpu.TabIndex = 44;
            this.checkBox_usegpu.Text = "GPU Accelerated";
            // 
            // net_dc_on_ig_crit
            // 
            this.net_dc_on_ig_crit.AutoSize = true;
            this.net_dc_on_ig_crit.Location = new System.Drawing.Point(17, 209);
            this.net_dc_on_ig_crit.Name = "net_dc_on_ig_crit";
            this.net_dc_on_ig_crit.Size = new System.Drawing.Size(238, 17);
            this.net_dc_on_ig_crit.TabIndex = 45;
            this.net_dc_on_ig_crit.Text = "Disconnect on IG Crash (Check this for retail)";
            this.net_dc_on_ig_crit.UseVisualStyleBackColor = true;
            // 
            // net_dump_on_ig_crit
            // 
            this.net_dump_on_ig_crit.AutoSize = true;
            this.net_dump_on_ig_crit.Location = new System.Drawing.Point(17, 232);
            this.net_dump_on_ig_crit.Name = "net_dump_on_ig_crit";
            this.net_dump_on_ig_crit.Size = new System.Drawing.Size(187, 17);
            this.net_dump_on_ig_crit.TabIndex = 46;
            this.net_dump_on_ig_crit.Text = "Dump Network Buffer on IG Crash";
            this.net_dump_on_ig_crit.UseVisualStyleBackColor = true;
            // 
            // checkBox_ToggleBottingIfGMAction
            // 
            this.checkBox_ToggleBottingIfGMAction.Location = new System.Drawing.Point(301, 337);
            this.checkBox_ToggleBottingIfGMAction.Name = "checkBox_ToggleBottingIfGMAction";
            this.checkBox_ToggleBottingIfGMAction.Padding = new System.Windows.Forms.Padding(2);
            this.checkBox_ToggleBottingIfGMAction.Size = new System.Drawing.Size(269, 24);
            this.checkBox_ToggleBottingIfGMAction.TabIndex = 47;
            this.checkBox_ToggleBottingIfGMAction.Text = "Disable botting if GM action";
            // 
            // checkBox_togglebotifported
            // 
            this.checkBox_togglebotifported.AutoSize = true;
            this.checkBox_togglebotifported.Location = new System.Drawing.Point(301, 358);
            this.checkBox_togglebotifported.Name = "checkBox_togglebotifported";
            this.checkBox_togglebotifported.Padding = new System.Windows.Forms.Padding(2);
            this.checkBox_togglebotifported.Size = new System.Drawing.Size(158, 21);
            this.checkBox_togglebotifported.TabIndex = 48;
            this.checkBox_togglebotifported.Text = "Disable botting if teleported";
            this.checkBox_togglebotifported.UseVisualStyleBackColor = true;
            // 
            // checkBox_captcha_popup
            // 
            this.checkBox_captcha_popup.AutoSize = true;
            this.checkBox_captcha_popup.Location = new System.Drawing.Point(301, 58);
            this.checkBox_captcha_popup.Name = "checkBox_captcha_popup";
            this.checkBox_captcha_popup.Size = new System.Drawing.Size(264, 17);
            this.checkBox_captcha_popup.TabIndex = 49;
            this.checkBox_captcha_popup.Text = "Popup captcha images, HTML: (remember space) ";
            this.checkBox_captcha_popup.UseVisualStyleBackColor = true;
            // 
            // textBox_captcha_html1
            // 
            this.textBox_captcha_html1.Location = new System.Drawing.Point(301, 78);
            this.textBox_captcha_html1.Name = "textBox_captcha_html1";
            this.textBox_captcha_html1.Size = new System.Drawing.Size(81, 20);
            this.textBox_captcha_html1.TabIndex = 50;
            this.textBox_captcha_html1.Text = "captcha ";
            // 
            // label_captcha_html
            // 
            this.label_captcha_html.AutoSize = true;
            this.label_captcha_html.Location = new System.Drawing.Point(388, 83);
            this.label_captcha_html.Name = "label_captcha_html";
            this.label_captcha_html.Size = new System.Drawing.Size(67, 13);
            this.label_captcha_html.TabIndex = 51;
            this.label_captcha_html.Text = "+ captcha + ";
            // 
            // textBox_captcha_html2
            // 
            this.textBox_captcha_html2.Location = new System.Drawing.Point(461, 80);
            this.textBox_captcha_html2.Name = "textBox_captcha_html2";
            this.textBox_captcha_html2.Size = new System.Drawing.Size(100, 20);
            this.textBox_captcha_html2.TabIndex = 52;
            // 
            // checkBox_AutolearnSkills
            // 
            this.checkBox_AutolearnSkills.AutoSize = true;
            this.checkBox_AutolearnSkills.Location = new System.Drawing.Point(17, 190);
            this.checkBox_AutolearnSkills.Name = "checkBox_AutolearnSkills";
            this.checkBox_AutolearnSkills.Size = new System.Drawing.Size(98, 17);
            this.checkBox_AutolearnSkills.TabIndex = 53;
            this.checkBox_AutolearnSkills.Text = "Autolearn Skills";
            this.checkBox_AutolearnSkills.UseVisualStyleBackColor = true;
            // 
            // checkBox_writetolog
            // 
            this.checkBox_writetolog.Checked = true;
            this.checkBox_writetolog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_writetolog.Location = new System.Drawing.Point(15, 358);
            this.checkBox_writetolog.Name = "checkBox_writetolog";
            this.checkBox_writetolog.Size = new System.Drawing.Size(269, 24);
            this.checkBox_writetolog.TabIndex = 54;
            this.checkBox_writetolog.Text = "Write to Log";
            this.checkBox_writetolog.CheckedChanged += new System.EventHandler(this.checkBox_writetolog_CheckedChanged);
            // 
            // checkBox_npc_say
            // 
            this.checkBox_npc_say.Location = new System.Drawing.Point(402, 190);
            this.checkBox_npc_say.Name = "checkBox_npc_say";
            this.checkBox_npc_say.Padding = new System.Windows.Forms.Padding(2);
            this.checkBox_npc_say.Size = new System.Drawing.Size(97, 24);
            this.checkBox_npc_say.TabIndex = 55;
            this.checkBox_npc_say.Text = "npc say";
            // 
            // Setup
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(573, 440);
            this.ControlBox = false;
            this.Controls.Add(this.checkBox_npc_say);
            this.Controls.Add(this.checkBox_writetolog);
            this.Controls.Add(this.checkBox_AutolearnSkills);
            this.Controls.Add(this.textBox_captcha_html2);
            this.Controls.Add(this.label_captcha_html);
            this.Controls.Add(this.textBox_captcha_html1);
            this.Controls.Add(this.checkBox_captcha_popup);
            this.Controls.Add(this.checkBox_togglebotifported);
            this.Controls.Add(this.checkBox_ToggleBottingIfGMAction);
            this.Controls.Add(this.net_dump_on_ig_crit);
            this.Controls.Add(this.net_dc_on_ig_crit);
            this.Controls.Add(this.checkBox_usegpu);
            this.Controls.Add(this.checkBox_script386);
            this.Controls.Add(this.button_change_kill);
            this.Controls.Add(this.label_kill_key);
            this.Controls.Add(this.label_directinput2);
            this.Controls.Add(this.checkBox_IgnoreExitConf);
            this.Controls.Add(this.checkBox_shownames_items);
            this.Controls.Add(this.checkBox_shownames_players);
            this.Controls.Add(this.checkBox_shownames_npcs);
            this.Controls.Add(this.checkBox_social_npcs);
            this.Controls.Add(this.checkBox_social_self);
            this.Controls.Add(this.checkBox_social_pcs);
            this.Controls.Add(this.checkBox_downloadcrests);
            this.Controls.Add(this.label_viewrange);
            this.Controls.Add(this.comboBox_viewrange);
            this.Controls.Add(this.label_texturemode);
            this.Controls.Add(this.comboBox_texturemode);
            this.Controls.Add(this.checkBox_hide_mess);
            this.Controls.Add(this.checkBox_blank_gg);
            this.Controls.Add(this.checkBox_supress_quakes);
            this.Controls.Add(this.button_change_toggle);
            this.Controls.Add(this.label_toggle_key);
            this.Controls.Add(this.checkBox_white_names);
            this.Controls.Add(this.label_directinput);
            this.Controls.Add(this.label_l2_path);
            this.Controls.Add(this.textBox_l2path);
            this.Controls.Add(this.checkBox_scriptIO);
            this.Controls.Add(this.label_productkey);
            this.Controls.Add(this.textBox_key);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.checkBox_MinToTray);
            this.Controls.Add(this.label_voice);
            this.Controls.Add(this.comboBox_voice);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Setup";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Setup";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void button_save_Click(object sender, System.EventArgs e)
		{
            Globals.Voice = comboBox_voice.SelectedIndex;
            Globals.MinimizeToTray = checkBox_MinToTray.Checked;
            Globals.ProductKey = "LOVELKQKMGBOGNET";//textBox_key.Text;
            Globals.AllowFiles = checkBox_scriptIO.Checked;
            Globals.ScriptCompatibilityv386 = checkBox_script386.Checked;
            Globals.L2Path = textBox_l2path.Text;
            Globals.DirectInputKey = label_toggle_key.Text;
            Globals.DirectInputKey2 = label_kill_key.Text;
            Globals.White_Names = checkBox_white_names.Checked;
            Globals.Suppress_Quakes = checkBox_supress_quakes.Checked;
            Globals.Send_Blank_GG = checkBox_blank_gg.Checked;
            Globals.Hide_Message_Boxes = checkBox_hide_mess.Checked;
            Globals.Texture_Mode = comboBox_texturemode.SelectedIndex;
            Globals.ViewRange = comboBox_viewrange.SelectedIndex;
            Globals.Use_Hardware_Acceleration = checkBox_usegpu.Checked;

            //if (Globals.isValidKey)
            Globals.Popup_Captcha = checkBox_captcha_popup.Checked;
            //else
            //    Globals.Popup_Captcha = false;

            Globals.Captcha_HTML1 = textBox_captcha_html1.Text;
            Globals.Captcha_HTML2 = textBox_captcha_html2.Text;

            Globals.DownloadNewCrests = checkBox_downloadcrests.Checked;
            Globals.SocialSelf = checkBox_social_self.Checked;
            Globals.SocialPcs = checkBox_social_pcs.Checked;
            Globals.SocialNpcs = checkBox_social_npcs.Checked;
            Globals.NpcSay = checkBox_npc_say.Checked;
            Globals.ShowNamesNpcs = checkBox_shownames_npcs.Checked;
            Globals.ShowNamesPcs = checkBox_shownames_players.Checked;
            Globals.ShowNamesItems = checkBox_shownames_items.Checked;
            Globals.IgnoreExitConf = checkBox_IgnoreExitConf.Checked;
            Globals.ToggleBottingifGMAction = checkBox_ToggleBottingIfGMAction.Checked;
            Globals.ToggleBottingifTeleported = checkBox_togglebotifported.Checked;

            Globals.dc_on_ig_close = net_dc_on_ig_crit.Checked;
            Globals.dump_pbuff_on_ig_close = net_dump_on_ig_crit.Checked;

            Globals.AutolearnSkills = checkBox_AutolearnSkills.Checked;
            Globals.LogWriting = checkBox_writetolog.Checked;


			this.Hide();
        }

		private void button_cancel_Click(object sender, System.EventArgs e)
		{
			this.Hide();
		}


        private void button_change_toggle_Click(object sender, EventArgs e)
        {
            button_change_toggle.Enabled = false;
            button_change_kill.Enabled = false;
            comboBox_voice.Enabled = false;
            textBox_l2path.Enabled = false;
            textBox_key.Enabled = false;
            comboBox_texturemode.Enabled = false;
            comboBox_viewrange.Enabled = false;
            Globals.DirectInputSetup = true;
        }

        private void button_change_kill_Click(object sender, EventArgs e)
        {
            button_change_kill.Enabled = false;
            button_change_toggle.Enabled = false;
            comboBox_voice.Enabled = false;
            textBox_l2path.Enabled = false;
            textBox_key.Enabled = false;
            comboBox_texturemode.Enabled = false;
            comboBox_viewrange.Enabled = false;
            Globals.DirectInputSetup2 = true;
        }

        private void checkBox_writetolog_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_writetolog.Checked && Globals.text_out == null)
            {
                Globals.text_out = new System.IO.StreamWriter(Globals.PATH + "\\logs\\" + System.DateTime.Now.Year.ToString() + "-" + System.DateTime.Now.Month.ToString() + "-" + System.DateTime.Now.Day.ToString() + ".txt");
            }
            if (!checkBox_writetolog.Checked && Globals.text_out != null)
            {
                Globals.text_out = null;
            }
        }

    }
}
