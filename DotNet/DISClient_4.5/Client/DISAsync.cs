using System;
using Com.Bigdata.Dis.Sdk.DISCommon.Model;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using Com.Bigdata.Dis.Sdk.DISCommon;
using OBS.Runtime;
using Com.Bigdata.Dis.Sdk.DISCommon.Ext;
using Com.Bigdata.Dis.Sdk.DISCommon.DISWeb;
using Newtonsoft.Json;
using Com.Bigdata.Dis.Sdk.DISCommon.Config;
using Com.Bigdata.Dis.Sdk.DISCommon.Auth;
using System.Threading;
using Com.Bigdata.Dis.Sdk.DISCommon.Utils;
using Com.Bigdata.Dis.Sdk.DISCommon.Utils.Cache;

namespace DISClient_4._5.Client
{
    public delegate Task<PutRecordsResult> PutRecordAsyncMethod(PutRecordsRequest putRecordsParam);

    public class DISAsync : DISIngestionClient, IDISAsync
    {
        private static object objlock = new object();

        bool acquireLock = false;

        public DISAsync()
        {
            _disConfig = DISConfig.buildDefaultConfig();
            _credentials = new BasicCredentials(_disConfig.GetAK(), _disConfig.GetSK());
            _region = _disConfig.GetRegion();
        }

        public DISAsync(DISConfig disConfig)
        {
            _disConfig = DISConfig.BuildConfig(disConfig);
            _credentials = new BasicCredentials(_disConfig.GetAK(), _disConfig.GetSK());
            _region = _disConfig.GetRegion();
        }

        public async Task<int> PutFilesAsync(PutRecordsRequest putFilesRequest)
        {
            var result = await this.UploadFile(putFilesRequest.StreamName, putFilesRequest.FilePath, putFilesRequest.FileName);
            return result;
        }

        private async Task<int> UploadFile(string streamName, string file, string fileName)
        {
            int failedRecordCount = 0;
            //最多上传128M的文件，这里先设置为最多上传500k
            int splitFileSize = 500 * 1024;
            FileInfo fileInfo = new FileInfo(file);
            //分几次传
            int steps = (int)Math.Ceiling((decimal)fileInfo.Length / splitFileSize);
            string deliverDataId = Guid.NewGuid().ToString("N");
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
                                    FileName = fileName,
                                    DeliverDataId = deliverDataId,
                                    SequenceNumber = i,
                                    EndFlag = i == steps - 1 ? true : false,
                                }
                            };
                            putRecordsRequestEntries.Add(putRecordsRequestEntry);
                            putRecordsRequest.Records = putRecordsRequestEntries;

                            PutRecordsResult response = await this.UploadFile(putRecordsRequest);
                            failedRecordCount += response.FailedRecordCount;
                            //Thread.Sleep(500);
                            foreach (var item in response.Records)
                            {
                                Console.WriteLine("异步" + item);
                            }
                        }
                    }
                }
            }
            return failedRecordCount;
        }

        private async Task<PutRecordsResult> UploadFile(PutRecordsRequest putRecordsParam)
        {
            return await InnerPutRecordsSupportingCache(putRecordsParam, new PutRecordAsyncMethod(InnerUploadFile));
        }

        protected async Task<PutRecordsResult> InnerPutRecordsSupportingCache(PutRecordsRequest putRecordsParam, PutRecordAsyncMethod putRecordMethod)
        {
            if (_disConfig.IsDataCacheEnabled())
            {
                // 开启本地缓存
                PutRecordsResult putRecordsResult = null;
                try
                {
                    putRecordsResult = await InnerPutRecordsWithRetry(putRecordsParam, putRecordMethod);
                }
                catch (Exception e)
                {
                    string errorMsg = e.Message;
                    int statusCode = int.Parse(errorMsg.Split('\n')[0]);
                    // 如果不是可以重试的异常 或者 已达到重试次数，则直接抛出异常
                    if (Utils.IsCacheData(statusCode))
                    {
                        // 网络异常 全部记录上传失败
                        logger.Info("Local data cache is enabled, try to put failed records to local.");
                        CacheUtils.PutToCache(putRecordsParam, _disConfig); // 写入本地缓存
                    }
                    throw new Exception(e.Message.Substring(statusCode.ToString().Length + 1));
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
                return await InnerPutRecordsWithRetry(putRecordsParam, putRecordMethod);
            }
        }

        protected async Task<PutRecordsResult> InnerPutRecordsWithRetry(PutRecordsRequest putRecordsParam, PutRecordAsyncMethod putRecordMethod)
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
                            Monitor.Enter(objlock, ref acquireLock);
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
                        putRecordsResult = await putRecordMethod(retryPutRecordsRequest);
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
            //putRecordsResult.Result = new PutRecordsResult();
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

        private async Task<PutRecordsResult> InnerUploadFile(PutRecordsRequest putRecordsParam)
        {
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
            var results = await Request<PutRecordsResult>(putRecordsParam, requestobs);

            return results;
        }

        private async new Task<T> Request<T>(object param, OBS.Runtime.Internal.IRequest request) where T : new()
        {
            try
            {
                var clientWrapper = new RestClientWrapper(request, _disConfig);
                string result = await clientWrapper.RequestAsync(param, _credentials.GetAccessKeyId(), _credentials.GetSecretKey(), _region, _disConfig.GetProjectId());

                int statusCode = int.Parse(result.Split('\n')[0]);
                var task = new TaskCompletionSource<T>();
                if (statusCode >= 200 && statusCode < 300)
                {
                    result = result.Substring(statusCode.ToString().Length + 1);
                    result = result.Equals("Created") ? "{\"status_code\":\"201 Created\",\"content\":\"\"}"
                            : (result.Equals("NoContent") ? "{\"status_code\":\"204 NO CONTENT\",\"content\":\"\"}"
                            : result);
                    task.SetResult(JsonConvert.DeserializeObject<T>(result));
                    return task.Task.Result;
                }
                else
                {
                    throw new Exception(result);
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
