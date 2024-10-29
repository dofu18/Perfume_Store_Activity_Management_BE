using PerfumeStore.Repository.Model;
using PerfumeStore.Repository;
using PerfumeStore.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace PerfumeStore.Service.Service
{
    public class CartService
    {
        private readonly UnitOfWork _unitOfWork;

        public CartService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddToCartAsync(Guid userId, Guid perfumeId, int quantity)
        {
            // Find the existing cart entry for the same perfume and user
            var cartEntry = (await _unitOfWork.Carts
                .GetAllAsync(1,10)).FirstOrDefault(c => c.UserId == userId && c.PerfumeId == perfumeId);

            if (cartEntry != null)
            {
                // If the entry exists, update the quantity
                cartEntry.Quantity += quantity;
                cartEntry.DateAdded = DateTime.Now;
                _unitOfWork.Carts.Update(cartEntry);
            }
            else
            {
                // Fetch perfume product (optional, for validation)
                var perfume = await _unitOfWork.PerfumeProducts.GetByIdAsync(perfumeId);
                if (perfume == null)
                {
                    throw new ArgumentException("Perfume not found");
                }

                // Create a new cart entry
                cartEntry = new Cart
                {
                    CartId = Guid.NewGuid(),
                    UserId = userId,
                    PerfumeId = perfumeId,
                    Quantity = quantity,
                    DateAdded = DateTime.Now
                };

                await _unitOfWork.Carts.CreateAsync(cartEntry);
            }

            // Save changes through the Unit of Work
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0;
        }

        public async Task<List<Cart>> GetCartByUserIdAsync_Optimized(Guid userId)
        {
            return await _unitOfWork.Carts
                .FindByCondition(c => c.UserId == userId)
                .ToListAsync();
        }
    }
}
