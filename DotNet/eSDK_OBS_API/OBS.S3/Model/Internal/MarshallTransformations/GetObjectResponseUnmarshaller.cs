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

using OBS.S3.Util;
using System.Globalization;
using OBS.Util;
using System.Reflection;

namespace OBS.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    ///    Response Unmarshaller for GetObject operation
    /// </summary>
    public class GetObjectResponseUnmarshaller : S3ReponseUnmarshaller
    {
        public override ObsWebServiceResponse Unmarshall(XmlUnmarshallerContext context) 
        {   
            GetObjectResponse response = new GetObjectResponse();
            
            UnmarshallResult(context,response);                        
                 
                        
            return response;
        }
        
        private static void UnmarshallResult(XmlUnmarshallerContext context,GetObjectResponse response)
        {

            response.ResponseStream = context.Stream;
                            
            IWebResponseData responseData = context.ResponseData;
            if (responseData.IsHeaderPresent("x-amz-delete-marker"))
                response.DeleteMarker = S3Transforms.ToString(responseData.GetHeaderValue("x-amz-delete-marker"));
            if (responseData.IsHeaderPresent("accept-ranges"))
                response.AcceptRanges = S3Transforms.ToString(responseData.GetHeaderValue("accept-ranges"));
            if (responseData.IsHeaderPresent("x-amz-expiration"))
                response.Expiration = new Expiration(responseData.GetHeaderValue("x-amz-expiration"));
            if (responseData.IsHeaderPresent("x-amz-restore"))
            {
                bool restoreInProgress;
                DateTime? restoreExpiration;
                ObsS3Util.ParseAmzRestoreHeader(responseData.GetHeaderValue("x-amz-restore"), out restoreInProgress, out restoreExpiration);

                response.RestoreInProgress = restoreInProgress;
                response.RestoreExpiration = restoreExpiration;
            }
            if (responseData.IsHeaderPresent("Last-Modified"))
                response.LastModified = S3Transforms.ToDateTime(responseData.GetHeaderValue("Last-Modified"));
            if (responseData.IsHeaderPresent("ETag"))
                response.ETag = S3Transforms.ToString(responseData.GetHeaderValue("ETag"));
            if (responseData.IsHeaderPresent("x-amz-missing-meta"))
                response.MissingMeta = S3Transforms.ToInt(responseData.GetHeaderValue("x-amz-missing-meta"));
            if (responseData.IsHeaderPresent("x-amz-version-id"))
                response.VersionId = S3Transforms.ToString(responseData.GetHeaderValue("x-amz-version-id"));
            if (responseData.IsHeaderPresent("Cache-Control"))
                response.Headers.CacheControl = S3Transforms.ToString(responseData.GetHeaderValue("Cache-Control"));
            if (responseData.IsHeaderPresent("Content-Disposition"))
                response.Headers.ContentDisposition = S3Transforms.ToString(responseData.GetHeaderValue("Content-Disposition"));
            if (responseData.IsHeaderPresent("Content-Encoding"))
                response.Headers.ContentEncoding = S3Transforms.ToString(responseData.GetHeaderValue("Content-Encoding"));
            if (responseData.IsHeaderPresent("Content-Length"))
                response.Headers.ContentLength = long.Parse(responseData.GetHeaderValue("Content-Length"), CultureInfo.InvariantCulture);
            if (responseData.IsHeaderPresent("Content-Type"))
                response.Headers.ContentType = S3Transforms.ToString(responseData.GetHeaderValue("Content-Type"));
            if (responseData.IsHeaderPresent("Expires"))
                response.Expires = S3Transforms.ToDateTime(responseData.GetHeaderValue("Expires"));
            if (responseData.IsHeaderPresent("x-amz-website-redirect-location"))
                response.WebsiteRedirectLocation = S3Transforms.ToString(responseData.GetHeaderValue("x-amz-website-redirect-location"));
            if (responseData.IsHeaderPresent("x-amz-server-side-encryption"))
                response.ServerSideEncryptionMethod = S3Transforms.ToString(responseData.GetHeaderValue("x-amz-server-side-encryption"));
            if (responseData.IsHeaderPresent("x-amz-server-side-encryption-customer-algorithm"))
                response.ServerSideEncryptionCustomerMethod = ServerSideEncryptionCustomerMethod.FindValue(responseData.GetHeaderValue("x-amz-server-side-encryption-customer-algorithm"));
            if (responseData.IsHeaderPresent(HeaderKeys.XAmzServerSideEncryptionAwsKmsKeyIdHeader))
                response.ServerSideEncryptionKeyManagementServiceKeyId = S3Transforms.ToString(responseData.GetHeaderValue(HeaderKeys.XAmzServerSideEncryptionAwsKmsKeyIdHeader));

            if (responseData.IsHeaderPresent(HeaderKeys.XAmzSSECustomerKeyMD5Header))
                response.ServerSideEncryptionCustomerProvidedKeyMD5 = S3Transforms.ToString(responseData.GetHeaderValue(HeaderKeys.XAmzSSECustomerKeyMD5Header));
                     
            foreach (var name in responseData.GetHeaderNames())
            {
                if (name.StartsWith("x-amz-meta-", StringComparison.OrdinalIgnoreCase))
                    response.Metadata[name] = responseData.GetHeaderValue(name);
                LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("GetObjectResponseUnmarshaller header name: {0}, value: {1}", name, responseData.GetHeaderValue(name)));
            }

            return;
        }
        
        internal override bool HasStreamingProperty
        {
           get { return true;}
        }
        
        public override ObsServiceException UnmarshallException(XmlUnmarshallerContext context, Exception innerException, HttpStatusCode statusCode)
        {
            S3ErrorResponse errorResponse = S3ErrorResponseUnmarshaller.Instance.Unmarshall(context);
            //2015-5-27 w00322557
            LoggerMgr.Log_Run_Error(OBS.S3.ObsClient.strProduct,string.Format("GetObjectResponseUnmarshaller HttpStatus: {0}", statusCode.ToString()));
            LoggerMgr.Log_Run_Error(OBS.S3.ObsClient.strProduct,string.Format("GetObjectResponseUnmarshaller exception: {0}", innerException.Message));
            return new ObsS3Exception(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode, errorResponse.Id2);
        }
        
        private static GetObjectResponseUnmarshaller _instance;

        public static GetObjectResponseUnmarshaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GetObjectResponseUnmarshaller();
                }
                return _instance;
            }
        }
    }
}
    
