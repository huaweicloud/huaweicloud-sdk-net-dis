using Newtonsoft.Json;
using System.Collections.Generic;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class GetPartitionMetricRequest
    {
        /// <summary>
        /// 分区监控指标
        /// total_put_bytes_per_partition：分区总输入流量（KB）
        /// total_get_bytes_per_partition：分区总输出流量（KB）
        /// total_put_records_per_partition：分区总输入记录数（个）
        /// total_get_records_per_partition：分区总输出记录数（个）
        /// </summary>
        [JsonProperty("label")]
        public string Label { get; set; }

        /// <summary>
        /// 监控开始时间点，10位时间戳
        /// </summary>
        [JsonProperty("start_time")]
        public long StartTime { get; set; }

        /// <summary>
        /// 监控结束时间点，10位时间戳
        /// </summary>
        [JsonProperty("end_time")]
        public long EndTime { get; set; }
    }
}
