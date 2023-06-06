using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IdentityCustomMVC.Models
{
    public class ChangePasswordViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [DisplayName("Senha Atual")]
        [DataType(DataType.Password)]
        public string? CurrentPassword { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [DisplayName("Nova Senha")]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [DisplayName("Confirmar Nova Senha")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "As novas senhas não conferem.")]
        public string? ConfirmPassword { get; set; }
    }
}
