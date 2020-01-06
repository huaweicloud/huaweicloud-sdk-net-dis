/*
 * Copyright 2010-2013 OBS.com, Inc. or its affiliates. All Rights Reserved.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 * 
 *  
 * 
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 */
using System;
using System.Net;
using System.Collections.Generic;
using OBS.S3.Model;
using OBS.Runtime;
using OBS.Runtime.Internal;
using OBS.Runtime.Internal.Transform;

using OBS.Util;
using OBS.S3.Util;
using System.Globalization;
using System.Reflection;
using eSDK_OBS_API.OBS.Util;

namespace OBS.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    ///    Response Unmarshaller for GetObjectMetadata operation
    /// </summary>
    internal class GetBucketMetadataResponseUnmarshaller : S3ReponseUnmarshaller
    {
        public override ObsWebServiceResponse Unmarshall(XmlUnmarshallerContext context)
        {
            GetBucketMetadataResponse response = new GetBucketMetadataResponse();

            UnmarshallResult(context, response);


            return response;
        }

        private static void UnmarshallResult(XmlUnmarshallerContext context, GetBucketMetadataResponse response)
        {
            System.IO.StreamReader reader = new System.IO.StreamReader(context.Stream);
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct, string.Format("GetBucketMetadataResponseUnmarshaller response xml context: {0}", reader.ReadToEnd()));

            IWebResponseData responseData = context.ResponseData;            
            foreach (var name in responseData.GetHeaderNames())
            {
                response.Headers.Add(name, responseData.GetHeaderValue(name));

                LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct, string.Format("GetBucketMetadataResponseUnmarshaller name: {0}, value: {1}", name, responseData.GetHeaderValue(name)));
            }
            return;
        }

        public override ObsServiceException UnmarshallException(XmlUnmarshallerContext context, Exception innerException, HttpStatusCode statusCode)
        {
            S3ErrorResponse errorResponse = S3ErrorResponseUnmarshaller.Instance.Unmarshall(context);
            //2015-5-27 w00322557
            LoggerMgr.Log_Run_Error(OBS.S3.ObsClient.strProduct, string.Format("GetBucketMetadataResponseUnmarshaller HttpStatus: {0}", statusCode.ToString()));
            LoggerMgr.Log_Run_Error(OBS.S3.ObsClient.strProduct, string.Format("GetBucketMetadataResponseUnmarshaller exception: {0}", innerException.Message));
            return new ObsS3Exception(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode, errorResponse.Id2);
        }

        private static GetBucketMetadataResponseUnmarshaller _instance;

        public static GetBucketMetadataResponseUnmarshaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GetBucketMetadataResponseUnmarshaller();
                }
                return _instance;
            }
        }
    }
}

