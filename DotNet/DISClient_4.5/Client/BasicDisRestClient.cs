using System;
using System.Collections.Generic;
using Com.Bigdata.Dis.Sdk.DISCommon.Config;
using Newtonsoft.Json;
using OBS.Runtime.Internal;
using System.Net.Security;
using Com.Bigdata.Dis.Sdk.DISCommon.Model;
using System.IO;
using System.Net;
using DISClient_4._5.Protobuf;
using Com.Bigdata.Dis.Sdk.DISCommon.Utils;

namespace DISClient_4._5.Client
{
    public class BasicDisRestClient : Com.Bigdata.Dis.Sdk.DISCommon.Client.BasicDisRestClient, IDISRestClient
    {
        private static object _lock = new object();

        protected static volatile new IDISRestClient _idisRestClient;

        protected BasicDisRestClient(DISConfig disConfig) : base(disConfig)
        {
            this._disConfig = disConfig;
        }

        public static new IDISRestClient GetInstance(DISConfig disConfig)
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

        public T Get<T>(RequestContext requestContext, string baseUrl, string resource, IDictionary<string, string> headerMaps, object req, InterfaceType interfaceName = InterfaceType.DISInterfaceNone)
        {
            if (_disConfig.GetBodySerializeType().ToLower().Equals(BodySerializeType.Protobuf.ToString().ToLower())
                && InterfaceType.DISInterfaceGetRecords == interfaceName)
            {
                string kkk = ProtobufExecute(baseUrl, resource, headerMaps, req);

                return (T)Convert.ChangeType(kkk, TypeCode.String);
            }
            else
            {
                string kkk = Execute(requestContext, baseUrl, resource, headerMaps, req);

                return (T)Convert.ChangeType(kkk, TypeCode.String);
            }
        }

        public T Post<T>(string baseUrl, string resource, IDictionary<string, string> headerMaps, object req, InterfaceType interfaceName = InterfaceType.DISInterfaceNone)
        {
            if (_disConfig.GetBodySerializeType().ToLower().Equals(BodySerializeType.Protobuf.ToString().ToLower())
                && InterfaceType.DISInterfacePutRecords == interfaceName)
            {
                string kkk = ProtobufExecutePost(baseUrl, resource, headerMaps, req);

                return (T)Convert.ChangeType(kkk, TypeCode.String);
            }
            else
            {
                string kkk = ExecutePost(baseUrl, resource, headerMaps, req);

                return (T)Convert.ChangeType(kkk, TypeCode.String);
            }
        }

        public String ProtobufExecutePost(string baseUrl, string resource, IDictionary<string, string> headerMaps, object req)
        {           
            string url = baseUrl.TrimEnd('/') + resource;
            HttpWebRequest request;
            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version11;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "POST";
            request.Timeout = 15000;
            request.AllowAutoRedirect = false;
            
            foreach (KeyValuePair<string, string> keyValuePair in headerMaps)
            {
                if (keyValuePair.Key.Equals("Content-Type"))
                {
                    request.ContentType = keyValuePair.Value;
                }
                else if (keyValuePair.Key.Equals("accept"))
                {
                    request.Accept = keyValuePair.Value;
                }
                else if (keyValuePair.Key.Equals("Host"))
                {
                    request.Host = keyValuePair.Value;
                }
                else
                {
                    request.Headers.Add(keyValuePair.Key, keyValuePair.Value);
                }
            }

            WebResponse response = null;
            string responseStr = null;
            HuaweiCloud.DIS.Api.Protobuf.PutRecordsResult result = null;
            int statusCode = 0;
            try
            {
                byte[] content;

                if (req is byte[])
                {
                    content = (byte[])req;
                }
                else if (req is string || req is int)
                {
                    content = Utils.EncodingBytes(req.ToString());
                }
                else
                {
                    string reqJson = JsonConvert.SerializeObject(req);
                    content = Utils.EncodingBytes(reqJson);
                }

                Stream dataStream = request.GetRequestStream();
                dataStream.Write(content, 0, content.Length);
                dataStream.Close();

                response = request.GetResponse();
                statusCode = (int)((HttpWebResponse)response).StatusCode;

                if (statusCode >= 200 && statusCode < 300)
                {
                    if (response != null)
                    {
                        Stream responseStream = response.GetResponseStream();
                        result = HuaweiCloud.DIS.Api.Protobuf.PutRecordsResult.Parser.ParseFrom(responseStream);
                        if (result != null)
                        {
                            responseStr = JsonConvert.SerializeObject(result);
                        }
                        else
                        {
                            logger.Warn(statusCode.ToString() + "\n" + "Protobuf ParseFrom Stream failed.");
                            throw new Exception(statusCode.ToString() + "\n" + "Protobuf ParseFrom Stream failed.");
                        }
                    }
                }
                else
                {
                    Stream responseStream = response.GetResponseStream();
                    StreamReader readStream = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
                    string errInfo = readStream.ReadToEnd();
                    throw new Exception(statusCode.ToString() + "\n" + errInfo);
                }
            }
            catch (Exception e)
            {
                throw new Exception(statusCode.ToString() + "\n" + e.Message);
            }
            finally
            {
                request = null;
                response = null;
            }

            return responseStr;
        }

        public String ProtobufExecute(string baseUrl, string resource, IDictionary<string, string> headerMaps, object req)
        {
            HttpWebRequest request;
            string url = baseUrl.TrimEnd('/') + resource.TrimEnd('/') + "?";

            var tmpJson = JsonConvert.SerializeObject(req);
            var getParamsObj = JsonConvert.DeserializeObject<Dictionary<string, object>>(tmpJson);
            
            if (null != getParamsObj)
            {
                foreach (var o in getParamsObj)
                {
                    url += o.Key + "=" + o.Value;
                }
            }

            //如果是发送HTTPS请求  
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }
            request.Method = "GET";
            request.Timeout = 15000;
            request.AllowAutoRedirect = false;
            foreach (KeyValuePair<string, string> keyValuePair in headerMaps)
            {
                if (keyValuePair.Key.Equals("Content-Type"))
                {
                    request.ContentType = keyValuePair.Value;
                }
                else if (keyValuePair.Key.Equals("accept"))
                {
                    request.Accept = keyValuePair.Value;
                }
                else if (keyValuePair.Key.Equals("Host"))
                {
                    request.Host = keyValuePair.Value;
                }
                else
                {
                    request.Headers.Add(keyValuePair.Key, keyValuePair.Value);
                }
            }

            WebResponse response = null;
            string responseStr = null;
            int statusCode = 0;
            try
            {
                response = request.GetResponse();
                statusCode = (int)((HttpWebResponse)response).StatusCode;
                if (statusCode >= 200 && statusCode < 300)
                {
                    Stream responseStream = response.GetResponseStream();
                    HuaweiCloud.DIS.Api.Protobuf.GetRecordsResult protoResult = HuaweiCloud.DIS.Api.Protobuf.GetRecordsResult.Parser.ParseFrom(responseStream);
                    Com.Bigdata.Dis.Sdk.DISCommon.Model.GetRecordsResult result = ProtobufUtils.ToGetRecordsResult(protoResult);
                    if (result != null)
                    {
                        responseStr = JsonConvert.SerializeObject(result);
                    }
                    else
                    {
                        logger.Warn(statusCode.ToString() + "\n" + "Protobuf ParseFrom Stream failed.");
                        throw new Exception(statusCode.ToString() + "\n" + "Protobuf ParseFrom Stream failed.");
                    }
                }
                else
                {
                    Stream responseStream = response.GetResponseStream();
                    StreamReader readStream = new StreamReader(responseStream, System.Text.Encoding.GetEncoding("utf-8"));
                    string errInfo = readStream.ReadToEnd();
                    throw new Exception(statusCode.ToString() + "\n" + errInfo);
                }
            }
            catch (Exception e)
            {
                throw new Exception(statusCode.ToString() + "\n" + e.Message);
            }
            finally
            {
                request = null;
                response = null;
            }

            return responseStr;
        }
    }
}
