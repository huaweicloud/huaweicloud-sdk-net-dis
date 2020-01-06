namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class GetRecordsWithCheckpointingRequest
    {
        public string StreamName { get; set; }

        public string ShardId { get; set; }

        public string AppId { get; set; }

        public int Limit { get; set; }


    }
}
