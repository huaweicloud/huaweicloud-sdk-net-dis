using System;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Utils
{
    public class Hex
    {
        private static readonly char[] DIGITS_LOWER =
            {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f'};

        /**
         * 用于建立十六进制字符的输出的大写字符数组
         */
        private static readonly char[] DIGITS_UPPER =
            {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};

        /**
         * 将字节数组转换为十六进制字符数组
         * 
         * @param data byte[]
         * @return 十六进制char[]
         */
        public static char[] EncodeHex(byte[] data)
        {
            return EncodeHex(data, true);
        }

        /**
         * 将字节数组转换为十六进制字符数组
         * 
         * @param data byte[]
         * @param toLowerCase <code>true</code> 传换成小写格式 ， <code>false</code> 传换成大写格式
         * @return 十六进制char[]
         */
        public static char[] EncodeHex(byte[] data, bool toLowerCase)
        {
            return EncodeHex(data, toLowerCase ? DIGITS_LOWER : DIGITS_UPPER);
        }

        /**
         * 将字节数组转换为十六进制字符数组
         * 
         * @param data byte[]
         * @param toDigits 用于控制输出的char[]
         * @return 十六进制char[]
         */
        protected static char[] EncodeHex(byte[] data, char[] toDigits)
        {
            var l = data.Length;
            var output = new char[l << 1];
            // two characters form the hex value.
            for (int i = 0, j = 0; i < l; i++)
            {
                uint value = (uint) (0xF0 & data[i]);
                output[j++] = toDigits[value >> 4];
                output[j++] = toDigits[0x0F & data[i]];
            }
            return output;
        }

        /**
         * 将字节数组转换为十六进制字符串
         * 
         * @param data byte[]
         * @return 十六进制String
         */
        public static string EncodeHexStr(byte[] data)
        {
            return EncodeHexStr(data, true);
        }

        /**
         * 将字节数组转换为十六进制字符串
         * 
         * @param data byte[]
         * @param toLowerCase <code>true</code> 传换成小写格式 ， <code>false</code> 传换成大写格式
         * @return 十六进制String
         */
        public static string EncodeHexStr(byte[] data, bool toLowerCase)
        {
            return EncodeHexStr(data, toLowerCase ? DIGITS_LOWER : DIGITS_UPPER);
        }

        /**
         * 将字节数组转换为十六进制字符串
         * 
         * @param data byte[]
         * @param toDigits 用于控制输出的char[]
         * @return 十六进制String
         */
        protected static string EncodeHexStr(byte[] data, char[] toDigits)
        {
            return new string(EncodeHex(data, toDigits));
        }

        /**
         * 将十六进制字符数组转换为字节数组
         * 
         * @param data 十六进制char[]
         * @return byte[]
         * @throws RuntimeException 如果源十六进制字符数组是一个奇怪的长度，将抛出运行时异常
         */
        public static byte[] DecodeHex(char[] data)
        {

            var len = data.Length;

            if ((len & 0x01) != 0)
            {
                throw new ArgumentException("Odd number of characters.");
            }

            var output = new byte[len >> 1];

            // two characters form the hex value.
            for (int i = 0, j = 0; j < len; i++)
            {
                var f = ToDigit(data[j], j) << 4;
                j++;
                f = f | ToDigit(data[j], j);
                j++;
                output[i] = (byte) (f & 0xFF);
            }

            return output;
        }

        /**
         * 将十六进制字符转换成一个整数
         * 
         * @param ch 十六进制char
         * @param index 十六进制字符在字符数组中的位置
         * @return 一个整数
         * @throws RuntimeException 当ch不是一个合法的十六进制字符时，抛出运行时异常
         */
        protected static int ToDigit(char ch, int index)
        {
            var digit = Convert.ToInt32(ch.ToString(), 16);
            if (digit == -1)
            {
                throw new ArgumentException("Illegal hexadecimal character " + ch + " at index " + index);
            }
            return digit;
        }
    }
}
