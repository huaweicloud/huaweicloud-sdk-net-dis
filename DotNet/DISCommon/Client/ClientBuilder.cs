using Com.Bigdata.Dis.Sdk.DISCommon.Auth;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Client
{
    public abstract class ClientBuilder
    {
        protected ICredentials _credentials;

        protected string _ak;

        protected string _sk;

        protected string _region;

        protected string _projectId;

        protected string _endpoint;

        public ClientBuilder WithCredentials(ICredentials credentials)
        {
            SetCredentials(credentials);
            return GetClientBuilder();
        }

        public ClientBuilder WithAk(string ak)
        {
            SetAk(ak);
            return GetClientBuilder();
        }

        public ClientBuilder WithSk(string sk)
        {
            SetSk(sk);
            return GetClientBuilder();
        }

        public ClientBuilder WithRegion(string region)
        {
            SetRegion(region);
            return GetClientBuilder();
        }

        public ClientBuilder WithProjectId(string projectId)
        {
            SetProjectId(projectId);
            return GetClientBuilder();
        }

        public ClientBuilder WithEndpoint(string endpoint)
        {
            SetEndpoint(endpoint);
            return GetClientBuilder();
        }

        /**
         * Gets the AWSCredentialsProvider currently configured in the builder.
         */
        public ICredentials GetCredentials()
        {
            return this._credentials;
        }

        public void SetCredentials(ICredentials credentials)
        {
            this._credentials = credentials;
        }

        public string GetAk()
        {
            return _ak;
        }

        public void SetAk(string ak)
        {
            this._ak = ak;
        }

        public string GetSk()
        {
            return _sk;
        }

        public void SetSk(string sk)
        {
            this._sk = sk;
        }

        public string GetRegion()
        {
            return _region;
        }

        public void SetRegion(string region)
        {
            this._region = region;
        }

        public string GetProjectId()
        {
            return _projectId;
        }

        public void SetProjectId(string projectId)
        {
            this._projectId = projectId;
        }

        public string GetEndpoint()
        {
            return _endpoint;
        }

        public void SetEndpoint(string endpoint)
        {
            this._endpoint = endpoint;
        }

        protected ClientBuilder GetSubclass()
        {
            return this;
        }

        protected ClientBuilder GetClientBuilder()
        {
            return this;
        }

        public abstract DISIngestionClient Build();
    }
}
