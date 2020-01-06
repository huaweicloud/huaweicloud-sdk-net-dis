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
using System.Reflection;

namespace OBS.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// List Objects Request Marshaller
    /// </summary>       
    public class ListObjectsRequestMarshaller : IMarshaller<IRequest, ListObjectsRequest> ,IMarshaller<IRequest,OBS.Runtime.ObsWebServiceRequest>
	{
		public IRequest Marshall(OBS.Runtime.ObsWebServiceRequest input)
		{
			return this.Marshall((ListObjectsRequest)input);
		}

        public IRequest Marshall(ListObjectsRequest listObjectsRequest)
        {
            IRequest request = new DefaultRequest(listObjectsRequest, "ObsS3");

            request.HttpMethod = "GET";
            //2015-5-26 w00322557
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("ListObjectsRequestMarshaller HttpMethod: {0}", request.HttpMethod));

            request.ResourcePath = string.Concat("/", S3Transforms.ToStringValue(listObjectsRequest.BucketName));
            //2015-5-26 w00322557
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("ListObjectsRequestMarshaller ResourcePath: {0}", request.ResourcePath));

            if (listObjectsRequest.IsSetDelimiter())
                request.Parameters.Add("delimiter", S3Transforms.ToStringValue(listObjectsRequest.Delimiter));
            if (listObjectsRequest.IsSetMarker())
                request.Parameters.Add("marker", S3Transforms.ToStringValue(listObjectsRequest.Marker));
            if (listObjectsRequest.IsSetMaxKeys())
                request.Parameters.Add("max-keys", S3Transforms.ToStringValue(listObjectsRequest.MaxKeys));
            if (listObjectsRequest.IsSetPrefix())
                request.Parameters.Add("prefix", S3Transforms.ToStringValue(listObjectsRequest.Prefix));
            if (listObjectsRequest.IsSetEncoding())
                request.Parameters.Add("encoding-type", S3Transforms.ToStringValue(listObjectsRequest.Encoding));

            foreach(var param in request.Parameters)
            {
                LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("ListObjectsRequestMarshaller Parameter key: {0}, value: {1}", param.Key,param.Value));
            }

            request.UseQueryString = true;
            
            return request;
        }
    }
}
    
