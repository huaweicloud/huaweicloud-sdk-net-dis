using RestSharp.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Utils
{
    public class Utils
    {
        private static UTF8Encoding utf8Encoding = new UTF8Encoding();
        private static int MAX_FILE_NAME_LENGTH = 128;
        public static byte[] EncodingBytes(String value)
        {
            return value == null ? null : utf8Encoding.GetBytes(value);
        }

        public static string DecodingString(byte[] value)
        {
            return (value == null && value.Length == 0) ? String.Empty : utf8Encoding.GetString(value);
        }

        public static bool IsValidFileName(String fileName)
        {
            // 文件名校验
            if (fileName == null || fileName.Length > MAX_FILE_NAME_LENGTH)
            {
                return false;
            }
            String[] paths = fileName.Split('/');
            for (int i = 0; i < paths.Length - 1; i++)
            {
                if (String.IsNullOrEmpty(paths[i]) || paths[i].Matches("^\\..*$|^.*[\"*<>?|/:\\\\]+.*$|.*\\.$"))
                {
                    return false;
                }
            }
            return !String.IsNullOrEmpty(paths[paths.Length - 1])
                && !paths[paths.Length - 1].Matches("^.*[\"*<>?|/:\\\\]+.*$");
        }

        /// <summary>        
        /// 获取时间戳       
        /// </summary>    
        /// <returns></returns>   
        public static long GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return (long)ts.TotalSeconds;
        }


        public static bool IsRetriableSendException(int statusCode)
        {
            // 对于连接超时/网络闪断/Socket异常/服务端5xx错误进行重试
            return (statusCode == (int)HttpStatusCode.RequestTimeout
                        || statusCode == (int)HttpStatusCode.NotFound
                        || (statusCode / 100 == 5)
                        || statusCode == 0);
        }

        public static bool IsCacheData(int statusCode)
        {
            // 对于连接超时/网络闪断/Socket异常/服务端5xx错误进行重试
            return (statusCode == (int)HttpStatusCode.RequestTimeout
                        || statusCode == (int)HttpStatusCode.NotFound
                        || (statusCode / 100 == 5)
                        || statusCode == 0);
        }
    }
}
