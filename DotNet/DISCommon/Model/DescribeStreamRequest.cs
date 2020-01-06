using Newtonsoft.Json;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class DescribeStreamRequest
    {
        private const int PartitionLimits = 1000;

        /// <summary>
        /// 需要查询的通道名称
        /// </summary>
        [JsonProperty("stream_name", NullValueHandling = NullValueHandling.Ignore)]
        public string StreamName { get; set; }

        /// <summary>
        /// 从该分区值开始返回分区列表，返回的分区列表不包括此分区
        /// </summary>
        [JsonProperty("start_partitionId", NullValueHandling = NullValueHandling.Ignore)]
        public string StartPartitionId { get; set; }

        /// <summary>
        /// 单次请求返回的最大分区数。
        /// 取值范围：1~1000。
        /// 默认值：100。
        /// </summary>
        [JsonProperty("limit_partitions", NullValueHandling = NullValueHandling.Ignore)]
        public int LimitPartitions;

        /// <summary>
        /// 单次请求返回通道列表的最大数量。
        /// 取值范围：1~100。
        /// 默认值：10。
        /// </summary>
        [JsonProperty("limit", NullValueHandling = NullValueHandling.Ignore)]
        public int Limit;

        /// <summary>
        /// 从该通道开始返回通道列表，返回的通道列表不包括此通道名称。
        /// 说明: 查询的通道列表根据通道名称来排序。
        /// </summary>
        [JsonProperty("start_stream_name", NullValueHandling = NullValueHandling.Ignore)]
        public string StartStreamName;

        public DescribeStreamRequest()
        {
            LimitPartitions = PartitionLimits;
        }

        public int GetLimitPartitions()
        {
            return LimitPartitions;
        }

        public void SetLimitPartitions(int limitPartitions)
        {
            this.LimitPartitions = (limitPartitions > PartitionLimits) ? PartitionLimits : limitPartitions;
        }

        public override string ToString()
        {
            return "DescribeStreamRequest{" +
                   "streamName='" + StreamName + '\'' +
                   ", startPartitionId='" + StartPartitionId + '\'' +
                   ", limitPartitions=" + LimitPartitions +
                   '}';
        }

        public override bool Equals(object o)
        {
            if (this == o) return true;
            if (o == null) return false;

            var that = (DescribeStreamRequest)o;

            return LimitPartitions == that.LimitPartitions
                   && string.Equals(StreamName, that.StreamName)
                   && string.Equals(StartPartitionId, that.StartPartitionId);
        }

        public override int GetHashCode()
        {
            var result = StreamName.GetHashCode();
            result = 31 * result + StartPartitionId.GetHashCode();
            result = 31 * result + LimitPartitions;
            return result;
        }
    }
}
