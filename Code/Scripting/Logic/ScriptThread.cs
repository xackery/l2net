using System;
using System.Collections.Generic;
using System.Text;

namespace L2_login
{
    class ScriptThread
    {
        public System.Collections.ArrayList _stack = new System.Collections.ArrayList();
        public System.Collections.Stack _functioncalls = new System.Collections.Stack();
        public System.Collections.Stack _subcalls = new System.Collections.Stack();

        public int ID = 0;
        public int StackHeight = 0;
        public int Line_Pos = 0;
        public string Current_File = "";
        public System.DateTime Sleep_Until = System.DateTime.Today.AddDays(-100);

        public void Start()
        {
            Sleep_Until = System.DateTime.Today.AddDays(-100);
        }

        public void Stop()
        {
            Sleep_Until = System.DateTime.Today.AddDays(100);
        }

        public void Kill()
        {
            Stop();
            ScriptEngine.Threads.Remove(ID);
        }
    }
}
