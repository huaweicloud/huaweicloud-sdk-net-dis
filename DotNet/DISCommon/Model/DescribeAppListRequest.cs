using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class DescribeAppListRequest
    {
        /// <summary>
        /// 单次请求返回通道列表的最大数量
        /// </summary>
        [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
        public int Limit;

        /// <summary>
        /// 从该通道开始返回app列表，返回的app列表不包括此app名称。
        /// </summary>
        [JsonProperty("start_app_name", NullValueHandling = NullValueHandling.Ignore)]
        public String StartAppName;
    }
}
