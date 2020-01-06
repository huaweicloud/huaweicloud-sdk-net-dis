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
using OBS.S3.Util;
using OBS.Runtime.Internal;
using OBS.Runtime.Internal.Transform;
using OBS.Util;
using System.Globalization;
using System.Reflection;

namespace OBS.S3.Model.Internal.MarshallTransformations
{
    /// <summary>
    /// Upload Part Copy Request Marshaller
    /// </summary>       
    public class CopyPartRequestMarshaller : IMarshaller<IRequest, CopyPartRequest> ,IMarshaller<IRequest,OBS.Runtime.ObsWebServiceRequest>
	{
		public IRequest Marshall(OBS.Runtime.ObsWebServiceRequest input)
		{
			return this.Marshall((CopyPartRequest)input);
		}

        public IRequest Marshall(CopyPartRequest copyPartRequest)
        {
            IRequest request = new DefaultRequest(copyPartRequest, "ObsS3");

            request.HttpMethod = "PUT";
            //2015-5-27 w00322557
            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("CopyPartRequestMarshaller HttpMethod: {0}", request.HttpMethod));

            if (copyPartRequest.IsSetSourceBucket())
                request.Headers.Add(HeaderKeys.XAmzCopySourceHeader, ConstructCopySourceHeaderValue(copyPartRequest.SourceBucket, copyPartRequest.SourceKey, copyPartRequest.SourceVersionId));

            if (copyPartRequest.IsSetETagToMatch())
                request.Headers.Add(HeaderKeys.XAmzCopySourceIfMatchHeader, AWSSDKUtils.Join(copyPartRequest.ETagToMatch));

            if (copyPartRequest.IsSetETagToNotMatch())
                request.Headers.Add(HeaderKeys.XAmzCopySourceIfNoneMatchHeader, AWSSDKUtils.Join(copyPartRequest.ETagsToNotMatch));

            if (copyPartRequest.IsSetModifiedSinceDate())
                request.Headers.Add(HeaderKeys.XAmzCopySourceIfModifiedSinceHeader, copyPartRequest.ModifiedSinceDate.ToUniversalTime().ToString(AWSSDKUtils.GMTDateFormat, CultureInfo.InvariantCulture));

            if (copyPartRequest.IsSetUnmodifiedSinceDate())
                request.Headers.Add(HeaderKeys.XAmzCopySourceIfUnmodifiedSinceHeader, copyPartRequest.UnmodifiedSinceDate.ToUniversalTime().ToString(AWSSDKUtils.GMTDateFormat, CultureInfo.InvariantCulture));

            if (copyPartRequest.IsSetServerSideEncryptionCustomerMethod())
                request.Headers.Add(HeaderKeys.XAmzSSECustomerAlgorithmHeader, copyPartRequest.ServerSideEncryptionCustomerMethod);
            if (copyPartRequest.IsSetServerSideEncryptionCustomerProvidedKey())
            {
                request.Headers.Add(HeaderKeys.XAmzSSECustomerKeyHeader, copyPartRequest.ServerSideEncryptionCustomerProvidedKey);
                if (copyPartRequest.IsSetServerSideEncryptionCustomerProvidedKeyMD5())
                    request.Headers.Add(HeaderKeys.XAmzSSECustomerKeyMD5Header, copyPartRequest.ServerSideEncryptionCustomerProvidedKeyMD5);
                else
                    request.Headers.Add(HeaderKeys.XAmzSSECustomerKeyMD5Header, ObsS3Util.ComputeEncodedMD5FromEncodedString(copyPartRequest.ServerSideEncryptionCustomerProvidedKey));
            }
            if (copyPartRequest.IsSetCopySourceServerSideEncryptionCustomerMethod())
                request.Headers.Add(HeaderKeys.XAmzCopySourceSSECustomerAlgorithmHeader, copyPartRequest.CopySourceServerSideEncryptionCustomerMethod);
            if (copyPartRequest.IsSetCopySourceServerSideEncryptionCustomerProvidedKey())
            {
                request.Headers.Add(HeaderKeys.XAmzCopySourceSSECustomerKeyHeader, copyPartRequest.CopySourceServerSideEncryptionCustomerProvidedKey);
                if (copyPartRequest.IsSetCopySourceServerSideEncryptionCustomerProvidedKeyMD5())
                    request.Headers.Add(HeaderKeys.XAmzCopySourceSSECustomerKeyMD5Header, copyPartRequest.CopySourceServerSideEncryptionCustomerProvidedKeyMD5);
                else
                    request.Headers.Add(HeaderKeys.XAmzCopySourceSSECustomerKeyMD5Header, ObsS3Util.ComputeEncodedMD5FromEncodedString(copyPartRequest.CopySourceServerSideEncryptionCustomerProvidedKey));
            }
            if (copyPartRequest.IsSetServerSideEncryptionKeyManagementServiceKeyId())
                request.Headers.Add(HeaderKeys.XAmzServerSideEncryptionAwsKmsKeyIdHeader, copyPartRequest.ServerSideEncryptionKeyManagementServiceKeyId);

            request.Headers.Add(HeaderKeys.XAmzCopySourceRangeHeader, ConstructCopySourceRangeHeader(copyPartRequest.FirstByte, copyPartRequest.LastByte));

            //2015-5-26 w00322557
            foreach (var head in request.Headers)
            {
                LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("CopyPartRequestMarshaller request header key: {0}, value: {1}", head.Key, head.Value));
            }

            request.ResourcePath = string.Format(CultureInfo.InvariantCulture, "/{0}/{1}",
                                                 S3Transforms.ToStringValue(copyPartRequest.DestinationBucket),
                                                 S3Transforms.ToStringValue(copyPartRequest.DestinationKey));

            LoggerMgr.Log_Run_Info(OBS.S3.ObsClient.strProduct,string.Format("CopyPartRequestMarshaller ResourcePath: {0}", request.ResourcePath));

            request.AddSubResource("partNumber", S3Transforms.ToStringValue(copyPartRequest.PartNumber));
            request.AddSubResource("uploadId", S3Transforms.ToStringValue(copyPartRequest.UploadId));

            request.UseQueryString = true;

            return request;
        }

        static string ConstructCopySourceHeaderValue(string bucket, string key, string version)
        {
            string source;
            if (!String.IsNullOrEmpty(key))
            {
                source = ObsS3Util.UrlEncode(String.Concat("/", bucket, "/", key), true);
                if (!String.IsNullOrEmpty(version))
                {
                    source = string.Format(CultureInfo.InvariantCulture, "{0}?versionId={1}", source, ObsS3Util.UrlEncode(version, true));
                }
            }
            else
            {
                source = ObsS3Util.UrlEncode(bucket, true);
            }

            return source;
        }

        static string ConstructCopySourceRangeHeader(long firstByte, long lastByte)
        {
            return string.Format(CultureInfo.InvariantCulture, "bytes={0}-{1}", firstByte, lastByte);
        }
    }
}

