using Com.Bigdata.Dis.Sdk.DISCommon.Config;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Client
{
    public class DISClientBuilder : ClientBuilder
    {
        private bool _dataEncryptEnabled;

        private bool _defaultClientCertAuthEnabled;

        public DISClientBuilder WithDataEncryptEnabled(bool dataEncryptEnabled)
        {
            SetDataEncryptEnabled(dataEncryptEnabled);
            return this;
        }

        public DISClientBuilder WithDefaultClientCertAuthEnabled(bool defaultClientCertAuthEnabled)
        {
            SetDefaultClientCertAuthEnabled(defaultClientCertAuthEnabled);
            return this;
        }

        public bool IsDataEncryptEnabled()
        {
            return _dataEncryptEnabled;
        }

        public void SetDataEncryptEnabled(bool dataEncryptEnabled)
        {
            this._dataEncryptEnabled = dataEncryptEnabled;
        }

        public bool IsDefaultClientCertAuthEnabled()
        {
            return _defaultClientCertAuthEnabled;
        }

        public void SetDefaultClientCertAuthEnabled(bool defaultClientCertAuthEnabled)
        {
            this._defaultClientCertAuthEnabled = defaultClientCertAuthEnabled;
        }

        public static DISClientBuilder Standard()
        {
            return new DISClientBuilder();
        }

        public static DISIngestionClient DefaultClient()
        {
            return Standard().Build();
        }

        public override DISIngestionClient Build()
        {
            var disConfig = new DISConfig();

            if (null != _credentials)
            {
                disConfig.Credentials = _credentials;
            }
            if (!string.IsNullOrEmpty(_ak))
                disConfig.SetAK(_ak);
            if (!string.IsNullOrEmpty(_sk))
                disConfig.SetSK(_sk);
            if (!string.IsNullOrEmpty(_projectId))
                disConfig.SetProjectId(_projectId);
            if (!string.IsNullOrEmpty(_region))
                disConfig.SetRegion(_region);
            if (!string.IsNullOrEmpty(_endpoint))
                disConfig.SetEndpoint(_endpoint);
            disConfig.SetDataEncryptEnabled(_dataEncryptEnabled);
            disConfig.SetDefaultClientCertAuthEnabled(_defaultClientCertAuthEnabled);

            return new DISIngestionClient(disConfig);
        }
    }
}
