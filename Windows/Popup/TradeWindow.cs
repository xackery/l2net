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
    public partial class TradeWindow : Form
    {
        public TradeWindow()
        {
            InitializeComponent();
            FillInvenrotyList();
        }
        /*23:59:38 :[CLIENT DUMP: 1B = AddTradeItem
         * 01 00 00 00 ??
         * 3B 97 13 10 Item unique ID
         * 01 00 00 00 Number of items
         * 00 00 00 00 number of items int64?
         * //Trade 1 soe to kamael village
00:00:07 :[CLIENT DUMP: 1C 01 00 00 00 //trade confirm

AddTradeItem = 0x1B*/


        private void button_trade_cancel_Click(object sender, EventArgs e)
        {
            ByteBuffer buff = new ByteBuffer(5);

            buff.WriteByte((byte)PClient.TradeDone);
            buff.WriteUInt32(0);

            Globals.gamedata.SendToGameServer(buff);
            this.Close();

        }

        private void button_trade_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_trade_confirm_Click(object sender, EventArgs e)
        {
            ByteBuffer buff = new ByteBuffer(5);

            buff.WriteByte((byte)PClient.TradeDone);
            buff.WriteUInt32(1);

            Globals.gamedata.SendToGameServer(buff);
            this.Close();
        }

        private void FillInvenrotyList()
        {
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

                    listView_trade_mychar.Items.Add(ObjListItem);
                }
            }
        }


        private void listView_trade_mychar_DoubleClick(object sender, EventArgs e)
        {
            string item = listView_trade_mychar.SelectedItems[0].SubItems[0].Text;
            int loc = listView_trade_mychar.SelectedItems[0].Index; //Index
            uint objID = System.Convert.ToUInt32(listView_trade_mychar.SelectedItems[0].SubItems[2].Text); //Obj ID
            ulong quantity = System.Convert.ToUInt64(listView_trade_mychar.SelectedItems[0].SubItems[1].Text); //Quantity in inventory
            ulong trquantity = System.Convert.ToUInt64(textBox_trade_quantity.Text);

            if (trquantity > quantity)
            {
                trquantity = quantity; //If user is trying to put more items in trade window than he has in inventory, set quantity to max
            }
            if (trquantity < 1) //If user is trying to trade less than 1 item, do nothing.
            {
                return;
            }

            ByteBuffer buff = new ByteBuffer(17);

            buff.WriteByte((byte)PClient.AddTradeItem);
            buff.WriteUInt32(1);
            buff.WriteUInt32(objID);
            buff.WriteUInt64(trquantity); //CT 2.3 limit increased
            //buff.WriteUInt32(0);
            Globals.gamedata.SendToGameServer(buff);

            if (quantity == trquantity) //Trade all
            {
                listView_trade_mychar.Items.RemoveAt(loc);
            }
            else //Trade some
            {
                listView_trade_mychar.SelectedItems[0].SubItems[1].Text = (quantity - trquantity).ToString();
            }

            System.Windows.Forms.ListViewItem ObjListItem;
            ObjListItem = new ListViewItem(item);
            ObjListItem.SubItems.Add(trquantity.ToString());
            listView_trade_send.Items.Add(ObjListItem);

        }

        private void textBox_trade_quantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+")) && (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\b")))
                e.Handled = true;
        }
    }
}
