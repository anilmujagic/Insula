using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Insula.Common
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string s)
        {
            return String.IsNullOrEmpty(s);
        }

        public static bool IsNullOrWhiteSpace(this string s)
        {
            //return String.IsNullOrWhiteSpace(s);  //Not supported by Portable Library
            return String.IsNullOrEmpty(s) || s.Trim().Length == 0;
        }

        public static bool ContainsAny(this string s, IEnumerable<string> values)
        {
            if (values.IsNullOrEmpty())
                return false;

            foreach (var value in values)
            {
                if (s.Contains(value))
                    return true;
            }

            return false;
        }

        public static bool ContainsAll(this string s, IEnumerable<string> values)
        {
            if (values.IsNullOrEmpty())
                return false;

            foreach (var value in values)
            {
                if (!s.Contains(value))
                    return false;
            }

            return true;
        }

        public static string CleanSpaces(this string s)
        {
            if (s.IsNullOrEmpty())
                return s;

            s = s.Trim();

            while (s.Contains("  "))
            {
                s = s.Replace("  ", " ");
            }

            return s;
        }

        /// <summary>
        /// Converts upper case letters, found on left side of the string, to lower case.
        /// Examples:
        /// "CustomerID" is converted to "customerID".
        /// "SalesOrderNo" is converted to "salesOrderNo".
        /// "UIThemeName" is converted to "uithemeName" if value of stopAfter parameter is 0 (converts until lowercase char is reached).
        /// "UIThemeName" is converted to "uiThemeName" if value of stopAfter parameter is 2.
        /// "UIThemeName" is converted to "uIThemeName" if value of stopAfter parameter is 1.
        /// </summary>
        /// <param name="s">String to convert</param>
        /// <param name="stopAfter">Number of characters after which converting stops. Default value is 0 (converts until lowercase char is reached).</param>
        public static string ToCamelCase(this string s, int stopAfter = 0)
        {
            if (stopAfter < 0)
                throw new ArgumentOutOfRangeException("stopAfter", "Value must be greather than or equal to zero.");

            if (s.IsNullOrWhiteSpace())
                return s;

            if (stopAfter == 0 || stopAfter > s.Length)
                stopAfter = s.Length;

            var position = 0;
            while (position < stopAfter)
            {
                if (s[position] == s.ToUpper()[position])
                {
                    var lower = s.Substring(position, 1).ToLower();
                    s = s.Remove(position, 1);
                    s = s.Insert(position, lower);
                }
                else
                {
                    break;
                }

                position++;
            }

            return s;
        }
    }
}
