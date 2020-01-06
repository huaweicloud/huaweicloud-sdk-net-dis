using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class CreateStreamRequest
    {
        /// <summary>
        /// 通道名称
        /// </summary>
        [JsonProperty("stream_name")]
        public string StreamName { get; set; }

        /// <summary>
        /// 通道类型。
        /// 取值范围：
        /// COMMON：普通通道，表示1MB带宽。
        /// ADVANCED：高级通道，表示5MB带宽。
        /// 缺省值：COMMON。
        /// </summary>
        [JsonProperty("stream_type", NullValueHandling = NullValueHandling.Ignore)]
        public string StreamType { get; set; }

        /// <summary>
        /// 数据保留时长。
        /// 取值范围：N*24，N的取值为1 ~7的整数。
        /// 单位：小时。
        /// 缺省值：24。
        /// 空表示使用缺省值
        /// </summary>
        [JsonProperty("data_duration", NullValueHandling = NullValueHandling.Ignore)]
        public int DataDuration { get; set; }

        /// <summary>
        /// 分区数
        /// </summary>
        [JsonProperty("partition_count")]
        public int PartitionCount { get; set; }

        /// <summary>
        /// 源数据类型
        /// 取值范围：
        /// BLOB：存储在数据库管理系统中的一组二进制数据。
        /// JSON：一种开放的文件格式，以易读的文字为基础，用来传输由属性值或者序列性的值组成的数据对象。
        /// CSV：纯文本形式存储的表格数据，分隔符默认采用逗号。
        /// FILE：源数据为文件
        /// 缺省值：BLOB。
        /// </summary>
        [JsonProperty("data_type", NullValueHandling = NullValueHandling.Ignore)]
        public string DataType { get; set; }

        /// <summary>
        /// 用于描述用户JOSN、CSV格式的源数据结构，采用Avro Schema的语法描述。Avro介绍请参见http://avro.apache.org/docs/current/#schemas。
        /// 说明 源数据转储为parquet和carbon格式时必选。
        /// </summary>
        [JsonProperty("data_schema", NullValueHandling = NullValueHandling.Ignore)]
        public string DataSchema { get; set; }

        /// <summary>
        /// 通道的标签。
        /// </summary>
        [JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)]
        public List<Tag> Tags { get; set; } 

        /// <summary>
        /// 转储目的地为OBS的参数列表。暂时不支持同时转储至多个目的地。
        /// 缺省值：空。
        /// 配置为空表示数据不转储到OBS。
        /// 说明:此参数，仅“源数据类型”为“FILE”时呈现且需要配置。
        /// </summary>
        [JsonProperty("obs_destination_descriptor", NullValueHandling = NullValueHandling.Ignore)]
        public List<ObsDestinationDescriptor> ObsDestinationDescriptor { get; set; }

        /// <summary>
        /// 用户数据转储CloudTable服务失败时，可选择将转储失败的数据备份至OBS服务，此参数为OBS服务的桶名称
        /// </summary>
        //[JsonProperty("obs_backup_bucket_path", NullValueHandling = NullValueHandling.Ignore)]
        //public string OBSBackupBucketPath { get; set; }

        ///// <summary>
        ///// 用户数据转储CloudTable服务失败时，可选择将转储失败的数据备份至OBS服务，此参数为OBS桶下的自定义目录，多级目录可用“/”进行分隔，不可以“/”开头。
        ///// </summary>
        //[JsonProperty("backup_file_prefix")]
        //public string BackupFilePrefix { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //[JsonProperty("data_schema", NullValueHandling = NullValueHandling.Ignore)]
        //public string DataSchema { get; set; }

        //[JsonProperty("obs_destination_descriptor", NullValueHandling = NullValueHandling.Ignore)]
        //public List<ObsDestinationDescriptorEntity> ObsDestinationDescriptor { get; set; }

        //[JsonProperty("mrs_destination_descriptor", NullValueHandling = NullValueHandling.Ignore)]
        //public List<MRSDestinationDescriptorEntity> MRSDestinationDescriptor { get; set; }

        //[JsonProperty("dli_destination_descriptor", NullValueHandling = NullValueHandling.Ignore)]
        //public List<DLIDestinationDescriptorEntity> DLIDestinationDescriptor { get; set; }

        //[JsonProperty("cloudtable_destination_descriptor", NullValueHandling = NullValueHandling.Ignore)]
        //public List<CloudtableDestinationDescriptorEntity> CloudtableDestinationDescriptor { get; set; }

        //[JsonProperty("dws_destination_descriptor", NullValueHandling = NullValueHandling.Ignore)]
        //public List<DwsDestinationDescriptorEntity> DwsDestinationDescriptor { get; set; }

        //public string ToString(DescribeStreamType type)
        //{
        //    switch (type)
        //    {

        //        //创建无转储任务的通道
        //        case DescribeStreamType.StreamWithNoneDump:
        //            return "DescribeStreamRequestEntity{" +
        //                           "streamName='" + StreamName + '\'' +
        //                           ", partitionCount=" + PartitionCount +
        //                           '}';

        //        //创建具有OBS按周期转储任务的通道
        //        case DescribeStreamType.StreamWithOBSPeriodDump:
        //            return "";

        //        //创建具有OBS自定义文件转储任务的通道
        //        case DescribeStreamType.StreamWithOBSCustomDump:
        //            return "";

        //        //创建具有MRS转储任务的通道
        //        case DescribeStreamType.StreamWithMRSDump:
        //            return "";

        //        //创建具有DLI转储任务的通道
        //        case DescribeStreamType.StreamWithDLIDump:
        //            return "";

        //        //创建具有CloudTable HBase转储任务的通道
        //        case DescribeStreamType.StreamWithCloudTableHBaseDump:
        //            return "";

        //        //创建具有CloudTable OpenTSDB转储任务的通道
        //        case DescribeStreamType.StreamWithCloudTableOpenTSDBDump:
        //            return "";
        //        //创建具有DWS转储任务的通道
        //        case DescribeStreamType.StreamWithDWSDump:
        //            return "";
        //        default: return "";
        //    }
        //}
    }

    public class Tag
    {
        /// <summary>
        /// 键。标签的key值不能包含“=”,“*”,“<”,“>”,“\”,“,”,“|”,“/”，且首尾字符不能为空格。
        /// </summary>
        [JsonProperty("key")]
        public string Key { get; set; }

        /// <summary>
        /// 值。标签的value值不能包含“=”,“*”,“<”,“>”,“\”,“,”,“|”,“/”，且首尾字符不能为空格。
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class ObsDestinationDescriptor
    {
        /// <summary>
        /// 在IAM中创建委托的名称，DIS需要获取IAM委托信息去访问您指定的资源。创建委托的参数设置如下：
        ///	委托类型：云服务
        /// 云服务：DIS
        /// 持续时间：永久
        /// “所属区域”为“全局服务”，“项目”为“对象存储服务”对应的“策略”包含“Tenant Administrator”。
        /// 取值范围：长度不超过64位，且不可配置为空。
        /// </summary>
        [JsonProperty("agency_name")]
        public string AgencyName { get; set; }

        /// <summary>
        /// 存储该通道数据的OBS桶名称
        /// </summary>
        [JsonProperty("obs_bucket_path")]
        public string ObsBucketPath { get; set; }
    }
}
