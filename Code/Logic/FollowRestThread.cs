using System;
using System.Collections;

namespace L2_login
{
    public class FollowRestThread
    {
        public System.Threading.Thread followrestthread;


        private static bool breaktotop;



        /*private float target_hp = 0;
        private float target_max_hp = 0;
        private uint NPCID = 0;*/

        public FollowRestThread()
        {
            followrestthread = new System.Threading.Thread(new System.Threading.ThreadStart(FollowRest));

            followrestthread.IsBackground = true;
        }

        private void FollowRest()
        {

            ChatterBotFactory factory = new ChatterBotFactory();

            ChatterBot CleverBot = factory.Create(ChatterBotType.CLEVERBOT);
            ChatterBotSession ChatBotSession = CleverBot.CreateSession();
            string s;
            string a;
            string b;
            string from;
            int marker;
          
            while (Globals.gamedata.running)
            {
                breaktotop = false;
                //moved sleep to the top for when breaking to top
                System.Threading.Thread.Sleep(Globals.SLEEP_FollowRestThread); //500, this thread don't need to be very responsive.


                //check if botting is on, and we are in game by having sent the EW packet.
                if (Globals.gamedata.BOTTING && Globals.enterworld_sent)
                {
                    if (!breaktotop && (Script_Ops.COUNT("NPC_TARGETME") == 0 && Script_Ops.COUNT("NPC_PARTYTARGETED") == 0) && (Globals.gamedata.botoptions.FollowRest == 1) && (Globals.gamedata.botoptions.FollowRestID != 0))
                    {
                        FollowRestInternal();
                    }

                    if (!breaktotop && Globals.gamedata.botoptions.SendParty == 1)
                    {
                        AutoInvitePartyMembers();
                    }
                    if (!breaktotop && Globals.gamedata.botoptions.LeavePartyOnLeader == 1)
                    {
                        CheckPartyLeader();
                    }
                }


                if (Globals.gamedata.LocalChatQueue.Count > 0)
                {
                    if (Globals.gamedata.autoreply)
                    {
                        s = (Globals.gamedata.LocalChatQueue.Dequeue()).ToString();
                        marker = s.IndexOf(':');
                        try
                        {
                            from = s.Substring(0, marker);
                        }
                        catch
                        {
                            from = "";
                        }

                        if (from.CompareTo(Globals.gamedata.my_char.Name) != 0)
                        {
                            s = s.Remove(0, marker + 2); //string message = from + ": " + text;

                            a = ChatBotSession.Think(s);

                            b = a.ToUpperInvariant();


                            if ((!String.IsNullOrEmpty(a))&& !b.Contains("CLEVERBOT"))
                            {
                                ServerPackets.Send_Text(0, ChatBotSession.Think(s));
                            }
                        }
                    }
                    else
                    {
                        //clear queue in case it wasn't empty when toggle autoreply button was clicked
                        Globals.gamedata.LocalChatQueue.Dequeue();
                    }
                }

                if (Globals.gamedata.PrivateMsgQueue.Count > 0)
                {
                    try
                    {
                        if (Globals.gamedata.autoreplyPM)
                        {
                            s = (Globals.gamedata.PrivateMsgQueue.Dequeue()).ToString();
                            marker = s.IndexOf(':');
                            try
                            {
                                from = s.Substring(0, marker);
                            }
                            catch
                            {
                                from = "";
                            }
                            if (from.CompareTo(Globals.gamedata.my_char.Name) != 0)
                            {
                                s = s.Remove(0, marker + 2);

                                a = ChatBotSession.Think(s);

                                b = a.ToUpperInvariant();

                                if ((!String.IsNullOrEmpty(a))&& !b.Contains("CLEVERBOT"))
                                {
                                    ServerPackets.Send_Text(2, from + " " + a);
                                }

                            }
                        }
                        else
                        {
                            //clear queue in case it wasn't empty when toggle autoreply button was clicked
                            Globals.gamedata.PrivateMsgQueue.Dequeue();
                        }
                    }
                    catch
                    {

                    }
                }
            }
        }

        private void FollowRestInternal()
        {
            CharInfo player = null;

            Globals.PlayerLock.EnterReadLock();
            try
            {
                player = Util.GetChar(Globals.gamedata.botoptions.FollowRestID);
            }
            finally
            {
                Globals.PlayerLock.ExitReadLock();
            }
            //Globals.l2net_home.Add_Text("Debug: Follow rest activated on: " + player.Name, Globals.Green, TextType.BOT);
            try
            {
                if (player != null)
                {
                    if ((player.isSitting == 0) && (Globals.gamedata.my_char.isSitting == 1)) //0 = Player is sitting, 1 = my char is standing
                    {
                        //Globals.l2net_home.Add_Text("Debug: Status changed, " + player.Name + " is sitting", Globals.Green, TextType.BOT);
                        System.Threading.Thread.Sleep(1000);
                        SitStandInternal();
                        System.Threading.Thread.Sleep(3000); //Give the char time to sit down
                        breaktotop = true;
                    }
                    else if ((player.isSitting == 1) && (Globals.gamedata.my_char.isSitting == 0)) //Stand up again
                    {
                        //Globals.l2net_home.Add_Text("Debug: Status changed, " + player.Name + " is standing up again", Globals.Green, TextType.BOT);
                        System.Threading.Thread.Sleep(1000); //Check again in 1000ms
                        SitStandInternal();
                        System.Threading.Thread.Sleep(3000); //Give the char time to stand up again
                        breaktotop = true;
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(1000); //Long sleep to keep thread from taking too much system resources
                    }
                }
            }
            catch
            {
                Globals.l2net_home.Add_Error("crash: FollowRest");
            }

        }

        private void SitStandInternal()
        {
            ServerPackets.Use_Action_Parse((int)PClientAction.SitStand, false, false);
        }
        private void CheckPartyLeader()
        {
            try
            {
                if ((Globals.gamedata.PartyLeader == Globals.gamedata.my_char.ID) && (Globals.gamedata.PartyCount > 0))
                {
                    //fuck this, I'm no leader!
                    Globals.l2net_home.Add_Text("Leaving party, cannot be leader due to bot options");
                    ServerPackets.Command_Leave();
                    breaktotop = true;
                }
            }
            catch
            {
                Globals.l2net_home.Add_Error("crash: Leaving party failed");
            }
        }

        private void AutoInvitePartyMembers()
        {
            String s = Globals.gamedata.botoptions.SendPartyNames;
            String[] PartyNames = s.Split(',', ';');
            String PN = "";
            int found;

            try
            {
                Globals.PartyLock.EnterReadLock();
                if ((Globals.gamedata.PartyCount < 7) && (Globals.gamedata.PartyLeader == Globals.gamedata.my_char.ID || Globals.gamedata.PartyCount <= 0))
                {
                    //cycle through all given text
                    for (int i = 0; i < PartyNames.Length; i++)
                    {
                        PN = PartyNames[i];
                        found = 0;

                        //loop through each member
                        foreach (PartyMember pl in Globals.gamedata.PartyMembers.Values)
                        {
                            //Globals.l2net_home.Add_Text("pl.name:  " + pl.Name + " PN: " + PN);
                            if (String.Equals(pl.Name, PN, StringComparison.OrdinalIgnoreCase))
                            {
                                found = 1;
                            }
                        }

                        //if member is not in our party, invite him.
                        if (found == 0 && PN != "" && PN != null)
                        {
                            //Globals.l2net_home.Add_Text("Inviting " + PN + " to party.");
                            ServerPackets.Command_Invite(PN);
                            System.Threading.Thread.Sleep(11500); //10000 = timeout for invite

                        }
                    }
                    breaktotop = true;
                }
            }
            catch
            {
                Globals.l2net_home.Add_Error("crash: Invite to party, check your settings.");
            }
            finally
            {
                Globals.PartyLock.ExitReadLock();
            }
        }
    }
}