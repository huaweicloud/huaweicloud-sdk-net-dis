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
using System.IO;

namespace OBS.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    ///    Response Unmarshaller for GetBucketWebsite operation
    /// </summary>
    internal class GetBucketWebsiteResponseUnmarshaller : S3ReponseUnmarshaller
    {
        public override ObsWebServiceResponse Unmarshall(XmlUnmarshallerContext context) 
        {   
            GetBucketWebsiteResponse response = new GetBucketWebsiteResponse();

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
                        
        private static void UnmarshallResult(XmlUnmarshallerContext context,GetBucketWebsiteResponse response)
        {   
            int originalDepth = context.CurrentDepth;
            int targetDepth = originalDepth + 1;
            
            if (context.IsStartOfDocument) 
               targetDepth += 2;

            response.WebsiteConfiguration = new WebsiteConfiguration();
            while (context.Read())
            {
                if (context.IsStartElement || context.IsAttribute)
                {
                    if (context.TestExpression("RedirectAllRequestsTo", targetDepth))
                    {
                        response.WebsiteConfiguration.RedirectAllRequestsTo = RoutingRuleRedirectUnmarshaller.Instance.Unmarshall(context);
                        //2015-5-27 w00322557
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("GetBucketWebsiteResponseUnmarshaller RedirectAllRequestsTo."));
                        continue;
                    }
                    if (context.TestExpression("IndexDocument/Suffix", targetDepth))
                    {
                        response.WebsiteConfiguration.IndexDocumentSuffix = StringUnmarshaller.Instance.Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("GetBucketWebsiteResponseUnmarshaller IndexDocument: {0}", response.WebsiteConfiguration.IndexDocumentSuffix));
                        continue;
                    }
                    if (context.TestExpression("ErrorDocument/Key", targetDepth))
                    {
                        response.WebsiteConfiguration.ErrorDocument = StringUnmarshaller.Instance.Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("GetBucketWebsiteResponseUnmarshaller ErrorDocument: {0}", response.WebsiteConfiguration.ErrorDocument));
                        continue;
                    }
                    if (context.TestExpression("RoutingRule", targetDepth + 1))
                    {
                        response.WebsiteConfiguration.RoutingRules.Add(RoutingRuleUnmarshaller.Instance.Unmarshall(context));
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("GetBucketWebsiteResponseUnmarshaller RoutingRule."));
                        continue;
                    }
                    //2015-6-3 w00322557
                    if (context.TestExpression("Code", targetDepth))
                    {
                        ObsClient.errorCode = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("GetBucketWebsiteResponseUnmarshaller Code: {0}", ObsClient.errorCode));
                        continue;
                    }

                    if (context.TestExpression("Message", targetDepth))
                    {
                        ObsClient.errorMessage = StringUnmarshaller.GetInstance().Unmarshall(context);
                        LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("GetBucketWebsiteResponseUnmarshaller Message: {0}", ObsClient.errorMessage));
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
            LoggerMgr.Log_Run_Error(OBS.S3.ObsClient.strProduct,string.Format("GetBucketWebsiteResponseUnmarshaller HttpStatus: {0}", statusCode.ToString()));
            LoggerMgr.Log_Run_Error(OBS.S3.ObsClient.strProduct,string.Format("GetBucketWebsiteResponseUnmarshaller exception: {0}", innerException.Message));
            return new ObsS3Exception(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode, errorResponse.Id2);
        }

        private static GetBucketWebsiteResponseUnmarshaller _instance;

        public static GetBucketWebsiteResponseUnmarshaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GetBucketWebsiteResponseUnmarshaller();
                }
                return _instance;
            }
        }
    
    }
}
    
