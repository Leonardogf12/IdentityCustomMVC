using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace IdentityCustomMVC.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [DisplayName("Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [DataType(DataType.Password)]
        [DisplayName("Senha")]
        public string? Password { get; set; }
        
        [DisplayName("Lembrar-me")]       
        public bool RememberMe { get; set; }
    }
}
