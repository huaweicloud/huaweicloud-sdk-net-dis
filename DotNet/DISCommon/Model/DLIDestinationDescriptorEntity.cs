using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class DLIDestinationDescriptorEntity
    {
        /// <summary>
        /// 转储任务的名称。
        /// 任务名称由英文字母、数字、中划线和下划线组成。长度为1～64个字符。
        /// </summary>
        [JsonProperty("task_name")]
        public string TaskName { get; set; }

        /// <summary>
        /// 在IAM中创建委托的名称，DIS需要获取IAM委托信息去访问您指定的资源。
        /// </summary>
        [JsonProperty("agency_name")]
        public string AgencyName { get; set; }

        /// <summary>
        /// 存储该通道数据的DLI数据库名称。
        /// </summary>
        [JsonProperty("dli_database_name")]
        public string DLIDatabaseName { get; set; }

        /// <summary>
        /// 存储该通道数据的DLI表名称。
        /// </summary>
        [JsonProperty("dli_table_name")]
        public string DLITableName { get; set; }

        /// <summary>
        /// 偏移量。
        /// LATEST：最大偏移量，即获取最新的数据。
        /// EARLIEST：最小偏移量，即读取最早的数据。
        /// 缺省值:LATEST
        /// </summary>
        [JsonProperty("consumer_strategy", NullValueHandling = NullValueHandling.Ignore)]
        public string ConsumerStrategy { get; set; }

        /// <summary>
        /// 临时存储该通道数据的OBS桶名称。
        /// </summary>
        [JsonProperty("obs_bucket_path")]
        public string OBSBucketPath { get; set; }

        /// <summary>
        /// 临时存储该通道数据的OBS桶下的自定义目录，多级目录可用“/”进行分隔，不可以“/”开头。
        /// 取值范围：英文字母、数字、下划线和斜杠，最大长度为50个字符。
        /// </summary>
        [JsonProperty("file_prefix", NullValueHandling = NullValueHandling.Ignore)]
        public string FilePrefix { get; set; }

        /// <summary>
        /// 根据用户配置的时间，周期性的将数据导入DLI中，若某个时间段内无数据，则此时间段不会生成打包文件。
        /// </summary>
        [JsonProperty("deliver_time_interval")]
        public int DeliverTimeInterval { get; set; }

        /// <summary>
        /// 用户数据导入DLI失败的重试失效时间。重试时间超过该配置项配置的值，则将转储失败的数据备份至“OBS桶/dli_error”目录下。
        /// </summary>
        [JsonProperty("retry_duration ", NullValueHandling = NullValueHandling.Ignore)]
        public int RetryDuration { get; set; }
    }
}
