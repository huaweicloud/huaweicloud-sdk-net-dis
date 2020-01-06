using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class StreamTransferTaskDetailResult
    {
        /// <summary>
        /// 转储任务名称。
        /// </summary>
        [JsonProperty("stream_name")]
        public String StreamName;

        /// <summary>
        /// 转储任务名称。
        /// </summary>
        [JsonProperty("task_name")]
        public String TaskName;

        /// <summary>
        /// 转储任务创建时间戳。
        /// </summary>
        [JsonProperty("create_time")]
        public long CreateTime;

        /// <summary>
        /// 最近一次转储时间戳。
        /// </summary>
        [JsonProperty("last_transfer_timestamp")]
        public long LastTransferTimestamp;

        /// <summary>
        /// 转储任务状态。
        /// ABNORMAL：异常
        /// RUNNING：运行中
        /// </summary>
        [JsonProperty("state")]
        //public TransferTaskStateType State;
        public string State;

        /// <summary>
        /// 转储任务类型。
        /// OBS
        /// MRS
        /// DLI
        /// CLOUDTABLE
        /// DWS
        /// </summary>
        [JsonProperty("destination_type")]
        public String DestinationType;

        /// <summary>
        /// 分区转储详情列表。
        /// </summary>
        [JsonProperty("partitions")]
        public List<PartitionTask> Partitions;

        /// <summary>
        /// 转储目的地为OBS的参数列表。暂时不支持同时转储至多个目的地。
        /// 缺省值：空。
        /// 配置为空表示数据不转储到OBS。
        /// </summary>
        [JsonProperty("obs_destination_description")]
        public ObsDestinationDescriptorEntity ObsDestinationDescription;

        /// <summary>
        /// 转储目的地为MRS的参数列表。暂时不支持同时转储至多个目的地。
        /// 缺省值：空。
        /// 配置为空表示数据不转储到MRS。
        /// </summary>
        [JsonProperty("mrs_destination_description")]
        public MRSDestinationDescriptorEntity MrsDestinationDescription;

        /// <summary>
        /// 转储目的地为DLI的参数列表。暂时不支持同时转储至多个目的地。
        /// 缺省值：空。
        /// 配置为空表示数据不转储到DLI。 
        /// </summary>
        [JsonProperty("dli_destination_description")]
        public DLIDestinationDescriptorEntity DliDestinationDescription;

        /// <summary>
        /// 转储目的地为Cloudtable的参数列表。
        /// 暂不支持同时转储至多个目的地。
        /// 缺省值：空。
        /// 配置为空表示数据不转储到CloudTable的HBase表或OpenTSDB。
        /// </summary>
        [JsonProperty("cloudtable_destination_description")]
        public CloudtableDestinationDescriptorEntity CloudtableDestinationDescription;

        /// <summary>
        /// 转储目的地为DWS的参数列表。暂时不支持同时转储至多个目的地。
        /// 缺省值：空。
        /// 配置为空表示数据不转储到DWS。
        /// </summary>
        [JsonProperty("dws_destination_description")]
        public DwsDestinationDescriptorEntity DwsDestinationDescription;
    }


    public class PartitionTask
    {
        /// <summary>
        /// 分区的唯一标识符。
        /// </summary>
        [JsonProperty("partitionId")]
        public string PartitionId;

        /// <summary>
        /// 当前分区脏数据的记录总数。
        /// </summary>
        [JsonProperty("discard")]
        public int Discard;

        /// <summary>
        /// 转储任务状态。
        /// ABNORMAL：异常
        /// RUNNING：运行中
        /// </summary>
        [JsonProperty("state")]
        public string State;

        /// <summary>
        /// 最近一次转储时间戳。
        /// </summary>
        [JsonProperty("last_transfer_timestamp")]
        public long LastTransferTimestamp;

        /// <summary>
        /// 最近一次转储的offset。
        /// </summary>
        [JsonProperty("last_transfer_offset")]
        public long LastTransferOffset;
    }
}
