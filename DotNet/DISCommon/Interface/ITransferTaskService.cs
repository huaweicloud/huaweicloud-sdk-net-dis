using Com.Bigdata.Dis.Sdk.DISCommon.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Interface
{
    /// <summary>
    /// 转储服务4个接口
    /// </summary>
    public interface ITransferTaskService
    {
        ResponseResult CreateTransferTask(string streamName, AddTransferTasksRequest addTransferTasksRequest);

        ResponseResult DeleteTransferTask(string streamName, string transferTaskName);

        StreamTransferTaskListResult GetStreamTransferTaskList(string streamName);

        StreamTransferTaskDetailResult GetStreamTransferTaskDetail(string streamName, string transferTaskName);
    }
}
