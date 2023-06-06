using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

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


        [Required]
        [MaxLength(255)]
        [DisplayName("Nome")]
        public string? Name { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("CPF")]
        public string? Cpf { get; set; }

        [Required]
        [DisplayName("Data de Nascimento")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Required]
        [MaxLength(255)]
        [DisplayName("Cep")]
        public string? Cep { get; set; }

        [Required]
        [MaxLength(255)]
        [DisplayName("Endereço")]
        public string? Address { get; set; }
        
        [Required]
        [MaxLength(20)]
        [DisplayName("Celular")]
        public string? CellPhone { get; set; }
   
        [DisplayName("Status")]
        public bool? Status { get; set; }
    }
}
