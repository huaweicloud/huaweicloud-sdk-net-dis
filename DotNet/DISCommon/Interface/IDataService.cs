using Com.Bigdata.Dis.Sdk.DISCommon.Model;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Interface
{
    /// <summary>
    /// 数据服务4个接口
    /// </summary>
    public interface IDataService
    {
        PutRecordsResult PutRecords(PutRecordsRequest putRecordsParam);

        GetShardIteratorResult GetShardIterator(GetShardIteratorRequest getShardIteratorParam);

        GetRecordsResult GetRecords(GetRecordsRequest getRecordsParam);

        PutRecordsResult PutFileRecords(PutRecordsRequest putRecordsParam);
    }
}
