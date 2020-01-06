using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class MRSDestinationDescriptorEntity
    {
        /// <summary>
        /// 在IAM中创建委托的名称，DIS需要获取IAM委托信息去访问您指定的资源
        /// </summary>
        [JsonProperty("agency_name", NullValueHandling = NullValueHandling.Ignore)]
        public string AgencyName { get; set; }

        /// <summary>
        /// 存储该通道数据的MRS集群ID。
        /// </summary>
        [JsonProperty("mrs_cluster_id", NullValueHandling = NullValueHandling.Ignore)]
        public string MRSClusterId { get; set; }

        /// <summary>
        /// 存储该通道数据的MRS集群名称。
        /// </summary>
        [JsonProperty("mrs_cluster_name", NullValueHandling = NullValueHandling.Ignore)]
        public string MRSClusterName { get; set; }

        /// <summary>
        /// 存储该通道数据的MRS集群的HDFS路径。
        /// </summary>
        [JsonProperty("mrs_hdfs_path", NullValueHandling = NullValueHandling.Ignore)]
        public string MRSHdfsPath { get; set; }

        /// <summary>
        /// 临时存储该通道数据的OBS桶名称。
        /// </summary>
        [JsonProperty("obs_bucket_path", NullValueHandling = NullValueHandling.Ignore)]
        public string OBSBucketPath { get; set; }

        /// <summary>
        /// 在MRS集群HDFS中存储通道文件的自定义目录，多级目录可用"/"进行分隔。
        /// </summary>
        [JsonProperty("file_prefix", NullValueHandling = NullValueHandling.Ignore)]
        public string FilePrefix { get; set; }

        /// <summary>
        /// 在MRS集群HDFS中存储通道文件的自定义目录，多级目录可用"/"进行分隔。
        /// 取值范围：0~50个字符。
        /// 默认配置为空。
        /// </summary>
        [JsonProperty("hdfs_prefix_folder", NullValueHandling = NullValueHandling.Ignore)]
        public string HdfsPrefixFolder { get; set; }

        /// <summary>
        /// 偏移量。
        /// LATEST：最大偏移量，即获取最新的数据。
        /// EARLIEST：最小偏移量，即读取最早的数据。
        /// 缺省值:LATEST
        /// </summary>
        [JsonProperty("consumer_strategy", NullValueHandling = NullValueHandling.Ignore)]
        public string ConsumerStrategy { get; set; }

        /// <summary>
        /// 根据用户配置的时间，周期性的将数据导入MRS集群HDFS中，若某个时间段内无数据，则此时间段不会生成打包文件
        /// </summary>
        [JsonProperty("deliver_time_interval", NullValueHandling = NullValueHandling.Ignore)]
        public int DeliverTimeInterval { get; set; }

        /// <summary>
        /// 用户数据转储失败的重试失效时间。重试时间超过该配置项配置的值，则将转储失败的数据备份至“OBS桶/ file_prefix/mrs_error”目录下。
        /// </summary>
        [JsonProperty("retry_duration ", NullValueHandling = NullValueHandling.Ignore)]
        public int RetryDuration { get; set; }
    }
}
