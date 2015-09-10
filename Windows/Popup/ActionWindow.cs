using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace L2_login
{
    public partial class ActionWindow : Form
    {
        public ActionWindow()
        {
            InitializeComponent();
            this.TransparencyKey = Color.DeepPink;
            //SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //this.BackColor = Color.Transparent;
        }

        /* Components for making window draggable */
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        /* End */

        private void button_sit_stand_Click(object sender, EventArgs e)
        {
            ServerPackets.Use_Action_Parse((uint)PClientAction.SitStand, false, false);
        }

        private void button_walk_run_Click(object sender, EventArgs e)
        {
            ServerPackets.Use_Action_Parse((int)PClientAction.RunWalk, false, false);
        }

        private void button_attack_Click(object sender, EventArgs e)
        {
            switch (Globals.gamedata.my_char.CurrentTargetType)
            {
                case TargetType.ERROR:
                case TargetType.NONE:
                    break;
                case TargetType.SELF:
                    break;
                case TargetType.MYPET:
                    ServerPackets.Force_Attack(Globals.gamedata.my_char.TargetID, Util.Float_Int32(Globals.gamedata.my_pet.X), Util.Float_Int32(Globals.gamedata.my_pet.Y), Util.Float_Int32(Globals.gamedata.my_pet.Z), false);
                    break;
                case TargetType.MYPET1:
                    ServerPackets.Force_Attack(Globals.gamedata.my_char.TargetID, Util.Float_Int32(Globals.gamedata.my_pet1.X), Util.Float_Int32(Globals.gamedata.my_pet1.Y), Util.Float_Int32(Globals.gamedata.my_pet1.Z), false);
                    break;
                case TargetType.MYPET2:
                    ServerPackets.Force_Attack(Globals.gamedata.my_char.TargetID, Util.Float_Int32(Globals.gamedata.my_pet2.X), Util.Float_Int32(Globals.gamedata.my_pet2.Y), Util.Float_Int32(Globals.gamedata.my_pet2.Z), false);
                    break;
                case TargetType.MYPET3:
                    ServerPackets.Force_Attack(Globals.gamedata.my_char.TargetID, Util.Float_Int32(Globals.gamedata.my_pet3.X), Util.Float_Int32(Globals.gamedata.my_pet3.Y), Util.Float_Int32(Globals.gamedata.my_pet3.Z), false);
                    break;
                case TargetType.PLAYER:
                    Globals.PlayerLock.EnterReadLock();
                    try
                    {
                        CharInfo player = Util.GetChar(Globals.gamedata.my_char.TargetID);

                        if (player != null)
                        {
                            ServerPackets.Force_Attack(Globals.gamedata.my_char.TargetID, Util.Float_Int32(player.X), Util.Float_Int32(player.Y), Util.Float_Int32(player.Z), false);
                        }
                    }
                    finally
                    {
                        Globals.PlayerLock.ExitReadLock();
                    }
                    break;
                case TargetType.NPC:
                    Globals.NPCLock.EnterReadLock();
                    try
                    {
                        NPCInfo npc = Util.GetNPC(Globals.gamedata.my_char.TargetID);

                        if (npc != null)
                        {
                            ServerPackets.Force_Attack(Globals.gamedata.my_char.TargetID, Util.Float_Int32(npc.X), Util.Float_Int32(npc.Y), Util.Float_Int32(npc.Z), false);
                        }
                    }
                    finally
                    {
                        Globals.NPCLock.ExitReadLock();
                    }
                    break;
            }
        }

        private void button_trade_Click(object sender, EventArgs e)
        {
            if (Globals.tradewindow == null || Globals.tradewindow.IsDisposed == true)
            {
                Globals.tradewindow = new TradeWindow();
            }
            Globals.tradewindow.TopMost = true;
            Globals.tradewindow.BringToFront();
            Globals.tradewindow.Show();

            ServerPackets.Command_Trade();
        }

        private void button_target_nearest_Click(object sender, EventArgs e)
        {
            Globals.scriptthread.Script_TARGET_NEAREST_Internal();
        }

        private void button_pickup_Click(object sender, EventArgs e)
        {
            Globals.scriptthread.Script_CLICK_NEAREST_ITEM();
        }

        private void button_assist_Click(object sender, EventArgs e)
        {
            ServerPackets.Assist();
        }

        private void button_privatestore_sell_Click(object sender, EventArgs e)
        {

            ServerPackets.Social_ActionNew((byte)PClientAction.Open_PrivateStore_Sell); //This is also true for servers below ct 2.4
            if (Globals.privatestoresellwindow == null || Globals.privatestoresellwindow.IsDisposed == true)
            {
                Globals.privatestoresellwindow = new PrivateStoreSellWindow();
            }
            Globals.privatestoresellwindow.TopMost = true;
            Globals.privatestoresellwindow.BringToFront();
            Globals.privatestoresellwindow.Show();
        }

        private void button_privatestore_buy_Click(object sender, EventArgs e)
        {

        }

        private void button_recommend_Click(object sender, EventArgs e)
        {
            ServerPackets.Command_Evaluate(Globals.gamedata.my_char.TargetID);
        }

        private void button_general_manufacture_Click(object sender, EventArgs e)
        {

        }

        private void button_request_duel_Click(object sender, EventArgs e)
        {

        }

        private void button_withdraw_from_duel_Click(object sender, EventArgs e)
        {

        }

        private void button_package_sale_Click(object sender, EventArgs e)
        {

        }

        private void button_party_invite_Click(object sender, EventArgs e)
        {
            ServerPackets.Command_Invite(Util.GetCharName(Globals.gamedata.my_char.TargetID));
        }

        private void button_party_leave_Click(object sender, EventArgs e)
        {
            ServerPackets.Command_Leave();
        }

        private void button_party_dismiss_Click(object sender, EventArgs e)
        {
            ServerPackets.Command_Dismiss(Util.GetCharName(Globals.gamedata.my_char.TargetID));
        }

        private void button_party_changeleader_Click(object sender, EventArgs e)
        {
            ServerPackets.Command_ChangePartyLeader(Util.GetCharName(Globals.gamedata.my_char.TargetID));
        }

        private void button_party_commandchnl_inv_Click(object sender, EventArgs e)
        {

        }

        private void button_party_partyduel_Click(object sender, EventArgs e)
        {

        }

        private void button_social_hello_Click(object sender, EventArgs e)
        {
            ServerPackets.Command_SocialHello();
        }

        private void button_social_victory_Click(object sender, EventArgs e)
        {
            ServerPackets.Command_SocialVictory();
        }

        private void button_social_advance_Click(object sender, EventArgs e)
        {
            ServerPackets.Command_SocialCharge();
        }

        private void button_social_yes_Click(object sender, EventArgs e)
        {
            ServerPackets.Command_SocialYes();
        }

        private void button_social_no_Click(object sender, EventArgs e)
        {
            ServerPackets.Command_SocialNo();
        }

        private void button_social_bow_Click(object sender, EventArgs e)
        {
            ServerPackets.Command_SocialBow();
        }

        private void button_social_unaware_Click(object sender, EventArgs e)
        {
            ServerPackets.Command_SocialUnaware();
        }

        private void button_social_waiting_Click(object sender, EventArgs e)
        {
            ServerPackets.Command_SocialWaiting();
        }

        private void button_social_laugh_Click(object sender, EventArgs e)
        {
            ServerPackets.Command_SocialLaugh();
        }

        private void button_social_clap_Click(object sender, EventArgs e)
        {
            ServerPackets.Command_SocialApplause();
        }

        private void button_social_dance_Click(object sender, EventArgs e)
        {
            ServerPackets.Command_SocialDance();
        }

        private void button_social_sorrow_Click(object sender, EventArgs e)
        {
            ServerPackets.Command_SocialSad();
        }

        private void button_social_charm_Click(object sender, EventArgs e)
        {
            ServerPackets.Command_SocialCharm();
        }

        private void button_social_shyness_Click(object sender, EventArgs e)
        {
            ServerPackets.Command_SocialShyness();
        }

        private void button_close_actions_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ActionWindow_MouseDown(object sender, MouseEventArgs e)
        {
            /* Make window draggable */
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

    }
}
