﻿/*
 * Copyright 2010-2014 OBS.com, Inc. or its affiliates. All Rights Reserved.
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OBS.Runtime;
using OBS.S3.Model;
using System.IO;
using OBS.S3.Util;
using OBS.Util;
using OBS.Runtime.Internal.Util;
using OBS.Runtime.Internal.Transform;
using OBS.Runtime.Internal;

namespace OBS.S3.Internal
{
    public class ObsS3ResponseHandler : GenericHandler
    {
        protected override void PostInvoke(IExecutionContext executionContext)
        {
            ProcessResponseHandlers(executionContext);
        }

        private static void ProcessResponseHandlers(IExecutionContext executionContext)
        {
            ObsWebServiceResponse response = executionContext.ResponseContext.Response;
            IRequest request = executionContext.RequestContext.Request;
            IWebResponseData webResponseData = executionContext.ResponseContext.HttpResponse;
            bool isSse = HasSSEHeaders(webResponseData);

            var getObjectResponse = response as GetObjectResponse;
            if (getObjectResponse != null)
            {
                GetObjectRequest getObjectRequest = request.OriginalRequest as GetObjectRequest;
                if (getObjectRequest == null)
                {
                    return;
                }
                getObjectResponse.BucketName = getObjectRequest.BucketName;
                getObjectResponse.Key = getObjectRequest.Key;

                // If ETag is present and is an MD5 hash (not a multi-part upload ETag), and no byte range is specified,
                // wrap the response stream in an MD5Stream.
                // If there is a customer encryption algorithm the etag is not an MD5.
                if (!string.IsNullOrEmpty(getObjectResponse.ETag)
                    && !getObjectResponse.ETag.Contains("-")
                    && !isSse
                    && getObjectRequest.ByteRange == null)
                {
                    string etag = getObjectResponse.ETag.Trim(etagTrimChars);
                    byte[] expectedHash = AWSSDKUtils.HexStringToBytes(etag);
                    HashStream hashStream = new MD5Stream(getObjectResponse.ResponseStream, expectedHash, getObjectResponse.ContentLength);
                    getObjectResponse.ResponseStream = hashStream;
                }
            }

            var deleteObjectsResponse = response as DeleteObjectsResponse;
            if (deleteObjectsResponse != null)
            {
                if (deleteObjectsResponse.DeleteErrors != null && deleteObjectsResponse.DeleteErrors.Count > 0)
                {
                    throw new DeleteObjectsException(deleteObjectsResponse as DeleteObjectsResponse);
                }
            }

            var putObjectResponse = response as PutObjectResponse;
            var putObjectRequest = request.OriginalRequest as PutObjectRequest;
            if (putObjectRequest != null)
            {
                // If InputStream was a HashStream, compare calculated hash to returned etag
                HashStream hashStream = putObjectRequest.InputStream as HashStream;
                if (hashStream != null)
                {
                    if (putObjectResponse != null && !isSse)
                    {
                        // Stream may not have been closed, so force calculation of hash
                        hashStream.CalculateHash();
                        CompareHashes(putObjectResponse.ETag, hashStream.CalculatedHash);
                    }

                    // Set InputStream to its original value
                    putObjectRequest.InputStream = hashStream.GetNonWrapperBaseStream();
                }
            }

            var listObjectsResponse = response as ListObjectsResponse;
            if (listObjectsResponse != null)
            {
                if (listObjectsResponse.IsTruncated &&
                    string.IsNullOrEmpty(listObjectsResponse.NextMarker) &&
                    listObjectsResponse.S3Objects.Count > 0)
                {
                    listObjectsResponse.NextMarker = listObjectsResponse.S3Objects.Last().Key;
                }
            }

            var uploadPartRequest = request.OriginalRequest as UploadPartRequest;
            var uploadPartResponse = response as UploadPartResponse;
            if (uploadPartRequest != null)
            {
                if (uploadPartResponse != null)
                    uploadPartResponse.PartNumber = uploadPartRequest.PartNumber;

                // If InputStream was a HashStream, compare calculated hash to returned etag
                HashStream hashStream = uploadPartRequest.InputStream as HashStream;
                if (hashStream != null)
                {
                    if (uploadPartResponse != null && !isSse)
                    {
                        // Stream may not have been closed, so force calculation of hash
                        hashStream.CalculateHash();
                        CompareHashes(uploadPartResponse.ETag, hashStream.CalculatedHash);
                    }

                    // Set InputStream to its original value
                    uploadPartRequest.InputStream = hashStream.GetNonWrapperBaseStream();
                }
            }

            var copyPartResponse = response as CopyPartResponse;
            if (copyPartResponse != null)
            {
                copyPartResponse.PartNumber = ((CopyPartRequest)request.OriginalRequest).PartNumber;
            }

            ObsS3Client.CleanupRequest(request);
        }

        private static bool HasSSEHeaders(IWebResponseData webResponseData)
        {
            bool usesCustomerAlgorithm = !string.IsNullOrEmpty(webResponseData.GetHeaderValue(HeaderKeys.XAmzSSECustomerAlgorithmHeader));
            bool usesKmsKeyId = !string.IsNullOrEmpty(webResponseData.GetHeaderValue(HeaderKeys.XAmzServerSideEncryptionAwsKmsKeyIdHeader));
            return usesCustomerAlgorithm || usesKmsKeyId;
        }

        private static char[] etagTrimChars = new char[] { '\"' };
        // Compares ETag from S3 to calculated hash
        // If ETag is empty or is for a multi-part upload, no comparison is made
        // If ETag doesn't match the hash, an exception is thrown
        private static void CompareHashes(string etag, byte[] hash)
        {
            if (string.IsNullOrEmpty(etag))
                return;

            // if etag contains '-' character, the file was a multi-upload and we can't
            // compare the etag to the hash value
            if (etag.Contains("-"))
                return;

            etag = etag.Trim(etagTrimChars);

            string hexHash = AWSSDKUtils.BytesToHexString(hash);
            if (!string.Equals(etag, hexHash, StringComparison.OrdinalIgnoreCase))
                throw new ObsClientException("Expected hash not equal to calculated hash");
        }
    }
}
