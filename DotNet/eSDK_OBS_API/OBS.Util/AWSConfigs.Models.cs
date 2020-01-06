/*******************************************************************************
 *  Copyright 2008-2014 OBS.com, Inc. or its affiliates. All Rights Reserved.
 *  Licensed under the Apache License, Version 2.0 (the "License"). You may not use
 *  this file except in compliance with the License. A copy of the License is located at
 *  or in the "license" file accompanying this file.
 *  This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
 *  CONDITIONS OF ANY KIND, either express or implied. See the License for the
 *  specific language governing permissions and limitations under the License.
 * *****************************************************************************
 *
 */
using System;
using System.Collections.Generic;

namespace OBS.Util
{
    /// <summary>
    /// Root AWS config
    /// </summary>
    internal partial class RootConfig
    {
        public LoggingConfig Logging { get; private set; }        
        public S3Config S3 { get; private set; }        
        public ProxyConfig Proxy { get; private set; }
        public string EndpointDefinition { get; set; }
        public string Region { get; set; }
        public string ProfileName { get; set; }
        public string ProfilesLocation { get; set; }

        public RegionEndpoint RegionEndpoint
        {
            get
            {
                if (string.IsNullOrEmpty(Region))
                    return null;
                return RegionEndpoint.GetBySystemName(Region);
            }
            set
            {
                if (value == null)
                    Region = null;
                else 
                    Region = value.SystemName;
            }
        }

        private const string _rootAwsSectionName = "aws";
        internal RootConfig()
        {
            Logging = new LoggingConfig();
            S3 = new S3Config();            
            Proxy = new ProxyConfig();

            EndpointDefinition = ObsConfigs._endpointDefinition;
            Region = ObsConfigs._obsRegion;
            ProfileName = ObsConfigs._obsProfileName;
            ProfilesLocation = ObsConfigs._obsAccountsLocation;

#if !WIN_RT && !WINDOWS_PHONE
            var root = ObsConfigs.GetSection<OBSSection>(_rootAwsSectionName);

            Logging.Configure(root.Logging);            
            S3.Configure(root.S3);            
            Proxy.Configure(root.Proxy);

            EndpointDefinition = Choose(EndpointDefinition, root.EndpointDefinition);
            Region = Choose(Region, root.Region);
            ProfileName = Choose(ProfileName, root.ProfileName);
            ProfilesLocation = Choose(ProfilesLocation, root.ProfilesLocation);
#endif
        }

        // If a is not null-or-empty, returns a; otherwise, returns b.
        private static string Choose(string a, string b)
        {
            return (string.IsNullOrEmpty(a) ? b : a);
        }
    }

    #region Basic sections

    /// <summary>
    /// S3 settings.
    /// </summary>
    public partial class S3Config
    {
        /// <summary>
        /// Configures if the S3 client should use Signature Version 4 signing with requests.
        /// By default, this setting is false, though Signature Version 4 may be used by default
        /// in some cases or with some regions. When the setting is true, Signature Version 4
        /// will be used for all requests.
        /// </summary>
        public bool UseSignatureVersion4 { get; set; }

        internal S3Config()
        {
            UseSignatureVersion4 = ObsConfigs._s3UseSignatureVersion4;
        }
    }


    /// <summary>
    /// Settings for configuring a proxy for the SDK to use.
    /// </summary>
    public partial class ProxyConfig
    {
        private System.Security.SecureString _pwd = new System.Security.SecureString();
        /// <summary>
        /// The host name or IP address of the proxy server.
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// The port number of the proxy.
        /// </summary>
        public int? Port { get; set; }

        /// <summary>
        /// The username to authenticate with the proxy server.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The password to authenticate with the proxy server.
        /// </summary>
        public string Password 
        { 
            get
            {
                return _pwd.ToString();
            }

            set 
            {
                if (!string.IsNullOrEmpty(value))
                {
                    foreach (char c in value)
                    {
                        _pwd.AppendChar(c);
                    }
                }
            } 
        }


        internal ProxyConfig()
        {
        }
    }

    /// <summary>
    /// Settings for logging in the SDK.
    /// </summary>
    public partial class LoggingConfig
    {
        private LoggingOptions _logTo;

        /// <summary>
        /// Logging destination.
        /// </summary>
        public LoggingOptions LogTo
        {
            get { return _logTo; }
            set
            {
                _logTo = value;
                ObsConfigs.OnPropertyChanged(ObsConfigs.LoggingDestinationProperty);
            }
        }
        /// <summary>
        /// When to log responses.
        /// </summary>
        public ResponseLoggingOption LogResponses { get; set; }
        /// <summary>
        /// Whether or not to log SDK metrics.
        /// </summary>
        public bool LogMetrics { get; set; }

        public LogMetricsFormatOption LogMetricsFormat { get; set; }

        /// <summary>
        /// A custom formatter added through Configuration
        /// </summary>
        public OBS.Runtime.IMetricsFormatter LogMetricsCustomFormatter { get; set; }

        internal LoggingConfig()
        {
            LogTo = ObsConfigs._logging;
            LogResponses = ObsConfigs._responseLogging;
            LogMetrics = ObsConfigs._logMetrics;
        }
    }

    #endregion
}
