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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using OBS.Runtime;
using OBS.Runtime.Internal;
using OBS.Runtime.Internal.Auth;
using OBS.Runtime.Internal.Transform;
using OBS.Runtime.Internal.Util;
using OBS.Util;
using OBS.S3.Internal;
using OBS.S3.Model;
using OBS.S3.Model.Internal.MarshallTransformations;
using OBS.S3.Util;
using Map = System.Collections.Generic.Dictionary<OBS.S3.S3QueryParameter, string>;

#if BCL45 || WIN_RT || WINDOWS_PHONE
using System.Threading.Tasks;
#endif

namespace OBS.S3
{
    public partial class ObsS3Client : ObsServiceClient, IObsS3
    {
        #region GetPreSignedURL

        /// <summary>
        /// Create a signed URL allowing access to a resource that would 
        /// usually require authentication.
        /// </summary>
        /// <remarks>
        /// <para>
        /// When using query string authentication you create a query,
        /// specify an expiration time for the query, sign it with your
        /// signature, place the data in an HTTP request, and distribute
        /// the request to a user or embed the request in a web page.
        /// </para>
        /// <para>
        /// A PreSigned URL can be generated for GET, PUT, DELETE and HEAD
        /// operations on your bucketName, keys, and versions.
        /// </para>
        /// </remarks>
        /// <param name="request">The GetPreSignedUrlRequest that defines the
        /// parameters of the operation.</param>
        /// <returns>A string that is the signed http request.</returns>
        /// <exception cref="T:System.ArgumentException" />
        /// <exception cref="T:System.ArgumentNullException" />
        public GetPreSignedUrlResponse GetPreSignedURL(GetPreSignedUrlRequest request)
        {
            if (Credentials == null)
                throw new ObsS3Exception("Credentials must be specified, cannot call method anonymously");

            if (request == null)
                throw new ArgumentNullException("request", "The PreSignedUrlRequest specified is null!");

            if (!request.IsSetExpires())
                throw new InvalidOperationException("The Expires specified is null!");

            var aws4Signing = ObsConfigs.S3Config.UseSignatureVersion4;
            //从配置文件中读取
            var authRegion = Config.AuthenticationRegion;


            var immutableCredentials = Credentials.GetCredentials();
            var irequest = Marshall(request, immutableCredentials.AccessKey, immutableCredentials.Token, aws4Signing);

            irequest.Endpoint = new Uri(Config.DetermineServiceURL());

            var context = new OBS.Runtime.Internal.ExecutionContext(new OBS.Runtime.Internal.RequestContext(true) { Request = irequest, ClientConfig = this.Config }, null);
            ObsS3PostMarshallHandler handler = new ObsS3PostMarshallHandler();
            handler.ProcessRequestHandlers(context);

            var metrics = new RequestMetrics();
            var actualSignedRequestHeaders = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            if (aws4Signing)
            {
                if (!irequest.Headers.ContainsKey(HeaderKeys.HostHeader))
                {
                    var hostHeader = irequest.Endpoint.Host;
                    if (!irequest.Endpoint.IsDefaultPort)
                        hostHeader += ":" + irequest.Endpoint.Port;
                    irequest.Headers.Add(HeaderKeys.HostHeader, hostHeader);
                }

            }
            foreach (var header in irequest.Headers)
            {
                actualSignedRequestHeaders.Add(header.Key.ToLower(CultureInfo.InvariantCulture), header.Value);
            }
            string authorization;
            if (aws4Signing)
            {
                var aws4Signer = new AWS4PreSignedUrlSigner();
                var signingResult = aws4Signer.SignRequest(irequest,
                                                           this.Config,
                                                           metrics,
                                                           immutableCredentials.AccessKey,
                                                           immutableCredentials.SecretKey);
                authorization = "&" + signingResult.ForQueryParameters.Replace("/", "%2F").Replace(";", "%3B");
            }
            else
            {
                this.Signer.Sign(irequest, this.Config, metrics, immutableCredentials.AccessKey, immutableCredentials.SecretKey);
                authorization = irequest.Headers[HeaderKeys.AuthorizationHeader];
                authorization = authorization.Substring(authorization.IndexOf(":", StringComparison.Ordinal) + 1);
                authorization = "&Signature=" + ObsS3Util.UrlEncode(authorization, false);
            }

            Uri url = ObsServiceClient.ComposeUrl(irequest);
            string tempSignUrl = url.AbsoluteUri + authorization;

            return new GetPreSignedUrlResponse(tempSignUrl, actualSignedRequestHeaders);
        }

        /// <summary>
        /// Marshalls the parameters for a presigned url for a preferred signing protocol.
        /// </summary>
        /// <param name="getPreSignedUrlRequest"></param>
        /// <param name="accessKey"></param>
        /// <param name="token"></param>
        /// <param name="aws4Signing">
        /// True if AWS4 signing will be used; if the expiry period in the request exceeds the
        /// maximum allowed for AWS4 (one week), an ArgumentException is thrown.
        /// </param>
        /// <returns></returns>
        private static IRequest Marshall(GetPreSignedUrlRequest getPreSignedUrlRequest, 
                                         string accessKey, 
                                         string token, 
                                         bool aws4Signing)
        {
            IRequest request = new DefaultRequest(getPreSignedUrlRequest, "ObsS3");

            request.HttpMethod = getPreSignedUrlRequest.Verb.ToString();

            var headers = getPreSignedUrlRequest.Headers;
            foreach (var key in headers.Keys)
                request.Headers[key] = headers[key];

            ObsS3Util.SetMetadataHeaders(request, getPreSignedUrlRequest.Metadata);

            if (!string.IsNullOrEmpty(token))
                request.Headers[HeaderKeys.XAmzSecurityTokenHeader] = token;

            var queryParameters = request.Parameters;
            if (getPreSignedUrlRequest.IsSetSubResources() && getPreSignedUrlRequest.SubResources.Count>0)
            {
                var subResources = getPreSignedUrlRequest.SubResources;
                foreach (var key in subResources.Keys)
                    request.SubResources[key] = subResources[key];
            }
            if (getPreSignedUrlRequest.IsSetParameters() && getPreSignedUrlRequest.Parameters.Count > 0)
            {
                var parameters = getPreSignedUrlRequest.Parameters;
                foreach (var key in parameters.Keys)
                    queryParameters.Add(key, parameters[key]);
            }

            var uriResourcePath = new StringBuilder("/");
            if (!string.IsNullOrEmpty(getPreSignedUrlRequest.BucketName))
                uriResourcePath.Append(S3Transforms.ToStringValue(getPreSignedUrlRequest.BucketName));
            if (!string.IsNullOrEmpty(getPreSignedUrlRequest.Key))
            {
                if (uriResourcePath.Length > 1)
                    uriResourcePath.Append("/");
                uriResourcePath.Append(S3Transforms.ToStringValue(getPreSignedUrlRequest.Key));
            }

            var baselineTime = aws4Signing ? DateTime.UtcNow : new DateTime(1970, 1, 1);
            var expires = Convert.ToInt64((getPreSignedUrlRequest.Expires.ToUniversalTime() - baselineTime).TotalSeconds);

            if (aws4Signing && expires > AWS4PreSignedUrlSigner.MaxAWS4PreSignedUrlExpiry)
            {
                var msg = string.Format(CultureInfo.InvariantCulture, "The maximum expiry period for a presigned url using AWS4 signing is {0} seconds",
                                        AWS4PreSignedUrlSigner.MaxAWS4PreSignedUrlExpiry);
                throw new ArgumentException(msg);
            }

            queryParameters.Add(aws4Signing ? "X-Amz-Expires" : "Expires", expires.ToString(CultureInfo.InvariantCulture));

            if (!string.IsNullOrEmpty(token))
                queryParameters.Add("x-amz-security-token", token);
            if (!aws4Signing)
                queryParameters.Add("AWSAccessKeyId", accessKey);


            request.ResourcePath = uriResourcePath.ToString();
            request.UseQueryString = true;

            return request;
        }

        #endregion    

        private Protocol DetermineProtocol()
        {
            string serviceUrl = Config.DetermineServiceURL();
            Protocol protocol = serviceUrl.StartsWith("https", StringComparison.OrdinalIgnoreCase) ? Protocol.HTTPS : Protocol.HTTP;
            return protocol;
        }

        internal void ConfigureProxy(HttpWebRequest httpRequest)
        {
#if BCL
            if (!string.IsNullOrEmpty(Config.ProxyHost) && Config.ProxyPort != -1)
            {
                WebProxy proxy = new WebProxy(Config.ProxyHost, Config.ProxyPort);
                httpRequest.Proxy = proxy;
            }

            if (httpRequest.Proxy != null && Config.ProxyCredentials != null)
            {
                httpRequest.Proxy.Credentials = Config.ProxyCredentials;
            }
#endif
        }

        internal static void CleanupRequest(IRequest request)
        {
            var putObjectRequest = request.OriginalRequest as PutObjectRequest;
            if (putObjectRequest != null)
            {
                if (putObjectRequest.InputStream != null
                    && (!string.IsNullOrEmpty(putObjectRequest.FilePath) || putObjectRequest.AutoCloseStream))
                {
                    putObjectRequest.InputStream.Dispose();
                }

                // Set the input stream to null since it was created during the request to represent the filepath or content body
                if (!string.IsNullOrEmpty(putObjectRequest.FilePath) || !string.IsNullOrEmpty(putObjectRequest.ContentBody)
#if WIN_RT || WINDOWS_PHONE
                    || putObjectRequest.StorageFile != null
#endif
)
                {
                    putObjectRequest.InputStream = null;
                }
            }

            var uploadPartRequest = request.OriginalRequest as UploadPartRequest;
            if (uploadPartRequest != null)
            {
                // FilePath was set, so we created the underlying stream, so we must close it
                if (uploadPartRequest.IsSetFilePath() && uploadPartRequest.InputStream != null)
                {
                    uploadPartRequest.InputStream.Dispose();
                }

                if (uploadPartRequest.IsSetFilePath()
#if WIN_RT || WINDOWS_PHONE
                    || uploadPartRequest.StorageFile != null
#endif
)
                    uploadPartRequest.InputStream = null;
            }
        }
    }
}

