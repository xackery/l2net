using System;
using System.Collections.Generic;
using System.Text;

namespace L2_login
{
    class Script_Class
    {
        public string Name = "";
        public string File = "";
        public string ParentName = "";
        public string ParentFile = "";
        public System.Collections.ArrayList _Variables = new System.Collections.ArrayList();

        public bool Has_Function(string name)
        {
            name = name.ToUpperInvariant();

            if (((ScriptFile)ScriptEngine.Files[File])._functionlist.ContainsKey(name))
            {
                return true;
            }
            /*foreach (ScriptLabel scr_lab in ((ScriptFile)ScriptEngine.Files[File])._functionlist)
            {
                if (System.String.Compare(name, scr_lab.Name) == 0)
                {
                    return true;
                }
            }*/

            return false;
        }

        private void Add_Function(string name, int line, ScriptFile filein, Var_State state)
        {
            ScriptLabel scr_lab = new ScriptLabel();

            scr_lab.Name = name.ToUpperInvariant();
            scr_lab.Line = line;
            scr_lab.File = filein.Name;
            scr_lab.State = state;

            filein._functionlist.Add(scr_lab.Name, scr_lab);
        }

        public void ReadFile(ScriptFile filein)
        {
            File = filein.Name;

            //gotta find the varbeing and varend and read in the middle
            bool cstarted = false;
            bool vstarted = false;
            bool cended = false;

            string p1, p2, p3;

            for(int i = 0; i < filein._ScriptLines.Count; i++)
            {
                ScriptLine cmd = (ScriptLine)filein._ScriptLines[i];

                string line = cmd.UnParsedParams;

                switch(cmd.Command)
                {
                    case ScriptCommands.INCLUDE:
                        if (!cstarted)
                        {
                            Globals.scriptthread.Script_INCLUDE(line);
                        }
                        break;
                    case ScriptCommands.PUBLIC:
                        if (cstarted && vstarted)
                        {
                            //defining a variable
                            p1 = ScriptEngine.Get_String(ref line);
                            p2 = ScriptEngine.Get_String(ref line).ToUpperInvariant();
                            p3 = ScriptEngine.Get_String(ref line);

                            ScriptVariable nv = ScriptEngine.Create_Variable(p1, p2, p3, Var_State.PUBLIC);

                            _Variables.Add(nv);
                        }
                        else if(cstarted)
                        {
                            //a function
                            p1 = ScriptEngine.Get_String(ref line);
                            Add_Function(p1, i, filein, Var_State.PUBLIC);
                        }
                        break;
                    case ScriptCommands.PRIVATE:
                        if (cstarted && vstarted)
                        {
                            //defining a variable
                            p1 = ScriptEngine.Get_String(ref line);
                            p2 = ScriptEngine.Get_String(ref line).ToUpperInvariant();
                            p3 = ScriptEngine.Get_String(ref line);

                            ScriptVariable nv = ScriptEngine.Create_Variable(p1, p2, p3, Var_State.PRIVATE);

                            _Variables.Add(nv);
                        }
                        else if (cstarted)
                        {
                            //a function
                            p1 = ScriptEngine.Get_String(ref line);
                            Add_Function(p1, i, filein, Var_State.PRIVATE);
                        }
                        break;
                    case ScriptCommands.PROTECTED:
                        if (cstarted && vstarted)
                        {
                            //defining a variable
                            p1 = ScriptEngine.Get_String(ref line);
                            p2 = ScriptEngine.Get_String(ref line).ToUpperInvariant();
                            p3 = ScriptEngine.Get_String(ref line);

                            ScriptVariable nv = ScriptEngine.Create_Variable(p1, p2, p3, Var_State.PROTECTED);

                            _Variables.Add(nv);
                        }
                        else if (cstarted)
                        {
                            //a function
                            p1 = ScriptEngine.Get_String(ref line);
                            Add_Function(p1, i, filein, Var_State.PROTECTED);
                        }
                        break;
                    case ScriptCommands.STATIC:
                        if (cstarted && vstarted)
                        {
                            //defining a variable
                            p1 = ScriptEngine.Get_String(ref line);
                            p2 = ScriptEngine.Get_String(ref line).ToUpperInvariant();
                            p3 = ScriptEngine.Get_String(ref line);

                            ScriptVariable nv = ScriptEngine.Create_Variable(p1, p2, p3, Var_State.STATIC);

                            _Variables.Add(nv);
                        }
                        else if (cstarted)
                        {
                            //a function
                            p1 = ScriptEngine.Get_String(ref line);
                            Add_Function(p1, i, filein, Var_State.STATIC);
                        }
                        break;
                    case ScriptCommands.CLASS:
                        cstarted = true;
                        Name = ScriptEngine.Get_String(ref line).ToUpperInvariant();
                        p1 = ScriptEngine.Get_String(ref line).ToUpperInvariant();
                        if (p1 != "NULL" && p1.Length != 0)
                        {
                            //gotta find this class... and copy over it's stuff
                            if (ScriptEngine.Classes.ContainsKey(p1))
                            {
                                Script_Class sc = (Script_Class)ScriptEngine.Classes[p1];

                                foreach (ScriptVariable nv in sc._Variables)
                                {
                                    _Variables.Add(nv.Clone());
                                }

                                ParentName = sc.Name;
                                ParentFile = sc.File;
                            }
                            else
                            {
                                ScriptEngine.Script_Error("CLASS [" + p1 + "] NOT LOADED PRIOR TO USAGE");
                            }
                        }
                        break;
                    case ScriptCommands.END_CLASS:
                        cended = true;
                        cstarted = false;
                        break;
                    case ScriptCommands.VAR_START:
                        vstarted = true;
                        break;
                    case ScriptCommands.VAR_END:
                        vstarted = false;
                        break;
                }

                if (cended)
                {
                    break;
                }
            }
        }
    }

    class Script_ClassData
    {

        public string Name;
        public System.Collections.SortedList _Variables = new System.Collections.SortedList();//ArrayList
        public bool Initialized = false;

        public void Init(string _name)
        {
            Name = _name.ToUpperInvariant();

            if (ScriptEngine.Classes.ContainsKey(Name))
            {
                Script_Class sc = (Script_Class)ScriptEngine.Classes[Name];
                //found our class... need to copy over the variables now
                foreach (ScriptVariable sv in sc._Variables)
                {
                    _Variables.Add(sv.Name, sv.Clone());
                }
            }
        }
    }
}
