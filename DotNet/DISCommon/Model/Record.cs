using System.IO;
using Newtonsoft.Json;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class Record
    {
        /// <summary>
        /// 用户上传数据时设置的partition_key。
        /// 说明: 上传数据时，如果传了partition_key参数，则下载数据时可返回此参数。
        /// 如果上传数据时，未传partition_key参数，而是传入partition_id，则不返回partition_id,即partition_key为空
        /// </summary>
        [JsonProperty("partition_key")]
        public string PartitionKey { get; set; }

        /// <summary>
        /// 序列号。序列号是每个记录的唯一标识符。
        /// 序列号由DIS在数据生产者调用PutRecords操作以添加数据到DIS数据通道时DIS服务自动分配的。
        /// 同一分区键的序列号通常会随时间变化增加。
        /// PutRecords请求之间的时间段越长，序列号越大。
        /// </summary>
        [JsonProperty("sequence_number")]
        public string SequenceNumber { get; set; }

        /// <summary>
        /// 数据。
        /// </summary>
        [JsonProperty("data")]
        public byte[] Data { get; set; }

        /// <summary>
        /// 记录写入DIS的时间戳。
        /// </summary>
        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        /// <summary>
        /// 时间戳类型。
        /// CreateTime：创建时间。
        /// </summary>
        [JsonProperty("timestamp_type")]
        public string TimestampType { get; set; }
    }
}
