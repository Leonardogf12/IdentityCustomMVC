using IdentityCustomMVC.Data;
using IdentityCustomMVC.Entities;
using IdentityCustomMVC.Interfaces;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Mail;
using System.Security.Claims;

namespace IdentityCustomMVC.Repositories
{
    public class UserRepository : IUser
    {
        private readonly DbContextOptions<AppDbContext> _context;
        private readonly IHttpContextAccessor _contextAccessor;
        

        public UserRepository(IHttpContextAccessor contextAccessor)
        {
            _context = new DbContextOptions<AppDbContext>();
            _contextAccessor = contextAccessor;

        }

        public string GetUserId()
        {
            return _contextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        //public bool EmailConfirmedUser(ApplicationUser user)
        //{
        //    using (var db = new AppDbContext(_context))
        //    {
        //        return (db.Users.Any(e => e.EmailConfirmed == Id)) == true ? true : false;
        //    }
        //}
    }
}
