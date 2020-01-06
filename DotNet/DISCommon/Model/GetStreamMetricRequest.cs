using Newtonsoft.Json;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class GetStreamMetricRequest
    {
        /// <summary>
        /// 通道监控指标。
        ///total_put_bytes_per_stream：总输入流量（KB）
        ///total_get_bytes_per_stream：总输出流量（KB）
        ///total_put_records_per_stream：总输入记录数（个）
        ///total_get_records_per_stream：总输出记录数（个）
        ///total_put_req_latency：上传请求平均处理时间（毫秒）
        ///total_get_req_latency：下载请求平均处理时间（毫秒）
        ///total_put_req_suc_per_stream：上传请求成功次数（个）
        ///total_get_req_suc_per_stream：下载请求成功次数（个）
        ///traffic_controll_put：因流控拒绝的上传请求次数 （个）
        ///traffic_controll_get：因流控拒绝的下载请求次数 （个）
        /// </summary>
        [JsonProperty("label")]
        public string Label { get; set; }

        /// <summary>
        /// 监控开始时间点，10位时间戳。
        /// </summary>
        [JsonProperty("start_time")]
        public long StartTime { get; set; }

        /// <summary>
        /// 监控结束时间点，10为时间戳。
        /// </summary>
        [JsonProperty("end_time")]
        public long EndTime { get; set; }
    }
}
