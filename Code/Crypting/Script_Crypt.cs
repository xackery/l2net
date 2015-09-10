using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace L2_login
{
    public static class Script_Crypt
    {
        private static string encrypt_key0 = "m6V9JkQm4mRy7q18";
        private static string encrypt_key1 = "7wplfkqPi00Zn10A";
        private static string encrypt_key2 = "J3oGUgvkNWqNFyvZ";
        private static string encrypt_key3 = "jfnO9eVxmI1Xd0F6";
        private static string encrypt_key4 = "9l8p0hor3Iou4Acn";
        private static string encrypt_key5 = "Tl9sBEkrODBMGQt9";

        public static void Encrypt(string source, string output)
        {
            StreamWriter outf = new System.IO.StreamWriter(output);

            BinaryReader b_read = new BinaryReader(new StreamReader(source).BaseStream);

            byte[] data = b_read.ReadBytes((int)b_read.BaseStream.Length);
            b_read.Close();

            byte[] size = System.BitConverter.GetBytes(data.Length);
            //zip it first
            MemoryStream ms = new MemoryStream();
            // Use the newly created memory stream for the compressed data.
            DeflateStream compressedzipStream = new DeflateStream(ms, CompressionMode.Compress, true);
            //Console.WriteLine("Compression");
            compressedzipStream.Write(data, 0, data.Length);
            // Close the stream.
            compressedzipStream.Close();
            ms.Position = 0;
            data = new byte[ms.Length + 44];
            ms.Read(data, 44, (int)ms.Length);

            for (int i = 0; i < 40; i++)
            {
                data[i] = (byte)Globals.Rando.Next(0, 255);
            }

            data[40] = size[0];
            data[41] = size[1];
            data[42] = size[2];
            data[43] = size[3];

            int key = Globals.Rando.Next(0, 1000000000);

            byte[] enc = AES.EncryptData(data, GetTrueKey(key), key.ToString() + "P8xvDLzPHvNiwVMkS3kPzQStAEDqdTMD", System.Security.Cryptography.PaddingMode.ISO10126);

            outf.WriteLine("ENCRYPTED " + key.ToString());
            outf.WriteLine(System.Convert.ToBase64String(enc));
            outf.Close();
        }

        public static StreamReader Decrypt(string source, int key)
        {
            byte[] data = System.Convert.FromBase64String(source);

            byte[] dec = AES.DecryptData(data, GetTrueKey(key), key.ToString() + "P8xvDLzPHvNiwVMkS3kPzQStAEDqdTMD", System.Security.Cryptography.PaddingMode.ISO10126);

            int d_len = System.BitConverter.ToInt32(dec, 40);

            MemoryStream ms = new MemoryStream(dec);
            ms.Position = 44;
            // Use the newly created memory stream for the compressed data.
            DeflateStream compressedzipStream = new DeflateStream(ms, CompressionMode.Decompress, true);
            //Console.WriteLine("Compression");
            byte[] zdec = new byte[d_len];
            int cnt = compressedzipStream.Read(zdec, 0, d_len);
            // Close the stream.
            compressedzipStream.Close();

            dec = new byte[cnt];
            System.Array.ConstrainedCopy(zdec, 0, dec, 0, cnt);

            MemoryStream mem_stream = new System.IO.MemoryStream(dec);
            StreamReader output = new System.IO.StreamReader((System.IO.Stream)mem_stream);

            return output;
        }

        private static string GetTrueKey(int key)
        {
            int true_key = key % 6;

            switch (true_key)
            {
                case 0:
                    return encrypt_key0;
                case 1:
                    return encrypt_key1;
                case 2:
                    return encrypt_key2;
                case 3:
                    return encrypt_key3;
                case 4:
                    return encrypt_key4;
                case 5:
                    return encrypt_key5;
            }

            return "";
        }
    }
}
