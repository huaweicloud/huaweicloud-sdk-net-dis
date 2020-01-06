using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class DescribeStreamRequestEntity
    {
        /// <summary>
        /// 通道名称
        /// </summary>
        [JsonProperty("stream-name")]
        public string StreamName { get; set; }

        /// <summary>
        /// 分区数
        /// </summary>
        [JsonProperty("partition_count")]
        public int PartitionCount;
    }
}
