using System;

namespace L2_login
{
	/// <summary>
	/// Summary description for AddInfo.
	/// </summary>
	public class AddInfo
	{
		public AddInfo()
		{
		}

		public static void Set_Target_HP()
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
                        }
					}//unlock
					finally
					{
                        Globals.NPCLock.ExitReadLock();
					}
					break;
			}

			Globals.l2net_home.Set_Target_CP(cp);
			Globals.l2net_home.Set_Target_HP(hp);
			Globals.l2net_home.Set_Target_MP(mp);
			//Globals.l2net_home.panel_target.Refresh();

            if (Globals.l2net_home.menuItem_cmd_overlay.Checked && Globals.overlaywindow != null)
			{
                Globals.overlaywindow.Set_Target_CP(cp);
                Globals.overlaywindow.Set_Target_HP(hp);
                Globals.overlaywindow.Set_Target_MP(mp);
				//L2NET.overlaywindow.Refresh();
			}
		}

		public static void Set_PartyVisible()
		{
            Globals.l2net_home.Set_PartyVisible();
		}

		public static void Set_PartyInfo()
		{
            Globals.l2net_home.Set_PartyInfo();
        }

        public static int Get_Item_Image_Index(uint id)
        {
            try
            {
                lock (Globals.ItemImagesLock)
                {
                    if (!Globals.l2net_home.imageList_items_loaded.ContainsKey(id))
                    {
                        System.Drawing.Bitmap img;

                        if (System.IO.File.Exists(Util.GetItemImagePath(id)))
                        {
                            img = new System.Drawing.Bitmap(Util.GetItemImagePath(id));
                        }
                        else
                        {
                            img = new System.Drawing.Bitmap(32, 32);
                        }

                        Globals.l2net_home.imageList_items.Images.Add(img);
                        Globals.l2net_home.imageList_items_loaded.Add(id, Globals.l2net_home.imageList_items.Images.Count - 1);
                    }

                    return (int)Globals.l2net_home.imageList_items_loaded[id];
                }
            }
            catch
            {
                return -1;
            }
        }

        public static int Get_Skill_Image_Index(uint id)
        {
            try
            {
                string sk = id.ToString();
                while (sk.Length < 4)
                {
                    sk = "0" + sk;
                }

                lock (Globals.SkillImagesLock)
                {
                    if (!Globals.l2net_home.imageList_skills_loaded.ContainsKey(sk))
                    {
                        System.Drawing.Bitmap img;

                        try
                        {
                            img = new System.Drawing.Bitmap(Globals.PATH + "\\Icons\\skill" + sk + "_0.BMP");
                        }
                        catch
                        {
                            try
                            {
                                img = new System.Drawing.Bitmap(Globals.PATH + "\\Icons\\skill0000_0.BMP");
                            }
                            catch
                            {
                                img = new System.Drawing.Bitmap(32, 32);
                            }
                        }

                        Globals.l2net_home.imageList_skills.Images.Add(img);
                        Globals.l2net_home.imageList_skills_loaded.Add(sk, Globals.l2net_home.imageList_skills.Images.Count - 1);
                    }

                    return (int)Globals.l2net_home.imageList_skills_loaded[sk];
                }
            }
            catch
            {
                return -1;
            }
        }

		public static void Set_SkillList(int active)
		{
			Globals.l2net_home.listView_skills.BeginUpdate();
            Globals.l2net_home.listView_skills.ListViewItemSorter = null;

			//lets clear our skill list and skill image list
			Globals.l2net_home.listView_skills.Items.Clear();
			//Globals.l2net_home.imageList_skills.Images.Clear();

            Globals.SkillListLock.EnterReadLock();
			try
			{
                foreach (UserSkill us in Globals.gamedata.skills.Values)
				{
                    if (active == 1 && us.Passive == 0) //Add active skills to list
                    {
                        System.Windows.Forms.ListViewItem ObjListItem = Globals.l2net_home.listView_skills.Items.Add(Util.GetSkillName(us.ID, us.Level));
                        ObjListItem.SubItems.Add(us.Level.ToString());
                        //ObjListItem.SubItems.Add("Active");
                        //ObjListItem.SubItems.Add("");
                        ObjListItem.SubItems.Add(us.ID.ToString());
                        try
                        {
                            ObjListItem.ImageIndex = Get_Skill_Image_Index(us.ID);
                        }
                        catch
                        {
                            ObjListItem.ImageIndex = -1;
                        }
                    }
                    if (active == 0 && us.Passive == 1) //Add Passive skills to list
                    {
                        System.Windows.Forms.ListViewItem ObjListItem = Globals.l2net_home.listView_skills.Items.Add(Util.GetSkillName(us.ID, us.Level));
                        ObjListItem.SubItems.Add(us.Level.ToString());
                        //ObjListItem.SubItems.Add("Passive");
                        //ObjListItem.SubItems.Add("");
                        ObjListItem.SubItems.Add(us.ID.ToString());
                        try
                        {
                            ObjListItem.ImageIndex = Get_Skill_Image_Index(us.ID);
                        }
                        catch
                        {
                            ObjListItem.ImageIndex = -1;
                        }
                    }
				}
			}
			finally
			{
                Globals.SkillListLock.ExitReadLock();
			}

            Globals.l2net_home.listView_skills.ListViewItemSorter = Globals.l2net_home.lvwColumnSorter_skills;
            Globals.l2net_home.listView_skills.EndUpdate();
		}

        public static string Get_XP_Percent()
        {
            try
            {
                ulong xp = (ulong)Globals.gamedata.my_char.XP;
                uint lvl = (uint)Globals.gamedata.my_char.Level;

                if (xp == 0)
                {
                    return "0%";
                }

                ulong lastlvlxp;
                if (lvl == 1)
                {
                    lastlvlxp = 0;
                }
                else
                {
                    lastlvlxp = (ulong)Globals.levelexp[lvl];
                }
                ulong nextlvlxp = (ulong)Globals.levelexp[lvl + 1];

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

        public static int Get_XP_Percent_Int()
        {
            try
            {
                ulong xp = (ulong)Globals.gamedata.my_char.XP;
                uint lvl = (uint)Globals.gamedata.my_char.Level;

                if (xp == 0)
                {
                    return 0;
                }

                ulong lastlvlxp;
                if (lvl == 1)
                {
                    lastlvlxp = 0;
                }
                else
                {
                    lastlvlxp = (ulong)Globals.levelexp[lvl];
                }
                ulong nextlvlxp = (ulong)Globals.levelexp[lvl + 1];

                xp -= lastlvlxp;
                nextlvlxp -= lastlvlxp;

                //float per = ((float)xp * 100) / ((float)nextlvlxp);
                float per = (((float)xp) / ((float)nextlvlxp))*100;
                //per = System.Convert.ToSingle(System.Math.Round(per, 6));

                return System.Convert.ToInt32(per);
            }
            catch
            {
                return 0;
            }
        }

        public static string Get_Vitality_Level()
        {
            try
            {
                uint vit = (uint)Globals.gamedata.my_char.Vitality_Points;
                if (vit >=0x4268)
                    return "4";
                else if (vit >= 0x32C8)
                    return "3";
                else if (vit >= 0x07D0)
                    return "2";
                else if (vit >= 0xF0)
                    return "1";
                else
                    return "0";
            }
            catch
            {
                return "0";
            }
        }

		public static void Set_Char_Info_Basic()
		{
            Globals.l2net_home.Set_Char_Info_Basic();
            //Globals.l2net_home.tabPage_char_info.Refresh();
            //Globals.l2net_home.panel_charinfo_ul.Refresh();
		}

		public static void Set_Char_Info()
		{
			Set_Char_Info_Basic();
			//Set_Char_Cords();//called by Set_Char_Info_Basic

            Globals.l2net_home.Set_Char_Info();

			//Set_CrestImages();

			//l2net_home.tabPage_char_detail.Refresh();
		}

        public static void Set_Pet_Info()
        {
            Globals.petwindow.Set_Pet_Info();
        }

		public static void Inventory_Update(ByteBuffer buff)
		{
            uint cnt = buff.ReadUInt16();//ushort

            Globals.InventoryLock.EnterWriteLock();
            try
            {
                for (int i = 0; i < cnt; i++)
                {
                    uint change = buff.ReadUInt16();//ushort

                    InventoryInfo inv_inf = new InventoryInfo();

                    if (change == 1 || change == 2)
                    {
                        inv_inf.Load(buff, 1);
                    }
                    else
                    {
                        inv_inf.Load(buff, 0);
                    }

                    if (inv_inf.Count == 0)
                    {
                        change = 3;
                    }

                    switch (change)
                    {
                        case 1://add
                            Add_Inventory(inv_inf);
                            break;
                        case 2://modify
                            Add_Inventory(inv_inf);
                            break;
                        case 3://remove
                            Remove_Inventory(inv_inf.ID);
                            break;
                    }
                }
            }
            finally
            {
                Globals.InventoryLock.ExitWriteLock();
            }

            Globals.l2net_home.timer_inventory.Start();
		}

        public static void Remove_Inventory(uint objID)
		{
            if (Globals.gamedata.inventory.ContainsKey(objID))
            {
                Globals.gamedata.inventory.Remove(objID);
            }
		}

		public static void Add_Inventory(InventoryInfo inv_inf)
		{
            if (Globals.gamedata.inventory.ContainsKey(inv_inf.ID))
            {
                if (((InventoryInfo)Globals.gamedata.inventory[inv_inf.ID]).isEquipped == 1 && inv_inf.isEquipped != 1)
                {
                    //was equipped... not now
                    Globals.l2net_home.Clear_Equip(inv_inf.Slot);
                }
                else if (((InventoryInfo)Globals.gamedata.inventory[inv_inf.ID]).isEquipped != 1 && inv_inf.isEquipped == 1)
                {
                    //was not equipped... is now
                    Globals.l2net_home.Do_Equip(inv_inf);
                }

                //already in the array
                Globals.gamedata.inventory[inv_inf.ID] = inv_inf;
            }
            else
            {
                Globals.gamedata.inventory.Add(inv_inf.ID, inv_inf);

                if (inv_inf.isEquipped == 1)
                {
                    Globals.l2net_home.Do_Equip(inv_inf);
                }
            }
		}



        public static void PetInventory_Update(ByteBuffer buff)
        {
            uint cnt = buff.ReadUInt16();
            Globals.PetInventoryLock.EnterWriteLock();
            try
            {
                for (int i = 0; i < cnt; i++)
                {
                    uint change = buff.ReadUInt16();
                    PetInventoryInfo inv_inf = new PetInventoryInfo();
                    inv_inf.Load(buff);

                    if (inv_inf.Count == 0)
                    {
                        change = 3;
                    }

                    switch (change)
                    {
                        case 1: //add
                            Add_PetInventory(inv_inf);
                            break;
                        case 2: //modify
                            Add_PetInventory(inv_inf);
                            break;
                        case 3: //delete
                            Remove_PetInventory(inv_inf.ID);
                            break;
                    }

                }
            }
            finally
            {
                Globals.PetInventoryLock.ExitWriteLock();
            }


        }

        public static void Remove_PetInventory(uint objID)
        {
            if (Globals.gamedata.my_pet.inventory.ContainsKey(objID))
            {
                Globals.gamedata.my_pet.inventory.Remove(objID);
            }
        }

        public static void Add_PetInventory(PetInventoryInfo inv_inf)
        {
            if (Globals.gamedata.my_pet.inventory.ContainsKey(inv_inf.ID))
            {
                //already in list, update
                Globals.gamedata.my_pet.inventory[inv_inf.ID] = inv_inf;
            }
            else
            {
                //Not in list, add it
                Globals.gamedata.my_pet.inventory.Add(inv_inf.ID, inv_inf);
            }
        }

        public static void Add_Buylist(BuyList b_list)
        {
            Globals.gamedata.buylist.Add(b_list.ItemID, b_list);
        }

        public static void Dead_Count()
        {
            int dead = 0;

            Globals.NPCLock.EnterReadLock();
            try
            {
                foreach (NPCInfo npc in Globals.gamedata.nearby_npcs.Values)
                {
                    if (npc.isAlikeDead != 0)
                    {
                        dead++;
                    }
                }
            }
            catch
            {
                //oh well
            }
            finally
            {
                Globals.NPCLock.ExitReadLock();
            }

            Globals.l2net_home.Add_Text("Dead Count: " + dead.ToString());
        }

		public static void Remove_NPCInfo(uint id)
		{
            Globals.NPCLock.EnterWriteLock();
            try
            {
                if (Globals.gamedata.nearby_npcs.ContainsKey(id))
                {
                    Globals.gamedata.nearby_npcs.Remove(id);
                }
            }
            catch
            {
                //oh well
            }
            finally
            {
                Globals.NPCLock.ExitWriteLock();
            }
        }

        public static void CleanUp_NPC()
        {
            long now = System.DateTime.Now.Ticks;

            System.Collections.ArrayList remove = new System.Collections.ArrayList();

            Globals.NPCLock.EnterReadLock();
            try
            {
                foreach(NPCInfo npc in Globals.gamedata.nearby_npcs.Values)
                {
                    int dist = Util.Distance(Globals.gamedata.my_char.X, Globals.gamedata.my_char.Y, Globals.gamedata.my_char.Z, npc.X, npc.Y, npc.Z);

                    if (npc.RemoveAt <= now && dist >= Globals.REMOVE_RANGE_INNER)
                    {
                        remove.Add(npc.ID);
                    }
                    else if (dist >= Globals.REMOVE_RANGE)
                    {
                        remove.Add(npc.ID);
                    }
                }
            }
            catch
            {
                //oh well
            }
            finally
            {
                Globals.NPCLock.ExitReadLock();
            }

            if (remove.Count > 0)
            {
                foreach (uint id in remove)
                {
                    //Globals.l2net_home.Add_OnlyDebug("NPC Cleanup - Removing: " + id.ToString());
                    Remove_NPCInfo(id);
                }

                Globals.l2net_home.timer_npcs.Start();
            }
        }

		public static void Add_NPCInfo(NPCInfo npc_inf)
		{
            Globals.NPCLock.EnterWriteLock();
            try
            {
                if (Globals.gamedata.nearby_npcs.ContainsKey(npc_inf.ID))
                {
                    //already in the array
                    //Globals.gamedata.nearby_npcs[npc_inf.ID] = npc_inf;
                    ((NPCInfo)Globals.gamedata.nearby_npcs[npc_inf.ID]).X = npc_inf.X;
                    ((NPCInfo)Globals.gamedata.nearby_npcs[npc_inf.ID]).Y = npc_inf.Y;
                    ((NPCInfo)Globals.gamedata.nearby_npcs[npc_inf.ID]).Z = npc_inf.Z;
                    //((NPCInfo)Globals.gamedata.nearby_npcs[npc_inf.ID]).isInvisible = npc_inf.isInvisible;
                    ((NPCInfo)Globals.gamedata.nearby_npcs[npc_inf.ID]).isAlikeDead = npc_inf.isAlikeDead;
                    ((NPCInfo)Globals.gamedata.nearby_npcs[npc_inf.ID]).isInCombat = npc_inf.isInCombat;
                    ((NPCInfo)Globals.gamedata.nearby_npcs[npc_inf.ID]).isSitting = npc_inf.isSitting;
                    ((NPCInfo)Globals.gamedata.nearby_npcs[npc_inf.ID]).isRunning = npc_inf.isRunning;
                    ((NPCInfo)Globals.gamedata.nearby_npcs[npc_inf.ID]).ExtendedEffects = npc_inf.ExtendedEffects;
                    ((NPCInfo)Globals.gamedata.nearby_npcs[npc_inf.ID]).AbnormalEffects = npc_inf.AbnormalEffects;
                    if (npc_inf.TargetID == Globals.gamedata.my_char.ID)
                    {
                        Globals.l2net_home.Add_Debug("Got aggro? " + npc_inf.Name);
                    }
                    else
                    {
                       // Globals.l2net_home.Add_Debug("npc:" + npc_inf.Name + " targetid:" + npc_inf.TargetID);
                    }
                }
                else
                {
                    Globals.gamedata.nearby_npcs.Add(npc_inf.ID, npc_inf);
                }
            }
            catch
            {
                //oh well
            }
            finally
            {
                Globals.NPCLock.ExitWriteLock();
            }

            Globals.l2net_home.timer_npcs.Start();
		}

		public static void Add_ItemInfo(ItemInfo itm_inf)
		{
            Globals.ItemLock.EnterWriteLock();
            try
            {
                if (Globals.gamedata.nearby_items.ContainsKey(itm_inf.ID))
                {
                    //already in the array
                    Globals.gamedata.nearby_items[itm_inf.ID] = itm_inf;
                }
                else
                {
                    if (itm_inf.DroppedBy == Globals.gamedata.my_char.TargetID)
                    {

                        //do nothing

                        //itm_inf.IsMine = true;
                        //Globals.l2net_home.Add_Text("My target with objid: " + itm_inf.DroppedBy + " dropped an item. IsMine = true", Globals.Green, TextType.BOT);
                    }
                    else
                    {
                        //bool is_party_target = false;

                        Globals.PartyLock.EnterReadLock();
                        try
                        {
                            foreach (PartyMember pl in Globals.gamedata.PartyMembers.Values)
                            {
                                CharInfo ch;

                                Globals.PlayerLock.EnterReadLock();
                                try
                                {
                                    ch = Util.GetChar(pl.ID);
                                }
                                finally
                                {
                                    Globals.PlayerLock.ExitReadLock();
                                }

                                /*
                                if (itm_inf.DroppedBy == ch.TargetID)
                                {
                                    is_party_target = true;
                                    break;
                                }
                                 */
                            }
                        }
                        finally
                        {
                            Globals.PartyLock.ExitReadLock();
                        }

                        /*if (!is_party_target)
                        {
                            itm_inf.IsMine = false;
                        }
                        else
                        {
                            itm_inf.IsMine = true;
                        }
                        //Globals.l2net_home.Add_Text("My target with objid: " + itm_inf.DroppedBy + " dropped an item. IsMine = false", Globals.Green, TextType.BOT);
                        */
                    }
                    Globals.gamedata.nearby_items.Add(itm_inf.ID, itm_inf);
                }
            }
            catch
            {
                //oh well
            }
            finally
            {
                Globals.ItemLock.ExitWriteLock();
            }

            Globals.l2net_home.timer_items.Start();
		}

        public static void CleanUp_Item()
        {
            System.Collections.ArrayList remove = new System.Collections.ArrayList();

            Globals.ItemLock.EnterReadLock();
            try
            {
                foreach (ItemInfo item in Globals.gamedata.nearby_items.Values)
                {
                    int dist = Util.Distance(Globals.gamedata.my_char.X, Globals.gamedata.my_char.Y, Globals.gamedata.my_char.Z, item.X, item.Y, item.Z);

                    if (dist >= Globals.REMOVE_RANGE)
                    {
                        remove.Add(item.ID);
                    }
                }
            }
            catch
            {
                //oh well
            }
            finally
            {
                Globals.ItemLock.ExitReadLock();
            }

            if (remove.Count > 0)
            {
                foreach (uint id in remove)
                {
                    //Globals.l2net_home.Add_OnlyDebug("Item Cleanup - Removing: " + id.ToString());
                    Remove_Item(id);
                }

                Globals.l2net_home.timer_items.Start();
            }
        }

		public static void Remove_Item(uint objID)
		{
            Globals.ItemLock.EnterWriteLock();
            try
            {
                if (Globals.gamedata.nearby_items.ContainsKey(objID))
                {
                    Globals.gamedata.nearby_items.Remove(objID);
                }
            }
            catch
            {
                //oh well
            }
            finally
            {
                Globals.ItemLock.ExitWriteLock();
            }
        }

        public static void Remove_CharInfo(uint id)
		{
            Globals.PlayerLock.EnterWriteLock();
            try
            {
                if (Globals.gamedata.nearby_chars.ContainsKey(id))
                {
                    Globals.gamedata.nearby_chars.Remove(id);
                }
            }
            catch
            {
                //oh well
            }
            finally
            {
                Globals.PlayerLock.ExitWriteLock();
            }
		}

        public static void CleanUp_Char()
        {
            System.Collections.ArrayList remove = new System.Collections.ArrayList();

            Globals.PlayerLock.EnterReadLock();
            try
            {
                foreach(CharInfo player in Globals.gamedata.nearby_chars.Values)
                {
                    int dist = Util.Distance(Globals.gamedata.my_char.X, Globals.gamedata.my_char.Y, Globals.gamedata.my_char.Z, player.X, player.Y, player.Z);

                    if (dist >= Globals.REMOVE_RANGE)
                    {
                        remove.Add(player.ID);
                    }
                }
            }
            catch
            {
                //oh well
            }
            finally
            {
                Globals.PlayerLock.ExitReadLock();
            }

            if (remove.Count > 0)
            {
                foreach (uint id in remove)
                {
                    //Globals.l2net_home.Add_OnlyDebug("Player Cleanup - Removing: " + id.ToString());
                    Remove_CharInfo(id);
                }

                Globals.l2net_home.timer_players.Start();
            }
        }

        public static void Set_WarState(uint id, int state, uint combat)
        {
            Globals.PlayerLock.EnterReadLock();
            try
            {
                //check if it is already in the list
                CharInfo player = Util.GetChar(id);

                if (player != null)
                {
                    player.isInCombat = combat;

                    if (player.WarState != state)
                    {
                        player.WarState = state;

                        Globals.l2net_home.timer_players.Start();
                    }
                    if (Globals.gamedata.nearby_chars.ContainsKey(id))
                    {
                        Globals.gamedata.nearby_chars.Remove(id);
                        Globals.gamedata.nearby_chars.Add(player.ID, player);
                    }
                }
            }
            catch
            {
                //oh well
            }
            finally
            {
                Globals.PlayerLock.ExitReadLock();
            }
        }

        public static void Set_WarState(uint id, int state, uint combat, int karma, uint pvpflag)
        {
            Globals.PlayerLock.EnterReadLock();
            try
            {
                //check if it is already in the list
                CharInfo player = Util.GetChar(id);

                if (player != null)
                {
                    player.isInCombat = combat;
                    player.Karma = karma;
                    player.PvPFlag = pvpflag;

                    if (player.WarState != state)
                    {
                        player.WarState = state;

                        Globals.l2net_home.timer_players.Start();
                    }
                    // test to fix
                    if (Globals.gamedata.nearby_chars.ContainsKey(id))
                    {
                        Globals.gamedata.nearby_chars.Remove(id);
                        Globals.gamedata.nearby_chars.Add(player.ID, player);
                    }

                }
            }
            catch
            {
                //oh well
            }
            finally
            {
                Globals.PlayerLock.ExitReadLock();
            }
        }

		public static void Set_Relation(uint id, uint relation)
		{
			Globals.PlayerLock.EnterReadLock();
			try
			{
				CharInfo player = Util.GetChar(id);
				
				if (player != null)
				{
                    // Globals.l2net_home.Add_Text("AddInfo.cs (set) -> ID:" + id + " Relation: " + relation, Globals.Cyan, TextType.BOT);
					player.Relation = relation;
                    // Globals.l2net_home.Add_Text("AddInfo.cs (read) -> ID:" + player.ID + " Relation: " + player.Relation, Globals.Cyan, TextType.BOT);
				}
			}
			catch
			{
				// oh well
			}
			finally
			{
				Globals.PlayerLock.ExitReadLock();
			}
		}

		public static void Add_CharInfo(CharInfo ch_inf)
		{
			//lets check thru the list view to see if the char is there... if so lets update instead of add
            Globals.PlayerLock.EnterWriteLock();
            try
            {
                if (Globals.gamedata.nearby_chars.ContainsKey(ch_inf.ID))
                {
                    //in the array already
                    //Globals.gamedata.nearby_chars[ch_inf.ID] = ch_inf;
                    ((CharInfo)Globals.gamedata.nearby_chars[ch_inf.ID]).CopyNew(ch_inf);
                }
                else
                {
                    Globals.gamedata.nearby_chars.Add(ch_inf.ID, ch_inf);
                }
            }
            catch
            {
                //oh well
            }
            finally
            {
                Globals.PlayerLock.ExitWriteLock();
            }

            Globals.l2net_home.timer_players.Start();
		}
	}//end of class
}//end of namespace