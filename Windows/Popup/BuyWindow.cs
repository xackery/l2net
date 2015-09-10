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
    public partial class BuyWindow : Form
    {
        public BuyWindow()
        {
            InitializeComponent();
            FillBuyList();

        }
        /*40 PClient.RequestBuyItem
         *7C 91 2E 00 //List ID
         *01 00 00 00 //Number of different items
         *6B 0F 00 00 //Item ID
         *02 00 00 00 //Count
         *00 00 00 00 */
        /*40
         * 7C 91 2E 00
         * 02 00 00 00
         * 
         * 2B 07 00 00 
         * 01 00 00 00
         * 00 00 00 00 
         * CD 09 00 00 //itemID
         * 01 00 00 00
         * 00 00 00 00 */
        //Buy ngss and ngsps
        /*40
         * 11 00 00 00 //listview ID feil
         * 02 00 00 00 
         * 11 00 00 00 //Wooden arrow
         * 01 00 00 00 rett
         * 00 00 00 00 rett
         * 2B 07 00 00rett 
         * 02 00 00 00 rett 
         * 00 00 00 00 */




        private void button_trade_cancel_Click(object sender, EventArgs e)
        {
            this.Close();

        }


        private void button_trade_confirm_Click(object sender, EventArgs e)
        {

            uint itms = System.Convert.ToUInt32(listView_buylist_selected_items.Items.Count);
            ByteBuffer buff = new ByteBuffer();

            buff.WriteByte((byte)PClient.RequestBuyItem);
            buff.WriteUInt32(Globals.gamedata.blistID); //7C 91 2E 00 //List ID
            buff.WriteUInt32(itms); //01 00 00 00 //Number of different items
            for (int i = 0; i < itms; i++)
            {
                string tmp = listView_buylist_selected_items.Items[i].SubItems[2].Text;
                buff.WriteUInt32(System.Convert.ToUInt32(tmp)); //Item ID
                buff.WriteUInt32(System.Convert.ToUInt32(listView_buylist_selected_items.Items[i].SubItems[1].Text)); //Number of items to buy
                buff.WriteUInt32(0);
            }
            buff.TrimToIndex();
            Globals.gamedata.SendToGameServer(buff);
            this.Close();
        }

        private void FillBuyList()
        {
            //add
            System.Windows.Forms.ListViewItem ObjListItem;
            foreach (BuyList b_list in Globals.gamedata.buylist.Values)
            {
                ObjListItem = new ListViewItem(Util.GetItemName(b_list.ItemID));
                ObjListItem.SubItems.Add(b_list.Price.ToString()); //Price
                ObjListItem.SubItems.Add(b_list.ItemID.ToString()); //Item ID
                //ObjListItem.SubItems.Add(b_list.ItemID.ToString()); //Buylist ID
                listView_buylist.Items.Add(ObjListItem);
            }
        }

        private void listView_buylist_DoubleClick(object sender, EventArgs e)
        {
            int loc = listView_buylist.SelectedItems[0].Index; //Location of the item we are buying
            string item = listView_buylist.SelectedItems[0].SubItems[0].Text;
            uint itemID = System.Convert.ToUInt32(listView_buylist.SelectedItems[0].SubItems[2].Text);
            //uint blistID = System.Convert.ToUInt32(listView_buylist.SelectedItems[0].SubItems[3].Text);
            uint quantity = System.Convert.ToUInt32(textBox_buywindow_quantity.Text);

            System.Windows.Forms.ListViewItem ObjListItem;
            ObjListItem = new ListViewItem(item);
            ObjListItem.SubItems.Add(quantity.ToString());
            ObjListItem.SubItems.Add(itemID.ToString());
            //ObjListItem.SubItems.Add(blistID.ToString());
            listView_buylist_selected_items.Items.Add(ObjListItem);

        }

    }

}
