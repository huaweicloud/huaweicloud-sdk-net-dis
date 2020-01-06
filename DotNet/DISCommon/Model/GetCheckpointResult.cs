using Newtonsoft.Json;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class GetCheckpointResult
    {
        /// <summary>
        /// 序列号。序列号是每个记录的唯一标识符。
        /// </summary>
        [JsonProperty("sequence_number")]
        public string ShardIterator { get; set; }

        /// <summary>
        /// 用户消费程序端的元数据信息。
        /// </summary>
        [JsonProperty("metadata")]
        public string Metadata { get; set; }

        public override string ToString()
        {
            return "GetCheckpointResult [sequence_number=" + ShardIterator +
                ",metadata=" + Metadata +
                "]";
        }
    }
}
