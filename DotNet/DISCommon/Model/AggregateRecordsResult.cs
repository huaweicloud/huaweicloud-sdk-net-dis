namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class AggregateRecordsResult
    {
        public bool IsStreamAnalyticsSetSuccessful { get; set; }

        public string InputStreamName { get; set; }

        public string AppId { get; set; }

        public string OutputStreamName { get; set; }

    }
}
