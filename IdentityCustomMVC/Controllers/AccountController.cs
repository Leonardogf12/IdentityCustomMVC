using IdentityCustomMVC.Entities;
using IdentityCustomMVC.Interfaces;
using IdentityCustomMVC.Interfaces.Emails;
using IdentityCustomMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Encodings.Web;

namespace IdentityCustomMVC.Controllers
{

    public class AccountController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUser _IUser;
        private readonly IAccount _IAccount;               
        private readonly IEmailService _IEmailService;


        public AccountController(UserManager<ApplicationUser> userManager,
                                    SignInManager<ApplicationUser> signManager,
                                    IUser IUser,
                                    IAccount IAccount,                                                                
                                    IEmailService IEmailService)
        {
            _signInManager = signManager;
            _userManager = userManager;
            _IUser = IUser;
            _IAccount = IAccount;    
            _IEmailService = IEmailService;
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

        [Route("change-password")]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }

        [Route("Account/AccessDenied")]
        public ActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("forgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("reset-password")]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "O token para reset da senha não é válido.");
                return View();
            }
            else
            {
                var model = new ResetPasswordViewModel { Token = token, Email = email };
                return View(model);
            }
            
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
                    CellPhone = model.CellPhone,
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

                foreach (var erro in result.Errors)
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


                ModelState.AddModelError(string.Empty, "Login inválido");

            }

            return View(model);

        }

        [HttpPost]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {

                var userId = _IUser.GetUserId();
                var user = await _userManager.FindByIdAsync(userId);

                var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    ModelState.Clear();
                    ViewBag.IsSuccess = true;
                    return View();
                }

                foreach (var error in result.Errors)
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
        [HttpPost]
        [AllowAnonymous]
        [Route("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null /*&& await _userManager.IsEmailConfirmedAsync(user)*/)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                                        
                    var urlConfirmation = Url.Action(nameof(ResetPassword), "Account", new { token, email = model.Email }, Request.Scheme);

                    var message = new StringBuilder();

                    message.Append($"<p>Olá, {user.Name}.</p>");
                    message.Append("<p>Houve uma solicitação de redefinição de " +
                        "senha para seu usuário em nosso sistema. Se não foi você que fez " +
                        "a solicitação, apenas ignore este email. Caso tenha sido você, clique " +
                        "no link abaixo para criar sua nova senha.</p>");
                    message.Append($"<p><a href='{urlConfirmation}'>Redefinir Senha</a></p>");
                    message.Append($"<p>Atensiosamente, <br>Equipe de Suporte</br></p>");

                    await _IEmailService.SendEmailAsync(user.Email,
                                                        "Redefinição de Senha",
                                                        message.ToString(),"");
                   
                    return View("ForgotPasswordConfirmation");

                }
                return View("ForgotPasswordConfirmation");
            }

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

                    if (result.Succeeded)
                    {
                        return View("ResetPasswordConfirmation");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(model);
                }
                return View("ResetPasswordConfirmation");
            }

            return View(model);
        }

        #endregion

    }
}
