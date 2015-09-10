using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;//do wyj

namespace L2_login
{
    public class pck_window_thr
    {

        public bool pck_recording = false;
        public bool save_visable = true;
        public bool wind_combo_set = false;// fal = server / true client
        public System.Collections.Concurrent.ConcurrentQueue<pck_window_dat> mine_queue = new System.Collections.Concurrent.ConcurrentQueue<pck_window_dat>();
        //public static List<pck_window_dat> mine_queue = new List<pck_window_dat>(); // for actions
        public List<pck_window_dat> pck_record = new List<pck_window_dat>(); // for data
        public packet_window pck_window;
        public Packet_filter_window pck_fill_window;
        public bool filter_wind_pck = false; // filter on/off
        public List<int> filtered_pck = new List<int>();//int = index in the main pck_record ...
        public List<int> client_pck_filter = new List<int>();
        public  List<int> server_pck_filter = new List<int>();
        public bool folow_new_pck = false;
        public  bool search_pck_names = false;
        public bool white_list_filter = false;
        public List<System.Windows.Forms.ListViewItem> temp_cache_for_pck_window = new List<System.Windows.Forms.ListViewItem>(); // for data

        //
        //public static List<System.Windows.Forms.ListViewItem> cache_for_pck_window[] = new List<System.Windows.Forms.ListViewItem>(10000); // for data
        // public static System.Windows.Forms.ListViewItem[] cache_for_pck_window = new System.Windows.Forms.ListViewItem[100000]; // for data

        //
        public System.Collections.SortedList client_pck_names = new System.Collections.SortedList();
        public System.Collections.SortedList server_pck_names = new System.Collections.SortedList();

        // conv
        public converter conv_obj = new converter();

        // temp filter pck/
        public List<int> tmp_client_pck_filter = new List<int>();
        public List<int> tmp_server_pck_filter = new List<int>();
        public bool hide_cli_pck = false;
        public bool hide_srv_pck = false;
        //---------------------------------------------------------
        //private object lock_obj = new object();
        public bool run = false;
        private System.DateTime time_check1; // var for tiem check - update list
        private Thread newthr;
        private pck_window_dat queue_safe_object = new pck_window_dat();
        public void stop()
        {
            run = false;
        }
        public void start()
        {
            run = true;
            newthr = new Thread(pck_proces);
            time_check1 = System.DateTime.Now.AddSeconds(1);
            newthr.Start();
           }
           private void pck_proces()
            {
               /*
                * action 1 = add to array + window
                *           2 = clean list 
                *           3 = load data
                *           4 = save data
                *           5 = refresh 
                *           6 = normal filter = temp filter (apply changes)
                *           7 = save filters
                *           8 = load filters
                *           9 = search (from begining)
                *           10 = search up(from selected)
                *           11 = search down(from selected)
                *           12 = search from beg by pck name
                *           13 = search down (pck name)
                *           14 = search up (pck name)
                *           --------------
                * typ 1 = client
                *      2 = server
                *      ----------------------------------------------------
                *     todo: new updater for pck window based on time + index 
                * */

                while (run)
                {
                    while (Globals.pck_thread.mine_queue.Count > 0)
                    {
                      //  lock (lock_obj)
                       // {
                        if (Globals.pck_thread.mine_queue.TryDequeue(out queue_safe_object))
                        {
                            if (queue_safe_object.action == 1) // add to arraylist + add to window
                            {
                                new_add_pck(queue_safe_object);
                            }
                            if (queue_safe_object.action == 2) // clear list
                            {
                                clean_data();
                            }
                            if (queue_safe_object.action == 3) //load
                            {
                                Load_data(queue_safe_object);
                            }
                            if (queue_safe_object.action == 4) // save
                            {
                                save_data(queue_safe_object);
                            }
                            if (queue_safe_object.action == 5) // refresh
                            {
                                //add pack to filtered list ....
                                if (Globals.pck_thread.filter_wind_pck == true)
                                {
                                    create_filtered_list();
                                }
                                Globals.pck_thread.pck_window.refresh_window();
                            }
                            if (queue_safe_object.action == 6) //copy temp filters to normal 1 (apply changes)
                            {
                                apply_filters();
                            }
                            if (queue_safe_object.action == 7) //save filters
                            {
                                save_filters();
                            }
                            if (queue_safe_object.action == 8) //load filters
                            {
                                load_filters();
                            }
                            if (queue_safe_object.action == 9) //search from beg
                            {
                                search_from_beg(queue_safe_object);
                            }
                            if (queue_safe_object.action == 10) //search from index up
                            {
                                search_up(queue_safe_object);
                            }
                            if (queue_safe_object.action == 11) //search from index down
                            {
                                search_down(queue_safe_object);
                            }
                            if (queue_safe_object.action == 12) //search from beg by pck name
                            {
                                search_from_beg_by_name(queue_safe_object);
                            }
                            if (queue_safe_object.action == 13) //search down (name)
                            {
                                search_down_by_name(queue_safe_object);
                            }
                            if (queue_safe_object.action == 14) //search up (name)
                            {
                                search_up_by_name(queue_safe_object);
                            }
                            
                        }
                        Thread.Sleep(0);
                            //Globals.pck_thread.mine_queue.TryDequeue();
                            //Globals.pck_thread.mine_queue.RemoveAt(0);
                       // }
                    }
                    listview_updater(); // anti lag solution for listviev in pck window ....
                    Thread.Sleep(0);
                    Thread.Sleep(1);
                    // updater

            }
        }
           private void new_add_pck(pck_window_dat tmp_dat)
           {
               try
               {
                   Globals.pck_thread.pck_record.Add(tmp_dat);
                   if (Globals.pck_thread.filter_wind_pck == true)
                   {
                       if (tmp_dat.type == 1)// client pck
                       {
                           if (check_client_pck_in_filter(tmp_dat))
                           {
                               if (Globals.pck_thread.white_list_filter)
                               {
                                   if (!hide_cli_pck)// hide client pck
                                   {
                                       int temp_count = Globals.pck_thread.pck_record.Count - 1;
                                       Globals.pck_thread.filtered_pck.Add(temp_count);
                                       //Globals.pck_thread.pck_window.add_one_to_cache();
                                       Globals.pck_thread.pck_window.add_one_to_temp_cache(tmp_dat);
                                   }
                               }
                               //Globals.pck_thread.pck_window.add_to_list(Globals.pck_thread.mine_queue.First()); // add to normal list
                           }
                           else// not in filter list ...
                           {
                               if (!Globals.pck_thread.white_list_filter)
                               {
                                   if (!hide_cli_pck)// hide client pck
                                   {
                                       int temp_count = Globals.pck_thread.pck_record.Count - 1;
                                       Globals.pck_thread.filtered_pck.Add(temp_count);
                                       //Globals.pck_thread.pck_window.add_one_to_cache();
                                       Globals.pck_thread.pck_window.add_one_to_temp_cache(tmp_dat);
                                   }
                               }
                           }
                       }
                       else // server pck
                       {
                           if (check_Serwer_pck_in_filter(tmp_dat))
                           {

                               if (Globals.pck_thread.white_list_filter)
                               {
                                   if (!hide_srv_pck)// hide client pck
                                   {
                                       int temp_count = Globals.pck_thread.pck_record.Count - 1;
                                       Globals.pck_thread.filtered_pck.Add(temp_count);
                                       //Globals.pck_thread.pck_window.add_one_to_cache();
                                       Globals.pck_thread.pck_window.add_one_to_temp_cache(tmp_dat);
                                   }
                               }
                               //Globals.pck_thread.pck_window.add_to_list(Globals.pck_thread.mine_queue.First()); // add to normal list

                           }
                           else// not in filter list ...
                           {
                               if (!Globals.pck_thread.white_list_filter)
                               {
                                   if (!hide_srv_pck)// hide client pck
                                   {
                                       int temp_count = Globals.pck_thread.pck_record.Count - 1;
                                       Globals.pck_thread.filtered_pck.Add(temp_count);
                                       //Globals.pck_thread.pck_window.add_one_to_cache();
                                       Globals.pck_thread.pck_window.add_one_to_temp_cache(tmp_dat);
                                   }
                               }
                           }
                       }
                   }
                   else // without pck filter
                   {
                       //Globals.pck_thread.pck_window.add_one_to_cache();
                       Globals.pck_thread.pck_window.add_one_to_temp_cache(tmp_dat);
                   }
               }
               catch
               {

               }


           }
           private void add_pck(pck_window_dat tmp_dat)
           {
               try
               {
                   Globals.pck_thread.pck_record.Add(tmp_dat);
                   if (Globals.pck_thread.filter_wind_pck == true)
                   {
                       if (tmp_dat.type == 1)// client pck
                       {
                           if (check_client_pck_in_filter(tmp_dat))
                           {
                               if (Globals.pck_thread.white_list_filter)
                               {
                                   if (!hide_cli_pck)// hide client pck
                                   {
                                       int temp_count = Globals.pck_thread.pck_record.Count - 1;
                                       Globals.pck_thread.filtered_pck.Add(temp_count);
                                       //Globals.pck_thread.pck_window.add_one_to_cache();
                                       Globals.pck_thread.pck_window.add_to_list(tmp_dat);
                                   }
                               }
                               //Globals.pck_thread.pck_window.add_to_list(Globals.pck_thread.mine_queue.First()); // add to normal list
                           }
                           else// not in filter list ...
                           {
                               if (!Globals.pck_thread.white_list_filter)
                               {
                                   if (!hide_cli_pck)// hide client pck
                                   {
                                       int temp_count = Globals.pck_thread.pck_record.Count - 1;
                                       Globals.pck_thread.filtered_pck.Add(temp_count);
                                       //Globals.pck_thread.pck_window.add_one_to_cache();
                                       Globals.pck_thread.pck_window.add_to_list(tmp_dat);
                                   }
                               }
                           }
                           // spr w liscie filtra
                           // ex packi tez
                           // jak niema to dodanie do filtered
                           // dodanei do okna
                       }
                       else // server pck
                       {
                           if (check_Serwer_pck_in_filter(tmp_dat))
                           {

                               if (Globals.pck_thread.white_list_filter)
                               {
                                   if (!hide_srv_pck)// hide client pck
                                   {
                                       int temp_count = Globals.pck_thread.pck_record.Count - 1;
                                       Globals.pck_thread.filtered_pck.Add(temp_count);
                                       //Globals.pck_thread.pck_window.add_one_to_cache();
                                       Globals.pck_thread.pck_window.add_to_list(tmp_dat);
                                   }
                               }
                               //Globals.pck_thread.pck_window.add_to_list(Globals.pck_thread.mine_queue.First()); // add to normal list

                           }
                           else// not in filter list ...
                           {
                               if (!Globals.pck_thread.white_list_filter)
                               {
                                   if (!hide_srv_pck)// hide client pck
                                   {
                                       int temp_count = Globals.pck_thread.pck_record.Count - 1;
                                       Globals.pck_thread.filtered_pck.Add(temp_count);
                                       //Globals.pck_thread.pck_window.add_one_to_cache();
                                       Globals.pck_thread.pck_window.add_to_list(tmp_dat);
                                   }
                               }
                           }
                       }
                   }
                   else // without pck filter
                   {
                       //Globals.pck_thread.pck_window.add_one_to_cache();
                       Globals.pck_thread.pck_window.add_to_list(tmp_dat);
                   }
               }
               catch
               {

               }
           }
        private void save_data(pck_window_dat tmp_dat)
        {
            try
            {
                    //System.IO.BinaryWriter test =  new BinaryWriter(File.Open(tmp_dat.time, FileMode.Create));
                    if (save_visable)
                    {
                        if (filter_wind_pck == true)
                        {
                            System.IO.BinaryWriter test = new BinaryWriter(File.Open(tmp_dat.time, FileMode.Create));
                            test.Write(filtered_pck.Count);
                            for (int i = 0; i < filtered_pck.Count; i++)
                            {
                                test.Write((Globals.pck_thread.pck_record[filtered_pck[i]]).action);
                                test.Write((Globals.pck_thread.pck_record[filtered_pck[i]]).type);
                                test.Write((Globals.pck_thread.pck_record[filtered_pck[i]]).time);
                                test.Write((Globals.pck_thread.pck_record[filtered_pck[i]]).bytebuffer.Length);
                                //test.Write(Globals.pck_thread.pck_record[i]).bytebuffer);
                                for (int j = 0; j < (Globals.pck_thread.pck_record[filtered_pck[i]]).bytebuffer.Length; j++)
                                    test.Write((Globals.pck_thread.pck_record[filtered_pck[i]]).bytebuffer[j]);
                            }
                            test.Flush();
                        }
                        else
                        {
                            rec_fun(tmp_dat);
                        }
                    }
                    else
                    {
                        rec_fun(tmp_dat);
                        /*
                        test.Write(Globals.pck_thread.pck_record.Count);
                        for (int i = 0; i < Globals.pck_thread.pck_record.Count; i++)
                        {
                            test.Write((Globals.pck_thread.pck_record[i]).action);
                            test.Write((Globals.pck_thread.pck_record[i]).type);
                            test.Write((Globals.pck_thread.pck_record[i]).time);
                            test.Write((Globals.pck_thread.pck_record[i]).bytebuffer.Length);
                            //test.Write(Globals.pck_thread.pck_record[i]).bytebuffer);
                            for (int j = 0; j < (Globals.pck_thread.pck_record[i]).bytebuffer.Length; j++)
                                test.Write((Globals.pck_thread.pck_record[i]).bytebuffer[j]);
                        }
                        test.Flush();
                         */
                    }
            }

            catch
            {
                // to do ...
            }

        }
        private void rec_fun(pck_window_dat tmp_dat)
        {
            try
            {
                System.IO.BinaryWriter test = new BinaryWriter(File.Open(tmp_dat.time, FileMode.Create));
                test.Write(Globals.pck_thread.pck_record.Count);
                for (int i = 0; i < Globals.pck_thread.pck_record.Count; i++)
                {
                    test.Write((Globals.pck_thread.pck_record[i]).action);
                    test.Write((Globals.pck_thread.pck_record[i]).type);
                    test.Write((Globals.pck_thread.pck_record[i]).time);
                    test.Write((Globals.pck_thread.pck_record[i]).bytebuffer.Length);
                    //test.Write(Globals.pck_thread.pck_record[i]).bytebuffer);
                    for (int j = 0; j < (Globals.pck_thread.pck_record[i]).bytebuffer.Length; j++)
                        test.Write((Globals.pck_thread.pck_record[i]).bytebuffer[j]);
                }
                test.Flush();
            }
            catch
            {

            }
        }
        private void Load_data(pck_window_dat tmp_dat)
        {
            try
            {
                clean_data();
                System.IO.BinaryReader redfil = new BinaryReader(File.Open(tmp_dat.time, FileMode.Open));
                int temp_ilo = redfil.ReadInt32();
                pck_window_dat temp_dat;
                for (int i = 0; i < temp_ilo; i++)
                {
                    temp_dat = new pck_window_dat();
                    temp_dat.action = redfil.ReadInt32();
                    temp_dat.type = redfil.ReadInt32();
                    temp_dat.time = redfil.ReadString();
                    temp_dat.bytebuffer = redfil.ReadBytes(redfil.ReadInt32());

                    //for (int j = 0; j < temp_dat.bytebuffer.Length; j++)
                    //{
                       // temp_dat.bytebuffer[j] = redfil.ReadByte();
                    //}
                    Globals.pck_thread.pck_record.Add(temp_dat);
                }
                if (Globals.pck_thread.filter_wind_pck == true)
                {
                    create_filtered_list();
                }
                Globals.pck_thread.pck_window.refresh_window();
                //Globals.pck_thread.pck_recording = true;

            }
            catch
            {
                // to do ...
            }
        }
           private void clean_data()
         {
             //Globals.pck_thread.pck_window.clear_cache();
            Globals.pck_thread.temp_cache_for_pck_window.Clear();
             Globals.pck_thread.filtered_pck.Clear();
             Globals.pck_thread.pck_record.Clear();
             Globals.pck_thread.pck_window.refresh_window();
           }
           private bool check_client_pck_in_filter(pck_window_dat tmp_dat)
           {
               try
               {
                   if (tmp_dat.bytebuffer[0] != 0xd0)// not ex client pck
                   {
                       for (int i = 0; i < Globals.pck_thread.client_pck_filter.Count; i++)
                       {
                           if(tmp_dat.bytebuffer[0] ==  Globals.pck_thread.client_pck_filter[i])
                           {
                               return true;
                           }
                       }
                       return false;
                   }
                   else // id ts ex ...
                   {
                        int temp_id = System.BitConverter.ToUInt16(tmp_dat.bytebuffer,1);
                        temp_id=temp_id+0xd0;
                        for (int i = 0; i < Globals.pck_thread.client_pck_filter.Count; i++)
                         {
                            if(temp_id ==  Globals.pck_thread.client_pck_filter[i])
                            {
                               return true;
                            }
                          }
                       return false;
                   }

               }
               catch
               {
                   return false;
                    // len packet wrong ...
               }
           }
           private bool check_Serwer_pck_in_filter(pck_window_dat tmp_dat)
           {
               try
               {
                   if (tmp_dat.bytebuffer[0] != 0xfe)// not ex client pck
                   {
                       for (int i = 0; i < Globals.pck_thread.server_pck_filter.Count; i++)
                       {
                           if (tmp_dat.bytebuffer[0] == Globals.pck_thread.server_pck_filter[i])
                           {
                               return true;
                           }
                       }
                       return false;
                   }
                   else // id ts ex ...
                   {
                       byte[] temp_array = new byte[4];
                       temp_array[0] = tmp_dat.bytebuffer[0];
                       temp_array[1] = tmp_dat.bytebuffer[1];
                       temp_array[2] = tmp_dat.bytebuffer[2];
                       int temp_id = System.BitConverter.ToInt32(temp_array, 0);
                       //temp_id = temp_id + 0xfe;
                       for (int i = 0; i < Globals.pck_thread.server_pck_filter.Count; i++)
                       {
                           if (temp_id == Globals.pck_thread.server_pck_filter[i])
                           {
                               return true;
                           }
                       }
                       return false;
                   }

               }
               catch
               {
                   return false;
                   // len packet wrong ...
               }
           }
           public byte[] StringToByteArray2(string hex)
           {
               byte[] bytes = new byte[hex.Length / 2];
               int bl = bytes.Length;
               for (int i = 0; i < bl; ++i)
               {
                   bytes[i] = (byte)((hex[2 * i] > 'F' ? hex[2 * i] - 0x57 : hex[2 * i] > '9' ? hex[2 * i] - 0x37 : hex[2 * i] - 0x30) << 4);
                   bytes[i] |= (byte)(hex[2 * i + 1] > 'F' ? hex[2 * i + 1] - 0x57 : hex[2 * i + 1] > '9' ? hex[2 * i + 1] - 0x37 : hex[2 * i + 1] - 0x30);
               }
               return bytes;
           }
          /* public string get_cli_pck_name(byte[] bbufer)
           {
               switch (bbufer[0])
               #region cli packets
               {
                   //case 0x1f:
                   // return "[RequestMoveToMerchant]";
                   case 0x0a:
                       return "[RequestRefreshPrivateMarketInfo]";
                   case 0x6e:
                       return "[RequestReload]";
                   case 0x6d:
                       return "[RequestSendMsnChatLog]";
                   case 0x6b:
                       return "[RequestSendL2FriendSay]";
                   case 0xcb:
                       return "[ReplyGameGuardQuery]";
                   case 0xc8:
                       return "[RequestSSQStatus]";
                   case 0xc6:
                       return "[ConfirmDlg]";
                   case 0xc2:
                       return "[VoteSociality]";
                   case 0xcc:
                       return "[RequestPledgePower]";
                   case 0xc1:
                       return "[RequestObserverEndPacket]";
                   case 0xb3:
                       return "[BypassUserCmd]";
                   case 0xb2:
                       return "[RequestRemainTime]";
                   case 0xb4:
                       return "[Snoop_quit]";
                   case 0xaf:
                       return "[RequestSetCastleSiegeTime]";
                   case 0xae:
                       return "[RequestConfirmCastleSiegeWaitingList]";
                   case 0xad:
                       return "[RequestJoinCastleSiege]";
                   case 0xac:
                       return "[RequestCastleSiegeInfo]";
                   case 0xab:
                       return "[RequestCastleSiegeAttackerList]";
                   case 0xaa:
                       return "[RequestCastleSiegeInfo]";
                   case 0xa7:
                       return "[RequestPackageSendableItemList]";
                   case 0xa6:
                       return "[RequestSkillCoolTime]";
                   case 0x70:
                       return "[RequestHennaUnequipList]";
                   case 0xc3:
                       return "[RequestHennaItemList]";
                   case 0x72:
                       return "[RequestHennaUnequip]";
                   case 0x71:
                       return "[RequestHennaUnequipInfo]";
                   case 0x6f:
                       return "[RequestHennaEquip]";
                   case 0xc4:
                       return "[RequestHennaItemInfo]";
                   case 0xbf:
                       return "[RequestRecipeShopMakeDo]";
                   case 0xbe:
                       return "[RequestRecipeShopMakeInfo]";
                   case 0xc0:
                       return "[RequestRecipeShopSellList]";
                   case 0xbd:
                       return "[RequestRecipeShopManageCancel]";
                   case 0xbc:
                       return "[RequestRecipeShopManageQuit]";
                   case 0xba:
                       return "[RequestRecipeShopMessageSet]";
                   case 0xb8:
                       return "[RequestRecipeItemMakeSelf]";
                   case 0xb7:
                       return "[RequestRecipeItemMakeInfo]";
                   case 0xb6:
                       return "[RequestRecipeItemDelete]";
                   case 0xb5:
                       return "[RequestRecipeBookOpen]";
                   case 0x93:
                       return "[RequestChangePetName]";
                   //case 0x17:
                   // return "[RequestDraopItemPacket]";
                   case 0x94:
                       return "[RequestPetUseItem]";
                   case 0x98:
                       return "[RequestPetGetItem]";
                   case 0x8b:
                       return "[RequestGMList]";
                   case 0x8a:
                       return "[RequestPetitionCancel]";
                   case 0x89:
                       return "[RequestPetition]";
                   case 0x7e:
                       return "[RequestGMCommand]";
                   case 0x7c:
                       return "[RequestAcquireSkill]";
                   case 0x73:
                       return "[RequestAcquireSkillInfo]";
                   case 0x5f:
                       return "[RequestEnchantItem]";
                   case 0x63:
                       return "[RequestDestroyQuest]";
                   case 0x62:
                       return "[RequestQuestList]";
                   case 0x2f:
                       return "[RequestCrystallizeItem]";
                   case 0x60:
                       return "[RequestDestroyItem]";
                   // case 0x60:
                   //return "[SendDestroyItem]";
                   case 0x74:
                       return "[SendBypassBuildCmd]";
                   case 0x5e:
                       return "[RequestShowboard]";
                   case 0x5a:
                       return "[RequestSEKCustom]";
                   case 0x7d:
                       return "[RequestRestartPoint]";
                   //case 0x1f:
                   //return "[Action]";
                   case 0x56:
                       return "[RequestActionUse]";
                   case 0x55:
                       return "[Answer]";
                   case 0x50:
                       return "[Request]";
                   case 0x16:
                       return "[Request]";
                   case 0x00:
                       return "[Logout]";
                   case 0x49:
                       return "[Say2]";
                   case 0x39:
                       return "[RequestMagicSkillUse]";
                   case 0x38:
                       return "[RequestMagicSkillList]";
                   case 0xce:
                       return "[RequestDeleteMacro]";
                   case 0x6c:
                       return "[RequestOpenMinimap]";
                   case 0x48:
                       return "[RequestTargetCancel]";
                   case 0x3f:
                       return "[RequestShortCutDel]";
                   case 0x3d:
                       return "[RequestShortCutReg]";
                   case 0x81:
                       return "[RequestPartyMatchDetail]";
                   case 0x80:
                       return "[RequestPartyMatchList]";
                   case 0x7f:
                       return "[RequestPartyMatchConfig]";
                   case 0x46:
                       return "[RequestDismissParty]";
                   case 0x45:
                       return "[RequestOustPartyMember]";
                   case 0x44:
                       return "[RequestWithDrawalParty]";
                   case 0x43:
                       return "[RequestAnswerJoinParty]";
                   case 0x42:
                       return "[RequestJoinParty]";
                   case 0x6a:
                       return "[RequestFriendInfoList]";
                   case 0xa9:
                       return "[RequestBlock]";
                   case 0x78:
                       return "[RequestFriendAddReply]";
                   case 0x7a:
                       return "[RequestFriendDel]";
                   case 0x77:
                       return "[RequestFriendInvite]";
                   case 0x92:
                       return "[RequestAllyCrest]";
                   case 0x91:
                       return "[RequestSetAllyCrest]";
                   case 0x90:
                       return "[RequestDismissAlly]";
                   case 0x8e:
                       return "[RequestWithdrawAlly]";
                   case 0x2e:
                       return "[RequestAllyInfo]";
                   case 0x8f:
                       return "[RequestOustAlly]";
                   case 0x8d:
                       return "[RequestAnswerJoinAlly]";
                   case 0x8c:
                       return "[RequestJoinAlly]";
                   case 0x0b:
                       return "[RequestGiveNickName]";
                   case 0x05:
                       return "[RequestStopPledgeWar]";
                   case 0x03:
                       return "[RequestStartPledgeWar]";
                   case 0x28:
                       return "[RequestWithDrawalPledge]";
                   case 0x26:
                       return "[RequestJoinPledge]";
                   case 0x29:
                       return "[RequestOustPledgeMember]";
                   case 0x27:
                       return "[RequestAnswerJoinPledge]";
                   case 0x4d:
                       return "[RequestPledgeMemberList]";
                   case 0x66:
                       return "[RequestPledgeExtendedInfo]";
                   case 0x67:
                       return "[RequestPledgeCrest]";
                   case 0x09:
                       return "[RequestSetPledgeCrest]";
                   case 0x65:
                       return "[RequestPledgeInfo]";
                   case 0x86:
                       return "[RequestTutorialPassCmdToServer]";
                   case 0x23:
                       return "[RequestBypassToServer]";
                   case 0x24:
                       return "[RequestBBSWrite]";
                   case 0x88:
                       return "[RequestTutorialClientEvent]";
                   case 0x87:
                       return "[RequestTutorialQuestionMarkPressed]";
                   case 0x85:
                       return "[RequestTutorialLinkHtml]";
                   case 0x22:
                       return "[RequestLinkHtml]";
                   case 0x17:
                       return "[RequestDraopItemPacket]";
                   case 0x19:
                       return "[UseItemPacket]";
                   case 0x11:
                       return "[EnterWorldPacket]";
                   case 0x1c:
                       return "[TradeDonePacket]";
                   case 0x1b:
                       return "[AddTradeItemPacket]";
                   case 0x1a:
                       return "[TradeRequestPacket]";
                   case 0x0c:
                       return "[NewCharacter]";
                   case 0x13:
                       return "[NewCharacterPacket]";
                   case 0x7b:
                       return "[CharacterRestorePacket]";
                   case 0x0d:
                       return "[CharacterDeletePacket]";
                   case 0x12:
                       return "[CharacterSelect]";
                   case 0x5c:
                       return "[FinishRotating]";
                   case 0x5b:
                       return "[StartRotating]";
                   case 0x52:
                       return "[MoveWithDelta]";
                   case 0x3a:
                       return "[SendApperingPacket]";
                   case 0x36:
                       return "[ChangeWaitType]";
                   case 0x35:
                       return "[ChangeMoveType]";
                   case 0x34:
                       return "[SocialAction]";
                   case 0x01:
                       return "[Attack]";
                   case 0x1f:
                       return "[Action]";
                   case 0x0f:
                       return "[MoveBackwardToLocation]";
                   // case 0x05:
                   //return "[RequestServerListPacket]";
                   case 0x07:
                       return "[ResponseAuthGameGuard]";
                   case 0x57:
                       return "[RequestRestart]";
                   case 0xb0:
                       return "[RequestMultiSellChoose]";
                   case 0xa8:
                       return "[RequestPackageSend]";
                   case 0x9f:
                       return "[SendPrivateStoreBuyList]";
                   case 0x9a:
                       return "[SetPrivateStoreList]";
                   case 0x83:
                       return "[SendPrivateStoreBuyList]";
                   case 0x31:
                       return "[SetPrivateStoreList]";
                   case 0xbb:
                       return "[RequestRecipeShopListSet]";
                   case 0xcd:
                       return "[RequestMakeMacro ccdcS]";
                   case 0x3c:
                       return "[SendWareHouseWithDrawList]";
                   case 0x3b:
                       return "[SendWareHouseDepositList]";
                   //case 0x80:
                   //return "[RequestSCCheck]";
                   case 0xc5:
                       return "[RequestBuySeed]";
                   case 0xc7:
                       return "[RequestPreviewItem]";
                   case 0x40:
                       return "[RequestBuyItem]";
                   case 0x37:
                       return "[RequestSellItem]";
                   case 0xd0:
                       return get_cli_ex_pck_name(bbufer);
                   default:
                       return "[Unknow packet]";
               }
               #endregion

           }*/
          /* public string get_cli_ex_pck_name(byte[] bbufer)
           {
               int temp_id = System.BitConverter.ToUInt16(bbufer, 1);
#region client ex
               switch (temp_id)
               {
                   case 0x40:
                       return "[RequestGetBossRecord]";
                   case 0xac:
                       return "[Request24HzSessionID]";
                   case 0xab:
                       return "[RequestAcceptWaitingSubstitute]";
                   case 0xaa:
                       return "[RequestRegistWaitingSubstitute]";
                   case 0xa9:
                       return "[RequestDeletePartySubstitute]";
                   case 0xb0:
                       return "[RequestCharacterNameCreatable]";
                   case 0xa8:
                       return "[RequestRegistPartySubstitute]";
                   case 0xaf:
                       return "[RequestEx2ndPasswordReq]";
                   case 0xae:
                       return "[RequestEx2ndPasswordVerify]";
                   case 0xad:
                       return "[RequestEx2ndPasswordCheck]";
                   case 0xb2:
                       return "[RequestUseGoodsInventoryItem]";
                   case 0xb1:
                       return "[RequestGoodsInventoryInfo]";
                   case 0x90:
                       return "[RequstBR_LectureMark]";
                   case 0x8f:
                       return "[RequestBR_MinigameInsertScore]";
                   case 0x8e:
                       return "[RequestBR_MinigameLoadScores]";
                   case 0x8d:
                       return "[RequestBR_RecentProductList]";
                   case 0x8c:
                       return "[RequestBR_BuyProduct]";
                   case 0x8b:
                       return "[RequestBR_ProductInfo]";
                   case 0x8a:
                       return "[RequestBR_ProductList]";
                   case 0x89:
                       return "[BR_GamePoint]";
                   case 0xa7:
                       return "[RequestUserStatistics]";
                   case 0xa3:
                       return "[RequestCommissionRegisteredItem]";
                   case 0xa2:
                       return "[RequestCommissionBuyItem]";
                   case 0xa1:
                       return "[RequestCommissionBuyInfo]";
                   case 0xa0:
                       return "[RequestCommissionList]";
                   case 0x9f:
                       return "[RequestCommissionDelete]";
                   case 0x9e:
                       return "[RequestCommissionCancel]";
                   case 0x9d:
                       return "[RequestCommissionRegister]";
                   case 0x9b:
                       return "[RequestCommissionRegistrableItemList]";
                   case 0x9c:
                       return "[RequestCommissionInfo]";
                   case 0xa5:
                       return "[RequestChangeToAwakenedClass]";
                   case 0xa4:
                       return "[RequestCallToChangeClass]";
                   case 0xb4:
                       return "[RequestFlyMoveStart]";
                   case 0x94:
                       return "[RequestFlyMove]";
                   case 0x93:
                       return "[RequestExEscapeScene]";
                   case 0x91:
                       return "[RequestCrystallizeEstimate]";
                   case 0x86:
                       return "[RequestExShowPostFriendListForPostBox]";
                   case 0x85:
                       return "[RequestExDeletePostFriendForPostBox]";
                   case 0x84:
                       return "[RequestExAddPostFriendForPostBox]";
                   case 0x7e:
                       return "[NewVoteSystem]";
                   case 0x7d:
                       return "[RequestAddExpandQuestAlarm]";
                   case 0x7b:
                       return "[RequestBR_EventRankerList]";
                   case 0x77:
                       return "[RequestEventMatchObserverEnd]";
                   case 0x76:
                       return "[RequestBuySellUIClose]";
                   case 0x72:
                       return "[RequestShowStepThree]";
                   case 0x71:
                       return "[RequestShowStepTwo]";
                   case 0x70:
                       return "[RequestShowNewUserPetition]";
                   case 0x65:
                       return "[RequestPostItemList]";
                   case 0x6f:
                       return "[RequestCancelSentPost]";
                   case 0x6e:
                       return "[RequestRequestSentPost]";
                   case 0x6c:
                       return "[RequestRequestSentPostList]";
                   case 0x6b:
                       return "[RequestRejectPost]";
                   case 0x6a:
                       return "[RequestReceivePost]";
                   case 0x69:
                       return "[RequestRequestReceivedPost]";
                   case 0x67:
                       return "[RequestRequestReceivedPostList]";
                   case 0x5b:
                       return "[EndScenePlayer]";
                   case 0x5c:
                       return "[RequestExBlockGameVote]";
                   case 0x5a:
                       return "[RequestExBlockGameEnter]";
                   case 0x53:
                       return "[RequestJump]";
                   case 0x56:
                       return "[NotifyStartMiniGame]";
                   case 0x59:
                       return "[RequestExCleftEnter]";
                   case 0x58:
                       return "[RequestDominionInfo]";
                   case 0x57:
                       return "[RequestJoinDominionWar]";
                   case 0x55:
                       return "[RequestStopShowCrataeCubeRank]";
                   case 0x54:
                       return "[RequestStartShowCrataeCubeRank]";
                   case 0x52:
                       return "[RequestWithDrawPremiumItem]";
                   case 0x51:
                       return "[RequestChangeBookMarkSlot*]";
                   //case 0x51:
                      // return "[RequestTeleportBookMark]";
                  // case 0x51:
                      // return "[RequestDeleteBookMarkSlot]";
                  // case 0x51:
                       //return "[RequestModifyBookMarkSlot]";
                 //  case 0x51:
                       //return "[RequestSaveBookMarkSlot]";
                  // case 0x51:
                       //return "[RequestBookMarkSlotInfo]";
                   case 0x50:
                       return "[RequestResetNickname]";
                   case 0x4f:
                       return "[RequestChangeNicknameColor]";
                   case 0x4e:
                       return "[RequestExCancelEnchantItem]";
                   case 0x4d:
                       return "[RequestExTryToPutEnchantSupportItem]";
                   case 0x4c:
                       return "[RequestExTryToPutEnchantTargetItem]";
                   case 0x4b:
                       return "[RequestDispel]";
                   case 0x49:
                       return "[RequestPVPMatchRecord]";
                   case 0x22:
                       return "[RequestSaveKeyMapping]";
                   case 0x21:
                       return "[RequestKeyMapping]";
                   case 0x48:
                       return "[RequestFortressMapInfo]";
                   case 0x3f:
                       return "[RequestFortressSiegeInfo]";
                   case 0x3e:
                       return "[RequestAllAgitInfo]";
                   case 0x3d:
                       return "[RequestAllFortressInfo]";
                   case 0x3c:
                       return "[RequestAllCastleInfo]";
                   case 0x3a:
                       return "[RequestInfoItemAuction]";
                   case 0x39:
                       return "[RequestBidItemAuction]";
                   case 0x45:
                       return "[RequestDuelSurrender]";
                   case 0x43:
                       return "[RequestRefineCancel]";
                   case 0x42:
                       return "[RequestConfirmCancelItem]";
                   case 0x41:
                       return "[RequestRefine]";
                   case 0x28:
                       return "[RequestConfirmGemStone]";
                   case 0x27:
                       return "[RequestConfirmRefinerItem]";
                   case 0x26:
                       return "[RequestConfirmTargetItem]";
                   case 0x1c:
                       return "[RequestDuelAnswerStart]";
                   case 0x1b:
                       return "[RequestDuelStart]";
                   case 0x1e:
                       return "[RequestExRqItemLink]";
                   case 0x1d:
                       return "[RequestExSetTutorial]";
                   case 0x3b:
                       return "[RequestExChangeName]";
                   case 0x63:
                       return "[RequestSeedPhase]";
                   case 0x64:
                       return "[RequestMpccPartymasterList]";
                   case 0x62:
                       return "[RequestWithdrawMpccRoom]";
                   case 0x61:
                       return "[RequestDismissMpccRoom]";
                   case 0x60:
                       return "[RequestOustFromMpccRoom]";
                   case 0x5f:
                       return "[RequestJoinMpccRoom]";
                   case 0x5e:
                       return "[RequestManageMpccRoom]";
                   case 0x5d:
                       return "[RequestListMpccWaiting]";
                   case 0x25:
                       return "[RequestExitPartyMatchingWaitingRoom]";
                   //case 0x31:
                       //return "[RequestListPartyMatchingWaitingRoom]";
                   case 0x30:
                       return "[AnswerJoinPartyRoom]";
                   case 0x2f:
                       return "[RequestAskJoinPartyRoom]";
                   case 0x2b:
                       return "[RequestCursedWeaponLocation]";
                   case 0x2a:
                       return "[RequestCursedWeaponList]";
                   case 0x18:
                       return "[RequestExFishRanking]";
                   case 0x34:
                       return "[RequestExEnchantSkill]";
                   case 0x33:
                       return "[RequestExEnchantSkill]";
                   case 0x32:
                       return "[RequestExEnchantSkill]";
                   case 0x0f:
                       return "[RequestExEnchantSkill]";
                   case 0x46:
                       return "[RequestExEnchantSkillInfoDetail]";
                   case 0x0e:
                       return "[RequestExEnchantSkillInfo]";
                   case 0x95:
                       return "[RequestSurrenderPledgeWarEX]";
                   case 0x2d:
                       return "[RequestExMPCCShowPartyMembersInfo]";
                   case 0x08:
                       return "[RequestExOustFromMPCC]";
                   case 0x07:
                       return "[RequestExAcceptJoinMPCC]";
                   case 0x06:
                       return "[RequestExAskJoinMPCC]";
                   case 0x23:
                       return "[RequestExRemoveItemAttribute]";
                   case 0x35:
                       return "[RequestEnchantItemAttribute]";
                   case 0x92:
                       return "[RequestCrystallizeItemCancel]";
                   case 0x7a:
                       return "[AnswerCoupleAction]";
                   case 0x4a:
                       return "[SetPrivateStoreWholeMsg]";
                   case 0x44:
                       return "[RequestExMagicSkillUseGround]";
                   case 0x0d:
                       return "[RequestAutoSoulShot]";
                   case 0x0b:
                       return "[RequestWithdrawPartyRoom]";
                   case 0x0a:
                       return "[RequestDismissPartyRoom]";
                   case 0x09:
                       return "[RequestOustFromPartyRoom]";
                   case 0x88:
                       return "[RequestOlympiadMatchList]";
                   case 0x29:
                       return "[RequestOlympiadObserverEnd]";
                   case 0x05:
                       return "[RequestWriteHeroWords]";
                   case 0x01:
                       return "[RequestManorList]";
                   case 0x0c:
                       return "[RequestHandOverPartyMaster]";
                   case 0x7c:
                       return "[ask]";
                   case 0x79:
                       return "[answer]";
                   case 0x78:
                       return "[requestPartylooting]";
                   case 0x9a:
                       return "[RequestInzonePartyInfoHistory]";
                   case 0x99:
                       return "[RequestUpdateBlockMemo]";
                   case 0x98:
                       return "[RequestUpdateFriendMemo]";
                   case 0x97:
                       return "[RequestFriendDetailInfo]";
                   case 0x19:
                       return "[RequestPCCafeCouponUse]";
                   case 0x2c:
                       return "[RequestPledgeReorganizeMember]";
                   case 0x17:
                       return "[RequestPledgeWarList]";
                   case 0x16:
                       return "[RequestPledgeMemberInfo]";
                   case 0x15:
                       return "[RequestPledgeSetMemberPowerGrade]";
                   case 0x14:
                       return "[RequestPledgeMemberPowerInfo]";
                   case 0x13:
                       return "[RequestPledgePowerGradeList]";
                   case 0x12:
                       return "[RequestPledgeSetAcademyMaster]";
                   case 0x10:
                       return "[RequestExPledgeCrestLarge]";
                   case 0x11:
                       return "[RequestExSetPledgeCrestLarge]";
                   case 0xb9:
                       return "[RequestChangeAttributeCancel]";
                   case 0xb8:
                       return "[RequestChangeAttributeItem]";
                   case 0xb7:
                       return "[SendChangeAttributeTargetItem]";
                   case 0xb3:
                       return "[RequestFirstPlayStart]";
                   case 0xa6:
                       return "[RequestWorldStatistics]";
                   case 0x31:
                       return "[RequestPartyMatchWaitList*]";
                   case 0x6d:
                       return "[RequestDeleteSentPost]";
                   case 0x68:
                       return "[RequestDeleteReceivedPost]";
                   case 0x66:
                       return "[RequestSendPost]";
                   case 0x24:
                       return "[RequestSaveInventoryOrder]";
                   case 0x04:
                       return "[RequestSetCrop]";
                   case 0x02:
                       return "[RequestProcureCropList]";
                   case 0x75:
                       return "[RequestRefundItem]";
                   case 0x80:
                       return "[RequestLogin]";
                   case 0xc2:
                       return "[RequestExCancelShape_Shifting_Item]";
                   case 0xc1:
                       return "[RequestExTryToPut_Shape_Shifting_EnchantSupportItem]";
                   case 0xc0:
                       return "[RequestExTryToPut_Shape_Shifting_TargetItem]";
                   case 0xc3:
                       return "[RequestShape_Shifting_Item]";
                   case 0xb5:
                       return "[RequestHardWareInfo]";
                   case 1174:
                       return "RequestDynamicQuestHTML";
                   case 662:
                       return "RequestDynamicQuestProgressInfo";
                   case 918:
                       return "RequestDynamicQuestScoreBoard";
                   case 4227:
                       return "RequestExReBid";
                   case 2691:
                       return "RequestExProceedCancelAgitBid*";
                   case 2435:
                       return "RequestExConfirmCancelAgitBid*";
                   case 2179:
                       return "RequestExProceedCancelRegisteringAgit";
                   case 1923:
                       return "RequestExConfirmCancelRegisteringAgit";
                   case 1411:
                       return "RequestExRegisterAgitForBidStep3*";
                   case 1155:
                       return "RequestExRegisterAgitForBidStep1";
                   case 643:
                       return "RequestExAgitDetailInfo";
                   case 899:
                       return "RequestExMyAgitState";
                   case 3971:
                       return "RequestExApplyForBidStep3";
                   case 3715:
                       return "RequestExApplyForBidStep2";
                   case 3459:
                       return "RequestExApplyForBidStep1";
                   case 5251:
                       return "RequestExAgitListForBid";
                   case 4995:
                       return "RequestExApplyForAgitLotStep2";
                   case 4739:
                       return "RequestExApplyForAgitLotStep1";
                   case 4483:
                       return "RequestExAgitListForLot";
                   case 387:
                       return "RequestExAgitInitialize";
                   case 18207:
                       return "CanNotMoveAnymore";
                   case 457:
                       return "PetitionVote";
                   case 3969:
                       return "MoveToLocationInVehicle";
                   default:
                       return "[Unknow D0 Packet]";
               }
#endregion
               //return "ex temp solution"; // too lazy , will be done in future ....
               
           }*/
          /* public string get_srv_pck_name(byte[] bbufer)
           {
               #region srv pck name

               switch (bbufer[0])
               {
                   case 0x00:
                       return "[DiePacket]";
                   case 0x01:
                       return "[RevivePacket]";
                   case 0x02:
                       return "[AttackOutofRangePacket]";
                   case 0x03:
                       return "[AttackinCoolTimePacket]";
                   case 0x04:
                       return "[AttackDeadTargetPacket]";
                   case 0x05:
                       return "[SpawnItemPacket]";
                   case 0x06:
                       return "[SellListPacket]";
                   case 0x07:
                       return "[BuyListPacket]";
                   case 0x08:
                       return "[DeleteObjectPacket]";
                   case 0x09:
                       return "[CharacterSelectionInfoPacket]";
                   case 0x0a:
                       return "[LoginFailPacket]";
                   case 0x0b:
                       return "[CharacterSelectedPacket]";
                   case 0x0c:
                       return "[NpcInfoPacket]";
                   case 0x0d:
                       return "[NewCharacterSuccessPacket]";
                   case 0x0e:
                       return "[NewCharacterFailPacket]";
                   case 0x0f:
                       return "[CharacterCreateSuccessPacket]";
                   case 0x10:
                       return "[CharacterCreateFailPacket]";
                   case 0x11:
                       return "[ItemListPacket]";
                   case 0x12:
                       return "[SunRisePacket]";
                   case 0x13:
                       return "[SunSetPacket]";
                   case 0x14:
                       return "[TradeStartPacket]";
                   case 0x15:
                       return "[TradeStartOkPacket]";
                   case 0x16:
                       return "[DropItemPacket]";
                   case 0x17:
                       return "[GetItemPacket]";
                   case 0x18:
                       return "[StatusUpdatePacket]";
                   case 0x19:
                       return "[NpcHtmlMessagePacket]";
                   case 0x1a:
                       return "[TradeOwnAddPacket]";
                   case 0x1b:
                       return "[TradeOtherAddPacket]";
                   case 0x1c:
                       return "[TradeDonePacket]";
                   case 0x1d:
                       return "[CharacterDeleteSuccessPacket]";
                   case 0x1e:
                       return "[CharacterDeleteFailPacket]";
                   case 0x1f:
                       return "[ActionFailPacket]";
                   case 0x20:
                       return "[SeverClosePacket]";
                   case 0x21:
                       return "[InventoryUpdatePacket]";
                   case 0x22:
                       return "[TeleportToLocationPacket]";
                   case 0x23:
                       return "[TargetSelectedPacket]";
                   case 0x24:
                       return "[TargetUnselectedPacket]";
                   case 0x25:
                       return "[AutoAttackStartPacket]";
                   case 0x26:
                       return "[AutoAttackStopPacket]";
                   case 0x27:
                       return "[SocialActionPacket]";
                   case 0x28:
                       return "[ChangeMoveTypePacket]";
                   case 0x29:
                       return "[ChangeWaitTypePacket]";
                   case 0x2a:
                       return "[ManagePledgePowerPacket]";
                   case 0x2b:
                       return "[CreatePledgePacket]";
                   case 0x2c:
                       return "[AskJoinPledgePacket]";
                   case 0x2d:
                       return "[JoinPledgePacket]";
                   case 0x2e:
                       return "[VersionCheckPacket]";
                   case 0x2f:
                       return "[MTLPacket]";
                   case 0x30:
                       return "[NSPacket]";
                   case 0x31:
                       return "[CIPacket]";
                   case 0x32:
                       return "[UIPacket]";
                   case 0x33:
                       return "[AttackPacket]";
                   case 0x34:
                       return "[WithdrawalPledgePacket]";
                   case 0x35:
                       return "[OustPledgeMemberPacket]";
                   case 0x36:
                       return "[SetOustPledgeMemberPacket]";
                   case 0x37:
                       return "[DismissPledgePacket]";
                   case 0x38:
                       return "[SetDismissPledgePacket]";
                   case 0x39:
                       return "[AskJoinPartyPacket]";
                   case 0x3a:
                       return "[JoinPartyPacket]";
                   case 0x3b:
                       return "[WithdrawalPartyPacket]";
                   case 0x3c:
                       return "[OustPartyMemberPacket]";
                   case 0x3d:
                       return "[SetOustPartyMemberPacket]";
                   case 0x3e:
                       return "[DismissPartyPacket]";
                   case 0x3f:
                       return "[SetDismissPartyPacket]";
                   case 0x40:
                       return "[MagicAndSkillList]";
                   case 0x41:
                       return "[WareHouseDepositListPacket]";
                   case 0x42:
                       return "[WareHouseWithdrawListPacket]";
                   case 0x43:
                       return "[WareHouseDonePacket]";
                   case 0x44:
                       return "[ShortCutRegisterPacket]";
                   case 0x45:
                       return "[ShortCutInitPacket]";
                   case 0x46:
                       return "[ShortCutDeletePacket]";
                   case 0x47:
                       return "[StopMovePacket]";
                   case 0x48:
                       return "[MagicSkillUse]";
                   case 0x49:
                       return "[MagicSkillCanceled]";
                   case 0x4a:
                       return "[SayPacket2]";
                   case 0x4b:
                       return "[EquipUpdatePacket]";
                   case 0x4c:
                       return "[DoorInfoPacket]";
                   case 0x4d:
                       return "[DoorStatusUpdatePacket]";
                   case 0x4e:
                       return "[PartySmallWindowAllPacket]";
                   case 0x4f:
                       return "[PartySmallWindowAddPacket]";
                   case 0x50:
                       return "[PartySmallWindowDeleteAllPacket]";
                   case 0x51:
                       return "[PartySmallWindowDeletePacket]";
                   case 0x52:
                       return "[PartySmallWindowUpdatePacket]";
                   case 0x53:
                       return "[TradePressOwnOkPacket]";
                   case 0x54:
                       return "[MagicSkillLaunchedPacket]";
                   case 0x55:
                       return "[FriendAddRequestResult]";
                   case 0x56:
                       return "[FriendAdd]";
                   case 0x57:
                       return "[FriendRemove]";
                   case 0x58:
                       return "[FriendList]";
                   case 0x59:
                       return "[FriendStatus]";
                   case 0x5a:
                       return "[PledgeShowMemberListAllPacket]";
                   case 0x5b:
                       return "[PledgeShowMemberListUpdatePacket]";
                   case 0x5c:
                       return "[PledgeShowMemberListAddPacket]";
                   case 0x5d:
                       return "[PledgeShowMemberListDeletePacket]";
                   case 0x5e:
                       return "[MagicListPacket]";
                   case 0x5f:
                       return "[SkillListPacket]";
                   case 0x60:
                       return "[VehicleInfoPacket]";
                   case 0x61:
                       return "[FinishRotatingPacket]";
                   case 0x62:
                       return "[SystemMessagePacket]";
                   case 0x63:
                       return "[StartPledgeWarPacket]";
                   case 0x64:
                       return "[ReplyStartPledgeWarPacket]";
                   case 0x65:
                       return "[StopPledgeWarPacket]";
                   case 0x66:
                       return "[ReplyStopPledgeWarPacket]";
                   case 0x67:
                       return "[SurrenderPledgeWarPacket]";
                   case 0x68:
                       return "[ReplySurrenderPledgeWarPacket]";
                   case 0x69:
                       return "[SetPledgeCrestPacket]";
                   case 0x6a:
                       return "[PledgeCrestPacket]";
                   case 0x6b:
                       return "[SetupGaugePacket]";
                   case 0x6c:
                       return "[VehicleDeparturePacket]";
                   case 0x6d:
                       return "[VehicleCheckLocationPacket]";
                   case 0x6e:
                       return "[GetOnVehiclePacket]";
                   case 0x6f:
                       return "[GetOffVehiclePacket]";
                   case 0x70:
                       return "[TradeRequestPacket]";
                   case 0x71:
                       return "[RestartResponsePacket]";
                   case 0x72:
                       return "[MoveToPawnPacket]";
                   case 0x73:
                       return "[SSQInfoPacket]";
                   case 0x74:
                       return "[GameGuardQueryPacket]";
                   case 0x75:
                       return "[L2FriendListPacket]";
                   case 0x76:
                       return "[L2FriendPacket]";
                   case 0x77:
                       return "[L2FriendStatusPacket]";
                   case 0x78:
                       return "[L2FriendSayPacket]";
                   case 0x79:
                       return "[ValidateLocationPacket]";
                   case 0x7a:
                       return "[StartRotatingPacket]";
                   case 0x7b:
                       return "[ShowBoardPacket]";
                   case 0x7c:
                       return "[ChooseInventoryItemPacket]";
                   case 0x7d:
                       return "[DummyPacket]";
                   case 0x7e:
                       return "[MoveToLocationInVehiclePacket]";
                   case 0x7f:
                       return "[StopMoveInVehiclePacket]";
                   case 0x80:
                       return "[ValidateLocationInVehiclePacket]";
                   case 0x81:
                       return "[TradeUpdatePacket]";
                   case 0x82:
                       return "[TradePressOtherOkPacket]";
                   case 0x83:
                       return "[FriendAddRequest]";
                   case 0x84:
                       return "[LogOutOkPacket]";
                   case 0x85:
                       return "[AbnormalStatusUpdatePacket]";
                   case 0x86:
                       return "[QuestListPacket]";
                   case 0x87:
                       return "[EnchantResultPacket]";
                   case 0x88:
                       return "[PledgeShowMemberListDeleteAllPacket]";
                   case 0x89:
                       return "[PledgeInfoPacket]";
                   case 0x8a:
                       return "[PledgeExtendedInfoPacket]";
                   case 0x8b:
                       return "[SurrenderPersonallyPacket]";
                   case 0x8c:
                       return "[RidePacket]";
                   case 0x8d:
                       return "[DummyPacket]";
                   case 0x8e:
                       return "[PledgeShowInfoUpdatePacket]";
                   case 0x8f:
                       return "[ClientActionPacket]";
                   case 0x90:
                       return "[AcquireSkillListPacket]";
                   case 0x91:
                       return "[AcquireSkillInfoPacket]";
                   case 0x92:
                       return "[ServerObjectInfoPacket]";
                   case 0x93:
                       return "[GMHidePacket]";
                   case 0x94:
                       return "[AcquireSkillDonePacket]";
                   case 0x95:
                       return "[GMViewCharacterInfoPacket]";
                   case 0x96:
                       return "[GMViewPledgeInfoPacket]";
                   case 0x97:
                       return "[GMViewSkillInfoPacket]";
                   case 0x98:
                       return "[GMViewMagicInfoPacket]";
                   case 0x99:
                       return "[GMViewQuestInfoPacket]";
                   case 0x9a:
                       return "[GMViewItemListPacket]";
                   case 0x9b:
                       return "[GMViewWarehouseWithdrawListPacket]";
                   case 0x9c:
                       return "[ListPartyWatingPacket]";
                   case 0x9d:
                       return "[PartyRoomInfoPacket]";
                   case 0x9e:
                       return "[PlaySoundPacket]";
                   case 0x9f:
                       return "[StaticObjectPacket]";
                   case 0xa0:
                       return "[PrivateStoreManageList]";
                   case 0xa1:
                       return "[PrivateStoreList]";
                   case 0xa2:
                       return "[PrivateStoreMsg]";
                   case 0xa3:
                       return "[ShowMinimapPacket]";
                   case 0xa4:
                       return "[ReviveRequestPacket]";
                   case 0xa5:
                       return "[AbnormalVisualEffectPacket]";
                   case 0xa6:
                       return "[TutorialShowHtmlPacket]";
                   case 0xa7:
                       return "[TutorialShowQuestionMarkPacket]";
                   case 0xa8:
                       return "[TutorialEnableClientEventPacket]";
                   case 0xa9:
                       return "[TutorialCloseHtmlPacket]";
                   case 0xaa:
                       return "[ShowRadarPacket]";
                   case 0xab:
                       return "[WithdrawAlliancePacket]";
                   case 0xac:
                       return "[OustAllianceMemberPledgePacket]";
                   case 0xad:
                       return "[DismissAlliancePacket]";
                   case 0xae:
                       return "[SetAllianceCrestPacket]";
                   case 0xaf:
                       return "[AllianceCrestPacket]";
                   case 0xb0:
                       return "[ServerCloseSocketPacket]";
                   case 0xb1:
                       return "[PetStatusShowPacket]";
                   case 0xb2:
                       return "[PetInfoPacket]";
                   case 0xb3:
                       return "[PetItemListPacket]";
                   case 0xb4:
                       return "[PetInventoryUpdatePacket]";
                   case 0xb5:
                       return "[AllianceInfoPacket]";
                   case 0xb6:
                       return "[PetStatusUpdatePacket]";
                   case 0xb7:
                       return "[PetDeletePacket]";
                   case 0xb8:
                       return "[DeleteRadarPacket]";
                   case 0xb9:
                       return "[MyTargetSelectedPacket]";
                   case 0xba:
                       return "[PartyMemberPositionPacket]";
                   case 0xbb:
                       return "[AskJoinAlliancePacket]";
                   case 0xbc:
                       return "[JoinAlliancePacket]";
                   case 0xbd:
                       return "[PrivateStoreBuyManageList]";
                   case 0xbe:
                       return "[PrivateStoreBuyList]";
                   case 0xbf:
                       return "[PrivateStoreBuyMsg]";
                   case 0xc0:
                       return "[VehicleStartPacket]";
                   case 0xc1:
                       return "[RequestTimeCheckPacket]";
                   case 0xc2:
                       return "[StartAllianceWarPacket]";
                   case 0xc3:
                       return "[ReplyStartAllianceWarPacket]";
                   case 0xc4:
                       return "[StopAllianceWarPacket]";
                   case 0xc5:
                       return "[ReplyStopAllianceWarPacket]";
                   case 0xc6:
                       return "[SurrenderAllianceWarPacket]";
                   case 0xc7:
                       return "[SkillCoolTimePacket]";
                   case 0xc8:
                       return "[PackageToListPacket]";
                   case 0xc9:
                       return "[CastleSiegeInfoPacket]";
                   case 0xca:
                       return "[CastleSiegeAttackerListPacket]";
                   case 0xcb:
                       return "[CastleSiegeDefenderListPacket]";
                   case 0xcc:
                       return "[NickNameChangedPacket]";
                   case 0xcd:
                       return "[PledgeStatusChangedPacket]";
                   case 0xce:
                       return "[RelationChangedPacket]";
                   case 0xcf:
                       return "[EventTriggerPacket]";
                   case 0xd0:
                       return "[MultiSellListPacket]";
                   case 0xd1:
                       return "[SetSummonRemainTimePacket]";
                   case 0xd2:
                       return "[PackageSendableListPacket]";
                   case 0xd3:
                       return "[EarthQuakePacket]";
                   case 0xd4:
                       return "[FlyToLocationPacket]";
                   case 0xd5:
                       return "[BlockListPacket]";
                   case 0xd6:
                       return "[SpecialCameraPacket]";
                   case 0xd7:
                       return "[NormalCameraPacket]";
                   case 0xd8:
                       return "[SkillRemainSecPacket]";
                   case 0xd9:
                       return "[NetPingPacket]";
                   case 0xda:
                       return "[DicePacket]";
                   case 0xdb:
                       return "[SnoopPacket]";
                   case 0xdc:
                       return "[RecipeBookItemListPacket]";
                   case 0xdd:
                       return "[RecipeItemMakeInfoPacket]";
                   case 0xde:
                       return "[RecipeShopManageListPacket]";
                   case 0xdf:
                       return "[RecipeShopSellListPacket]";
                   case 0xe0:
                       return "[RecipeShopItemInfoPacket]";
                   case 0xe1:
                       return "[RecipeShopMsgPacket]";
                   case 0xe2:
                       return "[ShowCalcPacket]";
                   case 0xe3:
                       return "[MonRaceInfoPacket]";
                   case 0xe4:
                       return "[HennaItemInfoPacket]";
                   case 0xe5:
                       return "[HennaInfoPacket]";
                   case 0xe6:
                       return "[HennaUnequipListPacket]";
                   case 0xe7:
                       return "[HennaUnequipInfoPacket]";
                   case 0xe8:
                       return "[MacroListPacket]";
                   case 0xe9:
                       return "[BuyListSeedPacket]";
                   case 0xea:
                       return "[ShowTownMapPacket]";
                   case 0xeb:
                       return "[ObserverStartPacket]";
                   case 0xec:
                       return "[ObserverEndPacket]";
                   case 0xed:
                       return "[ChairSitPacket]";
                   case 0xee:
                       return "[HennaEquipListPacket]";
                   case 0xef:
                       return "[SellListProcurePacket]";
                   case 0xf0:
                       return "[GMHennaInfoPacket]";
                   case 0xf1:
                       return "[RadarControlPacket]";
                   case 0xf2:
                       return "[ClientSetTimePacket]";
                   case 0xf3:
                       return "[ConfirmDlgPacket]";
                   case 0xf4:
                       return "[PartySpelledPacket]";
                   case 0xf5:
                       return "[ShopPreviewListPacket]";
                   case 0xf6:
                       return "[ShopPreviewInfoPacket]";
                   case 0xf7:
                       return "[CameraModePacket]";
                   case 0xf8:
                       return "[ShowXMasSealPacket]";
                   case 0xf9:
                       return "[EtcStatusUpdatePacket]";
                   case 0xfa:
                       return "[ShortBuffStatusUpdatePacket]";
                   case 0xfb:
                       return "[SSQStatusPacket]";
                   case 0xfc:
                       return "[PetitionVotePacket]";
                   case 0xfd:
                       return "[AgitDecoInfoPacket]";
                   case 0xfe:
                       return get_ex_srv_name(bbufer);
                   default:
                       return "[Unknow packet]";
               }
               #endregion
           }
           public string get_ex_srv_name(byte[] bbufer)
           {
               int temp_id = System.BitConverter.ToUInt16(bbufer, 1);
               switch (temp_id)
               {
                   #region srv ex pck
                   case 0x00:
                       return "[DummyPacket]";
                   case 0x01:
                       return "[ExRegenMaxPacket]";
                   case 0x02:
                       return "[ExEventMatchUserInfoPacket]";
                   case 0x03:
                       return "[ExColosseumFenceInfoPacket]";
                   case 0x04:
                       return "[ExEventMatchSpelledInfoPacket]";
                   case 0x05:
                       return "[ExEventMatchFirecrackerPacket]";
                   case 0x06:
                       return "[ExEventMatchTeamUnlockedPacket]";
                   case 0x07:
                       return "[ExEventMatchGMTestPacket]";
                   case 0x08:
                       return "[ExPartyRoomMemberPacket]";
                   case 0x09:
                       return "[ExClosePartyRoomPacket]";
                   case 0x0a:
                       return "[ExManagePartyRoomMemberPacket]";
                   case 0x0b:
                       return "[ExEventMatchLockResult]";
                   case 0x0c:
                       return "[ExAutoSoulShot]";
                   case 0x0d:
                       return "[ExEventMatchListPacket]";
                   case 0x0e:
                       return "[ExEventMatchObserverPacket]";
                   case 0x0f:
                       return "[ExEventMatchMessagePacket]";
                   case 0x10:
                       return "[ExEventMatchScorePacket]";
                   case 0x11:
                       return "[ExServerPrimitivePacket]";
                   case 0x12:
                       return "[ExOpenMPCCPacket]";
                   case 0x13:
                       return "[ExCloseMPCCPacket]";
                   case 0x14:
                       return "[ExShowCastleInfo]";
                   case 0x15:
                       return "[ExShowFortressInfo]";
                   case 0x16:
                       return "[ExShowAgitInfo]";
                   case 0x17:
                       return "[ExShowFortressSiegeInfo]";
                   case 0x18:
                       return "[ExPartyPetWindowAdd]";
                   case 0x19:
                       return "[ExPartyPetWindowUpdate]";
                   case 0x1a:
                       return "[ExAskJoinMPCCPacket]";
                   case 0x1b:
                       return "[ExPledgeEmblem]";
                   case 0x1c:
                       return "[ExEventMatchTeamInfoPacket]";
                   case 0x1d:
                       return "[ExEventMatchCreatePacket]";
                   case 0x1e:
                       return "[ExFishingStartPacket]";
                   case 0x1f:
                       return "[ExFishingEndPacket]";
                   case 0x20:
                       return "[ExShowQuestInfoPacket]";
                   case 0x21:
                       return "[ExShowQuestMarkPacket]";
                   case 0x22:
                       return "[ExSendManorListPacket]";
                   case 0x23:
                       return "[ExShowSeedInfoPacket]";
                   case 0x24:
                       return "[ExShowCropInfoPacket]";
                   case 0x25:
                       return "[ExShowManorDefaultInfoPacket]";
                   case 0x26:
                       return "[ExShowSeedSettingPacket]";
                   case 0x27:
                       return "[ExFishingStartCombatPacket]";
                   case 0x28:
                       return "[ExFishingHpRegenPacket]";
                   case 0x29:
                       return "[ExEnchantSkillListPacket]";
                   case 0x2a:
                       return "[ExEnchantSkillInfoPacket]";
                   case 0x2b:
                       return "[ExShowCropSettingPacket]";
                   case 0x2c:
                       return "[ExShowSellCropListPacket]";
                   case 0x2d:
                       return "[ExOlympiadMatchEndPacket]";
                   case 0x2e:
                       return "[ExMailArrivedPacket]";
                   case 0x2f:
                       return "[ExStorageMaxCountPacket]";
                   case 0x30:
                       return "[ExEventMatchManagePacket]";
                   case 0x31:
                       return "[ExMultiPartyCommandChannelInfoPacket]";
                   case 0x32:
                       return "[ExPCCafePointInfoPacket]";
                   case 0x33:
                       return "[ExSetCompassZoneCode]";
                   case 0x34:
                       return "[ExGetBossRecord]";
                   case 0x35:
                       return "[ExAskJoinPartyRoom]";
                   case 0x36:
                       return "[ExListPartyMatchingWaitingRoom]";
                   case 0x37:
                       return "[ExSetMpccRouting]";
                   case 0x38:
                       return "[ExShowAdventurerGuideBook]";
                   case 0x39:
                       return "[ExShowScreenMessage]";
                   case 0x3a:
                       return "[PledgeSkillListPacket]";
                   case 0x3b:
                       return "[PledgeSkillListAddPacket]";
                   case 0x3c:
                       return "[PledgePowerGradeList]";
                   case 0x3d:
                       return "[PledgeReceivePowerInfo]";
                   case 0x3e:
                       return "[PledgeReceiveMemberInfo]";
                   case 0x3f:
                       return "[PledgeReceiveWarList]";
                   case 0x40:
                       return "[PledgeReceiveSubPledgeCreated]";
                   case 0x41:
                       return "[ExRedSkyPacket]";
                   case 0x42:
                       return "[PledgeReceiveUpdatePower]";
                   case 0x43:
                       return "[FlySelfDestinationPacket]";
                   case 0x44:
                       return "[ShowPCCafeCouponShowUI]";
                   case 0x45:
                       return "[ExSearchOrc]";
                   case 0x46:
                       return "[ExCursedWeaponList]";
                   case 0x47:
                       return "[ExCursedWeaponLocation]";
                   case 0x48:
                       return "[ExRestartClient]";
                   case 0x49:
                       return "[ExRequestHackShield]";
                   case 0x4a:
                       return "[ExUseSharedGroupItem]";
                   case 0x4b:
                       return "[ExMPCCShowPartyMemberInfo]";
                   case 0x4c:
                       return "[ExDuelAskStart]";
                   case 0x4d:
                       return "[ExDuelReady]";
                   case 0x4e:
                       return "[ExDuelStart]";
                   case 0x4f:
                       return "[ExDuelEnd]";
                   case 0x50:
                       return "[ExDuelUpdateUserInfo]";
                   case 0x51:
                       return "[ExShowVariationMakeWindow]";
                   case 0x52:
                       return "[ExShowVariationCancelWindow]";
                   case 0x53:
                       return "[ExPutItemResultForVariationMake]";
                   case 0x54:
                       return "[ExPutIntensiveResultForVariationMake]";
                   case 0x55:
                       return "[ExPutCommissionResultForVariationMake]";
                   case 0x56:
                       return "[ExVariationResult]";
                   case 0x57:
                       return "[ExPutItemResultForVariationCancel]";
                   case 0x58:
                       return "[ExVariationCancelResult]";
                   case 0x59:
                       return "[ExDuelEnemyRelation]";
                   case 0x5a:
                       return "[ExPlayAnimation]";
                   case 0x5b:
                       return "[ExMPCCPartyInfoUpdate]";
                   case 0x5c:
                       return "[ExPlayScene]";
                   case 0x5d:
                       return "[ExSpawnEmitterPacket]";
                   case 0x5e:
                       return "[ExEnchantSkillInfoDetailPacket]";
                   case 0x5f:
                       return "[ExBasicActionList]";
                   case 0x60:
                       return "[ExAirShipInfo]";
                   case 0x61:
                       return "[ExAttributeEnchantResultPacket]";
                   case 0x62:
                       return "[ExChooseInventoryAttributeItemPacket]";
                   case 0x63:
                       return "[ExGetOnAirShipPacket]";
                   case 0x64:
                       return "[ExGetOffAirShipPacket]";
                   case 0x65:
                       return "[ExMoveToLocationAirShipPacket]";
                   case 0x66:
                       return "[ExStopMoveAirShipPacket]";
                   case 0x67:
                       return "[ExShowTracePacket]";
                   case 0x68:
                       return "[ExItemAuctionInfoPacket]";
                   case 0x69:
                       return "[ExNeedToChangeName]";
                   case 0x6a:
                       return "[ExPartyPetWindowDelete]";
                   case 0x6b:
                       return "[ExTutorialList]";
                   case 0x6c:
                       return "[ExRpItemLink]";
                   case 0x6d:
                       return "[ExMoveToLocationInAirShipPacket]";
                   case 0x6e:
                       return "[ExStopMoveInAirShipPacket]";
                   case 0x6f:
                       return "[ExValidateLocationInAirShipPacket]";
                   case 0x70:
                       return "[ExUISettingPacket]";
                   case 0x71:
                       return "[ExMoveToTargetInAirShipPacket]";
                   case 0x72:
                       return "[ExAttackInAirShipPacket]";
                   case 0x73:
                       return "[ExMagicSkillUseInAirShipPacket]";
                   case 0x74:
                       return "[ExShowBaseAttributeCancelWindow]";
                   case 0x75:
                       return "[ExBaseAttributeCancelResult]";
                   case 0x76:
                       return "[ExSubPledgetSkillAdd]";
                   case 0x77:
                       return "[ExResponseFreeServer]";
                   case 0x78:
                       return "[ExShowProcureCropDetailPacket]";
                   case 0x79:
                       return "[ExHeroListPacket]";
                   case 0x7a:
                       return "[ExOlympiadUserInfoPacket]";
                   case 0x7b:
                       return "[ExOlympiadSpelledInfoPacket]";
                   case 0x7c:
                       return "[ExOlympiadModePacket]";
                   case 0x7d:
                       return "[ExShowFortressMapInfo]";
                   case 0x7e:
                       return "[ExPVPMatchRecord]";
                   case 0x7f:
                       return "[ExPVPMatchUserDie]";
                   case 0x80:
                       return "[ExPrivateStoreWholeMsg]";
                   case 0x81:
                       return "[ExPutEnchantTargetItemResult]";
                   case 0x82:
                       return "[ExPutEnchantSupportItemResult]";
                   case 0x83:
                       return "[ExChangeNicknameNColor]";
                   case 0x84:
                       return "[ExGetBookMarkInfoPacket]";
                   case 0x85:
                       return "[ExNotifyPremiumItem]";
                   case 0x86:
                       return "[ExGetPremiumItemListPacket]";
                   case 0x87:
                       return "[ExPeriodicItemList]";
                   case 0x88:
                       return "[ExJumpToLocation]";
                   case 0x89:
                       return "[ExPVPMatchCCRecord]";
                   case 0x8a:
                       return "[ExPVPMatchCCMyRecord]";
                   case 0x8b:
                       return "[ExPVPMatchCCRetire]";
                   case 0x8c:
                       return "[ExShowTerritory]";
                   case 0x8d:
                       return "[ExNpcQuestHtmlMessage]";
                   case 0x8e:
                       return "[ExSendUIEventPacket]";
                   case 0x8f:
                       return "[ExNotifyBirthDay]";
                   case 0x90:
                       return "[ExShowDominionRegistry]";
                   case 0x91:
                       return "[ExReplyRegisterDominion]";
                   case 0x92:
                       return "[ExReplyDominionInfo]";
                   case 0x93:
                       return "[ExShowOwnthingPos]";
                   case 0x94:
                       return "[ExCleftList]";
                   case 0x95:
                       return "[ExCleftState]";
                   case 0x96:
                       return "[ExDominionChannelSet]";
                   case 0x97:
                       return "[ExBlockUpSetList]";
                   case 0x98:
                       return "[ExBlockUpSetState]";
                   case 0x99:
                       return "[ExStartScenePlayer]";
                   case 0x9a:
                       return "[ExAirShipTeleportList]";
                   case 0x9b:
                       return "[ExMpccRoomInfo]";
                   case 0x9c:
                       return "[ExListMpccWaiting]";
                   case 0x9d:
                       return "[ExDissmissMpccRoom]";
                   case 0x9e:
                       return "[ExManageMpccRoomMember]";
                   case 0x9f:
                       return "[ExMpccRoomMember]";
                   case 0xa0:
                       return "[ExVitalityPointInfo]";
                   case 0xa1:
                       return "[ExShowSeedMapInfo]";
                   case 0xa2:
                       return "[ExMpccPartymasterList]";
                   case 0xa3:
                       return "[ExDominionWarStart]";
                   case 0xa4:
                       return "[ExDominionWarEnd]";
                   case 0xa5:
                       return "[ExShowLines]";
                   case 0xa6:
                       return "[ExPartyMemberRenamed]";
                   case 0xa7:
                       return "[ExEnchantSkillResult]";
                   case 0xa8:
                       return "[ExRefundList]";
                   case 0xa9:
                       return "[ExNoticePostArrived]";
                   case 0xaa:
                       return "[ExShowReceivedPostList]";
                   case 0xab:
                       return "[ExReplyReceivedPost]";
                   case 0xac:
                       return "[ExShowSentPostList]";
                   case 0xad:
                       return "[ExReplySentPost]";
                   case 0xae:
                       return "[ExResponseShowStepOne]";
                   case 0xaf:
                       return "[ExResponseShowStepTwo]";
                   case 0xb0:
                       return "[ExResponseShowContents]";
                   case 0xb1:
                       return "[ExShowPetitionHtml]";
                   case 0xb2:
                       return "[ExReplyPostItemList]";
                   case 0xb3:
                       return "[ExChangePostState]";
                   case 0xb4:
                       return "[ExReplyWritePost]";
                   case 0xb5:
                       return "[ExInitializeSeed]";
                   case 0xb6:
                       return "[ExRaidReserveResult]";
                   case 0xb7:
                       return "[ExBuySellListPacket]";
                   case 0xb8:
                       return "[ExCloseRaidSocket]";
                   case 0xb9:
                       return "[ExPrivateMarketListPacket]";
                   case 0xba:
                       return "[ExRaidCharacterSelected]";
                   case 0xbb:
                       return "[ExAskCoupleAction]";
                   case 0xbc:
                       return "[ExBrBroadcastEventState]";
                   case 0xbd:
                       return "[ExBR_LoadEventTopRankersPacket]";
                   case 0xbe:
                       return "[ExChangeNPCState]";
                   case 0xbf:
                       return "[ExAskModifyPartyLooting]";
                   case 0xc0:
                       return "[ExSetPartyLooting]";
                   case 0xc1:
                       return "[ExRotation]";
                   case 0xc2:
                       return "[ExChangeClientEffectInfo]";
                   case 0xc3:
                       return "[ExMembershipInfo]";
                   case 0xc4:
                       return "[ExReplyHandOverPartyMaster]";
                   case 0xc5:
                       return "[ExQuestNpcLogList]";
                   case 0xc6:
                       return "[ExQuestItemListPacket]";
                   case 0xc7:
                       return "[ExGMViewQuestItemListPacket]";
                   case 0xc8:
                       return "[ExResartResponse]";
                   case 0xc9:
                       return "[ExVoteSystemInfoPacket]";
                   case 0xca:
                       return "[ExShuttuleInfoPacket]";
                   case 0xcb:
                       return "[ExSuttleGetOnPacket]";
                   case 0xcc:
                       return "[ExSuttleGetOffPacket]";
                   case 0xcd:
                       return "[ExSuttleMovePacket]";
                   case 0xce:
                       return "[ExMTLInSuttlePacket]";
                   case 0xcf:
                       return "[ExStopMoveInShuttlePacket]";
                   case 0xd0:
                       return "[ExValidateLocationInShuttlePacket]";
                   case 0xd1:
                       return "[ExAgitAuctionCmdPacket]";
                   case 0xd2:
                       return "[ExConfirmAddingPostFriend]";
                   case 0xd3:
                       return "[ExReceiveShowPostFriend]";
                   case 0xd4:
                       return "[ExReceiveOlympiadPacket]";
                   case 0xd5:
                       return "[ExBR_GamePointPacket]";
                   case 0xd6:
                       return "[ExBR_ProductListPacket]";
                   case 0xd7:
                       return "[ExBR_ProductInfoPacket]";
                   case 0xd8:
                       return "[ExBR_BuyProductPacket]";
                   case 0xd9:
                       return "[ExBR_PremiumStatePacket]";
                   case 0xda:
                       return "[ExBrExtraUserInfo]";
                   case 0xdb:
                       return "[ExBrBuffEventState]";
                   case 0xdc:
                       return "[ExBR_RecentProductListPacket]";
                   case 0xdd:
                       return "[ExBR_MinigameLoadScoresPacket]";
                   case 0xde:
                       return "[ExBR_AgathionEnergyInfoPacket]";
                   case 0xdf:
                       return "[ExShowChannelingEffectPacket]";
                   case 0xe0:
                       return "[ExShowChannelingEffectPacket]";
                   case 0xe1:
                       return "[ExGetCrystalizingFail]";
                   case 0xe2:
                       return "[ExNavitAdventPointInfoPacket]";
                   case 0xe3:
                       return "[ExNavitAdventEffectPacket]";
                   case 0xe4:
                       return "[ExNavitAdventTimeChangePacket]";
                   case 0xe5:
                       return "[ExAbnormalStatusUpdateFromTargetPacket]";
                   case 0xe6:
                       return "[ExStopScenePlayerPacket]";
                   case 0xe7:
                       return "[ExFlyMove]";
                   case 0xe8:
                       return "[ExDynamicQuestPacket]";
                   case 0xe9:
                       return "[ExSubjobInfo]";
                   case 0xea:
                       return "[ExChangeMPCost]";
                   case 0xeb:
                       return "[ExFriendDetailInfo]";
                   case 0xec:
                       return "[ExBlockAddResult]";
                   case 0xed:
                       return "[ExBlockRemoveResult]";
                   case 0xee:
                       return "[ExBlockDefailInfo]";
                   case 0xef:
                       return "[ExLoadInzonePartyHistory]";
                   case 0xf0:
                       return "[ExFriendNotifyNameChange]";
                   case 0xf1:
                       return "[ExShowCommission]";
                   case 0xf2:
                       return "[ExResponseCommissionItemList]";
                   case 0xf3:
                       return "[ExResponseCommissionInfo]";
                   case 0xf4:
                       return "[ExResponseCommissionRegister]";
                   case 0xf5:
                       return "[ExResponseCommissionDelete]";
                   case 0xf6:
                       return "[ExResponseCommissionList]";
                   case 0xf7:
                       return "[ExResponseCommissionBuyInfo]";
                   case 0xf8:
                       return "[ExResponseCommissionBuyItem]";
                   case 0xf9:
                       return "[ExAcquirableSkillListByClass]";
                   case 0xfa:
                       return "[ExMagicAttackInfo]";
                   case 0xfb:
                       return "[ExAcquireSkillInfo]";
                   case 0xfc:
                       return "[ExNewSkillToLearnByLevelUp]";
                   case 0xfd:
                       return "[ExCallToChangeClass]";
                   case 0xfe:
                       return "[ExChangeToAwakenedClass]";
                   case 0xff:
                       return "[ExTacticalSign]";
                   case 0x100:
                       return "[ExLoadStatWorldRank]";
                   case 0x101:
                       return "[ExLoadStatUser]";
                   case 0x102:
                       return "[ExLoadStatHotLink]";
                   case 0x103:
                       return "[ExWaitWaitingSubStituteInfo]";
                   case 0x104:
                       return "[ExRegistWaitingSubstituteOk]";
                   case 0x105:
                       return "[ExRegistPartySubstitute]";
                   case 0x106:
                       return "[ExDeletePartySubstitute]";
                   case 0x107:
                       return "[ExTimeOverPartySubstitute]";
                   case 0x108:
                       return "[ExGet24HzSessionID]";
                   case 0x109:
                       return "[Ex2NDPasswordCheckPacket]";
                   case 0x10a:
                       return "[Ex2NDPasswordVerifyPacket]";
                   case 0x10b:
                       return "[Ex2NDPasswordAckPacket]";
                   case 0x10c:
                       return "[ExFlyMoveBroadcast]";
                   case 0x10d:
                       return "[ExShowUsmPacket]";
                   case 0x10e:
                       return "[ExShowStatPage]";
                   case 0x10f:
                       return "[ExIsCharNameCreatable]";
                   case 0x110:
                       return "[ExGoodsInventoryChangedNotiPacket]";
                   case 0x111:
                       return "[ExGoodsInventoryInfoPacket]";
                   case 0x112:
                       return "[ExGoodsInventoryResultPacket]";
                   case 0x113:
                       return "[ExAlterSkillRequest]";
                   case 0x114:
                       return "[ExNotifyFlyMoveStart]";
                   case 0x115:
                       return "[ExDummyPacket]";
                   case 0x116:
                       return "[ExCloseCommission]";
                   case 0x117:
                       return "[ExChangeAttributeItemList]";
                   case 0x118:
                       return "[ExChangeAttributeInfo]";
                   case 0x119:
                       return "[ExChangeAttributeOk]";
                   case 0x11a:
                       return "[ExChangeAttributeFail]";
                   case 0x11b:
                       return "[ExExchangeSubstitute]";
                   case 0x11c:
                       return "[ExLightingCandleEvent]";
                   case 0x11d:
                       return "[ExVitalityEffectInfo]";
                   case 0x11e:
                       return "[ExLoginVitalityEffectInfo]";
                   case 0x11f:
                       return "[ExBR_PresentBuyProductPacket]";
                   case 0x120:
                       return "[ExMentorList]";
                   case 0x121:
                       return "[ExMentorAdd]";
                   case 0x122:
                       return "[ExChoose_Shape_Shifting_Item]";
                   case 0x123:
                       return "[ExPut_Shape_Shifting_Target_Item_Result]";
                   case 0x124:
                       return "[ExPut_Shape_Shifting_Extraction_Item_Result]";
                   case 0x125:
                       return "[ExShape_Shifting_Result]";
                   case 0x126:
                       return "[ExCheck_SpeedHack]";
                   default:
                       return "[Unknow Ex Packet]";
                   #endregion

               }


           }
           */
           private void apply_filters()
           {
               Globals.pck_thread.client_pck_filter.Clear();
               Globals.pck_thread.server_pck_filter.Clear();
               Globals.pck_thread.white_list_filter = Globals.pck_thread.pck_fill_window.check_white_filter_state();
               for(int i = 0;i<tmp_client_pck_filter.Count;i++)
               {
                   Globals.pck_thread.client_pck_filter.Add(tmp_client_pck_filter[i]);
               }
               for(int i = 0;i<tmp_server_pck_filter.Count;i++)
               {
                   Globals.pck_thread.server_pck_filter.Add(tmp_server_pck_filter[i]);
               }
               if (Globals.pck_thread.filter_wind_pck == true)
               {
                   create_filtered_list();
                   Globals.pck_thread.pck_window.refresh_window();
               }
           }
           private void save_filters()
           {
               try
               {
                   apply_filters();
                   string file_patch = Globals.PATH;
                   file_patch += "\\pck_filters.dat";
                   System.IO.BinaryWriter test = new BinaryWriter(File.Open(file_patch, FileMode.Create));
                   test.Write(Globals.pck_thread.client_pck_filter.Count );
                   test.Write(Globals.pck_thread.server_pck_filter.Count );
                   //client filter
                   for(int i = 0;i<Globals.pck_thread.client_pck_filter.Count;i++)
                   {
                       test.Write(Globals.pck_thread.client_pck_filter[i]);
                   }
                   // server filter
                    for(int i = 0;i<Globals.pck_thread.server_pck_filter.Count;i++)
                   {
                       test.Write(Globals.pck_thread.server_pck_filter[i]);
                   }
               }

               catch
               {
                   // to do ...
               }
           }
           public void load_filters()
           {
               try
               {
                  // Globals.pck_thread.addtemp_var();
                   client_pck_filter.Clear();
                   server_pck_filter.Clear();
                   tmp_client_pck_filter.Clear();
                   tmp_server_pck_filter.Clear();
                   string file_patch = Globals.PATH;
                   file_patch += "\\pck_filters.dat";
                  // if(file_patch.
                   System.IO.BinaryReader redfil = new BinaryReader(File.Open(file_patch, FileMode.Open));
                   int temp_ilo_cli = redfil.ReadInt32();
                   int temp_ilo_srv = redfil.ReadInt32();

                   if (temp_ilo_cli > 0)
                   {
                       for (int i = 0; i < temp_ilo_cli; i++)
                       {
                           Globals.pck_thread.client_pck_filter.Add(redfil.ReadInt32());
                       }
                   }
                   if (temp_ilo_srv > 0)
                   {
                       for (int i = 0; i < temp_ilo_srv; i++)
                       {
                           Globals.pck_thread.server_pck_filter.Add(redfil.ReadInt32());
                       }
                   }
                   //
                   //
                   for (int i = 0; i < Globals.pck_thread.client_pck_filter.Count; i++)
                   {
                      tmp_client_pck_filter.Add(Globals.pck_thread.client_pck_filter[i]);
                   }
                   for (int i = 0; i < Globals.pck_thread.server_pck_filter.Count; i++)
                   {
                       tmp_server_pck_filter.Add(Globals.pck_thread.server_pck_filter[i]);
                   }
                   Globals.pck_thread.pck_fill_window.refresh_windows();
                   apply_filters();
                   //if (Globals.pck_thread.filter_wind_pck == true)
                   //{
                       //Globals.pck_thread.pck_window.refresh_window();
                   //}
               }
               catch
               {
                   // to do ...
               }


           }
           private void create_filtered_list()
           {
               Globals.pck_thread.filtered_pck.Clear();
               for (int i = 0; i < Globals.pck_thread.pck_record.Count; i++)
               {

                   if (Globals.pck_thread.pck_record[i].type == 1)// client pck
                   {
                       if (!hide_cli_pck)// hide client pck
                       {
                           if (check_client_pck_in_filter(Globals.pck_thread.pck_record[i]))
                           {
                               if (Globals.pck_thread.white_list_filter)
                               {
                                   Globals.pck_thread.filtered_pck.Add(i);
                               }
                               // Globals.pck_thread.pck_window.add_to_list(Globals.pck_thread.mine_queue.First()); // add to normal list
                           }
                           else// not in filter list ...
                           {
                               if (!Globals.pck_thread.white_list_filter)
                               {
                                   Globals.pck_thread.filtered_pck.Add(i);
                               }
                           }
                       }
                   }
                   else // server pck
                   {
                       if (!hide_srv_pck)
                       {
                           if (check_Serwer_pck_in_filter(Globals.pck_thread.pck_record[i]))
                           {
                               if (Globals.pck_thread.white_list_filter)
                               {
                                   Globals.pck_thread.filtered_pck.Add(i);
                               }
                               // Globals.pck_thread.pck_window.add_to_list(Globals.pck_thread.mine_queue.First()); // add to normal list
                           }
                           else// not in filter list ...
                           {
                               if (!Globals.pck_thread.white_list_filter)
                               {
                                   Globals.pck_thread.filtered_pck.Add(i);
                               }
                           }
                       }
                   }
               }
           }
           private void search_from_beg(pck_window_dat tmp_dat)
           {
               try
               {
                    string txt_from_box = "";
                   if(tmp_dat.type == 2) // search in server packets
                   {
#region server pck check
                        txt_from_box = tmp_dat.time.Replace(" ", ""); // removing spaces
                        txt_from_box = txt_from_box.Replace(":", ""); // removing :
                      if (txt_from_box.Length >= 2 && txt_from_box.Length <= 6)
                        {
                            if (txt_from_box.Length % 2==0)// check for 3 and 5 legh...
                            {
                                byte[] bytebuf = new byte[4];
                                byte [] tem_bytebuf = Globals.pck_thread.StringToByteArray2(txt_from_box);
                                for (int i = 0; i < tem_bytebuf.Length; i++)
                                {
                                    bytebuf[i] = tem_bytebuf[i];
                                }
                                //check part
                                int index = search_server_pck_new(bytebuf, 0, 1);
                                if(index != -1)//
                                 {
                                        Globals.pck_thread.pck_window.selectitem(index);
                                 }
                                /*
                                if (bytebuf[0] != 0xfe) // not ex pck
                                {
                                    // fun for check
                                    int index = search_server_pck(bytebuf[0],0,1);
                                    if(index != -1)//
                                    {
                                        Globals.pck_thread.pck_window.selectitem(index);
                                    }

                                }
                                else // ex pck
                                {
                                    int temp_id = System.BitConverter.ToUInt16(bytebuf, 1);
                                    temp_id = temp_id + 0xfe;
                                   int index = search_server_pck(temp_id,0,1);
                                    if(index != -1)//
                                    {
                                        Globals.pck_thread.pck_window.selectitem(index);
                                    }
                                    
                                }
                                */
                            }

                      }
#endregion
                   }
                   else if(tmp_dat.type == 1)//search in client pck ...
                   {
                       #region client pck check
                       txt_from_box = tmp_dat.time.Replace(" ", ""); // removing spaces
                       txt_from_box = txt_from_box.Replace(":", ""); // removing :
                                if (txt_from_box.Length >= 2 && txt_from_box.Length <= 6)
                                    {
                                        if (txt_from_box.Length % 2==0)// check for 3 and 5 legh...
                                        {
                                            byte[] bytebuf = new byte[4];
                                            byte [] tem_bytebuf = Globals.pck_thread.StringToByteArray2(txt_from_box);
                                            for (int i = 0; i < tem_bytebuf.Length; i++)
                                            {
                                                bytebuf[i] = tem_bytebuf[i];
                                            }
                                            //check part
                                            int index = search_cli_pck_new(bytebuf, 0, 1);
                                            if (index != -1)//
                                            {
                                                Globals.pck_thread.pck_window.selectitem(index);
                                            }

                                            /*
                                            if (bytebuf[0] != 0xd0) // not d0 pck
                                            {
                                                // fun for check
                                                int index = search_cli_pck(bytebuf[0],0,1);
                                                if(index != -1)//
                                                {
                                                    Globals.pck_thread.pck_window.selectitem(index);
                                                }

                                            }
                                            else //  d0 pck
                                            {
                                                int temp_id = System.BitConverter.ToUInt16(bytebuf, 1);
                                                temp_id = temp_id + 0xd0;
                                               int index = search_cli_pck(temp_id,0,1);
                                                if(index != -1)//
                                                {
                                                    Globals.pck_thread.pck_window.selectitem(index);
                                                }
                                    
                                            }
                                            */
                                        }

                                    }
                       #endregion
                   }


               }
               catch
               {
                   //catch them all
               }
           }
        /*  public int search_server_pck(int id,int start_from,int type_search)//id = packet id , start from =index start , type 1 - up,2 - down
        {
            if(Globals.pck_thread.filter_wind_pck == true)
            {
                if (type_search == 1)
                {
                    for (int i = start_from; i < Globals.pck_thread.filtered_pck.Count; i++)
                    {
                        if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].type == 2)
                        {
                            if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer[0] == id)
                            {
                                return i;
                            }
                        }
                    }
                }
                else // search up ... i know .. tress but fuck that im lazy :P
                {
                    for (int i = start_from; i >=0 ; i--)
                    {
                        if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].type == 2)
                        {
                            if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer[0] == id)
                            {
                                return i;
                            }
                        }
                    }
                }
            }
            else // filter off
            {
                if (type_search == 1)
                {
                    for (int i = start_from; i < Globals.pck_thread.pck_record.Count; i++)
                    {
                        if (Globals.pck_thread.pck_record[i].type == 2)
                        {
                            if (Globals.pck_thread.pck_record[i].bytebuffer[0] == id)
                            {
                                return i;
                            }
                        }
                    }
                }
                else // tress tress :P
                {
                    for (int i = start_from; i >=0; i--)
                    {
                        if (Globals.pck_thread.pck_record[i].type == 2)
                        {
                            if (Globals.pck_thread.pck_record[i].bytebuffer[0] == id)
                            {
                                return i;
                            }
                        }
                    }
                }
            }
            return -1;

        }
         *
        //   public int search_cli_pck(int id,int start_from,int type)
        {
            if(Globals.pck_thread.filter_wind_pck == true)
            {
                if (type == 1)
                {
                    for (int i = start_from; i < Globals.pck_thread.filtered_pck.Count; i++)
                    {
                        if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].type == 1)
                        {
                            if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer[0] == id)
                            {
                                return i;
                            }
                        }
                    }
                }
                else // search up ... i know .. tress but fuck that im lazy :P
                {
                    for (int i = start_from; i >=0; i--)
                    {
                        if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].type == 1)
                        {
                            if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer[0] == id)
                            {
                                return i;
                            }
                        }
                    }
                }
            }
            else // filter off
            {
                if (type == 1)
                {
                    for (int i = start_from; i < Globals.pck_thread.pck_record.Count; i++)
                    {
                        if (Globals.pck_thread.pck_record[i].type == 1)
                        {
                            if (Globals.pck_thread.pck_record[i].bytebuffer[0] == id)
                            {
                                return i;
                            }
                        }
                    }
                }
                else
                {
                    for (int i = start_from; i >=0; i--)
                    {
                        if (Globals.pck_thread.pck_record[i].type == 1)
                        {
                            if (Globals.pck_thread.pck_record[i].bytebuffer[0] == id)
                            {
                                return i;
                            }
                        }
                    }
                }
            }
            return -1;
         }
         */
           private void search_down(pck_window_dat tmp_dat)
           {
               try
               {
                   string txt_from_box = "";
                   int index_start = Globals.pck_thread.pck_window.ret_select();
                   index_start++;
                   if (index_start != -1)
                   {
                       if (tmp_dat.type == 2) // search in server packets
                       {
                           #region server pck check2
                           txt_from_box = tmp_dat.time.Replace(" ", ""); // removing spaces
                           txt_from_box = txt_from_box.Replace(":", ""); // removing :

                           if (txt_from_box.Length >= 2 && txt_from_box.Length <= 6)
                           {
                               if (txt_from_box.Length % 2 == 0)// check for 3 and 5 legh...
                               {
                                   byte[] bytebuf = new byte[4];
                                   byte[] tem_bytebuf = StringToByteArray2(txt_from_box);
                                   for (int i = 0; i < tem_bytebuf.Length; i++)
                                   {
                                       bytebuf[i] = tem_bytebuf[i];
                                   }
                                   //check part
                                   int index = search_server_pck_new(bytebuf, index_start, 1);
                                   if (index != -1)//
                                   {
                                       Globals.pck_thread.pck_window.selectitem(index);
                                   }


                                   /*
                                   if (bytebuf[0] != 0xfe) // not ex pck
                                   {
                                       // fun for check
                                       int index = search_server_pck(bytebuf[0], index_start,1);
                                       if (index != -1)//
                                       {
                                           Globals.pck_thread.pck_window.selectitem(index);
                                       }

                                   }
                                   else // ex pck
                                   {
                                       int temp_id = System.BitConverter.ToUInt16(bytebuf, 1);
                                       temp_id = temp_id + 0xfe;
                                       int index = search_server_pck(temp_id, index_start,1);
                                       if (index != -1)//
                                       {
                                           Globals.pck_thread.pck_window.selectitem(index);
                                       }

                                   }
                                   */
                               }

                           }
                           #endregion
                       }
                       else if (tmp_dat.type == 1)//search in client pck ...
                       {
                           #region client pck check2
                           txt_from_box = tmp_dat.time.Replace(" ", ""); // removing spaces
                           txt_from_box = tmp_dat.time.Replace(":", ""); // removing :
                           if (txt_from_box.Length >= 2 && txt_from_box.Length <= 6)
                           {
                               if (txt_from_box.Length % 2 == 0)// check for 3 and 5 legh...
                               {
                                   byte[] bytebuf = new byte[4];
                                   byte[] tem_bytebuf = Globals.pck_thread.StringToByteArray2(txt_from_box);
                                   for (int i = 0; i < tem_bytebuf.Length; i++)
                                   {
                                       bytebuf[i] = tem_bytebuf[i];
                                   }
                                   int index = search_cli_pck_new(bytebuf, index_start, 1);
                                   if (index != -1)//
                                   {
                                       Globals.pck_thread.pck_window.selectitem(index);
                                   }


                                   /*
                                   //check part

                                   if (bytebuf[0] != 0xd0) // not d0 pck
                                   {
                                       // fun for check
                                       int index = search_cli_pck(bytebuf[0], index_start, 1);
                                       if (index != -1)//
                                       {
                                           Globals.pck_thread.pck_window.selectitem(index);
                                       }

                                   }
                                   else //  d0 pck
                                   {
                                       int temp_id = System.BitConverter.ToUInt16(bytebuf, 1);
                                       temp_id = temp_id + 0xd0;
                                       int index = search_cli_pck(temp_id, index_start,1);
                                       if (index != -1)//
                                       {
                                           Globals.pck_thread.pck_window.selectitem(index);
                                       }

                                   }
                                   */
                               }

                           }
                           #endregion
                       }
                   }
               }
               catch
               {
                   //throw pika
               }
           }
           private void search_up(pck_window_dat tmp_dat)
           {
               try
               {
                   string txt_from_box = "";
                   int index_start = Globals.pck_thread.pck_window.ret_select();
                   index_start--;
                   if (index_start != -1)
                   {
                       if (tmp_dat.type == 2) // search in server packets
                       {
                           #region server pck check2
                           txt_from_box = tmp_dat.time.Replace(" ", ""); // removing spaces
                           txt_from_box = txt_from_box.Replace(":", ""); // removing :

                           if (txt_from_box.Length >= 2 && txt_from_box.Length <= 6)
                           {
                               if (txt_from_box.Length % 2 == 0)// check for 3 and 5 legh...
                               {
                                   byte[] bytebuf = new byte[4];
                                   byte[] tem_bytebuf = StringToByteArray2(txt_from_box);
                                   for (int i = 0; i < tem_bytebuf.Length; i++)
                                   {
                                       bytebuf[i] = tem_bytebuf[i];
                                   }
                                   //check part
                                   int index = search_server_pck_new(bytebuf, index_start, 2);
                                   if (index != -1)//
                                   {
                                       Globals.pck_thread.pck_window.selectitem(index);
                                   }

                                   /*
                                   if (bytebuf[0] != 0xfe) // not ex pck
                                   {
                                       // fun for check
                                       int index = search_server_pck(bytebuf[0], index_start,2);
                                       if (index != -1)//
                                       {
                                           Globals.pck_thread.pck_window.selectitem(index);
                                       }

                                   }
                                   else // ex pck
                                   {
                                       int temp_id = System.BitConverter.ToUInt16(bytebuf, 1);
                                       temp_id = temp_id + 0xfe;
                                       int index = search_server_pck(temp_id, index_start,2);
                                       if (index != -1)//
                                       {
                                           Globals.pck_thread.pck_window.selectitem(index);
                                       }

                                   }
                                   */
                               }

                           }
                           #endregion
                       }
                       else if (tmp_dat.type == 1)//search in client pck ...
                       {
                           #region client pck check2
                           txt_from_box = tmp_dat.time.Replace(" ", ""); // removing spaces
                           txt_from_box = tmp_dat.time.Replace(":", ""); // removing :
                           if (txt_from_box.Length >= 2 && txt_from_box.Length <= 6)
                           {
                               if (txt_from_box.Length % 2 == 0)// check for 3 and 5 legh...
                               {
                                   byte[] bytebuf = new byte[4];
                                   byte[] tem_bytebuf = Globals.pck_thread.StringToByteArray2(txt_from_box);
                                   for (int i = 0; i < tem_bytebuf.Length; i++)
                                   {
                                       bytebuf[i] = tem_bytebuf[i];
                                   }
                                   int index = search_cli_pck_new(bytebuf, index_start, 2);
                                   if (index != -1)//
                                   {
                                       Globals.pck_thread.pck_window.selectitem(index);
                                   }

                                   /*
                                   //check part
                                   if (bytebuf[0] != 0xd0) // not d0 pck
                                   {
                                       // fun for check
                                       int index = search_cli_pck(bytebuf[0], index_start,2);
                                       if (index != -1)//
                                       {
                                           Globals.pck_thread.pck_window.selectitem(index);
                                       }

                                   }
                                   else //  d0 pck
                                   {
                                       int temp_id = System.BitConverter.ToUInt16(bytebuf, 1);
                                       temp_id = temp_id + 0xd0;
                                       int index = search_cli_pck(temp_id, index_start,2);
                                       if (index != -1)//
                                       {
                                           Globals.pck_thread.pck_window.selectitem(index);
                                       }

                                   }
                                   */
                               }

                           }
                           #endregion
                       }
                   }
               }
               catch
               {
                   //throw pika
               }
           }
           private int search_server_pck_new(byte[] bytbuf, int start_from, int type_search)//id = packet id , start from =index start , type 1 - up(0-222..),2 - down(222-0)
           {
               if (Globals.pck_thread.filter_wind_pck == true)
               {
                   if (type_search == 1)
                   {
                       for (int i = start_from; i < Globals.pck_thread.filtered_pck.Count; i++)
                       {
                           if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].type == 2)
                           {
                               if(bytbuf[0] != 0xfe )
                               {
                                   if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer[0] == bytbuf[0])
                                   {
                                       return i;
                                   }
                               }
                               else
                               {
                                   int yes = 0;
                                   if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer.Length >= 3)
                                   {
                                       for (int j = 0; j < 3; j++)
                                       {
                                           if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer[j] == bytbuf[j])
                                           {
                                               yes++;
                                           }
                                       }
                                   }
                                   if (yes == 3)
                                       return i;
                               }
                           }
                       }
                   }
                   else // search up ... i know .. tress but fuck that im lazy :P
                   {
                       for (int i = start_from; i >= 0; i--)
                       {
                           if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].type == 2)
                           {
                               if (bytbuf[0] != 0xfe)
                               {
                                   if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer[0] == bytbuf[0])
                                   {
                                       return i; ;
                                   }
                               }
                               else
                               {
                                   int yes = 0;
                                   if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer.Length >= 3)
                                   {
                                       for (int j = 0; j < 3; j++)
                                       {
                                           if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer[j] == bytbuf[j])
                                           {
                                               yes++;
                                           }
                                       }
                                   }
                                   if (yes == 3)
                                   {
                                       return i;
                                   }
                               }
                           }
                       }
                   }
               }
               else // filter off
               {
                   if (type_search == 1)
                   {
                       for (int i = start_from; i < Globals.pck_thread.pck_record.Count; i++)
                       {
                           if (Globals.pck_thread.pck_record[i].type == 2)
                           {
                               if (bytbuf[0] != 0xfe)
                               {
                                   if (Globals.pck_thread.pck_record[i].bytebuffer[0] == bytbuf[0])
                                   {
                                       return i; ;
                                   }
                               }
                               else
                               {
                                   int yes = 0;
                                   if (Globals.pck_thread.pck_record[i].bytebuffer.Length >= 3)
                                   {
                                       for (int j = 0; j < 3; j++)
                                       {
                                           if (Globals.pck_thread.pck_record[i].bytebuffer[j] == bytbuf[j])
                                           {
                                               yes++;
                                           }
                                       }
                                   }
                                   if (yes == 3)
                                   {
                                       return i;
                                   }

                               }
                           }
                       }
                   }
                   else // tress tress :P
                   {
                       for (int i = start_from; i >= 0; i--)
                       {
                           if (Globals.pck_thread.pck_record[i].type == 2)
                           {
                               if (bytbuf[0] != 0xfe)
                               {
                                   if (Globals.pck_thread.pck_record[i].bytebuffer[0] == bytbuf[0])
                                   {
                                       return i; ;
                                   }
                               }
                               else
                               {
                                   int yes = 0;
                                   if (Globals.pck_thread.pck_record[i].bytebuffer.Length >= 3)
                                   {
                                       for (int j = 0; j < 3; j++)
                                       {
                                           if (Globals.pck_thread.pck_record[i].bytebuffer[j] == bytbuf[j])
                                           {
                                               yes++;
                                           }
                                       }
                                   }
                                   if (yes == 3)
                                   {
                                       return i;
                                   }

                               }
                           }
                       }
                   }
               }
               return -1;

           }
           private int search_cli_pck_new(byte[] bytbuf, int start_from, int type_search)////id = packet id , start from =index start , type 1 - up(0-222..),2 - down(222-0)
           {
               if (Globals.pck_thread.filter_wind_pck == true)
               {
                   if (type_search == 1)
                   {
                       for (int i = start_from; i < Globals.pck_thread.filtered_pck.Count; i++)
                       {
                           if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].type == 1)
                           {
                               if (bytbuf[0] != 0xd0)
                               {
                                   if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer[0] == bytbuf[0])
                                   {
                                       return i; ;
                                   }
                               }
                               else
                               {
                                   int yes = 0;
                                   if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer.Length >= 3)
                                   {
                                       for (int j = 0; j < 3; j++)
                                       {
                                           if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer[j] == bytbuf[j])
                                           {
                                               yes++;
                                           }
                                       }
                                   }
                                   if (yes == 3)
                                       return i;
                               }
                           }
                       }
                   }
                   else // search up ... i know .. tress but fuck that im lazy :P
                   {
                       for (int i = start_from; i >= 0; i--)
                       {
                           if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].type == 1)
                           {
                               if (bytbuf[0] != 0xd0)
                               {
                                   if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer[0] == bytbuf[0])
                                   {
                                       return i; ;
                                   }
                               }
                               else
                               {
                                   int yes = 0;
                                   if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer.Length >= 3)
                                   {
                                       for (int j = 0; j < 3; j++)
                                       {
                                           if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer[j] == bytbuf[j])
                                           {
                                               yes++;
                                           }
                                       }
                                   }
                                   if (yes == 3)
                                   {
                                       return i;
                                   }
                               }
                           }
                       }
                   }
               }
               else // filter off
               {
                   if (type_search == 1)
                   {
                       for (int i = start_from; i < Globals.pck_thread.pck_record.Count; i++)
                       {
                           if (Globals.pck_thread.pck_record[i].type == 1)
                           {
                               if (bytbuf[0] != 0xd0)
                               {
                                   if (Globals.pck_thread.pck_record[i].bytebuffer[0] == bytbuf[0])
                                   {
                                       return i; ;
                                   }
                               }
                               else
                               {
                                   int yes = 0;
                                   if (Globals.pck_thread.pck_record[i].bytebuffer.Length >= 3)
                                   {
                                       for (int j = 0; j < 3; j++)
                                       {
                                           if (Globals.pck_thread.pck_record[i].bytebuffer[j] == bytbuf[j])
                                           {
                                               yes++;
                                           }
                                       }
                                   }
                                   if (yes == 3)
                                   {
                                       return i;
                                   }

                               }
                           }
                       }
                   }
                   else // tress tress :P
                   {
                       for (int i = start_from; i >= 0; i--)
                       {
                           if (Globals.pck_thread.pck_record[i].type == 1)
                           {
                               if (bytbuf[0] != 0xd0)
                               {
                                   if (Globals.pck_thread.pck_record[i].bytebuffer[0] == bytbuf[0])
                                   {
                                       return i; ;
                                   }
                               }
                               else
                               {
                                   int yes = 0;
                                   if (Globals.pck_thread.pck_record[i].bytebuffer.Length >= 3)
                                   {
                                       for (int j = 0; j < 3; j++)
                                       {
                                           if (Globals.pck_thread.pck_record[i].bytebuffer[j] == bytbuf[j])
                                           {
                                               yes++;
                                           }
                                       }
                                   }
                                   if (yes == 3)
                                   {
                                       return i;
                                   }

                               }
                           }
                       }
                   }
               }
               return -1;

           }
           public void load_client_names()
           {
               //client_pck_names - w globals
               //                   System.IO.BinaryReader redfil = new BinaryReader(File.Open(file_patch, FileMode.Open));
               try
               {
                Globals.pck_thread.client_pck_names.Clear();
               string file_patch = Globals.PATH;
               file_patch += "\\data\\client_packets.txt";
               if (File.Exists(file_patch))
               {
                   System.IO.StreamReader pack_file = new System.IO.StreamReader(file_patch);
                   string newline;
                   string separator = "|";

                   int tmp_index = 0;
                  // int tem_index2 = 0;
                   string help_string;
                   string help_string2;
                   while((newline=pack_file.ReadLine())!=null)
                   {
                       if (newline.Length > 0)
                       {
                           if(newline.Contains(separator))
                           {
                               newline = newline.Replace(" ", ""); // removing spaces
                               tmp_index = newline.IndexOf(separator);
                               help_string = newline.Substring(0, tmp_index);
                               help_string2 = newline.Substring(tmp_index + 1, newline.Length - tmp_index-1);
                               help_string = help_string.Replace(":", "");


                               if (help_string.Length >= 2 && help_string.Length <= 6)
                               {
                                   if (help_string.Length % 2 == 0)// check for 3 and 5 legh...
                                   {
                                       int tmp_id = 0;
                                       byte[] tem_bytebuf = Globals.pck_thread.StringToByteArray2(help_string);
                                       for (int i = 0; i < tem_bytebuf.Length; i++)
                                       {
                                           tmp_id += tem_bytebuf[i];
                                       }
                                       if(!check_client_sortedlist(tmp_id))
                                       {
                                            Globals.pck_thread.client_pck_names.Add(tmp_id.ToString(), help_string2);
                                       }


                                   }
                               } //  if (help_string.Length >= 2 && help_string.Length <= 6)

                               //Globals.pck_thread.client_pck_names.Add(help_string, help_string2);
                           }//if(newline.Contains(separator))

                       }// if (newline.Length > 0)
                   }//while ...

               }//if (File.Exists(file_patch))
               //StreamReader sr = new StreamReader("TestFile.txt")
               }
                catch
               {
                   System.Windows.Forms.MessageBox.Show("problem with client_packets.txt file", "Pika Pika!", System.Windows.Forms.MessageBoxButtons.OK);
               }
           }
           private bool check_client_sortedlist(int id)
           {
               if( Globals.pck_thread.client_pck_names.Contains(id.ToString()))
               {
                   return true;
               }
               else
               {
                   return false;
               }
           }
           public void load_server_names()
           {
               try
               {
                   Globals.pck_thread.server_pck_names.Clear();
                   string file_patch = Globals.PATH;
                   file_patch += "\\data\\server_packets.txt";
                   if (File.Exists(file_patch))
                   {
                       System.IO.StreamReader pack_file = new System.IO.StreamReader(file_patch);
                       string newline;
                       string separator = "|";

                       int tmp_index = 0;
                       // int tem_index2 = 0;
                       string help_string;
                       string help_string2;
                       while ((newline = pack_file.ReadLine()) != null)
                       {
                           if (newline.Length > 0)
                           {
                               if (newline.Contains(separator))
                               {
                                   newline = newline.Replace(" ", ""); // removing spaces
                                   tmp_index = newline.IndexOf(separator);
                                   help_string = newline.Substring(0, tmp_index);
                                   help_string2 = newline.Substring(tmp_index + 1, newline.Length - tmp_index - 1);
                                   help_string = help_string.Replace(":", "");


                                   if (help_string.Length >= 2 && help_string.Length <= 6)
                                   {
                                       if (help_string.Length % 2 == 0)// check for 3 and 5 legh...
                                       {
                                           int tmp_id = 0;
                                           byte[] tem_bytebuf = Globals.pck_thread.StringToByteArray2(help_string);
                                           for (int i = 0; i < tem_bytebuf.Length; i++)
                                           {
                                               tmp_id += tem_bytebuf[i];
                                           }
                                           if (!check_server_sortedlist(tmp_id))
                                           {
                                               Globals.pck_thread.server_pck_names.Add(tmp_id.ToString(), help_string2);
                                           }


                                       }
                                   } //  if (help_string.Length >= 2 && help_string.Length <= 6)

                                   //Globals.pck_thread.client_pck_names.Add(help_string, help_string2);
                               }//if(newline.Contains(separator))

                           }// if (newline.Length > 0)
                       }//while ...

                   }//if (File.Exists(file_patch))
               }
               catch
               {
                   System.Windows.Forms.MessageBox.Show("problem with server_packets.txt file", "Pika Pika!", System.Windows.Forms.MessageBoxButtons.OK);
               }
           }
           private bool check_server_sortedlist(int id)
           {
               if (Globals.pck_thread.server_pck_names.Contains(id.ToString()))
               {
                   return true;
               }
               else
               {
                   return false;
               }
           }
           public string new_get_client_pck_name(int id)
           {
               if (Globals.pck_thread.client_pck_names.Contains(id.ToString()))
               {
                 return (string)Globals.pck_thread.client_pck_names[id.ToString()];
               }
               else
               {
                   return "unknow packet";
               }
           }
           public string new_get_server_pck_name(int id)
           {
               if (Globals.pck_thread.server_pck_names.Contains(id.ToString()))
               {
                   return (string)Globals.pck_thread.server_pck_names[id.ToString()];
               }
               else
               {
                   return "unknow packet";
               }
           }
           private void search_from_beg_by_name(pck_window_dat tmp_dat)
           {
               try
               {
                   string txt_from_box = "";
                   txt_from_box = tmp_dat.time.Replace(" ", ""); // removing spaces
                   txt_from_box = txt_from_box.Replace(":", ""); // removing :
                   txt_from_box = txt_from_box.Replace("[", ""); // removing [
                   txt_from_box = txt_from_box.Replace("]", ""); // removing ]
                   txt_from_box = txt_from_box.ToLower();
                   if (tmp_dat.type == 1) //search_cli_pck_new in ClientPackets pck
                   {
                       if (Globals.pck_thread.filter_wind_pck == true)
                       {
                           int temp_id = 0;
                           for (int i = 0; i < Globals.pck_thread.filtered_pck.Count; i++)
                           {
                               if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].type == 1)
                               {
                                   if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer[0] < 0xd0)
                                   {
                                       temp_id = Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer[0];
                                   }
                                   else
                                   {
                                       if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer.Length > 3)
                                       {
                                           for (int j = 0; j < 3; j++)
                                           {
                                               temp_id += Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer[j];
                                           }
                                       }
                                   }

                                   string tmp_name = new_get_client_pck_name(temp_id).ToLower();
                                   if(tmp_name.Contains(tmp_dat.time))
                                   {
                                           Globals.pck_thread.pck_window.selectitem(i);
                                           return;
                                   }
                                   /*
                                   if (tmp_dat.time.Length < tmp_name.Length)
                                   {
                                       int par_count = tmp_dat.time.Length;
                                       for (int j = 0; j < tmp_dat.time.Length; j++)
                                       {
                                           if (tmp_dat.time[j] == tmp_name[j])
                                           {
                                               par_count--;
                                           }
                                       }
                                       if (par_count == 0)
                                       {
                                           Globals.pck_thread.pck_window.selectitem(i);
                                           return;
                                       }
                                    
                                   }
                                    */ 
                               }
                           }
                       }
                       else
                       {
                            int temp_id = 0;
                            for (int i = 0; i < Globals.pck_thread.pck_record.Count; i++)
                            {
                                if (Globals.pck_thread.pck_record[i].type == 1)
                                {
                                    if (Globals.pck_thread.pck_record[i].bytebuffer[0] < 0xd0)
                                    {
                                        temp_id =Globals.pck_thread.pck_record[i].bytebuffer[0] ;
                                    }
                                    else
                                    {
                                        if (Globals.pck_thread.pck_record[i].bytebuffer.Length > 3)
                                        {
                                            for (int j = 0; j < 3; j++)
                                            {
                                                temp_id += Globals.pck_thread.pck_record[i].bytebuffer[j];
                                            }
                                        }
                                    }
                                    string tmp_name = new_get_client_pck_name(temp_id).ToLower();
                                    if (tmp_name.Contains(tmp_dat.time))
                                    {
                                        Globals.pck_thread.pck_window.selectitem(i);
                                        return;
                                    }
                                    /*
                                    if (tmp_dat.time.Length < tmp_name.Length)
                                    {
                                        int par_count = tmp_dat.time.Length;
                                        for (int j = 0; j < tmp_dat.time.Length; j++)
                                        {
                                            if (tmp_dat.time[j] == tmp_name[j])
                                            {
                                                par_count--;
                                            }
                                        }
                                        if (par_count == 0)
                                        {
                                                Globals.pck_thread.pck_window.selectitem(i);
                                                return;
                                        }
                                    }

                                     */
                                }// if cli
                            }//for
                          // string tmp_name= new_get_client_pck_name();
                       }
                   }
                   else // server pck
                   {
                       if (Globals.pck_thread.filter_wind_pck == true)
                       {
                           int temp_id = 0;
                           for (int i = 0; i < Globals.pck_thread.filtered_pck.Count; i++)
                           {
                               if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].type == 2)
                               {
                                   if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer[0] < 0xfe)
                                   {
                                       temp_id = Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer[0];
                                   }
                                   else
                                   {
                                       if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer.Length > 3)
                                       {
                                           for (int j = 0; j < 3; j++)
                                           {
                                               temp_id += Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer[j];
                                           }
                                       }
                                   }

                                   string tmp_name = new_get_server_pck_name(temp_id).ToLower();
                                   if (tmp_name.Contains(tmp_dat.time))
                                   {
                                       Globals.pck_thread.pck_window.selectitem(i);
                                       return;
                                   }
                                   /*
                                   if (tmp_dat.time.Length < tmp_name.Length)
                                   {
                                       int par_count = tmp_dat.time.Length;
                                       for (int j = 0; j < tmp_dat.time.Length; j++)
                                       {
                                           if (tmp_dat.time[j] == tmp_name[j])
                                           {
                                               par_count--;
                                           }
                                       }
                                       if (par_count == 0)
                                       {
                                           Globals.pck_thread.pck_window.selectitem(i);
                                           return;
                                       }
                                   }
                                    */
                               }
                           }

                       }
                       else
                       {
                           int temp_id = 0;
                           for (int i = 0; i < Globals.pck_thread.pck_record.Count; i++)
                           {
                               if (Globals.pck_thread.pck_record[i].type == 2)
                               {
                                   if (Globals.pck_thread.pck_record[i].bytebuffer[0] < 0xfe)
                                   {
                                       temp_id = Globals.pck_thread.pck_record[i].bytebuffer[0];
                                   }
                                   else
                                   {
                                       if (Globals.pck_thread.pck_record[i].bytebuffer.Length > 3)
                                       {
                                           for (int j = 0; j < 3; j++)
                                           {
                                               temp_id += Globals.pck_thread.pck_record[i].bytebuffer[j];
                                           }
                                       }
                                   }
                                   string tmp_name = new_get_server_pck_name(temp_id).ToLower();
                                   if (tmp_name.Contains(tmp_dat.time))
                                   {
                                       Globals.pck_thread.pck_window.selectitem(i);
                                       return;
                                   }
                                   /*
                                   if (tmp_dat.time.Length < tmp_name.Length)
                                   {
                                       int par_count = tmp_dat.time.Length;
                                       for (int j = 0; j < tmp_dat.time.Length; j++)
                                       {
                                           if (tmp_dat.time[j] == tmp_name[j])
                                           {
                                               par_count--;
                                           }
                                       }
                                       if (par_count == 0)
                                       {
                                           Globals.pck_thread.pck_window.selectitem(i);
                                           return;
                                       }
                                   }
                                    */
                               }
                           }
                       }
                   }
               }
               catch
               {
                   // leg == 0:d
               }
           }
           private void search_down_by_name(pck_window_dat tmp_dat)
           {
               try
               {
                   string txt_from_box = "";
                   txt_from_box = tmp_dat.time.Replace(" ", ""); // removing spaces
                   txt_from_box = txt_from_box.Replace(":", ""); // removing :
                   txt_from_box = txt_from_box.Replace("[", ""); // removing [
                   txt_from_box = txt_from_box.Replace("]", ""); // removing ]
                   txt_from_box = txt_from_box.ToLower();
                   int index_start = Globals.pck_thread.pck_window.ret_select();
                   index_start++;
                   if (index_start != -1)
                   {
                       if (tmp_dat.type == 1) // search clients packets
                       {
                           if (Globals.pck_thread.filter_wind_pck == true)
                           {
                               int temp_id = 0;
                               for (int i = index_start; i < Globals.pck_thread.filtered_pck.Count; i++)
                               {
                                   if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].type == 1)
                                   {
                                       if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer[0] < 0xd0)
                                       {
                                           temp_id = Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer[0];
                                       }
                                       else
                                       {
                                           if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer.Length > 3)
                                           {
                                               for (int j = 0; j < 3; j++)
                                               {
                                                   temp_id += Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer[j];
                                               }
                                           }
                                       }

                                       string tmp_name = new_get_client_pck_name(temp_id).ToLower();
                                       if (tmp_name.Contains(tmp_dat.time))
                                       {
                                           Globals.pck_thread.pck_window.selectitem(i);
                                           return;
                                       }
                                       /*
                                       if (tmp_dat.time.Length < tmp_name.Length)
                                       {
                                           int par_count = tmp_dat.time.Length;
                                           for (int j = 0; j < tmp_dat.time.Length; j++)
                                           {
                                               if (tmp_dat.time[j] == tmp_name[j])
                                               {
                                                   par_count--;
                                               }
                                           }
                                           if (par_count == 0)
                                           {
                                               Globals.pck_thread.pck_window.selectitem(i);
                                               return;
                                           }
                                       }
                                        * 
                                        */
                                   }
                               }
                           }
                           else
                           {
                               int temp_id = 0;
                               for (int i = index_start; i < Globals.pck_thread.pck_record.Count; i++)
                               {
                                   if (Globals.pck_thread.pck_record[i].type == 1)
                                   {
                                       if (Globals.pck_thread.pck_record[i].bytebuffer[0] < 0xd0)
                                       {
                                           temp_id = Globals.pck_thread.pck_record[i].bytebuffer[0];
                                       }
                                       else
                                       {
                                           if (Globals.pck_thread.pck_record[i].bytebuffer.Length > 3)
                                           {
                                               for (int j = 0; j < 3; j++)
                                               {
                                                   temp_id += Globals.pck_thread.pck_record[i].bytebuffer[j];
                                               }
                                           }
                                       }
                                       string tmp_name = new_get_client_pck_name(temp_id).ToLower();
                                       if (tmp_name.Contains(tmp_dat.time))
                                       {
                                           Globals.pck_thread.pck_window.selectitem(i);
                                           return;
                                       }
                                       /*
                                       if (tmp_dat.time.Length < tmp_name.Length)
                                       {
                                           int par_count = tmp_dat.time.Length;
                                           for (int j = 0; j < tmp_dat.time.Length; j++)
                                           {
                                               if (tmp_dat.time[j] == tmp_name[j])
                                               {
                                                   par_count--;
                                               }
                                           }
                                           if (par_count == 0)
                                           {
                                               Globals.pck_thread.pck_window.selectitem(i);
                                               return;
                                           }
                                       }
                                        */

                                   }// if cli
                               }//for
                               // string tmp_name= new_get_client_pck_name();
                           }
                       }
                       else//server
                       {
                           if (Globals.pck_thread.filter_wind_pck == true)
                           {
                               int temp_id = 0;
                               for (int i = index_start; i < Globals.pck_thread.filtered_pck.Count; i++)
                               {
                                   if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].type == 2)
                                   {
                                       if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer[0] < 0xfe)
                                       {
                                           temp_id = Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer[0];
                                       }
                                       else
                                       {
                                           if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer.Length > 3)
                                           {
                                               for (int j = 0; j < 3; j++)
                                               {
                                                   temp_id += Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer[j];
                                               }
                                           }
                                       }

                                       string tmp_name = new_get_server_pck_name(temp_id).ToLower();
                                       if (tmp_name.Contains(tmp_dat.time))
                                       {
                                           Globals.pck_thread.pck_window.selectitem(i);
                                           return;
                                       }
                                       /*
                                       if (tmp_dat.time.Length < tmp_name.Length)
                                       {
                                           int par_count = tmp_dat.time.Length;
                                           for (int j = 0; j < tmp_dat.time.Length; j++)
                                           {
                                               if (tmp_dat.time[j] == tmp_name[j])
                                               {
                                                   par_count--;
                                               }
                                           }
                                           if (par_count == 0)
                                           {
                                               Globals.pck_thread.pck_window.selectitem(i);
                                               return;
                                           }
                                       }
                                        */
                                   }
                               }

                           }
                           else
                           {
                               int temp_id = 0;
                               for (int i = index_start; i < Globals.pck_thread.pck_record.Count; i++)
                               {
                                   if (Globals.pck_thread.pck_record[i].type == 2)
                                   {
                                       if (Globals.pck_thread.pck_record[i].bytebuffer[0] < 0xfe)
                                       {
                                           temp_id = Globals.pck_thread.pck_record[i].bytebuffer[0];
                                       }
                                       else
                                       {
                                           if (Globals.pck_thread.pck_record[i].bytebuffer.Length > 3)
                                           {
                                               for (int j = 0; j < 3; j++)
                                               {
                                                   temp_id += Globals.pck_thread.pck_record[i].bytebuffer[j];
                                               }
                                           }
                                       }
                                       string tmp_name = new_get_server_pck_name(temp_id).ToLower();
                                       if (tmp_name.Contains(tmp_dat.time))
                                       {
                                           Globals.pck_thread.pck_window.selectitem(i);
                                           return;
                                       }
                                       /*
                                       if (tmp_dat.time.Length < tmp_name.Length)
                                       {
                                           int par_count = tmp_dat.time.Length;
                                           for (int j = 0; j < tmp_dat.time.Length; j++)
                                           {
                                               if (tmp_dat.time[j] == tmp_name[j])
                                               {
                                                   par_count--;
                                               }
                                           }
                                           if (par_count == 0)
                                           {
                                               Globals.pck_thread.pck_window.selectitem(i);
                                               return;
                                           }
                                       }
                                        */
                                   }
                               }
                           }
                       }// type

                   }
               }
               catch
               {
                   // probably from string operations ....
               }
           }
           private void search_up_by_name(pck_window_dat tmp_dat)
           {
               try
               {
                   string txt_from_box = "";
                   txt_from_box = tmp_dat.time.Replace(" ", ""); // removing spaces
                   txt_from_box = txt_from_box.Replace(":", ""); // removing :
                   txt_from_box = txt_from_box.Replace("[", ""); // removing [
                   txt_from_box = txt_from_box.Replace("]", ""); // removing ]
                   txt_from_box = txt_from_box.ToLower();
                   int index_start = Globals.pck_thread.pck_window.ret_select();
                   index_start--;
                   if (index_start != -1)
                   {
                       if (tmp_dat.type == 1) // search clients packets
                       {
                           if (Globals.pck_thread.filter_wind_pck == true)
                           {
                               int temp_id = 0;
                               for (int i = index_start; i >= 0; i--)
                               {
                                   if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].type == 1)
                                   {
                                       if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer[0] < 0xd0)
                                       {
                                           temp_id = Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer[0];
                                       }
                                       else
                                       {
                                           if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer.Length > 3)
                                           {
                                               for (int j = 0; j < 3; j++)
                                               {
                                                   temp_id += Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer[j];
                                               }
                                           }
                                       }

                                       string tmp_name = new_get_client_pck_name(temp_id).ToLower();
                                       if (tmp_name.Contains(tmp_dat.time))
                                       {
                                           Globals.pck_thread.pck_window.selectitem(i);
                                           return;
                                       }
                                       /*
                                       if (tmp_dat.time.Length < tmp_name.Length)
                                       {
                                           int par_count = tmp_dat.time.Length;
                                           for (int j = 0; j < tmp_dat.time.Length; j++)
                                           {
                                               if (tmp_dat.time[j] == tmp_name[j])
                                               {
                                                   par_count--;
                                               }
                                           }
                                           if (par_count == 0)
                                           {
                                               Globals.pck_thread.pck_window.selectitem(i);
                                               return;
                                           }
                                       }
                                        */
                                   }
                               }
                           }
                           else
                           {
                               int temp_id = 0;
                               for (int i = index_start; i >= 0; i--)
                               {
                                   if (Globals.pck_thread.pck_record[i].type == 1)
                                   {
                                       if (Globals.pck_thread.pck_record[i].bytebuffer[0] < 0xd0)
                                       {
                                           temp_id = Globals.pck_thread.pck_record[i].bytebuffer[0];
                                       }
                                       else
                                       {
                                           if (Globals.pck_thread.pck_record[i].bytebuffer.Length > 3)
                                           {
                                               for (int j = 0; j < 3; j++)
                                               {
                                                   temp_id += Globals.pck_thread.pck_record[i].bytebuffer[j];
                                               }
                                           }
                                       }
                                       string tmp_name = new_get_client_pck_name(temp_id).ToLower();
                                       if (tmp_name.Contains(tmp_dat.time))
                                       {
                                           Globals.pck_thread.pck_window.selectitem(i);
                                           return;
                                       }
                                       /*
                                       if (tmp_dat.time.Length < tmp_name.Length)
                                       {
                                           int par_count = tmp_dat.time.Length;
                                           for (int j = 0; j < tmp_dat.time.Length; j++)
                                           {
                                               if (tmp_dat.time[j] == tmp_name[j])
                                               {
                                                   par_count--;
                                               }
                                           }
                                           if (par_count == 0)
                                           {
                                               Globals.pck_thread.pck_window.selectitem(i);
                                               return;
                                           }
                                       }

                                        */
                                   }// if cli
                               }//for
                               // string tmp_name= new_get_client_pck_name();
                           }
                       }
                       else//server
                       {
                           if (Globals.pck_thread.filter_wind_pck == true)
                           {
                               int temp_id = 0;
                               for (int i = index_start; i >= 0; i--)
                               {
                                   if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].type == 2)
                                   {
                                       if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer[0] < 0xfe)
                                       {
                                           temp_id = Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer[0];
                                       }
                                       else
                                       {
                                           if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer.Length > 3)
                                           {
                                               for (int j = 0; j < 3; j++)
                                               {
                                                   temp_id += Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]].bytebuffer[j];
                                               }
                                           }
                                       }

                                       string tmp_name = new_get_server_pck_name(temp_id).ToLower();
                                       if (tmp_name.Contains(tmp_dat.time))
                                       {
                                           Globals.pck_thread.pck_window.selectitem(i);
                                           return;
                                       }
                                       /*
                                       if (tmp_dat.time.Length < tmp_name.Length)
                                       {
                                           int par_count = tmp_dat.time.Length;
                                           for (int j = 0; j < tmp_dat.time.Length; j++)
                                           {
                                               if (tmp_dat.time[j] == tmp_name[j])
                                               {
                                                   par_count--;
                                               }
                                           }
                                           if (par_count == 0)
                                           {
                                               Globals.pck_thread.pck_window.selectitem(i);
                                               return;
                                           }
                                       }
                                        */
                                   }
                               }

                           }
                           else
                           {
                               int temp_id = 0;
                               for (int i = index_start; i >= 0; i--)
                               {
                                   if (Globals.pck_thread.pck_record[i].type == 2)
                                   {
                                       if (Globals.pck_thread.pck_record[i].bytebuffer[0] < 0xfe)
                                       {
                                           temp_id = Globals.pck_thread.pck_record[i].bytebuffer[0];
                                       }
                                       else
                                       {
                                           if (Globals.pck_thread.pck_record[i].bytebuffer.Length > 3)
                                           {
                                               for (int j = 0; j < 3; j++)
                                               {
                                                   temp_id += Globals.pck_thread.pck_record[i].bytebuffer[j];
                                               }
                                           }
                                       }
                                       string tmp_name = new_get_server_pck_name(temp_id).ToLower();
                                       if (tmp_name.Contains(tmp_dat.time))
                                       {
                                           Globals.pck_thread.pck_window.selectitem(i);
                                           return;
                                       }
                                       /*
                                       if (tmp_dat.time.Length < tmp_name.Length)
                                       {
                                           int par_count = tmp_dat.time.Length;
                                           for (int j = 0; j < tmp_dat.time.Length; j++)
                                           {
                                               if (tmp_dat.time[j] == tmp_name[j])
                                               {
                                                   par_count--;
                                               }
                                           }
                                           if (par_count == 0)
                                           {
                                               Globals.pck_thread.pck_window.selectitem(i);
                                               return;
                                           }
                                       }
                                        */
                                   }
                               }
                           }
                       }// type

                   }
               }
               catch
               {
                   // qwert
               }
           }
           public void listview_updater()
           {
               System.DateTime time_check2;
               time_check2 = System.DateTime.Now;
               if (time_check2 > time_check1)
               {
                   if (Globals.pck_thread.temp_cache_for_pck_window.Count > 0)
                   {
                       Globals.pck_thread.pck_window.add_range_from_cache();
                   }
                   time_check1 = System.DateTime.Now.AddSeconds(1);
               }
           }
    }//class end
}//namespace
/*
                     Globals.pck_thread.pck_record.Add(Globals.pck_thread.mine_queue.First());
                      if (Globals.pck_thread.filter_wind_pck == true)
                      {
                          if (Globals.pck_thread.mine_queue.First().type == 1)// client pck
                          {
                              if (check_client_pck_in_filter(Globals.pck_thread.mine_queue.First()))
                              {
                                  if (Globals.pck_thread.white_list_filter)
                                  {
                                      if (!Globals.pck_thread.hide_cli_pck)// hide client pck
                                      {
                                          int temp_count = Globals.pck_thread.pck_record.Count - 1;
                                          Globals.pck_thread.filtered_pck.Add(temp_count);
                                          //Globals.pck_thread.pck_window.add_one_to_cache();
                                          Globals.pck_thread.pck_window.add_to_list(Globals.pck_thread.mine_queue.First());
                                      }
                                  }
                                  //Globals.pck_thread.pck_window.add_to_list(Globals.pck_thread.mine_queue.First()); // add to normal list
                              }
                              else// not in filter list ...
                              {
                                  if (!Globals.pck_thread.white_list_filter)
                                  {
                                      if (!Globals.pck_thread.hide_cli_pck)// hide client pck
                                      {
                                          int temp_count = Globals.pck_thread.pck_record.Count - 1;
                                          Globals.pck_thread.filtered_pck.Add(temp_count);
                                          //Globals.pck_thread.pck_window.add_one_to_cache();
                                          Globals.pck_thread.pck_window.add_to_list(Globals.pck_thread.mine_queue.First());
                                      }
                                  }
                              }
                              // spr w liscie filtra
                              // ex packi tez
                              // jak niema to dodanie do filtered
                              // dodanei do okna
                          }
                          else // server pck
                          {
                              if (check_Serwer_pck_in_filter(Globals.pck_thread.mine_queue.First()))
                              {

                                  if (Globals.pck_thread.white_list_filter)
                                  {
                                      if (!Globals.pck_thread.hide_srv_pck)// hide client pck
                                      {
                                          int temp_count = Globals.pck_thread.pck_record.Count - 1;
                                          Globals.pck_thread.filtered_pck.Add(temp_count);
                                          //Globals.pck_thread.pck_window.add_one_to_cache();
                                          Globals.pck_thread.pck_window.add_to_list(Globals.pck_thread.mine_queue.First());
                                      }
                                  }
                                   //Globals.pck_thread.pck_window.add_to_list(Globals.pck_thread.mine_queue.First()); // add to normal list

                              }
                              else// not in filter list ...
                              {
                                  if (!Globals.pck_thread.white_list_filter)
                                  {
                                      if (!Globals.pck_thread.hide_srv_pck)// hide client pck
                                      {
                                          int temp_count = Globals.pck_thread.pck_record.Count - 1;
                                          Globals.pck_thread.filtered_pck.Add(temp_count);
                                          //Globals.pck_thread.pck_window.add_one_to_cache();
                                          Globals.pck_thread.pck_window.add_to_list(Globals.pck_thread.mine_queue.First());
                                      }
                                  }
                              }
                          }
                      }
                      else // without pck filter
                      {
                          //Globals.pck_thread.pck_window.add_one_to_cache();
                          Globals.pck_thread.pck_window.add_to_list(Globals.pck_thread.mine_queue.First());
                      }
 * 
 * ----------------------
 *                             if (Globals.pck_thread.mine_queue.First().action == 1) // add to arraylist + add to window
                  {
                      add_pck(Globals.pck_thread.mine_queue.First());
                  }
                  if (Globals.pck_thread.mine_queue.First().action == 2) // clear list
                  {
                      clean_data();
                  }
                  if (Globals.pck_thread.mine_queue.First().action == 3) //load
                  {
                      Load_data(Globals.pck_thread.mine_queue.First());
                  }
                  if (Globals.pck_thread.mine_queue.First().action == 4) // save
                  {
                      save_data(Globals.pck_thread.mine_queue.First());
                  }
                  if (Globals.pck_thread.mine_queue.First().action == 5) // refresh
                  {
                      //add pack to filtered list ....
                      if (Globals.pck_thread.filter_wind_pck == true)
                      {
                          create_filtered_list();
                      }
                      Globals.pck_thread.pck_window.refresh_window();
                  }
                  if (Globals.pck_thread.mine_queue.First().action == 6) //copy temp filters to normal 1 (apply changes)
                  {
                      apply_filters();
                  }
                  if (Globals.pck_thread.mine_queue.First().action == 7) //save filters
                  {
                      save_filters();
                  }
                  if (Globals.pck_thread.mine_queue.First().action == 8) //load filters
                  {
                      load_filters();
                  }
                  if (Globals.pck_thread.mine_queue.First().action == 9) //search from beg
                  {
                      search_from_beg(Globals.pck_thread.mine_queue.First());
                  }
                  if (Globals.pck_thread.mine_queue.First().action == 10) //search from index up
                  {
                      search_up(Globals.pck_thread.mine_queue.First());
                  }
                  if (Globals.pck_thread.mine_queue.First().action == 11) //search from index down
                  {
                      search_down(Globals.pck_thread.mine_queue.First());
                  }
                  if (Globals.pck_thread.mine_queue.First().action == 12) //search from beg by pck name
                  {
                      search_from_beg_by_name(Globals.pck_thread.mine_queue.First());
                  }
                  if (Globals.pck_thread.mine_queue.First().action == 13) //search down (name)
                  {
                      search_down_by_name(Globals.pck_thread.mine_queue.First());
                  }
                  if (Globals.pck_thread.mine_queue.First().action == 14) //search up (name)
                  {
                      search_up_by_name(Globals.pck_thread.mine_queue.First());
                  }
 * -------------
*/