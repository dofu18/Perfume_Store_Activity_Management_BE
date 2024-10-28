using PerfumeStore.Repository;
using PerfumeStore.Repository.Model;
using PerfumeStore.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeStore.Service.Service
{
    public class PerfumeCharacteristicService
    {
        private readonly UnitOfWork _unitOfWork;

        public PerfumeCharacteristicService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PerfumeCharacteristicModel> GetCharacteristicByIdAsync(Guid id)
        {
            var characteristic = await _unitOfWork.PerfumeCharacteristics.GetByIdAsync(id);
            if (characteristic == null) return null;

            return new PerfumeCharacteristicModel
            {
                CharacteristicId = characteristic.CharacteristicId,
                PerfumeId = characteristic.PerfumeId,
                AttributeName = characteristic.AttributeName,
                AttributeValue = characteristic.AttributeValue,
                LastUpdated = characteristic.LastUpdated,
            };
        }

        public IEnumerable<PerfumeCharacteristic> GetCharacteristic(string search, string sortBy, bool desc, int page, int pageSize)
        {
            // Handle search (filter)
            Expression<Func<PerfumeCharacteristic, bool>> filter = null;
            if (!string.IsNullOrEmpty(search))
            {
                filter = p => p.AttributeName.Contains(search);  // Filter by name
            }
            else
            {
                // If no search term is provided, just return all records
                filter = p => true;  // This will allow all results to be returned
            }

            // Handle sorting
            Func<IQueryable<PerfumeCharacteristic>, IOrderedQueryable<PerfumeCharacteristic>> orderBy = null;
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "name":
                        orderBy = p => desc ? p.OrderByDescending(x => x.AttributeName) : p.OrderBy(x => x.AttributeName);
                        break;
                    case "value":
                        orderBy = p => desc ? p.OrderByDescending(x => x.AttributeValue) : p.OrderBy(x => x.AttributeValue);
                        break;
                    default:
                        orderBy = p => p.OrderBy(x => x.LastUpdated);  // Default sorting by name
                        break;
                }
            }
            else
            {
                // Default sorting if no sortBy is provided
                orderBy = p => p.OrderBy(x => x.LastUpdated);  // Sorting by name if not specified
            }

            // Apply filtering
            var characteristics = _unitOfWork.PerfumeCharacteristics.FindByCondition(filter);

            // Apply sorting
            if (orderBy != null)
            {
                characteristics = orderBy(characteristics);
            }

            // Apply paging
            return characteristics.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task<bool> UpdateCharacteristicAsync(Guid id, PerfumeCharacteristicModel characteristicModel)
        {
            var characteristicsToUpdate = await _unitOfWork.PerfumeCharacteristics.GetByIdAsync(id);

            characteristicsToUpdate.CharacteristicId = characteristicModel.CharacteristicId;
            characteristicsToUpdate.PerfumeId = characteristicModel.PerfumeId;
            characteristicsToUpdate.AttributeName = characteristicModel.AttributeName;
            characteristicsToUpdate.AttributeValue = characteristicModel.AttributeValue;
            characteristicsToUpdate.LastUpdated = characteristicModel.LastUpdated;

            _unitOfWork.PerfumeCharacteristics.Update(characteristicsToUpdate);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<Guid> InsertChacracteristicAsync(PerfumeCharacteristicModel characteristicModel)
        {
            //if (perfumeModel.PerfumeId == Guid.Empty) perfumeModel.PerfumeId = Guid.NewGuid();
            var characteristicEntity = new PerfumeCharacteristic
            {
                CharacteristicId = Guid.NewGuid(),
                PerfumeId = characteristicModel.PerfumeId,
                AttributeName = characteristicModel.AttributeName,
                AttributeValue = characteristicModel.AttributeValue,
                LastUpdated = DateTime.UtcNow,
            };

            await _unitOfWork.PerfumeCharacteristics.CreateAsync(characteristicEntity);
            await _unitOfWork.SaveAsync();
            return characteristicEntity.CharacteristicId;
        }

        public async Task<bool> DeleteCharacteristicAsync(Guid id)
        {
            var characteristic = await _unitOfWork.PerfumeCharacteristics.GetByIdAsync(id);
            if (characteristic == null) return false;

            _unitOfWork.PerfumeCharacteristics.Remove(characteristic);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> CharacteristicExistAsync(Guid id)
        {
            return await _unitOfWork.PerfumeCharacteristics.IsExist(id);
        }
    }
}
