using PerfumeStore.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeStore.Repository
{
    public class UnitOfWork : IDisposable
    {
        private PerfumeStoreActivityManagementContext _context;
        private GenericRepository<Perfume> _perfume;

        private bool disposed = false;

        public UnitOfWork(PerfumeStoreActivityManagementContext context)
        {
            _context = context;
        }

        public GenericRepository<Perfume> Perfumes
        {
            get
            {
                if (this._perfume == null)
                {
                    this._perfume = new GenericRepository<Perfume>(_context);
                }
                return _perfume;
            }
        }

        public async Task<(List<Perfume>, int)> GetPaginatedPerfumesAsync(int pageNumber, int pageSize)
        {
            var perfumes = await Perfumes.GetAllAsync(pageNumber, pageSize);
            var totalCount = await Perfumes.GetAllCountAsync(); // Get total count for pagination
            return (perfumes, totalCount); // Return both paginated data and total count
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
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
