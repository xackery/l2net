using System;
using System.Collections.Generic;
using System.Text;

namespace L2_login
{
    public partial class L2NET
    {
        delegate void HTMLWindow_Create_Callback(string ID);
        public void HTMLWindow_Create(string ID)
        {
            if (this.label_target_name.InvokeRequired)
            {
                HTMLWindow_Create_Callback d = new HTMLWindow_Create_Callback(HTMLWindow_Create);
                label_target_name.Invoke(d, new object[] { ID });
                return;
            }

            ScriptWindows.Add(ID, new ScriptWindow_HTML());
        }

        delegate void HTMLWindow_Show_Callback(string ID);
        public void HTMLWindow_Show(string ID)
        {
            if (this.label_target_name.InvokeRequired)
            {
                HTMLWindow_Show_Callback d = new HTMLWindow_Show_Callback(HTMLWindow_Show);
                label_target_name.Invoke(d, new object[] { ID });
                return;
            }

            ((ScriptWindow_HTML)ScriptWindows[ID]).BringToFront();
            ((ScriptWindow_HTML)ScriptWindows[ID]).Show();
        }

        delegate void HTMLWindow_Close_Callback(string ID);
        public void HTMLWindow_Close(string ID)
        {
            if (this.label_target_name.InvokeRequired)
            {
                HTMLWindow_Close_Callback d = new HTMLWindow_Close_Callback(HTMLWindow_Close);
                label_target_name.Invoke(d, new object[] { ID });
                return;
            }

            ((ScriptWindow_HTML)ScriptWindows[ID]).Close();
        }

        delegate void HTMLWindow_Set_WindowName_Callback(string ID, string name);
        public void HTMLWindow_Set_WindowName(string ID, string name)
        {
            if (this.label_target_name.InvokeRequired)
            {
                HTMLWindow_Set_WindowName_Callback d = new HTMLWindow_Set_WindowName_Callback(HTMLWindow_Set_WindowName);
                label_target_name.Invoke(d, new object[] { ID, name });
                return;
            }

            ((ScriptWindow_HTML)ScriptWindows[ID]).Text = name;
        }

        delegate void HTMLWindow_Set_FileName_Callback(string ID, string name);
        public void HTMLWindow_Set_FileName(string ID, string name)
        {
            if (this.label_target_name.InvokeRequired)
            {
                HTMLWindow_Set_FileName_Callback d = new HTMLWindow_Set_FileName_Callback(HTMLWindow_Set_FileName);
                label_target_name.Invoke(d, new object[] { ID, name });
                return;
            }

            ((ScriptWindow_HTML)ScriptWindows[ID]).SetFileName(name);
        }

        delegate void HTMLWindow_Set_HTML_Callback(string ID, string name);
        public void HTMLWindow_Set_HTML(string ID, string name)
        {
            if (this.label_target_name.InvokeRequired)
            {
                HTMLWindow_Set_HTML_Callback d = new HTMLWindow_Set_HTML_Callback(HTMLWindow_Set_HTML);
                label_target_name.Invoke(d, new object[] { ID, name });
                return;
            }

            ((ScriptWindow_HTML)ScriptWindows[ID]).SetHTMLText(name);
        }

        delegate void HTMLWindow_Refresh_Callback(string ID);
        public void HTMLWindow_Refresh(string ID)
        {
            if (this.label_target_name.InvokeRequired)
            {
                HTMLWindow_Refresh_Callback d = new HTMLWindow_Refresh_Callback(HTMLWindow_Refresh);
                label_target_name.Invoke(d, new object[] { ID });
                return;
            }

            ((ScriptWindow_HTML)ScriptWindows[ID]).RefreshHTML();
        }
    }
}
