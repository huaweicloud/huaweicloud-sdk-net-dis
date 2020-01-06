using System;
using System.Security.Cryptography;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Utils
{
    public class PBKDF2Coder
    {
        private static readonly RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider();
        private const int iterations = 1000;

        public static string RandomHex(int bytes)
        {
            var salt = new byte[bytes];
            rngCryptoServiceProvider.GetBytes(salt);
            return ToHex(salt);
        }

        /// <summary>
        /// 加盐哈希
        /// </summary>
        /// <param name="data"></param>
        /// <param name="hexSalt"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenHash(string data, string hexSalt, int length)
        {
            char[] chars = data.ToCharArray();
            byte[] salt = Hex.DecodeHex(hexSalt.ToCharArray());

            byte[] hash = PBKDF2(data, salt, iterations, length);
            return ToHex(hash);
        }

        public static string GenHash(string data, string hexSalt)
        {
            return GenHash(data, hexSalt, 64 * 16);
        }

        private static string ToHex(byte[] array)
        {
            return Hex.EncodeHexStr(array);
        }

        private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
        {
            Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password, salt);
            pbkdf2.IterationCount = iterations;
             var hash = pbkdf2.GetBytes(16);
            return hash;
        }
    }
}