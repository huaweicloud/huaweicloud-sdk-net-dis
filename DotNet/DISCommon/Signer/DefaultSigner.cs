using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Bigdata.Dis.Sdk.DISCommon.Auth;
using Com.Bigdata.Dis.Sdk.DISCommon.DISWeb;
using Com.Bigdata.Dis.Sdk.DISCommon.Ext;
using Com.Bigdata.Dis.Sdk.DISCommon.Utils;
using OBS.Runtime;
using OBS.Runtime.Internal.Util;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Signer
{
    public class DefaultSigner : AbstractSigner, IRegionSigner, IServiceSigner
    {
        private static int SIGNER_CACHE_MAX_SIZE = 300;
        private static string LINUX_NEW_LINE = "\n";
        protected string serviceName;
        protected string regionName;
        protected bool doubleUrlEncode;

        public DefaultSigner()
        {
            this.doubleUrlEncode = true;
        }

        public DefaultSigner(bool doubleUrlEncoding)
        {
            this.doubleUrlEncode = doubleUrlEncoding;
        }

        public new void Sign<T>(IRequest<T> request, ICredentials credentials)
        {
            var sanitizedCredentials = this.SanitizeCredentials(credentials);
            var signerParams = new SignerRequestParams<T>(request, this.regionName, this.serviceName, "SDK-HMAC-SHA256");
            this.AddHostHeader(request);
            request.AddHeader("X-Sdk-Date", signerParams.GetFormattedSigningDateTime());
            String contentSha256 = this.CalculateContentHash(request);
            /* Not support for now
            String contentSha256 = this.calculateContentHash(request);
            if ("required".equals(request.getHeaders().get("x-sdk-content-sha256")))
            {
                request.addHeader("x-sdk-content-sha256", contentSha256);
            }

            String canonicalRequest = this.createCanonicalRequest(request, contentSha256);
            string stringToSign = this.CreateStringToSign(canonicalRequest, signerParams);
            byte[] signingKey = this.deriveSigningKey(sanitizedCredentials, signerParams);
            byte[] signature = this.computeSignature(stringToSign, signingKey, signerParams);
            */
            //byte[] signature = this.computeSignature(stringToSign, signingKey, signerParams);

            request.AddHeader("Authorization", this.BuildAuthorizationHeader(request, null, sanitizedCredentials, signerParams));
        }

        public void SetServiceName(string serviceName)
        {
            this.serviceName = serviceName;
        }

        public void SetRegionName(string regionName)
        {
            this.regionName = regionName;
        }

        protected void AddHostHeader<T>(IRequest<T> request)
        {
            var endpoint = request.GetEndpoint();
            var hostHeaderBuilder = new StringBuilder(endpoint.Host);
            if (HttpUtils.IsUsingNonDefaultPort(endpoint))
            {
                hostHeaderBuilder.Append(":").Append(endpoint.Port);
            }

            request.AddHeader("Host", hostHeaderBuilder.ToString());
        }

        protected string CalculateContentHash<T>(IRequest<T> request)
        {
            throw new NotImplementedException();
        }

        private string BuildAuthorizationHeader<T>(IRequest<T> request, byte[] signature, ICredentials credentials, SignerRequestParams<T> signerParams)
        {
            var signingCredentials = credentials.GetAccessKeyId() + "/" + signerParams.GetScope();
            var credential = "Credential=" + signingCredentials;
            var signerHeaders = "SignedHeaders=" + this.GetSignedHeadersString(request);
            var signatureHeader = "Signature=" + BinaryUtils.ToHex(signature);
            var authHeaderBuilder = new StringBuilder();
            authHeaderBuilder.Append("SDK-HMAC-SHA256")
                             .Append(" ")
                             .Append(credential)
                             .Append(", ")
                             .Append(signerHeaders)
                             .Append(", ")
                             .Append(signatureHeader);
            return authHeaderBuilder.ToString();
        }

        protected string GetSignedHeadersString<T>(IRequest<T> request)
        {
            var headers = request.GetHeaders();

            IEnumerable<KeyValuePair<string, string>> pairs = headers.OrderBy(kvp => headers[kvp.Key]);
            var buffer = new StringBuilder();

            foreach (var keyValuePair in pairs)
            {
                if (buffer.Length > 0)
                {
                    buffer.Append(";");
                }
                buffer.Append(keyValuePair.Key.ToLower());
            }

            return buffer.ToString();
        }

        protected string CreateStringToSign<T>(string canonicalRequest, SignerRequestParams<T> signerParams)
        {
            var stringToSignBuilder = new StringBuilder(signerParams.GetSigningAlgorithm());
            stringToSignBuilder.Append("\n")
                               .Append(signerParams.GetFormattedSigningDateTime())
                               .Append("\n")
                               .Append(signerParams.GetScope())
                               .Append("\n")
                               .Append(BinaryUtils.ToHex(this.Hash(canonicalRequest)));
            var stringToSign = stringToSignBuilder.ToString();
            return stringToSign;
        }

        protected string createCanonicalRequest<T>(IRequest<T> request, string contentSha256)
        {
            return "";
            /*
            var path = HttpUtils.AppendUri(request.GetEndpoint().AbsolutePath, request.GetResourcePath());
            var canonicalRequestBuilder = new StringBuilder(request.GetHttpMethod().ToString());
            canonicalRequestBuilder.Append("\n")
                                   .Append(this.GetCanonicalizedResourcePath(path, this.doubleUrlEncode))
                                   .Append("\n").Append(this.getCanonicalizedQueryString(request))
                                   .Append("\n").Append(this.getCanonicalizedHeaderString(request))
                                   .Append("\n").Append(this.getSignedHeadersString(request))
                                   .Append("\n").Append(contentSha256);
            var canonicalRequest = canonicalRequestBuilder.ToString();
            return canonicalRequest;
            */
        }

        protected string GetCanonicalizedResourcePath(string resourcePath, bool urlEncode)
        {
            if (!string.IsNullOrEmpty(resourcePath))
            {
                var value = urlEncode ? HttpUtils.UrlEncode(resourcePath, true) : resourcePath;
                return value.StartsWith("/") ? value : "/" + value;
            }

            return "/";
        }

        protected string getCanonicalizedQueryString(Dictionary<string, string> parameters)
        {
            return "";
            /*
            TreeMap sorted = new TreeMap();
            Iterator pairs = parameters.entrySet().iterator();

            while (pairs.hasNext())
            {
                Entry builder = (Entry)pairs.next();
                var pair = (string)builder.getKey();
                var value = (string)builder.getValue();
                sorted.put(HttpUtils.urlEncode(pair, false), HttpUtils.urlEncode(value, false));
            }

            var builder1 = new StringBuilder();
            pairs = sorted.entrySet().iterator();

            while (pairs.hasNext())
            {
                Entry pair1 = (Entry)pairs.next();
                builder1.append((string)pair1.getKey());
                builder1.append("=");
                builder1.append((string)pair1.getValue());
                if (pairs.hasNext())
                {
                    builder1.append("&");
                }
            }

            return builder1.toString();
            */
        }

        /*
         * protected byte[] computeSignature(String stringToSign, byte[] signingKey)
        {
            return this.Sign(Encoding.UTF8.GetBytes(stringToSign), signingKey, SigningAlgorithm.HmacSHA256);
        }

        protected byte[] Sign(byte[] data, byte[] key, SigningAlgorithm algorithm)
        {
        try {
            Mac e = Mac.getInstance(algorithm.toString());
            e.init(new SecretKeySpec(key, algorithm.toString()));
            return e.doFinal(data);
        } catch (Exception var5) {
    throw new ClientException("Unable to calculate a request signature: " + var5.getMessage(), var5);
}
}
*/
    }
}
