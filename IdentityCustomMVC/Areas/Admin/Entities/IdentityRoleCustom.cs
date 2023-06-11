using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityCustomMVC.Areas.Admin.Entities
{
    [NotMapped]
    public class IdentityRoleCustom : IdentityRole
    {
        [Required(ErrorMessage = "Este campo é obrigatório.")]
        [DisplayName("Nome")]
        public string? Name { get; set; }
    }
}
