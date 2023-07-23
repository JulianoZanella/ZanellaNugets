using System.ComponentModel.DataAnnotations;
using Zanella.Utilities.Validation;

namespace Utilities.Test.Models
{
    internal class Example
    {
        [MinLength(3)]
        [Display(Name = "Nome")]
        public string Name { get; set; } = string.Empty;

        [EmailAddress]
        public string? Email { get; set; }

        [CPF_CNPJ_Document(EDocumentType.CPF)]
        public string? CPF { get; set; }

        [Range(1, 150)]
        [Display(Name = "Idade")]
        public int Age { get; set; }

    }
}
