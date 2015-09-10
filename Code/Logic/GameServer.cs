using System;
using System.Collections.Generic;
using System.Text;

namespace L2_login
{
    public partial class GameServer
    {
        public static void Init(string[] args)
        {
            VoicePlayer.Init();
            VoicePlayer.PlaySound(1);

            Globals.PATH = Environment.CurrentDirectory;

            try
            {
                if (Globals.LogWriting)
                {
                    Globals.text_out = new System.IO.StreamWriter(Globals.PATH + "\\Text\\" + System.DateTime.Now.Month.ToString() + "-" + System.DateTime.Now.Day.ToString() + "-" + System.DateTime.Now.Year.ToString() + "_" + System.DateTime.Now.Hour.ToString() + "=" + System.DateTime.Now.Minute.ToString() + "=" + System.DateTime.Now.Second.ToString() + ".txt");
                }
                else
                {
                    Globals.text_out = null;
                }
            }
            catch
            {
                Globals.text_out = null;
                Globals.l2net_home.Add_PopUpError("failed to create text output log file... do you have the datapack?" + Environment.NewLine + "L2.net needs write/create access to the \\Text\\ folder to log chat.");
            }

#if DEBUG
            try
            {
                Globals.gamedataout = new System.IO.StreamWriter(Globals.PATH + "\\Logs\\from_gamelog.txt");
                Globals.gamedatato = new System.IO.StreamWriter(Globals.PATH + "\\Logs\\to_gamelog.txt");
                Globals.clientdataout = new System.IO.StreamWriter(Globals.PATH + "\\Logs\\from_clientlog.txt");
                Globals.clientdatato = new System.IO.StreamWriter(Globals.PATH + "\\Logs\\to_clientlog.txt");

                Globals.gamedataout.AutoFlush = true;
                Globals.gamedatato.AutoFlush = true;
                Globals.clientdataout.AutoFlush = true;
                Globals.clientdatato.AutoFlush = true;
            }
            catch
            {
            }
#endif

            //set up all the arraylists for data
            Globals.gamedata.my_char = new Player_Info();
            Globals.gamedata.my_pet = new Pet_Info();
            Globals.gamedata.my_pet1 = new Pet_Info();
            Globals.gamedata.my_pet2 = new Pet_Info();
            Globals.gamedata.my_pet3 = new Pet_Info();

            //flush and clear all the shortcuts
            for (int i = 0; i < Globals.Skills_Pages * Globals.Skills_PerPage; i++)
            {
                Globals.gamedata.ShortCuts.Add(new ShortCut());
            }

            Globals.gamedata.botoptions = new BotOptions();
            Globals.gamedata.alertoptions = new AlertOptions();

            LoadData.LoadDataFiles();

            AddInfo.Set_PartyVisible();
            AddInfo.Set_PartyInfo();

            try
            {
                Globals.Keyboard = new DX_Keyboard();
            }
            catch (System.Exception e)
            {
                Globals.l2net_home.Add_Error("crash: failed to create SlimDX Keyboard : " + e.Message);
            }

            Globals.broadcastthread = new BroadcastThread();

            Util.Setup_Threads();

            Globals.l2net_home.Add_Text("Full version enabled, enjoy :)", Globals.Red, TextType.BOT);


            //process command line crap
            //TD: modified to allow for longer command line parameter names
            //      Kept compatability with old command line options. 
            //      Added support for more verbose options.

            
            foreach (string s in args)
            {
               // char type = s[1];
                string command;
                string data;
                int indexOfColon = s.IndexOf(':');
                if (indexOfColon == -1)
                {
                    command = s;
                    data = "";
                }
                else
                {

                    command = s.Substring(0, indexOfColon);
                    data = s.Substring(indexOfColon + 1, s.Length - (indexOfColon + 1));
                    
                }
               
                command = command.ToLower();

                switch (command)
                {
                    case "-a":
                    case "-accept":
                        Globals.pre_agree = true;
                        break;

                    case "-b":
                    case "-blowfish":
                        Globals.pre_blowfish = data;
                        break;

                    case "-c":
                    case "-protocol":
                        Globals.pre_chron_cmd = true;
                        Globals.pre_protocol = data;
                        break;

                    case "-d":
                    case "-loginport":
                        Globals.pre_login_port = data;
                        break;

                    case "-i":
                    case "-loginip":
                        Globals.pre_login_ip = data;
                        break;

                    case "-o":
                    case "-options":
                        Globals.BotOptionsFile = data;
                        break;

                    case "-p":
                    case "-password":
                        Globals.pre_password = data;
                        break;
                    case "-u":
                    case "-username":
                        Globals.pre_username = data;
                        break;

                    case "-s":
                    case "-script":
                        Globals.Script_MainFile = data;
                        break;

                    case "-x":
                    case "-iglistenport":
                        Globals.pre_IG_listen_port = data;
                        break;

                    case "-y":
                    case "-iglistenip":
                        Globals.pre_IG_listen_ip = data;
                        break;

                    case "-z":
                    case "-chronicle":
                        Globals.pre_chron = Util.GetInt32(data);
                        break;

                    case "-overidegameserver":
                        Globals.pre_useGameServerOveride = true;
                        Globals.pre_checkAdvancedSettings = true;
                        break;
                    case "-useproxylogin":
                        Globals.pre_useProxyServerForLogin = true;
                        Globals.pre_checkAdvancedSettings = true;
                        break;

                    case "-useproxygame":
                        Globals.pre_useProxyServerForGameserver = true;
                        Globals.pre_checkAdvancedSettings = true;
                        break;       
        
                    case "-gameserverip":
                        Globals.pre_gameserver_override_ip = data;
                        break;
                    case "-gameserverport":
                        Globals.pre_gameserver_override_port = data;
                        break;
                    case "-socks5ip":
                        Globals.pre_socks5_ip = data;
                        break;
                    case "-socks5port":
                        Globals.pre_socks5_port = data;
                        break;
                    case "-socks5username":
                        Globals.pre_socks5_username = data;
                        break;
                    case "-socks5password":
                        Globals.pre_socks5_password = data;
                        break;
                    case "-ew":
                    case "-enterworld":
                        Globals.pre_EnterWorldCheckbox = true;
                        break;
                    case "-ig":
                        Globals.pre_IG = true;
                        break;
                    case "-oog":
                        Globals.pre_OOG = true;
                        break;
                    case "-ewip":
                        Globals.pre_enterworld_ip = true;
                        break;
                    case "-ip01":
                        Globals.pre_enterworld_ip_tab[0] = data;
                        break;
                    case "-ip02":
                        Globals.pre_enterworld_ip_tab[1] = data;
                        break;
                    case "-ip03":
                        Globals.pre_enterworld_ip_tab[2] = data;
                        break;
                    case "-ip04":
                        Globals.pre_enterworld_ip_tab[3] = data;
                        break;
                    case "-ip11":
                        Globals.pre_enterworld_ip_tab[4] = data;
                        break;
                    case "-ip12":
                        Globals.pre_enterworld_ip_tab[5] = data;
                        break;
                    case "-ip13":
                        Globals.pre_enterworld_ip_tab[6] = data;
                        break;
                    case "-ip14":
                        Globals.pre_enterworld_ip_tab[7] = data;
                        break;
                    case "-ip21":
                        Globals.pre_enterworld_ip_tab[8] = data;
                        break;
                    case "-ip22":
                        Globals.pre_enterworld_ip_tab[9] = data;
                        break;
                    case "-ip23":
                        Globals.pre_enterworld_ip_tab[10] = data;
                        break;
                    case "-ip24":
                        Globals.pre_enterworld_ip_tab[11] = data;
                        break;
                    case "-ip31":
                        Globals.pre_enterworld_ip_tab[12] = data;
                        break;
                    case "-ip32":
                        Globals.pre_enterworld_ip_tab[13] = data;
                        break;
                    case "-ip33":
                        Globals.pre_enterworld_ip_tab[14] = data;
                        break;
                    case "-ip34":
                        Globals.pre_enterworld_ip_tab[15] = data;
                        break;
                    case "-ip41":
                        Globals.pre_enterworld_ip_tab[16] = data;
                        break;
                    case "-ip42":
                        Globals.pre_enterworld_ip_tab[17] = data;
                        break;
                    case "-ip43":
                        Globals.pre_enterworld_ip_tab[18] = data;
                        break;
                    case "-ip44":
                        Globals.pre_enterworld_ip_tab[19] = data;
                        break;
                    case "-ubs":
                        Globals.pre_unknow_blowfish = true;
                        break;
                    case "-wasp":
                        Globals.pre_proxy_serv = true;
                        break;
                    case "-gslp":
                        Globals.pre_game_srv_listen_prt = data;
                        break;
                    case "-ggip":
                        Globals.gamedata.GG_IP = data;
                        break;
                    case "-ggport":
                        Globals.gamedata.GG_Port = Util.GetInt32(data);
                        break;
                    case "-ggsrv":
                        Globals.pre_GGSrv = true;
                        break;
                    case "-ggcl":
                        Globals.pre_GGClient = true;
                        break;
                    case "-secpin":
                        Globals.SecurityPin = data;
                        break;
                    case "-oldclient":
                        Globals.gamedata.SecurityPinOldClient = true;
                        break;




                }
            }
        }

        public static void ProcessDataThread()
        {
            Globals.gamedata.my_char.lastVerifyTime = System.DateTime.Now;

            DateTime last_animate = DateTime.Now;
            DateTime last_alert = DateTime.Now;
            DateTime last_clean = DateTime.Now;
            //DateTime last_draw = DateTime.Now;

            Globals.l2net_home.timer_mybuffs.Start();

			try
			{
                while (Globals.gamedata.running)
                {
                    //Util.PopUp_Check();

                    HandlePackets();

                    System.Threading.Thread.Sleep(Globals.SLEEP_ProcessDataThread);//sleep for 10ms; when we get new data it should wake us up

                    if (((TimeSpan)(DateTime.Now - last_animate)).Ticks > Globals.SLEEP_Animate)
                    {
                        AnimateStuff();
                        last_animate = DateTime.Now;
                    }

                    if (((TimeSpan)(DateTime.Now - last_alert)).Ticks > Globals.SLEEP_Sound_Alerts)
                    {
                        PlayAlerts();
                        last_alert = DateTime.Now;
                    }

                    if (((TimeSpan)(DateTime.Now - last_clean)).Ticks > Globals.CLEAN_TIMER)
                    {
                        CleanUp();
                        last_clean = DateTime.Now;
                    }
                }//end of while running
			}
            catch (System.Exception e)
            {
                Globals.l2net_home.Add_Error("crash: ProcessDataThread : hardcore crash... every bot function is DOA now : " + e.Message);
            }
        }
    }//end of class
}
