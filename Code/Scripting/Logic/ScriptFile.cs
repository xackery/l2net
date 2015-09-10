using System;
using System.Collections.Generic;
using System.Text;

namespace L2_login
{
    class ScriptFile
    {
        public System.Collections.ArrayList _ScriptLines = new System.Collections.ArrayList();

        public System.Collections.SortedList _labellist = new System.Collections.SortedList();
        public System.Collections.SortedList _functionlist = new System.Collections.SortedList();
        public System.Collections.SortedList _sublist = new System.Collections.SortedList();
        public string Name = "";

        public void ReadScript(System.IO.StreamReader filein)
        {
            _ScriptLines.Clear();

            string line;

            while ((line = filein.ReadLine()) != null)
            {
                line = line.Trim();

                ScriptLine sl = new ScriptLine();
                sl.FullLine = line;

                string cmd = ScriptEngine.Get_String(ref line, false).ToUpperInvariant();

                sl.Command = ScriptEngine.GetCommandType(cmd);

                switch (sl.Command)
                {
                    case ScriptCommands.ENCRYPTED:
                        //need to scrap everything that has been loaded so far...
                        _ScriptLines.Clear();
                        int key = Util.GetInt32(line);

                        string encoded_script = filein.ReadLine();

                        filein = Script_Crypt.Decrypt(encoded_script, key);
                        break;
                    case ScriptCommands.IF:
                    case ScriptCommands.WHILE:
                    case ScriptCommands.LOOP:
                    case ScriptCommands.UNKNOWN:
                        if (Globals.ScriptCompatibilityv386)
                        {
                            sl.UnParsedParams = line.Replace("(", " ( ").Replace(")", " ) ");
                        }
                        else
                        {
                            sl.UnParsedParams = line;
                        }

                        _ScriptLines.Add(sl);
                        break;
                    default:
                        sl.UnParsedParams = line;

                        _ScriptLines.Add(sl);
                        break;
                }
            }

            //need to parse out comments in the file here...
            bool comment_block = false;

            for (int i = 0; i < _ScriptLines.Count; i++)
            {
                string cmd = ((ScriptLine)_ScriptLines[i]).FullLine.ToUpperInvariant();

                //start a comment block
                if (cmd.StartsWith("/*"))
                {
                    comment_block = true;
                }

                if (comment_block)
                {
                    ((ScriptLine)_ScriptLines[i]).Command = ScriptCommands.NULL;
                    ((ScriptLine)_ScriptLines[i]).FullLine = "";
                }

                //end of comment block
                //  we end after we clear because we want to clear this line still (inclusive)
                if (cmd.EndsWith("*/"))
                {
                    comment_block = false;
                }

                //normal line comment
                if (cmd.StartsWith("//"))
                {
                    ((ScriptLine)_ScriptLines[i]).Command = ScriptCommands.NULL;
                    ((ScriptLine)_ScriptLines[i]).FullLine = "";
                }
            }
        }
    }
}
