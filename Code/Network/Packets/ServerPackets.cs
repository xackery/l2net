using System;

namespace L2_login
{
    /// <summary>
    /// Summary description for GamePackets.
    /// </summary>
    public class ServerPackets
    {
        public ServerPackets()
        {
        }

        public static void SelectChar(int id)
        {
            ByteBuffer buff = new ByteBuffer(19);

            buff.WriteByte((byte)PClient.CharacterSelect);
            buff.WriteInt32(id);
            buff.WriteUInt16(0);
            buff.WriteUInt32(0);
            buff.WriteUInt32(0);
            buff.WriteUInt32(0);

            Globals.gamedata.SendToGameServer(buff);
        }
        public static void DeleteChar(int id)
        {
            ByteBuffer buff = new ByteBuffer(19);

            buff.WriteByte((byte)PClient.CharacterDelete);
            buff.WriteInt32(id);
            buff.WriteUInt16(0);
            buff.WriteUInt32(0);
            buff.WriteUInt32(0);
            buff.WriteUInt32(0);

            Globals.gamedata.SendToGameServer(buff);
        }

        public static void DeleteShortCut(int slot)
        {
            ByteBuffer bbuff = new ByteBuffer(5);

            bbuff.WriteByte((byte)PClient.RequestShortCutDel);
            bbuff.WriteInt32(slot);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void RegisterShortCut(int type, uint slot, uint id, uint other)
        {
            ByteBuffer bbuff = new ByteBuffer(21);

            bbuff.WriteByte((byte)PClient.RequestShortCutReg);
            bbuff.WriteInt32(type);
            bbuff.WriteUInt32(slot);
            bbuff.WriteUInt32(id);
            bbuff.WriteUInt32(other);
            bbuff.WriteUInt32(1);//dunno why

            //TODO - wtf is the other?
            //Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Force_Attack(uint id, int x, int y, int z, bool shift)
        {
            ByteBuffer bbuff = new ByteBuffer(18);

            bbuff.WriteByte((byte)PClient.AttackRequest);//force attack request that bitch
            bbuff.WriteUInt32(id);
            bbuff.WriteInt32(x);
            bbuff.WriteInt32(y);
            bbuff.WriteInt32(z);
            if (shift)
                bbuff.WriteByte(1);
            else
                bbuff.WriteByte(0);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Use_ShortCut(int id, bool control, bool shift)
        {
            bool is_skill = true;
            Use_ShortCut(id, control, shift, ref is_skill);
        }

        public static void Use_ShortCut(int id, bool control, bool shift, ref bool is_skill)
        {
            try
            {
                is_skill = false;

                ShortCut sc = ((ShortCut)Globals.gamedata.ShortCuts[id]);

                switch (sc.Type)
                {
                    case ShortCut_Types.ITEM:
                        ServerPackets.Use_Item(sc.ID);
                        break;
                    case ShortCut_Types.SKILL:
                        ServerPackets.Try_Use_Skill(sc.ID, control, shift);
                        is_skill = true;
                        break;
                    case ShortCut_Types.ACTION:
                        ServerPackets.Use_Action_Parse(sc.ID, control, shift);
                        break;
                    case ShortCut_Types.MACRO:
                        //um... do we handle this? or the server?
                        break;
                    case ShortCut_Types.RECIPE:
                        //durka durke
                        break;
                    default:
                        //dunno what this is
                        break;
                }
            }
            catch
            {
                //shortcut broke...meh
            }
        }

        public static void Use_Action_Parse(uint id, bool control, bool shift)
        {
            Actions act = ((Actions)Globals.actionlist[id]);

            switch ((PClientAction)act.ID)
            {
                case PClientAction.SitStand:
                    Use_Action_Internal((int)PClientAction.SitStand, control, shift);
                    break;
                case PClientAction.RunWalk:
                    Use_Action_Internal((int)PClientAction.RunWalk, control, shift);
                    break;
                case PClientAction.Attack://2
                    ClickOBJ(id, control, shift);
                    break;
                case PClientAction.Trade://3
                    break;
                case PClientAction.NextTarget://4
                    break;
                case PClientAction.PickUp://5
                    Globals.scriptthread.Script_CLICK_NEAREST_ITEM();
                    break;
                case PClientAction.Assist://6
                    ServerPackets.Assist();
                    break;
                case PClientAction.Invite://7
                    ServerPackets.Command_Invite(Util.GetCharName(Globals.gamedata.my_char.TargetID));
                    break;
                case PClientAction.LeaveParty://8
                    ServerPackets.Command_Leave();
                    break;
                case PClientAction.DismissPartyMember://9
                    ServerPackets.Command_Dismiss(Util.GetCharName(Globals.gamedata.my_char.TargetID));
                    break;
                case PClientAction.Open_PrivateStore_Sell://10
                    //Use_Action_Parse((int)PClientAction.Open_PrivateStore_Sell, control, shift);
                    break;
                case PClientAction.PartyMatching://11
                    break;
                case PClientAction.Social_Greeting://12
                    ServerPackets.Command_SocialHello();
                    break;
                case PClientAction.Social_Victory://13
                    ServerPackets.Command_SocialVictory();
                    break;
                case PClientAction.Social_Advance://14
                    ServerPackets.Command_SocialCharge();
                    break;
                case PClientAction.Pet_RunWalk://15
                    Use_Action_Internal(15, control, shift);
                    break;
                case PClientAction.Pet_Attack://16
                    Use_Action_Internal(16, control, shift);
                    break;
                case PClientAction.Pet_Stop://17
                    Use_Action_Internal(17, control, shift);
                    break;
                case PClientAction.Pet_PickUp://18
                    break;
                case PClientAction.Pet_Unsummon://19
                    Use_Action_Internal(19, control, shift);
                    break;
                case PClientAction.Pet_Special://20
                    break;
                case PClientAction.Summon_RunWalk://21
                    Use_Action_Internal(21, control, shift);
                    break;
                case PClientAction.Summon_Attack://22
                    Use_Action_Internal(22, control, shift);
                    break;
                case PClientAction.Summon_Stop://23
                    Use_Action_Internal(23, control, shift);
                    break;
                case PClientAction.Social_Yes://24
                    ServerPackets.Command_SocialYes();
                    break;
                case PClientAction.Social_No://25
                    ServerPackets.Command_SocialNo();
                    break;
                case PClientAction.Social_Bow://26
                    ServerPackets.Command_SocialBow();
                    break;
                case PClientAction.Summon_Special://27
                    break;
                case PClientAction.Open_PrivateStore_Buy://28
                    Use_Action_Parse((int)PClientAction.Open_PrivateStore_Buy, control, shift);
                    break;
                case PClientAction.Social_Unaware://29
                    ServerPackets.Command_SocialUnaware();
                    break;
                case PClientAction.Social_Waiting://30
                    ServerPackets.Command_SocialWaiting();
                    break;
                case PClientAction.Social_Laugh://31
                    ServerPackets.Command_SocialLaugh();
                    break;
                case PClientAction.SwitchMode://32
                    Use_Action_Internal(32, control, shift);
                    break;
                case PClientAction.Social_Applaud://33
                    ServerPackets.Command_SocialApplause();
                    break;
                case PClientAction.Social_Dance://34
                    ServerPackets.Command_SocialDance();
                    break;
                case PClientAction.Social_Sorrow://35
                    ServerPackets.Command_SocialSad();
                    break;
                case PClientAction.ToxicSmoke://36
                    Use_Action_Internal(36, control, shift);
                    break;
                case PClientAction.Open_DwarvenManufacture://37
                    Use_Action_Internal(37, control, shift);
                    break;
                case PClientAction.Mount://38
                    Use_Action_Internal(38, control, shift);
                    break;
                case PClientAction.ParasiteBurst://39
                    Use_Action_Internal(39, control, shift);
                    break;
                case PClientAction.Evaluate://40
                    ServerPackets.Command_Evaluate(Globals.gamedata.my_char.TargetID);
                    break;
                case PClientAction.WildCannon://41
                    Use_Action_Internal(41, control, shift);
                    break;
                case PClientAction.SelfDamageShield://42
                    Use_Action_Internal(42, control, shift);
                    break;
                case PClientAction.HydroScrew://43
                    Use_Action_Internal(43, control, shift);
                    break;
                case PClientAction.BoomAttack://44
                    Use_Action_Internal(44, control, shift);
                    break;
                case PClientAction.MasterRecharge://45
                    Use_Action_Internal(45, control, shift);
                    break;
                case PClientAction.MegaStormStrike://46
                    Use_Action_Internal(46, control, shift);
                    break;
                case PClientAction.StealBlood://47
                    Use_Action_Internal(47, control, shift);
                    break;
                case PClientAction.MechCannon://48
                    Use_Action_Internal(48, control, shift);
                    break;
                case PClientAction.WildTemper://49
                    Use_Action_Internal(49, control, shift);
                    break;
                case PClientAction.ChangePartyLeader://50
                    ServerPackets.Command_ChangePartyLeader(Util.GetCharName(Globals.gamedata.my_char.TargetID));
                    break;
                case PClientAction.Open_GeneralManufacture://51
                    Use_Action_Internal(51, control, shift);
                    break;
                case PClientAction.Summon_Unsummon://52
                    Use_Action_Internal(52, control, shift);
                    break;
                case PClientAction.Summon_Move://53
                    Use_Action_Internal(53, control, shift);
                    break;
                case PClientAction.Pet_Move://54
                    Use_Action_Internal(54, control, shift);
                    break;
                case PClientAction.Record://55
                    break;
                case PClientAction.CommandChannelInvite://56
                    break;
                case PClientAction.FindStore://57
                    break;
                case PClientAction.Duel://58
                    break;
                case PClientAction.Withdraw://59
                    break;
                case PClientAction.PartyDuel://60
                    break;
                case PClientAction.Open_PackageSale://61
                    Use_Action_Internal(61, control, shift);
                    break;
                case PClientAction.Social_Charm://62
                    ServerPackets.Command_SocialCharm();
                    break;
                case PClientAction.MiniGame://63
                    break;
                case PClientAction.MyTeleports://64
                    break;
                case PClientAction.BotReport://65
                    break;
                case PClientAction.Social_Shyness://66
                    ServerPackets.Command_SocialShyness();
                    break;
                case PClientAction.Steer://67
                    break;
                case PClientAction.CancelControl://68
                    break;
                case PClientAction.DestinationMap://69
                    break;
                case PClientAction.ExitAirship://70
                    break;
                default:
                    Use_Action_Internal(act.ID, control, shift);
                    break;
            }
        }

        private static void Use_Action_Internal(uint id, bool control, bool shift)
        {
            ByteBuffer bbuff = new ByteBuffer(10);

            bbuff.WriteByte((byte)PClient.RequestActionUse);
            bbuff.WriteUInt32(id);
            if (control)
                bbuff.WriteUInt32(1);
            else
                bbuff.WriteUInt32(0);

            if (shift)
                bbuff.WriteByte(1);
            else
                bbuff.WriteByte(0);

            Globals.gamedata.SendToGameServer(bbuff);

            //open private craft shop = 25 0 0 0
            //open general craft shop = 33 0 0 0
        }

        public static void Abort_CraftShop()
        {
            ByteBuffer bbuff = new ByteBuffer(1);

            bbuff.WriteByte((byte)PClient.RequestRecipeShopManageQuit);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Abort_GeneralCraftShop()
        {
            ByteBuffer bbuff = new ByteBuffer(1);

            bbuff.WriteByte((byte)PClient.RequestRecipeShopManageQuit);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Abort_BuyShop()
        {
            ByteBuffer bbuff = new ByteBuffer(1);

            bbuff.WriteByte((byte)PClient.RequestPrivateStoreQuitBuy);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Abort_SellShop()
        {
            ByteBuffer bbuff = new ByteBuffer(1);

            bbuff.WriteByte((byte)PClient.RequestPrivateStoreQuitSell);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Return(int b)
        {
            //0 - town
            //1 - clan hall
            //2 - castle
            //3 - siege hq
            //4 - fortress
            ByteBuffer bbuff = new ByteBuffer(5);

            bbuff.WriteByte((byte)PClient.RequestRestartPoint);
            bbuff.WriteInt32(b);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Target(string name)
        {
            ServerPackets.ClickNearest(name);
        }

        public static void Target(uint id, int x, int y, int z, bool shift)
        {
            ByteBuffer bbuff = new ByteBuffer(18);

            bbuff.WriteByte((byte)PClient.Action);//action request-no need to attack oneself
            bbuff.WriteUInt32(id);
            bbuff.WriteInt32(x);
            bbuff.WriteInt32(y);
            bbuff.WriteInt32(z);
            if (shift)
                bbuff.WriteByte(1);
            else
                bbuff.WriteByte(0);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void MoveToPacket(int dx, int dy)
        {
            ServerPackets.MoveToPacket(dx, dy, Util.Float_Int32(Globals.gamedata.my_char.Z));
        }

        public static void MoveToPacket(int dx, int dy, int dz)
        {

            //Globals.l2net_home.Add_Text("I'm not attacking something (moving)", Globals.Green, TextType.BOT);
            Globals.gamedata.my_char.isAttacking = false; //char is not attacking if moving somewhere

            ByteBuffer bbuff = new ByteBuffer(29);

            bbuff.WriteByte((byte)PClient.MoveBackwardToLocation);
            bbuff.WriteInt32(dx);
            bbuff.WriteInt32(dy);
            bbuff.WriteInt32(dz);
            bbuff.WriteInt32(Util.Float_Int32(Globals.gamedata.my_char.X));
            bbuff.WriteInt32(Util.Float_Int32(Globals.gamedata.my_char.Y));
            bbuff.WriteInt32(Util.Float_Int32(Globals.gamedata.my_char.Z));
            bbuff.WriteInt32(1);//1 - mouse | 0 - keyboard

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Send_Verify()
        {
            ByteBuffer bbuff = new ByteBuffer(21);

            bbuff.WriteByte((byte)PClient.ValidatePosition);
            bbuff.WriteInt32(Util.Float_Int32(Globals.gamedata.my_char.X));
            bbuff.WriteInt32(Util.Float_Int32(Globals.gamedata.my_char.Y));
            bbuff.WriteInt32(Util.Float_Int32(Globals.gamedata.my_char.Z));
            bbuff.WriteInt32(0);//heading
            bbuff.WriteInt32(0);//other

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Use_Skill(string name)
        {
            //gotta find the id of the skill with this name... fun fun

            //TODO
        }

        public static void Use_Skill(uint id)
        {
            ServerPackets.Use_Skill(id, false, false);
        }

        public static void RequestDispel(uint object_id, uint skill_id, uint skill_level)
        {
            if (Globals.gamedata.my_char.Cur_HP > 0)
            {  
                //          object_id    skill_id     skill_level
                // D0 4B 00 3F 2E 01 4E  2B 05 00 00  01 00 00 00
                ByteBuffer bbuff = new ByteBuffer(15);
                bbuff.WriteByte((byte)PClient.EXPacket);
                bbuff.WriteUInt16((byte)PClientEX.RequestDispel);
                bbuff.WriteUInt32(object_id);
                bbuff.WriteUInt32(skill_id);
                bbuff.WriteUInt32(skill_level);

                Globals.gamedata.SendToGameServer(bbuff);
            }
        }

        public static void Use_Skill(uint id, bool control, bool shift)
        {
            if (Globals.gamedata.my_char.Cur_HP > 0)
            {
                Globals.gamedata.my_char.ExpiresTime = 0;
                Globals.gamedata.my_char.Resisted = 0;

                ByteBuffer bbuff = new ByteBuffer(10);

                bbuff.WriteByte((byte)PClient.RequestMagicSkillUse);
                bbuff.WriteUInt32(id);

                if (control)
                    bbuff.WriteUInt32(1);
                else
                    bbuff.WriteUInt32(0);

                if (shift)
                    bbuff.WriteByte(1);
                else
                    bbuff.WriteByte(0);

                Globals.gamedata.SendToGameServer(bbuff);
            }
        }

        public static void Try_Use_Skill(uint id, bool control, bool shift)
        {
            if (Globals.gamedata.my_char.Cur_HP > 0)
            {
                Globals.SkillListLock.EnterReadLock();
                try
                {
                    if (Globals.gamedata.skills.ContainsKey(id))
                    {
                        UserSkill sk = (UserSkill)Globals.gamedata.skills[id];

                        if (sk.IsReady())//sk.Time
                        {
                            ServerPackets.Use_Skill(sk.ID, control, shift);
                            sk.LastTime = System.DateTime.Now;
                            sk.NextTime = System.DateTime.Now.AddMilliseconds(Globals.SKILL_MIN_REUSE);
                        }
                        else
                        {
                            long msec = (sk.NextTime.Ticks - System.DateTime.Now.Ticks) / System.TimeSpan.TicksPerMillisecond;
                            Globals.l2net_home.Add_Text("tried to use skill too early: " + Util.GetSkillName(sk.ID, sk.Level) + " : try again in " + msec.ToString() + " milliseconds.");
                        }
                    }
#if DEBUG
                    else
                    {
                        Globals.l2net_home.Add_Text("tried to use a skill you don't have: " + Util.GetSkillName(id, 1));
                    }
#endif
                }
                finally
                {
                    Globals.SkillListLock.ExitReadLock();
                }
            }
        }

        public static void Try_Use_Skill_Smart(uint id, bool control, bool shift)
        {
            if (Globals.gamedata.my_char.Cur_HP > 0)
            {
                Globals.SkillListLock.EnterReadLock();
                try
                {
                    if (Globals.gamedata.skills.ContainsKey(id))
                    {
                        UserSkill sk = (UserSkill)Globals.gamedata.skills[id];

                        if (sk.IsReady())//sk.Time
                        {
                            ServerPackets.Use_Skill(sk.ID, control, shift);
                            //sk.LastTime = System.DateTime.Now;
                            sk.NextTime = System.DateTime.Now.AddMilliseconds(Globals.SKILL_MIN_REUSE);

                            while (Globals.gamedata.my_char.ExpiresTime == 0)
                            {
                                System.Threading.Thread.Sleep(1);
                            }
                            
                            while (System.DateTime.Now.Ticks < Globals.gamedata.my_char.ExpiresTime) 
                            {
                                System.Threading.Thread.Sleep(1);
                            }                            
                        }
                        else
                        {
                            long msec = (sk.NextTime.Ticks - System.DateTime.Now.Ticks) / System.TimeSpan.TicksPerMillisecond;
                            // Globals.l2net_home.Add_Text("tried to use skill too early: " + Util.GetSkillName(sk.ID, sk.Level) + " : try again in " + msec.ToString() + " milliseconds.");
                            Globals.gamedata.my_char.ExpiresTime = 1;
                        }
                        
                    }
#if DEBUG
                    else
                    {
                        Globals.l2net_home.Add_Text("tried to use a skill you don't have: " + Util.GetSkillName(id, 1));
                    }
#endif
                }
                finally
                {
                    Globals.SkillListLock.ExitReadLock();
                }
            }
        }

        public static bool TrySkill(string name, CharBuffTimer cbt, BuffTargetClass bft)
        {
            uint tmp_id;
            if (System.String.Equals("PET", name.ToUpperInvariant()))
            {
                tmp_id = Globals.gamedata.my_pet.ID;
            }
            if (System.String.Equals("PET1", name.ToUpperInvariant()))
            {
                tmp_id = Globals.gamedata.my_pet1.ID;
            }
            if (System.String.Equals("PET2", name.ToUpperInvariant()))
            {
                tmp_id = Globals.gamedata.my_pet2.ID;
            }
            if (System.String.Equals("PET3", name.ToUpperInvariant()))
            {
                tmp_id = Globals.gamedata.my_pet3.ID;
            }
            else
            {
                tmp_id = Util.GetCharID(name);
            }

            if (Util.Distance(tmp_id) > bft.Range)
            {
                //cant buff them now
                //Globals.gamedata.my_char.BuffTarget = 0;
                return false;
            }
            else
            {
                //do we actually need a target?
                Globals.gamedata.my_char.LastBuffTime = System.DateTime.Now;
                Globals.gamedata.my_char.BuffSkillID = bft.SkillID;
                Globals.gamedata.my_char.BuffTarget = tmp_id;
                Globals.gamedata.my_char.LastTarget = Globals.gamedata.my_char.TargetID;

                Globals.gamedata.BOT_STATE = BotState.Buffing;

                if (bft.NeedTarget == 1)
                {
                    Globals.gamedata.my_char.BuffNeedTarget = 1;

                    if (!Globals.gamedata.my_char.CanBuff())
                    {
                        int x = 0, y = 0, z = 0;
                        Util.GetCharLoc(Globals.gamedata.my_char.BuffTarget, ref x, ref y, ref z);
                        Target(Globals.gamedata.my_char.BuffTarget, x, y, z, false);
                    }
                }
                else
                {
                    Globals.gamedata.my_char.BuffNeedTarget = 0;
                }

                return true;
            }
        }

        public static void RequestMagicSkillList()
        {
            //RequestMagicSkillList
            //RequestMagicSkillList:c(ID)c(c)c(c)c(c)d(ObjectID)d(CharID)
            ByteBuffer bbuff = new ByteBuffer(12);

            bbuff.WriteByte((byte)PClient.RequestMagicSkillList);
            bbuff.WriteByte(0x00);
            bbuff.WriteByte(0x00);
            bbuff.WriteByte(0x00);
            bbuff.WriteUInt32(Globals.gamedata.my_char.charID); //Char ID from charselected packet
            bbuff.WriteUInt32(Globals.gamedata.my_char.ID); //char ID from userinfo packet

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void RequestClanInfo(uint clanid)
        {
            //request clan/ally name
            ByteBuffer bbuff = new ByteBuffer(5);

            bbuff.WriteByte((byte)PClient.RequestPledgeInfo);
            bbuff.WriteUInt32(clanid);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void RequestCrest(uint crestid)
        {
            ByteBuffer bbuff = new ByteBuffer(5);

            bbuff.WriteByte((byte)PClient.RequestPledgeCrest);
            bbuff.WriteUInt32(crestid);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void NPC_Chat_Click(string text)
        {
            if (Globals.Script_Debugging)
            {
                Globals.l2net_home.Add_Debug("NPC Chat Click: " + text);
            }

            byte type = 0;

            if (text.ToUpperInvariant().StartsWith("LINK "))
            {
                text = text.Substring(5, text.Length - 5);
                type = (byte)PClient.RequestLinkHtml;
            }
            if (text.ToUpperInvariant().StartsWith("BYPASS -H "))
            {
                text = text.Substring(10, text.Length - 10);
                type = (byte)PClient.RequestBypassToServer;
            }

            if (text.ToUpperInvariant().Contains(Globals.Captcha_HTML1.ToUpper()) || text.ToUpperInvariant().Contains(Globals.Captcha_HTML2.ToUpper()))
            {
                type = (byte)PClient.RequestBypassToServer;
            }

            if (type != 0)
            {
#if DEBUG
                if (Globals.Script_Debugging)
                {
                    Globals.l2net_home.Add_Debug("NPC Chat Click2: " + text);
                }
#endif

                ByteBuffer bbuff = new ByteBuffer(text.Length * 2 + 2 + 1);

                bbuff.WriteByte(type);

                bbuff.WriteString(text);

                Globals.gamedata.SendToGameServer(bbuff);
            }
        }

        public static void Try_Use_Item(uint ItemID, bool exp)
        {
            Globals.InventoryLock.EnterReadLock();
            try
            {
                if (exp)
                {
                    foreach (InventoryInfo inv_inf in Globals.gamedata.inventory.Values)
                    {
                        if (inv_inf.ID == ItemID)
                        {
                            Use_Item(ItemID);
                            return;
                        }
                    }
                }
                else
                {
                    foreach (InventoryInfo inv_inf in Globals.gamedata.inventory.Values)
                    {
                        if (inv_inf.ItemID == ItemID)
                        {
                            Use_Item(inv_inf.ID);
                            return;
                        }
                    }
                }
            }
            finally
            {
                Globals.InventoryLock.ExitReadLock();
            }
        }

        public static void Use_Item(uint ItemID)
        {
            if (Globals.gamedata.my_char.Cur_HP > 0)
            {
                ByteBuffer bbuff = new ByteBuffer(9);

                bbuff.WriteByte((byte)PClient.UseItem);
                bbuff.WriteUInt32(ItemID);
                bbuff.WriteUInt32(0);

                Globals.gamedata.SendToGameServer(bbuff);
            }
        }

        public static void Send_Appearing()
        {
            ByteBuffer bbuff = new ByteBuffer(1);

            bbuff.WriteByte((byte)PClient.Appearing);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void RequestLogin1()
        {
            ByteBuffer bbuff = new ByteBuffer(3);

            bbuff.WriteByte((byte)PClient.EXPacket);
            bbuff.WriteByte((byte)PClientEX.Login1);
            bbuff.WriteByte(0x00);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void RequestLogin2()
        {
            ByteBuffer bbuff = new ByteBuffer(3);

            bbuff.WriteByte((byte)PClient.EXPacket);
            bbuff.WriteByte((byte)PClientEX.Login2);
            bbuff.WriteByte(0x00);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void RequestLogin3()
        {
            ByteBuffer bbuff = new ByteBuffer(3);

            bbuff.WriteByte((byte)PClient.EXPacket);
            bbuff.WriteByte((byte)PClientEX.Login3);
            bbuff.WriteByte(0x00);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void RequestLogin4()
        {
            ByteBuffer bbuff = new ByteBuffer(3);

            bbuff.WriteByte((byte)PClient.EXPacket);
            bbuff.WriteByte((byte)PClientEX.Login4);
            bbuff.WriteByte(0x00);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void RequestAllFortressInfo()
        {
            ByteBuffer bbuff = new ByteBuffer(3);

            bbuff.WriteByte((byte)PClient.EXPacket);
            bbuff.WriteByte((byte)PClientEX.RequestAllFortressInfo);
            bbuff.WriteByte(0x00);

            Globals.gamedata.SendToGameServer(bbuff);
        }


        public static void RequestManorList()
        {
            ByteBuffer bbuff = new ByteBuffer(3);

            bbuff.WriteByte((byte)PClient.EXPacket);
            bbuff.WriteByte((byte)PClientEX.RequestManorList);
            bbuff.WriteByte(0x00);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void RequestKeyMapping()
        {
            ByteBuffer bbuff = new ByteBuffer(3);

            bbuff.WriteByte((byte)PClient.EXPacket);
            bbuff.WriteByte((byte)PClientEX.RequestKeyMapping);
            bbuff.WriteByte(0x00);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void RequestSecurityPinWindow()
        {
            ByteBuffer bbuff = new ByteBuffer(3);

            bbuff.WriteByte((byte)PClient.EXPacket);
            bbuff.WriteByte((byte)PClientEX.RequestSecurityPinWindow);
            bbuff.WriteByte(0x00);
            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void SecurityPin()
        {
            ByteBuffer bbuff = new ByteBuffer((Globals.SecurityPin.Length * 2) + 6);
            bbuff.WriteByte((byte)PClient.EXPacket);
            bbuff.WriteByte((byte)PClientEX.SecurityPin);
            bbuff.WriteByte(0x00);
            bbuff.WriteString(Globals.SecurityPin);
            bbuff.WriteByte(0x00);
            bbuff.WriteByte(0x00);
            bbuff.WriteByte(0x00);

            //for(int i = 0; i < 50;i=i+2)
            //{
                //Globals.l2net_home.Add_Error("PIN: " + bbuff.GetByte(i/2).ToString("X2"));
            //}

            Globals.gamedata.SendToGameServer(bbuff);

        }

        public static void Send_EnterWorld()
        {
            if (Globals.enterworld_check)
            {
                //Custom enterworld packet
                ByteBuffer bbuff = new ByteBuffer(512);
                string ew = Globals.enterworld_custom;
                ew = ew.Replace(" ", "");
                string sm;

                for (int i = 0; i < ew.Length; i += 2)
                {
                    sm = (ew[i].ToString()) + (ew[i + 1].ToString());

                    bbuff.WriteByte(byte.Parse(sm, System.Globalization.NumberStyles.HexNumber));
                }
                Globals.l2net_home.Add_Debug("Enterworld Custom");
                bbuff.TrimToIndex();

                if (Globals.enterworld_ip)//(adifenix) - change ip bytes
                {
                    Random rand = new Random();
                    bbuff.SetIndex(85);
                    int temp_int = new int();
                    for (int i = 0; i < Globals.ew_chc_ed_array.Count; i++)
                    {
                        if ((Globals.ew_chc_ed_array[i] as System.Windows.Forms.CheckBox).Checked)
                        {
                            bbuff.WriteByte((byte)rand.Next(1, 255));
                        }
                        else// grab text from edit
                        {
                            if ((Globals.ew_con_array[i] as System.Windows.Forms.TextBox).TextLength > 0)
                            {
                                try
                                {
                                    temp_int = System.Convert.ToInt32((Globals.ew_con_array[i] as System.Windows.Forms.TextBox).Text);
                                }
                                catch (FormatException) // non digit chars
                                {
                                    temp_int = rand.Next(1, 255);
                                }
                                if (temp_int >= 0 && temp_int <= 255)
                                {
                                    bbuff.WriteByte((byte)temp_int);
                                }
                            }
                            else // text legh == 0  so gen random ip
                            {
                                temp_int = rand.Next(1, 255);
                                bbuff.WriteByte((byte)temp_int);
                            }
                        }

                    }
                }


                Globals.gamedata.SendToGameServer(bbuff);
            }
            else
            {
                ByteBuffer bbuff = new ByteBuffer(105);
                bbuff.WriteByte((byte)PClient.EnterWorld);

                //11 23 01 00 00 67 45 00 00 AB 89 00 00 EF CD 00 00 C9 BC F2 A7 66 5A 0B 98 36 A5 BD 89 ED 7F E4 D7 6B 49 E2 9F EF 76 EB CE A3 FA F4 BF 0C 64 A3 B4 A4 CE DC C6 08 3E 6E EA 45 CA D3 FE 88 13 87 B8 06 2C 96 F0 9B 1E 8E BC C6 9B 98 C8 63 16 CF D0 29 00 00 00 C0 A8 00 79 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 
                //11 23 01 00 00 67 45 00 00 AB 89 00 00 EF CD 00 00 C9 BC F2 A7 66 5A 0B 98 36 A5 BD 89 ED 7F E4 D7 6B 49 E2 9F EF 76 EB CE A3 FA F4 BF 0C 64 A3 B4 A4 CE DC C6 08 3E 6E EA 45 CA D3 FE 88 13 87 B8 06 2C 96 F0 9B 1E 8E BC C6 9B 98 C8 63 16 CF D0 29 00 00 00 C0 A8 00 79 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 

                //11 23 01 00 00 67 45 00 00 AB 89 00 00 EF CD 00 00 D0 CB 72 72 1E 98 C9 F4 8D B6 2E 60 D9 0B F2 71 0A 08 EF C8 1F A1 22 1B D0 7A 94 92 E6 23 3E A2 2D 8C C7 BA 85 A3 D9 CE E6 8C B3 36 AA 0E 17 3B C3 F3 D9 0B DD A2 3D 4B E7 68 A9 52 12 70 D6 DC 18 4B 00 00 C0 A8 0A 03 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 - CT 2.3
                //11 23 01 00 00 67 45 00 00 AB 89 00 00 EF CD 00 00 2C 89 D9 4B 2E 0E 27 7F DC 86 F1 6F A8 72 ED 74 47 A5 9E AA B6 09 11 86 1F D0 3D 77 6F 9D 2C A9 F2 AC F2 0E 9C F0 35 EF D5 91 63 2F D8 E1 A3 A8 F5 A6 93 D7 DB 0A EE BB F8 54 57 E1 F0 FC E7 C4 23 34 00 00 C0 A8 0A 03 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 - CT 2.3

                //11 14 7E 68 DA 09 5B 95 39 41 76 B3 39 86 1A 00 38 86 0E 0A 49 53 EE DA 69 EC 0A F1 E8 8B 9E FE 33 8E 8D 55 79 AC 8C D6 7D 58 5C 73 8C 6D 98 2C C0 63 F8 24 76 E5 4E 1D 12 4E 49 F4 EA 36 6F 23 8F 27 1E DD F1 FD FA 21 77 BC 7B 6E AE 57 7C 61 04 8B 23 00 00 C0 A8 01 05 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 - CT 2.4 G+
                //11 14 7E 68 DA 09 5B 95 39 41 76 B3 39 86 1A 00 38 20 1C 5C 47 D6 1A F0 B6 73 3E 09 43 FC 2E 0D 70 4D B1 E6 55 6A 86 F5 10 B8 48 99 D9 E7 33 1A B6 27 53 31 0D 23 DB D6 EC EB DD FD 76 26 7C 04 8D 9D 7A 3A 82 BD D5 6C 7C 6D 99 96 47 86 91 FB 1F E0 6F 00 00 74 44 88 3D 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 - CT 2.4 (proto 146)
                //11 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 3A 0F 3F 07 16 D2 43 FD 15 05 0F D4 5C 29 C9 77 71 A1 45 23 82 30 3B 81 E1 FC EF BC 0A 2B 14 25 8F 30 1F 80 3C 91 0D 79 A3 C2 75 FB 3A 22 FF F7 05 86 F8 E7 81 F0 6B 42 DA 39 79 F7 13 4B FE D2 B9 0A 00 00 74 44 88 3D 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 00 - CT 2.5 (no GG)

                //11 23 01 00 00 67 45 00 00 AB 89 00 00 EF CD 00 00
                bbuff.WriteByte(Globals.Login_GG_Reply[0]);//
                bbuff.WriteByte(Globals.Login_GG_Reply[1]);
                bbuff.WriteByte(Globals.Login_GG_Reply[2]);
                bbuff.WriteByte(Globals.Login_GG_Reply[3]);
                bbuff.WriteByte(Globals.Login_GG_Reply[4]);
                bbuff.WriteByte(Globals.Login_GG_Reply[5]);
                bbuff.WriteByte(Globals.Login_GG_Reply[6]);
                bbuff.WriteByte(Globals.Login_GG_Reply[7]);
                bbuff.WriteByte(Globals.Login_GG_Reply[8]);
                bbuff.WriteByte(Globals.Login_GG_Reply[9]);
                bbuff.WriteByte(Globals.Login_GG_Reply[10]);
                bbuff.WriteByte(Globals.Login_GG_Reply[11]);
                bbuff.WriteByte(Globals.Login_GG_Reply[12]);
                bbuff.WriteByte(Globals.Login_GG_Reply[13]);
                bbuff.WriteByte(Globals.Login_GG_Reply[14]);
                bbuff.WriteByte(Globals.Login_GG_Reply[15]);

                switch (Globals.gamedata.Chron)
                {
                    case Chronicle.CT3_2:
                        Globals.l2net_home.Add_Debug("Enterworld Glory Days");
                        //Oddi: I'm lazy :D
                        string ewwGD = "14 92 A4 E7 BE 5D 7D 68 F3 53 B9 0B 94 8E 1D 9A 58 E6 A2 C2 94 29 1B 51 B6 45 CD F9 74 FD 30 5A 4A 9B 5B C5 F6 8C F7 C2 09 61 E4 D9 44 34 FB D3 DB 0B 5B D5 48 20 D2 73 76 FD B3 45 A7 25 25 12 C3 72 00 00 D8 6B F2 C3";
                        ewwGD = ewwGD.Replace(" ", "");
                        string smwGD;
                        for (int iz = 0; iz < ewwGD.Length; iz += 2)
                        {
                            smwGD = (ewwGD[iz].ToString()) + (ewwGD[iz + 1].ToString());

                            bbuff.WriteByte(byte.Parse(smwGD, System.Globalization.NumberStyles.HexNumber));
                        }
                        break;

                    case Chronicle.CT3_1:
                        Globals.l2net_home.Add_Debug("Enterworld Tauti");
                        //Oddi: I'm lazy :D
                        string ewwT = "C2 E0 A9 89 1F 2A 1E E2 44 8E 8A 0B AF 0A 69 24 1A 87 11 4D 05 4E 86 05 6C D0 41 83 B4 40 28 11 04 07 7F 57 E9 F2 D9 1D 91 53 5B 29 23 DF B4 2B FE 4F DE 10 BD 88 E9 75 AB 20 FE 4B EC F4 D2 B3 08 5B 00 00 74 44 88 3D";
                        ewwT = ewwT.Replace(" ", "");
                        string smwT;
                        for (int iz = 0; iz < ewwT.Length; iz += 2)
                        {
                            smwT = (ewwT[iz].ToString()) + (ewwT[iz + 1].ToString());

                            bbuff.WriteByte(byte.Parse(smwT, System.Globalization.NumberStyles.HexNumber));
                        }
                        break;
                    case Chronicle.CT3_0:
                        Globals.l2net_home.Add_Debug("Enterworld GoD");
                        //Oddi: I'm lazy :D
                        string eww = "60 2C 39 0D 74 3F F0 E4 A9 67 E7 90 27 F6 26 04 72 7B 24 4E 8E 60 81 0B 9E 29 C4 3A 54 29 E7 F9 9C 1B 30 AD E9 59 15 52 2D A1 D9 48 52 A3 1F 3C 6C 68 D9 3F 68 41 96 C9 F5 E1 62 3F 18 8B 82 E8 29 20 00 00 74 44 88 3D";
                        eww = eww.Replace(" ", "");
                        string smw;
                        for (int iz = 0; iz < eww.Length; iz += 2)
                        {
                            smw = (eww[iz].ToString()) + (eww[iz + 1].ToString());

                            bbuff.WriteByte(byte.Parse(smw, System.Globalization.NumberStyles.HexNumber));
                        }
                        break;
                    case Chronicle.CT2_6:
                        Globals.l2net_home.Add_Debug("Enterworld High Five");
                        bbuff.WriteByte(0xC1);
                        bbuff.WriteByte(0x7A);
                        bbuff.WriteByte(0xD7);
                        bbuff.WriteByte(0x78);
                        bbuff.WriteByte(0x45);
                        bbuff.WriteByte(0x10);
                        bbuff.WriteByte(0xD7);
                        bbuff.WriteByte(0x96);
                        bbuff.WriteByte(0x15);
                        bbuff.WriteByte(0xA4);
                        bbuff.WriteByte(0x98);
                        bbuff.WriteByte(0x8F);
                        bbuff.WriteByte(0x39);
                        bbuff.WriteByte(0x4C);
                        bbuff.WriteByte(0xEF);
                        bbuff.WriteByte(0x78);
                        bbuff.WriteByte(0xC4);
                        bbuff.WriteByte(0x81);
                        bbuff.WriteByte(0xEA);
                        bbuff.WriteByte(0x9F);
                        bbuff.WriteByte(0x25);
                        bbuff.WriteByte(0xB5);
                        bbuff.WriteByte(0xBC);
                        bbuff.WriteByte(0x5B);
                        bbuff.WriteByte(0xF2);
                        bbuff.WriteByte(0xC2);
                        bbuff.WriteByte(0x79);
                        bbuff.WriteByte(0x06);
                        bbuff.WriteByte(0x63);
                        bbuff.WriteByte(0x67);
                        bbuff.WriteByte(0x47);
                        bbuff.WriteByte(0x10);
                        bbuff.WriteByte(0x96);
                        bbuff.WriteByte(0x88);
                        bbuff.WriteByte(0xD4);
                        bbuff.WriteByte(0x75);
                        bbuff.WriteByte(0xE0);
                        bbuff.WriteByte(0x8F);
                        bbuff.WriteByte(0xCF);
                        bbuff.WriteByte(0x71);
                        bbuff.WriteByte(0xA8);
                        bbuff.WriteByte(0x2F);
                        bbuff.WriteByte(0xDA);
                        bbuff.WriteByte(0x35);
                        bbuff.WriteByte(0x5D);
                        bbuff.WriteByte(0x9E);
                        bbuff.WriteByte(0x45);
                        bbuff.WriteByte(0x6E);
                        bbuff.WriteByte(0x81);
                        bbuff.WriteByte(0xFC);
                        bbuff.WriteByte(0x7B);
                        bbuff.WriteByte(0xB9);
                        bbuff.WriteByte(0x2A);
                        bbuff.WriteByte(0x62);
                        bbuff.WriteByte(0x96);
                        bbuff.WriteByte(0xEA);
                        bbuff.WriteByte(0x44);
                        bbuff.WriteByte(0x13);
                        bbuff.WriteByte(0x09);
                        bbuff.WriteByte(0xCA);
                        bbuff.WriteByte(0x81);
                        bbuff.WriteByte(0x52);
                        bbuff.WriteByte(0x16);
                        bbuff.WriteByte(0x3E);
                        bbuff.WriteByte(0x74);
                        bbuff.WriteByte(0x16);
                        bbuff.WriteByte(0x00);
                        bbuff.WriteByte(0x00);
                        bbuff.WriteByte(0xD8);
                        bbuff.WriteByte(0x6B);
                        bbuff.WriteByte(0xF2);
                        bbuff.WriteByte(0xC3);
                        break;
                    case Chronicle.CT2_5:
                        Globals.l2net_home.Add_Debug("Enterworld Freya");
                        //3A 0F 3F 07 16 D2 43 FD 15 05 0F D4 5C 29 C9 77 71 A1 45 23 82 30 3B 81 E1 FC EF BC 0A 2B 14 25 8F 30 1F 80 3C 91 0D 79 A3 C2 75 FB 3A 22 FF F7 05 86 F8 E7 81 F0 6B 42 DA 39 79 F7 13 4B FE D2 B9 0A 00 00 74 44 88 3D
                        bbuff.WriteByte(0x3A);
                        bbuff.WriteByte(0x0F);
                        bbuff.WriteByte(0x3F);
                        bbuff.WriteByte(0x07);
                        bbuff.WriteByte(0x16);
                        bbuff.WriteByte(0xD2);
                        bbuff.WriteByte(0x43);
                        bbuff.WriteByte(0xFD);
                        bbuff.WriteByte(0x15);
                        bbuff.WriteByte(0x05);
                        bbuff.WriteByte(0x0F);
                        bbuff.WriteByte(0xD4);
                        bbuff.WriteByte(0x5C);
                        bbuff.WriteByte(0x29);
                        bbuff.WriteByte(0xC9);
                        bbuff.WriteByte(0x77);
                        bbuff.WriteByte(0x71);
                        bbuff.WriteByte(0xA1);
                        bbuff.WriteByte(0x45);
                        bbuff.WriteByte(0x23);
                        bbuff.WriteByte(0x82);
                        bbuff.WriteByte(0x30);
                        bbuff.WriteByte(0x3B);
                        bbuff.WriteByte(0x81);
                        bbuff.WriteByte(0xE1);
                        bbuff.WriteByte(0xFC);
                        bbuff.WriteByte(0xEF);
                        bbuff.WriteByte(0xBC);
                        bbuff.WriteByte(0x0A);
                        bbuff.WriteByte(0x2B);
                        bbuff.WriteByte(0x14);
                        bbuff.WriteByte(0x25);
                        bbuff.WriteByte(0x8F);
                        bbuff.WriteByte(0x30);
                        bbuff.WriteByte(0x1F);
                        bbuff.WriteByte(0x80);
                        bbuff.WriteByte(0x3C);
                        bbuff.WriteByte(0x91);
                        bbuff.WriteByte(0x0D);
                        bbuff.WriteByte(0x79);
                        bbuff.WriteByte(0xA3);
                        bbuff.WriteByte(0xC2);
                        bbuff.WriteByte(0x75);
                        bbuff.WriteByte(0xFB);
                        bbuff.WriteByte(0x3A);
                        bbuff.WriteByte(0x22);
                        bbuff.WriteByte(0xFF);
                        bbuff.WriteByte(0xF7);
                        bbuff.WriteByte(0x05);
                        bbuff.WriteByte(0x86);
                        bbuff.WriteByte(0xF8);
                        bbuff.WriteByte(0xE7);
                        bbuff.WriteByte(0x81);
                        bbuff.WriteByte(0xF0);
                        bbuff.WriteByte(0x6B);
                        bbuff.WriteByte(0x42);
                        bbuff.WriteByte(0xDA);
                        bbuff.WriteByte(0x39);
                        bbuff.WriteByte(0x79);
                        bbuff.WriteByte(0xF7);
                        bbuff.WriteByte(0x13);
                        bbuff.WriteByte(0x4B);
                        bbuff.WriteByte(0xFE);
                        bbuff.WriteByte(0xD2);
                        bbuff.WriteByte(0xB9);
                        bbuff.WriteByte(0x0A);
                        bbuff.WriteByte(0x00);
                        bbuff.WriteByte(0x00);
                        bbuff.WriteByte(0x74);
                        bbuff.WriteByte(0x44);
                        bbuff.WriteByte(0x88);
                        bbuff.WriteByte(0x3D);
                        break;
                    case Chronicle.CT2_4:
                        Globals.l2net_home.Add_Debug("Enterworld CT G+");
                        //20 1C 5C 47
                        bbuff.WriteByte(0x20);
                        bbuff.WriteByte(0x1C);
                        bbuff.WriteByte(0x5C);
                        bbuff.WriteByte(0x47);
                        //D6 1A F0 B6 
                        bbuff.WriteByte(0xD6);
                        bbuff.WriteByte(0x1A);
                        bbuff.WriteByte(0xF0);
                        bbuff.WriteByte(0xB6);
                        //73 3E 09 43
                        bbuff.WriteByte(0x73);
                        bbuff.WriteByte(0x3E);
                        bbuff.WriteByte(0x09);
                        bbuff.WriteByte(0x43);
                        //FC 2E 0D 70
                        bbuff.WriteByte(0xFC);
                        bbuff.WriteByte(0x2E);
                        bbuff.WriteByte(0x0D);
                        bbuff.WriteByte(0x70);
                        //4D B1 E6 55
                        bbuff.WriteByte(0x4D);
                        bbuff.WriteByte(0xB1);
                        bbuff.WriteByte(0xE6);
                        bbuff.WriteByte(0x55);
                        //6A 86 F5 10
                        bbuff.WriteByte(0x6A);
                        bbuff.WriteByte(0x86);
                        bbuff.WriteByte(0xF5);
                        bbuff.WriteByte(0x10);
                        //B8 48 99 D9
                        bbuff.WriteByte(0xB8);
                        bbuff.WriteByte(0x48);
                        bbuff.WriteByte(0x99);
                        bbuff.WriteByte(0xD9);
                        //E7 33 1A B6
                        bbuff.WriteByte(0xE7);
                        bbuff.WriteByte(0x33);
                        bbuff.WriteByte(0x1A);
                        bbuff.WriteByte(0xB6);
                        //27 53 31 0D
                        bbuff.WriteByte(0x27);
                        bbuff.WriteByte(0x53);
                        bbuff.WriteByte(0x31);
                        bbuff.WriteByte(0x0D);
                        //23 DB D6 EC 
                        bbuff.WriteByte(0x23);
                        bbuff.WriteByte(0xDB);
                        bbuff.WriteByte(0xD6);
                        bbuff.WriteByte(0xEC);
                        //EB DD FD 76
                        bbuff.WriteByte(0xEB);
                        bbuff.WriteByte(0xDD);
                        bbuff.WriteByte(0xFD);
                        bbuff.WriteByte(0x76);
                        //26 7C 04 8D
                        bbuff.WriteByte(0x26);
                        bbuff.WriteByte(0x7C);
                        bbuff.WriteByte(0x04);
                        bbuff.WriteByte(0x8D);
                        //9D 7A 3A 82
                        bbuff.WriteByte(0x9D);
                        bbuff.WriteByte(0x7A);
                        bbuff.WriteByte(0x3A);
                        bbuff.WriteByte(0x82);
                        //BD D5 6C 7C
                        bbuff.WriteByte(0xBD);
                        bbuff.WriteByte(0xD5);
                        bbuff.WriteByte(0x6C);
                        bbuff.WriteByte(0x7C);
                        //6D 99 96 47
                        bbuff.WriteByte(0x6D);
                        bbuff.WriteByte(0x99);
                        bbuff.WriteByte(0x96);
                        bbuff.WriteByte(0x47);
                        //86 91 FB 1F
                        bbuff.WriteByte(0x86);
                        bbuff.WriteByte(0x91);
                        bbuff.WriteByte(0xFB);
                        bbuff.WriteByte(0x1F);
                        //E0 6F 00 00
                        bbuff.WriteByte(0xE0);
                        bbuff.WriteByte(0x6F);
                        bbuff.WriteByte(0x00);
                        bbuff.WriteByte(0x00);
                        //74 44 88 3D
                        bbuff.WriteByte(0x74);
                        bbuff.WriteByte(0x44);
                        bbuff.WriteByte(0x88);
                        bbuff.WriteByte(0x3D);
                        break;
                    case Chronicle.CT2_3:
                        Globals.l2net_home.Add_Debug("Enterworld CT2.3");
                        //D0 CB 72 72
                        bbuff.WriteByte(0xD0);
                        bbuff.WriteByte(0xCB);
                        bbuff.WriteByte(0x72);
                        bbuff.WriteByte(0x72);
                        //1E 98 C9 F4
                        bbuff.WriteByte(0x1E);
                        bbuff.WriteByte(0x98);
                        bbuff.WriteByte(0xC9);
                        bbuff.WriteByte(0xF4);
                        //8D B6 2E 60
                        bbuff.WriteByte(0x8D);
                        bbuff.WriteByte(0xB6);
                        bbuff.WriteByte(0x2E);
                        bbuff.WriteByte(0x60);
                        //D9 0B F2 71
                        bbuff.WriteByte(0xD9);
                        bbuff.WriteByte(0x0B);
                        bbuff.WriteByte(0xF2);
                        bbuff.WriteByte(0x71);
                        //0A 08 EF C8
                        bbuff.WriteByte(0x0A);
                        bbuff.WriteByte(0x08);
                        bbuff.WriteByte(0xEF);
                        bbuff.WriteByte(0xC8);
                        //1F A1 22 1B
                        bbuff.WriteByte(0x1F);
                        bbuff.WriteByte(0xA1);
                        bbuff.WriteByte(0x22);
                        bbuff.WriteByte(0x1B);
                        //D0 7A 94 92
                        bbuff.WriteByte(0xD0);
                        bbuff.WriteByte(0x7A);
                        bbuff.WriteByte(0x94);
                        bbuff.WriteByte(0x92);
                        //E6 23 3E A2
                        bbuff.WriteByte(0xE6);
                        bbuff.WriteByte(0x23);
                        bbuff.WriteByte(0x3E);
                        bbuff.WriteByte(0xA2);
                        //2D 8C C7 BA
                        bbuff.WriteByte(0x2D);
                        bbuff.WriteByte(0x8C);
                        bbuff.WriteByte(0xC7);
                        bbuff.WriteByte(0xBA);
                        //85 A3 D9 CE
                        bbuff.WriteByte(0x85);
                        bbuff.WriteByte(0xA3);
                        bbuff.WriteByte(0xD9);
                        bbuff.WriteByte(0xCE);
                        //E6 8C B3 36
                        bbuff.WriteByte(0xE6);
                        bbuff.WriteByte(0x8C);
                        bbuff.WriteByte(0xB3);
                        bbuff.WriteByte(0x36);
                        //AA 0E 17 3B
                        bbuff.WriteByte(0xAA);
                        bbuff.WriteByte(0x0E);
                        bbuff.WriteByte(0x17);
                        bbuff.WriteByte(0x3B);
                        //C3 F3 D9 0B
                        bbuff.WriteByte(0xC3);
                        bbuff.WriteByte(0xF3);
                        bbuff.WriteByte(0xD9);
                        bbuff.WriteByte(0x0B);
                        //DD A2 3D 4B
                        bbuff.WriteByte(0xDD);
                        bbuff.WriteByte(0xA2);
                        bbuff.WriteByte(0x3D);
                        bbuff.WriteByte(0x4B);
                        //E7 68 A9 52
                        bbuff.WriteByte(0xE7);
                        bbuff.WriteByte(0x68);
                        bbuff.WriteByte(0xA9);
                        bbuff.WriteByte(0x52);
                        //12 70 D6 DC
                        bbuff.WriteByte(0x12);
                        bbuff.WriteByte(0x70);
                        bbuff.WriteByte(0xD6);
                        bbuff.WriteByte(0xDC);
                        //18 4B 00 00
                        bbuff.WriteByte(0x18);
                        bbuff.WriteByte(0x4B);
                        bbuff.WriteByte(0x00);
                        bbuff.WriteByte(0x00);
                        //C0 A8 0A 03
                        bbuff.WriteByte(0xC0);
                        bbuff.WriteByte(0xA8);
                        bbuff.WriteByte(0x0A);
                        bbuff.WriteByte(0x03);
                        break;
                    default:
                        Globals.l2net_home.Add_Debug("Enterworld CT2.2 or before");
                        //C9 BC F2 A7
                        bbuff.WriteByte(0xC9);
                        bbuff.WriteByte(0xBC);
                        bbuff.WriteByte(0xF2);
                        bbuff.WriteByte(0xA7);
                        //66 5A 0B 98
                        bbuff.WriteByte(0x66);
                        bbuff.WriteByte(0x5A);
                        bbuff.WriteByte(0x0B);
                        bbuff.WriteByte(0x98);
                        //36 A5 BD 89
                        bbuff.WriteByte(0x36);
                        bbuff.WriteByte(0xA5);
                        bbuff.WriteByte(0xBD);
                        bbuff.WriteByte(0x89);
                        //ED 7F E4 D7
                        bbuff.WriteByte(0xED);
                        bbuff.WriteByte(0x7F);
                        bbuff.WriteByte(0xE4);
                        bbuff.WriteByte(0xD7);
                        //6B 49 E2 9F
                        bbuff.WriteByte(0x6B);
                        bbuff.WriteByte(0x49);
                        bbuff.WriteByte(0xE2);
                        bbuff.WriteByte(0x9F);
                        //EF 76 EB CE
                        bbuff.WriteByte(0xEF);
                        bbuff.WriteByte(0x76);
                        bbuff.WriteByte(0xEB);
                        bbuff.WriteByte(0xCE);
                        //A3 FA F4 BF
                        bbuff.WriteByte(0xA3);
                        bbuff.WriteByte(0xFA);
                        bbuff.WriteByte(0xF4);
                        bbuff.WriteByte(0xBF);
                        //0C 64 A3 B4
                        bbuff.WriteByte(0x0C);
                        bbuff.WriteByte(0x64);
                        bbuff.WriteByte(0xA3);
                        bbuff.WriteByte(0xB4);
                        //A4 CE DC C6
                        bbuff.WriteByte(0xA4);
                        bbuff.WriteByte(0xCE);
                        bbuff.WriteByte(0xDC);
                        bbuff.WriteByte(0xC6);
                        //08 3E 6E EA
                        bbuff.WriteByte(0x08);
                        bbuff.WriteByte(0x3E);
                        bbuff.WriteByte(0x6E);
                        bbuff.WriteByte(0xEA);
                        //45 CA D3 FE
                        bbuff.WriteByte(0x45);
                        bbuff.WriteByte(0xCA);
                        bbuff.WriteByte(0xD3);
                        bbuff.WriteByte(0xFE);
                        //88 13 87 B8
                        bbuff.WriteByte(0x88);
                        bbuff.WriteByte(0x13);
                        bbuff.WriteByte(0x87);
                        bbuff.WriteByte(0xB8);
                        //06 2C 96 F0
                        bbuff.WriteByte(0x06);
                        bbuff.WriteByte(0x2C);
                        bbuff.WriteByte(0x96);
                        bbuff.WriteByte(0xF0);
                        //9B 1E 8E BC
                        bbuff.WriteByte(0x9B);
                        bbuff.WriteByte(0x1E);
                        bbuff.WriteByte(0x8E);
                        bbuff.WriteByte(0xBC);
                        //C6 9B 98 C8
                        bbuff.WriteByte(0xC6);
                        bbuff.WriteByte(0x9B);
                        bbuff.WriteByte(0x98);
                        bbuff.WriteByte(0xC8);
                        //63 16 CF D0
                        bbuff.WriteByte(0x63);
                        bbuff.WriteByte(0x16);
                        bbuff.WriteByte(0xCF);
                        bbuff.WriteByte(0xD0);
                        //29 00 00 00
                        bbuff.WriteByte(0x29);
                        bbuff.WriteByte(0x00);
                        bbuff.WriteByte(0x00);
                        bbuff.WriteByte(0x00);
                        //C0 A8 00 79
                        bbuff.WriteByte(0xC0);
                        bbuff.WriteByte(0xA8);
                        bbuff.WriteByte(0x00);
                        bbuff.WriteByte(0x79);
                        break;
                }

                //00 00 00 00
                bbuff.WriteByte(0x00);
                bbuff.WriteByte(0x00);
                bbuff.WriteByte(0x00);
                bbuff.WriteByte(0x00);

                //00 00 00 00
                bbuff.WriteByte(0x00);
                bbuff.WriteByte(0x00);
                bbuff.WriteByte(0x00);
                bbuff.WriteByte(0x00);
                //00 00 00 00
                bbuff.WriteByte(0x00);
                bbuff.WriteByte(0x00);
                bbuff.WriteByte(0x00);
                bbuff.WriteByte(0x00);
                //00 00 00 00 
                bbuff.WriteByte(0x00);
                bbuff.WriteByte(0x00);
                bbuff.WriteByte(0x00);
                bbuff.WriteByte(0x00);

                if (Globals.enterworld_ip)//(adifenix) - change ip bytes
                {
                    Random rand = new Random();
                    bbuff.SetIndex(85);
                    int temp_int = new int();
                    for (int i = 0; i < Globals.ew_chc_ed_array.Count; i++)
                    {
                        if ((Globals.ew_chc_ed_array[i] as System.Windows.Forms.CheckBox).Checked)
                        {
                            bbuff.WriteByte((byte)rand.Next(1, 255));
                        }
                        else// grab text from edit
                        {
                            if ((Globals.ew_con_array[i] as System.Windows.Forms.TextBox).TextLength > 0)
                            {
                                try
                                {
                                    temp_int = System.Convert.ToInt32((Globals.ew_con_array[i] as System.Windows.Forms.TextBox).Text);
                                }
                                catch (FormatException) // non digit chars
                                {
                                    temp_int = rand.Next(1, 255);
                                }
                                if (temp_int >= 0 && temp_int <= 255)
                                {
                                    bbuff.WriteByte((byte)temp_int);
                                }
                            }
                            else // text legh == 0  so gen random ip
                            {
                                temp_int = rand.Next(1, 255);
                                bbuff.WriteByte((byte)temp_int);
                            }
                        }

                    }
                }
                Globals.gamedata.SendToGameServer(bbuff);
            }
        }

        public static void Send_GameGuardVerify(string query)
        {
            //gg login packets
            //old
            //07 xx xx xx xx 45 00 01 00 1e 37 a2 f5 00 00 00 00 00 00 00 00 00 00 00 f2 a6 bd 81 00 00 00 00
            //new
            //07 xx xx xx xx 23 92 90 4D 18 30 B5 7C 96 61 41 47 05 07 96 FB 00 00 00 8A 55 4E D0 00 00 00 00
            //end of gg login packets

            //que: F9 04 CA 64 FC 3F DE F5 02 AB 66 E5 AE 17 B5 0C A3 
            //new: CA B6 14 57 E9 D9 DC 98 BA 5D 6D EE AF 02 62 71 E1

            //new: CA 73 FD A1 F4 F8 DC 2B D1 B8 F5 F5 50 98 19 82 96 
            //old: CA 45 00 01 00 1E 37 A2 F5 00 00 00 00 00 00 00 00

            //*************//

            //que: 74 D9 3D 53 27 1D A5 72 2E 8B 03 17 20 A3 1E 5B C3 
            //CT1: CB 7F 97 F0 78 04 3C E6 D6 71 0C F6 89 DD 9E 06 70

            //if we know the answer... reply

            if (Globals.GG_List.ContainsKey(query))
            {
                byte[] reply = (byte[])Globals.GG_List[query];

                if (Globals.Script_Debugging)
                {
                    string gg = "";

                    for (int i = 0; i < reply.Length; i++)
                    {
                        gg += reply[i].ToString("X2") + " ";
                    }

                    gg = gg.Trim();

                    Globals.l2net_home.Add_Debug("GameGuard Reply: " + gg);
                }

                ByteBuffer bbuff = new ByteBuffer(17);

                bbuff.WriteByte((byte)PClient.GameGuardReply);
                bbuff.WriteByte(reply[0]);//0x7F);//
                bbuff.WriteByte(reply[1]);//0x97);
                bbuff.WriteByte(reply[2]);//0xF0);
                bbuff.WriteByte(reply[3]);//0x78);
                bbuff.WriteByte(reply[4]);//0x04);//
                bbuff.WriteByte(reply[5]);//0x3C);
                bbuff.WriteByte(reply[6]);//0xE6);
                bbuff.WriteByte(reply[7]);//0xD6);
                bbuff.WriteByte(reply[8]);//0x71);//
                bbuff.WriteByte(reply[9]);//0x0C);
                bbuff.WriteByte(reply[10]);//0xF6);
                bbuff.WriteByte(reply[11]);//0x89);
                bbuff.WriteByte(reply[12]);//0xDD);//
                bbuff.WriteByte(reply[13]);//0x9E);
                bbuff.WriteByte(reply[14]);//0x06);
                bbuff.WriteByte(reply[15]);//0x70);

                Globals.gamedata.SendToGameServer(bbuff);
            }
            else
            {
                if (Globals.Send_Blank_GG)
                {
                    Globals.l2net_home.Add_Error("Unknown GG query... blank reply sent");

                    ByteBuffer bbuff = new ByteBuffer(17);

                    bbuff.WriteByte((byte)PClient.GameGuardReply);
                    bbuff.WriteInt32(0);
                    bbuff.WriteInt32(0);
                    bbuff.WriteInt32(0);
                    bbuff.WriteInt32(0);

                    Globals.gamedata.SendToGameServer(bbuff);
                }
                else
                {
                    Globals.l2net_home.Add_Error("Unknown GG query... no reply sent");
                }
            }
        }

        public static void Send_GGInit()
        {
            ByteBuffer bb = new ByteBuffer(18);

            bb.WriteByte((byte)PClient.GameGuardReply);
            bb.WriteByte(Globals.gamedata.gameguardInit[0]);
            bb.WriteByte(Globals.gamedata.gameguardInit[1]);
            bb.WriteByte(Globals.gamedata.gameguardInit[2]);
            bb.WriteByte(Globals.gamedata.gameguardInit[3]);
            bb.WriteByte(Globals.gamedata.gameguardInit[4]);
            bb.WriteByte(Globals.gamedata.gameguardInit[5]);
            bb.WriteByte(Globals.gamedata.gameguardInit[6]);
            bb.WriteByte(Globals.gamedata.gameguardInit[7]);
            bb.WriteByte(Globals.gamedata.gameguardInit[8]);
            bb.WriteByte(Globals.gamedata.gameguardInit[9]);
            bb.WriteByte(Globals.gamedata.gameguardInit[10]);
            bb.WriteByte(Globals.gamedata.gameguardInit[11]);
            bb.WriteByte(Globals.gamedata.gameguardInit[12]);
            bb.WriteByte(Globals.gamedata.gameguardInit[13]);
            bb.WriteByte(Globals.gamedata.gameguardInit[14]);
            bb.WriteByte(Globals.gamedata.gameguardInit[15]);
            bb.WriteByte(0x01);

            Globals.gamedata.SendToGameServer(bb);

            Globals.l2net_home.Add_Text("Sent GG init packet", Globals.Green, TextType.BOT);
        }

        public static void RequestInventory()
        {
            ByteBuffer bbuff = new ByteBuffer(1);

            bbuff.WriteByte((byte)PClient.RequestItemList);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void RequestSkillList()
        {
            ByteBuffer bbuff = new ByteBuffer(1);

            bbuff.WriteByte((byte)PClient.RequestSkillList);

            Globals.gamedata.SendToGameServer(bbuff);

            /***************/

            bbuff = new ByteBuffer(1);

            bbuff.WriteByte((byte)PClient.RequestSkillCoolTime);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Send_Text(int index, string total_text)
        {
            //did they even say anything?
            if (total_text.Length == 0)
                return;

            bool ok = false;
            string start = "";
            uint type = 0;
            string end = "";
            int marker;

            //change the first thing if needed
            if (index == 0)
            {
                switch (total_text[0])
                {
                    case '!'://shout
                        index = 1;
                        total_text = total_text.Substring(1, total_text.Length - 1);
                        break;
                    case '"'://tell
                        index = 2;
                        total_text = total_text.Substring(1, total_text.Length - 1);
                        break;
                    case '+'://trade
                        index = 8;
                        total_text = total_text.Substring(1, total_text.Length - 1);
                        break;
                    case '#'://party
                        index = 3;
                        total_text = total_text.Substring(1, total_text.Length - 1);
                        break;
                    case '@'://clan
                        index = 4;
                        total_text = total_text.Substring(1, total_text.Length - 1);
                        break;
                    case '$'://ally
                        index = 9;
                        total_text = total_text.Substring(1, total_text.Length - 1);
                        break;
                    case '%'://hero
                        index = 17;
                        total_text = total_text.Substring(1, total_text.Length - 1);
                        break;
                    case '&'://reply to gm
                        index = 7;
                        total_text = total_text.Substring(1, total_text.Length - 1);
                        break;
                    case '/'://run command
                        ServerPackets.Run_Command(total_text);
                        index = -1;
                        break;
                }
            }

            if (total_text.Length > Globals.MAX_MESSAGE_LEN)
            {
                total_text = total_text.Substring(0, Globals.MAX_MESSAGE_LEN);
            }

            switch (index)
            {
                case 0://all 0x00
                    type = 0x00;
                    start = total_text;
                    ok = true;
                    break;
                case 1://shout 0x01
                    type = 0x01;
                    start = total_text;
                    ok = true;
                    break;
                case 2://tell player 0x02
                    type = 0x02;
                    string inp = total_text;

                    marker = inp.IndexOf(' ');
                    try
                    {
                        end = inp.Substring(0, marker);
                    }
                    catch
                    {
                        end = "";
                    }
                    inp = inp.Remove(0, marker + 1);
                    start = inp;
                    ok = true;
                    break;
                case 3://party 0x03
                    type = 0x03;
                    start = total_text;
                    ok = true;
                    break;
                case 4://clan 0x04
                    type = 0x04;
                    start = total_text;
                    ok = true;
                    break;
                case 5://from gm 0x05
                    Globals.l2net_home.Add_Text("GM only tell -no message sent]", Globals.Red, TextType.ALL);
                    break;
                case 6://reply to petition from 0x06
                    type = 0x06;
                    start = total_text;
                    ok = true;
                    break;
                case 7://petition from gm 0x07
                    Globals.l2net_home.Add_Text("GM only chat in petition box -no message sent]", Globals.Red, TextType.ALL);
                    break;
                case 8://trade 0x08
                    type = 0x08;
                    start = total_text;
                    ok = true;
                    break;
                case 9://alliance 0x09
                    type = 0x09;
                    start = total_text;
                    ok = true;
                    break;
                case 10://announcement 0x0A
                    Globals.l2net_home.Add_Text("announcement -no message sent]", Globals.Red, TextType.ALL);
                    break;
                case 11://crash client 0x0B
                    Globals.l2net_home.Add_Text("boat message -no message sent]", Globals.Red, TextType.SYSTEM);
                    break;
                case 12://fake1 0x0C
                    Globals.l2net_home.Add_Text("bad -no message sent]", Globals.Red, TextType.BOT);
                    break;
                case 13://fake2 0x0D
                    Globals.l2net_home.Add_Text("bad -no message sent]", Globals.Red, TextType.BOT);
                    break;
                case 14://fake3 0x0E
                    Globals.l2net_home.Add_Text("bad -no message sent]", Globals.Red, TextType.BOT);
                    break;
                case 15://party room all 0x0F
                    Globals.l2net_home.Add_Text("text to party room members -no message sent]", Globals.Red, TextType.PARTY);
                    break;
                case 16://party room commander 0x10
                    type = 0x10;
                    start = total_text;
                    ok = true;
                    break;
                case 17://hero 0x11
                    type = 0x11;
                    start = total_text;
                    ok = true;
                    break;
            }

            if (!ok)
            {
                return;
            }

            if (start.StartsWith("--"))
            {
                start = start.Substring(1, start.Length - 1);

                ServerPackets.Send_Text(type, start, end);
            }
            else if (start.StartsWith("-"))
            {
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
            else
            {
                //need to build a packet and send it out
                ServerPackets.Send_Text(type, start, end);
            }
        }

        public static void Send_Text(uint type, string start, string end)
        {
            int startlen = start.Length * 2 + 2;
            int endlen = end.Length * 2;
            if (endlen > 0)
                endlen += 2;

            ByteBuffer bbuff = new ByteBuffer(5 + startlen + endlen);

            bbuff.WriteByte((byte)PClient.Say2);

            bbuff.WriteString(start);

            bbuff.WriteUInt32(type);
            //bbuff.WriteByte(type[0]);
            //bbuff.WriteByte(type[1]);
            //bbuff.WriteByte(type[2]);
            //bbuff.WriteByte(type[3]);

            if (endlen > 0)
            {
                bbuff.WriteString(end);
            }

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void JoinPartyReply(bool yes)
        {
            ByteBuffer bbuff = new ByteBuffer(5);

            bbuff.WriteByte((byte)PClient.RequestAnswerJoinParty);

            if (yes)
            {
                bbuff.WriteUInt32(1);
            }
            else
            {
                bbuff.WriteUInt32(0);
            }

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void JoinClanReply(bool yes)
        {
            ByteBuffer bbuff = new ByteBuffer(5);

            bbuff.WriteByte((byte)PClient.RequestAnswerJoinPledge);

            if (yes)
            {
                //join clan
                bbuff.WriteUInt32(1);
            }
            else
            {
                //decline
                bbuff.WriteUInt32(0);
            }

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void JoinFriendReply(bool yes)
        {
            ByteBuffer bbuff = new ByteBuffer(5);

            bbuff.WriteByte((byte)PClient.RequestAnswerFriendInvite);

            if (yes)
            {
                //join friend
                bbuff.WriteUInt32(1);
            }
            else
            {
                //decline
                bbuff.WriteUInt32(0);
            }

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void JoinAllyReply(bool yes)
        {//TODO
            /*ByteBuffer bbuff = new ByteBuffer(5);

            bbuff.WriteByte((byte)PClient.RequestAnswerJoinAlly);

            if(yes)
            {
                //join ally
                bbuff.WriteUInt32(1);
            }
            else
            {
                //decline
                bbuff.WriteUInt32(0);
            }

            Globals.gamedata.SendToGameServer(bbuff);*/
        }

        public static void DialogReply(bool yes)
        {
            ByteBuffer bbuff = new ByteBuffer(13);

            bbuff.WriteByte((byte)PClient.DlgAnswer);
            bbuff.WriteUInt32(Globals.LastRezz1);
            //bbuff.WriteByte(0xE6);//
            //bbuff.WriteByte(0x05);
            //bbuff.WriteByte(0x00);
            //bbuff.WriteByte(0x00);

            //rezz: ED E6 5 0 0 2 0 0 0 0 0 0 0 4E 0 61 0 7A 0 61 0 72 0 61 0 74 0 0 0 1 0 0 0 0 0 0 0 
            //rezz: C5 E6 5 0 0 1 0 0 0 0 0 0 0

            //summon: F3 32 07 00 00 02 00 00 00 00 00 00 00 4D 00 79 00 50 00 72 00 65 00 63 00 69 00 6F 00 75 00 73 00 00 00 07 00 00 00 12 CE FF FF EE DF 01 00 D8 F3 FF FF 30 75 00 00 E1 6A D0 4B 
            //summon: C6 32 07 00 00 01 00 00 00 E1 6A D0 4B 
            if (yes)
            {
                //Rezz
                bbuff.WriteUInt32(1);

            }
            else
            {
                //decline
                bbuff.WriteUInt32(0);
            }

            bbuff.WriteUInt32(Globals.LastRezz2);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void TradeReply(bool yes)
        {
            ByteBuffer bbuff = new ByteBuffer(5);

            bbuff.WriteByte((byte)PClient.RequestAnswerTrade);

            if (yes)
            {
                //trade
                bbuff.WriteUInt32(1);
                Globals.gamedata.SendToGameServer(bbuff);

                if (Globals.tradewindow == null || Globals.tradewindow.IsDisposed == true)
                {
                    Globals.tradewindow = new TradeWindow();
                }
                Globals.tradewindow.TopMost = true;
                Globals.tradewindow.BringToFront();
                Globals.tradewindow.Show();
            }
            else
            {
                //decline
                bbuff.WriteUInt32(0);
                Globals.gamedata.SendToGameServer(bbuff);
            }


        }

        public static void Nick(string name)
        {
            ByteBuffer bbuff = new ByteBuffer(Globals.gamedata.my_char.Name.Length * 2 + name.Length * 2 + 5);

            bbuff.WriteByte((byte)PClient.RequestGiveNickName);
            bbuff.WriteString(Globals.gamedata.my_char.Name);
            bbuff.WriteString(name);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void ClickNearest(string name)
        {
            name = name.ToUpperInvariant();

            if (System.String.Equals(name, Globals.gamedata.my_char.Name.ToUpperInvariant()))
            {
                ClickChar(Globals.gamedata.my_char.ID, Util.Float_Int32(Globals.gamedata.my_char.X), Util.Float_Int32(Globals.gamedata.my_char.Y), Util.Float_Int32(Globals.gamedata.my_char.Z), false, false);
                return;
            }

            if (Globals.gamedata.my_pet.ID != 0)
            {
                if (Globals.gamedata.my_pet.Name.ToUpperInvariant() == name)
                {
                    ClickChar(Globals.gamedata.my_pet.ID, Util.Float_Int32(Globals.gamedata.my_pet.X), Util.Float_Int32(Globals.gamedata.my_pet.Y), Util.Float_Int32(Globals.gamedata.my_pet.Z), false, false);
                    return;
                }
            }

            CharInfo player = null;

            Globals.PlayerLock.EnterReadLock();
            try
            {
                player = Util.GetChar(name);
            }
            finally
            {
                Globals.PlayerLock.ExitReadLock();
            }

            if (player != null)
            {
                ClickChar(player.ID, Util.Float_Int32(player.X), Util.Float_Int32(player.Y), Util.Float_Int32(player.Z), false, false);
                return;
            }

            PartyMember party = null;

            Globals.PartyLock.EnterReadLock();
            try
            {
                party = Util.GetCharParty(name);
            }
            finally
            {
                Globals.PartyLock.ExitReadLock();
            }

            if (party != null)
            {
                ClickChar(party.ID, party.X, party.Y, party.Z, false, false);
                return;
            }

            uint t_id = 0;
            float mx = Globals.gamedata.my_char.X;
            float my = Globals.gamedata.my_char.Y;
            float mz = Globals.gamedata.my_char.Z;
            float dist = float.MaxValue;
            float ndist;
            float x = 0, y = 0, z = 0;

            Globals.NPCLock.EnterReadLock();
            try
            {
                foreach (NPCInfo npc in Globals.gamedata.nearby_npcs.Values)
                {
                    if (System.String.Equals(name, Util.GetNPCName(npc.NPCID).ToUpperInvariant()))
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
            }//unlock
            finally
            {
                Globals.NPCLock.ExitReadLock();
            }

            if (t_id != 0)
            {
                ClickChar(t_id, Util.Float_Int32(x), Util.Float_Int32(y), Util.Float_Int32(z), false, false);
            }
        }

        public static void ClickNearest(uint id)
        {
            //really fast check if we are targeting ourselves
            if (id == Globals.gamedata.my_char.ID)
            {
                ClickChar(id, Util.Float_Int32(Globals.gamedata.my_char.X), Util.Float_Int32(Globals.gamedata.my_char.Y), Util.Float_Int32(Globals.gamedata.my_char.Z), false, false);
                return;
            }

            if (id == Globals.gamedata.my_pet.ID)
            {
                ClickChar(id, Util.Float_Int32(Globals.gamedata.my_pet.X), Util.Float_Int32(Globals.gamedata.my_pet.Y), Util.Float_Int32(Globals.gamedata.my_pet.Z), false, false);
                return;
            }

            //most likely case, target a nearby mob
            uint t_id = 0;
            float mx = Globals.gamedata.my_char.X;
            float my = Globals.gamedata.my_char.Y;
            float mz = Globals.gamedata.my_char.Z;
            float dist = float.MaxValue;
            float ndist;
            float x = 0, y = 0, z = 0;

            Globals.NPCLock.EnterReadLock();
            try
            {
                foreach (NPCInfo npc in Globals.gamedata.nearby_npcs.Values)
                {
                    if (id == npc.NPCID)
                    {
                        //lets get the distance
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
            finally
            {
                Globals.NPCLock.ExitReadLock();
            }

            if (t_id != 0)
            {
                ClickChar(t_id, Util.Float_Int32(x), Util.Float_Int32(y), Util.Float_Int32(z), false, false);
                return;
            }

            //check players just incase
            CharInfo player = null;

            Globals.PlayerLock.EnterReadLock();
            try
            {
                player = Util.GetChar(id);
            }
            finally
            {
                Globals.PlayerLock.ExitReadLock();
            }

            if (player != null)
            {
                ClickChar(id, Util.Float_Int32(player.X), Util.Float_Int32(player.Y), Util.Float_Int32(player.Z), false, false);
                return;
            }

            PartyMember party = null;

            Globals.PartyLock.EnterReadLock();
            try
            {
                party = Util.GetCharParty(id);
            }
            finally
            {
                Globals.PartyLock.ExitReadLock();
            }

            if (party != null)
            {
                ClickChar(id, party.X, party.Y, party.Z, false, false);
                return;
            }
        }

        public static void ClickOBJ(uint id, bool control, bool shift)
        {
            TargetType type = Util.GetType(id);

            switch (type)
            {
                case TargetType.SELF:
                    ClickChar(id, Util.Float_Int32(Globals.gamedata.my_char.X), Util.Float_Int32(Globals.gamedata.my_char.Y), Util.Float_Int32(Globals.gamedata.my_char.Z), control, shift);
                    break;
                case TargetType.MYPET:
                    ClickChar(id, Util.Float_Int32(Globals.gamedata.my_pet.X), Util.Float_Int32(Globals.gamedata.my_pet.Y), Util.Float_Int32(Globals.gamedata.my_pet.Z), control, shift);
                    break;
                case TargetType.MYPET1:
                    ClickChar(id, Util.Float_Int32(Globals.gamedata.my_pet1.X), Util.Float_Int32(Globals.gamedata.my_pet1.Y), Util.Float_Int32(Globals.gamedata.my_pet1.Z), control, shift);
                    break;
                case TargetType.MYPET2:
                    ClickChar(id, Util.Float_Int32(Globals.gamedata.my_pet2.X), Util.Float_Int32(Globals.gamedata.my_pet2.Y), Util.Float_Int32(Globals.gamedata.my_pet2.Z), control, shift);
                    break;
                case TargetType.MYPET3:
                    ClickChar(id, Util.Float_Int32(Globals.gamedata.my_pet3.X), Util.Float_Int32(Globals.gamedata.my_pet3.Y), Util.Float_Int32(Globals.gamedata.my_pet3.Z), control, shift);
                    break;
                case TargetType.PLAYER:
                    CharInfo player = null;

                    Globals.PlayerLock.EnterReadLock();
                    try
                    {
                        player = Util.GetChar(id);
                    }
                    finally
                    {
                        Globals.PlayerLock.ExitReadLock();
                    }

                    if (player != null)
                    {
                        ClickChar(id, Util.Float_Int32(player.X), Util.Float_Int32(player.Y), Util.Float_Int32(player.Z), control, shift);
                    }
                    break;
                case TargetType.NPC:
                    NPCInfo npc = null;

                    Globals.NPCLock.EnterReadLock();
                    try
                    {
                        npc = Util.GetNPC(id);
                    }
                    finally
                    {
                        Globals.NPCLock.ExitReadLock();
                    }

                    if (npc != null)
                    {
                        ClickChar(id, Util.Float_Int32(npc.X), Util.Float_Int32(npc.Y), Util.Float_Int32(npc.Z), control, shift);
                    }
                    break;
                case TargetType.ITEM:
                    ItemInfo itm = null;

                    Globals.ItemLock.EnterReadLock();
                    try
                    {
                        itm = Util.GetItem(id);
                    }
                    finally
                    {
                        Globals.ItemLock.ExitReadLock();
                    }

                    if (itm != null)
                    {
                        ClickItem(id, Util.Float_Int32(itm.X), Util.Float_Int32(itm.Y), Util.Float_Int32(itm.Z), shift);
                    }
                    break;
            }
        }

        public static void ClickChar(uint id, int x, int y, int z, bool control, bool shift)
        {
            //used to click on npcs and players
            if (control)
            {
                Force_Attack(id, x, y, z, shift);
            }
            else
            {
                Target(id, x, y, z, shift);
            }
        }

        public static void ClickItem(uint objID, int x, int y, int z, bool shift)
        {
            Target(objID, x, y, z, shift);
        }

        public static void DropItem(uint objID, ulong count, int _x, int _y, int _z)
        {
            ByteBuffer bbuff;

            if (Globals.gamedata.Chron >= Chronicle.CT2_3)
            {
                bbuff = new ByteBuffer(25);
            }
            else
            {
                bbuff = new ByteBuffer(21);
            }

            bbuff.WriteByte((byte)PClient.RequestDropItem);
            bbuff.WriteUInt32(objID);
            if (Globals.gamedata.Chron >= Chronicle.CT2_3)
            {
                bbuff.WriteUInt64(count);
            }
            else
            {
                bbuff.WriteUInt32((uint)count);
            }
            bbuff.WriteInt32(_x);
            bbuff.WriteInt32(_y);
            bbuff.WriteInt32(_z);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void DeleteItem(uint objID, ulong count)
        {
            ByteBuffer bbuff;

            if (Globals.gamedata.Chron >= Chronicle.CT2_3)
            {
                bbuff = new ByteBuffer(13);
            }
            else
            {
                bbuff = new ByteBuffer(9);
            }

            bbuff.WriteByte((byte)PClient.RequestDestroyItem);
            bbuff.WriteUInt32(objID);
            if (Globals.gamedata.Chron >= Chronicle.CT2_3)
            {
                bbuff.WriteUInt64(count);
            }
            else
            {
                bbuff.WriteUInt32((uint)count);
            }

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void CrystalizeItem(uint objID, ulong count)
        {
            //RequestCrystalizeItemCancel D0   92 00 
            ByteBuffer bbuff1 = new ByteBuffer(3);
            bbuff1 = new ByteBuffer(3);
            bbuff1.WriteUInt16((byte)PClientEX.RequestCrystallizeCancel);


            //RequestCrystalizeEstimate D0   91 00   45 56 27 42   01 00 00 00 00 00 00 00 
            ByteBuffer bbuff2 = new ByteBuffer(15);
            bbuff2.WriteByte((byte)PClient.EXPacket);
            bbuff2.WriteUInt16((byte)PClientEX.RequestCrystallizeEstimate);
            bbuff2.WriteUInt32(objID);
            bbuff2.WriteUInt64(count);

            //RequestCrystalize 2F   45 56 27 42   01 00 00 00 00 00 00 00 
            ByteBuffer bbuff3 = new ByteBuffer(13);
            bbuff3.WriteByte((byte)PClient.RequestCrystallizeItem);
            bbuff3.WriteUInt32(objID);
            bbuff3.WriteUInt64(count);

            Globals.gamedata.SendToGameServer(bbuff1);
            System.Threading.Thread.Sleep(100);
            Globals.gamedata.SendToGameServer(bbuff2);
            System.Threading.Thread.Sleep(250);
            Globals.gamedata.SendToGameServer(bbuff3);
        }

        public static void Send_Logout()
        {
            ByteBuffer bbuff = new ByteBuffer(1);

            bbuff.WriteByte((byte)PClient.Logout);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Send_Restart()
        {
            ByteBuffer bbuff = new ByteBuffer(1);

            bbuff.WriteByte((byte)PClient.RequestRestart);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Send_CancelTarget()
        {
            ByteBuffer bbuff = new ByteBuffer(3);

            bbuff.WriteByte((byte)PClient.RequestTargetCanceled);
            bbuff.WriteByte(0x00);
            bbuff.WriteByte(0x00);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void GM_Command(string cmdtext)
        {//TODO //gm stuff
            /*cmdtext = cmdtext.Substring(2, cmdtext.Length - 2);

            int startlen = cmdtext.Length * 2 + 2;

            ByteBuffer bbuff = new ByteBuffer(1 + startlen);

            bbuff.WriteByte((byte)PClient.SendBypassBuildCmd);

            bbuff.WriteString(cmdtext);

            Globals.gamedata.SendToGameServer(bbuff);*/
        }

        public static void Run_Command(string cmdtext)
        {
            if (cmdtext[0] != '/')//if this isnt a command, ditch it
            {
                return;
            }

            if (cmdtext.Length > 1 && cmdtext[1] == '/')
            {
                //gm command thingy
                GM_Command(cmdtext);
                return;
            }

            cmdtext = cmdtext.Substring(1, cmdtext.Length - 1);

            int space = cmdtext.IndexOf(" ", 0, cmdtext.Length);

            string cmd = "";

            if (space > 0)
            {
                cmd = cmdtext.Substring(0, space);
                cmdtext = cmdtext.Remove(0, space + 1);
            }
            else
            {
                //do nothing
                cmd = cmdtext;
                cmdtext = "";
            }

            cmd = cmd.ToUpperInvariant();

            switch (cmd)
            {
                case "DEADCOUNT":
                    AddInfo.Dead_Count();
                    break;
                case "DWARVENMANUFACTURE":
                    Use_Action_Parse((uint)PClientAction.Open_DwarvenManufacture, Globals.gamedata.Control, false);
                    break;
                case "VENDOR":
                    Social_ActionNew((byte)PClientAction.Open_PrivateStore_Sell); //Same for CT 1.0 to CT 2.4+ servers
                    if (Globals.privatestoresellwindow == null || Globals.privatestoresellwindow.IsDisposed == true)
                    {
                        Globals.privatestoresellwindow = new PrivateStoreSellWindow();
                    }
                    Globals.privatestoresellwindow.TopMost = true;
                    Globals.privatestoresellwindow.BringToFront();
                    Globals.privatestoresellwindow.Show();
                    break;
                case "BUY":
                    Use_Action_Parse((int)PClientAction.Open_PrivateStore_Buy, Globals.gamedata.Control, Globals.gamedata.Shift);
                    break;
                case "GENERALMANUFACTURE":
                    Use_Action_Parse((int)PClientAction.Open_GeneralManufacture, Globals.gamedata.Control, Globals.gamedata.Shift);
                    break;
                case "GETSKILLS":
                    ServerPackets.RequestSkillList();
                    break;
                case "SPAUSE":
                    Globals.gamedata.CurrentScriptState = ScriptState.Paused;
                    break;
                case "SRESUME":
                    Globals.gamedata.CurrentScriptState = ScriptState.Running;
                    break;
                case "SSTOP":
                    Globals.gamedata.CurrentScriptState = ScriptState.Stopped;
                    break;
                case "BOTSTATE":
                    switch (Globals.gamedata.BOT_STATE)
                    {
                        case BotState.Nothing:
                            Globals.l2net_home.Add_Text("BOT_STATE: Nothing");
                            break;
                        case BotState.Attacking:
                            Globals.l2net_home.Add_Text("BOT_STATE: Attacking/Moving");
                            break;
                        case BotState.Buffing:
                            Globals.l2net_home.Add_Text("BOT_STATE: Buffing");
                            break;
                        case BotState.BuffWaiting:
                            Globals.l2net_home.Add_Text("BOT_STATE: Buff Waiting");
                            break;
                        case BotState.FinishedBuffing:
                            Globals.l2net_home.Add_Text("BOT_STATE: Finished Buffing");
                            break;
                    }
                    break;
                case "ATTACK":
                    ServerPackets.ClickOBJ(Globals.gamedata.my_char.TargetID, Globals.gamedata.Control, Globals.gamedata.Shift);
                    break;
                case "USESKILL":
                    ServerPackets.Use_Skill(cmdtext);
                    break;
                case "TARGET":
                    ServerPackets.Target(cmdtext);
                    break;
                case "ASSIST":
                    if (cmdtext.Length != 0)
                        ServerPackets.Assist(cmdtext);
                    else
                        ServerPackets.Assist();
                    break;
                case "PLAYERLOC":
                    ServerPackets.Player_Loc(cmdtext);
                    break;
                case "FORCELOGOUT":
                    Util.Stop_Connections();
                    break;
                case "LOGOUT":
                    ServerPackets.Send_Logout();
                    break;
                case "RESTART":
                    ServerPackets.Send_Restart();
                    break;
                case "HIDEACCEPT":
                    Globals.l2net_home.Hide_YesNo();
                    break;
                case "HIDEDEAD":
                    Globals.l2net_home.Hide_Dead();
                    break;
                case "FLUSH":
                    Util.Flush_TextFile();
                    break;
                case "CLS":
                    Globals.l2net_home.Clear_ChatBox();
                    break;
                case "GMLIST":
                    ServerPackets.Command_GMList();
                    break;
                case "SCRIPT":
                    Globals.scriptthread.Proccess_Line(cmdtext, false);
                    break;
                case "INVITE"://send party invite
                    ServerPackets.Command_Invite(cmdtext);
                    break;
                case "DISMISS":
                    ServerPackets.Command_Dismiss(cmdtext);
                    break;
                case "CHANGEPARTYLEADER":
                    ServerPackets.Command_ChangePartyLeader(cmdtext);
                    break;
                case "SOCIALHELLO":
                    ServerPackets.Command_SocialHello();
                    break;
                case "SOCIALVICTORY":
                    ServerPackets.Command_SocialVictory();
                    break;
                case "SOCIALCHARGE":
                    ServerPackets.Command_SocialCharge();
                    break;
                case "SOCIALNO":
                    ServerPackets.Command_SocialNo();
                    break;
                case "SOCIALYES":
                    ServerPackets.Command_SocialYes();
                    break;
                case "SOCIALBOW":
                    ServerPackets.Command_SocialBow();
                    break;
                case "SOCIALUNAWARE":
                    ServerPackets.Command_SocialUnaware();
                    break;
                case "SOCIALWAITING":
                    ServerPackets.Command_SocialWaiting();
                    break;
                case "SOCIALLAUGH":
                    ServerPackets.Command_SocialLaugh();
                    break;
                case "SOCIALAPPLAUSE":
                    ServerPackets.Command_SocialApplause();
                    break;
                case "SOCIALDANCE":
                    ServerPackets.Command_SocialDance();
                    break;
                case "SOCIALSAD":
                    ServerPackets.Command_SocialSad();
                    break;
                case "CHARM":
                    ServerPackets.Command_SocialCharm();
                    break;
                case "NSOCIALLEVELUP":
                    ServerPackets.Command_NSocialLevelUp();
                    //Add_Text("[level up animation]",Gray);
                    break;
                case "NSOCIALHERO":
                    ServerPackets.Command_NSocialHero();
                    //Add_Text("[become hero animation]",Gray);
                    break;
                case "NSOCIALFLAME"://C5 emote only :O
                    ServerPackets.Command_NSocialFlame();
                    //Add_Text("[flame animation]",Gray);
                    break;
                case "SIT":
                    Use_Action_Parse((uint)PClientAction.SitStand, Globals.gamedata.Control, false);
                    //Add_Text("[sit]",Gray);
                    break;
                case "STAND":
                    Use_Action_Parse((uint)PClientAction.SitStand, Globals.gamedata.Control, false);
                    //Add_Text("[stand]",Gray);
                    break;
                case "LEAVE":
                    ServerPackets.Command_Leave();
                    //Add_Text("[leave party]",Gray);
                    break;
                case "WALK":
                    Use_Action_Parse((int)PClientAction.RunWalk, Globals.gamedata.Control, Globals.gamedata.Shift);
                    //Add_Text("[/walk]",Gray);
                    break;
                case "RUN":
                    Use_Action_Parse((int)PClientAction.RunWalk, Globals.gamedata.Control, Globals.gamedata.Shift);
                    //Add_Text("[/run]",Gray);
                    break;
                case "BUTTONSIT":
                    Use_Action_Parse((uint)PClientAction.SitStand, Globals.gamedata.Control, false);
                    //	Add_Text("[button sit/stand]",Gray);
                    break;
                case "BUTTONWALK":
                    Use_Action_Parse((uint)PClientAction.SitStand, Globals.gamedata.Control, false);
                    //	Add_Text("[button walk/run]",Gray);
                    break;
                //https://opensvn.csie.org/traccgi/l2jc4/browser/trunk/L2_Gameserver/java/net/sf/l2j/gameserver/clientpackets/RequestMagicSkillUse.java
                case "SKILL":
                    uint sid = Util.GetUInt32(cmdtext);
                    ServerPackets.Try_Use_Skill(sid, Globals.gamedata.Control, Globals.gamedata.Shift);
                    //Add_Text("[skill]",Gray);
                    break;
                case "UNSTUCK":
                    ServerPackets.Command_Unstuck();
                    break;
                case "LOC":
                    ServerPackets.Command_Loc();
                    break;
                case "MOUNT":
                    ServerPackets.Command_Mount();
                    break;
                case "DISMOUNT":
                    ServerPackets.Command_Dismount();
                    break;
                case "TIME":
                    ServerPackets.Command_Time();
                    break;
                case "PARTYINFO":
                    ServerPackets.Command_PartyInfo();
                    break;
                case "ATTACKLIST":
                    ServerPackets.Command_AttackList();
                    break;
                case "WARLIST":
                    ServerPackets.Command_WarList();
                    break;
                case "CLANPENALTY":
                    ServerPackets.Command_ClanPenalty();
                    break;
                case "INSTANCEZONE":
                    ServerPackets.Command_InstanceZone();
                    break;
                case "MYBIRTHDAY":
                    ServerPackets.Command_MyBirthday();
                    break;
                case "FRIENDLIST":
                    ServerPackets.Command_FriendList();
                    break;
                case "FRIENDINVITE":
                    ServerPackets.Command_FriendInvite(cmdtext);
                    break;
                case "FRIENDDEL":
                    ServerPackets.Command_FriendDel(cmdtext);
                    break;
                case "BLOCK":
                    ServerPackets.Command_Block(cmdtext);
                    break;
                case "UNBLOCK":
                    ServerPackets.Command_Unblock(cmdtext);
                    break;
                case "BLOCKLIST":
                    ServerPackets.Command_BlockList();
                    break;
                case "EVALUATE":
                    ServerPackets.Command_Evaluate(Globals.gamedata.my_char.TargetID);
                    break;
                case "TRADE":
                    if (Globals.tradewindow == null || Globals.tradewindow.IsDisposed == true)
                    {
                        Globals.tradewindow = new TradeWindow();
                    }
                    Globals.tradewindow.TopMost = true;
                    Globals.tradewindow.BringToFront();
                    Globals.tradewindow.Show();
                    ServerPackets.Command_Trade();
                    break;
                case "NICK":
                    ServerPackets.Nick(cmdtext);
                    break;
                case "GODMODE":
                    //lol yeah right...
                    System.Diagnostics.Process.Start("http://www.youtube.com/watch?v=eBGIQ7ZuuiU");
                    break;
                case "ALLBLOCK":
                    ServerPackets.Command_AllBlock();
                    break;
                case "ALLUNBLOCK":
                    ServerPackets.Command_AllUnblock();
                    break;
                case "GMCANCEL":
                    //8A C7 45 FC FF FF FF FF 8B 4D F4 64 89 0D 00 00 00 
                    //static?
                    break;
                case "CLANWARSTART":
                    ServerPackets.Command_ClanWarStart(cmdtext);
                    break;
                case "CLANWARSTOP":
                    ServerPackets.Command_ClanWarStop(cmdtext);
                    break;
                case "SIEGESTATUS":
                    ServerPackets.Command_SiegeStatus();
                    break;
                case "DELETEALLIANCECREST":
                    //91 00 00 00 00
                    //maybe the alliance crest id?
                    break;
                case "ALLYINFO":
                    ServerPackets.Command_AllyInfo();
                    break;
                case "DUEL":
                    //no target
                    //48 00 00 - cancel target
                    ServerPackets.Command_Duel(cmdtext);
                    break;
                case "PARTYDUEL":
                    ServerPackets.Command_PartyDuel(cmdtext);
                    break;
                case "WITHDRAW":
                    ServerPackets.Command_Withdraw();
                    break;
                case "PETATTACK":
                    Use_Action_Parse((uint)PClientAction.Pet_Attack, Globals.gamedata.Control, false);
                    break;
            }
        }

        public static void Command_GMList()
        {
            ByteBuffer bbuff = new ByteBuffer(1);

            bbuff.WriteByte((byte)PClient.RequestGmList);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Command_Invite(string cmdtext)//,int lootstyle)
        {
            ByteBuffer bbuff = new ByteBuffer(cmdtext.Length * 2 + 2 + 5);

            bbuff.WriteByte((byte)PClient.RequestJoinParty);
            bbuff.WriteString(cmdtext);

            //loot
            //1 - random
            bbuff.WriteUInt32(Globals.gamedata.LootType);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Command_Dismiss(string cmdtext)
        {
            ByteBuffer bbuff = new ByteBuffer(cmdtext.Length * 2 + 2 + 1);

            bbuff.WriteByte((byte)PClient.RequestOustPartyMember);
            bbuff.WriteString(cmdtext);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Command_ChangePartyLeader(string cmdtext)
        {
            ByteBuffer bbuff = new ByteBuffer(cmdtext.Length * 2 + 2 + 3);

            bbuff.WriteByte((byte)PClient.EXPacket);
            bbuff.WriteByte((byte)PClientEX.RequestChangePartyLeader);
            bbuff.WriteByte(0x00);
            bbuff.WriteString(cmdtext);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Command_Duel(string cmdtext)
        {
            ByteBuffer bbuff = new ByteBuffer(cmdtext.Length * 2 + 2 + 7);

            bbuff.WriteByte((byte)PClient.EXPacket);
            bbuff.WriteByte((byte)PClientEX.RequestDuelStart);
            bbuff.WriteByte(0x00);
            bbuff.WriteString(cmdtext);
            bbuff.WriteInt32(0x00);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Command_PartyDuel(string cmdtext)
        {
            ByteBuffer bbuff = new ByteBuffer(cmdtext.Length * 2 + 2 + 7);

            bbuff.WriteByte((byte)PClient.EXPacket);
            bbuff.WriteByte((byte)PClientEX.RequestDuelStart);
            bbuff.WriteByte(0x00);
            bbuff.WriteString(cmdtext);
            bbuff.WriteInt32(0x01);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Command_Withdraw()
        {
            ByteBuffer bbuff = new ByteBuffer(3);

            bbuff.WriteByte((byte)PClient.EXPacket);
            bbuff.WriteByte((byte)PClientEX.RequestDuelSurrender);
            bbuff.WriteByte(0x00);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Social_Action(int id)
        {
            ByteBuffer bbuff = new ByteBuffer(5);

            bbuff.WriteByte((byte)PClient.RequestSocialAction);
            bbuff.WriteInt32(id);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Social_ActionNew(byte id)
        {
            ByteBuffer bbuff = new ByteBuffer(10);

            bbuff.WriteByte((byte)PClient.RequestActionUse);
            bbuff.WriteByte(id);
            bbuff.WriteInt32(0); //1 when /socialhello 0 when you click on the button
            bbuff.WriteInt32(0); //1 when /socialhello 0 when you click on the button

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Command_SocialHello()
        {
            if (Globals.gamedata.Chron >= Chronicle.CT2_4)
            {
                Social_ActionNew((byte)PClientAction.Social_Greeting);
            }
            else
            {
                Social_Action(0x02);
            }
        }

        public static void Command_SocialVictory()
        {
            if (Globals.gamedata.Chron >= Chronicle.CT2_4)
            {
                Social_ActionNew((byte)PClientAction.Social_Victory);
            }
            else
            {
                Social_Action(0x03);
            }
        }

        public static void Command_SocialCharge()
        {
            if (Globals.gamedata.Chron >= Chronicle.CT2_4)
            {
                Social_ActionNew((byte)PClientAction.Social_Advance);
            }
            else
            {
                Social_Action(0x04);
            }
        }

        public static void Command_SocialNo()
        {
            if (Globals.gamedata.Chron >= Chronicle.CT2_4)
            {
                Social_ActionNew((byte)PClientAction.Social_No);
            }
            else
            {
                Social_Action(0x05);
            }
        }

        public static void Command_SocialYes()
        {
            if (Globals.gamedata.Chron >= Chronicle.CT2_4)
            {
                Social_ActionNew((byte)PClientAction.Social_Yes);
            }
            else
            {
                Social_Action(0x06);
            }
        }

        public static void Command_SocialBow()
        {
            if (Globals.gamedata.Chron >= Chronicle.CT2_4)
            {
                Social_ActionNew((byte)PClientAction.Social_Bow);
            }
            else
            {
                Social_Action(0x07);
            }
        }

        public static void Command_SocialUnaware()
        {
            if (Globals.gamedata.Chron >= Chronicle.CT2_4)
            {
                Social_ActionNew((byte)PClientAction.Social_Unaware);
            }
            else
            {
                Social_Action(0x08);
            }
        }

        public static void Command_SocialWaiting()
        {
            if (Globals.gamedata.Chron >= Chronicle.CT2_4)
            {
                Social_ActionNew((byte)PClientAction.Social_Waiting);
            }
            else
            {
                Social_Action(0x09);
            }
        }

        public static void Command_SocialLaugh()
        {
            if (Globals.gamedata.Chron >= Chronicle.CT2_4)
            {
                Social_ActionNew((byte)PClientAction.Social_Laugh);
            }
            else
            {
                Social_Action(0x0A);
            }
        }

        public static void Command_SocialApplause()
        {
            if (Globals.gamedata.Chron >= Chronicle.CT2_4)
            {
                Social_ActionNew((byte)PClientAction.Social_Applaud);
            }
            else
            {
                Social_Action(0x0B);
            }
        }

        public static void Command_SocialDance()
        {
            if (Globals.gamedata.Chron >= Chronicle.CT2_4)
            {
                Social_ActionNew((byte)PClientAction.Social_Dance);
            }
            else
            {
                Social_Action(0x0C);
            }
        }

        public static void Command_SocialSad()
        {
            if (Globals.gamedata.Chron >= Chronicle.CT2_4)
            {
                Social_ActionNew((byte)PClientAction.Social_Sorrow);
            }
            else
            {
                Social_Action(0x0D);
            }
        }

        public static void Command_SocialCharm()
        {
            if (Globals.gamedata.Chron >= Chronicle.CT2_4)
            {
                Social_ActionNew((byte)PClientAction.Social_Charm);
            }
            else if (Globals.gamedata.Chron >= Chronicle.CT2_3)
            {
                Social_Action(0x0E);
            }
            else
            {
            }
        }

        public static void Command_SocialShyness()
        {
            if (Globals.gamedata.Chron >= Chronicle.CT2_4)
            {
                Social_ActionNew((byte)PClientAction.Social_Shyness);
            }
            else
            {
            }
        }

        public static void Command_NSocialLevelUp()
        {
            if (Globals.gamedata.Chron >= Chronicle.CT2_4)
            {
            }
            else
            {
                Social_Action(0x0F);
            }
        }

        public static void Command_NSocialHero()
        {
            if (Globals.gamedata.Chron >= Chronicle.CT2_4)
            {
            }
            else
            {
                Social_Action(0x10);
            }
        }

        public static void Command_NSocialFlame()
        {
            if (Globals.gamedata.Chron >= Chronicle.CT2_4)
            {
            }
            else
            {
                Social_Action(0x11);
            }
        }

        public static void Command_Leave()
        {
            ByteBuffer bbuff = new ByteBuffer(1);

            bbuff.WriteByte((byte)PClient.RequestWithDrawalParty);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void User_Command(int id)
        {
            ByteBuffer bbuff = new ByteBuffer(5);
            bbuff.WriteByte((byte)PClient.RequestUserCommand);
            bbuff.WriteInt32(id);
            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Command_Unstuck()
        {
            //send the unstuck thingy
            User_Command((byte)PClientCmd.Stuck);

            //send our location thingy
            Send_Verify();
        }

        public static void Command_Loc()
        {
            User_Command((byte)PClientCmd.Loc);
        }

        public static void Command_Mount()
        {
            User_Command((byte)PClientCmd.Mount);
        }

        public static void Command_Dismount()
        {
            User_Command((byte)PClientCmd.Dismount);
        }

        public static void Command_Time()
        {
            User_Command((byte)PClientCmd.Time);
        }

        public static void Command_PartyInfo()
        {
            User_Command((byte)PClientCmd.PartyInfo);
        }

        public static void Command_AttackList()
        {
            User_Command((byte)PClientCmd.AttackList);
        }

        public static void Command_WarList()
        {
            User_Command((byte)PClientCmd.WarList);
        }

        public static void Command_ClanPenalty()
        {
            User_Command((byte)PClientCmd.ClanPenalty);
        }

        public static void Command_InstanceZone()
        {
            User_Command((byte)PClientCmd.InstanceZone);
        }

        public static void Command_SiegeStatus()
        {
            User_Command((byte)PClientCmd.SiegeStatus);
        }

        public static void Command_MyBirthday()
        {
            User_Command((byte)PClientCmd.MyBirthday);
        }

        public static void Command_ClanWarStart(string cmdtext)
        {
            ByteBuffer bbuff = new ByteBuffer(cmdtext.Length * 2 + 2 + 1);

            bbuff.WriteByte((byte)PClient.RequestStartPledgeWar);
            bbuff.WriteString(cmdtext);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Command_ClanWarStop(string cmdtext)
        {
            ByteBuffer bbuff = new ByteBuffer(cmdtext.Length * 2 + 2 + 1);

            bbuff.WriteByte((byte)PClient.RequestStopPledgeWar);
            bbuff.WriteString(cmdtext);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Command_FriendList()
        {
            ByteBuffer bbuff = new ByteBuffer(1);

            bbuff.WriteByte((byte)PClient.RequestFriendList);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Command_FriendInvite(string cmdtext)
        {
            ByteBuffer bbuff = new ByteBuffer(cmdtext.Length * 2 + 2 + 1);

            bbuff.WriteByte((byte)PClient.RequestFriendInvite);
            bbuff.WriteString(cmdtext);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Command_FriendDel(string cmdtext)
        {
            ByteBuffer bbuff = new ByteBuffer(cmdtext.Length * 2 + 2 + 1);

            bbuff.WriteByte((byte)PClient.RequestFriendDel);
            bbuff.WriteString(cmdtext);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Command_Block(string cmdtext)
        {
            ByteBuffer bbuff = new ByteBuffer(cmdtext.Length * 2 + 2 + 5);

            bbuff.WriteByte((byte)PClient.RequestBlock);
            bbuff.WriteUInt32(0);
            bbuff.WriteString(cmdtext);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Command_Unblock(string cmdtext)
        {
            ByteBuffer bbuff = new ByteBuffer(cmdtext.Length * 2 + 2 + 5);

            bbuff.WriteByte((byte)PClient.RequestBlock);
            bbuff.WriteUInt32(1);
            bbuff.WriteString(cmdtext);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Command_AllBlock()
        {
            ByteBuffer bbuff = new ByteBuffer(5);

            bbuff.WriteByte((byte)PClient.RequestBlock);
            bbuff.WriteUInt32(3);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Command_AllUnblock()
        {
            ByteBuffer bbuff = new ByteBuffer(5);

            bbuff.WriteByte((byte)PClient.RequestBlock);
            bbuff.WriteUInt32(4);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Command_BlockList()
        {
            ByteBuffer bbuff = new ByteBuffer(5);

            bbuff.WriteByte((byte)PClient.RequestBlock);
            bbuff.WriteUInt32(2);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Command_AllyInfo()
        {
            ByteBuffer bbuff = new ByteBuffer(1);

            bbuff.WriteByte((byte)PClient.RequestAllyInfo);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Command_Evaluate(uint id)
        {
            if (Globals.gamedata.Chron >= Chronicle.CT2_5)
            {
                ByteBuffer bbuff = new ByteBuffer(7);

                bbuff.WriteByte((byte)PClient.EXPacket);
                bbuff.WriteByte((byte)PClientEX.RequestEvaluate);
                bbuff.WriteByte(0x00);
                bbuff.WriteUInt32(id);

                Globals.gamedata.SendToGameServer(bbuff);

            }
            else
            {
                ByteBuffer bbuff = new ByteBuffer(5);

                bbuff.WriteByte((byte)PClient.RequestEvaluate);
                bbuff.WriteUInt32(id);

                Globals.gamedata.SendToGameServer(bbuff);
            }
        }

        public static void Command_Trade()
        {
            ByteBuffer bbuff = new ByteBuffer(5);

            bbuff.WriteByte((byte)PClient.TradeRequest);
            bbuff.WriteUInt32(Globals.gamedata.my_char.TargetID);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Send_Friend_Message(string name, string message)
        {
            ByteBuffer bbuff = new ByteBuffer(5 + name.Length * 2 + message.Length * 2);

            bbuff.WriteByte((byte)PClient.RequestSendFriendMsg);
            bbuff.WriteString(message);
            bbuff.WriteString(name);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void RequestGiveItemToPet(uint id, ulong quantity)
        {
            ByteBuffer bbuff = new ByteBuffer(13);
            bbuff.WriteByte((byte)PClient.RequestGiveItemToPet);
            bbuff.WriteUInt32(id);
            bbuff.WriteUInt64(quantity);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void RequestGetItemFromPet(uint id, ulong quantity)
        {
            ByteBuffer bbuff = new ByteBuffer(17);
            bbuff.WriteByte((byte)PClient.RequestGetItemFromPet);
            bbuff.WriteUInt32(id);
            bbuff.WriteUInt64(quantity);
            bbuff.WriteUInt32(0x00);

            Globals.gamedata.SendToGameServer(bbuff);
        }

        public static void Player_Loc(string name)
        {
            //need to loop thru all the players and find their location
            int x = 0, y = 0, z = 0;

            uint id = Util.GetCharID(name);
            Util.GetCharLoc(id, ref x, ref y, ref z);

            Globals.l2net_home.Add_Text(name + " at : " + Util.Float_Int32(x).ToString() + ", " + Util.Float_Int32(y).ToString() + ", " + Util.Float_Int32(z).ToString());
        }

        public static void Assist(string name)
        {
            //need to assist the named target
            uint target_id = Util.GetCharID(name);

            Assist(target_id);
        }

        public static void Assist()
        {
            uint target_id = Globals.gamedata.my_char.TargetID;

            Assist(target_id);
        }

        private static void Assist(uint target_id)
        {
            if (target_id == 0)
            {
                //do nothing
                return;
                //found = true;
            }

            TargetType target_type = Util.GetType(target_id);
            float x = 0, y = 0, z = 0;
            bool found = false;

            switch (target_type)
            {
                case TargetType.SELF:
                    x = Globals.gamedata.my_char.X;
                    y = Globals.gamedata.my_char.Y;
                    z = Globals.gamedata.my_char.Z;
                    found = true;
                    break;
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
                case TargetType.PLAYER:
                    CharInfo player = null;

                    Globals.PlayerLock.EnterReadLock();
                    try
                    {
                        player = Util.GetChar(target_id);
                    }
                    finally
                    {
                        Globals.PlayerLock.ExitReadLock();
                    }

                    if (player != null)
                    {
                        target_id = player.TargetID;
                        x = player.X;
                        y = player.Y;
                        z = player.Z;
                        found = true;
                    }
                    break;
                case TargetType.NPC:
                    NPCInfo npc = null;

                    Globals.NPCLock.EnterReadLock();
                    try
                    {
                        npc = Util.GetNPC(target_id);
                    }
                    finally
                    {
                        Globals.NPCLock.ExitReadLock();
                    }

                    if (npc != null)
                    {
                        target_id = npc.TargetID;
                        x = npc.X;
                        y = npc.Y;
                        z = npc.Z;
                        found = true;
                    }
                    break;
            }

            if (!found)
            {
                //oh well
            }
            else
            {
                ServerPackets.Target(target_id, Util.Float_Int32(x), Util.Float_Int32(y), Util.Float_Int32(z), Globals.gamedata.Shift);
            }
        }
    }//end of class
}
