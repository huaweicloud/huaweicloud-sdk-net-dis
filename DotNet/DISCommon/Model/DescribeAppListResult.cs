using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class DescribeAppListResult
    {
        /// <summary>
        /// 是否还有更多满足条件的App。
        /// </summary>
        [JsonProperty("has_more_app")]
        public Boolean HasMoreApp;

        /// <summary>
        /// 满足当前请求条件的App的列表
        /// </summary>
        [JsonProperty("apps")]
        public List<App> Apps;
    }

    public class App
    {
        /// <summary>
        /// App的id
        /// </summary>
        [JsonProperty("app_id")]
        public String AppId;

        /// <summary>
        /// App的名字
        /// </summary>
        [JsonProperty("app_name")]
        public String AppName;

        /// <summary>
        /// App创建的时间，单位ms
        /// </summary>
        [JsonProperty("create_time")]
        public long CreateTime;
    }

}
