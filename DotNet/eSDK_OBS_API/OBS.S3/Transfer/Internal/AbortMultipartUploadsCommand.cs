﻿/*******************************************************************************
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
using System.Collections.Generic;
using System.Text;

using OBS.S3;
using OBS.S3.Model;

namespace OBS.S3.Transfer.Internal
{
    internal partial class AbortMultipartUploadsCommand : BaseCommand
    {
        IObsS3 _s3Client;
        string _bucketName; 
        DateTime _initiatedDate;

        internal AbortMultipartUploadsCommand(IObsS3 s3Client, string bucketName, DateTime initiateDate)
        {
            this._s3Client = s3Client;
            this._bucketName = bucketName;
            this._initiatedDate = initiateDate;
        }

        private ListMultipartUploadsRequest ConstructListMultipartUploadsRequest(ListMultipartUploadsResponse listResponse)
            {
                ListMultipartUploadsRequest listRequest = new ListMultipartUploadsRequest()
                {
                    BucketName = this._bucketName,
                    KeyMarker = listResponse.KeyMarker,
                    UploadIdMarker = listResponse.NextUploadIdMarker,
                };
                listRequest.BeforeRequestEvent += this.RequestEventHandler;
            return listRequest;
        }

        private AbortMultipartUploadRequest ConstructAbortMultipartUploadRequest(MultipartUpload upload)
                    {
                        var abortRequest = new AbortMultipartUploadRequest()
                        {
                            BucketName = this._bucketName,
                            Key = upload.Key,
                            UploadId = upload.UploadId,
                        };
                        abortRequest.BeforeRequestEvent += this.RequestEventHandler;
            return abortRequest;
        }
    }
}
