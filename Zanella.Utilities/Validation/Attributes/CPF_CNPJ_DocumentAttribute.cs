﻿using System.ComponentModel.DataAnnotations;

namespace Zanella.Utilities.Validation
{
    /// <summary>
    /// Validate if document is valid
    /// </summary>
    public class CPF_CNPJ_DocumentAttribute : ValidationAttribute
    {
        /// <summary>
        /// Type of Document
        /// </summary>
        public EDocumentType DocumentType { get; set; }

        /// <summary>
        /// Validate if document is valid
        /// </summary>
        public CPF_CNPJ_DocumentAttribute() : this(EDocumentType.CPF_or_CNPJ)
        {
        }

        /// <summary>
        /// Validate if document is valid
        /// </summary>
        /// <param name="documentType">Type of Document</param>
        public CPF_CNPJ_DocumentAttribute(EDocumentType documentType)
        {
            DocumentType = documentType;
            ErrorMessage ??= DocumentType switch
            {
                EDocumentType.CPF_or_CNPJ => "{0} is an invalid CPF/CNPJ",
                EDocumentType.CPF => "{0} is an invalid CPF",
                EDocumentType.CNPJ => "{0} is an invalid CNPJ",
                _ => null
            };
        }

        /// <summary>
        /// Is Valid
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool IsValid(object? value)
        {
            var cpfCnpj = value?.ToString();

            if (string.IsNullOrEmpty(cpfCnpj))
                return true;

            return DocumentType switch
            {
                EDocumentType.CPF_or_CNPJ => DocumentValidation.IsValidCPForCPNJ(cpfCnpj),
                EDocumentType.CPF => DocumentValidation.IsValidCPF(cpfCnpj),
                EDocumentType.CNPJ => DocumentValidation.IsValidCPNJ(cpfCnpj),
                _ => false,
            };
        }
    }

    /// <summary>
    /// Document Type
    /// </summary>
    public enum EDocumentType
    {
        /// <summary>
        /// CPF or CNPJ
        /// </summary>
        CPF_or_CNPJ,

        /// <summary>
        /// CPF
        /// </summary>
        CPF,

        /// <summary>
        /// CNPJ
        /// </summary>
        CNPJ,
    }
}
