using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class DescribeAppResult
    {
        /// <summary>
        /// APP的名称，用户数据消费程序的唯一标识符。
        /// 应用名称由字母、数字、下划线和中划线组成，长度为1～50个字符
        /// </summary>
        [JsonProperty("app_name")]
        public string AppName { get; set; }

        /// <summary>
        /// App的id
        /// </summary>
        [JsonProperty("app_id")]
        public string AppId { get; set; }

        /// <summary>
        /// App创建的时间，单位ms
        /// </summary>
        [JsonProperty("create_time")]
        public long CreateTime { get; set; }
    }
}
