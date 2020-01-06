using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Utils
{
    public class Sha256Hash
    {
        public static string Hash(String s)
        {
            HashAlgorithm Hasher = new SHA256CryptoServiceProvider();
            byte[] strBytes = Encoding.UTF8.GetBytes(s);
            byte[] strHash = Hasher.ComputeHash(strBytes);

            return BitConverter.ToString(strHash).Replace("-", "").ToLowerInvariant().Trim();
        }

        public static string Hash(byte[] strBytes)
        {
            HashAlgorithm Hasher = new SHA256CryptoServiceProvider();
            byte[] strHash = Hasher.ComputeHash(strBytes);

            return BitConverter.ToString(strHash).Replace("-", "").ToLowerInvariant().Trim();
        }
    }
}
