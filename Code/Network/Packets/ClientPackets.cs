using System;
using System.Collections;

namespace L2_login
{
    /// <summary>
    /// Summary description for ClientPackets.
    /// </summary>
    public class ClientPackets
    {
        public ClientPackets()
        {
        }

        public static void GameGuardReply(ByteBuffer buffe)
        {
            //convert the query to a string and store it...
            //then when we catch the reply we can add our new pair

            //check if we are running OOG... if so... look for the pair to reply
            //if not known... give an error message and dont reply
            if (buffe.Length() != 17)
            {
                buffe.Resize(17);
            }

            byte[] ggq = new byte[16];

            ggq[0] = buffe.ReadByte();
            ggq[1] = buffe.ReadByte();
            ggq[2] = buffe.ReadByte();
            ggq[3] = buffe.ReadByte();
            ggq[4] = buffe.ReadByte();
            ggq[5] = buffe.ReadByte();
            ggq[6] = buffe.ReadByte();
            ggq[7] = buffe.ReadByte();
            ggq[8] = buffe.ReadByte();
            ggq[9] = buffe.ReadByte();
            ggq[10] = buffe.ReadByte();
            ggq[11] = buffe.ReadByte();
            ggq[12] = buffe.ReadByte();
            ggq[13] = buffe.ReadByte();
            ggq[14] = buffe.ReadByte();
            ggq[15] = buffe.ReadByte();

            string gg = "";

            for (int i = 0; i < ggq.Length; i++)
            {
                gg += ggq[i].ToString("X2") + " ";
            }

            gg = gg.Trim();

            if (Globals.Script_Debugging)
            {
                Globals.l2net_home.Add_Debug("GameGuard Query: " + gg);
            }


            if (Globals.GG_Clientmode)
            {
                byte[] ggq2 = new byte[17];
                ggq2[0] = (byte)PServer.GameGuardQuery;
                Buffer.BlockCopy(ggq, 0, ggq2, 1, 16);

                string gg2 = "";

                for (int i = 0; i < ggq2.Length; i++)
                {
                    gg2 += ggq2[i].ToString("X2") + " ";
                }

                gg2 = gg2.Trim();

                //Globals.l2net_home.Add_Text("Gameguard query: " + gg2, Globals.Green, TextType.BOT);
                GameGuardClient.SendGGQuery(ggq2);

            }
            else if (Globals.gamedata.OOG)
            {
                gg = gg.Replace(" ", "");
                ServerPackets.Send_GameGuardVerify(gg);
            }
        }

        public static void NetPing(ByteBuffer buffe)
        {
            if (Globals.gamedata.OOG)
            {
                byte cID_0 = buffe.ReadByte();
                byte cID_1 = buffe.ReadByte();
                byte cID_2 = buffe.ReadByte();
                byte cID_3 = buffe.ReadByte();

                ByteBuffer breply = new ByteBuffer(13);

                breply.WriteByte((byte)PClient.NetPingReply);

                breply.WriteByte(cID_0);
                breply.WriteByte(cID_1);
                breply.WriteByte(cID_2);
                breply.WriteByte(cID_3);

                breply.WriteInt32(Globals.Rando.Next(5, 15));
                if (Globals.gamedata.Chron >= Chronicle.CT2_4)
                {
                    breply.WriteInt32(6144);
                }
                else
                {
                    breply.WriteInt32(2048);
                }

                Globals.gamedata.SendToGameServer(breply);
            }
        }

        public static void NPCSay(ByteBuffer buffe)
        {
            //objic
            //texttype
            //npcid
            uint npcstr = 0;
            string n_msg = "";
            uint obj_id = buffe.ReadUInt32();
            uint texttype = buffe.ReadUInt32();
            uint npc_id = buffe.ReadUInt32();
            if (Globals.gamedata.Chron >= Chronicle.CT1_5)
            {
                npcstr = buffe.ReadUInt32();
                if (npcstr == 0)
                {
                    n_msg = Util.GetNPCName(npc_id) + ": " + buffe.ReadString();
                }
                else
                {
                    try
                    {
                        string str = ((NPCString)Globals.npcstring[npcstr]).text;
                        n_msg = Util.GetNPCName(npc_id) + ": " + str;
                    }
                    catch
                    {

                    }
                }

            }

            else
            {
                n_msg = Util.GetNPCName(npc_id) + ": " + buffe.ReadString();
            }

            if ((texttype != 0x16) && Globals.NpcSay)
            {
            Globals.l2net_home.Add_Text(n_msg, System.Drawing.Brushes.White, TextType.LOCAL);     
            }
        }

        public static string SystemMessageInternal(uint type, ByteBuffer buffe)
        {
            string message = "";
            System.Collections.ArrayList msglist = new System.Collections.ArrayList();

            uint count = buffe.ReadUInt32();

            if (count > 5)
            {
                count = 5;
            }

            for (uint i = 0; i < count; i++)
            {
                uint type2 = buffe.ReadUInt32();

                switch (type2)
                {
                    case 0x00://string
                    case 0x0C:
                        msglist.Add(buffe.ReadString());
                        break;
                    case 0x01://number
                        msglist.Add(buffe.ReadInt32().ToString());
                        break;
                    case 0x02://npc name
                        msglist.Add(Util.GetNPCName(buffe.ReadUInt32()));
                        break;
                    case 0x03://item name
                        msglist.Add(Util.GetItemName(buffe.ReadUInt32()));
                        break;
                    case 0x04://skill name
                        uint data1 = buffe.ReadUInt32();//id - 2037
                        uint data2 = buffe.ReadUInt32();//level - 1
                        msglist.Add(Util.GetSkillName(data1, data2));
                        break;
                    case 0x05://town names or something...
                        msglist.Add("poopy town");
                        buffe.ReadUInt32();//
                        break;
                    case 0x06://number double
                        msglist.Add(buffe.ReadInt64().ToString());
                        break;
                    case 0x07://zone name
                        int idata1 = buffe.ReadInt32();//x
                        int idata2 = buffe.ReadInt32();//y
                        int idata3 = buffe.ReadInt32();//z
                        msglist.Add("(" + idata1.ToString() + ", " + idata2.ToString() + ", " + idata3.ToString() + ")");
                        break;
                    case 0x08://augmented item
                        msglist.Add(Util.GetItemName(buffe.ReadUInt32()));
                        buffe.ReadUInt32();//the augment data?
                        break;
                    default://poop
                        msglist.Add("<SYSTEM MESSAGE ERROR>");
                        buffe.ReadUInt32();//
                        break;
                }
            }

            while (msglist.Count < 5)
            {
                msglist.Add(" ");
            }

            if (Globals.systemmsg.ContainsKey(type))
            {
                message = ((SystemMsg)Globals.systemmsg[type]).Get_Output((string)msglist[0], (string)msglist[1], (string)msglist[2], (string)msglist[3], (string)msglist[4]);
            }
            else
            {
                message = "UNKNOWN SYSTEM MESSAGE: " + type.ToString() + " : " + (string)msglist[0] + " : " + (string)msglist[1] + " : " + (string)msglist[2] + " : " + (string)msglist[3] + " : " + (string)msglist[4];
            }

            return message;
        }

        public static void SystemMessage(ByteBuffer buffe)
        {
            //64 28 04 00 00 02 00 00 00 01 00 00 00 07 00 00 00 08 00 00 00 10 16 00 00 AC 00 CF 1C 
            //64 5F 00 00 00 02 00 00 00 06 00 00 00 3D 13 00 00 00 00 00 00 01 00 00 00 FA 01 00 00 
            uint type = buffe.ReadUInt32();

            string message = SystemMessageInternal(type, buffe);
            int red = 255;
            int green = 255;
            int blue = 255;

            try
            {
                red = (int)((SystemMsg)Globals.systemmsg[type]).Red;
                green = (int)((SystemMsg)Globals.systemmsg[type]).Green;
                blue = (int)((SystemMsg)Globals.systemmsg[type]).Blue;
            }
            catch
            {
            }

            Globals.l2net_home.Add_Text(message, new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(255, red, green, blue)), TextType.SYSTEM);

            /*if (Globals.gamedata.BOT_STATE == 3)
			{
				if(type == 46)//use skill
				{
					//changing of bot state based on successful skill done elsewhere now
					//Globals.gamedata.my_char.Clear_Botting_Buffing();
				}
				//did our buff fail?... it shouldnt have because of our checks before hand... but just incase
				if(type == 48)//not ready at this time
				{
                    Globals.gamedata.my_char.Clear_Botting_Buffing(false);
				}
				if(type == 50)//target cannot be found
				{
                    Globals.gamedata.my_char.Clear_Botting_Buffing(false);
				}
				if(type == 51)//cannot use skill on self
				{
                    Globals.gamedata.my_char.Clear_Botting_Buffing(false);
				}
				if(type == 113)//cannot use skill unsuitable terms
				{
                    Globals.gamedata.my_char.Clear_Botting_Buffing(false);
				}
				if(type == 181)//cannot see target
				{
                    Globals.gamedata.my_char.Clear_Botting_Buffing(false);
				}
				if(type == 1123)//cannt use skill over weight
				{
                    Globals.gamedata.my_char.Clear_Botting_Buffing(false);
				}
			}*/

            /* Freya:
 * 14:51:47 :[CLIENT DUMP: 38 00 00 00 A0 4F 19 00 93 23 D1 48 //??? 0x38
 * 14:51:47 :[CLIENT DUMP: D0 1A 00 59 07 5C 7B 82 43 A7 A9 6F 20 50 89 5D 62 9B 06 3E 29 04 F0 77 58 1C BE 09 00 2A AD 67 C1 04 43 D9 9D 75 A5 2F 58 12 2A 24 FF BC CF 98 AA 0F FF 9D 0F 31 FD 55 B7 76 F9 EA A7 65 70 EF 64 5F 49 //somethinglong1
 * 14:51:47 :[CLIENT DUMP: CB 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 //GameGuardReply = 0xCB,
*/

            if (type == 0x22  && Globals.gamedata.OOG) //Welcome to the world of Lineage II
            {
                if (Globals.gamedata.Chron >= Chronicle.CT2_5)
                {
                    //ServerPackets.Somethinglong1;
                    ServerPackets.RequestMagicSkillList();
                }

                //request skills
                if (Globals.Got_Skills == false)
                {
                    ServerPackets.RequestSkillList();
                    Globals.Got_Skills = true;
                }
            }

            switch (type)
            {
                case 17:        // Not Enough HP
                case 18:        // Not Enough MP
                case 109:       // Invalid Target                
                case 2156:      // There are not enough necessary items to use the skill
                case 2161:      // There is not enough space to move, the skill cannot be used.
                case 2167:      // A malicious skill cannot be used in a peace zone.
                    Globals.gamedata.my_char.ExpiresTime = 1;
                    break;
                case 139:                    
                    Globals.gamedata.my_char.Resisted = 1;
                    break;
                case 181:       // Cannot See Target
                    Globals.gamedata.my_char.CannotSeeTarget = true;
                    Globals.gamedata.my_char.ExpiresTime = 1;
                    break;
                case 357:       // It has already been spoiled.
                    Globals.gamedata.my_char.TargetSpoiled = true;
                    Globals.gamedata.my_char.ExpiresTime = 1;
                    break;
                case 612:       // The Spoil condition has been activated.
                    Globals.gamedata.my_char.TargetSpoiled = true;
                    break;
                case 1987:
                    if (Globals.ToggleBottingifGMAction)
                    {
                        Globals.l2net_home.Add_Text("Warning, GM performed an action on you!", Globals.Red, TextType.ALL);
                        Globals.l2net_home.Toggle_Botting(1);
                    }
                    break;
            }

            if (Globals.gamedata.CurrentScriptState == ScriptState.Running)
            {
                ScriptEvent sc_ev = new ScriptEvent();
                sc_ev.Type = EventType.SystemMessage;
                sc_ev.Variables.Add(new ScriptVariable((long)type, "MESSAGETYPE", Var_Types.INT, Var_State.PUBLIC));
                sc_ev.Variables.Add(new ScriptVariable(message, "MESSAGE", Var_Types.STRING, Var_State.PUBLIC));
                ScriptEngine.SendToEventQueue(sc_ev);
            }
        }

        public static void MoveToPawn(ByteBuffer buffe)
        {
            uint data1 = buffe.ReadUInt32();//self id
            uint data2 = buffe.ReadUInt32();//target id
            uint dist = buffe.ReadUInt32();
            int ox = buffe.ReadInt32();
            int oy = buffe.ReadInt32();
            int oz = buffe.ReadInt32();

            TargetType movetargettype = Util.GetType(data2);

            if (movetargettype == TargetType.ERROR)
            {
                //we couldn't find the object to move to...

                //who gives a fck?
                //Globals.l2net_home.Add_OnlyDebug("couldnt find pawn for: " + data1.ToString() + " to move to: " + data2.ToString());
                //return;
            }

            TargetType mover = Util.GetType(data1);

            switch (mover)
            {
                case TargetType.SELF:
                    Globals.gamedata.my_char.Moving = true;
                    Globals.gamedata.my_char.MoveTarget = data2;
                    Globals.gamedata.my_char.MoveTargetType = movetargettype;
                    Globals.gamedata.my_char.lastMoveTime = System.DateTime.Now;
                    Globals.gamedata.my_char.X = ox;
                    Globals.gamedata.my_char.Y = oy;
                    Globals.gamedata.my_char.Z = oz;
                    break;
                case TargetType.MYPET:
                    Globals.gamedata.my_pet.Moving = true;
                    Globals.gamedata.my_pet.MoveTarget = data2;
                    Globals.gamedata.my_pet.MoveTargetType = movetargettype;
                    Globals.gamedata.my_pet.lastMoveTime = System.DateTime.Now;
                    Globals.gamedata.my_pet.X = ox;
                    Globals.gamedata.my_pet.Y = oy;
                    Globals.gamedata.my_pet.Z = oz;
                    break;
                case TargetType.MYPET1:
                    Globals.gamedata.my_pet1.Moving = true;
                    Globals.gamedata.my_pet1.MoveTarget = data2;
                    Globals.gamedata.my_pet1.MoveTargetType = movetargettype;
                    Globals.gamedata.my_pet1.lastMoveTime = System.DateTime.Now;
                    Globals.gamedata.my_pet1.X = ox;
                    Globals.gamedata.my_pet1.Y = oy;
                    Globals.gamedata.my_pet1.Z = oz;
                    break;
                case TargetType.MYPET2:
                    Globals.gamedata.my_pet2.Moving = true;
                    Globals.gamedata.my_pet2.MoveTarget = data2;
                    Globals.gamedata.my_pet2.MoveTargetType = movetargettype;
                    Globals.gamedata.my_pet2.lastMoveTime = System.DateTime.Now;
                    Globals.gamedata.my_pet2.X = ox;
                    Globals.gamedata.my_pet2.Y = oy;
                    Globals.gamedata.my_pet2.Z = oz;
                    break;
                case TargetType.MYPET3:
                    Globals.gamedata.my_pet3.Moving = true;
                    Globals.gamedata.my_pet3.MoveTarget = data2;
                    Globals.gamedata.my_pet3.MoveTargetType = movetargettype;
                    Globals.gamedata.my_pet3.lastMoveTime = System.DateTime.Now;
                    Globals.gamedata.my_pet3.X = ox;
                    Globals.gamedata.my_pet3.Y = oy;
                    Globals.gamedata.my_pet3.Z = oz;
                    break;
                case TargetType.PLAYER:
                    Globals.PlayerLock.EnterReadLock();
                    try
                    {
                        CharInfo player = Util.GetChar(data1);

                        if (player != null)
                        {
                            player.Moving = true;
                            player.MoveTarget = data2;
                            player.MoveTargetType = movetargettype;
                            player.lastMoveTime = System.DateTime.Now;
                            player.X = ox;
                            player.Y = oy;
                            player.Z = oz;
                        }
                    }
                    finally
                    {
                        Globals.PlayerLock.ExitReadLock();
                    }
                    break;
                case TargetType.NPC:
                    Globals.NPCLock.EnterReadLock();
                    try
                    {
                        NPCInfo npc = Util.GetNPC(data1);

                        if (npc != null)
                        {
                            npc.Moving = true;
                            npc.MoveTarget = data2;
                            npc.MoveTargetType = movetargettype;
                            npc.lastMoveTime = System.DateTime.Now;
                            npc.X = ox;
                            npc.Y = oy;
                            npc.Z = oz;
                        }
                    }
                    finally
                    {
                        Globals.NPCLock.ExitReadLock();
                    }
                    break;
            }
        }

        public static void RestartResponse(ByteBuffer buffe)
        {
            uint data1 = buffe.ReadUInt32();//System.BitConverter.ToInt32(buffe,1);

            if (data1 == 1)
            {
                //time to restart and go back to the char login screen
                //deal with restarting later, for now quit it all
                //Globals.gamedata.running = false;

                //clear nearby shit...
                Util.ClearAllNearby();
                Util.ClearAllSelf();
            }
        }

        public static void PartySpelled(ByteBuffer buff)
        {

            /*
            	protected final void writeImpl()
	            {
		            writeC(0xf4);
		            writeD(_activeChar instanceof L2SummonInstance ? 2 : _activeChar instanceof L2PetInstance ? 1 : 0);
		            writeD(_activeChar.getObjectId());
		            writeD(_effects.size());
		            for (Effect temp : _effects)
		            {
			            writeD(temp._skillId);
			            writeH(temp._dat);
			            writeD(temp._duration / 1000);
		            }
		
	            }
            */

            uint update_type = buff.ReadUInt32();
            uint update_object = buff.ReadUInt32();
            int update_count = buff.ReadInt32();
            int skill_duration = 0;
            uint skill_id = 0;

            try
            {
                if (Globals.gamedata.PartyMembers.ContainsKey(update_object))
                {
                    PartyMember ph = null;
                    CharInfo ch;
                    Globals.PlayerLock.EnterReadLock();
                    try
                    {
                        ph = Util.GetCharParty(update_object);
                        ch = Util.GetChar(update_object);
                    }
                    finally
                    {
                        Globals.PlayerLock.ExitReadLock();
                    }

                    Globals.PlayerLock.EnterWriteLock();
                    try
                    {
                        if (ph != null)
                        {
                            ph.my_buffs.Clear();

                            for (int i = 0; i < update_count; i++)
                            {
                                skill_id = buff.ReadUInt32();
                                buff.ReadInt16();
                                skill_duration = buff.ReadInt32();
                                Add_PartyBuff(update_object, skill_id, skill_duration, ph);
                            }

                            if (Globals.gamedata.CurrentScriptState == ScriptState.Running)
                            {

                                Script_ClassData cd = new Script_ClassData();
                                cd.Name = "PLAYER";
                                cd._Variables.Add("ID", new ScriptVariable((long)ph.ID, "ID", Var_Types.INT, Var_State.PUBLIC));
                                cd._Variables.Add("NAME", new ScriptVariable(ph.Name, "NAME", Var_Types.STRING, Var_State.PUBLIC));

                                cd._Variables.Add("CLASS", new ScriptVariable((long)ph.Class, "CLASS", Var_Types.INT, Var_State.PUBLIC));
                                cd._Variables.Add("LEVEL", new ScriptVariable((long)ph.Level, "LEVEL", Var_Types.INT, Var_State.PUBLIC));

                                cd._Variables.Add("HP", new ScriptVariable((long)ph.Cur_HP, "HP", Var_Types.INT, Var_State.PUBLIC));
                                cd._Variables.Add("MAX_HP", new ScriptVariable((long)ph.Max_HP, "MAX_HP", Var_Types.INT, Var_State.PUBLIC));
                                cd._Variables.Add("MP", new ScriptVariable((long)ph.Cur_MP, "MP", Var_Types.INT, Var_State.PUBLIC));
                                cd._Variables.Add("MAX_MP", new ScriptVariable((long)ph.Max_MP, "MAX_MP", Var_Types.INT, Var_State.PUBLIC));
                                cd._Variables.Add("CP", new ScriptVariable((long)ph.Cur_CP, "CP", Var_Types.INT, Var_State.PUBLIC));
                                cd._Variables.Add("MAX_CP", new ScriptVariable((long)ph.Max_CP, "MAX_CP", Var_Types.INT, Var_State.PUBLIC));

                                try
                                {
                                    System.Collections.SortedList buffs = new System.Collections.SortedList();
                                    foreach (CharBuff _buff in ph.my_buffs.Values)
                                    {
                                        Script_ClassData ncd = new Script_ClassData();
                                        ncd.Name = "EFFECT";
                                        ncd._Variables.Add("ID", new ScriptVariable((long)_buff.ID, "ID", Var_Types.INT, Var_State.PUBLIC));
                                        ncd._Variables.Add("LEVEL", new ScriptVariable((long)_buff.SkillLevel, "LEVEL", Var_Types.INT, Var_State.PUBLIC));
                                        ncd._Variables.Add("DURATION", new ScriptVariable((long)_buff.ExpiresTime, "DURATION", Var_Types.INT, Var_State.PUBLIC));
                                        ncd._Variables.Add("NAME", new ScriptVariable(Util.GetSkillName(_buff.ID, _buff.SkillLevel), "NAME", Var_Types.STRING, Var_State.PUBLIC));
                                        ScriptVariable nsv = new ScriptVariable(ncd, "EFFECT", Var_Types.CLASS, Var_State.PUBLIC);

                                        buffs.Add(_buff.ID.ToString(), nsv);
                                    }

                                    cd._Variables.Add("EFFECTS", new ScriptVariable((System.Collections.SortedList)buffs, "EFFECTS", Var_Types.SORTEDLIST, Var_State.PUBLIC));
                                }
                                catch
                                {
                                    System.Collections.SortedList empty = new System.Collections.SortedList();
                                    cd._Variables.Add("EFFECTS", new ScriptVariable((System.Collections.SortedList)empty, "EFFECTS", Var_Types.SORTEDLIST, Var_State.PUBLIC));
                                }


                                if (ch == null)
                                {
                                    cd._Variables.Add("X", new ScriptVariable((long)ph.X, "X", Var_Types.INT, Var_State.PUBLIC));
                                    cd._Variables.Add("Y", new ScriptVariable((long)ph.Y, "Y", Var_Types.INT, Var_State.PUBLIC));
                                    cd._Variables.Add("Z", new ScriptVariable((long)ph.Z, "Z", Var_Types.INT, Var_State.PUBLIC));

                                    cd._Variables.Add("TITLE", new ScriptVariable("", "TITLE", Var_Types.STRING, Var_State.PUBLIC));
                                    cd._Variables.Add("CLAN", new ScriptVariable(0L, "CLAN", Var_Types.INT, Var_State.PUBLIC));
                                    cd._Variables.Add("ALLY", new ScriptVariable(0L, "ALLY", Var_Types.INT, Var_State.PUBLIC));

                                    cd._Variables.Add("RACE", new ScriptVariable(0L, "RACE", Var_Types.INT, Var_State.PUBLIC));
                                    cd._Variables.Add("SEX", new ScriptVariable(0L, "SEX", Var_Types.INT, Var_State.PUBLIC));

                                    cd._Variables.Add("PVPFLAG", new ScriptVariable(0L, "PVPFLAG", Var_Types.INT, Var_State.PUBLIC));
                                    cd._Variables.Add("KARMA", new ScriptVariable(0L, "KARMA", Var_Types.INT, Var_State.PUBLIC));

                                    cd._Variables.Add("ATTACK_SPEED", new ScriptVariable(0L, "ATTACK_SPEED", Var_Types.INT, Var_State.PUBLIC));
                                    cd._Variables.Add("CAST_SPEED", new ScriptVariable(0L, "CAST_SPEED", Var_Types.INT, Var_State.PUBLIC));
                                    cd._Variables.Add("RUN_SPEED", new ScriptVariable(0L, "RUN_SPEED", Var_Types.INT, Var_State.PUBLIC));

                                    cd._Variables.Add("TARGET_ID", new ScriptVariable(0L, "TARGET_ID", Var_Types.INT, Var_State.PUBLIC));

                                    cd._Variables.Add("DEST_X", new ScriptVariable(0L, "DEST_X", Var_Types.INT, Var_State.PUBLIC));
                                    cd._Variables.Add("DEST_Y", new ScriptVariable(0L, "DEST_Y", Var_Types.INT, Var_State.PUBLIC));
                                    cd._Variables.Add("DEST_Z", new ScriptVariable(0L, "DEST_Z", Var_Types.INT, Var_State.PUBLIC));

                                    cd._Variables.Add("LOOKS_DEAD", new ScriptVariable(0L, "LOOKS_DEAD", Var_Types.INT, Var_State.PUBLIC));
                                    cd._Variables.Add("IN_COMBAT", new ScriptVariable(0L, "IN_COMBAT", Var_Types.INT, Var_State.PUBLIC));
                                    //cd._Variables.Add("EFFECTS", new ScriptVariable((System.Collections.SortedList)pl.my_buffs, "EFFECTS", Var_Types.SORTEDLIST, Var_State.PUBLIC));
                                }
                                else
                                {
                                    cd._Variables.Add("X", new ScriptVariable((long)ch.X, "X", Var_Types.INT, Var_State.PUBLIC));
                                    cd._Variables.Add("Y", new ScriptVariable((long)ch.Y, "Y", Var_Types.INT, Var_State.PUBLIC));
                                    cd._Variables.Add("Z", new ScriptVariable((long)ch.Z, "Z", Var_Types.INT, Var_State.PUBLIC));

                                    cd._Variables.Add("TITLE", new ScriptVariable(ch.Title, "TITLE", Var_Types.STRING, Var_State.PUBLIC));
                                    cd._Variables.Add("CLAN", new ScriptVariable((long)ch.ClanID, "CLAN", Var_Types.INT, Var_State.PUBLIC));
                                    cd._Variables.Add("ALLY", new ScriptVariable((long)ch.AllyID, "ALLY", Var_Types.INT, Var_State.PUBLIC));

                                    cd._Variables.Add("RACE", new ScriptVariable((long)ch.Race, "RACE", Var_Types.INT, Var_State.PUBLIC));
                                    cd._Variables.Add("SEX", new ScriptVariable((long)ch.Sex, "SEX", Var_Types.INT, Var_State.PUBLIC));

                                    cd._Variables.Add("PVPFLAG", new ScriptVariable((long)ch.PvPFlag, "PVPFLAG", Var_Types.INT, Var_State.PUBLIC));
                                    cd._Variables.Add("KARMA", new ScriptVariable((long)ch.Karma, "KARMA", Var_Types.INT, Var_State.PUBLIC));

                                    cd._Variables.Add("ATTACK_SPEED", new ScriptVariable((long)(ch.PatkSpeed * ch.AttackSpeedMult), "ATTACK_SPEED", Var_Types.INT, Var_State.PUBLIC));
                                    cd._Variables.Add("CAST_SPEED", new ScriptVariable((long)ch.MatkSpeed, "CAST_SPEED", Var_Types.INT, Var_State.PUBLIC));
                                    cd._Variables.Add("RUN_SPEED", new ScriptVariable((long)(ch.RunSpeed * ch.MoveSpeedMult), "RUN_SPEED", Var_Types.INT, Var_State.PUBLIC));

                                    cd._Variables.Add("TARGET_ID", new ScriptVariable((long)ch.TargetID, "TARGET_ID", Var_Types.INT, Var_State.PUBLIC));

                                    cd._Variables.Add("DEST_X", new ScriptVariable((long)ch.Dest_X, "DEST_X", Var_Types.INT, Var_State.PUBLIC));
                                    cd._Variables.Add("DEST_Y", new ScriptVariable((long)ch.Dest_Y, "DEST_Y", Var_Types.INT, Var_State.PUBLIC));
                                    cd._Variables.Add("DEST_Z", new ScriptVariable((long)ch.Dest_Z, "DEST_Z", Var_Types.INT, Var_State.PUBLIC));

                                    cd._Variables.Add("LOOKS_DEAD", new ScriptVariable((long)ch.isAlikeDead, "LOOKS_DEAD", Var_Types.INT, Var_State.PUBLIC));
                                    cd._Variables.Add("IN_COMBAT", new ScriptVariable((long)ch.isInCombat, "IN_COMBAT", Var_Types.INT, Var_State.PUBLIC));
                                    //cd._Variables.Add("EFFECTS", new ScriptVariable((System.Collections.SortedList)pl.my_buffs, "EFFECTS", Var_Types.SORTEDLIST, Var_State.PUBLIC));
                                }

                                ScriptEvent sc_ev = new ScriptEvent();
                                sc_ev.Type = EventType.PartyEffect;
                                sc_ev.Variables.Add(new ScriptVariable(cd, "PARTYMEMBER", Var_Types.CLASS, Var_State.PUBLIC));
                                ScriptEngine.SendToEventQueue(sc_ev);
                            }
                        }
                    }
                    finally
                    {
                        Globals.PlayerLock.ExitWriteLock();
                    }
                }
            }
            finally
            {
                
            }
        }

        public static void Add_PartyBuff(uint object_id, uint skill_id, int skill_duration, PartyMember ph)
        {
            // XXX: Not Thread Safe by itself
            try
            {
                if (Globals.gamedata.PartyMembers.ContainsKey(object_id))
                {
                    try
                    {
                        CharBuff cb = new CharBuff();
                        cb.ID = skill_id;
                        cb.SkillLevel = 1; // partyspelld doesnt give level, so set to 1 so we can lookup the name if needed
                        /*if (skill_duration == -1)
                        {
                            // slothmo: this is a delete for a buff we don't have...
                            // d00d: actually -1 == toggle buff, fix later
                        }
                        else
                        {*/
                            cb.ExpiresTime = System.DateTime.Now.AddSeconds(skill_duration).Ticks;
                            cb.EFFECT_TIME = skill_duration;
                            //v392B11: This hopefully fixes F4 packeterror
                            if (ph.my_buffs.ContainsKey(cb.ID))
                            {
                                ph.my_buffs[cb.ID] = cb;
                            }
                            else
                            {
                                ph.my_buffs.Add(cb.ID, cb);
                            }
                        //}
                    }
                    finally
                    {

                    }
                }
            }
            finally
            {
            }
        }

        public static void LoadBuffList(ByteBuffer buff)
        {
            int cnt = buff.ReadInt16();

            Globals.MyBuffsListLock.EnterWriteLock();
            try
            {
                //duration in seconds
                Globals.gamedata.mybuffs.Clear();

                for (int i = 0; i < cnt; i++)
                {
                    Add_Buff(buff);
                }

                if (Globals.gamedata.CurrentScriptState == ScriptState.Running)
                {
                    SortedList _mybuff = new SortedList();
                    if (Globals.gamedata.mybuffs.Count > 0)
                    {
                        foreach (CharBuff _buff in Globals.gamedata.mybuffs.Values)
                        {
                            Script_ClassData cd = new Script_ClassData();
                            cd.Name = "EFFECT";
                            cd._Variables.Add("ID", new ScriptVariable((long)_buff.ID, "ID", Var_Types.INT, Var_State.PUBLIC));
                            cd._Variables.Add("LEVEL", new ScriptVariable((long)_buff.SkillLevel, "LEVEL", Var_Types.INT, Var_State.PUBLIC));
                            cd._Variables.Add("DURATION", new ScriptVariable((long)_buff.ExpiresTime, "DURATION", Var_Types.INT, Var_State.PUBLIC));
                            cd._Variables.Add("EFFECT_TIME", new ScriptVariable((long)_buff.EFFECT_TIME, "EFFECT_TIME", Var_Types.INT, Var_State.PUBLIC));
                            cd._Variables.Add("NAME", new ScriptVariable(Util.GetSkillName(_buff.ID, _buff.SkillLevel), "NAME", Var_Types.STRING, Var_State.PUBLIC));
                            ScriptVariable sv = new ScriptVariable(cd, "EFFECT", Var_Types.CLASS, Var_State.PUBLIC);
                            _mybuff.Add(_buff.ID.ToString(), sv);
                        }
                    }
                    ScriptEvent sc_ev = new ScriptEvent();
                    sc_ev.Type = EventType.CharEffect;
                    sc_ev.Variables.Add(new ScriptVariable(_mybuff, "EFFECTLIST", Var_Types.SORTEDLIST, Var_State.PUBLIC));
                    ScriptEngine.SendToEventQueue(sc_ev);
                }

            }
            finally
            {
                Globals.MyBuffsListLock.ExitWriteLock();
            }
        }

        public static void Add_Buff(ByteBuffer buff)
        {
            uint skill_id = buff.ReadUInt32();
            uint skill_level = buff.ReadUInt16();
            int skill_duration = buff.ReadInt32();

            //need to see if the buff exists in our list...
            //if so, update the remaining time
            //otherwise, add the buff
            if (Globals.gamedata.mybuffs.ContainsKey(skill_id))
            {
                CharBuff cb = (CharBuff)Globals.gamedata.mybuffs[skill_id];
                cb.SkillLevel = skill_level;
                if (skill_duration == -1)
                {
                    //remove the buff
                    //Globals.gamedata.mybuffs.Remove(skill_id);


                    // Toggle buff!
                    cb.ExpiresTime = -1;
                    cb.EFFECT_TIME = -1;
                }
                else
                {
                    cb.ExpiresTime = System.DateTime.Now.AddSeconds(skill_duration).Ticks;
                }
            }
            else
            {
                CharBuff cb = new CharBuff();
                cb.ID = skill_id;
                cb.SkillLevel = skill_level;
                if (skill_duration == -1)
                {
                    //toggle buff

                    cb.ExpiresTime = -1;
                    cb.EFFECT_TIME = -1;
                    Globals.gamedata.mybuffs.Add(cb.ID, cb);

                }
                else
                {
                    cb.EFFECT_TIME = skill_duration;
                    cb.ExpiresTime = System.DateTime.Now.AddSeconds(skill_duration).Ticks;
                    Globals.gamedata.mybuffs.Add(cb.ID, cb);
                }
            }
        }

        public static void UpdateBuff(ByteBuffer buff)
        {
            uint skill_id = buff.ReadUInt32();
            uint skill_level = buff.ReadUInt32(); // maybe it iwll fix it
            int skill_duration = buff.ReadInt32();

            Globals.MyBuffsListLock.EnterWriteLock();
            try
            {
                //need to see if the buff exists in our list...
                //if so, update the remaining time
                //otherwise, add the buff
                if (Globals.gamedata.mybuffs.ContainsKey(skill_id))
                {
                    CharBuff cb = (CharBuff)Globals.gamedata.mybuffs[skill_id];
                    cb.SkillLevel = skill_level;
                    if (skill_duration == -1)
                    {
                        //remove the buff
                        //Globals.gamedata.mybuffs.Remove(skill_id);


                        //toggle buff
                        cb.ExpiresTime = -1;
                        cb.EFFECT_TIME = -1;
                    }
                    else
                    {
                        cb.EFFECT_TIME = skill_duration;
                        cb.ExpiresTime = System.DateTime.Now.AddSeconds(skill_duration).Ticks;
                    }
                }
                else
                {
                    CharBuff cb = new CharBuff();
                    cb.ID = skill_id;
                    cb.SkillLevel = skill_level;
                    if (skill_duration == -1)
                    {
                        //toggle buff

                        cb.ExpiresTime = -1;
                        cb.EFFECT_TIME = -1;
                        Globals.gamedata.mybuffs.Add(cb.ID, cb);
                    }
                    else
                    {
                        cb.EFFECT_TIME = skill_duration;
                        cb.ExpiresTime = System.DateTime.Now.AddSeconds(skill_duration).Ticks;
                        Globals.gamedata.mybuffs.Add(cb.ID, cb);
                    }
                }
                if (Globals.gamedata.CurrentScriptState == ScriptState.Running)
                {
                    SortedList _mybuff = new SortedList();
                    if (Globals.gamedata.mybuffs.Count > 0)
                    {
                        foreach (CharBuff _buff in Globals.gamedata.mybuffs.Values)
                        {
                            Script_ClassData cd = new Script_ClassData();
                            cd.Name = "EFFECT";
                            cd._Variables.Add("ID", new ScriptVariable((long)_buff.ID, "ID", Var_Types.INT, Var_State.PUBLIC));
                            cd._Variables.Add("LEVEL", new ScriptVariable((long)_buff.SkillLevel, "LEVEL", Var_Types.INT, Var_State.PUBLIC));
                            cd._Variables.Add("DURATION", new ScriptVariable((long)_buff.ExpiresTime, "DURATION", Var_Types.INT, Var_State.PUBLIC));
                            cd._Variables.Add("EFFECT_TIME", new ScriptVariable((long)_buff.EFFECT_TIME, "EFFECT_TIME", Var_Types.INT, Var_State.PUBLIC));
                            cd._Variables.Add("NAME", new ScriptVariable(Util.GetSkillName(_buff.ID, _buff.SkillLevel), "NAME", Var_Types.STRING, Var_State.PUBLIC));
                            ScriptVariable sv = new ScriptVariable(cd, "EFFECT", Var_Types.CLASS, Var_State.PUBLIC);
                            _mybuff.Add(_buff.ID.ToString(), sv);
                        }
                    }
                    ScriptEvent sc_ev = new ScriptEvent();
                    sc_ev.Type = EventType.CharEffect;
                    sc_ev.Variables.Add(new ScriptVariable(_mybuff, "EFFECTLIST", Var_Types.SORTEDLIST, Var_State.PUBLIC));
                    ScriptEngine.SendToEventQueue(sc_ev);
                }
            }
            finally
            {
                Globals.MyBuffsListLock.ExitWriteLock();
            }
        }

        public static void SkillList(ByteBuffer buffe)
        {
            uint cnt = 0;
            if (Globals.gamedata.Chron < Chronicle.CT3_0)
            {
                 cnt = buffe.ReadUInt32();
            }
            else
            {
                cnt = buffe.ReadUInt16();
            }

            Globals.SkillListLock.EnterWriteLock();
            try
            {
                //flag all our skills as possibly old
                foreach (UserSkill old_us in Globals.gamedata.skills.Values)
                {
                    old_us.OldSkill = true;
                }

                //load all of our new skill
                for (uint i = 0; i < cnt; i++)
                {
                    UserSkill us = new UserSkill();
                    if (Globals.gamedata.Chron >= Chronicle.CT3_0)
                    {
                        buffe.ReadUInt16(); //dunno
                    }
                    us.Passive = buffe.ReadUInt32();
                    us.Level = buffe.ReadUInt32();
                    us.ID = buffe.ReadUInt32();
                    buffe.ReadByte();//dunno what the new 1 byte is

                    if (Globals.gamedata.Chron >= Chronicle.CT2_4)
                    {
                        buffe.ReadByte();//dunno what the new 1 byte is
                    }

                    if (Globals.gamedata.Chron >= Chronicle.CT3_0)
                    {
                        buffe.ReadByte(); //FF
                        buffe.ReadByte(); //FF
                    }

                    //us.NextTime = new System.DateTime(0L); //System.DateTime.Now.AddMilliseconds(Globals.SKILL_INIT_REUSE);
                    //us.LastTime = new System.DateTime(0L);//start at the begining of time

                    if (Globals.gamedata.skills.ContainsKey(us.ID))
                    {
                        ((UserSkill)Globals.gamedata.skills[us.ID]).Level = us.Level;
                        ((UserSkill)Globals.gamedata.skills[us.ID]).OldSkill = false;
                    }
                    else
                    {
                        Globals.gamedata.skills.Add(us.ID, us);
                    }
                }

                //remove all of our old skills
                System.Collections.ArrayList dirty_items = new System.Collections.ArrayList();

                foreach (UserSkill old_us in Globals.gamedata.skills.Values)
                {
                    if (old_us.OldSkill)
                    {
                        dirty_items.Add(old_us.ID);
                    }
                }

                for (int i = 0; i < dirty_items.Count; i++)
                {
                    Globals.gamedata.skills.Remove((uint)dirty_items[i]);
                }
                dirty_items.Clear();
            }
            finally
            {
                Globals.SkillListLock.ExitWriteLock();
                if (Globals.show_active_skills)
                {
                    AddInfo.Set_SkillList(1);
                }
                else
                {
                    AddInfo.Set_SkillList(0);
                }
            }
        }

        public static void SkillCoolTime(ByteBuffer buffe)
        {
            int count = buffe.ReadInt32();

            Globals.SkillListLock.EnterReadLock();
            try
            {
                for (int i = 0; i < count; i++)
                {
                    uint skillid = buffe.ReadUInt32();
                    int poop = buffe.ReadInt32();
                    int total_time = buffe.ReadInt32();
                    int remain_time = buffe.ReadInt32();

                    if (Globals.gamedata.skills.ContainsKey(skillid))
                    {
                        ((UserSkill)Globals.gamedata.skills[skillid]).LastTime = System.DateTime.Now;
                        remain_time = remain_time * 1000;

                        //Globals.l2net_home.Add_Text("remain time: " + remain_time.ToString());
                        ((UserSkill)Globals.gamedata.skills[skillid]).NextTime = System.DateTime.Now.AddMilliseconds(remain_time);
                    }
                }
            }
            finally
            {
                Globals.SkillListLock.ExitReadLock();
            }
        }

        public static void OtherMessage(ByteBuffer buffe)
        {
            uint data1 = buffe.ReadUInt32();//objid - mob that said it
            uint data2 = buffe.ReadUInt32();//type of message

            //offset = 9;
            string from = buffe.ReadString();
            if (Globals.gamedata.Chron >= Chronicle.CT2_6)
            {
                buffe.ReadUInt32();
            }
            string text = buffe.ReadString();

            if (Globals.gamedata.CurrentScriptState == ScriptState.Running)
            {
                ScriptEvent sc_ev = new ScriptEvent();
                sc_ev.Type = EventType.Chat;
                sc_ev.Variables.Add(new ScriptVariable((long)data1, "SENDERID", Var_Types.INT, Var_State.PUBLIC));
                sc_ev.Variables.Add(new ScriptVariable((long)data2, "MESSAGETYPE", Var_Types.INT, Var_State.PUBLIC));
                sc_ev.Variables.Add(new ScriptVariable(from, "SENDERNAME", Var_Types.STRING, Var_State.PUBLIC));
                sc_ev.Variables.Add(new ScriptVariable(text, "MESSAGE", Var_Types.STRING, Var_State.PUBLIC));
                ScriptEngine.SendToEventQueue(sc_ev);
            }

            string message = from + ": " + text;

            switch (data2)
            {
                case 0x00://local
                    if (Globals.gamedata.alertoptions.beepon_whitechat)
                    {
                        VoicePlayer.PlayAlarm();
                    }
                    Globals.l2net_home.Add_Text(message, Globals.White, TextType.LOCAL);

                    if ((Globals.gamedata.LocalChatQueue.Count < 4) && Globals.gamedata.autoreply && (data1 != Globals.gamedata.my_char.ID))
                    {
                        Globals.gamedata.LocalChatQueue.Enqueue(message); //Max 3 messages at a time
                    }

                    break;
                case 0x01://shout
                    Globals.l2net_home.Add_Text(message, new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(((System.Byte)(253)), ((System.Byte)(100)), ((System.Byte)(0)))), TextType.LOCAL);
                    break;
                case 0x02://tell
                    if (Globals.gamedata.alertoptions.beepon_privatemessage)
                    {
                        VoicePlayer.PlayAlarm();
                    }
                    Globals.l2net_home.Add_Text(message, Globals.Tell_Brush, TextType.ALL);

                    if ((Globals.gamedata.PrivateMsgQueue.Count < 4)&& Globals.gamedata.autoreplyPM && (data1 != Globals.gamedata.my_char.ID))
                    {
                        Globals.gamedata.PrivateMsgQueue.Enqueue(message);
                    }
                    break;
                case 0x03://party
                    Globals.l2net_home.Add_Text(message, Globals.Party_Brush, TextType.PARTY);
                    break;
                case 0x04://clan
                    Globals.l2net_home.Add_Text(message, Globals.Clan_Brush, TextType.CLAN);
                    break;
                case 0x05://gm msg
                    Globals.l2net_home.Add_Text("GM MSG: " + message, Globals.Red, TextType.ALL);
                    break;
                case 0x06://gm petition text
                    Globals.l2net_home.Add_Text("GM PETITION: " + message, Globals.Red, TextType.ALL);
                    break;
                case 0x07://gm petition reply
                    Globals.l2net_home.Add_Text("GM PETITION REPLY: " + message, Globals.Red, TextType.ALL);
                    break;
                case 0x08://trade
                    Globals.l2net_home.Add_Text(message, Globals.Trade_Brush, TextType.TRADE);
                    break;
                case 0x09://ally
                    Globals.l2net_home.Add_Text(message, Globals.Ally_Brush, TextType.ALLY);
                    break;
                case 0x0A://anouncement
                    Globals.l2net_home.Add_Text(message, Globals.Announcement_Brush, TextType.ALL);
                    break;
                case 0x0B://crash
                    //Globals.l2net_home.Add_Text("Boat Message: " + message, Globals.Red, TextType.SYSTEM); //Disabled, boat message is bugged anyway...
                    break;
                case 0x0C://fake1
                    Globals.l2net_home.Add_Text("FAKE1: " + message, Globals.Red, TextType.ALL);
                    break;
                case 0x0D://fake2
                    Globals.l2net_home.Add_Text("FAKE2: " + message, Globals.Red, TextType.ALL);
                    break;
                case 0x0E://fake3
                    Globals.l2net_home.Add_Text("FAKE3: " + message, Globals.Red, TextType.ALL);
                    break;
                case 0x0F://party room all
                    Globals.l2net_home.Add_Text(message, Globals.Salmon, TextType.PARTY);
                    break;
                case 0x10://party room commander
                    Globals.l2net_home.Add_Text(message, Globals.Yellow, TextType.PARTY);
                    break;
                case 0x11://hero
                    Globals.l2net_home.Add_Text(message, Globals.Cyan, TextType.HERO);
                    break;
                case 0x12://Announcement 2
                    Globals.l2net_home.Add_Text(message, Globals.Announcement_Brush, TextType.SYSTEM);
                    break;
                case 0x14://territory battle
                    Globals.l2net_home.Add_Text(message, Globals.Gold, TextType.SYSTEM);
                    break;
                default:
                    Globals.l2net_home.Add_Text("0x" + data2.ToString("X2") + ": " + message, Globals.Red, TextType.BOT);
                    break;
            }
        }

        public static void RequestReply(ByteBuffer buffe)
        {
            string request = "resurrection";

            Globals.LastRezz1 = buffe.ReadUInt32();
            buffe.ReadInt32();
            buffe.ReadInt32();
            string tmp = buffe.ReadString();

            switch (buffe.ReadInt32())
            {
                case 0://string
                    buffe.ReadString();
                    break;
                case 1://number
                    buffe.ReadInt32();
                    break;
                case 2://npc name
                    buffe.ReadUInt32();//GetNPCName
                    break;
                case 3://item name
                    buffe.ReadUInt32();//GetItemName
                    break;
                case 4://skill name
                    buffe.ReadUInt32();//id - 2037
                    buffe.ReadUInt32();//level - 1
                    break;
                case 5://poop
                    buffe.ReadUInt32();//
                    break;
                case 6://number double
                    buffe.ReadInt64();
                    break;
                case 7://zone name
                    request = "summoning";
                    buffe.ReadInt32();//x
                    buffe.ReadInt32();//y
                    buffe.ReadInt32();//z
                    break;
                case 8://augmented item
                    buffe.ReadUInt32();//item type id
                    buffe.ReadUInt32();//the augment data?
                    break;
                default://poop
                    buffe.ReadUInt32();//
                    break;
            }
            buffe.ReadUInt32();
            Globals.LastRezz2 = buffe.ReadUInt32();

            Globals.l2net_home.Set_YesNo(tmp + " is attmepting " + request + ". " + Environment.NewLine + "Do you accept?");

            Globals.gamedata.yesno_state = 5;

            Globals.l2net_home.Show_YesNo();
        }

        public static void RequestJoinAlly(ByteBuffer buffe)
        {
            uint data1 = buffe.ReadUInt32();//id of the inviter
            string tmp = buffe.ReadString();//TODO: ally name? or player name? no idea

            //data1 = buffe.ReadUInt32();//??

            Globals.l2net_home.Set_YesNo(Util.GetCharName(data1) + " has invited you to join the ally " + tmp + Environment.NewLine + "Do you accept?");

            Globals.gamedata.yesno_state = 4;

            Globals.l2net_home.Show_YesNo();
        }

        public static void RequestJoinFriend(ByteBuffer buffe)
        {
            string tmp = buffe.ReadString();

            //data1 = buffe.ReadUInt32();//??

            Globals.l2net_home.Set_YesNo(tmp + " has invited you to be a friend." + Environment.NewLine + "Do you accept?");

            Globals.gamedata.yesno_state = 3;

            Globals.l2net_home.Show_YesNo();
        }

        public static void RequestJoinParty(ByteBuffer buffe)
        {
            string tmp = buffe.ReadString();
            int sent = 0;

            uint data1 = buffe.ReadUInt32();//party loot style

            foreach (CharInfo player in Globals.gamedata.nearby_chars.Values)
            {
                if (player.Name == tmp && player.ClanID == Globals.gamedata.my_char.ClanID && Globals.gamedata.botoptions.AcceptPartyClan == 1 && sent == 0)
                {
                    ServerPackets.JoinPartyReply(true);
                    sent = 1;
                }
                if (player.Name == tmp && player.AllyID == Globals.gamedata.my_char.AllyID && Globals.gamedata.botoptions.AcceptPartyAlly == 1 && sent == 0)
                {
                    ServerPackets.JoinPartyReply(true);
                    sent = 1;
                }
            }

            Globals.l2net_home.Set_YesNo(tmp + " has invited you to a party." + Environment.NewLine + "Loot Style: " + data1.ToString() + Environment.NewLine + "Do you accept?");

            if (sent == 0)
            {
                Globals.gamedata.yesno_state = 1;
                Globals.l2net_home.Show_YesNo();
            }
        }

        public static void RequestJoinClan(ByteBuffer buffe)
        {
            uint data1 = buffe.ReadUInt32();

            string tmp = buffe.ReadString();

            Globals.l2net_home.Set_YesNo(Util.GetCharName(data1) + " has invited you to clan " + tmp + "." + Environment.NewLine + "Do you accept?");

            Globals.gamedata.yesno_state = 2;

            Globals.l2net_home.Show_YesNo();
        }

        public static void TradeRequest(ByteBuffer buffe)
        {
            uint data1 = buffe.ReadUInt32();

            Globals.l2net_home.Set_YesNo(Util.GetCharName(data1) + " would like to trade." + Environment.NewLine + "Do you accept?");

            Globals.gamedata.yesno_state = 6;

            Globals.l2net_home.Show_YesNo();
        }

        public static void ItemList(ByteBuffer buffe)
        {
            uint h1 = buffe.ReadUInt16();//1 - open the inventory window up//ushort
            uint h2 = buffe.ReadUInt16();//ushort

            //offset = 5;

            //flag all inventory as old
            Globals.InventoryLock.EnterReadLock();
            try
            {
                foreach (InventoryInfo inv_inf in Globals.gamedata.inventory.Values)
                {
                    inv_inf.InNewList = false;
                }
            }
            finally
            {
                Globals.InventoryLock.ExitReadLock();
            }

            Globals.InventoryLock.EnterWriteLock();
            try
            {
                Globals.gamedata.inventory.Clear();
                Globals.l2net_home.Clear_Equip();

                for (uint i = 0; i < h2; i++)
                {
                    InventoryInfo inv_info = new InventoryInfo();
                    inv_info.Load(buffe,0);
                    //inv_info.InNewList = true;
                    AddInfo.Add_Inventory(inv_info);
                }
            }
            finally
            {
                Globals.InventoryLock.ExitWriteLock();
            }

            Globals.l2net_home.timer_inventory.Start();
        }

        public static void ExQuestItemList(ByteBuffer buffe)
        {
            uint h2 = buffe.ReadUInt16();//ushort


            //No need to clear stuff here, as Itemlist is always run first and fixes all that.
            Globals.InventoryLock.EnterWriteLock();
            try
            {

                for (uint i = 0; i < h2; i++)
                {
                    InventoryInfo inv_info = new InventoryInfo();
                    inv_info.Load(buffe,0);
                    AddInfo.Add_Inventory(inv_info);
                }
            }
            finally
            {
                Globals.InventoryLock.ExitWriteLock();
            }

            //Globals.l2net_home.timer_inventory.Start();
        }


        public static void ExVitalityUpdate(ByteBuffer buffe)
        {
            Globals.gamedata.my_char.Vitality_Points = buffe.ReadInt32();
            AddInfo.Set_Char_Info();

        }

        public static void BuyList(ByteBuffer buffe)
        {
            /*07
            BA 25 3D 00 //Money
            00 00 00 00 //??
            7C 91 2E 00 //Buy list id

            2A 00 //Itemcount*/

            //buffe.ReadByte();
            buffe.ReadUInt32();
            buffe.ReadUInt32();
            Globals.gamedata.blistID = buffe.ReadUInt32();

            uint itemcnt = buffe.ReadUInt16(); //Number of objects in list
            //Globals.l2net_home.Add_Text("Buylist itemcnt:" + itemcnt, Globals.Red, TextType.BOT);
            Globals.BuyListLock.EnterWriteLock();
            try
            {
                Globals.gamedata.buylist.Clear(); //Need to clear this every time the user enters a shop

                for (uint i = 0; i < itemcnt; i++)
                {
                    BuyList b_list = new BuyList();
                    b_list.Load(buffe);
                    AddInfo.Add_Buylist(b_list);
                }
            }

            finally
            {
                Globals.BuyListLock.ExitWriteLock();
                if (Globals.OOG)
                {
                    BuyWindow bw = new BuyWindow();
                    bw.ShowDialog();
                }
            }
        }

        public static void CharSelected(ByteBuffer buffe)
        {
            Globals.gamedata.my_char.Load(buffe);
            AddInfo.Set_Char_Info();

            if (Globals.GG_Clientmode || Globals.gamedata.OOG)
            {
                ServerPackets.Send_GGInit();
            }

            if (Globals.gamedata.OOG)
            {
                System.Threading.Thread.Sleep(2500);

                //D0 01 00 
                ServerPackets.RequestManorList();

                //D0 CE 00
                ServerPackets.RequestLogin4();

                //D0 21 00 
                ServerPackets.RequestKeyMapping();

                //send the EnterWorld packet
                ServerPackets.Send_EnterWorld();

            }
            System.Threading.Thread.Sleep(1500);
            Globals.enterworld_sent = true;
        }

        public static void NPCInfo(ByteBuffer buffe)
        {
            try
            {
                NPCInfo npc_inf = new NPCInfo();
                npc_inf.Load(buffe);
                AddInfo.Add_NPCInfo(npc_inf);
            }
            catch(Exception e)
            {
                Globals.l2net_home.Add_Text("0C packet error caught! " + e.Message, Globals.Red, TextType.ALL);
            }
        }

        public static void ExNPCInfo(ByteBuffer buffe)
        {
            try
            {
                NPCInfo npc_inf = new NPCInfo();
                npc_inf.LoadEX(buffe);
                AddInfo.Add_NPCInfo(npc_inf);
            }
            catch (Exception e)
            {
                Globals.l2net_home.Add_Text("FE:6601 packet error caught! " + e.Message, Globals.Red, TextType.ALL);
            }

        }
        public static void ExUserInfoStats(ByteBuffer buffe)
        {
            try
            {
                Player_Info player_inf = new Player_Info();
                player_inf.LoadStatsEX(buffe);
                AddInfo.Set_Char_Info();
            }
            catch (Exception e)
            {
                Globals.l2net_home.Add_Text("FE:5F01 packet error caught! " + e.Message, Globals.Red, TextType.ALL);
            }
        }

        public static void ExUserInfoItems(ByteBuffer buffe)
        {
            try
            {
                Player_Info player_inf = new Player_Info();
                player_inf.LoadItemsEX(buffe);
                AddInfo.Set_Char_Info();
            }
            catch (Exception e)
            {
                Globals.l2net_home.Add_Text("FE:6101 packet error caught! " + e.Message, Globals.Red, TextType.ALL);
            }

        }


        public static void ServerObjectInfo(ByteBuffer buffe)
        {
            NPCInfo npc_inf = new NPCInfo();
            npc_inf.LoadServerObject(buffe);
            AddInfo.Add_NPCInfo(npc_inf);
        }

        public static void StaticObjectInfo(ByteBuffer buffe)
        {
            NPCInfo npc_inf = new NPCInfo();
            npc_inf.LoadStaticObject(buffe);
            AddInfo.Add_NPCInfo(npc_inf);
        }

        public static void ItemDrop(ByteBuffer buffe)
        {
            ItemInfo itm_info = new ItemInfo();
            itm_info.LoadDrop(buffe);
            AddInfo.Add_ItemInfo(itm_info);
        }

        public static void AddItem(ByteBuffer buffe)
        {
            ItemInfo itm_info = new ItemInfo();
            itm_info.Load(buffe);
            AddInfo.Add_ItemInfo(itm_info);
        }

        public static void UserInfo(ByteBuffer buffe)
        {
            int loc = buffe.GetIndex();
            buffe.ReadInt32();
            buffe.ReadInt32();
            buffe.ReadInt32();
            buffe.ReadInt32();
            uint ID = buffe.ReadUInt32();//A0 B9 B0 49
            string name = buffe.ReadString();

            buffe.SetIndex(loc);

            if (name == Globals.gamedata.my_char.Name)
            {
                Globals.gamedata.my_char.Load_User(buffe);
                AddInfo.Set_Char_Info();
                if (Globals.gamedata.teleported)
                {
                    Globals.gamedata.teleported = false;

                    GameServer.CleanUp();
                }

                //we are fully in the world now
                Globals.FulllyIn = true;
            }
            else
            {
                //we got a UserInfo packet for someone other than ourself...
                Globals.l2net_home.Add_Debug("got userinfo for other char: " + name);

                CharInfo player = null;

                Globals.PlayerLock.EnterReadLock();
                try
                {
                    player = Util.GetChar(ID);
                }
                finally
                {
                    Globals.PlayerLock.ExitReadLock();
                }

                if (player != null)
                {
                    player.Load_UI(buffe);
                }
            }
        }

        public static void EXUserInfo(ByteBuffer buffe)
        {
            int loc = buffe.GetIndex();
            uint ID = buffe.ReadUInt32();//A0 B9 B0 49
            buffe.ReadInt32();
            buffe.ReadInt32();
            buffe.ReadInt32();
            buffe.ReadInt32();
            buffe.ReadByte();
            string name = buffe.ReadString();

            buffe.SetIndex(loc);

            //this packet only gets sent once
            if (name == Globals.gamedata.my_char.Name)
            {
                Globals.gamedata.my_char.Load_UserEX(buffe);
                AddInfo.Set_Char_Info();
                if (Globals.gamedata.teleported)
                {
                    Globals.gamedata.teleported = false;

                    GameServer.CleanUp();
                }

                //we are fully in the world now
                Globals.FulllyIn = true;
            }
            else
            {
                //Update stuff
                if (ID == Globals.gamedata.my_char.ID)
                {
                    //do things
                }
            }
        }


        public static void PetStatusShow(ByteBuffer buffe)
        {
            Globals.gamedata.my_pet.SummonType = buffe.ReadUInt32();
        }

        public static void PetInfo(ByteBuffer buffe)
        {
            //Oddi: I know this is really ugly, todo: make it better :D
            if (Globals.gamedata.Chron >= Chronicle.CT3_0)
            {
                int loc = buffe.GetIndex();
                buffe.ReadInt32();
                uint ID = buffe.ReadUInt32();//A0 B9 B0 49
                buffe.SetIndex(loc);

                if (ID == Globals.gamedata.my_pet.ID)
                {
                    Globals.gamedata.my_pet.Load_Pet(buffe);
                }
                else if (ID == Globals.gamedata.my_pet1.ID)
                {
                    Globals.gamedata.my_pet1.Load_Pet(buffe);
                }
                else if (ID == Globals.gamedata.my_pet2.ID)
                {
                    Globals.gamedata.my_pet2.Load_Pet(buffe);
                }
                else if (ID == Globals.gamedata.my_pet3.ID)
                {
                    Globals.gamedata.my_pet3.Load_Pet(buffe);
                }
                else
                {
                    if (Globals.gamedata.my_pet.ID == 0)
                    {
                        Globals.gamedata.my_pet.Load_Pet(buffe);
                    }
                    else if (Globals.gamedata.my_pet1.ID == 0)
                    {
                        Globals.gamedata.my_pet1.Load_Pet(buffe);
                    }
                    else if (Globals.gamedata.my_pet2.ID == 0)
                    {
                        Globals.gamedata.my_pet2.Load_Pet(buffe);
                    }
                    else if (Globals.gamedata.my_pet3.ID == 0)
                    {
                        Globals.gamedata.my_pet3.Load_Pet(buffe);
                    }
                    else //Fallback, this should never happend
                    {
                        Globals.gamedata.my_pet.Load_Pet(buffe);
                    }

                }
            }
            else
            {
                Globals.gamedata.my_pet.Load_Pet(buffe);
            }
            if (Globals.petwindow != null)
            {
                AddInfo.Set_Pet_Info();
            }
        }

        public static void PetDelete(ByteBuffer buffe)
        {
            //need to remove the pet...
            if (Globals.gamedata.Chron >= Chronicle.CT3_0)
            {
                //writeC(0xb7);
                //writeD(_petType); // 1 = any summon (reapers, lions, healer's tree etc); 2 = any pet ( striders, wolfs, etc)
                //writeD(_petObjId); // ID of deleted summon, pet 
                buffe.ReadInt32();
                uint ID = buffe.ReadUInt32();
                if (ID == Globals.gamedata.my_pet.ID)
                {
                    Globals.gamedata.my_pet.ID = 0;
                }
                else if (ID == Globals.gamedata.my_pet1.ID)
                {
                    Globals.gamedata.my_pet1.ID = 0;
                }
                else if (ID == Globals.gamedata.my_pet2.ID)
                {
                    Globals.gamedata.my_pet2.ID = 0;
                }
                else if (ID == Globals.gamedata.my_pet3.ID)
                {
                    Globals.gamedata.my_pet3.ID = 0;
                }
                else  //Fallback, this should never happend
                {
                    Globals.gamedata.my_pet.ID = 0;
                }

            }
            else
            {
                Globals.gamedata.my_pet.ID = 0;
            }
        }

        public static void PetStatusUpdate(ByteBuffer buffe)
        {
            if (Globals.gamedata.Chron >= Chronicle.CT3_0)
            {
                int loc = buffe.GetIndex();
                buffe.ReadInt32();
                uint ID = buffe.ReadUInt32();//A0 B9 B0 49
                buffe.SetIndex(loc);
                if (ID == Globals.gamedata.my_pet.ID)
                {
                    Globals.gamedata.my_pet.PetUpdate(buffe);
                }
                else if (ID == Globals.gamedata.my_pet1.ID)
                {
                    Globals.gamedata.my_pet1.PetUpdate(buffe);
                }
                else if (ID == Globals.gamedata.my_pet2.ID)
                {
                    Globals.gamedata.my_pet2.PetUpdate(buffe);
                }
                else if (ID == Globals.gamedata.my_pet3.ID)
                {
                    Globals.gamedata.my_pet3.PetUpdate(buffe);
                }
            }
            else
            {
                Globals.gamedata.my_pet.PetUpdate(buffe);
            }
            if (Globals.petwindow != null)
            {
                AddInfo.Set_Pet_Info();
            }
        }

        public static void CharInfo(ByteBuffer buffe)
        {
            CharInfo ch_inf = new CharInfo();
            ch_inf.Load(buffe);

            AddInfo.Add_CharInfo(ch_inf);

            //gotta add the char to the list before we can search for it to follow it
            if (System.String.Equals(ch_inf.Name.ToUpperInvariant(), Globals.gamedata.botoptions.ActiveFollowName.ToUpperInvariant()))
            {
                Globals.gamedata.botoptions.Set_ActiveFollow(Globals.gamedata.botoptions.ActiveFollowName);
            }
        }

        public static void RelationChanged(ByteBuffer buffe)
        {
            /*
		    RELATION_PARTY1       = 0x00001; // party member
		    RELATION_PARTY2       = 0x00002; // party member
		    RELATION_PARTY3       = 0x00004; // party member
		    RELATION_PARTY4       = 0x00008; // party member (for information, see L2PcInstance.getRelation())
		    RELATION_PARTYLEADER  = 0x00010; // true if is party leader
		    RELATION_HAS_PARTY    = 0x00020; // true if is in party
		    RELATION_CLAN_MEMBER  = 0x00040; // true if is in clan
		    RELATION_LEADER 	  = 0x00080; // true if is clan leader
		    RELATION_CLAN_MATE    = 0x00100; // true if is in same clan
		    RELATION_INSIEGE   	  = 0x00200; // true if in siege
		    RELATION_ATTACKER     = 0x00400; // true when attacker
		    RELATION_ALLY         = 0x00800; // blue siege icon, cannot have if red
		    RELATION_ENEMY        = 0x01000; // true when red icon, doesn't matter with blue
		    RELATION_MUTUAL_WAR   = 0x04000; // double fist
		    RELATION_1SIDED_WAR   = 0x08000; // single fist
		    RELATION_ALLY_MEMBER  = 0x10000; // clan is in alliance
		    RELATION_TERRITORY_WAR= 0x80000; // show Territory War icon								
            */
            
            if (Globals.gamedata.Chron >= Chronicle.CT2_2)
            {
                uint char_id, combat, pvpflag, relation;
                int multi = 0, karma;
                string char_name;
                // int relation;

                if (Globals.gamedata.Chron >= Chronicle.CT2_4)
                {
                    multi = buffe.ReadInt32();
                    // Globals.l2net_home.Add_Text("ClientPackets.cs -> multi: " + multi, Globals.Cyan, TextType.BOT);
                    for (int i = 1; i <= multi; i++)
                    {
                        char_id = buffe.ReadUInt32();
                        relation = buffe.ReadUInt32();
                        combat = buffe.ReadUInt32();
                        karma = buffe.ReadInt32();
                        pvpflag = buffe.ReadUInt32();
                        char_name = Util.GetCharName(char_id);
                        // Globals.l2net_home.Add_Text("ClientPackets.cs -> Name: " + char_name + " ID: " + char_id + " Relation: " + relation, Globals.Cyan, TextType.BOT);
                        AddInfo.Set_Relation(char_id, relation);
                        bool they_declared = false;
                        bool we_declared = false;

                        if ((relation & 0x04000) >= 1)
                        {
                            they_declared = true;
                        }
                        if ((relation & 0x08000) >= 1)
                        {
                            we_declared = true;
                        }

                        if (they_declared && we_declared)
                        {
                            //2 way war
                            AddInfo.Set_WarState(char_id, 2, combat, karma, pvpflag);
                        }
                        else
                        {
                            if (they_declared)
                            {
                                //-1 way war
                                AddInfo.Set_WarState(char_id, -1, combat, karma, pvpflag);
                            }
                            else if (we_declared)
                            {
                                //1 way war
                                AddInfo.Set_WarState(char_id, 1, combat, karma, pvpflag);
                            }
                            else
                            {
                                AddInfo.Set_WarState(char_id, 0, combat, karma, pvpflag);
                            }     

                        }
                    }
                }
                else
                {
                    char_id = buffe.ReadUInt32();
                    relation = buffe.ReadUInt32();
                    combat = buffe.ReadUInt32();
                    karma = buffe.ReadInt32();
                    pvpflag = buffe.ReadUInt32();

                    bool they_declared = false;
                    bool we_declared = false;

                    if ((relation & 0x04000) >= 1)
                    {
                        they_declared = true;
                    }
                    if ((relation & 0x08000) >= 1)
                    {
                        we_declared = true;
                    }

                    if (they_declared && we_declared)
                    {
                        //2 way war
                        AddInfo.Set_WarState(char_id, 2, combat, karma, pvpflag);
                    }
                    else if (they_declared)
                    {
                        //-1 way war
                        AddInfo.Set_WarState(char_id, -1, combat, karma, pvpflag);
                    }
                    else if (we_declared)
                    {
                        //1 way war
                        AddInfo.Set_WarState(char_id, 1, combat, karma, pvpflag);
                    }
                    else
                    {
                        AddInfo.Set_WarState(char_id, 0, combat, karma, pvpflag);
                    }
                }				
            }
            else
            {
                uint char_id = buffe.ReadUInt32();

                uint weird = buffe.ReadByte();//0x40 or 0x58//byte
                uint them = buffe.ReadByte();//byte
                uint us = buffe.ReadByte();//byte
                buffe.ReadByte();

                uint combat = buffe.ReadByte();//in combat//byte
                buffe.ReadByte();
                buffe.ReadByte();
                buffe.ReadByte();

                buffe.ReadUInt32();
                buffe.ReadUInt32();

                //them: 0 = 0x00
                //them: 1 = 0x80
                bool w_them = false;
                if (them == 0x80)
                {
                    w_them = true;
                }

                //us: 0 = 0x02
                //us: 1 = 0x03
                bool w_us = false;
                if (us == 0x01)
                {
                    w_us = true;
                }
                if (us == 0x03)
                {
                    w_us = true;
                }

                if (w_them && w_us)
                {
                    //2 way
                    AddInfo.Set_WarState(char_id, 2, combat);
                }
                else if (w_them)
                {
                    //-1 way
                    AddInfo.Set_WarState(char_id, -1, combat);
                }
                else if (w_us)
                {
                    //1 way
                    AddInfo.Set_WarState(char_id, 1, combat);
                }
                else
                {
                    AddInfo.Set_WarState(char_id, 0, combat);
                }
            }
        }

        public static void DeleteItem(ByteBuffer buffe)
        {
            uint dead_object = buffe.ReadUInt32();//System.BitConverter.ToInt32(buffe,1);
            //buffe.ReadUInt32();
            //4 bytes of poop after the objid

            if (Globals.gamedata.my_char.MoveTarget == dead_object)
            {
                Globals.gamedata.my_char.Moving = false;
                Globals.gamedata.my_char.MoveTarget = 0;
                Globals.gamedata.my_char.MoveTargetType = TargetType.NONE;
                Globals.gamedata.my_char.Dest_X = Globals.gamedata.my_char.X;
                Globals.gamedata.my_char.Dest_Y = Globals.gamedata.my_char.Y;
                Globals.gamedata.my_char.Dest_Z = Globals.gamedata.my_char.Z;
            }

            if (Globals.gamedata.my_pet.MoveTarget == dead_object)
            {
                Globals.gamedata.my_pet.Moving = false;
                Globals.gamedata.my_pet.MoveTarget = 0;
                Globals.gamedata.my_pet.MoveTargetType = TargetType.NONE;
                Globals.gamedata.my_pet.Dest_X = Globals.gamedata.my_char.X;
                Globals.gamedata.my_pet.Dest_Y = Globals.gamedata.my_char.Y;
                Globals.gamedata.my_pet.Dest_Z = Globals.gamedata.my_char.Z;
            }
            if (Globals.gamedata.my_pet1.MoveTarget == dead_object)
            {
                Globals.gamedata.my_pet1.Moving = false;
                Globals.gamedata.my_pet1.MoveTarget = 0;
                Globals.gamedata.my_pet1.MoveTargetType = TargetType.NONE;
                Globals.gamedata.my_pet1.Dest_X = Globals.gamedata.my_char.X;
                Globals.gamedata.my_pet1.Dest_Y = Globals.gamedata.my_char.Y;
                Globals.gamedata.my_pet1.Dest_Z = Globals.gamedata.my_char.Z;
            }
            if (Globals.gamedata.my_pet2.MoveTarget == dead_object)
            {
                Globals.gamedata.my_pet2.Moving = false;
                Globals.gamedata.my_pet2.MoveTarget = 0;
                Globals.gamedata.my_pet2.MoveTargetType = TargetType.NONE;
                Globals.gamedata.my_pet2.Dest_X = Globals.gamedata.my_char.X;
                Globals.gamedata.my_pet2.Dest_Y = Globals.gamedata.my_char.Y;
                Globals.gamedata.my_pet2.Dest_Z = Globals.gamedata.my_char.Z;
            }
            if (Globals.gamedata.my_pet3.MoveTarget == dead_object)
            {
                Globals.gamedata.my_pet3.Moving = false;
                Globals.gamedata.my_pet3.MoveTarget = 0;
                Globals.gamedata.my_pet3.MoveTargetType = TargetType.NONE;
                Globals.gamedata.my_pet3.Dest_X = Globals.gamedata.my_char.X;
                Globals.gamedata.my_pet3.Dest_Y = Globals.gamedata.my_char.Y;
                Globals.gamedata.my_pet3.Dest_Z = Globals.gamedata.my_char.Z;
            }

            Globals.PlayerLock.EnterReadLock();
            try
            {
                foreach (CharInfo player in Globals.gamedata.nearby_chars.Values)
                {
                    if (player.MoveTarget == dead_object)
                    {
                        player.Moving = false;
                        player.MoveTarget = 0;
                        player.MoveTargetType = TargetType.NONE;
                        player.Dest_X = player.X;
                        player.Dest_Y = player.Y;
                        player.Dest_Z = player.Z;
                    }
                }
            }
            catch
            {
                //oops
            }
            finally
            {
                Globals.PlayerLock.ExitReadLock();
            }

            Globals.NPCLock.EnterReadLock();
            try
            {
                foreach (NPCInfo npc in Globals.gamedata.nearby_npcs.Values)
                {
                    if (npc.MoveTarget == dead_object)
                    {
                        npc.Moving = false;
                        npc.MoveTarget = 0;
                        npc.MoveTargetType = TargetType.NONE;
                        npc.Dest_X = npc.X;
                        npc.Dest_Y = npc.Y;
                        npc.Dest_Z = npc.Z;
                    }
                }
            }
            catch
            {
                //oops
            }
            finally
            {
                Globals.NPCLock.ExitReadLock();
            }

            switch (Util.GetType(dead_object))
            {
                case TargetType.PLAYER:
                    AddInfo.Remove_CharInfo(dead_object);
                    Globals.l2net_home.timer_players.Start();
                    break;
                case TargetType.MYPET:
                    Globals.gamedata.my_pet.ID = 0;
                    break;
                case TargetType.MYPET1:
                    Globals.gamedata.my_pet1.ID = 0;
                    break;
                case TargetType.MYPET2:
                    Globals.gamedata.my_pet2.ID = 0;
                    break;
                case TargetType.MYPET3:
                    Globals.gamedata.my_pet3.ID = 0;
                    break;
                case TargetType.NPC:
                    AddInfo.Remove_NPCInfo(dead_object);
                    Globals.l2net_home.timer_npcs.Start();
                    break;
                case TargetType.ITEM:
                    AddInfo.Remove_Item(dead_object);
                    Globals.l2net_home.timer_items.Start();
                    break;
                /*default:
                    //maybe this is an inventory item? ... stupid ncsoft
                    AddInfo.Remove_Inventory(dead_object);
                    break;*/
            }

            //need to check if anything had this targeted and set it's Dest_ to the current location
        }

        public static void StatusUpdate(ByteBuffer buffe)
        {
            uint data1 = buffe.ReadUInt32();//ID


            buffe.ReadUInt32(); //??

            buffe.ReadByte(); //1 or 0
            uint data2 = buffe.ReadByte(); //count

            TargetType type = Util.GetType(data1);

            switch (type)
            {
                case TargetType.SELF:
                    for (uint i = 0; i < data2; i++)
                    {
                        Globals.gamedata.my_char.Update(buffe);
                    }
                    AddInfo.Set_Char_Info_Basic();
                    BroadcastThread.SendSelfStatus();
                    break;
                case TargetType.MYPET:
                    for (uint i = 0; i < data2; i++)
                    {
                        Globals.gamedata.my_pet.Update(buffe);
                    }
                    BroadcastThread.SendSelfStatus();
                    break;
                case TargetType.MYPET1:
                    for (uint i = 0; i < data2; i++)
                    {
                        Globals.gamedata.my_pet1.Update(buffe);
                    }
                    BroadcastThread.SendSelfStatus();
                    break;
                case TargetType.MYPET2:
                    for (uint i = 0; i < data2; i++)
                    {
                        Globals.gamedata.my_pet2.Update(buffe);
                    }
                    BroadcastThread.SendSelfStatus();
                    break;
                case TargetType.MYPET3:
                    for (uint i = 0; i < data2; i++)
                    {
                        Globals.gamedata.my_pet3.Update(buffe);
                    }
                    BroadcastThread.SendSelfStatus();
                    break;
                case TargetType.PLAYER:
                    Globals.PlayerLock.EnterReadLock();
                    try
                    {
                        CharInfo player = Util.GetChar(data1);

                        if (player != null)
                        {
                            for (uint i2 = 0; i2 < data2; i2++)
                            {
                                player.Update(buffe);
                            }
                        }
                    }//unlock
                    finally
                    {
                        Globals.PlayerLock.ExitReadLock();
                    }
                    break;
                case TargetType.NPC:
                    Globals.NPCLock.EnterReadLock();
                    try
                    {
                        NPCInfo npc = Util.GetNPC(data1);

                        if (npc != null)
                        {
                            Globals.l2net_home.Add_Text("Got npc: " + npc.Name);
                            for (uint i2 = 0; i2 < data2; i2++)
                            {
                                npc.Update(buffe);
                            }
                        }
                    }//unlock
                    finally
                    {
                        Globals.NPCLock.ExitReadLock();
                    }
                    break;
            }

            if (data1 == Globals.gamedata.my_char.TargetID)
            {
                AddInfo.Set_Target_HP();
            }
        }

        public static void StopMove(ByteBuffer buffe)
        {
            uint data1 = buffe.ReadUInt32();
            int dx = buffe.ReadInt32();
            int dy = buffe.ReadInt32();
            int dz = buffe.ReadInt32();
            int dh = buffe.ReadInt32();

            TargetType type = Util.GetType(data1);

            switch (type)
            {
                case TargetType.SELF:
                    Globals.gamedata.my_char.Moving = false;
                    Globals.gamedata.my_char.MoveTarget = 0;
                    Globals.gamedata.my_char.MoveTargetType = TargetType.NONE;
                    Globals.gamedata.my_char.X = dx;
                    Globals.gamedata.my_char.Y = dy;
                    Globals.gamedata.my_char.Z = dz;
                    Globals.gamedata.my_char.Dest_X = dx;
                    Globals.gamedata.my_char.Dest_Y = dy;
                    Globals.gamedata.my_char.Dest_Z = dz;
                    Globals.gamedata.my_char.Heading = dh;
                    //Globals.gamedata.my_char.Clear_Botting_Buffing(false);

                    // More smarts for USE_SKILL_SMART
                    // This is one of the scenarios that would normally
                    // cause USE_SKILL_SMART to sleep forever unless we react by setting HitTime to 1.
                    Globals.gamedata.my_char.ExpiresTime = 1;
                    // Globals.l2net_home.Add_Text("StopMove setting ExpiresTime to 1 (" + Globals.gamedata.my_char.ExpiresTime + ")", Globals.Cyan, TextType.BOT);

                    if (Globals.gamedata.CurrentScriptState == ScriptState.Running)
                    {
                        ScriptEvent sc_ev = new ScriptEvent();
                        sc_ev.Type = EventType.SelfStopMove;
                        ScriptEngine.SendToEventQueue(sc_ev);
                    }
                    break;
                case TargetType.MYPET:
                    Globals.gamedata.my_pet.Moving = false;
                    Globals.gamedata.my_pet.MoveTarget = 0;
                    Globals.gamedata.my_pet.MoveTargetType = TargetType.NONE;
                    Globals.gamedata.my_pet.X = dx;
                    Globals.gamedata.my_pet.Y = dy;
                    Globals.gamedata.my_pet.Z = dz;
                    Globals.gamedata.my_pet.Dest_X = dx;
                    Globals.gamedata.my_pet.Dest_Y = dy;
                    Globals.gamedata.my_pet.Dest_Z = dz;
                    Globals.gamedata.my_pet.Heading = dh;
                    break;
                case TargetType.MYPET1:
                    Globals.gamedata.my_pet1.Moving = false;
                    Globals.gamedata.my_pet1.MoveTarget = 0;
                    Globals.gamedata.my_pet1.MoveTargetType = TargetType.NONE;
                    Globals.gamedata.my_pet1.X = dx;
                    Globals.gamedata.my_pet1.Y = dy;
                    Globals.gamedata.my_pet1.Z = dz;
                    Globals.gamedata.my_pet1.Dest_X = dx;
                    Globals.gamedata.my_pet1.Dest_Y = dy;
                    Globals.gamedata.my_pet1.Dest_Z = dz;
                    Globals.gamedata.my_pet1.Heading = dh;
                    break;
                case TargetType.MYPET2:
                    Globals.gamedata.my_pet2.Moving = false;
                    Globals.gamedata.my_pet2.MoveTarget = 0;
                    Globals.gamedata.my_pet2.MoveTargetType = TargetType.NONE;
                    Globals.gamedata.my_pet2.X = dx;
                    Globals.gamedata.my_pet2.Y = dy;
                    Globals.gamedata.my_pet2.Z = dz;
                    Globals.gamedata.my_pet2.Dest_X = dx;
                    Globals.gamedata.my_pet2.Dest_Y = dy;
                    Globals.gamedata.my_pet2.Dest_Z = dz;
                    Globals.gamedata.my_pet2.Heading = dh;
                    break;
                case TargetType.MYPET3:
                    Globals.gamedata.my_pet3.Moving = false;
                    Globals.gamedata.my_pet3.MoveTarget = 0;
                    Globals.gamedata.my_pet3.MoveTargetType = TargetType.NONE;
                    Globals.gamedata.my_pet3.X = dx;
                    Globals.gamedata.my_pet3.Y = dy;
                    Globals.gamedata.my_pet3.Z = dz;
                    Globals.gamedata.my_pet3.Dest_X = dx;
                    Globals.gamedata.my_pet3.Dest_Y = dy;
                    Globals.gamedata.my_pet3.Dest_Z = dz;
                    Globals.gamedata.my_pet3.Heading = dh;
                    break;
                case TargetType.PLAYER:
                    Globals.PlayerLock.EnterReadLock();
                    try
                    {
                        CharInfo player = Util.GetChar(data1);

                        if (player != null)
                        {
                            player.Moving = false;
                            player.MoveTarget = 0;
                            player.MoveTargetType = TargetType.NONE;
                            player.X = dx;
                            player.Y = dy;
                            player.Z = dz;
                            player.Dest_X = dx;
                            player.Dest_Y = dy;
                            player.Dest_Z = dz;
                            player.Heading = dh;
                        }
                    }
                    finally
                    {
                        Globals.PlayerLock.ExitReadLock();
                    }
                    break;
                case TargetType.NPC:
                    Globals.NPCLock.EnterReadLock();
                    try
                    {
                        NPCInfo npc = Util.GetNPC(data1);

                        if (npc != null)
                        {
                            npc.Moving = false;
                            npc.MoveTarget = 0;
                            npc.MoveTargetType = TargetType.NONE;
                            npc.X = dx;
                            npc.Y = dy;
                            npc.Z = dz;
                            npc.Dest_X = dx;
                            npc.Dest_Y = dy;
                            npc.Dest_Z = dz;
                            npc.Heading = dh;
                        }
                    }
                    finally
                    {
                        Globals.NPCLock.ExitReadLock();
                    }
                    break;
            }
        }

        public static void MoveToLocation(ByteBuffer buffe)
        {
            uint data1 = buffe.ReadUInt32();
            int dx = buffe.ReadInt32();
            int dy = buffe.ReadInt32();
            int dz = buffe.ReadInt32();
            int ox = buffe.ReadInt32();
            int oy = buffe.ReadInt32();
            int oz = buffe.ReadInt32();

            TargetType type = Util.GetType(data1);

            if (dx == ox && dy == oy && dz == oz)
            {
                switch (type)
                {
                    case TargetType.SELF:
                        //Globals.gamedata.my_char.Clear_Botting_Buffing(false);
                        Globals.gamedata.my_char.X = ox;
                        Globals.gamedata.my_char.Y = oy;
                        Globals.gamedata.my_char.Z = oz;
                        Globals.gamedata.my_char.Dest_X = dx;
                        Globals.gamedata.my_char.Dest_Y = dy;
                        Globals.gamedata.my_char.Dest_Z = dz;
                        Globals.gamedata.my_char.Moving = false;
                        Globals.gamedata.my_char.MoveTarget = 0;
                        Globals.gamedata.my_char.MoveTargetType = 0;
                        Globals.gamedata.my_char.lastMoveTime = System.DateTime.Now;
                        break;
                    case TargetType.MYPET:
                        Globals.gamedata.my_pet.X = ox;
                        Globals.gamedata.my_pet.Y = oy;
                        Globals.gamedata.my_pet.Z = oz;
                        Globals.gamedata.my_pet.Dest_X = dx;
                        Globals.gamedata.my_pet.Dest_Y = dy;
                        Globals.gamedata.my_pet.Dest_Z = dz;
                        Globals.gamedata.my_pet.Moving = false;
                        Globals.gamedata.my_pet.MoveTarget = 0;
                        Globals.gamedata.my_pet.MoveTargetType = 0;
                        Globals.gamedata.my_pet.lastMoveTime = System.DateTime.Now;
                        break;
                    case TargetType.MYPET1:
                        Globals.gamedata.my_pet1.X = ox;
                        Globals.gamedata.my_pet1.Y = oy;
                        Globals.gamedata.my_pet1.Z = oz;
                        Globals.gamedata.my_pet1.Dest_X = dx;
                        Globals.gamedata.my_pet1.Dest_Y = dy;
                        Globals.gamedata.my_pet1.Dest_Z = dz;
                        Globals.gamedata.my_pet1.Moving = false;
                        Globals.gamedata.my_pet1.MoveTarget = 0;
                        Globals.gamedata.my_pet1.MoveTargetType = 0;
                        Globals.gamedata.my_pet1.lastMoveTime = System.DateTime.Now;
                        break;
                    case TargetType.MYPET2:
                        Globals.gamedata.my_pet2.X = ox;
                        Globals.gamedata.my_pet2.Y = oy;
                        Globals.gamedata.my_pet2.Z = oz;
                        Globals.gamedata.my_pet2.Dest_X = dx;
                        Globals.gamedata.my_pet2.Dest_Y = dy;
                        Globals.gamedata.my_pet2.Dest_Z = dz;
                        Globals.gamedata.my_pet2.Moving = false;
                        Globals.gamedata.my_pet2.MoveTarget = 0;
                        Globals.gamedata.my_pet2.MoveTargetType = 0;
                        Globals.gamedata.my_pet2.lastMoveTime = System.DateTime.Now;
                        break;
                    case TargetType.MYPET3:
                        Globals.gamedata.my_pet3.X = ox;
                        Globals.gamedata.my_pet3.Y = oy;
                        Globals.gamedata.my_pet3.Z = oz;
                        Globals.gamedata.my_pet3.Dest_X = dx;
                        Globals.gamedata.my_pet3.Dest_Y = dy;
                        Globals.gamedata.my_pet3.Dest_Z = dz;
                        Globals.gamedata.my_pet3.Moving = false;
                        Globals.gamedata.my_pet3.MoveTarget = 0;
                        Globals.gamedata.my_pet3.MoveTargetType = 0;
                        Globals.gamedata.my_pet3.lastMoveTime = System.DateTime.Now;
                        break;
                    case TargetType.PLAYER:
                        Globals.PlayerLock.EnterReadLock();
                        try
                        {
                            CharInfo player = Util.GetChar(data1);

                            if (player != null)
                            {
                                player.X = ox;
                                player.Y = oy;
                                player.Z = oz;
                                player.Dest_X = dx;
                                player.Dest_Y = dy;
                                player.Dest_Z = dz;
                                player.Moving = false;
                                player.MoveTarget = 0;
                                player.MoveTargetType = 0;
                                player.lastMoveTime = System.DateTime.Now;
                            }
                        }//unlock
                        finally
                        {
                            Globals.PlayerLock.ExitReadLock();
                        }
                        break;
                    case TargetType.NPC:
                        Globals.NPCLock.EnterReadLock();
                        try
                        {
                            NPCInfo npc = Util.GetNPC(data1);

                            if (npc != null)
                            {
                                npc.X = ox;
                                npc.Y = oy;
                                npc.Z = oz;
                                npc.Dest_X = dx;
                                npc.Dest_Y = dy;
                                npc.Dest_Z = dz;
                                npc.Moving = false;
                                npc.MoveTarget = 0;
                                npc.MoveTargetType = 0;
                                npc.lastMoveTime = System.DateTime.Now;
                            }
                        }//unlock
                        finally
                        {
                            Globals.NPCLock.ExitReadLock();
                        }
                        break;
                }
            }
            else
            {
                switch (type)
                {
                    case TargetType.SELF:
                        //Globals.gamedata.my_char.Clear_Botting_Buffing(false);
                        Globals.gamedata.my_char.X = ox;
                        Globals.gamedata.my_char.Y = oy;
                        Globals.gamedata.my_char.Z = oz;
                        Globals.gamedata.my_char.Dest_X = dx;
                        Globals.gamedata.my_char.Dest_Y = dy;
                        Globals.gamedata.my_char.Dest_Z = dz;
                        Globals.gamedata.my_char.Moving = true;
                        Globals.gamedata.my_char.MoveTarget = 0;
                        Globals.gamedata.my_char.MoveTargetType = 0;
                        Globals.gamedata.my_char.lastMoveTime = System.DateTime.Now;
                        break;
                    case TargetType.MYPET:
                        Globals.gamedata.my_pet.X = ox;
                        Globals.gamedata.my_pet.Y = oy;
                        Globals.gamedata.my_pet.Z = oz;
                        Globals.gamedata.my_pet.Dest_X = dx;
                        Globals.gamedata.my_pet.Dest_Y = dy;
                        Globals.gamedata.my_pet.Dest_Z = dz;
                        Globals.gamedata.my_pet.Moving = true;
                        Globals.gamedata.my_pet.MoveTarget = 0;
                        Globals.gamedata.my_pet.MoveTargetType = 0;
                        Globals.gamedata.my_pet.lastMoveTime = System.DateTime.Now;
                        break;
                    case TargetType.MYPET1:
                        Globals.gamedata.my_pet1.X = ox;
                        Globals.gamedata.my_pet1.Y = oy;
                        Globals.gamedata.my_pet1.Z = oz;
                        Globals.gamedata.my_pet1.Dest_X = dx;
                        Globals.gamedata.my_pet1.Dest_Y = dy;
                        Globals.gamedata.my_pet1.Dest_Z = dz;
                        Globals.gamedata.my_pet1.Moving = true;
                        Globals.gamedata.my_pet1.MoveTarget = 0;
                        Globals.gamedata.my_pet1.MoveTargetType = 0;
                        Globals.gamedata.my_pet1.lastMoveTime = System.DateTime.Now;
                        break;
                    case TargetType.MYPET2:
                        Globals.gamedata.my_pet2.X = ox;
                        Globals.gamedata.my_pet2.Y = oy;
                        Globals.gamedata.my_pet2.Z = oz;
                        Globals.gamedata.my_pet2.Dest_X = dx;
                        Globals.gamedata.my_pet2.Dest_Y = dy;
                        Globals.gamedata.my_pet2.Dest_Z = dz;
                        Globals.gamedata.my_pet2.Moving = true;
                        Globals.gamedata.my_pet2.MoveTarget = 0;
                        Globals.gamedata.my_pet2.MoveTargetType = 0;
                        Globals.gamedata.my_pet2.lastMoveTime = System.DateTime.Now;
                        break;
                    case TargetType.MYPET3:
                        Globals.gamedata.my_pet3.X = ox;
                        Globals.gamedata.my_pet3.Y = oy;
                        Globals.gamedata.my_pet3.Z = oz;
                        Globals.gamedata.my_pet3.Dest_X = dx;
                        Globals.gamedata.my_pet3.Dest_Y = dy;
                        Globals.gamedata.my_pet3.Dest_Z = dz;
                        Globals.gamedata.my_pet3.Moving = true;
                        Globals.gamedata.my_pet3.MoveTarget = 0;
                        Globals.gamedata.my_pet3.MoveTargetType = 0;
                        Globals.gamedata.my_pet3.lastMoveTime = System.DateTime.Now;
                        break;
                    case TargetType.PLAYER:
                        Globals.PlayerLock.EnterReadLock();
                        try
                        {
                            CharInfo player = Util.GetChar(data1);

                            if (player != null)
                            {
                                player.X = ox;
                                player.Y = oy;
                                player.Z = oz;
                                player.Dest_X = dx;
                                player.Dest_Y = dy;
                                player.Dest_Z = dz;
                                player.Moving = true;
                                player.MoveTarget = 0;
                                player.MoveTargetType = 0;
                                player.lastMoveTime = System.DateTime.Now;
                            }
                        }//unlock
                        finally
                        {
                            Globals.PlayerLock.ExitReadLock();
                        }
                        break;
                    case TargetType.NPC:
                        Globals.NPCLock.EnterReadLock();
                        try
                        {
                            NPCInfo npc = Util.GetNPC(data1);

                            if (npc != null)
                            {
                                npc.X = ox;
                                npc.Y = oy;
                                npc.Z = oz;
                                npc.Dest_X = dx;
                                npc.Dest_Y = dy;
                                npc.Dest_Z = dz;
                                npc.Moving = true;
                                npc.MoveTarget = 0;
                                npc.MoveTargetType = 0;
                                npc.lastMoveTime = System.DateTime.Now;
                            }
                        }//unlock
                        finally
                        {
                            Globals.NPCLock.ExitReadLock();
                        }
                        break;
                }
            }
        }

        public static void EnterCombat(ByteBuffer inp, bool incombat)
        {
            uint _ID = inp.ReadUInt32();
            uint combat;//byte

            if (incombat)
            {
                combat = 1;
            }
            else
            {
                combat = 0;
            }

            TargetType type = Util.GetType(_ID);

            switch (type)
            {
                case TargetType.SELF:
                    Globals.gamedata.my_char.isInCombat = combat;

                    if (Globals.gamedata.CurrentScriptState == ScriptState.Running)
                    {
                        ScriptEvent sc_ev = new ScriptEvent();
                        if (incombat)
                            sc_ev.Type = EventType.SelfEnterCombat;
                        else
                            sc_ev.Type = EventType.SelfLeaveCombat;
                        ScriptEngine.SendToEventQueue(sc_ev);
                    }
                    break;
                case TargetType.MYPET:
                    Globals.gamedata.my_pet.isInCombat = combat;
                    break;
                case TargetType.MYPET1:
                    Globals.gamedata.my_pet1.isInCombat = combat;
                    break;
                case TargetType.MYPET2:
                    Globals.gamedata.my_pet2.isInCombat = combat;
                    break;
                case TargetType.MYPET3:
                    Globals.gamedata.my_pet3.isInCombat = combat;
                    break;
                case TargetType.PLAYER:
                    Globals.PlayerLock.EnterReadLock();
                    try
                    {
                        CharInfo player = Util.GetChar(_ID);

                        if (player != null)
                        {
                            player.isInCombat = combat;
                        }
                    }
                    finally
                    {
                        Globals.PlayerLock.ExitReadLock();
                    }
                    break;
                case TargetType.NPC:
                    Globals.NPCLock.EnterReadLock();
                    try
                    {
                        NPCInfo npc = Util.GetNPC(_ID);

                        if (npc != null)
                        {
                            npc.isInCombat = combat;
                        }
                    }
                    finally
                    {
                        Globals.NPCLock.ExitReadLock();
                    }
                    break;
            }
        }

        public static void AttackCanceled_Packet(ByteBuffer inp)
        {
            //int id

            //do we even give a shit?
        }

        public static void Attack_Packet(ByteBuffer inp)
        {
            uint attacker = inp.ReadUInt32();//System.BitConverter.ToInt32(inp,1);
            uint target = inp.ReadUInt32();//System.BitConverter.ToInt32(inp,5);
            //5 bytes (1 0 0 0 0)
            //x
            //y
            //z
            //2 bytes (0 0)
            if (attacker == Globals.gamedata.my_char.ID)
            {
                //Globals.l2net_home.Add_Text("I'm attacking something", Globals.White, TextType.BOT);
                Globals.gamedata.my_char.isAttacking = true;
            }

            StopMoveStartCombat(attacker, true);

            TargetType type = Util.GetType(target);

            //force target for the npc
            Globals.NPCLock.EnterReadLock();
            try
            {
                NPCInfo npc = Util.GetNPC(attacker);

                if (npc != null)
                {
                    npc.TargetID = target;
                    npc.CurrentTargetType = type;
                }
            }
            finally
            {
                Globals.NPCLock.ExitReadLock();
            }
            //end of force target for the npc

            bool found = false;

            //this is our active follow attack crap
            if (Globals.gamedata.BOTTING && (Globals.gamedata.botoptions.ActiveFollowAttack == 1 && !Globals.picking_up_items) && (Globals.gamedata.botoptions.ActiveFollowID == attacker) && Globals.gamedata.ReadyState())
            {
                found = false;
                float x = 0, y = 0, z = 0;

                switch (type)
                {
                    case TargetType.MYPET:
                        x = Globals.gamedata.my_pet.X;
                        y = Globals.gamedata.my_pet.Y;
                        z = Globals.gamedata.my_pet.Z;
                        found = true;
                        break;
                    case TargetType.MYPET1:
                        x = Globals.gamedata.my_pet1.X;
                        y = Globals.gamedata.my_pet1.Y;
                        z = Globals.gamedata.my_pet1.Z;
                        found = true;
                        break;
                    case TargetType.MYPET2:
                        x = Globals.gamedata.my_pet2.X;
                        y = Globals.gamedata.my_pet2.Y;
                        z = Globals.gamedata.my_pet2.Z;
                        found = true;
                        break;
                    case TargetType.MYPET3:
                        x = Globals.gamedata.my_pet3.X;
                        y = Globals.gamedata.my_pet3.Y;
                        z = Globals.gamedata.my_pet3.Z;
                        found = true;
                        break;
                    case TargetType.NPC:
                        NPCInfo npc = null;

                        Globals.NPCLock.EnterReadLock();
                        try
                        {
                            npc = Util.GetNPC(target);
                        }
                        finally
                        {
                            Globals.NPCLock.ExitReadLock();
                        }

                        if (npc != null)
                        {
                            x = npc.X;
                            y = npc.Y;
                            z = npc.Z;
                            found = true;
                        }
                        break;
                    case TargetType.PLAYER:
                        CharInfo player = null;

                        Globals.PlayerLock.EnterReadLock();
                        try
                        {
                            player = Util.GetChar(target);
                        }
                        finally
                        {
                            Globals.PlayerLock.ExitReadLock();
                        }

                        if (player != null)
                        {
                            x = player.X;
                            y = player.Y;
                            z = player.Z;
                            found = true;
                        }
                        break;
                }

                if (found)
                {
                    //need to attack the target if char is not stunned etc
                    //ServerPackets.Force_Attack(target, Util.Float_Int32(x), Util.Float_Int32(y), Util.Float_Int32(z), false);
                    if ((Globals.gamedata.my_char.HasEffect(AbnormalEffects.FEAR)) ||
                        (Globals.gamedata.my_char.HasEffect(AbnormalEffects.STUN)) ||
                        (Globals.gamedata.my_char.HasEffect(AbnormalEffects.SLEEP)) ||
                        (Globals.gamedata.my_char.HasEffect(AbnormalEffects.HOLD_1)) ||
                        (Globals.gamedata.my_char.HasEffect(AbnormalEffects.PETRIFIED)) ||
                        (Globals.gamedata.my_char.HasEffect(AbnormalEffects.FLOATING_ROOT)) ||
                        (Globals.gamedata.my_char.HasEffect(AbnormalEffects.DANCE_STUNNED)) ||
                        (Globals.gamedata.my_char.HasEffect(AbnormalEffects.FIREROOT_STUN)) ||
                        (Globals.gamedata.my_char.HasEffect(AbnormalEffects.SKULL_FEAR)) ||
                        (Globals.gamedata.my_char.HasExtendedEffect(ExtendedEffects.AIR_STUN)) ||
                        (Globals.gamedata.my_char.HasExtendedEffect(ExtendedEffects.FREEZING)))
                    {
                        Globals.gamedata.my_char.isAttacking = false;
                    }
                    else
                    {
                        if (Globals.gamedata.Control)
                        {
                            ServerPackets.Force_Attack(target, Util.Float_Int32(x), Util.Float_Int32(y), Util.Float_Int32(z), Globals.gamedata.Shift);
                        }
                        else
                        {
                            ServerPackets.ClickOBJ(target, false, Globals.gamedata.Shift);
                        }
                        Globals.gamedata.BOT_STATE = BotState.Attacking;
                    }
                    BotAIThread.was_smart_moving = false;
                }
            }
        }

        public static void SevenSignSky(ByteBuffer buff)
        {
            //F5 - SKY - PERIOD
            //F8 01 01 || 257 = dusk
            //F8 02 01 || 258 = dawn
            //F8 00 01 = none?
            int b = 0;
            if (buff.Length() > 1)
            {
                b = buff.ReadInt16();
            }

            if (b == 257)
            {
                Globals.l2net_home.Add_Text("The Dusk moon is in the sky.", Globals.Gray, TextType.SYSTEM);
            }
            else if (b == 258)
            {
                Globals.l2net_home.Add_Text("The Dawn moon is in the sky.", Globals.Gray, TextType.SYSTEM);
            }
            else
            {
                Globals.l2net_home.Add_Text("The normal moon is in the sky.", Globals.Gray, TextType.SYSTEM);
            }
        }

        public static void Get_Item(ByteBuffer buff)
        {
            uint playID = buff.ReadUInt32();
            uint itemID = buff.ReadUInt32();
            int x = buff.ReadInt32();
            int y = buff.ReadInt32();
            int z = buff.ReadInt32();

            //
            AddInfo.Remove_Item(itemID);
            Globals.l2net_home.timer_items.Start();
        }

        public static void MagicSkillLaunched(ByteBuffer buff)
        {
            if (Globals.gamedata.Chron >= Chronicle.CT3_0)
            {
                buff.ReadUInt32();  //00 00 00 00 
            }
            uint _caster = buff.ReadUInt32();
            uint _skillID = buff.ReadUInt32();
            uint _skillLevel = buff.ReadUInt32();
            uint _targetshit = buff.ReadUInt32();//0 = failed
            ArrayList _targets = new ArrayList();

            for (int i = 0; i < _targetshit; i++)
            {
                //more than 1 target id if targetshit > 1
                uint _targetID = buff.ReadUInt32();

                _targets.Add(_targetID);
            }

            /*if (_targetshit == 0)
            {
                //failed or some shit...
                return;
            }*/

            //not setting caster combat to true?
            StopMoveStartCombat(_caster, false);

            if (_caster == Globals.gamedata.my_char.ID)
            {                
                // Globals.gamedata.my_char.HitTime = 0;
                // Globals.l2net_home.Add_Text("MagicSkillLaunched setting hit time to 0 (" + Globals.gamedata.my_char.HitTime + ")", Globals.Cyan, TextType.BOT);

                if (Globals.gamedata.CurrentScriptState == ScriptState.Running)
                {
                    ScriptEvent sc_ev = new ScriptEvent();
                    sc_ev.Type = EventType.SkillLaunched;
                    sc_ev.Variables.Add(new ScriptVariable((long)_skillID, "SKILL_ID", Var_Types.INT, Var_State.PUBLIC));
                    sc_ev.Variables.Add(new ScriptVariable((long)_skillLevel, "SKILL_LEVEL", Var_Types.INT, Var_State.PUBLIC));
                    sc_ev.Variables.Add(new ScriptVariable((long)_targetshit, "TARGET_HIT", Var_Types.INT, Var_State.PUBLIC));
                    ScriptEngine.SendToEventQueue(sc_ev);
                }

                if (Globals.gamedata.BOT_STATE == BotState.BuffWaiting)
                {
                    if (Globals.gamedata.my_char.BuffTarget == 0)
                    {
                        Globals.gamedata.my_char.Clear_Botting_Buffing(true);
                        return;
                    }

                    if (_targets.Contains(Globals.gamedata.my_char.BuffTarget))
                    {
                        //Add_Text("magicskill launched");

                        //launching a magic attack... this is sufficent for having used our skill
                        Globals.gamedata.my_char.Clear_Botting_Buffing(true);
                        return;
                    }
                }
            }
            else
            {
                if (Globals.gamedata.CurrentScriptState == ScriptState.Running)
                {
                    ScriptEvent sc_ev = new ScriptEvent();
                    sc_ev.Type = EventType.OtherSkillLaunched;
                    sc_ev.Variables.Add(new ScriptVariable((long)_caster, "USER_ID", Var_Types.INT, Var_State.PUBLIC));
                    sc_ev.Variables.Add(new ScriptVariable((long)_skillID, "SKILL_ID", Var_Types.INT, Var_State.PUBLIC));
                    sc_ev.Variables.Add(new ScriptVariable((long)_skillLevel, "SKILL_LEVEL", Var_Types.INT, Var_State.PUBLIC));
                    sc_ev.Variables.Add(new ScriptVariable((long)_targetshit, "TARGET_HIT", Var_Types.INT, Var_State.PUBLIC));
                    ScriptEngine.SendToEventQueue(sc_ev);
                }

                //force target for the npc
                foreach (uint _target in _targets)
                {
                    TargetType type = Util.GetType(_target);

                    Globals.NPCLock.EnterReadLock();
                    try
                    {
                        NPCInfo npc = Util.GetNPC(_caster);

                        if (npc != null)
                        {
                            npc.TargetID = _target;
                            npc.CurrentTargetType = type;
                        }
                    }
                    finally
                    {
                        Globals.NPCLock.ExitReadLock();
                    }
                }
                //end of force target for the npc
            }
        }

        public static void MagicSkillCancel(ByteBuffer buff)
        {
            uint _caster = buff.ReadUInt32();

            if (_caster == Globals.gamedata.my_char.ID)
            {
                Globals.gamedata.my_char.ExpiresTime = 1;

                if (Globals.gamedata.CurrentScriptState == ScriptState.Running)
                {
                    ScriptEvent sc_ev = new ScriptEvent();
                    sc_ev.Type = EventType.SkillCanceled;
                    ScriptEngine.SendToEventQueue(sc_ev);
                }

                if (Globals.gamedata.BOT_STATE == BotState.BuffWaiting)
                {
                    Globals.gamedata.my_char.Clear_Botting_Buffing(false);
                }
            }
            else
            {
                if (Globals.gamedata.CurrentScriptState == ScriptState.Running)
                {
                    ScriptEvent sc_ev = new ScriptEvent();
                    sc_ev.Type = EventType.OtherSkillCanceled;
                    sc_ev.Variables.Add(new ScriptVariable((long)_caster, "USER_ID", Var_Types.INT, Var_State.PUBLIC));
                    ScriptEngine.SendToEventQueue(sc_ev);
                }
            }
        }

        public static void MagicSkillUser(ByteBuffer buff)
        {
           if (Globals.gamedata.Chron >= Chronicle.CT3_0)
            {
                /*god packet
                  * 48 
                     00 00 00 00 // null
                     0B AA C0 4E //char id
                     0B AA C0 4E //targetid
                     00 //null
                     C0 04 00 00 //skill id
                     01 00 00 00 // lvl ?
                     8D 10 00 00 //hit time ?(4237)
                     FF FF FF FF 
                     B8 0B 00 00 // Reuse ?(3000)?
                     CB 41 FE FF //x
                     6E CF 03 00 //y
                     E8 F8 FF FF //z
                     00 00 00 00 // null
                     CB 41 FE FF //tar x
                     6E CF 03 00 //tar y
                     E8 F8 FF FF //tar z
                  * 
                  * 
                  **/
                buff.ReadUInt32(); // 48 00 00 00 00 43 28 01 48 43 28 01 48 00 FF 07 00 00 01 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 3F 41 FE FF 79 DE 03 00 00 FA FF FF 00 00 00 00 3F 41 FE FF 79 DE 03 00 00 FA FF FF 
            }
            uint _caster = buff.ReadUInt32();
            uint _target = buff.ReadUInt32();
            if (Globals.gamedata.Chron >= Chronicle.CT3_0)
            {
                buff.ReadByte();
            }
            uint _skillid = buff.ReadUInt32();
            uint _skilllevel = buff.ReadUInt32();
            uint _hittime = buff.ReadUInt32();
            if (Globals.gamedata.Chron >= Chronicle.CT3_0)
            {
                int _baseSkillID = buff.ReadInt32(); //FF FF FF FF

                if (_baseSkillID > 0)
                    _skillid = (uint)_baseSkillID;

            }
            int _delay = buff.ReadInt32();
            //4 bytescha.getX();
            //4 bytes cha.getY();
            //4 bytes cha.getZ();
            //00 00 00 00

            //not setting caster combat to true?
            StopMoveStartCombat(_caster, false);

            if (_caster == Globals.gamedata.my_char.ID)
            {                
                Globals.gamedata.my_char.Resisted = 0;
                
                // Globals.gamedata.my_char.ExpiresTime = ((1000 + _hittime) * TimeSpan.TicksPerMillisecond) + System.DateTime.Now.Ticks;
                Globals.gamedata.my_char.ExpiresTime = ((_hittime) * TimeSpan.TicksPerMillisecond) + System.DateTime.Now.Ticks;
                
                if (Globals.gamedata.CurrentScriptState == ScriptState.Running)
                {
                    ScriptEvent sc_ev = new ScriptEvent();
                    sc_ev.Type = EventType.SkillUsed;
                    sc_ev.Variables.Add(new ScriptVariable((long)_target, "SKILL_TARGET_ID", Var_Types.INT, Var_State.PUBLIC));
                    sc_ev.Variables.Add(new ScriptVariable((long)_skillid, "SKILL_ID", Var_Types.INT, Var_State.PUBLIC));
                    sc_ev.Variables.Add(new ScriptVariable((long)_skilllevel, "SKILL_LEVEL", Var_Types.INT, Var_State.PUBLIC));
                    sc_ev.Variables.Add(new ScriptVariable((long)_hittime, "HIT_TIME", Var_Types.INT, Var_State.PUBLIC));
                    sc_ev.Variables.Add(new ScriptVariable((long)_delay, "DELAY", Var_Types.INT, Var_State.PUBLIC));
                    ScriptEngine.SendToEventQueue(sc_ev);
                }

                //we cast the skill... let's set our reuse timer...

                Globals.SkillListLock.EnterReadLock();
                try
                {                    
                    if (Globals.gamedata.skills.ContainsKey(_skillid))
                    {
                        ((UserSkill)Globals.gamedata.skills[_skillid]).LastTime = System.DateTime.Now;                     
                        ((UserSkill)Globals.gamedata.skills[_skillid]).NextTime = System.DateTime.Now.AddMilliseconds(_delay);                        
                    }
                }
                finally
                {
                    Globals.SkillListLock.ExitReadLock();
                }
            }
            else
            {
                TargetType type = Util.GetType(_target);

                //force target for the npc
                Globals.NPCLock.EnterReadLock();
                try
                {
                    NPCInfo npc = Util.GetNPC(_caster);

                    if (npc != null)
                    {
                        npc.TargetID = _target;
                        npc.CurrentTargetType = type;
                    }
                }
                finally
                {
                    Globals.NPCLock.ExitReadLock();
                }
                //end of force target for the npc

                if (Globals.gamedata.CurrentScriptState == ScriptState.Running)
                {
                    ScriptEvent sc_ev = new ScriptEvent();
                    sc_ev.Type = EventType.OtherSkillUsed;
                    sc_ev.Variables.Add(new ScriptVariable((long)_caster, "USER_ID", Var_Types.INT, Var_State.PUBLIC));
                    sc_ev.Variables.Add(new ScriptVariable((long)_target, "SKILL_TARGET_ID", Var_Types.INT, Var_State.PUBLIC));
                    sc_ev.Variables.Add(new ScriptVariable((long)_skillid, "SKILL_ID", Var_Types.INT, Var_State.PUBLIC));
                    sc_ev.Variables.Add(new ScriptVariable((long)_skilllevel, "SKILL_LEVEL", Var_Types.INT, Var_State.PUBLIC));
                    sc_ev.Variables.Add(new ScriptVariable((long)_hittime, "HIT_TIME", Var_Types.INT, Var_State.PUBLIC));
                    sc_ev.Variables.Add(new ScriptVariable((long)_delay, "DELAY", Var_Types.INT, Var_State.PUBLIC));
                    ScriptEngine.SendToEventQueue(sc_ev);
                }
            }
        }

        public static void StopMoveStartCombat(uint caster, bool combat)
        {
            TargetType type = Util.GetType(caster);

            switch (type)
            {
                case TargetType.SELF:
                    if (combat)
                    {
                        Globals.gamedata.my_char.isInCombat = 1;
                    }
                    Globals.gamedata.my_char.Moving = false;
                    Globals.gamedata.my_char.MoveTarget = 0;
                    Globals.gamedata.my_char.MoveTargetType = TargetType.NONE;
                    Globals.gamedata.my_char.Dest_X = Globals.gamedata.my_char.X;
                    Globals.gamedata.my_char.Dest_Y = Globals.gamedata.my_char.Y;
                    Globals.gamedata.my_char.Dest_Z = Globals.gamedata.my_char.Z;
                    break;
                case TargetType.MYPET:
                    if (combat)
                    {
                        Globals.gamedata.my_pet.isInCombat = 1;
                    }
                    Globals.gamedata.my_pet.Moving = false;
                    Globals.gamedata.my_pet.MoveTarget = 0;
                    Globals.gamedata.my_pet.MoveTargetType = TargetType.NONE;
                    Globals.gamedata.my_pet.Dest_X = Globals.gamedata.my_char.X;
                    Globals.gamedata.my_pet.Dest_Y = Globals.gamedata.my_char.Y;
                    Globals.gamedata.my_pet.Dest_Z = Globals.gamedata.my_char.Z;
                    break;
                case TargetType.MYPET1:
                    if (combat)
                    {
                        Globals.gamedata.my_pet1.isInCombat = 1;
                    }
                    Globals.gamedata.my_pet1.Moving = false;
                    Globals.gamedata.my_pet1.MoveTarget = 0;
                    Globals.gamedata.my_pet1.MoveTargetType = TargetType.NONE;
                    Globals.gamedata.my_pet1.Dest_X = Globals.gamedata.my_char.X;
                    Globals.gamedata.my_pet1.Dest_Y = Globals.gamedata.my_char.Y;
                    Globals.gamedata.my_pet1.Dest_Z = Globals.gamedata.my_char.Z;
                    break;
                case TargetType.MYPET2:
                    if (combat)
                    {
                        Globals.gamedata.my_pet2.isInCombat = 1;
                    }
                    Globals.gamedata.my_pet2.Moving = false;
                    Globals.gamedata.my_pet2.MoveTarget = 0;
                    Globals.gamedata.my_pet2.MoveTargetType = TargetType.NONE;
                    Globals.gamedata.my_pet2.Dest_X = Globals.gamedata.my_char.X;
                    Globals.gamedata.my_pet2.Dest_Y = Globals.gamedata.my_char.Y;
                    Globals.gamedata.my_pet2.Dest_Z = Globals.gamedata.my_char.Z;
                    break;
                case TargetType.MYPET3:
                    if (combat)
                    {
                        Globals.gamedata.my_pet3.isInCombat = 1;
                    }
                    Globals.gamedata.my_pet3.Moving = false;
                    Globals.gamedata.my_pet3.MoveTarget = 0;
                    Globals.gamedata.my_pet3.MoveTargetType = TargetType.NONE;
                    Globals.gamedata.my_pet3.Dest_X = Globals.gamedata.my_char.X;
                    Globals.gamedata.my_pet3.Dest_Y = Globals.gamedata.my_char.Y;
                    Globals.gamedata.my_pet3.Dest_Z = Globals.gamedata.my_char.Z;
                    break;
                case TargetType.PLAYER:
                    Globals.PlayerLock.EnterReadLock();
                    try
                    {
                        CharInfo player = Util.GetChar(caster);

                        if (player != null)
                        {
                            if (combat)
                            {
                                player.isInCombat = 1;
                            }
                            player.Moving = false;
                            player.MoveTarget = 0;
                            player.MoveTargetType = TargetType.NONE;
                            player.Dest_X = player.X;
                            player.Dest_Y = player.Y;
                            player.Dest_Z = player.Z;
                        }
                    }//unlock
                    finally
                    {
                        Globals.PlayerLock.ExitReadLock();
                    }
                    break;
                case TargetType.NPC:
                    Globals.NPCLock.EnterReadLock();
                    try
                    {
                        NPCInfo npc = Util.GetNPC(caster);

                        if (npc != null)
                        {
                            if (combat)
                            {
                                npc.isInCombat = 1;
                            }
                            npc.Moving = false;
                            npc.MoveTarget = 0;
                            npc.MoveTargetType = TargetType.NONE;
                            npc.Dest_X = npc.X;
                            npc.Dest_Y = npc.Y;
                            npc.Dest_Z = npc.Z;
                        }
                    }//unlock
                    finally
                    {
                        Globals.NPCLock.ExitReadLock();
                    }
                    break;
            }
        }

        public static void MyTargetSelected(ByteBuffer buffe)
        {
            uint nid = buffe.ReadUInt32();
            Globals.gamedata.my_char.TargetColor = buffe.ReadUInt16();
            buffe.ReadUInt32(); //00 00 00 00
            ClientPackets.Set_Target_Data(nid);
            AddInfo.Set_Target_HP();
        }

        public static void TargetSelected(ByteBuffer buff, bool type)
        {
            uint _ID = 0;
            uint _targetID = 0;
            int _x = 0;
            int _y = 0;
            int _z = 0;
            string _targeter_name = "";
            int _targeter_war = 0;

            bool targeter_is_player = true;

            _ID = buff.ReadUInt32();

            TargetType targeter_type = Util.GetType(_ID);

            try
            {
                if (type)
                {
                    //target selected
                    _targetID = buff.ReadUInt32();
                    _x = buff.ReadInt32();
                    _y = buff.ReadInt32();
                    _z = buff.ReadInt32();
                    buff.ReadInt32();//0x00 ?

                    TargetType target_type = Util.GetType(_targetID);

                    //active follow attack targeting
                    if (!Globals.picking_up_items)
                    {
                        if (Globals.gamedata.DoTargeting(_ID, _targetID))
                        {
                            if (_targetID != 0)
                            {
                                //lets target it
                                ServerPackets.Target(_targetID, _x, _y, _z, false);

                                //instant attack
                                if (Globals.gamedata.botoptions.ActiveFollowAttackInstant == 1)
                                {
                                    if (Globals.gamedata.Control)
                                    {
                                        ServerPackets.Force_Attack(_targetID, Util.Float_Int32(_x), Util.Float_Int32(_y), Util.Float_Int32(_z), Globals.gamedata.Shift);
                                    }
                                    else
                                    {
                                        ServerPackets.ClickOBJ(_targetID, false, Globals.gamedata.Shift);
                                    }
                                }
                            }
                        }
                    }

                    //set the targetid of the targeter
                    switch (targeter_type)
                    {
                        case TargetType.SELF:
                            Set_Target_Data(_targetID);
                            AddInfo.Set_Target_HP();
                            _targeter_name = Globals.gamedata.my_char.Name;
                            _targeter_war = 0;
                            if (Globals.gamedata.BOT_STATE == BotState.Attacking || Globals.gamedata.BOT_STATE == BotState.FinishedBuffing)
                                Globals.gamedata.BOT_STATE = BotState.Nothing;
                            break;
                        case TargetType.MYPET:
                            Globals.gamedata.my_pet.TargetID = _targetID;
                            Globals.gamedata.my_pet.CurrentTargetType = target_type;
                            _targeter_name = Globals.gamedata.my_pet.Name;
                            _targeter_war = 0;
                            break;
                        case TargetType.MYPET1:
                            Globals.gamedata.my_pet1.TargetID = _targetID;
                            Globals.gamedata.my_pet1.CurrentTargetType = target_type;
                            _targeter_name = Globals.gamedata.my_pet1.Name;
                            _targeter_war = 0;
                            break;
                        case TargetType.MYPET2:
                            Globals.gamedata.my_pet2.TargetID = _targetID;
                            Globals.gamedata.my_pet2.CurrentTargetType = target_type;
                            _targeter_name = Globals.gamedata.my_pet2.Name;
                            _targeter_war = 0;
                            break;
                        case TargetType.MYPET3:
                            Globals.gamedata.my_pet3.TargetID = _targetID;
                            Globals.gamedata.my_pet3.CurrentTargetType = target_type;
                            _targeter_name = Globals.gamedata.my_pet3.Name;
                            _targeter_war = 0;
                            break;
                        case TargetType.PLAYER:
                            Globals.PlayerLock.EnterReadLock();
                            try
                            {
                                CharInfo player = Util.GetChar(_ID);

                                if (player != null)
                                {
                                    player.TargetID = _targetID;
                                    player.CurrentTargetType = target_type;
                                    _targeter_name = player.Name;
                                    _targeter_war = player.WarState;
                                }
                            }
                            finally
                            {
                                Globals.PlayerLock.ExitReadLock();
                            }
                            break;
                        case TargetType.NPC:
                            Globals.NPCLock.EnterReadLock();
                            try
                            {
                                NPCInfo npc = Util.GetNPC(_ID);

                                if (npc != null)
                                {
                                    npc.TargetID = _targetID;
                                    npc.CurrentTargetType = target_type;
                                    targeter_is_player = false;
                                    _targeter_name = npc.Name;
                                    _targeter_war = 0;
                                }
                            }
                            finally
                            {
                                Globals.NPCLock.ExitReadLock();
                            }

                            try
                            {
                                if (Globals.gamedata.botoptions.OnlyPickMine == 1 && Globals.gamedata.botoptions.Pickup == 1)
                                {
                                    try
                                    {
                                        Globals.MobListLock.EnterWriteLock();
                                        if ((_targetID == Globals.gamedata.my_char.ID) || (_targetID == Globals.gamedata.my_pet.ID) || (_targetID == Globals.gamedata.my_pet1.ID) || (_targetID == Globals.gamedata.my_pet2.ID) || (_targetID == Globals.gamedata.my_pet3.ID))
                                        {
                                            Globals.gamedata.MobList.Add(_ID);
#if DEBUG
                                            Globals.l2net_home.Add_Text("Adding npc to moblist(self or summon): " + _ID, Globals.Green, TextType.SYSTEM);
#endif
                                        }
                                        else
                                        {
                                            foreach (PartyMember pmem in Globals.gamedata.PartyMembers.Values)
                                            {
                                                if ((_targetID == pmem.ID) || (_targetID == pmem.petID) || (_targetID == pmem.pet1ID) || (_targetID == pmem.pet2ID) || (_targetID == pmem.pet3ID))
                                                {
                                                    Globals.gamedata.MobList.Add(_ID);
#if DEBUG
                                                    Globals.l2net_home.Add_Text("Adding npc to moblist(partymember/summon): " + _ID, Globals.Green, TextType.SYSTEM);
#endif
                                                }
                                            }

                                        }

                                        //cleanup old mobs... get the last 75 mobs and save to new array
                                        if (Globals.gamedata.MobList.Count > 350)
                                        {
                                            //System.Collections.ArrayList tmparr = new System.Collections.ArrayList();
                                            //int count = Globals.gamedata.MobList.Count;
                                            //for (int i = 0; i < 75; i++)
                                            //{
                                            //    tmparr.Add(Globals.gamedata.MobList[count - (1 + i)]);
                                            //}
                                            //Globals.gamedata.MobList = tmparr;

                                            //Oddi: Remove first 275 elements in list.

                                            Globals.gamedata.MobList.RemoveRange(0, 225);
                                            #if DEBUG
                                            Globals.l2net_home.Add_Text("resizing array: " + Globals.gamedata.MobList.Count, Globals.Green, TextType.SYSTEM);
                                            #endif

                                        }
                                    }
                                    finally
                                    {
                                        Globals.MobListLock.ExitWriteLock();
                                    }
                                }
                                    
                            }
                            catch(Exception e)
                            {
                                Globals.l2net_home.Add_Error("Error with moblist adding or resizing " + e.Message);
                            }
                            break;
                    }
                }
                else
                {
                    //target unselected
                    _x = buff.ReadInt32();
                    _y = buff.ReadInt32();
                    _z = buff.ReadInt32();
                    buff.ReadInt32();//0x00 ?

                    //active follow attack targeting
                    if (Globals.gamedata.DoTargeting(_ID, _targetID))
                    {
#if DEBUG
                                                    Globals.l2net_home.Add_Text("Canceleling target from Clientpackets: ", Globals.Blue, TextType.SYSTEM);
#endif
                        //cancel target
                        ServerPackets.Send_CancelTarget();
                    }

                    switch (targeter_type)
                    {
                        case TargetType.SELF:
                            Set_Target_Data(0);
                            AddInfo.Set_Target_HP();
                            _targeter_name = Globals.gamedata.my_char.Name;
                            _targeter_war = 0;
                            if (Globals.gamedata.BOT_STATE == BotState.Attacking)
                                Globals.gamedata.BOT_STATE = BotState.Nothing;
                            break;
                        case TargetType.MYPET:
                            Globals.gamedata.my_pet.TargetID = 0;
                            Globals.gamedata.my_pet.CurrentTargetType = TargetType.NONE;
                            _targeter_name = Globals.gamedata.my_pet.Name;
                            _targeter_war = 0;
                            break;
                        case TargetType.MYPET1:
                            Globals.gamedata.my_pet1.TargetID = 0;
                            Globals.gamedata.my_pet1.CurrentTargetType = TargetType.NONE;
                            _targeter_name = Globals.gamedata.my_pet1.Name;
                            _targeter_war = 0;
                            break;
                        case TargetType.MYPET2:
                            Globals.gamedata.my_pet2.TargetID = 0;
                            Globals.gamedata.my_pet2.CurrentTargetType = TargetType.NONE;
                            _targeter_name = Globals.gamedata.my_pet2.Name;
                            _targeter_war = 0;
                            break;
                        case TargetType.MYPET3:
                            Globals.gamedata.my_pet3.TargetID = 0;
                            Globals.gamedata.my_pet3.CurrentTargetType = TargetType.NONE;
                            _targeter_name = Globals.gamedata.my_pet3.Name;
                            _targeter_war = 0;
                            break;
                        case TargetType.PLAYER:
                            Globals.PlayerLock.EnterReadLock();
                            try
                            {
                                CharInfo player = Util.GetChar(_ID);

                                if (player != null)
                                {
                                    player.TargetID = 0;
                                    player.CurrentTargetType = TargetType.NONE;
                                    _targeter_name = player.Name;
                                    _targeter_war = player.WarState;
                                }
                            }
                            finally
                            {
                                Globals.PlayerLock.ExitReadLock();
                            }
                            break;
                        case TargetType.NPC:
                            Globals.NPCLock.EnterReadLock();
                            try
                            {
                                NPCInfo npc = Util.GetNPC(_ID);

                                if (npc != null)
                                {
                                    npc.TargetID = 0;
                                    npc.CurrentTargetType = TargetType.NONE;
                                    targeter_is_player = false;
                                    _targeter_name = npc.Name;
                                    _targeter_war = 0;
                                }
                            }
                            finally
                            {
                                Globals.NPCLock.ExitReadLock();
                            }
                            break;
                    }
                }
            }
            finally
            {
                if (_targetID == Globals.gamedata.my_char.ID)
                {
                    if (Globals.gamedata.CurrentScriptState == ScriptState.Running)
                    {
                        ScriptEvent sc_ev = new ScriptEvent();
                        if (type)
                            sc_ev.Type = EventType.SelfTargeted;
                        else
                            sc_ev.Type = EventType.SelfUnTargeted;
                        sc_ev.Variables.Add(new ScriptVariable((long)_ID, "TARGETER_ID", Var_Types.INT, Var_State.PUBLIC));
                        sc_ev.Variables.Add(new ScriptVariable((long)_x, "TARGETER_X", Var_Types.INT, Var_State.PUBLIC));
                        sc_ev.Variables.Add(new ScriptVariable((long)_y, "TARGETER_Y", Var_Types.INT, Var_State.PUBLIC));
                        sc_ev.Variables.Add(new ScriptVariable((long)_z, "TARGETER_Z", Var_Types.INT, Var_State.PUBLIC));
                        sc_ev.Variables.Add(new ScriptVariable(targeter_is_player ? 1L : 0L, "TARGETER_IS_PLAYER", Var_Types.INT, Var_State.PUBLIC));
                        sc_ev.Variables.Add(new ScriptVariable(_targeter_name, "TARGETER_NAME", Var_Types.STRING, Var_State.PUBLIC));
                        sc_ev.Variables.Add(new ScriptVariable((long)_targeter_war, "TARGETER_WARSTATE", Var_Types.INT, Var_State.PUBLIC));
                        ScriptEngine.SendToEventQueue(sc_ev);
                    }
                }
            }
        }

        public static void Social_Action(ByteBuffer buff)
        {
            uint playerid = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,1);
            uint socialid = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,5);

            string skill = "";

            switch (socialid)
            {
                case 0x02:
                    skill = "Greeting";
                    break;
                case 0x03:
                    skill = "Victory";
                    break;
                case 0x04:
                    skill = "Advance";
                    break;
                case 0x05:
                    skill = "No";
                    break;
                case 0x06:
                    skill = "Yes";
                    break;
                case 0x07:
                    skill = "Bow";
                    break;
                case 0x08:
                    skill = "Unaware";
                    break;
                case 0x09:
                    skill = "Waiting";
                    break;
                case 0x0A:
                    skill = "Laugh";
                    break;
                case 0x0B:
                    skill = "Applaud";
                    break;
                case 0x0C:
                    skill = "Dance";
                    break;
                case 0x0D:
                    skill = "Sorrow";
                    break;
                case 0x0E:
                    skill = "Charm";
                    break;
                case 0x0F:
                    skill = "Shyness";
                    break;
                case 0x10:
                    skill = "Become Hero";
                    break;
                case 0x11:
                    skill = "Flame Weapon";
                    break;
                case 0x084A:
                    skill = "Level Up";
                    break;
                default:
                    skill = "pooping: " + socialid.ToString("X2");
                    break;
            }
            

            TargetType type = Util.GetType(playerid);

            switch (type)
            {
                case TargetType.SELF:
                    if (Globals.SocialSelf)
                    {
                        Globals.l2net_home.Add_Text("[I used social skill: " + skill + "]", Globals.Gray);
                    }
                    break;
                case TargetType.MYPET:
                    if (Globals.SocialSelf)
                    {
                        Globals.l2net_home.Add_Text("[" + Globals.gamedata.my_pet.Name + " used social skill: " + skill + "]", Globals.Gray, TextType.SYSTEM);
                    }
                    break;
                case TargetType.MYPET1:
                    if (Globals.SocialSelf)
                    {
                        Globals.l2net_home.Add_Text("[" + Globals.gamedata.my_pet1.Name + " used social skill: " + skill + "]", Globals.Gray, TextType.SYSTEM);
                    }
                    break;
                case TargetType.MYPET2:
                    if (Globals.SocialSelf)
                    {
                        Globals.l2net_home.Add_Text("[" + Globals.gamedata.my_pet2.Name + " used social skill: " + skill + "]", Globals.Gray, TextType.SYSTEM);
                    }
                    break;
                case TargetType.MYPET3:
                    if (Globals.SocialSelf)
                    {
                        Globals.l2net_home.Add_Text("[" + Globals.gamedata.my_pet3.Name + " used social skill: " + skill + "]", Globals.Gray, TextType.SYSTEM);
                    }
                    break;
                case TargetType.PLAYER:
                    CharInfo player = null;

                    Globals.PlayerLock.EnterReadLock();
                    try
                    {
                        player = Util.GetChar(playerid);
                    }
                    finally
                    {
                        Globals.PlayerLock.ExitReadLock();
                    }

                    if (player != null)
                    {
                        if (Globals.SocialPcs)
                        {
                            Globals.l2net_home.Add_Text("[" + player.Name + " used social skill: " + skill + "]", Globals.Gray, TextType.SYSTEM);
                        }
                    }
                    break;
                case TargetType.NPC:
                    NPCInfo npc = null;

                    Globals.NPCLock.EnterReadLock();
                    try
                    {
                        npc = Util.GetNPC(playerid);

                        if (npc != null)
                        {
                            npc.Active();
                        }
                    }
                    finally
                    {
                        Globals.NPCLock.ExitReadLock();
                    }

                    if (npc != null)
                    {
                        if (Globals.SocialNpcs)
                        {
                            Globals.l2net_home.Add_Text("[" + Util.GetNPCName(npc.NPCID) + " used social skill: " + skill + "]", Globals.Gray, TextType.SYSTEM);
                        }
                    }
                    break;
            }
        }

        public static void Add_PartyInfo(ByteBuffer buff)
        {
            buff.ReadUInt32();
            buff.ReadUInt32();

            PartyMember pmem = new PartyMember();
            pmem.Load(buff);

            Globals.PartyLock.EnterWriteLock();
            try
            {
                if (Globals.gamedata.PartyMembers.ContainsKey(pmem.ID))
                {
                    Globals.gamedata.PartyMembers[pmem.ID] = pmem;
                }
                else
                {
                    Globals.gamedata.PartyMembers.Add(pmem.ID, pmem);
                    Globals.gamedata.PartyCount++;
                }
            }
            finally
            {
                Globals.PartyLock.ExitWriteLock();
            }
        }

        public static void Load_PartyInfo(ByteBuffer buff)
        {
            Globals.gamedata.PartyLeader = buff.ReadUInt32();
            Globals.gamedata.PartyLoot = buff.ReadUInt32();
            uint cnt = buff.ReadUInt32();

            Globals.PartyLock.EnterWriteLock();
            try
            {
                Globals.gamedata.PartyMembers.Clear();
                Globals.gamedata.PartyCount = cnt;

                for (uint i = 0; i < cnt; i++)
                {
                    PartyMember pmem = new PartyMember();
                    pmem.Load(buff);

                    if (Globals.gamedata.PartyMembers.ContainsKey(pmem.ID))
                    {
                        Globals.gamedata.PartyMembers[pmem.ID] = pmem;
                    }
                    else
                    {
                        Globals.gamedata.PartyMembers.Add(pmem.ID, pmem);
                    }
                }
            }
            finally
            {
                Globals.PartyLock.ExitWriteLock();
            }

            if (Globals.gamedata.CurrentScriptState == ScriptState.Running)
            {
                ScriptEvent sc_ev = new ScriptEvent();
                sc_ev.Type = EventType.JoinParty;
                ScriptEngine.SendToEventQueue(sc_ev);
            }
        }

        public static void Update_PartyInfo(ByteBuffer buff)//(smallwindow update??)
        {
            uint id = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,off);off+=4;

            //should be able to get away with a reader lock, since we are only modifying
            Globals.PartyLock.EnterReadLock();
            try
            {
                PartyMember pmem = Util.GetCharParty(id);

                if (pmem != null)
                {
                    pmem.Update(buff);

                    //we want to send a status update here too
                    NetPacket np = new NetPacket();
                    np.Type = (uint)NetPacketType.Update;//regular old status update
                    np.Name = pmem.Name;
                    np.ID = pmem.ID;
                    np.MaxCP = pmem.Max_CP;
                    np.CurCP = pmem.Cur_CP;
                    np.MaxHP = pmem.Max_HP;
                    np.CurHP = pmem.Cur_HP;
                    np.MaxMP = pmem.Max_MP;
                    np.CurMP = pmem.Cur_MP;

                    BroadcastThread.SendStatus(np);
                }
            }
            finally
            {
                Globals.PartyLock.ExitReadLock();
            }
        }

        public static void Set_PartyLocations(ByteBuffer buff)
        {
            uint cnt = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,off);off+=4;
            uint id;

            Globals.PartyLock.EnterReadLock();
            try
            {
                for (uint i = 0; i < cnt; i++)
                {
                    id = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,off);off+=4;

                    PartyMember pmem = Util.GetCharParty(id);

                    if (pmem != null)
                    {
                        pmem.LoadLoc(buff);
                    }
                }
            }
            finally
            {
                Globals.PartyLock.ExitReadLock();
            }
        }

        public static void Delete_Party()
        {
            //def need a writer lock here
            Globals.PartyLock.EnterWriteLock();
            try
            {
                Globals.gamedata.PartyMembers.Clear();
                Globals.gamedata.PartyCount = 0;
            }
            finally
            {
                Globals.PartyLock.ExitWriteLock();
            }

            if (Globals.gamedata.CurrentScriptState == ScriptState.Running)
            {
                ScriptEvent sc_ev = new ScriptEvent();
                sc_ev.Type = EventType.LeaveParty;
                ScriptEngine.SendToEventQueue(sc_ev);
            }
        }

        public static void Delete_PartyInfo(ByteBuffer buff)
        {
            //int off = 1;
            uint id = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,off);off+=4;
            string name = buff.ReadString();//Util.Get_String(buff,ref off);

            Globals.PartyLock.EnterWriteLock();
            try
            {
                if (Globals.gamedata.PartyMembers.ContainsKey(id))
                {
                    Globals.gamedata.PartyMembers.Remove(id);
                    Globals.gamedata.PartyCount--;
                }
            }
            finally
            {
                Globals.PartyLock.ExitWriteLock();
            }
        }

        public static void ClanStatusChanged(ByteBuffer buff)
        {
            //if (Globals.gamedata.Chron <= Chronicle.CT2_6)
            //{
                Globals.l2net_home.ClanStatusChanged(buff);
            //}
        }

        public static void PledgeShowInfoUpdate(ByteBuffer buff)
        {
            //if (Globals.gamedata.Chron <= Chronicle.CT2_6)
            //{
                Globals.l2net_home.ClanInfoUpdate(buff);
            //}
        }

        public static void PledgeShowMemberListAll(ByteBuffer buff)
        {
            //if (Globals.gamedata.Chron <= Chronicle.CT2_6)
            //{
                Globals.l2net_home.Set_ClanInfo(buff);
            //}
        }

        public static void PledgeShowMemberListUpdate(ByteBuffer buff)
        {
            //if (Globals.gamedata.Chron <= Chronicle.CT2_6)
            //{
                Globals.l2net_home.Set_ClanInfoUpdate(buff);
            //}
        }

        public static void PledgeShowMemberListAdd(ByteBuffer buff)
        {
            //if (Globals.gamedata.Chron <= Chronicle.CT2_6)
            //{
                Globals.l2net_home.Set_MemberInfo(buff, true);
            //}
        }

        public static void PledgeShowMemberListDelete(ByteBuffer buff)
        {
            //if (Globals.gamedata.Chron <= Chronicle.CT2_6)
            //{
                Globals.l2net_home.Set_MemberInfoDelete(buff);
            //}
        }

        public static void PledgeShowMemberListDeleteAll(ByteBuffer buff)
        {
            //if (Globals.gamedata.Chron <= Chronicle.CT2_6)
            //{
                Globals.l2net_home.Set_MemberInfoDeleteAll(buff);
            //}
        }

        public static void Revive(uint id)
        {
            TargetType type = Util.GetType(id);

            switch (type)
            {
                case TargetType.SELF:
                    //alive again
                    Globals.l2net_home.Hide_Dead();

                    Globals.gamedata.my_char.isAlikeDead = 0;

                    if (Globals.gamedata.yesno_state == 5)
                    {
                        Globals.l2net_home.Hide_YesNo();
                    }

                    if (Globals.gamedata.CurrentScriptState == ScriptState.Running)
                    {
                        ScriptEvent sc_ev = new ScriptEvent();
                        sc_ev.Type = EventType.SelfRez;
                        ScriptEngine.SendToEventQueue(sc_ev);
                    }
                    break;
                case TargetType.MYPET:
                    Globals.gamedata.my_pet.isAlikeDead = 0;
                    break;
                case TargetType.MYPET1:
                    Globals.gamedata.my_pet1.isAlikeDead = 0;
                    break;
                case TargetType.MYPET2:
                    Globals.gamedata.my_pet2.isAlikeDead = 0;
                    break;
                case TargetType.MYPET3:
                    Globals.gamedata.my_pet3.isAlikeDead = 0;
                    break;
                case TargetType.PLAYER:
                    Globals.PlayerLock.EnterReadLock();
                    try
                    {
                        CharInfo player = Util.GetChar(id);

                        if (player != null)
                        {
                            player.isAlikeDead = 0;
                        }
                    }
                    finally
                    {
                        Globals.PlayerLock.ExitReadLock();
                    }
                    break;
                case TargetType.NPC:
                    Globals.NPCLock.EnterReadLock();
                    try
                    {
                        NPCInfo npc = Util.GetNPC(id);

                        if (npc != null)
                        {
                            npc.isAlikeDead = 0;
                        }
                    }
                    finally
                    {
                        Globals.NPCLock.ExitReadLock();
                    }
                    break;
            }
        }

        public static void ChangeMoveType(ByteBuffer buff)
        {
            uint id = buff.ReadUInt32();
            uint is_running = buff.ReadUInt32();

            TargetType type = Util.GetType(id);

            switch (type)
            {
                case TargetType.SELF:
                    Globals.gamedata.my_char.isRunning = is_running;
                    break;
                case TargetType.MYPET:
                    Globals.gamedata.my_pet.isRunning = is_running;
                    break;
                case TargetType.MYPET1:
                    Globals.gamedata.my_pet1.isRunning = is_running;
                    break;
                case TargetType.MYPET2:
                    Globals.gamedata.my_pet2.isRunning = is_running;
                    break;
                case TargetType.MYPET3:
                    Globals.gamedata.my_pet3.isRunning = is_running;
                    break;
                case TargetType.PLAYER:
                    Globals.PlayerLock.EnterReadLock();
                    try
                    {
                        CharInfo player = Util.GetChar(id);

                        if (player != null)
                        {
                            player.isRunning = is_running;
                        }
                    }
                    finally
                    {
                        Globals.PlayerLock.ExitReadLock();
                    }
                    break;
                case TargetType.NPC:
                    Globals.NPCLock.EnterReadLock();
                    try
                    {
                        NPCInfo npc = Util.GetNPC(id);

                        if (npc != null)
                        {
                            npc.isRunning = is_running;
                        }
                    }
                    finally
                    {
                        Globals.NPCLock.ExitReadLock();
                    }
                    break;
            }
        }

        public static void ChangeWaitType(ByteBuffer buff)
        {
            uint id = buff.ReadUInt32();
            uint is_sitting = buff.ReadUInt32();
            int x = buff.ReadInt32();
            int y = buff.ReadInt32();
            int z = buff.ReadInt32();

            /*
                WT_SITTING = 0; 
                WT_STANDING = 1; 
                WT_START_FAKEDEATH = 2; 
                WT_STOP_FAKEDEATH = 3; 
            */

            TargetType type = Util.GetType(id);

            switch (type)
            {
                case TargetType.SELF:
                    Globals.gamedata.my_char.isSitting = is_sitting;
                    break;
                case TargetType.MYPET:
                    //Globals.gamedata.my_pet.isSitting = is_sitting;
                    break;
                case TargetType.MYPET1:
                    //Globals.gamedata.my_pet.isSitting = is_sitting;
                    break;
                case TargetType.MYPET2:
                    //Globals.gamedata.my_pet.isSitting = is_sitting;
                    break;
                case TargetType.MYPET3:
                    //Globals.gamedata.my_pet.isSitting = is_sitting;
                    break;
                case TargetType.PLAYER:
                    Globals.PlayerLock.EnterReadLock();
                    try
                    {
                        CharInfo player = Util.GetChar(id);

                        if (player != null)
                        {
                            player.isSitting = is_sitting;
                        }
                    }
                    finally
                    {
                        Globals.PlayerLock.ExitReadLock();
                    }
                    break;
                case TargetType.NPC:
                    Globals.NPCLock.EnterReadLock();
                    try
                    {
                        NPCInfo npc = Util.GetNPC(id);

                        if (npc != null)
                        {
                            npc.isSitting = is_sitting;
                        }
                    }
                    finally
                    {
                        Globals.NPCLock.ExitReadLock();
                    }
                    break;
            }
        }

        public static void Validate_Location(ByteBuffer buff, bool inVehicle)
        {
            //int offset = 1;
            uint chid = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,offset);offset+=4;
            if (inVehicle)
            {
                uint vid = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,offset);offset+=4;
            }
            int x = buff.ReadInt32();
            int y = buff.ReadInt32();
            int z = buff.ReadInt32();
            int heading = buff.ReadInt32();

            TargetType type = Util.GetType(chid);

            switch (type)
            {
                case TargetType.SELF:
                    Globals.gamedata.my_char.X = x;
                    Globals.gamedata.my_char.Y = y;
                    Globals.gamedata.my_char.Z = z;
                    Globals.gamedata.my_char.Heading = heading;
                    break;
                case TargetType.MYPET:
                    Globals.gamedata.my_pet.X = x;
                    Globals.gamedata.my_pet.Y = y;
                    Globals.gamedata.my_pet.Z = z;
                    Globals.gamedata.my_pet.Heading = heading;
                    break;
                case TargetType.MYPET1:
                    Globals.gamedata.my_pet1.X = x;
                    Globals.gamedata.my_pet1.Y = y;
                    Globals.gamedata.my_pet1.Z = z;
                    Globals.gamedata.my_pet1.Heading = heading;
                    break;
                case TargetType.MYPET2:
                    Globals.gamedata.my_pet2.X = x;
                    Globals.gamedata.my_pet2.Y = y;
                    Globals.gamedata.my_pet2.Z = z;
                    Globals.gamedata.my_pet2.Heading = heading;
                    break;
                case TargetType.MYPET3:
                    Globals.gamedata.my_pet3.X = x;
                    Globals.gamedata.my_pet3.Y = y;
                    Globals.gamedata.my_pet3.Z = z;
                    Globals.gamedata.my_pet3.Heading = heading;
                    break;
                case TargetType.PLAYER:
                    Globals.PlayerLock.EnterReadLock();
                    try
                    {
                        CharInfo player = Util.GetChar(chid);

                        if (player != null)
                        {
                            player.X = x;
                            player.Y = y;
                            player.Z = z;
                            player.Heading = heading;
                        }
                    }//unlock
                    finally
                    {
                        Globals.PlayerLock.ExitReadLock();
                    }
                    break;
                case TargetType.NPC:
                    Globals.NPCLock.EnterReadLock();
                    try
                    {
                        NPCInfo npc = Util.GetNPC(chid);

                        if (npc != null)
                        {
                            npc.X = x;
                            npc.Y = y;
                            npc.Z = z;
                            npc.Heading = heading;
                        }
                    }//unlock
                    finally
                    {
                        Globals.NPCLock.ExitReadLock();
                    }
                    break;
            }
        }

        public static void Die_Packet(ByteBuffer inp)
        {
            uint dead_id = inp.ReadUInt32();
            uint toVillage = inp.ReadUInt32();
            uint toClanHall = inp.ReadUInt32();
            uint toCastle = inp.ReadUInt32();
            uint toSiegeHQ = inp.ReadUInt32();
            uint sweep = inp.ReadUInt32();
            uint toFortress = inp.ReadUInt32();
            //25, 4bytes access level

            if (Globals.gamedata.my_char.ID == dead_id)
            {
                Globals.l2net_home.DeadButtons(toVillage, toClanHall, toCastle, toSiegeHQ, toFortress);
                Globals.l2net_home.Show_Dead();

                if (Globals.gamedata.CurrentScriptState == ScriptState.Running)
                {
                    ScriptEvent sc_ev = new ScriptEvent();
                    sc_ev.Type = EventType.SelfDie;
                    ScriptEngine.SendToEventQueue(sc_ev);
                }
            }

            if (Globals.gamedata.my_char.TargetID == dead_id)
            {
                /*
                if (sweep == 1 && Globals.gamedata.botoptions.AutoSweep == 1)
                {
                    ServerPackets.Try_Use_Skill(Globals.Skill_SWEEP, false, false);
                }*/

                if (Globals.gamedata.BOT_STATE == BotState.Attacking)
                {
                    Globals.gamedata.BOT_STATE = BotState.Nothing;
                }

                if (Globals.gamedata.CurrentScriptState == ScriptState.Running)
                {
                    ScriptEvent sc_ev = new ScriptEvent();
                    sc_ev.Type = EventType.TargetDie;
                    sc_ev.Variables.Add(new ScriptVariable((long)sweep, "TARGET_BLUE", Var_Types.INT, Var_State.PUBLIC));
                    ScriptEngine.SendToEventQueue(sc_ev);
                }
            }

            TargetType type = Util.GetType(dead_id);

            switch (type)
            {
                case TargetType.SELF:
                    Globals.gamedata.my_char.Dest_X = Util.Float_Int32(Globals.gamedata.my_char.X);
                    Globals.gamedata.my_char.Dest_Y = Util.Float_Int32(Globals.gamedata.my_char.Y);
                    Globals.gamedata.my_char.Dest_Z = Util.Float_Int32(Globals.gamedata.my_char.Z);
                    Globals.gamedata.my_char.Moving = false;
                    Globals.gamedata.my_char.isAlikeDead = 1;
                    break;
                case TargetType.MYPET:
                    Globals.gamedata.my_pet.Dest_X = Util.Float_Int32(Globals.gamedata.my_pet.X);
                    Globals.gamedata.my_pet.Dest_Y = Util.Float_Int32(Globals.gamedata.my_pet.Y);
                    Globals.gamedata.my_pet.Dest_Z = Util.Float_Int32(Globals.gamedata.my_pet.Z);
                    Globals.gamedata.my_pet.Moving = false;
                    Globals.gamedata.my_pet.isAlikeDead = 1;
                    break;
                case TargetType.MYPET1:
                    Globals.gamedata.my_pet1.Dest_X = Util.Float_Int32(Globals.gamedata.my_pet1.X);
                    Globals.gamedata.my_pet1.Dest_Y = Util.Float_Int32(Globals.gamedata.my_pet1.Y);
                    Globals.gamedata.my_pet1.Dest_Z = Util.Float_Int32(Globals.gamedata.my_pet1.Z);
                    Globals.gamedata.my_pet1.Moving = false;
                    Globals.gamedata.my_pet1.isAlikeDead = 1;
                    break;
                case TargetType.MYPET2:
                    Globals.gamedata.my_pet2.Dest_X = Util.Float_Int32(Globals.gamedata.my_pet2.X);
                    Globals.gamedata.my_pet2.Dest_Y = Util.Float_Int32(Globals.gamedata.my_pet2.Y);
                    Globals.gamedata.my_pet2.Dest_Z = Util.Float_Int32(Globals.gamedata.my_pet2.Z);
                    Globals.gamedata.my_pet2.Moving = false;
                    Globals.gamedata.my_pet2.isAlikeDead = 1;
                    break;
                case TargetType.MYPET3:
                    Globals.gamedata.my_pet3.Dest_X = Util.Float_Int32(Globals.gamedata.my_pet3.X);
                    Globals.gamedata.my_pet3.Dest_Y = Util.Float_Int32(Globals.gamedata.my_pet3.Y);
                    Globals.gamedata.my_pet3.Dest_Z = Util.Float_Int32(Globals.gamedata.my_pet3.Z);
                    Globals.gamedata.my_pet3.Moving = false;
                    Globals.gamedata.my_pet3.isAlikeDead = 1;
                    break;
                case TargetType.PLAYER:
                    Globals.PlayerLock.EnterReadLock();
                    try
                    {
                        CharInfo player = Util.GetChar(dead_id);

                        if (player != null)
                        {
                            player.Dest_X = player.X;
                            player.Dest_Y = player.Y;
                            player.Dest_Z = player.Z;
                            player.Moving = false;
                            player.isAlikeDead = 1;
                        }
                    }//unlock
                    finally
                    {
                        Globals.PlayerLock.ExitReadLock();
                    }
                    break;
                case TargetType.NPC:
                    Globals.NPCLock.EnterReadLock();
                    try
                    {
                        NPCInfo npc = Util.GetNPC(dead_id);

                        if (npc != null)
                        {
                            try
                            {
                                Globals.MobListLock.EnterWriteLock();
                                if ((npc.ID == Globals.gamedata.my_char.TargetID) || (npc.ID == Globals.gamedata.my_pet.TargetID) || (npc.ID == Globals.gamedata.my_pet1.TargetID) || (npc.ID == Globals.gamedata.my_pet2.TargetID) || (npc.ID == Globals.gamedata.my_pet3.TargetID))
                                {
                                    Globals.gamedata.MobList.Add(npc.ID);
#if DEBUG
                                    Globals.l2net_home.Add_Text("Adding npc to moblist(npc dead): " + npc.ID, Globals.Green, TextType.SYSTEM);
#endif
                                }
                            }
                            finally
                            {
                                Globals.MobListLock.ExitWriteLock();
                            }

                            npc.Dest_X = npc.X;
                            npc.Dest_Y = npc.Y;
                            npc.Dest_Z = npc.Z;
                            npc.Moving = false;
                            npc.TargetID = 0;
                            npc.CurrentTargetType = TargetType.NONE;
                            npc.isAlikeDead = 1;
                            if (sweep == 1)
                                npc.IsSpoiled = true;
                        }
                        if (Globals.gamedata.botoptions.Cancel_Target == 1)
                        {
                            ServerPackets.Send_CancelTarget();
                        }
                    }//unlock
                    finally
                    {
                        Globals.NPCLock.ExitReadLock();
                    }
                    break;
            }
        }

        public static void Set_Target_Data(uint nid)
        {
            if (Globals.gamedata.my_char.TargetID != nid)
            {
                Globals.gamedata.my_char.TargetSpoiled = false;
                Globals.gamedata.my_char.TargetID = nid;
            }

            string name = "xxx";
            bool found = false;

            TargetType type = Util.GetType(nid);
            Globals.gamedata.my_char.CurrentTargetType = type;

            switch (type)
            {
                case TargetType.NONE:
                    name = "-none-";
                    found = true;
                    break;
                case TargetType.SELF:
                    name = Globals.gamedata.my_char.Name;
                    found = true;
                    break;
                case TargetType.MYPET:
                    name = Globals.gamedata.my_pet.Name;
                    found = true;
                    break;
                case TargetType.MYPET1:
                    name = Globals.gamedata.my_pet1.Name;
                    found = true;
                    break;
                case TargetType.MYPET2:
                    name = Globals.gamedata.my_pet2.Name;
                    found = true;
                    break;
                case TargetType.MYPET3:
                    name = Globals.gamedata.my_pet3.Name;
                    found = true;
                    break;
                case TargetType.PLAYER:
                    CharInfo player = null;

                    Globals.PlayerLock.EnterReadLock();
                    try
                    {
                        player = Util.GetChar(nid);
                    }
                    finally
                    {
                        Globals.PlayerLock.ExitReadLock();
                    }

                    if (player != null)
                    {
                        name = player.Name;
                        found = true;
                    }
                    break;
                case TargetType.NPC:
                    NPCInfo npc = null;

                    Globals.NPCLock.EnterReadLock();
                    try
                    {
                        npc = Util.GetNPC(nid);
                    }
                    finally
                    {
                        Globals.NPCLock.ExitReadLock();
                    }

                    if (npc != null)
                    {
                        name = Util.GetNPCName(npc.NPCID);
                        found = true;
                    }
                    break;
                default:
                    PartyMember party = null;

                    Globals.PartyLock.EnterReadLock();
                    try
                    {
                        party = Util.GetCharParty(nid);
                    }
                    finally
                    {
                        Globals.PartyLock.ExitReadLock();
                    }

                    if (party != null)
                    {
                        name = party.Name;
                        found = true;

                        Globals.gamedata.my_char.CurrentTargetType = TargetType.PLAYER;
                    }
                    break;
            }

            if (!found)
            {
                Globals.gamedata.my_char.CurrentTargetType = TargetType.ERROR;
                name = "-Bad Target-";
            }

            Globals.l2net_home.Set_Target_Name(name);

            if (Globals.l2net_home.menuItem_cmd_overlay.Checked && Globals.overlaywindow != null)
            {
                Globals.overlaywindow.Set_Target_Name(name);
            }
        }

        public static void Teleport(ByteBuffer buff)
        {
            uint id = buff.ReadUInt32();
            int _x = buff.ReadInt32();
            int _y = buff.ReadInt32();
            int _z = buff.ReadInt32();

            TargetType type = Util.GetType(id);

            switch (type)
            {
                case TargetType.SELF:
#if DEBUG
                    Globals.l2net_home.Add_Text("my_char.X: " + Globals.gamedata.my_char.X.ToString(), Globals.Red, TextType.BOT);
                    Globals.l2net_home.Add_Text("my_char.Y: " + Globals.gamedata.my_char.Y.ToString(), Globals.Red, TextType.BOT);
                    Globals.l2net_home.Add_Text("my_char.Z: " + Globals.gamedata.my_char.Z.ToString(), Globals.Red, TextType.BOT);

                    Globals.l2net_home.Add_Text("new.X: " + _x.ToString(), Globals.Red, TextType.BOT);
                    Globals.l2net_home.Add_Text("new.Y: " + _y.ToString(), Globals.Red, TextType.BOT);
                    Globals.l2net_home.Add_Text("new.Z: " + _z.ToString(), Globals.Red, TextType.BOT);
#endif
                    //send to verify my old location
                    Globals.l2net_home.Add_Text("Teleported", Globals.Red, TextType.BOT);

                    if(Globals.ToggleBottingifTeleported)
                    {
                        Globals.l2net_home.Add_Text("Disabling the bot due to being teleported...", Globals.Red, TextType.ALL);
                        Globals.l2net_home.Toggle_Botting(1);
                    }

                    if (Globals.gamedata.OOG)
                    {
                        Util.ClearAllNearby();

                        ServerPackets.Send_Verify();
                        ServerPackets.Send_Appearing();
                    }

                    Globals.gamedata.my_char.X = _x;
                    Globals.gamedata.my_char.Y = _y;
                    Globals.gamedata.my_char.Z = _z;
                    Globals.gamedata.my_char.Dest_X = _x;
                    Globals.gamedata.my_char.Dest_Y = _y;
                    Globals.gamedata.my_char.Dest_Z = _z;

                    Globals.gamedata.teleported = true;
                    break;
                case TargetType.MYPET:
                    Globals.gamedata.my_pet.X = _x;
                    Globals.gamedata.my_pet.Y = _y;
                    Globals.gamedata.my_pet.Z = _z;
                    Globals.gamedata.my_pet.Dest_X = _x;
                    Globals.gamedata.my_pet.Dest_Y = _y;
                    Globals.gamedata.my_pet.Dest_Z = _z;
                    break;
                case TargetType.MYPET1:
                    Globals.gamedata.my_pet1.X = _x;
                    Globals.gamedata.my_pet1.Y = _y;
                    Globals.gamedata.my_pet1.Z = _z;
                    Globals.gamedata.my_pet1.Dest_X = _x;
                    Globals.gamedata.my_pet1.Dest_Y = _y;
                    Globals.gamedata.my_pet1.Dest_Z = _z;
                    break;
                case TargetType.MYPET2:
                    Globals.gamedata.my_pet2.X = _x;
                    Globals.gamedata.my_pet2.Y = _y;
                    Globals.gamedata.my_pet2.Z = _z;
                    Globals.gamedata.my_pet2.Dest_X = _x;
                    Globals.gamedata.my_pet2.Dest_Y = _y;
                    Globals.gamedata.my_pet2.Dest_Z = _z;
                    break;
                case TargetType.MYPET3:
                    Globals.gamedata.my_pet3.X = _x;
                    Globals.gamedata.my_pet3.Y = _y;
                    Globals.gamedata.my_pet3.Z = _z;
                    Globals.gamedata.my_pet3.Dest_X = _x;
                    Globals.gamedata.my_pet3.Dest_Y = _y;
                    Globals.gamedata.my_pet3.Dest_Z = _z;
                    break;
                case TargetType.PLAYER:
                    Globals.PlayerLock.EnterReadLock();
                    try
                    {
                        CharInfo player = Util.GetChar(id);

                        if (player != null)
                        {
                            player.X = _x;
                            player.Y = _y;
                            player.Z = _z;
                            player.Dest_X = _x;
                            player.Dest_Y = _y;
                            player.Dest_Z = _z;
                        }
                    }//unlock
                    finally
                    {
                        Globals.PlayerLock.ExitReadLock();
                    }
                    break;
                case TargetType.NPC:
                    Globals.NPCLock.EnterReadLock();
                    try
                    {
                        NPCInfo npc = Util.GetNPC(id);

                        if (npc != null)
                        {
                            npc.X = _x;
                            npc.Y = _y;
                            npc.Z = _z;
                            npc.Dest_X = _x;
                            npc.Dest_Y = _y;
                            npc.Dest_Z = _z;
                        }
                    }//unlock
                    finally
                    {
                        Globals.NPCLock.ExitReadLock();
                    }
                    break;
            }
        }

        public static void Set_Henna_Info(ByteBuffer buff)
        {
            //int offset = 1;
            buff.ReadByte();//offset+=1;//INT 1bytes
            buff.ReadByte();//offset+=1;//STR 1bytes
            buff.ReadByte();//offset+=1;//CON 1bytes
            buff.ReadByte();//offset+=1;//MEN 1bytes
            buff.ReadByte();//offset+=1;//DEX 1bytes
            buff.ReadByte();//offset+=1;//WIT 1bytes
            Globals.gamedata.my_char.MaxTats = buff.ReadUInt32();

            int tat_cnt = buff.ReadInt32();

            switch (tat_cnt)
            {
                case 0:
                    Globals.gamedata.my_char.Symbol1 = 0;
                    Globals.gamedata.my_char.Symbol2 = 0;
                    Globals.gamedata.my_char.Symbol3 = 0;
                    break;
                case 1:
                    Globals.gamedata.my_char.Symbol1 = buff.ReadUInt32();//dye id 4bytes
                    buff.ReadUInt32();
                    Globals.gamedata.my_char.Symbol2 = 0;
                    Globals.gamedata.my_char.Symbol3 = 0;
                    break;
                case 2:
                    Globals.gamedata.my_char.Symbol1 = buff.ReadUInt32();//dye id 4bytes
                    buff.ReadUInt32();
                    Globals.gamedata.my_char.Symbol2 = buff.ReadUInt32();//dye id 4bytes
                    buff.ReadUInt32();
                    Globals.gamedata.my_char.Symbol3 = 0;
                    break;
                case 3:
                    Globals.gamedata.my_char.Symbol1 = buff.ReadUInt32();//dye id 4bytes
                    buff.ReadUInt32();
                    Globals.gamedata.my_char.Symbol2 = buff.ReadUInt32();//dye id 4bytes
                    buff.ReadUInt32();
                    Globals.gamedata.my_char.Symbol3 = buff.ReadUInt32();//dye id 4bytes
                    buff.ReadUInt32();
                    break;
            }

            Globals.l2net_home.Set_HennaTips();
        }

        public static void Dice(ByteBuffer buff)
        {
            //writeD(_playerId);  //object id of player
            //writeD(_itemId);     // item id of dice (spade)  4625,4626,4627,4628
            //writeD(_number);      //number rolled
            //writeD(_x);       //x
            //writeD(_y);       //y
            //writeD(_z);     //z

            uint pl = buff.ReadUInt32();

            string name = Util.GetCharName(pl) + " rolled a dice of type: " + Util.GetItemName(buff.ReadUInt32()) + " and got a " + buff.ReadUInt32().ToString();

            Globals.l2net_home.Add_Text(name, Globals.Red, TextType.SYSTEM);
        }

        public static void Get_Friend_Message(ByteBuffer buff)
        {
            buff.ReadUInt32();//junk data

            string reciever = buff.ReadString();
            string sender = buff.ReadString();
            string message = buff.ReadString();

            if (Globals.gamedata.alertoptions.beepon_friendchat)
            {
                VoicePlayer.PlayAlarm();
            }

            Globals.l2net_home.Add_Text("Friend Message From " + sender + " : " + message);
        }

        public static void EtcStatusUpdate(ByteBuffer buff)
        {
            Globals.gamedata.my_char.Charges = buff.ReadUInt32();
            buff.ReadUInt32();//weight penalty
            buff.ReadUInt32();//chat blocked
            buff.ReadUInt32();//danger area
            buff.ReadUInt32();//grade penalty - weapon
            if (Globals.gamedata.Chron >= Chronicle.CT2_4)
            {
                buff.ReadUInt32();//grade penalty - armor
            }
            buff.ReadUInt32();//charm of courage
            Globals.gamedata.my_char.DeathPenalty = buff.ReadUInt32();//death penalty
            Globals.gamedata.my_char.Souls = buff.ReadUInt32();
        }

        public static void Load_Shortcuts(ByteBuffer buff)
        {
            int length = buff.ReadInt32();

            //Globals.l2net_home.Add_Text("Lenght:" + length.ToString("X2"), Globals.Green, TextType.BOT);

            for (uint i = 0; i < length; i++)
            {
                Load_Shortcut(buff);
            }
        }

        public static void Load_Shortcut(ByteBuffer buff)
        {
            ShortCut_Types stype;

            int type = buff.ReadInt32();
            int loc = buff.ReadInt32();

            uint id = buff.ReadUInt32();
            uint level = buff.ReadUInt32();
            byte c = 1;

            switch (type)
            {
                case 1://item
                    //CT3.0:
                    //01 00 00 00 
                    //0B 00 00 00 
                    //0B F8 C4 41 
                    //01 00 00 00 
                    //FF FF FF FF 
                    //00 00 00 00  
                    //00 00 00 00 
                    //00 00 00 00 
                    //00 00 00 00
                    stype = ShortCut_Types.ITEM;
                    buff.ReadUInt32();//-1
                    buff.ReadUInt32();//0
                    buff.ReadUInt32();//0
                    buff.ReadUInt32();//2x 00 00
                    if (Globals.gamedata.Chron >= Chronicle.CT3_0)
                    {
                        buff.ReadUInt32(); //00 00 00 00
                    }

                    break;
                case 2://skill
                    stype = ShortCut_Types.SKILL;
                    if (Globals.gamedata.Chron >= Chronicle.CT3_0)
                    {
                        buff.ReadUInt32(); //FF FF FF FF ??
                    }
                    c = buff.ReadByte();//?? 1 bytes or two ??
                    buff.ReadInt32();//1
                    break;
                case 3://action
                    stype = ShortCut_Types.ACTION;
                    break;
                case 4://macro
                    stype = ShortCut_Types.MACRO;
                    break;
                case 5://recipe
                    stype = ShortCut_Types.RECIPE;
                    break;
                default://dunno
                    stype = ShortCut_Types.NULL;
                    break;
            }

            if (loc >= 0 && loc < Globals.Skills_Pages * Globals.Skills_PerPage)
            {
                ((ShortCut)Globals.gamedata.ShortCuts[loc]).Type = stype;
                ((ShortCut)Globals.gamedata.ShortCuts[loc]).ID = id;
                ((ShortCut)Globals.gamedata.ShortCuts[loc]).Level = level;
                ((ShortCut)Globals.gamedata.ShortCuts[loc]).c = c;
            }
            else
            {
                Globals.l2net_home.Add_Text("Failed to add shortcut at location " + loc.ToString());

                //Globals.l2net_home.Add_Text("Type:" + type.ToString("X2"), Globals.Green, TextType.BOT);
                //Globals.l2net_home.Add_Text("Loc:" + loc.ToString("X2"), Globals.Green, TextType.BOT);
                //Globals.l2net_home.Add_Text("ID:" + id.ToString("X2"), Globals.Green, TextType.BOT);
                //Globals.l2net_home.Add_Text("Level:" + level.ToString("X2"), Globals.Green, TextType.BOT);

            }
        }

        public static void ExShowScreenMessage(ByteBuffer buff)
        {
            //buff.ReadByte(); //0x00
            uint type = buff.ReadUInt32();//type
            Globals.l2net_home.Add_Text("Type: " + type.ToString(), Globals.Yellow, TextType.BOT);
            uint messageid = buff.ReadUInt32();//system messge id
            buff.ReadInt32();//position
            buff.ReadInt32();//0
            buff.ReadInt32();//size
            buff.ReadInt32();//0

            buff.ReadInt32();//0
            buff.ReadInt32();//effect

            buff.ReadInt32();//time

            buff.ReadInt32();//0
            buff.ReadInt32();//0

            string message;

            if (type == 1)
            {
                message = buff.ReadString();
            }
            else
            {
                message = SystemMessageInternal(messageid, buff);
            }

            if (!string.IsNullOrEmpty(message))
            {
                Globals.l2net_home.Add_Text("Screen Message : " + message, Globals.LightYellow, TextType.SYSTEM);
            }
        }

        public static void ExSetCompassZoneCode(ByteBuffer buff)
        {
            uint type = buff.ReadUInt32();//
            buff.ReadUInt16(); //00 00
            buff.ReadByte(); //00
            Globals.gamedata.cur_zone = type;
        }

        public static void AllyCrest(ByteBuffer buff)
        {
            uint crestid = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,1);
            uint crestsize = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,5);

            //data of length size
            //how is the data formatted?
        }

        public static void PledgeCrest(ByteBuffer buffe)
        {
            //Add_Text("got clan crest",Red);,
            uint crestid = buffe.ReadUInt32();//System.BitConverter.ToInt32(buff,offset);offset+=4;
            int crestsize = buffe.ReadInt32();//System.BitConverter.ToInt32(buff,offset);offset+=4;

            //Add_Text(crestid.ToString(),Red);
            //Add_Text(crestsize.ToString(),Red);
            try
            {
                //if (crestid > 1000000)
                //{
                System.Drawing.Bitmap img;

                //Globals.l2net_home.Add_Text("Saving crest: " + crestid.ToString());

                if (!System.IO.File.Exists(Globals.PATH + "\\crests\\" + crestid.ToString() + ".bmp"))
                {
                    //Globals.l2net_home.Add_Text(":::" + buffe.Length().ToString());
                    byte[] data = new byte[crestsize];
                    for (int i = 0; i < crestsize; i++)
                    {
                        data[i] = buffe.ReadByte();
                    }

                    if (crestsize < 257)
                    {
                        img = Util.GetBMP(data);
                    }
                    else
                    {
                        img = Util.Dds2BMP(data);
                    }

                    img.Save(Globals.PATH + "\\crests\\" + crestid.ToString() + ".bmp");
                }
                else
                {
                    //got this crest file already
                    img = new System.Drawing.Bitmap(Globals.PATH + "\\crests\\" + crestid.ToString() + ".bmp");
                }
                Globals.l2net_home.imageList_crests.Images.Add(img);
                Globals.crestids.Add(crestid);

                //find players with this crest and set the id...

                Globals.PlayerLock.EnterReadLock();
                try
                {
                    foreach (CharInfo player in Globals.gamedata.nearby_chars.Values)
                    {
                        if (player.ClanCrestID == crestid)
                        {
                            player.ClanCrestIndex = Globals.l2net_home.imageList_crests.Images.Count - 1;

                            //populate info in the list
                            Globals.l2net_home.timer_players.Start();
                        }
                    }
                }
                finally
                {
                    Globals.PlayerLock.ExitReadLock();
                }
            }
            catch
            {

            }
            //}
        }

        public static void PledgeInfo(ByteBuffer buffe)
        {
            Clan_Info c_i = new Clan_Info();
            c_i.Load(buffe);

            //add the clan info to the list...
            Globals.ClanListLock.EnterWriteLock();
            try
            {
                if (Globals.clanlist.ContainsKey(c_i.ID))
                {
                    Globals.clanlist[c_i.ID] = c_i;
                }
                else
                {
                    Globals.clanlist.Add(c_i.ID, c_i);
                }
            }
            finally
            {
                Globals.ClanListLock.ExitWriteLock();
            }

            //find players in this clan and set the info...
            Globals.PlayerLock.EnterReadLock();
            try
            {
                foreach (CharInfo player in Globals.gamedata.nearby_chars.Values)
                {
                    if (player.ClanID == c_i.ID)
                    {
                        player.ClanName = c_i.ClanName;
                        player.AllyName = c_i.AllyName;
                    }
                }
            }
            finally
            {
                Globals.PlayerLock.ExitReadLock();
            }

            //populate info in the list
            Globals.l2net_home.timer_players.Start();
        }

        public static void PetItemList(ByteBuffer buffe)
        {

            uint count = buffe.ReadUInt16();

            Globals.PetInventoryLock.EnterReadLock();
            try
            {
                foreach (PetInventoryInfo inv_inf in Globals.gamedata.my_pet.inventory.Values)
                {
                    inv_inf.InNewList = false;
                }

            }

            finally
            {
                Globals.PetInventoryLock.ExitReadLock();
            }

            Globals.PetInventoryLock.EnterWriteLock();

            try
            {
                Globals.gamedata.my_pet.inventory.Clear();

                for (uint i = 0; i < count; i++)
                {
                    PetInventoryInfo inv_info = new PetInventoryInfo();
                    inv_info.Load(buffe);
                    AddInfo.Add_PetInventory(inv_info);
                }
            }

            finally
            {
                Globals.PetInventoryLock.ExitWriteLock();
            }
        }

        public static void ExAcquireSkillInfo(ByteBuffer buffe)
        {
            if (Globals.gamedata.Chron >= Chronicle.CT3_0 && Globals.AutolearnSkills)
            {
                uint skillID = buffe.ReadUInt32();
                uint lvl = buffe.ReadUInt32();
                buffe.ReadUInt32(); //32 00 00 00 
                buffe.ReadUInt32(); //05 00 00 00
                buffe.ReadUInt32(); //00 00 00 00 
                buffe.ReadUInt16(); //00 00

                ByteBuffer breply = new ByteBuffer(13);
                breply.WriteByte((byte)PClient.RequestAquireSkill);
                breply.WriteUInt32(skillID);
                breply.WriteUInt32(lvl);
                breply.WriteUInt32(0x00);
                Globals.gamedata.SendToGameServer(breply);
            }
        }

#region Mailing

        public static void Load_ReceivedMails(ByteBuffer buff)
        {
          if (Globals.mailboxwindow == null || Globals.mailboxwindow.IsDisposed == true)
          {
            return;
          }

          Globals.mailboxwindow.timeSeconds = buff.ReadUInt32();
          Globals.mailboxwindow.inboxSize = buff.ReadUInt32();
          MailboxWindow.ReceivedMail tempReceived;
          Globals.mailboxwindow.listViewReceived.Items.Clear();
          Globals.mailboxwindow.receivedMails.Clear();
          System.Windows.Forms.ListViewItem tempListItem;
          uint tempInt;
          string tempStr;
          for (uint i = 0; i < Globals.mailboxwindow.inboxSize; i++)
          {

            tempReceived = new MailboxWindow.ReceivedMail();
            tempReceived.msgId = buff.ReadUInt32();
            tempReceived.subject = buff.ReadString();
            tempReceived.senderName = buff.ReadString();
            tempReceived.isLocked = buff.ReadUInt32();
            tempReceived.expireSecs = buff.ReadUInt32();
            tempReceived.isUnread = buff.ReadUInt32();
            buff.ReadUInt32();
            tempReceived.hasAttachs = buff.ReadUInt32();
            tempReceived.isReturned = buff.ReadUInt32();
            tempReceived.isSystem = buff.ReadUInt32();
            buff.ReadUInt32();

            tempListItem = new System.Windows.Forms.ListViewItem(tempReceived.senderName);
            tempListItem.SubItems.Add(tempReceived.subject);

            tempInt = tempReceived.expireSecs - Globals.mailboxwindow.timeSeconds;
            tempStr = (tempInt % 60).ToString() + "s";
            tempInt = (tempInt - (tempInt % 60)) / 60;
            tempStr = (tempInt % 60 ).ToString() + "m " + tempStr;
            tempInt = (tempInt - (tempInt % 60)) / 60;
            tempStr = (tempInt % 24).ToString() + "h " + tempStr;
            tempInt = (tempInt - (tempInt % 24)) / 24;
            tempStr = tempInt.ToString() + "d " + tempStr;
            tempListItem.SubItems.Add(tempStr);

            tempStr = "";
            if (tempReceived.isUnread == 1) tempStr += ", NEW";
            if (tempReceived.isLocked == 1) tempStr += ", Locked";
            if (tempReceived.hasAttachs == 1) tempStr += ", Attachs";
            if (tempReceived.isReturned == 1) tempStr += ", Returned";
            if (tempReceived.isSystem == 1) tempStr += ", System";
            if (tempStr != "") tempStr = tempStr.Substring(2);
            tempListItem.SubItems.Add(tempStr);

            Globals.mailboxwindow.listViewReceived.Items.Add(tempListItem);
            Globals.mailboxwindow.receivedMails.Add(tempReceived);

          }

        }

#endregion

        public static void Extargetbuffs(ByteBuffer buffe)
        {
            /*
             * FE E6 00 
                59 B0 21 48 // char id
                02 00 // buff count
	                01 2D 00 00 // buff id
	                01 00  //lvl
	                A3 01 00 00 //  ???
	                5D 03 00 00 //  real dur in sec 
	                59 B0 21 48 // id from char what put that buff

	                05 2D 00 00 // buff id
	                01 00 
	                A5 01 00 00 
	                62 03 00 00 
	                59 B0 21 48 
             * 
             */

                uint id = buffe.ReadUInt32(); // char id
                int buff_count = buffe.ReadInt16(); // buff count
                if (id > 0)
                {
                    TargetType type = Util.GetType(id);

                    switch (type)
                    {
                        case TargetType.PLAYER:
                            Globals.PlayerLock.EnterWriteLock();
                            try
                            {
                                Globals.PartyLock.EnterReadLock();
                                if (!Globals.gamedata.PartyMembers.ContainsKey(id))
                                {
                                    CharInfo player = Util.GetChar(id);
                                    if (player != null)
                                    {
                                        player.my_buffs.Clear();
                                        if (buff_count > 0)
                                        {

                                            for (int i = 0; i < buff_count; i++)
                                            {
                                                CharBuff _mybuff = new CharBuff();
                                                _mybuff.ID = buffe.ReadUInt32(); // skill id
                                                _mybuff.SkillLevel = (uint)buffe.ReadInt16(); // skill lvl
                                                buffe.ReadUInt32(); // xxx
                                                _mybuff.EFFECT_TIME = buffe.ReadInt32();
                                                _mybuff.ExpiresTime = System.DateTime.Now.AddSeconds(_mybuff.EFFECT_TIME).Ticks;
                                                buffe.ReadUInt32();
                                                if (player.my_buffs.ContainsKey(_mybuff.ID))
                                                {
                                                    player.my_buffs[_mybuff.ID] = _mybuff;
                                                }
                                                else
                                                {
                                                    player.my_buffs.Add(_mybuff.ID, _mybuff);
                                                }
                                                //clearing player buffs
                                            }
                                        }


                                    }
                                }
                            }
                            finally
                            {
                                Globals.PlayerLock.ExitWriteLock();
                                Globals.PartyLock.ExitReadLock();
                            }
                            break;
                        case TargetType.NPC:
                            Globals.NPCLock.EnterWriteLock();
                            try
                            {
                                NPCInfo npc = Util.GetNPC(id);
                                if (npc != null)
                                {

                                    if (npc.ID != Globals.gamedata.my_pet.ID &&
                                        npc.ID != Globals.gamedata.my_pet1.ID &&
                                        npc.ID != Globals.gamedata.my_pet2.ID &&
                                        npc.ID != Globals.gamedata.my_pet3.ID)
                                    {
                                        npc.my_buffs.Clear();
                                        if (buff_count > 0)
                                        {
                                            for (int i = 0; i < buff_count; i++)
                                            {
                                                CharBuff _mybuff = new CharBuff();
                                                _mybuff.ID = buffe.ReadUInt32(); // skill id
                                                _mybuff.SkillLevel = (uint)buffe.ReadInt16(); // skill lvl
                                                buffe.ReadUInt32(); // xxx
                                                _mybuff.EFFECT_TIME = buffe.ReadInt32();
                                                _mybuff.ExpiresTime = System.DateTime.Now.AddSeconds(_mybuff.EFFECT_TIME).Ticks;
                                                buffe.ReadUInt32();
                                                if (npc.my_buffs.ContainsKey(_mybuff.ID))
                                                {
                                                    npc.my_buffs[_mybuff.ID] = _mybuff;
                                                }
                                                else
                                                {
                                                    npc.my_buffs.Add(_mybuff.ID, _mybuff);
                                                }
                                                //clearing player buffs
                                            }
                                        }
                                    }


                                    //
                                }
                            }//unlock
                            finally
                            {
                                Globals.NPCLock.ExitWriteLock();
                            }
                            break;

                    } // switch end
                } // if id > 0
                
        }
        public static void ExPartyPetWindowAdd(ByteBuffer buffe)
        {

            uint pet_id = buffe.ReadUInt32(); // char id
            buffe.ReadUInt32(); // npcid
            buffe.ReadUInt32(); // sumon type
            uint owner = buffe.ReadUInt32();
            Globals.PartyLock.EnterWriteLock();
             try
            {
                
                if (Globals.gamedata.PartyMembers.ContainsKey(owner))
                {
                    if (((PartyMember)Globals.gamedata.PartyMembers[owner]).petID == 0)
                    {
                        ((PartyMember)Globals.gamedata.PartyMembers[owner]).petID = pet_id;
                    }
                    else
                    {
                        if (((PartyMember)Globals.gamedata.PartyMembers[owner]).pet1ID == 0)
                        {
                            ((PartyMember)Globals.gamedata.PartyMembers[owner]).pet1ID = pet_id;
                        }
                        else
                        {
                            if (((PartyMember)Globals.gamedata.PartyMembers[owner]).pet2ID == 0)
                            {
                                ((PartyMember)Globals.gamedata.PartyMembers[owner]).pet2ID = pet_id;
                            }
                            else
                            {
                                if (((PartyMember)Globals.gamedata.PartyMembers[owner]).pet3ID == 0)
                                {
                                    ((PartyMember)Globals.gamedata.PartyMembers[owner]).pet3ID = pet_id;
                                }
                            }
                        }
                    }
                }
                else
                {
                    // pet without owner ? it looks like someone got eaten by kitten :d
                }
            }
             finally
             {
                 Globals.PartyLock.ExitWriteLock();
             }
        }

        public static void ExPartyPetWindowDelete(ByteBuffer buffe)
        {

            uint pet_id = buffe.ReadUInt32(); // char id
            uint owner = buffe.ReadUInt32();
            Globals.PartyLock.EnterWriteLock();
            try
            {

                if (Globals.gamedata.PartyMembers.ContainsKey(owner))
                {
                    if (((PartyMember)Globals.gamedata.PartyMembers[owner]).petID == pet_id)
                    {
                        ((PartyMember)Globals.gamedata.PartyMembers[owner]).petID = 0;
                    }
                    else
                    {
                        if (((PartyMember)Globals.gamedata.PartyMembers[owner]).pet1ID == pet_id)
                        {
                            ((PartyMember)Globals.gamedata.PartyMembers[owner]).pet1ID = 0;
                        }
                        else
                        {
                            if (((PartyMember)Globals.gamedata.PartyMembers[owner]).pet2ID == pet_id)
                            {
                                ((PartyMember)Globals.gamedata.PartyMembers[owner]).pet2ID = 0;
                            }
                            else
                            {
                                if (((PartyMember)Globals.gamedata.PartyMembers[owner]).pet3ID == pet_id)
                                {
                                    ((PartyMember)Globals.gamedata.PartyMembers[owner]).pet3ID = 0;
                                }
                            }
                        }
                    }
                }
                else
                {
                    // pet without owner ? it looks like someone got eaten by kitten :d
                }
            }
            finally
            {
                Globals.PartyLock.ExitWriteLock();
            }

        }

    }//end of class
}//end of namespace
