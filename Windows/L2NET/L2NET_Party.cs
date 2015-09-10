using System;
using System.Collections.Generic;
using System.Text;

namespace L2_login
{
    public partial class L2NET
    {
        delegate void Set_PartyInfo_Callback();
        public void Set_PartyInfo()
        {
            if (this.label_1_name.InvokeRequired)
            {
                Set_PartyInfo_Callback d = new Set_PartyInfo_Callback(Set_PartyInfo);
                label_1_name.Invoke(d);
                return;
            }

            Globals.PartyLock.EnterReadLock();
            try
            {
                int i = 0;

                foreach (PartyMember pmem in Globals.gamedata.PartyMembers.Values)
                {
                    switch (i)
                    {
                        case 0:
                            label_1_name.Text = pmem.Name + " :" + pmem.Level.ToString();
                            /*if (Globals.gamedata.Chron >= Chronicle.CT2_4 && Globals.gamedata.Official_Server)
                            {
                                label_1_hp.Text = ("HP: " + pmem.Cur_HP / 100000).ToString() + "%";
                            }
                            else
                            {*/
                                label_1_hp.Text = "HP: " + pmem.Cur_HP.ToString() + "/" + pmem.Max_HP.ToString();
                            //}
                            label_1_mp.Text = "MP: " + pmem.Cur_MP.ToString() + "/" + pmem.Max_MP.ToString();
                            label_1_cp.Text = "CP: " + pmem.Cur_CP.ToString() + "/" + pmem.Max_CP.ToString();
                            break;
                        case 1:
                            label_2_name.Text = pmem.Name + " :" + pmem.Level.ToString();
                            /*if (Globals.gamedata.Chron >= Chronicle.CT2_4 && Globals.gamedata.Official_Server)
                            {
                                label_2_hp.Text = ("HP: " + pmem.Cur_HP / 100000).ToString() + "%";
                            }
                            else
                            {*/
                                label_2_hp.Text = "HP: " + pmem.Cur_HP.ToString() + "/" + pmem.Max_HP.ToString();
                            //}
                            label_2_mp.Text = "MP: " + pmem.Cur_MP.ToString() + "/" + pmem.Max_MP.ToString();
                            label_2_cp.Text = "CP: " + pmem.Cur_CP.ToString() + "/" + pmem.Max_CP.ToString();
                            break;
                        case 2:
                            label_3_name.Text = pmem.Name + " :" + pmem.Level.ToString();
                            /*if (Globals.gamedata.Chron >= Chronicle.CT2_4 && Globals.gamedata.Official_Server)
                            {
                                label_3_hp.Text = ("HP: " + pmem.Cur_HP / 100000).ToString() + "%";
                            }
                            else
                            {*/
                                label_3_hp.Text = "HP: " + pmem.Cur_HP.ToString() + "/" + pmem.Max_HP.ToString();
                            //}
                            label_3_mp.Text = "MP: " + pmem.Cur_MP.ToString() + "/" + pmem.Max_MP.ToString();
                            label_3_cp.Text = "CP: " + pmem.Cur_CP.ToString() + "/" + pmem.Max_CP.ToString();
                            break;
                        case 3:
                            label_4_name.Text = pmem.Name + " :" + pmem.Level.ToString();
                            /*if (Globals.gamedata.Chron >= Chronicle.CT2_4 && Globals.gamedata.Official_Server)
                            {
                                label_4_hp.Text = ("HP: " + pmem.Cur_HP / 100000).ToString() + "%";
                            }
                            else
                            {*/
                                label_4_hp.Text = "HP: " + pmem.Cur_HP.ToString() + "/" + pmem.Max_HP.ToString();
                            //}
                            label_4_mp.Text = "MP: " + pmem.Cur_MP.ToString() + "/" + pmem.Max_MP.ToString();
                            label_4_cp.Text = "CP: " + pmem.Cur_CP.ToString() + "/" + pmem.Max_CP.ToString();
                            break;
                        case 4:
                            label_5_name.Text = pmem.Name + " :" + pmem.Level.ToString();
                            /*if (Globals.gamedata.Chron >= Chronicle.CT2_4 && Globals.gamedata.Official_Server)
                            {
                                label_5_hp.Text = ("HP: " + pmem.Cur_HP / 100000).ToString() + "%";
                            }
                            else
                            {*/
                                label_5_hp.Text = "HP: " + pmem.Cur_HP.ToString() + "/" + pmem.Max_HP.ToString();
                            //}
                            label_5_mp.Text = "MP: " + pmem.Cur_MP.ToString() + "/" + pmem.Max_MP.ToString();
                            label_5_cp.Text = "CP: " + pmem.Cur_CP.ToString() + "/" + pmem.Max_CP.ToString();
                            break;
                        case 5:
                            label_6_name.Text = pmem.Name + " :" + pmem.Level.ToString();
                            /*if (Globals.gamedata.Chron >= Chronicle.CT2_4 && Globals.gamedata.Official_Server)
                            {
                                label_6_hp.Text = ("HP: " + pmem.Cur_HP / 100000).ToString() + "%";
                            }
                            else
                            {*/
                                label_6_hp.Text = "HP: " + pmem.Cur_HP.ToString() + "/" + pmem.Max_HP.ToString();
                            //}
                            label_6_mp.Text = "MP: " + pmem.Cur_MP.ToString() + "/" + pmem.Max_MP.ToString();
                            label_6_cp.Text = "CP: " + pmem.Cur_CP.ToString() + "/" + pmem.Max_CP.ToString();
                            break;
                        case 6:
                            label_7_name.Text = pmem.Name + " :" + pmem.Level.ToString();
                            /*if (Globals.gamedata.Chron >= Chronicle.CT2_4 && Globals.gamedata.Official_Server)
                            {
                                label_7_hp.Text = ("HP: " + pmem.Cur_HP / 100000).ToString() + "%";
                            }
                            else
                            {*/
                                label_7_hp.Text = "HP: " + pmem.Cur_HP.ToString() + "/" + pmem.Max_HP.ToString();
                            //}
                            label_7_mp.Text = "MP: " + pmem.Cur_MP.ToString() + "/" + pmem.Max_MP.ToString();
                            label_7_cp.Text = "CP: " + pmem.Cur_CP.ToString() + "/" + pmem.Max_CP.ToString();
                            break;
                        case 7:
                            label_8_name.Text = pmem.Name + " :" + pmem.Level.ToString();
                            /*if (Globals.gamedata.Chron >= Chronicle.CT2_4 && Globals.gamedata.Official_Server)
                            {
                                label_8_hp.Text = ("HP: " + pmem.Cur_HP / 100000).ToString() + "%";
                            }
                            else
                            {*/
                                label_8_hp.Text = "HP: " + pmem.Cur_HP.ToString() + "/" + pmem.Max_HP.ToString();
                            //}
                            label_8_mp.Text = "MP: " + pmem.Cur_MP.ToString() + "/" + pmem.Max_MP.ToString();
                            label_8_cp.Text = "CP: " + pmem.Cur_CP.ToString() + "/" + pmem.Max_CP.ToString();
                            break;
                    }

                    i++;
                }
            }
            finally
            {
                Globals.PartyLock.ExitReadLock();
            }
        }

        delegate void Set_PartyVisible_Callback();
        public void Set_PartyVisible()
        {
            if (this.panel_party_1.InvokeRequired)
            {
                Set_PartyVisible_Callback d = new Set_PartyVisible_Callback(Set_PartyVisible);
                panel_party_1.Invoke(d);
                return;
            }

            uint pcount = 0;

            Globals.PartyLock.EnterReadLock();
            try
            {
                pcount = Globals.gamedata.PartyCount;
            }
            finally
            {
                Globals.PartyLock.ExitReadLock();
            }

            if (pcount > 0)
                panel_party_1.BringToFront();
            else
                panel_party_1.SendToBack();

            if (pcount > 1)
                panel_party_2.BringToFront();
            else
                panel_party_2.SendToBack();

            if (pcount > 2)
                panel_party_3.BringToFront();
            else
                panel_party_3.SendToBack();

            if (pcount > 3)
                panel_party_4.BringToFront();
            else
                panel_party_4.SendToBack();

            if (pcount > 4)
                panel_party_5.BringToFront();
            else
                panel_party_5.SendToBack();

            if (pcount > 5)
                panel_party_6.BringToFront();
            else
                panel_party_6.SendToBack();

            if (pcount > 6)
                panel_party_7.BringToFront();
            else
                panel_party_7.SendToBack();

            if (pcount > 7)
                panel_party_8.BringToFront();
            else
                panel_party_8.SendToBack();
        }
    }
}
