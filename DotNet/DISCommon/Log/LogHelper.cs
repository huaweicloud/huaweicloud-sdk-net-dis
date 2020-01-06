using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Log
{
    public class LogHelper
    {
        private static ILog logger;

        public static ILog GetInstance()
        {
            logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            return logger;
        }

        
    }
}
