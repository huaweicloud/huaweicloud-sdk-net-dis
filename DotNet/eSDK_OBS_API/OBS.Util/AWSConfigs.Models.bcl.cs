﻿/*******************************************************************************
 *  Copyright 2008-2014 OBS.com, Inc. or its affiliates. All Rights Reserved.
 *  Licensed under the Apache License, Version 2.0 (the "License"). You may not use
 *  this file except in compliance with the License. A copy of the License is located at
 *  or in the "license" file accompanying this file.
 *  This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
 *  CONDITIONS OF ANY KIND, either express or implied. See the License for the
 *  specific language governing permissions and limitations under the License.
 *
 */

using OBS.Runtime;
using System;
using System.Collections.Generic;

namespace OBS.Util
{
    /// <summary>
    /// Root AWS config
    /// </summary>
    internal partial class RootConfig
    {
    }

    #region Basic sections

    /// <summary>
    /// S3 settings.
    /// </summary>
    public partial class S3Config
    {
        internal void Configure(V4ClientSection section)
        {
            if (section.ElementInformation.IsPresent)
                UseSignatureVersion4 = section.UseSignatureVersion4.GetValueOrDefault(false);
        }
    }


    /// <summary>
    /// Settings for configuring a proxy for the SDK to use.
    /// </summary>
    public partial class ProxyConfig
    {
        internal void Configure(ProxySection section)
        {
            if (section.ElementInformation.IsPresent)
            {
                Host = section.Host;
                Port = section.Port;
                Username = section.Username;
                Password = section.Password;
            }
        }
    }

    /// <summary>
    /// Settings for logging in the SDK.
    /// </summary>
    public partial class LoggingConfig
    {
        internal void Configure(LoggingSection section)
        {
            if (section.ElementInformation.IsPresent)
            {
                LogTo = section.LogTo;
                LogResponses = section.LogResponses;
                LogMetrics = section.LogMetrics.GetValueOrDefault(false);
                LogMetricsFormat = section.LogMetricsFormat;
                if (section.LogMetricsCustomFormatter != null
                    && typeof(IMetricsFormatter).IsAssignableFrom(section.LogMetricsCustomFormatter))
                {
                    LogMetricsCustomFormatter = Activator.CreateInstance(section.LogMetricsCustomFormatter) as IMetricsFormatter;
                }
            }
        }
    }

    #endregion
}
