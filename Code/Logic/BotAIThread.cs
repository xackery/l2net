using System;

namespace L2_login
{
    public enum BotState : byte
    {
        //0 nothing/following | 1 - attacking | 2 - buffing | 3 - waiting for buff to be used
        Nothing = 0,
        Attacking = 1,
        Buffing = 2,
        BuffWaiting = 3,
        FinishedBuffing = 4,
    }

    public class BotAIThread
    {
        public System.Threading.Thread botaithread;

        private static bool _was_smart_moving = false;

        private static bool breaktotop;
        private static bool found;
        private static bool needs_buff;
        private static uint PickUpItem = 0;
        private static uint PickUpItemSleep = 0;

        private float target_hp = 0;
        private float target_max_hp = 0;
        private uint NPCID = 0;
        private uint new_id = 0, old_id = 0;
        private int target_timeout_loops = 0;

        public BotAIThread()
        {
            botaithread = new System.Threading.Thread(new System.Threading.ThreadStart(BotAI));

            botaithread.IsBackground = true;
        }

        private void BotAI()
        {
            uint OOCDist = 0;
            uint OOCLoops = 0;
            int loops = 1;
            int blacklistloops = 0;
            int active_followloops = 0;
            int move_before_attackloops = 25;
            int move_before_docombatloops = 25;
            int anti_ks_loops = 0;
            bool anti_ks_changed_target = false;
            bool target_party_pet = false;
            bool target_my_pet = false;
            bool is_my_summon = false;
            float my_x = 0;
            uint current_targetID = 0, ignID = 0;
            while (Globals.gamedata.running)
            {
                breaktotop = false;
                //moved sleep to the top for when breaking to top
                System.Threading.Thread.Sleep(Globals.SLEEP_BotAI);

                if ((Globals.gamedata.BOTTING) && (Globals.gamedata.my_char.Cur_HP > 0))
                {
                    /////////////////////////////Do items
                    if (!breaktotop)//we always want to be able to use items, who gives a shit about our target
                    {
                        DoItems();
                    }

                    /////////////////////////////Do buffs/heals part 1
                    /////this is the section that will actually use the skill
                    if (!breaktotop && (Globals.gamedata.BOT_STATE == BotState.Buffing) && Globals.gamedata.my_char.CanBuff())
                    {
                        DoBuffsPart1();

                        if (breaktotop)
                        {
                            was_smart_moving = false;
                        }
                    }//end of BOT_STATE = 2

                    //if it has been more than 15 seconds since we tried to do the buff... lets set our state back to normal
                    if (!breaktotop && Globals.gamedata.JustBuffing() && (Globals.gamedata.my_char.LastBuffTime.AddTicks(Globals.FAILED_BUFF) <= System.DateTime.Now))
                    {
                        Globals.gamedata.my_char.Clear_Botting_Buffing(false);

                    }

                    /////////////////////Do buffs/heals part 2
                    ///this is the part where we decide if we need to do a buff or anything of the sort
                    if (!breaktotop && Globals.gamedata.ReadyState())
                    {
                        DoBuffsPart2();

                        if (breaktotop)
                        {
                            was_smart_moving = false;
                        }
                    }




                    ///////////pick up loot
                    //Globals.picking_up_items required to stop the bot from assisting/doing skills(combat only) while picking up
                    if (!breaktotop && Globals.gamedata.ReadyState() && Globals.gamedata.botoptions.Pickup == 1)
                    {
                        //pickup after a kill, regardless of what's around
                        if (Globals.gamedata.botoptions.PickupAfterAttack == 0)
                        {
                            if (CheckItemsForPickup())
                            {
                                breaktotop = true;
                                Pickup();
                                Globals.picking_up_items = true;
                            }
                            else
                            {
                                Globals.picking_up_items = false;
                            }
                        }
                        //Let's pick up after everything is dead
                        else if (Globals.gamedata.botoptions.PickupAfterAttack == 1)
                        {
                            if (Script_Ops.COUNT("NPC_TARGETME") == 0 && Script_Ops.COUNT("NPC_PARTYTARGETED") == 0)
                            {

                                if (CheckItemsForPickup())
                                {
                                    breaktotop = true;
                                    Pickup();
                                    Globals.picking_up_items = true;
                                }
                                else
                                {
                                    Globals.picking_up_items = false;
                                }
                            }
                            else
                            {
                                Globals.picking_up_items = false;
                            }
                        }
                        else
                        {
                            Globals.picking_up_items = false;
                        }
                    }
                    else
                    {
                        Globals.picking_up_items = false;
                    }


                    /////
                    //////////////////////Do spoil when following
                    if (!breaktotop && (Globals.gamedata.botoptions.AutoSpoil == 1) && !Globals.gamedata.my_char.TargetSpoiled && Globals.gamedata.CanAttack() && (Globals.gamedata.my_char.TargetID != 0) && (Globals.gamedata.my_char.CurrentTargetType == TargetType.NPC) && ((Globals.gamedata.botoptions.ActiveFollow == 1) || Globals.gamedata.botoptions.ActiveFollowAttack == 1))
                    {
                        AutoSpoil();
                        if (Globals.gamedata.botoptions.AutoSpoilUntilSuccess == 0)
                        {
                            Globals.gamedata.my_char.TargetSpoiled = true;
                        }
                        Globals.gamedata.my_char.isAttacking = false;
                    }

                    //////////////////////Rest
                    if (!breaktotop && (Script_Ops.COUNT("NPC_TARGETME") == 0 && Script_Ops.COUNT("NPC_PARTYTARGETED") == 0) && (Globals.gamedata.my_char.Cur_HP > 0) && (((Globals.gamedata.botoptions.RestBelowHP == 1) && (Globals.gamedata.my_char.Cur_HP < Globals.gamedata.botoptions.RestBelowHealth)) || ((Globals.gamedata.botoptions.RestBelowMP == 1) && (Globals.gamedata.my_char.Cur_MP < Globals.gamedata.botoptions.RestBelowMana))))
                    {
                        //Globals.l2net_home.Add_Text("Resting", Globals.Yellow);
                        RestBelow();
                    }

                    ////////////Stuck Check
                    #region StuckCheck
                    if (!breaktotop && ((Globals.gamedata.botoptions.StuckCheck == 1) || (Globals.gamedata.botoptions.AutoBlacklist == 1)) && (Globals.gamedata.my_char.CurrentTargetType == TargetType.NPC) && (Globals.gamedata.my_char.isSitting == 1) && (Globals.gamedata.my_char.Cur_HP > 0) && (Globals.gamedata.my_char.TargetID != 0)) //If we have target, unstuck is activated, active target or attack is activated, char is not dead and char is not sitting down. //&& (Globals.gamedata.botoptions.Target == 1 || Globals.gamedata.botoptions.Attack == 1)
                    {
                        GetTargetInfo();
                        if (target_hp == target_max_hp)
                        {
                            if (Globals.gamedata.my_char.CannotSeeTarget)
                            {
                                if (Globals.gamedata.botoptions.StuckCheck == 1)
                                {
                                    StuckCheckInternal();
                                }

                                Globals.gamedata.my_char.CannotSeeTarget = false;
                                blacklistloops++;
                            }
                            //Globals.l2net_home.Add_Text("Debug: Loops: " + loops + " my_x: " + my_x, Globals.Green, TextType.BOT);
                            if (loops == 1)
                            {
                                my_x = Globals.gamedata.my_char.X;
                                current_targetID = Globals.gamedata.my_char.TargetID;
                                //Globals.l2net_home.Add_Text("Debug: Loops = 1, setting my_x to :" + my_x, Globals.Green, TextType.BOT);
                            }
                            if (current_targetID == Globals.gamedata.my_char.TargetID)
                            {
                                if ((loops == 15) && (my_x == Globals.gamedata.my_char.X)) //If the char has been stuck in the same spot for some time.
                                {
                                    if (Globals.gamedata.botoptions.StuckCheck == 1)
                                    {
                                        StuckCheckInternal();
                                    }
                                    loops = 0;
                                    blacklistloops++; //Number of times the unstuck algorithm has run on this particular mob
                                    //Globals.l2net_home.Add_Text("Debug: Blacklist loops: " + blacklistloops, Globals.Green, TextType.BOT);
                                }
                                else if ((loops == 15) && (my_x != Globals.gamedata.my_char.X)) //Char is not stuck
                                {
                                    loops = 0; //Reset counter
                                }
                                else if (blacklistloops == Globals.gamedata.botoptions.BlacklistTries) //char has been stuck 4 times on this mob, its probably unreachable.
                                {
                                    blacklistloops = 0; //reset counter
                                    if (Globals.gamedata.botoptions.AutoBlacklist == 1)
                                    {
                                        BlacklistNPC(NPCID); //blacklist npc
#if DEBUG
                                        Globals.l2net_home.Add_Text("Canceleling target from BlacklistNPC: ", Globals.Blue, TextType.SYSTEM);
#endif
                                        ServerPackets.Send_CancelTarget(); //cancel target
                                        System.Threading.Thread.Sleep(1500); //Give game time to lose the target
                                        Target(); //get a new target
                                    }
                                }
                            }
                            else
                            {
                                //Globals.l2net_home.Add_Text("Debug: New target", Globals.Green, TextType.BOT);
                                loops = 0; //New target, reset counter.
                                blacklistloops = 0;
                            }
                            loops++;
                        }
                        else
                        {
                            loops = 1; //Target is under attack, no need to unstuck.
                            blacklistloops = 0;
                        }
                        //System.Threading.Thread.Sleep(1000);
                    }
                    #endregion


                    //////////Stuck Check "Move before targeting"
                    #region Stuck Check "Move Before Targeting"
                    if ((!breaktotop && ((Globals.gamedata.botoptions.StuckCheck == 1) || (Globals.gamedata.botoptions.AutoBlacklist == 1))) && (Globals.gamedata.botoptions.MoveBeforeTargeting == 1) && (Globals.gamedata.my_char.isSitting == 1) && (Globals.gamedata.my_char.Cur_HP > 0))
                    {
                        if ((Globals.gamedata.BOT_STATE != BotState.Attacking) && (Globals.gamedata.BOT_STATE != BotState.Buffing))
                        {

                            if (loops == 1)
                            {
                                //Set X
                                my_x = Globals.gamedata.my_char.X;
                            }

                            //Can't See Target
                            if (Globals.gamedata.my_char.CannotSeeTarget)
                            {
                                if (Globals.gamedata.botoptions.StuckCheck == 1)
                                {
                                    StuckCheckInternal();
                                }

                                Globals.gamedata.my_char.CannotSeeTarget = false;
                                blacklistloops++;
                            }


                            if ((loops == 15) && (my_x != Globals.gamedata.my_char.X)) //Char is not stuck
                            {
                                loops = 0; //Reset counter
                            }

                            if ((loops == 15) && (my_x == Globals.gamedata.my_char.X)) //If the char has been stuck in the same spot for some time.
                            {
                                if (Globals.gamedata.botoptions.StuckCheck == 1)
                                {
                                    StuckCheckInternal();
                                }
                                loops = 0;
                                blacklistloops++; //Number of times the unstuck algorithm has run on this particular mob
                            }

                            if (blacklistloops == Globals.gamedata.botoptions.BlacklistTries)
                            {
                                blacklistloops = 0; //reset counter
                                if (Globals.gamedata.botoptions.AutoBlacklist == 1)
                                {
                                    BlacklistNPC(new_id); //blacklist npc
                                    Target(); //get a new target
                                }

                            }

                        }
                        else
                        {
                            loops = 1; //Target is under attack, no need to unstuck.
                            blacklistloops = 0;
                        }




                    }
                    #endregion



                    //////////////////////////////Do active follow shit
                    if (!breaktotop && Globals.gamedata.ReadyState() && (Globals.gamedata.botoptions.ActiveFollow == 1) && (Globals.gamedata.botoptions.ActiveFollowID != 0))
                    {
                        if (active_followloops == Globals.gamedata.botoptions.AutoFollowDelay)
                        {
                            ActiveFollow();
                            active_followloops = 0;
                        }
                        else
                        {
                            active_followloops++;
                        }
                    }

                    //target something...
                    if (!breaktotop && Globals.gamedata.ReadyState())
                    {
                        if (Globals.gamedata.botoptions.Target == 1)
                        {
                            if (Target())
                            {
                                //Globals.l2net_home.Add_Text("Found target: " + new_id.ToString("X2"), Globals.Green);
                                was_smart_moving = false;
                            }
                        }
                        else
                        {
                            //we aren't set to auto target...
                            //but we should set our state to attackable if we really are...
                            if ((Globals.gamedata.my_char.TargetID != Globals.gamedata.my_char.BuffTarget) && (Globals.gamedata.my_char.TargetID != Globals.gamedata.my_char.BuffTargetLast))
                            {
                                //we aren't targeting who we were trying to buff...or the target before that
                                Globals.gamedata.BOT_STATE = BotState.Nothing;
                            }
                        }
                    }

                    ///////////Anti KS
                    if (!breaktotop && Globals.gamedata.botoptions.Target == 1 && BotOptions.Target_COMBAT == 0) //Bot it set to not KS and its set to autotarget
                    {
                        if (anti_ks_loops == Globals.gamedata.botoptions.AntiKSDelay)
                        {
                            Globals.PlayerLock.EnterReadLock();
                            Globals.NPCLock.EnterReadLock();
                            Globals.PartyLock.EnterReadLock();
                            try
                            {
                                foreach (NPCInfo npc in Globals.gamedata.nearby_npcs.Values)
                                {
                                    if ((npc.ID == Globals.gamedata.my_pet.ID) || (npc.ID == Globals.gamedata.my_pet1.ID) || (npc.ID == Globals.gamedata.my_pet2.ID) || (npc.ID == Globals.gamedata.my_pet3.ID))
                                    {
                                        is_my_summon = true;
                                    }

                                    //if npc got my pet or party pet on target
                                    if (!is_my_summon)
                                    {
                                        if (npc.TargetID != 0)
                                        {
                                            foreach (PartyMember pmem in Globals.gamedata.PartyMembers.Values)
                                            {
                                                if ((npc.ID == pmem.petID) || (npc.ID == pmem.pet1ID) || (npc.ID == pmem.pet2ID) || (npc.ID == pmem.pet3ID))
                                                {
                                                    is_my_summon = true;
                                                }


                                                if (!is_my_summon)
                                                {
                                                    if ((npc.TargetID == pmem.petID) || (npc.TargetID == pmem.pet1ID) || (npc.TargetID == pmem.pet2ID) || (npc.TargetID == pmem.pet3ID))
                                                    {
                                                        target_party_pet = true;
                                                    }
                                                }

                                            }

                                            if ((npc.TargetID == Globals.gamedata.my_pet.ID) || (npc.TargetID == Globals.gamedata.my_pet1.ID) || (npc.TargetID == Globals.gamedata.my_pet2.ID) || (npc.TargetID == Globals.gamedata.my_pet3.ID))
                                            {
                                                target_my_pet = true;
                                            }

                                        }
                                    }

                                    if (npc.TargetID != Globals.gamedata.my_char.ID && !Globals.gamedata.PartyMembers.ContainsKey(npc.TargetID) && !target_party_pet && !target_my_pet && !is_my_summon)// && ((npc.TargetID != Globals.gamedata.my_pet.ID) && (Globals.gamedata.my_pet.ID != 0))) //If NPC is not targeting us or my pet.
                                    {
                                        //if npc is summon, lets not ks
                                        if (npc.SummonedNameColor > 0)
                                        {
                                            if ((npc.TargetID == Globals.gamedata.my_char.TargetID) || npc.TargetID == new_id)
                                            {
                                                anti_ks_changed_target = true;
                                                ignID = npc.TargetID;
                                            }
                                        }


                                        if (!anti_ks_changed_target)
                                        {
                                            foreach (CharInfo player in Globals.gamedata.nearby_chars.Values)
                                            {
                                                //Somebody else has targeted my target
                                                if ((((player.TargetID == Globals.gamedata.my_char.TargetID) || (player.TargetID == new_id)) && !Globals.gamedata.PartyMembers.ContainsKey(player.ID)) && ((npc.TargetID != Globals.gamedata.my_char.ID) && !Globals.gamedata.PartyMembers.ContainsKey(npc.TargetID) && !target_party_pet && !target_my_pet && !is_my_summon))
                                                {
                                                    anti_ks_changed_target = true;
                                                    ignID = player.TargetID;
                                                }
                                            }
                                        }
                                    }
                                    //reset vars for every npc
                                    is_my_summon = false;
                                    target_my_pet = false;
                                    target_party_pet = false;
                                }


                            }
                            finally
                            {
                                //Globals.l2net_home.Add_Text("Exit locks", Globals.Green);
                                Globals.PlayerLock.ExitReadLock();
                                Globals.NPCLock.ExitReadLock();
                                Globals.PartyLock.ExitReadLock();

                                if (anti_ks_changed_target)
                                {
                                    if (Globals.Script_Debugging)
                                    {
                                        Globals.l2net_home.Add_Debug("Someone else got my mob on target, changing target");
                                    }

                                    ServerPackets.Send_CancelTarget(); //cancel target
#if DEBUG
                                    Globals.l2net_home.Add_Text("Canceleling target from AntiKS: ", Globals.Blue, TextType.SYSTEM);
#endif

                                    int delay = 0;

                                    while ((Globals.gamedata.my_char.TargetID != 0) && delay < 1500) //Give game time to lose the target, max 1,5 seconds
                                    {
                                        System.Threading.Thread.Sleep(50);
                                        delay += 50;
                                    }

                                    //System.Threading.Thread.Sleep(1500); //Give game time to lose the target
                                    Target(ignID); //get a new target
                                }

                                anti_ks_loops = 0;
                                anti_ks_changed_target = false;
                            }
                        }
                        else
                        {
                            anti_ks_loops++;
                        }

                    }

                    /////////////////////////////Do combat shit
                    if (!breaktotop && Globals.gamedata.CanAttack() && (Globals.gamedata.my_char.TargetID != 0) && ((Globals.gamedata.my_char.CurrentTargetType == TargetType.PLAYER) || (Globals.gamedata.my_char.CurrentTargetType == TargetType.NPC)))
                    {
                        //Summon instant attack
                        if (Globals.gamedata.botoptions.SummonInstantAttack == 1)
                        {
                            if (Globals.gamedata.botoptions.PetAssist == 1)
                            {
                                ServerPackets.Use_Action_Parse((uint)PClientAction.Pet_Attack, Globals.gamedata.Control, Globals.gamedata.Shift);
                            }
                            if (Globals.gamedata.botoptions.SummonAssist == 1)
                            {
                                ServerPackets.Use_Action_Parse((uint)PClientAction.Summon_Attack, Globals.gamedata.Control, Globals.gamedata.Shift);
                            }
                        }
                        if (Globals.gamedata.botoptions.MoveFirstNormal == 1)
                        {
                            if (Util.Distance(Globals.gamedata.my_char.TargetID) <= Globals.gamedata.botoptions.MoveRange)
                            {
                                //Mob is in range, do combat stuff
                                move_before_docombatloops = 25;


                                DoCombat();
                            }
                            else
                            {
                                if (move_before_docombatloops == 25)
                                {
                                    //need to move into range
                                    //was_smart_moving = true;
                                    //need to get the x,y,z, of where to move...
                                    int dx = 0, dy = 0, dz = 0;
                                    Util.GetLoc(Globals.gamedata.my_char.TargetID, ref dx, ref dy, ref dz);

                                    //Globals.l2net_home.Add_Text("Move to X: " + dx.ToString() + " Y: " + dy.ToString() + " Z: " + dz.ToString(), Globals.Green);
                                    //Globals.l2net_home.Add_Text("Sending moveto packet", Globals.Cyan, TextType.BOT);
                                    ServerPackets.MoveToPacket((dx + Util.RandomNumber(-100, 100)), (dy + Util.RandomNumber(-100, 100)), dz);
                                    //ServerPackets.MoveToPacket(Util.Float_Int32(dx), Util.Float_Int32(dy), Util.Float_Int32(dz));
                                    move_before_docombatloops = 0;
                                }
                                else
                                {
                                    move_before_docombatloops++;
                                }

                            }

                        }
                        else
                        {
                            DoCombat();
                        }
                    }

                    //attack it!
                    if (!breaktotop && Globals.gamedata.CanAttack() && ((Globals.gamedata.botoptions.Attack == 1) || (Globals.gamedata.botoptions.PetAttackSolo == 1)) && (Globals.gamedata.my_char.TargetID != 0))
                    {
                        #region MoveSmart
                        if (Globals.gamedata.botoptions.MoveFirst == 1)
                        {
                            if (Util.Distance(Globals.gamedata.my_char.TargetID) <= Globals.gamedata.botoptions.MoveRange)
                            {
                                was_smart_moving = false;

                                #region CannotSeeTarget
                                if (Globals.gamedata.my_char.CannotSeeTarget)
                                {
                                    if (Globals.gamedata.botoptions.StuckCheck == 1)
                                    {
                                        StuckCheckInternal();
                                    }

                                    if (blacklistloops == Globals.gamedata.botoptions.BlacklistTries) //char has been stuck 4 times on this mob, its probably unreachable.
                                    {
                                        blacklistloops = 0; //reset counter
                                        if (Globals.gamedata.botoptions.AutoBlacklist == 1)
                                        {
                                            BlacklistNPC(NPCID); //blacklist npc
                                            ServerPackets.Send_CancelTarget(); //cancel target
#if DEBUG
                                            Globals.l2net_home.Add_Text("Canceleling target from BlacklistNPC: ", Globals.Blue, TextType.SYSTEM);
#endif
                                            //System.Threading.Thread.Sleep(1500); //Give game time to lose the target
                                            int delay = 0;

                                            while ((Globals.gamedata.my_char.TargetID != 0) && delay < 1500) //Give game time to lose the target, max 1,5 seconds
                                            {
                                                System.Threading.Thread.Sleep(50);
                                                delay += 50;
                                            }

                                            //System.Threading.Thread.Sleep(1500); //Give game time to lose the target
                                            Target(); //get a new target
                                        }
                                    }

                                    blacklistloops++;
                                    Globals.gamedata.my_char.CannotSeeTarget = false;
                                }
                                #endregion

                                Attack();
                            }
                            else
                            {
                                //need to move into range
                                was_smart_moving = true;
                                //need to get the x,y,z, of where to move...
                                int dx = 0, dy = 0, dz = 0;
                                Util.GetLoc(Globals.gamedata.my_char.TargetID, ref dx, ref dy, ref dz);

                                Globals.scriptthread.Script_MOVE_SMART_Internal(dx, dy, dz, Globals.gamedata.botoptions.MoveRange, false);

                                if (ScriptEngine.is_Moving == false)
                                {
                                    was_smart_moving = false;
                                    #region CannotSeeTarget
                                    if (Globals.gamedata.my_char.CannotSeeTarget)
                                    {
                                        if (Globals.gamedata.botoptions.StuckCheck == 1)
                                        {
                                            StuckCheckInternal();
                                        }

                                        if (blacklistloops == Globals.gamedata.botoptions.BlacklistTries) //char has been stuck 4 times on this mob, its probably unreachable.
                                        {
                                            blacklistloops = 0; //reset counter
                                            if (Globals.gamedata.botoptions.AutoBlacklist == 1)
                                            {
                                                BlacklistNPC(NPCID); //blacklist npc
                                                ServerPackets.Send_CancelTarget(); //cancel target
#if DEBUG
                                                Globals.l2net_home.Add_Text("Canceleling target from autoBlacklistNPC: ", Globals.Blue, TextType.SYSTEM);
#endif
                                                //System.Threading.Thread.Sleep(1500); //Give game time to lose the target
                                                int delay = 0;

                                                while ((Globals.gamedata.my_char.TargetID != 0) && delay < 1500) //Give game time to lose the target, max 1,5 seconds
                                                {
                                                    System.Threading.Thread.Sleep(50);
                                                    delay += 50;
                                                }
                                                Target(); //get a new target
                                            }
                                        }

                                        blacklistloops++;
                                        Globals.gamedata.my_char.CannotSeeTarget = false;
                                    }
                                    #endregion

                                    Attack();
                                }

                                if (Util.Distance(Globals.gamedata.my_char.TargetID) <= Globals.gamedata.botoptions.MoveRange)
                                {
                                    was_smart_moving = false;

                                    #region CannotSeeTarget
                                    if (Globals.gamedata.my_char.CannotSeeTarget)
                                    {
                                        if (Globals.gamedata.botoptions.StuckCheck == 1)
                                        {
                                            StuckCheckInternal();
                                        }

                                        if (blacklistloops == Globals.gamedata.botoptions.BlacklistTries) //char has been stuck 4 times on this mob, its probably unreachable.
                                        {
                                            blacklistloops = 0; //reset counter
                                            if (Globals.gamedata.botoptions.AutoBlacklist == 1)
                                            {
                                                BlacklistNPC(NPCID); //blacklist npc
                                                ServerPackets.Send_CancelTarget(); //cancel target
#if DEBUG
                                                Globals.l2net_home.Add_Text("Canceleling target from autoBlacklistNPC: ", Globals.Blue, TextType.SYSTEM);
#endif
                                                //System.Threading.Thread.Sleep(1500); //Give game time to lose the target
                                                int delay = 0;

                                                while ((Globals.gamedata.my_char.TargetID != 0) && delay < 1500) //Give game time to lose the target, max 1,5 seconds
                                                {
                                                    System.Threading.Thread.Sleep(50);
                                                    delay += 50;
                                                }
                                                Target(); //get a new target
                                            }
                                        }

                                        blacklistloops++;
                                        Globals.gamedata.my_char.CannotSeeTarget = false;
                                    }
                                    #endregion

                                    Attack();
                                }



                                /*while (Script_IGNORE_ITEM)
                                {
                                    System.Threading.Thread.Sleep(1000);
                                }*/
                                //Globals.l2net_home.Add_Debug("Script move smart internal done");
                            }
                        }
                        #endregion
                        else if (Globals.gamedata.botoptions.MoveFirstNormal == 1)
                        {
                            if (Util.Distance(Globals.gamedata.my_char.TargetID) <= Globals.gamedata.botoptions.MoveRange)
                            {
                                //reset loop.
                                move_before_attackloops = 25;
                                //Distance is lower than input box, attack
                                was_smart_moving = false;

                                #region CannotSeeTarget
                                /*
                                if (Globals.gamedata.my_char.CannotSeeTarget)
                                {
                                    if (Globals.gamedata.botoptions.StuckCheck == 1)
                                    {
                                        StuckCheckInternal();
                                    }

                                    if (blacklistloops == Globals.gamedata.botoptions.BlacklistTries) //char has been stuck 4 times on this mob, its probably unreachable.
                                    {
                                        blacklistloops = 0; //reset counter
                                        if (Globals.gamedata.botoptions.AutoBlacklist == 1)
                                        {
                                            BlacklistNPC(NPCID); //blacklist npc
                                            ServerPackets.Send_CancelTarget(); //cancel target
                                            System.Threading.Thread.Sleep(1500); //Give game time to lose the target
                                            Target(); //get a new target
                                        }
                                    }

                                    blacklistloops++;
                                    Globals.gamedata.my_char.CannotSeeTarget = false;
                                }*/
                                #endregion

                                Attack();
                            }
                            else
                            {
                                if (move_before_attackloops == 25)
                                {
                                    //need to move into range
                                    //was_smart_moving = true;
                                    //need to get the x,y,z, of where to move...
                                    int dx = 0, dy = 0, dz = 0;
                                    Util.GetLoc(Globals.gamedata.my_char.TargetID, ref dx, ref dy, ref dz);

                                    //Globals.l2net_home.Add_Text("Move to X: " + dx.ToString() + " Y: " + dy.ToString() + " Z: " + dz.ToString(), Globals.Green);
                                    //Globals.l2net_home.Add_Text("Sending moveto packet", Globals.Cyan, TextType.BOT);
                                    ServerPackets.MoveToPacket((dx + Util.RandomNumber(-100, 100)), (dy + Util.RandomNumber(-100, 100)), dz);
                                    //ServerPackets.MoveToPacket(Util.Float_Int32(dx), Util.Float_Int32(dy), Util.Float_Int32(dz));
                                    move_before_attackloops = 0;
                                }
                                else
                                {
                                    move_before_attackloops++;
                                }
                            }
                        }
                        else if (Globals.gamedata.botoptions.MoveBeforeTargeting == 1 && Globals.gamedata.BOT_STATE != BotState.Attacking)
                        {
                            if (Util.Distance(new_id) <= Globals.gamedata.botoptions.MoveRange)
                            {
                                //reset loop.
                                move_before_attackloops = 25;
                                //Distance is lower than input box, attack
                                was_smart_moving = false;

                                #region CannotSeeTarget
                                if (Globals.gamedata.my_char.CannotSeeTarget)
                                {
                                    if (Globals.gamedata.botoptions.StuckCheck == 1)
                                    {
                                        StuckCheckInternal();
                                    }

                                    if (blacklistloops == Globals.gamedata.botoptions.BlacklistTries) //char has been stuck 4 times on this mob, its probably unreachable.
                                    {
                                        blacklistloops = 0; //reset counter
                                        if (Globals.gamedata.botoptions.AutoBlacklist == 1)
                                        {
                                            BlacklistNPC(NPCID); //blacklist npc
                                            ServerPackets.Send_CancelTarget(); //cancel target
#if DEBUG
                                            Globals.l2net_home.Add_Text("Canceleling target from autoBlacklistNPC: ", Globals.Blue, TextType.SYSTEM);
#endif
                                            //System.Threading.Thread.Sleep(1500); //Give game time to lose the target
                                            int delay = 0;

                                            while ((Globals.gamedata.my_char.TargetID != 0) && delay < 1500) //Give game time to lose the target, max 1,5 seconds
                                            {
                                                System.Threading.Thread.Sleep(50);
                                                delay += 50;
                                            }
                                            Target(); //get a new target
                                        }
                                    }

                                    blacklistloops++;
                                    Globals.gamedata.my_char.CannotSeeTarget = false;
                                }
                                #endregion


                                int tx = 0, ty = 0, tz = 0, delay2 = 0;

                                Util.GetLoc(new_id, ref tx, ref ty, ref tz);

                                //Globals.l2net_home.Add_Debug("Clicking on : " + new_id.ToString("X2"));
                                ServerPackets.ClickChar(new_id, tx, ty, tz, Globals.gamedata.Control, Globals.gamedata.Shift);

                                while (delay2 < Globals.SLEEP_BotAIDelay_Target)
                                {
                                    if ((old_id == 0 && Globals.gamedata.my_char.TargetID != 0) ||
                                        (old_id != Globals.gamedata.my_char.TargetID && Globals.gamedata.my_char.TargetID != 0))
                                    {
                                        Globals.gamedata.BOT_STATE = BotState.Attacking;
                                    }
                                    delay2 += Globals.SLEEP_BotAIDelay_TargetInc;
                                    System.Threading.Thread.Sleep(Globals.SLEEP_BotAIDelay_TargetInc);
                                }

                                Attack();
                            }
                            else
                            {
                                if (move_before_attackloops == 25)
                                {
                                    //need to move into range
                                    //was_smart_moving = true;
                                    //need to get the x,y,z, of where to move...
                                    int dx = 0, dy = 0, dz = 0;
                                    Util.GetLoc(new_id, ref dx, ref dy, ref dz);

                                    //Globals.l2net_home.Add_Text("Move to X: " + dx.ToString() + " Y: " + dy.ToString() + " Z: " + dz.ToString(), Globals.Green);
                                    //Globals.l2net_home.Add_Text("Sending moveto packet", Globals.Cyan, TextType.BOT);
                                    ServerPackets.MoveToPacket((dx + Util.RandomNumber(-100, 100)), (dy + Util.RandomNumber(-100, 100)), dz);
                                    //ServerPackets.MoveToPacket(Util.Float_Int32(dx), Util.Float_Int32(dy), Util.Float_Int32(dz));
                                    move_before_attackloops = 0;
                                }
                                else
                                {
                                    move_before_attackloops++;
                                }
                            }
                        }
                        else
                        {
                            was_smart_moving = false;

                            #region CannotSeeTarget
                            if (Globals.gamedata.my_char.CannotSeeTarget)
                            {
                                if (Globals.gamedata.botoptions.StuckCheck == 1)
                                {
                                    StuckCheckInternal();
                                }

                                if (blacklistloops == Globals.gamedata.botoptions.BlacklistTries) //char has been stuck 4 times on this mob, its probably unreachable.
                                {
                                    blacklistloops = 0; //reset counter
                                    if (Globals.gamedata.botoptions.AutoBlacklist == 1)
                                    {
                                        BlacklistNPC(NPCID); //blacklist npc
                                        ServerPackets.Send_CancelTarget(); //cancel target
#if DEBUG
                                        Globals.l2net_home.Add_Text("Canceleling target from autoBlacklistNPC: ", Globals.Blue, TextType.SYSTEM);
#endif
                                        //System.Threading.Thread.Sleep(1500); //Give game time to lose the target
                                        int delay = 0;

                                        while ((Globals.gamedata.my_char.TargetID != 0) && delay < 1500) //Give game time to lose the target, max 1,5 seconds
                                        {
                                            System.Threading.Thread.Sleep(50);
                                            delay += 50;
                                        }
                                        Target(); //get a new target
                                    }
                                }

                                blacklistloops++;
                                Globals.gamedata.my_char.CannotSeeTarget = false;
                            }
                            #endregion

                            Attack();
                        }
                    }


                    //logout shit
                }
                if ((Globals.gamedata.BOTTING) && (Globals.gamedata.my_char.Cur_HP <= 0))
                {

                    if (Globals.gamedata.botoptions.DeadToggleBotting == 1)
                    {
                        Globals.l2net_home.Toggle_Botting(1);
                    }

                    if ((Globals.gamedata.botoptions.DeadLogoutDelay > Globals.gamedata.botoptions.DeadReturnDelay) && (Globals.gamedata.botoptions.DeadLogout == 1) && (Globals.gamedata.botoptions.DeadReturn >= 0))
                    {
                        //Return First
                        System.Threading.Thread.Sleep(Globals.gamedata.botoptions.DeadReturnDelay * 1000);
                        ServerPackets.Return(Globals.gamedata.botoptions.DeadReturn);

                        //Then Logout
                        System.Threading.Thread.Sleep((Globals.gamedata.botoptions.DeadLogoutDelay - Globals.gamedata.botoptions.DeadReturnDelay) * 1000);
                        ServerPackets.Send_Logout();
                    }

                    if (Globals.gamedata.botoptions.DeadLogout == 1)
                    {
                        System.Threading.Thread.Sleep(Globals.gamedata.botoptions.DeadLogoutDelay * 1000);
                        ServerPackets.Send_Logout();
                    }

                    if (Globals.gamedata.botoptions.DeadReturn >= 0)
                    {
                        System.Threading.Thread.Sleep(Globals.gamedata.botoptions.DeadReturnDelay * 1000);
                        ServerPackets.Return(Globals.gamedata.botoptions.DeadReturn);
                    }



                }

                if (!breaktotop && (Globals.gamedata.BOTTING) && (Globals.gamedata.my_char.Cur_HP > 0) && (Globals.gamedata.botoptions.MoveToLoc == 1))
                {
                    OOCLoops++;
                    OOCDist = DISTANCE(Util.GetInt32(Globals.gamedata.botoptions.Moveto_X), Util.GetInt32(Globals.gamedata.botoptions.Moveto_Y), Util.GetInt32(Globals.gamedata.botoptions.Moveto_Z), Util.Float_Int32(Globals.gamedata.my_char.X), Util.Float_Int32(Globals.gamedata.my_char.Y), Util.Float_Int32(Globals.gamedata.my_char.Z));
                    if (Globals.gamedata.botoptions.OutOfCombat == 1)
                    {

                        if (CheckForMobs() == 0 && OOCDist > Globals.gamedata.botoptions.MoveToLeash && OOCDist < 4000)
                        {
                            if (Globals.gamedata.my_char.TargetID != 0 && Globals.gamedata.botoptions.AutoSpoil == 0)
                            {
                                ServerPackets.Send_CancelTarget();
                                System.Threading.Thread.Sleep(100);
                                ServerPackets.Send_CancelTarget();
                                System.Threading.Thread.Sleep(100);

                            }
                            if (OOCLoops > 15)
                            {
                                //move to location
                                OOCLoops = 0;
                                ServerPackets.MoveToPacket(Util.GetInt32(Globals.gamedata.botoptions.Moveto_X), Util.GetInt32(Globals.gamedata.botoptions.Moveto_Y), Util.GetInt32(Globals.gamedata.botoptions.Moveto_Z));
#if DEBUG
                                Globals.l2net_home.Add_Text("moving when no mobs: " + CheckForMobs() + " dist " + OOCDist);
#endif
                            }
                        }
                        else
                        {
                            //mobs still around
#if DEBUG
                            Globals.l2net_home.Add_Text("not moving anywhere: " + CheckForMobs());
#endif
                        }
                    }

                    else
                    {
                        if (OOCDist > Globals.gamedata.botoptions.MoveToLeash && OOCDist < 4000 && OOCLoops > 5)
                        {

                            //move to location
                            OOCLoops = 0;
                            ServerPackets.MoveToPacket(Util.GetInt32(Globals.gamedata.botoptions.Moveto_X), Util.GetInt32(Globals.gamedata.botoptions.Moveto_Y), Util.GetInt32(Globals.gamedata.botoptions.Moveto_Z));
#if DEBUG
                            Globals.l2net_home.Add_Text("moving regardless ");
#endif
                        }

                    }
                }


            }//end of while loop
        }
            

    

        private uint CheckForMobs()
        {
            return Globals.scriptthread.Script_TARGET_NEAREST_Internal(0, false);
        }

        private uint DISTANCE(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            double xlim = System.Convert.ToDouble(x1) - System.Convert.ToDouble(x2);
            double ylim = System.Convert.ToDouble(y1) - System.Convert.ToDouble(y2);
            double zlim = System.Convert.ToDouble(z1) - System.Convert.ToDouble(z2);

            double dist = System.Math.Sqrt(System.Math.Pow(xlim, 2) + System.Math.Pow(ylim, 2) + System.Math.Pow(zlim, 2));

            return (uint)dist;
        }


        private bool Target()
        {
            bool tmp = Target(0);
            return tmp;
        }

        private bool Target(uint IgnoreID)
        {
            bool need_target = true;

            switch (Globals.gamedata.my_char.CurrentTargetType)
            {
                case TargetType.ERROR:
                    need_target = true;
                    break;
                case TargetType.NONE:
                case TargetType.SELF:
                case TargetType.ITEM:
                case TargetType.MYPET:
                case TargetType.MYPET1:
                case TargetType.MYPET2:
                case TargetType.MYPET3:
                    need_target = true;
                    break;
                case TargetType.PLAYER:
                    CharInfo player = null;

                    Globals.PlayerLock.EnterReadLock();
                    try
                    {
                        player = Util.GetChar(Globals.gamedata.my_char.TargetID);
                    }
                    finally
                    {
                        Globals.PlayerLock.ExitReadLock();
                    }

                    if (player != null)
                    {
                        if (player.isAlikeDead != 0)
                        {
                            need_target = true;
                        }
                        else
                        {
                            need_target = false;
                        }
                    }
                    break;
                case TargetType.NPC:
                    NPCInfo npc = null;

                    Globals.NPCLock.EnterReadLock();
                    try
                    {
                        npc = Util.GetNPC(Globals.gamedata.my_char.TargetID);
                    }
                    finally
                    {
                        Globals.NPCLock.ExitReadLock();
                    }

                    if (npc != null)
                    {
                        if (npc.isAlikeDead != 0)
                        {
                            need_target = true;
                            if (npc.IsSpoiled && Globals.gamedata.botoptions.AutoSweep == 1)
                            {
                                AutoSweep();
                            }
                        }
                        else
                        {
                            need_target = false;
                        }
                    }
                    break;
            }

            if (Globals.gamedata.BOT_STATE == BotState.FinishedBuffing)
            {
                //we just finished a buff of some sort
                need_target = true;
            }

            if (need_target)
            {
                old_id = Globals.gamedata.my_char.TargetID;
                //Globals.l2net_home.Add_Text("I'm not attacking something (need_target)", Globals.Green, TextType.BOT);
                Globals.gamedata.my_char.isAttacking = false;

                //time to find a new target
                new_id = Globals.scriptthread.Script_TARGET_NEAREST_Internal(IgnoreID);
                if (new_id == 0)
                    return false;

                //Globals.l2net_home.Add_Text("Found target: " + new_id.ToString("X2"), Globals.Green);

                if (old_id == new_id)
                {
                    //our current target is really still a good target
                    Globals.gamedata.BOT_STATE = BotState.Attacking;
                    return true;
                }

                if (Globals.gamedata.botoptions.MoveBeforeTargeting == 0)
                {
                    int delay = 0;
                    while (delay < Globals.SLEEP_BotAIDelay_Target)
                    {
                        if ((old_id == 0 && Globals.gamedata.my_char.TargetID != 0) ||
                            (old_id != Globals.gamedata.my_char.TargetID && Globals.gamedata.my_char.TargetID != 0))
                        {
                            Globals.gamedata.BOT_STATE = BotState.Attacking;
                            return true;
                        }
                        delay += Globals.SLEEP_BotAIDelay_TargetInc;
                        System.Threading.Thread.Sleep(Globals.SLEEP_BotAIDelay_TargetInc);
                    }
                    target_timeout_loops++;
                    if (target_timeout_loops == 5) //10 seconds
                    {
                        BlacklistNPC(new_id, false);
                        target_timeout_loops = 0;
                    }
                }
                else
                {
                    return true;
                }
            }

            return false;
        }

        private void Attack()
        {

            //If char is disabled, sleep
            if ((Globals.gamedata.my_char.HasEffect(AbnormalEffects.FEAR)) ||
                (Globals.gamedata.my_char.HasEffect(AbnormalEffects.STUN)) ||
                (Globals.gamedata.my_char.HasEffect(AbnormalEffects.SLEEP)) ||
                (Globals.gamedata.my_char.HasEffect(AbnormalEffects.HOLD_1)) ||
                (Globals.gamedata.my_char.HasEffect(AbnormalEffects.PETRIFIED)) ||
                (Globals.gamedata.my_char.HasEffect(AbnormalEffects.FLOATING_ROOT)) ||
                (Globals.gamedata.my_char.HasEffect(AbnormalEffects.DANCE_STUNNED)) ||
                (Globals.gamedata.my_char.HasEffect(AbnormalEffects.FIREROOT_STUN)) ||
                (Globals.gamedata.my_char.HasEffect(AbnormalEffects.SKULL_FEAR)) ||
                (Globals.gamedata.my_char.HasExtendedEffect(ExtendedEffects.AIR_STUN)) ||
                (Globals.gamedata.my_char.HasExtendedEffect(ExtendedEffects.FREEZING)))
            {
                Globals.l2net_home.Add_Text("I'm not attacking something (stunned etc)", Globals.Green, TextType.BOT);
                Globals.gamedata.my_char.isAttacking = false;
                System.Threading.Thread.Sleep(Globals.SLEEP_BotAIDelay);
            }
            else
            {
                //Spoil first
                if (Globals.gamedata.botoptions.AutoSpoil == 1)
                {
                    if (!Globals.gamedata.my_char.TargetSpoiled && Globals.gamedata.CanAttack() && (Globals.gamedata.my_char.TargetID != 0) && (Globals.gamedata.my_char.CurrentTargetType == TargetType.NPC))
                    {
                        AutoSpoil();
                        if (Globals.gamedata.botoptions.AutoSpoilUntilSuccess == 0)
                        {
                            Globals.gamedata.my_char.TargetSpoiled = true;
                        }
                        Globals.gamedata.my_char.isAttacking = false;
                    }
                }
                //time to kill our target
                if (Globals.gamedata.botoptions.SummonInstantAttack != 1) //if summon is not already attacking (assisting master)
                {
                    if (Globals.gamedata.botoptions.PetAssist == 1)
                    {
                        ServerPackets.Use_Action_Parse((uint)PClientAction.Pet_Attack, Globals.gamedata.Control, Globals.gamedata.Shift);
                    }
                    if (Globals.gamedata.botoptions.SummonAssist == 1)
                    {
                        ServerPackets.Use_Action_Parse((uint)PClientAction.Summon_Attack, Globals.gamedata.Control, Globals.gamedata.Shift);
                    }
                }
                if (!Globals.gamedata.my_char.isAttacking && (Globals.gamedata.botoptions.PetAttackSolo != 1))
                {
                    Globals.scriptthread.Script_ATTACK_TARGET();
                    //Globals.l2net_home.Add_Text("Sending attack packet", Globals.Cyan, TextType.BOT);
                }
                System.Threading.Thread.Sleep(Globals.SLEEP_BotAIDelay);

            }
        }

        private void DoBuffsPart1()
        {
            try
            {
                //need to set the buff/heal as having been cast now
                Globals.gamedata.BOT_STATE = BotState.BuffWaiting;

                ServerPackets.Try_Use_Skill(Globals.gamedata.my_char.BuffSkillID, Globals.gamedata.botoptions.ControlBuffing == 1, Globals.gamedata.botoptions.ShiftBuffing == 1);

                Globals.gamedata.my_char.LastBuffTime = System.DateTime.Now;
                //Globals.l2net_home.Add_Text("I'm not attacking something (buffing)", Globals.Green, TextType.BOT);
                Globals.gamedata.my_char.isAttacking = false;
                breaktotop = true;
            }
            catch
            {
                Globals.l2net_home.Add_Error("crash: DoBuffsPart1");
            }
        }

        private void DoBuffsPart2()
        {
            Globals.BuffListLock.EnterReadLock();
            Globals.BuffsGivenLock.EnterWriteLock();
            try
            {
                foreach (BuffTargetClass bft in BotOptions.BuffTargets)
                {
                    if (bft.Active == true && bft.IsReady())
                    {
                        for (int name_i = 0; name_i < bft.TargetNames.Count; name_i++)
                        {
                            found = false;

                            foreach (CharBuffTimer cbt in Globals.gamedata.BuffsGiven)
                            {
                                if (System.String.Equals(cbt.Name, ((string)bft.TargetNames[name_i])))
                                {
                                    found = true;
                                    needs_buff = false;
                                    switch (cbt.Has_Buff(bft))
                                    {
                                        case BuffState.DoesntHave:
                                            //doesnt have the buff
                                            cbt.Add_Buff(bft);
                                            needs_buff = true;
                                            break;
                                        case BuffState.Needs://needs buff
                                            needs_buff = true;
                                            break;
                                        case BuffState.Has://doesnt need buff
                                            //needs_buff = false;
                                            break;
                                    }//end of switch on buffstate
                                    if (needs_buff)
                                    {
                                        if (ServerPackets.TrySkill(((string)bft.TargetNames[name_i]), cbt, bft))
                                        {
                                            Globals.gamedata.Set_Char_To_Buffing();
                                            breaktotop = true;
                                            return;
                                        }
                                        else
                                        {
                                            //the target is too far or something for us to buff it
                                            //set it to have an extra delay of 20 seconds
                                            cbt.Add_Time(bft.SkillID, Globals.FAILED_BUFF);
                                        }
                                    }
                                    break;
                                }
                            }//end of foreach cbt

                            if (breaktotop)
                                return;

                            if (!found)
                            {
                                //need to add this name to the list
                                CharBuffTimer cbuff = new CharBuffTimer();
                                cbuff.Name = ((string)bft.TargetNames[name_i]).ToUpperInvariant();
                                Globals.gamedata.BuffsGiven.Add(cbuff);
                                cbuff.Add_Buff(bft);

                                if (cbuff.Has_Buff(bft) != BuffState.Has)
                                {
                                    if (ServerPackets.TrySkill(((string)bft.TargetNames[name_i]), cbuff, bft))
                                    {
                                        Globals.gamedata.Set_Char_To_Buffing();
                                        breaktotop = true;
                                        return;
                                    }
                                    else
                                    {
                                        //the target is too far for us to buff it or something
                                        //set it to have an extra delay of 20 seconds
                                        cbuff.Add_Time(bft.SkillID, Globals.FAILED_BUFF);
                                    }
                                }
                            }
                        }//end of foreach name
                    }//end of isActive
                    if (breaktotop)
                        return;
                }//end of for buffs
            }//unlock
            catch
            {
                Globals.l2net_home.Add_Error("crash: DoBuffsPart2");
            }
            finally
            {
                Globals.BuffsGivenLock.ExitWriteLock();
                Globals.BuffListLock.ExitReadLock();
                Globals.gamedata.my_char.isAttacking = false;
            }
        }

        private bool CheckItemsForPickup()
        {
            if (Script_Ops.NEAREST_ITEM_DISTANCE(true) <= Globals.gamedata.botoptions.LootRange)
            {
                return true;
            }
            return false;
        }

        private void Pickup()
        {
            uint nearest_item;
            nearest_item = Script_Ops.NEAREST_ITEM_DISTANCE(false);
            if (nearest_item != 0)
            {
                if (nearest_item == BotAIThread.PickUpItem)
                {
                    if (BotAIThread.PickUpItemSleep >= (Globals.gamedata.botoptions.PickupTimeout * 1000)) //Globals.SLEEP_BotAIDelay_Pickup)
                    {
                        //need to flag this item as useless...
                        Util.IgnoreItem(nearest_item, true);
                        return;
                    }
                    else
                    {
                        //will pick up below
                    }
                }
                else
                {
                    BotAIThread.PickUpItemSleep = 0;
                    BotAIThread.PickUpItem = nearest_item;
                    //will pickup below
                }

                if ((Globals.gamedata.my_char.HasEffect(AbnormalEffects.FEAR)) ||
                (Globals.gamedata.my_char.HasEffect(AbnormalEffects.STUN)) ||
                (Globals.gamedata.my_char.HasEffect(AbnormalEffects.SLEEP)) ||
                (Globals.gamedata.my_char.HasEffect(AbnormalEffects.HOLD_1)) ||
                (Globals.gamedata.my_char.HasEffect(AbnormalEffects.PETRIFIED)) ||
                (Globals.gamedata.my_char.HasEffect(AbnormalEffects.FLOATING_ROOT)) ||
                (Globals.gamedata.my_char.HasEffect(AbnormalEffects.DANCE_STUNNED)) ||
                (Globals.gamedata.my_char.HasEffect(AbnormalEffects.FIREROOT_STUN)) ||
                (Globals.gamedata.my_char.HasEffect(AbnormalEffects.SKULL_FEAR)) ||
                (Globals.gamedata.my_char.HasExtendedEffect(ExtendedEffects.AIR_STUN)) ||
                (Globals.gamedata.my_char.HasExtendedEffect(ExtendedEffects.FREEZING)))
                {
                    Globals.l2net_home.Add_Text("I'm not picking up something because... (stunned etc)", Globals.Green, TextType.BOT);
                }
                else
                {
                    Script_CLICK_NEAREST_ITEM_GUI(nearest_item);
                    System.Threading.Thread.Sleep(Globals.SLEEP_BotAIDelay_PickupInc);
                    BotAIThread.PickUpItemSleep += Globals.SLEEP_BotAIDelay_PickupInc;
                }
            }
            else 
            {
                //not breaking to top, and we don't give a shit about the item since it's id is 0
                Globals.picking_up_items = false;
            }

        }


        public void Script_CLICK_NEAREST_ITEM_GUI(long nearest_item)
        {
            float mx = Globals.gamedata.my_char.X;
            float my = Globals.gamedata.my_char.Y;
            float mz = Globals.gamedata.my_char.Z;
            float x = 0, y = 0, z = 0;

            Globals.DoNotItemLock.EnterReadLock();
            Globals.ItemLock.EnterReadLock();
            try
            {
                foreach (ItemInfo item in Globals.gamedata.nearby_items.Values)
                {
                    if (item.ID == nearest_item)
                    {
                        x = item.X;
                        y = item.Y;
                        z = item.Z;
                        break;
                    }
                }
            }//unlock
            finally
            {
                Globals.ItemLock.ExitReadLock();
                Globals.DoNotItemLock.ExitReadLock();
            }

            if (nearest_item != 0)
            {
                ServerPackets.ClickItem((uint)nearest_item, Util.Float_Int32(x), Util.Float_Int32(y), Util.Float_Int32(z), false);
            }
        }

        private void DoItems()
        {
            Globals.ItemListLock.EnterReadLock();
            try
            {
                foreach (ItemTargetClass it in BotOptions.ItemTargets)
                {
                    if (it.Active && it.ItemID != 0 && it.LastTickTime + it.TickDuration < System.DateTime.Now.Ticks)
                    {
                        bool use_item = false;

                        switch (it.Type)
                        {
                            case BuffTriggers.Always://always
                                use_item = true;
                                break;
                            case BuffTriggers.CP:
                                if (((float)Globals.gamedata.my_char.Cur_CP) / ((float)Globals.gamedata.my_char.Max_CP) < ((float)it.Min_Per) / 100.0f)
                                    use_item = true;
                                break;
                            case BuffTriggers.HP:
                                if (((float)Globals.gamedata.my_char.Cur_HP) / ((float)Globals.gamedata.my_char.Max_HP) < ((float)it.Min_Per) / 100.0f)
                                    use_item = true;
                                break;
                            case BuffTriggers.MP:
                                if (((float)Globals.gamedata.my_char.Cur_MP) / ((float)Globals.gamedata.my_char.Max_MP) < ((float)it.Min_Per) / 100.0f)
                                    use_item = true;
                                break;
                            case BuffTriggers.Dead:
                                if (Globals.gamedata.my_char.Cur_HP == 0)
                                    use_item = true;
                                break;
                            case BuffTriggers.Charges:
                                if (Globals.gamedata.my_char.Charges < it.Min_Per)
                                    use_item = true;
                                break;
                            case BuffTriggers.Souls:
                                if (Globals.gamedata.my_char.Souls < it.Min_Per)
                                    use_item = true;
                                break;
                            case BuffTriggers.DeathPenalty:
                                if (Globals.gamedata.my_char.DeathPenalty >= it.Min_Per)
                                    use_item = true;
                                break;
                            case BuffTriggers.AB_Bleeding:
                                if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.BLEEDING))
                                    use_item = true;
                                break;
                            case BuffTriggers.AB_Poison:
                                if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.POISON))
                                    use_item = true;
                                break;
                            case BuffTriggers.AB_Ice:
                                if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.ICE))
                                    use_item = true;
                                break;
                            case BuffTriggers.AB_Wind:
                                if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.WIND))
                                    use_item = true;
                                break;
                            case BuffTriggers.AB_Fear:
                                if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.FEAR))
                                    use_item = true;
                                break;
                            case BuffTriggers.AB_Stun:
                                if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.STUN))
                                    use_item = true;
                                break;
                            case BuffTriggers.AB_Sleep:
                                if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.SLEEP))
                                    use_item = true;
                                break;
                            case BuffTriggers.AB_Muted:
                                if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.MUTED))
                                    use_item = true;
                                break;
                            case BuffTriggers.AB_Root:
                                if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.ROOT))
                                    use_item = true;
                                break;
                            case BuffTriggers.AB_Paralysis:
                                if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.HOLD_1))
                                    use_item = true;
                                break;
                            case BuffTriggers.AB_Petrified:
                                if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.PETRIFIED))
                                    use_item = true;
                                break;
                            case BuffTriggers.AB_Invulnerable:
                                if (Globals.gamedata.my_char.HasEffect(AbnormalEffects.INVULNERABLE))
                                    use_item = true;
                                break;
                        }

                        if (use_item)
                        {
                            it.LastTickTime = System.DateTime.Now.Ticks;
                            //should be access by reference

                            //lets check for the item still in inventory
                            ServerPackets.Try_Use_Item(it.ItemID, false);
                        }
                    }//end of isActive
                }
            }
            catch
            {
                Globals.l2net_home.Add_Error("crash: DoItems");
            }
            finally
            {
                Globals.ItemListLock.ExitReadLock();
            }
        }

        private void DoCombat()
        {
            //gotta get our target's info
            float hp_per = 0;
            float mp_per = 0;
            float cp_per = 0;
            bool is_summon = false;

            switch (Globals.gamedata.my_char.CurrentTargetType)
            {
                case TargetType.SELF:
                    if (Globals.gamedata.my_char.TargetID == Globals.gamedata.my_char.ID)
                    {
                        hp_per = Globals.gamedata.my_char.Cur_HP / Globals.gamedata.my_char.Max_HP;
                        mp_per = Globals.gamedata.my_char.Cur_MP / Globals.gamedata.my_char.Max_MP;
                        cp_per = Globals.gamedata.my_char.Cur_CP / Globals.gamedata.my_char.Max_CP;
                    }
                    break;
                case TargetType.MYPET:
                    if (Globals.gamedata.my_char.TargetID == Globals.gamedata.my_pet.ID)
                    {
                        hp_per = Globals.gamedata.my_pet.Cur_HP / Globals.gamedata.my_pet.Max_HP;
                        mp_per = Globals.gamedata.my_pet.Cur_MP / Globals.gamedata.my_pet.Max_MP;
                        cp_per = Globals.gamedata.my_pet.Cur_CP / Globals.gamedata.my_pet.Max_CP;
                    }
                    break;
                case TargetType.MYPET1:
                    if (Globals.gamedata.my_char.TargetID == Globals.gamedata.my_pet1.ID)
                    {
                        hp_per = Globals.gamedata.my_pet1.Cur_HP / Globals.gamedata.my_pet1.Max_HP;
                        mp_per = Globals.gamedata.my_pet1.Cur_MP / Globals.gamedata.my_pet1.Max_MP;
                        cp_per = Globals.gamedata.my_pet1.Cur_CP / Globals.gamedata.my_pet1.Max_CP;
                    }
                    break;
                case TargetType.MYPET2:
                    if (Globals.gamedata.my_char.TargetID == Globals.gamedata.my_pet2.ID)
                    {
                        hp_per = Globals.gamedata.my_pet2.Cur_HP / Globals.gamedata.my_pet2.Max_HP;
                        mp_per = Globals.gamedata.my_pet2.Cur_MP / Globals.gamedata.my_pet2.Max_MP;
                        cp_per = Globals.gamedata.my_pet2.Cur_CP / Globals.gamedata.my_pet2.Max_CP;
                    }
                    break;
                case TargetType.MYPET3:
                    if (Globals.gamedata.my_char.TargetID == Globals.gamedata.my_pet3.ID)
                    {
                        hp_per = Globals.gamedata.my_pet3.Cur_HP / Globals.gamedata.my_pet3.Max_HP;
                        mp_per = Globals.gamedata.my_pet3.Cur_MP / Globals.gamedata.my_pet3.Max_MP;
                        cp_per = Globals.gamedata.my_pet3.Cur_CP / Globals.gamedata.my_pet3.Max_CP;
                    }
                    break;
                case TargetType.PLAYER:
                    Globals.PlayerLock.EnterReadLock();
                    try
                    {
                        CharInfo player = Util.GetChar(Globals.gamedata.my_char.TargetID);

                        if (player != null)
                        {
                            hp_per = player.Cur_HP / player.Max_HP;
                            mp_per = player.Cur_MP / player.Max_MP;
                            cp_per = player.Cur_CP / player.Max_CP;
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
                            hp_per = npc.Cur_HP / npc.Max_HP;
                            mp_per = npc.Cur_MP / npc.Max_MP;
                            cp_per = npc.Cur_CP / npc.Max_CP;
                            if (npc.SummonedNameColor > 0)
                            {
                                is_summon = true;
                            }
                        }
                    }//unlock
                    finally
                    {
                        Globals.NPCLock.ExitReadLock();
                    }
                    break;
            }

            Globals.CombatListLock.EnterReadLock();
            try
            {
                foreach (CombatTargetClass ct in BotOptions.CombatTargets)
                {
                    if (ct.Active && (ct.LastTickTime + ct.TickDuration <= System.DateTime.Now.Ticks) && (Globals.gamedata.my_char.Cur_MP >= ct.Min_MP))
                    {
                        bool use_item = false;

                        switch (ct.Type)
                        {
                            case BuffTriggers.Always:
                                use_item = true;
                                break;
                            case BuffTriggers.CP:
                                if (ct.Conditional == 0)
                                {
                                    if (cp_per >= ((float)ct.Min_Per) / 100.0f)
                                        use_item = true;
                                }
                                else
                                {
                                    if (cp_per <= ((float)ct.Min_Per) / 100.0f)
                                        use_item = true;
                                }
                                break;
                            case BuffTriggers.HP:
                                if (ct.Conditional == 0)
                                {
                                    if (hp_per >= ((float)ct.Min_Per) / 100.0f)
                                        use_item = true;
                                }
                                else
                                {
                                    if (hp_per <= ((float)ct.Min_Per) / 100.0f)
                                        use_item = true;
                                }
                                break;
                            case BuffTriggers.MP:
                                if (ct.Conditional == 0)
                                {
                                    if (mp_per >= ((float)ct.Min_Per) / 100.0f)
                                        use_item = true;
                                }
                                else
                                {
                                    if (mp_per <= ((float)ct.Min_Per) / 100.0f)
                                        use_item = true;
                                }
                                break;
                            case BuffTriggers.Dead:
                                if (hp_per == 0)
                                    use_item = true;
                                break;
                            case BuffTriggers.Charges:
                                if (ct.Conditional == 0)
                                {
                                    if (Globals.gamedata.my_char.Charges >= ct.Min_Per)
                                        use_item = true;
                                }
                                else
                                {
                                    if (Globals.gamedata.my_char.Charges <= ct.Min_Per)
                                        use_item = true;
                                }
                                break;
                            case BuffTriggers.Souls:
                                if (ct.Conditional == 0)
                                {
                                    if (Globals.gamedata.my_char.Souls >= ct.Min_Per)
                                        use_item = true;
                                }
                                else
                                {
                                    if (Globals.gamedata.my_char.Souls <= ct.Min_Per)
                                        use_item = true;
                                }
                                break;
                            case BuffTriggers.DeathPenalty:
                                if (ct.Conditional == 0)
                                {
                                    if (Globals.gamedata.my_char.DeathPenalty >= ct.Min_Per)
                                        use_item = true;
                                }
                                else
                                {
                                    if (Globals.gamedata.my_char.DeathPenalty <= ct.Min_Per)
                                        use_item = true;
                                }
                                break;
                        }

                        if (use_item)
                        {
                            breaktotop = true;

                            ct.LastTickTime = System.DateTime.Now.Ticks;

                            if (!is_summon && !Globals.picking_up_items)
                            {
                                ServerPackets.Use_ShortCut(ct.ShortCutID, Globals.gamedata.Control, Globals.gamedata.Shift);
                            }

                            if ((Globals.gamedata.my_char.CannotSeeTarget) && (Globals.gamedata.botoptions.MoveFirstNormal == 1))
                            {
                                int dx = 0, dy = 0, dz = 0;
                                Util.GetLoc(Globals.gamedata.my_char.TargetID, ref dx, ref dy, ref dz);
                                ServerPackets.MoveToPacket((dx + Util.RandomNumber(-100, 100)), (dy + Util.RandomNumber(-100, 100)), dz);
                                Globals.gamedata.my_char.CannotSeeTarget = false;
                            }
                            is_summon = false;

                        }
                    }//end of isActive
                }
            }
            catch
            {
                Globals.l2net_home.Add_Error("crash: DoCombat");
            }
            finally
            {
                Globals.CombatListLock.ExitReadLock();
            }
        }

        private void AutoSpoil()
        {
            if (Globals.gamedata.my_char.Cur_MP >= Globals.gamedata.botoptions.SpoilMPAbove)
            {
                Globals.SkillListLock.EnterReadLock();
                try
                {
                    bool used = false;

                    if (Globals.gamedata.botoptions.SpoilCrush == 1)
                    {
                        if (Globals.gamedata.skills.ContainsKey(Globals.Skill_SPOILCRUSH))//spoil crush
                        {
                            UserSkill sk = (UserSkill)Globals.gamedata.skills[Globals.Skill_SPOILCRUSH];
                            if (sk.IsReady())
                            {
                                used = true;
                                breaktotop = true;
                                ServerPackets.Use_Skill(sk.ID);
                                sk.LastTime = System.DateTime.Now;
                            }
                        }
                    }

                    if ((Globals.gamedata.botoptions.Plunder == 1) && (used == false))
                    {
                        if (Globals.gamedata.skills.ContainsKey(Globals.Skill_PLUNDER))//Plunder
                        {
                            UserSkill sk = (UserSkill)Globals.gamedata.skills[Globals.Skill_PLUNDER];
                            if (sk.IsReady())
                            {
                                used = true;
                                breaktotop = true;
                                ServerPackets.Use_Skill(sk.ID);
                                sk.LastTime = System.DateTime.Now;
                            }
                        }
                    }

                    if (used == false)
                    {
                        if (Globals.gamedata.skills.ContainsKey(Globals.Skill_SPOIL))//spoil
                        {
                            UserSkill sk = (UserSkill)Globals.gamedata.skills[Globals.Skill_SPOIL];
                            if (sk.IsReady())
                            {
                                used = true;
                                breaktotop = true;
                                ServerPackets.Use_Skill(sk.ID);
                                sk.LastTime = System.DateTime.Now;
                            }
                        }
                    }
                }
                catch
                {
                    Globals.l2net_home.Add_Error("crash: AutoSpoil");
                }
                finally
                {
                    Globals.SkillListLock.ExitReadLock();
                }
            }
        }

        private void ActiveFollow()
        {
            CharInfo player = null;

            Globals.PlayerLock.EnterReadLock();
            try
            {
                player = Util.GetChar(Globals.gamedata.botoptions.ActiveFollowID);
            }
            finally
            {
                Globals.PlayerLock.ExitReadLock();
            }

            try
            {
                if (player != null)
                {
                    float vx, vy, vz;
                    float vxx;
                    float THRESHOLDs2;

                    if (Globals.gamedata.botoptions.ActiveFollowStyle == 0)
                    {//l2.net style follow
                        vx = player.X - Globals.gamedata.my_char.X;
                        vy = player.Y - Globals.gamedata.my_char.Y;
                        vz = player.Z - Globals.gamedata.my_char.Z;
                        THRESHOLDs2 = Globals.THRESHOLD_L2NET;
                    }
                    else
                    {//walker style follow
                        if ((player.Moving) && ((player.CurrentTargetType == TargetType.NONE) || (Globals.gamedata.botoptions.ActiveFollowAttack == 1)))
                        {
                            vx = player.Dest_X - Globals.gamedata.my_char.X;
                            vy = player.Dest_Y - Globals.gamedata.my_char.Y;
                            vz = player.Dest_Z - Globals.gamedata.my_char.Z;
                        }
                        else
                        {
                            vx = player.X - Globals.gamedata.my_char.X;
                            vy = player.Y - Globals.gamedata.my_char.Y;
                            vz = player.Z - Globals.gamedata.my_char.Z;
                        }
                        THRESHOLDs2 = 0;
                    }

                    vxx = System.Convert.ToSingle(Math.Sqrt(vx * vx + vy * vy + vz * vz));

                    if (vxx < Globals.gamedata.botoptions.ActiveFollowDistance + Globals.THRESHOLD + THRESHOLDs2)
                    {
                        //we are close enough
                    }
                    else
                    {
                        float ratio = Util.Float_Cap(1.0F - (Globals.gamedata.botoptions.ActiveFollowDistance / vxx));

                        if (ratio == 0)
                            return;//TODO - will this fix the weird running away shit?

                        vx = Util.Float_Cap2(vx * ratio);
                        vy = Util.Float_Cap2(vy * ratio);
                        vz = Util.Float_Cap2(vz * ratio);

                        vx += Globals.gamedata.my_char.X;
                        vy += Globals.gamedata.my_char.Y;
                        vz += Globals.gamedata.my_char.Z;

                        //Globals.l2net_home.Add_Text("Sending moveto packet", Globals.Cyan, TextType.BOT);
                        ServerPackets.MoveToPacket(Util.Float_Int32(vx), Util.Float_Int32(vy), Util.Float_Int32(vz));

                        //TODO - should we set break to top true since we are moving?
                    }

                    return;
                }
            }//unlock
            catch
            {
                Globals.l2net_home.Add_Error("crash: Active Follow");
            }
        }

        public static bool was_smart_moving
        {
            get
            {
                return _was_smart_moving;
            }
            set
            {
                if (_was_smart_moving && !value)
                {
                    ScriptEngine.is_Moving = false;
                }

                _was_smart_moving = value;
            }
        }

        private void RestBelow()
        {
            Globals.gamedata.my_char.isAttacking = false;
            Globals.RestBelowLock.EnterReadLock();
            try
            {
                if (((Globals.gamedata.my_char.Cur_HP > Globals.gamedata.botoptions.RestUntilHealth) && (Globals.gamedata.botoptions.RestBelowHP == 1)) || ((Globals.gamedata.my_char.Cur_MP > Globals.gamedata.botoptions.RestUntilMana) && (Globals.gamedata.botoptions.RestBelowMP == 1)))
                {
                    Globals.l2net_home.Add_Error("Rest until value lower than rest below");
                }
                else
                {
                    if (Globals.gamedata.my_char.isSitting == 1)
                    {
                        //Sit
                        //Globals.l2net_home.Add_Text("Debug: HP or MP low, Sitting down", Globals.Green, TextType.BOT);
                        //ServerPackets.Use_Action_Parse((uint)PClientAction.SitStand, Globals.gamedata.Control, false); //For some reason l2net gets real slow when using this here
                        System.Threading.Thread.Sleep(500); //Need a small break before sending sit/stand command or it won't react sometimes
                        SitStandInternal();

                    }

                    //while (((Globals.gamedata.my_char.Cur_HP < Globals.gamedata.my_char.Max_HP) && (Globals.gamedata.botoptions.RestBelowHP == 1))||((Globals.gamedata.my_char.Cur_MP < Globals.gamedata.my_char.Max_MP) && (Globals.gamedata.botoptions.RestBelowMP == 1)))
                    while (((Globals.gamedata.my_char.Cur_HP < Globals.gamedata.botoptions.RestUntilHealth) && (Globals.gamedata.botoptions.RestBelowHP == 1)) || ((Globals.gamedata.my_char.Cur_MP < Globals.gamedata.botoptions.RestUntilMana) && (Globals.gamedata.botoptions.RestBelowMP == 1)))
                    {
                        if (Script_Ops.COUNT("NPC_TARGETME") != 0) //Looks like a mob aggroed, go back in combat
                        {
                            break;
                        }
                        else
                        {
                            System.Threading.Thread.Sleep(1000);
                        }

                    }
                    if (Globals.gamedata.my_char.isSitting == 0)
                    {
                        //Stand
                        //Globals.l2net_home.Add_Text("Debug: Finished resting, standing up", Globals.Green, TextType.BOT);
                        //Globals.l2net_home.Add_Debug("HP full, standing up");
                        //ServerPackets.Use_Action_Parse((uint)PClientAction.SitStand, Globals.gamedata.Control, false);
                        SitStandInternal();
                        System.Threading.Thread.Sleep(500);
                    }
                }
                breaktotop = true;
            }
            catch
            {
                Globals.l2net_home.Add_Error("crash: RestBelow");
            }
            finally
            {
                Globals.RestBelowLock.ExitReadLock();
            }
        }


        /* Stuck test */
        private void StuckCheckInternal()
        {
            //Globals.l2net_home.Add_Text("I'm not attacking something (stuckcheck)", Globals.Green, TextType.BOT);
            Globals.gamedata.my_char.isAttacking = false; //Need to reset this or it will bug.
            float vx, vy, vz;
            float newX, newY, newZ;
            float tx = 0, ty = 0, tz = 0;
            float rx = 0, ry = 0, rz = 0, rx1 = 0, rx2 = 0, ry1 = 0, ry2 = 0;
            float dist;
            //Globals.l2net_home.Add_Text("Debug: Char is stuck", Globals.Green, TextType.BOT);



            /* Turn around 180 degrees */
            double anglerad = 180;
            anglerad = DegreeToRadian(anglerad);

            /* Calculate distance to target */
            Globals.NPCLock.EnterReadLock();
            try
            {
                NPCInfo npc = Util.GetNPC(Globals.gamedata.my_char.TargetID);
                if (npc != null)
                {
                    tx = npc.X;
                    ty = npc.Y;
                    tz = npc.Z;
                }
            }
            catch
            {
                //meh
            }
            Globals.NPCLock.ExitReadLock();
            vx = tx - Globals.gamedata.my_char.X;
            vy = ty - Globals.gamedata.my_char.Y;
            vz = tz - Globals.gamedata.my_char.Z;
            dist = System.Convert.ToSingle(Math.Sqrt(vx * vx + vy * vy + vz * vz));

            //Old Direction
            if (dist > 0)
            {
                vx = vx / dist;
                vy = vy / dist;
                vz = vz / dist;
            }

            /* Rotate char*/
            rz = vz; //ROTATED.Z = "#d<&DIRECTION.Z&>"

            rx1 = (float)Math.Cos(anglerad); //ENGINEX.GEO.COS ROTATED_X1 1 ANGLE_RAD
            rx1 = rx1 * vx; //ROTATED_X1 = ROTATED_X1 * DIRECTION.X
            rx2 = (float)Math.Sin(anglerad); //ENGINEX.GEO.SIN ROTATED_X2 1 ANGLE_RAD
            rx2 = rx2 * vy; //ROTATED_X2 = ROTATED_X2 * DIRECTION.Y
            rx = rx1 - rx2; //ROTATED.X = ROTATED_X1 - ROTATED_X2

            ry1 = (float)Math.Cos(anglerad); //ENGINEX.GEO.COS ROTATED_Y1 1 ANGLE_RAD
            ry1 = ry1 * vy; //ROTATED_Y1 = ROTATED_Y1 * DIRECTION.Y
            ry2 = (float)Math.Sin(anglerad); //ENGINEX.GEO.SIN ROTATED_Y2 1 ANGLE_RAD
            ry2 = ry2 * vx; //ROTATED_Y2 = ROTATED_Y2 * DIRECTION.X
            ry = ry1 + ry2; //ROTATED.Y = ROTATED_Y1 + ROTATED_Y2

            /* Calculate new pos */
            newX = rx * dist;
            newY = ry * dist;
            newZ = rz * dist;
            newX = newX + Globals.gamedata.my_char.X;
            newY = newY + Globals.gamedata.my_char.Y;
            newZ = newZ + Globals.gamedata.my_char.Z;

            //Globals.l2net_home.Add_Text("Debug: Current position. X: " + Globals.gamedata.my_char.X + " Y: " + Globals.gamedata.my_char.Y + " Z: " + Globals.gamedata.my_char.Z, Globals.Green, TextType.BOT);
            //Globals.l2net_home.Add_Text("Debug: New position. X: " + newX + " Y: " + newY + " Z: " + newZ, Globals.Green, TextType.BOT);

            /* Move to new pos */
            //Globals.l2net_home.Add_Text("Sending moveto packet", Globals.Cyan, TextType.BOT);
            ServerPackets.MoveToPacket(Util.Float_Int32(newX), Util.Float_Int32(newY), Util.Float_Int32(newZ));
            /* Sleep while char moves */
            System.Threading.Thread.Sleep(1200);


            /* Turn around 90 degrees */
            anglerad = 90;
            anglerad = DegreeToRadian(anglerad);

            /* Calculate distance to target */
            Globals.NPCLock.EnterReadLock();
            try
            {
                NPCInfo npc = Util.GetNPC(Globals.gamedata.my_char.TargetID);
                if (npc != null)
                {
                    tx = npc.X;
                    ty = npc.Y;
                    tz = npc.Z;
                }
            }
            catch
            {
                //meh
            }
            Globals.NPCLock.ExitReadLock();
            vx = tx - Globals.gamedata.my_char.X;
            vy = ty - Globals.gamedata.my_char.Y;
            vz = tz - Globals.gamedata.my_char.Z;
            dist = System.Convert.ToSingle(Math.Sqrt(vx * vx + vy * vy + vz * vz));

            //Old Direction
            if (dist > 0)
            {
                vx = vx / dist;
                vy = vy / dist;
                vz = vz / dist;
            }

            /* Rotate char*/
            rz = vz; //ROTATED.Z = "#d<&DIRECTION.Z&>"

            rx1 = (float)Math.Cos(anglerad); //ENGINEX.GEO.COS ROTATED_X1 1 ANGLE_RAD
            rx1 = rx1 * vx; //ROTATED_X1 = ROTATED_X1 * DIRECTION.X
            rx2 = (float)Math.Sin(anglerad); //ENGINEX.GEO.SIN ROTATED_X2 1 ANGLE_RAD
            rx2 = rx2 * vy; //ROTATED_X2 = ROTATED_X2 * DIRECTION.Y
            rx = rx1 - rx2; //ROTATED.X = ROTATED_X1 - ROTATED_X2

            ry1 = (float)Math.Cos(anglerad); //ENGINEX.GEO.COS ROTATED_Y1 1 ANGLE_RAD
            ry1 = ry1 * vy; //ROTATED_Y1 = ROTATED_Y1 * DIRECTION.Y
            ry2 = (float)Math.Sin(anglerad); //ENGINEX.GEO.SIN ROTATED_Y2 1 ANGLE_RAD
            ry2 = ry2 * vx; //ROTATED_Y2 = ROTATED_Y2 * DIRECTION.X
            ry = ry1 + ry2; //ROTATED.Y = ROTATED_Y1 + ROTATED_Y2

            /* Calculate new pos */
            newX = rx * dist;
            newY = ry * dist;
            newZ = rz * dist;
            newX = newX + Globals.gamedata.my_char.X;
            newY = newY + Globals.gamedata.my_char.Y;
            newZ = newZ + Globals.gamedata.my_char.Z;

            //Globals.l2net_home.Add_Text("Debug: Current position. X: " + Globals.gamedata.my_char.X + " Y: " + Globals.gamedata.my_char.Y + " Z: " + Globals.gamedata.my_char.Z, Globals.Green, TextType.BOT);
            //Globals.l2net_home.Add_Text("Debug: New position. X: " + newX + " Y: " + newY + " Z: " + newZ, Globals.Green, TextType.BOT);

            /* Move to new pos */
            //Globals.l2net_home.Add_Text("Sending moveto packet", Globals.Cyan, TextType.BOT);
            ServerPackets.MoveToPacket(Util.Float_Int32(newX), Util.Float_Int32(newY), Util.Float_Int32(newZ));
            /* Sleep while char moves */
            System.Threading.Thread.Sleep(1200);

            breaktotop = true;
        }

        private void SitStandInternal()
        {
            ByteBuffer bbuff = new ByteBuffer(10);

            bbuff.WriteByte((byte)PClient.RequestActionUse);
            bbuff.WriteUInt32(0);
            bbuff.WriteUInt32(0);
            bbuff.WriteByte(0);
            Globals.gamedata.SendToGameServer(bbuff);
        }

        private void GetTargetInfo()
        {
            Globals.NPCLock.EnterReadLock();
            try
            {
                NPCInfo npc = Util.GetNPC(Globals.gamedata.my_char.TargetID);
                if (npc != null)
                {
                    target_hp = npc.Cur_HP;
                    target_max_hp = npc.Max_HP;
                    NPCID = npc.ID;
                }
            }
            catch
            {
                //meh
            }
            Globals.NPCLock.ExitReadLock();
        }

        private void BlacklistNPC(uint id)
        {
            BlacklistNPC(id, false);
        }

        private void BlacklistNPC(uint id, bool showmsg)
        {
            Util.IgnoreNPC(id, true);
            if (showmsg)
            {
                Globals.l2net_home.Add_Text("NPC with objid: " + id + " blacklisted", Globals.Green, TextType.BOT);
            }

            System.Threading.Thread.Sleep(200);
        }

        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        private double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }

        private void AutoSweep()
        {
            ServerPackets.Try_Use_Skill(Globals.Skill_SWEEP, false, false);
        }
    }//end of class
}
