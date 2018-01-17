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
            var final = Regex.Replace(str, "{([^0-9{}]*)}", match => "{" + match.Value + "}");
            final = args.Length == 0 ? final : string.Format(final, args);
            return Regex.Replace(final, "{{([^0-9{}]*)}}", match => match.Value);
        }

        /// <summary>
        /// Abbreviates the specified string to the <c>length</c> and appends <c>appendix</c>.
        /// </summary>
        /// <returns>The abbreviated string.</returns>
        /// <param name="str">String to abbreviate.</param>
        /// <param name="length">Length of the output string including <c>appendix</c>.</param>
        /// <param name="appendix">Appendix appended to the abbreviated string.</param>
        public static string Abbreviate(this string str, int length = 8, string appendix = "...")
        {
            if (str.Length <= length)
                return str;

            return str.Substring(0, length - appendix.Length) + appendix;
		}
    }
}
