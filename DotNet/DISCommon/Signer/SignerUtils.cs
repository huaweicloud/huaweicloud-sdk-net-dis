using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Com.Bigdata.Dis.Sdk.DISCommon.Signer
{
    public class SignerUtils
    {
        private static readonly string dateFormatter = "yyyyMMdd";
        private static readonly string timeFormatter = "yyyyMMdd\'T\'HHmmss\'Z\'";

        public static string FormatDateStamp(long timeMilli)
        {
            //var time = TimeSpan.FromMilliseconds(timeMilli);
            //var dt = new DateTime(time.Ticks);
         
            //return dt.ToString(dateFormatter);

            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0)); // 当地时区
            DateTime dt = startTime.AddMilliseconds(timeMilli);
            return dt.ToString(dateFormatter);

        }

        public static string FormatTimestamp(long timeMilli)
        {
            //var time = TimeSpan.FromMilliseconds(timeMilli);
            //var dt = new DateTime(time.Ticks);
            //return dt.ToString(timeFormatter);
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0)); // 当地时区
            DateTime dt = startTime.AddMilliseconds(timeMilli);
            return dt.ToString(timeFormatter);
        }

        public static long ParseMillis(string signDate)
        {
            var dt = DateTime.ParseExact(signDate, timeFormatter, CultureInfo.InvariantCulture).ToUniversalTime();
            return dt.Millisecond;
        }
    }
}
