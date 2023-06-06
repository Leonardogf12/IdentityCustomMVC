using IdentityCustomMVC.Data;
using IdentityCustomMVC.Entities;
using IdentityCustomMVC.Interfaces;
using IdentityCustomMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityCustomMVC.Controllers
{
    public class AccountController : Controller
    {
        
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUser _IUser;


        public AccountController(UserManager<ApplicationUser> userManager,
                                    SignInManager<ApplicationUser> signManager,
                                    IUser IUser)
        {
            _signInManager = signManager;
            _userManager = userManager;
            _IUser = IUser;
        }

        #region GETS

        public async Task<IActionResult> Register()
        {
            return View();
        }

        public async Task<IActionResult> Login()
        {
            return View();
        }

        [Route("Change-Password")]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }

        [HttpGet]
        [Route("Account/AccessDenied")]
        public ActionResult AccessDenied()
        {
            return View();
        }

        #endregion

        #region POSTS

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var user = new IdentityUser
                //{
                //    UserName = model.Email,
                //    Email = model.Email,
                //};

                var user = new ApplicationUser
                {
                    Name = model.Name,
                    Cpf = model.Cpf,
                    BirthDate = model.BirthDate,
                    Address = model.Address,
                    Cep = model.Cep,
                    CellPhone   = model.CellPhone,
                    Status = model.Status,                    
                    UserName = model.Email,
                    Email = model.Email,
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "home");
                }

                foreach(var erro in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, erro.Description);
                }
            }

            return View(model);

        }

      

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email,
                    model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {                    
                    return RedirectToAction("Index", "home");
                }

               
                    ModelState.AddModelError(string.Empty,"Login inválido");
                
            }

            return View(model);

        }

        [HttpPost]
        [Route("Change-Password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
               
                var userId = _IUser.GetUserId();
                var user = await _userManager.FindByIdAsync(userId);
                
                var result =  await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if(result.Succeeded)
                {
                    ModelState.Clear();
                    ViewBag.IsSuccess = true;
                    return View();
                }

                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "home");
        }

        #endregion
    }
}
