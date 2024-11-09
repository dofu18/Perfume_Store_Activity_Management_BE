using PerfumeStore.Repository.Model;
using PerfumeStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PerfumeStore.Service.Service
{
    public class TransactionService
    {
        private readonly UnitOfWork _unitOfWork;

        public TransactionService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Transaction>> GetTransactionByOrderId(Guid orderId)
        {
            return await _unitOfWork.Transactions
                .FindByCondition(c => c.OrderId == orderId)
                .ToListAsync();
        }


    }
}
