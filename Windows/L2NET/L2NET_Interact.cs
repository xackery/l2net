using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace L2_login
{
    public partial class L2NET
    {
        private void forceLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Util.Stop_Connections();
        }

        public void menuItem_killthreads_Click(object sender, EventArgs e)
        {
            KillEverything();
        }

        delegate void KillEverything_Callback();
        public void KillEverything()
        {
            if (this.label_target_name.InvokeRequired)
            {
                KillEverything_Callback d = new KillEverything_Callback(KillEverything);
                label_target_name.Invoke(d);
                return;
            }

            //try to kill everything...
            Globals.gamedata.running = false;

            Util.Stop_Connections();

            Util.KillThreads();

            System.Threading.Thread.Sleep(Globals.SLEEP_KillReset);

            Util.Setup_Threads();

            if (Globals.gamedata.login_failed)
            {
                Globals.gamedata.login_failed = false;
                menuItem_cmd_logon_Click(null, null);

            }
        }

        private void button_sendtext_Click(object sender, System.EventArgs e)
        {
#if !DEBUG
            if (!Globals.gamedata.logged_in)
                return;
#endif
            string total_text = textBox_say.Text.Replace("\\n", Environment.NewLine);
            int index = comboBox_msg_type.SelectedIndex;

            textBox_say.Text = "";

            ServerPackets.Send_Text(index, total_text);
        } 

        private void button_yesno_yes_Click(object sender, System.EventArgs e)
        {
            panel_yesno.SendToBack();

            Process_YesNo(true);
        }

        private void button_yesno_no_Click(object sender, System.EventArgs e)
        {
            panel_yesno.SendToBack();

            Process_YesNo(false);
        }

        private void Process_YesNo(bool yes)
        {
            label_yesno.Text = "question";

            switch (Globals.gamedata.yesno_state)
            {
                case 1://join party
                    ServerPackets.JoinPartyReply(yes);
                    break;
                case 2://join clan
                    ServerPackets.JoinClanReply(yes);
                    break;
                case 3://join friend
                    ServerPackets.JoinFriendReply(yes);
                    break;
                case 4://join ally
                    ServerPackets.JoinAllyReply(yes);
                    break;
                case 5://rezz
                    ServerPackets.DialogReply(yes);
                    break;
                case 6://trade
                    ServerPackets.TradeReply(yes);
                    break;
            }
        }
        
        private void panel_charinfo_DoubleClick(object sender, EventArgs e)
        {
            if (Globals.gamedata.logged_in)
            {
                //action to this item
                ServerPackets.Target(Globals.gamedata.my_char.ID, Util.Float_Int32(Globals.gamedata.my_char.X), Util.Float_Int32(Globals.gamedata.my_char.Y), Util.Float_Int32(Globals.gamedata.my_char.Z), Globals.gamedata.Shift);
            }
        }

        private void button_dead_town_Click(object sender, System.EventArgs e)
        {
            //return to town
            ServerPackets.Return(0);
            panel_dead.Hide();
        }

        private void button_dead_clanhall_Click(object sender, System.EventArgs e)
        {
            //return to clanhall
            ServerPackets.Return(1);
            panel_dead.Hide();
        }

        private void button_dead_castle_Click(object sender, System.EventArgs e)
        {
            //return to castle
            ServerPackets.Return(2);
            panel_dead.Hide();
        }

        private void button_dead_siege_Click(object sender, System.EventArgs e)
        {
            //return to siege hq
            ServerPackets.Return(3);
            panel_dead.Hide();
        }

        private void button_dead_fort_Click(object sender, EventArgs e)
        {
            //return to fortress
            ServerPackets.Return(4);
            panel_dead.Hide();
        }

        private void button_npc_close_Click(object sender, System.EventArgs e)
        {
            panel_npc_chat.Hide();
        }

        private void menuItem_hosts_Click(object sender, System.EventArgs e)
        {
            string st = Environment.GetEnvironmentVariable("SystemRoot") + "\\system32\\drivers\\etc\\hosts";

            System.Diagnostics.Process notepad = new System.Diagnostics.Process();
            notepad.StartInfo.FileName = "notepad.exe";
            notepad.StartInfo.Arguments = st;
            notepad.Start();
        }

        private void dropStackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // https://opensvn.csie.org/traccgi/l2jc4/browser/trunk/L2_Gameserver/java/net/sf/l2j/gameserver/clientpackets/RequestDropItem.java
            //drop an item from inventory
            //probably safest to drop it on our own location
            //although perhaps we can drop things in the sky kekeke
            try
            {
                uint objID = Util.GetUInt32(listView_inventory.Items[listView_inventory.SelectedIndices[0]].SubItems[4].Text);
                uint count = Util.GetUInt32(listView_inventory.Items[listView_inventory.SelectedIndices[0]].SubItems[1].Text);
                int x = Util.Float_Int32(Globals.gamedata.my_char.X);
                int y = Util.Float_Int32(Globals.gamedata.my_char.Y);
                int z = Util.Float_Int32(Globals.gamedata.my_char.Z);

                ServerPackets.DropItem(objID, count, x, y, z);
            }
            catch
            {
            }
        }

        private void deleteStackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // https://opensvn.csie.org/traccgi/l2jc4/browser/trunk/L2_Gameserver/java/net/sf/l2j/gameserver/clientpackets/RequestDestroyItem.java
            //delete crap from inventory
            try
            {
                uint objID = Util.GetUInt32(listView_inventory.Items[listView_inventory.SelectedIndices[0]].SubItems[4].Text);
                uint count = Util.GetUInt32(listView_inventory.Items[listView_inventory.SelectedIndices[0]].SubItems[1].Text);

                ServerPackets.DeleteItem(objID, count);
            }
            catch
            {
            }
        }

        private void crystalizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // https://opensvn.csie.org/traccgi/l2jc4/browser/trunk/L2_Gameserver/java/net/sf/l2j/gameserver/clientpackets/RequestCrystallizeItem.java
            //crsytalize crap in our inventory
            try
            {
                uint objID = Util.GetUInt32(listView_inventory.Items[listView_inventory.SelectedIndices[0]].SubItems[4].Text);
                uint count = 1;//Util.GetInt32(listView_inventory.Items[listView_inventory.SelectedIndices[0]].SubItems[3].Text);

                ServerPackets.CrystalizeItem(objID, count);
            }
            catch
            {
            }
        }
        private void addToDoNotListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string name = (listView_items_data.Items[listView_items_data.SelectedIndices[0]].SubItems[0].Text);
                uint id = Util.GetItemID(name);

                Globals.DoNotItemLock.EnterWriteLock();
                try
                {
                    BotOptions.DoNotItems.Add(id); //Item ID
                }
                finally
                {
                    Globals.DoNotItemLock.ExitWriteLock();
                }
                Add_Text(name + " added to Do Not List", Globals.Green, TextType.BOT);
            }
            catch
            {
                //Do nothing
            }

        }

        private void addTodoNotListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                string name = (listView_inventory.Items[listView_inventory.SelectedIndices[0]].SubItems[0].Text);
                uint id = Util.GetItemID(name);

                Globals.DoNotItemLock.EnterWriteLock();
                try
                {
                    if (!BotOptions.DoNotItems.Contains(id))
                    {
                        BotOptions.DoNotItems.Add(id); //Item ID
                        Add_Text(name + " added to Do Not List", Globals.Green, TextType.BOT);
                    }
                    else
                    {
                        Add_Text(name + " is already in Do Not List", Globals.Green, TextType.BOT);
                    }
                }
                finally
                {
                    Globals.DoNotItemLock.ExitWriteLock();
                }
                
            }
            catch
            {
                //Do nothing
            }

        }

        private void addToDoNotListNPCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string name = (listView_npc_data.Items[listView_npc_data.SelectedIndices[0]].SubItems[0].Text);
                //uint id = Util.GetNPCID(name);
                uint id = Convert.ToUInt32((listView_npc_data.Items[listView_npc_data.SelectedIndices[0]].SubItems[3].Text));

                if (id < Globals.NPC_OFF)
                {
                    id += Globals.NPC_OFF;
                }

                Globals.DoNotNPCLock.EnterWriteLock();
                try
                {
                    if (!BotOptions.DoNotNPCs.Contains(id))
                    {
                        BotOptions.DoNotNPCs.Add(id); //NPC ID
                        Add_Text(name + " added to Do Not List", Globals.Green, TextType.BOT);
                    }
                    else
                    {
                        Add_Text(id.ToString() + " is already in Do Not List",Globals.Green, TextType.BOT);
                    }
                }
                finally
                {
                    Globals.DoNotNPCLock.ExitWriteLock();
                }
                
            }
            catch
            {
                //Do nothing
            }

        }

        private void addToBlackListNPCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string name = (listView_npc_data.Items[listView_npc_data.SelectedIndices[0]].SubItems[0].Text);
                uint objID = Convert.ToUInt32((listView_npc_data.Items[listView_npc_data.SelectedIndices[0]].SubItems[2].Text));
                Util.IgnoreNPC(objID, true);
                Add_Text("NPC with name: " +name + " and objID: " + objID.ToString() + " added to blacklist", Globals.Green, TextType.BOT);
            }
            catch
            {
                //Do nothing
            }

        }

        private void blacklistTargetToolStripMenuItem_Click(object sender, EventArgs e) //Commands -> Blacklist target
        {
            try
            {
                Util.IgnoreNPC(Globals.gamedata.my_char.TargetID, true);
                Add_Text("NPC with objID: "+ Globals.gamedata.my_char.TargetID.ToString() + " added to blacklist", Globals.Green, TextType.BOT);
            }
            catch
            {
                Add_Text("Failed to blacklist current target", Globals.Green, TextType.BOT);
            }

        }

        private void menuItem_cmd_logon_Click(object sender, System.EventArgs e)
        {
            Globals.gamedata.drawing_game = false;

            if (Globals.login_window == null || Globals.login_window.IsDisposed == true)
            {
                Globals.login_window = new Login(this);
            }
            Globals.login_window.BringToFront();
            Globals.login_window.Show();
        }

        private void menuItem_cmd_game_Click(object sender, System.EventArgs e)
        {
            Globals.gamedata.drawing_game = true;

            if (Globals.map_window == null || Globals.map_window.IsDisposed == true)
            {
                Globals.map_window = new Map(this);
            }
            Globals.map_window.BringToFront();
            Globals.map_window.Show();
        }

        private void menuItem_cmd_logout_Click(object sender, System.EventArgs e)
        {
            if (Globals.gamedata.running && Globals.gamedata.logged_in)
            {
                ServerPackets.Send_Logout();
                Add_Text("Sent Logout packet", Globals.Red, TextType.BOT);
            }
        }

        private void menuItem_cmd_restart_Click(object sender, System.EventArgs e)
        {
            if (Globals.gamedata.running && Globals.gamedata.logged_in)
            {
                ServerPackets.Send_Restart();
                Add_Text("Sent Restart packet", Globals.Red, TextType.BOT);
            }
        }

        private void menuItem_cmd_cancel_Click(object sender, System.EventArgs e)
        {
            if (Globals.gamedata.logged_in)
            {
                ServerPackets.Send_CancelTarget();
                Add_Text("Cancel Target", Globals.Red, TextType.BOT);
            }
        }

        private void menuItem_toggle_botting_Click(object sender, System.EventArgs e)
        {
            Toggle_Botting();
        }

        public void Toggle_Botting()
        {
            Toggle_Botting(0);
        }

        public void Toggle_Botting(int botting)
        {
            if (botting == 0)
            {
                menuItem_toggle_botting.Checked = !menuItem_toggle_botting.Checked;
                toggleBottingToolStripMenuItem.Checked = menuItem_toggle_botting.Checked;

                Globals.gamedata.BOTTING = menuItem_toggle_botting.Checked;
            }
            else
            {
                menuItem_toggle_botting.Checked = false;
                toggleBottingToolStripMenuItem.Checked = false;
                Globals.gamedata.BOTTING = false;
            }

            if (Globals.gamedata.BOTTING)
            {
                Add_Text("Automatic Botting is ON");
                label_char_level.BackColor = System.Drawing.Color.Green;

                if (Globals.overlaywindow != null && Globals.overlaywindow.IsDisposed == false)
                {
                    Globals.overlaywindow.Set_Bot(true);
                }
            }
            else
            {
                Add_Text("Automatic Botting is OFF");
                label_char_level.BackColor = System.Drawing.Color.Red;

                if (Globals.overlaywindow != null && Globals.overlaywindow.IsDisposed == false)
                {
                    Globals.overlaywindow.Set_Bot(false);
                }
            }
        }

        private void menuItem_toggle_autoreply_Click(object sender, EventArgs e)
        {

            Toggle_AutoReply();

        }

        private void Toggle_AutoReply()
        {
            if (!Globals.gamedata.autoreply)
            {
                Globals.gamedata.autoreply = true;
                menuItem_toggle_autoreply.Checked = true;
                Globals.l2net_home.Add_Text("Auto reply local is ON!", Globals.Red, TextType.BOT);
            }
            else
            {
                Globals.gamedata.autoreply = false;
                menuItem_toggle_autoreply.Checked = false;
                Globals.l2net_home.Add_Text("Auto reply local is OFF!", Globals.Red, TextType.BOT);
            }
        }

        private void menuItem_toggle_autoreplyPM_Click(object sender, EventArgs e)
        {
            Toggle_AutoReplyPM();
        }

        private void Toggle_AutoReplyPM()
        {
            if (!Globals.gamedata.autoreplyPM)
            {
                Globals.gamedata.autoreplyPM = true;
                menuItem_toggle_autoreplyPM.Checked = true;
                Globals.l2net_home.Add_Text("Auto reply PM is ON!", Globals.Red, TextType.BOT);
            }
            else
            {
                Globals.gamedata.autoreplyPM = false;
                menuItem_toggle_autoreplyPM.Checked = false;
                Globals.l2net_home.Add_Text("Auto reply PM is OFF!", Globals.Red, TextType.BOT);
            }
        }

        private void menuItem_language_Click(object sender, System.EventArgs e)
        {
            Language lang = new Language();
            lang.parent_form = this;
            lang.ShowDialog();
            lang.Dispose();
        }

        private void menuItem_Options_Click(object sender, EventArgs e)
        {
            if (Globals.botoptionsscreen == null || Globals.botoptionsscreen.IsDisposed == true)
            {
                Globals.botoptionsscreen = new BotOptionsScreen();
            }
            else
            {
                Globals.botoptionsscreen.Setup();
            }
            Globals.botoptionsscreen.TopMost = true;
            Globals.botoptionsscreen.BringToFront();
            Globals.botoptionsscreen.Show();
        }

        private void menuItem_options_setup_Click(object sender, System.EventArgs e)
        {
            //Setup
            if (Globals.setupwindow == null || Globals.setupwindow.IsDisposed == true)
            {
                Globals.setupwindow = new Setup();
            }
            Globals.setupwindow.TopMost = true;
            Globals.setupwindow.BringToFront();
            Globals.setupwindow.Show();
        }

        private void checkBox_op_control_CheckedChanged(object sender, EventArgs e)
        {
            Globals.gamedata.Control = checkBox_op_control.Checked;
        }

        private void checkBox_op_shift_CheckedChanged(object sender, EventArgs e)
        {
            Globals.gamedata.Shift = checkBox_op_shift.Checked;
        }

        private void checkBox_BoundingPoints_CheckedChanged(object sender, EventArgs e)
        {
            Globals.gamedata.AddPolygon = checkBox_BoundingPoints.Checked;
        }

        private void menuItem_cmd_overlay_Click(object sender, System.EventArgs e)
        {
            if (menuItem_cmd_overlay.Checked)
            {
                Globals.overlaywindow.Hide();
                menuItem_cmd_overlay.Checked = false;
            }
            else
            {
                if (Globals.overlaywindow == null || Globals.overlaywindow.IsDisposed == true)
                {
                    Globals.overlaywindow = new Overlay();
                    //overlaywindow.MdiParent = this;
                }
                Globals.overlaywindow.TopMost = true;
                Globals.overlaywindow.BringToFront();
                Globals.overlaywindow.Show();
                menuItem_cmd_overlay.Checked = true;
            }
        }

        private void menuItem_cmd_shortcut_Click(object sender, System.EventArgs e)
        {
            if (menuItem_cmd_shortcut.Checked)
            {
                Globals.shortcutwindow.Hide();
                menuItem_cmd_shortcut.Checked = false;
            }
            else
            {
                if (Globals.shortcutwindow == null || Globals.shortcutwindow.IsDisposed == true)
                {
                    Globals.shortcutwindow = new ShortCutBar();
                }
                Globals.shortcutwindow.TopMost = true;
                Globals.shortcutwindow.BringToFront();
                Globals.shortcutwindow.Show();
                menuItem_cmd_shortcut.Checked = true;
            }
        }

        private void menuItem_help_donate_Click(object sender, System.EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://l2net.insane-gamers.com/donate.php");
            }
            catch
            {
                //problem opening browser?
            }
        }

        private void menuItem_forums_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://insane-gamers.com/forum.php");
            }
            catch
            {
                //problem opening browser?
            }
        }

        private void eULAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("EULA.txt");
            }
            catch
            {
                //problem opening browser?
            }
        }

        private void menuItem_scriptwindow_Click(object sender, System.EventArgs e)
        {
            try
            {
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WorkingDirectory = Globals.PATH + "\\IDE\\";
                startInfo.FileName = "javaw";
                startInfo.Arguments = "-jar \"" + Globals.PATH + "\\IDE\\JingJing_IDE.jar\" -l2net";

                System.Diagnostics.Process.Start(startInfo);
            }
            catch
            {
                Globals.l2net_home.Add_Error("Failed to start script editor");
            }
        }

        private void menuItem_startscript_Click(object sender, System.EventArgs e)
        {
            if (Globals.gamedata.running)
            {
                //need to toggle scripting on and off
                if (Globals.gamedata.CurrentScriptState == ScriptState.Stopped || Globals.gamedata.CurrentScriptState == ScriptState.Finished)
                {
                    try
                    {
                        //add reset here... to start the script over always...
                        Globals.scriptthread.Reset_Script();
                        //start the thread
                        Globals.scriptthread.scriptthread.Start();
                        Globals.gamedata.CurrentScriptState = ScriptState.Running;//running
                        menuItem_startscript.Text = Globals.m_ResourceManager.GetString("menuItem_stopscript");
                        startScriptToolStripMenuItem.Text = Globals.m_ResourceManager.GetString("menuItem_stopscript");
                    }
                    catch
                    {
                        this.Add_Error("Failed to START scripting thread" + Environment.NewLine + "Try to reset the script first?");
                    }
                }
                else
                {
                    try
                    {
                        Globals.scriptthread.scriptthread.Abort();
                        Globals.gamedata.CurrentScriptState = ScriptState.Stopped;//stopped
                        menuItem_startscript.Text = Globals.m_ResourceManager.GetString("menuItem_startscript");
                        startScriptToolStripMenuItem.Text = Globals.m_ResourceManager.GetString("menuItem_startscript");
                    }
                    catch
                    {
                        this.Add_Error("Failed to STOP scripting thread");
                    }
                }
            }
        }

        public void SetStartScript()
        {
            menuItem_startscript.Text = Globals.m_ResourceManager.GetString("menuItem_startscript");
            startScriptToolStripMenuItem.Text = Globals.m_ResourceManager.GetString("menuItem_startscript");
        }

        private void menuItem_scriptdebugger_Click(object sender, EventArgs e)
        {
            //launch the script debugger
            if (Globals.scriptdebugwindow == null || Globals.scriptdebugwindow.IsDisposed == true)
            {
                Globals.scriptdebugwindow = new ScriptDebugger();
            }
            Globals.scriptdebugwindow.BringToFront();
            Globals.scriptdebugwindow.Show();
        }

        private void menuItem_loadscript_Click(object sender, EventArgs e)
        {
            //need to load a script file into the script engine
            try
            {
                DialogResult okOrNo;//dialog return value, if it is DialogResult.OK then everything is OK

                openFileDialog1.Title = "Select script file"; 
                openFileDialog1.InitialDirectory = Globals.PATH + "\\Scripts";//Initial dir, where it begins looking at.
                openFileDialog1.Filter = "Script Files (*.l2s)|*.l2s";//this particualr format is for one file type.
                openFileDialog1.FilterIndex = 1;//this means that the first description is the default one.
                openFileDialog1.RestoreDirectory = true;//The next dialog box opened will start at the inital dir.
                okOrNo = openFileDialog1.ShowDialog();//open the dialog box and save the result.			

                if (okOrNo == DialogResult.OK)//if the dialog box works
                {
                    Globals.Script_MainFile = openFileDialog1.FileName;
                }
            }
            catch
            {
                Globals.l2net_home.Add_Error("ERROR WHILE SETTING SCRIPT START POINT!");
            }
        }

        private void menuItem_encryptscript_Click(object sender, EventArgs e)
        {
            try
            {
                string source_file, output_file;

                DialogResult okOrNo;//dialog return value, if it is DialogResult.OK then everything is OK

                openFileDialog1.Title = "Select source script file";
                openFileDialog1.InitialDirectory = Globals.PATH + "\\Scripts";//Initial dir, where it begins looking at.
                openFileDialog1.Filter = "Script Files (*.l2s)|*.l2s|Script Classes (*.l2c)|*.l2c";//this particualr format is for one file type.
                openFileDialog1.FilterIndex = 1;//this means that the first description is the default one.
                openFileDialog1.RestoreDirectory = true;//The next dialog box opened will start at the inital dir.
                okOrNo = openFileDialog1.ShowDialog();//open the dialog box and save the result.			

                if (okOrNo != DialogResult.OK)//if the dialog box works
                {
                    return;
                }

                source_file = openFileDialog1.FileName;

                saveFileDialog1.Title = "Select output script file";
                saveFileDialog1.InitialDirectory = Globals.PATH + "\\Scripts";//Initial dir, where it begins looking at.
                saveFileDialog1.Filter = "Script Files (*.l2s)|*.l2s";//this particualr format is for one file type.
                saveFileDialog1.FilterIndex = 1;//this means that the first description is the default one.
                saveFileDialog1.RestoreDirectory = true;//The next dialog box opened will start at the inital dir.
                okOrNo = saveFileDialog1.ShowDialog();//open the dialog box and save the result.			

                if (okOrNo != DialogResult.OK)//if the dialog box works
                {
                    return;
                }

                output_file = saveFileDialog1.FileName;

                //got our two files... now to encrypt
                Script_Crypt.Encrypt(source_file, output_file);
            }
            catch
            {
                Globals.l2net_home.Add_Error("ERROR WHILE ENCYPTING SCRIPT!");
            }
        }

        private void menuItem_debug_mode_Click(object sender, EventArgs e)
        {
            menuItem_debug_mode.Checked = !menuItem_debug_mode.Checked;

            Globals.Script_Debugging = menuItem_debug_mode.Checked;

            if (Globals.Script_Debugging)
            {
                Add_Text("Debugging Mode is ON");
            }
            else
            {
                Add_Text("Debugging Mode is OFF");
            }
        }

        private void menuItem_dump_mode_Click(object sender, EventArgs e)
        {
            menuItem_dump_mode.Checked = !menuItem_dump_mode.Checked;

            Globals.DumpModeClient = menuItem_dump_mode.Checked;

            if (Globals.DumpModeClient)
            {
                Add_Text("Clientside Dump Mode is ON");
            }
            else
            {
                Add_Text("Clientside Dump Mode is OFF");
            }
        }

        private void menuItem_dump_mode_server_Click(object sender, EventArgs e)
        {
            menuItem_dump_mode_server.Checked = !menuItem_dump_mode_server.Checked;

            Globals.DumpModeServer = menuItem_dump_mode_server.Checked;

            if (Globals.DumpModeServer)
            {
                Add_Text("Serverside Dump Mode is ON");
            }
            else
            {
                Add_Text("Serverside Dump Mode is OFF");
            }
        }

        private void label_1_name_Click(object sender, EventArgs e)
        {
            TargetParty(0);
        }

        private void label_2_name_Click(object sender, EventArgs e)
        {
            TargetParty(1);
        }

        private void label_3_name_Click(object sender, EventArgs e)
        {
            TargetParty(2);
        }

        private void label_4_name_Click(object sender, EventArgs e)
        {
            TargetParty(3);
        }

        private void label_5_name_Click(object sender, EventArgs e)
        {
            TargetParty(4);
        }

        private void label_6_name_Click(object sender, EventArgs e)
        {
            TargetParty(5);
        }

        private void label_7_name_Click(object sender, EventArgs e)
        {
            TargetParty(6);
        }

        private void label_8_name_Click(object sender, EventArgs e)
        {
            TargetParty(7);
        }

        private void TargetParty(int index)
        {
            int i = 0;
            uint t_id = 0;
            int t_x = 0, t_y = 0, t_z = 0;

            Globals.PartyLock.EnterReadLock();
            try
            {

                foreach (PartyMember pmem in Globals.gamedata.PartyMembers.Values)
                {
                    if (i == index)
                    {
                        t_id = pmem.ID;
                        t_x = pmem.X;
                        t_y = pmem.Y;
                        t_z = pmem.Z;
                        break;
                    }
                    i++;
                }
            }
            finally
            {
                Globals.PartyLock.ExitReadLock();
            }

            if (t_id != 0)
            {
                ServerPackets.Target(t_id, t_x, t_y, t_z, false);
            }
        }

        void TabControl_HandleCreated(object sender, System.EventArgs e)
        {
            // Send TCM_SETMINTABWIDTH
            SendMessage((sender as TabControl).Handle, 0x1300 + 49, IntPtr.Zero, (IntPtr)4);
        }
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wp, IntPtr lp);

        void tabControl_char_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Globals.FulllyIn)
            {
                switch (tabControl_char.SelectedIndex)
                {
                    case 0://char info
                        break;
                    case 1://inventory
                        ServerPackets.RequestInventory();
                        break;
                    case 2://skill list
                        ServerPackets.RequestSkillList();
                        break;
                    case 3://clan info
                        break;
                }
            }
        }

        private void ActionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.actionwindow == null || Globals.actionwindow.IsDisposed == true)
            {
                Globals.actionwindow = new ActionWindow();
            }
            Globals.actionwindow.TopMost = true;
            Globals.actionwindow.BringToFront();
            Globals.actionwindow.Show();
        }

        private void showTargetInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Globals.targetinfoscreen == null || Globals.targetinfoscreen.IsDisposed == true)
            {
                Globals.targetinfoscreen = new TargetInfoScreen();
            }
            Globals.targetinfoscreen.TopMost = true;
            Globals.targetinfoscreen.BringToFront();
            Globals.targetinfoscreen.Show();
        }
    }
}
