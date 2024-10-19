using Azure.Core;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using PerfumeStore.Repository;
using PerfumeStore.Repository.Model;
using PerfumeStore.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;



namespace PerfumeStore.Service.Service
{
    public class PerfumeService
    {
        private readonly UnitOfWork _unitOfWork;

        public PerfumeService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PerfumeProductModel> GetPerfumeByIdAsync(Guid id)
        {
            var perfume = await _unitOfWork.PerfumeProducts.GetByIdAsync(id);
            if (perfume == null) return null;

            return new PerfumeProductModel
            {
                PerfumeId = perfume.PerfumeId,
                Name = perfume.Name,
                Brand = perfume.Brand,
                Scent = perfume.Scent,
                Gender = perfume.Gender,
                StockQuantity = perfume.StockQuantity,
                Description = perfume.Description,
                ImageUrl = perfume.ImageUrl,
                ViewCount = perfume.ViewCount,
                Origin = perfume.Origin,
                ReleaseYear = perfume.ReleaseYear,
                Volume = perfume.Volume,
                Price = perfume.Price,
                Discount = perfume.Discount,
                TopNote = perfume.TopNote,
                MiddleNote = perfume.MiddleNote,
                BaseNote = perfume.BaseNote,
                DateAdded = perfume.DateAdded,
            };
        }

        public IEnumerable<PerfumeProduct> GetPerfumes(string search, string sortBy, bool desc, int page, int pageSize)
        {
            // Handle search (filter)
            Expression<Func<PerfumeProduct, bool>> filter = null;
            if (!string.IsNullOrEmpty(search))
            {
                filter = p => p.Gender.Contains(search);  // Filter by name
            }
            else
            {
                // If no search term is provided, just return all records
                filter = p => true;  // This will allow all results to be returned
            }

            // Handle sorting
            Func<IQueryable<PerfumeProduct>, IOrderedQueryable<PerfumeProduct>> orderBy = null;
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "name":
                        orderBy = p => desc ? p.OrderByDescending(x => x.Name) : p.OrderBy(x => x.Name);
                        break;
                    case "price":
                        orderBy = p => desc ? p.OrderByDescending(x => x.Price) : p.OrderBy(x => x.Price);
                        break;
                    default:
                        orderBy = p => p.OrderBy(x => x.Brand);  // Default sorting by name
                        break;
                }
            }
            else
            {
                // Default sorting if no sortBy is provided
                orderBy = p => p.OrderBy(x => x.Brand);  // Sorting by name if not specified
            }

            // Apply filtering
            var perfumes = _unitOfWork.PerfumeProducts.FindByCondition(filter);

            // Apply sorting
            if (orderBy != null)
            {
                perfumes = orderBy(perfumes);
            }

            // Apply paging
            return perfumes.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task<bool> UpdatePerfumeAsync(Guid id, PerfumeProductModel perfumeModel)
        {
            var perfumeToUpdate = await _unitOfWork.PerfumeProducts.GetByIdAsync(id);

            perfumeToUpdate.PerfumeId = perfumeModel.PerfumeId;
            perfumeToUpdate.Name = perfumeModel.Name;
            perfumeToUpdate.Brand = perfumeModel.Brand;
            perfumeToUpdate.Scent = perfumeModel.Scent;
            perfumeToUpdate.Gender = perfumeModel.Gender;
            perfumeToUpdate.StockQuantity = perfumeModel.StockQuantity;
            perfumeToUpdate.Description = perfumeModel.Description;
            perfumeToUpdate.ImageUrl = perfumeModel.ImageUrl;
            perfumeToUpdate.ViewCount = perfumeModel.ViewCount;
            perfumeToUpdate.Origin = perfumeModel.Origin;
            perfumeToUpdate.ReleaseYear = perfumeModel.ReleaseYear;
            perfumeToUpdate.Volume = perfumeModel.Volume;
            perfumeToUpdate.Price = perfumeModel.Price;
            perfumeToUpdate.Discount = perfumeModel.Discount;
            perfumeToUpdate.TopNote = perfumeModel.TopNote;
            perfumeToUpdate.MiddleNote = perfumeModel.MiddleNote;
            perfumeToUpdate.BaseNote = perfumeModel.BaseNote;
            perfumeToUpdate.DateAdded = perfumeModel.DateAdded;

            _unitOfWork.PerfumeProducts.Update(perfumeToUpdate);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<Guid> InsertPerfumeAsync(PerfumeProductModel perfumeModel)
        {
            //if (perfumeModel.PerfumeId == Guid.Empty) perfumeModel.PerfumeId = Guid.NewGuid();
            var perfumeEntity = new PerfumeProduct
            {
                PerfumeId = Guid.NewGuid(),
                Name = perfumeModel.Name,
                Brand = perfumeModel.Brand,
                Scent = perfumeModel.Scent,
                Gender = perfumeModel.Gender,
                StockQuantity = perfumeModel.StockQuantity,
                Description = perfumeModel.Description,
                ImageUrl = perfumeModel.ImageUrl,
                ViewCount = perfumeModel.ViewCount,
                Origin = perfumeModel.Origin,
                ReleaseYear = perfumeModel.ReleaseYear,
                Volume = perfumeModel.Volume,
                Price = perfumeModel.Price,
                Discount = perfumeModel.Discount,
                TopNote = perfumeModel.TopNote,
                MiddleNote = perfumeModel.MiddleNote,
                BaseNote = perfumeModel.BaseNote,
                DateAdded = perfumeModel.DateAdded,
            };

            await _unitOfWork.PerfumeProducts.CreateAsync(perfumeEntity);
            await _unitOfWork.SaveAsync();
            return perfumeEntity.PerfumeId;
        }

        public async Task<bool> DeletePerfumeAsync(Guid id)
        {
            var perfume = await _unitOfWork.PerfumeProducts.GetByIdAsync(id);
            if (perfume == null) return false;

            _unitOfWork.PerfumeProducts.Remove(perfume);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool>PerfumeExistAsync(Guid id)
        {
            return await _unitOfWork.PerfumeProducts.IsExist(id);
        }
    }
}
