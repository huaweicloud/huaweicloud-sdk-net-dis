using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Utils
{
    public class BinaryUtils
    {
        public static string ToHex(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 2);
            byte[] var2 = data;
            int var3 = data.Length;

            for (int var4 = 0; var4 < var3; ++var4)
            {
                byte b = var2[var4];
                string hex = b.ToString("X");
                if (hex.Length == 1)
                {
                    sb.Append("0");
                }
                else if (hex.Length == 8)
                {
                    hex = hex.Substring(6);
                }

                sb.Append(hex);
            }

            return sb.ToString().ToLower(CultureInfo.InvariantCulture);
        }
    }
}
