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
    public partial class PetWindowTake : Form
    {
        public PetWindowTake()
        {
            InitializeComponent();
            fill_pet_inventory();
        }

        private void listView_petTake_MyInv_DoubleClick(object sender, EventArgs e)
        {
            string item = listView_petTake_PetInv.SelectedItems[0].SubItems[0].Text;
            int loc = listView_petTake_PetInv.SelectedItems[0].Index; //Index
            uint objID = System.Convert.ToUInt32(listView_petTake_PetInv.SelectedItems[0].SubItems[2].Text); //Obj ID
            ulong quantity = System.Convert.ToUInt64(listView_petTake_PetInv.SelectedItems[0].SubItems[1].Text); //Quantity in inventory
            ulong trquantity = System.Convert.ToUInt64(textBox_petTake_Quantity.Text);

            if (trquantity > quantity)
            {
                trquantity = quantity; //If user is trying to put more items on self than pet has in inventory, set quantity to max
            }
            if (trquantity < 1) //If user is trying to trade less than 1 item, do nothing.
            {
                return;
            }

            //Update listview
            if (trquantity == quantity)
            {
                listView_petTake_PetInv.Items.RemoveAt(loc);
            }
            else
            {
                listView_petTake_PetInv.SelectedItems[0].SubItems[1].Text = (quantity - trquantity).ToString();
            }

            Globals.l2net_home.Add_Text("Took " + trquantity.ToString() + " " + item + " from pet", Globals.Green, TextType.BOT);

            ServerPackets.RequestGetItemFromPet(objID, trquantity);

        }

        private void textBox_petTake_Quantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+")) && (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\b")))
                e.Handled = true;
        }

        private void button_petTake_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void fill_pet_inventory()
        {
            try
            {
                listView_petTake_PetInv.Items.Clear();
                foreach (PetInventoryInfo inv_inf in Globals.gamedata.my_pet.inventory.Values)
                {
                    //add it
                    System.Windows.Forms.ListViewItem ObjListItem;

                    if (inv_inf.isEquipped == 0x01) //Equipped
                    {
                        //Do nothing
                    }
                    else if (inv_inf.Type2 == 0x03) //quest item
                    {
                        //Do Nothing
                    }
                    else
                    {
                        if (inv_inf.Enchant == 0)
                        {
                            ObjListItem = new ListViewItem(Util.GetItemName(inv_inf.ItemID));
                        }
                        else
                        {
                            ObjListItem = new ListViewItem("+" + inv_inf.Enchant.ToString() + " " + Util.GetItemName(inv_inf.ItemID));
                        }
                        ObjListItem.SubItems.Add(inv_inf.Count.ToString());//Count
                        ObjListItem.SubItems.Add(inv_inf.ID.ToString());//ObjID
                        listView_petTake_PetInv.Items.Add(ObjListItem);
                    }
                }
            }
            catch
            {
                //failed
            }
        }
    }
}
