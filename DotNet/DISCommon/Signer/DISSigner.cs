using eSDK_OBS_API.OBS.Util; 
﻿/*
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
using System.Text;

using OBS.Util;
using System.Globalization;
using System.IO;
using Com.Bigdata.Dis.Sdk.DISCommon.Auth;
using Com.Bigdata.Dis.Sdk.DISCommon.DISWeb;
using Com.Bigdata.Dis.Sdk.DISCommon.Utils;
using OBS.Runtime;
using OBS.Runtime.Internal.Auth;
using OBS.Runtime.Internal.Util;
using IRequest = OBS.Runtime.Internal.IRequest;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Signer
{
    public class DISSigner : AbstractDISSigner
    {        
        private const string HTTPS_SCHEME = "AWS3-HTTPS";
        protected string serviceName;
        protected string regionName;

        public DISSigner(string serviceName, string regionName)
        {
            SetServiceName(serviceName);
            SetRegionName(regionName);
        }

        public void SetServiceName(string serviceName)
        {
            this.serviceName = serviceName;
        }

        public void SetRegionName(string regionName)
        {
            this.regionName = regionName;
        }

        private bool UseAws3Https { get; set; }

        public DISSigner(bool useAws3Https)
        {
            UseAws3Https = useAws3Https;
        }
        public DISSigner()
            : this(false)
        {
        }

        public override ClientProtocol Protocol
        {
            get { return ClientProtocol.RestProtocol; }
        }

        public override void Sign(IRequest request, string awsAccessKeyId, string awsSecretAccessKey)
        {
            //请求指标
            var metrics = new RequestMetrics();
            SignerRequestParamsEx signerParams = new SignerRequestParamsEx(request, this.regionName, this.serviceName, "SDK-HMAC-SHA256");
            SignHttp(request, metrics, awsAccessKeyId, awsSecretAccessKey, signerParams);
        }

        public override void Sign(IRequest request, ICredentials credentials)
        {
            var sanitizedCredentials  = this.SanitizeCredentials(credentials);
            var signerParams = new SignerRequestParamsEx(request, this.regionName, this.serviceName, "SDK-HMAC-SHA256");
            this.AddHostHeader(request);
            request.Headers.Add("X-Sdk-Date", signerParams.GetFormattedSigningDateTime());

            String contentSha256 = this.CalculateContentHash(request);
            if ("required".Equals(request.Headers["x-sdk-content-sha256"]))
            {
                request.Headers.Add("x-sdk-content-sha256", contentSha256);
            }

            var metrics = new RequestMetrics();
            SignHttp(request, metrics, sanitizedCredentials.GetAccessKeyId(), sanitizedCredentials.GetSecretKey(), signerParams);
        }


        protected String CalculateContentHash(IRequest request)
        {
            return request.ComputeContentStreamHash();
        }

        protected void AddHostHeader(IRequest request)
        {
            var endpoint = request.Endpoint;
            var hostHeaderBuilder = new StringBuilder(endpoint.Host);
            if (HttpUtils.IsUsingNonDefaultPort(endpoint))
            {
                hostHeaderBuilder.Append(":").Append(endpoint.Port);
            }

            request.Headers.Add("Host", hostHeaderBuilder.ToString());
        }
        

        /// <summary>
        /// Signs the specified request with the AWS3 signing protocol by using the
        /// AWS account credentials given in the method parameters.
        /// </summary>
        /// <param name="awsAccessKeyId">The AWS public key</param>
        /// <param name="awsSecretAccessKey">The AWS secret key used to sign the request in clear text</param>
        /// <param name="metrics">Request metrics</param>
        /// <param name="clientConfig">The configuration that specifies which hashing algorithm to use</param>
        /// <param name="request">The request to have the signature compute for</param>
        /// <exception cref="OBS.Runtime.SignatureException">If any problems are encountered while signing the request</exception>
        public override void Sign(IRequest request, ClientConfig clientConfig, RequestMetrics metrics, string awsAccessKeyId, string awsSecretAccessKey) 
        {
            var signer = SelectSigner(request, clientConfig);
            var useV4 = signer is AWS4Signer;

            if (useV4)
                signer.Sign(request, clientConfig, metrics, awsAccessKeyId, awsSecretAccessKey);
            else
            {
                if (UseAws3Https)
                {
                    SignHttps(request, clientConfig, metrics, awsAccessKeyId, awsSecretAccessKey);
                }
                else
                {
                    SignHttp(request, metrics, awsAccessKeyId, awsSecretAccessKey);
                }
            }
        }

        private static void SignHttps(IRequest request, ClientConfig clientConfig, RequestMetrics metrics, string awsAccessKeyId, string awsSecretAccessKey)
        {
            string nonce = Guid.NewGuid().ToString();
            string date = AWSSDKUtils.FormattedCurrentTimestampRFC822;
            string stringToSign;

            stringToSign = date + nonce;
            metrics.AddProperty(Metric.StringToSign, stringToSign);

            string signature = ComputeHash(stringToSign, awsSecretAccessKey, clientConfig.SignatureMethod);

            StringBuilder builder = new StringBuilder();
            builder.Append(HTTPS_SCHEME).Append(" ");
            builder.Append("AWSAccessKeyId=" + awsAccessKeyId + ",");
            builder.Append("Algorithm=" + clientConfig.SignatureMethod.ToString() + ",");
            builder.Append("SignedHeaders=x-amz-date;x-amz-nonce,");
            builder.Append("Signature=" + signature);

            builder.Append(GetSignedHeadersComponent(request) + ",");

            request.Headers[HeaderKeys.XAmzAuthorizationHeader] = builder.ToString();
            request.Headers[HeaderKeys.XAmzNonceHeader] = nonce;
            request.Headers[HeaderKeys.XAmzDateHeader] = date;
        }

        private static void SignHttpEx(IRequest request, RequestMetrics metrics, string awsAccessKeyId,
            string awsSecretAccessKey, SignerRequestParamsEx param = null)
        {
            string date = "20171006T170510Z"; //AWSSDKUtils.FormattedCurrentTimestampISO8601;
            string hostHeader = request.Endpoint.Host;
            request.Headers[HttpHeaderKeys.HostHeader] = hostHeader;
            request.Headers[HttpHeaderKeys.SdkData] = date;


        }

        private static void SignHttp(IRequest request, RequestMetrics metrics, string awsAccessKeyId, string awsSecretAccessKey, SignerRequestParamsEx param = null)
        {
            string hostHeader = request.Endpoint.Host;
            request.Headers[HttpHeaderKeys.HostHeader] = hostHeader;

            SigningAlgorithm algorithm = SigningAlgorithm.HmacSHA256;
            string nonce = Guid.NewGuid().ToString();
            string date = AWSSDKUtils.FormattedCurrentTimestampISO8601;
            bool isHttps = IsHttpsRequest(request);

            // Temporarily disabling the AWS3 HTTPS signing scheme and only using AWS3 HTTP
            isHttps = false;

            request.Headers[HttpHeaderKeys.SdkData] = date;

            // AWS3 HTTP requires that we sign the Host header
            // so we have to have it in the request by the time we sign.
            
            if (!request.Endpoint.IsDefaultPort)
                hostHeader += ":" + request.Endpoint.Port;
            

            byte[] bytesToSign = null;
            string stringToSign;
            Uri url = request.Endpoint;
            if (!string.IsNullOrEmpty(request.ResourcePath))
                url = new Uri(request.Endpoint, request.ResourcePath);

            stringToSign = request.HttpMethod + "\n"
                + GetCanonicalizedResourcePath(url) + "\n"
                + GetCanonicalizedQueryString(request.Parameters) + "\n"
                + GetCanonicalizedHeadersForStringToSign(request) + "\n"
                + GetRequestPayload(request);

            String hehehe = CreateStringToSign(stringToSign, param);

            bytesToSign = CryptoUtilFactory.CryptoInstance.ComputeSHA256Hash(Encoding.UTF8.GetBytes(hehehe));

            metrics.AddProperty(Metric.StringToSign, stringToSign);
            string signature = AWSSDKUtils.ToHex(ComputeHash(bytesToSign, awsSecretAccessKey, algorithm, true), true);

            //this.ComputeSignature()


            StringBuilder builder = new StringBuilder();
            builder.Append("SDK-HMAC-SHA256");
            builder.Append(" ");
            builder.Append("Credential=" + awsAccessKeyId + "/20171006/cn-north-1/dis/sdk_request, ");
            //builder.Append("Algorithm=" + algorithm.ToString() + ",");
            builder.Append(GetSignedHeadersComponent(request) + ",");
            builder.Append("Signature=" + signature);
            string authorizationHeader = builder.ToString();
            request.Headers[HttpHeaderKeys.Authorization] = authorizationHeader;
        }

        protected static String CreateStringToSign(String canonicalRequest, SignerRequestParamsEx signerParams)
        {
            byte[] hhhhh = ComputeHash(canonicalRequest);
            string hex = AWSSDKUtils.ToHex(hhhhh, true);
            StringBuilder stringToSignBuilder = new StringBuilder("SDK-HMAC-SHA256");
            stringToSignBuilder.Append("\n")
                               .Append(signerParams.GetFormattedSigningDateTime())
                               .Append("\n")
                               .Append(signerParams.GetScope())
                               .Append("\n")
                               .Append(hex);
            String stringToSign = stringToSignBuilder.ToString();
            return stringToSign;
        }

        /*
        private String buildAuthorizationHeader(String credentials, String requestscope)
        {
            String signingCredentials = credentials + "/" + requestscope;
            String credential = "Credential=" + signingCredentials;
            String signerHeaders = "SignedHeaders=" + this.getSignedHeadersString(request);
            String signatureHeader = "Signature=" + BinaryUtils.toHex(signature);
            StringBuilder authHeaderBuilder = new StringBuilder();
            authHeaderBuilder.append("SDK-HMAC-SHA256").append(" ").append(credential).append(", ").append(signerHeaders).append(", ").append(signatureHeader);
            return authHeaderBuilder.toString();
        }
        */
        #region Http signing helpers

        private static string GetCanonicalizedResourcePath(Uri endpoint)
        {
            string uri = endpoint.AbsolutePath;
            if (string.IsNullOrEmpty(uri))
            {
                return "/";
            }
            else
            {
                return AWSSDKUtils.UrlEncode(uri, true);
            }
        }

        private static bool IsHttpsRequest(IRequest request)
        {
            string protocol = request.Endpoint.Scheme;
            if (protocol.Equals("http", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            else if (protocol.Equals("https", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else
            {
                throw new ObsServiceException(
                    "Unknown request endpoint protocol encountered while signing request: " + protocol);
            }
        }

        private static string GetCanonicalizedQueryString(IDictionary<string, string> parameters)
        {
            IDictionary<string, string> sorted =
                  new SortedDictionary<string, string>(parameters, StringComparer.Ordinal);

            StringBuilder builder = new StringBuilder();
            foreach (var pair in sorted)
            {
                if (pair.Value != null)
                {
                    string key = pair.Key;
                    string value = pair.Value;
                    builder.Append(AWSSDKUtils.UrlEncode(key, false));
                    builder.Append("=");
                    builder.Append(AWSSDKUtils.UrlEncode(value, false));
                    builder.Append("&");
                }
            }

            string result = builder.ToString();
            return (string.IsNullOrEmpty(result) ? string.Empty : result.Substring(0, result.Length - 1));
        }

        private static string GetRequestPayload(IRequest request)
        {
            if (request.Content == null)
                return string.Empty;

            Encoding encoding = Encoding.GetEncoding(DEFAULT_ENCODING);
            return encoding.GetString(request.Content, 0, request.Content.Length);
        }

        private static string GetSignedHeadersComponent(IRequest request)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("SignedHeaders=");
            bool first = true;
            foreach (string header in GetHeadersForStringToSign(request))
            {
                if (!first) builder.Append(";");
                builder.Append(header);
                first = false;
            }
            return builder.ToString();
        }

        private static List<string> GetHeadersForStringToSign(IRequest request)
        {
            List<string> headersToSign = new List<string>();
            foreach (var entry in request.Headers) {
                string key = entry.Key;
                if (key.StartsWith("x-sdk-date", StringComparison.OrdinalIgnoreCase)
                        || key.Equals("content-type", StringComparison.OrdinalIgnoreCase)
                    || key.Equals("x-sdk-version", StringComparison.OrdinalIgnoreCase)
                    || key.Equals("accept", StringComparison.OrdinalIgnoreCase)
                        || key.Equals("host", StringComparison.OrdinalIgnoreCase))
                {
                    headersToSign.Add(key.ToLower());
                }
            }

            headersToSign.Sort(StringComparer.OrdinalIgnoreCase);
            return headersToSign;
        }

        private static string GetCanonicalizedHeadersForStringToSign(IRequest request)
        {
            List<string> headersToSign = GetHeadersForStringToSign(request);

            for (int i = 0; i < headersToSign.Count; i++)
            {
                headersToSign[i] = headersToSign[i].ToLower(CultureInfo.InvariantCulture);
            }

            SortedDictionary<string,string> sortedHeaderMap = new SortedDictionary<string,string>();
            foreach (var entry in request.Headers)
            {
                if (headersToSign.Contains(entry.Key.ToLower(CultureInfo.InvariantCulture)))
                {
                    sortedHeaderMap[entry.Key] = entry.Value;
                }
            }

            StringBuilder builder = new StringBuilder();
            foreach (var entry in sortedHeaderMap)
            {
                string value = entry.Value;
                string key = entry.Key.ToLower(CultureInfo.InvariantCulture);
                if (key.Equals("x-sdk-date"))
                {
                    value = "20171006T170510Z";
                }

                builder.Append(key);
                builder.Append(":");
                builder.Append(value);
                builder.Append("\n");
            }

            return builder.ToString();
        }

        #endregion
    }

    internal class DISHTTPSigner : DISSigner
    {
        public DISHTTPSigner()
            : base(false)
        {
        }
    }
}
