using System.Text;
using System.Text.RegularExpressions;
using Zanella.Utilities.Extensions;

namespace Zanella.Utilities.Formatter
{
    /// <summary>
    /// Basic Formatter
    /// </summary>
    public class BasicFormatter
    {
        /// <summary>
        /// Format the string value in the specific format
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string? Format(string? value, EFormat format)
        {
            if (value == null)
                return null;

            var valueOnlyNumbers = value.ToOnlyNumbers(string.Empty) ?? string.Empty;

            switch (format)
            {
                case EFormat.Fone:
                    if (valueOnlyNumbers.Length <= 8)
                        return Regex.Replace(valueOnlyNumbers, @"(\w{4})(\w{4})", @"$1-$2", RegexOptions.Compiled);
                    if (valueOnlyNumbers.Length <= 10)
                        return Regex.Replace(valueOnlyNumbers, @"(\w{2})(\w{4})(\w{4})", @"($1) $2-$3", RegexOptions.Compiled);
                    if (valueOnlyNumbers.Length == 11)
                        return Regex.Replace(valueOnlyNumbers, @"(\w{2})(\w{1})(\w{4})(\w{4})", @"($1) $2 $3-$4", RegexOptions.Compiled);

                    return Regex.Replace(valueOnlyNumbers, @"(\w{2})(\w{2})(\w{1})(\w{4})(\w{4})", @"+$1 ($2) $3 $4-$5", RegexOptions.Compiled);
                case EFormat.CPF:
                    if (valueOnlyNumbers.Length < 11)
                        valueOnlyNumbers = valueOnlyNumbers.PadLeft(11, '0');
                    return Regex.Replace(valueOnlyNumbers, @"(\w{3})(\w{3})(\w{3})(\w{2})", @"$1.$2.$3-$4", RegexOptions.Compiled);
                case EFormat.CNPJ:
                    if (valueOnlyNumbers.Length < 14)
                        valueOnlyNumbers = valueOnlyNumbers.PadLeft(14, '0');
                    return Regex.Replace(valueOnlyNumbers, @"(\w{2})(\w{3})(\w{3})(\w{4})(\w{2})", @"$1.$2.$3/$4-$5", RegexOptions.Compiled);
                case EFormat.CPF_CNPJ:
                    if (valueOnlyNumbers.Length <= 11)
                    {
                        valueOnlyNumbers = valueOnlyNumbers.PadLeft(11, '0');
                        return Regex.Replace(valueOnlyNumbers, @"(\w{3})(\w{3})(\w{3})(\w{2})", @"$1.$2.$3-$4", RegexOptions.Compiled);
                    }
                    if (valueOnlyNumbers.Length < 14)
                        valueOnlyNumbers = valueOnlyNumbers.PadLeft(14, '0');
                    return Regex.Replace(valueOnlyNumbers, @"(\w{2})(\w{3})(\w{3})(\w{4})(\w{2})", @"$1.$2.$3/$4-$5", RegexOptions.Compiled);
                default:
                    return value;
            }
        }

        /// <summary>
        /// Format the string value in the specific format
        /// </summary>
        /// <param name="value"></param>
        /// <param name="format">Format string, including replacing char, example: <code>##.###-#/#</code></param>
        /// <param name="replaceChar"></param>
        /// <returns></returns>
        public static string? Format(string? value, string? format, char replaceChar = '#')
        {
            if (string.IsNullOrEmpty(format) || value == null)
                return value;

            var regValues = new StringBuilder();
            var regFormat = new StringBuilder();
            int position = 1;
            int qtdCaracteres = 0;
            foreach (var caractere in format!)
            {
                if (caractere == replaceChar)
                {
                    qtdCaracteres++;
                    continue;
                }

                regValues.Append("(\\w{").Append(qtdCaracteres).Append("})");
                qtdCaracteres = 0;
                regFormat.Append("$").Append(position).Append(caractere);
                position++;
            }

            return Regex.Replace(value, regValues.ToString(), regFormat.ToString());
        }
    }
}
