﻿using System;

namespace Utilities.Formatter
{
    public static class DocumentFormatter
    {
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

            if (value == null || value.Length != 11)
                return defaultValue;

            return $"{value[..3]}.{value.Substring(3, 3)}.{value.Substring(6, 3)}-{value.Substring(9, 2)}";
        }

        /// <summary>
        /// Format CNPJ in 00.000.000/0000-00
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string? FormataCNPJ(string? value, string? defaultValue = null)
        {
            if (value == null)
                return defaultValue;

            value = value.ToOnlyNumbers(defaultValue) ?? defaultValue;

            if (value == null || value.Length != 14)
                return value;

            return $"{value[..2]}.{value.Substring(2, 3)}.{value.Substring(5, 3)}/{value.Substring(8, 4)}-{value.Substring(12, 2)}";
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
            if (value == null)
                return defaultValue;

            if (value.Length < 11)
            {
                value = value.PadLeft(11, '0');
            }
            else if (value.Length > 11 && value.Length < 14)
            {
                value = value.PadLeft(14, '0');
            }

            if (value.Length != 11 && value.Length != 14)
                return defaultValue;

            return value.Length == 11 ? FormatCPF(value, defaultValue) : FormataCNPJ(value, defaultValue);
        }
    }
}