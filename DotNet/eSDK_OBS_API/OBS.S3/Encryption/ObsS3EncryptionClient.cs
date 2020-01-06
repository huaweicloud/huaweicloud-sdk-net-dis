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
using OBS.Runtime;
using OBS.Runtime.Internal;
using OBS.Runtime.Internal.Transform;
using OBS.Runtime.Internal.Util;
using OBS.S3.Model;
using OBS.Util;
using System.Collections.Generic;
using System.Globalization;

namespace OBS.S3.Encryption
{
    public partial class ObsS3EncryptionClient : ObsS3Client
    {
        internal EncryptionMaterials EncryptionMaterials
        {
            get;
            private set;
        }


        private ObsS3Client s3ClientForInstructionFile;
        
        internal ObsS3Client S3ClientForInstructionFile
	    {
	        get
	        {
	            if (s3ClientForInstructionFile == null)
	            {
                    s3ClientForInstructionFile = new ObsS3Client(Credentials, S3CryptoConfig);
                }
	            return s3ClientForInstructionFile;
	        }
	    }

        internal ObsS3CryptoConfiguration S3CryptoConfig
        {
            get;
            private set;
        }

        internal Dictionary<string, UploadPartEncryptionContext> CurrentMultiPartUploadKeys = new Dictionary<string, UploadPartEncryptionContext>();

        internal const string S3CryptoStream = "S3-Crypto-Stream";

        #region Constructors
        /// <summary>
        /// Constructs ObsS3EncryptionClient with the Encryption materials and credentials loaded from the application's
        /// default configuration, and if unsuccessful from the Instance Profile service on an EC2 instance.
        /// 
        /// Example App.config with credentials set. 
        /// <code>
        /// &lt;?xml version="1.0" encoding="utf-8" ?&gt;
        /// &lt;configuration&gt;
        ///     &lt;appSettings&gt;
        ///         &lt;add key="AWSProfileName" value="AWS Default"/&gt;
        ///     &lt;/appSettings&gt;
        /// &lt;/configuration&gt;
        /// </code>
        /// 
        /// </summary>
        /// <param name="materials">
        /// The encryption materials to be used to encrypt and decrypt envelope key.
        /// </param>
        public ObsS3EncryptionClient(EncryptionMaterials materials)
            : base()
        {
            this.EncryptionMaterials = materials;
            S3CryptoConfig = new ObsS3CryptoConfiguration();
        }

        /// <summary>
        /// Constructs ObsS3EncryptionClient with the Encryption materials and credentials loaded from the application's
        /// default configuration, and if unsuccessful from the Instance Profile service on an EC2 instance.
        /// 
        /// Example App.config with credentials set. 
        /// <code>
        /// &lt;?xml version="1.0" encoding="utf-8" ?&gt;
        /// &lt;configuration&gt;
        ///     &lt;appSettings&gt;
        ///         &lt;add key="AWSProfileName" value="AWS Default"/&gt;
        ///     &lt;/appSettings&gt;
        /// &lt;/configuration&gt;
        /// </code>
        /// 
        /// </summary>
        /// <param name="region">
        /// The region to connect.
        /// </param>
        /// <param name="materials">
        /// The encryption materials to be used to encrypt and decrypt envelope key.
        /// </param>
        public ObsS3EncryptionClient(RegionEndpoint region, EncryptionMaterials materials)
            : base(region)
        {
            this.EncryptionMaterials = materials;  
            S3CryptoConfig = new ObsS3CryptoConfiguration();
        }

        /// <summary>
        /// Constructs ObsS3EncryptionClient with the Encryption materials, 
        /// ObsS3 CryptoConfiguration object and credentials loaded from the application's
        /// default configuration, and if unsuccessful from the Instance Profile service on an EC2 instance.
        /// 
        /// Example App.config with credentials set. 
        /// <code>
        /// &lt;?xml version="1.0" encoding="utf-8" ?&gt;
        /// &lt;configuration&gt;
        ///     &lt;appSettings&gt;
        ///         &lt;add key="AWSProfileName" value="AWS Default"/&gt;
        ///     &lt;/appSettings&gt;
        /// &lt;/configuration&gt;
        /// </code>
        /// 
        /// </summary>
        /// <param name="config">
        /// The ObsS3EncryptionClient CryptoConfiguration Object
        /// </param>
        /// <param name="materials">
        /// The encryption materials to be used to encrypt and decrypt envelope key.
        /// </param>
        public ObsS3EncryptionClient(ObsS3CryptoConfiguration config, EncryptionMaterials materials)
            : base(config)
        {
            this.EncryptionMaterials = materials; 
            S3CryptoConfig = config;
        }

        /// <summary>
        ///  Constructs ObsS3EncryptionClient with AWS Credentials and Encryption materials.
        /// </summary>
        /// <param name="materials">
        /// The encryption materials to be used to encrypt and decrypt envelope key.
        /// </param>
        /// <param name="credentials">AWS Credentials</param>
        public ObsS3EncryptionClient(AWSCredentials credentials, EncryptionMaterials materials)
            : base(credentials)
        {
            this.EncryptionMaterials = materials; 
            S3CryptoConfig = new ObsS3CryptoConfiguration();
        }

        /// <summary>
        /// Constructs ObsS3EncryptionClient with AWS Credentials, Region and Encryption materials
        /// </summary>
        /// <param name="credentials">AWS Credentials</param>
        /// <param name="region">The region to connect.</param>
        /// <param name="materials">
        /// The encryption materials to be used to encrypt and decrypt envelope key.
        /// </param>
        public ObsS3EncryptionClient(AWSCredentials credentials, RegionEndpoint region, EncryptionMaterials materials)
            : base(credentials, region)
        {
            this.EncryptionMaterials = materials; 
            S3CryptoConfig = new ObsS3CryptoConfiguration();
        }

        /// <summary>
        /// Constructs ObsS3EncryptionClient with AWS Credentials, ObsS3CryptoConfiguration Configuration object
        /// and Encryption materials
        /// </summary>
        /// <param name="credentials">AWS Credentials</param>
        /// <param name="config">The ObsS3EncryptionClient CryptoConfiguration Object</param>
        /// <param name="materials">
        /// The encryption materials to be used to encrypt and decrypt envelope key.
        /// </param>
        public ObsS3EncryptionClient(AWSCredentials credentials, ObsS3CryptoConfiguration config, EncryptionMaterials materials)
            : base(credentials, config)
        {
            this.EncryptionMaterials = materials; 
            S3CryptoConfig = config;
        }

        /// <summary>
        /// Constructs ObsS3EncryptionClient with AWS Access Key ID,
        /// AWS Secret Key and Encryption materials
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="materials">The encryption materials to be used to encrypt and decrypt envelope key.</param>
        public ObsS3EncryptionClient(string awsAccessKeyId, string awsSecretAccessKey, EncryptionMaterials materials)
            : base(awsAccessKeyId, awsSecretAccessKey)
        {
            this.EncryptionMaterials = materials;
            S3CryptoConfig = new ObsS3CryptoConfiguration();
        }

        /// <summary>
        /// Constructs ObsS3EncryptionClient with AWS Access Key ID,
        /// AWS Secret Key, Region and Encryption materials
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="region">The region to connect.</param>
        /// <param name="materials">The encryption materials to be used to encrypt and decrypt envelope key.</param>
        public ObsS3EncryptionClient(string awsAccessKeyId, string awsSecretAccessKey, RegionEndpoint region, EncryptionMaterials materials)
            : base(awsAccessKeyId, awsSecretAccessKey, region)
        {
            this.EncryptionMaterials = materials; 
            S3CryptoConfig = new ObsS3CryptoConfiguration();
        }

        /// <summary>
        /// Constructs ObsS3EncryptionClient with AWS Access Key ID, Secret Key,
        /// ObsS3 CryptoConfiguration object and Encryption materials.
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="config">The ObsS3EncryptionClient CryptoConfiguration Object</param>
        /// <param name="materials">The encryption materials to be used to encrypt and decrypt envelope key.</param>
        public ObsS3EncryptionClient(string awsAccessKeyId, string awsSecretAccessKey, ObsS3CryptoConfiguration config, EncryptionMaterials materials)
            : base(awsAccessKeyId, awsSecretAccessKey, config)
        {
            this.EncryptionMaterials = materials;
            S3CryptoConfig = config;
        }

        /// <summary>
        /// Constructs ObsS3EncryptionClient with AWS Access Key ID, Secret Key,
        /// SessionToken and Encryption materials.
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="awsSessionToken">AWS Session Token</param>
        /// <param name="materials">
        /// The encryption materials to be used to encrypt and decrypt envelope key.
        /// </param>
        public ObsS3EncryptionClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken, EncryptionMaterials materials)
            : base(awsAccessKeyId, awsSecretAccessKey, awsSessionToken)
        {
            this.EncryptionMaterials = materials; 
            S3CryptoConfig = new ObsS3CryptoConfiguration();
        }

        /// <summary>
        /// Constructs ObsS3EncryptionClient with AWS Access Key ID, Secret Key,
        ///  SessionToken, Region and Encryption materials.
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="awsSessionToken">AWS Session Token</param>
        /// <param name="region">The region to connect.</param>
        /// <param name="materials">The encryption materials to be used to encrypt and decrypt envelope key.</param>
        public ObsS3EncryptionClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken, RegionEndpoint region, EncryptionMaterials materials)
            : base(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, region)
        {
            this.EncryptionMaterials = materials; 
            S3CryptoConfig = new ObsS3CryptoConfiguration();
        }

        /// <summary>
        /// Constructs ObsS3EncryptionClient with AWS Access Key ID, Secret Key, SessionToken
        /// ObsS3EncryptionClient CryptoConfiguration object and Encryption materials.
        /// </summary>
        /// <param name="awsAccessKeyId">AWS Access Key ID</param>
        /// <param name="awsSecretAccessKey">AWS Secret Access Key</param>
        /// <param name="awsSessionToken">AWS Session Token</param>
        /// <param name="config">The ObsS3EncryptionClient CryptoConfiguration Object</param>
        /// <param name="materials">
        /// The encryption materials to be used to encrypt and decrypt envelope key.
        /// </param>
        public ObsS3EncryptionClient(string awsAccessKeyId, string awsSecretAccessKey, string awsSessionToken, ObsS3CryptoConfiguration config, EncryptionMaterials materials)
            : base(awsAccessKeyId, awsSecretAccessKey, awsSessionToken, config)
        {
            this.EncryptionMaterials = materials; 
            S3CryptoConfig = config;
        }        

        #endregion


        /// <summary>
        /// Turn off response logging because it will interfere with decrypt of the data coming back from S3.
        /// </summary>
        protected override bool SupportResponseLogging
        {
            get
            {
                return false;
            }
        }

        protected override void CustomizeRuntimePipeline(RuntimePipeline pipeline)
        {
            base.CustomizeRuntimePipeline(pipeline);

            pipeline.AddHandlerBefore<OBS.Runtime.Internal.Marshaller>(new OBS.S3.Encryption.Internal.SetupEncryptionHandler(this));
            pipeline.AddHandlerAfter<OBS.Runtime.Internal.Marshaller>(new OBS.S3.Encryption.Internal.UserAgentHandler());
            pipeline.AddHandlerBefore<OBS.S3.Internal.ObsS3ResponseHandler>(new OBS.S3.Encryption.Internal.SetupDecryptionHandler(this));
        }  
    }
}
