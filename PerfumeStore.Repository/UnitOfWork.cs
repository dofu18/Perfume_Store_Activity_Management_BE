using PerfumeStore.Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeStore.Repository
{
    public class UnitOfWork : IDisposable
    {
        private PerfumeStoreContext _context;
        private GenericRepository<PerfumeProduct> _perfume;
        private GenericRepository<User> _user;
        private GenericRepository<Cart> _cart;

        private bool disposed = false;

        public UnitOfWork(PerfumeStoreContext context)
        {
            _context = context;
        }

        public GenericRepository<Cart> Carts
        {
            get
            {
                if (this._cart == null)
                {
                    this._cart = new GenericRepository<Cart>(_context);
                }
                return this._cart;
            }
        }

        public GenericRepository<PerfumeProduct> Perfumes
        {
            get
            {
                if (this._perfume == null)
                {
                    this._perfume = new GenericRepository<PerfumeProduct>(_context);
                }
                return _perfume;
            }
        }

        public GenericRepository<User> Users
        {
            get
            {
                if (this._user == null)
                {
                    this._user = new GenericRepository<User>(_context);
                }
                return _user;
            }
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Save()
        {
            _context.SaveChanges();
        }


        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
