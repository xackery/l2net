using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace L2_login
{
    public class Chat_Line
    {
        public string text = "";
        public System.Drawing.Brush color = System.Drawing.Brushes.Black;
        public TextType type = TextType.ALL;
    }

    public partial class L2NET
    {
        public void Add_Error(string text)
        {
            Add_Error(text, true);
        }
        
        public void Add_Error(string text, bool audio)
        {
            if (audio)
            {
                //lets play a random sad clip :'(
                int snd = Globals.Rando.Next(0, 4);

                switch (snd)
                {
                    case 0:
                        VoicePlayer.PlaySound(3);
                        break;
                    case 1:
                        VoicePlayer.PlaySound(4);
                        break;
                    case 2:
                        VoicePlayer.PlaySound(5);
                        break;
                    case 3:
                        VoicePlayer.PlaySound(6);
                        break;
                    case 4:
                        VoicePlayer.PlaySound(9);
                        break;
                }
            }

            //now the error text
            //Add_Text("#############################", Globals.Pink);
            Add_Text("ERROR: " + text, Globals.White, TextType.ALL);
            //Add_Text("#############################", Globals.Pink);
            //flush so we make sure we can view it again
            Util.Flush_TextFile();
        }

        public void Add_PopUpError(string text)
        {
            MessageBox.Show(text, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //Add_Error(text, true);
        }

        public void Add_PopUpError(string text, bool err)
        {
            MessageBox.Show(text, "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Add_Error(text, err);
        }

        public void Add_Alarm(string text)
        {
            Add_Text("ALARM: " + text, Globals.Orange, TextType.ALL);
        }

        public void Add_Logout(string text)
        {
            Add_Text("LOGOUT: " + text, Globals.Orange, TextType.ALL);
        }

        public void Add_Debug(string text)
        {
            Add_Text("DEBUG: " + text, Globals.Orange, TextType.SYSTEM);
        }

        public void Add_Dump(string text, bool is_server)
        {
            if (is_server)
            {
                Add_Text("SERVER DUMP: " + text, Globals.Yellow, TextType.BOT);
            }
            else
            {
                Add_Text("CLIENT DUMP: " + text, Globals.Orange, TextType.BOT);
            }
        }

        public void Add_OnlyDebug(string text)
        {
#if DEBUG
            Add_Text("DEBUGGING: " + text, Globals.Orange, TextType.BOT);
#endif
        }

        public void Add_Text(string text)
        {
            Add_Text(text, Globals.Red);
        }

        public void Add_Text(string text, System.Drawing.Brush col)
        {
            Add_Text(text, col, TextType.ALL);
        }

        public void Add_Text(string text, System.Drawing.Brush col, TextType type)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.

            string time = System.DateTime.Now.ToLongTimeString() + " :[" + text + Environment.NewLine;
            Chat_Line new_chat = new Chat_Line();
            new_chat.text = time;
            new_chat.color = col;
            new_chat.type = type;

            Globals.ChatLock.EnterWriteLock();
            try
            {
                Chat_Messages.Enqueue(new_chat);
            }
            finally
            {
                Globals.ChatLock.ExitWriteLock();
            }

            Globals.l2net_home.timer_chat.Start();
        }

        delegate void UpdateChatWindow_Callback();
        public void UpdateChatWindow()
        {
            if (!Globals.CanPrint || Chat_Messages.Count == 0)
            {
                Globals.l2net_home.timer_chat.Start();
                return;
            }

            //we only need to check invoke on 1 chat box... because they are all on the same form
            if (this.colorListBox_all.InvokeRequired)
            {
                UpdateChatWindow_Callback d = new UpdateChatWindow_Callback(UpdateChatWindow);
                colorListBox_all.Invoke(d);
                return;
            }

            if (Globals.ChatLock.TryEnterWriteLock(Globals.THREAD_WAIT_GUI))
            {
                try
                {
                    bool updating_local = false;
                    bool updating_trade = false;
                    bool updating_party = false;
                    bool updating_clan = false;
                    bool updating_ally = false;
                    bool updating_system = false;
                    bool updating_bot = false;
                    bool updating_hero = false;

                    colorListBox_all.Enabled = false;
                    colorListBox_all.BeginUpdate();

                    //no need to check if we have chat to print... we wouldn't get here if we didn't have any

                    Chat_Line pop;
                    int cnt = 0;

                    while (Chat_Messages.Count > 0)
                    {
                        while (Chat_Messages.Count > Globals.MAX_LINES_BYPASS)
                        {
                            pop = (Chat_Line)Chat_Messages.Dequeue();

                            if (Globals.text_out != null)
                            {
                                if (Globals.LogWriting)
                                    Globals.text_out.Write(pop.text);//it has a newline in it... so no need to use writeline
                            }
                        }

                        cnt++;
                        pop = (Chat_Line)Chat_Messages.Dequeue();

                        if (Globals.text_out != null)
                        {
                            if (Globals.LogWriting)
                            Globals.text_out.Write(pop.text);//it has a newline in it... so no need to use writeline
                        }

                        colorListBox_all.AddItemStart(pop.text, pop.color);

                        switch (pop.type)
                        {
                            case TextType.ALL:
                                if (!updating_local)
                                {
                                    updating_local = true;
                                    colorListBox_local.Enabled = false;
                                    colorListBox_local.BeginUpdate();
                                }
                                if (!updating_trade)
                                {
                                    updating_trade = true;
                                    colorListBox_trade.Enabled = false;
                                    colorListBox_trade.BeginUpdate();
                                }
                                if (!updating_party)
                                {
                                    updating_party = true;
                                    colorListBox_party.Enabled = false;
                                    colorListBox_party.BeginUpdate();
                                }
                                if (!updating_clan)
                                {
                                    updating_clan = true;
                                    colorListBox_clan.Enabled = false;
                                    colorListBox_clan.BeginUpdate();
                                }
                                if (!updating_ally)
                                {
                                    updating_ally = true;
                                    colorListBox_ally.Enabled = false;
                                    colorListBox_ally.BeginUpdate();
                                }
                                if (!updating_system)
                                {
                                    updating_system = true;
                                    colorListBox_system.Enabled = false;
                                    colorListBox_system.BeginUpdate();
                                }
                                if (!updating_bot)
                                {
                                    updating_bot = true;
                                    colorListBox_bot.Enabled = false;
                                    colorListBox_bot.BeginUpdate();
                                }
                                if (!updating_hero)
                                {
                                    updating_hero = true;
                                    colorListBox_hero.Enabled = false;
                                    colorListBox_hero.BeginUpdate();
                                }

                                colorListBox_local.AddItemStart(pop.text, pop.color);
                                colorListBox_trade.AddItemStart(pop.text, pop.color);
                                colorListBox_party.AddItemStart(pop.text, pop.color);
                                colorListBox_clan.AddItemStart(pop.text, pop.color);
                                colorListBox_ally.AddItemStart(pop.text, pop.color);
                                colorListBox_system.AddItemStart(pop.text, pop.color);
                                colorListBox_bot.AddItemStart(pop.text, pop.color);
                                break;
                            case TextType.LOCAL:
                                if (!updating_local)
                                {
                                    updating_local = true;
                                    colorListBox_local.BeginUpdate();
                                }
                                colorListBox_local.AddItemStart(pop.text, pop.color);
                                break;
                            case TextType.TRADE:
                                if (!updating_trade)
                                {
                                    updating_trade = true;
                                    colorListBox_trade.BeginUpdate();
                                }
                                colorListBox_trade.AddItemStart(pop.text, pop.color);
                                break;
                            case TextType.PARTY:
                                if (!updating_party)
                                {
                                    updating_party = true;
                                    colorListBox_party.BeginUpdate();
                                }
                                colorListBox_party.AddItemStart(pop.text, pop.color);
                                break;
                            case TextType.CLAN:
                                if (!updating_clan)
                                {
                                    updating_clan = true;
                                    colorListBox_clan.BeginUpdate();
                                }
                                colorListBox_clan.AddItemStart(pop.text, pop.color);
                                break;
                            case TextType.ALLY:
                                if (!updating_ally)
                                {
                                    updating_ally = true;
                                    colorListBox_ally.BeginUpdate();
                                }
                                colorListBox_ally.AddItemStart(pop.text, pop.color);
                                break;
                            case TextType.SYSTEM:
                                if (!updating_system)
                                {
                                    updating_system = true;
                                    colorListBox_system.BeginUpdate();
                                }
                                colorListBox_system.AddItemStart(pop.text, pop.color);
                                break;
                            case TextType.BOT:
                                if (!updating_bot)
                                {
                                    updating_bot = true;
                                    colorListBox_bot.BeginUpdate();
                                }
                                colorListBox_bot.AddItemStart(pop.text, pop.color);
                                break;
                            case TextType.HERO:
                                if (!updating_hero)
                                {
                                    updating_hero = true;
                                    colorListBox_hero.BeginUpdate();
                                }
                                colorListBox_hero.AddItemStart(pop.text, pop.color);
                                break;
                        }

                        if (cnt >= Globals.MAX_LINES_PASS)
                        {
                            Globals.l2net_home.timer_chat.Start();
                            break;
                        }
                    }

                    colorListBox_all.EndUpdate();
                    colorListBox_all.Enabled = true;
                    if (updating_local)
                    {
                        colorListBox_local.EndUpdate();
                        colorListBox_local.Enabled = true;
                    }
                    if (updating_trade)
                    {
                        colorListBox_trade.EndUpdate();
                        colorListBox_trade.Enabled = true;
                    }
                    if (updating_party)
                    {
                        colorListBox_party.EndUpdate();
                        colorListBox_party.Enabled = true;
                    }
                    if (updating_clan)
                    {
                        colorListBox_clan.EndUpdate();
                        colorListBox_clan.Enabled = true;
                    }
                    if (updating_ally)
                    {
                        colorListBox_ally.EndUpdate();
                        colorListBox_ally.Enabled = true;
                    }
                    if (updating_system)
                    {
                        colorListBox_system.EndUpdate();
                        colorListBox_system.Enabled = true;
                    }
                    if (updating_bot)
                    {
                        colorListBox_bot.EndUpdate();
                        colorListBox_bot.Enabled = true;
                    }
                    if (updating_hero)
                    {
                        colorListBox_hero.EndUpdate();
                        colorListBox_hero.Enabled = true;
                    }
                }
                catch
                {
                    //crashed...
                }
                finally
                {
                    Globals.ChatLock.ExitWriteLock();
                }
            }
            else
            {
                //we failed to lock... restart the timer
                Globals.l2net_home.timer_chat.Start();
                return;
            }
        }

        delegate void Clear_ChatBox_Callback();
        public void Clear_ChatBox()
        {
            if (this.colorListBox_all.InvokeRequired)
            {
                Clear_ChatBox_Callback d = new Clear_ChatBox_Callback(Clear_ChatBox);
                colorListBox_all.Invoke(d);
                return;
            }

            switch(tabControl_ChatSelect.SelectedIndex)
            {
                case 0:
                    colorListBox_all.BeginUpdate();
                    colorListBox_all.Clear();
                    colorListBox_all.EndUpdate();
                    break;
                case 1:
                    colorListBox_system.BeginUpdate();
                    colorListBox_system.Clear();
                    colorListBox_system.EndUpdate();
                    break;
                case 2:
                    colorListBox_bot.BeginUpdate();
                    colorListBox_bot.Clear();
                    colorListBox_bot.EndUpdate();
                    break;
                case 3:
                    colorListBox_local.BeginUpdate();
                    colorListBox_local.Clear();
                    colorListBox_local.EndUpdate();
                    break;
                case 4:
                    colorListBox_trade.BeginUpdate();
                    colorListBox_trade.Clear();
                    colorListBox_trade.EndUpdate();
                    break;
                case 5:
                    colorListBox_party.BeginUpdate();
                    colorListBox_party.Clear();
                    colorListBox_party.EndUpdate();
                    break;
                case 6:
                    colorListBox_clan.BeginUpdate();
                    colorListBox_clan.Clear();
                    colorListBox_clan.EndUpdate();
                    break;
                case 7:
                    colorListBox_ally.BeginUpdate();
                    colorListBox_ally.Clear();
                    colorListBox_ally.EndUpdate();
                    break;
            }
        }

        //npc chat dialog box
        public void Clear_DialogBox()
        {
            //invoke not needed... done on calling thread
            richTextBox_dialog.Text = "";
        }

        public void Suspend_Dialog(bool state)
        {
            //invoke not needed... done on calling thread
            if (state)
            {
                richTextBox_dialog.SuspendLayout();
            }
            else
            {
                richTextBox_dialog.ResumeLayout();
            }
        }
        
        public void Add_Dialog(string text)
        {
            //invoke not needed... done on calling thread
            richTextBox_dialog.AppendText(text);
        }

        public void Add_Dialog_Link(string text, string hyperlink)
        {
            //invoke not needed... done on calling thread
            richTextBox_dialog.InsertLink(text, hyperlink);
        }

        public void Add_Dialog_Image(string name)
        {
            Bitmap myBitmap = new Bitmap(Globals.PATH + "\\Crests\\" + name + ".bmp");

            richTextBox_dialog.ReadOnly = false;

            richTextBox_dialog.InsertBMP(myBitmap);

            richTextBox_dialog.ReadOnly = true;
        }

        public void Show_DialogBox()
        {
            //invoke not needed... done on calling thread
            if (Globals.Hide_Message_Boxes)
            {
            }
            else
            {
                panel_npc_chat.Show();
                tabControl_char.SelectedIndex = 8;
            }
        }
    }
}
