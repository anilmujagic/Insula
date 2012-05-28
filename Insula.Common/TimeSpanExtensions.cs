using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Insula.Common
{
    public static class TimeSpanExtensions
    {
        public static string ToLongString(this TimeSpan target)
        {
            return ToLongString(target, false);
        }

        public static string ToLongString(this TimeSpan target, bool alwaysShowMilliseconds)
        {
            var description = target.Milliseconds.ToString(CultureInfo.InvariantCulture) + "ms";

            if (target.TotalSeconds >= 1)
            {
                if (alwaysShowMilliseconds)
                    description = ", " + description;
                else
                    description = String.Empty;

                description = target.Seconds.ToString(CultureInfo.InvariantCulture) + "s" + description;
            }

            if (target.TotalMinutes >= 1)
                description = target.Minutes.ToString(CultureInfo.InvariantCulture) + "m, " + description;

            if (target.TotalHours >= 1)
                description = target.Hours.ToString(CultureInfo.InvariantCulture) + "h, " + description;

            if (target.TotalDays >= 1)
                description = target.Days.ToString(CultureInfo.InvariantCulture) + "d, " + description;

            return description;
        }
    }
}
