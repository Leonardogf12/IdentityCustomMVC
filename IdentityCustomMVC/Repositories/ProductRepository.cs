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
                return (db.Products.Any(e => e.Id == Id)) == true ? true : false;               
            }
        }
    }
}
