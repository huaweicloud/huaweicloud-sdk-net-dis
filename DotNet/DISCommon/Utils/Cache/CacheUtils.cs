using Com.Bigdata.Dis.Sdk.DISCommon.Config;
using Com.Bigdata.Dis.Sdk.DISCommon.Log;
using Com.Bigdata.Dis.Sdk.DISCommon.Model;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Utils.Cache
{
    public class ThreadInfo
    {
        public PutRecordsRequest putRecordsRequest { get; set; }
        public DISConfig disConfig { get; set; }
    }

    public class CacheUtils
    {
        private static ILog LOGGER = LogHelper.GetInstance();

        private static int DEFAULT_THREAD_POOL_SIZE = 100;

        public static void Work(PutRecordsRequest putRecordsRequest, DISConfig disConfig)
        {
            ThreadInfo threadInfo = new ThreadInfo()
            {
                putRecordsRequest = putRecordsRequest,
                disConfig = disConfig
            };
            ThreadPool.SetMinThreads(DEFAULT_THREAD_POOL_SIZE, 10);
            ThreadPool.QueueUserWorkItem(new WaitCallback(ProcessFile), threadInfo);
        }

        private static void ProcessFile(object a)
        {
            ThreadInfo threadInfo = a as ThreadInfo;
            PutRecordsRequest putRecordsRequest = threadInfo.putRecordsRequest;
            DISConfig disConfig = threadInfo.disConfig;

            PutToCache(putRecordsRequest, disConfig);
        }

        public static void PutToCache(PutRecordsRequest putRecordsRequest, DISConfig disConfig)
        {
            try
            {
                CacheManager cacheManager = CacheManager.GetInstance(disConfig);
                cacheManager.PutToCache(putRecordsRequest);
            }
            catch (Exception e)
            {
                // Failed to put failed records to local cache file, continue.
                LOGGER.Error("Failed to write failed records to local cache file.", e);
            }
        }
    }
}
