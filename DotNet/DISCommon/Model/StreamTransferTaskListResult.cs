using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class StreamTransferTaskListResult
    {
        /// <summary>
        /// 转储任务总数。
        /// </summary>
        [JsonProperty("total_number")]
        public int total_number;

        /// <summary>
        /// 转储任务列表。
        /// </summary>
        [JsonProperty("tasks")]
        public List<Task> tasks;
    }

    public class Task
    {
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
        public TransferTaskStateType State;

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
    }

}
