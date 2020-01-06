using eSDK_OBS_API.OBS.Util; 
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
using System.Reflection;

namespace OBS.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    ///    Response Unmarshaller for PutObject operation
    /// </summary>
    public class PutObjectResponseUnmarshaller : S3ReponseUnmarshaller
    {
        

        public override ObsWebServiceResponse Unmarshall(XmlUnmarshallerContext context) 
        {   
            PutObjectResponse response = new PutObjectResponse();
            
            UnmarshallResult(context,response);                        
                 
                        
            return response;
        }
        
        private static void UnmarshallResult(XmlUnmarshallerContext context,PutObjectResponse response)
        {
            IWebResponseData responseData = context.ResponseData;
            if (responseData.IsHeaderPresent("x-amz-expiration"))
                response.Expiration = new Expiration(responseData.GetHeaderValue("x-amz-expiration"));
            if (responseData.IsHeaderPresent("x-amz-server-side-encryption"))
                response.ServerSideEncryptionMethod = S3Transforms.ToString(responseData.GetHeaderValue("x-amz-server-side-encryption"));
            if (responseData.IsHeaderPresent("ETag"))
                response.ETag = S3Transforms.ToString(responseData.GetHeaderValue("ETag"));
            if (responseData.IsHeaderPresent("x-amz-version-id"))
                response.VersionId = S3Transforms.ToString(responseData.GetHeaderValue("x-amz-version-id"));
            if (responseData.IsHeaderPresent(HeaderKeys.XAmzServerSideEncryptionAwsKmsKeyIdHeader))
                response.ServerSideEncryptionKeyManagementServiceKeyId = S3Transforms.ToString(responseData.GetHeaderValue(HeaderKeys.XAmzServerSideEncryptionAwsKmsKeyIdHeader));
            if (responseData.IsHeaderPresent(HeaderKeys.XAmzSSECustomerAlgorithmHeader))
                response.ServerSideEncryptionCustomerMethod = S3Transforms.ToString(responseData.GetHeaderValue(HeaderKeys.XAmzSSECustomerAlgorithmHeader));
            if (responseData.IsHeaderPresent(HeaderKeys.XAmzSSECustomerKeyMD5Header))
                response.ServerSideEncryptionCustomerProvidedKeyMD5 = S3Transforms.ToString(responseData.GetHeaderValue(HeaderKeys.XAmzSSECustomerKeyMD5Header));
                                        
            return;
        }
        
        public override ObsServiceException UnmarshallException(XmlUnmarshallerContext context, Exception innerException, HttpStatusCode statusCode)
        {
            S3ErrorResponse errorResponse = S3ErrorResponseUnmarshaller.Instance.Unmarshall(context);
            //2015-5-27 w00322557
            LoggerMgr.Log_Run_Error(OBS.S3.ObsClient.strProduct,string.Format("PutObjectResponseUnmarshaller HttpStatus: {0}", statusCode.ToString()));
            LoggerMgr.Log_Run_Error(OBS.S3.ObsClient.strProduct,string.Format("PutObjectResponseUnmarshaller exception: {0}", innerException.Message));
            return new ObsS3Exception(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode, errorResponse.Id2);
        }

        private static PutObjectResponseUnmarshaller _instance;

        public static PutObjectResponseUnmarshaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PutObjectResponseUnmarshaller();
                }
                return _instance;
            }
        }
    }
}
    
