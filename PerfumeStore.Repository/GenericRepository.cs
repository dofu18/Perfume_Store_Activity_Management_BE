using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PerfumeStore.Repository.Models;
using System.Linq.Expressions;

namespace PerfumeStore.Repository
{
    public class GenericRepository<T> where T : class
    {
        protected PerfumeStoreActivityManagementContext _context;
        private readonly DbSet<T> _dbSet;

        public GenericRepository()
        {
            _context ??= new PerfumeStoreActivityManagementContext();
        }

        public GenericRepository(PerfumeStoreActivityManagementContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        // Get a single object based on a condition
        public T GetByCondition(Expression<Func<T, bool>> expression)
        {
            return _dbSet.FirstOrDefault(expression);
        }

        

        public List<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }
        public async Task<List<T>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
        public async Task<int> GetAllCountAsync()
        {
            return await _context.Set<T>().CountAsync();
        }
        public void Create(T entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public async Task<int> CreateAsync(T entity)
        {
            _context.Add(entity);
            return await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;

            return await _context.SaveChangesAsync();
        }

        public bool Remove(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
            return true;
        }

        public async Task<bool> RemoveAsync(T entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public T GetById(int id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
            return entity;
            //return _context.Set<T>().Find(id);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
            return entity;
            //return await _context.Set<T>().FindAsync(id);
        }

        public T GetById(string code)
        {
            var entity = _context.Set<T>().Find(code);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
            return entity;
            //return _context.Set<T>().Find(code);
        }

        public async Task<T> GetByIdAsync(string code)
        {
            var entity = await _context.Set<T>().FindAsync(code);
            if (entity == null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
            return entity;
            //return await _context.Set<T>().FindAsync(code);
        }

        public T GetById(Guid code)
        {
            var entity = _context.Set<T>().Find(code);
            if (entity != null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
            return entity;
            //return _context.Set<T>().Find(code);
        }

        public async Task<T> GetByIdAsync(Guid code)
        {
            var entity = await _context.Set<T>().FindAsync(code);
            if (entity == null)
            {
                _context.Entry(entity).State = EntityState.Detached;
            }
            return entity;
            //return await _context.Set<T>().FindAsync(code);
        }

        public virtual async Task<bool> IsExist(Guid id)
        {
            return (await GetByIdAsync(id)) is not null;
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }

        public IQueryable<T> GetAllSorted<TKey>(Expression<Func<T, TKey>> keySelector, bool descending = false)
        {
            return descending ? _dbSet.OrderByDescending(keySelector) : _dbSet.OrderBy(keySelector);
        }

        public IQueryable<T> GetPaged(int pageNumber, int pageSize)
        {
            return _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }


        #region Separating asign entity and save operators        

        public void PrepareCreate(T entity)
        {
            _context.Add(entity);
        }

        public void PrepareUpdate(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
        }

        public void PrepareRemove(T entity)
        {
            _context.Remove(entity);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
    #endregion Separating asign entity and save operators
}
