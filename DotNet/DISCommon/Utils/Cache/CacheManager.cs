using Com.Bigdata.Dis.Sdk.DISCommon.Config;
using Com.Bigdata.Dis.Sdk.DISCommon.Log;
using Com.Bigdata.Dis.Sdk.DISCommon.Model;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Utils.Cache
{
    /// <summary>
    /// 本地缓存管理
    /// </summary>
    public class CacheManager : ICacheManager<PutRecordsRequest>
    {
        private static ILog LOGGER = LogHelper.GetInstance();

        private static String CACHE_FILE_PREFIX = "dis-cache-data-";

        //缓存临时文件后缀
        private static String CACHE_TMP_FILE_SUFFIX = ".tmp";

        //缓存归档数据文件后缀
        private static String CACHE_ARCHIVE_DATA_FILE_SUFFIX = ".data";



        //缓存归档索引文件后缀
        private static String CACHE_ARCHIVE_INDEX_FILE_SUFFIX = ".index";

        private static CacheManager instance;

        private DISConfig disConfig;

        // 临时缓存数据文件名
        private string dataTmpFileName;

        // 临时缓存数据文件对象
        private FileInfo dataTmpFile;

        public FileInfo GetDataTmpFile()
        {
            SetDataTmpFile();
            return dataTmpFile;
        }

        public void SetDataTmpFile()
        {
            dataTmpFile = new FileInfo(dataTmpFileName);
        }

        // 临时缓存文件创建时间
        private long tmpFileCreateTime;

        private CacheManager()
        {
        }

        private CacheManager(DISConfig disConfig)
        {
            this.disConfig = disConfig;
        }

        private static readonly object syncLock = new object();
        public static CacheManager GetInstance(DISConfig disConfig)
        {
            lock (syncLock)
            {
                if (instance == null)
                {
                    instance = new CacheManager(disConfig);
                    instance.Init();
                }
                return instance;
            }
        }

        private static readonly object Lock = new object();

        public void PutToCache(PutRecordsRequest putRecordsRequest)
        {
            LOGGER.InfoFormat("Put records to cache, record size: {0}, cache dir: {1}.",
                putRecordsRequest.Records.Count,
                disConfig.GetDataCacheDir());
            string data = JsonConvert.SerializeObject(putRecordsRequest);

            //synchronized(this)
            lock (this)
            {
                if (NeedToArchive(data))
                {
                    // 缓存临时文件归档
                    LOGGER.DebugFormat("Need to archive cache tmp file, filename: '{0}'.", dataTmpFileName);
                    Archive();
                }

                if (HasEnoughSpace(data))
                {
                    WriteToFile(data);
                }
                else
                {
                    LOGGER.ErrorFormat("Put to cache failed, cache space is not enough, configured max dir size: {0}.",
                        disConfig.GetDataCacheDiskMaxSize());
                }
            }
        }


        public bool HasEnoughSpace(String data)
        {
            long dataSize = GetDataSize(data);
            if ((dataSize + FileSize(GetCacheDir())) / 1.0 / 1024 / 1024  > disConfig.GetDataCacheDiskMaxSize())
            {
                return false;
            }

            return true;
        }

        private bool NeedToArchive(String data)
        {
            long dataSize = GetDataSize(data);
            if (((dataSize + GetDataTmpFile().Length) / 1.0 / 1024 / 1024 > disConfig.GetDataCacheArchiveMaxSize())
                || (Utils.GetTimeStamp() - tmpFileCreateTime > disConfig.GetDataCacheArchiveLifeCycle() * 1000))
            {
                return true;
            }

            return false;
        }

        private long GetDataSize(String data)
        {
            long dataSize = 0;
            try
            {
                dataSize = Encoding.UTF8.GetBytes(data).Length;
            }
            catch (Exception)
            {
                LOGGER.Error("Failed to calculate data size.");
                throw;
            }

            return dataSize;
        }

        /// <summary>
        /// 临时缓存文件归档
        /// </summary>
        private void Archive()
        {
            if (dataTmpFile == null || !dataTmpFile.Exists)
            {
                LOGGER.ErrorFormat("Tmp cache file not exist, filename: '{0}'.", dataTmpFileName);
                return;
            }

            string archiveDataFilename = dataTmpFileName.Replace(CACHE_TMP_FILE_SUFFIX, "") + CACHE_ARCHIVE_DATA_FILE_SUFFIX;
            string archiveIndexFilename = dataTmpFileName.Replace(CACHE_TMP_FILE_SUFFIX, "") + CACHE_ARCHIVE_INDEX_FILE_SUFFIX;
            FileInfo archiveDataFile = new FileInfo(archiveDataFilename);
            FileInfo archiveIndexFile = new FileInfo(archiveIndexFilename);
            try
            {
                File.Move(dataTmpFile.FullName, archiveDataFile.FullName);
                FileStream fs = archiveIndexFile.Create();
                fs.Close();
            }
            catch (IOException e)
            {
                LOGGER.ErrorFormat("Failed to create archive files.", e);

                archiveDataFile.Delete();
                archiveIndexFile.Delete();
                return;
            }

            // 重置缓存临时文件
            Reset();
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            long timestamp = Utils.GetTimeStamp();
            string cacheTmpDataFileName = CACHE_FILE_PREFIX + timestamp + CACHE_TMP_FILE_SUFFIX;

            LOGGER.DebugFormat("Cache tmp data file name: '{0}'.", cacheTmpDataFileName);
            if (dataTmpFile == null || !dataTmpFile.Exists)
            {
                try
                {
                    // 生成缓存数据文件和缓存索引文件
                    dataTmpFileName = GetCacheDir() + Path.DirectorySeparatorChar + cacheTmpDataFileName;
                    dataTmpFile = new FileInfo(dataTmpFileName);
                    FileStream fs = dataTmpFile.Create();
                    fs.Close();
                    tmpFileCreateTime = timestamp;
                }
                catch (IOException e)
                {
                    LOGGER.ErrorFormat("Failed to create cache tmp file.", e);
                    throw;
                }
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        private void Reset()
        {
            dataTmpFile = null;
            dataTmpFileName = null;
        }


        /// <summary>
        /// 获取配置的缓存目录路径
        /// </summary>
        /// <returns>存放缓存文件的目录</returns>
        private string GetCacheDir()
        {
            try
            {
                string dataCacheDir = disConfig.GetDataCacheDir();
                DirectoryInfo directoryInfo = new DirectoryInfo(dataCacheDir);
                if (!Directory.Exists(dataCacheDir))
                {
                    directoryInfo.Create();
                }
                return directoryInfo.FullName; 
            }
            catch (IOException e)
            {
                LOGGER.ErrorFormat("Invalid cache dir: {0}.", disConfig.GetDataCacheDir());
                throw;
            }
        }

        /// <summary>
        /// 写入缓存文件
        /// </summary>
        /// <param name="data">待写入缓存文件的数据</param>
        private void WriteToFile(String data)
        {
            try
            {
                // 追加写 
                using (FileStream fs = File.Open(dataTmpFileName, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(data + Environment.NewLine);
                        sw.Flush();
                    }
                }
            }
            catch (IOException e)
            {
                LOGGER.ErrorFormat("Failed to write cache file.");
                throw;
            }
        }

        public long FileSize(string filePath)
        {
            long temp = 0;

            //判断当前路径所指向的是否为文件
            if (File.Exists(filePath) == false)
            {
                string[] str1 = Directory.GetFileSystemEntries(filePath);
                foreach (string s1 in str1)
                {
                    temp += FileSize(s1);
                }
            }
            else
            {

                //定义一个FileInfo对象,使之与filePath所指向的文件向关联,

                //以获取其大小
                FileInfo fileInfo = new FileInfo(filePath);
                return fileInfo.Length;
            }
            return temp;
        }
    }
}
