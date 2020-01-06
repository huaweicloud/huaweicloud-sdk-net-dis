﻿/*
 * Copyright 2011-2013 OBS.com, Inc. or its affiliates. All Rights Reserved.
 * 
 * Licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License.
 * A copy of the License is located at
 * 
 *  
 * 
 * or in the "license" file accompanying this file. This file is distributed
 * on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
 * express or implied. See the License for the specific language governing
 * permissions and limitations under the License.
 */
using System;
using System.Collections.Generic;
using System.Text;
using OBS.Runtime.Internal;
using System.Net;
using System.Collections.Specialized;

namespace OBS.Runtime
{
    public class RequestEventArgs : EventArgs
    {
        #region Constructor

        protected RequestEventArgs() { }
        
        #endregion
    }

    public class WebServiceRequestEventArgs : RequestEventArgs
    {
        #region Constructor

        protected WebServiceRequestEventArgs() { }

        #endregion

        #region Properties

        public IDictionary<string, string> Headers { get; protected set; }
        public IDictionary<string, string> Parameters { get; protected set; }
        public string ServiceName { get; protected set; }
        public Uri Endpoint { get; protected set; }
        public ObsWebServiceRequest Request { get; protected set; }
        [Obsolete("OriginalRequest property has been deprecated in favor of the Request property")]
        public ObsWebServiceRequest OriginalRequest { get { return Request; } }

        #endregion

        #region Creator method

        internal static WebServiceRequestEventArgs Create(IRequest request)
        {
            WebServiceRequestEventArgs args = new WebServiceRequestEventArgs
            {
                Headers = request.Headers,
                Parameters = request.Parameters,
                ServiceName = request.ServiceName,
                Request = request.OriginalRequest,
                Endpoint = request.Endpoint
            };
            return args;
        }

        #endregion
    }

    public class HeadersRequestEventArgs : RequestEventArgs
    {
        protected HeadersRequestEventArgs() { }
        public IDictionary<string, string> Headers { get; protected set; }

        internal static HeadersRequestEventArgs Create(IDictionary<string, string> headers)
        {
            HeadersRequestEventArgs args = new HeadersRequestEventArgs
            {
                Headers = headers
            };
            return args;
        }
    }

    public delegate void RequestEventHandler(object sender, RequestEventArgs e);
}
