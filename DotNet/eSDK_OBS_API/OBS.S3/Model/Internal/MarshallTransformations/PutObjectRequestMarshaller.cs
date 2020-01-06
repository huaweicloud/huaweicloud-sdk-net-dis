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
using System;
using System.Globalization;
using System.IO;
using OBS.Runtime.Internal.Auth;
using OBS.S3.Util;
using OBS.Runtime.Internal;
using OBS.Runtime.Internal.Transform;
using OBS.Runtime.Internal.Util;
using OBS.Util;
using OBS.Runtime;
using System.Reflection;

namespace OBS.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// Put Object Request Marshaller
    /// </summary>       
    public class PutObjectRequestMarshaller : IMarshaller<IRequest, PutObjectRequest> ,IMarshaller<IRequest,OBS.Runtime.ObsWebServiceRequest>
	{
        

		public IRequest Marshall(OBS.Runtime.ObsWebServiceRequest input)
		{
			return this.Marshall((PutObjectRequest)input);
		}

        public IRequest Marshall(PutObjectRequest putObjectRequest)
        {
            IRequest request = new DefaultRequest(putObjectRequest, "ObsS3");

            request.HttpMethod = "PUT";
            //2015-5-27 w00322557
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("PutObjectRequestMarshaller HttpMethod: {0}", request.HttpMethod));

            if(putObjectRequest.IsSetCannedACL())
                request.Headers.Add(HeaderKeys.XAmzAclHeader, S3Transforms.ToStringValue(putObjectRequest.CannedACL));

            var headers = putObjectRequest.Headers;
            foreach (var key in headers.Keys)
                request.Headers[key] = headers[key];

            if(putObjectRequest.IsSetMD5Digest())
                request.Headers[HeaderKeys.ContentMD5Header] = putObjectRequest.MD5Digest;

            HeaderACLRequestMarshaller.Marshall(request, putObjectRequest);

            if(putObjectRequest.IsSetStorageClass())
                request.Headers.Add(HeaderKeys.XAmzStorageClassHeader, S3Transforms.ToStringValue(putObjectRequest.StorageClass));
            
            if(putObjectRequest.IsSetWebsiteRedirectLocation())
                request.Headers.Add(HeaderKeys.XAmzWebsiteRedirectLocationHeader, S3Transforms.ToStringValue(putObjectRequest.WebsiteRedirectLocation));

            if (putObjectRequest.IsSetServerSideEncryptionCustomerMethod())
                request.Headers.Add(HeaderKeys.XAmzSSECustomerAlgorithmHeader, putObjectRequest.ServerSideEncryptionCustomerMethod);
            if (putObjectRequest.IsSetServerSideEncryptionCustomerProvidedKey())
            {
                request.Headers.Add(HeaderKeys.XAmzSSECustomerKeyHeader, putObjectRequest.ServerSideEncryptionCustomerProvidedKey);
                if (putObjectRequest.IsSetServerSideEncryptionCustomerProvidedKeyMD5())
                    request.Headers.Add(HeaderKeys.XAmzSSECustomerKeyMD5Header, putObjectRequest.ServerSideEncryptionCustomerProvidedKeyMD5);
                else
                    request.Headers.Add(HeaderKeys.XAmzSSECustomerKeyMD5Header, ObsS3Util.ComputeEncodedMD5FromEncodedString(putObjectRequest.ServerSideEncryptionCustomerProvidedKey));
            }

            if (putObjectRequest.IsSetServerSideEncryptionMethod())
                request.Headers.Add(HeaderKeys.XAmzServerSideEncryptionHeader, S3Transforms.ToStringValue(putObjectRequest.ServerSideEncryptionMethod));
            
            if (putObjectRequest.IsSetServerSideEncryptionKeyManagementServiceKeyId())
                request.Headers.Add(HeaderKeys.XAmzServerSideEncryptionAwsKmsKeyIdHeader, putObjectRequest.ServerSideEncryptionKeyManagementServiceKeyId);
            
            if (putObjectRequest.IsSetServerSideEncryptionContent())
                request.Headers.Add(HeaderKeys.XAmzServerSideEncryptionContent, putObjectRequest.ServerSideEncryptionContent);

            ObsS3Util.SetMetadataHeaders(request, putObjectRequest.Metadata);

            request.ResourcePath = string.Format(CultureInfo.InvariantCulture, "/{0}/{1}",
                                                 S3Transforms.ToStringValue(putObjectRequest.BucketName),
                                                 S3Transforms.ToStringValue(putObjectRequest.Key));

            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("PutObjectRequestMarshaller ResourcePath: {0}", request.ResourcePath));

            if (putObjectRequest.InputStream != null)
            {
                // Wrap the stream in a stream that has a length
                var streamWithLength = GetStreamWithLength(putObjectRequest.InputStream, putObjectRequest.Headers.ContentLength);
                if (streamWithLength.Length > 0)
                    request.UseChunkEncoding = true;
                var length = streamWithLength.Length - streamWithLength.Position;
                if (!request.Headers.ContainsKey(HeaderKeys.ContentLengthHeader))
                    request.Headers.Add(HeaderKeys.ContentLengthHeader, length.ToString(CultureInfo.InvariantCulture));

                // Wrap input stream in MD5Stream
                var hashStream = new MD5Stream(streamWithLength, null, length);
                putObjectRequest.InputStream = hashStream;
            }
        
            request.ContentStream = putObjectRequest.InputStream;
            if (!request.Headers.ContainsKey(HeaderKeys.ContentTypeHeader))
                request.Headers.Add(HeaderKeys.ContentTypeHeader, "text/plain");
                      
            return request;
        }

        // Returns a stream that has a length.
        // If the stream supports seeking, returns stream.
        // Otherwise, uses hintLength to create a read-only, non-seekable stream of given length
        private static Stream GetStreamWithLength(Stream baseStream, long hintLength)
        {
            Stream result = baseStream;
            bool shouldWrapStream = false;
            long length = -1;
            try
            {
                length = baseStream.Length - baseStream.Position;
            }
            catch (NotSupportedException)
            {
                shouldWrapStream = true;
                length = hintLength;
            }
            if (length < 0)
                throw new ObsS3Exception("Could not determine content length");

            // Wrap input stream if base stream doesn't have a length property
            if (shouldWrapStream)
                result = new PartialReadOnlyWrapperStream(baseStream, length);

            return result;
        }
    }
}

