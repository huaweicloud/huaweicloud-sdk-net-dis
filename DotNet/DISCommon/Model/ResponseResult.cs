using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class ResponseResult
    {
        /// <summary>
        /// 状态码
        /// </summary>
        [JsonProperty("status_code", NullValueHandling = NullValueHandling.Ignore)]
        public string StatusCode { get; set; }

        /// <summary>
        /// 响应消息体
        /// </summary>
        [JsonProperty("content", NullValueHandling = NullValueHandling.Ignore)]
        public string Content { get; set; }

        /// <summary>
        /// 错误码
        /// </summary>
        [JsonProperty("errorCode", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorCode { get; set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        [JsonProperty("message", NullValueHandling = NullValueHandling.Ignore)]
        public string ErrorMessage { get; set; }

        public override string ToString()
        {
            return "ResponseResult{" +
                   "status_code='" + StatusCode + '\'' +
                   ", content='" + Content + '\'' +
                   ", errorCode='" + ErrorCode + '\'' +
                   ", message='" + ErrorMessage + '\'' +
                   '}';
        }
    }
}
