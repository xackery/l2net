using System;
using System.Net.Sockets;

namespace L2_login
{
	public class BroadcastThread
	{
		private System.Threading.Thread readthread;

		public BroadcastThread()
		{
			readthread = new System.Threading.Thread(new System.Threading.ThreadStart(BroadcastReadThread));

			readthread.IsBackground = true;

            readthread.Start();
		}

		private void BroadcastReadThread()
		{
			try
			{
                System.Net.IPEndPoint LocalIpEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Any, Globals.UDP_Port);//we want to receive on all ips for our set port
                System.Net.IPEndPoint RemoteIpEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Broadcast, 0);//ghetto port shit... tells us where it came from
                
                UdpClient udp_receive = new UdpClient();

                udp_receive.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
                udp_receive.ExclusiveAddressUse = false;

                udp_receive.Client.Bind(LocalIpEndPoint);

				byte[] buff = new byte[1024];
				NetPacket np = new NetPacket();

				while(true == true)//Globals.gamedata.running)
				{
                    //this is a blocking call... hence no need to sleep ever
                    buff = udp_receive.Receive(ref RemoteIpEndPoint);
					np.Parse(buff);

                    switch ((NetPacketType)np.Type)
					{
                        case NetPacketType.Update://standard cp/hp/mp update
							BroadCastStautsUpdate(np);
							break;
                        case NetPacketType.Script://script net send
                            ScriptNetEvent(np);
                            break;
                        case NetPacketType.ScriptBB://script net send
                            ScriptNetEventBB(np);
                            break;
                        case NetPacketType.NPCUpdate:
                            BroadCastStautsUpdateNPC(np);
                            break;
                    }
				}

                //udp_receive.Close();
			}
			catch
			{
				Globals.l2net_home.Add_Error("NetError - local broadcast read thread failed");
			}
		}

        private void ScriptNetEvent(NetPacket np)
        {
            if (Globals.gamedata.CurrentScriptState == ScriptState.Running)
            {
                ScriptEvent sc_ev = new ScriptEvent();
                sc_ev.Type = EventType.UDPReceive;
                sc_ev.Variables.Add(new ScriptVariable(np.Sender, "SENDER", Var_Types.STRING, Var_State.PUBLIC));
                sc_ev.Variables.Add(new ScriptVariable((long)np.SenderID, "SENDERID", Var_Types.INT, Var_State.PUBLIC));
                sc_ev.Variables.Add(new ScriptVariable(np.Name, "STRING1", Var_Types.STRING, Var_State.PUBLIC));
                sc_ev.Variables.Add(new ScriptVariable((long)np.Param1, "PARAM1", Var_Types.INT, Var_State.PUBLIC));
                sc_ev.Variables.Add(new ScriptVariable((long)np.Param2, "PARAM2", Var_Types.INT, Var_State.PUBLIC));
                sc_ev.Variables.Add(new ScriptVariable((long)np.Param3, "PARAM3", Var_Types.INT, Var_State.PUBLIC));
                sc_ev.Variables.Add(new ScriptVariable((long)np.Param4, "PARAM4", Var_Types.INT, Var_State.PUBLIC));
                sc_ev.Variables.Add(new ScriptVariable(System.DateTime.Now.Ticks, "TIMESTAMP", Var_Types.INT, Var_State.PUBLIC));
                ScriptEngine.SendToEventQueue(sc_ev);
            }
        }

        private void ScriptNetEventBB(NetPacket np)
        {
            if (Globals.gamedata.CurrentScriptState == ScriptState.Running)
            {
                ScriptEvent sc_ev = new ScriptEvent();
                sc_ev.Type = EventType.UDPReceiveBB;
                sc_ev.Variables.Add(new ScriptVariable(np.Sender, "SENDER", Var_Types.STRING, Var_State.PUBLIC));
                sc_ev.Variables.Add(new ScriptVariable((long)np.SenderID, "SENDERID", Var_Types.INT, Var_State.PUBLIC));
                sc_ev.Variables.Add(new ScriptVariable(np.BBuff, "BBUFF", Var_Types.BYTEBUFFER, Var_State.PUBLIC));
                sc_ev.Variables.Add(new ScriptVariable(System.DateTime.Now.Ticks,"TIMESTAMP",Var_Types.INT, Var_State.PUBLIC));
                ScriptEngine.SendToEventQueue(sc_ev);
            }
        }

		private void BroadCastStautsUpdate(NetPacket np)
		{
            //no need to update with data from ourselves or about ourselves
            if (np.SenderID == Globals.gamedata.my_char.ID || np.ID == Globals.gamedata.my_char.ID)
            {
            }
            else
            {
                Globals.PlayerLock.EnterReadLock();
                try
                {
                    //no need to update people in our immediate party (we will get those updates from the server)
                    //...but
                    //any update we recieve directly from a player in our party is probably going to be more up to date than the packet we recieved from the server
                    //so for now... let's actually use the packet...

                    CharInfo player = Util.GetChar(np.ID);

                    if (player != null)
                    {
                        player.Max_CP = np.MaxCP;
                        player.Cur_CP = np.CurCP;
                        player.Max_HP = np.MaxHP;
                        player.Cur_HP = np.CurHP;
                        player.Max_MP = np.MaxMP;
                        player.Cur_MP = np.CurMP;
                    }
                }//unlock
                finally
                {
                    Globals.PlayerLock.ExitReadLock();

                    if (np.ID == Globals.gamedata.my_char.TargetID)
                    {
                        AddInfo.Set_Target_HP();
                    }
                }
            }//end of else
		}

        private void BroadCastStautsUpdateNPC(NetPacket np)
        {
            //no need to update with data from ourselves or about ourselves
            if (np.SenderID == Globals.gamedata.my_char.ID || np.ID == Globals.gamedata.my_char.ID)
            {
            }
            else
            {
                Globals.NPCLock.EnterReadLock();
                try
                {
                    //no need to update people in our immediate party (we will get those updates from the server)
                    //...but
                    //any update we recieve directly from a player in our party is probably going to be more up to date than the packet we recieved from the server
                    //so for now... let's actually use the packet...

                    NPCInfo npc = Util.GetNPC(np.ID);

                    if (npc != null)
                    {
                        npc.Max_CP = np.MaxCP;
                        npc.Cur_CP = np.CurCP;
                        npc.Max_HP = np.MaxHP;
                        npc.Cur_HP = np.CurHP;
                        npc.Max_MP = np.MaxMP;
                        npc.Cur_MP = np.CurMP;
                    }
                }//unlock
                finally
                {
                    Globals.NPCLock.ExitReadLock();

                    if (np.ID == Globals.gamedata.my_char.TargetID)
                    {
                        AddInfo.Set_Target_HP();
                    }
                }
            }//end of else
        }

        public static void SendSelfStatus()
        {
            //TODO - need to update this to include pet info...
            NetPacket np = new NetPacket();
            np.Type = (uint)NetPacketType.Update;//regular old status update
            np.Sender = Globals.gamedata.my_char.Name;
            np.SenderID = Globals.gamedata.my_char.ID;
            np.Name = np.Sender;
            np.ID = np.SenderID;
            np.MaxCP = (uint)Globals.gamedata.my_char.Max_CP;
            np.CurCP = (uint)Globals.gamedata.my_char.Cur_CP;
            np.MaxHP = (uint)Globals.gamedata.my_char.Max_HP;
            np.CurHP = (uint)Globals.gamedata.my_char.Cur_HP;
            np.MaxMP = (uint)Globals.gamedata.my_char.Max_MP;
            np.CurMP = (uint)Globals.gamedata.my_char.Cur_MP;

            NetCode.NetSend(np.GetBytes());
        }

        public static void SendStatus(NetPacket np)
        {
            np.Sender = Globals.gamedata.my_char.Name;
            np.SenderID = Globals.gamedata.my_char.ID;

            NetCode.NetSend(np.GetBytes());
        }
	}//end of BroadCastThread class
}
