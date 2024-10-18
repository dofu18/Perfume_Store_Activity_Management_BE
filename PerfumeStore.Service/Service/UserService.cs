using Microsoft.IdentityModel.Tokens;
using PerfumeStore.Repository;
using PerfumeStore.Repository.Models;
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
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                ProfileUrl = user.ProfileUrl,
                Metadata = user.Metadata,
                Status = user.Status,
                LastLoginAt = user.LastLoginAt,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
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
    }
}
