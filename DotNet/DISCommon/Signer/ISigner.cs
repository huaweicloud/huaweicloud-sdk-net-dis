using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Bigdata.Dis.Sdk.DISCommon.DISWeb;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Auth
{
    public interface ISigner
    {
        void Sign<T>(IRequest<T> var1, ICredentials var2);
    }
}
