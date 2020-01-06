/*******************************************************************************
 *  Copyright 2008-2013 OBS.com, Inc. or its affiliates. All Rights Reserved.
 *  Licensed under the Apache License, Version 2.0 (the "License"). You may not use
 *  this file except in compliance with the License. A copy of the License is located at
 *
 *  or in the "license" file accompanying this file.
 *  This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
 *  CONDITIONS OF ANY KIND, either express or implied. See the License for the
 *  specific language governing permissions and limitations under the License.
 * *****************************************************************************
 *  Obs API for .NET
 *
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;

#if !WIN_RT
using System.Configuration;
#endif

using OBS.Util;
using OBS.Runtime.Internal.Util;

namespace OBS
{
    /// <summary>
    /// Configuration options that apply to the entire SDK.
    /// 
    /// These settings can be configured through app.config or web.config.
    /// Below is a full sample configuration that illustrates all the possible options.
    /// <code>
    /// &lt;configSections&gt;
    ///   &lt;section name="OBS" type="OBS.OBSSection, OBSSDK"/&gt;
    /// &lt;/configSections&gt;
    /// &lt;OBS region="us-west-2"&gt;
    ///   &lt;logging logTo="Log4Net, SystemDiagnostics" logResponses="Always" logMetrics="true" /&gt;
    ///   &lt;s3 useSignatureVersion4="true" /&gt;
    ///   &lt;ec2 useSignatureVersion4="false" /&gt;
    ///   &lt;proxy host="localhost" port="8888" username="1" password="1" /&gt;
    ///   
    ///   &lt;dynamoDB&gt;
    ///     &lt;dynamoDBContext tableNamePrefix="Prod-"&gt;
    /// 
    ///       &lt;tableAliases&gt;
    ///         &lt;alias fromTable="FakeTable" toTable="People" /&gt;
    ///         &lt;alias fromTable="Persons" toTable="People" /&gt;
    ///       &lt;/tableAliases&gt;
    /// 
    ///       &lt;mappings&gt;
    ///         &lt;map type="Sample.Tests.Author, SampleDLL" targetTable="People" /&gt;
    ///         &lt;map type="Sample.Tests.Editor, SampleDLL" targetTable="People"&gt;
    ///           &lt;property name="FullName" attribute="Name" /&gt;
    ///           &lt;property name="EmployeeId" attribute="Id" /&gt;
    ///           &lt;property name="ComplexData" converter="Sample.Tests.ComplexDataConverter, SampleDLL" /&gt;
    ///           &lt;property name="Version" version="true" /&gt;
    ///           &lt;property name="Password" ignore="true" /&gt;
    ///         &lt;/map&gt;
    ///       &lt;/mappings&gt;
    /// 
    ///     &lt;/dynamoDBContext&gt;
    ///   &lt;/dynamoDB&gt;
    /// &lt;/OBS&gt;
    /// </code>
    /// </summary>
    public static partial class ObsConfigs
    {
        #region Private static members

        private static char[] validSeparators = new char[] { ' ', ',' };

        // Deprecated configs
        internal static string _obsRegion = GetConfig(OBSRegionKey);
        internal static LoggingOptions _logging = GetLoggingSetting();
        internal static ResponseLoggingOption _responseLogging = GetConfigEnum<ResponseLoggingOption>(ResponseLoggingKey);
        internal static bool _logMetrics = GetConfigBool(LogMetricsKey);
        internal static string _endpointDefinition = GetConfig(EndpointDefinitionKey);        
        //2015-12-7 w00322557默认采用V2鉴权
        internal static bool _s3UseSignatureVersion4 = false;
        internal static string _obsProfileName = GetConfig(OBSProfileNameKey);
        internal static string _obsAccountsLocation = GetConfig(OBSProfilesLocationKey);

        // New config section
        private static RootConfig _rootConfig = new RootConfig();

        #endregion

        #region Region

        /// <summary>
        /// Key for the OBSRegion property.
        /// <seealso cref="OBS.ObsConfigs.OBSRegion"/>
        /// </summary>
        public const string OBSRegionKey = "OBSRegion";

        /// <summary>
        /// Configures the default OBS region for clients which have not explicitly specified a region.
        /// Changes to this setting will only take effect for newly constructed instances of OBS clients.
        /// 
        /// This setting can be configured through the App.config. For example:
        /// <code>
        /// &lt;configSections&gt;
        ///   &lt;section name="OBS" type="OBS.OBSSection, OBSSDK"/&gt;
        /// &lt;/configSections&gt;
        /// &lt;OBS region="us-west-2" /&gt;
        /// </code>
        /// </summary>
        public static string OBSRegion
        {
            get { return _rootConfig.Region; }
            set { _rootConfig.Region = value; }
        }

        #endregion

        #region Account Name

        /// <summary>
        /// Key for the OBSProfileName property.
        /// <seealso cref="OBS.ObsConfigs.OBSProfileName"/>
        /// </summary>
        public const string OBSProfileNameKey = "OBSProfileName";

        /// <summary>
        /// Profile name for stored OBS credentials that will be used to make service calls.
        /// Changes to this setting will only take effect in newly-constructed clients.
        /// <para>
        /// To reference the account from an application's App.config or Web.config use the OBSProfileName setting.
        /// <code>
        /// &lt;?xml version="1.0" encoding="utf-8" ?&gt;
        /// &lt;configuration&gt;
        ///     &lt;appSettings&gt;
        ///         &lt;add key="OBSProfileName" value="development"/&gt;
        ///     &lt;/appSettings&gt;
        /// &lt;/configuration&gt;
        /// </code>
        /// </para>
        /// </summary>
        public static string OBSProfileName
        {
            get { return _rootConfig.ProfileName; }
            set { _rootConfig.ProfileName = value; }
        }

        #endregion

        #region Accounts Location

        /// <summary>
        /// Key for the OBSProfilesLocation property.
        /// <seealso cref="OBS.ObsConfigs.LogMetrics"/>
        /// </summary>
        public const string OBSProfilesLocationKey = "OBSProfilesLocation";

        /// <summary>
        /// Location of the credentials file shared with other OBS SDKs.
        /// By default, the credentials file is stored in the .OBS directory in the current user's home directory.
        /// 
        /// Changes to this setting will only take effect in newly-constructed clients.
        /// <para>
        /// To reference the profile from an application's App.config or Web.config use the OBSProfileName setting.
        /// <code>
        /// &lt;?xml version="1.0" encoding="utf-8" ?&gt;
        /// &lt;configuration&gt;
        ///     &lt;appSettings&gt;
        ///         &lt;add key="OBSProfilesLocation" value="<config path>"/&gt;
        ///     &lt;/appSettings&gt;
        /// &lt;/configuration&gt;
        /// </code>
        /// </para>
        /// </summary>
        public static string OBSProfilesLocation
        {
            get { return _rootConfig.ProfilesLocation; }
            set { _rootConfig.ProfilesLocation = value; }
        }

        #endregion

        #region Logging

        /// <summary>
        /// Key for the Logging property.
        /// <seealso cref="OBS.ObsConfigs.Logging"/>
        /// </summary>
        public const string LoggingKey = "OBSLogging";

        /// <summary>
        /// Configures how the SDK should log events, if at all.
        /// Changes to this setting will only take effect in newly-constructed clients.
        /// 
        /// The setting can be configured through App.config, for example:
        /// <code>
        /// &lt;appSettings&gt;
        ///   &lt;add key="OBSLogging" value="log4net"/&gt;
        /// &lt;/appSettings&gt;
        /// </code>
        /// </summary>
        [Obsolete("This property is obsolete. Use LoggingConfig.LogTo instead.")]
        public static LoggingOptions Logging
        {
            get { return _rootConfig.Logging.LogTo; }
            set { _rootConfig.Logging.LogTo = value; }
        }

        private static LoggingOptions GetLoggingSetting()
        {
            string value = GetConfig(LoggingKey);
            if (string.IsNullOrEmpty(value))
                return LoggingOptions.None;

            string[] settings = value.Split(validSeparators, StringSplitOptions.RemoveEmptyEntries);
            if (settings == null || settings.Length == 0)
                return LoggingOptions.None;

            LoggingOptions totalSetting = LoggingOptions.None;
            foreach (string setting in settings)
            {
                LoggingOptions l = ParseEnum<LoggingOptions>(setting);
                totalSetting |= l;
            }
            return totalSetting;
        }

        #endregion

        #region Response Logging

        /// <summary>
        /// Key for the ResponseLogging property.
        /// 
        /// <seealso cref="OBS.ObsConfigs.ResponseLogging"/>
        /// </summary>
        public const string ResponseLoggingKey = "OBSResponseLogging";

        /// <summary>
        /// Configures when the SDK should log service responses.
        /// Changes to this setting will take effect immediately.
        /// 
        /// The setting can be configured through App.config, for example:
        /// <code>
        /// &lt;appSettings&gt;
        ///   &lt;add key="OBSResponseLogging" value="OnError"/&gt;
        /// &lt;/appSettings&gt;
        /// </code>
        /// </summary>
        [Obsolete("This property is obsolete. Use LoggingConfig.LogResponses instead.")]
        public static ResponseLoggingOption ResponseLogging
        {
            get { return _rootConfig.Logging.LogResponses; }
            set { _rootConfig.Logging.LogResponses = value; }
        }

        #endregion

        #region Log Metrics

        /// <summary>
        /// Key for the LogMetrics property.
        /// <seealso cref="OBS.ObsConfigs.LogMetrics"/>
        /// </summary>
        public const string LogMetricsKey = "OBSLogMetrics";

        /// <summary>
        /// Configures if the SDK should log performance metrics.
        /// This setting configures the default LogMetrics property for all clients/configs.
        /// Changes to this setting will only take effect in newly-constructed clients.
        /// 
        /// The setting can be configured through App.config, for example:
        /// <code>
        /// &lt;appSettings&gt;
        ///   &lt;add key="OBSLogMetrics" value="true"/&gt;
        /// &lt;/appSettings&gt;
        /// </code>
        /// </summary>
        [Obsolete("This property is obsolete. Use LoggingConfig.LogMetrics instead.")]
        public static bool LogMetrics
        {
            get { return _rootConfig.Logging.LogMetrics; }
            set { _rootConfig.Logging.LogMetrics = value; }
        }

        #endregion

        #region Endpoint Configuration

        /// <summary>
        /// Key for the EndpointDefinition property.
        /// <seealso cref="OBS.ObsConfigs.LogMetrics"/>
        /// </summary>
        public const string EndpointDefinitionKey = "OBSEndpointDefinition";

        /// <summary>
        /// Configures if the SDK should use a custom configuration file that defines the regions and endpoints.
        /// <code>
        /// &lt;configSections&gt;
        ///   &lt;section name="OBS" type="OBS.OBSSection, OBSSDK"/&gt;
        /// &lt;/configSections&gt;
        /// &lt;OBS endpointDefinition="<endpints xml>" /&gt;
        /// </code>
        /// </summary>
        public static string EndpointDefinition
        {
            get { return _rootConfig.EndpointDefinition; }
            set { _rootConfig.EndpointDefinition = value; }
        }

        #endregion

        #region S3 SignatureV4

        /// <summary>
        /// Key for the S3UseSignatureVersion4 property.
        /// <seealso cref="OBS.ObsConfigs.S3UseSignatureVersion4"/>
        /// </summary>
        public const string S3UseSignatureVersion4Key = "OBS.S3.UseSignatureVersion4";

        /// <summary>
        /// Configures if the S3 client should use Signature Version 4 signing with requests.
        /// By default, this setting is false, though Signature Version 4 may be used by default
        /// in some cases or with some regions. When the setting is true, Signature Version 4
        /// will be used for all requests.
        /// 
        /// Changes to this setting will only take effect in newly-constructed clients.
        /// 
        /// The setting can be configured through App.config, for example:
        /// <code>
        /// &lt;appSettings&gt;
        ///   &lt;add key="OBS.S3.UseSignatureVersion4" value="true"/&gt;
        /// &lt;/appSettings&gt;
        /// </code>
        /// </summary>
        [Obsolete("This property is obsolete. Use S3Config.UseSignatureVersion4 instead.")]
        public static bool S3UseSignatureVersion4
        {
            get { return _rootConfig.S3.UseSignatureVersion4; }
            set { _rootConfig.S3.UseSignatureVersion4 = value; }
        }

        #endregion

       
        #region OBS Config Sections

        /// <summary>
        /// Configuration for the Logging section of OBS configuration.
        /// Changes to some settings may not take effect until a new client is constructed.
        /// 
        /// Example section:
        /// <code>
        /// &lt;configSections&gt;
        ///   &lt;section name="OBS" type="OBS.OBSSection, OBSSDK"/&gt;
        /// &lt;/configSections&gt;
        /// &lt;OBS&gt;
        ///   &lt;logging logTo="Log4Net, SystemDiagnostics" logResponses="Always" logMetrics="true" /&gt;
        /// &lt;/OBS&gt;
        /// </code>
        /// </summary>
        public static LoggingConfig LoggingConfig { get { return _rootConfig.Logging; } }

        /// <summary>
        /// Configuration for the S3 section of OBS configuration.
        /// Changes to some settings may not take effect until a new client is constructed.
        /// 
        /// Example section:
        /// <code>
        /// &lt;configSections&gt;
        ///   &lt;section name="OBS" type="OBS.OBSSection, OBSSDK"/&gt;
        /// &lt;/configSections&gt;
        /// &lt;OBS&gt;
        ///   &lt;s3 useSignatureVersion4="true" /&gt;
        /// &lt;/OBS&gt;
        /// </code>
        /// </summary>
        public static S3Config S3Config { get { return _rootConfig.S3; } }
        
        /// <summary>
        /// Configuration for the Proxy section of OBS configuration.
        /// Changes to some settings may not take effect until a new client is constructed.
        /// 
        /// Example section:
        /// <code>
        /// &lt;configSections&gt;
        ///   &lt;section name="OBS" type="OBS.OBSSection, OBSSDK"/&gt;
        /// &lt;/configSections&gt;
        /// &lt;OBS&gt;
        ///   &lt;proxy host="localhost" port="8888" username="1" password="1" /&gt;
        /// &lt;/OBS&gt;
        /// </code>
        /// </summary>
        public static ProxyConfig ProxyConfig { get { return _rootConfig.Proxy; } }

        /// <summary>
        /// Configuration for the region endpoint section of OBS configuration.
        /// Changes may not take effect until a new client is constructed.
        /// 
        /// Example section:
        /// <code>
        /// &lt;configSections&gt;
        ///   &lt;section name="OBS" type="OBS.OBSSection, OBSSDK"/&gt;
        /// &lt;/configSections&gt;
        /// &lt;OBS region="us-west-2" /&gt;
        /// </code>
        /// </summary>
        public static RegionEndpoint RegionEndpoint
        {
            get { return _rootConfig.RegionEndpoint; }
            set { _rootConfig.RegionEndpoint = value; }
        }

        #endregion

        #region Internal members

        internal static event PropertyChangedEventHandler PropertyChanged;
        internal const string LoggingDestinationProperty = "LogTo";
        internal static void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(null, new PropertyChangedEventArgs(name));
            }
        }

        #endregion

        #region Private general methods

        private static bool GetConfigBool(string name)
        {
            string value = GetConfig(name);
            bool result;
            if (bool.TryParse(value, out result))
                return result;
            return default(bool);
        }

        private static T GetConfigEnum<T>(string name)
        {
            var type = TypeFactory.GetTypeInfo(typeof(T));
            if (!type.IsEnum) throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "Type {0} must be enum", type.FullName));

            string value = GetConfig(name);
            if (string.IsNullOrEmpty(value))
                return default(T);
            T result = ParseEnum<T>(value);
            return result;
        }

        private static T ParseEnum<T>(string value)
        {
            T t;
            if (TryParseEnum<T>(value, out t))
                return t;
            Type type = typeof(T);
            string messageFormat = "Unable to parse value {0} as enum of type {1}. Valid values are: {2}";
            string enumNames = string.Join(", ", Enum.GetNames(type));
            throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, messageFormat, value, type.FullName, enumNames));
        }

        private static bool TryParseEnum<T>(string value, out T result)
        {
            result = default(T);

            if (string.IsNullOrEmpty(value))
                return false;

            try
            {
                T t = (T)Enum.Parse(typeof(T), value, true);
                result = t;
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        #endregion
    }

    /// <summary>
    /// Logging options.
    /// Can be combined to enable multiple loggers.
    /// </summary>
    [Flags]
    public enum LoggingOptions
    {
        /// <summary>
        /// No logging
        /// </summary>
        None = 0,
        
        /// <summary>
        /// Log using log4net
        /// </summary>
        Log4Net = 1,
        
        /// <summary>
        /// Log using System.Diagnostics
        /// </summary>
        SystemDiagnostics = 2
    }

    /// <summary>
    /// Response logging option.
    /// </summary>
    public enum ResponseLoggingOption
    {
        /// <summary>
        /// Never log service response
        /// </summary>
        Never = 0,

        /// <summary>
        /// Only log service response when there's an error
        /// </summary>
        OnError = 1,

        /// <summary>
        /// Always log service response
        /// </summary>
        Always = 2
    }

    /// <summary>
    /// Format for metrics data in the logs
    /// </summary>
    public enum LogMetricsFormatOption
    {
        /// <summary>
        /// Emit metrics in human-readable format
        /// </summary>
        Standard = 0,
        /// <summary>
        /// Emit metrics as JSON data
        /// </summary>
        JSON = 1
    }
}
