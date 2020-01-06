using System;
using System.Collections.Generic;
using Com.Bigdata.Dis.Sdk.DISCommon.Model;
using OBS.Runtime.Internal;


namespace DISClient_4._5.Client
{
    public interface IDISRestClient : Com.Bigdata.Dis.Sdk.DISCommon.Interface.IDISRestClient
    {
        T Get<T>(RequestContext requestContext, string baseUrl, string resource, IDictionary<String, String> headerMaps, object req, InterfaceType interfaceName = InterfaceType.DISInterfaceNone);

        T Post<T>(string baseUrl, string resource, IDictionary<String, String> headerMaps, object req, InterfaceType interfaceName = InterfaceType.DISInterfaceNone);
    }
}
