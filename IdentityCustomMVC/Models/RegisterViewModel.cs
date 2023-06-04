using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace IdentityCustomMVC.Models
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [DisplayName("Email")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [DataType(DataType.Password)]
        [DisplayName("Senha")]
        public string? Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Confirmar Senha")]
        [Compare("Password", ErrorMessage = "As senhas não conferem.")]
        public string? ConfirmPassword { get; set; }
    }
}
