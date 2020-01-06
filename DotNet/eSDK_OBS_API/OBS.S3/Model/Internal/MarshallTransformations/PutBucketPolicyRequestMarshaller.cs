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

using System.IO;
using System.Text;
using OBS.Runtime.Internal;
using OBS.Runtime.Internal.Transform;
using OBS.Util;
using System.Reflection;

namespace OBS.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// Put Bucket Policy Request Marshaller
    /// </summary>       
    public class PutBucketPolicyRequestMarshaller : IMarshaller<IRequest, PutBucketPolicyRequest> ,IMarshaller<IRequest,OBS.Runtime.ObsWebServiceRequest>
	{
        

		public IRequest Marshall(OBS.Runtime.ObsWebServiceRequest input)
		{
            return this.Marshall((PutBucketPolicyRequest)input);
		}

        public IRequest Marshall(PutBucketPolicyRequest putBucketPolicyRequest)
        {
            IRequest request = new DefaultRequest(putBucketPolicyRequest, "ObsS3");

            request.HttpMethod = "PUT";
            //2015-5-27 w00322557
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("PutBucketPolicyRequestMarshaller HttpMethod: {0}", request.HttpMethod));

            if (putBucketPolicyRequest.IsSetContentMD5())
                request.Headers.Add(HeaderKeys.ContentMD5Header, S3Transforms.ToStringValue(putBucketPolicyRequest.ContentMD5));
            if (!request.Headers.ContainsKey(HeaderKeys.ContentTypeHeader))
                request.Headers.Add(HeaderKeys.ContentTypeHeader, "text/plain");

            request.ResourcePath = string.Concat("/", S3Transforms.ToStringValue(putBucketPolicyRequest.BucketName));
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("PutBucketPolicyRequestMarshaller ResourcePath: {0}", request.ResourcePath));

            request.AddSubResource("policy");

            if (string.IsNullOrEmpty(putBucketPolicyRequest.Policy) || string.IsNullOrEmpty(putBucketPolicyRequest.Policy))
            {
                putBucketPolicyRequest.Policy = "<>";
            }
            else
            {
                request.ContentStream = new MemoryStream(Encoding.UTF8.GetBytes(putBucketPolicyRequest.Policy));
            }
            
            

            return request;
        }
    }
}

