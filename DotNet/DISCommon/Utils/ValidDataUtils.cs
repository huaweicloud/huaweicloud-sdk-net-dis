using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Utils
{
  public  class ValidDataUtils
    {
        public static bool DisValidateDisString(string param, int maxLen)
        {
            int loop = 0;
            if(param.Length > maxLen)
            {
                return false;
            }

            for(loop = 0; loop< param.Length; loop++)
            {
                if((param[loop] >= 'a' && param[loop] <= 'z') || (param[loop] >= 'A' && param[loop] <= 'Z') || (param[loop] >= '0' && param[loop] <= '9') || (param[loop] == '-') || (param[loop] == '_'))
                {
                    continue;
                }
                return false;
            }
            return true;
        }

    }
}
