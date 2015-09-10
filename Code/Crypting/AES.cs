using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.IO.Compression;

namespace L2_login
{
    public class AES
    {
        private static RijndaelManaged rm = new RijndaelManaged();

        /// <summary>
        /// Use AES to encrypt data string. The output string is the encrypted bytes as a base64 string.
        /// The same password must be used to decrypt the string.
        /// </summary>
        /// <param name="data">Clear string to encrypt.</param>
        /// <param name="password">Password used to encrypt the string.</param>
        /// <returns>Encrypted result as Base64 string.</returns>
        public static string EncryptData(string data, string password, string salt)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (password == null)
                throw new ArgumentNullException("password");
            if (salt == null)
                throw new ArgumentNullException("salt");
            byte[] encBytes = EncryptData(Encoding.UTF8.GetBytes(data), password, salt, PaddingMode.ISO10126);
            return Convert.ToBase64String(encBytes);
        }
        /// <summary>
        /// Decrypt the data string to the original string.  The data must be the base64 string
        /// returned from the EncryptData method.
        /// </summary>
        /// <param name="data">Encrypted data generated from EncryptData method.</param>
        /// <param name="password">Password used to decrypt the string.</param>
        /// <returns>Decrypted string.</returns>
        public static string DecryptData(string data, string password, string salt)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            if (password == null)
                throw new ArgumentNullException("password");
            if (salt == null)
                throw new ArgumentNullException("salt");
            byte[] encBytes = Convert.FromBase64String(data);
            byte[] decBytes = DecryptData(encBytes, password, salt,  PaddingMode.ISO10126);
            return Encoding.UTF8.GetString(decBytes);
        }

        public static byte[] EncryptData(byte[] data, string password, string salt, PaddingMode paddingMode)
        {
            if (data == null || data.Length == 0)
                throw new ArgumentNullException("data");
            if (password == null)
                throw new ArgumentNullException("password");
            if (salt == null)
                throw new ArgumentNullException("salt");
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, Encoding.UTF8.GetBytes(salt));
            rm.Padding = paddingMode;
            ICryptoTransform encryptor = rm.CreateEncryptor(pdb.GetBytes(16), pdb.GetBytes(16));
            using (MemoryStream msEncrypt = new MemoryStream())
            using (CryptoStream encStream = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            {
                encStream.Write(data, 0, data.Length);
                encStream.FlushFinalBlock();
                return msEncrypt.ToArray();
            }
        }

        public static byte[] DecryptData(byte[] data, string password, string salt, PaddingMode paddingMode)
        {
            if (data == null || data.Length == 0)
                throw new ArgumentNullException("data");
            if (password == null)
                throw new ArgumentNullException("password");
            if (salt == null)
                throw new ArgumentNullException("salt");
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(password, Encoding.UTF8.GetBytes(salt));
            rm.Padding = paddingMode;
            ICryptoTransform decryptor = rm.CreateDecryptor(pdb.GetBytes(16), pdb.GetBytes(16));
            using (MemoryStream msDecrypt = new MemoryStream(data))
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            {
                // Decrypted bytes will always be less then encrypted bytes, so len of encrypted data will be big enouph for buffer.
                byte[] fromEncrypt = new byte[data.Length];
                // Read as many bytes as possible.
                int read = csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);
                if (read < fromEncrypt.Length)
                {
                    // Return a byte array of proper size.
                    byte[] clearBytes = new byte[read];
                    Buffer.BlockCopy(fromEncrypt, 0, clearBytes, 0, read);
                    return clearBytes;
                }
                return fromEncrypt;
            }
        }

        public static byte[] Decrypt(string filename, string key, string salt)
        {
            System.IO.BinaryReader filein = new System.IO.BinaryReader(new System.IO.StreamReader(filename).BaseStream);

            byte[] data = filein.ReadBytes((int)filein.BaseStream.Length);
            filein.Close();

            try
            {
                return Decrypt(data, key, salt);
            }
            catch (Exception e)
            {
                string err;

                try
                {
                    err = e.Message + Environment.NewLine + e.InnerException.Message;
                }
                catch
                {
                    err = e.Message;
                }

                Globals.l2net_home.Add_PopUpError("failed to decrypt '" + filename + "' file data" + Environment.NewLine + err);
                throw e;
            }
        }

        public static byte[] Decrypt(byte[] data, string key, string salt)
        {
            byte[] dec;

            try
            {
                dec = AES.DecryptData(data, key, salt, System.Security.Cryptography.PaddingMode.ISO10126);
            }
            catch (Exception e)
            {
                throw e;
            }

            int d_len = System.BitConverter.ToInt32(dec, 0);

            System.IO.MemoryStream ms = new System.IO.MemoryStream(dec);
            ms.Position = 4;

            // Use the newly created memory stream for the compressed data.
            DeflateStream compressedzipStream = new DeflateStream(ms, CompressionMode.Decompress, true);
            //Console.WriteLine("Compression");
            byte[] zdec = new byte[d_len];
            int cnt = compressedzipStream.Read(zdec, 0, d_len);
            // Close the stream.
            compressedzipStream.Close();
            ms.Close();

            dec = new byte[cnt];
            System.Array.ConstrainedCopy(zdec, 0, dec, 0, cnt);

            zdec = null;

            return dec;
        }
    }//end of class
}
