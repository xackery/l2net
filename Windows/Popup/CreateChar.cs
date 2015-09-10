using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace L2_login
{
    public partial class CreateChar : Form
    {
        public CreateChar()
        {
            InitializeComponent();

            comboBox_race.SelectedIndex = 0;
            comboBox_sex.SelectedIndex = 0;
            comboBox_type.SelectedIndex = 0;
            comboBox_hairstyle.SelectedIndex = 0;
            comboBox_haircolor.SelectedIndex = 0;
            comboBox_face.SelectedIndex = 0;
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_create_Click(object sender, EventArgs e)
        {
            ByteBuffer bb = new ByteBuffer(1);
            bb.WriteByte((byte)PClient.NewCharacter);
            Globals.gamedata.SendToGameServer(bb);
            System.Threading.Thread.Sleep(100);

            bb = new ByteBuffer();
            bb.WriteByte((byte)PClient.CharacterCreate);
            bb.WriteString(textBox_name.Text);
            bb.WriteInt32(comboBox_race.SelectedIndex);

            //race based sex limit
            /*if (comboBox_race.SelectedIndex == 4)
            {
                bb.WriteInt32(0);
            }
            else
            {*/
                bb.WriteInt32(comboBox_sex.SelectedIndex);
            //}

            //race/sex based class restrictions
            switch(comboBox_race.SelectedIndex)
            {
                case 0://human
                    if (comboBox_type.SelectedIndex == 0)
                        bb.WriteInt32(0);
                    else
                        bb.WriteInt32(10);
                    break;
                case 1://elf
                    if (comboBox_type.SelectedIndex == 0)
                        bb.WriteInt32(18);
                    else
                        bb.WriteInt32(25);
                    break;
                case 2://dark elf
                    if (comboBox_type.SelectedIndex == 0)
                        bb.WriteInt32(31);
                    else
                        bb.WriteInt32(38);
                    break;
                case 3://orc
                    if (comboBox_type.SelectedIndex == 0)
                        bb.WriteInt32(44);
                    else
                        bb.WriteInt32(49);
                    break;
                case 4://dwarf
                    bb.WriteInt32(53);
                    break;
                case 5://kamael
                    bb.WriteInt32(123);
                    break;
            }

            bb.WriteInt32(0x1C);
            bb.WriteInt32(0x27);
            bb.WriteInt32(0x1E);
            bb.WriteInt32(0x1B);
            bb.WriteInt32(0x23);
            bb.WriteInt32(0x0B);

            //sex based hair style restriction
            if (comboBox_sex.SelectedIndex == 0)
            {
                if (comboBox_hairstyle.SelectedIndex == 5 || comboBox_hairstyle.SelectedIndex == 6)
                    comboBox_hairstyle.SelectedIndex = 0;
            }
            bb.WriteInt32(comboBox_hairstyle.SelectedIndex);

            //race based hair color restriction
            if (comboBox_race.SelectedIndex != 5 && comboBox_haircolor.SelectedIndex == 4)
                comboBox_haircolor.SelectedIndex = 0;
            bb.WriteInt32(comboBox_haircolor.SelectedIndex);

            //no restrictions on face
            bb.WriteInt32(comboBox_face.SelectedIndex);

            bb.TrimToIndex();
            Globals.gamedata.SendToGameServer(bb);
            System.Threading.Thread.Sleep(100);

            bb = new ByteBuffer(3);
            bb.WriteByte((byte)PClient.EXPacket);
            if (Globals.gamedata.Chron == Chronicle.CT2_6)
            {
                bb.WriteByte(0x36);
            }
            else
            {
                bb.WriteByte((byte)PClientEX.CreateCharConfirm);
            }
            bb.WriteByte(0x00);
            Globals.gamedata.SendToGameServer(bb);
            System.Threading.Thread.Sleep(100);

            this.Close();
        }
    }
}
