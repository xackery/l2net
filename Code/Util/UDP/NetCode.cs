using System;
using System.Net.Sockets;

namespace L2_login
{
	public class NetCode
	{
        static bool init = false;
        static Socket broadcast_sender;

		public NetCode()
		{
		}

        static private void Initialize()
        {
            try
            {
                init = true;

                broadcast_sender = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                System.Net.IPEndPoint RemoteIpEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Broadcast, Globals.UDP_Port);

                broadcast_sender.Connect(RemoteIpEndPoint);

                broadcast_sender.EnableBroadcast = true;
                broadcast_sender.DontFragment = true;
                broadcast_sender.MulticastLoopback = true;
            }
            catch
            {
                Globals.l2net_home.Add_Error("Netsend - failed to initialize broadcaster");
            }
        }

        static public void NetSend(byte[] buff)
        {
            //need to change this to just put the stuff on a queue
            //then pop off the queue and send across the network
            //probably need to add extra timing checks and crap so to keep only the most recent data
            //that is... if we care to send out hp packets for others around us
            if (!init)
            {
                Initialize();
            }

            try
            {
                broadcast_sender.Send(buff);
            }
            catch
            {
                Globals.l2net_home.Add_Error("Netsend - local broadcast failed");
            }
        }

        static public void NetSendIP(byte[] buff, string ip)
        {
            //need to change this to just put the stuff on a queue
            //then pop off the queue and send across the network
            //probably need to add extra timing checks and crap so to keep only the most recent data
            //that is... if we care to send out hp packets for others around us
            try
            {
                Socket ip_sender;

                ip_sender = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                System.Net.IPEndPoint RemoteIpEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Parse(ip), Globals.UDP_Port);

                ip_sender.Connect(RemoteIpEndPoint);

                ip_sender.DontFragment = true;
                ip_sender.Send(buff);
            }
            catch
            {
                Globals.l2net_home.Add_Error("Netsend - udp ip failed");
            }
        }
	}//end of class
}
