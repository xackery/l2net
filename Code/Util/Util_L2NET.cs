using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Compression;

namespace L2_login
{
    public static partial class Util
    {
        public static void KillThreads()
        {
            Stop_Threads();

            if (Globals.login_window != null && Globals.login_window.IsDisposed == false)
            {
                Globals.login_window.Close();
            }
            if (Globals.map_window != null && Globals.map_window.IsDisposed == false)
            {
                Globals.map_window.Close();
            }

            Util.ClearAllNearby();
            Util.ClearAllSelf();
        }

        public static void Setup_Threads()
        {
            Globals.Login_State = 0;
            Globals.Mixer = null;

            Globals.gamesocket_ready = false;
            Globals.clientsocket_ready = false;

            Globals.gamedata.CreateCrypt();

            Globals.oog_loginthread = new System.Threading.Thread(new System.Threading.ThreadStart(LoginServer.OOG_Login));

            Globals.ig_loginthread = new System.Threading.Thread(new System.Threading.ThreadStart(LoginServer.IG_Login));
            Globals.ig_listener = new System.Threading.Thread(new System.Threading.ThreadStart(LoginServer.IG_Listener));
            Globals.ig_Gamelistener = new System.Threading.Thread(new System.Threading.ThreadStart(LoginServer.IG_StartGameLinks));
            Globals.loginsendthread = new System.Threading.Thread(new System.Threading.ThreadStart(LoginServer.LoginSendThread));
            Globals.loginreadthread = new System.Threading.Thread(new System.Threading.ThreadStart(LoginServer.LoginReadThread));


            Globals.gameprocessdatathread = new System.Threading.Thread(new System.Threading.ThreadStart(GameServer.ProcessDataThread));

            Globals.gamedrawthread = new System.Threading.Thread(new System.Threading.ThreadStart(MapThread.DrawGameThread));

            Globals.gamethread = new ServerThread();
            Globals.clientthread = new ClientThread();

            Globals.botaithread = new BotAIThread();
            //Oddi: Follow Rest thread
            Globals.followrestthread = new FollowRestThread();
            Globals.scriptthread = new ScriptEngine();



            //this should mnake all the threads close once the foreground threads close
            Globals.oog_loginthread.IsBackground = true;

            Globals.ig_loginthread.IsBackground = true;
            Globals.ig_listener.IsBackground = true;
            Globals.ig_Gamelistener.IsBackground = true;
            Globals.loginsendthread.IsBackground = true;
            Globals.loginreadthread.IsBackground = true;

            Globals.gameprocessdatathread.IsBackground = true;

            Globals.gamedrawthread.IsBackground = true;
        }

        public static void Stop_Connections()
        {
            Globals.l2net_home.Add_Text("Killing all network connections");

            KillClientGameConnections();

            KillServerGameConnections();

            //////////////////////////////

            KillServerLoginConnections();

            KillClientLoginConnections();
        }

        public static void KillClientGameConnections()
        {
            try
            {
                if (Globals.Game_ClientLink != null)
                {
                    Globals.Game_ClientLink.Stop();
                }
            }
            catch//(Exception ex)
            {
                //Globals.l2net_home.Add_Error("Game_ClientLink : " + ex.Message);
            }

            Globals.Game_ClientLink = null;

            try
            {
                if (Globals.Game_ClientSocket != null)
                {
                    Globals.Game_ClientSocket.Close();
                }
            }
            catch//(Exception ex)
            {
                //Globals.l2net_home.Add_Error("Game_ClientSocket : " + ex.Message);
            }

            Globals.Game_ClientSocket = null;
        }

        public static void KillServerGameConnections()
        {
            try
            {
                if (Globals.Game_GameSocket != null)
                {
                    Globals.Game_GameSocket.Close();
                }
            }
            catch//(Exception ex)
            {
                //Globals.l2net_home.Add_Error("Game_GameLink : " + ex.Message);
            }

            Globals.Game_GameSocket = null;
        }

        public static void KillServerLoginConnections()
        {
            try
            {
                if (Globals.Login_GameSocket != null)
                {
                    Globals.Login_GameSocket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                    Globals.Login_GameSocket.Close();
                }
            }
            catch//(Exception ex)
            {
                //Globals.l2net_home.Add_Error("Login_GameLink : " + ex.Message);
            }

            Globals.Login_GameSocket = null;
        }

        public static void KillClientLoginConnections()
        {
            try
            {
                if (Globals.Login_ClientLink != null)
                {
                    Globals.Login_ClientLink.Stop();
                }
            }
            catch//(Exception ex)
            {
                //Globals.l2net_home.Add_Error("Login_ClientLink : " + ex.Message);
            }

            Globals.Login_ClientLink = null;

            try
            {
                if (Globals.Login_ClientSocket != null)
                {
                    Globals.Login_ClientSocket.Shutdown(System.Net.Sockets.SocketShutdown.Both);
                    Globals.Login_ClientSocket.Close();
                }
            }
            catch//(Exception ex)
            {
                //Globals.l2net_home.Add_Error("Login_ClientSocket : " + ex.Message);
            }

            Globals.Login_ClientSocket = null;
        }

        public static void Stop_Threads()
        {
            Globals.l2net_home.Add_Text("Killing all threads");

            try
            {
                if (Globals.oog_loginthread != null)
                    Globals.oog_loginthread.Abort();
            }
            catch//(Exception e)
            {
                //Globals.l2net_home.Add_Error("oog_loginthread : " + e.Message);
            } 
            
            try
            {
                if (Globals.ig_loginthread != null)
                    Globals.ig_loginthread.Abort();
            }
            catch//(Exception e)
            {
                //Globals.l2net_home.Add_Error("ig_loginthread : " + e.Message);
            }

            try
            {
                if (Globals.ig_listener != null)
                    Globals.ig_listener.Abort();
            }
            catch//(Exception e)
            {
                //Globals.l2net_home.Add_Error("ig_listener : " + e.Message);
            }

            try
            {
                if (Globals.ig_Gamelistener != null)
                    Globals.ig_Gamelistener.Abort();
            }
            catch//(Exception e)
            {
                //Globals.l2net_home.Add_Error("ig_Gamelistener : " + e.Message);
            }

            try
            {
                if (Globals.loginsendthread != null)
                    Globals.loginsendthread.Abort();
            }
            catch//(Exception e)
            {
                //Globals.l2net_home.Add_Error("loginsendthread : " + e.Message);
            }

            try
            {
                if (Globals.loginreadthread != null)
                    Globals.loginreadthread.Abort();
            }
            catch//(Exception e)
            {
                //Globals.l2net_home.Add_Error("loginreadthread : " + e.Message);
            }

            try
            {
                if (Globals.gameprocessdatathread != null)
                    Globals.gameprocessdatathread.Abort();
            }
            catch//(Exception e)
            {
                //Globals.l2net_home.Add_Error("gameprocessdatathread : " + e.Message);
            }

            try
            {
                if (Globals.gamedrawthread != null)
                    Globals.gamedrawthread.Abort();
            }
            catch//(Exception e)
            {
                //Globals.l2net_home.Add_Error("gamedrawthread : " + e.Message);
            }

            try
            {
                if (Globals.gamethread != null)
                    Globals.gamethread.readthread.Abort();
            }
            catch//(Exception e)
            {
                //Globals.l2net_home.Add_Error("gamethread.readthread : " + e.Message);
            }

            try
            {
                if (Globals.gamethread != null)
                    Globals.gamethread.sendthread.Abort();
            }
            catch//(Exception e)
            {
                //Globals.l2net_home.Add_Error("gamethread.sendthread : " + e.Message);
            }

            try
            {
                if (Globals.clientthread != null)
                    Globals.clientthread.readthread.Abort();
            }
            catch//(Exception e)
            {
                //Globals.l2net_home.Add_Error("clientthread.readthread : " + e.Message);
            }

            try
            {
                if (Globals.clientthread != null)
                    Globals.clientthread.sendthread.Abort();
            }
            catch//(Exception e)
            {
                //Globals.l2net_home.Add_Error("clientthread.sendthread : " + e.Message);
            }

            try
            {
                if (Globals.botaithread != null)
                    Globals.botaithread.botaithread.Abort();
            }
            catch//(Exception e)
            {
                //Globals.l2net_home.Add_Error("botaithread : " + e.Message);
            }

            //Oddi: FollowRestTHread
            try
            {
                if (Globals.followrestthread != null)
                    Globals.followrestthread.followrestthread.Abort();
            }
            catch//(Exception e)
            {
                //Globals.l2net_home.Add_Error("followrestthread : " + e.Message);
            }

            try
            {
                if (Globals.scriptthread != null)
                    Globals.scriptthread.scriptthread.Abort();
            }
            catch//(Exception e)
            {
                //Globals.l2net_home.Add_Error("scriptthread : " + e.Message);
            }
        }

        public static void Flush_TextFile()
        {
            if (Globals.text_out != null)
            {
                try
                {
#if DEBUG
                    Globals.gamedataout.Flush();
                    Globals.gamedatato.Flush();
                    Globals.clientdataout.Flush();
                    Globals.clientdatato.Flush();
#endif

                    Globals.text_out.Flush();
                }
                catch
                {
                    //error flushing
                }
            }
        }

        public static void Force_Collect()
        {
            Globals.l2net_home.Add_Text("*******************************");
            Globals.l2net_home.Add_Text("Handle count: " + System.Diagnostics.Process.GetCurrentProcess().HandleCount.ToString());
            Globals.l2net_home.Add_Text("Memory usage / allocated: " + GC.GetTotalMemory(false).ToString() + " / " + Environment.WorkingSet.ToString());
            Globals.l2net_home.Add_Text("Cleaning up memory... this may take a few moments");
            VoicePlayer.PlaySound(7);
            GC.Collect();
            Globals.l2net_home.Add_Text("Clean up complete!");
            Globals.l2net_home.Add_Text("Handle count: " + System.Diagnostics.Process.GetCurrentProcess().HandleCount.ToString());
            Globals.l2net_home.Add_Text("Memory usage / allocated: " + GC.GetTotalMemory(true).ToString() + " / " + Environment.WorkingSet.ToString());
            Globals.l2net_home.Add_Text("*******************************");
        }

        public static void Free_Assets()
        {
            Globals.gamedata.running = false;

            if (Globals.gameprocessdatathread != null && Globals.gameprocessdatathread.ThreadState == System.Threading.ThreadState.Running)
                Globals.gameprocessdatathread.Abort();
            if (Globals.gamedrawthread != null && Globals.gamedrawthread.ThreadState == System.Threading.ThreadState.Running)
                Globals.gamedrawthread.Abort();
            if (Globals.directinputthread != null && Globals.directinputthread.ThreadState == System.Threading.ThreadState.Running)
                Globals.directinputthread.Abort();
            Globals.pck_thread.stop();
            try
            {
#if DEBUG
                Globals.gamedatato.Flush();
                Globals.gamedataout.Flush();
                Globals.clientdatato.Flush();
                Globals.clientdataout.Flush();

                Globals.gamedatato.Close();
                Globals.gamedataout.Close();
                Globals.clientdatato.Close();
                Globals.clientdataout.Close();
#endif
                if (Globals.text_out != null)
                {
                    Globals.text_out.Flush();
                    Globals.text_out.Close();
                }
            }
            catch
            {
                //error closing something...oh well
            }

            if (Globals.Game_GameSocket != null)
            {
                Globals.Game_GameSocket.Close();
            }

            if (Globals.Game_ClientLink != null)
            {
                Globals.Game_ClientLink.Stop();
            }

            //GC.Collect();
        }

        public static void SayToClient(int index, string end)
        {
            uint type = 0;

            switch (index)
            {
                case 0://all 0x00
                    type = 0x00;
                    break;
                case 1://shout 0x01
                    type = 0x01;
                    break;
                case 2://tell player 0x02
                    type = 0x02;
                    break;
                case 3://party 0x03
                    type = 0x03;
                    break;
                case 4://clan 0x04
                    type = 0x04;
                    break;
                case 5://from gm 0x05
                    type = 0x05;
                    break;
                case 6://petition from gm 0x06
                    type = 0x06;
                    break;
                case 7://reply to petition from gm 0x07
                    type = 0x07;
                    break;
                case 8://trade 0x08
                    type = 0x08;
                    break;
                case 9://alliance 0x09
                    type = 0x09;
                    break;
                case 10://announcement 0x0A
                    type = 0x0A;
                    break;
                case 11://crash client 0x0B
                    type = 0x0B;
                    break;
                case 12://fake1 0x0C
                    type = 0x0C;
                    break;
                case 13://fake2 0x0D
                    type = 0x0D;
                    break;
                case 14://fake3 0x0E
                    type = 0x0E;
                    break;
                case 15://party room all 0x0F
                    type = 0x0F;
                    break;
                case 16://party room commander 0x10
                    type = 0x10;
                    break;
                case 17://hero 0x11
                    type = 0x11;
                    break;
            }

            string start = "=L2.Net=";

            //need to build a packet and send it out
            int startlen = start.Length * 2 + 2;
            int endlen = end.Length * 2;
            if (endlen > 0)
                endlen += 2;
            ByteBuffer bbuff;
            if (Globals.gamedata.Chron >= Chronicle.CT2_6)
            {
                bbuff = new ByteBuffer(13 + startlen + endlen);
            }
            else
            {
                bbuff = new ByteBuffer(9 + startlen + endlen);
            }
            bbuff.WriteByte((byte)PServer.CreatureSay);
            bbuff.WriteUInt32(0);
            bbuff.WriteUInt32(type);
            bbuff.WriteString(start);
            if (Globals.gamedata.Chron >= Chronicle.CT2_6)
            {
                bbuff.WriteInt32(-1);
            }
            bbuff.WriteString(end);

            Globals.gamedata.SendToClient(bbuff);
        }

        public static void Kill()
        {
            Util.KillThreads();
            System.Windows.Forms.Application.Exit();
        }
    }
}
