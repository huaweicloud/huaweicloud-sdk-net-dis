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

using eSDK_OBS_API.OBS.Util;
using OBS.Runtime.Internal;
using OBS.Runtime.Internal.Transform;
using System.Reflection;

namespace OBS.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// Delete Bucket Tagging Request Marshaller
    /// </summary>       
    public class GetBucketTaggingRequestMarshaller : IMarshaller<IRequest, GetBucketTaggingRequest> ,IMarshaller<IRequest,OBS.Runtime.ObsWebServiceRequest>
	{
		public IRequest Marshall(OBS.Runtime.ObsWebServiceRequest input)
		{
			return this.Marshall((GetBucketTaggingRequest)input);
		}

        public IRequest Marshall(GetBucketTaggingRequest getBucketTaggingRequest)
        {
            IRequest request = new DefaultRequest(getBucketTaggingRequest, "ObsS3");

            request.HttpMethod = "GET";
            //2015-5-27 w00322557
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("GetBucketTaggingRequestMarshaller HttpMethod: {0}", request.HttpMethod));

            request.ResourcePath = string.Concat("/", S3Transforms.ToStringValue(getBucketTaggingRequest.BucketName));
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct, string.Format("GetBucketTaggingRequestMarshaller ResourcePath: {0}", request.ResourcePath));

            request.AddSubResource("tagging");
            request.UseQueryString = true;
            
            return request;
        }
    }
}
    
