using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class AddTransferTasksRequest
    {
        /// <summary>
        /// 转储任务类型。
        /// OBS
        /// MRS
        /// DLI
        /// CLOUDTABLE
        /// DWS
        /// </summary>
        [JsonProperty("destination_type")]
        public string DestinationType { get; set; }

        /// <summary>
        /// 转储目的地为OBS的参数列表。暂时不支持同时转储至多个目的地。
        /// 缺省值：空。
        /// 配置为空表示数据不转储到OBS。
        /// </summary>
        [JsonProperty("obs_destination_descriptor", NullValueHandling = NullValueHandling.Ignore)]
        public ObsDestinationDescriptorEntity ObsDestinationDescriptor { get; set; }

        /// <summary>
        /// 转储目的地为MRS的参数列表。暂时不支持同时转储至多个目的地。
        /// 缺省值：空。
        /// 配置为空表示数据不转储到MRS。
        /// </summary>
        [JsonProperty("mrs_destination_descriptor", NullValueHandling = NullValueHandling.Ignore)]
        public MRSDestinationDescriptorEntity MRSDestinationDescriptor { get; set; }

        /// <summary>
        /// 	
        /// 转储目的地为DLI的参数列表。暂时不支持同时转储至多个目的地。
        /// 缺省值：空。
        /// 配置为空表示数据不转储到DLI。
        /// </summary>
        [JsonProperty("dli_destination_descriptor", NullValueHandling = NullValueHandling.Ignore)]
        public DLIDestinationDescriptorEntity DLIDestinationDescriptor { get; set; }

        /// <summary>
        /// 转储目的地为Cloudtable的参数列表。
        /// 暂不支持同时转储至多个目的地。
        /// 缺省值：空。
        /// 配置为空表示数据不转储到CloudTable的HBase表或OpenTSDB。
        /// </summary>
        [JsonProperty("cloudtable_destination_descriptor", NullValueHandling = NullValueHandling.Ignore)]
        public CloudtableDestinationDescriptorEntity CloudtableDestinationDescriptor { get; set; }

        /// <summary>
        /// 转储目的地为DWS的参数列表。暂时不支持同时转储至多个目的地。
        /// 缺省值：空。
        /// 配置为空表示数据不转储到DWS。
        /// </summary>
        [JsonProperty("dws_destination_descriptor", NullValueHandling = NullValueHandling.Ignore)]
        public DwsDestinationDescriptorEntity DwsDestinationDescriptor { get; set; }
    }
}
