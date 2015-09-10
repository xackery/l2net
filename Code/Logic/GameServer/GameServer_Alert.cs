using System.Text;

namespace L2_login
{
    public partial class GameServer
    {
        private static void PlayAlerts()
        {
            try
            {
                bool beep = false;
                bool logout = false;

                //check sounds/logout
                if (Globals.gamedata.alertoptions.beepon_2waywar || Globals.gamedata.alertoptions.logouton_2waywar)
                {
                    if (Check_2WayWar())
                    {
                        if (Globals.gamedata.alertoptions.beepon_2waywar)
                        {
                            beep = true;
                            Globals.l2net_home.Add_Alarm("2 way war in range!");
                        }
                        if (Globals.gamedata.alertoptions.logouton_2waywar)
                        {
                            logout = true;
                            Globals.l2net_home.Add_Logout("2 way war in range!");
                        }
                    }
                }
                if (Globals.gamedata.alertoptions.beepon_1waywar || Globals.gamedata.alertoptions.logouton_1waywar)
                {
                    if (Check_1WayWar())
                    {
                        if (Globals.gamedata.alertoptions.beepon_1waywar)
                        {
                            beep = true;
                            Globals.l2net_home.Add_Alarm("1 way war in range!");
                        }
                        if (Globals.gamedata.alertoptions.logouton_1waywar)
                        {
                            logout = true;
                            Globals.l2net_home.Add_Logout("1 way war in range!");
                        }
                    }
                }
                if (Globals.gamedata.alertoptions.beepon_n1waywar || Globals.gamedata.alertoptions.logouton_n1waywar)
                {
                    if (Check_N1WayWar())
                    {
                        if (Globals.gamedata.alertoptions.beepon_n1waywar)
                        {
                            beep = true;
                            Globals.l2net_home.Add_Alarm("-1 way war in range!");
                        }
                        if (Globals.gamedata.alertoptions.logouton_n1waywar)
                        {
                            logout = true;
                            Globals.l2net_home.Add_Logout("-1 way war in range!");
                        }
                    }
                }

                //check if i need to play sounds
                if (Globals.gamedata.alertoptions.beepon_hp)
                {
                    if (Check_HP())
                    {
                        beep = true;
                        Globals.l2net_home.Add_Alarm("hp too low!");
                    }
                }
                if (Globals.gamedata.alertoptions.beepon_mp)
                {
                    if (Check_MP())
                    {
                        beep = true;
                        Globals.l2net_home.Add_Alarm("mp too low!");
                    }
                }
                if (Globals.gamedata.alertoptions.beepon_cp)
                {
                    if (Check_CP())
                    {
                        beep = true;
                        Globals.l2net_home.Add_Alarm("cp too low!");
                    }
                }
                if (Globals.gamedata.alertoptions.beepon_clan)
                {
                    if (Check_Clan())
                    {
                        beep = true;
                        Globals.l2net_home.Add_Alarm("bad clan in range!");
                    }
                }
                if (Globals.gamedata.alertoptions.beepon_player)
                {
                    if (Check_Player())
                    {
                        beep = true;
                        Globals.l2net_home.Add_Alarm("bad player in range!");
                    }
                }

                //check if I need to log out
                if (Globals.gamedata.alertoptions.logouton_hp)
                {
                    if (Check_HP_Logout())
                    {
                        logout = true;
                        Globals.l2net_home.Add_Logout("hp too low!");
                    }
                }
                if (Globals.gamedata.alertoptions.logouton_mp)
                {
                    if (Check_MP_Logout())
                    {
                        logout = true;
                        Globals.l2net_home.Add_Logout("mp too low!");
                    }
                }
                if (Globals.gamedata.alertoptions.logouton_cp)
                {
                    if (Check_CP_Logout())
                    {
                        logout = true;
                        Globals.l2net_home.Add_Logout("cp too low!");
                    }
                }
                if (Globals.gamedata.alertoptions.logouton_clan)
                {
                    if (Check_Clan_Logout())
                    {
                        logout = true;
                        Globals.l2net_home.Add_Logout("bad clan in range!");
                    }
                }
                if (Globals.gamedata.alertoptions.logouton_player)
                {
                    if (Check_Player_Logout())
                    {
                        logout = true;
                        Globals.l2net_home.Add_Logout("bad player in range!");
                    }
                }

                if (beep)
                {
                    VoicePlayer.PlayAlarm();
                }
                if (logout)
                {
                    Util.Stop_Connections();
                }
            }
            catch
            {
                Globals.l2net_home.Add_Error("crash: Sound Alert Thread");
            }
        }


        private static bool Check_2WayWar()
        {
            Globals.PlayerLock.EnterReadLock();
            try
            {
                foreach (CharInfo player in Globals.gamedata.nearby_chars.Values)
                {
                    if (player.WarState == 2)
                    {
                        return true;
                    }
                }
            }
            finally
            {
                Globals.PlayerLock.ExitReadLock();
            }

            return false;
        }

        private static bool Check_1WayWar()
        {
            Globals.PlayerLock.EnterReadLock();
            try
            {
                foreach (CharInfo player in Globals.gamedata.nearby_chars.Values)
                {
                    if (player.WarState == 1)
                    {
                        return true;
                    }
                }
            }
            finally
            {
                Globals.PlayerLock.ExitReadLock();
            }

            return false;
        }

        private static bool Check_N1WayWar()
        {
            Globals.PlayerLock.EnterReadLock();
            try
            {
                foreach (CharInfo player in Globals.gamedata.nearby_chars.Values)
                {
                    if (player.WarState == -1)
                    {
                        return true;
                    }
                }
            }
            finally
            {
                Globals.PlayerLock.ExitReadLock();
            }

            return false;
        }

        private static bool Check_HP()
        {
            if (Globals.gamedata.my_char.Max_HP * Globals.gamedata.alertoptions.hp_value > Globals.gamedata.my_char.Cur_HP * 100)
            {
                return true;
            }
            return false;
        }

        private static bool Check_MP()
        {
            if (Globals.gamedata.my_char.Max_MP * Globals.gamedata.alertoptions.mp_value > Globals.gamedata.my_char.Cur_MP * 100)
            {
                return true;
            }
            return false;
        }

        private static bool Check_CP()
        {
            if (Globals.gamedata.my_char.Max_CP * Globals.gamedata.alertoptions.cp_value > Globals.gamedata.my_char.Cur_CP * 100)
            {
                return true;
            }
            return false;
        }

        private static bool Check_Clan()
        {
            System.Collections.ArrayList names = Util.GetArray(Globals.gamedata.alertoptions.clan_value);
            System.Collections.ArrayList clan_ids = new System.Collections.ArrayList();

            //first lets convert all our clan names to an arraylist of uint ids
            Globals.ClanListLock.EnterReadLock();
            try
            {
                foreach (Clan_Info clan_info in Globals.clanlist.Values)
                {
                    foreach (string clan_name in names)
                    {
                        if (System.String.Equals(clan_info.ClanName.ToUpperInvariant(), clan_name))
                        {
                            clan_ids.Add(clan_info.ID);
                        }
                    }
                }
            }
            finally
            {
                Globals.ClanListLock.ExitReadLock();
            }

            //now lets look thru every char and see if we can find ppl with those clan ids
            Globals.PlayerLock.EnterReadLock();
            try
            {
                foreach (CharInfo player in Globals.gamedata.nearby_chars.Values)
                {
                    foreach (uint clan_id in clan_ids)
                    {
                        if (player.ClanID == clan_id)
                        {
                            if (Globals.gamedata.alertoptions.beepon_clan_ignoreparty)
                            {
                                Globals.PartyLock.EnterReadLock();
                                try
                                {
                                    //make sure they aren't in are party... or OOP name list
                                    if (!Util.IsPartyMember(player.ID) && !Globals.gamedata.botoptions.OOPIDs.Contains(player.ID))
                                    {
                                        return true;
                                    }
                                }
                                finally
                                {
                                    Globals.PartyLock.ExitReadLock();
                                }
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            finally
            {
                Globals.PlayerLock.ExitReadLock();
            }

            return false;
        }

        private static bool Check_Player()
        {
            System.Collections.ArrayList names = Util.GetArray(Globals.gamedata.alertoptions.player_value);

            Globals.PlayerLock.EnterReadLock();
            try
            {
                foreach (CharInfo player in Globals.gamedata.nearby_chars.Values)
                {
                    if (names.Contains(player.Name.ToUpperInvariant()))
                    {
                        if (Globals.gamedata.alertoptions.beepon_player_ignoreparty)
                        {
                            Globals.PartyLock.EnterReadLock();
                            try
                            {
                                //make sure they aren't in are party... or OOP name list
                                if (!Util.IsPartyMember(player.ID) && !Globals.gamedata.botoptions.OOPIDs.Contains(player.ID))
                                {
                                    return true;
                                }
                            }
                            finally
                            {
                                Globals.PartyLock.ExitReadLock();
                            }
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
            finally
            {
                Globals.PlayerLock.ExitReadLock();
            }

            return false;
        }

        private static bool Check_HP_Logout()
        {
            if (Globals.gamedata.my_char.Max_HP * Globals.gamedata.alertoptions.hp_value_logout > Globals.gamedata.my_char.Cur_HP * 100)
            {
                return true;
            }
            return false;
        }

        private static bool Check_MP_Logout()
        {
            if (Globals.gamedata.my_char.Max_MP * Globals.gamedata.alertoptions.mp_value_logout > Globals.gamedata.my_char.Cur_MP * 100)
            {
                return true;
            }
            return false;
        }

        private static bool Check_CP_Logout()
        {
            if (Globals.gamedata.my_char.Max_CP * Globals.gamedata.alertoptions.cp_value_logout > Globals.gamedata.my_char.Cur_CP * 100)
            {
                return true;
            }
            return false;
        }

        private static bool Check_Clan_Logout()
        {
            System.Collections.ArrayList names = Util.GetArray(Globals.gamedata.alertoptions.clan_value_logout);
            System.Collections.ArrayList clan_ids = new System.Collections.ArrayList();

            //first lets convert all our clan names to an arraylist of uint ids
            Globals.ClanListLock.EnterReadLock();
            try
            {
                foreach (Clan_Info clan_info in Globals.clanlist.Values)
                {
                    foreach (string clan_name in names)
                    {
                        if (System.String.Equals(clan_info.ClanName.ToUpperInvariant(), clan_name))
                        {
                            clan_ids.Add(clan_info.ID);
                        }
                    }
                }
            }
            finally
            {
                Globals.ClanListLock.ExitReadLock();
            }

            //now lets look thru every char and see if we can find ppl with those clan ids
            Globals.PlayerLock.EnterReadLock();
            try
            {
                foreach (CharInfo player in Globals.gamedata.nearby_chars.Values)
                {
                    foreach (uint clan_id in clan_ids)
                    {
                        if (player.ClanID == clan_id)
                        {
                            return true;
                        }
                    }
                }
            }
            finally
            {
                Globals.PlayerLock.ExitReadLock();
            }

            return false;
        }

        private static bool Check_Player_Logout()
        {
            System.Collections.ArrayList names = Util.GetArray(Globals.gamedata.alertoptions.player_value_logout);

            Globals.PlayerLock.EnterReadLock();
            try
            {
                foreach (CharInfo player in Globals.gamedata.nearby_chars.Values)
                {
                    if (names.Contains(player.Name.ToUpperInvariant()))
                    {
                        return true;
                    }
                }
            }
            finally
            {
                Globals.PlayerLock.ExitReadLock();
            }

            return false;
        }
    }
}
