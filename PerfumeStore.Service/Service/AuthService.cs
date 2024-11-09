using Google.Apis.Auth;
using PerfumeStore.Repository;
using PerfumeStore.Repository.Model;
using PerfumeStore.Service.BusinessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeStore.Service.Service
{
    public class AuthService
    {
        private readonly UnitOfWork _unitOfWork;

        public AuthService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public User GetUserByEmail(string email)
        {
            // Fetch a perfume by the 'Name' field using GetByCondition
            return _unitOfWork.Users.GetByCondition(p => p.Email == email);
        }

        public async Task UpdateUserAsync(User user)
        {
            // Update user details in your database
            await _unitOfWork.Users.UpdateAsync(user);
        }

        public async Task CreateUserAsync(User user)
        {
            // Create a new user in your database
            await _unitOfWork.Users.CreateAsync(user);
        }

        public User HandleGoogleLogin(GoogleJsonWebSignature.Payload payload)
        {
            // Check if the user already exists in the database
            var user = _unitOfWork.Users.GetByCondition(u => u.Email == payload.Email);

            if (user != null)
            {
                // User exists, update last login time
                user.LastLogin = DateTime.Now;
                _unitOfWork.Users.Update(user);
            }
            else
            {
                // User doesn't exist, create a new one
                user = new User
                {
                    Email = payload.Email,
                    FirstName = payload.Name,
                    LastLogin = DateTime.Now
                };

                _unitOfWork.Users.Create(user);
            }

            _unitOfWork.Save();  // Save changes to the database
            return user;
        }

        public async Task<BusinessModel.UserModel> AuthenticateGoogleUser(string email, string name)
        {
            var user = _unitOfWork.Users.GetByCondition(u => u.Email == email);

            if (user == null)
            {
                user = new Repository.Model.User
                {
                    UserId = Guid.NewGuid(),
                    Email = email,
                    FirstName = name,
                    LastName = "",
                    PasswordHash = "",
                    Role = "Customer",
                    Metadata = "",
                    Phone = "",
                    ProfileUrl = "",
                    Status = "isActive",
                    DateCreated = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    LastLogin = DateTime.UtcNow
                };

                _unitOfWork.Users.Create(user);
            }
            else
            {
                user.LastLogin = DateTime.UtcNow;
                user.UpdatedAt = DateTime.UtcNow;
                _unitOfWork.Users.Update(user);
            }

            await _unitOfWork.SaveAsync();

            return new UserModel
            {
                UserId = user.UserId,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                PasswordHash = user.PasswordHash,
                Phone = user.Phone,
                ProfileUrl = user.ProfileUrl,
                Metadata = user.Metadata,
                Address = user.Address,
                Status = user.Status,
                DateCreated = user.DateCreated,
                LastLogin = (DateTime)user.LastLogin
            };
        }
    }
}
