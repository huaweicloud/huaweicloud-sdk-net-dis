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
using OBS.Runtime.Internal;
using OBS.Util;

#if WIN_RT || WINDOWS_PHONE
using System.Threading.Tasks;
#endif

namespace OBS.S3.Internal
{
    public class ObsS3PreMarshallHandler : GenericHandler
    {
        protected override void PreInvoke(IExecutionContext executionContext)
        {
            ProcessPreRequestHandlers(executionContext);
        }

        private static void ProcessPreRequestHandlers(IExecutionContext executionContext)
        {
            var request = executionContext.RequestContext.OriginalRequest;
            var config = executionContext.RequestContext.ClientConfig;


            var putObjectRequest = request as PutObjectRequest;
            if (putObjectRequest != null)
            {
                if (putObjectRequest.InputStream != null && !string.IsNullOrEmpty(putObjectRequest.FilePath))
                    throw new ArgumentException("Please specify one of either an InputStream or a FilePath to be PUT as an S3 object.");
                if (!string.IsNullOrEmpty(putObjectRequest.ContentBody) && !string.IsNullOrEmpty(putObjectRequest.FilePath))
                    throw new ArgumentException("Please specify one of either a FilePath or the ContentBody to be PUT as an S3 object.");
                if (putObjectRequest.InputStream != null && !string.IsNullOrEmpty(putObjectRequest.ContentBody))
                    throw new ArgumentException("Please specify one of either an InputStream or the ContentBody to be PUT as an S3 object.");

                if (!putObjectRequest.Headers.IsSetContentType())
                {
                    // Get the extension of the file from the path.
                    // Try the key as well.
                    string ext = null;
                    if (!string.IsNullOrEmpty(putObjectRequest.FilePath))
                        ext = AWSSDKUtils.GetExtension(putObjectRequest.FilePath);
#if WIN_RT || WINDOWS_PHONE
                    if(putObjectRequest.StorageFile != null)
                        ext = AWSSDKUtils.GetExtension(putObjectRequest.StorageFile.Path);
#endif
                    if (String.IsNullOrEmpty(ext) && putObjectRequest.IsSetKey())
                    {
                        ext = AWSSDKUtils.GetExtension(putObjectRequest.Key);
                    }
                    if (!String.IsNullOrEmpty(ext))
                    // Use the extension to get the mime-type
                    {
                        putObjectRequest.Headers.ContentType = ObsS3Util.MimeTypeFromExtension(ext);
                    }
                }

                if (putObjectRequest.InputStream != null)
                {
                    if (putObjectRequest.AutoResetStreamPosition && putObjectRequest.InputStream.CanSeek)
                    {
                        putObjectRequest.InputStream.Seek(0, SeekOrigin.Begin);
                    }
                }

                if (!string.IsNullOrEmpty(putObjectRequest.FilePath))
                {
                    putObjectRequest.SetupForFilePath();
                }
#if WIN_RT || WINDOWS_PHONE
                else if(putObjectRequest.StorageFile != null)
                {
                    putObjectRequest.InputStream = Task.Run(() =>
                        putObjectRequest.StorageFile.OpenAsync(Windows.Storage.FileAccessMode.Read).AsTask())
                        .Result.AsStreamForRead();
                    if (string.IsNullOrEmpty(putObjectRequest.Key))
                    {
                        putObjectRequest.Key = Path.GetFileName(putObjectRequest.StorageFile.Name);
                    }
                }
#endif
                else if (null == putObjectRequest.InputStream)
                {
                    if (string.IsNullOrEmpty(putObjectRequest.Headers.ContentType))
                        putObjectRequest.Headers.ContentType = "text/plain";

                    var payload = Encoding.UTF8.GetBytes(putObjectRequest.ContentBody ?? "");

                    putObjectRequest.InputStream = new MemoryStream(payload);
                }
            }

            var putBucketRequest = request as PutBucketRequest;
            if (putBucketRequest != null)
            {
                // UseClientRegion only applies if neither BucketRegionName and BucketRegion are set.
                if (putBucketRequest.UseClientRegion &&
                    !(putBucketRequest.IsSetBucketRegionName() || putBucketRequest.IsSetBucketRegion()))
                {
                    var regionCode = DetermineBucketRegionCode(config);
                    if (regionCode == S3Constants.REGION_US_EAST_1)
                        regionCode = null;
                    else if (regionCode == S3Constants.REGION_EU_WEST_1)
                        regionCode = "EU";

                    putBucketRequest.BucketRegion = regionCode;
                }
            }

            var deleteBucketRequest = request as DeleteBucketRequest;
            if (deleteBucketRequest != null)
            {
                if (deleteBucketRequest.UseClientRegion && !deleteBucketRequest.IsSetBucketRegion())
                {
                    var regionCode = DetermineBucketRegionCode(config);
                    if (regionCode == S3Constants.REGION_US_EAST_1)
                        regionCode = null;

                    if (regionCode != null)
                        deleteBucketRequest.BucketRegion = regionCode;
                }
            }

            var uploadPartRequest = request as UploadPartRequest;
            if (uploadPartRequest != null)
            {
                if (uploadPartRequest.InputStream != null && !string.IsNullOrEmpty(uploadPartRequest.FilePath))
                    throw new ArgumentException("Please specify one of either a InputStream or a FilePath to be PUT as an S3 object.");

                if (uploadPartRequest.IsSetFilePath())
                {
                    uploadPartRequest.SetupForFilePath();
                }
#if WIN_RT || WINDOWS_PHONE
                else if(uploadPartRequest.StorageFile != null)
                {
                    uploadPartRequest.InputStream = Task.Run(() =>
                        uploadPartRequest.StorageFile.OpenAsync(Windows.Storage.FileAccessMode.Read).AsTask())
                        .Result.AsStreamForRead();
                    uploadPartRequest.InputStream.Position = uploadPartRequest.FilePosition;
                }
#endif
            }

            var initMultipartRequest = request as InitiateMultipartUploadRequest;
            if (initMultipartRequest != null)
            {
                if (!initMultipartRequest.Headers.IsSetContentType())
                {
                    // Get the extension of the object key.
                    string ext = AWSSDKUtils.GetExtension(initMultipartRequest.Key);

                    // Use the extension to get the mime-type
                    if (!String.IsNullOrEmpty(ext))
                    {
                        initMultipartRequest.Headers.ContentType = ObsS3Util.MimeTypeFromExtension(ext);
                    }
                }
            }
        }

        static string DetermineBucketRegionCode(ClientConfig config)
        {
            if (config.RegionEndpoint != null && string.IsNullOrEmpty(config.ServiceURL))
                return config.RegionEndpoint.SystemName;

            return AWSSDKUtils.DetermineRegion(config.DetermineServiceURL());
        }
    }
}
