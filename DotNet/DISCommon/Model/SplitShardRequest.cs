namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class SplitShardRequest
    {
        public string StreamName { get; set; }

        public string ShardToSplit { get; set; }

        public string StartingHashKey { get; set; }

        public string StreamRowkey { get; set; }
    }
}
