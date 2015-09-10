using System;

namespace L2_login
{
	/// <summary>
	/// Sends packets between the bot and the client
	/// </summary>
	public class ClientThread
	{
		public System.Threading.Thread sendthread;
		public System.Threading.Thread readthread;

		public ClientThread()
		{
            Init();
		}

        public void Init()
        {
            sendthread = new System.Threading.Thread(new System.Threading.ThreadStart(ClientSendThread));
            readthread = new System.Threading.Thread(new System.Threading.ThreadStart(ClientReadThread));

            sendthread.IsBackground = true;
            readthread.IsBackground = true;

            sendthread.Priority = System.Threading.ThreadPriority.Highest;
            readthread.Priority = System.Threading.ThreadPriority.Highest;
        }

        public void network_exception()
        {
            if (Globals.dc_on_ig_close)
            {
                Globals.l2net_home.Add_Error("Terminating all connections as due to break in client connection");
                Util.Stop_Connections();
            }

            if (Globals.dump_pbuff_on_ig_close)
            {
                ByteBuffer bbtmp;
                System.Text.StringBuilder dumpbuilder;

                for (int pkt = 0; pkt < Globals.gamedata.stc_buffer.Count; pkt++)
                {                    
                    bbtmp = (ByteBuffer)Globals.gamedata.stc_buffer.Pop();
                    // Globals.l2net_home.Add_Error("pkt: " + pkt + "bbtmp.length: " + bbtmp.Length());
                    dumpbuilder = new System.Text.StringBuilder();
                    int z = 0;
                    for (int i = 0; i < bbtmp.Length(); i++)
                    {
                        z = bbtmp.ReadByte();
                        dumpbuilder.Append(z.ToString("X2"));
                        dumpbuilder.Append(" ");
                    }
                    Globals.l2net_home.Add_Dump(dumpbuilder.ToString(), true);
                }
            }
        }

		private void ClientSendThread()
		{
			byte[] b2 = new byte[2];
			byte[] buff;
			byte[] buffe;

			ByteBuffer bbuffer0;

			try
			{
				while(Globals.gamedata.running && !Globals.gamedata.OOG)
				{
					while(Globals.gamedata.GetCount_DataToClient() > 0)
					{
                        bbuffer0 = null;

                        Globals.ClientSendQueueLock.EnterWriteLock();
                        try
                        {
                            bbuffer0 = (ByteBuffer)Globals.gamedata.clientsendqueue.Dequeue();
                        }
                        finally
                        {
                            Globals.ClientSendQueueLock.ExitWriteLock();
                        }

						buff = bbuffer0.Get_ByteArray();
                       /* if (Globals.pck_thread.pck_recording)
                        {
                           // lock (Globals.pck_thread.lock_obj)
                           // {
                                pck_window_dat pck_dat = new pck_window_dat(buff);
                                pck_dat.action = 1;
                                pck_dat.type = 2;
                                pck_dat.time = System.DateTime.Now.TimeOfDay.ToString();
                                Globals.pck_thread.mine_queue.Enqueue(pck_dat);
                           // }
                        }*/
#if DEBUG

						//need to output to the send to client log file
                        Globals.clientdatato.WriteLine(":::time:::" + System.DateTime.Now.TimeOfDay.ToString() + ":::");
                        Globals.clientdatato.WriteLine("-data from bot to client hex-");
						for (uint i = 0; i < buff.Length; i++)
						{
                            Globals.clientdatato.Write(buff[i].ToString("X2"));
                            Globals.clientdatato.Write(" ");
						}
                        Globals.clientdatato.WriteLine("");
                        Globals.clientdatato.WriteLine("-data from bot to client string-");
						for(uint i = 0; i < buff.Length; i++)
						{
                            Globals.clientdatato.Write((char)buff[i]);
						}
                        Globals.clientdatato.WriteLine("");
#endif

						Globals.gamedata.crypt_clientout.encrypt(buff);

						buffe = new byte[2+buff.Length];

						b2 = System.BitConverter.GetBytes((short)buffe.Length);
						buffe[0] = b2[0];
						buffe[1] = b2[1];;

                        buff.CopyTo(buffe, 2);

                        Globals.Game_ClientSocket.Send(buffe);//,0,buffe.Length);

						//release the crap we dont need anymore
						buff = null;
						buffe = null;
					}

                    System.Threading.Thread.Sleep(Globals.SLEEP_ClientSendThread);//sleep for 100 mili seconds; this is okay because a new send should wake the thread up
				}//end of while running
			}
            catch (System.Exception e)
            {
                Globals.l2net_home.Add_Error("crash: ClientSendThread : " + e.Message);
                network_exception();
            }
		}

		private void ClientReadThread()
		{
            byte[] buffread = new byte[Globals.BUFFER_MAX];
			byte[] buffpacket;
            byte[] buffpacketin;

			int cnt = 0;
			int size = 0;
            int ggcnt = 0;
            bool forward = true;

			ByteBuffer bbuffer0;
            System.Text.StringBuilder dumpbuilder;

			//Add_Text("Welcome to the game loop",Color.Red);

			try
			{
				while(Globals.gamedata.running && !Globals.gamedata.OOG)
				{
                    cnt += Globals.Game_ClientSocket.Receive(buffread, cnt, Globals.BUFFER_PACKET - cnt, System.Net.Sockets.SocketFlags.None);
					size = System.BitConverter.ToUInt16(buffread,0);

					while(cnt >= size && cnt > 2)
					{
						//if we got partial shit we cant use, read some more until it is full
						while(size > cnt)
						{
                            cnt += Globals.Game_ClientSocket.Receive(buffread, cnt, Globals.BUFFER_PACKET - cnt, System.Net.Sockets.SocketFlags.None);
						}

						buffpacketin = new byte[size - 2];
                        buffpacket = new byte[size - 2];

                        Array.Copy(buffread, 2, buffpacketin, 0, size - 2);

						Globals.gamedata.crypt_clientin.decrypt(buffpacketin);

                        Array.Copy(buffpacketin, 0, buffpacket, 0, size - 2);

                        if (Globals.Mixer != null)
                        {
                            Globals.Mixer.Decrypt0(buffpacket);
                        }
                        if (Globals.pck_thread.pck_recording)
                        {

                            pck_window_dat pck_dat = new pck_window_dat(buffpacket);
                            pck_dat.action = 1;
                            pck_dat.type = 1;
                            pck_dat.time = System.DateTime.Now.TimeOfDay.ToString();
                            Globals.pck_thread.mine_queue.Enqueue(pck_dat);
                             
                        }
#if DEBUG
                        try
                        {
                            //
                           /* if (Globals.pck_thread.pck_recording)
                             {
                            // *                             lock (Globals.pck_thread.lock_obj)
                            // {
                                 pck_window_dat pck_dat = new pck_window_dat(buffpacket);
                                 pck_dat.action = 1;
                                 pck_dat.type = 1;
                                 pck_dat.time = System.DateTime.Now.TimeOfDay.ToString();
                                 Globals.pck_thread.mine_queue.Enqueue(pck_dat);
                             //* }
                             }*/
                            //
                            Globals.clientdataout.WriteLine("packet...-size: " + size.ToString() + " -count:" + cnt.ToString() + " :::time:::" + System.DateTime.Now.TimeOfDay.ToString() + ":::");
                            Globals.clientdataout.WriteLine("-data from client to bot hex-");
                            for (uint i = 0; i < size - 2; i++)
                            {
                                Globals.clientdataout.Write(buffpacket[i].ToString("X2"));
                                Globals.clientdataout.Write(" ");
                            }
                            Globals.clientdataout.WriteLine("");
                            Globals.clientdataout.WriteLine("-data from client to bot string-");
                            for (uint i = 0; i < size - 2; i++)
                            {
                                Globals.clientdataout.Write((char)buffpacket[i]);
                            }
                            Globals.clientdataout.WriteLine("");
                        }
                        catch
                        {
                            //failed to write... oh well
                        }
#endif

                        if (Globals.DumpModeClient)
                        {
                            dumpbuilder = new System.Text.StringBuilder();

                            for (int i = 0; i < size - 2; i++)
                            {
                                dumpbuilder.Append(buffpacket[i].ToString("X2"));
                                dumpbuilder.Append(" ");
                            }
                            Globals.l2net_home.Add_Dump(dumpbuilder.ToString(), false);
                        }

						//shift the data over by size
						for(uint i = 0; i < cnt - size; i ++)
						{
							buffread[i] = buffread[size + i];
						}

						cnt -= size;

						if(buffpacket.Length > 0)
						{
                            forward = true;

                            if (Globals.gamedata.CurrentScriptState == ScriptState.Running)
                            {
                                if ((PClient)buffpacket[0] == PClient.EXPacket)
                                {
                                    if (ScriptEngine.Blocked_ClientPacketsEX.ContainsKey(System.Convert.ToInt32(buffpacket[1])))
                                    {
                                        forward = false;
                                    }

                                    if (ScriptEngine.ClientPacketsEXContainsKey(System.Convert.ToInt32(buffpacket[1])))
                                    {
                                        ByteBuffer bb = new ByteBuffer(buffpacket);

                                        ScriptEvent sc_ev = new ScriptEvent();
                                        sc_ev.Type = EventType.ClientPacketEX;
                                        sc_ev.Type2 = System.Convert.ToInt32(buffpacket[1]);
                                        sc_ev.Variables.Add(new ScriptVariable(bb, "PACKET", Var_Types.BYTEBUFFER, Var_State.PUBLIC));
                                        sc_ev.Variables.Add(new ScriptVariable(System.DateTime.Now.Ticks, "TIMESTAMP", Var_Types.INT, Var_State.PUBLIC));
                                        ScriptEngine.SendToEventQueue(sc_ev);
                                    }
                                }
                                else
                                {
                                    if (ScriptEngine.Blocked_ClientPackets.ContainsKey(System.Convert.ToInt32(buffpacket[0])))
                                    {
                                        forward = false;
                                    }

                                    if (ScriptEngine.ClientPacketsContainsKey(System.Convert.ToInt32(buffpacket[0])))
                                    {
                                        ByteBuffer bb = new ByteBuffer(buffpacket);

                                        ScriptEvent sc_ev = new ScriptEvent();
                                        sc_ev.Type = EventType.ClientPacket;
                                        sc_ev.Type2 = System.Convert.ToInt32(buffpacket[0]);
                                        sc_ev.Variables.Add(new ScriptVariable(bb, "PACKET", Var_Types.BYTEBUFFER, Var_State.PUBLIC));
                                        sc_ev.Variables.Add(new ScriptVariable(System.DateTime.Now.Ticks, "TIMESTAMP", Var_Types.INT, Var_State.PUBLIC));
                                        ScriptEngine.SendToEventQueue(sc_ev);
                                    }
                                }
                            }

							//this is where we would want to handle packets sent by the client
                            switch ((PClient)buffpacket[0])
							{
                                case PClient.AuthLogin://protocol version
                                    int prot = System.BitConverter.ToInt16(buffpacket, 1);
                                    Globals.l2net_home.Add_Text("protocol version: " + prot.ToString(), Globals.Red, TextType.BOT);

                                    if (prot == -2)
                                    {
                                        Globals.l2net_home.Add_Text("-2 protocol... just a ping... we've been had XD...", Globals.Red, TextType.BOT);
                                    }
                                    else
                                    {
                                        //valid protocol...
                                        Globals.l2net_home.Add_Text("valid protocol...", Globals.Red, TextType.BOT);
                                    }
                                    break;
								case PClient.GameGuardReply://gameguard reply
                                    if (Globals.GG_Servermode)
                                    {
                                        if (Globals.GG_QueryReceived)
                                        {
                                            ggcnt++;
                                            if (ggcnt == 1)
                                            {
                                                //GameGuardServer.send_gg_answer(buffpacket, false);
                                                GameGuardServer.SendGGReply(buffpacket);
                                            }
                                            
                                            if (ggcnt == 2)
                                            {
                                                Globals.GG_QueryReceived = false;
                                                //GameGuardServer.send_gg_answer(buffpacket, true);
                                                GameGuardServer.SendGGReply(buffpacket);
                                                ggcnt = 0;
                                            }
                                            forward = false;
                                        }
                                    }
                                    if (Globals.Script_Debugging)
                                    {
                                        string gg = "";

                                        for (int i = 1; i < buffpacket.Length; i++)
                                        {
                                            gg += buffpacket[i].ToString("X2") + " ";
                                        }

                                        Globals.l2net_home.Add_Debug("GameGuard Reply: " + gg);
                                    }
									break;
								case PClient.Say2://say text
									if(Globals.gamedata.OOG == false)
									{
                                        ByteBuffer bbuff = new ByteBuffer(buffpacket);

                                        bbuff.ReadByte();
                                        string start = bbuff.ReadString();
                                        uint type = bbuff.ReadUInt32();
                                        string end = bbuff.ReadString();
                                        
                                        //wanna handle text if prefaced with "-"
                                        //since these are commands to the bot

                                        if (start.StartsWith("--"))
                                        {
                                            forward = false;

                                            start = start.Substring(1, start.Length - 1);

                                            ServerPackets.Send_Text(type, start, end);
                                        }
                                        else if (start.StartsWith("-"))
                                        {
                                            forward = false;

                                            start = start.Substring(1, start.Length - 1);

                                            if (Globals.gamedata.CurrentScriptState == ScriptState.Running)
                                            {
                                                ScriptEvent sc_ev = new ScriptEvent();
                                                sc_ev.Type = EventType.ChatToBot;
                                                sc_ev.Variables.Add(new ScriptVariable((long)type, "MESSAGETYPE", Var_Types.INT, Var_State.PUBLIC));
                                                sc_ev.Variables.Add(new ScriptVariable(end, "TARGETNAME", Var_Types.STRING, Var_State.PUBLIC));
                                                sc_ev.Variables.Add(new ScriptVariable(start, "MESSAGE", Var_Types.STRING, Var_State.PUBLIC));
                                                ScriptEngine.SendToEventQueue(sc_ev);
                                            }
                                        }
									}
									break;
                                case PClient.CharacterSelect:
                                    if ((!Globals.gamedata.OOG) && (!System.String.IsNullOrEmpty(Globals.SecurityPin)) && (Globals.gamedata.SecurityPinOldClient) && (!Globals.gamedata.SecurityPinSent))
                                    {
                                        ServerPackets.RequestSecurityPinWindow();

                                        while (!Globals.gamedata.SecurityPinWindow)
                                        {
                                            System.Threading.Thread.Sleep(100);
                                        }

                                        Globals.l2net_home.Add_Text("Sending security pin: " + Globals.SecurityPin, Globals.Green, TextType.BOT);

                                        ServerPackets.SecurityPin();

                                        while (!Globals.gamedata.SecurityPinOk)
                                        {
                                            System.Threading.Thread.Sleep(100);
                                        }
                                        Globals.l2net_home.Add_Text("Pin OK", Globals.Green, TextType.BOT);
                                        Globals.gamedata.SecurityPinSent = true;
                                    }
                                    break;
							}

                            if (forward)
                            {
                                //send packets from the client right to the server
                                bbuffer0 = new ByteBuffer(buffpacketin);

                                Globals.gamedata.SendToGameServerNF(bbuffer0);
                            }
						}

						if(cnt > 2)
							size = System.BitConverter.ToUInt16(buffread,0);
					}//end of while loop
				}//end of while running
			}
            catch (System.Exception e)
            {
                Globals.l2net_home.Add_Error("crash: ClientReadThread : " + e.Message);
                network_exception();
            }
		}//end of read data
	}//end of clientsendthread
}
