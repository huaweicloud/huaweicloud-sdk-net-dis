using System;
using System.Linq;
using System.Net;
using Com.Bigdata.Dis.Sdk.DISCommon.Config;
using Com.Bigdata.Dis.Sdk.DISCommon.DISWeb;
using Com.Bigdata.Dis.Sdk.DISCommon.Ext;
using Com.Bigdata.Dis.Sdk.DISCommon.Model;
using Com.Bigdata.Dis.Sdk.DISCommon.Signer;
using Com.Bigdata.Dis.Sdk.DISCommon.Utils;
using OBS.Runtime.Internal;
using System.Threading.Tasks;

namespace DISClient_4._5.Client
{
    public class RestClientWrapper : Com.Bigdata.Dis.Sdk.DISCommon.Client.RestClientWrapper
    {
        private IBasicIdisAsyncRestClient _ibasicIdisRestClient = new BasicDisAsyncRestClient();

        protected IDISRestClient _idisRestClient_4_5;        

        public RestClientWrapper(IRequest<HttpRequest> request, DISConfig disConfig) : base(request, disConfig)
        {
            this.disConfig = disConfig;
            this._idisRestClient_4_5 = BasicDisRestClient.GetInstance(disConfig);
            this._idisRestClient = Com.Bigdata.Dis.Sdk.DISCommon.Client.BasicDisRestClient.GetInstance(disConfig);
        }

        public RestClientWrapper(OBS.Runtime.Internal.IRequest request, DISConfig disConfig) : base(request, disConfig)
        {
            this.requestObs = request;
            this.disConfig = disConfig;
            this._idisRestClient_4_5 = BasicDisRestClient.GetInstance(disConfig);
            this._idisRestClient = Com.Bigdata.Dis.Sdk.DISCommon.Client.BasicDisRestClient.GetInstance(disConfig);
        }

        public RestClientWrapper(IDISRequest request, DISConfig disConfig) : base(request, disConfig)
        {
            this.disRequest = (DefaultRequest)request;
            this.disConfig = disConfig;
            this._idisRestClient_4_5 = BasicDisRestClient.GetInstance(disConfig);
            this._idisRestClient = Com.Bigdata.Dis.Sdk.DISCommon.Client.BasicDisRestClient.GetInstance(disConfig);
        }


        public T Request<T>(object requestContent, string ak, string sk, string region, string projectId, InterfaceType interfaceName = InterfaceType.DISInterfaceNone)
        {
            BeforeRequest(requestContent, region, projectId);

            requestObs = SignUtil.Sign(requestObs, ak, sk, region);

            T result = DoRequest<T>(requestContent, interfaceName);
            return result;
        }

        private T DoRequest<T>(object requestContent, InterfaceType interfaceName = InterfaceType.DISInterfaceNone)
        {
            int retryCount = -1;
            ExponentialBackOff backOff = null;
            do
            {
                retryCount++;
                if (retryCount > 0)
                {
                    // 等待一段时间再发起重试
                    if (backOff == null)
                    {
                        backOff = new ExponentialBackOff(250, 2.0, disConfig.GetBackOffMaxIntervalMs(),
                            ExponentialBackOff.DEFAULT_MAX_ELAPSED_TIME);
                    }
                    backOff.backOff(backOff.getNextBackOff());
                }

                try
                {
                    requestObs.Headers.Remove(HttpHeaderKeys.Authorization);
                    requestObs.Headers.Remove(HttpHeaderKeys.SdkData);
                    requestObs.Headers.Remove(HttpHeaderKeys.SdkShaContent);
                    requestObs.Headers.Remove(HttpHeaderKeys.HostHeader);
                    // 每次重传需要重新签名
                    requestObs = SignUtil.Sign(requestObs, disConfig.GetAK(), disConfig.GetSK(), disConfig.GetRegion());
                    return DoRequest<T>(requestObs, requestContent, interfaceName); 
                }
                catch (Exception t)
                {
                    String errorMsg = t.Message;
                    int statusCode = int.Parse(errorMsg.Split('\n')[0]);
                    // 如果不是可以重试的异常 或者 已达到重试次数，则直接抛出异常
                    if (!Utils.IsRetriableSendException(statusCode) || retryCount >= disConfig.GetExceptionRetries())
                    {
                        throw new Exception(errorMsg.Substring(statusCode.ToString().Length + 1), t);
                    }

                    logger.WarnFormat("Find Retriable Exception {0}, url [{1} {2}], currRetryCount is {3}",
                    errorMsg.Replace("\r\n", ""), requestObs.HttpMethod, requestObs.Endpoint.Host.Trim('/') + requestObs.ResourcePath, retryCount);
                }
                
            } while (retryCount < disConfig.GetExceptionRetries());

            return default(T);
        }

        private T DoRequest<T>(OBS.Runtime.Internal.IRequest requestEx, object requestContent, InterfaceType interfaceName = InterfaceType.DISInterfaceNone)
        {
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, certificate, chain, sslPolicyErrors) => true;

            ServicePointManager.Expect100Continue = true;
            //如果是4.0以下的版本
            if (int.Parse(Environment.Version.ToString().Split('.').FirstOrDefault()) < 4)
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                    | SecurityProtocolType.Ssl3
                    | (SecurityProtocolType)768
                    | (SecurityProtocolType)3072;

            }
            //如果是4.0以上的版本
            else
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                    | SecurityProtocolType.Ssl3
                                    | (SecurityProtocolType)768
                                    | (SecurityProtocolType)3072;
            }

            ServicePointManager.DefaultConnectionLimit = 9999;

            HttpMethodName method = (HttpMethodName)Enum.Parse(typeof(HttpMethodName), requestObs.HttpMethod);
            switch (method)
            {
                case HttpMethodName.POST:
                    {
                        return _idisRestClient_4_5.Post<T>(requestEx.Endpoint.AbsoluteUri, requestEx.ResourcePath, requestEx.Headers, requestContent, interfaceName);
                    }
                case HttpMethodName.GET:
                    {
                        var requestContext = new RequestContext(true)
                        {
                            OriginalRequest = new DISWebServiceRequest(),
                            Request = requestEx,
                            ClientConfig = new DISExConfig
                            {
                                Timeout = new TimeSpan(1, 0, 0, 0),
                                ReadWriteTimeout = new TimeSpan(1, 0, 0, 0)
                            }
                        };
                        return _idisRestClient_4_5.Get<T>(requestContext, disConfig.GetFinalBaseURL(), requestEx.ResourcePath, requestEx.Headers, requestContent, interfaceName);
                    }
                case HttpMethodName.PUT:
                    return _idisRestClient_4_5.Put<T>(requestEx.Endpoint.AbsoluteUri, requestEx.ResourcePath, requestEx.Headers, requestContent);

                case HttpMethodName.DELETE:
                    return _idisRestClient_4_5.Delete<T>(disConfig.GetFinalBaseURL() + requestEx.ResourcePath, requestEx.Headers, requestContent);
            }

            return default(T);
        }

        public async Task<string> RequestAsync(object requestContent, string ak, string sk, string region, string projectId)
        {
            this.BeforeRequest(requestContent, region, projectId);

            requestObs = SignUtil.Sign(requestObs, ak, sk, region);

            var result = await DoRequestAsync<string>(requestContent);
            return result;
        }

        private async Task<T> DoRequestAsync<T>(object requestContent)
        {
            int retryCount = -1;
            ExponentialBackOff backOff = null;
            do
            {
                retryCount++;
                if (retryCount > 0)
                {
                    // 等待一段时间再发起重试
                    if (backOff == null)
                    {
                        backOff = new ExponentialBackOff(250, 2.0, disConfig.GetBackOffMaxIntervalMs(),
                            ExponentialBackOff.DEFAULT_MAX_ELAPSED_TIME);
                    }
                    backOff.backOff(backOff.getNextBackOff());
                }

                try
                {
                    requestObs.Headers.Remove(HttpHeaderKeys.Authorization);
                    requestObs.Headers.Remove(HttpHeaderKeys.SdkData);
                    requestObs.Headers.Remove(HttpHeaderKeys.SdkShaContent);
                    requestObs.Headers.Remove(HttpHeaderKeys.HostHeader);
                    // 每次重传需要重新签名
                    requestObs = SignUtil.Sign(requestObs, disConfig.GetAK(), disConfig.GetSK(), disConfig.GetRegion());
                    return await DoRequestAsync<T>(requestObs, requestContent);
                }
                catch (Exception t)
                {
                    String errorMsg = t.Message;
                    int statusCode = int.Parse(errorMsg.Split('\n')[0]);
                    // 如果不是可以重试的异常 或者 已达到重试次数，则直接抛出异常
                    if (!Utils.IsRetriableSendException(statusCode) || retryCount >= disConfig.GetExceptionRetries())
                    {
                        throw new Exception(errorMsg.Substring(statusCode.ToString().Length + 1), t);
                    }

                    logger.WarnFormat("Find Retriable Exception {0}, url [{1} {2}], currRetryCount is {3}",
                    errorMsg.Replace("\r\n", ""), requestObs.HttpMethod, requestObs.Endpoint.Host.Trim('/') + requestObs.ResourcePath, retryCount);
                }
            } while (retryCount < disConfig.GetExceptionRetries());

            return default(T);

            //return await DoRequestAsync<T>(requestObs, requestContent);
        }

        private async Task<T> DoRequestAsync<T>(OBS.Runtime.Internal.IRequest requestEx, object requestContent)
        {
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, certificate, chain, sslPolicyErrors) => true;

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol =
                  SecurityProtocolType.Tls
                | SecurityProtocolType.Ssl3
                | SecurityProtocolType.Tls11
                | SecurityProtocolType.Tls12;

            ServicePointManager.DefaultConnectionLimit = 9999;

            HttpMethodName method = (HttpMethodName)Enum.Parse(typeof(HttpMethodName), requestObs.HttpMethod);
            switch (method)
            {
                case HttpMethodName.POST:
                    {
                        return await _ibasicIdisRestClient.PostAsync<T>(requestEx.Endpoint.AbsoluteUri, requestEx.ResourcePath, requestEx.Headers, requestContent);
                    }
            }

            return default(T);
        }
    }
}
