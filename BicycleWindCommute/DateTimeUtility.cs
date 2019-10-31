using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BicycleWindCommute
{
    public static class DateTimeUtility
    {
        public static DateTime UnixTimeStampToDateTime(string unixTimeStampString)
        {
            long unixTimeStamp;
            if (!Int64.TryParse(unixTimeStampString, out unixTimeStamp))
            {
                Console.WriteLine("Unix timestamp conversion failed for " + unixTimeStampString);
                return DateTime.MinValue;
            }

            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
