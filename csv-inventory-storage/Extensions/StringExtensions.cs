using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CSVInventoryStorage.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Formats the string escaping non-numeric template expressions.
        /// </summary>
        /// <returns>The formatted string.</returns>
        /// <param name="str">The string to format.</param>
        /// <param name="args">Optional formatting arguments.</param>
        public static string FormatNumeric(this string str, params object[] args)
        {
            var final = Regex.Replace(str, "{([^0-9{}]*)}", (Match match) => "{" + match.Value + "}");
            final = args.Length == 0 ? final : string.Format(final, args);
            return Regex.Replace(final, "{{([^0-9{}]*)}}", (Match match) => match.Value);
        }
    }
}
