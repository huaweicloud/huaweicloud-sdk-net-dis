﻿/*
 * Copyright 2010-2014 OBS.com, Inc. or its affiliates. All Rights Reserved.
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
using System.Linq;
using System.Text;
using OBS.Runtime;
using OBS.S3.Model;
using System.IO;
using OBS.S3.Util;
using OBS.Runtime.Internal;
using OBS.S3;
using System.Text.RegularExpressions;
using OBS.Util;
using System.Globalization;

namespace OBS.S3.Internal
{
    public class ObsS3PostMarshallHandler : GenericHandler
    {
        protected override void PreInvoke(IExecutionContext executionContext)
        {
            ProcessRequestHandlers(executionContext);
        }

        public void ProcessRequestHandlers(IExecutionContext executionContext)
        {
            var request = executionContext.RequestContext.Request;
            var config = executionContext.RequestContext.ClientConfig;

            var bucketName = GetBucketName(request.ResourcePath);
            if (string.IsNullOrEmpty(bucketName))
                return;

            var s3Config = config as ObsS3Config;
            if (s3Config == null)
            {
                return;
            }
            // If path style is not forced and the bucket name is DNS
            // compatible modify the endpoint to use virtual host style
            // addressing
            var bucketIsDnsCompatible = IsDnsCompatibleBucketName(bucketName);
            var ub = new UriBuilder(EndpointResolver.DetermineEndpoint(s3Config, request));
            var isHttp = string.Equals(ub.Scheme, "http", StringComparison.OrdinalIgnoreCase);

            if (!s3Config.ForcePathStyle && bucketIsDnsCompatible)
            {
                // If using HTTPS, bucketName cannot contain a period
                if (isHttp || bucketName.IndexOf('.') < 0)
                {
                    // Add bucket to host
                    ub.Host = string.Concat(bucketName, ".", ub.Host);
                    request.Endpoint = ub.Uri;

                    // Remove bucket from resource path but retain in canonical resource
                    // prefix, so it gets included when we sign the request later
                    var resourcePath = request.ResourcePath;
                    var canonicalBucketName = string.Concat("/", bucketName);
                    if (resourcePath.IndexOf(canonicalBucketName, StringComparison.Ordinal) == 0)
                        resourcePath = resourcePath.Substring(canonicalBucketName.Length);
                    request.ResourcePath = resourcePath;

                    request.CanonicalResourcePrefix = canonicalBucketName;
                }
            }

            // Some parameters should not be sent over HTTP, just HTTPS
            if (isHttp)
            {
                ValidateHttpsOnlyHeaders(request);
            }
        }

        private static void ValidateHttpsOnlyHeaders(IRequest request)
        {
            var foundHttpsOnlyHeaders = request.Headers
                .Where(kvp => !string.IsNullOrEmpty(kvp.Value) && httpsOnlyHeaders.Contains(kvp.Key))
                .Select(kvp => kvp.Key)
                .ToArray();
            if (foundHttpsOnlyHeaders.Length > 0)
            {
                string message = string.Format(CultureInfo.InvariantCulture,
                    "Request contains headers which can only be transmitted over HTTPS: {0}",
                    string.Join(", ", foundHttpsOnlyHeaders));
                throw new ObsClientException(message);
            }
        }

        private static HashSet<string> httpsOnlyHeaders = new HashSet<string>
        {
            HeaderKeys.XAmzSSECustomerKeyHeader,
            HeaderKeys.XAmzServerSideEncryptionAwsKmsKeyIdHeader
        };

        private static char[] separators = new char[] { '/', '?' };
        // Gets the bucket name from resource path
        internal static string GetBucketName(string resourcePath)
        {
            resourcePath = resourcePath.Trim().Trim(separators);
            var parts = resourcePath.Split(separators, 2);
            var bucketName = parts[0];
            return bucketName;
        }

#if BCL
        private static Regex bucketValidationRegex = new Regex(@"^[A-Za-z0-9._\-]+$", RegexOptions.Compiled);
#else
        private static Regex bucketValidationRegex = new Regex(@"^[A-Za-z0-9._\-]+$");
#endif
        // Returns true if the bucket name is valid
        public static bool IsValidBucketName(string bucketName)
        {
            // Check if bucket is null/empty string
            if (string.IsNullOrEmpty(bucketName))
                return false;

            // Check if the bucket name is between 3 and 255 characters
            if (bucketName.Length < 3 || bucketName.Length > 255)
                return false;

            // Check if the bucket contains a newline character
            if (bucketName.IndexOf('\n') >= 0)
                return false;

            // Check if bucket only contains:
            //  uppercase letters, lowercase letters, numbers
            //  periods (.), underscores (_), dashes (-)
            if (!bucketValidationRegex.IsMatch(bucketName))
                return false;

            return true;
        }

#if BCL
        private static Regex dnsValidationRegex1 = new Regex(@"^[a-z0-9][a-z0-9.-]+[a-z0-9]$", RegexOptions.Compiled);
        private static Regex dnsValidationRegex2 = new Regex("(\\d+\\.){3}\\d+", RegexOptions.Compiled);
#else
        private static Regex dnsValidationRegex1 = new Regex(@"^[a-z0-9][a-z0-9.-]+[a-z0-9]$");
        private static Regex dnsValidationRegex2 = new Regex("(\\d+\\.){3}\\d+");
#endif
        private static string[] invalidPatterns = new string[] { "..", "-.", ".-" };
        // Returns true if the given bucket name is DNS compatible
        // DNS compatible bucket names may be accessed like:
        //     http://dns.compat.bucket.name.s3.amazonaws.com/
        // Whereas non-dns compatible bucket names must place the bucket name in the url path, like:
        //     http://s3.amazonaws.com/dns_incompat_bucket_name/
        public static bool IsDnsCompatibleBucketName(string bucketName)
        {
            // Check basic validation
            if (!IsValidBucketName(bucketName))
                return false;

            // Bucket names should between 3 and 63 characters
            if (bucketName.Length > 63)
                return false;

            // Bucket names must only contain lowercase letters, numbers, dots, and dashes
            // and must start and end with a lowercase letter or a number
            if (!dnsValidationRegex1.IsMatch(bucketName))
                return false;

            // Bucket names should not be formatted like an IP address (e.g., 192.168.5.4)
            if (dnsValidationRegex2.IsMatch(bucketName))
                return false;

            // Bucket names cannot contain two adjacent periods or dashes next to periods
            if (StringContainsAny(bucketName, invalidPatterns, StringComparison.Ordinal))
                return false;

            return true;
        }

        // Returns true if string toCheck contains any of strings in values
        private static bool StringContainsAny(string toCheck, string[] values, StringComparison stringComparison)
        {
            foreach (var value in values)
            {
                if (toCheck.IndexOf(value, stringComparison) >= 0)
                    return true;
            }
            return false;
        }
    }
}
