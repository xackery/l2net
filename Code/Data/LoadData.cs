using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO.Compression;

namespace L2_login
{
    class LoadData
    {
        private static byte[] data_lvlexp;
        private static byte[] data_servername;
        private static byte[] data_systemmsg;
        private static byte[] data_hennagrp;
        private static byte[] data_npcname;
        private static byte[] data_itemname;
        private static byte[] data_etcitemgrp;
        private static byte[] data_weapongrp;
        private static byte[] data_armorgrp;
        private static byte[] data_classes;
        private static byte[] data_races;
        private static byte[] data_skillname;
        private static byte[] data_actionname;
        private static byte[] data_questname;
        private static byte[] data_zonename;
        private static byte[] data_npcstring;

        private static bool isDataLoaded = false;
        public static void LoadDataFiles()
        {
            if (isDataLoaded)
            { //This can only be loaded once.
                return;
            }

            try
            {
                data_lvlexp = GetData(Globals.PATH + "\\data\\lvlexp.txt");
                data_servername = GetData(Globals.PATH + "\\data\\servername.txt");
                data_systemmsg = GetData(Globals.PATH + "\\data\\systemmsg.txt");
                data_hennagrp = GetData(Globals.PATH + "\\data\\hennagrp.txt");
                data_npcname = GetData(Globals.PATH + "\\data\\npcname.txt");
                data_itemname = GetData(Globals.PATH + "\\data\\itemname.txt");
                data_etcitemgrp = GetData(Globals.PATH + "\\data\\etcitemgrp.txt");
                data_weapongrp = GetData(Globals.PATH + "\\data\\weapongrp.txt");
                data_armorgrp = GetData(Globals.PATH + "\\data\\armorgrp.txt");
                data_classes = GetData(Globals.PATH + "\\data\\classes.txt");
                data_races = GetData(Globals.PATH + "\\data\\races.txt");
                data_skillname = GetData(Globals.PATH + "\\data\\skillname.txt");
                data_actionname = GetData(Globals.PATH + "\\data\\actionname.txt");
                //data_questname = GetData(Globals.PATH + "\\data\\questname.txt");
                data_zonename = GetData(Globals.PATH + "\\data\\zonename.txt");
                data_npcstring = GetData(Globals.PATH + "\\data\\npcstring.txt");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            LoadSkills();
            LoadItemName();
            LoadSystemMsg();
            LoadNPCName();
            LoadActions();
            LoadHennaGrp();
            LoadZones();
            LoadServerName();
            LoadXP();
            LoadClasses();
            LoadRaces();
            LoadGG();
            LoadNPCString();
            Globals.pck_thread.load_filters();
            /*
            System.Threading.Tasks.Parallel.For(0, 14, (i) =>
                {
                    switch (i)
                    {
                        case 0:
                            LoadSkills();
                            break;
                        case 1:
                            LoadItemName();
                            break;
                        case 2:
                            LoadSystemMsg();
                            break;
                        //case 3:
                            //LoadQuests();
                            //break;
                        case 4:
                            LoadNPCName();
                            break;
                        case 5:
                            LoadActions();
                            break;
                        case 6:
                            LoadHennaGrp();
                            break;
                        case 7:
                            LoadZones();
                            break;
                        case 8:
                            LoadServerName();
                            break;
                        case 9:
                            LoadXP();
                            break;
                        case 10:
                            LoadClasses();
                            break;
                        case 11:
                            LoadRaces();
                            break;
                        case 12:
                            LoadGG();
                            break;
                        case 13:
                            LoadNPCString();
                            break;
                        case 3:
                            Globals.pck_thread.load_filters();
                            break;
                    }
                }
            );
            */

            data_lvlexp = null;
            data_servername = null;
            data_systemmsg = null;
            data_hennagrp = null;
            data_npcname = null;
            data_itemname = null;
            data_etcitemgrp = null;
            data_weapongrp = null;
            data_armorgrp = null;
            data_classes = null;
            data_races = null;
            data_skillname = null;
            data_actionname = null;
            data_questname = null;
            data_zonename = null;
            data_npcstring = null;

            isDataLoaded = true;
        }

        private static byte[] GetData(string filename)
        {
            if (!System.IO.File.Exists(filename))
            {
                throw new Exception("File not found: " + filename);
            }


            System.IO.BinaryReader filein = new System.IO.BinaryReader(new System.IO.StreamReader(filename).BaseStream);

            byte[] data = new byte[filein.BaseStream.Length];
            data = filein.ReadBytes((int)filein.BaseStream.Length);
            filein.Close();

            return data;
        }

        private static byte[] GetData(byte[] data, string salt)
        {
            return AES.Decrypt(data, Globals.AES_Key, salt);
        }

        private static void LoadGG()
        {
#if !DEBUG
            try
            {
#endif
            System.IO.StreamReader filein = new System.IO.StreamReader(Globals.PATH + "\\data\\gg.txt");

            string login_gg = filein.ReadLine();
            //need to convert this string and store in the global thingy...
            login_gg = login_gg.Replace(" ", "");

            Globals.Login_GG_Reply = new byte[login_gg.Length/2];

            string sm;
            for (int i = 0; i < login_gg.Length; i += 2)
            {
                sm = (login_gg[i].ToString()) + (login_gg[i + 1].ToString());

                Globals.Login_GG_Reply[i / 2] = byte.Parse(sm, System.Globalization.NumberStyles.HexNumber);
            }

            filein.ReadLine();

            Globals.GG_List = new SortedList();

            string gg_q, gg_r;

            while ((gg_q = filein.ReadLine()) != null)
            {
                gg_r = filein.ReadLine();

                //need to parse the strings and add to the list
                gg_q = gg_q.Replace(" ", "");
                gg_r = gg_r.Replace(" ", "");

                byte[] reply_gg = new byte[gg_r.Length / 2];

                for (int i = 0; i < gg_r.Length; i += 2)
                {
                    sm = (gg_r[i].ToString()) + (gg_r[i + 1].ToString());

                    reply_gg[i / 2] = byte.Parse(sm, System.Globalization.NumberStyles.HexNumber);
                }

                Globals.GG_List.Add(gg_q, reply_gg);

                //
                filein.ReadLine();
            }

            filein.Close();

            //Add_Text("loaded gg", Globals.Red);
#if !DEBUG
            }
            catch
            {
                Globals.l2net_home.Add_PopUpError("failed to load data\\gg.txt");
            }
#endif
        }

        private static void LoadXP()
        {
            string loaded;

            byte[] dec;
            System.IO.StreamReader temp_stream;
            System.IO.MemoryStream mem_stream;

#if !DEBUG
            try
            {
#endif
            dec = GetData(data_lvlexp, "<M4H90ag7{_j6~3[");

            mem_stream = new System.IO.MemoryStream(dec);
            temp_stream = new System.IO.StreamReader((System.IO.Stream)mem_stream);

            int version = Util.GetInt32(temp_stream.ReadLine());
            if (version < Globals.MinDataPack)
            {
                System.Windows.Forms.MessageBox.Show("lvlexp.txt is too old for this version of L2.Net!");
                System.Windows.Forms.Application.Exit();
            }

            Globals.levelexp = new SortedList();

            while ((loaded = temp_stream.ReadLine()) != null)
            {
                int pipe;
                //lvl
                pipe = loaded.IndexOf('|');
                uint lvl = Util.GetUInt32(loaded.Substring(0, pipe));
                //xp
                ulong xp = Util.GetUInt64(loaded.Substring(pipe + 1, loaded.Length - pipe - 1));

                Globals.levelexp.Add(lvl, xp);
            }

            mem_stream.Close();
            temp_stream.Close();

            //Add_Text("loaded lvlexp", Globals.Red);
#if !DEBUG
            }
            catch
            {
                Globals.l2net_home.Add_PopUpError("failed to load data\\lvlexp.txt");
            }
#endif

            dec = null;
        }

        private static void LoadServerName()
        {
            string loaded;

            byte[] dec;
            System.IO.StreamReader temp_stream;
            System.IO.MemoryStream mem_stream;

            try
            {
                dec = GetData(data_servername, "-8:2eF_08$6-o)IJ");

                mem_stream = new System.IO.MemoryStream(dec);
                temp_stream = new System.IO.StreamReader((System.IO.Stream)mem_stream);

                int version = Util.GetInt32(temp_stream.ReadLine());
                if (version < Globals.MinDataPack)
                {
                    System.Windows.Forms.MessageBox.Show("servername.txt is too old for this version of L2.Net!");
                    System.Windows.Forms.Application.Exit();
                }

                Globals.servername = new SortedList();
                Globals.servername.Capacity = Globals.COUNT_SERVERNAME;

                while ((loaded = temp_stream.ReadLine()) != null)
                {
                    ServerName sername = new ServerName();

                    sername.Parse(loaded);

                    Globals.servername.Add(sername.ID, sername);
                }

                mem_stream.Close();
                temp_stream.Close();

                //Add_Text("loaded servername", Globals.Red);
            }
            catch
            {
                Globals.l2net_home.Add_PopUpError("failed to load data\\servername.txt");
            }

            dec = null;
        }

        private static void LoadSystemMsg()
        {
            string loaded;

            byte[] dec;
            System.IO.StreamReader temp_stream;
            System.IO.MemoryStream mem_stream;

            try
            {
                dec = GetData(data_systemmsg, "gA7ru8akusakenaf");

                mem_stream = new System.IO.MemoryStream(dec);
                temp_stream = new System.IO.StreamReader((System.IO.Stream)mem_stream);

                int version = Util.GetInt32(temp_stream.ReadLine());
                if (version < Globals.MinDataPack)
                {
                    System.Windows.Forms.MessageBox.Show("systemmsg.txt is too old for this version of L2.Net!");
                    System.Windows.Forms.Application.Exit();
                }

                Globals.systemmsg = new SortedList();
                Globals.systemmsg.Capacity = Globals.COUNT_SYSTEMMSG;

                while ((loaded = temp_stream.ReadLine()) != null)
                {
                    SystemMsg sysmsg = new SystemMsg();

                    sysmsg.Parse(loaded);

                    Globals.systemmsg.Add(sysmsg.ID, sysmsg);
                }

                mem_stream.Close();
                temp_stream.Close();

                //Add_Text("loaded systemmsgs", Globals.Red);
            }
            catch
            {
                Globals.l2net_home.Add_PopUpError("failed to load data\\systemmsg.txt");
            }

            dec = null;
        }

        private static void LoadHennaGrp()
        {
            string loaded;

            byte[] dec;
            System.IO.StreamReader temp_stream;
            System.IO.MemoryStream mem_stream;

            try
            {
                dec = GetData(data_hennagrp, "8Er5FREjaCen7Thu");

                mem_stream = new System.IO.MemoryStream(dec);
                temp_stream = new System.IO.StreamReader((System.IO.Stream)mem_stream);

                int version = Util.GetInt32(temp_stream.ReadLine());
                if (version < Globals.MinDataPack)
                {
                    System.Windows.Forms.MessageBox.Show("hennagrp.txt is too old for this version of L2.Net!");
                    System.Windows.Forms.Application.Exit();
                }

                Globals.hennagrp = new SortedList();
                Globals.hennagrp.Capacity = Globals.COUNT_HENNAGROUP;

                while ((loaded = temp_stream.ReadLine()) != null)
                {
                    HennaGroup hengrp = new HennaGroup();

                    hengrp.Parse(loaded);

                    Globals.hennagrp.Add(hengrp.ID, hengrp);
                }

                mem_stream.Close();
                temp_stream.Close();

                HennaGroup null_dye = new HennaGroup();
                null_dye.ID = 0;
                null_dye.Name = "no dye";
                Globals.hennagrp.Add((uint)0, null_dye);

                //Add_Text("loaded hennagrp", Globals.Red);
            }
            catch
            {
                Globals.l2net_home.Add_PopUpError("failed to load data\\hennagrp.txt");
            }

            dec = null;
        }

        private static void LoadNPCName()
        {
            string loaded;

            byte[] dec;
            System.IO.StreamReader temp_stream;
            System.IO.MemoryStream mem_stream;

            try
            {
                dec = GetData(data_npcname, "c8c2xagUga5enE7A");

                mem_stream = new System.IO.MemoryStream(dec);
                temp_stream = new System.IO.StreamReader((System.IO.Stream)mem_stream);

                int version = Util.GetInt32(temp_stream.ReadLine());
                if (version < Globals.MinDataPack)
                {
                    System.Windows.Forms.MessageBox.Show("npcname.txt is too old for this version of L2.Net!");
                    System.Windows.Forms.Application.Exit();
                }

                Globals.npcname = new SortedList();
                Globals.npcname.Capacity = Globals.COUNT_NPCNAME;

                while ((loaded = temp_stream.ReadLine()) != null)
                {
                    NPCName npcnm = new NPCName();

                    npcnm.Parse(loaded);

                    Globals.npcname.Add(npcnm.ID, npcnm);
                }

                mem_stream.Close();
                temp_stream.Close();

                NPCName null_npc = new NPCName();
                null_npc.ID = 0;
                null_npc.Name = "null npc";
                Globals.npcname.Add((uint)0, null_npc);

                //Add_Text("loaded npcname", Globals.Red);
            }
            catch
            {
                Globals.l2net_home.Add_PopUpError("failed to load data\\npcname.txt");
            }

            dec = null;
        }

        private static void LoadItemName()
        {
            string loaded;

            byte[] dec;
            System.IO.StreamReader temp_stream;
            System.IO.MemoryStream mem_stream;

            try
            {
                dec = GetData(data_itemname, "speves4azu52JutH");

                mem_stream = new System.IO.MemoryStream(dec);
                temp_stream = new System.IO.StreamReader((System.IO.Stream)mem_stream);

                int version = Util.GetInt32(temp_stream.ReadLine());
                if (version < Globals.MinDataPack)
                {
                    System.Windows.Forms.MessageBox.Show("itemname.txt is too old for this version of L2.Net!");
                    System.Windows.Forms.Application.Exit();
                }
                
                Globals.itemname = new SortedList();
                Globals.itemname.Capacity = Globals.COUNT_ITEMNAME;

                while ((loaded = temp_stream.ReadLine()) != null)
                {
                    ItemName itemnm = new ItemName();

                    itemnm.Parse(loaded);

                    Globals.itemname.Add(itemnm.ID, itemnm);
                }

                mem_stream.Close();
                temp_stream.Close();

                ItemName null_itm = new ItemName();
                null_itm.ID = 0;
                null_itm.Name = "no item";
                Globals.itemname.Add((uint)0, null_itm);

                //Add_Text("loaded itemname", Globals.Red);
            }
            catch(Exception e)
            {
                Globals.l2net_home.Add_PopUpError("failed to load data\\itemname.txt" + e.Message);
            }
            //////////////////
            try
            {
                dec = GetData(data_etcitemgrp, "5rePruchetupHuth");

                mem_stream = new System.IO.MemoryStream(dec);
                temp_stream = new System.IO.StreamReader((System.IO.Stream)mem_stream);

                int version2 = Util.GetInt32(temp_stream.ReadLine());
                if (version2 < Globals.MinDataPack)
                {
                    System.Windows.Forms.MessageBox.Show("etcitemgrp.txt is too old for this version of L2.Net!");
                    System.Windows.Forms.Application.Exit();
                }
                
                while ((loaded = temp_stream.ReadLine()) != null)
                {
                    ItemName itemnm = new ItemName();

                    itemnm.ParseETC(loaded);

                    ((ItemName)Globals.itemname[itemnm.ID]).ParseETC(itemnm);
                }

                mem_stream.Close();
                temp_stream.Close();

                //Add_Text("loaded etcitemgrp", Globals.Red);
            }
            catch
            {
                Globals.l2net_home.Add_PopUpError("failed to load data\\etcitemgrp.txt");
            }
            //////////////
            try
            {
                dec = GetData(data_weapongrp, "defru6e4ezuyuSWE");

                mem_stream = new System.IO.MemoryStream(dec);
                temp_stream = new System.IO.StreamReader((System.IO.Stream)mem_stream);

                int version232 = Util.GetInt32(temp_stream.ReadLine());
                if (version232 < Globals.MinDataPack)
                {
                    System.Windows.Forms.MessageBox.Show("weapongrp.txt is too old for this version of L2.Net!");
                    System.Windows.Forms.Application.Exit();
                }
                
                while ((loaded = temp_stream.ReadLine()) != null)
                {
                    ItemName itemnm = new ItemName();

                    itemnm.ParseWeapon(loaded);

                    if (Globals.itemname.ContainsKey(itemnm.ID))
                    {
                        ((ItemName)Globals.itemname[itemnm.ID]).ParseWeapon(itemnm);
                    }
                }

                mem_stream.Close();
                temp_stream.Close();

                //Add_Text("loaded weapongrp", Globals.Red);
            }
            catch (Exception e)
            {
                Globals.l2net_home.Add_PopUpError("failed to load data\\weapongrp.txt " + e.Message);
            }
            //////////////
            try
            {
                dec = GetData(data_armorgrp, "8uf53XU3ravudepe");

                mem_stream = new System.IO.MemoryStream(dec);
                temp_stream = new System.IO.StreamReader((System.IO.Stream)mem_stream);

                int version = Util.GetInt32(temp_stream.ReadLine());
                if (version < Globals.MinDataPack)
                {
                    System.Windows.Forms.MessageBox.Show("armorgrp.txt is too old for this version of L2.Net!");
                    System.Windows.Forms.Application.Exit();
                }
                
                while ((loaded = temp_stream.ReadLine()) != null)
                {
                    ItemName itemnm = new ItemName();

                    itemnm.ParseArmor(loaded);

                    ((ItemName)Globals.itemname[itemnm.ID]).ParseArmor(itemnm);
                }

                mem_stream.Close();
                temp_stream.Close();

                //Add_Text("loaded armorgrp", Globals.Red);
            }
            catch
            {
                Globals.l2net_home.Add_PopUpError("failed to load data\\armorgrp.txt");
            }

            dec = null;
        }

        private static void LoadClasses()
        {
            string loaded;

            byte[] dec;
            System.IO.StreamReader temp_stream;
            System.IO.MemoryStream mem_stream;

            try
            {
                dec = GetData(data_classes, "-^P_6.97R)YEFW4y");

                mem_stream = new System.IO.MemoryStream(dec);
                temp_stream = new System.IO.StreamReader((System.IO.Stream)mem_stream);

                int version = Util.GetInt32(temp_stream.ReadLine());
                if (version < Globals.MinDataPack)
                {
                    System.Windows.Forms.MessageBox.Show("classes.txt is too old for this version of L2.Net!");
                    System.Windows.Forms.Application.Exit();
                }
                
                Globals.classes = new SortedList();
                Globals.classes.Capacity = Globals.COUNT_CLASSES;

                while ((loaded = temp_stream.ReadLine()) != null)
                {
                    Classes classs = new Classes();

                    classs.Parse(loaded);

                    Globals.classes.Add(classs.ID, classs);
                }

                mem_stream.Close();
                temp_stream.Close();

                //Add_Text("loaded classes", Globals.Red);
            }
            catch
            {
                Globals.l2net_home.Add_PopUpError("failed to load data\\classes.txt");
            }

            dec = null;
        }

        private static void LoadRaces()
        {
            string loaded;

            byte[] dec;
            System.IO.StreamReader temp_stream;
            System.IO.MemoryStream mem_stream;

            try
            {
                dec = GetData(data_races, "3L=GSb_H4j-&S£m1");

                mem_stream = new System.IO.MemoryStream(dec);
                temp_stream = new System.IO.StreamReader((System.IO.Stream)mem_stream);

                int version = Util.GetInt32(temp_stream.ReadLine());
                if (version < Globals.MinDataPack)
                {
                    System.Windows.Forms.MessageBox.Show("races.txt is too old for this version of L2.Net!");
                    System.Windows.Forms.Application.Exit();
                }

                Globals.races = new SortedList();
                Globals.races.Capacity = Globals.COUNT_RACES;

                while ((loaded = temp_stream.ReadLine()) != null)
                {
                    Races race = new Races();

                    race.Parse(loaded);

                    Globals.races.Add(race.ID, race);
                }

                mem_stream.Close();
                temp_stream.Close();

                //Add_Text("loaded races", Globals.Red);
            }
            catch
            {
                Globals.l2net_home.Add_PopUpError("failed to load data\\races.txt");
            }

            dec = null;
        }

        private static void LoadSkills()
        {
            try {
                string loaded;

                byte[] dec;
                System.IO.StreamReader temp_stream;
                System.IO.MemoryStream mem_stream;

                dec = GetData(data_skillname, "br2qeSw65ephepH8");

                mem_stream = new System.IO.MemoryStream(dec);
                temp_stream = new System.IO.StreamReader((System.IO.Stream)mem_stream);

                int version = Util.GetInt32(temp_stream.ReadLine());

                if (version < Globals.MinDataPack)
                {
                    System.Windows.Forms.MessageBox.Show("skillname.txt is too old for this version of L2.Net!");
                    System.Windows.Forms.Application.Exit();
                }

                Globals.skilllist = new SortedList();
                Globals.skilllist.Capacity = Globals.COUNT_SKILLS;

                while ((loaded = temp_stream.ReadLine()) != null)
                {
                    SkillInfo sk_inf = new SkillInfo();

                    sk_inf.Parse(loaded);

                    if (Globals.skilllist.IndexOfKey(sk_inf.ID) == -1)
                    {
                        //the key wasnt found
                        SkillList skill = new SkillList();

                        Globals.skilllist.Add(sk_inf.ID, skill);
                    }

                    ((SkillList)Globals.skilllist[sk_inf.ID]).Add_Level(sk_inf);
                }

                mem_stream.Close();
                temp_stream.Close();

                //Add_Text("loaded skills", Globals.Red);

                dec = null;
            } catch (Exception e)
            {
                throw new Exception("Error during LoadSkill: " + e.Message);
            }
        }

        private static void LoadActions()
        {
            string loaded;

            byte[] dec;
            System.IO.StreamReader temp_stream;
            System.IO.MemoryStream mem_stream;

            try
            {
                dec = GetData(data_actionname, "bruyurEja6rUwaph");

                mem_stream = new System.IO.MemoryStream(dec);
                temp_stream = new System.IO.StreamReader((System.IO.Stream)mem_stream);

                int version = Util.GetInt32(temp_stream.ReadLine());
                if (version < Globals.MinDataPack)
                {
                    System.Windows.Forms.MessageBox.Show("actionname.txt is too old for this version of L2.Net!");
                    System.Windows.Forms.Application.Exit();
                }
                
                Globals.actionlist = new SortedList();
                Globals.actionlist.Capacity = Globals.COUNT_ACTIONS;

                while ((loaded = temp_stream.ReadLine()) != null)
                {
                    Actions act_inf = new Actions();

                    act_inf.Parse(loaded);

                    Globals.actionlist.Add(act_inf.ID, act_inf);
                }

                mem_stream.Close();
                temp_stream.Close();

                //Add_Text("loaded actions", Globals.Red);
            }
            catch
            {
                Globals.l2net_home.Add_PopUpError("failed to load data\\actionname.txt");
            }

            dec = null;
        }

        private static void LoadQuests()
        {
            string loaded;

            byte[] dec;
            System.IO.StreamReader temp_stream;
            System.IO.MemoryStream mem_stream;

            try
            {
                dec = GetData(data_questname, "D_d/-pmzRPnC|!mS");

                mem_stream = new System.IO.MemoryStream(dec);
                temp_stream = new System.IO.StreamReader((System.IO.Stream)mem_stream);

                int version = Util.GetInt32(temp_stream.ReadLine());
                if (version < Globals.MinDataPack)
                {
                    System.Windows.Forms.MessageBox.Show("questname.txt is too old for this version of L2.Net!");
                    System.Windows.Forms.Application.Exit();
                }
                
                Globals.questlist = new SortedList();
                Globals.questlist.Capacity = Globals.COUNT_QUESTS;

                while ((loaded = temp_stream.ReadLine()) != null)
                {
                    QuestName qst_inf = new QuestName();

                    qst_inf.Parse(loaded);

                    Globals.questlist.Add(qst_inf.ID * Globals.ID_OFFSET + qst_inf.quest_prog, qst_inf);
                }

                mem_stream.Close();
                temp_stream.Close();

                //Add_Text("loaded quests", Globals.Red);
            }
            catch
            {
                Globals.l2net_home.Add_PopUpError("failed to load data\\questname.txt");
            }

            dec = null;
        }

        private static void LoadZones()
        {
            string loaded;

            byte[] dec;
            System.IO.StreamReader temp_stream;
            System.IO.MemoryStream mem_stream;

            try
            {
                dec = GetData(data_zonename, "pUB6epUC7uwraxaw");

                mem_stream = new System.IO.MemoryStream(dec);
                temp_stream = new System.IO.StreamReader((System.IO.Stream)mem_stream);

                int version = Util.GetInt32(temp_stream.ReadLine());
                if (version < Globals.MinDataPack)
                {
                    System.Windows.Forms.MessageBox.Show("zonename.txt is too old for this version of L2.Net!");
                    System.Windows.Forms.Application.Exit();
                }
                
                Globals.zonelist = new SortedList();
                Globals.zonelist.Capacity = Globals.COUNT_ZONES;

                while ((loaded = temp_stream.ReadLine()) != null)
                {
                    ZoneName zn_inf = new ZoneName();

                    zn_inf.Parse(loaded);

                    Globals.zonelist.Add(zn_inf.ID, zn_inf);
                }

                mem_stream.Close();
                temp_stream.Close();

                //Add_Text("loaded zones", Globals.Red);
            }
            catch
            {
                Globals.l2net_home.Add_PopUpError("failed to load data\\zonename.txt");
            }

            dec = null;
        }

        private static void LoadNPCString()
        {
            string loaded;

            byte[] dec;
            System.IO.StreamReader temp_stream;
            System.IO.MemoryStream mem_stream;

            try
            {
                ///#)"(#&JH(S&ZKS=
                //dec = GetData(Globals.PATH + "\\data\\npcstring.txt");
                dec = GetData(data_npcstring, ")#&!%J)(/S)J/&%¤");

                mem_stream = new System.IO.MemoryStream(dec);
                temp_stream = new System.IO.StreamReader((System.IO.Stream)mem_stream);

                int version = Util.GetInt32(temp_stream.ReadLine());
                if (version < Globals.MinDataPack)
                {
                    System.Windows.Forms.MessageBox.Show("npcstring.txt is too old for this version of L2.Net!");
                    System.Windows.Forms.Application.Exit();
                }

                Globals.npcstring = new SortedList();
                Globals.npcstring.Capacity = Globals.COUNT_NPCSTRING;

                while ((loaded = temp_stream.ReadLine()) != null)
                {
                    NPCString npcstr = new NPCString();

                    npcstr.Parse(loaded);

                    Globals.npcstring.Add(npcstr.ID, npcstr);
                }

                mem_stream.Close();
                temp_stream.Close();

                //Globals.l2net_home.Add_Text("loaded npcstring", Globals.Red);
            }
            catch
            {
                Globals.l2net_home.Add_PopUpError("failed to load data\\npcstring.txt");
            }

            dec = null;
        }

        /*private static void LoadStaticObjects()
        {
            //   wvYoty4^#E9(1l3d
        }*/

        /*private static void LoadOptionData()
        {
            //   #E9(1l3dwvYoty4^
        }*/
    }
}
