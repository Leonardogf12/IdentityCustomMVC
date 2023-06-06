using IdentityCustomMVC.Data;
using IdentityCustomMVC.Entities;
using IdentityCustomMVC.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IdentityCustomMVC.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProduct
    {
        private readonly DbContextOptions<AppDbContext> _context;

        public ProductRepository()
        {
            _context = new DbContextOptions<AppDbContext>();
        }

        public bool Exists(int Id)
        {            
            using (var db = new AppDbContext(_context))
            {                              
                var idd = db.Products.Any(e => e.Id == Id);

                if (idd)
                    return true;
                else
                    return false;
            }
        }
    }
}
