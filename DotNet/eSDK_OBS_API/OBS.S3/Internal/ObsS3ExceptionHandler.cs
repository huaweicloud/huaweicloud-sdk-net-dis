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
using OBS.Runtime.Internal;
using OBS.Runtime.Internal.Util;
using OBS.S3.Model;

namespace OBS.S3.Internal
{
    public class ObsS3ExceptionHandler : GenericExceptionHandler
    {
        protected override void HandleException(IExecutionContext executionContext, Exception exception)
        {

            var putObjectRequest = executionContext.RequestContext.OriginalRequest as PutObjectRequest;
            if (putObjectRequest != null)
            {
                // If InputStream was a HashStream, compare calculated hash to returned etag
                HashStream hashStream = putObjectRequest.InputStream as HashStream;
                if (hashStream != null)
                {
                    // Set InputStream to its original value
                    putObjectRequest.InputStream = hashStream.GetNonWrapperBaseStream();
                }
            }

            var uploadPartRequest = executionContext.RequestContext.OriginalRequest as UploadPartRequest;
            if (uploadPartRequest != null)
            {
                // If InputStream was a HashStream, compare calculated hash to returned etag
                HashStream hashStream = uploadPartRequest.InputStream as HashStream;
                if (hashStream != null)
                {
                    // Set InputStream to its original value
                    uploadPartRequest.InputStream = hashStream.GetNonWrapperBaseStream();
                }
            }

            if (executionContext.RequestContext.Request != null)
                ObsS3Client.CleanupRequest(executionContext.RequestContext.Request);
        }
    }
}
