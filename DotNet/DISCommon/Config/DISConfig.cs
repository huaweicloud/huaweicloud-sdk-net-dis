using System;
using System.IO;
using System.Text;
using Com.Bigdata.Dis.Sdk.DISCommon.Auth;
using Com.Bigdata.Dis.Sdk.DISCommon.Ext;

// ReSharper disable InconsistentNaming

namespace Com.Bigdata.Dis.Sdk.DISCommon.Config
{
    public class DISConfig : Properties
    {
        /** 默认配置文件名 */
        private const string FILE_NAME = "dis.properties";

        private const string DEFAULT_VALUE_REGION_ID = null;
        private const string DEFAULT_VALUE_SERVER_URL = null;
        private const string DEFAULT_VALUE_ENDPOINT = null;
        private const string DEFAULT_VALUE_PORT = "20004";
        private const string DEFAULT_VALUE_IS_SSL = "true";
        private const string DEFAULT_VALUE_PROJECT_ID = null;

        private const int DEFAULT_VALUE_CONNECTION_TIMEOUT = 30;
        private const int DEFAULT_VALUE_SOCKET_TIMEOUT = 60;
        private const int DEFAULT_VALUE_MAX_PER_ROUTE = 100;
        private const int DEFAULT_VALUE_MAX_TOTAL = 500;
        private const bool DEFAULT_VALUE_IS_DEFAULT_TRUSTED_JKS_ENABLED = true;
        private const bool DEFAULT_VALUE_IS_DEFAULT_DATA_ENCRYPT_ENABLED = false;
        private const bool DEFAULT_VALUE_DATA_COMPRESS_ENABLED = false;
        private const bool DEFAULT_VALUE_DATA_CACHE_ENABLED = false;
        private const string DEFAULT_VALUE_DATA_CACHE_DIR = "/data/dis";
        private const int DEFAULT_VALUE_DATA_CACHE_DIR_MAX_SIZE = 2048;
        private const int DEFAULT_VALUE_DATA_CACHE_ARCHIVE_MAX_SIZE = 512;
        private const int DEFAULT_VALUE_DATA_CACHE_ARCHIVE_LIFE_CYCLE = 24 * 3600;

        private const string PROPERTY_REGION_ID = "region";
        private const string PROPERTY_SERVER_URL = "gwurl";
        private const string PROPERTY_ENDPOINT = "endpoint";
        private const string PROPERTY_PORT = "port";
        private const string PROPERTY_IS_SSL = "ssl";
        private const string PROPERTY_PROJECT_ID = "projectId";

        private const string PROPERTY_CONNECTION_TIMEOUT = "CONNECTION_TIME_OUT";
        private const string PROPERTY_SOCKET_TIMEOUT = "SOCKET_TIME_OUT";
        private const string PROPERTY_MAX_PER_ROUTE = "DEFAULT_MAX_PER_ROUTE";
        private const string PROPERTY_MAX_TOTAL = "DEFAULT_MAX_TOTAL";

        private const string PROPERTY_AK = "ak";
        private const string PROPERTY_SK = "sk";
        private const string PROPERTY_DATA_PASSWORD = "data.password";
        private const string PROPERTY_IS_DEFAULT_TRUSTED_JKS_ENABLED = "IS_DEFAULT_TRUSTED_JKS_ENABLED";

        private const string PROPERTY_IS_DEFAULT_DATA_ENCRYPT_ENABLED = "data.encrypt.enabled";
        private const string PROPERTY_DATA_COMPRESS_ENABLED = "data.compress.enabled";
        private const string PROPERTY_DATA_CACHE_ENABLED = "data.cache.enabled";
        private const string PROPERTY_DATA_CACHE_DIR = "data.cache.dir";
        private const string PROPERTY_DATA_CACHE_DISK_MAX_SIZE = "data.cache.disk.max.size";
        private const string PROPERTY_DATA_CACHE_ARCHIVE_MAX_SIZE = "data.cache.archive.max.size";
        private const string PROPERTY_DATA_CACHE_ARCHIVE_LIFE_CYCLE = "data.cache.archive.life.cycle";

        private const string PROPERTY__BODY_SERIALIZE_TYPE = "body.serialize.type";

        private const string PROPERTY_BACK_OFF_MAX_INTERVAL_MS = "backoff.max.interval.ms";

        private const string PROPERTY_PRODUCER_EXCEPTION_RETRIES = "exception.retries";

        private const string PROPERTY_PRODUCER_RECORDS_RETRIES = "records.retries";



        public ICredentials Credentials { get; set; }

        public bool GetIsDefaultTrustedJksEnabled()
        {
            return GetBool(PROPERTY_IS_DEFAULT_TRUSTED_JKS_ENABLED, DEFAULT_VALUE_IS_DEFAULT_TRUSTED_JKS_ENABLED);
        }

        public bool GetIsDefaultDataEncryptEnabled()
        {
            return GetBool(PROPERTY_IS_DEFAULT_DATA_ENCRYPT_ENABLED, DEFAULT_VALUE_IS_DEFAULT_DATA_ENCRYPT_ENABLED);
        }

        public bool GetIsDefaultDataCompressEnabled()
        {
            return GetBool(PROPERTY_DATA_COMPRESS_ENABLED, DEFAULT_VALUE_DATA_COMPRESS_ENABLED);
        }

        public string GetAK()
        {
            return Get(PROPERTY_AK, null);
        }

        public string GetSK()
        {
            return Get(PROPERTY_SK, null);
        }

        public string GetDataPassword()
        {
            return Get(PROPERTY_DATA_PASSWORD, null);
        }

        public string GetRegion()
        {
            return Get(PROPERTY_REGION_ID, null);
        }

        public int GetConnectionTimeOut()
        {
            return GetInt(PROPERTY_CONNECTION_TIMEOUT, DEFAULT_VALUE_CONNECTION_TIMEOUT) * 1000;
        }

        public int GetSocketTimeOut()
        {
            return GetInt(PROPERTY_SOCKET_TIMEOUT, DEFAULT_VALUE_SOCKET_TIMEOUT) * 1000;
        }

        public int GetMaxPerRoute()
        {
            return GetInt(PROPERTY_MAX_PER_ROUTE, DEFAULT_VALUE_MAX_PER_ROUTE);
        }

        public int GetMaxTotal()
        {
            return GetInt(PROPERTY_MAX_TOTAL, DEFAULT_VALUE_MAX_TOTAL);
        }

        public string GetRegionId()
        {
            return Get(PROPERTY_REGION_ID, DEFAULT_VALUE_REGION_ID);
        }

        public string GetServerURL()
        {
            return Get(PROPERTY_SERVER_URL, DEFAULT_VALUE_SERVER_URL);
        }

        public string GetEndpoint()
        {
            return Get(PROPERTY_ENDPOINT, DEFAULT_VALUE_ENDPOINT);
        }

        public string GetPort()
        {
            return Get(PROPERTY_PORT, DEFAULT_VALUE_PORT);
        }

        public string GetIsSSL()
        {
            return Get(PROPERTY_IS_SSL, DEFAULT_VALUE_IS_SSL);
        }

        public string GetProjectId()
        {
            return Get(PROPERTY_PROJECT_ID, DEFAULT_VALUE_PROJECT_ID);
        }

        public string GetAK(string ak)
        {
            return Get(PROPERTY_AK, null);
        }

        public string GetBodySerializeType()
        {
            return Get(PROPERTY__BODY_SERIALIZE_TYPE, "json");
        }

        public long GetBackOffMaxIntervalMs()
        {
            return long.Parse(Get(PROPERTY_BACK_OFF_MAX_INTERVAL_MS, (30 * 1000).ToString()));
        }

        /// <summary>
        /// 接口异常重试次数
        /// </summary>
        /// <returns></returns>
        public int GetExceptionRetries()
        {
            int exceptionRetry = int.Parse(Get(PROPERTY_PRODUCER_EXCEPTION_RETRIES, "3"));
            if (exceptionRetry < 0)
            {
                return int.MaxValue;
            }
            return exceptionRetry;
        }

        /// <summary>
        /// 记录上传重试次数
        /// </summary>
        /// <returns></returns>
        public int GetRecordsRetries()
        {
            int recordsRetry = int.Parse(Get(PROPERTY_PRODUCER_RECORDS_RETRIES, "20"));
            if (recordsRetry < 0)
            {
                return int.MaxValue;
            }
            return recordsRetry;
        }

        public bool IsDataCacheEnabled()
        {
            return GetBool(PROPERTY_DATA_CACHE_ENABLED, DEFAULT_VALUE_DATA_CACHE_ENABLED);
        }

        public String GetDataCacheDir()
        {
            return Get(PROPERTY_DATA_CACHE_DIR, DEFAULT_VALUE_DATA_CACHE_DIR);
        }

        public int GetDataCacheDiskMaxSize()
        {
            return GetInt(PROPERTY_DATA_CACHE_DISK_MAX_SIZE, DEFAULT_VALUE_DATA_CACHE_DIR_MAX_SIZE);
        }

        public int GetDataCacheArchiveMaxSize()
        {
            return GetInt(PROPERTY_DATA_CACHE_ARCHIVE_MAX_SIZE, DEFAULT_VALUE_DATA_CACHE_ARCHIVE_MAX_SIZE);
        }

        public int GetDataCacheArchiveLifeCycle()
        {
            return GetInt(PROPERTY_DATA_CACHE_ARCHIVE_LIFE_CYCLE, DEFAULT_VALUE_DATA_CACHE_ARCHIVE_LIFE_CYCLE);
        }

        public void SetAK(string ak)
        {
            Set(PROPERTY_AK, ak);
        }

        public void SetSK(string sk)
        {
            Set(PROPERTY_SK, sk);
        }

        public void SetRegion(string region)
        {
            Set(PROPERTY_REGION_ID, region);
        }

        public void SetProjectId(string projectId)
        {
            Set(PROPERTY_PROJECT_ID, projectId);
        }

        public void SetEndpoint(string endpoint)
        {
            Set(PROPERTY_ENDPOINT, endpoint);
        }

        public void SetDataEncryptEnabled(bool dataEncryptEnabled)
        {
            Set(PROPERTY_IS_DEFAULT_DATA_ENCRYPT_ENABLED, dataEncryptEnabled.ToString());
        }

        /// <summary>
        /// 客户端证书校验
        /// </summary>
        /// <param name="defaultClientCertAuthEnabled"></param>
        public void SetDefaultClientCertAuthEnabled(bool defaultClientCertAuthEnabled)
        {
            Set(PROPERTY_IS_DEFAULT_TRUSTED_JKS_ENABLED, defaultClientCertAuthEnabled.ToString());
        }

        public void SetIsDefaultDataCompressEnabled(bool defaultDataCompressEnabled)
        {
            Set(PROPERTY_DATA_COMPRESS_ENABLED, defaultDataCompressEnabled.ToString());
        }

        public void Set(string key, string value)
        {
            SetProperty(key, value);
        }

        public string GetFinalBaseURL()
        {
            var endPoint = GetEndpoint();
            var isSSL = GetIsSSL();
            if (null != endPoint)
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append(endPoint);
                return stringBuilder.ToString();
            }

            var region = GetRegionId();
            var port = GetPort();
            if (null != region)
            {
                var stringBuilder = new StringBuilder();
                stringBuilder.Append("true".Equals(isSSL) ? "https" : "http");
                stringBuilder.Append("://dis.");
                stringBuilder.Append(region);
                stringBuilder.Append(".myhuaweicloud.com:");
                stringBuilder.Append(port);
                stringBuilder.Append("/dis-gw");
                return stringBuilder.ToString();
            }

            throw new ArgumentException("endpoint error.");
        }

        public bool IsDataCompressEnabled()
        {
            return GetBool(PROPERTY_DATA_COMPRESS_ENABLED, DEFAULT_VALUE_DATA_COMPRESS_ENABLED);
        }

        #region Helper functions
        public bool GetBool(string propName, bool defaultValue)
        {
            var value = Get(propName, null);
            return value != null ? bool.Parse(value) : defaultValue;
        }

        public int GetInt(string propName, int defaultValue)
        {
            var value = Get(propName, null);
            if (value == null) return defaultValue;

            int intValue;
            if (int.TryParse(value, out intValue))
            {
                return intValue;
            }

            // Log error for failed parse
            Console.WriteLine("Invalid Value '{0}' for key '{1}'", value, propName);
            return defaultValue;
        }

        public string Get(string propName, string defaultValue)
        {
            var value = GetProperty(propName);
            return value?.Trim() ?? defaultValue;
        }

        private void Load(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException(fileName);
            }

            using (var streamReader = new StreamReader(fileName))
            {
                Load(streamReader);
            }
        }

        public static DISConfig buildDefaultConfig()
        {
            return BuildConfig(FILE_NAME);
        }

        public static DISConfig BuildConfig(string configFile)
        {
            var disConfig = new DISConfig();

            var configFileDefined = !string.IsNullOrEmpty(configFile?.Trim());
            var needLoadFromDefault = false;

            if (configFileDefined)
            {
                try
                {
                    disConfig.Load(configFile);
                }
                catch (IOException e)
                {
                    needLoadFromDefault = true;
                    Console.WriteLine("load config from file {0} failed. {1}", configFile, e.Message);
                }
            }

            if (!configFileDefined || needLoadFromDefault)
            {
                try
                {
                    disConfig.Load(FILE_NAME);
                }
                catch (IOException e)
                {
                    Console.WriteLine("load config from default file {0} failed. {1}", FILE_NAME, e.Message);
                }
            }

            return disConfig;
        }

        public static DISConfig BuildConfig(DISConfig disConfig)
        {
            var fileConfig = buildDefaultConfig();

            foreach (var keyValue in disConfig)
            {
                fileConfig.SetProperty((string) keyValue.Key, (string) keyValue.Value);
            }

            return fileConfig;
        }
        #endregion
    }
}
