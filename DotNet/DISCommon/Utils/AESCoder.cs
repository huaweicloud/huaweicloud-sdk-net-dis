using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Utils
{
    public class AESCoder
    {

        /**
         * 密钥算法
         */
        private static String KEY_ALGORITHM = "AES";

        private static String DEFAULT_CIPHER_ALGORITHM = "AES/CBC/PKCS5Padding";

        public static byte[] InitSecretKey()
        {
            // 生成一个密钥
            AesCryptoServiceProvider aesProvider = new AesCryptoServiceProvider();
            aesProvider.GenerateKey();

            return GetAesKey(Utils.DecodingString(aesProvider.Key));
        }




        private static byte[] GetAesKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key", "Aes秘钥不能为空");
            }
            if (key.Length < 32)
            {
                key = key.PadRight(32, '0');
            }
            if (key.Length > 32)
            {
                key = key.Substring(0, 32);
            }
            return Hex.DecodeHex(key.ToCharArray());
        }



        public static string Encrypt(byte[] toEncrypt, string key, string iv)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] ivArray = Hex.DecodeHex(iv.ToCharArray());

            using (RijndaelManaged rDel = new RijndaelManaged())
            {
                rDel.Key = keyArray;
                rDel.IV = ivArray;
                rDel.Mode = CipherMode.CBC;
                rDel.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform cryptoTransform = rDel.CreateEncryptor())
                {
                    byte[] inputBuffer = toEncrypt;
                    byte[] result = cryptoTransform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
                    return Convert.ToBase64String(result, 0, result.Length);
                }
            }
        }

        public static byte[] Encrypt(byte[] source, byte[] key, byte[] hexIv)
        {
            using (RijndaelManaged rDel = new RijndaelManaged())
            {
                rDel.Key = key;
                rDel.IV = hexIv;
                rDel.Mode = CipherMode.CBC;
                rDel.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform cryptoTransform = rDel.CreateEncryptor())
                {
                    byte[] inputBuffer = source;
                    byte[] result = cryptoTransform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
                    //return Convert.ToBase64String(result, 0, result.Length);
                    return result;
                }
            }
        }

        public static byte[] Decrypt(string toDecrypt, string key, string iv)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] ivArray = Hex.DecodeHex(iv.ToCharArray());
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
            using (RijndaelManaged rDel = new RijndaelManaged())
            {
                rDel.Key = keyArray;
                rDel.IV = ivArray;
                rDel.Mode = CipherMode.CBC;
                rDel.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform cryptoTransform = rDel.CreateDecryptor())
                {
                    byte[] inputBuffer = toEncryptArray;
                    byte[] result = cryptoTransform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
                    //return UTF8Encoding.UTF8.GetString(result);
                    return result;
                }
            }
        }

        public static byte[] Decrypt(byte[] source, byte[] key, byte[] hexIv)
        {
            using (RijndaelManaged rDel = new RijndaelManaged())
            {
                rDel.Key = key;
                rDel.IV = hexIv;
                rDel.Mode = CipherMode.CBC;
                rDel.Padding = PaddingMode.PKCS7;
                using (ICryptoTransform cryptoTransform = rDel.CreateDecryptor())
                {
                    byte[] inputBuffer = source;
                    byte[] result = cryptoTransform.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
                    //return Utils.DecodingString(result); 
                    return result;
                }
            }
        }


        public static string Hex_2To_16(byte[] bytes)
        {
            string hexString = String.Empty;
            Int32 iLength = 65535;
            if (bytes != null)
            {
                StringBuilder sb = new StringBuilder();
                if (bytes.Length < iLength)
                {
                    iLength = bytes.Length;
                }
                for (int i = 0; i < iLength; i++)
                {
                    sb.Append(bytes[i].ToString("X2"));
                }
                hexString = sb.ToString();
            }

            return hexString;
        }

        public static byte[] Hex_16To_2(string hexString)
        {
            if ((hexString.Length % 2) != 0)
            {
                hexString += " ";
            }

            byte[] returnBytes = new byte[hexString.Length / 2];

            for (int i = 0; i < returnBytes.Length; i++)
            {
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }
            return returnBytes;
        }
    }
}