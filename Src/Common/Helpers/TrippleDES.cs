using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public class TrippleDES
    {
        public static string Decrypt(string cipher, string key)
        {
            try
            {
                var encryptedText = cipher;
                TripleDESCryptoServiceProvider mDes = new TripleDESCryptoServiceProvider();
                mDes.Key = Convert.FromBase64String(key);
                mDes.Mode = CipherMode.ECB;
                mDes.Padding = PaddingMode.Zeros;
                ICryptoTransform mDesEnc = mDes.CreateDecryptor();
                byte[] data = Convert.FromBase64String(encryptedText);
                var plain = Encoding.ASCII.GetString(mDesEnc.TransformFinalBlock(data, 0, data.Length));

                return plain.Replace("\0", "");

            }
            catch (Exception exception)
            {
                return "";
            }
        }
        public static string Encrypt(string clearText, string key)
        {
            try
            {
                var PlainText = clearText;
                TripleDESCryptoServiceProvider mDes = new TripleDESCryptoServiceProvider();
                mDes.Key = Convert.FromBase64String(key);
                mDes.Mode = CipherMode.ECB;
                mDes.Padding = PaddingMode.Zeros;
                ICryptoTransform mDesEnc = mDes.CreateEncryptor();
                byte[] data = Encoding.UTF8.GetBytes(PlainText);
                var crypto = Convert.ToBase64String(mDesEnc.TransformFinalBlock(data, 0, data.Length));
                return crypto;

            }
            catch (Exception exception)
            {
                return "";
            }
        }
    }
}
