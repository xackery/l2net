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
    public partial class PetWindow : Form
    {
        public PetWindow()
        {
            InitializeComponent();
            fill_pet_inventory();
        }

        private void button_pet_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        delegate void Set_Pet_Info_Callback();
        public void Set_Pet_Info()
        {
            label_pet_Name.Text = Globals.gamedata.my_pet.Name;
            if (string.IsNullOrWhiteSpace(Globals.gamedata.my_pet.Name))
                label_pet_Name.Text = "No Name";
            label_pet_Level.Text = Globals.gamedata.my_pet.Level.ToString();
            label_pet_PAtk.Text = Globals.gamedata.my_pet.Patk.ToString();
            label_pet_PDef.Text = Globals.gamedata.my_pet.PDef.ToString();
            label_pet_MAtk.Text = Globals.gamedata.my_pet.Matk.ToString();
            label_pet_MDef.Text = Globals.gamedata.my_pet.MDef.ToString();
            label_pet_Accuracy.Text = Globals.gamedata.my_pet.Accuracy.ToString();
            label_pet_CritRate.Text = Globals.gamedata.my_pet.Focus.ToString();
            label_pet_AtkSpd.Text = Globals.gamedata.my_pet.PatkSpeed.ToString();
            label_pet_Soulshot.Text = Globals.gamedata.my_pet.SSUsage.ToString();
            label_pet_Evasion.Text = Globals.gamedata.my_pet.Evasion.ToString();
            label_pet_Speed.Text = Globals.gamedata.my_pet.RunSpeed.ToString();
            label_pet_Casting.Text = Globals.gamedata.my_pet.MatkSpeed.ToString();
            label_pet_Spiritshot.Text = Globals.gamedata.my_pet.SPSUSage.ToString();
            label_pet_SP.Text = Globals.gamedata.my_pet.SP.ToString();

            if (this.progressBar_pet_HP.InvokeRequired)
            {
                Set_Pet_Info_Callback d = new Set_Pet_Info_Callback(Set_Pet_Info);
                progressBar_pet_HP.Invoke(d);
                return;
            }

            try
            {
                progressBar_pet_HP.Value = System.Convert.ToInt32((Globals.gamedata.my_pet.Cur_HP / Globals.gamedata.my_pet.Max_HP) * 100);
                progressBar_pet_HP.BarText = Globals.gamedata.my_pet.Cur_HP.ToString() + " / " + Globals.gamedata.my_pet.Max_HP.ToString();
            }
            catch
            {
                progressBar_pet_HP.Value = 0;
            }

            try
            {
                progressBar_pet_MP.Value = System.Convert.ToInt32((Globals.gamedata.my_pet.Cur_MP / Globals.gamedata.my_pet.Max_MP) * 100);
                progressBar_pet_MP.BarText = Globals.gamedata.my_pet.Cur_MP.ToString() + " / " + Globals.gamedata.my_pet.Max_MP.ToString();
            }
            catch
            {
                progressBar_pet_MP.Value = 0;
            }

            try
            {
                progressBar_pet_Food.Value = System.Convert.ToInt32((System.Convert.ToDecimal(Globals.gamedata.my_pet.Cur_Fed) / System.Convert.ToDecimal(Globals.gamedata.my_pet.Max_Fed)) * 100);
                progressBar_pet_Food.BarText = (System.Convert.ToDecimal(Globals.gamedata.my_pet.Cur_Fed) / System.Convert.ToDecimal(Globals.gamedata.my_pet.Max_Fed)).ToString("P", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch
            {
                progressBar_pet_Food.Value = 0;
            }


            try
            {
                progressBar_pet_Load.Value = System.Convert.ToInt32((System.Convert.ToDecimal(Globals.gamedata.my_pet.Cur_Load) / System.Convert.ToDecimal(Globals.gamedata.my_pet.Max_Load)) * 100);
                progressBar_pet_Load.BarText = (System.Convert.ToDecimal(Globals.gamedata.my_pet.Cur_Load) / System.Convert.ToDecimal(Globals.gamedata.my_pet.Max_Load)).ToString("P", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch
            {
                progressBar_pet_Load.Value = 0;
            }





            progressBar_pet_XP.BarText = Get_Pet_XP_Percent();
            progressBar_pet_XP.Value = Get_Pet_XP_Percent_Int();

        }




        public static int Get_Pet_XP_Percent_Int()
        {
            try
            {
                ulong xp = (ulong)Globals.gamedata.my_pet.XP;

                if (xp == 0)
                {
                    return 0;
                }

                ulong lastlvlxp = Globals.gamedata.my_pet.XP_ThisLevel;
                ulong nextlvlxp = Globals.gamedata.my_pet.XP_NextLevel;

                xp -= lastlvlxp;
                nextlvlxp -= lastlvlxp;

                //float per = ((float)xp * 100) / ((float)nextlvlxp);
                float per = (((float)xp) / ((float)nextlvlxp)) * 100;
                //per = System.Convert.ToSingle(System.Math.Round(per, 6));

                return System.Convert.ToInt32(per);
            }
            catch
            {
                return 0;
            }
        }

        public static string Get_Pet_XP_Percent()
        {
            try
            {
                ulong xp = (ulong)Globals.gamedata.my_pet.XP;

                if (xp == 0)
                {
                    return "0%";
                }

                ulong lastlvlxp = Globals.gamedata.my_pet.XP_ThisLevel;
                ulong nextlvlxp = Globals.gamedata.my_pet.XP_NextLevel;

                xp -= lastlvlxp;
                nextlvlxp -= lastlvlxp;

                //float per = ((float)xp * 100) / ((float)nextlvlxp);
                float per = ((float)xp) / ((float)nextlvlxp);
                //per = System.Convert.ToSingle(System.Math.Round(per, 6));

                return per.ToString("P", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch
            {
                return "0%";
            }
        }


        private void button_pet_Unsummon_Click(object sender, EventArgs e)
        {
            ServerPackets.Use_Action_Parse((uint)PClientAction.Pet_Unsummon, false, false);
            this.Close();
        }


        public void fill_pet_inventory()
        {
            try
            {
                listView_pet_PetInv.Items.Clear();
                foreach (PetInventoryInfo inv_inf in Globals.gamedata.my_pet.inventory.Values)
                {
                    //Add
                    ListViewItem ObjListItem;
                    if (inv_inf.Enchant == 0)
                    {
                        ObjListItem = new ListViewItem(Util.GetItemName(inv_inf.ItemID));
                    }
                    else
                    {
                        ObjListItem = new ListViewItem("+" + inv_inf.Enchant.ToString() + " " + Util.GetItemName(inv_inf.ItemID));
                    }
                    ObjListItem.SubItems.Add(inv_inf.Count.ToString());//Count
                    if (inv_inf.isEquipped == 0x01)
                    {
                        ObjListItem.SubItems.Add("X");
                    }
                    else
                    {
                        ObjListItem.SubItems.Add("");
                    }
                    ObjListItem.SubItems.Add(inv_inf.ID.ToString());//ObjID
                    listView_pet_PetInv.Items.Add(ObjListItem);
                }

            }
            catch
            {
                //failed
                Globals.l2net_home.Add_Text("ERROR: Failed to fill pet inventory list", Globals.Red, TextType.BOT);
            }

        }




        private void listView_pet_PetInv_DoubleClick(object sender, EventArgs e)
        {

        }

        private void button_pet_GiveItem_Click(object sender, EventArgs e)
        {
            if (Globals.petwindowgive == null || Globals.petwindowgive.IsDisposed == true)
            {
                Globals.petwindowgive = new PetWindowGive();
            }
            Globals.petwindowgive.TopMost = true;
            Globals.petwindowgive.BringToFront();
            Globals.petwindowgive.Show();

        }

        private void button_pet_TakeItem_Click(object sender, EventArgs e)
        {
            if (Globals.petwindowtake == null || Globals.petwindowtake.IsDisposed == true)
            {
                Globals.petwindowtake = new PetWindowTake();
            }
            Globals.petwindowtake.TopMost = true;
            Globals.petwindowtake.BringToFront();
            Globals.petwindowtake.Show();
        }

        private void button_pet_change_movement_mode_Click(object sender, EventArgs e)
        {
            if (Globals.gamedata.my_pet.SummonType == 1) //summon
            {
                ServerPackets.Use_Action_Parse((uint)PClientAction.Summon_RunWalk, false, false);
            }
            else if (Globals.gamedata.my_pet.SummonType == 2) //Pet
            {
                ServerPackets.Use_Action_Parse((uint)PClientAction.Pet_RunWalk, false, false);
            }
        }

        private void button_pet_attack_Click(object sender, EventArgs e)
        {
            if (Globals.gamedata.my_pet.SummonType == 1) //summon
            {
                ServerPackets.Use_Action_Parse((uint)PClientAction.Summon_Attack, Globals.gamedata.Control, Globals.gamedata.Shift);
            }
            else if (Globals.gamedata.my_pet.SummonType == 2) //Pet
            {
                ServerPackets.Use_Action_Parse((uint)PClientAction.Pet_Attack, Globals.gamedata.Control, Globals.gamedata.Shift);
            }
        }

        private void button_pet_stop_Click(object sender, EventArgs e)
        {
            if (Globals.gamedata.my_pet.SummonType == 1) //summon
            {
                ServerPackets.Use_Action_Parse((uint)PClientAction.Summon_Stop, false, false);
            }
            else if (Globals.gamedata.my_pet.SummonType == 2) //Pet
            {
                ServerPackets.Use_Action_Parse((uint)PClientAction.Pet_Stop, false, false);
            }
        }

        private void button_pet_pickup_Click(object sender, EventArgs e)
        {
            if (Globals.gamedata.my_pet.SummonType == 1) //summon
            {
                //do nothing
            }
            else if (Globals.gamedata.my_pet.SummonType == 2) //Pet
            {
                ServerPackets.Use_Action_Parse((uint)PClientAction.Pet_PickUp, false, false);
            }
        }

        private void button_pet_move_Click(object sender, EventArgs e)
        {
            if (Globals.gamedata.my_pet.SummonType == 1) //summon
            {
                ServerPackets.Use_Action_Parse((uint)PClientAction.Summon_Move, false, false);
            }
            else if (Globals.gamedata.my_pet.SummonType == 2) //Pet
            {
                ServerPackets.Use_Action_Parse((uint)PClientAction.Pet_Move, false, false);
            }
        }

        private void button_pet_mount_Click(object sender, EventArgs e)
        {
            if (Globals.gamedata.my_pet.Mountable == 1)
            {
                ServerPackets.Use_Action_Parse((uint)PClientAction.Mount, false, false);
            }
        }


    }
}
