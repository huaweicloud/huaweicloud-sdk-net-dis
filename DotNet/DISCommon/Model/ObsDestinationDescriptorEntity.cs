using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class ObsDestinationDescriptorEntity
    {
        /// <summary>
        /// 转储任务的名称。
        /// 任务名称由英文字母、数字、中划线和下划线组成。长度为1～64个字符。
        /// </summary>
        [JsonProperty("task_name")]
        public string TaskName { get; set; }

        /// <summary>
        /// 转储文件格式。
        /// text
        /// parquet
        /// carbon
        /// 说明：“源数据类型”为“JSON”，“转储服务类型”为“OBS”时才可选择“parquet”或“carbon”格式。
        /// </summary>
        [JsonProperty("destination_file_type")]
        public string DestinationFileType { get; set; }

        /// <summary>
        /// OBS文件路径，文件中的数据必须是JSON格式.
        /// 说明：“源数据类型”为“JSON”，“转储服务类型”为“OBS”，“转储文件格式”为“parquet”和“carbon”时需要配置此参数。
        /// </summary>
        //[JsonProperty("data_schema_path", NullValueHandling = NullValueHandling.Ignore)]
        //public string data_schema_path { get; set; }
        //[JsonProperty("target_data_schema", NullValueHandling = NullValueHandling.Ignore)]
        //public string TargetDataSchema { get; set; }

        /// <summary>
        /// 根据源数据的时间戳和已配置的"partition_format"生成对应的转储时间目录。将源数据的时间戳使用“yyyy/M/dd/HH/mm”格式生成分区字符串，用来定义写到OBS的Object文件所在的目录层次结构
        /// 说明: 转储的目标文件格式为parquet，如果需要自定义OBS的时间目录，则必选。
        /// </summary>
        [JsonProperty("processing_schema", NullValueHandling = NullValueHandling.Ignore)]
        public ProcessingSchema ProcessingSchema { get; set; }

        /// <summary>
        /// 偏移量。
        /// LATEST：最大偏移量，即获取最新的数据。
        /// EARLIEST：最小偏移量，即读取最早的数据。
        /// 缺省值:LATEST
        /// </summary>
        [JsonProperty("consumer_strategy", NullValueHandling = NullValueHandling.Ignore)]
        public string ConsumerStrategy { get; set; }

        /// <summary>
        /// 在IAM中创建委托的名称，DIS需要获取IAM委托信息去访问您指定的资源。创建委托的参数设置如下：
        ///委托类型：云服务
        ///云服务：DIS
        ///持续时间：永久
        ///“所属区域”为“全局服务”，“项目”为“对象存储服务”对应的“权限集”包含“Tenant Administrator”。
        ///取值范围：长度不超过64位，且不可配置为空。
        /// </summary>
        [JsonProperty("agency_name")]
        public string AgencyName { get; set; }

        /// <summary>
        /// 存储该通道数据的OBS桶名称。
        /// </summary>
        [JsonProperty("obs_bucket_path")]
        public string ObsBucketPath { get; set; }

        /// <summary>
        /// 在OBS中存储通道文件的自定义目录，多级目录可用“/”进行分隔，不可以“/”开头。
        /// </summary>
        [JsonProperty("file_prefix", NullValueHandling = NullValueHandling.Ignore)]
        public string FilePrefix { get; set; }

        /// <summary> 	
        ///根据用户配置的时间，周期性的将数据导入OBS，若某个时间段内无数据，则此时间段不会生成打包文件。
        /// </summary>
        [JsonProperty("deliver_time_interval")]
        public int DeliverTimeInterval { get; set; }

        /// <summary>	
        ///将转储任务创建时间使用“yyyy/MM/dd/HH/mm”格式生成分区字符串，用来定义写到OBS的Object文件所在的目录层次结构。其中，“/”表示一级OBS目录。
        /// </summary>
        [JsonProperty("partition_format", NullValueHandling = NullValueHandling.Ignore)]
        public string PartitionFormat { get; set; }

        /// <summary>
        /// 当数据转储类型为“自定义文件转储”时，需要配置该参数。
        /// 默认值：file_stream。
        /// </summary>
        [JsonProperty("deliver_data_type", NullValueHandling = NullValueHandling.Ignore)]
        public string DeliverDataType { get; set; }

        /// <summary>
        /// 转储文件的记录分隔符，用于分隔写入转储文件的用户数据
        /// </summary>
        [JsonProperty("record_delimiter", NullValueHandling = NullValueHandling.Ignore)]
        public string RecordDelimiter { get; set; }

    }

    public class ProcessingSchema
    {
        /// <summary>
        /// 源数据时间戳的属性名称。
        /// </summary>
        [JsonProperty("timestamp_name")]
        public string TimestampName { get; set; }

        /// <summary>
        /// 源数据时间戳的类型。
        /// String
        /// Timestamp：Long类型的13位时间戳
        /// </summary>
        [JsonProperty("timestamp_type")]
        public string TimestampType { get; set; }

        /// <summary>
        /// 源数据时间戳的类型为String时必选，用于根据时间戳格式生成OBS的时间目录。
        /// 取值范围：
        /// yyyy/MM/dd HH:mm:ss
        /// MM/dd/yyyy HH:mm:ss
        /// dd/MM/yyyy HH:mm:ss
        /// yyyy-MM-dd HH:mm:ss
        /// MM-dd-yyyy HH:mm:ss
        /// dd-MM-yyyy HH:mm:ss
        /// </summary>
        [JsonProperty("timestamp_format", NullValueHandling = NullValueHandling.Ignore)]
        public string TimestampFormat { get; set; }
    }
}
