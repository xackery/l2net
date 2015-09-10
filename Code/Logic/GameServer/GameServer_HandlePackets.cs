using System.Text;

namespace L2_login
{
    public partial class GameServer
    {
        private static void HandlePackets()
        {
            ByteBuffer buffe;
            uint b0 = 0, b1 = 0;//byte
            string last_p = "";//, last_p2 = "";

            while (Globals.gamedata.GetCount_DataToBot() > 0)
            {
                try
                {
                    //buffe = null;

                    Globals.GameReadQueueLock.EnterWriteLock();
                    try
                    {
                        buffe = (ByteBuffer)Globals.gamedata.gamereadqueue.Dequeue();
                    }
                    catch (System.Exception e)
                    {
                        Globals.l2net_home.Add_Error("Packet Error Reading Queue : " + e.Message);
                        break;
                    }
                    finally
                    {
                        Globals.GameReadQueueLock.ExitWriteLock();
                    }

                    //buffe contains unencoded data
                    b0 = buffe.ReadByte();
                    //last_p2 = last_p;
                    last_p = b0.ToString("X2");

                    //do we have an event for this packet?
                    if (ScriptEngine.ServerPacketsContainsKey((int)b0))
                    {
                        ScriptEvent sc_ev = new ScriptEvent();
                        sc_ev.Type = EventType.ServerPacket;
                        sc_ev.Type2 = (int)b0;
                        sc_ev.Variables.Add(new ScriptVariable(buffe, "PACKET", Var_Types.BYTEBUFFER, Var_State.PUBLIC));
                        sc_ev.Variables.Add(new ScriptVariable(System.DateTime.Now.Ticks, "TIMESTAMP", Var_Types.INT, Var_State.PUBLIC));
                        ScriptEngine.SendToEventQueue(sc_ev);
                    }

                    switch ((PServer)b0)
                    {
                        case PServer.MTL:
                            ClientPackets.MoveToLocation(buffe);
                            break;
                        case PServer.NS:
                            ClientPackets.NPCSay(buffe);
                            break;
                        case PServer.CI:
                            ClientPackets.CharInfo(buffe);
                            break;
                        case PServer.RelationChanged:
                            ClientPackets.RelationChanged(buffe);
                            break;
                        case PServer.UI:
                            ClientPackets.UserInfo(buffe);
                            break;
                        case PServer.Attack:
                            ClientPackets.Attack_Packet(buffe);
                            break;
                        case PServer.Die:
                            ClientPackets.Die_Packet(buffe);
                            break;
                        case PServer.Revive:
                            ClientPackets.Revive(buffe.ReadUInt32());
                            break;
                        case PServer.ChangeMoveType:
                            ClientPackets.ChangeMoveType(buffe);
                            break;
                        case PServer.ChangeWaitType:
                            ClientPackets.ChangeWaitType(buffe);
                            break;
                        case PServer.AttackDeadTarget:
                            ClientPackets.AttackCanceled_Packet(buffe);
                            break;
                        case PServer.SpawnItem://add item (to ground)
                            ClientPackets.AddItem(buffe);
                            break;
                        case PServer.DropItem:
                            ClientPackets.ItemDrop(buffe);
                            break;
                        case PServer.GetItem:
                            ClientPackets.Get_Item(buffe);
                            break;
                        case PServer.StatusUpdate:
                            ClientPackets.StatusUpdate(buffe);
                            break;
                        case PServer.NpcHtmlMessage:
                            NPC_Chat.Npc_Chat(buffe);
                            break;
                        case PServer.BuyList: //Buylist
                            ClientPackets.BuyList(buffe);
                            break;
                        case PServer.DeleteObject:
                            ClientPackets.DeleteItem(buffe);
                            break;
                        case PServer.CharacterSelectionInfo:
                            Globals.login_window.CharList(buffe);
                            //this is the list of chars
                            //not handled by ig

                            //need to handle this packet here and not on the oog login
                            //on ig, the list will just show up, then disappear on 0x15 CharSelected so its all good
                            break;
                        case PServer.CharSelected:
                            ClientPackets.CharSelected(buffe);
                            break;
                        case PServer.NpcInfo:
                            ClientPackets.NPCInfo(buffe);
                            break;
                        case PServer.ServerObjectInfo:
                            ClientPackets.ServerObjectInfo(buffe);
                            break;
                        case PServer.StaticObject:
                            ClientPackets.StaticObjectInfo(buffe);
                            break;
                        case PServer.ItemList:
                            ClientPackets.ItemList(buffe);
                            break;
                        case PServer.SunRise:
                            Globals.l2net_home.Add_Text("[The sun has just risen]", Globals.Gray, TextType.SYSTEM);
                            break;
                        case PServer.SunSet:
                            Globals.l2net_home.Add_Text("[The sun has just set]", Globals.Gray, TextType.SYSTEM);
                            break;
                        case PServer.TradeStart:
                            break;
                        case PServer.TradeDone:
                            try
                            {
                                Globals.tradewindow.Close();
                            }
                            catch
                            {
                                //trade window is not opened.
                            }
                            break;
                        case PServer.ActionFailed:
                            //l2j is ghei and throws this packet at us a lot
                            // (Freya) Retail also causes this packet to be sent before any Attack or skill use that requires CTRL
                            break;
                        case PServer.InventoryUpdate:
                            AddInfo.Inventory_Update(buffe);
                            break;
                        case PServer.TeleportToLocation:
                            ClientPackets.Teleport(buffe);
                            break;
                        case PServer.MyTargetSelected:
                            ClientPackets.MyTargetSelected(buffe);
                            break;
                        case PServer.TargetSelected:
                            ClientPackets.TargetSelected(buffe, true);
                            break;
                        case PServer.TargetUnselected:
                            ClientPackets.TargetSelected(buffe, false);
                            break;
                        case PServer.AutoAttackStart:
                            //4bytes target id
                            ClientPackets.EnterCombat(buffe, true);
                            break;
                        case PServer.AutoAttackStop:
                            //4bytes target id
                            ClientPackets.EnterCombat(buffe, false);
                            break;
                        case PServer.SocialAction:
                            ClientPackets.Social_Action(buffe);
                            break;
                        case PServer.AskJoinPledge:
                            ClientPackets.RequestJoinClan(buffe);
                            break;
                        case PServer.AskJoinParty:
                            ClientPackets.RequestJoinParty(buffe);
                            break;
                        case PServer.TradeRequest:
                            ClientPackets.TradeRequest(buffe);
                            break;
                        case PServer.ShortCutRegister:
                            ClientPackets.Load_Shortcut(buffe);
                            break;
                        case PServer.ShortCutInit:
                            ClientPackets.Load_Shortcuts(buffe);
                            break;
                        case PServer.StopMove:
                            ClientPackets.StopMove(buffe);
                            break;
                        case PServer.MagicSkillUser:
                            ClientPackets.MagicSkillUser(buffe);
                            break;
                        case PServer.MagicSkillCancelId:
                            //4bytes - ID of caster that canceled
                            ClientPackets.MagicSkillCancel(buffe);
                            break;
                        case PServer.CreatureSay:
                            ClientPackets.OtherMessage(buffe);
                            break;
                        case PServer.PartySmallWindowAll:
                            if (Globals.gamedata.yesno_state == 1)
                                Globals.l2net_home.Hide_YesNo();
                            ClientPackets.Load_PartyInfo(buffe);
                            AddInfo.Set_PartyVisible();
                            AddInfo.Set_PartyInfo();
                            break;
                        case PServer.PartySmallWindowAdd:
                            if (Globals.gamedata.yesno_state == 1)
                                Globals.l2net_home.Hide_YesNo();
                            ClientPackets.Add_PartyInfo(buffe);
                            AddInfo.Set_PartyVisible();
                            AddInfo.Set_PartyInfo();
                            break;
                        case PServer.PartySmallWindowDeleteAll:
                            ClientPackets.Delete_Party();
                            AddInfo.Set_PartyVisible();
                            AddInfo.Set_PartyInfo();
                            break;
                        case PServer.PartySmallWindowDelete:
                            ClientPackets.Delete_PartyInfo(buffe);
                            AddInfo.Set_PartyVisible();
                            AddInfo.Set_PartyInfo();
                            break;
                        case PServer.PartySmallWindowUpdate:
                            ClientPackets.Update_PartyInfo(buffe);
                            AddInfo.Set_PartyInfo();
                            break;
                        case PServer.PledgeShowMemberListAll:
                            ClientPackets.PledgeShowMemberListAll(buffe);
                            break;
                        case PServer.PledgeShowMemberListUpdate:
                            ClientPackets.PledgeShowMemberListUpdate(buffe);
                            break;
                        case PServer.PledgeShowMemberListAdd:
                            //changes the clan status of one char... need to update/add as needed
                            ClientPackets.PledgeShowMemberListAdd(buffe);
                            break;
                        case PServer.PledgeShowMemberListDelete:
                            ClientPackets.PledgeShowMemberListDelete(buffe);
                            break;
                        case PServer.PledgeShowInfoUpdate:
                            ClientPackets.PledgeShowInfoUpdate(buffe);
                            break;
                        case PServer.PledgeShowMemberListDeleteAll:
                            ClientPackets.PledgeShowMemberListDeleteAll(buffe);
                            break;
                        case PServer.AbnormalStatusUpdate:
                            ClientPackets.LoadBuffList(buffe);
                            break;
                        case PServer.PartySpelled:
                            ClientPackets.PartySpelled(buffe);
                            break;
                        case PServer.ShortBuffStatusUpdate:
                            ClientPackets.UpdateBuff(buffe);
                            break;
                        case PServer.SkillList:
                            ClientPackets.SkillList(buffe);
                            break;
                        case PServer.SkillCoolTime:
                            ClientPackets.SkillCoolTime(buffe);
                            break;
                        case PServer.RestartResponse:
                            ClientPackets.RestartResponse(buffe);
                            break;
                        case PServer.MoveToPawn:
                            ClientPackets.MoveToPawn(buffe);
                            break;
                        case PServer.ValidateLocation:
                            ClientPackets.Validate_Location(buffe, false);
                            //int id
                            //int rotation
                            break;
                        case PServer.SystemMessage:
                            ClientPackets.SystemMessage(buffe);
                            break;
                        case PServer.PledgeCrest:
                            ClientPackets.PledgeCrest(buffe);
                            break;
                        case PServer.PledgeInfo:
                            ClientPackets.PledgeInfo(buffe);
                            break;
                        case PServer.ValidateLocationInVehicle:
                            ClientPackets.Validate_Location(buffe, true);
                            break;
                        case PServer.MagicSkillLaunched:
                            ClientPackets.MagicSkillLaunched(buffe);
                            //Validate_Location(buffe,false);
                            break;
                        case PServer.FriendAddRequest:
                            ClientPackets.RequestJoinFriend(buffe);
                            break;
                        case PServer.LogOutOk:
                            //ok time to end
                            Globals.gamedata.running = false;
                            Globals.l2net_home.KillEverything();
                            break;
                        case PServer.PartyMemberPosition:
                            ClientPackets.Set_PartyLocations(buffe);
                            break;
                        case PServer.AskJoinAlly:
                            ClientPackets.RequestJoinAlly(buffe);
                            break;
                        case PServer.AllianceCrest:
                            ClientPackets.AllyCrest(buffe);
                            break;
                        case PServer.Earthquake:
                            //x,y,z,power,duration
                            Globals.l2net_home.Add_Text("[Earthquake at X:" + buffe.ReadInt32().ToString() + " Y:" + buffe.ReadInt32().ToString() + " Z:" + buffe.ReadInt32().ToString() + " Of Power:" + buffe.ReadInt32().ToString() + " For:" + buffe.ReadInt32().ToString() + "]", Globals.Gray, TextType.SYSTEM);
                            break;
                        case PServer.PledgeStatusChanged:
                            ClientPackets.ClanStatusChanged(buffe);
                            break;
                        case PServer.Dice:
                            ClientPackets.Dice(buffe);
                            break;
                        case PServer.HennaInfo:
                            ClientPackets.Set_Henna_Info(buffe);
                            break;
                        case PServer.ConfirmDlg:
                            ClientPackets.RequestReply(buffe);
                            break;
                        case PServer.SSQInfo://SignSky:
                            ClientPackets.SevenSignSky(buffe);
                            break;
                        case PServer.GameGuardQuery:
                            ClientPackets.GameGuardReply(buffe);
                            break;
                        case PServer.L2FriendSay:
                            ClientPackets.Get_Friend_Message(buffe);
                            break;
                        case PServer.EtcStatusUpdate:
                            ClientPackets.EtcStatusUpdate(buffe);
                            break;
                        case PServer.PetStatusShow:
                            ClientPackets.PetStatusShow(buffe);
                            break;
                        case PServer.PetInfo:
                            ClientPackets.PetInfo(buffe);
                            break;
                        case PServer.PetStatusUpdate:
                            ClientPackets.PetStatusUpdate(buffe);
                            break;
                        case PServer.PetDelete:
                            ClientPackets.PetDelete(buffe);
                            break;
                        case PServer.PetItemList:
                            ClientPackets.PetItemList(buffe);
                            if (Globals.petwindow != null)
                            {
                                Globals.petwindow.fill_pet_inventory();
                            }
                            break;
                        case PServer.PetInventoryUpdate:
                            AddInfo.PetInventory_Update(buffe);
                            if (Globals.petwindow != null)
                            {
                                Globals.petwindow.fill_pet_inventory();
                            }
                            break;
                        case PServer.NetPing:
                            ClientPackets.NetPing(buffe);
                            break;



                        case PServer.EXPacket:
                            b1 = buffe.ReadUInt16();
                            //buffe.ReadByte();

                            last_p = last_p + " " + b1.ToString("X2");

                            //do we have an event for this packet?
                            if (ScriptEngine.ServerPacketsEXContainsKey((int)b1))
                            {
                                ScriptEvent sc_ev = new ScriptEvent();
                                sc_ev.Type = EventType.ServerPacketEX;
                                sc_ev.Type2 = (int)b1;
                                sc_ev.Variables.Add(new ScriptVariable(buffe, "PACKET", Var_Types.BYTEBUFFER, Var_State.PUBLIC));
                                sc_ev.Variables.Add(new ScriptVariable(System.DateTime.Now.Ticks, "TIMESTAMP", Var_Types.INT, Var_State.PUBLIC));
                                ScriptEngine.SendToEventQueue(sc_ev);
                            }
                            //Globals.l2net_home.Add_Text(last_p);
                            switch ((PServerEX)b1)
                            {
                                /*case PServerEX.ExPledgeCrestLarge:
                                    //https://www.l2jserver.com/trac/browser/trunk/L2_GameServer_It/java/net/sf/l2j/gameserver/serverpackets/ExPledgeCrestLarge.java
                                    break;*/
                                case PServerEX.ExMailArrived:
                                    Globals.l2net_home.Add_Text("[Mail has arrived]");
                                    VoicePlayer.PlaySound(8);
                                    break;
                                case PServerEX.ExSetCompassZoneCode:
                                    ClientPackets.ExSetCompassZoneCode(buffe);
                                    break;
                                case PServerEX.ExShowScreenMessage:
                                    ClientPackets.ExShowScreenMessage(buffe);
                                    break;
                                case PServerEX.ExQuestHTML:
                                    if (Globals.gamedata.Chron >= Chronicle.CT2_3)
                                    {
                                        NPC_Chat.Npc_Chat(buffe);
                                    }
                                    break;
                                case PServerEX.ExSendUIEventPacket:
                                    if (Globals.gamedata.Chron >= Chronicle.CT3_2)
                                    {
                                        NPC_Chat.Npc_Chat(buffe);
                                    }
                                    break;
                                case PServerEX.ExQuestItemList:
                                    if (Globals.gamedata.Chron >= Chronicle.CT2_5)
                                    {
                                        ClientPackets.ExQuestItemList(buffe);
                                    }
                                    break;
                                case PServerEX.ExVitalityUpdate:
                                    if (Globals.gamedata.Chron >= Chronicle.CT2_1)
                                    {
                                        ClientPackets.ExVitalityUpdate(buffe);
                                    }
                                    break;
                                case PServerEX.ExShowReceivedPostList:
                                    ClientPackets.Load_ReceivedMails(buffe);
                                    break;
                                case PServerEX.ExShowSecurityPinWindow:
                                    Globals.gamedata.SecurityPinWindow = true;
                                    if ((!Globals.OOG) && (!System.String.IsNullOrEmpty(Globals.SecurityPin)) && (!Globals.gamedata.SecurityPinOldClient))
                                    {
                                        System.Threading.Thread.Sleep(3000);
                                        ServerPackets.SecurityPin();
                                        Globals.gamedata.SecurityPinSent = true;
                                    }
                                    break;
                                case PServerEX.ExSecurityPinCorrect:
                                    Globals.gamedata.SecurityPinOk = true;
                                    break;
                                case PServerEX.ExAcquireSkillInfo:
                                    ClientPackets.ExAcquireSkillInfo(buffe);
                                    break;
                                case PServerEX.ExNewSkillToLearnByLevelup:
                                    //Glory days
                                    ClientPackets.ExAcquireSkillInfo(buffe);
                                    break;
                                case PServerEX.ExtargetBuffs:
                                    ClientPackets.Extargetbuffs(buffe);
                                    break;
                                case PServerEX.ExPartyPetWindowAdd:
                                    ClientPackets.ExPartyPetWindowAdd(buffe);
                                    break;
                                case PServerEX.ExPartyPetWindowDelete:
                                    ClientPackets.ExPartyPetWindowDelete(buffe);
                                    break;
                                case PServerEX.ExNpcInfo:
                                    ClientPackets.ExNPCInfo(buffe);
                                    break;
                                case PServerEX.ExUserinfoItems:
                                    ClientPackets.ExUserInfoItems(buffe);
                                    break;
                                case PServerEX.ExUserinfoStats:
                                    ClientPackets.ExUserInfoStats(buffe);
                                    break;
                                case PServerEX.ExUserInfo:
                                    ClientPackets.EXUserInfo(buffe);
                                    break;
                            }
                            break;
                    }//end of switch on packet type

                }
                catch (System.Exception e)
                {
                    //Globals.l2net_home.Add_Error("Packet Error: " + last_p + " Previous Packet: " + last_p2 + " : " + e.Message);
                    Globals.l2net_home.Add_Error("Packet Error: " + last_p + " :: " + e.Message);
                }
            }//end of loop to handle queue data
        }//end of HandlePackets
    }
}
