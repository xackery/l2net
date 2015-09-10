using System;

namespace L2_login
{
	/// <summary>
	/// Summary description for Script_Ops.
	/// </summary>
	public class Script_Ops
	{
		public Script_Ops()
		{
		}

        public static long NEAREST_PLAYER_DISTANCE(bool return_distance)
		{
            float mx = Globals.gamedata.my_char.X;
            float my = Globals.gamedata.my_char.Y;
            float mz = Globals.gamedata.my_char.Z;
            uint player_id = 0;
            long dist = long.MaxValue;
            long ndist;

            Globals.PlayerLock.EnterReadLock();
			try
			{
                foreach(CharInfo player in Globals.gamedata.nearby_chars.Values)
				{
                    ndist = System.Convert.ToInt64(System.Math.Sqrt(System.Math.Pow(mx - player.X, 2) + System.Math.Pow(my - player.Y, 2) + System.Math.Pow(mz - player.Z, 2)));

					if(ndist < dist)
					{
                        player_id = player.ID;
						dist = ndist;
					}
				}
			}
			finally
			{
                Globals.PlayerLock.ExitReadLock();
			}

            if (return_distance)
            {
                return (long)dist;
            }
            else
            {
                return (long)player_id;
            }
		}

        public static long NEAREST_NPC_DISTANCE(bool return_distance)
		{
            float mx = Globals.gamedata.my_char.X;
            float my = Globals.gamedata.my_char.Y;
            float mz = Globals.gamedata.my_char.Z;
            uint npc_id = 0;
            long dist = long.MaxValue;
            long ndist;

            Globals.DoNotNPCLock.EnterReadLock();
            Globals.NPCLock.EnterReadLock();
			try
			{
                foreach(NPCInfo npc in Globals.gamedata.nearby_npcs.Values)
				{
                    if (BotOptions.DoNotNPCs.Contains(npc.NPCID))
                    {
                        //break;
                    }
                    else
                    {
                        ndist = System.Convert.ToInt64(System.Math.Sqrt(System.Math.Pow(mx - npc.X, 2) + System.Math.Pow(my - npc.Y, 2) + System.Math.Pow(mz - npc.Z, 2)));

                        if (ndist < dist)
                        {
                            npc_id = npc.ID;
                            dist = ndist;
                        }
                    }
				}
			}
			finally
			{
                Globals.NPCLock.ExitReadLock();
                Globals.DoNotNPCLock.ExitReadLock();
			}

            if (return_distance)
            {
                return (long)dist;
            }
            else
            {
                return (long)npc_id;
            }
		}

		public static uint NEAREST_ITEM_DISTANCE(bool return_distance)
		{
            float mx = Globals.gamedata.my_char.X;
            float my = Globals.gamedata.my_char.Y;
            float mz = Globals.gamedata.my_char.Z;
            uint item_id = 0;
            uint dist = uint.MaxValue;
            uint ndist;
            bool myitem;

            Globals.DoNotItemLock.EnterReadLock();
            Globals.ItemLock.EnterReadLock();
			try
			{
                foreach (ItemInfo item in Globals.gamedata.nearby_items.Values)
                {
                    myitem = false;
                    //whitelist
                    if (Globals.gamedata.botoptions.PickOnlyItemsInList == 1)
                    {
                        //main checks, we dont really care about mesh and stuff since it's a whitelist...
                        if ((item.Ignore == false && ((item.HasValidZ && !item.Loadedin) || (item.Loadedin && Math.Abs(item.Z - Globals.gamedata.my_char.Z) < Globals.PICKUP_Z_Diff))) && ((Globals.gamedata.botoptions.IgnoreItems == 1 && Util.GetItemName(item.ItemID) != Globals.UnknownItem) || Globals.gamedata.botoptions.IgnoreItems == 0))
                        {
                            //contains the item? if so, we can pick it up
                            if (BotOptions.DoNotItems.Contains(item.ItemID))
                            {
                                if (Globals.gamedata.botoptions.OnlyPickMine == 1)
                                {
                                    try
                                    {
                                        Globals.MobListLock.EnterReadLock();
                                        //lets check for mobs that were engaged with us, the droppedby value can be checked with our moblist
                                        if (Globals.gamedata.MobList.Contains(item.DroppedBy))
                                        {
                                            myitem = true;
                                            //Globals.l2net_home.Add_Text("My item: " + Util.GetItemName(item.ItemID));
                                        }
                                    }
                                    finally
                                    {
                                        Globals.MobListLock.ExitReadLock();
                                    }
                                }

                                if (myitem || Globals.gamedata.botoptions.OnlyPickMine == 0)
                                {
                                    //if everything checks out, let's register this item and keep trying to find a closer one.
                                    ndist = System.Convert.ToUInt32(System.Math.Sqrt(System.Math.Pow(mx - item.X, 2) + System.Math.Pow(my - item.Y, 2) + System.Math.Pow(mz - item.Z, 2)));

                                    if (ndist < dist)
                                    {
                                        item_id = item.ID;
                                        dist = ndist;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //dont care about the item
                        }
                    }
                    //donot without whitelist
                    else
                    {
                        //main checks, check for everything.
                        if (((item.Ignore == false && item.HasMesh && ((item.HasValidZ && !item.Loadedin) || (item.Loadedin && Math.Abs(item.Z - Globals.gamedata.my_char.Z) < Globals.PICKUP_Z_Diff)) && Globals.gamedata.botoptions.IgnoreMeshlessItems == 1) || (item.Ignore == false && Globals.gamedata.botoptions.IgnoreMeshlessItems == 0)) &&
                          ((Globals.gamedata.botoptions.IgnoreItems == 1 && Util.GetItemName(item.ItemID) != Globals.UnknownItem) || Globals.gamedata.botoptions.IgnoreItems == 0))
                        {
                            //does not contain the item? if so, we can pick it up
                            if (!BotOptions.DoNotItems.Contains(item.ItemID))
                            {
                                if (Globals.gamedata.botoptions.OnlyPickMine == 1)
                                {
                                    try
                                    {
                                        Globals.MobListLock.EnterReadLock();
                                        if (Globals.gamedata.MobList.Contains(item.DroppedBy))
                                        {
                                            myitem = true;
                                            //Globals.l2net_home.Add_Text("My item: " + Util.GetItemName(item.ItemID));
                                        }
                                    }
                                    finally
                                    {
                                        Globals.MobListLock.ExitReadLock();
                                    }
                                }
                                if (myitem || Globals.gamedata.botoptions.OnlyPickMine == 0)
                                {
                                    //if everything checks out, let's register this item and keep trying to find a closer one.
                                    ndist = System.Convert.ToUInt32(System.Math.Sqrt(System.Math.Pow(mx - item.X, 2) + System.Math.Pow(my - item.Y, 2) + System.Math.Pow(mz - item.Z, 2)));

                                    if (ndist < dist)
                                    {
                                        item_id = item.ID;
                                        dist = ndist;
                                    }
                                }
                            }
                        }
                        else
                        {
                            //dont care about the item
                        }
                    }
                }
			}
			finally
			{
                Globals.ItemLock.ExitReadLock();
                Globals.DoNotItemLock.ExitReadLock();
			}

            if (return_distance)
            {
                return dist;
            }
            else
            {
                return item_id;
            }
		}

		public static string TARGET_STRING(string req)
		{
            switch (Globals.gamedata.my_char.CurrentTargetType)
            {
                case TargetType.ERROR:
                case TargetType.NONE:
                    break;
                case TargetType.SELF:
                    switch (req)
                    {
                        case "NAME":
                            return Globals.gamedata.my_char.Name;
                        case "TITLE":
                            return Globals.gamedata.my_char.Title;
                        case "CLAN":
                            return ((Clan_Info)Globals.clanlist[Globals.gamedata.my_char.ClanID]).ClanName;
                        case "ALLY":
                            return ((Clan_Info)Globals.clanlist[Globals.gamedata.my_char.ClanID]).AllyName;
                        default:
                            ScriptEngine.Script_Error("invalid invalid target data(self) request");
                            break;
                    }
                    break;
                case TargetType.MYPET:
                    switch (req)
                    {
                        case "NAME":
                            return Globals.gamedata.my_pet.Name;
                        case "TITLE":
                            return Globals.gamedata.my_pet.Title;
                        case "CLAN":
                            return ((Clan_Info)Globals.clanlist[Globals.gamedata.my_char.ClanID]).ClanName;
                        case "ALLY":
                            return ((Clan_Info)Globals.clanlist[Globals.gamedata.my_char.ClanID]).AllyName;
                        default:
                            ScriptEngine.Script_Error("invalid invalid target data(my pet) request");
                            break;
                    }
                    break;
                case TargetType.MYPET1:
                    switch (req)
                    {
                        case "NAME":
                            return Globals.gamedata.my_pet1.Name;
                        case "TITLE":
                            return Globals.gamedata.my_pet1.Title;
                        case "CLAN":
                            return ((Clan_Info)Globals.clanlist[Globals.gamedata.my_char.ClanID]).ClanName;
                        case "ALLY":
                            return ((Clan_Info)Globals.clanlist[Globals.gamedata.my_char.ClanID]).AllyName;
                        default:
                            ScriptEngine.Script_Error("invalid invalid target data(my pet) request");
                            break;
                    }
                    break;
                case TargetType.MYPET2:
                    switch (req)
                    {
                        case "NAME":
                            return Globals.gamedata.my_pet2.Name;
                        case "TITLE":
                            return Globals.gamedata.my_pet2.Title;
                        case "CLAN":
                            return ((Clan_Info)Globals.clanlist[Globals.gamedata.my_char.ClanID]).ClanName;
                        case "ALLY":
                            return ((Clan_Info)Globals.clanlist[Globals.gamedata.my_char.ClanID]).AllyName;
                        default:
                            ScriptEngine.Script_Error("invalid invalid target data(my pet) request");
                            break;
                    }
                    break;
                case TargetType.MYPET3:
                    switch (req)
                    {
                        case "NAME":
                            return Globals.gamedata.my_pet3.Name;
                        case "TITLE":
                            return Globals.gamedata.my_pet3.Title;
                        case "CLAN":
                            return ((Clan_Info)Globals.clanlist[Globals.gamedata.my_char.ClanID]).ClanName;
                        case "ALLY":
                            return ((Clan_Info)Globals.clanlist[Globals.gamedata.my_char.ClanID]).AllyName;
                        default:
                            ScriptEngine.Script_Error("invalid invalid target data(my pet) request");
                            break;
                    }
                    break;
                case TargetType.PLAYER:
                    Globals.PlayerLock.EnterReadLock();
                    try
                    {
                        CharInfo player = Util.GetChar(Globals.gamedata.my_char.TargetID);

                        if (player != null)
                        {
                            switch (req)
                            {
                                case "NAME":
                                    return player.Name;
                                case "TITLE":
                                    return player.Title;
                                case "CLAN":
                                    return ((Clan_Info)Globals.clanlist[player.ClanID]).ClanName;
                                case "ALLY":
                                    return ((Clan_Info)Globals.clanlist[player.ClanID]).AllyName;
                                default:
                                    ScriptEngine.Script_Error("invalid invalid target data(player) request");
                                    break;
                            }
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
                        NPCInfo npc = Util.GetNPC(Globals.gamedata.my_char.TargetID);

                        if (npc != null)
                        {
                            switch (req)
                            {
                                case "NAME":
                                    return Util.GetNPCName(npc.NPCID);
                                case "TITLE":
                                    return npc.Title;
                                default:
                                    ScriptEngine.Script_Error("invalid target data(npc) request");
                                    break;
                            }
                        }
                    }//unlock
                    finally
                    {
                        Globals.NPCLock.ExitReadLock();
                    }
                    break;
            }

			return "";
		}

		public static long TARGET_INT(string req)
		{
            switch (Globals.gamedata.my_char.CurrentTargetType)
            {
                case TargetType.ERROR:
                case TargetType.NONE:
                    break;
                case TargetType.SELF:
                    switch (req)
                    {
                        case "KARMA":
                            return (long)Globals.gamedata.my_char.Karma;
                        case "TARGETID":
                            return (long)Globals.gamedata.my_char.TargetID;
                        case "ID":
                            return (long)Globals.gamedata.my_char.ID;
                        case "TYPEID":
                            return (long)0;
                        case "X":
                            return (long)Globals.gamedata.my_char.X;
                        case "Y":
                            return (long)Globals.gamedata.my_char.Y;
                        case "Z":
                            return (long)Globals.gamedata.my_char.Z;
                        case "DESTX":
                            return (long)Globals.gamedata.my_char.Dest_X;
                        case "DESTY":
                            return (long)Globals.gamedata.my_char.Dest_Y;
                        case "DESTZ":
                            return (long)Globals.gamedata.my_char.Dest_Z;
                        case "IS_MOVING":
                            return Globals.gamedata.my_char.Moving ? 1L : 0L;
                        case "MAX_HP":
                            return (long)Globals.gamedata.my_char.Max_HP;
                        case "MAX_MP":
                            return (long)Globals.gamedata.my_char.Max_MP;
                        case "MAX_CP":
                            return (long)Globals.gamedata.my_char.Max_CP;
                        case "CUR_HP":
                            return (long)Globals.gamedata.my_char.Cur_HP;
                        case "CUR_MP":
                            return (long)Globals.gamedata.my_char.Cur_MP;
                        case "CUR_CP":
                            return (long)Globals.gamedata.my_char.Cur_CP;
                        case "PER_HP":
                            return (long)(100.0 * (Globals.gamedata.my_char.Cur_HP / Globals.gamedata.my_char.Max_HP));
                        case "PER_MP":
                            return (long)(100.0 * (Globals.gamedata.my_char.Cur_MP / Globals.gamedata.my_char.Max_MP));
                        case "PER_CP":
                            return (long)(100.0 * (Globals.gamedata.my_char.Cur_CP / Globals.gamedata.my_char.Max_CP));
                        case "RUN_SPEED":
                            return (long)(Globals.gamedata.my_char.RunSpeed * Globals.gamedata.my_char.MoveSpeedMult);
                        case "WALK_SPEED":
                            return (long)(Globals.gamedata.my_char.WalkSpeed * Globals.gamedata.my_char.MoveSpeedMult);
                        case "ATTACK_SPEED":
                            return (long)Globals.gamedata.my_char.AttackSpeedMult;
                        case "CAST_SPEED":
                            return (long)Globals.gamedata.my_char.MatkSpeed;
                        case "EVAL":
                            return (long)Globals.gamedata.my_char.RecAmount;
                        case "RUNNING":
                            return (long)Globals.gamedata.my_char.isRunning;
                        case "SITTING":
                            return (long)Globals.gamedata.my_char.isSitting;
                        case "LOOKS_DEAD":
                            return (long)Globals.gamedata.my_char.isAlikeDead;
                        default:
                            ScriptEngine.Script_Error("invalid target data(self) request");
                            break;
                    }
                    break;
                case TargetType.MYPET:
                    switch (req)
                    {
                        case "KARMA":
                            return (long)Globals.gamedata.my_pet.Karma;
                        case "TARGETID":
                            return (long)Globals.gamedata.my_pet.TargetID;
                        case "ID":
                            return (long)Globals.gamedata.my_pet.ID;
                        case "TYPEID":
                            return (long)Globals.gamedata.my_pet.NPCID;
                        case "X":
                            return (long)Globals.gamedata.my_pet.X;
                        case "Y":
                            return (long)Globals.gamedata.my_pet.Y;
                        case "Z":
                            return (long)Globals.gamedata.my_pet.Z;
                        case "DESTX":
                            return (long)Globals.gamedata.my_pet.Dest_X;
                        case "DESTY":
                            return (long)Globals.gamedata.my_pet.Dest_Y;
                        case "DESTZ":
                            return (long)Globals.gamedata.my_pet.Dest_Z;
                        case "IS_MOVING":
                            return Globals.gamedata.my_pet.Moving ? 1L : 0L;
                        case "MAX_HP":
                            return (long)Globals.gamedata.my_pet.Max_HP;
                        case "MAX_MP":
                            return (long)Globals.gamedata.my_pet.Max_MP;
                        case "MAX_CP":
                            return (long)Globals.gamedata.my_pet.Max_CP;
                        case "CUR_HP":
                            return (long)Globals.gamedata.my_pet.Cur_HP;
                        case "CUR_MP":
                            return (long)Globals.gamedata.my_pet.Cur_MP;
                        case "CUR_CP":
                            return (long)Globals.gamedata.my_pet.Cur_CP;
                        case "PER_HP":
                            return (long)(100.0 * (Globals.gamedata.my_pet.Cur_HP / Globals.gamedata.my_pet.Max_HP));
                        case "PER_MP":
                            return (long)(100.0 * (Globals.gamedata.my_pet.Cur_MP / Globals.gamedata.my_pet.Max_MP));
                        case "PER_CP":
                            return (long)(100.0 * (Globals.gamedata.my_pet.Cur_CP / Globals.gamedata.my_pet.Max_CP));
                        case "RUN_SPEED":
                            return (long)(Globals.gamedata.my_pet.RunSpeed * Globals.gamedata.my_pet.MoveSpeedMult);
                        case "WALK_SPEED":
                            return (long)(Globals.gamedata.my_pet.WalkSpeed * Globals.gamedata.my_pet.MoveSpeedMult);
                        case "ATTACK_SPEED":
                            return (long)Globals.gamedata.my_pet.AttackSpeedMult;
                        case "CAST_SPEED":
                            return (long)Globals.gamedata.my_pet.MatkSpeed;
                        case "EVAL":
                            return (long)0;
                        case "RUNNING":
                            return (long)Globals.gamedata.my_pet.isRunning;
                        case "SITTING":
                            return (long)0;
                        case "LOOKS_DEAD":
                            return (long)Globals.gamedata.my_pet.isAlikeDead;
                        default:
                            ScriptEngine.Script_Error("invalid target data(self) request");
                            break;
                    }
                    break;
                case TargetType.MYPET1:
                    switch (req)
                    {
                        case "KARMA":
                            return (long)Globals.gamedata.my_pet1.Karma;
                        case "TARGETID":
                            return (long)Globals.gamedata.my_pet1.TargetID;
                        case "ID":
                            return (long)Globals.gamedata.my_pet1.ID;
                        case "TYPEID":
                            return (long)Globals.gamedata.my_pet1.NPCID;
                        case "X":
                            return (long)Globals.gamedata.my_pet1.X;
                        case "Y":
                            return (long)Globals.gamedata.my_pet1.Y;
                        case "Z":
                            return (long)Globals.gamedata.my_pet1.Z;
                        case "DESTX":
                            return (long)Globals.gamedata.my_pet1.Dest_X;
                        case "DESTY":
                            return (long)Globals.gamedata.my_pet1.Dest_Y;
                        case "DESTZ":
                            return (long)Globals.gamedata.my_pet1.Dest_Z;
                        case "IS_MOVING":
                            return Globals.gamedata.my_pet1.Moving ? 1L : 0L;
                        case "MAX_HP":
                            return (long)Globals.gamedata.my_pet1.Max_HP;
                        case "MAX_MP":
                            return (long)Globals.gamedata.my_pet1.Max_MP;
                        case "MAX_CP":
                            return (long)Globals.gamedata.my_pet1.Max_CP;
                        case "CUR_HP":
                            return (long)Globals.gamedata.my_pet1.Cur_HP;
                        case "CUR_MP":
                            return (long)Globals.gamedata.my_pet1.Cur_MP;
                        case "CUR_CP":
                            return (long)Globals.gamedata.my_pet1.Cur_CP;
                        case "PER_HP":
                            return (long)(100.0 * (Globals.gamedata.my_pet1.Cur_HP / Globals.gamedata.my_pet1.Max_HP));
                        case "PER_MP":
                            return (long)(100.0 * (Globals.gamedata.my_pet1.Cur_MP / Globals.gamedata.my_pet1.Max_MP));
                        case "PER_CP":
                            return (long)(100.0 * (Globals.gamedata.my_pet1.Cur_CP / Globals.gamedata.my_pet1.Max_CP));
                        case "RUN_SPEED":
                            return (long)(Globals.gamedata.my_pet1.RunSpeed * Globals.gamedata.my_pet1.MoveSpeedMult);
                        case "WALK_SPEED":
                            return (long)(Globals.gamedata.my_pet1.WalkSpeed * Globals.gamedata.my_pet1.MoveSpeedMult);
                        case "ATTACK_SPEED":
                            return (long)Globals.gamedata.my_pet1.AttackSpeedMult;
                        case "CAST_SPEED":
                            return (long)Globals.gamedata.my_pet1.MatkSpeed;
                        case "EVAL":
                            return (long)0;
                        case "RUNNING":
                            return (long)Globals.gamedata.my_pet1.isRunning;
                        case "SITTING":
                            return (long)0;
                        case "LOOKS_DEAD":
                            return (long)Globals.gamedata.my_pet1.isAlikeDead;
                        default:
                            ScriptEngine.Script_Error("invalid target data(self) request");
                            break;
                    }
                    break;
                case TargetType.MYPET2:
                    switch (req)
                    {
                        case "KARMA":
                            return (long)Globals.gamedata.my_pet2.Karma;
                        case "TARGETID":
                            return (long)Globals.gamedata.my_pet2.TargetID;
                        case "ID":
                            return (long)Globals.gamedata.my_pet2.ID;
                        case "TYPEID":
                            return (long)Globals.gamedata.my_pet2.NPCID;
                        case "X":
                            return (long)Globals.gamedata.my_pet2.X;
                        case "Y":
                            return (long)Globals.gamedata.my_pet2.Y;
                        case "Z":
                            return (long)Globals.gamedata.my_pet2.Z;
                        case "DESTX":
                            return (long)Globals.gamedata.my_pet2.Dest_X;
                        case "DESTY":
                            return (long)Globals.gamedata.my_pet2.Dest_Y;
                        case "DESTZ":
                            return (long)Globals.gamedata.my_pet2.Dest_Z;
                        case "IS_MOVING":
                            return Globals.gamedata.my_pet2.Moving ? 1L : 0L;
                        case "MAX_HP":
                            return (long)Globals.gamedata.my_pet2.Max_HP;
                        case "MAX_MP":
                            return (long)Globals.gamedata.my_pet2.Max_MP;
                        case "MAX_CP":
                            return (long)Globals.gamedata.my_pet2.Max_CP;
                        case "CUR_HP":
                            return (long)Globals.gamedata.my_pet2.Cur_HP;
                        case "CUR_MP":
                            return (long)Globals.gamedata.my_pet2.Cur_MP;
                        case "CUR_CP":
                            return (long)Globals.gamedata.my_pet2.Cur_CP;
                        case "PER_HP":
                            return (long)(100.0 * (Globals.gamedata.my_pet2.Cur_HP / Globals.gamedata.my_pet2.Max_HP));
                        case "PER_MP":
                            return (long)(100.0 * (Globals.gamedata.my_pet2.Cur_MP / Globals.gamedata.my_pet2.Max_MP));
                        case "PER_CP":
                            return (long)(100.0 * (Globals.gamedata.my_pet2.Cur_CP / Globals.gamedata.my_pet2.Max_CP));
                        case "RUN_SPEED":
                            return (long)(Globals.gamedata.my_pet2.RunSpeed * Globals.gamedata.my_pet2.MoveSpeedMult);
                        case "WALK_SPEED":
                            return (long)(Globals.gamedata.my_pet2.WalkSpeed * Globals.gamedata.my_pet2.MoveSpeedMult);
                        case "ATTACK_SPEED":
                            return (long)Globals.gamedata.my_pet2.AttackSpeedMult;
                        case "CAST_SPEED":
                            return (long)Globals.gamedata.my_pet2.MatkSpeed;
                        case "EVAL":
                            return (long)0;
                        case "RUNNING":
                            return (long)Globals.gamedata.my_pet2.isRunning;
                        case "SITTING":
                            return (long)0;
                        case "LOOKS_DEAD":
                            return (long)Globals.gamedata.my_pet2.isAlikeDead;
                        default:
                            ScriptEngine.Script_Error("invalid target data(self) request");
                            break;
                    }
                    break;
                case TargetType.MYPET3:
                    switch (req)
                    {
                        case "KARMA":
                            return (long)Globals.gamedata.my_pet3.Karma;
                        case "TARGETID":
                            return (long)Globals.gamedata.my_pet3.TargetID;
                        case "ID":
                            return (long)Globals.gamedata.my_pet3.ID;
                        case "TYPEID":
                            return (long)Globals.gamedata.my_pet3.NPCID;
                        case "X":
                            return (long)Globals.gamedata.my_pet3.X;
                        case "Y":
                            return (long)Globals.gamedata.my_pet3.Y;
                        case "Z":
                            return (long)Globals.gamedata.my_pet3.Z;
                        case "DESTX":
                            return (long)Globals.gamedata.my_pet3.Dest_X;
                        case "DESTY":
                            return (long)Globals.gamedata.my_pet3.Dest_Y;
                        case "DESTZ":
                            return (long)Globals.gamedata.my_pet3.Dest_Z;
                        case "IS_MOVING":
                            return Globals.gamedata.my_pet3.Moving ? 1L : 0L;
                        case "MAX_HP":
                            return (long)Globals.gamedata.my_pet3.Max_HP;
                        case "MAX_MP":
                            return (long)Globals.gamedata.my_pet3.Max_MP;
                        case "MAX_CP":
                            return (long)Globals.gamedata.my_pet3.Max_CP;
                        case "CUR_HP":
                            return (long)Globals.gamedata.my_pet3.Cur_HP;
                        case "CUR_MP":
                            return (long)Globals.gamedata.my_pet3.Cur_MP;
                        case "CUR_CP":
                            return (long)Globals.gamedata.my_pet3.Cur_CP;
                        case "PER_HP":
                            return (long)(100.0 * (Globals.gamedata.my_pet3.Cur_HP / Globals.gamedata.my_pet3.Max_HP));
                        case "PER_MP":
                            return (long)(100.0 * (Globals.gamedata.my_pet3.Cur_MP / Globals.gamedata.my_pet3.Max_MP));
                        case "PER_CP":
                            return (long)(100.0 * (Globals.gamedata.my_pet3.Cur_CP / Globals.gamedata.my_pet3.Max_CP));
                        case "RUN_SPEED":
                            return (long)(Globals.gamedata.my_pet3.RunSpeed * Globals.gamedata.my_pet3.MoveSpeedMult);
                        case "WALK_SPEED":
                            return (long)(Globals.gamedata.my_pet3.WalkSpeed * Globals.gamedata.my_pet3.MoveSpeedMult);
                        case "ATTACK_SPEED":
                            return (long)Globals.gamedata.my_pet3.AttackSpeedMult;
                        case "CAST_SPEED":
                            return (long)Globals.gamedata.my_pet3.MatkSpeed;
                        case "EVAL":
                            return (long)0;
                        case "RUNNING":
                            return (long)Globals.gamedata.my_pet3.isRunning;
                        case "SITTING":
                            return (long)0;
                        case "LOOKS_DEAD":
                            return (long)Globals.gamedata.my_pet3.isAlikeDead;
                        default:
                            ScriptEngine.Script_Error("invalid target data(self) request");
                            break;
                    }
                    break;
                case TargetType.PLAYER:
                    Globals.PlayerLock.EnterReadLock();
                    try
                    {
                        CharInfo player = Util.GetChar(Globals.gamedata.my_char.TargetID);

                        if(player != null)
                        {
                            switch (req)
                            {
                                case "KARMA":
                                    return (long)player.Karma;
                                case "TARGETID":
                                    return (long)player.TargetID;
                                case "ID":
                                    return (long)player.ID;
                                case "TYPEID":
                                    return (long)0;
                                case "X":
                                    return (long)player.X;
                                case "Y":
                                    return (long)player.Y;
                                case "Z":
                                    return (long)player.Z;
                                case "DESTX":
                                    return (long)player.Dest_X;
                                case "DESTY":
                                    return (long)player.Dest_Y;
                                case "DESTZ":
                                    return (long)player.Dest_Z;
                                case "IS_MOVING":
                                    return player.Moving ? 1L : 0L;
                                case "MAX_HP":
                                    return (long)player.Max_HP;
                                case "MAX_MP":
                                    return (long)player.Max_MP;
                                case "MAX_CP":
                                    return (long)player.Max_CP;
                                case "CUR_HP":
                                    return (long)player.Cur_HP;
                                case "CUR_MP":
                                    return (long)player.Cur_MP;
                                case "CUR_CP":
                                    return (long)player.Cur_CP;
                                case "PER_HP":
                                    return (long)(100.0 * (player.Cur_HP / player.Max_HP));
                                case "PER_MP":
                                    return (long)(100.0 * (player.Cur_MP / player.Max_MP));
                                case "PER_CP":
                                    return (long)(100.0 * (player.Cur_CP / player.Max_CP));
                                case "RUN_SPEED":
                                    return (long)(player.RunSpeed * Globals.gamedata.my_char.MoveSpeedMult);
                                case "WALK_SPEED":
                                    return (long)(player.WalkSpeed * Globals.gamedata.my_char.MoveSpeedMult);
                                case "ATTACK_SPEED":
                                    return (long)player.AttackSpeedMult;
                                case "CAST_SPEED":
                                    return (long)player.MatkSpeed;
                                case "EVAL":
                                    return (long)player.RecAmount;
                                case "RUNNING":
                                    return (long)player.isRunning;
                                case "SITTING":
                                    return (long)player.isSitting;
                                case "LOOKS_DEAD":
                                    return (long)player.isAlikeDead;
                                default:
                                    ScriptEngine.Script_Error("invalid target data(player) request");
                                    break;
                            }
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
                        NPCInfo npc = Util.GetNPC(Globals.gamedata.my_char.TargetID);

                        if(npc != null)
                        {
                            switch (req)
                            {
                                case "KARMA":
                                    return (long)npc.Karma;
                                case "TARGETID":
                                    return (long)npc.TargetID;
                                case "ID":
                                    return (long)npc.ID;
                                case "TYPEID":
                                    return (long)npc.NPCID;
                                case "X":
                                    return (long)npc.X;
                                case "Y":
                                    return (long)npc.Y;
                                case "Z":
                                    return (long)npc.Z;
                                case "DESTX":
                                    return (long)npc.Dest_X;
                                case "DESTY":
                                    return (long)npc.Dest_Y;
                                case "DESTZ":
                                    return (long)npc.Dest_Z;
                                case "IS_MOVING":
                                    return npc.Moving ? 1L : 0L;
                                case "MAX_HP":
                                    return (long)npc.Max_HP;
                                case "MAX_MP":
                                    return (long)npc.Max_MP;
                                case "MAX_CP":
                                    return (long)npc.Max_CP;
                                case "CUR_HP":
                                    return (long)npc.Cur_HP;
                                case "CUR_MP":
                                    return (long)npc.Cur_MP;
                                case "CUR_CP":
                                    return (long)npc.Cur_CP;
                                case "PER_HP":
                                    return (long)(100.0 * (npc.Cur_HP / npc.Max_HP));
                                case "PER_MP":
                                    return (long)(100.0 * (npc.Cur_MP / npc.Max_MP));
                                case "PER_CP":
                                    return (long)(100.0 * (npc.Cur_CP / npc.Max_CP));
                                case "RUN_SPEED":
                                    return (long)(npc.RunSpeed * Globals.gamedata.my_char.MoveSpeedMult);
                                case "WALK_SPEED":
                                    return (long)(npc.WalkSpeed * Globals.gamedata.my_char.MoveSpeedMult);
                                case "ATTACK_SPEED":
                                    return (long)npc.AttackSpeedMult;
                                case "CAST_SPEED":
                                    return (long)npc.MatkSpeed;
                                case "SPOILED":
                                    if (Globals.gamedata.my_char.TargetSpoiled)
                                        return (long)(1);
                                    else
                                        return (long)(0);
                                case "RUNNING":
                                    return (long)npc.isRunning;
                                case "SITTING":
                                    return (long)npc.isSitting;
                                case "LOOKS_DEAD":
                                    return (long)npc.isAlikeDead;
                                default:
                                    ScriptEngine.Script_Error("invalid target data(npc) request");
                                    break;
                            }
                        }
                    }//unlock
                    finally
                    {
                        Globals.NPCLock.ExitReadLock();
                    }
                    break;
            }
			

			return (long)0;
		}

		public static long COUNT(string req)
		{
			long count = 0;

			switch(req)
			{
				case "NPC_TARGETME":
                    Globals.NPCLock.EnterReadLock();
					try
					{
                        foreach(NPCInfo npc in Globals.gamedata.nearby_npcs.Values)
						{
                            if (Globals.gamedata.my_char.ID == npc.TargetID || (npc.TargetID == Globals.gamedata.my_pet1.ID) || (npc.TargetID == Globals.gamedata.my_pet2.ID) || (npc.TargetID == Globals.gamedata.my_pet3.ID))
							{
								count++;
							}
						}
					}
					finally
					{
                        Globals.NPCLock.ExitReadLock();
					}
					break;
				case "PLAYER_TARGETME":
                    Globals.PlayerLock.EnterReadLock();
					try
					{
                        foreach(CharInfo player in Globals.gamedata.nearby_chars.Values)
						{
                            if (Globals.gamedata.my_char.ID == player.TargetID)
							{
								count++;
							}
						}
					}
					finally
					{
                        Globals.PlayerLock.ExitReadLock();
					}
					break;
                case "NPC_PARTYTARGETED":
                    Globals.NPCLock.EnterReadLock();
                    Globals.PartyLock.EnterReadLock();
                    try
                    {
                        foreach (NPCInfo npc in Globals.gamedata.nearby_npcs.Values)
                        {
                            foreach (PartyMember pmem in Globals.gamedata.PartyMembers.Values)
                            {
                                if (pmem.ID == npc.TargetID || (npc.TargetID == pmem.petID) || (npc.TargetID == pmem.pet1ID) || (npc.TargetID == pmem.pet2ID) || (npc.TargetID == pmem.pet3ID))
                                {
                                    count++;
                                }

                            }
                        }
                    }
                    finally
                    {
                        Globals.NPCLock.ExitReadLock();
                        Globals.PartyLock.ExitReadLock();
                    }
                    break;
				default:
                    ScriptEngine.Script_Error("invalid COUNT request");
					break;
			}

			return count;
		}
	}//end of class
}
