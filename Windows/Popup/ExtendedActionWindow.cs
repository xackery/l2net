using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace L2_login
{
  public partial class ExtendedActionWindow : Form
  {
    public ExtendedActionWindow()
    {
      InitializeComponent();
    }

    private void button_mailbox_Click(object sender, EventArgs e)
    {
        if (Globals.mailboxwindow == null || Globals.mailboxwindow.IsDisposed == true)
        {
            Globals.mailboxwindow = new MailboxWindow();
            this.Close();
        }
        Globals.mailboxwindow.TopMost = true;
        Globals.mailboxwindow.BringToFront();
        Globals.mailboxwindow.Show();
    }


  }
}
