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
    public partial class converter : Form
    {
        public converter()
        {
            InitializeComponent();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
        public int radio_check()
        {
            if (radioButton1.Checked)
            {
                return 1; // byte
            }
            if (radioButton2.Checked)
            {
                return 2;// int 16
            }
            if (radioButton3.Checked)
            {
                return 3;//int 32
            }
            if (radioButton4.Checked)
            {
                return 4;// int 64
            }
            if (rad_double.Checked)
            {
                return 5; // double
            }
            if (radioButton5.Checked)
            {
                return 6; // string
            }
            return 0;
        }

        private void button2_Click(object sender, EventArgs e) // convert hex to normal
        {
            try
            {
                string text_from_hex = textBox2.Text;
                text_from_hex = text_from_hex.Replace(" ", "");
                string rdy_data;
                if(text_from_hex.Length > 1)
                {
                    rdy_data = con_data_from_hex(radio_check(), text_from_hex);
                    textBox1.Text = rdy_data;// put result in textbox

                }
            }

            catch
            {
                // we got pokeball !
            }

        }// end of method
        public string con_data_from_hex(int type,string text)
        {

              switch(type)
               {
                     case 1:
                       return conv_hex_to_byte(text);
                  case 2:
                       return conv_hex_to_int16(text);
                  case 3:
                       return conv_hex_to_int32(text);
                  case 4:
                       return conv_hex_to_int64(text);
                  case 5:
                       return conv_hex_to_double(text);
                  case 6:
                       return conv_hex_to_string(text);
                  default:
                      return "";

                    }

        }
        public string conv_hex_to_byte(string text)
        {
            byte[] data = Globals.pck_thread.StringToByteArray2(text);
            return data[0].ToString();
        }
        public string conv_hex_to_int16(string text)
        {
            byte[] data = Globals.pck_thread.StringToByteArray2(text);
            if (data.Length > 1)
            {
                if (checkBox1.Checked) // unsigned
                {
                    return System.BitConverter.ToUInt16(data, 0).ToString();
                }
                else // signed
                {
                    return System.BitConverter.ToInt16(data, 0).ToString();
                }
            }
            return "";
        }
        public string conv_hex_to_int32(string text)
        {
            byte[] data = Globals.pck_thread.StringToByteArray2(text);
            if (data.Length > 3)
            {
                if (checkBox1.Checked) // unsigned
                {
                    return System.BitConverter.ToUInt32(data, 0).ToString();
                }
                else // signed
                {
                    return System.BitConverter.ToInt32(data, 0).ToString();
                }
            }
            return "";
        }
        public string conv_hex_to_int64(string text)
        {
            byte[] data = Globals.pck_thread.StringToByteArray2(text);
            if (data.Length > 5)
            {
                if (checkBox1.Checked) // unsigned
                {
                    return System.BitConverter.ToUInt64(data, 0).ToString();
                }
                else // signed
                {
                    return System.BitConverter.ToInt64(data, 0).ToString();
                }
            }
            return "";
        }
        public string conv_hex_to_double(string text)
        {
            byte[] data = Globals.pck_thread.StringToByteArray2(text);
            if (data.Length > 5)
            {
                    return System.BitConverter.ToDouble(data, 0).ToString();
            }
            return "";

        }
        public string conv_hex_to_string(string text)
        {
            try
            {
            byte[] data = Globals.pck_thread.StringToByteArray2(text);
            string tmp_data = "";
            for (int i = 0; i < data.Length; i=i+2)
            {
                tmp_data+= (char)data[i];
            }
            return tmp_data;
            }
            catch
            {
                return "";
            }
        }

        private void button1_Click(object sender, EventArgs e) // convert normal to hex
        {
            try
            {
                string text_from_hex = textBox1.Text;
                string rdy_data;
                if (text_from_hex.Length > 0)
                {
                    rdy_data = con_data_from_normal(radio_check(), text_from_hex);
                    textBox2.Text = rdy_data;// put result in textbox

                }
            }

            catch
            {
                // we got pokeball !
            }


        }           
        public string con_data_from_normal(int type,string text)
        {
            switch (type)
            {
                case 1:
                    return conv_byte_to_hex(text);
                case 2:
                     return conv_int16_to_hex(text);
                case 3:
                     return conv_int32_to_hex(text);
                case 4:
                     return conv_int64_to_hex(text);
                case 5:
                     return conv_double_to_hex(text);
                case 6:
                     return conv_string_to_hex(text);
                default:
                    return "";
            }
        }
        public string conv_byte_to_hex(string text)
        {
            int temp= System.Convert.ToByte(text);
            if (temp < 255)
            {
                return temp.ToString("X2");
            }
            else
                return "";
        }
        public string conv_int16_to_hex(string text)
        {
                int temp = System.Convert.ToInt16(text);
                byte[] con = System.BitConverter.GetBytes(temp);
                string oki = con[0].ToString("X2");
                oki += " ";
                oki += con[1].ToString("X2");
                return oki;
         
        }
        public string conv_int32_to_hex(string text)
        {
            try
            {
                int temp = System.Convert.ToInt32(text);
                byte[] con = System.BitConverter.GetBytes(temp);
                string oki = con[0].ToString("X2");
                oki += " ";
                oki += con[1].ToString("X2");
                oki += " ";
                oki += con[2].ToString("X2");
                oki += " ";
                oki += con[3].ToString("X2");
                return oki;
            }
            catch
            { // ugly but im lazy
                uint temp = System.Convert.ToUInt32(text);
                byte[] con = System.BitConverter.GetBytes(temp);
                string oki = con[0].ToString("X2");
                oki += " ";
                oki += con[1].ToString("X2");
                oki += " ";
                oki += con[2].ToString("X2");
                oki += " ";
                oki += con[3].ToString("X2");
                return oki;
            }
        }
        public string conv_int64_to_hex(string text)
        {
            try
            {
                long temp = System.Convert.ToInt64(text);
                byte[] con = System.BitConverter.GetBytes(temp);
                string oki = con[0].ToString("X2");
                oki += " ";
                oki += con[1].ToString("X2");
                oki += " ";
                oki += con[2].ToString("X2");
                oki += " ";
                oki += con[3].ToString("X2");
                oki += " ";
                oki += con[4].ToString("X2");
                oki += " ";
                oki += con[5].ToString("X2");
                oki += " ";
                oki += con[6].ToString("X2");
                oki += " ";
                oki += con[7].ToString("X2");
                return oki;
            }
            catch
            { // ugly but im lazy ...
                ulong temp = System.Convert.ToUInt64(text);
                byte[] con = System.BitConverter.GetBytes(temp);
                string oki = con[0].ToString("X2");
                oki += " ";
                oki += con[1].ToString("X2");
                oki += " ";
                oki += con[2].ToString("X2");
                oki += " ";
                oki += con[3].ToString("X2");
                oki += " ";
                oki += con[4].ToString("X2");
                oki += " ";
                oki += con[5].ToString("X2");
                oki += " ";
                oki += con[6].ToString("X2");
                oki += " ";
                oki += con[7].ToString("X2");
                return oki;
            }
        }
        public string conv_double_to_hex(string text)
        {
            double temp = System.Convert.ToDouble(text);
            byte[] con = System.BitConverter.GetBytes(temp);
            string oki = con[0].ToString("X2");
            oki += " ";
            oki += con[1].ToString("X2");
            oki += " ";
            oki += con[2].ToString("X2");
            oki += " ";
            oki += con[3].ToString("X2");
            oki += " ";
            oki += con[4].ToString("X2");
            oki += " ";
            oki += con[5].ToString("X2");
            oki += " ";
            oki += con[6].ToString("X2");
            oki += " ";
            oki += con[7].ToString("X2");
            return oki;
        }
        public string conv_string_to_hex(string text)
        {
            byte[] data = System.Text.Encoding.Unicode.GetBytes(text);
            string ret_str = "";
            for (int i = 0; i < data.Length; i++)
            {
                ret_str += data[i].ToString("X2");
                ret_str += " ";
            }
            return ret_str;
        }
    }
}
//int temp_id = System.BitConverter.ToUInt16(bbufer, 1);