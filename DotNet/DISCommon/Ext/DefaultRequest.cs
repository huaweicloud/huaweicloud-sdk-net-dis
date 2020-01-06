using System;
using System.Collections.Generic;
using System.IO;
using Com.Bigdata.Dis.Sdk.DISCommon.DISWeb;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Ext
{
    public class DefaultRequest : IDISRequest
    {
        private string _resourcePath;
        private Dictionary<string, string> _parameters;
        private Dictionary<string, string> _headers;
        private Uri _endpoint;
        private readonly string _serviceName;
        private HttpMethodName _httpMethod;
        private Stream _content;
        private int _timeOffset;

        public DefaultRequest(string serviceName)
        {
            _parameters = new Dictionary<string, string>();
            _headers = new Dictionary<string, string>();
            _httpMethod = HttpMethodName.POST;
            _serviceName = serviceName;
        }

        public void AddHeader(string name, string value)
        {
            _headers.Add(name, value); 
        }

        public Dictionary<string, string> GetHeaders()
        {
            return _headers;
        }

        public void SetHeaders(Dictionary<string, string> headers)
        {
            _headers.Clear();
            _headers = headers;
        }

        public void SetResourcePath(string resourcePath)
        {
            _resourcePath = resourcePath;
        }

        public string GetResourcePath()
        {
            return _resourcePath;
        }

        public void AddParameter(string name, string value)
        {
            _parameters[name] = value;
        }

        public Dictionary<string, string> GetParameters()
        {
            return _parameters;
        }

        public void SetParameters(Dictionary<string, string> parameters)
        {
            _parameters.Clear();
            _parameters = parameters;
        }

        public Uri GetEndpoint()
        {
            return _endpoint;
        }

        public void SetEndpoint(Uri endpoint)
        {
            _endpoint = endpoint;
        }

        public HttpMethodName GetHttpMethod()
        {
            return _httpMethod;
        }

        public void SetHttpMethod(HttpMethodName httpMethod)
        {
            _httpMethod = httpMethod;
        }

        public Stream GetContent()
        {
            return _content;
        }

        public void SetContent(Stream content)
        {
            _content = content;
        }

        public string GetServiceName()
        {
            return _serviceName;
        }

        public int GetTimeOffset()
        {
            return _timeOffset;
        }

        public void SetTimeOffset(int timeOffset)
        {
            _timeOffset = timeOffset;
        }

        public IDISRequest WithTimeOffset(int timeOffset)
        {
            SetTimeOffset(timeOffset);
            return this;
        }

        public IDISRequest WithParameter(string name, string value)
        {
            AddParameter(name, value);
            return this;
        }
    }
}
