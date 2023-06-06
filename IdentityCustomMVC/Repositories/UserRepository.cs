using IdentityCustomMVC.Interfaces;
using System.Security.Claims;

namespace IdentityCustomMVC.Repositories
{
    public class UserRepository : IUser
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserRepository(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetUserId()
        {
            return _contextAccessor.HttpContext.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
