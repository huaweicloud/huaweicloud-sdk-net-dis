using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class ErrorEntity
    {
        /// <summary>
        /// 错误编码
        /// </summary>
        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [JsonProperty("message")]
        public string ErrorMessage { get; set; }
    }
}
