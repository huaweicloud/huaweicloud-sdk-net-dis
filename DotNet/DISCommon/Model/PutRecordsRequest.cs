using System.Collections.Generic;
using Newtonsoft.Json;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class PutRecordsRequest
    {
        /// <summary>
        /// Console控制台已创建的通道名称。
        /// 通道名称由字母、数字、下划线和中划线组成，长度为1～64字符。
        /// </summary>
        [JsonProperty("stream_name")]
        public string StreamName { get; set; }

        /// <summary>
        /// List record，record为对象结构体。
        /// </summary>
        [JsonProperty("records", NullValueHandling = NullValueHandling.Ignore)]
        public List<PutRecordsRequestEntry> Records { get; set; }

        /// <summary>
        /// 实时文件数据的目标存储文件。
        /// 目标存储文件名长度为1 ~128个字符，不能包含特殊字符。
        /// </summary>
        [JsonProperty("file_name", NullValueHandling = NullValueHandling.Ignore)]
        public string FileName { get; set; }

        /// <summary>
        /// 本地磁盘文件路径
        /// </summary>
        [JsonProperty("file_path", NullValueHandling = NullValueHandling.Ignore)]
        public string FilePath { get; set; }
    }
}
