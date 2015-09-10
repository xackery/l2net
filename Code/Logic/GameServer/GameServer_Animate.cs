using System;
using System.Text;

namespace L2_login
{
    public partial class GameServer
    {
        private static void AnimateStuff()
        {
            bool found = false;
            float vx, vy, vz;
            float vxx;
            float movespeed;
            float time;

            ////////////////////////////////Animate self
            try
            {
                if (Globals.gamedata.my_char.Moving == true)
                {
                    if (Globals.gamedata.OOG && ((System.TimeSpan)(System.DateTime.Now - Globals.gamedata.my_char.lastVerifyTime)).Milliseconds > 1000)
                    {
                        //send the verify pos packet
                        ServerPackets.Send_Verify();
                    }
                    if (Globals.gamedata.my_char.MoveTarget != 0)
                    {
                        found = false;
                        //need to set the dest
                        switch (Globals.gamedata.my_char.MoveTargetType)
                        {
                            case TargetType.ERROR:
                            case TargetType.NONE:
                                //shouldn't get this ever
                                //Globals.l2net_home.Add_OnlyDebug("MoveToPawn MoveTargetType invalid - char: " + Globals.gamedata.my_char.Name + ":" + Globals.gamedata.my_char.ID.ToString() + "--: " + Globals.gamedata.my_char.MoveTarget.ToString());
                                break;
                            case TargetType.SELF:
                                //shouldn't get this ever...
                                Globals.gamedata.my_char.Dest_X = Globals.gamedata.my_char.X;
                                Globals.gamedata.my_char.Dest_Y = Globals.gamedata.my_char.Y;
                                Globals.gamedata.my_char.Dest_Z = Globals.gamedata.my_char.Z;
                                found = true;
                                break;
                            case TargetType.MYPET:
                                Globals.gamedata.my_char.Dest_X = Globals.gamedata.my_pet.X;
                                Globals.gamedata.my_char.Dest_Y = Globals.gamedata.my_pet.Y;
                                Globals.gamedata.my_char.Dest_Z = Globals.gamedata.my_pet.Z;
                                found = true;
                                break;
                            case TargetType.MYPET1:
                                Globals.gamedata.my_char.Dest_X = Globals.gamedata.my_pet1.X;
                                Globals.gamedata.my_char.Dest_Y = Globals.gamedata.my_pet1.Y;
                                Globals.gamedata.my_char.Dest_Z = Globals.gamedata.my_pet1.Z;
                                found = true;
                                break;
                            case TargetType.MYPET2:
                                Globals.gamedata.my_char.Dest_X = Globals.gamedata.my_pet2.X;
                                Globals.gamedata.my_char.Dest_Y = Globals.gamedata.my_pet2.Y;
                                Globals.gamedata.my_char.Dest_Z = Globals.gamedata.my_pet2.Z;
                                found = true;
                                break;
                            case TargetType.MYPET3:
                                Globals.gamedata.my_char.Dest_X = Globals.gamedata.my_pet3.X;
                                Globals.gamedata.my_char.Dest_Y = Globals.gamedata.my_pet3.Y;
                                Globals.gamedata.my_char.Dest_Z = Globals.gamedata.my_pet3.Z;
                                found = true;
                                break;
                            case TargetType.PLAYER:
                                CharInfo player_target = null;

                                Globals.PlayerLock.EnterReadLock();
                                try
                                {
                                    player_target = Util.GetChar(Globals.gamedata.my_char.MoveTarget);
                                }
                                finally
                                {
                                    Globals.PlayerLock.ExitReadLock();
                                }

                                if (player_target != null)
                                {
                                    Globals.gamedata.my_char.Dest_X = player_target.X;
                                    Globals.gamedata.my_char.Dest_Y = player_target.Y;
                                    Globals.gamedata.my_char.Dest_Z = player_target.Z;
                                    found = true;
                                }
                                break;
                            case TargetType.NPC:
                                NPCInfo npc_target = null;

                                Globals.NPCLock.EnterReadLock();
                                try
                                {
                                    npc_target = Util.GetNPC(Globals.gamedata.my_char.MoveTarget);
                                }
                                finally
                                {
                                    Globals.NPCLock.ExitReadLock();
                                }

                                if (npc_target != null)
                                {
                                    Globals.gamedata.my_char.Dest_X = npc_target.X;
                                    Globals.gamedata.my_char.Dest_Y = npc_target.Y;
                                    Globals.gamedata.my_char.Dest_Z = npc_target.Z;
                                    found = true;
                                }
                                break;
                            case TargetType.ITEM:
                                ItemInfo item_target = null;

                                Globals.ItemLock.EnterReadLock();
                                try
                                {
                                    item_target = Util.GetItem(Globals.gamedata.my_char.MoveTarget);
                                }
                                finally
                                {
                                    Globals.ItemLock.ExitReadLock();
                                }

                                if (item_target != null)
                                {
                                    Globals.gamedata.my_char.Dest_X = item_target.X;
                                    Globals.gamedata.my_char.Dest_Y = item_target.Y;
                                    Globals.gamedata.my_char.Dest_Z = item_target.Z;
                                    found = true;
                                }
                                break;
                        }

                        if (!found)
                        {
                            Globals.gamedata.my_char.Dest_X = Globals.gamedata.my_char.X;
                            Globals.gamedata.my_char.Dest_Y = Globals.gamedata.my_char.Y;
                            Globals.gamedata.my_char.Dest_Z = Globals.gamedata.my_char.Z;
                        }
                    }

                    //move me towards my target
                    vx = Globals.gamedata.my_char.Dest_X - Globals.gamedata.my_char.X;
                    vy = Globals.gamedata.my_char.Dest_Y - Globals.gamedata.my_char.Y;
                    vz = Globals.gamedata.my_char.Dest_Z - Globals.gamedata.my_char.Z;

                    //movespeed = ((float)my_char.RunSpeed)*ch.MoveSpeedMult;
                    movespeed = Globals.gamedata.my_char.RunSpeed * Globals.gamedata.my_char.MoveSpeedMult;

                    time = System.Convert.ToSingle(((System.TimeSpan)(System.DateTime.Now - Globals.gamedata.my_char.lastMoveTime)).Milliseconds) * Globals.INV_THOUSAND;

                    vxx = System.Convert.ToSingle(Math.Sqrt(vx * vx + vy * vy + vz * vz));
                    if (vxx != 0)
                    {
                        vxx = Util.Float_Cap((movespeed * time) / vxx);
                    }

                    vx *= vxx;
                    vy *= vxx;
                    vz *= vxx;

                    Globals.gamedata.my_char.X += vx;
                    Globals.gamedata.my_char.Y += vy;
                    Globals.gamedata.my_char.Z += vz;
                    Globals.gamedata.my_char.lastMoveTime = System.DateTime.Now;

                    if ((Globals.gamedata.my_char.MoveTarget == 0) && (Math.Sqrt(Math.Pow(Globals.gamedata.my_char.X - Globals.gamedata.my_char.Dest_X, 2) + Math.Pow(Globals.gamedata.my_char.Y - Globals.gamedata.my_char.Dest_Y, 2) + Math.Pow(Globals.gamedata.my_char.Z - Globals.gamedata.my_char.Dest_Z, 2)) < Globals.THRESHOLD))
                    {
                        Globals.gamedata.my_char.Moving = false;
                    }
                }
            }
            catch
            {
                Globals.l2net_home.Add_Error("crash: Animate self");
            }

            ///////////////////////Animate Pet
            if (Globals.gamedata.my_pet.ID != 0)
            {
                try
                {
                    if (Globals.gamedata.my_pet.Moving == true)
                    {
                        if (Globals.gamedata.my_pet.MoveTarget != 0)
                        {
                            found = false;
                            //need to set the dest
                            switch (Globals.gamedata.my_pet.MoveTargetType)
                            {
                                case TargetType.ERROR:
                                case TargetType.NONE:
                                    //shouldn't get this ever
                                    //Globals.l2net_home.Add_OnlyDebug("MoveToPawn MoveTargetType invalid - pet: " + Globals.gamedata.my_pet.Name + ":" + Globals.gamedata.my_pet.ID.ToString() + "--: " + Globals.gamedata.my_pet.MoveTarget.ToString());
                                    break;
                                case TargetType.SELF:
                                    Globals.gamedata.my_pet.Dest_X = Globals.gamedata.my_char.X;
                                    Globals.gamedata.my_pet.Dest_Y = Globals.gamedata.my_char.Y;
                                    Globals.gamedata.my_pet.Dest_Z = Globals.gamedata.my_char.Z;
                                    found = true;
                                    break;
                                case TargetType.MYPET:
                                    //shouldn't get this ever...
                                    Globals.gamedata.my_pet.Dest_X = Globals.gamedata.my_pet.X;
                                    Globals.gamedata.my_pet.Dest_Y = Globals.gamedata.my_pet.Y;
                                    Globals.gamedata.my_pet.Dest_Z = Globals.gamedata.my_pet.Z;
                                    found = true;
                                    break;
                                case TargetType.MYPET1:
                                    //shouldn't get this ever...
                                    Globals.gamedata.my_pet1.Dest_X = Globals.gamedata.my_pet1.X;
                                    Globals.gamedata.my_pet1.Dest_Y = Globals.gamedata.my_pet1.Y;
                                    Globals.gamedata.my_pet1.Dest_Z = Globals.gamedata.my_pet1.Z;
                                    found = true;
                                    break;
                                case TargetType.MYPET2:
                                    //shouldn't get this ever...
                                    Globals.gamedata.my_pet2.Dest_X = Globals.gamedata.my_pet2.X;
                                    Globals.gamedata.my_pet2.Dest_Y = Globals.gamedata.my_pet2.Y;
                                    Globals.gamedata.my_pet2.Dest_Z = Globals.gamedata.my_pet2.Z;
                                    found = true;
                                    break;
                                case TargetType.MYPET3:
                                    //shouldn't get this ever...
                                    Globals.gamedata.my_pet3.Dest_X = Globals.gamedata.my_pet3.X;
                                    Globals.gamedata.my_pet3.Dest_Y = Globals.gamedata.my_pet3.Y;
                                    Globals.gamedata.my_pet3.Dest_Z = Globals.gamedata.my_pet3.Z;
                                    found = true;
                                    break;
                                case TargetType.PLAYER:
                                    CharInfo player_target = null;

                                    Globals.PlayerLock.EnterReadLock();
                                    try
                                    {
                                        player_target = Util.GetChar(Globals.gamedata.my_pet.MoveTarget);
                                    }
                                    finally
                                    {
                                        Globals.PlayerLock.ExitReadLock();
                                    }

                                    if (player_target != null)
                                    {
                                        Globals.gamedata.my_pet.Dest_X = player_target.X;
                                        Globals.gamedata.my_pet.Dest_Y = player_target.Y;
                                        Globals.gamedata.my_pet.Dest_Z = player_target.Z;
                                        found = true;
                                    }
                                    break;
                                case TargetType.NPC:
                                    NPCInfo npc_target = null;

                                    Globals.NPCLock.EnterReadLock();
                                    try
                                    {
                                        npc_target = Util.GetNPC(Globals.gamedata.my_pet.MoveTarget);
                                    }
                                    finally
                                    {
                                        Globals.NPCLock.ExitReadLock();
                                    }

                                    if (npc_target != null)
                                    {
                                        Globals.gamedata.my_pet.Dest_X = npc_target.X;
                                        Globals.gamedata.my_pet.Dest_Y = npc_target.Y;
                                        Globals.gamedata.my_pet.Dest_Z = npc_target.Z;
                                        found = true;
                                    }
                                    break;
                                case TargetType.ITEM:
                                    ItemInfo item_target = null;

                                    Globals.ItemLock.EnterReadLock();
                                    try
                                    {
                                        item_target = Util.GetItem(Globals.gamedata.my_pet.MoveTarget);
                                    }
                                    finally
                                    {
                                        Globals.ItemLock.ExitReadLock();
                                    }

                                    if (item_target != null)
                                    {
                                        Globals.gamedata.my_pet.Dest_X = item_target.X;
                                        Globals.gamedata.my_pet.Dest_Y = item_target.Y;
                                        Globals.gamedata.my_pet.Dest_Z = item_target.Z;
                                        found = true;
                                    }
                                    break;
                            }

                            if (!found)
                            {
                                Globals.gamedata.my_pet.Dest_X = Globals.gamedata.my_pet.X;
                                Globals.gamedata.my_pet.Dest_Y = Globals.gamedata.my_pet.Y;
                                Globals.gamedata.my_pet.Dest_Z = Globals.gamedata.my_pet.Z;
                            }
                        }

                        //move me towards my target
                        vx = Globals.gamedata.my_pet.Dest_X - Globals.gamedata.my_pet.X;
                        vy = Globals.gamedata.my_pet.Dest_Y - Globals.gamedata.my_pet.Y;
                        vz = Globals.gamedata.my_pet.Dest_Z - Globals.gamedata.my_pet.Z;

                        //movespeed = ((float)my_char.RunSpeed)*ch.MoveSpeedMult;
                        movespeed = Globals.gamedata.my_pet.RunSpeed * Globals.gamedata.my_pet.MoveSpeedMult;

                        time = System.Convert.ToSingle(((System.TimeSpan)(System.DateTime.Now - Globals.gamedata.my_pet.lastMoveTime)).Milliseconds) * Globals.INV_THOUSAND;

                        vxx = System.Convert.ToSingle(Math.Sqrt(vx * vx + vy * vy + vz * vz));
                        if (vxx != 0)
                        {
                            vxx = Util.Float_Cap((movespeed * time) / vxx);
                        }

                        vx *= vxx;
                        vy *= vxx;
                        vz *= vxx;

                        Globals.gamedata.my_pet.X += vx;
                        Globals.gamedata.my_pet.Y += vy;
                        Globals.gamedata.my_pet.Z += vz;
                        Globals.gamedata.my_pet.lastMoveTime = System.DateTime.Now;

                        if ((Globals.gamedata.my_pet.MoveTarget == 0) && (Math.Sqrt(Math.Pow(Globals.gamedata.my_pet.X - Globals.gamedata.my_pet.Dest_X, 2) + Math.Pow(Globals.gamedata.my_pet.Y - Globals.gamedata.my_pet.Dest_Y, 2) + Math.Pow(Globals.gamedata.my_pet.Z - Globals.gamedata.my_pet.Dest_Z, 2)) < Globals.THRESHOLD))
                        {
                            Globals.gamedata.my_pet.Moving = false;
                        }
                    }
                }
                catch
                {
                    Globals.l2net_home.Add_Error("crash: Animate pet");
                }
            }

            ///////////////////////Animate other chars
            Globals.PlayerLock.EnterReadLock();
            try
            {
                foreach (CharInfo player in Globals.gamedata.nearby_chars.Values)
                {
                    if (player.Moving)
                    {
                        if (player.MoveTarget != 0)
                        {
                            found = false;
                            //need to set the dest
                            switch (player.MoveTargetType)
                            {
                                case TargetType.ERROR:
                                case TargetType.NONE:
                                    //shouldn't get this ever...
                                    //Globals.l2net_home.Add_OnlyDebug("MoveToPawn MoveTargetType invalid - pc: " + player.Name + ":" + player.ID.ToString() + "--: " + player.MoveTarget.ToString());
                                    break;
                                case TargetType.SELF:
                                    player.Dest_X = Globals.gamedata.my_char.X;
                                    player.Dest_Y = Globals.gamedata.my_char.Y;
                                    player.Dest_Z = Globals.gamedata.my_char.Z;
                                    found = true;
                                    break;
                                case TargetType.MYPET:
                                    player.Dest_X = Globals.gamedata.my_pet.X;
                                    player.Dest_Y = Globals.gamedata.my_pet.Y;
                                    player.Dest_Z = Globals.gamedata.my_pet.Z;
                                    found = true;
                                    break;
                                case TargetType.MYPET1:
                                    player.Dest_X = Globals.gamedata.my_pet1.X;
                                    player.Dest_Y = Globals.gamedata.my_pet1.Y;
                                    player.Dest_Z = Globals.gamedata.my_pet1.Z;
                                    found = true;
                                    break;
                                case TargetType.MYPET2:
                                    player.Dest_X = Globals.gamedata.my_pet2.X;
                                    player.Dest_Y = Globals.gamedata.my_pet2.Y;
                                    player.Dest_Z = Globals.gamedata.my_pet2.Z;
                                    found = true;
                                    break;
                                case TargetType.MYPET3:
                                    player.Dest_X = Globals.gamedata.my_pet3.X;
                                    player.Dest_Y = Globals.gamedata.my_pet3.Y;
                                    player.Dest_Z = Globals.gamedata.my_pet3.Z;
                                    found = true;
                                    break;
                                case TargetType.PLAYER:
                                    CharInfo player_target = null;

                                    //no need to lock... we already have a writer lock
                                    player_target = Util.GetChar(Globals.gamedata.my_char.MoveTarget);

                                    if (player_target != null)
                                    {
                                        player.Dest_X = player_target.X;
                                        player.Dest_Y = player_target.Y;
                                        player.Dest_Z = player_target.Z;
                                        found = true;
                                    }
                                    break;
                                case TargetType.NPC:
                                    NPCInfo npc_target = null;

                                    Globals.NPCLock.EnterReadLock();
                                    try
                                    {
                                        npc_target = Util.GetNPC(player.MoveTarget);
                                    }
                                    finally
                                    {
                                        Globals.NPCLock.ExitReadLock();
                                    }

                                    if (npc_target != null)
                                    {
                                        player.Dest_X = npc_target.X;
                                        player.Dest_Y = npc_target.Y;
                                        player.Dest_Z = npc_target.Z;
                                        found = true;
                                    }
                                    break;
                                case TargetType.ITEM:
                                    ItemInfo item_target = null;

                                    Globals.ItemLock.EnterReadLock();
                                    try
                                    {
                                        item_target = Util.GetItem(player.MoveTarget);
                                    }//unlock
                                    finally
                                    {
                                        Globals.ItemLock.ExitReadLock();
                                    }

                                    if (item_target != null)
                                    {
                                        player.Dest_X = item_target.X;
                                        player.Dest_Y = item_target.Y;
                                        player.Dest_Z = item_target.Z;
                                        found = true;
                                    }
                                    break;
                            }

                            if (!found)
                            {
                                player.Dest_X = player.X;
                                player.Dest_Y = player.Y;
                                player.Dest_Z = player.Z;
                            }
                        }

                        vx = player.Dest_X - player.X;
                        vy = player.Dest_Y - player.Y;
                        vz = player.Dest_Z - player.Z;

                        if (player.isRunning != 0)
                        {
                            movespeed = player.RunSpeed * player.MoveSpeedMult;
                        }
                        else
                        {
                            movespeed = player.WalkSpeed * player.MoveSpeedMult;
                        }

                        time = System.Convert.ToSingle(((System.TimeSpan)(System.DateTime.Now - player.lastMoveTime)).Milliseconds) * Globals.INV_THOUSAND;

                        vxx = System.Convert.ToSingle(Math.Sqrt(vx * vx + vy * vy + vz * vz));
                        if (vxx != 0)
                        {
                            vxx = Util.Float_Cap((movespeed * time) / vxx);
                        }

                        vx *= vxx;
                        vy *= vxx;
                        vz *= vxx;

                        player.X += Util.Float_Int32(vx);
                        player.Y += Util.Float_Int32(vy);
                        player.Z += Util.Float_Int32(vz);
                        player.lastMoveTime = System.DateTime.Now;

                        if ((player.MoveTarget == 0) && (Math.Sqrt(Math.Pow(player.X - player.Dest_X, 2) + Math.Pow(player.Y - player.Dest_Y, 2) + Math.Pow(player.Z - player.Dest_Z, 2)) < Globals.THRESHOLD))
                        {
                            player.Moving = false;
                        }
                    }
                }
            }//unlock
            catch
            {
                Globals.l2net_home.Add_Error("crash: Animate other players");
            }
            finally
            {
                Globals.PlayerLock.ExitReadLock();
            }

            //animate npcs
            Globals.NPCLock.EnterReadLock();
            try
            {
                foreach (NPCInfo npc in Globals.gamedata.nearby_npcs.Values)
                {
                    if (npc.Moving)
                    {
                        if (npc.MoveTarget != 0)
                        {
                            found = false;
                            //need to set the dest
                            switch (npc.MoveTargetType)
                            {
                                case TargetType.ERROR:
                                case TargetType.NONE:
                                    //shouldn't get this ever...
                                    //Globals.l2net_home.Add_OnlyDebug("MoveToPawn MoveTargetType invalid - npc: " + npc.Name + ":" + npc.ID.ToString() + "--: " + npc.MoveTarget.ToString());
                                    break;
                                case TargetType.SELF:
                                    npc.Dest_X = Globals.gamedata.my_char.X;
                                    npc.Dest_Y = Globals.gamedata.my_char.Y;
                                    npc.Dest_Z = Globals.gamedata.my_char.Z;
                                    found = true;
                                    break;
                                case TargetType.MYPET:
                                    npc.Dest_X = Globals.gamedata.my_pet.X;
                                    npc.Dest_Y = Globals.gamedata.my_pet.Y;
                                    npc.Dest_Z = Globals.gamedata.my_pet.Z;
                                    found = true;
                                    break;
                                case TargetType.MYPET1:
                                    npc.Dest_X = Globals.gamedata.my_pet1.X;
                                    npc.Dest_Y = Globals.gamedata.my_pet1.Y;
                                    npc.Dest_Z = Globals.gamedata.my_pet1.Z;
                                    found = true;
                                    break;
                                case TargetType.MYPET2:
                                    npc.Dest_X = Globals.gamedata.my_pet2.X;
                                    npc.Dest_Y = Globals.gamedata.my_pet2.Y;
                                    npc.Dest_Z = Globals.gamedata.my_pet2.Z;
                                    found = true;
                                    break;
                                case TargetType.MYPET3:
                                    npc.Dest_X = Globals.gamedata.my_pet3.X;
                                    npc.Dest_Y = Globals.gamedata.my_pet3.Y;
                                    npc.Dest_Z = Globals.gamedata.my_pet3.Z;
                                    found = true;
                                    break;
                                case TargetType.PLAYER:
                                    CharInfo player_target = null;

                                    Globals.PlayerLock.EnterReadLock();
                                    try
                                    {
                                        player_target = Util.GetChar(npc.MoveTarget);
                                    }
                                    finally
                                    {
                                        Globals.PlayerLock.ExitReadLock();
                                    }

                                    if (player_target != null)
                                    {
                                        npc.Dest_X = player_target.X;
                                        npc.Dest_Y = player_target.Y;
                                        npc.Dest_Z = player_target.Z;
                                        found = true;
                                    }
                                    break;
                                case TargetType.NPC:
                                    NPCInfo npc_target = null;

                                    //no need to lock... we already have a writer lock
                                    npc_target = Util.GetNPC(npc.MoveTarget);

                                    if (npc_target != null)
                                    {
                                        npc.Dest_X = npc_target.X;
                                        npc.Dest_Y = npc_target.Y;
                                        npc.Dest_Z = npc_target.Z;
                                        found = true;
                                    }
                                    break;
                                case TargetType.ITEM:
                                    ItemInfo item_target = null;

                                    Globals.ItemLock.EnterReadLock();
                                    try
                                    {
                                        item_target = Util.GetItem(npc.MoveTarget);
                                    }
                                    finally
                                    {
                                        Globals.ItemLock.ExitReadLock();
                                    }

                                    if (item_target != null)
                                    {
                                        npc.Dest_X = item_target.X;
                                        npc.Dest_Y = item_target.Y;
                                        npc.Dest_Z = item_target.Z;
                                        found = true;
                                    }
                                    break;
                            }//end of switch

                            if (!found)
                            {
                                npc.Dest_X = npc.X;
                                npc.Dest_Y = npc.Y;
                                npc.Dest_Z = npc.Z;
                            }
                        }

                        vx = npc.Dest_X - npc.X;
                        vy = npc.Dest_Y - npc.Y;
                        vz = npc.Dest_Z - npc.Z;

                        if (npc.isRunning != 0)
                        {
                            movespeed = npc.RunSpeed * npc.MoveSpeedMult;
                        }
                        else
                        {
                            movespeed = npc.WalkSpeed * npc.MoveSpeedMult;
                        }

                        time = System.Convert.ToSingle(((System.TimeSpan)(System.DateTime.Now - npc.lastMoveTime)).Milliseconds) * Globals.INV_THOUSAND;

                        vxx = System.Convert.ToSingle(Math.Sqrt(vx * vx + vy * vy + vz * vz));
                        if (vxx != 0)
                        {
                            vxx = Util.Float_Cap((movespeed * time) / vxx);
                        }

                        vx *= vxx;
                        vy *= vxx;
                        vz *= vxx;

                        npc.X += Util.Float_Int32(vx);
                        npc.Y += Util.Float_Int32(vy);
                        npc.Z += Util.Float_Int32(vz);
                        npc.lastMoveTime = System.DateTime.Now;

                        if ((npc.MoveTarget == 0) && (Math.Sqrt(Math.Pow(npc.X - npc.Dest_X, 2) + Math.Pow(npc.Y - npc.Dest_Y, 2) + Math.Pow(npc.Z - npc.Dest_Z, 2)) < Globals.THRESHOLD))
                        {
                            npc.Moving = false;
                        }
                    }
                }
            }//unlock
            catch
            {
                Globals.l2net_home.Add_Error("crash: Animate npcs");
            }
            finally
            {
                Globals.NPCLock.ExitReadLock();
            }
        }//end of AnimateStuff
    }
}
