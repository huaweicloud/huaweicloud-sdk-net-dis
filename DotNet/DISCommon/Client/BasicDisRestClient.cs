using System;
using System.Collections.Generic;
using Com.Bigdata.Dis.Sdk.DISCommon.Config;
using Com.Bigdata.Dis.Sdk.DISCommon.Interface;
using Newtonsoft.Json;
using OBS.Runtime.Internal;
using RestSharp;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Com.Bigdata.Dis.Sdk.DISCommon.Log;
using log4net;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Client
{
    public class BasicDisRestClient : IDISRestClient
    {
        protected static volatile IDISRestClient _idisRestClient;

        private static object _lock = new object();

        protected DISConfig _disConfig;

        protected static ILog logger = LogHelper.GetInstance();

        protected BasicDisRestClient(DISConfig disConfig)
        {
            this._disConfig = disConfig;
        }

        public static IDISRestClient GetInstance(DISConfig disConfig)
        {
            if (_idisRestClient != null)
                return _idisRestClient;

            lock (_lock)
            {
                if (_idisRestClient == null)
                {
                    _idisRestClient = new BasicDisRestClient(disConfig);
                }
            }

            return _idisRestClient;
        }

        public T Get<T>(string url, IDictionary<string, string> headerMaps, object req)
        {
            string kkk = Execute(null, url, "", headerMaps, req);

            return (T)Convert.ChangeType(kkk, TypeCode.String);
        }

        public T Get<T>(string baseUrl, string resource, IDictionary<string, string> headerMaps, object req)
        {
            string kkk = Execute(null, baseUrl, resource, headerMaps, req);

            return (T)Convert.ChangeType(kkk, TypeCode.String);
        }

        public T Get<T>(RequestContext requestContext, string baseUrl, string resource, IDictionary<string, string> headerMaps, object req)
        {
            string kkk = Execute(requestContext, baseUrl, resource, headerMaps, req);

            return (T)Convert.ChangeType(kkk, TypeCode.String);
        }

        public T Post<T>(string baseUrl, string resource, IDictionary<string, string> headerMaps, object req)
        {
            string kkk = ExecutePost(baseUrl, resource, headerMaps, req);

            return (T)Convert.ChangeType(kkk, TypeCode.String);
        }

        public T Post<T>(string url, IDictionary<string, string> headerMaps, object req)
        {
            string kkk = "";

            return (T)Convert.ChangeType(kkk, TypeCode.String);
        }

        public T Put<T>(string url, string resource, IDictionary<string, string> headerMaps, object req)
        {
            string kkk = ExecutePut(url, resource, headerMaps, req);

            return (T)Convert.ChangeType(kkk, TypeCode.String);
        }

        public T Delete<T>(string url, IDictionary<string, string> headerMaps, object req)
        {
            string kkk = ExecuteDelete(url, headerMaps, req);

            return (T)Convert.ChangeType(kkk, TypeCode.String);
        }

        /// <summary>
        /// delete
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="headerMaps"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public String ExecuteDelete(string baseUrl, /*string resource,*/ IDictionary<string, string> headerMaps, object req)
        {
            var client = new RestSharp.RestClient(baseUrl.Trim('/'));
            var request = new RestRequest(Method.DELETE);

            //var client = new RestSharp.RestClient(baseUrl.Trim('/') + "?" + urlParam.TrimEnd('&'));
            //Resource = resource.Trim('/')

            request.RequestFormat = DataFormat.Json;
            foreach (KeyValuePair<string, string> keyValuePair in headerMaps)
            {
                request.AddHeader(keyValuePair.Key, keyValuePair.Value);
            }

            var tmpJson = JsonConvert.SerializeObject(req);
            var getParamsObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(tmpJson);
            //string urlParam = "";
            if (null != getParamsObj)
            {
                foreach (var o in getParamsObj)
                {
                    request.AddParameter(o.Key, o.Value.ToString(), ParameterType.QueryString);
                    //urlParam += o.Key + "=" + o.Value.ToString() + "&";
                }
            }

            try
            {
                var response = client.Execute(request);
                int statusCode = (int)response.StatusCode;
                if (statusCode >= 200 && statusCode < 300)
                {
                    return !string.IsNullOrEmpty(response.Content)
                        ? response.Content.Equals("{}") ? response.StatusCode.ToString() : response.Content
                        : response.StatusCode.ToString();
                }
                else
                {
                    if (!string.IsNullOrEmpty(response.Content))
                    {
                        throw new Exception(statusCode.ToString() + "\n" + response.Content);
                    }
                    else
                    {
                        throw new Exception(statusCode.ToString() + "\n" + response.ErrorMessage);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// put
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="headerMaps"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public String ExecutePut(string baseUrl, string resource, IDictionary<string, string> headerMaps, object req)
        {
            var client = new RestSharp.RestClient(baseUrl.Trim('/'));
            var request = new RestRequest(Method.PUT)
            {
                Resource = resource.Trim('/')
            };
            var tmpJson = JsonConvert.SerializeObject(req);
            request.AddParameter("application/json; charset=utf-8", tmpJson, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            foreach (KeyValuePair<string, string> keyValuePair in headerMaps)
            {
                request.AddHeader(keyValuePair.Key, keyValuePair.Value);
            }

            try
            {
                var response = client.Execute(request);
                int statusCode = (int)response.StatusCode;
                if (statusCode >= 200 && statusCode < 300)
                {
                    return !string.IsNullOrEmpty(response.Content)
                        ? response.Content.Equals("{}") ? response.StatusCode.ToString() : response.Content
                        : response.StatusCode.ToString();
                }
                else
                {
                    if (!string.IsNullOrEmpty(response.Content))
                    {
                        throw new Exception(statusCode.ToString() + "\n" + response.Content);
                    }
                    else
                    {
                        throw new Exception(statusCode.ToString() + "\n" + response.ErrorMessage);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// post
        /// </summary>
        /// <param name="baseUrl"></param>
        /// <param name="resource"></param>
        /// <param name="headerMaps"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public String ExecutePost(string baseUrl, string resource, IDictionary<string, string> headerMaps, object req)
        {
            var client = new RestSharp.RestClient(baseUrl);
            var request = new RestRequest(Method.POST) { Resource = resource.Trim('/') };
            var tmpJson = JsonConvert.SerializeObject(req);
            request.AddParameter("application/json; charset=utf-8", tmpJson, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            foreach (KeyValuePair<string, string> keyValuePair in headerMaps)
            {
                request.AddHeader(keyValuePair.Key, keyValuePair.Value);
            }
            try
            {
                var response = client.Execute(request);
                int statusCode = (int)response.StatusCode;
                if (statusCode >= 200 && statusCode < 300)
                {
                    return !string.IsNullOrEmpty(response.Content)
                        ? response.Content.Equals("{}") ? response.StatusCode.ToString() : response.Content
                        : response.StatusCode.ToString();
                }
                else
                {
                    if (!string.IsNullOrEmpty(response.Content))
                    {
                        throw new Exception(((int)response.StatusCode).ToString() + "\n" + response.Content);
                    }
                    else
                    {
                        throw new Exception(((int)response.StatusCode).ToString() + "\n" + response.ErrorMessage);
                    }
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>
        /// get
        /// </summary>
        /// <param name="requestContext"></param>
        /// <param name="baseUrl"></param>
        /// <param name="resource"></param>
        /// <param name="headerMaps"></param>
        /// <param name="req"></param>
        /// <returns></returns>
        public String Execute(RequestContext requestContext, string baseUrl, string resource, IDictionary<string, string> headerMaps, object req)
        {
            /*            
            HttpWebRequestFactory aa = new HttpWebRequestFactory();

            var kk = aa.CreateHttpRequest(new Uri(baseUrl + resource));
            kk.SetRequestHeaders(headerMaps);
            kk.ConfigureRequest(requestContext);
            var aaa = kk.GetResponse();
            HttpStatusCode status = aaa.StatusCode;

            var k2k = kk.GetRequestContent();
            */


            var client = new RestSharp.RestClient(baseUrl);
            var request = new RestRequest(Method.GET) { Resource = resource.Trim('/') };

            var tmpJson = JsonConvert.SerializeObject(req);
            var getParamsObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(tmpJson);

            foreach (KeyValuePair<string, string> keyValuePair in headerMaps)
            {
                request.AddHeader(keyValuePair.Key, keyValuePair.Value);
            }

            if (null != getParamsObj)
            {
                foreach (var o in getParamsObj)
                {
                    request.AddParameter(o.Key, o.Value.ToString());
                }
            }

            try
            {
                var response = client.Execute(request);
                int statusCode = (int)response.StatusCode;
                if (statusCode >= 200 && statusCode < 300)
                {
                    return !string.IsNullOrEmpty(response.Content)
                        ? response.Content.Equals("{}") ? response.StatusCode.ToString() : response.Content
                        : response.StatusCode.ToString();
                }
                else
                {
                    if (!string.IsNullOrEmpty(response.Content))
                    {
                        throw new Exception(statusCode.ToString() + "\n" + response.Content);
                    }
                    else
                    {
                        throw new Exception(statusCode.ToString() + "\n" + response.ErrorMessage);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true; //总是接受
        }
    }
}
