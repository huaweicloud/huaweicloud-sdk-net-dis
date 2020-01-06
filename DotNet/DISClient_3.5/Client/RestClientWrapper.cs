using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Com.Bigdata.Dis.Sdk.DISCommon.Auth;
using Com.Bigdata.Dis.Sdk.DISCommon.Config;
using Com.Bigdata.Dis.Sdk.DISCommon.DISWeb;
using OBS.Runtime.Internal;

namespace DISClient_3._5.Client
{
    public class RestClientWrapper: Com.Bigdata.Dis.Sdk.DISCommon.Client.RestClientWrapper
    {
        public RestClientWrapper(IRequest<HttpRequest> request, DISConfig disConfig):base(request, disConfig)
        {
        }

        public RestClientWrapper(OBS.Runtime.Internal.IRequest request, DISConfig disConfig) : base(request, disConfig)
        {
        }

        public RestClientWrapper(IDISRequest request, DISConfig disConfig) : base(request, disConfig)
        {
        }
    }
}
