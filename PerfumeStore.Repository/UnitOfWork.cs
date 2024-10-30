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
        private GenericRepository<Order> _order;
        private GenericRepository<Transaction> _transaction;
        private GenericRepository<PerfumeCharacteristic> _characteristic;
        private GenericRepository<Category> _category;
        private GenericRepository<ActivityLog> _activityLog;

        private bool disposed = false;

        public UnitOfWork(PerfumeStoreContext context)
        {
            _context = context;
        }

        public GenericRepository<ActivityLog> ActivityLogs
        {
            get
            {
                if (_activityLog == null)
                {
                    _activityLog = new GenericRepository<ActivityLog>(_context);
                }
                return _activityLog;
            }
        }

        public GenericRepository<Category> Category
        {
            get
            {
                if (this._category == null)
                {
                    this._category = new GenericRepository<Category>(_context);
                }
                return this._category;
            }
        }

        public GenericRepository<PerfumeCharacteristic> PerfumeCharacteristics
        {
            get
            {
                if (this._characteristic == null)
                {
                    this._characteristic = new GenericRepository<PerfumeCharacteristic>(_context);
                }
                return this._characteristic;
            }
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

        public GenericRepository<PerfumeProduct> PerfumeProducts
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

        public GenericRepository<Order> Orders
        {
            get
            {
                if (this._order == null)
                {
                    this._order = new GenericRepository<Order>(_context);
                }
                return _order;
            }
        }

        // Implement CompleteOrder in UnitOfWork
        public int CompleteOrder(Order order, User user)
        {
            using (var transactionScope = _context.Database.BeginTransaction())
            {
                try
                {
                    // Update the order status to completed
                    order.Status = "Done";
                    //order.UserId = user.UserId;  // Example, if tracking the user who completed it
                    order.OrderDate = DateTime.Now;

                    Orders.Update(order);  // Update the order in the repository

                    // Process each order item and create corresponding transactions
                    foreach (var orderItem in order.OrderItems)
                    {
                        var perfume = orderItem.Perfume;
                        decimal perfumePrice = orderItem.Price * orderItem.Quantity;

                        var newTransaction = new Transaction
                        {
                            TransactionId = Guid.NewGuid(),
                            OrderId = order.OrderId,
                            PaymentMethod = order.PaymentMethod,
                            PaymentStatus = "Completed",
                            TransactionDate = DateTime.Now
                        };

                        Transactions.Create(newTransaction);  // Add the transaction via the repository
                    }
                        
                    // Save all changes
                    int result = SaveChanges();

                    // Commit the transaction
                    transactionScope.Commit();

                    return result;
                }
                catch (Exception)
                {
                    // Rollback in case of error
                    transactionScope.Rollback();
                    throw;
                }
            }
        }

        public GenericRepository<Transaction> Transactions
        {
            get
            {
                if (this._transaction == null)
                {
                    this._transaction = new GenericRepository<Transaction>(_context);
                }
                return _transaction;
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

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }


        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
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
