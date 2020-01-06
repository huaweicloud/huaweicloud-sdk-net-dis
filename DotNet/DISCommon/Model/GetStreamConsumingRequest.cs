using Newtonsoft.Json;
using System.Collections.Generic;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class GetStreamConsumingRequest
    {
        /// <summary>
        /// Checkpoint类型。
        /// 配置为：LAST_READ。
        /// </summary>
        [JsonProperty("checkpoint_type")]
        public string CheckpointType { get; set; }

        /// <summary>
        /// 表示需要返回多少个parition的消费状态。
        /// 取值范围为1~100.
        /// 默认值为10。
        /// </summary>
        [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
        public long Limit { get; set; }

        /// <summary>
        /// 表示获取流消费信息时的起始partition。
        /// 取值范围为0~(partition总数量-1)。
        /// 默认值为0。
        /// </summary>
        [JsonProperty("start_partition_id", NullValueHandling = NullValueHandling.Ignore)]
        public string StartPartitionId { get; set; }
    }
}
