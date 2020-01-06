using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class UpdateShardsResult
    {
        /// <summary>
        /// 需要变更分区数量的流名称。
        /// </summary>
        [JsonProperty("stream_name")]
        public string StreamName { get; set; }

        /// <summary>
        /// 变更前的分区数量。
        /// </summary>
        [JsonProperty("current_partition_count")]
        public string CurrentPartitionCount { get; set; }

        /// <summary>
        /// 申请的目标分区数量。
        /// </summary>
        [JsonProperty("target_partition_count")]
        public string TargetPartitionCount { get; set; }

        public override string ToString()
        {
            return "UpdateShardsResult{" +
                   "stream_name='" + StreamName + '\'' +
                   ", current_partition_count='" + CurrentPartitionCount + '\'' +
                   ", target_partition_count='" + TargetPartitionCount + '\'' +
                   '}';
        }
    }
}
