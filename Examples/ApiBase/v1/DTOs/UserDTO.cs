using System.ComponentModel.DataAnnotations;

namespace ApiBase.v1.DTOs
{
    public class UserDTO
    {
        [Required]
        public string Token { get; set; } = string.Empty;
    }
}
