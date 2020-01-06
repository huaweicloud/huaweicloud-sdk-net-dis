using Newtonsoft.Json;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class CommitCheckpointRequest
    {
        /// <summary>
        /// Console控制台已创建的通道名称
        /// </summary>
        [JsonProperty("stream_name")]
        public string StreamName { get; set; }

        /// <summary>
        /// APP的名称，用户数据消费程序的唯一标识符。
        /// 应用名称由字母、数字、下划线和中划线组成，长度为1～50个字符
        /// </summary>
        [JsonProperty("app_name")]
        public string AppName { get; set; }

        /// <summary>
        /// 分区的唯一标识符。
        /// </summary>
        [JsonProperty("partition_id")]
        public string PartitionId { get; set; }

        /// <summary>
        /// 序列号。序列号是每个记录的唯一标识符。
        /// </summary>
        [JsonProperty("sequence_number")]
        public string SequenceNumber { get; set; }

        /// <summary>
        /// 用户消费程序端的元数据信息。
        /// 元数据信息的最大长度为1000个字符。
        /// </summary>
        [JsonProperty("metadata", NullValueHandling = NullValueHandling.Ignore)]
        public string Metadata { get; set; }

        /// <summary>
        /// Checkpoint类型。
        /// LAST_READ：在数据库中只记录序列号。
        /// </summary>
        [JsonProperty("checkpoint_type")]
        public string CheckpointType { get; set; }
    }
}
