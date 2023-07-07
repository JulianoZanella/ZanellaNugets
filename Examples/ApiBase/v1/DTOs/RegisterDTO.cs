using System.ComponentModel.DataAnnotations;

namespace ApiBase.v1.DTOs
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,8}$", ErrorMessage = "Password most be complex")]
        public string? Password { get; set; }
    }
}
