using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Com.Bigdata.Dis.Sdk.DISCommon.Log;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Utils
{
    public class CompressUtils
    {
        private static ILog logger = LogHelper.GetInstance();

        ///// <summary>  
        ///// 字节数组压缩  
        ///// </summary>  
        ///// <param name="data"></param>  
        ///// <returns></returns>  
        //public static byte[] Compress(byte[] data)
        //{
        //    if (data == null)
        //    {
        //        throw new ArgumentException();
        //    }

        //    try
        //    {
        //        byte[] buffer = null;
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true))
        //            {
        //                zip.Write(data, 0, data.Length);
        //            }
        //            buffer = new byte[ms.Length];
        //            ms.Position = 0;
        //            ms.Read(buffer, 0, buffer.Length);
        //        }
        //        return buffer;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}
        ///// <summary>  
        ///// 字节数组解压缩  
        ///// </summary>  
        ///// <param name="data"></param>  
        ///// <returns></returns>  
        //public static byte[] Decompress(byte[] data)
        //{
        //    if (data == null)
        //    {
        //        throw new ArgumentException();
        //    }

        //    try
        //    {
        //        byte[] buffer = new byte[0x1000];
        //        using (MemoryStream msreader = new MemoryStream())
        //        {
        //            using (MemoryStream ms = new MemoryStream(data))
        //            {
        //                using (GZipStream zip = new GZipStream(ms, CompressionMode.Decompress, true))
        //                {
        //                    while (true)
        //                    {
        //                        int reader = zip.Read(buffer, 0, buffer.Length);
        //                        if (reader <= 0)
        //                        {
        //                            break;
        //                        }
        //                        msreader.Write(buffer, 0, reader);
        //                    }
        //                }
        //            }
        //            msreader.Position = 0;
        //            buffer = msreader.ToArray();
        //        }
        //        return buffer;
        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

        /// <summary>
        /// 字符串压缩
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] data)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true);
                zip.Write(data, 0, data.Length);
                zip.Close();
                byte[] buffer = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(buffer, 0, buffer.Length);
                ms.Close();
                return buffer;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 字符串解压缩
        /// </summary>
        /// <param name="strSource"></param>
        /// <returns></returns>
        public static byte[] Decompress(byte[] data)
        {
            try
            {
                MemoryStream ms = new MemoryStream(data);
                GZipStream zip = new GZipStream(ms, CompressionMode.Decompress, true);
                MemoryStream msreader = new MemoryStream();
                byte[] buffer = new byte[0x1000];
                while (true)
                {
                    int reader = zip.Read(buffer, 0, buffer.Length);
                    if (reader <= 0)
                    {
                        break;
                    }
                    msreader.Write(buffer, 0, reader);
                }
                zip.Close();
                ms.Close();
                msreader.Position = 0;
                buffer = msreader.ToArray();
                msreader.Close();
                return buffer;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        /// <summary>
        /// 字符串压缩
        /// </summary>
        /// <returns>The string.</returns>
        /// <param name="str">String.</param>
        public static string CompressString(string str)
        {
            string compressString = "";
            byte[] compressBeforeByte = Encoding.UTF8.GetBytes(str);
            byte[] compressAfterByte = Compress(compressBeforeByte);
            //compressString = Encoding.GetEncoding("UTF-8").GetString(compressAfterByte);    
            compressString = Convert.ToBase64String(compressAfterByte);
            return compressString;
        }
        /// <summary>
        /// 字符串解压缩
        /// </summary>
        /// <returns>The string.</returns>
        /// <param name="str">String.</param>
        public static string DecompressString(string str)
        {
            string compressString = "";
            //byte[] compressBeforeByte = Encoding.GetEncoding("UTF-8").GetBytes(str);    
            byte[] compressBeforeByte = Convert.FromBase64String(str);
            byte[] compressAfterByte = Decompress(compressBeforeByte);
            compressString = Encoding.UTF8.GetString(compressAfterByte);
            return compressString;
        }

    }
}
