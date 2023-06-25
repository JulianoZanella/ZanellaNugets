using System.Text.RegularExpressions;
using Zanella.Utilities.Extensions;

namespace Zanella.Utilities.Validation
{
    public static class DocumentValidation
    {
        /// <summary>
        /// Verify CPF is valid
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public static bool IsValidCPF(long? cpf)
        {
            return IsValidCPF(cpf?.ToString());
        }

        /// <summary>
        /// Verify CPF is valid
        /// </summary>
        /// <param name="cpf"></param>
        /// <returns></returns>
        public static bool IsValidCPF(string? cpf)
        {
            cpf = cpf.ToOnlyNumbers();

            if (string.IsNullOrEmpty(cpf) || cpf.Length > 11)
                return false;

            if (cpf.Length < 11)
                cpf = cpf.PadLeft(11, '0');

            var rgx = new Regex("\\A(\\d)\\1+\\Z");
            if (rgx.IsMatch(cpf))
                return false;

            var firstDV = int.Parse(cpf.Substring(9, 1));
            var secondDV = int.Parse(cpf.Substring(10, 1));
            cpf = cpf[..9];
            var firstCalculatedDV = Module11(cpf, 11);
            var sexondCalculatedDV = Module11(cpf + firstDV.ToString(), 11);

            return firstDV == firstCalculatedDV && secondDV == sexondCalculatedDV;
        }

        /// <summary>
        /// Verify CNPJ is valid
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        public static bool IsValidCPNJ(long? cnpj)
        {
            return IsValidCPNJ(cnpj?.ToString());
        }

        /// <summary>
        /// Verify CNPJ is valid
        /// </summary>
        /// <param name="cnpj"></param>
        /// <returns></returns>
        public static bool IsValidCPNJ(string? cnpj)
        {
            cnpj = cnpj.ToOnlyNumbers();

            if (string.IsNullOrEmpty(cnpj) || cnpj.Length > 14)
                return false;

            if (cnpj.Length < 14)
                cnpj = cnpj.PadLeft(14, '0');

            var rgx = new Regex("\\A(\\d)\\1+\\Z");
            if (rgx.IsMatch(cnpj))
                return false;

            var firstDV = int.Parse(cnpj.Substring(12, 1));
            var secondDV = int.Parse(cnpj.Substring(13, 1));
            cnpj = cnpj[..12];
            var firstCalculatedDV = Module11(cnpj, 9);
            var secondCalculatedDV = Module11(cnpj + firstDV.ToString(), 9);

            return firstDV == firstCalculatedDV && secondDV == secondCalculatedDV;
        }

        /// <summary>
        /// Verify CPF or CNPJ is valid
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static bool IsValidCPForCPNJ(long? doc)
        {
            return IsValidCPForCPNJ(doc?.ToString());
        }

        /// <summary>
        /// Verify CPF or CNPJ is valid
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static bool IsValidCPForCPNJ(string? doc)
        {
            doc = doc.ToOnlyNumbers();

            if (doc == null)
                return false;

            if (doc.Length <= 11)
                return IsValidCPF(doc);

            if (doc.Length <= 14)
                return IsValidCPNJ(doc);

            return false;
        }

        private static int Module11(string number, int multiplyLimit)
        {
            number = number.ToOnlyNumbers(string.Empty) ?? string.Empty;
            int weight = 2;
            int soma = 0;

            for (int i = number.Length - 1; i >= 0; i--)
            {
                var value = int.Parse(number[i].ToString());
                soma += (value * weight);
                weight++;
                if (weight > multiplyLimit)
                    weight = 2;
            }

            var rest = soma % 11;
            if (rest == 0 || rest == 1)
                rest = 0;
            else
                rest = 11 - rest;

            return rest;
        }
    }
}
