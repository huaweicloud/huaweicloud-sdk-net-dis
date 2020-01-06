using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Com.Bigdata.Dis.Sdk.DISCommon.Config;
using Com.Bigdata.Dis.Sdk.DISCommon.DISWeb;
using Com.Bigdata.Dis.Sdk.DISCommon.Ext;
using Com.Bigdata.Dis.Sdk.DISCommon.Model;
using Com.Bigdata.Dis.Sdk.DISCommon.Signer;
using Com.Bigdata.Dis.Sdk.DISCommon.Utils;
using Com.Bigdata.Dis.Sdk.DISCommon.Log;
using Newtonsoft.Json;
using OBS.Runtime.Internal;
using Com.Bigdata.Dis.Sdk.DISCommon.Interface;
using log4net;


namespace Com.Bigdata.Dis.Sdk.DISCommon.Client
{
    public class RestClientWrapper
    {
        private const string HEADER_SDK_VERSION = "X-SDK-Version";
        private const string HEADER_PROJECT_ID = "X-Project-Id";
        protected DISConfig disConfig;
        protected IDISRestClient _idisRestClient;
        protected DefaultRequest disRequest;
        protected OBS.Runtime.Internal.IRequest requestObs;
        protected static ILog logger = LogHelper.GetInstance();

        public RestClientWrapper(IRequest<HttpRequest> request, DISConfig disConfig)
        {
            this.disConfig = disConfig;
            this._idisRestClient = BasicDisRestClient.GetInstance(disConfig);
        }

        public RestClientWrapper(OBS.Runtime.Internal.IRequest request, DISConfig disConfig)
        {
            this.requestObs = request;
            this.disConfig = disConfig;
            this._idisRestClient = BasicDisRestClient.GetInstance(disConfig);
        }

        public RestClientWrapper(IDISRequest request, DISConfig disConfig)
        {
            this.disRequest = (DefaultRequest)request;
            this.disConfig = disConfig;
            this._idisRestClient = BasicDisRestClient.GetInstance(disConfig);
        }

        public string Request(object requestContent, string ak, string sk, string region, string projectId)
        {
            BeforeRequest(requestContent, region, projectId);

            requestObs = SignUtil.Sign(requestObs, ak, sk, region);

            string result = DoRequest<string>(requestContent);
            return result;
        }

        protected void BeforeRequest(object requestContent, string region, string projectId)
        {
            //first set ResourcePath，then set endpoint
            SetResourcePath();
            // Set endpoint
            SetEndpoint();

            // Set request header
            SetContentType();
            SetSdkVersion();
            SetProjectId(projectId);
            // Set request parameters
            //get
            SetParameters(requestContent);

            // Set request content
            //post
            SetContent(requestContent);

            // Set request querystring
            //delete
            SetQueryString(requestContent);

        }

        protected void SetQueryString(object requestContent)
        {
            bool isDELETE = false;
            isDELETE = requestObs.HttpMethod.Equals(HttpMethodName.DELETE.ToString());

            if (isDELETE)
            {
                if (requestContent != null)
                {
                    requestObs.UseQueryString = true;

                    var parametersMap = new Dictionary<string, string>();
                    Dictionary<string, object> getParamsObj;
                    if (requestContent is Dictionary<string, object>)
                    {
                        getParamsObj = (Dictionary<string, object>)requestContent;
                    }
                    else
                    {
                        var tmpJson = JsonConvert.SerializeObject(requestContent);
                        getParamsObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(tmpJson);
                    }

                    if (getParamsObj.Count != 0)
                    {
                        foreach (var keyValue in getParamsObj)
                        {
                            var value = keyValue.Value;
                            if (value == null)
                            {
                                continue;
                            }

                            parametersMap[keyValue.Key] = value.ToString();
                        }
                    }

                    if (parametersMap.Count > 0)
                    {
                        foreach (KeyValuePair<string, string> keyValuePair in parametersMap)
                        {
                            requestObs.Parameters.Add(keyValuePair.Key, keyValuePair.Value);
                        }
                    }
                }
            }
        }

        protected void SetContent(object requestContent)
        {
            bool isPost = false;
            isPost = requestObs.HttpMethod.Equals(HttpMethodName.POST.ToString()) || requestObs.HttpMethod.Equals(HttpMethodName.PUT.ToString());

            if (isPost)
            {
                if (requestContent is byte[])
                {
                    requestObs.Content = (byte[])requestContent;
                }
                else if (requestContent is string || requestContent is int)
                {
                    requestObs.Content = Utils.Utils.EncodingBytes(requestContent.ToString());
                }
                else
                {
                    string reqJson = JsonConvert.SerializeObject(requestContent);
                    requestObs.Content = Utils.Utils.EncodingBytes(reqJson);
                }
            }
        }

        protected void SetContentType()
        {
            if (!requestObs.Headers.ContainsKey("Content-Type"))
            {
                requestObs.Headers.Add("Content-Type", "application/json; charset=utf-8");
            }

            if (!requestObs.Headers.ContainsKey("accept"))
            {
                requestObs.Headers.Add("accept", "application/json, application/xml, text/json, text/x-json, text/javascript, text/xml");
            }
        }


        public void SetResourcePath()
        {
            var url = new Uri(disConfig.GetEndpoint());
            if (!string.IsNullOrEmpty(url.LocalPath.Trim('/')))
            {
                requestObs.ResourcePath = url.LocalPath + requestObs.ResourcePath;
            }
        }


        protected void SetSdkVersion()
        {
            requestObs.Headers.Add(HEADER_SDK_VERSION, VersionUtils.GetVersion() + "/" + VersionUtils.GetPlatform());
        }

        protected void SetProjectId(string projectId)
        {
            requestObs.Headers.Add(HEADER_PROJECT_ID, projectId);
        }

        protected void SetEndpoint()
        {
            var url = new Uri(disConfig.GetEndpoint());
            var endpoint = new Uri((url.Scheme + "://" + url.Host + ":" + url.Port).ToString());
            requestObs.Endpoint = endpoint;
        }

        protected void SetParameters(object requestContent)
        {
            bool isGet = false;

            isGet = requestObs.HttpMethod.Equals(HttpMethodName.GET.ToString());

            if (isGet)
            {
                if (requestContent != null)
                {
                    var parametersMap = new Dictionary<string, string>();
                    Dictionary<string, object> getParamsObj;
                    if (requestContent is Dictionary<string, object>)
                    {
                        getParamsObj = (Dictionary<string, object>)requestContent;
                    }
                    else
                    {
                        var tmpJson = JsonConvert.SerializeObject(requestContent);
                        getParamsObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(tmpJson);
                    }

                    if (getParamsObj.Count != 0)
                    {
                        foreach (var keyValue in getParamsObj)
                        {
                            var value = keyValue.Value;
                            if (value == null)
                            {
                                continue;
                            }

                            parametersMap[keyValue.Key] = value.ToString();
                        }
                    }

                    if (parametersMap.Count > 0)
                    {
                        foreach (KeyValuePair<string, string> keyValuePair in parametersMap)
                        {
                            requestObs.Parameters.Add(keyValuePair.Key, keyValuePair.Value);
                        }
                    }
                }
            }

        }

        protected T DoRequest<T>(object requestContent)
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
                    return DoRequest<T>(requestObs, requestContent); 
                }
                catch (Exception t)
                {
                    String errorMsg = t.Message;
                    int statusCode = int.Parse(errorMsg.Split('\n')[0]);
                    // 如果不是可以重试的异常 或者 已达到重试次数，则直接抛出异常
                    if (!Utils.Utils.IsRetriableSendException(statusCode) || retryCount >= disConfig.GetExceptionRetries())
                    {
                        throw new Exception(errorMsg.Substring(statusCode.ToString().Length + 1), t);
                    }

                    logger.WarnFormat("Find Retriable Exception {0}, url [{1} {2}], currRetryCount is {3}",
                    errorMsg.Replace("\r\n", ""), requestObs.HttpMethod, requestObs.Endpoint.Host.Trim('/') + requestObs.ResourcePath, retryCount);
                }
                
            } while (retryCount < disConfig.GetExceptionRetries());

            return default(T);
        }

        protected T DoRequest<T>(OBS.Runtime.Internal.IRequest requestEx, object requestContent)
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
                        //return _idisRestClient.Post<T>(disConfig.GetFinalBaseURL(), requestEx.ResourcePath, requestEx.Headers, requestContent);
                        return _idisRestClient.Post<T>(requestEx.Endpoint.AbsoluteUri, requestEx.ResourcePath, requestEx.Headers, requestContent);
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
                        return _idisRestClient.Get<T>(requestContext, disConfig.GetFinalBaseURL(), requestEx.ResourcePath, requestEx.Headers, requestContent);

                    }
                case HttpMethodName.PUT:
                    return _idisRestClient.Put<T>(requestEx.Endpoint.AbsoluteUri, requestEx.ResourcePath, requestEx.Headers, requestContent);

                case HttpMethodName.DELETE:
                    return _idisRestClient.Delete<T>(disConfig.GetFinalBaseURL() + requestEx.ResourcePath, requestEx.Headers, requestContent);
            }

            return default(T);
        }
    }
}
