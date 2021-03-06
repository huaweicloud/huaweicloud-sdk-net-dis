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
    ///    Response Unmarshaller for PutBucketLogging operation
    /// </summary>
    internal class PutBucketLoggingResponseUnmarshaller : S3ReponseUnmarshaller
    {
        

        public override ObsWebServiceResponse Unmarshall(XmlUnmarshallerContext context) 
        {
            PutBucketLoggingResponse response = new PutBucketLoggingResponse();

            return response;
        }
        
        
        public override ObsServiceException UnmarshallException(XmlUnmarshallerContext context, Exception innerException, HttpStatusCode statusCode)
        {
            S3ErrorResponse errorResponse = S3ErrorResponseUnmarshaller.Instance.Unmarshall(context);
            //2015-5-27 w00322557
            LoggerMgr.Log_Run_Error(OBS.S3.ObsClient.strProduct,string.Format("PutBucketLoggingResponseUnmarshaller HttpStatus: {0}", statusCode.ToString()));
            LoggerMgr.Log_Run_Error(OBS.S3.ObsClient.strProduct,string.Format("PutBucketLoggingResponseUnmarshaller exception: {0}", innerException.Message));
            return new ObsS3Exception(errorResponse.Message, innerException, errorResponse.Type, errorResponse.Code, errorResponse.RequestId, statusCode, errorResponse.Id2);
        }
        
        private static PutBucketLoggingResponseUnmarshaller _instance;

        public static PutBucketLoggingResponseUnmarshaller Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PutBucketLoggingResponseUnmarshaller();
                }
                return _instance;
            }
        }

    
    }
}
    
