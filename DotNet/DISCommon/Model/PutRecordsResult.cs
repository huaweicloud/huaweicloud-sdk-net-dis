using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class PutRecordsResult
    {
        /// <summary>
        /// 上传失败的数据数量。
        /// </summary>
        [JsonProperty("failed_record_count")]
        public int FailedRecordCount { get; set; }

        /// <summary>
        /// List record，record为对象结构体。
        /// </summary>
        [JsonProperty("records")]
        public List<PutRecordsResultEntry> Records { get; set; }
    }
}
