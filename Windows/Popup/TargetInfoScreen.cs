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
    public partial class TargetInfoScreen : System.Windows.Forms.Form
    {
        public TargetInfoScreen()
        {
            InitializeComponent();
            ListTargetInfo();
        }



        private void ListTargetInfo()
        {
            string cp = "0/0";
            string hp = "0/0";
            string mp = "0/0";
            uint target_id = Globals.gamedata.my_char.TargetID;

            switch (Globals.gamedata.my_char.CurrentTargetType)
            {

                case TargetType.SELF:
                    hp = Globals.gamedata.my_char.Cur_HP.ToString() + "/" + Globals.gamedata.my_char.Max_HP.ToString();
                    mp = Globals.gamedata.my_char.Cur_MP.ToString() + "/" + Globals.gamedata.my_char.Max_MP.ToString();
                    cp = Globals.gamedata.my_char.Cur_CP.ToString() + "/" + Globals.gamedata.my_char.Max_CP.ToString();
                    break;
                case TargetType.MYPET:
                    hp = Globals.gamedata.my_pet.Cur_HP.ToString() + "/" + Globals.gamedata.my_pet.Max_HP.ToString();
                    mp = Globals.gamedata.my_pet.Cur_MP.ToString() + "/" + Globals.gamedata.my_pet.Max_MP.ToString();
                    cp = Globals.gamedata.my_pet.Cur_CP.ToString() + "/" + Globals.gamedata.my_pet.Max_CP.ToString();
                    break;
                case TargetType.MYPET1:
                    hp = Globals.gamedata.my_pet1.Cur_HP.ToString() + "/" + Globals.gamedata.my_pet1.Max_HP.ToString();
                    mp = Globals.gamedata.my_pet1.Cur_MP.ToString() + "/" + Globals.gamedata.my_pet1.Max_MP.ToString();
                    cp = Globals.gamedata.my_pet1.Cur_CP.ToString() + "/" + Globals.gamedata.my_pet1.Max_CP.ToString();
                    break;
                case TargetType.MYPET2:
                    hp = Globals.gamedata.my_pet2.Cur_HP.ToString() + "/" + Globals.gamedata.my_pet2.Max_HP.ToString();
                    mp = Globals.gamedata.my_pet2.Cur_MP.ToString() + "/" + Globals.gamedata.my_pet2.Max_MP.ToString();
                    cp = Globals.gamedata.my_pet2.Cur_CP.ToString() + "/" + Globals.gamedata.my_pet2.Max_CP.ToString();
                    break;
                case TargetType.MYPET3:
                    hp = Globals.gamedata.my_pet3.Cur_HP.ToString() + "/" + Globals.gamedata.my_pet3.Max_HP.ToString();
                    mp = Globals.gamedata.my_pet3.Cur_MP.ToString() + "/" + Globals.gamedata.my_pet3.Max_MP.ToString();
                    cp = Globals.gamedata.my_pet3.Cur_CP.ToString() + "/" + Globals.gamedata.my_pet3.Max_CP.ToString();
                    break;
                case TargetType.PLAYER:
                    Globals.PlayerLock.EnterReadLock();
                    try
                    {
                        CharInfo player = Util.GetChar(target_id);

                        if (player != null)
                        {
                            /*if (Globals.gamedata.Chron >= Chronicle.CT2_4 && Globals.gamedata.Official_Server)
                            {
                                /*
                                hp = ("HP: " + player.Cur_HP / 100000).ToString() + "%";
                                mp = ("MP: " + player.Cur_MP / 100000).ToString() + "%";
                                cp = ("CP: " + player.Cur_CP / 100000).ToString() + "%";
                                hp = ("HP: " + (player.Cur_HP / 10000000).ToString("P", System.Globalization.CultureInfo.InvariantCulture));
                                mp = ("CP: " + (player.Cur_MP / 10000000).ToString("P", System.Globalization.CultureInfo.InvariantCulture));
                                cp = ("MP: " + (player.Cur_CP / 10000000).ToString("P", System.Globalization.CultureInfo.InvariantCulture)); 

                            }
                            else
                            {*/
                            hp = player.Cur_HP.ToString() + "/" + player.Max_HP.ToString();
                            mp = player.Cur_MP.ToString() + "/" + player.Max_MP.ToString();
                            cp = player.Cur_CP.ToString() + "/" + player.Max_CP.ToString();
                            //}
                        }
                    }//unlock
                    finally
                    {
                        Globals.PlayerLock.ExitReadLock();
                    }
                    break;
                case TargetType.NPC:
                    Globals.NPCLock.EnterReadLock();
                    try
                    {
                        NPCInfo npc = Util.GetNPC(target_id);

                        if (npc != null)
                        {
                            

                            hp = npc.Cur_HP.ToString() + "/" + npc.Max_HP.ToString();
                            mp = npc.Cur_MP.ToString() + "/" + npc.Max_MP.ToString();
                            cp = npc.Cur_CP.ToString() + "/" + npc.Max_CP.ToString();

                            label_targetinfo.Text = "Target Info: " + Environment.NewLine
                                + "ID : " + npc.ID.ToString() + Environment.NewLine
                                + "NPCID: " + npc.NPCID.ToString() + Environment.NewLine
                                + "Attackable: " + npc.isAttackable.ToString() + Environment.NewLine
                                + "X: " + npc.X.ToString() + Environment.NewLine
                                + "Y: " + npc.Y.ToString() + Environment.NewLine
                                + "Z: " + npc.Z.ToString() + Environment.NewLine
                                + "Heading: " + npc.Heading.ToString() + Environment.NewLine
                                + "MAtk Speed: " + npc.MatkSpeed.ToString() + Environment.NewLine
                                + "PAtk Speed: " + npc.PatkSpeed.ToString() + Environment.NewLine
                                + "Run Speed: " + npc.RunSpeed.ToString() + Environment.NewLine
                                + "Walk Speed: " + npc.WalkSpeed.ToString() + Environment.NewLine
                                + "Move Speed Mult: " + npc.MoveSpeedMult.ToString() + Environment.NewLine
                                + "Atk Speed Mult: " + npc.AttackSpeedMult.ToString() + Environment.NewLine
                                + "Collision Rad: " + npc.CollisionRadius.ToString() + Environment.NewLine
                                + "Collision Height: " + npc.CollisionHeight.ToString() + Environment.NewLine
                                + "RHand: " + npc.RHand.ToString() + Environment.NewLine
                                + "LHand: " + npc.LHand.ToString() + Environment.NewLine
                                + "LRHand: " + npc.LRHand.ToString() + Environment.NewLine
                                + "NameShows: " + npc.NameShows.ToString() + Environment.NewLine
                                + "IsRunning: " + npc.isRunning.ToString() + Environment.NewLine
                                + "IsInCombat: " + npc.isInCombat.ToString() + Environment.NewLine
                                + "IsAlikeDead: " + npc.isAlikeDead.ToString() + Environment.NewLine
                                + "IsInvisible: " + npc.isInvisible.ToString() + Environment.NewLine
                                + "IsSitting: " + npc.isSitting.ToString();
                        }
                    }//unlock
                    finally
                    {
                        Globals.NPCLock.ExitReadLock();
                    }
                    break;
            }


        }

        private void button_Refresh_Click(object sender, EventArgs e)
        {
            ListTargetInfo();
        }
    }
}
