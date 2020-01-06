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
using eSDK_OBS_API.OBS.Util;

namespace OBS.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    ///    Response Unmarshaller for DeleteBucketTagging operation
    /// </summary>
    internal class GetBucketTaggingResponseUnmarshaller : S3ReponseUnmarshaller
    {

        public override ObsWebServiceResponse Unmarshall(XmlUnmarshallerContext context) 
        {
            GetBucketTaggingResponse response = new GetBucketTaggingResponse();
            while (context.Read())
            {
                if (context.IsStartElement)
                {
                    UnmarshallResult(context, response);
                    continue;
                }
            }

            return response;
        }
        private static void UnmarshallResult(XmlUnmarshallerContext context, GetBucketTaggingResponse response)
        {

            int originalDepth = context.CurrentDepth;
            int targetDepth = originalDepth + 1;

            if (context.IsStartOfDocument)
                targetDepth += 2;

            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct, string.Format("originaDepth={0},targetDepth={1}",originalDepth,targetDepth));

            while (context.Read())
            {
                LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct, string.Format("context={0}",context));
                if (context.IsStartElement || context.IsAttribute)
                {
                    if (response.TagList == null)
                    {
                        response.TagList = new List<Tag>();
                    }    
                    if (context.TestExpression("Tag"))
                        {
                            Tag tag = TagUnmarshaller.Instance.Unmarshall(context);
                            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct, string.Format("UnmarshallResult Tag:{0}", tag.Key));
                            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct, string.Format("UnmarshallResult Tag:{0}", tag.Value));
                            response.TagList.Add(tag);
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
            LoggerMgr.Log_Run_Error(OBS.S3.ObsClient.strProduct, string.Format("GetBucketTaggingResponseUnmarshaller HttpStatus: {0}", statusCode.ToString()));
            LoggerMgr.Log_Run_Error(OBS.S3.ObsClient.strProduct, string.Format("GetBucketTaggingResponseUnmarshaller exception: {0}", innerException.Message));
            return new ObsS3Exception(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode, errorResponse.Id2);
        }

        private static GetBucketTaggingResponseUnmarshaller _instance;

        public static GetBucketTaggingResponseUnmarshaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GetBucketTaggingResponseUnmarshaller();
                }
                return _instance;
            }
        }
    }
}
    
