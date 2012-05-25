using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Insula.Common
{
    public static class TimeSpanExtensions
    {
        public static string ToLongString(this TimeSpan ts, bool alwaysShowMilliseconds = false)
        {
            var description = String.Format("{0}ms", ts.Milliseconds);

            if (ts.TotalSeconds >= 1)
            {
                if (alwaysShowMilliseconds)
                    description = ", " + description;
                else
                    description = String.Empty;

                description = String.Format("{0}s", ts.Seconds) + description;
            }

            if (ts.TotalMinutes >= 1)
                description = String.Format("{0}m, ", ts.Minutes) + description;

            if (ts.TotalHours >= 1)
                description = String.Format("{0}h, ", ts.Hours) + description;

            if (ts.TotalDays >= 1)
                description = String.Format("{0}d, ", ts.Days) + description;

            return description;
        }
    }
}
