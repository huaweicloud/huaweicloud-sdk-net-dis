using System.IO;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Model
{
    public class PutRecordRequest
    {
        public string PartitionKey { get; set; }

        public string StreamName { get; set; }

        public MemoryStream Data { get; set; }

        public string SequenceNumberForOrdering { get; set; }

        public string ExplicitHashKey { get; set; }
    }
}
