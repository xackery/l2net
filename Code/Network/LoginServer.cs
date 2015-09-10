using System;
using System.Collections.Generic;
using System.Text;

namespace L2_login
{
    class LoginServer
    {
        public static byte[] BuildAuthLogin(int prot)
        {
            byte[] buff = new byte[512];//263

            buff[0] = 0x0B; ;//2 byte size
            buff[1] = 0x01;
            buff[2] = (byte)PClient.AuthLogin;

            byte[] bff2 = new byte[4];
            bff2 = System.BitConverter.GetBytes(prot);
            buff[3] = bff2[0];
            buff[4] = bff2[1];
            buff[5] = bff2[2];
            buff[6] = bff2[3];
            int off = 7;

            //Globals.l2net_home.Add_Text(bff2[0].ToString() + " " + bff2[1].ToString());

            //"00 94 02
            string pee = "09 07 54 56 03 09 0B 01 07 02 54 " +
                "54 56 07 00 02 55 56 00 51 00 53 57 04 07 55 08 " +
                "54 01 07 01 53 00 56 55 56 01 06 05 04 51 03 08 " +
                "51 08 51 56 04 54 06 55 08 02 09 51 56 01 53 06 " +
                "55 04 53 00 56 56 53 01 09 02 09 01 51 54 51 09 " +
                "55 56 09 03 04 07 05 55 04 06 55 04 06 09 04 51 " +
                "01 08 08 06 05 52 06 04 01 07 54 03 06 52 55 06 " +
                "55 55 51 01 02 04 54 03 55 54 01 57 51 55 05 52 " +
                "05 54 07 51 51 55 07 02 53 53 00 52 05 52 07 01 " +
                "54 00 03 05 05 08 06 05 05 06 03 00 0D 08 01 07 " +
                "09 03 51 03 07 53 09 51 06 07 54 0A 50 56 02 52 " +
                "04 05 55 51 02 53 00 08 54 04 52 56 06 02 09 00 " +
                "08 03 53 56 01 05 00 55 06 08 56 04 0D 06 07 52 " +
                "06 07 04 0A 06 01 04 54 04 00 05 02 04 54 00 09 " +
                "52 53 05 04 01 04 05 05 01 52 51 52 0D 06 51 08 " +
                "09 54 53 00 0D 01 02 03 54 53 01 05 03 08 56 54 " +
                "07 02 54 0B 06 ";//change last 4 each chron

            switch(Globals.gamedata.Chron)
            {
                case Chronicle.CT3_0: //Still the same in GoD
                    pee = pee + "A6 23 F4 FE";
                    break;
                case Chronicle.CT2_6:
                    pee = pee + "A6 23 F4 FE";
                    break;
                case Chronicle.CT2_5:
                    pee = pee + "A6 23 F4 FE";
                    break;
                case Chronicle.CT2_4:
                    pee = pee + "A6 23 F4 FE";
                    break;
                case Chronicle.CT2_3:
                    pee = pee + "11 5D 1F 60";
                    break;
                case Chronicle.CT2_2:
                case Chronicle.CT2_1:
                    pee = pee + "EB EF 3D E6";
                    break;
                case Chronicle.CT1_5:
                case Chronicle.CT1:
                    pee = pee + "8D 71 5F 08";
                    break;
                case Chronicle.Interlude:
                    pee = pee + "DC 4F 61 4F";
                    break;
                case Chronicle.Chronicle_5:
                    pee = pee + "A7 08 2B C0";
                    break;
            }

            pee = pee.Replace(" ", "");
            string sm;

            for (int i = 0; i < pee.Length; i += 2)
            {
                sm = (pee[i].ToString()) + (pee[i + 1].ToString());

                buff[off + i / 2] = byte.Parse(sm, System.Globalization.NumberStyles.HexNumber);
            }

            return buff;
        }

        /******************game guard packets**************/
        public static void Set_GG(byte[] fill, int off)
        {
            //old
            //07 xx xx xx xx 45 00 01 00 1e 37 a2 f5 00 00 00 00 00 00 00 00 00 00 00 f2 a6 bd 81 00 00 00 00
            //new
            //07 xx xx xx xx 23 92 90 4D 18 30 B5 7C 96 61 41 47 05 07 96 FB 00 00 00 8A 55 4E D0 00 00 00 00
            //07 FD 8A 22 00 23 92 90 4D 18 30 B5 7C 96 61 41 47 05 07 96 FB 00 00 00 8A 55 4E D0 00 00 00 00 
            //07 FD 8A 22 00 23 92 90 4D 18 30 B5 7C 96 61 41 47 05 07 96 FB 00 00 00 8A 55 4E D0 00 00 00 00 
            //07 xx xx xx xx 23 92 90 4D 18 30 B5 7C 96 61 41 47 05 07 96 FB 00 00 00 - GG 1057
            //first 5 are packet header bs...                                      //checksum    //padding   // last 8 are garbage
            //07 15 1D B1 21 23 92 90 4D 18 30 B5 7C 96 61 41 47 05 07 96 FB 00 00 00 AB BD D9 43 00 00 00 00 00 00 00 00 00 00 00 00 
            //07 E4 8F 63 94 23 92 90 4D 18 30 B5 7C 96 61 41 47 05 07 96 FB 00 00 00 1E 4C 4B 91 00 00 00 00 00 00 00 00 00 00 00 00 
            //07 38 0B BC 75 23 92 90 4D 18 30 B5 7C 96 61 41 47 05 07 96 FB 00 00 00 FF 90 CF 4E 00 00 00 00 00 00 00 00 00 00 00 00 
            //               23 92 90 4D 18 30 B5 7C 96 61 41 47 05 07 96 FB 00 00 00 :: CT 2.1 and previous
            //               23 01 00 00 67 45 00 00 AB 89 00 00 EF CD 00 00 08 00 00
            //               23 01 00 00 67 45 00 00 AB 89 00 00 EF CD 00 00 00 00 00 :: CT 2.2
            fill[00 + off] = Globals.Login_GG_Reply[0];//0x23;
            fill[01 + off] = Globals.Login_GG_Reply[1];//0x92;
            fill[02 + off] = Globals.Login_GG_Reply[2];//0x90;
            fill[03 + off] = Globals.Login_GG_Reply[3];//0x4d;
            fill[04 + off] = Globals.Login_GG_Reply[4];//0x18;
            fill[05 + off] = Globals.Login_GG_Reply[5];//0x30;
            fill[06 + off] = Globals.Login_GG_Reply[6];//0xb5;
            fill[07 + off] = Globals.Login_GG_Reply[7];//0x7c;
            fill[08 + off] = Globals.Login_GG_Reply[8];//0x96;
            fill[09 + off] = Globals.Login_GG_Reply[9];//0x61;
            fill[10 + off] = Globals.Login_GG_Reply[10];//0x41;
            fill[11 + off] = Globals.Login_GG_Reply[11];//0x47;
            fill[12 + off] = Globals.Login_GG_Reply[12];//0x05;
            fill[13 + off] = Globals.Login_GG_Reply[13];//0x07;
            fill[14 + off] = Globals.Login_GG_Reply[14];//0x96;
            fill[15 + off] = Globals.Login_GG_Reply[15];//0xfb;
            //fill[16 + off] = Globals.Login_GG_Reply[16];//0x00;
            //fill[17 + off] = Globals.Login_GG_Reply[17];//0x00;
            //fill[18 + off] = Globals.Login_GG_Reply[18];//0x00;
        }

        public static void Start_Threads()
        {
            Globals.gamethread.readthread.Start();
            Globals.gamethread.sendthread.Start();
            Globals.gameprocessdatathread.Start();
            Globals.botaithread.botaithread.Start();
            //Oddi: Start followrestthread
            Globals.followrestthread.followrestthread.Start();

            Globals.gamedrawthread.Start();
        }

        public static void IG_Init()
        {
            //set to ingame mode
            Globals.gamedata.ig_login = true;
            Globals.gamedata.OOG = false;

            //start the login tunnel in case the login and server ip are the same
            Globals.ig_loginthread.Start();

            //start the game tunnel listener shit...
            Globals.ig_listener.Start();
        }

        public static void IG_Login()
        {
			try
			{
                if (!Globals.gamedata.Unkown_Blowfish || Globals.gamedata.LS_GS_Same_IP)
                {
                    System.Net.IPAddress ipAd = System.Net.IPAddress.Parse(Globals.gamedata.IG_Local_IP);

                    Globals.l2net_home.Add_Text("client -> bot loginserver : waiting", Globals.Red, TextType.BOT);

                    Globals.Login_ClientLink = new System.Net.Sockets.TcpListener(ipAd, Globals.gamedata.IG_Local_Login_Port);
                    Globals.Login_ClientLink.Start();

                    //get our client connection
                    CreateClientLoginLink();

                    Globals.l2net_home.Add_Text("client -> bot loginserver : connected", Globals.Red, TextType.BOT);
                    OpenLoginServerConnection();

                    Globals.loginsendthread.Start();
                    Globals.loginreadthread.Start();
                }
                else
                {
                    Globals.ig_Gamelistener.Start();
                }
			}
			catch
			{
				Globals.l2net_home.Add_Error("crash: starting the login server tunnel");
			}
        }

        public static void IG_Listener()
        {
			try
			{
                bool valid = false;

                while (valid == false)
                {
                    System.Net.IPAddress ipAd = System.Net.IPAddress.Parse(Globals.gamedata.IG_Local_IP);//textBox_ig_local.Text);

                    Globals.l2net_home.Add_Text("client -> bot gameserver : waiting");
                    if (Globals.proxy_serv)
                    {
                         Globals.gamedata.IG_Local_Game_Port = System.Convert.ToInt16(Globals.game_srv_listen_prt);
                    }
                    bool got_connection = false;

                    while (!got_connection)
                    {
                        try
                        {
                             Globals.Game_ClientLink = new System.Net.Sockets.TcpListener(ipAd, Globals.gamedata.IG_Local_Game_Port);

                            Globals.Game_ClientLink.Start();
                            got_connection = true;
                        }
                        catch
                        {
                            if (!Globals.clientport_ready)
                            {
                                Globals.gamedata.IG_Local_Game_Port = Globals.gamedata.IG_Local_Game_Port + 1;
                            }
                        }
                    }
                    Globals.l2net_home.Add_Text("client -> bot gameserver : using port " + Globals.gamedata.IG_Local_Game_Port.ToString(), Globals.Red, TextType.BOT);
                    Globals.clientport_ready = true;

                    //get our client connection
                    CreateClientGameLink();
                    Globals.Game_ClientLink.Stop(); // i hope this will stop the multipel listening ...


                    Globals.l2net_home.Add_Text("client -> bot gameserver : connected", Globals.Red, TextType.BOT);

                    //need to check protocol here... see if we got punked...

                    byte[] buffread = new byte[Globals.BUFFER_MAX];
                    // proxy srv war(adifenix)
                    int cnt = 0;
                    int proxy_step = 0;
                    if (Globals.proxy_serv)
                    {
                        bool temp_proxy_var = true;
                        while (temp_proxy_var)
                        {
                            cnt = Globals.Game_ClientSocket.Receive(buffread, 0, Globals.BUFFER_PACKET, System.Net.Sockets.SocketFlags.None);
                            if (buffread.Length > 0)
                            {
                                if (proxy_step == 0) // first step == 1 packet
                                {
                                    if (buffread[0] == 5)// checking proxy version
                                    {
                                        // packet struct for sock 5 
                                        //based on http://www.faqs.org/rfcs/rfc1928.html
                                        // buffread[0]  == 5  // proxy sock ver 5
                                        // buffread[1]  ==(1-255) no. methods for log in on serv
                                        // buffread[2-no methods] - methods for login (without login/pass , with login pass etc)

                                        /*     methods "id"
                                         *           o  X'00' NO AUTHENTICATION REQUIRED
                                                      o  X'01' GSSAPI
                                                      o  X'02' USERNAME/PASSWORD
                                                      o  X'03' to X'7F' IANA ASSIGNED
                                                      o  X'80' to X'FE' RESERVED FOR PRIVATE METHODS
                                                      o  X'FF' NO ACCEPTABLE METHODS
                                         * 
                                         * 
                                         */

                                        for (int i = 2; i < buffread.Length; i++)//try to find method (0) aka without login/pass
                                        {
                                            if (buffread[i] == 0)// found method 0 (without pass)
                                            {
                                                // seding answer to client...
                                                buffread[1] = 0;
                                                Globals.Game_ClientSocket.Send(buffread, 2, System.Net.Sockets.SocketFlags.None);
                                                proxy_step = 1;
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Globals.l2net_home.Add_Text("wrong proxy version", Globals.Red, TextType.BOT);
                                    }
                                }
                                else if (proxy_step == 1)
                                {

                                    // we get packet with info about dest conection
                                    // atm without handler (lazy :P) ( upd  4.12.12 - some parsing :P)
                                    Globals.proxy_serv_ip[0] = buffread[4];
                                    Globals.proxy_serv_ip[1] = buffread[5];
                                    Globals.proxy_serv_ip[2] = buffread[6];
                                    Globals.proxy_serv_ip[3] = buffread[7];

                                    Globals.proxy_serv_port[0] = buffread[9]; // litle / big endian port ...
                                    Globals.proxy_serv_port[1] = buffread[8];
#if DEBUG
                                    Globals.l2net_home.Add_Text("step 1 - last one before  prot packet", Globals.Red, TextType.BOT);
#endif
                                    // sending answer
                                    buffread[0] = 5; //ver sock 5
                                    buffread[1] = 0;//sucefull
                                    buffread[2] = 0;// reserved (must be 0)
                                    buffread[3] = 1;//adres type (1) ipv4
                                    buffread[4] = 127;
                                    buffread[5] = 0;
                                    buffread[6] = 0;
                                    buffread[7] = 1;
                                    buffread[8] = 0;
                                    buffread[9] = 0;
                                    //
                                    Globals.Game_ClientSocket.Send(buffread, 10, System.Net.Sockets.SocketFlags.None);
                                    temp_proxy_var = false;
                                }
                            }
                        }// after while (proxy ....) reci protocol packet
                        cnt = Globals.Game_ClientSocket.Receive(buffread, 0, Globals.BUFFER_PACKET, System.Net.Sockets.SocketFlags.None);
                    }
                    else
                    {
                        cnt = Globals.Game_ClientSocket.Receive(buffread, 0, Globals.BUFFER_PACKET, System.Net.Sockets.SocketFlags.None);
                    }
                    int size = System.BitConverter.ToUInt16(buffread, 0);
                   
                    if (buffread.Length > 0)
                    {
#if DEBUG
                        
                        try
                        {
                            Globals.clientdataout.WriteLine("packet...-size: " + size.ToString() + " -count:" + cnt.ToString() + " :::time:::" + System.DateTime.Now.TimeOfDay.ToString() + ":::");
                            Globals.clientdataout.WriteLine("-data from client to bot hex-");
                            for (uint i = 2; i < size; i++)
                            {
                                Globals.clientdataout.Write(buffread[i].ToString("X2"));
                                Globals.clientdataout.Write(" ");
                            }
                            Globals.clientdataout.WriteLine("");
                            Globals.clientdataout.WriteLine("-data from client to bot string-");
                            for (uint i = 2; i < size; i++)
                            {
                                Globals.clientdataout.Write((char)buffread[i]);
                            }
                            Globals.clientdataout.WriteLine("");
                        }
                        catch
                        {
                            //failed to write... oh well
                        }
#endif

                        switch ((PClient)buffread[2])
                        {
                            case PClient.AuthLogin:
                                int prot = System.BitConverter.ToInt16(buffread, 3);
                                Globals.l2net_home.Add_Text("protocol version: " + prot.ToString(), Globals.Red, TextType.BOT);

                                if (prot == -2)
                                {
                                    Globals.l2net_home.Add_Text("-2 protocol... just a ping... we've been punked XD...", Globals.Red, TextType.BOT);
                                    Globals.l2net_home.Add_Text("killing listener and attempting reconnection...", Globals.Red, TextType.BOT);

                                    Util.KillClientGameConnections();

                                    valid = false;
                                }
                                else
                                {
                                    //valid protocol...
                                    Globals.clientsocket_ready = true;

                                    Globals.l2net_home.Add_Text("valid protocol... waiting for game server connection", Globals.Red, TextType.BOT);

                                    while (!Globals.gamesocket_ready)
                                    {
                                        //lets take a short nap until we have the connection
                                        System.Threading.Thread.Sleep(Globals.SLEEP_WaitIGConnection);
                                    }

                                    Globals.l2net_home.Add_Text("valid protocol... forwarding to game server", Globals.Red, TextType.BOT);

                                    if (Globals.gamedata.OverrideProtocol)
                                    {
                                        byte[] prot_out = new byte[4];
                                        prot_out = System.BitConverter.GetBytes(Globals.gamedata.Protocol);
                                        buffread[3] = prot_out[0];
                                        buffread[4] = prot_out[1];
                                        buffread[5] = prot_out[2];
                                        buffread[6] = prot_out[3];
                                    }
                                    //need to forward this packet along...
                                    Globals.Game_GameSocket.Send(buffread, 0, size, System.Net.Sockets.SocketFlags.None);

                                    valid = true;
                                }
                                break;
                        }
                    }
                }

                Globals.l2net_home.Add_Text("client -> bot gameserver : connected", Globals.Red, TextType.BOT);
			}
			catch
			{
				Globals.l2net_home.Add_Error("crash: ingame listener manager");
			}
        }

        public static void LoginSendThread()
        {
            int data_num = 0;

            byte[] buff = new byte[Globals.BUFFER_PACKET];
            byte[] dec_buff = new byte[Globals.BUFFER_PACKET];
            int cnt = 0;

            byte[] sess = new byte[4];

            BlowfishEngine bfengs = new BlowfishEngine();

#if DEBUG
            System.IO.StreamWriter login_serverout = new System.IO.StreamWriter("Logs\\login_from_serverlog.txt");
            login_serverout.AutoFlush = true;
#endif

            try
            {
                //get data from the server
                //and forward to the client
                while (Globals.gamedata.ig_login)
                {
                    data_num++;
                    cnt = Globals.Login_GameSocket.Receive(buff, 0, Globals.BUFFER_PACKET, System.Net.Sockets.SocketFlags.None);
                    if (Globals.gamedata.Unkown_Blowfish)
                    {
                        /*
                        Globals.l2net_home.Add_Text("Data_num: " + data_num.ToString(), Globals.Green);
                        if (data_num > 10)
                        {
                            Globals.l2net_home.Add_Text("Data_num > 10", Globals.Red);
                            Globals.gamedata.ig_login = false;
                        }*/

                    }
                    else
                    {
                        //unblowfishzor
                        bfengs.init(false, Globals.gamedata.blow_key);
                        bfengs.processBigBlock(buff, 2, dec_buff, 0, cnt - 2);

                        if (data_num == 1)
                        {
                            //need to unxor this shit
                            NewCrypt.decXORPass(dec_buff, 0, cnt - 2, System.BitConverter.ToInt32(dec_buff, cnt - 10));
                        }

#if DEBUG

                    login_serverout.WriteLine(" :::time:::" + System.DateTime.Now.TimeOfDay.ToString() + ":::" + cnt.ToString() + ":::" + data_num.ToString());
                    login_serverout.WriteLine("-ENcrypted data from login server to bot hex-");
                    for (int i = 0; i < cnt; i++)
                    {
                        login_serverout.Write(buff[i].ToString("X2"));
                        login_serverout.Write(" ");
                    }
                    login_serverout.WriteLine("");
                    login_serverout.WriteLine("-ENcrypted data from login server to bot string-");
                    for (int i = 0; i < cnt; i++)
                    {
                        login_serverout.Write((char)buff[i]);
                    }
                    login_serverout.WriteLine("");

                    //login_serverout.WriteLine(" :::time:::" + System.DateTime.Now.TimeOfDay.ToString() + "::: size=" + cnt.ToString());
                    login_serverout.WriteLine("-data from login server to bot hex-");
                    for (int i = 0; i < cnt - 2; i++)
                    {
                        login_serverout.Write(dec_buff[i].ToString("X2"));
                        login_serverout.Write(" ");
                    }
                    login_serverout.WriteLine("");
                    login_serverout.WriteLine("-data from login server to bot string-");
                    for (int i = 0; i < cnt - 2; i++)
                    {
                        login_serverout.Write((char)dec_buff[i]);
                    }
                    login_serverout.WriteLine("");
                    login_serverout.WriteLine("");
#endif

                        if (data_num == 1)//buff[0] == 0x00)
                        {
                            //RSA key
                            sess[0] = dec_buff[1];
                            sess[1] = dec_buff[2];
                            sess[2] = dec_buff[3];
                            sess[3] = dec_buff[4];
                            //5,6,7,8 = protocol revision
                            //9 - 136 = rsa key
                            //137,138,139,140
                            //141,142,143,144
                            //145,146,147,148
                            //149,150,151,152

                            //need to grab the new blowfish key
                            byte[] tmp_bf = new byte[16];

#if DEBUG
                        login_serverout.Write("blowfish key:");
#endif

                            for (int i = 0; i < 16; i++)
                            {
                                tmp_bf[i] = dec_buff[153 + i];
#if DEBUG
                            login_serverout.Write(tmp_bf[i].ToString("X2"));
#endif
                            }
#if DEBUG
                        login_serverout.WriteLine("");
#endif

                            Globals.gamedata.SetBlowfishKey(tmp_bf);
                        }
                        else
                        {
                            switch (dec_buff[0])
                            {
                                case 0x01://login fail or security card
                                    //http://www.l2jserver.com/svn/trunk/L2_GameServer/java/com/l2jserver/loginserver/serverpackets/LoginFail.java
                                    if (dec_buff[1] == 0x1F)
                                    {
                                        Globals.l2net_home.Add_Text("Security card", Globals.Red, TextType.BOT);
                                        break;
                                    }
                                    else if (dec_buff[1] == 0x02)
                                    {
                                        Globals.l2net_home.Add_Text("Wrong password", Globals.Red, TextType.BOT);
                                        Globals.gamedata.ig_login = false;
                                        Globals.gamedata.login_failed = true;
                                        break;
                                    }
                                    else if (dec_buff[1] == 0x03)
                                    {
                                        Globals.l2net_home.Add_Text("Wrong username or password", Globals.Red, TextType.BOT);
                                        Globals.gamedata.ig_login = false;
                                        Globals.gamedata.login_failed = true;
                                        break;
                                    }
                                    else if (dec_buff[1] == 0x07)
                                    {
                                        Globals.l2net_home.Add_Text("Account already in use", Globals.Red, TextType.BOT);
                                        Globals.gamedata.ig_login = false;
                                        Globals.gamedata.login_failed = true;
                                        break;
                                    }
                                    else if (dec_buff[1] == 0x0F)
                                    {
                                        Globals.l2net_home.Add_Text("Server overloaded", Globals.Red, TextType.BOT);
                                        Globals.gamedata.ig_login = false;
                                        Globals.gamedata.login_failed = true;
                                        break;
                                    }
                                    else if (dec_buff[1] == 0x10)
                                    {
                                        Globals.l2net_home.Add_Text("Server maintenance", Globals.Red, TextType.BOT);
                                        Globals.gamedata.ig_login = false;
                                        Globals.gamedata.login_failed = true;
                                        break;
                                    }
                                    else
                                    {
                                        Globals.l2net_home.Add_Text("login fail", Globals.Red, TextType.BOT);
                                        Globals.gamedata.ig_login = false;
                                        Globals.gamedata.login_failed = true;
                                        break;
                                    }
                                case 0x03://login ok
                                    Globals.l2net_home.Add_Text("login ok", Globals.Red, TextType.BOT);
                                    break;
                                case 0x04://serverlist
                                    Globals.l2net_home.Add_Text("loginsend - got the server list; modifying packet and sending to client", Globals.Red, TextType.BOT);
                                    //lets change the ips before we send to the client

                                    //change the server ips
                                    byte ip1, ip2, ip3, ip4;

                                    int pipe;
                                    string inp = Globals.gamedata.IG_Local_IP;
                                    //IP1
                                    pipe = inp.IndexOf('.');
                                    ip1 = System.Convert.ToByte(inp.Substring(0, pipe));
                                    inp = inp.Remove(0, pipe + 1);
                                    //IP2
                                    pipe = inp.IndexOf('.');
                                    ip2 = System.Convert.ToByte(inp.Substring(0, pipe));
                                    inp = inp.Remove(0, pipe + 1);
                                    //IP3
                                    pipe = inp.IndexOf('.');
                                    ip3 = System.Convert.ToByte(inp.Substring(0, pipe));
                                    inp = inp.Remove(0, pipe + 1);
                                    //IP4
                                    ip4 = System.Convert.ToByte(inp);

                                    //we need to make suer we are listening already...
                                    while (!Globals.clientport_ready)
                                    {
                                        //lets take a short nap until we have the connection
                                        System.Threading.Thread.Sleep(Globals.SLEEP_WaitIGConnection);
                                    }

                                    byte[] bport = System.BitConverter.GetBytes(Globals.gamedata.IG_Local_Game_Port);

                                    int Login_ServerCount = dec_buff[1];//number of servers
                                    const int offset = 3;

                                    int m = 21;

                                    Globals.Login_Servers = new System.Collections.SortedList();
                                    Globals.Login_Servers.Capacity = Login_ServerCount;

                                    for (int i = 0; i < Login_ServerCount; i++)
                                    {
                                        try
                                        {
                                            Server n_sev = new Server();
                                            n_sev.ID = (uint)System.Convert.ToSByte(dec_buff[0 + offset + i * m]);
                                            n_sev.IP = ((int)dec_buff[1 + offset + i * m]).ToString() + "." + ((int)dec_buff[2 + offset + i * m]).ToString() + "." + ((int)dec_buff[3 + offset + i * m]).ToString() + "." + ((int)dec_buff[4 + offset + i * m]).ToString();
                                            n_sev.Port = System.BitConverter.ToInt32(dec_buff, 5 + offset + i * m);
                                            Globals.Login_Servers.Add(n_sev.ID, n_sev);

                                            dec_buff[1 + offset + i * m] = ip1;
                                            dec_buff[2 + offset + i * m] = ip2;
                                            dec_buff[3 + offset + i * m] = ip3;
                                            dec_buff[4 + offset + i * m] = ip4;

                                            dec_buff[5 + offset + i * m] = bport[0];
                                            dec_buff[6 + offset + i * m] = bport[1];
                                            dec_buff[7 + offset + i * m] = bport[2];
                                            dec_buff[8 + offset + i * m] = bport[3];

                                            Globals.l2net_home.Add_Text(Util.GetServer((uint)dec_buff[0 + offset + i * m] - 1) + ": " + System.BitConverter.ToUInt16(dec_buff, 11 + offset + i * m).ToString() + "/" + System.BitConverter.ToUInt16(dec_buff, 13 + offset + i * m).ToString(), Globals.Red, TextType.BOT);
                                        }
                                        catch
                                        {

                                        }
                                    }

                                    //adjust the checksum
                                    NewCrypt.appendChecksum(dec_buff, 0, cnt - 2);

                                    //re-encode
                                    bfengs.init(true, Globals.gamedata.blow_key);
                                    bfengs.processBigBlock(dec_buff, 0, buff, 2, cnt - 2);
                                    break;
                                case 0x06://play fail
                                    Globals.gamedata.ig_login = false;
                                    Globals.gamedata.login_failed = true;
                                    break;
                                case 0x07://play ok
                                    //we shouldnt get anymore data from the gameserver now
                                    Globals.gamedata.ig_login = false;
                                    break;
                                case 0x0B://gameguard check reply from server
                                    break;
                            }//end of switch
                        }
                    }

                    Globals.Login_ClientSocket.Send(buff, 0, cnt, System.Net.Sockets.SocketFlags.None);
                }
            }
            catch
            {
                Globals.l2net_home.Add_Error("crash: IG LoginSendThread");
            }
            finally
            {
#if DEBUG
                login_serverout.Close();
#endif
            }

            //close the connection to the server
            //Globals.l2net_home.Add_Text("Closing loginserver connection (login send thread)", Globals.Green, TextType.BOT);
            Globals.Login_GameSocket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
            Globals.Login_GameSocket.Close();

            if (Globals.gamedata.login_failed)
            {
                Globals.pre_IG = true;
                Globals.l2net_home.KillEverything();
            }
        }

        public static void LoginReadThread()
        {
            byte[] buff = new byte[Globals.BUFFER_PACKET];
            byte[] dec_buff = new byte[Globals.BUFFER_PACKET];

            BlowfishEngine bfengr = new BlowfishEngine();

            int cnt;
            bool started = false;

#if DEBUG
            System.IO.StreamWriter login_clientout = new System.IO.StreamWriter("Logs\\login_from_clientlog.txt");
            login_clientout.AutoFlush = true;
#endif

            try
            {
                //get data from the client
                //and forward to the server
                while (Globals.gamedata.ig_login)
                {
                    cnt = Globals.Login_ClientSocket.Receive(buff, 0, Globals.BUFFER_PACKET, System.Net.Sockets.SocketFlags.None);

                    if (Globals.gamedata.Unkown_Blowfish)
                    {
                        //Globals.l2net_home.Add_Text("Unknown Blowfish", Globals.Red);
                        if (!started)
                        {
                            Globals.ig_Gamelistener.Start();
                            started = true;
                        }
                    }
                    else
                    {
                        bfengr.init(false, Globals.gamedata.blow_key);
                        bfengr.processBigBlock(buff, 2, dec_buff, 0, cnt - 2);

#if DEBUG
                    login_clientout.WriteLine(" :::time:::" + System.DateTime.Now.TimeOfDay.ToString() + "::: size=" + cnt.ToString());
                    login_clientout.WriteLine("-ENcrypted data from login client to bot hex-");
                    for (int i = 0; i < cnt; i++)
                    {
                        login_clientout.Write(buff[i].ToString("X2"));
                        login_clientout.Write(" ");
                    }
                    login_clientout.WriteLine("");
                    login_clientout.WriteLine("-ENcrypted data from login client to bot string-");
                    for (int i = 0; i < cnt; i++)
                    {
                        login_clientout.Write((char)buff[i]);
                    }
                    login_clientout.WriteLine("");

                    login_clientout.WriteLine("-data from login client to bot hex-");
                    for (int i = 0; i < cnt - 2; i++)
                    {
                        login_clientout.Write(dec_buff[i].ToString("X2"));
                        login_clientout.Write(" ");
                    }
                    login_clientout.WriteLine("");
                    login_clientout.WriteLine("-data from login client to bot string-");
                    for (int i = 0; i < cnt - 2; i++)
                    {
                        login_clientout.Write((char)dec_buff[i]);
                    }
                    login_clientout.WriteLine("");
                    login_clientout.WriteLine("");
#endif

                        switch (dec_buff[0])
                        {
                            case 0x00://login username packet thingy.. lets grab the gg data
                                if (Globals.Script_Debugging)
                                {
                                    string gg_login = "";
                                    for (int i = 0; i < 24; i++)
                                    {
                                        gg_login += dec_buff[133 + i].ToString("X2") + " ";
                                    }
                                    Globals.l2net_home.Add_Debug("GameGuard Login Reply: " + gg_login);
                                }
                                break;
                            case 0x02://server select
                                Globals.Login_SelectedServer = System.Convert.ToUInt32(dec_buff[9]);

                                Globals.ig_Gamelistener.Start();
                                break;
                        }
                    }

                    Globals.Login_GameSocket.Send(buff, 0, cnt, System.Net.Sockets.SocketFlags.None);
                }
            }
            catch
            {
                //this thread will crash when the client closes the connection, since it continues to try to read the data
                //gotta change the while loop above to stop if the connection closes


                //Globals.l2net_home.Add_Error("crash: IG LoginReadThread");
            }
            finally
            {
#if DEBUG

                login_clientout.Close();
#endif
                try
                {
                //close the connection to the client
                //Globals.l2net_home.Add_Text("Closing loginserver connection (login read thread)", Globals.Green, TextType.BOT);
                Globals.Login_ClientSocket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                Globals.Login_ClientSocket.Close();

                //this fixes the socket error when trying to start two ig bots
                System.Threading.Thread.Sleep(Globals.SLEEP_LoginDelay);

                    Globals.Login_ClientLink.Stop();
                }
                catch
                {

                }
            }
        }

        public static void IG_StartGameLinks()
        {
			try
			{
                //we need to wait until the client has connected to the bot
                while (!Globals.clientsocket_ready)
                {
                    //lets take a short nap until we have the connection
                    System.Threading.Thread.Sleep(Globals.SLEEP_WaitIGConnection);
                }

                if (Globals.gamedata.Unkown_Blowfish)
                {
                    Globals.gamedata.ig_login = false;
                }
                //now lets open our bot connection to the game server
                OpenGameServerConnection();
                Globals.gamesocket_ready = true;

                //our connections are open...
                //lets start our threads now since they should handle this whole login process
                Globals.gamedata.running = true;
                Globals.Login_State = 3;

                Globals.l2net_home.Add_Text("ig - starting bot threads", Globals.Red, TextType.BOT);

                //start all the stuff
                Globals.clientthread.readthread.Start();
                Globals.clientthread.sendthread.Start();

                LoginServer.Start_Threads();
			}
			catch
			{
				Globals.l2net_home.Add_Error("open game server connection fail");
			}
        }

        //////////////////////////oog login
        
        public static void OOG_Init()
        {
            //set to ingame mode
            Globals.gamedata.ig_login = false;
            Globals.gamedata.OOG = true;

            Globals.oog_loginthread.Start();
        }

        public static void OOG_Login()
        {
#if !DEBUG
			try
			{
#endif
            Globals.l2net_home.Add_Text("Starting login Process", Globals.Red, TextType.BOT);

            OpenLoginServerConnection();

            byte[] buff = new byte[Globals.BUFFER_PACKET];
            byte[] dec_buff = new byte[Globals.BUFFER_PACKET];
            int cnt;

            byte[] sess = new byte[4];
            byte[] enckey = new byte[128];

            //global stream now

            BlowfishEngine bfeng = new BlowfishEngine();

            #region RSA Packet
            cnt = Globals.Login_GameSocket.Receive(buff, 0, Globals.BUFFER_PACKET, System.Net.Sockets.SocketFlags.None);

            //need to unblowfish
            bfeng.init(false, Globals.gamedata.blow_key);
            bfeng.processBigBlock(buff, 2, dec_buff, 0, cnt - 2);

            //need to unxor this shit
            NewCrypt.decXORPass(dec_buff, 0, cnt - 2, System.BitConverter.ToInt32(dec_buff, cnt - 10));

            sess[0] = dec_buff[1];
            sess[1] = dec_buff[2];
            sess[2] = dec_buff[3];
            sess[3] = dec_buff[4];

            //	if(cnt != 155)
            //	{
            //		Globals.l2net_home.Add_Error("packet of wrong size, possible wrong server type...going to continue to attempt login");
            //	}

            //9 thru 136
            //need to decode the RSA key
            for (int i = 0; i < 128; i++)
            {
                enckey[i] = dec_buff[9 + i];
            }

            //got the encoded key in enckey
            // step 4 : xor last 0x40 bytes with  first 0x40 bytes
            for (int i = 0; i < 0x40; i++)
            {
                enckey[0x40 + i] = (byte)(enckey[0x40 + i] ^ enckey[i]);
            }
            // step 3 : xor bytes 0x0d-0x10 with bytes 0x34-0x38
            for (int i = 0; i < 4; i++)
            {
                enckey[0x0d + i] = (byte)(enckey[0x0d + i] ^ enckey[0x34 + i]);
            }
            // step 2 : xor first 0x40 bytes with  last 0x40 bytes 
            for (int i = 0; i < 0x40; i++)
            {
                enckey[i] = (byte)(enckey[i] ^ enckey[0x40 + i]);
            }
            // step 1 : 0x4d-0x50 <-> 0x00-0x04
            for (int i = 0; i < 4; i++)
            {
                byte temp = enckey[0x00 + i];
                enckey[0x00 + i] = enckey[0x4d + i];
                enckey[0x4d + i] = temp;
            }

            Globals.l2net_home.Add_Text("Got RSA key", Globals.Red, TextType.BOT);

            byte[] tmp_bf = new byte[16];

            for (int i = 0; i < 16; i++)
            {
                tmp_bf[i] = dec_buff[153 + i];
            }
            Globals.gamedata.SetBlowfishKey(tmp_bf);
            Globals.l2net_home.Add_Text("Got Blowfish key", Globals.Red, TextType.BOT);


            /*****************game guard shit*******************/
            Globals.l2net_home.Add_Text("lol gameguard", Globals.Red, TextType.BOT);

            //42byte thingy
            byte[] send = new byte[40];
            byte[] sende = new byte[40];

            send[00] = 0x07;
            send[01] = sess[0];
            send[02] = sess[1];
            send[03] = sess[2];
            send[04] = sess[3];
            LoginServer.Set_GG(send, 5);

            NewCrypt.appendChecksum(send, 0, 28);

            bfeng.init(true, Globals.gamedata.blow_key);
            bfeng.processBigBlock(send, 0, sende, 0, 40);

            byte[] pack_out = new byte[42];

            pack_out[0] = 0x2A;
            pack_out[1] = 0x0;
            sende.CopyTo(pack_out, 2);

            Globals.Login_GameSocket.Send(pack_out, 0, 42, System.Net.Sockets.SocketFlags.None);
            #endregion

            bool oog_login = true;
            while (oog_login)
            {
                cnt = Globals.Login_GameSocket.Receive(buff, 0, Globals.BUFFER_PACKET, System.Net.Sockets.SocketFlags.None);

                bfeng.init(false, Globals.gamedata.blow_key);
                bfeng.processBigBlock(buff, 2, dec_buff, 0, cnt - 2);

                switch (dec_buff[0])
                {
                    case 0x00://RSA Packet
                        //handled above
                        break;
                    case 0x01://login fail or security card
                        if (dec_buff[1] == 0x1F)
                        {
                            #region security card
                            Globals.l2net_home.Add_Text("Sending security card packet", Globals.Red, TextType.BOT);

                            string seccard = "06 00 00 00 00 38 8C F0 41 6F 69 D6 25 EA A7 F8 82 64 18 81 EB E0 33 30 73 E4 92 75 1C 7F FF 71 26 36 99 BB CD AD 79 AC CA 27 F0 47 0C 5E 12 72 AD 42 24 96 86 52 82 63 C4 77 AC 5C FF B9 95 82 8B 64 3F F8 AC 52 61 90 AF 5E 26 AF A7 29 1B 71 49 9E 70 E8 CD B2 13 31 5D 70 32 9B 7C 98 BA 8A D1 B9 28 4D 86 CD F6 BE 85 15 E4 29 6D 5A 75 1A F5 D9 CE 32 1D C3 11 57 E1 4E 9E E9 CF 34 BD 3E E9 E5 A8 EC CD 00 00 00 8E A5 0A 82 00 00 00 00 00 00 00 00 00 00 00 00";
                            seccard = seccard.Replace(" ", "");

                            byte[] Bytes = new byte[seccard.Length / 2];
                            int[] HexValue = new int[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09,
                                 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0A, 0x0B, 0x0C, 0x0D,
                                 0x0E, 0x0F };

                            for (int x = 0, i = 0; i < seccard.Length; i += 2, x += 1)
                            {
                                Bytes[x] = (byte)(HexValue[Char.ToUpper(seccard[i + 0]) - '0'] << 4 |
                                                  HexValue[Char.ToUpper(seccard[i + 1]) - '0']);
                            }


                            byte[] sec_unenc = new byte[152];

                            bfeng.init(true, Globals.gamedata.blow_key);
                            bfeng.processBigBlock(Bytes, 0, sec_unenc, 0, 152);

                            byte[] sec = new byte[154];
                            sec[0] = 0x9A;
                            sec[1] = 0x00;
                            sec_unenc.CopyTo(sec, 2);
                            Globals.Login_GameSocket.Send(sec, 0, 154, System.Net.Sockets.SocketFlags.None);
                            break;
                            #endregion
                        }
                        else if (dec_buff[1] == 0x02)
                        {
                            Globals.l2net_home.Add_Text("Wrong password", Globals.Red, TextType.BOT);
                            oog_login = false;
                            Globals.Login_State = 0;
                            break;
                        }
                        else if (dec_buff[1] == 0x03)
                        {
                            Globals.l2net_home.Add_Text("Wrong username or password", Globals.Red, TextType.BOT);
                            oog_login = false;
                            Globals.Login_State = 0;
                            break;
                        }
                        else if (dec_buff[1] == 0x07)
                        {
                            Globals.l2net_home.Add_Text("Account already in use", Globals.Red, TextType.BOT);
                            oog_login = false;
                            Globals.Login_State = 0;
                            break;
                        }
                        else if (dec_buff[1] == 0x0F)
                        {
                            Globals.l2net_home.Add_Text("Server overloaded", Globals.Red, TextType.BOT);
                            oog_login = false;
                            Globals.Login_State = 0;
                            break;
                        }
                        else if (dec_buff[1] == 0x10)
                        {
                            Globals.l2net_home.Add_Text("Server maintenance", Globals.Red, TextType.BOT);
                            oog_login = false;
                            Globals.Login_State = 0;
                            break;
                        }
                        else
                        {
                            Globals.l2net_home.Add_Text("login fail", Globals.Red, TextType.BOT);
                            oog_login = false;
                            Globals.Login_State = 0;
                            break;
                        }
                    case 0x03://login ok
                        Globals.l2net_home.Add_Text("login ok", Globals.Red, TextType.BOT);
                        #region LoginOK
                        send = new byte[32];

                        send[0] = 0x05;
                        send[1] = dec_buff[1];
                        send[2] = dec_buff[2];
                        send[3] = dec_buff[3];
                        send[4] = dec_buff[4];
                        send[5] = dec_buff[5];
                        send[6] = dec_buff[6];
                        send[7] = dec_buff[7];
                        send[8] = dec_buff[8];
                        send[9] = 0x04;
                        send[10] = 0x00;
                        send[11] = 0x00;
                        send[12] = 0x00;
                        send[13] = 0x00;
                        send[14] = 0x00;
                        send[15] = 0x00;

                        Globals.gamedata.login_ok[0] = dec_buff[1];
                        Globals.gamedata.login_ok[1] = dec_buff[2];
                        Globals.gamedata.login_ok[2] = dec_buff[3];
                        Globals.gamedata.login_ok[3] = dec_buff[4];
                        Globals.gamedata.login_ok[4] = dec_buff[5];
                        Globals.gamedata.login_ok[5] = dec_buff[6];
                        Globals.gamedata.login_ok[6] = dec_buff[7];
                        Globals.gamedata.login_ok[7] = dec_buff[8];

                        NewCrypt.appendChecksum(send, 0, 20);

                        sende = new byte[32];

                        bfeng.init(true, Globals.gamedata.blow_key);
                        bfeng.processBigBlock(send, 0, sende, 0, 32);

                        pack_out = new byte[34];

                        pack_out[0] = 0x22;
                        pack_out[1] = 0x00;
                        sende.CopyTo(pack_out, 2);

                        Globals.Login_GameSocket.Send(pack_out, 0, 34, System.Net.Sockets.SocketFlags.None);
                        break;
                        #endregion
                    case 0x04://serverlist
                        #region ServerList
                        //parse the 208 packet
                        int Login_ServerCount = dec_buff[1];//number of servers

                        int offset = 3;

                        int m = 21;

                        Globals.Login_Servers = new System.Collections.SortedList();
                        Globals.Login_Servers.Capacity = Login_ServerCount;

                        for (int i = 0; i < Login_ServerCount; i++)
                        {
                            try
                            {
                                Server n_sev = new Server();
                                n_sev.ID = (uint)System.Convert.ToSByte(dec_buff[0 + offset + i * m]);
                                n_sev.IP = ((int)dec_buff[1 + offset + i * m]).ToString() + "." + ((int)dec_buff[2 + offset + i * m]).ToString() + "." + ((int)dec_buff[3 + offset + i * m]).ToString() + "." + ((int)dec_buff[4 + offset + i * m]).ToString();
                                n_sev.Port = System.BitConverter.ToInt32(dec_buff, 5 + offset + i * m);
                                Globals.Login_Servers.Add(n_sev.ID, n_sev);
                            }
                            catch
                            {

                            }
                        }

                        Globals.login_window.FillServerInfo(dec_buff);
                        break;
                        #endregion
                    case 0x06://play fail
                        oog_login = false;
                        Globals.Login_State = 0;
                        break;
                    case 0x07://play ok
                        #region PlayOK
                        Globals.gamedata.play_ok[0] = dec_buff[1];
                        Globals.gamedata.play_ok[1] = dec_buff[2];
                        Globals.gamedata.play_ok[2] = dec_buff[3];
                        Globals.gamedata.play_ok[3] = dec_buff[4];
                        Globals.gamedata.play_ok[4] = dec_buff[5];
                        Globals.gamedata.play_ok[5] = dec_buff[6];
                        Globals.gamedata.play_ok[6] = dec_buff[7];
                        Globals.gamedata.play_ok[7] = dec_buff[8];
                        //Globals.gamedata.play_ok[8] = dec_buff[9];

                        oog_login = false;
                        Util.KillServerLoginConnections();

                        LoginServer.PlayOKProcess();
                        break;
                        #endregion
                    case 0x0B://gameguard check verified from server
                        //Query (42): 0B 5B 87 04 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 9E 9D 66 76 E6 16 6D BD 45 C9 28 C0 66 C5 27 A1 BC 4E C6 
                        //Reply (322): 00 7C 78 FD 38 EA AB B6 AE 4F CC 91 C5 24 18 3B 5D 8A 42 BD D0 3B F5 F1 BE 5D 7C 75 4F 08 48 60 E4 23 C7 B0 11 59 CA D5 1C 2A 62 3D D9 50 C8 43 27 0B E1 54 F2 1D EF 8C F5 F2 D2 F4 39 1A 87 5E 9A FA D4 04 10 46 12 8C B5 59 84 AA 23 1C FD 89 69 58 FB 8D 16 3F C8 FA CE 5D BD 36 57 A7 15 B8 BE 91 8D CF 82 D9 FF 83 B9 AB 55 0F 1F 7F 2E 54 A7 55 E1 D8 F4 D4 A5 12 2C 70 19 1D 87 F5 65 1E 4D 5B F4 42 2C 14 F3 DD 16 F3 F0 B5 53 F8 F2 5E 53 99 DC A7 1F 1F BD DE A8 51 94 AD 6E 65 A3 B7 18 54 1B 32 61 CD CF E8 1B E2 0E 24 5A 2B 16 1E 32 61 77 CB 91 C9 A6 A2 2B 80 AA 44 9A BC 22 62 E4 7C A0 45 61 CD F0 60 73 D3 D0 CC C3 44 70 B0 FE FF 6D B8 BD 83 74 8D 53 26 D6 93 50 B3 7B AA 05 8A B7 95 C3 56 4A DC 7C 9C 2E EB A4 6F 42 4D E0 F5 C4 68 E0 7A 9D D7 D7 FD FF 6D D7 21 A3 9A 02 5B 87 04 00 23 01 00 00 67 45 00 00 AB 89 00 00 EF CD 00 00 08 00 00 00 00 00 00 40 4F 61 4F 21 19 3C 62 2A 6A 8B 2A 3B 3D 7C EF 00 00 00 00 03 C8 56 FB 00 00 00 00 00 00 00 00 00 00 00 00 
                        #region GameGuard verify reply
                        Globals.l2net_home.Add_Text("login info - packing username/pw", Globals.Red, TextType.BOT);

                        byte[] login_info_user = new byte[128];
                        byte[] login_info_pass = new byte[128];

                        if (Globals.UserName.Length > 50)
                            Globals.l2net_home.Add_Error("username is too long");
                        if (Globals.Password.Length > 16)
                            Globals.l2net_home.Add_Error("password is too long");

                        //24 thingy?
                        //login_info[0x5B] = 0x24;

                        //pack the username
                        for (int i = 0; i < Globals.UserName.Length; i++)
                        {
                            login_info_user[0x4E + i] = (byte)Globals.UserName[i];
                        }
                        //pack the password
                        for (int i = 0; i < Globals.Password.Length; i++)
                        {
                            login_info_pass[0x5C + i] = (byte)Globals.Password[i];
                        }

                        Globals.l2net_home.Add_Text("login info - rsa start", Globals.Red, TextType.BOT);

                        byte[] Exponent = { 1, 0, 1 };

                        //Create a new instance of RSAParameters.
                        System.Security.Cryptography.RSAParameters RSAKeyInfo = new System.Security.Cryptography.RSAParameters();

                        //Set RSAKeyInfo to the public key values. 
                        RSAKeyInfo.Modulus = enckey;
                        RSAKeyInfo.Exponent = Exponent;

                        RSAManaged poo = new RSAManaged();
                        poo.ImportParameters(RSAKeyInfo);

                        byte[] outb = new byte[128];
                        byte[] outc = new byte[128];

                        outb = poo.EncryptValue(login_info_user);
                        outc = poo.EncryptValue(login_info_pass);

                        Globals.l2net_home.Add_Text("login info - rsa end", Globals.Red, TextType.BOT);

                        byte[] login_send = new byte[320];
                        byte[] login_sende = new byte[320];

                        outb.CopyTo(login_send, 1);
                        outc.CopyTo(login_send, 129);
                        //need to put the other 40bytes here

                        Globals.l2net_home.Add_Text("login info - gameguard start", Globals.Red, TextType.BOT);

                        //old
                        //45 00 01 1E 37 A2 F5 00 00 00 00 00 00 00 00 00
                        //new (TODO need to check this)
                        //23 92 90 4D 18 30 B5 7C 96 61 41 47 05 07 96 FB
                        //23 01 00 00 67 45 00 00 AB 89 00 00 EF CD 00 00 - 1057
                        login_send[257] = sess[0];
                        login_send[258] = sess[1];
                        login_send[259] = sess[2];
                        login_send[260] = sess[3];

                        byte[] query = new byte[16];
                        for (int ii = 0; ii < 16; ii++)
                        {
                            query[ii] = dec_buff[5 + ii];
                        }

                        string gg = "";
                        for (int i = 0; i < query.Length; i++)
                        {
                            gg += query[i].ToString("X2");
                        }

                        if (Globals.GG_List.ContainsKey(gg))
                        {
                            Globals.l2net_home.Add_Text("login info - gameguard known query... sending known reply", Globals.Red, TextType.BOT);
                            byte[] reply = (byte[])Globals.GG_List[gg];

                            //start at byte 5 is the gg query
                            login_send[261] = reply[0];//gameguard reply start
                            login_send[262] = reply[1];
                            login_send[263] = reply[2];
                            login_send[264] = reply[3];
                            login_send[265] = reply[4];//
                            login_send[266] = reply[5];
                            login_send[267] = reply[6];
                            login_send[268] = reply[7];
                            login_send[269] = reply[8];//
                            login_send[270] = reply[9];
                            login_send[271] = reply[10];
                            login_send[272] = reply[11];
                            login_send[273] = reply[12];//
                            login_send[274] = reply[13];
                            login_send[275] = reply[14];
                            login_send[276] = reply[15];//game guard reply stop
                        }
                        else
                        {
                            Globals.l2net_home.Add_Text("login info - gameguard UNknown query... sending reply for blank query...", Globals.Red, TextType.BOT);

                            login_send[261] = 0x23;//gameguard reply start
                            login_send[262] = 0x01;
                            login_send[263] = 0x00;
                            login_send[264] = 0x00;
                            login_send[265] = 0x67;//
                            login_send[266] = 0x45;
                            login_send[267] = 0x00;
                            login_send[268] = 0x00;
                            login_send[269] = 0xAB;//
                            login_send[270] = 0x89;
                            login_send[271] = 0x00;
                            login_send[272] = 0x00;
                            login_send[273] = 0xEF;//
                            login_send[274] = 0xCD;
                            login_send[275] = 0x00;
                            login_send[276] = 0x00;//game guard reply stop
                        }

                        login_send[277] = 0x08;//08 00 00 00 00 00 00

                        //40 4F 61 4F 21 19 3C 62 
                        login_send[284] = 0x40;
                        login_send[285] = 0x4F;
                        login_send[286] = 0x61;
                        login_send[287] = 0x4F;
                        login_send[288] = 0x21;
                        login_send[289] = 0x19;
                        login_send[290] = 0x3C;
                        login_send[291] = 0x62;


                        //2A 6A 8B 2A 3B 3D 7C EF 
                        login_send[292] = 0x2A;
                        login_send[293] = 0x6A;
                        login_send[294] = 0x8B;
                        login_send[295] = 0x2A;
                        login_send[296] = 0x3B;
                        login_send[297] = 0x3D;
                        login_send[298] = 0x7C;
                        login_send[299] = 0xEF;
                        //00 00 00 00 
                        //03 C8 56 FB 
                        login_send[304] = 0x03;
                        login_send[305] = 0xC8;
                        login_send[306] = 0x56;
                        login_send[307] = 0xFB;
                        //00 00 00 00 00 00 00 00 00 00 00 00 

                        //login_send[150] = 0x00;
                        //login_send[151] = 0x00;
                        //login_send[152] = 0x00;
                        //login_send[153] = 0x00;//
                        //login_send[154] = 0x00;
                        //login_send[155] = 0x00;
                        //login_send[156] = 0x00;
                        //login_send[157] = 0x00;//
                        //login_send[158] = 0x00;
                        //login_send[159] = 0x00;

                        Globals.l2net_home.Add_Text("login info - gameguard end/checksum start", Globals.Red, TextType.BOT);

                        NewCrypt.appendChecksum(login_send, 0, 308);

                        Globals.l2net_home.Add_Text("login info - checksum end", Globals.Red, TextType.BOT);

                        //need to encode with blowfish
                        bfeng.init(true, Globals.gamedata.blow_key);
                        bfeng.processBigBlock(login_send, 0, login_sende, 0, 320);

                        Globals.l2net_home.Add_Text("login info - blowfish done", Globals.Red, TextType.BOT);

                        byte[] login_send2 = new byte[322];
                        login_send2[0] = 0x42;
                        login_send2[1] = 0x01;

                        login_sende.CopyTo(login_send2, 2);

                        Globals.l2net_home.Add_Text("login info - sending login info", Globals.Red, TextType.BOT);

                        //this line sends the login data
                        Globals.Login_GameSocket.Send(login_send2, 0, 322, System.Net.Sockets.SocketFlags.None);

                        Globals.l2net_home.Add_Text("login info - login info sent", Globals.Red, TextType.BOT);
                        break;
                        #endregion
                }
            }
#if !DEBUG
			}
			catch
			{
				Globals.l2net_home.Add_Error("crash: OOG Login thread");
			}
#endif
        }

        public static void OOG_SelectServer()
        {
            Globals.l2net_home.Add_Text("Encoding and sending server select", Globals.Red, TextType.BOT);

            byte[] buff = new byte[32];
            byte[] buffe = new byte[32];

            buff[0] = 0x02;
            buff[1] = Globals.gamedata.login_ok[0];
            buff[2] = Globals.gamedata.login_ok[1];
            buff[3] = Globals.gamedata.login_ok[2];
            buff[4] = Globals.gamedata.login_ok[3];
            buff[5] = Globals.gamedata.login_ok[4];
            buff[6] = Globals.gamedata.login_ok[5];
            buff[7] = Globals.gamedata.login_ok[6];
            buff[8] = Globals.gamedata.login_ok[7];
            buff[9] = System.Convert.ToByte(((Server)Globals.Login_Servers[Globals.Login_SelectedServer]).ID);
            buff[10] = 0x00;
            buff[11] = 0x00;
            buff[12] = 0x00;
            buff[13] = 0x00;
            buff[14] = 0x00;
            buff[15] = 0x00;

            NewCrypt.appendChecksum(buff, 0, 20);

            BlowfishEngine bfeng = new BlowfishEngine();
            bfeng.init(true, Globals.gamedata.blow_key);
            bfeng.processBigBlock(buff, 0, buffe, 0, 32);

            byte[] pack_out = new byte[34];

            pack_out[0] = 0x22;
            pack_out[1] = 0x00;
            buffe.CopyTo(pack_out, 2);

            Globals.Login_GameSocket.Send(pack_out, 0, 34, System.Net.Sockets.SocketFlags.None);
        }

        public static void PlayOKProcess()
        {
            try
            {
                OpenGameServerConnection();

                byte[] buff = LoginServer.BuildAuthLogin(Globals.gamedata.Protocol);

                Globals.Game_GameSocket.Send(buff, 0, 267, System.Net.Sockets.SocketFlags.None);

                //the other stuff should be handled by our packet handlers...
                //lets start our threads up

                Globals.gamedata.running = true;

                LoginServer.Start_Threads();
            }
            catch
            {
                Globals.l2net_home.Add_Error("play ok process fail");
            }
        }

        public static void CreateClientGameLink()
        {
            //connection to the client - game server tunnel
            Globals.Game_ClientSocket = Globals.Game_ClientLink.AcceptSocket();

            Globals.Game_ClientSocket.NoDelay = true;
            Globals.Game_ClientSocket.ReceiveTimeout = Globals.TimeOut;
            Globals.Game_ClientSocket.SendTimeout = Globals.TimeOut;
            Globals.Game_ClientSocket.SendBufferSize = Globals.BUFFER_NETWORK;
            Globals.Game_ClientSocket.ReceiveBufferSize = Globals.BUFFER_NETWORK;
        }

        public static void CreateClientLoginLink()
        {
            //connection to the client - login server tunnel
            Globals.Login_ClientSocket = Globals.Login_ClientLink.AcceptSocket();

            Globals.Login_ClientSocket.NoDelay = true;
            Globals.Login_ClientSocket.ReceiveTimeout = Globals.TimeOut;
            Globals.Login_ClientSocket.SendTimeout = Globals.TimeOut;
            Globals.Login_ClientSocket.SendBufferSize = Globals.BUFFER_NETWORK;
            Globals.Login_ClientSocket.ReceiveBufferSize = Globals.BUFFER_NETWORK;
        }

        public static void OpenGameServerConnection()
        {
            Globals.l2net_home.Add_Text("bot -> gameserver : opening", Globals.Red, TextType.BOT);
            if (Globals.gamedata.Override_GameServer)
            {

                Globals.gamedata.Game_IP = Globals.gamedata.Override_Game_IP;
                Globals.gamedata.Game_Port = Globals.gamedata.Override_Game_Port;

            }
            else
            {
                if (Globals.proxy_serv)
                { // lazy to do it in a normal way ... :P
                    Globals.gamedata.Game_IP = Globals.proxy_serv_ip[0].ToString() + "." + Globals.proxy_serv_ip[1].ToString() + "." + Globals.proxy_serv_ip[2].ToString() + "." + Globals.proxy_serv_ip[3].ToString();
                    Globals.gamedata.Game_Port = System.BitConverter.ToUInt16(Globals.proxy_serv_port, 0);
                }
                else
                {

                    Globals.gamedata.Game_IP = ((Server)Globals.Login_Servers[Globals.Login_SelectedServer]).IP; //((int)_servers[0 + offset]).ToString() + "." + ((int)_servers[1 + offset]).ToString() + "." + ((int)_servers[2 + offset]).ToString() + "." + ((int)_servers[3 + offset]).ToString();
                    Globals.gamedata.Game_Port = ((Server)Globals.Login_Servers[Globals.Login_SelectedServer]).Port;//System.BitConverter.ToUInt32(_servers,4+offset);
                }
            }
            
            if (Globals.gamedata.UseProxy_GameServer)
            {
                try
                {
                    Globals.Game_GameSocket = LMKR.SocksProxy.ConnectToSocks5Proxy(Globals.gamedata.Proxy_IP, (ushort)Globals.gamedata.Proxy_Port, Globals.gamedata.Game_IP, (ushort)Globals.gamedata.Game_Port, Globals.gamedata.Proxy_UserName, Globals.gamedata.Proxy_Password);
                }
                catch (Exception e)
                {
                    Globals.l2net_home.Add_Text(e.Message);
                }
            }
            else
            {
                Globals.Game_GameSocket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
                Globals.Game_GameSocket.Connect(Globals.gamedata.Game_IP, Globals.gamedata.Game_Port);
            }

            Globals.Game_GameSocket.NoDelay = true;
            Globals.Game_GameSocket.ReceiveTimeout = Globals.TimeOut;
            Globals.Game_GameSocket.SendTimeout = Globals.TimeOut;
            Globals.Game_GameSocket.SendBufferSize = Globals.BUFFER_NETWORK;
            Globals.Game_GameSocket.ReceiveBufferSize = Globals.BUFFER_NETWORK;

            Globals.l2net_home.Add_Text("bot -> gameserver : connected", Globals.Red, TextType.BOT);
        }

        public static void OpenLoginServerConnection()
        {
            Globals.l2net_home.Add_Text("bot -> loginserver : opening", Globals.Red, TextType.BOT);

            if (Globals.gamedata.UseProxy_LoginServer)
            {
                try
                {
                    Globals.Login_GameSocket = LMKR.SocksProxy.ConnectToSocks5Proxy(Globals.gamedata.Proxy_IP, (ushort)Globals.gamedata.Proxy_Port, Globals.gamedata.Login_IP, (ushort)Globals.gamedata.Login_Port, Globals.gamedata.Proxy_UserName, Globals.gamedata.Proxy_Password);
                }
                catch (Exception e)
                {
                    Globals.l2net_home.Add_Text(e.Message);
                }
            }
            else
            {
                Globals.Login_GameSocket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Stream, System.Net.Sockets.ProtocolType.Tcp);
                Globals.Login_GameSocket.Connect(Globals.gamedata.Login_IP, Globals.gamedata.Login_Port);
            }

            Globals.Login_GameSocket.NoDelay = true;
            Globals.Login_GameSocket.ReceiveTimeout = Globals.TimeOut;
            Globals.Login_GameSocket.SendTimeout = Globals.TimeOut;
            Globals.Login_GameSocket.SendBufferSize = Globals.BUFFER_NETWORK;
            Globals.Login_GameSocket.ReceiveBufferSize = Globals.BUFFER_NETWORK;

            Globals.l2net_home.Add_Text("bot -> loginserver : connected", Globals.Red, TextType.BOT);
        }
    }
}
