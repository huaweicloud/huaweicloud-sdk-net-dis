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
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Com.Bigdata.Dis.Sdk.DISCommon.Auth;
using Com.Bigdata.Dis.Sdk.DISCommon.Utils;
using OBS;
using OBS.Runtime;
using OBS.Runtime.Internal.Auth;
using OBS.Util;
using OBS.Runtime.Internal.Util;
using OBS.Runtime.Internal;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Signer
{
    public abstract class AbstractDISSigner
    {
        private AWS4Signer _aws4Signer;
        private AWS4Signer AWS4SignerInstance
        {
            get
            {
                if (_aws4Signer == null)
                {
                    lock (this)
                    {
                        if (_aws4Signer == null)
                            _aws4Signer = new AWS4Signer();
                    }
                }

                return _aws4Signer;
            }
        }


        protected const string DEFAULT_ENCODING = "UTF-8";

        /// <summary>
        /// Computes RFC 2104-compliant HMAC signature.
        /// </summary>
        protected static byte[] ComputeHash(string data)
        {
            try 
            {
                ///string signature = CryptoUtilFactory.CryptoInstance.HMACSign(data, secretkey, algorithm);

                //return signature;

                using (var sha256 = SHA256.Create())
                {
                    return sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
                }
            } 
            catch (Exception e) 
            {
                throw new OBS.Runtime.SignatureException("Failed to generate signature: " + e.Message, e);
            }
        }

        protected static string ComputeHash(string data, string secretkey, SigningAlgorithm algorithm)
        {
            try
            {
                string signature = CryptoUtilFactory.CryptoInstance.HMACSign(data, secretkey, algorithm);

                return signature;
            }
            catch (Exception e)
            {
                throw new OBS.Runtime.SignatureException("Failed to generate signature: " + e.Message, e);
            }
        }

        /// <summary>
        /// Computes RFC 2104-compliant HMAC signature.
        /// </summary>
        protected static string ComputeHash(byte[] data, string secretkey, SigningAlgorithm algorithm)
        {
            try
            {
                string signature = CryptoUtilFactory.CryptoInstance.HMACSign(data, secretkey, algorithm);

                return signature;
            }
            catch (Exception e)
            {
                throw new OBS.Runtime.SignatureException("Failed to generate signature: " + e.Message, e);
            }
        }

        protected static byte[] ComputeHash(byte[] data, string secretkey, SigningAlgorithm algorithm, bool ll)
        {
            return Encoding.UTF8.GetBytes(ComputeHash(data, secretkey, algorithm));
        }

        public abstract void Sign(IRequest request, ClientConfig clientConfig, RequestMetrics metrics, string awsAccessKeyId, string awsSecretAccessKey);

        public abstract void Sign(IRequest request, string awsAccessKeyId, string awsSecretAccessKey);

        public abstract void Sign(IRequest request, ICredentials credentials);

        public abstract ClientProtocol Protocol { get; }

        /// <summary>
        /// Inspects the supplied evidence to return the signer appropriate for the operation
        /// </summary>
        /// <param name="useSigV4Setting">Global setting for the service</param>
        /// <param name="request">The request.</param>
        /// <param name="config">Configuration for the client</param>
        /// <returns>True if signature v4 request signing should be used</returns>
        protected static bool UseV4Signing(bool useSigV4Setting, IRequest request, ClientConfig config)
        {
			string V4SignatureVersion = "4";
            if (useSigV4Setting || request.UseSigV4 || config.SignatureVersion == V4SignatureVersion)
                return true;

            // do a cascading series of checks to try and arrive at whether we have
            // a recognisable region; this is required to use the AWS4 signer
            RegionEndpoint r = null;
            if (!string.IsNullOrEmpty(request.AuthenticationRegion))
                r = RegionEndpoint.GetBySystemName(request.AuthenticationRegion);

            if (r == null && !string.IsNullOrEmpty(config.ServiceURL))
            {
                var parsedRegion = AWSSDKUtils.DetermineRegion(config.ServiceURL);
                if (!string.IsNullOrEmpty(parsedRegion))
                    r = RegionEndpoint.GetBySystemName(parsedRegion);
            }

            if (r == null && config.RegionEndpoint != null)
                r = config.RegionEndpoint;

            if (r != null)
            {
                var endpoint = r.GetEndpointForService(config.RegionEndpointServiceName);
                if (endpoint != null && endpoint.SignatureVersionOverride == "4")
                    return true;
            }

            return false;
        }

        
        protected AbstractDISSigner SelectSigner(IRequest request, ClientConfig config)
        {
            return SelectSigner(this, useSigV4Setting: false, request: request, config: config);
        }

        protected AbstractDISSigner SelectSigner(AbstractDISSigner defaultSigner,bool useSigV4Setting, 
            IRequest request, ClientConfig config)
        {
            return defaultSigner;
        }

        protected ICredentials SanitizeCredentials(ICredentials credentials)
        {
            String accessKeyId = credentials.GetAccessKeyId();
            String secretKey = credentials.GetSecretKey();
            if (secretKey != null)
            {
                secretKey = secretKey.Trim();
            }

            if (accessKeyId != null)
            {
                accessKeyId = accessKeyId.Trim();
            }

            return new BasicCredentials(accessKeyId, secretKey);
        }

        /*
        protected Stream getBinaryRequestPayloadStream(IRequest request)
        {
            if (HttpUtils.UsePayloadForQueryParameters(request))
            {
                String encodedParameters = HttpUtils.EncodeParameters(request);
                return encodedParameters == null ? new ByteArrayInputStream(new byte[0]) : new ByteArrayInputStream(encodedParameters.getBytes(StringUtils.UTF8));
            }
            else
            {
                return this.getBinaryRequestPayloadStreamWithoutQueryParams(request);
            }
        }
        */
    }
}
