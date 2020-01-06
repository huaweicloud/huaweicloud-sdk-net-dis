using Newtonsoft.Json;
using System.Collections.Generic;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class GetStreamConsumingResult
    {
        /// <summary>
        /// 通道名称。
        /// </summary>
        [JsonProperty("stream_name")]
        public string StreamName { get; set; }

        /// <summary>
        /// App名称。
        /// </summary>
        [JsonProperty("app_name")]
        public string AppName { get; set; }

        /// <summary>
        /// 每个分区的消费状态。
        /// </summary>
        [JsonProperty("partition_consuming_states")]
        public List<PartitionConsumingState> PartitionConsumingState { get; set; }

        /// <summary>
        /// 表示获取流消费信息时的起始partition。
        /// 取值范围为0~(partition总数量-1)。
        /// 默认值为0。
        /// </summary>
        [JsonProperty("has_more")]
        public bool HasMore { get; set; }
    }

    public class PartitionConsumingState
    {
        /// <summary>
        /// 分区的唯一标识符。
        /// </summary>
        [JsonProperty("partition_id")]
        public string PartitionId { get; set; }

        /// <summary>
        /// 序列号。序列号是每个记录的唯一标识符。
        /// 说明：如果没有提交或者已经删除，则为-1。
        /// </summary>
        [JsonProperty("sequence_number")]
        public string SequenceNumber { get; set; }

        /// <summary>
        /// 分区数据最新的offset。
        /// </summary>
        [JsonProperty("latest_offset")]
        public long LatestOffset { get; set; }

        /// <summary>
        /// 分区数据最旧的offset。
        /// </summary>
        [JsonProperty("earliest_offset")]
        public long EarliestOffset { get; set; }

        /// <summary>
        /// Checkpoint类型，配置为LAST_READ。
        /// </summary>
        [JsonProperty("checkpoint_type")]
        public string CheckpointType { get; set; }

        /// <summary>
        /// 用户新增checkpoint时的metadata
        /// </summary>
        [JsonProperty("metadata")]
        public string Metadata { get; set; }
    }
}
