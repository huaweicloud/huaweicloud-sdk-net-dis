using Newtonsoft.Json;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class GetCheckpointRequest
    {
        /// <summary>
        /// Console控制台已创建的通道名称。通道名称由字母、数字、下划线和中划线组成，长度为1～64字符。
        /// </summary>
        [JsonProperty("stream_name")]
        public string StreamName { get; set; }

        /// <summary>
        /// 分区的唯一标识符
        /// </summary>
        [JsonProperty("partition_id")]
        public string ShardId { get; set; }

        /// <summary>
        /// Checkpoint类型
        /// LAST_READ：在数据库中只记录序列号。
        /// </summary>
        [JsonProperty("checkpoint_type")]
        public string CheckpointType { get; set; }

        /// <summary>
        /// 应用ID，用户数据消费程序的唯一标识符。应用名称由字母、数字、下划线和中划线组成，长度为1～50个字符。
        /// </summary>
        [JsonProperty("app_name")]
        public string AppId { get; set; }
    }
}
