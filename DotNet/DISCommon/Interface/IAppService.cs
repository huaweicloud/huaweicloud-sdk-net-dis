using Com.Bigdata.Dis.Sdk.DISCommon.Model;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Interface
{
    /// <summary>
    /// App服务4个接口
    /// </summary>
    public interface IAppService
    {
        ResponseResult CreateApp(AppRequest getShardIteratorParam);

        ResponseResult DeleteApp(AppRequest createAppRequest);

        DescribeAppResult DescribeApp(AppRequest appRequest);

        DescribeAppListResult DescribeAppList(DescribeAppListRequest describeAppRequest);
    }
}
