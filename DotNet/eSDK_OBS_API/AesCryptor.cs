using eSDK_OBS_API.OBS.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace OBS.S3
{
    public class AesCryptor
    {
        private const int aes_encrypt_length = 128;
        private const int iv_length = 16;
        private const int block_size = 128;

        private static byte[] s_iv = null;
        private static string s_cryptkey = null;

        /// <summary>
        /// encrypt the text by AES function
        /// </summary>
        /// <param name="text">the text that will be encrypted</param>
        /// <returns>encrypted text</returns>
        public static string Encrypt(string text)
        {
            string cryptText = null;

            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            // clear the key and iv
            s_cryptkey = null;
            s_iv = null;

            try
            {
                RijndaelManaged rijndaelCipher = new RijndaelManaged();
                rijndaelCipher.Mode = CipherMode.CBC;
                rijndaelCipher.Padding = PaddingMode.PKCS7;
                rijndaelCipher.KeySize = aes_encrypt_length;
                rijndaelCipher.BlockSize = block_size;
                rijndaelCipher.Key = CreateKey(); ;
                rijndaelCipher.IV = CreateIv(); ;

                ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(text);
                byte[] cipherBytes = transform.TransformFinalBlock(plainText, 0, plainText.Length);

                // save work_key
                s_cryptkey = Convert.ToBase64String(Protect(rijndaelCipher.Key));
                s_iv = rijndaelCipher.IV;

                cryptText = Convert.ToBase64String(cipherBytes);
            }
            catch (System.Exception ex)
            {
                LoggerMgr.Log_Run_Error(OBS.S3.ObsClient.strProduct, string.Format("Encrypt the text exception: {0}", ex.Message));
            }

            return cryptText;
        }

        /// <summary>
        /// decrypt data
        /// </summary>
        /// <param name="text">the text that has been encrypted</param>
        /// <returns>decrypted text</returns>
        public static string Decrypt(string text)
        {
            byte[] plainText = null;

            if (string.IsNullOrEmpty(s_cryptkey) || s_iv == null || string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            //read key adn iv
            byte[] work_key = Unprotect(Convert.FromBase64String(s_cryptkey));

            try
            {
                RijndaelManaged rijndaelCipher = new RijndaelManaged();

                rijndaelCipher.Mode = CipherMode.CBC;
                rijndaelCipher.Padding = PaddingMode.PKCS7;
                rijndaelCipher.KeySize = aes_encrypt_length;
                rijndaelCipher.BlockSize = aes_encrypt_length;
                rijndaelCipher.Key = work_key;
                rijndaelCipher.IV = s_iv;


                byte[] encryptedData = Convert.FromBase64String(text);

                ICryptoTransform transform = rijndaelCipher.CreateDecryptor();

                plainText = transform.TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            }
            catch (System.Exception ex)
            {
                LoggerMgr.Log_Run_Error(OBS.S3.ObsClient.strProduct, string.Format("Decrypt the text exception: {0}", ex.Message));
            }

            return Encoding.UTF8.GetString(plainText);
        }

        private static byte[] CreateIv()
        {
            return GetRandomString(iv_length);
        }

        private static byte[] CreateKey()
        {
            return GetRandomString(aes_encrypt_length / 8);
        }

        private static byte[] GetRandomString(int size)
        {
            byte[] randomNumber = new byte[size];
            //using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
            //{
                RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider();
            // Fill the array with a random value.
            rngCsp.GetBytes(randomNumber);
            //}

            return randomNumber;
        }

        //ProtectedData.Protect
        private static byte[] Protect(byte[] data)
        {
            try
            {
                // Encrypt the data using DataProtectionScope.CurrentUser. The result can be decrypted only by the same current user.
                return ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);
            }
            catch (CryptographicException e)
            {
                LoggerMgr.Log_Run_Error(OBS.S3.ObsClient.strProduct, string.Format("Protect the data exception: {0}", e.Message));
                return null;
            }
        }

        //ProtectedData.Unprotect
        private static byte[] Unprotect(byte[] data)
        {
            try
            {
                //Decrypt the data using DataProtectionScope.CurrentUser.
                return ProtectedData.Unprotect(data, null, DataProtectionScope.CurrentUser);
            }
            catch (CryptographicException e)
            {
                LoggerMgr.Log_Run_Error(OBS.S3.ObsClient.strProduct, string.Format("Unprotect the data exception: {0}", e.Message));
                return null;
            }
        }
    }
}
