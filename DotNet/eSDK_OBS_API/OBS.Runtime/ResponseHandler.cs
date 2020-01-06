using System;
using System.Collections.Generic;
using System.Text;
using OBS.Runtime.Internal;
using OBS.Runtime.Internal.Transform;

namespace OBS.Runtime
{
    public delegate void ResponseEventHandler(object sender, ResponseEventArgs e);

    public class ResponseEventArgs : EventArgs
    {
        #region Constructor

        protected ResponseEventArgs() { }

        #endregion
    }

    public class WebServiceResponseEventArgs : ResponseEventArgs
    {
        #region Constructor

        protected WebServiceResponseEventArgs() { }

        #endregion

        #region Properties

        public IDictionary<string, string> RequestHeaders { get; private set; }
        public IDictionary<string, string> ResponseHeaders { get; private set; }
        public IDictionary<string, string> Parameters { get; private set; }
        public string ServiceName { get; private set; }
        public Uri Endpoint { get; private set; }
        public ObsWebServiceRequest Request { get; private set; }
        public ObsWebServiceResponse Response { get; private set; }

        #endregion

        #region Creator method

        internal static WebServiceResponseEventArgs Create(ObsWebServiceResponse response, IRequest request, IWebResponseData webResponseData)
        {
            WebServiceResponseEventArgs args = new WebServiceResponseEventArgs
            {
                RequestHeaders = request.Headers,
                Parameters = request.Parameters,
                ServiceName = request.ServiceName,
                Request = request.OriginalRequest,
                Endpoint = request.Endpoint,
                Response = response
            };
            args.ResponseHeaders = new Dictionary<string, string>();
            if (webResponseData != null)
            {
                var headerNames = webResponseData.GetHeaderNames();
                foreach (var responseHeaderName in headerNames)
                {
                    string responseHeaderValue = webResponseData.GetHeaderValue(responseHeaderName);
                    args.ResponseHeaders[responseHeaderName] = responseHeaderValue;
                }
            }
            return args;
        }

        #endregion
    }
}
