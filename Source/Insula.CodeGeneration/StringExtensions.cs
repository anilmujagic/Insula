﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Insula.Common;

namespace Insula.CodeGeneration
{
    public static class StringExtensions
    {
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
        public static string ToCamelCase(this string target, int stopAfter = 0)
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
                if (target[position] == target.ToUpperInvariant()[position])
                {
                    var lower = target.Substring(position, 1).ToLowerInvariant();
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
