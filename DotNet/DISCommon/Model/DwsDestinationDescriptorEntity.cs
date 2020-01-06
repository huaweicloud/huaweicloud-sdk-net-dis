using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class DwsDestinationDescriptorEntity
    {
        /// <summary>
        /// 偏移量。
        /// LATEST：最大偏移量，即获取最新的数据。
        /// EARLIEST：最小偏移量，即读取最早的数据。
        /// 缺省值:LATEST
        /// </summary>
        [JsonProperty("consumer_strategy", NullValueHandling = NullValueHandling.Ignore)]
        public string ConsumerStrategy { get; set; }

        /// <summary>
        /// 在IAM中创建委托的名称，DIS需要获取IAM委托信息去访问您指定的资源
        /// </summary>
        [JsonProperty("agency_name", NullValueHandling = NullValueHandling.Ignore)]
        public string AgencyName { get; set; }

        /// <summary>
        /// 存储该通道数据的DWS集群名称。
        /// </summary>
        [JsonProperty("dws_cluster_name", NullValueHandling = NullValueHandling.Ignore)]
        public string dws_cluster_name { get; set; }

        /// <summary>
        /// 存储该通道数据的DWS集群ID。
        /// </summary>
        [JsonProperty("dws_cluster_id", NullValueHandling = NullValueHandling.Ignore)]
        public string dws_cluster_id { get; set; }

        /// <summary>
        /// 存储该通道数据的DWS数据库名称。
        /// </summary>
        [JsonProperty("dws_database_name", NullValueHandling = NullValueHandling.Ignore)]
        public string dws_database_name { get; set; }

        /// <summary>
        /// 存储该通道数据的DWS数据库模式。
        /// </summary>
        [JsonProperty("dws_schema", NullValueHandling = NullValueHandling.Ignore)]
        public string dws_schema { get; set; }

        /// <summary>
        /// 存储该通道数据的DWS数据库模式下的数据表。
        /// </summary>
        [JsonProperty("dws_table_name", NullValueHandling = NullValueHandling.Ignore)]
        public string dws_table_name { get; set; }

        /// <summary>
        /// 用户数据的字段分隔符，根据此分隔符分隔用户数据插入DWS数据表的相应列
        /// </summary>
        [JsonProperty("dws_delimiter", NullValueHandling = NullValueHandling.Ignore)]
        public string dws_delimiter { get; set; }

        /// <summary>
        /// 存储该通道数据的DWS数据库的用户名
        /// </summary>
        [JsonProperty("user_name", NullValueHandling = NullValueHandling.Ignore)]
        public string user_name { get; set; }

        /// <summary>
        /// 存储该通道数据的DWS数据库的密码。
        /// </summary>
        [JsonProperty("user_password", NullValueHandling = NullValueHandling.Ignore)]
        public string user_password { get; set; }

        /// <summary>
        /// 用户在密钥管理服务（简称KMS）创建的用户主密钥名称，用于加密存储DWS数据库的密码。
        /// </summary>
        [JsonProperty("kms_user_key_name", NullValueHandling = NullValueHandling.Ignore)]
        public string kms_user_key_name { get; set; }

        /// <summary>
        /// 用户在密钥管理服务（简称KMS）创建的用户主密钥ID，用于加密存储DWS数据库的密码。
        /// </summary>
        [JsonProperty("kms_user_key_id", NullValueHandling = NullValueHandling.Ignore)]
        public string kms_user_key_id { get; set; }

        /// <summary>
        /// 临时存储该通道数据的OBS桶名称。
        /// </summary>
        [JsonProperty("obs_bucket_path", NullValueHandling = NullValueHandling.Ignore)]
        public string obs_bucket_path { get; set; }

        /// <summary>
        /// 临时存储该通道数据的OBS桶下的自定义目录，多级目录可用“/”进行分隔，不可以“/”开头
        /// </summary>
        [JsonProperty("file_prefix", NullValueHandling = NullValueHandling.Ignore)]
        public string file_prefix { get; set; }

        /// <summary>
        /// 根据用户配置的时间，周期性的将数据导入DWS集群的数据表中，若某个时间段内无数据，则此时间段不会生成打包文件。
        /// </summary>
        [JsonProperty("deliver_time_interval", NullValueHandling = NullValueHandling.Ignore)]
        public int deliver_time_interval { get; set; }

        /// <summary>
        /// 用户数据导入DWS集群失败的重试失效时间。
        /// </summary>
        [JsonProperty("retry_duration", NullValueHandling = NullValueHandling.Ignore)]
        public int retry_duration { get; set; }
    }
}
