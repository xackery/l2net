using System;
using System.Collections.Generic;
using System.Text;

namespace L2_login
{
    public partial class ScriptEngine
    {
        private void Script_TEST_WEBSITE(string line)
        {
            string svar = Get_String(ref line);
            ScriptVariable var = Get_Var(svar);

            string addy = Get_String(ref line);

            bool success = Util.TestWebsite(addy);

            if (success)
            {
                switch (var.Type)
                {
                    case Var_Types.INT:
                        var.Value = 1L;
                        break;
                    case Var_Types.DOUBLE:
                        var.Value = 1D;
                        break;
                    case Var_Types.STRING:
                        var.Value = "1";
                        break;
                }
            }
            else
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
                }
            }
        }

        private void Script_TEST_ODBC(string line)
        {
            string svar = Get_String(ref line);
            ScriptVariable var = Get_Var(svar);

            string addy = Get_String(ref line);

            bool success = Util.TestODBC(addy);

            if (success)
            {
                switch (var.Type)
                {
                    case Var_Types.INT:
                        var.Value = 1L;
                        break;
                    case Var_Types.DOUBLE:
                        var.Value = 1D;
                        break;
                    case Var_Types.STRING:
                        var.Value = "1";
                        break;
                }
            }
            else
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
                }
            }
        }

        private void Script_TEST_DNS(string line)
        {
            string svar = Get_String(ref line);
            ScriptVariable var = Get_Var(svar);

            string addy = Get_String(ref line);

            bool success = Util.TestDNS(addy);

            if (success)
            {
                switch (var.Type)
                {
                    case Var_Types.INT:
                        var.Value = 1L;
                        break;
                    case Var_Types.DOUBLE:
                        var.Value = 1D;
                        break;
                    case Var_Types.STRING:
                        var.Value = "1";
                        break;
                }
            }
            else
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
                }
            }
        }

        private void Script_TEST_PING(string line)
        {
            string svar = Get_String(ref line);
            ScriptVariable var = Get_Var(svar);

            string addy = Get_String(ref line);

            bool success = Util.TestPing(addy);

            if (success)
            {
                switch (var.Type)
                {
                    case Var_Types.INT:
                        var.Value = 1L;
                        break;
                    case Var_Types.DOUBLE:
                        var.Value = 1D;
                        break;
                    case Var_Types.STRING:
                        var.Value = "1";
                        break;
                }
            }
            else
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
                }
            }
        }

        private void Script_SEND_EMAIL(string line)
        {
            string _from = Get_String(ref line);
            string _to = Get_String(ref line);
            string _sub = Get_String(ref line);
            string _body = Get_String(ref line);
            string _server = Get_String(ref line);
            int _port = Util.GetInt32(Get_String(ref line));

            Util.Send_Email(_from, _to, _sub, _body, _server, _port);
        }
    }
}
