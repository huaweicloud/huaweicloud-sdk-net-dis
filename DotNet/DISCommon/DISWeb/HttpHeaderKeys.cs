using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Com.Bigdata.Dis.Sdk.DISCommon.DISWeb
{
    public abstract class HttpHeaderKeys
    {
        public const string SdkData = "X-Sdk-Date";
        public const string SdkShaContent = "x-sdk-content-sha256";
        public const string Authorization = "Authorization";
        public const string HostHeader = "host";

    }
}
