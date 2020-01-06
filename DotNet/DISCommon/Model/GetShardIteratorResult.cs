using Newtonsoft.Json;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class GetShardIteratorResult
    {
        /// <summary>
        /// 数据游标。
        /// 取值范围：1~512个字符。
        /// 说明
        /// 数据游标有效期为5分钟
        /// </summary>
        [JsonProperty("partition_cursor")]
        public string ShardIterator { get; set; }

        public override string ToString()
        {
            return "GetShardIteratorResult [shardIterator=" + ShardIterator + "]";
        }
    }
}
