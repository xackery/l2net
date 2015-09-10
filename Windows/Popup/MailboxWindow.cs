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
  public partial class MailboxWindow : Form
  {

    public class ReceivedMail {
      public uint msgId;
      public string subject;
      public string senderName;
      public uint isLocked;
      public uint expireSecs;
      public uint isUnread;
      public uint hasAttachs;
      public uint isReturned;
      public uint isSystem;
    }
    public List<ReceivedMail> receivedMails = new List<ReceivedMail>();
    public uint timeSeconds = 0;
    public uint inboxSize = 0;

    public MailboxWindow()
    {
      InitializeComponent();
    }

    private void MailMenuWindow_Load(object sender, EventArgs e)
    {

    }

    private void MailMenuWindow_FormClosed(object sender, EventArgs e)
    {
      listViewReceived.Items.Clear();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      ByteBuffer bbuff = new ByteBuffer(512);
      bbuff.WriteByte((byte)208);
      bbuff.WriteInt16((byte)103);
      bbuff.TrimToIndex();
      Globals.gamedata.SendToGameServer(bbuff);

    }









  }
}
