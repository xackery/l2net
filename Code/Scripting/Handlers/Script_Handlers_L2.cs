using System;
using System.Collections.Generic;
using System.Text;

namespace L2_login
{
    public partial class ScriptEngine
    {
        private void Script_GENERATE_POLY(string line)
        {
            double radius = Util.GetDouble(Get_String(ref line));
            int sides = Util.GetInt32(Get_String(ref line));
            double offset = Util.GetDouble(Get_String(ref line));

            if (sides < 3)
            {
                return;
            }

            Globals.gamedata.Paths.PointList.Clear();

            offset = offset - 180 / sides + 90;

            for (int i = 0; i < sides; i++)
            {
                double degrees = (360 / sides * i + offset) * Math.PI / 180;
                int x = (int)(radius * Math.Cos(degrees)) + Util.Float_Int32(Globals.gamedata.my_char.X);
                int y = (int)(radius * Math.Sin(degrees)) + Util.Float_Int32(Globals.gamedata.my_char.Y);

                Point p = new Point(x, y);
                Globals.gamedata.Paths.PointList.Add(p);
            }
        }

        private void Script_GET_ZONE(string line)
        {
            string svar = Get_String(ref line);

            ScriptVariable var = Get_Var(svar);

            var.Value = Globals.gamedata.cur_zone;
        }

        private void Script_PLAYWAV(string line)
        {
            VoicePlayer.PlayWavFile(Globals.PATH + "\\Sounds\\" + Get_String(ref line));
        }

        private void Script_PLAYSOUND(string line)
        {
            VoicePlayer.PlaySound(Util.GetInt32(Get_String(ref line)));
        }

        private void Script_UDP_SEND(string inp)
        {
            string st = Get_String(ref inp);
            string sp1 = Get_String(ref inp);
            string sp2 = Get_String(ref inp);
            string sp3 = Get_String(ref inp);
            string sp4 = Get_String(ref inp);

            ScriptVariable p1 = Get_Var(sp1);
            ScriptVariable p2 = Get_Var(sp2);
            ScriptVariable p3 = Get_Var(sp3);
            ScriptVariable p4 = Get_Var(sp4);

            NetPacket np = new NetPacket();

            np.Type = (uint)NetPacketType.Script;
            np.Sender = Globals.gamedata.my_char.Name;
            np.SenderID = Globals.gamedata.my_char.ID;
            np.Name = st;
            np.Param1 = System.Convert.ToInt32(p1.Value);
            np.Param2 = System.Convert.ToInt32(p2.Value);
            np.Param3 = System.Convert.ToInt32(p3.Value);
            np.Param4 = System.Convert.ToInt32(p4.Value);

            NetCode.NetSend(np.GetBytes());
        }

        private void Script_UDP_SENDBB(string inp)
        {
            string sp1 = Get_String(ref inp);

            ScriptVariable p1 = Get_Var(sp1);

            NetPacket np = new NetPacket();

            np.Type = (uint)NetPacketType.ScriptBB;
            np.Sender = Globals.gamedata.my_char.Name;
            np.SenderID = Globals.gamedata.my_char.ID;
            np.BBuff = ((ByteBuffer)p1.Value);

            NetCode.NetSend(np.GetBytes());
        }

        private void Script_UDP_IP_SEND(string inp)
        {
            string ip = Get_String(ref inp);
            string st = Get_String(ref inp);
            string sp1 = Get_String(ref inp);
            string sp2 = Get_String(ref inp);
            string sp3 = Get_String(ref inp);
            string sp4 = Get_String(ref inp);

            ScriptVariable p1 = Get_Var(sp1);
            ScriptVariable p2 = Get_Var(sp2);
            ScriptVariable p3 = Get_Var(sp3);
            ScriptVariable p4 = Get_Var(sp4);

            NetPacket np = new NetPacket();

            np.Type = (uint)NetPacketType.Script;
            np.Sender = Globals.gamedata.my_char.Name;
            np.SenderID = Globals.gamedata.my_char.ID;
            np.Name = st;
            np.Param1 = System.Convert.ToInt32(p1.Value);
            np.Param2 = System.Convert.ToInt32(p2.Value);
            np.Param3 = System.Convert.ToInt32(p3.Value);
            np.Param4 = System.Convert.ToInt32(p4.Value);

            NetCode.NetSendIP(np.GetBytes(), ip);
        }

        private void Script_UDP_IP_SENDBB(string inp)
        {
            string ip = Get_String(ref inp);
            string sp1 = Get_String(ref inp);

            ScriptVariable p1 = Get_Var(sp1);

            NetPacket np = new NetPacket();

            np.Type = (uint)NetPacketType.ScriptBB;
            np.Sender = Globals.gamedata.my_char.Name;
            np.SenderID = Globals.gamedata.my_char.ID;
            np.BBuff = ((ByteBuffer)p1.Value);

            NetCode.NetSendIP(np.GetBytes(), ip);
        }

        private void Script_BLOCK_CLIENT(string line)
        {
            int packet_id = Util.GetInt32(Get_String(ref line));

            if (ScriptEngine.Blocked_ClientPackets.ContainsKey(packet_id))
            {
            }
            else
            {
                ScriptEngine.Blocked_ClientPackets.Add(packet_id, 0);
            }
        }

        private void Script_BLOCKEX_CLIENT(string line)
        {
            int packetex_id = Util.GetInt32(Get_String(ref line));

            if (ScriptEngine.Blocked_ClientPacketsEX.ContainsKey(packetex_id))
            {
            }
            else
            {
                ScriptEngine.Blocked_ClientPacketsEX.Add(packetex_id, 0);
            }
        }

        private void Script_UNBLOCK_CLIENT(string line)
        {
            int packet_id = Util.GetInt32(Get_String(ref line));

            if (ScriptEngine.Blocked_ClientPackets.ContainsKey(packet_id))
            {
                ScriptEngine.Blocked_ClientPackets.Remove(packet_id);
            }
        }

        private void Script_UNBLOCKEX_CLIENT(string line)
        {
            int packetex_id = Util.GetInt32(Get_String(ref line));

            if (ScriptEngine.Blocked_ClientPacketsEX.ContainsKey(packetex_id))
            {
                ScriptEngine.Blocked_ClientPacketsEX.Remove(packetex_id);
            }
        }

        private void Script_CLEAR_BLOCK_CLIENT()
        {
            ScriptEngine.Blocked_ClientPackets.Clear();
        }

        private void Script_CLEAR_BLOCKEX_CLIENT()
        {
            ScriptEngine.Blocked_ClientPacketsEX.Clear();
        }

        private void Script_BLOCK_SELF(string line)
        {
            int packet_id = Util.GetInt32(Get_String(ref line));

            if (ScriptEngine.Blocked_SelfPackets.ContainsKey(packet_id))
            {
            }
            else
            {
                ScriptEngine.Blocked_SelfPackets.Add(packet_id, 0);
            }
        }

        private void Script_BLOCK_SELF_ALL()
        {
            int opcode = 0;
            PClient[] values = (PClient[])Enum.GetValues(typeof(PClient));
            for (int i = 0; i<values.Length; i++)
            {
                opcode = (int)values[i];
                if (ScriptEngine.Blocked_SelfPackets.ContainsKey(opcode))
                {
                }
                else
                {
                    //Globals.l2net_home.Add_Text(opcode + " added to blocklist", Globals.Green);
                    ScriptEngine.Blocked_SelfPackets.Add(opcode, 0);
                }
            }

        }

        private void Script_BLOCKEX_SELF(string line)
        {
            int packetex_id = Util.GetInt32(Get_String(ref line));

            if (ScriptEngine.Blocked_SelfPacketsEX.ContainsKey(packetex_id))
            {
            }
            else
            {
                ScriptEngine.Blocked_SelfPacketsEX.Add(packetex_id, 0);
            }
        }

        private void Script_BLOCKEX_SELF_ALL()
        {
            int opcode = 0;
            PClient[] values = (PClient[])Enum.GetValues(typeof(PClient));
            for (int i = 0; i < values.Length; i++)
            {
                opcode = (int)values[i];
                if (ScriptEngine.Blocked_SelfPacketsEX.ContainsKey(opcode))
                {
                }
                else
                {
                    //Globals.l2net_home.Add_Text(opcode + " added to blocklist", Globals.Green);
                    ScriptEngine.Blocked_SelfPacketsEX.Add(opcode, 0);
                }
            }

        }

        private void Script_UNBLOCK_SELF(string line)
        {
            int packet_id = Util.GetInt32(Get_String(ref line));

            if (ScriptEngine.Blocked_SelfPackets.ContainsKey(packet_id))
            {
                ScriptEngine.Blocked_SelfPackets.Remove(packet_id);
            }
        }

        private void Script_UNBLOCKEX_SELF(string line)
        {
            int packetex_id = Util.GetInt32(Get_String(ref line));

            if (ScriptEngine.Blocked_SelfPacketsEX.ContainsKey(packetex_id))
            {
                ScriptEngine.Blocked_SelfPacketsEX.Remove(packetex_id);
            }
        }

        private void Script_CLEAR_BLOCK_SELF()
        {
            ScriptEngine.Blocked_SelfPackets.Clear();
        }

        private void Script_CLEAR_BLOCKEX_SELF()
        {
            ScriptEngine.Blocked_SelfPacketsEX.Clear();
        }
        
        private void Script_BLOCK(string line)
        {
            int packet_id = Util.GetInt32(Get_String(ref line));

            if (ScriptEngine.Blocked_ServerPackets.ContainsKey(packet_id))
            {
            }
            else
            {
                ScriptEngine.Blocked_ServerPackets.Add(packet_id, 0);
            }
        }

        private void Script_BLOCKEX(string line)
        {
            int packetex_id = Util.GetInt32(Get_String(ref line));

            if (ScriptEngine.Blocked_ServerPacketsEX.ContainsKey(packetex_id))
            {
            }
            else
            {
                ScriptEngine.Blocked_ServerPacketsEX.Add(packetex_id, 0);
            }
        }

        private void Script_UNBLOCK(string line)
        {
            int packet_id = Util.GetInt32(Get_String(ref line));

            if (ScriptEngine.Blocked_ServerPackets.ContainsKey(packet_id))
            {
                ScriptEngine.Blocked_ServerPackets.Remove(packet_id);
            }
        }

        private void Script_UNBLOCKEX(string line)
        {
            int packetex_id = Util.GetInt32(Get_String(ref line));

            if (ScriptEngine.Blocked_ServerPacketsEX.ContainsKey(packetex_id))
            {
                ScriptEngine.Blocked_ServerPacketsEX.Remove(packetex_id);
            }
        }

        private void Script_CLEAR_BLOCK()
        {
            ScriptEngine.Blocked_ServerPackets.Clear();
        }

        private void Script_CLEAR_BLOCKEX()
        {
            ScriptEngine.Blocked_ServerPacketsEX.Clear();
        }

        private void Script_TARGET_SELF()
        {
            ServerPackets.Target(Globals.gamedata.my_char.ID, (int)Globals.gamedata.my_char.X, (int)Globals.gamedata.my_char.Y, (int)Globals.gamedata.my_char.Z, true);
        }

        private void Script_CANCEL_TARGET()
        {
            ServerPackets.Send_CancelTarget();
        }

        private void Script_SLEEP_HUMAN_READING(string inp)
        {
            string text = Get_String(ref inp);

            float words = text.Length / Globals.Average_Word_Length;

            float speed = System.Convert.ToSingle(Globals.Rando.NextDouble()) + 3.3F;

            float time = words / speed * 1000.0F;

            ((ScriptThread)Threads[CurrentThread]).Sleep_Until = System.DateTime.Now.AddMilliseconds(time);
        }

        private void Script_SLEEP_HUMAN_WRITING(string inp)
        {
            string text = Get_String(ref inp);

            //gotta find the relative difference between the letters

            //fast = 7 char per second
            //slow = 2.5 char per second

            //best difference = 0

            text = text.ToLower();

            float diff = 0;

            for (int i = 0; i < text.Length - 1; i++)
            {
                diff += Math.Abs(ELIZA.Get_Complexity(text[i].ToString()) - ELIZA.Get_Complexity(text[i + 1].ToString()));
            }

            float keys = diff / Globals.Difficulty_Balance;

            float speed = System.Convert.ToSingle(Globals.Rando.NextDouble()) + 3.0F;

            float time = keys / speed * 1000;

            ((ScriptThread)Threads[CurrentThread]).Sleep_Until = System.DateTime.Now.AddMilliseconds(time);
        }

        private void Script_GET_ELIZA(string inp)
        {
            string sdest = Get_String(ref inp);
            ScriptVariable dest = Get_Var(sdest);

            if (dest.Type != Var_Types.STRING)
            {
                ScriptEngine.Script_Error("INVLAID DESTINATION TYPE");
                return;
            }

            string query = Get_String(ref inp);
            string reply = ELIZA.GetReply(query);

            dest.Value = reply;
        }

        private void Script_GET_NPCS(string inp)
        {
            string sdest = Get_String(ref inp);
            ScriptVariable dest = Get_Var(sdest);

            if (dest.Type == Var_Types.ARRAYLIST)
            {
                ((System.Collections.ArrayList)dest.Value).Clear();
            }
            else if (dest.Type == Var_Types.SORTEDLIST)
            {
                ((System.Collections.SortedList)dest.Value).Clear();
            }
            else
            {
                ScriptEngine.Script_Error("INVLAID DESTINATION TYPE");
                return;
            }

            Globals.NPCLock.EnterReadLock();
            try
            {
                foreach (NPCInfo npc in Globals.gamedata.nearby_npcs.Values)
                {
                    Script_ClassData cd = new Script_ClassData();
                    cd.Name = "NPC";
                    cd._Variables.Add("ID", new ScriptVariable((long)npc.ID, "ID", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("X", new ScriptVariable((long)npc.X, "X", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("Y", new ScriptVariable((long)npc.Y, "Y", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("Z", new ScriptVariable((long)npc.Z, "Z", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("NAME", new ScriptVariable(Util.GetNPCName(npc.NPCID), "NAME", Var_Types.STRING, Var_State.PUBLIC));

                    cd._Variables.Add("NPC_ID", new ScriptVariable((long)npc.NPCID, "NPC_ID", Var_Types.INT, Var_State.PUBLIC));

                    cd._Variables.Add("TITLE", new ScriptVariable(npc.Title, "TITLE", Var_Types.STRING, Var_State.PUBLIC));
                    cd._Variables.Add("ATTACKABLE", new ScriptVariable((long)npc.isAttackable, "ATTACKABLE", Var_Types.INT, Var_State.PUBLIC));

                    cd._Variables.Add("LEVEL", new ScriptVariable((long)npc.Level, "LEVEL", Var_Types.INT, Var_State.PUBLIC));

                    cd._Variables.Add("HP", new ScriptVariable((long)npc.Cur_HP, "HP", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("MAX_HP", new ScriptVariable((long)npc.Max_HP, "MAX_HP", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("MP", new ScriptVariable((long)npc.Cur_MP, "MP", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("MAX_MP", new ScriptVariable((long)npc.Max_MP, "MAX_MP", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("CP", new ScriptVariable((long)npc.Cur_CP, "CP", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("MAX_CP", new ScriptVariable((long)npc.Max_CP, "MAX_CP", Var_Types.INT, Var_State.PUBLIC));

                    cd._Variables.Add("PER_HP", new ScriptVariable(Math.Round((double)npc.Cur_HP / (double)npc.Max_HP * 100, 2), "PER_HP", Var_Types.DOUBLE, Var_State.PUBLIC));
                    cd._Variables.Add("PER_MP", new ScriptVariable(Math.Round((double)npc.Cur_MP / (double)npc.Max_MP * 100, 2), "PER_MP", Var_Types.DOUBLE, Var_State.PUBLIC));
                    cd._Variables.Add("PER_CP", new ScriptVariable(Math.Round((double)npc.Cur_CP / (double)npc.Max_CP * 100, 2), "PER_CP", Var_Types.DOUBLE, Var_State.PUBLIC));

                    cd._Variables.Add("KARMA", new ScriptVariable((long)npc.Karma, "KARMA", Var_Types.INT, Var_State.PUBLIC));

                    cd._Variables.Add("ATTACK_SPEED", new ScriptVariable((long)(npc.PatkSpeed * npc.AttackSpeedMult), "ATTACK_SPEED", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("CAST_SPEED", new ScriptVariable((long)npc.PatkSpeed, "CAST_SPEED", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("RUN_SPEED", new ScriptVariable((long)(npc.RunSpeed * npc.MoveSpeedMult), "RUN_SPEED", Var_Types.INT, Var_State.PUBLIC));

                    cd._Variables.Add("TARGET_ID", new ScriptVariable((long)npc.TargetID, "TARGET_ID", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("FOLLOW_TARGET_ID", new ScriptVariable((long)npc.MoveTarget, "FOLLOW_TARGET_ID", Var_Types.INT, Var_State.PUBLIC));

                    cd._Variables.Add("DEST_X", new ScriptVariable((long)npc.Dest_X, "DEST_X", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("DEST_Y", new ScriptVariable((long)npc.Dest_Y, "DEST_Y", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("DEST_Z", new ScriptVariable((long)npc.Dest_Z, "DEST_Z", Var_Types.INT, Var_State.PUBLIC));

                    cd._Variables.Add("LOOKS_DEAD", new ScriptVariable((long)npc.isAlikeDead, "LOOKS_DEAD", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("IN_COMBAT", new ScriptVariable((long)npc.isInCombat, "IN_COMBAT", Var_Types.INT, Var_State.PUBLIC));
                    try
                    {
                        System.Collections.SortedList buffs = new System.Collections.SortedList();
                        foreach (CharBuff buff in npc.my_buffs.Values)
                        {
                            Script_ClassData ncd = new Script_ClassData();
                            ncd.Name = "EFFECT";
                            ncd._Variables.Add("ID", new ScriptVariable((long)buff.ID, "ID", Var_Types.INT, Var_State.PUBLIC));
                            ncd._Variables.Add("LEVEL", new ScriptVariable((long)buff.SkillLevel, "LEVEL", Var_Types.INT, Var_State.PUBLIC));
                            ncd._Variables.Add("DURATION", new ScriptVariable((long)buff.ExpiresTime, "DURATION", Var_Types.INT, Var_State.PUBLIC));
                            ncd._Variables.Add("NAME", new ScriptVariable(Util.GetSkillName(buff.ID, buff.SkillLevel), "NAME", Var_Types.STRING, Var_State.PUBLIC));
                            ScriptVariable nsv = new ScriptVariable(ncd, "EFFECT", Var_Types.CLASS, Var_State.PUBLIC);

                            buffs.Add(buff.ID.ToString(), nsv);
                        }

                        cd._Variables.Add("EFFECTS", new ScriptVariable((System.Collections.SortedList)buffs, "EFFECTS", Var_Types.SORTEDLIST, Var_State.PUBLIC));
                    }
                    catch
                    {
                        System.Collections.SortedList empty = new System.Collections.SortedList();
                        cd._Variables.Add("EFFECTS", new ScriptVariable((System.Collections.SortedList)empty, "EFFECTS", Var_Types.SORTEDLIST, Var_State.PUBLIC));
                    }


                    ScriptVariable sv = new ScriptVariable(cd, "NPC", Var_Types.CLASS, Var_State.PUBLIC);

                    if (dest.Type == Var_Types.ARRAYLIST)
                    {
                        ((System.Collections.ArrayList)dest.Value).Add(sv);
                    }
                    else if (dest.Type == Var_Types.SORTEDLIST)
                    {
                        ((System.Collections.SortedList)dest.Value).Add(npc.ID.ToString(), sv);
                    }
                }
            }
            finally
            {
                Globals.NPCLock.ExitReadLock();
            }
        }

        private void Script_GET_INVENTORY(string inp)
        {
            string sdest = Get_String(ref inp);
            ScriptVariable dest = Get_Var(sdest);

            if (dest.Type == Var_Types.ARRAYLIST)
            {
                ((System.Collections.ArrayList)dest.Value).Clear();
            }
            else if (dest.Type == Var_Types.SORTEDLIST)
            {
                ((System.Collections.SortedList)dest.Value).Clear();
            }
            else
            {
                ScriptEngine.Script_Error("INVLAID DESTINATION TYPE");
                return;
            }

            Globals.InventoryLock.EnterReadLock();
            try
            {
                foreach (InventoryInfo it in Globals.gamedata.inventory.Values)
                {
                    Script_ClassData cd = new Script_ClassData();
                    cd.Name = "INVENTORY";
                    cd._Variables.Add("ID", new ScriptVariable((long)it.ID, "ID", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("NAME", new ScriptVariable(Util.GetItemName(it.ItemID), "NAME", Var_Types.STRING, Var_State.PUBLIC));

                    cd._Variables.Add("ITEM_ID", new ScriptVariable((long)it.ItemID, "ITEM_ID", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("COUNT", new ScriptVariable((long)it.Count, "COUNT", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("EQUIPPED", new ScriptVariable((long)it.isEquipped, "EQUIPPED", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("SLOT", new ScriptVariable((long)it.Slot, "SLOT", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("ENCHANT", new ScriptVariable((long)it.Enchant, "ENCHANT", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("AUG_ID", new ScriptVariable((long)it.AugID, "AUG_ID", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("MANA", new ScriptVariable((long)it.Mana, "MANA", Var_Types.INT, Var_State.PUBLIC));                    
                    cd._Variables.Add("LEASE_TIME", new ScriptVariable((long)it.lease_time, "LEASE_TIME", Var_Types.INT, Var_State.PUBLIC));

                    cd._Variables.Add("ATTACK_ELEMENT", new ScriptVariable((long)it.attack_element, "ATTACK_ELEMENT", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("ATTACK_MAGNITUDE", new ScriptVariable((long)it.attack_magnitude, "ATTACK_MAGNITUDE", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("FIRE_DEFENSE", new ScriptVariable((long)it.fire_defense, "FIRE_DEFENSE", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("WATER_DEFENSE", new ScriptVariable((long)it.water_defense, "WATER_DEFENSE", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("WIND_DEFENSE", new ScriptVariable((long)it.wind_defense, "WIND_DEFENSE", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("EARTH_DEFENSE", new ScriptVariable((long)it.earth_defense, "EARTH_DEFENSE", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("UNHOLY_DEFENSE", new ScriptVariable((long)it.unholy_defense, "UNHOLY_DEFENSE", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("HOLY_DEFENSE", new ScriptVariable((long)it.holy_defense, "HOLY_DEFENSE", Var_Types.INT, Var_State.PUBLIC));

                    cd._Variables.Add("ENCHANT_EFFECT_0", new ScriptVariable((long)it.enchant_effect_0, "ENCHANT_EFFECT_0", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("ENCHANT_EFFECT_1", new ScriptVariable((long)it.enchant_effect_1, "ENCHANT_EFFECT_1", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("ENCHANT_EFFECT_2", new ScriptVariable((long)it.enchant_effect_2, "ENCHANT_EFFECT_2", Var_Types.INT, Var_State.PUBLIC));

                    ScriptVariable sv = new ScriptVariable(cd, "INVENTORY", Var_Types.CLASS, Var_State.PUBLIC);

                    if (dest.Type == Var_Types.ARRAYLIST)
                    {
                        ((System.Collections.ArrayList)dest.Value).Add(sv);
                    }
                    else if (dest.Type == Var_Types.SORTEDLIST)
                    {
                        ((System.Collections.SortedList)dest.Value).Add(it.ID.ToString(), sv);
                    }
                }
            }
            finally
            {
                Globals.InventoryLock.ExitReadLock();
            }
        }

        private void Script_GET_ITEMS(string inp)
        {
            string sdest = Get_String(ref inp);
            ScriptVariable dest = Get_Var(sdest);

            if (dest.Type == Var_Types.ARRAYLIST)
            {
                ((System.Collections.ArrayList)dest.Value).Clear();
            }
            else if (dest.Type == Var_Types.SORTEDLIST)
            {
                ((System.Collections.SortedList)dest.Value).Clear();
            }
            else
            {
                ScriptEngine.Script_Error("INVLAID DESTINATION TYPE");
                return;
            }

            Globals.ItemLock.EnterReadLock();
            try
            {
                foreach (ItemInfo it in Globals.gamedata.nearby_items.Values)
                {
                    Script_ClassData cd = new Script_ClassData();
                    cd.Name = "ITEM";
                    cd._Variables.Add("ID", new ScriptVariable((long)it.ID, "ID", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("X", new ScriptVariable((long)it.X, "X", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("Y", new ScriptVariable((long)it.Y, "Y", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("Z", new ScriptVariable((long)it.Z, "Z", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("NAME", new ScriptVariable(Util.GetItemName(it.ItemID), "NAME", Var_Types.STRING, Var_State.PUBLIC));

                    cd._Variables.Add("ITEM_ID", new ScriptVariable((long)it.ItemID, "ITEM_ID", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("COUNT", new ScriptVariable((long)it.Count, "COUNT", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("DROPPED_BY", new ScriptVariable((long)it.DroppedBy, "DROPPED_BY", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("DROP_RADIUS", new ScriptVariable((long)it.DropRadius, "DROP_RADIUS", Var_Types.DOUBLE, Var_State.PUBLIC));
                    cd._Variables.Add("STACKABLE", new ScriptVariable((long)it.Stackable, "STACKABLE", Var_Types.INT, Var_State.PUBLIC));

                    ScriptVariable sv = new ScriptVariable(cd, "ITEM", Var_Types.CLASS, Var_State.PUBLIC);

                    if (dest.Type == Var_Types.ARRAYLIST)
                    {
                        ((System.Collections.ArrayList)dest.Value).Add(sv);
                    }
                    else if (dest.Type == Var_Types.SORTEDLIST)
                    {
                        ((System.Collections.SortedList)dest.Value).Add(it.ID.ToString(), sv);
                    }
                }
            }
            finally
            {
                Globals.ItemLock.ExitReadLock();
            }
        }

        private void Script_GET_SKILLS(string inp)
        {
                string sdest = Get_String(ref inp);
                ScriptVariable dest = Get_Var(sdest);

                if (dest.Type == Var_Types.ARRAYLIST)
                {
                    ((System.Collections.ArrayList)dest.Value).Clear();
                }
                else if (dest.Type == Var_Types.SORTEDLIST)
                {
                    ((System.Collections.SortedList)dest.Value).Clear();
                }
                else
                {
                    ScriptEngine.Script_Error("GET_SKILLS: INVLAID DESTINATION TYPE - SORTEDLIST OR ARRAYLIST ONLY");
                    return;
                }

                Globals.SkillListLock.EnterReadLock();
                try
                {
                    foreach (UserSkill us in Globals.gamedata.skills.Values)
                    {
                        Script_ClassData cd = new Script_ClassData();
                        cd.Name = "SKILL";
                        cd._Variables.Add("ID", new ScriptVariable((long)us.ID, "ID", Var_Types.INT, Var_State.PUBLIC));
                        cd._Variables.Add("LEVEL", new ScriptVariable((long)us.Level, "LEVEL", Var_Types.INT, Var_State.PUBLIC));
                        cd._Variables.Add("PASSIVE", new ScriptVariable((long)us.Passive, "PASSIVE", Var_Types.INT, Var_State.PUBLIC));
                        cd._Variables.Add("NAME", new ScriptVariable(Util.GetSkillName(us.ID, us.Level), "NAME", Var_Types.STRING, Var_State.PUBLIC));
                        cd._Variables.Add("REUSE", new ScriptVariable(Util.GetSkillReuse(us.ID), "REUSE", Var_Types.INT, Var_State.PUBLIC));

                        ScriptVariable sv = new ScriptVariable(cd, "SKILL", Var_Types.CLASS, Var_State.PUBLIC);

                        if (dest.Type == Var_Types.ARRAYLIST)
                        {
                            ((System.Collections.ArrayList)dest.Value).Add(sv);
                        }
                        else if (dest.Type == Var_Types.SORTEDLIST)
                        {
                            ((System.Collections.SortedList)dest.Value).Add(us.ID.ToString(), sv);
                        }
                    }
                }
                finally
                {
                    Globals.SkillListLock.ExitReadLock();
                }
        }

        private void Script_GET_EFFECTS(string inp)
        {
                string sdest = Get_String(ref inp);
                string s_id = Get_String(ref inp);
                uint _id = Util.GetUInt32(s_id);

                ScriptVariable dest = Get_Var(sdest);

                if (dest.Type == Var_Types.SORTEDLIST)
                {
                    ((System.Collections.SortedList)dest.Value).Clear();
                }
                else
                {
                    ScriptEngine.Script_Error("Usage: GET_EFFECTS SORTEDLIST OBJECT_ID");
                    return;
                }

                Globals.PartyLock.EnterReadLock();
                try
                {
                    TargetType idType = Util.GetType(_id);
                    switch (idType)
                    {
                        case TargetType.NONE:
                        case TargetType.ERROR:
                        case TargetType.ITEM:
                        case TargetType.MYPET:
                        case TargetType.MYPET1:
                        case TargetType.MYPET2:
                        case TargetType.MYPET3:
                        case TargetType.NPC:
                            try
                            {
                                if (Globals.gamedata.nearby_npcs.ContainsKey(_id))
                                {
                                    NPCInfo npc = null;
                                    Globals.NPCLock.EnterReadLock();
                                    try
                                    {
                                        npc = Util.GetNPC(_id);
                                        foreach (CharBuff buff in npc.my_buffs.Values)
                                        {
                                            Script_ClassData cd = new Script_ClassData();
                                            cd.Name = "EFFECT";
                                            cd._Variables.Add("ID", new ScriptVariable((long)buff.ID, "ID", Var_Types.INT, Var_State.PUBLIC));
                                            cd._Variables.Add("LEVEL", new ScriptVariable((long)buff.SkillLevel, "LEVEL", Var_Types.INT, Var_State.PUBLIC));
                                            cd._Variables.Add("DURATION", new ScriptVariable((long)buff.ExpiresTime, "DURATION", Var_Types.INT, Var_State.PUBLIC));
                                            cd._Variables.Add("EFFECT_TIME", new ScriptVariable((long)buff.EFFECT_TIME, "EFFECT_TIME", Var_Types.INT, Var_State.PUBLIC));
                                            cd._Variables.Add("NAME", new ScriptVariable(Util.GetSkillName(buff.ID, buff.SkillLevel), "NAME", Var_Types.STRING, Var_State.PUBLIC));
                                            ScriptVariable sv = new ScriptVariable(cd, "EFFECT", Var_Types.CLASS, Var_State.PUBLIC);

                                            if (dest.Type == Var_Types.SORTEDLIST)
                                            {
                                                // xxx - Think about this before implementing
                                                // ((System.Collections.SortedList)dest.Value).Add(skill_name, sv);
                                                ((System.Collections.SortedList)dest.Value).Add(buff.ID.ToString(), sv);
                                            }
                                        }

                                    }
                                    finally
                                    {
                                        Globals.NPCLock.ExitReadLock();
                                    }

                                }

                            }
                            finally
                            {

                            }
                            return;
                        case TargetType.PLAYER:
                            try
                            {
                                if (Globals.gamedata.PartyMembers.ContainsKey(_id))
                                {

                                    PartyMember ph = null;

                                    Globals.PlayerLock.EnterReadLock();
                                    try
                                    {
                                        ph = Util.GetCharParty(_id);

                                        foreach (CharBuff buff in ph.my_buffs.Values)
                                        {
                                            Script_ClassData cd = new Script_ClassData();
                                            cd.Name = "EFFECT";
                                            cd._Variables.Add("ID", new ScriptVariable((long)buff.ID, "ID", Var_Types.INT, Var_State.PUBLIC));
                                            cd._Variables.Add("LEVEL", new ScriptVariable((long)buff.SkillLevel, "LEVEL", Var_Types.INT, Var_State.PUBLIC));
                                            cd._Variables.Add("DURATION", new ScriptVariable((long)buff.ExpiresTime, "DURATION", Var_Types.INT, Var_State.PUBLIC));
                                            cd._Variables.Add("EFFECT_TIME", new ScriptVariable((long)buff.EFFECT_TIME, "EFFECT_TIME", Var_Types.INT, Var_State.PUBLIC));
                                            cd._Variables.Add("NAME", new ScriptVariable(Util.GetSkillName(buff.ID, buff.SkillLevel), "NAME", Var_Types.STRING, Var_State.PUBLIC));
                                            ScriptVariable sv = new ScriptVariable(cd, "EFFECT", Var_Types.CLASS, Var_State.PUBLIC);

                                            if (dest.Type == Var_Types.SORTEDLIST)
                                            {
                                                // xxx - Think about this before implementing
                                                // ((System.Collections.SortedList)dest.Value).Add(skill_name, sv);
                                                ((System.Collections.SortedList)dest.Value).Add(buff.ID.ToString(), sv);
                                            }
                                        }
                                    }
                                    finally
                                    {
                                        Globals.PlayerLock.ExitReadLock();
                                    }
                                }
                                else // not in pt 
                                {
                                         Globals.PlayerLock.EnterReadLock();
                                         try
                                            {
                                                 CharInfo pla = Util.GetChar(_id);
                                                  if (pla != null)
                                                       {


                                                           foreach (CharBuff buff in pla.my_buffs.Values)
                                                           {
                                                               Script_ClassData cd = new Script_ClassData();
                                                               cd.Name = "EFFECT";
                                                               cd._Variables.Add("ID", new ScriptVariable((long)buff.ID, "ID", Var_Types.INT, Var_State.PUBLIC));
                                                               cd._Variables.Add("LEVEL", new ScriptVariable((long)buff.SkillLevel, "LEVEL", Var_Types.INT, Var_State.PUBLIC));
                                                               cd._Variables.Add("DURATION", new ScriptVariable((long)buff.ExpiresTime, "DURATION", Var_Types.INT, Var_State.PUBLIC));
                                                               cd._Variables.Add("EFFECT_TIME", new ScriptVariable((long)buff.EFFECT_TIME, "EFFECT_TIME", Var_Types.INT, Var_State.PUBLIC));
                                                               cd._Variables.Add("NAME", new ScriptVariable(Util.GetSkillName(buff.ID, buff.SkillLevel), "NAME", Var_Types.STRING, Var_State.PUBLIC));
                                                               ScriptVariable sv = new ScriptVariable(cd, "EFFECT", Var_Types.CLASS, Var_State.PUBLIC);

                                                               if (dest.Type == Var_Types.SORTEDLIST)
                                                               {
                                                                   // xxx - Think about this before implementing
                                                                   // ((System.Collections.SortedList)dest.Value).Add(skill_name, sv);
                                                                   ((System.Collections.SortedList)dest.Value).Add(buff.ID.ToString(), sv);
                                                               }
                                                           }
                                                        }
                                            }
                                            finally
                                            {
                                                Globals.PlayerLock.ExitReadLock();
                                             }
                                }
                            }
                            finally
                            {

                            }
                            return;
                        case TargetType.SELF:
                            Globals.MyBuffsListLock.EnterWriteLock();
                            try
                            {
                                foreach (CharBuff buff in Globals.gamedata.mybuffs.Values)
                                {
                                    Script_ClassData cd = new Script_ClassData();
                                    cd.Name = "EFFECT";
                                    cd._Variables.Add("ID", new ScriptVariable((long)buff.ID, "ID", Var_Types.INT, Var_State.PUBLIC));
                                    cd._Variables.Add("LEVEL", new ScriptVariable((long)buff.SkillLevel, "LEVEL", Var_Types.INT, Var_State.PUBLIC));
                                    cd._Variables.Add("DURATION", new ScriptVariable((long)buff.ExpiresTime, "DURATION", Var_Types.INT, Var_State.PUBLIC));
                                    cd._Variables.Add("EFFECT_TIME", new ScriptVariable((long)buff.EFFECT_TIME, "EFFECT_TIME", Var_Types.INT, Var_State.PUBLIC));
                                    cd._Variables.Add("NAME", new ScriptVariable(Util.GetSkillName(buff.ID, buff.SkillLevel), "NAME", Var_Types.STRING, Var_State.PUBLIC));
                                    ScriptVariable sv = new ScriptVariable(cd, "EFFECT", Var_Types.CLASS, Var_State.PUBLIC);

                                    if (dest.Type == Var_Types.SORTEDLIST)
                                    {
                                        // xxx - Think about this before implementing
                                        // ((System.Collections.SortedList)dest.Value).Add(skill_name, sv);
                                        ((System.Collections.SortedList)dest.Value).Add(buff.ID.ToString(), sv);
                                    }

                                }
                            }
                            finally
                            {
                                Globals.MyBuffsListLock.ExitWriteLock();
                            }

                            return;

                        default:
                            return;
                    }
                }

                finally
                {
                    Globals.PartyLock.ExitReadLock();
                }
        }

        private void Script_GET_PARTY(string inp)
        {
            string sdest = Get_String(ref inp);
            ScriptVariable dest = Get_Var(sdest);

            if (dest.Type == Var_Types.ARRAYLIST)
            {
                ((System.Collections.ArrayList)dest.Value).Clear();
            }
            else if (dest.Type == Var_Types.SORTEDLIST)
            {
                ((System.Collections.SortedList)dest.Value).Clear();
            }
            else
            {
                ScriptEngine.Script_Error("INVLAID DESTINATION TYPE");
                return;
            }

            Globals.PartyLock.EnterReadLock();
            try
            {
                foreach (PartyMember pl in Globals.gamedata.PartyMembers.Values)
                {
                    CharInfo ch;

                    Globals.PlayerLock.EnterReadLock();
                    try
                    {
                        ch = Util.GetChar(pl.ID);
                    }
                    finally
                    {
                        Globals.PlayerLock.ExitReadLock();
                    }

                    Script_ClassData cd = new Script_ClassData();
                    cd.Name = "PLAYER";
                    cd._Variables.Add("ID", new ScriptVariable((long)pl.ID, "ID", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("NAME", new ScriptVariable(pl.Name, "NAME", Var_Types.STRING, Var_State.PUBLIC));

                    cd._Variables.Add("CLASS", new ScriptVariable((long)pl.Class, "CLASS", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("LEVEL", new ScriptVariable((long)pl.Level, "LEVEL", Var_Types.INT, Var_State.PUBLIC));

                    cd._Variables.Add("HP", new ScriptVariable((long)pl.Cur_HP, "HP", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("MAX_HP", new ScriptVariable((long)pl.Max_HP, "MAX_HP", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("MP", new ScriptVariable((long)pl.Cur_MP, "MP", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("MAX_MP", new ScriptVariable((long)pl.Max_MP, "MAX_MP", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("CP", new ScriptVariable((long)pl.Cur_CP, "CP", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("MAX_CP", new ScriptVariable((long)pl.Max_CP, "MAX_CP", Var_Types.INT, Var_State.PUBLIC));


                    cd._Variables.Add("PER_HP", new ScriptVariable(Math.Round((double)pl.Cur_HP / (double)pl.Max_HP * 100, 2), "PER_HP", Var_Types.DOUBLE, Var_State.PUBLIC));
                    cd._Variables.Add("PER_MP", new ScriptVariable(Math.Round((double)pl.Cur_MP / (double)pl.Max_MP * 100, 2), "PER_MP", Var_Types.DOUBLE, Var_State.PUBLIC));
                    cd._Variables.Add("PER_CP", new ScriptVariable(Math.Round((double)pl.Cur_CP / (double)pl.Max_CP * 100, 2), "PER_CP", Var_Types.DOUBLE, Var_State.PUBLIC));

                    try
                    {
                        System.Collections.SortedList buffs = new System.Collections.SortedList();
                        foreach (CharBuff buff in pl.my_buffs.Values)
                        {
                            Script_ClassData ncd = new Script_ClassData();
                            ncd.Name = "EFFECT";
                            ncd._Variables.Add("ID", new ScriptVariable((long)buff.ID, "ID", Var_Types.INT, Var_State.PUBLIC));
                            ncd._Variables.Add("LEVEL", new ScriptVariable((long)buff.SkillLevel, "LEVEL", Var_Types.INT, Var_State.PUBLIC));
                            ncd._Variables.Add("DURATION", new ScriptVariable((long)buff.ExpiresTime, "DURATION", Var_Types.INT, Var_State.PUBLIC));
                            ncd._Variables.Add("NAME", new ScriptVariable(Util.GetSkillName(buff.ID, buff.SkillLevel), "NAME", Var_Types.STRING, Var_State.PUBLIC));
                            ScriptVariable nsv = new ScriptVariable(ncd, "EFFECT", Var_Types.CLASS, Var_State.PUBLIC);

                            buffs.Add(buff.ID.ToString(), nsv);
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
                        cd._Variables.Add("X", new ScriptVariable((long)pl.X, "X", Var_Types.INT, Var_State.PUBLIC));
                        cd._Variables.Add("Y", new ScriptVariable((long)pl.Y, "Y", Var_Types.INT, Var_State.PUBLIC));
                        cd._Variables.Add("Z", new ScriptVariable((long)pl.Z, "Z", Var_Types.INT, Var_State.PUBLIC));

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

                    ScriptVariable sv = new ScriptVariable(cd, "PLAYER", Var_Types.CLASS, Var_State.PUBLIC);

                    if (dest.Type == Var_Types.ARRAYLIST)
                    {
                        ((System.Collections.ArrayList)dest.Value).Add(sv);
                    }
                    else if (dest.Type == Var_Types.SORTEDLIST)
                    {
                        ((System.Collections.SortedList)dest.Value).Add(pl.ID.ToString(), sv);
                    }
                }
            }
            finally
            {
                Globals.PartyLock.ExitReadLock();
            }
        }

        private void Script_GET_PLAYERS(string inp)
        {
            string sdest = Get_String(ref inp);
            ScriptVariable dest = Get_Var(sdest);

            if (dest.Type == Var_Types.ARRAYLIST)
            {
                ((System.Collections.ArrayList)dest.Value).Clear();
            }
            else if (dest.Type == Var_Types.SORTEDLIST)
            {
                ((System.Collections.SortedList)dest.Value).Clear();
            }
            else
            {
                ScriptEngine.Script_Error("INVLAID DESTINATION TYPE");
                return;
            }

            Globals.PlayerLock.EnterReadLock();
            try
            {
                foreach (CharInfo ch in Globals.gamedata.nearby_chars.Values)
                {
                    Script_ClassData cd = new Script_ClassData();
                    cd.Name = "PLAYER";
                    cd._Variables.Add("ID", new ScriptVariable((long)ch.ID, "ID", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("X", new ScriptVariable((long)ch.X, "X", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("Y", new ScriptVariable((long)ch.Y, "Y", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("Z", new ScriptVariable((long)ch.Z, "Z", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("NAME", new ScriptVariable(ch.Name, "NAME", Var_Types.STRING, Var_State.PUBLIC));

                    cd._Variables.Add("TITLE", new ScriptVariable(ch.Title, "TITLE", Var_Types.STRING, Var_State.PUBLIC));
                    cd._Variables.Add("CLAN", new ScriptVariable((long)ch.ClanID, "CLAN", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("ALLY", new ScriptVariable((long)ch.AllyID, "ALLY", Var_Types.INT, Var_State.PUBLIC));

                    cd._Variables.Add("RACE", new ScriptVariable((long)ch.Race, "RACE", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("SEX", new ScriptVariable((long)ch.Sex, "SEX", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("CLASS", new ScriptVariable((long)ch.Class, "CLASS", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("LEVEL", new ScriptVariable((long)ch.Level, "LEVEL", Var_Types.INT, Var_State.PUBLIC));

                    cd._Variables.Add("HP", new ScriptVariable((long)ch.Cur_HP, "HP", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("MAX_HP", new ScriptVariable((long)ch.Max_HP, "MAX_HP", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("MP", new ScriptVariable((long)ch.Cur_MP, "MP", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("MAX_MP", new ScriptVariable((long)ch.Max_MP, "MAX_MP", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("CP", new ScriptVariable((long)ch.Cur_CP, "CP", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("MAX_CP", new ScriptVariable((long)ch.Max_CP, "MAX_CP", Var_Types.INT, Var_State.PUBLIC));

                    cd._Variables.Add("PER_HP", new ScriptVariable(Math.Round((double)ch.Cur_HP / (double)ch.Max_HP * 100, 2), "PER_HP", Var_Types.DOUBLE, Var_State.PUBLIC));
                    cd._Variables.Add("PER_MP", new ScriptVariable(Math.Round((double)ch.Cur_MP / (double)ch.Max_MP * 100, 2), "PER_MP", Var_Types.DOUBLE, Var_State.PUBLIC));
                    cd._Variables.Add("PER_CP", new ScriptVariable(Math.Round((double)ch.Cur_CP / (double)ch.Max_CP * 100, 2), "PER_CP", Var_Types.DOUBLE, Var_State.PUBLIC));

                    cd._Variables.Add("PVPFLAG", new ScriptVariable((long)ch.PvPFlag, "PVPFLAG", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("KARMA", new ScriptVariable((long)ch.Karma, "KARMA", Var_Types.INT, Var_State.PUBLIC));

                    cd._Variables.Add("ATTACK_SPEED", new ScriptVariable((long)(ch.PatkSpeed * ch.AttackSpeedMult), "ATTACK_SPEED", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("CAST_SPEED", new ScriptVariable((long)ch.MatkSpeed, "CAST_SPEED", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("RUN_SPEED", new ScriptVariable((long)(ch.RunSpeed * ch.MoveSpeedMult), "RUN_SPEED", Var_Types.INT, Var_State.PUBLIC));

                    cd._Variables.Add("TARGET_ID", new ScriptVariable((long)ch.TargetID, "TARGET_ID", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("FOLLOW_TARGET_ID", new ScriptVariable((long)ch.MoveTarget, "FOLLOW_TARGET_ID", Var_Types.INT, Var_State.PUBLIC));

                    cd._Variables.Add("DEST_X", new ScriptVariable((long)ch.Dest_X, "DEST_X", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("DEST_Y", new ScriptVariable((long)ch.Dest_Y, "DEST_Y", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("DEST_Z", new ScriptVariable((long)ch.Dest_Z, "DEST_Z", Var_Types.INT, Var_State.PUBLIC));

                    cd._Variables.Add("LOOKS_DEAD", new ScriptVariable((long)ch.isAlikeDead, "LOOKS_DEAD", Var_Types.INT, Var_State.PUBLIC));
                    cd._Variables.Add("IN_COMBAT", new ScriptVariable((long)ch.isInCombat, "IN_COMBAT", Var_Types.INT, Var_State.PUBLIC));
                    try
                    {
                        System.Collections.SortedList buffs = new System.Collections.SortedList();
                        foreach (CharBuff buff in ch.my_buffs.Values)
                        {
                            Script_ClassData ncd = new Script_ClassData();
                            ncd.Name = "EFFECT";
                            ncd._Variables.Add("ID", new ScriptVariable((long)buff.ID, "ID", Var_Types.INT, Var_State.PUBLIC));
                            ncd._Variables.Add("LEVEL", new ScriptVariable((long)buff.SkillLevel, "LEVEL", Var_Types.INT, Var_State.PUBLIC));
                            ncd._Variables.Add("DURATION", new ScriptVariable((long)buff.ExpiresTime, "DURATION", Var_Types.INT, Var_State.PUBLIC));
                            ncd._Variables.Add("NAME", new ScriptVariable(Util.GetSkillName(buff.ID, buff.SkillLevel), "NAME", Var_Types.STRING, Var_State.PUBLIC));
                            ScriptVariable nsv = new ScriptVariable(ncd, "EFFECT", Var_Types.CLASS, Var_State.PUBLIC);

                            buffs.Add(buff.ID.ToString(), nsv);
                        }

                        cd._Variables.Add("EFFECTS", new ScriptVariable((System.Collections.SortedList)buffs, "EFFECTS", Var_Types.SORTEDLIST, Var_State.PUBLIC));
                    }
                    catch
                    {
                        System.Collections.SortedList empty = new System.Collections.SortedList();
                        cd._Variables.Add("EFFECTS", new ScriptVariable((System.Collections.SortedList)empty, "EFFECTS", Var_Types.SORTEDLIST, Var_State.PUBLIC));
                    }




                    ScriptVariable sv = new ScriptVariable(cd, "PLAYER", Var_Types.CLASS, Var_State.PUBLIC);

                    if (dest.Type == Var_Types.ARRAYLIST)
                    {
                        ((System.Collections.ArrayList)dest.Value).Add(sv);
                    }
                    else if (dest.Type == Var_Types.SORTEDLIST)
                    {
                        ((System.Collections.SortedList)dest.Value).Add(ch.ID.ToString(), sv);
                    }
                }
            }
            finally
            {
                Globals.PlayerLock.ExitReadLock();
            }
        }

        private void Script_FORCE_LOG()
        {
            Util.Stop_Connections();
        }

        private void Script_DELETE_SHORTCUT(string inp)
        {
            string svar = Get_String(ref inp);
            int var = Util.GetInt32(svar);

            ServerPackets.DeleteShortCut(var);
        }

        private void Script_REGISTER_SHORTCUT(string inp)
        {
            string stype = Get_String(ref inp);
            int type = Util.GetInt32(stype);

            string sslot = Get_String(ref inp);
            uint slot = (uint)Util.GetInt32(sslot);

            string sid = Get_String(ref inp);
            uint id = (uint)Util.GetInt32(sid);

            ServerPackets.RegisterShortCut(type, slot, id, 3);//TODO -WTF is this last value?
        }

        private void Script_USE_SHORTCUT(string inp)
        {
            string svar = Get_String(ref inp);
            string s_control = Get_String(ref inp);
            string s_shift = Get_String(ref inp);

            int var = Util.GetInt32(svar);
            uint control = Util.GetUInt32(s_control);
            uint shift = Util.GetUInt32(s_shift);

            ServerPackets.Use_ShortCut(var, control != 0, shift != 0);
        }

        private void Script_CRYSTALIZE_ITEM(string line)
        {
            string sID = Get_String(ref line);
            string sCount = Get_String(ref line);

            uint ID = Util.GetUInt32(sID);
            uint Count = Util.GetUInt32(sCount);

            ServerPackets.CrystalizeItem(ID, Count);
        }

        private void Script_DELETE_ITEM(string line)
        {
            string sID = Get_String(ref line);
            string sCount = Get_String(ref line);

            uint ID = Util.GetUInt32(sID);
            uint Count = Util.GetUInt32(sCount);

            ServerPackets.DeleteItem(ID, Count);
        }

        private void Script_DROP_ITEM(string line)
        {
            string sID = Get_String(ref line);
            string sCount = Get_String(ref line);
            string sX = Get_String(ref line);
            string sY = Get_String(ref line);
            string sZ = Get_String(ref line);

            uint ID = Util.GetUInt32(sID);
            uint Count = Util.GetUInt32(sCount);
            int X = Util.GetInt32(sX);
            int Y = Util.GetInt32(sY);
            int Z = Util.GetInt32(sZ);

            ServerPackets.DropItem(ID, Count, X, Y, Z);
        }

        private void Script_BOTSET(string line)
        {
            string setting = Get_String(ref line);
            setting = setting.ToUpperInvariant();

            switch (setting)
            {
                case "ACTIVEFOLLOW_ON":
                    if (Util.GetInt32(Get_String(ref line)) == 1)
                        Globals.gamedata.botoptions.ActiveFollow = 1;
                    else
                        Globals.gamedata.botoptions.ActiveFollow = 0;
                    break;
                case "ACTIVEFOLLOW_WALKERSTYLE":
                    Globals.gamedata.botoptions.ActiveFollowStyle = Util.GetInt32(Get_String(ref line));
                    break;
                case "ACTIVEFOLLOW_ATTACK":
                    Globals.gamedata.botoptions.ActiveFollowAttack = Util.GetInt32(Get_String(ref line));
                    break;
                case "ACTIVEFOLLOW_TARGET":
                    Globals.gamedata.botoptions.ActiveFollowTarget = Util.GetInt32(Get_String(ref line));
                    break;
                case "ACTIVEFOLLOW_NAME":
                    Globals.gamedata.botoptions.Set_ActiveFollow(Get_String(ref line));
                    break;
                case "ACTIVEFOLLOW_DISTANCE":
                    Globals.gamedata.botoptions.ActiveFollowDistance = Util.GetInt32(Get_String(ref line));
                    break;
                case "CLEAR_ITEMS":
                    Globals.ItemListLock.EnterWriteLock();
                    try
                    {
                        BotOptions.ItemTargets.Clear();
                    }
                    finally
                    {
                        Globals.ItemListLock.ExitWriteLock();
                    }
                    break;
                case "CLEAR_BUFFS":
                    Globals.BuffListLock.EnterWriteLock();
                    try
                    {
                        BotOptions.BuffTargets.Clear();
                    }
                    finally
                    {
                        Globals.BuffListLock.ExitWriteLock();
                    }
                    break;
                case "CLEAR_COMBAT":
                    Globals.CombatListLock.EnterWriteLock();
                    try
                    {
                        BotOptions.CombatTargets.Clear();
                    }
                    finally
                    {
                        Globals.CombatListLock.ExitWriteLock();
                    }
                    break;
                case "ACCEPT_INVITE":
                    Globals.gamedata.botoptions.AcceptPartyNames = Get_String(ref line);
                    break;
                case "ACCEPT_INVITE_ON":
                    if (Util.GetInt32(Get_String(ref line)) == 1)
                        Globals.gamedata.botoptions.AcceptParty = 1;
                    else
                        Globals.gamedata.botoptions.AcceptParty = 0;
                    break;
                case "ACCEPT_REZ":
                    Globals.gamedata.botoptions.AcceptRezNames = Get_String(ref line);
                    break;
                case "ACCEPT_REZ_ON":
                    if (Util.GetInt32(Get_String(ref line)) == 1)
                        Globals.gamedata.botoptions.AcceptRez = 1;
                    else
                        Globals.gamedata.botoptions.AcceptRez = 0;
                    break;
                case "HEAL_RANGE":
                    Globals.gamedata.botoptions.HealRange = Util.GetInt32(Get_String(ref line));
                    break;
                case "LOOT_RANGE":
                    Globals.gamedata.botoptions.LootRange = Util.GetInt32(Get_String(ref line));
                    break;
                case "ACTIVE_TARGET_ON":
                    if (Util.GetInt32(Get_String(ref line)) == 1)
                        Globals.gamedata.botoptions.Target = 1;
                    else
                        Globals.gamedata.botoptions.Target = 0;
                    break;
                case "ACTIVE_ATTACK_ON":
                    if (Util.GetInt32(Get_String(ref line)) == 1)
                        Globals.gamedata.botoptions.Attack = 1;
                    else
                        Globals.gamedata.botoptions.Attack = 0;
                    break;
                case "PICKUP_ON":
                    if (Util.GetInt32(Get_String(ref line)) == 1)
                        Globals.gamedata.botoptions.Pickup = 1;
                    else
                        Globals.gamedata.botoptions.Pickup = 0;
                    break;
                case "AUTO_UNSTUCK_ON":
                    if (Util.GetInt32(Get_String(ref line)) == 1)
                        Globals.gamedata.botoptions.StuckCheck = 1;
                    else
                        Globals.gamedata.botoptions.StuckCheck = 0;
                    break;
                case "MOVE_BEFORE_ATTACK_ON":
                    if (Util.GetInt32(Get_String(ref line)) == 1)
                        Globals.gamedata.botoptions.MoveFirstNormal = 1;
                    else
                        Globals.gamedata.botoptions.MoveFirstNormal = 0;
                    break;
                case "AUTO_BLACKLIST_ON":
                    if (Util.GetInt32(Get_String(ref line)) == 1)
                        Globals.gamedata.botoptions.AutoBlacklist = 1;
                    else
                        Globals.gamedata.botoptions.AutoBlacklist = 0;
                    break;
                case "MOVE_SMART_BEFORE_ATTACK_ON":
                    if (Util.GetInt32(Get_String(ref line)) == 1)
                        Globals.gamedata.botoptions.MoveFirst = 1;
                    else
                        Globals.gamedata.botoptions.MoveFirst = 0;
                    break;
                default:
                    ScriptEngine.Script_Error("bot setting does not exist : " + setting);
                    break;
            }
        }

        private void Script_PLAYALARM()
        {
            VoicePlayer.PlayAlarm();
        }

        private void Script_LOGOUT()
        {
            ServerPackets.Send_Logout();
        }

        private void Script_RESTART()
        {
            ServerPackets.Send_Restart();
        }

        private void Script_SAY_TO_CLIENT(string line)
        {
            string param1 = Get_String(ref line);
            string param2 = Get_String(ref line);
            Util.SayToClient(Util.GetInt32(param1), param2);
        }

        private void Script_SAY_TEXT(string line)
        {
            string param1 = Get_String(ref line);
            string param2 = Get_String(ref line);
            string param3 = Get_String(ref line);
            ServerPackets.Send_Text(Util.GetUInt32(param1), param2, param3);
        }

        private void Script_TAP_TO(string line)
        {
            string svar = Get_String(ref line);

            ScriptVariable ret = Get_Var(svar);

            ServerPackets.Return(System.Convert.ToInt32(ret.Value));
        }

        private void Script_INJECTBB(string line)
        {
            string svar = Get_String(ref line);

            ScriptVariable bbuff = Get_Var(svar);

            ByteBuffer temp = new ByteBuffer((ByteBuffer)bbuff.Value);

            Globals.gamedata.SendToGameServerInject(temp);
        }

        private void Script_INJECT(string line)
        {
            string hex = Get_String(ref line);

            ByteBuffer send = new ByteBuffer();

            hex = hex.Replace(" ", "");
            string sm;

            for (int i = 0; i < hex.Length; i += 2)
            {
                //make the buffer larger if we need to
                if (i / 2 >= send.Length())
                {
                    send.Resize(send.Length() * 2);
                }

                sm = (hex[i].ToString()) + (hex[i + 1].ToString());

                send.WriteByte(byte.Parse(sm, System.Globalization.NumberStyles.HexNumber));
            }

            send.TrimToIndex();

            Globals.gamedata.SendToGameServerInject(send);
        }

        private void Script_INJECTBB_CLIENT(string line)
        {
            string svar = Get_String(ref line);

            ScriptVariable bbuff = Get_Var(svar);

            ByteBuffer temp = new ByteBuffer((ByteBuffer)bbuff.Value);

            Globals.gamedata.SendToClient(temp);
        }

        private void Script_INJECT_CLIENT(string line)
        {
            string hex = Get_String(ref line);

            ByteBuffer send = new ByteBuffer();

            hex = hex.Replace(" ", "");
            string sm;

            for (int i = 0; i < hex.Length; i += 2)
            {
                //make the buffer larger if we need to
                if (i / 2 >= send.Length())
                {
                    send.Resize(send.Length() * 2);
                }

                sm = (hex[i].ToString()) + (hex[i + 1].ToString());

                send.WriteByte(byte.Parse(sm, System.Globalization.NumberStyles.HexNumber));
            }

            send.TrimToIndex();

            Globals.gamedata.SendToClient(send);
        }

        private void Script_COMMAND(string line)
        {
            string cmd = Get_String(ref line);

            ServerPackets.Run_Command(cmd);
        }

        public void Script_IGNORE_ITEM(string line)
        {
            string param1 = Get_String(ref line);
            Util.IgnoreItem(Util.GetUInt32(param1), true);
        }

        public void Script_CLICK_NEAREST_ITEM()
        {
            uint t_id = 0;
            float mx = Globals.gamedata.my_char.X;
            float my = Globals.gamedata.my_char.Y;
            float mz = Globals.gamedata.my_char.Z;
            float dist = float.MaxValue;
            float ndist;
            float x = 0, y = 0, z = 0;

            Globals.DoNotItemLock.EnterReadLock();
            Globals.ItemLock.EnterReadLock();
            try
            {
                foreach (ItemInfo item in Globals.gamedata.nearby_items.Values)
                {
                    if (Globals.gamedata.botoptions.PickOnlyItemsInList == 1)
                    {

                        if (!Globals.gamedata.Paths.IsPointInside(Util.Float_Int32(item.X), Util.Float_Int32(item.Y)) || (!BotOptions.DoNotItems.Contains(item.ItemID)) || 
                            item.Ignore || (Globals.gamedata.botoptions.IgnoreItems == 1 && Util.GetItemName(item.ItemID) == Globals.UnknownItem) || 
                            (Globals.gamedata.botoptions.IgnoreMeshlessItems == 1 && !item.HasMesh))
                        {
                            //break;
                        }
                        else
                        {

                            ndist = System.Convert.ToSingle(System.Math.Sqrt(System.Math.Pow(mx - item.X, 2) + System.Math.Pow(my - item.Y, 2) + System.Math.Pow(mz - item.Z, 2)));

                            if (ndist <= Globals.gamedata.botoptions.LootRange)
                            {
                                if (ndist < dist)
                                {
                                    t_id = item.ID;
                                    x = item.X;
                                    y = item.Y;
                                    z = item.Z;
                                    dist = ndist;
                                }
                            }
                        }
                    }
                    else
                    {

                        if (!Globals.gamedata.Paths.IsPointInside(Util.Float_Int32(item.X), Util.Float_Int32(item.Y)) || item.Ignore || BotOptions.DoNotItems.Contains(item.ItemID) || 
                            (Globals.gamedata.botoptions.IgnoreItems == 1 && Util.GetItemName(item.ItemID) == Globals.UnknownItem) || 
                            (Globals.gamedata.botoptions.IgnoreMeshlessItems == 1 && !item.HasMesh))
                        {
                            //break;
                        }
                        else
                        {

                            ndist = System.Convert.ToSingle(System.Math.Sqrt(System.Math.Pow(mx - item.X, 2) + System.Math.Pow(my - item.Y, 2) + System.Math.Pow(mz - item.Z, 2)));
                            if (ndist <= Globals.gamedata.botoptions.LootRange)
                            {
                                if (ndist < dist)
                                {
                                    t_id = item.ID;
                                    x = item.X;
                                    y = item.Y;
                                    z = item.Z;
                                    dist = ndist;
                                }
                            }
                        }
                    }
                }
            }//unlock
            finally
            {
                Globals.ItemLock.ExitReadLock();
                Globals.DoNotItemLock.ExitReadLock();
            }

            if (t_id != 0)
            {
                ServerPackets.ClickItem(t_id, Util.Float_Int32(x), Util.Float_Int32(y), Util.Float_Int32(z), false);
            }
        }

        private void Script_INVEN_GET_ITEMID(string line)
        {
            string var = Get_String(ref line);
            string s_id = Get_String(ref line);
            uint _id = Util.GetUInt32(s_id);

            ScriptVariable v_str = Get_Var(var);
            v_str.Type = Var_Types.INT;
            v_str.Value = (long)Util.GetInventoryItemID(_id);
        }

        private void Script_INVEN_GET_UID(string line)
        {
            string var = Get_String(ref line);
            string s_id = Get_String(ref line);
            uint _id = Util.GetUInt32(s_id);

            ScriptVariable v_str = Get_Var(var);
            v_str.Type = Var_Types.INT;
            v_str.Value = (long)Util.GetInventoryUID(_id);
        }

        private void Script_ITEM_GET_ID(string line)
        {
            string var = Get_String(ref line);
            string s_name = Get_String(ref line);

            ScriptVariable v_str = Get_Var(var);

            v_str.Type = Var_Types.INT;
            v_str.Value = (long)Util.GetItemID(s_name);
        }

        private void Script_ITEM_GET_NAME(string line)
        {
            string var = Get_String(ref line);
            string s_id = Get_String(ref line);
            uint _id = Util.GetUInt32(s_id);

            ScriptVariable v_str = Get_Var(var);
            v_str.Type = Var_Types.STRING;
            v_str.Value = Util.GetItemName(_id);
        }

        private void Script_ITEM_GET_DESC(string line)
        {
            string var = Get_String(ref line);
            string s_id = Get_String(ref line);
            uint _id = Util.GetUInt32(s_id);

            ScriptVariable v_str = Get_Var(var);
            v_str.Type = Var_Types.STRING;
            v_str.Value = Util.GetItemDescription(_id);
        }

        private void Script_CLAN_GET_ID(string line)
        {
            string var = Get_String(ref line);
            string s_name = Get_String(ref line).ToUpperInvariant();

            ScriptVariable v_str = Get_Var(var);

            Globals.ClanListLock.EnterReadLock();
            try
            {
                foreach (Clan_Info clan_info in Globals.clanlist.Values)
                {
                    if (System.String.Equals(clan_info.ClanName.ToUpperInvariant(), s_name))
                    {
                        v_str.Type = Var_Types.INT;
                        v_str.Value = (long)clan_info.ID;
                    }
                }
            }
            finally
            {
                Globals.ClanListLock.ExitReadLock();
            }

            v_str.Type = Var_Types.INT;
            v_str.Value = (long)0;
        }

        private void Script_CLAN_GET_NAME(string line)
        {
            string var = Get_String(ref line);
            string s_id = Get_String(ref line);
            uint _id = Util.GetUInt32(s_id);

            ScriptVariable v_str = Get_Var(var);

            Globals.ClanListLock.EnterReadLock();
            try
            {
                if (Globals.clanlist.ContainsKey(_id))
                {
                    v_str.Type = Var_Types.STRING;
                    v_str.Value = ((Clan_Info)Globals.clanlist[_id]).ClanName;
                }
                else
                {
                    v_str.Type = Var_Types.STRING;
                    v_str.Value = "~unknown clan~";
                }
            }
            finally
            {
                Globals.ClanListLock.ExitReadLock();
            }
        }

        private void Script_NPC_GET_ID(string line)
        {
            string var = Get_String(ref line);
            string s_name = Get_String(ref line);

            ScriptVariable v_str = Get_Var(var);

            v_str.Type = Var_Types.INT;
            v_str.Value = (long)Util.GetNPCID(s_name);
        }

        private void Script_NPC_GET_NAME(string line)
        {
            string var = Get_String(ref line);
            string s_id = Get_String(ref line);
            uint _id = Util.GetUInt32(s_id);

            ScriptVariable v_str = Get_Var(var);
            v_str.Type = Var_Types.STRING;
            v_str.Value = Util.GetNPCName(_id + Globals.NPC_OFF);
        }

        private void Script_SKILL_GET_ID(string line)
        {
            string var = Get_String(ref line);
            string s_name = Get_String(ref line);

            ScriptVariable v_str = Get_Var(var);

            v_str.Type = Var_Types.INT;
            v_str.Value = (long)Util.GetSkillID(s_name);
        }

        private void Script_SKILL_GET_REUSE(string line)
        {
            string var = Get_String(ref line);
            string s_id = Get_String(ref line);

            uint _id = Util.GetUInt32(s_id);

            long rtime = 0;

            Globals.SkillListLock.EnterReadLock();
            try
            {
                if (Globals.gamedata.skills.ContainsKey(_id))
                {
                    UserSkill us = Util.GetSkill(_id);

                    if (us.IsReady())
                    {
                        rtime = 0;
                    }
                    else
                    {
                        rtime = (us.NextTime.Ticks - System.DateTime.Now.Ticks) / System.TimeSpan.TicksPerMillisecond;
                    }
                }
            }
            finally
            {
                Globals.SkillListLock.ExitReadLock();
            }

            ScriptVariable v_str = Get_Var(var);
            v_str.Type = Var_Types.INT;
            v_str.Value = rtime;
        }

        private void Script_SKILL_GET_NAME(string line)
        {
            string var = Get_String(ref line);
            string s_id = Get_String(ref line);
            string s_level = Get_String(ref line);
            uint _id = Util.GetUInt32(s_id);
            uint _level = Util.GetUInt32(s_level);

            ScriptVariable v_str = Get_Var(var);
            v_str.Type = Var_Types.STRING;
            v_str.Value = Util.GetSkillName(_id, _level);
        }

        private void Script_SKILL_GET_DESC1(string line)
        {
            Script_SKILL_GET_DESC(line, 1);
        }

        private void Script_SKILL_GET_DESC2(string line)
        {
            Script_SKILL_GET_DESC(line, 2);
        }

        private void Script_SKILL_GET_DESC3(string line)
        {
            Script_SKILL_GET_DESC(line, 3);
        }

        private void Script_SKILL_GET_DESC(string line, int num)
        {
            string var = Get_String(ref line);
            string s_id = Get_String(ref line);
            string s_level = Get_String(ref line);
            uint _id = Util.GetUInt32(s_id);
            uint _level = Util.GetUInt32(s_level);

            ScriptVariable v_str = Get_Var(var);
            v_str.Type = Var_Types.STRING;
            v_str.Value = Util.GetSkillDesc(_id, _level, num);
        }

        private void Script_CHAR_GET_ID(string line)
        {
            string var = Get_String(ref line);
            string s_name = Get_String(ref line);

            ScriptVariable v_str = Get_Var(var);

            v_str.Type = Var_Types.INT;
            v_str.Value = (long)Util.GetCharID(s_name);
        }

        private void Script_CHAR_GET_NAME(string line)
        {
            string var = Get_String(ref line);
            string s_id = Get_String(ref line);
            uint _id = Util.GetUInt32(s_id);

            ScriptVariable v_str = Get_Var(var);
            v_str.Type = Var_Types.STRING;
            v_str.Value = Util.GetCharName(_id);
        }

        private void Script_ADD_WALL(string line)
        {
            string s_x1 = Get_String(ref line);
            string s_y1 = Get_String(ref line);
            string s_x2 = Get_String(ref line);
            string s_y2 = Get_String(ref line);

            int i_x1 = Util.GetInt32(s_x1);
            int i_y1 = Util.GetInt32(s_y1);
            int i_x2 = Util.GetInt32(s_x2);
            int i_y2 = Util.GetInt32(s_y2);

            Point npt1 = new Point();
            npt1.X = i_x1;
            npt1.Y = i_y1;
            npt1.Z = 0;

            Point npt2 = new Point();
            npt2.X = i_x2;
            npt2.Y = i_y2;
            npt2.Z = 0;

            Wall wall = new Wall();
            wall.P1 = npt1;
            wall.P2 = npt2;

            Globals.gamedata.Walls.Add(wall);
        }

        private void Script_ADD_PATH_PT(string line)
        {
            string s_x = Get_String(ref line);
            string s_y = Get_String(ref line);

            int i_x = Util.GetInt32(s_x);
            int i_y = Util.GetInt32(s_y);

            Point npt = new Point();
            npt.X = i_x;
            npt.Y = i_y;
            npt.Z = 0;

            Globals.gamedata.Paths.PointList.Add(npt);
        }

        private void Script_NPC_DIALOG(string line)
        {
            string say = Get_String(ref line);

            ServerPackets.NPC_Chat_Click(say);
        }

        private void Script_CHECK_TARGETING(string line)
        {
            string var = Get_String(ref line);
            string s_id = Get_String(ref line);
            string attrib = Get_String(ref line).ToUpperInvariant();
            uint _id = Util.GetUInt32(s_id);

            ScriptVariable v_str = Get_Var(var);

            long val = -1;

            TargetType type = Util.GetType(_id);

            switch(type)
            {
                case TargetType.NPC:

                    NPCInfo npc = Util.GetNPC(_id);
                    
                    switch (attrib)
                    {
                        case "TYPE":
                            val = 0;
                            break;
                        case "ATTACKABLE":
                            if (npc != null)
                            {
                                if (npc.isAttackable == 0)
                                    val = 0;
                                else
                                    val = 1;
                            }
                            break;
                        case "ALIVE":
                            if (npc != null)
                            {
                                if (npc.isAlikeDead == 0)
                                    val = 0;
                                else
                                    val = 1;
                            }
                            break;
                        case "INBOX":
                            if (npc != null)
                            {
                                if ( Globals.gamedata.Paths.IsPointInside(Util.Float_Int32(npc.X), Util.Float_Int32(npc.Y)) )
                                    val = 0;
                                else
                                    val = 1;
                            }
                            break;
                        case "COMBAT":
                            if (npc != null)
                            {
                                if (npc.CheckCombat())
                                    val = 1;
                                else
                                    val = 0;
                            }
                            break;
                        case "TARGETTED":
                            break;
                        case "TARGETING":
                            break;
                        case "IGNORE":
                            break;
                        case "ZRANGE":
                            if (npc != null)
                            {
                                if (Math.Abs(Globals.gamedata.my_char.Z - npc.Z) <= BotOptions.Target_ZRANGE)
                                    val = 0;
                                else
                                    val = 1;
                            }
                            break;
                        case "PATHFINDING":
                            if (npc != null)
                            {
                                if (Globals.gamedata.pathManager.runASTAR(npc.X, npc.Y))
                                {
                                    val = 1;
                                }
                                else
                                {
                                    val = 0;
                                }
                            }
                            break;
                        default:
                            //lol?
                            ScriptEngine.Script_Error("INVALID TARGET PARAMETER - CHECK");
                            break;
                    }
                    break;
                case TargetType.PLAYER:
                    CharInfo pla = Util.GetChar(_id);

                    switch (attrib)
                    {
                        case "TYPE":
                            val = 0;
                            break;
                        case "ATTACKABLE":
                            if (pla != null)
                            {
                                if (pla.PvPFlag == 0)
                                    val = 0;
                                else
                                    val = 1;
                            }
                            break;
                        case "ALIVE":
                            if (pla != null)
                            {
                                if (pla.isAlikeDead == 0)
                                    val = 0;
                                else
                                    val = 1;
                            }
                            break;
                        case "INBOX":
                            if (pla != null)
                            {
                                if (Globals.gamedata.Paths.IsPointInside(Util.Float_Int32(pla.X), Util.Float_Int32(pla.Y)))
                                    val = 0;
                                else
                                    val = 1;
                            }
                            break;
                        case "COMBAT":
                            if (pla != null)
                            {
                                if (pla.CheckCombat())
                                    val = 1;
                                else
                                    val = 0;
                            }
                            break;
                        case "TARGETTED":
                            break;
                        case "TARGETING":
                            break;
                        case "IGNORE":
                            break;
                        case "ZRANGE":
                            if (pla != null)
                            {
                                if (Math.Abs(Globals.gamedata.my_char.Z - pla.Z) <= BotOptions.Target_ZRANGE)
                                    val = 0;
                                else
                                    val = 1;
                            }
                            break;
                        case "PATHFINDING":
                            if (pla != null)
                            {
                                if (Globals.gamedata.pathManager.runASTAR(pla.X, pla.Y))
                                {
                                    val = 1;
                                }
                                else
                                {
                                    val = 0;
                                }
                            }
                            break;
                        default:
                            //lol?
                            ScriptEngine.Script_Error("INVALID TARGET PARAMETER - CHECK");
                            break;
                    }
                    break;
                case TargetType.SELF:
                    break;
                case TargetType.MYPET:
                    break;
                case TargetType.MYPET1:
                    break;
                case TargetType.MYPET2:
                    break;
                case TargetType.MYPET3:
                    break;
            }

            v_str.Type = Var_Types.INT;
            v_str.Value = val;

        }

        private void Script_SET_TARGETING(string line)
        {
            string attrib = Get_String(ref line).ToUpperInvariant();
            string str_var_val = Get_String(ref line);

            ScriptVariable var_val = Get_Var(str_var_val);

            switch (attrib)
            {
                case "TYPE":
                    //0 = npcs
                    //1 = players
                    //2 = both
                    BotOptions.Target_TYPE = System.Convert.ToInt32(var_val.Value);
                    break;
                case "ATTACKABLE":
                    //0 = attackable
                    //1 = invincible
                    //2 = both
                    BotOptions.Target_ATTACKABLE = System.Convert.ToInt32(var_val.Value);
                    break;
                case "ALIVE":
                    //0 = alive
                    //1 = dead
                    //2 = both
                    BotOptions.Target_ALIVE = System.Convert.ToInt32(var_val.Value);
                    break;
                case "INBOX":
                    //0 = in box
                    //1 = outside box
                    //2 = both
                    BotOptions.Target_INBOX = System.Convert.ToInt32(var_val.Value);
                    break;
                case "COMBAT":
                    //0 = not being attack
                    //1 = under attack
                    //2 = both
                    BotOptions.Target_COMBAT = System.Convert.ToInt32(var_val.Value);
                    break;
                case "TARGETTED":
                    //0 = not targeted
                    //1 = targeted by someone
                    //2 = both
                    BotOptions.Target_TARGETTED = System.Convert.ToInt32(var_val.Value);
                    break;
                case "TARGETING":
                    //0 = targeting nobody
                    //1 = targeting something
                    //2 = both
                    BotOptions.Target_TARGETING = System.Convert.ToInt32(var_val.Value);
                    break;
                case "IGNORE":
                    //0 = not on the ignore list
                    //1 = on the ignore list
                    //2 = both
                    BotOptions.Target_IGNORE = System.Convert.ToInt32(var_val.Value);
                    break;
                case "ZRANGE":
                    BotOptions.Target_ZRANGE = System.Convert.ToInt32(var_val.Value);
                    break;
                case "PATHFINDING":
                    BotOptions.Target_Pathfinding = System.Convert.ToInt32(var_val.Value);
                    break;

                default:
                    //lol?
                    ScriptEngine.Script_Error("INVALID TARGET PARAMETER - SET");
                    break;
            }
        }

        private void Script_TARGET_NEAREST()
        {
            Script_TARGET_NEAREST_Internal();
        }
        public uint Script_TARGET_NEAREST_Internal()
        {
            uint t_id = Script_TARGET_NEAREST_Internal(0);
            return t_id;
        }

        public uint Script_TARGET_NEAREST_Internal(uint IgnID, bool DoTarget = true)
        {
            //lets use the parameters provided for this
            //GamePackets.ClickNearest(Script_Target_TYPE, Script_Target_ATTACKABLE, Script_Target_ALIVE, Script_Target_INBOX, Script_Target_COMBAT, Script_Target_TARGETTED, Script_Target_TARGETING, Script_Target_IGNORE, Script_Target_ZRANGE);

            uint t_id = 0;
            float mx = Globals.gamedata.my_char.X;
            float my = Globals.gamedata.my_char.Y;
            float mz = Globals.gamedata.my_char.Z;
            uint char_target = Globals.gamedata.my_char.TargetID;
            float dist = float.MaxValue;
            float ndist;
            float x = 0, y = 0, z = 0;
            bool attackingself = false;
            bool issummon = false;
            //bool attackedbyothers = false;

            //uint t_id_protect = 0;
            //float dist_protect = float.MaxValue;
            //float x_protect = 0, y_protect = 0, z_protect = 0;

            if (BotOptions.Target_TYPE == 0 || BotOptions.Target_TYPE == 2)
            {
                Globals.DoNotNPCLock.EnterReadLock();
                Globals.NPCLock.EnterReadLock();
                //Globals.PlayerLock.EnterReadLock();
                try
                {
                    foreach (NPCInfo npc in Globals.gamedata.nearby_npcs.Values)
                    {
                        //See if the npc is attacking us or a party member.
                        //TargetType type = Util.GetType(npc.ID);
                        //if ((type != TargetType.SUMMON) && (type != TargetType.MYPET))
                        foreach (uint smn in Globals.gamedata.SummonIDs)
                        {
                            if (smn == npc.NPCID)
                            {
                                issummon = true;
                            }
                        }
                        
                        //if the npc is close enough... no need to retarget
                        if (npc.NPCID == char_target)
                        {
                            ndist = System.Convert.ToSingle(System.Math.Sqrt(System.Math.Pow(mx - npc.X, 2) + System.Math.Pow(my - npc.Y, 2) + System.Math.Pow(mz - npc.Z, 2)));

                            if (ndist < 100)
                            {
                                t_id = npc.ID;
                                x = npc.X;
                                y = npc.Y;
                                z = npc.Z;
                                dist = 0;
                            }
                        }
                        if (!issummon && dist != 0)
                        {
                            if (((Globals.gamedata.my_char.ID == npc.TargetID) || Globals.gamedata.PartyMembers.ContainsKey(npc.TargetID) || ((Globals.gamedata.my_pet.ID == npc.TargetID) && (Globals.gamedata.my_pet.ID != 0))) && (npc.SummonedNameColor <= 0))
                            {
                                attackingself = true;
                            }
                            else
                            {
                                attackingself = false;
                            }

                            if (Globals.gamedata.botoptions.AttackOnlyMobsInList == 1)
                            {
                                if (((BotOptions.DoNotNPCs.Contains(npc.NPCID) || attackingself) && npc.isInvisible == 0) && (!npc.Ignore || attackingself) && (npc.isAttackable == 1 && npc.showName == 1 && npc.isTargetable == 1))
                                {
                                    if (MeetsConditions(npc)) //npc.TargetID == Globals.gamedata.my_char.ID || 
                                    {
                                        if (attackingself)
                                        {
                                            t_id = npc.ID;
                                            dist = 0;
                                        }
                                        else
                                        {
                                            ndist = System.Convert.ToSingle(System.Math.Sqrt(System.Math.Pow(mx - npc.X, 2) + System.Math.Pow(my - npc.Y, 2) + System.Math.Pow(mz - npc.Z, 2)));

                                            if (ndist < dist)
                                            {
                                                t_id = npc.ID;
                                                x = npc.X;
                                                y = npc.Y;
                                                z = npc.Z;
                                                dist = ndist;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                //Globals.l2net_home.Add_Error("attackingself " + attackingself + " BotOptions.DoNotNPCs.Contains(npc.NPCID) " + BotOptions.DoNotNPCs.Contains(npc.NPCID) + " npc.Ignore " + npc.Ignore + " npc.isInvisible " + npc.isInvisible + " npc.isTargetable " + npc.isTargetable + " npc.isAttackable " + npc.isAttackable);

                                if (!attackingself && (BotOptions.DoNotNPCs.Contains(npc.NPCID) || npc.Ignore || npc.isInvisible == 1 || npc.showName == 0 || npc.isTargetable == 0 || npc.ID == IgnID || npc.isAttackable == 0 || (npc.SummonedNameColor > 0)))
                                {
                                    //break;
                                }
                                else
                                {
                               
                                    /*if (Globals.gamedata.botoptions.ProtectPriority == 1 && npc.PriorityTarget())
                                    {
                                        ndist = System.Convert.ToSingle(System.Math.Sqrt(System.Math.Pow(mx - npc.X, 2) + System.Math.Pow(my - npc.Y, 2) + System.Math.Pow(mz - npc.Z, 2)));

                                        if (ndist < dist_protect)
                                        {
                                            t_id_protect = npc.ID;
                                            x_protect = npc.X;
                                            y_protect = npc.Y;
                                            z_protect = npc.Z;
                                            dist_protect = ndist;
                                        }
                                    }*/

                                    if (MeetsConditions(npc)) //|| npc.TargetID == Globals.gamedata.my_char.ID <- Now it will only target mobs in box, even if mobs outside are targeting you
                                    {
                                        if (attackingself)
                                        {
                                            t_id = npc.ID;
                                            dist = 0;
                                        }
                                        else
                                        {
                                            ndist = System.Convert.ToSingle(System.Math.Sqrt(System.Math.Pow(mx - npc.X, 2) + System.Math.Pow(my - npc.Y, 2) + System.Math.Pow(mz - npc.Z, 2)));

                                            if (ndist < dist)
                                            {
                                                t_id = npc.ID;
                                                x = npc.X;
                                                y = npc.Y;
                                                z = npc.Z;
                                                dist = ndist;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        attackingself = false;
                        issummon = false;
                    }
                }//unlock
                finally
                {
                    Globals.NPCLock.ExitReadLock();
                    Globals.DoNotNPCLock.ExitReadLock();
                    //Globals.PlayerLock.ExitReadLock();
                }
            }

            if (BotOptions.Target_TYPE == 1 || BotOptions.Target_TYPE == 2)
            {
                Globals.PlayerLock.EnterReadLock();
                try
                {
                    foreach (CharInfo player in Globals.gamedata.nearby_chars.Values)
                    {
                        if (MeetsConditions(player))
                        {
                            ndist = System.Convert.ToSingle(System.Math.Sqrt(System.Math.Pow(mx - player.X, 2) + System.Math.Pow(my - player.Y, 2) + System.Math.Pow(mz - player.Z, 2)));

                            if (ndist < dist)
                            {
                                t_id = player.ID;
                                x = player.X;
                                y = player.Y;
                                z = player.Z;
                                dist = ndist;
                            }
                        }
                    }
                }//unlcok
                finally
                {
                    Globals.PlayerLock.ExitReadLock();
                }
            }

            /*if (t_id_protect != 0)
            {
                ServerPackets.ClickChar(t_id_protect, Util.Float_Int32(x_protect), Util.Float_Int32(y_protect), Util.Float_Int32(z_protect), false, false);
            }
            else*/

            if (Globals.gamedata.botoptions.MoveBeforeTargeting == 0 && DoTarget)
            {
                if (t_id != 0)
                {
                    ServerPackets.ClickChar(t_id, Util.Float_Int32(x), Util.Float_Int32(y), Util.Float_Int32(z), false, false);
                }
            }

            return t_id;
        }

        private void Script_TARGET_NEAREST_NAME(string line)
        {
            string s_name = Get_String(ref line);

            uint t_id = 0;
            float mx = Globals.gamedata.my_char.X;
            float my = Globals.gamedata.my_char.Y;
            float mz = Globals.gamedata.my_char.Z;
            float dist = float.MaxValue;
            float ndist;
            float x = 0, y = 0, z = 0;

            //uint t_id_protect = 0;
            //float dist_protect = float.MaxValue;
            //float x_protect = 0, y_protect = 0, z_protect = 0;

            if (BotOptions.Target_TYPE == 0 || BotOptions.Target_TYPE == 2)
            {
                Globals.DoNotNPCLock.EnterReadLock();
                Globals.NPCLock.EnterReadLock();
                try
                {
                    foreach (NPCInfo npc in Globals.gamedata.nearby_npcs.Values)
                    {
                        if (BotOptions.DoNotNPCs.Contains(npc.NPCID))
                        {
                            //break;
                        }
                        else
                        {
                            if (System.String.Equals(npc.Name, s_name))
                            {
                                /*if (Globals.gamedata.botoptions.ProtectPriority == 1 && npc.PriorityTarget())
                                {
                                    ndist = System.Convert.ToSingle(System.Math.Sqrt(System.Math.Pow(mx - npc.X, 2) + System.Math.Pow(my - npc.Y, 2) + System.Math.Pow(mz - npc.Z, 2)));

                                    if (ndist < dist_protect)
                                    {
                                        t_id_protect = npc.ID;
                                        x_protect = npc.X;
                                        y_protect = npc.Y;
                                        z_protect = npc.Z;
                                        dist_protect = ndist;
                                    }
                                }*/

                                if ((npc.TargetID == Globals.gamedata.my_char.ID || MeetsConditions(npc) && npc.isInvisible == 0) && (!npc.Ignore) && (npc.showName == 1 && npc.isTargetable == 1))
                                {
                                    ndist = System.Convert.ToSingle(System.Math.Sqrt(System.Math.Pow(mx - npc.X, 2) + System.Math.Pow(my - npc.Y, 2) + System.Math.Pow(mz - npc.Z, 2)));

                                    if (ndist < dist)
                                    {
                                        t_id = npc.ID;
                                        x = npc.X;
                                        y = npc.Y;
                                        z = npc.Z;
                                        dist = ndist;
                                    }
                                }
                            }
                        }
                    }
                }//unlock
                finally
                {
                    Globals.NPCLock.ExitReadLock();
                    Globals.DoNotNPCLock.ExitReadLock();
                }
            }

            if (BotOptions.Target_TYPE == 1 || BotOptions.Target_TYPE == 2)
            {
                Globals.PlayerLock.EnterReadLock();
                try
                {
                    foreach (CharInfo player in Globals.gamedata.nearby_chars.Values)
                    {
                        if (System.String.Equals(player.Name, s_name) && MeetsConditions(player))
                        {
                            ndist = System.Convert.ToSingle(System.Math.Sqrt(System.Math.Pow(mx - player.X, 2) + System.Math.Pow(my - player.Y, 2) + System.Math.Pow(mz - player.Z, 2)));

                            if (ndist < dist)
                            {
                                t_id = player.ID;
                                x = player.X;
                                y = player.Y;
                                z = player.Z;
                                dist = ndist;
                            }
                        }
                    }
                }//unlcok
                finally
                {
                    Globals.PlayerLock.ExitReadLock();
                }
            }

            /*if (t_id_protect != 0)
            {
                ServerPackets.ClickChar(t_id_protect, Util.Float_Int32(x_protect), Util.Float_Int32(y_protect), Util.Float_Int32(z_protect), false, false);
            }
            else*/ if (t_id != 0)
            {
                ServerPackets.ClickChar(t_id, Util.Float_Int32(x), Util.Float_Int32(y), Util.Float_Int32(z), false, false);
            }
        }

        private void Script_TARGET_NEAREST_ID(string line)
        {
            string s_id = Get_String(ref line);

            uint id = Util.GetUInt32(s_id);

            uint t_id = 0;
            float mx = Globals.gamedata.my_char.X;
            float my = Globals.gamedata.my_char.Y;
            float mz = Globals.gamedata.my_char.Z;
            float dist = float.MaxValue;
            float ndist;
            float x = 0, y = 0, z = 0;

			//Globals.l2net_home.Add_Debug("Entered target nearest id");
            //uint t_id_protect = 0;
            //float dist_protect = float.MaxValue;
            //float x_protect = 0, y_protect = 0, z_protect = 0;

            if (BotOptions.Target_TYPE == 0 || BotOptions.Target_TYPE == 2)
            {
                Globals.DoNotNPCLock.EnterReadLock();
                Globals.NPCLock.EnterReadLock();
                try
                {
                    foreach (NPCInfo npc in Globals.gamedata.nearby_npcs.Values)
                    {
                        if (BotOptions.DoNotNPCs.Contains(npc.NPCID))
                        {
                            //break;
                        }
                        else
                        {
                            if (npc.NPCID == id)
                            {
                                /*if (Globals.gamedata.botoptions.ProtectPriority == 1 && npc.PriorityTarget())
                                {
                                    ndist = System.Convert.ToSingle(System.Math.Sqrt(System.Math.Pow(mx - npc.X, 2) + System.Math.Pow(my - npc.Y, 2) + System.Math.Pow(mz - npc.Z, 2)));

                                    if (ndist < dist_protect)
                                    {
                                        t_id_protect = npc.ID;
                                        x_protect = npc.X;
                                        y_protect = npc.Y;
                                        z_protect = npc.Z;
                                        dist_protect = ndist;
                                    }
                                }*/

                                if (npc.TargetID == Globals.gamedata.my_char.ID || MeetsConditions(npc))
                                {
                                    ndist = System.Convert.ToSingle(System.Math.Sqrt(System.Math.Pow(mx - npc.X, 2) + System.Math.Pow(my - npc.Y, 2) + System.Math.Pow(mz - npc.Z, 2)));

                                    if (ndist < dist)
                                    {
                                        t_id = npc.ID;
                                        x = npc.X;
                                        y = npc.Y;
                                        z = npc.Z;
                                        dist = ndist;
                                    }
                                }
                            }
                        }
                    }
                }//unlock
                finally
                {
                    Globals.NPCLock.ExitReadLock();
                    Globals.DoNotNPCLock.ExitReadLock();
                }
            }

            if (BotOptions.Target_TYPE == 1 || BotOptions.Target_TYPE == 2)
            {
                Globals.PlayerLock.EnterReadLock();
                try
                {
                    foreach (CharInfo player in Globals.gamedata.nearby_chars.Values)
                    {
                        if (player.ID == id && MeetsConditions(player))
                        {
                            ndist = System.Convert.ToSingle(System.Math.Sqrt(System.Math.Pow(mx - player.X, 2) + System.Math.Pow(my - player.Y, 2) + System.Math.Pow(mz - player.Z, 2)));

                            if (ndist < dist)
                            {
                                t_id = player.ID;
                                x = player.X;
                                y = player.Y;
                                z = player.Z;
                                dist = ndist;
                            }
                        }
                    }
                }//unlcok
                finally
                {
                    Globals.PlayerLock.ExitReadLock();
                }
            }


            /*if (t_id_protect != 0)
            {
                ServerPackets.ClickChar(t_id_protect, Util.Float_Int32(x_protect), Util.Float_Int32(y_protect), Util.Float_Int32(z_protect), false, false);
            }
            else*/ if (t_id != 0)
            {
                ServerPackets.ClickChar(t_id, Util.Float_Int32(x), Util.Float_Int32(y), Util.Float_Int32(z), false, false);
            }
        }

        private void Script_TARGET(string line)
        {
            string s_id = Get_String(ref line);

            uint id = Util.GetUInt32(s_id);

            ServerPackets.ClickOBJ(id, false, false);
        }

        private bool MeetsConditions(NPCInfo npc)
        {

            if (BotOptions.Target_Pathfinding == 1)
            {
                if (!Globals.gamedata.pathManager.runASTAR(npc.X, npc.Y))
                {
#if DEBUG
                    Globals.l2net_home.Add_Debug("Couldn't target mob because we could not find a path to it =)");
#endif
                    return false;
                }
            }
            if (
                ((BotOptions.Target_ATTACKABLE == 2) || (BotOptions.Target_ATTACKABLE == 0 && npc.isAttackable != 0) || (BotOptions.Target_ATTACKABLE == 1 && npc.isAttackable == 0)) &&
                ((BotOptions.Target_ALIVE == 2) || (BotOptions.Target_ALIVE == 0 && npc.isAlikeDead == 0) || (BotOptions.Target_ALIVE == 1 && npc.isAlikeDead != 0)) &&
                ((BotOptions.Target_INBOX == 2) || (BotOptions.Target_INBOX == 0 && Globals.gamedata.Paths.IsPointInside(Util.Float_Int32(npc.X), Util.Float_Int32(npc.Y))) || (BotOptions.Target_INBOX == 1 && !Globals.gamedata.Paths.IsPointInside(Util.Float_Int32(npc.X), Util.Float_Int32(npc.Y)))) &&
                ((BotOptions.Target_COMBAT == 2) || (BotOptions.Target_COMBAT == 0 && npc.CheckCombat() == false) || (BotOptions.Target_COMBAT == 1 && npc.CheckCombat() == true)) &&
                (Math.Abs(Globals.gamedata.my_char.Z - npc.Z) <= BotOptions.Target_ZRANGE)
                )
            {
                return true;
            }

            return false;
        }

        private bool MeetsConditions(CharInfo player)
        {
            if (
                ((BotOptions.Target_ATTACKABLE == 2) || (BotOptions.Target_ATTACKABLE == 0 && player.Invisible == 0) || (BotOptions.Target_ATTACKABLE == 1 && player.Invisible != 0)) &&
                ((BotOptions.Target_ALIVE == 2) || (BotOptions.Target_ALIVE == 0 && player.isAlikeDead == 0) || (BotOptions.Target_ALIVE == 1 && player.isAlikeDead != 0)) &&
                ((BotOptions.Target_INBOX == 2) || (BotOptions.Target_INBOX == 0 && Globals.gamedata.Paths.IsPointInside(Util.Float_Int32(player.X), Util.Float_Int32(player.Y))) || (BotOptions.Target_INBOX == 1 && !Globals.gamedata.Paths.IsPointInside(Util.Float_Int32(player.X), Util.Float_Int32(player.Y)))) &&
                ((BotOptions.Target_COMBAT == 2) || (BotOptions.Target_COMBAT == 0 && player.CheckCombat() == false) || (BotOptions.Target_COMBAT == 1 && player.CheckCombat() == true)) &&
                (Math.Abs(Globals.gamedata.my_char.Z - player.Z) <= BotOptions.Target_ZRANGE)
                )
            {
                return true;
            }

            return false;
        }

        private void Script_TALK_TARGET()
        {
            ServerPackets.ClickOBJ(Globals.gamedata.my_char.TargetID, false, false);
        }

        public void Script_ATTACK_TARGET()
        {
            if (Globals.gamedata.Control)
            {
                ServerPackets.ClickOBJ(Globals.gamedata.my_char.TargetID, true, false);
            }
            else
            {
                bool atk = true;
                Globals.PartyLock.EnterReadLock();
                try
                {
                    foreach (PartyMember pm in Globals.gamedata.PartyMembers.Values)
                    {
                        if (Globals.gamedata.my_char.TargetID == pm.ID)
                        {
                            atk = false;
                        }
                    }
                }
                finally
                {
                    Globals.PartyLock.ExitReadLock();
                }
                Globals.NPCLock.EnterReadLock();
                try
                {
                    NPCInfo npc = Util.GetNPC(Globals.gamedata.my_char.TargetID);
                    if (npc != null)
                    {
                        if (npc.SummonedNameColor > 0)
                        {
                            atk = false;
                        }
                    }

                }
                finally
                {
                    Globals.NPCLock.ExitReadLock();
                }
                if (atk)
                {
                    ServerPackets.ClickOBJ(Globals.gamedata.my_char.TargetID, false, false);
                }
                atk = true;
            }
        }

        private void Script_USE_ACTION(string line)
        {
            string skill_id = Get_String(ref line);
            string s_control = Get_String(ref line);
            string s_shift = Get_String(ref line);

            uint id = Util.GetUInt32(skill_id);
            uint control = Util.GetUInt32(s_control);
            uint shift = Util.GetUInt32(s_shift);

            ServerPackets.Use_Action_Parse(id, control != 0, shift != 0);
        }

        private void Script_CANCEL_BUFF(string line)
        {
            string skill_id = Get_String(ref line);
            uint id = Util.GetUInt32(skill_id);

            Globals.MyBuffsListLock.EnterWriteLock();
            try
            {
                if (Globals.gamedata.mybuffs.ContainsKey(id))
                {
                    CharBuff cb = (CharBuff)Globals.gamedata.mybuffs[id];
                    ServerPackets.RequestDispel(Globals.gamedata.my_char.ID, cb.ID, cb.SkillLevel);
                }
                else
                {
                    Globals.l2net_home.Add_Error("CANCEL_BUFF " + id + " aborted due to the effect not being present.");
                }
            }
            finally
            {
                Globals.MyBuffsListLock.ExitWriteLock();
            }
        }

        private void Script_USE_SKILL(string line)
        {
            string skill_id = Get_String(ref line);
            string s_control = Get_String(ref line);
            string s_shift = Get_String(ref line);

            uint id = Util.GetUInt32(skill_id);
            uint control = Util.GetUInt32(s_control);
            uint shift = Util.GetUInt32(s_shift);

            ServerPackets.Try_Use_Skill(id, control != 0, shift != 0);
        }

        private void Script_USE_SKILL_SMART(string line)
        {
            string skill_id = Get_String(ref line);
            string s_control = Get_String(ref line);
            string s_shift = Get_String(ref line);

            uint id = Util.GetUInt32(skill_id);
            uint control = Util.GetUInt32(s_control);
            uint shift = Util.GetUInt32(s_shift);

            ServerPackets.Try_Use_Skill_Smart(id, control != 0, shift != 0);
        }

        private void Script_USE_ITEM(string line, bool exp)
        {
            string sitm_id = Get_String(ref line);

            uint itm_id = Util.GetUInt32(sitm_id);

            ServerPackets.Try_Use_Item(itm_id, exp);
        }

        private void Script_ITEM_COUNT(string line)
        {
            string var = Get_String(ref line);
            string itm_id = Get_String(ref line);

            ScriptVariable v_itm = Get_Var(var);
            uint id = Util.GetUInt32(itm_id);

            Globals.InventoryLock.EnterReadLock();
            try
            {
                foreach (InventoryInfo itm in Globals.gamedata.inventory.Values)
                {
                    if (itm.ItemID == id)
                    {
                        //we have the item in our inventory
                        //lets set the quanity
                        if (v_itm.Type == Var_Types.INT)
                        {
                            v_itm.Value = (long)itm.Count;
                        }
                        else if (v_itm.Type == Var_Types.DOUBLE)
                        {
                            v_itm.Value = (double)itm.Count;
                        }
                        else if (v_itm.Type == Var_Types.STRING)
                        {
                            v_itm.Value = itm.Count.ToString();
                        }

                        return;
                    }
                }
            }
            finally
            {
                Globals.InventoryLock.ExitReadLock();
            }

            if (v_itm.Type == Var_Types.INT)
            {
                v_itm.Value = 0L;
            }
            else if (v_itm.Type == Var_Types.DOUBLE)
            {
                v_itm.Value = 0.0D;
            }
            else if (v_itm.Type == Var_Types.STRING)
            {
                v_itm.Value = "none";
            }
        }

        private void Script_MOVE_TO(string line)
        {
            string sx = Get_String(ref line);
            string sy = Get_String(ref line);
            string sz = Get_String(ref line);

            //MOVE_TO 10 10 10
            int x = Util.GetInt32(sx);
            int y = Util.GetInt32(sy);
            int z = Util.GetInt32(sz);

            if (Globals.Script_Debugging)
            {
                Globals.l2net_home.Add_Debug("MOVE_TO " + x.ToString() + "," + y.ToString() + "," + z.ToString());
            }

            ServerPackets.MoveToPacket(x, y, z);
        }

        private void Script_MOVE_WAIT(string line)
        {
            string sx = Get_String(ref line);
            string sy = Get_String(ref line);
            string sz = Get_String(ref line);
            string sdist = Get_String(ref line);

            int x = Util.GetInt32(sx);
            int y = Util.GetInt32(sy);
            int z = Util.GetInt32(sz);
            double dist = Util.GetDouble(sdist);

            if (dist < 5)
            {
                dist = 5;
            }

            try
            {
                Script_MOVE_WAIT_Internal(x, y, z, dist, true);
            }
            catch
            {
                Line_Pos++;
            }
        }

        private void Script_MOVE_WAIT_Internal(int x, int y, int z, double dist, bool inc)
        {
            if (is_Moving)
            {
                if (x == Moving_X && y == Moving_Y && z == Moving_Z)
                {
                    //check if we are close enough to our destination
                    if (Math.Sqrt(Math.Pow(Globals.gamedata.my_char.X - x, 2) +
                        Math.Pow(Globals.gamedata.my_char.Y - y, 2) +
                        Math.Pow(Globals.gamedata.my_char.Z - z, 2)) <= dist)
                    {
                        //we are close enough to our destination
                        is_Moving = false;

                        if (inc)
                        {
                            Line_Pos++;
                        }
                    }
                    else
                    {
                        //are we moving?
                        if (Globals.gamedata.my_char.Moving)
                        {
                            //are we moving to the correct location?
                            if (Globals.gamedata.my_char.Dest_X == x &&
                                Globals.gamedata.my_char.Dest_Y == y &&
                                Globals.gamedata.my_char.Dest_Z == z)
                            {
                                Script_SLEEP(10);
                            }
                            else
                            {
                                //lets get us moving to the correct location
                                ServerPackets.MoveToPacket(x, y, z);

                                Script_SLEEP(100);
                            }
                        }
                        else
                        {
                            //not moving... lets get us moving
                            ServerPackets.MoveToPacket(x, y, z);

                            Script_SLEEP(100);
                        }
                    }
                }
                else
                {
                    //we need to wait for this move to complete before we can move...
                    Script_SLEEP(10);
                }
            }
            else
            {
                Moving_X = x;
                Moving_Y = y;
                Moving_Z = z;

                is_Moving = true;

                ServerPackets.MoveToPacket(x, y, z);

                Script_SLEEP(100);
            }
        }

        private void Script_MOVE_SMART(string line)
        {
            string sx = Get_String(ref line);
            string sy = Get_String(ref line);
            string sz = Get_String(ref line);
           // string sdist = Get_String(ref line);

            int x = Util.GetInt32(sx);
            int y = Util.GetInt32(sy);
            int z = Util.GetInt32(sz);
           // double dist = Util.GetDouble(sdist);

            double dist = 100;

            try
            {
                Script_MOVE_SMART_Internal(x, y, z, dist, true);
            }
            catch(Exception e)
            {
				Globals.l2net_home.Add_Debug("Warning:" + e.Message);
                Globals.l2net_home.Add_Debug(e.StackTrace);
                Line_Pos++;
				is_Moving = false;
            }
        }
        public void Script_MOVE_INTERRUPT()
        {
            if(is_Moving)
                moveSmartInterruptFlag = true;
            Line_Pos++;
        }

        public void Script_MOVE_SMART_Internal(int x, int y, int z, double dist, bool inc)
        {
            int tolerance = 10;
            //we ignore z values in the move routine
            if (moveSmartInterruptFlag == true)
            {
                moveSmartInterruptFlag = false;
                is_Moving = false;
                Line_Pos++;
                return;
            }

            if (is_Moving)
            {
                if (x == Moving_X && y == Moving_Y && z == Moving_Z)
                {
                    // Globals.l2net_home.Add_Debug("moving...");
                    //check if we are close enough to our destination
                    if (Math.Sqrt(Math.Pow(Globals.gamedata.my_char.X - x, 2) +
                        Math.Pow(Globals.gamedata.my_char.Y - y, 2)) <= dist)
                    {
                        //we are close enough to our destination
                        is_Moving = false;
#if DEBUG
                        Globals.l2net_home.Add_Debug("Finished moving");
#endif
                        if (inc)
                        {
                            Line_Pos++;

                        }
                    }
                    else
                    {
                        // Globals.l2net_home.Add_Debug("not close enough to target...");
                        //if (Globals.gamedata.my_char.Moving)//why?
                        //{
                        // Globals.l2net_home.Add_Debug("still moving...");
                        //are we moving to the correct location?
                        if (Moving_List != null && Moving_List.Count != 0)
                        {

                            if (Math.Sqrt(Math.Pow(Globals.gamedata.my_char.Dest_X - ((Point)Moving_List[0]).X, 2) +
                               Math.Pow(Globals.gamedata.my_char.Dest_Y - ((Point)Moving_List[0]).Y, 2)) <= tolerance)
                            {
                                // Globals.l2net_home.Add_Debug("moving to correct point...");
                                //need to check how close we are to our next point...
                                //  Globals.l2net_home.Add_Debug(Math.Sqrt(Math.Pow(Globals.gamedata.my_char.X - ((Point)Moving_List[0]).X, 2) +
                                //     Math.Pow(Globals.gamedata.my_char.Y - ((Point)Moving_List[0]).Y, 2)) + " vs " + dist);
                                if (Math.Sqrt(Math.Pow(Globals.gamedata.my_char.X - ((Point)Moving_List[0]).X, 2) +
                                    Math.Pow(Globals.gamedata.my_char.Y - ((Point)Moving_List[0]).Y, 2)) <= dist)
                                {
#if DEBUG
                                    Globals.l2net_home.Add_Debug("finished point");
#endif
                                    //close enough to this point...
                                    Moving_List.RemoveAt(0);

                                    //move to the next point
                                    Script_MOVE_SMART_InternalP0();
                                }
                                else
                                {
#if DEBUG
                                    Globals.l2net_home.Add_Debug("waiting 1");
#endif
                                    Script_SLEEP(10);
                                }
                            }
                            else
                            {
#if DEBUG
                                Globals.l2net_home.Add_Debug("destX:" + Globals.gamedata.my_char.Dest_X +
                                                             " moveX:" + ((Point)Moving_List[0]).X);
#endif
                                //lets get us moving to the correct point in the list
                                Script_MOVE_SMART_InternalP0();
                            }
                        }
                        else
                        {
                            //  Globals.l2net_home.Add_Debug("move list was null");
                        }
                        // }
                        //else
                        //{

                        //     Globals.l2net_home.Add_Debug("waiting 2");
                        //    Script_MOVE_SMART_InternalP0();
                        // }
                    }
                    //  }
                    // else
                    // {
                    //we need to wait for this move to complete before we can move...
                    //     Globals.l2net_home.Add_Debug("waiting 2");
                    //     Script_SLEEP(10);
                }
                else
                {
                    is_Moving = false;
                    Moving_List.Clear();
                }
            }
            else
            {
#if DEBUG
                Globals.l2net_home.Add_Debug("not moving... getting path");
#endif
                Moving_X = x;
                Moving_Y = y;
                Moving_Z = z;

                is_Moving = true;

                Point my_pos = new Point();
                my_pos.X = Globals.gamedata.my_char.X;
                my_pos.Y = Globals.gamedata.my_char.Y;
                my_pos.Z = Globals.gamedata.my_char.Z;

                Point my_dest = new Point();
                my_dest.X = x;
                my_dest.Y = y;
                my_dest.Z = z;

                Moving_List = Pathing.GetPath(my_pos, my_dest);

                if (Moving_List == null || Moving_List.Count == 0)
                {
                    Globals.l2net_home.Add_Debug("Cannot find a path. Giving up.");
                    is_Moving = false;
                    Line_Pos++;
          
                }


                Script_MOVE_SMART_InternalP0();
            }
        }

        private void Script_MOVE_SMART_InternalP0()
        {
            if(Moving_List != null)
                if(Moving_List.Count > 0)
                    ServerPackets.MoveToPacket((int)((Point)Moving_List[0]).X, (int)((Point)Moving_List[0]).Y);

            Script_SLEEP(100);
        }
    }
}
