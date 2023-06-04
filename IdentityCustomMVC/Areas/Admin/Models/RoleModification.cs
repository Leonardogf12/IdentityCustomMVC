using System.ComponentModel.DataAnnotations;

namespace IdentityCustomMVC.Areas.Admin.Models
{
    public class RoleModification //*PERMITE MODIFICAR UMA ROLE.
    {
        [Required]
        public string? RoleName { get; set; }
        public string? RoleId { get; set; }
        public string[]? AddIds { get; set; }
        public string[]? DeleteIds { get; set; }
    }
}
