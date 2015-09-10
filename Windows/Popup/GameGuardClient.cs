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
    public partial class GameGuardClient : Form
    {
        public static byte[] m_dataBuffer = new byte[64];
        public static IAsyncResult m_result;
        public static AsyncCallback m_pfnCallBack;
        public static Socket m_clientSocket;
        public GameGuardClient()
        {
            InitializeComponent();
        }

        public static void pre_connect()
        {
            Globals.GG_Clientmode = true;
            try
            {

                m_clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // Set the remote IP address
                IPAddress ip = IPAddress.Parse(Globals.gamedata.GG_IP);
                int iPortNo = Globals.gamedata.GG_Port;
                // Create the end point 
                IPEndPoint ipEnd = new IPEndPoint(ip, iPortNo);
                // Connect to the remote host
                m_clientSocket.Connect(ipEnd);
                if (m_clientSocket.Connected)
                {
                    Globals.l2net_home.Add_Text(String.Format("Gameguard client connected to: {0}:{1}", Globals.gamedata.GG_IP.ToString(), Globals.gamedata.GG_Port.ToString()), Globals.Yellow, TextType.BOT);
                    //Wait for data asynchronously 
                    WaitForData();
                }

            }
            catch (SocketException se)
            {
                Globals.l2net_home.Add_Text("Connection failed, is the server running? Exception: " + se.Message, Globals.Red, TextType.BOT);
            }
        }

        private void button_gg_connect_Click(object sender, EventArgs e)
        {
            Globals.GG_Clientmode = true;
            try
            {

                m_clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                Globals.gamedata.GG_IP = textBox_gg_srv_ip.Text;
                Globals.gamedata.GG_Port = Util.GetInt32(textBox_gg_srv_port.Text);

                // Set the remote IP address
                IPAddress ip = IPAddress.Parse(Globals.gamedata.GG_IP);
                int iPortNo = Globals.gamedata.GG_Port;
                // Create the end point 
                IPEndPoint ipEnd = new IPEndPoint(ip, iPortNo);
                // Connect to the remote host
                m_clientSocket.Connect(ipEnd);
                if (m_clientSocket.Connected)
                {
                    Globals.l2net_home.Add_Text(String.Format("Connected to: {0}:{1}", textBox_gg_srv_ip.Text, textBox_gg_srv_port.Text), Globals.Yellow, TextType.BOT);
                    button_gg_connect.Enabled = false;
                    //Wait for data asynchronously 
                    WaitForData();
                    this.Close();
                }

            }
            catch (SocketException se)
            {
                Globals.l2net_home.Add_Text("Connection failed, is the server running? Exception: " + se.Message, Globals.Red, TextType.BOT);
            }
        }

        public static void WaitForData()
        {
            try
            {
                if (m_pfnCallBack == null)
                {
                    m_pfnCallBack = new AsyncCallback(OnDataReceived);
                }
                SocketPacket theSocPkt = new SocketPacket();
                theSocPkt.thisSocket = m_clientSocket;
                // Start listening to the data asynchronously
                m_result = m_clientSocket.BeginReceive(theSocPkt.dataBuffer,
                                                        0, theSocPkt.dataBuffer.Length,
                                                        SocketFlags.None,
                                                        m_pfnCallBack,
                                                        theSocPkt);
            }
            catch (SocketException se)
            {
                Globals.l2net_home.Add_Text("WaitForData, exception: " + se.Message, Globals.Red, TextType.BOT);
            }

        }
        public class SocketPacket
        {
            public System.Net.Sockets.Socket thisSocket;
            public byte[] dataBuffer = new byte[64];
        }

        public static void OnDataReceived(IAsyncResult asyn)
        {
            try
            {
                SocketPacket theSockId = (SocketPacket)asyn.AsyncState;
                int iRx = theSockId.thisSocket.EndReceive(asyn);
                byte[] message = new byte[iRx];

                Buffer.BlockCopy(theSockId.dataBuffer, 0, message, 0, iRx);
                string gg = "";

                for (int i = 0; i < message.Length; i++)
                {
                    gg += message[i].ToString("X2") + " ";
                }
                Globals.l2net_home.Add_Text("Data: " + gg, Globals.Yellow, TextType.BOT);
                Globals.l2net_home.Add_Text("Lenght: " + iRx.ToString(), Globals.Yellow, TextType.BOT);
                Globals.l2net_home.Add_Text("Received gameguard reply from GG server", Globals.Green, TextType.BOT);

                ByteBuffer bout = new ByteBuffer(message);
                Globals.gamedata.SendToGameServer(bout);

                WaitForData();
            }
            catch (SocketException se)
            {
                Globals.l2net_home.Add_Text("OnDataReceived, exception: " + se.Message, Globals.Red, TextType.BOT);
            }
        }

        public static void SendGGQuery(byte[] data)
        {
            try
            {
                if (m_clientSocket != null)
                {
                    m_clientSocket.Send(data);
                }
            }
            catch (SocketException se)
            {
                Globals.l2net_home.Add_Text("SendGGQuery, exception: " + se.Message, Globals.Red, TextType.BOT);
            }	
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] data = new byte[3];
                data[0] = 0x74;
                data[1] = 0x13;
                data[2] = 0x37;
                if (m_clientSocket != null)
                {
                    m_clientSocket.Send(data);
                }
            }
            catch (SocketException se)
            {
                Globals.l2net_home.Add_Text("SendGGQuery, exception: " + se.Message, Globals.Red, TextType.BOT);
            }	
        
        }

    }
}
