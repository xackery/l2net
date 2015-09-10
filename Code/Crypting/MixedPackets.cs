using System;
using System.Collections.Generic;
using System.Text;

namespace L2_login
{
    public class MixedPackets
    {
        private long _seed;
        private byte[] PacketIDs = new byte[256];//209
        private byte[] SuperIDs = new byte[256];//78
        private int PacketIDLimit = 0xFF;
        private int SuperIDLimit = 0xFF;//0x80;//0x4e

        private int PseudoRandom
        {
            get
            {
                long temp;
                temp = _seed * 0x343fd + 0x269ec3 & 0xFFFFFFFF;
                _seed = temp;
                int output = (int)(_seed >> 16) & 0x7FFF;
                return output;
            }
        }

        public MixedPackets(int Seed)
        {
            _seed = Seed;

            int i;

            for (i = 0; i < PacketIDs.Length; i++)
            {
                PacketIDs[i] = (byte)i;
            }
            for (i = 0; i < SuperIDs.Length; i++)
            {
                SuperIDs[i] = (byte)i;
            }

            if (_seed == 0)
            {
                //l2j

                //do nothing... why can't you just leave the ids alone!
            }
            else
            {
                int j;
                byte x;

                for (i = 2; i <= PacketIDLimit; i++)
                {
                    j = PseudoRandom % i;
                    x = PacketIDs[j];
                    PacketIDs[j] = PacketIDs[i - 1];
                    PacketIDs[i - 1] = x;
                }
                for (i = 2; i <= SuperIDLimit; i++)
                {
                    j = PseudoRandom % i;
                    x = SuperIDs[j];
                    SuperIDs[j] = SuperIDs[i - 1];
                    SuperIDs[i - 1] = x;
                }

                j = GetIndexOf(0x12, PacketIDs);
                x = PacketIDs[0x12];
                PacketIDs[0x12] = 0x12;
                PacketIDs[j] = x;

                j = GetIndexOf(0xB1, PacketIDs);
                x = PacketIDs[0xB1];
                PacketIDs[0xB1] = 0xB1;
                PacketIDs[j] = x;
            }
        }

        private static int GetIndexOf(int Value, byte[] IdTable)
        {
            int i;
            for (i = 0; i < IdTable.Length; i++)
            {
                if (IdTable[i] == Value)
                    return i;
            }
            return -1;
        }

        public void Encrypt(byte[] Packet)
        {
            if ((Globals.gamedata.Chron >= Chronicle.CT2_5 && Packet[2] == (byte)PClient.EnterWorld))// || (Globals.gamedata.Chron >= Chronicle.CT3_0 && Packet[2] == (byte)PClient.Action))
            {

            }
            else
            {
                if (Packet[2] == (byte)PClient.EXPacket)
                {
                    Packet[3] = (byte)GetIndexOf(Packet[3], SuperIDs);
                }
                else if (Globals.gamedata.Chron >= Chronicle.CT2_5)//If not expacket and Freya
                {
                    Packet[2] = (byte)GetIndexOf(Packet[2], PacketIDs);
                }
                if (Globals.gamedata.Chron <= Chronicle.CT2_4)
                {
                    Packet[2] = (byte)GetIndexOf(Packet[2], PacketIDs);
                }
            }
        }

        public void Encrypt0(byte[] Packet)
        {
            try
            {
                if ((Globals.gamedata.Chron >= Chronicle.CT2_5 && Packet[0] == (byte)PClient.EnterWorld))// || (Globals.gamedata.Chron >= Chronicle.CT3_0 && Packet[0] == (byte)PClient.Action))
                {

                }
                else
                {
                    if (Packet[0] == (byte)PClient.EXPacket)
                    {
                        Packet[1] = (byte)GetIndexOf(Packet[1], SuperIDs);
                    }
                    else if (Globals.gamedata.Chron >= Chronicle.CT2_5)//freya
                    {
                        Packet[0] = (byte)GetIndexOf(Packet[0], PacketIDs);
                    }

                    if (Globals.gamedata.Chron <= Chronicle.CT2_4)
                    {
                        Packet[0] = (byte)GetIndexOf(Packet[0], PacketIDs);
                    }
                }
            }
            catch
            {
#if DEBUG
                Globals.l2net_home.Add_OnlyDebug("crahsed MixedPackets.Encrypt0 byte[]");
#endif
            }
        }

        public void Encrypt0(ByteBuffer Packet)
        {
            try
            {
                if ((Globals.gamedata.Chron >= Chronicle.CT2_5 && Packet.GetByte(0) == (byte)PClient.EnterWorld))//|| (Globals.gamedata.Chron >= Chronicle.CT3_0 && Packet.GetByte(0) == (byte)PClient.Action))
                {

                }
                else
                {
                    if (Packet.GetByte(0) == (byte)PClient.EXPacket)
                    {
                        Packet.SetByte(1, (byte)GetIndexOf(Packet.GetByte(1), SuperIDs));
                    }
                    else if (Globals.gamedata.Chron >= Chronicle.CT2_5)//freya
                    {
                        Packet.SetByte(0, (byte)GetIndexOf(Packet.GetByte(0), PacketIDs));
                    }
                    
                    if (Globals.gamedata.Chron <= Chronicle.CT2_4)
                    {
                        Packet.SetByte(0, (byte)GetIndexOf(Packet.GetByte(0), PacketIDs));
                    }

                }
            }
            catch
            {
#if DEBUG
                Globals.l2net_home.Add_OnlyDebug("crahsed MixedPackets.Encrypt0 ByteBuffer");
#endif
            }
        }

        public int Encrypt(int id)
        {
            return GetIndexOf(id, PacketIDs);
        }

        public int EncryptSuper(int id)
        {
            return GetIndexOf(id, SuperIDs);
        }

        public void Decrypt(byte[] Packet)
        {
            if ((Globals.gamedata.Chron >= Chronicle.CT2_5 && Packet[2] == (byte)PClient.EnterWorld))// || (Globals.gamedata.Chron >= Chronicle.CT3_0 && Packet[2] == (byte)PClient.Action))
            {

            }
            else
            {
                if (Globals.gamedata.Chron <= Chronicle.CT2_4)
                {
                    Packet[2] = PacketIDs[Packet[2]];
                }
                if (Packet[2] == (byte)PClient.EXPacket)
                {
                    Packet[3] = SuperIDs[Packet[3]];
                }
                else if (Globals.gamedata.Chron >= Chronicle.CT2_5)
                {
                    Packet[2] = PacketIDs[Packet[2]];
                }
            }
        }

        public void Decrypt0(byte[] Packet)
        {
            try
            {
                if ((Globals.gamedata.Chron >= Chronicle.CT2_5 && Packet[0] == (byte)PClient.EnterWorld))// || (Globals.gamedata.Chron >= Chronicle.CT3_0 && Packet[0] == (byte)PClient.Action))
                {

                }
                else
                {
                    if (Globals.gamedata.Chron <= Chronicle.CT2_4)
                    {
                        Packet[0] = PacketIDs[Packet[0]];
                    }
                    if (Packet[0] == (byte)PClient.EXPacket)
                    {
                        Packet[1] = SuperIDs[Packet[1]];
                    }
                    else if (Globals.gamedata.Chron >= Chronicle.CT2_5)
                    {
                        Packet[0] = PacketIDs[Packet[0]];
                    }
                }
            }
            catch
            {
            }
        }

        public int Decrypt(int id)
        {
            return PacketIDs[id];
        }

        public int DecryptSuper(int id)
        {
            return SuperIDs[id];
        }
    }
}
