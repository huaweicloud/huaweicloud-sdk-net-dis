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
using System.IO;
using System.Text;

using OBS.S3;
using OBS.S3.Model;
using System.Threading;

namespace OBS.S3.Transfer.Internal
{
    internal partial class DownloadDirectoryCommand : BaseCommand
    {
        IObsS3 _s3Client;
        TransferUtilityDownloadDirectoryRequest _request;
        int _totalNumberOfFilesToDownload;
        int _numberOfFilesDownloaded;
        long _totalBytes;
        long _transferredBytes;        
        string _currentFile;

        internal DownloadDirectoryCommand(IObsS3 s3Client, TransferUtilityDownloadDirectoryRequest request)
        {
            this._s3Client = s3Client;
            this._request = request;
        }

        private void downloadedProgressEventCallback(object sender, WriteObjectProgressArgs e)
        {
            var transferredBytes = Interlocked.Add(ref _transferredBytes, e.IncrementTransferred);

            int numberOfFilesDownloaded = _numberOfFilesDownloaded;
            if (e.TransferredBytes == e.TotalBytes)
            {
                numberOfFilesDownloaded = Interlocked.Increment(ref _numberOfFilesDownloaded);
            }

            DownloadDirectoryProgressArgs downloadDirectoryProgress = null;
            if (_request.DownloadFilesConcurrently)
            {
                // If concurrent download is enabled, values for current file, 
                // transferred and total bytes for current file are not set.
                downloadDirectoryProgress = new DownloadDirectoryProgressArgs(numberOfFilesDownloaded, _totalNumberOfFilesToDownload,
                           transferredBytes, _totalBytes,
                           null, 0, 0);
            }
            else
            {
                downloadDirectoryProgress = new DownloadDirectoryProgressArgs(numberOfFilesDownloaded, _totalNumberOfFilesToDownload,
                    transferredBytes, _totalBytes,
                    _currentFile, e.TransferredBytes, e.TotalBytes);
            }
            _request.OnRaiseProgressEvent(downloadDirectoryProgress);
        }

        private void EnsureDirectoryExists(DirectoryInfo directory)
        {
            if (directory.Exists)
                return;

            EnsureDirectoryExists(directory.Parent);
            directory.Create();
        }

        private TransferUtilityDownloadRequest ConstructTransferUtilityDownloadRequest(S3Object s3Object, int prefixLength)
        {
            var downloadRequest = new TransferUtilityDownloadRequest();
            downloadRequest.BucketName = this._request.BucketName;
            downloadRequest.Key = s3Object.Key;
            var file = s3Object.Key.Substring(prefixLength).Replace('/','\\');
            downloadRequest.FilePath = Path.Combine(this._request.LocalDirectory, file);
            downloadRequest.WriteObjectProgressEvent += downloadedProgressEventCallback;

            return downloadRequest;
        }

        private ListObjectsRequest ConstructListObjectRequest()
        {
            ListObjectsRequest listRequest = new ListObjectsRequest();
            listRequest.BucketName = this._request.BucketName;
            listRequest.Prefix = this._request.S3Directory;

            listRequest.Prefix = listRequest.Prefix.Replace('\\', '/');
            if (!listRequest.Prefix.EndsWith("/", StringComparison.Ordinal))
                listRequest.Prefix += "/";

            if (listRequest.Prefix.StartsWith("/", StringComparison.Ordinal))
            {
                if (listRequest.Prefix.Length == 1)
                    listRequest.Prefix = "";
                else
                    listRequest.Prefix = listRequest.Prefix.Substring(1);
            }

            return listRequest;
        }

        private void ValidateRequest()
        {
            if (!this._request.IsSetBucketName())
            {
                throw new InvalidOperationException("The bucketName Specified is null or empty!");
            }
            if (!this._request.IsSetS3Directory())
            {
                throw new InvalidOperationException("The S3Directory Specified is null or empty!");
            }
            if (!this._request.IsSetLocalDirectory())
            {
                throw new InvalidOperationException("The LocalDirectory Specified is null or empty!");
            }

            if (File.Exists(this._request.S3Directory))
            {
                throw new InvalidOperationException("A file already exists with the same name indicated by LocalDirectory!");
            }
        }
    }
}
