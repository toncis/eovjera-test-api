using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace eOvjera.Common
{
    /// <summary>
    /// String extensions class.
    /// </summary>
    public static class StringExtensions  
    {
        /// <summary>
        /// Converts Pascal case notation to Snake case notation string.
        /// </summary>
        /// <param name="input">The Pascal case notaion string.</param>
        /// <returns>An Snake case representation of Pascal case string.</returns>
        public static string ToSnakeCase(this string input)
        {
            if (string.IsNullOrEmpty(input)) { return input; }

            var startUnderscores = Regex.Match(input, @"^_+");
            return startUnderscores + Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower();
        }
    }
}
