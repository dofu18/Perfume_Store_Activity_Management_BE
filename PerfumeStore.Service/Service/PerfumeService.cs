using Azure.Core;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using PerfumeStore.Repository;
using PerfumeStore.Repository.Models;
using PerfumeStore.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
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


        public async Task<IEnumerable<PerfumeModel>> GetPerfumesAsync()
        {
            var perfumes = await _unitOfWork.Perfumes.GetAllAsync();
            return perfumes.Select(perfume => new PerfumeModel
            {
                PerfumeId = perfume.PerfumeId,
                ViewCount = perfume.ViewCount,
                Origin = perfume.Origin,
                ReleaseYear = perfume.ReleaseYear,
                Concentration = perfume.Concentration,
                Bartender = perfume.Bartender,
                FlavorGroup = perfume.FlavorGroup,
                Capacity = perfume.Capacity,
                Price = perfume.Price,
                Discount = perfume.Discount,
                TopNote = perfume.TopNote,
                MiddleNote = perfume.MiddleNote,
                BaseNote = perfume.BaseNote,
                //PerfumeEditions = perfume.PerfumeEditions,
            });
        }

        public async Task<PerfumeModel> GetPerfumeByIdAsync(Guid id)
        {
            var perfume = await _unitOfWork.Perfumes.GetByIdAsync(id);
            if (perfume == null) return null;

            return new PerfumeModel
            {
                PerfumeId = perfume.PerfumeId,
                ViewCount = perfume.ViewCount,
                Origin = perfume.Origin,
                ReleaseYear = perfume.ReleaseYear,
                Concentration = perfume.Concentration,
                Bartender = perfume.Bartender,
                FlavorGroup = perfume.FlavorGroup,
                Capacity = perfume.Capacity,
                Price = perfume.Price,
                Discount = perfume.Discount,
                TopNote = perfume.TopNote,
                MiddleNote = perfume.MiddleNote,
                BaseNote = perfume.BaseNote,
            };
        }

        public async Task<bool> UpdatePerfumeAsync(Guid id, PerfumeModel perfumeModel)
        {
            var perfumeToUpdate = await _unitOfWork.Perfumes.GetByIdAsync(id);

            perfumeToUpdate.ViewCount = perfumeModel.ViewCount;
            perfumeToUpdate.Origin = perfumeModel.Origin;
            perfumeToUpdate.ReleaseYear = perfumeModel.ReleaseYear;
            perfumeToUpdate.Concentration = perfumeModel.Concentration;
            perfumeToUpdate.Bartender = perfumeModel.Bartender;
            perfumeToUpdate.FlavorGroup = perfumeModel.FlavorGroup;
            perfumeToUpdate.Capacity = perfumeModel.Capacity;
            perfumeToUpdate.Price = perfumeModel.Price;
            perfumeToUpdate.Discount = perfumeModel.Discount;
            perfumeToUpdate.TopNote = perfumeModel.TopNote;
            perfumeToUpdate.MiddleNote = perfumeModel.MiddleNote;
            perfumeToUpdate.BaseNote = perfumeModel.BaseNote;

            _unitOfWork.Perfumes.Update(perfumeToUpdate);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<Guid> InsertPerfumeAsync(PerfumeModel perfumeModel)
        {
            //if (perfumeModel.PerfumeId == Guid.Empty) perfumeModel.PerfumeId = Guid.NewGuid();
            var perfumeEntity = new Perfume
            {
                PerfumeId = Guid.NewGuid(),
                ViewCount = perfumeModel.ViewCount,
                Origin = perfumeModel.Origin,
                ReleaseYear = perfumeModel.ReleaseYear,
                Concentration = perfumeModel.Concentration,
                Bartender = perfumeModel.Bartender,
                FlavorGroup = perfumeModel.FlavorGroup,
                Capacity = perfumeModel.Capacity,
                Price = perfumeModel.Price,
                Discount = perfumeModel.Discount,
                TopNote = perfumeModel.TopNote,
                MiddleNote = perfumeModel.MiddleNote,
                BaseNote = perfumeModel.BaseNote,
            };

            await _unitOfWork.Perfumes.CreateAsync(perfumeEntity);
            await _unitOfWork.SaveAsync();
            return perfumeEntity.PerfumeId;
        }

        public async Task<bool> DeletePerfumeAsync(Guid id)
        {
            var perfume = await _unitOfWork.Perfumes.GetByIdAsync(id);
            if (perfume == null) return false;

            _unitOfWork.Perfumes.Remove(perfume);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool>PerfumeExistAsync(Guid id)
        {
            return await _unitOfWork.Perfumes.IsExist(id);
        }
    }
}
