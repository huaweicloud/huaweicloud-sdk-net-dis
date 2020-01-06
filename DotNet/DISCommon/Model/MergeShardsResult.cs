namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class MergeShardsResult
    {
        public bool MergeRequestSubmitted { get; set; }

        public MergeShardsResult()
        {
            MergeRequestSubmitted = false;
        }
    }
}
