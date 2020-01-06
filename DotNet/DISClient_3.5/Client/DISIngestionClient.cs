using Com.Bigdata.Dis.Sdk.DISCommon.Auth;
using Com.Bigdata.Dis.Sdk.DISCommon.Config;

namespace DISClient_3._5.Client
{
    public class DISIngestionClient : Com.Bigdata.Dis.Sdk.DISCommon.Client.DISIngestionClient
    {
        public DISIngestionClient() : base()
        {
        }

        public DISIngestionClient(ICredentials credentials, string region) : base(credentials, region)
        {
        }

        public DISIngestionClient(DISConfig disConfig) : base(disConfig)
        {
        }

        public DISIngestionClient(string disConfigFile) : base(disConfigFile)
        {
        }

        public DISIngestionClient(ICredentials credentials, string region, string disConfigFile) : base(credentials, region, disConfigFile)
        {
        }

        public DISIngestionClient(ICredentials credentials, string region, DISConfig disConfig) : base(credentials, region, disConfig)
        {
        }
    }
}
