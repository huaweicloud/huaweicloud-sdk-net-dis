namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class SplitShardResult
    {
        public bool SplitRequestSubmitted { get; set; }

        public SplitShardResult()
        {
            SplitRequestSubmitted = false;
        }
    }
}
