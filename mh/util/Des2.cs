using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace mh.util
{
    class Des2
    {
        private static string so = "1234567890abcdefghijklmnopqrstuvwxyzQWERTYUIOPASDFGHJKLZXCVBNM";

        /// <summary>
        /// 生成内容随机key
        /// </summary>
        /// <returns></returns>
        public static string GetContent()
        {
            Random rand = new Random();
            string str = null;
            for (int i = 0; i < 8; i++)
            {
                str += so.Substring(rand.Next(62), 1);
            }
            return str;
        }
        //  private static byte[] Keys = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        private static byte[] Keys = { 0x92, 0x01, 0x00, 0x20, 0x01, 0x00, 0x00, 0x00, 0x00 };
        /// <summary> 
        /// DES加密字符串 
        /// </summary> 
        /// <param name="encryptString">待加密的字符串</param> 
        /// <returns>加密成功返回加密后的字符串，失败返回源串</returns> 
        public static string Encrypt(string encryptString)
        {
            try
            {
                Random rand = new Random();
                int r = rand.Next(9);
                string encryptKey = GetContent();
                string first = encryptKey.Substring(0, r);
                string last = encryptKey.Substring(r);

                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 8));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                DESCryptoServiceProvider dCsp = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dCsp.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                string str = Convert.ToBase64String(mStream.ToArray());
                int i = 0;
                for (int j = str.Length - 1; j > 0; j--)
                {
                    if (str[j] != '=')
                    {
                        break;
                    }
                    else
                    {
                        i++;
                    }
                }
                str = str.Substring(0, str.Length - i);
                return i.ToString() + r.ToString() + first + str + last;
            }
            catch
            {
                return encryptString;
            }
        }
        /// <summary> 
        /// DES解密字符串 
        /// </summary> 
        /// <param name="decryptString">待解密的字符串</param> 
        /// <returns>解密成功返回解密后的字符串，失败返源串</returns> 
        public static string Decrypt(string decryptString)
        {
            try
            {
                string am=decryptString.Substring(1, 1);
                int code = Int32.Parse(am);

                int i = Int32.Parse(decryptString.Substring(0, 1));

                string first = decryptString.Substring(2, code);
                string last = decryptString.Substring(decryptString.Length - 8 + code);
                string encryptKey = first + last;
                decryptString = decryptString.Substring(code + 2, decryptString.Length - 10);
                for (int j = 0; j < i; j++)
                {
                    decryptString += "=";
                }
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey);
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                DESCryptoServiceProvider dcsp = new DESCryptoServiceProvider();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, dcsp.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch
            {
                return decryptString;
            }
        }
    }
}
