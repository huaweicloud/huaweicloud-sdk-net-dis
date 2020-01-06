using System.Collections.Generic;
using Newtonsoft.Json;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class DescribeStreamResult
    {
        /// <summary>
        /// 通道名称
        /// </summary>
        [JsonProperty("stream_name")]
        public string StreamName { get; set; }

        /// <summary>
        /// 通道唯一标示符
        /// </summary>
        [JsonProperty("stream_id")]
        public string StreamId { get; set; }

        /// <summary>
        /// 通道创建时间戳
        /// </summary>
        [JsonProperty("create_time")]
        public long CreateTime { get; set; }

        /// <summary>
        /// 通道最近一次修改时间戳
        /// </summary>
        [JsonProperty("last_modified_time")]
        public long LastModifiedTime { get; set; }

        /// <summary>
        /// 数据保留时长
        /// </summary>
        [JsonProperty("retention_period")]
        public int RetentionPeriod { get; set; }

        /// <summary>
        /// 通道的当前状态
        /// CREATING
        /// RUNNING
        /// TERMINATING
        /// FROZEN
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// 通道类型
        /// </summary>
        [JsonProperty("stream_type")]
        public string StreamType { get; set; }

        /// <summary>
        /// 通道源数据类型
        /// </summary>
        [JsonProperty("data_type")]
        public string DataType { get; set; }

        /// <summary>
        /// 用于描述用户JOSN、CSV格式的源数据结构，采用Avro Schema的语法描述。Avro介绍请参见http://avro.apache.org/docs/current/#schemas
        /// </summary>
        [JsonProperty("data_schema")]
        public string DataSchema { get; set; }

        /// <summary>
        /// 可写分区总数量（包含ACTIVE与DELETED状态的分区）。
        /// </summary>
        [JsonProperty("writable_partition_count")]
        public string WritablePartitionCount { get; set; }

        /// <summary>
        /// 可读分区总数量（只包含ACTIVE状态的分区）。
        /// </summary>
        [JsonProperty("readable_partition_count")]
        public string ReadablePartitionCount { get; set; }

        /// <summary>
        /// 通道的标签
        /// </summary>
        [JsonProperty("tags")]
        public List<Tag> Tags { get; set; }

        /// <summary>
        /// 通道的分区列表
        /// </summary>
        [JsonProperty("partitions")]
        public List<PartitionResult> Partitions { get; set; }

        /// <summary>
        /// 是否还有更多满足请求条件的分区
        /// 是：true
        /// 否：false
        /// </summary>
        [JsonProperty("has_more_partitions")]
        public bool HasMorePartitions { get; set; }
    }
}
