using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Auth
{
    public interface IRegionSigner : ISigner
    {
        void SetRegionName(String var1);
    }
}
