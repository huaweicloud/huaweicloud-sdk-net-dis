using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using Com.Bigdata.Dis.Sdk.DISCommon.Model;
using DISClient_4._5.Client;
using Newtonsoft.Json;
namespace ConsoleApplication_4_5
{
    class Program
    {
        public static void Main(string[] args)
        {
            string streamName = "streamName";
            //string streamName = "dis_file_test";
            const string shardId = "shardId-0000000000";
            string appName = "testAppName";

            #region MyRegion
            //创建通道
            try
            {
                DISClient.CreateStream(streamName, 1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //上传数据
            try
            {
                DISClient.RunProducerDemo(streamName, shardId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //下载数据
            try
            {
                DISClient.RunConsumerDemo(streamName, shardId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //同步上传小文件
            try
            {
                int result = DISClient.UploadFileDemo("dis_file_test", @"C:\Users\Public\Pictures\Sample Pictures\Penguins.jpg");
                if (result == 0)
                {
                    Console.WriteLine("Success.");
                }
                else
                {
                    Console.WriteLine("Fail.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //异步上传小文件
            try
            {
                var dic = new DISAsync();
                PutRecordsRequest putRecordsRequest = new PutRecordsRequest();
                putRecordsRequest.StreamName = "dis_file_test";
                putRecordsRequest.FileName = @"thisisshawnfolder/2018/Penguins1110.jpg";
                putRecordsRequest.FilePath = @"C:\Users\Public\Pictures\Sample Pictures\Penguins.jpg";
                var taskResult = dic.PutFilesAsync(putRecordsRequest);

                if (taskResult.GetAwaiter().GetResult() == 0)
                {
                    Console.WriteLine("Success.");
                }
                else
                {
                    Console.WriteLine("Fail.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //查询通道列表
            try
            {
                DISClient.DescribeStreamList(null, 100);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //查询通道详情
            try
            {
                DISClient.DescribeStream(streamName, null, 100);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //获取数据游标
            try
            {
                DISClient.GetCursorDemo(streamName, shardId);
            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.Message);
            }

            //创建APP
            try
            {
                ResponseResult response = DISClient.CreateAPPDemo(appName);
                Console.Out.WriteLine(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //查询APP列表
            try
            {
                DescribeAppListResult response = DISClient.DescribeAppList("", null);
                var reqJson = JsonConvert.SerializeObject(response);
                Console.WriteLine(reqJson);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //查询APP详情
            try
            {
                DISClient.DescribeApp("testAppName");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //新增Checkpoint
            try
            {
                DISClient.AddCheckPointDemo(streamName, appName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //查询Checkpoint
            try
            {
                DISClient.GetCheckPointDemo(streamName, appName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //变更分区数量
            //Custom file stream is not suitable for operating.
            try
            {
                DISClient.UpdatePartitionCount(streamName, 1);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //添加OBS转储服务
            try
            {
                DISClient.CreateTransferTaskWithOBS(streamName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //添加DLI转储任务
            try
            {
                DISClient.CreateTransferTaskWithDLI(streamName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //查询通道监控信息
            try
            {
                DISClient.GetStreamMetricInfo(streamName, "total_put_bytes_per_stream", DISClient.GetTimeStamp() - 3 * 24 * 60 * 60, DISClient.GetTimeStamp());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //查询分区监控信息
            try
            {
                DISClient.GetPartitionMetricInfo(streamName, "0", "total_get_records_per_partition", DISClient.GetTimeStamp() - 3 * 24 * 60 * 60, DISClient.GetTimeStamp());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //查询转储任务列表
            try
            {
                DISClient.GetStreamTransferTaskList(streamName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //查询转储任务详情
            try
            {
                DISClient.GetStreamTransferTaskDetail(streamName, "task_1234");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //获取流消费信息
            try
            {
                DISClient.GetStreamConsumingInfo(streamName, appName, "", null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //删除Checkpoint
            try
            {
                DISClient.DeleteCheckpoint(streamName, appName, "0", "LAST_READ");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //删除APP
            try
            {
                ResponseResult response = DISClient.DeleteAPPDemo(appName);
                Console.Out.WriteLine(response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //删除转储任务
            try
            {
                DISClient.DeleteTransferTask(streamName, "task_1234");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //删除通道
            try
            {
                DISClient.DeleteStream(streamName);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            #endregion
        }
    }


    public class DISClient
    {
        /// <summary>        
        /// 获取时间戳       
        /// </summary>    
        /// <returns></returns>   
        public static long GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return (long)ts.TotalSeconds;
        }

        #region DescribeStream
        /// <summary>
        /// 查询通道列表
        /// </summary>
        /// <param name="startStreamName">起始通道名称</param>
        /// <param name="limit">每次查询时返回的通道数量</param>
        /// <returns></returns>
        public static DescribeStreamListResult DescribeStreamList(string startStreamName, int? limit)
        {
            DescribeStreamListResult response = null;
            var dic = new DISIngestionClient();
            var request = new DescribeStreamRequest();
            if (!string.IsNullOrWhiteSpace(startStreamName))
            {
                //从该通道开始返回通道列表，返回的通道列表不包括此通道名称。
                request.StartStreamName = startStreamName;
            }

            if (limit != null)
            {
                //单次请求返回通道列表的最大数量
                request.Limit = limit.Value;
            }

            response = dic.DescribeStreamList(request);
            var reqJson = JsonConvert.SerializeObject(response);
            Console.WriteLine(reqJson);
            return response;
        }

        /// <summary>
        /// 查询通道详情
        /// </summary>
        /// <param name="streamName">通道名称 </param>
        /// <param name="startPartitionId">起始分区ID </param>
        /// <param name="limitPartitions">单次请求返回的最大分区数 </param>
        /// <returns></returns>
        public static DescribeStreamResult DescribeStream(string streamName, string startPartitionId, int? limitPartitions)
        {
            DescribeStreamResult response = null;
            var dic = new DISIngestionClient();
            var request = new DescribeStreamRequest { StreamName = streamName };

            if (!string.IsNullOrWhiteSpace(startPartitionId))
            {
                //从该分区值开始返回分区列表，返回的分区列表不包括此分区
                request.StartPartitionId = startPartitionId;
            }

            if (limitPartitions != null)
            {
                //单次请求返回的最大分区数
                request.LimitPartitions = limitPartitions.Value;
            }

            response = dic.DescribeStream(request);
            var responseJson = JsonConvert.SerializeObject(response);
            Console.WriteLine(responseJson);
            return response;
        }
        #endregion

        #region DeleteStream
        /// <summary>
        /// 删除通道
        /// </summary>
        /// <param name="streamName">通道名称</param>
        /// <returns></returns>
        public static ResponseResult DeleteStream(string streamName)
        {
            var dic = new DISIngestionClient();
            var request = new DescribeStreamRequest { StreamName = streamName };
            ResponseResult response = dic.DeleteStream(request);
            Console.WriteLine(response);
            return response;
        }
        #endregion

        #region CreateStream
        /// <summary>
        /// 创建无转储任务的通道
        /// </summary>
        /// <param name="streamName"> 通道名称</param>
        /// <param name="partitionCount">分区数</param>
        /// <returns></returns>
        public static ResponseResult CreateStream(string streamName, int partitionCount)
        {
            DISIngestionClient dic = new DISIngestionClient();
            var request = new CreateStreamRequest
            {
                //通道名称
                StreamName = streamName,
                //通道类型
                StreamType = "ADVANCED",
                //分区数量
                PartitionCount = partitionCount,
                //源数据类型有BLOB、JSON、CSV、FILE
                DataType = "JSON",
                //生命周期
                DataDuration = 7 * 24,
            };

            //通道的标签,可选参数，如果不设置，则注释该代码片段
            request.Tags = new List<Tag>();
            request.Tags.Add(new Tag()
            {
                Key = "B key",
                Value = "BV value"
            });
            request.Tags.Add(new Tag()
            {
                Key = "B key",
                Value = "BV value2"
            });

            //源数据转储为parquet和carbon格式时必选
            if (request.DataType.Equals("JSON") || request.DataType.Equals("CSV"))
            {
                //用于描述用户JOSN、CSV格式的源数据结构
                //创建通道时源数据 Schema，Json格式的数据类型的{"key":"value"}转为源数据 Schema格式{"type":"record","name":"RecordName","fields":[{"name":"key","type":"string","doc":"Type inferred from '\"value\"'"}]}
                request.DataSchema = "{\"type\":\"record\",\"name\":\"RecordName\",\"fields\":[{\"name\":\"key\",\"type\":\"string\",\"doc\":\"Type inferred from '\\\"value\\\"'\"}]}";
            }

            //源数据转储为FILE时必选
            if (request.DataType.Equals("FILE"))
            {
                request.ObsDestinationDescriptor = new List<ObsDestinationDescriptor>();
                request.ObsDestinationDescriptor.Add(new ObsDestinationDescriptor()
                {
                    //在IAM中创建委托的名称
                    AgencyName = "all",
                    //存储该通道数据的OBS桶名称
                    ObsBucketPath = "obs-shawnobstest"
                });
            }

            ResponseResult response = dic.CreateStream(request);
            Console.WriteLine(response);
            return response;
        }
        #endregion

        #region RunProducerDemo
        /// <summary>
        /// 上传数据
        /// </summary>
        /// <param name="streamName">通道名称</param>
        /// <param name="shardId">分区ID</param>
        public static PutRecordsResult RunProducerDemo(string streamName, string shardId)
        {
            var dic = new DISIngestionClient();
            PutRecordsRequest putRecordsRequest = new PutRecordsRequest();
            putRecordsRequest.StreamName = streamName;
            var putRecordsRequestEntries = new List<PutRecordsRequestEntry>();
            for (int i = 0; i < 3; i++)
            {
                string a = shardId + i;

                var putRecordsRequestEntry = new PutRecordsRequestEntry
                {
                    //需要上传的数据
                    Data = Encoding.UTF8.GetBytes(a),
                    //用于明确数据需要写入分区的哈希值，此哈希值将覆盖“PartitionKey”的哈希值
                    //ExplicitHashKey = "0",
                    //如果传了PartitionId参数，则优先使用PartitionId参数。如果PartitionId没有传，则使用PartitionKey
                    //PartitionKey = new Random().Next().ToString(),
                    //分区的唯一标识符
                    PartitionId = shardId,
                };
                putRecordsRequestEntries.Add(putRecordsRequestEntry);
            }
            putRecordsRequest.Records = putRecordsRequestEntries;

            PutRecordsResult response = dic.PutRecords(putRecordsRequest);

            foreach (var item in response.Records)
            {
                Console.WriteLine(item);
            }
            return response;
        }

        /// <summary>
        /// 同步方式上传文件
        /// </summary>
        /// <param name="streamName">通道名称</param>
        /// <param name="file">文件名</param>
        public static int UploadFileDemo(string streamName, string file)
        {
            int failedRecordCount = 0;
            var dic = new DISIngestionClient();
            //最多上传128M的文件，这里先设置为最多上传500k
            int splitFileSize = 500 * 1024;
            string deliverDataId = Guid.NewGuid().ToString("N");
            FileInfo fileInfo = new FileInfo(file);
            //分几次传
            int steps = (int)Math.Ceiling((decimal)fileInfo.Length / splitFileSize);
            using (FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    for (int i = 0; i < steps; i++)
                    {
                        byte[] input = br.ReadBytes(splitFileSize);
                        {
                            PutRecordsRequest putRecordsRequest = new PutRecordsRequest();
                            putRecordsRequest.StreamName = streamName;
                            var putRecordsRequestEntries = new List<PutRecordsRequestEntry>();
                            var putRecordsRequestEntry = new PutRecordsRequestEntry
                            {
                                Data = input,
                                ExtenedInfo = new PutRecordsRequestEntryExtendedInfo()
                                {
                                    FileName = @"thisisshawnfolder/2018/07/04/14/56/Penguins2.jpg",
                                    DeliverDataId = deliverDataId,
                                    SequenceNumber = i,
                                    EndFlag = i == steps - 1 ? true : false,
                                }
                            };
                            putRecordsRequestEntries.Add(putRecordsRequestEntry);
                            putRecordsRequest.Records = putRecordsRequestEntries;
                            PutRecordsResult response = dic.PutFileRecords(putRecordsRequest);
                            failedRecordCount += response.FailedRecordCount;
                            //Thread.Sleep(500);
                            foreach (var item in response.Records)
                            {
                                Console.WriteLine("同步" + item);
                            }
                        }
                    }
                }
            }
            return failedRecordCount;
        }
        #endregion

        #region RunConsumerDemo
        /// <summary>
        /// 下载数据
        /// </summary>
        /// <param name="streamName">通道名称</param>
        /// <param name="shardId">分区ID</param>
        public static void RunConsumerDemo(string streamName, string shardId)
        {
            var dic = new DISIngestionClient();

            //该参数与游标类型AT_TIMESTAMP搭配使用
            long timestamp = 1543397197333;

            //该参数与游标类型AT_SEQUENCE_NUMBER、AFTER_SEQUENCE_NUMBER搭配使用
            const string startingSequenceNumber = "0";

            //AT_SEQUENCE_NUMBER：从特定序列号所在的记录开始读取。此类型为默认游标类型。
            //AFTER_SEQUENCE_NUMBER：从特定序列号后的记录开始读取。
            //TRIM_HORIZON：从分区中时间最长的记录开始读取。
            //LATEST：在分区中最新的记录之后开始读取，以确保始终读取分区中的最新数据。
            //AT_TIMESTAMP：从特定时间戳开始读取。
            const string shardIteratorType = "AT_SEQUENCE_NUMBER";

            var request = new GetShardIteratorRequest
            {
                //通道名称 
                StreamName = streamName,
                //分区值 
                ShardId = shardId,
                //序列号，可选参数 
                StartingSequenceNumber = startingSequenceNumber,
                //游标类型，可选参数 
                ShardIteratorType = shardIteratorType,
                //时间戳，可选参数
                Timestamp = timestamp
            };

            var recordsRequest = new GetRecordsRequest();
            var response = dic.GetShardIterator(request);
            Console.Out.WriteLine(response);

            var iterator = response.ShardIterator;

            //下载数据，这里的进程不会退出，只要检测到有数据就下载数据
            while (true)
            {
                //数据游标。 
                recordsRequest.ShardIterator = iterator;
                GetRecordsResult recordResponse = dic.GetRecords(recordsRequest);
                // 下一批数据游标
                iterator = recordResponse.NextShardIterator;

                if (recordResponse.Records.Count > 0)
                {
                    foreach (var record in recordResponse.Records)
                    {
                        Console.WriteLine("Record[{0}] = {1}", record.SequenceNumber, DecodeData(record.Data));
                    }
                    Thread.Sleep(1000);
                }
                else
                {
                    Thread.Sleep(1000);
                }
            }
        }

        public static string DecodeData(byte[] byteBufferData)
        {
            return Encoding.UTF8.GetString(byteBufferData);
        }
        #endregion

        #region GetCursorDemo
        /// <summary>
        /// 获取数据游标
        /// </summary>
        /// <param name="streamName">通道名称</param>
        /// <param name="shardId">分区ID</param>
        public static GetShardIteratorResult GetCursorDemo(string streamName, string shardId)
        {
            var dic = new DISIngestionClient();

            //该参数与游标类型AT_TIMESTAMP搭配使用
            long timestamp = 1543397197333;

            //该参数与游标类型AT_SEQUENCE_NUMBER、AFTER_SEQUENCE_NUMBER搭配使用
            string startingSequenceNumber = "0";

            //AT_SEQUENCE_NUMBER：从特定序列号所在的记录开始读取。此类型为默认游标类型。
            //AFTER_SEQUENCE_NUMBER：从特定序列号后的记录开始读取。
            //TRIM_HORIZON：从分区中时间最长的记录开始读取。
            //LATEST：在分区中最新的记录之后开始读取，以确保始终读取分区中的最新数据。
            //AT_TIMESTAMP：从特定时间戳开始读取。
            string shardIteratorType = "AT_SEQUENCE_NUMBER";

            var request = new GetShardIteratorRequest
            {
                //通道名称 
                StreamName = streamName,
                //分区值 
                ShardId = shardId,
                //游标类型，可选参数 
                ShardIteratorType = shardIteratorType,
                //序列号，可选参数 
                StartingSequenceNumber = startingSequenceNumber,
                //时间戳，可选参数
                Timestamp = timestamp
            };

            var response = dic.GetShardIterator(request);
            Console.Out.WriteLine(response);
            return response;
        }
        #endregion

        #region CreateAPPDemo
        /// <summary>
        /// 创建APP
        /// </summary>
        /// <param name="appName">app名称</param>
        public static ResponseResult CreateAPPDemo(string appName)
        {
            var dic = new DISIngestionClient();
            var request = new AppRequest
            {
                AppName = appName,
            };

            ResponseResult response = dic.CreateApp(request);
            return response;
        }
        #endregion

        #region DeleteAPPDemo
        /// <summary>
        /// 删除APP
        /// </summary>
        /// <param name="appName">app名称</param>
        public static ResponseResult DeleteAPPDemo(string appName)
        {
            var dic = new DISIngestionClient();
            var request = new AppRequest
            {
                AppName = appName,
            };

            var response = dic.DeleteApp(request);
            return response;
        }
        #endregion

        #region AddCheckPoint
        /// <summary>
        /// 新增Checkpoint
        /// </summary>
        /// <param name="streamName">通道名称</param>
        /// <param name="appName">app名称</param>
        public static ResponseResult AddCheckPointDemo(string streamName, string appName)
        {
            var dic = new DISIngestionClient();
            var request = new CommitCheckpointRequest
            {
                //通道名称 
                StreamName = streamName,
                //APP的名称 
                AppName = appName,
                //分区的唯一标识符 
                PartitionId = "shardId-0000000000",
                //序列号 
                SequenceNumber = "10",
                //用户消费程序端的元数据信息 
                Metadata = "metadata",
                //Checkpoint类型 
                CheckpointType = "LAST_READ",
            };

            var response = dic.CommitCheckpoint(request);
            Console.Out.WriteLine(response);
            return response;
        }
        #endregion

        #region GetCheckPoint
        /// <summary>
        /// 查询Checkpoint
        /// </summary>
        /// <param name="streamName">通道名称</param>
        /// <param name="appName">app名称</param>
        public static GetCheckpointResult GetCheckPointDemo(string streamName, string appName)
        {
            var dic = new DISIngestionClient();
            var request = new GetCheckpointRequest
            {
                StreamName = streamName,
                AppId = appName,
                ShardId = "shardId-0000000000",
                CheckpointType = "LAST_READ",
            };

            GetCheckpointResult response = dic.GetCheckpoint(request);
            Console.Out.WriteLine(response);
            return response;
        }
        #endregion

        #region UpdatePartitionCount
        /// <summary>
        /// 变更分区数量
        /// </summary>
        /// <param name="streamName">通道名称</param>
        /// <param name="count">变更的目标分区数量</param>
        public static UpdateShardsResult UpdatePartitionCount(string streamName, int count)
        {
            var dic = new DISIngestionClient();
            var request = new UpdateShardsRequest
            {
                StreamName = streamName,
                TargetPartitionCount = count,
            };

            var response = dic.UpdatePartition(request);
            Console.Out.WriteLine(response);
            return response;
        }
        #endregion

        #region DescribeApp
        /// <summary>
        /// 查询App详情
        /// </summary>
        /// <param name="appName">App名称</param>
        /// <returns></returns>
        public static DescribeAppResult DescribeApp(string appName)
        {
            DescribeAppResult response = null;
            var dic = new DISIngestionClient();
            var request = new AppRequest
            {
                AppName = appName,
            };
            response = dic.DescribeApp(request);
            var responseJson = JsonConvert.SerializeObject(response);
            Console.WriteLine(responseJson);
            return response;
        }
        #endregion

        #region DescribeAppList
        /// <summary>
        /// 查询App列表
        /// </summary>
        /// <param name="startAppName">起始App名称</param>
        /// <param name="limit">每次查询时返回的App数量</param>
        /// <returns></returns>
        public static DescribeAppListResult DescribeAppList(string startAppName, int? limit)
        {
            DescribeAppListResult response = null;
            var dic = new DISIngestionClient();
            var request = new DescribeAppListRequest();
            if (!string.IsNullOrWhiteSpace(startAppName))
            {
                //从该App开始返回App列表，返回的App列表不包括此App名称 
                request.StartAppName = startAppName;
            }

            //单次请求返回App列表的最大数量 
            if (limit != null)
            {
                request.Limit = limit.Value;
            }
            else
            {
                request.Limit = 10;
            }

            response = dic.DescribeAppList(request);
            return response;

        }
        #endregion

        #region DeleteCheckpoint
        /// <summary>
        /// 删除Checkpoint
        /// </summary>
        /// <param name="streamName">通道名称</param>
        /// <param name="appName">App名称</param>
        /// <param name="partitionId">分区ID</param>
        /// <param name="checkpointType">Checkpoint类型</param>
        /// <returns></returns>
        public static ResponseResult DeleteCheckpoint(string streamName = "", string appName = "", string partitionId = "", string checkpointType = "LAST_READ")
        {
            var dic = new DISIngestionClient();
            var request = new CheckPointRequest();
            if (!String.IsNullOrEmpty(streamName))
            {
                request.StreamName = streamName;
            }

            if (!String.IsNullOrEmpty(appName))
            {
                request.AppName = appName;
            }

            if (!String.IsNullOrEmpty(partitionId))
            {
                request.PartitionId = partitionId;
            }

            if (!String.IsNullOrEmpty(checkpointType))
            {
                request.CheckpointType = checkpointType;
            }

            ResponseResult response = dic.DeleteCheckpoint(request);
            Console.WriteLine(response);
            return response;
        }
        #endregion

        #region GetStreamMetricInfo
        /// <summary>
        /// 查询通道监控信息
        /// </summary>
        /// <param name="streamName">通道名称</param>
        /// <param name="label">通道监控指标</param>
        /// <param name="startTime">监控开始时间点，10位时间戳</param>
        /// <param name="endTime">监控结束时间点，10位时间戳</param>
        /// <returns></returns>
        public static GetMetricResult GetStreamMetricInfo(string streamName, string label, long startTime, long endTime)
        {
            GetMetricResult response = null;
            var dic = new DISIngestionClient();
            var request = new GetStreamMetricRequest
            {
                Label = label,
                StartTime = startTime,
                EndTime = endTime,
            };
            response = dic.GetStreamMetricInfo(streamName, request);
            var responseJson = JsonConvert.SerializeObject(response);
            Console.WriteLine(responseJson);
            return response;
        }
        #endregion

        #region GetPartitionMetricInfo
        /// <summary>
        /// 查询分区监控信息
        /// </summary>
        /// <param name="streamName">通道名称</param>
        /// <param name="partitionId">分区ID</param>
        /// <param name="label">分区监控指标</param>
        /// <param name="startTime">监控开始时间点，10位时间戳</param>
        /// <param name="endTime">监控结束时间点，10位时间戳</param>
        /// <returns></returns>
        public static GetMetricResult GetPartitionMetricInfo(string streamName, string partitionId, string label, long startTime, long endTime)
        {
            GetMetricResult response = null;
            var dic = new DISIngestionClient();
            var request = new GetPartitionMetricRequest
            {
                Label = label,
                StartTime = startTime,
                EndTime = endTime,
            };
            response = dic.GetPartitionMetricInfo(streamName, partitionId, request);
            var responseJson = JsonConvert.SerializeObject(response);
            Console.WriteLine(responseJson);
            return response;
        }
        #endregion

        #region CreateTransferTask
        /// <summary>
        /// 添加OBS转储服务
        /// </summary>
        /// <param name="streamName">通道名称</param>
        /// <returns></returns>
        public static ResponseResult CreateTransferTaskWithOBS(string streamName)
        {
            var dic = new DISIngestionClient();
            var request = new AddTransferTasksRequest
            {
                DestinationType = "OBS",
                ObsDestinationDescriptor = new ObsDestinationDescriptorEntity
                {
                    //转储任务的名称
                    TaskName = "task_1234",
                    //通过DestinationFileType可以设置转储文件格式为text、parquet和carbon
                    DestinationFileType = "text",
                    //偏移量
                    ConsumerStrategy = "LATEST",
                    //IAM委托名称
                    AgencyName = "all",
                    //桶名称
                    ObsBucketPath = "obs-shawn",
                    //自定义文件
                    FilePrefix = "1012",
                    //数据导入OBS时间,取值范围：30～900
                    DeliverTimeInterval = 30,
                    //目录层次结构
                    PartitionFormat = "yyyy",
                    //分隔符
                    RecordDelimiter = "\n"
                }
            };

            //创建parquet和carbon的转储任务，必须先创建一个具有源数据Schema的通道，否则无法创建转储任务

            //转储的目标文件格式为parquet，如果需要自定义OBS的时间目录，则必选下面代码片段
            //自定义时间目录，如果不设置，则注释该代码片段
            if (request.ObsDestinationDescriptor.DestinationFileType.Equals("parquet"))
            {
                //根据源数据的时间戳和已配置的"partition_format"生成对应的转储时间目录
                request.ObsDestinationDescriptor.ProcessingSchema = new ProcessingSchema();
                //创建通道时源数据 Schema，Json格式的数据类型的{"key":"value"}转为源数据 Schema格式{"type":"record","name":"RecordName","fields":[{"name":"key","type":"string","doc":"Type inferred from '\"value\"'"}]}
                //TimestampName的值就是Json格式的键值
                request.ObsDestinationDescriptor.ProcessingSchema.TimestampName = "key";
                //源数据时间戳的类型有String、Timestamp
                request.ObsDestinationDescriptor.ProcessingSchema.TimestampType = "String";
                //源数据时间戳的类型为String时必选，用于根据时间戳格式生成OBS的时间目录。取值范围有下面的几种
                // yyyy/MM/dd HH:mm:ss
                // MM/dd/yyyy HH:mm:ss
                // dd/MM/yyyy HH:mm:ss
                // yyyy-MM-dd HH:mm:ss
                // MM-dd-yyyy HH:mm:ss
                // dd-MM-yyyy HH:mm:ss
                request.ObsDestinationDescriptor.ProcessingSchema.TimestampFormat = "yyyy/MM/dd HH:mm:ss";
            }

            ResponseResult response = dic.CreateTransferTask(streamName, request);
            Console.WriteLine(response);
            return response;
        }

        /// <summary>
        /// 添加DLI转储任务
        /// </summary>
        /// <param name="streamName"></param>
        /// <returns></returns>
        public static ResponseResult CreateTransferTaskWithDLI(string streamName)
        {
            var dic = new DISIngestionClient();
            var request = new AddTransferTasksRequest
            {
                DestinationType = "DLI",
                DLIDestinationDescriptor = new DLIDestinationDescriptorEntity
                {
                    //转储任务的名称
                    TaskName = "task_1111",
                    //IAM委托名称
                    AgencyName = "all",
                    //存储该通道数据的DLI数据库名称
                    DLIDatabaseName = "dli",
                    //存储该通道数据的DLI表名称
                    DLITableName = "test1",
                    //偏移量
                    ConsumerStrategy = "LATEST",
                    //桶名称
                    OBSBucketPath = "obs-shawn",
                    //自定义文件
                    FilePrefix = "1012",
                    //数据导入OBS时间,取值范围：30～900
                    DeliverTimeInterval = 30,
                    //重试时间
                    RetryDuration = 10
                }
            };

            ResponseResult response = dic.CreateTransferTask(streamName, request);
            Console.WriteLine(response);
            return response;
        }
        #endregion

        #region DeleteTransferTask
        /// <summary>
        /// 删除转储任务
        /// </summary>
        /// <param name="streamName">通道名称</param>
        /// <param name="transferTaskName">转储任务名称</param>
        /// <returns></returns>
        public static ResponseResult DeleteTransferTask(string streamName, string transferTaskName)
        {
            var dic = new DISIngestionClient();
            ResponseResult response = dic.DeleteTransferTask(streamName, transferTaskName);
            Console.WriteLine(response);
            return response;
        }
        #endregion

        #region GetStreamTransferTaskDetail
        /// <summary>
        /// 查询转储任务详情
        /// </summary>
        /// <param name="streamName">通道名称</param>
        /// <param name="transferTaskName">转储任务名称</param>
        /// <returns></returns>
        public static StreamTransferTaskDetailResult GetStreamTransferTaskDetail(string streamName, string transferTaskName)
        {
            StreamTransferTaskDetailResult response = null;
            var dic = new DISIngestionClient();
            response = dic.GetStreamTransferTaskDetail(streamName, transferTaskName);
            var responseJson = JsonConvert.SerializeObject(response);
            Console.WriteLine(responseJson);
            return response;
        }
        #endregion

        #region GetStreamTransferTaskList
        /// <summary>
        /// 查询转储任务列表
        /// </summary>
        /// <param name="streamName">通道名称</param>
        /// <returns></returns>
        public static StreamTransferTaskListResult GetStreamTransferTaskList(string streamName)
        {
            StreamTransferTaskListResult response = null;
            var dic = new DISIngestionClient();
            response = dic.GetStreamTransferTaskList(streamName);
            var responseJson = JsonConvert.SerializeObject(response);
            Console.WriteLine(responseJson);
            return response;
        }
        #endregion

        #region GetStreamConsumingInfo
        /// <summary>
        /// 获取通道消费信息
        /// </summary>
        /// <param name="streamName">通道名称</param>
        /// <param name="appName">App名称</param>
        /// <param name="startPartitionId">起始分区ID</param>
        /// <param name="limit">表示需要返回多少个parition的消费状态</param>
        /// <returns></returns>
        public static GetStreamConsumingResult GetStreamConsumingInfo(string streamName, string appName, string startPartitionId, long? limit)
        {
            GetStreamConsumingResult response = null;
            var dic = new DISIngestionClient();
            var request = new GetStreamConsumingRequest { CheckpointType = "LAST_READ" };

            //表示获取流消费信息时的起始partition
            if (!string.IsNullOrEmpty(startPartitionId))
            {
                request.StartPartitionId = startPartitionId;
            }
            else
            {
                request.StartPartitionId = "0";
            }

            //表示需要返回多少个parition的消费状态
            if (limit != null)
            {
                request.Limit = limit.Value;
            }
            else
            {
                request.Limit = 10;
            }

            response = dic.GetStreamConsumingInfo(streamName, appName, request);
            var responseJson = JsonConvert.SerializeObject(response);
            Console.WriteLine(responseJson);
            return response;
        }
        #endregion
    }
}
