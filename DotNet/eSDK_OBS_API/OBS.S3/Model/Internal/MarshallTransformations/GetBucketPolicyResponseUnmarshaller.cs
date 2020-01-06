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
using OBS.S3.Util;

namespace OBS.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    ///    Response Unmarshaller for GetBucketPolicy operation
    /// </summary>
    public class GetBucketPolicyResponseUnmarshaller : S3ReponseUnmarshaller
    {
        public override ObsWebServiceResponse Unmarshall(XmlUnmarshallerContext context) 
        {   
            GetBucketPolicyResponse response = new GetBucketPolicyResponse();
                        
            UnmarshallResult(context,response);                        

            return response;
        }
        
        private static void UnmarshallResult(XmlUnmarshallerContext context,GetBucketPolicyResponse response)
        {
            using (System.IO.StreamReader reader = new System.IO.StreamReader(context.Stream))
            {
                response.Policy = reader.ReadToEnd();
                LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("GetBucketPolicyResponseUnmarshaller response policy: {0}", response.Policy));

                if (response.Policy.StartsWith("<?xml", StringComparison.OrdinalIgnoreCase))
                {
                 
                }                
            }

            return;
        }
        
        public override ObsServiceException UnmarshallException(XmlUnmarshallerContext context, Exception innerException, HttpStatusCode statusCode)
        {
            S3ErrorResponse errorResponse = S3ErrorResponseUnmarshaller.Instance.Unmarshall(context);
            //2015-5-27 w00322557
            LoggerMgr.Log_Run_Error(OBS.S3.ObsClient.strProduct,string.Format("GetBucketPolicyResponseUnmarshaller HttpStatus: {0}", statusCode.ToString()));
            LoggerMgr.Log_Run_Error(OBS.S3.ObsClient.strProduct,string.Format("GetBucketPolicyResponseUnmarshaller exception: {0}", innerException.Message));

            return new ObsS3Exception(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode, errorResponse.Id2);
        }

        private static GetBucketPolicyResponseUnmarshaller _instance;

        public static GetBucketPolicyResponseUnmarshaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GetBucketPolicyResponseUnmarshaller();
                }
                return _instance;
            }
        }
    
    }
}
    
