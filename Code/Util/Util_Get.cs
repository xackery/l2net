using System;
using System.Collections.Generic;
using System.Text;

namespace L2_login
{
    public static partial class Util
    {
        static public void ClearAllNearby()
        {
            Globals.l2net_home.listView_npc_data.VirtualListSize = 0;
            Globals.l2net_home.listView_items_data.VirtualListSize = 0;
            Globals.l2net_home.listView_players_data.VirtualListSize = 0;

            //lets get rid of all the mobs/npcs that are in our list
            Globals.ItemLock.EnterWriteLock();
            try
            {
                Globals.gamedata.nearby_items.Clear();
                Globals.l2net_home.listView_inventory_items.Clear();
            }
            finally
            {
                Globals.ItemLock.ExitWriteLock();
            }

            Globals.NPCLock.EnterWriteLock();
            try
            {
                Globals.gamedata.nearby_npcs.Clear();
                Globals.l2net_home.listView_npc_data_items.Clear();
            }
            finally
            {
                Globals.NPCLock.ExitWriteLock();
            }

            Globals.PlayerLock.EnterWriteLock();
            try
            {
                Globals.gamedata.nearby_chars.Clear();
                Globals.l2net_home.listView_players_data_items.Clear();
            }
            finally
            {
                Globals.PlayerLock.ExitWriteLock();
            }

            /*Globals.l2net_home.timer_players.Start();
            Globals.l2net_home.timer_items.Start();
            Globals.l2net_home.timer_npcs.Start();*/
        }

        static public void ClearAllSelf()
        {
            Globals.l2net_home.listView_inventory.VirtualListSize = 0;

            Globals.InventoryLock.EnterWriteLock();
            try
            {
                Globals.gamedata.inventory.Clear();
                Globals.l2net_home.listView_inventory_items.Clear();
            }
            finally
            {
                Globals.InventoryLock.ExitWriteLock();
            }

            Globals.SkillListLock.EnterWriteLock();
            try
            {
                Globals.gamedata.skills.Clear();
                Globals.l2net_home.listView_skills.Items.Clear();
            }
            finally
            {
                Globals.SkillListLock.ExitWriteLock();
            }

            Globals.MyBuffsListLock.EnterWriteLock();
            try
            {
                Globals.gamedata.mybuffs.Clear();
                Globals.l2net_home.listView_mybuffs_data.Items.Clear();
            }
            finally
            {
                Globals.MyBuffsListLock.ExitWriteLock();
            }

            Globals.PartyLock.EnterWriteLock();
            try
            {
                Globals.gamedata.PartyCount = 0;
                Globals.gamedata.PartyMembers.Clear();
            }
            finally
            {
                Globals.PartyLock.ExitWriteLock();
            }

            Globals.l2net_home.Set_PartyVisible();

            Globals.l2net_home.listView_char_clan.Items.Clear();
            Globals.l2net_home.listView_char_data.Items.Clear();

            //Globals.l2net_home.timer_inventory.Start();
        }

        static public uint GetSkillID(string str)
        {
            str = str.ToUpperInvariant();

            foreach (SkillList sk in Globals.skilllist.Values)
            {
                if (sk.Levels.Count != 0)
                {
                    if (System.String.Equals(str, ((SkillInfo)sk.Levels[0]).Name.ToUpperInvariant()))
                        return ((SkillInfo)sk.Levels[0]).ID;
                }
            }

            return 0;
        }

        static public int GetSkillReuse(uint id)
        {
            long getskill_reuse = 0;
            UserSkill us = Util.GetSkill(id);
            if (us.IsReady())
            {
                getskill_reuse = 0;
            }
            else
            {
                getskill_reuse = (us.NextTime.Ticks - System.DateTime.Now.Ticks) / System.TimeSpan.TicksPerMillisecond;
            }
            return (int)getskill_reuse;
        }

        public static string GetSkillName(uint id, uint level)
        {
            string name = "-unknown skill-";

            try
            {
                name = ((SkillList)Globals.skilllist[id]).GetLevel(level).Name;
            }
            catch
            {
                name = "-unknown skill-";
            }

            return name;
        }

        public static string GetSkillDesc(uint id, uint level, int desc)
        {
            string name = "-unknown skill desc-";

            try
            {
                switch (desc)
                {
                    case 1:
                        name = ((SkillList)Globals.skilllist[id]).GetLevel(level).Desc1;
                        break;
                    case 2:
                        name = ((SkillList)Globals.skilllist[id]).GetLevel(level).Desc2;
                        break;
                    case 3:
                        name = ((SkillList)Globals.skilllist[id]).GetLevel(level).Desc3;
                        break;
                    default:
                        name = "-unknown skill desc-";
                        break;
                }
            }
            catch
            {
                name = "-unknown skill desc-";
            }

            return name;
        }

        static public UserSkill GetSkill(uint id)
        {
            try
            {
                return ((UserSkill)Globals.gamedata.skills[id]);
            }
            catch
            {
            }
            return null;
        }

        static public string GetCharName(uint id)
        {
            //is self?
            if (id == Globals.gamedata.my_char.ID)
            {
                return Globals.gamedata.my_char.Name;
            }

            //is it a party member?
            Globals.PartyLock.EnterReadLock();
            try
            {
                PartyMember pmem = Util.GetCharParty(id);

                if (pmem != null)
                {
                    return pmem.Name;
                }
            }
            finally
            {
                Globals.PartyLock.ExitReadLock();
            }

            //is in the list of chars?
            Globals.PlayerLock.EnterReadLock();
            try
            {
                CharInfo player = Util.GetChar(id);

                if (player != null)
                {
                    return player.Name;
                }
            }
            finally
            {
                Globals.PlayerLock.ExitReadLock();
            }

            return "-nobody-";
        }

        static public uint GetCharID(string str)
        {
            str = str.ToUpperInvariant();

            //is self?
            if (System.String.Equals(str, Globals.gamedata.my_char.Name.ToUpperInvariant()))
            {
                return Globals.gamedata.my_char.ID;
            }

            //is it a party member?
            Globals.PartyLock.EnterReadLock();
            try
            {
                PartyMember pmem = Util.GetCharParty(str);

                if (pmem != null)
                {
                    return pmem.ID;
                }
            }
            finally
            {
                Globals.PartyLock.ExitReadLock();
            }

            //is in the list of chars?
            Globals.PlayerLock.EnterReadLock();
            try
            {
                CharInfo player = Util.GetChar(str);

                if (player != null)
                {
                    return player.ID;
                }
            }
            finally
            {
                Globals.PlayerLock.ExitReadLock();
            }

            return 0;
        }

        static public void GetLoc(uint id, ref int x, ref int y, ref int z)
        {
            //is self?
            if (Globals.gamedata.my_char.ID == id)
            {
                x = Util.Float_Int32(Globals.gamedata.my_char.X);
                y = Util.Float_Int32(Globals.gamedata.my_char.Y);
                z = Util.Float_Int32(Globals.gamedata.my_char.Z);
                return;
            }

            //is my pet?
            if (Globals.gamedata.my_pet.ID == id)
            {
                x = Util.Float_Int32(Globals.gamedata.my_pet.X);
                y = Util.Float_Int32(Globals.gamedata.my_pet.Y);
                z = Util.Float_Int32(Globals.gamedata.my_pet.Z);
                return;
            }
            if (Globals.gamedata.my_pet1.ID == id)
            {
                x = Util.Float_Int32(Globals.gamedata.my_pet1.X);
                y = Util.Float_Int32(Globals.gamedata.my_pet1.Y);
                z = Util.Float_Int32(Globals.gamedata.my_pet1.Z);
                return;
            }
            if (Globals.gamedata.my_pet2.ID == id)
            {
                x = Util.Float_Int32(Globals.gamedata.my_pet2.X);
                y = Util.Float_Int32(Globals.gamedata.my_pet2.Y);
                z = Util.Float_Int32(Globals.gamedata.my_pet2.Z);
                return;
            }
            if (Globals.gamedata.my_pet3.ID == id)
            {
                x = Util.Float_Int32(Globals.gamedata.my_pet3.X);
                y = Util.Float_Int32(Globals.gamedata.my_pet3.Y);
                z = Util.Float_Int32(Globals.gamedata.my_pet3.Z);
                return;
            }

            //is in the list of chars?
            Globals.PlayerLock.EnterReadLock();
            try
            {
                CharInfo player = Util.GetChar(id);

                if (player != null)
                {
                    x = Util.Float_Int32(player.X);
                    y = Util.Float_Int32(player.Y);
                    z = Util.Float_Int32(player.Z);
                    return;
                }
            }
            finally
            {
                Globals.PlayerLock.ExitReadLock();
            }

            //is it a party member?
            Globals.PartyLock.EnterReadLock();
            try
            {
                PartyMember pmem = Util.GetCharParty(id);

                if (pmem != null)
                {
                    x = pmem.X;
                    y = pmem.Y;
                    z = pmem.Z;
                    return;
                }
            }
            finally
            {
                Globals.PartyLock.ExitReadLock();
            }

            Globals.NPCLock.EnterReadLock();
            try
            {
                NPCInfo npc = Util.GetNPC(id);

                if (npc != null)
                {
                    x = Util.Float_Int32(npc.X);
                    y = Util.Float_Int32(npc.Y);
                    z = Util.Float_Int32(npc.Z);
                    return;
                }
            }
            finally
            {
                Globals.NPCLock.ExitReadLock();
            }
        }

        static public void GetCharLoc(uint id, ref int x, ref int y, ref int z)
        {
            //this is for getting the location of a player, self or otherwise, for the purpose of buffing
            //dont give 2 shits about buffing mobs/items

            //is self?
            if (Globals.gamedata.my_char.ID == id)
            {
                x = Util.Float_Int32(Globals.gamedata.my_char.X);
                y = Util.Float_Int32(Globals.gamedata.my_char.Y);
                z = Util.Float_Int32(Globals.gamedata.my_char.Z);
                return;
            }

            //is my pet?
            if (Globals.gamedata.my_pet.ID == id)
            {
                x = Util.Float_Int32(Globals.gamedata.my_pet.X);
                y = Util.Float_Int32(Globals.gamedata.my_pet.Y);
                z = Util.Float_Int32(Globals.gamedata.my_pet.Z);
                return;
            }
            if (Globals.gamedata.my_pet1.ID == id)
            {
                x = Util.Float_Int32(Globals.gamedata.my_pet1.X);
                y = Util.Float_Int32(Globals.gamedata.my_pet1.Y);
                z = Util.Float_Int32(Globals.gamedata.my_pet1.Z);
                return;
            }
            if (Globals.gamedata.my_pet2.ID == id)
            {
                x = Util.Float_Int32(Globals.gamedata.my_pet2.X);
                y = Util.Float_Int32(Globals.gamedata.my_pet2.Y);
                z = Util.Float_Int32(Globals.gamedata.my_pet2.Z);
                return;
            }
            if (Globals.gamedata.my_pet3.ID == id)
            {
                x = Util.Float_Int32(Globals.gamedata.my_pet3.X);
                y = Util.Float_Int32(Globals.gamedata.my_pet3.Y);
                z = Util.Float_Int32(Globals.gamedata.my_pet3.Z);
                return;
            }

            //is in the list of chars?
            Globals.PlayerLock.EnterReadLock();
            try
            {
                CharInfo player = Util.GetChar(id);

                if (player != null)
                {
                    x = Util.Float_Int32(player.X);
                    y = Util.Float_Int32(player.Y);
                    z = Util.Float_Int32(player.Z);
                    return;
                }
            }
            finally
            {
                Globals.PlayerLock.ExitReadLock();
            }

            //is it a party member?
            Globals.PartyLock.EnterReadLock();
            try
            {
                PartyMember pmem = Util.GetCharParty(id);

                if (pmem != null)
                {
                    x = pmem.X;
                    y = pmem.Y;
                    z = pmem.Z;
                    return;
                }
            }
            finally
            {
                Globals.PartyLock.ExitReadLock();
            }
        }

        static public void GetNPCLoc(uint id, ref int x, ref int y, ref int z)
        {
            Globals.NPCLock.EnterReadLock();
            try
            {
                NPCInfo npc = Util.GetNPC(id);

                if (npc != null)
                {
                    x = Util.Float_Int32(npc.X);
                    y = Util.Float_Int32(npc.Y);
                    z = Util.Float_Int32(npc.Z);
                }
            }
            finally
            {
                Globals.NPCLock.ExitReadLock();
            }

            return;
        }

        public static TargetType GetType(uint id)
        {
            if (id == 0)
            {
                return TargetType.NONE;
            }
            if (Globals.gamedata.my_char.ID == id)
            {
                return TargetType.SELF;
            }
            if (Globals.gamedata.my_pet.ID == id)
            {
                return TargetType.MYPET;
            }
            if (Globals.gamedata.my_pet1.ID == id)
            {
                return TargetType.MYPET1;
            }
            if (Globals.gamedata.my_pet2.ID == id)
            {
                return TargetType.MYPET2;
            }
            if (Globals.gamedata.my_pet3.ID == id)
            {
                return TargetType.MYPET3;
            }

            Globals.PlayerLock.EnterReadLock();
            try
            {
                if (isChar(id))
                {
                    return TargetType.PLAYER;
                }
            }
            finally
            {
                Globals.PlayerLock.ExitReadLock();
            }

            Globals.NPCLock.EnterReadLock();
            try
            {
                if (isNPC(id))
                {
                    return TargetType.NPC;
                }
            }
            finally
            {
                Globals.NPCLock.ExitReadLock();
            }

            Globals.ItemLock.EnterReadLock();
            try
            {
                if (isItem(id))
                {
                    return TargetType.ITEM;
                }
            }
            finally
            {
                Globals.ItemLock.ExitReadLock();
            }

            return TargetType.ERROR;
        }

        public static bool isChar(uint id)
        {
            if (Globals.gamedata.my_char.ID == id)
            {
                return true;
            }
            else if (Globals.gamedata.nearby_chars.ContainsKey(id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsPartyMember(uint id)
        {
            try
            {
                return Globals.gamedata.PartyMembers.ContainsKey(id);
            }
            catch
            {
            }

            return false;
        }

        public static PartyMember GetCharParty(uint id)
        {
            try
            {
                return ((PartyMember)Globals.gamedata.PartyMembers[id]);
            }
            catch
            {
            }

            return null;
        }

        public static PartyMember GetCharParty(string str)
        {
            str = str.ToUpperInvariant();

            foreach (PartyMember p in Globals.gamedata.PartyMembers.Values)
            {
                if (System.String.Equals(p.Name.ToUpperInvariant(), str))
                {
                    return p;
                }
            }

            return null;
        }

        public static CharInfo GetChar(uint id)
        {
            try
            {
                return ((CharInfo)Globals.gamedata.nearby_chars[id]);
            }
            catch
            {
            }
            return null;
        }

        public static CharInfo GetChar(string str)
        {
            str = str.ToUpperInvariant();

            foreach (CharInfo player in Globals.gamedata.nearby_chars.Values)
            {
                if (System.String.Equals(player.Name.ToUpperInvariant(), str))
                {
                    return player;
                }
            }

            return null;
        }

        public static bool isNPC(uint id)
        {
            if (Globals.gamedata.nearby_npcs.ContainsKey(id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static NPCInfo GetNPC(uint id)
        {
            try
            {
                return ((NPCInfo)Globals.gamedata.nearby_npcs[id]);
            }
            catch
            {
                return null;
            }
        }

        public static CharBuff GetBuff(uint id)
        {
            try
            {
                return ((CharBuff)Globals.gamedata.mybuffs[id]);
            }
            catch
            {
                return null;
            }
        }

        public static void IgnoreItem(uint id, bool ignore)
        {
            Globals.ItemLock.EnterWriteLock();
            try
            {
                ((ItemInfo)Globals.gamedata.nearby_items[id]).Ignore = ignore;
            }
           // catch(Exception e)
           // {
                //Globals.l2net_home.Add_Error("Crashed ignoring item " + e.Message);
           // }
            finally
            {
                Globals.ItemLock.ExitWriteLock();
            }
        }

        public static bool isItem(uint id)
        {
            if (Globals.gamedata.nearby_items.ContainsKey(id))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static ItemInfo GetItem(uint id)
        {
            try
            {
                return ((ItemInfo)Globals.gamedata.nearby_items[id]);
            }
            catch
            {
                return null;
            }
        }

        public static InventoryInfo GetInventory(uint id)
        {
            try
            {
                return ((InventoryInfo)Globals.gamedata.inventory[id]);
            }
            catch
            {
                return null;
            }
        }

        public static string GetNPCName(uint id)
        {
            try
            {
                return ((NPCName)Globals.npcname[(uint)(id - Globals.NPC_OFF)]).Name;
            }
            catch
            {
                return Globals.UnknownNPC;
            }
        }

        public static string GetNPCTitle(uint id)
        {
            try
            {
                return ((NPCName)Globals.npcname[(uint)(id - Globals.NPC_OFF)]).Description;
            }
            catch
            {
                return Globals.UnknownTitle;
            }
        }

        public static uint GetNPCID(string str)
        {
            str = str.ToUpperInvariant();

            foreach (NPCName npc in Globals.npcname.Values)
            {
                if (System.String.Equals(npc.Name.ToUpperInvariant(), str))
                {
                    return npc.ID;
                }
            }

            return 0;
        }

        public static void IgnoreNPC(uint id, bool ignore)
        {
            Globals.NPCLock.EnterReadLock();
            try
            {
                ((NPCInfo)Globals.gamedata.nearby_npcs[id]).Ignore = ignore;
            }
            catch
            {
            }
            finally
            {
                Globals.NPCLock.ExitReadLock();
            }
        }

        public static string GetItemName(uint id)
        {
            try
            {
                return ((ItemName)Globals.itemname[id]).Name;
            }
            catch
            {
                return Globals.UnknownItem;
            }
        }

        public static bool GetItemHasMesh(uint id)
        {
            try
            {
                return ((ItemName)Globals.itemname[id]).Has_Mesh;
            }
            catch
            {
                return false;
            }
        }

        public static string GetItemDescription(uint id)
        {
            try
            {
                return ((ItemName)Globals.itemname[id]).Description;
            }
            catch
            {
                return Globals.UnknownItemDesc;
            }
        }

        public static string GetItemImagePath(uint id)
        {
            try
            {
                return Globals.PATH + "\\Icons\\" + ((ItemName)Globals.itemname[id]).Icon + "_0.BMP";
            }
            catch
            {
                return Globals.UnknownItemImagePath;
            }
        }

        public static string GetRace(uint id)
        {
            try
            {
                return ((Races)Globals.races[id]).Name;
            }
            catch
            {
                return Globals.UnknownRace;
            }
        }

        public static string GetServer(uint id)
        {
            try
            {
                return ((ServerName)Globals.servername[id]).Name;
            }
            catch
            {
                return Globals.UnknownServer;
            }
        }

        public static string GetClass(uint id)
        {
            try
            {
                return ((Classes)Globals.classes[id]).Name;
            }
            catch
            {
                return Globals.UnknownClass;
            }
        }

        public static string GetClassNick(uint id)
        {
            try
            {
                return ((string)Globals.gamedata.ClassNick[id]);
            }
            catch
            {
                return Globals.UnknownClass;
            }
        }

        public static uint GetItemID(string str)
        {
            str = str.ToUpperInvariant();

            foreach (ItemName itm in Globals.itemname.Values)
            {
                if (System.String.Equals(itm.Name.ToUpperInvariant(), str))
                {
                    return itm.ID;
                }
            }

            return 0;
        }

        public static uint GetInventoryItemID(uint id)
        {
            //I hope I didn't break something... -mpj123
            //Globals.InventoryLock.EnterReadLock();
            lock (Globals.InventoryLock)
            {
                try
                {
                    foreach (InventoryInfo inv_inf in Globals.gamedata.inventory.Values)
                    {
                        if (inv_inf.ID == id)
                            return inv_inf.ItemID;
                    }
                }//unlock
                finally
                {
                    //Globals.InventoryLock.ExitReadLock();
                }
            }

            return 0;
        }

        public static uint GetInventoryUID(uint id)
        {
            Globals.InventoryLock.EnterReadLock();
            try
            {
                foreach (InventoryInfo inv_inf in Globals.gamedata.inventory.Values)
                {
                    if (inv_inf.ItemID == id)
                        return inv_inf.ID;
                }
            }//unlock
            finally
            {
                Globals.InventoryLock.ExitReadLock();
            }

            return 0;
        }
    }
}
