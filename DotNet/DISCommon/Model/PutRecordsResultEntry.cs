using Newtonsoft.Json;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class PutRecordsResultEntry
    {
        /// <summary>
        /// 分区ID。
        /// </summary>
        [JsonProperty("partition_id")]
        public string ShardId { get; set; }

        /// <summary>
        /// 序列号。序列号是每个记录的唯一标识符。
        /// 序列号由DIS在数据生产者调用PutRecords操作以添加数据到DIS数据通道时DIS服务自动分配的。
        /// 同一分区键的序列号通常会随时间变化增加。
        /// PutRecords请求之间的时间段越长，序列号越大。
        /// </summary>
        [JsonProperty("sequence_number")]
        public string SequenceNumber { get; set; }

        /// <summary>
        /// 错误码。
        /// </summary>
        [JsonProperty("error_code")]
        public string ErrorCode { get; set; }

        /// <summary>
        /// 错误消息。
        /// </summary>
        [JsonProperty("error_message")]
        public string ErrorMessage { get; set; }

        public override string ToString()
        {
            return "PutRecordsResultEntry [shardId=" + ShardId + ", sequenceNumber=" + SequenceNumber + ", errorCode="
                   + ErrorCode + ", errorMessage=" + ErrorMessage + "]";
        }
    }
}
