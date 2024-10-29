using Microsoft.AspNetCore.Mvc;
using PerfumeStore.Repository;
using PerfumeStore.Repository.Model;
using PerfumeStore.Service.BusinessModel;
using PerfumeStore.Service.BusinessModel.CustomResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeStore.Service.Service
{
    public class CategoryService
    {
        private readonly UnitOfWork _unitOfWork;

        public CategoryService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryModel> GetCategoryByIdAsync(Guid id)
        {
            var category = await _unitOfWork.Category.GetByIdAsync(id);
            if (category == null) return null;

            return new CategoryModel
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
            };
        }

        public IEnumerable<Category> GetCategories(string search, string sortBy, bool desc, int page, int pageSize)
        {
            // Handle search (filter)
            Expression<Func<Category, bool>> filter = null;
            if (!string.IsNullOrEmpty(search))
            {
                filter = p => p.CategoryName.Contains(search);  // Filter by name
            }
            else
            {
                // If no search term is provided, just return all records
                filter = p => true;  // This will allow all results to be returned
            }

            // Handle sorting
            Func<IQueryable<Category>, IOrderedQueryable<Category>> orderBy = null;
            if (!string.IsNullOrEmpty(sortBy))
            {
                orderBy = p => p.OrderBy(x => x.CategoryName);
            }

            // Apply filtering
            var categories = _unitOfWork.Category.FindByCondition(filter);

            // Apply sorting
            if (orderBy != null)
            {
                categories = orderBy(categories);
            }

            // Apply paging
            return categories.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task<IActionResult> UpdateCategoryAsync(Guid id, string categoryName)
        {
            try
            {
                var categoryToUpdate = await _unitOfWork.Category.GetByIdAsync(id);
                if (categoryToUpdate == null) return ErrorResp.NotFound("Category not found");

                categoryToUpdate.CategoryName = categoryName;

                _unitOfWork.Category.Update(categoryToUpdate);

                await _unitOfWork.SaveAsync();
                return SuccessResp.Ok("Update Category Successfully");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IActionResult> InsertCategoryAsync(string categoryName)
        {
            //if (perfumeModel.PerfumeId == Guid.Empty) perfumeModel.PerfumeId = Guid.NewGuid();
            var categoryEntity = new Category
            {
                CategoryId = Guid.NewGuid(),
                CategoryName = categoryName
            };

            var result =  await _unitOfWork.Category.CreateAsync(categoryEntity);
            if (result > 0)
            {
                await _unitOfWork.SaveAsync();
                return SuccessResp.Created("Create category successfully");
            }
            else
            {
                return ErrorResp.InternalServerError("Create category failed");
            }
        }

        public async Task<bool> DeleteCategoryAsync(Guid id)
        {
            var perfume = await _unitOfWork.PerfumeProducts.GetByIdAsync(id);
            if (perfume == null) return false;

            _unitOfWork.PerfumeProducts.Remove(perfume);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> PerfumeExistAsync(Guid id)
        {
            return await _unitOfWork.PerfumeProducts.IsExist(id);
        }
    }
}
