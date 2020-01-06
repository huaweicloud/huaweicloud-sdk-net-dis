using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OBS.Runtime.Internal.Transform
{
    public static class CustomMarshallTransformations
    {
        public static long ConvertDateTimeToEpochMilliseconds(DateTime dateTime)
        {
            TimeSpan ts = new TimeSpan(dateTime.ToUniversalTime().Ticks - OBS.Util.AWSSDKUtils.EPOCH_START.Ticks);
            return (long)ts.TotalMilliseconds;
        }
    }
}
