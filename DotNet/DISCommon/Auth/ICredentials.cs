using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Auth
{
    public interface ICredentials
    {
        string GetAccessKeyId();

        string GetSecretKey();
    }
}
