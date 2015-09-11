using System;
using System.Collections.Generic;
using System.Text;

namespace L2_login
{
    public partial class L2NET
    {
        public void UpdateUI()
        {
            menuItem_File.Text = Globals.m_ResourceManager.GetString("menuItem_File");
            menuItem_cmd_logon.Text = Globals.m_ResourceManager.GetString("menuItem_cmd_logon");
            menuItem_cmd_game.Text = Globals.m_ResourceManager.GetString("menuItem_cmd_game");
            menuItem_cmd_restart.Text = Globals.m_ResourceManager.GetString("menuItem_cmd_restart");
            menuItem_cmd_logout.Text = Globals.m_ResourceManager.GetString("menuItem_cmd_logout");
            menuItem_closeclient.Text = Globals.m_ResourceManager.GetString("menuItem_closeclient");
            menuItem_exit.Text = Globals.m_ResourceManager.GetString("menuItem_exit");
            menuItem_Commands.Text = Globals.m_ResourceManager.GetString("menuItem_Commands");
            menuItem_cmd_cancel.Text = Globals.m_ResourceManager.GetString("menuItem_cmd_cancel");
            menuItem_toggle_botting.Text = Globals.m_ResourceManager.GetString("menuItem_toggle_botting");
            menuItem_Options.Text = Globals.m_ResourceManager.GetString("menuItem_bot_options");
            menuItem_saveinterface.Text = Globals.m_ResourceManager.GetString("menuItem_saveinterface");
            menuItem_Help.Text = Globals.m_ResourceManager.GetString("menuItem_Help");
            menuItem_about.Text = Globals.m_ResourceManager.GetString("menuItem_about");
            menuItem_hosts.Text = Globals.m_ResourceManager.GetString("menuItem_hosts");
            menuItem_language.Text = Globals.m_ResourceManager.GetString("menuItem_language");
            button_sendtext.Text = Globals.m_ResourceManager.GetString("button_sendtext");
            button_dead_town.Text = Globals.m_ResourceManager.GetString("button_dead_town");
            button_dead_castle.Text = Globals.m_ResourceManager.GetString("button_dead_castle");
            button_dead_clanhall.Text = Globals.m_ResourceManager.GetString("button_dead_clanhall");
            button_dead_siege.Text = Globals.m_ResourceManager.GetString("button_dead_siege");
            button_yesno_yes.Text = Globals.m_ResourceManager.GetString("button_yesno_yes");
            button_yesno_no.Text = Globals.m_ResourceManager.GetString("button_yesno_no");
            label_youdied.Text = Globals.m_ResourceManager.GetString("label_youdied");
            button_npc_close.Text = Globals.m_ResourceManager.GetString("button_npc_close");

            checkBox_minimap.Text = Globals.m_ResourceManager.GetString("checkBox_minimap");
            checkBox_op_control.Text = Globals.m_ResourceManager.GetString("checkBox_op_control");
            checkBox_op_shift.Text = Globals.m_ResourceManager.GetString("checkBox_op_shift");

            menuItem_scripting.Text = Globals.m_ResourceManager.GetString("menuItem_scripting");
            menuItem_loadscript.Text = Globals.m_ResourceManager.GetString("menuItem_loadscript");
            menuItem_startscript.Text = Globals.m_ResourceManager.GetString("menuItem_startscript");
            startScriptToolStripMenuItem.Text = Globals.m_ResourceManager.GetString("menuItem_startscript");
            menuItem_scriptwindow.Text = Globals.m_ResourceManager.GetString("menuItem_scriptwindow");
            menuItem_scriptdebugger.Text = Globals.m_ResourceManager.GetString("menuItem_scriptdebugger");
            menuItem_debug_mode.Text = Globals.m_ResourceManager.GetString("menuItem_debug_mode");

            menuItem_dump_mode.Text = Globals.m_ResourceManager.GetString("menuItem_dump_mode");
            menuItem_launchl2.Text = Globals.m_ResourceManager.GetString("menuItem_launchl2");
            menuItem_killthreads.Text = Globals.m_ResourceManager.GetString("menuItem_killthreads");
            menuItem_options_setup.Text = Globals.m_ResourceManager.GetString("menuItem_options_setup");
            menuItem_forcecollect.Text = Globals.m_ResourceManager.GetString("menuItem_forcecollect");
            menuItem_help_donate.Text = Globals.m_ResourceManager.GetString("menuItem_help_donate");

            //tab pages
            tabControl_char.TabPages[0].Text = Globals.m_ResourceManager.GetString("tab_Char");
            tabControl_char.TabPages[1].Text = Globals.m_ResourceManager.GetString("tab_Inv");
            tabControl_char.TabPages[2].Text = Globals.m_ResourceManager.GetString("tab_Skills");
            tabControl_char.TabPages[3].Text = Globals.m_ResourceManager.GetString("tab_Clan");
            tabControl_char.TabPages[4].Text = Globals.m_ResourceManager.GetString("char_detail");//char detail
            tabControl_char.TabPages[5].Text = Globals.m_ResourceManager.GetString("tab_Players");
            tabControl_char.TabPages[6].Text = Globals.m_ResourceManager.GetString("tab_Items");
            tabControl_char.TabPages[7].Text = Globals.m_ResourceManager.GetString("tab_NPC");
            tabControl_char.TabPages[8].Text = Globals.m_ResourceManager.GetString("NPC_chat");
            tabControl_char.TabPages[9].Text = Globals.m_ResourceManager.GetString("buffs");

            listView_char_data.Columns[0].Text = Globals.m_ResourceManager.GetString("col_Trait");
            listView_char_data.Columns[1].Text = Globals.m_ResourceManager.GetString("col_Value");

            listView_players_data.Columns[0].Text = Globals.m_ResourceManager.GetString("col_War");
            listView_players_data.Columns[1].Text = Globals.m_ResourceManager.GetString("label_followname");
            listView_players_data.Columns[2].Text = Globals.m_ResourceManager.GetString("col_Class");
            listView_players_data.Columns[3].Text = Globals.m_ResourceManager.GetString("col_Clan");
            listView_players_data.Columns[4].Text = Globals.m_ResourceManager.GetString("col_Ally");
            listView_players_data.Columns[5].Text = Globals.m_ResourceManager.GetString("col_ObjID");

            listView_items_data.Columns[0].Text = Globals.m_ResourceManager.GetString("col_Item");
            listView_items_data.Columns[1].Text = Globals.m_ResourceManager.GetString("col_Count");
            listView_items_data.Columns[2].Text = Globals.m_ResourceManager.GetString("col_ObjID");

            listView_npc_data.Columns[0].Text = Globals.m_ResourceManager.GetString("col_NPC");
            listView_npc_data.Columns[1].Text = Globals.m_ResourceManager.GetString("col_Title");
            listView_npc_data.Columns[2].Text = Globals.m_ResourceManager.GetString("col_ObjID");

            listView_inventory.Columns[0].Text = Globals.m_ResourceManager.GetString("col_Item");
            listView_inventory.Columns[1].Text = Globals.m_ResourceManager.GetString("col_Count");
            listView_inventory.Columns[2].Text = Globals.m_ResourceManager.GetString("col_Equipped");
            listView_inventory.Columns[3].Text = Globals.m_ResourceManager.GetString("col_Slot");
            listView_inventory.Columns[4].Text = Globals.m_ResourceManager.GetString("col_ObjID");

            listView_skills.Columns[0].Text = Globals.m_ResourceManager.GetString("Skill");
            listView_skills.Columns[1].Text = Globals.m_ResourceManager.GetString("col_Level");
            listView_skills.Columns[2].Text = Globals.m_ResourceManager.GetString("col_ObjID");
            radiobutton_active.Text = Globals.m_ResourceManager.GetString("radio_active");
            radiobutton_passive.Text = Globals.m_ResourceManager.GetString("radio_passive");

            listView_char_clan.Columns[0].Text = Globals.m_ResourceManager.GetString("label_followname");
            listView_char_clan.Columns[1].Text = Globals.m_ResourceManager.GetString("col_Level");
            listView_char_clan.Columns[2].Text = Globals.m_ResourceManager.GetString("col_Class");
            listView_char_clan.Columns[3].Text = Globals.m_ResourceManager.GetString("col_Online");

            listView_mybuffs_data.Columns[0].Text = Globals.m_ResourceManager.GetString("label_followname");
            listView_mybuffs_data.Columns[1].Text = Globals.m_ResourceManager.GetString("col_Level");
            listView_mybuffs_data.Columns[2].Text = Globals.m_ResourceManager.GetString("remaining");
            listView_mybuffs_data.Columns[3].Text = Globals.m_ResourceManager.GetString("col_ObjID");

            menuItem_cmd_overlay.Text = Globals.m_ResourceManager.GetString("overlay_window");
            menuItem_cmd_shortcut.Text = Globals.m_ResourceManager.GetString("shortcut_window");

            label_zrange_map.Text = Globals.m_ResourceManager.GetString("zrange_map");
            this.forceLogToolStripMenuItem.Text = Globals.m_ResourceManager.GetString("forcelog");
            this.forceLogToolStripMenuItem1.Text = Globals.m_ResourceManager.GetString("forcelog");
            button_dead_fort.Text = Globals.m_ResourceManager.GetString("button_dead_fort");

            menuItem_forums.Text = Globals.m_ResourceManager.GetString("Forums");
            eULAToolStripMenuItem.Text = Globals.m_ResourceManager.GetString("EULA");
            menuItem_dump_mode_server.Text = Globals.m_ResourceManager.GetString("server_dump_mode");
            blacklistTargetToolStripMenuItem.Text = Globals.m_ResourceManager.GetString("blacklist_target");
            menuItem_actions.Text = Globals.m_ResourceManager.GetString("actions");

            this.Refresh();
        }

        private void menuItem_saveinterface_Click(object sender, System.EventArgs e)
        {
#if !DEBUG
            try
            {
#endif
                System.IO.StreamWriter interout = new System.IO.StreamWriter("interface.txt");

                interout.WriteLine(Globals.LanuageSet.ToString());

                //listView_char_data - report
                foreach (System.Windows.Forms.ColumnHeader clm in listView_char_data.Columns)
                {
                    interout.WriteLine(clm.Width.ToString());
                }

                //listView_players_data - report
                foreach (System.Windows.Forms.ColumnHeader clm in listView_players_data.Columns)
                {
                    interout.WriteLine(clm.Width.ToString());
                }

                //listView_items_data - report
                foreach (System.Windows.Forms.ColumnHeader clm in listView_items_data.Columns)
                {
                    interout.WriteLine(clm.Width.ToString());
                }

                //listView_npc_data - report
                foreach (System.Windows.Forms.ColumnHeader clm in listView_npc_data.Columns)
                {
                    interout.WriteLine(clm.Width.ToString());
                }

                //listView_inventory - report
                foreach (System.Windows.Forms.ColumnHeader clm in listView_inventory.Columns)
                {
                    interout.WriteLine(clm.Width.ToString());
                }

                //listView_skills - report
                foreach (System.Windows.Forms.ColumnHeader clm in listView_skills.Columns)
                {
                    interout.WriteLine(clm.Width.ToString());
                }

                //listView_char_clan - report
                foreach (System.Windows.Forms.ColumnHeader clm in listView_char_clan.Columns)
                {
                    interout.WriteLine(clm.Width.ToString());
                }

                //listView_mybuffs_data - report
                foreach (System.Windows.Forms.ColumnHeader clm in listView_mybuffs_data.Columns)
                {
                    interout.WriteLine(clm.Width.ToString());
                }

                interout.WriteLine(Globals.Voice.ToString());
                if (Globals.MinimizeToTray)
                    interout.WriteLine("1");
                else
                    interout.WriteLine("0");

                interout.WriteLine(this.Height.ToString());
                interout.WriteLine(this.Width.ToString());

                if (checkBox_minimap.Checked)
                    interout.WriteLine("1");
                else
                    interout.WriteLine("0");
                if (Globals.ShowNamesNpcs)
                    interout.WriteLine("1");
                else
                    interout.WriteLine("0");
                if (Globals.DownloadNewCrests)
                    interout.WriteLine("1");
                else
                    interout.WriteLine("0");
                if (Globals.SocialSelf)
                    interout.WriteLine("1");
                else
                    interout.WriteLine("0");
                if (Globals.SocialPcs)
                    interout.WriteLine("1");
                else
                    interout.WriteLine("0");
                if (Globals.SocialNpcs)
                    interout.WriteLine("1");
                else
                    interout.WriteLine("0");
                interout.WriteLine(Globals.ProductKey);
                interout.WriteLine(Globals.L2Path);
                if(Globals.AllowFiles)
                    interout.WriteLine("1");
                else
                    interout.WriteLine("0");
                interout.WriteLine(Globals.DirectInputKey);
                interout.WriteLine(Globals.DirectInputKey2); //Oddi, Force quit key
                if (Globals.Use_Direct_Sound)
                    interout.WriteLine("1");
                else
                    interout.WriteLine("0");
                interout.WriteLine(Globals.Texture_Mode.ToString());
                if (Globals.ShowNamesPcs)
                    interout.WriteLine("1");
                else
                    interout.WriteLine("0");
                if (Globals.ShowNamesItems)
                    interout.WriteLine("1");
                else
                    interout.WriteLine("0");
                if (Globals.IgnoreExitConf) //Oddi, Ignore Exit Confirmation
                    interout.WriteLine("1");
                else
                    interout.WriteLine("0");
                if (Globals.White_Names)
                    interout.WriteLine("1");
                else
                    interout.WriteLine("0");
                if (Globals.Suppress_Quakes)
                    interout.WriteLine("1");
                else
                    interout.WriteLine("0");
                if (Globals.Send_Blank_GG)
                    interout.WriteLine("1");
                else
                    interout.WriteLine("0");
                if (Globals.Hide_Message_Boxes)
                    interout.WriteLine("1");
                else
                    interout.WriteLine("0");
                interout.WriteLine(Globals.ViewRange.ToString());

                interout.WriteLine(textBox_zrange_map.Text); //Map ZRange
                interout.WriteLine(trackBar_map_zoom.Value.ToString()); //Map Zoom

                //v387... script compatibility with v386 code
                if (Globals.ScriptCompatibilityv386)
                    interout.WriteLine("1");
                else
                    interout.WriteLine("0");

                //v388
                if (Globals.Use_Hardware_Acceleration)
                    interout.WriteLine("1");
                else
                    interout.WriteLine("0");

                //v389
                if (Globals.dc_on_ig_close)
                    interout.WriteLine("1");
                else
                    interout.WriteLine("0");

                if (Globals.dump_pbuff_on_ig_close)
                    interout.WriteLine("1");
                else
                    interout.WriteLine("0");

                if (Globals.ToggleBottingifGMAction)
                    interout.WriteLine("1");
                else
                    interout.WriteLine("0");

                if (Globals.ToggleBottingifTeleported)
                    interout.WriteLine("1");
                else
                    interout.WriteLine("0");

                //v391
                if(Globals.Popup_Captcha)
                    interout.WriteLine("1");
                else
                    interout.WriteLine("0");

                interout.WriteLine(Globals.Captcha_HTML1);
                interout.WriteLine(Globals.Captcha_HTML2);

                //v396
                if (Globals.AutolearnSkills)
                    interout.WriteLine("1");
                else
                    interout.WriteLine("0");

                if (Globals.LogWriting)
                    interout.WriteLine("1");
                else
                    interout.WriteLine("0");

                if (Globals.NpcSay)
                    interout.WriteLine("1");
                else
                    interout.WriteLine("0");

                interout.Close();
#if !DEBUG
            }
            catch
            {
                Globals.l2net_home.Add_Text("Error while saving interface");
            }
#endif
        }

        private void Load_Interface()
        {
            System.IO.StreamReader interin;
            try
            {
                interin = new System.IO.StreamReader("config\\interface.txt");
            } catch (Exception e)
            {
                throw new Exception("Failed to open file: " + e.Message);
            }
            try {
                Globals.LanuageSet = Util.GetInt32(interin.ReadLine());

                switch (Globals.LanuageSet)
                {
                    case 0:
                        Globals.m_ResourceManager = new System.Resources.ResourceManager("L2_login.Languages.English", System.Reflection.Assembly.GetExecutingAssembly());
                        break;
                    case 1:
                        Globals.m_ResourceManager = new System.Resources.ResourceManager("L2_login.Languages.PortugueseBR", System.Reflection.Assembly.GetExecutingAssembly());
                        break;
                    case 2:
                        Globals.m_ResourceManager = new System.Resources.ResourceManager("L2_login.Languages.French", System.Reflection.Assembly.GetExecutingAssembly());
                        break;
                    case 3:
                        Globals.m_ResourceManager = new System.Resources.ResourceManager("L2_login.Languages.Spanish", System.Reflection.Assembly.GetExecutingAssembly());
                        break;
                    case 4:
                        Globals.m_ResourceManager = new System.Resources.ResourceManager("L2_login.Languages.German", System.Reflection.Assembly.GetExecutingAssembly());
                        break;
                    case 5:
                        Globals.m_ResourceManager = new System.Resources.ResourceManager("L2_login.Languages.Polish", System.Reflection.Assembly.GetExecutingAssembly());
                        break;
                    case 6:
                        Globals.m_ResourceManager = new System.Resources.ResourceManager("L2_login.Languages.Russian", System.Reflection.Assembly.GetExecutingAssembly());
                        break;
                    case 7:
                        Globals.m_ResourceManager = new System.Resources.ResourceManager("L2_login.Languages.Japanese", System.Reflection.Assembly.GetExecutingAssembly());
                        break;
                    case 8:
                        Globals.m_ResourceManager = new System.Resources.ResourceManager("L2_login.Languages.Chinese", System.Reflection.Assembly.GetExecutingAssembly());
                        break;
                    case 9:
                        Globals.m_ResourceManager = new System.Resources.ResourceManager("L2_login.Languages.Greek", System.Reflection.Assembly.GetExecutingAssembly());
                        break;
                    case 10:
                        Globals.m_ResourceManager = new System.Resources.ResourceManager("L2_login.Languages.Korean", System.Reflection.Assembly.GetExecutingAssembly());
                        break;
                    case 11:
                        Globals.m_ResourceManager = new System.Resources.ResourceManager("L2_login.Languages.Italian", System.Reflection.Assembly.GetExecutingAssembly());
                        break;
                    case 12:
                        Globals.m_ResourceManager = new System.Resources.ResourceManager("L2_login.Languages.Lithuanian", System.Reflection.Assembly.GetExecutingAssembly());
                        break;
                    case 13:
                        Globals.m_ResourceManager = new System.Resources.ResourceManager("L2_login.Languages.Dutch", System.Reflection.Assembly.GetExecutingAssembly());
                        break;
                    case 14:
                        Globals.m_ResourceManager = new System.Resources.ResourceManager("L2_login.Languages.Romanian", System.Reflection.Assembly.GetExecutingAssembly());
                        break;
                    case 15:
                        Globals.m_ResourceManager = new System.Resources.ResourceManager("L2_login.Languages.Leet", System.Reflection.Assembly.GetExecutingAssembly());
                        break;
                    case 16:
                        Globals.m_ResourceManager = new System.Resources.ResourceManager("L2_login.Languages.Hungarian", System.Reflection.Assembly.GetExecutingAssembly());
                        break;
                    case 17:
                        Globals.m_ResourceManager = new System.Resources.ResourceManager("L2_login.Languages.Marklar", System.Reflection.Assembly.GetExecutingAssembly());
                        break;
                    case 18:
                        Globals.m_ResourceManager = new System.Resources.ResourceManager("L2_login.Languages.Norwegian", System.Reflection.Assembly.GetExecutingAssembly());
                        break;
                    case 19:
                        Globals.m_ResourceManager = new System.Resources.ResourceManager("L2_login.Languages.Czech", System.Reflection.Assembly.GetExecutingAssembly());
                        break;
                    case 20:
                        Globals.m_ResourceManager = new System.Resources.ResourceManager("L2_login.Languages.Slovenian", System.Reflection.Assembly.GetExecutingAssembly());
                        break;
                    case 21:
                        Globals.m_ResourceManager = new System.Resources.ResourceManager("L2_login.Languages.Swedish", System.Reflection.Assembly.GetExecutingAssembly());
                        break;
                    case 22:
                        Globals.m_ResourceManager = new System.Resources.ResourceManager("L2_login.Languages.Danish", System.Reflection.Assembly.GetExecutingAssembly());
                        break;
                    case 23:
                        Globals.m_ResourceManager = new System.Resources.ResourceManager("L2_login.Languages.PortuguesePT", System.Reflection.Assembly.GetExecutingAssembly());
                        break;
                    case 24:
                        Globals.m_ResourceManager = new System.Resources.ResourceManager("L2_login.Languages.Finnish", System.Reflection.Assembly.GetExecutingAssembly());
                        break;
                    case 25:
                        Globals.m_ResourceManager = new System.Resources.ResourceManager("L2_login.Languages.Vietnamese", System.Reflection.Assembly.GetExecutingAssembly());
                        break;
                    case 26:
                        Globals.m_ResourceManager = new System.Resources.ResourceManager("L2_login.Languages.Thai", System.Reflection.Assembly.GetExecutingAssembly());
                        break;
                }

                //listView_char_data - report
                foreach (System.Windows.Forms.ColumnHeader clm in listView_char_data.Columns)
                {
                    clm.Width = Util.GetInt32(interin.ReadLine());
                }

                //listView_players_data - report
                foreach (System.Windows.Forms.ColumnHeader clm in listView_players_data.Columns)
                {
                    clm.Width = Util.GetInt32(interin.ReadLine());
                }

                //listView_items_data - report
                foreach (System.Windows.Forms.ColumnHeader clm in listView_items_data.Columns)
                {
                    clm.Width = Util.GetInt32(interin.ReadLine());
                }

                //listView_npc_data - report
                foreach (System.Windows.Forms.ColumnHeader clm in listView_npc_data.Columns)
                {
                    clm.Width = Util.GetInt32(interin.ReadLine());
                }

                //listView_inventory - report
                foreach (System.Windows.Forms.ColumnHeader clm in listView_inventory.Columns)
                {
                    clm.Width = Util.GetInt32(interin.ReadLine());
                }

                //listView_skills - report
                foreach (System.Windows.Forms.ColumnHeader clm in listView_skills.Columns)
                {
                    clm.Width = Util.GetInt32(interin.ReadLine());
                }

                //listView_char_clan - report
                foreach (System.Windows.Forms.ColumnHeader clm in listView_char_clan.Columns)
                {
                    clm.Width = Util.GetInt32(interin.ReadLine());
                }

                //listView_mybuffs_data - report
                foreach (System.Windows.Forms.ColumnHeader clm in listView_mybuffs_data.Columns)
                {
                    clm.Width = Util.GetInt32(interin.ReadLine());
                }

                Globals.Voice = Util.GetInt32(interin.ReadLine());
                if (Util.GetInt32(interin.ReadLine()) == 1)
                    Globals.MinimizeToTray = true;
                else
                    Globals.MinimizeToTray = false;

                this.Height = Util.GetInt32(interin.ReadLine());
                this.Width = Util.GetInt32(interin.ReadLine());

                if (Util.GetInt32(interin.ReadLine()) == 1)
                    checkBox_minimap.Checked = true;
                else
                    checkBox_minimap.Checked = false;
                if (Util.GetInt32(interin.ReadLine()) == 1)
                    Globals.ShowNamesNpcs = true;
                else
                    Globals.ShowNamesNpcs = false;
                if (Util.GetInt32(interin.ReadLine()) == 1)
                    Globals.DownloadNewCrests = true;
                else
                    Globals.DownloadNewCrests = false;
                if (Util.GetInt32(interin.ReadLine()) == 1)
                    Globals.SocialSelf = true;
                else
                    Globals.SocialSelf = false;
                if (Util.GetInt32(interin.ReadLine()) == 1)
                    Globals.SocialPcs = true;
                else
                    Globals.SocialPcs = false;
                if (Util.GetInt32(interin.ReadLine()) == 1)
                    Globals.SocialNpcs = true;
                else
                    Globals.SocialNpcs = false;
                Globals.ProductKey = interin.ReadLine();
                Globals.L2Path = interin.ReadLine();
                if (Util.GetInt32(interin.ReadLine()) == 1)
                    Globals.AllowFiles = true;
                else
                    Globals.AllowFiles = false;
                Globals.DirectInputKey = interin.ReadLine();
                Globals.DirectInputKey2 = interin.ReadLine(); //Oddi, Force quit key

                if (Util.GetInt32(interin.ReadLine()) == 1)
                    Globals.Use_Direct_Sound = true;
                else
                    Globals.Use_Direct_Sound = false;
                Globals.Texture_Mode = Util.GetInt32(interin.ReadLine());
                if (Util.GetInt32(interin.ReadLine()) == 1)
                    Globals.ShowNamesPcs = true;
                else
                    Globals.ShowNamesPcs = false;
                if (Util.GetInt32(interin.ReadLine()) == 1)
                    Globals.ShowNamesItems = true;
                else
                    Globals.ShowNamesItems = false;
                if (Util.GetInt32(interin.ReadLine()) == 1) // Change by Oddi, Ignore Exit Confirmation
                    Globals.IgnoreExitConf = true;
                else
                    Globals.IgnoreExitConf = false;
                if (Util.GetInt32(interin.ReadLine()) == 1)
                    Globals.White_Names = true;
                else
                    Globals.White_Names = false;
                if (Util.GetInt32(interin.ReadLine()) == 1)
                    Globals.Suppress_Quakes = true;
                else
                    Globals.Suppress_Quakes = false;
                try
                {
                    if (Util.GetInt32(interin.ReadLine()) == 1)
                        Globals.Send_Blank_GG = true;
                    else
                        Globals.Send_Blank_GG = false;
                    if (Util.GetInt32(interin.ReadLine()) == 1)
                        Globals.Hide_Message_Boxes = true;
                    else
                        Globals.Hide_Message_Boxes = false;
                }
                catch
                {
                    //old ini file
                    Globals.Send_Blank_GG = false;
                    Globals.Hide_Message_Boxes = false;
                }
                try
                {
                    Globals.ViewRange = Util.GetInt32(interin.ReadLine());
                }
                catch
                {
                }

                try
                {
                    textBox_zrange_map.Text = interin.ReadLine(); //Map ZRange
                    trackBar_map_zoom.Value = Util.GetInt32(interin.ReadLine()); //Map Zoom Value
                }
                catch
                {
                    //Default settings
                    textBox_zrange_map.Text = "500"; 
                    trackBar_map_zoom.Value = 2;
                }

                //v387
                try
                {
                    if (Util.GetInt32(interin.ReadLine()) == 1)
                        Globals.ScriptCompatibilityv386 = true;
                    else
                        Globals.ScriptCompatibilityv386 = false;
                }
                catch
                {
                }

                //v388
                try
                {
                    if (Util.GetInt32(interin.ReadLine()) == 1)
                        Globals.Use_Hardware_Acceleration = true;
                    else
                        Globals.Use_Hardware_Acceleration = false;
                }
                catch
                {
                }

                //v389
                try
                {
                    if (Util.GetInt32(interin.ReadLine()) == 1)
                        Globals.dc_on_ig_close = true;
                    else
                        Globals.dc_on_ig_close = false;
                }
                catch
                {
                }

                try
                {
                    if (Util.GetInt32(interin.ReadLine()) == 1)
                        Globals.dump_pbuff_on_ig_close = true;
                    else
                        Globals.dump_pbuff_on_ig_close = false;
                }
                catch
                {
                }

                try
                {
                    if (Util.GetInt32(interin.ReadLine()) == 1)
                        Globals.ToggleBottingifGMAction = true;
                    else
                        Globals.ToggleBottingifGMAction = false;
                }
                catch
                {
                }

                try
                {
                    if (Util.GetInt32(interin.ReadLine()) == 1)
                        Globals.ToggleBottingifTeleported = true;
                    else
                        Globals.ToggleBottingifTeleported = false;
                }
                catch
                {
                }

                //v391
                try
                {
                    if (Util.GetInt32(interin.ReadLine()) == 1)
                    {
                        Globals.Popup_Captcha = true;
                    }
                    else
                        Globals.Popup_Captcha = false;

                    Globals.Captcha_HTML1 = interin.ReadLine().ToString();
                    Globals.Captcha_HTML2 = interin.ReadLine().ToString();
                }
                catch
                {
                }

                //v396
                try
                {
                    if (Util.GetInt32(interin.ReadLine()) == 1)
                        Globals.AutolearnSkills = true;
                    else
                        Globals.AutolearnSkills = false;

                }
                catch
                {
                }
                try
                {
                    if (Util.GetInt32(interin.ReadLine()) == 1)
                        Globals.LogWriting = true;
                    else
                        Globals.LogWriting = false;


                    if (Util.GetInt32(interin.ReadLine()) == 1)
                        Globals.NpcSay = true;
                    else
                        Globals.NpcSay = false;


                }
                catch
                {
                }


                interin.Close();

                this.UpdateUI();
                if (Globals.login_window != null)
                    Globals.login_window.UpdateUI();
                if (Globals.botoptionsscreen != null)
                    Globals.botoptionsscreen.UpdateUI();
                if (Globals.setupwindow != null)
                    Globals.setupwindow.UpdateUI();
                if (Globals.scriptdebugwindow != null)
                    Globals.scriptdebugwindow.UpdateUI();

                Globals.LoadedInterface = true;

                Add_Text("loaded interface", Globals.Red, TextType.BOT);
			}
			catch
			{
                Add_PopUpError("failed to load interface file, continuing with default parameters for unloaded values");
			}
        }
    }
}
