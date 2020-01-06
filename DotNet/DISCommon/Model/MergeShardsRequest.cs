namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class MergeShardsRequest
    {
        public string StreamName { get; set; }

        public string ShardToMerge { get; set; }

        public string AdjacentShard { get; set; }

        public string StreamRowkey { get; set; }
    }
}
