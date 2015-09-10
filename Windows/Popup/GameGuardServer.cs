using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace L2_login
{
    public partial class GameGuardServer : Form
    {

        const int MAX_CLIENTS = 500;

        public static AsyncCallback pfnWorkerCallBack;
        public static Socket m_mainSocket;
        public static Socket[] m_workerSocket = new Socket[MAX_CLIENTS];
        public static int m_clientCount = 0;

        public GameGuardServer()
        {
            InitializeComponent();
        }

        public static void pre_listen()
        {
            System.Net.IPAddress ipAd = System.Net.IPAddress.Parse(Globals.gamedata.GG_IP);

            Globals.GG_Servermode = true;
            try
            {
                // Create the listening socket...
                m_mainSocket = new Socket(AddressFamily.InterNetwork,
                                          SocketType.Stream,
                                          ProtocolType.Tcp);
                IPEndPoint ipLocal = new IPEndPoint(ipAd, Globals.gamedata.GG_Port);
                // Bind to local IP Address...
                m_mainSocket.Bind(ipLocal);
                // Start listening...
                m_mainSocket.Listen(4);
                // Create the call back for any client connections...
                m_mainSocket.BeginAccept(new AsyncCallback(OnClientConnect), null);

            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }

            Globals.l2net_home.Add_Text(String.Format("Gameguard server started on: {0}:{1}", Globals.gamedata.GG_IP, Globals.gamedata.GG_Port), Globals.Yellow, TextType.BOT);
        }

        private void button_gg_listen_Click(object sender, EventArgs e)
        {
            Globals.gamedata.GG_IP = textBox_gg_local_ip.Text;
            Globals.gamedata.GG_Port = Util.GetInt32(textBox_gg_local_port.Text);
            System.Net.IPAddress ipAd = System.Net.IPAddress.Parse(Globals.gamedata.GG_IP);

            Globals.GG_Servermode = true;
            try
            {
                // Create the listening socket...
                m_mainSocket = new Socket(AddressFamily.InterNetwork,
                                          SocketType.Stream,
                                          ProtocolType.Tcp);
                IPEndPoint ipLocal = new IPEndPoint(ipAd, Globals.gamedata.GG_Port);
                // Bind to local IP Address...
                m_mainSocket.Bind(ipLocal);
                // Start listening...
                m_mainSocket.Listen(4);
                // Create the call back for any client connections...
                m_mainSocket.BeginAccept(new AsyncCallback(OnClientConnect), null);

            }
            catch (SocketException se)
            {
                MessageBox.Show(se.Message);
            }

            button_gg_listen.Enabled = false;
            Globals.l2net_home.Add_Text(String.Format("Gameguard server started on: {0}:{1}", Globals.gamedata.GG_IP, Globals.gamedata.GG_Port), Globals.Yellow, TextType.BOT);
            this.Close();
        }


        public static void OnClientConnect(IAsyncResult asyn)
        {
            try
            {
                // Here we complete/end the BeginAccept() asynchronous call
                // by calling EndAccept() - which returns the reference to
                // a new Socket object
                m_workerSocket[m_clientCount] = m_mainSocket.EndAccept(asyn);
                // Let the worker Socket do the further processing for the 
                // just connected client
                WaitForData(m_workerSocket[m_clientCount]);
                // Now increment the client count
                ++m_clientCount;
                // Display this client connection as a status message on the GUI	
                String str = String.Format("Client # {0} connected", m_clientCount);
                Globals.l2net_home.Add_Text(str, Globals.Yellow, TextType.BOT);

                // Since the main Socket is now free, it can go back and wait for
                // other clients who are attempting to connect
                m_mainSocket.BeginAccept(new AsyncCallback(OnClientConnect), null);
            }
            catch (SocketException se)
            {
                Globals.l2net_home.Add_Text("OnClientConnect, exception: " + se.Message, Globals.Red, TextType.BOT);
            }

        }

        public class SocketPacket
        {
            public System.Net.Sockets.Socket m_currentSocket;
            public byte[] dataBuffer = new byte[64];
        }

        // Start waiting for data from the client
        public static void WaitForData(System.Net.Sockets.Socket soc)
        {
            try
            {
                if (pfnWorkerCallBack == null)
                {
                    // Specify the call back function which is to be 
                    // invoked when there is any write activity by the 
                    // connected client
                    pfnWorkerCallBack = new AsyncCallback(OnDataReceived);
                }
                SocketPacket theSocPkt = new SocketPacket();
                theSocPkt.m_currentSocket = soc;
                // Start receiving any data written by the connected client
                // asynchronously
                soc.BeginReceive(theSocPkt.dataBuffer, 0,
                                   theSocPkt.dataBuffer.Length,
                                   SocketFlags.None,
                                   pfnWorkerCallBack,
                                   theSocPkt);
            }
            catch (SocketException se)
            {
                Globals.l2net_home.Add_Text("Errror: Wait for data, exception: " + se.Message, Globals.Red, TextType.BOT);
            }

        }

        // This the call back function which will be invoked when the socket
        // detects any client writing of data on the stream
        public static void OnDataReceived(IAsyncResult asyn)
        {
            try
            {
                SocketPacket socketData = (SocketPacket)asyn.AsyncState;

                Globals.gamedata.GGsocket = socketData.m_currentSocket;

                int iRx = 0;
                // Complete the BeginReceive() asynchronous call by EndReceive() method
                // which will return the number of characters written to the stream 
                // by the client
                iRx = socketData.m_currentSocket.EndReceive(asyn);

                byte[] message = new byte[iRx];

                Buffer.BlockCopy(socketData.dataBuffer, 0, message, 0, iRx);

                string gg = "";

                for (int i = 0; i < message.Length; i++)
                {
                    gg += message[i].ToString("X2") + " ";
                }

                if (message[0] == (byte)PServer.GameGuardQuery)
                {
                    Globals.l2net_home.Add_Text("Data: " + gg, Globals.Yellow, TextType.BOT);
                    Globals.l2net_home.Add_Text("Lenght: " + iRx.ToString(), Globals.Yellow, TextType.BOT);
                    Globals.l2net_home.Add_Text("Received gameguard query from OOG client", Globals.Yellow, TextType.BOT);
                    Globals.GG_QueryReceived = true;

                    ByteBuffer bout = new ByteBuffer(message);
                    Globals.gamedata.SendToClient(bout);

                }

                // Continue the waiting for data on the Socket
                WaitForData(socketData.m_currentSocket);
            }
            catch (SocketException se)
            {
                Globals.l2net_home.Add_Text("Error: OnDataReceived, exception: " + se.Message, Globals.Red, TextType.BOT);
            }
        }

        public static void SendGGReply(byte[] data)
        {
            try
            {
                /*
                for (int i = 0; i < m_clientCount; i++)
                {
                    if (m_workerSocket[i] != null)
                    {
                        if (m_workerSocket[i].Connected)
                        {
                            m_workerSocket[i].Send(data);
                        }
                    }
                }*/

                if (Globals.gamedata.GGsocket != null)
                {
                    if (Globals.gamedata.GGsocket.Connected)
                    {
                        Globals.gamedata.GGsocket.Send(data);
                    }

                }
            }
            catch (SocketException se)
            {
                Globals.l2net_home.Add_Text("Error: SendGGReply, exception: " + se.Message, Globals.Red, TextType.BOT);
            }
        }

    }
}
