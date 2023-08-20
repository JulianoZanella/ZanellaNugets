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

            return BasicFormatter.Format(value, EFormat.CPF);
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

            return BasicFormatter.Format(value, EFormat.CNPJ);
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

            return BasicFormatter.Format(value, EFormat.CPF_CNPJ);
        }
    }
}
