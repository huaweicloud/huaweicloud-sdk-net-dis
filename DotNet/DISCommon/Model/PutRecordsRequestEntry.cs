using System.IO;
using Newtonsoft.Json;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class PutRecordsRequestEntry
    {
        /// <summary>
        /// 需要上传的数据。
        /// </summary>
        [JsonProperty("data")]
        public byte[] Data { get; set; }

        /// <summary>
        /// 用于明确数据需要写入分区的哈希值，此哈希值将覆盖“partition_key”的哈希值
        /// </summary>
        [JsonProperty("explicit_hash_key", NullValueHandling = NullValueHandling.Ignore)]
        public string ExplicitHashKey { get; set; }

        /// <summary>
        /// 分区的唯一标识符。
        /// </summary>
        [JsonProperty("partition_id", NullValueHandling = NullValueHandling.Ignore)]
        public string PartitionId { get; set; }

        /// <summary>
        /// 数据将写入的分区
        /// 如果传了partition_id参数，则优先使用partition_id参数。如果partition_id没有传，则使用partition_key
        /// </summary>
        [JsonProperty("partition_key", NullValueHandling = NullValueHandling.Ignore)]
        public string PartitionKey { get; set; }

        /// <summary>
        /// 上传实时文件数据的配置信息
        /// </summary>
        [JsonProperty("extended_info", NullValueHandling = NullValueHandling.Ignore)]
        public PutRecordsRequestEntryExtendedInfo ExtenedInfo { get; set; }

    }


    public class PutRecordsRequestEntryExtendedInfo
    {
        /// <summary>
        /// 实时文件数据的目标存储文件。
        /// 目标存储文件名长度为1~128个字符，不能包含特殊字符
        /// </summary>
        [JsonProperty("file_name")]
        public string FileName { get; set; }

        /// <summary>
        /// 实时文件数据的ID，用户自行指定
        /// 长度为1~32个字符，由字母和数字组成
        /// </summary>
        [JsonProperty("deliver_data_id")]
        public string DeliverDataId { get; set; }

        /// <summary>
        /// 待上传文件的块序列号。起始序列号为0，当收到序列号为0的请求系统默认上传新文件或重新上传文件
        /// </summary>
        [JsonProperty("sequence_number")]
        public long SequenceNumber { get; set; }

        /// <summary>
        /// 当前文件块是否为最后一个文件块
        /// 是：true; 否：false
        /// </summary>
        [JsonProperty("end_flag")]
        public bool EndFlag { get; set; }
    }
}
