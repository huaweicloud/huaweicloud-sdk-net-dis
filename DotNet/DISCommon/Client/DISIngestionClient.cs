using System;
using System.Collections.Generic;
using System.IO;
using Com.Bigdata.Dis.Sdk.DISCommon.Auth;
using Com.Bigdata.Dis.Sdk.DISCommon.Config;
using Com.Bigdata.Dis.Sdk.DISCommon.DISWeb;
using Com.Bigdata.Dis.Sdk.DISCommon.Ext;
using Com.Bigdata.Dis.Sdk.DISCommon.Interface;
using Com.Bigdata.Dis.Sdk.DISCommon.Model;
using Com.Bigdata.Dis.Sdk.DISCommon.Log;
using Newtonsoft.Json;
using OBS.Runtime;
using Com.Bigdata.Dis.Sdk.DISCommon.Utils;
using log4net;
using IRequest = OBS.Runtime.Internal.IRequest;
using Com.Bigdata.Dis.Sdk.DISCommon.Utils.Cache;
using System.Net;
using ICredentials = Com.Bigdata.Dis.Sdk.DISCommon.Auth.ICredentials;
using System.Threading;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Client
{
    public delegate PutRecordsResult PutRecordMethod(PutRecordsRequest putRecordsParam);

    public class DISIngestionClient : IDIS
    {
        protected static ILog logger = LogHelper.GetInstance();

        protected string _region;

        protected DISConfig _disConfig;

        protected ICredentials _credentials;

        private static object objlock = new object();

        public DISIngestionClient()
        {
            _disConfig = DISConfig.buildDefaultConfig();
            _credentials = new BasicCredentials(_disConfig.GetAK(), _disConfig.GetSK());
            _region = _disConfig.GetRegion();
        }

        public DISIngestionClient(ICredentials credentials, string region)
        {
            _credentials = credentials;
            _region = region;
            _disConfig = DISConfig.buildDefaultConfig();
        }

        public DISIngestionClient(DISConfig disConfig)
        {
            _disConfig = DISConfig.BuildConfig(disConfig);
            _credentials = new BasicCredentials(_disConfig.GetAK(), _disConfig.GetSK());
            _region = _disConfig.GetRegion();
        }

        public DISIngestionClient(string disConfigFile)
        {
            _disConfig = DISConfig.BuildConfig(disConfigFile);
            _credentials = new BasicCredentials(_disConfig.GetAK(), _disConfig.GetSK());
            _region = _disConfig.GetRegion();
        }

        public DISIngestionClient(ICredentials credentials, string region, string disConfigFile)
        {
            _credentials = credentials;
            _region = region;
            _disConfig = DISConfig.BuildConfig(disConfigFile);
        }

        public DISIngestionClient(ICredentials credentials, string region, DISConfig disConfig)
        {
            _credentials = credentials;
            _region = region;
            _disConfig = DISConfig.BuildConfig(disConfig);
        }

        #region Implemented Methods
        //public PutRecordResult PutRecord(PutRecordRequest putRecordParam)
        //{
        //    ObsWebServiceRequest obsWebServiceRequest = new DISWebServiceRequest();
        //    OBS.Runtime.Internal.IRequest requestobs = new DISDefaultRequest(obsWebServiceRequest, Constants.SERVICENAME)
        //    {
        //        HttpMethod = HttpMethodName.POST.ToString()
        //    };

        //    string resourcePath = ResourcePathBuilder.Standard()
        //        .WithProjectId(_disConfig.GetProjectId())
        //        .WithResource(new RecordResource(null))
        //        .Build();
        //    requestobs.ResourcePath = resourcePath;
        //    var results = Request<PutRecordResult>(putRecordParam, requestobs);

        //    return results;
        //}

        protected PutRecordsRequest DecorateRecords(PutRecordsRequest putRecordsParam)
        {
            //加密
            if (IsEncrypt())
            {
                if (putRecordsParam.Records != null)
                {
                    for (int i = 0; i < putRecordsParam.Records.Count; i++)
                    {
                        putRecordsParam.Records[i].Data = Encrypt(new MemoryStream(putRecordsParam.Records[i].Data));
                    }
                }
            }

            //压缩
            if (_disConfig.IsDataCompressEnabled())
            {
                if (putRecordsParam.Records != null)
                {
                    for (int i = 0; i < putRecordsParam.Records.Count; i++)
                    {
                        byte[] input = putRecordsParam.Records[i].Data;
                        try
                        {
                            byte[] compressedInput = CompressUtils.Compress(input);
                            putRecordsParam.Records[i].Data = compressedInput;
                        }
                        catch (IOException e)
                        {
                            logger.Error(e.Message, e);
                            throw new Exception(e.Message);
                        }
                    }
                }
            }

            return putRecordsParam;
        }

        /// <summary>
        /// 流式数据上传
        /// </summary>
        /// <param name="putRecordsParam"></param>
        /// <returns></returns>
        public PutRecordsResult PutRecords(PutRecordsRequest putRecordsParam)
        {
            return InnerPutRecordsSupportingCache(putRecordsParam, new PutRecordMethod(InnerPutRecords));
        }

        protected PutRecordsResult InnerPutRecordsSupportingCache(PutRecordsRequest putRecordsParam, PutRecordMethod putRecordMethod)
        {
            if (_disConfig.IsDataCacheEnabled())
            {
                // 开启本地缓存
                PutRecordsResult putRecordsResult = null;
                try
                {
                    putRecordsResult = InnerPutRecordsWithRetry(putRecordsParam, putRecordMethod);
                }
                catch (Exception e)
                {
                    string errorMsg = e.InnerException.Message;
                    int statusCode = int.Parse(errorMsg.Split('\n')[0]);
                    // 如果不是可以重试的异常 或者 已达到重试次数，则直接抛出异常
                    if (Utils.Utils.IsCacheData(statusCode))
                    {
                        // 网络异常 全部记录上传失败
                        logger.Info("Local data cache is enabled, try to put failed records to local.");
                        CacheUtils.PutToCache(putRecordsParam, _disConfig); // 写入本地缓存
                    }
                    throw e;
                }

                try
                {
                    // 部分记录上传失败
                    if (putRecordsResult.FailedRecordCount > 0)
                    {
                        // 过滤出上传失败的记录
                        List<PutRecordsResultEntry> putRecordsResultEntries = putRecordsResult.Records;
                        List<PutRecordsRequestEntry> failedPutRecordsRequestEntries = new List<PutRecordsRequestEntry>();
                        int index = 0;
                        foreach (PutRecordsResultEntry putRecordsResultEntry in putRecordsResultEntries)
                        {
                            if (!String.IsNullOrEmpty(putRecordsResultEntry.ErrorCode))
                            {
                                failedPutRecordsRequestEntries.Add(putRecordsParam.Records[index]);
                            }
                            index++;
                        }
                        putRecordsParam.Records = failedPutRecordsRequestEntries;

                        logger.Info("Local data cache is enabled, try to put failed records to local.");

                        CacheUtils.PutToCache(putRecordsParam, _disConfig); // 写入本地缓存
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }

                return putRecordsResult;
            }
            else
            {
                return InnerPutRecordsWithRetry(putRecordsParam, putRecordMethod);
            }
        }


        protected PutRecordsResult InnerPutRecordsWithRetry(PutRecordsRequest putRecordsParam, PutRecordMethod putRecordMethod)
        {
            //数据上传的结果集
            PutRecordsResult putRecordsResult = null;

            //用该数组来汇总每次请求后的结果
            PutRecordsResultEntry[] putRecordsResultEntryList = null;

            //记录每次请求失败的下标位置
            int[] retryIndex = null;

            //每次需要上传的请求数据
            PutRecordsRequest retryPutRecordsRequest = putRecordsParam;

            int retryCount = -1;
            int currentFailed = 0;
            ExponentialBackOff backOff = null;
            try
            {
                do
                {
                    retryCount++;
                    if (retryCount > 0)
                    {
                        // 等待一段时间再发起重试
                        if (backOff == null)
                        {
                            Monitor.Enter(objlock);
                            logger.Info("Put records retry lock.");
                            backOff = new ExponentialBackOff(ExponentialBackOff.DEFAULT_INITIAL_INTERVAL,
                                ExponentialBackOff.DEFAULT_MULTIPLIER, _disConfig.GetBackOffMaxIntervalMs(),
                                ExponentialBackOff.DEFAULT_MAX_ELAPSED_TIME);
                        }

                        if (putRecordsResult != null && currentFailed != putRecordsResult.Records.Count)
                        {
                            // 部分失败则重置退避时间
                            backOff.resetCurrentInterval();
                        }

                        long sleepMs = backOff.getNextBackOff();

                        if (retryPutRecordsRequest.Records.Count > 0)
                        {
                            logger.DebugFormat(
                                "Put {0} records but {1} failed, will re-try after backoff {2} ms, current retry count is {3}.",
                                putRecordsResult != null ? putRecordsResult.Records.Count
                                    : putRecordsParam.Records.Count,
                                currentFailed,
                                sleepMs,
                                retryCount);
                        }

                        backOff.backOff(sleepMs);
                    }

                    try
                    {
                        putRecordsResult = putRecordMethod(retryPutRecordsRequest);
                    }
                    catch (Exception t)
                    {
                        if (putRecordsResultEntryList != null)
                        {
                            logger.Error(t.Message, t);
                            break;
                        }
                        throw t;
                    }

                    if (putRecordsResult != null)
                    {
                        currentFailed = putRecordsResult.FailedRecordCount;

                        if (putRecordsResultEntryList == null && currentFailed == 0 || _disConfig.GetRecordsRetries() == 0)
                        {
                            // 第一次发送全部成功或者不需要重试，则直接返回结果
                            return putRecordsResult;
                        }

                        if (putRecordsResultEntryList == null)
                        {
                            // 存在发送失败的情况，需要重试，则使用数组来汇总每次请求后的结果。
                            putRecordsResultEntryList = new PutRecordsResultEntry[putRecordsParam.Records.Count];
                        }

                        // 需要重试发送数据的原始下标
                        List<int> retryIndexTemp = new List<int>(currentFailed);

                        if (currentFailed > 0)
                        {
                            // 初始化重试发送的数据请求
                            retryPutRecordsRequest = new PutRecordsRequest();
                            retryPutRecordsRequest.StreamName = putRecordsParam.StreamName;
                            retryPutRecordsRequest.Records = new List<PutRecordsRequestEntry>(currentFailed);
                        }

                        // 对每条结果分析，更新结果数据
                        for (int i = 0; i < putRecordsResult.Records.Count; i++)
                        {
                            // 获取重试数据在原始数据中的下标位置
                            int originalIndex = retryIndex == null ? i : retryIndex[i];
                            PutRecordsResultEntry putRecordsResultEntry = putRecordsResult.Records[i];
                            // 对所有异常进行重试 && "DIS.4303".equals(putRecordsResultEntry.getErrorCode())
                            if (!string.IsNullOrEmpty(putRecordsResultEntry.ErrorCode))
                            {
                                retryIndexTemp.Add(originalIndex);
                                retryPutRecordsRequest.Records.Add(putRecordsParam.Records[originalIndex]);
                            }
                            putRecordsResultEntryList[originalIndex] = putRecordsResultEntry;
                        }
                        retryIndex = retryIndexTemp.Count > 0 ? retryIndexTemp.ToArray()
                            : new int[0];
                    }
                } while ((retryIndex == null || retryIndex.Length > 0) && retryCount < _disConfig.GetRecordsRetries());
            }
            finally
            {
                if (retryCount > 0)
                {
                    Monitor.Exit(objlock);
                    logger.Info("Put records retry unlock.");
                }
            }
            putRecordsResult = new PutRecordsResult();
            if (retryIndex == null)
            {
                // 不可能存在此情况，完全没有发送出去会直接抛出异常
                putRecordsResult.FailedRecordCount = putRecordsParam.Records.Count;
            }
            else
            {
                putRecordsResult.FailedRecordCount = retryIndex.Length;
                putRecordsResult.Records = new List<PutRecordsResultEntry>(putRecordsResultEntryList);
            }

            return putRecordsResult;
        }

        protected PutRecordsResult InnerPutRecords(PutRecordsRequest putRecordsParam)
        {
            // Decorate PutRecordsRequest if needed
            putRecordsParam = DecorateRecords(putRecordsParam);

            ObsWebServiceRequest obsWebServiceRequest = new DISWebServiceRequest();
            OBS.Runtime.Internal.IRequest requestobs = new DISDefaultRequest(obsWebServiceRequest, Constants.SERVICENAME)
            {
                HttpMethod = HttpMethodName.POST.ToString()
            };

            string resourcePath = ResourcePathBuilder.Standard()
                .WithProjectId(_disConfig.GetProjectId())
                .WithResource(new RecordResource(null))
                .Build();
            requestobs.ResourcePath = resourcePath;
            var results = Request<PutRecordsResult>(putRecordsParam, requestobs);
            return results;
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        /// <param name="putRecordsParam"></param>
        /// <returns></returns>
        public PutRecordsResult PutFileRecords(PutRecordsRequest putRecordsParam)
        {
            return InnerPutRecordsSupportingCache(putRecordsParam, new PutRecordMethod(InnerPutFileRecords));
        }

        protected PutRecordsResult InnerPutFileRecords(PutRecordsRequest putRecordsParam)
        {
            // Decorate PutRecordsRequest if needed
            putRecordsParam = DecorateRecords(putRecordsParam);

            ObsWebServiceRequest obsWebServiceRequest = new DISWebServiceRequest();
            OBS.Runtime.Internal.IRequest requestobs = new DISDefaultRequest(obsWebServiceRequest, Constants.SERVICENAME)
            {
                HttpMethod = HttpMethodName.POST.ToString()
            };

            string resourcePath = ResourcePathBuilder.Standard()
                .WithProjectId(_disConfig.GetProjectId())
                .WithResource(new RecordResource(null))
                .Build();
            requestobs.ResourcePath = resourcePath;

            var results = Request<PutRecordsResult>(putRecordsParam, requestobs);
            return results;
        }

        public GetShardIteratorResult GetShardIterator(GetShardIteratorRequest getShardIteratorParam)
        {
            return InnerGetShardIterator(getShardIteratorParam);
        }

        private GetShardIteratorResult InnerGetShardIterator(GetShardIteratorRequest getShardIteratorParam)
        {
            ObsWebServiceRequest obsWebServiceRequest = new DISWebServiceRequest();
            OBS.Runtime.Internal.IRequest requestobs = new DISDefaultRequest(obsWebServiceRequest, Constants.SERVICENAME)
            {
                HttpMethod = HttpMethodName.GET.ToString()
            };

            string resourcePath = ResourcePathBuilder.Standard()
                                                     .WithProjectId(_disConfig.GetProjectId())
                                                     .WithResource(new CursorResource(null))
                                                     .Build();
            requestobs.ResourcePath = resourcePath;
            var results = Request<GetShardIteratorResult>(getShardIteratorParam, requestobs);

            return results;
        }
        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="getRecordsParam"></param>
        /// <returns></returns>
        public GetRecordsResult GetRecords(GetRecordsRequest getRecordsParam)
        {
            ObsWebServiceRequest obsWebServiceRequest = new DISWebServiceRequest();
            IRequest requestobs = new DISDefaultRequest(obsWebServiceRequest, Constants.SERVICENAME)
            {
                HttpMethod = HttpMethodName.GET.ToString()
            };

            string resourcePath = ResourcePathBuilder.Standard()
                .WithProjectId(_disConfig.GetProjectId())
                .WithResource(new RecordResource(null))
                .Build();
            requestobs.ResourcePath = resourcePath;
            GetRecordsResult result = null;
            result = Request<GetRecordsResult>(getRecordsParam, requestobs);

            result = DecorateRecords(result);
            return result;
        }

        protected GetRecordsResult DecorateRecords(GetRecordsResult result)
        {
            //解压
            if (_disConfig.IsDataCompressEnabled())
            {
                if (result.Records != null)
                {
                    for (int i = 0; i < result.Records.Count; i++)
                    {
                        byte[] input = result.Records[i].Data;
                        try
                        {
                            byte[] uncompressedInput = CompressUtils.Decompress(input);
                            result.Records[i].Data = uncompressedInput;
                        }
                        catch (IOException e)
                        {
                            logger.Error(e.Message, e);
                            throw new Exception(e.Message);
                        }
                    }
                }
            }

            //解密
            if (IsEncrypt())
            {
                List<Record> records = result.Records;
                if (records != null)
                {
                    foreach (var record in records)
                    {
                        record.Data = Decrypt(record.Data);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 新增Checkpoint
        /// </summary>
        /// <param name="commitCheckpointParam"></param>
        /// <returns></returns>
        public ResponseResult CommitCheckpoint(CommitCheckpointRequest commitCheckpointParam)
        {
            ObsWebServiceRequest obsWebServiceRequest = new DISWebServiceRequest();
            OBS.Runtime.Internal.IRequest requestobs = new DISDefaultRequest(obsWebServiceRequest, Constants.SERVICENAME)
            {
                HttpMethod = HttpMethodName.POST.ToString()
            };

            string resourcePath = ResourcePathBuilder.Standard()
                                                     .WithProjectId(_disConfig.GetProjectId())
                                                     .WithResource(new CheckPointResource(null))
                                                     .Build();
            requestobs.ResourcePath = resourcePath;
            var results = Request<ResponseResult>(commitCheckpointParam, requestobs);

            return results;
        }
        /// <summary>
        /// 查询Checkpoint
        /// </summary>
        /// <param name="getCheckpointRequest"></param>
        /// <returns></returns>
        public GetCheckpointResult GetCheckpoint(GetCheckpointRequest getCheckpointRequest)
        {
            ObsWebServiceRequest obsWebServiceRequest = new DISWebServiceRequest();
            IRequest requestobs = new DISDefaultRequest(obsWebServiceRequest, Constants.SERVICENAME)
            {
                HttpMethod = HttpMethodName.GET.ToString()
            };

            string resourcePath = ResourcePathBuilder.Standard()
                .WithProjectId(_disConfig.GetProjectId())
                .WithResource(new CheckPointResource(null))
                .Build();
            requestobs.ResourcePath = resourcePath;
            var result = Request<GetCheckpointResult>(getCheckpointRequest, requestobs);
            return result;
        }

        public UpdateShardsResult UpdatePartition(UpdateShardsRequest updateShardsRequest)
        {
            ObsWebServiceRequest obsWebServiceRequest = new DISWebServiceRequest();
            IRequest requestobs = new DISDefaultRequest(obsWebServiceRequest, Constants.SERVICENAME)
            {
                HttpMethod = HttpMethodName.PUT.ToString()
            };

            string resourcePath = ResourcePathBuilder.Standard()
                .WithProjectId(_disConfig.GetProjectId())
                .WithResource(new StreamResource(null, updateShardsRequest.StreamName))
                .Build();
            requestobs.ResourcePath = resourcePath;
            var result = Request<UpdateShardsResult>(updateShardsRequest, requestobs);
            return result;
        }

        public DescribeStreamResult DescribeStream(DescribeStreamRequest describeStreamRequest)
        {
            ObsWebServiceRequest obsWebServiceRequest = new DISWebServiceRequest();
            IRequest requestobs = new DISDefaultRequest(obsWebServiceRequest, Constants.SERVICENAME)
            {
                HttpMethod = HttpMethodName.GET.ToString()
            };

            var resourcePath = ResourcePathBuilder.Standard()
                .WithProjectId(_disConfig.GetProjectId())
                .WithResource(new StreamResource(null, describeStreamRequest.StreamName))
                .Build();
            requestobs.ResourcePath = resourcePath;
            var results = this.Request<DescribeStreamResult>(describeStreamRequest, requestobs);

            return results;
        }

        public DescribeAppResult DescribeApp(AppRequest appRequest)
        {
            ObsWebServiceRequest obsWebServiceRequest = new DISWebServiceRequest();
            IRequest requestobs = new DISDefaultRequest(obsWebServiceRequest, Constants.SERVICENAME)
            {
                HttpMethod = HttpMethodName.GET.ToString()
            };

            var resourcePath = ResourcePathBuilder.Standard()
                .WithProjectId(_disConfig.GetProjectId())
                .WithResource(new AppResource(null, appRequest.AppName))
                .Build();
            requestobs.ResourcePath = resourcePath;
            var results = this.Request<DescribeAppResult>(appRequest, requestobs);

            return results;
        }

        public GetMetricResult GetStreamMetricInfo(string streamName, GetStreamMetricRequest metricRequest)
        {
            ObsWebServiceRequest obsWebServiceRequest = new DISWebServiceRequest();
            IRequest requestobs = new DISDefaultRequest(obsWebServiceRequest, Constants.SERVICENAME)
            {
                HttpMethod = HttpMethodName.GET.ToString()
            };

            var resourcePath = ResourcePathBuilder.Standard()
                .WithProjectId(_disConfig.GetProjectId())
                .WithResource(new StreamResource(null, streamName))
                .WithResource(new MetricResource(null))
                .Build();
            requestobs.ResourcePath = resourcePath;
            var results = this.Request<GetMetricResult>(metricRequest, requestobs);

            return results;
        }

        public GetMetricResult GetPartitionMetricInfo(string streamName, string partitionId, GetPartitionMetricRequest metricRequest)
        {
            ObsWebServiceRequest obsWebServiceRequest = new DISWebServiceRequest();
            IRequest requestobs = new DISDefaultRequest(obsWebServiceRequest, Constants.SERVICENAME)
            {
                HttpMethod = HttpMethodName.GET.ToString()
            };

            var resourcePath = ResourcePathBuilder.Standard()
                .WithProjectId(_disConfig.GetProjectId())
                .WithResource(new StreamResource(null, streamName))
                .WithResource(new PartitionResource(null, partitionId))
                .WithResource(new MetricResource(null))
                .Build();
            requestobs.ResourcePath = resourcePath;
            var results = this.Request<GetMetricResult>(metricRequest, requestobs);

            return results;
        }

        public GetStreamConsumingResult GetStreamConsumingInfo(string streamName, string appName, GetStreamConsumingRequest consumingRequest)
        {
            ObsWebServiceRequest obsWebServiceRequest = new DISWebServiceRequest();
            IRequest requestobs = new DISDefaultRequest(obsWebServiceRequest, Constants.SERVICENAME)
            {
                HttpMethod = HttpMethodName.GET.ToString()
            };

            var resourcePath = ResourcePathBuilder.Standard()
                .WithProjectId(_disConfig.GetProjectId())
                .WithResource(new AppResource(null, appName))
                .WithResource(new StreamResource(null, streamName))
                .Build();
            requestobs.ResourcePath = resourcePath;
            var results = this.Request<GetStreamConsumingResult>(consumingRequest, requestobs);

            return results;
        }

        public StreamTransferTaskListResult GetStreamTransferTaskList(string streamName)
        {
            ObsWebServiceRequest obsWebServiceRequest = new DISWebServiceRequest();
            IRequest requestobs = new DISDefaultRequest(obsWebServiceRequest, Constants.SERVICENAME)
            {
                HttpMethod = HttpMethodName.GET.ToString()
            };

            var resourcePath = ResourcePathBuilder.Standard()
                .WithProjectId(_disConfig.GetProjectId())
                .WithResource(new StreamResource(null, streamName))
                .WithResource(new TransferTaskResource(null))
                .Build();
            requestobs.ResourcePath = resourcePath;
            var results = this.Request<StreamTransferTaskListResult>(null, requestobs);

            return results;
        }

        public StreamTransferTaskDetailResult GetStreamTransferTaskDetail(string streamName, string transferTaskName)
        {
            ObsWebServiceRequest obsWebServiceRequest = new DISWebServiceRequest();
            IRequest requestobs = new DISDefaultRequest(obsWebServiceRequest, Constants.SERVICENAME)
            {
                HttpMethod = HttpMethodName.GET.ToString()
            };

            var resourcePath = ResourcePathBuilder.Standard()
                .WithProjectId(_disConfig.GetProjectId())
                .WithResource(new StreamResource(null, streamName))
                .WithResource(new TransferTaskResource(null, transferTaskName))
                .Build();
            requestobs.ResourcePath = resourcePath;
            var results = this.Request<StreamTransferTaskDetailResult>(null, requestobs);

            return results;
        }

        public DescribeStreamListResult DescribeStreamList(DescribeStreamRequest describeStreamRequest)
        {
            ObsWebServiceRequest obsWebServiceRequest = new DISWebServiceRequest();
            IRequest requestobs = new DISDefaultRequest(obsWebServiceRequest, Constants.SERVICENAME)
            {
                HttpMethod = HttpMethodName.GET.ToString()
            };

            var resourcePath = ResourcePathBuilder.Standard()
                .WithProjectId(_disConfig.GetProjectId())
                .WithResource(new StreamResource(null, describeStreamRequest.StreamName))
                .Build();
            requestobs.ResourcePath = resourcePath;
            var results = this.Request<DescribeStreamListResult>(describeStreamRequest, requestobs);

            return results;
        }

        public DescribeAppListResult DescribeAppList(DescribeAppListRequest describeAppRequest)
        {
            ObsWebServiceRequest obsWebServiceRequest = new DISWebServiceRequest();
            IRequest requestobs = new DISDefaultRequest(obsWebServiceRequest, Constants.SERVICENAME)
            {
                HttpMethod = HttpMethodName.GET.ToString()
            };

            var resourcePath = ResourcePathBuilder.Standard()
                .WithProjectId(_disConfig.GetProjectId())
                .WithResource(new AppResource(null))
                .Build();
            requestobs.ResourcePath = resourcePath;
            var results = this.Request<DescribeAppListResult>(describeAppRequest, requestobs);

            return results;
        }

        /// <summary>
        /// 删除通道
        /// </summary>
        /// <param name="describeStreamRequest"></param>
        /// <returns></returns>
        public ResponseResult DeleteStream(DescribeStreamRequest describeStreamRequest)
        {
            ObsWebServiceRequest obsWebServiceRequest = new DISWebServiceRequest();
            IRequest requestobs = new DISDefaultRequest(obsWebServiceRequest, Constants.SERVICENAME)
            {
                HttpMethod = HttpMethodName.DELETE.ToString()
            };

            var resourcePath = ResourcePathBuilder.Standard()
                .WithProjectId(_disConfig.GetProjectId())
                .WithResource(new StreamResource(null, describeStreamRequest.StreamName))
                .Build();
            requestobs.ResourcePath = resourcePath;
            ResponseResult results = this.Request<ResponseResult>(describeStreamRequest, requestobs);

            return results;
        }

        /// <summary>
        /// 删除Checkpoint
        /// </summary>
        /// <param name="checkPointRequest"></param>
        /// <returns></returns>
        public ResponseResult DeleteCheckpoint(CheckPointRequest checkPointRequest)
        {
            ObsWebServiceRequest obsWebServiceRequest = new DISWebServiceRequest();
            IRequest requestobs = new DISDefaultRequest(obsWebServiceRequest, Constants.SERVICENAME)
            {
                HttpMethod = HttpMethodName.DELETE.ToString()
            };

            var resourcePath = ResourcePathBuilder.Standard()
                .WithProjectId(_disConfig.GetProjectId())
                .WithResource(new CheckPointResource(null))
                .Build();
            requestobs.ResourcePath = resourcePath;
            //requestobs.ResourcePath = resourcePath.TrimEnd('/') + "?stream_name=" + checkPointRequest.StreamName + "&app_name=" + checkPointRequest.AppName;

            //if (!string.IsNullOrEmpty(checkPointRequest.PartitionId))
            //{
            //    requestobs.ResourcePath += "&partition_id=" + checkPointRequest.PartitionId;
            //}

            //if (!string.IsNullOrEmpty(checkPointRequest.CheckpointType))
            //{
            //    requestobs.ResourcePath += "&checkpoint_type=" + checkPointRequest.CheckpointType;
            //}

            ResponseResult results = this.Request<ResponseResult>(checkPointRequest, requestobs);

            return results;
        }

        /// <summary>
        /// 创建通道
        /// </summary>
        /// <param name="createStreamRequest"></param>
        /// <returns></returns>
        public ResponseResult CreateStream(CreateStreamRequest createStreamRequest)
        {
            ObsWebServiceRequest obsWebServiceRequest = new DISWebServiceRequest();
            IRequest requestobs = new DISDefaultRequest(obsWebServiceRequest, Constants.SERVICENAME)
            {
                HttpMethod = HttpMethodName.POST.ToString()
            };

            var resourcePath = ResourcePathBuilder.Standard()
                .WithProjectId(_disConfig.GetProjectId())
                .WithResource(new StreamResource(null, ""))
                .Build();
            requestobs.ResourcePath = resourcePath;

            var results = this.Request<ResponseResult>(createStreamRequest, requestobs);

            return results;
        }

        public ResponseResult CreateTransferTask(string streamName, AddTransferTasksRequest addTransferTasksRequest)
        {
            ObsWebServiceRequest obsWebServiceRequest = new DISWebServiceRequest();
            IRequest requestobs = new DISDefaultRequest(obsWebServiceRequest, Constants.SERVICENAME)
            {
                HttpMethod = HttpMethodName.POST.ToString()
            };

            var resourcePath = ResourcePathBuilder.Standard()
                .WithProjectId(_disConfig.GetProjectId())
                .WithResource(new StreamResource(null, streamName))
                .WithResource(new TransferTaskResource(null))
                .Build();
            requestobs.ResourcePath = resourcePath;
            var results = this.Request<ResponseResult>(addTransferTasksRequest, requestobs);

            return results;
        }

        //public ResponseResult CreateSchema(CreateSchemaRequest schemaRequest)
        //{
        //    ObsWebServiceRequest obsWebServiceRequest = new DISWebServiceRequest();
        //    IRequest requestobs = new DISDefaultRequest(obsWebServiceRequest, Constants.SERVICENAME)
        //    {
        //        HttpMethod = HttpMethodName.POST.ToString()
        //    };

        //    var resourcePath = ResourcePathBuilder.Standard().WithVersion("v1")
        //        .WithProjectId(_disConfig.GetProjectId())
        //        .WithResource(new SchemaResource(null))
        //        .Build();
        //    requestobs.ResourcePath = resourcePath.Trim('/') + "?type=JSON";
        //    var results = this.Request<ResponseResult>(schemaRequest, requestobs);

        //    return results;
        //}

        public ResponseResult CreateApp(AppRequest getShardIteratorParam)
        {
            ObsWebServiceRequest obsWebServiceRequest = new DISWebServiceRequest();
            OBS.Runtime.Internal.IRequest requestobs = new DISDefaultRequest(obsWebServiceRequest, Constants.SERVICENAME)
            {
                HttpMethod = HttpMethodName.POST.ToString()
            };

            string resourcePath = ResourcePathBuilder.Standard()
                                                     .WithProjectId(_disConfig.GetProjectId())
                                                     .WithResource(new AppResource(null))
                                                     .Build();
            requestobs.ResourcePath = resourcePath;
            var results = Request<ResponseResult>(getShardIteratorParam, requestobs);

            return results;
        }

        public ResponseResult DeleteApp(AppRequest createAppRequest)
        {
            ObsWebServiceRequest obsWebServiceRequest = new DISWebServiceRequest();
            OBS.Runtime.Internal.IRequest requestobs = new DISDefaultRequest(obsWebServiceRequest, Constants.SERVICENAME)
            {
                HttpMethod = HttpMethodName.DELETE.ToString()
            };

            string resourcePath = ResourcePathBuilder.Standard()
                                                     .WithProjectId(_disConfig.GetProjectId())
                                                     .WithResource(new AppResource(null, createAppRequest.AppName))
                                                     .Build();
            requestobs.ResourcePath = resourcePath;
            ResponseResult results = null;
            try
            {
                results = Request<ResponseResult>(createAppRequest, requestobs);
            }
            catch (Exception)
            {

                throw;
            }

            return results;
        }

        public ResponseResult DeleteTransferTask(string streamName, string transferTaskName)
        {
            ObsWebServiceRequest obsWebServiceRequest = new DISWebServiceRequest();
            OBS.Runtime.Internal.IRequest requestobs = new DISDefaultRequest(obsWebServiceRequest, Constants.SERVICENAME)
            {
                HttpMethod = HttpMethodName.DELETE.ToString()
            };

            string resourcePath = ResourcePathBuilder.Standard()
                                                     .WithProjectId(_disConfig.GetProjectId())
                                                     .WithResource(new StreamResource(null, streamName))
                                                     .WithResource(new TransferTaskResource(null, transferTaskName))
                                                     .Build();
            requestobs.ResourcePath = resourcePath;
            ResponseResult results = null;
            try
            {
                results = Request<ResponseResult>(null, requestobs);
            }
            catch (Exception)
            {

                throw;
            }

            return results;
        }

        #endregion

        #region Helper Methods
        private bool IsEncrypt()
        {
            return _disConfig.GetIsDefaultDataEncryptEnabled() && !string.IsNullOrEmpty(_disConfig.GetDataPassword());
        }

        private byte[] Encrypt(MemoryStream src)
        {
            String cipher = null;
            try
            {
                cipher = EncryptUtils.Gen(new String[] { _disConfig.GetDataPassword() }, src.ToArray());
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                throw new Exception(e.Message);
            }
            return Utils.Utils.EncodingBytes(cipher);
        }

        private byte[] Decrypt(byte[] cipher)
        {
            byte[] src;
            try
            {
                src = EncryptUtils.Dec(new String[] { _disConfig.GetDataPassword() }, Utils.Utils.DecodingString(cipher));
            }
            catch (Exception e)
            {
                logger.Error(e.Message, e);
                throw new Exception(e.Message);
            }
            return src;
        }

        private MemoryStream Decrypt(MemoryStream cipher)
        {
            throw new NotImplementedException();
        }

        protected void Check()
        {
            if (_credentials == null)
            {
                throw new NullReferenceException("credentials can not be null.");
            }

            if (string.IsNullOrEmpty(_credentials.GetAccessKeyId()))
            {
                throw new NullReferenceException("credentials ak can not be null.");
            }

            if (string.IsNullOrEmpty(_credentials.GetSecretKey()))
            {
                throw new NullReferenceException("credentials sk can not be null.");
            }

            if (string.IsNullOrEmpty(_region))
            {
                throw new NullReferenceException("region can not be null.");
            }
        }
        #endregion

        #region Request execution methods


        public T Request<T>(object param, OBS.Runtime.Internal.IRequest request) where T : new()
        {
            try
            {
                Check();
                var clientWrapper = new RestClientWrapper(request, _disConfig);
                string result = clientWrapper.Request(param, _credentials.GetAccessKeyId(), _credentials.GetSecretKey(), _region, _disConfig.GetProjectId());

                result = result.Equals("Created") ? "{\"status_code\":\"201 Created\",\"content\":\"\"}"
                    : (result.Equals("NoContent") ? "{\"status_code\":\"204 NO CONTENT\",\"content\":\"\"}"
                    : result);

                if (result.Contains("errorCode") && result.Contains("message"))
                {
                    throw new Exception(result);
                }
                else
                {
                    return JsonConvert.DeserializeObject<T>(result);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }


        #endregion

    }
}
