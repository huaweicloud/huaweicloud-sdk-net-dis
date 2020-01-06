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
using System.Reflection;

namespace OBS.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// List Parts Request Marshaller
    /// </summary>       
    public class ListPartsRequestMarshaller : IMarshaller<IRequest, ListPartsRequest> ,IMarshaller<IRequest,OBS.Runtime.ObsWebServiceRequest>
	{
        

		public IRequest Marshall(OBS.Runtime.ObsWebServiceRequest input)
		{
			return this.Marshall((ListPartsRequest)input);
		}

        public IRequest Marshall(ListPartsRequest listPartsRequest)
        {
            IRequest request = new DefaultRequest(listPartsRequest, "ObsS3");

            request.HttpMethod = "GET";
            //2015-5-27 w00322557
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("ListPartsRequestMarshaller HttpMethod: {0}", request.HttpMethod));

            request.ResourcePath = string.Format(CultureInfo.InvariantCulture, "/{0}/{1}",
                                                 S3Transforms.ToStringValue(listPartsRequest.BucketName),
                                                 S3Transforms.ToStringValue(listPartsRequest.Key));
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("ListPartsRequestMarshaller ResourcePath: {0}", request.ResourcePath));

            if (listPartsRequest.IsSetUploadId())
                request.AddSubResource("uploadId", S3Transforms.ToStringValue(listPartsRequest.UploadId));

            if (listPartsRequest.IsSetMaxParts())
                request.Parameters.Add("max-parts", S3Transforms.ToStringValue(listPartsRequest.MaxParts));
            if (listPartsRequest.IsSetPartNumberMarker())
                request.Parameters.Add("part-number-marker", S3Transforms.ToStringValue(listPartsRequest.PartNumberMarker));
            if (listPartsRequest.IsSetEncoding())
                request.Parameters.Add("encoding-type", S3Transforms.ToStringValue(listPartsRequest.Encoding));

            request.UseQueryString = true;
            
            return request;
        }
    }
}
    
