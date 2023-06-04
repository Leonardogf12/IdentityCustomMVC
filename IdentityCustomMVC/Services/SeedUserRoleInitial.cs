using Microsoft.AspNetCore.Identity;
using System.Data;

namespace IdentityCustomMVC.Services
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {

        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<IdentityUser> userManager,
                                    RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }


        #region CRIACAO DAS ROLES INICIAIS E DO USUARIO MASTER.

        public async Task SeedRoleAsync()
        {
            if (!await _roleManager.RoleExistsAsync("Comum"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Comum";
                role.NormalizedName = "COMUM";
                role.ConcurrencyStamp = Guid.NewGuid().ToString();

                IdentityResult result = await _roleManager.CreateAsync(role);
            }

            if (!await _roleManager.RoleExistsAsync("Gerente"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Gerente";
                role.NormalizedName = "GERENTE";
                role.ConcurrencyStamp = Guid.NewGuid().ToString();

                IdentityResult result = await _roleManager.CreateAsync(role);
            }

            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                role.ConcurrencyStamp = Guid.NewGuid().ToString();

                IdentityResult result = await _roleManager.CreateAsync(role);
            }

            if (!await _roleManager.RoleExistsAsync("Master"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Master";
                role.NormalizedName = "MASTER";
                role.ConcurrencyStamp = Guid.NewGuid().ToString();

                IdentityResult result = await _roleManager.CreateAsync(role);
            }
        }

        public async Task SeedUserAsync()
        {
            if (await _userManager.FindByEmailAsync("master@master.com") == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "master@master.com";
                user.Email = "master@master.com";
                user.NormalizedUserName = "MASTER@MASTER.COM";
                user.NormalizedEmail = "MASTER@MASTER.COM";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = await _userManager.CreateAsync(user, "Net@2023");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Master");
                }
            }

            if (await _userManager.FindByEmailAsync("admin@admin.com") == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "admin@admin.com";
                user.Email = "admin@admin.com";
                user.NormalizedUserName = "ADMIN@ADMIN.COM";
                user.NormalizedEmail = "ADMIN@ADMIN.COM";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString();

                IdentityResult result = await _userManager.CreateAsync(user, "Net@2023");

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }

        #endregion
    }
}
