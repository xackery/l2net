using System;
using System.Collections.Generic;
using System.Text;

namespace L2_login
{
    class NonBMessageBox
    {
        public string title;
        public string text;
        public long type;

        private System.Threading.Thread bthread;

        public NonBMessageBox(bool back = true)
        {
            bthread = new System.Threading.Thread(new System.Threading.ThreadStart(ShowBox));
            bthread.IsBackground = back;
        }

        public void Show()
        {
            bthread.Start();
        }

        private void ShowBox()
        {
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
    }
}
