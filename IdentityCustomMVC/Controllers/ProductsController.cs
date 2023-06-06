using IdentityCustomMVC.Data;
using IdentityCustomMVC.Entities;
using IdentityCustomMVC.Interfaces;
using Microsoft.AspNetCore.Authorization;
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

        #endregion

        #region OTHERS

        #endregion

    }
}
