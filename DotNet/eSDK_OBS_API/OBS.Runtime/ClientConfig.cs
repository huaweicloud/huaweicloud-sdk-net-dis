﻿ /*
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
using System.Collections.Specialized;
using System.Net;
using System.Text;

using OBS.Runtime.Internal.Auth;
using OBS.Util;
using System.Globalization;
using System.Text.RegularExpressions;

namespace OBS.Runtime
{
    /// <summary>
    /// This class is the base class of all the configurations settings to connect
    /// to a service.
    /// </summary>
    public abstract partial class ClientConfig
    {
        // Represents infinite timeout. http://msdn.microsoft.com/en-us/library/system.threading.timeout.infinite.aspx
        internal static readonly TimeSpan InfiniteTimeout = TimeSpan.FromMilliseconds(-1);

        // Represents max timeout.
        internal static readonly TimeSpan MaxTimeout = TimeSpan.FromMilliseconds(int.MaxValue);

#if !BCL
        private RegionEndpoint regionEndpoint = null;
#else
        private RegionEndpoint regionEndpoint = GetDefaultRegionEndpoint();
#endif

        private bool useHttp = false;
        private string serviceURL = null;
        private string authRegion = null;
        private string authServiceName = null;
        private string userAgent = OBS.Util.AWSSDKUtils.SDKUserAgent;
        private string signatureVersion = "2";

        //2015-5-27 w00322557 修改默认算法
        private SigningAlgorithm signatureMethod = SigningAlgorithm.HmacSHA1;

        private int maxErrorRetry = 4;
        private bool readEntireResponse = false;
        private bool logResponse = false;
        private int bufferSize = AWSSDKUtils.DefaultBufferSize;
        private long progressUpdateInterval = AWSSDKUtils.DefaultProgressUpdateInterval;
        private bool resignRetries = false;
        private ICredentials proxyCredentials;
        private bool logMetrics = ObsConfigs.LoggingConfig.LogMetrics;
        private bool disableLogging = false;
        private TimeSpan? timeout = null;
        private bool allowAutoRedirect = true;

        private bool _validateCertificate = false;
        /// <summary>
        /// Get and set whether validate server certificate by CA certificate.
        /// The CA certificate should be install in root trust store.
        /// </summary>
        public bool ValidateCertificate
        {
            get { return this._validateCertificate; }
            set { this._validateCertificate = value; }
        }

        /// <summary>
        /// Gets Service Version
        /// </summary>
        public abstract string ServiceVersion
        {
            get;
        }

        /// <summary>
        /// Gets and sets of the signatureMethod property.
        /// </summary>
        public SigningAlgorithm SignatureMethod
        {
            get { return this.signatureMethod; }
            set { this.signatureMethod = value; }
        }

        /// <summary>
        /// Gets and sets of the SignatureVersion property.
        /// </summary>
        public string SignatureVersion
        {
            get { return this.signatureVersion; }
            set { this.signatureVersion = value; }
        }

        /// <summary>
        /// Gets and sets of the UserAgent property.
        /// </summary>
        public string UserAgent
        {
            get { return this.userAgent; }
            set { this.userAgent = value; }
        }

        /// <summary>
        /// Gets and sets the RegionEndpoint property.  The region constant to use that 
        /// determines the endpoint to use.  If this is not set
        /// then the client will fallback to the value of ServiceURL.
        /// </summary>
        public RegionEndpoint RegionEndpoint
        {
            get
            {
                return regionEndpoint;
            }
            set
            {
                this.serviceURL = null;
                this.regionEndpoint = value;
                if (this.regionEndpoint != null)
                {
                    var endpoint = this.regionEndpoint.GetEndpointForService(RegionEndpointServiceName);
                    if (endpoint != null && endpoint.SignatureVersionOverride != null)
                        this.SignatureVersion = endpoint.SignatureVersionOverride;
                }
            }
        }

        /// <summary>
        /// The constant used to lookup in the region hash the endpoint.
        /// </summary>
        public abstract string RegionEndpointServiceName
        {
            get;
        }

        /// <summary>
        /// Gets and sets of the ServiceURL property.
        /// This is an optional property; change it
        /// only if you want to try a different service
        /// endpoint.
        /// </summary>
        public string ServiceURL
        {
            get { return this.serviceURL; }
            set 
            {
                this.regionEndpoint = null;
                this.serviceURL = value; 
            }
        }

        /// <summary>
        /// Gets and sets the UseHttp.
        /// If this property is set to true, the client attempts
        /// to use HTTP protocol, if the target endpoint supports it.
        /// By default, this property is set to false.
        /// </summary>
        public bool UseHttp
        {
            get { return this.useHttp; }
            set { this.useHttp = value; }
        }

        public string DetermineServiceURL()
        {
            string url;
            if (this.ServiceURL != null)
            {
                url = this.ServiceURL;
            }
            else
            {
                url = GetUrl(this.RegionEndpoint, this.RegionEndpointServiceName, this.UseHttp);
            }

            return url;
        }

        internal static string GetUrl(RegionEndpoint regionEndpoint, string regionEndpointServiceName, bool useHttp)
        {
            var endpoint = regionEndpoint.GetEndpointForService(regionEndpointServiceName);
            bool shouldUseHttp;
            if (endpoint.HTTP && endpoint.HTTPS)    // if both are supported, go with user preference
                shouldUseHttp = useHttp;
            else if (endpoint.HTTPS)                // if HTTPS is supported, go with HTTPS
                shouldUseHttp = false;
            else                                    // if HTTP is supported, go with HTTP
                shouldUseHttp = true;

            string url = new Uri(string.Format(CultureInfo.InvariantCulture, "{0}{1}", shouldUseHttp ? "http://" : "https://", endpoint.Hostname)).AbsoluteUri;
            return url;
        }

        /// <summary>
        /// Gets and sets the AuthenticationRegion property.
        /// Used in AWS4 request signing, this is an optional property; 
        /// change it only if the region cannot be determined from the 
        /// service endpoint.
        /// </summary>
        public string AuthenticationRegion
        {
            get { return this.authRegion; }
            set { this.authRegion = value; }
        }

        /// <summary>
        /// Gets and sets the AuthenticationServiceName property.
        /// Used in AWS4 request signing, this is the short-form
        /// name of the service being called.
        /// </summary>
        public string AuthenticationServiceName
        {
            get { return this.authServiceName; }
            set { this.authServiceName = value; }
        }

        /// <summary>
        /// Gets and sets of the MaxErrorRetry property.
        /// </summary>
        public int MaxErrorRetry
        {
            get { return this.maxErrorRetry; }
            set { this.maxErrorRetry = value; }
        }

        /// <summary>
        /// Gets and sets the LogResponse.
        /// If this property is set to true, the service response
        /// is read in its entirety and logged.
        /// </summary>
        public bool LogResponse
        {
            get { return this.logResponse; }
            set { this.logResponse = value; }
        }

        /// <summary>
        /// Gets and sets the ReadEntireResponse.
        /// If this property is set to true, the service response
        /// is read in its entirety before being processed.
        /// </summary>
        public bool ReadEntireResponse
        {
            get { return this.readEntireResponse; }
            set { this.readEntireResponse = value; }
        }

        /// <summary>
        /// Gets and Sets the BufferSize property.
        /// The BufferSize controls the buffer used to read in from input streams and write 
        /// out to the request.
        /// </summary>
        public int BufferSize
        {
            get { return this.bufferSize; }
            set { this.bufferSize = value; }
        }

        /// <summary>
        /// <para>
        /// Gets or sets the interval at which progress update events are raised
        /// for upload operations. By default, the progress update events are 
        /// raised at every 100KB of data transferred. 
        /// </para>
        /// <para>
        /// If the value of this property is set less than ClientConfig.BufferSize, 
        /// progress updates events will be raised at the interval specified by ClientConfig.BufferSize.
        /// </para>
        /// </summary>
        public long ProgressUpdateInterval
        {
            get { return progressUpdateInterval; }
            set { progressUpdateInterval = value; }
        }


        /// <summary>
        /// Flag on whether to resign requests on retry or not.
        /// </summary>
        public bool ResignRetries
        {
            get { return this.resignRetries; }
            set { this.resignRetries = value; }
        }

        /// <summary>
        /// This flag controls if .NET HTTP infrastructure should follow redirection
        ///  responses (e.g. HTTP 307 - temporary redirect).
        /// </summary>
        public bool AllowAutoRedirect
        {
            get
            {
                return this.allowAutoRedirect;
            }
            set
            {
                this.allowAutoRedirect = value;
            }
        }

        /// <summary>
        /// Flag on whether to log metrics for service calls.
        /// 
        /// This can be set in the application's configs, as below:
        /// <code>
        /// &lt;?xml version="1.0" encoding="utf-8" ?&gt;
        /// &lt;configuration&gt;
        ///     &lt;appSettings&gt;
        ///         &lt;add key="AWSLogMetrics" value"true"/&gt;
        ///     &lt;/appSettings&gt;
        /// &lt;/configuration&gt;
        /// </code>
        /// </summary>
        public bool LogMetrics
        {
            get { return this.logMetrics; }
            set { this.logMetrics = value; }
        }

        /// <summary>
        /// Flag on whether to completely disable logging for this client or not.
        /// </summary>
        internal bool DisableLogging
        {
            get { return this.disableLogging; }
            set { this.disableLogging = value; }
        }

        /// <summary>
        /// Credentials to use with a proxy.
        /// </summary>
        public ICredentials ProxyCredentials
        {
            get 
            {
                if(this.proxyCredentials == null &&
                    (!string.IsNullOrEmpty(ObsConfigs.ProxyConfig.Username) ||
                    !string.IsNullOrEmpty(ObsConfigs.ProxyConfig.Password)))
                {
                    return new NetworkCredential(ObsConfigs.ProxyConfig.Username, ObsConfigs.ProxyConfig.Password ?? string.Empty);
                }
                return this.proxyCredentials; 
            }
            set { this.proxyCredentials = value; }
        }

        #region Constructor 
        public ClientConfig()
        {
            Initialize();
        }

        #endregion

        protected virtual void Initialize()
        {
        }

        /// <summary>
        /// Overrides the default request timeout value.
        /// </summary>
        /// <remarks>
        /// <para>
        /// If the value is set, the value is assigned to the Timeout property of the HTTPWebRequest/HttpClient object used
        /// to send requests.
        /// </para>
        /// <para>
        /// Please specify a timeout value only if the operation will not complete within the default intervals
        /// specified for an HttpWebRequest/HttpClient.
        /// </para>
        /// </remarks>
        /// <exception cref="System.ArgumentNullException">The timeout specified is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">The timeout specified is less than or equal to zero and is not Infinite.</exception>
        /// <seealso cref="P:System.Net.HttpWebRequest.Timeout"/>
        /// <seealso cref="P:System.Net.Http.HttpClient.Timeout"/>
        public TimeSpan? Timeout
        {
            get { return this.timeout; }
            set
            {
                ValidateTimeout(value);
                this.timeout = value;
            }
        }

        internal void SetUseNagleIfAvailable(bool useNagle)
        {
#if BCL
            this.UseNagleAlgorithm = useNagle;                
#endif
        }
        
        /// <summary>
        /// Performs validation on this config object.
        /// Throws exception if any of the required values are missing/invalid.
        /// </summary>
        internal virtual void Validate()
        {
            if (RegionEndpoint == null && string.IsNullOrEmpty(this.ServiceURL))
                throw new ObsClientException("No RegionEndpoint or ServiceURL configured");
        }

        internal static void ValidateTimeout(TimeSpan? timeout)
        {
            if (!timeout.HasValue)
            {
                throw new ArgumentNullException("timeout");
            }

            if (timeout != InfiniteTimeout && (timeout <= TimeSpan.Zero || timeout > MaxTimeout))
            {
                throw new ArgumentOutOfRangeException("timeout");
            }
        }

        /// <summary>
        /// Returns the request timeout value if its value is set, 
        /// else returns client timeout value.
        /// </summary>        
        internal static TimeSpan? GetTimeoutValue(TimeSpan? clientTimeout, TimeSpan? requestTimeout)
        {
            return requestTimeout.HasValue ? requestTimeout
                : (clientTimeout.HasValue ? clientTimeout : null);
        }
    }
}
