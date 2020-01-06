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
    /// Create Multipart Upload Request Marshaller
    /// </summary>       
    public class InitiateMultipartUploadRequestMarshaller : IMarshaller<IRequest, InitiateMultipartUploadRequest> ,IMarshaller<IRequest,OBS.Runtime.ObsWebServiceRequest>
	{
		public IRequest Marshall(OBS.Runtime.ObsWebServiceRequest input)
		{
			return this.Marshall((InitiateMultipartUploadRequest)input);
		}

        public IRequest Marshall(InitiateMultipartUploadRequest initiateMultipartUploadRequest)
        {
            IRequest request = new DefaultRequest(initiateMultipartUploadRequest, "ObsS3");

            request.HttpMethod = "POST";
            //2015-5-27 w00322557
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("InitiateMultipartUploadRequestMarshaller HttpMethod: {0}", request.HttpMethod));

            if (initiateMultipartUploadRequest.IsSetCannedACL())
                request.Headers.Add(HeaderKeys.XAmzAclHeader, S3Transforms.ToStringValue(initiateMultipartUploadRequest.CannedACL));

            var headers = initiateMultipartUploadRequest.Headers;
            foreach (var key in headers.Keys)
                request.Headers.Add(key, headers[key]);

            HeaderACLRequestMarshaller.Marshall(request, initiateMultipartUploadRequest);

            if (initiateMultipartUploadRequest.IsSetServerSideEncryptionMethod())
                request.Headers.Add(HeaderKeys.XAmzServerSideEncryptionHeader, S3Transforms.ToStringValue(initiateMultipartUploadRequest.ServerSideEncryptionMethod));
            if (initiateMultipartUploadRequest.IsSetServerSideEncryptionCustomerMethod())
                request.Headers.Add(HeaderKeys.XAmzSSECustomerAlgorithmHeader, initiateMultipartUploadRequest.ServerSideEncryptionCustomerMethod);
            if (initiateMultipartUploadRequest.IsSetServerSideEncryptionCustomerProvidedKey())
            {
                request.Headers.Add(HeaderKeys.XAmzSSECustomerKeyHeader, initiateMultipartUploadRequest.ServerSideEncryptionCustomerProvidedKey);
                if (initiateMultipartUploadRequest.IsSetServerSideEncryptionCustomerProvidedKeyMD5())
                    request.Headers.Add(HeaderKeys.XAmzSSECustomerKeyMD5Header, initiateMultipartUploadRequest.ServerSideEncryptionCustomerProvidedKeyMD5);
                else
                    request.Headers.Add(HeaderKeys.XAmzSSECustomerKeyMD5Header, ObsS3Util.ComputeEncodedMD5FromEncodedString(initiateMultipartUploadRequest.ServerSideEncryptionCustomerProvidedKey));
            }

            if (initiateMultipartUploadRequest.IsSetServerSideEncryptionKeyManagementServiceKeyId())
                request.Headers.Add(HeaderKeys.XAmzServerSideEncryptionAwsKmsKeyIdHeader, initiateMultipartUploadRequest.ServerSideEncryptionKeyManagementServiceKeyId);

            if (initiateMultipartUploadRequest.IsSetServerSideEncryptionContent())
                request.Headers.Add(HeaderKeys.XAmzServerSideEncryptionContent, initiateMultipartUploadRequest.ServerSideEncryptionContent);

            if (initiateMultipartUploadRequest.IsSetStorageClass())
                request.Headers.Add(HeaderKeys.XAmzStorageClassHeader, S3Transforms.ToStringValue(initiateMultipartUploadRequest.StorageClass));

            if (initiateMultipartUploadRequest.IsSetWebsiteRedirectLocation())
                request.Headers.Add(HeaderKeys.XAmzWebsiteRedirectLocationHeader, S3Transforms.ToStringValue(initiateMultipartUploadRequest.WebsiteRedirectLocation));

            ObsS3Util.SetMetadataHeaders(request, initiateMultipartUploadRequest.Metadata);
            //2015-5-26 w00322557
            foreach (var head in request.Headers)
            {
                LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("InitiateMultipartUploadRequestMarshaller request header key: {0}, value: {1}", head.Key, head.Value));
            }

            request.ResourcePath = string.Format(CultureInfo.InvariantCulture, "/{0}/{1}",
                                                 S3Transforms.ToStringValue(initiateMultipartUploadRequest.BucketName),
                                                 S3Transforms.ToStringValue(initiateMultipartUploadRequest.Key));
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("InitiateMultipartUploadRequestMarshaller ResourcePath: {0}", request.ResourcePath));

            request.AddSubResource("uploads");

            request.UseQueryString = true;

            return request;
        }
    }
}

