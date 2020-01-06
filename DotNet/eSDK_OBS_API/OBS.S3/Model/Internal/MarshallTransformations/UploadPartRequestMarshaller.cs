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

using System.Globalization;
using System.IO;
using OBS.Runtime.Internal;
using OBS.Runtime.Internal.Auth;
using OBS.Runtime.Internal.Transform;
using OBS.Runtime.Internal.Util;
using OBS.S3.Util;
using OBS.Util;
using System.Reflection;

namespace OBS.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// Upload Part Request Marshaller
    /// </summary>       
    public class UploadPartRequestMarshaller : IMarshaller<IRequest, UploadPartRequest> ,IMarshaller<IRequest,OBS.Runtime.ObsWebServiceRequest>
	{
        

		public IRequest Marshall(OBS.Runtime.ObsWebServiceRequest input)
		{
			return this.Marshall((UploadPartRequest)input);
		}

        public IRequest Marshall(UploadPartRequest uploadPartRequest)
        {
            IRequest request = new DefaultRequest(uploadPartRequest, "ObsS3");

            request.HttpMethod = "PUT";
            //2015-5-27 w00322557
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("UploadPartRequestMarshaller HttpMethod: {0}", request.HttpMethod));

            if (uploadPartRequest.IsSetMD5Digest())
                request.Headers[HeaderKeys.ContentMD5Header] = uploadPartRequest.MD5Digest;

            if (uploadPartRequest.IsSetServerSideEncryptionCustomerMethod())
                request.Headers.Add(HeaderKeys.XAmzSSECustomerAlgorithmHeader, uploadPartRequest.ServerSideEncryptionCustomerMethod);
            if (uploadPartRequest.IsSetServerSideEncryptionCustomerProvidedKey())
            {
                request.Headers.Add(HeaderKeys.XAmzSSECustomerKeyHeader, uploadPartRequest.ServerSideEncryptionCustomerProvidedKey);
                if (uploadPartRequest.IsSetServerSideEncryptionCustomerProvidedKeyMD5())
                    request.Headers.Add(HeaderKeys.XAmzSSECustomerKeyMD5Header, uploadPartRequest.ServerSideEncryptionCustomerProvidedKeyMD5);
                else
                    request.Headers.Add(HeaderKeys.XAmzSSECustomerKeyMD5Header, ObsS3Util.ComputeEncodedMD5FromEncodedString(uploadPartRequest.ServerSideEncryptionCustomerProvidedKey));
            }

            request.ResourcePath = string.Format(CultureInfo.InvariantCulture, "/{0}/{1}",
                                                 S3Transforms.ToStringValue(uploadPartRequest.BucketName),
                                                 S3Transforms.ToStringValue(uploadPartRequest.Key));
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("UploadPartRequestMarshaller ResourcePath: {0}", request.ResourcePath));

            if (uploadPartRequest.IsSetPartNumber())
                request.AddSubResource("partNumber", S3Transforms.ToStringValue(uploadPartRequest.PartNumber));
            if (uploadPartRequest.IsSetUploadId())
                request.AddSubResource("uploadId", S3Transforms.ToStringValue(uploadPartRequest.UploadId));

            if (uploadPartRequest.InputStream != null)
            {
                // Wrap input stream in partial wrapper (to upload only part of the stream)
                var partialStream = new PartialWrapperStream(uploadPartRequest.InputStream, uploadPartRequest.PartSize);
                if (partialStream.Length > 0)
                    request.UseChunkEncoding = true;
                if (!request.Headers.ContainsKey(HeaderKeys.ContentLengthHeader))
                    request.Headers.Add(HeaderKeys.ContentLengthHeader, partialStream.Length.ToString(CultureInfo.InvariantCulture));

                // Wrap input stream in MD5Stream; after this we can no longer seek or position the stream
                var hashStream = new MD5Stream(partialStream, null, partialStream.Length);
                uploadPartRequest.InputStream = hashStream;
            }

            request.ContentStream = uploadPartRequest.InputStream;

            if (!request.Headers.ContainsKey(HeaderKeys.ContentTypeHeader))
                request.Headers.Add(HeaderKeys.ContentTypeHeader, "text/plain");

            return request;
        }
    }
}

