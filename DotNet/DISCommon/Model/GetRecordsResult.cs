using System.Collections.Generic;
using Newtonsoft.Json;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class GetRecordsResult
    {
        /// <summary>
        /// List Record，Record为对象结构体。
        /// </summary>
        [JsonProperty("records")]
        public List<Record> Records { get; set; }

        /// <summary>
        /// 下一个迭代器。
        /// 说明:　数据游标有效期为5分钟。
        /// </summary>
        [JsonProperty("next_partition_cursor")]
        public string NextShardIterator { get; set; }
    }
}
