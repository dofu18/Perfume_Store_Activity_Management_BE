using Microsoft.IdentityModel.Tokens;
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
    public class UserService
    {
        private readonly UnitOfWork _unitOfWork;

        public UserService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserModel> GetUserByIdAsync(Guid id)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id);
            if (user == null) return null;
            
            return new UserModel
            {
                UserId = user.UserId,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PasswordHash = user.PasswordHash,
                Role = user.Role,
                Phone = user.Phone,
                ProfileUrl = user.ProfileUrl,
                Metadata = user.Metadata,
                Status = user.Status,
                LastLogin = user.LastLogin,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                DateCreated = user.DateCreated,
            };
        }

        public IEnumerable<User> GetAllUsers(string search, string sortBy, bool desc, int page, int pageSize)
        {
            Expression<Func<User, bool>> filter = null;

            if (!string.IsNullOrEmpty(search))
            {
                filter = p => p.FirstName.Contains(search);  // Filter by name
            }
            else
            {
                // If no search term is provided, just return all records
                filter = p => true;  // This will allow all results to be returned
            }

            // Handle sorting
            Func<IQueryable<User>, IOrderedQueryable<User>> orderBy = null;
            if (!string.IsNullOrEmpty(sortBy))
            {
                switch (sortBy.ToLower())
                {
                    case "firstName":
                        orderBy = p => desc ? p.OrderByDescending(x => x.FirstName) : p.OrderBy(x => x.FirstName);
                        break;
                    case "lastName":
                        orderBy = p => desc ? p.OrderByDescending(x => x.LastName) : p.OrderBy(x => x.LastName);
                        break;
                    default:
                        orderBy = p => p.OrderBy(x => x.FirstName);  // Default sorting by name
                        break;
                }
            }
            else
            {
                // Default sorting if no sortBy is provided
                orderBy = p => p.OrderBy(x => x.FirstName);  // Sorting by name if not specified
            }

            // Apply filtering
            var users = _unitOfWork.Users.FindByCondition(filter);

            // Apply sorting
            if (orderBy != null)
            {
                users = orderBy(users);
            }

            // Apply Paging
            return users.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task<bool> UpdateUserAsync(Guid id, UpdateUserModel userModel)
        {
            //var userToUpdate = await _unitOfWork.Users.GetByIdAsync(id);

            //userToUpdate.FirstName = userModel.FirstName;
            //userToUpdate.LastName = userModel.LastName;
            //userToUpdate.Email = userModel.Email;
            //userToUpdate.PasswordHash = userModel.PasswordHash;
            //userToUpdate.Role = userModel.Role;
            //userToUpdate.Phone = userModel.Phone;
            //userToUpdate.ProfileUrl = userModel.ProfileUrl;
            //userToUpdate.Metadata = userModel.Metadata;
            //userToUpdate.Status = userModel.Status;
            //userToUpdate.UpdatedAt = DateTime.Now;

            //_unitOfWork.Users.Update(userToUpdate);
            //await _unitOfWork.SaveAsync();
            //return true;
            var userToUpdate = await _unitOfWork.Users.GetByIdAsync(id);
            if (userToUpdate == null) return false;

            // Use reflection to update non-null properties, skipping mismatched types
            var properties = typeof(UpdateUserModel).GetProperties();
            foreach (var property in properties)
            {
                if (property.Name == nameof(userToUpdate.UserId))
                {
                    // Skip updating PerfumeId as it's the primary key and should remain immutable
                    continue;
                }

                var newValue = property.GetValue(userModel);
                if (newValue != null)
                {
                    var targetProperty = typeof(User).GetProperty(property.Name);
                    if (targetProperty != null && targetProperty.PropertyType == property.PropertyType)
                    {
                        targetProperty.SetValue(userToUpdate, newValue);
                    }
                }
            }

            userToUpdate.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.Users.Update(userToUpdate);
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}
