using System;

namespace L2_login
{
	/// <summary>
	/// Summary description for NPC_Chat.
	/// </summary>
	public class NPC_Chat
	{
		public NPC_Chat()
		{
		}

        delegate void Npc_Chat_Callback(ByteBuffer buff);
		public static void Npc_Chat(ByteBuffer buff)
		{
            if (Globals.l2net_home.richTextBox_dialog.InvokeRequired)
            {
                Npc_Chat_Callback d = new Npc_Chat_Callback(Npc_Chat);
                Globals.l2net_home.richTextBox_dialog.Invoke(d, new object[] { buff });
                return;
            }

            try
            {
                Globals.l2net_home.textBox_rtb_input.Visible = false;
                //if (Globals.gamedata.Chron < Chronicle.CT3_0)
                //{
                    uint messge = buff.ReadUInt32();//System.BitConverter.ToInt32(buff,1);offset+=4;
                //}
                //if (Globals.gamedata.Chron >= Chronicle.CT3_0)
                //{
                    //buff.ReadByte();
                //}
            
                string html = buff.ReadString();//Util.Get_String(buff,ref offset);
                //buff.ReadUInt32(); //item id?

#if DEBUG
                System.IO.StreamWriter outf = new System.IO.StreamWriter(Globals.PATH + "\\Logs\\npc.html");
                outf.Write(html);
                outf.Close();
#endif

                string command = "", tlower = "", tmp2 = "";
                string tline = html.Replace("\r\n", "");
                int q1, q2;
                int npc_state = 0;
                int fstring = 0;

                string link_data = "";
                //System.Drawing.Bitmap img;

                Globals.l2net_home.Suspend_Dialog(true);
                Globals.l2net_home.Clear_DialogBox();

                while (tline.Length > 0)
                {
                    if (tline[0] == '<')
                    {
                        q1 = tline.IndexOf('<', 0, tline.Length);
                        if (q1 == -1)
                        {
                            command = tline;
                            tline = "";
                        }
                        else
                        {
                            q2 = tline.IndexOf('>', q1, tline.Length - q1) + 1;

                            command = tline.Substring(q1, q2 - q1);
                            tline = tline.Remove(0, q2);
                        }
                    }
                    else
                    {
                        q1 = tline.IndexOf('<', 0, tline.Length);
                        if (q1 == -1)
                        {
                            command = tline;
                            tline = "";
                        }
                        else
                        {
                            command = tline.Substring(0, q1);
                            tline = tline.Remove(0, q1);
                        }
                    }

                    tlower = command.ToLowerInvariant();
                    switch (tlower)
                    {
                        case "<html>":
                            break;
                        case "<head>":
                            break;
                        case "<body>":
                            break;
                        case "</html>":
                            break;
                        case "</head>":
                            break;
                        case "</body>":
                            break;
                        case "<br>":
                            Globals.l2net_home.Add_Dialog(Environment.NewLine);
                            break;
                        case "<br1>":
                            Globals.l2net_home.Add_Dialog(Environment.NewLine);
                            break;
                        case "</a>":
                            break;
                        case "</font>":
                            break;
                        case "<tr>":
                            break;
                        case "</tr>":
                            Globals.l2net_home.Add_Dialog(Environment.NewLine);
                            break;
                        case "</td>":
                            Globals.l2net_home.Add_Dialog(" ");
                            break;
                        case "<center>":
                            break;
                        case "</center>":
                            break;
                        case "<title>":
                            break;
                        case "</title>":
                            break;
                        case "<p>":
                            break;
                        case "</p>":
                            break;
                        case "</table>":
                            break;
                        case "<fstring>":
                            fstring = 1;
                            break;
                        case "</fstring>":
                            break;
                        default:
                            if (tlower[0] == '<' && tlower[1] == 'f' && tlower[2] == 'o' && tlower[3] == 'n' && tlower[4] == 't')
                            {
                                //just font, no one cares
                            }
                            else if (tlower[0] == '<' && tlower[1] == 'a' && tlower[2] == ' ')
                            {
                                //char[] poo1 = "<a action=\"bypass -h ";
                                //char[] poo2 = "\">";
                                if (tlower.Contains("\"") == false)
                                {
                                    //maybe they fucked up and tried to use ' instead of "
                                    q1 = tlower.IndexOf('\'', 0) + 1;
                                    q2 = tlower.IndexOf('\'', q1 + 1);
                                }
                                else
                                {
                                    q1 = tlower.IndexOf('\"', 0) + 1;
                                    q2 = tlower.IndexOf('\"', q1 + 1);
                                }

                                if (q2 - q1 > 0)
                                {
                                    link_data = command.Substring(q1, q2 - q1);
                                }
                                else
                                {
                                    link_data = "";
                                }

                                npc_state = 1;

                            }
                            else if (tlower[0] == '<' && tlower[1] == 'b' && tlower[2] == 'u' && tlower[3] == 't' && tlower[4] == 't' && tlower[5] == 'o' && tlower[6] == 'n')
                            {
                                q1 = tlower.IndexOf("value=\"", 0) + 7;
                                q2 = tlower.IndexOf("\"", q1 + 1);
                                tmp2 = command.Substring(q1, q2 - q1);

                                //char[] poo1 = "<a action=\"bypass -h ";
                                //char[] poo2 = "\">";
                                q1 = tlower.IndexOf("action=\"", 0) + 8;
                                q2 = tlower.IndexOf('\"', q1 + 1);
                                link_data = command.Substring(q1, q2 - q1);

                                Globals.l2net_home.Add_Dialog_Link(tmp2, link_data);

                            }
                            else if (tlower.StartsWith("<img src="))
                            {
                                //captcha or other custom crest img
                                if (tlower.Contains("crest"))
                                {

                                    q1 = tlower.IndexOf("_1_") + 3;
                                    q2 = tlower.IndexOf("width=") - 2;
                                    link_data = command.Substring(q1, q2 - q1);
                                    if (!Globals.Popup_Captcha)
                                    {
                                        Globals.l2net_home.Add_Dialog_Image(link_data);
                                        //Globals.l2net_home.textBox_rtb_input.Visible = true;
                                    }
                                    else
                                    {
                                        if (Globals.captchawindow == null || Globals.captchawindow.IsDisposed == true)
                                        {
                                            Globals.captchawindow = new Captcha();
                                        }
                                        //Globals.l2net_home.Add_Text("Showing captcha window", Globals.Red, TextType.BOT);
                                        Globals.captchawindow.Add_Captcha_Image(link_data);
                                        Globals.captchawindow.TopMost = true;
                                        Globals.captchawindow.BringToFront();
                                        Globals.captchawindow.Show();
                                    }
                                }

                            }
                            else if (tlower.StartsWith("<table"))
                            {
                                //we dont care since this is just a table
                            }
                            else if (tlower.StartsWith("<td"))
                            {
                                Globals.l2net_home.Add_Dialog(" ");
                            }
                            else if (tlower.StartsWith("<tr"))
                            {
                                Globals.l2net_home.Add_Dialog(Environment.NewLine);
                            }
                            else if (tlower.StartsWith("<\\"))
                            {
                                //we dont care since this is just a terminator
                            }
                            else if (tlower.StartsWith("<fstring"))
                            {
                                fstring = 1;
                                break;
                            }
                            else
                            {
                                if (fstring == 1)
                                {
                                    fstring = 0;
                                    command = ((NPCString)Globals.npcstring[System.Convert.ToUInt32(command)]).text;
                                }
                                else
                                {
                                    command = System.Web.HttpUtility.HtmlDecode(command);
                                }


                                if (npc_state == 1)
                                {
                                    if (command != "[")
                                    {
                                        Globals.l2net_home.Add_Dialog_Link(command, link_data);
                                        npc_state = 0;
                                    }
                                    else
                                    {
                                        Globals.l2net_home.Add_Dialog(command);
                                    }
                                }
                                else if (npc_state == 0)
                                {
                                    Globals.l2net_home.Add_Dialog(command);
                                }
                            }
                            break;
                    }//end of switch

                }//end of while data in this line

                Globals.l2net_home.Show_DialogBox();
            }
            catch
            {
                //npc chat messed up or something
            }
            finally
            {
                Globals.l2net_home.Suspend_Dialog(false);
            }
		}
	}//end of class
}//end of namespace