using System;
using System.Collections.Generic;
using OBS.Runtime.Internal;


namespace Com.Bigdata.Dis.Sdk.DISCommon.Interface
{
    public interface IDISRestClient
    {
        T Get<T>(string url, IDictionary<String, String> headerMaps, object req);

        T Get<T>(string baseUrl, string resource, IDictionary<String, String> headerMaps, object req);

        T Get<T>(RequestContext requestContext, string baseUrl, string resource, IDictionary<String, String> headerMaps, object req);

        T Post<T>(string baseUrl, string resource, IDictionary<String, String> headerMaps, object req);
        T Post<T>(string url, IDictionary<String, String> headerMaps, object req);
        T Put<T>(string url, string resource, IDictionary<String, String> headerMaps, object req);
        T Delete<T>(string url, IDictionary<String, String> headerMaps, object req);
    }
}
