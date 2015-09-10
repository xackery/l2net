using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace L2_login
{
    public partial class L2NET
    {
        private void listView_inventory_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = "";

            if (listView_inventory.SelectedIndices.Count > 0)
            {
                uint id = Util.GetUInt32(listView_inventory.Items[listView_inventory.SelectedIndices[0]].SubItems[4].Text);
                InventoryInfo item = null;

                if (Globals.InventoryLock.TryEnterReadLock(Globals.THREAD_WAIT_GUI))
                {
                    try
                    {
                        item = Util.GetInventory(id);
                    }
                    finally
                    {
                        Globals.InventoryLock.ExitReadLock();
                    }
                }

                if (item != null)
                {
                    text = (item.Enchant == 0 ? "" : "+" + item.Enchant.ToString() + " ") + Util.GetItemName(item.ItemID) + Environment.NewLine +
                        (item.isEquipped == 0x01 ? "Equipped" + Environment.NewLine : "") +
                        "Count: " + item.Count.ToString() + Environment.NewLine +
                        "Augment: " + item.AugID.ToString() + Environment.NewLine +
                        "Mana: " + item.Mana.ToString() + Environment.NewLine +
                        "Slot: " + item.Slot.ToString() + Environment.NewLine +
                        "Type 1: " + item.Type.ToString() + Environment.NewLine +
                        "Type 2: " + item.Type2.ToString() + Environment.NewLine +
                        "Type 3: " + item.Type3.ToString() + Environment.NewLine +
                        "Type 4: " + item.Type4.ToString() + Environment.NewLine +
                        "Type ID: " + item.ItemID + Environment.NewLine +
                        "Unique ID: " + item.ID;
                }
            }

            toolTip1.SetToolTip(listView_inventory, text);
        }

        private void listView_npc_data_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = "";

            if (listView_npc_data.SelectedIndices.Count > 0)
            {
                //action to this item
                uint id = Util.GetUInt32(listView_npc_data.Items[listView_npc_data.SelectedIndices[0]].SubItems[2].Text);
                NPCInfo npc = null;

                if (Globals.NPCLock.TryEnterReadLock(Globals.THREAD_WAIT_GUI))
                {
                    try
                    {
                        npc = Util.GetNPC(id);
                    }
                    finally
                    {
                        Globals.NPCLock.ExitReadLock();
                    }
                }

                if (npc != null)
                {
                    text = npc.Name + Environment.NewLine +
                        npc.Title + Environment.NewLine +
                        (npc.isAttackable == 0x00 ? "Invincible" : "Attackable") + Environment.NewLine +
                        "Cast Speed: " + npc.MatkSpeed.ToString() + Environment.NewLine +
                        "Attack Speed: " + (npc.AttackSpeedMult * npc.PatkSpeed).ToString() + Environment.NewLine +
                        "Run Speed: " + (npc.RunSpeed * npc.MoveSpeedMult).ToString() + Environment.NewLine +
                        "R: " + Util.GetItemName(npc.RHand) + Environment.NewLine +
                        "LR: " + Util.GetItemName(npc.LRHand) + Environment.NewLine +
                        "L: " + Util.GetItemName(npc.LHand) + Environment.NewLine +
                        (npc.isAlikeDead == 0x00 ? "Alive" : "Dead") + Environment.NewLine +
                        /*(npc.isInvisible == 0x00 ? "Spawned" : "Summoned") + Environment.NewLine +*/
                        "Karma: " + npc.Karma.ToString() + Environment.NewLine +
                        "Abnormal Effects: " + npc.AbnormalEffects.ToString() + Environment.NewLine +
                        "X: " + npc.X.ToString() + Environment.NewLine +
                        "Y: " + npc.Y.ToString() + Environment.NewLine +
                        "Z: " + npc.Z.ToString() + Environment.NewLine +
                        "Dist: " + Util.Distance(Globals.gamedata.my_char.X, Globals.gamedata.my_char.Y, Globals.gamedata.my_char.Z, npc.X, npc.Y, npc.Z).ToString() + Environment.NewLine +
                        "Cur HP: " + npc.Cur_HP + Environment.NewLine +
                        "Max HP: " + npc.Max_HP + Environment.NewLine +
                        (npc.isInvisible == 0x00 ? "Visible" : "Invisible") + Environment.NewLine +
                        "Type ID: " + npc.NPCID + Environment.NewLine +
                        "Unique ID: " + npc.ID + Environment.NewLine +
                        "SummonedNameColor: " + npc.SummonedNameColor;
                }
            }

            toolTip1.SetToolTip(listView_npc_data, text);
        }

        private void listView_items_data_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = "";

            if (listView_items_data.SelectedIndices.Count > 0)
            {
                //action to this item
                uint id = Util.GetUInt32(listView_items_data.Items[listView_items_data.SelectedIndices[0]].SubItems[2].Text);
                ItemInfo item = null;

                if (Globals.ItemLock.TryEnterReadLock(Globals.THREAD_WAIT_GUI))
                {
                    try
                    {
                        item = Util.GetItem(id);
                    }
                    finally
                    {
                        Globals.ItemLock.ExitReadLock();
                    }
                }

                if (item != null)
                {
                    text = Util.GetItemName(item.ItemID) + Environment.NewLine +
                        item.Count.ToString() + Environment.NewLine +
                        (item.Stackable == 0x01 ? "Stacks" : "Single") + Environment.NewLine +
                        "X: " + item.X.ToString() + Environment.NewLine +
                        "Y: " + item.Y.ToString() + Environment.NewLine +
                        "Z: " + item.Z.ToString() + Environment.NewLine +
                        "Dist: " + Util.Distance(Globals.gamedata.my_char.X, Globals.gamedata.my_char.Y, Globals.gamedata.my_char.Z, item.X, item.Y, item.Z).ToString() + Environment.NewLine +
                        "Type ID: " + item.ItemID + Environment.NewLine +
                        "Unique ID: " + item.ID;
                }
            }

            toolTip1.SetToolTip(listView_items_data, text);
        }

        private void listView_players_data_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = "";

            if (listView_players_data.SelectedIndices.Count > 0)
            {
                //action to this item
                uint id = Util.GetUInt32(listView_players_data.Items[listView_players_data.SelectedIndices[0]].SubItems[5].Text);
                CharInfo player = null;

                if (Globals.PlayerLock.TryEnterReadLock(Globals.THREAD_WAIT_GUI))
                {
                    try
                    {
                        player = Util.GetChar(id);
                    }
                    finally
                    {
                        Globals.PlayerLock.ExitReadLock();
                    }
                }

                if (player != null)
                {
                    text = player.Name + Environment.NewLine +
                        player.Title + Environment.NewLine +
                        (player.Sex == 0 ? "Male " : "Female ") + Util.GetRace(player.Race) + " " + Util.GetClass(player.Class) + Environment.NewLine +
                        "R: " + (player.EnchantAmount == 0 ? "" : "+" + player.EnchantAmount.ToString() + " ") + Util.GetItemName(player.RHand) + Environment.NewLine +
                        "LR: " + Util.GetItemName(player.LRHand) + Environment.NewLine +
                        "L: " + Util.GetItemName(player.LHand) + Environment.NewLine +
                        Util.GetItemName(player.Head) + Environment.NewLine +
                        Util.GetItemName(player.Gloves) + Environment.NewLine +
                        Util.GetItemName(player.Feet) + Environment.NewLine +
                        Util.GetItemName(player.Chest) + Environment.NewLine +
                        Util.GetItemName(player.Legs) + Environment.NewLine +
                        Util.GetItemName(player.Back) + Environment.NewLine +
                        Util.GetItemName(player.Hair) + Environment.NewLine +
                        "PvP Flag: " + player.PvPFlag.ToString() + Environment.NewLine +
                        (player.isAlikeDead == 0x00 ? "Alive" : "Dead") + Environment.NewLine +
                        "Karma: " + player.Karma.ToString() + Environment.NewLine +
                        "Cast Speed: " + player.MatkSpeed.ToString() + Environment.NewLine +
                        "Attack Speed: " + (player.PatkSpeed/* * player.AttackSpeedMult*/).ToString() + Environment.NewLine +
                        "Run Speed: " + (player.RunSpeed * player.MoveSpeedMult).ToString() + Environment.NewLine +
                        "Rec: " + player.RecAmount.ToString() + Environment.NewLine +
                        "X: " + player.X.ToString() + Environment.NewLine +
                        "Y: " + player.Y.ToString() + Environment.NewLine +
                        "Z: " + player.Z.ToString() + Environment.NewLine +
                        "Dist: " + Util.Distance(Globals.gamedata.my_char.X, Globals.gamedata.my_char.Y, Globals.gamedata.my_char.Z, player.X, player.Y, player.Z).ToString() + Environment.NewLine +
                        "Unique ID: " + player.ID + Environment.NewLine +
                        "Name Color: " + player.NameColor.ToString() ;
                }
            }

            toolTip1.SetToolTip(listView_players_data, text);
        }

        private void listView_skills_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = "";

            if (listView_skills.SelectedIndices.Count > 0)
            {
                uint id = Util.GetUInt32(listView_skills.Items[listView_skills.SelectedIndices[0]].SubItems[2].Text);
                UserSkill us = null;

                if (Globals.SkillListLock.TryEnterReadLock(Globals.THREAD_WAIT_GUI))
                {
                    try
                    {
                        us = Util.GetSkill(id);
                    }
                    finally
                    {
                        Globals.SkillListLock.ExitReadLock();
                    }
                }

                if (us != null)
                {
                    text = Util.GetSkillName(us.ID, us.Level) + Environment.NewLine +
                        "Level: " + us.Level.ToString() + Environment.NewLine +
                        (us.Passive == 0x01 ? "Passive" : "Active") + Environment.NewLine +
                        Util.GetSkillDesc(us.ID, us.Level, 1) + Environment.NewLine +
                        Util.GetSkillDesc(us.ID, us.Level, 2) + Environment.NewLine +
                        Util.GetSkillDesc(us.ID, us.Level, 3) + Environment.NewLine +
                        "Type ID: " + us.ID;
                }
            }

            toolTip1.SetToolTip(listView_skills, text);
        }

        private void listView_inventory_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter_inventory.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter_inventory.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter_inventory.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter_inventory.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter_inventory.SortColumn = e.Column;
                lvwColumnSorter_inventory.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            //this.listView_inventory.Sort();
            Globals.l2net_home.timer_inventory.Start();
        }

        private void listView_npc_data_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter_npc_data.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter_npc_data.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter_npc_data.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter_npc_data.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter_npc_data.SortColumn = e.Column;
                lvwColumnSorter_npc_data.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            //this.listView_npc_data.Sort();
            Globals.l2net_home.timer_npcs.Start();
        }

        private void listView_items_data_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter_item_data.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter_item_data.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter_item_data.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter_item_data.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter_item_data.SortColumn = e.Column;
                lvwColumnSorter_item_data.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            //this.listView_items_data.Sort();
            Globals.l2net_home.timer_items.Start();
        }

        private void listView_players_data_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter_players_data.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter_players_data.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter_players_data.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter_players_data.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter_players_data.SortColumn = e.Column;
                lvwColumnSorter_players_data.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            //this.listView_players_data.Sort();
            Globals.l2net_home.timer_players.Start();
        }

        private void listView_mybuffs_data_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter_mybuffs_data.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter_mybuffs_data.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter_mybuffs_data.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter_mybuffs_data.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter_mybuffs_data.SortColumn = e.Column;
                lvwColumnSorter_mybuffs_data.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            //this.listView_players_data.Sort();
            Globals.l2net_home.timer_mybuffs.Start();
        }

        private void listView_skills_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter_skills.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter_skills.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter_skills.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter_skills.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter_skills.SortColumn = e.Column;
                lvwColumnSorter_skills.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.listView_skills.Sort();
        }

        private void listView_char_clan_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter_clan.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter_clan.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter_clan.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter_clan.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter_clan.SortColumn = e.Column;
                lvwColumnSorter_clan.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.listView_char_clan.Sort();
        }

        private void listView_inventory_DoubleClick(object sender, EventArgs e)
        {
            if (Globals.gamedata.running && Globals.gamedata.logged_in)
            {
                if (listView_inventory.SelectedIndices.Count > 0)
                {
                    uint id = Util.GetUInt32(listView_inventory.Items[listView_inventory.SelectedIndices[0]].SubItems[4].Text);

                    if (id != 0)
                    {
                        ServerPackets.Use_Item(id);
                    }
                }
            }
        }

        private void listView_npc_data_DoubleClick(object sender, EventArgs e)
        {
            //double click on an npc in the list
            //normal - follow them
            //cntrl - attack them
            if (listView_npc_data.SelectedIndices.Count > 0)
            {
                //action to this item
                uint id = Util.GetUInt32(listView_npc_data.Items[listView_npc_data.SelectedIndices[0]].SubItems[2].Text);

                if (Globals.NPCLock.TryEnterReadLock(Globals.THREAD_WAIT_GUI))
                {
                    try
                    {
                        NPCInfo npc = Util.GetNPC(id);

                        if (npc != null)
                        {
                            ServerPackets.ClickChar(id, Util.Float_Int32(npc.X), Util.Float_Int32(npc.Y), Util.Float_Int32(npc.Z), Globals.gamedata.Control, Globals.gamedata.Shift);
                        }
                    }
                    finally
                    {
                        Globals.NPCLock.ExitReadLock();
                    }
                }
            }
        }

        private void listView_items_data_DoubleClick(object sender, EventArgs e)
        {
            if (listView_items_data.SelectedIndices.Count > 0)
            {
                //action to this item
                uint id = Util.GetUInt32(listView_items_data.Items[listView_items_data.SelectedIndices[0]].SubItems[2].Text);

                if (Globals.ItemLock.TryEnterReadLock(Globals.THREAD_WAIT_GUI))
                {
                    try
                    {
                        ItemInfo item = Util.GetItem(id);

                        if (item != null)
                        {
                            ServerPackets.ClickItem(id, Util.Float_Int32(item.X), Util.Float_Int32(item.Y), Util.Float_Int32(item.Z), Globals.gamedata.Shift);
                        }
                    }
                    finally
                    {
                        Globals.ItemLock.ExitReadLock();
                    }
                }
            }
        }

        private void listView_players_data_DoubleClick(object sender, EventArgs e)
        {
            //double click on a player in the list
            //normal - follow them
            //cntrl - attack them
            if (listView_players_data.SelectedIndices.Count > 0)
            {
                //action to this item
                uint id = Util.GetUInt32(listView_players_data.Items[listView_players_data.SelectedIndices[0]].SubItems[5].Text);

                if (Globals.PlayerLock.TryEnterReadLock(Globals.THREAD_WAIT_GUI))
                {
                    try
                    {
                        CharInfo player = Util.GetChar(id);

                        if (player != null)
                        {
                            ServerPackets.ClickChar(id, Util.Float_Int32(player.X), Util.Float_Int32(player.Y), Util.Float_Int32(player.Z), Globals.gamedata.Control, Globals.gamedata.Shift);
                        }
                    }
                    finally
                    {
                        Globals.PlayerLock.ExitReadLock();
                    }
                }
            }
        }

        private void listView_skills_DoubleClick(object sender, EventArgs e)
        {
            if (Globals.gamedata.logged_in)
            {
                if (listView_skills.SelectedIndices.Count > 0) //&& listView_skills.Items[listView_skills.SelectedIndices[0]].SubItems[2].Text == "Active"
                {
                    uint id = Util.GetUInt32(listView_skills.Items[listView_skills.SelectedIndices[0]].SubItems[2].Text);

                    if (id != 0)
                    {
                        ServerPackets.Try_Use_Skill(id, Globals.gamedata.Control, Globals.gamedata.Shift);
                    }
                }//dont want to use passive skills
            }//end of check for running
        }
    }
}
