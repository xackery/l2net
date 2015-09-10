using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace L2_login
{
    class pck_parser
    {
        private string parse_code;
        private string pck_string;
        public pck_parser(string tmp_code, string parse_pck)
        {
            parse_code = tmp_code;
            pck_string = parse_pck;
        }
        private string allowed_chars1 = "1234567890abcdefABCDEF ";
        private string allowed_chars2 = "()1234567890abcdefABCDEF ";
        private int error_code = 0;
        //error_code:
        /*  1 - wrong char in parse code
            2 - wrong char in pck_string
        */
        public bool code_check()
                {
                    error_code = 0;
                    bool check_ok = false;
                    for (int i = 0; i < parse_code.Length; i++)
                    {
                        for (int j = 0; j < allowed_chars2.Length; j++)
                        {
                            if (parse_code[i] == allowed_chars2[j])
                            {
                                check_ok = true;
                            }
                        }
                        if (check_ok == false)
                        {
                            error_code = 1;
                            return false;
                        }
                    }
                    return true;
                }
        public bool pck_check()
        {
            error_code = 0;
            bool check_ok = false;
            for (int i = 0; i < pck_string.Length; i++)
            {
                for (int j = 0; j < allowed_chars1.Length; j++)
                {
                    if (pck_string[i] == allowed_chars1[j])
                    {
                        check_ok = true;
                    }
                }
                if (check_ok == false)
                {
                    error_code = 2;
                    return false;
                }
                check_ok = false;
            }
            return true;

        }
        public bool error_check()
        {
            if (error_code != 0)
                return true;
            else
                return false;

        }

        public string error_string()
        {
            string tmp_return;
            switch(error_code)
            {
                case 1:
                    tmp_return = "wrong char in parse code";
                    return tmp_return;
                case 2:
                    tmp_return = "wrong char in pck string";
                    return tmp_return;
                case 3:
                    tmp_return = "pck too short";
                    return tmp_return;
                case 4:
                    tmp_return = "bytes left";
                    return tmp_return;
                default:
                    tmp_return = "something bad chapend";
                    return tmp_return;

            }

        }

        public string parse()
        {
            string rdy_data = "";
            string tmp_vars = "";
            int pck_index = 0;

            // help vars
            int copy_var = 0;
            int byte_size = 0;
            
            // loop vars
            bool loop_work = false;
            int loop_count = 0;
            int start_loop_index = 0;
            for (int i = 0; i < parse_code.Length; i++)
            {
                if (parse_code[i] != '(')
                {
                    if (parse_code[i] != ')')
                    {
                        if (loop_work) // true
                        {
                            rdy_data = rdy_data + " --";
                        }

                        byte_size = comp_calc1(pck_index, pck_string, parse_code[i]);
                        if (check_place_for_parse(pck_index, pck_string.Length, byte_size))
                        {
                            for (int j = pck_index; j < pck_string.Length; j++)
                            {
                                tmp_vars = tmp_vars + pck_string[j];
                                //rdy_data = rdy_data + pck_string[j];
                                if (pck_string[j] != ' ')
                                {
                                    copy_var++;
                                }
                                if (copy_var == byte_size)
                                {
                                    if ((j + 1) < pck_string.Length)
                                    {
                                        rdy_data = rdy_data + pck_string[j + 1];
                                    }
                                    pck_index = j + 1;
                                    //pck_index = pck_index + byte_size + 1;
                                    break;
                                }
                            }
                            rdy_data = rdy_data + tmp_vars;
                            rdy_data = rdy_data + "= (";
                            tmp_vars = tmp_vars.Replace(" ", "");
                            tmp_vars = convert(char_to_cod(parse_code[i]), tmp_vars);
                            rdy_data = rdy_data + tmp_vars;
                            rdy_data = rdy_data + ")";


                            if (!loop_work)
                            {
                                if ((i + 1) < parse_code.Length)
                                {
                                    if (parse_code[i + 1] == '(')
                                    {

                                    tmp_vars= tmp_vars.Replace("(","");
                                    tmp_vars = tmp_vars.Replace(")", "");
                                    loop_count = System.Convert.ToInt32(tmp_vars);
                                    if (loop_count > 0)
                                    {
                                        loop_work = true;
                                        start_loop_index = i;

                                    }
                                    else // == 0
                                    {
                                        for (int k = i; k < parse_code.Length; k++)
                                        {
                                            if (parse_code[k] == ')')
                                            {
                                                i = k;
                                            }
                                        }
                                    }
                                    rdy_data = rdy_data + " (loop)";
                                    }
                                }
                            }

                            tmp_vars = "";
                            copy_var = 0;
                            rdy_data = rdy_data + "\r\n";

                        }
                        else //   if (check_place_for_parse(pck_index, pck_string.Length, byte_size))
                        {
                            error_code = 3;
                        }
                    }
                    else // end ')'
                    {
                        if (loop_work)
                        {
                            rdy_data = rdy_data + "-----------------------\r\n";
                            loop_count--;
                            if (loop_count == 0)
                            {
                                loop_work = false;
                            }
                            else
                            {
                                i = start_loop_index;
                            }
                        }
                    }
                }
                /*else
                {
                    if (!loop_work)
                    {
                        if ((i + 1) < parse_code.Length)
                        {
                            if (parse_code[i + 1] == '(')
                            {
                                loop_work = true;
                                loop_count = System.Convert.ToInt32(tmp_vars);
                                start_loop_index = i;
                                rdy_data = rdy_data + " (loop)";
                            }
                        }
                    }
                }*/
            }
            if ((pck_index + 1) != pck_string.Length)
            {
                error_code = 4;
                for (int i = pck_index; i < pck_string.Length; i++)
                {
                    rdy_data = rdy_data + pck_string[i];
                }
            }

            return rdy_data;
        }
        private int char_to_cod(char a)
        {
            switch(a)
            {
                case 'c':
                    return 1;
                case 'd':
                    return 2;
                case 's':
                    return 3;
                case 'h':
                    return 4;
                case 'q':
                    return 5;
                case 'f':
                    return 6;

                default:
                    return 0;
            }

        }
        private bool check_place_for_parse(int str_index, int str_leght, int bytes)
        {
            int tmp_calc = str_index + bytes;
            if (tmp_calc < str_leght)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private int str_legh(int index, string pck)
        {
            int zeros = 0; // " 00 00" end of string = 4 zeros
            int byte_count = 0;

            for (int i = index; i < pck.Length; i++)
            {
                if (pck[i] != ' ')
                {
                    byte_count++;
                    if (pck[i] == '0')
                    {
                        zeros++;
                        if (zeros == 4)
                        {
                            //return pck.Length - i;
                            return byte_count;
                        }
                    }
                    else
                    {
                        zeros--;
                    }
                }
            }
            return 0;
        } // check for strin end
        private int comp_calc1(int index , string pck,char code_char)
        {
            int code = char_to_cod(code_char);
            if (code == 1) // byte 
            {
                return 2;// 00
            }
            if (code == 2)
            {
                return 8; // 00 00 00 00
            }
            if (code == 3 ) // string
            {
                return str_legh(index , pck);
            }
            if (code == 4) // 2 bytes
            {
                return 4;
            }
            if (code == 5) // 8 bytes (int 64)
            {
                return 16;
            }
            if (code == 6) // 8 bytes (double)
            {
                return 16;
            }
            return 0;
        }
        private string convert(int code, string bytes)
        {
            string tmp_str = "";// " = (";

            switch(code)
            {
                case 1:
                    tmp_str = tmp_str + conv_hex_to_byte(bytes);
                    break;
                case 2:
                    tmp_str = tmp_str + conv_hex_to_int32(bytes);
                    break;
                case 3:
                    tmp_str = tmp_str + conv_hex_to_string(bytes);
                    break;

                case 4:
                    tmp_str = tmp_str + conv_hex_to_int16(bytes);
                    break;

                case 5:
                    tmp_str = tmp_str + conv_hex_to_int64(bytes);
                    break;
                case 6:
                    tmp_str = tmp_str + conv_hex_to_double(bytes);
                    break;
                default:
                    tmp_str = tmp_str + "pika error";
                    break;
            }
          //  tmp_str = tmp_str + ")";
            return tmp_str;
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
                    return System.BitConverter.ToInt16(data, 0).ToString();
            }
            return "";
        }
        public string conv_hex_to_int32(string text)
        {
            byte[] data = Globals.pck_thread.StringToByteArray2(text);
            if (data.Length > 3)
            {
                    return System.BitConverter.ToInt32(data, 0).ToString();
            }
            return "";
        }
        public string conv_hex_to_int64(string text)
        {
            byte[] data = Globals.pck_thread.StringToByteArray2(text);
            if (data.Length > 5)
            {
                    return System.BitConverter.ToInt64(data, 0).ToString();
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
                for (int i = 0; i < data.Length; i = i + 2)
                {
                    tmp_data += (char)data[i];
                }
                tmp_data = tmp_data.Replace("\0", "");
                return tmp_data;
            }
            catch
            {
                return "";
            }
        }
    }
    }