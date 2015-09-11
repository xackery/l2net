using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace L2_login
{
	/// <summary>
	/// Summary description for Login.
	/// </summary>
	public class Login : Base
	{
		private System.Windows.Forms.Panel panel_select;
		private System.Windows.Forms.Button button_select_ig;
		private System.Windows.Forms.Button button_select_oog;
		private System.Windows.Forms.Panel panel_ig;
		private System.Windows.Forms.ComboBox comboBox_ig_login;
		private System.Windows.Forms.TextBox textBox_local_ip;
		private System.Windows.Forms.TextBox textBox_ig_login_port;
		private System.Windows.Forms.TextBox textBox_ig_login_ip;
        private System.Windows.Forms.Button button_ig_listen;
		private System.Windows.Forms.ComboBox comboBox_blowfish;
        private System.Windows.Forms.TextBox textBox_blowfish;
		private System.Windows.Forms.TextBox textBox_local_port;
		private System.Windows.Forms.Label label_blowfishkey;
		private System.Windows.Forms.Label label_localport;
        private System.Windows.Forms.Label label_loginserver;
        private System.Windows.Forms.Button button_back_ig;
        private TextBox textBox_oog_logon_ip;
        private ListView listView_servers;
        private ColumnHeader columnHeader1;
        private ColumnHeader columnHeader79;
        private ColumnHeader columnHeader2;
        private ColumnHeader columnHeader3;
        private ColumnHeader columnHeader4;
        private ColumnHeader columnHeader5;
        private ColumnHeader columnHeader6;
        private ColumnHeader columnHeader7;
        private ColumnHeader columnHeader8;
        private ColumnHeader columnHeader9;
        private TextBox textBox_oog_logon_port;
        private Button button_logon;
        private TextBox textBox_pword;
        private Button button_server;
        private Button button_char;
        private TextBox textBox_lname;
        private ComboBox comboBox_oog_login;
        private Button button_back_oog;
        private Panel panel_oog;
        private Label label_prot;
        private TextBox textBox_logon_prot;
        private Label label_loginport;
        private Label label_ip;
        private Label label_oog_password;
        private Label label_oog_username;
        private Button button_oog_create;
        private Label label_oog_loginport;

        public ArrayList blowfish_list = new ArrayList();
        private Panel panel_advanced;
        private CheckBox checkBox_game_proxy;
        private TextBox textBox_socks_password;
        private Label label_socks_password;
        private TextBox textBox_socks_username;
        private Label label_socks_username;
        private Label label_socks_ip;
        private TextBox textBox_socks_port;
        private Label label_socks_port;
        private TextBox textBox_socks_ip;
        private CheckBox checkBox_login_proxy;
        private Label label_game_ip;
        private TextBox textBox_game_port;
        private Label label_game_port;
        private TextBox textBox_game_ip;
        private CheckBox checkBox_override_gameserver;
        private CheckBox checkBox_advanced;
        private Button button_delete_char;
        private TextBox textBox_Custom_EnterWorld;
        private CheckBox checkBox_Custom_EnterWorld;
        private Label label_packet;
        private CheckBox checkBox_Unknown_Blowfish;
        private CheckBox checkBox_LS_GS_Same_IP;
        private CheckBox checkBox_ManualGK;
        private ComboBox comboBox_gsList;
        public ArrayList loginserver_list = new ArrayList();
        private ListView listView_chars;
        private ColumnHeader columnHeader10;
        private ColumnHeader columnHeader11;
        private ColumnHeader columnHeader12;
        private ColumnHeader columnHeader13;
        private ColumnHeader columnHeader14;
        private ColumnHeader columnHeader15;
        private ColumnHeader columnHeader16;
        private ColumnHeader columnHeader17;
        private ColumnHeader columnHeader18;
        private ColumnHeader columnHeader19;
        private ColumnHeader columnHeader20;
        private ColumnHeader columnHeader21;
        private ColumnHeader columnHeader22;
        private ColumnHeader columnHeader23;
        private ColumnHeader columnHeader24;
        private ColumnHeader columnHeader25;
        private ColumnHeader columnHeader26;
        private ColumnHeader columnHeader27;
        private ColumnHeader columnHeader28;
        private CheckBox checkBox_OverrideProtocol;
        private TextBox textBox_game_listenport;
        private Label label1;
        private Label label5;
        private Label label4;
        private Label label3;
        private Label label2;
        private CheckBox c_ew_ip;
        private Label label6;
        private CheckBox c_ew_c44;
        private TextBox c_ew_e44;
        private CheckBox c_ew_c34;
        private TextBox c_ew_e34;
        private CheckBox c_ew_c24;
        private TextBox c_ew_e24;
        private CheckBox c_ew_c14;
        private TextBox c_ew_e14;
        private CheckBox c_ew_c43;
        private CheckBox c_ew_c42;
        private CheckBox c_ew_c41;
        private TextBox c_ew_e43;
        private TextBox c_ew_e42;
        private TextBox c_ew_e41;
        private CheckBox c_ew_c33;
        private CheckBox c_ew_c32;
        private CheckBox c_ew_c31;
        private TextBox c_ew_e33;
        private TextBox c_ew_e32;
        private TextBox c_ew_e31;
        private CheckBox c_ew_c23;
        private CheckBox c_ew_c22;
        private CheckBox c_ew_c21;
        private TextBox c_ew_e23;
        private TextBox c_ew_e22;
        private TextBox c_ew_e21;
        private CheckBox c_ew_c13;
        private CheckBox c_ew_c12;
        private CheckBox c_ew_c11;
        private TextBox c_ew_e13;
        private TextBox c_ew_e12;
        private TextBox c_ew_e11;
        private CheckBox c_ew_c04;
        private TextBox c_ew_e04;
        private CheckBox c_ew_c03;
        private CheckBox c_ew_c02;
        private CheckBox c_ew_c01;
        private TextBox c_ew_e03;
        private TextBox c_ew_e02;
        private TextBox c_ew_e01;
        private Label label7;
        private CheckBox ch_w_a_s;
        private Label label_security_pin;
        private TextBox textBox_security_pin;
        private Label label_IG_pin;
        private TextBox textBox_IG_pin;
        private CheckBox checkBox_security_old_client;
        private RadioButton radioButton_c17;
        private RadioButton radioButton_c18;
        public ArrayList gameserver_list = new ArrayList();

		public Login(System.Windows.Forms.Form pf)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
            add_controls_to_array();
            checkBox_advanced.Enabled = true;
            checkBox_Unknown_Blowfish.Enabled = true;

			//
            textBox_blowfish.Text = Globals.pre_blowfish;
            textBox_logon_prot.Text = Globals.pre_protocol;
            textBox_local_port.Text = Globals.pre_login_port;
            textBox_lname.Text = Globals.pre_username;
            textBox_pword.Text = Globals.pre_password;
            textBox_security_pin.Text = Globals.SecurityPin;
            textBox_IG_pin.Text = Globals.SecurityPin;
            checkBox_security_old_client.Checked = Globals.gamedata.SecurityPinOldClient;

            switch(Globals.pre_chron)
            {
                case 17: //Glory Days
                    radioButton_c17.Checked = true;
                    break;
                case 18:
                    radioButton_c18.Checked = true;
                    break;
                default:
                    radioButton_c18.Checked = true;
                    break;
            }

            textBox_oog_logon_ip.Text = Globals.pre_login_ip;
            textBox_oog_logon_port.Text = Globals.pre_login_port;
            textBox_ig_login_ip.Text = Globals.pre_login_ip;
            textBox_ig_login_port.Text = Globals.pre_login_port;
            textBox_local_ip.Text = Globals.pre_IG_listen_ip;
            textBox_local_port.Text = Globals.pre_IG_listen_port;

#if DEBUG
            textBox_game_ip.Text = Globals.pre_gameserver_override_ip;
            textBox_game_port.Text = Globals.pre_gameserver_override_port;
            textBox_socks_ip.Text = Globals.pre_socks5_ip;
            textBox_socks_port.Text = Globals.pre_socks5_port;
            textBox_socks_username.Text = Globals.pre_socks5_username;
            textBox_socks_password.Text = Globals.pre_socks5_password;
            checkBox_override_gameserver.Checked = Globals.pre_useGameServerOveride;
            checkBox_game_proxy.Checked = Globals.pre_useProxyServerForGameserver;
            checkBox_login_proxy.Checked = Globals.pre_useProxyServerForLogin;
            if (Globals.pre_EnterWorldCheckbox)
            {
                checkBox_advanced.Checked = true;
                checkBox_Custom_EnterWorld.Checked = true;
            }
            else
            {
                checkBox_advanced.Checked = Globals.pre_checkAdvancedSettings;
            }

            if (Globals.pre_IG)
            {
                if (Set_Modes())
                    panel_ig.BringToFront();
                button_ig_listen_Click(null, null);

            }

            if (Globals.pre_OOG)
            {
                if (Set_Modes())
                    panel_oog.BringToFront();
            }

            if (Globals.pre_GGClient)
            {
                GameGuardClient.pre_connect();
            }

            if (Globals.pre_GGSrv)
            {
                GameGuardServer.pre_listen();
            }

#else

            textBox_game_ip.Text = Globals.pre_gameserver_override_ip;
            textBox_game_port.Text = Globals.pre_gameserver_override_port;
            // textBox_game_listenport.Text = Globals.pre_gameserver_override_port;
            textBox_socks_ip.Text = Globals.pre_socks5_ip;
            textBox_socks_port.Text = Globals.pre_socks5_port;
            textBox_socks_username.Text = Globals.pre_socks5_username;
            textBox_socks_password.Text = Globals.pre_socks5_password;
            checkBox_override_gameserver.Checked = Globals.pre_useGameServerOveride;
            checkBox_game_proxy.Checked = Globals.pre_useProxyServerForGameserver;
            checkBox_login_proxy.Checked = Globals.pre_useProxyServerForLogin;
            checkBox_advanced.Checked = Globals.pre_checkAdvancedSettings;
            if (Globals.pre_EnterWorldCheckbox)
            {
                checkBox_advanced.Checked = true;
                checkBox_Custom_EnterWorld.Checked = true;
            }


            if (Globals.pre_IG)
            {
                if (Set_Modes())
                    panel_ig.BringToFront();
                button_ig_listen_Click(null, null);

            }

            if (Globals.pre_OOG)
            {
                if (Set_Modes())
                    panel_oog.BringToFront();
            }

            if (Globals.pre_GGClient)
            {
                GameGuardClient.pre_connect();
            }

            if (Globals.pre_GGSrv)
            {
                GameGuardServer.pre_listen();
            }
#endif

            comboBox_blowfish.SelectedIndexChanged += new EventHandler(comboBox_blowfish_SelectedIndexChanged);
			comboBox_ig_login.SelectedIndexChanged += new EventHandler(comboBox_ig_login_SelectedIndexChanged);
			comboBox_oog_login.SelectedIndexChanged += new EventHandler(comboBox_oog_login_SelectedIndexChanged);

			this.MdiParent = pf;
			this.MdiParent.Resize += new EventHandler(MdiParent_Resize);
			MdiParent_Resize(null,null);

			Load_Servers();
            Load_GameServers();
			Load_Blowfish();
            Load_EnterWorld();

			UpdateUI();
		}

		public void UpdateUI()
		{
            button_select_oog.Text = Globals.m_ResourceManager.GetString("button_select_oog");
            button_select_ig.Text = Globals.m_ResourceManager.GetString("button_select_ig");
            label_blowfishkey.Text = Globals.m_ResourceManager.GetString("label_blowfishkey");
            label_loginserver.Text = Globals.m_ResourceManager.GetString("label_loginserver");
            label_localport.Text = Globals.m_ResourceManager.GetString("label_localport");
            button_ig_listen.Text = Globals.m_ResourceManager.GetString("button_ig_listen");
            button_logon.Text = Globals.m_ResourceManager.GetString("button_logon");
            button_server.Text = Globals.m_ResourceManager.GetString("button_server");
            button_char.Text = Globals.m_ResourceManager.GetString("button_char");
            button_delete_char.Text = Globals.m_ResourceManager.GetString("button_delete_char");
			this.Refresh();
		}

        public void Disable_ip_edit(int index)//adifenix
        {


            if((index<Globals.ew_con_array.Count) && (index >= 0))
            {
                (Globals.ew_con_array[index] as System.Windows.Forms.TextBox).ReadOnly = true;
                (Globals.ew_con_array[index] as System.Windows.Forms.TextBox).ForeColor = SystemColors.ControlText;
            }

        }
        public void Disable_all_ip_edit()//adifenix
        {
            for (int i = 0; i < Globals.ew_con_array.Count; i++)
            {

                (Globals.ew_con_array[i] as System.Windows.Forms.TextBox).ReadOnly = true;
                (Globals.ew_con_array[i] as System.Windows.Forms.TextBox).ForeColor = SystemColors.ControlText;
            }
        }
        public void Enable_ip_edit(int index)//adifenix
        {
            if ((index < Globals.ew_con_array.Count) && (index >= 0))
            {
                (Globals.ew_con_array[index] as System.Windows.Forms.TextBox).ReadOnly = false;
                (Globals.ew_con_array[index] as System.Windows.Forms.TextBox).ForeColor = SystemColors.WindowText;
            }
           }


        public void Enable_all_ip_edit()//adifenix
        {
            for (int i = 0; i < Globals.ew_con_array.Count; i++)
            {
                if (!(Globals.ew_chc_ed_array[i] as System.Windows.Forms.CheckBox).Checked)
                {
                    (Globals.ew_con_array[i] as System.Windows.Forms.TextBox).ReadOnly = false;
                    (Globals.ew_con_array[i] as System.Windows.Forms.TextBox).ForeColor = SystemColors.WindowText;
                }
            }

        }
        public void add_controls_to_array()//adifenix
        {
            Globals.ew_con_array.Clear();
            Globals.ew_chc_ed_array.Clear();
            // ip edits 01-44
            Globals.ew_con_array.Add(c_ew_e01);
            Globals.ew_con_array.Add(c_ew_e02);
            Globals.ew_con_array.Add(c_ew_e03);
            Globals.ew_con_array.Add(c_ew_e04);

            Globals.ew_con_array.Add(c_ew_e11);
            Globals.ew_con_array.Add(c_ew_e12);
            Globals.ew_con_array.Add(c_ew_e13);
            Globals.ew_con_array.Add(c_ew_e14);

            Globals.ew_con_array.Add(c_ew_e21);
            Globals.ew_con_array.Add(c_ew_e22);
            Globals.ew_con_array.Add(c_ew_e23);
            Globals.ew_con_array.Add(c_ew_e24);

            Globals.ew_con_array.Add(c_ew_e31);
            Globals.ew_con_array.Add(c_ew_e32);
            Globals.ew_con_array.Add(c_ew_e33);
            Globals.ew_con_array.Add(c_ew_e34);

            Globals.ew_con_array.Add(c_ew_e41);
            Globals.ew_con_array.Add(c_ew_e42);
            Globals.ew_con_array.Add(c_ew_e43);
            Globals.ew_con_array.Add(c_ew_e44);

            // chechbox 01-44
            Globals.ew_chc_ed_array.Add(c_ew_c01);
            Globals.ew_chc_ed_array.Add(c_ew_c02);
            Globals.ew_chc_ed_array.Add(c_ew_c03);
            Globals.ew_chc_ed_array.Add(c_ew_c04);

            Globals.ew_chc_ed_array.Add(c_ew_c11);
            Globals.ew_chc_ed_array.Add(c_ew_c12);
            Globals.ew_chc_ed_array.Add(c_ew_c13);
            Globals.ew_chc_ed_array.Add(c_ew_c14);

            Globals.ew_chc_ed_array.Add(c_ew_c21);
            Globals.ew_chc_ed_array.Add(c_ew_c22);
            Globals.ew_chc_ed_array.Add(c_ew_c23);
            Globals.ew_chc_ed_array.Add(c_ew_c24);

            Globals.ew_chc_ed_array.Add(c_ew_c31);
            Globals.ew_chc_ed_array.Add(c_ew_c32);
            Globals.ew_chc_ed_array.Add(c_ew_c33);
            Globals.ew_chc_ed_array.Add(c_ew_c34);

            Globals.ew_chc_ed_array.Add(c_ew_c41);
            Globals.ew_chc_ed_array.Add(c_ew_c42);
            Globals.ew_chc_ed_array.Add(c_ew_c43);
            Globals.ew_chc_ed_array.Add(c_ew_c44);
            Globals.proxy_serv = Globals.pre_proxy_serv;
            Globals.unknow_blowfish = Globals.pre_unknow_blowfish;
            Globals.game_srv_listen_prt = Globals.pre_game_srv_listen_prt;
            ch_w_a_s.Checked = Globals.proxy_serv;// work as proxy srv
            textBox_game_listenport.Text = Globals.game_srv_listen_prt;
            checkBox_Unknown_Blowfish.Checked = Globals.unknow_blowfish;

            if (Globals.pre_enterworld_ip)
            {
               Globals.enterworld_ip = true;
               c_ew_ip.Checked = true;
                for(int i = 0;i<Globals.ew_con_array.Count;i++)
                {
                    try
                    {
                        if ((Globals.pre_enterworld_ip_tab[i].Length > 0) && (Globals.pre_enterworld_ip_tab[i].Length<=3))
                        {
                            (Globals.ew_con_array[i] as System.Windows.Forms.TextBox).Text = Globals.pre_enterworld_ip_tab[i];
                        }
                        else // check random if string leg == 0 or leg > 3
                        {
                            (Globals.ew_chc_ed_array[i] as System.Windows.Forms.CheckBox).Checked = true;
                        }
                    }
                    catch // exceptions for leng == 0 ??? loled
                    {
                        (Globals.ew_chc_ed_array[i] as System.Windows.Forms.CheckBox).Checked = true;
                    }
                }
            }
        }
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
            this.MdiParent.Resize -= new EventHandler(MdiParent_Resize);

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
            this.panel_select = new System.Windows.Forms.Panel();
            this.radioButton_c18 = new System.Windows.Forms.RadioButton();
            this.radioButton_c17 = new System.Windows.Forms.RadioButton();
            this.checkBox_LS_GS_Same_IP = new System.Windows.Forms.CheckBox();
            this.checkBox_Unknown_Blowfish = new System.Windows.Forms.CheckBox();
            this.panel_advanced = new System.Windows.Forms.Panel();
            this.ch_w_a_s = new System.Windows.Forms.CheckBox();
            this.c_ew_c04 = new System.Windows.Forms.CheckBox();
            this.c_ew_e04 = new System.Windows.Forms.TextBox();
            this.c_ew_c03 = new System.Windows.Forms.CheckBox();
            this.c_ew_c02 = new System.Windows.Forms.CheckBox();
            this.c_ew_c01 = new System.Windows.Forms.CheckBox();
            this.c_ew_e03 = new System.Windows.Forms.TextBox();
            this.c_ew_e02 = new System.Windows.Forms.TextBox();
            this.c_ew_e01 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.c_ew_c44 = new System.Windows.Forms.CheckBox();
            this.c_ew_e44 = new System.Windows.Forms.TextBox();
            this.c_ew_c34 = new System.Windows.Forms.CheckBox();
            this.c_ew_e34 = new System.Windows.Forms.TextBox();
            this.c_ew_c24 = new System.Windows.Forms.CheckBox();
            this.c_ew_e24 = new System.Windows.Forms.TextBox();
            this.c_ew_c14 = new System.Windows.Forms.CheckBox();
            this.c_ew_e14 = new System.Windows.Forms.TextBox();
            this.c_ew_c43 = new System.Windows.Forms.CheckBox();
            this.c_ew_c42 = new System.Windows.Forms.CheckBox();
            this.c_ew_c41 = new System.Windows.Forms.CheckBox();
            this.c_ew_e43 = new System.Windows.Forms.TextBox();
            this.c_ew_e42 = new System.Windows.Forms.TextBox();
            this.c_ew_e41 = new System.Windows.Forms.TextBox();
            this.c_ew_c33 = new System.Windows.Forms.CheckBox();
            this.c_ew_c32 = new System.Windows.Forms.CheckBox();
            this.c_ew_c31 = new System.Windows.Forms.CheckBox();
            this.c_ew_e33 = new System.Windows.Forms.TextBox();
            this.c_ew_e32 = new System.Windows.Forms.TextBox();
            this.c_ew_e31 = new System.Windows.Forms.TextBox();
            this.c_ew_c23 = new System.Windows.Forms.CheckBox();
            this.c_ew_c22 = new System.Windows.Forms.CheckBox();
            this.c_ew_c21 = new System.Windows.Forms.CheckBox();
            this.c_ew_e23 = new System.Windows.Forms.TextBox();
            this.c_ew_e22 = new System.Windows.Forms.TextBox();
            this.c_ew_e21 = new System.Windows.Forms.TextBox();
            this.c_ew_c13 = new System.Windows.Forms.CheckBox();
            this.c_ew_c12 = new System.Windows.Forms.CheckBox();
            this.c_ew_c11 = new System.Windows.Forms.CheckBox();
            this.c_ew_e13 = new System.Windows.Forms.TextBox();
            this.c_ew_e12 = new System.Windows.Forms.TextBox();
            this.c_ew_e11 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.c_ew_ip = new System.Windows.Forms.CheckBox();
            this.textBox_game_listenport = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_gsList = new System.Windows.Forms.ComboBox();
            this.checkBox_ManualGK = new System.Windows.Forms.CheckBox();
            this.label_packet = new System.Windows.Forms.Label();
            this.textBox_Custom_EnterWorld = new System.Windows.Forms.TextBox();
            this.checkBox_Custom_EnterWorld = new System.Windows.Forms.CheckBox();
            this.checkBox_game_proxy = new System.Windows.Forms.CheckBox();
            this.textBox_socks_password = new System.Windows.Forms.TextBox();
            this.label_socks_password = new System.Windows.Forms.Label();
            this.textBox_socks_username = new System.Windows.Forms.TextBox();
            this.label_socks_username = new System.Windows.Forms.Label();
            this.label_socks_ip = new System.Windows.Forms.Label();
            this.textBox_socks_port = new System.Windows.Forms.TextBox();
            this.label_socks_port = new System.Windows.Forms.Label();
            this.textBox_socks_ip = new System.Windows.Forms.TextBox();
            this.checkBox_login_proxy = new System.Windows.Forms.CheckBox();
            this.textBox_game_port = new System.Windows.Forms.TextBox();
            this.label_game_port = new System.Windows.Forms.Label();
            this.textBox_game_ip = new System.Windows.Forms.TextBox();
            this.checkBox_override_gameserver = new System.Windows.Forms.CheckBox();
            this.label_game_ip = new System.Windows.Forms.Label();
            this.checkBox_advanced = new System.Windows.Forms.CheckBox();
            this.label_prot = new System.Windows.Forms.Label();
            this.textBox_logon_prot = new System.Windows.Forms.TextBox();
            this.comboBox_blowfish = new System.Windows.Forms.ComboBox();
            this.label_blowfishkey = new System.Windows.Forms.Label();
            this.textBox_blowfish = new System.Windows.Forms.TextBox();
            this.button_select_ig = new System.Windows.Forms.Button();
            this.button_select_oog = new System.Windows.Forms.Button();
            this.panel_ig = new System.Windows.Forms.Panel();
            this.checkBox_security_old_client = new System.Windows.Forms.CheckBox();
            this.label_IG_pin = new System.Windows.Forms.Label();
            this.textBox_IG_pin = new System.Windows.Forms.TextBox();
            this.checkBox_OverrideProtocol = new System.Windows.Forms.CheckBox();
            this.label_loginport = new System.Windows.Forms.Label();
            this.label_ip = new System.Windows.Forms.Label();
            this.button_back_ig = new System.Windows.Forms.Button();
            this.textBox_local_port = new System.Windows.Forms.TextBox();
            this.label_localport = new System.Windows.Forms.Label();
            this.comboBox_ig_login = new System.Windows.Forms.ComboBox();
            this.label_loginserver = new System.Windows.Forms.Label();
            this.textBox_local_ip = new System.Windows.Forms.TextBox();
            this.textBox_ig_login_port = new System.Windows.Forms.TextBox();
            this.textBox_ig_login_ip = new System.Windows.Forms.TextBox();
            this.button_ig_listen = new System.Windows.Forms.Button();
            this.textBox_oog_logon_ip = new System.Windows.Forms.TextBox();
            this.listView_servers = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader79 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.textBox_oog_logon_port = new System.Windows.Forms.TextBox();
            this.button_logon = new System.Windows.Forms.Button();
            this.textBox_pword = new System.Windows.Forms.TextBox();
            this.button_server = new System.Windows.Forms.Button();
            this.button_char = new System.Windows.Forms.Button();
            this.textBox_lname = new System.Windows.Forms.TextBox();
            this.comboBox_oog_login = new System.Windows.Forms.ComboBox();
            this.button_back_oog = new System.Windows.Forms.Button();
            this.panel_oog = new System.Windows.Forms.Panel();
            this.label_security_pin = new System.Windows.Forms.Label();
            this.textBox_security_pin = new System.Windows.Forms.TextBox();
            this.listView_chars = new System.Windows.Forms.ListView();
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader19 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader20 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader21 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader22 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader23 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader24 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader25 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader26 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader27 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader28 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.button_delete_char = new System.Windows.Forms.Button();
            this.button_oog_create = new System.Windows.Forms.Button();
            this.label_oog_password = new System.Windows.Forms.Label();
            this.label_oog_username = new System.Windows.Forms.Label();
            this.label_oog_loginport = new System.Windows.Forms.Label();
            this.panel_select.SuspendLayout();
            this.panel_advanced.SuspendLayout();
            this.panel_ig.SuspendLayout();
            this.panel_oog.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_select
            // 
            this.panel_select.Controls.Add(this.radioButton_c18);
            this.panel_select.Controls.Add(this.radioButton_c17);
            this.panel_select.Controls.Add(this.checkBox_LS_GS_Same_IP);
            this.panel_select.Controls.Add(this.checkBox_Unknown_Blowfish);
            this.panel_select.Controls.Add(this.panel_advanced);
            this.panel_select.Controls.Add(this.checkBox_advanced);
            this.panel_select.Controls.Add(this.label_prot);
            this.panel_select.Controls.Add(this.textBox_logon_prot);
            this.panel_select.Controls.Add(this.comboBox_blowfish);
            this.panel_select.Controls.Add(this.label_blowfishkey);
            this.panel_select.Controls.Add(this.textBox_blowfish);
            this.panel_select.Controls.Add(this.button_select_ig);
            this.panel_select.Controls.Add(this.button_select_oog);
            this.panel_select.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_select.Location = new System.Drawing.Point(0, 0);
            this.panel_select.Name = "panel_select";
            this.panel_select.Size = new System.Drawing.Size(607, 445);
            this.panel_select.TabIndex = 0;
            // 
            // radioButton_c18
            // 
            this.radioButton_c18.Checked = true;
            this.radioButton_c18.Location = new System.Drawing.Point(303, 34);
            this.radioButton_c18.Name = "radioButton_c18";
            this.radioButton_c18.Size = new System.Drawing.Size(135, 18);
            this.radioButton_c18.TabIndex = 43;
            this.radioButton_c18.TabStop = true;
            this.radioButton_c18.Text = "Lindvior";
            // 
            // radioButton_c17
            // 
            this.radioButton_c17.Location = new System.Drawing.Point(303, 12);
            this.radioButton_c17.Name = "radioButton_c17";
            this.radioButton_c17.Size = new System.Drawing.Size(135, 18);
            this.radioButton_c17.TabIndex = 42;
            this.radioButton_c17.Text = "Glory Days";
            this.radioButton_c17.Visible = false;
            // 
            // checkBox_LS_GS_Same_IP
            // 
            this.checkBox_LS_GS_Same_IP.AutoSize = true;
            this.checkBox_LS_GS_Same_IP.Enabled = false;
            this.checkBox_LS_GS_Same_IP.Location = new System.Drawing.Point(160, 38);
            this.checkBox_LS_GS_Same_IP.Name = "checkBox_LS_GS_Same_IP";
            this.checkBox_LS_GS_Same_IP.Size = new System.Drawing.Size(119, 17);
            this.checkBox_LS_GS_Same_IP.TabIndex = 38;
            this.checkBox_LS_GS_Same_IP.Text = "LS and GS same IP";
            this.checkBox_LS_GS_Same_IP.UseVisualStyleBackColor = true;
            // 
            // checkBox_Unknown_Blowfish
            // 
            this.checkBox_Unknown_Blowfish.AutoSize = true;
            this.checkBox_Unknown_Blowfish.Location = new System.Drawing.Point(160, 19);
            this.checkBox_Unknown_Blowfish.Name = "checkBox_Unknown_Blowfish";
            this.checkBox_Unknown_Blowfish.Size = new System.Drawing.Size(108, 17);
            this.checkBox_Unknown_Blowfish.TabIndex = 37;
            this.checkBox_Unknown_Blowfish.Text = "Unkown Blowfish";
            this.checkBox_Unknown_Blowfish.UseVisualStyleBackColor = true;
            this.checkBox_Unknown_Blowfish.CheckedChanged += new System.EventHandler(this.checkBox_Unknown_Blowfish_CheckedChanged);
            // 
            // panel_advanced
            // 
            this.panel_advanced.Controls.Add(this.ch_w_a_s);
            this.panel_advanced.Controls.Add(this.c_ew_c04);
            this.panel_advanced.Controls.Add(this.c_ew_e04);
            this.panel_advanced.Controls.Add(this.c_ew_c03);
            this.panel_advanced.Controls.Add(this.c_ew_c02);
            this.panel_advanced.Controls.Add(this.c_ew_c01);
            this.panel_advanced.Controls.Add(this.c_ew_e03);
            this.panel_advanced.Controls.Add(this.c_ew_e02);
            this.panel_advanced.Controls.Add(this.c_ew_e01);
            this.panel_advanced.Controls.Add(this.label7);
            this.panel_advanced.Controls.Add(this.label6);
            this.panel_advanced.Controls.Add(this.c_ew_c44);
            this.panel_advanced.Controls.Add(this.c_ew_e44);
            this.panel_advanced.Controls.Add(this.c_ew_c34);
            this.panel_advanced.Controls.Add(this.c_ew_e34);
            this.panel_advanced.Controls.Add(this.c_ew_c24);
            this.panel_advanced.Controls.Add(this.c_ew_e24);
            this.panel_advanced.Controls.Add(this.c_ew_c14);
            this.panel_advanced.Controls.Add(this.c_ew_e14);
            this.panel_advanced.Controls.Add(this.c_ew_c43);
            this.panel_advanced.Controls.Add(this.c_ew_c42);
            this.panel_advanced.Controls.Add(this.c_ew_c41);
            this.panel_advanced.Controls.Add(this.c_ew_e43);
            this.panel_advanced.Controls.Add(this.c_ew_e42);
            this.panel_advanced.Controls.Add(this.c_ew_e41);
            this.panel_advanced.Controls.Add(this.c_ew_c33);
            this.panel_advanced.Controls.Add(this.c_ew_c32);
            this.panel_advanced.Controls.Add(this.c_ew_c31);
            this.panel_advanced.Controls.Add(this.c_ew_e33);
            this.panel_advanced.Controls.Add(this.c_ew_e32);
            this.panel_advanced.Controls.Add(this.c_ew_e31);
            this.panel_advanced.Controls.Add(this.c_ew_c23);
            this.panel_advanced.Controls.Add(this.c_ew_c22);
            this.panel_advanced.Controls.Add(this.c_ew_c21);
            this.panel_advanced.Controls.Add(this.c_ew_e23);
            this.panel_advanced.Controls.Add(this.c_ew_e22);
            this.panel_advanced.Controls.Add(this.c_ew_e21);
            this.panel_advanced.Controls.Add(this.c_ew_c13);
            this.panel_advanced.Controls.Add(this.c_ew_c12);
            this.panel_advanced.Controls.Add(this.c_ew_c11);
            this.panel_advanced.Controls.Add(this.c_ew_e13);
            this.panel_advanced.Controls.Add(this.c_ew_e12);
            this.panel_advanced.Controls.Add(this.c_ew_e11);
            this.panel_advanced.Controls.Add(this.label5);
            this.panel_advanced.Controls.Add(this.label4);
            this.panel_advanced.Controls.Add(this.label3);
            this.panel_advanced.Controls.Add(this.label2);
            this.panel_advanced.Controls.Add(this.c_ew_ip);
            this.panel_advanced.Controls.Add(this.textBox_game_listenport);
            this.panel_advanced.Controls.Add(this.label1);
            this.panel_advanced.Controls.Add(this.comboBox_gsList);
            this.panel_advanced.Controls.Add(this.checkBox_ManualGK);
            this.panel_advanced.Controls.Add(this.label_packet);
            this.panel_advanced.Controls.Add(this.textBox_Custom_EnterWorld);
            this.panel_advanced.Controls.Add(this.checkBox_Custom_EnterWorld);
            this.panel_advanced.Controls.Add(this.checkBox_game_proxy);
            this.panel_advanced.Controls.Add(this.textBox_socks_password);
            this.panel_advanced.Controls.Add(this.label_socks_password);
            this.panel_advanced.Controls.Add(this.textBox_socks_username);
            this.panel_advanced.Controls.Add(this.label_socks_username);
            this.panel_advanced.Controls.Add(this.label_socks_ip);
            this.panel_advanced.Controls.Add(this.textBox_socks_port);
            this.panel_advanced.Controls.Add(this.label_socks_port);
            this.panel_advanced.Controls.Add(this.textBox_socks_ip);
            this.panel_advanced.Controls.Add(this.checkBox_login_proxy);
            this.panel_advanced.Controls.Add(this.textBox_game_port);
            this.panel_advanced.Controls.Add(this.label_game_port);
            this.panel_advanced.Controls.Add(this.textBox_game_ip);
            this.panel_advanced.Controls.Add(this.checkBox_override_gameserver);
            this.panel_advanced.Controls.Add(this.label_game_ip);
            this.panel_advanced.Location = new System.Drawing.Point(3, 180);
            this.panel_advanced.Name = "panel_advanced";
            this.panel_advanced.Size = new System.Drawing.Size(596, 239);
            this.panel_advanced.TabIndex = 33;
            this.panel_advanced.Visible = false;
            // 
            // ch_w_a_s
            // 
            this.ch_w_a_s.AutoSize = true;
            this.ch_w_a_s.Location = new System.Drawing.Point(225, 53);
            this.ch_w_a_s.Name = "ch_w_a_s";
            this.ch_w_a_s.Size = new System.Drawing.Size(111, 17);
            this.ch_w_a_s.TabIndex = 97;
            this.ch_w_a_s.Text = "Work as proxy srv";
            this.ch_w_a_s.UseVisualStyleBackColor = true;
            this.ch_w_a_s.CheckedChanged += new System.EventHandler(this.ch_w_a_s_CheckedChanged);
            // 
            // c_ew_c04
            // 
            this.c_ew_c04.AutoSize = true;
            this.c_ew_c04.Location = new System.Drawing.Point(572, 83);
            this.c_ew_c04.Name = "c_ew_c04";
            this.c_ew_c04.Size = new System.Drawing.Size(15, 14);
            this.c_ew_c04.TabIndex = 96;
            this.c_ew_c04.UseVisualStyleBackColor = true;
            this.c_ew_c04.CheckedChanged += new System.EventHandler(this.chc_ew_iprand);
            // 
            // c_ew_e04
            // 
            this.c_ew_e04.Location = new System.Drawing.Point(541, 80);
            this.c_ew_e04.MaxLength = 3;
            this.c_ew_e04.Name = "c_ew_e04";
            this.c_ew_e04.Size = new System.Drawing.Size(25, 20);
            this.c_ew_e04.TabIndex = 95;
            // 
            // c_ew_c03
            // 
            this.c_ew_c03.AutoSize = true;
            this.c_ew_c03.Location = new System.Drawing.Point(524, 83);
            this.c_ew_c03.Name = "c_ew_c03";
            this.c_ew_c03.Size = new System.Drawing.Size(15, 14);
            this.c_ew_c03.TabIndex = 94;
            this.c_ew_c03.UseVisualStyleBackColor = true;
            this.c_ew_c03.CheckedChanged += new System.EventHandler(this.chc_ew_iprand);
            // 
            // c_ew_c02
            // 
            this.c_ew_c02.AutoSize = true;
            this.c_ew_c02.Location = new System.Drawing.Point(472, 83);
            this.c_ew_c02.Name = "c_ew_c02";
            this.c_ew_c02.Size = new System.Drawing.Size(15, 14);
            this.c_ew_c02.TabIndex = 93;
            this.c_ew_c02.UseVisualStyleBackColor = true;
            this.c_ew_c02.CheckedChanged += new System.EventHandler(this.chc_ew_iprand);
            // 
            // c_ew_c01
            // 
            this.c_ew_c01.AutoSize = true;
            this.c_ew_c01.Location = new System.Drawing.Point(420, 83);
            this.c_ew_c01.Name = "c_ew_c01";
            this.c_ew_c01.Size = new System.Drawing.Size(15, 14);
            this.c_ew_c01.TabIndex = 92;
            this.c_ew_c01.UseVisualStyleBackColor = true;
            this.c_ew_c01.CheckedChanged += new System.EventHandler(this.chc_ew_iprand);
            // 
            // c_ew_e03
            // 
            this.c_ew_e03.Location = new System.Drawing.Point(493, 80);
            this.c_ew_e03.MaxLength = 3;
            this.c_ew_e03.Name = "c_ew_e03";
            this.c_ew_e03.Size = new System.Drawing.Size(25, 20);
            this.c_ew_e03.TabIndex = 91;
            // 
            // c_ew_e02
            // 
            this.c_ew_e02.Location = new System.Drawing.Point(441, 80);
            this.c_ew_e02.MaxLength = 3;
            this.c_ew_e02.Name = "c_ew_e02";
            this.c_ew_e02.Size = new System.Drawing.Size(25, 20);
            this.c_ew_e02.TabIndex = 90;
            // 
            // c_ew_e01
            // 
            this.c_ew_e01.Location = new System.Drawing.Point(389, 80);
            this.c_ew_e01.MaxLength = 3;
            this.c_ew_e01.Name = "c_ew_e01";
            this.c_ew_e01.Size = new System.Drawing.Size(25, 20);
            this.c_ew_e01.TabIndex = 89;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(365, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(26, 13);
            this.label7.TabIndex = 88;
            this.label7.Text = "IP0:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(489, 208);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(104, 13);
            this.label6.TabIndex = 87;
            this.label6.Text = "Checkbox - Random";
            // 
            // c_ew_c44
            // 
            this.c_ew_c44.AutoSize = true;
            this.c_ew_c44.Location = new System.Drawing.Point(572, 188);
            this.c_ew_c44.Name = "c_ew_c44";
            this.c_ew_c44.Size = new System.Drawing.Size(15, 14);
            this.c_ew_c44.TabIndex = 86;
            this.c_ew_c44.UseVisualStyleBackColor = true;
            this.c_ew_c44.CheckedChanged += new System.EventHandler(this.chc_ew_iprand);
            // 
            // c_ew_e44
            // 
            this.c_ew_e44.Location = new System.Drawing.Point(541, 185);
            this.c_ew_e44.MaxLength = 3;
            this.c_ew_e44.Name = "c_ew_e44";
            this.c_ew_e44.Size = new System.Drawing.Size(25, 20);
            this.c_ew_e44.TabIndex = 85;
            // 
            // c_ew_c34
            // 
            this.c_ew_c34.AutoSize = true;
            this.c_ew_c34.Location = new System.Drawing.Point(572, 164);
            this.c_ew_c34.Name = "c_ew_c34";
            this.c_ew_c34.Size = new System.Drawing.Size(15, 14);
            this.c_ew_c34.TabIndex = 84;
            this.c_ew_c34.UseVisualStyleBackColor = true;
            this.c_ew_c34.CheckedChanged += new System.EventHandler(this.chc_ew_iprand);
            // 
            // c_ew_e34
            // 
            this.c_ew_e34.Location = new System.Drawing.Point(541, 161);
            this.c_ew_e34.MaxLength = 3;
            this.c_ew_e34.Name = "c_ew_e34";
            this.c_ew_e34.Size = new System.Drawing.Size(25, 20);
            this.c_ew_e34.TabIndex = 83;
            // 
            // c_ew_c24
            // 
            this.c_ew_c24.AutoSize = true;
            this.c_ew_c24.Location = new System.Drawing.Point(572, 138);
            this.c_ew_c24.Name = "c_ew_c24";
            this.c_ew_c24.Size = new System.Drawing.Size(15, 14);
            this.c_ew_c24.TabIndex = 82;
            this.c_ew_c24.UseVisualStyleBackColor = true;
            this.c_ew_c24.CheckedChanged += new System.EventHandler(this.chc_ew_iprand);
            // 
            // c_ew_e24
            // 
            this.c_ew_e24.Location = new System.Drawing.Point(541, 135);
            this.c_ew_e24.MaxLength = 3;
            this.c_ew_e24.Name = "c_ew_e24";
            this.c_ew_e24.Size = new System.Drawing.Size(25, 20);
            this.c_ew_e24.TabIndex = 81;
            // 
            // c_ew_c14
            // 
            this.c_ew_c14.AutoSize = true;
            this.c_ew_c14.Location = new System.Drawing.Point(572, 111);
            this.c_ew_c14.Name = "c_ew_c14";
            this.c_ew_c14.Size = new System.Drawing.Size(15, 14);
            this.c_ew_c14.TabIndex = 80;
            this.c_ew_c14.UseVisualStyleBackColor = true;
            this.c_ew_c14.CheckedChanged += new System.EventHandler(this.chc_ew_iprand);
            // 
            // c_ew_e14
            // 
            this.c_ew_e14.Location = new System.Drawing.Point(541, 108);
            this.c_ew_e14.MaxLength = 3;
            this.c_ew_e14.Name = "c_ew_e14";
            this.c_ew_e14.Size = new System.Drawing.Size(25, 20);
            this.c_ew_e14.TabIndex = 79;
            // 
            // c_ew_c43
            // 
            this.c_ew_c43.AutoSize = true;
            this.c_ew_c43.Location = new System.Drawing.Point(524, 188);
            this.c_ew_c43.Name = "c_ew_c43";
            this.c_ew_c43.Size = new System.Drawing.Size(15, 14);
            this.c_ew_c43.TabIndex = 78;
            this.c_ew_c43.UseVisualStyleBackColor = true;
            this.c_ew_c43.CheckedChanged += new System.EventHandler(this.chc_ew_iprand);
            // 
            // c_ew_c42
            // 
            this.c_ew_c42.AutoSize = true;
            this.c_ew_c42.Location = new System.Drawing.Point(472, 188);
            this.c_ew_c42.Name = "c_ew_c42";
            this.c_ew_c42.Size = new System.Drawing.Size(15, 14);
            this.c_ew_c42.TabIndex = 77;
            this.c_ew_c42.UseVisualStyleBackColor = true;
            this.c_ew_c42.CheckedChanged += new System.EventHandler(this.chc_ew_iprand);
            // 
            // c_ew_c41
            // 
            this.c_ew_c41.AutoSize = true;
            this.c_ew_c41.Location = new System.Drawing.Point(420, 188);
            this.c_ew_c41.Name = "c_ew_c41";
            this.c_ew_c41.Size = new System.Drawing.Size(15, 14);
            this.c_ew_c41.TabIndex = 76;
            this.c_ew_c41.UseVisualStyleBackColor = true;
            this.c_ew_c41.CheckedChanged += new System.EventHandler(this.chc_ew_iprand);
            // 
            // c_ew_e43
            // 
            this.c_ew_e43.Location = new System.Drawing.Point(493, 185);
            this.c_ew_e43.MaxLength = 3;
            this.c_ew_e43.Name = "c_ew_e43";
            this.c_ew_e43.Size = new System.Drawing.Size(25, 20);
            this.c_ew_e43.TabIndex = 75;
            // 
            // c_ew_e42
            // 
            this.c_ew_e42.Location = new System.Drawing.Point(441, 185);
            this.c_ew_e42.MaxLength = 3;
            this.c_ew_e42.Name = "c_ew_e42";
            this.c_ew_e42.Size = new System.Drawing.Size(25, 20);
            this.c_ew_e42.TabIndex = 74;
            // 
            // c_ew_e41
            // 
            this.c_ew_e41.Location = new System.Drawing.Point(389, 185);
            this.c_ew_e41.MaxLength = 3;
            this.c_ew_e41.Name = "c_ew_e41";
            this.c_ew_e41.Size = new System.Drawing.Size(25, 20);
            this.c_ew_e41.TabIndex = 73;
            // 
            // c_ew_c33
            // 
            this.c_ew_c33.AutoSize = true;
            this.c_ew_c33.Location = new System.Drawing.Point(524, 164);
            this.c_ew_c33.Name = "c_ew_c33";
            this.c_ew_c33.Size = new System.Drawing.Size(15, 14);
            this.c_ew_c33.TabIndex = 72;
            this.c_ew_c33.UseVisualStyleBackColor = true;
            this.c_ew_c33.CheckedChanged += new System.EventHandler(this.chc_ew_iprand);
            // 
            // c_ew_c32
            // 
            this.c_ew_c32.AutoSize = true;
            this.c_ew_c32.Location = new System.Drawing.Point(472, 164);
            this.c_ew_c32.Name = "c_ew_c32";
            this.c_ew_c32.Size = new System.Drawing.Size(15, 14);
            this.c_ew_c32.TabIndex = 71;
            this.c_ew_c32.UseVisualStyleBackColor = true;
            this.c_ew_c32.CheckedChanged += new System.EventHandler(this.chc_ew_iprand);
            // 
            // c_ew_c31
            // 
            this.c_ew_c31.AutoSize = true;
            this.c_ew_c31.Location = new System.Drawing.Point(420, 164);
            this.c_ew_c31.Name = "c_ew_c31";
            this.c_ew_c31.Size = new System.Drawing.Size(15, 14);
            this.c_ew_c31.TabIndex = 70;
            this.c_ew_c31.UseVisualStyleBackColor = true;
            this.c_ew_c31.CheckedChanged += new System.EventHandler(this.chc_ew_iprand);
            // 
            // c_ew_e33
            // 
            this.c_ew_e33.Location = new System.Drawing.Point(493, 161);
            this.c_ew_e33.MaxLength = 3;
            this.c_ew_e33.Name = "c_ew_e33";
            this.c_ew_e33.Size = new System.Drawing.Size(25, 20);
            this.c_ew_e33.TabIndex = 69;
            // 
            // c_ew_e32
            // 
            this.c_ew_e32.Location = new System.Drawing.Point(441, 161);
            this.c_ew_e32.MaxLength = 3;
            this.c_ew_e32.Name = "c_ew_e32";
            this.c_ew_e32.Size = new System.Drawing.Size(25, 20);
            this.c_ew_e32.TabIndex = 68;
            // 
            // c_ew_e31
            // 
            this.c_ew_e31.Location = new System.Drawing.Point(389, 161);
            this.c_ew_e31.MaxLength = 3;
            this.c_ew_e31.Name = "c_ew_e31";
            this.c_ew_e31.Size = new System.Drawing.Size(25, 20);
            this.c_ew_e31.TabIndex = 67;
            // 
            // c_ew_c23
            // 
            this.c_ew_c23.AutoSize = true;
            this.c_ew_c23.Location = new System.Drawing.Point(524, 138);
            this.c_ew_c23.Name = "c_ew_c23";
            this.c_ew_c23.Size = new System.Drawing.Size(15, 14);
            this.c_ew_c23.TabIndex = 66;
            this.c_ew_c23.UseVisualStyleBackColor = true;
            this.c_ew_c23.CheckedChanged += new System.EventHandler(this.chc_ew_iprand);
            // 
            // c_ew_c22
            // 
            this.c_ew_c22.AutoSize = true;
            this.c_ew_c22.Location = new System.Drawing.Point(472, 138);
            this.c_ew_c22.Name = "c_ew_c22";
            this.c_ew_c22.Size = new System.Drawing.Size(15, 14);
            this.c_ew_c22.TabIndex = 65;
            this.c_ew_c22.UseVisualStyleBackColor = true;
            this.c_ew_c22.CheckedChanged += new System.EventHandler(this.chc_ew_iprand);
            // 
            // c_ew_c21
            // 
            this.c_ew_c21.AutoSize = true;
            this.c_ew_c21.Location = new System.Drawing.Point(420, 138);
            this.c_ew_c21.Name = "c_ew_c21";
            this.c_ew_c21.Size = new System.Drawing.Size(15, 14);
            this.c_ew_c21.TabIndex = 64;
            this.c_ew_c21.UseVisualStyleBackColor = true;
            this.c_ew_c21.CheckedChanged += new System.EventHandler(this.chc_ew_iprand);
            // 
            // c_ew_e23
            // 
            this.c_ew_e23.Location = new System.Drawing.Point(493, 135);
            this.c_ew_e23.MaxLength = 3;
            this.c_ew_e23.Name = "c_ew_e23";
            this.c_ew_e23.Size = new System.Drawing.Size(25, 20);
            this.c_ew_e23.TabIndex = 63;
            // 
            // c_ew_e22
            // 
            this.c_ew_e22.Location = new System.Drawing.Point(441, 135);
            this.c_ew_e22.MaxLength = 3;
            this.c_ew_e22.Name = "c_ew_e22";
            this.c_ew_e22.Size = new System.Drawing.Size(25, 20);
            this.c_ew_e22.TabIndex = 62;
            // 
            // c_ew_e21
            // 
            this.c_ew_e21.Location = new System.Drawing.Point(389, 135);
            this.c_ew_e21.MaxLength = 3;
            this.c_ew_e21.Name = "c_ew_e21";
            this.c_ew_e21.Size = new System.Drawing.Size(25, 20);
            this.c_ew_e21.TabIndex = 61;
            // 
            // c_ew_c13
            // 
            this.c_ew_c13.AutoSize = true;
            this.c_ew_c13.Location = new System.Drawing.Point(524, 111);
            this.c_ew_c13.Name = "c_ew_c13";
            this.c_ew_c13.Size = new System.Drawing.Size(15, 14);
            this.c_ew_c13.TabIndex = 60;
            this.c_ew_c13.UseVisualStyleBackColor = true;
            this.c_ew_c13.CheckedChanged += new System.EventHandler(this.chc_ew_iprand);
            // 
            // c_ew_c12
            // 
            this.c_ew_c12.AutoSize = true;
            this.c_ew_c12.Location = new System.Drawing.Point(472, 111);
            this.c_ew_c12.Name = "c_ew_c12";
            this.c_ew_c12.Size = new System.Drawing.Size(15, 14);
            this.c_ew_c12.TabIndex = 59;
            this.c_ew_c12.UseVisualStyleBackColor = true;
            this.c_ew_c12.CheckedChanged += new System.EventHandler(this.chc_ew_iprand);
            // 
            // c_ew_c11
            // 
            this.c_ew_c11.AutoSize = true;
            this.c_ew_c11.Location = new System.Drawing.Point(420, 111);
            this.c_ew_c11.Name = "c_ew_c11";
            this.c_ew_c11.Size = new System.Drawing.Size(15, 14);
            this.c_ew_c11.TabIndex = 58;
            this.c_ew_c11.UseVisualStyleBackColor = true;
            this.c_ew_c11.CheckedChanged += new System.EventHandler(this.chc_ew_iprand);
            // 
            // c_ew_e13
            // 
            this.c_ew_e13.Location = new System.Drawing.Point(493, 108);
            this.c_ew_e13.MaxLength = 3;
            this.c_ew_e13.Name = "c_ew_e13";
            this.c_ew_e13.Size = new System.Drawing.Size(25, 20);
            this.c_ew_e13.TabIndex = 56;
            // 
            // c_ew_e12
            // 
            this.c_ew_e12.Location = new System.Drawing.Point(441, 108);
            this.c_ew_e12.MaxLength = 3;
            this.c_ew_e12.Name = "c_ew_e12";
            this.c_ew_e12.Size = new System.Drawing.Size(25, 20);
            this.c_ew_e12.TabIndex = 55;
            // 
            // c_ew_e11
            // 
            this.c_ew_e11.Location = new System.Drawing.Point(389, 108);
            this.c_ew_e11.MaxLength = 3;
            this.c_ew_e11.Name = "c_ew_e11";
            this.c_ew_e11.Size = new System.Drawing.Size(25, 20);
            this.c_ew_e11.TabIndex = 54;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(365, 188);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 53;
            this.label5.Text = "IP4:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(365, 165);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 52;
            this.label4.Text = "IP3:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(365, 138);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(26, 13);
            this.label3.TabIndex = 51;
            this.label3.Text = "IP2:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(365, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(26, 13);
            this.label2.TabIndex = 50;
            this.label2.Text = "IP1:";
            // 
            // c_ew_ip
            // 
            this.c_ew_ip.AutoSize = true;
            this.c_ew_ip.Location = new System.Drawing.Point(376, 57);
            this.c_ew_ip.Name = "c_ew_ip";
            this.c_ew_ip.Size = new System.Drawing.Size(130, 17);
            this.c_ew_ip.TabIndex = 49;
            this.c_ew_ip.Text = "Custom EnterWorld IP";
            this.c_ew_ip.UseVisualStyleBackColor = true;
            this.c_ew_ip.CheckedChanged += new System.EventHandler(this.chc_ew_ip_CheckedChanged);
            // 
            // textBox_game_listenport
            // 
            this.textBox_game_listenport.Location = new System.Drawing.Point(124, 51);
            this.textBox_game_listenport.MaxLength = 5;
            this.textBox_game_listenport.Name = "textBox_game_listenport";
            this.textBox_game_listenport.Size = new System.Drawing.Size(81, 20);
            this.textBox_game_listenport.TabIndex = 48;
            this.textBox_game_listenport.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(236, 93);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 24);
            this.label1.TabIndex = 47;
            this.label1.Text = "Game Server Port";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // comboBox_gsList
            // 
            this.comboBox_gsList.Location = new System.Drawing.Point(171, 2);
            this.comboBox_gsList.Name = "comboBox_gsList";
            this.comboBox_gsList.Size = new System.Drawing.Size(157, 21);
            this.comboBox_gsList.TabIndex = 46;
            this.comboBox_gsList.SelectedIndexChanged += new System.EventHandler(this.comboBox_gsList_SelectedIndexChanged);
            // 
            // checkBox_ManualGK
            // 
            this.checkBox_ManualGK.AutoSize = true;
            this.checkBox_ManualGK.Location = new System.Drawing.Point(225, 76);
            this.checkBox_ManualGK.Name = "checkBox_ManualGK";
            this.checkBox_ManualGK.Size = new System.Drawing.Size(109, 17);
            this.checkBox_ManualGK.TabIndex = 45;
            this.checkBox_ManualGK.Text = "Manual Gamekey";
            this.checkBox_ManualGK.UseVisualStyleBackColor = true;
            // 
            // label_packet
            // 
            this.label_packet.Location = new System.Drawing.Point(332, 27);
            this.label_packet.Name = "label_packet";
            this.label_packet.Size = new System.Drawing.Size(51, 24);
            this.label_packet.TabIndex = 44;
            this.label_packet.Text = "Packet";
            this.label_packet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_Custom_EnterWorld
            // 
            this.textBox_Custom_EnterWorld.Location = new System.Drawing.Point(379, 28);
            this.textBox_Custom_EnterWorld.MaxLength = 4096;
            this.textBox_Custom_EnterWorld.Name = "textBox_Custom_EnterWorld";
            this.textBox_Custom_EnterWorld.Size = new System.Drawing.Size(209, 20);
            this.textBox_Custom_EnterWorld.TabIndex = 43;
            this.textBox_Custom_EnterWorld.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkBox_Custom_EnterWorld
            // 
            this.checkBox_Custom_EnterWorld.AutoSize = true;
            this.checkBox_Custom_EnterWorld.Location = new System.Drawing.Point(376, 4);
            this.checkBox_Custom_EnterWorld.Name = "checkBox_Custom_EnterWorld";
            this.checkBox_Custom_EnterWorld.Size = new System.Drawing.Size(154, 17);
            this.checkBox_Custom_EnterWorld.TabIndex = 42;
            this.checkBox_Custom_EnterWorld.Text = "Custom EnterWorld Packet";
            this.checkBox_Custom_EnterWorld.UseVisualStyleBackColor = true;
            // 
            // checkBox_game_proxy
            // 
            this.checkBox_game_proxy.AutoSize = true;
            this.checkBox_game_proxy.Location = new System.Drawing.Point(5, 100);
            this.checkBox_game_proxy.Name = "checkBox_game_proxy";
            this.checkBox_game_proxy.Size = new System.Drawing.Size(210, 17);
            this.checkBox_game_proxy.TabIndex = 41;
            this.checkBox_game_proxy.Text = "Use Proxy for Game Server connection";
            this.checkBox_game_proxy.UseVisualStyleBackColor = true;
            // 
            // textBox_socks_password
            // 
            this.textBox_socks_password.Location = new System.Drawing.Point(144, 201);
            this.textBox_socks_password.MaxLength = 32;
            this.textBox_socks_password.Name = "textBox_socks_password";
            this.textBox_socks_password.Size = new System.Drawing.Size(184, 20);
            this.textBox_socks_password.TabIndex = 40;
            this.textBox_socks_password.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_socks_password
            // 
            this.label_socks_password.Location = new System.Drawing.Point(5, 199);
            this.label_socks_password.Name = "label_socks_password";
            this.label_socks_password.Size = new System.Drawing.Size(133, 24);
            this.label_socks_password.TabIndex = 39;
            this.label_socks_password.Text = "SOCKS5 Password";
            this.label_socks_password.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_socks_username
            // 
            this.textBox_socks_username.Location = new System.Drawing.Point(142, 175);
            this.textBox_socks_username.MaxLength = 32;
            this.textBox_socks_username.Name = "textBox_socks_username";
            this.textBox_socks_username.Size = new System.Drawing.Size(184, 20);
            this.textBox_socks_username.TabIndex = 38;
            this.textBox_socks_username.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_socks_username
            // 
            this.label_socks_username.Location = new System.Drawing.Point(5, 173);
            this.label_socks_username.Name = "label_socks_username";
            this.label_socks_username.Size = new System.Drawing.Size(131, 24);
            this.label_socks_username.TabIndex = 37;
            this.label_socks_username.Text = "SOCKS5 Username";
            this.label_socks_username.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_socks_ip
            // 
            this.label_socks_ip.Location = new System.Drawing.Point(8, 120);
            this.label_socks_ip.Name = "label_socks_ip";
            this.label_socks_ip.Size = new System.Drawing.Size(128, 24);
            this.label_socks_ip.TabIndex = 36;
            this.label_socks_ip.Text = "SOCKS5 IP";
            this.label_socks_ip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_socks_port
            // 
            this.textBox_socks_port.Location = new System.Drawing.Point(142, 149);
            this.textBox_socks_port.MaxLength = 5;
            this.textBox_socks_port.Name = "textBox_socks_port";
            this.textBox_socks_port.Size = new System.Drawing.Size(184, 20);
            this.textBox_socks_port.TabIndex = 35;
            this.textBox_socks_port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_socks_port
            // 
            this.label_socks_port.Location = new System.Drawing.Point(8, 146);
            this.label_socks_port.Name = "label_socks_port";
            this.label_socks_port.Size = new System.Drawing.Size(128, 24);
            this.label_socks_port.TabIndex = 34;
            this.label_socks_port.Text = "SOCKS5 Port";
            this.label_socks_port.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_socks_ip
            // 
            this.textBox_socks_ip.Location = new System.Drawing.Point(142, 123);
            this.textBox_socks_ip.MaxLength = 15;
            this.textBox_socks_ip.Name = "textBox_socks_ip";
            this.textBox_socks_ip.Size = new System.Drawing.Size(184, 20);
            this.textBox_socks_ip.TabIndex = 33;
            this.textBox_socks_ip.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkBox_login_proxy
            // 
            this.checkBox_login_proxy.AutoSize = true;
            this.checkBox_login_proxy.Location = new System.Drawing.Point(5, 77);
            this.checkBox_login_proxy.Name = "checkBox_login_proxy";
            this.checkBox_login_proxy.Size = new System.Drawing.Size(208, 17);
            this.checkBox_login_proxy.TabIndex = 32;
            this.checkBox_login_proxy.Text = "Use Proxy for Login Server connection";
            this.checkBox_login_proxy.UseVisualStyleBackColor = true;
            // 
            // textBox_game_port
            // 
            this.textBox_game_port.Location = new System.Drawing.Point(245, 27);
            this.textBox_game_port.MaxLength = 5;
            this.textBox_game_port.Name = "textBox_game_port";
            this.textBox_game_port.Size = new System.Drawing.Size(81, 20);
            this.textBox_game_port.TabIndex = 30;
            this.textBox_game_port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_game_port
            // 
            this.label_game_port.Location = new System.Drawing.Point(3, 49);
            this.label_game_port.Name = "label_game_port";
            this.label_game_port.Size = new System.Drawing.Size(133, 24);
            this.label_game_port.TabIndex = 29;
            this.label_game_port.Text = "Game Server Listen Port";
            this.label_game_port.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox_game_ip
            // 
            this.textBox_game_ip.Location = new System.Drawing.Point(109, 28);
            this.textBox_game_ip.MaxLength = 15;
            this.textBox_game_ip.Name = "textBox_game_ip";
            this.textBox_game_ip.Size = new System.Drawing.Size(131, 20);
            this.textBox_game_ip.TabIndex = 28;
            this.textBox_game_ip.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkBox_override_gameserver
            // 
            this.checkBox_override_gameserver.AutoSize = true;
            this.checkBox_override_gameserver.Location = new System.Drawing.Point(5, 4);
            this.checkBox_override_gameserver.Name = "checkBox_override_gameserver";
            this.checkBox_override_gameserver.Size = new System.Drawing.Size(165, 17);
            this.checkBox_override_gameserver.TabIndex = 0;
            this.checkBox_override_gameserver.Text = "Overide Game Server IP/Port";
            this.checkBox_override_gameserver.UseVisualStyleBackColor = true;
            // 
            // label_game_ip
            // 
            this.label_game_ip.Location = new System.Drawing.Point(3, 25);
            this.label_game_ip.Name = "label_game_ip";
            this.label_game_ip.Size = new System.Drawing.Size(116, 24);
            this.label_game_ip.TabIndex = 31;
            this.label_game_ip.Text = "Game Server IP/Port";
            this.label_game_ip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBox_advanced
            // 
            this.checkBox_advanced.AutoSize = true;
            this.checkBox_advanced.Location = new System.Drawing.Point(60, 161);
            this.checkBox_advanced.Name = "checkBox_advanced";
            this.checkBox_advanced.Size = new System.Drawing.Size(143, 17);
            this.checkBox_advanced.TabIndex = 32;
            this.checkBox_advanced.Text = "Advanced Login Options";
            this.checkBox_advanced.UseVisualStyleBackColor = true;
            this.checkBox_advanced.CheckedChanged += new System.EventHandler(this.checkBox_advanced_CheckedChanged);
            // 
            // label_prot
            // 
            this.label_prot.Location = new System.Drawing.Point(78, 88);
            this.label_prot.Name = "label_prot";
            this.label_prot.Size = new System.Drawing.Size(48, 24);
            this.label_prot.TabIndex = 28;
            this.label_prot.Text = "Protocol";
            this.label_prot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_logon_prot
            // 
            this.textBox_logon_prot.Location = new System.Drawing.Point(132, 91);
            this.textBox_logon_prot.Name = "textBox_logon_prot";
            this.textBox_logon_prot.Size = new System.Drawing.Size(88, 20);
            this.textBox_logon_prot.TabIndex = 27;
            this.textBox_logon_prot.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBox_blowfish
            // 
            this.comboBox_blowfish.Location = new System.Drawing.Point(7, 32);
            this.comboBox_blowfish.Name = "comboBox_blowfish";
            this.comboBox_blowfish.Size = new System.Drawing.Size(144, 21);
            this.comboBox_blowfish.TabIndex = 0;
            // 
            // label_blowfishkey
            // 
            this.label_blowfishkey.Location = new System.Drawing.Point(6, 5);
            this.label_blowfishkey.Name = "label_blowfishkey";
            this.label_blowfishkey.Size = new System.Drawing.Size(120, 24);
            this.label_blowfishkey.TabIndex = 23;
            this.label_blowfishkey.Text = "Blowfish Key(token)";
            this.label_blowfishkey.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_blowfish
            // 
            this.textBox_blowfish.Location = new System.Drawing.Point(7, 65);
            this.textBox_blowfish.MaxLength = 32;
            this.textBox_blowfish.Name = "textBox_blowfish";
            this.textBox_blowfish.Size = new System.Drawing.Size(274, 20);
            this.textBox_blowfish.TabIndex = 1;
            this.textBox_blowfish.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button_select_ig
            // 
            this.button_select_ig.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_select_ig.Location = new System.Drawing.Point(248, 115);
            this.button_select_ig.Name = "button_select_ig";
            this.button_select_ig.Size = new System.Drawing.Size(152, 40);
            this.button_select_ig.TabIndex = 6;
            this.button_select_ig.Text = "IG";
            this.button_select_ig.Click += new System.EventHandler(this.button_select_ig_Click);
            // 
            // button_select_oog
            // 
            this.button_select_oog.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_select_oog.Location = new System.Drawing.Point(60, 115);
            this.button_select_oog.Name = "button_select_oog";
            this.button_select_oog.Size = new System.Drawing.Size(160, 40);
            this.button_select_oog.TabIndex = 5;
            this.button_select_oog.Text = "OOG";
            this.button_select_oog.Click += new System.EventHandler(this.button_select_oog_Click);
            // 
            // panel_ig
            // 
            this.panel_ig.Controls.Add(this.checkBox_security_old_client);
            this.panel_ig.Controls.Add(this.label_IG_pin);
            this.panel_ig.Controls.Add(this.textBox_IG_pin);
            this.panel_ig.Controls.Add(this.checkBox_OverrideProtocol);
            this.panel_ig.Controls.Add(this.label_loginport);
            this.panel_ig.Controls.Add(this.label_ip);
            this.panel_ig.Controls.Add(this.button_back_ig);
            this.panel_ig.Controls.Add(this.textBox_local_port);
            this.panel_ig.Controls.Add(this.label_localport);
            this.panel_ig.Controls.Add(this.comboBox_ig_login);
            this.panel_ig.Controls.Add(this.label_loginserver);
            this.panel_ig.Controls.Add(this.textBox_local_ip);
            this.panel_ig.Controls.Add(this.textBox_ig_login_port);
            this.panel_ig.Controls.Add(this.textBox_ig_login_ip);
            this.panel_ig.Controls.Add(this.button_ig_listen);
            this.panel_ig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_ig.Location = new System.Drawing.Point(0, 0);
            this.panel_ig.Name = "panel_ig";
            this.panel_ig.Size = new System.Drawing.Size(607, 445);
            this.panel_ig.TabIndex = 0;
            // 
            // checkBox_security_old_client
            // 
            this.checkBox_security_old_client.AutoSize = true;
            this.checkBox_security_old_client.Location = new System.Drawing.Point(266, 91);
            this.checkBox_security_old_client.Name = "checkBox_security_old_client";
            this.checkBox_security_old_client.Size = new System.Drawing.Size(71, 17);
            this.checkBox_security_old_client.TabIndex = 32;
            this.checkBox_security_old_client.Text = "Old Client";
            this.checkBox_security_old_client.UseVisualStyleBackColor = true;
            this.checkBox_security_old_client.Visible = false;
            // 
            // label_IG_pin
            // 
            this.label_IG_pin.Location = new System.Drawing.Point(61, 86);
            this.label_IG_pin.Name = "label_IG_pin";
            this.label_IG_pin.Size = new System.Drawing.Size(93, 24);
            this.label_IG_pin.TabIndex = 31;
            this.label_IG_pin.Text = "Pin";
            this.label_IG_pin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_IG_pin
            // 
            this.textBox_IG_pin.Location = new System.Drawing.Point(160, 89);
            this.textBox_IG_pin.Name = "textBox_IG_pin";
            this.textBox_IG_pin.Size = new System.Drawing.Size(100, 20);
            this.textBox_IG_pin.TabIndex = 30;
            this.textBox_IG_pin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // checkBox_OverrideProtocol
            // 
            this.checkBox_OverrideProtocol.AutoSize = true;
            this.checkBox_OverrideProtocol.Location = new System.Drawing.Point(160, 178);
            this.checkBox_OverrideProtocol.Name = "checkBox_OverrideProtocol";
            this.checkBox_OverrideProtocol.Size = new System.Drawing.Size(146, 17);
            this.checkBox_OverrideProtocol.TabIndex = 29;
            this.checkBox_OverrideProtocol.Text = "Override Protocol Version";
            this.checkBox_OverrideProtocol.UseVisualStyleBackColor = true;
            // 
            // label_loginport
            // 
            this.label_loginport.Location = new System.Drawing.Point(63, 57);
            this.label_loginport.Name = "label_loginport";
            this.label_loginport.Size = new System.Drawing.Size(93, 24);
            this.label_loginport.TabIndex = 28;
            this.label_loginport.Text = "Login Server Port";
            this.label_loginport.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_ip
            // 
            this.label_ip.Location = new System.Drawing.Point(107, 123);
            this.label_ip.Name = "label_ip";
            this.label_ip.Size = new System.Drawing.Size(47, 24);
            this.label_ip.TabIndex = 27;
            this.label_ip.Text = "Local IP";
            this.label_ip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_back_ig
            // 
            this.button_back_ig.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_back_ig.Location = new System.Drawing.Point(16, 208);
            this.button_back_ig.Name = "button_back_ig";
            this.button_back_ig.Size = new System.Drawing.Size(104, 24);
            this.button_back_ig.TabIndex = 26;
            this.button_back_ig.Text = "Back";
            this.button_back_ig.Click += new System.EventHandler(this.button_back_ig_Click);
            // 
            // textBox_local_port
            // 
            this.textBox_local_port.Location = new System.Drawing.Point(160, 152);
            this.textBox_local_port.MaxLength = 5;
            this.textBox_local_port.Name = "textBox_local_port";
            this.textBox_local_port.Size = new System.Drawing.Size(184, 20);
            this.textBox_local_port.TabIndex = 25;
            this.textBox_local_port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_localport
            // 
            this.label_localport.Location = new System.Drawing.Point(62, 149);
            this.label_localport.Name = "label_localport";
            this.label_localport.Size = new System.Drawing.Size(92, 24);
            this.label_localport.TabIndex = 24;
            this.label_localport.Text = "Local Login Port";
            this.label_localport.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_localport.Visible = false;
            // 
            // comboBox_ig_login
            // 
            this.comboBox_ig_login.Location = new System.Drawing.Point(10, 28);
            this.comboBox_ig_login.Name = "comboBox_ig_login";
            this.comboBox_ig_login.Size = new System.Drawing.Size(144, 21);
            this.comboBox_ig_login.TabIndex = 1;
            // 
            // label_loginserver
            // 
            this.label_loginserver.Location = new System.Drawing.Point(210, 4);
            this.label_loginserver.Name = "label_loginserver";
            this.label_loginserver.Size = new System.Drawing.Size(88, 24);
            this.label_loginserver.TabIndex = 19;
            this.label_loginserver.Text = "Login Server IP";
            this.label_loginserver.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_local_ip
            // 
            this.textBox_local_ip.Location = new System.Drawing.Point(160, 126);
            this.textBox_local_ip.MaxLength = 15;
            this.textBox_local_ip.Name = "textBox_local_ip";
            this.textBox_local_ip.Size = new System.Drawing.Size(184, 20);
            this.textBox_local_ip.TabIndex = 0;
            this.textBox_local_ip.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_ig_login_port
            // 
            this.textBox_ig_login_port.Location = new System.Drawing.Point(162, 60);
            this.textBox_ig_login_port.MaxLength = 5;
            this.textBox_ig_login_port.Name = "textBox_ig_login_port";
            this.textBox_ig_login_port.Size = new System.Drawing.Size(184, 20);
            this.textBox_ig_login_port.TabIndex = 3;
            this.textBox_ig_login_port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_ig_login_ip
            // 
            this.textBox_ig_login_ip.Location = new System.Drawing.Point(162, 28);
            this.textBox_ig_login_ip.MaxLength = 64;
            this.textBox_ig_login_ip.Name = "textBox_ig_login_ip";
            this.textBox_ig_login_ip.Size = new System.Drawing.Size(184, 20);
            this.textBox_ig_login_ip.TabIndex = 2;
            this.textBox_ig_login_ip.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button_ig_listen
            // 
            this.button_ig_listen.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_ig_listen.Location = new System.Drawing.Point(176, 208);
            this.button_ig_listen.Name = "button_ig_listen";
            this.button_ig_listen.Size = new System.Drawing.Size(168, 24);
            this.button_ig_listen.TabIndex = 7;
            this.button_ig_listen.Text = "Listen";
            this.button_ig_listen.Click += new System.EventHandler(this.button_ig_listen_Click);
            // 
            // textBox_oog_logon_ip
            // 
            this.textBox_oog_logon_ip.Location = new System.Drawing.Point(183, 6);
            this.textBox_oog_logon_ip.MaxLength = 64;
            this.textBox_oog_logon_ip.Name = "textBox_oog_logon_ip";
            this.textBox_oog_logon_ip.Size = new System.Drawing.Size(182, 20);
            this.textBox_oog_logon_ip.TabIndex = 1;
            this.textBox_oog_logon_ip.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // listView_servers
            // 
            this.listView_servers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader79,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            this.listView_servers.FullRowSelect = true;
            this.listView_servers.GridLines = true;
            this.listView_servers.HideSelection = false;
            this.listView_servers.Location = new System.Drawing.Point(7, 84);
            this.listView_servers.Name = "listView_servers";
            this.listView_servers.Size = new System.Drawing.Size(552, 71);
            this.listView_servers.TabIndex = 7;
            this.listView_servers.UseCompatibleStateImageBehavior = false;
            this.listView_servers.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "ID";
            this.columnHeader1.Width = 0;
            // 
            // columnHeader79
            // 
            this.columnHeader79.Text = "Server Name";
            this.columnHeader79.Width = 99;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "IP";
            this.columnHeader2.Width = 115;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Port";
            this.columnHeader3.Width = 76;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Age";
            this.columnHeader4.Width = 0;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "PvP";
            this.columnHeader5.Width = 0;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Players";
            this.columnHeader6.Width = 97;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Max";
            this.columnHeader7.Width = 93;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Online";
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Poop";
            this.columnHeader9.Width = 0;
            // 
            // textBox_oog_logon_port
            // 
            this.textBox_oog_logon_port.Location = new System.Drawing.Point(423, 6);
            this.textBox_oog_logon_port.MaxLength = 5;
            this.textBox_oog_logon_port.Name = "textBox_oog_logon_port";
            this.textBox_oog_logon_port.Size = new System.Drawing.Size(88, 20);
            this.textBox_oog_logon_port.TabIndex = 2;
            this.textBox_oog_logon_port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button_logon
            // 
            this.button_logon.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_logon.Location = new System.Drawing.Point(399, 57);
            this.button_logon.Name = "button_logon";
            this.button_logon.Size = new System.Drawing.Size(148, 24);
            this.button_logon.TabIndex = 6;
            this.button_logon.Text = "Logon";
            this.button_logon.Click += new System.EventHandler(this.button_logon_Click);
            // 
            // textBox_pword
            // 
            this.textBox_pword.Location = new System.Drawing.Point(183, 58);
            this.textBox_pword.MaxLength = 16;
            this.textBox_pword.Name = "textBox_pword";
            this.textBox_pword.PasswordChar = '*';
            this.textBox_pword.Size = new System.Drawing.Size(182, 20);
            this.textBox_pword.TabIndex = 5;
            this.textBox_pword.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button_server
            // 
            this.button_server.Enabled = false;
            this.button_server.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_server.Location = new System.Drawing.Point(399, 160);
            this.button_server.Name = "button_server";
            this.button_server.Size = new System.Drawing.Size(148, 24);
            this.button_server.TabIndex = 8;
            this.button_server.Text = "Select Server";
            this.button_server.Click += new System.EventHandler(this.button_server_Click);
            // 
            // button_char
            // 
            this.button_char.Enabled = false;
            this.button_char.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_char.Location = new System.Drawing.Point(434, 307);
            this.button_char.Name = "button_char";
            this.button_char.Size = new System.Drawing.Size(125, 24);
            this.button_char.TabIndex = 10;
            this.button_char.Text = "Select Char";
            this.button_char.Click += new System.EventHandler(this.button_char_Click);
            // 
            // textBox_lname
            // 
            this.textBox_lname.Location = new System.Drawing.Point(183, 32);
            this.textBox_lname.MaxLength = 255;
            this.textBox_lname.Name = "textBox_lname";
            this.textBox_lname.Size = new System.Drawing.Size(182, 20);
            this.textBox_lname.TabIndex = 4;
            this.textBox_lname.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBox_oog_login
            // 
            this.comboBox_oog_login.Location = new System.Drawing.Point(23, 6);
            this.comboBox_oog_login.Name = "comboBox_oog_login";
            this.comboBox_oog_login.Size = new System.Drawing.Size(144, 21);
            this.comboBox_oog_login.TabIndex = 0;
            // 
            // button_back_oog
            // 
            this.button_back_oog.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_back_oog.Location = new System.Drawing.Point(20, 307);
            this.button_back_oog.Name = "button_back_oog";
            this.button_back_oog.Size = new System.Drawing.Size(104, 24);
            this.button_back_oog.TabIndex = 11;
            this.button_back_oog.Text = "Back";
            this.button_back_oog.Click += new System.EventHandler(this.button_back_oog_Click);
            // 
            // panel_oog
            // 
            this.panel_oog.Controls.Add(this.label_security_pin);
            this.panel_oog.Controls.Add(this.textBox_security_pin);
            this.panel_oog.Controls.Add(this.listView_chars);
            this.panel_oog.Controls.Add(this.button_delete_char);
            this.panel_oog.Controls.Add(this.button_oog_create);
            this.panel_oog.Controls.Add(this.label_oog_password);
            this.panel_oog.Controls.Add(this.label_oog_username);
            this.panel_oog.Controls.Add(this.label_oog_loginport);
            this.panel_oog.Controls.Add(this.button_back_oog);
            this.panel_oog.Controls.Add(this.comboBox_oog_login);
            this.panel_oog.Controls.Add(this.textBox_lname);
            this.panel_oog.Controls.Add(this.button_char);
            this.panel_oog.Controls.Add(this.button_server);
            this.panel_oog.Controls.Add(this.textBox_pword);
            this.panel_oog.Controls.Add(this.button_logon);
            this.panel_oog.Controls.Add(this.textBox_oog_logon_port);
            this.panel_oog.Controls.Add(this.listView_servers);
            this.panel_oog.Controls.Add(this.textBox_oog_logon_ip);
            this.panel_oog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_oog.Location = new System.Drawing.Point(0, 0);
            this.panel_oog.Name = "panel_oog";
            this.panel_oog.Size = new System.Drawing.Size(607, 445);
            this.panel_oog.TabIndex = 16;
            // 
            // label_security_pin
            // 
            this.label_security_pin.Location = new System.Drawing.Point(384, 29);
            this.label_security_pin.Name = "label_security_pin";
            this.label_security_pin.Size = new System.Drawing.Size(32, 24);
            this.label_security_pin.TabIndex = 32;
            this.label_security_pin.Text = "Pin";
            this.label_security_pin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox_security_pin
            // 
            this.textBox_security_pin.Location = new System.Drawing.Point(423, 32);
            this.textBox_security_pin.MaxLength = 14;
            this.textBox_security_pin.Name = "textBox_security_pin";
            this.textBox_security_pin.Size = new System.Drawing.Size(88, 20);
            this.textBox_security_pin.TabIndex = 31;
            this.textBox_security_pin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // listView_chars
            // 
            this.listView_chars.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15,
            this.columnHeader16,
            this.columnHeader17,
            this.columnHeader18,
            this.columnHeader19,
            this.columnHeader20,
            this.columnHeader21,
            this.columnHeader22,
            this.columnHeader23,
            this.columnHeader24,
            this.columnHeader25,
            this.columnHeader26,
            this.columnHeader27,
            this.columnHeader28});
            this.listView_chars.FullRowSelect = true;
            this.listView_chars.GridLines = true;
            this.listView_chars.HideSelection = false;
            this.listView_chars.Location = new System.Drawing.Point(9, 190);
            this.listView_chars.Name = "listView_chars";
            this.listView_chars.Size = new System.Drawing.Size(552, 114);
            this.listView_chars.TabIndex = 30;
            this.listView_chars.UseCompatibleStateImageBehavior = false;
            this.listView_chars.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Name";
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "ID";
            this.columnHeader11.Width = 0;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "login name";
            this.columnHeader12.Width = 0;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "session";
            this.columnHeader13.Width = 0;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "clan";
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "???";
            this.columnHeader15.Width = 0;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "sex";
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "race";
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "baseclass";
            // 
            // columnHeader19
            // 
            this.columnHeader19.Text = "active";
            // 
            // columnHeader20
            // 
            this.columnHeader20.Text = "x";
            // 
            // columnHeader21
            // 
            this.columnHeader21.Text = "y";
            // 
            // columnHeader22
            // 
            this.columnHeader22.Text = "z";
            // 
            // columnHeader23
            // 
            this.columnHeader23.Text = "hp cur";
            // 
            // columnHeader24
            // 
            this.columnHeader24.Text = "mp cur";
            // 
            // columnHeader25
            // 
            this.columnHeader25.Text = "sp";
            // 
            // columnHeader26
            // 
            this.columnHeader26.Text = "xp";
            // 
            // columnHeader27
            // 
            this.columnHeader27.Text = "lvl";
            // 
            // columnHeader28
            // 
            this.columnHeader28.Text = "karma";
            // 
            // button_delete_char
            // 
            this.button_delete_char.Enabled = false;
            this.button_delete_char.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_delete_char.Location = new System.Drawing.Point(172, 307);
            this.button_delete_char.Name = "button_delete_char";
            this.button_delete_char.Size = new System.Drawing.Size(125, 24);
            this.button_delete_char.TabIndex = 24;
            this.button_delete_char.Text = "Delete Char";
            this.button_delete_char.Click += new System.EventHandler(this.button_delete_char_Click);
            // 
            // button_oog_create
            // 
            this.button_oog_create.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_oog_create.Location = new System.Drawing.Point(303, 307);
            this.button_oog_create.Name = "button_oog_create";
            this.button_oog_create.Size = new System.Drawing.Size(125, 24);
            this.button_oog_create.TabIndex = 23;
            this.button_oog_create.Text = "Create New Char";
            this.button_oog_create.Click += new System.EventHandler(this.button_oog_create_Click);
            // 
            // label_oog_password
            // 
            this.label_oog_password.AutoSize = true;
            this.label_oog_password.Location = new System.Drawing.Point(124, 61);
            this.label_oog_password.Name = "label_oog_password";
            this.label_oog_password.Size = new System.Drawing.Size(53, 13);
            this.label_oog_password.TabIndex = 22;
            this.label_oog_password.Text = "Password";
            // 
            // label_oog_username
            // 
            this.label_oog_username.AutoSize = true;
            this.label_oog_username.Location = new System.Drawing.Point(124, 35);
            this.label_oog_username.Name = "label_oog_username";
            this.label_oog_username.Size = new System.Drawing.Size(55, 13);
            this.label_oog_username.TabIndex = 21;
            this.label_oog_username.Text = "Username";
            // 
            // label_oog_loginport
            // 
            this.label_oog_loginport.Location = new System.Drawing.Point(385, 3);
            this.label_oog_loginport.Name = "label_oog_loginport";
            this.label_oog_loginport.Size = new System.Drawing.Size(32, 24);
            this.label_oog_loginport.TabIndex = 20;
            this.label_oog_loginport.Text = "Port";
            this.label_oog_loginport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Login
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(607, 445);
            this.Controls.Add(this.panel_select);
            this.Controls.Add(this.panel_ig);
            this.Controls.Add(this.panel_oog);
            this.Name = "Login";
            this.Text = "Login";
            this.panel_select.ResumeLayout(false);
            this.panel_select.PerformLayout();
            this.panel_advanced.ResumeLayout(false);
            this.panel_advanced.PerformLayout();
            this.panel_ig.ResumeLayout(false);
            this.panel_ig.PerformLayout();
            this.panel_oog.ResumeLayout(false);
            this.panel_oog.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

		private bool Set_Modes()
		{
            try
            {

                string blowkey = textBox_blowfish.Text;
                //blowkey.PadRight(20,' ');
                Globals.gamedata.SetBlowfishKey(blowkey);
                if (radioButton_c17.Checked)
                    Globals.gamedata.Chron = Chronicle.CT3_2;
                if (radioButton_c18.Checked)
                    Globals.gamedata.Chron = Chronicle.CT4_0;
                return true;
            }
            catch
            {
                Globals.l2net_home.Add_Error("crash: wtf blowfish key... double check and try again");
                return false;
            }
        }

		private void Load_Blowfish()
		{
			try
			{
				//load the blowfish list
				blowfish_list.Clear();
				System.IO.StreamReader blowfish_file = new System.IO.StreamReader("config\\blowfish.txt");

				string b_name;
				string b_ip;

				while( (b_name = blowfish_file.ReadLine()) != null)
				{
					b_ip = blowfish_file.ReadLine();
					comboBox_blowfish.Items.Add(b_name);
					blowfish_list.Add(b_ip);
				}
				blowfish_file.Close();

				Globals.l2net_home.Add_Text("loaded saved blowfish list", Globals.Red, TextType.BOT);
			}
			catch
			{
				Globals.l2net_home.Add_Error("failed to load saved blowfish list");
			}
		}

		private void Load_Servers()
		{
			try
			{
				//load the login server list
				loginserver_list.Clear();
				System.IO.StreamReader login_file = new System.IO.StreamReader("config\\loginlist.txt");

				string l_name;
				string l_ip;

				while( (l_name = login_file.ReadLine()) != null)
				{
					l_ip = login_file.ReadLine();
					comboBox_ig_login.Items.Add(l_name);
					comboBox_oog_login.Items.Add(l_name);
					loginserver_list.Add(l_ip);
				}
				login_file.Close();
				Globals.l2net_home.Add_Text("loaded saved server list", Globals.Red, TextType.BOT);
			}
			catch
			{
				Globals.l2net_home.Add_Error("failed to load saved server list");
			}
		}

        private void Load_GameServers()
        {
            try
            {
                //load the login server list
                gameserver_list.Clear();
                System.IO.StreamReader gs_file = new System.IO.StreamReader("config\\gslist.txt");

                string gs_name;
                string gs_ip;

                while ((gs_name = gs_file.ReadLine()) != null)
                {
                    gs_ip = gs_file.ReadLine();
                    comboBox_gsList.Items.Add(gs_name);
                    gameserver_list.Add(gs_ip);
                }
                gs_file.Close();
                Globals.l2net_home.Add_Text("loaded saved gameserver list", Globals.Red, TextType.BOT);
            }
            catch
            {
                //Globals.l2net_home.Add_Error("failed to load saved gameserver list");
            }
        }

        private void Load_EnterWorld()
        {
            try
            {
                System.IO.StreamReader ew_file = new System.IO.StreamReader("config\\enterworld.txt");

                string ew = ew_file.ReadLine();
                ew_file.Close();

                textBox_Custom_EnterWorld.Text = ew;

                Globals.l2net_home.Add_Text("loaded saved enterworld packet", Globals.Red, TextType.BOT);
            }
            catch
            {
                //Globals.l2net_home.Add_Error("failed to load saved enterworld packet");
            }
        }

		private void comboBox_ig_login_SelectedIndexChanged(object sender, EventArgs e)
		{
            string s = loginserver_list[comboBox_ig_login.SelectedIndex].ToString();
            string s2 = null;
            string[] words = s.Split(' ',':');
            textBox_ig_login_ip.Text = words[0];
            try
            {
                s2 = words[1];
            }
            catch
            {    
                //Old loginlist.txt file
                //Globals.l2net_home.Add_Error("No port set in loginlist.txt, defaulting to port 2106");
                textBox_ig_login_port.Text = "2106";
            }
            if (!String.IsNullOrEmpty(s2))
            {
                textBox_ig_login_port.Text = s2;
            }
			//textBox_ig_login_ip.Text = loginserver_list[comboBox_ig_login.SelectedIndex].ToString();
		}

		private void comboBox_oog_login_SelectedIndexChanged(object sender, EventArgs e)
		{
            string s = loginserver_list[comboBox_oog_login.SelectedIndex].ToString();
            string s2 = null;
            string[] words = s.Split(' ', ':');
            textBox_oog_logon_ip.Text = words[0];
            try
            {
                s2 = words[1];
            }
            catch
            {
                //Old loginlist.txt file
                //Globals.l2net_home.Add_Error("No port set in loginlist.txt, defaulting to port 2106");
                textBox_oog_logon_port.Text = "2106";
            }
            if (!String.IsNullOrEmpty(s2))
            {
                textBox_oog_logon_port.Text = s2;
            }
			//textBox_oog_logon_ip.Text = loginserver_list[comboBox_oog_login.SelectedIndex].ToString();
		}

		private void comboBox_blowfish_SelectedIndexChanged(object sender, EventArgs e)
		{
			textBox_blowfish.Text = blowfish_list[comboBox_blowfish.SelectedIndex].ToString();
		}

		private void button_select_oog_Click(object sender, System.EventArgs e)
		{
			if(Set_Modes())
			    panel_oog.BringToFront();
		}

		private void button_select_ig_Click(object sender, System.EventArgs e)
		{
			if(Set_Modes())
			    panel_ig.BringToFront();
		}

        private void Save_Common()
        {
            Globals.gamedata.Protocol = Util.GetInt32(textBox_logon_prot.Text);

            if (checkBox_Unknown_Blowfish.Checked)
            {
                Globals.gamedata.Unkown_Blowfish = true;
            }

            if (checkBox_LS_GS_Same_IP.Checked)
            {
                Globals.gamedata.LS_GS_Same_IP = true;
            }

            if (checkBox_advanced.Checked)
            {
                if (checkBox_override_gameserver.Checked)
                {
                    Globals.gamedata.Override_GameServer = true;
                    Globals.gamedata.Override_Game_IP = textBox_game_ip.Text;
                    Globals.gamedata.Override_Game_Port = Util.GetInt32(textBox_game_port.Text);
                    if (checkBox_Unknown_Blowfish.Checked)
                    {
                        Globals.gamedata.IG_Local_Game_Port = Util.GetInt32(textBox_game_listenport.Text);
                    }
                }

                if (checkBox_login_proxy.Checked)
                {
                    Globals.gamedata.UseProxy_LoginServer = true;
                }

                if (checkBox_game_proxy.Checked)
                {
                    Globals.gamedata.UseProxy_GameServer = true;
                }

                if (checkBox_Custom_EnterWorld.Checked)
                {
                    Globals.enterworld_check = true;
                    Globals.enterworld_custom = textBox_Custom_EnterWorld.Text;
                }

                if (checkBox_ManualGK.Checked)
                {
                    Globals.gamedata.ManualGameKey = true;
                }

                Globals.game_srv_listen_prt = textBox_game_listenport.Text;
                Globals.gamedata.Proxy_IP = textBox_socks_ip.Text;
                Globals.gamedata.Proxy_Port = Util.GetInt32(textBox_socks_port.Text);
                Globals.gamedata.Proxy_UserName = textBox_socks_username.Text;
                Globals.gamedata.Proxy_Password = textBox_socks_password.Text;
            }
        }

/******************In game login section*************************/
		private void button_ig_listen_Click(object sender, System.EventArgs e)
		{
            if (checkBox_OverrideProtocol.Checked)
            {
                Globals.gamedata.OverrideProtocol = true;
            }
            else
            {
                Globals.gamedata.OverrideProtocol = false;
            }
            if (checkBox_security_old_client.Checked)
            {
                Globals.gamedata.SecurityPinOldClient = true;
            }
            else
            {
                Globals.gamedata.SecurityPinOldClient = false;
            }
			button_back_ig.Enabled = false;
			button_ig_listen.Enabled = false;
            Globals.OOG = false;

            Globals.gamedata.IG_Local_IP = textBox_local_ip.Text;
            Globals.gamedata.IG_Local_Login_Port = Util.GetInt32(textBox_local_port.Text);

            Globals.gamedata.Login_IP = textBox_ig_login_ip.Text;
            Globals.gamedata.Login_Port = Util.GetInt32(textBox_ig_login_port.Text);
            Globals.SecurityPin = textBox_IG_pin.Text;

            Save_Common();

            LoginServer.IG_Init();
    	}
		
/******************out of game login section*************************/
		private void button_logon_Click(object sender, System.EventArgs e)
		{
            if (Globals.Login_State != 0)
            {
                Globals.l2net_home.Add_Error("wrong state...");
                button_logon.Enabled = true;
                return;
            }

			button_back_oog.Enabled = false;
			button_logon.Enabled = false;
            Globals.OOG = true;

            Globals.UserName = textBox_lname.Text;
            Globals.Password = textBox_pword.Text;
            Globals.SecurityPin = textBox_security_pin.Text;

            Globals.gamedata.Login_IP = textBox_oog_logon_ip.Text;
            Globals.gamedata.Login_Port = Util.GetInt32(textBox_oog_logon_port.Text);

            Save_Common();

            LoginServer.OOG_Init();
		}

        delegate void FillServerInfoCallback(byte[] dec_buff);
        public void FillServerInfo(byte[] dec_buff)
        {
            if (this.listView_servers.InvokeRequired)
            {
                FillServerInfoCallback d = new FillServerInfoCallback(FillServerInfo);
                //listView_servers.BeginInvoke(d, new object[] { dec_buff });
                listView_servers.Invoke(d, new object[] { dec_buff });
                return;
            }

            listView_servers.BeginUpdate();
            int Login_ServerCount = dec_buff[1];//number of servers

            int offset = 3;

            listView_servers.Items.Clear();

            ListViewItem ObjListItem;

            int m = 21;

            Globals.Login_Servers.Capacity = Login_ServerCount;

            for (int i = 0; i < Login_ServerCount; i++)
            {
                ObjListItem = listView_servers.Items.Add(dec_buff[0 + offset + i * m].ToString("X"));//21 is the length of the packet
                ObjListItem.SubItems.Add(Util.GetServer(Util.HexToUInt(ObjListItem.SubItems[0].Text) - 1));
                ObjListItem.SubItems.Add(((int)dec_buff[1 + offset + i * m]).ToString() + "." + ((int)dec_buff[2 + offset + i * m]).ToString() + "." + ((int)dec_buff[3 + offset + i * m]).ToString() + "." + ((int)dec_buff[4 + offset + i * m]).ToString());
                ObjListItem.SubItems.Add(System.BitConverter.ToUInt32(dec_buff, 5 + offset + i * m).ToString());//((int)pack210u[5 + offset + i * m] + ((int)pack210u[6 + offset + i * m]*256)).ToString() );// + "" + pack210u[7 + offset + i * m].ToString("X") + "-" + pack210u[8 + offset + i * m].ToString("X"));
                ObjListItem.SubItems.Add(dec_buff[9 + offset + i * m].ToString("X2"));//age limit
                ObjListItem.SubItems.Add(dec_buff[10 + offset + i * m].ToString("X2"));//pvp
                ObjListItem.SubItems.Add(System.BitConverter.ToUInt16(dec_buff, 11 + offset + i * m).ToString());//player count
                ObjListItem.SubItems.Add(System.BitConverter.ToUInt16(dec_buff, 13 + offset + i * m).ToString());//max
                ObjListItem.SubItems.Add(dec_buff[15 + offset + i * m].ToString("X2"));//online
                ObjListItem.SubItems.Add(dec_buff[16 + offset + i * m].ToString("X2") + "-" + dec_buff[17 + offset + i * m].ToString("X2") + "-" + dec_buff[18 + offset + i * m].ToString("X2") + "-" + dec_buff[19 + offset + i * m].ToString("X2") + "-" + dec_buff[20 + offset + i * m].ToString("X2"));//poo
            }
            listView_servers.EndUpdate();

            Globals.Login_State = 1;
            button_server.Enabled = true;
        }

		private void button_server_Click(object sender, System.EventArgs e)
		{
			button_server.Enabled = false;

			try
			{
                if (Globals.Login_State != 1)
				{
					Globals.l2net_home.Add_Error("wrong state...");
					button_server.Enabled = true;
					return;
				}

				if(listView_servers.SelectedIndices.Count == 0)
				{
					Globals.l2net_home.Add_Error("select a server first...");
					button_server.Enabled = true;
					return;
				}

                Globals.Login_SelectedServer = uint.Parse(listView_servers.Items[listView_servers.SelectedIndices[0]].SubItems[0].Text, System.Globalization.NumberStyles.HexNumber);

                LoginServer.OOG_SelectServer();
			}
			catch
			{
				Globals.l2net_home.Add_Error("crash: ServerSelect");
			}
		}

        delegate void CharListCallback(ByteBuffer bb);
		public void CharList(ByteBuffer bb)
		{
            if (this.listView_servers.InvokeRequired)
            {
                CharListCallback d = new CharListCallback(CharList);
                listView_servers.BeginInvoke(d, new object[] { bb });
                return;
            }

			/******************time to read in the char data**************/
            Globals.l2net_home.Add_Text("Getting char list", Globals.Red, TextType.BOT);
            
            int char_count = bb.ReadInt32();
            bb.ReadInt32();//dunno?
            bb.ReadByte();

            if (Globals.gamedata.Chron >= Chronicle.CT3_0)
            {
                bb.ReadByte(); //0x01
                bb.ReadInt32(); //00 00 00 00
            }

			ListViewItem ObjListItem;

            listView_chars.BeginUpdate();

            listView_chars.Items.Clear();
			
			for(int i = 0; i < char_count; i++)
			{
				ObjListItem = listView_chars.Items.Add(bb.ReadString());//name
				ObjListItem.SubItems.Add(bb.ReadUInt32().ToString("X"));//id
                ObjListItem.SubItems.Add(bb.ReadString());//login name
				ObjListItem.SubItems.Add(bb.ReadUInt32().ToString("X"));//session id
                ObjListItem.SubItems.Add(bb.ReadUInt32().ToString());//clan id
                ObjListItem.SubItems.Add(bb.ReadUInt32().ToString());//???
                if (bb.ReadUInt32() == 0x00)//sex
					ObjListItem.SubItems.Add("Male");
				else
					ObjListItem.SubItems.Add("Female");
                ObjListItem.SubItems.Add(Util.GetRace(bb.ReadUInt32()));//race
                ObjListItem.SubItems.Add(Util.GetClass(bb.ReadUInt32()));//base class id
                ObjListItem.SubItems.Add(bb.ReadUInt32().ToString());//active
                ObjListItem.SubItems.Add(bb.ReadInt32().ToString());//x
                ObjListItem.SubItems.Add(bb.ReadInt32().ToString());//y
                ObjListItem.SubItems.Add(bb.ReadInt32().ToString());//z
                ObjListItem.SubItems.Add(bb.ReadDouble().ToString());//hp
				ObjListItem.SubItems.Add(bb.ReadDouble().ToString());//mp
                ObjListItem.SubItems.Add(bb.ReadUInt32().ToString());//sp
				//C5 has 8bytes for XP
                ObjListItem.SubItems.Add(bb.ReadUInt64().ToString());//xp
                if (Globals.gamedata.Chron >= Chronicle.CT2_6)
                {
                    bb.ReadInt32(); //CT 2.6 newits blessing?
                    bb.ReadInt32();
                }
                ObjListItem.SubItems.Add(bb.ReadUInt32().ToString());//level
                ObjListItem.SubItems.Add(bb.ReadUInt32().ToString());//karma
                bb.SetIndex(bb.GetIndex() + 185); //Freya +221
                if (Globals.gamedata.Chron >= Chronicle.CT2_3)
                {
                    bb.ReadUInt32();
                }
                if (Globals.gamedata.Chron >= Chronicle.CT2_5)
                {
                    bb.SetIndex(bb.GetIndex() + 32);
                }
                if (Globals.gamedata.Chron >= Chronicle.CT2_6)
                {
                    bb.ReadUInt32();
                }
                if (Globals.gamedata.Chron >= Chronicle.CT3_0)
                {
                    bb.ReadUInt32();
                }
                if (Globals.gamedata.Chron >= Chronicle.CT3_0)
                {
                    bb.SetIndex(bb.GetIndex() + 36);
                }
			}

            listView_chars.EndUpdate();

            Globals.Login_State = 2;
            button_char.Enabled = true;
            button_delete_char.Enabled = true;
		}

		private void button_char_Click(object sender, System.EventArgs e)
		{
			button_char.Enabled = false;

			try
			{
				if(listView_chars.SelectedIndices.Count == 0)
				{
					Globals.l2net_home.Add_Error("select a char first...");
					button_char.Enabled = true;
					return;
				}

                Globals.l2net_home.Add_Text("logging into char", Globals.Red, TextType.BOT);

                //ServerPackets.SelectChar(int.Parse(listView_chars.Items[listView_chars.SelectedIndices[0]].SubItems[1].Text, System.Globalization.NumberStyles.HexNumber));
                if (Globals.gamedata.Chron >= Chronicle.CT3_0 && (!System.String.IsNullOrEmpty(Globals.SecurityPin)))
                {
                    ServerPackets.RequestSecurityPinWindow();

                    while (!Globals.gamedata.SecurityPinWindow)
                    {
                        System.Threading.Thread.Sleep(100);
                    }

                    Globals.l2net_home.Add_Text("Sending security pin: " + Globals.SecurityPin, Globals.Green, TextType.BOT);

                    System.Threading.Thread.Sleep(2143);
                    ServerPackets.SecurityPin();

                    while (!Globals.gamedata.SecurityPinOk)
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                    Globals.l2net_home.Add_Text("Pin OK", Globals.Green, TextType.BOT);
                }
                ServerPackets.SelectChar(listView_chars.SelectedIndices[0]);

                Globals.Login_State = 3;
			}
			catch
			{
				Globals.l2net_home.Add_Error("crash: CharLogin");
			}
		}

		private void button_back_ig_Click(object sender, System.EventArgs e)
		{
			panel_select.BringToFront();
		}

		private void button_back_oog_Click(object sender, System.EventArgs e)
		{
			panel_select.BringToFront();
		}

        private void button_oog_create_Click(object sender, EventArgs e)
        {
            CreateChar create = new CreateChar();
            create.ShowDialog();
        }

        private void checkBox_advanced_CheckedChanged(object sender, EventArgs e)
        {
            panel_advanced.Visible = !panel_advanced.Visible;

            if (Globals.enterworld_ip) // disable/enable ip edits (adifenix)
            {
                Enable_all_ip_edit();
            }
            else
            {
                Disable_all_ip_edit();
            }
        }

        private void button_delete_char_Click(object sender, EventArgs e)
        {

            try
            {
                if (listView_chars.SelectedIndices.Count == 0)
                {
                    Globals.l2net_home.Add_Error("select a char first...");
                    button_char.Enabled = true;
                    return;
                }

                if (MessageBox.Show("Are you sure you want to delete char in slot: "+ listView_chars.SelectedIndices[0] + " ?", "Delete Verification", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
                Globals.l2net_home.Add_Text("Deleting Char", Globals.Red, TextType.BOT);

                //ServerPackets.SelectChar(int.Parse(listView_chars.Items[listView_chars.SelectedIndices[0]].SubItems[1].Text, System.Globalization.NumberStyles.HexNumber));
                ServerPackets.DeleteChar(listView_chars.SelectedIndices[0]);

            }
            catch
            {
                Globals.l2net_home.Add_Error("crash: CharLogin");
            }
        }


        private void checkBox_Unknown_Blowfish_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Unknown_Blowfish.Checked)
            {
                Globals.unknow_blowfish = true;
                comboBox_blowfish.Enabled = false;
                textBox_blowfish.Enabled = false;
                checkBox_LS_GS_Same_IP.Enabled = true;
            }
            else
            {
                Globals.unknow_blowfish = false;
                comboBox_blowfish.Enabled = true;
                textBox_blowfish.Enabled = true;
                checkBox_LS_GS_Same_IP.Enabled = false;
            }
        }

        private void comboBox_gsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            string s = gameserver_list[comboBox_gsList.SelectedIndex].ToString();
            string s2 = null;
            string[] words = s.Split(' ', ':');
            textBox_game_ip.Text = words[0];
            try
            {
                s2 = words[1];
            }
            catch
            {
                //Old loginlist.txt file
                //Globals.l2net_home.Add_Error("No port set in loginlist.txt, defaulting to port 2106");
                textBox_game_port.Text = "7777";
                textBox_game_listenport.Text = "7777";
            }
            if (!String.IsNullOrEmpty(s2))
            {
                textBox_game_port.Text = s2;
                textBox_game_listenport.Text = s2;
            }

        }


        private void chc_ew_ip_CheckedChanged(object sender, EventArgs e)
        {
            if (c_ew_ip.Checked)// on - show edits
            {
                Globals.enterworld_ip = true;
                Enable_all_ip_edit();
            }
            else // off - hide edits
            {
                Globals.enterworld_ip =false;
                Disable_all_ip_edit();
            }
        }

        private void chc_ew_iprand(object sender, EventArgs e)
        {
            
            for(int i = 0 ; i<Globals.ew_chc_ed_array.Count;i++)
                if (Globals.ew_chc_ed_array[i] == sender)
                {
                    if((Globals.ew_chc_ed_array[i] as System.Windows.Forms.CheckBox).Checked)
                    {
                        Disable_ip_edit(i);
                    }
                    else
                    {
                        if (Globals.enterworld_ip)
                        {
                            Enable_ip_edit(i);
                        }
                    }

                }

        }


        private void ch_w_a_s_CheckedChanged(object sender, EventArgs e)
        {
            if(ch_w_a_s.Checked)
            {
                Globals.proxy_serv = true;
            }
            else
            {
                Globals.proxy_serv = false;
            }
        }

        //private void panel_select_Paint(object sender, PaintEventArgs e)
        //{
            //checkBox_advanced.Checked = true;
            //ch_w_a_s.Checked = true;
            //checkBox_Unknown_Blowfish.Checked = true;
            //textBox_game_ip.Text = "64.25.37.135";
            //textBox_game_port.Text = "7777";
            //textBox_game_listenport.Text = "1999";
        //}

	}//end of class
}
