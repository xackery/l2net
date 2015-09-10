using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace L2_login
{
    public partial class ScriptWindow_HTML : Form
    {
        public ScriptWindow_HTML()
        {
            InitializeComponent();
            webBrowser_view.AllowWebBrowserDrop = false;
        }

        public void SetFileName(string name)
        {
            this.webBrowser_view.Navigate(name);
            //this.webBrowser_view.Refresh();
        }

        public void SetHTMLText(string html)
        {
            this.webBrowser_view.DocumentText = html;
            //this.webBrowser_view.Refresh();
        }

        public void RefreshHTML()
        {
            this.webBrowser_view.Refresh();
        }
    }
}
