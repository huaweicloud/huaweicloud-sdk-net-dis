using Newtonsoft.Json;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class PartitionResult
    {
        /// <summary>
        /// 分区的当前状态。
        /// CREATING
        /// ACTIVE
        /// DELETED
        /// EXPIRED
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// 分区的唯一标识符
        /// </summary>
        [JsonProperty("partition_id")]
        public string PartitionId { get; set; }

        /// <summary>
        /// 分区的可能哈希键值范围
        /// </summary>
        [JsonProperty("hash_range")]
        public string HashRange { get; set; }

        /// <summary>
        /// 分区的序列号范围
        /// </summary>
        [JsonProperty("sequence_number_range")]
        public string SequenceNumberRange { get; set; }

    }
}
