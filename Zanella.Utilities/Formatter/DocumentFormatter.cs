using Zanella.Utilities.Extensions;

namespace Zanella.Utilities.Formatter
{
    /// <summary>
    /// Masks Formatter
    /// </summary>
    public static class DocumentFormatter
    {
        /// <summary>
        /// Format CPF in 000.000.000-00
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string? FormatCPF(long? value, string? defaultValue = null)
        {
            return FormatCPF(value?.ToString(), defaultValue);
        }

        /// <summary>
        /// Format CPF in 000.000.000-00
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string? FormatCPF(string? value, string? defaultValue = null)
        {
            if (value == null)
                return defaultValue;

            value = value.ToOnlyNumbers(defaultValue);

            if (string.IsNullOrEmpty(value))
                return defaultValue;

            if (value!.Length < 11)
                value = value.PadLeft(11, '0');

            return $"{value.Substring(0, 3)}.{value.Substring(3, 3)}.{value.Substring(6, 3)}-{value.Substring(9, 2)}";
        }

        /// <summary>
        /// Format CNPJ in 00.000.000/0000-00
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string? FormatCNPJ(long? value, string? defaultValue = null)
        {
            return FormatCNPJ(value?.ToString(), defaultValue);
        }

        /// <summary>
        /// Format CNPJ in 00.000.000/0000-00
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string? FormatCNPJ(string? value, string? defaultValue = null)
        {
            if (value == null)
                return defaultValue;

            value = value.ToOnlyNumbers(defaultValue) ?? defaultValue;

            if (string.IsNullOrEmpty(value))
                return defaultValue;

            if (value!.Length < 14)
                value = value.PadLeft(14, '0');

            return $"{value.Substring(0, 2)}.{value.Substring(2, 3)}.{value.Substring(5, 3)}/{value.Substring(8, 4)}-{value.Substring(12, 2)}";
        }

        /// <summary>
        /// Format CPF in 000.000.000-00
        /// or
        /// Format CNPJ in 00.000.000/0000-00
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string? FormatCPForCNPJ(long? value, string? defaultValue = null)
        {
            return FormatCPForCNPJ(value?.ToString(), defaultValue);
        }

        /// <summary>
        /// Format CPF in 000.000.000-00
        /// or
        /// Format CNPJ in 00.000.000/0000-00
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string? FormatCPForCNPJ(string? value, string? defaultValue = null)
        {
            if (value == null)
                return defaultValue;

            value = value.ToOnlyNumbers(defaultValue) ?? defaultValue;
            if (string.IsNullOrEmpty(value))
                return defaultValue;

            if (value!.Length <= 11)
                return FormatCPF(value, defaultValue);

            return FormatCNPJ(value, defaultValue);
        }
    }
}
