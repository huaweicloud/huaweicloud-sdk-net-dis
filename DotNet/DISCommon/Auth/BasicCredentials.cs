using System;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Auth
{
    public class BasicCredentials : ICredentials
    {
        private readonly string _accessKey;
        private readonly string _secretKey;

        public BasicCredentials(string accessKey, string secretKey)
        {
            if (accessKey == null)
            {
                throw new ArgumentException("Access key cannot be null.");
            }

            if (secretKey == null)
            {
                throw new ArgumentException("Secret key cannot be null.");
            }

            _accessKey = accessKey;
            _secretKey = secretKey;
        }

        public string GetAccessKeyId()
        {
            return _accessKey;
        }

        public string GetSecretKey()
        {
            return _secretKey;
        }
    }
}
