using Com.Bigdata.Dis.Sdk.DISCommon.Model;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Interface
{
    /// <summary>
    /// 通道服务6个接口
    /// </summary>
    public interface IStreamService
    {
        ResponseResult CreateStream(CreateStreamRequest createStreamRequest);

        ResponseResult DeleteStream(DescribeStreamRequest describeStreamRequest);

        DescribeStreamListResult DescribeStreamList(DescribeStreamRequest describeStreamRequest);

        DescribeStreamResult DescribeStream(DescribeStreamRequest describeStreamRequest);

        GetMetricResult GetStreamMetricInfo(string streamName, GetStreamMetricRequest metricRequest);

        GetStreamConsumingResult GetStreamConsumingInfo(string streamName, string appName, GetStreamConsumingRequest consumingRequest);
    }
}
