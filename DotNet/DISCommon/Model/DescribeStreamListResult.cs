using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class DescribeStreamListResult
    {
        /// <summary>
        /// 当前租户所有通道数量
        /// </summary>
        [JsonProperty("total_number")]
        public int TotalNumber;

        /// <summary>
        /// 满足当前请求条件的通道名称的列表。
        /// </summary>
        [JsonProperty("stream_names")]
        public List<String> StreamNames;

        /// <summary>
        /// 是否还有更多满足条件的通道。
        /// </summary>
        [JsonProperty("has_more_streams")]
        public Boolean HasMoreStreams;

    }
}
