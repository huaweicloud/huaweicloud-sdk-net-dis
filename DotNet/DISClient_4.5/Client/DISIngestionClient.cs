using System;
using Com.Bigdata.Dis.Sdk.DISCommon.Auth;
using Com.Bigdata.Dis.Sdk.DISCommon.Config;
using Com.Bigdata.Dis.Sdk.DISCommon.DISWeb;
using Com.Bigdata.Dis.Sdk.DISCommon.Ext;
using Com.Bigdata.Dis.Sdk.DISCommon.Model;
using Newtonsoft.Json;
using OBS.Runtime;
using IRequest = OBS.Runtime.Internal.IRequest;
using Com.Bigdata.Dis.Sdk.DISCommon;
using Com.Bigdata.Dis.Sdk.DISCommon.Client;
using Google.Protobuf;
using DISClient_4._5.Protobuf;
using GetRecordsResult = Com.Bigdata.Dis.Sdk.DISCommon.Model.GetRecordsResult;
using PutRecordsResult = Com.Bigdata.Dis.Sdk.DISCommon.Model.PutRecordsResult;
using PutRecordsRequest = Com.Bigdata.Dis.Sdk.DISCommon.Model.PutRecordsRequest;
using GetRecordsRequest = Com.Bigdata.Dis.Sdk.DISCommon.Model.GetRecordsRequest;

namespace DISClient_4._5.Client
{
    public class DISIngestionClient : Com.Bigdata.Dis.Sdk.DISCommon.Client.DISIngestionClient
    {
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
        /// <summary>
        /// 流式数据上传
        /// </summary>
        /// <param name="putRecordsParam"></param>
        /// <returns></returns>
        public new PutRecordsResult PutRecords(PutRecordsRequest putRecordsParam)
        {
            return InnerPutRecordsSupportingCache(putRecordsParam, new PutRecordMethod(this.InnerPutRecords));
        }

        protected new PutRecordsResult InnerPutRecords(PutRecordsRequest putRecordsParam)
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

            if (_disConfig.GetBodySerializeType().ToLower().Equals(BodySerializeType.Protobuf.ToString().ToLower()))
            {
                requestobs.Headers.Add("Content-Type", "application/x-protobuf; charset=utf-8");
                requestobs.Headers.Add("accept", "*/*");
                HuaweiCloud.DIS.Api.Protobuf.PutRecordsRequest protoRequest = ProtobufUtils.ToProtobufPutRecordsRequest(putRecordsParam);
                HuaweiCloud.DIS.Api.Protobuf.PutRecordsResult protoResult = Request<HuaweiCloud.DIS.Api.Protobuf.PutRecordsResult>(protoRequest.ToByteArray(), requestobs, InterfaceType.DISInterfacePutRecords);
                PutRecordsResult result = ProtobufUtils.ToPutRecordsResult(protoResult);
                return result;
            }
            else
            {
                var results = Request<PutRecordsResult>(putRecordsParam, requestobs);
                return results;
            }
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="getRecordsParam"></param>
        /// <returns></returns>
        public new GetRecordsResult GetRecords(GetRecordsRequest getRecordsParam)
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
            if (_disConfig.GetBodySerializeType().ToLower().Equals(BodySerializeType.Protobuf.ToString().ToLower()))
            {
                requestobs.Headers.Add("Content-Type", "application/x-protobuf; charset=utf-8");
                requestobs.Headers.Add("accept", "*/*");
                result = Request<GetRecordsResult>(getRecordsParam, requestobs, InterfaceType.DISInterfaceGetRecords);
            }
            else
            {
                result = Request<GetRecordsResult>(getRecordsParam, requestobs);
            }

            result = DecorateRecords(result);
            return result;
        }

        #endregion

        #region Request execution methods
        public T Request<T>(object param, OBS.Runtime.Internal.IRequest request, InterfaceType interfaceName = InterfaceType.DISInterfaceNone) where T : new()
        {
            try
            {
                Check();
                var clientWrapper = new RestClientWrapper(request, _disConfig);
                string result = clientWrapper.Request<string>(param, _credentials.GetAccessKeyId(), _credentials.GetSecretKey(), _region, _disConfig.GetProjectId(), interfaceName);
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
