using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityCustomMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Master")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
