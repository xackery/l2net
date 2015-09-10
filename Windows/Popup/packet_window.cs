using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;//do wyj
namespace L2_login
{
    public partial class packet_window : Form
    {
        public System.Windows.Forms.ListViewItem[] cache_lictview_items;//= new System.Windows.Forms.ListViewItem[10000];
        //public int max_listview_count = 0;

        public packet_window()
        {
            InitializeComponent();
            this.listView1.DoubleBuffer();
            Globals.pck_thread.load_client_names();
            Globals.pck_thread.load_server_names();
            checkBox_filter.Checked = Globals.pck_thread.filter_wind_pck;
            if (checkBox_filter.Checked)
            {
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
            }
            else
            {
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
            }
            checkBox2.Checked = Globals.pck_thread.hide_cli_pck; // hide client
            checkBox3.Checked = Globals.pck_thread.hide_srv_pck; // hide server
            checkBox4.Checked =Globals.pck_thread.search_pck_names; // search in pck names

            if (Globals.pck_thread.wind_combo_set == true)
            {
                comboBox_srv_cli.SelectedIndex = 1;// client
            }
            else
            {
                comboBox_srv_cli.SelectedIndex = 0; // server
            }

            if (Globals.pck_thread.pck_recording == false)
            {
                button_start_rec.Text = "Start Recording";
            }
            else
            {
                button_start_rec.Text = "Stop Recording";
            }
            pck_window_dat temp_pck = new pck_window_dat();
            temp_pck.action = 5;
            Globals.pck_thread.mine_queue.Enqueue(temp_pck);
            //Globals.pck_thread.mine_queue.Enqueue(temp_pck);
            if (Globals.pck_thread.folow_new_pck)// folow pck
            {
                checkBox1.Checked = true;
            }
            else
            {
                checkBox1.Checked = false;
            }
            checkBox5.Checked = Globals.pck_thread.save_visable; // save visable
            this.Text = "Packet Window (" + Globals.gamedata.my_char.Name + ")";


            //SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            //this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            // this.UpdateStyles();
        }
  /*      private void listView1_retriveitem(object sender, RetrieveVirtualItemEventArgs e)
        {
            try
            {
                if (e.ItemIndex >= cache_listviev.Count)
                {
                    if (Globals.pck_thread.filter_wind_pck == true)
                    {
                        if (e.ItemIndex >= Globals.pck_thread.filtered_pck.Count)
                        {
                            // wtf here ???
                        }
                        else
                        {
                            #region eeee1
                            e.Item = new ListViewItem();
                            e.Item.Text = e.ItemIndex.ToString(); // index
                            if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[e.ItemIndex]].type == 1) // client
                            {
                                e.Item.SubItems.Add("Cli");
                            }
                            else //srv
                            {
                                e.Item.SubItems.Add("Srv");
                            }
                            e.Item.SubItems.Add(Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[e.ItemIndex]].time);
                            e.Item.SubItems.Add(Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[e.ItemIndex]].bytebuffer.Length.ToString());
                            if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[e.ItemIndex]].type == 1) // client
                            {
                                e.Item.SubItems.Add(Globals.pck_thread.get_cli_pck_name(Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[e.ItemIndex]].bytebuffer));
                                string pck_id;
                                if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[e.ItemIndex]].bytebuffer[0] == 0xd0)
                                {
                                    pck_id = Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[e.ItemIndex]].bytebuffer[0].ToString("X2");
                                    pck_id += ":";
                                    pck_id += Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[e.ItemIndex]].bytebuffer[1].ToString("X2");
                                    pck_id += Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[e.ItemIndex]].bytebuffer[2].ToString("X2");
                                }
                                else
                                {
                                    pck_id = Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[e.ItemIndex]].bytebuffer[0].ToString("X2");
                                }
                                e.Item.SubItems.Add(pck_id);
                                e.Item.BackColor = System.Drawing.Color.FromArgb(255, 250, 168);
                                //
                            }
                            else//srv
                            {
                                e.Item.SubItems.Add(Globals.pck_thread.get_srv_pck_name(Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[e.ItemIndex]].bytebuffer));
                                string pck_id;
                                if (Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[e.ItemIndex]].bytebuffer[0] == 0xfe)
                                {
                                    pck_id = Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[e.ItemIndex]].bytebuffer[0].ToString("X2");
                                    pck_id += ":";
                                    pck_id += Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[e.ItemIndex]].bytebuffer[1].ToString("X2");
                                    pck_id += Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[e.ItemIndex]].bytebuffer[2].ToString("X2");
                                }
                                else
                                {
                                    pck_id = Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[e.ItemIndex]].bytebuffer[0].ToString("X2");
                                }
                                e.Item.SubItems.Add(pck_id);
                                e.Item.BackColor = System.Drawing.Color.FromArgb(224, 245, 192);
                            }
                            #endregion
                            cache_listviev.Add(e.Item);
                            return;
                        }

                    }
                    else// bez filtra
                    {
                        if (e.ItemIndex >= Globals.pck_thread.pck_record.Count)
                        {
                            // wtf here ???
                        }
                        else
                        {
                            #region eee2
                            //listView1.VirtualListSize++;
                            e.Item = new ListViewItem();
                            e.Item.Text = e.ItemIndex.ToString(); // index
                            if (Globals.pck_thread.pck_record[e.ItemIndex].type == 1) // client
                            {
                                e.Item.SubItems.Add("Cli");
                            }
                            else // srv
                            {
                                e.Item.SubItems.Add("Srv");
                            }
                            e.Item.SubItems.Add(Globals.pck_thread.pck_record[e.ItemIndex].time);
                            e.Item.SubItems.Add(Globals.pck_thread.pck_record[e.ItemIndex].bytebuffer.Length.ToString());
                            if (Globals.pck_thread.pck_record[e.ItemIndex].type == 1) // client
                            {
                                e.Item.SubItems.Add(Globals.pck_thread.get_cli_pck_name(Globals.pck_thread.pck_record[e.ItemIndex].bytebuffer));
                                string pck_id;
                                if (Globals.pck_thread.pck_record[e.ItemIndex].bytebuffer[0] == 0xd0)
                                {
                                    pck_id = Globals.pck_thread.pck_record[e.ItemIndex].bytebuffer[0].ToString("X2");
                                    pck_id += ":";
                                    pck_id += Globals.pck_thread.pck_record[e.ItemIndex].bytebuffer[1].ToString("X2");
                                    pck_id += Globals.pck_thread.pck_record[e.ItemIndex].bytebuffer[2].ToString("X2");
                                }
                                else
                                {
                                    pck_id = Globals.pck_thread.pck_record[e.ItemIndex].bytebuffer[0].ToString("X2");
                                }
                                e.Item.SubItems.Add(pck_id);
                                e.Item.BackColor = System.Drawing.Color.FromArgb(255, 250, 168);
                                //
                            }
                            else//srv
                            {
                                e.Item.SubItems.Add(Globals.pck_thread.get_srv_pck_name(Globals.pck_thread.pck_record[e.ItemIndex].bytebuffer));
                                string pck_id;
                                if (Globals.pck_thread.pck_record[e.ItemIndex].bytebuffer[0] == 0xfe)
                                {
                                    pck_id = Globals.pck_thread.pck_record[e.ItemIndex].bytebuffer[0].ToString("X2");
                                    pck_id += ":";
                                    pck_id += Globals.pck_thread.pck_record[e.ItemIndex].bytebuffer[1].ToString("X2");
                                    pck_id += Globals.pck_thread.pck_record[e.ItemIndex].bytebuffer[2].ToString("X2");
                                }
                                else
                                {
                                    pck_id = Globals.pck_thread.pck_record[e.ItemIndex].bytebuffer[0].ToString("X2");
                                }
                                e.Item.SubItems.Add(pck_id);
                                e.Item.BackColor = System.Drawing.Color.FromArgb(224, 245, 192);

                            }
                            #endregion
                            cache_listviev.Add(e.Item);
                            return;
                        }
                    }
                }
                else
                {
                    e.Item = cache_listviev[e.ItemIndex];
                }

            }
            catch
            {
                e.Item = new ListViewItem();
                e.Item.SubItems.Add("excep");
                e.Item.SubItems.Add("excep");
                e.Item.SubItems.Add("excep");
                e.Item.SubItems.Add("excep");
                e.Item.SubItems.Add("excep");
                e.Item.SubItems.Add("excep");
                // oki
            }


        }
   */
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = this.listView1.SelectedIndices;
            if (indexes.Count > 0)
            {
                if (indexes[0] < Globals.pck_thread.pck_record.Count)
                {
                    string temp_string = "time:";
                    if (Globals.pck_thread.filter_wind_pck == true)
                    {
                        temp_string += Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[indexes[0]]].time;// index from listviev -> check the value at that index in filtered -> tahe the int val as index in real array
                        temp_string += "\r\n";
                        temp_string += "size:";
                        temp_string += Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[indexes[0]]].bytebuffer.Length.ToString();
                        temp_string += "\r\n";
                        for (uint i = 0; i < Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[indexes[0]]].bytebuffer.Length; i++)
                        {
                            temp_string += Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[indexes[0]]].bytebuffer[i].ToString("X2"); ;
                            temp_string += " ";
                        }
                    }
                    else
                    {

                        temp_string += Globals.pck_thread.pck_record[indexes[0]].time;
                        temp_string += "\r\n";
                        temp_string += "size:";
                        temp_string += Globals.pck_thread.pck_record[indexes[0]].bytebuffer.Length.ToString();
                        temp_string += "\r\n";
                        for (uint i = 0; i < Globals.pck_thread.pck_record[indexes[0]].bytebuffer.Length; i++)
                        {
                            temp_string += Globals.pck_thread.pck_record[indexes[0]].bytebuffer[i].ToString("X2"); ;
                            temp_string += " ";
                        }

                    }
                    textBox2.Text = temp_string;
                }
            }
            // textBox2.Text = Globals.pck_thread.pck_record
            //sender.
            //textBox2
        }

        private void button_start_rec_Click(object sender, EventArgs e)
        {
            if (Globals.pck_thread.pck_recording == true)
            {
                button_start_rec.Text = "Start Recording";
                Globals.pck_thread.pck_recording = false;
            }
            else
            {
                button_start_rec.Text = "Stop Recording";
                Globals.pck_thread.pck_recording = true;
            }


        }

        private void button_clear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clear Packet Log?", "Pika Pika!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                pck_window_dat pck_dat = new pck_window_dat();
                pck_dat.action = 2;
                Globals.pck_thread.mine_queue.Enqueue(pck_dat);
            }
        }
        public void refresh_window()
        {
            /* clear_cache();
             if(Globals.pck_thread.filter_wind_pck == true)
             {
                 set_virtual_item(Globals.pck_thread.filtered_pck.Count);
             }
             else
             {
                 set_virtual_item(Globals.pck_thread.pck_record.Count);
              }
             */
            listView1.BeginUpdate();
           Globals.pck_thread.temp_cache_for_pck_window.Clear();
            if (Globals.pck_thread.filter_wind_pck == true)
            {
                if (Globals.pck_thread.filtered_pck.Count == 0)
                {
                    listView1.Items.Clear();
                }
                else
                {
                    listView1.Items.Clear();
                    for (int i = 0; i < Globals.pck_thread.filtered_pck.Count; i++)
                    {
                        Globals.pck_thread.pck_window.add_one_to_temp_cache(Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]]);
                        //Globals.pck_thread.pck_window.add_to_list(Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[i]]);
                    }
                }
            }
            else // filter pck is off
            {
                if (Globals.pck_thread.pck_record.Count == 0)
                {
                    listView1.Items.Clear();
                    //listView1.Refresh();
                }
                else
                {
                    listView1.Items.Clear();
                    for (int i = 0; i < Globals.pck_thread.pck_record.Count; i++)
                    {
                        Globals.pck_thread.pck_window.add_one_to_temp_cache(Globals.pck_thread.pck_record[i]);
                        //Globals.pck_thread.pck_window.add_to_list(Globals.pck_thread.pck_record[i]);
                    }
                }
            }
            listView1.EndUpdate();

        }

        private void button_Save_list_Click(object sender, EventArgs e)
        {
            //DialogResult sav_win_res;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Packet log(*.lpl)|*.lpl";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.RestoreDirectory = true;
            saveFileDialog1.InitialDirectory = Globals.PATH + "\\Packet Logs";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pck_window_dat temp_pck = new pck_window_dat();
                temp_pck.action = 4;
                temp_pck.time = saveFileDialog1.FileName; // same type so i dont see problem here :P
                Globals.pck_thread.mine_queue.Enqueue(temp_pck);
            }

        }

        private void button_Load_list_Click(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //openFileDialog1.InitialDirectory = "c:\\" ;
            openFileDialog1.Title = "Load Packet log";
            openFileDialog1.Filter = "Packet log (*.lpl)|*.lpl";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.InitialDirectory = Globals.PATH + "\\Packet Logs";
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //FileStream fs = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                pck_window_dat temp_pck = new pck_window_dat();
                temp_pck.action = 3;
                temp_pck.time = openFileDialog1.FileName; // same type so i dont see problem here :P
                Globals.pck_thread.mine_queue.Enqueue(temp_pck);
            }
        }

        private void comboBox_srv_cli_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_srv_cli.SelectedIndex == 0)
            {
                Globals.pck_thread.wind_combo_set = false;
            }
            else
            {
                Globals.pck_thread.wind_combo_set = true;
            }
        }

        private void button_inject_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.TextLength > 0)
                {
                    string temp_proc_txt = textBox1.Text;
                    string proctext = temp_proc_txt.Replace(" ", "");
                    if ((proctext.Length % 2) == 0)
                    {
                        if (comboBox_srv_cli.SelectedIndex == 0)// send to server
                        {

                            byte[] bytearre = Globals.pck_thread.StringToByteArray2(proctext);
                            ByteBuffer bytebuff = new ByteBuffer(bytearre);
                            Globals.gamedata.SendToGameServerInject(bytebuff);
                            
                        }
                        else // send to client
                        {
                            byte[] bytearre = Globals.pck_thread.StringToByteArray2(proctext);
                            ByteBuffer bytebuff = new ByteBuffer(bytearre);
                            Globals.gamedata.SendToClient(bytebuff);
                        }
                    }
                }
            }
            catch
            {
                // string legh == 0 ...
            }
        }



        private void checkBox_filter_CheckedChanged(object sender, EventArgs e)
        {
            Globals.pck_thread.filter_wind_pck = checkBox_filter.Checked;
            pck_window_dat temp_pck = new pck_window_dat();
            temp_pck.action = 5;
            if (checkBox_filter.Checked)
            {
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
            }
            else
            {
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
            }
            Globals.pck_thread.mine_queue.Enqueue(temp_pck);
        }

        private void button_filter_Click(object sender, EventArgs e)
        {
            if (Globals.pck_thread.pck_fill_window == null || Globals.pck_thread.pck_fill_window.IsDisposed == true)
            {
                Globals.pck_thread.pck_fill_window = new Packet_filter_window();
            }
            Globals.pck_thread.pck_fill_window.BringToFront();
            Globals.pck_thread.pck_fill_window.Show();
            // refresh window - load array filter ...
        }

        private void button1_Click(object sender, EventArgs e)// converter buttom
        {
            if (Globals.pck_thread.conv_obj == null || Globals.pck_thread.conv_obj.IsDisposed == true)
            {
                Globals.pck_thread.conv_obj = new converter();
            }
            Globals.pck_thread.conv_obj.BringToFront();
            Globals.pck_thread.conv_obj.Show();
        }

        private void button4_Click(object sender, EventArgs e)//search buttom
        {
            if (textBox3.TextLength > 0)
            {
                if (radioButton1.Checked == true) // client
                {
                    if (checkBox4.Checked)
                    {
                        pck_window_dat temp_pck = new pck_window_dat();
                        temp_pck.time = textBox3.Text;
                        temp_pck.type = 1;
                        temp_pck.action = 12;
                        Globals.pck_thread.mine_queue.Enqueue(temp_pck);
                    }
                    else
                    {
                        pck_window_dat temp_pck = new pck_window_dat();
                        temp_pck.time = textBox3.Text;
                        temp_pck.type = 1;
                        temp_pck.action = 9;
                        Globals.pck_thread.mine_queue.Enqueue(temp_pck);
                    }
                }
                else if (radioButton2.Checked == true) // server
                {
                    if (checkBox4.Checked)
                    {
                        pck_window_dat temp_pck = new pck_window_dat();
                        temp_pck.time = textBox3.Text;
                        temp_pck.type = 2;
                        temp_pck.action = 12;
                        Globals.pck_thread.mine_queue.Enqueue(temp_pck);
                    }
                    else
                    {
                        pck_window_dat temp_pck = new pck_window_dat();
                        temp_pck.time = textBox3.Text;
                        temp_pck.type = 2;
                        temp_pck.action = 9;
                        Globals.pck_thread.mine_queue.Enqueue(temp_pck);
                    }
                }
            }
        }
        public void selectitem(int index)//listviev select
        {
            listView1.Focus();
            listView1.Items[index].Selected = true;
            listView1.Items[index].EnsureVisible();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.TextLength > 0)
            {
                if (radioButton1.Checked == true) // client
                {
                    if (checkBox4.Checked)
                    {
                        pck_window_dat temp_pck = new pck_window_dat();
                        temp_pck.time = textBox3.Text;
                        temp_pck.type = 1;
                        temp_pck.action = 13;
                        Globals.pck_thread.mine_queue.Enqueue(temp_pck);
                    }
                    else
                    {
                        pck_window_dat temp_pck = new pck_window_dat();
                        temp_pck.time = textBox3.Text;
                        temp_pck.type = 1;
                        temp_pck.action = 11;
                        Globals.pck_thread.mine_queue.Enqueue(temp_pck);
                    }
                }
                else if (radioButton2.Checked == true) // server
                {
                    if (checkBox4.Checked)
                    {
                        pck_window_dat temp_pck = new pck_window_dat();
                        temp_pck.time = textBox3.Text;
                        temp_pck.type = 2;
                        temp_pck.action = 13;
                        Globals.pck_thread.mine_queue.Enqueue(temp_pck);
                    }
                    else
                    {
                        pck_window_dat temp_pck = new pck_window_dat();
                        temp_pck.time = textBox3.Text;
                        temp_pck.type = 2;
                        temp_pck.action = 11;
                        Globals.pck_thread.mine_queue.Enqueue(temp_pck);
                    }
                }
            }
        } // search down
        public int ret_select()
        {
            ListView.SelectedIndexCollection indexes = this.listView1.SelectedIndices;
            if (indexes.Count > 0)
            {
                return indexes[0];
            }
            else
            {
                return -1;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.TextLength > 0)
            {
                if (radioButton1.Checked == true) // client
                {
                    if (checkBox4.Checked)
                    {
                        pck_window_dat temp_pck = new pck_window_dat();
                        temp_pck.time = textBox3.Text;
                        temp_pck.type = 1;
                        temp_pck.action = 14;
                        Globals.pck_thread.mine_queue.Enqueue(temp_pck);
                    }
                    else
                    {
                        pck_window_dat temp_pck = new pck_window_dat();
                        temp_pck.time = textBox3.Text;
                        temp_pck.type = 1;
                        temp_pck.action = 10;
                        Globals.pck_thread.mine_queue.Enqueue(temp_pck);
                    }
                }
                else if (radioButton2.Checked == true) // server
                {
                    if (checkBox4.Checked)
                    {
                        pck_window_dat temp_pck = new pck_window_dat();
                        temp_pck.time = textBox3.Text;
                        temp_pck.type = 2;
                        temp_pck.action = 14;
                        Globals.pck_thread.mine_queue.Enqueue(temp_pck);
                    }
                    else
                    {
                        pck_window_dat temp_pck = new pck_window_dat();
                        temp_pck.time = textBox3.Text;
                        temp_pck.type = 2;
                        temp_pck.action = 10;
                        Globals.pck_thread.mine_queue.Enqueue(temp_pck);
                    }
                }

            }
        } // serarch up

        private void checkBox1_CheckedChanged(object sender, EventArgs e) // folow new pck
        {
            if(checkBox1.Checked)
            {
                Globals.pck_thread.folow_new_pck = true;
            }
            else
            {
                Globals.pck_thread.folow_new_pck = false;
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            Globals.pck_thread.hide_cli_pck = checkBox2.Checked;
            pck_window_dat temp_pck = new pck_window_dat();
            temp_pck.action = 5;
            Globals.pck_thread.mine_queue.Enqueue(temp_pck);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            Globals.pck_thread.hide_srv_pck = checkBox3.Checked;
            pck_window_dat temp_pck = new pck_window_dat();
            temp_pck.action = 5;
            Globals.pck_thread.mine_queue.Enqueue(temp_pck);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
           Globals.pck_thread.search_pck_names = checkBox4.Checked;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            Globals.pck_thread.save_visable = checkBox5.Checked;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string temp_pck = " ";
            if (checkBox6.Checked) // parse edit
            {
                temp_pck = textBox2.Text;
            }
            else
            {
                ListView.SelectedIndexCollection indexes = this.listView1.SelectedIndices;
                
                if (indexes.Count > 0)
                {
                    if (indexes[0] < Globals.pck_thread.pck_record.Count)
                    {
                        if (Globals.pck_thread.filter_wind_pck == true)
                        {
                            for (uint i = 0; i < Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[indexes[0]]].bytebuffer.Length; i++)
                            {
                                temp_pck += Globals.pck_thread.pck_record[Globals.pck_thread.filtered_pck[indexes[0]]].bytebuffer[i].ToString("X2"); ;
                                temp_pck += " ";
                            }
                        }
                        else
                        {
                            for (uint i = 0; i < Globals.pck_thread.pck_record[indexes[0]].bytebuffer.Length; i++)
                            {
                                temp_pck += Globals.pck_thread.pck_record[indexes[0]].bytebuffer[i].ToString("X2"); ;
                                temp_pck += " ";
                            }
                        }
                    }
                }
            }
            if(parse_codebox.TextLength > 0)
            {
                if (temp_pck.Length > 0)
                {
                    pck_parser tmp_data = new pck_parser(parse_codebox.Text, temp_pck);

                    if (tmp_data.code_check())
                    {
                        if (tmp_data.pck_check())
                        {
                            textBox2.Text =  tmp_data.parse();
                        }
                    }
                    //if (tmp_data.error_check())
                    //
                        error_label.Text = tmp_data.error_string();
                       // MessageBox.Show("dupa error.", "Pika Pika!", MessageBoxButtons.YesNo);
                    //}
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            help_parser new_win = new help_parser();
            new_win.BringToFront();
            new_win.Show();
        }

      

        
       // public void Close()
       // {
        //    MessageBox.Show("close wind?", "Pika Pika!", MessageBoxButtons.YesNo);
       // }

    }//end class
}