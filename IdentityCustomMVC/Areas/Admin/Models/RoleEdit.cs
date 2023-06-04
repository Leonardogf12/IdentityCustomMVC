using Microsoft.AspNetCore.Identity;

namespace IdentityCustomMVC.Areas.Admin.Models
{
    public class RoleEdit //*PERMITE OBTER OS USUARIOS QUE PERTENCEM A CERTA ROLE.
    {
        public IdentityRole? Role { get; set; }
        public IEnumerable<IdentityUser>? Members { get; set; }
        public IEnumerable<IdentityUser>? NonMembers { get; set; }
    }
}
