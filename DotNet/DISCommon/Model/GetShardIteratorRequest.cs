using Newtonsoft.Json;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class GetShardIteratorRequest
    {
        /// <summary>
        /// Console控制台已创建的通道名称
        /// </summary>
        [JsonProperty("stream-name")]
        public string StreamName { get; set; }

        /// <summary>
        /// 分区值。取值范围：0~2147483647。
        /// </summary>
        [JsonProperty("partition-id")]
        public string ShardId { get; set; }

        /// <summary>
        /// 游标类型。
        ///	AT_SEQUENCE_NUMBER：从特定序列号所在的记录开始读取。此类型为默认游标类型。
        ///	AFTER_SEQUENCE_NUMBER：从特定序列号后的记录开始读取。
        ///	TRIM_HORIZON：从分区中时间最长的记录开始读取。
        ///	LATEST：在分区中最新的记录之后开始读取，以确保始终读取分区中的最新数据。
        ///	AT_TIMESTAMP：从特定时间戳开始读取。
        /// </summary>
        [JsonProperty("cursor-type", NullValueHandling = NullValueHandling.Ignore)]
        public string ShardIteratorType { get; set; }

        /// <summary>
        /// 序列号。序列号是每个记录的唯一标识符。序列号由DIS在数据生产者调用PutRecords操作以添加数据到DIS数据通道时DIS服务自动分配的。同一分区键的序列号通常会随时间变化增加。PutRecords请求之间的时间段越长，序列号越大。
        ///取值范围：0~9223372036854775807
        /// </summary>
        [JsonProperty("starting-sequence-number", NullValueHandling = NullValueHandling.Ignore)]
        public string StartingSequenceNumber { get; set; }

        /// <summary>
        /// 开始读取数据记录的时间戳，与游标类型▪AT_TIMESTAMP：从特定时间戳开始读取...一起使用
        /// </summary>
        [JsonProperty("timestamp", NullValueHandling = NullValueHandling.Ignore)]
        public long Timestamp { get; set; }


    }
}
