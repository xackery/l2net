using System;

namespace L2_login
{
	/// <summary>
    /// Sends packets between the bot and the server
	/// </summary>
	public class ServerThread
	{
		public System.Threading.Thread readthread;
		public System.Threading.Thread sendthread;

		public ServerThread()
		{
            Init();
		}

        public void Init()
        {
            sendthread = new System.Threading.Thread(new System.Threading.ThreadStart(GameSendThread));
            readthread = new System.Threading.Thread(new System.Threading.ThreadStart(GameReadThread));

            sendthread.IsBackground = true;
            readthread.IsBackground = true;

            sendthread.Priority = System.Threading.ThreadPriority.Highest;
            readthread.Priority = System.Threading.ThreadPriority.Highest;
        }

		private void GameSendThread()
		{
			byte[] b2 = new byte[2];
			byte[] buff;
			byte[] buffe;

			ByteBuffer bbuffer0;
			try
			{
				while(Globals.gamedata.running)
				{
					while(Globals.gamedata.GetCount_DataToServer() > 0)
					{
						bbuffer0 = null;

                        Globals.GameSendQueueLock.EnterWriteLock();
                        try
                        {
                            bbuffer0 = (ByteBuffer)Globals.gamedata.gamesendqueue.Dequeue();
                        }
                        finally
                        {
                            Globals.GameSendQueueLock.ExitWriteLock();
                        }

    					buff = bbuffer0.Get_ByteArray();
                        
                        /*if (Globals.pck_thread.pck_recording)
                       {
                            //lock (Globals.pck_thread.lock_obj)
                           // {
                                pck_window_dat pck_dat = new pck_window_dat(buff);
                                pck_dat.action = 1;
                                pck_dat.type = 1;
                                pck_dat.time = System.DateTime.Now.TimeOfDay.ToString();
                                Globals.pck_thread.mine_queue.Enqueue(pck_dat);
                           // }
                        }*/
                         
#if DEBUG

						//need to output to the send to game file
                        Globals.gamedatato.WriteLine(" :::time:::" + System.DateTime.Now.TimeOfDay.ToString() + ":::");
                        Globals.gamedatato.WriteLine("-data from bot to gameserver hex-");
                        for (uint i = 0; i < buff.Length; i++)
						{
                            Globals.gamedatato.Write(buff[i].ToString("X2"));
                            Globals.gamedatato.Write(" ");
						}
                        Globals.gamedatato.WriteLine("");
                        Globals.gamedatato.WriteLine("-data from bot to gameserver string-");
                        for (int i = 0; i < buff.Length; i++)
						{
                            Globals.gamedatato.Write((char)buff[i]);
						}
                        Globals.gamedatato.WriteLine("");
#endif

                        /*if (L2NET.Mixer != null)
                        {
                            L2NET.Mixer.Encrypt0(buff);
                        }*/

						Globals.gamedata.crypt_out.encrypt(buff);

						buffe = new byte[2+buff.Length];

						b2 = System.BitConverter.GetBytes(buffe.Length);
						buffe[0] = b2[0];
						buffe[1] = b2[1];

                        buff.CopyTo(buffe, 2);

                        Globals.Game_GameSocket.Send(buffe, 0, buffe.Length, System.Net.Sockets.SocketFlags.None);
					}

					System.Threading.Thread.Sleep(Globals.SLEEP_GameSendThread);//sleep for 100 mili seconds; this is okay because a new send should wake the thread up
				}//end of while running
			}
            catch (System.Exception e)
            {
                Globals.l2net_home.Add_Error("crash: GameSendThread : " + e.Message);
            }
		}

		private void GameReadThread()
		{
            byte[] buffread = new byte[Globals.BUFFER_MAX];
			byte[] buffpacket;

			int cnt = 0;
			int size = 0;
            bool handle = true;
            bool forward = true;
            string tmp;

			ByteBuffer bbuffer0;
			ByteBuffer bbuffer1;
            ByteBuffer bbtmp;
			ByteBuffer bbtmp1;
            
            uint b0 = 0; //byte

            System.Text.StringBuilder dumpbuilder;

            System.Collections.ArrayList names;

            Globals.l2net_home.Add_Text("Welcome to the game loop", Globals.Red, TextType.BOT);

			try
			{
				while(Globals.gamedata.running)
				{
                    cnt += Globals.Game_GameSocket.Receive(buffread, cnt, Globals.BUFFER_PACKET - cnt, System.Net.Sockets.SocketFlags.None);
                    size = System.BitConverter.ToUInt16(buffread, 0);

                       
                        while (cnt >= size && cnt > 2)
                        {
                            //if we got partial shit we cant use, read some more until it is full
                            while (size > cnt)
                            {
                                cnt += Globals.Game_GameSocket.Receive(buffread, cnt, Globals.BUFFER_PACKET - cnt, System.Net.Sockets.SocketFlags.None);
                            }

                            while (size < 2)
                            {
                                Globals.l2net_home.Add_Text("L2J : tiny packet - start", Globals.Red, TextType.BOT);
                                if (!Globals.gamedata.OOG)
                                {
                                    Globals.Game_ClientSocket.Send(buffread, cnt, System.Net.Sockets.SocketFlags.None);
                                }
                                cnt = 0;
                                Globals.l2net_home.Add_Text("L2J : tiny packet - finished", Globals.Red, TextType.BOT);
                                cnt = Globals.Game_GameSocket.Receive(buffread, 0, Globals.BUFFER_PACKET, System.Net.Sockets.SocketFlags.None);
                                size = System.BitConverter.ToUInt16(buffread, 0);
                            }

                            buffpacket = new byte[size - 2];

                            Array.Copy(buffread, 2, buffpacket, 0, size - 2);

                            Globals.gamedata.crypt_in.decrypt(buffpacket);
                            if (Globals.pck_thread.pck_recording)
                            {
                                //*                             lock (Globals.pck_thread.lock_obj)
                                //{
                                pck_window_dat pck_dat = new pck_window_dat(buffpacket);
                                pck_dat.action = 1;
                                pck_dat.type = 2;
                                pck_dat.time = System.DateTime.Now.TimeOfDay.ToString();
                                Globals.pck_thread.mine_queue.Enqueue(pck_dat);
                                //* }
                            }
#if DEBUG

                            //need to output to the send to gameserver log file
                            Globals.gamedataout.WriteLine("-size: " + size.ToString() + " -count:" + cnt.ToString() + " :::time:::" + System.DateTime.Now.TimeOfDay.ToString() + ":::");
                            Globals.gamedataout.WriteLine("-data from game server to bot hex-");
                            for (uint i = 0; i < size - 2; i++)
                            {
                                Globals.gamedataout.Write(buffpacket[i].ToString("X2"));
                                Globals.gamedataout.Write(" ");
                            }
                            Globals.gamedataout.WriteLine("");
                            Globals.gamedataout.WriteLine("-data from game server to bot string-");
                            for (uint i = 0; i < size - 2; i++)
                            {
                                Globals.gamedataout.Write((char)buffpacket[i]);
                            }
                            Globals.gamedataout.WriteLine("");
#endif

                            if (Globals.DumpModeServer)
                            {
                                dumpbuilder = new System.Text.StringBuilder();

                                for (int i = 0; i < size - 2; i++)
                                {
                                    dumpbuilder.Append(buffpacket[i].ToString("X2"));
                                    dumpbuilder.Append(" ");
                                }

                                Globals.l2net_home.Add_Dump(dumpbuilder.ToString(), true);
                            }

                            //shift the data over by size
                            for (uint i = 0; i < cnt - size; i++)
                            {
                                buffread[i] = buffread[size + i];
                            }

                            cnt -= size;

                            if (buffpacket.Length > 0)
                            {
                                handle = true;
                                forward = true;

                                bbuffer0 = new ByteBuffer(buffpacket);

                                if (Globals.gamedata.CurrentScriptState == ScriptState.Running)
                                {
                                    if ((PServer)buffpacket[0] == PServer.EXPacket)
                                    {
                                        if (ScriptEngine.Blocked_ServerPacketsEX.ContainsKey(System.Convert.ToInt32(buffpacket[1])))
                                        {
                                            forward = false;
                                        }
                                    }
                                    else
                                    {
                                        if (ScriptEngine.Blocked_ServerPackets.ContainsKey(System.Convert.ToInt32(buffpacket[0])))
                                        {
                                            forward = false;
                                        }
                                    }
                                }

                                switch ((PServer)buffpacket[0])
                                {
                                    case PServer.VersionCheck:
                                        handle = false;
                                        forward = false;

                                        if (!Globals.gamedata.ManualGameKey)
                                        {
                                            Globals.gamedata.game_key[0] = buffpacket[2];
                                            Globals.gamedata.game_key[1] = buffpacket[3];
                                            Globals.gamedata.game_key[2] = buffpacket[4];
                                            Globals.gamedata.game_key[3] = buffpacket[5];
                                            Globals.gamedata.game_key[4] = buffpacket[6];//0xa1;
                                            Globals.gamedata.game_key[5] = buffpacket[7];//0x6c;
                                            Globals.gamedata.game_key[6] = buffpacket[8];//0x54;
                                            Globals.gamedata.game_key[7] = buffpacket[9];//0x87
                                            Globals.gamedata.game_key[8] = 0xc8;
                                            Globals.gamedata.game_key[9] = 0x27;
                                            Globals.gamedata.game_key[10] = 0x93;
                                            Globals.gamedata.game_key[11] = 0x01;
                                            Globals.gamedata.game_key[12] = 0xa1;
                                            Globals.gamedata.game_key[13] = 0x6c;
                                            Globals.gamedata.game_key[14] = 0x31;
                                            Globals.gamedata.game_key[15] = 0x97;
                                            //buff[10] - d 0x01
                                            //buff[14] - d 0x01
                                            //buff[18] - c 0x00
                                            //buff[19] - d new packet encryption
                                            //buff[20] - 
                                            //buff[21] - 
                                            //buff[22] - 
                                            Globals.l2net_home.Add_Text("gameserver - got packet encryption key", Globals.Red, TextType.BOT);
                                        }


                                        // ugly
                                        bbtmp1 = new ByteBuffer(buffpacket);

                                        bbtmp1.ReadByte();
                                        bbtmp1.ReadByte();
                                        bbtmp1.ReadInt32();
                                        bbtmp1.ReadInt32();
                                        bbtmp1.ReadInt32();
                                        Globals.gamedata.Server_ID = bbtmp1.ReadInt32();
                                        bbtmp1.ReadByte();
                                        Globals.gamedata.Obfuscation_Key = bbtmp1.ReadInt32();

                                        if (Globals.gamedata.OOG)
                                        {
                                            //don't need to send this packet anywhere...
                                        }
                                        else
                                        {
                                            //need to send this here... so it dones't get encrypted
                                            byte[] key_send = new byte[2 + buffpacket.Length];

                                            byte[] b2 = System.BitConverter.GetBytes((short)key_send.Length);
                                            key_send[0] = b2[0];
                                            key_send[1] = b2[1];

                                            buffpacket.CopyTo(key_send, 2);

                                            Globals.Game_ClientSocket.Send(key_send);
                                            Globals.l2net_home.Add_Text("gameserver - keys forwarded to client", Globals.Red, TextType.BOT);
                                            if (Globals.gamedata.ManualGameKey)
                                            {
                                                Globals.l2net_home.Add_Text("Start gamekey setup", Globals.Red, TextType.BOT);
                                                ManualGameKey setkey = new ManualGameKey();
                                                setkey.ShowDialog();
                                            }
                                        }

                                        if (Globals.gamedata.Chron >= Chronicle.CT1_5)
                                        {
                                            if (buffpacket.Length > 18)
                                            {
                                                try
                                                {
                                                    Globals.Mixer = new MixedPackets(System.BitConverter.ToInt32(buffpacket, 19));
                                                }
                                                catch
                                                {
                                                    //Globals.l2net_home.Add_Text("gameserver - Negative client ID encryption key... going to continue", Globals.Red, TextType.BOT);
                                                }
                                                finally
                                                {
                                                    Globals.l2net_home.Add_Text("gameserver - got client ID encryption key", Globals.Red, TextType.BOT);
                                                }
                                            }
                                            else
                                            {
                                                Globals.l2net_home.Add_Text("gameserver - missing client ID encryption key... going to continue", Globals.Red, TextType.BOT);
                                            }
                                        }

                                        //setup the key
                                        Globals.gamedata.crypt_in.setKey(Globals.gamedata.game_key);
                                        Globals.gamedata.crypt_out.setKey(Globals.gamedata.game_key);
                                        Globals.gamedata.crypt_clientin.setKey(Globals.gamedata.game_key);
                                        Globals.gamedata.crypt_clientout.setKey(Globals.gamedata.game_key);

                                        Globals.gamedata.logged_in = true;

                                        if (Globals.gamedata.OOG)
                                        {
                                            //send our request player list packet
                                            int startlen = Globals.UserName.Length * 2 + 2;
                                            ByteBuffer send;

                                            //CT2.3 increased length by 4
                                            if (Globals.gamedata.Chron > Chronicle.CT2_2)
                                            {
                                                send = new ByteBuffer(startlen + 33); //CT 2.3 and above
                                            }
                                            else if (Globals.gamedata.Chron < Chronicle.CT2_1)
                                            {
                                                send = new ByteBuffer(startlen + 21); //CT 1.5 and below
                                            }
                                            else
                                            {
                                                send = new ByteBuffer(startlen + 29); //CT 2.1, 2.2 (?)
                                            }

                                            send.WriteByte((byte)PClient.RequestPlayerList);
                                            send.WriteString(Globals.UserName);

                                            send.WriteByte(Globals.gamedata.play_ok[4]);
                                            send.WriteByte(Globals.gamedata.play_ok[5]);
                                            send.WriteByte(Globals.gamedata.play_ok[6]);
                                            send.WriteByte(Globals.gamedata.play_ok[7]);

                                            send.WriteByte(Globals.gamedata.play_ok[0]);
                                            send.WriteByte(Globals.gamedata.play_ok[1]);
                                            send.WriteByte(Globals.gamedata.play_ok[2]);
                                            send.WriteByte(Globals.gamedata.play_ok[3]);

                                            send.WriteByte(Globals.gamedata.login_ok[0]);
                                            send.WriteByte(Globals.gamedata.login_ok[1]);
                                            send.WriteByte(Globals.gamedata.login_ok[2]);
                                            send.WriteByte(Globals.gamedata.login_ok[3]);

                                            send.WriteByte(Globals.gamedata.login_ok[4]);
                                            send.WriteByte(Globals.gamedata.login_ok[5]);
                                            send.WriteByte(Globals.gamedata.login_ok[6]);
                                            send.WriteByte(Globals.gamedata.login_ok[7]);

                                            send.WriteByte(0x01);
                                            send.WriteByte(0x00);
                                            send.WriteByte(0x00);
                                            send.WriteByte(0x00);

                                            //15 44 34 92 1D 00 00 00 -different?
                                            //3C 01 00 00 00 00 00 00 - CT2.3 prot 83
                                            //D8 02 00 00 00 00 00 00 - CT 2.3 new
                                            //D8 02 00 00 00 00 00 00 - CT 2.4 (same as 2.3 new)
                                            //3C 01 00 00 00 00 00 00 00 00 00 00 - CT 2.6
                                            //69 02 00 00 00 00 00 00 00 00 00 00 - CT 3.0

                                            if (Globals.gamedata.Chron == Chronicle.CT3_0)
                                            {
                                                send.WriteByte(0x69);
                                                send.WriteByte(0x02);
                                                send.WriteByte(0x00);
                                                send.WriteByte(0x00);
                                                send.WriteByte(0x00);
                                                send.WriteByte(0x00);
                                                send.WriteByte(0x00);
                                                send.WriteByte(0x00);
                                                /*
                                                send.WriteByte(0x00);
                                                send.WriteByte(0x00);
                                                send.WriteByte(0x00);
                                                send.WriteByte(0x00);*/

                                            }

                                            else if (Globals.gamedata.Chron == Chronicle.CT2_6)
                                            {
                                                send.WriteByte(0x3C); //0x3C
                                                send.WriteByte(0x01); //0x01
                                                send.WriteByte(0x00);
                                                send.WriteByte(0x00);
                                                send.WriteByte(0x00);
                                                send.WriteByte(0x00);
                                                send.WriteByte(0x00);
                                                send.WriteByte(0x00);

                                            }

                                            else if (Globals.gamedata.Chron > Chronicle.CT1_5)
                                            {
                                                send.WriteByte(0xD8); //0x3C
                                                send.WriteByte(0x02); //0x01
                                                send.WriteByte(0x00);
                                                send.WriteByte(0x00);
                                                send.WriteByte(0x00);
                                                send.WriteByte(0x00);
                                                send.WriteByte(0x00);
                                                send.WriteByte(0x00);
                                            }

                                            if (Globals.gamedata.Chron == Chronicle.CT2_3)
                                            {
                                                send.WriteByte(0x01); //CT 2.3 0x01, CT 2.4 0x00 //CT 2.6 0x00 //CT3.0 0x00
                                            }

                                            //old
                                            //send.WriteByte(0xc0);
                                            //send.WriteByte(0xa8);
                                            //send.WriteByte(0x01);
                                            //send.WriteByte(0x64);

                                            Globals.gamedata.SendToGameServer(send);
                                        }
                                        break;
                                    case PServer.GameGuardQuery: //Don't forward gameguard packets to client if GG client is activated, but forward them to bot
                                        if (Globals.GG_Clientmode)
                                        {
                                            handle = true;
                                            forward = false;
                                        }
                                        break;
                                    case PServer.AskJoinParty://request party invite
                                        if (Globals.gamedata.botoptions.AcceptParty == 1)
                                        {
                                            bbtmp = new ByteBuffer(buffpacket);
                                            bbtmp.ReadByte();
                                            tmp = bbtmp.ReadString().ToUpperInvariant();

                                            names = Util.GetArray(Globals.gamedata.botoptions.AcceptPartyNames);

                                            if (names.Contains(tmp))
                                            {
                                                //accept party invite
                                                ServerPackets.JoinPartyReply(true);
                                                handle = false;
                                                forward = false;
                                            }
                                        }
                                        break;
                                    case PServer.ConfirmDlg://request rezz invite
                                        int sent = 0;
                                            bbtmp = new ByteBuffer(buffpacket);
                                            bbtmp.ReadByte();
                                            Globals.LastRezz1 = bbtmp.ReadUInt32();
                                            bbtmp.ReadInt32();
                                            bbtmp.ReadInt32();
                                            tmp = bbtmp.ReadString().ToUpperInvariant();
                                            switch (bbtmp.ReadInt32())
                                            {
                                                case 0://string
                                                    bbtmp.ReadString();
                                                    break;
                                                case 1://number
                                                    bbtmp.ReadInt32();
                                                    break;
                                                case 2://npc name
                                                    bbtmp.ReadUInt32();//GetNPCName
                                                    break;
                                                case 3://item name
                                                    bbtmp.ReadUInt32();//GetItemName
                                                    break;
                                                case 4://skill name
                                                    bbtmp.ReadUInt32();//id - 2037
                                                    bbtmp.ReadUInt32();//level - 1
                                                    break;
                                                case 5://poop
                                                    bbtmp.ReadUInt32();//
                                                    break;
                                                case 6://number double
                                                    bbtmp.ReadInt64();
                                                    break;
                                                case 7://zone name
                                                    bbtmp.ReadInt32();//x
                                                    bbtmp.ReadInt32();//y
                                                    bbtmp.ReadInt32();//z
                                                    break;
                                                case 8://augmented item
                                                    bbtmp.ReadUInt32();//item type id
                                                    bbtmp.ReadUInt32();//the augment data?
                                                    break;
                                                default://poop
                                                    bbtmp.ReadUInt32();//
                                                    break;
                                            }
                                            bbtmp.ReadUInt32();
                                            Globals.LastRezz2 = bbtmp.ReadUInt32();
                                            if (Globals.gamedata.botoptions.AcceptRez == 1)
                                            {
                                            names = Util.GetArray(Globals.gamedata.botoptions.AcceptRezNames);

                                            if (names.Contains(tmp))
                                            {
                                                //accept party invite
                                                ServerPackets.DialogReply(true);
                                                handle = false;
                                                forward = false;
                                            }
                                        }
                                        foreach (CharInfo player in Globals.gamedata.nearby_chars.Values)
                                        {
                                            if (player.Name == tmp && player.ClanID == Globals.gamedata.my_char.ClanID && Globals.gamedata.botoptions.AcceptRezClan == 1 && sent == 0)
                                            {
                                                ServerPackets.DialogReply(true);
                                                handle = false;
                                                forward = false;
                                                sent = 1;
                                            }
                                            if (player.Name == tmp && player.AllyID == Globals.gamedata.my_char.AllyID && Globals.gamedata.botoptions.AcceptRezAlly == 1 && sent == 0)
                                            {
                                                ServerPackets.DialogReply(true);
                                                handle = false;
                                                forward = false;
                                                sent = 1;
                                            }
                                        }
                                        if (Globals.gamedata.botoptions.AcceptRezParty == 1 && sent == 0)
                                        {
                                            //accept rez from party
                                            int found = 0;

                                            //loop through each member
                                            foreach (PartyMember pl in Globals.gamedata.PartyMembers.Values)
                                            {
                                                if (String.Equals(pl.Name, tmp, StringComparison.OrdinalIgnoreCase))
                                                {
                                                    found = 1;
                                                }
                                            }
                                            if (found == 1)
                                            {
                                                ServerPackets.DialogReply(true);
                                                handle = false;
                                                forward = false;
                                                sent = 1;
                                            }
                                        }
                                        break;
                                    case PServer.Earthquake:
                                        if (Globals.Suppress_Quakes)
                                        {
                                            forward = false;
                                            break;
                                        }
                                        break;
                                    case PServer.TargetSelected:
                                        if (Globals.lagfilter_TargetSelected)
                                        {
                                            bbtmp = new ByteBuffer(buffpacket);
                                            bbtmp.ReadByte();
                                            uint source = bbtmp.ReadUInt32();
                                            uint dest = bbtmp.ReadUInt32();

                                            forward = false;

                                            // Party Member targeting something ?
                                            if (Globals.gamedata.PartyMembers.ContainsKey(source))
                                            {
                                                forward = true;
                                            }
                                            else
                                            {
                                                // Somebody targeting a partymember or me?
                                                if ((Globals.gamedata.PartyMembers.ContainsKey(dest)) || (dest == Globals.gamedata.my_char.ID))
                                                {
                                                    forward = true;
                                                }
                                            }
                                        }
                                        break;
                                    case PServer.TargetUnselected:
                                        if (Globals.lagfilter_TargetUnselected)
                                        {
                                            bbtmp = new ByteBuffer(buffpacket);
                                            bbtmp.ReadByte();
                                            uint source = bbtmp.ReadUInt32();

                                            TargetType target_type = Util.GetType(source);

                                            if (target_type == TargetType.PLAYER)
                                            {
                                                forward = false;
                                                Globals.PlayerLock.EnterReadLock();
                                                try
                                                {
                                                    CharInfo player = Util.GetChar(source);

                                                    if (player != null)
                                                    {
                                                        // Was this person who broke target originally on a party member/me ?
                                                        if ((Globals.gamedata.PartyMembers.ContainsKey(player.TargetID)) ||
                                                           ((player.TargetID == Globals.gamedata.my_char.ID)))
                                                        {
                                                            forward = true;
                                                        }
                                                    }
                                                }
                                                finally
                                                {
                                                    Globals.PlayerLock.ExitReadLock();
                                                }
                                            }
                                        }
                                        break;
                                    case PServer.MagicSkillUser:
                                        if (Globals.lagfilter_Skills)
                                        {
                                            bbtmp = new ByteBuffer(buffpacket);
                                            bbtmp.ReadByte();
                                            uint source = bbtmp.ReadUInt32();
                                            uint dest = bbtmp.ReadUInt32();
                                            uint skill_id = bbtmp.ReadUInt32();

                                            forward = true;

                                            foreach (uint sid in Globals.lagfilter_ignoreskills)
                                            {
                                                // Is the skill one of the filtered skills
                                                if (skill_id == sid)
                                                {
                                                    // Default drop the packet unless..
                                                    forward = false;

                                                    // Party Member targeting something ?
                                                    if (Globals.gamedata.PartyMembers.ContainsKey(source))
                                                    {
                                                        forward = true;
                                                    }
                                                    else
                                                    {
                                                        // Somebody targeting a partymember or me?
                                                        if ((Globals.gamedata.PartyMembers.ContainsKey(dest)) || (dest == Globals.gamedata.my_char.ID))
                                                        {
                                                            forward = true;
                                                        }
                                                    }
                                                    break;
                                                }
                                            }
                                        }
                                        break;
                                    case PServer.MagicSkillLaunched:
                                        bbtmp = new ByteBuffer(buffpacket);
                                        bbtmp.ReadByte();
                                        uint src = bbtmp.ReadUInt32();
                                        uint sk_id = bbtmp.ReadUInt32();
                                        bbtmp.ReadUInt32();
                                        bbtmp.ReadUInt32();
                                        uint dst = bbtmp.ReadUInt32();

                                        if (src == Globals.gamedata.my_char.ID)
                                        {
                                            Globals.gamedata.my_char.isAttacking = false;
                                        }

                                        if (Globals.lagfilter_Skills)
                                        {
                                            forward = true;

                                            foreach (uint sid in Globals.lagfilter_ignoreskills)
                                            {
                                                // Is the skill one of the filtered skills
                                                if (sk_id == sid)
                                                {
                                                    // Default drop the packet unless..
                                                    forward = false;

                                                    // Party Member targeting something ?
                                                    if (Globals.gamedata.PartyMembers.ContainsKey(src))
                                                    {
                                                        forward = true;
                                                    }
                                                    else
                                                    {
                                                        // Somebody targeting a partymember or me?
                                                        if ((Globals.gamedata.PartyMembers.ContainsKey(dst)) || (dst == Globals.gamedata.my_char.ID))
                                                        {
                                                            forward = true;
                                                        }
                                                    }
                                                    break;
                                                }
                                            }
                                        }
                                        break;
                                    case PServer.CI:
                                        if (Globals.gamedata.Chron >= Chronicle.CT2_5)
                                        {

                                            if ((Globals.lagfilter_xf_ci_striptitle == false) &&
                                                (Globals.lagfilter_xf_ci_stripenchant == false) &&
                                                (Globals.lagfilter_xf_ci_stripaug == false) &&
                                                (Globals.lagfilter_xf_ci_stripunseen == false) &&
                                                (Globals.lagfilter_xf_ci_striprecs == false) &&
                                                (Globals.lagfilter_xf_ci_simple_gender == false) &&
                                                (Globals.lagfilter_xf_ci_simple_apperance == false))
                                            {
                                            }
                                            else
                                            {
                                                bbtmp = new ByteBuffer(buffpacket);
                                                ByteBuffer xf_CI = new ByteBuffer();

                                                xf_CI.Resize(bbtmp.Length());

                                                xf_CI.WriteByte(bbtmp.ReadByte());                      // CI Header
                                                xf_CI.WriteInt32(bbtmp.ReadInt32());                    // X
                                                xf_CI.WriteInt32(bbtmp.ReadInt32());                    // Y
                                                xf_CI.WriteInt32(bbtmp.ReadInt32());                    // Z
                                                xf_CI.WriteInt32(bbtmp.ReadInt32());                    // VID
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());                  // OID
                                                xf_CI.WriteString(bbtmp.ReadString());                  // Name

                                                if (Globals.lagfilter_xf_ci_simple_race)
                                                {
                                                    xf_CI.WriteUInt32(4);
                                                    bbtmp.ReadUInt32();
                                                }
                                                else
                                                {
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());                  // Race
                                                }

                                                if (Globals.lagfilter_xf_ci_simple_gender)
                                                {
                                                    xf_CI.WriteUInt32(1);
                                                    bbtmp.ReadUInt32();
                                                }
                                                else
                                                {
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());              // Gender
                                                }

                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());                  // Root Class

                                                if (Globals.lagfilter_xf_ci_stripunseen)
                                                {
                                                    xf_CI.WriteUInt32(0);
                                                    xf_CI.WriteUInt32(0);
                                                    bbtmp.ReadUInt32();
                                                    bbtmp.ReadUInt32();
                                                }
                                                else
                                                {
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());              // OID Underwear
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());              // OID Head
                                                }

                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());  	            // OID RHand
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());      		    // OID LHand
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());		            // OID Gloves
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());		            // OID Chest
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());	        	    // OID Legs
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());	        	    // OID Feet
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());	        	    // OID Back
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());	        	    // OID LRHand
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());	        	    // OID Hair
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());	        	    // OID DollFace

                                                if (Globals.lagfilter_xf_ci_stripunseen)
                                                {
                                                    for (int i = 0; i < 8; i++)
                                                    {
                                                        xf_CI.WriteUInt32(0);
                                                        bbtmp.ReadUInt32();
                                                    }
                                                }
                                                else
                                                {
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());                 // OID R Bracelet
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());                 // OID L Bracelet
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());                 // OID Talisman 1
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());                 // OID Talisman 2
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());                 // OID Talisman 3
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());                 // OID Talisman 4
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());                 // OID Talisman 5
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());                 // OID Talisman 6                
                                                }

                                                if (Globals.gamedata.Chron >= Chronicle.CT2_3)
                                                {
                                                    if (Globals.lagfilter_xf_ci_stripunseen)
                                                    {
                                                        xf_CI.WriteUInt32(0);
                                                        bbtmp.ReadUInt32();
                                                    }
                                                    else
                                                    {
                                                        xf_CI.WriteUInt32(bbtmp.ReadUInt32()); // OID Belt
                                                    }
                                                }

                                                if ((Globals.lagfilter_xf_ci_stripunseen) || (Globals.lagfilter_xf_ci_stripaug))
                                                {
                                                    xf_CI.WriteUInt32(0);
                                                    xf_CI.WriteUInt32(0);
                                                    bbtmp.ReadUInt32();
                                                    bbtmp.ReadUInt32();
                                                }
                                                else
                                                {
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());		            // AID underwear
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());	        	    // AID head                
                                                }

                                                if (Globals.lagfilter_xf_ci_stripaug)
                                                {
                                                    for (int i = 0; i < 10; i++)
                                                    {
                                                        xf_CI.WriteUInt32(0);
                                                        bbtmp.ReadUInt32();
                                                    }
                                                }
                                                else
                                                {
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());              // AID RHand
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());              // AID LHand
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());	    	    // AID gloves
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());	    	    // AID chest
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());	    	    // AID legs
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());	    	    // AID feet
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());	    	    // AID back
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());	    	    // AID lr hand
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());	    	    // AID hair
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());	    	    // AID doll face                
                                                }

                                                if ((Globals.lagfilter_xf_ci_stripunseen) || (Globals.lagfilter_xf_ci_stripaug))
                                                {
                                                    for (int i = 0; i < 8; i++)
                                                    {
                                                        xf_CI.WriteUInt32(0);
                                                        bbtmp.ReadUInt32();
                                                    }
                                                }
                                                else
                                                {
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());	        // AID right bracelet
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());		    // AID left bracelet
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());		    // AID Talisman 1
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());		    // AID Talisman 2
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());		    // AID Talisman 3
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());		    // AID Talisman 4
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());		    // AID Talisman 5
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());		    // AID Talisman 6
                                                }

                                                if (Globals.gamedata.Chron >= Chronicle.CT2_3)
                                                {
                                                    if ((Globals.lagfilter_xf_ci_stripunseen) || (Globals.lagfilter_xf_ci_stripaug))
                                                    {
                                                        xf_CI.WriteUInt32(0);
                                                        bbtmp.ReadUInt32();
                                                    }
                                                    else
                                                    {
                                                        xf_CI.WriteUInt32(bbtmp.ReadUInt32()); // AID Belt
                                                    }
                                                }

                                                if (Globals.gamedata.Chron >= Chronicle.CT2_3)
                                                {
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());
                                                }

                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());                 // PvP Flag
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());                 // Karma
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());                 // Matkspd
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());                 // Patkspd

                                                if (Globals.gamedata.Chron >= Chronicle.CT2_5)
                                                {
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());
                                                }
                                                else if (Globals.gamedata.Chron >= Chronicle.CT2_3)
                                                {
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());
                                                }

                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());		    // RunSpeed
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());		    // WalkSpeed
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());		    // SwimRunSpeed
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());		    // SwimWalkSpeed
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());		    // flRunSpeed
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());		    // flWalkSpeed
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());		    // FlyRunSpeed
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());		    // FlyWalkSpeed


                                                xf_CI.WriteDouble(bbtmp.ReadDouble());		    // MoveSpeedMult
                                                xf_CI.WriteDouble(bbtmp.ReadDouble());		    // AttackSpeedMult
                                                xf_CI.WriteDouble(bbtmp.ReadDouble());		    // CollisionRadius
                                                xf_CI.WriteDouble(bbtmp.ReadDouble());		    // CollisionHeight

                                                if (Globals.lagfilter_xf_ci_simple_apperance)
                                                {
                                                    xf_CI.WriteUInt32(0);
                                                    xf_CI.WriteUInt32(0);
                                                    xf_CI.WriteUInt32(0);
                                                    bbtmp.ReadUInt32();
                                                    bbtmp.ReadUInt32();
                                                    bbtmp.ReadUInt32();
                                                }
                                                else
                                                {
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());		    // HairSytle
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());		    // HairColor
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());		    // Face
                                                }

                                                if (Globals.lagfilter_xf_ci_striptitle)
                                                {
                                                    bbtmp.ReadString();
                                                    xf_CI.WriteString("");
                                                }
                                                else
                                                {
                                                    xf_CI.WriteString(bbtmp.ReadString());		            // Title
                                                }

                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());		                // ClanID
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());		                // ClanCrestID
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());		                // AllyID
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());		                // AllyCrestID

                                                if (Globals.gamedata.Chron < Chronicle.CT2_5)
                                                {
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());
                                                }

                                                xf_CI.WriteByte(bbtmp.ReadByte());		                // isSitting
                                                xf_CI.WriteByte(bbtmp.ReadByte());		                // isRunning
                                                xf_CI.WriteByte(bbtmp.ReadByte());		                // isInCombat
                                                xf_CI.WriteByte(bbtmp.ReadByte());		                // isAlikeDead
                                                xf_CI.WriteByte(bbtmp.ReadByte());		                // Invisible
                                                xf_CI.WriteByte(bbtmp.ReadByte());		                // MountType
                                                xf_CI.WriteByte(bbtmp.ReadByte());		                // PrivateStoreType	
                                                int CC = bbtmp.ReadUInt16();	                        // CubicCount
                                                xf_CI.WriteUInt16((ushort)CC);

                                                for (uint i = 0; i < CC; i++)
                                                {
                                                    xf_CI.WriteUInt16(bbtmp.ReadUInt16());
                                                }

                                                xf_CI.WriteByte(bbtmp.ReadByte());		                // FindParty
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());		            // AbnormalEffects
                                                xf_CI.WriteByte(bbtmp.ReadByte());		                // isFlying

                                                if (Globals.lagfilter_xf_ci_striprecs)
                                                {
                                                    xf_CI.WriteUInt16(0);
                                                    bbtmp.ReadUInt16();
                                                }
                                                else
                                                {
                                                    xf_CI.WriteUInt16(bbtmp.ReadUInt16());		            // RecAmount
                                                }

                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());		                // MountNpcID npc id
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());		                // CurrentClass
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());

                                                if (Globals.lagfilter_xf_ci_stripenchant)
                                                {
                                                    xf_CI.WriteByte(0);
                                                    bbtmp.ReadByte();
                                                }
                                                else
                                                {
                                                    xf_CI.WriteByte(bbtmp.ReadByte());		                // EnchantAmount
                                                }

                                                xf_CI.WriteByte(bbtmp.ReadByte());		                    // TeamCircle
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());		                // ClanCrestIDLarge
                                                xf_CI.WriteByte(bbtmp.ReadByte());		                    // isNoble noble
                                                xf_CI.WriteByte(bbtmp.ReadByte());		                    // isHero hero
                                                xf_CI.WriteByte(bbtmp.ReadByte());		                    // isFishing
                                                xf_CI.WriteInt32(bbtmp.ReadInt32());		                // FishX
                                                xf_CI.WriteInt32(bbtmp.ReadInt32());		                // FishY
                                                xf_CI.WriteInt32(bbtmp.ReadInt32());		                // FishZ
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());		                // NameColor
                                                xf_CI.WriteInt32(bbtmp.ReadInt32());		                // Heading
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());		                // PledgeClass
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());		                // TitleColor
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());		                // DemonSword
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());		                // ClanRep
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());		                // Transform_ID
                                                xf_CI.WriteUInt32(bbtmp.ReadUInt32());		                // Agathon_ID

                                                if (Globals.gamedata.Chron >= Chronicle.CT2_1)
                                                {
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());
                                                }

                                                if (Globals.gamedata.Chron >= Chronicle.CT2_5)
                                                {
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());              // Extended Effects
                                                }
                                                else if (Globals.gamedata.Chron >= Chronicle.CT2_3)
                                                {
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());//CT2.3
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());//CT2.3
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());//CT2.3
                                                    xf_CI.WriteUInt32(bbtmp.ReadUInt32());//CT2.3
                                                }

                                                /*
                                                if (Globals.lagfilter_xf_ci_striptitle)
                                                {                                                
                                                    xf_CI.ResetIndex();
                                                
                                                    dumpbuilder = new System.Text.StringBuilder();
                                                    int z = 0;
                                                    for (int i = 0; i < xf_CI.Length(); i++)
                                                    {
                                                        z = xf_CI.ReadByte();
                                                        dumpbuilder.Append(z.ToString("X2"));
                                                        dumpbuilder.Append(" ");
                                                    }

                                                    Globals.l2net_home.Add_Dump(dumpbuilder.ToString(), true);

                                                    dumpbuilder = new System.Text.StringBuilder();
                                                    z = 0;
                                                    bbtmp.ResetIndex();
                                                    for (int i = 0; i < bbtmp.Length(); i++)
                                                    {
                                                        z = bbtmp.ReadByte();
                                                        dumpbuilder.Append(z.ToString("X2"));
                                                        dumpbuilder.Append(" ");
                                                    }

                                                    Globals.l2net_home.Add_Dump(dumpbuilder.ToString(), true);
                                          
                                                }
                                                */

                                                xf_CI.TrimToIndex();

                                                forward = false;
                                                if (Globals.dump_pbuff_on_ig_close)
                                                {
                                                    if (Globals.gamedata.stc_buffer.Count > 100)
                                                    {
                                                        Globals.gamedata.stc_buffer.Pop();
                                                        Globals.gamedata.stc_buffer.Push(xf_CI);
                                                    }
                                                    else
                                                    {
                                                        Globals.gamedata.stc_buffer.Push(xf_CI);
                                                    }
                                                }
                                                Globals.gamedata.SendToClient(xf_CI);
                                            }
                                        }
                                        break;
                                    case PServer.EXPacket:
                                        bbtmp = new ByteBuffer(buffpacket);
                                        b0 = bbtmp.ReadByte();

                                        switch ((PServerEX)b0)
                                        {
                                            case PServerEX.ExBrExtraUserInfo:
                                                if (Globals.lagfilter_ExBrExtraUserInfo)
                                                {
                                                    bbtmp = new ByteBuffer(buffpacket);
                                                    bbtmp.ReadByte();
                                                    bbtmp.ReadByte();
                                                    uint source = bbtmp.ReadUInt32();

                                                    forward = true;

                                                    TargetType target_type = Util.GetType(source);

                                                    if (target_type == TargetType.PLAYER)
                                                    {
                                                        forward = false;
                                                        Globals.PlayerLock.EnterReadLock();
                                                        try
                                                        {
                                                            CharInfo player = Util.GetChar(source);

                                                            if (player != null)
                                                            {
                                                                // Was this person targeting me or my party ?
                                                                if ((Globals.gamedata.PartyMembers.ContainsKey(player.TargetID)) ||
                                                                   ((player.TargetID == Globals.gamedata.my_char.ID)))
                                                                {
                                                                    forward = true;
                                                                }
                                                            }
                                                        }
                                                        finally
                                                        {
                                                            Globals.PlayerLock.ExitReadLock();
                                                        }
                                                    }
                                                }
                                                break;
                                        }
                                        break;
                                }
                                if (forward)
                                {
                                    bbuffer1 = new ByteBuffer(buffpacket);

                                    if (Globals.dump_pbuff_on_ig_close)
                                    {
                                        if (Globals.gamedata.stc_buffer.Count > 100)
                                        {
                                            Globals.gamedata.stc_buffer.Pop();
                                            Globals.gamedata.stc_buffer.Push(bbuffer1);
                                        }
                                        else
                                        {
                                            Globals.gamedata.stc_buffer.Push(bbuffer1);
                                        }
                                    }
                                    Globals.gamedata.SendToClient(bbuffer1);
                                }
                                if (handle)
                                {
                                    Globals.gamedata.SendToBotRead(bbuffer0);
                                }
                            }

                            if (cnt > 2)
                                size = System.BitConverter.ToUInt16(buffread, 0);
                        }//end of while loop
				}//end of while running
			}
            catch (System.Exception e)
            {
               Globals.l2net_home.Add_Error("crash: GameReadThread : " + e.Message);
            }
		}//end of read data
	}//end of class
}
