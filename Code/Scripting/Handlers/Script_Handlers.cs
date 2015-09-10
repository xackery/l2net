using System;
using System.Collections.Generic;
using System.Text;

namespace L2_login
{
    public partial class ScriptEngine
    {
        private void Dead_Command(string oldcmd, string newcmd)
        {
            ScriptEngine.Script_Error(oldcmd + " no longer exists... please update your script and use " + newcmd + " instead!");
        }

        private void Script_SORT(string line)
        {
            string svar = Get_String(ref line);
            ScriptVariable var = Get_Var(svar);

            string func = Get_String(ref line);

            //tricky...
            //need to sort... but call the function whenever we need to compare two values.
            //but the function is a script call... so we can't just pause the script engine or whatever...
            //and we will need to handle the return somehow...
        }

        private void Script_DELETE_GLOBAL(string line)
        {
            Script_DELETE(line, true);
        }

        private void Script_DELETE(string line, bool global = false)
        {
            string param1 = Get_String(ref line);

            if (param1.Length == 0)
            {
                ScriptEngine.Script_Error("variable name missing");
                return;
            }

            //make the name capital
            param1 = param1.ToUpperInvariant();

            //lets check if the variable already exists
            if (global)
            {
                if (GlobalVariables.ContainsKey(param1))
                {
                    GlobalVariables.Remove(param1);
                    return;
                }
            }
            else
            {
                if (((VariableList)Stack[StackHeight]).ContainsKey(param1))
                {
                    switch(((ScriptVariable)((VariableList)Stack[StackHeight])[param1]).Type)
                    {
                        case Var_Types.THREAD:
                            //stop the thread before we delete the variable
                            ((ScriptThread)((ScriptVariable)((VariableList)Stack[StackHeight])[param1]).Value).Kill();
                            break;
                    }

                    ((VariableList)Stack[StackHeight]).Remove(param1);
                    return;
                }
            }

            ScriptEngine.Script_Error("VARIABLE OF THIS NAME DOES NOT EXIST");
        }

        private void Script_MESSAGE_BOX(string line)
        {
            string title = Get_String(ref line);
            string text = Get_String(ref line);

            string svar = Get_String(ref line);
            ScriptVariable var = Get_Var(svar);

            long type = Util.GetInt64(var.Value.ToString());

            switch (type)
            {
                case 0://Asterisk
                    System.Windows.Forms.MessageBox.Show(text, title, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Asterisk);
                    break;
                case 1://Error
                    System.Windows.Forms.MessageBox.Show(text, title, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    break;
                case 2://Exclamation
                    System.Windows.Forms.MessageBox.Show(text, title, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    break;
                case 3://Hand
                    System.Windows.Forms.MessageBox.Show(text, title, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand);
                    break;
                case 4://Information
                    System.Windows.Forms.MessageBox.Show(text, title, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                    break;
                case 5://None
                    System.Windows.Forms.MessageBox.Show(text, title, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.None);
                    break;
                case 6://Question
                    System.Windows.Forms.MessageBox.Show(text, title, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Question);
                    break;
                case 7://Stop
                    System.Windows.Forms.MessageBox.Show(text, title, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Stop);
                    break;
                case 8://Warning
                    System.Windows.Forms.MessageBox.Show(text, title, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    break;
            }
        }

        private void Script_NMESSAGE_BOX(string line)
        {
            NonBMessageBox bbox = new NonBMessageBox(false);
            
            bbox.title = Get_String(ref line);
            bbox.text = Get_String(ref line);

            string svar = Get_String(ref line);
            ScriptVariable var = Get_Var(svar);

            bbox.type = Util.GetInt64(var.Value.ToString());

            bbox.Show();
        }

        private void Script_NMESSAGE_BOX2(string line)
        {
            NonBMessageBox bbox = new NonBMessageBox(true);

            bbox.title = Get_String(ref line);
            bbox.text = Get_String(ref line);

            string svar = Get_String(ref line);
            ScriptVariable var = Get_Var(svar);

            bbox.type = Util.GetInt64(var.Value.ToString());

            bbox.Show();
        }

        private void Script_GET_FILESIZE(string line)
        {
            string svar = Get_String(ref line);
            ScriptVariable var = Get_Var(svar);

            string filename = Get_String(ref line);

            try
            {
                System.IO.FileInfo finfo = new System.IO.FileInfo(filename);

                switch (var.Type)
                {
                    case Var_Types.INT:
                        var.Value = finfo.Length;
                        break;
                    case Var_Types.DOUBLE:
                        var.Value = (double)finfo.Length;
                        break;
                    case Var_Types.STRING:
                        var.Value = finfo.Length.ToString();
                        break;
                };
            }
            catch
            {
                switch (var.Type)
                {
                    case Var_Types.INT:
                        var.Value = 0L;
                        break;
                    case Var_Types.DOUBLE:
                        var.Value = 0D;
                        break;
                    case Var_Types.STRING:
                        var.Value = "0";
                        break;
                };
            }
        }

        private void Script_SWITCH(string inp)
        {
            string svar = Get_String(ref inp);

            ScriptVariable var = Get_Var(svar);

            //got the variable... need to find the case that matches it now

            int tmp_ifcount = 0;

            for (int i = Line_Pos + 1; i < System.Int32.MaxValue; i++)
            {
                ScriptLine cmd = Get_Line(i);

                switch (cmd.Command)
                {
                    case ScriptCommands.END_OF_FILE:
                        ScriptEngine.Script_Error("UNEXPECTED END_OF_FILE");
                        Globals.gamedata.CurrentScriptState = ScriptState.EOF;
                        return;
                    case ScriptCommands.SWITCH:
                        tmp_ifcount++;
                        break;
                    case ScriptCommands.ENDSWITCH:
                        if (tmp_ifcount == 0)
                        {
                            Line_Pos = i;//move to the line of the endif
                            return;
                        }
                        else
                        {
                            tmp_ifcount--;
                        }
                        break;
                    case ScriptCommands.CASE:
                        if (tmp_ifcount == 0)
                        {
                            string tmpcase = cmd.UnParsedParams;

                            string snvar = Get_String(ref tmpcase);
                            ScriptVariable nvar = Get_Var(snvar);

                            if (var.Type == nvar.Type)
                            {
                                bool same = false;

                                switch(var.Type)
                                {
                                    case Var_Types.NULL:
                                        same = true;
                                        break;
                                    case Var_Types.INT:
                                        if (System.Convert.ToInt64(var.Value) == System.Convert.ToInt64(nvar.Value))
                                            same = true;
                                        break;
                                    case Var_Types.DOUBLE:
                                        if (System.Convert.ToDouble(var.Value) == System.Convert.ToDouble(nvar.Value))
                                            same = true;
                                        break;
                                    case Var_Types.STRING:
                                        if (System.String.Equals(var.Value.ToString(), nvar.Value.ToString()))
                                            same = true;
                                        break;
                                }
                                if (same)
                                {
                                    Line_Pos = i + 1;//move to the line after the case
                                    return;
                                }
                            }
                        }
                        break;
                    case ScriptCommands.DEFAULT:
                        if (tmp_ifcount == 0)
                        {
                            Line_Pos = i + 1;//move to the line after the default
                            return;
                        }
                        break;
                }//if of switch
            }//end of for loop

        }

        private bool Script_LOCK(string line)
        {
            string lname = Get_String(ref line);
            lname = lname.ToUpperInvariant();

            if (Locks.ContainsKey(lname))
            {
                //the lock is in use... we need to wait a bit
                //we should able to just idle until it is open... i think
                Script_SLEEP(0);
                return false;
            }
            else
            {
                Locks.Add(lname, "lock");
                Line_Pos++;
                return true;
            }
        }

        private void Script_UNLOCK(string line)
        {
            string lname = Get_String(ref line);
            lname = lname.ToUpperInvariant();

            if (Locks.ContainsKey(lname))
            {
                Locks.Remove(lname);
            }
            else
            {
                ScriptEngine.Script_Error("lock does not exist");
            }
        }

        private void Script_SET_EVENT(string line)
        {
            int event_id = Util.GetInt32(Get_String(ref line));
            string file = Get_String(ref line);
            string function = Get_String(ref line);
            ScriptEventCaller sc_ec = new ScriptEventCaller();
            sc_ec.Type = (EventType)event_id;
            sc_ec.File = file;
            sc_ec.Function = function.ToUpperInvariant();

            switch(sc_ec.Type)
            {
                case EventType.ServerPacket:
                    int packet_id = Util.GetInt32(Get_String(ref line));

                    if (ServerPacketsContainsKey(packet_id))
                    {
                        ServerPacketsRemoveKey(packet_id);
                    }

                    ServerPacketsAddKey(packet_id, sc_ec);
                    break;
                case EventType.ServerPacketEX:
                    int packetex_id = Util.GetInt32(Get_String(ref line));

                    if (ServerPacketsEXContainsKey(packetex_id))
                    {
                        ServerPacketsEXRemoveKey(packetex_id);
                    }

                    ServerPacketsEXAddKey(packetex_id, sc_ec);
                    break;
                case EventType.ClientPacket:
                    int cpacket_id = Util.GetInt32(Get_String(ref line));

                    if (ClientPacketsContainsKey(cpacket_id))
                    {
                        ClientPacketsRemoveKey(cpacket_id);
                    }

                    ClientPacketsAddKey(cpacket_id, sc_ec);
                    break;
                case EventType.ClientPacketEX:
                    int cpacketex_id = Util.GetInt32(Get_String(ref line));

                    if (ClientPacketsEXContainsKey(cpacketex_id))
                    {
                        ClientPacketsEXRemoveKey(cpacketex_id);
                    }

                    ClientPacketsEXAddKey(cpacketex_id, sc_ec);
                    break;
                case EventType.SelfPacket:
                    int spacket_id = Util.GetInt32(Get_String(ref line));

                    //Globals.l2net_home.Add_Text("spacket_id: " + spacket_id.ToString(),Globals.Green);

                    if (SelfPacketsContainsKey(spacket_id))
                    {
                        SelfPacketsRemoveKey(spacket_id);
                    }

                    SelfPacketsAddKey(spacket_id, sc_ec);
                    break;
                case EventType.SelfPacketEX:
                    int spacketex_id = Util.GetInt32(Get_String(ref line));

                    if (SelfPacketsEXContainsKey(spacketex_id))
                    {
                        SelfPacketsEXRemoveKey(spacketex_id);
                    }

                    SelfPacketsEXAddKey(spacketex_id, sc_ec);
                    break;
                default:
                    if (EventsContainsKey(event_id))
                    {
                        EventsRemoveKey(event_id);
                    }
                    EventsAddKey(event_id, sc_ec);
                    break;
            }
        }

        private void Script_THREAD(string line)
        {
            string fname = Get_String(ref line);

            ScriptThread scr_thread = CreateThread(fname);

            Threads.Add(scr_thread.ID, scr_thread);
        }

        private static ScriptThread CreateThread(string fname)
        {
            int lin = Get_Function_Line(fname, ((ScriptThread)Threads[CurrentThread]).Current_File);

            ScriptThread scr_thread = new ScriptThread();
            scr_thread.Current_File = ((ScriptThread)Threads[CurrentThread]).Current_File;
            scr_thread.Line_Pos = lin + 1;

            VariableList stack_bottom = new VariableList();
            scr_thread._stack.Add(stack_bottom);

            scr_thread.ID = GetUniqueThreadID();

            return scr_thread;
        }

        private void Script_GET_RAND(string line)
        {
            string svar = Get_String(ref line);

            ScriptVariable var = Get_Var(svar);
            //System.DateTime.Now.Ticks

            switch (var.Type)
            {
                case Var_Types.NULL:
                    //do nothing lolz
                    break;
                case Var_Types.INT:
                    int minI = Util.GetInt32(Get_String(ref line));
                    int maxI = Util.GetInt32(Get_String(ref line)) + 1;
                    if (minI > maxI)
                    {
                        var.Value = 0L;
                        ScriptEngine.Script_Error("MIN > MAX");
                    }
                    else
                    {
                        var.Value = (long)Globals.Rando.Next(minI, maxI);
                    }
                    break;
                case Var_Types.DOUBLE:
                    double minD = Util.GetDouble(Get_String(ref line));
                    double maxD = Util.GetDouble(Get_String(ref line));
                    if (minD > maxD)
                    {
                        ScriptEngine.Script_Error("MIN > MAX");
                    }
                    var.Value = (Globals.Rando.NextDouble() * (maxD - minD)) + minD;
                    break;
                case Var_Types.STRING:
                    //lol string?
                    break;
            }
        }

        private void Script_BREAK(string line)
        {
            string scount = Get_String(ref line);

            uint count = 0;
            uint total = Util.GetUInt32(scount);

            //
            for (int i = Line_Pos + 1; i < System.Int32.MaxValue; i++)
            {
                ScriptLine cmd = Get_Line(i);

                switch (cmd.Command)
                {
                    case ScriptCommands.END_OF_FILE:
                        ScriptEngine.Script_Error("UNEXPECTED END_OF_FILE");
                        Globals.gamedata.CurrentScriptState = ScriptState.EOF;
                        return;
                    case ScriptCommands.FUNCTION:
                    case ScriptCommands.SUB:
                        Line_Pos = i;
                        return;
                    //gotta think this through more... it's ok to skip these commands...
                    //but we want to stop at the last one for the current function
                    //how to know if we are at the end?
                    /*case "SCRIPT_END":
                    case "RETURN":
                    case "RETURNSUB":
                        Line_Pos = i;
                        return;*/
                    case ScriptCommands.ENDIF:
                    case ScriptCommands.ENDSWITCH:
                    case ScriptCommands.WEND:
                    case ScriptCommands.LOOP:
                    case ScriptCommands.NEXT:
                    case ScriptCommands.NEXTEACH:
                        count++;
                        if (total == count)
                        {
                            Line_Pos = i + 1;
                            return;
                        }
                        break;
                    case ScriptCommands.IF:
                    case ScriptCommands.SWITCH:
                    case ScriptCommands.WHILE:
                    case ScriptCommands.DO:
                    case ScriptCommands.FOR:
                    case ScriptCommands.FOREACH:
                        count--;
                        break;
                }
            }//end of loop
        }

        private void Script_GET_TIME(string line)
        {
            string svar = Get_String(ref line);

            ScriptVariable var = Get_Var(svar);
            //System.DateTime.Now.Ticks

            switch (var.Type)
            {
                case Var_Types.INT:
                    var.Value = System.DateTime.Now.Ticks;
                    break;
                case Var_Types.DOUBLE:
                    var.Value = (double)System.DateTime.Now.Ticks;
                    break;
                case Var_Types.STRING:
                    var.Value = System.DateTime.Now.Ticks.ToString();
                    break;
            }
        }

        private void Script_PRINT_TEXT(string line, bool is_debug)
        {
            if (!is_debug || (is_debug && Globals.Script_Debugging))
            {
                string param1 = Get_String(ref line);
                Globals.l2net_home.Add_Text(param1,Globals.Red,TextType.BOT);
            }
        }

        private void Script_JUMP_TO_LABEL(string line)
        {
            string param1 = Get_String(ref line);
            int tmp_int1 = Get_Label_Line(param1, ((ScriptThread)Threads[CurrentThread]).Current_File);
            if (tmp_int1 == -1)
            {
                ScriptEngine.Script_Error("LABEL DOES NOT EXIST");
                Globals.gamedata.CurrentScriptState = ScriptState.Error;
            }
            else
            {
                Line_Pos = tmp_int1;
            }
        }

        private void Script_LABEL(string line)
        {
            string param1 = Get_String(ref line);

            Add_Label(param1, Line_Pos, ((ScriptThread)Threads[CurrentThread]).Current_File);
        }

        private void Script_JUMP_TO_LINE(string line)
        {
            string param1 = Get_String(ref line);
            int tmp_int1 = Util.GetInt32(param1);
            if (tmp_int1 < 0)
            {
                ScriptEngine.Script_Error("LINE VALUE IS INVALID");
                Globals.gamedata.CurrentScriptState = ScriptState.Error;
            }
            else
            {
                Line_Pos = tmp_int1;
            }
        }

        private void Script_SLEEP(string line)
        {
            string param1 = Get_String(ref line);

            Script_SLEEP((double)Util.GetInt64(param1));
        }

        private void Script_SLEEP(double time)
        {
            try
            {
                if (time != 0)
                {
                    ((ScriptThread)Threads[CurrentThread]).Sleep_Until = System.DateTime.Now.AddMilliseconds(time);
                }

                BumpThread = true;
            }
            catch
            {
                System.Threading.Thread.Sleep((int)time);
            }
        }

        private void Script_CALLSUB(string line)
        {
            string s_name = Get_String(ref line);

            //check for the existance of the function in the functionlist
            //if we cant find it, then search through the whole script for the function
            s_name = s_name.ToUpperInvariant();
            int call_line = Line_Pos;

            int dest_line = Get_Sub_Line(s_name, ((ScriptThread)Threads[CurrentThread]).Current_File);
            if (dest_line == -1)
            {
                ScriptEngine.Script_Error("SUB DOES NOT EXIST");
                Globals.gamedata.CurrentScriptState = ScriptState.Error;
                return;
            }

            //lets push our return line onto the stack
            Add_ReturnSub(s_name, call_line, ((ScriptThread)Threads[CurrentThread]).Current_File);

            Line_Pos = dest_line + 1;//lets move to the sub definition line + 1
        }

        private void Script_RETURNSUB()
        {
            ScriptLabel source = (ScriptLabel)Subcalls.Pop();

            ((ScriptThread)Threads[CurrentThread]).Current_File = source.File;

            //no return type
            Line_Pos = source.Line + 1;//the source line + 1
            return;
        }

        private void Script_SUB(string line)
        {
            string name = Get_String(ref line);
            //this is just the title of a function...
            //lets cache it for quick calling later

            //other than that, nothing to do
            //shouldn't really ever come across this line unless a loop or something broke and it fell thru to here
            //and that is a script error, not a code error
            name = name.ToUpperInvariant();
            Add_Sub(name, Line_Pos, ((ScriptThread)Threads[CurrentThread]).Current_File);

            Line_Pos++;
        }

        public void Script_INCLUDE(string line)
        {
            string s_file = Get_String(ref line);
            s_file = Globals.PATH + "\\Scripts\\" + s_file;

            s_file = s_file.ToUpperInvariant().Replace('/','\\');

            try
            {
                Script_Class sc = new Script_Class();

                if (!Files.ContainsKey(s_file))
                {
                    System.IO.StreamReader filein = new System.IO.StreamReader(s_file);
                    ScriptFile sf = new ScriptFile();
                    sf.Name = s_file;
                    sf.ReadScript(filein);
                    filein.Close();

                    Files.Add(sf.Name, sf);

                    sc.ReadFile(sf);

                    if (!Classes.ContainsKey(sc.Name))
                    {
                        Classes.Add(sc.Name, sc);
                    }
                    else
                    {
                        //we already have a class of this name... from a different file
                        ScriptEngine.Script_Error("FAILED TO INCLUDE FILE : " + s_file + " : A class of this name [" + sc.Name + "] from a different file has already been loaded");
                        Globals.gamedata.CurrentScriptState = ScriptState.Error;
                    }
                }
                /*else
                {
                    //we already have this file loaded... why would we want to reload it?
                    //what was I thinking here?  maybe they would have a class in a non class file?  fuck that...
                    //sc.ReadFile((ScriptFile)Files[s_file]);
                }*/
            }
            catch
            {
                ScriptEngine.Script_Error("FAILED TO INCLUDE FILE : " + s_file);
                Globals.gamedata.CurrentScriptState = ScriptState.Error;
            }
        }

        private void Script_CALL_EXTERN(string line)
        {
            string s_file = Get_String(ref line);
            string s_name = Get_String(ref line);
            string s_var = Get_String(ref line);
            string s_values = line;

            //check if the file has been loaded yet...
            //if not... load the file into memory
            s_file = Globals.PATH + "\\Scripts\\" + s_file;

            if (!Files.ContainsKey(s_file))
            {
                System.IO.StreamReader filein = new System.IO.StreamReader(s_file);
                ScriptFile sf = new ScriptFile();
                sf.Name = s_file;
                sf.ReadScript(filein);
                filein.Close();

                Files.Add(sf.Name, sf);
            }

            Function_Call(s_file, s_name, s_var, s_values);
        }

        private void Script_CALL(string line)
        {
            string sline = line;

            string s_name = Get_String(ref line);
            string s_var = Get_String(ref line);
            string s_values = line;

            if (s_var == "=")
            {
                //we have an assignment here...
                ScriptVariable dest = Get_Var(s_name);

                Assignment(dest, s_values);
                Line_Pos++;
                return;
            }

            //check for the existance of the function in the functionlist
            //if we cant find it, then search through the whole script for the function
            if (s_name.Contains("."))
            {
                //got a class here...
                int pipe;
                pipe = s_name.LastIndexOf('.');
                string cname = s_name.Substring(0, pipe);
                string fname = s_name.Remove(0, pipe + 1);

                /*if (cname == "BASE")
                {
                    pipe = s_name.LastIndexOf('.');
                    cname = s_name.Substring(0, pipe);
                    fname = s_name.Remove(0, pipe + 1); 
                    
                    ScriptVariable svar = Get_Var(cname);

                    Function_Call(((Script_Class)ScriptEngine._classes[((Script_ClassData)s_class.Value).Name]).ParentFile, s_name.Substring(7), s_var, s_values, isClass, s_class);
                    return;
                }*/

                int call_base = 0;

                while(cname.EndsWith("BASE"))
                {
                    pipe = cname.LastIndexOf('.');
                    cname = cname.Substring(0, pipe);

                    call_base++;
                }

                ScriptVariable svar = Get_Var(cname);

                if (svar.Type == Var_Types.CLASS)
                {
                    if (call_base == 0)
                    {
                        Script_Class sc = (Script_Class)ScriptEngine.Classes[((Script_ClassData)svar.Value).Name];

                        Function_Call(sc.File, fname, s_var, s_values, true, svar);
                    }
                    else
                    {
                        Script_Class sc = (Script_Class)ScriptEngine.Classes[((Script_ClassData)svar.Value).Name];

                        for(int i = 0; i < call_base; i++)
                        {
                            sc = (Script_Class)ScriptEngine.Classes[sc.ParentName];
                        }

                        Function_Call(sc.File, fname, s_var, s_values, true, svar);
                    }
                }
                else
                {
                    try
                    {
                        if (sline.StartsWith("\""))
                        {
                            string sout = Get_String(ref sline);
                            if (sline.Length > 0)
                            {
                                sline = sout + " " + sline;
                            }
                            else
                            {
                                sline = sout;
                            }
                        }

                        Get_Var(sline);
                        Line_Pos++;
                    }
                    catch (Exception e)
                    {
                        ScriptEngine.Script_Error("Error parsing class function : " + cname + "." + fname + " :: " + e.Message);
                        Globals.gamedata.CurrentScriptState = ScriptState.Error;
                    }
                    return;
                }
            }
            else
            {
                Function_Call(((ScriptThread)Threads[CurrentThread]).Current_File, s_name, s_var, s_values);
            }
        }

        private void Function_Call(string s_file, string s_name, string s_var, string s_values, bool isClass = false, ScriptVariable s_class = null)
        {
            s_name = s_name.ToUpperInvariant();
            int call_line = Line_Pos;

            int dest_line = Get_Function_Line(s_name, s_file);
            if (dest_line == -1)
            {
                if (isClass)
                {
                    if (((Script_Class)ScriptEngine.Classes[((Script_ClassData)s_class.Value).Name]).ParentFile != "")
                    {
                        Function_Call(((Script_Class)ScriptEngine.Classes[((Script_ClassData)s_class.Value).Name]).ParentFile, s_name, s_var, s_values, isClass, s_class);
                        return;
                    }
                }
                else
                {
                    ScriptEngine.Script_Error("FUNCTION DOES NOT EXIST : " + s_name);
                    Globals.gamedata.CurrentScriptState = ScriptState.Error;
                    return;
                }
            }

            //lets grab the line of the function
            ScriptLine new_line = Get_Line(dest_line, s_file);
            string line_params = new_line.UnParsedParams;

            string st_count = Get_String(ref s_values);
            int s_count = Util.GetInt32(st_count);

            switch (new_line.Command)
            {
                case ScriptCommands.FUNCTION:
                    //public
                    break;
                case ScriptCommands.PUBLIC:
                    //public
                    break;
                case ScriptCommands.PRIVATE:
                    if (((ScriptThread)Threads[CurrentThread]).Current_File != s_file)
                    {
                        ScriptEngine.Script_Error("ILLEGAL PRIVATE FUNCTION CALL");
                        Line_Pos++;
                        return;
                    }
                    break;
                case ScriptCommands.PROTECTED:
                    if (((ScriptThread)Threads[CurrentThread]).Current_File != s_file)
                    {
                        ScriptEngine.Script_Error("ILLEGAL PROTECTED FUNCTION CALL");
                        Line_Pos++;
                        return;
                    }
                    break;
                default:
                    ScriptEngine.Script_Error("INVALID TYPED FUNCTION CALL");
                    Line_Pos++;
                    return;
            }

            string d_name = Get_String(ref line_params);
            string ds_count = Get_String(ref line_params);
            int d_count = Util.GetInt32(ds_count);

            string s_vname;
            string d_vname;

            Create_Level(StackHeight + 1);//lets clear out any variables that might be left over from the last function call

            //need to loop through all the variables passed and store in the named variables of the dest call
            for (int i = 0; i < d_count; i++)
            {
                if (i < s_count)
                {
                    //the source value is there, lets copy the contents to our new variable
                    s_vname = Get_String(ref s_values);//the source call
                    d_vname = Get_String(ref line_params);//the function def

                    ScriptVariable src_tmp = Get_Var(s_vname);

                    ScriptVariable dst_tmp = new ScriptVariable();
                    dst_tmp.Name = d_vname.ToUpperInvariant();
                    dst_tmp.Type = src_tmp.Type;
                    dst_tmp.Value = src_tmp.Value;
                    dst_tmp.State = src_tmp.State;

                    Add_Variable(dst_tmp, StackHeight + 1);
                }
                else
                {
                    //the source value is missing
                    //make it up?
                    d_vname = Get_String(ref line_params);//the function def

                    ScriptVariable dst_tmp = new ScriptVariable();
                    dst_tmp.Name = d_vname;
                    dst_tmp.Type = Var_Types.NULL;
                    dst_tmp.Value = 0L;
                    dst_tmp.State = Var_State.PUBLIC;

                    Add_Variable(dst_tmp, StackHeight + 1);
                }
            }

            if (isClass)
            {
                ScriptVariable src_tmp = new ScriptVariable();
                src_tmp.Name = "THIS";
                src_tmp.Type = Var_Types.CLASS;
                src_tmp.State = Var_State.PUBLIC;
                src_tmp.Value = s_class.Value;
                Add_Variable(src_tmp, StackHeight + 1);
            }

            //lets push our return line onto the stack
            Add_Return(s_name, call_line, ((ScriptThread)Threads[CurrentThread]).Current_File);

            //and now move us along
            StackHeight = StackHeight + 1;
            ((ScriptThread)Threads[CurrentThread]).Current_File = s_file;
            Line_Pos = dest_line + 1;//lets move to the function definition line + 1
        }

        private void Script_RETURN(string line)
        {
            string var = Get_String(ref line);

            ScriptVariable ret = Get_Var(var);

            if (Functioncalls.Count > 0)
            {
                ScriptLabel source = (ScriptLabel)Functioncalls.Pop();

                ((ScriptThread)Threads[CurrentThread]).Current_File = source.File;

                if (ret.Name == "VOID")
                {
                    //no return type
                    //just need to up the stack by one and go to the source line
                    StackHeight = StackHeight - 1;
                    Line_Pos = source.Line + 1;//the source line + 1

                    Remove_Level(StackHeight + 1);//lets clear out any variables that might be left over from the last function call
                    return;
                }
                else
                {
                    //lets grab the variable
                    ScriptLine cmd = Get_Line(source.Line);
                    string line_params = cmd.UnParsedParams;

                    switch (cmd.Command)
                    {
                        case ScriptCommands.CALL:
                            Get_String(ref line_params, false);//function name
                            break;
                        case ScriptCommands.CALL_EXTERN:
                            Get_String(ref line_params, false);//file name
                            Get_String(ref line_params, false);//function name
                            break;
                        case ScriptCommands.DEFINE:
                        case ScriptCommands.DEFINE_GLOBAL:
                            //this was some constructor...
                            //just return and inc the line
                            StackHeight = StackHeight - 1;
                            Line_Pos = source.Line + 1;//the source line + 1

                            Remove_Level(StackHeight + 1);//lets clear out any variables that might be left over from the last function call
                            return;
                    }

                    string ret_var = Get_String(ref line_params);

                    StackHeight = StackHeight - 1;
                    ScriptVariable dest = Get_Var(ret_var);//, StackHeight - 1);

                    dest.Type = ret.Type;
                    dest.Value = ret.Value;

                    Line_Pos = source.Line + 1;//the source line + 1

                    Remove_Level(StackHeight + 1);//lets clear out any variables that might be left over from the last function call
                    return;
                }
            }
            else
            {
                try
                {
                    //need to kill this thread
                    Threads.Remove(CurrentThread);
                }
                catch (Exception e)
                {
                    ScriptEngine.Script_Error("failed to delete thread id " + CurrentThread.ToString() + " : " + e.Message);
                }
                return;
            }
        }

        private void Script_FUNCTION(string line)
        {
            string name = Get_String(ref line);
            //this is just the title of a function...
            //lets cache it for quick calling later

            //other than that, nothing to do
            //shouldn't really ever come across this line unless a loop or something broke and it fell thru to here
            //and that is a script error, not a code error
            name = name.ToUpperInvariant();
            Add_Function(name, Line_Pos, ((ScriptThread)Threads[CurrentThread]).Current_File, Var_State.PUBLIC);

            Line_Pos++;
        }

        private void Script_DEFINE_GLOBAL(string line)
        {
            Script_DEFINE(line, true, true);
        }
        
        private void Script_DEFINE(string line, bool global = false, bool can_advance = true)
        {
            string param1 = Get_String(ref line);
            string param2 = Get_String(ref line).ToUpperInvariant();
            string param3 = Get_String(ref line);

            bool do_advance = true;

            try
            {
                if (param1.Length == 0)
                {
                    ScriptEngine.Script_Error("variable type missing");
                    return;
                }

                if (param2.Length == 0)
                {
                    ScriptEngine.Script_Error("variable name missing");
                    return;
                }

                //we can have empty values...
                /*if (param3.Length == 0)
                {
                    ScriptEngine.Script_Error("default value missing");
                    return;
                }*/

                //lets check if the variable already exists

                if (global)
                {
                    if (GlobalVariables.ContainsKey(param2))
                    {
                        //variable of this name already exists
                        ScriptEngine.Script_Error("GLOBAL VARIABLE OF THIS NAME ALREADY EXISTS");
                        return;
                    }
                }
                else
                {
                    if (((VariableList)Stack[StackHeight]).ContainsKey(param2))
                    {
                        //variable of this name already exists
                        ScriptEngine.Script_Error("VARIABLE OF THIS NAME ALREADY EXISTS");
                        return;
                    }
                }

                ScriptVariable new_var = Create_Variable(param1, param2, param3, Var_State.PUBLIC);

                if (global)
                {
                    Add_Global(new_var);
                }
                else
                {
                    Add_Variable(new_var, StackHeight);
                }

                if (new_var.Type == Var_Types.CLASS)
                {
                    if (!((Script_ClassData)new_var.Value).Initialized)
                    {
                        if (((Script_Class)ScriptEngine.Classes[((Script_ClassData)new_var.Value).Name]).Has_Function("CONSTRUCT"))
                        {
                            Globals.scriptthread.Function_Call(((Script_Class)ScriptEngine.Classes[((Script_ClassData)new_var.Value).Name]).File, "CONSTRUCT", new_var.Name, param3, true, new_var);
                            ((Script_ClassData)new_var.Value).Initialized = true;
                            do_advance = false;
                        }
                        else
                        {
                            do_advance = true;
                        }
                    }
                    else
                    {
                        do_advance = true;
                    }
                }
            }
            finally
            {
                if (can_advance && do_advance)
                {
                    Line_Pos++;
                }
            }

        }

        public static ScriptVariable Create_Variable(string type, string name, string value, Var_State v)
        {
            type = type.ToUpperInvariant();
            name = name.ToUpperInvariant();

            ScriptVariable new_var = new ScriptVariable();
            new_var.Name = name;
            new_var.State = v;

            switch (type)
            {
                case "NULL":
                    new_var.Type = Var_Types.NULL;
                    break;
                case "INT":
                    new_var.Type = Var_Types.INT;
                    new_var.Value = Util.GetInt64(value);
                    break;
                case "DOUBLE":
                    new_var.Type = Var_Types.DOUBLE;
                    new_var.Value = Util.GetDouble(value);
                    break;
                case "STRING":
                    new_var.Type = Var_Types.STRING;
                    new_var.Value = System.Convert.ToString(value);
                    break;
                case "FILEWRITER":
                    if (Globals.AllowFiles)
                    {
                        //need to figure out how I want to let users write to files outside of the \Scripts\Files\ folder...

                        new_var.Type = Var_Types.FILEWRITER;
                        new_var.Value = new System.IO.StreamWriter(Globals.PATH + "\\Scripts\\Files\\" + System.Convert.ToString(value), false);
                    }
                    else
                    {
                        ScriptEngine.Script_Error("script tried to create file writer without permission");
                    }
                    break;
                case "FILEWRITER_APPEND"://really good idea by toydolls
                    if (Globals.AllowFiles)
                    {
                        new_var.Type = Var_Types.FILEWRITER;
                        new_var.Value = new System.IO.StreamWriter(Globals.PATH + "\\Scripts\\Files\\" + System.Convert.ToString(value), true);
                    }
                    else
                    {
                        ScriptEngine.Script_Error("script tried to create file writer - append without permission");
                    }
                    break;
                case "FILEREADER":
                    if (Globals.AllowFiles)
                    {
                        new_var.Type = Var_Types.FILEREADER;

                        if (System.IO.File.Exists(Globals.PATH + "\\Scripts\\Files\\" + System.Convert.ToString(value)))
                        {
                            new_var.Value = new System.IO.StreamReader(Globals.PATH + "\\Scripts\\Files\\" + System.Convert.ToString(value));
                        }
                        else
                        {
                            ScriptEngine.Script_Error("file [" + value + "] does not exist for reading");
                        }
                    }
                    else
                    {
                        ScriptEngine.Script_Error("script tried to create file reader without permission");
                    }
                    break;
                case "ARRAYLIST":
                    new_var.Type = Var_Types.ARRAYLIST;
                    new_var.Value = new System.Collections.ArrayList(Util.GetInt32(value));
                    break;
                case "SORTEDLIST":
                    new_var.Type = Var_Types.SORTEDLIST;
                    new_var.Value = new System.Collections.SortedList(Util.GetInt32(value));
                    break;
                case "STACK":
                    new_var.Type = Var_Types.STACK;
                    new_var.Value = new System.Collections.Stack();
                    break;
                case "QUEUE":
                    new_var.Type = Var_Types.QUEUE;
                    new_var.Value = new System.Collections.Queue();
                    break;
                case "BYTEBUFFER":
                    new_var.Type = Var_Types.BYTEBUFFER;
                    new_var.Value = new ByteBuffer(Util.GetInt32(value));
                    break;
                case "WINDOW":
                    new_var.Type = Var_Types.WINDOW;
                    new_var.Value = new ScriptWindow();
                    switch (value.ToUpperInvariant())
                    {
                        case "NULL":
                            ((ScriptWindow)new_var.Value).Set_Type(Win_Types.NULL);
                            break;
                        case "CMD":
                            ((ScriptWindow)new_var.Value).Set_Type(Win_Types.CMD);
                            break;
                        case "GUI":
                            ((ScriptWindow)new_var.Value).Set_Type(Win_Types.GUI);
                            break;
                        case "HTML":
                            ((ScriptWindow)new_var.Value).Set_Type(Win_Types.HTML);
                            break;
                        case "GDI":
                            ((ScriptWindow)new_var.Value).Set_Type(Win_Types.GDI);
                            break;
                    }
                    break;
                case "THREAD":
                    new_var.Type = Var_Types.THREAD;
                    new_var.Value = ScriptEngine.CreateThread(value);

                    ((ScriptThread)new_var.Value).Stop();

                    Threads.Add(((ScriptThread)new_var.Value).ID, ((ScriptThread)new_var.Value));
                    break;
                default:
                    //check if it is a user defined type:
                    if (ScriptEngine.Classes.ContainsKey(type))
                    {
                        //Script_Class sc = (Script_Class)ScriptEngine.Classes[type];
                        new_var.Type = Var_Types.CLASS;
                        new_var.Value = new Script_ClassData();
                        ((Script_ClassData)new_var.Value).Init(type);
                    }
                    else
                    {
                        ScriptEngine.Script_Error("INVALID VARIABLE TYPE");
                    }
                    break;
            }

            return new_var;
        }

        private void Create_Level(int h)
        {
            while (h + 1 > Stack.Count)
            {
                Stack.Add(new VariableList());
            }
            //_stack[h] = new VariableList();
            ((VariableList)Stack[h]).Clear();
        }

        private void Remove_Level(int h)
        {
            if (Stack.Count >= h)
            {
                ((VariableList)Stack[h]).Clear();
                Stack.RemoveAt(h);
            }
        }

        private void Add_Variable(ScriptVariable var, int h)
        {
            ((VariableList)Stack[h]).Add_Variable(var);
        }

        private void Add_Global(ScriptVariable var)
        {
            GlobalVariables.Add_Variable(var);
        }

        private void Script_DISTANCE(string line)
        {
            string sdest = Get_String(ref line);
            string sx1 = Get_String(ref line);
            string sy1 = Get_String(ref line);
            string sz1 = Get_String(ref line);
            string sx2 = Get_String(ref line);
            string sy2 = Get_String(ref line);
            string sz2 = Get_String(ref line);

            ScriptVariable dest = Get_Var(sdest);
            ScriptVariable x1 = Get_Var(sx1);
            ScriptVariable y1 = Get_Var(sy1);
            ScriptVariable z1 = Get_Var(sz1);
            ScriptVariable x2 = Get_Var(sx2);
            ScriptVariable y2 = Get_Var(sy2);
            ScriptVariable z2 = Get_Var(sz2);

            double xlim = System.Convert.ToDouble(x1.Value) - System.Convert.ToDouble(x2.Value);
            double ylim = System.Convert.ToDouble(y1.Value) - System.Convert.ToDouble(y2.Value);
            double zlim = System.Convert.ToDouble(z1.Value) - System.Convert.ToDouble(z2.Value);

            double dist = System.Math.Sqrt(System.Math.Pow(xlim, 2) + System.Math.Pow(ylim, 2) + System.Math.Pow(zlim, 2));

            if (dest.Type == Var_Types.INT)
            {
                dest.Value = (long)dist;
            }
            else if (dest.Type == Var_Types.DOUBLE)
            {
                dest.Value = dist;
            }
        }

        private void Script_WHILE(string inp)
        {
            if (Evaluate(inp))
            {
                Line_Pos++;
            }
            else
            {
                ScriptLine calling_line = Get_Line(Line_Pos);

                if (calling_line.LinkedLine != -1)
                {
                    Line_Pos = calling_line.LinkedLine;
                    return;
                }
                else
                {
                    //find the WEND and lets gtfo
                    int tmp__whilecount = 0;

                    for (int i = Line_Pos + 1; i < System.Int32.MaxValue; i++)
                    {
                        ScriptLine cmd = Get_Line(i);

                        switch (cmd.Command)
                        {
                            case ScriptCommands.END_OF_FILE:
                                ScriptEngine.Script_Error("UNEXPECTED END_OF_FILE");
                                Globals.gamedata.CurrentScriptState = ScriptState.EOF;
                                return;
                            case ScriptCommands.WHILE:
                                tmp__whilecount++;
                                break;
                            case ScriptCommands.WEND:
                                if (tmp__whilecount == 0)
                                {
                                    calling_line.LinkedLine = i + 1;
                                    Line_Pos = calling_line.LinkedLine;
                                    return;
                                }
                                else
                                {
                                    tmp__whilecount--;
                                }
                                break;
                        }//if of switch
                    }//end of for loop
                }
            }
        }

        private void Script_WEND()
        {
            ScriptLine calling_line = Get_Line(Line_Pos);

            if (calling_line.LinkedLine != -1)
            {
                Line_Pos = calling_line.LinkedLine;
                return;
            }
            else
            {
                //gotta step up and find the start of this loop
                int tmp__whilecount = 0;

                for (int i = Line_Pos - 1; i >= 0; i--)
                {
                    ScriptLine cmd = Get_Line(i);

                    switch (cmd.Command)
                    {
                        case ScriptCommands.END_OF_FILE:
                            ScriptEngine.Script_Error("UNEXPECTED END_OF_FILE");
                            Globals.gamedata.CurrentScriptState = ScriptState.EOF;
                            return;
                        case ScriptCommands.WEND:
                            tmp__whilecount--;
                            break;
                        case ScriptCommands.WHILE:
                            if (tmp__whilecount == 0)
                            {
                                calling_line.LinkedLine = i;
                                Line_Pos = calling_line.LinkedLine;
                                return;
                            }
                            else
                            {
                                tmp__whilecount++;
                            }
                            break;
                    }//if of switch
                }//end of for loop
            }

            ScriptEngine.Script_Error("WEND ran without finding WHILE");
        }

        private void Script_LOOP(string inp)
        {
            if (Evaluate(inp))
            {
                ScriptLine calling_line = Get_Line(Line_Pos);

                if (calling_line.LinkedLine != -1)
                {
                    Line_Pos = calling_line.LinkedLine;
                    return;
                }
                else
                {
                    //gotta step up and find the start of this loop
                    int tmp__doloopcount = 0;

                    for (int i = Line_Pos - 1; i >= 0; i--)
                    {
                        ScriptLine cmd = Get_Line(i);

                        switch (cmd.Command)
                        {
                            case ScriptCommands.END_OF_FILE:
                                ScriptEngine.Script_Error("UNEXPECTED END_OF_FILE");
                                Globals.gamedata.CurrentScriptState = ScriptState.EOF;
                                return;
                            case ScriptCommands.LOOP:
                                tmp__doloopcount--;
                                break;
                            case ScriptCommands.DO:
                                if (tmp__doloopcount == 0)
                                {
                                    calling_line.LinkedLine = i;
                                    Line_Pos = calling_line.LinkedLine;
                                    return;
                                }
                                else
                                {
                                    tmp__doloopcount++;
                                }
                                break;
                        }//if of switch
                    }//end of for loop
                }

                ScriptEngine.Script_Error("LOOP ran without finding DO");
            }
            else
            {
                //the condition is false... lets just move along
                Line_Pos++;
            }
        }

        private void Script_FOREACH(string inp)
        {
            string sval = Get_String(ref inp).ToUpperInvariant();
            string stype = Get_String(ref inp).ToUpperInvariant();
            string ssortedlist = Get_String(ref inp).ToUpperInvariant();

            //assign the start to val
            //if the variable does not exist... we need to make it

            bool found = VariableExists(sval);

            ScriptVariable var, sortedlist;
            bool is_sortedlist = false;
            int _length = 0;

            //if the variable doesnt exist, lets create it with our starting value
            if (!found)
            {
                string nline = "INT " + sval + " 0";
                Script_DEFINE(nline, false, false);
                var = Get_Var(sval);
            }
            else//otherwise lets just set the value for it
            {
                var = Get_Var(sval);
                var.Type = Var_Types.INT;
                var.Value = 0L;
            }

            //find the sorted list
            sortedlist = Get_Var(ssortedlist);

            if (sortedlist.Type == Var_Types.SORTEDLIST)
            {
                is_sortedlist = true;
                _length = ((System.Collections.SortedList)sortedlist.Value).Values.Count;
            }
            else if (sortedlist.Type == Var_Types.ARRAYLIST)
            {
                is_sortedlist = false;
                _length = ((System.Collections.ArrayList)sortedlist.Value).Count;
            }
            else
            {
                ScriptEngine.Script_Error("INVALID DATASET");
            }

            bool skip = true;
            Var_Types vt;
            string ct = "";

            //lets find the type we are gonna be looking for
            switch (stype)
            {
                case "NULL":
                    vt = Var_Types.NULL;
                    break;
                case "INT":
                    vt = Var_Types.INT;
                    break;
                case "DOUBLE":
                    vt = Var_Types.DOUBLE;
                    break;
                case "STRING":
                    vt = Var_Types.STRING;
                    break;
                case "FILEWRITER":
                    vt = Var_Types.FILEWRITER;
                    break;
                case "FILEREADER":
                    vt = Var_Types.FILEREADER;
                    break;
                case "ARRAYLIST":
                    vt = Var_Types.ARRAYLIST;
                    break;
                case "SORTEDLIST":
                    vt = Var_Types.SORTEDLIST;
                    break;
                case "STACK":
                    vt = Var_Types.STACK;
                    break;
                case "QUEUE":
                    vt = Var_Types.QUEUE;
                    break;
                case "BYTEBUFFER":
                    vt = Var_Types.BYTEBUFFER;
                    break;
                default:
                    //check if it is a user defined type:
                    if (ScriptEngine.Classes.ContainsKey(stype))
                    {
                        vt = Var_Types.CLASS;
                        ct = stype;
                    }
                    else
                    {
                        ScriptEngine.Script_Error("INVALID VARIABLE TYPE");
                        return;
                    }
                    break;
            }

            //lets find the first instance of the value in the array and go with it
            for (int index = 0; index < _length; index++)
            {
                if (is_sortedlist)
                {
                    if (((ScriptVariable)((System.Collections.SortedList)sortedlist.Value).GetByIndex(index)).Type == vt)
                    {
                        if (vt == Var_Types.CLASS)
                        {
                            if (((Script_ClassData)((ScriptVariable)((System.Collections.SortedList)sortedlist.Value).GetByIndex(index)).Value).Name == ct)
                            {
                                var.Value = (long)index;
                                skip = false;
                                break;
                            }
                        }
                        else
                        {
                            var.Value = (long)index;
                            skip = false;
                            break;
                        }
                    }
                }
                else
                {
                    if (((ScriptVariable)((System.Collections.ArrayList)sortedlist.Value)[index]).Type == vt)
                    {
                        if (vt == Var_Types.CLASS)
                        {
                            if (((Script_ClassData)((ScriptVariable)((System.Collections.ArrayList)sortedlist.Value)[index]).Value).Name == ct)
                            {
                                var.Value = (long)index;
                                skip = false;
                                break;
                            }
                        }
                        else
                        {
                            var.Value = (long)index;
                            skip = false;
                            break;
                        }
                    }
                }
            }

            if (skip)
            {
                ScriptLine calling_line = Get_Line(Line_Pos);

                //lets find the next and jump to the line after it
                if (calling_line.LinkedLine != -1)
                {
                    Line_Pos = calling_line.LinkedLine;
                    return;
                }
                else
                {
                    int tmp_forcount = 0;

                    for (int i = Line_Pos + 1; i < System.Int32.MaxValue; i++)
                    {
                        ScriptLine cmd = Get_Line(i);

                        switch (cmd.Command)
                        {
                            case ScriptCommands.END_OF_FILE:
                                ScriptEngine.Script_Error("UNEXPECTED END_OF_FILE");
                                Globals.gamedata.CurrentScriptState = ScriptState.EOF;
                                return;
                            case ScriptCommands.FOREACH:
                                tmp_forcount++;
                                break;
                            case ScriptCommands.NEXTEACH:
                                if (tmp_forcount == 0)
                                {
                                    calling_line.LinkedLine = i + 1;
                                    Line_Pos = calling_line.LinkedLine;
                                    return;
                                }
                                else
                                {
                                    tmp_forcount--;
                                }
                                break;
                        }
                    }
                }
            }//end of skip
            else
            {
                //its all good, move 1 line
                Line_Pos++;
            }
        }

        private void Script_NEXTEACH()
        {
            ScriptLine calling_line = Get_Line(Line_Pos);
            ScriptLine cmd;

            //lets find the line of the for and get the info out of it
            if (calling_line.LinkedLine == -1)
            {
                int tmp_forcount = 0;

                for (int i = Line_Pos - 1; i >= 0; i--)
                {
                    cmd = Get_Line(i);

                    switch (cmd.Command)
                    {
                        case ScriptCommands.END_OF_FILE:
                            ScriptEngine.Script_Error("UNEXPECTED END_OF_FILE");
                            Globals.gamedata.CurrentScriptState = ScriptState.EOF;
                            return;
                        case ScriptCommands.FOREACH:
                            if (tmp_forcount == 0)
                            {
                                calling_line.LinkedLine = i;
                            }
                            else
                            {
                                tmp_forcount++;
                            }
                            break;
                        case ScriptCommands.NEXTEACH:
                            tmp_forcount--;
                            break;
                    }

                    if (calling_line.LinkedLine != -1)
                        break;
                }
            }

            if (calling_line.LinkedLine == -1)
            {
                //baaad
                ScriptEngine.Script_Error("NEXTEACH ran without finding FOREACH");
                Line_Pos++;//just skip out of this loop and move on
                return;
            }

            //lets increment the var by sstep
            cmd = this.Get_Line(calling_line.LinkedLine);
            string line = cmd.UnParsedParams.ToUpperInvariant();

            //get the var
            string svar = Get_String(ref line);

            //get the type out
            string stype = Get_String(ref line);

            //get the end
            string ssortedlist = Get_String(ref line);

            ScriptVariable var = Get_Var(svar);
            ScriptVariable sortedlist = Get_Var(ssortedlist);
            bool is_sortedlist = false;
            int _length = 0;

            if (sortedlist.Type == Var_Types.SORTEDLIST)
            {
                is_sortedlist = true;
                _length = ((System.Collections.SortedList)sortedlist.Value).Values.Count;
            }
            else if (sortedlist.Type == Var_Types.ARRAYLIST)
            {
                is_sortedlist = false;
                _length = ((System.Collections.ArrayList)sortedlist.Value).Count;
            }
            else
            {
                ScriptEngine.Script_Error("INVALID DATASET");
            }

            bool skip = true;
            Var_Types vt;
            string ct = "";

            //lets find the type we are gonna be looking for
            switch (stype)
            {
                case "NULL":
                    vt = Var_Types.NULL;
                    break;
                case "INT":
                    vt = Var_Types.INT;
                    break;
                case "DOUBLE":
                    vt = Var_Types.DOUBLE;
                    break;
                case "STRING":
                    vt = Var_Types.STRING;
                    break;
                case "FILEWRITER":
                    vt = Var_Types.FILEWRITER;
                    break;
                case "FILEREADER":
                    vt = Var_Types.FILEREADER;
                    break;
                case "ARRAYLIST":
                    vt = Var_Types.ARRAYLIST;
                    break;
                case "SORTEDLIST":
                    vt = Var_Types.SORTEDLIST;
                    break;
                case "STACK":
                    vt = Var_Types.STACK;
                    break;
                case "QUEUE":
                    vt = Var_Types.QUEUE;
                    break;
                default:
                    //check if it is a user defined type:
                    if (ScriptEngine.Classes.ContainsKey(stype))
                    {
                        vt = Var_Types.CLASS;
                        ct = stype;
                    }
                    else
                    {
                        ScriptEngine.Script_Error("INVALID VARIABLE TYPE");
                        return;
                    }
                    break;
            }

            //lets find the first instance of the value in the array and go with it
            for (int index = System.Convert.ToInt32(var.Value) + 1; index < _length; index++)
            {
                if (is_sortedlist)
                {
                    if (((ScriptVariable)((System.Collections.SortedList)sortedlist.Value).GetByIndex(index)).Type == vt)
                    {
                        if (vt == Var_Types.CLASS)
                        {
                            if (((Script_ClassData)((ScriptVariable)((System.Collections.SortedList)sortedlist.Value).GetByIndex(index)).Value).Name == ct)
                            {
                                var.Value = (long)index;
                                skip = false;
                                break;
                            }
                        }
                        else
                        {
                            var.Value = (long)index;
                            skip = false;
                            break;
                        }
                    }
                }
                else
                {
                    if (((ScriptVariable)((System.Collections.ArrayList)sortedlist.Value)[index]).Type == vt)
                    {
                        if (vt == Var_Types.CLASS)
                        {
                            if (((Script_ClassData)((ScriptVariable)((System.Collections.ArrayList)sortedlist.Value)[index]).Value).Name == ct)
                            {
                                var.Value = (long)index;
                                skip = false;
                                break;
                            }
                        }
                        else
                        {
                            var.Value = (long)index;
                            skip = false;
                            break;
                        }
                    }
                }
            }

            if (skip)
            {
                Line_Pos++;
            }
            else
            {
                //move to the line after the FOR, so it doesnt mess us up
                Line_Pos = calling_line.LinkedLine + 1;
            }
        }
        
        private void Script_FOR(string inp)
        {
            string sval = Get_String(ref inp).ToUpperInvariant();
            string sstart = Get_String(ref inp);
            string send = Get_String(ref inp);
            string sstep = Get_String(ref inp);

            //assign the start to val
            //if the variable does not exist... we need to make it

            bool found = VariableExists(sval);

            ScriptVariable var;

            //if the variable doesnt exist, lets create it with our starting value
            if (!found)
            {
                string nline = "INT " + sval + " " + sstart;
                Script_DEFINE(nline, false, false);
                var = Get_Var(sval);
            }
            else//otherwise lets just set the value for it
            {
                var = Get_Var(sval);
                var.Type = Var_Types.INT;
                var.Value = Util.GetInt64(sstart);
            }

            long end = Util.GetInt64(send);
            long step = Util.GetInt64(sstep);
            bool skip = false;

            //check if start > end or start < end for negative
            if (step > 0)
            {
                //positive increment
                if (((long)var.Value) >= end)
                {
                    skip = true;
                }
            }
            else
            {
                //negative increment
                if (((long)var.Value) <= end)
                {
                    skip = true;
                }
            }

            //if skip
            if (skip)
            {
                ScriptLine calling_line = Get_Line(Line_Pos);

                if (calling_line.LinkedLine != -1)
                {
                    Line_Pos = calling_line.LinkedLine;
                    return;
                }
                else
                {
                    //lets find the next and jump to the line after it
                    int tmp_forcount = 0;

                    for (int i = Line_Pos + 1; i < System.Int32.MaxValue; i++)
                    {
                        ScriptLine cmd = Get_Line(i);

                        switch (cmd.Command)
                        {
                            case ScriptCommands.END_OF_FILE:
                                ScriptEngine.Script_Error("UNEXPECTED END_OF_FILE");
                                Globals.gamedata.CurrentScriptState = ScriptState.EOF;
                                return;
                            case ScriptCommands.FOR:
                                tmp_forcount++;
                                break;
                            case ScriptCommands.NEXT:
                                if (tmp_forcount == 0)
                                {
                                    calling_line.LinkedLine = i + 1;
                                    Line_Pos = calling_line.LinkedLine;
                                    return;
                                }
                                else
                                {
                                    tmp_forcount--;
                                }
                                break;
                        }
                    }
                }
            }//end of skip
            else
            {
                //its all good, move 1 line
                Line_Pos++;
            }
        }

        private void Script_NEXT()
        {
            ScriptLine calling_line = Get_Line(Line_Pos);
            ScriptLine cmd;

            //lets find the line of the for and get the info out of it
            if (calling_line.LinkedLine == -1)
            {
                int tmp_forcount = 0;

                for (int i = Line_Pos - 1; i >= 0; i--)
                {
                    cmd = Get_Line(i);

                    switch (cmd.Command)
                    {
                        case ScriptCommands.END_OF_FILE:
                            ScriptEngine.Script_Error("UNEXPECTED END_OF_FILE");
                            Globals.gamedata.CurrentScriptState = ScriptState.EOF;
                            return;
                        case ScriptCommands.FOR:
                            if (tmp_forcount == 0)
                            {
                                calling_line.LinkedLine = i;
                            }
                            else
                            {
                                tmp_forcount++;
                            }
                            break;
                        case ScriptCommands.NEXT:
                            tmp_forcount--;
                            break;
                    }

                    if (calling_line.LinkedLine != -1)
                        break;
                }
            }

            if (calling_line.LinkedLine == -1)
            {
                //baaad
                ScriptEngine.Script_Error("NEXT ran without finding FOR");
                Line_Pos++;//just skip out of this loop and move on
                return;
            }

            //lets increment the var by sstep
            cmd = this.Get_Line(calling_line.LinkedLine);
            string line = cmd.UnParsedParams.ToUpperInvariant();

            //get the var
            string svar = Get_String(ref line);

            //get the start out
            string sstart = Get_String(ref line, false);

            //get the end
            string send = Get_String(ref line);

            //get the step
            string sstep = Get_String(ref line);

            ScriptVariable var = Get_Var(svar);
            long end = Util.GetInt64(send);
            long step = Util.GetInt64(sstep);

            var.Value = step + ((long)var.Value);

            //lets check if we should exit, if so, just move down one line
            bool skip = false;

            //check if start > end or start < end for negative
            //check if start > end or start < end for negative
            if (step > 0)
            {
                //positive increment
                if (((long)var.Value) >= end)
                {
                    skip = true;
                }
            }
            else
            {
                //negative increment
                if (((long)var.Value) <= end)
                {
                    skip = true;
                }
            }

            if (skip)
            {
                Line_Pos++;
            }
            else
            {
                //move to the line after the FOR, so it doesnt mess us up
                Line_Pos = calling_line.LinkedLine + 1;
            }
        }

        private void Script_IF(string inp)
        {
            //if the condition is true, then continue as normal
            //otherwise we need to check for an ELSE
            //or skip down to the ENDIF
            if (Evaluate(inp))
            {
                //everything is good, lets move down 1 line
                Line_Pos++;
            }
            else
            {
                ScriptLine calling_line = Get_Line(Line_Pos);

                if (calling_line.LinkedLine2 != -1)
                {
                    //jump to the line after the else
                    Line_Pos = calling_line.LinkedLine2;
                }
                else if (calling_line.LinkedLine != -1)
                {
                    //jump to the endif
                    Line_Pos = calling_line.LinkedLine;
                }
                else
                {
                    //lets find the else and jump to it
                    int tmp_ifcount = 0;

                    for (int i = Line_Pos + 1; i < System.Int32.MaxValue; i++)
                    {
                        ScriptLine cmd = Get_Line(i);

                        switch (cmd.Command)
                        {
                            case ScriptCommands.END_OF_FILE:
                                ScriptEngine.Script_Error("UNEXPECTED END_OF_FILE");
                                Globals.gamedata.CurrentScriptState = ScriptState.EOF;
                                return;
                            case ScriptCommands.IF:
                                tmp_ifcount++;
                                break;
                            case ScriptCommands.ENDIF:
                                if (tmp_ifcount == 0)
                                {
                                    calling_line.LinkedLine = i;//move to the line of the endif
                                    Line_Pos = calling_line.LinkedLine;
                                    return;
                                }
                                else
                                {
                                    tmp_ifcount--;
                                }
                                break;
                            case ScriptCommands.ELSE:
                                if (tmp_ifcount == 0)
                                {
                                    calling_line.LinkedLine2 = i + 1;//stupid thing needs to go to the line AFTER the else
                                    Line_Pos = calling_line.LinkedLine2;
                                    return;
                                }
                                break;
                        }//if of switch
                    }//end of for loop
                }
            }//end of if(Evaluate
        }

        private void Script_ELSE()
        {
            ScriptLine calling_line = Get_Line(Line_Pos);

            if (calling_line.LinkedLine != -1)
            {
                Line_Pos = calling_line.LinkedLine;
                return;
            }
            else
            {
                int tmp_ifcount = 0;

                for (int i = Line_Pos + 1; i < System.Int32.MaxValue; i++)
                {
                    ScriptLine cmd = Get_Line(i);

                    switch (cmd.Command)
                    {
                        case ScriptCommands.END_OF_FILE:
                            ScriptEngine.Script_Error("UNEXPECTED END_OF_FILE");
                            Globals.gamedata.CurrentScriptState = ScriptState.EOF;
                            return;
                        case ScriptCommands.IF:
                            tmp_ifcount++;
                            break;
                        case ScriptCommands.ENDIF:
                            if (tmp_ifcount == 0)
                            {
                                calling_line.LinkedLine = i;
                                Line_Pos = calling_line.LinkedLine;
                                return;
                            }
                            else
                            {
                                tmp_ifcount--;
                            }
                            break;
                    }//if of switch
                }//end of for loop
            }
        }

        private int Get_Label_Line(string label_name, string file)
        {
            label_name = label_name.ToUpperInvariant();

            if (((ScriptFile)Files[file])._labellist.ContainsKey(label_name))
            {
                return ((ScriptLabel)(((ScriptFile)Files[file])._labellist[label_name])).Line;
            }
            /*foreach (ScriptLabel scr_lab in ((ScriptFile)Files[file])._labellist)
            {
                if (scr_lab.Name == label_name)
                {
                    return scr_lab.Line;
                }
            }*/

            string tmp, tmpline;

            for (int i = 0; i < ((ScriptFile)Files[file])._ScriptLines.Count; i++)
            {
                ScriptLine tmpcmd = ((ScriptLine)((ScriptFile)Files[file])._ScriptLines[i]);

                if (tmpcmd.Command == ScriptCommands.LABEL)
                {
                    tmpline = tmpcmd.UnParsedParams;

                    tmp = Get_String(ref tmpline, false).ToUpperInvariant();

                    if (tmp == label_name)
                    {
                        //lets add the label to our known list for future reference
                        Add_Label(label_name, i, file);

                        return i;
                    }
                }
            }

            return -1;
        }

        private static int Get_Function_Line(string label_name, string file)
        {
            label_name = label_name.ToUpperInvariant();

            if (((ScriptFile)Files[file])._functionlist.ContainsKey(label_name))
            {
                return ((ScriptLabel)(((ScriptFile)Files[file])._functionlist[label_name])).Line;
            }
            /*foreach (ScriptLabel scr_lab in ((ScriptFile)Files[file])._functionlist)
            {
                if (scr_lab.Name == label_name)
                {
                    return scr_lab.Line;
                }
            }*/

            //we must not have parsed this file yet... let's do so now

            bool vstart = false;
            string tmp, tmpline;

            for (int i = 0; i < ((ScriptFile)Files[file])._ScriptLines.Count; i++)
            {
                ScriptLine tmpcmd = ((ScriptLine)((ScriptFile)Files[file])._ScriptLines[i]);
                tmpline = tmpcmd.UnParsedParams;

                switch (tmpcmd.Command)
                {
                    case ScriptCommands.VAR_START:
                        vstart = true;
                        break;
                    case ScriptCommands.VAR_END:
                        vstart = false;
                        break;
                    case ScriptCommands.FUNCTION:
                        tmp = Get_String(ref tmpline, false).ToUpperInvariant();
                        if (tmp == label_name)
                        {
                            //lets add the label to our known list for future reference
                            Add_Function(label_name, i, file, Var_State.PUBLIC);

                            return i;
                        }
                        break;
                    case ScriptCommands.PUBLIC:
                        if (vstart)
                        {
                            //a variable
                        }
                        else
                        {
                            tmp = Get_String(ref tmpline, false).ToUpperInvariant();
                            if (tmp == label_name)
                            {
                                //lets add the label to our known list for future reference
                                Add_Function(label_name, i, file, Var_State.PUBLIC);

                                return i;
                            }
                        }
                        break;
                    case ScriptCommands.PRIVATE:
                        if (vstart)
                        {
                            //a variable
                        }
                        else
                        {
                            tmp = Get_String(ref tmpline, false).ToUpperInvariant();
                            if (tmp == label_name)
                            {
                                //lets add the label to our known list for future reference
                                Add_Function(label_name, i, file, Var_State.PRIVATE);

                                return i;
                            }
                        }
                        break;
                    case ScriptCommands.PROTECTED:
                        if (vstart)
                        {
                            //a variable
                        }
                        else
                        {
                            tmp = Get_String(ref tmpline, false).ToUpperInvariant();
                            if (tmp == label_name)
                            {
                                //lets add the label to our known list for future reference
                                Add_Function(label_name, i, file, Var_State.PROTECTED);

                                return i;
                            }
                        }
                        break;
                    case ScriptCommands.STATIC:
                        if (vstart)
                        {
                            //a variable
                        }
                        else
                        {
                            tmp = Get_String(ref tmpline, false).ToUpperInvariant();
                            if (tmp == label_name)
                            {
                                //lets add the label to our known list for future reference
                                Add_Function(label_name, i, file, Var_State.STATIC);

                                return i;
                            }
                        }
                        break;
                }
            }

            return -1;
        }

        private int Get_Sub_Line(string label_name, string file)
        {
            label_name = label_name.ToUpperInvariant();

            if (((ScriptFile)Files[file])._sublist.ContainsKey(label_name))
            {
                return ((ScriptLabel)(((ScriptFile)Files[file])._sublist[label_name])).Line;
            }
            /*foreach (ScriptLabel scr_lab in ((ScriptFile)Files[file])._sublist)
            {
                if (scr_lab.Name == label_name)
                {
                    return scr_lab.Line;
                }
            }*/

            string tmp, tmpline;

            for (int i = 0; i < ((ScriptFile)Files[file])._ScriptLines.Count; i++)
            {
                ScriptLine tmpcmd = ((ScriptLine)((ScriptFile)Files[file])._ScriptLines[i]);

                if (tmpcmd.Command == ScriptCommands.SUB)
                {
                    tmpline = tmpcmd.UnParsedParams;
                    tmp = Get_String(ref tmpline, false).ToUpperInvariant();

                    if (tmp == label_name)
                    {
                        //lets add the label to our known list for future reference
                        Add_Sub(label_name, i, file);

                        return i;
                    }
                }
            }

            return -1;
        }

        private void Add_Label(string name, int line, string file)
        {
            ScriptLabel scr_lab = new ScriptLabel();

            scr_lab.Name = name.ToUpperInvariant();
            scr_lab.Line = line;
            scr_lab.File = file;

            if (!((ScriptFile)Files[file])._labellist.ContainsKey(scr_lab.Name))
            {
                ((ScriptFile)Files[file])._labellist.Add(scr_lab.Name, scr_lab);
            }
            //Labellist.Add(scr_lab);
        }


        private static void Add_Function(string name, int line, string file, Var_State state)
        {
            ScriptLabel scr_lab = new ScriptLabel();

            scr_lab.Name = name.ToUpperInvariant();
            scr_lab.Line = line;
            scr_lab.File = file;
            scr_lab.State = state;

            if (!((ScriptFile)Files[file])._functionlist.ContainsKey(scr_lab.Name))
            {
                ((ScriptFile)Files[file])._functionlist.Add(scr_lab.Name, scr_lab);
            }
            //Functionlist.Add(scr_lab);
        }

        private void Add_Sub(string name, int line, string file)
        {
            ScriptLabel scr_lab = new ScriptLabel();

            scr_lab.Name = name.ToUpperInvariant();
            scr_lab.Line = line;
            scr_lab.File = file;

            if (!((ScriptFile)Files[file])._sublist.ContainsKey(scr_lab.Name))
            {
                ((ScriptFile)Files[file])._sublist.Add(scr_lab.Name, scr_lab);
            }
            //Sublist.Add(scr_lab);
        }

        private void Add_Return(string name, int line, string file)
        {
            ScriptLabel scr_lab = new ScriptLabel();

            scr_lab.Name = name.ToUpperInvariant();
            scr_lab.Line = line;
            scr_lab.File = file;

            Functioncalls.Push(scr_lab);
        }

        private void Add_ReturnSub(string name, int line, string file)
        {
            ScriptLabel scr_lab = new ScriptLabel();

            scr_lab.Name = name.ToUpperInvariant();
            scr_lab.Line = line;
            scr_lab.File = file;

            Subcalls.Push(scr_lab);
        }

        private void Script_HEX_TO_DEC(string line)
        {
            string var = Get_String(ref line);
            string hexValue = Get_String(ref line);
            int decValue = int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);

            ScriptVariable v_str = Get_Var(var);
            v_str.Type = Var_Types.INT;
            v_str.Value = decValue;
        }
    }
}
