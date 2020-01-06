using log4net;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Com.Bigdata.Dis.Sdk.DISCommon.Log;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Utils
{
    public class EncryptUtils
    {
        private static ILog logger = LogHelper.GetInstance();

        /**
         * 生成一定位数的安全随机数
         * 
         * @param bytes 随机数位数
         * @return 随机数进行 Hex小写编码的结果
         */
        public static string RandomHex(int bytes)
        {
            return PBKDF2Coder.RandomHex(bytes);
        }

        /**
         * 使用PBKDF2进行不可逆hash转换
         * 
         * @param data 待转换的文本
         * @param hexSalt 小写hex编码的盐值
         * @return 转换后的并经过hex小写编码的结果,HASH长度默认64*8
         */
        public static string PBKDF2encode(string data, string hexSalt)
        {
            return PBKDF2Coder.GenHash(data, hexSalt);
        }

        /**
         * 使用PBKDF2进行不可逆hash转换
         * 
         * @param data 待转换的文本
         * @param hexSalt 小写hex编码的盐值
         * @param length HASH结果长度
         * @return 转换后的并经过hex小写编码的结果
         */
        public static string PBKDF2encode(string data, string hexSalt, int length)
        {
            return PBKDF2Coder.GenHash(data, hexSalt, length);
        }

        /**
         * 生成AES 128位key,并做hex小写编码
         * 
         * @return key hex
         */
        public static string InitAES128Key()
        {
            byte[] key = AESCoder.InitSecretKey();
            return Hex.EncodeHexStr(key);
        }

        /**
         * 对称加密文本，返回经hex小写编码的结果
         * 
         * @param data 待加密文本
         * @param hexKey 小写hex编码的key
         * @param hexIv 小写hex编码的IV
         * @return 加密结果
         * @throws UnsupportedEncodingException
         */
        public static string EncryptAES128(byte[] data, string hexKey, string hexIv)
        {
            byte[] key = Hex.DecodeHex(hexKey.ToCharArray());
            return EncryptAES128(data, key, hexIv);
        }

        /**
         * 对称加密文本，返回经hex小写编码的结果
         * 
         * @param data 待加密文本
         * @param key key
         * @param hexIv 小写hex编码的IV
         * @return 加密结果
         */
        public static string EncryptAES128(byte[] data, byte[] key, string hexIv)
        {
            byte[] encryptData = AESCoder.Encrypt(data, key, Hex.DecodeHex(hexIv.ToCharArray()));
            return Hex.EncodeHexStr(encryptData);
        }

        /**
         * 将hex格式的文本进行对称解密
         * 
         * @param hexData 待加密文本
         * @param hexKey 小写hex编码的key
         * @param hexIv 小写hex编码的IV
         * @return 解密结果
         */
        public static string DecryptAES128(string hexData, string hexKey, string hexIv)
        {
            byte[] key = Hex.DecodeHex(hexKey.ToCharArray());
            return DecryptAES128(hexData, key, hexIv);
        }

        /**
         * 将hex格式的文本进行对称解密
         * 
         * @param hexData 待加密文本
         * @param hexKey 小写hex编码的key
         * @param hexIv 小写hex编码的IV
         * @return 解密结果
         */
        public static string DecryptAES128(string hexData, byte[] key, string hexIv)
        {
            try
            {
                byte[] data = Hex.DecodeHex(hexData.ToCharArray());
                byte[] iv = Hex.DecodeHex(hexIv.ToCharArray());
                byte[] res = AESCoder.Decrypt(data, key, iv);
                return Utils.DecodingString(res);
            }
            catch (Exception e)
            {
                logger.Error("---------------UnsupportedEncoding----------------");
            }
            return null;
        }

        private static string SPLIT_STR = "::";

        public static string Gen(string[] srcKeys, byte[] pwd)
        {
            // 分片算法，建议使用XOR
            StringBuilder srcKey = new StringBuilder();
            foreach (var item in srcKeys)
            {
                srcKey.Append(item);
            }
            string hexSalt = EncryptUtils.RandomHex(8);
            string key = EncryptUtils.PBKDF2encode(srcKey.ToString(), hexSalt, 128);
            string hexIv = EncryptUtils.RandomHex(16);
            //string sK = EncryptUtils.EncryptAES128(pwd, key, hexIv);
            string sK = AESCoder.Encrypt(pwd, key, hexIv);
            string res = hexSalt + SPLIT_STR + hexIv + SPLIT_STR + sK;
            try
            {
                return Hex.EncodeHexStr(Utils.EncodingBytes(res));
            }
            catch (Exception e)
            {
                logger.Error("---------------UnsupportedEncoding----------------");
            }
            return null;
        }

        /**
         * 根秘钥加密
         * 
         * @param srcKey [] 秘钥分片
         * @param pwd 待加密数据
         * @return 加密结果
         * @throws UnsupportedEncodingException 
         */
        public static string Gen(string[] srcKeys, string pwd)
        {
            return Gen(srcKeys, Utils.EncodingBytes(pwd)); 
        }

        /**
         * 根秘钥解密
         * 
         * @param srcKey [] 秘钥分片
         * @param pwd 待解密数据
         * @return 解密结果
         */
        public static byte[] Dec(string[] srcKeys, string ser)
        {
            // 分片算法，建议使用XOR
            StringBuilder srcKey = new StringBuilder();
            foreach (var item in srcKeys)
            {
                srcKey.Append(item);
            }

            string s = "";
            try
            {
                s = Encoding.UTF8.GetString(Hex.DecodeHex(ser.ToCharArray()));
            }
            catch (Exception e)
            {
                logger.Error("---------------UnsupportedEncoding----------------");
            }

            string[] tmps = Regex.Split(s, SPLIT_STR);
            string key = EncryptUtils.PBKDF2encode(srcKey.ToString(), tmps[0], 128);
            string iv = tmps[1];
            //string jkspwd = EncryptUtils.DecryptAES128(tmps[2], key, iv);
            byte[] jkspwd = AESCoder.Decrypt(tmps[2], key, iv);
            return jkspwd;
        }
    }
}
