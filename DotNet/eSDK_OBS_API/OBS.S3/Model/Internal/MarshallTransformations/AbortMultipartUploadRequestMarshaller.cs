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

using OBS.Runtime.Internal;
using OBS.Runtime.Internal.Transform;
using System.Globalization;

namespace OBS.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// Abort Multipart Upload Request Marshaller
    /// </summary>       
    public class AbortMultipartUploadRequestMarshaller : IMarshaller<IRequest, AbortMultipartUploadRequest> ,IMarshaller<IRequest,OBS.Runtime.ObsWebServiceRequest>
	{
		public IRequest Marshall(OBS.Runtime.ObsWebServiceRequest input)
		{
			return this.Marshall((AbortMultipartUploadRequest)input);
		}

        public IRequest Marshall(AbortMultipartUploadRequest abortMultipartUploadRequest)
        {
            IRequest request = new DefaultRequest(abortMultipartUploadRequest, "ObsS3");

            request.HttpMethod = "DELETE";
            //2015-5-27 w00322557
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("AbortMultipartUploadRequestMarshaller HttpMethod: {0}", request.HttpMethod));

            request.ResourcePath = string.Format(CultureInfo.InvariantCulture, "/{0}/{1}", 
                                                 S3Transforms.ToStringValue(abortMultipartUploadRequest.BucketName),
                                                 S3Transforms.ToStringValue(abortMultipartUploadRequest.Key));
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("AbortMultipartUploadRequestMarshaller ResourcePath: {0}", request.ResourcePath));

            request.AddSubResource("uploadId", S3Transforms.ToStringValue(abortMultipartUploadRequest.UploadId));
            request.UseQueryString = true;
            
            return request;
        }
    }
}
    
