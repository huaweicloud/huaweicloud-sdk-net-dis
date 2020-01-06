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
using System.Reflection;

namespace OBS.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    ///    Response Unmarshaller for ListMultipartUploads operation
    /// </summary>
    internal class ListMultipartUploadsResponseUnmarshaller : S3ReponseUnmarshaller
    {
        public override ObsWebServiceResponse Unmarshall(XmlUnmarshallerContext context) 
        {   
            ListMultipartUploadsResponse response = new ListMultipartUploadsResponse();
            
            while (context.Read())
            {
                if (context.IsStartElement)
                {                    
                    UnmarshallResult(context,response);                        
                    continue;
                }
            }
                 
                        
            return response;
        }
        
        private static void UnmarshallResult(XmlUnmarshallerContext context,ListMultipartUploadsResponse response)
        {
            
            int originalDepth = context.CurrentDepth;
            int targetDepth = originalDepth + 1;
            
            if (context.IsStartOfDocument) 
               targetDepth += 2;
            
            while (context.Read())
            {
                if (context.IsStartElement || context.IsAttribute)
                {
                    if (context.TestExpression("Bucket", targetDepth))
                    {
                        response.BucketName = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("ListMultipartUploadsResponseUnmarshaller BucketName: {0}", response.BucketName));
                        continue;
                    }
                    if (context.TestExpression("KeyMarker", targetDepth))
                    {
                        response.KeyMarker = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("ListMultipartUploadsResponseUnmarshaller KeyMarker: {0}", response.KeyMarker));
                        continue;
                    }
                    if (context.TestExpression("UploadIdMarker", targetDepth))
                    {
                        response.UploadIdMarker = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("ListMultipartUploadsResponseUnmarshaller UploadIdMarker: {0}", response.UploadIdMarker));
                        continue;
                    }
                    if (context.TestExpression("NextKeyMarker", targetDepth))
                    {
                        response.NextKeyMarker = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("ListMultipartUploadsResponseUnmarshaller NextKeyMarker: {0}", response.NextKeyMarker));
                        continue;
                    }
                    if (context.TestExpression("NextUploadIdMarker", targetDepth))
                    {
                        response.NextUploadIdMarker = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("ListMultipartUploadsResponseUnmarshaller NextUploadIdMarker: {0}", response.NextUploadIdMarker));
                        continue;
                    }
                    if (context.TestExpression("MaxUploads", targetDepth))
                    {
                        response.MaxUploads = IntUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("ListMultipartUploadsResponseUnmarshaller MaxUploads: {0}", response.MaxUploads));
                        continue;
                    }
                    if (context.TestExpression("IsTruncated", targetDepth))
                    {
                        response.IsTruncated = BoolUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("ListMultipartUploadsResponseUnmarshaller IsTruncated: {0}", response.IsTruncated));
                        continue;
                    }
                    if (context.TestExpression("Upload", targetDepth))
                    {
                        response.MultipartUploads.Add(MultipartUploadUnmarshaller.Instance.Unmarshall(context));
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("ListMultipartUploadsResponseUnmarshaller MultipartUploads count: {0}", response.MultipartUploads.Count));
                        continue;
                    }
                    if (context.TestExpression("Delimiter", targetDepth))
                    {
                        response.Delimiter = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("ListMultipartUploadsResponseUnmarshaller Delimiter: {0}", response.Delimiter));
                        continue;
                    }
                    if (context.TestExpression("Prefix", targetDepth))
                    {
                        response.Prefix = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("ListMultipartUploadsResponseUnmarshaller Prefix: {0}", response.Prefix));
                        continue;
                    }
                    if (context.TestExpression("CommonPrefixes", targetDepth))
                    {
                        var prefix = CommonPrefixesItemUnmarshaller.Instance.Unmarshall(context);

                        if (prefix != null)
                            response.CommonPrefixes.Add(prefix);

                        continue;
                    }
                }
                else if (context.IsEndElement && context.CurrentDepth < originalDepth)
                {
                    return;
                }
            }

            return;
        }
        
        public override ObsServiceException UnmarshallException(XmlUnmarshallerContext context, Exception innerException, HttpStatusCode statusCode)
        {
            S3ErrorResponse errorResponse = S3ErrorResponseUnmarshaller.Instance.Unmarshall(context);
            //2015-5-27 w00322557
            LoggerMgr.Log_Run_Error(OBS.S3.ObsClient.strProduct,string.Format("ListMultipartUploadsResponseUnmarshaller HttpStatus: {0}", statusCode.ToString()));
            LoggerMgr.Log_Run_Error(OBS.S3.ObsClient.strProduct,string.Format("ListMultipartUploadsResponseUnmarshaller exception: {0}", innerException.Message));
            return new ObsS3Exception(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode, errorResponse.Id2);
        }
        
        private static ListMultipartUploadsResponseUnmarshaller _instance;

        public static ListMultipartUploadsResponseUnmarshaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ListMultipartUploadsResponseUnmarshaller();
                }
                return _instance;
            }
        }
    }
}
    
