using System.Text.RegularExpressions;

namespace Zanella.Utilities.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Remove all not numeric caracteres
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue">Default value case is null or empty or don't have any numbers</param>
        /// <returns>Only numbers string</returns>
        public static string? ToOnlyNumbers(this string? value, string? defaultValue = null)
        {
            if (value == null)
                return defaultValue;

            var returned = Regex.Replace(value, @"\D", string.Empty, RegexOptions.Compiled);

            return returned.Length == 0 ? defaultValue : returned;
        }
    }
}
