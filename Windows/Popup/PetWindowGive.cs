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
    public partial class PetWindowGive : Form
    {
        public PetWindowGive()
        {
            InitializeComponent();
            fill_char_inventory();
        }

        private void listView_petGive_MyInv_DoubleClick(object sender, EventArgs e)
        {
            string item = listView_petGive_MyInv.SelectedItems[0].SubItems[0].Text;
            int loc = listView_petGive_MyInv.SelectedItems[0].Index; //Index
            uint objID = System.Convert.ToUInt32(listView_petGive_MyInv.SelectedItems[0].SubItems[2].Text); //Obj ID
            ulong quantity = System.Convert.ToUInt64(listView_petGive_MyInv.SelectedItems[0].SubItems[1].Text); //Quantity in inventory
            ulong trquantity = System.Convert.ToUInt64(textBox_petGive_Quantity.Text);

            if (trquantity > quantity)
            {
                trquantity = quantity; //If user is trying to put more items on pet than he has in inventory, set quantity to max
            }
            if (trquantity < 1) //If user is trying to trade less than 1 item, do nothing.
            {
                return;
            }

            //Update listview
            if (trquantity == quantity)
            {
                listView_petGive_MyInv.Items.RemoveAt(loc);
            }
            else
            {
                listView_petGive_MyInv.SelectedItems[0].SubItems[1].Text = (quantity - trquantity).ToString();
            }

            Globals.l2net_home.Add_Text("Gave " + trquantity.ToString() + " " + item + " to pet", Globals.Green, TextType.BOT);
            //Globals.l2net_home.Add_Text("Loc: " + loc.ToString());
            //Globals.l2net_home.Add_Text("ObjID: " + objID.ToString());
            //Globals.l2net_home.Add_Text("Quantity: " + trquantity.ToString());


            ServerPackets.RequestGiveItemToPet(objID, trquantity);
        }

        private void fill_char_inventory()
        {
            try
            {
                listView_petGive_MyInv.Items.Clear();
                foreach (InventoryInfo inv_inf in Globals.gamedata.inventory.Values)
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
                        listView_petGive_MyInv.Items.Add(ObjListItem);
                    }
                }
            }
            catch
            {
                //failed
            }
        }

        private void textBox_petGive_Quantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+")) && (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\b")))
                e.Handled = true;
        }

        private void button_petGive_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
