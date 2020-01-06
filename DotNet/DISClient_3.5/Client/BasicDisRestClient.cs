using Com.Bigdata.Dis.Sdk.DISCommon.Interface;
using Com.Bigdata.Dis.Sdk.DISCommon.Client;
using Com.Bigdata.Dis.Sdk.DISCommon.Config;

namespace DISClient_3._5.Client
{
    public class BasicDisRestClient : Com.Bigdata.Dis.Sdk.DISCommon.Client.BasicDisRestClient
    {
        private BasicDisRestClient(DISConfig disConfig) : base(disConfig)
        {
        }

        public static IDISRestClient GetInstances(DISConfig disConfig)
        {
            return GetInstance(disConfig);
        }
    }
}
