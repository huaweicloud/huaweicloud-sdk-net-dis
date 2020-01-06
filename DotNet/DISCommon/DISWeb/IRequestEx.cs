using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Com.Bigdata.Dis.Sdk.DISCommon.Ext;

namespace Com.Bigdata.Dis.Sdk.DISCommon.DISWeb
{
    public interface IRequest<T>
    {
        void AddHeader(string var1, string var2);

        Dictionary<string, string> GetHeaders();

        void SetHeaders(Dictionary<string, string> var1);

        void SetResourcePath(string var1);

        string GetResourcePath();

        void AddParameter(string var1, string var2);

        IRequest<T> WithParameter(string var1, string var2);

        Dictionary<string, string> GetParameters();

        void SetParameters(Dictionary<string, string> var1);

        Uri GetEndpoint();

        void SetEndpoint(Uri var1);

        HttpMethodName GetHttpMethod();

        void SetHttpMethod(HttpMethodName var1);

        Stream GetContent();

        void SetContent(Stream var1);

        string GetServiceName();

        // Not supoported yet
        //WebServiceRequest GetOriginalRequest();

        int GetTimeOffset();

        void SetTimeOffset(int var1);

        IRequest<T> WithTimeOffset(int var1);
    }
}
