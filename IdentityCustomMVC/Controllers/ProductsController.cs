using IdentityCustomMVC.Data;
using IdentityCustomMVC.Entities;
using IdentityCustomMVC.Interfaces;
using IdentityCustomMVC.Migrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IdentityCustomMVC.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IProduct _IProduct;

        public ProductsController(AppDbContext context, IProduct IProduct)
        {
            _context = context;
            _IProduct = IProduct;
        }

        #region GETS

        public async Task<IActionResult> Index()
        {
            return _IProduct.List() != null ?
                        View(await _IProduct.List()) :
                        Problem("A entidade 'AppDbContext.Products' é nula.");
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _IProduct.GetEntityById(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        [Authorize(Roles = "Master, Admin, Gerente")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Master, Admin, Gerente")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _IProduct.GetEntityById(id);

            if (product == null)
                return NotFound();

            return View(product);
        }

        [Authorize(Roles = "Master, Admin, Gerente")]
        public async Task<IActionResult> Delete(int id)
        {

            var product = await _IProduct.GetEntityById(id);

            if (product == null)
                return NotFound();

            return View(product);
        }       

        #endregion

        #region POSTS

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _IProduct.Add(product);
                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _IProduct.Update(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_IProduct.Exists(product.Id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _IProduct.GetEntityById(id);

            if (product != null)
                await _IProduct.Delete(product);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteSeveral(string _registers)
        {
            try
            {
                string mensagem = string.Empty;

                string[] separators = { "," };
                string[] aAux;
                int[] aRegisters;

                aAux = _registers.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                aRegisters = Array.ConvertAll(aAux, int.Parse);

                for (int i = 0; i < aAux.Length; i++)
                {
                    var product = await _IProduct.GetEntityById(aRegisters[i]);

                    if (product != null)
                        await _IProduct.Delete(product);
                    else
                    {
                        ModelState.AddModelError("", "Role não encontrada.");
                        return RedirectToAction(nameof(Index));
                    }
                }

                return PartialView("ListProductsPartial", await _IProduct.List());
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
                return Json(ex.Message);
            }
        }

        #endregion

        #region OTHERS

        #endregion

    }
}
