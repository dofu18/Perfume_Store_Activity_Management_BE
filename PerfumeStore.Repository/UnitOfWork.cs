﻿using PerfumeStore.Repository.Models;
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
        private GenericRepository<User> _user;

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
