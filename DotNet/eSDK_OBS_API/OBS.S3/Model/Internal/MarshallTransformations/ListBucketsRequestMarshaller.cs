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

using OBS.Runtime;
using OBS.Runtime.Internal;
using OBS.Runtime.Internal.Transform;
using System.Reflection;

namespace OBS.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// List Buckets Request Marshaller
    /// </summary>       
    public class ListBucketsRequestMarshaller : IMarshaller<IRequest, ListBucketsRequest> ,IMarshaller<IRequest,OBS.Runtime.ObsWebServiceRequest>
	{
		public IRequest Marshall(OBS.Runtime.ObsWebServiceRequest input)
		{
			return this.Marshall((ListBucketsRequest)input);
		}

        public IRequest Marshall(ListBucketsRequest listBucketsRequest)
        {
            IRequest request = new DefaultRequest(listBucketsRequest, "ObsS3");

            request.HttpMethod = "GET";
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("ListBucketsRequestMarshaller HttpMethod: {0}", request.HttpMethod));
            
            request.ResourcePath = "/";
            request.UseQueryString = true;

            //2015-5-26 w00322557
            foreach (var head in request.Headers)
            {
                LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("ListBucketsRequestMarshaller request header key: {0}, value: {1}", head.Key, head.Value));
            }

            return request;
        }
    }
}
    
