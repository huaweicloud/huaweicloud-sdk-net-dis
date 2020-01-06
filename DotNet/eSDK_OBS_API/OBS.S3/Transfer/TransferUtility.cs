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
using System.Threading;

using OBS.Runtime.Internal.Util;
using OBS.S3.Model;
using OBS.S3.Transfer.Internal;
using OBS.Util;
using System.Globalization;

namespace OBS.S3.Transfer
{
    /// <summary>
    /// 	<para>
    /// 	Provides a high level utility for managing transfers to and from Obs S3.
    /// 	</para>
    /// 	<para>
    /// 	<c>TransferUtility</c> provides a simple API for 
    /// 	uploading content to and downloading content
    /// 	from Obs S3. It makes extensive use of Obs S3 multipart uploads to
    /// 	achieve enhanced throughput, performance, and reliability. 
    /// 	</para>
    /// 	<para>
    /// 	When uploading large files by specifying file paths instead of a stream, 
    /// 	<c>TransferUtility</c> uses multiple threads to upload
    /// 	multiple parts of a single upload at once. When dealing with large content
    /// 	sizes and high bandwidth, this can increase throughput significantly.
    /// 	</para>
    /// </summary>
    /// <remarks>
    /// 	<para>
    /// 	Transfers are stored in memory. If the application is restarted, 
    /// 	previous transfers are no longer accessible. In this situation, if necessary, 
    /// 	you should clean up any multipart uploads that are incomplete.
    /// 	</para>
    /// </remarks>
    public partial class TransferUtility : IDisposable
    {
        TransferUtilityConfig _config;
        IObsS3 _s3Client;
        bool _shouldDispose = false;
        bool _isDisposed;

        #region Constructors

        /// <summary>
        /// 	Constructs a new <see cref="TransferUtility"/> class.
        /// </summary>
        /// <param name="awsAccessKeyId">
        /// 	The AWS Access Key ID.
        /// </param>
        /// <param name="awsSecretAccessKey">
        /// 	The AWS Secret Access Key.
        /// </param>
        /// <remarks>
        /// <para>
        /// If a Timeout needs to be specified, use the constructor which takes an <see cref="OBS.S3.ObsS3Client"/> as a paramater.
        /// Use an instance of <see cref="OBS.S3.ObsS3Client"/> constructed with an <see cref="OBS.S3.ObsS3Config"/> object with the Timeout specified. 
        /// </para>        
        /// </remarks>
        public TransferUtility(string awsAccessKeyId, string awsSecretAccessKey)
            : this(new ObsS3Client(awsAccessKeyId, awsSecretAccessKey))
        {
            this._shouldDispose = true;
        }

        /// <summary>
        /// 	Constructs a new <see cref="TransferUtility"/> class.
        /// </summary>
        /// <param name="awsAccessKeyId">
        /// 	The AWS Access Key ID.
        /// </param>
        /// <param name="awsSecretAccessKey">
        /// 	The AWS Secret Access Key.
        /// </param>
        /// <param name="region">
        ///     The region to configure the transfer utility for.
        /// </param>
        /// <remarks>
        /// <para>
        /// If a Timeout needs to be specified, use the constructor which takes an <see cref="OBS.S3.ObsS3Client"/> as a paramater.
        /// Use an instance of <see cref="OBS.S3.ObsS3Client"/> constructed with an <see cref="OBS.S3.ObsS3Config"/> object with the Timeout specified. 
        /// </para>        
        /// </remarks>
        public TransferUtility(string awsAccessKeyId, string awsSecretAccessKey, RegionEndpoint region)
            : this(new ObsS3Client(awsAccessKeyId, awsSecretAccessKey, region))
        {
            this._shouldDispose = true;
        }

        /// <summary>
        /// 	Constructs a new instance of the <see cref="TransferUtility"/> class.
        /// </summary>
        /// <param name="awsAccessKeyId">
        /// 	The AWS Access Key ID.
        /// </param>
        /// <param name="awsSecretAccessKey">
        /// 	The AWS Secret Access Key.
        /// </param>
        /// <param name="config">
        /// 	Specifies advanced settings.
        /// </param>
        /// <remarks>
        /// <para>
        /// If a Timeout needs to be specified, use the constructor which takes an <see cref="OBS.S3.ObsS3Client"/> as a paramater.
        /// Use an instance of <see cref="OBS.S3.ObsS3Client"/> constructed with an <see cref="OBS.S3.ObsS3Config"/> object with the Timeout specified. 
        /// </para>        
        /// </remarks>
        public TransferUtility(string awsAccessKeyId, string awsSecretAccessKey, TransferUtilityConfig config)
            : this(new ObsS3Client(awsAccessKeyId, awsSecretAccessKey), config)
        {
            this._shouldDispose = true;
        }

        /// <summary>
        /// 	Constructs a new instance of the <see cref="TransferUtility"/> class.
        /// </summary>
        /// <param name="awsAccessKeyId">
        /// 	The AWS Access Key ID.
        /// </param>
        /// <param name="awsSecretAccessKey">
        /// 	The AWS Secret Access Key.
        /// </param>
        /// <param name="region">
        ///     The region to configure the transfer utility for.
        /// </param>
        /// <param name="config">
        /// 	Specifies advanced settings.
        /// </param>
        /// <remarks>
        /// <para>
        /// If a Timeout needs to be specified, use the constructor which takes an <see cref="OBS.S3.ObsS3Client"/> as a paramater.
        /// Use an instance of <see cref="OBS.S3.ObsS3Client"/> constructed with an <see cref="OBS.S3.ObsS3Config"/> object with the Timeout specified. 
        /// </para>        
        /// </remarks>
        public TransferUtility(string awsAccessKeyId, string awsSecretAccessKey, RegionEndpoint region, TransferUtilityConfig config)
            : this(new ObsS3Client(awsAccessKeyId, awsSecretAccessKey, region), config)
        {
            this._shouldDispose = true;
        }


        /// <summary>
        /// 	Constructs a new instance of the <see cref="TransferUtility"/> class.
        /// </summary>
        /// <param name="s3Client">
        /// 	The Obs S3 client.
        /// </param>
        /// <remarks>
        /// <para>
        /// If a Timeout needs to be specified, use the constructor which takes an <see cref="OBS.S3.ObsS3Client"/> as a paramater.
        /// Use an instance of <see cref="OBS.S3.ObsS3Client"/> constructed with an <see cref="OBS.S3.ObsS3Config"/> object with the Timeout specified. 
        /// </para>        
        /// </remarks>
        public TransferUtility(IObsS3 s3Client)
            : this(s3Client, new TransferUtilityConfig())
        {
        }

        /// <summary>
        /// 	Initializes a new instance of the <see cref="TransferUtility"/> class.
        /// </summary>
        /// <param name="s3Client">
        /// 	The Obs S3 client.
        /// </param>
        /// <param name="config">
        /// 	Specifies advanced configuration settings for <see cref="TransferUtility"/>.
        /// </param>
        /// <remarks>
        /// <para>
        /// If a Timeout needs to be specified, use the constructor which takes an <see cref="OBS.S3.ObsS3Client"/> as a paramater.
        /// Use an instance of <see cref="OBS.S3.ObsS3Client"/> constructed with an <see cref="OBS.S3.ObsS3Config"/> object with the Timeout specified. 
        /// </para>        
        /// </remarks>
        public TransferUtility(IObsS3 s3Client, TransferUtilityConfig config)
        {
            this._s3Client = s3Client;
            this._config = config;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 	Gets the Obs S3 client used for making calls into Obs S3.
        /// </summary>
        /// <value>
        /// 	The Obs S3 client used for making calls into Obs S3.
        /// </value>
        public IObsS3 S3Client
        {
            get
            {
                return this._s3Client;
            }
        }

        #endregion

        #region Dispose Pattern Implementation

        /// <summary>
        /// Implements the Dispose pattern
        /// </summary>
        /// <param name="disposing">Whether this object is being disposed via a call to Dispose
        /// or garbage collected.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._isDisposed)
            {
                if (disposing && _s3Client != null && _shouldDispose)
                {
                    _s3Client.Dispose();
                    _s3Client = null;
                }
                this._isDisposed = true;
            }
        }

        /// <summary>
        /// Disposes of all managed and unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        private static TransferUtilityUploadRequest ConstructUploadRequest(string filePath, string bucketName)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("filePath");
            }
#if BCL     // Validations for Win RT/Win Phone are done in GetUploadCommand method's call to validate.
            if (!File.Exists(filePath))
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The file {0} does not exists!", filePath));
            }
#endif
            return new TransferUtilityUploadRequest()
            {
                BucketName = bucketName,
                FilePath = filePath
            };
        }

        private static TransferUtilityUploadRequest ConstructUploadRequest(string filePath, string bucketName, string key)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentNullException("filePath");
            }
#if BCL     // Validations for Win RT/Win Phone are done in GetUploadCommand method's call to validate.
            if (!File.Exists(filePath))
            {
                throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The file {0} does not exists!", filePath));
            }
# endif
            return new TransferUtilityUploadRequest()
            {
                BucketName = bucketName,
                Key = key,
                FilePath = filePath
            };
        }

        private static TransferUtilityUploadRequest ConstructUploadRequest(Stream stream, string bucketName, string key)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream");
            }
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException("key");
            }

            return new TransferUtilityUploadRequest()
            {
                BucketName = bucketName,
                Key = key,
                InputStream = stream
            };
        }

        internal BaseCommand GetUploadCommand(TransferUtilityUploadRequest request)
        {
            validate(request);

            if (IsMultipartUpload(request))
            {
                return new MultipartUploadCommand(this._s3Client, this._config, request);
            }
            else
            {
                return new SimpleUploadCommand(this._s3Client, this._config, request);
            }
            }

        bool IsMultipartUpload(TransferUtilityUploadRequest request)
        {
            return request.ContentLength >= this._config.MinSizeBeforePartUpload;
        }

        static void validate(TransferUtilityUploadRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            if (!request.IsSetBucketName())
            {
                throw new InvalidOperationException("Please specify BucketName to PUT an object into Obs S3.");
            }
#if BCL
            if (!request.IsSetFilePath() &&
                !request.IsSetInputStream())
            {
                throw new InvalidOperationException(
                    "Please specify either a Filename or provide a Stream to PUT an object into Obs S3.");
            }
#elif WIN_RT || WINDOWS_PHONE
            if (!request.IsSetFilePath() &&
                !request.IsSetStorageFile() &&
                !request.IsSetInputStream())
            {
                throw new InvalidOperationException(
                    "Please specify either a StorageFile, FilePath or provide a Stream to PUT an object into Obs S3.");
            }
#endif
            if (!request.IsSetKey())
            {
                if (request.IsSetFilePath())
                {
                    request.Key = Path.GetFileName(request.FilePath);
                }
#if WIN_RT || WINDOWS_PHONE
                else if (request.IsSetStorageFile())
                {
                    request.Key = request.StorageFile.Name;
                }
#endif
                else
                {
                    throw new ArgumentException(
                        "The Key property must be specified when using a Stream to upload into Obs S3.");
                }
            }
#if BCL
            if (request.IsSetFilePath() && !File.Exists(request.FilePath))
                throw new ArgumentException("The file indicated by the FilePath property does not exist!");
#elif WIN_RT || WINDOWS_PHONE
            if (request.IsSetFilePath() && !request.IsSetStorageFile())
        {
                try
            {
                    request.StorageFile = System.Threading.Tasks.Task.Run(() =>
                        Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(new Uri(request.FilePath)).AsTask()).Result;
        }
                catch (Exception exception)
        {
                    throw new ArgumentException("An error occured while loading the file indicated by the FilePath property.", exception);
        }
        }
#endif
        }

    }
}
