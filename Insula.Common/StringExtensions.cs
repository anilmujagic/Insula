using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace Insula.Common
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string target)
        {
            return string.IsNullOrEmpty(target);
        }

        public static bool IsNullOrWhiteSpace(this string target)
        {
            //return string.IsNullOrWhiteSpace(s);  //Not supported by Portable Library
            return string.IsNullOrEmpty(target) || target.Trim().Length == 0;
        }

        //public static string Format(this string target, params object[] args)
        //{
        //    return string.Format(target, args);
        //}

        public static string Format(this string target, IFormatProvider provider, params object[] args)
        {
            return string.Format(provider, target, args);
        }

        public static string FormatInvariant(this string target, params object[] args)
        {
            return string.Format(CultureInfo.InvariantCulture, target, args);
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

        /// <summary>
        /// Trims the string and replaces occurrences of multiple space characters with single space character.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
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
    }
}
