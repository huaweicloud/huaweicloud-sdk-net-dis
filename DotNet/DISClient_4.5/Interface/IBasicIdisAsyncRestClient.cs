using System;
using System.Collections.Generic;
using OBS.Runtime.Internal;
using System.Threading.Tasks;
using Com.Bigdata.Dis.Sdk.DISCommon.Interface;

namespace DISClient_4._5.Client
{
    public interface IBasicIdisAsyncRestClient
    {
        Task<T> PostAsync<T>(string baseUrl, string resource, IDictionary<string, string> headerMaps, object req);
    }
}
