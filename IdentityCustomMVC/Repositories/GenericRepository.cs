using IdentityCustomMVC.Data;
using IdentityCustomMVC.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace IdentityCustomMVC.Repositories
{
    public class GenericRepository<T> : IGeneric<T>, IDisposable where T : class
    {
        private readonly DbContextOptions<AppDbContext> _context;

        public GenericRepository()
        {
            _context = new DbContextOptions<AppDbContext>();
        }

        public async Task Add(T Object)
        {
            using (var db = new AppDbContext(_context)){

                await db.Set<T>().AddAsync(Object);
                await db.SaveChangesAsync();
            }
        }

        public async Task Delete(T Object)
        {
            using (var db = new AppDbContext(_context))
            {
                db.Set<T>().Remove(Object);
                await db.SaveChangesAsync();
            }
        }

        public async Task<T> GetEntityById(int Id)
        {
            using (var db = new AppDbContext(_context))
            {
                return await db.Set<T>().FindAsync(Id);                        
            }
        }

        public async Task<List<T>> List()
        {
            using (var db = new AppDbContext(_context))
            {
              return await db.Set<T>().ToListAsync();              
            }
        }

        public async Task Update(T Object)
        {
            using (var db = new AppDbContext(_context))
            {
                db.Set<T>().Update(Object);
                await db.SaveChangesAsync();
            }
        }
        
        #region DISPOSE
        //*ESTA CONFIGURAÇÃO É PADRÃO DA MICROSOFT.

        bool disposedValue = false;

        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposedValue)
                return;

            if (disposing)
                handle.Dispose();

            disposedValue = true;
        }
        
        #endregion
    }
}
