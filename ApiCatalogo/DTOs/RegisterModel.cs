using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.DTOs
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Usuario é obrigatório")]
        public string?  Username { get; set; }
        [EmailAddress]
        [Required(ErrorMessage = "Email é obrigatório")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Senha é obrigatória")]
        public string? Password { get; set; }

    }
}
