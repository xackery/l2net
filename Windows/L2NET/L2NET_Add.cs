using System;
using System.Collections.Generic;
using System.Text;

namespace L2_login
{
    public partial class L2NET
    {
        delegate void DeadButtons_Callback(uint town, uint clanhall, uint castle, uint siege, uint fort);
        public void DeadButtons(uint town, uint clanhall, uint castle, uint siege, uint fort)
        {
            if (this.label_clan_level.InvokeRequired)
            {
                DeadButtons_Callback d = new DeadButtons_Callback(DeadButtons);
                label_clan_level.Invoke(d, new object[] { town, clanhall, castle, siege, fort });
                return;
            }

            if (town == 1)
                button_dead_town.Visible = true;
            else
                button_dead_town.Visible = false;
            if (clanhall == 1)
                button_dead_clanhall.Visible = true;
            else
                button_dead_clanhall.Visible = false;
            if (castle == 1)
                button_dead_castle.Visible = true;
            else
                button_dead_castle.Visible = false;
            if (siege == 1)
                button_dead_siege.Visible = true;
            else
                button_dead_siege.Visible = false;
            if (fort == 1)
                button_dead_fort.Visible = true;
            else
                button_dead_fort.Visible = false;
        }

        delegate void Do_Equip_Callback(InventoryInfo inv_inf);
        public void Do_Equip(InventoryInfo inv_inf)
        {
            if (this.panel_inven_lear.InvokeRequired)
            {
                Do_Equip_Callback d = new Do_Equip_Callback(Do_Equip);
                panel_inven_lear.Invoke(d, new object[] { inv_inf });
                return;
            }

            string name = Util.GetItemName(inv_inf.ItemID);
            string description = Util.GetItemDescription(inv_inf.ItemID);
            string path = Util.GetItemImagePath(inv_inf.ItemID);
            bool bmp = System.IO.File.Exists(path);

    		switch(inv_inf.Slot)
			{
                case InventorySlots.Shirt:
                    toolTip1.SetToolTip(panel_inven_shirt, "+" + inv_inf.Enchant.ToString() + " " + name + Environment.NewLine + description);
                    if (panel_inven_shirt.BackgroundImage != null)
                    {
                        panel_inven_shirt.BackgroundImage.Dispose();
                        panel_inven_shirt.BackgroundImage = null;
                    }
                    if (bmp)
                        panel_inven_shirt.BackgroundImage = new System.Drawing.Bitmap(path);
                    break;
                case InventorySlots.Ear:
					if(panel_inven_lear.BackgroundImage == null)
					{
						//load to lear
						toolTip1.SetToolTip(panel_inven_lear,"+"+inv_inf.Enchant.ToString() + " " + name + Environment.NewLine + description);
						if(panel_inven_lear.BackgroundImage != null)
						{
							panel_inven_lear.BackgroundImage.Dispose();
							panel_inven_lear.BackgroundImage = null;
						}
						if(bmp)
                            panel_inven_lear.BackgroundImage = new System.Drawing.Bitmap(path);
					}
					else
					{
						//load to rear
						toolTip1.SetToolTip(panel_inven_rear,"+"+inv_inf.Enchant.ToString() + " " + name + Environment.NewLine + description);
						if(panel_inven_rear.BackgroundImage != null)
						{
							panel_inven_rear.BackgroundImage.Dispose();
							panel_inven_rear.BackgroundImage = null;
						}
						if(bmp)
                            panel_inven_rear.BackgroundImage = new System.Drawing.Bitmap(path);
					}
					break;
                case InventorySlots.Neck:
					toolTip1.SetToolTip(panel_inven_neck,"+"+inv_inf.Enchant.ToString() + " " + name + Environment.NewLine + description);
					if(panel_inven_neck.BackgroundImage != null)
					{
						panel_inven_neck.BackgroundImage.Dispose();
						panel_inven_neck.BackgroundImage = null;
					}
					if(bmp)
                        panel_inven_neck.BackgroundImage = new System.Drawing.Bitmap(path);
					break;
                case InventorySlots.Finger:
					if(panel_inven_lfinger.BackgroundImage == null)
					{
						//load to lear
						toolTip1.SetToolTip(panel_inven_lfinger,"+"+inv_inf.Enchant.ToString() + " " + name + Environment.NewLine + description);
						if(panel_inven_lfinger.BackgroundImage != null)
						{
							panel_inven_lfinger.BackgroundImage.Dispose();
							panel_inven_lfinger.BackgroundImage = null;
						}
						if(bmp)
                            panel_inven_lfinger.BackgroundImage = new System.Drawing.Bitmap(path);
					}
					else
					{
						//load to rear
						toolTip1.SetToolTip(panel_inven_rfinger,"+"+inv_inf.Enchant.ToString() + " " + name + Environment.NewLine + description);
						if(panel_inven_rfinger.BackgroundImage != null)
						{
							panel_inven_rfinger.BackgroundImage.Dispose();
							panel_inven_rfinger.BackgroundImage = null;
						}
						if(bmp)
                            panel_inven_rfinger.BackgroundImage = new System.Drawing.Bitmap(path);
					}
					break;
                case InventorySlots.Head:
					toolTip1.SetToolTip(panel_inven_head,"+"+inv_inf.Enchant.ToString() + " " + name + Environment.NewLine + description);
					if(panel_inven_head.BackgroundImage != null)
					{
						panel_inven_head.BackgroundImage.Dispose();
						panel_inven_head.BackgroundImage = null;
					}
					if(bmp)
                        panel_inven_head.BackgroundImage = new System.Drawing.Bitmap(path);
					break;
                case InventorySlots.RHand:
					toolTip1.SetToolTip(panel_inven_rhand,"+"+inv_inf.Enchant.ToString() + " " + name + Environment.NewLine + description);
					if(panel_inven_rhand.BackgroundImage != null)
					{
						panel_inven_rhand.BackgroundImage.Dispose();
						panel_inven_rhand.BackgroundImage = null;
					}
					if(bmp)
                        panel_inven_rhand.BackgroundImage = new System.Drawing.Bitmap(path);
					break;
                case InventorySlots.LHand:
					toolTip1.SetToolTip(panel_inven_lhand,"+"+inv_inf.Enchant.ToString() + " " + name + Environment.NewLine + description);
					if(panel_inven_lhand.BackgroundImage != null)
					{
						panel_inven_lhand.BackgroundImage.Dispose();
						panel_inven_lhand.BackgroundImage = null;
					}
					if(bmp)
                        panel_inven_lhand.BackgroundImage = new System.Drawing.Bitmap(path);
					break;
                case InventorySlots.Gloves:
					toolTip1.SetToolTip(panel_inven_gloves,"+"+inv_inf.Enchant.ToString() + " " + name + Environment.NewLine + description);
					if(panel_inven_gloves.BackgroundImage != null)
					{
						panel_inven_gloves.BackgroundImage.Dispose();
						panel_inven_gloves.BackgroundImage = null;
					}
					if(bmp)
                        panel_inven_gloves.BackgroundImage = new System.Drawing.Bitmap(path);
					break;
                case InventorySlots.Chest:
					toolTip1.SetToolTip(panel_inven_top,"+"+inv_inf.Enchant.ToString() + " " + name + Environment.NewLine + description);
					if(panel_inven_top.BackgroundImage != null)
					{
						panel_inven_top.BackgroundImage.Dispose();
						panel_inven_top.BackgroundImage = null;
					}
					if(bmp)
                        panel_inven_top.BackgroundImage = new System.Drawing.Bitmap(path);
					break;
                case InventorySlots.Pants:
					toolTip1.SetToolTip(panel_inven_pants,"+"+inv_inf.Enchant.ToString() + " " + name + Environment.NewLine + description);
					if(panel_inven_pants.BackgroundImage != null)
					{
						panel_inven_pants.BackgroundImage.Dispose();
						panel_inven_pants.BackgroundImage = null;
					}
					if(bmp)
                        panel_inven_pants.BackgroundImage = new System.Drawing.Bitmap(path);
					break;
                case InventorySlots.Feet:
					toolTip1.SetToolTip(panel_inven_boots,"+"+inv_inf.Enchant.ToString() + " " + name + Environment.NewLine + description);
					if(panel_inven_boots.BackgroundImage != null)
					{
						panel_inven_boots.BackgroundImage.Dispose();
						panel_inven_boots.BackgroundImage = null;
					}
					if(bmp)
                        panel_inven_boots.BackgroundImage = new System.Drawing.Bitmap(path);
					break;
                case InventorySlots.Dunno:
					break;
                case InventorySlots.LRHand:
					toolTip1.SetToolTip(panel_inven_rhand,"+"+inv_inf.Enchant.ToString() + " " + name + Environment.NewLine + description);
					if(panel_inven_rhand.BackgroundImage != null)
					{
						panel_inven_rhand.BackgroundImage.Dispose();
						panel_inven_rhand.BackgroundImage = null;
					}
					if(bmp)
                        panel_inven_rhand.BackgroundImage = new System.Drawing.Bitmap(path);
					break;
                case InventorySlots.FullBody:
					//just use the top image for now
					toolTip1.SetToolTip(panel_inven_top,"+"+inv_inf.Enchant.ToString() + " " + name + Environment.NewLine + description);
					if(panel_inven_top.BackgroundImage != null)
					{
						panel_inven_top.BackgroundImage.Dispose();
						panel_inven_top.BackgroundImage = null;
					}
					if(bmp)
                        panel_inven_top.BackgroundImage = new System.Drawing.Bitmap(path);
					break;
                case InventorySlots.Accessory:
                    toolTip1.SetToolTip(panel_inven_acc, "+" + inv_inf.Enchant.ToString() + " " + name + Environment.NewLine + description);
                    if (panel_inven_acc.BackgroundImage != null)
                    {
                        panel_inven_acc.BackgroundImage.Dispose();
                        panel_inven_acc.BackgroundImage = null;
                    }
                    if (bmp)
                        panel_inven_acc.BackgroundImage = new System.Drawing.Bitmap(path);
                    break;
            }
		}

        delegate void Clear_Equip2_Callback(InventorySlots slot);
        public void Clear_Equip(InventorySlots slot)
        {
            if (this.panel_inven_lear.InvokeRequired)
            {
                Clear_Equip2_Callback d = new Clear_Equip2_Callback(Clear_Equip);
                panel_inven_lear.Invoke(d, new object[] { slot });
                return;
            }

            switch (slot)
            {
                case InventorySlots.Shirt:
                    toolTip1.SetToolTip(panel_inven_shirt, "");
                    if (panel_inven_shirt.BackgroundImage != null)
                    {
                        panel_inven_shirt.BackgroundImage.Dispose();
                        panel_inven_shirt.BackgroundImage = null;
                    }
                    break;
                case InventorySlots.Ear:
                    break;
                case InventorySlots.Neck:
                    toolTip1.SetToolTip(panel_inven_neck, "");
                    if (panel_inven_neck.BackgroundImage != null)
                    {
                        panel_inven_neck.BackgroundImage.Dispose();
                        panel_inven_neck.BackgroundImage = null;
                    }
                    break;
                case InventorySlots.Finger:
                    break;
                case InventorySlots.Head:
                    toolTip1.SetToolTip(panel_inven_head, "");
                    if (panel_inven_head.BackgroundImage != null)
                    {
                        panel_inven_head.BackgroundImage.Dispose();
                        panel_inven_head.BackgroundImage = null;
                    }
                    break;
                case InventorySlots.RHand:
                    toolTip1.SetToolTip(panel_inven_rhand, "");
                    if (panel_inven_rhand.BackgroundImage != null)
                    {
                        panel_inven_rhand.BackgroundImage.Dispose();
                        panel_inven_rhand.BackgroundImage = null;
                    }
                    break;
                case InventorySlots.LHand:
                    toolTip1.SetToolTip(panel_inven_lhand, "");
                    if (panel_inven_lhand.BackgroundImage != null)
                    {
                        panel_inven_lhand.BackgroundImage.Dispose();
                        panel_inven_lhand.BackgroundImage = null;
                    }
                    break;
                case InventorySlots.Gloves:
                    toolTip1.SetToolTip(panel_inven_gloves, "");
                    if (panel_inven_gloves.BackgroundImage != null)
                    {
                        panel_inven_gloves.BackgroundImage.Dispose();
                        panel_inven_gloves.BackgroundImage = null;
                    }
                    break;
                case InventorySlots.Chest:
                    toolTip1.SetToolTip(panel_inven_top, "");
                    if (panel_inven_top.BackgroundImage != null)
                    {
                        panel_inven_top.BackgroundImage.Dispose();
                        panel_inven_top.BackgroundImage = null;
                    }
                    break;
                case InventorySlots.Pants:
                    toolTip1.SetToolTip(panel_inven_pants, "");
                    if (panel_inven_pants.BackgroundImage != null)
                    {
                        panel_inven_pants.BackgroundImage.Dispose();
                        panel_inven_pants.BackgroundImage = null;
                    }
                    break;
                case InventorySlots.Feet:
                    toolTip1.SetToolTip(panel_inven_boots, "");
                    if (panel_inven_boots.BackgroundImage != null)
                    {
                        panel_inven_boots.BackgroundImage.Dispose();
                        panel_inven_boots.BackgroundImage = null;
                    }
                    break;
                case InventorySlots.Dunno:
                    break;
                case InventorySlots.LRHand:
                    toolTip1.SetToolTip(panel_inven_lhand, "");
                    toolTip1.SetToolTip(panel_inven_rhand, "");
                    if (panel_inven_lhand.BackgroundImage != null)
                    {
                        panel_inven_lhand.BackgroundImage.Dispose();
                        panel_inven_lhand.BackgroundImage = null;
                    }
                    if (panel_inven_rhand.BackgroundImage != null)
                    {
                        panel_inven_rhand.BackgroundImage.Dispose();
                        panel_inven_rhand.BackgroundImage = null;
                    }
                    break;
                case InventorySlots.FullBody:
                    toolTip1.SetToolTip(panel_inven_top, "");
                    toolTip1.SetToolTip(panel_inven_pants, "");
                    if (panel_inven_top.BackgroundImage != null)
                    {
                        panel_inven_top.BackgroundImage.Dispose();
                        panel_inven_top.BackgroundImage = null;
                    }
                    if (panel_inven_pants.BackgroundImage != null)
                    {
                        panel_inven_pants.BackgroundImage.Dispose();
                        panel_inven_pants.BackgroundImage = null;
                    }
                    break;
                case InventorySlots.Accessory:
                    toolTip1.SetToolTip(panel_inven_acc, "");
                    if (panel_inven_acc.BackgroundImage != null)
                    {
                        panel_inven_acc.BackgroundImage.Dispose();
                        panel_inven_acc.BackgroundImage = null;
                    }
                    break;
            }
        }

        delegate void Clear_Equip_Callback();
        public void Clear_Equip()
        {
            if (this.panel_inven_lear.InvokeRequired)
            {
                Clear_Equip_Callback d = new Clear_Equip_Callback(Clear_Equip);
                panel_inven_lear.Invoke(d);
                return;
            }

            if (panel_inven_lear.BackgroundImage != null)
            {
                panel_inven_lear.BackgroundImage.Dispose();
                panel_inven_lear.BackgroundImage = null;
            }
            if (panel_inven_rear.BackgroundImage != null)
            {
                panel_inven_rear.BackgroundImage.Dispose();
                panel_inven_rear.BackgroundImage = null;
            }
            if (panel_inven_neck.BackgroundImage != null)
            {
                panel_inven_neck.BackgroundImage.Dispose();
                panel_inven_neck.BackgroundImage = null;
            }
            if (panel_inven_lfinger.BackgroundImage != null)
            {
                panel_inven_lfinger.BackgroundImage.Dispose();
                panel_inven_lfinger.BackgroundImage = null;
            }
            if (panel_inven_rfinger.BackgroundImage != null)
            {
                panel_inven_rfinger.BackgroundImage.Dispose();
                panel_inven_rfinger.BackgroundImage = null;
            }
            if (panel_inven_head.BackgroundImage != null)
            {
                panel_inven_head.BackgroundImage.Dispose();
                panel_inven_head.BackgroundImage = null;
            }
            if (panel_inven_rhand.BackgroundImage != null)
            {
                panel_inven_rhand.BackgroundImage.Dispose();
                panel_inven_rhand.BackgroundImage = null;
            }
            if (panel_inven_lhand.BackgroundImage != null)
            {
                panel_inven_lhand.BackgroundImage.Dispose();
                panel_inven_lhand.BackgroundImage = null;
            }
            if (panel_inven_gloves.BackgroundImage != null)
            {
                panel_inven_gloves.BackgroundImage.Dispose();
                panel_inven_gloves.BackgroundImage = null;
            }
            if (panel_inven_top.BackgroundImage != null)
            {
                panel_inven_top.BackgroundImage.Dispose();
                panel_inven_top.BackgroundImage = null;
            }
            if (panel_inven_pants.BackgroundImage != null)
            {
                panel_inven_pants.BackgroundImage.Dispose();
                panel_inven_pants.BackgroundImage = null;
            }
            if (panel_inven_boots.BackgroundImage != null)
            {
                panel_inven_boots.BackgroundImage.Dispose();
                panel_inven_boots.BackgroundImage = null;
            }
        }
        
        delegate void Set_HennaTips_Callback();
        public void Set_HennaTips()
        {
            if (this.panel_tat1.InvokeRequired)
            {
                Set_HennaTips_Callback d = new Set_HennaTips_Callback(Set_HennaTips);
                panel_tat1.Invoke(d);
                return;
            }

            try
            {
                if (Globals.gamedata.my_char.Symbol1 != 0)
                {
                    panel_tat1.BackgroundImage = new System.Drawing.Bitmap(Globals.PATH + "\\Icons\\" + ((HennaGroup)Globals.hennagrp[Globals.gamedata.my_char.Symbol1]).Icon + "_0.BMP");
                    toolTip1.SetToolTip(Globals.l2net_home.panel_tat1, ((HennaGroup)Globals.hennagrp[Globals.gamedata.my_char.Symbol1]).Name + " : " + ((HennaGroup)Globals.hennagrp[Globals.gamedata.my_char.Symbol1]).Add_Name);
                }
                else
                {
                    toolTip1.SetToolTip(Globals.l2net_home.panel_tat1, "");
                    panel_tat1.BackgroundImage = null;
                }
            }
            catch
            {
            }

            try
            {
                if (Globals.gamedata.my_char.Symbol2 != 0)
                {
                    panel_tat2.BackgroundImage = new System.Drawing.Bitmap(Globals.PATH + "\\Icons\\" + ((HennaGroup)Globals.hennagrp[Globals.gamedata.my_char.Symbol2]).Icon + "_0.BMP");
                    toolTip1.SetToolTip(Globals.l2net_home.panel_tat2, ((HennaGroup)Globals.hennagrp[Globals.gamedata.my_char.Symbol2]).Name + " +" + ((HennaGroup)Globals.hennagrp[Globals.gamedata.my_char.Symbol2]).Add_Name);
                }
                else
                {
                    toolTip1.SetToolTip(Globals.l2net_home.panel_tat2, "");
                    panel_tat2.BackgroundImage = null;
                }
            }
            catch
            {
            }

            try
            {
                if (Globals.gamedata.my_char.Symbol3 != 0)
                {
                    panel_tat3.BackgroundImage = new System.Drawing.Bitmap(Globals.PATH + "\\Icons\\" + ((HennaGroup)Globals.hennagrp[Globals.gamedata.my_char.Symbol3]).Icon + "_0.BMP");
                    toolTip1.SetToolTip(Globals.l2net_home.panel_tat3, ((HennaGroup)Globals.hennagrp[Globals.gamedata.my_char.Symbol3]).Name + " +" + ((HennaGroup)Globals.hennagrp[Globals.gamedata.my_char.Symbol3]).Add_Name);
                }
                else
                {
                    toolTip1.SetToolTip(Globals.l2net_home.panel_tat3, "");
                    panel_tat3.BackgroundImage = null;
                }
            }
            catch
            {
            }
        }

        delegate void Hide_Dead_Callback();
        public void Hide_Dead()
        {
            if (this.panel_dead.InvokeRequired)
            {
                Hide_Dead_Callback d = new Hide_Dead_Callback(Hide_Dead);
                panel_dead.Invoke(d);
                return;
            }

            this.panel_dead.Hide();
        }

        delegate void Show_Dead_Callback();
        public void Show_Dead()
        {
            if (this.panel_dead.InvokeRequired)
            {
                Show_Dead_Callback d = new Show_Dead_Callback(Show_Dead);
                panel_dead.Invoke(d);
                return;
            }

            this.panel_dead.Show();
            tabControl_char.SelectedIndex = 0;
        }

        delegate void Hide_YesNo_Callback();
        public void Hide_YesNo()
        {
            if (this.panel_yesno.InvokeRequired)
            {
                Hide_YesNo_Callback d = new Hide_YesNo_Callback(Hide_YesNo);
                panel_yesno.Invoke(d);
                return;
            }

            this.panel_yesno.Hide();
        }

        delegate void Show_YesNo_Callback();
        public void Show_YesNo()
        {
            if (this.panel_yesno.InvokeRequired)
            {
                Show_YesNo_Callback d = new Show_YesNo_Callback(Show_YesNo);
                panel_yesno.Invoke(d);
                return;
            }

            this.panel_yesno.Show();
            tabControl_char.SelectedIndex = 0;
        }

        delegate void Set_YesNo_Callback(string text);
        public void Set_YesNo(string text)
        {
            if (this.label_yesno.InvokeRequired)
            {
                Set_YesNo_Callback d = new Set_YesNo_Callback(Set_YesNo);
                label_yesno.Invoke(d, new object[] { text });
                return;
            }

            label_yesno.Text = text;
        }

        delegate void Set_Target_CP_Callback(string text);
        public void Set_Target_CP(string text)
        {
            if (this.label_target_cp.InvokeRequired)
            {
                Set_Target_CP_Callback d = new Set_Target_CP_Callback(Set_Target_CP);
                label_target_cp.Invoke(d, new object[] { text });
                return;
            }

            label_target_cp.Text = text;
        }

        delegate void Set_Target_MP_Callback(string text);
        public void Set_Target_MP(string text)
        {
            if (this.label_target_mp.InvokeRequired)
            {
                Set_Target_MP_Callback d = new Set_Target_MP_Callback(Set_Target_MP);
                label_target_mp.Invoke(d, new object[] { text });
                return;
            }

            label_target_mp.Text = text;
        }

        delegate void Set_Target_HP_Callback(string text);
        public void Set_Target_HP(string text)
        {
            if (this.label_target_hp.InvokeRequired)
            {
                Set_Target_HP_Callback d = new Set_Target_HP_Callback(Set_Target_HP);
                label_target_hp.Invoke(d, new object[] { text });
                return;
            }

            label_target_hp.Text = text;
        }

        delegate void Set_Target_Name_Callback(string name);
        public void Set_Target_Name(string name)
        {
            if (this.label_target_name.InvokeRequired)
            {
                Set_Target_Name_Callback d = new Set_Target_Name_Callback(Set_Target_Name);
                label_target_name.Invoke(d, new object[] { name });
                return;
            }

            label_target_name.Text = name;
        }

        delegate void Set_Char_Info_Basic_Callback();
        public void Set_Char_Info_Basic()
        {
            if (this.label_char_hp.InvokeRequired)
            {
                Set_Char_Info_Basic_Callback d = new Set_Char_Info_Basic_Callback(Set_Char_Info_Basic);
                label_char_hp.Invoke(d);
                return;
            }

            label_char_hp.Text = Globals.gamedata.my_char.Cur_HP.ToString() + "/" + Globals.gamedata.my_char.Max_HP.ToString();
            try
            {
                progressBar_char_HP.Value = System.Convert.ToInt32((Globals.gamedata.my_char.Cur_HP / Globals.gamedata.my_char.Max_HP) * 100);
                progressBar_char_HP.BarText = Globals.gamedata.my_char.Cur_HP.ToString() + "/" + Globals.gamedata.my_char.Max_HP.ToString();
            }
            catch
            {
                progressBar_char_HP.Value = 0;
            }
            label_char_mp.Text = Globals.gamedata.my_char.Cur_MP.ToString() + "/" + Globals.gamedata.my_char.Max_MP.ToString();
            try
            {
                progressBar_char_MP.Value = System.Convert.ToInt32((Globals.gamedata.my_char.Cur_MP / Globals.gamedata.my_char.Max_MP) * 100);
                progressBar_char_MP.BarText = Globals.gamedata.my_char.Cur_MP.ToString() + "/" + Globals.gamedata.my_char.Max_MP.ToString();
            }
            catch
            {
                progressBar_char_MP.Value = 0;
            }
            label_char_cp.Text = Globals.gamedata.my_char.Cur_CP.ToString() + "/" + Globals.gamedata.my_char.Max_CP.ToString();
            try
            {
                progressBar_char_CP.Value = System.Convert.ToInt32((Globals.gamedata.my_char.Cur_CP / Globals.gamedata.my_char.Max_CP) * 100);
                progressBar_char_CP.BarText = Globals.gamedata.my_char.Cur_CP.ToString() + "/" + Globals.gamedata.my_char.Max_CP.ToString();
            }
            catch
            {
                progressBar_char_CP.Value = 0;
            }

            label_info_hp.Text = Globals.gamedata.my_char.Cur_HP.ToString() + "/" + Globals.gamedata.my_char.Max_HP.ToString();
            label_info_mp.Text = Globals.gamedata.my_char.Cur_MP.ToString() + "/" + Globals.gamedata.my_char.Max_MP.ToString();
            label_info_cp.Text = Globals.gamedata.my_char.Cur_CP.ToString() + "/" + Globals.gamedata.my_char.Max_CP.ToString();
            //lets do our xp %
            label_info_xp.Text = Globals.gamedata.my_char.XP.ToString();
            label_char_xp.Text = AddInfo.Get_XP_Percent();
            try
            {
                if (Globals.gamedata.Chron >= Chronicle.CT2_6)
                {
                    progressBar_char_XP.Value = System.Convert.ToInt32(Globals.gamedata.my_char.XPPercent * 100); //AddInfo.Get_XP_Percent_Int();
                    progressBar_char_XP.BarText = Globals.gamedata.my_char.XPPercent.ToString("P", System.Globalization.CultureInfo.InvariantCulture); //AddInfo.Get_XP_Percent();
                }
                else
                {
                    progressBar_char_XP.Value = AddInfo.Get_XP_Percent_Int();
                    progressBar_char_XP.BarText = AddInfo.Get_XP_Percent();
                }
            }
            catch
            {
                progressBar_char_XP.Value = 0;
            }
            //end of xp
            label_info_sp.Text = Globals.gamedata.my_char.SP.ToString();
            label_char_vitality.Text = AddInfo.Get_Vitality_Level();

            label_info_str.Text = Globals.gamedata.my_char.STR.ToString();
            label_info_dex.Text = Globals.gamedata.my_char.DEX.ToString();
            label_info_con.Text = Globals.gamedata.my_char.CON.ToString();
            label_info_int.Text = Globals.gamedata.my_char.INT.ToString();
            label_info_wit.Text = Globals.gamedata.my_char.WIT.ToString();
            label_info_men.Text = Globals.gamedata.my_char.MEN.ToString();

            label_info_patk.Text = Globals.gamedata.my_char.Patk.ToString();
            label_info_pdef.Text = Globals.gamedata.my_char.PDef.ToString();
            label_info_acc.Text = Globals.gamedata.my_char.Accuracy.ToString();
            label_info_crit.Text = Globals.gamedata.my_char.Focus.ToString();
            label_info_atkspd.Text = Globals.gamedata.my_char.PatkSpeed.ToString();
            label_info_matk.Text = Globals.gamedata.my_char.Matk.ToString();
            label_info_mdef.Text = Globals.gamedata.my_char.MDef.ToString();
            label_info_eva.Text = Globals.gamedata.my_char.Evasion.ToString();
            label_info_spd.Text = ((int)(Globals.gamedata.my_char.RunSpeed * Globals.gamedata.my_char.MoveSpeedMult)).ToString();
            label_info_matkspd.Text = Globals.gamedata.my_char.MatkSpeed.ToString();
            label_info_maccuracy.Text = Globals.gamedata.my_char.MAccuracy.ToString();
            label_info_mevasion.Text = Globals.gamedata.my_char.MEvasion.ToString();
            label_info_mcritical.Text = Globals.gamedata.my_char.MCritical.ToString();

            //label_info_load.Text = Globals.gamedata.my_char.Cur_Load.ToString() + "/" + Globals.gamedata.my_char.Max_Load.ToString();
            if (Globals.gamedata.my_char.Max_Load > 0)
            {
                label_info_load.Text = (System.Convert.ToDecimal(Globals.gamedata.my_char.Cur_Load) / System.Convert.ToDecimal(Globals.gamedata.my_char.Max_Load)).ToString("P", System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                label_info_load.Text = Globals.gamedata.my_char.Cur_Load.ToString() + "/" + Globals.gamedata.my_char.Max_Load.ToString();
            }
            label_info_karma.Text = Globals.gamedata.my_char.Karma.ToString();
            label_info_pvp.Text = Globals.gamedata.my_char.PvPCount.ToString() + "/" + Globals.gamedata.my_char.PKCount.ToString();
            label_info_eval.Text = Globals.gamedata.my_char.RecAmount.ToString() + "/" + Globals.gamedata.my_char.RecLeft.ToString();

            //new stuff
            if (Globals.gamedata.Chron >= Chronicle.CT2_3)
            {
                switch (Globals.gamedata.my_char.AtkAttrib)
                {
                    case -2:
                        label_info_atk_attrib.Text = "None";
                        break;
                    case 0:
                        label_info_atk_attrib.Text = "Fire";
                        break;
                    case 1:
                        label_info_atk_attrib.Text = "Water";
                        break;
                    case 2:
                        label_info_atk_attrib.Text = "Wind";
                        break;
                    case 3:
                        label_info_atk_attrib.Text = "Earth";
                        break;
                    case 4:
                        label_info_atk_attrib.Text = "Holy";
                        break;
                    case 5:
                        label_info_atk_attrib.Text = "Dark";
                        break;
                    default:
                        label_info_atk_attrib.Text = "None";
                        break;
                }

                //label_info_atk_attrib.Text = Globals.gamedata.my_char.AtkAttrib.ToString();
                label_info_atk_attrib_value.Text = Globals.gamedata.my_char.AtkAttribVal.ToString();
                label_info_fire.Text = Globals.gamedata.my_char.DefAttribFire.ToString();
                label_info_water.Text = Globals.gamedata.my_char.DefAttribWater.ToString();
                label_info_wind.Text = Globals.gamedata.my_char.DefAttribWind.ToString();
                label_info_earth.Text = Globals.gamedata.my_char.DefAttribEarth.ToString();
                label_info_divinity.Text = Globals.gamedata.my_char.DefAttribHoly.ToString();
                label_info_darkness.Text = Globals.gamedata.my_char.DefAttribUnholy.ToString();
                label_info_fame.Text = Globals.gamedata.my_char.Fame.ToString();
            }
            
            //stats stuff
            DateTime temp = DateTime.Now;
            TimeSpan ts = Globals.start_time - temp;

            //negative values by default, converting to positive
            int hours = ts.Hours * -1;
            int minutes = ts.Minutes * -1;
            int seconds = ts.Seconds * -1;


            ulong tempXPGained = ((ulong)Globals.gamedata.my_char.XP - (ulong)GameData.initial_XP);
            if (tempXPGained > 0)
            {
                //xp stuff
                float xpsec = 0;
                if (hours > 0 || minutes > 0 || seconds > 0)
                {
                    xpsec = ((float)tempXPGained) / (float)(seconds + (minutes * 60) + (hours * 60 * 60));
                }
                float xphour = xpsec * 60 * 60;
                label_XP.Text = xphour.ToString("N0", System.Globalization.CultureInfo.InvariantCulture);
            }


            //sp stuff
            ulong tempSPGained = ((ulong)Globals.gamedata.my_char.SP - (ulong)GameData.initial_SP);
            if(tempSPGained>0)
            {
                float spsec = 0;
                if (hours > 0 || minutes > 0 || seconds > 0)
                {
                    spsec = ((float)tempSPGained) / (float)(seconds + (minutes * 60) + (hours * 60 * 60));
                }
                float sphour = spsec * 60 * 60;
                label_SP.Text = sphour.ToString("N0", System.Globalization.CultureInfo.InvariantCulture);
            }

            //adena stuff
            ulong tempAdenaGained = ((ulong)GameData.current_Adena - (ulong)GameData.initial_Adena);
            if(tempAdenaGained>0)
            {
                ulong tempTotalAdenaGained = tempAdenaGained;
                float adenasec = 0;
                if ((tempAdenaGained > 0 && Globals.gamedata.initial_Adena_Gained_received) && (hours > 0 || minutes > 0 || seconds > 0))
                {
                    adenasec = ((float)tempAdenaGained) / (float)(seconds + (minutes * 60) + (hours * 60 * 60));
                }
                float adenahour = adenasec * 60 * 60;

                if (GameData.current_Adena != 0)
                {
                    label_Adena.Text = adenahour.ToString("N0", System.Globalization.CultureInfo.InvariantCulture);
                    label_adena_total.Text = tempTotalAdenaGained.ToString("N0", System.Globalization.CultureInfo.InvariantCulture);
                }
            }

            label_meshlessignored.Text = GameData.meshless_ignored.ToString();
            label_badmobs.Text = GameData.badmobs_ignored.ToString();

            


            //Globals.l2net_home.Add_Text("char adena:" + tempAdenaGained + " gained " + GameData.current_Adena + " initial " + GameData.initial_Adena, Globals.Gray, TextType.SYSTEM);

        }

        delegate void Set_Char_Info_Callback();
        public void Set_Char_Info()
        {
            //TODO: this gives a weird exception on retail
            if (this.listView_char_data.InvokeRequired)
            {
                Set_Char_Info_Callback d = new Set_Char_Info_Callback(Set_Char_Info);
                label_char_name.Invoke(d);
                return;
            }

            SetName();
            label_char_level.Text = Globals.gamedata.my_char.Level.ToString();
            label_info_level.Text = Globals.gamedata.my_char.Level.ToString();
            label_info_title.Text = Globals.gamedata.my_char.Title;

            //lets do that display list
            listView_char_data.BeginUpdate();

            listView_char_data.Items.Clear();

            System.Windows.Forms.ListViewItem ObjListItem;
            ObjListItem = listView_char_data.Items.Add("Name"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.Name);
            ObjListItem = listView_char_data.Items.Add("Title"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.Title);

            ObjListItem = listView_char_data.Items.Add("X"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.X.ToString());
            ObjListItem = listView_char_data.Items.Add("Y"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.Y.ToString());
            ObjListItem = listView_char_data.Items.Add("Z"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.Z.ToString());

            ObjListItem = listView_char_data.Items.Add("Heading"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.Heading.ToString());
            ObjListItem = listView_char_data.Items.Add("ID"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.ID.ToString());
            ObjListItem = listView_char_data.Items.Add("CharID"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.charID.ToString());
            ObjListItem = listView_char_data.Items.Add("Race"); ObjListItem.SubItems.Add(Util.GetRace(Globals.gamedata.my_char.Race));
            ObjListItem = listView_char_data.Items.Add("Sex"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.Sex.ToString());
            ObjListItem = listView_char_data.Items.Add("Class"); ObjListItem.SubItems.Add(Util.GetClass(Globals.gamedata.my_char.Class));
            ObjListItem = listView_char_data.Items.Add("XP"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.XP.ToString());
            ObjListItem = listView_char_data.Items.Add("STR"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.STR.ToString());
            ObjListItem = listView_char_data.Items.Add("DEX"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.DEX.ToString());
            ObjListItem = listView_char_data.Items.Add("CON"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.CON.ToString());
            ObjListItem = listView_char_data.Items.Add("INT"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.INT.ToString());
            ObjListItem = listView_char_data.Items.Add("WIT"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.WIT.ToString());
            ObjListItem = listView_char_data.Items.Add("MEN"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.MEN.ToString());
            ObjListItem = listView_char_data.Items.Add("Max Hp"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.Max_HP.ToString());
            ObjListItem = listView_char_data.Items.Add("HP"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.Cur_HP.ToString());
            ObjListItem = listView_char_data.Items.Add("Max MP"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.Max_MP.ToString());
            ObjListItem = listView_char_data.Items.Add("MP"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.Cur_MP.ToString());
            ObjListItem = listView_char_data.Items.Add("Max CP"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.Max_CP.ToString());
            ObjListItem = listView_char_data.Items.Add("CP"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.Cur_CP.ToString());
            ObjListItem = listView_char_data.Items.Add("SP"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.SP.ToString());
            ObjListItem = listView_char_data.Items.Add("Current Load"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.Cur_Load.ToString());
            ObjListItem = listView_char_data.Items.Add("Max Load"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.Max_Load.ToString());
            /*ObjListItem = listView_char_data.Items.Add("ObjUnder"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.obj_Under.ToString());
            ObjListItem = listView_char_data.Items.Add("ObjREar"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.obj_REar.ToString());
            ObjListItem = listView_char_data.Items.Add("ObjLEar"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.obj_LEar.ToString());
            ObjListItem = listView_char_data.Items.Add("ObjNeck"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.obj_Neck.ToString());
            ObjListItem = listView_char_data.Items.Add("ObjRFinger"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.obj_RFinger.ToString());
            ObjListItem = listView_char_data.Items.Add("ObjLFinger"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.obj_LFinger.ToString());
            ObjListItem = listView_char_data.Items.Add("ObjHead"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.obj_Head.ToString());
            ObjListItem = listView_char_data.Items.Add("ObjRHand"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.obj_RHand.ToString());
            ObjListItem = listView_char_data.Items.Add("ObjLHand"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.obj_LHand.ToString());
            ObjListItem = listView_char_data.Items.Add("ObjGloves"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.obj_Gloves.ToString());
            ObjListItem = listView_char_data.Items.Add("ObjChest"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.obj_Chest.ToString());
            ObjListItem = listView_char_data.Items.Add("ObjLegs"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.obj_Legs.ToString());
            ObjListItem = listView_char_data.Items.Add("ObjFeet"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.obj_Feet.ToString());
            ObjListItem = listView_char_data.Items.Add("ObjBack"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.obj_Back.ToString());
            ObjListItem = listView_char_data.Items.Add("ObjLRHand"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.obj_LRHand.ToString());
            ObjListItem = listView_char_data.Items.Add("ObjHair"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.obj_Hair.ToString());*/
            ObjListItem = listView_char_data.Items.Add("ItmUnder"); ObjListItem.SubItems.Add(Util.GetItemName(Globals.gamedata.my_char.itm_Under));
            ObjListItem = listView_char_data.Items.Add("ItmREar"); ObjListItem.SubItems.Add(Util.GetItemName(Globals.gamedata.my_char.itm_REar));
            ObjListItem = listView_char_data.Items.Add("ItmLEar"); ObjListItem.SubItems.Add(Util.GetItemName(Globals.gamedata.my_char.itm_LEar));
            ObjListItem = listView_char_data.Items.Add("ItmNeck"); ObjListItem.SubItems.Add(Util.GetItemName(Globals.gamedata.my_char.itm_Neck));
            ObjListItem = listView_char_data.Items.Add("ItmRFinger"); ObjListItem.SubItems.Add(Util.GetItemName(Globals.gamedata.my_char.itm_RFinger));
            ObjListItem = listView_char_data.Items.Add("ItmLFinger"); ObjListItem.SubItems.Add(Util.GetItemName(Globals.gamedata.my_char.itm_LFinger));
            ObjListItem = listView_char_data.Items.Add("ItmHead"); ObjListItem.SubItems.Add(Util.GetItemName(Globals.gamedata.my_char.itm_Head));
            ObjListItem = listView_char_data.Items.Add("ItmRHand"); ObjListItem.SubItems.Add(Util.GetItemName(Globals.gamedata.my_char.itm_RHand));
            ObjListItem = listView_char_data.Items.Add("ItmLHand"); ObjListItem.SubItems.Add(Util.GetItemName(Globals.gamedata.my_char.itm_LHand));
            ObjListItem = listView_char_data.Items.Add("ItmGloves"); ObjListItem.SubItems.Add(Util.GetItemName(Globals.gamedata.my_char.itm_Gloves));
            ObjListItem = listView_char_data.Items.Add("ItmChest"); ObjListItem.SubItems.Add(Util.GetItemName(Globals.gamedata.my_char.itm_Chest));
            ObjListItem = listView_char_data.Items.Add("ItmLegs"); ObjListItem.SubItems.Add(Util.GetItemName(Globals.gamedata.my_char.itm_Legs));
            ObjListItem = listView_char_data.Items.Add("ItmFeet"); ObjListItem.SubItems.Add(Util.GetItemName(Globals.gamedata.my_char.itm_Feet));
            ObjListItem = listView_char_data.Items.Add("ItmBack"); ObjListItem.SubItems.Add(Util.GetItemName(Globals.gamedata.my_char.itm_Back));
            ObjListItem = listView_char_data.Items.Add("ItmLRHand"); ObjListItem.SubItems.Add(Util.GetItemName(Globals.gamedata.my_char.itm_LRHand));
            ObjListItem = listView_char_data.Items.Add("ItmHair"); ObjListItem.SubItems.Add(Util.GetItemName(Globals.gamedata.my_char.itm_Hair));
            ObjListItem = listView_char_data.Items.Add("Patk"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.Patk.ToString());
            ObjListItem = listView_char_data.Items.Add("Patkspeed"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.PatkSpeed.ToString());
            ObjListItem = listView_char_data.Items.Add("Pdef"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.PDef.ToString());
            ObjListItem = listView_char_data.Items.Add("Evasion"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.Evasion.ToString());
            ObjListItem = listView_char_data.Items.Add("Accuracy"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.Accuracy.ToString());
            ObjListItem = listView_char_data.Items.Add("Focus"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.Focus.ToString());
            ObjListItem = listView_char_data.Items.Add("Matk"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.Matk.ToString());
            ObjListItem = listView_char_data.Items.Add("Matkspeed"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.MatkSpeed.ToString());
            ObjListItem = listView_char_data.Items.Add("Mdef"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.MDef.ToString());
            ObjListItem = listView_char_data.Items.Add("pvpflag"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.PvPFlag.ToString());
            ObjListItem = listView_char_data.Items.Add("karma"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.Karma.ToString());
            ObjListItem = listView_char_data.Items.Add("RunSpeed"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.RunSpeed.ToString());
            ObjListItem = listView_char_data.Items.Add("WalkSpeed"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.WalkSpeed.ToString());
            ObjListItem = listView_char_data.Items.Add("SwimRunSpeed"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.SwimRunSpeed.ToString());
            ObjListItem = listView_char_data.Items.Add("SwimWalkSpeed"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.SwimWalkSpeed.ToString());
            ObjListItem = listView_char_data.Items.Add("flRunSpeed"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.flRunSpeed.ToString());
            ObjListItem = listView_char_data.Items.Add("flWalkSpeed"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.flWalkSpeed.ToString());
            ObjListItem = listView_char_data.Items.Add("FlyRunSpeed"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.FlyRunSpeed.ToString());
            ObjListItem = listView_char_data.Items.Add("FlyWalkSpeed"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.FlyWalkSpeed.ToString());
            ObjListItem = listView_char_data.Items.Add("MoveMultiplier"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.MoveSpeedMult.ToString());
            ObjListItem = listView_char_data.Items.Add("AttackMultiplier"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.AttackSpeedMult.ToString());
            ObjListItem = listView_char_data.Items.Add("Coll Radius"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.CollisionRadius.ToString());
            ObjListItem = listView_char_data.Items.Add("Coll Height"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.CollisionHeight.ToString());
            ObjListItem = listView_char_data.Items.Add("HairStyle"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.HairSytle.ToString());
            ObjListItem = listView_char_data.Items.Add("HairColor"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.HairColor.ToString());
            ObjListItem = listView_char_data.Items.Add("Face"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.Face.ToString());
            ObjListItem = listView_char_data.Items.Add("AccessLevel"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.AccessLevel.ToString());
            ObjListItem = listView_char_data.Items.Add("ClanID"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.ClanID.ToString());
            ObjListItem = listView_char_data.Items.Add("ClanCrestID"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.ClanCrestID.ToString());
            ObjListItem = listView_char_data.Items.Add("AllyID"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.AllyID.ToString());
            ObjListItem = listView_char_data.Items.Add("AllyCrestID"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.AllyCrestID.ToString());
            ObjListItem = listView_char_data.Items.Add("isClanLeader"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.isClanLeader.ToString());
            ObjListItem = listView_char_data.Items.Add("Mount"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.MountType.ToString());
            ObjListItem = listView_char_data.Items.Add("StoreType"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.PrivateStoreType.ToString());
            ObjListItem = listView_char_data.Items.Add("DwarfCraft"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.hasDwarfCraft.ToString());
            ObjListItem = listView_char_data.Items.Add("PKCount"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.PKCount.ToString());
            ObjListItem = listView_char_data.Items.Add("PvPCount"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.PvPCount.ToString());
            ObjListItem = listView_char_data.Items.Add("CubicCount"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.CubicCount.ToString());
            ObjListItem = listView_char_data.Items.Add("LFG"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.FindParty.ToString());
            ObjListItem = listView_char_data.Items.Add("AbnormalEffects"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.AbnormalEffects.ToString());
            ObjListItem = listView_char_data.Items.Add("Swimming"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.isRunning.ToString());
            ObjListItem = listView_char_data.Items.Add("RecLeft"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.RecLeft.ToString());
            ObjListItem = listView_char_data.Items.Add("RecAmount"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.RecAmount.ToString());
            ObjListItem = listView_char_data.Items.Add("InventoryLimit"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.InventoryLimit.ToString());
            ObjListItem = listView_char_data.Items.Add("Enchant"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.EnchantAmount.ToString());
            ObjListItem = listView_char_data.Items.Add("TeamCircle"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.TeamCircle.ToString());
            ObjListItem = listView_char_data.Items.Add("ClanCrestLargeID"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.ClanCrestIDLarge.ToString());
            ObjListItem = listView_char_data.Items.Add("HeroIcon"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.HeroIcon.ToString());
            ObjListItem = listView_char_data.Items.Add("HeroAura"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.HeroGlow.ToString());
            ObjListItem = listView_char_data.Items.Add("Fishing"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.isFishing.ToString());
            ObjListItem = listView_char_data.Items.Add("FishX"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.FishX.ToString());
            ObjListItem = listView_char_data.Items.Add("FishY"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.FishY.ToString());
            ObjListItem = listView_char_data.Items.Add("FishZ"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.FishZ.ToString());
            ObjListItem = listView_char_data.Items.Add("NameColor"); ObjListItem.SubItems.Add(Globals.gamedata.my_char.NameColor.ToString());

            listView_char_data.EndUpdate();
        }
    }
}
