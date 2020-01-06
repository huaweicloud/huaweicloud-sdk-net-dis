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

using OBS.S3.Util;
using OBS.Runtime.Internal;
using OBS.Runtime.Internal.Transform;
using System.Globalization;
using OBS.Util;
using System.Reflection;

namespace OBS.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// Get Object Request Marshaller
    /// </summary>       
    public class GetObjectRequestMarshaller : IMarshaller<IRequest, GetObjectRequest> ,IMarshaller<IRequest,OBS.Runtime.ObsWebServiceRequest>
	{
 		public IRequest Marshall(OBS.Runtime.ObsWebServiceRequest input)
		{
			return this.Marshall((GetObjectRequest)input);
		}

        public IRequest Marshall(GetObjectRequest getObjectRequest)
        {
            IRequest request = new DefaultRequest(getObjectRequest, "ObsS3");

            request.HttpMethod = "GET";
            //2015-5-27 w00322557
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("GetObjectRequestMarshaller HttpMethod: {0}", request.HttpMethod));

            if (getObjectRequest.IsSetEtagToMatch())
                request.Headers.Add(HeaderKeys.IfMatchHeader, S3Transforms.ToStringValue(getObjectRequest.EtagToMatch));

            if (getObjectRequest.IsSetModifiedSinceDate())
                request.Headers.Add(HeaderKeys.IfModifiedSinceHeader, S3Transforms.ToStringValue(getObjectRequest.ModifiedSinceDate));

            if (getObjectRequest.IsSetEtagToNotMatch())
                request.Headers.Add(HeaderKeys.IfNoneMatchHeader, S3Transforms.ToStringValue(getObjectRequest.EtagToNotMatch));
            
            if(getObjectRequest.IsSetUnmodifiedSinceDate())
                request.Headers.Add(HeaderKeys.IfUnmodifiedSinceHeader, S3Transforms.ToStringValue(getObjectRequest.UnmodifiedSinceDate));
            
            if(getObjectRequest.IsSetByteRange())
                request.Headers.Add(HeaderKeys.RangeHeader, getObjectRequest.ByteRange.FormattedByteRange);

            if (getObjectRequest.IsSetServerSideEncryptionCustomerMethod())
                request.Headers.Add(HeaderKeys.XAmzSSECustomerAlgorithmHeader, getObjectRequest.ServerSideEncryptionCustomerMethod);
            if (getObjectRequest.IsSetServerSideEncryptionCustomerProvidedKey())
            {
                request.Headers.Add(HeaderKeys.XAmzSSECustomerKeyHeader, getObjectRequest.ServerSideEncryptionCustomerProvidedKey);
                if (getObjectRequest.IsSetServerSideEncryptionCustomerProvidedKeyMD5())
                    request.Headers.Add(HeaderKeys.XAmzSSECustomerKeyMD5Header, getObjectRequest.ServerSideEncryptionCustomerProvidedKeyMD5);
                else
                    request.Headers.Add(HeaderKeys.XAmzSSECustomerKeyMD5Header, ObsS3Util.ComputeEncodedMD5FromEncodedString(getObjectRequest.ServerSideEncryptionCustomerProvidedKey));
            }

            request.ResourcePath = string.Format(CultureInfo.InvariantCulture, "/{0}/{1}",
                                                 S3Transforms.ToStringValue(getObjectRequest.BucketName),
                                                 S3Transforms.ToStringValue(getObjectRequest.Key));
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("GetObjectRequestMarshaller ResourcePath: {0}", request.ResourcePath));

            var headerOverrides = getObjectRequest.ResponseHeaderOverrides;
            if (headerOverrides.CacheControl != null)
                request.Parameters.Add("response-cache-control", S3Transforms.ToStringValue(headerOverrides.CacheControl));
            if (headerOverrides.ContentDisposition != null)
                request.Parameters.Add("response-content-disposition", S3Transforms.ToStringValue(headerOverrides.ContentDisposition));
            if (headerOverrides.ContentEncoding != null)
                request.Parameters.Add("response-content-encoding", S3Transforms.ToStringValue(headerOverrides.ContentEncoding));
            if (headerOverrides.ContentLanguage != null)
                request.Parameters.Add("response-content-language", S3Transforms.ToStringValue(headerOverrides.ContentLanguage));
            if (headerOverrides.ContentType != null)
                request.Parameters.Add("response-content-type", S3Transforms.ToStringValue(headerOverrides.ContentType));
            if (getObjectRequest.IsSetResponseExpires())
                request.Parameters.Add("response-expires", S3Transforms.ToStringValue(getObjectRequest.ResponseExpires));
            if (getObjectRequest.IsSetVersionId())
                request.AddSubResource("versionId", S3Transforms.ToStringValue(getObjectRequest.VersionId));

            request.UseQueryString = true;

            return request;
        }
    }
}
    
