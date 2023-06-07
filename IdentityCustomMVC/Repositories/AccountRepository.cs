using IdentityCustomMVC.Entities;
using IdentityCustomMVC.Interfaces;
using IdentityCustomMVC.Models;
using IdentityCustomMVC.Services;
using Microsoft.AspNetCore.Identity;

namespace IdentityCustomMVC.Repositories
{
    public class AccountRepository : IAccount
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;      

        public AccountRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

    }
}
