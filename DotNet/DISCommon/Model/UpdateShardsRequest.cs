using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class UpdateShardsRequest
    {
        /// <summary>
        /// Console控制台已创建的通道名称。
        /// </summary>
        [JsonProperty("stream_name")]
        public string StreamName { get; set; }

        /// <summary>
        /// 变更的目标分区数量。
        /// 取值为大于0的整数。
        /// 设置的值大于当前分区数量表示扩容，小于当前分区数量表示缩容。
        /// 说明
        /// 每个通道在一小时内扩容和缩容总次数最多5次，且一小时内扩容或缩容操作有一次成功则最近一小时内不允许再次进行扩容或缩容操作。
        /// </summary>
        [JsonProperty("target_partition_count")]
        public int TargetPartitionCount { get; set; }
    }
}
