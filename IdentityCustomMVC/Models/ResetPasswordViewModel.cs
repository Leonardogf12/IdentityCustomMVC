using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IdentityCustomMVC.Models
{
    public class ResetPasswordViewModel
    {
        public int Id { get; set; }

        [DisplayName("Email")]
        public string? Email { get; set; }

        public string? Token { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [DataType(DataType.Password)]
        [DisplayName("Senha")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "As novas senhas não conferem.")]
        [DisplayName("Confirmar Senha")]
        public string? ConfirmPassword { get; set; }
               
    }
}
