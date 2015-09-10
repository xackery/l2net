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
    public partial class PrivateStoreSellWindow : Form
    {
        public PrivateStoreSellWindow()
        {
            InitializeComponent();
            FillInventoryList();
        }

        private void button__privatestoresell_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_privatestoresell_abort_Click(object sender, EventArgs e)
        {
            ServerPackets.Abort_SellShop();
            this.Close();
        }

        private void FillInventoryList()
        {
            foreach (InventoryInfo inv_inf in Globals.gamedata.inventory.Values)
            {
                //add it
                System.Windows.Forms.ListViewItem ObjListItem;

                if (inv_inf.isEquipped == 0x01 || inv_inf.Type2 == 0x03 || inv_inf.Type2 == 0x04) //Equipped, quest item or adena
                {
                    //Do nothing
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

                    listView_privatestoresell_inv.Items.Add(ObjListItem);
                }
            }
        }

        private void listView_privatestoresell_inv_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                string item = listView_privatestoresell_inv.SelectedItems[0].SubItems[0].Text;
                int loc = listView_privatestoresell_inv.SelectedItems[0].Index;
                uint objID = System.Convert.ToUInt32(listView_privatestoresell_inv.SelectedItems[0].SubItems[2].Text); //Obj ID
                ulong invquantity = System.Convert.ToUInt64(listView_privatestoresell_inv.SelectedItems[0].SubItems[1].Text); //Number of items in invetory
                ulong sellquantity = System.Convert.ToUInt64(textBox_privatestoresell_quantity.Text); //Number of items to sell
                ulong price = System.Convert.ToUInt64(textBox_privatestoresell_price.Text);
                string message = textBox_privatestoresell_message.Text;

                if (sellquantity > invquantity)
                {
                    sellquantity = invquantity; //If user is trying to put more items in trade window than he has in inventory, set invquantity to max
                }
                if (sellquantity < 1) //If user is trying to sell less than 1 item, do nothing.
                {
                    return;
                }

                if (invquantity == sellquantity) //Sell all
                {
                    listView_privatestoresell_inv.Items.RemoveAt(loc);
                }
                else //sell some
                {
                    listView_privatestoresell_inv.SelectedItems[0].SubItems[1].Text = (invquantity - sellquantity).ToString();
                }

                System.Windows.Forms.ListViewItem ObjListItem;
                ObjListItem = new ListViewItem(item);
                ObjListItem.SubItems.Add(sellquantity.ToString());
                ObjListItem.SubItems.Add(price.ToString());
                ObjListItem.SubItems.Add(objID.ToString());
                listView_privatestoresell_ItemsOnSale.Items.Add(ObjListItem);
            }
            catch
            {
                Globals.l2net_home.Add_Error("Failed to add item to private store, are any of the textboxes empty?");
            }
        }

        private void textBox_privatestoresell_quantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+"))&&(!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\b" )))
                e.Handled = true;

        }

        private void textBox_privatestoresell_price_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+")) && (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\b")))
                e.Handled = true;
        }

        private void button__privatestoresell_start_Click(object sender, EventArgs e)
        {
            Send_SetPrivateStoreMsgSell();
            Send_PrivateStoreSellList();
            ServerPackets.Send_Verify(); //Validate position
            //ServerPackets.FinishRotating(); //TODO
            this.Close();
        }

        private void Send_SetPrivateStoreMsgSell()
        {
            ByteBuffer bbuff = new ByteBuffer(512);
            bbuff.WriteByte((byte)PClient.SetPrivateStoreMsgSell);
            bbuff.WriteString(textBox_privatestoresell_message.Text);
            bbuff.WriteByte(0x00);
            bbuff.TrimToIndex();
            Globals.gamedata.SendToGameServer(bbuff);
        }

        private void Send_PrivateStoreSellList()
        {
            /*  31 //SetPrivateStoreListSell
                00 00 00 00 
                01 00 00 00 //Number of different items
                64 0C 05 10 //Unique ID
                01 00 00 00 00 00 00 00 //quantity
                0A 00 00 00 00 00 00 00 //price  */
            uint NumberOfItems = System.Convert.ToUInt32(listView_privatestoresell_ItemsOnSale.Items.Count);

            ByteBuffer bbuff = new ByteBuffer(512);

            bbuff.WriteByte((byte)PClient.SetPrivateStoreListSell);
            bbuff.WriteUInt32(0x00);
            bbuff.WriteUInt32(NumberOfItems);
            for (int i = 0; i < NumberOfItems; i++)
            {
                bbuff.WriteUInt32(System.Convert.ToUInt32(listView_privatestoresell_ItemsOnSale.Items[i].SubItems[3].Text)); //ObjID
                if (Globals.gamedata.Chron >= Chronicle.CT2_3)
                {
                    bbuff.WriteUInt64(System.Convert.ToUInt64(listView_privatestoresell_ItemsOnSale.Items[i].SubItems[1].Text)); //Quantity
                    bbuff.WriteUInt64(System.Convert.ToUInt64(listView_privatestoresell_ItemsOnSale.Items[i].SubItems[2].Text)); //Price
                }
                else
                {
                    bbuff.WriteUInt32(System.Convert.ToUInt32(listView_privatestoresell_ItemsOnSale.Items[i].SubItems[1].Text)); //Quantity
                    bbuff.WriteUInt32(System.Convert.ToUInt32(listView_privatestoresell_ItemsOnSale.Items[i].SubItems[2].Text)); //Price
                }
            }
            bbuff.TrimToIndex();
            Globals.gamedata.SendToGameServer(bbuff);
        }
    }
}
