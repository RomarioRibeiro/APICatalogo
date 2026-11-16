using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.DTOs
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Usuario é obrigatório")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória")]
        public string? Password { get; set; }
    }
}
