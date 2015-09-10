using System;
using System.Collections.Generic;
using System.Text;

namespace L2_login
{
    public enum Win_Types
    {
        NULL = 0,
        CMD = 1,
        GUI = 2,
        HTML = 3,
        GDI = 4
    }

    class ScriptWindow
    {
        private System.Diagnostics.Process _my_proc = null;
        private string _name = "NULL";
        private Win_Types _my_type = Win_Types.NULL;
        private string _hash;

        private readonly object nameLock = new object();

        private bool _ready = false;
        private bool _running = false;

        public ScriptWindow()
        {
            _hash = System.Guid.NewGuid().ToString();
        }

        public void Reset()
        {
            if (_my_proc != null)
            {
                try
                {
                    _my_proc.Kill();
                    _my_proc.Dispose();
                }
                catch
                {
                    //do we really care?
                    //maybe... probably not
                    //just want to make sure the old process is dead
                }
            }

            switch(_my_type)
            {
                case Win_Types.CMD:
                    _my_proc = new System.Diagnostics.Process();
                    _my_proc.StartInfo.CreateNoWindow = true;
                    _my_proc.StartInfo.UseShellExecute = false;
                    _my_proc.StartInfo.RedirectStandardInput = true;
                    _my_proc.StartInfo.RedirectStandardOutput = true;
                    _my_proc.StartInfo.RedirectStandardError = true;
                    break;
                case Win_Types.GUI:
                    break;
                case Win_Types.HTML:
                    Globals.l2net_home.HTMLWindow_Create(_hash);
                    break;
                case Win_Types.GDI:
                    break;
            }

            _ready = false;
            _running = false;

        }

        public void Set_Type(Win_Types type)
        {
            _my_type = type;

            Reset();
        }

        public void Set_WindowName(string name)
        {
            switch (_my_type)
            {
                case Win_Types.CMD:
                    break;
                case Win_Types.GUI:
                    break;
                case Win_Types.HTML:
                    Globals.l2net_home.HTMLWindow_Set_WindowName(_hash, name);
                    break;
                case Win_Types.GDI:
                    break;
            }
        }

        public void Set_FileName(string fname, string args)
        {
            switch(_my_type)
            {
                case Win_Types.CMD:
                    _my_proc.StartInfo.FileName = fname;
                    _my_proc.StartInfo.Arguments = args;
                    break;
                case Win_Types.GUI:
                    _my_proc.StartInfo.FileName = fname;
                    _my_proc.StartInfo.Arguments = args;
                    break;
                case Win_Types.HTML:
                    Globals.l2net_home.HTMLWindow_Set_FileName(_hash, fname);
                    break;
                case Win_Types.GDI:
                    break;
            }

            _ready = true;
        }

        public void Set_HTML(string html)
        {
            switch (_my_type)
            {
                case Win_Types.CMD:
                    break;
                case Win_Types.GUI:
                    break;
                case Win_Types.HTML:
                    Globals.l2net_home.HTMLWindow_Set_HTML(_hash, html);
                    _ready = true;
                    break;
                case Win_Types.GDI:
                    break;
            }
        }

        public void Refresh()
        {
            switch (_my_type)
            {
                case Win_Types.CMD:
                    break;
                case Win_Types.GUI:
                    break;
                case Win_Types.HTML:
                    Globals.l2net_home.HTMLWindow_Refresh(_hash);
                    _ready = true;
                    break;
                case Win_Types.GDI:
                    break;
            }
        }

        public void Open_Window()
        {
            if (_ready && !_running)
            {
                //try
                //{
                    switch(_my_type)
                    {
                        case Win_Types.NULL:
                            break;
                        case Win_Types.CMD:
                            _my_proc.Start();
                            //shouldnt need to do anything, since the start up parameters should hide it already
                            break;
                        case Win_Types.GUI:
                            _my_proc.Start();
                            //TODO
                            //gotta grab the process name and then force the window to hide
                            break;
                        case Win_Types.HTML:
                            Globals.l2net_home.HTMLWindow_Show(_hash);
                            break;
                        case Win_Types.GDI:
                            break;
                    }

                    _running = true;
/*                }
                catch
                {
                    _running = false;

#if DEBUG
                    System.Windows.Forms.MessageBox.Show("Error starting process");
#endif
                }*/
            }
        }

        public void Send_StandardInput(string inp)
        {
            if(_running)
            {
                try
                {
                    switch(_my_type)
                    {
                        case Win_Types.CMD:
                            _my_proc.StandardInput.WriteLine(inp);
                            _my_proc.StandardInput.Flush();
                            break;
                        case Win_Types.GUI:
                            _my_proc.StandardInput.WriteLine(inp);
                            _my_proc.StandardInput.Flush();
                            break;
                        case Win_Types.HTML:
                            break;
                        case Win_Types.GDI:
                            break;
                    }
                }
                catch
                {
                    //we failed to send data to standard input...
                    //this is really bad and perhaps we should "close" our process now
                    _running = false;
                    _ready = false;
                }
            }
        }

        public void Wait_Close()
        {
            if (_running)
            {
                switch (_my_type)
                {
                    case Win_Types.CMD:
                        _my_proc.WaitForExit();
                        break;
                    case Win_Types.GUI:
                        _my_proc.WaitForExit();
                        break;
                    case Win_Types.HTML:
                        break;
                    case Win_Types.GDI:
                        break;
                }

                _running = false;
            }
        }

        public void Wait_Idle()
        {
            if (_running)
            {
                switch (_my_type)
                {
                    case Win_Types.CMD:
                        _my_proc.WaitForInputIdle();
                        break;
                    case Win_Types.GUI:
                        _my_proc.WaitForInputIdle();
                        break;
                    case Win_Types.HTML:
                        break;
                    case Win_Types.GDI:
                        break;
                }
            }
        }

        public void Kill()
        {
            if (_running)
            {
                switch (_my_type)
                {
                    case Win_Types.CMD:
                        _my_proc.Kill();
                        break;
                    case Win_Types.GUI:
                        _my_proc.Kill();
                        break;
                    case Win_Types.HTML:
                        Globals.l2net_home.HTMLWindow_Close(_hash);
                        break;
                    case Win_Types.GDI:
                        break;
                }

                _running = false;
            }
        }

        public string Name
        {
            get
            {
                string tmp;
                lock (nameLock)
                {
                    tmp = this._name;
                }
                return tmp;
            }
            set
            {
                lock (nameLock)
                {
                    _name = value;
                }
            }
        }
    }//end of class
}
