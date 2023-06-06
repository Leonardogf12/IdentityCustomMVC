using IdentityCustomMVC.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityCustomMVC.Areas.Admin.Models
{
    public class RoleEdit //*PERMITE OBTER OS USUARIOS QUE PERTENCEM A CERTA ROLE.
    {
        public IdentityRole? Role { get; set; }
        public IEnumerable<ApplicationUser>? Members { get; set; }
        public IEnumerable<ApplicationUser>? NonMembers { get; set; }
    }
}
