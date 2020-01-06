using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Com.Bigdata.Dis.Sdk.DISCommon.Ext;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Utils
{
    /// <summary>
    /// 获取SDK版本号
    /// </summary>
    public class VersionUtils
    {
        private const string VERSION_FILE = "version.properties";
    
        private const string DEFAULT_VERSION = "unknown";
    
        private const string DEFAULT_PLATFORM = "java";
    
        /** SDK version info */
        private static string version;

        /** SDK platform info */
        private static string platform;

        private static object _lock = new object();

        /** 获取版本信息 */
        public static string GetVersion()
        {
            if (version == null)
            {
                lock(_lock)
                {
                    if (version == null)
                        InitializeVersion();
                }
            }
            return version;
        }
    
        /** 获取平台信息 */
        public static string GetPlatform()
        {
            if (platform == null)
            {
                lock (_lock)
                {
                    if (platform == null)
                        InitializeVersion();
                }
            }
            return platform;
        }
    
        /** 从版本配置文件读取配置文件信息 */
        private static void InitializeVersion()
        {
            var versionConfiguation = new Properties();
            versionConfiguation.Load(new StreamReader(VERSION_FILE));

            version = versionConfiguation.GetProperty("version", DEFAULT_VERSION);
            platform = versionConfiguation.GetProperty("platform", DEFAULT_PLATFORM);
        }
    }
}
