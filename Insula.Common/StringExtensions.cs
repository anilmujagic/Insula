using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Insula.Common
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string target)
        {
            return String.IsNullOrEmpty(target);
        }

        public static bool IsNullOrWhiteSpace(this string target)
        {
            //return String.IsNullOrWhiteSpace(s);  //Not supported by Portable Library
            return String.IsNullOrEmpty(target) || target.Trim().Length == 0;
        }

        public static bool ContainsAny(this string target, IEnumerable<string> values)
        {
            if (values.IsNullOrEmpty())
                return false;

            foreach (var value in values)
            {
                if (target.Contains(value))
                    return true;
            }

            return false;
        }

        public static bool ContainsAll(this string target, IEnumerable<string> values)
        {
            if (values.IsNullOrEmpty())
                return false;

            foreach (var value in values)
            {
                if (!target.Contains(value))
                    return false;
            }

            return true;
        }

        public static string CleanSpaces(this string target)
        {
            if (target.IsNullOrEmpty())
                return target;

            target = target.Trim();

            while (target.Contains("  "))
            {
                target = target.Replace("  ", " ");
            }

            return target;
        }

        /// <summary>
        /// Converts upper case letters, found on left side of the string, to lower case, until lowercase char is reached.
        /// </summary>
        /// <param name="target">String to convert</param>
        public static string ToCamelCase(this string target)
        {
            return ToCamelCase(target, 0);
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
        /// <param name="target">String to convert</param>
        /// <param name="stopAfter">Number of characters after which converting stops. Default value is 0 (converts until lowercase char is reached).</param>
        public static string ToCamelCase(this string target, int stopAfter)
        {
            if (stopAfter < 0)
                throw new ArgumentOutOfRangeException("stopAfter", "Value must be greather than or equal to zero.");

            if (target.IsNullOrWhiteSpace())
                return target;

            if (stopAfter == 0 || stopAfter > target.Length)
                stopAfter = target.Length;

            var position = 0;
            while (position < stopAfter)
            {
                if (target[position] == target.ToUpper()[position])
                {
                    var lower = target.Substring(position, 1).ToLower();
                    target = target.Remove(position, 1);
                    target = target.Insert(position, lower);
                }
                else
                {
                    break;
                }

                position++;
            }

            return target;
        }
    }
}
