using Com.Bigdata.Dis.Sdk.DISCommon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Interface
{
    /// <summary>
    /// Checkpoint服务3个接口
    /// </summary>
    public interface ICheckpointService
    {
        ResponseResult CommitCheckpoint(CommitCheckpointRequest commitCheckpointParam);

        ResponseResult DeleteCheckpoint(CheckPointRequest checkPointRequest);

        GetCheckpointResult GetCheckpoint(GetCheckpointRequest getCheckpointRequest);
    }
}
