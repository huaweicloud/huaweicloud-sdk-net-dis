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
using eSDK_OBS_API.OBS.Util;

namespace OBS.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// Delete Bucket Cors Request Marshaller
    /// </summary>       
    public class DeleteCORSConfigurationRequestMarshaller : IMarshaller<IRequest, DeleteCORSConfigurationRequest> ,IMarshaller<IRequest,OBS.Runtime.ObsWebServiceRequest>
	{
		public IRequest Marshall(OBS.Runtime.ObsWebServiceRequest input)
		{
			return this.Marshall((DeleteCORSConfigurationRequest)input);
		}

        public IRequest Marshall(DeleteCORSConfigurationRequest deleteCORSConfigurationRequest)
        {
            IRequest request = new DefaultRequest(deleteCORSConfigurationRequest, "ObsS3");

            request.HttpMethod = "DELETE";
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct, string.Format("DeleteCORSConfigurationRequestMarshaller HttpMethod: {0}", request.HttpMethod));

            request.ResourcePath = string.Concat("/", S3Transforms.ToStringValue(deleteCORSConfigurationRequest.BucketName));
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct, string.Format("DeleteCORSConfigurationRequestMarshaller ResourcePath: {0}", request.ResourcePath));

            request.AddSubResource("cors");
            request.UseQueryString = true;
            
            return request;
        }
    }
}
    
