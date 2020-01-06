using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Com.Bigdata.Dis.Sdk.DISCommon.Auth;
using Com.Bigdata.Dis.Sdk.DISCommon.DISWeb;
using Com.Bigdata.Dis.Sdk.DISCommon.Ext;
using Com.Bigdata.Dis.Sdk.DISCommon.Utils;
using OBS.Runtime.Internal;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Signer
{
    public abstract class AbstractSigner : ISigner
    {
        public void Sign<T>(IRequest<T> var1, ICredentials var2)
        {
            throw new NotImplementedException();
        }

        protected ICredentials SanitizeCredentials(ICredentials credentials)
        {
            var accessKeyId = credentials.GetAccessKeyId();
            var secretKey = credentials.GetSecretKey();
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

        public byte[] Hash(String text)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            var hashstring = new SHA256Managed();
            return hashstring.ComputeHash(bytes);
        }

        protected Stream getBinaryRequestPayloadStream<T>(IRequest<T> request)
        {
            return null;
            /*
            if (HttpUtils.UsePayloadForQueryParameters(request))
            {
                String encodedParameters = HttpUtils.EncodeParameters(request);
                return encodedParameters == null ? new ByteArrayInputStream(new byte[0]) : new ByteArrayInputStream(encodedParameters.getBytes(StringUtils.UTF8));
            }
            else
            {
                return this.getBinaryRequestPayloadStreamWithoutQueryParams(request);
            }
            */
        }

    }
}
