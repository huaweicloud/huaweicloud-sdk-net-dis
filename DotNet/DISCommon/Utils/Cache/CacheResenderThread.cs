using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Com.Bigdata.Dis.Sdk.DISCommon.Log;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Utils.Cache
{
    /// <summary>
    /// 缓存重发线程
    /// </summary>
    public class CacheResenderThread
    {
        private static ILog LOGGER = LogHelper.GetInstance();

        private Thread _thread;

        protected CacheResenderThread()
        {
            _thread = new Thread(new ThreadStart(this.RunThread));
        }

        public void RunThread()
        {
            LOGGER.Info("Starting cache resender thread.");

            // TODO 归档缓存数据重传


            LOGGER.Info("Terminate cache resender thread.");
        }


        public CacheResenderThread(String name)
        {
            this._thread.Name = "Cache-ResenderThread-" + name;
            this._thread.IsBackground = true;            
        }
    }
}
