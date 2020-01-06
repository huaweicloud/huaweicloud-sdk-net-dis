/*******************************************************************************
 *  Copyright 2008-2013 OBS.com, Inc. or its affiliates. All Rights Reserved.
 *  Licensed under the Apache License, Version 2.0 (the "License"). You may not use
 *  this file except in compliance with the License. A copy of the License is located at
 *
 *  
 *
 *  or in the "license" file accompanying this file.
 *  This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
 *  CONDITIONS OF ANY KIND, either express or implied. See the License for the
 *  specific language governing permissions and limitations under the License.
 * *****************************************************************************
 *    __  _    _  ___
 *   (  )( \/\/ )/ __)
 *   /__\ \    / \__ \
 *  (_)(_) \/\/  (___/
 *
 *  AWS SDK for .NET
 *  API Version: 2006-03-01
 *
 */

using System;
using System.IO;
using OBS.Runtime;
using OBS.S3.Model;

namespace OBS.S3.Transfer.Internal
{
    internal partial class DownloadCommand
    {
        public override void Execute()
        {
            ValidateRequest();
            GetObjectRequest getRequest = ConvertToGetObjectRequest(this._request);

            var maxRetries = ((ObsS3Client)_s3Client).Config.MaxErrorRetry;
            var retries = 0;
            bool shouldRetry = false;
            do
            {
                shouldRetry = false;
                using (var response = this._s3Client.GetObject(getRequest))
                {
                    try
                    {
                        response.WriteObjectProgressEvent += OnWriteObjectProgressEvent;
                        response.WriteResponseStreamToFile(this._request.FilePath);
                    }
                    catch (Exception exception)
                    {
                        retries++;
                        shouldRetry = HandleException(exception, retries, maxRetries);
                        if (!shouldRetry)
                        {
                            if (exception is IOException)
                            {
                                throw;
                            }
                            else if (exception is ObsServiceException ||
                                exception is ObsClientException)
                            {
                                throw;
                            }

                            else
                            {
                                throw new ObsServiceException(exception);
                            }
                        }
                    }
                }
                WaitBeforeRetry(retries);
            } while (shouldRetry);
        }        
    }
}
