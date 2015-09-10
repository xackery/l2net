using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace L2_login
{
    public partial class L2NET
    {
        public static void timer_chat_Tick()
        {
            Globals.l2net_home.timer_chat.Stop();

            Globals.l2net_home.UpdateChatWindow();
        }

        public static void timer_inventory_Tick()
        {
            //need to update the inventory list
            Globals.l2net_home.timer_inventory.Stop();

            Globals.l2net_home.UpdateInventoryList();
        }

        public static void timer_npcs_Tick()
        {
            //need to update the npcs list
            Globals.l2net_home.timer_npcs.Stop();

            Globals.l2net_home.UpdateNPCsList();
        }

        public static void timer_items_Tick()
        {
            //need to update the items list
            Globals.l2net_home.timer_items.Stop();

            Globals.l2net_home.UpdateItemsList();
        }

        public static void timer_players_Tick()
        {
            //need to update the players list
            Globals.l2net_home.timer_players.Stop();

            Globals.l2net_home.UpdatePlayersList();
        }

        public static void timer_mybuffs_Tick()
        {
            //need to update the buff list
            //Globals.l2net_home.timer_mybuffs.Stop();

            Globals.l2net_home.UpdateMyBuffsList();
        }

        /***********************************************/

        void listView_players_data_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            try
            {
                e.Item = (ListViewItem)listView_players_data_items[e.ItemIndex];
                return;
            }
            catch
            {
                e.Item = new ListViewItem();
                e.Item.SubItems.Add("");
                e.Item.SubItems.Add("");
                e.Item.SubItems.Add("");
                e.Item.SubItems.Add("");
                e.Item.SubItems.Add("");
                e.Item.ImageIndex = -1; 
            }
        }

        private void UpdatePlayersList()
        {
            try
            {
                listView_players_data.BeginUpdate();

                Globals.PlayerLock.EnterReadLock();
                try
                {
                    UpdatePlayerListInternal();
                }
                finally
                {
                    Globals.PlayerLock.ExitReadLock();
                }

                listView_players_data.VirtualListSize = listView_players_data_items.Count;
                Util.Sort(listView_players_data_items, lvwColumnSorter_players_data);
                //listView_players_data_items.Sort(lvwColumnSorter_players_data);
            }
            catch
            {
                //failed to acquire the lock...
                this.timer_players.Start();
            }
            finally
            {
                listView_players_data.EndUpdate();
                listView_players_data.Refresh();
            }
        }

        private void UpdatePlayerListInternal()
        {
            bool Active = true;

            float x = Globals.gamedata.my_char.X;
            float y = Globals.gamedata.my_char.Y;
            float z = Globals.gamedata.my_char.Z;

            System.Collections.ArrayList dirty_items = new System.Collections.ArrayList();

            for (int i = 0; i < listView_players_data_items.Count; i++)
            {
                uint id = Util.GetUInt32(((ListViewItem)listView_players_data_items[i]).SubItems[5].Text);

                if (Globals.gamedata.nearby_chars.ContainsKey(id))
                {
                    CharInfo player = Util.GetChar(id);

                    player.InList = true;

                    //update it
                    ((ListViewItem)listView_players_data_items[i]).SubItems[0].Text = player.WarState.ToString();
                    //((ListViewItem)listView_players_data_items[i]).SubItems[1].Text = player.Name;
                    if (Active)
                    {
                        ((ListViewItem)listView_players_data_items[i]).SubItems[2].Text = Util.GetClass(player.Class) + (player.isAlikeDead == 0x00 ? " :A: " : " :D: ") + ((int)Util.Distance(x, y, z, player.X, player.Y, player.Z)).ToString("0000");
                    }
                    else
                    {
                        ((ListViewItem)listView_players_data_items[i]).SubItems[2].Text = Util.GetClass(player.Class);
                    }
                    ((ListViewItem)listView_players_data_items[i]).SubItems[3].Text = player.ClanName;//clan
                    ((ListViewItem)listView_players_data_items[i]).SubItems[4].Text = player.AllyName;//ally
                    //((ListViewItem)listView_players_data_items[i]).SubItems[5].Text = player.ID.ToString();//didnt change

                    ((ListViewItem)listView_players_data_items[i]).ImageIndex = player.ClanCrestIndex;
                }
                else
                {
                    dirty_items.Add(i);
                }
            }

            //need to remove all dirty items now
            for (int i = dirty_items.Count - 1; i >= 0; i--)
            {
                listView_players_data_items.RemoveAt((int)dirty_items[i]);
            }
            dirty_items.Clear();

            foreach (CharInfo player in Globals.gamedata.nearby_chars.Values)
            {
                //find the item in our arraylist...

                if (!player.InList)
                {
                    player.InList = true;

                    //add it
                    System.Windows.Forms.ListViewItem ObjListItem;
                    ObjListItem = new ListViewItem(player.WarState.ToString());//war
                    ObjListItem.SubItems.Add(player.Name);//name
                    if (Active)
                    {
                        ObjListItem.SubItems.Add(Util.GetClass(player.Class) + (player.isAlikeDead == 0x00 ? " :A: " : " :D: ") + ((int)Util.Distance(x, y, z, player.X, player.Y, player.Z)).ToString("0000"));//class
                    }
                    else
                    {
                        ObjListItem.SubItems.Add(Util.GetClass(player.Class));//class
                    }
                    ObjListItem.SubItems.Add(player.ClanName);//clan
                    ObjListItem.SubItems.Add(player.AllyName);//ally
                    ObjListItem.SubItems.Add(player.ID.ToString());//ID

                    //UpdateClanInfo(ObjListItem);
                    if (player.ClanCrestIndex != 0)
                    {
                        ObjListItem.ImageIndex = player.ClanCrestIndex;
                    }
                    else
                    {
                        ObjListItem.ImageIndex = -1;
                    }

                    //oop cache
                    if (Globals.gamedata.botoptions.OOPNamesArray.Contains(player.Name.ToUpperInvariant()))
                    {
                        if (!Globals.gamedata.botoptions.OOPIDs.Contains(player.ID))
                        {
                            Globals.gamedata.botoptions.OOPIDs.Add(player.ID);
                        }
                    }

                    listView_players_data_items.Add(ObjListItem);
                }
            }
        }

        /***********************************************/

        void listView_items_data_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            try
            {
                e.Item = (ListViewItem)listView_items_data_items[e.ItemIndex];
                return;
            }
            catch
            {
                e.Item = new ListViewItem();
                e.Item.SubItems.Add("");
                e.Item.SubItems.Add("");
                e.Item.SubItems.Add("");
                e.Item.ImageIndex = -1;
            }
        }

        private void UpdateItemsList()
        {
            try
            {
                listView_items_data.BeginUpdate();

                Globals.ItemLock.EnterReadLock();
                try
                {
                    UpdateItemsListInternal();
                }
                finally
                {
                    Globals.ItemLock.ExitReadLock();
                }

                listView_items_data.VirtualListSize = listView_items_data_items.Count;
                Util.Sort(listView_items_data_items, lvwColumnSorter_item_data);
                //listView_items_data_items.Sort(lvwColumnSorter_item_data);
            }
            catch
            {
                //failed to acquire the lock...
                this.timer_items.Start();
            }
            finally
            {
                listView_items_data.EndUpdate();
                listView_items_data.Refresh();
            }
        }

        private void UpdateItemsListInternal()
        {
            System.Collections.ArrayList dirty_items = new System.Collections.ArrayList();

            for (int i = 0; i < listView_items_data_items.Count; i++)
            {
                uint id = Util.GetUInt32(((ListViewItem)listView_items_data_items[i]).SubItems[2].Text);

                if (Globals.gamedata.nearby_items.ContainsKey(id))
                {
                    ItemInfo item = Util.GetItem(id);

                    item.InList = true;

                    //update it
                    //((ListViewItem)listView_items_data_items[i]).SubItems[0].Text = Util.GetItemName(item.ItemID);
                    //((ListViewItem)listView_items_data_items[i]).SubItems[1].Text = item.Count.ToString();
                    //((ListViewItem)listView_items_data_items[i]).SubItems[2].Text = item.ID.ToString();
                }
                else
                {
                    dirty_items.Add(i);
                }
            }

            //need to remove all dirty items now
            for (int i = dirty_items.Count - 1; i >= 0; i--)
            {
                listView_items_data_items.RemoveAt((int)dirty_items[i]);
            }
            dirty_items.Clear();

            foreach (ItemInfo item in Globals.gamedata.nearby_items.Values)
            {
                if (!item.InList)
                {
                    item.InList = true;

                    string mesh_info = "";
                    if (!item.HasMesh)
                    {
                        mesh_info = " [NO MESH]";
                    }

                    //add it
                    System.Windows.Forms.ListViewItem ObjListItem;
                    ObjListItem = new ListViewItem(Util.GetItemName(item.ItemID) + mesh_info);//ItmID
                    ObjListItem.SubItems.Add(item.Count.ToString());//Count
                    ObjListItem.SubItems.Add(item.ID.ToString());//ObjID
                    ObjListItem.ImageIndex = AddInfo.Get_Item_Image_Index(item.ItemID);

                    listView_items_data_items.Add(ObjListItem);
                }
            }
        }

        /***********************************************/

        void listView_npc_data_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            try
            {
                e.Item = (ListViewItem)listView_npc_data_items[e.ItemIndex];
                return;
            }
            catch
            {
                e.Item = new ListViewItem();
                e.Item.SubItems.Add("");
                e.Item.SubItems.Add("");
                e.Item.SubItems.Add("");
                e.Item.SubItems.Add("");
                e.Item.ImageIndex = -1;
            } 
        }

        private void UpdateNPCsList()
        {
            try
            {
                listView_npc_data.BeginUpdate();

                Globals.NPCLock.EnterReadLock();
                try
                {
                    UpdateNPCListInternal();
                }
                finally
                {
                    Globals.NPCLock.ExitReadLock();
                }

                listView_npc_data.VirtualListSize = listView_npc_data_items.Count;
                Util.Sort(listView_npc_data_items, lvwColumnSorter_npc_data);
                //listView_npc_data_items.Sort(lvwColumnSorter_npc_data);
            }
            catch
            {
                //failed to acquire the lock...
                this.timer_npcs.Start();
            }
            finally
            {
                listView_npc_data.EndUpdate();
                listView_npc_data.Refresh();
            }
        }

        private void UpdateNPCListInternal()
        {
            System.Collections.ArrayList dirty_items = new System.Collections.ArrayList();

            for (int i = 0; i < listView_npc_data.Items.Count; i++)
            {
                uint id = Util.GetUInt32(((ListViewItem)listView_npc_data_items[i]).SubItems[2].Text);

                if (Globals.gamedata.nearby_npcs.ContainsKey(id))
                {
                    NPCInfo npc = Util.GetNPC(id);

                    npc.InList = true;

                    //update it
                    //((ListViewItem)listView_npc_data_items[i]).SubItems[0].Text = Util.GetNPCName(npc.NPCID);
                    //((ListViewItem)listView_npc_data_items[i]).SubItems[1].Text = npc.Title;
                    //((ListViewItem)listView_npc_data_items[i]).SubItems[2].Text = npc.ID.ToString();
                    //((ListViewItem)listView_npc_data_items[i]).SubItems[3].Text = npc.NPCID.ToString();
                }
                else
                {
                    dirty_items.Add(i);
                }
            }

            //need to remove all dirty items now
            for (int i = dirty_items.Count - 1; i >= 0; i--)
            {
                listView_npc_data_items.RemoveAt((int)dirty_items[i]);
            }
            dirty_items.Clear();

            foreach (NPCInfo npc in Globals.gamedata.nearby_npcs.Values)
            {
                if (!npc.InList)
                {
                    npc.InList = true;
                    if (npc.isInvisible != 1)
                    {
                        //add it
                        System.Windows.Forms.ListViewItem ObjListItem;
                        ObjListItem = new ListViewItem(Util.GetNPCName(npc.NPCID));//Name
                        ObjListItem.SubItems.Add(npc.Title);//Title
                        ObjListItem.SubItems.Add(npc.ID.ToString());//ObjID
                        ObjListItem.SubItems.Add(npc.NPCID.ToString());//TypeID

                        listView_npc_data_items.Add(ObjListItem);
                    }
                }
            }
        }

        /***********************************************/

        void listView_inventory_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            try
            {
                e.Item = (ListViewItem)listView_inventory_items[e.ItemIndex];
                return;
            }
            catch
            {
                e.Item = new ListViewItem();
                e.Item.SubItems.Add("");
                e.Item.SubItems.Add("");
                e.Item.SubItems.Add("");
                e.Item.SubItems.Add("");
                e.Item.SubItems.Add("");
                e.Item.ImageIndex = -1;
            } 
        }

        private void UpdateInventoryList()
        {
            //Clear_Equip();

            try
            {
                listView_inventory.BeginUpdate();

                Globals.InventoryLock.EnterReadLock();
                try
                {
                    UpdateInventoryListInternal();
                }
                finally
                {
                    Globals.InventoryLock.ExitReadLock();
                }

                listView_inventory.VirtualListSize = listView_inventory_items.Count;
                Util.Sort(listView_inventory_items, lvwColumnSorter_inventory);
                //listView_inventory_items.Sort(lvwColumnSorter_inventory);
            }
            catch
            {
                //failed to acquire the lock...
                this.timer_inventory.Start();
            }
            finally
            {
                listView_inventory.EndUpdate();
                listView_inventory.Refresh();

                label_inventory_count.Text = listView_inventory.Items.Count.ToString() + "/" + Globals.gamedata.my_char.InventoryLimit.ToString();
            }
        }

        private void UpdateInventoryListInternal()
        {
            System.Collections.ArrayList dirty_items = new System.Collections.ArrayList();
            #region Equipped Items
            if (radioButton_inv_equipped.Checked)
            {
                for (int i = 0; i < listView_inventory_items.Count; i++)
                {
                    uint id = Util.GetUInt32(((ListViewItem)listView_inventory_items[i]).SubItems[4].Text);

                    if (Globals.gamedata.inventory.ContainsKey(id))
                    {
                        //already in the list...
                        InventoryInfo inv_inf = Util.GetInventory(id);
                        if (inv_inf.isEquipped == 0x01)
                        {
                            inv_inf.InList = true;

                            //update the entry
                            if (inv_inf.Enchant == 0)
                            {
                                ((ListViewItem)listView_inventory_items[i]).SubItems[0].Text = Util.GetItemName(inv_inf.ItemID);
                            }
                            else
                            {
                                ((ListViewItem)listView_inventory_items[i]).SubItems[0].Text = "+" + inv_inf.Enchant.ToString() + " " + Util.GetItemName(inv_inf.ItemID);
                            }
                            ((ListViewItem)listView_inventory_items[i]).SubItems[1].Text = inv_inf.Count.ToString();//Count
                            if (inv_inf.isEquipped == 0x01)//isEquipped
                            {
                                ((ListViewItem)listView_inventory_items[i]).SubItems[2].Text = "X";
                            }
                            else
                            {
                                ((ListViewItem)listView_inventory_items[i]).SubItems[2].Text = " ";
                            }
                            //((ListViewItem)listView_inventory_items[i]).SubItems[3].Text = inv_inf.Slot.ToString();//Slot
                        }
                        else
                        {
                            //not in the list...
                            //delete this item
                            dirty_items.Add(i);
                        }
                    }
                    else
                    {
                        //not in the list...
                        //delete this item
                        dirty_items.Add(i);
                    }
                }

                //need to remove all dirty items now
                for (int i = dirty_items.Count - 1; i >= 0; i--)
                {
                    listView_inventory_items.RemoveAt((int)dirty_items[i]);
                }
                dirty_items.Clear();

                foreach (InventoryInfo inv_inf in Globals.gamedata.inventory.Values)
                {
                    if (!inv_inf.InList && inv_inf.isEquipped == 0x01)
                    {
                        inv_inf.InList = true;

                        //add it
                        System.Windows.Forms.ListViewItem ObjListItem;

                        if (inv_inf.Enchant == 0)
                        {
                            ObjListItem = new ListViewItem(Util.GetItemName(inv_inf.ItemID));
                        }
                        else
                        {
                            ObjListItem = new ListViewItem("+" + inv_inf.Enchant.ToString() + " " + Util.GetItemName(inv_inf.ItemID));
                        }
                        ObjListItem.SubItems.Add(inv_inf.Count.ToString());//Count
                        if (inv_inf.isEquipped == 0x01)//isEquipped
                        {
                            ObjListItem.SubItems.Add("X");
                            //Do_Equip(inv_inf);
                        }
                        else
                        {
                            ObjListItem.SubItems.Add(" ");
                        }
                        ObjListItem.SubItems.Add(inv_inf.Slot.ToString());//Slot
                        ObjListItem.SubItems.Add(inv_inf.ID.ToString());//ObjID
                        ObjListItem.ImageIndex = AddInfo.Get_Item_Image_Index(inv_inf.ItemID);

                        listView_inventory_items.Add(ObjListItem);
                    }
                }
            }
            #endregion

            #region Normal Items
            if (radioButton_inv_items.Checked)
            {
                for (int i = 0; i < listView_inventory_items.Count; i++)
                {
                    uint id = Util.GetUInt32(((ListViewItem)listView_inventory_items[i]).SubItems[4].Text);

                    if (Globals.gamedata.inventory.ContainsKey(id))
                    {
                        //already in the list...
                        InventoryInfo inv_inf = Util.GetInventory(id);
                        if (inv_inf.isEquipped != 0x01 && inv_inf.Type2 != 0x03)
                        {
                            inv_inf.InList = true;

                            //update the entry
                            if (inv_inf.Enchant == 0)
                            {
                                ((ListViewItem)listView_inventory_items[i]).SubItems[0].Text = Util.GetItemName(inv_inf.ItemID);
                            }
                            else
                            {
                                ((ListViewItem)listView_inventory_items[i]).SubItems[0].Text = "+" + inv_inf.Enchant.ToString() + " " + Util.GetItemName(inv_inf.ItemID);
                            }
                            ((ListViewItem)listView_inventory_items[i]).SubItems[1].Text = inv_inf.Count.ToString();//Count
                            if (inv_inf.isEquipped == 0x01)//isEquipped
                            {
                                ((ListViewItem)listView_inventory_items[i]).SubItems[2].Text = "X";
                            }
                            else
                            {
                                ((ListViewItem)listView_inventory_items[i]).SubItems[2].Text = " ";
                            }
                            //((ListViewItem)listView_inventory_items[i]).SubItems[3].Text = inv_inf.Slot.ToString();//Slot
                        }
                        else
                        {
                            //not in the list...
                            //delete this item
                            dirty_items.Add(i);
                        }
                    }
                    else
                    {
                        //not in the list...
                        //delete this item
                        dirty_items.Add(i);
                    }
                }

                //need to remove all dirty items now
                for (int i = dirty_items.Count - 1; i >= 0; i--)
                {
                    listView_inventory_items.RemoveAt((int)dirty_items[i]);
                }
                dirty_items.Clear();

                foreach (InventoryInfo inv_inf in Globals.gamedata.inventory.Values)
                {
                    if (!inv_inf.InList && inv_inf.isEquipped != 0x01 && inv_inf.Type2 != 0x03)
                    {
                        inv_inf.InList = true;

                        //add it
                        System.Windows.Forms.ListViewItem ObjListItem;

                        if (inv_inf.Enchant == 0)
                        {
                            ObjListItem = new ListViewItem(Util.GetItemName(inv_inf.ItemID));
                        }
                        else
                        {
                            ObjListItem = new ListViewItem("+" + inv_inf.Enchant.ToString() + " " + Util.GetItemName(inv_inf.ItemID));
                        }
                        ObjListItem.SubItems.Add(inv_inf.Count.ToString());//Count
                        if (inv_inf.isEquipped == 0x01)//isEquipped
                        {
                            ObjListItem.SubItems.Add("X");
                            //Do_Equip(inv_inf);
                        }
                        else
                        {
                            ObjListItem.SubItems.Add(" ");
                        }
                        ObjListItem.SubItems.Add(inv_inf.Slot.ToString());//Slot
                        ObjListItem.SubItems.Add(inv_inf.ID.ToString());//ObjID
                        ObjListItem.ImageIndex = AddInfo.Get_Item_Image_Index(inv_inf.ItemID);

                        listView_inventory_items.Add(ObjListItem);
                    }
                }
            }
            #endregion

            #region Quest Items
            if (radioButton_inv_quest.Checked)
            {
                for (int i = 0; i < listView_inventory_items.Count; i++)
                {
                    uint id = Util.GetUInt32(((ListViewItem)listView_inventory_items[i]).SubItems[4].Text);

                    if (Globals.gamedata.inventory.ContainsKey(id))
                    {
                        //already in the list...
                        InventoryInfo inv_inf = Util.GetInventory(id);
                        if (inv_inf.Type2 == 0x03)
                        {
                            inv_inf.InList = true;

                            //update the entry
                            if (inv_inf.Enchant == 0)
                            {
                                ((ListViewItem)listView_inventory_items[i]).SubItems[0].Text = Util.GetItemName(inv_inf.ItemID);
                            }
                            else
                            {
                                ((ListViewItem)listView_inventory_items[i]).SubItems[0].Text = "+" + inv_inf.Enchant.ToString() + " " + Util.GetItemName(inv_inf.ItemID);
                            }
                            ((ListViewItem)listView_inventory_items[i]).SubItems[1].Text = inv_inf.Count.ToString();//Count
                            if (inv_inf.isEquipped == 0x01)//isEquipped
                            {
                                ((ListViewItem)listView_inventory_items[i]).SubItems[2].Text = "X";
                            }
                            else
                            {
                                ((ListViewItem)listView_inventory_items[i]).SubItems[2].Text = " ";
                            }
                            //((ListViewItem)listView_inventory_items[i]).SubItems[3].Text = inv_inf.Slot.ToString();//Slot
                        }
                        else
                        {
                            //not in the list...
                            //delete this item
                            dirty_items.Add(i);
                        }
                    }
                    else
                    {
                        //not in the list...
                        //delete this item
                        dirty_items.Add(i);
                    }
                }

                //need to remove all dirty items now
                for (int i = dirty_items.Count - 1; i >= 0; i--)
                {
                    listView_inventory_items.RemoveAt((int)dirty_items[i]);
                }
                dirty_items.Clear();

                foreach (InventoryInfo inv_inf in Globals.gamedata.inventory.Values)
                {
                    if (!inv_inf.InList && inv_inf.Type2 == 0x03)
                    {
                        inv_inf.InList = true;

                        //add it
                        System.Windows.Forms.ListViewItem ObjListItem;

                        if (inv_inf.Enchant == 0)
                        {
                            ObjListItem = new ListViewItem(Util.GetItemName(inv_inf.ItemID));
                        }
                        else
                        {
                            ObjListItem = new ListViewItem("+" + inv_inf.Enchant.ToString() + " " + Util.GetItemName(inv_inf.ItemID));
                        }
                        ObjListItem.SubItems.Add(inv_inf.Count.ToString());//Count
                        if (inv_inf.isEquipped == 0x01)//isEquipped
                        {
                            ObjListItem.SubItems.Add("X");
                            //Do_Equip(inv_inf);
                        }
                        else
                        {
                            ObjListItem.SubItems.Add(" ");
                        }
                        ObjListItem.SubItems.Add(inv_inf.Slot.ToString());//Slot
                        ObjListItem.SubItems.Add(inv_inf.ID.ToString());//ObjID
                        ObjListItem.ImageIndex = AddInfo.Get_Item_Image_Index(inv_inf.ItemID);

                        listView_inventory_items.Add(ObjListItem);
                    }
                }
            }
            #endregion
        }

        /***********************************************/

        void listView_mybuffs_data_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            try
            {
                e.Item = (ListViewItem)listView_mybuffs_data_items[e.ItemIndex];
                return;
            }
            catch
            {
                e.Item = new ListViewItem();
                e.Item.SubItems.Add("");
                e.Item.SubItems.Add("");
                e.Item.SubItems.Add("");
                e.Item.SubItems.Add("");
                e.Item.ImageIndex = -1;
            }
        }

        private void UpdateMyBuffsList()
        {
            try
            {
                listView_mybuffs_data.BeginUpdate();

                Globals.MyBuffsListLock.EnterReadLock();
                try
                {
                    UpdateMyBuffsListInternal();
                }
                finally
                {
                    Globals.MyBuffsListLock.ExitReadLock();
                }

                listView_mybuffs_data.VirtualListSize = listView_mybuffs_data_items.Count;
                Util.Sort(listView_mybuffs_data_items, lvwColumnSorter_mybuffs_data);
                //listView_mybuffs_data_items.Sort(lvwColumnSorter_mybuffs_data);
            }
            catch
            {
                //failed to acquire the lock...
                this.timer_mybuffs.Start();
            }
            finally
            {
                listView_mybuffs_data.EndUpdate();
                listView_mybuffs_data.Refresh();
            }
        }

        private void UpdateMyBuffsListInternal()
        {
            System.Collections.ArrayList dirty_items = new System.Collections.ArrayList();

            for (int i = 0; i < listView_mybuffs_data.Items.Count; i++)
            {
                uint id = Util.GetUInt32(((ListViewItem)listView_mybuffs_data_items[i]).SubItems[3].Text);

                if (Globals.gamedata.mybuffs.ContainsKey(id))
                {
                    CharBuff cb = Util.GetBuff(id);

                    cb.InList = true;

                    if (cb.ExpiresTime == -1)
                    {
                        ((ListViewItem)listView_mybuffs_data_items[i]).SubItems[2].Text = "ON";
                    }
                    else
                    {
                        System.TimeSpan remain = new System.TimeSpan(cb.ExpiresTime - System.DateTime.Now.Ticks);

                        //update it
                        //((ListViewItem)listView_npc_data_items[i]).SubItems[0].Text = Util.GetNPCName(npc.NPCID);
                        //((ListViewItem)listView_npc_data_items[i]).SubItems[1].Text = npc.Title;
                        ((ListViewItem)listView_mybuffs_data_items[i]).SubItems[2].Text = ((int)remain.TotalMinutes).ToString() + ":" + remain.Seconds.ToString();
                        //((ListViewItem)listView_mybuffs_data_items[i]).SubItems[2].Text = cb.ID.ToString();
                    }
                }
                else
                {
                    dirty_items.Add(i);
                }
            }

            //need to remove all dirty items now
            for (int i = dirty_items.Count - 1; i >= 0; i--)
            {
                listView_mybuffs_data_items.RemoveAt((int)dirty_items[i]);
            }
            dirty_items.Clear();

            foreach (CharBuff cb in Globals.gamedata.mybuffs.Values)
            {
                if (!cb.InList)
                {
                    cb.InList = true;

                    System.TimeSpan remain = new System.TimeSpan(0);
                    if (cb.ExpiresTime == -1)
                    {

                    }
                    else
                    {
                        remain = new System.TimeSpan(cb.ExpiresTime - System.DateTime.Now.Ticks);
                    }

                    //add it
                    System.Windows.Forms.ListViewItem ObjListItem;
                    ObjListItem = new ListViewItem(Util.GetSkillName(cb.ID,cb.SkillLevel));//Name
                    ObjListItem.SubItems.Add(cb.SkillLevel.ToString());//Title
                    if (cb.ExpiresTime == -1)
                    {
                        ObjListItem.SubItems.Add("ON");
                    }
                    else
                    {
                        ObjListItem.SubItems.Add(((int)remain.TotalMinutes).ToString() + ":" + remain.Seconds.ToString());//Remaining Time
                    }
                    ObjListItem.SubItems.Add(cb.ID.ToString());//ObjID
                    ObjListItem.ImageIndex = AddInfo.Get_Skill_Image_Index(cb.ID);

                    listView_mybuffs_data_items.Add(ObjListItem);
                }
            }
        }

        /////
    }
}
