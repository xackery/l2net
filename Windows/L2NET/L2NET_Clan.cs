using System;
using System.Collections.Generic;
using System.Text;

namespace L2_login
{
    public partial class L2NET
    {
        delegate void Set_ClanInfoUpdate_Callback(ByteBuffer buff);
        public void Set_ClanInfoUpdate(ByteBuffer buff)
        {
            if (this.label_clan_level.InvokeRequired)
            {
                Set_ClanInfoUpdate_Callback d = new Set_ClanInfoUpdate_Callback(Set_ClanInfoUpdate);
                label_clan_level.Invoke(d, new object[] { buff });
                return;
            }

            string mem_name = buff.ReadString();//Util.Get_String(buff,ref offset);
            uint mem_level = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,offset);offset+=4;
            uint mem_class = buff.ReadUInt32();//System.BitConverter.ToUInt32(buff,offset);offset+=4;
            buff.ReadUInt32();//sex
            buff.ReadUInt32();//race
            uint mem_online = buff.ReadUInt32();//online
            buff.ReadUInt32();//pledge type
            buff.ReadUInt32();//yellow name for sponsor

            bool found = false;
            foreach (System.Windows.Forms.ListViewItem obj in Globals.l2net_home.listView_char_clan.Items)
            {
                if (System.String.Equals(obj.SubItems[0].Text, mem_name))
                {
                    obj.SubItems[1].Text = mem_level.ToString();
                    obj.SubItems[2].Text = Util.GetClass(mem_class);
                    if (mem_online != 0)
                    {
                        if (obj.SubItems[3].Text == "X")
                        {
                        }
                        else
                        {
                            Globals.ClanOnline++;
                            obj.SubItems[3].Text = "X";
                        }
                    }
                    else
                    {
                        if (obj.SubItems[3].Text == " ")
                        {
                        }
                        else
                        {
                            Globals.ClanOnline--;
                            obj.SubItems[3].Text = " ";
                        }
                    }
                    
                    found = true;
                }
            }

            if (!found)
            {
                System.Windows.Forms.ListViewItem ObjListItem = Globals.l2net_home.listView_char_clan.Items.Add(mem_name);
                ObjListItem.SubItems.Add(mem_level.ToString());
                ObjListItem.SubItems.Add(Util.GetClass(mem_class));
                if (mem_online == 1)
                {
                    Globals.ClanOnline++;
                    ObjListItem.SubItems.Add("X");
                }
                else
                {
                    ObjListItem.SubItems.Add(" ");
                }

                Globals.ClanMembers++;
            }

            label_clan_online.Text = Globals.m_ResourceManager.GetString("col_Online") + ": " + Globals.ClanOnline.ToString() + "/" + Globals.ClanMembers.ToString();
        }

        delegate void Set_ClanInfo_Callback(ByteBuffer buff);
        public void Set_ClanInfo(ByteBuffer buff)
        {
            if (this.label_clan_level.InvokeRequired)
            {
                Set_ClanInfo_Callback d = new Set_ClanInfo_Callback(Set_ClanInfo);
                label_clan_level.Invoke(d, new object[] { buff });
                return;
            }

            string clan_name = "";
            string clan_leader = "";
            uint clan_crest = 0;
            uint clan_level = 0;
            uint clan_castle = 0;
            uint clan_hideout = 0;
            uint my_level = 0;
            int clan_rep = 0;
            uint ally_id = 0;
            string ally_name = ""; ;
            uint ally_crest = 0;
            uint in_war = 0;

            buff.ReadUInt32();//main or sub pledge 1 = academy, 0 = main
            uint clan_id = buff.ReadUInt32();
            uint clan_type = buff.ReadUInt32();//pledge type,  0 = main

            bool update = false;

            if (Globals.gamedata.Chron >= Chronicle.CT3_0)
            {
                switch (clan_type)
                {
                    case 0x00: //main clan
                        clan_name = buff.ReadString();
                        clan_leader = buff.ReadString();

                        clan_crest = buff.ReadUInt32();
                        clan_level = buff.ReadUInt32();

                        clan_castle = buff.ReadUInt32();
                        clan_hideout = buff.ReadUInt32();
                        buff.ReadUInt32(); //fort?
                        buff.ReadUInt32(); //rank?
                        my_level = buff.ReadUInt32();
                        clan_rep = buff.ReadInt32();
                        buff.ReadUInt32();//1 0 0 0
                        buff.ReadUInt32();//0 0 0 0

                        ally_id = buff.ReadUInt32();
                        ally_name = buff.ReadString();
                        ally_crest = buff.ReadUInt32();
                        in_war = buff.ReadUInt32();
                        buff.ReadUInt32(); //territory castle id?

                        Globals.ClanMembers = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,offset);offset+=4;
                        //Globals.l2net_home.Add_Text("Clan name: " + clan_name, Globals.Green, TextType.BOT);
                        //Globals.l2net_home.Add_Text("Clan leader: " + clan_leader, Globals.Green, TextType.BOT);
                        //Globals.l2net_home.Add_Text("Clan crest: " + clan_crest.ToString("X2"), Globals.Green, TextType.BOT);
                        //Globals.l2net_home.Add_Text("Clan level: " + clan_level.ToString(), Globals.Green, TextType.BOT);
                        //Globals.l2net_home.Add_Text("Clan rep: " + clan_rep.ToString(), Globals.Green, TextType.BOT);
                        //Globals.l2net_home.Add_Text("Ally name: " + ally_name, Globals.Green, TextType.BOT);
                        //Globals.l2net_home.Add_Text("Clan members: " + Globals.ClanMembers.ToString(), Globals.Green, TextType.BOT);



                        Globals.ClanOnline = 0;

                        listView_char_clan.BeginUpdate();

                        listView_char_clan.Items.Clear();
                        for (uint i = 0; i < Globals.ClanMembers; i++)
                        {
                            Set_MemberInfo(buff, false);
                        }

                        listView_char_clan.EndUpdate();
                        update = true;
                        break;
                    case 0x64000000: //1st royal guard
                        break;
                    case 0xC8000000: //2nd royal guard
                        break;
                    case 0xE9030000: //1st order of knights
                        break;
                    case 0xEA030000: //2nd order of knights
                        break;
                    case 0xD1070000: //3rd order of knights
                        break;
                    case 0xD2070000: //4th order of knights
                        break;
                    case 0xFFFFFFFF: //academy
                        break;
                }


            }
            else
            {
                clan_name = buff.ReadString();
                clan_leader = buff.ReadString();

                clan_crest = buff.ReadUInt32();
                clan_level = buff.ReadUInt32();

                clan_castle = buff.ReadUInt32();
                clan_hideout = buff.ReadUInt32();
                my_level = buff.ReadUInt32();
                clan_rep = buff.ReadInt32();
                buff.ReadUInt32();//1 0 0 0
                buff.ReadUInt32();//0 0 0 0
                buff.ReadUInt32();//0 0 0 0

                ally_id = buff.ReadUInt32();
                ally_name = buff.ReadString();
                ally_crest = buff.ReadUInt32();
                in_war = buff.ReadUInt32();

                Globals.ClanMembers = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,offset);offset+=4;
                //Globals.l2net_home.Add_Text("Clan members: " + Globals.ClanMembers.ToString(), Globals.Green, TextType.BOT);


                Globals.ClanOnline = 0;

                listView_char_clan.BeginUpdate();
                //listView_char_clan.ListViewItemSorter = null;

                listView_char_clan.Items.Clear();
                for (uint i = 0; i < Globals.ClanMembers; i++)
                {
                    Set_MemberInfo(buff, false);
                }

                //listView_char_clan.ListViewItemSorter = lvwColumnSorter_clan;
                listView_char_clan.EndUpdate();
                update = true;
            }

            if (update)
            {
                label_clan_name.Text = clan_name;
                label_clan_leader.Text = clan_leader;
                label_clan_level.Text = clan_level.ToString();
                label_clan_castle.Text = clan_castle.ToString();
                label_clan_hall.Text = clan_hideout.ToString();
                label_clan_rep.Text = clan_rep.ToString();
                label_clan_war.Text = in_war.ToString();
                label_caln_ally.Text = ally_name;
                label_clan_online.Text = Globals.m_ResourceManager.GetString("col_Online") + ": " + Globals.ClanOnline.ToString() + "/" + Globals.ClanMembers.ToString();

                try
                {
                    if (pictureBox_clan_crest.BackgroundImage != null)
                    {
                        pictureBox_clan_crest.BackgroundImage.Dispose();
                        pictureBox_clan_crest.BackgroundImage = null;
                    }
                    string path = Globals.PATH + "\\crests\\" + clan_crest.ToString() + ".bmp";


                    //Globals.l2net_home.Add_Text("Crest location pre: " + path);
                    pictureBox_clan_crest.BackgroundImage = new System.Drawing.Bitmap(path);
                    //Globals.l2net_home.Add_Text("Crest location: " +path);
                }
                catch (Exception e)
                {
                    pictureBox_clan_crest.BackgroundImage = new System.Drawing.Bitmap(16, 8);
                    Globals.l2net_home.Add_Text("Exception: " + e.Message, Globals.Green, TextType.BOT);
                }
            }
        }


        delegate void ClanInfoUpdate_Callback(ByteBuffer buff);
        public void ClanInfoUpdate(ByteBuffer buff)
        {
            if (this.label_clan_level.InvokeRequired)
            {
                ClanInfoUpdate_Callback d = new ClanInfoUpdate_Callback(ClanInfoUpdate);
                label_clan_level.Invoke(d, new object[] { buff });
                return;
            }

            uint clan_id = buff.ReadUInt32();
            buff.ReadUInt32();
            uint clan_level = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,offset);offset+=4;

            uint clan_castle = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,offset);offset+=4;
            uint clan_hideout = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,offset);offset+=4;
            buff.ReadUInt32();
            int clan_rep = buff.ReadInt32();

            buff.ReadInt32();//0
            buff.ReadInt32();//0

            buff.ReadInt32();//0
            buff.ReadString();//bili
            buff.ReadInt32();//0
            buff.ReadInt32();//0
            if (Globals.gamedata.Chron >= Chronicle.CT3_0)
            {
                buff.ReadInt32();//0
                buff.ReadInt32();//0
                buff.ReadInt32();//0
                buff.ReadInt32();//0
            }

            label_clan_level.Text = clan_level.ToString();
            label_clan_castle.Text = clan_castle.ToString();
            label_clan_hall.Text = clan_hideout.ToString();
            label_clan_rep.Text = clan_rep.ToString();
        }

        delegate void ClanStatusChanged_Callback(ByteBuffer buff);
        public void ClanStatusChanged(ByteBuffer buff)
        {
            if (this.label_clan_level.InvokeRequired)
            {
                ClanStatusChanged_Callback d = new ClanStatusChanged_Callback(ClanStatusChanged);
                label_clan_level.Invoke(d, new object[] { buff });
                return;
            }

            uint leader_id = buff.ReadUInt32();
            uint clan_id = buff.ReadUInt32();
            buff.ReadUInt32();
            uint clan_level = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,offset);offset+=4;

            buff.ReadUInt32();//0
            buff.ReadUInt32();//0
            buff.ReadUInt32();//0

            label_clan_level.Text = clan_level.ToString();
        }


        delegate void Set_MemberInfoDeleteAll_Callback(ByteBuffer buff);
        public void Set_MemberInfoDeleteAll(ByteBuffer buff)
        {
            if (this.label_clan_level.InvokeRequired)
            {
                Set_MemberInfoDeleteAll_Callback d = new Set_MemberInfoDeleteAll_Callback(Set_MemberInfoDeleteAll);
                label_clan_level.Invoke(d, new object[] { buff });
                return;
            }

            listView_char_clan.BeginUpdate();
            //listView_char_clan.ListViewItemSorter = null;

            listView_char_clan.Items.Clear();

            //listView_char_clan.ListViewItemSorter = lvwColumnSorter_clan;
            listView_char_clan.EndUpdate();

            Globals.ClanOnline = 0;
        }

        delegate void Set_MemberInfoDelete_Callback(ByteBuffer buff);
        public void Set_MemberInfoDelete(ByteBuffer buff)
        {
            if (this.label_clan_level.InvokeRequired)
            {
                Set_MemberInfoDelete_Callback d = new Set_MemberInfoDelete_Callback(Set_MemberInfoDelete);
                label_clan_level.Invoke(d, new object[] { buff });
                return;
            }

            string mem_name = buff.ReadString();

            foreach (System.Windows.Forms.ListViewItem obj in Globals.l2net_home.listView_char_clan.Items)
            {
                if (System.String.Equals(obj.SubItems[0].Text, mem_name))
                {
                    if (obj.SubItems[3].Text == "X")
                    {
                        Globals.ClanOnline--;
                    }

                    listView_char_clan.BeginUpdate();
                    //listView_char_clan.ListViewItemSorter = null;

                    listView_char_clan.Items.Remove(obj);

                    //listView_char_clan.ListViewItemSorter = lvwColumnSorter_clan;
                    listView_char_clan.EndUpdate();

                    break;
                }
            }
        }

        delegate void Set_MemberInfo_Callback(ByteBuffer buff, bool update);
        public void Set_MemberInfo(ByteBuffer buff, bool update)
        {
            if (this.label_clan_level.InvokeRequired)
            {
                Set_MemberInfo_Callback d = new Set_MemberInfo_Callback(Set_MemberInfo);
                label_clan_level.Invoke(d, new object[] { buff, update });
                return;
            }

            string mem_name = buff.ReadString();//Util.Get_String(buff,ref offset);
            uint mem_level = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,offset);offset+=4;
            uint mem_class = buff.ReadUInt32();//System.BitConverter.ToUInt32(buff,offset);offset+=4;
            buff.ReadUInt32();//sex
            buff.ReadUInt32();//race
            uint mem_online = buff.ReadUInt32();//online
            if (Globals.gamedata.Chron <= Chronicle.CT2_6)
            {
                buff.ReadUInt32();//pledge type
            }
            if (Globals.gamedata.Chron >= Chronicle.CT3_0)
            {
                buff.ReadUInt32(); //Sponsor
            }

            if (mem_name == "") {
                return;
            }
            bool found = false;
            foreach (System.Windows.Forms.ListViewItem obj in Globals.l2net_home.listView_char_clan.Items)
            {
                if (System.String.Equals(obj.SubItems[0].Text, mem_name))
                {
                    obj.SubItems[1].Text = mem_level.ToString();
                    obj.SubItems[2].Text = Util.GetClass(mem_class);
                    if (mem_online != 0)
                    {
                        if (obj.SubItems[3].Text == "X")
                        {
                        }
                        else
                        {
                            Globals.ClanOnline++;
                            obj.SubItems[3].Text = "X";
                        }
                    }
                    else
                    {
                        if (obj.SubItems[3].Text == " ")
                        {
                        }
                        else
                        {
                            Globals.ClanOnline--;
                            obj.SubItems[3].Text = " ";
                        }
                    }
                    
                    found = true;
                }
            }

            if (!found)
            {
                System.Windows.Forms.ListViewItem ObjListItem = Globals.l2net_home.listView_char_clan.Items.Add(mem_name);
                ObjListItem.SubItems.Add(mem_level.ToString());
                ObjListItem.SubItems.Add(Util.GetClass(mem_class));
                if (mem_online != 0)
                {
                    Globals.ClanOnline++;
                    ObjListItem.SubItems.Add("X");
                }
                else
                {
                    ObjListItem.SubItems.Add(" ");
                }

                if (update)
                {
                    Globals.ClanMembers++;
                }
            }

            if (update)
            {
                label_clan_online.Text = Globals.m_ResourceManager.GetString("col_Online") + ": " + Globals.ClanOnline.ToString() + "/" + Globals.ClanMembers.ToString();
            }
        }
    }//end of class
}
