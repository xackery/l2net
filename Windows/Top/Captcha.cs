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
    public partial class Captcha : Form
    {
        int i;
        public Captcha()
        {
            InitializeComponent();
            this.Text = "~ Captcha ~ : " + Globals.gamedata.my_char.Name;
        }

        public void Add_Captcha_Image(string name)
        {
            Bitmap myBitmap = new Bitmap(Globals.PATH + "\\crests\\" + name + ".bmp");
            pictureBox_captcha.Size = myBitmap.Size;
            pictureBox_captcha.Image = myBitmap;
            i = 0;
            timer_captcha.Start();

            textBox_captcha.Location = new System.Drawing.Point(12, myBitmap.Height + 18);
            button_send_captcha.Location = new System.Drawing.Point(12, pictureBox_captcha.Height + 18 + textBox_captcha.Height);

        }

        private void button_send_captcha_Click(object sender, EventArgs e)
        {
            string link = Globals.Captcha_HTML1 + textBox_captcha.Text + Globals.Captcha_HTML2;
            ServerPackets.NPC_Chat_Click(link);
            timer_captcha.Stop();
            this.Close();
        }

        private void textBox_captcha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                string link = Globals.Captcha_HTML1 + textBox_captcha.Text + Globals.Captcha_HTML2;
                ServerPackets.NPC_Chat_Click(link);
                timer_captcha.Stop();
                this.Close();
            }
        }

        private void button_send_captcha_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                string link = Globals.Captcha_HTML1 + textBox_captcha.Text + Globals.Captcha_HTML2;
                ServerPackets.NPC_Chat_Click(link);
                timer_captcha.Stop();
                this.Close();
            }
        }

        private void timer_captcha_Tick(object sender, EventArgs e)
        {
            i++;
            this.Text = "~ Captcha ~ : " + Globals.gamedata.my_char.Name + " " + i.ToString() + "s";
        }
    }
}
