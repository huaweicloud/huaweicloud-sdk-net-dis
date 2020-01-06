using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Com.Bigdata.Dis.Sdk.DISCommon.DISWeb;
using Com.Bigdata.Dis.Sdk.DISCommon.Ext;
using OBS.Runtime.Internal;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Utils
{
    public class HttpUtils
    {
        private static string DEFAULT_ENCODING = "UTF-8";

        public HttpUtils()
        {
        }

        public static string UrlEncode(string value, bool path)
        {
            if (value == null)
            {
                return "";
            }
            
            return HttpUtility.UrlEncode(value, new UTF8Encoding());
        }

        public static bool IsUsingNonDefaultPort(Uri uri)
        {
            var scheme = uri.Scheme.ToLower();
            var port = uri.Port;
            return port > 0 && ((!scheme.Equals("http") || port != 80) && (!scheme.Equals("https") || port != 443));
        }



        public static string AppendUri(string baseUri, string path)
        {
            return AppendUri(baseUri, path, false);
        }

        public static string AppendUri(string baseUri, string path, bool escapeDoubleSlash)
        {
            var resultUri = baseUri;
            if (!string.IsNullOrEmpty(path))
            {
                if (path.StartsWith("/"))
                {
                    if (baseUri.EndsWith("/"))
                    {
                        resultUri = baseUri.Substring(0, baseUri.Length - 1);
                    }
                }
                else if (!baseUri.EndsWith("/"))
                {
                    resultUri = baseUri + "/";
                }

                var encodedPath = UrlEncode(path, true);
                if (escapeDoubleSlash)
                {
                    encodedPath = encodedPath.Replace("//", "/%2F");
                }

                resultUri = resultUri + encodedPath;
            }
            else if (!baseUri.EndsWith("/"))
            {
                resultUri = baseUri + "/";
            }

            return resultUri;
        }
        public static bool UsePayloadForQueryParameters(IRequest request)
        {
            var requestIsPOST = request.HttpMethod.Equals( HttpMethodName.POST.ToString(), StringComparison.InvariantCulture);
            var requestHasNoPayload = request.Content == null;
            return requestIsPOST && requestHasNoPayload;
        }

        public static string EncodeParameters(IRequest request)
        {
            var parameters = request.Parameters;
            var encodedParams = HttpUtility.UrlEncode(string.Join("&", parameters.Select(kvp =>string.Format("{0}={1}", kvp.Key, kvp.Value)).ToArray()), Encoding.UTF8);

            return encodedParams;
        }
    }
}
