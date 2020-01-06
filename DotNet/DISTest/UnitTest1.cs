using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Com.Bigdata.Dis.Sdk.DISCommon.Model;
using Com.Bigdata.Dis.Sdk.DISCommon.Client;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DISTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string streamName = "dis-shaw";
            const string shardId = "shardId-0000000000";

            //上传数据
            RunProducerDemo(streamName, shardId);
            //下载数据
            RunConsumerDemo(streamName, shardId);
        }

        //[TestMethod]
        //public static DescribeStreamResult DescribeStream(string streamName, string startPartitionId, Int32? limitPartitions)
        //{
        //    DescribeStreamResult response = null;
        //    var dic = new DISIngestionClient();
        //    var request = new DescribeStreamRequest { StreamName = streamName };

        //    if (!string.IsNullOrWhiteSpace(startPartitionId))
        //    {
        //        request.StartPartitionId = startPartitionId;
        //    }

        //    if (limitPartitions != null)
        //    {
        //        request.LimitPartitions = limitPartitions.Value;
        //    }

        //    response = dic.DescribeStream(request);

        //    return response;
        //}

        //[TestMethod]
        //public static DescribeStreamResult CreateStream(string streamName, Int32 partitionCount)
        //{
        //    DescribeStreamResult response = null;
        //    var dic = new DISIngestionClient();
        //    var request = new DescribeStreamRequest { StreamName = streamName };
        //    DescribeStreamRequestEntity describeStreamRequestEntity = new DescribeStreamRequestEntity
        //    {
        //        StreamName = streamName,
        //        PartitionCount = partitionCount
        //    };
        //    request.RequestContent = describeStreamRequestEntity;

        //    //if (!string.IsNullOrWhiteSpace(startPartitionId))
        //    //{
        //    //    request.StartPartitionId = startPartitionId;
        //    //}

        //    //if (limitPartitions != null)
        //    //{
        //    //    request.LimitPartitions = limitPartitions.Value;
        //    //}

        //    response = dic.CreateStream(request);

        //    return response;
        //}


        public static void RunProducerDemo(string streamName, string shardId)
        {
            var dic = new DISIngestionClient();

            PutRecordsRequest putRecordsRequest = new PutRecordsRequest();
            putRecordsRequest.StreamName = streamName;
            var putRecordsRequestEntries = new List<PutRecordsRequestEntry>();
            //for (int i = 0; i < 3; i++)
            {
                int i = 0;
                string a = shardId + i;

                var putRecordsRequestEntry = new PutRecordsRequestEntry
                {
                    Data = Encoding.UTF8.GetBytes(a),
                    ExplicitHashKey = "123",
                    PartitionKey = i.ToString()
                };
                putRecordsRequestEntries.Add(putRecordsRequestEntry);
            }
            putRecordsRequest.Records = putRecordsRequestEntries;
            var response = dic.PutRecords(putRecordsRequest);

            foreach (var item in response.Records)
            {
                Console.WriteLine(item);
            }
        }

        
        public static void RunConsumerDemo(string streamName, string shardId)
        {
            var dic = new DISIngestionClient();
            const string startingSequenceNumber = "0";
            const string shardIteratorType = "TRIM_HORIZON";

            var request = new GetShardIteratorRequest
            {
                StreamName = streamName,
                ShardId = shardId,
                StartingSequenceNumber = startingSequenceNumber,
                ShardIteratorType = shardIteratorType
            };

            var recordsRequest = new GetRecordsRequest();
            var response = dic.GetShardIterator(request);
            Console.Out.WriteLine(response);

            var iterator = response.ShardIterator;

            while (true)
            {
                recordsRequest.ShardIterator = iterator;
                recordsRequest.Limit = 1;
                var recordResponse = dic.GetRecords(recordsRequest);
                if (recordResponse.Records.Count > 0)
                {
                    foreach (var record in recordResponse.Records)
                    {
                        Console.WriteLine("Record[{0}] = {1}", record.SequenceNumber, DecodeData(record.Data));
                    }
                }
                else
                {
                    break;
                }
                iterator = recordResponse.NextShardIterator;
                Thread.Sleep(1000);
            }
        }

        public static string DecodeData(byte[] byteBufferData)
        {
            return Encoding.UTF8.GetString(byteBufferData);
        }



    }
}
