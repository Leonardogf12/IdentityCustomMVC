using IdentityCustomMVC.Areas.Admin.Entities;
using IdentityCustomMVC.Areas.Admin.Models;
using IdentityCustomMVC.Data;
using IdentityCustomMVC.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text.RegularExpressions;

namespace IdentityCustomMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Master")]
    public class AdminRolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminRolesController(RoleManager<IdentityRole> roleManager,
                                    UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }


        #region GETS 

        public ActionResult Index()
        {
            return View(_roleManager.Roles);
        }

        public ActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> Update(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);

            List<ApplicationUser> members = new List<ApplicationUser>();
            List<ApplicationUser> nonMembers = new List<ApplicationUser>();

            foreach (ApplicationUser user in _userManager.Users.ToList())
            {

                var list = await _userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;

                list.Add(user);
            }

            return View(new RoleEdit
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }

        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ModelState.AddModelError("", "Role não encontrada.");
                return View("Index", _roleManager.Roles);
            }

            return View(role);
        }

        #endregion

        #region POSTS

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name")] IdentityRoleCustom obj)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(obj.Name));

                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else
            {
                return View(obj);
            }

            return RedirectToAction("Index");
            
        }

        [HttpPost]
        public async Task<IActionResult> Update(RoleModification model)
        {
            IdentityResult result;

            if (ModelState.IsValid)
            {
                foreach (string userId in model.AddIds ?? new string[] { })
                {
                    ApplicationUser user = await _userManager.FindByIdAsync(userId);

                    if (user != null)
                    {
                        result = await _userManager.AddToRoleAsync(user, model.RoleName);

                        if (!result.Succeeded)
                            Errors(result);
                    }
                }

                foreach (string userId in model.DeleteIds ?? new string[] { })
                {
                    ApplicationUser user = await _userManager.FindByIdAsync(userId);

                    if (user != null)
                    {
                        result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);

                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
            }

            if (ModelState.IsValid)
                return RedirectToAction(nameof(Index));
            else
                return await Update(model.RoleId);

        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role != null)
            {
                IdentityResult result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                    return RedirectToAction(nameof(Index));
                else
                    Errors(result);
            }
            else
            {
                ModelState.AddModelError("", "Role não encontrada.");
            }

            return View("Index", _roleManager.Roles);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSeveral(string _registros)
        {
            try
            {
                string mensagem = string.Empty;

                string[] separators = { "," };
                string[] aAux;
                
                aAux = _registros.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                
                for (int i = 0; i < aAux.Length; i++)
                {
                    var role = await _roleManager.FindByIdAsync(aAux[i]);
                    if (role != null)
                    {
                        IdentityResult result = await _roleManager.DeleteAsync(role);

                        if (!result.Succeeded)
                            Errors(result);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Role não encontrada.");
                    }
                }

                return PartialView("ListRolesPartial", _roleManager.Roles);
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Json(ex.Message);
            }
        }

        #endregion

        #region OTHERS

        public void Errors(IdentityResult result)
        {
            foreach (IdentityError erro in result.Errors)
            {
                ModelState.AddModelError("", erro.Description);
            }
        }

        #endregion
    }
}
