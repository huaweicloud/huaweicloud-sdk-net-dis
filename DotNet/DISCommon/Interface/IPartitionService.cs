using Com.Bigdata.Dis.Sdk.DISCommon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Interface
{
    /// <summary>
    /// 分区服务2个接口
    /// </summary>
    public interface IPartitionService
    {
        UpdateShardsResult UpdatePartition(UpdateShardsRequest updateShardsRequest);

        GetMetricResult GetPartitionMetricInfo(string streamName, string partitionId, GetPartitionMetricRequest metricRequest);
    }
}
