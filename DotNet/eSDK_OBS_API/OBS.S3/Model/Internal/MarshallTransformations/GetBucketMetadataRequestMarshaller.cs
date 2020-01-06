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

using OBS.Runtime.Internal;
using OBS.Runtime.Internal.Transform;
using System.Globalization;
using OBS.S3.Util;
using OBS.Util;
using System.Reflection;
using eSDK_OBS_API.OBS.Util;

namespace OBS.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// GetObjectMetadata Marshaller
    /// </summary>       
    public class GetBucketMetadataRequestMarshaller : IMarshaller<IRequest, GetBucketMetadataRequest> ,IMarshaller<IRequest,OBS.Runtime.ObsWebServiceRequest>
	{
		public IRequest Marshall(OBS.Runtime.ObsWebServiceRequest input)
		{
            return this.Marshall((GetBucketMetadataRequest)input);
		}

        public IRequest Marshall(GetBucketMetadataRequest getBucketMetadataRequest)
        {
            IRequest request = new DefaultRequest(getBucketMetadataRequest, "ObsS3");

            request.HttpMethod = "HEAD";
            //2015-5-27 w00322557
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("GetBucketMetadataRequestMarshaller HttpMethod: {0}", request.HttpMethod));

            request.ResourcePath = string.Concat("/", S3Transforms.ToStringValue(getBucketMetadataRequest.BucketName));
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct, string.Format("GetBucketMetadata RequestMarshaller ResourcePath: {0}", request.ResourcePath));

            request.UseQueryString = true;

            //Origin
            if (getBucketMetadataRequest.IsSetOrigin())
                request.Headers.Add(HeaderKeys.Origin, getBucketMetadataRequest.Origin);
            //Access-Control-Request-Headers
            if (getBucketMetadataRequest.IsSetAccessControlRequestHeaders())
            {
                foreach (string header in getBucketMetadataRequest.AccessControlRequestHeaders)
                {
                    if (!request.Headers.ContainsKey(HeaderKeys.AccessControlRequestHeaders))
                    {
                        request.Headers.Add(HeaderKeys.AccessControlRequestHeaders, header);
                    }
                    else
                    {
                        request.Headers[HeaderKeys.AccessControlRequestHeaders] = header;
                    }
                }
            }
            
            
            return request;
        }
    }
}
    
