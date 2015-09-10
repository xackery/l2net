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
    public partial class ManualGameKey : Form
    {
        public ManualGameKey()
        {
            InitializeComponent();
        }

        private void button_GameKey_Set_Click(object sender, EventArgs e)
        {
            string keyinternal1 = textBox_StaticKey.Text.ToString();
            keyinternal1 = keyinternal1.Replace(" ", "");
            byte [] gk1 = HexString2Bytes(keyinternal1);
            Globals.gamedata.game_key[8] = gk1[0];
            Globals.gamedata.game_key[9] = gk1[1];
            Globals.gamedata.game_key[10] = gk1[2];
            Globals.gamedata.game_key[11] = gk1[3];
            Globals.gamedata.game_key[12] = gk1[4];
            Globals.gamedata.game_key[13] = gk1[5];
            Globals.gamedata.game_key[14] = gk1[6];
            Globals.gamedata.game_key[15] = gk1[7];

            string keyinternal2 = textBox_NewKey.Text.ToString();
            keyinternal2 = keyinternal2.Replace(" ", "");
            byte[] gk2 = HexString2Bytes(keyinternal2);
            Globals.gamedata.game_key[0] = gk2[0];
            Globals.gamedata.game_key[1] = gk2[1];
            Globals.gamedata.game_key[2] = gk2[2];
            Globals.gamedata.game_key[3] = gk2[3];
            Globals.gamedata.game_key[4] = gk2[4];
            Globals.gamedata.game_key[5] = gk2[5];
            Globals.gamedata.game_key[6] = gk2[6];
            Globals.gamedata.game_key[7] = gk2[7];

            Globals.gamedata.crypt_in.setKey(Globals.gamedata.game_key);
            Globals.gamedata.crypt_out.setKey(Globals.gamedata.game_key);
            Globals.gamedata.crypt_clientin.setKey(Globals.gamedata.game_key);
            Globals.gamedata.crypt_clientout.setKey(Globals.gamedata.game_key);

            System.Text.StringBuilder dumpbuilder;
            dumpbuilder = new System.Text.StringBuilder();

            for (int i = 0; i < Globals.gamedata.game_key.Length; i++)
            {
                dumpbuilder.Append(Globals.gamedata.game_key[i].ToString("X2"));
                dumpbuilder.Append(" ");
            }

            Globals.l2net_home.Add_Text("Key set to: " + dumpbuilder.ToString(), Globals.Red, TextType.BOT);


            this.Close();
        }

        private byte[] HexString2Bytes(string hexString)
        {
            //check for null
            if (hexString == null) return null;
            //get length
            int len = hexString.Length;
            if (len % 2 == 1) return null;
            int len_half = len / 2;
            //create a byte array
            byte[] bs = new byte[len_half];
            try
            {
                //convert the hexstring to bytes
                for (int i = 0; i != len_half; i++)
                {
                    bs[i] = (byte)Int32.Parse(hexString.Substring(i * 2, 2), System.Globalization.NumberStyles.HexNumber);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception : " + ex.Message);
            }
            //return the byte array
            return bs;
        }
    }
}
