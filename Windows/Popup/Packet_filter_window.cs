using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace L2_login
{
    public partial class Packet_filter_window : Form
    {
        public Packet_filter_window()
        {
            InitializeComponent();
            refresh_windows();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.TextLength > 0)
                {
                    string txt_from_box = textBox1.Text;
                    txt_from_box = txt_from_box.Replace(" ", ""); // removing spaces
                    txt_from_box = txt_from_box.Replace(":", ""); // removing :
                    if (txt_from_box.Length >= 2 && txt_from_box.Length <= 6)
                    {
                        if (txt_from_box.Length % 2==0)// check for 3 and 5 legh...
                          {
                              int temp_id = 0;
                              byte[] bbuffer_ = new byte[4];
                              byte[] tem_bytebuf = Globals.pck_thread.StringToByteArray2(txt_from_box);
                                for (int i = 0; i < tem_bytebuf.Length; i++)
                                {
                                    bbuffer_[i] = tem_bytebuf[i];
                                    //bytebuf[i] = tem_bytebuf[i];
                                }
                                temp_id = System.BitConverter.ToInt32(bbuffer_, 0);
                                if (check__temp_serv_id_exist(temp_id)== false)
                                {
                                    int temp_cout = listView1.Items.Count;
                                    listView1.Items.Add(txt_from_box);
                                    listView1.Items[temp_cout].SubItems.Add(Globals.pck_thread.new_get_server_pck_name(bbuffer_[0] + bbuffer_[1] + bbuffer_[2]) + bbuffer_[3]);
                                    Globals.pck_thread.tmp_server_pck_filter.Add(temp_id);
                                }

                            /*
                                if (bytebuf[0] != 0xfe) // not ex pck
                                {
                                    //check if we dont have it ...
                                        if (check__temp_serv_id_exist(bytebuf[0]) == false)//check temp ..tmp_server_pck_filter ...
                                        {
                                            int temp_cout = listView1.Items.Count;
                                            listView1.Items.Add(txt_from_box);
                                            listView1.Items[temp_cout].SubItems.Add(Globals.pck_thread.get_srv_pck_name(bytebuf));
                                            Globals.pck_thread.tmp_server_pck_filter.Add(bytebuf[0]);
                                        }
                                }
                                else// ex packet
                                {
                                    int temp_id = System.BitConverter.ToUInt16(bytebuf, 1);
                                    temp_id = temp_id + 0xfe;
                                    //check if we dont have it ...
                                    if (check__temp_serv_id_exist(temp_id) == false)
                                        {
                                            int temp_cout = listView1.Items.Count;
                                            listView1.Items.Add(txt_from_box);
                                            listView1.Items[temp_cout].SubItems.Add(Globals.pck_thread.get_srv_pck_name(bytebuf));
                                            Globals.pck_thread.tmp_server_pck_filter.Add(temp_id);
                                        }
                                }
                            */
                        }
                    }
                }
            }
            catch
            {

            }
            // tab 1 = server pck
            //textBox1
        }
        public bool check_serv_id_exist(int id)
        {
            for (int i = 0; i < Globals.pck_thread.server_pck_filter.Count; i++)
            {
                if (Globals.pck_thread.server_pck_filter[i] == id)
                {
                    return true;
                }
            }
            return false;
        }
        public bool check__temp_serv_id_exist(int id)
        {
            for (int i = 0; i < Globals.pck_thread.tmp_server_pck_filter.Count; i++)
            {
                if (Globals.pck_thread.tmp_server_pck_filter[i] == id)
                {
                    return true;
                }
            }
            return false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                ListView.SelectedIndexCollection indexes = this.listView1.SelectedIndices;
                if (indexes.Count > 0)
                {
                    Globals.pck_thread.tmp_server_pck_filter.RemoveAt(indexes[0]);
                    listView1.Items.RemoveAt(indexes[0]);
                }
            }
            catch
            {
                //pika pika crash ..
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clear Server Filters?", "Pika Pika!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                listView1.Items.Clear();
                Globals.pck_thread.tmp_server_pck_filter.Clear();
            }
        }

        private void button_add_pck_cli_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox_client.TextLength > 0)
                {
                    string txt_from_box = textBox_client.Text;
                    txt_from_box = txt_from_box.Replace(" ", ""); // removing spaces
                    txt_from_box = txt_from_box.Replace(":", ""); // removing :
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
                            if (bytebuf[0] != 0xd0) // not d0 pck
                            {
                                //check if we dont have it ...
                                    if (check__temp_cli_id_exist(bytebuf[0]) == false)//check temp ..tmp_cli_pck_filter ...
                                    {
                                        int temp_cout = listView2.Items.Count;
                                        listView2.Items.Add(txt_from_box);
                                        listView2.Items[temp_cout].SubItems.Add(Globals.pck_thread.new_get_client_pck_name(bytebuf[0]));
                                        Globals.pck_thread.tmp_client_pck_filter.Add(bytebuf[0]);
                                    }
                            }
                            else// d0 packet
                            {
                                int temp_id = System.BitConverter.ToUInt16(bytebuf, 1);
                                temp_id = temp_id + 0xd0;
                                //check if we dont have it ...
                                if (check__temp_cli_id_exist(temp_id) == false)
                                    {
                                        int temp_cout = listView2.Items.Count;
                                        listView2.Items.Add(txt_from_box);
                                        listView2.Items[temp_cout].SubItems.Add(Globals.pck_thread.new_get_client_pck_name(temp_id));
                                        Globals.pck_thread.tmp_client_pck_filter.Add(temp_id);
                                    }
                            }
                        }
                    }
                }
            }
            catch
            {

            }

        }
        public bool check_cli_id_exist(int id)
        {
            for (int i = 0; i < Globals.pck_thread.client_pck_filter.Count; i++)
            {
                if (Globals.pck_thread.client_pck_filter[i] == id)
                {
                    return true;
                }
            }
            return false;
        }
        public bool check__temp_cli_id_exist(int id)
        {
            for (int i = 0; i < Globals.pck_thread.tmp_client_pck_filter.Count; i++)
            {
                if (Globals.pck_thread.tmp_client_pck_filter[i] == id)
                {
                    return true;
                }
            }
            return false;
        }

        private void button_rem_sel_cli_Click(object sender, EventArgs e)
        {
            try
            {
                ListView.SelectedIndexCollection indexes = this.listView2.SelectedIndices;
                if (indexes.Count > 0)
                {
                    Globals.pck_thread.tmp_client_pck_filter.RemoveAt(indexes[0]);
                    listView2.Items.RemoveAt(indexes[0]);
                }
            }
            catch
            {
                //pika pika crash ..
            }
        }

        private void button_clear_client_filter_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Clear Client Filters?", "Pika Pika!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                listView2.Items.Clear();
                Globals.pck_thread.tmp_client_pck_filter.Clear();
            }
        }
        public void refresh_windows()
        {
            listView1.Items.Clear();
            listView2.Items.Clear();
            checkBox1.Checked = Globals.pck_thread.white_list_filter;
            if (Globals.pck_thread.tmp_server_pck_filter.Count > 0)
            {
                byte[] tmp_bytes = new byte[4];
                for (int i = 0; i < Globals.pck_thread.tmp_server_pck_filter.Count; i++)
                {
                    if (Globals.pck_thread.tmp_server_pck_filter[i] < 0xfe)
                    {
                        tmp_bytes[0] = (byte)Globals.pck_thread.tmp_server_pck_filter[i];
                        listView1.Items.Add(tmp_bytes[0].ToString("X2"));
                        listView1.Items[i].SubItems.Add(Globals.pck_thread.new_get_server_pck_name(Globals.pck_thread.tmp_server_pck_filter[i]));
                    }
                    else
                    {
                        int tmp_int = Globals.pck_thread.tmp_server_pck_filter[i];// -0xfe;
                        tmp_bytes = System.BitConverter.GetBytes(tmp_int);
                       // tmp_bytes[2] = tmp_bytes[1];
                       // tmp_bytes[1] = tmp_bytes[0];
                       // tmp_bytes[0] = 0xfe;
                        string tmp_str = tmp_bytes[0].ToString("X2");
                        tmp_str += tmp_bytes[1].ToString("X2");
                        tmp_str += tmp_bytes[2].ToString("X2");
                        listView1.Items.Add(tmp_str);
                        listView1.Items[i].SubItems.Add(Globals.pck_thread.new_get_server_pck_name(tmp_bytes[0] + tmp_bytes[1] + tmp_bytes[2] + tmp_bytes[3]));
                    }

                    /*
                    if (Globals.pck_thread.tmp_server_pck_filter[i] < 0xfe)
                    {
                        tmp_bytes[0] = (byte)Globals.pck_thread.tmp_server_pck_filter[i];
                        listView1.Items.Add(tmp_bytes[0].ToString("X2"));
                        listView1.Items[i].SubItems.Add(Globals.pck_thread.get_srv_pck_name(tmp_bytes));
                    }
                    else
                    {
                        int tmp_int = Globals.pck_thread.tmp_server_pck_filter[i] - 0xfe;
                        tmp_bytes = System.BitConverter.GetBytes(tmp_int);
                        tmp_bytes[2] = tmp_bytes[1];
                        tmp_bytes[1] = tmp_bytes[0];
                        tmp_bytes[0] = 0xfe;
                        string tmp_str = tmp_bytes[0].ToString("X2");
                        tmp_str += tmp_bytes[1].ToString("X2");
                        tmp_str += tmp_bytes[2].ToString("X2");
                        listView1.Items.Add(tmp_str);
                        listView1.Items[i].SubItems.Add(Globals.pck_thread.get_srv_pck_name(tmp_bytes));
                    }
                    */
                }

            }
            if (Globals.pck_thread.tmp_client_pck_filter.Count > 0)
            {
                byte[] tmp_bytes = new byte[4];
                for (int i = 0; i < Globals.pck_thread.tmp_client_pck_filter.Count; i++)
                {
                    if (Globals.pck_thread.tmp_client_pck_filter[i] < 0xd0)
                    {
                        tmp_bytes[0] = (byte)Globals.pck_thread.tmp_client_pck_filter[i];
                        listView2.Items.Add(tmp_bytes[0].ToString("X2"));
                        listView2.Items[i].SubItems.Add(Globals.pck_thread.new_get_client_pck_name(Globals.pck_thread.tmp_client_pck_filter[i]));
                    }
                    else
                    {
                        int tmp_int = Globals.pck_thread.tmp_client_pck_filter[i] - 0xd0;
                        tmp_bytes = System.BitConverter.GetBytes(tmp_int);
                        tmp_bytes[2] = tmp_bytes[1];
                        tmp_bytes[1] = tmp_bytes[0];
                        tmp_bytes[0] = 0xfe;
                        string tmp_str = tmp_bytes[0].ToString("X2");
                        tmp_str += tmp_bytes[1].ToString("X2");
                        tmp_str += tmp_bytes[2].ToString("X2");
                        listView2.Items.Add(tmp_str);
                        listView2.Items[i].SubItems.Add(Globals.pck_thread.new_get_client_pck_name(Globals.pck_thread.tmp_client_pck_filter[i]));
                    }

                    /*
                    if (Globals.pck_thread.tmp_client_pck_filter[i] < 0xd0)
                    {
                        tmp_bytes[0] = (byte)Globals.pck_thread.tmp_client_pck_filter[i];
                        listView2.Items.Add(tmp_bytes[0].ToString("X2"));
                        listView2.Items[i].SubItems.Add(Globals.pck_thread.get_cli_pck_name(tmp_bytes));
                    }
                    else
                    {
                        int tmp_int = Globals.pck_thread.tmp_client_pck_filter[i] - 0xd0;
                        tmp_bytes = System.BitConverter.GetBytes(tmp_int);
                        tmp_bytes[2] = tmp_bytes[1];
                        tmp_bytes[1] = tmp_bytes[0];
                        tmp_bytes[0] = 0xd0;
                        string tmp_str = tmp_bytes[0].ToString("X2");
                        tmp_str += tmp_bytes[1].ToString("X2");
                        tmp_str += tmp_bytes[2].ToString("X2");
                        listView2.Items.Add(tmp_str);
                        listView2.Items[i].SubItems.Add(Globals.pck_thread.get_cli_pck_name(tmp_bytes));
                    }
                    */
                }

            }
            //

        }

        private void button1_Click(object sender, EventArgs e)// aply
        {
            pck_window_dat pck_dat = new pck_window_dat();
            pck_dat.action = 6;
            Globals.pck_thread.mine_queue.Enqueue(pck_dat);
        }

        private void button5_Click(object sender, EventArgs e)//save
        {
            if (MessageBox.Show("Apply and save filters ?", "Pika Pika!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                pck_window_dat pck_dat = new pck_window_dat();
                pck_dat.action = 7;
                Globals.pck_thread.mine_queue.Enqueue(pck_dat);
            }
        }

        private void button6_Click(object sender, EventArgs e)// load ?
        {
            if (MessageBox.Show("Load and apply filters ?", "Pika Pika!", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                pck_window_dat pck_dat = new pck_window_dat();
                pck_dat.action = 8;
                Globals.pck_thread.mine_queue.Enqueue(pck_dat);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

           // Globals.pck_thread.white_list_filter = checkBox1.Checked;
        }

        public bool check_white_filter_state()
        {
            return checkBox1.Checked;
        }
    }// end of class

}
